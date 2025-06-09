using AxoneMFGRJ.Utility;
using BusLib.Transaction;
using DevExpress.XtraGrid.Columns;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmNonQCLiveStock : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        string strKapan = "";
        Int32 strPktNo = 0;
        string strTag = "";
        DataTable DTabQC = new DataTable();

        public FrmNonQCLiveStock()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            GrdDet.BeginUpdate();
            if (MainGrid.RepositoryItems.Count == 4)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            GrdDet.EndUpdate();

            txtKapan.Focus();
            this.Show();
        }

        private void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjKapan);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                strKapan = Val.ToString(txtKapan.Text);
                strPktNo = Val.ToInt32(txtPktNo.Text);
                strTag = Val.ToString(txtTag.Text);
                this.Cursor = Cursors.WaitCursor;
                DTabQC = ObjKapan.NonQCGetData(strKapan, strPktNo, strTag);
                this.Cursor = Cursors.Default;
                MainGrid.DataSource = DTabQC;
                MainGrid.Refresh();
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtKapan.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPktNo.Text) == 0)
                {
                    txtKapan.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapan.Focus();
                    return;
                }

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;
                        

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtKapan.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtPktNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapan.Text = string.Empty;
                        txtPktNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapan.Focus();                        
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }
                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.NonQCGetDataRowWise(txtKapan.Text, Val.ToInt32(txtPktNo.Text), txtTag.Text);
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapan.Text, Val.ToInt(txtPktNo.Text), txtTag.Text, 0);
                        string StrMsg = string.Empty;
                        if (DRowOS != null)
                        {
                            StrMsg = StrMsg + "Packet : " + Val.ToString(DRowOS["PACKETTAG"]) + "\n\n";
                            StrMsg = StrMsg + "EntryType : " + Val.ToString(DRowOS["CURR_ENTRYTYPE"]) + "\n\n";
                            StrMsg = StrMsg + "Employee : " + Val.ToString(DRowOS["TOEMPLOYEENAME"]) + "\n\n";
                            StrMsg = StrMsg + "Department : " + Val.ToString(DRowOS["TODEPARTMENTNAME"]) + "\n\n";
                            StrMsg = StrMsg + "For Process : " + Val.ToString(DRowOS["TOPROCESSNAME"]) + "\n\n";
                            StrMsg = StrMsg + "Manager : " + Val.ToString(DRowOS["TOMANAGERNAME"]) + "\n\n";
                        }

                        DRowOS = null;

                        Global.MessageError(txtKapan.Text + "-" + txtPktNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtKapan.Text = string.Empty;
                        txtPktNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapan.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabQC.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapan.Text = string.Empty;
                            txtPktNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapan.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabQC.NewRow();
                        foreach (DataColumn DCol in DTabQC.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabQC.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtKapan.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtPktNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabQC.AcceptChanges();
                                break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DRow = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();             
                MainGrid.Refresh();
             
                txtKapan.Text = string.Empty;
                txtPktNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapan.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtKapan.Focus();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKapan.Text = string.Empty;
            txtPktNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            ObjGridSelection.ClearSelection();

        }
    }
}
