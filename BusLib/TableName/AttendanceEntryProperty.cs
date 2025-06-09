using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class AttendanceEntryProperty
    {
        public Guid ATD_ID { get; set; }

        public string ATDDATE { get; set; }
        public Int32 SRNO { get; set; }
        
        public Int64 EMPLOYEE_ID { get; set; }

        public string MULTIEMPLOYEECODE { get; set; }

        public Int32 DEPARTMENT_ID { get; set; }
        public Int32 DESIGNATION_ID { get; set; }
        public string AP { get; set; }
        public double WDAYS{ get; set; }
        public double WHOURS { get; set; }

        public double WMINTS { get; set; }

        public double OTHH { get; set; }
        public Int32 OTMM { get; set; }


        // #P : 29-11-2020
        public string PUNCH_INDATE { get; set; }
        public string PUNCH_INTIME { get; set; }
        public string PUNCH_OUTDATE { get; set; }
        public string PUNCH_OUTTIME { get; set; }

        public string PUNCH_INDATETIME { get; set; }
        public string PUNCH_OUTDATETIME { get; set; }
        public string IDLE_INDATETIME { get; set; }
        public string IDLE_OUTDATETIME { get; set; }

        public string IDLE_INDATE { get; set; }
        public string IDLE_INTIME { get; set; }
        public string IDLE_OUTDATE { get; set; }
        public string IDLE_OUTTIME { get; set; }
        public string SHIFTTYPE { get; set; }
        public bool ISCHANGEIDLETIME { get; set; }
        public double TOTALMIN { get; set; }

        public bool ISSUNDAYABSENT { get; set; }
        public bool ISCONSIDERSHIFTOUTTIME { get; set; }

        public string SHIFTSTARTTIME { get; set; }
        public string SHIFTENDTIME { get; set; }

        // End : #P : 29-11-2020

        public string REMARK { get; set; }       
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
