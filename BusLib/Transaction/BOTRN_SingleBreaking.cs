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
    public class BOTRN_SingleBreaking
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Fill(string pStrFromDate, string pStrToDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);
            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_SingleBreakingGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public SingleBreakingProperty Save(SingleBreakingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("BREAKING_ID", pClsProperty.BREAKING_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("BREAKINGTYPE_ID", pClsProperty.BREAKINGTYPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BREAKINGDATE", pClsProperty.BREAKINGDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("KAPANNAME", pClsProperty.KAPANNAME, DbType.String, ParameterDirection.Input);

                Ope.AddParams("PACKET_ID", pClsProperty.PACKET_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("PACKETNO", pClsProperty.PACKETNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("TAG", pClsProperty.TAG, DbType.String, ParameterDirection.Input);

                Ope.AddParams("BFCARAT", pClsProperty.BFCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("BFSHAPE_ID", pClsProperty.BFSHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFCOLOR_ID", pClsProperty.BFCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFCLARITY_ID", pClsProperty.BFCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFCUT_ID", pClsProperty.BFCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFPOL_ID", pClsProperty.BFPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFSYM_ID", pClsProperty.BFSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFFL_ID", pClsProperty.BFFL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("BFRAPDATE", pClsProperty.BFRAPDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("BFRAPAPORT", pClsProperty.BFRAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("BFDISCOUNT", pClsProperty.BFDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("BFPRICEPERCARAT", pClsProperty.BFPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("BFAMOUNT", pClsProperty.BFAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("AFCARAT", pClsProperty.AFCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("AFSHAPE_ID", pClsProperty.AFSHAPE_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFCOLOR_ID", pClsProperty.AFCOLOR_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFCLARITY_ID", pClsProperty.AFCLARITY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFCUT_ID", pClsProperty.AFCUT_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFPOL_ID", pClsProperty.AFPOL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFSYM_ID", pClsProperty.AFSYM_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFFL_ID", pClsProperty.AFFL_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("AFRAPDATE", pClsProperty.AFRAPDATE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("AFRAPAPORT", pClsProperty.AFRAPAPORT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("AFDISCOUNT", pClsProperty.AFDISCOUNT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("AFPRICEPERCARAT", pClsProperty.AFPRICEPERCARAT, DbType.Decimal, ParameterDirection.Input);
                Ope.AddParams("AFAMOUNT", pClsProperty.AFAMOUNT, DbType.Decimal, ParameterDirection.Input);

                Ope.AddParams("EMPLOYEE_ID", pClsProperty.EMPLOYEE_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("PENALTYAMOUNT", pClsProperty.PENALTYAMOUNT, DbType.Decimal, ParameterDirection.Input);
              
                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleBreakingSave", CommandType.StoredProcedure);

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

        public SingleBreakingProperty Delete(SingleBreakingProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("BREAKING_ID", pClsProperty.BREAKING_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);
                
                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_SingleBreakingDelete" , CommandType.StoredProcedure);

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


