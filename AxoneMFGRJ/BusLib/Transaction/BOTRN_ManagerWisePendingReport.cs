using AxonDataLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using System.Data;


namespace BusLib.Transaction
{
	public class BOTRN_ManagerWisePendingReport
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataSet ManagerWiseGetData(string StrShape_ID, string StrColor_ID, string StrClarity_ID, string StrSize_ID, Int32 intDepartment_ID)
		{
			DataSet DS = new DataSet();

			Ope.ClearParams();
			Ope.AddParams("SHAPE_ID", StrShape_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("COLOR_ID", StrColor_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("CLARITY_ID", StrClarity_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("SIZE_ID", StrSize_ID, DbType.String, ParameterDirection.Input);
			Ope.AddParams("DEPARTMENT_ID", intDepartment_ID, DbType.Int32, ParameterDirection.Input);

    		Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "TEMP", "RP_ManagerWisePendingReport_GetData", CommandType.StoredProcedure);
            //Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "TEMP", "RP_ManagerWisePendingReport_GetData_Pinali", CommandType.StoredProcedure);

			return DS;
		}

		public DataTable ManagerWiseDetailGetData(string StrEmpCode)
		{
			DataTable Dtab = new DataTable();

			Ope.ClearParams();
			Ope.AddParams("EMPCODE", StrEmpCode, DbType.Date, ParameterDirection.Input);

			Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "", CommandType.StoredProcedure);

			return Dtab;
		}

	}
}
