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
    public class BOTRN_PacketUnlockSetting
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrFromDate,string pStrToDate,string pStrKapan)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "UNLOCKPACKETDETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketUnlockEntryGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataRow GetScanPacketDetail(string pStrKapan, int pIntPacketNo, string pStrTag)
        {
            Ope.ClearParams();
            Ope.AddParams("OPE", "SCANPACKETDETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_PacketUnlockEntryGetData", CommandType.StoredProcedure);
        }

        public TrnPacketUnlockSettingProperty Save(TrnPacketUnlockSettingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("UNLOCK_ID", pClsProperty.UNLOCK_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("UNLOCKDATE", pClsProperty.UNLOCKDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("UNLOCKSETTINGTYPE", pClsProperty.UNLOCKSETTINGTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("TOTALHOURS", pClsProperty.TOTALHOURS, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("FROMDATETIME", pClsProperty.FROMDATETIME, DbType.DateTime, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketUnlockEntrySave", CommandType.StoredProcedure);

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
        public TrnPacketUnlockSettingProperty Delete(TrnPacketUnlockSettingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("UNLOCK_ID", pClsProperty.UNLOCK_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketUnlockEntryDelete", CommandType.StoredProcedure);

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
