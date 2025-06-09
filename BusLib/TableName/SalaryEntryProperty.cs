using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class SalaryEntryProperty
    {
        public Guid SALARY_ID { get; set; }

        public Int32 YYYY { get; set; }
        public Int32 MM { get; set; }
        
        public string SALARYDATE{ get; set; }

        public Int64 EMPLOYEE_ID { get; set; }
        public Int32 DEPARTMENT_ID { get; set; }
        public Int32 DESIGNATION_ID { get; set; }
        public Int64 MANAGER_ID { get; set; }

        public string SALARYTYPE { get; set; }

        public double SALARY { get; set; }

        public Int32 TOTALPCS { get; set; }
        public double TOTALCARAT { get; set; }
        public double TOTALDAYS { get; set; }
        public double WDAYS{ get; set; }


        public double TOTALHOURS { get; set; }
        public double WHOURS { get; set; }

        public double  OTHOURS { get; set; }
        public double AVGSALARY { get; set; }
        public double GROSSSALARY{ get; set; }
        public double EXTRAAMOUNT { get; set; }
        public string EXTRAREMARK { get; set; }

        //#P : 08-01-2021
        public Int64 WMINTS { get; set; }
        public Int64 OTMINTS { get; set; }
        public Int64 TOTALMINTS { get; set; }
        //End : #P : 08-01-2021

        public double TOTALUPAD { get; set; }

        public double NETSALARY { get; set; }
        public double OTSALARY { get; set; }
        public double NETPAYABLE { get; set; }
     
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
