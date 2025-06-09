using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TRN_FixedExpenseEntryProperty
    {
        public Guid EXPENSE_ID{ get; set; }


        public Int32 YYYY { get; set; }
        public Int32 MM { get; set; }
        
        public string LEDGERNAME { get; set; }
        public Int64 LEDGER_ID { get; set; }
        public double AMOUNT { get; set; }
        public string REMARK { get; set; }

        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
