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
    public class BOTRN_KapanCreate
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetLotUsedCaratData(string pStrOpe, Int64 pIntLotID, string pStrKapan)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", pStrOpe, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pIntLotID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_RoughPurchaseUsedCaratData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetRejectionData(string pStrRejectionFrom, TrnKapanCreateProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("REJECTIONFROM", pStrRejectionFrom, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Guid, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_RejectionCreateGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public TrnKapanCreateProperty Save(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANGROUP", pClsProperty.KAPANGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANCATEGORY", pClsProperty.KAPANCATEGORY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPARMEMO", pClsProperty.COMPARMEMO, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANDATE", pClsProperty.KAPANDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANPCS", pClsProperty.KAPANPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANCARAT", pClsProperty.KAPANCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("KAPANRATE", pClsProperty.KAPANRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANAMOUNT", pClsProperty.KAPANAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXPMAKPER", pClsProperty.EXPMAKPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPMAKCARAT", pClsProperty.EXPMAKCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPOLPER", pClsProperty.EXPPOLPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPOLCARAT", pClsProperty.EXPPOLCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPDOLLAR", pClsProperty.EXPDOLLAR, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LOTGROUP", pClsProperty.LOTGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DUEDAYS", pClsProperty.DUEDAYS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EXPGIAPER", pClsProperty.EXPGIAPER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISNOTAPPLYANYLOCK", pClsProperty.ISNOTAPPLYANYLOCK, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANTYPE", pClsProperty.KAPANTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("DIAMONDTYPE", pClsProperty.DIAMONDTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanCreateSave", CommandType.StoredProcedure);

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

        //public TrnKapanCreateProperty CheckValSaveKapanWithSublot(TrnKapanCreateProperty pClsProperty, string pStrOpe)  //Add : Pinali : 18-07-2019 :For Check Kapan Created With Proper Sublot Sequence
        //{
        //    try
        //    {
        //        Ope.ClearParams();
        //        Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("SUBLOT", pClsProperty.SUBLOT, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("SUBLOT1", pClsProperty.SUBLOT1, DbType.String, ParameterDirection.Input);
        //        Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
        //        Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

        //        Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
        //        Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

        //        ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "CheckValSaveForKapanSublot", CommandType.StoredProcedure);
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

        public TrnKapanCreateProperty CheckValSaveKapanForRateAndAmount(TrnKapanCreateProperty pClsProperty, string KapanXmlDetail, string pStrEntryMode)  //Add : Pinali : 09-09-2020 : KapanAmount Not Greater than InvoiceAmount
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLKAPANDETAIL", KapanXmlDetail, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYMODE", pStrEntryMode, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "CheckValSaveKapanWithRateAndAmount", CommandType.StoredProcedure);
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


        public DataTable GetDataForPurchaseLiveStock(bool pBoolDispAllLot)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALLLOT", pBoolDispAllLot, DbType.Boolean, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PurchaseLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataRow GetOrgAndBalCarat(Guid pIntLotID)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "SELECT CARAT,BALANCECARAT FROM TRN_IMPORT WITH(NOLOCK) WHERE LOT_ID = '" + pIntLotID + "'";

            DataRow Dr = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);

            return Dr;
        }

        public DataTable GetDataForKapanLiveStock(string pStrReport, string pStrKapanStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("REPORT", pStrReport, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANSTATUS", pStrKapanStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            //Ope.AddParams("TODATE", pClsProperty.TODATE, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }



        public DataTable GetDataForSingleKapanLiveStock(string pStrKapanStatus, Int64 pIntLot_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANSTATUS", pStrKapanStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pIntLot_ID, DbType.UInt64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleKapanLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataTable GetDataForSinglePacketLiveStock(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag,
            string pStrBarcode,
            string pStrRFID,
            bool pIsWithExtraStock, string pStrJangedNo, bool pIsGrpJangedNo, int pStrTopktNo, string pStrDeptJangedNo, string XmlForDeptJanged)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISGRPJANGEDNO", pIsGrpJangedNo, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RFIDTAG", pStrRFID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", pStrTopktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DEPTJANGEDNO", pStrDeptJangedNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("XMLFORDEPTJANGED", XmlForDeptJanged, DbType.Xml, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataRow GetDataForSinglePacketLiveStockPacketInfo(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, int pStrPktSerialNo, string pStrJangedNo, int PintToPkt, int pIntFromProcessID)
        {
            Ope.ClearParams();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMPROCESS_ID", pIntFromProcessID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", PintToPkt, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrPktSerialNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input); //#P : 10-06-2022
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketLiveStockGetData", CommandType.StoredProcedure);
        }


        public DataTable GetDataForSinglePacketLiveStockJangednoWiseInfo(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, int pStrPktSerialNo, string pStrJangedNo, string pStrDeptJangedNo) //#P : 11-06-2022 : Used ForJangedNo
        {
            Ope.ClearParams();

            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrPktSerialNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input); //#P : 10-06-2022
            Ope.AddParams("DEPTJANGEDNO", pStrDeptJangedNo, DbType.String, ParameterDirection.Input); //#P : 10-06-2022
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketLiveStockGetData_Test", CommandType.StoredProcedure);
            return DTab;
        }


        public DataRow GetDataForSinglePacketLiveStockCurrentOutstanding(string pStrKapanName, int pIntPacketNo, string pStrTag, Int64 pIntBarcode)
        {
            Ope.ClearParams();

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pIntBarcode, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetCurrentOutStanding", CommandType.StoredProcedure);
        }

        public DataTable GetPacketDataForBarcodePrint(string KapanName, string SubLot, string SubLot1, Int32 pIntFromPacketNo, Int32 pIntToPacketNo, string pStrTag, string pStrPacketType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPAN_ID", null, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", KapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("FROMPACKETNO", pIntFromPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", pIntToPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETTYPE", pStrPacketType, DbType.String, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetPacketDataForGradingBarcodePrint(Int64 pIntJangedNo) //Add : Pinali : 16-08-2019 Used In Grading Barcode Print
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketGradingBarcodePrintData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataRow GetPacketDataForGradingBarcodePrint(string pStrKapan, Int32 pIntPacketNo, string pStrTag, Int64 pIntJangedNo = 0) //Add : Pinali : 16-08-2019 Used In Grading Barcode Print
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("JANGEDNO", pIntJangedNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGradingBarcodePrintData", CommandType.StoredProcedure);
        }

        public DataRow GetBarcodePrintForLabIssueReturn(string KapanName, int pIntPacketNo, string pStrTag) //Add : Pinali : 12-08-2019
        {
            Ope.ClearParams();

            Ope.AddParams("KAPAN_ID", null, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", KapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketGetData", CommandType.StoredProcedure);
        }

        public DataTable GetPacketLiveStock(string pStrReport, string pStrPacketStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("REPORT", pStrReport, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("PACKETSTATUS", pStrPacketStatus, DbType.String, ParameterDirection.Input);

            //Ope.AddParams("TODATE", pClsProperty.TODATE, DbType.Date, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public TrnKapanCreateProperty EditKapan(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("STATUS", pClsProperty.STATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANPCS", pClsProperty.KAPANPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("KAPANCARAT", pClsProperty.KAPANCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANDATE", pClsProperty.KAPANDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANGROUP", pClsProperty.KAPANGROUP, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("ISHIDE", pClsProperty.ISHIDE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("COMPLETEDATE", pClsProperty.COMPLETEDATE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("CLVCOMPLETEDATE", pClsProperty.CLVCOMPLETEDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MFGISSUEDATE", pClsProperty.MFGISSUEDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("GHATCOMPLETEDATE", pClsProperty.GHATCOMPLETEDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("POLISHRECVDATE", pClsProperty.POLISHRECVDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MUMBAIRECVDATE", pClsProperty.MUMBAIRECVDATE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISNOTAPPLYANYLOCK", pClsProperty.ISNOTAPPLYANYLOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("LABOURAMOUNT", pClsProperty.LABOURAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("KAPANRATE", pClsProperty.KAPANRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANAMOUNT", pClsProperty.KAPANAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("OPE", pClsProperty.Ope, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DIAMONDTYPE", pClsProperty.DIAMONDTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanCreateUpdateDelete", CommandType.StoredProcedure);

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

        public TrnKapanCreateProperty EditKapanpcs(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("STATUS", pClsProperty.STATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANPCS", pClsProperty.KAPANPCS, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanCreateUpdatPcs", CommandType.StoredProcedure);

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

        public TrnKapanCreateProperty EditKapanRate(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("STATUS", pClsProperty.STATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANRATE", pClsProperty.KAPANRATE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("KAPANAMOUNT", pClsProperty.KAPANRATE, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanCreateUpdatRate", CommandType.StoredProcedure);

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


        public TrnKapanCreateProperty EditKapanCarat(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("STATUS", pClsProperty.STATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANCARAT", pClsProperty.KAPANCARAT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanCreateUpdateCarat", CommandType.StoredProcedure);

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

        public TrnPacketCreationProperty DeleteIssRetEntry(TrnPacketCreationProperty pSProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", pSProperty.Ope, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKET_ID", pSProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            //Ope.AddParams("TODATE", pClsProperty.TODATE, DbType.Date, ParameterDirection.Input);


            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_DelIssRetEntry", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pSProperty.ReturnValue = Val.ToString(AL[0]);
                pSProperty.ReturnMessageType = Val.ToString(AL[1]);
                pSProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pSProperty;
        }


        public TrnPacketCreationProperty PacketUpdateFromLiveStock(TrnPacketCreationProperty pSProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("PACKET_ID", pSProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pSProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("PURITY_ID", pSProperty.PURITY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CHARNI_ID", pSProperty.CHARNI_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ISSUEPCS", pSProperty.ISSUEPCS, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("ISSUECARAT", pSProperty.ISSUECARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("RETURNPCS", pSProperty.RETURNPCS, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("RETURNCARAT", pSProperty.RETURNCARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("LOSSCARAT", pSProperty.LOSSCARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("KACHAPCS", pSProperty.KACHAPCS, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("KACHACARAT", pSProperty.KACHACARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("CANCELPCS", pSProperty.CANCELPCS, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CANCELCARAT", pSProperty.CANCELCARAT, DbType.Double, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_PacketUpdateFromLiveStock", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pSProperty.ReturnValue = Val.ToString(AL[0]);
                pSProperty.ReturnMessageType = Val.ToString(AL[1]);
                pSProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pSProperty;
        }

        public DataTable GetDataForSinglePacketFinalJamaInfo(string pStrKapanName, int pIntPacketNo, string pStrTag) //#P : 27-06-2020 : Used In FinalJama
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketFinalJamaGetData", CommandType.StoredProcedure);
            //return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketFinalJamaGetData", CommandType.StoredProcedure);
            return DTab;
        }

        #region Grid Layout

        public int SaveGridLayout(string pStrFormName, string pStrGridName, string pStrGridLayout)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "Delete From MST_GridLayout With(RowLock) Where Employee_ID = " + Config.gEmployeeProperty.LEDGER_ID + " And FormName='" + pStrFormName + "' And GridName='" + pStrGridName + "' ";
            StrQuery += " Insert Into MST_GridLayout With(RowLock) (Employee_ID,FormName,GridName,GridLayout) Values (" + Config.gEmployeeProperty.LEDGER_ID + ",'" + pStrFormName + "','" + pStrGridName + "','" + pStrGridLayout + "') ";

            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
        }

        public int DeleteGridLayout(string pStrFormName, string pStrGridName)
        {
            string StrQuery = "";

            Ope.ClearParams();

            StrQuery = "Delete From MST_GridLayout With(RowLock) Where Employee_ID = " + Config.gEmployeeProperty.LEDGER_ID + " And FormName='" + pStrFormName + "' And GridName='" + pStrGridName + "' ";

            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
        }


        public string GetGridLayout(string pStrFormName, string pStrGridName)
        {
            Ope.ClearParams();

            string StrQuery = " And Employee_ID = '" + Config.gEmployeeProperty.LEDGER_ID + "' AND FormName = '" + pStrFormName + "' And GridName = '" + pStrGridName + "'";

            return Ope.FindText(Config.ConnectionString, Config.ProviderName, "MST_GridLayout", "GridLayout", StrQuery);

        }
        public DataTable GetDataForSinglePacketAutoIssue(string pStrKapanName, int pIntPacketNo, string pStrTag, string pFromDate, string pToDate) //#D : 22-06-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FROMDATE", pFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", pToDate, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketAutoIssueGetData", CommandType.StoredProcedure);
            return DTab;
        }

        #endregion

        public DataRow GetDataForParcelPacketLiveStockBarcodeInfo(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, Int32 pStrBarcode, string pStrTag)
        {
            Ope.ClearParams();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.Int32, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_ParcelPacketLiveStockGetDataBarcode", CommandType.StoredProcedure);
        }
        public DataRow GetDataForParcelPacketLiveStockPacketInfo(string StrMixStockType, string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, bool pIsWithExtraStock, bool pISDisplayAll, bool pIsSubPktDisplayAll)
        {
            Ope.ClearParams();

            Ope.AddParams("MIXSTOCKTYPE", StrMixStockType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALL", pISDisplayAll, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISSUBPACKETDISPLAYALL", pIsSubPktDisplayAll, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODENO", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", "", DbType.String, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_MixPacketLiveStockGetData", CommandType.StoredProcedure);
        }
        public DataTable GetDataForParcelPacketLiveStockJangedWiseInfo(string StrMixStockType, string StrOpe, string pStrDisplay, string pStrJangedNo, bool pIsWithExtraStock, bool pISDisplayAll, bool pIsSubPktDisplayAll)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("MIXSTOCKTYPE", StrMixStockType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALL", pISDisplayAll, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISSUBPACKETDISPLAYALL", pIsSubPktDisplayAll, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", 0, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODENO", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_MixPacketLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForParcelPacketLiveStock(string StrMixStockType, string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, bool pIsWithExtraStock, bool pISDisplayAll, bool pIsSubPktDisplayAll) // Dhara : 19-04-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("MIXSTOCKTYPE", StrMixStockType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);

            Ope.AddParams("WITHEXTRASTOCK", pIsWithExtraStock, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISDISPLAYALL", pISDisplayAll, DbType.Boolean, ParameterDirection.Input);
            Ope.AddParams("ISSUBPACKETDISPLAYALL", pIsSubPktDisplayAll, DbType.Boolean, ParameterDirection.Input);

            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_MixPacketLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetDataForPacketHistory(string pStrKapanName) // Dhara : 22-04-2022
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_PacketHistoryGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataRow GetDataForKapanLiveStockForReturnPacketInfo(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, string pStrJangedNo, Int32 pStrPktStNo, Int64 pStrEmployee_ID, int pIntDEPTJangedNo, string pStrDptJangedDate, string StrSearchType)
        {
            Ope.ClearParams();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrPktStNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SEARCHTYPE", StrSearchType, DbType.String, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_SinglePacketLiveStockGetDataForReturn", CommandType.StoredProcedure);

        }

        public DataRow GetMainServerPath() //Add : Pinali : 12-11-2022 : GalaxyOperator Process Default consider kari lidhu 6e
        {
            Ope.ClearParams();
            string Str = "Select Isnull(UploadServerPath,'') AS UploadServerPath, ISNULL(UploadServerUserName,'') AS UploadServerUserName,ISNULL(UploadServerPassword,'') AS UploadServerPassword From MST_PARA With(Nolock) Where UPPER(PARATYPE) = 'PROCESS' AND PARA_ID = 3846"; //3846: GalaxyOperator
            //return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }

        public TrnSingleIssueReturnProperty QCImportDataDelete(TrnSingleIssueReturnProperty pClsProperty, string pStrReason)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REASON", pStrReason, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_QCImportDataDelete", CommandType.StoredProcedure);

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

        public TrnSingleIssueReturnProperty QCImportDataSave(TrnSingleIssueReturnProperty pClsProperty, string pStrQCDataXml, string pStrOpe)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("XMLQCIMPORTDATA", pStrQCDataXml, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("TRN_ID", pClsProperty.TRN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNTYPE", pClsProperty.RETURNTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTREASON_ID", pClsProperty.REJECTREASON_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("REJECTREMARK", pClsProperty.REJECTREMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTAPPROVALSTATUS", pClsProperty.REJECTAPPROVALSTATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REJECTSTATUSBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("REJECTSTATUSIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnValuePacketID", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                
                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_QCImportDataSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnValuePacketID = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[2]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[3]);

                    if (pClsProperty.RETURNTYPE == "COMPLETE" && pClsProperty.ReturnMessageType == "SUCCESS")
                    {
                        try
                        {
                            
                            Ope.ClearParams();
                            string StrSql = @"
                            Insert Into filedtl
                            (
                            IDate,ITime,[User],FName,Ext,UPath,SPath,PCName
                            )
                            Values
                            (
                            getdate(),getdate(),'" + Config.gEmployeeProperty.LEDGERCODE + "','" + pClsProperty.FName + "','" + pClsProperty.Ext + "','','" + pClsProperty.SPath + "','" + Config.ComputerName + "')";
                            Ope.ExeNonQuery(Config.QCConnectionString, Config.QCProviderName, StrSql, CommandType.Text);
                        }
                        catch (Exception ex)
                        {
                            System.Windows.Forms.MessageBox.Show(ex.Message);
                        }
                    }
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


        public TrnSingleIssueReturnProperty QCImportDataValSave(TrnSingleIssueReturnProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_QCImportDataValSave", CommandType.StoredProcedure);

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
        public DataRow NonQCGetDataRowWise(string StrKapan, int StrPktNo, string StrTag) //Krina
        {
            Ope.ClearParams();

            Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", StrPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", StrTag, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "RP_NonQCLiveStockGetData", CommandType.StoredProcedure);
        }

        public DataTable NonQCGetData(string strKapan, int strPktNo, string strTag) //Krina
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("KAPANNAME", strKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", strPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", strTag, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_NonQCLiveStockGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataRow GetDataForQCLiveStock(string strKapan, int strPktNo, string strTag, string StrFromDate, string StrToDate, string StrStatus)
        {
            Ope.ClearParams();

            Ope.AddParams("KAPANNAME", strKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", strPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", strTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", StrFromDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.String, ParameterDirection.Input);
            Ope.AddParams("STATUS", StrStatus, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "TRN_RejectionGetData", CommandType.StoredProcedure);
        }

        public DataTable GetDataForPath(int Employee_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("EMPLOYEE_ID", Employee_ID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_GetDataForPath", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable DateUpate(Int64 pStrJangedno, string PstrTransdate)
        {
            Ope.ClearParams();
            DataTable DtabDateUpdate = new DataTable();
            Ope.AddParams("JANGEDNO", pStrJangedno, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DATE", Val.SqlDate(PstrTransdate), DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "DateUpdateJanged", CommandType.StoredProcedure);
            return DtabDateUpdate;

        }

        public DataTable GetDataForKapanLiveStockForReturn(string StrOpe, string pStrDisplay, string pStrKapanName, int pIntPacketNo, string pStrTag, string pStrBarcode, string pStrJangedNo, Int32 pStrPktStNo, Int64 pStrEmployee_ID, int pIntDEPTJangedNo, string pStrDptJangedDate, string pStrSearchType, string pStrDeptJangedNo, string XmlForDeptJanged)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", StrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pStrEmployee_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DISPLAYTYPE", pStrDisplay, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JANGEDNO", pStrJangedNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", pIntPacketNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("BARCODE", pStrBarcode, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PKTSERIALNO", pStrPktStNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", Config.gEmployeeProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("SEARCHTYPE", pStrSearchType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DEPTJANGEDNO", pStrDeptJangedNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("XMLFORDEPTJANGED", XmlForDeptJanged, DbType.Xml, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SinglePacketLiveStockGetDataForReturn", CommandType.StoredProcedure);//K
            return DTab;
        }

        public TrnKapanCreateProperty UpdateKapanlock(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ISKAPANLOCK", pClsProperty.ISKAPANLOCK, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanLockUpdate", CommandType.StoredProcedure);

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

        public TrnKapanCreateProperty UpdateDiameterLock(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("DIAMETERLOCK", pClsProperty.DIAMETERLOCK, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanDiameterLockUpdate", CommandType.StoredProcedure);

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
        //Gunjan:03/01/2025

        public TrnKapanCreateProperty HideKapan(TrnKapanCreateProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ISHIDE", pClsProperty.ISHIDE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanHide", CommandType.StoredProcedure);

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
        public DataTable GetLabPredictionData(string pStrKapan, int pIntFromPktNo, int pIntToPktNo, string pStrTag, string pStrFromDate, string pStrToDate)
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMPACKETNO", pIntFromPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TOPACKETNO", pIntToPktNo, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("TAG", pStrTag, DbType.String, ParameterDirection.Input);           
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "", CommandType.StoredProcedure);

            return DTab;
        }
        //End As Gunjan
    }
}
