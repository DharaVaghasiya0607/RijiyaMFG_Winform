using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Diagnostics;
using System.Collections;
using DevExpress.XtraPrinting;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Data;
using BusLib.Account;
using BusLib.Configuration;
using BusLib.Transaction;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using System.IO;
using AxoneMFGRJ.Parcel;
using DevExpress.XtraGrid.Columns;
using BusLib;

namespace AxoneMFGRJ.Account
{
    public partial class FrmRejectionView : DevExpress.XtraEditors.XtraForm
    {
        BOTRN_Rejection ObjRejection = new BOTRN_Rejection();

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BODevGridSelection ObjGridSelection;

        #region Constructor

        double DouCarat = 0;//urvisha
        double DouRejectionAmount = 0;//urvisha
        //double DouSaleRapaport = 0;//urvisha
        //double DouSaleRapaportAmt = 0;//urvisha
        double DouRejectionCarat = 0;//urvisha
        public FrmRejectionView()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();

            if (MainGridDet.RepositoryItems.Count == 1)
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
            CmbKapan.Properties.DataSource = new BOTRN_SinglePacketCreate().FindKapan();
            CmbKapan.Properties.DisplayMember = "KAPANNAME";
            CmbKapan.Properties.ValueMember = "KAPANNAME";
            CmbKapan.Focus();
            GrpParcelPacketCreate.Visible = false;

        }

