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
    public class BOMST_FormPermission
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataSet Fill(Int64 pIntEmployeeID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "MST_FormPermissionGetData", CommandType.StoredProcedure);
            return DS;
        }

        public DataTable CheckUsername(Int64 pEmployeeID, string pUserName)
        {
            DataTable Dtab = new DataTable();
            Ope.ClearParams();
            string Str = "SELECT Isnull(Username,'') as Username FROM MST_Ledger With(RowLock) Where Ledger_ID <> " + pEmployeeID + " And UserName = '" + pUserName + "' ; ";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, Str, CommandType.Text);
            return Dtab;
        }



        public int Save(Int64 pIntEmployeeID, DataTable DTab, DataTable pDTabDisplay, DataTable pDTabTransfer, DataTable pDTabProcess, DataTable pDTabReport, EmployeeActionRightsProperty pClsProperty)
        {
            int IntRes = 0;

            try
            {

                Ope.ClearParams();
                string Str = "DELETE FROM MST_FORMPERMISSION With(RowLock) Where Employee_ID = '" + pIntEmployeeID + "' ; ";
                Str = Str + " DELETE FROM MST_EmployeeAccessRights With(RowLock) Where Employee_ID = '" + pIntEmployeeID + "' ; ";
                string Str1 = "DELETE FROM MST_REPORTPERMISSION  With(RowLock) Where Employee_ID = '" + pIntEmployeeID + "' ; ";
                Str1 = Str1 + " DELETE FROM MST_EmployeeAccessRights With(RowLock) Where Employee_ID = '" + pIntEmployeeID + "' ; ";
              
                Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
                Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str1, CommandType.Text);


                foreach (DataRow DRow in DTab.Rows)
                {
                    Ope.ClearParams();
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("FORM_ID", Val.ToInt32(DRow["FORM_ID"]), DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("ISVIEW", Val.ToBoolean(DRow["ISVIEW"]), DbType.Boolean, ParameterDirection.Input);
                    Ope.AddParams("ISINSERT", Val.ToBoolean(DRow["ISINSERT"]), DbType.Boolean, ParameterDirection.Input);
                    Ope.AddParams("ISUPDATE", Val.ToBoolean(DRow["ISUPDATE"]), DbType.Boolean, ParameterDirection.Input);
                    Ope.AddParams("ISDELETE", Val.ToBoolean(DRow["ISDELETE"]), DbType.Boolean, ParameterDirection.Input);
                    Ope.AddParams("PASSWORD", Val.ToString(DRow["PASSWORD"]), DbType.String, ParameterDirection.Input);
                    Ope.AddParams("IP", Val.ToString(DRow["IP"]), DbType.String, ParameterDirection.Input);
                    IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_FormPermissionSave", CommandType.StoredProcedure);
                }

                foreach (DataRow DRow in pDTabDisplay.Rows)
                {
                    if (Val.ToBoolean(DRow["SEL"]) == false)
                    {
                        continue;
                    }

                    Ope.ClearParams();
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ACCESSEMPLOYEE_ID", Val.ToInt64(DRow["EMPLOYEE_ID"]), DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ACCESSTYPE", "DISPLAY", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("ISSUETYPE", Val.ToString(DRow["ISSUETYPE"]), DbType.String, ParameterDirection.Input); // K : 24/12/2022
                    Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                    IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_FormPermissionEmployeeRightsSave", CommandType.StoredProcedure);
                }

                foreach (DataRow DRow in pDTabTransfer.Rows)
                {
                    if (Val.ToBoolean(DRow["SEL"]) == false)
                    {
                        continue;
                    }
                    Ope.ClearParams();
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ACCESSEMPLOYEE_ID", Val.ToInt64(DRow["EMPLOYEE_ID"]), DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ACCESSTYPE", "TRANSFER", DbType.String, ParameterDirection.Input);
                    Ope.AddParams("ISSUETYPE", Val.ToString(DRow["ISSUETYPE"]), DbType.String, ParameterDirection.Input); // K : 24/12/2022
                    Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                    IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_FormPermissionEmployeeRightsSave", CommandType.StoredProcedure);
                }

                foreach (DataRow DRow in pDTabProcess.Rows)
                {
                    if (Val.Val(DRow["TOAMOUNT"]) == 0)
                    {
                        continue;
                    }
                    Ope.ClearParams();
                    Ope.AddParams("PROCESSSETTING_ID", Val.ToString(DRow["PROCESSSETTING_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(DRow["PROCESSSETTING_ID"].ToString()), DbType.Guid, ParameterDirection.Input);
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("SRNO", Val.ToInt32(DRow["SRNO"]), DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("PROCESS_ID", Val.ToInt32(DRow["PROCESS_ID"]), DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("FROMAMOUNT", Val.Val(DRow["FROMAMOUNT"]), DbType.Decimal, ParameterDirection.Input);
                    Ope.AddParams("TOAMOUNT", Val.Val(DRow["TOAMOUNT"]), DbType.Decimal, ParameterDirection.Input);
                    Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                    IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_EmployeeProcessSettingSave", CommandType.StoredProcedure);
                }

                foreach (DataRow DRow in pDTabReport.Rows)// add by Urvisha : 21012023  
                {

                    Ope.ClearParams();
                    Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("REPORT_ID", Val.ToInt32(DRow["REPORT_ID"]), DbType.Int32, ParameterDirection.Input);
                    Ope.AddParams("ISVIEW", Val.ToBoolean(DRow["ISVIEW"]), DbType.Boolean, ParameterDirection.Input);
                    Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                    Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                    IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_ReportPermissionSave", CommandType.StoredProcedure);
                }

                Ope.ClearParams();

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("PRDTYPE_ID", pClsProperty.PRDTYPE_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISFULLSTOCK", pClsProperty.ISFULLSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISDEPTSTOCK", pClsProperty.ISDEPTSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMYSTOCK", pClsProperty.ISMYSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISOTHERSTOCK", pClsProperty.ISOTHERSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("DEPTTRANSFER", pClsProperty.DEPTTRANSFER, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("EMPISSUE", pClsProperty.EMPISSUE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("EMPRETURN", pClsProperty.EMPRETURN, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("RETURNWITHSPLIT", pClsProperty.RETURNWITHSPLIT, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("REJECTIONTRANSFER", pClsProperty.REJECTIONTRANSFER, DbType.Boolean, ParameterDirection.Input);
                // Dhara : 21-04-2022
                Ope.AddParams("ISMAINFULLSTOCK", pClsProperty.ISMAINFULLSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMAINDEPTSTOCK", pClsProperty.ISMAINDEPTSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMAINMYSTOCK", pClsProperty.ISMAINMYSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISMAINOTHERSTOCK", pClsProperty.ISMAINOTHERSTOCK, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISSUBFULLSTOCK", pClsProperty.ISSUBFULLSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBDEPTSTOCK", pClsProperty.ISSUBDEPTSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBMYSTOCK", pClsProperty.ISSUBMYSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBOTHERSTOCK", pClsProperty.ISSUBOTHERSTOCK, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISSUBDEPTTRANSFER", pClsProperty.ISSUBDEPTTRANSFER, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBEMPISSUE", pClsProperty.ISSUBEMPISSUE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBEMPRETURN", pClsProperty.ISSUBEMPRETURN, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBRETURNWITHSPLIT", pClsProperty.ISSUBRETURNWITHSPLIT, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISSUBREJECTIONTRANSFER", pClsProperty.ISSUBREJECTIONTRANSFER, DbType.Boolean, ParameterDirection.Input);
                // Dhara : 21-04-2022
                Ope.AddParams("IPADDRESS", pClsProperty.IPADDRESS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ALLOWALLIP", pClsProperty.ALLOWALLIP, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("MAXPACKETSTOCK", pClsProperty.MAXPACKETSTOCK, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("USERNAME", pClsProperty.USERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PASSWORD", pClsProperty.PASSWORD, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RAPPASSFORDISPDISC", pClsProperty.RAPPASSFORDISPDISC, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RAPCHANGEEMPLOYEE", pClsProperty.RAPCHANGEEMPLOYEE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("RAPCHANGEPACKETS", pClsProperty.RAPCHANGEPACKETS, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("RAPUPDATEPREDICTION", pClsProperty.RAPUPDATEPREDICTION, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ISALLOWEXTRAMIN", pClsProperty.ISALLOWEXTRAMIN, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("EXTRAMINPER", pClsProperty.EXTRAMINPER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ISCONFIRMGRADER", pClsProperty.ISCONFIRMGRADER, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISGROUPJANGADNO", pClsProperty.ISGROUPJANGADNO, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("BPRINTTYPE", pClsProperty.BPRINTTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("UPLOADSERVERPATH", pClsProperty.UPLOADSERVERPATH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("UPLOADSERVERUSERNAME", pClsProperty.UPLOADSERVERUSERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("UPLOADSERVERPASSWORD", pClsProperty.UPLOADSERVERPASSWORD, DbType.String, ParameterDirection.Input);

                Ope.AddParams("QCMAINSERVERPATH", pClsProperty.QCMAINSERVERPATH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("QCMAINSERVERUSERNAME", pClsProperty.QCMAINSERVERUSERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("QCMAINSERVERPASSWORD", pClsProperty.QCMAINSERVERPASSWORD, DbType.String, ParameterDirection.Input);

                Ope.AddParams("QCUSERWISESERVERPATH", pClsProperty.QCUSERWISESERVERPATH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("QCUSERWISEUSERNAME", pClsProperty.QCUSERWISEUSERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("QCUSERWISEPASSWARD", pClsProperty.QCUSERWISEPASSWARD, DbType.String, ParameterDirection.Input);

                // #Dhara : 14-4-2023
                Ope.AddParams("ISDOLLARLOCK", pClsProperty.ISDOLLARLOCK, DbType.Boolean, ParameterDirection.Input);
                // #Dhara : 14-04-2023

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                IntRes = IntRes + Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "MST_EmployeeActionRightsSave", CommandType.StoredProcedure);
            }
            catch (System.Exception ex)
            {
                return -1;
            }

            return IntRes;
        }

        public EmployeeActionRightsProperty EmployeeActionRightsGetDataByPK(Int64 pIntEmployeeID)
        {
            Ope.ClearParams();
            Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);
            DataRow DRow = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "MST_EmployeeActionRightsGetdata", CommandType.StoredProcedure);

            EmployeeActionRightsProperty Property = new EmployeeActionRightsProperty();
            Property.EMPLOYEE_ID = pIntEmployeeID;
            Property.PRDTYPE_ID = "";
            Property.ISFULLSTOCK = false;
            Property.ISDEPTSTOCK = false;
            Property.ISMYSTOCK = false;
            Property.ISOTHERSTOCK = false;
            Property.DEPTTRANSFER = false;
            Property.EMPISSUE = false;
            Property.EMPRETURN = false;
            Property.RETURNWITHSPLIT = false;
            // Dhara : 21-04-22
            Property.ISMAINFULLSTOCK = false;
            Property.ISMAINDEPTSTOCK = false;
            Property.ISMAINMYSTOCK = false;
            Property.ISMAINOTHERSTOCK = false;

            Property.ISSUBFULLSTOCK = false;
            Property.ISSUBDEPTSTOCK = false;
            Property.ISSUBMYSTOCK = false;
            Property.ISSUBOTHERSTOCK = false;

            Property.ISSUBDEPTTRANSFER = false;
            Property.ISSUBEMPISSUE = false;
            Property.ISSUBEMPRETURN = false;
            Property.ISSUBRETURNWITHSPLIT = false;
            // Dhara : 21-04-22
            Property.IPADDRESS = "";
            Property.ALLOWALLIP = false;
            Property.REJECTIONTRANSFER = false;

            Property.RAPPASSFORDISPDISC = "";
            Property.RAPCHANGEEMPLOYEE = false;
            Property.RAPCHANGEPACKETS = false;
            Property.RAPUPDATEPREDICTION = false;
            Property.MAXPACKETSTOCK = 0;
            Property.USERNAME = string.Empty;
            Property.PASSWORD = string.Empty;

            // #D: 02-09-2020
            Property.ISALLOWEXTRAMIN = false;
            Property.EXTRAMINPER = 0;
            //# end 02-09-2020
            Property.ISCONFIRMGRADER = false;

            Property.ISGROUPJANGADNO = false;
            Property.BPRINTTYPE = "";
            Property.UPLOADSERVERPATH = "";
            Property.UPLOADSERVERUSERNAME = "";
            Property.UPLOADSERVERPASSWORD = "";

            Property.QCMAINSERVERPATH = "";
            Property.QCMAINSERVERUSERNAME = "";
            Property.QCMAINSERVERPASSWORD = "";

            Property.QCUSERWISESERVERPATH = "";
            Property.QCUSERWISEUSERNAME = "";
            Property.QCUSERWISEPASSWARD = "";

            Property.ISDOLLARLOCK = false;

            if (DRow != null)
            {
                Property.PRDTYPE_ID = Val.ToString(DRow["PRDTYPE_ID"]);
                Property.ISFULLSTOCK = Val.ToBoolean(DRow["ISFULLSTOCK"]);
                Property.ISDEPTSTOCK = Val.ToBoolean(DRow["ISDEPTSTOCK"]);
                Property.ISMYSTOCK = Val.ToBoolean(DRow["ISMYSTOCK"]);
                Property.ISOTHERSTOCK = Val.ToBoolean(DRow["ISOTHERSTOCK"]);
                Property.DEPTTRANSFER = Val.ToBoolean(DRow["DEPTTRANSFER"]);
                Property.EMPISSUE = Val.ToBoolean(DRow["EMPISSUE"]);
                Property.EMPRETURN = Val.ToBoolean(DRow["EMPRETURN"]);
                Property.REJECTIONTRANSFER = Val.ToBoolean(DRow["REJECTIONTRANSFER"]);
                Property.RETURNWITHSPLIT = Val.ToBoolean(DRow["RETURNWITHSPLIT"]);
                // DHARA : 21-04-22
                Property.ISMAINFULLSTOCK = Val.ToBoolean(DRow["ISMAINFULLSTOCK"]);
                Property.ISMAINDEPTSTOCK = Val.ToBoolean(DRow["ISMAINDEPTSTOCK"]);
                Property.ISMAINMYSTOCK = Val.ToBoolean(DRow["ISMAINMYSTOCK"]);
                Property.ISMAINOTHERSTOCK = Val.ToBoolean(DRow["ISMAINOTHERSTOCK"]);

                Property.ISSUBFULLSTOCK = Val.ToBoolean(DRow["ISSUBFULLSTOCK"]);
                Property.ISSUBDEPTSTOCK = Val.ToBoolean(DRow["ISSUBDEPTSTOCK"]);
                Property.ISSUBMYSTOCK = Val.ToBoolean(DRow["ISSUBMYSTOCK"]);
                Property.ISSUBOTHERSTOCK = Val.ToBoolean(DRow["ISSUBOTHERSTOCK"]);

                Property.ISSUBDEPTTRANSFER = Val.ToBoolean(DRow["ISSUBDEPTTRANSFER"]);
                Property.ISSUBEMPISSUE = Val.ToBoolean(DRow["ISSUBEMPISSUE"]);
                Property.ISSUBEMPRETURN = Val.ToBoolean(DRow["ISSUBEMPRETURN"]);
                Property.ISSUBREJECTIONTRANSFER = Val.ToBoolean(DRow["ISSUBREJECTIONTRANSFER"]);
                Property.ISSUBRETURNWITHSPLIT = Val.ToBoolean(DRow["ISSUBRETURNWITHSPLIT"]);
                // Dhara : 21-04-22
                Property.IPADDRESS = Val.ToString(DRow["IPADDRESS"]);
                Property.ALLOWALLIP = Val.ToBoolean(DRow["ALLOWALLIP"]);

                Property.RAPPASSFORDISPDISC = Val.ToString(DRow["RAPPASSFORDISPDISC"]);
                Property.RAPCHANGEEMPLOYEE = Val.ToBoolean(DRow["RAPCHANGEEMPLOYEE"]);
                Property.RAPCHANGEPACKETS = Val.ToBoolean(DRow["RAPCHANGEPACKETS"]);
                Property.RAPUPDATEPREDICTION = Val.ToBoolean(DRow["RAPUPDATEPREDICTION"]);
                Property.MAXPACKETSTOCK = Val.ToInt(DRow["MAXPACKETSTOCK"]);
                Property.USERNAME = Val.ToString(DRow["USERNAME"]);
                Property.PASSWORD = Val.ToString(DRow["PASSWORD"]);
                Property.ISALLOWEXTRAMIN = Val.ToBoolean(DRow["ISALLOWEXTRAMIN"]);
                Property.EXTRAMINPER = Val.Val(DRow["EXTRAMINPER"]);
                Property.ISCONFIRMGRADER = Val.ToBoolean(DRow["ISCONFIRMGRADER"]);
                Property.ISGROUPJANGADNO = Val.ToBoolean(DRow["ISGROUPJANGADNO"]);
                Property.BPRINTTYPE = Val.ToString(DRow["BPRINTTYPE"]);
                Property.UPLOADSERVERPATH = Val.ToString(DRow["UPLOADSERVERPATH"]);
                Property.UPLOADSERVERUSERNAME = Val.ToString(DRow["UPLOADSERVERUSERNAME"]);
                Property.UPLOADSERVERPASSWORD = Val.ToString(DRow["UPLOADSERVERPASSWORD"]);
                Property.QCMAINSERVERPATH = Val.ToString(DRow["QCMAINSERVERPATH"]);
                Property.QCMAINSERVERUSERNAME = Val.ToString(DRow["QCMAINSERVERUSERNAME"]);
                Property.QCMAINSERVERPASSWORD = Val.ToString(DRow["QCMAINSERVERPASSWORD"]);

                Property.QCUSERWISESERVERPATH = Val.ToString(DRow["QCUSERWISESERVERPATH"]);
                Property.QCUSERWISEUSERNAME = Val.ToString(DRow["QCUSERWISEUSERNAME"]);
                Property.QCUSERWISEPASSWARD = Val.ToString(DRow["QCUSERWISEPASSWARD"]);

                Property.ISDOLLARLOCK = Val.ToBoolean(DRow["ISDOLLARLOCK"]); // Dhara : 14-04-2023
            }
            return Property;
        }


        public DataTable GetUserAuthenticationGetData(Int64 pIntEmployeeID)
        {

            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_EmployeePermissionGetDataAuthentication", CommandType.StoredProcedure);
            return DTab;

        }

        public string GetMessage()
        {
            Ope.ClearParams();
            string Str = "Select SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY = 'MESSAGE'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

        }


        public string GetRapnetUserName()
        {
            Ope.ClearParams();
            string Str = "Select SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY = 'RapnetUserName'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

        }

        public string GetRapnetPassword()
        {
            Ope.ClearParams();
            string Str = "Select SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY = 'RapnetPassword'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

        }

        public int SaveMessage(string Message)
        {
            Ope.ClearParams();
            string Str = "Update MST_Setting With(RowLock) Set SETTINGVALUE='" + Message + "' Where SETTINGKEY = 'MESSAGE'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }

        public int SaveTransactionLockUnlock(string pStrMessage) //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Update MST_Setting With(RowLock) Set SETTINGVALUE='" + pStrMessage + "'";
            Str = Str + ", UpdateDate = GETDATE() , UpdateBy = " + Val.ToString(Config.gEmployeeProperty.LEDGER_ID) + " , UpdateIP = '" + Val.ToString(Config.ComputerIP) + "'";
            Str = Str + " Where UPPER(SETTINGKEY) = 'TRANSACTIONLOCK'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }
        public int SaveDepartmentIssueReturnLockUnlock(string pStrMessage) //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Update MST_Setting With(RowLock) Set SETTINGVALUE='" + pStrMessage + "'";
            Str = Str + ", UpdateDate = GETDATE() , UpdateBy = " + Val.ToString(Config.gEmployeeProperty.LEDGER_ID) + " , UpdateIP = '" + Val.ToString(Config.ComputerIP) + "'";
            Str = Str + " Where UPPER(SETTINGKEY) = 'DEPARTMENTISSUERETURNLOCK'";
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }

        public string GetTransactionLockUnlockValue() //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            //string Str = "Select Upper(Isnull(SETTINGVALUE,'')) AS SETTINGVALUE From MST_Setting With(Nolock) Where UPPER(SETTINGKEY) = 'TRANSACTIONLOCK'";
            //return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);

            string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "MST_Setting", "Upper(Isnull(SETTINGVALUE,'')) ", " And UPPER(SETTINGKEY) = 'TRANSACTIONLOCK'");

            return Str;

        }
        public string GetDepartmentIssueReturnLockUnlockkValue() //Add : Pinali : 04-12-2019 : Used In Transaction Lock/Unlock
        {
            Ope.ClearParams();
            string Str = "Select Upper(Isnull(SETTINGVALUE,'')) AS SETTINGVALUE From MST_Setting With(Nolock) Where UPPER(SETTINGKEY) = 'DEPARTMENTISSUERETURNLOCK'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }
        //public string GetExeUpdatePath() //Add : Pinali : 04-12-2019 : Used In Exe Update Get Path
        //{
        //    string StrRes = "";
        //    try
        //    {
        //        Ope.ClearParams();
        //        string Str = "Select Upper(Isnull(SETTINGVALUE,'')) AS SETTINGVALUE From MST_Setting With(Nolock) Where UPPER(SETTINGKEY) = 'EXEUPDATEPATH'";
        //        StrRes = Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        //    }
        //    catch (Exception ex)
        //    {
        //        StrRes = string.Empty;
        //    }
        //    return StrRes;
        //}
        public DataTable GetSettingDataForExePathAndConnection() //Add : Pinali : 04-12-2019 : Used In Exe Update Get Path
        {
            string StrRes = "";
            DataTable DTab = new DataTable();
            try
            {
                Ope.ClearParams();
                string Str = "Select (Isnull(SETTINGKEY,'')) AS SETTINGKEY, (Isnull(SETTINGVALUE,'')) AS SETTINGVALUE From MST_Setting With(Nolock) Where UPPER(SETTINGKEY) LIKE '%EXEUPDATEPATH%'";
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            }
            catch (Exception ex)
            {
                StrRes = string.Empty;
            }
            return DTab;
        }

        public string GetFileTransferUsername()
        {
            Ope.ClearParams();
            string Str = "Select SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY LIKE 'FILETRANSFERUSERNAME'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }
        public string GetFileTransferPassword()
        {
            Ope.ClearParams();
            string Str = "Select SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY LIKE 'FILETRANSFERPASSWORD'";
            return Ope.ExeScal(Config.ConnectionString, Config.ProviderName, Str, CommandType.Text);
        }

        public string GetNoteImagePath() //Add : Pinali : 16-01-2021
        {
            Ope.ClearParams();
            string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "MST_Setting", "Upper(Isnull(SETTINGVALUE,'')) ", " And UPPER(SETTINGKEY) = 'NOTEIMAGEPATH'");

            return Str;

        }
        public DataSet GetDataKapanTransferTableList()
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            //Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "UTI_KapanTransferTableListGetData", CommandType.StoredProcedure);
            return DS;
        }
        public DataTable GetRapCalcSharingFileCredential() //#P : 19-02-2022 : Use In RapCalc : Import Button
        {
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            string Str = "Select SETTINGKEY,SETTINGVALUE FROM MST_Setting With(NoLock) Where SETTINGKEY In ('RAPCALCFILESHARINGUSERNAME','RAPCALCFILESHARINGPASSWORD','RAPCALCFILESHARINGPATH')";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, Str, CommandType.Text);
            return DTab;
        }
    }
}
