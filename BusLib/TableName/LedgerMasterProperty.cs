using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class LedgerMasterProperty
    {
        public Int64 LEDGER_ID { get; set; }

        public string LEDGERCODE { get; set; }
        public string LEDGERNAME { get; set; }
        public string LEDGERNAMEGUJARATI { get; set; }
        public string LEDGERGROUP { get; set; }
        public string CONTACTPERSON { get; set; }

        public string SHAPE_ID { get; set; }

        public string MOBILENO1 { get; set; }
        public string MOBILENO2 { get; set; }
        
        public double  DEBITAMOUNT { get; set; }
        public string TRNTYPECREDIT { get; set; }
        
        public double CREDITAMOUNT { get; set; }
        public string TRNTYPEDEBIT { get; set; }

        public string BANKNAME { get; set; }
        public string BANKIFSCCODE { get; set; }
        public string BANKACCOUNTNAME { get; set; }
        public string BANKACCOUNTNO { get; set; }
        public string GSTNO { get; set; }
        public string CSTNO { get; set; }
        public string PANNO { get; set; }
        public string BILLINGADDRESS { get; set; }
        public string BILLINGSTATE { get; set; }
        public string SHIPPINGADDRESS { get; set; }
        public string SHIPPINGSTATE { get; set; }
        
        public string REMARK { get; set; }
        public string ADDRESS { get; set; }

        public bool ISACTIVE { get; set; }

        public double EXPSALARY	{ get; set; }
        public string USERNAME	{ get; set; }
        public string PASSWORD	{ get; set; }
        public string ADHARNO	{ get; set; }
        public string EMPLOYEETYPE	{ get; set; }
        
        public string IDCARDNO	{ get; set; }
        public string CONTACTPERSONMOBILENO	{ get; set; }
        public string PREVCOMPANYNAME	{ get; set; }
        public string PREVDESIGNATION	{ get; set; }
        public double PREVSALARY	{ get; set; }
        public double TOTALEXP { get; set; }
        public byte[] EMPPHOTO { get; set; }


        public Int32 PARTYGROUP_ID { get; set; }
        public Int64 TABLE_ID { get; set; }
        public string TABLENAME { get; set; }

        public string EXEVERSION { get; set; } //#P : 07-02-2020

        public string STYDY { get; set; }
        public string DOMAINKNOWLEDGE { get; set; }
        public double STUDYPER { get; set; }
        public string STUDYCOLLEGENAME { get; set; }
        public string LANGUAGEKNOWN { get; set; }
        public int AGE { get; set; }
        public string RELIGION { get; set; }
        public string CAST { get; set; }
        public string BLOODGROUP { get; set; }
        public string MARRIEDSTATUS { get; set; }
        public string VILLAGENAME { get; set; }
        public string VILLAGETALUKA { get; set; }
        public string VILLAGEDISTRICT { get; set; }
        public string VILLAGEADDRESS{ get; set; }
        public string DIAMONDKNOWLEDGE { get; set; }
        public string SARINKNOWLEDGE { get; set; }
        public string COMPUTERKNOWLEDGE { get; set; }
        public string LASERKNOWLEDGE { get; set; }
        public string SHAPEKNOWN { get; set; }

        public string GENDER{ get; set; }
        public string SUBCAST { get; set; }


        public Int32 DEPARTMENT_ID { get; set; }
        public string DEPARTMENTNAME { get; set; }

        public string DEPARTMENTGROUP { get; set; }

        public Int32 DESIGNATION_ID { get; set; }
        public string DESIGNATIONNAME { get; set; }

        public Int64 COMPANY_ID { get; set; }
        public string COMPANYNAME { get; set; }
       
        public Int64 MANAGER_ID { get; set; }
        public string MANAGERCODE { get; set; }
        public string MANAGERNAME { get; set; }
        public double SALARY { get; set; }
        public string SALARYTYPE { get; set; }

        public bool AUTOCONFIRM { get; set; }

        public Int64 LOGINHST_ID { get; set; } // #P : 16-10-2019

        public bool ISSALARYACCOUNTCLEAR { get; set; }

        public string BPRINTTYPE { get; set; }

        public string DATEOFJOIN { get; set; }
        public string DATEOFBIRTH { get; set; }
        public string DATEOFLEAVE { get; set; }
        public string LEAVEREASON { get; set; }
        public string CONTACTPERSONCODE { get; set; }
        public string CONTACTPERSONADDRESS { get; set; }

        public string EMPSHORTNAME { get; set; }


        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }



        public Int64 KAPANMAINMANAGER_ID { get; set; }
    }

}
