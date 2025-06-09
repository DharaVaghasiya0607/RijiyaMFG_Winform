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
    public class BOTRN_PurchaseLiveStock
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataRow IsKapanNameExists(string StrKapanName)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "Select * From Trn_Kapan with(nolock) Where KAPANNAME+SUBLOT+SUBLOT1= '" + StrKapanName + "'  And Company_ID = '" + Config.gEmployeeProperty.COMPANY_ID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;                     
           
        }

       

        //public DataTable GetDataForPurchaseLiveStock(bool pBoolDispAllLot)
        //{
        //    Ope.ClearParams();
        //    DataTable DTab = new DataTable();

        //    Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int32, ParameterDirection.Input);
        //    Ope.AddParams("ISDISPLAYALLLOT", pBoolDispAllLot, DbType.Boolean, ParameterDirection.Input);


        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PurchaseLiveStockGetData", CommandType.StoredProcedure);
        //    return DTab;
        //}

      
    }
}
