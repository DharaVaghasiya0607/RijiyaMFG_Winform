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
    public class BOTRN_Rejection
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public TRN_RejectionProperty Save(TRN_RejectionProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("REJECTIONTRN_ID", pClsProperty.REJECTIONTRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REJECTION_ID", pClsProperty.REJECTION_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTIONDATE", pClsProperty.REJECTIONDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("REJECTIONFROM", pClsProperty.REJECTIONFROM, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTYNAME", pClsProperty.PARTYNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PCS", pClsProperty.PCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RejectionSave", CommandType.StoredProcedure);

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


        public TRN_RejectionProperty KapanSave(TRN_RejectionProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("REJECTIONTRN_ID", pClsProperty.REJECTIONTRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REJECTION_ID", pClsProperty.REJECTION_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTIONDATE", pClsProperty.REJECTIONDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("REJECTIONFROM", pClsProperty.REJECTIONFROM, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTYNAME", pClsProperty.PARTYNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PCS", pClsProperty.PCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanRejectionSave", CommandType.StoredProcedure);

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

        public DataRow GetOrgAndBalCaratOfLot(Int64 pIntLotID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = @"SELECT a.NEWBALANCECARAT as BALANCECARAT,b.* FROM VIW_LOTBALANCE A  WITH(NOLOCK) 
                    INNER JOIN TRN_ImportDetail b WITH(NOLOCK)  ON A.Lot_ID = B.Lot_ID WHERE a.LOT_ID = '" + pIntLotID + "'";

            //StrQuery = "SELECT CARAT,BALANCECARAT FROM TRN_IMPORTDETAIL WITH(NOLOCK) WHERE LOT_ID = '" + pIntLotID + "'";

            DataRow Dr= Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
            
            return Dr;
        }

        public DataRow GetOrgAndBalCaratOfKapan(Int64 pIntRoughID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT KAPANCARAT,BALANCECARAT FROM TRN_KAPAN WITH(NOLOCK) WHERE KAPAN_ID = '" + pIntRoughID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }


        public DataSet GetRejectionView(string pStrKapan, Int64 pIntMarkerID, string pStrFromDate, string pStrToDate, string pStrMainManager)
        {
            DataSet DS = new DataSet();

            Ope.ClearParams();
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MAINMANAGER_ID", pStrMainManager, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MARKER_ID", pIntMarkerID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.String, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Trn_RejectionView", CommandType.StoredProcedure);

            return DS;

        }

        public TRN_RejectionProperty DeleteRejectionID(TRN_RejectionProperty pClsProperty)
        {
            Ope.ClearParams();
            Ope.AddParams("REJECTIONTRN_ID", pClsProperty.REJECTIONTRN_ID, DbType.Int64, ParameterDirection.Input);
            
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);


            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RejectionDelete", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;
            
        }

        public TRN_RejectionProperty UpdateRejectionID(TRN_RejectionProperty pClsProperty)
        {
            Ope.ClearParams();
            Ope.AddParams("REJECTIONTRN_ID", pClsProperty.REJECTIONTRN_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PCS", pClsProperty.PCS, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("REJECTION_ID", pClsProperty.REJECTION_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
            Ope.AddParams("REJECTIONDATE", pClsProperty.REJECTIONDATE, DbType.Date, ParameterDirection.Input);// URVISHA ADD BY :04022023
            
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RejectionUpdate", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;

        }

        public DataTable CheckValidationForPCNRejection(string pStrCheckPCNPacketsListXML) //Add : Pinali : 22-10-2019
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("XMLCHECKPCNPACKETSLIST", pStrCheckPCNPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_CheckValidationForPCNRejection", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetRejectionPacketRMkblRate(string pStrCheckPCNPacketsListXML) //Add : Pinali : 05-10-2020
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("XMLCHECKPCNPACKETSLIST", pStrCheckPCNPacketsListXML, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_GetRejectionPacketRMkblRate", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForMergeExistingPkt(Int32 pIntPacketNo, Int64 pIntKapan_ID) // Dhara : 07-05-2022
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPAN_ID", pIntKapan_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_TransferToMixExistingPacketGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable ValidationForMergePkt(string pStrKapanName, int pIntPktNo, string pStrTag) // Dhara : 07-05-2022
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_RejectionMeregePacketValidation", CommandType.StoredProcedure);
            return DTab;
        }

        public TRN_RejectionProperty CaratTransferToParcel(TRN_RejectionProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();            
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EXTRACARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRANSFERCARAT", pClsProperty.TRANSFERCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("REJECTION_ID", pClsProperty.REJECTION_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANMAINMANAGER_ID", pClsProperty.KAPANMAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);//GUNJAN : 27/03/2023

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_ParcelCaratTransfer", CommandType.StoredProcedure);

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
