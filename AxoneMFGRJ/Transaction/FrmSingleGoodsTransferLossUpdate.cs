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
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Net;
using System.Xml;
using BarcodeLib.Barcode;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleGoodsTransferLossUpdate : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleIssueReturn ObjTrn = new BOTRN_SingleIssueReturn();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        DataTable DTabPacket = new DataTable();
        string mStrParentFormType = "";
        bool mBoolAutoConfirm = false;
        string mStrStockType = "";
        decimal ReadyCarat = 0;
        Int64 PacketID = 0;
        bool IsReadyCaratFocus = false;

        //FORMTYPE mFormType = FORMTYPE.ROUGH;

        public enum FORMTYPE
        {
            TRANSFER = 1,
            STAFFISSUE = 2,
            STAFFRETURN = 3
        }

        public FORMTYPE mFormType { get; set; }

        #region Property Settings

        public FrmSingleGoodsTransferLossUpdate()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            //RbtBarcode.Checked = true;
            //txtBarcode.Focus();
            DTPTransferDate.Value = DateTime.Now;

            DTabPacket = new DataTable();
            DTabPacket.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
            DTabPacket.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("TAG", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("MAINPACKETTAG", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("BARCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("RFIDTAG", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("ISSUEPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("ISSUECARAT", typeof(double)));
            DTabPacket.Columns.Add(new DataColumn("READYPCS", typeof(Int32)));
            DTabPacket.Columns.Add(new DataColumn("READYCARAT", typeof(double)));

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

            DTabPacket.Columns.Add(new DataColumn("TEMPMARKER_ID", typeof(Int64))); //hinal : 01-01-2022

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

            DTabPacket.Columns.Add(new DataColumn("OLDTRN_ID", typeof(Int64)));

            DTabPacket.Columns.Add(new DataColumn("REMARK", typeof(string)));


            DTabPacket.Columns.Add(new DataColumn("GIASHAPECODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIACLARITYCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIACOLORCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIACUTCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIAPOLCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIASYMCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIAFLCODE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIAREPORTNO", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIACONTROLNO", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("GIAAMOUNT", typeof(string)));

            DTabPacket.Columns.Add(new DataColumn("EXPWEIGHT", typeof(double)));

            DTabPacket.Columns.Add(new DataColumn("ENTRYTYPE", typeof(string)));
            DTabPacket.Columns.Add(new DataColumn("PKTSERIALNO", typeof(Int32)));

            ChkPrintBarcode.Visible = true; //#H : 01-01-2022

            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();

            CalculateSummary();

            if (RbtTransfer.Checked == true)
            {
                RbtTransfer.Checked = true;
                txtTransferTo.Focus();
                txtPolishCarat1.Visible = false;
                txtPolishCarat2.Visible = false;
                lblPolish.Visible = false;
                GrdDet.Columns["EXPWEIGHT"].Visible = false;
                DtpTransdate.Visible = false;
                lblTransdate.Visible = false;
            }
            else if (RbtStaffIssue.Checked == true)
            {
                RbtStaffIssue.Checked = true;
                txtTransferTo.Focus();
                txtPolishCarat1.Visible = false;
                txtPolishCarat2.Visible = false;
                lblPolish.Visible = false;
                GrdDet.Columns["EXPWEIGHT"].Visible = false;
                DtpTransdate.Visible = false;
                lblTransdate.Visible = false;
            }
            else if (RbtStaffReturn.Checked == true)
            {
                RbtStaffReturn.Checked = true;
                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;

                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;

                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
                txtDepartment.AccessibleDescription = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP;

                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;

                txtPolishCarat1.Visible = true;
                txtPolishCarat2.Visible = true;
                lblPolish.Visible = true;
                DtpTransdate.Visible = true;
                lblTransdate.Visible = true;
                txtProcessTo.Focus();
            }

            if (mStrParentFormType == "BOMBAY")
            {
                BtnGiaExcel.Visible = true;
                BtnMalcaExcel.Visible = true;
                BtnByGrdPrint.Visible = true;
                DtpTransdate.Visible = false;
            }
            else
            {
                BtnGiaExcel.Visible = false;
                BtnMalcaExcel.Visible = false;
                BtnByGrdPrint.Visible = false;
            }
            RbtTransfer_CheckedChanged(null, null);
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
                    FrmSearch.mSearchField = "LEDGERCODE, LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    if (mStrParentFormType == "ROUGH")
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO_NONMFG);
                    }
                    else
                    {
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEETRANSFERTO);
                    }

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

                        txtDepartment.AccessibleDescription = Val.ToString(FrmSearch.mDRow["DEPARTMENTGROUP"]);

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
                Global.MessageError(ex.Message);
            }
        }

        private void txtToProcess_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    DataTable DTabProcess = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mDTab = DTabProcess;
                    //FrmSearch.DTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                    FrmSearch.mColumnsToHide = "PROCESS_ID,PROCESSGROUP";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtProcessTo.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtProcessTo.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        txtRequiredProcess.Text = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);
                        txtRequiredProcess.Tag = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);

                        if ((Val.ToInt32(txtProcessTo.Tag) == 581) && (RbtStaffIssue.Checked = true))
                        {
                            GrdDet.Columns["EXPWEIGHT"].Visible = true;
                        }
                        else
                        {
                            GrdDet.Columns["EXPWEIGHT"].Visible = false;
                        }


                        foreach (DataRow DRow in DTabPacket.Rows)
                        {
                            DRow["TOPROCESS_ID"] = Val.ToString(FrmSearch.mDRow["PROCESS_ID"]);
                            DRow["TOPROCESSNAME"] = Val.ToString(FrmSearch.mDRow["PROCESSNAME"]);

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

                // D: 23-08-2021
                string pStrEntryType = Val.ToString(DRow["ENTRYTYPE"]);
                double pDouExpWeight = Val.Val(DRow["EXPWEIGHT"]);
                // D: 23-08-2021

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

                int IntDiff = Val.ToInt32(IntPcs - IntReadyPcs - IntLostPcs);
                double DouDiff = Math.Round(Val.Val(DouCarat - DouReadyCarat - DouLostCarat - DouLossCarat - DouMixingLessPlus), 3);

                if (DouDiff != 0)
                {
                    Global.MessageError("Carat Mismatched At Row : " + Int.ToString() + " PacketNo : " + DRow["BARCODE"].ToString().Replace("\n", " "));
                    txtRequiredProcess.Focus();
                    return false;
                }

                //// D: 23-08-2021
                //if (Val.ToInt32(txtProcessTo.Tag) == 581 && (RbtTransfer.Checked == true || RbtStaffIssue.Checked == true) && pDouExpWeight == 0)// Process Name : 4p
                //{
                //    Global.Message("ExpWeight is Zero, You Can't Issue/Trransfer This Packet:" + " " + DRow["PACKETNO"].ToString());
                //    txtRequiredProcess.Focus();
                //    return false;
                //}
                ////D: 23-08-2021

                // D: 02/09/2021
                if (DouReadyCarat == 0 && (RbtTransfer.Checked == true || RbtStaffReturn.Checked == true))
                {
                    Global.Message("Ready Carat is Zero, You Can't Issue/Trransfer This Packet:" + " " + DRow["PACKETNO"].ToString());
                    txtRequiredProcess.Focus();
                    return false;
                }
                // D: 02/09/2021

                //// D: 18-09-2021
                //if (pDouExpWeight != 0 && (RbtStaffIssue.Checked == true || RbtTransfer.Checked == true) && (Val.ToInt32(txtProcessTo.Tag) == 465 || Val.ToInt32(txtProcessTo.Tag) == 2763)) //LASER SAWING RE-CUT,LASER SAWING
                //{
                //    Global.Message("Exp Carat is more then Zero, You Can't Issue/Trransfer This Packet:" + " " + DRow["PACKETNO"].ToString());
                //    txtRequiredProcess.Focus();
                //    return false;
                //}
                //// D: 18-09-2021
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

                #region STAFF ISSUE CODE
                //if (mFormType == FORMTYPE.STAFFISSUE)
                //{
                //    DataTable DtPkt = new DataTable();

                //    DtPkt.Columns.Add(new DataColumn("OLDTRN_ID", typeof(Int64)));
                //    DtPkt.Columns.Add(new DataColumn("KAPAN_ID", typeof(Int64)));
                //    DtPkt.Columns.Add(new DataColumn("KAPANNAME", typeof(string)));
                //    DtPkt.Columns.Add(new DataColumn("PACKET_ID", typeof(Int64)));
                //    DtPkt.Columns.Add(new DataColumn("PACKETNO", typeof(Int32)));
                //    DtPkt.Columns.Add(new DataColumn("TAG", typeof(string)));
                //    DtPkt.Columns.Add(new DataColumn("MAINPACKETNO", typeof(Int32)));
                //    DtPkt.Columns.Add(new DataColumn("MTAG", typeof(string)));

                //    string str = Regex.Match("1A", @"\d+").Value;

                //    foreach (DataRow DR in DTabPacket.Rows)
                //    {
                //        DataRow DRNew = DtPkt.NewRow();

                //        DRNew["OLDTRN_ID"] = DR["OLDTRN_ID"];
                //        DRNew["KAPAN_ID"] = DR["KAPAN_ID"];
                //        DRNew["KAPANNAME"] = DR["KAPANNAME"];
                //        DRNew["PACKET_ID"] = DR["PACKET_ID"];
                //        DRNew["PACKETNO"] = DR["PACKETNO"];
                //        DRNew["TAG"] = DR["TAG"];
                //        DRNew["MAINPACKETNO"] = Regex.Match(Val.ToString(DR["MAINPACKETTAG"]), @"\d+").Value;
                //        DRNew["MTAG"] = Val.ToString(DR["MAINPACKETTAG"]).Replace(Regex.Match(Val.ToString(DR["MAINPACKETTAG"]), @"\d+").Value, string.Empty);

                //        DtPkt.Rows.Add(DRNew);
                //    }

                //    DataTable Dt = ObjTrn.CheckStaffIssueValidationLockAmountSettingWise(DtPkt, Val.ToInt32(txtProcessTo.Tag), Val.ToInt64(txtTransferTo.Tag));


                //    if (Dt.Rows.Count > 0)
                //    {
                //        FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                //        // FrmPopupGrid.DTab = DtData;                   
                //        FrmPopupGrid.CountedColumn = "PACKETNO";
                //        FrmPopupGrid.SummrisedColumn = "AMOUNT";
                //        FrmPopupGrid.ColumnsToHide = "PACKET_ID";
                //        FrmPopupGrid.MainGrid.DataSource = Dt;
                //        FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                //        FrmPopupGrid.Text = "List Of Packets Where Employee Lock Amount Setting Is Out Of Range.";
                //        FrmPopupGrid.ISPostBack = true;
                //        this.Cursor = Cursors.Default;

                //        FrmPopupGrid.Width = 1000;
                //        FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                //        //FrmPopupGrid.Size = this.Size;

                //        FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "Packet No";
                //        FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";
                //        FrmPopupGrid.GrdDet.Columns["AMOUNT"].Caption = "Amount";
                //        FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Caption = "Prd. Type";
                //        FrmPopupGrid.GrdDet.Columns["MARKERCODE"].Caption = "Code";

                //        FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Width = 200;
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 100;
                //        //FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                //        FrmPopupGrid.ShowDialog();
                //        FrmPopupGrid.Hide();
                //        FrmPopupGrid.Dispose();
                //        FrmPopupGrid = null;

                //        var idsNotInB = DTabPacket.AsEnumerable().Select(r => r.Field<Guid>("PACKET_ID"))
                //        .Except(Dt.AsEnumerable().Select(r => r.Field<Guid>("PACKET_ID")));

                //        if (idsNotInB.Count() > 0)
                //        {
                //            DataTable TableC = (from row in DTabPacket.AsEnumerable()
                //                                join id in idsNotInB
                //                                on row.Field<Guid>("PACKET_ID") equals id
                //                                select row).CopyToDataTable();

                //            if (Global.Confirm("Some Packets Amount Is Out Of Rang Do You Still Want To Continue...?") == System.Windows.Forms.DialogResult.No)
                //            {
                //                return;
                //            }
                //            DTabPacket = TableC;
                //        }
                //        else
                //            return;
                //    }
                //}

                //DataTable DtabCheckPacketlist = DTabPacket.Copy();
                //DtabCheckPacketlist.TableName = "CheckPacketList";

                ////#P : 16-07-2020 : Check that Jo fullTop ni Process thi Issue kare 6e packet To ano Tflag padi gyo hovo joiye... to j aa Form mathi issue kari sakse...
                //if (RbtStaffIssue.Checked == true && Val.ToInt32(txtProcessTo.Tag) == 503) //FULL TOP PROCESS
                //{
                //    //DataTable DtabIssuePktList = DTabPacket.Copy();
                //    //DtabIssuePktList.TableName = "IssuePacketList";

                //    string StrIsseuePacketListXML;
                //    using (StringWriter sw = new StringWriter())
                //    {
                //        DtabCheckPacketlist.WriteXml(sw);
                //        StrIsseuePacketListXML = sw.ToString();
                //    }
                //    DataTable DtabFullTopPacketsList = ObjTrn.CheckPacketFullTopValidation(StrIsseuePacketListXML);

                //    if (DtabFullTopPacketsList.Rows.Count > 0)
                //    {
                //        FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                //        // FrmPopupGrid.DTab = DtData;                   
                //        FrmPopupGrid.CountedColumn = "PACKETNO";
                //        FrmPopupGrid.ColumnsToHide = "KAPAN_ID,PACKET_ID";
                //        FrmPopupGrid.MainGrid.DataSource = DtabFullTopPacketsList;
                //        FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                //        FrmPopupGrid.Text = "List Of Packets TFlag Is Not Exists.";
                //        FrmPopupGrid.LblTitle.Text = "List Of Packets TFlag Is Not Exists.";
                //        FrmPopupGrid.ISPostBack = true;
                //        this.Cursor = Cursors.Default;

                //        FrmPopupGrid.Width = 1000;
                //        FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;

                //        FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                //        FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "Packet No";
                //        FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";

                //        FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 100;
                //        //FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                //        FrmPopupGrid.ShowDialog();
                //        FrmPopupGrid.Hide();
                //        FrmPopupGrid.Dispose();
                //        FrmPopupGrid = null;
                //        return;
                //    }
                //}
                ////End : #P : 16-07-2020


                ////#P : 21-08-2020 :  Packets Return Lese.... To Issue Ni Process thi j return hase Or ISRequiredForReturn true vali hase to j return thava dey.. Otherwise Not..
                //if (RbtStaffReturn.Checked == true)
                //{
                //    DataTable DtabIssuePktList = DTabPacket.Copy();
                //    DtabIssuePktList.TableName = "IssuePacketList";

                //    string StrIsseuePacketListXML;
                //    using (StringWriter sw = new StringWriter())
                //    {
                //        DtabCheckPacketlist.WriteXml(sw);
                //        StrIsseuePacketListXML = sw.ToString();
                //    }

                //    DataTable DtabProcessIssuePktList = ObjTrn.CheckPacketProcessReturnValidation(StrIsseuePacketListXML);

                //    if (DtabProcessIssuePktList.Rows.Count > 0)
                //    {
                //        FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                //        // FrmPopupGrid.DTab = DtData;                   
                //        FrmPopupGrid.CountedColumn = "PACKETNO";
                //        FrmPopupGrid.ColumnsToHide = "KAPAN_ID,PACKET_ID";
                //        FrmPopupGrid.MainGrid.DataSource = DtabProcessIssuePktList;
                //        FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                //        FrmPopupGrid.Text = "List Of Packets Return Process Is Not Valid.";
                //        FrmPopupGrid.ISPostBack = true;
                //        FrmPopupGrid.LblTitle.Text = "List Of Packets In Which Return Process Is Not Valid";
                //        this.Cursor = Cursors.Default;


                //        FrmPopupGrid.Width = 1000;
                //        FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;

                //        FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                //        FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "Packet No";
                //        FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";

                //        FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                //        FrmPopupGrid.GrdDet.Columns["PACKETNO"].Width = 100;
                //        //FrmPopupGrid.GrdDet.Columns["REMARK"].Width = 150;
                //        FrmPopupGrid.ShowDialog();
                //        FrmPopupGrid.Hide();
                //        FrmPopupGrid.Dispose();
                //        FrmPopupGrid = null;
                //        return;
                //    }
                //}
                ////End : #P : 21-08-2020
                #endregion

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

                int IntSrNo = 0;
                txtJangedNo.Text = string.Empty;

                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    IntSrNo++;
                    TrnSingleIssueReturnProperty Property = new TrnSingleIssueReturnProperty();
                    Property.TRN_ID = 0;
                    Property.OLDTRN_ID = Val.ToInt64(DRow["OLDTRN_ID"].ToString());
                    Property.KAPAN_ID = Val.ToInt64(DRow["KAPAN_ID"].ToString());

                    Property.KAPANNAME = Val.ToString(DRow["KAPANNAME"].ToString());
                    Property.PACKETNO = Val.ToInt(DRow["PACKETNO"].ToString());
                    Property.TAG = Val.ToString(DRow["TAG"].ToString());

                    Property.PACKET_ID = Val.ToInt64(DRow["PACKET_ID"].ToString());
                    Property.JANGEDNO = Val.ToInt64(txtJangedNo.Text);

                    Property.BARCODE = Val.ToString(DRow["BARCODE"]);

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

                    Property.ISSUEPCS = Val.ToInt32(DRow["ISSUEPCS"]);
                    Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);

                    Property.EXPWEIGHT = Val.Val(DRow["EXPWEIGHT"]);

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

                    Property.TRANSDATE = DtpTransdate.Value.ToString();
                    Property.TRANSTYPE = EntryType;
                    Property.REMARK = Val.ToString(DRow["REMARK"]);
                    Property.AUTOCONFIRM = mBoolAutoConfirm;

                    Property.ISFROMFINALISSUE = false;

                    Property.TEMPMARKER_ID = ChkPrintBarcode.Checked ? Val.ToInt64(txtTransferTo.Tag) : Val.ToInt64(DRow["TEMPMARKER_ID"]);
                    Property.OPTIONTRANSFER = "TRANSFERLOSS";

                    Property = ObjTrn.TransferGoods(Property);
                    txtJangedNo.Text = Property.JANGEDNO.ToString();
                    if (Property.ReturnMessageType == "FAIL")
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError(Property.ReturnMessageDesc);
                        //txtJangedNo.Text = "0";
                        this.Cursor = Cursors.WaitCursor;

                        if (Property.ReturnValue == "-5")
                            break;

                    }
                    if (ChkPrintBarcode.Checked)
                    {
                        //Global.BarcodePrint(Property.KAPANNAME,
                        //       Val.ToString(Property.PACKETNO),
                        //       Val.ToString(Property.TAG),
                        //       Property.TRANSDATE,
                        //       Val.ToString(Property.ISSUECARAT),
                        //       Val.ToString(txtTransferTo.Text));

                        //Global.BarcodePrintTSC(Property.KAPANNAME,
                        //  Val.ToString(Property.PACKETNO),
                        //  Val.ToString(Property.TAG),
                        //  Property.TRANSDATE,
                        //  Val.ToString(Property.ISSUECARAT),
                        //  Val.ToString(txtTransferTo.Text),
                        //  Val.ToString(Property.BARCODE)
                        //  );
                    }
                    Property = null;
                }
                mBoolAutoConfirm = false;

                this.Cursor = Cursors.Default;
                if (Val.Val(txtJangedNo.Text) != 0)
                {

                    if (txtTransferTo.Text.Contains("GIA"))
                    {
                        /* temporary commet JYARE KARVANU THAY TYARE un comment karvi .. but atyaare jarur nathi
                        if (Global.Confirm("Are You Sure You Want To Issue GIA Through API ?") == DialogResult.Yes)
                        {
                            IssueToGIA();
                        }*/
                    }


                    if (mStrParentFormType == "BOMBAY")
                    {
                        Global.Message("Your Goods Successfully Transfer To : " + txtTransferTo.Text + "\n\nYour Slip Number : " + txtJangedNo.Text);

                        BtnSave.Enabled = false;
                    }
                    else
                    {
                        BtnPrint_Click(null, null);
                        DTabPacket.Rows.Clear();
                    }
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
                string pIntJangedNo = "";

                if (txtJangedNo.Text == string.Empty)
                {
                    Global.MessageError("Please Enter Slip No");
                    txtJangedNo.Focus();
                    return;
                }

                var barcode = new Linear();
                barcode.Type = BarcodeType.CODE128;
                barcode.ShowText = false;
                pIntJangedNo = Val.ToString(txtJangedNo.Text);
                barcode.Data = pIntJangedNo;
                string pStrOpe = "";
                if (RtbDetail.Checked == true)
                {
                    pStrOpe = "DETAIL";
                }
                else
                {
                    pStrOpe = "SUMMARY";
                }

                DataTable DTab = ObjTrn.PopupJangedForPrint(mStrParentFormType, Val.ToInt64(txtJangedNo.Text), null, pStrOpe);

                if (DTab.Rows.Count == 0)
                {
                    Global.MessageError("There Is No Data For Print");
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {
                    DRow["Barcode"] = barcode.drawBarcodeAsBytes();
                }

                FrmReportViewer FrmReportViewer = new FrmReportViewer();
                FrmReportViewer.MdiParent = Global.gMainRef;
                if (mStrParentFormType == "ROUGH")
                {
                    if (RtbDetail.Checked == true)
                    {
                        //FrmReportViewer.ShowWithPrint("JangedPrint", DTab);
                        FrmReportViewer.ShowWithPrint("JangedPrintWithDuplicate", DTab);
                    }
                    else
                    {
                        //FrmReportViewer.ShowWithPrint("JangedPrintSummary", DTab);
                        FrmReportViewer.ShowWithPrint("JangedPrintSummaryWithDuplicate", DTab);
                    }
                }
                else
                {
                    //FrmReportViewer.ShowWithPrint("JangedPrintGrd", DTab); //Cmnt And : Add : #P : 21-02-2022
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
        }

        private void RbtTransfer_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtTransfer.Checked == true || RbtStaffIssue.Checked == true)
            {
                GrdDet.Columns["READYPCS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["READYCARAT"].OptionsColumn.AllowEdit = true;

                GrdDet.Columns["LOSTPCS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["LOSTCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["LOSSCARAT"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["MIXINGLESSPLUS"].OptionsColumn.AllowEdit = false;
                GrdDet.Columns["RETURNTYPE"].OptionsColumn.AllowEdit = false;
                txtPolishCarat1.Visible = false;
                txtPolishCarat2.Visible = false;
                lblPolish.Visible = false;
                //txtReadyCarat.Visible = false;
                //lblRdyCrt.Visible = false;
                //RbtBarcode.Checked = true;
                //txtBarcode.Focus();
                if (RbtBarcode.Checked == true)
                    txtBarcode.Focus();
                else if (RbtPacketNo.Checked == true)
                    txtKapanName.Focus();
                else if (RbtPktSerialNo.Checked == true)
                    txtSrNoKapanName.Focus();
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
                txtPolishCarat1.Visible = true;
                txtPolishCarat2.Visible = true;
                lblPolish.Visible = true;
                //txtReadyCarat.Visible = true;
                //lblRdyCrt.Visible = true;
                //RbtBarcode.Checked = true;
                //txtBarcode.Focus();
                if (RbtBarcode.Checked == true)
                    txtBarcode.Focus();
                else if (RbtPacketNo.Checked == true)
                    txtKapanName.Focus();
                else if (RbtPktSerialNo.Checked == true)
                    txtSrNoKapanName.Focus();
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
                    double DouReady = Val.Val(DRow["READYCARAT"]);
                    double DouIssue = Val.Val(DRow["ISSUECARAT"]);
                    double DouLossCarat = Math.Round(DouIssue - DouReady, 3);

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
                    break;
                default:
                    break;
            }
        }

        private void txtJangedNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "JANGEDNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjTrn.PopupJangedForPrint("", 0, Val.SqlDate(DTPTransferDate.Value.ToShortDateString()));
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtJangedNo.Text = Val.ToString(FrmSearch.mDRow["JANGEDNO"]);
                        txtExcRate.Text = Val.ToString(FrmSearch.mDRow["EXCRATE"]);
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

        private void txtTransferTo_KeyDown(object sender, KeyEventArgs e)
        {

        }

        private void BtnGiaExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFormType == FORMTYPE.TRANSFER || mFormType == FORMTYPE.STAFFISSUE)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DTab = ObjTrn.PopupJangedForPrintForLab(Val.ToInt64(txtJangedNo.Text), Val.Val(txtExcRate.Text));
                    if (DTab.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError("There Is No Data For Print");
                        return;
                    }

                    string StrDestination = Application.StartupPath + "\\LABFiles\\" + txtJangedNo.Text + "_GIA.xlsx";
                    string StrSourceFile = Application.StartupPath + "\\Format\\GIAIssueFile.xlsx";

                    if (File.Exists(StrDestination))
                    {
                        File.Delete(StrDestination);
                    }
                    File.Copy(StrSourceFile, StrDestination);

                    FileInfo workBook = new FileInfo(StrDestination);
                    using (ExcelPackage xlPackage = new ExcelPackage(workBook))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                        worksheet.Cells[2, 1, 2, 6].Value = Val.ToString(DTab.Rows[0]["LEDGERNAME"]);
                        worksheet.Cells[2, 1, 2, 6].Merge = true;
                        worksheet.Cells[2, 1, 2, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[3, 1, 6, 6].Value = Val.ToString(DTab.Rows[0]["LEDGERADDRESS"]);
                        worksheet.Cells[3, 1, 6, 6].Merge = true;
                        worksheet.Cells[3, 1, 6, 6].Style.WrapText = true;
                        worksheet.Cells[3, 1, 6, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[2, 8, 2, 10].Value = "Client Name : " + Val.ToString(DTab.Rows[0]["CLIENTNAME"]);
                        worksheet.Cells[2, 8, 2, 10].Merge = true;
                        worksheet.Cells[2, 8, 2, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[3, 8, 3, 10].Value = "Client ID : " + Val.ToString(DTab.Rows[0]["CLIENT_ID"]);
                        worksheet.Cells[3, 8, 3, 10].Merge = true;
                        worksheet.Cells[3, 8, 3, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[2, 12, 2, 16].Value = "Memo No : " + Val.ToString(DTab.Rows[0]["JANGEDNO"]);
                        worksheet.Cells[2, 12, 2, 16].Merge = true;
                        worksheet.Cells[2, 12, 2, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[4, 12, 4, 16].Value = "Memo Date : " + Val.ToString(DTab.Rows[0]["TRANSDATE"]);
                        worksheet.Cells[4, 12, 4, 16].Merge = true;
                        worksheet.Cells[4, 12, 4, 16].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        for (int IntI = 0; IntI < DTab.Rows.Count; IntI++)
                        {
                            worksheet.Cells[IntI + 8, 1].Value = Val.ToString(DTab.Rows[IntI]["CONTROLNO"]);
                            worksheet.Cells[IntI + 8, 2].Value = Val.ToString(DTab.Rows[IntI]["CLIENT_ID"]);
                            worksheet.Cells[IntI + 8, 3].Value = Val.ToString(DTab.Rows[IntI]["LABCONTROLNUMBER"]);
                            worksheet.Cells[IntI + 8, 4].Value = Val.ToString(DTab.Rows[IntI]["REMARK"]);
                            worksheet.Cells[IntI + 8, 5].Value = Val.ToString(DTab.Rows[IntI]["SHAPE"]);
                            worksheet.Cells[IntI + 8, 6].Value = Val.ToString(DTab.Rows[IntI]["CARAT"]);
                            worksheet.Cells[IntI + 8, 7].Value = Val.ToString(DTab.Rows[IntI]["AMOUNT"]);
                            worksheet.Cells[IntI + 8, 8].Value = "";
                            worksheet.Cells[IntI + 8, 9].Value = "";
                            worksheet.Cells[IntI + 8, 10].Value = "";
                            worksheet.Cells[IntI + 8, 11].Value = Val.ToString(DTab.Rows[IntI]["COLOR"]);
                            worksheet.Cells[IntI + 8, 12].Value = Val.ToString(DTab.Rows[IntI]["CLARITY"]);
                            worksheet.Cells[IntI + 8, 13].Value = Val.ToString(DTab.Rows[IntI]["POL"]);
                            worksheet.Cells[IntI + 8, 14].Value = Val.ToString(DTab.Rows[IntI]["SYM"]);
                            worksheet.Cells[IntI + 8, 15].Value = Val.ToString(DTab.Rows[IntI]["CUT"]);
                            worksheet.Cells[IntI + 8, 16].Value = Val.ToString(DTab.Rows[IntI]["FL"]);
                            worksheet.Cells[IntI + 8, 19].Value = Val.ToString(DTab.Rows[IntI]["LENGTH"]);
                            worksheet.Cells[IntI + 8, 20].Value = Val.ToString(DTab.Rows[IntI]["WIDTH"]);
                            worksheet.Cells[IntI + 8, 21].Value = Val.ToString(DTab.Rows[IntI]["DEPTH"]);

                            worksheet.Cells[IntI + 8, 38].Value = Val.ToString(DTab.Rows[IntI]["HPHT"]);


                        }

                        worksheet.Cells[8, 1, DTab.Rows.Count + 8, 21].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 1, DTab.Rows.Count + 8, 21].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 1, DTab.Rows.Count + 8, 21].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 1, DTab.Rows.Count + 8, 21].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        worksheet.Cells[8, 38, DTab.Rows.Count + 8, 38].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 38, DTab.Rows.Count + 8, 38].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 38, DTab.Rows.Count + 8, 38].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[8, 38, DTab.Rows.Count + 8, 38].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        xlPackage.Save();
                    }
                    this.Cursor = Cursors.Default;
                    System.Diagnostics.Process.Start(StrDestination, "CMD");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }




        }

        private void BtnMalcaExcel_Click(object sender, EventArgs e)
        {
            try
            {
                if (mFormType == FORMTYPE.TRANSFER || mFormType == FORMTYPE.STAFFISSUE)
                {
                    this.Cursor = Cursors.WaitCursor;
                    DataTable DTab = ObjTrn.PopupJangedForPrintForLab(Val.ToInt64(txtJangedNo.Text), Val.Val(txtExcRate.Text));
                    if (DTab.Rows.Count == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.MessageError("There Is No Data For Print");
                        return;
                    }

                    string StrDestination = Application.StartupPath + "\\LABFiles\\" + txtJangedNo.Text + "_Malca.xlsm";
                    string StrSourceFile = Application.StartupPath + "\\Format\\MalcaPackingList.xlsm";

                    if (File.Exists(StrDestination))
                    {
                        File.Delete(StrDestination);
                    }
                    File.Copy(StrSourceFile, StrDestination);

                    FileInfo workBook = new FileInfo(StrDestination);
                    using (ExcelPackage xlPackage = new ExcelPackage(workBook))
                    {
                        ExcelWorksheet worksheet = xlPackage.Workbook.Worksheets[1];

                        worksheet.Cells[4, 1, 4, 6].Value = Val.ToString(DTab.Rows[0]["LEDGERNAME"]);
                        worksheet.Cells[4, 1, 4, 6].Merge = true;
                        worksheet.Cells[4, 1, 4, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[5, 1, 8, 6].Value = Val.ToString(DTab.Rows[0]["LEDGERADDRESS"]);
                        worksheet.Cells[5, 1, 8, 6].Merge = true;
                        worksheet.Cells[5, 1, 8, 6].Style.WrapText = true;
                        worksheet.Cells[5, 1, 8, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[9, 1, 9, 6].Value = "GIA GSTN : " + Val.ToString(DTab.Rows[0]["LEDGERGSTNO"]);
                        worksheet.Cells[9, 1, 9, 6].Merge = true;
                        worksheet.Cells[9, 1, 9, 6].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[3, 9, 3, 13].Value = "JOB NO : ";
                        worksheet.Cells[3, 9, 3, 13].Merge = true;
                        worksheet.Cells[3, 9, 3, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;


                        worksheet.Cells[4, 9, 4, 13].Value = "Dated : " + Val.ToString(DTab.Rows[0]["TRANSDATE"]);
                        worksheet.Cells[4, 9, 4, 13].Merge = true;
                        worksheet.Cells[4, 9, 4, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[5, 9, 5, 13].Value = "Client ID : " + Val.ToString(DTab.Rows[0]["CLIENT_ID"]);
                        worksheet.Cells[5, 9, 5, 13].Merge = true;
                        worksheet.Cells[5, 9, 5, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[6, 9, 6, 13].Value = "Client GSTN : " + Val.ToString(DTab.Rows[0]["CLIENTGST_NO"]);
                        worksheet.Cells[6, 9, 6, 13].Merge = true;
                        worksheet.Cells[6, 9, 6, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[7, 9, 7, 13].Value = "Memo No : " + Val.ToString(DTab.Rows[0]["JANGEDNO"]);
                        worksheet.Cells[7, 9, 7, 13].Merge = true;
                        worksheet.Cells[7, 9, 7, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        //worksheet.Cells[8, 9, 8, 13].Value = "Dollar Rate : " + txtExcRate.Text;
                        //worksheet.Cells[8, 9, 8, 13].Merge = true;
                        //worksheet.Cells[8, 9, 8, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;

                        worksheet.Cells[8, 9, 8, 9].Value = "Dollar Rate : ";
                        worksheet.Cells[8, 9, 8, 9].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                        worksheet.Cells[8, 10, 8, 10].Value = Val.Val(txtExcRate.Text);
                        worksheet.Cells[8, 10, 8, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;


                        double DouCarat = 0;
                        double DouAmountDollar = 0;
                        double DouAmountRs = 0;
                        string StrRuppesAmtInWords = "";

                        int StartRow = 0, EndRow = 0, CaratNo = 0, DollarAmountNo = 0, RsAmountNo = 0, PricePerCaratNo = 0;
                        string ExcRateNo = "J8";
                        DataColumnCollection columns = DTab.Columns;
                        if (columns.Contains("CARAT"))
                            CaratNo = 5;
                        if (columns.Contains("AMOUNT"))
                            DollarAmountNo = 11;

                        PricePerCaratNo = DollarAmountNo + 1;
                        RsAmountNo = DollarAmountNo + 2;

                        StartRow = 15;
                        EndRow = (DTab.Rows.Count + 15);

                        string S;
                        string E;

                        for (int IntI = 0; IntI < DTab.Rows.Count; IntI++)
                        {
                            worksheet.Cells[IntI + 15, 1].Value = (IntI + 1).ToString();
                            worksheet.Cells[IntI + 15, 2].Value = Val.ToString(DTab.Rows[IntI]["CONTROLNO"]);
                            worksheet.Cells[IntI + 15, 3].Value = Val.ToString(DTab.Rows[IntI]["SHAPE"]);
                            worksheet.Cells[IntI + 15, 4].Value = Val.ToString(DTab.Rows[IntI]["REMARK"]);
                            worksheet.Cells[IntI + 15, 5].Value = Val.Val(DTab.Rows[IntI]["CARAT"]);
                            worksheet.Cells[IntI + 15, 6].Value = Val.ToString(DTab.Rows[IntI]["LENGTH"]);
                            worksheet.Cells[IntI + 15, 7].Value = Val.ToString(DTab.Rows[IntI]["WIDTH"]);
                            worksheet.Cells[IntI + 15, 8].Value = Val.ToString(DTab.Rows[IntI]["DEPTH"]);
                            worksheet.Cells[IntI + 15, 9].Value = Val.ToString(DTab.Rows[IntI]["COLOR"]);
                            worksheet.Cells[IntI + 15, 10].Value = Val.ToString(DTab.Rows[IntI]["CLARITY"]);
                            worksheet.Cells[IntI + 15, 11].Value = Val.Val(DTab.Rows[IntI]["AMOUNT"]); //Add : Pinali : 13-08-2019
                            //worksheet.Cells[IntI + 15, 12].Value = Val.Val(txtExcRate.Text) * Val.Val(DTab.Rows[IntI]["PRICEPERCARAT"]);
                            //worksheet.Cells[IntI + 15, 13].Value = Val.Val(txtExcRate.Text) * Val.Val(DTab.Rows[IntI]["AMOUNT"]);

                            S = Global.ColumnIndexToColumnLetter(CaratNo) + (IntI + 15);
                            E = Global.ColumnIndexToColumnLetter(DollarAmountNo) + (IntI + 15);
                            string P = Global.ColumnIndexToColumnLetter(PricePerCaratNo) + (IntI + 15);
                            worksheet.Cells[IntI + 15, 12].Formula = "Round((" + E + "*" + ExcRateNo + ") / " + S + ",2)"; //PricePerCarat
                            worksheet.Cells[IntI + 15, 13].Formula = "Round((" + P + "*" + S + "),0)";  //AmountRS

                            DouCarat = DouCarat + (Val.Val(DTab.Rows[IntI]["CARAT"]));
                            DouAmountRs = DouAmountRs + (Val.Val(DTab.Rows[IntI]["AMOUNT"]) * Val.Val(txtExcRate.Text));
                            DouAmountDollar = DouAmountDollar + Val.Val(DTab.Rows[IntI]["AMOUNT"]);
                        }

                        StrRuppesAmtInWords = Global.ConvertNumbertoWords(Math.Round(Val.Val(DouAmountRs), 0), true);

                        worksheet.Cells[DTab.Rows.Count + 16, 1, DTab.Rows.Count + 16, 4].Value = "Total Carat";
                        worksheet.Cells[DTab.Rows.Count + 16, 1, DTab.Rows.Count + 16, 4].Merge = true;
                        worksheet.Cells[DTab.Rows.Count + 16, 1, DTab.Rows.Count + 16, 4].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[DTab.Rows.Count + 16, 1, DTab.Rows.Count + 16, 4].Style.Font.Bold = true;

                        //Total Carat
                        S = Global.ColumnIndexToColumnLetter(CaratNo) + StartRow;
                        E = Global.ColumnIndexToColumnLetter(CaratNo) + EndRow;
                        worksheet.Cells[DTab.Rows.Count + 16, 5, DTab.Rows.Count + 16, 5].Formula = "SUM(" + S + ":" + E + ")";

                        //worksheet.Cells[DTab.Rows.Count + 16, 5, DTab.Rows.Count + 16, 5].Value = DouCarat;
                        worksheet.Cells[DTab.Rows.Count + 16, 5, DTab.Rows.Count + 16, 5].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        worksheet.Cells[DTab.Rows.Count + 16, 5, DTab.Rows.Count + 16, 5].Style.Font.Bold = true;

                        //worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 11].Value = "Total Amount In Rs.";
                        //worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 11].Merge = true;
                        //worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 11].Style.Font.Bold = true;

                        worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 10].Value = "Total Amount In Dollar.";
                        worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 10].Merge = true;
                        worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 10].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[DTab.Rows.Count + 16, 8, DTab.Rows.Count + 16, 10].Style.Font.Bold = true;

                        //Total Dollar Amount
                        S = Global.ColumnIndexToColumnLetter(DollarAmountNo) + StartRow;
                        E = Global.ColumnIndexToColumnLetter(DollarAmountNo) + EndRow;
                        worksheet.Cells[DTab.Rows.Count + 16, 11, DTab.Rows.Count + 16, 11].Formula = "SUM(" + S + ":" + E + ")";
                        //worksheet.Cells[DTab.Rows.Count + 16, 11, DTab.Rows.Count + 16, 11].Value = DouAmountDollar;
                        worksheet.Cells[DTab.Rows.Count + 16, 11, DTab.Rows.Count + 16, 11].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        worksheet.Cells[DTab.Rows.Count + 16, 11, DTab.Rows.Count + 16, 11].Style.Font.Bold = true;

                        worksheet.Cells[DTab.Rows.Count + 16, 12, DTab.Rows.Count + 16, 12].Value = "Total Amount In RS.";
                        worksheet.Cells[DTab.Rows.Count + 16, 12, DTab.Rows.Count + 16, 12].Merge = true;
                        worksheet.Cells[DTab.Rows.Count + 16, 12, DTab.Rows.Count + 16, 12].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[DTab.Rows.Count + 16, 12, DTab.Rows.Count + 16, 12].Style.Font.Bold = true;

                        //Total RS Amount
                        S = Global.ColumnIndexToColumnLetter(RsAmountNo) + StartRow;
                        E = Global.ColumnIndexToColumnLetter(RsAmountNo) + EndRow;
                        worksheet.Cells[DTab.Rows.Count + 16, 13, DTab.Rows.Count + 16, 13].Formula = "SUM(" + S + ":" + E + ")";
                        //worksheet.Cells[DTab.Rows.Count + 16, 13, DTab.Rows.Count + 16, 13].Value = DouAmountRs;
                        worksheet.Cells[DTab.Rows.Count + 16, 13, DTab.Rows.Count + 16, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Right;
                        worksheet.Cells[DTab.Rows.Count + 16, 13, DTab.Rows.Count + 16, 13].Style.Font.Bold = true;

                        //worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Value = StrRuppesAmtInWords.ToUpper();
                        //worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Merge = true;
                        //worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        //worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Style.Font.Bold = true;

                        S = Global.ColumnIndexToColumnLetter(RsAmountNo) + (Val.ToInt(DTab.Rows.Count) + 16);
                        worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Formula = "SpellIndian(" + S + ")";
                        worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Merge = true;
                        worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Center;
                        worksheet.Cells[DTab.Rows.Count + 17, 1, DTab.Rows.Count + 17, 13].Style.Font.Bold = true;


                        worksheet.Cells[15, 1, DTab.Rows.Count + 16, 13].Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[15, 1, DTab.Rows.Count + 16, 13].Style.Border.Left.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[15, 1, DTab.Rows.Count + 16, 13].Style.Border.Right.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                        worksheet.Cells[15, 1, DTab.Rows.Count + 16, 13].Style.Border.Top.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;

                        xlPackage.Save();
                    }
                    this.Cursor = Cursors.Default;
                    System.Diagnostics.Process.Start(StrDestination, "CMD");
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message);
            }
        }

        private void BtnByGrdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                if (DTabPacket.Rows.Count < 0)
                    return;


                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }


                foreach (DataRow DrPkt in DTabPacket.Rows)
                {
                    DataRow DrPrint = new BOTRN_KapanCreate().GetBarcodePrintForLabIssueReturn(Val.ToString(DrPkt["KAPANNAME"]), Val.ToInt32(DrPkt["PACKETNO"]), Val.ToString(DrPkt["TAG"]));

                    if (DrPrint != null && Val.Val(DrPrint["BYGRDCARAT"]) != 0)
                    {
                        Global.BarcodeBombayGrdPrint(Val.ToString(DrPrint["KAPANNAME"]),
                            Val.ToString(DrPrint["PACKETNO"]),
                            Val.ToString(DrPrint["TAG"]),
                            Val.ToString(DateTime.Parse(DrPrint["BYGRDDATE"].ToString()).ToString("dd-MM-yy")),
                            Val.ToString(DrPrint["BYGRDCARAT"]),
                            "", // Mark Code
                            Val.ToString(DrPrint["BYSHAPENAME"]),
                            Val.ToString(DrPrint["BYCOLORNAME"]),
                            Val.ToString(DrPrint["BYCLARITYNAME"]),
                            Val.ToString(DrPrint["BYCUTNAME"]),
                            Val.ToString(DrPrint["BYPOLNAME"]),
                            Val.ToString(DrPrint["BYSYMNAME"]),
                            Val.ToString(DrPrint["BYFLNAME"]),
                            Val.ToString(DrPrint["DIAMIN"]),
                            Val.ToString(DrPrint["DIAMAX"]),
                            Val.ToString(DrPrint["HEIGHT"]),
                            "",
                            Val.ToString(DrPrint["HELIUMRATIO"]),
                            Val.ToString(DrPrint["HELIUMTOTALDEPTH"]),
                            Val.ToString(DrPrint["HELIUMTABLEPC"]));
                    }
                    else
                    {
                        Global.Message("No Data Found For Print.");
                        return;
                    }
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void txtPolishCarat1_Validated(object sender, EventArgs e)
        {
            try
            {
                double pDouCarat = 0;
                double pDouReadyCarat = 0;
                pDouCarat = Val.Val(DTabPacket.Compute("SUM(ISSUECARAT)", string.Empty));
                foreach (DataRow DRow in DTabPacket.Rows)
                {
                    txtPolishCarat2.Text = Val.ToString(Math.Round((Val.Val(txtPolishCarat1.Text) / pDouCarat), 3));
                    pDouReadyCarat = Val.Val(txtPolishCarat2.Text) * Val.Val(DRow["ISSUECARAT"]);
                    DRow["READYCARAT"] = Math.Round((pDouReadyCarat), 3);
                    DRow["LOSSCARAT"] = Math.Round((Val.Val(DRow["ISSUECARAT"]) - Val.Val(DRow["READYCARAT"])), 3);
                }

            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message.ToString());
            }
        }

        private void PanelHeader_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtTag_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (IsReadyCaratFocus == false)
                {
                    mStrStockType = ""; ReadyCarat = 0; PacketID = 0;
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
                    if (RbtDeptStock.Checked == true)
                    {
                        mStrStockType = RbtDeptStock.Tag.ToString();
                    }
                    else if (RbtMYStock.Checked == true)
                    {
                        mStrStockType = RbtMYStock.Tag.ToString();
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
                        DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", mStrStockType, txtKapanName.Text, Val.ToInt(txtPacketNo.Text), txtTag.Text, "", 0, "", Val.ToInt(txtPacketNo.Text), 0);
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
                            IEnumerable<DataRow> rowsNew = DTabPacket.Rows.Cast<DataRow>();

                            if (Val.ISDate(DRow["CONFDATE"]) == false && RbtTransfer.Checked == true || Val.ISDate(DRow["CONFDATE"]) == false && RbtStaffIssue.Checked == true)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                                txtKapanName.Text = string.Empty;
                                txtPacketNo.Text = string.Empty;
                                txtTag.Text = string.Empty;
                                txtKapanName.Focus();
                                return;
                            }

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


                            if (RbtStaffReturn.Checked == true)
                            {
                                txtProcessTo.Text = Val.ToString(DRow["TOPROCESSNAME"]);
                                txtProcessTo.Tag = Val.ToString(DRow["TOPROCESS_ID"]);
                                txtRequiredProcess.Text = Val.ToString(DRow["NEXTPROCESSNAME"]);
                                txtRequiredProcess.Tag = Val.ToString(DRow["NEXTPROCESS_ID"]);
                                GrdDet.Columns["EXPWEIGHT"].Visible = false;

                                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;
                                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;
                                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
                                txtDepartment.AccessibleDescription = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP;
                                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;

                                DtpTransdate.Visible = true;
                                lblTransdate.Visible = true;
                            }

                            DataRow DRNew = DTabPacket.NewRow();

                            DRNew["OLDTRN_ID"] = DRow["TRN_ID"];
                            DRNew["PACKET_ID"] = DRow["PACKET_ID"];
                            DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                            DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                            DRNew["PACKETNO"] = DRow["PACKETNO"];
                            DRNew["TAG"] = DRow["TAG"];
                            DRNew["MAINPACKETTAG"] = DRow["MAINPACKETTAG"];
                            DRNew["BARCODE"] = DRow["BARCODE"];
                            DRNew["RFIDTAG"] = DRow["RFIDTAG"];
                            DRNew["ISSUEPCS"] = DRow["BALANCEPCS"];
                            DRNew["ISSUECARAT"] = DRow["BALANCECARAT"];
                            DRNew["READYPCS"] = DRow["BALANCEPCS"];
                            DRNew["READYCARAT"] = DRow["BALANCECARAT"];
                            DRNew["LOSTPCS"] = 0;
                            DRNew["LOSTCARAT"] = 0.00;
                            DRNew["LOSSCARAT"] = 0.00;
                            DRNew["MIXINGLESSPLUS"] = 0.00;
                            DRNew["FROMEMPLOYEE_ID"] = DRow["TOEMPLOYEE_ID"];
                            DRNew["FROMEMPLOYEENAME"] = DRow["TOEMPLOYEENAME"];
                            DRNew["FROMMANAGER_ID"] = DRow["TOMANAGER_ID"];
                            DRNew["FROMMANAGERNAME"] = DRow["TOMANAGERNAME"];
                            DRNew["FROMDEPARTMENT_ID"] = DRow["TODEPARTMENT_ID"];
                            DRNew["FROMDEPARTMENTNAME"] = DRow["TODEPARTMENTNAME"];
                            DRNew["FROMPROCESS_ID"] = DRow["TOPROCESS_ID"];
                            DRNew["FROMPROCESSNAME"] = DRow["TOPROCESSNAME"];
                            DRNew["TOPROCESS_ID"] = Val.ToInt(txtProcessTo.Tag);
                            DRNew["TOPROCESSNAME"] = txtProcessTo.Text;
                            DRNew["NEXTPROCESS_ID"] = Val.ToInt(txtRequiredProcess.Tag);
                            DRNew["NEXTPROCESSNAME"] = txtRequiredProcess.Text;
                            DRNew["RETURNTYPE"] = "DONE";
                            DRNew["REMARK"] = "";
                            DRNew["GIASHAPECODE"] = DRow["GIASHAPECODE"];
                            DRNew["GIACLARITYCODE"] = DRow["GIACLARITYCODE"];
                            DRNew["GIACOLORCODE"] = DRow["GIACOLORCODE"];
                            DRNew["GIACUTCODE"] = DRow["GIACUTCODE"];
                            DRNew["GIAPOLCODE"] = DRow["GIAPOLCODE"];
                            DRNew["GIASYMCODE"] = DRow["GIASYMCODE"];
                            DRNew["GIAFLCODE"] = DRow["GIAFLCODE"];
                            DRNew["GIAREPORTNO"] = DRow["GIAREPORTNO"];
                            DRNew["GIACONTROLNO"] = DRow["GIACONTROLNO"];
                            DRNew["GIAAMOUNT"] = DRow["GIAAMOUNT"];
                            DRNew["EXPWEIGHT"] = DRow["EXPWEIGHT"];
                            DRNew["ENTRYTYPE"] = DRow["ENTRYTYPE"];
                            ReadyCarat = Val.ToDecimal(DRow["BALANCECARAT"]);
                            PacketID = Val.ToInt64(DRow["PACKET_ID"]);

                            DTabPacket.Rows.Add(DRNew);
                            MainGrid.DataSource = DTabPacket;
                            GrdDet.RefreshData();
                        }
                        DRow = null;
                    }

                    GrdDet.RefreshData();
                    GrdDet.BestFitMaxRowCount = 500;
                    GrdDet.BestFitColumns();
                    MainGrid.Refresh();
                    CalculateSummary();
                    txtKapanName.Text = string.Empty;
                    txtPacketNo.Text = string.Empty;
                    txtTag.Text = string.Empty;
                    //RbtPacketNo.Checked = true;
                    //txtKapanName.Focus();
                    if (RbtTransfer.Checked == true)
                    {
                        if (DTabPacket == null)
                        {
                            txtReadyCarat.Text = "0";
                            txtReadyCarat.Tag = "0";
                        }
                        else
                        {
                            txtReadyCarat.Text = ReadyCarat.ToString("0.000");
                            txtReadyCarat.Tag = PacketID;
                        }
                        txtReadyCarat.Focus();
                    }
                    else
                    {
                        txtKapanName.Focus();
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    txtKapanName.Focus();
                    IsReadyCaratFocus = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
                txtKapanName.Focus();
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
            PanelBarcode.Visible = RbtBarcode.Checked;
            PanelJangedNo.Visible = RbtJangedNo.Checked;
            PanelPacketNo.Visible = RbtPacketNo.Checked;
            PanelPktSerialNo.Visible = RbtPktSerialNo.Checked;
        }

        private void txtBarcode_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (IsReadyCaratFocus == false)
                {
                    mStrStockType = ""; ReadyCarat = 0; PacketID = 0;
                    if (txtBarcode.Text.Trim().Length == 0)
                    {
                        return;
                    }
                    if (RbtDeptStock.Checked == true)
                    {
                        mStrStockType = RbtDeptStock.Tag.ToString();
                    }
                    else if (RbtMYStock.Checked == true)
                    {
                        mStrStockType = RbtMYStock.Tag.ToString();
                    }
                    this.Cursor = Cursors.WaitCursor;
                    bool ISFind = false;
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);
                        if (txtBarcode.Text.Trim() == Val.ToString(DRow["BARCODE"]).Trim())
                        {
                            ISFind = true;
                            GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                            txtBarcode.Text = string.Empty;
                            RbtBarcode.Checked = true;
                            txtBarcode.Focus();
                            CalculateSummary();
                            GrdDet.FocusedRowHandle = 0;
                            break;
                        }
                    }

                    if (ISFind == false)
                    {
                        DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", mStrStockType, "", 0, "", txtBarcode.Text, 0, "", 0, 0);
                        if (DRow == null)
                        {
                            this.Cursor = Cursors.Default;
                            Global.MessageError(txtBarcode.Text + " Packet Not In Stock Kindly Check");
                            txtBarcode.Text = string.Empty;
                            RbtBarcode.Checked = true;
                            txtBarcode.Focus();
                            return;
                        }
                        else
                        {
                            IEnumerable<DataRow> rowsNew = DTabPacket.Rows.Cast<DataRow>();

                            if (Val.ISDate(DRow["CONFDATE"]) == false && RbtTransfer.Checked == true || Val.ISDate(DRow["CONFDATE"]) == false && RbtStaffIssue.Checked == true)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                                txtBarcode.Text = string.Empty;
                                RbtBarcode.Checked = true;
                                txtBarcode.Focus();
                                return;
                            }

                            if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("This Packet Is Already Selected.");
                                txtBarcode.Text = string.Empty;
                                RbtBarcode.Checked = true;
                                txtBarcode.Focus();
                                return;
                            }

                            if (RbtStaffReturn.Checked == true)
                            {
                                txtProcessTo.Text = Val.ToString(DRow["TOPROCESSNAME"]);
                                txtProcessTo.Tag = Val.ToString(DRow["TOPROCESS_ID"]);
                                txtRequiredProcess.Text = Val.ToString(DRow["NEXTPROCESSNAME"]);
                                txtRequiredProcess.Tag = Val.ToString(DRow["NEXTPROCESS_ID"]);
                                GrdDet.Columns["EXPWEIGHT"].Visible = false;

                                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;
                                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;
                                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
                                txtDepartment.AccessibleDescription = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP;
                                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;

                                DtpTransdate.Visible = true;
                                lblTransdate.Visible = true;
                            }


                            DataRow DRNew = DTabPacket.NewRow();

                            DRNew["OLDTRN_ID"] = DRow["TRN_ID"];
                            DRNew["PACKET_ID"] = DRow["PACKET_ID"];
                            DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                            DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                            DRNew["PACKETNO"] = DRow["PACKETNO"];
                            DRNew["TAG"] = DRow["TAG"];
                            DRNew["MAINPACKETTAG"] = DRow["MAINPACKETTAG"];
                            DRNew["BARCODE"] = DRow["BARCODE"];
                            DRNew["RFIDTAG"] = DRow["RFIDTAG"];
                            DRNew["ISSUEPCS"] = DRow["BALANCEPCS"];
                            DRNew["ISSUECARAT"] = DRow["BALANCECARAT"];
                            DRNew["READYPCS"] = DRow["BALANCEPCS"];
                            DRNew["READYCARAT"] = DRow["BALANCECARAT"];
                            DRNew["LOSTPCS"] = 0;
                            DRNew["LOSTCARAT"] = 0.00;
                            DRNew["LOSSCARAT"] = 0.00;
                            DRNew["MIXINGLESSPLUS"] = 0.00;
                            DRNew["FROMEMPLOYEE_ID"] = DRow["TOEMPLOYEE_ID"];
                            DRNew["FROMEMPLOYEENAME"] = DRow["TOEMPLOYEENAME"];
                            DRNew["FROMMANAGER_ID"] = DRow["TOMANAGER_ID"];
                            DRNew["FROMMANAGERNAME"] = DRow["TOMANAGERNAME"];
                            DRNew["FROMDEPARTMENT_ID"] = DRow["TODEPARTMENT_ID"];
                            DRNew["FROMDEPARTMENTNAME"] = DRow["TODEPARTMENTNAME"];
                            DRNew["FROMPROCESS_ID"] = DRow["TOPROCESS_ID"];
                            DRNew["FROMPROCESSNAME"] = DRow["TOPROCESSNAME"];
                            DRNew["TOPROCESS_ID"] = Val.ToInt(txtProcessTo.Tag);
                            DRNew["TOPROCESSNAME"] = txtProcessTo.Text;
                            DRNew["NEXTPROCESS_ID"] = Val.ToInt(txtRequiredProcess.Tag);
                            DRNew["NEXTPROCESSNAME"] = txtRequiredProcess.Text;
                            DRNew["RETURNTYPE"] = "DONE";
                            DRNew["REMARK"] = "";
                            DRNew["GIASHAPECODE"] = DRow["GIASHAPECODE"];
                            DRNew["GIACLARITYCODE"] = DRow["GIACLARITYCODE"];
                            DRNew["GIACOLORCODE"] = DRow["GIACOLORCODE"];
                            DRNew["GIACUTCODE"] = DRow["GIACUTCODE"];
                            DRNew["GIAPOLCODE"] = DRow["GIAPOLCODE"];
                            DRNew["GIASYMCODE"] = DRow["GIASYMCODE"];
                            DRNew["GIAFLCODE"] = DRow["GIAFLCODE"];
                            DRNew["GIAREPORTNO"] = DRow["GIAREPORTNO"];
                            DRNew["GIACONTROLNO"] = DRow["GIACONTROLNO"];
                            DRNew["GIAAMOUNT"] = DRow["GIAAMOUNT"];
                            DRNew["EXPWEIGHT"] = DRow["EXPWEIGHT"];
                            DRNew["ENTRYTYPE"] = DRow["ENTRYTYPE"];
                            ReadyCarat = Val.ToDecimal(DRow["BALANCECARAT"]);
                            PacketID = Val.ToInt64(DRow["PACKET_ID"]);

                            DTabPacket.Rows.Add(DRNew);
                            MainGrid.DataSource = DTabPacket;
                            GrdDet.RefreshData();
                        }
                        DRow = null;
                    }

                    GrdDet.RefreshData();
                    GrdDet.BestFitMaxRowCount = 500;
                    GrdDet.BestFitColumns();
                    MainGrid.Refresh();
                    CalculateSummary();
                    txtBarcode.Text = string.Empty;
                    //RbtBarcode.Checked = true;
                    //txtBarcode.Focus();
                    if (RbtStaffReturn.Checked == true)
                    {
                        if (DTabPacket == null)
                        {
                            txtReadyCarat.Text = "0";
                            txtReadyCarat.Tag = "0";
                        }
                        else
                        {
                            txtReadyCarat.Text = ReadyCarat.ToString("0.000");
                            txtReadyCarat.Tag = PacketID;
                        }
                        txtReadyCarat.Focus();
                    }
                    else
                    {
                        txtBarcode.Focus();
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    txtBarcode.Focus();
                    IsReadyCaratFocus = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
                txtBarcode.Focus();
            }
        }

        private void txtSrNoSerialNo_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (IsReadyCaratFocus == false)
                {
                    mStrStockType = ""; ReadyCarat = 0; PacketID = 0;
                    if (txtSrNoKapanName.Text.Trim().Length == 0)
                    {
                        return;
                    }
                    if (Val.ToInt(txtSrNoSerialNo.Text) == 0)
                    {
                        txtSrNoSerialNo.Focus();
                        return;
                    }
                    if (RbtDeptStock.Checked == true)
                    {
                        mStrStockType = RbtDeptStock.Tag.ToString();
                    }
                    else if (RbtMYStock.Checked == true)
                    {
                        mStrStockType = RbtMYStock.Tag.ToString();
                    }
                    this.Cursor = Cursors.WaitCursor;
                    bool ISFind = false;
                    for (int IntI = 0; IntI < GrdDet.RowCount; IntI++)
                    {
                        DataRow DRow = GrdDet.GetDataRow(IntI);
                        if (txtSrNoKapanName.Text.Trim() == Val.ToString(DRow["KAPANNAME"]).Trim()
                            && txtSrNoSerialNo.Text.Trim() == Val.ToString(DRow["PKTSERIALNO"]).Trim())
                        {
                            ISFind = true;
                            GrdDet.SetRowCellValue(IntI, "COLSELECTCHECKBOX", true);
                            txtSrNoKapanName.Text = string.Empty;
                            txtSrNoSerialNo.Text = string.Empty;
                            RbtPktSerialNo.Checked = true;
                            txtSrNoKapanName.Focus();
                            CalculateSummary();
                            GrdDet.FocusedRowHandle = 0;
                            break;
                        }
                    }

                    if (ISFind == false)
                    {
                        DataRow DRow = ObjKapan.GetDataForSinglePacketLiveStockPacketInfo("ALL", mStrStockType, txtSrNoKapanName.Text, 0, "", "", Val.ToInt32(txtSrNoSerialNo.Text), "", 0, 0);
                        if (DRow == null)
                        {
                            this.Cursor = Cursors.Default;
                            Global.MessageError(txtSrNoKapanName.Text + "-" + txtSrNoSerialNo.Text + " Packet Not In Stock Kindly Check");
                            txtSrNoKapanName.Text = string.Empty;
                            txtSrNoSerialNo.Text = string.Empty;
                            RbtPktSerialNo.Checked = true;
                            txtSrNoKapanName.Focus();
                            return;
                        }
                        else
                        {
                            IEnumerable<DataRow> rowsNew = DTabPacket.Rows.Cast<DataRow>();

                            if (Val.ISDate(DRow["CONFDATE"]) == false && RbtTransfer.Checked == true || Val.ISDate(DRow["CONFDATE"]) == false && RbtStaffIssue.Checked == true)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("First You Confirmed Goods ? PacketNo : " + Val.ToString(DRow["BARCODE"]).Replace("\n", " - "));
                                txtSrNoKapanName.Text = string.Empty;
                                txtSrNoSerialNo.Text = string.Empty;
                                RbtPktSerialNo.Checked = true;
                                txtSrNoKapanName.Focus();
                                return;
                            }

                            if (rowsNew.Where(s => Val.ToString(s["PACKET_ID"]) == Val.ToString(DRow["PACKET_ID"])).Count() > 0)
                            {
                                this.Cursor = Cursors.Default;
                                Global.Message("This Packet Is Already Selected.");
                                txtSrNoKapanName.Text = string.Empty;
                                txtSrNoSerialNo.Text = string.Empty;
                                RbtPktSerialNo.Checked = true;
                                txtSrNoKapanName.Focus();
                                return;
                            }

                            if (RbtStaffReturn.Checked == true)
                            {
                                txtProcessTo.Text = Val.ToString(DRow["TOPROCESSNAME"]);
                                txtProcessTo.Tag = Val.ToString(DRow["TOPROCESS_ID"]);
                                txtRequiredProcess.Text = Val.ToString(DRow["NEXTPROCESSNAME"]);
                                txtRequiredProcess.Tag = Val.ToString(DRow["NEXTPROCESS_ID"]);
                                GrdDet.Columns["EXPWEIGHT"].Visible = false;

                                txtTransferTo.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE;
                                txtTransferTo.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID;
                                mBoolAutoConfirm = BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM;
                                txtManager.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID;
                                txtManager.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE;
                                txtDepartment.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME;
                                txtDepartment.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID;
                                txtDepartment.AccessibleDescription = BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP;
                                txtType.Text = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;
                                txtType.Tag = BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP;

                                DtpTransdate.Visible = true;
                                lblTransdate.Visible = true;
                            }


                            DataRow DRNew = DTabPacket.NewRow();

                            DRNew["OLDTRN_ID"] = DRow["TRN_ID"];
                            DRNew["PACKET_ID"] = DRow["PACKET_ID"];
                            DRNew["KAPAN_ID"] = DRow["KAPAN_ID"];
                            DRNew["KAPANNAME"] = DRow["KAPANNAME"];
                            DRNew["PACKETNO"] = DRow["PACKETNO"];
                            DRNew["TAG"] = DRow["TAG"];
                            DRNew["MAINPACKETTAG"] = DRow["MAINPACKETTAG"];
                            DRNew["BARCODE"] = DRow["BARCODE"];
                            DRNew["RFIDTAG"] = DRow["RFIDTAG"];
                            DRNew["ISSUEPCS"] = DRow["BALANCEPCS"];
                            DRNew["ISSUECARAT"] = DRow["BALANCECARAT"];
                            DRNew["READYPCS"] = DRow["BALANCEPCS"];
                            DRNew["READYCARAT"] = DRow["BALANCECARAT"];
                            DRNew["LOSTPCS"] = 0;
                            DRNew["LOSTCARAT"] = 0.00;
                            DRNew["LOSSCARAT"] = 0.00;
                            DRNew["MIXINGLESSPLUS"] = 0.00;
                            DRNew["FROMEMPLOYEE_ID"] = DRow["TOEMPLOYEE_ID"];
                            DRNew["FROMEMPLOYEENAME"] = DRow["TOEMPLOYEENAME"];
                            DRNew["FROMMANAGER_ID"] = DRow["TOMANAGER_ID"];
                            DRNew["FROMMANAGERNAME"] = DRow["TOMANAGERNAME"];
                            DRNew["FROMDEPARTMENT_ID"] = DRow["TODEPARTMENT_ID"];
                            DRNew["FROMDEPARTMENTNAME"] = DRow["TODEPARTMENTNAME"];
                            DRNew["FROMPROCESS_ID"] = DRow["TOPROCESS_ID"];
                            DRNew["FROMPROCESSNAME"] = DRow["TOPROCESSNAME"];
                            DRNew["TOPROCESS_ID"] = Val.ToInt(txtProcessTo.Tag);
                            DRNew["TOPROCESSNAME"] = txtProcessTo.Text;
                            DRNew["NEXTPROCESS_ID"] = Val.ToInt(txtRequiredProcess.Tag);
                            DRNew["NEXTPROCESSNAME"] = txtRequiredProcess.Text;
                            DRNew["RETURNTYPE"] = "DONE";
                            DRNew["REMARK"] = "";
                            DRNew["GIASHAPECODE"] = DRow["GIASHAPECODE"];
                            DRNew["GIACLARITYCODE"] = DRow["GIACLARITYCODE"];
                            DRNew["GIACOLORCODE"] = DRow["GIACOLORCODE"];
                            DRNew["GIACUTCODE"] = DRow["GIACUTCODE"];
                            DRNew["GIAPOLCODE"] = DRow["GIAPOLCODE"];
                            DRNew["GIASYMCODE"] = DRow["GIASYMCODE"];
                            DRNew["GIAFLCODE"] = DRow["GIAFLCODE"];
                            DRNew["GIAREPORTNO"] = DRow["GIAREPORTNO"];
                            DRNew["GIACONTROLNO"] = DRow["GIACONTROLNO"];
                            DRNew["GIAAMOUNT"] = DRow["GIAAMOUNT"];
                            DRNew["EXPWEIGHT"] = DRow["EXPWEIGHT"];
                            DRNew["ENTRYTYPE"] = DRow["ENTRYTYPE"];
                            DRNew["PKTSERIALNO"] = DRow["PKTSERIALNO"];
                            ReadyCarat = Val.ToDecimal(DRow["BALANCECARAT"]);
                            PacketID = Val.ToInt64(DRow["PACKET_ID"]);

                            DTabPacket.Rows.Add(DRNew);
                            MainGrid.DataSource = DTabPacket;
                            GrdDet.RefreshData();
                        }
                        DRow = null;
                    }

                    GrdDet.RefreshData();
                    GrdDet.BestFitMaxRowCount = 500;
                    GrdDet.BestFitColumns();
                    MainGrid.Refresh();
                    CalculateSummary();
                    txtSrNoKapanName.Text = string.Empty;
                    txtSrNoSerialNo.Text = string.Empty;
                    //RbtPktSerialNo.Checked = true;
                    //txtSrNoKapanName.Focus();
                    if (RbtStaffReturn.Checked == true)
                    {
                        if (DTabPacket == null)
                        {
                            txtReadyCarat.Text = "0";
                            txtReadyCarat.Tag = "0";
                        }
                        else
                        {
                            txtReadyCarat.Text = ReadyCarat.ToString("0.000");
                            txtReadyCarat.Tag = PacketID;
                        }
                        txtReadyCarat.Focus();
                    }
                    else
                    {
                        txtSrNoKapanName.Focus();
                    }
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    txtSrNoKapanName.Focus();
                    IsReadyCaratFocus = false;
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
                txtSrNoKapanName.Focus();
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtKapanName.Text = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtTag.Text = string.Empty;
            txtTransferTo.Text = string.Empty;
            txtTransferTo.Tag = string.Empty;
            txtManager.Text = string.Empty;
            txtManager.Tag = string.Empty;
            txtDepartment.Text = string.Empty;
            txtDepartment.Tag = string.Empty;
            txtProcessTo.Text = string.Empty;
            txtProcessTo.Tag = string.Empty;
            txtRequiredProcess.Text = string.Empty;
            txtRequiredProcess.Tag = string.Empty;
            DTPTransferDate.Value = DateTime.Now;
            txtBarcode.Text = string.Empty;
            txtSrNoKapanName.Text = string.Empty;
            txtSrNoSerialNo.Text = string.Empty;
            txtReadyCarat.Text = string.Empty;
            txtReadyCarat.Tag = string.Empty;
            DTabPacket.Rows.Clear();
            MainGrid.DataSource = DTabPacket;
            GrdDet.RefreshData();
            if (RbtBarcode.Checked == true)
                txtBarcode.Focus();
            else if (RbtPacketNo.Checked == true)
                txtKapanName.Focus();
            else if (RbtPktSerialNo.Checked == true)
                txtSrNoKapanName.Focus();
        }

        private void txtReadyCarat_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        private void txtReadyCarat_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (txtReadyCarat.Text.Trim().Length == 0)
                    {
                        return;
                    }
                    for (int i = 0; i < DTabPacket.Rows.Count; i++)
                    {
                        if (Val.ToInt64(DTabPacket.Rows[i]["PACKET_ID"].ToString()) == Val.ToInt64(txtReadyCarat.Tag))
                        {
                            double DouReady = Val.Val(txtReadyCarat.Text);
                            double DouIssue = Val.Val(DTabPacket.Rows[i]["ISSUECARAT"]);
                            double DouLossCarat = Math.Round(DouIssue - DouReady, 3);
                            if (DouLossCarat < 0)
                            {
                                Global.MessageError("Loss Carat Is Less Then Zero");
                                DTabPacket.Rows[i]["READYCARAT"] = DouIssue;
                                DTabPacket.Rows[i]["LOSSCARAT"] = 0;
                            }
                            else
                            {
                                DTabPacket.Rows[i]["READYCARAT"] = DouReady;
                                DTabPacket.Rows[i]["LOSSCARAT"] = DouLossCarat;
                            }
                        }
                    }
                    if (RbtBarcode.Checked == true)
                    {
                        IsReadyCaratFocus = true;
                        txtBarcode.Focus();
                    }
                    else if (RbtPacketNo.Checked == true)
                    {
                        IsReadyCaratFocus = true;
                        txtTag.Focus();
                    }
                    else if (RbtPktSerialNo.Checked == true)
                    {
                        IsReadyCaratFocus = true;
                        txtSrNoSerialNo.Focus();
                    }
                    txtReadyCarat.Text = string.Empty;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void FrmSingleGoodsTransferLossUpdate_Shown(object sender, EventArgs e)
        {
            try
            {
                txtBarcode.Focus();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
