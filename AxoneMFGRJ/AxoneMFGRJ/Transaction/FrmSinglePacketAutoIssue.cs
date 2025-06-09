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
using System.Text.RegularExpressions;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSinglePacketAutoIssue : DevExpress.XtraEditors.XtraForm
    {
        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        FORMTYPE mFormType = FORMTYPE.ROUGH;
        string mStrParentFormType = "";

        public enum FORMTYPE
        {
            ROUGH = 0,
            MAK = 1,
            POL = 2,
            ALL = 3,
            BOMBAY = 4
        }

        #region Property Settings

        public FrmSinglePacketAutoIssue()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            // Action Button Rights
            EmployeeActionRightsProperty Property = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            Property = null;

            this.Show();

            //BtnSearch.PerformClick();

            DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketAutoIssue("NONE", 0, "NONE", null, null);
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

            string Str = new BOTRN_KapanCreate().GetGridLayout(this.Name, GrdDet.Name);

            if (Str != "")
            {
                byte[] byteArray = Encoding.ASCII.GetBytes(Str);
                MemoryStream stream = new MemoryStream(byteArray);
                GrdDet.RestoreLayoutFromStream(stream);

            }

            txtKapanName.Focus();

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

        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
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

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);
            if (DTab == null)
            {
                return;
            }

            foreach (DataRow DRow in DTab.Rows)
            {
                IntSelPcs = IntSelPcs + 1;
                DouSelCarat = DouSelCarat + Val.Val(DRow["BALANCECARAT"]);
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
        }

        private void MainGrid_KeyUp(object sender, KeyEventArgs e)
        {
            CalculateSummary();
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

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
                    DataTable DTabSelectedData = ObjKapan.GetDataForSinglePacketAutoIssue(txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, null, null);

                    if (DTabSelectedData.Rows.Count <= 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
                        txtKapanName.Text = string.Empty;
                        txtPacketNo.Text = string.Empty;
                        txtTag.Text = string.Empty;
                        txtKapanName.Focus();
                        return;
                    }

                    DataRow DRow = DTabSelectedData.Rows[0];
                    if (DRow == null)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
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
                        GrdDet.SetRowCellValue(DTabPacketLiveStock.Rows.Count - 1, "COLSELECTCHECKBOX", true);
                    }
                    DRow = null;
                }

                GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
                GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
                GrdDet.RefreshData();

                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();
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

            }

            //bool ISFind = false;
            //for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            //{
            //    DataRow DRow = GrdDet.GetDataRow(IntI);
            //    if (txtKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
            //        && txtPacketNo.Text.Trim() == Val.ToString(DRow["PACKETNO"]).Trim()
            //        && txtTag.Text.Trim() == Val.ToString(DRow["TAG"]).Trim()
            //        )
            //    {
            //        ISFind = true;
            //        GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
            //        txtKapanName.Text = string.Empty;
            //        txtPacketNo.Text = string.Empty;
            //        txtTag.Text = string.Empty;

            //        txtKapanName.Focus();
            //        CalculateSummary();
            //        break;
            //    }
            //}

            //if (ISFind == false)
            //{
            //    Global.MessageError(txtKapanName.Text + "-" + txtPacketNo.Text + "-" + txtTag.Text + " Packet Not In Stock Kindly Check");
            //    txtKapanName.Text = string.Empty;
            //    txtPacketNo.Text = string.Empty;
            //    txtTag.Text = string.Empty;

            //    txtKapanName.Focus();
            //}
            //else
            //{
            //    GrdDet.Columns["COLSELECTCHECKBOX"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;
            //    GrdDet.Columns["COLSELECTCHECKBOX"].SortMode = DevExpress.XtraGrid.ColumnSortMode.Value;
            //    GrdDet.RefreshData();
            //}

        }

        private void BtnReturnWithSplit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

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
                DataTable DTabPacket = GetTableOfSelectedRows(GrdDet, true);

                if (DTabPacket == null || DTabPacket.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    if (Val.ISDate(DRow["CONFDATE"]) == false)
                    {
                        Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                        return;
                    }
                }

                DataTable DTabDistinct = DTabPacket.DefaultView.ToTable(true, "STATUS");
                if (DTabDistinct == null)
                {
                    Global.Message("Status Is Blank");
                    return;
                }
                if (DTabDistinct.Rows.Count != 1)
                {
                    Global.Message("You Have Selected Multiple Status(Rough,Makable,Polish) Stone \n\nKindly Select Only One Status(Rough,Makable,Polish) Stones For Action.");
                    return;
                }

                if (Global.Confirm("Are You Sure to Issue Selected Packets To Employee..? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                mStrParentFormType = Val.ToString(DTabDistinct.Rows[0]["STATUS"]);

                this.Cursor = Cursors.WaitCursor;

                string EntryType = "";
                EntryType = "EMPISS";

                string StrTransDate = "";

                StrTransDate = DateTime.Now.ToString();
                txtJangedNo.Text = string.Empty;

                int IntSrNo = 0;
                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = 0;
                    Property.OLDTRN_ID = Val.ToInt64(DRow["TRN_ID"].ToString());
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"].ToString());

                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                    Property.TAG = Val.ToString(DRow["TAG"].ToString());

                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());
                    Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);
                    Property.ENTRYSRNO = IntSrNo;
                    Property.ENTRYTYPE = EntryType;

                    Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["TODEPARTMENT_ID"]);
                    Property.TODEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);

                    Property.FROMMANAGER_ID = Val.ToInt64(DRow["TOMANAGER_ID"]);
                    Property.TOMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);

                    Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["TOEMPLOYEE_ID"]);
                    Property.TOEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);

                    Property.FROMPROCESS_ID = Val.ToInt32(DRow["TOPROCESS_ID"]);
                    Property.TOPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]);
                    Property.NEXTPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]); //RequiredProcess

                    Property.ISSUEPCS = Val.ToInt32(DRow["BALANCEPCS"]);
                    Property.ISSUECARAT = Val.Val(DRow["BALANCECARAT"]);

                    //Property.RETURNTYPE = Val.ToString(DRow["RETURNTYPE"]);
                    Property.RETURNTYPE = "DONE";

                    if (Property.RETURNTYPE == "DONE")
                    {
                        Property.READYPCS = Val.ToInt32(DRow["BALANCEPCS"]);
                        Property.READYCARAT = Val.Val(DRow["BALANCECARAT"]);

                        Property.RRPCS = 0;
                        Property.RRCARAT = 0;

                        Property.LOSTPCS = 0;
                        Property.LOSTCARAT = 0;
                        Property.LOSSCARAT = 0;
                        Property.MIXINGLESSPLUS = 0;
                    }
                    else if (Property.RETURNTYPE == "NOT DONE")
                    {
                        Property.READYPCS = 0;
                        Property.READYCARAT = 0.00;

                        Property.RRPCS = Val.ToInt32(DRow["BALANCEPCS"]);
                        Property.RRCARAT = Val.Val(DRow["BALANCECARAT"]);

                        Property.LOSTPCS = 0;
                        Property.LOSTCARAT = 0;
                        Property.LOSSCARAT = 0;
                        Property.MIXINGLESSPLUS = 0;
                    }

                    Property.TRANSDATE = Val.SqlDate(StrTransDate);
                    Property.TRANSTYPE = EntryType;
                    Property.REMARK = "Auto Issue";
                    Property.AUTOCONFIRM = Val.ToBoolean(DRow["FROMEMPAUTOCONFIRM"]);
                    Property = new BOTRN_SingleIssueReturn().TransferGoods(Property);
                    txtJangedNo.Text = Property.JANGEDNO.ToString();
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(Property.ReturnMessageDesc);
                        txtJangedNo.Text = "0";
                        this.Cursor = Cursors.WaitCursor;

                        if (Property.ReturnValue == "-5")
                            break;
                    }
                    Property = null;
                }
                this.Cursor = Cursors.Default;
                if (Val.Val(txtJangedNo.Text) != 0)
                {
                    BtnPrint_Click(null, null);
                    btnSearch_Click(null, null);
                }
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
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

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

        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitMaxRowCount = 5000000;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
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
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

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

        private void BtnTransferToMix_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
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

                PanelHeader.Enabled = false;
                panel2.Enabled = false;
                MainGrid.Enabled = false;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnParcelDetailClose_Click(object sender, EventArgs e)
        {
            try
            {
                PanelHeader.Enabled = true;
                panel2.Enabled = true;
                MainGrid.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnTransferToPCNRejection_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
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

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;


                string pFromDate = "";
                string pToDate = "";

                if (DtpFromDate.Checked == true)
                {
                    pFromDate = Val.SqlDate(DtpFromDate.Value.ToShortDateString());
                }
                if (DtpToDate.Checked == true)
                {
                    pToDate = Val.SqlDate(DtpToDate.Value.ToShortDateString());
                }

                DTabPacketLiveStock.Rows.Clear();
                ObjGridSelection.ClearSelection();

                DTabPacketLiveStock = ObjKapan.GetDataForSinglePacketAutoIssue(Val.ToString(txtKapanName.Text), Val.ToInt32(txtPacketNo.Text), Val.ToString(txtTag.Text), pFromDate, pToDate);
                MainGrid.DataSource = DTabPacketLiveStock;
                GrdDet.BestFitMaxRowCount = 500;
                GrdDet.BestFitColumns();
                MainGrid.Refresh();

                CalculateSummary();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
            }

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DataTable DTab = new BOTRN_SingleIssueReturn().PopupJangedForPrint(mStrParentFormType, Val.ToInt64(txtJangedNo.Text));
            if (DTab.Rows.Count == 0)
            {
                Global.MessageError("There Is No Data For Print");
                return;
            }
            FrmReportViewer FrmReportViewer = new FrmReportViewer();
            FrmReportViewer.MdiParent = Global.gMainRef;
            if (mStrParentFormType == "ROUGH")
            {
                FrmReportViewer.ShowWithPrint("JangedPrint", DTab);
            }
            else
            {
                FrmReportViewer.ShowWithPrint("JangedPrintGrd", DTab);
            }
        }
    }
}
