using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnDollarLabourPerProperty
    {
        public Guid LABOUR_ID { get; set; }


        public int YY { get; set; }
        public int MM { get; set; }
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public double PER { get; set; }
        public string DOLLARLABOURTYPE { get; set; }

        public int SHAPE_ID { get; set; }
        public int CUT_ID { get; set; }
        public int POL_ID { get; set; }
        public int SYM_ID { get; set; }

        public string SHAPETYPE { get; set; }

        public double FROMAMOUNT { get; set; }
        public double TOAMOUNT { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
