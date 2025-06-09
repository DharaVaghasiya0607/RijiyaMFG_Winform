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
    public class BOTRN_SingleBreakingPacketEntry
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataSet GetBreakingPacketDetail(int pBreakingType_ID = 0,string pStrKapanName = "", int pIntPacketNo = 0, string pStrTag = "", Int64 PacketID = 0, Int64 EmployeeID = 0, Int64 ManagerID = 0)
        {
            DataSet DSet = new DataSet();
            Ope.ClearParams();
            //Ope.AddParams("OPE", "FRESH", DbType.String, ParameterDirection.Input);
            Ope.AddParams("BREAKINGTYPE_ID", pBreakingType_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DSet, "Temp", "TRN_SingleBreakingPacketDetGetData", CommandType.StoredProcedure);
            return DSet;
        }

        public DataTable GetBreakingList(string pStrKapan, Int32 pIntFromPktNo, Int32 pIntToPktNo, string pStrTag, string pStrEmployee_ID, string pStrBreakingType_ID, string pStrFromDate, string pStrToDate)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKETNO", pIntFromPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", pIntToPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BREAKINGTYPE_ID", pStrBreakingType_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleBreakingPacketListGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetBreakingListPktInfo(string pStrKapan, Int32 pIntFromPktNo, Int32 pIntToPktNo, string pStrTag, string pStrEmployee_ID, string pStrBreakingType_ID, string pStrFromDate, string pStrToDate, Int64 pIntBarcode)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKETNO", pIntFromPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", pIntToPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BREAKINGTYPE_ID", pStrBreakingType_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pIntBarcode, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleBreakingPacketListInfoGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public TRN_SingleBreakingPacketEntry Save(TRN_SingleBreakingPacketEntry pClsProperty, string pStrBeforeBrkDetail, string pStrAfterBrkDetail)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("GROUP_ID", pClsProperty.GROUP_ID, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("BREAKINGTYPE_ID", pClsProperty.BREAKINGTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BREAKINGDATE", pClsProperty.BREAKINGDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("BREAKINGEMPLOYEE_ID", pClsProperty.BREAKINGEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ISCONSIDERORIGINAL", pClsProperty.ISCONSIDERORIGINAL, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("DIFFAMOUNT", pClsProperty.DIFFAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("XMLBEFOREBRKDETAIL", pStrBeforeBrkDetail, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("XMLAFTERBRKDETAIL", pStrAfterBrkDetail, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BREAKINGREASON_ID", pClsProperty.BREAKINGREASON_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleBreakingPacketDetSave", CommandType.StoredProcedure);

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

        public TRN_SingleBreakingPacketEntry Delete(TRN_SingleBreakingPacketEntry pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("BREAKING_ID", pClsProperty.BREAKING_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("GROUP_ID", pClsProperty.GROUP_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleBreakingPacketDetDelete", CommandType.StoredProcedure);

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


