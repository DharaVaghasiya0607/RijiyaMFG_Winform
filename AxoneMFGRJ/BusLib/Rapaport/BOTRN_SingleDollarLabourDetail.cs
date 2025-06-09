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

namespace BusLib.Rapaport
{
    public class BOTRN_SingleDollarLabourDetail
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetData(int pIntShapeID, int pIntYear,int pIntMonth)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("SHAPE_ID", pIntShapeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("YY", pIntYear, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MM", pIntMonth, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleDollarLabourPriceGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public PriceHeadDetailProperty DollarLabourQuickUpload(PriceHeadDetailProperty pClsProperty, string pStrDollarLabourXML)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("XMLDOLLARLABOURDETAIL", pStrDollarLabourXML, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("YY", pClsProperty.YY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleDollarLabourQuickUpload", CommandType.StoredProcedure);

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

        public int DeleteShapeWiseDollarLabourDetail(int pIntYear, int pIntShape_ID)
        {
            try
            {
                Ope.ClearParams();
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Delete From TRN_SingleDollarLabourPrice With(RowLock) Where  YY = '" + pIntYear.ToString() + "' AND Shape_ID = '" + pIntShape_ID.ToString() + "'", CommandType.Text);

            }
            catch (System.Exception ex)
            {
                return 0;
            }

        }


        public int CopyPasteDollarLabour(int pIntFromYear, int pIntToYear,int pIntFromMonth,int pIntToMonth) //int pIntFromMonth, , int pIntToMonth
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("FROMYY", pIntFromYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMMM", pIntFromMonth, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("TOYY", pIntToYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMM", pIntToMonth, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "TRN_SingleDollarLabourPriceCopyPaste", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return -1;
            }
        }

    }
}
