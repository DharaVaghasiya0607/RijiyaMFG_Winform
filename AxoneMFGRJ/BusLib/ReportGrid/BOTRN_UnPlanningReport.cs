using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System;
using System.Data;
using AxonDataLib;
using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using BusLib.TableName;
using System.Collections;
using System.IO;


namespace BusLib.View
{
    public class BOTRN_UnPlanningReport
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable GetStockUnPlanningReport(string pStrOpe, string pStrKapanName, string pStrMarker_ID, string pStrMainManager_ID, string pStrViewType,string pStrPcsType, string pStrPacketCategory, string pStrPacketGroup)
        {
            try
            {
                DataTable DTab = new DataTable();
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pStrKapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MARKER_ID", pStrMarker_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("MAINMANAGER_ID", pStrMainManager_ID, DbType.String, ParameterDirection.Input);
                Ope.AddParams("VIEWTYPE", pStrViewType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PCSTYPE", pStrPcsType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETCATEGORY", pStrPacketCategory, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PACKETGROUP", pStrPacketGroup, DbType.String, ParameterDirection.Input);

                Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "RP_UnPlanningReport", CommandType.StoredProcedure);
                return DTab;
            }
            catch (Exception Ex)
            {
                return null;
            }
        }
    }
}
