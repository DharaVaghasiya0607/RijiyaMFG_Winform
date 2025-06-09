using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Collections;
using System.Diagnostics;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;

namespace BusLib.Account
{
    public class BOLedgerTransaction
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper(Config.ConnectionString);
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Issue

        public string Save(ArrayList AL, Int64 pIntTrnID)
        {
            string Str = "";

            foreach (LedgerTransactionProperty pClsProperty in AL)
            {
                Ope.ClearParams();

                Ope.AddParams("Trn_ID", pClsProperty.Trn_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SrNo", pClsProperty.SrNo, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("EntryType", pClsProperty.EntryType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BookType", pClsProperty.BookType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BookTypeFull", pClsProperty.BookTypeFull, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FINYEAR_ID", Config.FINYEAR_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FINYEAR", Config.FINYEARNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VoucherNo", pClsProperty.VoucherNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("VoucherStr", pClsProperty.VoucherStr, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VoucherDate", pClsProperty.VoucherDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("Ledger_ID", pClsProperty.Ledger_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LedgerName", pClsProperty.LedgerName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RefLedger_ID", pClsProperty.RefLedger_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RefLedgerName", pClsProperty.RefLedgerName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("Amount", pClsProperty.Amount, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ExcRate", pClsProperty.ExcRate, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FAmount", pClsProperty.FAmount, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("Currency_ID", pClsProperty.Currency_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("PendingAmount", pClsProperty.PendingAmount, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PendingAmountFE", pClsProperty.PendingAmountFE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("SubType", pClsProperty.SubType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RefDocNo", pClsProperty.RefDocNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ChequeNo", pClsProperty.ChequeNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("Note", pClsProperty.Note, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EntryDate", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EntryIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TrnType", pClsProperty.TrnType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("Memo_ID", pClsProperty.MEMO_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("INVCURRENCY_ID", pClsProperty.InvCurrency_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("INVEXCRATE", pClsProperty.InvExcRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("EXCRATEDIFF", pClsProperty.ExcRateDiff, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("BANKCHARGES", pClsProperty.BankCharges, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("BANKCHARGESFE", pClsProperty.BankChargesFE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PAYMENTTYPE", pClsProperty.PAYMENTTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONVERTTOINR", pClsProperty.CONVERTTOINR, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("XMLPAYMENTDETAIL", pClsProperty.XMLPAYMENTDETAIL, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList arryList = new ArrayList();
                arryList = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Acc_LedgerTransactionSave", CommandType.StoredProcedure);
                if (arryList.Count != 0)
                {
                    pClsProperty.ReturnValueTrnID = Val.ToString(arryList[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(arryList[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(arryList[2]);

                    Str = pClsProperty.ReturnMessageDesc;

                    pIntTrnID = Val.ToInt64(pClsProperty.ReturnValueTrnID);
                }

            }

            return Str;
        }

        public Int64 FindVoucherNo(string pStrFinYearID, string pStrBookType)
        {
            string StrSql = " And BOOKTYPE = '" + pStrBookType + "'";
            Ope.ClearParams();
            Int64 IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "ACC_LedgerTransaction", "MAX(VOUCHERNO)", StrSql);

            return IntNewID;
        }

        public bool ISExistsVoucherNo(Int64 pIntVoucherNo, string pStrBookType)
        {
            string StrSql = " And  Company_ID='" + Config.gEmployeeProperty.COMPANY_ID + "' AND BOOKTYPE = '" + pStrBookType + "' And VOUCHERNO='" + pIntVoucherNo + "'";
            Ope.ClearParams();
            string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "ACC_LedgerTransaction", "VOUCHERNO", StrSql);
            if (Str == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable GetSummary(string pStrFromDate,string pStrToDate,string pStrBook,string pStrTrnType)
        {
            Ope.ClearParams();
            
            DataTable DTabSummary = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("BOOKTYPE", pStrBook, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.String, ParameterDirection.Input);
                           
            Ope.FillDTab(Config.ConnectionString,Config.ProviderName,DTabSummary,"Acc_LedgerTransactionGetSummary", CommandType.StoredProcedure);
            return DTabSummary;
        }



        public double FindLedgerClosing(Int64 pIntLedgerID)
        {
            Ope.ClearParams();

            Ope.AddParams("Ledger_ID", pIntLedgerID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
              
            DataRow DRow = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName,"Acc_FindLedgerClosing", CommandType.StoredProcedure);

            if (DRow == null)
            {
                return 0;
            }

            return Val.Val(DRow[0]);
        }

        public DataTable GetDetail(Int64 pIntTrnID)
        {
            Ope.ClearParams();

            DataTable DTabSummary = new DataTable();

            Ope.AddParams("TRN_ID", pIntTrnID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabSummary, "Acc_LedgerTransactionGetDetail", CommandType.StoredProcedure);
            return DTabSummary;

        }


        public DataTable FindBankAndCashBalance()
        {
            Ope.ClearParams();

            DataTable DTabSummary = new DataTable();
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabSummary, "Acc_FindBankAndCashBalance", CommandType.StoredProcedure);
            return DTabSummary;

        }


        public DataSet HomePageDashboard()
        {
            Ope.ClearParams();

            DataSet DTabSummary = new DataSet();
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DTabSummary, "Temp", "Acc_HomePageDashboard", CommandType.StoredProcedure);
            return DTabSummary;

        }

        public int Delete(Int64 pIntTrnID)
        {
            Ope.ClearParams();
            Ope.AddParams("TRN_ID", pIntTrnID, DbType.Int64, ParameterDirection.Input);
                
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName,"Acc_LedgerTransactionDelete", CommandType.StoredProcedure);

        }


        public DataSet GetRojMelReport(string pStrFromDate, string pStrToDate, string pStrAccount, string pStrTrnType, bool ChkGujaratiFlag)
        {
            DataSet DS = new DataSet();
            Ope.ClearParams();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("PARTY", pStrAccount, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ISGUJARATI", ChkGujaratiFlag, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp1", "Acc_RPRojMelGetData", CommandType.StoredProcedure);

            return DS;
        }


        public DataSet GetLedgerReport(bool  pBoolCashBank, string pStrFromDate, string pStrToDate, Int64 pIntAccount, Int64 pIntRefAccount, string pStrTrnType, bool ChkGujaratiFlag)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("ISBANKCASH", pBoolCashBank, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("LEDGER_ID", pIntAccount, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("REFLEDGER_ID", pIntRefAccount, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ISGUJARATI", ChkGujaratiFlag, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp1", "Acc_RPLedgerReportGetData", CommandType.StoredProcedure);

            return DS;
        }


        public DataTable GetGSTReport(string pStrFromDate, string pStrToDate, string pStrTrnType,string pStrGroupBy)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("GROUPBY", pStrGroupBy, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_RPGSTReportGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetTrialBalanceReport(string pStrFromDate, string pStrToDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_RPTrialBalanceReport", CommandType.StoredProcedure);

            return DTab;
        }


        public DataTable GetPivotReportAnalysis(string pStrFromDate, string pStrToDate, string pStrTrnType, string pStrColumnArea, string pStrDataArea, string pStrInvoiceType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ROWAREA", pStrColumnArea, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DATAAREA", pStrDataArea, DbType.String, ParameterDirection.Input);
            Ope.AddParams("INVOICETYPE", pStrInvoiceType, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_RPPivotReportGetData", CommandType.StoredProcedure);

            return DTab;
        }



        public DataTable GetInvoiceMasterDetailReport(string pStrFromDate, string pStrToDate, string pStrTrnType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_RPInvoiceReportGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetDataAsReturnFormat(string pStrFromDate, string pStrToDate, string pStrTrnType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_RPGSTExcelFormatGetData", CommandType.StoredProcedure);

            return DTab;
        }

        //public DataTable GetBillWiseOutstanding(Int64 pIntTrnID, Int64 pIntLedgerID)
        //{
        //    Ope.ClearParams();

        //    DataTable DTabBillDetail = new DataTable();
        //    Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("TRN_ID", pIntTrnID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("LEDGER_ID", pIntLedgerID, DbType.Int64, ParameterDirection.Input);

        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabBillDetail, "Acc_LedgerPendingBillGetData", CommandType.StoredProcedure);
        //    return DTabBillDetail;

        //}

        public DataTable FindLedgerClosingAmt(Int64 pGuidInvoice_ID, String pStrPaymentType) // Dhara : 01-12-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("INVOICE_ID", pGuidInvoice_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_FindLedgerClosing", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable FindLedgerInvoice(Int64 pIntLedgerID, string pStrPaymentType) // Dhara : 01-12-2022 Find Invoice For Get Bill Data
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("Ledger_ID", pIntLedgerID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_FindInvoice", CommandType.StoredProcedure);
            return DTab;
        }

        //public DataTable GetBillWiseOutstandingNew(Guid pGuidLedgerID, Guid pGuidTrnId, string pStrEntryDate, Int32 CURRENCY_ID, string pStrPaymentType = "") // Dhara : 01-12-2022
        //{
        //    Ope.ClearParams();

        //    DataTable DTabBillDetail = new DataTable();

        //    Ope.AddParams("LEDGER_ID", pGuidLedgerID, DbType.Guid, ParameterDirection.Input);
        //    Ope.AddParams("TRN_ID", pGuidTrnId, DbType.Guid, ParameterDirection.Input);
        //    Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("ENTRYDATE", pStrEntryDate, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("CURRENCY_ID", CURRENCY_ID, DbType.Int32, ParameterDirection.Input);

        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabBillDetail, "Acc_LedgerPendingBillGetDatanew", CommandType.StoredProcedure);

        //    return DTabBillDetail;

        //}

        public DataTable GetBillWiseOutstanding(Int64 PGuidInvoice_ID, Int64 pIntLedgerID, string pStrPaymentType, string jangedNostr = "") // Dhara : 01-12-2022
        {
            Ope.ClearParams();

            DataTable DTabBillDetail = new DataTable();

            Ope.AddParams("LEDGER_ID", pIntLedgerID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNOSTR", jangedNostr, DbType.String, ParameterDirection.Input);
            Ope.AddParams("INVOICE_ID", PGuidInvoice_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabBillDetail, "Acc_LedgerPendingBillGetData", CommandType.StoredProcedure);
            return DTabBillDetail;

        }

        public DataTable GetBillWisePaymentGetData(string pStrOpe, string pStrFromDate, string pStrToDate, string pStrBookType, int pIntVoucherNo) // 01-12-2022
        {
            Ope.ClearParams();

            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

            Ope.AddParams("BOOKTYPE", pStrBookType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("VOUCHERNO", pIntVoucherNo, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_BillWisePaymentGetData", CommandType.StoredProcedure);

            return DTab;

        }

        public DataTable FindLedgerParty(string pStrLedgerType, string pStr) //Dhara:01-02-2023
        {
            Ope.ClearParams();

            DataTable DTabSummary = new DataTable();

            Ope.AddParams("LedgerCategory", pStrLedgerType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Str", pStr, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabSummary, "Acc_FindLedgerParty", CommandType.StoredProcedure);
            return DTabSummary;

        }

        public DataTable GetBillWiseOutstandingNew(Guid pGuidLedgerID, Guid pGuidTrnId, string pStrEntryDate, Int32 CURRENCY_ID, string pStrPaymentType = "") // Dhara : 01-02-2023
        {
            Ope.ClearParams();

            DataTable DTabBillDetail = new DataTable();

            Ope.AddParams("LEDGER_ID", pGuidLedgerID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("TRN_ID", pGuidTrnId, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYDATE", pStrEntryDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("CURRENCY_ID", CURRENCY_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabBillDetail, "Acc_LedgerPendingBillGetDatanew", CommandType.StoredProcedure);

            return DTabBillDetail;

        }

        public DataTable FindLedgerClosingAmt(Guid pGuidInvoice_ID, String pStrPaymentType) // Dhara : 01-02-2023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("INVOICE_ID", pGuidInvoice_ID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("PAYMENTTYPE", pStrPaymentType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Acc_FindLedgerClosing", CommandType.StoredProcedure);
            return DTab;
        }

        public LedgerTransactionProperty Delete(LedgerTransactionProperty pClsProperty) // Dhara : 01-02-2023
        {
            Ope.ClearParams();

            Ope.AddParams("TRN_ID", pClsProperty.Trn_ID, DbType.Guid, ParameterDirection.Input);
            //Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
            //Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


            ArrayList AL = new ArrayList();
            AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Acc_LedgerTransactionDelete", CommandType.StoredProcedure);
            if (AL.Count != 0)
            {
                pClsProperty.ReturnValueTrnID = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);

            }
            return pClsProperty;
        }




        #endregion
    }
}
