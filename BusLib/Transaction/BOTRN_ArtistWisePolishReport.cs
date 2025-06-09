using AxonDataLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;

namespace BusLib.Transaction
{
	public class BOTRN_ArtistWisePolishReport
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataTable FindEmployee()
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("OPE", "EMPLOYEE", DbType.String, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "GetEmpCodeFroArtistReport", CommandType.StoredProcedure);

			return DTab;
		}

		public DataTable FindManager()
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();

			Ope.AddParams("OPE", "MANAGER", DbType.String, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "GetEmpCodeFroArtistReport", CommandType.StoredProcedure);

			return DTab;
		}

		public DataTable GetDataOfArtistWiseReport(string pStrFromDate, string pStrToDate, string pStrEmpCode, string pStrManagerCode)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("EMPCODE", pStrEmpCode, DbType.String, ParameterDirection.Input);
			Ope.AddParams("MANAGERCODE", pStrManagerCode, DbType.String, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_ArtistWisePolishReportGetData", CommandType.StoredProcedure);

			return DTab;
		}

		public DataTable GetDetailOfArtistWiseReport(string pStrEmpCode, string pStrDate, string StrFromDT, string StrToDT)//, Int64 pStrEmp_ID)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("EMPCODE", pStrEmpCode, DbType.String, ParameterDirection.Input);
			Ope.AddParams("DATE", pStrDate, DbType.Date, ParameterDirection.Input);

			Ope.AddParams("FROMDATE", StrFromDT, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("TODATE", StrToDT, DbType.Date, ParameterDirection.Input);
			//Ope.AddParams("EMPLOYEE_ID", pStrEmp_ID, DbType.Int64, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_ArtistWisePolishReportDetail", CommandType.StoredProcedure);

			return DTab;
		}
	}
}
