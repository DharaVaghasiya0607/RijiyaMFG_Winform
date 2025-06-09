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

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePacketLiveStock : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();
        string DeptJangedXML = string.Empty;

        FORMTYPE mFormType = FORMTYPE.ROUGH;

        string mStrStockType = "";
        bool mISWithExtraStock = false;
        string mStrJangedNo = "";
        string mStrJangedDate = "";
        string mStrDptJangedNo = "";

        string mStrKapanName = "";
        int mStrToPktNo = 0;

        string mStrBarcode = "";

        public enum FORMTYPE
        {
            ROUGH = 0,
            MAK = 1,
            POL = 2,
            ALL = 3,
            BOMBAY = 4
        }

        #region Property Settings

        public FrmSinglePacketLiveStock()
        {
            InitializeComponent();
        }

        public void ShowForm(FORMTYPE pFormType)
        {

            mFormType = pFormType;
            if (mFormType == FORMTYPE.ROUGH)
            {
                PanelHeader.BackColor = Color.White;
                this.Text = "ROUGH PACKET LIVE STOCK";
                this.Name = "FrmSinglePacketRoughLiveStock";
                lblRoughMak.Text = "ROU.";
            }
            else if (mFormType == FORMTYPE.MAK)
            {
                PanelHeader.BackColor = Color.FromArgb(192, 192, 255);
                this.Text = "MAKABLE PACKET LIVE STOCK";
                this.Name = "FrmSinglePacketMakableLiveStock";
                lblRoughMak.Text = "MAK.";
            }
            else if (mFormType == FORMTYPE.POL)
            {
                PanelHeader.BackColor = Color.FromArgb(255, 192, 192);
                this.Text = "POLISHED PACKET LIVE STOCK";
                this.Name = "FrmSinglePacketPolishLiveStock";
                lblRoughMak.Text = "POL.";
            }
            else if (mFormType == FORMTYPE.ALL)
            {
                PanelHeader.BackColor = Color.PowderBlue;
                this.Text = "ALL LIVE STOCK";
                this.Name = "FrmSinglePacketAllLiveStock";
                lblRoughMak.Text = "ALL.";
            }
            else if (mFormType == FORMTYPE.BOMBAY)
            {
                PanelHeader.BackColor = Color.FromArgb(192, 192, 255);
                this.Text = "GRADING/BOMBAY LIVE STOCK";
                this.Name = "FrmSinglePacketBombayLiveStock";
                lblRoughMak.Text = "BOM.";
                //BtnTransferToMix.Visible = true;
            }
            mFormType = pFormType;
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            // Action Button Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            RbtFullStock.Checked = false;
            RbtDeptStock.Checked = false;
            RbtMYStock.Checked = false;
            RbtOtherStock.Checked = false;

            RbtFullStock.Enabled = Property.ISFULLSTOCK;
            ChkGrpJangedNo.Visible = Property.ISGROUPJANGADNO;

            RbtMYStock.Enabled = Property.ISMYSTOCK;
            RbtMYStock.Checked = Property.ISMYSTOCK;

            RbtDeptStock.Enabled = Property.ISDEPTSTOCK;
            RbtDeptStock.Checked = Property.ISDEPTSTOCK;

            RbtOtherStock.Enabled = Property.ISOTHERSTOCK;

            BtnTransfer.Visible = Property.DEPTTRANSFER;
            BtnStaffIssue.Visible = Property.EMPISSUE;
            BtnStaffReturn.Visible = Property.EMPRETURN;
            BtnReturnWithSplit.Visible = Property.RETURNWITHSPLIT;
            BtnRejection.Visible = Property.REJECTIONTRANSFER;

            Property = null;

            this.Show();

            GrdDet.BeginUpdate();
            DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketLiveStock("NONE", "NONE", "NONE", 0, "", "", "", Val.ToBoolean(ChkWithExtraStock.Checked), "", ChkGrpJangedNo.Checked, 0, "", "");
            MainGrid.DataSource = DTabPacketLiveStock;
            MainGrid.Refresh();

            if (MainGrid.RepositoryItems.Count == 1)
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
            RbtBarcode.Checked = true;
            if (RbtBarcode.Checked == true)
            {
                PanelBarcode.Visible = true;
                PanelJangedNo.Visible = false;
                PanelPacketNo.Visible = false;
                PanelPktSerialNo.Visible = false;
                PanelFromToPkt.Visible = false;
                PnlDPTJangedno.Visible = false;
            }

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

                BtnClear_Click(null, null);

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

                mISWithExtraStock = Val.ToBoolean(ChkWithExtraStock.Checked);
                mStrJangedNo = Val.ToString(txtJangedNo.Text);

                if (TxtDPTJangedNo.Text == string.Empty)
                {
                    TxtDPTJangedNo.Tag = string.Empty;
                }


                if (RbtnRange.Checked == true)
                {
                    mStrKapanName = Val.ToString(TxttoKapan.Text);
                }
                else
                {
                    mStrKapanName = Val.ToString(txtKapanName.Text);
                }
                mStrToPktNo = Val.ToInt32(TxtToPkt.Text);
                mStrBarcode = Val.ToString(txtBarcode.Text);

                DTabPacketLiveStock.Rows.Clear();

                if (ObjGridSelection != null)
                {
                    ObjGridSelection.ClearSelection();
                }

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = true;
                BtnSearch.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

            }
            catch (Exception ex)
            {
                BtnSearch.Enabled = true;
                PanelProgress.Visible = false;
                // Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketLiveStock(mFormType.ToString(), mStrStockType, mStrKapanName, Val.ToInt32(TxtfromPkt.Text), Val.ToString(TxtToTag.Text), mStrBarcode, "", Val.ToBoolean(mISWithExtraStock), mStrJangedNo, ChkGrpJangedNo.Checked, mStrToPktNo,Val.ToString(TxtDPTJangedNo.Text), Val.ToString(TxtDPTJangedNo.Tag));
            }
            catch (Exception ex)
            {
                BtnSearch.Enabled = true;
                PanelProgress.Visible = false;
                Global.Message(ex.Message.ToString());
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
            else if (e.KeyCode == Keys.F7)
            {
                BtnTransfer.PerformClick();
            }
        }

        public void CalculateSummary()
        {
            int IntPcs = 0;
            int RepPcs = 0;
            double RepCarat = 0;
            double DouCarat = 0;
            int IntSelPcs = 0;
            double DouSelCarat = 0;



            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                if (DRow["TOPROCESSNAME"].ToString().Contains("REP"))
                {
                    RepPcs += 1;
                    RepCarat += Val.Val(DRow["BALANCECARAT"]);
                }

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
            TxtRepCarat.Text = RepCarat.ToString();
            TxtRepPcs.Text = RepPcs.ToString();

            //txtCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtTrfCarat.Text = Math.Round(DouCarat, 3).ToString();
            //txtAmount.Text = Math.Round(DouAmount, 3).ToString();
            //double DouRate = DouCarat != 0 ? Math.Round(DouAmount / DouCarat, 3) : 0;
            //txtRate.Text = Math.Round(DouRate, 3).ToString();
            //txtTransferRate.Text = Math.Round(DouRate, 3).ToString();
        }

        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }


        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDet);
        }

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnSearch_Click(null, null);
        }

        private void MainGrid_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSummary();
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
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
                    // Comment By Vipul 20-03-2023 As Discuss With Brijeshbhai
                    //if (Val.ToString(DRow["QCSTATUS"]) == "QC_ISSUE")
                    //{
                    //  Global.Message("This Packet Is In QC Process...PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - ") + " So You Can't Transfer.");
                    // return;
                    // }
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

                string StrType = string.Empty;
                if (mFormType == FORMTYPE.BOMBAY)
                {
                    StrType = "BOMBAY";
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "ROU")
                {
                    StrType = FORMTYPE.ROUGH.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "POL")
                {
                    StrType = FORMTYPE.POL.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "MAK")
                {
                    StrType = FORMTYPE.MAK.ToString();
                }

                DTabDistinct.Dispose();
                DTabDistinct = null;

                FrmSingleGoodsTransfer FrmSingleGoodsTransfer = new FrmSingleGoodsTransfer();
                FrmSingleGoodsTransfer.MdiParent = Global.gMainRef;
                FrmSingleGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmSingleGoodsTransfer.ShowForm(DTab, Transaction.FrmSingleGoodsTransfer.FORMTYPE.TRANSFER, StrType);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

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
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo(mFormType.ToString(), StrType, txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, "", 0, "", Val.ToInt(txtPacketNo.Text), 0);
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
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo(mFormType.ToString(), StrType, "", 0, "", txtBarcode.Text, 0, "", 0, 0);
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


        private void BtnReturnWithSplit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

                if (DTab == null)
                {
                    Global.Message("Please Select One Packet For Spliting Operation");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    if (Val.ISDate(DRow["CONFDATE"]) == false)
                    {
                        Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                    // Comment By Vipul 20-03-2023 As Discuss With Brijeshbhai
                    //if (Val.ToString(DRow["QCSTATUS"]) == "QC_ISSUE")
                    //{
                    //  Global.Message("This Packet Is In QC Process...PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - ") + " So You Can't Transfer.");
                    // return;
                    // }
                }

                if (DTab.Rows.Count != 1)
                {
                    Global.Message("Please Select Only One Packet For Spliting Operation");
                    return;
                }

                FrmSingleGoodsReturnWithSplit FrmSingleGoodsReturnWithSplit = new FrmSingleGoodsReturnWithSplit();
                FrmSingleGoodsReturnWithSplit.MdiParent = Global.gMainRef;
                FrmSingleGoodsReturnWithSplit.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmSingleGoodsReturnWithSplit.ShowForm(Val.ToString(DTab.Rows[0]["KAPANNAME"]), Val.ToString(DTab.Rows[0]["PACKETNO"]), Val.ToString(DTab.Rows[0]["TAG"]));

            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnStaffIssue_Click(object sender, EventArgs e)
        {
            try
            {
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

                    //Comment By Vipul On 20-03-2023 As Discusss With Brijeshbhai
                    //if (Val.ToString(DRow["QCSTATUS"]) == "QC_ISSUE")
                    //{
                    //Global.Message("This Packet Is In QC Process...PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - ") + " So You Can't Issue.");
                    //return;
                    //}
                }

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "STATUS");
                //if (DTabDistinct == null)
                //{
                //    Global.Message("Status Is Blank");
                //    return;
                //}
                //if (DTabDistinct.Rows.Count != 1)
                //{
                //    Global.Message("You Have Selected Multiple Status(Rough,Makable,Polish) Stone \n\nKindly Select Only One Status(Rough,Makable,Polish) Stones For Action ");
                //    return;
                //}

                string StrType = string.Empty;
                if (mFormType == FORMTYPE.BOMBAY)
                {
                    StrType = "BOMBAY";
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "ROU")
                {
                    StrType = FORMTYPE.ROUGH.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "POL")
                {
                    StrType = FORMTYPE.POL.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "MAK")
                {
                    StrType = FORMTYPE.MAK.ToString();
                }

                DTabDistinct.Dispose();
                DTabDistinct = null;

                FrmSingleGoodsTransfer FrmSingleGoodsTransfer = new FrmSingleGoodsTransfer();
                FrmSingleGoodsTransfer.MdiParent = Global.gMainRef;
                FrmSingleGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmSingleGoodsTransfer.ShowForm(DTab, Transaction.FrmSingleGoodsTransfer.FORMTYPE.STAFFISSUE, StrType);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

        }

        private void BtnStaffReturn_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                //foreach (DataRow DRow in DTab.Rows)
                //{
                //    if (Val.ISDate(DRow["CONFDATE"]) == false)
                //    {
                //        Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                //        return;
                //    }
                //}


                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "STATUS");
                //if (DTabDistinct == null)
                //{
                //    Global.Message("Status Is Blank");
                //    return;
                //}
                //if (DTabDistinct.Rows.Count != 1)
                //{
                //    Global.Message("You Have Selected Multiple Status(Rough,Makable,Polish) Stone \n\nKindly Select Only One Status(Rough,Makable,Polish) Stones For Action ");
                //    return;
                //}

                //#P : 15-10-2020 : Multiple Process na Stone Ek sathe Return na thava devu joiye...
                if (DTab.DefaultView.ToTable(true, "TOPROCESS_ID").Rows.Count > 1)
                {
                    Global.Message("Opps... You Are Selecting Multy Process Packets For Return. Please Select Common Process Packets..");
                    return;
                }
                //End : #P : 15-10-2020 : 


                string StrType = string.Empty;
                if (mFormType == FORMTYPE.BOMBAY)
                {
                    StrType = "BOMBAY";
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "ROU")
                {
                    StrType = FORMTYPE.ROUGH.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "POL")
                {
                    StrType = FORMTYPE.POL.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "MAK")
                {
                    StrType = FORMTYPE.MAK.ToString();
                }

                DTabDistinct.Dispose();
                DTabDistinct = null;


                FrmSingleGoodsTransfer FrmSingleGoodsTransfer = new FrmSingleGoodsTransfer();
                FrmSingleGoodsTransfer.MdiParent = Global.gMainRef;
                FrmSingleGoodsTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmSingleGoodsTransfer.ShowForm(DTab, Transaction.FrmSingleGoodsTransfer.FORMTYPE.STAFFRETURN, StrType);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }

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

            if (DR["TOPROCESSNAME"].ToString().Contains("REP"))
            {
                e.Appearance.BackColor = Color.DarkSeaGreen;
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
            BtnTransfer.Enabled = !RbtFullStock.Checked;
            BtnReturnWithSplit.Enabled = !RbtFullStock.Checked;
            BtnStaffIssue.Enabled = !RbtFullStock.Checked;
            BtnStaffReturn.Enabled = !RbtFullStock.Checked;
            BtnRejection.Enabled = !RbtFullStock.Checked;
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

        private void BtnRejection_Click(object sender, EventArgs e)
        {
            try
            {
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
                }

                FrmSingleRejectionTransfer FrmSingleRejectionTransfer = new FrmSingleRejectionTransfer();
                FrmSingleRejectionTransfer.MdiParent = Global.gMainRef;
                FrmSingleRejectionTransfer.FormClosing += new FormClosingEventHandler(FormTransfer_Closing);
                FrmSingleRejectionTransfer.ShowForm(DTab);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
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


        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                BtnSearch.Enabled = true;
                PanelProgress.Visible = false;

                GrdDet.BeginUpdate();
                MainGrid.DataSource = DTabPacketLiveStock;
                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();
                MainGrid.Refresh();
                GrdDet.EndUpdate();
                TxttoKapan.Text = string.Empty;
                TxtToPkt.Text = string.Empty;
                TxtfromPkt.Text = string.Empty;
                TxtToTag.Text = string.Empty;
                TxtDPTJangedNo.Text = string.Empty;
                TxtJangedDate.Text = string.Empty;
                // TxttoKapan.Focus();

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
                PanelProgress.Visible = false;
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

                string StrType = string.Empty;
                if (mFormType == FORMTYPE.BOMBAY)
                {
                    StrType = "BOMBAY";
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "ROU")
                {
                    StrType = FORMTYPE.ROUGH.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "POL")
                {
                    StrType = FORMTYPE.POL.ToString();
                }
                else if (Val.ToString(DTabDistinct.Rows[0]["STATUS"]).ToUpper() == "MAK")
                {
                    StrType = FORMTYPE.MAK.ToString();
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
            else if (RbtnRange.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                TxttoKapan.Focus();
            }
            else if (RbtnDPTJangedNo.Checked)
            {
                txtKapanName.Text = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtTag.Text = string.Empty;
                txtJangedNo.Text = string.Empty;
                txtBarcode.Text = string.Empty;
                txtSrNoKapanName.Text = string.Empty;
                txtSrNoSerialNo.Text = string.Empty;
                TxttoKapan.Text = string.Empty;
                TxtToPkt.Text = string.Empty;
                TxtToTag.Text = string.Empty;
                TxtfromPkt.Text = string.Empty;
                TxtDPTJangedNo.Focus();
            }

            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
            PanelFromToPkt.Visible = RbtnRange.Checked;
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
                    DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo(mFormType.ToString(), StrType, txtSrNoKapanName.Text, 0, "", "", Val.ToInt32(txtSrNoSerialNo.Text), "", 0, 0);
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
                    DataTable DTabJangedData = ObjKapan.GetDataForSinglePacketLiveStockJangednoWiseInfo(mFormType.ToString(), StrType, "", 0, "", "", 0, txtJangedNo.Text, TxtDPTJangedNo.Text);
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

        private void BtnPolishOk_Click(object sender, EventArgs e)
        {
            try
            {
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

                    if (Val.ToBoolean(DRow["POLISHMERGE"]) == true)
                    {
                        Global.Message("This Packets are Already Transfer... You Can't Transfer Agin");
                        return;
                    }
                }

                DataTable DTabDistinct = DTab.DefaultView.ToTable(true, "STATUS");
                DTabDistinct.Dispose();
                DTabDistinct = null;

                FrmSinglePolishOKTransfer FrmSinglePolishOKTransfer = new FrmSinglePolishOKTransfer();
                FrmSinglePolishOKTransfer.MdiParent = Global.gMainRef;
                FrmSinglePolishOKTransfer.FormClosing += new FormClosingEventHandler(FromPolishOk_Closing);
                FrmSinglePolishOKTransfer.ShowForm(DTab);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void FromPolishOk_Closing(object sender, FormClosingEventArgs e)
        {
            DTabPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            CalculateSummary();
            txtKapanName.Focus();
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            DTabPacketLiveStock.Rows.Clear();
            ObjGridSelection.ClearSelection();
            CalculateSummary();
            txtKapanName.Focus();
        }
    }
}
