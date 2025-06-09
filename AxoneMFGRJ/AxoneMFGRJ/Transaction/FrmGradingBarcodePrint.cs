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
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmGradingBarcodePrint : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        #region Property Settings

        public FrmGradingBarcodePrint()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            txtJangedNo.Focus();
            PnlPacketScan.Visible = false;

            DTabPacketLiveStock = ObjKapan.GetPacketDataForGradingBarcodePrint(1); // Pass 1 Coz Get Only Column Header

            MainGrid.DataSource = DTabPacketLiveStock;
            GrdDet.RefreshData();

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

            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        private void BtnSearch_Click(object sender, EventArgs e)
        {

            if (txtJangedNo.Text.Trim().Length == 0)
            {
                Global.Message("Janged No Is Required");
                txtJangedNo.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            ChkLabPacketScanning.Checked = false;

            DTabPacketLiveStock.Rows.Clear();

            DTabPacketLiveStock = ObjKapan.GetPacketDataForGradingBarcodePrint(Val.ToInt64(txtJangedNo.Text));
            MainGrid.DataSource = DTabPacketLiveStock;
            MainGrid.Refresh();

            ObjGridSelection.SelectAll();

            this.Cursor = Cursors.Default;
        }

        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();


            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = ObjGridSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BOTRN_SingleIssueReturn().PopupJangedForPrint("", 0, Val.SqlDate(DTPTransferDate.Value.ToShortDateString()));
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
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


        private void txtMarkerID_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);
                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtMarkerID.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtMarkerID.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnUpdateMarkerID_Click(object sender, EventArgs e)
        {
            Global.Message("YOU HAVE TO USE. FINAL ISSUE FORM FOR THIS UPDATE");
            return;

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Update");
                return;
            }

            if (Global.Confirm("Are you Sure You Want To Update [ " + txtMarkerID.Text + " ] To All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (txtMarkerID.Text.Trim().Length == 0)
            {
                if (Global.Confirm("Marker ID Is Blank , Means You Want To Make It Blank ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            string StrOpe = "";

            if (RbtMarker.Checked == true)
            {
                StrOpe = "Marker";
            }
            else if (RbtWorker.Checked == true)
            {
                StrOpe = "Worker";
            }

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                IntRes = IntRes + ObjPacket.UpdateMainMarkerIDInPacketMaster(StrOpe, Val.ToString(DRow["PACKET_ID"]), Val.ToInt64(txtMarkerID.Tag));
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("" + StrOpe + " ID ,,, SUCCESSFULLY UPDATED");
                BtnSearch_Click(null, null);
            }

        }

        private void BtnBarcodePrintCurrEmp_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
                    Val.ToString(DRow["PACKETNO"]),
                    Val.ToString(DRow["TAG"]),
                    Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    Val.ToString(DRow["LOTCARAT"]),
                    Val.ToString(DRow["EMPLOYEECODE"]));
            }

        }

        private void BtnPrintMarker_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
                    Val.ToString(DRow["PACKETNO"]),
                    Val.ToString(DRow["TAG"]),
                    Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    Val.ToString(DRow["LOTCARAT"]),
                    Val.ToString(DRow["MARKERCODE"]));
            }
        }

        private void BtnPrintWorker_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
                    Val.ToString(DRow["PACKETNO"]),
                    Val.ToString(DRow["TAG"]),
                    Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    Val.ToString(DRow["LOTCARAT"]),
                    Val.ToString(DRow["WORKERCODE"]));
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "EMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void BtnByGrdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {


                    if (Val.Val(DRow["BYGRDCARAT"]) > 0 && ChkLabPacketScanning.Checked == false)
                    {
                        Global.BarcodeBombayGrdPrint(Val.ToString(DRow["KAPANNAME"]),
                            Val.ToString(DRow["PACKETNO"]),
                            Val.ToString(DRow["TAG"]),
                            Val.ToString(DateTime.Parse(DRow["BYGRDDATE"].ToString()).ToString("dd-MM-yy")),
                            Val.ToString(DRow["BYGRDCARAT"]),
                            "", // Mark Code
                            Val.ToString(DRow["BYSHAPENAME"]),
                            Val.ToString(DRow["BYCOLORNAME"]),
                            Val.ToString(DRow["BYCLARITYNAME"]),
                            Val.ToString(DRow["BYCUTNAME"]),
                            Val.ToString(DRow["BYPOLNAME"]),
                            Val.ToString(DRow["BYSYMNAME"]),
                            Val.ToString(DRow["BYFLNAME"]),
                            Val.ToString(DRow["BYDIAMIN"]),
                            Val.ToString(DRow["BYDIAMAX"]),
                            Val.ToString(DRow["BYHEIGHT"]),
                            "",
                            Val.ToString(DRow["BYHELIUMRATIO"]),
                            Val.ToString(DRow["BYHELIUMTOTALDEPTH"]),
                            Val.ToString(DRow["BYHELIUMTABLEPC"])

                         );
                    }
                    else if (Val.Val(DRow["LABGRDCARAT"]) > 0 && ChkLabPacketScanning.Checked)
                    {
                        Global.BarcodeBombayGrdPrint(Val.ToString(DRow["KAPANNAME"]),
                            Val.ToString(DRow["PACKETNO"]),
                            Val.ToString(DRow["TAG"]),
                            Val.ToString(DateTime.Parse(DRow["LABGRDDATE"].ToString()).ToString("dd-MM-yy")),
                            Val.ToString(DRow["LABGRDCARAT"]),
                            "", // Mark Code
                            Val.ToString(DRow["LABSHAPENAME"]),
                            Val.ToString(DRow["LABCOLORNAME"]),
                            Val.ToString(DRow["LABCLARITYNAME"]),
                            Val.ToString(DRow["LABCUTNAME"]),
                            Val.ToString(DRow["LABPOLNAME"]),
                            Val.ToString(DRow["LABSYMNAME"]),
                            Val.ToString(DRow["LABFLNAME"]),
                            Val.ToString(DRow["LABDIAMIN"]),
                            Val.ToString(DRow["LABDIAMAX"]),
                            Val.ToString(DRow["LABHEIGHT"]),
                            Val.ToString(DRow["LABREPORTNO"]),
                            Val.ToString(DRow["LABHELIUMRATIO"]),
                            Val.ToString(DRow["LABHELIUMTOTALDEPTH"]),
                            Val.ToString(DRow["LABHELIUMTABLEPC"])
                         );
                    }

                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void ChkLabPacketScanning_CheckedChanged(object sender, EventArgs e)
        {
            //try
            //{
            //    PnlPacketScan.Visible = ChkLabPacketScanning.Checked;

            //    txtJangedNo.Focus();
            //    if (ChkLabPacketScanning.Checked)
            //    {
            //        txtKapan.Focus();
            //        txtJangedNo.Text = string.Empty;
            //    }

            //    DTabPacketLiveStock.Rows.Clear();
            //    txtKapan.Text = string.Empty;
            //    txtPacketNo.Text = string.Empty;
            //    txtTag.Text = string.Empty;
            //    GrdDet.RefreshData();
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}

        }

        private void txtTag_Validating(object sender, CancelEventArgs e)
        {
            try
            {

                if (Val.ToString(txtTag.Text).Trim().Equals(string.Empty))
                    return;



                if (txtKapan.Text.Trim().Length == 0)
                {
                    Global.MessageError("Kapan Is Required");
                    txtKapan.Focus();
                    return;
                }
                else if (txtPacketNo.Text.Trim().Length == 0)
                {
                    Global.MessageError("Packet No Is Required");
                    txtPacketNo.Focus();
                    return;
                }

                string Str = txtKapan.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text;

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtKapan.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                        && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
                        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
                        )
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapan.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapan.Focus();
                        break;
                    }
                }

                if (ISFind == false)
                {
                    bool ISExists = false;
                    DataRow DRow = ObjKapan.GetPacketDataForGradingBarcodePrint(Val.ToString(txtKapan.Text), Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text));

                    if (DRow == null)
                    {
                        Global.MessageError("Oops. " + Str + "  Packet Not Exists Please Check..!");

                        txtKapan.Text = "";
                        txtPacketNo.Text = "";
                        txtTag.Text = "";
                        txtKapan.Focus();
                        //txtFinalEmpCode.Focus();
                        return;
                    }
                    else
                    {

                        if (DRow.ItemArray.Count() > 0)
                        {
                            foreach (DataRow DD in DTabPacketLiveStock.Rows)
                            {
                                if (Val.ToString(DD["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"]))
                                {
                                    ISExists = true;
                                    break;
                                }
                            }

                            if (ISExists == false)
                            {
                                this.Cursor = Cursors.WaitCursor;
                                DataRow DRNew = DTabPacketLiveStock.NewRow();
                                foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                                {
                                    DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                                }
                                DTabPacketLiveStock.Rows.Add(DRNew);
                                //GrdDet.SetRowCellValue(DTabPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);

                                this.Cursor = Cursors.Default;
                            }
                        }

                        MainGrid.DataSource = DTabPacketLiveStock;
                        MainGrid.Refresh();
                        GrdDet.SetRowCellValue(DTabPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);
                    }
                }
                GrdDet.RefreshData();

                txtKapan.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtKapan.Focus();
                //bool ISFind = false;


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }

        }




    }
}
