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
    public class BOMST_Parameter
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrGroup)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("PARATYPE", pStrGroup, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, SProc.MST_ParameterGetData, CommandType.StoredProcedure);
            return DTab;
        }

        public ParameterMasterProperty Save(ParameterMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PARA_ID", pClsProperty.PARA_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PARACODE", pClsProperty.PARACODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARANAME", pClsProperty.PARANAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SHORTNAME", pClsProperty.SHORTNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PROCESSGROUP", pClsProperty.PROCESSGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("POPUPPROCESS_ID", pClsProperty.POPUPPROCESS_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PARATYPE", pClsProperty.PARATYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SEQUENCENO", pClsProperty.SEQUENCENO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("REQPRDTYPE_ID", pClsProperty.REQPRDTYPE_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOCKAMTPRDTYPE_ID", pClsProperty.LOCKAMTPRDTYPE_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LABCODE", pClsProperty.LABCODE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISFINALISSUE", pClsProperty.ISFINALISSUE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);
                Ope.AddParams("ISCOMMANJANGED", pClsProperty.ISCOMMANJANGED, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("DUEHOURS", pClsProperty.DUEHOURS, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("LOSSPER", pClsProperty.LOSSPER, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("UPPERPARA_ID", pClsProperty.UPPERPARA_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("NUMBEROFISSUE", pClsProperty.NUMBEROFISSUE, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("KAPANROLLINGGROUP", pClsProperty.KAPANROLLINGGROUP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISDISPLAYONRETURN", pClsProperty.ISDISPLAYONRETURN, DbType.String, ParameterDirection.Input);

                Ope.AddParams("LOCATION_ID", pClsProperty.LOCATION_ID, DbType.String, ParameterDirection.Input);

                Ope.AddParams("KAPANRUNNINGGROUP", pClsProperty.KAPANRUNNINGGROUP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISLOSSDPT", pClsProperty.ISLOSSDPT, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("KAPANFINALREPORTGRP", pClsProperty.KAPANFINALREPORTGRP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SUBGROUPNAME", pClsProperty.SUBGROUP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("UPLOADFILENAME", pClsProperty.UPLOADFILENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("UPLOADSERVERPATH", pClsProperty.UPLOADSERVERPATH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("UPLOADSERVERUSERNAME", pClsProperty.UPLOADSERVERUSERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("UPLOADSERVERPASSWORD", pClsProperty.UPLOADSERVERPASSWORD, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DOWNLOADFILENAME", pClsProperty.DOWNLOADFILENAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DOWNLOADSERVERPATH", pClsProperty.DOWNLOADSERVERPATH, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DOWNLOADSERVERUSERNAME", pClsProperty.DOWNLOADSERVERUSERNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DOWNLOADSERVERPASSWORD", pClsProperty.DOWNLOADSERVERPASSWORD, DbType.String, ParameterDirection.Input);
                Ope.AddParams("LABOURPCS", pClsProperty.LABOURPCS, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("CLARITYWISEDEPARTMENT_ID", pClsProperty.CLARITYWISEDEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);//GUNJAN : 25/03/2023

                Ope.AddParams("ROUGHTYPE", pClsProperty.ROUGHTYPE, DbType.String, ParameterDirection.Input);//GUNJAN : 25/03/2023
                Ope.AddParams("ISMAKABLECOSNSIDER", pClsProperty.ISMAKABLECONSIDER, DbType.Boolean, ParameterDirection.Input);//GUNJAN : 28/11/023

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString,Config.ProviderName, SProc.MST_ParameterSave, CommandType.StoredProcedure);

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

        public ParameterMasterProperty Delete(ParameterMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PARA_ID", pClsProperty.PARA_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.MST_ParameterDelete, CommandType.StoredProcedure);

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
        public DataTable GetParameterData()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            string StrQuery = "SELECT ISNULL(PARA_ID,0) AS PARA_ID,ISNULL(PARACODE,'') AS PARACODE ,ISNULL(PARANAME,'') AS PARANAME , ISNULL(REMARK,'') AS REMARK,ISNULL(LABCODE,'') AS LABCODE, PARATYPE FROM MST_PARA WITH(NOLOCK) WHERE 1=1 ";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);
            return DTab;
        }

    }
}
