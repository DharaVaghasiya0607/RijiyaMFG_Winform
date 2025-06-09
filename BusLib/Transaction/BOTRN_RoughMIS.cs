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
    public class BOTRN_RoughMIS
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Issue

       
        public DataSet GetRoughMISAnalysis(string pStrInvoiceID, string pStrKapan,string pStrProcess)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("INVOICE_ID", pStrInvoiceID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pStrProcess, DbType.String, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp",  "MIS_KapanAnalysis", CommandType.StoredProcedure);

            return DS;
        }

        

        public DataTable GetRoughMISComparePivot(string pStrKapan)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("ROUGH_ID", pStrKapan, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MIS_KapanComparePivot", CommandType.StoredProcedure);

            return DTab;
        }


        public DataSet GetRoughMISCompareGrid(string pStrKapan, string pStrInvoiceNo, string pStrReport)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("KAPAN_ID", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("INVOICE_ID", pStrInvoiceNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("REPORT", pStrReport, DbType.String, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "MIS_KapanCompareGrid", CommandType.StoredProcedure);

          
            return DS;
        }


        public Int32 TruncateAllTable()
        {
            Ope.ClearParams();
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "A_TruncateAll", CommandType.StoredProcedure);

        }


        public DataSet GetRoughFinalMISAnalysis(string pStrRough, string pStrKapan)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("ROUGH_ID", pStrRough, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", pStrKapan, DbType.String, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "MIS_KapanAnalysisComplete", CommandType.StoredProcedure);

            return DS;
        }


        #endregion
    }
}
