using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class PriceHeadDetailProperty
    {
        public Guid PRICE_ID { get; set; }

        public string SHAPENAME { get; set; }
        public string CUTNAME { get; set; }
        public string POLNAME { get; set; }
        public string SYMNAME { get; set; }
        
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public Int32 YY { get; set; }
        public Int32 MM { get; set; }
       
        public Int32 SHAPE_ID{ get; set; }
        public Int32 CUT_ID { get; set; }
        public Int32 POL_ID { get; set; }
        public Int32 SYM_ID { get; set; }
        
        public double NVALUE { get; set; }

        public string COLORNAME { get; set; }
        public Int32 COLOR_ID { get; set; }

        public string CLARITYNAME { get; set; }
        public Int32 CLARITY_ID { get; set; }
        public string RAPDATE { get; set; }

        public string FROMCOLORNAME { get; set; }
        public string TOCOLORNAME { get; set; }

        public string FROMCLARITYNAME { get; set; }
        public string TOCLARITYNAME { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
