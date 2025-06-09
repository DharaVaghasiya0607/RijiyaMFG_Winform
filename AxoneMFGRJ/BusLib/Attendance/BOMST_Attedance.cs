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

namespace BusLib.Attendance
{
    public class BOMST_Attendance
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataSet GetDataForAttendanceEntry(AttendanceEntryProperty pClsProperty)
        {
            //Ope.ClearParams();
            //DataTable DTab = new DataTable();

            //Ope.AddParams("ATDDATE", pClsProperty.ATDDATE, DbType.Date, ParameterDirection.Input);
            //Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            //Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

            //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, SProc.Hr_AttendanceEntryGetData, CommandType.StoredProcedure);
            //return DTab;

            Ope.ClearParams();
            DataSet DS = new DataSet();

            Ope.AddParams("ATDDATE", pClsProperty.ATDDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("EMPLOYEECODE", pClsProperty.MULTIEMPLOYEECODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp1", SProc.Hr_AttendanceEntryGetData, CommandType.StoredProcedure);
            return DS;
        }


        public DataTable GetDataFroAtdProcess(AttendanceEntryProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DS = new DataTable();

            Ope.AddParams("ATDDATE", pClsProperty.ATDDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEECODE", pClsProperty.MULTIEMPLOYEECODE, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DS, "Trn_AtdEntryProcessGetData", CommandType.StoredProcedure);
            return DS;
        }


        public string GetServerDate()
        {
            Ope.ClearParams();
            DataRow DR = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Select Convert(Date,GetDate(),103)", CommandType.Text);
            return Val.ToDate(DR[0].ToString(), BOConversion.DateFormat.DDMMYYYY);
        }


        public AttendanceEntryProperty Save(AttendanceEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ATD_ID", pClsProperty.ATD_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("SRNO", pClsProperty.SRNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ATDDATE", pClsProperty.ATDDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);


                Ope.AddParams("AP", pClsProperty.AP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("WDAYS", pClsProperty.WDAYS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("WHOURS", pClsProperty.WHOURS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("WMINTS", pClsProperty.WMINTS, DbType.Double, ParameterDirection.Input);


                Ope.AddParams("TOTALMIN", pClsProperty.TOTALMIN, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTHH", pClsProperty.OTHH, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTMM", pClsProperty.OTMM, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PUNCH_INDATE", pClsProperty.PUNCH_INDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PUNCH_INTIME", pClsProperty.PUNCH_INTIME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PUNCH_OUTDATE", pClsProperty.PUNCH_OUTDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PUNCH_OUTTIME", pClsProperty.PUNCH_OUTTIME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IDLE_INDATE", pClsProperty.IDLE_INDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IDLE_INTIME", pClsProperty.IDLE_INTIME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IDLE_OUTDATE", pClsProperty.IDLE_OUTDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IDLE_OUTTIME", pClsProperty.IDLE_OUTTIME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PUNCH_INDATETIME", pClsProperty.PUNCH_INDATETIME, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("PUNCH_OUTDATETIME", pClsProperty.PUNCH_OUTDATETIME, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("IDLE_INDATETIME", pClsProperty.IDLE_INDATETIME, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("IDLE_OUTDATETIME", pClsProperty.IDLE_OUTDATETIME, DbType.DateTime, ParameterDirection.Input);

                Ope.AddParams("SHIFTSTARTTIME", pClsProperty.SHIFTSTARTTIME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHIFTENDTIME", pClsProperty.SHIFTENDTIME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SHIFTTYPE", pClsProperty.SHIFTTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISCHANGEIDLETIME", pClsProperty.ISCHANGEIDLETIME, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISSUNDAYABSENT", pClsProperty.ISSUNDAYABSENT, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISCONSIDERSHIFTOUTTIME", pClsProperty.ISCONSIDERSHIFTOUTTIME, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.HR_AttendanceEntrySave, CommandType.StoredProcedure);

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

        public AttendanceEntryProperty Delete(AttendanceEntryProperty pClsProperty,string pStrOpe)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ATD_ID", pClsProperty.ATD_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ATDDATE", pClsProperty.ATDDATE, DbType.Date, ParameterDirection.Input);
                //Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.HR_AttendanceEntryDelete, CommandType.StoredProcedure);

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

        public DataTable GetAttendanceRegister(int pIntYearMonth, int pIntDepartmentID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("YEARMONTH", pIntYearMonth, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartmentID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "HR_AttendanceRegister", CommandType.StoredProcedure);
            return DTab;
        }


        public string GetRemark(Int64 pIntEmployeeID, string pStrAtdYYMMDD)
        {
            Ope.ClearParams();
            return Ope.FindText(Config.ConnectionString, Config.ProviderName, "Trn_Atd", "Remark", " And Employee_ID = '" + pIntEmployeeID + "' And AtdYYYYMMDD='" + pStrAtdYYMMDD + "'");
        }

        public DataTable GetAttendanceRegisterPrint(int pIntYearMonth)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("YEARMONTH", pIntYearMonth, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "HR_AttendanceRegisterPrint", CommandType.StoredProcedure);
            return DTab;
        }
        //ADD BHAGYASHREE 06/08/2019
        public DataTable GetAttendanceCaptureData(string pStrATDDate, int pIntDepartment_ID) //Add : Pinali : 10-07-2019
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("ATDDATE", pStrATDDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pIntDepartment_ID, DbType.Int32, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "HR_AttendanceCapture_GetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetAdminDashboardData(string pStrFromDate, string pStrToDate, Int64 pIntLedger_ID, string pStrOpe) //Used In Admin Dashboard
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("LEDGER_ID", pIntLedger_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_AdminDashboardGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetEmployeeIdleReport(string pStrEmployee_ID) //Used In Admin Dashboard
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("MULTIEMPLOYEE_ID", pStrEmployee_ID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_EmployeeRestReport", CommandType.StoredProcedure);
            return DTab;
        }

    }
}
