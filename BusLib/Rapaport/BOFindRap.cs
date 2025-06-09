using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxonDataLib;
using BusLib.TableName;
using System.Collections;
using System.IO;

namespace BusLib.Rapaport
{
    public class BOFindRap
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        #region Form Operation

        //#Add By Milan : 12-05-2021
        public DataTable GetVelForRapCal(string PacketID)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_ValidationForRapCalc", CommandType.StoredProcedure);

            return DTab;
        }
        //#End Milan : 12-05-2021

        public DataTable GetPredictionData(int pPrdTypeID = 0, string PacketID = null, Int64 EmployeeID = 0, Int64 ManagerID = 0)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "FRESH", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetData", CommandType.StoredProcedure);

            return DTab;
        }


        public DataTable GetPredictionDataFromPrevious(int pPrdTypeID = 0, string PacketID = null, Int64 pIntEmployee_ID = 0)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "FETCH FROM LAST", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pIntEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable GetPredictionDataForPrint(int pPrdTypeID = 0, string PacketID = null, Int64 EmployeeID = 0, Int64 ManagerID = 0)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("USERNAME", BusLib.Configuration.BOConfiguration.gEmployeeProperty.USERNAME + " - " + BusLib.Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdPrint", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetAllParameterTable()
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select * From MST_Para With(NOLOCK) Where ISActive = 1";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }

        public DataTable GetPreditctionType(string pStrPrdType)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select * From MST_PrdType With(NOLOCK) Where 1=1 And ISActive = 1 And PrdType_ID In (" + pStrPrdType + ") Order By SequenceNo";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }

        public DataRow GetPacketDataRow(string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcodeNo = "",string pStrSrNoKapanName = "", int pIntPktSerialNo = 0)
        {
            //string Str = "SELECT * From Trn_SinglePacketMaster With(Nolock) WHERE (KapanName = '" + pStrKapanName + "' And PacketNo = " + pIntPacketNo + " And Tag = '" + pStrTag + "') Or Barcode = '" + pStrBarcodeNo + "' Or (KapanName = '" + pStrSrNoKapanName + "' And PktSerialNo = " + pIntPktSerialNo + ")";
            string Str = "SELECT Pkt.*,  Case When Isnull(Emp.EmpShortName,'') = '' Then Emp.LedgerCode Else Emp.EmpShortName End As TOEMPLOYEECODE,Case When Isnull(Mrk.EmpShortName,'') = '' Then Mrk.LedgerCode Else Mrk.EmpShortName End As MARKERCODE From Trn_SinglePacketMaster Pkt With(Nolock) ";
            Str = Str + " Left Join MST_Ledger Emp With(Nolock) On Emp.Ledger_ID = Pkt.ToEmployee_ID";
            Str = Str + " Left Join MST_Ledger Mrk With(Nolock) On Mrk.Ledger_ID = Pkt.Marker_ID";
            Str = Str + " WHERE (KapanName = '" + pStrKapanName + "' And PacketNo = " + pIntPacketNo + " And Tag = '" + pStrTag + "' And PktSerialNo <> 0) Or Barcode = '" + pStrBarcodeNo + "' Or (KapanName = '" + pStrSrNoKapanName + "' And PktSerialNo = '" + pIntPktSerialNo + "') OR (KapanName = '" + pStrKapanName + "' And PacketNo = " + pIntPacketNo + " And Tag = '" + pStrTag + "' And PktSerialNo = 0)";
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }


        public DataTable GetKapan()
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "KAPAN", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }



        public DataTable GetPacketNo(string pStrKapanName)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "PACKETNO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetTag(string pStrKapanName, int pIntPacketNo)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "TAG", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetPacketList(string pStrKapanName, int pIntPacketNo, string pStrTag) //Add : Pinali : 25-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "PACKETLIST", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }



        public DataTable GetRejectionPCNKapan() //Add : Pinali : 13-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "KAPAN", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionPCNKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetRejectionPCNPacketNo(string pStrKapanName) //Add : Pinali : 13-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "PACKETNO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionPCNKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetRejectionPCNTag(string pStrKapanName, int pIntPacketNo) //Add : Pinali : 13-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "TAG", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionPCNKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetRejectionPCNPacketList(string pStrKapanName, int pIntPacketNo, string pStrTag) //Add : Pinali : 13-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "PACKETLIST", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionPCNKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetRejectionPacketsWithRoughMakPrd(string pStrKapanName, int pIntPacketNo, string pStrTag) //Add : Pinali : 13-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "PCNPACKETSWITHROUGHMAKPRD", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionPCNKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }
        public string GetPacketFlName(string pStrKapanName, string pStrPacketNo, string pStrTag, string pStrBarcodeNo, string pStrSrNoKapanName, int pSrNoSerialNo)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();

                string StrQuery = "";
                StrQuery = "Select ISNULL(FL_ID,0) AS FL_ID From Trn_SinglePacketMaster Pkt WITH(NOLOCK) WHERE (Pkt.KapanName = '" + pStrKapanName + "' And Pkt.PacketNo = " + pStrPacketNo + " And Pkt.Tag = '" + pStrTag + "') Or Pkt.Barcode = '" + pStrBarcodeNo + "' Or (Pkt.KapanName = '" + pStrSrNoKapanName + "' And Pkt.PktSerialNo = " + pSrNoSerialNo + ") ";
                return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            }
            catch (Exception ex)
            {
                return "FAIL";
            }

        }
        //public DataTable GetEmployeeOSKapan()
        //{
        //    DataTable DTab = new DataTable();
        //    Ope.ClearParams();
        //    Ope.AddParams("OPE", "KAPAN", DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("KAPANNAME", "", DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
        //    return DTab;
        //}

        //public DataTable GetEmployeeOSPacketNo(string pStrKapanName)
        //{
        //    DataTable DTab = new DataTable();
        //    Ope.ClearParams();
        //    Ope.AddParams("OPE", "PACKETNO", DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
        //    return DTab;
        //}


        //public DataTable GetEmployeeOSTag(string pStrKapanName, int pIntPacketNo)
        //{
        //    DataTable DTab = new DataTable();
        //    Ope.ClearParams();
        //    Ope.AddParams("OPE", "TAG", DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
        //    Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
        //    Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
        //    return DTab;
        //}


        //select * from TRN_Rejection
        //WHERE KapanName = '195'
        //AND PacketNo = '99'
        //AND Tag = 'A'

        public Trn_RapSaveProperty CheckRejectionOut(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValCheckRejectionOut", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty CheckValForDesignation(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_ValidationForRapCalcEmpDesignation", CommandType.StoredProcedure);
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




        public Trn_RapSaveProperty ValSave(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSave", CommandType.StoredProcedure);
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


        //public Trn_RapSaveProperty ValidationForFinalPalnAmt(Trn_RapSaveProperty pClsProperty)
        //{
        //    try
        //    {
        //        Ope.ClearParams();

        //        Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
        //        Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
        //        Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
        //        Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
        //        Ope.AddParams("PLANAMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

        //        Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

        //        ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdVaidationForFinalAmount", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty ValSaveCheckWithMakable(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SHAPENAME", pClsProperty.SHAPENAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORNAME", pClsProperty.COLORNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CLARITYNAME", pClsProperty.CLARITYNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("CUT_ID", pClsProperty.CUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CUTNAME", pClsProperty.CUTNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("POL_ID", pClsProperty.POL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("POLNAME", pClsProperty.POLNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SYM_ID", pClsProperty.SYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYMNAME", pClsProperty.SYMNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FLNAME", pClsProperty.FLNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTSNAME", pClsProperty.NATTSNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LBLCNAME", pClsProperty.LBLCNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LAB_ID", pClsProperty.LAB_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LABCODE", pClsProperty.LABCODE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("HELIUMTOTALDEPTH", pClsProperty.HELIUMTOTALDEPTH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("HELIUMTABLEPC", pClsProperty.HELIUMTABLEPC, DbType.String, ParameterDirection.Input);
                Ope.AddParams("HELIUMRATIO", pClsProperty.HELIUMRATIO, DbType.String, ParameterDirection.Input);

                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("BALANCECARAT", pClsProperty.BALANCECARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LABPROCESS", pClsProperty.LABPROCESS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("DIAMIN", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DIAMAX", pClsProperty.DIAMAX, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("HEIGHT", pClsProperty.HEIGHT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DIAMETER", pClsProperty.DIAMETER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISBYPASSVALIDATION", pClsProperty.ISBYPASSVALIDATION, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ISBYPASSVALIDATIONFORPARAMETER", pClsProperty.ISBYPASSVALIDATIONFORPARAMETER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSaveCheckWithMakable", CommandType.StoredProcedure);
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


        public Trn_RapSaveProperty ValSaveCheckWithRoughType(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSaveCheckWithRoughType", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty ValSaveForUpdateOnlyTag(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValCheckForUpdateOnlyTag", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty ValSaveCheckWithRapCalcCarat(Trn_RapSaveProperty pClsProperty, string pStrPrdDetailXml) //Add : #P : 25-01-2020
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("XMLPRDDETAIL", pStrPrdDetailXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSaveCheckWithRapCalcCarat", CommandType.StoredProcedure);
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
        /*
        public Trn_RapSaveProperty ValSaveForCheckTFlagChangeValidation(Trn_RapSaveProperty pClsProperty, string pStrPrdDetailXml) //Add : #P : 08-12-2020
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("XMLPRDDETAIL", pStrPrdDetailXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSaveForCheckTFlagChangeValidation", CommandType.StoredProcedure);
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
        */

        public int DeleteAll(int PrdTypeID, string pStrKapanname, int pIntPacketNo, string pStrMTag, Int64 pIntEmployeeID, string pStrPacketID, bool pISTflag)
        {
            Ope.ClearParams();

            int IntRes = 0;

            string StrWagesBase = "";

            if (PrdTypeID == 8)
                StrWagesBase = "GRADING";
            else if (PrdTypeID == 9)
                StrWagesBase = "BOMBAY";
            else if (PrdTypeID == 11)
                StrWagesBase = "LAB";

            string Str1 = "", Str2 = "";
            Str1 = "Delete From Trn_SinglePrd With(RowLock) Where PrdType_ID = '" + PrdTypeID + "' And KapanName = '" + pStrKapanname + "' And PacketNo = '" + pIntPacketNo.ToString() + "' And MTag = '" + pStrMTag + "' And Employee_ID = '" + pIntEmployeeID + "' And Packet_ID = '" + pStrPacketID + "'";
            IntRes = Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str1, CommandType.Text);
            return IntRes;
        }



        public int UpdateTag(string pStrID, string StrTag, int pIntSrNo, string pStrKapanname, string pStrPacketNo, string pStrOldTag, bool ISTflagEntry)
        {
            if (pStrID != "")
            {
                Ope.ClearParams();
                string Str = "";

                if (ISTflagEntry)
                {
                    Str = "Update Trn_SinglePrd With(RowLock) Set Tag = '" + StrTag + "', TagSrNo = '" + pIntSrNo + "' ,ISWagesUpdate = 0,ISWagesUpdateForBrkDiff = 0 Where ID = '" + pStrID + "'";
                }
                else
                {
                    Str = "Update Trn_SinglePrd With(RowLock) Set Tag = '" + StrTag + "', TagSrNo = '" + pIntSrNo + "' ,ISWagesUpdate = 0 Where ID = '" + pStrID + "'";
                }
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
            }
            return -1;
        }


        public Trn_RapSaveProperty Save(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ID", pClsProperty.ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE", pClsProperty.PRDTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PLANNO", pClsProperty.PLANNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAGSRNO", pClsProperty.TAGSRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CUT_ID", pClsProperty.CUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("POL_ID", pClsProperty.POL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYM_ID", pClsProperty.SYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("SAKHAT_ID", pClsProperty.SAKHAT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("BLACKINC_ID", pClsProperty.BLACKINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("OPENINC_ID", pClsProperty.OPENINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("WHITEINC_ID", pClsProperty.WHITEINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LUSTER_ID", pClsProperty.LUSTER_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("HA_ID", pClsProperty.HA_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PAV_ID", pClsProperty.PAV_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EYECLEAN_ID", pClsProperty.EYECLEAN_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATURAL_ID", pClsProperty.NATURAL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GRAIN_ID", pClsProperty.GRAIN_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GCARAT", pClsProperty.GCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GCOLOR_ID", pClsProperty.GCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GCLARITY_ID", pClsProperty.GCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GCUT_ID", pClsProperty.GCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GPOL_ID", pClsProperty.GPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GSYM_ID", pClsProperty.GSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DISCOUNT", pClsProperty.DISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNTDISCOUNT", pClsProperty.AMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPAPORT", pClsProperty.RAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PRICEPERCARAT", pClsProperty.PRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("GDISCOUNT", pClsProperty.GDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GAMOUNTDISCOUNT", pClsProperty.GAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GRAPAPORT", pClsProperty.GRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GPRICEPERCARAT", pClsProperty.GPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GAMOUNT", pClsProperty.GAMOUNT, DbType.Double, ParameterDirection.Input);

                //Add : Pinali : 07-09-2019
                Ope.AddParams("MDISCOUNT", pClsProperty.MDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MPRICEPERCARAT", pClsProperty.MPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MAMOUNT", pClsProperty.MAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("MGDISCOUNT", pClsProperty.MGDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MGPRICEPERCARAT", pClsProperty.MGPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MGAMOUNT", pClsProperty.MGAMOUNT, DbType.Double, ParameterDirection.Input);
                //End : Pinali : 07-09-2019

                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISFINAL", pClsProperty.ISFINAL, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("COMPUTERNAME", Config.ComputerName, DbType.String, ParameterDirection.Input);

                Ope.AddParams("TFLAG", pClsProperty.TFLAG, DbType.Boolean, ParameterDirection.Input);  //Add : Pinali : 15-09-2019

                Ope.AddParams("LAB_ID", pClsProperty.LAB_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("UPCOLOR_ID", pClsProperty.UPCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("UPCOLORDISCOUNT", pClsProperty.UPCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORAMOUNTDISCOUNT", pClsProperty.UPCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORRAPAPORT", pClsProperty.UPCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORPRICEPERCARAT", pClsProperty.UPCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORAMOUNT", pClsProperty.UPCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("DOWNCOLOR_ID", pClsProperty.DOWNCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORDISCOUNT", pClsProperty.DOWNCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORAMOUNTDISCOUNT", pClsProperty.DOWNCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORRAPAPORT", pClsProperty.DOWNCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORPRICEPERCARAT", pClsProperty.DOWNCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORAMOUNT", pClsProperty.DOWNCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("UPCLARITY_ID", pClsProperty.UPCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYDISCOUNT", pClsProperty.UPCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYAMOUNTDISCOUNT", pClsProperty.UPCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYRAPAPORT", pClsProperty.UPCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYPRICEPERCARAT", pClsProperty.UPCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYAMOUNT", pClsProperty.UPCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("DOWNCLARITY_ID", pClsProperty.DOWNCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYDISCOUNT", pClsProperty.DOWNCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYAMOUNTDISCOUNT", pClsProperty.DOWNCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYRAPAPORT", pClsProperty.DOWNCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYPRICEPERCARAT", pClsProperty.DOWNCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYAMOUNT", pClsProperty.DOWNCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("COPYFROMEMPLOYEE_ID", pClsProperty.COPYFROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("COPYFROMPRD_ID", pClsProperty.COPYFROMPRD_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COPYFROM_ID", pClsProperty.COPYFROM_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COPYTOEMPLOYEE_ID", pClsProperty.COPYTOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("COPYTOPRD_ID", pClsProperty.COPYTOPRD_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COPYTO_ID", pClsProperty.COPYTO_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISDIFF", pClsProperty.ISDIFF, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LABPROCESS", pClsProperty.LABPROCESS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABSELECTION", pClsProperty.LABSELECTION, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DIAMIN", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DIAMAX", pClsProperty.DIAMAX, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("HEIGHT", pClsProperty.HEIGHT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ISMIXRATE", pClsProperty.ISMIXRATE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REPORTNO", pClsProperty.REPORTNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DIAMETER", pClsProperty.DIAMETER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISSAVEWITHPASSWORD", pClsProperty.ISSAVEWITHPASSWORD, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISPCNGRDBYLABENTRY", pClsProperty.ISPCNGRDBYLABENTRY, DbType.Boolean, ParameterDirection.Input); //#P
                Ope.AddParams("PCNGRDBYLAB_ID", pClsProperty.PCNGRDBYLAB_ID, DbType.Int64, ParameterDirection.Input); //#P

                Ope.AddParams("ISNOBGM", pClsProperty.ISNOBGM, DbType.Boolean, ParameterDirection.Input); //#P
                Ope.AddParams("ISNOBLACK", pClsProperty.ISNOBLACK, DbType.Boolean, ParameterDirection.Input); //#P

                //#P : 27-05-2020
                Ope.AddParams("MKAVRAPAPORT", pClsProperty.MKAVRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKAVDISCOUNT", pClsProperty.MKAVDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKAVPRICEPERCARAT", pClsProperty.MKAVPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKAVAMOUNT", pClsProperty.MKAVAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXPRAPAPORT", pClsProperty.EXPRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPDISCOUNT", pClsProperty.EXPDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPRICEPERCARAT", pClsProperty.EXPPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPAMOUNT", pClsProperty.EXPAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RAPNETRAPAPORT", pClsProperty.RAPNETRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPNETDISCOUNT", pClsProperty.RAPNETDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPNETPRICEPERCARAT", pClsProperty.RAPNETPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPNETAMOUNT", pClsProperty.RAPNETAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPNETLINK", pClsProperty.RAPNETLINK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LABRESULTSTATUS", pClsProperty.LABRESULTSTATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CURRENTLABRESULTSTATUS", pClsProperty.CURRENTLABRESULTSTATUS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RCHKREPDIFFAMOUNT", pClsProperty.RCHKREPDIFFAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RCHKREPDIFFPER", pClsProperty.RCHKREPDIFFPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RCHKREPCOMMENT", pClsProperty.RCHKREPCOMMENT, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYMODE", pClsProperty.ENTRYMODE, DbType.String, ParameterDirection.Input);
                //End : #P : 27-05-2020

                //#P : 04-09-2020
                Ope.AddParams("GRDRESULTSTATUS", pClsProperty.GRDRESULTSTATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CURRENTGRDRESULTSTATUS", pClsProperty.CURRENTGRDRESULTSTATUS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SURATEXPLABCHARGE", pClsProperty.SURATEXPLABCHARGE, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("SURATEXPBEFORERAPAPORT", pClsProperty.SURATEXPBEFORERAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPBEFOREPRICEPERCARAT", pClsProperty.SURATEXPBEFOREPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPBEFOREDISCOUNT", pClsProperty.SURATEXPBEFOREDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPBEFOREAMOUNT", pClsProperty.SURATEXPBEFOREAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("SURATEXPAFTERRAPAPORT", pClsProperty.SURATEXPAFTERRAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPAFTERPRICEPERCARAT", pClsProperty.SURATEXPAFTERPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPAFTERDISCOUNT", pClsProperty.SURATEXPAFTERDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("SURATEXPAFTERAMOUNT", pClsProperty.SURATEXPAFTERAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("MUMBAIEXPLABCHARGE", pClsProperty.MUMBAIEXPLABCHARGE, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("MUMBAIEXPBEFORERAPAPORT", pClsProperty.MUMBAIEXPBEFORERAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPBEFOREDISCOUNT", pClsProperty.MUMBAIEXPBEFOREDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPBEFOREPRICEPERCARAT", pClsProperty.MUMBAIEXPBEFOREPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPBEFOREAMOUNT", pClsProperty.MUMBAIEXPBEFOREAMOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPAFTERRAPAPORT", pClsProperty.MUMBAIEXPAFTERRAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPAFTERDISCOUNT", pClsProperty.MUMBAIEXPAFTERDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPAFTERPRICEPERCARAT", pClsProperty.MUMBAIEXPAFTERPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("MUMBAIEXPAFTERAMOUNT", pClsProperty.MUMBAIEXPAFTERAMOUNT, DbType.Decimal, ParameterDirection.Input);


                Ope.AddParams("HELIUMTABLEPC", pClsProperty.HELIUMTABLEPC, DbType.String, ParameterDirection.Input);
                Ope.AddParams("HELIUMRATIO", pClsProperty.HELIUMRATIO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("HELIUMTOTALDEPTH", pClsProperty.HELIUMTOTALDEPTH, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISCONFIRMGRADER", pClsProperty.ISCONFIRMGRADER, DbType.Boolean, ParameterDirection.Input);
                //#P : End : 04-09-2020

                Ope.AddParams("ISCOPYROUGHMKBLPLANINTOFINALTFLAGPRD", pClsProperty.ISCOPYROUGHMKBLPLANINTOFINALTFLAGPRD, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("SAWINGTYPE", pClsProperty.SAWINGTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISEXCELIMPORT", pClsProperty.ISEXCELIMPORT, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ROUGHWEIGHT", pClsProperty.ROUGHWEIGHT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KACHUVAJAN", pClsProperty.KACHUVAJAN, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("WIDTH", pClsProperty.WIDTH, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LENGTH", pClsProperty.LENGTH, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RATIO", pClsProperty.RATIO, DbType.Int32, ParameterDirection.Input);

                // Dhara : 04-06-2022
                Ope.AddParams("OPE", pClsProperty.Ope, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINPACKET_ID", pClsProperty.MAINPACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PCS", pClsProperty.PCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOTCARAT", pClsProperty.LOTCARAT, DbType.Double, ParameterDirection.Input);
                //Ope.AddParams("TODEPARTMENT_ID", pClsProperty.SAWINGTYPE, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.SAWINGTYPE, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("TOMANAGER_ID", pClsProperty.SAWINGTYPE, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("PACKETGROUP_ID", pClsProperty.SAWINGTYPE, DbType.String, ParameterDirection.Input);
                // Dhara : 04-06-2022
                Ope.AddParams("TABLEOPEN_ID", pClsProperty.TO_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CROWNOPEN_ID", pClsProperty.CO_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PAVILLIONOPEN_ID", pClsProperty.PO_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("PKTBALANCECARAT", pClsProperty.BALANCECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PKTTRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("KAPANMAINMANAGER_ID", pClsProperty.KAPANMAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);//GUNJAN:27/03/203


                Ope.AddParams("ISKACHHUVAJAN", pClsProperty.IsKachhuVajan, DbType.Boolean, ParameterDirection.Input); // dHARA : 14-09-2023

                Ope.AddParams("REJECTION_ID", pClsProperty.Rejection_ID, DbType.Int32, ParameterDirection.Input); // dHARA : 16-02-2024

                Ope.AddParams("REFKAPAN_ID", pClsProperty.REFKAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REFKAPANNAME", pClsProperty.REFKAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REFPACKETNO", pClsProperty.REFPACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("REFTAG", pClsProperty.REFTAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdSave_Dhara", CommandType.StoredProcedure);
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


        public Trn_RapSaveProperty SaveDPrint(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ID", pClsProperty.ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SLIPNO", pClsProperty.SLIPNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE", pClsProperty.PRDTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PLANNO", pClsProperty.PLANNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAGSRNO", pClsProperty.TAGSRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CUT_ID", pClsProperty.CUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("POL_ID", pClsProperty.POL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYM_ID", pClsProperty.SYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BLACKINC_ID", pClsProperty.BLACKINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("OPENINC_ID", pClsProperty.OPENINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("WHITEINC_ID", pClsProperty.WHITEINC_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LUSTER_ID", pClsProperty.LUSTER_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("HA_ID", pClsProperty.HA_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PAV_ID", pClsProperty.PAV_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EYECLEAN_ID", pClsProperty.EYECLEAN_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NATURAL_ID", pClsProperty.NATURAL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GRAIN_ID", pClsProperty.GRAIN_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GCARAT", pClsProperty.GCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GCOLOR_ID", pClsProperty.GCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GCLARITY_ID", pClsProperty.GCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GCUT_ID", pClsProperty.GCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GPOL_ID", pClsProperty.GPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("GSYM_ID", pClsProperty.GSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DISCOUNT", pClsProperty.DISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNTDISCOUNT", pClsProperty.AMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RAPAPORT", pClsProperty.RAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PRICEPERCARAT", pClsProperty.PRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("GDISCOUNT", pClsProperty.GDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GAMOUNTDISCOUNT", pClsProperty.GAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GRAPAPORT", pClsProperty.GRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GPRICEPERCARAT", pClsProperty.GPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GAMOUNT", pClsProperty.GAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISFINAL", pClsProperty.ISFINAL, DbType.Boolean, ParameterDirection.Input);


                Ope.AddParams("LAB_ID", pClsProperty.LAB_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("UPCOLOR_ID", pClsProperty.UPCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("UPCOLORDISCOUNT", pClsProperty.UPCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORAMOUNTDISCOUNT", pClsProperty.UPCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORRAPAPORT", pClsProperty.UPCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORPRICEPERCARAT", pClsProperty.UPCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCOLORAMOUNT", pClsProperty.UPCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("DOWNCOLOR_ID", pClsProperty.DOWNCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORDISCOUNT", pClsProperty.DOWNCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORAMOUNTDISCOUNT", pClsProperty.DOWNCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORRAPAPORT", pClsProperty.DOWNCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORPRICEPERCARAT", pClsProperty.DOWNCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCOLORAMOUNT", pClsProperty.DOWNCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("UPCLARITY_ID", pClsProperty.UPCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYDISCOUNT", pClsProperty.UPCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYAMOUNTDISCOUNT", pClsProperty.UPCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYRAPAPORT", pClsProperty.UPCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYPRICEPERCARAT", pClsProperty.UPCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("UPCLARITYAMOUNT", pClsProperty.UPCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("DOWNCLARITY_ID", pClsProperty.DOWNCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYDISCOUNT", pClsProperty.DOWNCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYAMOUNTDISCOUNT", pClsProperty.DOWNCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYRAPAPORT", pClsProperty.DOWNCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYPRICEPERCARAT", pClsProperty.DOWNCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DOWNCLARITYAMOUNT", pClsProperty.DOWNCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISDIFF", pClsProperty.ISDIFF, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LABPROCESS", pClsProperty.LABPROCESS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABSELECTION", pClsProperty.LABSELECTION, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DIAMIN", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DIAMAX", pClsProperty.DIAMAX, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("HEIGHT", pClsProperty.HEIGHT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ISMIXRATE", pClsProperty.ISMIXRATE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdDPrintSave", CommandType.StoredProcedure);
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

        //public string SaveLoop(ArrayList pAL)
        //{ 
        //    string StrRes = "";
        //    try
        //    {
        //        Ope.CreateConnection(Config.ConnectionString);
        //        foreach (Trn_RapSaveProperty pClsProperty in pAL)
        //        {

        //            Ope.ClearParams();

        //            Ope.AddParams("ID", pClsProperty.ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
        //            Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("PRDTYPE", pClsProperty.PRDTYPE, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("PLANNO", pClsProperty.PLANNO, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("TAGSRNO", pClsProperty.TAGSRNO, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
        //            Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
        //            Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("COLOR_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("COLORSHADE_ID", pClsProperty.COLORSHADE_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("CUT_ID", pClsProperty.CUT_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("POL_ID", pClsProperty.POL_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("SYM_ID", pClsProperty.SYM_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("FL_ID", pClsProperty.FL_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("MILKY_ID", pClsProperty.MILKY_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("LBLC_ID", pClsProperty.LBLC_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("NATTS_ID", pClsProperty.NATTS_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("TENSION_ID", pClsProperty.TENSION_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("BLACKINC_ID", pClsProperty.BLACKINC_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("OPENINC_ID", pClsProperty.OPENINC_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("WHITEINC_ID", pClsProperty.WHITEINC_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("LUSTER_ID", pClsProperty.LUSTER_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("HA_ID", pClsProperty.HA_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("PAV_ID", pClsProperty.PAV_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("EYECLEAN_ID", pClsProperty.EYECLEAN_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("NATURAL_ID", pClsProperty.NATURAL_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("GRAIN_ID", pClsProperty.GRAIN_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GCARAT", pClsProperty.GCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GCOLOR_ID", pClsProperty.GCOLOR_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("GCLARITY_ID", pClsProperty.GCLARITY_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("GCUT_ID", pClsProperty.GCUT_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("GPOL_ID", pClsProperty.GPOL_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("GSYM_ID", pClsProperty.GSYM_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("DISCOUNT", pClsProperty.DISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("AMOUNTDISCOUNT", pClsProperty.AMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("RAPAPORT", pClsProperty.RAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("PRICEPERCARAT", pClsProperty.PRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("GDISCOUNT", pClsProperty.GDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GAMOUNTDISCOUNT", pClsProperty.GAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GRAPAPORT", pClsProperty.GRAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GPRICEPERCARAT", pClsProperty.GPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("GAMOUNT", pClsProperty.GAMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
        //            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("ENTRYDATE", pClsProperty.ENTRYDATE, DbType.Date, ParameterDirection.Input);
        //            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("ISFINAL", pClsProperty.ISFINAL, DbType.Boolean, ParameterDirection.Input);


        //            Ope.AddParams("LAB_ID", pClsProperty.LAB_ID, DbType.Int32, ParameterDirection.Input);

        //            Ope.AddParams("UPCOLOR_ID", pClsProperty.UPCOLOR_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("UPCOLORDISCOUNT", pClsProperty.UPCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCOLORAMOUNTDISCOUNT", pClsProperty.UPCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCOLORRAPAPORT", pClsProperty.UPCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCOLORPRICEPERCARAT", pClsProperty.UPCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCOLORAMOUNT", pClsProperty.UPCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("DOWNCOLOR_ID", pClsProperty.DOWNCOLOR_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCOLORDISCOUNT", pClsProperty.DOWNCOLORDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCOLORAMOUNTDISCOUNT", pClsProperty.DOWNCOLORAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCOLORRAPAPORT", pClsProperty.DOWNCOLORRAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCOLORPRICEPERCARAT", pClsProperty.DOWNCOLORPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCOLORAMOUNT", pClsProperty.DOWNCOLORAMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("UPCLARITY_ID", pClsProperty.UPCLARITY_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("UPCLARITYDISCOUNT", pClsProperty.UPCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCLARITYAMOUNTDISCOUNT", pClsProperty.UPCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCLARITYRAPAPORT", pClsProperty.UPCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCLARITYPRICEPERCARAT", pClsProperty.UPCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("UPCLARITYAMOUNT", pClsProperty.UPCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("DOWNCLARITY_ID", pClsProperty.DOWNCLARITY_ID, DbType.Int32, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCLARITYDISCOUNT", pClsProperty.DOWNCLARITYDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCLARITYAMOUNTDISCOUNT", pClsProperty.DOWNCLARITYAMOUNTDISCOUNT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCLARITYRAPAPORT", pClsProperty.DOWNCLARITYRAPAPORT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCLARITYPRICEPERCARAT", pClsProperty.DOWNCLARITYPRICEPERCARAT, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DOWNCLARITYAMOUNT", pClsProperty.DOWNCLARITYAMOUNT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("COPYFROMEMPLOYEE_ID", pClsProperty.COPYFROMEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
        //            Ope.AddParams("COPYFROMPRD_ID", pClsProperty.COPYFROMPRD_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("COPYFROM_ID", pClsProperty.COPYFROM_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("COPYTOEMPLOYEE_ID", pClsProperty.COPYTOEMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
        //            Ope.AddParams("COPYTOPRD_ID", pClsProperty.COPYTOPRD_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("COPYTO_ID", pClsProperty.COPYTO_ID, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("ISDIFF", pClsProperty.ISDIFF, DbType.Boolean, ParameterDirection.Input);
        //            Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

        //            Ope.AddParams("LABPROCESS", pClsProperty.LABPROCESS, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("LABSELECTION", pClsProperty.LABSELECTION, DbType.String, ParameterDirection.Input);
        //            Ope.AddParams("DIAMIN", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("DIAMAX", pClsProperty.DIAMAX, DbType.Double, ParameterDirection.Input);
        //            Ope.AddParams("HEIGHT", pClsProperty.HEIGHT, DbType.Double, ParameterDirection.Input);

        //            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
        //            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
        //            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

        //            try
        //            {
        //                ArrayList AL = Ope.ExeNonQueryWithOutParameterLoop(Ope.GConn, "Trn_SinglePrdSave", CommandType.StoredProcedure);
        //                if (AL.Count != 0)
        //                {
        //                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
        //                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
        //                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
        //                    StrRes = "SUCCESS";
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                StrRes = "FAIL";
        //            }

        //        }
        //        Ope.CloseConnction(Ope.GConn);

        //    }
        //    catch (System.Exception ex)
        //    {
        //        StrRes = "FAIL";
        //    }
        //    return StrRes;
        //}




        public Trn_RapSaveProperty SaveMaxAmountFlag(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRD_ID", pClsProperty.PRD_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdSaveMaxAmountFlag", CommandType.StoredProcedure);
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

        #endregion

        #region FindScal Region

        public Trn_RapSaveProperty FindRapWithUpDown(Trn_RapSaveProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("L_Code", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("Carat", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("Shape_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("Shape_Code", pClsProperty.SHAPECODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Color_ID", pClsProperty.COLOR_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("Color_Code", pClsProperty.COLORCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Clarity_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("Clarity_Code", pClsProperty.CLARITYCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Lab", pClsProperty.LABCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Cut_Code", pClsProperty.CUTCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Polish_Code", pClsProperty.POLCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Symmetry_Code", pClsProperty.SYMCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Flursance_Code", pClsProperty.FLCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LbLc_Code", pClsProperty.LBLCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Nts_Code", pClsProperty.NATTSCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Milky_Code", pClsProperty.MILKYCODE, DbType.String, ParameterDirection.Input);

            //Ope.AddParams("Diameter", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("Diameter", pClsProperty.DIAMIN, DbType.Double, ParameterDirection.Input); //23-08-2020

            Ope.AddParams("ColorShade_Code", pClsProperty.COLORSHADECODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OpenInclusion_Code", pClsProperty.OPENINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BlackInclusion_Code", pClsProperty.BLACKINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("WhiteInclusion_Code", pClsProperty.WHITEINCCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Pavallion_Code", pClsProperty.PAVCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("CanadaMark_Code", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EyeClean_Code", pClsProperty.EYECLEANCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Luster_Code", pClsProperty.LUSTERCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Natural_Code", pClsProperty.NATURALCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Grain_Code", pClsProperty.GRAINCODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FutureDis", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("PlanningType", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("ManualDiscount", 0, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("RapDate", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("IsGetBack", 1, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Rate", "", DbType.String, ParameterDirection.Output);

            Ope.AddParams("GCarat", pClsProperty.GCARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("GCut_Code", pClsProperty.GCUTCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("GPolish_Code", pClsProperty.GPOLCODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("GSymmetry_Code", pClsProperty.GSYMCODE, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdFindRap", CommandType.StoredProcedure);

            pClsProperty.DISCOUNT = 0;
            pClsProperty.AMOUNTDISCOUNT = 0;
            pClsProperty.PRICEPERCARAT = 0;
            pClsProperty.RAPAPORT = 0;
            pClsProperty.AMOUNT = 0;
            pClsProperty.ISMIXRATE = false;
            pClsProperty.GIANONGIA = "GIA";

            pClsProperty.UPCOLORDISCOUNT = 0;
            pClsProperty.UPCOLORAMOUNTDISCOUNT = 0;
            pClsProperty.UPCOLORPRICEPERCARAT = 0;
            pClsProperty.UPCOLORRAPAPORT = 0;
            pClsProperty.UPCOLORAMOUNT = 0;

            pClsProperty.UPCOLOR_ID = 0;
            pClsProperty.UPCOLORCODE = "";
            pClsProperty.UPCOLORNAME = "";

            pClsProperty.DOWNCOLORDISCOUNT = 0;
            pClsProperty.DOWNCOLORAMOUNTDISCOUNT = 0;
            pClsProperty.DOWNCOLORPRICEPERCARAT = 0;
            pClsProperty.DOWNCOLORRAPAPORT = 0;
            pClsProperty.DOWNCOLORAMOUNT = 0;

            pClsProperty.DOWNCOLOR_ID = 0;
            pClsProperty.DOWNCOLORCODE = "";
            pClsProperty.DOWNCOLORNAME = "";

            pClsProperty.UPCLARITYDISCOUNT = 0;
            pClsProperty.UPCLARITYAMOUNTDISCOUNT = 0;
            pClsProperty.UPCLARITYPRICEPERCARAT = 0;
            pClsProperty.UPCLARITYRAPAPORT = 0;
            pClsProperty.UPCLARITYAMOUNT = 0;

            pClsProperty.UPCLARITY_ID = 0;
            pClsProperty.UPCLARITYCODE = "";
            pClsProperty.UPCLARITYNAME = "";

            pClsProperty.DOWNCLARITYDISCOUNT = 0;
            pClsProperty.DOWNCLARITYAMOUNTDISCOUNT = 0;
            pClsProperty.DOWNCLARITYPRICEPERCARAT = 0;
            pClsProperty.DOWNCLARITYRAPAPORT = 0;
            pClsProperty.DOWNCLARITYAMOUNT = 0;

            pClsProperty.DOWNCLARITY_ID = 0;
            pClsProperty.DOWNCLARITYCODE = "";
            pClsProperty.DOWNCLARITYNAME = "";

            pClsProperty.GDISCOUNT = 0;
            pClsProperty.GAMOUNTDISCOUNT = 0;
            pClsProperty.GPRICEPERCARAT = 0;
            pClsProperty.GRAPAPORT = 0;
            pClsProperty.GAMOUNT = 0;

            pClsProperty.DRowDisRegular = null;
            pClsProperty.DRowDisGraph = null;
            pClsProperty.DRowDisUpColor = null;
            pClsProperty.DRowDisUpClarity = null;
            pClsProperty.DRowDisDownColor = null;
            pClsProperty.DRowDisDownClarity = null;

            foreach (DataRow DRow in DTab.Rows)
            {
                if (Val.ToString(DRow["Ope"]) == "Regular")
                {

                    pClsProperty.DISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.AMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.PRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.RAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.AMOUNT = Val.Val(DRow["Amount"]);

                    pClsProperty.COLOR_ID = Val.ToInt(DRow["Color_ID"]);
                    pClsProperty.COLORCODE = Val.ToString(DRow["ColorCode"]);
                    pClsProperty.COLORNAME = Val.ToString(DRow["ColorName"]);

                    pClsProperty.CLARITY_ID = Val.ToInt(DRow["Clarity_ID"]);
                    pClsProperty.CLARITYCODE = Val.ToString(DRow["ClarityCode"]);
                    pClsProperty.CLARITYNAME = Val.ToString(DRow["ClarityName"]);

                    pClsProperty.ISMIXRATE = Val.ToBoolean(DRow["ISMIXRATE"]);
                    if (pClsProperty.ISMIXRATE == true)
                    {
                        pClsProperty.GIANONGIA = "MIX";
                    }
                    else if (pClsProperty.ISMIXRATE == false)
                    {
                        pClsProperty.GIANONGIA = "GIA";
                    }

                    pClsProperty.DRowDisRegular = DRow;

                    DRow.Table.TableName = "ROW";

                    string originalXmlString = string.Empty;
                    using (StringWriter sw = new StringWriter())
                    {
                        DRow.Table.WriteXml(sw);
                        originalXmlString = sw.ToString();
                    }

                    pClsProperty.DRowDisRegularXML = originalXmlString;
                }
                else if (Val.ToString(DRow["Ope"]) == "UpColor")
                {
                    pClsProperty.DRowDisUpColor = DRow;

                    pClsProperty.UPCOLORDISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.UPCOLORAMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.UPCOLORPRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.UPCOLORRAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.UPCOLORAMOUNT = Val.Val(DRow["Amount"]);

                    pClsProperty.UPCOLOR_ID = Val.ToInt(DRow["Color_ID"]);
                    pClsProperty.UPCOLORCODE = Val.ToString(DRow["ColorCode"]);
                    pClsProperty.UPCOLORNAME = Val.ToString(DRow["ColorName"]);

                }
                else if (Val.ToString(DRow["Ope"]) == "DownColor")
                {
                    pClsProperty.DRowDisDownColor = DRow;

                    pClsProperty.DOWNCOLORDISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.DOWNCOLORAMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.DOWNCOLORPRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.DOWNCOLORRAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.DOWNCOLORAMOUNT = Val.Val(DRow["Amount"]);

                    pClsProperty.DOWNCOLOR_ID = Val.ToInt(DRow["Color_ID"]);
                    pClsProperty.DOWNCOLORCODE = Val.ToString(DRow["ColorCode"]);
                    pClsProperty.DOWNCOLORNAME = Val.ToString(DRow["ColorName"]);
                }
                else if (Val.ToString(DRow["Ope"]) == "UpClarity")
                {
                    pClsProperty.DRowDisUpClarity = DRow;

                    pClsProperty.UPCLARITYDISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.UPCLARITYAMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.UPCLARITYPRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.UPCLARITYRAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.UPCLARITYAMOUNT = Val.Val(DRow["Amount"]);

                    pClsProperty.UPCLARITY_ID = Val.ToInt(DRow["Clarity_ID"]);
                    pClsProperty.UPCLARITYCODE = Val.ToString(DRow["ClarityCode"]);
                    pClsProperty.UPCLARITYNAME = Val.ToString(DRow["ClarityName"]);

                }
                else if (Val.ToString(DRow["Ope"]) == "DownClarity")
                {
                    pClsProperty.DRowDisDownClarity = DRow;

                    pClsProperty.DOWNCLARITYDISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.DOWNCLARITYAMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.DOWNCLARITYPRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.DOWNCLARITYRAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.DOWNCLARITYAMOUNT = Val.Val(DRow["Amount"]);

                    pClsProperty.DOWNCLARITY_ID = Val.ToInt(DRow["Clarity_ID"]);
                    pClsProperty.DOWNCLARITYCODE = Val.ToString(DRow["ClarityCode"]);
                    pClsProperty.DOWNCLARITYNAME = Val.ToString(DRow["ClarityName"]);
                }
                else if (Val.ToString(DRow["Ope"]) == "Graph")
                {
                    pClsProperty.DRowDisGraph = DRow;

                    pClsProperty.GDISCOUNT = Val.Val(DRow["TotalDiscount"]);
                    pClsProperty.GAMOUNTDISCOUNT = Val.Val(DRow["TotalAmountDiscount"]);
                    pClsProperty.GPRICEPERCARAT = Val.Val(DRow["Rate"]);
                    pClsProperty.GRAPAPORT = Val.Val(DRow["OriginalRate"]);
                    pClsProperty.GAMOUNT = Val.Val(DRow["Amount"]);
                }
            }

            return pClsProperty;
        }


        public int AllowForUpdate(string pStrID, int Value)
        {
            Ope.ClearParams();
            string Str = "Update Trn_SinglePrd With(ROWLOCK) Set AllowForUpdate = " + Value + " Where 1=1 And ID = '" + pStrID + "'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }



        public string DeleteRecord(string pStrID)
        {
            string ReturnValue = string.Empty;
            string ReturnMessageType = string.Empty;
            string ReturnMessageDesc = string.Empty;

            try
            {

                Ope.ClearParams();

                Ope.AddParams("ID", pStrID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdDelete", CommandType.StoredProcedure);
                if (AL.Count != 0)
                {
                    ReturnValue = Val.ToString(AL[0]);
                    ReturnMessageType = Val.ToString(AL[1]);
                    ReturnMessageDesc = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                ReturnValue = "";
                ReturnMessageType = "FAIL";
                ReturnMessageDesc = ex.Message;

            }
            return ReturnMessageType;
        }

        public int DeleteMakable(string pStrPacketID)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Update TRN_SinglePacketMaster With(ROWLOCK) Set MakCarat = 0, MakDate = NULL Where 1=1 And Packet_ID = '" + pStrPacketID + "' ";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }


        public int DeleteGrading(string pStrPacketID)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Update TRN_SinglePacketMaster With(ROWLOCK) Set PolCarat = 0, PolDate = NULL Where 1=1 And Packet_ID = '" + pStrPacketID + "' ";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }
        public DataTable ValCheckFurtherPrdExistsOrNotForDelete(Int64 pStrID, Int64 pStrPrd_ID, int pIntPlanNo, int pIntPrdType_ID) //Add : Pinali : 15-09-2019
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("ID", pStrID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRD_ID", pStrPrd_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PLANNO", pIntPlanNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PRDTYPE_ID", pIntPrdType_ID, DbType.Int32, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "ValCheckFurtherPrdExistsOrNotForDelete", CommandType.StoredProcedure);
            return DTab;
        }
        #endregion

        public DataTable GetSinglePrdLabPricingData(int pPrdTypeID = 0, string PacketID = null, Int64 EmployeeID = 0, Int64 ManagerID = 0) //#P : 27-05-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdLabPricingGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetSinglePrdGrdPricingData(int pPrdTypeID = 0, string PacketID = null, Int64 EmployeeID = 0, Int64 ManagerID = 0) //#P : 04-09-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGrdPricingGetData", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetSinglePacketHeliumDetail(string pStrKapanName, int pIntPacketNo, string pStrTag) //#P : 04-09-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketLatestHeliumDetail", CommandType.StoredProcedure);

            return DTab;
        }

        public DataTable GetSinglePrdSerialNoGenerateGetData(string pStrOpe, string pStrFromDate, string pStrToDate, string pStrStoneNo, string pStrXmlForSerialNo) //#P : 27-05-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STONENO", pStrStoneNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("XMLSERIALNOPKTDETAIL", pStrXmlForSerialNo, DbType.Xml, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePrdSerialNoGenerateGetData", CommandType.StoredProcedure);

            return DTab;
        }
        public Trn_RapSaveProperty CheckLabPricingValdiation(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_CheckLabPricingValidation", CommandType.StoredProcedure);
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
        public DataTable GetDataForMumbaiBarcodePrint(string pStrOpe, string pStrFromDate, string pStrToDate, string pStrStoneNo) //#D : 15-06-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STONENO", pStrStoneNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePrdMumbaiBarcodePrint", CommandType.StoredProcedure);
            return DTab;
        }
        public Trn_RapSaveProperty ValSaveRapCalcValidationForper(Trn_RapSaveProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BALANCECARAT", pClsProperty.BALANCECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDCARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_RapCalcValidationForper", CommandType.StoredProcedure);
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

        public string GetValForRapdate(string StrKapanName, int StrPacketNo, string StrRapdate) //#D : 15-10-2020 
        {
            Ope.ClearParams();
            string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrd", "RapDate", " AND KAPANNAME = '" + StrKapanName + "' AND PACKETNO = '" + StrPacketNo + "' AND RAPDATE <> '" + StrRapdate + "' AND PRDTYPE_ID = 2");
            return Str;
        }

        public double ValidationForAmount(string StrKapanName, Int32 StrPacketNo) //#D : 15-10-2020
        {
            string Str = "";

            Double IntRes = 0;

            Ope.ClearParams();
            Str = "Select SUM(Amount) As Amount From Trn_SinglePrd With(NOLOCK) WHERE KAPANNAME = '" + StrKapanName + "' AND PACKETNO = '" + StrPacketNo + "' AND PRDTYPE_ID = 2 AND IsFinal = 1";
            IntRes = Val.Val(Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text));
            return IntRes;

        }
        public Trn_RapSaveProperty UpdateByGrdExpDetailWithExcel(Trn_RapSaveProperty pClsProperty, string StrXml, string pStrOpe) // ADD : D: 24-10-2020
        {
            try
            {

                Ope.ClearParams();

                Ope.AddParams("XML", StrXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdUpdatWithExcel", CommandType.StoredProcedure);
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
        public DataTable SaveMakLOg(string pStrMakLogXml) //#D : 27-11-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("MAKLOGXML", pStrMakLogXml, DbType.Xml, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SinglePrdMakLogInsert", CommandType.StoredProcedure);
            return DTab;
        }
        #region Process Lock

        public DataTable PredictionLockGetData(Trn_SinglePrdProcessLockProperty pClsProperty)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdProcessLockGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public Trn_SinglePrdProcessLockProperty PredictionLockSave(Trn_SinglePrdProcessLockProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("LOCKPRDTYPE_ID", pClsProperty.LOCKPRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ISLOCK", pClsProperty.ISLOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISUNLOCK", pClsProperty.ISUNLOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdProcessLockSave", CommandType.StoredProcedure);
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

        public Trn_RapSaveProperty SaveBrkDetailFromPrdData(Trn_RapSaveProperty pClsProperty) //#P : 08-12-2020 
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);


                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);
                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdSaveBrkDetailFromPrdData", CommandType.StoredProcedure);
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

        #endregion
        public DataTable GetdataForLabInclusion(string pXMLStrLabIncl) //#D : 27-11-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("LABINCLUSIONXML", pXMLStrLabIncl, DbType.Xml, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_LabInclusionFileGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public Trn_RapSaveProperty UpdateByLabIncclusionExpDetailWithExcel(Trn_RapSaveProperty pClsProperty, string StrLabIncXml) // ADD : D: 05-01-2021
        {
            try
            {

                Ope.ClearParams();

                Ope.AddParams("XMLFORLABINCLUSION", StrLabIncXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE", pClsProperty.PRDTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("HOSTNAME", Config.ComputerName, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_LabInclusionUpdatWithExcel", CommandType.StoredProcedure);
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
        public ParameterDiscountProperty ParameterBackUpdate(ParameterDiscountProperty pClsProperty, string pStrParameterDetailXml, double pDouDiaMin) //#P : 01-03-2021 : Update Back From RapCalc
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLFORPARAMETERDETAIL", pStrParameterDetailXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("OPE", pClsProperty.OPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("NEWVALUE", pClsProperty.NEWVALUE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("DIAMIN", pDouDiaMin, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_ParameterDiscountUpdateFromRapCalc", CommandType.StoredProcedure);
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
        public Trn_RapSaveProperty ValSaveRapCalcForRecutProcess(Trn_RapSaveProperty pClsProperty) //#P : 06-03-2021 : Jo Recut ni Process thi Koi Packet Overdue 6e To RapCalc Open j na thavu joiye
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdValSaveRapCalcForRecutProcess", CommandType.StoredProcedure);
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

        public DataTable GetDataForExcelExport(int pPrdTypeID = 0, string PacketID = null, Int64 EmployeeID = 0, Int64 ManagerID = 0)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "EXCELEXPORT", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRDTYPE_ID", pPrdTypeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", PacketID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", EmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", ManagerID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetDataForExcelExport", CommandType.StoredProcedure);

            return DTab;
        }


        //Add Milan (22-09-2021)
        public DataTable ImportExcelForRapCalc(string pStrExcelXml)
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("EXCELXML", pStrExcelXml, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_ImportExcelForRapCalc", CommandType.StoredProcedure);
            return DTab;
        }
        //END

        public DataTable GetBarcodeWiseEmployeeCode(string pStrBarcode = "", string pStrKapanName = "", int pStrPktSrNo = 0)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("OPE", "BARCODEORPKTSERIALNO", DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", 0, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrPktSrNo, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePrdGetOSKapanPacketTag", CommandType.StoredProcedure);
            return DTab;
        }

        public Trn_RapSaveProperty ValidationForFinalPalnAmt(Trn_RapSaveProperty pClsProperty) // Dhara : 18-4-2023
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MTAG", pClsProperty.MTAG, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PLANAMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_SinglePrdVaidationForFinalAmount", CommandType.StoredProcedure);
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
