using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class HelPrintMaxLimitProperty
    {
        public Guid MAXLIMIT_ID { get; set; }
        public string SHAPETYPE { get; set; }
        public double  FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public Int32 MAXPRINTLIMIT { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
