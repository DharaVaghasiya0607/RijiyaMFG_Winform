using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class TrnKapanCreateProperty
    {
        public Int64 KAPAN_ID { get; set; }
        public string KAPANDATE { get; set; }
        public string KAPANNAME { get; set; }
        public string KAPANGROUP { get; set; }
        public string LOTGROUP { get; set; }
        public string KAPANCATEGORY { get; set; }
        public string COMPARMEMO { get; set; }

        public Int64 MANAGER_ID { get; set; }
        
        public bool ISHIDE { get; set; }

        public double KAPANRATE { get; set; }
        public double KAPANAMOUNT { get; set; }

        public double EXPMAKPER { get; set; }
        public double EXPMAKCARAT { get; set; }
        public double EXPPOLPER { get; set; }
        public double EXPPOLCARAT { get; set; }
        public double EXPDOLLAR { get; set; }
        public double EXPGIAPER { get; set; }
        public Int32  DUEDAYS { get; set; }
        public Int64 LOT_ID { get; set; }
        public string COMPLETEDATE { get; set; }

        public Int32 KAPANPCS { get; set; }
        public double KAPANCARAT { get; set; }
        public string STATUS { get; set; }

        public bool ISNOTAPPLYANYLOCK { get; set; }
        public double LABOURAMOUNT { get; set; }

        public string CLVCOMPLETEDATE { get; set; }//urvisha 24/11/22
        public string MFGISSUEDATE { get; set; }//urvisha 24/11/22
        public string GHATCOMPLETEDATE { get; set; }//urvisha 24/11/22
        public string POLISHRECVDATE { get; set; }//urvisha 24/11/22
        public string MUMBAIRECVDATE { get; set; }//urvisha24/11/22 



        //.......Urvisha add :25052023......
        public Int64 INWARD_ID { get; set; }
        public Int64 SIZEASSROT_ID { get; set; }
        public Int64 SHAPE_ID { get; set; }
        public Int64 SIZE_ID { get; set; }
        public Int64 DETAIL_ID { get; set; }
        public double RATE { get; set; }
        //......Urvisha End ......



        public string DIAMONDTYPE { get; set; } //#P : 13-09-2022

        public string REMARK { get; set; }
        public string KAPANTYPE { get; set; }
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


        public Boolean ISKAPANLOCK { get; set; }
        public Boolean ISKAPANHIDE { get; set; }
        public Boolean DIAMETERLOCK { get; set; }

        public string Ope { get; set; }     //Used in KapanLiveStock Form(On Delete)

    }

}
