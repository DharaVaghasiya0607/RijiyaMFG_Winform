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
    public class BOTRN_SingleFileUpload
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataTable Save(DataTable DtFileUpload,Guid StrGroup_ID,string strUploadType,string pStrUploadDate)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("TBL_SingleFileUpload", DtFileUpload, DbType.Object, ParameterDirection.Input);
            Ope.AddParams("GROUP_ID", StrGroup_ID, DbType.Guid, ParameterDirection.Input);
            Ope.AddParams("UPLOADTYPE", strUploadType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("UPLOADDATE", pStrUploadDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("COMPANY_ID", Config.gEmployeeProperty.COMPANY_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleFileUploadInsUpd_UsingDataTable", CommandType.StoredProcedure);
            return DTab;
        }

        public SingleFileUploadProperty Delete(SingleFileUploadProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ATD_ID", pClsProperty.UPLOAD_ID, DbType.Guid, ParameterDirection.Input);
                Ope.AddParams("ATDDATE", pClsProperty.UPLOADDATE, DbType.Date, ParameterDirection.Input);
                //Ope.AddParams("DEPARTMENT_ID", pClsProperty.DEPARTMENT_ID, DbType.Int32, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, SProc.HR_AttendanceEntryDelete, CommandType.StoredProcedure);

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
        public DataTable GetFileUploadData(Guid GuidTrn_ID, string StrFromDate, string StrToDate ,string StrKapan ,string StrPacketNo ,string StrPacketTag,string StrLabType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("UPLOAD_ID", GuidTrn_ID, DbType.Guid, ParameterDirection.Input);

            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", StrPacketNo, DbType.String, ParameterDirection.Input);
            Ope.AddParams("TAG", StrPacketTag, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LABTYPE", StrLabType, DbType.String, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleFileUpload_GetData", CommandType.StoredProcedure);
            return DTab;
        }
        public DataTable GetFileUploadDataWithFilter(string StrFromDate,string StrToDate , string StrKapan , string StrPacket,string StrPacketTag,string StrLabType)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FROMDATE", StrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", StrToDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("KAPANNAME", StrKapan, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETNO", StrPacket, DbType.String, ParameterDirection.Input);
            Ope.AddParams("PACKETTAG", StrPacket, DbType.String, ParameterDirection.Input);
            Ope.AddParams("LABTYPE", StrLabType, DbType.String, ParameterDirection.Input);


            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "Trn_SingleFileUpload_GetData", CommandType.StoredProcedure);
            return DTab;
        }

    }
}
