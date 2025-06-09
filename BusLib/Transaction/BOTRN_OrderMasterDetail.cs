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
    public class BOTRN_OrderMasterDetail
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        public TrnOrderEntryProperty OrderSave(TrnOrderEntryProperty pClsProperty, string pStrRoughDetailXml)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("ORDER_ID", pClsProperty.ORDER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ORDERDATE", pClsProperty.ORDERDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ORDERYEAR", pClsProperty.ORDERYEAR, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("ORDERMONTH", pClsProperty.ORDERMONTH, DbType.Int16, ParameterDirection.Input);
                Ope.AddParams("ORDERNO", pClsProperty.ORDERNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("SYSTEMORDERNO", pClsProperty.SYSTEMORDERNO, DbType.String, ParameterDirection.Input);

                Ope.AddParams("MANUALORDERNO", pClsProperty.MANUALORDERNO, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ROUGHTYPE", pClsProperty.ROUGHTYPE, DbType.String, ParameterDirection.Input);
                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PARTYNAME", pClsProperty.PARTYNAME, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ORDERDUE", pClsProperty.ORDERDUE, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("ORDERDUEDATE", pClsProperty.ORDERDUEDATE, DbType.Date, ParameterDirection.Input);
                Ope.AddParams("ORDERPRIORITY", pClsProperty.ORDERPRIORITY, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ORDERSEQNO", pClsProperty.ORDERSEQNO, DbType.Int32, ParameterDirection.Input);
                Ope.AddParams("PARTYREMARK", pClsProperty.PARTYREMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("YOURREMARK", pClsProperty.YOURREMARK, DbType.String, ParameterDirection.Input);
                Ope.AddParams("XMLDETAIL", pClsProperty.XMLDETAIL, DbType.Xml, ParameterDirection.Input);
                
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_OrderMasterDetailSave", CommandType.StoredProcedure);
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


        public DataTable GetDataForOrder(string pStrFromOrderDate,string pStrToOrderDate,
            string pStrFromDueDate,string pStrToDueDate,
            string pStrPartyID,string pStrStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("FromOrderDate", pStrFromOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToOrderDate", pStrToOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FromDueDate", pStrFromDueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToDueDate", pStrToDueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("Party_ID", pStrPartyID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Status", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Order_ID", 0, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_OrderMasterDetailGetData", CommandType.StoredProcedure);
            return DTab;
        }

        public DataSet GetOrderDetail(Int64 pIntOrderID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("Order_ID", pIntOrderID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "TRN_OrderMasterDetailGetData", CommandType.StoredProcedure);
            return DS;
        }



        public DataRow FindNewOrderNo(string pStrOrderDate, string pStrRoughType)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("ORDERDATE", pStrOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ROUGHTYPE", pStrRoughType, DbType.String, ParameterDirection.Input);

            return Ope.GetDataRow(Config.ConnectionString, Config.ProviderName, "Trn_OrderMasterDetailFindInvoiceNo", CommandType.StoredProcedure);
        }

        public TrnOrderEntryProperty OrderDelete(string pStrOpe, TrnOrderEntryProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();
                Ope.AddParams("OPE", pStrOpe, DbType.String, ParameterDirection.Input);
                Ope.AddParams("ORDER_ID", pClsProperty.ORDER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ORDERDETAIL_ID", pClsProperty.ORDERDETAIL_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_OrderMasterDetailDelete", CommandType.StoredProcedure);

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
        public bool ISExistsSystemOrderNo(string pStrSystemInvoiceNo, string pRoughType)
        {

            string S = Ope.FindText(Config.ConnectionString, Config.ProviderName, "TRN_Order", "SystemOrderNo", " And SystemOrderNo='" + pStrSystemInvoiceNo + "' AND RoughType = '" + pRoughType + "'  ");
            if (S.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public DataTable GetDashboardSummary(
            
            string pStrFromOrderDate, string pStrToOrderDate,
           string pStrFromDueDate, string pStrToDueDate,
           string pStrPartyID, string pStrStatus)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("Ope", "SUMMARY", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PrdType", "", DbType.String, ParameterDirection.Input);
            Ope.AddParams("FromOrderDate", pStrFromOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToOrderDate", pStrToOrderDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("FromDueDate", pStrFromDueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("ToDueDate", pStrToDueDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("Party_ID", pStrPartyID, DbType.String, ParameterDirection.Input);
            Ope.AddParams("Status", pStrStatus, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OrderDetail_ID", 0, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_OrderDashboard", CommandType.StoredProcedure);
            return DTab;
        }

        public DataTable GetDashboardDetail(string pStrPrdType, string pStrOrderDetailID)
        {
            Ope.ClearParams();
            DataTable DTab = new DataTable();
            Ope.AddParams("Ope", "DETAIL", DbType.String, ParameterDirection.Input);
            Ope.AddParams("PrdType", pStrPrdType, DbType.String, ParameterDirection.Input);
            Ope.AddParams("OrderDetail_ID", pStrOrderDetailID, DbType.Int64, ParameterDirection.Input);

            Ope.FillDTab(Config.ConnectionString, Config.ProviderName, DTab, "TRN_OrderDashboard", CommandType.StoredProcedure);
            return DTab;
        }

        public int RestAllFlag()
        {
            Ope.ClearParams();
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "TRN_OrderDashboardResetAllField", CommandType.StoredProcedure);
        }

        public int UpdateAllOrders()
        {
            Ope.ClearParams();
            return Ope.ExeNonQuery(Config.ConnectionString, Config.ProviderName, "Job_AutoUpdateOrder", CommandType.StoredProcedure);
        }

    }
}
