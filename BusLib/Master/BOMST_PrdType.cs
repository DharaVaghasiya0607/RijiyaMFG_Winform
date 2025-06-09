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
    public class BOMST_PrdType
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_PrdTypeGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public MSTPrdTypeMasterProperty Save(MSTPrdTypeMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDTYPENAME", pClsProperty.PRDTYPENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPECODE", pClsProperty.PRDTYPECODE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISKAPAN", pClsProperty.ISKAPAN, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISPACKETNO", pClsProperty.ISPACKETNO, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISTAG", pClsProperty.ISTAG, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISEMPLOYEE", pClsProperty.ISEMPLOYEE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMANAGER", pClsProperty.ISMANAGER, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISGRAPH", pClsProperty.ISGRAPH, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISEXP", pClsProperty.ISEXP, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMAK", pClsProperty.ISMAK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISPOL", pClsProperty.ISPOL, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("TFLAG", pClsProperty.TFLAG, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("SEQUENCENO", pClsProperty.SEQUENCENO, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("REQPRDTYPE_ID", pClsProperty.REQPRDTYPE_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PARAMCHECKPRDTYPE_ID", pClsProperty.PARAMCHECKPRDTYPE_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARAMCHECKBREAKINGTYPE_ID", pClsProperty.PARAMCHECKBREAKINGTYPE_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RAPCALCPER", pClsProperty.RAPCALCPER, DbType.Decimal, ParameterDirection.Input);
				Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_PrdTypeSave", CommandType.StoredProcedure);

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

        public MSTPrdTypeMasterProperty Delete(MSTPrdTypeMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_PrdTypeDelete", CommandType.StoredProcedure);

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
