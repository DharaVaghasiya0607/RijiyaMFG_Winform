using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ExcRateMasterProperty
    {
        public Guid EXCRATE_ID { get; set; }

        public int YY { get; set; }
        public int MM { get; set; }

        public double EXCRATE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