        private void AttachFormDefaultEvent()
        {
            this.KeyPreview = true;
            ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = false;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjRejection);
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);

        }

        #endregion


        #region Control Events

        #endregion

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapan = Val.Trim(CmbKapan.Properties.GetCheckedItems());
                string StrFromDate = null;
                string StrToDate = null;
                string StrMainManager_ID = null;

                if (DTPFromDate.Checked == true)
                {
                    StrFromDate = Val.SqlDate(DTPFromDate.Value.ToShortDateString());
                }
                if (DTPToDate.Checked == true)
                {
                    StrToDate = Val.SqlDate(DTPToDate.Value.ToShortDateString());
                }

                StrMainManager_ID = Val.Trim(txtMultiMainManager.Text).Equals(string.Empty) ? "" : Val.ToString(txtMultiMainManager.Tag);

                if (txtMarker.Text.Trim().Length == 0)
                {
                    txtMarker.Tag = "";
                }
                this.Cursor = Cursors.WaitCursor;

                DataSet DS = ObjRejection.GetRejectionView(StrKapan, Val.ToInt64(txtMarker.Tag), StrFromDate, StrToDate, StrMainManager_ID);

                MainGridSum.DataSource = DS.Tables[0];
                MainGridSum.Refresh();
                GrdDetSum.BestFitColumns();

                MainGridDet.DataSource = DS.Tables[1];
                MainGridDet.Refresh();
                GrdDet.BestFitColumns();

                ObjGridSelection.ClearSelection();

                GrdDetSum.ExpandAllGroups();
                GrdDetSum.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "KAPANNAME", GrdDetSum.Columns["KAPANNAME"], "{0:N0}");
                GrdDetSum.GroupSummary.Add(DevExpress.Data.SummaryItemType.Count, "REJECTIONNAME", GrdDetSum.Columns["REJECTIONNAME"], "{0:N0}");
                GrdDetSum.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "CARAT", GrdDetSum.Columns["CARAT"], "{0:N3}");
                GrdDetSum.GroupSummary.Add(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", GrdDetSum.Columns["AMOUNT"], "{0:N4}");

                PnlParcelMerge.Enabled = false;
                txtKapanName.Text = string.Empty;
                txtKapanName.Tag = string.Empty;
                txtPacketNo.Text = string.Empty;
                txtPacketNo.Tag = string.Empty;


                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }


        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("RejectionDetail", GrdDet);
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("RejectionSummary", GrdDetSum);
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("Are You Sure You Want To Delete this Entry ?") == System.Windows.Forms.DialogResult.Yes)
                {
                    TRN_RejectionProperty Property = new TRN_RejectionProperty();
                    Property.REJECTIONTRN_ID = Val.ToInt64(GrdDet.GetFocusedRowCellValue("REJECTIONTRN_ID"));

                    Property = ObjRejection.DeleteRejectionID(Property);
                    Global.Message(Property.ReturnMessageDesc);
                    if (Property.ReturnMessageType == "SUCCESS")
                    {
                        BtnShow_Click(null, null);
                    }
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
            }

        }

        private void txtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME,PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MIXPACKET);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void Form_Closing(object sender, FormClosingEventArgs e)
        {
            BtnShow_Click(null, null);
        }

        private void btnMerge_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Packet For Transfer");
                    return;
                }

                double pDouCarat = 0;
                Int64 pIntKapan_id = 0;
                string pStrKapanName = "";
                double pIntExistingPkt = 0;
                string pStrMarketName = "";
                Int64 pIntMarker_ID = 0;
                Int64 pIntPkt_ID = 0;
                bool pBoolIsMerge = false;
                int pIntPacketNo = 0;
                string pStrTag = "";

                foreach (DataRow DRow in DTab.Rows)
                {
                    pDouCarat = Math.Round(pDouCarat + Val.Val(DRow["CARAT"]), 3);
                    pIntKapan_id = Val.ToInt64(DRow["KAPAN_ID"]);
                    pStrKapanName = Val.ToString(DRow["KAPANNAME"]);
                    pBoolIsMerge = Val.ToBoolean(DRow["ISREJECTIONMERGE"]);
                    pIntPacketNo = Val.ToInt32(DRow["PACKETNO"]);
                    pStrTag = Val.ToString(DRow["TAG"]);
                }

                DataTable DTabValidation = ObjRejection.ValidationForMergePkt(pStrKapanName, pIntPacketNo, pStrTag);
                if (DTabValidation.Rows.Count > 0)
                {
                    string pStrReturnMasType = DTabValidation.Rows[0]["RETURNMESSAGETYPE"].ToString();
                    if (pStrReturnMasType == "FAIL")
                    {
                        Global.Message("This Packet is Alreay Merge in:" + pStrKapanName);
                        return;
                    }
                }

                if (pBoolIsMerge == true)
                {
                    Global.Message("This Packet is Alreay Merge in:" + pStrKapanName);
                    return;
                }

                if (pStrKapanName != Val.ToString(txtKapanName.Text))
                {
                    Global.Message("You can't Merge This Packet Coz Selected Kapan And Merge Kapan IS Different..Pls Check.");
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    return;
                }

                DataTable DTabDistinctKapnan = DTab.DefaultView.ToTable(true, "KAPANNAME");
                if (DTabDistinctKapnan.Rows.Count > 1)
                {
                    Global.Message("Please Select Only One Kapan Print");
                    return;
                }

                DataTable DTabExistibPkt = ObjRejection.GetDataForMergeExistingPkt(Val.ToInt32(txtPacketNo.Text), pIntKapan_id);
                if (DTabExistibPkt.Rows.Count > 0)
                {
                    pIntExistingPkt = Val.Val(DTabExistibPkt.Rows[0]["LOTCARAT"]);
                    pStrMarketName = Val.ToString(DTabExistibPkt.Rows[0]["MARKERNAME"]);
                    pIntMarker_ID = Val.ToInt64(DTabExistibPkt.Rows[0]["MARKER_ID"]);
                    pIntPkt_ID = Val.ToInt64(DTabExistibPkt.Rows[0]["PACKET_ID"]);
                }

                DTab.TableName = "Table";
                string StrXMLValues = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    StrXMLValues = sw.ToString();
                }

                FrmMixPacketCreate FrmMixPackertCreate = new FrmMixPacketCreate();
                FrmMixPackertCreate.MdiParent = Global.gMainRef;
                FrmMixPackertCreate.Tag = ("PacketCreate");
                FrmMixPackertCreate.ShowForm(StrXMLValues, pDouCarat, Parcel.FrmMixPacketCreate.FORMTYPE.TransferToParcel, pIntKapan_id, pStrKapanName, pIntExistingPkt, Val.ToInt32(txtPacketNo.Text), pStrMarketName, pIntMarker_ID, pIntPkt_ID);
                FrmMixPackertCreate.FormClosing += new FormClosingEventHandler(Form_Closing);
                BtnShow_Click(null, null);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void txtKapanName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME,PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MIXPACKET);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapanName.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
                        txtPacketNo.Text = Val.ToString(FrmSearch.mDRow["PACKETNO"]);
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

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtKapanName.Text = string.Empty;
            txtPacketNo.Text = string.Empty;

            txtMarker.Text = string.Empty;
            txtMarker.Tag = string.Empty;
            CmbKapan.SetEditValue(0);
            DTPFromDate.Text = Val.ToString(DateTime.Now);
            DTPToDate.Text = Val.ToString(DateTime.Now);
            DTPFromDate.Checked = false;
            DTPToDate.Checked = false;
        }

        private void txtMarker_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (txtMarker.Enabled == false)
                {
                    return;
                }
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
                        txtMarker.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]);
                        txtMarker.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnExisting_Click(object sender, EventArgs e)
        {
            PnlParcelMerge.Enabled = true;
        }

        private void BrnNew_Click(object sender, EventArgs e)
        {
            try
            {
                PnlParcelMerge.Enabled = false;

                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab == null || DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select Atleast One Stone For Transfer");
                    return;
                }

                double pDouCarat = 0;
                Int64 pIntKapan_id = 0;
                string pStrKapanName = "";
                string pStrMarketName = "";
                Int64 pIntMarker_ID = 0;

                DataTable DTabDistinctKapnan = DTab.DefaultView.ToTable(true, "KAPANNAME");
                if (DTabDistinctKapnan.Rows.Count > 1)
                {
                    Global.Message("Please Select Only One Kapan For Transfer to Parcel");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    pDouCarat = Math.Round(pDouCarat + Val.Val(DRow["CARAT"]), 2);
                    pIntKapan_id = Val.ToInt64(DRow["KAPAN_ID"]);
                    pStrKapanName = Val.ToString(DRow["KAPANNAME"]);
                }

                DTab.TableName = "Table";
                string StrXMLValues = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    StrXMLValues = sw.ToString();
                }

                FrmMixPacketCreate FrmMixPackertCreate = new FrmMixPacketCreate();
                FrmMixPackertCreate.MdiParent = Global.gMainRef;
                FrmMixPackertCreate.Tag = ("PacketCreate");
                FrmMixPackertCreate.ShowForm(StrXMLValues, pDouCarat, Parcel.FrmMixPacketCreate.FORMTYPE.TransferToParcel, pIntKapan_id, pStrKapanName, 0, Val.ToInt32(txtPacketNo.Text), pStrMarketName, pIntMarker_ID, 0);
                FrmMixPackertCreate.FormClosing += new FormClosingEventHandler(Form_Closing);
                BtnShow_Click(null, null);
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message);
            }
        }

        private void txtMultiMainManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBoxMultipleSelect FrmSearch = new FrmSearchPopupBoxMultipleSelect();
                    FrmSearch.mDTab = new BOComboFill().FillCmb(BOComboFill.TABLE.MST_MAINMANAGER);
                    FrmSearch.mStrColumnsToHide = "LEDGER_ID,AUTOCONFIRM";
                    FrmSearch.ValueMemeter = "LEDGER_ID";
                    FrmSearch.DisplayMemeter = "LEDGERCODE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.SelectedDisplaymember != "" && FrmSearch.SelectedValuemember != "")
                    {
                        txtMultiMainManager.Text = Val.ToString(FrmSearch.SelectedDisplaymember);
                        txtMultiMainManager.Tag = Val.ToString(FrmSearch.SelectedValuemember);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDet_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
        {
            try
            {
                if (e.SummaryProcess == CustomSummaryProcess.Start)
                {
                    DouCarat = 0;
                    DouRejectionAmount = 0;
                    DouRejectionCarat = 0;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
                {
                    DouCarat = DouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "CARAT"));

                    DouRejectionAmount = DouRejectionAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "AMOUNT"));
                    DouRejectionCarat = DouRejectionAmount / DouCarat;
                }
                else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
                {

                    if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RATE") == 0)
                    {
                        if (Val.Val(DouCarat) > 0)
                            e.TotalValue = Math.Round(Val.Val(DouRejectionAmount) / Val.Val(DouCarat), 2);
                        else
                            e.TotalValue = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repBtnParcelTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDetSum.FocusedRowHandle < 0)
                {
                    return;
                }
                GrpParcelPacketCreate.Visible = true;
                DataRow Dr = GrdDetSum.GetFocusedDataRow();
                if (Dr != null)
                {
                    txtParcelPktCrtKapan.Text = Val.ToString(Dr["KAPANNAME"]);
                    txtParcelPktCrtKapan.Tag = Val.ToString(Dr["KAPAN_ID"]);
                    txtExtraCts.Text = Val.ToString(Dr["CARAT"]);
                    TxtRejection_ID.Text = Val.ToString(Dr["REJECTION_ID"]);
                    txtParcelPktCrtTransCts.Focus();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSinglePktCreate_Click(object sender, EventArgs e)
        {
            try
            {

                if (Val.Val(txtParcelPktCrtTransCts.Text) <= 0)
                {
                    Global.MessageError("Please Enter Proper Transfer Carat..");
                    txtParcelPktCrtTransCts.Focus();
                    return;
                }
                else if (Val.Val(txtParcelPktCrtTransCts.Text) > Val.Val(txtExtraCts.Text))
                {
                    Global.MessageError("Transfer Carat should not be greater than balance carat...Please Check");
                    txtParcelPktCrtTransCts.Focus();
                    return;
                }
                if (txtMainManager.Text.Trim().Length == 0)//Gunjan:27/03/2023
                {
                    Global.Message("Kapan Main Manager Is Required");
                    txtMainManager.Focus();
                    return;
                }
                this.Cursor = Cursors.WaitCursor;

                TRN_RejectionProperty Property = new TRN_RejectionProperty();
                Property.KAPAN_ID = Val.ToInt64(txtParcelPktCrtKapan.Tag);
                Property.KAPANNAME = Val.ToString(txtParcelPktCrtKapan.Text);
                Property.CARAT = Val.Val(txtExtraCts.Text);
                Property.TRANSFERCARAT = Val.Val(txtParcelPktCrtTransCts.Text);
                Property.REJECTION_ID = Val.ToInt32(TxtRejection_ID.Text);
                Property.KAPANMAINMANAGER_ID = Val.ToInt64(txtMainManager.Tag);//GUNJAN : 27/03/2023

                Property = new BOTRN_Rejection().CaratTransferToParcel(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnSnglPktCrtClose_Click(null, null);
                    BtnShow_Click(null, null);
                }
                this.Cursor = Cursors.Default;

            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnSinglePktCreateExit_Click(object sender, EventArgs e)
        {
            GrpParcelPacketCreate.Visible = false;

            txtParcelPktCrtKapan.Text = string.Empty;
            txtParcelPktCrtKapan.Tag = string.Empty;
            txtParcelPktCrtTransCts.Text = string.Empty;
            txtExtraCts.Text = string.Empty;
        }

        private void BtnSnglPktCrtClose_Click(object sender, EventArgs e)
        {
            BtnSinglePktCreateExit_Click(null, null);
        }

        private void txtMainManager_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtMainManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtMainManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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


        private void RepMakTransfer_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = "";
                Int64 StrKapan_ID = 0;

                double pDouBalanceCts = 0;
                int Rejection_ID = 0;

                if (GrdDetSum.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = 0;
                }
                else
                {
                    DataRow Dr = GrdDetSum.GetFocusedDataRow();
                    if (Dr != null)
                    {
                        StrKapanName = Val.ToString(Dr["KAPANNAME"]);
                        StrKapan_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                        pDouBalanceCts = Val.Val(Dr["CARAT"]);
                        Rejection_ID = Val.ToInt32(Dr["REJECTION_ID"]);
                    }
                }
                FrmMakebalPacketCreateTest FrmMakebalPacketCreateTest = new FrmMakebalPacketCreateTest();
                FrmMakebalPacketCreateTest.MdiParent = Global.gMainRef;
                if ((Rejection_ID == 530) && (pDouBalanceCts >= 0)) // Extra 
                {
                    FrmMakebalPacketCreateTest.ShowForm(StrKapanName, StrKapan_ID, pDouBalanceCts, Rejection_ID);
                }
                FrmMakebalPacketCreateTest.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString()); ;
            }
        }

        private void RepBtnOrderRejection_Click(object sender, EventArgs e)
        {
            try
            {
                string StrKapanName = "";
                Int64 StrKapan_ID = 0;

                double pDouBalanceCts = 0;
                int Rejection_ID = 0;
                string pStrOpe = "";

                if (GrdDetSum.FocusedRowHandle < 0)
                {
                    StrKapanName = string.Empty;
                    StrKapan_ID = 0;
                }
                else
                {
                    DataRow Dr = GrdDetSum.GetFocusedDataRow();
                    if (Dr != null)
                    {
                        StrKapanName = Val.ToString(Dr["KAPANNAME"]);
                        StrKapan_ID = Val.ToInt64(Dr["KAPAN_ID"]);
                        pDouBalanceCts = Val.Val(Dr["CARAT"]);
                        Rejection_ID = Val.ToInt32(Dr["REJECTION_ID"]);

                    }
                }
                FrmMakebalPacketCreateTest FrmMakebalPacketCreateTest = new FrmMakebalPacketCreateTest();
                FrmMakebalPacketCreateTest.MdiParent = Global.gMainRef;
                if ((Rejection_ID == 5185) && (pDouBalanceCts >= 0)) // Order Rejection
                {
                    FrmMakebalPacketCreateTest.OrderRejectionShowForm(StrKapanName, StrKapan_ID, pDouBalanceCts, Rejection_ID, "ORDER REJECTION");
                }
                FrmMakebalPacketCreateTest.FormClosing += new FormClosingEventHandler(Form_Closing);
            }
            catch(Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }
    }
}