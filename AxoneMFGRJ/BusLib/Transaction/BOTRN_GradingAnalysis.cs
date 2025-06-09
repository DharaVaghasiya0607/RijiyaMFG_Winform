using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxonDataLib;
using BusLib.Configuration;
using System.Data;

namespace BusLib.ReportGrid
{
	public class BOTRN_GradingAnalysis
	{
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

		public DataSet GetDataForGradingAnalysis(string StrFromDate, string StrToDate, string StrType, string StrLab, string StrMonthly, string StrGroup, string StrKapan)
		{
			try
			{
				DataSet Ds = new DataSet();

				Ope.ClearParams();
				Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
				Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);
				Ope.AddParams("TYPE", StrType, DbType.String, ParameterDirection.Input);
				Ope.AddParams("LAB", StrLab, DbType.String, ParameterDirection.Input);
				Ope.AddParams("MONTHLY", StrMonthly, DbType.String, ParameterDirection.Input);
				Ope.AddParams("GROUP", StrGroup, DbType.String, ParameterDirection.Input);
				Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, Ds, "Temp", "RP_GradingAnalysisReportGetData", CommandType.StoredProcedure);
				return Ds;
			}
			catch (Exception Ex)
			{
				return null;
			}
		}

		public DataTable GerGradingComparisionDetail(string pStrFromDate, string pStrToDate, 
		  string pStrClickType,
		  string pStrRowValue,
		  string pStrColValue,
		  int pShape_ID,
		  string pStrSize,
			string StrGroup,
			string StrType,
			string StrLab

		  )
		{
			try
			{
				DataTable DTab = new DataTable();

				Ope.ClearParams();
				Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
				Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
				//Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
				Ope.AddParams("CLICKTYPE", pStrClickType, DbType.String, ParameterDirection.Input);
				Ope.AddParams("ROWVALUE", pStrRowValue, DbType.String, ParameterDirection.Input);
				Ope.AddParams("COLVALUE", pStrColValue, DbType.String, ParameterDirection.Input);
				Ope.AddParams("SHAPE_ID", pShape_ID, DbType.Int32, ParameterDirection.Input);
				Ope.AddParams("SIZE", pStrSize, DbType.String, ParameterDirection.Input);
				Ope.AddParams("GROUP", StrGroup, DbType.String, ParameterDirection.Input);
				Ope.AddParams("TYPE", StrType, DbType.String, ParameterDirection.Input);
				Ope.AddParams("LAB", StrLab, DbType.String, ParameterDirection.Input);

				Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_GradingAnalysisReportDetail", CommandType.StoredProcedure);
				return DTab;
			}
			catch (Exception Ex)
			{
				return null;
			}
		}
        public DataSet GetDataForMFGPrdComparision(string StrFromDate, string StrToDate, string StrType, string StrKapan)
        {
            try
            {
                DataSet Ds = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", StrType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, Ds, "Temp", "RP_MFGPrdComparision", CommandType.StoredProcedure);
                return Ds;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }

	}
}
