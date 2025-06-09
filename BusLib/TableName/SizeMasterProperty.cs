using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class SizeMasterProperty
    {
        public Int32 SIZE_ID { get; set; }
        public string SIZENAME { get; set; }
        public string SIZETYPE { get; set; }
        public double  FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public bool ISACTIVE  { get; set; }
        public string REMARK  { get; set; }
        public bool ISADDITIONALASSORTMENT { get; set; } //U:25052023
        public string FINALREPORTGROUP { get; set; }
        public string ROUGHTYPE { get; set; }
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }



        public int SEQUENCENO { get; set; }

        public int DEPARTMENT_ID { get; set; }

        public string SIZEWISEDEPARTMENT_ID { get; set; }
    }

}
