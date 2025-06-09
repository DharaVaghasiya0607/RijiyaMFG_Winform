using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TRN_RejectionProperty
    {
        public Int64 REJECTIONTRN_ID { get; set; }
        public string REJECTIONDATE { get; set; }
        
        public Int64 LOT_ID { get; set; }
        public string PARTYNAME { get; set; }

        public Int64 KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }

        public Int64 PACKET_ID { get; set; }
        public Int32 PACKETNO { get; set; }
        public string TAG { get; set; }


        public Int32 REJECTION_ID { get; set; }

        public int PCS { get; set; }
        public double CARAT { get; set; }
        public double RATE { get; set; }
        public double AMOUNT { get; set; }
        public double TRANSFERCARAT { get; set; }

        public string REJECTIONFROM { get; set; }
        
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

        public string Ope { get; set; }     //Used in KapanLiveStock Form(On Delete)

        public Int64 KAPANMAINMANAGER_ID { get; set; }
    }
}
