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
    public class BOTRN_TesnSakhatLabourPer 
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(int pIntYear, int pIntMonth, string pStrDollarLabourType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("YY", pIntYear, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MM", pIntMonth, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("LABOURTYPE", pStrDollarLabourType, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_TenSakhatLabourPerGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public TrnTenSakhatLabourPerProperty Save(TrnTenSakhatLabourPerProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LABOUR_ID", pClsProperty.LABOUR_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("YY", pClsProperty.YY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LABOURTYPE", pClsProperty.LABOURTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PER", pClsProperty.PER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LABOURTYPE_ID", pClsProperty.LABOURTYPE_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABOURTYPENAME", pClsProperty.LABOURTYPENAME, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABOUR_SRNO", pClsProperty.LABOUR_SRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_TenSakhatLabourPerSave", CommandType.StoredProcedure);

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

        public TrnTenSakhatLabourPerProperty Delete(TrnTenSakhatLabourPerProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LABOUR_ID", pClsProperty.LABOUR_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_TenSakhatLabourPerDelete", CommandType.StoredProcedure);

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
        public int CopyPasteTenSakhatLabourPerData(int pIntFromYear, int pIntFromMonth, int pIntToYear, int pIntToMonth, string pStrDollarType)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("FROMYY", pIntFromYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMMM", pIntFromMonth, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("TOYY", pIntToYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMM", pIntToMonth, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LABOURTYPE", pStrDollarType, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_TenSakhatLabourPerCopyPaste", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return -1;
            }
        }
      

    }
}
