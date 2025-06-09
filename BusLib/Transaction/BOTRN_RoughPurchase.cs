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
    public class BOTRN_RoughPurchase
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public TrnRoughPurchaseProperty Delete(TrnRoughPurchaseProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseDelete", CommandType.StoredProcedure);

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
        
        public DataTable GetDataForPurchaseLiveStock(string FormType,bool pBoolDispAllLot, String RoughType, string pStrKapan)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALLLOT", pBoolDispAllLot, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ROUGHTYPE", RoughType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FORMTYPE",FormType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PurchaseLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetLotIDTracking(Int64 pIntLotID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("LOT_ID", pIntLotID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_PurchaseLiveStockGetDataOfLotIDTracking", CommandType.StoredProcedure);
            return DS;
        }

        public DataTable GetMixSplitHistory(string pStrOpe, string pStrFromDate, string pStrToDate,Int64 pIntTrnID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TRN_ID", pIntTrnID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RoughPurchaseMasterDetailSaveMixSplitGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public TrnRoughPurchaseProperty MixSplitHistoryDelete(TrnRoughPurchaseProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailSaveMixSplitDelete", CommandType.StoredProcedure);
            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;
        }

        public TrnRoughPurchaseProperty MixSplitHistoryDeleteSingle(TrnRoughPurchaseProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("OPERATION", pClsProperty.OPERATION, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LOTNO", pClsProperty.LOTNO, DbType.String, ParameterDirection.Input);
            Ope.AddParams("REFLOT_ID", pClsProperty.REFLOT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("REFLOTNO", pClsProperty.REFLOTNO, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SRNO", pClsProperty.SRNO, DbType.Int32, ParameterDirection.Input);
            
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughPurchaseMasterDetailSaveMixSplitDeleteSingle", CommandType.StoredProcedure);
            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;
        }

        //public DataRow GetOrgAndBalCarat(Int64 pIntLotID)
        //{
        //    string StrQuery = "";

        //    Ope.ClearParams();

        //    StrQuery = "SELECT CARAT,BALANCECARAT,RATE,GROSSBROKRATE,EXCRATE FROM TRN_IMPORTDETAIL WITH(NOLOCK) WHERE LOT_ID = '" + pIntLotID + "'";

        //    DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

        //    return Dr;
        //}

        public DataRow GetOrgAndBalCarat(Int64 pIntLotID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = @"SELECT a.NEWBALANCECARAT as BALANCECARAT,b.* FROM VIW_LOTBALANCE A  WITH(NOLOCK) 
                    INNER JOIN TRN_ImportDetail b WITH(NOLOCK)  ON A.Lot_ID = B.Lot_ID WHERE a.LOT_ID = '" + pIntLotID + "'";

            //StrQuery = "SELECT *  FROM TRN_ImportDetail WITH(NOLOCK) WHERE LOT_ID = '" + pIntLotID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }

        public string GetLastCreateKapanName() //#P : 13-10-2022 : Used In KapanCreate Module
        {
            string StrQuery = "";
            
            Ope.ClearParams();

            StrQuery = "SELECT TOP 1 KAPANNAME FROM TRN_KAPAN WITH(NOLOCK) ORDER BY ENTRYDATE DESC";

            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
        }


        public bool ISExists(string pStrKapanName)
        {

            string S = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_KAPAN", "KAPANNAME", " And KAPANNAME='" + pStrKapanName + "'");
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
        public DataTable GetDataForAssortFromAssort1(string pGuidRough_ID) //#P : 03-12-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("ROUGH_ID", pGuidRough_ID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RoughPurchaseAssortDataFromAssortGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public int SaveCompleteRough(bool pBoolIsComplete, Guid pGuidLotID)
        {
            string pStrRes = "";

            Ope.ClearParams();
            pStrRes = "Update TRN_Import With(RowLock) Set ISCOMPLETE ='" + pBoolIsComplete + "' WHERE LOT_ID='" + pGuidLotID + "'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, pStrRes, CommandType.Text);
        }
        public DataTable GetRoughCostingReport(string pStrFromDate, string pStrToDate)// urvisha(26/11/2022)...
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_PurchaseCostingReport", CommandType.StoredProcedure);

            return DTab;
        }
        public RoughUpdateProperty UpdateRough(RoughUpdateProperty pClsProperty)
        {
            Ope.AddParams("ISCOMPLETE", pClsProperty.ISCOMPLETE, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("COMPLETEDATE", pClsProperty.COMPLETEDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int16, ParameterDirection.Input);

            Ope.AddParams("UPDATEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("UPDATEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_UpdateRough", CommandType.StoredProcedure);
            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;

        }


    }
}
