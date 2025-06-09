using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using AxonDataLib;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;
namespace BusLib
{
    public class BOFormPermission
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
               
        System.Windows.Forms.Form mForm;


        public bool ISView { get; set; }
        public bool ISInsert { get; set; }
        public bool ISUpdate { get; set; }
        public bool ISDelete { get; set; }
        public string Password { get; set; }
        
        public BOFormPermission()
        {
                
        }

        public void GetPermission(System.Windows.Forms.Form pForm)
        {
            mForm = pForm;

            try
            {
                Ope.ClearParams();

                Ope.AddParams("FormName", mForm.Tag.ToString(), DbType.String, ParameterDirection.Input);
                Ope.AddParams("FormCaption", mForm.Text, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("EntryBy", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("EntryIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                DataRow DR =  Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "MST_GetFormPermissionData", CommandType.StoredProcedure);

                ISView = false;
                ISInsert = false;
                ISUpdate = false;
                ISDelete = false;
                Password = string.Empty;
                
                if (DR != null)
                {
                    Password = Val.ToString(DR["Pass"]);
                    ISView = Val.ToBoolean(DR["ISView"]);
                    ISInsert = Val.ToBoolean(DR["ISInsert"]);
                    ISUpdate = Val.ToBoolean(DR["ISUpdate"]);
                    ISDelete = Val.ToBoolean(DR["ISDelete"]);                    
                }
            }
            catch (System.Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
            }     
        }

        public DataTable GetPermissionDataOfEmployee()
        {
            
            try
            {
                Ope.ClearParams();
                DataTable DTab = new DataTable();
                Ope.AddParams("LEDGER_ID", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab,"MST_GetFormPermissionEmployeeData", CommandType.StoredProcedure);
                return DTab;
            }
            catch (System.Exception ex)
            {
                //BusLib.BOException.Save(this.GetType().Name, new System.Diagnostics.StackFrame(1, true).GetMethod().ToString(), new System.Diagnostics.StackFrame(0, true).GetFileLineNumber(), ex.Message, ex.ToString());
            }
            return null;
        }

        public DataTable GetUserPermissionForVisibleMenu(int pIntEmployeeID)
        {

            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("LEDGER_ID", pIntEmployeeID, DbType.String, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "MST_EmployeePermissionGetData", CommandType.StoredProcedure);
            return DTab;
        }
     }

}
