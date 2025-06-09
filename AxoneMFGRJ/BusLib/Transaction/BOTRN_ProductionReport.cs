using AxonDataLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;

namespace BusLib.Transaction
{
    public class BOTRN_ProductionReport
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable ProductionRptGetData(string pStrFromDate, string pStrToDate)// Milan:
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Rp_ProductionReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

        public DataTable GetDetailOfReport(string StrDate, string StrOpe, string pStrFromDate, string pStrToDate)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("DATE", StrDate, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FROMDATE", pStrFromDate, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("TODATE", pStrToDate, DbType.DateTime, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_ProductionReportDetail", CommandType.StoredProcedure);

                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}
