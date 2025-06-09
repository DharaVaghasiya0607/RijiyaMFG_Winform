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
    public class BOMST_Ledger
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrGroup,string StrActive = "ALL")
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", StrActive, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LEDGERGROUP", pStrGroup, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
               
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, SProc.MST_LedgerGetData, CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable PrintIDCard(string pStrXML)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("xml", pStrXML, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_LedgerPrintIDCard", CommandType.StoredProcedure);
            return DTab;
        }

        public Int32 FindMaxLedgerCode()
        {
            int pIntLedgerCode = 0; string StrQuery = "";

            Ope.ClearParams();
            DataTable DTab = new DataTable();
            StrQuery = "SELECT MAX(LEDGERCODE) AS LedgerCode FROM MST_LEDGER WITH(NOLOCK) WHERE 1=1";

            pIntLedgerCode = Val.ToInt32(Ope.ExeScal(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text));
            return pIntLedgerCode + 1;
        }

        public LedgerMasterProperty Save(LedgerMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LEDGER_ID", pClsProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LEDGERCODE", pClsProperty.LEDGERCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LEDGERNAME", pClsProperty.LEDGERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LEDGERGROUP", pClsProperty.LEDGERGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONTACTPERSON", pClsProperty.CONTACTPERSON, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MOBILENO1", pClsProperty.MOBILENO1, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MOBILENO2", pClsProperty.MOBILENO2, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CREDITAMOUNT", pClsProperty.CREDITAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRNTYPECREDIT", pClsProperty.TRNTYPECREDIT, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DEBITAMOUNT", pClsProperty.DEBITAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TRNTYPEDEBIT", pClsProperty.TRNTYPEDEBIT, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BANKNAME", pClsProperty.BANKNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BANKIFSCCODE", pClsProperty.BANKIFSCCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BANKACCOUNTNO", pClsProperty.BANKACCOUNTNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BANKACCOUNTNAME", pClsProperty.BANKACCOUNTNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("GSTNO", pClsProperty.GSTNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CSTNO", pClsProperty.CSTNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PANNO", pClsProperty.PANNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BILLINGADDRESS", pClsProperty.BILLINGADDRESS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BILLINGSTATE", pClsProperty.BILLINGSTATE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("SHIPPINGADDRESS", pClsProperty.SHIPPINGADDRESS, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHIPPINGSTATE", pClsProperty.SHIPPINGSTATE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                
                Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("DESIGNATION_ID", pClsProperty.DESIGNATION_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("MANAGER_ID", pClsProperty.MANAGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("SALARY", pClsProperty.SALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("SALARYTYPE", pClsProperty.SALARYTYPE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("EXPSALARY", pClsProperty.EXPSALARY, DbType.Double, ParameterDirection.Input);
                //Ope.AddParams("USERNAME", pClsProperty.USERNAME, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("PASSWORD", pClsProperty.PASSWORD, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ADHARNO", pClsProperty.ADHARNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EMPLOYEETYPE", pClsProperty.EMPLOYEETYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STYDY", pClsProperty.STYDY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("IDCARDNO", pClsProperty.IDCARDNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONTACTPERSONMOBILENO", pClsProperty.CONTACTPERSONMOBILENO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVCOMPANYNAME", pClsProperty.PREVCOMPANYNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVDESIGNATION", pClsProperty.PREVDESIGNATION, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PREVSALARY", pClsProperty.PREVSALARY, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOTALEXP", pClsProperty.TOTALEXP, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EMPPHOTO", pClsProperty.EMPPHOTO, DbType.Object, ParameterDirection.Input);

                Ope.AddParams("DATEOFJOIN", pClsProperty.DATEOFJOIN, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("DATEOFBIRTH", pClsProperty.DATEOFBIRTH, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("DATEOFLEAVE", pClsProperty.DATEOFLEAVE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("LEAVEREASON", pClsProperty.LEAVEREASON, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONTACTPERSONCODE", pClsProperty.CONTACTPERSONCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CONTACTPERSONADDRESS", pClsProperty.CONTACTPERSONADDRESS, DbType.String, ParameterDirection.Input);


                //08-04-2019
                Ope.AddParams("DOMAINKNOWLEDGE", pClsProperty.DOMAINKNOWLEDGE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("STUDYPER", pClsProperty.STUDYPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("STUDYCOLLEGENAME", pClsProperty.STUDYCOLLEGENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LANGUAGEKNOWN", pClsProperty.LANGUAGEKNOWN, DbType.String, ParameterDirection.Input);

                Ope.AddParams("AGE", pClsProperty.AGE, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RELIGION", pClsProperty.RELIGION, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CAST", pClsProperty.CAST, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BLOODGROUP", pClsProperty.BLOODGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MARRIEDSTATUS", pClsProperty.MARRIEDSTATUS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("VILLAGENAME", pClsProperty.VILLAGENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VILLAGETALUKA", pClsProperty.VILLAGETALUKA, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VILLAGEDISTRICT", pClsProperty.VILLAGEDISTRICT, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VILLAGEADDRESS", pClsProperty.VILLAGEADDRESS, DbType.String, ParameterDirection.Input);

                Ope.AddParams("DIAMONDKNOWLEDGE", pClsProperty.DIAMONDKNOWLEDGE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SARINKNOWLEDGE", pClsProperty.SARINKNOWLEDGE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COMPUTERKNOWLEDGE", pClsProperty.COMPUTERKNOWLEDGE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LASERKNOWLEDGE", pClsProperty.LASERKNOWLEDGE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHAPEKNOWN", pClsProperty.SHAPEKNOWN, DbType.String, ParameterDirection.Input);

                Ope.AddParams("GENDER", pClsProperty.GENDER, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBCAST", pClsProperty.SUBCAST, DbType.String, ParameterDirection.Input);
                //End : On : 08-04-2019

                Ope.AddParams("AUTOCONFIRM", pClsProperty.AUTOCONFIRM, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISSALARYACCOUNTCLEAR", pClsProperty.ISSALARYACCOUNTCLEAR, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("EMPSHORTNAME", pClsProperty.EMPSHORTNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTYGROUP_ID", pClsProperty.PARTYGROUP_ID, DbType.Int32, ParameterDirection.Input); //K: 28/11/22
                Ope.AddParams("TABLE_ID", pClsProperty.TABLE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("TABLENAME", pClsProperty.TABLENAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("KAPANMAINMANAGER_ID", pClsProperty.KAPANMAINMANAGER_ID, DbType.Int64, ParameterDirection.Input);//Gunjan:27/03/2023

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString,Config.ProviderName, SProc.MST_LedgerSave, CommandType.StoredProcedure);

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


        public LedgerMasterProperty SaveLedgerDetailInfo(LedgerMasterProperty pClsProperty, DataTable DtExperience, DataTable DtFamily, DataTable DtReference, DataTable DtAttachment, DataTable DtProcessSetting,DataTable DtItemIssueDetail)
        {
            try
            {
                Ope.ClearParams();
                //Ope.AddParams("LEDGER_ID", pClsProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                //Ope.AddParams("EXPERIENCE_ID", pClsProperty.EXPERIENCE_ID, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EXPSRNO", pClsProperty.EXPSRNO, DbType.Int32, ParameterDirection.Input);
                //Ope.AddParams("EXPCOMPANYNAME", pClsProperty.EXPCOMPANYNAME, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("EXPMANAGERNAME", pClsProperty.EXPMANAGERNAME, DbType.String, ParameterDirection.Input);
                //Ope.AddParams("TOTEXPYEAR", pClsProperty.TOTEXPYEAR, DbType.Decimal, ParameterDirection.Input);
                //Ope.AddParams("EXPSALARYFROM", pClsProperty.EXPSALARYFROM, DbType.Decimal, ParameterDirection.Input);
                //Ope.AddParams("EXPSALARYTO", pClsProperty.EXPSALARYTO, DbType.Decimal, ParameterDirection.Input);
                //Ope.AddParams("LEAVECOMPANYREASON", pClsProperty.LEAVECOMPANYREASON, DbType.String, ParameterDirection.Input);

                Ope.AddParams("TBL_MST_LEDGEREXPERIENCEDETAILS", DtExperience, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("TBL_MST_LEDGERFAMILYDETAILS", DtFamily, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("TBL_MST_LEDGERREFERENCEDETAILS", DtReference, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("TBL_MST_LEDGERATTACHMENTDETAILS", DtAttachment, DbType.Object, ParameterDirection.Input);
                //Ope.AddParams("TBL_MST_LEDGERPROCESSSETTING", DtProcessSetting, DbType.Object, ParameterDirection.Input);
                Ope.AddParams("TBL_MST_LEDGERITEMISSUEDETAILS", DtItemIssueDetail, DbType.Object, ParameterDirection.Input);

                Ope.AddParams("LEDGER_ID", pClsProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_LedgerDetailInfoSave", CommandType.StoredProcedure);

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
        public int DeleteLedgerDetailInfo(string StrOpe, string StrID)
        {
            string StrQuery = "";

            if (StrOpe == "EXPERIENCEDETAIL")
            {
                StrQuery = "DELETE FROM MST_LEDGEREXPERIENCEDETAILS WITH(ROWLOCK) WHERE EXPERIENCE_ID = '" + StrID + "'"; 
            }
            else if (StrOpe == "FAMILYDETAIL")
            {
                StrQuery = "DELETE FROM MST_LEDGERFAMILYDETAILS WITH(ROWLOCK) WHERE FAMILY_ID = '" + StrID + "'";
            }
            else if (StrOpe == "REFERENCEDETAIL")
            {
                StrQuery = "DELETE FROM MST_LEDGERREFERENCEDETAILS WITH(ROWLOCK) WHERE REFERENCE_ID = '" + StrID + "'";
            }
            else if (StrOpe == "ATTACHMENTDETAIL")
            {
                StrQuery = "DELETE FROM MST_LEDGERATTACHMENTDETAILS WITH(ROWLOCK) WHERE ATTACHMENT_ID = '" + StrID + "'";
            }
            else if (StrOpe == "PROCESSSETTING")
            {
                StrQuery = "DELETE FROM MST_LEDGERPROCESSSETTING WITH(ROWLOCK) WHERE PROCESSSETTING_ID = '" + StrID + "'";
            }
            else if (StrOpe == "ITEMISSUEDETAIL")
            {
                StrQuery = "DELETE FROM MST_LEDGERITEMISSUEDETAILS WITH(ROWLOCK) WHERE ITEMISSUE_ID = '" + StrID + "'";
            }

           return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, StrQuery, CommandType.Text);
        }



        public DataRow CheckLogin(string pStrUserName,string pStrPassword)
        {
            Ope.ClearParams();
            Ope.AddParams("USERNAME", pStrUserName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PASSWORD", pStrPassword, DbType.String, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            DataRow DR = Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "CheckLogin", CommandType.StoredProcedure);

            return DR;

        }
        public DataTable ChangeUser(Int64 pIntEmployeeID)
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("EMPLOYEE_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "ChangeUser", CommandType.StoredProcedure);

            return DTab;

        }

        public string CheckLoginValidation(string pStrUserName, string pStrPassword,string pStrExeVersion)
        {
            Ope.ClearParams();
            Ope.AddParams("USERNAME", pStrUserName, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PASSWORD", pStrPassword, DbType.String, ParameterDirection.Input);

            Ope.AddParams("EXEVERSION", pStrExeVersion, DbType.String, ParameterDirection.Input); //#P : 07-02-2020

            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "CheckLoginValidation", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                return Val.ToString(AL[2]);
            }
            return "";
        }

        public LedgerMasterProperty Delete(LedgerMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LEDGER_ID", pClsProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.MST_LedgerDelete, CommandType.StoredProcedure);

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


        public LedgerMasterProperty UpdateDepartmentInAllTransaction(LedgerMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("LEDGER_ID", pClsProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_LedgerDepartmentUpdate", CommandType.StoredProcedure);

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


        public Int64 GetCompnayID(string pStrName)
        {
            string Str = Ope.FindText(Config.ConnectionString, Config.ProviderName, "MST_Ledger", "Ledger_ID", " And LedgerName = '"+pStrName+"' And LedgerGroup='Company'");
            return Val.ToInt64(Str);
        }


        public DataRow GetLedgerInfoByCode(string pStrGroup, string pStrCode)
        {
            Ope.ClearParams();
            Ope.AddParams("LEDGERGROUP", pStrGroup, DbType.String, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LEDGERCODE", pStrCode, DbType.String, ParameterDirection.Input);
            return Ope.GetDataRow(BusLib.Configuration.BOConfiguration.ConnectionString, BusLib.Configuration.BOConfiguration.ProviderName, "MST_LedgerGetData", CommandType.StoredProcedure);
        }

        public DataSet GetledgerDetailDInfoata(Int64 IntLedger_ID) //06-04-2019
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();

//          Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LEDGER_ID", IntLedger_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "MST_LedgerDetailInfoGetData", CommandType.StoredProcedure);

            return DS;
        }
        public DataTable GetDataForLedgerPrint(Int64 pIntLedger_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("LEDGER_ID", pIntLedger_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_Ledger_Print", CommandType.StoredProcedure);
            return DTab;
        }


        public string ChangePassWord(Int64 pLeger_Id, string pOldPassWord, string pNewPassWord)
        {
            Ope.ClearParams();
            Ope.AddParams("LEDGER_ID", pLeger_Id, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("OLDPASSWORD", pOldPassWord, DbType.String, ParameterDirection.Input);
            Ope.AddParams("NEWPASSWORD", pNewPassWord, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "ChangePassword", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                return Val.ToString(AL[2]);
            }
            return "";
        }

        public string SetExpGoal(string pStrGoalDate, Int32 pIntExpGoal)
        {
            Ope.ClearParams();
            Ope.AddParams("EMPLOYEE_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("GOALDATE", pIntExpGoal, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("GOALDATE", pStrGoalDate, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "MST_ExpGoalSave", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {

                return Val.ToString(AL[2]);
            }
            return "";
        }

        public DataTable CheckEvent(string strMacAddress)
        {
            DataTable Dtab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("MACADDRESS", strMacAddress, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, Dtab, "MST_AxoneEvents", CommandType.StoredProcedure);

            return Dtab;

        }


    }
}
