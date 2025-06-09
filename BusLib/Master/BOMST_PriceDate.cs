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

namespace BusLib.Master
{
    public class BOMST_PriceDate
    {

        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrGroup)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("PRICETYPE", pStrGroup, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_PriceDateGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public PriceDateMasterProperty Save(PriceDateMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PRICE_ID", pClsProperty.PRICE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRICETYPE", pClsProperty.PRICETYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRICEDATE", pClsProperty.PRICEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_PriceDateSave", CommandType.StoredProcedure);

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

        public DataTable SavePriceListUsingDataTable(string pStrStockUploadXML, Int32 pIntDepartment_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLSTOCKUPLOAD", pStrStockUploadXML, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment_ID, DbType.Int32, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_PriceChartBulkUpload", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetParcelPriceData(int pIntPriceID, int pIntShapeID, int pIntDepartmentID, string pStrPriceType) //WITHOUTSTOCK OR WITHSTOCK
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("PRICE_ID", pIntPriceID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRICETYPE", pStrPriceType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pIntShapeID, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartmentID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_PriceChartGetData", CommandType.StoredProcedure);

            return DTab;
        }


        public DataTable SaveSingleData(ParcelPriceChartProperty pClsProperty) //WITHOUTSTOCK OR WITHSTOCK
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("PRICE_ID", pClsProperty.PRICE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PRICEDATE", pClsProperty.PRICEDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("PRICETYPE", pClsProperty.PRICETYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("SIZE_ID", pClsProperty.MIXSIZE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MIXCLARITY_ID", pClsProperty.MIXCLARITY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_PriceChartSingleSave", CommandType.StoredProcedure);

            return DTab;
        }
    }
}
