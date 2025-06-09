using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class KapanOutwardProperty
    {
        public Int64 OUTWARD_ID { get; set; }
        public Int64 KAPAN_ID { get; set; }
        public string KAPANNAME { get; set; }
        public decimal INWARDCARAT { get; set; }
        public decimal ASSORTCARAT { get; set; }
        public string OUTWARDDATE { get; set; }
        public decimal RATE { get; set; }
        public decimal RATERS { get; set; }
        public string REMARK { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
