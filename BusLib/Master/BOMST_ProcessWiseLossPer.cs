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
    public class BOMST_ProcessWiseLossPer

    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();



        public DataTable Fill(int pIntprocess, int pIntDeaprtment, string pStrFromType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("PROCESS_ID", pIntprocess, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDeaprtment, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FORMTYPE", pStrFromType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ProcessLossGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ProcessMasterProperty Save(ProcessMasterProperty pClsProperty, string pStrFromType)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("FORMTYPE", pStrFromType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGHTYPE", pClsProperty.RoughType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PER_ID", pClsProperty.PER_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PER", pClsProperty.PER, DbType.String, ParameterDirection.Input);
                Ope.AddParams("NOOFISSUE", pClsProperty.NoOfIssue, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISIssueReturnLock", pClsProperty.ISIssueReturnLock, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("AGEINGHOURS", pClsProperty.AGEINGHOURS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ProcessLossSave", CommandType.StoredProcedure);

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
        public ProcessMasterProperty Delete(ProcessMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PER_ID", pClsProperty.PER_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ProcessLossDelete", CommandType.StoredProcedure);

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
