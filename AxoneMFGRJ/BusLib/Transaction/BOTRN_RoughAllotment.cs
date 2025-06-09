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

namespace BusLib.Sale
{
    public class BOTRN_RoughAllotment
    {
        AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();


        public RoughAllotmentProperty Save(RoughAllotmentProperty pClsProperty)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ALLOTMENT_ID", pClsProperty.ALLOTMENT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ALLOTMENTDATE", pClsProperty.ALLOTMENTDATE, DbType.Date, ParameterDirection.Input);

                Ope.AddParams("INVOICE_ID", pClsProperty.INVOICE_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("LOT_ID", pClsProperty.LOT_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("ROUGHWEIGHT", pClsProperty.ROUGHWEIGHT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("PARTY_ID", pClsProperty.PARTY_ID, DbType.Int64, ParameterDirection.Input);

                Ope.AddParams("BROKER_ID", pClsProperty.BROKER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("BORKERAGEPER", pClsProperty.BORKERAGEPER, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("EXTRAEXP", pClsProperty.EXTRAEXP, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("EXCRATE", pClsProperty.EXCRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ROUGHOUTRATE", pClsProperty.ROUGHOUTRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FROUGHOUTRATE", pClsProperty.FROUGHOUTRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("NETAMOUNT", pClsProperty.NETAMOUNT, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FNETAMOUNT", pClsProperty.FNETAMOUNT, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("OUTROUGHCTS", pClsProperty.OUTROUGHCTS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("OUTRATE", pClsProperty.OUTRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FOUTRATE", pClsProperty.FOUTRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ASSORTMENTCTS", pClsProperty.ASSORTMENTCTS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ASSORTMENTRATE", pClsProperty.ASSORTMENTRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FASSORTMENTRATE", pClsProperty.FASSORTMENTRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("ASSORTMENTBALANCECTS", pClsProperty.ASSORTMENTBALANCECTS, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("MFGCTS", pClsProperty.MFGCTS, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MFGRATE", pClsProperty.MFGRATE, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FMFGRATE", pClsProperty.FMFGRATE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("REMARK", pClsProperty.REMARK, DbType.String, ParameterDirection.Input);

                Ope.AddParams("XMLFORROUGHDETAIL", pClsProperty.XMLFORROUGHDETAIL, DbType.Xml, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "Trn_RoughAllotmentMasterDetailSave", CommandType.StoredProcedure);

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

        public RoughAllotmentProperty Delete(RoughAllotmentProperty pClsProperty, double pDouAssortmentBalance, double pMFGRate, double pMFGRateFE)
        {
            try
            {
                Ope.ClearParams();

                Ope.AddParams("ALLOTMENT_ID", pClsProperty.ALLOTMENT_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ALLOTMENTDETAIL_ID", pClsProperty.ALLOTMENTDETAIL_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ASSORTMENTBALANCE",pDouAssortmentBalance, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("MFGRATE", pMFGRate, DbType.Double, ParameterDirection.Input);
                Ope.AddParams("FMFGRATE", pMFGRateFE, DbType.Double, ParameterDirection.Input);

                Ope.AddParams("ENTRYBY", Config.gEmployeeProperty.LEDGER_ID, DbType.Int64, ParameterDirection.Input);
                Ope.AddParams("ENTRYIP", Config.ComputerIP, DbType.String, ParameterDirection.Input);

                Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageType", "", DbType.String, ParameterDirection.Output);
                Ope.AddParams("ReturnMessageDesc", "", DbType.String, ParameterDirection.Output);

                ArrayList AL = Ope.ExeNonQueryWithOutParameter(Config.ConnectionString, Config.ProviderName, "TRN_RoughAllotmentMasterDetailDelete", CommandType.StoredProcedure);

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

        //public DataSet GetAllotmentDetail(Int64 pIntAllotment_ID, Int64 pIntInvoice_ID, string pStrFromDate = "", string pStrToDate = "")
        //{
        //    Ope.ClearParams();
        //    DataSet DS = new DataSet();
        //    Ope.AddParams("ALLOTMENT_ID", pIntAllotment_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("INVOICE_ID", pIntInvoice_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
        //    Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

        //    Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "TRN_RoughAllotmentMasterDetailGetData", CommandType.StoredProcedure);
        //    return DS;
        //}
        public DataSet GetAllotmentDetail(Int64 pIntAllotment_ID, Int64 pIntLot_ID, string pStrFromDate = "", string pStrToDate = "")
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("ALLOTMENT_ID", pIntAllotment_ID, DbType.Int64, ParameterDirection.Input);
            Ope.AddParams("LOT_ID", pIntLot_ID, DbType.Int64, ParameterDirection.Input); // K: 30/11/22
            Ope.AddParams("FROMDATE", pStrFromDate, DbType.Date, ParameterDirection.Input);
            Ope.AddParams("TODATE", pStrToDate, DbType.Date, ParameterDirection.Input);

            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "TRN_RoughAllotmentMasterDetailGetData", CommandType.StoredProcedure);
            return DS;
        }

        //public DataSet GetInvoiceDetail(Int64 pIntInvoice_ID)
        //{
        //    Ope.ClearParams();
        //    DataSet DS = new DataSet();
        //    Ope.AddParams("INVOICE_ID", pIntInvoice_ID, DbType.Int64, ParameterDirection.Input);
        //    Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "TRN_RoughAllotmentDetailGetData", CommandType.StoredProcedure);
        //    return DS;
        //}

        public DataSet GetInvoiceDetail(Int64 pIntLot_ID)
        {
            Ope.ClearParams();
            DataSet DS = new DataSet();
            Ope.AddParams("LOT_ID", pIntLot_ID, DbType.Int64, ParameterDirection.Input);
            Ope.FillDSet(Config.ConnectionString, Config.ProviderName, DS, "Temp", "TRN_RoughAllotmentDetailGetData", CommandType.StoredProcedure);
            return DS;
        }
    }
}
