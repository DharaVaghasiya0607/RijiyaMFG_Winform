using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class RoughAllotmentProperty
    {
        public Int64 ALLOTMENT_ID { get; set; }

        public Int64 ALLOTMENTDETAIL_ID { get; set; }

        public Int64 INVOICE_ID { get; set; }
        public Int64 LOT_ID { get; set; }//K :28/11/22
        public double ROUGHWEIGHT { get; set; }
        public Int64 PARTY_ID { get; set; }
        public string ALLOTMENTDATE { get; set; }
        public Int64 BROKER_ID { get; set; }
        public double BORKERAGEPER { get; set; }
        public double EXTRAEXP { get; set; }
        public double EXCRATE { get; set; }
        public double ROUGHOUTRATE { get; set; }
        public double FROUGHOUTRATE { get; set; }
        public double NETAMOUNT { get; set; }
        public double FNETAMOUNT { get; set; }
        public double OUTROUGHCTS { get; set; }
        public double OUTRATE { get; set; }
        public double FOUTRATE { get; set; }
        public double ASSORTMENTCTS { get; set; }
        public double ASSORTMENTRATE { get; set; }
        public double FASSORTMENTRATE { get; set; }
        public double ASSORTMENTBALANCECTS { get; set; }
        public double MFGCTS { get; set; }
        public double MFGRATE { get; set; }
        public double FMFGRATE { get; set; }
        public string XMLFORROUGHDETAIL { get; set; }
        public string REMARK { get; set; }

        public Int64 ENTRYBY { get; set; }
        public string ENTRYIP { get; set; }
        public Int64 UPDATEBY { get; set; }
        public string UPDATEIP { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
