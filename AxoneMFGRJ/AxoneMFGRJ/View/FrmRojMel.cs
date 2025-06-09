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
using BusLib.Account;
using BusLib.Configuration;

namespace AxoneMFGRJ.Account
{
    public partial class FrmRojMel : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        BOLedgerTransaction objLedger = new BOLedgerTransaction();
        DataTable DTabReport = new DataTable();

        #region Constructor

        public FrmRojMel()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            //GetData();

            DTPFromDate.Value = DateTime.Now;
            DTPToDate.Value = DateTime.Now;

            DTabReport.Columns.Add(new DataColumn("ENTRYDATE", typeof(DateTime)));
            DTabReport.Columns.Add(new DataColumn("SRNO", typeof(int)));
            DTabReport.Columns.Add(new DataColumn("INCOMELEDGER_ID", typeof(Int64)));
            DTabReport.Columns.Add(new DataColumn("INCOMELEDGERNAME", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("INCOMEAMOUNT", typeof(string)));

            DTabReport.Columns.Add(new DataColumn("EXPENSELEDGER_ID", typeof(Int64)));
            DTabReport.Columns.Add(new DataColumn("EXPENSELEDGERNAME", typeof(string)));
            DTabReport.Columns.Add(new DataColumn("EXPENSEAMOUNT", typeof(string)));

            MainGrid.DataSource = DTabReport;
            MainGrid.RefreshDataSource();


            DataTable DTabParty = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LEDGERALL);

            ChkCmbParty.Properties.DataSource = DTabParty;
            ChkCmbParty.Properties.DisplayMember = "LEDGERNAME";
            ChkCmbParty.Properties.ValueMember = "LEDGER_ID";



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

                this.Cursor = Cursors.WaitCursor;

                DTabReport.Rows.Clear();

                string StrFromDate = DTPFromDate.Text;
                string StrToDate = DTPToDate.Text;

