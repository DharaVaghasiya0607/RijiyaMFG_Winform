using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class RoughUpdateProperty
    {
        public bool ISCOMPLETE { get; set; }
        public string COMPLETEDATE { get; set; }
        public int LOT_ID { get; set;
        }

        public Int64 UPDATEBY { get; set; }
        public string UPDATEIP { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }
}
