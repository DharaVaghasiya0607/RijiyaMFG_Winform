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

namespace BusLib.Master
{
    public class BOTRN_KapanInward
    {

        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        public DataTable GetDataForKapanCreate(string pStrXmlValues)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANINWARDXML", pStrXmlValues, DbType.Xml, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_KapanInwardSingleToMixGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParcelKapanInwardProperty Save(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("INWARDNO", pClsProperty.INWARDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("INWARDDATE", pClsProperty.INWARDDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("KAPANINWARDXML", pClsProperty.StrInwardXml, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("INWARDXML", pClsProperty.StrXmlValuesForInwardXml, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_KapanInwardSave", CommandType.StoredProcedure);

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

        public DataTable FillDetail(string pStrInwardNo, string pStrInwardDate, string pStrKapanName)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "DETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("INWARDNO", pStrInwardNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("INWARDDATE", pStrInwardDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_KapanInwardGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetDataForOutward(string KapanName, string pStrFromDate, string pStrToDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", KapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_KapanInwardForOutwardGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParcelKapanInwardProperty Delete(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {

                Ope.ClearParams();
                Ope.AddParams("INWARDNO", pClsProperty.INWARDNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("INWARD_ID", pClsProperty.INWARD_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_KapanInwardDelete", CommandType.StoredProcedure);

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

        public DataTable FillSummary(string pStrInwardNo, string pStrKapan, string pStrFromData, string pStrToDate, string pStrStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "SUMMARY", DbType.String, ParameterDirection.Input);
            Ope.AddParams("INWARDNO", pStrInwardNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromData, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("INWARDDATE", null, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STATUS", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_KapanInwardGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public DataSet FillSummaryForOutward(string pStrInwardNo, string pStrKapan, string pStrFromData, string pStrToDate, string pStrStatus)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("OPE", "SUMMARY", DbType.String, ParameterDirection.Input);
            Ope.AddParams("INWARDNO", pStrInwardNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromData, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("INWARDDATE", null, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STATUS", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName,DS, "TEMP", "Parcel_KapanInwardGetDataForOutward", CommandType.StoredProcedure);
            return DS;
        }

        public DataTable SizeAssortmentGetKapanData(string pStrInwardNo, string pStrKapan, string pStrFromData, string pStrToDate, string pStrStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "KAPAN", DbType.String, ParameterDirection.Input);
            Ope.AddParams("INWARDNO", pStrInwardNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromData, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STATUS", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_SizeAssortmentGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParcelKapanInwardProperty SizeAssortmentSave(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("INWARD_ID", pClsProperty.INWARD_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZEASSORTXML", pClsProperty.StrInwardXml, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_SizeAssortmentSave", CommandType.StoredProcedure);

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


        public ParcelKapanInwardProperty SizeAssortmentDelete(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("INWARD_ID", pClsProperty.INWARD_ID, DbType.Guid, ParameterDirection.Input);
                // Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_SizeAssortmentDelete", CommandType.StoredProcedure);

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

        public DataTable SizeAssortmentGetSizeData(string pStrKapanName , int pIntShapeID, bool IsAdditionalAssortment)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "DETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pIntShapeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);
           

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_SizeAssortmentGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable ClarityAssortmentGetKapanData(string pStrKapan, string pStrFromData, string pStrToDate, string pStrStatus, string pStrMixSizeID, Guid pGuidUser_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "SIZE", DbType.String, ParameterDirection.Input);
            //Ope.AddParams("INWARDNO", pStrInwardNo, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("FROMDATE", pStrFromData, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("STATUS", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("MIXSIZE_ID", pStrMixSizeID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", Config.gEmployeeProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("USER_ID", pGuidUser_ID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_ClarityAssortmentGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParcelKapanInwardProperty ClarityAssortmentSave(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MIXSIZE_ID", pClsProperty.MIXSIZE_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SIZEASSORT_ID", pClsProperty.SIZEASSORT_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("SIZEASSORTXML", pClsProperty.StrInwardXml, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.Int32, ParameterDirection.Input);


                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_ClarityAssortmentSave", CommandType.StoredProcedure);

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

        public ParcelKapanInwardProperty ClarityAssortmentDelete(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MIXSIZE_ID", pClsProperty.MIXSIZE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_ClarityAssortmentDelete", CommandType.StoredProcedure);

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

        public DataTable ClarityAssortmentGetSizeData(string pKapanName, Guid pStrSizeAssortID, int pIntDepartmentID, string pStrMixSize_ID, Int32 pStrShape_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", "DETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", pKapanName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SIZEASSORT_ID", pStrSizeAssortID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartmentID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MIXSIZE_ID", pStrMixSize_ID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pStrShape_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("YEAR_ID", Config.FINYEAR_ID, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_ClarityAssortmentGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetClarityAssortmentData(string pStrKapan, string pIntPriceDate)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("KAPANNAME", pStrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PRICEDATE", Val.SqlDate(pIntPriceDate), DbType.Date, ParameterDirection.Input);//Gunjan:19/04/2023
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "View_KapanAnalysisGetData", CommandType.StoredProcedure);
            return DS;
        }

        public TrnFancyRateProperty IsAdditionalSave(TrnFancyRateProperty pClsProperty)   //urvisha :26/05/2
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("DETAIL_ID ", pClsProperty.DETAIL_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SIZEASSROT_ID", pClsProperty.SIZEASSROT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("INWARD_ID", pClsProperty.INWARD_ID, DbType.Guid, ParameterDirection.Input);

                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("SIZE_ID", pClsProperty.SIZE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SIZENAME", pClsProperty.SIZENAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("CARAT", pClsProperty.CARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("AMOUNT", pClsProperty.AMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("UPDATEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("UPDATEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SizeAssortmentDetailSave", CommandType.StoredProcedure);

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
        public DataTable IsAdditionalGet (string pStrKapanName)  //urvisha :26052023
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Parcel_MIXSizeAssortmentDetail", CommandType.StoredProcedure);
            return DTab;
        }

        public ParcelKapanInwardProperty UpdateOutwardDate(ParcelKapanInwardProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("OUTWARDDATE", pClsProperty.OUTWARDDATE, DbType.String, ParameterDirection.Input); //#p : 04-10-2022

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerMACID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Parcel_KapanOutwardSave", CommandType.StoredProcedure);

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
