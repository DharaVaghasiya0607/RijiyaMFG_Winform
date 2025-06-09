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
    public class BOInvoiceEntry
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Issue

        public string Save(TrnInvoiceProperty pClsProperty , ArrayList AL)
        {
            string Str = "";

            pClsProperty = Delete(pClsProperty);

            if (pClsProperty.ReturnMessageType == "FAIL")
            {
                return pClsProperty.ReturnValueTrnID;
            }

            Ope.ClearParams();

            Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("INVOICETYPE", pClsProperty.INVOICETYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pClsProperty.TRNTYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           
            Ope.AddParams("FINYEAR", pClsProperty.FINYEAR, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BILLDATE", pClsProperty.BILLDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("BILLNO", pClsProperty.BILLNO, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CASHCREDIT", pClsProperty.CASHCREDIT, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BILLINGLEDGER_ID", pClsProperty.BILLINGLEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SHIPPINGLEDGER_ID", pClsProperty.SHIPPINGLEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("BILLINGLEDGERNAME", pClsProperty.BILLINGLEDGERNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHIPPINGLEDGERNAME", pClsProperty.SHIPPINGLEDGERNAME, DbType.String, ParameterDirection.Input);

            Ope.AddParams("REFNO1", pClsProperty.REFNO1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("REFNO2", pClsProperty.REFNO2, DbType.String, ParameterDirection.Input);

            Ope.AddParams("PAYMENTDAYS", pClsProperty.PAYMENTDAYS, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PAYMENTDATE", pClsProperty.PAYMENTDATE, DbType.Date, ParameterDirection.Input);

            Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BILLINGADDRESS", pClsProperty.BILLINGADDRESS, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BILLINGSTATE", pClsProperty.BILLINGSTATE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHIPPINGADDRESS", pClsProperty.SHIPPINGADDRESS, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHIPPINGSTATE", pClsProperty.SHIPPINGSTATE, DbType.String, ParameterDirection.Input);
           
            Ope.AddParams("GROSSAMOUNT", pClsProperty.GROSSAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("IGSTAMOUNT", pClsProperty.IGSTAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("CGSTAMOUNT", pClsProperty.CGSTAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("SGSTAMOUNT", pClsProperty.SGSTAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("TOTALGSTAMOUNT", pClsProperty.TOTALGSTAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("TOTALWITHGSTAMOUNT", pClsProperty.TOTALWITHGSTAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("ADDLESSAMOUNT", pClsProperty.ADDLESSAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("NETAMOUNT", pClsProperty.NETAMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
            
            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList arryList = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_InvoiceSave", CommandType.StoredProcedure);

            if (arryList != null && arryList.Count != 0)
            {
                pClsProperty.ReturnValueTrnID = Val.ToString(arryList[0]);
                pClsProperty.ReturnMessageType = Val.ToString(arryList[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(arryList[2]);

                Str = Val.ToString(pClsProperty.ReturnValueTrnID);

                pClsProperty.INVOICE_ID = Val.ToInt64(pClsProperty.ReturnValueTrnID);

                if (pClsProperty.ReturnMessageType == "SUCCESS" && pClsProperty.INVOICE_ID != 0)
                {
                    foreach (TrnInvoiceDetailProperty propdetail in AL)
                    {
                        Ope.ClearParams();
                        Ope.AddParams("LOT_ID", propdetail.LOT_ID, DbType.Int64, ParameterDirection.Input);
                        Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.String, ParameterDirection.Input);
                        Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                        Ope.AddParams("ITEM_ID", propdetail.ITEM_ID, DbType.String, ParameterDirection.Input);
                        Ope.AddParams("ITEMDESCRIPTION", propdetail.ITEMDESCRIPTION, DbType.String, ParameterDirection.Input);
                        Ope.AddParams("SRNO", propdetail.SRNO, DbType.String, ParameterDirection.Input);
                        Ope.AddParams("QTY", propdetail.QTY, DbType.Date, ParameterDirection.Input);
                        Ope.AddParams("RATE", propdetail.RATE, DbType.Int32, ParameterDirection.Input);
                        Ope.AddParams("AMOUNT", propdetail.AMOUNT, DbType.String, ParameterDirection.Input);
                        Ope.AddParams("IGSTPER", propdetail.IGSTPER, DbType.Int64, ParameterDirection.Input);
                        Ope.AddParams("IGSTAMOUNT", propdetail.IGSTAMOUNT, DbType.Int32, ParameterDirection.Input);
                        Ope.AddParams("CGSTPER", propdetail.CGSTPER, DbType.Date, ParameterDirection.Input);
                        Ope.AddParams("CGSTAMOUNT", propdetail.CGSTAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("SGSTPER", propdetail.SGSTPER, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("SGSTAMOUNT", propdetail.SGSTAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("TOTALGSTAMOUNT", propdetail.TOTALGSTAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("TOTALWITHGSTAMOUNT", propdetail.TOTALWITHGSTAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("ADDLESSAMOUNT", propdetail.ADDLESSAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("OTHERCHARGES", propdetail.OTHERCHARGES, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("NETAMOUNT", propdetail.NETAMOUNT, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("REMARK", propdetail.REMARK, DbType.Double, ParameterDirection.Input);
                        Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                        Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                        Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                        Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                        Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                        Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_InvoiceDetailSave", CommandType.StoredProcedure);

                    }
                }
            }

            return Str;
        }

        public Int64 FindBillNo(string pStrFinYearID, string pStrBookType,string pStrInvoiceType)
        {
            string StrSql = "  And Company_ID='" + Config.gEmployeeProperty.COMPANY_ID + "' And InvoiceType='" + pStrInvoiceType + "' And TrnType = '" + pStrBookType + "'";
            Ope.ClearParams();
            Int64 IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "Trn_Invoice", "MAX(BillNo)", StrSql);

            return IntNewID;
        }

        public bool ISExists(string pStrFinYearID, string pStrBookType, int pIntBillNo, string pStrInvoiceType)
        {
            string StrSql = "  And Company_ID='" + Config.gEmployeeProperty.COMPANY_ID + "' And InvoiceType='" + pStrInvoiceType + "' And TrnType = '" + pStrBookType + "' And BillNo = '" + pIntBillNo + "'";
            Ope.ClearParams();
            string s = Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_Invoice", "BillNo", StrSql);

            if (s != "")
            {
                return true;
            }
            return false;

        }


        public DataTable FillInvoiceDetail(Int64 pIntInvoiceID)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("INVOICE_ID", pIntInvoiceID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab,"Trn_InvoiceGetData", CommandType.StoredProcedure);

            return DTab;
        }

       
        public DataTable FillInvoicePrint(Int64 pIntInvoiceID)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("INVOICE_ID", pIntInvoiceID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_InvoicePrint", CommandType.StoredProcedure);

            return DTab;
        }


        public DataTable FillInvoiceData(string pStrFromDate,string pStrToDate,string pStrTrnType)
        {
            Ope.ClearParams();

            DataTable DTabDetail = new DataTable();

            Ope.AddParams("INVOICE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabDetail, "Trn_InvoiceGetData", CommandType.StoredProcedure);
            return DTabDetail;

        }


        public DataTable FillItemLiveStock(string pStrAsOnDate)
        {
            Ope.ClearParams();

            DataTable DTabDetail = new DataTable();

            Ope.AddParams("ASONDATE", pStrAsOnDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabDetail, "ItemLiveStock", CommandType.StoredProcedure);
            return DTabDetail;

        }

        public TrnInvoiceProperty Delete(TrnInvoiceProperty pClsProperty)
        {
            Ope.ClearParams();
            Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList arryList = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_InvoiceDelete", CommandType.StoredProcedure);
            if (arryList.Count != 0)
            {
                pClsProperty.ReturnValueTrnID = Val.ToString(arryList[0]);
                pClsProperty.ReturnMessageType = Val.ToString(arryList[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(arryList[2]);
            }
            return pClsProperty;
        }



        public DataRow FindInvoiceMasterData(int pIntBillNo, string pStrInvoiceType, string pStrTrnType)
        {
            Ope.ClearParams();

            DataTable DTabDetail = new DataTable();

            Ope.AddParams("BILL_NO", pIntBillNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TRNTYPE", pStrTrnType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("INVOICETYPE", pStrInvoiceType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_InvoiceGetData", CommandType.StoredProcedure);
            
        }



        #endregion

        #region Production Master


        public DataTable FillProductionSummary(string pStrFromDate, string pStrToDate)
        {
            Ope.ClearParams();

            DataTable DTabDetail = new DataTable();

            Ope.AddParams("PRODUCTIONDATE", null, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabDetail, "TRN_ProductionGetData", CommandType.StoredProcedure);
            return DTabDetail;

        }

        public DataTable FillProductionDetail(string pStrProductionDate)
        {
            Ope.ClearParams();

            DataTable DTabDetail = new DataTable();

            Ope.AddParams("PRODUCTIONDATE", pStrProductionDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", null, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", null, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTabDetail, "TRN_ProductionGetData", CommandType.StoredProcedure);
            return DTabDetail;

        }

        public TrnInvoiceDetailProperty ProductionSave(TrnInvoiceDetailProperty pClsProperty)
        {
            Ope.ClearParams();

            Ope.AddParams("PROD_ID", pClsProperty.PROD_ID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("PRODUCTIONDATE", pClsProperty.PRODUCTIONDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           
            Ope.AddParams("YYYYMMDD", pClsProperty.YYYYMMDD, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("SRNO", pClsProperty.SRNO, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PRODUCTION", pClsProperty.PRODUCTION, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("HEAD", pClsProperty.HEAD, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList arryList = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_ProductionSave", CommandType.StoredProcedure);
            if (arryList.Count != 0)
            {
                pClsProperty.ReturnValueTrnID = Val.ToString(arryList[0]);
                pClsProperty.ReturnMessageType = Val.ToString(arryList[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(arryList[2]);
            }
            return pClsProperty;
        }


        public TrnInvoiceDetailProperty ProductionDelete(TrnInvoiceDetailProperty pClsProperty)
        {
            Ope.ClearParams();

            Ope.AddParams("PRODUCTIONDATE", pClsProperty.PRODUCTIONDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList arryList = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_ProductionDelete", CommandType.StoredProcedure);
            if (arryList.Count != 0)
            {
                pClsProperty.ReturnValueTrnID = Val.ToString(arryList[0]);
                pClsProperty.ReturnMessageType = Val.ToString(arryList[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(arryList[2]);
            }
            return pClsProperty;
        }
        #endregion
    }
}
