using AxonDataLib;
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
    public class BOTRN_SaleDemandEntry
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetParameterData()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            string StrQuery = "SELECT ISNULL(PARA_ID,0) AS PARA_ID,ISNULL(PARACODE,'') AS PARACODE ,ISNULL(PARANAME,'') AS PARANAME , ISNULL(REMARK,'') AS REMARK,ISNULL(LABCODE,'') AS LABCODE, PARATYPE FROM MST_PARA WITH(NOLOCK) WHERE 1=1 ";
            //DataRow DrParam =  Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            //return Val.ToInt32(DrParam[0]);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);
            return DTab;
        }

        public string OrderDemandSave(string StrOrderDemandXml, string StrMode, string StrOrder)
        {
            Ope.ClearParams();

            Ope.AddParams("ORDERDEMANDXML", StrOrderDemandXml, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("MODE", StrMode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ORDERNO", StrOrder, DbType.String, ParameterDirection.Input);

            Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SaleDemandSave", CommandType.StoredProcedure);
            string Res = "";

            if (AL.Count != 0)
            {
                Res = Val.ToString(AL[1]);
            }
            else
            {
                Res = Val.ToString(AL[0]);
                Res = Val.ToString(AL[1]);
                Res = Val.ToString(AL[2]);
            }
            return Res;
        }

        public string OrderDemandDelete(string StrMode, Guid pStrDemand_ID)
        {
            Ope.ClearParams();

            Ope.AddParams("MODE", StrMode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SALEDEMAND_ID", pStrDemand_ID, DbType.Guid, ParameterDirection.Input);

            Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SaleDemandSave", CommandType.StoredProcedure);
            string Res = "";

            if (AL.Count != 0)
            {
                Res = Val.ToString(AL[1]);
            }
            else
            {
                Res = Val.ToString(AL[0]);
                Res = Val.ToString(AL[1]);
                Res = Val.ToString(AL[2]);
            }
            return Res;
        }


        public DataTable OrderDetail(string FromOrderDate, string ToOrderDate, string FromCompDate, string ToCompDate, string pStrOpe, string pStrSaleDemand_ID)
        {
            DataTable Dtab = new DataTable();

            Ope.ClearParams();

            Ope.AddParams("FROMORDERDATE", FromOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TOORDERDATE", ToOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FROMCOMORDERDATE", FromCompDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TOCOMORDERDATE", ToCompDate, DbType.Date, ParameterDirection.Input);

            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SALEDEMAND_ID", pStrSaleDemand_ID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "Trn_SaleDemandGetDataDetail", CommandType.StoredProcedure);

            return Dtab;
        }
        public int OrderDemandUpdateCompDate(string pStrSaleDemand_ID, int pIntISOrderComplete)
        {
            int IntRes = 0;
            Ope.ClearParams();
            string StrQuery = "Update Trn_SaleDemand With(RowLock) Set ISOrderComplete = " + pIntISOrderComplete + ", OrderCompleteUpdateDate = GetDate() Where Convert(Varchar(50),SaleDemand_ID) = '" + pStrSaleDemand_ID + "'";
            //Ope.AddParams("ORDERDEMANDXML", StrOrderDemandXml, DbType.Xml, ParameterDirection.Input);
            IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            return IntRes;

        }
    }
}
