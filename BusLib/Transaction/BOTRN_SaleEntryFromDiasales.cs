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
    public class BOTRN_SaleEntryFromDiasales
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable FetchSaleDataFromDiasales(string StrKapanName)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANNAME", StrKapanName, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString_Mumbai, Config.ProviderName_Mumbai, DTab, "TRN_SalesEntryFromDiaSalesGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable SaveSalesDataFromDiasales(string StrXml) // ADD : D: 24-10-2020
        {

            DataTable DTab = new DataTable();

            Ope.ClearParams();

            Ope.AddParams("DIASALESXML", StrXml, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SalesEntryFromDiaSalesSave", CommandType.StoredProcedure);
            return DTab;
        }


    }
}
