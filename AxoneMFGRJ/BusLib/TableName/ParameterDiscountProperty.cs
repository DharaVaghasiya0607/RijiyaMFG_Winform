using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ParameterDiscountProperty
    {
        public double FROMCARAT { get; set; }
        public double TOCARAT { get; set; }
        public string SHAPECODE { get; set; }
        public string COLORCODE { get; set; }
        public string COLORNAME { get; set; }
        public string QCODE { get; set; }
        public string QNAME { get; set; }
        public string RAPDATE { get; set; }
        public string PARAMETERTYPE { get; set; }
        public string PARAMETERVALUE { get; set; }
        public string XML { get; set; }
        public double OLDVALUE { get; set; }
        public double NEWVALUE { get; set; }


        public string FC_CODE { get; set; }
        public string FC_NAME { get; set; }
        public string FQ_CODE { get; set; }
        public string FQ_NAME { get; set; }

        public string TC_CODE { get; set; }
        public string TC_NAME { get; set; }
        public string TQ_CODE { get; set; }
        public string TQ_NAME { get; set; }

        //#P : 11-02-2020
        public string FFL_CODE { get; set; }
        public string FFL_NAME { get; set; }
        public string TFL_CODE { get; set; }
        public string TFL_NAME { get; set; }

        public string OPE { get; set; }

        public string RoundXml  { get; set; }
        public string PearXml { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
