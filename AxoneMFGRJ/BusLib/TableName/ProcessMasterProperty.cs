using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
     public class ProcessMasterProperty
    {
        public Int64  PER_ID  { get; set; }
        public string PER { get; set; }
        public Int64 PROCESS_ID { get; set; }
        public string PROCESSNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
