using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxonDataLib;
using BusLib.TableName;
using System.Data;
using System.Collections;
using System.Windows.Forms;

namespace BusLib.Attendance
{
    public class BOTRN_LeaveEntry
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrFromDate, string pStrToDate,string pStrEmpId,string pStrStatus,Guid gStrLeave_ID)
        {
            Ope.ClearParams();
            DataTable DT = new DataTable();
            Ope.AddParams("LEAVEFROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("LEAVETODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("EMPLOYEE_ID", pStrEmpId, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LEAVESTATUS", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LEAVE_ID", gStrLeave_ID, DbType.Guid, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DT,"TRN_LeaveEntryGetData", CommandType.StoredProcedure);
            return DT;
        }

        public LeaveEntryProperty Save(LeaveEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LEAVE_ID", pClsProperty.LEAVE_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("SLIPNO", pClsProperty.SLIPNO, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SLIPDATE", pClsProperty.SLIPDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("REASON_ID", pClsProperty.REASON_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("OTHERREASON", pClsProperty.OTHERREASON, DbType.String, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LEAVETYPE", pClsProperty.LEAVETYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LEAVEFROMDATE", pClsProperty.LEAVEFROMDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("LEAVETODATE", pClsProperty.LEAVETODATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("TOTALDAYS", pClsProperty.TOTALDAYS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("LEAVEFROMTIME", pClsProperty.LEAVEFROMTIME, DbType.Time, ParameterDirection.Input);
                Ope.AddParams("LEAVETOTIME", pClsProperty.LEAVETOTIME, DbType.Time, ParameterDirection.Input);
                Ope.AddParams("TOTALHOURS", pClsProperty.TOTALHOURS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("HH", pClsProperty.HH, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MM", pClsProperty.MM, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ISOFFICEWORK", pClsProperty.ISOFFICEWORK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("LEAVESTATUS", pClsProperty.LEAVESTATUS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMMENT", pClsProperty.COMMENT, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_LeaveEntrySave", CommandType.StoredProcedure);

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
        public int UpdatePendingLeaveStatus(LeaveEntryProperty pClsProperty)
        {
            int i = 0;
            try
            {
                Ope.ClearParams();

                string StrQuery = "";
                StrQuery = "Update TRN_Leave WITH(ROWLOCK) SET ";
                StrQuery = StrQuery + "COMMENT = '" + pClsProperty.COMMENT + "' ,";
                StrQuery = StrQuery + "LEAVESTATUS = '" + pClsProperty.LEAVESTATUS + "' ,";
                //StrQuery = StrQuery + "STATUSUPDATEDATE = '" + (pClsProperty.STATUSUPDATEDATE) + "' ,";
                StrQuery = StrQuery + "STATUSUPDATEDATE = '" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "' ,";
                StrQuery = StrQuery + "STATUSUPDATEBY = '" + Val.ToString(Config.gEmployeeProperty.LEDGER_ID) + "' ,";
                StrQuery = StrQuery + "STATUSUPDATEIP = '" + Val.ToString(Config.ComputerIP) + "' ";
                StrQuery = StrQuery + "WHERE LEAVE_ID = '" + Val.ToString(pClsProperty.LEAVE_ID)+ "' ";

                i = Ope.ExeNonQuery(BusLib.Configuration.BOConfiguration.ConnectionString, BusLib.Configuration.BOConfiguration.ProviderName, StrQuery, CommandType.Text);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return i;
        }

        public int CheckValidationForLiveApproval(Int64 EMP_ID)   //ADD Bhagyashree 25/07/2019
        {
            int J = 0;
            try
            {
                Ope.ClearParams();
                Ope.AddParams("EMP_ID", EMP_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("RETURNVALUE", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_CheckValidationForLiveApproval", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    J = Val.ToInt(AL[0]);
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
            return J;
        }
    }
}
