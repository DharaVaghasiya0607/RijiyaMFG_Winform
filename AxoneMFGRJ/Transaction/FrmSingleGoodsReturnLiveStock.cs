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
using BusLib.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.Utils;
using System.Reflection;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleGoodsReturnLiveStock : DevExpress.XtraEditors.XtraForm
    {
        public delegate void SetControlValueCallback(Control oControl, string propName, object propValue);
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();

        DataTable DTabSelected = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        string mStrOperation = string.Empty;
        FORMTYPE mFormType = FORMTYPE.RETURNSTOCK;

        string mStrStockType = "";
        bool mISWithExtraStock = false;
        string mStrJangedNo = "";
        string StrUploadServer = string.Empty;
        string StrUploadServerUserName = string.Empty;
        string StrUploadServerPassword = string.Empty;
        string pStrOpeStatus = string.Empty;

        string mStrKapanName = "";

        string mStrBarcode = "";
        int mStrPktSrno = 0;
        Int32 mReason_Id = 0;
        string mRemark = "";
        DataTable DTabQC = new DataTable();

        private object lblMessage;

        public enum FORMTYPE
        {
            RETURNSTOCK,
            QCSTOCK
        }

        #region Property Settings

        public FrmSingleGoodsReturnLiveStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {

            mFormType = pFormType;
            if (mFormType == FORMTYPE.RETURNSTOCK)
            {
                PanelHeader.BackColor = Color.White;
                this.Text = "SINGLE GOODS RETURN";
                this.Name = "FrmSingleGoodsReturnStock";
                lblRoughMak.Text = "ROU.";
                BtnQCImportData.Visible = false;
                TxtEmployee.Enabled = false;
                TxtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;
                TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
            }
            else if (mFormType == FORMTYPE.QCSTOCK)
            {
                PanelHeader.BackColor = Color.FromArgb(192, 192, 255);
                this.Text = "QC STOCK GOODS RETURN";
                this.Name = "FrmSingleGoodsQCReturnStock";
                lblRoughMak.Text = "QC.";
                TxtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;
                TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                BtnQCImportData.Visible = true;
                TxtEmployee.Enabled = false;
            }

            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            // Action Button Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            GrdDet.Columns["PARTYNAME"].Visible = Property.ISALLOWINWARDINFO;
            GrdDet.Columns["PARTYCODE"].Visible = Property.ISALLOWINWARDINFO;
            GrdDet.Columns["INWARDNO"].Visible = Property.ISALLOWINWARDINFO;
            GrdDet.Columns["INWARDDATE"].Visible = Property.ISALLOWINWARDINFO;
            GrdDet.Columns["TOTALAGE"].Visible = Property.ISALLOWINWARDINFO;

            RbtFullStock.Checked = false;
            RbtDeptStock.Checked = false;
            RbtMYStock.Checked = false;
            RbtOtherStock.Checked = false;

            RbtBarcode.Checked = true;
            if (RbtBarcode.Checked == true)
            {
                PanelPacketNo.Visible = false;
                PanelJangedNo.Visible = false;
                PanelPktSerialNo.Visible = false;
                PnlDPTJangedno.Visible = false;
            }
            RbtFullStock.Enabled = Property.ISFULLSTOCK;
            //  ChkGrpJangedNo.Visible = Property.ISGROUPJANGADNO;

            RbtMYStock.Enabled = Property.ISMYSTOCK;
            RbtMYStock.Checked = Property.ISMYSTOCK;

            RbtDeptStock.Enabled = Property.ISDEPTSTOCK;
            RbtDeptStock.Checked = Property.ISDEPTSTOCK;

            RbtOtherStock.Enabled = Property.ISOTHERSTOCK;
            Property = null;

            TxtEmployee.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME;
            TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;


            this.Show();


            //DTabPacketLiveStock = ObjKapan.GetDataForKapanLiveStockForReturn("NONE", "NONE", "", 0, "", "", "", 0, 0);
            //MainGrid.DataSource = DTabPacketLiveStock;
            //MainGrid.Refresh();

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

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);

            }

            txtKapanName.Focus();
            RbtFullStock.Enabled = false;
            RbtDeptStock.Enabled = false;
            RbtOtherStock.Enabled = false;
            RbtMYStock.Checked = true;
            BtnSearch_Click(null, null);


            DTabQC.Columns.Add(new DataColumn("STONENO", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("SEARCHSTATUS", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("STONESTATUS", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("UPLOADSTATUS", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("SEARCHURL", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("SOURCEURL", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("DESTINATIONSERVERPATH", typeof(string)));
            DTabQC.Columns.Add(new DataColumn("DESTINATIONURL", typeof(string)));

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
            try
            {
                // this.Cursor = Cursors.WaitCursor;

                mStrStockType = "";

                if (RbtDeptStock.Checked == true)
                {
                    mStrStockType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    mStrStockType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    mStrStockType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    mStrStockType = RbtOtherStock.Tag.ToString();
                }

                txtKapanName.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;

                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;

                mStrPktSrno = Val.ToInt32(txtSrNoSerialNo.Text);

                // mISWithExtraStock = Val.ToBoolean(ChkWithExtraStock.Checked);
                mStrJangedNo = Val.ToString(txtJangedNo.Text);

                mStrKapanName = Val.ToString(txtKapanName.Text);
                mStrBarcode = Val.ToString(txtBarcode.Text);

                if (TxtDPTJangedNo.Text == string.Empty)
                {
                    TxtDPTJangedNo.Tag = string.Empty;
                }

                DTabPacketLiveStock.Rows.Clear();

                if (ObjGridSelection != null)
                {
                    ObjGridSelection.ClearSelection();
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnSearch.Enabled = false;
                PanelProgress1.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                BtnSearch.Enabled = true;
                PanelProgress1.Visible = false;
                // Global.Message(ex.Message.ToString());
            }
        }


        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
            //else 
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch.PerformClick();
            }
            else if (e.KeyCode == Keys.F6)
            {
                BtnAckPending.PerformClick();
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
                DouCarat = DouCarat + Val.Val(DRow["BALANCECARAT"]);
            }

            if (ObjGridSelection != null)
            {
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);
                if (DTab == null)
                {
                    return;
                }
                foreach (DataRow DRow in DTab.Rows)
                {
                    IntSelPcs = IntSelPcs + 1;
                    DouSelCarat = DouSelCarat + Val.Val(DRow["BALANCECARAT"]);
                }
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();
            txtSelectedPcs.Text = IntSelPcs.ToString();
            txtSelectedCarat.Text = DouSelCarat.ToString();
        }

        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDet);
        }



        private void MainGrid_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSummary();
        }

        private void FormTransfer_Closing(object sender, FormClosingEventArgs e)
        {
            DTabPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            CalculateSummary();
            txtKapanName.Focus();
        }

        private void BtnAckPending_Click(object sender, EventArgs e)
        {
            FrmSinglePacketConfirmation FrmSinglePacketConfirmation = new FrmSinglePacketConfirmation();
            FrmSinglePacketConfirmation.MdiParent = Global.gMainRef;
            FrmSinglePacketConfirmation.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
            FrmSinglePacketConfirmation.ShowForm();
        }

        private void txtTag_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtPacketNo.Text) == 0)
                {
                    txtKapanName.Focus();
                    return;
                }
                if (txtTag.Text.Trim().Length == 0)
                {
                    txtKapanName.Focus();
                    return;
                }

                if (Val.ISNumeric(txtTag.Text) == true)
                {
                    Char c = (Char)(64 + Val.ToInt(txtTag.Text));
                    txtTag.Text = c.ToString();
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtDeptStock.Checked == true)
                {
                    StrType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    StrType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    StrType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    StrType = RbtOtherStock.Tag.ToString();
                }

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
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForKapanLiveStockForReturnPacketInfo(mFormType.ToString(), StrType, txtKapanName.Text, Val.ToInt32(txtPacketNo.Text), txtTag.Text, "", "", 0, 0, 0, "","");
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, 0);
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

                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
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
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;

                txtKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtKapanName.Focus();
            }
        }

        private void txtBarcode_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtBarcode.Text.Trim().Length == 0)
                {
                    return;
                }


                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtDeptStock.Checked == true)
                {
                    StrType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    StrType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    StrType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    StrType = RbtOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtBarcode.Text = string.Empty;
                        txtBarcode.Focus();
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForKapanLiveStockForReturnPacketInfo(mFormType.ToString(), StrType, "", 0, "", txtBarcode.Text, "", 0, 0, 0, "","");
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, Val.ToInt64(txtBarcode.Text));
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

                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                                && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
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

                CalculateSummary();

                txtBarcode.Text = string.Empty;
                txtBarcode.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GrdDet_RowStyle(object sender, RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DR = GrdDet.GetDataRow(e.RowHandle);
            if (Val.ISDate(DR["CONFDATE"]) == true)
            {
                e.Appearance.BackColor = lblConfiredGoods.BackColor;
            }
            else if (Val.ISDate(DR["CONFDATE"]) == false)
            {
                e.Appearance.BackColor = lblPendingsGoods.BackColor;
            }
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitMaxRowCount = 5000000;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void RbtFullStock_CheckedChanged(object sender, EventArgs e)
        {
            BtnAckPending.Enabled = !RbtFullStock.Checked;

            //BtnSearch_Click(null, null);
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMEMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOEMPLOYEENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "FROMMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "FROMMANAGERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TOMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TOMANAGERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "TRANSBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "TRANSBYNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "CONFBYCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "CONFBYNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "ISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "ISSUENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "PREVISSUECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "PREVISSUENAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MAINMANAGERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MAINMANAGERNAME")));
                    return;
                }
                else if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "POLISHFINALEMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "POLISHFINALEMPLOYEENAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }
        private void lblSaveLayout_Click(object sender, EventArgs e)
        {
            Stream str = new System.IO.MemoryStream();
            GrdDet.SaveLayoutToStream(str);
            str.Seek(0, System.IO.SeekOrigin.Begin);
            StreamReader reader = new StreamReader(str);
            string text = reader.ReadToEnd();

            int IntRes = new BOTRN_KapanCreate().SaveGridLayout(this.Name, GrdDet.Name, text);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Saved");
            }
        }

        private void lblDefaultLayout_Click(object sender, EventArgs e)
        {
            int IntRes = new BOTRN_KapanCreate().DeleteGridLayout(this.Name, GrdDet.Name);
            if (IntRes != -1)
            {
                Global.Message("Layout Successfully Deleted");
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DTabPacketLiveStock = ObjKapan.GetDataForKapanLiveStockForReturn(mFormType.ToString(), mStrStockType, mStrKapanName, 0, "", mStrBarcode, mStrJangedNo, mStrPktSrno, Val.ToInt64(TxtEmployee.Tag), 0, "", Val.ToString(mFormType),Val.ToString(TxtDPTJangedNo.Text), Val.ToString(TxtDPTJangedNo.Tag));
            }
            catch (Exception ex)
            {
                BtnSearch.Enabled = true;
                PanelProgress1.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnSearch.Enabled = true;
                PanelProgress1.Visible = false;

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DTabPacketLiveStock;
                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();
                MainGrid.Refresh();
                GrdDet.EndUpdate();

                BtnAckPending.Text = "Ack.(0)";
                if (DTabPacketLiveStock.Rows.Count != 0)
                {
                    BtnAckPending.Text = "&Ack.(" + DTabPacketLiveStock.Rows[0]["PENDINGJANGED"].ToString() + ")";
                }
                CalculateSummary();
            }
            catch (Exception ex)
            {
                BtnSearch.Enabled = true;
                PanelProgress1.Visible = false;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnGroupJanged_Click(object sender, EventArgs e)
        {
            try
            {
                string ReturnValue = "";
                string ReturnMessageType = "";
                string ReturnMessageDesc = "";
                string ReturnValueJangedNo = "";

                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ISDate(DRow["CONFDATE"]) == false)
                    {
                        Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }

                    if (Val.ToString(DRow["ENTRYTYPE"]) != "EMPRET" && Val.ToString(DRow["ENTRYTYPE"]) != "TRANSFER")
                    {
                        Global.Message("You Can't Group This Packet" + " " + Val.ToString(DRow["PACKETNO"]) + " " + ", Please Check EntryType");
                        return;
                    }

                    if (Val.ToInt64(DRow["JANGEDNO"]) != Val.ToInt64(DRow["GRPJANGEDNO"]) && Val.ToInt64(DRow["GRPJANGEDNO"]) != 0)
                    {
                        Global.Message("This packet:" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + " " + "has already Group Janged No, Please Check EntryType");
                        return;
                    }
                }

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "STATUS");
                if (DTabDistinct == null)
                {
                    Global.Message("Status Is Blank");
                    return;
                }
                if (DTabDistinct.Rows.Count != 1)
                {
                    Global.Message("You Have Selected Multiple Status(Rough,Makable,Polish) Stone \n\nKindly Select Only One Status(Rough,Makable,Polish) Stones For Action ");
                    return;
                }


                DTabDistinct.Dispose();
                DTabDistinct = null;

                if (Global.Confirm("Are You Sure You Want To Create Group Janged ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DTab.TableName = "GroupJanged";

                string GrouJangedXml = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    GrouJangedXml = sw.ToString();
                }
                Int64 pIntGrpJangedNo = new BOTRN_SingleIssueReturn().SaveGroupJanged(GrouJangedXml);
                if (pIntGrpJangedNo != 0)
                {
                    Global.Message("GroupJanged No Created Sucessfully:" + Val.ToString(pIntGrpJangedNo));
                    BtnSearch_Click(null, null);
                }
                else
                {
                    Global.Message("Opps...Something Wrong, Please Check...!");
                    return;
                }
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
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
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Focus();
            }
            else if (RbtnDPTJangedNo.Checked)
            {

                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                TxtDPTJangedNo.Focus();
            }
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
            PnlDPTJangedno.Visible = RbtnDPTJangedNo.Checked;

        }

        private void txtSrNoSerialNo_Validated(object sender, EventArgs e)
        {
            try
            {

                if (txtSrNoKapanName.Text.Trim().Length == 0)
                {
                    return;
                }
                if (Val.ToInt(txtSrNoSerialNo.Text) == 0)
                {
                    txtSrNoSerialNo.Focus();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtDeptStock.Checked == true)
                {
                    StrType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    StrType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    StrType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    StrType = RbtOtherStock.Tag.ToString();
                }

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
                        GrdDet.FocusedRowHandle = 0;
                        break;
                    }
                }

                if (ISFind == false)
                {
                    DataRow DRow = ObjKapan.GetDataForKapanLiveStockForReturnPacketInfo(mFormType.ToString(), StrType, txtSrNoKapanName.Text, 0, "", "", "", Val.ToInt32(txtSrNoSerialNo.Text), 0, 0, "","");
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;

                        DataRow DRowOS = ObjKapan.GetDataForSinglePacketLiveStockCurrentOutstanding(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, 0);
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
                        Global.MessageError(txtSrNoKapanName.Text + "-" + txtSrNoSerialNo.Text + " Packet Not In Stock Kindly Check\n\n" + StrMsg);
                        txtSrNoKapanName.Text = string.Empty;
                        txtSrNoSerialNo.Text = string.Empty;

                        txtSrNoKapanName.Focus();
                        return;
                    }
                    else
                    {

                        //Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                        if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("This Packet Is Already Selected.");
                            txtKapanName.Text = string.Empty;
                            txtPacketNo.Text = string.Empty;
                            txtTag.Text = string.Empty;
                            txtSrNoKapanName.Focus();
                            return;
                        }
                        // 07-06-2019

                        DataRow DRNew = DTabPacketLiveStock.NewRow();
                        foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                        {
                            DRNew[DCol.ColumnName] = DRow[DCol.ColumnName];
                        }
                        DTabPacketLiveStock.Rows.Add(DRNew);


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                                && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRowGrid["PKTSERIALNO"]).Trim()
                                )
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
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
                //GrdDet.BestFitMaxRowCount = 500;
                //GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;

                txtSrNoKapanName.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtSrNoKapanName.Focus();
            }
        }

        private void txtJangedNo_Validated(object sender, EventArgs e) //#P : 10-06-2022
        {
            try
            {

                if (txtJangedNo.Text.Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtDeptStock.Checked == true)
                {
                    StrType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    StrType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    StrType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    StrType = RbtOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (txtJangedNo.Text.Trim() == Val.ToString(DRow["JANGEDNO"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        //break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DTabJangedData = ObjKapan.GetDataForKapanLiveStockForReturn(mFormType.ToString(), StrType, "", 0, "", "", Val.ToString(txtBarcode.Text), 0, 0, 0, "", Val.ToString(mFormType),"","");
                    if (DTabJangedData == null || DTabJangedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError("JangedNo : " + Val.ToString(txtJangedNo.Text) + " Is Not In Your Stock Please Check..");
                        txtJangedNo.Text = string.Empty;
                        txtJangedNo.Focus();
                        return;
                    }
                    else
                    {

                        ////Check That Packet Already Exists In Grid then Skip - 07-06-2019
                        //IEnumerable<DataRow> rowsNew = DTabPacketLiveStock.Rows.Cast<DataRow>();
                        //if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                        //{
                        //    this.Cursor = Cursors.Default;
                        //    Global.Message("This Packet Is Already Selected.");
                        //    txtKapanName.Text = string.Empty;
                        //    txtPacketNo.Text = string.Empty;
                        //    txtTag.Text = string.Empty;
                        //    txtKapanName.Focus();
                        //    return;
                        //}


                        var matched = from table1 in DTabPacketLiveStock.AsEnumerable()
                                      join table2 in DTabJangedData.AsEnumerable() on table1.Field<Int64>("JANGEDNO") equals table2.Field<Int64>("JANGEDNO")
                                      where table1.Field<Int64>("PACKET_ID") == table2.Field<Int64>("PACKET_ID")
                                      select table1;

                        if (matched.Any())
                        {
                            this.Cursor = Cursors.Default;
                            string Str = matched.FirstOrDefault()["JANGEDNO"].ToString();
                            Global.Message("This JangedNo Is Already Selected." + Str);
                            txtJangedNo.Text = string.Empty;
                            txtJangedNo.Focus();
                            return;
                        }

                        // 07-06-2019

                        foreach (DataRow DR in DTabJangedData.Rows)
                        {
                            DataRow DRNew = DTabPacketLiveStock.NewRow();
                            foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                            {
                                DRNew[DCol.ColumnName] = DR[DCol.ColumnName];
                            }
                            DTabPacketLiveStock.Rows.Add(DRNew);
                        }


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);
                            //if (txtKapanName.Text.Trim() == Val.ToString(DRowGrid["KAPANNAME"]).Trim()
                            //    && txtPacketNo.Text.Trim() == Val.ToString(DRowGrid["PACKETNO"]).Trim()
                            //    && txtTag.Text.Trim() == Val.ToString(DRowGrid["TAG"]).Trim()
                            //    )
                            if (txtJangedNo.Text.Trim() == Val.ToString(DRowGrid["JANGEDNO"]).Trim())
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
                                //break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DTabJangedData = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGrid.Refresh();

                CalculateSummary();

                txtJangedNo.Text = string.Empty;
                txtJangedNo.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        private void BtnDownload_Click(object sender, EventArgs e)
        {
            DTabSelected = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DTabSelected == null || DTabSelected.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Transfer");
                return;
            }
            if (Global.Confirm("Are You Sure To Download Selected File ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            foreach (DataRow DRow in DTabSelected.Rows)
            {
                if (mFormType == FORMTYPE.QCSTOCK && Val.ToString(DRow["PACKETSTATUS"]) != "RUNNING")
                {
                    Global.MessageError("Selected Packet's \n\n No : " + Val.ToString(DRow["PACKETNO"]) + " Is Not Running Status So You Can't Download.");
                    return;
                }

            }


            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            BtnSearch.Enabled = false;
            mStrOperation = "Download";
            PanelProgress.Visible = true;
            SetControlPropertyValue(lblMessage2, "Text", "Downloding Start");
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void SetControlPropertyValue(Control oControl, string propName, object propValue)
        {
            if (oControl.InvokeRequired)
            {
                SetControlValueCallback d = new SetControlValueCallback(SetControlPropertyValue);
                oControl.Invoke(d, new object[]
                        {
                            oControl,
                            propName,
                            propValue
                        });
            }
            else
            {
                Type t = oControl.GetType();
                PropertyInfo[] props = t.GetProperties();
                foreach (PropertyInfo p in props)
                {
                    if ((p.Name.ToUpper() == propName.ToUpper()))
                    {
                        p.SetValue(oControl, propValue, null);
                    }
                }
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            if (mStrOperation == "Download")
            {
                DOWNLOAD();
            }
            else if (mStrOperation == "Complete" && mFormType == FORMTYPE.RETURNSTOCK)
            {
                COMPLETE();
            }
            else if (mStrOperation == "Complete" && mFormType == FORMTYPE.QCSTOCK)
            {
                COMPLETE_QCRETURN();
            }
            else if (mStrOperation == "Reject" && mFormType == FORMTYPE.RETURNSTOCK)
            {
                REJECT();
            }
            else if (mStrOperation == "Reject" && mFormType == FORMTYPE.QCSTOCK)
            {               
                REJECT_QCRETURN();
            }
            else if (mStrOperation == "Cancel")
            {
                CANCEL();
            }
        }

        private void REJECT_QCRETURN()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                int IntSrNo = 0;

                Int64 StrJangedNo = 0;
                for (int j = 0; j < GrdDet.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDet.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDet.GetDataRow(j);
                        IntI++;
                        string StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);
                        string StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        string StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Has Start . (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                        //string StrUploadServer = Val.ToString(DRow["UPLOADSERVERPATH"]);
                        //string StrUploadServerUserName = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                        //string StrUploadServerPassword = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);

                        //string UploadExt = Val.ToString(DRow["UPLOADEXT"]);                      

                        int IntFileNeedToBeUpload = 0;
                        int IntUploadedCount = 0;

                        try
                        {

                            string StrQCSourceServer = "";
                            string StrQCSourceServerPath = "";
                            string StrQCSourceServerUserName = "";
                            string StrQCSourceServerPassword = "";

                            //Same Server Pr QC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..(Server noi Path Planning ni Process ma Upload ma je hase te consider karse
                            DataTable DTabPath = new DataTable();
                            int Employee_ID = 0;
                            TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                            Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                            DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                            if (DTabPath.Columns.Count == 0)
                            {
                                DataRow DRowQCServerUpload = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                                StrQCSourceServer = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                StrQCSourceServerPath = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                StrQCSourceServerUserName = Val.ToString(DRowQCServerUpload["UPLOADSERVERUSERNAME"]);
                                StrQCSourceServerPassword = Val.ToString(DRowQCServerUpload["UPLOADSERVERPASSWORD"]);
                            }
                            else
                            {
                                StrQCSourceServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                                StrQCSourceServerPath = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                                StrQCSourceServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                                StrQCSourceServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                            }
                            var lastFolder = Path.GetDirectoryName(StrQCSourceServer);
                            StrQCSourceServer = lastFolder;
                            StrQCSourceServerPath = lastFolder;
                            //Same Server Pr QC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..(Server noi Path Planning ni Process ma Upload ma je hase te consider karse
                           
                            string StrQCDestinationServerPath = "";
                            string StrQCDestinationServer = "";

                            string pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE + "\\" + StrPacketNo + "(GAL)" + ".cap";
                            string pStrQCDestinationServer = StrQCSourceServer + "\\QCFail\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE;
                            string pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".cap";

                            if (File.Exists(pStrQCSourceServerPath) == false)
                            {
                                 pStrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE + "\\" + StrPacketNo + "(GAL)" + ".adv";
                                 pStrQCDestinationServer = StrQCSourceServer + "\\QCFail\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE;
                                 pStrQCDestinationServerPath = pStrQCDestinationServer + "\\" + StrPacketNo + "(GAL)" + ".adv";
                            }

                            using (new AxonDataLib.BONetworkConnect(pStrQCDestinationServer, StrQCSourceServerUserName, StrQCSourceServerPassword))
                            {
                                if (Directory.Exists(pStrQCDestinationServer) == false)
                                {
                                    Directory.CreateDirectory(pStrQCDestinationServer);
                                }
                                File.Move(pStrQCSourceServerPath, pStrQCDestinationServerPath);
                            }
                            //End : Same Server Pr OC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..
                        }
                        catch (Exception ex)
                        {
                            //StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Error In Upload Same Server QC File. (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                            //SetControlPropertyValue(lblMessage2, "Text", StrMessage);
                            Global.MessageError("Error In Upload Same Server QC File For Packet : " + Val.ToString(DRow["PACKETNO"]) + "\n" + ex.Message.ToString());
                            return;
                        }

                        if (IntFileNeedToBeUpload == IntUploadedCount)
                        {
                            DRow["REJECT"] = true;
                        }
                        else
                        {
                            DRow["REJECT"] = false;
                            Global.Message("Stone No : " + Val.ToString(DRow["PACKETNO"]) + "\n\nTotal Required File : " + IntFileNeedToBeUpload + "\n\nTotal Uploaded File : " + IntUploadedCount + "\n\nPlease Check. Something goes wrong");
                            return;
                        }

                        DataTable DtabCheckPacketlist = DTabPacketLiveStock.Copy();
                        DtabCheckPacketlist.TableName = "CheckPacketList";
                        if (Val.ToBoolean(DRow["REJECT"]) == true)
                        {
                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                            Property.TRN_ID = Val.ToInt64(DRow["TRN_ID"]);
                            Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                            Property.RETURNTYPE = "REJECT";
                            Property.REJECTREASON_ID = Val.ToInt32(txtReason.Tag);
                            Property.REJECTREMARK = txtRemark.Text;
                            Property.REJECTAPPROVALSTATUS = "PENDING";
                            Property = ObjKapan.QCImportDataSave(Property, "", "REJECT");

                            if (Property.ReturnMessageType == "FAIL")
                            {
                                this.Cursor = Cursors.Default;
                                Global.MessageError(Property.ReturnMessageDesc);
                                //txtJangedNo.Text = "0";
                                //     this.Cursor = Cursors.WaitCursor;

                                if (Property.ReturnValue != "0")
                                    break;
                            }
                            Property = null;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BtnSearch.Enabled = true;
            BtnSearch.PerformClick();
            PanelProgress.Visible = false;
            BtnSearch_Click(null, null);
            if (mStrOperation == "Complete" && mFormType == FORMTYPE.QCSTOCK)
            {
                System.Threading.Thread.Sleep(800);                
                BtnQCImportData_Click(null, null);
                //ImportStoneForJob();
            }

            //if (GrpReject.Visible == true)
            //{
            //    BtnIssueReturnClose_Click(null, null);
            //}
        }


        private void DOWNLOAD()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                for (int j = 0; j < GrdDet.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDet.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDet.GetDataRow(j);
                        IntI++;
                        //string StrPacketNo =  Val.ToString(DRow["PACKETNO"]);
                        string StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);
                        string StrFileName = StrPacketNo;
                        string StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        string StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Has Start . (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                        string StrDownloadServer = Val.ToString(DRow["DOWNLOADSERVERPATH"]);
                        string StrDownloadServerUserName = Val.ToString(DRow["DOWNLOADSERVERUSERNAME"]);
                        string StrDownloadServerPassword = Val.ToString(DRow["DOWNLOADSERVERPASSWORD"]);
                        string StrDownloadFilePath = Val.ToString(DRow["DOWNLOADFILEPATH"]);
                        string DownloadExt = Val.ToString(DRow["DOWNLOADEXT"]);     
                        

                        using (new AxonDataLib.BONetworkConnect(StrDownloadServer, StrDownloadServerUserName, StrDownloadServerPassword))
                        {
                            string[] FileExtetion = DownloadExt.Split(',');

                            for (int i = 0; i < FileExtetion.Length; i++)
                            {
                                string DownloadLocalPath = Global.gStrLocalDownloadPath;
                               
                                try
                                {
                                    // Download Full Image Folder
                                    if (FileExtetion[i] == "{0}/*.*")
                                    {
                                        StrDownloadFilePath = Val.ToString(DRow["DOWNLOADFILEPATH"]);
                                        DownloadLocalPath = DownloadLocalPath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo);
                                        if (Directory.Exists(DownloadLocalPath) == false)
                                        {
                                            Directory.CreateDirectory(DownloadLocalPath);
                                        }
                                     

                                        StrDownloadFilePath = StrDownloadFilePath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo);

                                        FileInfo[] files = new DirectoryInfo(StrDownloadFilePath).GetFiles();

                                        int IntFileCount = files.Length;
                                        int Count = 0;
                                        //this section is what's really important for your application.
                                        foreach (FileInfo file in files)
                                        {
                                            Count++;
                                            SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Downloading ");
                                            file.CopyTo(DownloadLocalPath + "\\" + file.Name, true);
                                        }

                                        //DoubleData Download


                                        StrDownloadFilePath = Val.ToString(DRow["DOWNLOADFILEPATH"]);
                                        DownloadLocalPath = Global.gStrLocalDownloadPath;

                                        DownloadLocalPath = DownloadLocalPath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo) + "\\DoubleData\\";
                                        if (Directory.Exists(DownloadLocalPath) == false)
                                        {
                                            Directory.CreateDirectory(DownloadLocalPath);
                                        }
                                        StrDownloadFilePath = StrDownloadFilePath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo) + "\\DoubleData\\";

                                        files = new DirectoryInfo(StrDownloadFilePath).GetFiles();
                                     
                                        IntFileCount = files.Length;
                                        Count = 0;
                                        //this section is what's really important for your application.
                                        foreach (FileInfo file in files)
                                        {
                                            Count++;
                                            SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Downloading ");
                                            file.CopyTo(DownloadLocalPath + "\\" + file.Name, true);
                                        }

                                        //UPDOWN Download
                                        StrDownloadFilePath = Val.ToString(DRow["DOWNLOADFILEPATH"]);
                                        DownloadLocalPath = Global.gStrLocalDownloadPath + "\\" + StrPacketNo;

                                        DownloadLocalPath = DownloadLocalPath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo) + "\\UPDOWN\\";
                                        if (Directory.Exists(DownloadLocalPath) == false)
                                        {
                                            Directory.CreateDirectory(DownloadLocalPath);
                                        }
                                        StrDownloadFilePath = StrDownloadFilePath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo) + "\\UPDOWN\\";
                                        

                                        files = new DirectoryInfo(StrDownloadFilePath).GetFiles();

                                        IntFileCount = files.Length;
                                        Count = 0;
                                        //this section is what's really important for your application.
                                        foreach (FileInfo file in files)
                                        {
                                            Count++;
                                            SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Downloading ");
                                            file.CopyTo(DownloadLocalPath + "\\" + file.Name, true);
                                           
                                        }
                                    }
                                    else
                                    {                                      
                                        //StrDownloadFilePath = Val.ToString(DRow["DOWNLOADFILEPATH"]) + "\\" + StrKapanName;
                                        StrDownloadFilePath = mFormType == FORMTYPE.QCSTOCK ? Val.ToString(DRow["DOWNLOADFILEPATH"]) + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE : Val.ToString(DRow["DOWNLOADFILEPATH"]) + "\\" + StrKapanName; 
                                      
                                        if (Directory.Exists(DownloadLocalPath) == false)
                                        {
                                            Directory.CreateDirectory(DownloadLocalPath);                                           
                                        }
                                      
                                        if (FileExtetion[i].Contains(".cap"))
                                        {
                                            StrPacketNo = FileExtetion[i].Contains(".cap") ? StrFileName + "(GAL)" : StrFileName; //Coz .Cap File hase tyare StoneNo ma (GAL) word hase..eg.:N101-1-A(GAL)
                                            DownloadLocalPath = DownloadLocalPath + "\\" + StrPacketNo + ".cap";
                                            StrDownloadFilePath = StrDownloadFilePath + "\\" + StrPacketNo + ".cap";
                                        }
                                        else if (FileExtetion[i].Contains(".adv"))
                                        {
                                            StrPacketNo = FileExtetion[i].Contains(".adv") ? StrFileName + "(GAL)" : StrFileName; //Coz .Cap File hase tyare StoneNo ma (GAL) word hase..eg.:N101-1-A(GAL)
                                            DownloadLocalPath = DownloadLocalPath + "\\" + StrPacketNo + ".adv";
                                            StrDownloadFilePath = StrDownloadFilePath + "\\" + StrPacketNo + ".adv";
                                        }
                                       
                                        //Global.Message(StrDownloadFilePath);

                                        //Global.Message(DownloadLocalPath);
                                            
                                        if (File.Exists(StrDownloadFilePath) == false)
                                        {
                                            continue;
                                        }

                                        SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : File [" + string.Format(FileExtetion[i], StrPacketNo) + "] Downloading");                                        

                                        File.Copy(StrDownloadFilePath, DownloadLocalPath, true);
                                        

                                    }
                                    // Save History
                                    new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                        Val.ToString(DRow["PACKET_ID"]),
                                        StrPacketNo,
                                        "DOWNLOAD",
                                        "Only File Download",
                                        Val.ToInt(DRow["TOPROCESS_ID"]),
                                        "Success",
                                        StrMessage,
                                        string.Format(FileExtetion[i], StrPacketNo),
                                        StrDownloadFilePath,
                                        DownloadLocalPath,
                                        0);

                                    DRow["DOWNLOAD"] = true;

                                }
                                catch (Exception ex)
                                {
                                    StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Error In Download. (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                                    SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                                    // Save History
                                    new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                        Val.ToString(DRow["PACKET_ID"]),
                                        Val.ToString(DRow["PACKETNO"]),
                                        "DOWNLOAD",
                                        "Only File Download",
                                        Val.ToInt(DRow["TOPROCESS_ID"]),
                                        "Exception",
                                        ex.Message,
                                        string.Format(FileExtetion[i], StrPacketNo),
                                        StrDownloadFilePath,
                                        DownloadLocalPath,
                                        0);
                                }

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        }

        private void COMPLETE()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                int IntSrNo = 0;

                Int64 StrJangedNo = 0;
                for (int j = 0; j < GrdDet.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDet.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDet.GetDataRow(j);
                        IntI++;
                        string StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);
                        string StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        string StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Has Start . (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                        string StrUploadServer = Val.ToString(DRow["UPLOADSERVERPATH"]);
                        string StrUploadServerUserName = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                        string StrUploadServerPassword = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);

                        Global.Message(StrUploadServer);

                        string UploadExt = Val.ToString(DRow["UPLOADEXT"]);



                        int IntFileNeedToBeUpload = 0;
                        int IntUploadedCount = 0;

                        if (UploadExt != "")
                        {
                            using (new AxonDataLib.BONetworkConnect(StrUploadServer, StrUploadServerUserName, StrUploadServerPassword))
                            {
                                //Global.Message(StrUploadServer + "test1");
                                //Global.Message(StrUploadServerUserName + "test2");
                                //Global.Message(StrUploadServerPassword + "test3");

                                string[] FileExtetion = UploadExt.Split(',');
                                IntFileNeedToBeUpload = FileExtetion.Length;

                                for (int i = 0; i < FileExtetion.Length; i++)
                                {

                                    string UploadLocalPath = ""; //Global.gStrLocalOutputPath;
                                    string UploadLocalPathServer = "";
                                    string UploadLocalPathUserName = "";
                                    string UploadLocalPathPassword = "";
                                    if (!Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]).Trim().Equals(string.Empty))
                                    {
                                        UploadLocalPath = Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]); //+ "\\" + string.Format(FileExtetion[i], StrPacketNo); 
                                        UploadLocalPathServer = Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]);
                                        UploadLocalPathUserName = Val.ToString(DRow["LOCALOUTPUTPATHUSERNAME"]);
                                        UploadLocalPathPassword = Val.ToString(DRow["LOCALOUTPUTPATHPASSWORD"]);

                                        //Global.Message(UploadLocalPath + "test4");
                                        //Global.Message(UploadLocalPathServer + "test5");
                                        //Global.Message(UploadLocalPathUserName + "test6");
                                        //Global.Message(UploadLocalPathPassword + "test7");
                                    }
                                    else
                                    {
                                        UploadLocalPath = Global.gStrLocalOutputPath;
                                        //Global.Message(Global.gStrLocalOutputPath + "test8");
                                    }

                                    //string StrUploadFilePath = Val.ToInt32(DRow["TOPROCESS_ID"]) == 3846 ? Val.ToString(DRow["UPLOADFILEPATH"]) : Val.ToString(DRow["UPLOADFILEPATH"]) + "\\" + StrKapanName; //3846  : GalaxyOperator hase to Server Path ma ServerPath/QC nu Folder banavse je Parameter ma path QC sudhi no aapyo hase and ema .Cap file padse...OtherWise ServerPath/KapanName ma .cap file padse

                                    string StrUploadFilePath = "";
                                    
                                    //3846  : GalaxyOperator hase to Server Path ma ServerPath/QC nu Folder banavse je Parameter ma path QC sudhi no aapyo hase and ema .Cap file padse...OtherWise ServerPath/KapanName ma .cap file padse
                                    if(Val.ToInt32(DRow["TOPROCESS_ID"]) == 3846 && Val.Val(DRow["CARAT"]) <= 0.300) //0.300 Down na stones ne GalaxyOperator Parameter -> NonQC na folder ma file mukvani baki na stones direct GalaxyOperator Path(QC) ma mukvana..
                                    {
                                         StrUploadFilePath = Val.ToString(DRow["UPLOADFILEPATH"]) + "\\NonQC" ; 
                                    }
                                    else if (Val.ToInt32(DRow["TOPROCESS_ID"]) == 3846 && Val.Val(DRow["CARAT"]) > 0.300) 
                                    {
                                        StrUploadFilePath = Val.ToString(DRow["UPLOADFILEPATH"]);
                                    }
                                    else
                                    {
                                        StrUploadFilePath = Val.ToString(DRow["UPLOADFILEPATH"]) + "\\" + StrKapanName;
                                    }


                                    try
                                    {
                                        // Upload Full Image Folder
                                        if (FileExtetion[i] == "{0}/*.*")
                                        {
                                            // Upload Main Folder

                                            //StrUploadFilePath = StrUploadFilePath + "\\" + StrPacketNo + "\\";
                                            StrUploadFilePath = StrUploadFilePath + "\\" + StrKapanName + "\\" + StrPacketNo + "\\";
                                            UploadLocalPath = UploadLocalPath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo);
                                            if (Directory.Exists(StrUploadFilePath) == false)
                                            {
                                                Directory.CreateDirectory(StrUploadFilePath);
                                            }

                                            FileInfo[] files = new DirectoryInfo(UploadLocalPath).GetFiles();
                                            int IntFileCount = files.Length;
                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount - 1;
                                            }

                                            int Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrUploadFilePath + "\\" + file.Name, true);
                                                IntUploadedCount = IntUploadedCount + 1;
                                            }


                                            // DoubleData Folder

                                            string StrSubFolderUpload = StrUploadFilePath + "\\DoubleData\\";
                                            string StrSubFolderLocal = UploadLocalPath + "\\DoubleData\\";


                                            if (Directory.Exists(StrSubFolderUpload) == false)
                                            {
                                                Directory.CreateDirectory(StrSubFolderUpload);
                                            }

                                            files = new DirectoryInfo(StrSubFolderLocal).GetFiles();
                                            IntFileCount = files.Length;

                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount;
                                            }

                                            Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrSubFolderUpload + "\\" + file.Name, true);
                                                IntUploadedCount = IntUploadedCount + 1;
                                            }


                                            // UPDOWN Folder

                                            StrSubFolderUpload = StrUploadFilePath + "\\UPDOWN\\";
                                            StrSubFolderLocal = UploadLocalPath + "\\UPDOWN\\";


                                            if (Directory.Exists(StrSubFolderUpload) == false)
                                            {
                                                Directory.CreateDirectory(StrSubFolderUpload);
                                            }

                                            files = new DirectoryInfo(StrSubFolderLocal).GetFiles();
                                            IntFileCount = files.Length;

                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount;
                                            }

                                            Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrSubFolderUpload + "\\" + file.Name, true);
                                                IntUploadedCount = IntUploadedCount + 1;
                                            }
                                        }
                                        else
                                        {

                                            UploadLocalPath = UploadLocalPath + "\\" + string.Format(FileExtetion[i], StrPacketNo);
                                            if (Directory.Exists(StrUploadFilePath) == false)
                                            {
                                                Directory.CreateDirectory(StrUploadFilePath);
                                            }

                                            if (FileExtetion[i].Contains(".cap"))
                                            {
                                                StrPacketNo = FileExtetion[i].Contains(".cap") ? StrPacketNo + "(GAL)" : StrPacketNo; //Coz .Cap File hase tyare StoneNo ma (GAL) word Save thavo joiye While Upload in Server..eg.:N101-1-A(GAL)..But Jya thi file lese tya to N101-1-A name ni file hase etle UploadLocalPath aa set thay jaay pa6i mukyu 6e
                                            }
                                            if (FileExtetion[i].Contains(".adv"))
                                            {
                                                StrPacketNo = FileExtetion[i].Contains(".adv") ? StrPacketNo + "(GAL)" : StrPacketNo; //Coz .Cap File hase tyare StoneNo ma (GAL) word Save thavo joiye While Upload in Server..eg.:N101-1-A(GAL)..But Jya thi file lese tya to N101-1-A name ni file hase etle UploadLocalPath aa set thay jaay pa6i mukyu 6e
                                            }


                                            if (File.Exists(UploadLocalPath) == false)
                                            {
                                                continue ;
                                            }

                                            StrUploadFilePath = StrUploadFilePath + "\\" + string.Format(FileExtetion[i], StrPacketNo);

                                            SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : File [" + string.Format(FileExtetion[i], StrPacketNo) + "] Uploading ");

                                            //Global.Message("LocalPath : " + UploadLocalPath);
                                            //Global.Message("ServerPath : " + StrUploadFilePath);

                                            //File.Copy(UploadLocalPath, StrUploadFilePath, true);  //Cmnt & Add : #p : 20-10-2022
                                            if (UploadLocalPathUserName.Trim().Equals(string.Empty))
                                            {
                                                File.Copy(UploadLocalPath, StrUploadFilePath, true);
                                            }
                                            else
                                            {
                                                string StrStartupPath = Application.StartupPath + "\\" + string.Format(FileExtetion[i], StrPacketNo);
                                                using (new AxonDataLib.BONetworkConnect(UploadLocalPathServer, UploadLocalPathUserName, UploadLocalPathPassword))
                                                {
                                                    File.Copy(UploadLocalPath, StrStartupPath, true);
                                                }
                                                using (new AxonDataLib.BONetworkConnect(StrUploadServer, StrUploadServerUserName, StrUploadServerPassword))
                                                {
                                                    File.Copy(StrStartupPath, StrUploadFilePath, true);
                                                }
                                                File.Delete(StrStartupPath);
                                            }
                                            IntUploadedCount = IntUploadedCount + 1;

                                        }

                                        // Save History
                                        new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                            Val.ToString(DRow["PACKET_ID"]),
                                            StrPacketNo,
                                            "UPLOAD",
                                            "Only File Download",
                                            Val.ToInt(DRow["TOPROCESS_ID"]),
                                            "Success",
                                            StrMessage,
                                            string.Format(FileExtetion[i], StrPacketNo),
                                            StrUploadFilePath,
                                            UploadLocalPath,
                                            0);



                                    }
                                    catch (Exception ex)
                                    {
                                        StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Error In Download. (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                                        // Save History
                                        new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                            Val.ToString(DRow["PACKET_ID"]),
                                            StrPacketNo,
                                            "UPLOAD",
                                            "Only File Download",
                                            Val.ToInt(DRow["TOPROCESS_ID"]),
                                            "Exception",
                                            ex.Message,
                                            string.Format(FileExtetion[i], StrPacketNo),
                                            StrUploadFilePath,
                                            UploadLocalPath,
                                            0);
                                    }
                                }
                            }
                        }

                        if (IntFileNeedToBeUpload == IntUploadedCount)
                        {
                            DRow["COMPLETE"] = true;
                        }
                        else
                        {
                            DRow["COMPLETE"] = false;
                            Global.Message("Stone No : " + Val.ToString(DRow["PACKETNO"]) + "\n\nTotal Required File : " + IntFileNeedToBeUpload + "\n\nTotal Uploaded File : " + IntUploadedCount + "\n\nPlease Check. Something goes wrong");
                            return;
                        }

                        DataTable DtabCheckPacketlist = DTabPacketLiveStock.Copy();
                        DtabCheckPacketlist.TableName = "CheckPacketList";
                        if (Val.ToBoolean(DRow["COMPLETE"]) == true)
                        {
                            IntSrNo++;
                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                            Property.TRN_ID = 0;
                            Property.OLDTRN_ID = Val.ToInt64(DRow["TRN_ID"].ToString());

                            //Property.CLIENTREFNO = Val.ToString(DRow["CLIENTREFNO"].ToString());
                            Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"]);
                            Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"]);
                            Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);
                            Property.TAG = Val.ToString(DRow["TAG"]);

                            Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());
                            Property.JANGEDNO = Val.ToInt64(StrJangedNo);
                            Property.ENTRYSRNO = IntI;
                            Property.ENTRYTYPE = "EMPRET";

                            Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);
                            Property.TODEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);

                            Property.FROMMANAGER_ID = Val.ToInt64(DRow["TOMANAGER_ID"]);
                            Property.TOMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);

                            Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);
                            Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);

                            Property.FROMPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                            Property.TOPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                            Property.NEXTPROCESS_ID = Val.ToInt32(DRow["NEXTPROCESS_ID"]);
                            //Property.NEXTPROCESS = Val.ToString(DRow["NEXTPROCESS"]);

                            Property.READYPCS = 1;
                            Property.READYCARAT = Val.Val(DRow["CARAT"]);
                            Property.MACHINE_ID = Val.ToInt(DRow["MACHINE_ID"]);

                            Property.RETURNTYPE =  (Val.ToInt32(DRow["TOPROCESS_ID"]) == 3846 && Val.Val(DRow["CARAT"]) <= 0.300) ? "NONQC" :  "COMPLETE";  //3846 : GalaxyOperator

                            Property.DOWNLOAD = Val.ToBoolean(DRow["DOWNLOAD"]);
                            Property.COMPLETE = true;
                            Property.CANCEL = false;
                            Property.REJECT = false;

                            Property.TRANSDATE = Val.SqlDate(DateTime.Now.ToShortDateString());
                            Property.REMARK = Val.ToString(DRow["REMARK"]);

                            Property = ObjTrn.TransferGoods(Property);
                            StrJangedNo = Val.ToInt64(Property.JANGEDNO);
                            //txtJangedNo.Text = Property.JANGEDNO.ToString();
                            //ValJangedNoRet = Property.JANGEDNORet;
                            //ValJangedNoTran = Property.JANGEDNOTran;

                            if (Property.ReturnMessageType == "FAIL")
                            {
                                this.Cursor = Cursors.Default;
                                Global.MessageError(Property.ReturnMessageDesc);
                                //txtJangedNo.Text = "0";
                                //     this.Cursor = Cursors.WaitCursor;

                                if (Property.ReturnValue == "-5")
                                    break;
                            }
                            Property = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        }

        private void COMPLETE_QCRETURN()  //.cap file pr j Work thase in Whole QC Process
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                int IntSrNo = 0;
                string StrPacketNo = "";
                Int64 StrJangedNo = 0;
                for (int j = 0; j < GrdDet.RowCount; j++)
                {
                    if (Val.ToBoolean(GrdDet.GetRowCellValue(j, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDet.GetDataRow(j);
                        IntI++;
                         StrPacketNo = Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + "-" + Val.ToString(DRow["TAG"]);
                         string StrFileName = StrPacketNo;


                        string StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        string StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Has Start . (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                        string StrUploadServer = Val.ToString(DRow["UPLOADSERVERPATH"]);
                        string StrUploadServerUserName = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                        string StrUploadServerPassword = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);

                        string UploadExt = Val.ToString(DRow["UPLOADEXT"]);

                        int IntFileNeedToBeUpload = 0;
                        int IntUploadedCount = 0;                       


                        if (UploadExt != "")
                        {
                            using (new AxonDataLib.BONetworkConnect(StrUploadServer, StrUploadServerUserName, StrUploadServerPassword))
                            {
                                string[] FileExtetion = UploadExt.Split(',');
                                IntFileNeedToBeUpload = FileExtetion.Length;

                                for (int i = 0; i < FileExtetion.Length; i++)
                                {

                                    string UploadLocalPath = ""; //Global.gStrLocalOutputPath;
                                    string UploadLocalPathServer = "";
                                    string UploadLocalPathUserName = "";
                                    string UploadLocalPathPassword = "";
                                    if (!Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]).Trim().Equals(string.Empty))
                                    {
                                        UploadLocalPath = Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]); //+ "\\" + string.Format(FileExtetion[i], StrPacketNo); 
                                        UploadLocalPathServer = Val.ToString(DRow["LOCALOUTPUTPATHSERVER"]);
                                        UploadLocalPathUserName = Val.ToString(DRow["LOCALOUTPUTPATHUSERNAME"]);
                                        UploadLocalPathPassword = Val.ToString(DRow["LOCALOUTPUTPATHPASSWORD"]);                                       
                                    }
                                    else
                                    {
                                        UploadLocalPath = Global.gStrLocalOutputPath;
                                    }

                                    string StrUploadFilePath = StrUploadServer; //+ "\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE;                                    

                                    try
                                    {
                                        // Upload Full Image Folder
                                        if (FileExtetion[i] == "{0}/*.*")
                                        {
                                            // Upload Main Folder

                                            //StrUploadFilePath = StrUploadFilePath + "\\" + StrPacketNo + "\\";
                                            StrUploadFilePath = StrUploadFilePath + "\\" + StrKapanName + "\\" + StrPacketNo + "\\";
                                            UploadLocalPath = UploadLocalPath + "\\" + string.Format(FileExtetion[i].Replace("*.*", ""), StrPacketNo);
                                           
                                            if (Directory.Exists(StrUploadFilePath) == false)
                                            {
                                                Directory.CreateDirectory(StrUploadFilePath);
                                            }

                                            FileInfo[] files = new DirectoryInfo(UploadLocalPath).GetFiles();
                                            int IntFileCount = files.Length;
                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount - 1;
                                            }

                                            int Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrUploadFilePath + "\\" + file.Name, true);
                                                IntUploadedCount = IntUploadedCount + 1;
                                               
                                            }

                                            // DoubleData Folder

                                            string StrSubFolderUpload = StrUploadFilePath + "\\DoubleData\\";
                                            string StrSubFolderLocal = UploadLocalPath + "\\DoubleData\\";


                                            if (Directory.Exists(StrSubFolderUpload) == false)
                                            {
                                                Directory.CreateDirectory(StrSubFolderUpload);
                                            }

                                            files = new DirectoryInfo(StrSubFolderLocal).GetFiles();
                                            IntFileCount = files.Length;

                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount;
                                            }

                                            Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrSubFolderUpload + "\\" + file.Name, true);                                               
                                                IntUploadedCount = IntUploadedCount + 1;
                                            }


                                            // UPDOWN Folder

                                            StrSubFolderUpload = StrUploadFilePath + "\\UPDOWN\\";
                                            StrSubFolderLocal = UploadLocalPath + "\\UPDOWN\\";


                                            if (Directory.Exists(StrSubFolderUpload) == false)
                                            {
                                                Directory.CreateDirectory(StrSubFolderUpload);
                                            }

                                            files = new DirectoryInfo(StrSubFolderLocal).GetFiles();
                                            IntFileCount = files.Length;

                                            if (IntFileCount != 0)
                                            {
                                                IntFileNeedToBeUpload = IntFileNeedToBeUpload + IntFileCount;
                                            }

                                            Count = 0;
                                            //this section is what's really important for your application.
                                            foreach (FileInfo file in files)
                                            {
                                                Count++;
                                                SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : " + Count + "/" + IntFileCount + " File [" + file.Name + "] Uploading ");
                                                file.CopyTo(StrSubFolderUpload + "\\" + file.Name, true);                                               
                                                IntUploadedCount = IntUploadedCount + 1;
                                            }
                                        }
                                        else
                                        {
                                            //UploadLocalPath = UploadLocalPath + "\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE + "\\" + string.Format(FileExtetion[i], StrPacketNo);

                                            if (FileExtetion[i].Contains(".cap"))
                                            {
                                                StrPacketNo = FileExtetion[i].Contains(".cap") ? StrFileName + "(GAL)" : StrFileName; //Coz .Cap File hase tyare StoneNo ma (GAL) word hase..eg.:N101-1-A(GAL)

                                            }
                                            if (FileExtetion[i].Contains(".adv"))
                                            {
                                                StrPacketNo = FileExtetion[i].Contains(".adv") ? StrFileName + "(GAL)" : StrFileName; //Coz .Cap File hase tyare StoneNo ma (GAL) word hase..eg.:N101-1-A(GAL)
                                            }

                                            UploadLocalPath = UploadLocalPath + "\\" + string.Format(FileExtetion[i], StrPacketNo);

                                            if (File.Exists(UploadLocalPath) == false)
                                            {
                                                IntUploadedCount = IntUploadedCount + 1;
                                                continue;
                                            }

                                            if (Directory.Exists(StrUploadFilePath) == false)
                                            {
                                                Directory.CreateDirectory(StrUploadFilePath);
                                            }
                                            StrUploadFilePath = StrUploadFilePath + "\\" + string.Format(FileExtetion[i], StrPacketNo);

                                            SetControlPropertyValue(lblMessage2, "Text", StrPacketNo + " : File [" + string.Format(FileExtetion[i], StrPacketNo) + "] Uploading ");

                                            //File.Copy(UploadLocalPath, StrUploadFilePath, true);  //Cmnt & Add : #p : 20-10-2022
                                            if (UploadLocalPathUserName.Trim().Equals(string.Empty))
                                            {
                                                File.Copy(UploadLocalPath, StrUploadFilePath, true);                                                

                                            }
                                            else
                                            {
                                                using (new AxonDataLib.BONetworkConnect(UploadLocalPathServer, UploadLocalPathUserName, UploadLocalPathPassword))
                                                {
                                                    File.Copy(UploadLocalPath, StrUploadFilePath, true);                                                    
                                                }
                                            }

                                            IntUploadedCount = IntUploadedCount + 1;
                                        }

                                        Global.Message("Save History");
                                        // Save History
                                        new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                            Val.ToString(DRow["PACKET_ID"]),
                                            StrPacketNo,
                                            "UPLOAD",
                                            "Only File Download",
                                            Val.ToInt(DRow["TOPROCESS_ID"]),
                                            "Success",
                                            StrMessage,
                                            string.Format(FileExtetion[i], StrPacketNo),
                                            StrUploadFilePath,
                                            UploadLocalPath,
                                            0);
                                        Global.Message("Save History Done");

                                        try
                                        {
                                            string StrQCSourceServer = "";
                                            string StrQCSourceServerPath = "";
                                            string StrQCSourceServerUserName = "";
                                            string StrQCSourceServerPassword = "";

                                            //Same Server Pr QC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..(Server noi Path Planning ni Process ma Upload ma je hase te consider karse
                                            DataTable DTabPath = new DataTable();
                                            int Employee_ID = 0;
                                            TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                                            Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                                            DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                                            if (DTabPath.Columns.Count == 0)
                                            {
                                                DataRow DRowQCServerUpload = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                                                StrQCSourceServer = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                                StrQCSourceServerPath = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                                StrQCSourceServerUserName = Val.ToString(DRowQCServerUpload["UPLOADSERVERUSERNAME"]);
                                                StrQCSourceServerPassword = Val.ToString(DRowQCServerUpload["UPLOADSERVERPASSWORD"]);
                                            }
                                            else
                                            {
                                                StrQCSourceServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                                                StrQCSourceServerPath = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                                                StrQCSourceServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                                                StrQCSourceServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                                            }
                                            var lastFolder = Path.GetDirectoryName(StrQCSourceServerPath);
                                            StrQCSourceServerPath = lastFolder;
                                            StrQCSourceServer = lastFolder;                                            

                                            //Same Server Pr QC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..(Server noi Path Planning ni Process ma Upload ma je hase te consider karse
                                            //DataRow DRowQCServerUpload = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                                            //string StrQCSourceServer = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                            //string StrQCSourceServerPath = Val.ToString(DRowQCServerUpload["UPLOADSERVERPATH"]);
                                            //string StrQCSourceServerUserName = Val.ToString(DRowQCServerUpload["UPLOADSERVERUSERNAME"]);
                                            //string StrQCSourceServerPassword = Val.ToString(DRowQCServerUpload["UPLOADSERVERPASSWORD"]);
                                            string StrQCDestinationServerPath = "";
                                            string StrQCDestinationServer = "";
                                           
                                            //string StrPath = GetHierarchyOfFolder(StrQCSourceServer, "", ".cap");
                                            ////var lastFolder = Path.GetDirectoryName(StrPath);                                            

                                            StrQCSourceServerPath = StrQCSourceServerPath + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE + "\\" + string.Format(FileExtetion[i], StrPacketNo);
                                            StrQCDestinationServer = StrQCSourceServer + "\\QCComplete\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE;
                                            StrQCDestinationServerPath = StrQCDestinationServer + "\\" + string.Format(FileExtetion[i], StrPacketNo);

                                            using (new AxonDataLib.BONetworkConnect(StrQCSourceServer, StrQCSourceServerUserName, StrQCSourceServerPassword))
                                            {
                                                if (Directory.Exists(StrQCDestinationServer) == false)
                                                {
                                                    Directory.CreateDirectory(StrQCDestinationServer);
                                                }
                                                File.Move(StrQCSourceServerPath, StrQCDestinationServerPath);
                                               
                                            }
                                            //End : Same Server Pr OC -> QCPending na Folder mathi File remove thay ne QCComplete na Folder Replace thase..

                                            EmployeeActionRightsProperty PropertyRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
                                            string StrMainServerPath = Val.ToString(PropertyRights.QCMAINSERVERPATH);

                                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                                            Property.TRN_ID = Val.ToInt64(DRow["TRN_ID"]);
                                            Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                                           
                                            if (StrQCDestinationServerPath.Contains(".cap"))
                                            {
                                                Property.FName = StrPacketNo + ".cap";
                                                Property.Ext = ".cap";
                                                Property.SPath = StrMainServerPath + "\\" + Property.FName;

                                            }

                                            if (StrQCDestinationServerPath.Contains(".adv"))
                                            {
                                                Property.FName = StrPacketNo + ".adv";
                                                Property.Ext = ".adv";
                                                Property.SPath = StrMainServerPath + "\\" + Property.FName;

                                            }
                                            //Global.Message("Step:1");
                                            Property.RETURNTYPE = "COMPLETE";
                                            //Global.Message("Step:2");
                                            Property = ObjKapan.QCImportDataSave(Property, "", "RETURN");
                                            //Global.Message("Step:3");
                                            if (Property.ReturnMessageType == "FAIL")
                                            {
                                               // Global.Message("Step:4");
                                                this.Cursor = Cursors.Default;
                                                Global.MessageError(Property.ReturnMessageDesc);
                                                //txtJangedNo.Text = "0";
                                                //     this.Cursor = Cursors.WaitCursor;

                                                if (Property.ReturnValue != "0")
                                                    break;
                                            }
                                            Property = null;
                                        }
                                        catch (Exception ex)
                                        {
                                            //StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Error In Upload Same Server QC File. (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                                            //SetControlPropertyValue(lblMessage2, "Text", StrMessage);
                                            Global.MessageError("Error In Upload Same Server QC File For Packet : " + Val.ToString(DRow["PACKETNO"]) + "\n" + ex.Message.ToString());
                                            return;
                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Error In Download. (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                                        // Save History
                                        new BOTRN_SingleIssueReturn().FileUploadDownloadHistorySave(Val.ToString(DRow["TRN_ID"]),
                                            Val.ToString(DRow["PACKET_ID"]),
                                            StrPacketNo,
                                            "UPLOAD",
                                            "Only File Download",
                                            Val.ToInt(DRow["TOPROCESS_ID"]),
                                            "Exception",
                                            ex.Message,
                                            string.Format(FileExtetion[i], StrPacketNo),
                                            StrUploadFilePath,
                                            UploadLocalPath,
                                            0);
                                    }


                                }
                            }
                        }


                        /*
                        if (IntFileNeedToBeUpload == IntUploadedCount)
                        {
                            DRow["COMPLETE"] = true;
                        }
                        else
                        {
                            DRow["COMPLETE"] = false;
                            Global.Message("Stone No : " + Val.ToString(DRow["PACKETNO"]) + "\n\nTotal Required File : " + IntFileNeedToBeUpload + "\n\nTotal Uploaded File : " + IntUploadedCount + "\n\nPlease Check. Something goes wrong");
                            return;
                        }

                        DataTable DtabCheckPacketlist = DTabPacketLiveStock.Copy();
                        DtabCheckPacketlist.TableName = "CheckPacketList";
                        if (Val.ToBoolean(DRow["COMPLETE"]) == true)
                        {
                            EmployeeActionRightsProperty PropertyRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
                            string StrMainServerPath = Val.ToString(PropertyRights.QCMAINSERVERPATH);

                            TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                            Property.TRN_ID = Val.ToInt64(DRow["TRN_ID"]);
                            Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                            Property.FName = StrPacketNo + ".cap";
                            Property.Ext = ".cap";
                            Property.SPath = StrMainServerPath + "\\" + Property.FName;

                            if (File.Exists(Property.SPath) == false)
                            {
                                Property.FName = StrPacketNo + ".adv";
                                Property.Ext = ".adv";
                                Property.SPath = StrMainServerPath + "\\" + Property.FName;
                            }

                            Property.RETURNTYPE = "COMPLETE";
                            Property = ObjKapan.QCImportDataSave(Property, "", "RETURN");

                            if (Property.ReturnMessageType == "FAIL")
                            {
                                this.Cursor = Cursors.Default;
                                Global.MessageError(Property.ReturnMessageDesc);
                                //txtJangedNo.Text = "0";
                                //     this.Cursor = Cursors.WaitCursor;

                                if (Property.ReturnValue != "0")
                                    break;
                            }
                            Property = null;
                        }*/
                    }
                }
            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        }


        private void REJECT()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;


                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;
                Int64 StrJangedNo = 0;

                foreach (DataRow DRow in DTabPacketLiveStock.Rows)
                {
                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                    Property.TRN_ID = 0;
                    Property.OLDTRN_ID = Val.ToInt64(DRow["TRN_ID"].ToString());

                    //Property.CLIENTREFNO = Val.ToString(DRow["CLIENTREFNO"].ToString());
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"]);
                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"]);
                    Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);
                    Property.TAG = Val.ToString(DRow["TAG"]);

                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());
                    Property.JANGEDNO = Val.ToInt64(StrJangedNo);
                    Property.ENTRYSRNO = IntSrNo;
                    Property.ENTRYTYPE = "EMPRET";

                    Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);
                    Property.TODEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);

                    Property.FROMMANAGER_ID = Val.ToInt64(DRow["TOMANAGER_ID"]);
                    Property.TOMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);

                    Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);
                    Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);

                    Property.FROMPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                    Property.TOPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                    Property.NEXTPROCESS_ID = Val.ToInt32(DRow["NEXTPROCESS_ID"]);
                    //Property.NEXTPROCESS = Val.ToString(DRow["NEXTPROCESS"]);

                    Property.READYPCS = 1;
                    Property.READYCARAT = Val.Val(DRow["CARAT"]);
                    // Add By VV 05112020
                    Property.MACHINE_ID = Val.ToInt(DRow["MACHINE_ID"]);

                    Property.RETURNTYPE = "COMPLETE";

                    Property.DOWNLOAD = Val.ToBoolean(DRow["DOWNLOAD"]);
                    Property.COMPLETE = true;
                    Property.CANCEL = false;
                    Property.REJECT = false;

                    Property.TRANSDATE = Val.SqlDate(DateTime.Now.ToShortDateString());
                    Property.REMARK = Val.ToString(DRow["REMARK"]);

                    Property = ObjTrn.TransferGoods(Property);
                    //StrJangedNo = Property.JANGEDNO.ToString();
                    //txtJangedNo.Text = Property.JANGEDNO.ToString();
                    //ValJangedNoRet = Property.JANGEDNORet;
                    //ValJangedNoTran = Property.JANGEDNOTran;
                    StrJangedNo = Val.ToInt64(Property.JANGEDNO);

                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(Property.ReturnMessageDesc);
                        //txtJangedNo.Text = "0";
                        //     this.Cursor = Cursors.WaitCursor;

                        if (Property.ReturnValue == "-5")
                            break;
                    }
                    //End : #P : 20-09-2022

                    Property = null;
                }

            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        }

        private void CANCEL()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                Int64 StrJangedNo = 0;
                int IntSrNo = 0;

                for (int i = 0; i < GrdDet.RowCount; i++)
                {
                    if (Val.ToBoolean(GrdDet.GetRowCellValue(i, "COLSELECTCHECKBOX")) == true)
                    {
                        DataRow DRow = GrdDet.GetDataRow(i);
                        DRow["CANCEL"] = true;
                        IntSrNo++;
                        string StrPacketNo = Val.ToString(DRow["PACKETNO"]);

                        TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();

                        Property.TRN_ID = 0;
                        Property.OLDTRN_ID = Val.ToInt64(DRow["TRN_ID"].ToString());

                        //Property.CLIENTREFNO = Val.ToString(DRow["CLIENTREFNO"].ToString());
                        Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"]);
                        Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"]);
                        Property.PACKETNO = Val.ToInt32(DRow["PACKETNO"]);
                        Property.TAG = Val.ToString(DRow["TAG"]);

                        Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());
                        Property.JANGEDNO = Val.ToInt64(StrJangedNo);
                        Property.ENTRYSRNO = IntSrNo;
                        Property.ENTRYTYPE = "EMPRET";

                        Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);
                        Property.TODEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);

                        Property.FROMMANAGER_ID = Val.ToInt64(DRow["TOMANAGER_ID"]);
                        Property.TOMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);

                        Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);
                        Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);

                        Property.FROMPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                        Property.TOPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                        Property.NEXTPROCESS_ID = Val.ToInt32(DRow["NEXTPROCESS_ID"]);
                        //Property.NEXTPROCESS = Val.ToString(DRow["NEXTPROCESS"]);

                        //Property.CARAT = Val.Val(DRow["CARAT"]);
                        Property.READYPCS = 1;
                        Property.READYCARAT = Val.Val(DRow["CARAT"]);

                        Property.MACHINE_ID = Val.ToInt(DRow["MACHINE_ID"]);

                        Property.RETURNTYPE = "CANCEL";

                        Property.DOWNLOAD = Val.ToBoolean(DRow["DOWNLOAD"]);
                        Property.COMPLETE = false;
                        Property.CANCEL = true;
                        Property.REJECT = false;

                        Property.TRANSDATE = Val.SqlDate(DateTime.Now.ToShortDateString());
                        Property.REMARK = Val.ToString(DRow["REMARK"]);

                        Property = ObjTrn.TransferGoods(Property);
                        StrJangedNo = Val.ToInt64(Property.JANGEDNO);

                        string StrMessage = "Packet : " + Val.ToString(DRow["PACKETNO"]) + " Has Cancelled . (" + IntI.ToString() + " / " + IntTotal.ToString() + ")";
                        SetControlPropertyValue(lblMessage2, "Text", StrMessage);

                        Property = null;
                    }
                }
            }
            catch (Exception ex)
            {
                SetControlPropertyValue(lblMessage2, "Text", ex.Message);
            }
        }

        private void BtnComplete_Click(object sender, EventArgs e)
        {
            DTabSelected = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DTabSelected == null || DTabSelected.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Transfer");
                return;
            }

            foreach (DataRow DRow in DTabSelected.Rows)
            {
                if (Val.ToString(DRow["DOWNLOADEXT"]).Length != 0 && Val.ToBoolean(DRow["DOWNLOAD"]) == false)
                {
                    Global.Message("You Have To Download File First Then & Then You Can Complete This Stone\n\nPacket No Is : " + Val.ToString(DRow["PACKETNO"]));
                    return;
                }

                if (mFormType == FORMTYPE.QCSTOCK && Val.ToString(DRow["PACKETSTATUS"]) != "RUNNING")
                {
                    Global.Message("Selected Packet's \n\n No : " + Val.ToString(DRow["PACKETNO"]) + " Is Not Running Status So You Can't Complete.");
                    return;
                }

            }
            if (Global.Confirm("Are You Sure To Complete Selected Stones ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            BtnSearch.Enabled = false;
            mStrOperation = "Complete";
            PanelProgress.Visible = true;
            SetControlPropertyValue(lblMessage2, "Text", "Downloding Start");
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            DTabSelected = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DTabSelected == null || DTabSelected.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Transfer");
                return;
            }

            foreach (DataRow DRow in DTabSelected.Rows)
            {
                if (mFormType == FORMTYPE.QCSTOCK && Val.ToString(DRow["PACKETSTATUS"]) != "RUNNING")
                {
                    Global.Message("Selected Packet's \n\n No : " + Val.ToString(DRow["PACKETNO"]) + " Is Not Running Status So You Can't Cancel.");
                    return;
                }
            }

            if (Global.Confirm("Are You Sure To Cancel Selected Stones ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

            BtnSearch.Enabled = false;
            mStrOperation = "Cancel";
            PanelProgress.Visible = true;
            SetControlPropertyValue(lblMessage2, "Text", "Downloding Start");
            if (!backgroundWorker2.IsBusy)
            {
                backgroundWorker2.RunWorkerAsync();
            }
        }

        private void BtnReject_Click(object sender, EventArgs e)
        {
            DTabSelected = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

            if (DTabSelected == null || DTabSelected.Rows.Count == 0)
            {
                Global.Message("Please Select Atleast One Stone For Transfer");
                return;
            }                        

            foreach (DataRow DRow in DTabSelected.Rows)
            {
                if (Val.ToString(DRow["DOWNLOADEXT"]).Length != 0 && Val.ToBoolean(DRow["DOWNLOAD"]) == false)
                {
                    Global.Message("You Have To Download File First Then & Then You Can Reject This Stone\n\nPacket No Is : " + Val.ToString(DRow["PACKETNO"]));
                    return;
                }

                if (mFormType == FORMTYPE.QCSTOCK && Val.ToString(DRow["PACKETSTATUS"]) != "RUNNING")
                {
                    Global.Message("Selected Packet's \n\n No : " + Val.ToString(DRow["PACKETNO"]) + " Is Not Running Status So You Can't Complete.");
                    return;
                }

            }

            if (Global.Confirm("Are You Sure To Reject Selected Stones ?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            GrpReject.Visible = true;
            //panel1.Enabled = false;
            PanelHeader.Enabled = false;
            panel2.Enabled = false;
            MainGrid.Enabled = false;
        }

        private void TxtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE,EMPLOYEENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        TxtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnRejectReason_Click(object sender, EventArgs e)
        {
            if (mFormType == FORMTYPE.RETURNSTOCK)
            {
                mReason_Id = Val.ToInt32(txtReason.Tag);
                mRemark = txtRemark.Text;

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;

                BtnSearch.Enabled = false;
                mStrOperation = "Reject";

                PanelProgress.Visible = true;
                SetControlPropertyValue(lblMessage2, "Text", "Downloding Start");
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }
            }
            else
            {
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnSearch.Enabled = false;
                mStrOperation = "Reject";
                PanelProgress.Visible = true;
                SetControlPropertyValue(lblMessage2, "Text", "Downloding Start");
                if (!backgroundWorker2.IsBusy)
                {
                    backgroundWorker2.RunWorkerAsync();
                }
               
                GrpReject.Visible = false;
                //panel1.Enabled = true;
                PanelHeader.Enabled = true;
                panel2.Enabled = true;
                MainGrid.Enabled = true;
            }
               
        }

        private void TxtDPTJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {

            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();

                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_DEPTJANGEDNO);
                    FrmSearch.mStrSearchText = e.KeyChar.ToString();
                    FrmSearch.mStrSearchField = "DEPTJANGEDNO,TRANSDATE";
                    //FrmSearch.mStrColumnsToHide = "EMPLOYEE_ID";
                    FrmSearch.ValueMemeter = "DEPTJANGEDNO";
                    FrmSearch.DisplayMemeter = "TRANSDATE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;

                    if (FrmSearch.mDTab != null)
                    {
                        DataTable DTabDeptJanged = FrmSearch.DTabResult;
                        string DeptJangedXML = string.Empty;
                        DTabDeptJanged.TableName = "Table1";
                        DeptJangedXML = string.Empty;
                        using (StringWriter sw = new StringWriter())
                        {
                            DTabDeptJanged.WriteXml(sw);
                            DeptJangedXML = sw.ToString();
                        }

                        TxtDPTJangedNo.Text = Val.ToString(FrmSearch.SelectedValuemember);
                        TxtDPTJangedNo.Tag = DeptJangedXML;
                        TxtJangedDate.Text = Val.ToString(FrmSearch.SelectedDisplaymember);

                        BtnSearch_Click(null, null);
                       
                    }
                  
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }

            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "DEPTJANGEDNO,TRANSDATE";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPTJANGEDNO);

            //        //FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            TxtDPTJangedNo.Text = Val.ToString(FrmSearch.mDRow["DEPTJANGEDNO"]);
            //            TxtJangedDate.Text = Val.ToString(FrmSearch.mDRow["TRANSDATE"]);
            //            //TxtJangedDate.Text =  Val.SqlDate(FrmSearch.mDRow["TRANSDATE"]);
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }

            //}
            //catch (Exception EX)
            //{
            //    Global.Message(EX.Message);
            //}
        }

        private void TxtDPTJangedNo_Validated(object sender, EventArgs e)
        {
            try
            {
                if (TxtDPTJangedNo.Text.Trim().Length == 0)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrType = string.Empty;
                if (RbtDeptStock.Checked == true)
                {
                    StrType = RbtDeptStock.Tag.ToString();
                }
                else if (RbtFullStock.Checked == true)
                {
                    StrType = RbtFullStock.Tag.ToString();
                }
                else if (RbtMYStock.Checked == true)
                {
                    StrType = RbtMYStock.Tag.ToString();
                }
                else if (RbtOtherStock.Checked == true)
                {
                    StrType = RbtOtherStock.Tag.ToString();
                }

                bool ISFind = false;
                for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                {
                    DataRow DRow = GrdDet.GetDataRow(IntI);
                    if (TxtDPTJangedNo.Text.Trim() == Val.ToString(DRow["DPTJANGEDNO"]).Trim())
                    {
                        ISFind = true;
                        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;

                        txtKapanName.Focus();
                        CalculateSummary();
                        GrdDet.FocusedRowHandle = 0;
                        //break;
                    }
                }

                if (ISFind == false)
                {
                    DataTable DTabJangedData = ObjKapan.GetDataForKapanLiveStockForReturn(mFormType.ToString(), StrType, "", 0, "", "", "", 0, 0, Val.ToInt(TxtDPTJangedNo.Text), "", Val.ToString(mFormType),"","");
                    if (DTabJangedData == null || DTabJangedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;

                        Global.MessageError("DPTJangedNo : " + Val.ToString(txtJangedNo.Text) + " Is Not In Your Stock Please Check..");
                        //TxtDPTJangedNo.Text = string.Empty;
                        TxtDPTJangedNo.Focus();
                        return;
                    }
                    else
                    {
                        var matched = from table1 in DTabPacketLiveStock.AsEnumerable()
                                      join table2 in DTabJangedData.AsEnumerable() on table1.Field<Int64>("DPTJANGEDNO") equals table2.Field<Int64>("DPTJANGEDNO")
                                      where table1.Field<Int64>("PACKET_ID") == table2.Field<Int64>("PACKET_ID")
                                      select table1;

                        if (matched.Any())
                        {
                            this.Cursor = Cursors.Default;
                            string Str = matched.FirstOrDefault()["DPTJANGEDNO"].ToString();
                            Global.Message("This JangedNo Is Already Selected." + Str);
                            //TxtDPTJangedNo.Text = string.Empty;
                            TxtDPTJangedNo.Focus();
                            return;
                        }



                        foreach (DataRow DR in DTabJangedData.Rows)
                        {
                            DataRow DRNew = DTabPacketLiveStock.NewRow();
                            foreach (DataColumn DCol in DTabPacketLiveStock.Columns)
                            {
                                DRNew[DCol.ColumnName] = DR[DCol.ColumnName];
                            }
                            DTabPacketLiveStock.Rows.Add(DRNew);
                        }


                        for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                        {
                            DataRow DRowGrid = GrdDet.GetDataRow(IntI);

                            if (TxtDPTJangedNo.Text.Trim() == Val.ToString(DRowGrid["DPTJANGEDNO"]).Trim())
                            {
                                ISFind = true;
                                GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                                DTabPacketLiveStock.AcceptChanges();
                                //break;
                            }
                        }
                        GrdDet.FocusedRowHandle = 0;
                    }
                    DTabJangedData = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.FocusedRowHandle = 0;
                GrdDet.RefreshData();
                MainGrid.Refresh();

                CalculateSummary();

                //TxtDPTJangedNo.Text = string.Empty;
                TxtDPTJangedNo.Focus();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                txtBarcode.Focus();
            }
        }

        private void BtnQCImportData_Click(object sender, EventArgs e)
        {
            try
            {
                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnSearch.Enabled = false;
                progressPanelQCImport.Visible = true;
                if (!BkgQCImport.IsBusy)
                {
                    BkgQCImport.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }

        public string GetHierarchyOfFolder(string StrRootPath, string StrFolderName, string StrExtension)
        {
            string StrDirectoryPath = "";
            try
            {
                Stack<string> dirs = new Stack<string>(20);

                

                string root = StrRootPath;

                if (!System.IO.Directory.Exists(root))
                {                    
                    throw new ArgumentException();
                }
                dirs.Push(root);

                while (dirs.Count > 0)
                {
                    string currentDir = dirs.Pop();

                    DirectoryInfo di = new DirectoryInfo(@currentDir);

                    DirectoryInfo[] subDirs1 = di.GetDirectories();



                    //string[] subDirs;
                    //try
                    //{
                    //    subDirs1 = di.GetDirectories(currentDir);
                    //}
                    //catch (UnauthorizedAccessException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //    continue;
                    //}
                    //catch (System.IO.DirectoryNotFoundException e)
                    //{
                    //    Console.WriteLine(e.Message);
                    //    continue;
                    //}


                    string[] files = null;
                    try
                    {
                        //  files = System.IO.Directory.GetFiles(currentDir);
                    }

                    catch (UnauthorizedAccessException e)
                    {

                        Console.WriteLine(e.Message);
                        continue;
                    }

                    catch (System.IO.DirectoryNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }

                    /*
                    foreach (string file in files)
                    {
                        try
                        {
                            System.IO.FileInfo fi = new System.IO.FileInfo(file);
                            Console.WriteLine("{0}: {1}, {2}", fi.Name, fi.Length, fi.CreationTime);
                        }
                        catch (System.IO.FileNotFoundException e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                    }
                    */

                    /*
                    foreach (string str in subDirs)
                    {
                        dirs.Push(str);
                    }
                    */

                    //Path ni under je sub Folder's hoy eni under File Check karse and Path aapse
                    foreach (DirectoryInfo Str in subDirs1)
                    {
                        //if (Val.ToString(Str.Name).ToUpper() == Val.ToString(StrFolderName).ToUpper())
                        if (Val.ToString(Str.Name).ToUpper() != "QCPENDING" && Val.ToString(Str.Name).ToUpper() != "QCCOMPLETE") //coz Server na Main path ma j QCPending And QCComplete nu folder hase..
                        {
                            FileInfo[] Files = Str.GetFiles("*" + StrExtension.ToLower()); //Getting Text files
                            string str = "";
                            if (Files.Length > 0)
                            {
                                foreach (FileInfo file in Files)
                                {
                                    dirs.Push(file.Name);
                                    StrDirectoryPath = file.FullName;
                                    break;
                                }
                            }

                            //if (StrExtension == Path.GetExtension(Str.Name))
                            //{
                            //    dirs.Push(Str.FullName);
                            //    StrDirectoryPath = Str.FullName;
                            //    break;
                            //}
                            //else
                            //{
                            //    dirs.Push(Str.FullName);
                            //}
                        }
                    }

                    if (StrDirectoryPath == "")
                    {
                        //Path ni under Files Check karse and path aapse...SubFolder mathi nahi
                        FileInfo[] Files = di.GetFiles("*" + StrExtension.ToLower()); //Getting Text files
                        if (Files.Length > 0)
                        {
                            foreach (FileInfo file in Files)
                            {
                                dirs.Push(file.Name);
                                StrDirectoryPath = file.FullName;
                                break;
                            }
                        }
                    }

                    if (StrDirectoryPath != "")
                        break;

                }
                
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }

            return StrDirectoryPath;
        }


        public FileInfo[] GetHierarchyOfFiles(string StrRootPath, string StrFolderName, string StrExtension) //Get Files Listing
        {
            Stack<string> dirs = new Stack<string>(20);

            string root = StrRootPath;

            if (!System.IO.Directory.Exists(root))
            {
                throw new ArgumentException();
            }
            dirs.Push(root);

            FileInfo[] Files = null;

            while (dirs.Count > 0)
            {
                string currentDir = dirs.Pop();
                DirectoryInfo di = new DirectoryInfo(@currentDir);
                DirectoryInfo[] subDirs1 = di.GetDirectories();
                Files = di.GetFiles("*" + StrExtension.ToLower()); //Getting Text files
            }
            return Files;
        }

        public void ImportStoneForJob()
        {
            try
            {
                int IntI = 0;
                int IntTotal = DTabSelected.Rows.Count;
                int IntSrNo = 0;

                string StrUploadServer = "";
                string StrUploadServerUserName = "";
                string StrUploadServerPassword = "";

                DataTable DTabPath = new DataTable();
                int Employee_ID = 0;
                TxtEmployee.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                Employee_ID = Val.ToInt32(TxtEmployee.Tag);
                DTabPath = ObjKapan.GetDataForPath(Employee_ID);
                if (DTabPath.Columns.Count == 0)
                {
                    DataRow DRow = ObjKapan.GetMainServerPath(); //Defult GalaxyOperator Process na Credential Consider karavya 6e..
                    StrUploadServer = Val.ToString(DRow["UPLOADSERVERPATH"]);
                    StrUploadServerUserName = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                    StrUploadServerPassword = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);
                    //Global.Message(StrUploadServer + "test9");
                    //Global.Message(StrUploadServerUserName + "test10");
                    //Global.Message(StrUploadServerPassword + "test11");
                }
                else
                {
                    StrUploadServer = Val.ToString(DTabPath.Rows[0]["QCUSERWISESERVERPATH"]);
                    StrUploadServerUserName = Val.ToString(DTabPath.Rows[0]["QCUSERWISEUSERNAME"]);
                    StrUploadServerPassword = Val.ToString(DTabPath.Rows[0]["QCUSERWISEPASSWARD"]);
                    //Global.Message(StrUploadServer + "test12");
                    //Global.Message(StrUploadServerUserName + "test13");
                    //Global.Message(StrUploadServerPassword + "test14");
                }

                // VALIDATTION FOR CHECK EXISTING RUNNING OR NOT

                TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                Property = ObjKapan.QCImportDataValSave(Property);
                if (Property.ReturnMessageType != "SUCCESS")
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    return;
                }
                string StrUploadServerQCPendingPath = string.Empty;

                DTabQC.Rows.Clear();
                using (new SharingLockUnlock(StrUploadServer, StrUploadServerUserName, StrUploadServerPassword))
                {
                    //Global.Message(StrUploadServer + "test15");
                    //Global.Message(StrUploadServerUserName + "test16");
                    //Global.Message(StrUploadServerPassword + "test17");
                    for (int I = 0; I < 1; I++)
                    {

                        string StrPath = GetHierarchyOfFolder(StrUploadServer, "", ".cap");
                        if (StrPath == "")
                        {
                            StrPath = GetHierarchyOfFolder(StrUploadServer, "", ".adv");
                        }
                        //NetworkShare.DisconnectFromShare(StrDirectoryPath, false); //Disconnect from the server.

                        if (!Val.ToString(StrPath).Trim().Equals(string.Empty))
                        {
                            var lastFolder = Path.GetDirectoryName(StrPath);
                            var pathWithoutLastFolder = Path.GetDirectoryName(lastFolder);

                            string StrUploadServerQCPending = pathWithoutLastFolder + "\\QCPending\\" + BOConfiguration.gEmployeeProperty.LEDGERCODE;
                            StrUploadServerQCPendingPath = StrUploadServerQCPending + "\\" + Path.GetFileName(StrPath);

                            DataRow DRowQC = DTabQC.NewRow();
                            DRowQC["SEARCHSTATUS"] = "SUCCESS";
                            DRowQC["SEARCHURL"] = Val.ToString(StrPath);
                            DRowQC["STONENO"] = Val.ToString(Path.GetFileNameWithoutExtension(StrPath)).Replace("(GAL)", "");
                            DRowQC["STONESTATUS"] = I == 0 ? "RUNNING" : "NEXTPROCESS";
                            DRowQC["SOURCEURL"] = StrPath;
                            DRowQC["DESTINATIONSERVERPATH"] = StrUploadServerQCPending;
                            DRowQC["DESTINATIONURL"] = StrUploadServerQCPendingPath;
                            DTabQC.Rows.Add(DRowQC);

                            if (Directory.Exists(StrUploadServerQCPending) == false)
                            {
                                Directory.CreateDirectory(StrUploadServerQCPending);
                            }
                            //Global.Message("StrUploadServerQCPending : " + StrUploadServerQCPending);
                            //File.Copy(StrPath, StrUploadServerQCPendingPath, true);
                            File.Move(StrPath, StrUploadServerQCPendingPath);
                        }
                        //else
                        //{
                        //    Global.Message("No Data Found For Import..For Round : " + I.ToString());
                        //}
                    }

                    if (File.Exists(StrUploadServerQCPendingPath))
                    {
                        if (DTabQC.Rows.Count <= 0)
                        {
                            Global.Message("No Data Found For Import..");
                            return;
                        }

                        string StrQCDataXml = "";
                        DTabQC.TableName = "Table";
                        using (StringWriter sw = new StringWriter())
                        {
                            DTabQC.WriteXml(sw);
                            StrQCDataXml = sw.ToString();
                        }

                        Property = new TrnSingleIssueReturnProperty();
                        Property = ObjKapan.QCImportDataSave(Property, StrQCDataXml, "ISSUE");

                        if (Property.ReturnMessageType != "SUCCESS")
                        {
                            Global.MessageError(Property.ReturnMessageDesc);
                            foreach (DataRow Dr in DTabQC.Rows)
                            {
                                string StrSourcePath = Val.ToString(Dr["SOURCEURL"]);
                                string StrDestinationServerPath = Val.ToString(Dr["DESTINATIONSERVERPATH"]);
                                string StrDestinationPath = Val.ToString(Dr["DESTINATIONURL"]);
                                try
                                {
                                    if (Directory.Exists(StrDestinationServerPath) == false)
                                    {
                                        Directory.CreateDirectory(StrDestinationServerPath);
                                    }
                                    File.Move(StrDestinationPath, StrSourcePath);
                                }
                                catch (Exception ex)
                                {
                                    Property = ObjKapan.QCImportDataDelete(Property, ex.Message);
                                }
                            }
                        }
                        else
                        {
                            //Global.Message(Property.ReturnMessageDesc);
                        }
                    }

                }
                DTabQC.Rows.Clear();
            }
            catch (Exception ex)
            {
                //SetControlPropertyValue(lblMessage2, "Text", ex.Message);
                Global.MessageError(ex.Message.ToString());
            }
        }

        private void BkgQCImport_DoWork(object sender, DoWorkEventArgs e)
        {
            ImportStoneForJob();
        }

        private void BkgQCImport_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                progressPanelQCImport.Visible = false;
                BtnSearch.Enabled = true;
                BtnSearch_Click(null, null);
                
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }

        }

        private void txtReason_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(mFormType == FORMTYPE.QCSTOCK)
            {
                try
                {
                     if (Global.OnKeyPressToOpenPopup(e))
                     {
                        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                        FrmSearch.mSearchField = "REJECTCODE,REJECTNAME";
                        FrmSearch.mSearchText = e.KeyChar.ToString();
                        this.Cursor = Cursors.WaitCursor;
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_REJECTREASON);

                        FrmSearch.mColumnsToHide = "REJECT_ID";
                        this.Cursor = Cursors.Default;
                        FrmSearch.ShowDialog();
                        e.Handled = true;
                        if (FrmSearch.mDRow != null)
                        {
                            txtReason.Text = Val.ToString(FrmSearch.mDRow["REJECTNAME"]);
                            txtReason.Tag = Val.ToString(FrmSearch.mDRow["REJECT_ID"]);
                        }
                        FrmSearch.Hide();
                        FrmSearch.Dispose();
                        FrmSearch = null;
                     }
                }
                catch (Exception ex)
                {
                    Global.Message(ex.Message);
                }            
            }
        }

        private void btnShowComplete_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                DataTable DTab = ObjTrn.GetCompleteGetData(Val.ToInt64(TxtEmployee.Tag));

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DTab;
                GrdDet.RefreshData();
                GrdDet.EndUpdate();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

    }
}
