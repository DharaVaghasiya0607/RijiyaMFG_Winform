using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.Transaction;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePacketConfirmationOld : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;        
        BODevGridSelection ObjGridSelSummary;

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
      
        #region Property Settings

        public FrmSinglePacketConfirmationOld()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            this.Show();
            DTPConfirmDate.Value = DateTime.Now;
            BtnSearch.PerformClick();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }
            else if (e.KeyCode == Keys.F5)
            {
                BtnSearch.PerformClick();
            }
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataSet DS = ObjTrn.GetPendingConfirmationData(0);

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DS.Tables[0];
                GrdDet.RefreshData();

                //MainGridSummary.DataSource = DS.Tables[1];
                //GrdDetSummary.RefreshData();

                if (MainGrid.RepositoryItems.Count == 0)
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

                CalculateSummary();

                GrdDet.EndUpdate();
                
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
           
        }

        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;
            int IntSelPcs = 0;
            double DouSelCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
            if (DTab == null)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                IntSelPcs = IntSelPcs + 1;
                DouSelCarat = DouSelCarat + Val.Val(DRow["ISSUECARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();
            txtSelectedPcs.Text = IntSelPcs.ToString();
            txtSelectedCarat.Text = DouSelCarat.ToString();

            //txtCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtTrfCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtAmount.Text = Math.Round(DouAmount, 3).ToString();
            //double DouRate = DouCarat != 0 ? Math.Round(DouAmount / DouCarat, 3) : 0;
            //txtRate.Text = Math.Round(DouRate, 3).ToString();
            //txtTransferRate.Text = Math.Round(DouRate, 3).ToString();
        }
        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }


        private DataTable GetTableOfSelectedRows(GridView pBoolView, Boolean pBoolIsSelect, BODevGridSelection pSelection)
        {
            if (pBoolView.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();

            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)pBoolView.DataSource).Table;

            if (pBoolIsSelect)
            {
                aryLst = pSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnConfirm_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
            if (DTab == null || DTab.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Update");
                return;
            }
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

           // DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = Val.SqlDate(DTPConfirmDate.Value.ToShortDateString());
                Property.PACKET_ID = Val.ToInt64(Val.ToString(DRow["PACKET_ID"]));
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

               Property =  ObjTrn.ConfirmJanged("PACKET", Property);
               if (Property.ReturnMessageType == "SUCCESS")
               {
                   IntRes++;
               }
               Property = null;
            }
            this.Cursor = Cursors.Default;
            
            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;

        }

        private void BtnConfirmJanged_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("Are You Sure To Confirm ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            DataTable DTab = GetTableOfSelectedRows(GrdDetSummary, true, ObjGridSelSummary);

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property.CONFDATE = Val.SqlDate(DTPConfirmDate.Value.ToShortDateString());
                Property.JANGEDNO = Val.ToInt64(DRow["JANGEDNO"]);

                Property = ObjTrn.ConfirmJanged("JANGED", Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    IntRes++;
                }
                Property = null;
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("Successfully Confirmed");
                BtnSearch.PerformClick();
            }
            DTab = null;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                    && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                    && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;

                    txtKapanName.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtBarcode.Text = string.Empty;

                    txtBarcode.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtBarcode.Text + " Packet Not In Stock Kindly Check");
                txtBarcode.Text = string.Empty;

            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }
      
        private void RbtBarcode_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtBarcode.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Focus();
            }
            else if (RbtJangedNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNo.Focus();
            }
            else if (RbtPacketNo.Checked)
            {
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtKapanName.Focus();
            }
            else if (RbtPktSerialNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }

            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void txtJangedNo_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtJangedNo.Text = string.Empty;

                    txtJangedNo.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtJangedNo.Text + " Packet Not In Stock Kindly Check");
                txtJangedNo.Text = string.Empty;

            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            bool ISFind = false;
            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                    && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRow["PKTSERIALNO"]).Trim()
                    )
                {
                    ISFind = true;
                    GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                    txtSrNoKapanName.Text = string.Empty;
                    txtSrNoSerialNo.Text = string.Empty;

                    txtSrNoKapanName.Focus();
                    CalculateSummary();
                    break;
                }
            }

            if (ISFind == false)
            {
                Global.MessageError(txtSrNoKapanName.Text + "-" + txtSrNoSerialNo.Text + " Packet Not In Stock Kindly Check");
                txtSrNoKapanName.Text = string.Empty;
            }
            else
            {
                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();
            }
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if(Val.ToInt32(TxtLimit.Text) <= 0)
                {
                    Global.Message("Please Enter Limit..");
                    TxtLimit.Focus();
                    return;
                }

                DataTable DTab = GetTableOfSelectedRows(GrdDet, true, ObjGridSelection);
                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                if (Val.ToInt32(TxtLimit.Text) > DTab.Rows.Count)
                {
                    Global.Message("Please Enter Valid Limit..");
                    TxtLimit.Focus();
                    return;
                }                
                if (Global.Confirm("Are You Sure To Update ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                int limit = Val.ToInt32(TxtLimit.Text);
                int jangedno = 0;
                DateTime DT = DateTime.Now;

                string StrDt = Val.SqlDate(Val.ToString(DT));
                int MaxJanged = ObjTrn.GetMaxDeptJangedNoStr(Val.SqlDate(DTPConfirmDate.Value.ToShortDateString()));
                int A = 1;               

                for (int i = 0; i < DTab.Rows.Count; i++)
                {
                    A = A + i;

                    if (i % limit == 0)
                    {
                        jangedno = MaxJanged + 1;
                        DTab.Rows[i]["DEPTJANGEDNO"] = jangedno;
                        MaxJanged = jangedno;
                    }
                    else
                    {
                        DTab.Rows[i]["DEPTJANGEDNO"] = jangedno;
                    }                    
                }

                DTab.TableName = "NewJangedNo";

                string NewJangedXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    NewJangedXml = sw.ToString();
                }

                string pStrReturnMessageDesc = ObjTrn.SaveNewDeptJangedNoStr(NewJangedXml, Val.SqlDate(DTPConfirmDate.Value.ToShortDateString()));
                if (pStrReturnMessageDesc == "SUCCESS")
                {
                    Global.Message("DeptJanged No Created Sucessfully");
                    BtnSearch_Click(null, null);
                }
                else
                {
                    Global.Message(pStrReturnMessageDesc);
                    //Global.Message("Opps...Something Wrong, Please Check...!");
                    return;
                }
            }
            catch(Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void TxtDeptJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPTJANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.PopupDeptJangedForPrint(0,null,"POPUP");
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtDeptJangedNo.Text = Val.ToString(FrmSearch.mDRow["DEPTJANGEDNO"]);
                        DtpJangedNo.Text = Val.ToString(FrmSearch.mDRow["DEPTJANGEDDATE"]);
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void btnSlipPrint_Click(object sender, EventArgs e)
        {
            try
            {
                int pIntJangedNo = 0;
                
                if (TxtDeptJangedNo.Text == string.Empty)
                {
                    Global.Message("Please Select Slip No First..");
                    TxtDeptJangedNo.Focus();
                    return;
                }
                pIntJangedNo = Val.ToInt32(TxtDeptJangedNo.Text);
                DataTable DTab = ObjTrn.PopupDeptJangedForPrint(pIntJangedNo, Val.SqlDate(DtpJangedNo.Value.ToShortDateString()),"");

                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                FrmReportViewer.ShowWithPrint("DeptJangedNoDetail", DTab);
            }
            catch(Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
    }
}
