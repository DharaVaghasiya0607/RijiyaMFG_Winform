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
    public class BOTRN_RoughCostAnalysis
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public DataSet GetData(RoughCostAnalysisProperty pClsProperty) 
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();

                Ope.AddParams("ReportType", pClsProperty.ReportType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("KapanName", pClsProperty.KapanName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RoughName", pClsProperty.RoughName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RoughType", pClsProperty.RoughType, DbType.String, ParameterDirection.Input);
                Ope.AddParams("CleaverName", pClsProperty.CleaverName, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RoughDescription", pClsProperty.RoughDescription, DbType.String, ParameterDirection.Input);
                Ope.AddParams("RoughStatus", pClsProperty.RoughStatus, DbType.String, ParameterDirection.Input);
                Ope.AddParams("FromPolDate", pClsProperty.FromPolDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ToPolDate", pClsProperty.ToPolDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("FromClvIssueDate", pClsProperty.FromClvIssueDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ToClvIssueDate", pClsProperty.ToClvIssueDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("SendFromDate", pClsProperty.SendFromDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("SendToDate", pClsProperty.SendToDate, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ISPendingToSend", pClsProperty.IsPending, DbType.Boolean, ParameterDirection.Input);

                Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "RP_RoughCostingReport", CommandType.StoredProcedure);
                return DS;
            }
            catch (Exception Ex)
            {

                return null;
            }
        }

        public RoughCostAnalysisProperty Update(RoughCostAnalysisProperty pClsProperty)
        {
            try
            {
                DataSet DS = new DataSet();

                Ope.ClearParams();
                Ope.AddParams("KAPAN_ID", pClsProperty.KAPAN_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("MFGSDATE", Val.SqlDate(pClsProperty.MFGSDATE), DbType.Date, ParameterDirection.Input);
                Ope.AddParams("POLRDATE", Val.SqlDate(pClsProperty.POLRDATE), DbType.Date, ParameterDirection.Input);
                Ope.AddParams("MUMBAIRECVDATE", Val.SqlDate(pClsProperty.MUMBAIRECVDATE), DbType.Date, ParameterDirection.Input);


                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);


                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughUpdate", CommandType.StoredProcedure);

                if (AL.Count != 0)
                {
                    pClsProperty.ReturnValue = Val.ToString(AL[0]);
                    pClsProperty.ReturnMessageType = Val.ToString(AL[1]);
                    pClsProperty.ReturnMessageDesc = Val.ToString(AL[2]);
                }
                return pClsProperty;
            }
            catch (Exception Ex)
            {
                return null;
            }

        }
    }


    public class RoughCostAnalysisProperty
    {
        public string ReportType { get; set; }
        public string KapanName { get; set; }
        public string RoughName { get; set; }
        public string RoughType { get; set; }
        public string CleaverName { get; set; }
        public string RoughDescription { get; set; }
        public string RoughStatus { get; set; }
        public string FromPolDate { get; set; }
        public string ToPolDate { get; set; }
        public string FromClvIssueDate { get; set; }
        public string ToClvIssueDate { get; set; }
        public string SendFromDate { get; set; }
        public string SendToDate { get; set; }
        public bool IsPending { get; set; }
        public string MFGSDATE { get; set; }
        public string POLRDATE { get; set; }
        public string MUMBAIRECVDATE { get; set; }
        public Int64 KAPAN_ID { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }
}

