using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL = BusLib;
using AxonDataLib;

namespace BusLib
{
    public class BOComboFill
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        public enum TABLE
        {
            MST_COUNTRY = 0,
            MST_STATE = 1,
            MST_CITY = 2,

            MST_BUSINESSTYPE = 3,
            MST_LEDGERGROUP = 4,
            MST_ACCOUNTTYPE = 5,

            MST_DEPARTMENT = 6,
            MST_DESIGNATION = 7,
            MST_EMPLOYEE = 8,

            MST_SERVICETYPE = 9,
            MST_SERVICESUBTYPE = 10,

            MST_COMPANY = 11,
            MST_LEDGERCLIENT = 12,

            MST_EXPENSETYPE = 13,
            MST_PRICEGROUP = 14,
            MST_LEDGERBANK = 15,
            MST_LEDGERCASH = 16,

            MST_LEDGERGENERAL = 17,
            MST_LEDGERALL = 18,
            MST_LEDGERBANKCASH = 19,
            MST_LEDGERNONCASH = 20,
            MST_ITEM = 21,
            MST_LEDGERCOMPANY = 22,
            MST_LEDGERPURCHASE = 23,

            MST_LEDGERSALE = 24,
            MST_YEAR = 25,
            MST_ITEMGROUP = 26,
            MST_LEDGEREMPLOYEE = 27,

            MST_PROCESS = 28,

            MST_SHAPE = 29,
            MST_CLARITY = 30,
            MST_CUT = 31,
            MST_POL = 32,
            MST_SYM = 33,
            MST_FL = 34,
            MST_MILKY = 35,
            MST_LBLC = 36,
            MST_NATTS = 37,

            MST_CHARNI = 38,
            MST_LEDGERBROKER = 39,

            TRN_KAPANMIX = 40,

            TRN_IMPORT = 41,

            MST_REJECTION = 42,
            MST_FORMGROUP = 43,

            MST_COLOR = 44,

            RAPDATE = 45,

            MST_COLORSHADE = 46,

            MST_SOURCE = 47,
            MST_MSIZE = 48,
            MST_ARTICLE = 49,
            MST_TENSION = 50,
            TRN_KAPANSINGLE = 51,
            MST_EMPLOYEETRANSFERTO = 52,
            MST_SUBLOT = 53,
            MST_REASON = 54,
            MST_EMPLOYEEDISPLAY = 55,
            MST_PRDTYPE = 56,

            TRN_SINGLEFINDTAG = 57,
            MST_REPORT = 58,
            MST_EMPLOYEETRANSFERTO_NONMFG = 59,
            MST_PROCESSFINAL = 60,
            FILE_TRANSFER_TYPE = 61,
            MST_DOCUMENTTYPE = 62,
            MST_POLISHSIZE = 63,
            MST_EMPLOYEEWITHIMAGE = 64,
            MST_OTHERREASON = 65,
            MST_BREAKINGTYPE = 66,

            MST_CLARITYALL = 67,
            MST_SHAPEALL = 68,
            MST_COLORALL = 69,
            MST_BLACK = 72,
            MST_WHITE = 73,
            MST_OPEN = 74,
            MST_PAVALION = 75,
            MST_LUSTER = 76,
            MST_EYECLEAN = 77,
            MST_HA = 78,
            HEL_COLUMN = 79,
            MST_LEAVETYPE = 80,
            MST_GIASERVICE = 81,
            MST_ROUGHOTHEREXP = 82,
            MST_ROUGHCOMPANY = 83,
            MST_ROUGHADATPARTY = 84,
            MST_PRDTYPERAPMISTAKE = 85,
            MST_ROUGHNAME = 86,
            MST_MINES = 87,
            PRI_ORIGINAL_BACKSIZE = 88,
            MST_PACKETGROUP = 89,
            MST_LOCATION = 90,
            MST_MIXPACKET = 91,
            MST_MAINMANAGER = 92,
            TRN_INVOICE = 93,
            MST_ASSORTMENT = 94,
            MST_LABOURPROCESS = 95,
            MST_REQUIREDPROCESS = 96,
            MST_PACKETGRADE = 97,
            MST_BREAKINGREASON = 98,
            MST_DOLLARLABOURSIZE = 99,
            MST_4PLABOURPROCESS = 100,
            MST_DEPARTMENTSUBGROUP = 101,
            MST_DEPTJANGEDNO = 102,
            MST_TABLE = 103,
            MST_LEDGERCASHPAYMENT = 104,
            MST_CURRENCY = 105,
            MST_PARTY = 106,


            TRN_LOT = 107,
            MST_ROUGHTYPE = 108,
            MST_ROUGHDESC = 109,
            MST_REJECTREASON = 110,
            MST_JOBWORKLEDGER = 111,

            MST_PRICEHEAD = 112,//Gunjan:22/03/2023
            PARCEL_KAPANINWARD = 113,
            PARCEL_MIXSIZE = 114,
            PARCEL_KAPAN = 115,
            MST_DEPARTMENTMIX = 116,
            PARCEL_KAPANOUTWARD = 117,
            TRN_KAPANINWARD = 118,
            TRN_KAPAN = 119,
            MST_SIZE = 120,
            MST_PENLTYPOINT = 121,
            MST_MONTH = 122,
            MST_PACKETCATEGORY = 123,

            MST_PARAORDERPARTY = 124,
            MST_PARAORDERCOMMENT = 125,
            MST_POLISHJANGEDNO = 126,
            MST_DIAMONDTYPE = 127,
            MST_EMPLOYEEKAPAN = 128,
            MST_TAGSRNO = 129,
            MST_MANAGER = 130,
            MST_KAPANREMARK = 131,
            MST_PLANNINGGRADE = 132,
            MST_TABLEOPEN=133,
            MST_CROWNOPEN=134,
            MST_PAVILLIONOPEN=135,
            MST_LAB=136
        }

