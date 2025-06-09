using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class PolishIssueReturnProperty
    {
       public Int32 PARTY_ID { get; set; }
       public Int64 KAPAN_ID { get; set; }
       public string KAPANNAME { get; set; }
       public Int64 TOEMPLOYEE_ID { get; set; }
       public Int64 TOMANAGER_ID { get; set; }
       public Int32 TODEPARTMENT_ID { get; set; }
       public int PCS { get; set; }
       public double CARAT { get; set; }
       public string TOEMPLOYEECODE { get; set; }
       public string TOEMPLOYEENAME { get; set; }
       public Int32 PACKETNO { get; set; }
       public Int64 JANGEDNO { get; set; }

       public string PACKETCATEGORY { get; set; }
       public string PACKETTYPE { get; set; }

       public string  RETURNVALUEMAXPACKETNO { get; set; }

       public string ReturnValue { get; set; }
       public string ReturnMessageType { get; set; }
       public string ReturnMessageDesc { get; set; }
       public string RETURNVALUEJANGEDNO { get; set; }

    }
}
