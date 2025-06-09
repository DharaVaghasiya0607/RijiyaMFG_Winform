using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ParameterMasterProperty
    {
        public Int64 PARA_ID { get; set; }

        public string PARACODE { get; set; }
        public string PARANAME { get; set; }
        public string PARATYPE { get; set; }

        public string PROCESSGROUP{ get; set; }
        public string SHORTNAME { get; set; }
        public Int32 SEQUENCENO{ get; set; }
        public string REMARK { get; set; }
        public string REQPRDTYPE_ID { get; set; }

        public string POPUPPROCESS_ID { get; set; }

        public string LOCKAMTPRDTYPE_ID { get; set; }

        public string LABCODE { get; set; }
        public bool ISACTIVE { get; set; }
        public bool ISFINALISSUE { get; set; }

        public Int32 UPPERPARA_ID { get; set; } //#P : 20-02-2020
        public Int32 NUMBEROFISSUE { get; set; } //#P : 21-02-2020

        public double DUEHOURS { get; set; }
        public double LOSSPER { get; set; }

        public string KAPANROLLINGGROUP { get; set; }

        public bool ISDISPLAYONRETURN { get; set; }

        public Int64 LOCATION_ID { get; set; }  //hinal 09-02-2022   

        public string KAPANRUNNINGGROUP { get; set; }
        public bool ISLOSSDPT { get; set; }

        public string KAPANFINALREPORTGRP { get; set; }
        public string SUBGROUP { get; set; }

        public string UPLOADFILENAME { get; set; }
        public string UPLOADSERVERPATH { get; set; }
        public string UPLOADSERVERPASSWORD { get; set; }
        public string UPLOADSERVERUSERNAME { get; set; }
        public string DOWNLOADFILENAME { get; set; }
        public string DOWNLOADSERVERPATH { get; set; }
        public string DOWNLOADSERVERUSERNAME { get; set; }
        public string DOWNLOADSERVERPASSWORD { get; set; }

        public bool ISCOMMANJANGED { get; set; }

        public int LABOURPCS { get; set; }

        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }



        public string CLARITYWISEDEPARTMENT_ID { get; set; }

        public int SIZE_ID { get; set; }
    }

}
