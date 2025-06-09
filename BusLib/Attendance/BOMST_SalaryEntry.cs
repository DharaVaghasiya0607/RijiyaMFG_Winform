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
    public class BOMST_SalaryEntry
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetDataForSalaryEntry(string pStrOpe, SalaryEntryProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SALARYDATE", pClsProperty.SALARYDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("YEAR", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MONTH", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
           
            //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, SProc.Hr_SalaryEntryGetData, CommandType.StoredProcedure);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "HR_SalaryEntryGetData_WHOURS", CommandType.StoredProcedure);
            return DTab;
        }

        public SalaryEntryProperty Save(SalaryEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("SALARY_ID", pClsProperty.SALARY_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("SALARYDATE", pClsProperty.SALARYDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("YEAR", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MONTH", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("SALARYTYPE", pClsProperty.SALARYTYPE, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("SALARY", pClsProperty.SALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOTALPCS", pClsProperty.TOTALPCS, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TOTALCARAT", pClsProperty.TOTALCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOTALDAYS", pClsProperty.TOTALDAYS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("WDAYS", pClsProperty.WDAYS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTHOURS", pClsProperty.OTHOURS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOTALHOURS", pClsProperty.TOTALHOURS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("WHOURS", pClsProperty.WHOURS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("WMINTS", pClsProperty.WMINTS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("OTMINTS", pClsProperty.OTMINTS, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TOTALMINTS", pClsProperty.TOTALMINTS, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("AVGSALARY", pClsProperty.AVGSALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GROSSSALARY", pClsProperty.GROSSSALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOTALUPAD", pClsProperty.TOTALUPAD, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("NETSALARY", pClsProperty.NETSALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTSALARY", pClsProperty.OTSALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("NETPAYABLE", pClsProperty.NETPAYABLE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXTRAAMOUNT", pClsProperty.EXTRAAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXTRAREMARK", pClsProperty.EXTRAREMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.HR_SalaryEntrySave, CommandType.StoredProcedure);

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

        public SalaryEntryProperty Delete(SalaryEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                
                Ope.AddParams("YEAR", pClsProperty.YYYY, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MONTH", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SALARY_ID", pClsProperty.SALARY_ID, DbType.Guid, ParameterDirection.Input);

                //Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                //Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.HR_SalaryEntryDelete, CommandType.StoredProcedure);

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
