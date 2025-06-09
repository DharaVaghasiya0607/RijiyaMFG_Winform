using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class KapanAutomarkerSettingProprerty
    {
        public Int64 KAPAN_ID { get; set; }
        public Int64 ID { get; set; }
        public string KAPANNAME { get; set; }
        public Int64 DEPARTMENT_ID { get; set; }
        public Int64 MARKER_ID { get; set; }
        public int FROMSIZE { get; set; }
        public int TOSIZE { get; set; }


        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
