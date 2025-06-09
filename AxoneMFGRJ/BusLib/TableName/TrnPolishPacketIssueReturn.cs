using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnPolishPacketIssueReturn
    {
        public Int64 MAINPOLISHPACKET_ID { get; set; }   
        public Int64 KAPAN_ID { get; set; }

        public Int64 POLISHPACKET_ID { get; set; }
        public string KAPANNAME { get; set; }
        public Int32 NEXTPROCESS_ID { get; set; } 
        

        public Int32 PACKETNO { get; set; }

      
        public Int32 LOTPCS { get; set; }
        public double LOTCARAT { get; set; }
       
        public string ENTRYDATE { get; set; }
        public Int64 TRN_ID { get; set; }
        public string ENTRYTYPE { get; set; }      
      
        public Int64 TOEMPLOYEE_ID { get; set; }


      
        public Int64 TOMANAGER_ID { get; set; }

        public Int32 TODEPARTMENT_ID { get; set; }
       
        public Int32 TOPROCESS_ID { get; set; }
        public Int64 JANGEDNO { get; set; }

      
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
        public string ReturnValueJangedNo { get; set; }
       
    }
}