        public DataTable FillCmb(TABLE tenum)
        {
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", tenum.ToString());
            Ope.AddParams("EMPLOYEE_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            Ope.AddParams("DEPARTMENT_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.DEPARTMENT_ID);
            Ope.FillDTab(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, DTab, TPV.BOSProc.Fill_Combo, CommandType.StoredProcedure, "");
            return DTab;
        }

        public DataTable FillCombo(TABLE tenum, int PROCESS_ID)
        {
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", tenum.ToString());
            Ope.AddParams("PROCESS_ID", PROCESS_ID);
            Ope.AddParams("EMPLOYEE_ID", BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);
            Ope.FillDTab(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, DTab, TPV.BOSProc.Fill_Combo, CommandType.StoredProcedure, "");
            return DTab;
        }

        public DataTable FillCmb(TABLE tenum, string pStrDataTableName, string pStrRoughType)
        {
            DataTable DTab = new DataTable(pStrDataTableName);
            Ope.AddParams("OPE", tenum.ToString());
            Ope.FillDTab(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, DTab, TPV.BOSProc.Fill_Combo, CommandType.StoredProcedure, "");
            return DTab;
        }

        public DataTable FillCmbLedgerWithGroup(TABLE tenum, Int64 pIntLedgerGroup)
        {
            DataTable DTab = new DataTable();
            Ope.AddParams("OPE", tenum.ToString());
            Ope.AddParams("LEDGERGROUP_ID", pIntLedgerGroup);

            Ope.FillDTab(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, DTab, TPV.BOSProc.Fill_Combo, CommandType.StoredProcedure, "");
            return DTab;
        }

        public DataTable FillCmb(string tenum, Int64 pIntParentID)
        {
            DataTable DTab = new DataTable();

            Ope.AddParams("PARAMTYPE", tenum.ToString());
            Ope.AddParams("PARENTPARAM_ID", pIntParentID);

            Ope.FillDTab(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, DTab, TPV.BOSProc.Fill_ComboParam, CommandType.StoredProcedure, "");
            return DTab;
        }

    }
}
