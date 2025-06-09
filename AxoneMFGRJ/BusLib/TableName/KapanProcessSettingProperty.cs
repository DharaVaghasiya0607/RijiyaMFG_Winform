using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class KapanProcessSettingProperty
    {
        public Guid SETTING_ID { get; set; }

        public string SETTINGTYPE { get; set; }

        public string KAPANNAME { get; set; }
        public Int32 PARA_ID { get; set; }
        public string PARANAME { get; set; }

        public double DUEHOURS{ get; set; }
        public double LOSSPER { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }
    }

}
