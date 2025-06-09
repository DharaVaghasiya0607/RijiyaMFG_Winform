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

namespace BusLib.Polish
{
    public class BOTRN_PolishTransaction
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetDataForPolishPacketLiveStock(string StrOpe,string StrManager, string Kapanname, string pStrDisplayType, Int64 pIntEmp_ID, Boolean pBoolIsTransaction, Boolean pBoolIsMerge)
        {
            Ope.ClearParams();
            DataTable Dtab = new DataTable();
            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER", StrManager, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", Kapanname, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplayType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmp_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ISTRANSACTION", pBoolIsTransaction, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISMERGE", pBoolIsMerge, DbType.Boolean, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "TRN_PolishPacketGetDataLiveStock", CommandType.StoredProcedure);
            return Dtab;
        }


        public bool CheckMixPacketNoExists(string pStrKapanName, int IntPacketNo)
        {
            Ope.ClearParams();

            string StrQuery = " And KAPANNAME = '" + pStrKapanName + "' AND PACKETNO= '" + IntPacketNo + "'";

            string StrPacketNo = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_POLISHPACKETMASTER", "PACKETNO", StrQuery);


            if (StrPacketNo == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int FindNewPolishOKPacketNo(Int64 pIntKapanID, Int64 pIntMainPolishOkPacket_ID)// Chnage For Get Parcel Packet No // Dhara : 20-04-2022
        {
            string StrQuery = " And KAPAN_ID = '" + pIntKapanID + "' AND MainPolishPacket_ID = '"+pIntMainPolishOkPacket_ID+"' ";
            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "TRN_PolishPacketMaster", "MAX(PACKETNO)", StrQuery);
            return IntNewID;
        }


        public DataRow GetMainPktBalancePcsCarat(string StrKapan, Int64 StrKapan_ID,Int64 StrPacket_ID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT BALANCEPCS,BALANCECARAT FROM TRN_POLISHPACKETMASTER WITH(NOLOCK) WHERE KAPAN_ID = '" + StrKapan_ID + "' AND POLISHPACKET_ID = ' " + StrPacket_ID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }


        public TrnPolishPacketIssueReturn SaveProcessIssueForPolish(TrnPolishPacketIssueReturn pClsProperty)
        {
            try
            {
                 Ope.ClearParams();
                Ope.AddParams("MAINPOLISHPAKET_ID", pClsProperty.MAINPOLISHPACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("POLISHPACKET_ID", pClsProperty.POLISHPACKET_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ENTRYTYPE", pClsProperty.ENTRYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("ISSUEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ISSUEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETCATEGORY", pClsProperty.PACKETCATEGORY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);              
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);
               
                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PolishProcessIssueSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);                 
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[3]);
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

        public TrnSingleIssueReturnProperty TransferGoodsPolish(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);             
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);              

                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);    

                Ope.AddParams("READYPCS", pClsProperty.READYPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("READYCARAT", pClsProperty.READYCARAT, DbType.Double, ParameterDirection.Input);
                
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("PREVEMPLOYEE_ID", pClsProperty.PREVEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PREVMANAGER_ID", pClsProperty.PREVMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("PACKETCATEGORY", pClsProperty.PACKETCATEGORY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input);
               
                Ope.AddParams("TRANSDATE", pClsProperty.TRANSDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TRANSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TRANSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
               
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishReturnSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[3]);

                    pClsProperty.JANGEDNO = Val.ToInt64(pClsProperty.ReturnValueJangedNo);
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

        public DataTable GetDataForPolishAssortment()
        {
            Ope.ClearParams();
            DataTable Dtab = new DataTable();

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "TRN_PolishGetDataForAssortment", CommandType.StoredProcedure);
            return Dtab;
        }

        public DataTable PopupJangedForPolishPrint( Int64 pIntJangedNo = 0, string pStrOpe = "") 
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            //Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
            //Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            //Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
           // Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            //Ope.AddParams("TRANSDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrintForPolish", CommandType.StoredProcedure);

            return DTab;
        }

        public TrnSingleIssueReturnProperty KapanMerge(TrnSingleIssueReturnProperty pClsProperty)//Gunjan : 31/03/2023
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANMERGEXML", pClsProperty.StrKapanMergeXml, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_PolishKapanMerge", CommandType.StoredProcedure);

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