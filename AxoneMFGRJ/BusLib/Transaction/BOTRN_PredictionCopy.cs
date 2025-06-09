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
    public class BOTRN_PredictionCopy
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public TrnStockTallyProperty Save(TrnStockTallyProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("STOCKTALLYDATE", pClsProperty.STOCKTALLYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("FOUNDSTATUS", pClsProperty.FOUNDSTATUS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_StockTalllySave", CommandType.StoredProcedure);

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

        public TrnStockTallyProperty Delete(TrnStockTallyProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("STOCKTALLYDATE", pClsProperty.STOCKTALLYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_StockTalllyDelete", CommandType.StoredProcedure);

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


        public DataTable GetPredictionCopyPacketDetail(int pIntDepartment, Int64 pIntEmployee, bool pIsWithExtraStock,string pStrXmlPacketTag)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmployee, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKETTAGXML", pStrXmlPacketTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PredictionCopyPacketDetailGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public int CopyPridiction(string pStrXml, int pStrFPrdType, int pStrTPrdType, Int64 pIntEmployee) 
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLPRIDICTIONCOPY", pStrXml, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pIntEmployee, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMPRDTYPE_ID", pStrFPrdType, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOPRDTYPE_ID", pStrTPrdType, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Trn_PridictionCopy", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return -1;
            }
        }

      
        public DataTable Print(string pStrDate, int pIntDepartment, Int64 pIntEmployee)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("STOCKDATE", pStrDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntDepartment, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntEmployee, DbType.Int32, ParameterDirection.Input);
            
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_StockTalllyPrint", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DTab;
        }


    }
}