                string StrAccount = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());

                string StrTrnType = "";


                DataSet DS = objLedger.GetRojMelReport(Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), StrAccount, StrTrnType, false);

                DataTable DTabOpening = DS.Tables[0];
                DataTable DTabExpense = DS.Tables[1];
                DataTable DTabIncome = DS.Tables[2];
                DataTable DTabClosing = DS.Tables[3];

                double DouOpening = Val.Val(DTabOpening.Rows[0]["AMOUNT"]);

                DataRow DRNew = null;

                int IntSrNo = 0;

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["SRNO"] = 0;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);

                DouOpening = 0;

                foreach (DataRow DRow in DTabOpening.Rows)
                {
                    DRNew = DTabReport.NewRow();
                    DRNew["ENTRYDATE"] = StrFromDate;
                    DRNew["SRNO"] = 0;
                    DRNew["INCOMELEDGER_ID"] = 0;
                    DRNew["INCOMELEDGERNAME"] = "Opening " + Val.ToString(DRow["LedgerName"]);
                    DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DRow["AMOUNT"]);
                    DRNew["EXPENSELEDGER_ID"] = 0;
                    DRNew["EXPENSELEDGERNAME"] = "";
                    DRNew["EXPENSEAMOUNT"] = "";
                    DTabReport.Rows.Add(DRNew);
                    DouOpening = DouOpening + Val.Val(DRow["AMOUNT"]);
                }

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["SRNO"] = 0;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "Total";
                DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DouOpening);
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["SRNO"] = 0;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);

                double DouTotalIncome = 0;
                double DouTotalExpense = 0;

                IntSrNo = 0;
                if (DTabIncome.Rows.Count < DTabExpense.Rows.Count)
                {
                    int IntI = 0;
                    for (IntI = 0; IntI < DTabIncome.Rows.Count; IntI++)
                    {
                        IntSrNo++;

                        DRNew = DTabReport.NewRow();
                        DRNew["ENTRYDATE"] = StrFromDate;
                        DRNew["SRNO"] = IntSrNo;
                        DRNew["INCOMELEDGER_ID"] = Val.ToInt64(DTabIncome.Rows[IntI]["LEDGER_ID"]);
                        DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
                        DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DRNew["EXPENSELEDGER_ID"] = Val.ToInt64(DTabExpense.Rows[IntI]["LEDGER_ID"]);
                        DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
                        DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DTabExpense.Rows[IntI]["AMOUNT"]);
                        DTabReport.Rows.Add(DRNew);

                        DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);

                    }
                    for (; IntI < DTabExpense.Rows.Count; IntI++)
                    {
                        IntSrNo++;
                        DRNew = DTabReport.NewRow();
                        DRNew["ENTRYDATE"] = StrFromDate;
                        DRNew["SRNO"] = IntSrNo;
                        DRNew["INCOMELEDGER_ID"] = 0;
                        DRNew["INCOMELEDGERNAME"] = "";
                        DRNew["INCOMEAMOUNT"] = "";
                        DRNew["EXPENSELEDGER_ID"] = Val.ToInt64(DTabExpense.Rows[IntI]["LEDGER_ID"]);
                        DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
                        DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DTabExpense.Rows[IntI]["AMOUNT"]);
                        DTabReport.Rows.Add(DRNew);
                        DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);

                    }
                }

                else if (DTabIncome.Rows.Count > DTabExpense.Rows.Count)
                {
                    int IntI = 0;
                    for (IntI = 0; IntI < DTabExpense.Rows.Count; IntI++)
                    {
                        DRNew = DTabReport.NewRow();
                        DRNew["ENTRYDATE"] = StrFromDate;
                        DRNew["INCOMELEDGER_ID"] = Val.ToInt64(DTabIncome.Rows[IntI]["LEDGER_ID"]);
                        DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
                        DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DRNew["EXPENSELEDGER_ID"] = Val.ToInt64(DTabExpense.Rows[IntI]["LEDGER_ID"]);
                        DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
                        DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DTabExpense.Rows[IntI]["AMOUNT"]);
                        DTabReport.Rows.Add(DRNew);

                        DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
                    }
                    for (; IntI < DTabIncome.Rows.Count; IntI++)
                    {
                        DRNew = DTabReport.NewRow();
                        DRNew["ENTRYDATE"] = StrFromDate;
                        DRNew["INCOMELEDGER_ID"] = Val.ToInt64(DTabIncome.Rows[IntI]["LEDGER_ID"]);
                        DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
                        DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DRNew["EXPENSELEDGER_ID"] = 0;
                        DRNew["EXPENSELEDGERNAME"] = "";
                        DRNew["EXPENSEAMOUNT"] = "";
                        DTabReport.Rows.Add(DRNew);
                        DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
                    }
                }

                else if (DTabIncome.Rows.Count == DTabExpense.Rows.Count)
                {
                    int IntI = 0;
                    for (IntI = 0; IntI < DTabExpense.Rows.Count; IntI++)
                    {
                        DRNew = DTabReport.NewRow();
                        DRNew["ENTRYDATE"] = StrFromDate;
                        DRNew["INCOMELEDGER_ID"] = Val.ToInt64(DTabIncome.Rows[IntI]["LEDGER_ID"]);
                        DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
                        DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DRNew["EXPENSELEDGER_ID"] = Val.ToInt64(DTabExpense.Rows[IntI]["LEDGER_ID"]);
                        DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
                        DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DTabExpense.Rows[IntI]["AMOUNT"]);
                        DTabReport.Rows.Add(DRNew);

                        DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
                        DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
                    }
                }

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "Total Income";
                DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DouTotalIncome);
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "Total Expense";
                DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DouTotalExpense);
                DTabReport.Rows.Add(DRNew);

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);

                double DailyProfitLoss = Math.Round(DouOpening + DouTotalIncome - DouTotalExpense, 2);
                if (DailyProfitLoss < 0)
                {
                    DRNew = DTabReport.NewRow();
                    DRNew["ENTRYDATE"] = StrFromDate;
                    DRNew["INCOMELEDGER_ID"] = 0;
                    DRNew["INCOMELEDGERNAME"] = "Gross Loss (-)";
                    DRNew["INCOMEAMOUNT"] = Val.FormatWithCommaSeperator(DailyProfitLoss * -1);
                    DRNew["EXPENSELEDGER_ID"] = 0;
                    DRNew["EXPENSELEDGERNAME"] = "";
                    DRNew["EXPENSEAMOUNT"] = "";
                    DTabReport.Rows.Add(DRNew);
                }
                else
                {
                    DRNew = DTabReport.NewRow();
                    DRNew["ENTRYDATE"] = StrFromDate;
                    DRNew["INCOMELEDGER_ID"] = 0;
                    DRNew["INCOMELEDGERNAME"] = "";
                    DRNew["INCOMEAMOUNT"] = "";
                    DRNew["EXPENSELEDGER_ID"] = 0;
                    DRNew["EXPENSELEDGERNAME"] = "Gross Profit (+)";
                    DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DailyProfitLoss);
                    DTabReport.Rows.Add(DRNew);
                }

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = 0;
                DRNew["EXPENSELEDGERNAME"] = "";
                DRNew["EXPENSEAMOUNT"] = "";
                DTabReport.Rows.Add(DRNew);



                double DouClosing = 0;

                foreach (DataRow DRow in DTabClosing.Rows)
                {
                    DRNew = DTabReport.NewRow();
                    DRNew["ENTRYDATE"] = StrFromDate;
                    DRNew["SRNO"] = 0;
                    DRNew["INCOMELEDGER_ID"] = 0;
                    DRNew["INCOMELEDGERNAME"] = "";
                    DRNew["INCOMEAMOUNT"] = "";
                    DRNew["EXPENSELEDGER_ID"] = 0;
                    DRNew["EXPENSELEDGERNAME"] = "Closing " + Val.ToString(DRow["LedgerName"]);
                    DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DRow["AMOUNT"]);
                    DTabReport.Rows.Add(DRNew);
                    DouClosing = DouClosing + Val.Val(DRow["AMOUNT"]);
                }

                DRNew = DTabReport.NewRow();
                DRNew["ENTRYDATE"] = StrFromDate;
                DRNew["INCOMELEDGER_ID"] = 0;
                DRNew["INCOMELEDGERNAME"] = "";
                DRNew["INCOMEAMOUNT"] = "";
                DRNew["EXPENSELEDGER_ID"] = "0";
                DRNew["EXPENSELEDGERNAME"] = "Total";
                DRNew["EXPENSEAMOUNT"] = Val.FormatWithCommaSeperator(DouClosing);
                DTabReport.Rows.Add(DRNew);

                DTabReport.AcceptChanges();

                MainGrid.DataSource = DTabReport;
                MainGrid.RefreshDataSource();

                DTabOpening.Dispose();
                DTabIncome.Dispose();
                DTabExpense.Dispose();

                DTabOpening = null;
                DTabIncome = null;
                DTabExpense = null;


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

        private void BtnPrint_Click(object sender, EventArgs e)
        {

            //this.Cursor = Cursors.WaitCursor;

            //DataTable DTabDailyPrint = DTabReport.Clone();

            //DTabDailyPrint.Rows.Clear();

            //string StrFromDate = DTPFromDate.Text;
            //string StrToDate = DTPToDate.Text;
            //string StrAccount = Val.Trim(ChkCmbParty.Properties.GetCheckedItems());
            //string StrNote = Val.Trim(ChkCmbNote.Properties.GetCheckedItems());
            //string StrTrnType = Val.LRTrim(ChkCmbTrnType.Properties.GetCheckedItems());

            //while (DateTime.Parse(StrFromDate) <= DateTime.Parse(StrToDate))
            //{
            //    DataSet DS = objLedger.GetRojMelReport(Val.SqlDate(StrFromDate), Val.SqlDate(StrToDate), StrAccount, StrNote, StrTrnType);

            //    DataTable DTabOpening = DS.Tables[0];
            //    DataTable DTabExpense = DS.Tables[1];
            //    DataTable DTabIncome = DS.Tables[2];
            //    DataTable DTabClosing = DS.Tables[3];


            //    double DouOpening = Val.Val(DTabOpening.Rows[0]["AMOUNT"]);

            //    DataRow DRNew = null;

            //    int IntSrNo = 0;

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["SRNO"] = 0;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "";
            //    DRNew["INCOMEAMOUNT"] = 0;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "";
            //    DRNew["EXPENSEAMOUNT"] = 0;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    DouOpening = 0;

            //    foreach (DataRow DRow in DTabOpening.Rows)
            //    {
            //        DRNew = DTabDailyPrint.NewRow();
            //        DRNew["ENTRYDATE"] = StrFromDate;
            //        DRNew["SRNO"] = 0;
            //        DRNew["INCOMELEDGER_ID"] = 0;
            //        DRNew["INCOMELEDGERNAME"] = "Opening " + Val.ToString(DRow["LedgerName"]);
            //        DRNew["INCOMEAMOUNT"] = Val.Val(DRow["AMOUNT"]);
            //        DRNew["EXPENSELEDGER_ID"] = 0;
            //        DRNew["EXPENSELEDGERNAME"] = "";
            //        DRNew["EXPENSEAMOUNT"] = 0;
            //        DTabDailyPrint.Rows.Add(DRNew);
            //        DouOpening = DouOpening + Val.Val(DRow["AMOUNT"]);
            //    }

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["SRNO"] = 0;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "Total Opening";
            //    DRNew["INCOMEAMOUNT"] = DouOpening;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "";
            //    DRNew["EXPENSEAMOUNT"] = 0;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["SRNO"] = 0;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "";
            //    DRNew["INCOMEAMOUNT"] = 0;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "";
            //    DRNew["EXPENSEAMOUNT"] = 0;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    double DouTotalIncome = 0;
            //    double DouTotalExpense = 0;

            //    IntSrNo = 0;
            //    if (DTabIncome.Rows.Count < DTabExpense.Rows.Count)
            //    {
            //        int IntI = 0;
            //        for (IntI = 0; IntI < DTabIncome.Rows.Count; IntI++)
            //        {
            //            IntSrNo++;

            //            DRNew = DTabDailyPrint.NewRow();
            //            DRNew["ENTRYDATE"] = StrFromDate;
            //            DRNew["SRNO"] = IntSrNo;
            //            DRNew["INCOMELEDGER_ID"] = Val.ToInt(DTabIncome.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["INCOMEAMOUNT"] = Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DRNew["EXPENSELEDGER_ID"] = Val.ToInt(DTabExpense.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["EXPENSEAMOUNT"] = Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //            DTabDailyPrint.Rows.Add(DRNew);

            //            DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);

            //        }
            //        for (; IntI < DTabExpense.Rows.Count; IntI++)
            //        {
            //            IntSrNo++;
            //            DRNew = DTabDailyPrint.NewRow();
            //            DRNew["ENTRYDATE"] = StrFromDate;
            //            DRNew["SRNO"] = IntSrNo;
            //            DRNew["INCOMELEDGER_ID"] = 0;
            //            DRNew["INCOMELEDGERNAME"] = "";
            //            DRNew["INCOMEAMOUNT"] = 0;
            //            DRNew["EXPENSELEDGER_ID"] = Val.ToInt(DTabExpense.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["EXPENSEAMOUNT"] = Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //            DTabDailyPrint.Rows.Add(DRNew);
            //            DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);

            //        }
            //    }

            //    else if (DTabIncome.Rows.Count > DTabExpense.Rows.Count)
            //    {
            //        int IntI = 0;
            //        for (IntI = 0; IntI < DTabExpense.Rows.Count; IntI++)
            //        {
            //            DRNew = DTabDailyPrint.NewRow();
            //            DRNew["ENTRYDATE"] = StrFromDate;
            //            DRNew["INCOMELEDGER_ID"] = Val.ToInt(DTabIncome.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["INCOMEAMOUNT"] = Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DRNew["EXPENSELEDGER_ID"] = Val.ToInt(DTabExpense.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["EXPENSEAMOUNT"] = Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //            DTabDailyPrint.Rows.Add(DRNew);

            //            DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //        }
            //        for (; IntI < DTabIncome.Rows.Count; IntI++)
            //        {
            //            DRNew = DTabDailyPrint.NewRow();
            //            DRNew["ENTRYDATE"] = StrFromDate;
            //            DRNew["INCOMELEDGER_ID"] = Val.ToInt(DTabIncome.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["INCOMEAMOUNT"] = Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DRNew["EXPENSELEDGER_ID"] = 0;
            //            DRNew["EXPENSELEDGERNAME"] = "";
            //            DRNew["EXPENSEAMOUNT"] = 0;
            //            DTabDailyPrint.Rows.Add(DRNew);
            //            DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);

            //        }
            //    }

            //    else if (DTabIncome.Rows.Count == DTabExpense.Rows.Count)
            //    {
            //        int IntI = 0;
            //        for (IntI = 0; IntI < DTabExpense.Rows.Count; IntI++)
            //        {
            //            DRNew = DTabDailyPrint.NewRow();
            //            DRNew["ENTRYDATE"] = StrFromDate;
            //            DRNew["INCOMELEDGER_ID"] = Val.ToInt(DTabIncome.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["INCOMELEDGERNAME"] = Val.ToString(DTabIncome.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["INCOMEAMOUNT"] = Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DRNew["EXPENSELEDGER_ID"] = Val.ToInt(DTabExpense.Rows[IntI]["LEDGER_ID"]);
            //            DRNew["EXPENSELEDGERNAME"] = Val.ToString(DTabExpense.Rows[IntI]["LEDGERNAME"]);
            //            DRNew["EXPENSEAMOUNT"] = Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //            DTabDailyPrint.Rows.Add(DRNew);

            //            DouTotalIncome = DouTotalIncome + Val.Val(DTabIncome.Rows[IntI]["AMOUNT"]);
            //            DouTotalExpense = DouTotalExpense + Val.Val(DTabExpense.Rows[IntI]["AMOUNT"]);
            //        }
            //    }

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "";
            //    DRNew["INCOMEAMOUNT"] = 0;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "";
            //    DRNew["EXPENSEAMOUNT"] = 0;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "Total Income";
            //    DRNew["INCOMEAMOUNT"] = DouTotalIncome;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "Total Expense";
            //    DRNew["EXPENSEAMOUNT"] = DouTotalExpense;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "";
            //    DRNew["INCOMEAMOUNT"] = 0;
            //    DRNew["EXPENSELEDGER_ID"] = 0;
            //    DRNew["EXPENSELEDGERNAME"] = "";
            //    DRNew["EXPENSEAMOUNT"] = 0;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    double DouClosing = 0;

            //    foreach (DataRow DRow in DTabClosing.Rows)
            //    {
            //        DRNew = DTabDailyPrint.NewRow();
            //        DRNew["ENTRYDATE"] = StrFromDate;
            //        DRNew["SRNO"] = 0;
            //        DRNew["INCOMELEDGER_ID"] = 0;
            //        DRNew["INCOMELEDGERNAME"] = 0;
            //        DRNew["INCOMEAMOUNT"] = 0;
            //        DRNew["EXPENSELEDGER_ID"] = 0;
            //        DRNew["EXPENSELEDGERNAME"] = "Closing " + Val.ToString(DRow["LedgerName"]);
            //        DRNew["EXPENSEAMOUNT"] = Val.Val(DRow["AMOUNT"]);
            //        DTabDailyPrint.Rows.Add(DRNew);
            //        DouClosing = DouClosing + Val.Val(DRow["AMOUNT"]);
            //    }

            //    DRNew = DTabDailyPrint.NewRow();
            //    DRNew["ENTRYDATE"] = StrFromDate;
            //    DRNew["INCOMELEDGER_ID"] = 0;
            //    DRNew["INCOMELEDGERNAME"] = "";
            //    DRNew["INCOMEAMOUNT"] = 0;
            //    DRNew["EXPENSELEDGER_ID"] = "0";
            //    DRNew["EXPENSELEDGERNAME"] = "Total Closing";
            //    DRNew["EXPENSEAMOUNT"] = DouClosing;
            //    DTabDailyPrint.Rows.Add(DRNew);

            //    DTabOpening.Dispose();
            //    DTabIncome.Dispose();
            //    DTabExpense.Dispose();

            //    DTabOpening = null;
            //    DTabIncome = null;
            //    DTabExpense = null;

            //    StrFromDate = DateTime.Parse(StrFromDate).AddDays(1).ToString("dd/MM/yyyy");
            //}

            //DTabDailyPrint.AcceptChanges();

            //this.Cursor = Cursors.Default;


            //Report.FrmReportViewer FrmReportViewer = new Report.FrmReportViewer();

            //FrmReportViewer.ShowForm("ACC_RojmelDailyReport", DTabDailyPrint, 100, Report.FrmReportViewer.ReportFolder.NONE);
            //this.Cursor = Cursors.Default;


        }


        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("ROJMEL / રોજમેળ", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Filter
            TextBrick BrickTitlesFilter = e.Graph.DrawString("Date From : " + DTPFromDate.Text + " To " + DTPToDate.Text, System.Drawing.Color.Navy, new RectangleF(0, 45, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
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

        private void BtnBackWord_Click(object sender, EventArgs e)
        {
            string Str = DTPFromDate.Value.ToShortDateString();

            DTPFromDate.Text = DateTime.Parse(Str).AddDays(-1).ToString("dd/MM/yyyy");
            DTPToDate.Text = DateTime.Parse(Str).AddDays(-1).ToString("dd/MM/yyyy");
            BtnShow_Click(null, null);
        }

        private void BtnForward_Click(object sender, EventArgs e)
        {
            string Str = DTPFromDate.Value.ToShortDateString();

            DTPFromDate.Text = DateTime.Parse(Str).AddDays(1).ToString("dd/MM/yyyy");
            DTPToDate.Text = DateTime.Parse(Str).AddDays(1).ToString("dd/MM/yyyy");
            BtnShow_Click(null, null);

        }

        private void GrdDet_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }


            if (e.CellValue.ToString().ToUpper().Contains("TOTAL"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.BackColor = Color.FloralWhite;
                e.Appearance.BackColor2 = Color.FloralWhite;
            }

            else if (e.CellValue.ToString().ToUpper().Contains("PROFIT"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.DarkGreen;
            }
            else if (e.CellValue.ToString().ToUpper().Contains("LOSS"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.FromArgb(192, 0, 0);
            }

            else if (e.CellValue.ToString().ToUpper().Contains("OPENING") || e.CellValue.ToString().ToUpper().Contains("CLOSING"))
            {
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
                e.Appearance.ForeColor = Color.Green;
            }

        }

        private void BtnGPrint_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 100;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        private void FrmRojMel_FormClosing(object sender, FormClosingEventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }
    }
}