using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnEmployeeBehaviourPointProperty
    {
        public Guid BEHAVIOUR_ID { get; set; }

        public Int64 EMPLOYEE_ID { get; set; }

        public string DATE { get; set; }

        public double PLUSPOINT { get; set; }
        public double MINUSPOINT { get; set; }
        
        public string REMARK { get; set; }

        public string FROMDATE { get; set; }
        public string TODATE { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
