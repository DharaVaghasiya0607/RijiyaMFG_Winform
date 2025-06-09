using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnOrderEntryProperty
    {
        public Int64 ORDER_ID { get; set; }
        public Int64 ORDERDETAIL_ID { get; set; }
        public Int64 COMPANY_ID { get; set; }
        public Int32 ORDERYEAR { get; set; }
        public Int32 ORDERMONTH { get; set; }
        public Int32 ORDERNO { get; set; }
        public string SYSTEMORDERNO { get; set; }
        public string MANUALORDERNO { get; set; }
        public Int64 PARTY_ID { get; set; }
        public string PARTYNAME { get; set; }
        public Int32 ORDERDUE { get; set; }
        public string ORDERDUEDATE { get; set; }
        public string ORDERDATE { get; set; }
        public string ORDERPRIORITY { get; set; }
        public int ORDERSEQNO { get; set; }

        public string ROUGHTYPE { get; set; }
        public string PARTYREMARK { get; set; }
        public string YOURREMARK { get; set; }
        public string XMLDETAIL { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }

}
