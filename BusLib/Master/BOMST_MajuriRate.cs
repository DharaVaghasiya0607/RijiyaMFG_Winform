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

namespace BusLib.Master
{
    public class BOMST_MajuriRate
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(int pIntYear, int pIntMonth)
        {
            DataTable DTab = new DataTable();
            Ope.AddParams("YYYY", pIntYear, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MM", pIntMonth, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_MajuriRateGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public MajuriRateProperty Save(MajuriRateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("MAJURI_ID", pClsProperty.MAJURI_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SIZENAME", pClsProperty.SIZENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMAMT", pClsProperty.FROMAMT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOAMT", pClsProperty.TOAMT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("YYYY", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_MajuriInsertUpdate", CommandType.StoredProcedure);

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

        public MajuriRateProperty Delete(MajuriRateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("MAJURI_ID", pClsProperty.MAJURI_ID, DbType.Int32, ParameterDirection.Input);

                //Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                //Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_MajuriRateDelete", CommandType.StoredProcedure);

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

        public MajuriRateProperty SaveRate(MajuriRateProperty pClsProperty)
        {

            Ope.ClearParams();

            Ope.AddParams("RATE_ID", pClsProperty.RATE_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RATETYPE", pClsProperty.RATETYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RATE", pClsProperty.MAJURIRATE, DbType.Double, ParameterDirection.Input);

            Ope.AddParams("MEMOREPORTNOTE", pClsProperty.MEMOREPORTNOTE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_MemoAnalysisRateSave", CommandType.StoredProcedure);
            return pClsProperty;
        }
        public MajuriRateProperty SaveOtherRate(MajuriRateProperty pClsProperty)
        {

            Ope.ClearParams();

            Ope.AddParams("RATE_ID", pClsProperty.RATE_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RATETYPE", pClsProperty.RATETYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RATE", pClsProperty.MAJURIRATE, DbType.Double, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_MemoAnalysisReportOtherRateSave", CommandType.StoredProcedure);
            return pClsProperty;
        }

        public DataTable GetInfoByCode(string strKapName)
        {
            Ope.ClearParams();

            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", strKapName, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_MemoAnalysisRateGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetInfoByCodeForReportOther(string strKapName)
        {
            Ope.ClearParams();

            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", strKapName, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_MemoAnalysisReportOtherRateGetData", CommandType.StoredProcedure);
            return DTab;
        }


    }
}
