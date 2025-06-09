﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AxonDataLib;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;

namespace BusLib.Master
{
    public class BOMST_Report
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetDataSummary(string pStrReportGroup)
        {
            DataTable DTab = new DataTable();

            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORTGROUP", pStrReportGroup, DbType.Int32);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ReportGetData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DTab;
            }
        }


        public DataTable GetDataSummaryForReport(string pStrReportGroup)
        {
            DataTable DTab = new DataTable();

            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORT_ID", 0, DbType.Int32);
                Ope.AddParams("REPORTGROUP", pStrReportGroup, DbType.Int32);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ReportGetDataForReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception ex)
            {
               // BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DTab;
            }
        }


        public DataTable GetDataSummaryForReportNew(string pStrReportGroup)
        {
            DataTable DTab = new DataTable();

            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORT_ID", 1, DbType.Int32);
                Ope.AddParams("REPORTGROUPNEW", pStrReportGroup, DbType.Int32);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_ReportGetDataForReportNew", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception ex)
            {
               // BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DTab;
            }
        }

        public DataRow GetDataByReportID(int pIntReportID)
        {
            DataTable DTab = new DataTable();

            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORT_ID", pIntReportID, DbType.Int32);
                Ope.AddParams("REPORTGROUP", "ALL", DbType.Int32);
                return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "MST_ReportGetDataForReport", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
            }
            return null;
        }


        public DataSet GetData(int pIntReportID)
        {
            DataSet DS = new DataSet();

            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORT_ID", pIntReportID, DbType.Int32);
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "MST_ReportGetData", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
               // BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DS;
            }
        }

        public int FindNewID()
        {
            try
            {
                return Ope.FindNewID(Config.ConnectionString, Config.ProviderName, "MST_Report", "MAX(Report_ID)", " ");

            }
            catch (Exception ex)
            {
               // BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return 0;
            }
            
        }

        public bool CheckRecordExistsOrNot(int IntID)
        {
            try
            {
                string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "MST_Report", "ReportName", " And Report_ID = '" + IntID.ToString() + "'");
                if (string.IsNullOrEmpty(Str))
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
              //  BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return false;
            }
        }


        public MST_ReportProperty Save(MST_ReportProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("REPORT_ID", pClsProperty.REPORT_ID, DbType.Int32);
                Ope.AddParams("REPORTGROUP", pClsProperty.REPORTGROUP, DbType.Int32);
                Ope.AddParams("REPORTGROUPNEW", pClsProperty.REPORTGROUPNEW, DbType.Int32);
                Ope.AddParams("REPORTNAME", pClsProperty.REPORTNAME, DbType.Int32);
                Ope.AddParams("REPORTTYPE", pClsProperty.REPORTTYPE, DbType.Int32);
                Ope.AddParams("FORMNAME", pClsProperty.FORMNAME, DbType.Int32);
                Ope.AddParams("SEQUENCENO", pClsProperty.SEQUENCENO, DbType.Int32);
                Ope.AddParams("SPNAME", pClsProperty.SPNAME, DbType.Int32);
                Ope.AddParams("REPORTVIEW", pClsProperty.REPORTVIEW, DbType.Int32);
                Ope.AddParams("DISPLAYFONTNAME", pClsProperty.DISPLAYFONTNAME, DbType.Int32);
                Ope.AddParams("DISPLAYFONTSIZE", pClsProperty.DISPLAYFONTSIZE, DbType.Int32);
                Ope.AddParams("PRINTFONTNAME", pClsProperty.PRINTFONTNAME, DbType.Int32);
                Ope.AddParams("PRINTFONTSIZE", pClsProperty.PRINTFONTSIZE, DbType.Int32);
                Ope.AddParams("PRINTORIENTATION", pClsProperty.PRINTORIENTATION, DbType.Int32);
                Ope.AddParams("XMLDATA", pClsProperty.XMLDATA, DbType.Int32);
                Ope.AddParams("XMLDATAGROUP", pClsProperty.XMLDATAGROUP, DbType.Int32);
               
                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean);
                Ope.AddParams("ISPRINTFIRMNAME", pClsProperty.ISPRINTFIRMNAME, DbType.Boolean);
                Ope.AddParams("ISPRINTFIRMADDRESS", pClsProperty.ISPRINTFIRMADDRESS, DbType.Boolean);
                Ope.AddParams("ISPRINTFILTERCRITERIA", pClsProperty.ISPRINTFILTERCRITERIA, DbType.Boolean);
                Ope.AddParams("ISPRINTHEADINGONEACHPAGE", pClsProperty.ISPRINTHEADINGONEACHPAGE, DbType.Boolean);
                Ope.AddParams("ISPRINTDATETIME", pClsProperty.ISPRINTDATETIME, DbType.Boolean);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String);
              
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String);
                
                Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ReportSave", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.RETURNVALUE = Val.ToString(AL[0]);
                    pClsProperty.RETURNMESSAGETYPE = Val.ToString(AL[1]);
                    pClsProperty.RETURNMESSAGEDESC = Val.ToString(AL[2]);
                }
            }
            catch (System.Exception ex)
            {
                pClsProperty.RETURNVALUE = "";
                pClsProperty.RETURNMESSAGETYPE = "FAIL";
                pClsProperty.RETURNMESSAGEDESC = ex.Message;
               // BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
           }
            return pClsProperty;

        }

        public MST_ReportProperty Delete(MST_ReportProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPORT_ID", pClsProperty.REPORT_ID, DbType.Int32);
                Ope.AddParams("SRNO", pClsProperty.SRNO, DbType.Int32);
              
                Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGETYPE", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("RETURNMESSAGEDESC", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ReportDelete", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.RETURNVALUE = Val.ToString(AL[0]);
                    pClsProperty.RETURNMESSAGETYPE = Val.ToString(AL[1]);
                    pClsProperty.RETURNMESSAGEDESC = Val.ToString(AL[2]);
                }

            }
            catch (System.Exception ex)
            {
                pClsProperty.RETURNVALUE = "";
                pClsProperty.RETURNMESSAGETYPE = "FAIL";
                pClsProperty.RETURNMESSAGEDESC = ex.Message;
           //     BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
         
            }
            return pClsProperty;

        }

        public DataSet GenerateMaintainanceReport(MST_ReportProperty pClsProperty)
        {
            DataSet DS = new DataSet();

            try
            {
                Ope.ClearParams();

                Ope.AddParams("REPORTTYPE", pClsProperty.REPORTTYPE, DbType.String);
                Ope.AddParams("STOCKTYPE", pClsProperty.STOCKTYPE, DbType.String);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.String);
                Ope.AddParams("FROMPROCESS_ID", pClsProperty.FROMPROCESS_ID, DbType.String);
                Ope.AddParams("TOPROCESS_ID", pClsProperty.TOPROCESS_ID, DbType.String);
                Ope.AddParams("NEXTPROCESS_ID", pClsProperty.NEXTPROCESS_ID, DbType.String);
                Ope.AddParams("FROMDEPARTMENT_ID", pClsProperty.FROMDEPARTMENT_ID, DbType.String);
                Ope.AddParams("TODEPARTMENT_ID", pClsProperty.TODEPARTMENT_ID, DbType.String);
                Ope.AddParams("FROMEMPLOYEE_ID", pClsProperty.FROMEMPLOYEE_ID, DbType.String);
                Ope.AddParams("TOEMPLOYEE_ID", pClsProperty.TOEMPLOYEE_ID, DbType.String);
                Ope.AddParams("FROMMANAGER_ID", pClsProperty.FROMMANAGER_ID, DbType.String);
                Ope.AddParams("TOMANAGER_ID", pClsProperty.TOMANAGER_ID, DbType.String);
                Ope.AddParams("FROMFACTORYHEAD_ID", pClsProperty.FROMFACTORYHEAD_ID, DbType.String);
                Ope.AddParams("TOFACTORYHEAD_ID", pClsProperty.TOFACTORYHEAD_ID, DbType.String);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.String);
                Ope.AddParams("KAPANMANAGER_ID", pClsProperty.KAPANMANAGER_ID, DbType.String);
                Ope.AddParams("BARCODE", pClsProperty.BARCODE, DbType.String);
                Ope.AddParams("JANGEDNO", pClsProperty.JANGEDNO, DbType.String);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.String);
                Ope.AddParams("FROMDATE", pClsProperty.FROMDATE, DbType.String);
                Ope.AddParams("TODATE", pClsProperty.TODATE, DbType.String);

                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String);
                Ope.AddParams("PRICEDATE", pClsProperty.PRICEDATE, DbType.String);


                Ope.AddParams("COMPANY_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANY_ID, DbType.String);
                Ope.AddParams("REPORT_ID", pClsProperty.REPORT_ID, DbType.String);
                Ope.AddParams("EMPLOYEE_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.String);
                Ope.AddParams("GROUPBY", pClsProperty.GROUPBY, DbType.String);


                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", pClsProperty.SPNAME, CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DS;
            }
        }

        public DataSet GenerateMaintainanceReportNew(MST_ReportProperty pClsProperty)//Gunjan : 14/03/2023
        {
            DataSet DS = new DataSet();

            try
            {
                Ope.ClearParams();

                Ope.AddParams("REPORTTYPE", pClsProperty.REPORTTYPE, DbType.String);
                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.String);
                Ope.AddParams("SUPPLIER_ID", pClsProperty.SUPPLIER_ID, DbType.String);
                Ope.AddParams("BROCKER_ID", pClsProperty.BROCKER_ID, DbType.String);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.String);
                Ope.AddParams("MSIZE_ID", pClsProperty.MSIZE_ID, DbType.String);
                Ope.AddParams("MINES", pClsProperty.MINES, DbType.String);
                Ope.AddParams("ARTICLE", pClsProperty.ARTICLE, DbType.String);
                Ope.AddParams("ROUGH", pClsProperty.ROUGH, DbType.String);
                Ope.AddParams("FROMRECEIVEDATE", pClsProperty.FROMRECEIVEDATE, DbType.String);
                Ope.AddParams("TORECEIVEDATE", pClsProperty.TORECEIVEDATE, DbType.String);
                Ope.AddParams("FROMINVOICEDATE", pClsProperty.FROMINVOICEDATE, DbType.String);
                Ope.AddParams("TOINVOICEDATE", pClsProperty.TOINVOICEDATE, DbType.String);


                Ope.AddParams("COMPANY_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANY_ID, DbType.String);
                Ope.AddParams("REPORT_ID", pClsProperty.REPORT_ID, DbType.String);
                Ope.AddParams("EMPLOYEE_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.String);
                Ope.AddParams("GROUPBY", pClsProperty.GROUPBY, DbType.String);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", pClsProperty.SPNAME, CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
                return DS;
            }
        }
    }
}
