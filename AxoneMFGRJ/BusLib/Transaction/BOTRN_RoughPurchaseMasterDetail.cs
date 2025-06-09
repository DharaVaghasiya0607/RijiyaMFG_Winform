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
    public class BOTRN_RoughPurchaseMasterDetail
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetDataForRoughPurchase(string pStrFormType,string pStrFromInvoiceDate,string pStrToInvoiceDate,string pStrFromReceiveDate,string pStrToReceiveDate,string pStrPartyID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FormType", pStrFormType, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FromInvoiceDate", pStrFromInvoiceDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToInvoiceDate", pStrToInvoiceDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FromReceiveDate", pStrFromReceiveDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToReceiveDate", pStrToReceiveDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("Party_ID", pStrPartyID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Invoice_ID", 0, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RoughPurchaseMasterDetailGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetInvoiceDetail(string pFormType,Int64 pIntInvoiceID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("FormType", pFormType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Invoice_ID", pIntInvoiceID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS,"Temp", "Trn_RoughPurchaseMasterDetailGetData", CommandType.StoredProcedure);
            return DS;
        }

        public DataRow FindNewInvoiceNo(string pStrReceiveDate, string pStrRoughType, string pStrFormType)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("RECEIVEDATE", pStrReceiveDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ROUGHTYPE", pStrRoughType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailFindInvoiceNo", CommandType.StoredProcedure);
        }

        public TrnRoughPurchaseProperty Save(TrnRoughPurchaseProperty pClsProperty, string pStrRoughDetailXml)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RECEIVEDATE", pClsProperty.RECEIVEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("INVOICEYEAR", pClsProperty.INVOICEYEAR, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("INVOICEMONTH", pClsProperty.INVOICEMONTH, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("INVOICENO", pClsProperty.INVOICENO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYSTEMINVOICENO", pClsProperty.SYSTEMINVOICENO, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MANUALINVOICENO", pClsProperty.MANUALINVOICENO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTYINVOICENO", pClsProperty.PARTYINVOICENO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTYINVOICEDATE", pClsProperty.PARTYINVOICEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TERMSDAYS", pClsProperty.TERMSDAYS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PAYMENTDATE", pClsProperty.PAYMENTDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("OTHERCOMPANY", pClsProperty.OTHERCOMPANY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EXCRATE", pClsProperty.EXCRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PURCHASETYPE", pClsProperty.PURCHASETYPE, DbType.String, ParameterDirection.Input);
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
                Ope.AddParams("ROUGHTYPE", pClsProperty.ROUGHTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FORMTYPE", pClsProperty.FORMTYPE, DbType.String, ParameterDirection.Input);
               
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailSave", CommandType.StoredProcedure);
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


        public TrnRoughPurchaseProperty SaveMixSplit(TrnRoughPurchaseProperty pClsProperty,string pStrOpe)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("FROMLOTS", pClsProperty.FROMLOTS, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("TOLOTS", pClsProperty.TOLOTS, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("EXCRATE", pClsProperty.EXCRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailSaveMixSplit", CommandType.StoredProcedure);
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

        public TrnRoughPurchaseProperty Delete(TrnRoughPurchaseProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                
                Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailDelete", CommandType.StoredProcedure);

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
        public bool ISExistsSystemInvoiceNo(string pStrSystemInvoiceNo, string pRoughType, string FpFormtype)
        {

            string S = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_Import", "SystemInvoiceNo", " And SystemInvoiceNo='" + pStrSystemInvoiceNo + "' AND RoughType = '" + pRoughType + "' and FormType = '" + FpFormtype + "' ");
            if (S.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }


        public DataTable GetDataForPurchaseLiveStock(bool pBoolDispAllLot)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALLLOT", pBoolDispAllLot, DbType.Boolean, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PurchaseLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataRow GetOrgAndBalCarat(Guid pIntLotID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT CARAT,BALANCECARAT,RATE,RESULTPER,EXPPER,ROUGHCOSTWITHDALALI FROM TRN_IMPORT WITH(NOLOCK) WHERE LOT_ID = '" + pIntLotID + "'";

            DataRow Dr= Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            
            return Dr;
        }

        public bool ISExists(string pStrKapanName)
        {
            
            string S = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_KAPAN", "KAPANNAME", " And KAPANNAME='"+pStrKapanName+"'");
            if (S.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataSet GetActualMakAndPolishData()
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_CalculateResultAndExpCarat", CommandType.StoredProcedure);

            return DS;
        }

        public int SavePaymentStatus(string pStrPaymentStatus, string pGuidLotID) //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Update TRN_Import With(RowLock) Set PAYMENTSTATUS='" + pStrPaymentStatus + "' WHERE LOT_ID='" + pGuidLotID + "'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }

        public DataTable GetDataForPurchaseLiveStock()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PurchaseLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForAssortFromAssort(string pGuidRough_ID) //#P : 03-12-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("ROUGH_ID", pGuidRough_ID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RoughPurchaseAssortDataFromAssortGetData", CommandType.StoredProcedure);
            return DTab;
        }
    }
}
