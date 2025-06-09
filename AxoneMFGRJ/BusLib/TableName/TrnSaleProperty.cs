using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnSaleProperty
    {
        public Int64 INVOICE_ID { get; set; }

        public Int64 SALE_ID { get; set; }
        public Int64 LOT_ID { get; set; }
        public Int64 REJECTION_ID { get; set; }
        public Int64 COMPANY_ID { get; set; }
        public string INVOICEDATE { get; set; }
        public Int16 INVOICEYEAR { get; set; }
        public Int16 INVOICEMONTH { get; set; }
        public Int32 INVOICENO { get; set; }
        public string SYSTEMINVOICENO { get; set; }
        public string MANUALINVOICENO { get; set; }
        public Int64 PARTY_ID { get; set; }
        public Int32 TERMSDAYS { get; set; }
        public string PAYMENTDATE { get; set; }
        public string OTHERCOMPANY { get; set; }
        public double EXCRATE { get; set; }
        public string SALETYPE { get; set; }
        public string CURRENCYTYPE { get; set; }
        public string PAYMENTTYPE { get; set; }
        public Int32 TOTALPCS { get; set; }
        public double TOTALCARAT { get; set; }
        public double RATE { get; set; }
        public double GROSSAMOUNT { get; set; }
        public double TERMSPER { get; set; }
        public double TERMSAMOUNT { get; set; }
        public double EXPPER1 { get; set; }
        public double EXPAMOUNT1 { get; set; }
        public double EXPPER2 { get; set; }
        public double EXPAMOUNT2 { get; set; }
        public double EXPPER3 { get; set; }
        public double EXPAMOUNT3 { get; set; }
        public double EXPPER4 { get; set; }
        public double EXPAMOUNT4 { get; set; }
        public double ADDLESSPER { get; set; }
        public double ADDLESSAMOUNT { get; set; }
        public double NETAMOUNT { get; set; }
        public double EXTRAINTERESTPER { get; set; }
        public double BROKRAGEPER { get; set; }
        public double BROKRAGEAMOUNT { get; set; }
        public double ADATPER { get; set; }
        public double ADATAMOUNT { get; set; }
        public double FINALAMOUNT { get; set; }
        public string INVOICEREMARK { get; set; }
        public string YOURREMARK { get; set; }
        public string XMLFORROUGHDETAIL { get; set; }

        public string FROMLOTS { get; set; }
        public string TOLOTS { get; set; }


        public Int64 TRN_ID { get; set; }

        public string OPERATION { get; set; }
        public Int64 REFLOT_ID { get; set; }

        public string LOTNO { get; set; }
        public string REFLOTNO { get; set; }
        public Int32 SRNO { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }

}
