using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class LeaveEntryProperty
    {
        public Guid     LEAVE_ID { get; set; }

        public Int64    SLIPNO { get; set; }
        public string   SLIPDATE { get; set; }
        public Int64    EMPLOYEE_ID { get; set; }
        public Int32    REASON_ID { get; set; }
        public string   OTHERREASON { get; set; }
        public string   REMARK { get; set; }
        public string   LEAVETYPE{ get; set; }
        public string   LEAVEFROMDATE { get; set; }
        public string   LEAVETODATE { get; set; }
        public double   TOTALDAYS { get; set; }
        public string   LEAVEFROMTIME { get; set; }
        public string   LEAVETOTIME { get; set; }
        public double   TOTALHOURS { get; set; }

        public Int32 HH { get; set; }
        public Int32 MM { get; set; }
        public bool  ISOFFICEWORK { get; set; }

        public string ENTRYDATE { get; set; }
        public Int64  ENTRYBY { get; set; }
        public string ENTRYIP { get; set; }

        public string UPDATEDATE { get; set; }
        public Int64  UPDATEBY { get; set; }
        public string UPDATEIP { get; set; }

        public string LEAVESTATUS { get; set; }
        public string COMMENT{ get; set; }

        public string STATUSUPDATEDATE { get; set; }
        public Int64 STATUSUPDATEBY { get; set; }
        public string STATUSUPDATEIP { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
