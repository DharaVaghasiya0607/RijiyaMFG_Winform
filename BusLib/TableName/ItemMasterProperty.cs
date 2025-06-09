using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace BusLib.TableName
{
    public class ItemMasterProperty
    {
        public Int64 ITEM_ID { get; set; }
        public string ITEMCODE { get; set; }
        public string ITEMNAME { get; set; }
        
        
        public Int64 ITEMGROUP_ID { get; set; }
        public string ITEMNAMEGUJARATI { get; set; }

        public string HSNCODE { get; set; }
        public string BARCODE { get; set; }
        public string ITEMTYPE { get; set; }
        public string UNITTYPE { get; set; }
        
        public double  IGSTPER { get; set; }
        public double CGSTPER { get; set; }
        public double SGSTPER { get; set; }
        public double SALERATE { get; set; }
        public double OPENINGQTY { get; set; }
        public double OPENINGRATE { get; set; }


        public bool ISACTIVE { get; set; }
        public string REMARK { get; set; }
        
        public string ReturnValue { get; set; }
        public string ReturnMessageType { get; set; }
        public string ReturnMessageDesc { get; set; }


    }

}
