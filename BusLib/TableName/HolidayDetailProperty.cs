using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class HolidayDetailProperty
    {
       
        public Int32 HOLIDAY_ID { get; set; }

        public Int32 LEAVETYPE_ID { get; set; }
        public string LEAVETYPE { get; set; }
        public string HOLIDAYDATE { get; set; }

        public double WDAYS { get; set; }

        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
