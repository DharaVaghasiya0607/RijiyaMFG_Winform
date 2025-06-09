using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class RapaportCriteriaProperty
    {
        public int SHAPE_ID { get; set; }
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public int CLARITY_ID { get; set; }
        
        public string COLORCODE { get; set; }
        public string PARAMETERTYPE { get; set; }
        public string PARAMETERVALUECODE { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
