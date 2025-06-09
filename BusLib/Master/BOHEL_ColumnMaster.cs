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
    public class BOHEL_ColumnMaster
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill()
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "HEL_ColumnMasterGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public HelColumnMasterProperty Save(HelColumnMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ID", pClsProperty.ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("COLNAME", pClsProperty.COLNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("DATATYPE", pClsProperty.DATATYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ISACTIVE", pClsProperty.ISACTIVE, DbType.Boolean, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "HEL_ColumnMasterSave", CommandType.StoredProcedure);

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

        public DataTable HeliumData(string wORD_STR)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            string StrQuery = "select ID from Trn_HeliumList where ID != '' AND ID IN(" + wORD_STR + ")";
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, StrQuery, CommandType.Text);
            return DTab;            
        }

        public int SavetxtHeliumFile(StringBuilder st)
        {
            int i = 0;           
            Ope.ClearParams();
            i = Ope.ExeNonQuery(BusLib.Configuration.BOConfiguration.ConnectionString, BusLib.Configuration.BOConfiguration.ProviderName, st.ToString(), CommandType.Text);            
            return i;
        }

        public DataTable MumbaiTransferGetData(string StrStatus, string StrJangedNo)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("STATUS", StrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("JangedNo", StrJangedNo, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_MumbaiTransferGetData_New", CommandType.StoredProcedure);
            //Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_MumbaiTransferGetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable HeliumMumbaiGradingGetData(string Helium_ID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.ClearParams();
            Ope.AddParams("H_ID", Helium_ID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_HeliumMumbaiGradingGetData", CommandType.StoredProcedure);
            return DTab;
        }


        public HelColumnMasterProperty MumbaiTransfer(HelColumnMasterProperty pClsProperty, string StrXmlForBombayTransfer)
        {
            try
            {
                Ope.ClearParams();
                //Ope.AddParams("HELIUM_ID", strHelium_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLFORBOMBAYTRANSFER", StrXmlForBombayTransfer, DbType.Xml, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName,"TRN_MumbaiTransfer_Insert", CommandType.StoredProcedure);

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

        public HelColumnMasterProperty DeleteformMumbaiTransfer(HelColumnMasterProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("PACKET_ID", pClsProperty.Helium_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_MumbaiTransferDelete", CommandType.StoredProcedure);

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
