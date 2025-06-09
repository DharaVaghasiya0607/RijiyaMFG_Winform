﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class AgeKapanWiseSettingProperty
    {
        public Guid @AGINGSETTING_ID { get; set; }
        public string KAPANNAME { get; set; }
        public int @PROCESS_ID { get; set; }
        public double @AGINGMINUTE { get; set; }
        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        public double CARAT { get; set; }
        public double AMOUNT { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
