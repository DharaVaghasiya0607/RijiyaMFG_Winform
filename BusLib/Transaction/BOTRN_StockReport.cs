using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AxonDataLib;
using System.Data;
using System.Collections;
using System.Diagnostics;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;

namespace BusLib.Transaction
{
    public class BOTRN_StockReport
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Get Stock Data


        public DataTable GetStockReportGetData(string pStrFromIssueDate, string pstrToIssueDate, string pStrKapan, string pStrProcess, string pStrRoughName, string pStrGroup)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMISSUEDATE", pStrFromIssueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TOISSUEDATE", pstrToIssueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pStrProcess, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ROUGH_ID", pStrRoughName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("GROUP1", pStrGroup, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_StockReport", CommandType.StoredProcedure);

            return DTab;
        }


  


        #endregion
    }
}
