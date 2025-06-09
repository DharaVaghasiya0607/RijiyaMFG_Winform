using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ShiftMasterProperty
    {
        public Int32 SHIFT_ID { get; set; }

        public string SHIFTNAME { get; set; }

        public string SHIFTTYPE { get; set; }
        public string PUNCHSTARTTIME { get; set; }
        public string PUNCHENDTIME { get; set; }
        public string SHIFTSTARTTIME { get; set; }
        public string SHIFTENDTIME { get; set; }
        public string LUNCHSTARTTIME { get; set; }
        public string LUNCHENDTIME { get; set; }

        public string IDLEINTIME { get; set; }
        public string IDLEOUTTIME { get; set; }

        public Int32 TOTALHOURS { get; set; }
        public Int32 TOTALMINUTES { get; set; }

        public Int32 OTAPPLICABLEBEFORE { get; set; }
        public Int32 OTAPPLICABLEAFTER { get; set; }

        public string FROMDATE { get; set; }
        public string TODATE { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }




    }

}
