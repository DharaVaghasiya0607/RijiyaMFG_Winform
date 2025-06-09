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
    public class BOTRN_LabourProcess
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(TrnLabourProcessProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("YYYY", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SUBPROCESS_ID", pClsProperty.SUBPROCESS_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ROUGHTYPE", pClsProperty.ROUGHTYPE, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_LabourProcessGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public TrnLabourProcessProperty Save(TrnLabourProcessProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("LABOURPROCESS_ID", pClsProperty.LABOURPROCESS_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("YYYY", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SUBPROCESS_ID", pClsProperty.SUBPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("LABOURTYPE", pClsProperty.LABOURTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ROUGHTYPE", pClsProperty.ROUGHTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_LabourProcessSave", CommandType.StoredProcedure);

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

        public TrnLabourProcessProperty Delete(TrnLabourProcessProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LABOURPROCESS_ID", pClsProperty.LABOURPROCESS_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_LabourProcessDelete", CommandType.StoredProcedure);

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
        public int CopyPasteLabourProcessData(int pIntFromYear, int pIntFromMonth, int pIntToYear, string pStrToMonth, int pIntFromProcess_ID, int pIntToProcess_ID, int pIntFromDept_ID, int pIntToDept_ID, Int64 pIntFromManager_ID, Int64 pIntToManager_ID, string pXmlYearMonth,int SubProcess_Id, string pStrRoughType)//int COPYTOYYYY,
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("FROMYY", pIntFromYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMMM", pIntFromMonth, DbType.Int32, ParameterDirection.Input);
                
                Ope.AddParams("TOYY", pIntToYear, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMM", pStrToMonth, DbType.Int32, ParameterDirection.Input);
                
                Ope.AddParams("FROMPROCESS_ID", pIntFromProcess_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMMANAGER_ID", pIntFromManager_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMDEPARTMENT_ID", pIntFromDept_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("TOPROCESS_ID", pIntToProcess_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pIntToManager_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pIntToDept_ID, DbType.Int32, ParameterDirection.Input);

                //Ope.AddParams("COPYTOYYYY", COPYTOYYYY, DbType.Int32, ParameterDirection.Input);//urvisha add by:05-04-2023
                Ope.AddParams("XMLYEARMONTH", pXmlYearMonth, DbType.Xml, ParameterDirection.Input);//urvisha add by:05-04-2023

                Ope.AddParams("SUBPROCESS_ID", SubProcess_Id, DbType.Int32, ParameterDirection.Input);//Gunjan:26/06/2023
                Ope.AddParams("ROUGHTYPE", pStrRoughType, DbType.String, ParameterDirection.Input);//Gunjan:26/06/2023

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "TRN_LabourProcessCopyPaste", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return -1;
            }
        }
        public DataTable DallarFill(string pIntKapaname, int pIntPacketNo, string pIntTag, Double pDouAmont, Boolean pIntExpDollar) // add by Urvisha : 11-04-2023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            
            Ope.AddParams("KAPANNAME", pIntKapaname, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pIntTag, DbType.String, ParameterDirection.Input);
            //Ope.AddParams("PACKET_ID", pIntPacket_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EXPDOLLARREVICE", pDouAmont, DbType.Decimal, ParameterDirection.Input);
            Ope.AddParams("EXPDOLLAR", pIntExpDollar, DbType.Decimal, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketDollarGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public TrnSinglePacketCreationProperty DollarUpdate(TrnSinglePacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("EXPDOLLARREVICE", pClsProperty.EXPDOLLARREVICE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_DollarUpdate", CommandType.StoredProcedure);

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
      

    }
}
