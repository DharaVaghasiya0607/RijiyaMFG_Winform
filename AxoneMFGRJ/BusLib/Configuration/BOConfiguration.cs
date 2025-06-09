using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AxonDataLib;
using BusLib.TableName;
using System.IO;
using System.Data;
namespace BusLib.Configuration
{
    public class BOConfiguration
    {

        static AxonDataLib.BOSQLHelper Ope = new AxonDataLib.BOSQLHelper();
        static AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

        public static string ConnectionString = string.Empty;
        public static string ProviderName = string.Empty;
        public static string QCConnectionString = string.Empty;
        public static string QCProviderName = string.Empty;

        public static string ConnectionFileName = string.Empty;

        public static string ConnectionFileName_TransferDB = string.Empty;

        public static string ConnectionString_Mumbai = string.Empty;
        public static string ProviderName_Mumbai = string.Empty;

        public static string ConnectionString_TransferDB = string.Empty;
        public static string ProviderName_TransferDB = string.Empty;

        public static string ComputerIP = string.Empty;
        public static string ComputerMACID = string.Empty;
        public static string ComputerName = string.Empty;

        // Dhara : 01-12-2022
        public static string FINYEAR_ID = string.Empty;
        public static string FINYEARNAME = string.Empty;

        public static LedgerMasterProperty gEmployeeProperty = new LedgerMasterProperty();
        
        public static string ENCODE_DECODE(string pStr, string pStrToEncodeOrDecode)
        {
            try
            {
                int IntPos;
                string StrPass;
                string StrECode;
                string StrDCode;
                char ChrSingle;

                StrECode = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
                StrDCode = ")(*&^%$#@!";

                for (int IntLen = 1; IntLen <= 60; IntLen++)
                {
                    StrDCode = StrDCode + (Char)(IntLen + 160);
                }
                try
                {

                    StrPass = "";
                    for (int IntCnt = 0; IntCnt <= pStr.Trim().Length - 1; IntCnt++)
                    {
                        ChrSingle = char.Parse(pStr.Substring(IntCnt, 1));
                        if (pStrToEncodeOrDecode == "E")
                        {
                            IntPos = StrECode.IndexOf(ChrSingle, 0);
                        }
                        else
                        {
                            IntPos = StrDCode.IndexOf(ChrSingle, 0);
                        }
                        if (pStrToEncodeOrDecode == "E")
                        {
                            StrPass = StrPass + StrDCode.Substring(IntPos, 1);
                        }
                        else
                        {
                            StrPass = StrPass + StrECode.Substring(IntPos, 1);
                        }
                    }
                }
                catch (Exception EX)
                {
                    throw;
                }

                return StrPass;
            }
            catch (Exception EX)
            {
                return "";
            }
            
        }


        public static string BackUp(string StrFilePath)
        {
            try
            {
                string StrDirectory = StrFilePath;

                if (System.IO.Directory.Exists(StrDirectory) == false)
                {
                    System.IO.Directory.CreateDirectory(StrDirectory);
                }

            //    (from f in new DirectoryInfo(StrDirectory).GetFiles()
            //     where f.CreationTime < DateTime.Now.Subtract(TimeSpan.FromDays(30))
            //     select f
            //).ToList()
            //   .ForEach(f => f.Delete());

                string DatabaseName = System.Configuration.ConfigurationManager.AppSettings["DBName"].ToString();
                string BackUpName = DatabaseName + DateTime.Now.ToString("yyyyMMddhhmmss") + ".bak";

                string StrSql = "BACKUP DATABASE [" + DatabaseName + "] TO  DISK = N'";
                StrSql = StrSql + StrDirectory + BackUpName + "'";
                StrSql = StrSql + " WITH NOFORMAT, NOINIT,  NAME = N'" + DatabaseName + "-Full Database Backup', SKIP, NOREWIND, NOUNLOAD,  STATS = 10";

                Ope.ClearParams();
                Ope.ExeNonQuery(ConnectionString, ProviderName, StrSql, CommandType.Text);

                return BackUpName;
                // DBShrink
                //Comment by pinali 12-02-2018
                //if (DateTime.Now.DayOfWeek == DayOfWeek.Monday)
                //{
                //    StrSql = "DBCC SHRINKFILE (N'" + DatabaseName + "_log' , 0,TRUNCATEONLY)";
                //    Ope.ClearParams();
                //    Ope.ExeNonQuery(ConnectionString, ProviderName, StrSql, CommandType.Text);

                //}
                //End as

            }
            catch (Exception ex)
            {
                return "";
            }
        }

    }
}
