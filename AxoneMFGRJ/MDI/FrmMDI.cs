using AxoneMFGRJ.Account;
using AxoneMFGRJ.Attendance;
using AxoneMFGRJ.Dashboard;
using AxoneMFGRJ.Grading;
using AxoneMFGRJ.Master;
using AxoneMFGRJ.Masters;
using AxoneMFGRJ.PacketTreeDrawing;
using AxoneMFGRJ.Parcel;
using AxoneMFGRJ.Polish;
using AxoneMFGRJ.Rapaport;
using AxoneMFGRJ.Report;
using AxoneMFGRJ.ReportGrid;
using AxoneMFGRJ.Salary;
using AxoneMFGRJ.Transaction;
using AxoneMFGRJ.Utility;
using AxoneMFGRJ.View;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Deployment.Application;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.MDI
{
    public partial class FrmMDI : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public bool mBoolAdminDashboard = false;
        public bool mBoolMarkerDashboard = false;

        int IntCount = 0;

        public FrmMDI()
        {
            InitializeComponent();
        }

        private void FrmMDI_Load(object sender, EventArgs e)
        {
            PnlClientLogo.BackgroundImage = Properties.Resources.Background;
            PnlClientLogo.BackgroundImageLayout = ImageLayout.Zoom;


            Global.gStrSuvichar = new BOMST_FormPermission().GetMessage();
            if (Global.gStrSuvichar.Trim() == "")
            {
                Global.gStrSuvichar = "!! WELCOM " + Global.gStrCompanyName + " !!";
            }

            if (BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME != "AXONEADMIN")
            {
                CheckMenuPermission();
            }

            this.Text = "Welcome " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME + " [ USERNAME : " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME + "  & IP : " + BusLib.Configuration.BOConfiguration.ComputerIP + " ] [V : " + Global.gStrExeVersion + " ]";
            lblChangeUser.Text = "Change User ( " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME + " )";


            // For Check Db Is _Test then Color Red : 04-09-2019
            System.Data.Common.DbConnectionStringBuilder builder = new System.Data.Common.DbConnectionStringBuilder();
            builder.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;
            string database = builder["Initial Catalog"] as string;
        }

        private void CheckMenuPermission()
        {
            ToolStripDropDownItem ToolSubMenu1;

            DataTable DTab = new BOMST_FormPermission().GetUserAuthenticationGetData(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            foreach (ToolStripMenuItem ToolMainMenu in menuStrip1.Items)
            {

                if (ToolMainMenu.DropDownItems.Count > 0)
                {
                    int mainmenuCount = 0;
                    foreach (object ObjSubMenu1 in ToolMainMenu.DropDownItems)
                    {
                        if (ObjSubMenu1.GetType() == typeof(ToolStripSeparator))
                        {
                            continue;
                        }

                        ToolSubMenu1 = (ToolStripDropDownItem)ObjSubMenu1;

                        if (Val.ToString(ToolSubMenu1.Tag) == "FactoryRunningPossitionReportDepartmentWise")
                        {
                            ToolSubMenu1.Text = "Factory Running Possition (" + Val.ToString(BOConfiguration.gEmployeeProperty.DEPARTMENTNAME) + ")";
                        }

                        if (ToolSubMenu1.DropDownItems.Count > 0)
                        {
                            int sub1menuCount = 0;

                            foreach (ToolStripDropDownItem ObjSubMenu2 in ToolSubMenu1.DropDownItems)
                            {
                                if (ObjSubMenu2.DropDownItems.Count > 0)
                                {
                                    int sub2menuCount = 0;
                                    foreach (ToolStripDropDownItem sub3menu in ObjSubMenu2.DropDownItems)
                                    {
                                        if (ISViewPermission(Val.ToString(sub3menu.Tag), DTab))
                                        {

                                            ToolMainMenu.Visible = true;
                                            ToolSubMenu1.Visible = true;
                                            ObjSubMenu2.Visible = true;
                                            sub3menu.Visible = true;
                                        }
                                        else
                                        {

                                            sub3menu.Visible = false;
                                            sub2menuCount = sub2menuCount + 1;
                                            if (sub2menuCount == ObjSubMenu2.DropDownItems.Count)
                                            {
                                                ObjSubMenu2.Visible = false;
                                                sub1menuCount = sub1menuCount + 1;
                                                if (sub1menuCount == ToolSubMenu1.DropDownItems.Count)
                                                {
                                                    ToolSubMenu1.Visible = false;
                                                    mainmenuCount = mainmenuCount + 1;
                                                    if (mainmenuCount == ToolMainMenu.DropDownItems.Count)
                                                    {
                                                        ToolMainMenu.Visible = false;
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                                else if (ObjSubMenu2.DropDownItems.Count == 0)
                                {

                                    if (ISViewPermission(Val.ToString(ObjSubMenu2.Tag), DTab))
                                    {
                                        ToolMainMenu.Visible = true;
                                        ToolSubMenu1.Visible = true;
                                        ObjSubMenu2.Visible = true;
                                    }
                                    else
                                    {
                                        ObjSubMenu2.Visible = false;
                                        sub1menuCount = sub1menuCount + 1;
                                        if (sub1menuCount == ToolSubMenu1.DropDownItems.Count)
                                        {
                                            ToolSubMenu1.Visible = false;
                                            mainmenuCount = mainmenuCount + 1;
                                            if (mainmenuCount == ToolMainMenu.DropDownItems.Count)
                                            {
                                                ToolMainMenu.Visible = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                        else if (ToolSubMenu1.DropDownItems.Count == 0)
                        {

                            if (ISViewPermission(Val.ToString(ToolSubMenu1.Tag), DTab))
                            {
                                ToolMainMenu.Visible = true;
                                ToolSubMenu1.Visible = true;
                            }
                            else
                            {
                                ToolSubMenu1.Visible = false;
                                mainmenuCount = mainmenuCount + 1;
                                if (mainmenuCount == ToolMainMenu.DropDownItems.Count)
                                {
                                    ToolMainMenu.Visible = false;
                                }
                            }
                        }
                    }
                }
                else if (ToolMainMenu.DropDownItems.Count == 0)
                {

                    if (ISViewPermission(Val.ToString(ToolMainMenu.Tag), DTab))
                    {
                        ToolMainMenu.Visible = true;
                    }
                }
            }

        }
        private bool ISViewPermission(string MenuName, DataTable DTab)
        {
            bool Flag = false;
            try
            {
                DataRow[] DRow = DTab.Select("FormName = '" + MenuName + "'");

                if (DRow.Length != 0)
                {
                    Flag = Val.ToBoolean(DRow[0]["ISVIEW"]);
                    if (MenuName.ToUpper() == "FRMMARKERDASHBOARDFORMARKER")
                    {
                        mBoolMarkerDashboard = Flag;
                    }
                    else if (MenuName.ToUpper() == "FRMMARKERDASHBOARDFORADMIN ")
                    {
                        mBoolAdminDashboard = Flag;
                    }
                }
                else
                {
                    Flag = false;
                }

            }
            catch (Exception ex)
            {
            }
            return Flag;
        }



        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
                if (this.ActiveMdiChild == null && this.MdiChildren.Length == 0)
                {

                    if (Global.Confirm("Are you sure to close the application ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        Application.Exit();
                    }
                }
                else
                {
                    if (this.ActiveMdiChild == null && this.MdiChildren.Length != 0)
                    {
                        if (this.MdiChildren.Length != 0)
                        {
                            foreach (System.Windows.Forms.Form Frm in this.MdiChildren)
                            {
                                if (Frm.Tag + "" != "ExplicitClose")
                                {
                                    Frm.Activate();
                                    Frm.Focus();
                                    Frm.Close();
                                    return;
                                }
                            }
                        }
                    }
                    else
                    {
                        this.ActiveMdiChild.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.ToString());
            }
        }

        private void ledgerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = this;
            FrmLedger.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLedger.ShowForm("LEDGER");
        }

        private void jVEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void MNLedgerReport_Click(object sender, EventArgs e)
        {
            FrmLedgerReport FrmLedgerReport = new FrmLedgerReport();
            FrmLedgerReport.MdiParent = this;
            FrmLedgerReport.ShowForm();
        }

        private void dailyIncomeExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRojMel FrmRojMel = new FrmRojMel();
            FrmRojMel.MdiParent = this;
            FrmRojMel.ShowForm();
        }

        private void BtnStmt_Click(object sender, EventArgs e)
        {
            FrmLedgerReport FrmLedgerReport = new FrmLedgerReport();
            FrmLedgerReport.MdiParent = this;
            FrmLedgerReport.ShowForm();
        }

        private void BtnRojmel_Click(object sender, EventArgs e)
        {
            FrmRojMel FrmRojMel = new FrmRojMel();
            FrmRojMel.MdiParent = this;
            FrmRojMel.ShowForm();
        }

        private void BtnBackup_Click(object sender, EventArgs e)
        {
            //BOConfiguration.BackUp();
            //Global.Message("BACKUP SUCCESSFULLY DONE...............");
        }

        private void onAccountCashPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentEntry FrmPaymentEntry = new FrmPaymentEntry();
            FrmPaymentEntry.MdiParent = this;
            FrmPaymentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPaymentEntry.ShowForm(Account.FrmPaymentEntry.FORMTYPE.P);
        }

        private void onAccountBankPaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentEntry FrmPaymentEntry = new FrmPaymentEntry();
            FrmPaymentEntry.MdiParent = this;
            FrmPaymentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPaymentEntry.ShowForm(Account.FrmPaymentEntry.FORMTYPE.R);

        }

        private void onAccountCashReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentEntry FrmPaymentEntry = new FrmPaymentEntry();
            FrmPaymentEntry.MdiParent = this;
            FrmPaymentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPaymentEntry.ShowForm(Account.FrmPaymentEntry.FORMTYPE.CO);

        }

        private void onAccountBankReceiptToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentEntry FrmPaymentEntry = new FrmPaymentEntry();
            FrmPaymentEntry.MdiParent = this;
            FrmPaymentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPaymentEntry.ShowForm(Account.FrmPaymentEntry.FORMTYPE.P);

        }

        private void onAccountContraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPaymentEntry FrmPaymentEntry = new FrmPaymentEntry();
            FrmPaymentEntry.MdiParent = this;
            FrmPaymentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPaymentEntry.ShowForm(Account.FrmPaymentEntry.FORMTYPE.CO);

        }

        private void companyMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = this;
            FrmLedger.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLedger.ShowForm("COMPANY");

        }

        private void BtnNew_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = this;
            FrmLedger.ShowForm("LEDGER");
        }


        private void FrmMDI_FormClosing(object sender, FormClosingEventArgs e)
        {
            //BOConfiguration.BackUp();

            //#P : 16-10-2019
            try
            {
                this.Cursor = Cursors.WaitCursor;
                int IntRes = new BOTRN_SingleIssueReturn().UpdateLoguteDateTime();
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

            //End : #P : 16-10-2019

        }

        private void homeDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHome FrmHome = new FrmHome();
            FrmHome.MdiParent = this;
            FrmHome.ShowForm();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAboutUs FrmAboutUs = new FrmAboutUs();
            FrmAboutUs.ShowForm();
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var folderBrowserDialog1 = new FolderBrowserDialog();

            // Show the FolderBrowserDialog.
            DialogResult result = folderBrowserDialog1.ShowDialog();
            if (result == DialogResult.OK)
            {
                string folderName = folderBrowserDialog1.SelectedPath;
                this.Cursor = Cursors.WaitCursor;
                string StrBackupPath = BOConfiguration.BackUp(folderName);
                if (StrBackupPath != "")
                {
                    Global.Message("BACKUP SUCCESSFULLY DONE...............\n\nPath : " + StrBackupPath);
                }
                else
                {
                    Global.Message("Error While Taking Backup");
                }
                this.Cursor = Cursors.Default;
            }



        }



        private void yearTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Global.Message("UNDER DEVELOPMENT");
        }

        private void employeeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployeeMasterNew FrmEmployeeMasterNew = new FrmEmployeeMasterNew();
            FrmEmployeeMasterNew.MdiParent = this;
            FrmEmployeeMasterNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmEmployeeMasterNew.ShowForm();
        }

        private void productionEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void shapeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = this;
            FrmParameter.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParameter.ShowForm();
        }

        private void attendanceEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttendanceEntry FrmAttendanceEntry = new FrmAttendanceEntry();
            FrmAttendanceEntry.MdiParent = this;
            FrmAttendanceEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmAttendanceEntry.ShowForm();
        }

        private void salaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryEntry FrmSalaryEntry = new FrmSalaryEntry();
            FrmSalaryEntry.MdiParent = this;
            FrmSalaryEntry.ShowForm();
        }



        private void attendanceRegisterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAttendanceRegister FrmAttendanceRegister = new FrmAttendanceRegister();
            FrmAttendanceRegister.MdiParent = this;
            FrmAttendanceRegister.ShowForm();
        }

        private void kapanLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanDashboard FrmKapanDashboard = new FrmKapanDashboard();
            FrmKapanDashboard.MdiParent = this;
            FrmKapanDashboard.ShowForm();

        }

        private void deleteAllTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //if (Global.Confirm("Are You Sure Want To Delete All Transaction") == System.Windows.Forms.DialogResult.Yes)
            //{
            //    if (Global.Confirm("Are You Sure Want To Delete All Transaction") == System.Windows.Forms.DialogResult.Yes)
            //    {
            //        this.Cursor = Cursors.WaitCursor;
            //        //BOConfiguration.BackUp();
            //        int Intn = new BOTRN_RoughMIS().TruncateAllTable();
            //        this.Cursor = Cursors.Default;

            //        if (Intn != 0)
            //        {

            //            Global.Message("ALL RECORD DELETED");
            //        }
            //    }
            //}

        }

        private void rejectionMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = this;
            FrmParameter.ShowForm("REJECTION");
        }

        private void rejectionLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRejectionView FrmRejectionView = new FrmRejectionView();
            FrmRejectionView.MdiParent = this;
            FrmRejectionView.ShowForm();
        }

        private void asOnDateStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void monthlyFixedExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFixedExpenseEntry FrmFixedExpenseEntry = new FrmFixedExpenseEntry();
            FrmFixedExpenseEntry.MdiParent = this;
            FrmFixedExpenseEntry.ShowForm();

        }

        private void asOnDateStockReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmAsOnDateStockReport FrmAsOnDateStockReport = new FrmAsOnDateStockReport();
            FrmAsOnDateStockReport.MdiParent = this;
            FrmAsOnDateStockReport.ShowForm();
        }

        private void labourRateMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabourRate FrmLabourRate = new FrmLabourRate();
            FrmLabourRate.MdiParent = this;
            FrmLabourRate.ShowForm();
        }

        private void processIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPartyIssueWithPacket FrmPartyIssueWithPacket = new FrmPartyIssueWithPacket();
            FrmPartyIssueWithPacket.MdiParent = this;
            FrmPartyIssueWithPacket.ShowForm();
        }

        private void processReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanDashboard FrmKapanDashboard = new FrmKapanDashboard();
            FrmKapanDashboard.MdiParent = this;
            FrmKapanDashboard.ShowForm("MANUAL");

        }

        private void findRapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFindRapNewNewBackup FrmFindRapNewNewBackup = new FrmFindRapNewNewBackup();
            FrmFindRapNewNewBackup.MdiParent = this;
            FrmFindRapNewNewBackup.ShowForm();
        }

        private void createPacketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketCreate FrmSinglePacketCreate = new FrmSinglePacketCreate();
            FrmSinglePacketCreate.MdiParent = this;
            FrmSinglePacketCreate.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePacketCreate.ShowForm();
        }

        private void singleKapanLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleKapanDashboard FrmSingleKapanDashboard = new FrmSingleKapanDashboard();
            FrmSingleKapanDashboard.MdiParent = this;
            FrmSingleKapanDashboard.ShowForm();
        }
        private void roughPurchaseToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRoughPurchaseMasterDetail FrmRoughPurchaseMasterDetail = new FrmRoughPurchaseMasterDetail();
            FrmRoughPurchaseMasterDetail.MdiParent = this;
            FrmRoughPurchaseMasterDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughPurchaseMasterDetail.ShowForm();
            //FrmRoughPurchaseNew FrmRoughPurchaseNew = new FrmRoughPurchaseNew();
            //FrmRoughPurchaseNew.MdiParent = this;
            //FrmRoughPurchaseNew.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmRoughPurchaseNew.ShowForm();
        }

        private void mySingleStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.ROUGH);
        }

        private void ackPendingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketConfirmation FrmSinglePacketConfirmation = new FrmSinglePacketConfirmation();
            FrmSinglePacketConfirmation.MdiParent = this;
            FrmSinglePacketConfirmation.ShowForm();
        }

        private void splitEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsReturnWithSplit FrmSingleGoodsReturnWithSplit = new FrmSingleGoodsReturnWithSplit();
            FrmSingleGoodsReturnWithSplit.MdiParent = this;
            FrmSingleGoodsReturnWithSplit.ShowForm();
        }

        private void transactionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleTransactionView FrmSingleTransactionView = new FrmSingleTransactionView();
            FrmSingleTransactionView.MdiParent = this;
            FrmSingleTransactionView.ShowForm();
        }

        private void reasonMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmMessage FrmReason = new FrmMessage();
            //FrmReason.MdiParent = this;
            //FrmReason.ShowForm();
        }

        private void predictionTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrdType FrmPrdType = new FrmPrdType();
            FrmPrdType.MdiParent = this;
            FrmPrdType.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPrdType.ShowForm();
        }

        private void rapCalcNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFindRapNewNewBackup FrmFindRapNewNewBackup = new FrmFindRapNewNewBackup();
            FrmFindRapNewNewBackup.MdiParent = this;
            FrmFindRapNewNewBackup.ShowForm();
        }

        private void userPermissionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployeeRights FrmEmployeeRights = new FrmEmployeeRights();
            FrmEmployeeRights.MdiParent = this;
            FrmEmployeeRights.ShowForm();
        }

        private void packetLookupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPacketLookup FrmPacketLookup = new FrmPacketLookup();
            FrmPacketLookup.MdiParent = this;
            FrmPacketLookup.ShowForm();
        }

        private void barcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBarcodePrint FrmBarcodePrint = new FrmBarcodePrint();
            FrmBarcodePrint.MdiParent = this;
            FrmBarcodePrint.ShowForm();

        }

        private void liveMakPacketStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.MAK);
        }

        private void runningPossitionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRunningPossition FrmRunningPossition = new FrmRunningPossition();
            FrmRunningPossition.MdiParent = this;
            FrmRunningPossition.ShowForm(View.FrmRunningPossition.FORMTYPE.FACTORY);

        }

        private void polishPacketLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.POL);
        }

        private void facetwareCollectionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //AxonFacetWareData.FormTest FormTest = new AxonFacetWareData.FormTest();
            //FormTest.MdiParent = this;
            //FormTest.ShowForm();

        }

        private void predictionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void listToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void predictionViewMngtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrdViewManagement FrmPrdViewManagement = new FrmPrdViewManagement();
            FrmPrdViewManagement.MdiParent = this;
            FrmPrdViewManagement.ShowForm();
        }

        private void predictionViewMarkerToolStripMenuItem_Click(object sender, EventArgs e)
        {


        }

        private void rejectionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRejectionView FrmRejectionView = new FrmRejectionView();
            FrmRejectionView.MdiParent = this;
            FrmRejectionView.ShowForm();
        }

        private void xtraTabbedMdiManager1_PageAdded(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            PnlBack.Visible = false;
        }

        private void xtraTabbedMdiManager1_PageRemoved(object sender, DevExpress.XtraTabbedMdi.MdiTabPageEventArgs e)
        {
            if (xtraTabbedMdiManager1.Pages.Count == 0)
            {
                PnlBack.Visible = true;
            }
        }

        private void timer3Min_Tick(object sender, EventArgs e)
        {
            try
            {
                string StrExeUPdate = new BOMST_FormPermission().GetExeUpdateMessage(Global.gStrExeVersion);
                timer30Second.Enabled = false;
                DataTable DTab = new BOTRN_SingleIssueReturn().GetNotificationData();
                Global.gStrSuvichar = new BOMST_FormPermission().GetMessage() + StrExeUPdate;

                if (DTab.Rows.Count != 0)
                {
                    timer30Second.Enabled = true;
                }

                string Str = "";
                foreach (DataRow DRow in DTab.Rows)
                {
                    Str += "\nJangedNo : " + Val.ToString(DRow["JANGEDNO"]);
                    Str += "\nFrom Emp : " + Val.ToString(DRow["FROMEMP"]) + "\nTrf Pcs : " + Val.ToString(DRow["ISSUEPCS"]) + "\nTrf Cts : " + Val.ToString(DRow["ISSUECARAT"]);
                    Str += "\n----------------------------";
                }
                if (Str != "")
                {
                    iFormControls.iFormNotifier iFormNotifier = new iFormControls.iFormNotifier();
                    iFormNotifier = iForm;
                    iFormNotifier.TitleText = "Ack.Pendings";
                    iFormNotifier.MessageText = Str;
                    iFormNotifier.Show();
                    iFormNotifier.LinkClicked += new iFormControls.iFormNotifier.LinkClickedEventHandler(iForm_LinkClicked);
                    iFormNotifier = null;
                    new BOTRN_SingleIssueReturn().InsertNotification("PendingJanged", Str);

                    Global.Message(Str);
                }

                if (StrExeUPdate != "")
                {
                    iFormControls.iFormNotifier iFormNotifier = new iFormControls.iFormNotifier();
                    iFormNotifier = iForm;
                    iFormNotifier.TitleText = "Ack.Pendings";
                    iFormNotifier.MessageText = Str;
                    iFormNotifier.Show();
                    iFormNotifier.LinkClicked += new iFormControls.iFormNotifier.LinkClickedEventHandler(iForm_LinkClicked);
                    iFormNotifier = null;
                    new BOTRN_SingleIssueReturn().InsertNotification("PendingJanged", StrExeUPdate);

                }
                DTab.Dispose();
                DTab = null;
            }
            catch (Exception EX)
            {
            }

        }


        private void timer30Second_Tick(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = new BOTRN_SingleIssueReturn().GetNotificationData();

                if (DTab.Rows.Count == 0)
                {
                    timer30Second.Enabled = false;
                    return;
                }

                string Str = "";
                foreach (DataRow DRow in DTab.Rows)
                {

                    Str += "\nJangedNo : " + Val.ToString(DRow["JANGEDNO"]);
                    Str += "\nFrom Emp : " + Val.ToString(DRow["FROMEMP"]) + "\nTrf Pcs : " + Val.ToString(DRow["ISSUEPCS"]) + "\nTrf Cts : " + Val.ToString(DRow["ISSUECARAT"]);
                    Str += "\n----------------------------";

                }
                if (Str != "")
                {
                    iFormControls.iFormNotifier iFormNotifier = new iFormControls.iFormNotifier();
                    iFormNotifier = iForm;
                    iFormNotifier.TitleText = "Ack.Pendings";
                    iFormNotifier.MessageText = Str;
                    iFormNotifier.Show();
                    iFormNotifier.LinkClicked += new iFormControls.iFormNotifier.LinkClickedEventHandler(iForm_LinkClicked);
                    iFormNotifier = null;
                    new BOTRN_SingleIssueReturn().InsertNotification("PendingJanged", Str);

                    Global.Message(Str);
                }

                DTab.Dispose();
                DTab = null;
            }
            catch (Exception EX)
            {
            }

        }

        public void iForm_LinkClicked()
        {
            //FrmSinglePacketConfirmation FrmSinglePacketConfirmation = new FrmSinglePacketConfirmation();
            //FrmSinglePacketConfirmation.MdiParent = this;
            //FrmSinglePacketConfirmation.ShowForm();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            FrmFindRapNewNewBackup FrmFindRapNewNewBackup = new FrmFindRapNewNewBackup();
            FrmFindRapNewNewBackup.MdiParent = this;
            FrmFindRapNewNewBackup.ShowForm();
        }

        private void finalEmployeeIssueEngiArtistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsFinalIssue FrmSingleGoodsFinalIssue = new FrmSingleGoodsFinalIssue();
            FrmSingleGoodsFinalIssue.MdiParent = this;
            FrmSingleGoodsFinalIssue.ShowForm(Transaction.FrmSingleGoodsFinalIssue.FORMTYPE.SINGLE);
        }

        private void reportSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void gridDynamicReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void sizeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSize FrmSize = new FrmSize();
            FrmSize.MdiParent = this;
            FrmSize.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSize.ShowForm();
        }

        private void fullKapanAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }


        private void BtnAllLiveStock_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.ALL);
        }

        private void fileTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFileTransfer FrmFileTransfer = new FrmFileTransfer();
            //FrmFileTransfer.MdiParent = this;
            FrmFileTransfer.ShowForm();

        }

        private void factoryProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void labourPriceMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void issueReturnStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.ALL);
        }

        private void lblRefreshMenu_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            foreach (System.Windows.Forms.Form frm in this.MdiChildren)
            {
                frm.Dispose();
                frm.Close();
            }

            if (BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME != "AXONEADMIN")
            {
                CheckMenuPermission();
            }

            this.Cursor = Cursors.Default;
            Global.Message("MENU REFRESHED AS PER RIGHTS");
        }

        private void singleFileUploadToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FrmComparisionView FrmComparisionView = new FrmComparisionView();
            FrmComparisionView.MdiParent = this;
            FrmComparisionView.ShowForm(FrmComparisionView.FORMTYPE.MARKER);
        }

        private void predictionComparisionViewForAdminPivotViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmComparisionView FrmComparisionView = new FrmComparisionView();
            FrmComparisionView.MdiParent = this;
            FrmComparisionView.ShowForm(FrmComparisionView.FORMTYPE.ADMIN);
        }

        private void predictionViewMarkerLinearViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPrdViewMarker FrmPrdViewMarker = new FrmPrdViewMarker();
            FrmPrdViewMarker.MdiParent = this;
            FrmPrdViewMarker.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPrdViewMarker.ShowForm();
        }

        private void fullKapanAnalysisToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmFullKapanAnalysis FrmFullKapanAnalysis = new FrmFullKapanAnalysis();
            FrmFullKapanAnalysis.MdiParent = this;
            FrmFullKapanAnalysis.ShowForm();
        }

        private void factoryProductionReportFinalOKToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPolishOKReport FrmFactoryProduction = new FrmPolishOKReport();
            FrmFactoryProduction.MdiParent = this;
            FrmFactoryProduction.ShowForm();
        }

        private void rojmelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRojMel FrmRojMel = new FrmRojMel();
            FrmRojMel.MdiParent = this;
            FrmRojMel.ShowForm();
        }

        private void ledgerReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLedgerReport FrmLedgerReport = new FrmLedgerReport();
            FrmLedgerReport.MdiParent = this;
            FrmLedgerReport.ShowForm();
        }

        private void factoryLabourReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryLabourNew FrmFactoryLabourNew = new FrmFactoryLabourNew();
            FrmFactoryLabourNew.MdiParent = this;
            FrmFactoryLabourNew.ShowForm();
        }

        private void predictionComparisionViewForMarkerPivotViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmComparisionViewMarker FrmComparisionViewMarker = new FrmComparisionViewMarker();
            FrmComparisionViewMarker.MdiParent = this;
            FrmComparisionViewMarker.ShowForm(Masters.FrmComparisionViewMarker.FORMTYPE.MARKER);
        }

        private void labourPriceMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmLabourPriceHeadDetailNew FrmLabourPriceHeadDetailNew = new FrmLabourPriceHeadDetailNew();
            FrmLabourPriceHeadDetailNew.MdiParent = this;
            FrmLabourPriceHeadDetailNew.ShowForm();
        }

        private void labourMonthlyPcsProducToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabourPcs FrmLabourPcs = new FrmLabourPcs();
            FrmLabourPcs.MdiParent = this;
            FrmLabourPcs.ShowForm();

        }

        private void packetGradingEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketGradingEntry FrmSinglePacketGradingEntry = new FrmSinglePacketGradingEntry();
            FrmSinglePacketGradingEntry.MdiParent = this;
            FrmSinglePacketGradingEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePacketGradingEntry.ShowForm();
        }

        private void singleFileUploadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSingleFileUpload FrmSingleFileUpload = new FrmSingleFileUpload();
            FrmSingleFileUpload.MdiParent = this;
            FrmSingleFileUpload.ShowForm();
        }

        private void gradingRunningPossitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRunningPossition FrmRunningPossition = new FrmRunningPossition();
            FrmRunningPossition.MdiParent = this;
            FrmRunningPossition.ShowForm(View.FrmRunningPossition.FORMTYPE.BOMBAY);
        }

        private void factoryBombayRunningPossitionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRunningPossition FrmRunningPossition = new FrmRunningPossition();
            FrmRunningPossition.MdiParent = this;
            FrmRunningPossition.ShowForm(View.FrmRunningPossition.FORMTYPE.ALL);
        }

        private void gradingLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLiveStock FrmSinglePacketLiveStock = new FrmSinglePacketLiveStock();
            FrmSinglePacketLiveStock.MdiParent = this;
            FrmSinglePacketLiveStock.ShowForm(Transaction.FrmSinglePacketLiveStock.FORMTYPE.BOMBAY);
        }

        private void priveRevisedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPriceRevised FrmPriceRevised = new FrmPriceRevised();
            FrmPriceRevised.MdiParent = this;
            FrmPriceRevised.ShowForm();
        }

        private void messageToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmInputBox FrmInputBox = new AxoneMFGRJ.FrmInputBox();
            FrmInputBox.MdiParent = Global.gMainRef;
            FrmInputBox.ShowForm();

        }

        private void TimSuvichar_Tick(object sender, EventArgs e)
        {
            lblSuVichar.Visible = true;
            if (lblSuVichar.Left <= (PBox.Left - lblSuVichar.Width))
            {
                lblSuVichar.Left = PBox.Width;
                lblSuVichar.Text = Global.gStrSuvichar;
            }
            else if (lblSuVichar.Text.Length != 0)
            {
                lblSuVichar.Text = Global.gStrSuvichar;
                lblSuVichar.Left = (lblSuVichar.Left - 2);
            }
        }

        private void panultyIncentiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPenaltyIncentive FrmPenaltyIncentive = new FrmPenaltyIncentive();
            FrmPenaltyIncentive.MdiParent = this;
            FrmPenaltyIncentive.ShowForm();
        }

        private void workerLabourReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryLabourWorker FrmFactoryLabourWorker = new FrmFactoryLabourWorker();
            FrmFactoryLabourWorker.MdiParent = this;
            FrmFactoryLabourWorker.ShowForm();

        }

        private void blockingProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOtherProcessProduction FrmBlockingProduction = new FrmOtherProcessProduction();
            FrmBlockingProduction.MdiParent = this;
            FrmBlockingProduction.ShowForm(FrmOtherProcessProduction.FORMTYPE.BLOCKING);

        }

        private void conicProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOtherProcessProduction FrmBlockingProduction = new FrmOtherProcessProduction();
            FrmBlockingProduction.MdiParent = this;
            FrmBlockingProduction.ShowForm(FrmOtherProcessProduction.FORMTYPE.CONIC);
        }

        private void laserProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOtherProcessProduction FrmBlockingProduction = new FrmOtherProcessProduction();
            FrmBlockingProduction.MdiParent = this;
            FrmBlockingProduction.ShowForm(FrmOtherProcessProduction.FORMTYPE.SAWING);
        }

        private void blockingConicLabourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmLabourProcess FrmLabourProcess = new FrmLabourProcess();
            //FrmLabourProcess.MdiParent = this;
            //FrmLabourProcess.ShowForm(FrmLabourProcess.FORMTYPE.BLOCKINGCONIC);
        }

        private void maxiProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOtherProcessProduction FrmBlockingProduction = new FrmOtherProcessProduction();
            FrmBlockingProduction.MdiParent = this;
            FrmBlockingProduction.ShowForm(FrmOtherProcessProduction.FORMTYPE.MAXI);
        }

        private void dharProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOtherProcessProduction FrmBlockingProduction = new FrmOtherProcessProduction();
            FrmBlockingProduction.MdiParent = this;
            FrmBlockingProduction.ShowForm(FrmOtherProcessProduction.FORMTYPE.DHAR);
        }

        private void markerLabourPlusMinusReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerLabourPlusMinusReport FrmMarkerLabourPlusMinusReport = new FrmMarkerLabourPlusMinusReport();
            FrmMarkerLabourPlusMinusReport.MdiParent = this;
            FrmMarkerLabourPlusMinusReport.ShowForm();
        }

        private void markerRollingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

            FrmMarkerRollingReport FrmMarkerRollingReport = new FrmMarkerRollingReport();
            FrmMarkerRollingReport.MdiParent = this;
            FrmMarkerRollingReport.ShowForm();

            //FrmMarkerRollingReport FrmMarkerRollingReport = new FrmMarkerRollingReport();
            //FrmMarkerRollingReport.MdiParent = this;
            //FrmMarkerRollingReport.ShowForm();
        }

        private void productionAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductionAnalysis FrmProductionAnalysis = new FrmProductionAnalysis();
            FrmProductionAnalysis.MdiParent = this;
            FrmProductionAnalysis.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmProductionAnalysis.ShowForm();
        }

        private void workerRollingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmWorkerRollingReport FrmWorkerRollingReport = new FrmWorkerRollingReport();
            FrmWorkerRollingReport.MdiParent = this;
            FrmWorkerRollingReport.ShowForm();
        }

        private void productionAnalysisParameterWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMISAnalysisMakPolGrd FrmMISAnalysisMakPolGrd = new FrmMISAnalysisMakPolGrd();
            FrmMISAnalysisMakPolGrd.MdiParent = this;
            FrmMISAnalysisMakPolGrd.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMISAnalysisMakPolGrd.ShowForm();
        }

        private void artistPredictionListToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmArtistPredictionList FrmArtistPredictionList = new FrmArtistPredictionList();
            FrmArtistPredictionList.MdiParent = this;
            FrmArtistPredictionList.ShowForm();
        }

        private void itemMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmItem FrmItem = new FrmItem();
            FrmItem.MdiParent = this;
            FrmItem.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmItem.ShowForm();
        }

        private void parameterDiscountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParameterDiscount FrmParameterDiscount = new FrmParameterDiscount();
            FrmParameterDiscount.MdiParent = this;
            FrmParameterDiscount.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParameterDiscount.ShowForm();
        }

        private void discountDiffMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDiscountDiff FrmDiscountDiff = new FrmDiscountDiff();
            FrmDiscountDiff.MdiParent = this;
            FrmDiscountDiff.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDiscountDiff.ShowForm();
        }

        private void stockTallyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockTally FrmStockTally = new FrmStockTally();
            FrmStockTally.MdiParent = this;
            FrmStockTally.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmStockTally.ShowForm();
        }

        private void leaveMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLeaveEntry FrmLeaveEntry = new FrmLeaveEntry();
            FrmLeaveEntry.MdiParent = this;
            FrmLeaveEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLeaveEntry.ShowForm();
        }

        private void repairingLabourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSingleRepairingLabour FrmSingleRepairingLabour = new FrmSingleRepairingLabour();
            //FrmSingleRepairingLabour.MdiParent = this;
            //FrmSingleRepairingLabour.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmSingleRepairingLabour.ShowForm();
            FrmRepairingEntry frmRepairingEntry = new FrmRepairingEntry();
            frmRepairingEntry.MdiParent = this;
            frmRepairingEntry.ShowForm();
        }

        private void factoryAgingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryAgingReport FrmFactoryAgingReport = new FrmFactoryAgingReport();
            FrmFactoryAgingReport.MdiParent = this;
            FrmFactoryAgingReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmFactoryAgingReport.ShowForm();
        }

        private void dashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAdminDashboard FrmAdminDashboard = new FrmAdminDashboard();
            FrmAdminDashboard.MdiParent = this;
            FrmAdminDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmAdminDashboard.ShowForm();
        }

        private void breakingEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleBreakingEntry FrmSingleBreakingEntry = new FrmSingleBreakingEntry();
            FrmSingleBreakingEntry.MdiParent = this;
            FrmSingleBreakingEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleBreakingEntry.ShowForm();
        }

        private void gradingBarcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGradingBarcodePrint FrmGradingBarcodePrint = new FrmGradingBarcodePrint();
            FrmGradingBarcodePrint.MdiParent = this;
            FrmGradingBarcodePrint.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmGradingBarcodePrint.ShowForm();
        }

        private void predictionComparisionViewForAdminPivotViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmComparisionViewAdmin FrmComparisionViewAdmin = new FrmComparisionViewAdmin();
            FrmComparisionViewAdmin.MdiParent = this;
            FrmComparisionViewAdmin.ShowForm(Masters.FrmComparisionViewAdmin.FORMTYPE.ADMIN);
        }

        private void kapanProcessSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanProcessSetting FrmKapanProcessSetting = new FrmKapanProcessSetting();
            FrmKapanProcessSetting.MdiParent = this;
            FrmKapanProcessSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanProcessSetting.ShowForm();
        }

        private void packetUnlockEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPacketUnlockEntry FrmPacketUnlockEntry = new FrmPacketUnlockEntry();
            FrmPacketUnlockEntry.MdiParent = this;
            FrmPacketUnlockEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPacketUnlockEntry.ShowForm();
        }

        private void employeeOverDueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmployeeOverDueReport FrmEmployeeOverDueReport = new FrmEmployeeOverDueReport();
            FrmEmployeeOverDueReport.MdiParent = this;
            FrmEmployeeOverDueReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmEmployeeOverDueReport.ShowForm();
        }

        private void predictionPCNPacketsViewForAdminToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPCNPacketsView FrmPCNPacketsView = new FrmPCNPacketsView();
            FrmPCNPacketsView.MdiParent = this;
            FrmPCNPacketsView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPCNPacketsView.ShowForm(FrmPCNPacketsView.FORMTYPE.ADMIN);
        }

        private void gradingComparisionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerGradingComparision FrmMarkerGradingComparision = new FrmMarkerGradingComparision();
            FrmMarkerGradingComparision.MdiParent = this;
            FrmMarkerGradingComparision.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerGradingComparision.ShowForm();
        }

        private void transferFileFromOneToAnotherMachineToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGetFilesFromFTP FrmGetFilesFromFTP = new FrmGetFilesFromFTP();
            FrmGetFilesFromFTP.MdiParent = this;
            FrmGetFilesFromFTP.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmGetFilesFromFTP.ShowForm();
        }

        private void markerGradingComparisonWithLatestGrdByLabToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerGradingComparisionWithLatestGrd FrmMarkerGradingComparisionWithLatestGrd = new FrmMarkerGradingComparisionWithLatestGrd();
            FrmMarkerGradingComparisionWithLatestGrd.MdiParent = this;
            FrmMarkerGradingComparisionWithLatestGrd.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerGradingComparisionWithLatestGrd.ShowForm();
        }


        private void dollarLabourMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDollarLabourPriceDetail FrmDollarLabourPriceDetail = new FrmDollarLabourPriceDetail();
            FrmDollarLabourPriceDetail.MdiParent = this;
            FrmDollarLabourPriceDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDollarLabourPriceDetail.ShowForm();
        }

        private void markerDashboardToolStripMenuItemForMarker_Click(object sender, EventArgs e)
        {
            //FrmMarkerDashboard FrmMarkerDashboard = new FrmMarkerDashboard();
            //FrmMarkerDashboard.MdiParent = this;
            //FrmMarkerDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmMarkerDashboard.ShowForm(ReportGrid.FrmMarkerDashboard.FORMTYPE.MARKER);

            //FrmMarkerDashboardNew FrmMarkerDashboardNew = new FrmMarkerDashboardNew();
            //FrmMarkerDashboardNew.MdiParent = this;
            //FrmMarkerDashboardNew.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmMarkerDashboardNew.ShowForm(ReportGrid.FrmMarkerDashboardNew.FORMTYPE.MARKER);

            FrmMarkerDashboardCurrent FrmMarkerDashboardCurrent = new FrmMarkerDashboardCurrent();
            FrmMarkerDashboardCurrent.MdiParent = this;
            FrmMarkerDashboardCurrent.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerDashboardCurrent.ShowForm(ReportGrid.FrmMarkerDashboardCurrent.FORMTYPE.ADMIN);
        }

        private void markerDashboardToolStripMenuItemFirAdmin_Click(object sender, EventArgs e)
        {
            FrmMarkerDashboard FrmMarkerDashboard = new FrmMarkerDashboard();
            FrmMarkerDashboard.MdiParent = this;
            FrmMarkerDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerDashboard.ShowForm(ReportGrid.FrmMarkerDashboard.FORMTYPE.ADMIN);
        }

        private void transactionLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTransactionLock FrmTransactionLock = new FrmTransactionLock();
            FrmTransactionLock.MdiParent = this;
            FrmTransactionLock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmTransactionLock.ShowForm();
        }

        private void excRateMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void dollarTypePercentageToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmDollarLabourPer FrmDollarLabourPer = new FrmDollarLabourPer();
            FrmDollarLabourPer.MdiParent = this;
            FrmDollarLabourPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDollarLabourPer.ShowForm(FrmDollarLabourPer.FORMTYPE.OTHER);
        }

        private void monthlyExchangeRateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmExcRateMaster FrmExcRateMaster = new FrmExcRateMaster();
            FrmExcRateMaster.MdiParent = this;
            FrmExcRateMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmExcRateMaster.ShowForm();
        }

        private void factoryManagerLabourToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmLabourProcess FrmLabourProcess = new FrmLabourProcess();
            //FrmLabourProcess.MdiParent = this;
            //FrmLabourProcess.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmLabourProcess.ShowForm(FrmLabourProcess.FORMTYPE.FACTORYMANAGER);
        }

        private void factoryManagerProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryManagerProductionReport FrmFactoryManagerProductionReport = new FrmFactoryManagerProductionReport();
            FrmFactoryManagerProductionReport.MdiParent = this;
            FrmFactoryManagerProductionReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmFactoryManagerProductionReport.ShowForm(FrmFactoryManagerProductionReport.FORMTYPE.RD3);
        }

        private void salaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryView FrmSalaryView = new FrmSalaryView();
            FrmSalaryView.MdiParent = this;
            FrmSalaryView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryView.ShowForm();
        }

        private void markerProcessPredictionViewLinearViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerProcessPredictionView FrmMarkerProcessPredictionView = new FrmMarkerProcessPredictionView();
            FrmMarkerProcessPredictionView.MdiParent = this;
            FrmMarkerProcessPredictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerProcessPredictionView.ShowForm(FrmMarkerProcessPredictionView.FORMTYPE.CLV);
        }

        private void breakingPacketEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSingleBreakingPacketEntryDetail FrmSingleBreakingPacketEntryDetail = new FrmSingleBreakingPacketEntryDetail();
            //FrmSingleBreakingPacketEntryDetail.MdiParent = this;
            //FrmSingleBreakingPacketEntryDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmSingleBreakingPacketEntryDetail.ShowForm();

            FrmSingleBreakingPacketList FrmSingleBreakingPacketList = new FrmSingleBreakingPacketList();
            FrmSingleBreakingPacketList.MdiParent = this;
            FrmSingleBreakingPacketList.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleBreakingPacketList.ShowForm();
        }

        private void dFPlusMinusLabourPerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDollarLabourPer FrmDollarLabourPer = new FrmDollarLabourPer();
            FrmDollarLabourPer.MdiParent = this;
            FrmDollarLabourPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDollarLabourPer.ShowForm(FrmDollarLabourPer.FORMTYPE.DFPLUSMINUS);
        }

        private void kapanRollingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmKapanRollingReport FrmKapanRollingReport = new FrmKapanRollingReport();
            //FrmKapanRollingReport.MdiParent = this;
            //FrmKapanRollingReport.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmKapanRollingReport.ShowForm();

            FrmKapanRollingReportNew FrmKapanRollingReportNew = new FrmKapanRollingReportNew();
            FrmKapanRollingReportNew.MdiParent = this;
            FrmKapanRollingReportNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanRollingReportNew.ShowForm();
        }

        private void factoryManagerRD3ProductionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryManagerProductionReport FrmFactoryManagerProductionReport = new FrmFactoryManagerProductionReport();
            FrmFactoryManagerProductionReport.MdiParent = this;
            FrmFactoryManagerProductionReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmFactoryManagerProductionReport.ShowForm(FrmFactoryManagerProductionReport.FORMTYPE.RD4);
        }

        private void sawingSplitReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSawingSplitReport FrmSawingSplitReport = new FrmSawingSplitReport();
            FrmSawingSplitReport.MdiParent = this;
            FrmSawingSplitReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSawingSplitReport.ShowForm();
        }

        private void blockingPlusMinusReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBlockingLabourPlusMinusReport FrmBlockingLabourPlusMinusReport = new FrmBlockingLabourPlusMinusReport();
            FrmBlockingLabourPlusMinusReport.MdiParent = this;
            FrmBlockingLabourPlusMinusReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmBlockingLabourPlusMinusReport.ShowForm();
        }

        private void picBackground_Click(object sender, EventArgs e)
        {

        }

        private void workerProcessPredictionViewLinearViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerProcessPredictionView FrmMarkerProcessPredictionView = new FrmMarkerProcessPredictionView();
            FrmMarkerProcessPredictionView.MdiParent = this;
            FrmMarkerProcessPredictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerProcessPredictionView.ShowForm(FrmMarkerProcessPredictionView.FORMTYPE.MFG);
        }

        private void workerSalaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForWorker FrmSalaryViewForWorker = new FrmSalaryViewForWorker();
            FrmSalaryViewForWorker.MdiParent = this;
            FrmSalaryViewForWorker.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForWorker.ShowForm();
        }

        private void departmentTransactionLockUnlockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDepartmentIssueReturnLock FrmDepartmentIssueReturnLock = new FrmDepartmentIssueReturnLock();
            FrmDepartmentIssueReturnLock.MdiParent = this;
            FrmDepartmentIssueReturnLock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDepartmentIssueReturnLock.ShowForm();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmRunningPossitionDepartmentWise FrmRunningPossitionDepartmentWise = new FrmRunningPossitionDepartmentWise();
            FrmRunningPossitionDepartmentWise.MdiParent = this;
            FrmRunningPossitionDepartmentWise.ShowForm(View.FrmRunningPossitionDepartmentWise.FORMTYPE.FACTORY);
        }

        private void blockingProductionReportToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmBlockingSalaryView FrmBlockingSalaryView = new FrmBlockingSalaryView();
            FrmBlockingSalaryView.MdiParent = this;
            FrmBlockingSalaryView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmBlockingSalaryView.ShowForm();
        }

        private void predictionCopyModuleToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPredictionCopyModule FrmPredictionCopyModule = new FrmPredictionCopyModule();
            FrmPredictionCopyModule.MdiParent = this;
            FrmPredictionCopyModule.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPredictionCopyModule.ShowForm();
        }
        private void workerShapeWiseLabourPerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDollarLabourPer FrmDollarLabourPer = new FrmDollarLabourPer();
            FrmDollarLabourPer.MdiParent = this;
            FrmDollarLabourPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDollarLabourPer.ShowForm(FrmDollarLabourPer.FORMTYPE.WORKERSHAPEPER);
        }

        private void workerDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmWorkerDashboard FrmWorkerDashboard = new FrmWorkerDashboard();
            FrmWorkerDashboard.MdiParent = this;
            FrmWorkerDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmWorkerDashboard.ShowForm(ReportGrid.FrmWorkerDashboard.FORMTYPE.WORKER);
        }

        private void polishCheckerGradingAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPolishChkGradingAnalysis FrmPolishChkGradingAnalysis = new FrmPolishChkGradingAnalysis();
            FrmPolishChkGradingAnalysis.MdiParent = this;
            FrmPolishChkGradingAnalysis.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishChkGradingAnalysis.ShowForm();
        }

        private void memoAnalysisReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleMemoAnalysis FrmSingleMemoAnalysis = new FrmSingleMemoAnalysis();
            FrmSingleMemoAnalysis.MdiParent = this;
            FrmSingleMemoAnalysis.ShowForm();
        }

        private void labInclusionLabPricingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketLabPricingEntry FrmSinglePacketLabPricingEntry = new FrmSinglePacketLabPricingEntry();
            FrmSinglePacketLabPricingEntry.MdiParent = this;
            FrmSinglePacketLabPricingEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePacketLabPricingEntry.ShowForm();
        }

        private void labPrdSerialNoGenerateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSerialNoGenerate FrmSerialNoGenerat = new FrmSerialNoGenerate();
            FrmSerialNoGenerat.MdiParent = this;
            FrmSerialNoGenerat.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSerialNoGenerat.ShowForm();
        }

        private void labRecheckRepairingConfirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabReCheckConfirm FrmLabReCheckConfirm = new FrmLabReCheckConfirm();
            FrmLabReCheckConfirm.MdiParent = this;
            FrmLabReCheckConfirm.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabReCheckConfirm.ShowForm(FrmLabReCheckConfirm.FORMTYPE.LabReCheckConfirm);
        }

        private void roughPurchaseViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughPurchaseView FrmRoughPurchaseView = new FrmRoughPurchaseView();
            FrmRoughPurchaseView.MdiParent = this;
            FrmRoughPurchaseView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughPurchaseView.ShowForm();
        }

        private void roughPurchaseRollingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughPurchaseRolling FrmRoughPurchaseRolling = new FrmRoughPurchaseRolling();
            FrmRoughPurchaseRolling.MdiParent = this;
            FrmRoughPurchaseRolling.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughPurchaseRolling.ShowForm();
        }

        private void heliumColumnMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeliumMaster FrmHeliumMaster = new FrmHeliumMaster();
            FrmHeliumMaster.MdiParent = this;
            FrmHeliumMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumMaster.ShowForm();
        }

        private void lblChangePassword_Click(object sender, EventArgs e)
        {
            FrmChangePassword FrmChangePassword = new FrmChangePassword();
            FrmChangePassword.FormClosing += new FormClosingEventHandler(FrmChangePassword_FormClosing);
            FrmChangePassword.Show();
        }
        private void FrmChangePassword_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Close();
        }

        private void predictionViewFinalCheckerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmPrdViewFinalChecker FrmPrdViewFinalChecker = new FrmPrdViewFinalChecker();
            FrmPrdViewFinalChecker.MdiParent = this;
            FrmPrdViewFinalChecker.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPrdViewFinalChecker.ShowForm();
        }


        private void employeeBehaviourPointToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmpBehaviourPoint FrmEmpBehaviourPoint = new FrmEmpBehaviourPoint();
            FrmEmpBehaviourPoint.MdiParent = this;
            FrmEmpBehaviourPoint.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmEmpBehaviourPoint.ShowForm();
        }

        private void labPredictionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabPridictionView FrmLabPridictionView = new FrmLabPridictionView();
            FrmLabPridictionView.MdiParent = this;
            FrmLabPridictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabPridictionView.ShowForm();
        }

        private void MNHeliumDataView_Click(object sender, EventArgs e)
        {
            FrmHeliumDataView FrmHeliumDataView = new FrmHeliumDataView();
            FrmHeliumDataView.MdiParent = this;
            FrmHeliumDataView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumDataView.ShowForm();
        }

        private void allSalaryTypePerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryTypePer FrmSalaryTypePer = new FrmSalaryTypePer();
            FrmSalaryTypePer.MdiParent = this;
            FrmSalaryTypePer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryTypePer.ShowForm(FrmSalaryTypePer.FORMTYPE.ALL_OTHER);
        }

        private void officeSalaryTypePerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryTypePer FrmSalaryTypePer = new FrmSalaryTypePer();
            FrmSalaryTypePer.MdiParent = this;
            FrmSalaryTypePer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryTypePer.ShowForm(FrmSalaryTypePer.FORMTYPE.OFFICESALARY);
        }

        private void mumbaiBarcodePrintToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMumbaiBarcodePrint FrmMumbaiBarcodePrint = new FrmMumbaiBarcodePrint();
            FrmMumbaiBarcodePrint.MdiParent = this;
            FrmMumbaiBarcodePrint.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMumbaiBarcodePrint.ShowForm();
        }

        private void workerDashboardManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmWorkerDashboard FrmWorkerDashboard = new FrmWorkerDashboard();
            FrmWorkerDashboard.MdiParent = this;
            FrmWorkerDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmWorkerDashboard.ShowForm(ReportGrid.FrmWorkerDashboard.FORMTYPE.MANAGER);
        }

        private void singlePacketAutoIssue3ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketAutoIssue FrmSinglePacketAutoIssue = new FrmSinglePacketAutoIssue();
            FrmSinglePacketAutoIssue.MdiParent = this;
            FrmSinglePacketAutoIssue.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePacketAutoIssue.ShowForm();
        }

        private void PBox_Paint(object sender, PaintEventArgs e)
        {

        }

        private void myAgingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMyAgingReport FrmMyAgingReport = new FrmMyAgingReport();
            FrmMyAgingReport.MdiParent = this;
            FrmMyAgingReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMyAgingReport.ShowForm();
        }

        private void agingKapanWiseSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgingKapanWiseSetting FrmAgingKapanWiseSetting = new FrmAgingKapanWiseSetting();
            FrmAgingKapanWiseSetting.MdiParent = this;
            FrmAgingKapanWiseSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmAgingKapanWiseSetting.ShowForm();
        }

        private void agingProcessWiseSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgingProcessSetting FrmAgingProcessSetting = new FrmAgingProcessSetting();
            FrmAgingProcessSetting.MdiParent = this;
            FrmAgingProcessSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmAgingProcessSetting.ShowForm();
        }

        private void finalArtiestLowestPlanToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePrdArtistLowerPlanTick FrmSinglePrdArtistLowerPlanTick = new FrmSinglePrdArtistLowerPlanTick();
            FrmSinglePrdArtistLowerPlanTick.MdiParent = this;
            FrmSinglePrdArtistLowerPlanTick.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePrdArtistLowerPlanTick.ShowForm();
        }

        private void holidayMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmHolidayListMaster FrmHolidayListMaster = new FrmHolidayListMaster();
            FrmHolidayListMaster.MdiParent = this;
            FrmHolidayListMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHolidayListMaster.ShowForm();
        }

        private void heliumPrintMaxLimitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeliumPrintLimitSetting FrmHeliumPrintLimitSetting = new FrmHeliumPrintLimitSetting();
            FrmHeliumPrintLimitSetting.MdiParent = this;
            FrmHeliumPrintLimitSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumPrintLimitSetting.ShowForm();
        }

        private void heliumPacketPrintDetailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeliumPacketPrintDetail FrmHeliumPacketPrintDetail = new FrmHeliumPacketPrintDetail();
            FrmHeliumPacketPrintDetail.MdiParent = this;
            FrmHeliumPacketPrintDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumPacketPrintDetail.ShowForm();
        }

        private void dharArtistSalaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForDharAndMaxi FrmSalaryViewForDharAndMaxi = new FrmSalaryViewForDharAndMaxi();
            FrmSalaryViewForDharAndMaxi.MdiParent = this;
            FrmSalaryViewForDharAndMaxi.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForDharAndMaxi.ShowForm(FrmSalaryViewForDharAndMaxi.FORMTYPE.DHAR);
        }

        private void maxiArtistSalaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForDharAndMaxi FrmSalaryViewForDharAndMaxi = new FrmSalaryViewForDharAndMaxi();
            FrmSalaryViewForDharAndMaxi.MdiParent = this;
            FrmSalaryViewForDharAndMaxi.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForDharAndMaxi.ShowForm(FrmSalaryViewForDharAndMaxi.FORMTYPE.MAXI);
        }

        private void chiefSalaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForChiefArtist FrmSalaryViewForChiefArtist = new FrmSalaryViewForChiefArtist();
            FrmSalaryViewForChiefArtist.MdiParent = this;
            FrmSalaryViewForChiefArtist.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForChiefArtist.ShowForm();
        }

        private void memoAnalysisSummaryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleMemoAnalysisSummary FrmSingleMemoAnalysisSummary = new FrmSingleMemoAnalysisSummary();
            FrmSingleMemoAnalysisSummary.MdiParent = this;
            FrmSingleMemoAnalysisSummary.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleMemoAnalysisSummary.ShowForm();
        }

        private void settingMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSettingMaster FrmSettingMaster = new FrmSettingMaster();
            FrmSettingMaster.MdiParent = this;
            FrmSettingMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSettingMaster.ShowForm();
        }

        private void agingPacketUnlockEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmAgingPacketUnlockEntry FrmAgingPacketUnlockEntry = new FrmAgingPacketUnlockEntry();
            FrmAgingPacketUnlockEntry.MdiParent = this;
            FrmAgingPacketUnlockEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmAgingPacketUnlockEntry.ShowForm();
        }

        private void packetArtiestPolishCheckerEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePacketPolishChkGradingEntry FrmSinglePacketPolishChkGradingEntry = new FrmSinglePacketPolishChkGradingEntry();
            FrmSinglePacketPolishChkGradingEntry.MdiParent = this;
            FrmSinglePacketPolishChkGradingEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePacketPolishChkGradingEntry.ShowForm();
        }

        private void grdRepairingConfirmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabReCheckConfirm FrmLabReCheckConfirm = new FrmLabReCheckConfirm();
            FrmLabReCheckConfirm.MdiParent = this;
            FrmLabReCheckConfirm.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabReCheckConfirm.ShowForm(FrmLabReCheckConfirm.FORMTYPE.GradingRepairingConfirm);
        }

        private void toolStripMenuItem2_Click_1(object sender, EventArgs e)
        {
            FrmPridictionViewForGrading FrmPridictionViewForGrading = new FrmPridictionViewForGrading();
            FrmPridictionViewForGrading.MdiParent = this;
            FrmPridictionViewForGrading.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPridictionViewForGrading.ShowForm();
        }

        private void enginnerArtistWithProcessAmtZeroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryProcessDetailWithZero FrmSalaryProcessDetailWithZero = new FrmSalaryProcessDetailWithZero();
            FrmSalaryProcessDetailWithZero.MdiParent = this;
            FrmSalaryProcessDetailWithZero.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryProcessDetailWithZero.ShowForm();
        }

        private void GradingtoolStripMenuItem3_Click(object sender, EventArgs e)
        {
            FrmGradingComparisionView FrmGradingComparisionView = new FrmGradingComparisionView();
            FrmGradingComparisionView.MdiParent = this;
            FrmGradingComparisionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmGradingComparisionView.ShowForm();
        }

        private void polishCheckerComparisionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPolishCheckerComparisionView FrmPolishCheckerComparisionView = new FrmPolishCheckerComparisionView();
            FrmPolishCheckerComparisionView.MdiParent = this;
            FrmPolishCheckerComparisionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishCheckerComparisionView.ShowForm();

        }

        private void markergradingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGradingReportView FrmGradingReportView = new FrmGradingReportView();
            FrmGradingReportView.MdiParent = this;
            FrmGradingReportView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmGradingReportView.ShowForm();
        }

        private void markerVariationReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerVariationReport FrmMarkerVariationReport = new FrmMarkerVariationReport();
            FrmMarkerVariationReport.MdiParent = this;
            FrmMarkerVariationReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerVariationReport.ShowForm();
        }

        private void tensionSakhatLabourPerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmTensionSakhatLabourPer FrmTensionSakhatLabourPer = new FrmTensionSakhatLabourPer();
            FrmTensionSakhatLabourPer.MdiParent = this;
            FrmTensionSakhatLabourPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmTensionSakhatLabourPer.ShowForm();
        }

        private void packetGIAControlNoMappingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPacketControlNoMapping FrmPacketControlNoMapping = new FrmPacketControlNoMapping();
            FrmPacketControlNoMapping.MdiParent = this;
            FrmPacketControlNoMapping.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPacketControlNoMapping.ShowForm();
        }

        private void markerDashboardNEwToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerDashboardNew FrmMarkerDashboardNew = new FrmMarkerDashboardNew();
            FrmMarkerDashboardNew.MdiParent = this;
            FrmMarkerDashboardNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerDashboardNew.ShowForm(ReportGrid.FrmMarkerDashboardNew.FORMTYPE.MARKER);
        }

        private void salesEntryFromDiasalesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSaleEntryFromDiasales FrmSaleEntryFromDiasales = new FrmSaleEntryFromDiasales();
            FrmSaleEntryFromDiasales.MdiParent = this;
            FrmSaleEntryFromDiasales.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSaleEntryFromDiasales.ShowForm();
        }

        private void valueReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughWiseValueReport FrmRoughWiseValueReport = new FrmRoughWiseValueReport();
            FrmRoughWiseValueReport.MdiParent = this;
            FrmRoughWiseValueReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughWiseValueReport.ShowForm();
        }

        private void breakingDifferenceReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBreakingDifferenceReport FrmBreakingDifferenceReport = new FrmBreakingDifferenceReport();
            FrmBreakingDifferenceReport.MdiParent = this;
            FrmBreakingDifferenceReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmBreakingDifferenceReport.ShowForm();
        }

        private void breakingDiffProcessPredictionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBreakingDiffProcessPredictionView FrmBreakingDiffProcessPredictionView = new FrmBreakingDiffProcessPredictionView();
            FrmBreakingDiffProcessPredictionView.MdiParent = this;
            FrmBreakingDiffProcessPredictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmBreakingDiffProcessPredictionView.ShowForm();
        }

        private void makablePredictionLogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePrdMakLog FrmSinglePrdMakLog = new FrmSinglePrdMakLog();
            FrmSinglePrdMakLog.MdiParent = this;
            FrmSinglePrdMakLog.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePrdMakLog.ShowForm();
        }

        private void shiftMAsterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShiftMaster FrmShiftMaster = new FrmShiftMaster();
            FrmShiftMaster.MdiParent = this;
            FrmShiftMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmShiftMaster.ShowForm();
        }

        private void blockingProcessPredictionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerProcessPredictionView FrmMarkerProcessPredictionView = new FrmMarkerProcessPredictionView();
            FrmMarkerProcessPredictionView.MdiParent = this;
            FrmMarkerProcessPredictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerProcessPredictionView.ShowForm(FrmMarkerProcessPredictionView.FORMTYPE.BLOCKING);
        }

        private void blockingCheckerSalaryViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForBlockingChecker FrmSalaryViewForBlockingChecker = new FrmSalaryViewForBlockingChecker();
            FrmSalaryViewForBlockingChecker.MdiParent = this;
            FrmSalaryViewForBlockingChecker.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForBlockingChecker.ShowForm();
        }




        private void memoAnalysisReport1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleMemoAnalysisOther FrmSingleMemoAnalysisOther = new FrmSingleMemoAnalysisOther();
            FrmSingleMemoAnalysisOther.MdiParent = this;
            FrmSingleMemoAnalysisOther.ShowForm();
        }

        private void memoAnalysisSummaryReport1ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleMemoAnalysisOtherSummary FrmSingleMemoAnalysisOtherSummary = new FrmSingleMemoAnalysisOtherSummary();
            FrmSingleMemoAnalysisOtherSummary.MdiParent = this;
            FrmSingleMemoAnalysisOtherSummary.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleMemoAnalysisOtherSummary.ShowForm();
        }

        private void productionReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProductionReport FrmProductionReport = new FrmProductionReport();
            FrmProductionReport.MdiParent = this;
            FrmProductionReport.Show();
        }

        private void empWiseSplitReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmEmpWiseSplitReport FrmEmpWiseSplitReport = new FrmEmpWiseSplitReport();
            FrmEmpWiseSplitReport.MdiParent = this;
            FrmEmpWiseSplitReport.ShowForm();
        }

        private void bombayTransLabReportMIXToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBombayTransLabWiseReportMIX FrmBombayTransLabWiseReportMIX = new FrmBombayTransLabWiseReportMIX();
            FrmBombayTransLabWiseReportMIX.MdiParent = this;
            FrmBombayTransLabWiseReportMIX.ShowForm();
        }

        private void bombayTransLabReportGIAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBombayTransLabWiseReportGIA FrmBombayTransLabWiseReportGIA = new FrmBombayTransLabWiseReportGIA();
            FrmBombayTransLabWiseReportGIA.MdiParent = this;
            FrmBombayTransLabWiseReportGIA.ShowForm();
        }

        private void artistwisePolishReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArtistWisePolishReport FrmArtistWisePolishReport = new FrmArtistWisePolishReport();
            FrmArtistWisePolishReport.MdiParent = this;
            FrmArtistWisePolishReport.ShowForm();
        }

        private void artistWisePendingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmArtistWisePendingReport FrmArtistWisePendingReport = new FrmArtistWisePendingReport();
            FrmArtistWisePendingReport.MdiParent = this;
            FrmArtistWisePendingReport.ShowForm();
        }

        private void roughPurchaseEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughPurchaseMasterDetail FrmRoughPurchaseMasterDetail = new FrmRoughPurchaseMasterDetail();
            FrmRoughPurchaseMasterDetail.MdiParent = this;
            FrmRoughPurchaseMasterDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughPurchaseMasterDetail.ShowForm();
        }

        private void gradingAnalysisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGradingAnalysis FrmGradingAnalysis = new FrmGradingAnalysis();
            FrmGradingAnalysis.MdiParent = this;
            FrmGradingAnalysis.ShowForm();
        }

        private void saleDemandEntryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSaleDemandEntry FrmSaleDemandEntry = new FrmSaleDemandEntry();
            FrmSaleDemandEntry.MdiParent = this;
            FrmSaleDemandEntry.ShowForm();
        }

        private void managerWisePendingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManagerWisePendingReport FrmManagerWisePendingReport = new FrmManagerWisePendingReport();
            FrmManagerWisePendingReport.MdiParent = this;
            FrmManagerWisePendingReport.ShowForm();
        }

        private void polishOkPacketUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPolishOkPacketUpdate FrmPolishOkPacketUpdate = new FrmPolishOkPacketUpdate();
            FrmPolishOkPacketUpdate.MdiParent = this;
            FrmPolishOkPacketUpdate.ShowForm();
        }

        private void mFGPredictionComparisionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMfgPrdComp FrmMfgPrdComp = new FrmMfgPrdComp();
            FrmMfgPrdComp.MdiParent = this;
            FrmMfgPrdComp.ShowForm();
        }

        private void subjectMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSubject FrmSubject = new FrmSubject();
            FrmSubject.MdiParent = this;
            FrmSubject.ShowForm();
        }

        private void polishOKReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPolishOKReport FrmPolishOKReport = new FrmPolishOKReport();
            FrmPolishOKReport.MdiParent = this;
            FrmPolishOKReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishOKReport.ShowForm();
        }

        private void galaxyReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmGalaxyReport FrmGalaxyReport = new FrmGalaxyReport();
            FrmGalaxyReport.MdiParent = this;
            FrmGalaxyReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmGalaxyReport.ShowForm();
        }

        private void processWiseReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProcessWiseReport FrmProcessWiseReport = new FrmProcessWiseReport();
            FrmProcessWiseReport.MdiParent = this;
            FrmProcessWiseReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmProcessWiseReport.ShowForm();
        }

        private void cutWiseProcessReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmCutProcessWiseReport FrmCutProcessWiseReport = new FrmCutProcessWiseReport();
            //FrmCutProcessWiseReport.MdiParent = this;
            //FrmCutProcessWiseReport.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmCutProcessWiseReport.ShowForm();
        }

        private void labExpenseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabExpense FrmLabExpense = new FrmLabExpense();
            FrmLabExpense.MdiParent = this;
            FrmLabExpense.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabExpense.ShowForm();
        }

        private void mixSplitHistoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughPurchaseMixSplitView FrmRoughPurchaseMixSplitView = new FrmRoughPurchaseMixSplitView();
            FrmRoughPurchaseMixSplitView.MdiParent = this;
            FrmRoughPurchaseMixSplitView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughPurchaseMixSplitView.ShowForm();
        }

        private void finalEmployeeIssueManualToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsFinalIssue FrmSingleGoodsFinalIssue = new FrmSingleGoodsFinalIssue();
            FrmSingleGoodsFinalIssue.MdiParent = this;
            FrmSingleGoodsFinalIssue.ShowForm(Transaction.FrmSingleGoodsFinalIssue.FORMTYPE.LOOSE);
        }

        private void locationMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLedger FrmLedger = new FrmLedger();
            FrmLedger.MdiParent = this;
            FrmLedger.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLedger.ShowForm("LOCATION");
        }

        private void fullKapanAnalysisToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmFullKapanAnalysis FrmFullKapanAnalysis = new FrmFullKapanAnalysis();
            FrmFullKapanAnalysis.MdiParent = this;
            FrmFullKapanAnalysis.ShowForm();
        }

        private void planingReportAssorterWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPlaningReportAssorterWise FrmPlaningReportAssorterWise = new FrmPlaningReportAssorterWise();
            FrmPlaningReportAssorterWise.MdiParent = this;
            FrmPlaningReportAssorterWise.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPlaningReportAssorterWise.ShowForm();
        }

        private void goodToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsTransferNew FrmSingleGoodsTransferNew = new FrmSingleGoodsTransferNew();
            FrmSingleGoodsTransferNew.MdiParent = this;
            FrmSingleGoodsTransferNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleGoodsTransferNew.ShowForm();
        }

        private void kapanDetailReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanDetailReport FrmKapanDetailReport = new FrmKapanDetailReport();
            FrmKapanDetailReport.MdiParent = this;
            FrmKapanDetailReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanDetailReport.ShowForm();
        }

        private void sizeQuaterMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParaReportCriteria FrmParameterCriteriaMaster = new FrmParaReportCriteria();
            FrmParameterCriteriaMaster.MdiParent = this;
            FrmParameterCriteriaMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParameterCriteriaMaster.ShowForm();
        }

        private void singleGoodsTransferLossUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsTransferLossUpdate FrmSingleGoodsTransferLossUpdate = new FrmSingleGoodsTransferLossUpdate();
            FrmSingleGoodsTransferLossUpdate.MdiParent = this;
            FrmSingleGoodsTransferLossUpdate.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleGoodsTransferLossUpdate.ShowForm();
        }

        private void factoryWisePolishReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanFactoryWisePolishReport FrmKapanFactoryWisePolishReport = new FrmKapanFactoryWisePolishReport();
            FrmKapanFactoryWisePolishReport.MdiParent = this;
            FrmKapanFactoryWisePolishReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanFactoryWisePolishReport.ShowForm();
        }

        private void parcelLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParcelPacketLiveStock FrmParcelPacketLiveStock = new FrmParcelPacketLiveStock();
            FrmParcelPacketLiveStock.MdiParent = this;
            FrmParcelPacketLiveStock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParcelPacketLiveStock.ShowForm();
        }

        private void parcelTransactionViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParcelTransactionView FrmParcelTransactionView = new FrmParcelTransactionView();
            FrmParcelTransactionView.MdiParent = this;
            FrmParcelTransactionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParcelTransactionView.ShowForm();
        }

        private void roughAllotmentEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughAllotmentEntry FrmRoughAllotmentEntry = new FrmRoughAllotmentEntry();
            FrmRoughAllotmentEntry.MdiParent = this;
            FrmRoughAllotmentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmRoughAllotmentEntry.ShowForm();
        }

        private void labourProcessMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabourProcess FrmLabourProcess = new FrmLabourProcess();
            FrmLabourProcess.MdiParent = this;
            FrmLabourProcess.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabourProcess.ShowForm();
        }

        private void returnWithSplitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //FrmSingleGoodsReturnWithSplitNew FrmSingleGoodsReturnWithSplitNew = new FrmSingleGoodsReturnWithSplitNew();
            //FrmSingleGoodsReturnWithSplitNew.MdiParent = this;
            //FrmSingleGoodsReturnWithSplitNew.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmSingleGoodsReturnWithSplitNew.ShowForm();

            FrmSingleGoodsReturnWithSplit FrmSingleGoodsReturnWithSplit = new FrmSingleGoodsReturnWithSplit();
            FrmSingleGoodsReturnWithSplit.MdiParent = this;
            FrmSingleGoodsReturnWithSplit.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleGoodsReturnWithSplit.ShowForm();
        }

        private void processWiseLabourReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryViewForProcessWise FrmSalaryViewForProcessWise = new FrmSalaryViewForProcessWise();
            FrmSalaryViewForProcessWise.MdiParent = this;
            FrmSalaryViewForProcessWise.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForProcessWise.ShowForm();

        }

        private void rapCalculatorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFindRap FrmFindRap = new FrmFindRap();
            FrmFindRap.MdiParent = this;
            FrmFindRap.ShowForm();
        }

        private void pOkReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm4POKReport Frm4POKReport = new Frm4POKReport();
            Frm4POKReport.MdiParent = this;
            Frm4POKReport.ShowForm();
        }

        private void pWisePolishReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Frm4PWisePolishReport Frm4PWisePolishReport = new Frm4PWisePolishReport();
            Frm4PWisePolishReport.MdiParent = this;
            Frm4PWisePolishReport.ShowForm();
        }

        private void dollarLabourPriceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDollarLabourUpload FrmDollarLabourUpload = new FrmDollarLabourUpload();
            FrmDollarLabourUpload.MdiParent = this;
            FrmDollarLabourUpload.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDollarLabourUpload.ShowForm();
        }

        private void stockPrintReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockPrintReport FrmStockPrintReport = new FrmStockPrintReport();
            FrmStockPrintReport.MdiParent = this;
            FrmStockPrintReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmStockPrintReport.ShowForm();
        }

        private void workerSalaryDataProcessToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMarkerProcessPredictionView FrmMarkerProcessPredictionView = new FrmMarkerProcessPredictionView();
            FrmMarkerProcessPredictionView.MdiParent = this;
            FrmMarkerProcessPredictionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMarkerProcessPredictionView.ShowForm(FrmMarkerProcessPredictionView.FORMTYPE.MFG);
        }

        private void workerSalaryViewToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmSalaryViewForWorker FrmSalaryViewForWorker = new FrmSalaryViewForWorker();
            FrmSalaryViewForWorker.MdiParent = this;
            FrmSalaryViewForWorker.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSalaryViewForWorker.ShowForm();
        }

        private void shapeWiseMkblPrdReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmShapeWisePrdReport FrmShapeWisePrdReport = new FrmShapeWisePrdReport();
            FrmShapeWisePrdReport.MdiParent = this;
            FrmShapeWisePrdReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmShapeWisePrdReport.ShowForm();
        }

        private void processWiseLossPerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pLabourReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSalaryView4pLabourReport Frm4pLabourReport = new FrmSalaryView4pLabourReport();
            Frm4pLabourReport.MdiParent = this;
            Frm4pLabourReport.Tag = ((ToolStripMenuItem)sender).Tag;
            Frm4pLabourReport.ShowForm();
        }

        private void stockPrintReportKapanWiseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockPrintReportKapanWise FrmStockPrintReportKapanWise = new FrmStockPrintReportKapanWise();
            FrmStockPrintReportKapanWise.MdiParent = this;
            FrmStockPrintReportKapanWise.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmStockPrintReportKapanWise.ShowForm();
        }

        private void singleTransactionUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleTransactionUpdate FrmSingleTransactionUpdate = new FrmSingleTransactionUpdate();
            FrmSingleTransactionUpdate.MdiParent = this;
            FrmSingleTransactionUpdate.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleTransactionUpdate.ShowForm();
        }

        private void crapsIssueReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmCrapsIssueReturn FrmCrapsIssueReturn = new FrmCrapsIssueReturn();
            FrmCrapsIssueReturn.MdiParent = this;
            FrmCrapsIssueReturn.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmCrapsIssueReturn.ShowForm();
        }

        private void singlePolishOKTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSinglePolishOKTransfer FrmSinglePolishOKTransfer = new FrmSinglePolishOKTransfer();
            FrmSinglePolishOKTransfer.MdiParent = this;
            FrmSinglePolishOKTransfer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSinglePolishOKTransfer.ShowForm();
        }

        private void unPlanningReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmUnPlanningReport FrmUnPlanningReport = new FrmUnPlanningReport();
            FrmUnPlanningReport.MdiParent = this;
            FrmUnPlanningReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmUnPlanningReport.ShowForm();
        }

        private void returnWithSplitNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsReturnWithSplitNew FrmSingleGoodsReturnWithSplitNew = new FrmSingleGoodsReturnWithSplitNew();
            FrmSingleGoodsReturnWithSplitNew.MdiParent = this;
            FrmSingleGoodsReturnWithSplitNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleGoodsReturnWithSplitNew.ShowForm();
        }

        private void manualEntryLiveStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmManualEntryLiveStock FrmManualEntryLiveStock = new FrmManualEntryLiveStock();
            FrmManualEntryLiveStock.MdiParent = this;
            FrmManualEntryLiveStock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmManualEntryLiveStock.ShowForm();
        }

        private void departmentWiseProcessLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDepartmentWiseProcessLock frmdepartmentwiseprocesslock = new FrmDepartmentWiseProcessLock();
            frmdepartmentwiseprocesslock.MdiParent = this;
            frmdepartmentwiseprocesslock.Tag = ((ToolStripMenuItem)sender).Tag;
            frmdepartmentwiseprocesslock.ShowForm();
        }

        private void autoReturnTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsTransferNew frmSingleGoodsTransferNew = new FrmSingleGoodsTransferNew();
            frmSingleGoodsTransferNew.MdiParent = this;
            frmSingleGoodsTransferNew.Tag = ((ToolStripMenuItem)sender).Tag;
            frmSingleGoodsTransferNew.ShowForm_AutoReturn();
        }

        private void autoMarkerIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsTransferNew frmSingleGoodsTransferNew = new FrmSingleGoodsTransferNew();
            frmSingleGoodsTransferNew.MdiParent = this;
            frmSingleGoodsTransferNew.Tag = ((ToolStripMenuItem)sender).Tag;
            frmSingleGoodsTransferNew.ShowForm_AutoMarkerIssue();
        }

        private void myStockForReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsReturnLiveStock frmSingleGoodsReturn = new FrmSingleGoodsReturnLiveStock();
            frmSingleGoodsReturn.MdiParent = this;
            frmSingleGoodsReturn.Tag = ((ToolStripMenuItem)sender).Tag;
            frmSingleGoodsReturn.ShowForm(Transaction.FrmSingleGoodsReturnLiveStock.FORMTYPE.RETURNSTOCK);
        }

        private void lblUserConfig_Click(object sender, EventArgs e)
        {
            FrmUserConfig FrmUserConnectionMaster = new FrmUserConfig();
            FrmUserConnectionMaster.ShowForm();
        }

        private void qCStockForReturnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsReturnLiveStock frmSingleGoodsReturn = new FrmSingleGoodsReturnLiveStock();
            frmSingleGoodsReturn.MdiParent = this;
            frmSingleGoodsReturn.Tag = ((ToolStripMenuItem)sender).Tag;
            frmSingleGoodsReturn.ShowForm(Transaction.FrmSingleGoodsReturnLiveStock.FORMTYPE.QCSTOCK);
        }

        private void parcelFactoryRunningStockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRunningPossitionParcel FrmRunningPossitionParcel = new FrmRunningPossitionParcel();
            FrmRunningPossitionParcel.MdiParent = this;
            FrmRunningPossitionParcel.ShowForm(View.FrmRunningPossitionParcel.FORMTYPE.FACTORY);
        }

        private void nonQCLiveStockDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNonQCLiveStock FrmNonQCLiveStock = new FrmNonQCLiveStock();
            FrmNonQCLiveStock.MdiParent = this;
            FrmNonQCLiveStock.ShowForm();
        }

        private void kapanWiseRollingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanWiseRollingReportNew FrmKapanWiseRollingReportNew = new FrmKapanWiseRollingReportNew();
            FrmKapanWiseRollingReportNew.MdiParent = this;
            FrmKapanWiseRollingReportNew.ShowForm();
        }

        private void roughStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughStockReport FrmRoughStockReport = new FrmRoughStockReport();
            FrmRoughStockReport.MdiParent = this;
            FrmRoughStockReport.ShowForm(View.FrmRoughStockReport.FORMTYPE.INWARD);
        }

        private void billWisePaymentToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmBillWiseEntry FrmBillWiseEntry = new FrmBillWiseEntry();
            FrmBillWiseEntry.MdiParent = this;
            FrmBillWiseEntry.ShowForm();
        }

        private void roughCostReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughCostAnalysis FrmRoughCostAnalysis = new FrmRoughCostAnalysis();
            FrmRoughCostAnalysis.MdiParent = this;
            FrmRoughCostAnalysis.ShowForm();
        }

        private void reportMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmReportMasterNew FrmReportMasterNew = new FrmReportMasterNew();
            FrmReportMasterNew.MdiParent = this;
            FrmReportMasterNew.ShowForm();
        }

        private void stockReportsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilterStockReport FrmFilterStockReport = new FrmFilterStockReport();
            FrmFilterStockReport.MdiParent = this;
            FrmFilterStockReport.ShowForm();
        }

        private void kapanValuationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanValuation FrmKapanValuation = new FrmKapanValuation();
            FrmKapanValuation.MdiParent = this;
            FrmKapanValuation.ShowForm();
        }

        private void purchaseStockReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFilterPuchaseStockReport frmPuchaseStockReport = new FrmFilterPuchaseStockReport();
            frmPuchaseStockReport.MdiParent = this;
            frmPuchaseStockReport.ShowForm();
        }

        private void qCRejectionCheckToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmQCRejectionCheck frmQcRejectionCheck = new FrmQCRejectionCheck();
            frmQcRejectionCheck.MdiParent = this;
            frmQcRejectionCheck.ShowForm();
        }

        private void jobworkPurchaseToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmJobWorkPurchaseMasterDetail FrmJobWorkPurchaseMasterDetail = new FrmJobWorkPurchaseMasterDetail();
            FrmJobWorkPurchaseMasterDetail.MdiParent = this;
            FrmJobWorkPurchaseMasterDetail.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmJobWorkPurchaseMasterDetail.ShowForm();

        }

        private void jobworkPurchaseViewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmJobWorkPurchaseView FrmJobWorkPurchaseView = new FrmJobWorkPurchaseView();
            FrmJobWorkPurchaseView.MdiParent = this;
            FrmJobWorkPurchaseView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmJobWorkPurchaseView.ShowForm();
        }

        private void lblChangeUser_Click(object sender, EventArgs e)
        {
            try
            {
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "LEDGERCODE, LEDGERNAME";
                FrmSearch.mSearchText = "";
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = new BOMST_Ledger().ChangeUser(Global.gIntGlobalUserID);

                FrmSearch.mColumnsToHide = "LEDGER_ID,DEPARTMENT_ID,DESIGNATION_ID,MANAGER_ID,LOGINHST_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();

                if (FrmSearch.mDRow != null)
                {
                    DataRow DRow = FrmSearch.mDRow;

                    BusLib.Configuration.BOConfiguration.gEmployeeProperty = new LedgerMasterProperty();
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID = Val.ToInt64(DRow["LEDGER_ID"]);

                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERCODE = Val.ToString(DRow["LEDGERCODE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME = Val.ToString(DRow["LEDGERNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAMEGUJARATI = Val.ToString(DRow["LEDGERNAMEGUJARATI"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERGROUP = Val.ToString(DRow["LEDGERGROUP"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.CONTACTPERSON = Val.ToString(DRow["CONTACTPERSON"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.MOBILENO1 = Val.ToString(DRow["MOBILENO1"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.MOBILENO2 = Val.ToString(DRow["MOBILENO2"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKNAME = Val.ToString(DRow["BANKNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKIFSCCODE = Val.ToString(DRow["BANKIFSCCODE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKACCOUNTNO = Val.ToString(DRow["BANKACCOUNTNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BANKACCOUNTNAME = Val.ToString(DRow["BANKACCOUNTNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.GSTNO = Val.ToString(DRow["GSTNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.CSTNO = Val.ToString(DRow["CSTNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.PANNO = Val.ToString(DRow["PANNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BILLINGADDRESS = Val.ToString(DRow["BILLINGADDRESS"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.BILLINGSTATE = Val.ToString(DRow["BILLINGSTATE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.SHIPPINGADDRESS = Val.ToString(DRow["SHIPPINGADDRESS"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.SHIPPINGSTATE = Val.ToString(DRow["SHIPPINGSTATE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.ISACTIVE = Val.ToBoolean(DRow["ISACTIVE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID = Val.ToInt32(DRow["DEPARTMENT_ID"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTNAME = Val.ToString(DRow["DEPARTMENTNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENTGROUP = Val.ToString(DRow["DEPARTMENTGROUP"]);

                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATION_ID = Val.ToInt32(DRow["DESIGNATION_ID"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.DESIGNATIONNAME = Val.ToString(DRow["DESIGNATIONNAME"]);

                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANY_ID = Val.ToInt64(DRow["COMPANY_ID"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME = Val.ToString(DRow["COMPANYNAME"]);

                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGER_ID = Val.ToInt64(DRow["MANAGER_ID"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERCODE = Val.ToString(DRow["MANAGERCODE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.MANAGERNAME = Val.ToString(DRow["MANAGERNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.SALARYTYPE = Val.ToString(DRow["SALARYTYPE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.SALARY = Val.Val(DRow["SALARY"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.EXPSALARY = Val.Val(DRow["EXPSALARY"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME = Val.ToString(DRow["USERNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.PASSWORD = Val.ToString(DRow["PASSWORD"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.ADHARNO = Val.ToString(DRow["ADHARNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.EMPLOYEETYPE = Val.ToString(DRow["EMPLOYEETYPE"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.STYDY = Val.ToString(DRow["STYDY"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.IDCARDNO = Val.ToString(DRow["IDCARDNO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.CONTACTPERSONMOBILENO = Val.ToString(DRow["CONTACTPERSONMOBILENO"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVCOMPANYNAME = Val.ToString(DRow["PREVCOMPANYNAME"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVDESIGNATION = Val.ToString(DRow["PREVDESIGNATION"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.PREVSALARY = Val.Val(DRow["PREVSALARY"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.TOTALEXP = Val.Val(DRow["TOTALEXP"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.AUTOCONFIRM = Val.ToBoolean(DRow["AUTOCONFIRM"]);
                    BusLib.Configuration.BOConfiguration.gEmployeeProperty.LOGINHST_ID = Val.ToInt64(Val.ToString(DRow["LOGINHST_ID"])); //#P : 16-10-2019

                    this.Text = "Welcome " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME + " [ USERNAME : " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGERNAME + "  & IP : " + BusLib.Configuration.BOConfiguration.ComputerIP + " ] [V : " + Global.gStrExeVersion + " ]";
                    lblChangeUser.Text = "Change User ( " + BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME + " )";

                    this.Cursor = Cursors.WaitCursor;
                    foreach (System.Windows.Forms.Form frm in this.MdiChildren)
                    {
                        frm.Dispose();
                        frm.Close();
                    }
                    this.Cursor = Cursors.Default;
                    Global.Message("USER SESSION CHANGED");
                }

                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void yearMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmYearMaster frmyearmaster = new FrmYearMaster();
            frmyearmaster.MdiParent = this;
            frmyearmaster.Tag = ((ToolStripMenuItem)sender).Tag;
            frmyearmaster.ShowForm();
        }

        private void qCPenaltyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormQCPenalty FormQCPenalty = new FormQCPenalty();
            FormQCPenalty.MdiParent = this;
            FormQCPenalty.ShowForm();
        }

        private void qCLabourReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmQCPenaltyReport FrmQCPenaltyReport = new FrmQCPenaltyReport();
            FrmQCPenaltyReport.MdiParent = this;
            FrmQCPenaltyReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmQCPenaltyReport.ShowForm();
        }

        private void polishPacketLiveStockToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

            FrmPolishPacketLiveStock FrmPolishPacketLiveStock = new FrmPolishPacketLiveStock();
            FrmPolishPacketLiveStock.MdiParent = this;
            FrmPolishPacketLiveStock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishPacketLiveStock.ShowForm();
        }

        private void toolStripMenuItem1_Click_1(object sender, EventArgs e)
        {
            FrmPolishTransactionView FrmPolishTransactionView = new FrmPolishTransactionView();
            FrmPolishTransactionView.MdiParent = this;
            FrmPolishTransactionView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishTransactionView.ShowForm();
        }

        private void polishAssortmentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }

        private void stockTallyNewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmStockTallyNew FrmStockTallyNew = new FrmStockTallyNew();
            FrmStockTallyNew.MdiParent = this;
            FrmStockTallyNew.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmStockTallyNew.ShowForm();
        }

        private void qCTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {

            //FrmQCTransfer FrmQCTransfer = new FrmQCTransfer();
            //FrmQCTransfer.MdiParent = this;
            //FrmQCTransfer.Tag = ((ToolStripMenuItem)sender).Tag;
            //FrmQCTransfer.ShowForm();
        }



        private void menuStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void mixToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMixSize FrmMixSize = new FrmMixSize();
            FrmMixSize.MdiParent = this;
            FrmMixSize.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMixSize.ShowForm();
        }

        private void priceDateMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmPriceDateMaster FrmPriceDateMaster = new FrmPriceDateMaster();
            FrmPriceDateMaster.MdiParent = this;
            FrmPriceDateMaster.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPriceDateMaster.ShowForm();
        }

        private void mixClarityMasterToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = this;
            FrmParameter.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParameter.ShowForm1("MIX_CLARITY");
        }

        private void mixAssortmentPriceToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmMixPriceChart FrmParcelMixClarityPriceUpload = new FrmMixPriceChart();
            FrmParcelMixClarityPriceUpload.MdiParent = this;
            FrmParcelMixClarityPriceUpload.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParcelMixClarityPriceUpload.ShowForm();
        }

        private void kapanInwardToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmKapanInward FrmKapanInward = new FrmKapanInward();
            FrmKapanInward.MdiParent = this;
            FrmKapanInward.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanInward.ShowForm();
        }

        private void sizeWiseAssortmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmSizeAssortment FrmSizeAssortment = new FrmSizeAssortment();
            FrmSizeAssortment.MdiParent = this;
            FrmSizeAssortment.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSizeAssortment.ShowForm();
        }

        private void clarityWiseAssortmentToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmClarityAssortment FrmClarityAssortment = new FrmClarityAssortment();
            FrmClarityAssortment.MdiParent = this;
            FrmClarityAssortment.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmClarityAssortment.ShowForm();
        }

        private void clarityWiseAssortmentViewToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FrmClarityAssortmentView FrmClarityAssortmentView = new FrmClarityAssortmentView();
            FrmClarityAssortmentView.MdiParent = this;
            FrmClarityAssortmentView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmClarityAssortmentView.ShowForm();
        }

        private void packetDollarUnloockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPacketDollarUnloock FrmPacketDollarUnloock = new FrmPacketDollarUnloock();
            FrmPacketDollarUnloock.MdiParent = this;
            FrmPacketDollarUnloock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPacketDollarUnloock.ShowForm();

        }

        private void utilityToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void polishPacketLiveStockToolStripMenuItem_Click_2(object sender, EventArgs e)
        {
            FrmPolishPacketLiveStock FrmPolishPacketLiveStock = new FrmPolishPacketLiveStock();
            FrmPolishPacketLiveStock.MdiParent = this;
            FrmPolishPacketLiveStock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPolishPacketLiveStock.ShowForm();
        }

        private void kapanOutwardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOutwardKapan FrmOutwardKapan = new FrmOutwardKapan();
            FrmOutwardKapan.MdiParent = this;
            FrmOutwardKapan.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmOutwardKapan.ShowForm();
        }

        private void roughStockJobWarkReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmRoughStockReport FrmRoughStockReport = new FrmRoughStockReport();
            FrmRoughStockReport.MdiParent = this;
            FrmRoughStockReport.ShowForm(View.FrmRoughStockReport.FORMTYPE.JOBWORK);
        }

        private void kapanEstimnetToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanEstimentEntry FrmKapanEstimentEntry = new FrmKapanEstimentEntry();
            FrmKapanEstimentEntry.MdiParent = this;
            FrmKapanEstimentEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanEstimentEntry.ShowForm();
        }

        private void toolStripMenuItem2_Click_2(object sender, EventArgs e)
        {
            FrmKapanInwardView FrmKapanInwardView = new FrmKapanInwardView();
            FrmKapanInwardView.MdiParent = this;
            FrmKapanInwardView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanInwardView.ShowForm();
        }

        private void frmPenaltyIncentiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPenaltyIncentive FrmPenaltyIncentive = new FrmPenaltyIncentive();
            FrmPenaltyIncentive.MdiParent = this;
            FrmPenaltyIncentive.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPenaltyIncentive.ShowForm();
        }

        private void kapanTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanTransfer FrmKapanTransfer = new FrmKapanTransfer();
            //FrmDataTransfer.MdiParent = this;
            FrmKapanTransfer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanTransfer.ShowDialog();
        }

        private void departmentWiseJangedSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDepartmentWiseJangedSetting FrmDepartmentWiseJangedSetting = new FrmDepartmentWiseJangedSetting();
            FrmDepartmentWiseJangedSetting.MdiParent = this;
            FrmDepartmentWiseJangedSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDepartmentWiseJangedSetting.ShowForm();
        }

        private void orderEntryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrderEntry FrmOrderEntry = new FrmOrderEntry();
            FrmOrderEntry.MdiParent = this;
            FrmOrderEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmOrderEntry.ShowForm();
        }

        private void orderDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmOrderDashboardView FrmOrderDashboardView = new FrmOrderDashboardView();
            FrmOrderDashboardView.MdiParent = this;
            FrmOrderDashboardView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmOrderDashboardView.ShowForm();
        }

        private void dataTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmDataTransfer FrmDataTransfer = new FrmDataTransfer();
            FrmDataTransfer.MdiParent = this;
            FrmDataTransfer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmDataTransfer.ShowForm();
        }

        private void heliumFileImportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeliumFileImport FrmHeliumFileImport = new FrmHeliumFileImport();
            FrmHeliumFileImport.MdiParent = this;
            FrmHeliumFileImport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumFileImport.ShowForm();
        }

        private void mumbaiTransferToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMumbaiTransfer FrmMumbaiTransfer = new FrmMumbaiTransfer();
            FrmMumbaiTransfer.MdiParent = this;
            FrmMumbaiTransfer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmMumbaiTransfer.ShowForm();
        }

        private void ruoghPurchaseRajviToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void predictionSizeLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLockPredictionEntry FrmLockPredictionEntry = new FrmLockPredictionEntry();
            FrmLockPredictionEntry.MdiParent = this;
            FrmLockPredictionEntry.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLockPredictionEntry.ShowForm();
        }

        private void processWiseLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProcessWiseLossPer FrmProcessWiseLossPer = new FrmProcessWiseLossPer();
            FrmProcessWiseLossPer.MdiParent = this;
            FrmProcessWiseLossPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmProcessWiseLossPer.ShowForm(Masters.FrmProcessWiseLossPer.FormType.DEPTLOCK);
        }

        private void planningAdminDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPlanningDashboard FrmPlanningDashboard = new FrmPlanningDashboard();
            FrmPlanningDashboard.MdiParent = this;
            FrmPlanningDashboard.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPlanningDashboard.ShowForm();
        }

        private void planningAdminDashboardLockToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPlanningDashboardWithOrderAndLock FrmPlanningDashboardWithOrderAndLock = new FrmPlanningDashboardWithOrderAndLock();
            FrmPlanningDashboardWithOrderAndLock.MdiParent = this;
            FrmPlanningDashboardWithOrderAndLock.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPlanningDashboardWithOrderAndLock.ShowForm();
        }

        private void employeeWiseLossToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProcessWiseLossPer FrmProcessWiseLossPer = new FrmProcessWiseLossPer();
            FrmProcessWiseLossPer.MdiParent = this;
            FrmProcessWiseLossPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmProcessWiseLossPer.ShowForm(Masters.FrmProcessWiseLossPer.FormType.EMPLOCK);
        }

        private void PnlClientLogo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void packetGradeUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormPktGradeUpdate FormPktGradeUpdate = new FormPktGradeUpdate();
            FormPktGradeUpdate.MdiParent = this;
            FormPktGradeUpdate.Tag = ((ToolStripMenuItem)sender).Tag;
            FormPktGradeUpdate.ShowForm();
        }

        private void labourProcessMasterMakableToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabourProcessMakable FrmLabourProcessMakable = new FrmLabourProcessMakable();
            FrmLabourProcessMakable.MdiParent = this;
            FrmLabourProcessMakable.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabourProcessMakable.ShowForm();
        }

        private void penaltyIncentiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPenaltyIncentive FrmPenaltyIncentive = new FrmPenaltyIncentive();
            FrmPenaltyIncentive.MdiParent = this;
            FrmPenaltyIncentive.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPenaltyIncentive.ShowForm();
        }

        private void factoryLockIssueSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmFactoryIssueLockSetting FrmFactoryIssueLockSetting = new FrmFactoryIssueLockSetting();
            FrmFactoryIssueLockSetting.MdiParent = this;
            FrmFactoryIssueLockSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmFactoryIssueLockSetting.ShowForm();
        }

        private void kapanAutoMarkerSettingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanAutomarkerSetting FrmKapanAutomarkerSetting = new FrmKapanAutomarkerSetting();
            FrmKapanAutomarkerSetting.MdiParent = this;
            FrmKapanAutomarkerSetting.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanAutomarkerSetting.ShowForm();
        }

        private void fullKapanAssortmentReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmPuritySizeWCFGWiseReport FrmPuritySizeWCFGWiseReport = new FrmPuritySizeWCFGWiseReport();
            FrmPuritySizeWCFGWiseReport.MdiParent = this;
            FrmPuritySizeWCFGWiseReport.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmPuritySizeWCFGWiseReport.ShowForm();
        }

        private void singleTransactionToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void parcelToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void mastersToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void singleGoodTransferWithManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSingleGoodsTransferNewManagerWise FrmSingleGoodsTransferNewManagerWise = new FrmSingleGoodsTransferNewManagerWise();
            FrmSingleGoodsTransferNewManagerWise.MdiParent = this;
            FrmSingleGoodsTransferNewManagerWise.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmSingleGoodsTransferNewManagerWise.ShowForm();
        }

        private void kapanWiseLockLossAgeingNoOfIssueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmProcessWiseLossPer FrmProcessWiseLossPer = new FrmProcessWiseLossPer();
            FrmProcessWiseLossPer.MdiParent = this;
            FrmProcessWiseLossPer.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmProcessWiseLossPer.ShowForm(Masters.FrmProcessWiseLossPer.FormType.KAPAN);
        }

        private void cLientTicketToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmClientTicket FrmClientTicket = new FrmClientTicket();
            FrmClientTicket.MdiParent = this;
            FrmClientTicket.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmClientTicket.ShowForm();
        }

        private void plannigGradeMasterToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = this;
            FrmParameter.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmParameter.PlannigGradeShowForm("PLANNING GRADE");
        }

        private void labourProcessMasterPlannigToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmLabourProcessMakablePlanning FrmLabourProcessMakablePlanning = new FrmLabourProcessMakablePlanning();
            FrmLabourProcessMakablePlanning.MdiParent = this;
            FrmLabourProcessMakablePlanning.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmLabourProcessMakablePlanning.ShowForm();
        }

        private void heliumMumbaiGradingListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmHeliumMumbaiGradingDataView FrmHeliumMumbaiGradingDataView = new FrmHeliumMumbaiGradingDataView();
            FrmHeliumMumbaiGradingDataView.MdiParent = this;
            FrmHeliumMumbaiGradingDataView.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmHeliumMumbaiGradingDataView.ShowForm();
        }

        private void kapanCreationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmKapanCreation FrmKapanCreation = new FrmKapanCreation();
            FrmKapanCreation.MdiParent = this;
            FrmKapanCreation.Tag = ((ToolStripMenuItem)sender).Tag;
            FrmKapanCreation.ShowForm(0, "", "", Transaction.FrmKapanCreation.FORMTYPE.ORIGINALKAPAN);
        }
    }
}



