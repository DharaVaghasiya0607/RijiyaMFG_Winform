using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnPenaltyIncentiveProperty
    {
        public Int64 PENALTY_ID { get; set; }

        public Int32 POINT_ID { get; set; }
        public string POINTNAME { get; set; }
        public Int32 NOOFPCS { get; set; }

        public string PENALTYTYPE  { get; set; }
        public string PENALTYDATE { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }

        public string KAPANNAME { get; set; }
        public string PACKETNO { get; set; }
        public string PACKETTAG { get; set; }

        public string REASON{ get; set; }

        public double AMOUNT { get; set; }
        public double RATE { get; set; }
        public string REMARK { get; set; }

        public string StrFlagResetXml { get; set; }//Gunjan:30/03/2023
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
