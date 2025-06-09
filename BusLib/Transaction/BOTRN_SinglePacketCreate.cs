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
    public class BOTRN_SinglePacketCreate
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataRow GetBalancePcsCarat(string StrKapanName, Int64 pIntKapan_ID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT BALANCEPCS,BALANCECARAT FROM TRN_KAPAN WITH(NOLOCK) WHERE KAPAN_ID = '" + pIntKapan_ID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }


        public bool CheckPacketNoExists(string pStrKapanName, int IntPacketNo, string StrTag)
        {

            Ope.ClearParams();

            string StrQuery = " And KAPANNAME = '" + pStrKapanName + "' AND PACKETNO= '" + IntPacketNo + "' And Tag = '" + StrTag + "'";

            string StrPacketNo = Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketMaster", "PACKETNO", StrQuery);


            if (StrPacketNo == "")
            {
                return false;
            }
            else
            {
                return false;
            }
        }
        public int FindNewPacketNo(string pStrKapanName)
        {
            string StrQuery = " And KAPANNAME = '" + pStrKapanName + "'";

            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketMaster", "MAX(PACKETNO)", StrQuery);

            return IntNewID;
        }

        //Changed : Pinali : 17-07-2019 For Generate No SubLot Wise
        public TrnSinglePacketCreationProperty FindNewPacketNoWithKapan(TrnSinglePacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNVALUEMAXPACKETNO", pClsProperty.ReturnValueMaxPacketNo, DbType.Int32, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "GeneratePacketNoKapanSubLotWise", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValueMaxPacketNo = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                pClsProperty.ReturnValueMaxPacketNo = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = ex.Message;

            }
            return pClsProperty;
        }

        public TrnSinglePacketCreationProperty FindRoughType(TrnSinglePacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNROUGHTYPE", pClsProperty.RETURNROUGHTYPE, DbType.Int32, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanRoughTypeGetData", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.RETURNROUGHTYPE = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                pClsProperty.ReturnValueMaxPacketNo = "";
                pClsProperty.ReturnMessageType = "FAIL";
                pClsProperty.ReturnMessageDesc = ex.Message;

            }
            return pClsProperty;
        }

        public TrnSinglePacketCreationProperty CheckValidationForPrevPacketDetail(TrnSinglePacketCreationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PREVKAPANNAME", pClsProperty.PREVKAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETNO", pClsProperty.PREVPACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETTAG", pClsProperty.PREVPACKETTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RETURNVALUE", 0, DbType.Int32, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketCreateValSaveforPrevPktDetail", CommandType.StoredProcedure);

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


        public TrnSinglePacketCreationProperty CreatePacket(TrnSinglePacketCreationProperty pClsProperty, string pStrMode)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("MODE", pStrMode, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKETGRADE_ID", pClsProperty.PACKETGRADE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("KAPANCATEGORY", pClsProperty.KAPANCATEGORY, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RFIDTAG", pClsProperty.RFIDTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MARKER_ID", pClsProperty.MARKER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("PREVKAPANNAME", pClsProperty.PREVKAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETNO", pClsProperty.PREVPACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETTAG", pClsProperty.PREVPACKETTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVPACKET_ID", pClsProperty.PREVPACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETCARAT", pClsProperty.PREVPACKETCARAT, DbType.Decimal, ParameterDirection.Input);


                Ope.AddParams("PACKETGROUP_ID", pClsProperty.PACKETGROUP_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PKTSERIALNO", pClsProperty.PKTSERIALNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY_ID", pClsProperty.PACKETCATEGORY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PLANNINGGRADE_ID", pClsProperty.PLANNINGGRADE_ID, DbType.Int32, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketCreate", CommandType.StoredProcedure);

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


        public DataRow GetSinglePacketDetail(string pStrKapanName, int pIntPacketNo)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetData", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return null;
            }

        }
        public DataRow GetSinglePacketDetail(string pStrKapanName, string pStrSubLot, string pStrSubLot1, int pIntPacketNo)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBLOT", pStrSubLot, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBLOT1", pStrSubLot1, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
                return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetData", CommandType.StoredProcedure);

            }
            catch (System.Exception ex)
            {
                return null;
            }

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

        public DataTable GetLottingPrintData(string pStrRoughName, int pIntProcessID)
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

            string StrQuery = "Select * From Trn_LabourRate WITH(NOLOCK) WHERE PROCESS_ID = '" + IntProcessID + "' AND CHARNI_ID = '" + IntCharniID + "'";

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

            return DTab;
        }


        public DataTable FindKapan()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANTYPE", "ALL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "FindKapan", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable FindKapan(string pStrConnectionString)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANTYPE", "ALL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(pStrConnectionString, Config.ProviderName, DTab, "FindKapan", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable FindKapanSubLot(string pStrKapanName)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            string StrQuery = "Select DISTINCT SUBLOT From TRN_Kapan WITH(NOLOCK) WHERE KapanNAME = '" + pStrKapanName + "'";

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

            return DTab;
        }
        public DataTable FindKapanSubLot1(string pStrKapanName, string pStrSubLot)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            string StrQuery = "Select DISTINCT SUBLOT1 From TRN_Kapan WITH(NOLOCK) WHERE KapanNAME = '" + pStrKapanName + "' And SUBLOT = '" + pStrSubLot + "'";

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

            return DTab;
        }

        public string FindKapanID(string pStrKapanName)
        {
            Ope.ClearParams();

            string StrQuery = " And KapanNAME = '" + pStrKapanName + "'";

            return Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_Kapan", "KAPAN_ID", StrQuery);

        }
        public string FindKapanCategory(string pStrKapanName) //Add : Pinali : 25-10-2019
        {
            Ope.ClearParams();

            string StrQuery = " And KapanNAME = '" + pStrKapanName + "'";

            return Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_Kapan", "KAPANCATEGORY", StrQuery);

        }



        public int UpdateMainMarkerIDInPacketMaster(string pStrOpe, string pStrPacketID, Int64 pIntMarkerID)
        {
            Ope.ClearParams();
            string StrQuery = "";
            if (pStrOpe == "Marker")
            {
                StrQuery = "Update TRN_SinglePacketMaster WITH(ROWLOCK)  SET Marker_ID = '" + pIntMarkerID + "' WHERE Packet_ID = '" + pStrPacketID + "' ";
            }
            else if (pStrOpe == "Worker")
            {
                StrQuery = "Update TRN_SinglePacketMaster WITH(ROWLOCK)  SET Worker_ID = '" + pIntMarkerID + "' WHERE Packet_ID = '" + pStrPacketID + "' ";
            }

            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

        }

        public TrnSinglePacketCreationProperty CreateMixPacket(TrnSinglePacketCreationProperty pClsProperty, string pStrMode) // Dhara : 18-04-2022
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("MODE", pStrMode, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANCATEGORY", pClsProperty.KAPANCATEGORY, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RFIDTAG", pClsProperty.RFIDTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MARKER_ID", pClsProperty.MARKER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input); // Add Dhara : 18-04-2022
                Ope.AddParams("PACKETGROUP_ID", pClsProperty.PACKETGROUP_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("STRXMLFORREJECTION", pClsProperty.STRXMLFORREJECTION, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("OPEREJECT", pClsProperty.OPEREJECT, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTCARAT", pClsProperty.REJECTCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MixPacketCreate", CommandType.StoredProcedure);

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

        public bool CheckMixPacketNoExists(string pStrKapanName, int IntPacketNo) // Add Dhara : 18-04-2022
        {

            Ope.ClearParams();

            string StrQuery = " And KAPANNAME = '" + pStrKapanName + "' AND PACKETNO= '" + IntPacketNo + "'";

            string StrPacketNo = Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketMaster", "PACKETNO", StrQuery);


            if (StrPacketNo == "")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public DataRow GetMixPacket_ID(string StrKapanName, Int64 pIntKapan_ID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT PACKET_ID FROM TRN_SINGLEPACKETMASTER WITH(NOLOCK) WHERE KAPAN_ID = '" + pIntKapan_ID + "' AND PACKETNO = '" + 0 + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }
        public DataTable FindMakPacketNo(Int64 pIntKapan_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            string StrQuery = "Select DISTINCT PACKET_ID, PACKETNO From TRN_SINGLEPACKETMASTER WITH(NOLOCK) WHERE Kapan_ID = '" + pIntKapan_ID + "' AND PACKETNO = '0'";

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);

            return DTab;
        }

        public DataRow GetMakPktBalancePcsCarat(Int64 pIntKapan_ID, Int64 pIntMainPacket_ID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT BALANCEPCS,BALANCECARAT FROM Trn_SinglePacketMaster WITH(NOLOCK) WHERE KAPAN_ID = '" + pIntKapan_ID + "' And Packet_ID = '" + pIntMainPacket_ID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }
        public TrnSinglePacketCreationProperty CreateMakeblePacket(TrnSinglePacketCreationProperty pClsProperty, string pStrMode)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("MODE", pStrMode, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MAINPACKET_ID", pClsProperty.MAINPACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANCATEGORY", pClsProperty.KAPANCATEGORY, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RFIDTAG", pClsProperty.RFIDTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOTPCS", pClsProperty.LOTPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);

                // Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MARKER_ID", pClsProperty.MARKER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOEMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOMANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TODEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("PREVKAPANNAME", pClsProperty.PREVKAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETNO", pClsProperty.PREVPACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETTAG", pClsProperty.PREVPACKETTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVPACKET_ID", pClsProperty.PREVPACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PREVPACKETCARAT", pClsProperty.PREVPACKETCARAT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("PACKETTYPE", pClsProperty.PACKETTYPE, DbType.String, ParameterDirection.Input); // Add Dhara : 18-04-2022

                Ope.AddParams("PACKETGROUP_ID", pClsProperty.PACKETGROUP_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValueJangedNo", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MakeblePacketCreate", CommandType.StoredProcedure);

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

        public int FindNewMixPacketNo(Int64 pIntKapan_ID)
        {
            string StrQuery = " And KAPAN_ID = '" + pIntKapan_ID + "' AND PACKETTYPE = 'ORIGINAL' AND PACKETCATEGORY = 'MIX'";

            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketMaster", "MAX(PACKETNO)", StrQuery);

            return IntNewID;
        }
        public int FindNewMakPacketNo(Int64 pIntKapan_ID)
        {
            string StrQuery = " And KAPAN_ID = '" + pIntKapan_ID + "' AND PACKETCATEGORY = 'MIX' AND PACKETTYPE = 'ORIGINAL'";

            int IntNewID = Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketMaster", "MAX(PACKETNO)", StrQuery);

            return IntNewID;
        }

        public TrnSinglePacketCreationProperty PacketCaratUpdate(TrnSinglePacketCreationProperty pClsproperty) //#K : 09-11-2022
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsproperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsproperty.KAPAN_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsproperty.LOTCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsproperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsproperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsproperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SinglePacketCreateUpdate", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsproperty.ReturnValue = Val.ToString(AL[0]);
                    pClsproperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsproperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (Exception ex)
            {
                pClsproperty.ReturnValue = "";
                pClsproperty.ReturnMessageType = "FAIL";
                pClsproperty.ReturnMessageDesc = ex.Message;
            }
            return pClsproperty;
        }

    }
}
