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

namespace BusLib.Transaction
{
    public class BOTRN_KapanValuation
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public String GetRoughName(int Lot_Id)
        {
            Ope.ClearParams();

            Ope.AddParams("MIXSTOCKTYPE", Lot_Id, DbType.Int32, ParameterDirection.Input);

            // return Ope.ToString(Config.ConnectionString, Config.ProviderName, "TRN_GetRoughName", CommandType.StoredProcedure);
            return null;
        }

        public KapanValuationProperty UpdateKapan(KapanValuationProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("GHATCOMPLETEDATE", pClsProperty.GHATCOMPLETEDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("MFGISSUEDATE", pClsProperty.MFGISSUEDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("COMPLETEDATE", pClsProperty.COMPLETEDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("MUMBAIRECVDATE", pClsProperty.MUMBAIRECVDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("CLVCOMPLETEDATE", pClsProperty.CLVCOMPLETEDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("POLISHRECVDATE", pClsProperty.POLISHRECVDATE, DbType.DateTime, ParameterDirection.Input);

                Ope.AddParams("LABRATE", pClsProperty.LABRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTHERMFGEXPENSE", pClsProperty.OTHERMFGEXP, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OTHEREXPENSEAMT", pClsProperty.OTHEREXPENSEAMT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHAVG", pClsProperty.POLISHAVG, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHCONVRATE", pClsProperty.POLISHCONVERSIONRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOTCONVRATE", pClsProperty.LOSTCONVERSIONRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("SARINLABOUR", pClsProperty.SARINLABOUR, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("QCLABOUR", pClsProperty.QCLABOUR, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GALAXYLABOUR", pClsProperty.GALAXYLABOUR, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GALAXYISSUEPC", pClsProperty.GALAXYISSUEPC, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("MKBLSIZE", pClsProperty.MKBLSIZE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKBLPER", pClsProperty.MKBLPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKBLCARAT", pClsProperty.MKBLCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MKBLPC", pClsProperty.MKBLPC, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("POLISHREADYCARAT", pClsProperty.POLISHREADYCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHREADYPCS", pClsProperty.POLISHREADYPCS, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("POLISHISSUECARAT", pClsProperty.POLISHISSUECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHISSUEPCS", pClsProperty.POLISHISSUEPCS, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("CLVWEIGHTLOSS", pClsProperty.CLVWEIGHTLOSS, DbType.Double, ParameterDirection.Input);
               
               

               //Mayank Start
                Ope.AddParams("KAPANCARAT", pClsProperty.KAPANCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANRATE", pClsProperty.KAPANRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("KAPANAMTRS", pClsProperty.KAPANAMTRS,DbType.Double,ParameterDirection.Input);
                Ope.AddParams("KAPANAMTDOLLAR", pClsProperty.KAPANAMTDOLLAR,DbType.Double,ParameterDirection.Input);
                Ope.AddParams("EXPPOLPER", pClsProperty.EXPPOLPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPPOLCARAT", pClsProperty.EXPPOLCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPMAKPER", pClsProperty.EXPMAKPER, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXPMAKCARAT", pClsProperty.EXPMAKCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("LOTREMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("EXPDOLLAR", pClsProperty.EXPDOLLAR, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("CLVISSUEDATE", pClsProperty.CLVISSUEDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("KACHAPCS", pClsProperty.KACHAPCS, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("KACHACARAT", pClsProperty.KACHACARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GHATRECIEVECARAT", pClsProperty.GHATRECIEVECARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PADTAR", pClsProperty.PADTAR, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PADTARAMT", pClsProperty.PADTARAMT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OUTCARAT", pClsProperty.OUTCARAT, DbType.Double, ParameterDirection.Input);                
                Ope.AddParams("OUTAMOUNT", pClsProperty.OUTAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OUTAVG", pClsProperty.OUTAVG, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OUTPCS", pClsProperty.OUTPCS, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("KAPANTOTALAMOUNT", pClsProperty.KAPANTOTALAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("POLISHAMOUNT", pClsProperty.POLISHAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("GALAXYAMOUNT", pClsProperty.GALAXYAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("MUMBAIAVG", pClsProperty.MUMBAIAVG, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MUMBAICNVRATE", pClsProperty.MUMBAICNVRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MUMBAIAMOUNT", pClsProperty.MUMBAIAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("PADTARAVG", pClsProperty.PADTARAVG, DbType.Double, ParameterDirection.Input);
                
                //Mayank End

               // Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("UPDATEBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("UPDATEIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_KapanUpdate", CommandType.StoredProcedure);
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
         public DataTable GetKapanPolishData(string pStrKapan_ID)
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pStrKapan_ID, DbType.String, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_KapanWiseValuationReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

         public DataSet GetKapanValudationExistingData(string pStrKapan_ID)
         {
             try
             {
                 DataSet DS = new DataSet();

                 Ope.ClearParams();
                 Ope.AddParams("KAPAN_ID", pStrKapan_ID, DbType.String, ParameterDirection.Input);
                 Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_KapanWiseValuationGetExistingData", CommandType.StoredProcedure);
                 return DS;
             }
             catch (Exception Ex)
             {

                 return null;
             }
         }
        
    }
}
