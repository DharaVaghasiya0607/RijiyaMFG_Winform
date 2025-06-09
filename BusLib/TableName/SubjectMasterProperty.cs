using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class SubjectMasterProperty
    {
        public Int32 SUBJECT_ID { get; set; }
        public Int32 ID { get; set; }
        public string NAME { get; set; }
        public Int32  SUBJECT1 { get; set; }
        public Int32 SUBJECT2 { get; set; }
        public Int32 SUBJECT3 { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
