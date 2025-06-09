using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnInvoiceProperty
    { 
        public Int64  INVOICE_ID { get; set; }
        public string INVOICETYPE { get; set; }
        public string TRNTYPE { get; set; }
        public string FINYEAR { get; set; }
        public string BILLDATE { get; set; }
        public int BILLNO { get; set; }
        public string CASHCREDIT	{ get; set; }
        public string REFNO1 { get; set; }
        public string REFNO2 { get; set; }

        public Int64 BILLINGLEDGER_ID	{ get; set; }
        public Int64 SHIPPINGLEDGER_ID { get; set; }

        public string BILLINGLEDGERNAME { get; set; }
        public string SHIPPINGLEDGERNAME { get; set; }

        public int PAYMENTDAYS { get; set; }

        public string REMARK { get; set; }
        public string BILLINGADDRESS { get; set; }
        public string BILLINGSTATE { get; set; }
        public string SHIPPINGADDRESS { get; set; }
        public string SHIPPINGSTATE { get; set; }
        
        public string PAYMENTDATE { get; set; }
        public double GROSSAMOUNT { get; set; }
        public double IGSTAMOUNT { get; set; }
        public double CGSTAMOUNT { get; set; }
        public double SGSTAMOUNT { get; set; }
        public double TOTALGSTAMOUNT { get; set; }
        public double TOTALWITHGSTAMOUNT { get; set; }
        public double ADDLESSAMOUNT	{ get; set; }
        public double NETAMOUNT { get; set; }
        
        public string ReturnValueTrnID { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
