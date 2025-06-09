using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using System.Data;
using System.Collections;
using BusLib.TableName;

namespace BusLib.Transaction
{
    public class BOTRN_KapanEsstiment
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataTable Fill(string pStrKapanName, Int64 pIntKapan_ID)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", pIntKapan_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_KapanEsstimentGetData", CommandType.StoredProcedure);
			return DTab;
		}

        public TrnKapanEsstimentProperty Save(TrnKapanEsstimentProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLFORKAPANESSTIMENT", pClsProperty.XMLFORKAPANESSTIMENT, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanEsstimentdSave", CommandType.StoredProcedure);

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
