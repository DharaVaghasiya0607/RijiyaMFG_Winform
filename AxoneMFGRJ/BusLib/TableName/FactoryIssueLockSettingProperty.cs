using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
  public  class FactoryIssueLockSettingProperty
    {
        public Int64 EMPLOYEE_ID { get; set; }
        public Int32 DEPARTMENT_ID { get; set; }
        public Int32 PROCESS_ID { get; set; }
        public Int32 SHAPE_ID { get; set; }
        public Int32 COLOUR_ID { get; set; }
        public Int32 CLARITY_ID { get; set; }
      
      public string SHAPE { get; set; }

        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }

        public string COLOUR { get; set; }
        public string CLARITY { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
