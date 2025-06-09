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

namespace BusLib.Dashboard
{
    public class BODS_PlanningDashboard
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataSet Fill(string pStrFromDate, string pStrToDate, string pStrRoughType,string pStrXML,string pStrViewType)
        {
            Ope.ClearParams();
            Ope.AddParams("Ope", "Summary", DbType.String, ParameterDirection.Input);
            Ope.AddParams("FromDate", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToDate", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("RoughType", pStrRoughType, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("XMLFilter", pStrXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("Shape", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("SizeName", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PrdType", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("ViewType", pStrViewType, DbType.String, ParameterDirection.Input);

            DataSet DS = new DataSet();
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Dashboard_PlanningWithLockAndOrder", CommandType.StoredProcedure);
            return DS;
        }

        public DataSet FillDetail(string pStrFromDate, string pStrToDate, string pStrRoughType, string pStrXML, string pStrShape,string pStrSize,string pStrPrdType)
        {
            Ope.ClearParams();
            Ope.AddParams("Ope", "Detail", DbType.String, ParameterDirection.Input);
            Ope.AddParams("FromDate", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToDate", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("RoughType", pStrRoughType, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("XMLFilter", pStrXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("Shape", pStrShape, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SizeName", pStrSize, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PrdType", pStrPrdType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ViewType", "", DbType.String, ParameterDirection.Input);

            DataSet DS = new DataSet();
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Dashboard_Planning", CommandType.StoredProcedure);
            return DS;
        }
    }
}
