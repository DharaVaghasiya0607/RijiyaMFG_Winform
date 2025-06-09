using System;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;


namespace BusLib.View
{
    public class BOTRN_ProductionAnalysis
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        
        #region Other Function

        public DataSet GetProductionAnalysisData(string pStrOpe,string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_ProductionAnalysisMakable", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {
               
                return null;
            }
        }
        public DataTable GetMISAnalysisMakPolGrdData(string pStrOpe, string pStrFromDate, string pStrToDate,string pStrKapan,string pStrShape,string pStrColor,string pStrClarity,string pStrSize)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pStrShape, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pStrColor, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLARITY_ID", pStrClarity, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZE_ID", pStrSize, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab,"RP_MISAnalysisMakPolGrd", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetMarkerDashboardData(string pStrEmployee_ID, string pStrFromDate, string pStrToDate) //Add : Pinali : 25-11-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_MarkerDashboardGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetMarkerDashboardDataNew(string pStrEmployee_ID, string pStrFromDate, string pStrToDate) //Add : Pinali : 31/10/2020
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_MarkerDashboardGetDataNew", CommandType.StoredProcedure);
                //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_MarkerDashboardGetDataNew_PINALI", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetWorkerDashboardData(Int64 pStrEmployee_ID, string pStrFromDate, string pStrToDate, string pStrType) //Add : Pinali : 25-11-2019
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TYPE", pStrType, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_WorkerDashboardGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        public DataSet GetMarkerDashboardDataCurrent(string pStrEmployee_ID, string pStrFromDate, string pStrToDate) //#Krina : 06-10-2022
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_MarkerDashboardGetDataCurrent", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
        #endregion
    }
}

