using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class DepartmentWiseProcessLockProperty
    {
        public Int32 PROCESSLOCK_ID { get; set; }
        public Int32 DEPARTMENT_ID { get; set; }
        public string PREVENTRYTYPE { get; set; }
        public Int32 PREVPROCESS_ID { get; set; }
        public string NEXTENTRYTYPE { get; set; }
        public Int32 NEXTPROCESS_ID { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
