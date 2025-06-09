using System;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;


namespace BusLib.ReportGrid
{
    public class BOTRN_PredictionView
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        #region Other Function

        public DataSet PredictionViewGetData(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("MAINTAG", pStrMainTag, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("PARENTTAG", pStrParentTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePredictionViewNew", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataSet PredictionViewGetDataNew(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType, string pStrFromDate, string pStrToDate, string pStrOpe, Int64 pIntEmployeeID)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePrdViewComparision", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataSet PredictionPCNPAcketsViewGetData(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType, string pStrFromDate, string pStrToDate, string pStrOpe, Int64 pIntEmployeeID) //Add : Pinali : 17-10-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePrdPCNPacketsView", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataSet PredictionViewGetDataForAdmin(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType, string pStrFromDate, string pStrToDate, string pStrOpe, Int64 pIntEmployeeID, string pStrPrdTypeOther,
                                                       string strType)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PrdTypeOther_ID", pStrPrdTypeOther, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TYPE", strType, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePrdViewComparisionAdmin", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }



        public DataSet PredictionViewGetDataMarker(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType, string pStrFromDate, string pStrToDate, string pStrOpe, Int64 pIntEmployeeID)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePrdViewComparisionMarker", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable PredictionViewSummaryForMarkerSummary(string pStrOpe, string pStrKapan, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SinglePrdViewComparisionMarkerWiseSummary", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable DTabPredictionDataForManagement(string pStrKapan, int pIntPacketNo, string pStrTag)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SinglePrdViewForManagement", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataSet DTabPredictionDataForMarker(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, Int64 pIntEmployeeID, string pStrPrdType, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "temp", "RP_SinglePrdViewForMarker", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable PriceRevisedForFinalChecker(string pStrKapan, int pStrPacketNo, string pStrTag, string pStrRapdate, Int64 pStrEmployee_ID)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pStrPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RAPDATE", pStrRapdate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SinglePrdViewLatestRapCalc", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataSet DTabMarkerProcessPredictionDetail(string pStrDeptType, string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, Int64 pIntEmployeeID, string pStrPrdType, string pStrFromDate, string pStrToDate) //Add : Pinali : 26-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("DEPARTMENTTYPE", pStrDeptType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "temp", "RP_SingleMarkerProcessPredictionDetail", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataSet DTabBreakingDiffProcessPredictionDetail(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, Int64 pIntEmployeeID, string pStrPrdType, string pStrFromDate, string pStrToDate) //Add : Pinali : 01-12-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "temp", "RP_SingleBreakingDiffProcessPredictionDetail", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable PriceRevised(string pStrOpe, string pStrKapan, string pStrPrdType_ID, string pStrRapDate)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RAPDATE", pStrRapDate, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Job_SinglePriceRevised", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetDataForArtistPrediction(string pStrKapan, int pIntPacketNo, string pStrTag, Int64 pIntEmployeeID, string pStrPrdType, string pStrFromDate, string pStrToDate) //Add : Pinali : 07-06-2019
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleArtistPredictionList", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetDataForArtistPredictionPacketWise(string pStrKapan, int pIntPacketNo, string pStrTag) //Add : Pinali : 07-06-2019
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                //return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "RP_SingleArtistPredictionList", CommandType.StoredProcedure);            
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleArtistPredictionList", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public int UpdatePrdTFlag(Int64 pIntPrd_ID, Int64 pIntEmployee_ID, int pIntPlanNo, int pIntPrdType_ID,string pStrFormType) //Add : Pinali : 27-09-2019
        {
            try
            {
                int IntRet = 0;

                Ope.ClearParams();
                Ope.AddParams("PRD_ID", pIntPrd_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PLANNO", pIntPlanNo , DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pIntPrdType_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RETURNVALUE", 0, DbType.Int32, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdUpdateTFlag", CommandType.StoredProcedure);

                if (AL.Count > 0)
                {
                    IntRet = Val.ToInt32(AL[0]);
                }
                return IntRet;
            }
            catch (Exception ex)
            {
                return -5;
            }
        }

        //End As

        public DataTable HeliumGetDataView(
           string pStrOpe,
           string pStrKapan,
           int pIntFromPacketNo,
           int pIntToPacketNo,
           string pStrTag,
           Int64 pIntEmployeeID,
           string pStrFromDate,
           string pStrToDate
           )
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_HeliumViewGetData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        #endregion

        #region GIA Live Stock

        public DataSet GIALiveStock_GetData(string pStrKapan, int pIntPacketNo, string pStrTag, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMISSUEDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TOISSUEDATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_SinglePacketGIALiveStockGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public int GIALiveStock_ItemStatusSave(DataTable pDTabItemStatus)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("TBL_GIAItemStatus", pDTabItemStatus, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", BusLib.Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_GIAItemStatusSave", CommandType.StoredProcedure);

            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public DataTable GIALiveStock_GetAdvanceInforation(string pStrPacketID)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pStrPacketID, DbType.Guid, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_GIAItemAdvanceInfo", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public int GIA_SaveHistory(string MethodName, string InputXML, string OutputXML)
        {
            try
            {
                string Str = "Insert Into HST_GIAEventLog With(RowLock) (MethodName,InputXML,OutputXML,EntryDate,EntryBy,EntryIP) Values";
                Str = Str + "('" + MethodName + "',LEFT('" + InputXML + "',8000),LEFT('" + OutputXML + "',8000),GETDATE(),'" + BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID + "','" + BusLib.Configuration.BOConfiguration.ComputerIP + "')";
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public int GIALiveStock_GIAActionSave(string pStrControlNo, string pStrGIAAction, string pStrGIAResponse, int pIntServiceID, int pIntRecheckID,
                 string pStrInscriptionText,
                 string pStrClientComment,
                 string pStrReturnDate
             )
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("CONTROLNO", pStrControlNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("GIAACTION", pStrGIAAction, DbType.String, ParameterDirection.Input);
                Ope.AddParams("GIARESPONSE", pStrGIAResponse, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SERVICE_ID", pIntServiceID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RECHECK_ID", pIntRecheckID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("INSCRIPTIONTEXT", pStrInscriptionText, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLIENTCOMMENT", pStrClientComment, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RETURNDATE", pStrReturnDate, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", BusLib.Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_GIAActionSave", CommandType.StoredProcedure);

            }
            catch (Exception Ex)
            {
                return -1;
            }
        }
        public DateTime GIALiveStock_GetMaxReceivedDate()
        {
            try
            {
                Ope.ClearParams();
                string Str = "Select MAX(RECEIVEDDATE) AS RECEIVEDDATE From TRN_GIAItemReceived With(NOLOCK)";
                DataRow DR = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
                if (DR == null)
                {
                    return DateTime.Now;
                }
                else
                {
                    return DateTime.Parse(DR["RECEIVEDDATE"].ToString());
                    //return DateTime.Parse("21/12/2020 12:00:00 AM".ToString());
                }
            }
            catch (Exception Ex)
            {
                return DateTime.Now;
            }
        }
        public int GIALiveStock_ItemReceivedSave(DataTable pDTabItemStatus, string pStrReceivedDate)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("TBL_GIAItemItemsReceived", pDTabItemStatus, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", BusLib.Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RECEIVEDDATE", pStrReceivedDate, DbType.Date, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_GIAItemReceivedSave", CommandType.StoredProcedure);

            }
            catch (Exception Ex)
            {
                return -1;
            }
        }
        public DataSet PredictionViewForGradingGetData(string pStrKapan, int pIntFromPacketNo, int pIntToPacketNo, string pStrTag, string pStrParentTag, string pStrMainTag, string pStrPrdType, string pStrFromDate, string pStrToDate, string pStrOpe, Int64 pIntEmployeeID, string pStrStoneno)
        {
            try
            {
                DataSet DT = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("STONENO", pStrStoneno, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DT, "Temp", "RP_PridictionViewForGrading", CommandType.StoredProcedure);
                return DT;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable PrdMakLogGetData(string pStrKapan, int pStrPacketNo, string pStrTag, string pStrFromDate, string pStrToDate)// D: 19-11-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pStrPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePrdMakLogGetData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        #endregion
    }
}

