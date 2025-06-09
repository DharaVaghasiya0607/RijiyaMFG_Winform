using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using BLL = BusLib;
using AxonDataLib;
using System.Collections;

namespace BusLib
{
    public class BOMaximumID
    {

        public enum IDTYPE
        {
            PRD_ID = 0,
            ID = 1,
            TRN_ID = 2,
            JANGEDNO = 3,
            PACKET_ID = 4,
            BREAKGROUP_ID = 5,
            BREAKING_ID = 6
        }

        public static Int64 FindMaxID(IDTYPE pIDType )
        {
            AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
            AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

            Ope.ClearParams();
            Ope.AddParams("ID_TYPE", pIDType.ToString());
            Ope.AddParams("ReturnValue", "", DbType.String, ParameterDirection.Output);

            ArrayList AL = Ope.ExeNonQueryWithOutParameter(BLL.Configuration.BOConfiguration.ConnectionString, BLL.Configuration.BOConfiguration.ProviderName, "MST_Maximum_ID_Generate", CommandType.StoredProcedure);
            
            if (AL.Count != 0)
            {
                return Val.ToInt64(AL[0]);
            }
            return 0;
        }


    }
}
