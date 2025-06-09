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
    public class BOTRN_CrapsIssueReturn
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public TrnCrapsIssueReturnProperty CrapsSave(TrnCrapsIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                //Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("READYPCS", pClsProperty.READYPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSTPCS", pClsProperty.LOSTPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOSTCARAT", pClsProperty.LOSTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRANSDATE", pClsProperty.TRANSDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TRANSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRANSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                //Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);
                //Ope.AddParams("ReturnValueJangedNoRet", "", DbType.String, ParameterDirection.Output);
                //Ope.AddParams("ReturnValueJangedNoTran", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_CrapsIssueReturnSave", CommandType.StoredProcedure);
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

        public DataSet GetTransactionViewData(string Kapan, Int64 KapanId, Int32 PacketNo, string FromDate, string ToDate, Int64 pIntMarker_ID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("KAPANNAME", Kapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", KapanId, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", PacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", FromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", ToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("MARKER_ID", pIntMarker_ID, DbType.Int64, ParameterDirection.Input);

            //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CrapsIssueReturnGetData", CommandType.StoredProcedure);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_CrapsIssueReturnGetData", CommandType.StoredProcedure);
            return DS;
        }
        public TrnCrapsIssueReturnProperty RepairingEntrySave(TrnCrapsIssueReturnProperty pClsProperty) // K: 17/12/2022
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNTYPE", pClsProperty.RETURNTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("READYPCS", pClsProperty.READYPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSTPCS", pClsProperty.LOSTPCS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOSTCARAT", pClsProperty.LOSTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRANSDATE", pClsProperty.TRANSDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TRANSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRANSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RepairingEntrySave", CommandType.StoredProcedure);
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
        public DataSet GetRepairingEntryData(string Kapan, Int64 KapanId, Int32 PacketNo, Int32 Party_ID, string FromDate, string ToDate)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("KAPANNAME", Kapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPAN_ID", KapanId, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PARTY_ID", Party_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", PacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", FromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", ToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_RepairingEntryGetData", CommandType.StoredProcedure);
            return DS;
        }
    }
}
