using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;

namespace BusLib.Transaction
{
    public class BOTRN_SaleMasterDetail
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetDataForRoughPurchase(string pStrFromInvoiceDate,string pStrToInvoiceDate,string pStrPartyID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FromInvoiceDate", pStrFromInvoiceDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToInvoiceDate", pStrToInvoiceDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("Party_ID", pStrPartyID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Invoice_ID", 0, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SaleMasterDetailGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetInvoiceDetail(Int64 pIntInvoiceID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("Invoice_ID", pIntInvoiceID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS,"Temp", "Trn_SaleMasterDetailGetData", CommandType.StoredProcedure);
            return DS;
        }

        public int FindNewInvoiceNo(int IntYear,int IntMonth)
        {
            return Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "TRN_Sale", "MAX(InvoiceNo)", " And InvoiceYear = " + IntYear + " And InvoiceMonth=" + IntMonth + "");
        }

        public TrnSaleProperty Save(TrnSaleProperty pClsProperty, string pStrRoughDetailXml)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("INVOICEDATE", pClsProperty.INVOICEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("INVOICEYEAR", pClsProperty.INVOICEYEAR, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("INVOICEMONTH", pClsProperty.INVOICEMONTH, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("INVOICENO", pClsProperty.INVOICENO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYSTEMINVOICENO", pClsProperty.SYSTEMINVOICENO, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MANUALINVOICENO", pClsProperty.MANUALINVOICENO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TERMSDAYS", pClsProperty.TERMSDAYS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PAYMENTDATE", pClsProperty.PAYMENTDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("OTHERCOMPANY", pClsProperty.OTHERCOMPANY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EXCRATE", pClsProperty.EXCRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("SALETYPE", pClsProperty.SALETYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CURRENCYTYPE", pClsProperty.CURRENCYTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PAYMENTTYPE", pClsProperty.PAYMENTTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TERMSPER", pClsProperty.TERMSPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TERMSAMOUNT", pClsProperty.TERMSAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPER1", pClsProperty.EXPPER1, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPAMOUNT1", pClsProperty.EXPAMOUNT1, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPER2", pClsProperty.EXPPER2, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPAMOUNT2", pClsProperty.EXPAMOUNT2, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPER3", pClsProperty.EXPPER3, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPAMOUNT3", pClsProperty.EXPAMOUNT3, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPER4", pClsProperty.EXPPER4, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPAMOUNT4", pClsProperty.EXPAMOUNT4, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ADDLESSPER", pClsProperty.ADDLESSPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ADDLESSAMOUNT", pClsProperty.ADDLESSAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("NETAMOUNT", pClsProperty.NETAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXTRAINTERESTPER", pClsProperty.EXTRAINTERESTPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("INVOICEREMARK", pClsProperty.INVOICEREMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("YOURREMARK", pClsProperty.YOURREMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLFORROUGHDETAIL", pClsProperty.XMLFORROUGHDETAIL, DbType.Xml, ParameterDirection.Input);
               
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SaleMasterDetailSave", CommandType.StoredProcedure);
                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                pClsProperty.ReturnValue = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = ex.Message;

            }
            return pClsProperty;

        }


        public TrnSaleProperty Delete(TrnSaleProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                
                Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SALE_ID", pClsProperty.SALE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SaleMasterDetailDelete", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }

            }
            catch (System.Exception ex)
            {
                pClsProperty.ReturnValue = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = ex.Message;

            }
            return pClsProperty;
        }
        public bool ISExistsSystemInvoiceNo(string pStrSystemInvoiceNo)
        {

            string S = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_Sale", "SystemInvoiceNo", " And SystemInvoiceNo='" + pStrSystemInvoiceNo + "'");
            if (S.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
