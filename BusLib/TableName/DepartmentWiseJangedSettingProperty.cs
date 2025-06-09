using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class DepartmentWiseJangedSettingProperty
    {
        public Int32 ID { get; set; }
        public Int32 STARTFROMDEPARTMENT_ID { get; set; }
        public Int32 STARTTODEPARTMENT_ID { get; set; }
        public Int32 STARTFROMPROCESS_ID { get; set; }
        public Int32 STARTTOPROCESS_ID { get; set; }
        public string STARTENTRYTYPE { get; set; }

        public Int32 ENDFROMDEPARTMENT_ID { get; set; }
        public Int32 ENDTODEPARTMENT_ID { get; set; }
        public Int32 ENDFROMPROCESS_ID { get; set; }
        public Int32 ENDTOPROCESS_ID { get; set; }
        public string ENDENTRYTYPE { get; set; }

        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
