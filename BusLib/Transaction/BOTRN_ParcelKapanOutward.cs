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

namespace BusLib.Transaction
{
  public  class BOTRN_ParcelKapanOutward
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper(Config.ConnectionString);
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        public KapanOutwardProperty SaveKapanOutward(KapanOutwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("OUTWARD_ID", pClsProperty.OUTWARD_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("INWARDCARAT", pClsProperty.INWARDCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("ASSORTCARAT", pClsProperty.ASSORTCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("OUTWARDDATE", pClsProperty.OUTWARDDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("RATERS", pClsProperty.RATERS, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ParcelKapanOutward_Save", CommandType.StoredProcedure);

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
        public DataTable FillOutward(string StrFromDate, string StrToDate, string StrKapanName)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", StrKapanName, DbType.String, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ParcelKapanOutward_GetData", CommandType.StoredProcedure);
            return DTab;
        }
    }
}
