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
    public class BOTRN_RunningPositionParcel
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        public DataSet RunningPosstionDataParcel(string pStrFormType, string pStrStockCategory, string pStrStockType, string mStrKapan, Int32 mIntPacketNo, bool pIsWithExtraStock, string pStrProcess_ID, string pStrMainManager_ID, string pStrsubgruop)
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
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pStrProcess_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBGROUP", pStrsubgruop, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_FactoryRunningPositionParcel", CommandType.StoredProcedure);
                
                return DS;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable RunningPosstionDataWIPPivotReportParcel(string pStrStockCategory, string pStrStockType, string mStrKapan, Int32 mIntPacketNo)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("STOCKCATEGORY", pStrStockCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STOCKTYPE", pStrStockType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", mStrKapan, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", mIntPacketNo, DbType.Int32, ParameterDirection.Input);               
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_SingleRunningPossitionParcelProcessWisePivot", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }
    }
}
