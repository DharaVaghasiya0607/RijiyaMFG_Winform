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
    public class BOTRN_PacketCreate
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataRow GetBalancePcsCarat(string StrKapanName , Guid StrKapan_ID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT BALANCEPCS,BALANCECARAT FROM TRN_KAPAN WITH(NOLOCK) WHERE KAPAN_ID = '"+ StrKapan_ID +"'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }


        public bool CheckPacketNoExists(Guid StrKapan_ID, int ProcessID,int IntPacketNo)
        {
            
            Ope.ClearParams();

            string StrQuery = " And KAPAN_ID = '" + StrKapan_ID + "' AND PROCESS_ID = '" + ProcessID + "' And PACKETNO= '" + IntPacketNo + "'";

            string StrPacketNo = Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_PacketMaster", "PACKETNO", StrQuery);

        
            if (StrPacketNo == "")
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        public int FindNewPacketNo(Guid pGuidKapanID, int pIntProcessID)
        {
            string StrQuery = " And KAPAN_ID = '" + pGuidKapanID + "' AND PROCESS_ID = '" + pIntProcessID + "'";

            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "Trn_PacketMaster", "MAX(PACKETNO)", StrQuery);

            return IntNewID;
        }

        public TrnPacketCreationProperty SaveProcessIssue(TrnPacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PURITY_ID", pClsProperty.PURITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CHARNI_ID", pClsProperty.CHARNI_ID, DbType.Int32, ParameterDirection.Input);
                                
                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("SIZE", pClsProperty.SIZE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISSUEDATE", pClsProperty.ISSUEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISSUEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ISSUEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EXPPER", pClsProperty.EXPPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPCARAT", pClsProperty.EXPCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXPLOSSPER", pClsProperty.EXPLOSSPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPLOSSCARAT", pClsProperty.EXPLOSSCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LABOURTYPE", pClsProperty.LABOURTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABOURRATE", pClsProperty.LABOURRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
               
                //Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                //Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);               
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_ProcessIssueSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[4]);

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




        public TrnPacketCreationProperty SavePacketReturn(TrnPacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                
                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PURITY_ID", pClsProperty.PURITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CHARNI_ID", pClsProperty.CHARNI_ID, DbType.Int32, ParameterDirection.Input);
                            
                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
               

                Ope.AddParams("RETURNDATE", pClsProperty.RETURNDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("RETURNBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RETURNPCS", pClsProperty.RETURNPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RETURNCARAT", pClsProperty.RETURNCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RETURNPER", pClsProperty.RETURNPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KACHAPCS", pClsProperty.KACHAPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KACHACARAT", pClsProperty.KACHACARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("CANCELPCS", pClsProperty.CANCELPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CANCELCARAT", pClsProperty.CANCELCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);



                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketReturnSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[4]);

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




        public TrnPacketCreationProperty UpdatePacket(TrnPacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("PROCESS_ID", pClsProperty.PROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PURITY_ID", pClsProperty.PURITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CHARNI_ID", pClsProperty.CHARNI_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISSUEDATE", pClsProperty.ISSUEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISSUEPCS", pClsProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RETURNDATE", pClsProperty.RETURNDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("RETURNBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RETURNPCS", pClsProperty.RETURNPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RETURNCARAT", pClsProperty.RETURNCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RETURNPER", pClsProperty.RETURNPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KACHAPCS", pClsProperty.KACHAPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KACHACARAT", pClsProperty.KACHACARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("CANCELPCS", pClsProperty.CANCELPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CANCELCARAT", pClsProperty.CANCELCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOSSCARAT", pClsProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LABOURTYPE", pClsProperty.LABOURTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABOURRATE", pClsProperty.LABOURRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketReturnSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[4]);

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

        public DataTable GetJangedPrintData(string pStrJangedNo, string pStrRoughName)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();
         
      
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ROUGH_ID", pStrRoughName, DbType.String, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketPrint", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DTab;
        }

        public DataTable GetLottingPrintData(string pStrRoughName,int pIntProcessID)
        {
            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t Start", BLL.GlobalDec.gStrComputerIP);
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("ROUGH_ID", pStrRoughName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PROCESS_ID", pIntProcessID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", "", DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketPrint", CommandType.StoredProcedure);

            //ObjFile.Log(new StackTrace().GetFrame(0).GetMethod().Name, "", "\t End", BLL.GlobalDec.gStrComputerIP);

            return DTab;
        }

        public DataTable GetLabourRateAndType(Int32 IntProcessID, Int32 IntCharniID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            string StrQuery = "Select * From Trn_LabourRate WITH(NOLOCK) WHERE PROCESS_ID = '" + IntProcessID + "' AND CHARNI_ID = '" +IntCharniID+ "'";

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

            return DTab;
        }
        public bool CheckMixPacketNoExists(string pStrKapanName, int IntPacketNo) // Add Dhara : 20-04-2022
        {

            Ope.ClearParams();

            string StrQuery = " And KAPANNAME = '" + pStrKapanName + "' AND PACKETNO= '" + IntPacketNo + "'";

            string StrPacketNo = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_MIXPACKETMASTER", "PACKETNO", StrQuery);


            if (StrPacketNo == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public int FindNewMixPacketNo(Int64 pIntKapanID)// Chnage For Get Parcel Packet No // Dhara : 20-04-2022
        {
            string StrQuery = " And KAPAN_ID = '" + pIntKapanID + "'";
            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "TRN_MIXPACKETMASTER", "MAX(PACKETNO)", StrQuery);
            return IntNewID;
        }

        public DataRow GetMainPktBalancePcsCarat(string StrPacketNo, Int64 StrPacket_ID) //Dhara : 20-04-2022
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT BALANCEPCS,BALANCECARAT FROM TRN_SINGLEPACKETMASTER WITH(NOLOCK) WHERE PACKET_ID = '" + StrPacket_ID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }
        public TrnSinglePacketCreationProperty SaveProcessIssueForMix(TrnSinglePacketCreationProperty pClsProperty) // Dhara : 20-04-2022
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("MAINPACKET_ID", pClsProperty.MAINPACKET_ID, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYTYPE", pClsProperty.ENTRYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("ISSUEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ISSUEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EXPPER", pClsProperty.EXPPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPCARAT", pClsProperty.EXPCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("MARKERCODE", pClsProperty.MARKERCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MARKER_ID", pClsProperty.MARKER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANMAINMANAGER_ID", pClsProperty.KAPANMAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);//GUNJAN : 27/03/2023

                Ope.AddParams("AUTOCONFIRM", pClsProperty.AUTOCONFIRM, DbType.Boolean, ParameterDirection.Input);


                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueBarcodeno", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_ProcessIssueSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[3]);
                    pClsProperty.ReturnValueJangedNo = Val.ToString(AL[4]);
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
        public DataTable PopupJangedForPrint(string pStrFormType, Int64 pIntJangedNo = 0, string pStrDate = null) // Dhara : 20-04-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRANSDATE", pStrDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrint", CommandType.StoredProcedure);

            return DTab;
        }
        public DataTable PopupJangedForParcelPrint(string pStrFormType, Int64 pIntJangedNo = 0, string pStrDate = null, string pStrOpe = "") //#P : 24-08-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FORMTYPE", pStrFormType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TRANSDATE", pStrDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleIssueReturnGetJangedNoPrintForParcel", CommandType.StoredProcedure);

            return DTab;
        }
        public TrnSinglePacketCreationProperty MixToSinglePacketCreate(TrnSinglePacketCreationProperty pClsProperty) // #P : 29-08-2022
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BALANCECARAT", pClsProperty.BALANCECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRANSFERCARAT", pClsProperty.TRANSFERCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MixToSinglePacketCreateSave", CommandType.StoredProcedure);

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
