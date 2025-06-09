using System;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;
using System.IO;


namespace BusLib.View
{
    public class BOTRN_RunninPossition
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        #region Other Function

        //Add Milan 05-03-2022
        public DataSet PlanningReportAssoerterWiseGetData(string strFromDate, string strToDate)
        {
            try
            {
                DataSet DTab = new DataSet();

                Ope.ClearParams();
                //Ope.AddParams("FROMDATE", strFromDate, DbType.Date, ParameterDirection.Input);
                //Ope.AddParams("TODATE", strToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DTab, "Table", "RP_PlanningReportAssorterWiseGetData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        //End

        public DataSet RunningPosstionData(string pStrFormType, string pStrStockCategory, string pStrStockType, string mStrKapan, Int32 mIntPacketNo, string pStrTag, bool pIsWithExtraStock, string pStrProcess_ID, string pStrMainManager_ID, string pStrsubgruop)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKCATEGORY", pStrStockCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKTYPE", pStrStockType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", mStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", mIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pStrProcess_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBGROUP", pStrsubgruop, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleRunningPossitionNew", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleRunningPossitionNew_PrcWise", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataSet RunningPosstionDataDepartmentWise(string pStrFormType, string pStrStockCategory, string pStrStockType, string mStrKapan, Int32 mIntPacketNo, string pStrTag, bool pIsWithExtraStock) //#P : 20-02-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKCATEGORY", pStrStockCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKTYPE", pStrStockType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", mStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", mIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleRunningPossitionDepartmentWise", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }



        public DataTable RunningPosstionDataWIPPivotReport(string pStrStockCategory, string pStrStockType, string mStrKapan, Int32 mIntPacketNo, string pStrTag)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("STOCKCATEGORY", pStrStockCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKTYPE", pStrStockType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", mStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", mIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleRunningPossitionProcessWisePivot", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }



        public DataSet FullKapanAnalysis(string pStrKapan, string pStrOpe, string pStrFromDate, string pStrToDate, bool pIsWithPCNStock, string pStrGrdType, string pStrMainManager_ID, string pStrPacktCategory, string pStrPacketGroup)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHPCNSTOCK", pIsWithPCNStock, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("GRDTYPE", pStrGrdType, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("PACKETATEGORY", pStrPacktCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String , ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleKapanAnalysis", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable SizeWisePolishReport(string pStrKapan, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SizeWisePolishReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable FactoryWisePolishReport(string strKapan, string strFromDate, string strToDate, string reportType, string StrCurrEmp , string StrManager)
        {

            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", strKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", strFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", strToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CURREMPLOYEE_ID", StrCurrEmp, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REPORTTYPE", reportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER", StrManager, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_KapanWisePolishReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }

        }

        public DataSet GetDataForSinglMemoAnalysis(string pStrKapan, double pDouMajuri, double pDouSaleRate, double pDouPurchaseRate, double pDouExtraRate, double pDouOnOutRate, string pStrMemoReportNote,
                                                   Int32 pIntPrdType_ID, string pStrReportGenerateType) //Used in Single Memo Analysis
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("OPE", "DETAIL", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAJURI", pDouMajuri, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SALERATE", pDouSaleRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PURCHASERATE", pDouPurchaseRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("EXTRARATE", pDouExtraRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("ONOUTRATE", pDouOnOutRate, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("MEMOREPORTNOTE", pStrMemoReportNote, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PRDTYPE_ID", pIntPrdType_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("REPORTGENERATETYPE", pStrReportGenerateType, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                //Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleMemoAnalysisReport", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleMemoAnalysisReport_PINALI", CommandType.StoredProcedure);

                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataSet GetDataForSinglMemoAnalysisOther(string pStrKapan, double pDouMajuri, double pDouSaleRate, double pDouPurchaseRate, double pDouExtraRate, double pDouOnOutRate,
                                                   Int32 pIntPrdType_ID, string pStrReportGenerateType) //Used in Single Memo Analysis
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("OPE", "DETAIL", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAJURI", pDouMajuri, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SALERATE", pDouSaleRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PURCHASERATE", pDouPurchaseRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("EXTRARATE", pDouExtraRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("ONOUTRATE", pDouOnOutRate, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pIntPrdType_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("REPORTGENERATETYPE", pStrReportGenerateType, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleMemoAnalysisReportOther", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetDataForSinglMemoAnalysisForSummary(string pStrKapan, string pStrFromDate, string pStrToDate) //Used in Single Memo Analysis
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMemoAnalysisSummaryReport", CommandType.StoredProcedure);
                //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMemoAnalysisSummaryReport_PINALI", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetDataForSinglMemoAnalysisForOtherSummary(string pStrKapan) //Used in Single Memo Analysis Other Summary : #P : 27-01-2021
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMemoAnalysisReportOtherSummary", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataTable GetPolishOKReport(string pStrOpe, string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SinglePolishOKReport", CommandType.StoredProcedure);
                //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SinglePolishOKReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataSet GetProcessWiseReport(string pStrKapan, string pStrProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate, Int32 pBoolIsIssue, bool pBoolIsNotInProcess, string pStrMainManager_ID, string pStrDepartmentID
            , string pStrRequiredProcess
            )
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pStrProcessID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pStrRequiredProcess, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pStrDepartmentID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISISSUEWISE", pBoolIsIssue, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISNOTINPROCESS", pBoolIsNotInProcess, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "TEMP", "RP_ProcessWiseReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetGalaxyReport(string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate, int intSizeWise) // D:07-08-2021
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("SIZEWISE", intSizeWise, DbType.Int32, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "TEMP", "RP_SingleGalaxyReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetDataForCutWiseProcess(string pStrOPE, string pStrKapan, string pStrFromDate, string pStrToDate) // D:09-10-2021
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_CutWiseProcessReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetDataForCutWiseProcessDetail(string pStrMainJangedno) // D:09-10-2021
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("MAINJANGEDNO", pStrMainJangedno, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_CutWiseProcessDetailReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataSet GetOtherProcessProductionReport(string pStrOpe, string pStrKapan, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleOtherProcessProductionReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetFactoryManagerProductionReport(string pStrKapan, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate, string pStrFormType) //Add : Pinali : 12-12-2019
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                //Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleFactoryManagerProductionReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetSawingSplitReport(string pStrKapan, string pStrFromDate, string pStrToDate) //Add : Pinali : 12-12-2019
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleSawingSplitReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataSet GetFactoryProductionLabour(string pStrOpe, string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleFactoryProductionLabourReportNew", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GetFactoryProductionLabourDetail(string pStrKapan, int pIntPacketNo, string pStrTag, string pStrPacketID, Int64 pIntEmployeeID, string pStrReportType)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pStrPacketID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                if (pStrReportType == "FACTORY")
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleFactoryProductionLabourDetail", CommandType.StoredProcedure);

                else if (pStrReportType == "MARKER")
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMarkerProductionLabourDetail", CommandType.StoredProcedure);

                else if (pStrReportType == "WORKER")
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleWorkerProductionLabourDetail", CommandType.StoredProcedure);
                else if (pStrReportType == "BLOCKINGCHECKER")
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleBlockingCheckerProductionLabourDetail", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public int UpdateLiveGradingColorStatus(string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate)
        {
            try
            {
                //#P : 09-12-2019
                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "POLCHECKER", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleFactoryProductionLabourToUpdateGradingColor", CommandType.StoredProcedure);
                //End : #P : 09-12-2019

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "GRADING", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleFactoryProductionLabourToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "BOMBAY", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleFactoryProductionLabourToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "LAB", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleFactoryProductionLabourToUpdateGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }



        public int UpdateLiveGradingColorFlag(string pStrTrnID, string pStrWagesBase)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("TRN_ID", pStrTrnID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("WAGESBASE", pStrWagesBase, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleFactoryProductionLabourToUpdateFlag", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public int UpdateMarkerLabourLiveGradingColorStatus(string pStrKapan, string pStrFromDate, string pStrToDate, Int32 pIntFromPacketNo, Int32 pIntToPacketNo) // Add : Pinali : 12-05-2019
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "GRADING", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                //Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleMarkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "BOMBAY", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                //Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleMarkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "LAB", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                //Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleMarkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }
        public int UpdateWorkerLabourLiveGradingColorStatus(string pStrKapan, string pStrFromDate, string pStrToDate, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, Int32 pIntProcessID) // Add : Pinali : 12-05-2019
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "POLCHECKER", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleWorkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "GRADING", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleWorkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "BOMBAY", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleWorkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                Ope.ClearParams();
                Ope.AddParams("WAGESBASE", "LAB", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes += Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleWorkerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }
        public int ResetMarkerWorkerPacketDataForProcess(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrTag, string pStrFromDate, string pStrToDate, string pResetForType) // Add : Pinali : 19-03-2020
        {
            try
            {
                int IntRes = 0;
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("RESETFORTYPE", pResetForType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Job_SinglePrdMarkerWorkerPacketDataResetForProcess", CommandType.StoredProcedure);
                if (AL.Count != 0)
                {
                    IntRes = Val.ToInt(AL[0]);
                }
                return IntRes;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public int UpdateMarkerDataProcessWithOnlyPktDetail(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo) // Add : Pinali : 11-02-2021
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleMarkerLabourPlusMius_UpdateDayToDayGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }
        public int UpdateWorkerDataProcessWithOnlyPktDetail(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrTag) // Add : Pinali : 11-02-2021
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleWorkerLabourPlusMius_UpdateDayToDayGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public int UpdateBlockingChkrLabourLiveGradingColorStatus(string pStrKapan, string pStrFromDate, string pStrToDate, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrTag) // Add : Pinali : 12-05-2019
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETTAG", pStrTag, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                int IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_SingleBlockingCheckerLabourPlusMinusProductionToUpdateGradingColor", CommandType.StoredProcedure);

                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public DataSet GetMarkerDollarPlusMinusProductionLabour(string pStrOpe, string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate, Int32 pIntFromPacketNo, Int32 pIntToPacketNo)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleMarkerPlusMinusProductionLabourReportNew", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataSet GetBlockingDollarPlusMinusProductionLabour(string pStrOpe, string pStrKapan, int pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate) //Add : #P : 27-01-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SingleBlockingPlusMinusProductionLabourReportNew", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataTable StoreLabourProductionData(string pStrOpe, DataTable pDtabProd) //Add : Pinali : 21-05-2019
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TBL_TRN_SINGLELABOURPRODUCTIONREPORTDATA", pDtabProd, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_LabourProductionReportDataSave", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetMarkerRollingReport(string pStrOpe, Int64 pIntEmployee_ID, string pStrReportType = "SUMMARY", int pIsWithOw = 0, string pStrProcessName = "",
            string pStrKapanName = "", int pIntFromPacketNo = 0, int pIntToPacketNo = 0, string pStrTag = ""

            ) //Add : Pinali : 21-05-2019
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REPORTTYPE", pStrReportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHOW", pIsWithOw, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

                //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMarkerRollingReport", CommandType.StoredProcedure);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMarkerRollingReportNew", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetMarkerRollingReportNew(Int64 pIntEmployee_ID, string pStrReportType = "SUMMARY", string pStrProcessName = "", string pStrKapanName = "", int pIntFromPacketNo = 0, int pIntToPacketNo = 0, string pStrTag = "")
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REPORTTYPE", pStrReportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleMarkerRollingReportNew", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetWorkerRollingReport(string pStrOpe, Int64 pIntEmployee_ID, string pStrCurrentDate, string pStrReportType = "", bool pIsWithOw = false, string pStrProcessName = "") //Add : Pinali : 21-05-2019
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CURRENTDATE", pStrCurrentDate, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("REPORTTYPE", pStrReportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHOW", pIsWithOw, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleWorkerRollingReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetKapanRollingReport(string pStrOpe, string pStrReportType = "SUMMARY", string pStrDeptName = "",
           string pStrKapanName = "", int pIntFromPacketNo = 0, int pIntToPacketNo = 0, string pStrTag = "", bool pIsWithPCNStock = false) //Add : Pinali : 08-01-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REPORTTYPE", pStrReportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENTNAME", pStrDeptName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHPCNSTOCK", pIsWithPCNStock, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingReportNew", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetKapanRollingReportSummary(string pStrOpe, string pStrDeptName = "",
            string pStrKapanName = "", int pIntFromPacketNo = 0, int pIntToPacketNo = 0, string pStrTag = "", bool pIsWithPCNStock = false,
            string pStrShape_ID = "", string pStrColor_ID = "", string pStrClarity_ID = "", string pStrSize_ID = "",
            double pDouFromAmount = 0, double pDouToAmount = 0) //Add : Pinali : 08-01-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENTNAME", pStrDeptName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHPCNSTOCK", pIsWithPCNStock, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pStrShape_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pStrColor_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLARITY_ID", pStrClarity_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZE_ID", pStrSize_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMAMOUNT", pDouFromAmount, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOAMOUNT", pDouToAmount, DbType.Double, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingReportNewSummary", CommandType.StoredProcedure);
                //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingReportNewSummary_Pinali", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetStockPrintReport(string pStrOpe, string pStrKapanName, string pStrTable ,string pStrMarker_ID, string pStrProcessName, string pStrMainManager_ID, string pStrViewType, string pStrPacketCategory, string pStrPacketGroup)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TABLENAME", pStrTable, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MARKER_ID", pStrMarker_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VIEWTYPE", pStrViewType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY", pStrPacketCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String, ParameterDirection.Input);

                //  Ope.AddParams("ISMARKERWISEPRCWISESUM", pISMarkerWithPrcWiseSum, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_StockPrintReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetStockPrintKapanWiseReport(string pStrOpe, string pStrKapanName, string pStrTable, string pStrMarker_ID, string pStrProcessName, string pStrReportGroup, string pPacketCategorty, string pStrPacketGroup)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TABLENAME", pStrTable, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MARKER_ID", pStrMarker_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESSNAME", pStrProcessName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REPORTGROUP", pStrReportGroup, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY", pPacketCategorty, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_StockPrintKapanWiseReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public DataTable GetKapanRollingReportDetail(string pStrDeptName = "",
            string pStrKapanName = "", int pIntFromPacketNo = 0, int pIntToPacketNo = 0, string pStrTag = "", bool pIsWithPCNStock = false,
            string pStrShape_ID = "", string pStrColor_ID = "", string pStrClarity_ID = "", string pStrSize_ID = "",
             double pDouFromAmount = 0, double pDouToAmount = 0) //Add : Pinali : 26-08-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENTNAME", pStrDeptName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHPCNSTOCK", pIsWithPCNStock, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pStrShape_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pStrColor_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLARITY_ID", pStrClarity_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZE_ID", pStrSize_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMAMOUNT", pDouFromAmount, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOAMOUNT", pDouToAmount, DbType.Double, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingReportNewDetail", CommandType.StoredProcedure);
                //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingReportNewDetail_PINALI", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }



        public DataTable GetFactoryAgingReport(string pStrKapan, string pStrOpe, string pStrType = "") //Add : Pinali : 26-07-2019 : In Type "Final Makabke"/"Full Top" and In StrOpe=Summary/Detail
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TYPE", pStrType, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_FactoryAgingReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        //End As
        public DataSet GetEmployeeOverDueReport(string pStrOpe, Int64 pIntEmployee_ID, string pStrKapanName)
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("STATUS", pStrOpe, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SinglePacketOverDue", CommandType.StoredProcedure);

                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet GerGradingComparision(string pStrKapan, string pStrOpe, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_GradingComparison", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GerGradingComparisionWithLatestGrdByLab(string pStrKapan, string pStrOpe, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID) //Add : Pinali : 15-11-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_GradingComparisonWithLatestGrdByLab", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataTable GerGradingComparisionDetailWithLatestGrdByLab(string pStrKapan, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID,
            string pStrClickType,
            string pStrRowValue,
            string pStrColValue

            )
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_GradingComparisonDetailWithLatestGrdByLab", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet GerGradingComparisionDetailWithLatestGrdByLabWithPivotBox(string pStrKapan, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID,
            string pStrClickType,
            string pStrRowValue,
            string pStrColValue

            )
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_GradingComparisonDetailWithLatestGrdByLabWithPivotBox", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GerGradingComparisionDetail(string pStrKapan, string pStrOpe, string pStrFromDate, string pStrToDate, Int64 pIntEmployeeID,
            string pStrClickType,
            string pStrRowValue,
            string pStrColValue

            )
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_GradingComparisonDetail", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }


        public DataTable GetPopupDetail(string pStrOpe, Int64 pIntEmployeeID, string pStrDate, int pIntYear, int pIntMonth)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DATE", pStrDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("YEAR", pIntYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MONTH", pIntMonth, DbType.Int32, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_MarkerDashboardGetPopupDetail", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet GetSalaryViewSummaryDetail(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrSalaryType,
                                                  Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate, string pStrOpe, bool pISWithRejectedEmp) //Add : Pinali : 14-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SALARYTYPE", pStrSalaryType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHREJECTEDEMP", pISWithRejectedEmp, DbType.Boolean, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SalaryViewSummaryDetail", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SalaryViewSummaryDetail_Pinali", CommandType.StoredProcedure);

                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataTable GetSalaryViewDetailWithZeroAmt(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, Int64 pIntEmployee_ID,
                                                      string pStrFromDate, string pStrToDate, string pStrOpe) //Add : Pinali : 21-09-2020
        {
            //try
            //{
            //    DataTable DTab = new DataTable();

            //    Ope.ClearParams();
            //    Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            //    Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            //    Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            //    Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
            //    Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
            //    Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            //    Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
            //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SalaryViewDetailWithProcessAmtZero", CommandType.StoredProcedure);

            //    return DTab;
            //}
            //catch (Exception Ex)
            //{

            //    return null;
            //}

            try
            {
                DataTable DTab = new DataTable();
                if (pStrOpe == "MARKER")
                {
                    Ope.ClearParams();
                    Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                    Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("SALARYTYPE", "", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                    Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                    Ope.AddParams("OPE", "PROCESSDETAILWITHAMOUNTZERO", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("ISWITHREJECTEDEMP", 1, DbType.Boolean, ParameterDirection.Input);
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SalaryViewSummaryDetail", CommandType.StoredProcedure);
                }
                else if (pStrOpe == "WORKER")
                {
                    Ope.ClearParams();
                    Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                    Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("PROCESS_ID", 531, DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                    Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                    Ope.AddParams("SHAPETYPE", "ALL", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("OPE", "PROCESSDETAILWITHAMOUNTZERO", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("ISWITHREJECTEDEMP", 1, DbType.Boolean, ParameterDirection.Input);
                    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_WorkerSalaryViewSummaryDetail", CommandType.StoredProcedure);
                }
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }

        }
        public string ProcessSalaryViewDetailWithZeroAmt(Int64 pIntEmployee_ID, string pStrFromDate, string pStrToDate, string pStrPktDetail, string pStrOpe) //Add : Pinali : 21-09-2020
        {
            string StrRes = "";

            Ope.ClearParams();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("XMLPKTDETAIL", pStrPktDetail, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "RP_SalaryViewDetailWithProcessAmtZero", CommandType.StoredProcedure);
            if (AL.Count != 0)
            {
                StrRes = Val.ToString(AL[0]);
            }
            return StrRes;
        }


        public DataSet GetPolishCheckerGradingAnalysisData(string pStrKapan, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate) //Add : Pinali : 14-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_PolishCheckerGradingAnalysis", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataSet GetSalaryViewSummaryDetailForWorker(string pStrKapanName, Int32 pIntFromPacketNo, Int32 pIntToPacketNo,
                                                           Int32 pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate,
                                                           string pStrToDate) //Add : Pinali : 14-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_WorkerSalaryViewSummaryDetail", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public int JobUpdate(string pStrFromDate, string pStrToDate, string KapanName, Int32 FromPktNo, Int32 ToPktNo, string Tag) //Krina : 02/02/2023
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();

                Ope.AddParams("WAGESBASE", "POLCHECKER", DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", "", DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", 0, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", 0, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", 531, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("PACKETTAG", Tag, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_DeleteUpdate", CommandType.StoredProcedure);
            }
            catch (Exception Ex)
            {

                return -1;
            }
        }

        public DataSet GetSalaryViewSummaryDetailForChiefArtist(string pStrKapanName, Int32 pIntFromPacketNo, Int32 pIntToPacketNo,
                                                          Int32 pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate,
                                                          string pStrToDate, string pStrShapeType, bool pISWithRejectedEmp) //Add : Pinali : 14-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("SHAPETYPE", pStrShapeType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHREJECTEDEMP", pISWithRejectedEmp, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_ChiefArtistSalaryViewSummaryDetail", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_ChiefArtistSalaryViewSummaryDetail_Pinali", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public DataSet GetBlockingSalaryViewSummaryDetail(string pStrKapanName, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate) //Add : Pinali : 25-02-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_BlockingSalaryViewSummaryDetail", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }


        public DataSet GetBlockingCheckerSalaryViewSummaryDetail(string pStrKapanName, Int32 pIntFromPacketNo, Int32 pIntToPacketNo,
                                                           Int32 pIntProcessID, Int64 pIntEmployeeID, string pStrFromDate,
                                                           string pStrToDate) //Add : Pinali : 21-12-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_BlockingCheckerSalaryViewSummaryDetail", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }



        public DataSet GetDharAndMaxiSalaryViewSummaryDetail(string pStrOpe, string pStrKapan, Int64 pIntEmployeeID, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_DharMaxiSalaryViewSummaryDetail", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_DharMaxiSalaryViewSummaryDetail_Pinali", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetSalaryViewProcessWiseData(string pStrKapan, string pStrProcess_ID, string pStrEmployee_ID, string pStrManager_ID, string pStrDepartment_ID, string pStrFromDate, string pStrToDate) //#P : 13-06-2022
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pStrProcess_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pStrManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pStrDepartment_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SalaryViewForProcessWise", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetSalaryView4PLabourProcessWiseData(string pStrKapan, string pStrProcess_ID, string pStrEmployee_ID, string pStrManager_ID, string pStrDepartment_ID, string pStrFromDate, string pStrToDate) //#P : 13-06-2022
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pStrProcess_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pStrManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pStrDepartment_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_SalaryViewFor4PLabourProcessWise", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataTable GetBreakingDiffReportSummary(string pStrKapanName, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrFromDate, string pStrToDate) //Add : Pinali : 02-12-2020
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BreakingDifferenceReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataTable GetBreakingDiffReportDetail(string pStrKapan, int pIntPacketNo, int pIntBreakingType_ID)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BREAKINGTYPE_ID", pIntBreakingType_ID, DbType.Int32, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BreakingDifferenceDetailReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public int SaveWagesParameterData(string pStrOpe, string pStrKapanName, Int32 pIntPacketNo, string pStrTag, Int64 pIntEmployee_ID, bool pIsConsDPlusVartn,
                                          bool pIsProcessPktWithFMkblCla, bool pIsConsSuratExpAmtCalc,
                                          bool pIsNotConsiderDollarMinusVariation,
                                          string pStrDepartmentType) //Add : Pinali : 14-12-2019
        {
            try
            {
                DataSet DS = new DataSet();

                int IntRes = 0;

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENTTYPE", pStrDepartmentType, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISCONSIDERPLUSDOLLARVARIATION", pIsConsDPlusVartn, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISPROCESSPKTWITHFMKBLCLARITY", pIsProcessPktWithFMkblCla, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISCONSIDERSURATEXPAMTCALC", pIsConsSuratExpAmtCalc, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISNOTCONSIDERDOLLARMINUSVARIATION", pIsNotConsiderDollarMinusVariation, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RETURNVALUE", 0, DbType.Int32, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdWagesParameterSave", CommandType.StoredProcedure);
                if (AL.Count != 0)
                {
                    IntRes = Val.ToInt(AL[0]);
                }
                return IntRes;
            }
            catch (Exception Ex)
            {
                return -1;
            }
        }

        public DataTable GetDataForMyAgingReport(string pStrOpe, string pStrFromDate, string pStrToDate, string pStrKapan, Int64 pStrLedger_ID, string pStrDepartment_ID) //Add : #P : 06-07-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LEDGER_ID", pStrLedger_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pStrDepartment_ID, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_MyAgingReportDashboard", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public int SaveAgingExtraPlusMinutes(string pStrTrnAging_ID, int pIntExtraPlusMinutes) //Add : #P : 06-07-2020
        {
            try
            {
                Ope.ClearParams();
                string StrQuery = "Update Trn_AgingTransaction With(Rowlock) Set ExtraPlusMinutes = '" + pIntExtraPlusMinutes + "', ApproxReturnTime = dbo.GetAgingAproxDueDate(AgingMinutes + " + pIntExtraPlusMinutes + ",IssueDateTime) Where TrnAging_ID = '" + pStrTrnAging_ID + "'";
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataTable GetDataForHeliumPacketPrintLimitDetail(string pStrKapanName, string pStrFromDate, string pStrToDate, string pStrOpe) //Add : #P : 15-07-2020
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_HeliumPacketPrintLimitDetail", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public int SaveHeliumExtraPacketPrintLimit(string pStrHelLimit_ID, int pIntExtraPrintLimit) //Add : #P : 15-07-2020
        {
            try
            {
                Ope.ClearParams();
                string StrQuery = "Update HEL_PacketIssueReturnPrintLimitDetail With(Rowlock) Set EXTRAPRINTLIMIT = '" + pIntExtraPrintLimit + "' Where HelLimit_ID = '" + pStrHelLimit_ID + "'";
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataSet GetGradingComparisionView(string pStrFromDate, string pStrToDate, Int64 pStrEmployeeCode, string pStrOpe, string pStrClickType, string pStrRowValue, string pStrColValue, string pStrTotalType, string pStrTotalClickType
                                                   , int pStrMonth, int pStrYear) //Add : Dhara : 06-10-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployeeCode, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOTALTYPE", pStrTotalType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOTALCLICKTYPE", pStrTotalClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MONTH", pStrMonth, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("YEAR", pStrYear, DbType.Int32, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_GradingComparisionViewGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetPolishCheckerComparisionView(string pStrFromDate, string pStrToDate, Int64 pStrEmployeeCode, string pStrOpe, string pStrClickType, string pStrRowValue, string pStrColValue, string pStrTotalType, string pStrTotalClickType
                                                , int pStrMonth, int pStrYear) //Add : Dhara : 10-10-2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployeeCode, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOTALTYPE", pStrTotalType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOTALCLICKTYPE", pStrTotalClickType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MONTH", pStrMonth, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("YEAR", pStrYear, DbType.Int32, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_PolishComparisionViewGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataTable GetGradingReportGetData(string pStrFromDate, string pStrToDate, Int64 pStrEmployeeId) //Add : Dhara : 12-10-2020
        {
            try
            {
                DataTable Dtab = new DataTable();

                Ope.ClearParams();

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployeeId, DbType.Int64, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "RP_GradingReportGetData", CommandType.StoredProcedure);
                return Dtab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataTable GetVariationReportGetData(string pStrFromDate, string pStrToDate, Int64 pStrEmployeeId) //Add : Dhara : 19-10-2020
        {
            try
            {
                DataTable Dtab = new DataTable();

                Ope.ClearParams();

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pStrEmployeeId, DbType.Int64, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "RP_VariationReportGetData", CommandType.StoredProcedure);
                return Dtab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet PlanningReportAssorterWiseGetData(string strKapanName, string pStrTablename, string strFromDate, string strToDate, string pStrPrdType_ID, string pStrMainManager_ID, string pStrViewType, string pStrAssorter_ID, string pStrPacketCatogery, string pStrPacketGroup)
        {
            try
            {
                DataSet DTab = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", strKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TABLENAME", pStrTablename, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", strFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", strToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pStrPrdType_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VIEWTYPE", pStrViewType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ASSORTER_ID", pStrAssorter_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY", pStrPacketCatogery, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DTab, "Table", "RP_PlanningReportAssorterWiseGetData", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DTab, "Table", "RP_PlanningReportAssorterWiseGetData_17052022", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        #endregion

        #region Tender

        public Trn_TenderProperty SaveTenderEntry(Trn_TenderProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("LOT_ID", pClsProperty.Lot_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGH_ID", pClsProperty.Rough_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TENDERNAME", pClsProperty.TenderName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGHNAME", pClsProperty.RoughName, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SIZE_ID", pClsProperty.Size_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LOTNO", pClsProperty.LotNo, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINCARAT", pClsProperty.MainCarat, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TENDERDATE", pClsProperty.TenderDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("NOTE", pClsProperty.Note, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IMPDOLLARRATE", pClsProperty.IMPDOLLARRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPDOLLARRATE", pClsProperty.EXPDOLLARRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ROUGHIMPORTPER", pClsProperty.ROUGHIMPORTPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ROUGHIMPORTAMOUNT", pClsProperty.ROUGHIMPORTAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PROFITPER", pClsProperty.PROFITPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PROFITAMOUNT", pClsProperty.PROFITAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LABOURPER", pClsProperty.LABOURPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LABOURAMOUNT", pClsProperty.LABOURAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FINALAMOUNT", pClsProperty.FINALAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FINALAVG", pClsProperty.FINALAVG, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("CHECKBY", pClsProperty.CHECKBY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLDETAIL", pClsProperty.XmlDetail, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_TenderSave", CommandType.StoredProcedure);
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
        public DataTable GetAllParameterTableForTender()
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select * From MST_Para With(NOLOCK) Where 1=1 And ISActive = 1";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }
        public DataTable GetMajuriRateTableForTender()
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select * From MST_MajuriRate With(NOLOCK)";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }

        public DataTable GetSuratExpectedLabExpenseMaster()
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select * From MST_SuratExpectedLabExpenseRate With(NOLOCK)";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }

        public DataRow GetTenderSingleData(string pStrLotID, string pStrCheckBy)
        {
            string Str = @"SELECT 
            T.LOT_ID,
	        T.ROUGH_ID,
	        T.TENDERNAME,
	        T.ROUGHNAME,
        
            T.SIZE_ID,    
            S.PARANAME AS SIZENAME,

	        T.LOTNO,
	        T.MAINCARAT,
	        T.TENDERDATE,
	        T.NOTE,
	        T.CHECKBY,
	        T.IMPDOLLARRATE,
	        T.EXPDOLLARRATE,

	        T.ROUGHIMPORTPER,
	        T.ROUGHIMPORTAMOUNT,
	
	        T.PROFITPER,
	        T.PROFITAMOUNT,
	
	        T.LABOURPER,
	        T.LABOURAMOUNT,

	        T.FINALAMOUNT,
	        T.FINALAVG,
	        T.ENTRYDATE,
	        T.RAPDATE

	        FROM TRN_Tender T WITH(NOLOCK)	
            Left Join MST_Para S WITH(NOLOCK) ON S.PARA_ID = T.SIZE_ID
	        WHERE Lot_ID = '" + pStrLotID + "' And CheckBy = '" + pStrCheckBy + "'";

            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetTenderFillList(string pStrFromDate, string pStrToDate, string pStrTender, string pStrRough, string pStrSize)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("TENDER", pStrTender, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGH", pStrRough, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZE", pStrSize, DbType.String, ParameterDirection.Input);


                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_TenderGetFullList", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetTenderData(string pStrRoughID)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("ROUGH_ID", pStrRoughID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGHNAME", "", DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_TenderGetData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public string FindExchangeRate()
        {
            Ope.ClearParams();

            string Str = "Select SettingValue From MST_Setting With(NoLock) Where SettingKey = 'EXCRATE'";

            DataRow DRow = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
            if (DRow != null)
            {
                return Val.ToString(DRow[0]);
            }
            else
            {
                return "";
            }

        }
        public Trn_TenderProperty DeleteTenderDetail(Trn_TenderProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ROUGH_ID", pClsProperty.Rough_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PacketNo, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_TenderDeleteDetail", CommandType.StoredProcedure);
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
        public Trn_TenderProperty DeleteTenderEntry(Trn_TenderProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ROUGH_ID", pClsProperty.Rough_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.Lot_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CHECKBY", pClsProperty.CHECKBY, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_TenderDelete", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty FindTenderRapWithUpDown(Trn_RapSaveProperty pClsProperty)
        {
            DataSet DS = new DataSet();
            Ope.ClearParams();

            Ope.AddParams("L_Code", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("Carat", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("Shape_Code", pClsProperty.SHAPECODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("Color_Code1", pClsProperty.COLORCODE1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Color_Code2", pClsProperty.COLORCODE2, DbType.String, ParameterDirection.Input);

            Ope.AddParams("Clarity_Code1", pClsProperty.CLARITYCODE1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Clarity_Code2", pClsProperty.CLARITYCODE2, DbType.String, ParameterDirection.Input);

            Ope.AddParams("Lab", pClsProperty.LABCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Cut_Code", pClsProperty.CUTCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Polish_Code", pClsProperty.POLCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Symmetry_Code", pClsProperty.SYMCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Flursance_Code", pClsProperty.FLCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LbLc_Code", pClsProperty.LBLCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Nts_Code", pClsProperty.NATTSCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Milky_Code", pClsProperty.MILKYCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Diameter", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("ColorShade_Code", pClsProperty.COLORSHADECODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OpenInclusion_Code", pClsProperty.OPENINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BlackInclusion_Code", pClsProperty.BLACKINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("WhiteInclusion_Code", pClsProperty.WHITEINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Pavallion_Code", pClsProperty.PAVCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("CanadaMark_Code", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EyeClean_Code", pClsProperty.EYECLEANCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Luster_Code", pClsProperty.LUSTERCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Natural_Code", pClsProperty.NATURALCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Grain_Code", pClsProperty.GRAINCODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ShapeName", pClsProperty.SHAPENAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("CutCode", pClsProperty.CUTNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PolName", pClsProperty.POLNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SymName", pClsProperty.SYMNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FLName", pClsProperty.FLNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LBLCName", pClsProperty.LBLCNAME, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FutureDis", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("PlanningType", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("ManualDiscount", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("RapDate", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("IsGetBack", 1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Rate", "", DbType.String, ParameterDirection.Output);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_TenderFindRap", CommandType.StoredProcedure);

            pClsProperty.DISCOUNT = 0;
            pClsProperty.AMOUNTDISCOUNT = 0;
            pClsProperty.PRICEPERCARAT = 0;
            pClsProperty.RAPAPORT = 0;
            pClsProperty.AMOUNT = 0;
            pClsProperty.ISMIXRATE = false;
            pClsProperty.GIANONGIA = "GIA";

            pClsProperty.DRowDisRegular = null;

            if (DS.Tables.Count == 0)
            {
                return pClsProperty;
            }

            if (DS.Tables[0].Rows.Count == 0)
            {
                return pClsProperty;
            }

            pClsProperty.DISCOUNT = Val.Val(DS.Tables[0].Rows[0]["AvgDiscount"]);
            pClsProperty.AMOUNTDISCOUNT = 0;
            pClsProperty.PRICEPERCARAT = Val.Val(DS.Tables[0].Rows[0]["AvgPricePerCarat"]);
            pClsProperty.RAPAPORT = Val.Val(DS.Tables[0].Rows[0]["AvgRap"]);
            pClsProperty.AMOUNT = Val.Val(DS.Tables[0].Rows[0]["TotalAmount"]);
            pClsProperty.ISMIXRATE = false;
            pClsProperty.GIANONGIA = "GIA";

            DS.Tables[1].TableName = "ROW";

            string originalXmlString = string.Empty;
            using (StringWriter sw = new StringWriter())
            {
                DS.Tables[1].WriteXml(sw);
                originalXmlString = sw.ToString();
            }

            pClsProperty.XMLDETAIL = originalXmlString;

            return pClsProperty;
        }

        public DataTable GetAllSettings()
        {
            string Str = "Select SettingKey, SettingValue From MST_Setting With(NoLock)";
            DataTable DTab = new DataTable();
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }

        // #:D 18-09-2020
        public int SaveAgingTotalExtraPlusMinutes(string pStrTrnAging_ID, int PIntTotalExtraMin, int pIntExtraPlusMinutes) //Add : #D : 11-09-2020
        {
            try
            {
                Ope.ClearParams();
                string StrQuery = "Update Trn_AgingTransaction With(Rowlock) Set TotalExtraMin = '" + PIntTotalExtraMin + "',ExtraPlusMinutes = '" + pIntExtraPlusMinutes + "', ApproxReturnTime = dbo.GetAgingAproxDueDate(AgingMinutes + " + PIntTotalExtraMin + ",IssueDateTime) Where TrnAging_ID = '" + pStrTrnAging_ID + "'";
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            }
            catch (Exception Ex)
            {
                return 0;
            }
        }
        public DataTable GetDataForMyAgingReportPrint(string pStrOpe, string pStrFromDate, string pStrToDate, string pStrKapan, Int64 pStrLedger_ID, string pStrDepartmentName)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LEDGER_ID", pStrLedger_ID, DbType.UInt64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENTNAME", pStrDepartmentName, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_MyAgingReportDashboardPrint", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        // #:D 18-09-2020   

        public DataSet GetRoughWiseValueReport(string pStrOpe, string pStrInvoiceNo = "") //Add : #P : 09-11-2020
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("INVOICENO", pStrInvoiceNo, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_RoughWiseValueReportGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetDataForAtdCaptureDetail(string pStrLogdate, string pStrEmpCode, string StrActive) // #:D : 14-12-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("LOGDATE", pStrLogdate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPCODE", pStrEmpCode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", StrActive, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_AtdCaptureDetailGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public AttendanceEntryProperty Delete(string pEmpCode, string pLogDateTime, string pPunch, AttendanceEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("EMPCODE", pEmpCode, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOGDATETIME", pLogDateTime, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PUNCH", pPunch, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_AtdCaptureDetailDelete", CommandType.StoredProcedure);
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

        public AtdCaptureDetailProperty SaveAtdCaptureDetail(AtdCaptureDetailProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("EMP_ID", pClsProperty.EMP_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPCODE", pClsProperty.EMPCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPNAME", pClsProperty.EMPNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOGDATETIME", pClsProperty.LOGDATETIME, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("EMPDEVICECODE", pClsProperty.EMPDEVICECODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOGDATE", pClsProperty.LOGDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOGTIME", pClsProperty.LOGTIME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RFID", pClsProperty.RFID, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PUCHDERACTION", pClsProperty.PUCHDERACTION, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISADDMANUALPUNCH", pClsProperty.ISADDMANUALPUNCH, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_AtdCaptureDetailSave", CommandType.StoredProcedure);

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
        public DataTable GetDataForAtdCapture(string pStrFromDate, string pStrToDate, Int64 pStrEmpCode, string StrActive) // #:D : 08-12-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPCODE", pStrEmpCode, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("OPE", StrActive, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_AtdCaptureGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForBombayTransferLabGIA(string pStrFromDate, string pStrToDate, int pIntISWithMixTotal) //Add : Dhara : 20-03-2021
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISMIXTOTAL", pIntISWithMixTotal, DbType.Int32, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BombayTrasferLabWiseDetailReportGIA", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataTable GetBombayTransLabReportDetailGIA(string pStrFromDate, string pStrToDate, string pStrLabName) //Add : Dhara : 20-03-2021
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("LABNAME", pStrLabName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BombayTrasferLabWiseDetailReport_DetailGIA", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        //Add Milan(20-03-2021)
        public DataTable GetDataForBombayTransferLabMix(string StrFromDate, string StrToDate)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BombayTrasferLabWiseDetailReportMIX", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetDataForBombayTransferLab_DetailMix(string StrFromDate, string StrToDate)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_BombayTrasferLabWiseDetailReport_DetailMIX", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        //End Milan
        #endregion
        public DataSet KapanDetailReport(string pStrKapan, string pStrFromDate, string pStrToDate, string pStrMainManager_ID, string pStrPacketCategory, string pStrPacketGroup)
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY", pStrPacketCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_KapanNameWiseProcessData", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_KapanNameWiseProcessData_Mix", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet KapanFactoryWisePolishReport(string pStrKapan, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RptPartyWiseRecSummary", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet KapanFinalSummaryReport(string pStrKapan, string pStrFromDate, string pStrToDate, string pStrMainManager_ID)
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_KapanFinalSummaryReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetShapeWisePrdReport(string pStrOpe, string pStrKapanName, int pStrShape_ID, string pStrMainManager)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pStrShape_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_ShapeWiseSingleFinalPrdReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
        public DataSet GetShapeWiseMkblPrdReport(string pStrOpe, string pStrKapanName, int pStrShape_ID, string pStrMainManager_ID)
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pStrShape_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_ShapeWiseMakableReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        //public DataTable GetKapanWiseRollingReportDataSummary(string StrOpe,
        //                                                      string ViewType,
        //                                                      string pStrKapanName,
        //                                                      int pIntFromPacketNo,
        //                                                      int pIntToPacketNo,
        //                                                      string pStrTag,
        //                                                      string pStrDeparment_ID,
        //                                                      bool pISWithBombayStock
        //                                                     )
        //{
        //    try
        //    {
        //        DataTable DTab = new DataTable();
        //        Ope.ClearParams();

        //        Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("VIEWTYPE", ViewType, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("DEPARTMENT_ID", pStrDeparment_ID, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
        //        Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
        //        Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("ISWITHBOMBAYSTOCK", pISWithBombayStock, DbType.Boolean, ParameterDirection.Input);
        //        Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleKapanRollingPinali", CommandType.StoredProcedure);
        //        return DTab;
        //    }
        //    catch (Exception ex)
        //    {
        //        return null;
        //    }
        //}
        public DataSet GetKapanWiseRollingReportDataSummary(string StrOpe,
                                                             string ViewType,
                                                             string pStrKapanName,
                                                             int pIntFromPacketNo,
                                                             int pIntToPacketNo,
                                                             string pStrTag,
                                                             string pStrDeparment_ID,
                                                             bool pISWithBombayStock,
                                                             string PStrStoneType,
                                                             string pStrparty_ID
                                                            )
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();

                Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VIEWTYPE", ViewType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pStrDeparment_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISWITHBOMBAYSTOCK", pISWithBombayStock, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("STONETYPE", PStrStoneType, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pStrparty_ID, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Table", "RP_SingleKapanWiseRollingReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public DataSet GetRoughStockReport(string StrOpe,
                                          string ViewType,
                                          string pStrKapanName,
                                          string pStrFromDate,
                                          string pStrToDate,
                                          string pStrRoughType,
                                          string pStrDataType,
                                          string pStrClvData,
                                          string pStrFormType
                                         )
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();

                Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VIEWTYPE", ViewType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGHTYPE", pStrRoughType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DATATYPE", pStrDataType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLVEMPLOYEE", pStrClvData, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Table", "RP_RoughStockReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public DataTable GetRejectionData(string StrFromDate, string StrToDate, string StrStatus)
        {
            Ope.ClearParams();
            DataTable Dtab = new DataTable();
            
            Ope.AddParams("FROMDATE", StrFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("STATUS", StrStatus, DbType.String, ParameterDirection.Input);
            //Ope.AddParams("PACKETNO", PktNo, DbType.Int32, ParameterDirection.Input);            

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "TRN_RejectionGetData", CommandType.StoredProcedure);

            return Dtab;
        }

        public TrnSingleIssueReturnProperty ApprovedOrRejectTransaction(TrnSingleIssueReturnProperty pClsProperty,string StrOpe)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OPE", StrOpe, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("REJECTSTATUSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REJECTSTATUSIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);
               
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_RejectionDataApproved", CommandType.StoredProcedure);

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

        public DataSet GetQCLabourWiseData(string PstrKapan, string PstrProcess_ID, string StrFromDate, string StrToDate)
        {
            DataSet DS = new DataSet();

            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", PstrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", PstrProcess_ID, DbType.String, ParameterDirection.Input);
            //Ope.AddParams("EMPLOYEE_ID", PstrEmployee_ID, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_QCLabourReportGetData", CommandType.StoredProcedure);
            return DS;
            
        }


        public DataSet GetQCTransferData(string StrFromDate, string StrToDate) //add by urvisha : 18-03-2023
        {
            
            DataSet DS = new DataSet();
            Ope.ClearParams();
            Ope.AddParams("FROMDATE", StrFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.String, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_QCReturnGetData", CommandType.StoredProcedure);
            return DS;
            
        }


        public DataTable GetQCWiseData(string PstrKapan, string PstrProcess_ID, string StrFromDate, string StrToDate)
        {
            DataTable Dtab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", PstrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", PstrProcess_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "RP_QCReportGetData", CommandType.StoredProcedure);

            return Dtab;

        }

        public TrnPenaltyIncentiveProperty WorkerWagesFlagUpdate(TrnPenaltyIncentiveProperty pClsProperty)//GUNJAN:31/03/2023
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("FLAGRESETXML", pClsProperty.StrFlagResetXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_ISWagesForWorkerFlagUpdate", CommandType.StoredProcedure);

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
    }
}

