using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TRN_SingleBreakingPacketEntry
    {
        public Int64 BREAKING_ID { get; set; }
        public Int64 GROUP_ID { get; set; }

        public int BREAKINGTYPE_ID { get; set; }
        public string BREAKINGTYPE { get; set; }

        public Int64 KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }

        public Int64 PACKET_ID { get; set; }
        public int PACKETNO { get; set; }
        public string MTAG { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }

        public Int64 BREAKINGEMPLOYEE_ID { get; set; }
        public string BREAKINGDATE { get; set; }

        public string RAPDATE { get; set; }


        public bool ISCONSIDERORIGINAL { get; set; }
        public double DIFFAMOUNT { get; set; }

        public string REMARK { get; set; }

        public Int32 BREAKINGREASON_ID { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
