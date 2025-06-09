using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnInvoiceDetailProperty
    {
        public string LOT_ID { get; set; }
        public Int64  INVOICE_ID { get; set; }
        public int  SRNO { get; set; }
        public Int64  ITEM_ID { get; set; }
        public string ITEMDESCRIPTION { get; set; }
        public double QTY { get; set; }
        public double RATE { get; set; }
        public double AMOUNT { get; set; }
        public double IGSTPER { get; set; }
        public double IGSTAMOUNT { get; set; }
        public double CGSTPER { get; set; }
        public double CGSTAMOUNT { get; set; }
        public double SGSTPER { get; set; }
        public double SGSTAMOUNT { get; set; }
        public double TOTALGSTAMOUNT { get; set; }
        public double TOTALWITHGSTAMOUNT { get; set; }
        public double ADDLESSAMOUNT { get; set; }
        public double OTHERCHARGES	 { get; set; }
        public double NETAMOUNT	 { get; set; }
        public string REMARK { get; set; }


        public string PROD_ID { get; set; }
        public string PRODUCTIONDATE { get; set; }
        public int YYYYMMDD { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public int PRODUCTION { get; set; }
        public int HEAD { get; set; }

        public string ReturnValueTrnID { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
