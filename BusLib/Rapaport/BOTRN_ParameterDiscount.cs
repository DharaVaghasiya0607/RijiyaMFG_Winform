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
    public class BOTRN_ParameterDiscount
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetParameterDiscountData(string pStrOpe, string pStrParameterID, string pStrRapDate, string pStrShape, double pDouFromCarat, double pDouToCarat)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PARAMETERTYPE", pStrParameterID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHAPE", pStrShape, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RAPDATE", Val.SqlDate(pStrRapDate), DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FROMCARAT", pDouFromCarat, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("TOCARAT", pDouToCarat, DbType.Double, ParameterDirection.Input);
           // Ope.AddParams("DISPLAYTYPE", pStrDisplayType, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Pri_ParameterDiscountGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public ParameterDiscountProperty SaveParameterDiscount(ParameterDiscountProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("SHAPECODE", pClsProperty.SHAPECODE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("COLORCODE", pClsProperty.COLORCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLORNAME", pClsProperty.COLORNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("QCODE", pClsProperty.QCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("QNAME", pClsProperty.QNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.DateTime, ParameterDirection.Input);

                Ope.AddParams("PARAMETERTYPE", pClsProperty.PARAMETERTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARAMETERVALUE", pClsProperty.PARAMETERVALUE, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("OLDVALUE", pClsProperty.OLDVALUE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("NEWVALUE", pClsProperty.NEWVALUE, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_ParameterDiscountSave", CommandType.StoredProcedure);

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


        public ParameterDiscountProperty SaveParameterDiscountUsingXml(ParameterDiscountProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                
                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.DateTime, ParameterDirection.Input);
                Ope.AddParams("PARAMETERTYPE", pClsProperty.PARAMETERTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("xmlDisc", pClsProperty.XML, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_ParameterDiscountSaveXML", CommandType.StoredProcedure);

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


        public DataTable GetPatameterDiscountHistory(ParameterDiscountProperty pClsProperty)
        {
            DataTable DTab = new DataTable();

            Ope.ClearParams();
            Ope.AddParams("OPE", pClsProperty.OPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("F_CARAT", pClsProperty.FROMCARAT, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("T_CARAT", pClsProperty.TOCARAT, DbType.Double, ParameterDirection.Input);

            Ope.AddParams("S_CODE", pClsProperty.SHAPECODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("FC_CODE", pClsProperty.FC_CODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("FQ_CODE", pClsProperty.FQ_CODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("TC_CODE", pClsProperty.TC_CODE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TQ_CODE", pClsProperty.TQ_CODE, DbType.String, ParameterDirection.Input);

            Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.DateTime, ParameterDirection.Input);
            Ope.AddParams("PARAMETERTYPE", pClsProperty.PARAMETERTYPE, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PARAMETERVALUE", pClsProperty.PARAMETERVALUE, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Pri_ParameterDiscountGetHistory", CommandType.StoredProcedure);
            return DTab;

        }

        public DataTable GetOriginalRapData(string pStrOpe, string pStrRapDate, string pStrShape, double pDouFromCarat, double pDouToCarat)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PARAMETERTYPE", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("SHAPE", pStrShape, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RAPDATE", Val.SqlDate(pStrRapDate), DbType.Date, ParameterDirection.Input);
            Ope.AddParams("F_CARAT", pDouFromCarat, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("T_CARAT", pDouToCarat, DbType.Double, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Pri_OrigionalRapRateGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public ParameterDiscountProperty UpdateRapnetWithAllDiscount(ParameterDiscountProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();

            Ope.AddParams("tbl_Round", pClsProperty.RoundXml, DbType.Xml, ParameterDirection.Input);
            Ope.AddParams("tbl_Pear", pClsProperty.PearXml, DbType.Xml, ParameterDirection.Input);

            Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);

            Ope.AddParams("ENTRYBY", Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Configuration.BOConfiguration.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
            Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_UpdateRapnetWithAllDiscount", CommandType.StoredProcedure);

            if (AL.Count != 0)
            {
                pClsProperty.ReturnValue = Val.ToString(AL[0]);
                pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
            }
            return pClsProperty;
        }

        #region Price Check

        public int PriceCheck_SaveAllCombination(string pStrRapDate)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("RAPDATE", pStrRapDate, DbType.Date, ParameterDirection.Input);
                return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Rap_PriceCheckGetAllCombination", CommandType.StoredProcedure);

            }
            catch (Exception ex)
            {
                return -1;
            }

        }

        public DataRow PriceCheck_GetPendingCount(string pStrRapDate)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("RAPDATE", pStrRapDate, DbType.Date, ParameterDirection.Input);
                return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Rap_PriceCheckGetPendingProcess", CommandType.StoredProcedure);
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public DataTable PriceCheck_GetProblemData(
            string pStrRapDate,
            string pStrShape,
            string pStrColor,
            string pStrClarity,
            string pStrCut,
            string pStrPol,
            string pStrSym,
            string pStrFL,
            string pStrSize
            )
        {
            try
            {
                DataTable DTab = new DataTable();

                Ope.ClearParams();
                Ope.AddParams("RAPDATE", pStrRapDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("SHAPE", pStrShape, DbType.String, ParameterDirection.Input);
                Ope.AddParams("COLOR", pStrColor, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CLARITY", pStrClarity, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CUT", pStrCut, DbType.String, ParameterDirection.Input);
                Ope.AddParams("POL", pStrPol, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SYM", pStrSym, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FL", pStrFL, DbType.String, ParameterDirection.Input);
                Ope.AddParams("SIZE", pStrSize, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Rap_PriceCheckGetProblemData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public DataSet PriceCheck_GetParameter()
        {
            try
            {
                DataSet DS = new DataSet();
                Ope.ClearParams();
                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "Rap_PriceCheckGetAllParameter", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception ex)
            {
                return null;
            }

        }



        #endregion

        #region Extra Labour


        public LabExpenseMasterProperty ExtraLabourSave(LabExpenseMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("PARAMETERTYPE", pClsProperty.PARAMETERTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARAMETERVALUE", pClsProperty.PARAMETERVALUE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RAPDATE", pClsProperty.RAPDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FROMCARATOLD", pClsProperty.FROMCARATOLD, DbType.String, ParameterDirection.Input);
                Ope.AddParams("TOCARATOLD", pClsProperty.TOCARATOLD, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RATE", pClsProperty.RATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "PRI_ExtraLabourSave", CommandType.StoredProcedure);

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

        public DataTable ExtraLabourGetData(string pStrRapDate,string pStrParameterID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("PARAMETERTYPE", pStrParameterID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("RAPDATE", Val.SqlDate(pStrRapDate), DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "PRI_ExtraLabourGetData", CommandType.StoredProcedure);
            return DTab;
        }

        #endregion


        #region Rapaport Criteria


        public DataTable RapaportCriteriaGetData(int pIntShapeID, int pIntClarityID, double pDouFromCarat,double pDouToCarat)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("SHAPE_ID", pIntShapeID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CLARITY_ID", pIntClarityID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("FROMCARAT", pDouFromCarat, DbType.Double, ParameterDirection.Input);
            Ope.AddParams("TOCARAT", pDouToCarat, DbType.Double, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Pri_RapaportCriteriaGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public RapaportCriteriaProperty RapaportCriteriaSave(RapaportCriteriaProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("FROMCARAT", pClsProperty.FROMCARAT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("TOCARAT", pClsProperty.TOCARAT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("CLARITY_ID", pClsProperty.CLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLORCODE", pClsProperty.COLORCODE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARAMETERTYPE", pClsProperty.PARAMETERTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARAMETERVALUECODE", pClsProperty.PARAMETERVALUECODE, DbType.String, ParameterDirection.Input);
               
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Pri_RapaportCriteriaSave", CommandType.StoredProcedure);

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

        #endregion
    }
}
