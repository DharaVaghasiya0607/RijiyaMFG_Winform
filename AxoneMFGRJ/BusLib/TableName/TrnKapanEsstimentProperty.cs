using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnKapanEsstimentProperty
    {
       public Int64 KAPAN_ID { get; set; }
       public string KAPANNAME { get; set; }
       public string XMLFORKAPANESSTIMENT { get; set; }

       public string  RETURNVALUEMAXPACKETNO { get; set; }

       public string ReturnValue { get; set; }
       public string ReturnMessageType { get; set; }
       public string ReturnMessageDesc { get; set; }

    }
}
