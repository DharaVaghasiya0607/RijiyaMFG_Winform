using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxonDataLib;
using System.Data;

namespace BusLib.Transaction
{
	public class BOTRN_EmpWiseSplitReport
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
		AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataTable FindEmployee()
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();

			string StrQuery = "Select distinct LEDGERNAME,LEDGERCODE From MST_Ledger WITH(NOLOCK) WHERE 1=1";

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

			return DTab;
		}

		public DataTable GetDataEmpWise(string pStrFromDate, string pStrToDate, string pStrEmpCode)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("EMPLOYEECODE", pStrEmpCode, DbType.String, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_EmpWiseSplitReportGetData", CommandType.StoredProcedure);

			return DTab;
		}

		public DataTable GetDetailOfEmp(string pStrEmpCode, string pStrDate,string StrFromDT, string StrToDT)//, Int64 pStrEmp_ID)
		{
			Ope.ClearParams();
			DataTable DTab = new DataTable();
			Ope.AddParams("EMPCODE", pStrEmpCode, DbType.String, ParameterDirection.Input);
			Ope.AddParams("DATE", pStrDate, DbType.Date, ParameterDirection.Input);

			Ope.AddParams("FROMDATE", StrFromDT, DbType.Date, ParameterDirection.Input);
			Ope.AddParams("TODATE", StrToDT, DbType.Date, ParameterDirection.Input);
			//Ope.AddParams("EMPLOYEE_ID", pStrEmp_ID, DbType.Int64, ParameterDirection.Input);
			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_EmpWiseDetailGetData", CommandType.StoredProcedure);

			return DTab;
		}

	}
}
