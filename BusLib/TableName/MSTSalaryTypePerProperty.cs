using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class MSTSalaryTypePerProperty
    {
        public Guid WEDGES_ID { get; set; }


        public int YY { get; set; }
        public int MM { get; set; }
        public double PER { get; set; }

        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public int SALERYTYPE_ID { get; set; }
        public string SLARYTYPE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
