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
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using AxoneMFGRJ.Report;

namespace AxoneMFGRJ.Parcel
{
    public partial class FrmParcelGoodsTransfer : DevExpress.XtraEditors.XtraForm
    {

        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();

        DataTable DTabPacket = new DataTable();

        bool mBoolAutoConfirm = false;

        public enum FORMTYPE
        {
            TRANSFER = 1,
            STAFFISSUE = 2,
            STAFFRETURN = 3,
            STAFFRETURNWITHMERGE = 4
        }

        public FORMTYPE mFormType { get; set; }

        #region Property Settings

        public FrmParcelGoodsTransfer()
        {
            InitializeComponent();
        }

        public void ShowForm(DataTable pDTabStock, FORMTYPE pFormType)
        {


            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            mFormType = pFormType;

            DTPTransferDate.Value = DateTime.Now;

            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETTYPE", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("READYPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("READYCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("RRPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("RRCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("EXTRAPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("EXTRACARAT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("LOSTPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("LOSTCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("LOSSCARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("MIXINGLESSPLUS", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMEMPLOYEENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMDEPARTMENTNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("FROMPROCESSNAME", typeof(string)));

            //#P : 08-10-2019 : Used For Check Packet Lock Setting
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEE_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("TOEMPLOYEENAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENT_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TODEPARTMENTNAME", typeof(string)));
            // End : #P : 08-10-2019

            DTabPacket.Columns.Add(new DataColumn("TOPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TOPROCESSNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("NEXTPROCESS_ID", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("NEXTPROCESSNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("RETURNTYPE", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("FROMMANAGER_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("FROMMANAGERNAME", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("REMARK", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("MAINPACKET_ID", typeof(Int64)));

            foreach (DataRow DR in pDTabStock.Rows)
            {
                DataRow DRNew = DTabPacket.NewRow();

                DRNew["MAINPACKET_ID"] = DR["MAINPACKET_ID"];
                DRNew["PACKET_ID"] = DR["PACKET_ID"];
                DRNew["KAPAN_ID"] = DR["KAPAN_ID"];
                DRNew["KAPANNAME"] = DR["KAPANNAME"];
                DRNew["PACKETNO"] = DR["PACKETNO"];
                DRNew["TAG"] = DR["TAG"];
                DRNew["PACKETTYPE"] = DR["PACKETTYPE"];

                DRNew["BARCODE"] = DR["BARCODE"];

                DRNew["ISSUEPCS"] = DR["BALANCEPCS"];
                DRNew["ISSUECARAT"] = DR["BALANCECARAT"];
                DRNew["READYPCS"] = DR["BALANCEPCS"];
                DRNew["READYCARAT"] = DR["BALANCECARAT"];

                DRNew["LOSTPCS"] = 0;
                DRNew["LOSTCARAT"] = 0.00;
                DRNew["RRPCS"] = 0;
                DRNew["RRCARAT"] = 0.00;
                DRNew["EXTRAPCS"] = 0;
                DRNew["EXTRACARAT"] = 0.00;
                DRNew["LOSSCARAT"] = 0.00;
                DRNew["MIXINGLESSPLUS"] = 0.00;
                DRNew["FROMEMPLOYEE_ID"] = DR["EMPLOYEE_ID"];
                DRNew["FROMEMPLOYEENAME"] = DR["EMPLOYEENAME"];
                DRNew["FROMMANAGER_ID"] = DR["TOMANAGER_ID"];
                DRNew["FROMMANAGERNAME"] = DR["MANAGERNAME"];
                DRNew["FROMDEPARTMENT_ID"] = DR["DEPARTMENT_ID"];
                DRNew["FROMDEPARTMENTNAME"] = DR["DEPARTMENTNAME"];
                DRNew["FROMPROCESS_ID"] = DR["PROCESS_ID"];
                DRNew["FROMPROCESSNAME"] = DR["PROCESSNAME"];
                DRNew["TOPROCESS_ID"] = Val.ToInt(txtProcessTo.Tag);
                DRNew["TOPROCESSNAME"] = txtProcessTo.Text;
                DRNew["NEXTPROCESS_ID"] = Val.ToInt(txtRequiredProcess.Tag);
                DRNew["NEXTPROCESSNAME"] = txtRequiredProcess.Text;
                DRNew["RETURNTYPE"] = "DONE";
                DRNew["REMARK"] = "";

                DTabPacket.Rows.Add(DRNew);
            }

            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();
            RbtTransfer_CheckedChanged(null, null);

            CalculateSummary();

            if (pFormType == FORMTYPE.TRANSFER)
            {
                RbtTransfer.Checked = true;
                txtTransferTo.Focus();

                GrdDet.Columns["ISSUEPCS"].Caption = "BalPcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "BalCts";
                GrdDet.Columns["READYPCS"].Caption = "TrfPcs";
                GrdDet.Columns["READYCARAT"].Caption = "TrfCts";

                GrdDet.Columns["RRCARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["EXTRACARAT"].Visible = false;
                GrdDet.Columns["LOSSCARAT"].Visible = false;
                GrdDet.Columns["LOSSCARAT"].Visible = true;

                //  GrdDet.Columns["LOSSPCS"].Visible = false;

            }
            else if (pFormType == FORMTYPE.STAFFISSUE)
            {
                RbtStaffIssue.Checked = true;
                txtTransferTo.Focus();

                GrdDet.Columns["ISSUEPCS"].Caption = "BalPcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "BalCts";
                GrdDet.Columns["READYPCS"].Caption = "IssPcs";
                GrdDet.Columns["READYCARAT"].Caption = "IssCts";

                GrdDet.Columns["RRCARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["EXTRACARAT"].Visible = false;
                GrdDet.Columns["LOSSCARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["READYPCS"].OptionsColumn.AllowEdit = false;
            }
            else if (pFormType == FORMTYPE.STAFFRETURN)
            {
                RbtStaffReturn.Checked = true;
                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;

                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;

                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;

                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;


                GrdDet.Columns["ISSUEPCS"].Caption = "IssPcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "IssCts";
                GrdDet.Columns["READYPCS"].Caption = "RdyPcs";
                GrdDet.Columns["READYCARAT"].Caption = "RdyCts";
                GrdDet.Columns["LOSSCARAT"].Visible = true;


                GrdDet.Columns["RRCARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["EXTRACARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                txtProcessTo.Focus();
            }
            else if (pFormType == FORMTYPE.STAFFRETURNWITHMERGE)
            {
                RbtStaffReturnWithMerge.Checked = true;
                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;

                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;

                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;

                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;


                GrdDet.Columns["ISSUEPCS"].Caption = "IssPcs";
                GrdDet.Columns["ISSUECARAT"].Caption = "IssCts";
                GrdDet.Columns["READYPCS"].Caption = "RdyPcs";
                GrdDet.Columns["READYCARAT"].Caption = "RdyCts";


                GrdDet.Columns["RRCARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["EXTRACARAT"].Visible = false;
                GrdDet.Columns["RRPCS"].Visible = false;
                GrdDet.Columns["LOSSCARAT"].Visible = true;
                txtProcessTo.Focus();
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
            ObjFormEvent.ObjToDisposeList.Add(ObjTrn);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        public void CalculateSummary()
        {
            int IntPcs = 0;
            double DouCarat = 0;

            for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
            {
                DataRow DRow = GrdDet.GetDataRow(IntI);
                IntPcs = IntPcs + 1;
                DouCarat = DouCarat + Val.Val(DRow["READYCARAT"]);
            }

            txtTotalPcs.Text = IntPcs.ToString();
            txtTotalCarat.Text = DouCarat.ToString();

        }

        private void FrmPurchaseLiveStock_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
                    this.Close();
            }

        }

        private void FrmKapanDashboard_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        this.Close();
            //}
        }


        private void BtnKapanLiveStockAutoFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }


        private void BtnKapanLiveStockExcelExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("PacketLiveStock", GrdDet);
        }

        private void txtTransferTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);

                    FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtTransferTo.Text = Val.ToString(FrmSearch.mDRow["LEDGERCODE"]);
                        txtTransferTo.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                        txtType.Text = Val.ToString(FrmSearch.mDRow["LEDGERGROUP"]);
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["MANAGERNAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["MANAGER_ID"]);
                        mBoolAutoConfirm = Val.ToBoolean(FrmSearch.mDRow["AUTOCONFIRM"]);

                        //#P : 08-10-2019
                        foreach (DataRow DRow in DTabPacket.Rows)
                        {
                            DRow["TOEMPLOYEE_ID"] = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);
                            DRow["TOEMPLOYEENAME"] = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]);

                            DRow["TODEPARTMENT_ID"] = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                            DRow["TODEPARTMENTNAME"] = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        }

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

        private void txtProcessTo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    BusLib.BOComboFill ObjCmb = new BOComboFill();
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        foreach (DataRow DRow in DTabPacket.Rows)
                        {
                            DRow["TOPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["TOPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        }

                        DTabPacket.AcceptChanges();
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

        private void txtRequiredProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    BusLib.BOComboFill ObjCmb = new BOComboFill();
                    //FrmSearch.mDTab = ObjCmb.FillCombo(BusLib.BOComboFill.TABLE.MST_REQUIREDPROCESS, Val.ToInt32(txtProcessTo.Tag));

                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        foreach (DataRow DRow in DTabPacket.Rows)
                        {
                            DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        }

                        DTabPacket.AcceptChanges();
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
            /*try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        foreach (DataRow DRow in DTabPacket.Rows)
                        {
                            DRow["NEXTPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["NEXTPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        }

                        DTabPacket.AcceptChanges();
                    }

                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }*/
        }

        public bool ValSave()
        {
            if (txtTransferTo.Text.Trim().Length == 0)
            {
                Global.MessageError("Transfer To Name Is Required");
                txtTransferTo.Focus();
                return false;
            }
            if (txtDepartment.Text.Trim().Length == 0)
            {
                Global.MessageError("Department Name Is Required");
                txtDepartment.Focus();
                return false;
            }
            if (txtType.Text.Trim().Length == 0)
            {
                Global.MessageError("Type Is Required");
                txtType.Focus();
                return false;
            }
            if (txtProcessTo.Text.Trim().Length == 0)
            {
                Global.MessageError("For Process Field Is Required");
                txtProcessTo.Focus();
                return false;
            }
            if (txtRequiredProcess.Text.Trim().Length == 0)
            {
                Global.MessageError("Required Process Field Is Required");
                txtRequiredProcess.Focus();
                return false;
            }

            int Int = 0;

            foreach (DataRow DRow in DTabPacket.Rows)
            {
                Int++;
                int IntPcs = Val.ToInt32(DRow["ISSUEPCS"]);
                double DouCarat = Val.Val(DRow["ISSUECARAT"]);

                int IntReadyPcs = Val.ToInt32(DRow["READYPCS"]);
                double DouReadyCarat = Val.Val(DRow["READYCARAT"]);

                int IntLostPcs = Val.ToInt32(DRow["LOSTPCS"]);
                double DouLostCarat = Val.Val(DRow["LOSTCARAT"]);

                double DouLossCarat = Val.Val(DRow["LOSSCARAT"]);
                double DouMixingLessPlus = Val.Val(DRow["MIXINGLESSPLUS"]);


                if (DouReadyCarat > DouCarat)
                {
                    Global.MessageError("Ready Carat Is Greater Than Issue Carat At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                    txtRequiredProcess.Focus();
                    return false;
                }

                if (DouLossCarat < 0)
                {
                    Global.MessageError("Loss Carat Not Less Then Zepro At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                    txtRequiredProcess.Focus();
                    return false;
                }

                if (mFormType == FORMTYPE.STAFFRETURN)
                {
                    int IntDiff = Val.ToInt32(IntPcs - IntReadyPcs - IntLostPcs);
                    double DouDiff = Math.Round(Val.Val(DouCarat - DouReadyCarat - DouLostCarat - DouLossCarat - DouMixingLessPlus), 3);

                    if (DouDiff != 0)
                    {
                        Global.MessageError("Carat Mismatched At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                        txtRequiredProcess.Focus();
                        return false;
                    }

                }
            }

            return true;
        }

        private void BtnTransfer_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave() == false)
                {
                    return;
                }


                if (Global.Confirm("Are You Sure You Want To Transfer ? ") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                this.Cursor = Cursors.WaitCursor;

                string EntryType = "";

                if (RbtTransfer.Checked == true)
                {
                    EntryType = RbtTransfer.Tag.ToString();
                }
                else if (RbtStaffIssue.Checked == true)
                {
                    EntryType = RbtStaffIssue.Tag.ToString();
                }
                else if (RbtStaffReturn.Checked == true)
                {
                    EntryType = RbtStaffReturn.Tag.ToString();
                }
                else if (RbtStaffReturnWithMerge.Checked == true)
                {
                    EntryType = RbtStaffReturnWithMerge.Tag.ToString();
                }


                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = 0;
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"]);
                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                    Property.TAG = Val.ToString(DRow["TAG"].ToString());

                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"]);
                    Property.MAINPACKET_ID = Val.ToInt64(DRow["MAINPACKET_ID"]);
                    Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);
                    Property.ENTRYSRNO = IntSrNo;
                    Property.ENTRYTYPE = EntryType;

                    Property.FROMDEPARTMENT_ID = Val.ToInt32(DRow["FROMDEPARTMENT_ID"]);
                    Property.FROMMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);

                    if (txtType.Text.ToUpper() == "JOBWORK")
                    {
                        Property.TODEPARTMENT_ID = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
                        Property.TOMANAGER_ID = Val.ToInt64(BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID);
                    }
                    else
                    {
                        Property.TODEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
                        Property.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);
                    }

                    Property.FROMEMPLOYEE_ID = Val.ToInt64(DRow["FROMEMPLOYEE_ID"]);
                    Property.TOEMPLOYEE_ID = Val.ToInt64(txtTransferTo.Tag);

                    //Property.FROMMANAGER_ID = Val.ToInt64(DRow["FROMMANAGER_ID"]);
                    //Property.TOMANAGER_ID = Val.ToInt64(txtManager.Tag);

                    Property.FROMPROCESS_ID = Val.ToInt32(DRow["FROMPROCESS_ID"]);
                    Property.TOPROCESS_ID = Val.ToInt32(txtProcessTo.Tag);
                    Property.NEXTPROCESS_ID = Val.ToInt32(txtRequiredProcess.Tag);

                    if (mFormType == FORMTYPE.TRANSFER || mFormType == FORMTYPE.STAFFISSUE)
                    {
                        Property.ISSUEPCS = Val.ToInt32(DRow["READYPCS"]);
                        Property.ISSUECARAT = Val.Val(DRow["READYCARAT"]);
                    }
                    else
                    {
                        Property.ISSUEPCS = Val.ToInt32(DRow["READYPCS"]) + Val.ToInt32(DRow["RRPCS"]) + Val.ToInt32(DRow["EXTRAPCS"]);
                        Property.ISSUECARAT = Val.Val(DRow["READYCARAT"]) + Val.Val(DRow["LOSSCARAT"]) + Val.Val(DRow["RRCARAT"]) + Val.Val(DRow["EXTRACARAT"]);
                    }

                    Property.RETURNTYPE = Val.ToString(DRow["RETURNTYPE"]);

                    if (Property.RETURNTYPE == "DONE")
                    {
                        Property.READYPCS = Val.ToInt32(DRow["READYPCS"]);
                        Property.READYCARAT = Val.Val(DRow["READYCARAT"]);

                        Property.RRPCS = 0;
                        Property.RRCARAT = 0;

                        Property.LOSTPCS = Val.ToInt32(DRow["LOSTPCS"]);
                        Property.LOSTCARAT = Val.Val(DRow["LOSTCARAT"]);
                        Property.LOSSCARAT = Val.Val(DRow["LOSSCARAT"]);
                        Property.MIXINGLESSPLUS = Val.Val(DRow["MIXINGLESSPLUS"]);
                    }
                    else if (Property.RETURNTYPE == "NOT DONE")
                    {
                        Property.READYPCS = 0;
                        Property.READYCARAT = 0.00;

                        Property.RRPCS = Val.ToInt32(DRow["READYPCS"]);
                        Property.RRCARAT = Val.Val(DRow["READYCARAT"]);

                        Property.LOSTPCS = 0;
                        Property.LOSTCARAT = 0;
                        Property.LOSSCARAT = 0;
                        Property.MIXINGLESSPLUS = 0;
                    }

                    Property.TRANSDATE = Val.SqlDate(DTPTransferDate.Value.ToShortDateString());
                    Property.TRANSTYPE = EntryType;
                    Property.REMARK = Val.ToString(DRow["REMARK"]);
                    Property.AUTOCONFIRM = mBoolAutoConfirm;

                    Property.ISMERGE = (RbtStaffReturnWithMerge.Checked) ? true : false;

                    Property = ObjTrn.TransferGoodsParcel(Property);
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
                mBoolAutoConfirm = false;

                this.Cursor = Cursors.Default;
                if (Val.Val(txtJangedNo.Text) != 0)
                {
                    //Start :  Add By Vipul 20-07-2020
                    if (txtTransferTo.Text.Contains("GIA"))
                    {
                        /* temporary commet
                        if (Global.Confirm("Are You Sure You Want To Issue GIA Through API ?") == DialogResult.Yes)
                        {
                            IssueToGIA();
                        }*/
                    }
                    //End :  Add By Vipul 20-07-2020

                    BtnPrint_Click(null, null);
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.ToString());
            }


        }


        private void IssueToGIA()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string CLIENTID = "101189918233";
                string USERNAME = "SKASO002";

                List<JOB> listJob = new List<JOB>();

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    JOB ObjJob = new JOB();

                    ObjJob.CONTROL_NUMBER = Val.ToString(DRow["RFIDTAG"]);
                    ObjJob.STATED_SHAPE = Val.ToString(DRow["GIASHAPECODE"]);
                    ObjJob.STATED_WEIGHT = Val.Val(DRow["ISSUECARAT"]);
                    ObjJob.CLIENT_REF_NO = Val.ToString(DRow["GIACONTROLNO"]);
                    ObjJob.SUB_CLIENT_NO = CLIENTID;
                    ObjJob.SERVICE_CODES = txtProcessTo.Text == "RE-EX" && Convert.ToDouble(ObjJob.STATED_WEIGHT) < 1 ? "REEXDOSSDE" : txtProcessTo.Text;
                    ObjJob.INSCRIPTION_SERVICE_CODES = "";
                    ObjJob.INSCRIPTION_TEXT = "";
                    ObjJob.STATED_VALUE = Val.Val(DRow["GIAAMOUNT"]);
                    ObjJob.DESCRIPTION = "";
                    ObjJob.SEPERATION = "";
                    ObjJob.QUANTITY = "";
                    ObjJob.COLOR = Val.ToString(DRow["GIACOLORCODE"]);
                    ObjJob.CLARITY = Val.ToString(DRow["GIACLARITYCODE"]);
                    ObjJob.POLISH = Val.ToString(DRow["GIAPOLCODE"]);
                    ObjJob.SYMMETRY = Val.ToString(DRow["GIASYMCODE"]);
                    ObjJob.CLIENT_COMMENT = "";
                    ObjJob.PREVIOUS_REPORT_NO = Val.ToString(DRow["GIAREPORTNO"]);
                    ObjJob.STATED_MATERIAL = "";
                    ObjJob.ITEM_CATEGORY = "";
                    ObjJob.ITEM_DESCRIPTION = "";
                    ObjJob.ADDITIONAL_GEMPASSES = "";
                    ObjJob.IDENT_NO_OF_STONES = "";
                    ObjJob.IDENT_COLOR = "";
                    ObjJob.AR_FLAG = "";
                    ObjJob.AR_CLARITY = "";
                    ObjJob.AR_COLOR = "";
                    ObjJob.AR_POLISH = "";
                    ObjJob.AR_SYMMETRY = "";
                    ObjJob.AR_CUT = "";
                    ObjJob.AR_FLURO_INTENSITY = "";
                    ObjJob.AR_FLURO_COLOR = "";

                    listJob.Add(ObjJob);

                    ObjJob = null;
                }

                XmlDocument xmlDoc = Global.ConvertToXml(listJob);
                string filename = "<?xml version=\"1.0\" encoding=\"utf-8\"?><CREATE_JOB_FOR_INTAKE_REQUEST><HEADER><USER_NAME>" + USERNAME + "</USER_NAME><CLIENT_ID>" + CLIENTID + "</CLIENT_ID></HEADER><BODY><SITE_ID>7</SITE_ID><JOBS>";
                string xmlString = filename + xmlDoc.DocumentElement.InnerXml + " </JOBS></BODY></CREATE_JOB_FOR_INTAKE_REQUEST>";

                //ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12; //For .netFrameWork 4.5
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls; //For .netFrameWork 4

                GIAWebService.ConsolidatedWebServicesClient ObjLabService = new GIAWebService.ConsolidatedWebServicesClient();
                string StrLabXml = ObjLabService.createJob(xmlString);
                ArrayList list = new ArrayList();
                XmlDocument doc = new XmlDocument();
                doc.LoadXml(StrLabXml);
                XmlNodeList idNodes = doc.SelectNodes("CREATE_JOB_FOR_INTAKE_RESPONSE/STATUS");
                foreach (XmlNode node in idNodes)
                    list.Add(node.InnerText);

                if (list[0].ToString() == "SUCCESS")
                {
                    XmlNodeList idNodes1 = doc.SelectNodes("CREATE_JOB_FOR_INTAKE_RESPONSE/MESSAGE");
                    foreach (XmlNode node in idNodes1)
                        list.Add(node.InnerText);

                    string strMessge = list[1].ToString();
                    Global.Message(strMessge);
                }
                else
                {
                    XmlNodeList idNodes1 = doc.SelectNodes("CREATE_JOB_FOR_INTAKE_RESPONSE/ERROR");
                    foreach (XmlNode node in idNodes1)
                        list.Add(node.InnerText);

                    string strMessge = list[1].ToString();
                    Global.Message(strMessge);
                }
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }
        private void BtnPrint_Click(object sender, EventArgs e)
        {
            if (mFormType == FORMTYPE.TRANSFER || mFormType == FORMTYPE.STAFFISSUE)
            {

                string pStrOpe = "";
                if (RtbSummary.Checked == true)
                {
                    pStrOpe = "SUMMARY";
                }
                else //Detail And Marker Both
                {
                    pStrOpe = "DETAIL";
                }


                DataTable DTab = ObjTrn.PopupJangedForParcelPrint("ROUGH", Val.ToInt64(txtJangedNo.Text), null, pStrOpe);
                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                if (RtbDetail.Checked == true)
                {
                    FrmReportViewer.ShowWithPrint("JangedPrintWithDuplicate", DTab);
                }
                else
                {
                    FrmReportViewer.ShowWithPrint("JangedPrintSummaryWithDuplicate", DTab);
                }
            }
        }

        private void RbtTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtTransfer.Checked == true || RbtStaffIssue.Checked == true)
            {
                GrdDet.Columns["READYPCS"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = true;

                GrdDet.Columns["LOSTPCS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["LOSTCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["MIXINGLESSPLUS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["RETURNTYPE"].OptionsColumn.AllowEdit = false;

            }
            else
            {
                GrdDet.Columns["READYPCS"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = true;

                GrdDet.Columns["LOSTPCS"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["LOSTCARAT"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["MIXINGLESSPLUS"].OptionsColumn.AllowEdit = true;
                GrdDet.Columns["RETURNTYPE"].OptionsColumn.AllowEdit = true;


            }
        }

        private void GrdDet_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }
            DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
            switch (e.Column.FieldName.ToUpper())
            {
                case "READYCARAT":
                case "RRCARAT":
                case "EXTRACARAT":

                    if (mFormType == FORMTYPE.STAFFRETURN || mFormType == FORMTYPE.STAFFRETURNWITHMERGE)
                    {
                        double DouReady = Val.Val(DRow["READYCARAT"]);
                        double DouExtra = Val.Val(DRow["EXTRACARAT"]);
                        double DouRR = Val.Val(DRow["RRCARAT"]);
                        double DouIssue = Val.Val(DRow["ISSUECARAT"]);
                        double DouLossCarat = Math.Round(DouIssue - DouReady - DouExtra - DouRR, 3);

                        if (DouLossCarat < 0)
                        {
                            Global.MessageError("Loss Carat Is Less Then Zero");
                            GrdDet.SetRowCellValue(e.RowHandle, "READYCARAT", DouIssue);
                            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", 0);
                        }
                        else
                        {
                            GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", DouLossCarat);
                        }
                    }
                    else
                    {
                        GrdDet.SetRowCellValue(e.RowHandle, "LOSSCARAT", 0);
                    }

                    break;
                default:
                    break;
            }
        }

        private void txtJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressEveToPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchText = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.PopupJangedForParcelPrint("", 0, Val.SqlDate(DTPTransferDate.Value.ToShortDateString()));
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


        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtRequiredProcess_Validated(object sender, EventArgs e)
        {
            BtnSave.Focus();
        }

        private void GrdDet_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (mFormType == FORMTYPE.STAFFISSUE || mFormType == FORMTYPE.TRANSFER)
            {
                GridView view = sender as GridView;
                if ((view.FocusedColumn.FieldName == "READYPCS" || view.FocusedColumn.FieldName == "READYCARAT") && Val.ToString(view.GetFocusedRowCellValue("PACKETTYPE")) == "MIX")
                {
                    e.Cancel = false;
                }
                else
                {
                    e.Cancel = true;
                }
            }

            else if (mFormType == FORMTYPE.STAFFRETURN)
            {
                e.Cancel = false;
            }

        }

    }
}
