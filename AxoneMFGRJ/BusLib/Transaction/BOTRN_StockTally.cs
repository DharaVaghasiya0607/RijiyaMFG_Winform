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
    public class BOTRN_StockTally
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

                Ope.AddParams("SCANDEPARTMENT_ID", pClsProperty.SCANDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PKTSRNO", pClsProperty.PKTSRNO, DbType.Int32, ParameterDirection.Input);

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
        public TrnStockTallyProperty SaveNew(TrnStockTallyProperty pClsProperty)
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

                Ope.AddParams("SCANDEPARTMENT_ID", pClsProperty.SCANDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PKTSRNO", pClsProperty.PKTSRNO, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_StockTalllySaveNew", CommandType.StoredProcedure);

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
        public DataTable SaveDirectScan(TrnStockTallyProperty pClsProperty, string pStrDirectScanXml) //#P : 22-06-2022
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("STOCKTALLYDATE", pClsProperty.STOCKTALLYDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("XMLDIRECTSCANDETAIL", pStrDirectScanXml, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("FOUNDSTATUS", pClsProperty.FOUNDSTATUS, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANDEPARTMENT_ID", pClsProperty.SCANDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("SCANEMPLOYEECODE", Config.gEmployeeProperty.LEDGERCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANHOSTNAME", Config.ComputerName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            //Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            //Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            //Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_StockTallySave_DirectScan", CommandType.StoredProcedure);

            return DTab;

            //if (AL.Count != 0)
            //{
            //    pClsProperty.ReturnValue = Val.ToString(AL[0]);
            //    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
            //    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            //}
        }
        public DataTable SaveDirectScanNew(TrnStockTallyProperty pClsProperty, string pStrDirectScanXml) //#P : 22-06-2022
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("STOCKTALLYDATE", pClsProperty.STOCKTALLYDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("XMLDIRECTSCANDETAIL", pStrDirectScanXml, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("FOUNDSTATUS", pClsProperty.FOUNDSTATUS, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANDEPARTMENT_ID", pClsProperty.SCANDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("SCANEMPLOYEECODE", Config.gEmployeeProperty.LEDGERCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANHOSTNAME", Config.ComputerName, DbType.String, ParameterDirection.Input);

            Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            //Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            //Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            //Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_StockTallySave_DirectScanNew", CommandType.StoredProcedure);

            return DTab;

            //if (AL.Count != 0)
            //{
            //    pClsProperty.ReturnValue = Val.ToString(AL[0]);
            //    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
            //    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            //}
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
        public TrnStockTallyProperty DeleteNew(TrnStockTallyProperty pClsProperty)
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

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_StockTalllyDeleteNew", CommandType.StoredProcedure);

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
        public DataSet GetData(string pStrDate, int pIntDepartment, Int64 pIntEmployee, bool pIsWithExtraStock, Int64 pIntManager)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("STOCKDATE", pStrDate, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("EMPLOYEE_ID", pIntEmployee, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pIntManager, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_StockTalllyGetData", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DS;
        }
        public DataSet GetDataNew(string pStrDate, int pIntDepartment, Int64 pIntEmployee, bool pIsWithExtraStock, Int64 pIntManager)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("STOCKDATE", pStrDate, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("EMPLOYEE_ID", pIntEmployee, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pIntManager, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_StockTalllyGetDataNew", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DS;
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

        public DataTable PrintHistorySave(string pStrPrintDataSaveXml, string pStrScanEmployeeCode, string pStrScanHostName, string pStrScanIPAddress, string pStrEmpMngr, string pStrMessage, string PstrMessageTally)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("STOCKTALLYPRINTXML", pStrPrintDataSaveXml, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANEMPLOYEECODE", pStrScanEmployeeCode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANHOSTNAME", pStrScanHostName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SCANIPADDRESS", pStrScanIPAddress, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPMNGR", pStrEmpMngr, DbType.String, ParameterDirection.Input);
            Ope.AddParams("STOCKTALLYMESSAGE", pStrMessage, DbType.String, ParameterDirection.Input);
            Ope.AddParams("STOCKCONFIRMTALLYMESSAGE", PstrMessageTally, DbType.String, ParameterDirection.Input); // K : 07/12/2022
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_StockTalllyPrintHistorySave", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetDetailData(string pStrDate,int pIntDepartment)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("STOCKDATE", pStrDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment, DbType.Int32, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_StockTallyDetailGetData", CommandType.StoredProcedure);

            return DS;
        }

    }
}
