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

namespace AxoneMFGRJ.Account
{
    public partial class FrmLedgerReport : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOLedgerTransaction objLedger = new BOLedgerTransaction();
        
        DataTable DTabReport = new DataTable();

        double mDouOpening = 0;
        double OpeningBalance = 0;
        double DouRunningBalance = 0;

        #region Constructor

        public FrmLedgerReport()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            //GetData();

            DTPFromDate.Text = DateTime.Now.AddMonths(-1).ToShortDateString();
            DTPToDate.Text = DateTime.Now.ToShortDateString();

            DTabReport.Columns.Add(new DataColumn("TRN_ID", typeof(long)));
            DTabReport.Columns.Add(new DataColumn("BOOKTYPEFULL", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("VOUCHERDATE", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("ENTRYTYPE", typeof(string)));

            DTabReport.Columns.Add(new DataColumn("VOUCHERSTR", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("LEDGERNAME", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("REFLEDGERNAME", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("CREDITAMOUNTLOCAL", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("DEBITAMOUNTLOCAL", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("RUNNINGBALANCE", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("TRNTYPE", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("NOTE", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("REFDOC", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("DISCOUNT", typeof(double)));
            DTabReport.Columns.Add(new DataColumn("OPENING_BALANCE", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("PROPERTYNAME", typeof(string)));
            
            MainGrid.DataSource = DTabReport;
            MainGrid.RefreshDataSource();

            ChkCashBank_CheckedChanged(null, null);
            txtAccount.Focus();
        }

        private void AttachFormDefaultEvent()
        {
            this.KeyPreview = true;
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = false;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(objLedger);
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

                if (ChkCashBank.Checked == false)
                {
                    if (Val.ToString(txtAccount.Tag).ToString() == "")
                    {
                        Global.Message("ACCOUNT NAME IS REQUIRED FOR STATEMENT");
                        txtAccount.Focus();
                        return;
                    }
                }
                else if (ChkCashBank.Checked == true)
                {
                    txtAccount.Tag = "";
                    txtAccount.Text = "";
                }
                
                if (txtRefLedger.Text.Length == 0)
                {
                    txtRefLedger.Tag = "";
                }

                this.Cursor = Cursors.WaitCursor;

                DTabReport.Rows.Clear();

                string StrFromDate = DTPFromDate.Text;
                string StrToDate = DTPToDate.Text;

                Int64 IntAccount = Val.ToInt64(txtAccount.Tag);
                Int64 IntRefAccount = Val.ToInt64(txtRefLedger.Tag);

                string StrTrnType = "";// Val.Trim(ChkCmbTrnType.Properties.GetCheckedItems());


                DataSet DS = objLedger.GetLedgerReport(ChkCashBank.Checked, Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), IntAccount, IntRefAccount, StrTrnType, ChkLanguage.Checked);

                DataTable DTabOpening = DS.Tables[0];
                DataTable DTabTransaction = DS.Tables[1];

                double DouOpening = Val.Val(DTabOpening.Rows[0]["AMOUNT"]);

                mDouOpening = DouOpening;

                int IntSrNo = 1;
                DataRow DRNew = null;

                OpeningBalance = DouOpening;

                DRNew = DTabReport.NewRow();
                DRNew["TRN_ID"] = 0;

                DRNew["BOOKTYPEFULL"] = "";
                DRNew["VOUCHERDATE"] = DBNull.Value;

                DRNew["ENTRYTYPE"] = "";
                if (ChkLanguage.Checked)
                {
                    DRNew["VOUCHERSTR"] = Val.GetGujaratiNumber(IntSrNo.ToString());
                    DRNew["LEDGERNAME"] = "પેહલા નું બેલેન્સ";
                    DRNew["REFLEDGERNAME"] = "પેહલા નું બેલેન્સ";
                }
                else
                {
                    DRNew["VOUCHERSTR"] = IntSrNo;
                    DRNew["LEDGERNAME"] = "Opening Balance";
                    DRNew["REFLEDGERNAME"] = "Opening Balance";
                }
                DRNew["DISCOUNT"] = 0;
                DRNew["PROPERTYNAME"] = "";
                if (DouOpening < 0)
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouOpening * -1));
                    }
                    else
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouOpening * -1);
                    }
                }
                else
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouOpening));
                    }
                    else
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouOpening);
                    }
                }

                if (ChkLanguage.Checked)
                {
                    DRNew["RUNNINGBALANCE"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperatorWithCreditDebit(DouOpening));
                }
                else
                {
                    DRNew["RUNNINGBALANCE"] = Val.FormatWithCommaSeperatorWithCreditDebit(DouOpening);
                }

                DRNew["TRNTYPE"] = "";
                DRNew["NOTE"] = "";

                DTabReport.Rows.Add(DRNew);

                double DouTotalCredit = 0;
                double DouTotalDebit = 0;

                DouRunningBalance = DouOpening;

                foreach (DataRow DRow in DTabTransaction.Rows)
                {
                    IntSrNo++;

                    DRNew = DTabReport.NewRow();

                    if (ChkLanguage.Checked)
                    {
                        DRNew["VOUCHERSTR"] = Val.GetGujaratiNumber(IntSrNo.ToString());

                        if (Val.ToString(DRow["BOOKTYPEFULL"]).Contains("BP - BANK PAYMENT")) DRNew["BOOKTYPEFULL"] = "બેંક જાવક";
                        else if (Val.ToString(DRow["BOOKTYPEFULL"]).Contains("BR - BANK RECEIPT")) DRNew["BOOKTYPEFULL"] = "બેંક આવક";
                        else if (Val.ToString(DRow["BOOKTYPEFULL"]).Contains("CP - CASH PAYMENT")) DRNew["BOOKTYPEFULL"] = "કેશ જાવક";
                        else if (Val.ToString(DRow["BOOKTYPEFULL"]).Contains("CR - CASH RECEIPT")) DRNew["BOOKTYPEFULL"] = "કેશ આવક";
                        else if (Val.ToString(DRow["BOOKTYPEFULL"]).Contains("CO - CONTRA")) DRNew["BOOKTYPEFULL"] = "સેલ્ફ ટ્રાન્સફર";

                        DRNew["VOUCHERDATE"] = Val.GetGujaratiNumber(Val.ToDate(DRow["VOUCHERDATE"], AxonDataLib.BOConversion.DateFormat.DDMMYYYY));

                        DRNew["ENTRYTYPE"] = DRow["ENTRYTYPE"];

                        DRNew["CREDITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DRow["CREDITAMOUNT"]));
                        DRNew["DEBITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DRow["DEBITAMOUNT"]));

                        DouRunningBalance = Math.Round(DouRunningBalance + (Val.Val(DRow["CREDITAMOUNT"]) - Val.Val(DRow["DEBITAMOUNT"])), 2);

                        DRNew["RUNNINGBALANCE"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperatorWithCreditDebit(DouRunningBalance));

                    }
                    else
                    {
                        DRNew["VOUCHERSTR"] = IntSrNo.ToString();

                        DRNew["BOOKTYPEFULL"] = DRow["BOOKTYPEFULL"];

                        DRNew["VOUCHERDATE"] = Val.ToDate(DRow["VOUCHERDATE"], AxonDataLib.BOConversion.DateFormat.DDMMYYYY);

                        DRNew["ENTRYTYPE"] = DRow["ENTRYTYPE"];

                        DRNew["CREDITAMOUNTLOCAL"] = DRow["CREDITAMOUNT"];
                        DRNew["DEBITAMOUNTLOCAL"] = DRow["DEBITAMOUNT"];

                        DouRunningBalance = Math.Round(DouRunningBalance + (Val.Val(DRow["CREDITAMOUNT"]) - Val.Val(DRow["DEBITAMOUNT"])), 2);

                        DRNew["RUNNINGBALANCE"] = Val.FormatWithCommaSeperatorWithCreditDebit(DouRunningBalance);

                    }
                    DRNew["TRN_ID"] = DRow["TRN_ID"];
                    DRNew["REFDOC"] = DRow["REFDOC"];
                    DRNew["LEDGERNAME"] = DRow["LEDGERNAME"];
                    DRNew["REFLEDGERNAME"] = DRow["REFLEDGERNAME"];
                    DRNew["OPENING_BALANCE"] = 0;
                    DRNew["TRNTYPE"] = DRow["TRNTYPE"];
                    DRNew["NOTE"] = DRow["NOTE"];
                    DTabReport.Rows.Add(DRNew);

                    DouTotalCredit = Math.Round(DouTotalCredit + Val.Val(DRow["CREDITAMOUNT"]), 2);
                    DouTotalDebit = Math.Round(DouTotalDebit + Val.Val(DRow["DEBITAMOUNT"]), 2);
                }

                DTabReport.Rows.Add(DTabReport.NewRow());

                IntSrNo++;
                //opening

                DRNew = DTabReport.NewRow();
                DRNew["TRN_ID"] = 0;

                DRNew["BOOKTYPEFULL"] = "";
                DRNew["VOUCHERDATE"] = DBNull.Value;

                DRNew["ENTRYTYPE"] = "";
                if (ChkLanguage.Checked)
                {
                    DRNew["VOUCHERSTR"] = Val.GetGujaratiNumber(IntSrNo.ToString());
                    DRNew["LEDGERNAME"] = "પેહલા નું બેલેન્સ";
                    DRNew["REFLEDGERNAME"] = "પેહલા નું બેલેન્સ";
                }
                else
                {
                    DRNew["VOUCHERSTR"] = IntSrNo;
                    DRNew["LEDGERNAME"] = "Opening Balance";
                    DRNew["REFLEDGERNAME"] = "Opening Balance";
                }
                DRNew["DISCOUNT"] = 0;
                DRNew["PROPERTYNAME"] = "";
                if (DouOpening < 0)
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouOpening * -1));
                    }
                    else
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouOpening * -1);
                    }
                }
                else
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouOpening));
                    }
                    else
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouOpening);
                    }
                }

                if (ChkLanguage.Checked)
                {
                    DRNew["RUNNINGBALANCE"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperatorWithCreditDebit(DouOpening));
                }
                else
                {
                    DRNew["RUNNINGBALANCE"] = Val.FormatWithCommaSeperatorWithCreditDebit(DouOpening);
                }

                DRNew["TRNTYPE"] = "";
                DRNew["NOTE"] = "";
                DTabReport.Rows.Add(DRNew);

                IntSrNo++;
                //TOTAL CREDIT AND DEBIT

                DRNew = DTabReport.NewRow();
                DRNew["TRN_ID"] = 0;

                DRNew["BOOKTYPEFULL"] = "";
                DRNew["VOUCHERDATE"] = DBNull.Value;

                DRNew["ENTRYTYPE"] = "";
                if (ChkLanguage.Checked)
                {
                    DRNew["VOUCHERSTR"] = Val.GetGujaratiNumber(IntSrNo.ToString());
                    DRNew["LEDGERNAME"] = "ટોટલ આવક/જાવક";
                    DRNew["REFLEDGERNAME"] = "ટોટલ આવક/જાવક";
                }
                else
                {
                    DRNew["VOUCHERSTR"] = IntSrNo;
                    DRNew["LEDGERNAME"] = "Total Credit Debit";
                    DRNew["REFLEDGERNAME"] = "Total Credit Debit";
                }
                DRNew["DISCOUNT"] = 0;
                DRNew["PROPERTYNAME"] = "";
                if (ChkLanguage.Checked)
                {
                    DRNew["DEBITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouTotalDebit));
                    DRNew["CREDITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouTotalCredit));
                }
                else
                {
                    DRNew["DEBITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouTotalDebit);
                    DRNew["CREDITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouTotalCredit);
                }
                DRNew["RUNNINGBALANCE"] = "";
                DRNew["TRNTYPE"] = "";
                DRNew["NOTE"] = "";
                DTabReport.Rows.Add(DRNew);

                IntSrNo++;
                //Closing

                DRNew = DTabReport.NewRow();
                DRNew["TRN_ID"] = 0;

                DRNew["BOOKTYPEFULL"] = "";
                DRNew["VOUCHERDATE"] = DBNull.Value;
                DRNew["VOUCHERSTR"] = "";
                DRNew["ENTRYTYPE"] = "";
                if (ChkLanguage.Checked)
                {
                    DRNew["VOUCHERSTR"] = Val.GetGujaratiNumber(IntSrNo.ToString());
                    DRNew["LEDGERNAME"] = "કુલ ટોટલ";
                    DRNew["REFLEDGERNAME"] = "કુલ ટોટલ";
                }
                else
                {
                    DRNew["VOUCHERSTR"] = IntSrNo.ToString();
                    DRNew["LEDGERNAME"] = "Closing Balance";
                    DRNew["REFLEDGERNAME"] = "Closing Balance";
                }
                DRNew["DISCOUNT"] = 0;
                DRNew["PROPERTYNAME"] = "";
                if (DouOpening < 0)
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouRunningBalance * -1));
                    }
                    else
                    {
                        DRNew["DEBITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouRunningBalance * -1);
                    }
                }
                else
                {
                    if (ChkLanguage.Checked)
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperator(DouRunningBalance));
                    }
                    else
                    {
                        DRNew["CREDITAMOUNTLOCAL"] = Val.FormatWithCommaSeperator(DouRunningBalance);
                    }
                }

                if (ChkLanguage.Checked)
                {
                    DRNew["RUNNINGBALANCE"] = Val.GetGujaratiNumber(Val.FormatWithCommaSeperatorWithCreditDebit(DouRunningBalance));
                }
                else
                {
                    DRNew["RUNNINGBALANCE"] = Val.FormatWithCommaSeperatorWithCreditDebit(DouRunningBalance);
                }

                DRNew["TRNTYPE"] = "";
                DRNew["NOTE"] = "";

                DTabReport.Rows.Add(DRNew);

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

    

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Ledger Report", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Filter

            TextBrick BrickTitlesParam = e.Graph.DrawString("Account / ખાતું :- " + txtAccount.Text + " & Ref AC / સામે નું ખાતું : " + txtRefLedger.Text, System.Drawing.Color.Navy, new RectangleF(0, 45, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;

            // ' For Filter
            TextBrick BrickTitlesFilter = e.Graph.DrawString("Date From :- " + DTPFromDate.Text + " To :- " + DTPToDate.Text, System.Drawing.Color.Navy, new RectangleF(0, 65, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesFilter.Font = new Font("Verdana", 9, FontStyle.Bold);
            BrickTitlesFilter.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesFilter.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesFilter.ForeColor = Color.Black;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Navy, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold); ;
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnExcelExport_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = "RojMel";
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    GrdDet.ExportToXlsx(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        
        private void txtAccount_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERALL);
                    
                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtAccount.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]) + " - " + Val.ToString(FrmSearch.mDRow["LEDGERNAMEGUJARATI"]);
                        txtAccount.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);

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

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (GrdDet.GetRowCellValue(e.RowHandle,"REFLEDGERNAME").ToString().ToUpper().Contains("TOTAL"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.FromArgb(192, 0, 0);
            }

            else if (GrdDet.GetRowCellValue(e.RowHandle, "REFLEDGERNAME").ToString().ToUpper().Contains("OPENING"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }

            else if (GrdDet.GetRowCellValue(e.RowHandle, "REFLEDGERNAME").ToString().ToUpper().Contains("CLOSING"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Red;
            }
            
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.Clicks == 2 && e.Column.FieldName.ToUpper() == "VOUCHERSTR")
            {

            }
        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = false;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 130;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        private void GrdDet_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.DisplayText == "0.00" || e.DisplayText == "0" || e.DisplayText == "0.000")
            {
                e.DisplayText = String.Empty;
                // e.Appearance.ForeColor = System.Drawing.Color.White;
            }
        }

        private void GrdDet_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
           
        }

        private void ChkBankCash_CheckedChanged(object sender, EventArgs e)
        {
            txtAccount.Text = "";
            txtAccount.Tag = "";

        }

        private void GrdDet_CustomRowFilter(object sender, DevExpress.XtraGrid.Views.Base.RowFilterEventArgs e)
        {
            //GridView view = sender as GridView;
            //DataView dv = view.DataSource as DataView;

            //DataTable DTab = dv.ToTable();

            //double DouCredit = 0;
            //double DouDebit = 0;

            //foreach (DataRow DRow in DTab.Rows)
            //{
            //    if (Val.Trim(DRow["BOOKTYPEFULL"]) != "")
            //    {
            //        DouCredit = DouCredit + Val.Val(DRow["CREDITAMOUNTLOCAL"]);
            //        DouDebit = DouDebit + Val.Val(DRow["DEBITAMOUNTLOCAL"]);
            //    }
            //}

            //double DouClosing = mDouOpening + DouDebit - DouCredit;

            //txtOpening.Text = Val.FormatWithCommaSeperatorWithCreditDebit(mDouOpening);
            //txtCredit.Text = Val.FormatWithCommaSeperator(DouCredit);
            //txtDebit.Text = Val.FormatWithCommaSeperator(DouDebit);
            //txtClosing.Text = Val.FormatWithCommaSeperatorWithCreditDebit(DouClosing);


        }

        private void GrdDet_CustomSummaryCalculate(object sender, DevExpress.Data.CustomSummaryEventArgs e)
        {
            if (e.SummaryProcess == CustomSummaryProcess.Start)
            {
                //mDouAmount = 0;
                //mDouCarat = 0;
            }
            else if (e.SummaryProcess == CustomSummaryProcess.Calculate)
            {
                //mDouCarat = mDouCarat + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "Import_Carat"));
                //mDouAmount = mDouAmount + Val.Val(GrdDet.GetRowCellValue(e.RowHandle, "Cost_Amount_Dollar"));
            }

            else if (e.SummaryProcess == CustomSummaryProcess.Finalize)
            {
                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("RUNNINGBALANCE") == 0)
                {
                    e.TotalValue = Val.FormatWithCommaSeperatorWithCreditDebit(Val.Format(Math.Round(DouRunningBalance, 2), "######0.00"));
                }

                if (((DevExpress.XtraGrid.GridSummaryItem)e.Item).FieldName.CompareTo("OPENING_BALANCE") == 0)
                {
                    e.TotalValue = Val.FormatWithCommaSeperatorWithCreditDebit(Val.Format(Math.Round(DouRunningBalance, 2), "######0.00"));
                }

            }
        }

        private void ChkLanguage_CheckedChanged(object sender, EventArgs e)
        {
            if (ChkLanguage.Checked == true)
            {
                ChkLanguage.Text = "Convert To English";
                
                GrdDet.Columns["VOUCHERDATE"].Caption = "તારીખ";
                GrdDet.Columns["BOOKTYPEFULL"].Caption = "બુક";
                GrdDet.Columns["VOUCHERSTR"].Caption = "નં";
                GrdDet.Columns["REFLEDGERNAME"].Caption = "ખાતું";
                GrdDet.Columns["NOTE"].Caption = "ટીપ્પણી";
                GrdDet.Columns["CREDITAMOUNTLOCAL"].Caption = "આવક";
                GrdDet.Columns["DEBITAMOUNTLOCAL"].Caption = "જાવક";
                GrdDet.Columns["RUNNINGBALANCE"].Caption = "ટોટલ";
                GrdDet.Columns["REFDOC"].Caption = "સ્લીપ નં";


                GrdDet.Appearance.Row.Font = new Font(GrdDet.Appearance.Row.Font.FontFamily, float.Parse("12"));
            }
            else
            {
                ChkLanguage.Text = "Convert To ગુજરાતી";

                GrdDet.Columns["VOUCHERDATE"].Caption = "Date";
                GrdDet.Columns["BOOKTYPEFULL"].Caption = "Book";
                GrdDet.Columns["VOUCHERSTR"].Caption = "Sr.";
                GrdDet.Columns["REFLEDGERNAME"].Caption = "Account";
                GrdDet.Columns["NOTE"].Caption = "Note";
                GrdDet.Columns["CREDITAMOUNTLOCAL"].Caption = "Credit";
                GrdDet.Columns["DEBITAMOUNTLOCAL"].Caption = "Debit";
                GrdDet.Columns["RUNNINGBALANCE"].Caption = "Running";
                GrdDet.Columns["REFDOC"].Caption = "Ref Doc";


                GrdDet.Appearance.Row.Font = new Font(GrdDet.Appearance.Row.Font.FontFamily, float.Parse("10"));
            }
            BtnShow_Click(null, null);
        }

        private void txtRefLedger_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LEDGERCODE,LEDGERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERNONCASH);

                    FrmSearch.mColumnsToHide = "LEDGER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtRefLedger.Text = Val.ToString(FrmSearch.mDRow["LEDGERNAME"]) + " - " + Val.ToString(FrmSearch.mDRow["LEDGERNAMEGUJARATI"]);
                        txtRefLedger.Tag = Val.ToString(FrmSearch.mDRow["LEDGER_ID"]);

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

        private void FrmLedgerReport_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            GrdDet.BestFitColumns();
        }

        private void ChkCashBank_CheckedChanged(object sender, EventArgs e)
        {
            //GrdDet.Columns["LEDGERNAME"].Visible = ChkCashBank.Checked;
        }


    }
}