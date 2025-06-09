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
    public class BOTRN_PacketControlNoMapping
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public ArrayList Save(string pStrMappingType, string pStockPacketControlMappingXml)
        {
             ArrayList AL = new ArrayList(3);
            try
            {
                Ope.ClearParams();

                Ope.AddParams("MAPPINGTYPE", pStrMappingType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLSTOCKPACKETCONTROLMAPPING", pStockPacketControlMappingXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketGIAControlNoSave", CommandType.StoredProcedure);

                
               
            }
            catch (System.Exception ex)
            {
                AL[0] = "";
                AL[1] = "FAIL";
                AL[2] = ex.Message;

            }
            return AL;

        }

        //public TrnStockTallyProperty Delete(TrnStockTallyProperty pClsProperty)
        //{
        //    try
        //    {
        //        Ope.ClearParams();

        //        Ope.AddParams("STOCKTALLYDATE", pClsProperty.STOCKTALLYDATE, DbType.Date, ParameterDirection.Input);
        //        Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
        //        Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

        //        Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

        //        ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_StockTalllyDelete", CommandType.StoredProcedure);

        //        if (AL.Count != 0)
        //        {
        //            pClsProperty.ReturnValue = Val.ToString(AL[0]);
        //            pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
        //            pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
        //        }

        //    }
        //    catch (System.Exception ex)
        //    {
        //        pClsProperty.ReturnValue = "";
        //        pClsProperty.ReturnMessageType = "FAIL";
        //        pClsProperty.ReturnMessageDesc = ex.Message;

        //    }
        //    return pClsProperty;

        //}

        public DataSet GetData(string pStrMappingType)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("MAPPINGTYPE", pStrMappingType, DbType.String, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_PacketControlMapGetData", CommandType.StoredProcedure);
            return DS;
        }

        public DataTable GetSearchData(string pStrMappingType, string pStrKapanName , string pStrPacketNo ,string pStrTag , string pStrControlNo)
        {
            Ope.ClearParams();
            DataTable Dt = new DataTable();
            Ope.AddParams("MAPPINGTYPE", pStrMappingType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pStrPacketNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("CONTROLNO", pStrControlNo, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dt, "Trn_PacketGIAControlNoSearchData", CommandType.StoredProcedure);
            return Dt;
        }

        //public DataTable Print(string pStrDate, int pIntDepartment, Int64 pIntEmployee)
        //{
        //    //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
        //    Ope.ClearParams();
        //    DataTable DTab = new DataTable();

        //    Ope.AddParams("STOCKDATE", pStrDate, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("EMPLOYEE_ID", pIntDepartment, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("DEPARTMENT_ID", pIntEmployee, DbType.Int32, ParameterDirection.Input);
            
        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_StockTalllyPrint", CommandType.StoredProcedure);

        //    //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

        //    return DTab;
        //}


    }
}
