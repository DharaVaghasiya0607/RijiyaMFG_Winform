using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class QCPanultyProperty
    {
        public Int64 TRN_ID { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public string PENALTYDATE { get; set; }
        public string EMPLOYEENAME { get; set; }
        public string KAPANNAME { get; set; }
        public Int64 KAPAN_ID { get; set; }
        public Int32 PACKETNO { get; set; }
        public string TAG { get; set; }
        public Int32  NOOFPCS { get; set; }
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

    }
}
