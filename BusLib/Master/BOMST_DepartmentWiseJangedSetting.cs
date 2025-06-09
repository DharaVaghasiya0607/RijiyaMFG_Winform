using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;
using System.Data;

namespace BusLib.Master
{
    public class BOMST_DepartmentWiseJangedSetting
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
           
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_DepartmentWiseJangedSettingGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DepartmentWiseJangedSettingProperty Save(DepartmentWiseJangedSettingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ID", pClsProperty.ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("STARTFROMPROCESS_ID", pClsProperty.STARTFROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("STARTTOPROCESS_ID", pClsProperty.STARTTOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("STARTFROMDEPARTMENT_ID", pClsProperty.STARTFROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("STARTTODEPARTMENT_ID", pClsProperty.STARTTODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("STARTENTRYTYPE", pClsProperty.STARTENTRYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENDFROMPROCESS_ID", pClsProperty.ENDFROMPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENDTOPROCESS_ID", pClsProperty.ENDTOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENDFROMDEPARTMENT_ID", pClsProperty.ENDFROMDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENDTODEPARTMENT_ID", pClsProperty.ENDTODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENDENTRYTYPE", pClsProperty.ENDENTRYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_DepartmentWiseJangedSettingSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch(Exception Ex)
            {
                pClsProperty.ReturnValue = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = Ex.Message;
            }
            return pClsProperty;
        }

        public DepartmentWiseJangedSettingProperty Delete(DepartmentWiseJangedSettingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ID", pClsProperty.ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_DepartmentWiseJangedSettingDelete", CommandType.StoredProcedure);

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
