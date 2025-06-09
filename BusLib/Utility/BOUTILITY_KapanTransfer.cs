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
    public class BOUTILITY_KapanTransfer
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();



        public int KapanTransfer(string pStrKapanName, string pStrOpe)
        {
            int pIntReturnValue = 0;
            Ope.ClearParams();
            Ope.AddParams("Ope", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "A_KapanTransfer", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pIntReturnValue = Val.ToInt(AL[0]);
            }

            return pIntReturnValue;
        }


        public int KapanDelete(string pStrKapanName, string pStrOpe)
        {
            int pIntReturnValue = 0;
            Ope.ClearParams();
            Ope.AddParams("Ope", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "A_KapanTransfer", CommandType.StoredProcedure);


            if (AL.Count != 0)
            {
                pIntReturnValue = Val.ToInt(AL[0]);
            }

            return pIntReturnValue;
        }

      

    }
}
