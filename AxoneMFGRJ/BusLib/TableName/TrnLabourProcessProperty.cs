
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnLabourProcessProperty
    {
        public Int64 LABOURPROCESS_ID { get; set; }
        public Int32 YYYY { get; set; }
        public Int32 MM { get; set; }
        public Int32 PROCESS_ID { get; set; }
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public double RATE { get; set; }

        //public string KAPANNAME { get; set; }
        public double EXPDOLLARREVICE { get; set; }
        public double EXPDOLLAR { get; set; }
       
        public string LABOURTYPE { get; set; }

        public Int32 DEPARTMENT_ID { get; set; }
        public Int64 MANAGER_ID { get; set; }
        public string MANAGERNAME { get; set; }
        public string SYSDATE { get; set; }
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }

        public Int32 SUBPROCESS_ID { get; set; }//Gunjan:30/03/2023
    }

}
