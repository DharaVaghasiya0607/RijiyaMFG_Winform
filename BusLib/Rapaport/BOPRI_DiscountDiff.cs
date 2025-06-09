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

namespace BusLib.Rapaport
{
    public class BOPRI_DiscountDiff
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Pri_DiscountDiffGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public PriDiscountDiffProperty Save(PriDiscountDiffProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("DISCOUNT_ID", pClsProperty.DISCOUNT_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("S_CODE", pClsProperty.S_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("C_CODE", pClsProperty.C_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("Q_CODE", pClsProperty.Q_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FL_CODE", pClsProperty.FL_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CUT_CODE", pClsProperty.CUT_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("POL_CODE", pClsProperty.POL_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SYM_CODE", pClsProperty.SYM_CODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("F_CARAT", pClsProperty.FROMCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("T_CARAT", pClsProperty.TOCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("DISCOUNTDIFF", pClsProperty.DISCOUNTDIFF, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "PRI_DiscountDiffSave", CommandType.StoredProcedure);

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

        public PriDiscountDiffProperty Delete(PriDiscountDiffProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("DISCOUNT_ID", pClsProperty.DISCOUNT_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_DiscountDiffDelete", CommandType.StoredProcedure);

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
