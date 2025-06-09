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
    public class BOMST_ParaReportCriteria
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrDiamondType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("DIAMONDTYPE", pStrDiamondType, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ParaReportCriteriaGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParaReportCriteriaMasterProperty Save(ParaReportCriteriaMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("CRITERIA_ID", pClsProperty.CRITERIA_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("FROMSHAPE_ID", pClsProperty.FROMSHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOSHAPE_ID", pClsProperty.TOSHAPE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMCOLOR_ID", pClsProperty.FROMCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOCOLOR_ID", pClsProperty.TOCOLOR_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMCLARITY_ID", pClsProperty.FROMCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOCLARITY_ID", pClsProperty.TOCLARITY_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("FROMCUT_ID", pClsProperty.FROMCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOCUT_ID", pClsProperty.TOCUT_ID, DbType.Int32, ParameterDirection.Input);
                
                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("DIAMONDTYPE", pClsProperty.DIAMONDTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ParaReportCriteriaSave", CommandType.StoredProcedure);

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

        public ParaReportCriteriaMasterProperty Delete(ParaReportCriteriaMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("POLISHTRANSCRIETEARIA_ID", pClsProperty.CRITERIA_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_PolishTransCrieteariaDelete", CommandType.StoredProcedure);

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
