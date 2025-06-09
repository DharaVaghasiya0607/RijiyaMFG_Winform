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
    public class BOTRN_LabRecheckConfirm
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetLabRecheck(string pStrOpe, string StrKapanName, int StrFromPacketNo, int strToPacketNo, string strTag, string StrFromDate, string StrToDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", StrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPAKECTNO", StrFromPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPAKECTNO", strToPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", strTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_LabRecheckConfirmGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public int SaveRecheckConfirm(string pGuidPacket_ID, string pLabResultStatus, string pGuidPrdId) //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Update TRN_SinglePacketMaster With(RowLock) Set LabResultStatus = '" + pLabResultStatus + "' WHERE PACKET_ID ='" + pGuidPacket_ID + "'";
            Str = Str + "Update TRN_SinglePrd With(RowLock) Set CurrentLabResultStatus = '" + pLabResultStatus + "' WHERE PRD_ID = '" + pGuidPrdId + "'  ";
            Str = Str + "Update TRN_SinglePrd With(RowLock) Set CurrentLabResultStatus = '" + pLabResultStatus + "' WHERE PACKET_ID = '" + pGuidPacket_ID + "' And PrdType_ID = 14 "; //update LabInclusion CurrStatus also
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }
        public int SaveRepairingConfirmForGrading(string pGuidPacket_ID, string pGrdResultStatus, string pGuidPrdId, Int64 pIntEmployee_ID) //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Update TRN_SinglePacketMaster With(RowLock) Set GrdResultStatus = '" + pGrdResultStatus + "' WHERE PACKET_ID ='" + pGuidPacket_ID + "'";
            Str = Str + "Update TRN_SinglePrd With(RowLock) Set CurrentGrdResultStatus = '" + pGrdResultStatus + "' WHERE PRD_ID = '" + pGuidPrdId + "'  ";
            Str = Str + "Update TRN_SinglePrd With(RowLock) Set CurrentGrdResultStatus = '" + pGrdResultStatus + "' WHERE PACKET_ID = '" + pGuidPacket_ID + "' And Employee_ID = " + pIntEmployee_ID + " And PrdType_ID = 8 "; //update GradingPrd CurrStatus also
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }



    }
}
