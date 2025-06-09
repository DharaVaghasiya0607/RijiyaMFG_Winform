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

namespace BusLib.Rapaport
{
    public class BOTRN_SingleRepairingLabour
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrFromDate, string pStrToDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleRepairingLabourGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public SingleRepairingLabourProperty Save(SingleRepairingLabourProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPLABOUR_ID", pClsProperty.REPLABOUR_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("REPDATE", pClsProperty.REPDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RCOLOR_ID", pClsProperty.RCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RCLARITY_ID", pClsProperty.RCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RCUT_ID", pClsProperty.RCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RPOL_ID", pClsProperty.RPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RSYM_ID", pClsProperty.RSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("RFL_ID", pClsProperty.RFL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("RPRICEPERCARAT", pClsProperty.RPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("RAMOUNT", pClsProperty.RAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("POLISHCARAT", pClsProperty.POLISHCARAT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("PCOLOR_ID", pClsProperty.PCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PCLARITY_ID", pClsProperty.PCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PCUT_ID", pClsProperty.PCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PPOL_ID", pClsProperty.PPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PSYM_ID", pClsProperty.PSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PFL_ID", pClsProperty.PFL_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("PPRICEPERCARAT", pClsProperty.PPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("PAMOUNT", pClsProperty.PAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("LABOURRATE", pClsProperty.LABOURRATE, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("LABOURAMOUNT", pClsProperty.LABOURAMOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("LABOURPER", pClsProperty.LABOURPER, DbType.Decimal, ParameterDirection.Input);
              
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleRepairingLabourSave", CommandType.StoredProcedure);

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

        public SingleRepairingLabourProperty Delete(SingleRepairingLabourProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("REPLABOUR_ID", pClsProperty.REPLABOUR_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleRepairingLabourDelete" , CommandType.StoredProcedure);

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
        public DataTable FindLabourDetail(SingleRepairingLabourProperty pClsProperty)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("TRANSDATE", pClsProperty.REPDATE, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("SHAPE_ID", pClsProperty.SHAPE_ID , DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("COLOR_ID", pClsProperty.PCOLOR_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CLARITY_ID", pClsProperty.PCLARITY_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("CUT_ID", pClsProperty.PCUT_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("POL_ID", pClsProperty.PPOL_ID, DbType.Int32, ParameterDirection.Input);
            Ope.AddParams("SYM_ID", pClsProperty.PSYM_ID, DbType.Int32, ParameterDirection.Input);

            Ope.AddParams("ISSUECARAT", pClsProperty.ISSUECARAT, DbType.Decimal, ParameterDirection.Input);
            Ope.AddParams("READYCARAT", pClsProperty.POLISHCARAT, DbType.Decimal, ParameterDirection.Input);
            Ope.AddParams("PPRICEPERCARAT", pClsProperty.PPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);

            Ope.AddParams("LABOURPER", pClsProperty.LABOURPER, DbType.Decimal, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleRepairingLabourCalculate", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable RepairingExcelExportDetailGetData(string pStrRepairingExcelDataXml) //#P : 22-10-2020
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("XMLFORREPAIRINGEXCELDETAIL", pStrRepairingExcelDataXml, DbType.Xml, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleRepairingLabourExcelGetData", CommandType.StoredProcedure);
            return DTab;
        }

    }
}


