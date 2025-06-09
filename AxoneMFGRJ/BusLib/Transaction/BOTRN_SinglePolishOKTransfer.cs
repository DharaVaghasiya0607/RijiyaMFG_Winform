using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;


namespace BusLib.Transaction
{
    public class BOTRN_SinglePolishOKTransfer
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable PolishOKTransferGetData(Int64 pStrbarcode, Int32 pStrsrNo, string pStrkapanName, string pStrpktNo, string pStrtag, Int64 pStrjangedNo, string pStrfromDate, string pStrtoDate, Int64 pStrManager_ID,Int64 pStrEmployee_ID, String pStrMainKapan)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("BARCODE", pStrbarcode, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SRNO", pStrsrNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrkapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pStrpktNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrtag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrjangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrfromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrtoDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pStrManager_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("CURREMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MAINKAPAN", pStrMainKapan, DbType.String , ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePolishOKTransferGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public PolishIssueReturnProperty SaveAndUpdate(string StrXML, string StrXMLSelection, PolishIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
               // Ope.AddParams("DETXML", strDETXML, DbTyp.Xml, ParameterDirection.Input);
                Ope.AddParams("XML", StrXML, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("XMLSELECTION", StrXMLSelection, DbType.Xml, ParameterDirection.Input);
                //Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishPacketIssueReturnSave", CommandType.StoredProcedure);

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


        public PolishIssueReturnProperty Update(string StrXML, string StrXMLSelection, PolishIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                 Ope.AddParams("XML", StrXML, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("XMLSELECTION", StrXMLSelection, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishPacketIssueReturnUpdate", CommandType.StoredProcedure);

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

        public PolishIssueReturnProperty UpdateKapanMerge(string StrXMLSelection, PolishIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLSELECTION", StrXMLSelection, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishPacketKapanMeregUpdate", CommandType.StoredProcedure);

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

        public PolishIssueReturnProperty SaveAndMergePacket(string StrXML, string StrXMLSelection, PolishIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("XML", StrXML, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("XMLSELECTION", StrXMLSelection, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishPacketMeregeSave", CommandType.StoredProcedure);

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

        public DataRow GetDataForFilter(Int64 pStrbarcode, Int32 pStrsrNo, string pStrkapanName, string pStrpktNo, string pStrtag, Int64 pStrjangedNo, Int64 pIntEmployee_ID)
        {
            
            Ope.ClearParams();

            Ope.AddParams("BARCODE", pStrbarcode, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SRNO", pStrsrNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrkapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pStrpktNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrtag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrjangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("CURREMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "TRN_SinglePolishOKTransferGetData", CommandType.StoredProcedure);
        }
        public DataTable ManualEntryGetData(string strFromDate, string strToDate, string strKapan, int strFromPkt, int strToPkt, string strEmployee_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", strFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", strToDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", strKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKET", strFromPkt, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKET", strToPkt, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", strEmployee_ID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_ManualEntryLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public PolishIssueReturnProperty FindNewPacketNoWithKapanForPolishOkTransfer(PolishIssueReturnProperty pClsProperty) // #Dhara : 20-04-2023 : For Generate New PacketNo For Polish Ok Transfer
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNVALUEMAXPACKETNO", pClsProperty.RETURNVALUEMAXPACKETNO, DbType.Int32, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "GeneratePolishOkTransferPacketNoKapanSubLotWise", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.RETURNVALUEMAXPACKETNO = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                pClsProperty.RETURNVALUEMAXPACKETNO = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = ex.Message;

            }
            return pClsProperty;
        }

        public DataTable PolishOKTransferMainManager(string pStrMainKapan, Int64 pIntEmployee)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("MAINKAPAN", pStrMainKapan, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmployee, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePolishOKTransferManinManager", CommandType.StoredProcedure);
            return DTab;
        }


        public PolishIssueReturnProperty DeleteFromPolishOkTransfer(PolishIssueReturnProperty pClsProperty, Int64 pIntPacket_ID, Int64 pIntPolishPacket_ID)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pIntPacket_ID, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHPACKET_ID", pIntPolishPacket_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PCS", pClsProperty.PCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PolishOkTransferPacketHistoryDelete", CommandType.StoredProcedure);

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
        

        public DataTable PolishOKTransferPrint(Int64 pStrbarcode, Int32 pStrsrNo, string pStrkapanName, string pStrpktNo, string pStrtag, Int64 pStrjangedNo, string pStrfromDate, string pStrtoDate, Int64 pStrManager_ID, Int64 pStrEmployee_ID, String pStrMainKapan)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("BARCODE", pStrbarcode, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SRNO", pStrsrNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrkapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pStrpktNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrtag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrjangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrfromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrtoDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pStrManager_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("CURREMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MAINKAPAN", pStrMainKapan, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePolishOKTransferGetDataForPrint", CommandType.StoredProcedure);
            return DTab;
        }
    }
}
