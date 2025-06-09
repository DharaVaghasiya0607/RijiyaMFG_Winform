using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ItemGroupProperty
    {
        public Int64 ITEMGROUP_ID { get; set; }
        public string ITEMGROUPCODE { get; set; }
        public string ITEMGROUPNAME { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
