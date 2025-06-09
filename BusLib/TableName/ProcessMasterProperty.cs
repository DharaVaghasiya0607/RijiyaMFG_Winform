using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
     public class ProcessMasterProperty
    {
        public Int64  PER_ID  { get; set; }
        public Double PER { get; set; }
        public Int64 EMPLOYEE_ID { get; set; }
        public int PROCESS_ID { get; set; }
        public int DEPARTMENT_ID { get; set; }
        public int NoOfIssue { get; set; }
        public string RoughType { get; set; }
        public string PROCESSNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public double AGEINGHOURS { get; set; }

        public bool ISIssueReturnLock { get; set; }
        public string REMARK { get; set; }
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public Int64 KAPAN_ID { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
