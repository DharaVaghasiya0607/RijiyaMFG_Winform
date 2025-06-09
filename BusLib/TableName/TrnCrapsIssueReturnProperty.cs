using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnCrapsIssueReturnProperty
    {
        public Int64 TRN_ID { get; set; }
        public Int64 KAPAN_ID { get; set; }
        public Int64 JANGEDNO { get; set; }
        public Int32 TOPROCESS_ID { get; set; }
        public Int32 ISSUEPCS { get; set; }
        public double ISSUECARAT { get; set; }
        public Int32 READYPCS { get; set; }
        public double READYCARAT { get; set; }
        public string KAPANNAME { get; set; }
        public int PACKETNO { get; set; }
        public Int32 LOSTPCS { get; set; }
        public double LOSTCARAT { get; set; }
        public string TRANSDATE { get; set; }
        public Int64 TRANSBY { get; set; }
        public string TRANSIP { get; set; }
        public Int32 PARTY_ID { get; set; } // K : 17/12/2022
        public string RETURNTYPE { get; set; }// K : 17/12/2022

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
        public string ReturnValueJangedNo { get; set; }
        public string ReturnValueJangedNoRet { get; set; }
        public string ReturnValueJangedNoTran { get; set; }
        
    }

}
