using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using System.Data;
using System.Collections;

namespace BusLib.Transaction
{
	public class BOTRN_PolishOkPacketUpdate
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataTable Fill(string pStrFromDate)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("DATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PolishOKPacketGetData", CommandType.StoredProcedure);
			return DTab;
		}

		public string UpdateData(string Xml)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("XMLDATA", Xml, DbType.Xml, ParameterDirection.Input);
			Ope.AddParams("MESSAGEDESC", "", DbType.String, ParameterDirection.Output);

			ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PolishOKPacketUpdate", CommandType.StoredProcedure);

			if (AL.Count != 0)
			{
				return Val.ToString(AL[0]);
			}
			else
			{
				return "FAIL";
			}
		}

        public DataTable GetDataForPolishOkTransferPacketHistory(string pStrKapanName) // Dhara : 20-04-2023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PolishOkTransferPacketHistoryGetData", CommandType.StoredProcedure);
            return DTab;
        }

	}
}
