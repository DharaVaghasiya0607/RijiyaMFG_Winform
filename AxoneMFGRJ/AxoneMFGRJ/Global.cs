using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

using SProc = BusLib.TPV.BOSProc;
using TabName = BusLib.TPV.BOTableName;
using Config = BusLib.Configuration.BOConfiguration;
using AxoneMFGRJ.MDI;
using System.Net;
using System.Net.Sockets;
using AxoneMFGRJ.Utility;
using DevExpress.LookAndFeel;
using System.Data;
using BusLib;
using BusLib.Configuration;
using System.IO;
using Microsoft.VisualBasic;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Drawing2D;
using BusLib.Master;
using System.Xml;
using System.Xml.Serialization;
using System.Data.SqlClient;
using System.Collections;
using DevExpress.XtraGrid.Views.Grid;
using System.Net.NetworkInformation;
using System.Management;

namespace AxoneMFGRJ
{
    static class Global
    {
        public enum LANGUAGE
        {
            GUJARATI = 0,
            ENGLISH = 1
        }
        /// <summary>
        /// The main entry point for the application.
        /// </summary>

        static readonly string PasswordHash = "";
        static readonly string SaltKey = "AxoneInfotech";
        static readonly string VIKey = "AxoneRajVakadiya";

        public static string gStrMessage = string.Empty;
        public static Form gMainRef = null;
        public static List<InputLanguage> AL = new List<InputLanguage>();
        public static string gStrRegisterKey = string.Empty;
        public static string gStrExePath = string.Empty;
        public static string gStrExePathUserName = string.Empty;
        public static string gStrExePathPassword = string.Empty;

        public static string gStrExeVersion = string.Empty;
        public static string gStrCompanyName = string.Empty;
        public static string gStrSuvichar = string.Empty;
        public static string strEventUrl = string.Empty;
        public static string gStrLocalDownloadPath = string.Empty;
        public static string gStrLocalOutputPath = string.Empty;
        public static Int64 gIntGlobalUserID { get; set; }

        [STAThread]
        static void Main()
        {
            try
            {
                //Global.Message("Step 1");
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Thread.CurrentThread.CurrentCulture = new CultureInfo("en-GB");

                Config.ConnectionFileName = "conn1";  //For Live : 150.107.188.120,1433
                Config.ConnectionString = System.IO.File.ReadAllText(Application.StartupPath + "\\conn1.txt");
                Config.ConnectionString = Decrypt(Config.ConnectionString);
                Config.ProviderName = "System.Data.SqlClient";


                Config.QCConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["QCConnectionString"].ConnectionString;
                Config.QCProviderName = "System.Data.SqlClient";

                //string str1 = Encrypt(Config.ConnectionString);
                //Global.Message("Step 2_Connectionstring" + str1);
                gStrRegisterKey = System.Configuration.ConfigurationManager.AppSettings["SecurityKey"].ToString();

                //try
                //{
                //    //#P : 16-08-2020
                //    if (!IsServerConnected(Config.ConnectionString.ToString()))
                //    {
                //        Config.ConnectionFileName = "conn2";// For Live : 203.109.83.120
                //        Config.ConnectionString = System.IO.File.ReadAllText(Application.StartupPath + "\\conn2.txt");
                //        Config.ConnectionString = Decrypt(Config.ConnectionString);
                //        Config.ProviderName_Mumbai = "System.Data.SqlClient";
                //    }
                //}
                //catch (Exception ex)
                //{
                //    Global.Message(ex.Message.ToString());
                //}

                //string Str = Global.Encrypt(Config.ConnectionString.ToString());
                //Global.Message("Step 3" + Str);
                gStrCompanyName = System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString();

                //End : #P : 16-08-2020

                //gStrExePath = System.Configuration.ConfigurationManager.AppSettings["ExeUpdatePath"].ToString(); //Cmnt : #P : 04-12-2019
                //Global.Message("Step 4_Start Update try block" );
                try
                {

                    //Chng : #P : 04-06-2020 : Coz Mubai Connection used At the time of RoughPurchase
                    //gStrExePath = new BOMST_FormPermission().GetExeUpdatePath();
                    DataTable Dt = new BOMST_FormPermission().GetSettingDataForExePathAndConnection();

                    DataRow[] DrExePath = Dt.Select("SETTINGKEY = 'EXEUPDATEPATH'", "");

                    if (DrExePath.Length != 0)
                    {
                        gStrExePath = DrExePath[0]["SETTINGVALUE"].ToString();
                    }

                    DrExePath = Dt.Select("SETTINGKEY = 'EXEUPDATEPATHUSERNAME'", "");

                    if (DrExePath.Length != 0)
                    {
                        gStrExePathUserName = DrExePath[0]["SETTINGVALUE"].ToString();
                    }

                    DrExePath = Dt.Select("SETTINGKEY = 'EXEUPDATEPATHPASSWORD'", "");

                    if (DrExePath.Length != 0)
                    {
                        gStrExePathPassword= DrExePath[0]["SETTINGVALUE"].ToString();
                    }

                    /*
                    DataRow[] DrMumConnection = Dt.Select("SETTINGKEY = 'MUMBAICONNECTION'", "");
                    if (DrMumConnection.Length != 0)
                    {
                        Config.ConnectionString_Mumbai =  DrMumConnection[0]["SETTINGVALUE"].ToString();
                        Config.ProviderName_Mumbai = "System.Data.SqlClient";
                    }
                    */


                    if (gStrExePath != "" && ChkUpdate() == true)
                    {
                        return;
                    }
                    //End : Chng : #P : 04-06-2020
                }
                catch (Exception ex)
                {
                    Global.Message(ex.Message.ToString());
                }
                //Global.Message("Step 5_Complete update block");
                //if (System.Configuration.ConfigurationManager.AppSettings["EmailAddress"].ToString() != "AxoneInfotech")
                //{
                //    Activation Activation = new Activation();
                //    Activation.RegisterKey = gStrRegisterKey;
                //    Activation.ShowDialog();
                //}

                //else if (System.Configuration.ConfigurationManager.AppSettings["EmailAddress"].ToString() == "AxoneInfotech")
                //{
                //    Activation Activation = new Activation();
                //    Activation.RegisterKey = gStrRegisterKey;
                //    if (Activation.CheckActivation(gStrRegisterKey) == false)
                //    {
                //        Activation.Dispose();
                //        Activation = null;
                //        Application.Exit();
                //        return;
                //    }
                //    Activation.Dispose();
                //    Activation = null;
                //}
                //else
                //{
                //    Message("NO Any Activation Key Found , Please Contact To Administration");
                //    return;
                //}


                string strHostName = "";
                strHostName = System.Net.Dns.GetHostName();
                //Global.Message("Step 5_Host name" + strHostName);
                IPAddress[] localIPs = Dns.GetHostAddresses(Dns.GetHostName());

                foreach (IPAddress a in localIPs)
                {
                    if (a.AddressFamily == AddressFamily.InterNetwork)
                    {
                        strHostName = a.ToString();
                    }
                }
                //Global.Message("Step 5_Host name" + localIPs);
                if (localIPs.Length == 0)
                {
                    Config.ComputerIP = System.Environment.MachineName.ToString();
                    Config.ComputerName = System.Net.Dns.GetHostName();
                }
                else
                {
                    Config.ComputerIP = strHostName;
                    Config.ComputerName = System.Net.Dns.GetHostName();
                }


               // Config.ComputerMACID = Config.ComputerIP + " | " + GetProcessorID() + " | " + Config.ComputerName;
                Config.ComputerMACID = Config.ComputerIP;

                //Global.Message("Step 6_Skins Color");
                DevExpress.Skins.SkinManager.EnableFormSkins();//BLL.DBConnections.ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DiaMFGConnectionString"].ConnectionString;
                DevExpress.UserSkins.BonusSkins.Register();//BLL.DBConnections.ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["DiaMFGConnectionString"].ProviderName;
                UserLookAndFeel.Default.SetSkinStyle("Caramel");
                //UserLookAndFeel.Default.SetSkinStyle("Basic");


                foreach (InputLanguage ilItem in InputLanguage.InstalledInputLanguages)
                {
                    //Add all installed input languages on system to List<>
                    AL.Add(ilItem);
                }

                FrmLogin FrmLogin = new Utility.FrmLogin();
                FrmLogin.ShowForm();

                //Application.Run(new FrmLogin());
            }
            catch (Exception ex)
            {
                Global.Message("IN Catch");
                Message(ex.Message.ToString());
            }


        }

        public static DataTable GetSelectedRecordOfGrid(GridView view, Boolean IsSelect, BODevGridSelection pSelection)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();
            DataTable resultTable = new DataTable();
            DataTable sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = pSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }
            return resultTable;
        }
        private static bool IsServerConnected(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    return true;
                }
                catch (SqlException)
                {
                    return false;
                }
            }
        }
        public static XmlDocument ConvertToXml(object list)
        {
            XmlDocument xmldoc = new XmlDocument();
            XmlSerializer _XmlSerializer = new XmlSerializer(list.GetType());
            using (MemoryStream xmlStream = new MemoryStream())
            {
                _XmlSerializer.Serialize(xmlStream, list);
                xmlStream.Position = 0;
                xmldoc.Load(xmlStream);
                return xmldoc;
            }
        }
        public static string GetProcessorID()
        {
            StringBuilder computerID = new StringBuilder();

            // Mac ID
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                if (nic.OperationalStatus == OperationalStatus.Up)
                {
                    computerID.Append(nic.GetPhysicalAddress().ToString());
                    break;
                }
            }

            ManagementObjectSearcher searcher;

            searcher = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM Win32_Processor");
            foreach (ManagementObject queryObj in searcher.Get())
            {
                computerID.Append(" | ");
                computerID.Append(queryObj["ProcessorId"]);
            }

            return computerID.ToString();
        }

        public static string GetMonthName(int pIntMonthNumber)
        {
            if (pIntMonthNumber == 1) return "Jan";
            else if (pIntMonthNumber == 2) return "Feb";
            else if (pIntMonthNumber == 3) return "Mar";
            else if (pIntMonthNumber == 4) return "Apr";
            else if (pIntMonthNumber == 5) return "May";
            else if (pIntMonthNumber == 6) return "Jun";
            else if (pIntMonthNumber == 7) return "Jul";
            else if (pIntMonthNumber == 8) return "Aug";
            else if (pIntMonthNumber == 9) return "Sep";
            else if (pIntMonthNumber == 10) return "Oct";
            else if (pIntMonthNumber == 11) return "Nov";
            else if (pIntMonthNumber == 12) return "Dec";
            else
                return "";
        }
        public static string GetMonthCode(string pStrMonthNumber)
        {
            if (pStrMonthNumber.ToUpper() == "JAN") return "01";
            else if (pStrMonthNumber.ToUpper() == "FEB") return "02";
            else if (pStrMonthNumber.ToUpper() == "MAR") return "03";
            else if (pStrMonthNumber.ToUpper() == "APR") return "04";
            else if (pStrMonthNumber.ToUpper() == "MAY") return "05";
            else if (pStrMonthNumber.ToUpper() == "JUN") return "06";
            else if (pStrMonthNumber.ToUpper() == "JUL") return "07";
            else if (pStrMonthNumber.ToUpper() == "AUG") return "08";
            else if (pStrMonthNumber.ToUpper() == "SEP") return "09";
            else if (pStrMonthNumber.ToUpper() == "OCT") return "10";
            else if (pStrMonthNumber.ToUpper() == "NOV") return "11";
            else if (pStrMonthNumber.ToUpper() == "DEC") return "12";
            else
                return "";
        }


        /// <summary>
        /// Method For Check Exe Modification And Also Update
        /// </summary>
        /// <returns></returns>        
        private static Boolean ChkUpdate()
        {
            try
            {

                using (new AxonDataLib.BONetworkConnect(Global.gStrExePath, Global.gStrExePathUserName, Global.gStrExePathPassword))
                {
                    if (Global.gStrExePath != "" && File.Exists(Global.gStrExePath + "\\" + System.Windows.Forms.Application.ProductName + ".EXE"))
                    {
                        if (File.GetLastWriteTime(Application.StartupPath + "\\" + Application.ProductName + ".EXE") < File.GetLastWriteTime(Global.gStrExePath + "\\" + Application.ProductName + ".EXE"))
                        {
                            if (Global.Confirm("Would You Like Update Latest EXE") == DialogResult.Yes)
                            {
                                if (File.Exists(Application.StartupPath + "\\Update.Bat"))
                                {
                                    File.Delete(Application.StartupPath + "\\Update.Bat");
                                }
                                FileStream fs = new FileStream(Application.StartupPath + "\\Update.Bat", FileMode.Create, FileAccess.ReadWrite);
                                StreamWriter sw = new StreamWriter(fs, Encoding.Default);

                                sw.Write("taskkill /F /IM " + Application.ProductName + ".EXE");
                                sw.WriteLine("");
                                //sw.WriteLine("copy " + Global.gStrExePath + "\\" + Application.ProductName + ".EXE  " + Application.StartupPath + "\\" + Application.ProductName + ".EXE /Y ");
                                // sw.WriteLine("copy " + Global.gStrExePath + "\\*.*  " + Application.StartupPath + "\\*.* /Y ");
                                //sw.WriteLine(""); 
                                //sw.WriteLine("copy " + Global.gStrExePath + "\\RPT\\*.* " + Application.StartupPath + "\\RPT\\*.* /Y ");

                                sw.WriteLine("xcopy " + Global.gStrExePath + " " + Application.StartupPath + " /E /H /C /I /Y");

                                sw.Flush();
                                sw.Close();
                                fs.Close();
                                System.Diagnostics.Process.Start(Application.StartupPath + "\\Update.Bat").WaitForExit();

                                return true;
                            }
                            else
                                return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }

            return false;
        }

        public static long GetMillis()
        {
            DateTime Jan1st1970 = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

            return (long)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;
        }

        public static void SelectLanguage(LANGUAGE pLanguage)
        {
            if (pLanguage == LANGUAGE.GUJARATI)
            {
                foreach (InputLanguage lan in AL)
                {
                    if (lan.Culture.ToString().ToUpper() == "GU-IN")
                    {
                        InputLanguage.CurrentInputLanguage = lan;
                    }
                }
            }
            else if (pLanguage == LANGUAGE.ENGLISH)
            {
                foreach (InputLanguage lan in AL)
                {
                    if (lan.Culture.ToString().ToUpper() == "EN-US")
                    {
                        InputLanguage.CurrentInputLanguage = lan;
                    }
                }
            }
        }


        public static DataTable SprireGetDataTableFromExcel(string path, string StrSheetName, bool hasHeader = true)
        {
            try
            {
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        pck.Load(stream);
                    }
                    var ws = pck.Workbook.Worksheets[StrSheetName];
                    DataTable tbl = new DataTable();
                    foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                    {
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                    }
                    var startRow = hasHeader ? 2 : 1;
                    for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                    {
                        var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                        DataRow row = tbl.Rows.Add();
                        foreach (var cell in wsRow)
                        {
                            row[cell.Start.Column - 1] = cell.Text;
                        }
                    }
                    return tbl;
                }
            }
            catch (Exception EX)
            {
                MessageError(EX.Message);
                return null;
            }
        }

        public static void SprirelGetSheetNameFromExcel(AxonContLib.cComboBox pCombo, string path)
        {
            try
            {
                using (var pck = new OfficeOpenXml.ExcelPackage())
                {
                    using (var stream = File.OpenRead(path))
                    {
                        pck.Load(stream);
                    }
                    var ws = pck.Workbook.Worksheets;
                    pCombo.Items.Clear();
                    for (int rowNum = 1; rowNum <= ws.Count; rowNum++)
                    {
                        pCombo.Items.Add(ws[rowNum].Name);
                    }
                }
            }
            catch (Exception EX)
            {
                MessageError(EX.Message);
            }

        }

        public static DataTable ImportExcelXLSWithSheetName(string FileName, bool hasHeaders, string SheetName, int IntIMEX = 0)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "; TypeGuessRows=0;ImportMixedTypes=Text\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=" + IntIMEX + "; TypeGuessRows=0;ImportMixedTypes=Text\"";

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                conn.Open();

                DataTable schemaTable = new DataTable("Temp");
                try
                {
                    OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + SheetName + "]", conn);
                    cmd.CommandType = CommandType.Text;

                    new OleDbDataAdapter(cmd).Fill(schemaTable);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", SheetName, FileName), ex);
                }
                return schemaTable;
            }
        }
        public static DataTable GetDataTableFromCsv(string path, bool isFirstRowHeader)
        {
            string header = isFirstRowHeader ? "Yes" : "No";

            string pathOnly = Path.GetDirectoryName(path);
            string fileName = Path.GetFileName(path);

            string sql = @"SELECT * FROM [" + fileName + "]";

            using (OleDbConnection connection = new OleDbConnection(
                      @"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + pathOnly +
                      ";Extended Properties=\"Text;HDR=" + header + "\""))
            using (OleDbCommand command = new OleDbCommand(sql, connection))
            using (OleDbDataAdapter adapter = new OleDbDataAdapter(command))
            {
                DataTable dataTable = new DataTable();
                dataTable.Locale = CultureInfo.CurrentCulture;
                adapter.Fill(dataTable);
                return dataTable;
            }
        }
        public static DataTable GetDataTableFromTxt(string path)
        {

            DataTable dt = new DataTable();
            dt.Columns.AddRange(new DataColumn[13] { new DataColumn("StoneName"), new DataColumn("RoughWeight"), new DataColumn(""), new DataColumn(""), new DataColumn(""), new DataColumn("W"), new DataColumn("Pw"), new DataColumn(""), new DataColumn("Shape"), new DataColumn("Cut"), new DataColumn("Clarity"), new DataColumn("Color"), new DataColumn("ColorShade") });
            StringBuilder sb = new StringBuilder();
            sb.Length = 100;
            List<string> list = new List<string>();
            using (StreamReader srr = new StreamReader(path))
            {
                while (srr.Peek() >= 0)
                {
                    list.Add(srr.ReadLine());
                }
            }

            for (int i = 0; i < list.Count; i++)
            {
                string[] strlist = null;
                strlist = list[i].Split('\\');
                if (strlist.GetValue(0) == "")
                {
                    break;
                }
                DataRow dr = dt.NewRow();
                for (int j = 0; j < 13; j++)
                {
                    dr[j] = strlist[j];
                }
                dt.Rows.Add(dr);
            }
            return dt;
        }
        public static Image ResizeImage(Image image, Size size, bool preserveAspectRatio = true) //Add : Pinali : 19-08-2019
        {
            int newWidth;
            int newHeight;
            if (preserveAspectRatio)
            {
                int originalWidth = image.Width;
                int originalHeight = image.Height;
                float percentWidth = (float)size.Width / (float)originalWidth;
                float percentHeight = (float)size.Height / (float)originalHeight;
                float percent = percentHeight < percentWidth ? percentHeight : percentWidth;
                newWidth = (int)(originalWidth * percent);
                newHeight = (int)(originalHeight * percent);
            }
            else
            {
                newWidth = size.Width;
                newHeight = size.Height;
            }
            Image newImage = new Bitmap(newWidth, newHeight);
            using (Graphics graphicsHandle = Graphics.FromImage(newImage))
            {
                graphicsHandle.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphicsHandle.DrawImage(image, 0, 0, newWidth + 5, newHeight);
            }
            return newImage;
        }

        public static string ConvertNumbertoWords(double? numbers, Boolean paisaconversion = false) //Add : PInali : Used In Malka Excel File (Grd IssueReturn)
        {
            var pointindex = numbers.ToString().IndexOf(".");
            var paisaamt = 0;
            if (pointindex > 0)
                paisaamt = Convert.ToInt32(numbers.ToString().Substring(pointindex + 1, 2));

            int number = Convert.ToInt32(numbers);

            if (number == 0) return "Zero";
            if (number == -2147483648) return "Minus Two Hundred and Fourteen Crore Seventy Four Lakh Eighty Three Thousand Six Hundred and Forty Eight";
            int[] num = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            if (number < 0)
            {
                sb.Append("Minus ");
                number = -number;
            }
            string[] words0 = { "", "One ", "Two ", "Three ", "Four ", "Five ", "Six ", "Seven ", "Eight ", "Nine " };
            string[] words1 = { "Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ", "Fifteen ", "Sixteen ", "Seventeen ", "Eighteen ", "Nineteen " };
            string[] words2 = { "Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ", "Seventy ", "Eighty ", "Ninety " };
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };
            num[0] = number % 1000; // units
            num[1] = number / 1000;
            num[2] = number / 100000;
            num[1] = num[1] - 100 * num[2]; // thousands
            num[3] = number / 10000000; // crores
            num[2] = num[2] - 100 * num[3]; // lakhs
            for (int i = 3; i > 0; i--)
            {
                if (num[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (num[i] == 0) continue;
                u = num[i] % 10; // ones
                t = num[i] / 10;
                h = num[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }

            if (paisaamt == 0 && paisaconversion == false)
            {
                sb.Append("ruppes only");
            }
            else if (paisaamt > 0)
            {
                var paisatext = ConvertNumbertoWords(paisaamt, true);
                sb.AppendFormat("rupees {0} paise only", paisatext);
            }
            return sb.ToString().TrimEnd();

            //if (number == 0) return "ZERO";
            //if (number < 0) return "minus " + ConvertNumbertoWords(Math.Abs(number));
            //string words = "";
            //if ((number / 1000000) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 100000) + " LAKES ";
            //    number %= 1000000;
            //}
            //if ((number / 1000) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 1000) + " THOUSAND ";
            //    number %= 1000;
            //}
            //if ((number / 100) > 0)
            //{
            //    words += ConvertNumbertoWords(number / 100) + " HUNDRED ";
            //    number %= 100;
            //}
            ////if ((number / 10) > 0)  
            ////{  
            //// words += ConvertNumbertoWords(number / 10) + " RUPEES ";  
            //// number %= 10;  
            ////}  
            //if (number > 0)
            //{
            //    if (words != "") words += "AND ";
            //    var unitsMap = new[]   
            //        {  
            //        "ZERO", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT", "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN", "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"  
            //        };
            //    var tensMap = new[]   
            //        {  
            //        "ZERO", "TEN", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY", "NINETY"  
            //        };
            //    if (number < 20) words += unitsMap[Convert.ToInt32(number)];
            //    else
            //    {
            //        words += tensMap[Convert.ToInt32(number) / 10];
            //        if ((number % 10) > 0) words += " " + unitsMap[Convert.ToInt32(number) % 10];
            //    }
            //}
            //return words;
        }

        public static string ColumnIndexToColumnLetter(int colIndex)
        {
            int div = colIndex;
            string colLetter = String.Empty;
            int mod = 0;

            while (div > 0)
            {
                mod = (div - 1) % 26;
                colLetter = (char)(65 + mod) + colLetter;
                div = (int)((div - mod) / 26);
            }
            return colLetter;
        }

        public static void BarcodePrint(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string MarkerCode)
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_TCS.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;
                string StrPrint = StrKapanName + "-" + PacketNo + "-" + Tag;
                //string OM = "OM";
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    //sw.WriteLine("I8,A");
                    //sw.WriteLine("ZN");
                    //sw.WriteLine("q400");
                    //sw.WriteLine("O");
                    //sw.WriteLine("JF");
                    //sw.WriteLine("KIZZQ0");
                    //sw.WriteLine("KI9+0.0");
                    //sw.WriteLine("ZT");
                    //sw.WriteLine("Q120,25");
                    //sw.WriteLine("Arglabel 200 31");
                    //sw.WriteLine("exit");
                    //sw.WriteLine("KI80");
                    //sw.WriteLine("N");
                    ////sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                    //sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                    //sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                    //sw.WriteLine("P1");

                    //Barcode Width : Long : #P : 05-02-2022
                    sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 66.5 mm, 15.5 mm");
                    sw.WriteLine("GAP 2.5 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                    sw.WriteLine("ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 513,103,\"2\",180,1,1,\"" + StrPrint + "\"");
                    sw.WriteLine("TEXT 186,103,\"2\",180,1,1,\"" + Date + "\"");
                    //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                    sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");

                    sw.WriteLine("TEXT 462,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                    sw.WriteLine("TEXT 125,28,\"2\",180,1,1,\"" + Carat + "\"");
                    sw.WriteLine("PRINT 1,1");
                    sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodePrintTSC(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string MarkerCode, string StrBarcodeNo, string StrGroup, string StrParentTag)
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_TSC.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                //StrGroup = Val.ToString(DRow["PACKETGRADECODE"]) + "/" + Val.ToString(DRow["PACKETGROUPCODE"]);
                //StrParentTag = Val.ToString(DRow["PARENTTAG"]).Trim() == "" ? "" : "(" + Val.ToString(DRow["PARENTTAG"]) + ")";

                //string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;
                string StrBarcode = StrBarcodeNo;
                string StrPrint = StrKapanName + "-" + PacketNo + "-" + Tag;
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    ////Barcode Width : Long : #P : 05-02-2022
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 66.5 mm, 15.5 mm");
                    //sw.WriteLine("GAP 2.5 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                    //sw.WriteLine("ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 513,103,\"2\",180,1,1,\"" + StrPrint + "\"");
                    //sw.WriteLine("TEXT 186,103,\"2\",180,1,1,\"" + Date + "\"");
                    ////sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                    //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");

                    //sw.WriteLine("TEXT 325,103,\"2\",180,1,1,\"" + StrParentTag + "\"");
                    //sw.WriteLine("TEXT 325,28,\"2\",180,1,1,\"" + StrGroup + "\"");

                    //sw.WriteLine("TEXT 462,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                    //sw.WriteLine("TEXT 125,28,\"2\",180,1,1,\"" + Carat + "\"");
                    //sw.WriteLine("PRINT 1,1");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                    ////Barcode with  Height/Width Change
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    //sw.WriteLine("GAP 4 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ");
                    //sw.WriteLine("ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 403,100,\"2\",180,1,1,\"" + StrPrint + "\"");
                    //sw.WriteLine("TEXT 122,100,\"2\",180,1,1,\"" + Date + "\"");
                    ////sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                    //sw.WriteLine("BARCODE 399,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                    //sw.WriteLine("TEXT 216,100,\"2\",180,1,1,\"" + StrParentTag + "\"");
                    //sw.WriteLine("TEXT 230,28,\"2\",180,1,1,\"" + StrGroup + "\"");
                    //sw.WriteLine("TEXT 368,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                    //sw.WriteLine("TEXT 106,28,\"2\",180,1,1,\"" + Carat + "\"");
                    //sw.WriteLine("PRINT 1,1");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                    //Barcode with  Height/Width Change
                    sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ");
                    sw.WriteLine("ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 411,100,\"2\",180,1,1,\"" + StrPrint + "\"");
                    sw.WriteLine("TEXT 130,100,\"2\",180,1,1,\"" + Date + "\"");
                    //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                    sw.WriteLine("BARCODE 407,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                    sw.WriteLine("TEXT 224,100,\"2\",180,1,1,\"" + StrParentTag + "\"");
                    sw.WriteLine("TEXT 238,28,\"2\",180,1,1,\"" + StrGroup + "\"");
                    sw.WriteLine("TEXT 376,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                    sw.WriteLine("TEXT 114,28,\"2\",180,1,1,\"" + Carat + "\"");
                    sw.WriteLine("PRINT 1,1");
                    sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodePrintTSC(DataTable DTab, string StrType) //#P : 16-05-2022
        {
            try
            {
                AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_TSC.txt";

                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                //string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;

                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        string StrKapanName = "", PacketNo = "", Tag = "", Date = "", Carat = "", MarkerCode = "", StrBarcodeNo = "", StrGroup = "", StrParentTag = "";

                        StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        PacketNo = Val.ToString(DRow["PACKETNO"]);
                        Tag = Val.ToString(DRow["TAG"]);
                        Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                        Carat = Val.ToString(DRow["LOTCARAT"]);
                        MarkerCode = StrType == "MAINMNGR" ? Val.ToString(DRow["MAINMANAGERCODE"]) : Val.ToString(DRow["EMPLOYEECODE"]);
                        StrBarcodeNo = Val.ToString(DRow["BARCODE"]);
                        StrGroup = Val.ToString(DRow["PACKETGRADENAME"]) + "/" + Val.ToString(DRow["PACKETGROUPNAME"]);
                        StrParentTag = Val.ToString(DRow["PARENTTAG"]).Trim() == "" ? "" : "(" + Val.ToString(DRow["PARENTTAG"]) + ")";

                        string StrBarcode = StrBarcodeNo;
                        string StrPrint = StrKapanName + "-" + PacketNo + "-" + Tag;

                        //sw.WriteLine("I8,A");
                        //sw.WriteLine("ZN");
                        //sw.WriteLine("q400");
                        //sw.WriteLine("O");
                        //sw.WriteLine("JF");
                        //sw.WriteLine("KIZZQ0");
                        //sw.WriteLine("KI9+0.0");
                        //sw.WriteLine("ZT");
                        //sw.WriteLine("Q120,25");
                        //sw.WriteLine("Arglabel 200 31");
                        //sw.WriteLine("exit");
                        //sw.WriteLine("KI80");
                        //sw.WriteLine("N");
                        ////sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                        //sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                        //sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                        //sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                        //sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                        //sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                        //sw.WriteLine("P1");

                        //Barcode Width : Long : #P : 05-02-2022
                        //sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 66.5 mm, 15.5 mm");
                        //sw.WriteLine("GAP 2.5 mm, 0 mm");
                        //sw.WriteLine("DIRECTION 0,0");
                        //sw.WriteLine("REFERENCE 0,0");
                        //sw.WriteLine("OFFSET 0 mm");
                        //sw.WriteLine("SET PEEL OFF");
                        //sw.WriteLine("SET CUTTER OFF");
                        //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                        //sw.WriteLine("ON");
                        //sw.WriteLine("CLS");
                        //sw.WriteLine("CODEPAGE 1252");
                        //sw.WriteLine("TEXT 513,103,\"2\",180,1,1,\"" + StrPrint + "\"");
                        //sw.WriteLine("TEXT 186,103,\"2\",180,1,1,\"" + Date + "\"");
                        ////sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                        //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                        //sw.WriteLine("TEXT 325,103,\"2\",180,1,1,\"" + StrParentTag + "\"");
                        //sw.WriteLine("TEXT 325,28,\"2\",180,1,1,\"" + StrGroup + "\"");
                        //sw.WriteLine("TEXT 462,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                        //sw.WriteLine("TEXT 125,28,\"2\",180,1,1,\"" + Carat + "\"");
                        //sw.WriteLine("PRINT 1,1");
                        //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                        ////Barcode with  Height/Width Change
                        //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                        //sw.WriteLine("GAP 4 mm, 0 mm");
                        //sw.WriteLine("DIRECTION 0,0");
                        //sw.WriteLine("REFERENCE 0,0");
                        //sw.WriteLine("OFFSET 0 mm");
                        //sw.WriteLine("SET PEEL OFF");
                        //sw.WriteLine("SET CUTTER OFF");
                        //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ");
                        //sw.WriteLine("ON");
                        //sw.WriteLine("CLS");
                        //sw.WriteLine("CODEPAGE 1252");
                        //sw.WriteLine("TEXT 403,100,\"2\",180,1,1,\"" + StrPrint + "\"");
                        //sw.WriteLine("TEXT 122,100,\"2\",180,1,1,\"" + Date + "\"");
                        ////sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                        //sw.WriteLine("BARCODE 399,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                        //sw.WriteLine("TEXT 216,100,\"2\",180,1,1,\"" + StrParentTag + "\"");
                        //sw.WriteLine("TEXT 230,28,\"2\",180,1,1,\"" + StrGroup + "\"");
                        //sw.WriteLine("TEXT 368,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                        //sw.WriteLine("TEXT 106,28,\"2\",180,1,1,\"" + Carat + "\"");
                        //sw.WriteLine("PRINT 1,1");
                        //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");


                        //Barcode with  Height/Width Change
                        sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                        sw.WriteLine("GAP 3 mm, 0 mm");
                        sw.WriteLine("DIRECTION 0,0");
                        sw.WriteLine("REFERENCE 0,0");
                        sw.WriteLine("OFFSET 0 mm");
                        sw.WriteLine("SET PEEL OFF");
                        sw.WriteLine("SET CUTTER OFF");
                        sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ");
                        sw.WriteLine("ON");
                        sw.WriteLine("CLS");
                        sw.WriteLine("CODEPAGE 1252");
                        sw.WriteLine("TEXT 411,100,\"2\",180,1,1,\"" + StrPrint + "\"");
                        sw.WriteLine("TEXT 130,100,\"2\",180,1,1,\"" + Date + "\"");
                        //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                        sw.WriteLine("BARCODE 407,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                        sw.WriteLine("TEXT 224,100,\"2\",180,1,1,\"" + StrParentTag + "\"");
                        sw.WriteLine("TEXT 238,28,\"2\",180,1,1,\"" + StrGroup + "\"");
                        sw.WriteLine("TEXT 376,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                        sw.WriteLine("TEXT 114,28,\"2\",180,1,1,\"" + Carat + "\"");
                        sw.WriteLine("PRINT 1,1");
                        sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");


                    }
                    sw.Close();
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }




        public static void BarcodePrintTSCGalaxy(string StrKapanName, string PacketNo, string Tag, string Carat, string MarkerCode, string Group, string Shade, string StrBarcodeNo)
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_TSCGALAXY.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                string StrBarcode = "!105" + StrBarcodeNo;
                string StrPrint = MarkerCode + "#" + StrKapanName + "#" + PacketNo + "#" + Tag;
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    //Barcode Width :short :hinal 10-02-2022 
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 39.5 mm, 14 mm");
                    //sw.WriteLine("GAP 3 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                    //sw.WriteLine("ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("BARCODE 297,105,\"128M\",44,0,180,2,4,\"" + StrBarcode + "\"");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 299,58,\"ROMAN.TTF\",180,1,8,\"" +StrPrint + "\"");
                    //sw.WriteLine("TEXT 147,58,\"ROMAN.TTF\",180,1,8,\"" + Carat + "");
                    //sw.WriteLine("TEXT 72,103,\"ROMAN.TTF\",180,1,8,\""+ Group +"\"");
                    //sw.WriteLine("TEXT 72,58,\"0\",180,9,8,\""+ Shade +"\"");
                    //sw.WriteLine("PRINT 1,1");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
                    sw.WriteLine("SIZE 39.5 mm, 14 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("SET TEAR ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("BARCODE 297,105,\"128M\",44,0,180,2,4,\"" + StrBarcode + "\"");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 299,58,\"ROMAN.TTF\",180,1,8,\"" + StrPrint + "\"");
                    sw.WriteLine("TEXT 147,58,\"ROMAN.TTF\",180,1,8,\"" + Carat + "\"");
                    sw.WriteLine("TEXT 72,103,\"ROMAN.TTF\",180,1,8,\"" + Shade + "\"");
                    sw.WriteLine("TEXT 72,58,\"0\",180,9,8,\"" + Group + "\"");
                    sw.WriteLine("PRINT 1,1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_TSCGALAXY.BAT") && File.Exists(fileLoc))
                {

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_TSCGALAXY.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodePrintTSCGalaxy(DataTable DTab, string StrType) //#P : 16-05-2022
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_TSCGALAXY.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }
                System.IO.File.Create(fileLoc).Dispose();

                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        string StrKapanName, PacketNo, Tag, Carat, MarkerCode, Group, Shade, StrBarcodeNo;

                        StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        PacketNo = Val.ToString(DRow["PACKETNO"]);
                        Tag = Val.ToString(DRow["TAG"]);
                        Carat = Val.ToString(DRow["LOTCARAT"]);
                        MarkerCode = Val.ToString(DRow["KAPANMANAGERCODE"]);
                        Group = Val.ToString(DRow["PACKETGROUPNAME"]);
                        Shade = Val.ToString(DRow["COLORSHADECODE"]);
                        StrBarcodeNo = Val.ToString(DRow["BARCODE"]);


                        string StrBarcode = "!105" + StrBarcodeNo;
                        string StrPrint = MarkerCode + "#" + StrKapanName + "#" + PacketNo + "#" + Tag;

                        //Barcode Width :short :hinal 10-02-2022 
                        //sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 39.5 mm, 14 mm");
                        //sw.WriteLine("GAP 3 mm, 0 mm");
                        //sw.WriteLine("DIRECTION 0,0");
                        //sw.WriteLine("REFERENCE 0,0");
                        //sw.WriteLine("OFFSET 0 mm");
                        //sw.WriteLine("SET PEEL OFF");
                        //sw.WriteLine("SET CUTTER OFF");
                        //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                        //sw.WriteLine("ON");
                        //sw.WriteLine("CLS");
                        //sw.WriteLine("BARCODE 297,105,\"128M\",44,0,180,2,4,\"" + StrBarcode + "\"");
                        //sw.WriteLine("CODEPAGE 1252");
                        //sw.WriteLine("TEXT 299,58,\"ROMAN.TTF\",180,1,8,\"" +StrPrint + "\"");
                        //sw.WriteLine("TEXT 147,58,\"ROMAN.TTF\",180,1,8,\"" + Carat + "");
                        //sw.WriteLine("TEXT 72,103,\"ROMAN.TTF\",180,1,8,\""+ Group +"\"");
                        //sw.WriteLine("TEXT 72,58,\"0\",180,9,8,\""+ Shade +"\"");
                        //sw.WriteLine("PRINT 1,1");
                        //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
                        sw.WriteLine("SIZE 39.5 mm, 14 mm");
                        sw.WriteLine("GAP 3 mm, 0 mm");
                        sw.WriteLine("DIRECTION 0,0");
                        sw.WriteLine("REFERENCE 0,0");
                        sw.WriteLine("OFFSET 0 mm");
                        sw.WriteLine("SET PEEL OFF");
                        sw.WriteLine("SET CUTTER OFF");
                        sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        sw.WriteLine("SET TEAR ON");
                        sw.WriteLine("CLS");
                        sw.WriteLine("BARCODE 297,105,\"128M\",44,0,180,2,4,\"" + StrBarcode + "\"");
                        sw.WriteLine("CODEPAGE 1252");
                        sw.WriteLine("TEXT 299,58,\"ROMAN.TTF\",180,1,8,\"" + StrPrint + "\"");
                        sw.WriteLine("TEXT 147,58,\"ROMAN.TTF\",180,1,8,\"" + Carat + "\"");
                        sw.WriteLine("TEXT 72,103,\"ROMAN.TTF\",180,1,8,\"" + Shade + "\"");
                        sw.WriteLine("TEXT 72,58,\"0\",180,9,8,\"" + Group + "\"");
                        sw.WriteLine("PRINT 1,1");

                        sw.Close();
                    }
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_TSCGALAXY.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_TSCGALAXY.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }
                Thread.Sleep(800);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }


        public static void BarcodePrintCitizen(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string StrEmployeeCode, string StrBarcodeNo, string StrPktSrNo = "", string PTag = "")
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_Citizen.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                //string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;
                string StrBarcode = StrBarcodeNo;
                string StrKapanNames = StrKapanName;
                string StrPktNoTag = PacketNo + "-" + Tag;
                string StrParentTag = "(" + PTag + ")";
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    //sw.WriteLine("G0");
                    //sw.WriteLine("n");
                    //sw.WriteLine("M0500");
                    //sw.WriteLine("O0214");
                    //sw.WriteLine("V0");
                    //sw.WriteLine("t1");
                    //sw.WriteLine("Kf0070");
                    //sw.WriteLine("L");
                    //sw.WriteLine("D11");
                    //sw.WriteLine("A2");
                    //sw.WriteLine("1e6302400220007C" + StrBarcode + "");
                    //sw.WriteLine("ySPM");
                    //sw.WriteLine("1911A0600480003" + StrPrint + "");
                    //sw.WriteLine("1911A0600090003" + MarkerCode + "");
                    ////sw.WriteLine("1911A0600490197" + Date + ""); //2022
                    //sw.WriteLine("1911A0600490204" + Date + "");  //22
                    //sw.WriteLine("1911A0600100208" + Carat + "");
                    //sw.WriteLine("Q0001");
                    //sw.WriteLine("E");

                    // Added By Darshan 08-03-2022

                    sw.WriteLine("G0");
                    sw.WriteLine("n");
                    sw.WriteLine("M0500");
                    sw.WriteLine("O0214");
                    sw.WriteLine("V0");
                    sw.WriteLine("t1");
                    sw.WriteLine("Kf0070");
                    sw.WriteLine("L");
                    sw.WriteLine("D11");
                    sw.WriteLine("A2");
                    sw.WriteLine("1e6303100150058C" + StrBarcode + "");
                    sw.WriteLine("ySPM");
                    sw.WriteLine("1911A1200310007" + StrKapanNames + "");
                    sw.WriteLine("1911A0600040053" + StrEmployeeCode + "");
                    sw.WriteLine("1911A0600480174" + Date + "");
                    sw.WriteLine("1911A0600040186" + Carat + "");
                    sw.WriteLine("1911A1200140015" + StrPktNoTag + "");
                    sw.WriteLine("1911A0600040172" + StrParentTag + "");
                    sw.WriteLine("1911A0600480055" + StrPktSrNo + "");
                    sw.WriteLine("Q0001");
                    sw.WriteLine("E");

                    //End

                    //sw.WriteLine("I8,A");
                    //sw.WriteLine("ZN");
                    //sw.WriteLine("q400");
                    //sw.WriteLine("O");
                    //sw.WriteLine("JF");
                    //sw.WriteLine("KIZZQ0");
                    //sw.WriteLine("KI9+0.0");
                    //sw.WriteLine("ZT");
                    //sw.WriteLine("Q120,25");
                    //sw.WriteLine("Arglabel 200 31");
                    //sw.WriteLine("exit");
                    //sw.WriteLine("KI80");
                    //sw.WriteLine("N");
                    ////sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                    //sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                    //sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                    //sw.WriteLine("P1");

                    //Barcode Width : Long : #P : 05-02-2022
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 66.5 mm, 15.5 mm");
                    //sw.WriteLine("GAP 2.5 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                    //sw.WriteLine("ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 513,103,\"2\",180,1,1,\"" + StrPrint + "\"");
                    //sw.WriteLine("TEXT 186,103,\"2\",180,1,1,\"" + Date + "\"");
                    ////sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                    //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");

                    //sw.WriteLine("TEXT 462,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                    //sw.WriteLine("TEXT 125,28,\"2\",180,1,1,\"" + Carat + "\"");
                    //sw.WriteLine("PRINT 1,1");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_Citizen.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_Citizen.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodePrintCitizen(DataTable DTab, string StrType, string pStrOpe) //#P : 16-05-2022
        {
            try
            {

                AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData_Citizen.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }
                System.IO.File.Create(fileLoc).Dispose();
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        string StrKapanName, PacketNo, Tag, Date, Carat, StrEmployeeCode = "", StrBarcodeNo, StrPktSrNo = "", PTag = "";

                        StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        PacketNo = Val.ToString(DRow["PACKETNO"]);
                        Tag = Val.ToString(DRow["TAG"]);
                        Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                        Carat = Val.ToString(DRow["LOTCARAT"]);
                        if (pStrOpe == "BtnBarcodePrintCurrEmp")
                        {
                            StrEmployeeCode = StrType == "MAINMNGR" ? Val.ToString(DRow["MAINMANAGERCODE"]) : Val.ToString(DRow["EMPLOYEECODE"]);
                        }
                        else if (pStrOpe == "BtnPrintMarker")
                        {
                            StrEmployeeCode = StrType == "MAINMNGR" ? Val.ToString(DRow["MAINMANAGERCODE"]) : Val.ToString(DRow["MARKERCODE"]);
                        }
                        StrBarcodeNo = Val.ToString(DRow["BARCODE"]);
                        StrPktSrNo = Val.ToString(DRow["PKTSERIALNO"]);
                        PTag = Val.ToString(DRow["PARENTTAG"]);

                        string StrBarcode = StrBarcodeNo;
                        string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                        string StrPktNoTag = PacketNo + "-" + Tag;
                        string StrParentTag = "(" + PTag + ")";

                        // Added By Darshan 08-03-2022

                        sw.WriteLine("G0");
                        sw.WriteLine("n");
                        sw.WriteLine("M0500");
                        sw.WriteLine("O0214");
                        sw.WriteLine("V0");
                        sw.WriteLine("t1");
                        sw.WriteLine("Kf0070");
                        sw.WriteLine("L");
                        sw.WriteLine("D11");
                        sw.WriteLine("A2");
                        sw.WriteLine("1e6303100150058C" + StrBarcode + "");
                        sw.WriteLine("ySPM");
                        sw.WriteLine("1911A1200310007" + StrKapanNames + "");
                        sw.WriteLine("1911A0600040053" + StrEmployeeCode + "");
                        sw.WriteLine("1911A0600480174" + Date + "");
                        sw.WriteLine("1911A0600040186" + Carat + "");
                        sw.WriteLine("1911A1200140015" + StrPktNoTag + "");
                        sw.WriteLine("1911A0600040172" + StrParentTag + "");
                        sw.WriteLine("1911A0600480055" + StrPktSrNo + "");
                        sw.WriteLine("Q0001");
                        sw.WriteLine("E");
                        //End
                    }
                    sw.Close();
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_Citizen.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_Citizen.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodeProntMkblTSC(DataRow DRow) //#P : 09-06-2022
        {

            try
            {
                AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
                string fileLoc = Application.StartupPath + "\\PrintBarcodeDataMkbl_TSC.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }
                System.IO.File.Create(fileLoc).Dispose();

                string StrBarcode = Val.ToString(DRow["BARCODE"]);
                string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                string StrEmployeeCode = Val.ToString(DRow["EMPLOYEECODE"]);
                string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                string StrParameterAmt = DRow["COLORNAME"].ToString() + "-" + DRow["CLARITYNAME"].ToString() + "-" + DRow["CUTCODE"].ToString() + "-" + DRow["FLNAME"].ToString() + "-" + DRow["AMOUNT"].ToString();
                string StrShpBlnCts = DRow["SHAPECODE"].ToString() + "-" + DRow["CARAT"].ToString() + "-" + DRow["BALANCECARAT"].ToString();
                StreamWriter sw = new StreamWriter(fileLoc);

                using (sw)
                {
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    //sw.WriteLine("GAP 2.5 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='2' pitch='15.0 mm'></xpml>SET TEAR ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 406,75,\"2\",180,1,1,\"" + StrKapanNames + "\"");
                    //sw.WriteLine("BARCODE 304,73,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                    //sw.WriteLine("TEXT 406,13,\"1\",180,1,1,\"" + StrEmployeeCode + "\"");
                    //sw.WriteLine("TEXT 69,13,\"1\",180,1,1,\"" + StrPktNoTag + "\"");
                    //sw.WriteLine("TEXT 406,42,\"2\",180,1,1,\"" + StrPktSrNo + "\"");
                    //sw.WriteLine("TEXT 304,95,\"1\",180,1,1,\"" + StrShpBlnCts + "\"");
                    //sw.WriteLine("TEXT 69,95,\"1\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                    //sw.WriteLine("TEXT 304,13,\"1\",180,1,1,\"" + StrParameterAmt + "\"");
                    //sw.WriteLine("PRINT 1,2");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                    //Barocde : 55_15
                    //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    //sw.WriteLine("GAP 3.9 mm, 0 mm");
                    //sw.WriteLine("DIRECTION 0,0");
                    //sw.WriteLine("REFERENCE 0,0");
                    //sw.WriteLine("OFFSET 0 mm");
                    //sw.WriteLine("SET PEEL OFF");
                    //sw.WriteLine("SET CUTTER OFF");
                    //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON");
                    //sw.WriteLine("CLS");
                    //sw.WriteLine("CODEPAGE 1252");
                    //sw.WriteLine("TEXT 406,75,\"2\",180,1,1,\"" + StrKapanNames + "\"");
                    //sw.WriteLine("BARCODE 304,73,\"128M\",49,0,180,2,4,\"!105" + StrBarcode + "\"");
                    //sw.WriteLine("TEXT 406,13,\"1\",180,1,1,\"" + StrEmployeeCode + "\"");
                    //sw.WriteLine("TEXT 69,13,\"1\",180,1,1,\"" + StrPktNoTag + "\"");
                    //sw.WriteLine("TEXT 406,42,\"2\",180,1,1,\"" + StrPktSrNo + "\"");
                    //sw.WriteLine("TEXT 304,95,\"1\",180,1,1,\"" + StrShpBlnCts + "\"");
                    //sw.WriteLine("TEXT 69,95,\"1\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                    //sw.WriteLine("TEXT 304,13,\"1\",180,1,1,\"" + StrParameterAmt + "\"");
                    //sw.WriteLine("PRINT 1,1");
                    //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                    //Barocde : 55_15 With font Apperence Changes
                    sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 418,80,\"3\",180,1,1,\"" + StrKapanNames + "\"");
                    sw.WriteLine("BARCODE 324,75,\"128M\",49,0,180,3,6,\"!105" + StrBarcode + "\"");
                    sw.WriteLine("TEXT 414,17,\"2\",180,1,1,\"" + StrEmployeeCode + "\"");
                    sw.WriteLine("TEXT 73,17,\"2\",180,1,1,\"" + StrPktNoTag + "\"");
                    sw.WriteLine("TEXT 414,49,\"3\",180,1,1,\"" + StrPktSrNo + "\"");
                    sw.WriteLine("TEXT 325,94,\"2\",180,1,1,\"" + StrShpBlnCts + "\"");
                    sw.WriteLine("TEXT 73,94,\"2\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                    sw.WriteLine("TEXT 325,18,\"2\",180,1,1,\"" + StrParameterAmt + "\"");
                    sw.WriteLine("PRINT 1,1");
                    sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE_TSC.BAT" + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodeProntMkblCitizen(StreamWriter sw, string StrBarcode, string StrKapanNames, string StrEmployeeCode,
                                                     string StrPktNoTag, string StrPktSrNo, string StrParameterAmt,
                                                     string StrShpBlnCts) //#P : 09-06-2022
        {

            try
            {
                //using (sw)
                //{
                sw.WriteLine("G0");
                sw.WriteLine("n");
                sw.WriteLine("M0500");
                sw.WriteLine("O0214");
                sw.WriteLine("V0");
                sw.WriteLine("t1");
                sw.WriteLine("Kf0070");
                sw.WriteLine("L");
                sw.WriteLine("D11");
                sw.WriteLine("A2");
                sw.WriteLine("1e6303100150058C" + StrBarcode + "");
                sw.WriteLine("ySPM");
                sw.WriteLine("1911C1200350003" + StrKapanNames + "");
                sw.WriteLine("1911C0800020003" + StrEmployeeCode + "");
                sw.WriteLine("1911C0800450178" + DateTime.Now.ToString("dd-MM") + "");
                sw.WriteLine("1911C0800020176" + StrPktNoTag + "");
                sw.WriteLine("1911C1200190003" + StrPktSrNo + "");
                sw.WriteLine("1911C0800020059" + StrParameterAmt + "");
                sw.WriteLine("1911C0800450063" + StrShpBlnCts + "");
                sw.WriteLine("Q0001");
                sw.WriteLine("E");

                //}
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodeProntMkblTSC(StreamWriter sw, string StrBarcode, string StrKapanNames, string StrEmployeeCode,
                                                   string StrPktNoTag, string StrPktSrNo, string StrParameterAmt,
                                                   string StrShpBlnCts) //#P : 09-06-2022
        {

            try
            {
                //Barocde : 55_15 
                //sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                //sw.WriteLine("GAP 3.9 mm, 0 mm");
                //sw.WriteLine("DIRECTION 0,0");
                //sw.WriteLine("REFERENCE 0,0");
                //sw.WriteLine("OFFSET 0 mm");
                //sw.WriteLine("SET PEEL OFF");
                //sw.WriteLine("SET CUTTER OFF");
                //sw.WriteLine("SET PARTIAL_CUTTER OFF");
                //sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON");
                //sw.WriteLine("CLS");
                //sw.WriteLine("CODEPAGE 1252");
                //sw.WriteLine("TEXT 406,75,\"2\",180,1,1,\"" + StrKapanNames + "\"");
                //sw.WriteLine("BARCODE 304,73,\"128M\",49,0,180,2,4,\"!105" + StrBarcode + "\"");
                //sw.WriteLine("TEXT 406,13,\"1\",180,1,1,\"" + StrEmployeeCode + "\"");
                //sw.WriteLine("TEXT 69,13,\"1\",180,1,1,\"" + StrPktNoTag + "\"");
                //sw.WriteLine("TEXT 406,42,\"2\",180,1,1,\"" + StrPktSrNo + "\"");
                //sw.WriteLine("TEXT 304,95,\"1\",180,1,1,\"" + StrShpBlnCts + "\"");
                //sw.WriteLine("TEXT 69,95,\"1\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                //sw.WriteLine("TEXT 304,13,\"1\",180,1,1,\"" + StrParameterAmt + "\"");
                //sw.WriteLine("PRINT 1,1");
                //sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");

                //Barocde : 55_15 With font Apperence Changes
                sw.WriteLine("<xpml><page quantity='0' pitch='15.0 mm'></xpml>SIZE 52.5 mm, 15 mm");
                sw.WriteLine("GAP 3 mm, 0 mm");
                sw.WriteLine("DIRECTION 0,0");
                sw.WriteLine("REFERENCE 0,0");
                sw.WriteLine("OFFSET 0 mm");
                sw.WriteLine("SET PEEL OFF");
                sw.WriteLine("SET CUTTER OFF");
                sw.WriteLine("SET PARTIAL_CUTTER OFF");
                sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.0 mm'></xpml>SET TEAR ON");
                sw.WriteLine("CLS");
                sw.WriteLine("CODEPAGE 1252");
                sw.WriteLine("TEXT 418,80,\"3\",180,1,1,\"" + StrKapanNames + "\"");
                sw.WriteLine("BARCODE 324,75,\"128M\",49,0,180,3,6,\"!105" + StrBarcode + "\"");
                sw.WriteLine("TEXT 414,17,\"2\",180,1,1,\"" + StrEmployeeCode + "\"");
                sw.WriteLine("TEXT 73,17,\"2\",180,1,1,\"" + StrPktNoTag + "\"");
                sw.WriteLine("TEXT 414,49,\"3\",180,1,1,\"" + StrPktSrNo + "\"");
                sw.WriteLine("TEXT 325,94,\"2\",180,1,1,\"" + StrShpBlnCts + "\"");
                sw.WriteLine("TEXT 73,94,\"2\",180,1,1,\"" + DateTime.Now.ToString("dd-MM") + "\"");
                sw.WriteLine("TEXT 325,18,\"2\",180,1,1,\"" + StrParameterAmt + "\"");
                sw.WriteLine("PRINT 1,1");
                sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }



        public static void BarcodePrintTest(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string MarkerCode, string pStrFileLoc)
        {
            try
            {


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodePrintForPCN(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string MarkerCode, string StrRefKapanName, string RefPacketNo, string RefTag)
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;
                string StrPrint = StrKapanName + "-" + PacketNo + "-" + Tag;
                string StrRefPrint = StrRefKapanName + "-" + RefPacketNo + "-" + RefTag;
                string AXONE = "AXONE";
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    //sw.WriteLine("I8,A");
                    //sw.WriteLine("ZN");
                    //sw.WriteLine("q400");
                    //sw.WriteLine("O");
                    //sw.WriteLine("JF");
                    //sw.WriteLine("KIZZQ0");
                    //sw.WriteLine("KI9+0.0");
                    //sw.WriteLine("ZT");
                    //sw.WriteLine("Q120,25");
                    //sw.WriteLine("Arglabel 150 31");
                    //sw.WriteLine("exit");
                    //sw.WriteLine("KI80");
                    //sw.WriteLine("N");
                    //sw.WriteLine("B295,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("A295,104,2,1,1,1,N,\"" + StrPrint + "\"");
                    //sw.WriteLine("A375,91,2,1,2,2,N,\""+AXONE+"\"");
                    //sw.WriteLine("A135,109,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A359,53,2,1,1,1,N,\"" + MarkerCode + "\"");
                    //sw.WriteLine("A183,21,2,1,1,1,N,\"" + Carat + "\"");
                    //sw.WriteLine("P1");

                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q400");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q120,25");
                    sw.WriteLine("Arglabel 150 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("KI80");
                    sw.WriteLine("N");
                    sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    sw.WriteLine("A351,111,2,3,1,1,N,\"" + StrPrint + "\"");
                    //sw.WriteLine("A111,109,2,1,1,1,N,\"" + Date + "\"");
                    sw.WriteLine("A111,109,2,1,1,1,N,\"" + StrRefPrint + "\"");
                    sw.WriteLine("A343,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                    sw.WriteLine("A111,28,2,3,1,1,N,\"" + Carat + "\"");
                    sw.WriteLine("P1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    //Global.Message("Ready For Print");
                    //ProcessStartInfo proc = new ProcessStartInfo();
                    //proc.CreateNoWindow = false;
                    //proc.UseShellExecute = true;
                    //proc.WorkingDirectory = Application.StartupPath + "\\";
                    //proc.FileName = "PRINTBARCODE.BAT";
                    //proc.Arguments = fileLoc;
                    //proc.Verb = "runas";
                    //try
                    //{
                    //    System.Diagnostics.Process P = new System.Diagnostics.Process();
                    //    P.StartInfo = proc;
                    //    P.Start();
                    //    Global.Message("Ready For Print dONE");
                    //}
                    //catch (Exception EX)
                    //{
                    //    Global.Message(EX.Message);
                    //}

                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BarcodeBombayGrdPrint(string StrKapanName, string PacketNo, string Tag, string Date, string Carat, string MarkerCode, string mShape,
                                                  string mColor, string mClarity, string mCut, string mPol, string mSym, string mFL,
                                                  string StrDiaMin, string StrDiaMax, string StrHeight, string StrReportNo, string StrHeliumRatio, string StrTableDepth, string Strtable) // Add : Pinali : 12-08-2019
        {
            try
            {
                string fileLoc = Application.StartupPath + "\\PrintBarcodeData.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                string StrBarcode = StrKapanName + Environment.NewLine + PacketNo + Environment.NewLine + Tag;
                string StrPrint = StrKapanName + "-" + PacketNo + "-" + Tag;

                string StrParam = "";
                StrParam = mColor + "-" + mClarity + "-" + mCut + "-" + mPol + "-" + mSym + "-" + mFL + "" + " " + StrTableDepth + "/" + Strtable;

                if (!StrReportNo.ToString().Trim().Equals(string.Empty))
                    StrParam = StrParam + "-" + StrReportNo;

                string AXONE = "AXONE";
                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    //sw.WriteLine("I8,A");
                    //sw.WriteLine("ZN");
                    //sw.WriteLine("q400");
                    //sw.WriteLine("O");
                    //sw.WriteLine("JF");
                    //sw.WriteLine("KIZZQ0");
                    //sw.WriteLine("KI9+0.0");
                    //sw.WriteLine("ZT");
                    //sw.WriteLine("Q120,25");
                    //sw.WriteLine("Arglabel 150 31");
                    //sw.WriteLine("exit");
                    //sw.WriteLine("KI80");
                    //sw.WriteLine("N");
                    //sw.WriteLine("B380,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    ////sw.WriteLine("A85,65,2,2,1,1,N,\"" + mShape + "\"");
                    //sw.WriteLine("A45,65,2,2,1,1,N,\"" + mShape + "\"");
                    //sw.WriteLine("A380,111,2,3,1,1,N,\"" + StrPrint + "\"");
                    ////sw.WriteLine("A221,109,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A200,109,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A100,109,2,3,1,1,N,\"" + Carat + "\"");
                    //sw.WriteLine("A380,20,2,2,1,1,N,\"" + StrParam + "\"");
                    //sw.WriteLine("P1");

                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q400");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q120,25");
                    sw.WriteLine("Arglabel 150 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("KI80");
                    sw.WriteLine("N");
                    sw.WriteLine("B380,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    //sw.WriteLine("A45,65,2,2,1,1,N,\"" + mShape + "\"");
                    //sw.WriteLine("A380,111,2,3,1,1,N,\"" + StrPrint + "\"");
                    //sw.WriteLine("A200,109,2,1,1,1,N,\"" + Date + "\"");
                    //sw.WriteLine("A100,109,2,3,1,1,N,\"" + Carat + "\"");

                    //sw.WriteLine("A45,50,2,2,1,1,N,\"" + StrDiaMin + "\"");
                    //sw.WriteLine("A45,60,2,2,1,1,N,\"" + StrDiaMax + "\"");
                    //sw.WriteLine("A45,70,2,2,1,1,N,\"" + StrHeight + "\"");
                    sw.WriteLine("A50,87,2,2,1,1,N,\"" + StrDiaMin + "\"");
                    sw.WriteLine("A50,67,2,2,1,1,N,\"" + StrDiaMax + "\"");
                    sw.WriteLine("A50,47,2,2,1,1,N,\"" + StrHeight + "\"");
                    sw.WriteLine("A380,111,2,3,1,1,N,\"" + StrPrint + "\"");
                    sw.WriteLine("A200,109,2,3,1,1,N,\"" + Carat + "\"");
                    sw.WriteLine("A100,109,2,3,1,1,N,\"" + mShape + "\"");
                    sw.WriteLine("A60,109,2,3,1,1,N,\"" + StrHeliumRatio + "\"");

                    sw.WriteLine("A380,20,2,2,1,1,N,\"" + StrParam + "\"");
                    sw.WriteLine("P1");

                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void CertificateBarcodePrint(DataRow Dr)
        {
            try
            {
                AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
                string fileLoc = Application.StartupPath + "\\PrintCertificateBarcodeData.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q400");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q120,25");
                    sw.WriteLine("Arglabel 150 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("KI80");
                    sw.WriteLine("N");
                    sw.WriteLine("B351,87,2,1,2,4,51,N,\"" + Val.ToString(Dr["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("A351,111,2,3,1,1,N,\"" + Val.ToString(Dr["PARTYSTOCKNO"]) + "\"");
                    sw.WriteLine("A145,109,2,3,1,1,N,\"" + "AXONE:" + Val.ToString(Dr["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("A343,28,2,3,1,1,N,\"" + Val.ToString(Dr["LABREPORTNO"]) + "\"");
                    sw.WriteLine("A145,28,2,3,1,1,N,\"" + Val.ToString(Dr["EXPORTORDERNO"]) + "\"");
                    sw.WriteLine("P1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        public static void BombayPrintBarcodePrint(DataRow DRow) //#P : 15-06-2020 : Old
        {
            AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();

            try
            {
                // First page

                string fileLoc = Application.StartupPath + "\\PrintBarcodeDataBombay.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q548");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q288,B25");
                    sw.WriteLine("Arglabel 360 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("N");

                    sw.WriteLine("B524,269,2,1,2,4,51,N,\"" + Val.ToString(DRow["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("A218,272,2,3,1,1,N,\"AXONE\"");
                    sw.WriteLine("A173,273,2,3,1,1,N,\":" + Val.ToString(DRow["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("A218,241,2,3,1,1,N,\"ID\"");
                    sw.WriteLine("A173,240,2,3,1,1,N,\":" + Val.ToString(DRow["PARTYSTOCKNO"]) + "\"");
                    sw.WriteLine("A524,78,2,3,1,1,N,\"" + Val.ToString(DRow["LABNAME"]) + "\"");
                    sw.WriteLine("A463,78,2,3,1,1,N,\":" + Val.ToString(DRow["LABREPORTNO"]) + "\"");
                    sw.WriteLine("A170,111,2,3,1,1,N,\"FL\"");
                    sw.WriteLine("A120,111,2,3,1,1,N,\":" + Val.ToString(DRow["FLNAME"]) + "\"");
                    sw.WriteLine("A524,46,2,3,1,1,N,\"MEAS\"");
                    sw.WriteLine("A463,46,2,3,1,1,N,\":" + Val.ToString(DRow["MEASUREMENT"]) + "\"");
                    sw.WriteLine("A463,168,2,3,1,1,N,\":" + Val.ToString(DRow["CARAT"]) + "\"");
                    sw.WriteLine("A524,168,2,3,1,1,N,\"CTS\"");
                    sw.WriteLine("A170,198,2,3,1,1,N,\"CUT\"");
                    sw.WriteLine("A122,198,2,3,1,1,N,\":" + Val.ToString(DRow["CUTNAME"]) + "\"");
                    sw.WriteLine("A122,168,2,3,1,1,N,\":" + Val.ToString(DRow["POLNAME"]) + "\"");
                    sw.WriteLine("A170,168,2,3,1,1,N,\"POL\"");
                    sw.WriteLine("A122,139,2,3,1,1,N,\":" + Val.ToString(DRow["SYMNAME"]) + "\"");
                    sw.WriteLine("A170,139,2,3,1,1,N,\"SYM\"");
                    sw.WriteLine("A170,78,2,3,1,1,N,\"TD%\"");
                    sw.WriteLine("A120,78,2,3,1,1,N,\":" + Val.ToString(DRow["DEPTHPER"]) + "\"");
                    sw.WriteLine("A118,46,2,3,1,1,N,\":" + Val.ToString(DRow["TABLEPER"]) + "\"");
                    sw.WriteLine("A170,46,2,3,1,1,N,\"TB%\"");
                    sw.WriteLine("LO2,207,543,2");
                    sw.WriteLine("A524,198,2,3,1,1,N,\"SHP\"");
                    sw.WriteLine("A463,198,2,3,1,1,N,\":" + Val.ToString(DRow["SHAPENAME"]) + "\"");
                    sw.WriteLine("A524,139,2,3,1,1,N,\"COL\"");
                    sw.WriteLine("A463,139,2,3,1,1,N,\":" + Val.ToString(DRow["COLORNAME"]) + "\"");
                    sw.WriteLine("A524,111,2,3,1,1,N,\"CLA\"");
                    sw.WriteLine("A463,111,2,3,1,1,N,\":" + Val.ToString(DRow["CLARITYNAME"]) + "\"");
                    sw.WriteLine("P1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);


                fileLoc = Application.StartupPath + "\\PrintBarcodeDataBombay1.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q548");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q288,B25");
                    sw.WriteLine("Arglabel 360 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("N");

                    sw.WriteLine("LO275,156,2,129");
                    sw.WriteLine("A525,272,2,3,1,1,N,\"BLACK TABLE\"");
                    sw.WriteLine("A358,272,2,3,1,1,N,\":" + Val.ToString(DRow["BLACKTABLE"]) + "\"");
                    sw.WriteLine("A525,245,2,3,1,1,N,\"BLACK CROWN\"");
                    sw.WriteLine("A358,244,2,3,1,1,N,\":" + Val.ToString(DRow["BLACKCROWN"]) + "\"");
                    sw.WriteLine("A525,215,2,3,1,1,N,\"WHITE TABLE\"");
                    sw.WriteLine("A358,215,2,3,1,1,N,\":" + Val.ToString(DRow["WHITETABLE"]) + "\"");
                    sw.WriteLine("A358,185,2,3,1,1,N,\":" + Val.ToString(DRow["WHITECROWN"]) + "\"");
                    sw.WriteLine("A249,272,2,3,1,1,N,\"TABLE OPEN\"");
                    sw.WriteLine("A99,272,2,3,1,1,N,\":" + Val.ToString(DRow["TABLEOPEN"]) + "\"");
                    sw.WriteLine("A249,244,2,3,1,1,N,\"CROWN OPEN\"");
                    sw.WriteLine("A99,244,2,3,1,1,N,\":" + Val.ToString(DRow["CROWNOPEN"]) + "\"");
                    sw.WriteLine("A249,215,2,3,1,1,N,\"PAV OPEN\"");
                    sw.WriteLine("A99,215,2,3,1,1,N,\":" + Val.ToString(DRow["PAVILLIONOPEN"]) + "\"");
                    sw.WriteLine("A249,185,2,3,1,1,N,\"GIRDLE\"");
                    sw.WriteLine("A525,146,2,3,1,1,N,\"RATIO\"");
                    sw.WriteLine("A448,146,2,3,1,1,N,\":" + Val.ToString(DRow["RATIO"]) + "\"");
                    sw.WriteLine("A317,146,2,3,1,1,N,\"CA\"");
                    sw.WriteLine("A274,146,2,3,1,1,N,\":" + Val.ToString(DRow["CRANGLE"]) + "\"");
                    sw.WriteLine("A154,146,2,3,1,1,N,\"PA\"");
                    sw.WriteLine("A274,118,2,3,1,1,N,\":" + Val.ToString(DRow["STARLENGTH"]) + "\"");
                    sw.WriteLine("A317,118,2,3,1,1,N,\"ST\"");
                    sw.WriteLine("A525,118,2,3,1,1,N,\"LH\"");
                    sw.WriteLine("A448,118,2,3,1,1,N,\":" + Val.ToString(DRow["LOWERHALF"]) + "\"");
                    sw.WriteLine("A525,85,2,3,1,1,N,\"BGM\"");
                    sw.WriteLine("A448,85,2,3,1,1,N,\":" + Val.ToString(DRow["BGM"]) + "\"");
                    sw.WriteLine("A154,85,2,3,1,1,N,\"CS\"");
                    sw.WriteLine("A317,85,2,3,1,1,N,\"LUS\"");
                    sw.WriteLine("A274,85,2,3,1,1,N,\":" + Val.ToString(DRow["LUSTERNAME"]) + "\"");
                    sw.WriteLine("A525,59,2,3,1,1,N,\"MILKY\"");
                    sw.WriteLine("A448,59,2,3,1,1,N,\":" + Val.ToString(DRow["MILKYNAME"]) + "\"");
                    sw.WriteLine("A274,59,2,3,1,1,N,\":" + Val.ToString(DRow["EYECLEANNAME"]) + "\"");
                    sw.WriteLine("A317,59,2,3,1,1,N,\"EC\"");
                    sw.WriteLine("A154,59,2,3,1,1,N,\"HA\"");
                    sw.WriteLine("A124,59,2,3,1,1,N,\":" + Val.ToString(DRow["HANAME"]) + "\"");
                    sw.WriteLine("A326,29,2,2,1,1,N,\"AXONE.WORLD\"");
                    sw.WriteLine("A525,185,2,3,1,1,N,\"WHITE CROWN\"");
                    sw.WriteLine("LO0,155,545,2");
                    sw.WriteLine("A155,185,2,3,1,1,N,\":" + Val.ToString(DRow["GIRDLEDESC"]) + "\"");
                    sw.WriteLine("A124,146,2,3,1,1,N,\":" + Val.ToString(DRow["PAVANGLE"]) + "\"");
                    sw.WriteLine("A124,85,2,3,1,1,N,\":" + Val.ToString(DRow["COLORSHADENAME"]) + "\"");
                    sw.WriteLine("LO0,91,545,2");
                    sw.WriteLine("LO0,34,545,2");
                    sw.WriteLine("P1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static void BombayPrintBarcodePrintTSC(DataRow DRow) //#MILAN :27-02-2021 
        {
            AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
            try
            {
                // First page
                string fileLoc = Application.StartupPath + "\\PrintBarcodeDataBombayTSC.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                StreamWriter sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    sw.WriteLine("SIZE 65 mm, 35 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("SET TEAR ON");
                    sw.WriteLine("BAR 519,210, 1, 3");
                    sw.WriteLine("BAR 1,201, 518, 3");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 505,192,\"2\",180,1,1,\"SHP\"");
                    sw.WriteLine("TEXT 460,192,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 438,192,\"2\",180,1,1,\"" + Val.ToString(DRow["SHAPENAME"]) + "\"");
                    sw.WriteLine("TEXT 505,160,\"2\",180,1,1,\"CTS\"");
                    sw.WriteLine("TEXT 460,160,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 438,160,\"2\",180,1,1,\"" + Val.ToString(DRow["CARAT"]) + "\"");
                    sw.WriteLine("TEXT 505,128,\"2\",180,1,1,\"COL\"");
                    sw.WriteLine("TEXT 459,128,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 438,128,\"2\",180,1,1,\"" + Val.ToString(DRow["COLORNAME"]) + "\"");
                    sw.WriteLine("TEXT 505,98,\"2\",180,1,1,\"CLA\"");
                    sw.WriteLine("TEXT 460,95,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 438,98,\"2\",180,1,1,\"" + Val.ToString(DRow["CLARITYNAME"]) + "\"");
                    //sw.WriteLine("TEXT 505,67,\"2\",180,1,1,\"GIA\"");
                    sw.WriteLine("TEXT 505,67,\"2\",180,1,1,\"" + Val.ToString(DRow["LABNAME"]) + "\"");
                    sw.WriteLine("TEXT 460,67,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 438,67,\"2\",180,1,1,\"" + Val.ToString(DRow["LABREPORTNO"]) + "\""); //+ Val.ToString(DRow[""]) + 
                    sw.WriteLine("TEXT 513,37,\"2\",180,1,1,\"MEAS\"");
                    sw.WriteLine("TEXT 460,37,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 446,37,\"2\",180,1,1,\"" + Val.ToString(DRow["MEASUREMENT"]) + "\"");
                    sw.WriteLine("TEXT 201,192,\"2\",180,1,1,\"CUT\"");
                    sw.WriteLine("TEXT 201,160,\"2\",180,1,1,\"POL\"");
                    sw.WriteLine("TEXT 201,128,\"2\",180,1,1,\"SYM\"");
                    sw.WriteLine("TEXT 201,98,\"2\",180,1,1,\"FL\"");
                    sw.WriteLine("TEXT 201,67,\"2\",180,1,1,\"TD%\"");
                    sw.WriteLine("TEXT 201,39,\"2\",180,1,1,\"TB%\"");
                    sw.WriteLine("TEXT 154,98,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 154,128,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 154,192,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 154,160,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 154,67,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 154,39,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 140,192,\"2\",180,1,1,\"" + Val.ToString(DRow["CUTNAME"]) + "\"");
                    sw.WriteLine("TEXT 138,160,\"2\",180,1,1,\"" + Val.ToString(DRow["POLNAME"]) + "\"");
                    sw.WriteLine("TEXT 138,128,\"2\",180,1,1,\"" + Val.ToString(DRow["SYMNAME"]) + "\"");
                    sw.WriteLine("TEXT 137,98,\"2\",180,1,1,\"" + Val.ToString(DRow["FLNAME"]) + "\"");
                    sw.WriteLine("TEXT 138,67,\"2\",180,1,1,\"" + Val.ToString(DRow["DEPTHPER"]) + "\"");
                    sw.WriteLine("TEXT 138,39,\"2\",180,1,1,\"" + Val.ToString(DRow["TABLEPER"]) + "\"");
                    sw.WriteLine("TEXT 223,258,\"2\",180,1,1,\"AXONE\"");
                    sw.WriteLine("TEXT 188,258,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 174,258,\"2\",180,1,1,\"" + Val.ToString(DRow["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("TEXT 223,227,\"2\",180,1,1,\"ID\"");
                    sw.WriteLine("TEXT 188,227,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 174,227,\"2\",180,1,1,\"" + Val.ToString(DRow["PARTYSTOCKNO"]) + "\"");

                    sw.WriteLine("BARCODE 503,253,\"128M\",36,0,180,2,4,\"" + Val.ToString(DRow["GENERATESERIALNO"]) + "\"");
                    //sw.WriteLine("BARCODE 505,253,\"128M\",36,0,180,1,2,\"!105" + Val.ToString(DRow["SERIALNO"]) + "\"");
                    //sw.WriteLine("BARCODE 505,253,\"CODA\",36,0,180,2,5,\"" + Val.ToString(DRow["SERIALNO"]) + "\"");
                    //sw.WriteLine("BARCODE 505,257,\"128M\",45,0,180,3,6,\"!105"+"00017293"+ "\""); //Val.ToString(DRow["SERIALNO"])
                    //sw.WriteLine("TEXT 435,232,\"2\",180,1,1,\"12345678\"");
                    sw.WriteLine("PRINT 1,1");
                    //	sw.WriteLine("PRINT 1,1");
                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);


                fileLoc = Application.StartupPath + "\\PrintBarcodeDataBombay1TSC.txt";
                if (System.IO.File.Exists(fileLoc) == true)
                {
                    System.IO.File.Delete(fileLoc);
                }

                System.IO.File.Create(fileLoc).Dispose();

                sw = new StreamWriter(fileLoc);
                using (sw)
                {
                    sw.WriteLine("SIZE 65 mm, 35 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("SET TEAR ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 508,262,\"2\",180,1,1,\"BLACK\"");
                    sw.WriteLine("TEXT 425,262,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 508,184,\"2\",180,1,1,\"WHITE\"");
                    sw.WriteLine("TEXT 508,211,\"2\",180,1,1,\"WHITE\"");
                    sw.WriteLine("TEXT 508,238,\"2\",180,1,1,\"BLACK\"");
                    sw.WriteLine("TEXT 423,184,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 425,211,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 423,238,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 335,262,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,211,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,184,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,238,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 320,262,\"2\",180,1,1,\"" + Val.ToString(DRow["BLACKTABLE"]) + "\"");
                    sw.WriteLine("TEXT 321,238,\"2\",180,1,1,\"" + Val.ToString(DRow["BLACKCROWN"]) + "\"");
                    sw.WriteLine("TEXT 321,211,\"2\",180,1,1,\"" + Val.ToString(DRow["WHITETABLE"]) + "\"");
                    sw.WriteLine("TEXT 321,184,\"2\",180,1,1,\"" + Val.ToString(DRow["WHITECROWN"]) + "\"");
                    sw.WriteLine("BAR 255,144, 3, 135");
                    sw.WriteLine("BAR 255,145, 264, 3");
                    sw.WriteLine("BAR 2,145, 254, 3");
                    sw.WriteLine("TEXT 243,262,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 243,238,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 241,211,\"2\",180,1,1,\"PAV\"");
                    sw.WriteLine("TEXT 243,184,\"2\",180,1,1,\"GIRDLE\"");
                    sw.WriteLine("TEXT 155,262,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 155,238,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 192,211,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 95,238,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 95,211,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 95,262,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 155,184,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 78,262,\"2\",180,1,1,\"" + Val.ToString(DRow["TABLEOPEN"]) + "\"");
                    sw.WriteLine("TEXT 78,211,\"2\",180,1,1,\"" + Val.ToString(DRow["PAVILLIONOPEN"]) + "\"");
                    sw.WriteLine("TEXT 78,238,\"2\",180,1,1,\"" + Val.ToString(DRow["CROWNOPEN"]) + "\"");
                    sw.WriteLine("TEXT 141,184,\"2\",180,1,1,\"" + Val.ToString(DRow["GIRDLEDESC"]) + "\"");
                    sw.WriteLine("BAR 1,87, 518, 3");
                    sw.WriteLine("BAR 1,29, 518, 3");
                    sw.WriteLine("TEXT 498,139,\"2\",180,1,1,\"RATIO\"");
                    sw.WriteLine("TEXT 429,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 417,139,\"2\",180,1,1,\"" + Val.ToString(DRow["RATIO"]) + "\"");
                    sw.WriteLine("TEXT 429,108,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 498,108,\"2\",180,1,1,\"LH\"");
                    sw.WriteLine("TEXT 417,108,\"2\",180,1,1,\"" + Val.ToString(DRow["LOWERHALF"]) + "\"");
                    sw.WriteLine("TEXT 297,140,\"2\",180,1,1,\"CA\"");
                    sw.WriteLine("TEXT 300,108,\"2\",180,1,1,\"ST\"");
                    sw.WriteLine("TEXT 263,108,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 263,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 246,139,\"2\",180,1,1,\"" + Val.ToString(DRow["CRANGLE"]) + "\"");
                    sw.WriteLine("TEXT 246,108,\"2\",180,1,1,\"" + Val.ToString(DRow["STARLENGTH"]) + "\"");
                    sw.WriteLine("TEXT 146,139,\"2\",180,1,1,\"PA\"");
                    sw.WriteLine("TEXT 113,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 99,139,\"2\",180,1,1,\"" + Val.ToString(DRow["PAVANGLE"]) + "\"");
                    sw.WriteLine("TEXT 497,81,\"2\",180,1,1,\"BGM\"");
                    sw.WriteLine("TEXT 429,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 498,52,\"2\",180,1,1,\"MI\"");
                    sw.WriteLine("TEXT 429,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 417,81,\"2\",180,1,1,\"" + Val.ToString(DRow["BGM"]) + "\"");
                    sw.WriteLine("TEXT 416,52,\"2\",180,1,1,\"" + Val.ToString(DRow["MILKYNAME"]) + "\"");
                    sw.WriteLine("TEXT 313,81,\"2\",180,1,1,\"LUS\"");
                    sw.WriteLine("TEXT 299,52,\"2\",180,1,1,\"EC\"");
                    sw.WriteLine("TEXT 263,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 263,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 246,81,\"2\",180,1,1,\"" + Val.ToString(DRow["LUSTERNAME"]) + "\"");
                    sw.WriteLine("TEXT 246,52,\"2\",180,1,1,\"" + Val.ToString(DRow["EYECLEANNAME"]) + "\"");
                    sw.WriteLine("TEXT 146,81,\"2\",180,1,1,\"CS\"");
                    sw.WriteLine("TEXT 113,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 146,52,\"2\",180,1,1,\"HA\"");
                    sw.WriteLine("TEXT 113,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 99,81,\"2\",180,1,1,\"" + Val.ToString(DRow["COLORSHADENAME"]) + "\"");
                    sw.WriteLine("TEXT 98,52,\"2\",180,1,1,\"" + Val.ToString(DRow["HANAME"]) + "\"");
                    sw.WriteLine("TEXT 315,20,\"2\",180,1,1,\"AXONE.WORLD\"");
                    sw.WriteLine("TEXT 146,108,\"2\",180,1,1,\"" + Val.ToString(DRow["GENERATESERIALNO"]) + "\"");
                    sw.WriteLine("PRINT 1,1");

                    //sw.WriteLine("PRINT 1,1");


                    /*sw.WriteLine("SIZE 65 mm, 35 mm");
                    sw.WriteLine("GAP 3 mm, 0 mm");
                    sw.WriteLine("DIRECTION 0,0");
                    sw.WriteLine("REFERENCE 0,0");
                    sw.WriteLine("OFFSET 0 mm");
                    sw.WriteLine("SET PEEL OFF");
                    sw.WriteLine("SET CUTTER OFF");
                    sw.WriteLine("SET PARTIAL_CUTTER OFF");
                    sw.WriteLine("SET TEAR ON");
                    sw.WriteLine("CLS");
                    sw.WriteLine("CODEPAGE 1252");
                    sw.WriteLine("TEXT 508,262,\"2\",180,1,1,\"BLACK\"");
                    sw.WriteLine("TEXT 425,262,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 508,184,\"2\",180,1,1,\"WHITE\"");
                    sw.WriteLine("TEXT 508,211,\"2\",180,1,1,\"WHITE\"");
                    sw.WriteLine("TEXT 508,238,\"2\",180,1,1,\"BLACK\"");
                    sw.WriteLine("TEXT 423,184,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 425,211,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 423,238,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 335,262,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,211,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,184,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 335,238,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 321,262,\"2\",180,1,1,\"" + Val.ToString(DRow["BLACKTABLE"]) + "\"");
                    sw.WriteLine("TEXT 321,238,\"2\",180,1,1,\""+Val.ToString(DRow["BLACKCROWN"])+"\"");
                    sw.WriteLine("TEXT 321,211,\"2\",180,1,1,\"" + Val.ToString(DRow["WHITETABLE"]) + "\"");
                    sw.WriteLine("TEXT 321,184,\"2\",180,1,1,\"" + Val.ToString(DRow["WHITECROWN"]) + "\"");
                    sw.WriteLine("BAR 255,144, 3, 135");
                    sw.WriteLine("BAR 255,145, 264, 3");
                    sw.WriteLine("BAR 2,145, 254, 3");
                    sw.WriteLine("TEXT 243,262,\"2\",180,1,1,\"TABLE\"");
                    sw.WriteLine("TEXT 243,238,\"2\",180,1,1,\"CROWN\"");
                    sw.WriteLine("TEXT 241,211,\"2\",180,1,1,\"PAV\"");
                    sw.WriteLine("TEXT 243,184,\"2\",180,1,1,\"GIRDLE\"");
                    sw.WriteLine("TEXT 155,262,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 155,238,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 192,211,\"2\",180,1,1,\"OPEN\"");
                    sw.WriteLine("TEXT 95,238,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 95,211,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 95,262,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 155,184,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 81,262,\"2\",180,1,1,\"" + Val.ToString(DRow["TABLEOPEN"]) + "\"");
                    sw.WriteLine("TEXT 81,211,\"2\",180,1,1,\"" + Val.ToString(DRow["CROWNOPEN"]) + "\"");
                    sw.WriteLine("TEXT 81,238,\"2\",180,1,1,\"" + Val.ToString(DRow["PAVILLIONOPEN"]) + "\"");
                    sw.WriteLine("TEXT 137,184,\"2\",180,1,1,\"" + Val.ToString(DRow["GIRDLEDESC"]) + "\"");
                    sw.WriteLine("BAR 1,87, 518, 3");
                    sw.WriteLine("BAR 1,29, 518, 3");
                    sw.WriteLine("TEXT 498,139,\"2\",180,1,1,\"RATIO\"");
                    sw.WriteLine("TEXT 429,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 417,139,\"2\",180,1,1,\"" + Val.ToString(DRow["RATIO"]) + "\"");
                    sw.WriteLine("TEXT 429,108,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 498,108,\"2\",180,1,1,\"LH\"");
                    sw.WriteLine("TEXT 417,108,\"2\",180,1,1,\"" + Val.ToString(DRow["LOWERHALF"]) + "\"");
                    sw.WriteLine("TEXT 297,140,\"2\",180,1,1,\"CA\"");
                    sw.WriteLine("TEXT 300,108,\"2\",180,1,1,\"ST\"");
                    sw.WriteLine("TEXT 263,108,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 263,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 246,139,\"2\",180,1,1,\"" + Val.ToString(DRow["CRANGLE"]) + "\"");
                    sw.WriteLine("TEXT 246,108,\"2\",180,1,1,\"" + Val.ToString(DRow["STARLENGTH"]) + "\"");
                    sw.WriteLine("TEXT 146,139,\"2\",180,1,1,\"PA\"");
                    sw.WriteLine("TEXT 113,139,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 99,139,\"2\",180,1,1,\"" + Val.ToString(DRow["PAVANGLE"]) + "\"");
                    sw.WriteLine("TEXT 497,81,\"2\",180,1,1,\"BGM\"");
                    sw.WriteLine("TEXT 429,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 498,52,\"2\",180,1,1,\"MI\"");
                    sw.WriteLine("TEXT 429,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 417,81,\"2\",180,1,1,\"" + Val.ToString(DRow["BGM"]) + "\"");
                    sw.WriteLine("TEXT 416,52,\"2\",180,1,1,\"" + Val.ToString(DRow["MILKYNAME"]) + "\"");
                    sw.WriteLine("TEXT 313,81,\"2\",180,1,1,\"LUS\"");
                    sw.WriteLine("TEXT 299,52,\"2\",180,1,1,\"EC\"");
                    sw.WriteLine("TEXT 263,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 263,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 246,81,\"2\",180,1,1,\"" + Val.ToString(DRow["LUSTERNAME"]) + "\"");
                    sw.WriteLine("TEXT 246,52,\"2\",180,1,1,\"" + Val.ToString(DRow["EYECLEANNAME"]) + "\"");
                    sw.WriteLine("TEXT 146,81,\"2\",180,1,1,\"CS\"");
                    sw.WriteLine("TEXT 113,81,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 146,52,\"2\",180,1,1,\"HA\"");
                    sw.WriteLine("TEXT 113,52,\"2\",180,1,1,\":\"");
                    sw.WriteLine("TEXT 99,81,\"2\",180,1,1,\"" + Val.ToString(DRow["COLORSHADENAME"]) + "\"");
                    sw.WriteLine("TEXT 99,52,\"2\",180,1,1,\"" + Val.ToString(DRow["HANAME"]) + "\"");
                    sw.WriteLine("TEXT 315,20,\"2\",180,1,1,\"AXONE.WORLD\"");
                    sw.WriteLine("TEXT 146,108,\"2\",180,1,1,\"123456\"");
                    sw.WriteLine("P1");
                    //sw.WriteLine("PRINT 1,1");
                     * */

                }
                sw.Dispose();
                sw = null;
                if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
                {
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }

                Thread.Sleep(800);


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public static string NumbersToWords(int inputNumber)
        {
            int inputNo = inputNumber;

            if (inputNo == 0)
                return "Zero";

            int[] numbers = new int[4];
            int first = 0;
            int u, h, t;
            System.Text.StringBuilder sb = new System.Text.StringBuilder();

            if (inputNo < 0)
            {
                sb.Append("Minus ");
                inputNo = -inputNo;
            }

            string[] words0 = {"" ,"One ", "Two ", "Three ", "Four ",
        "Five " ,"Six ", "Seven ", "Eight ", "Nine "};
            string[] words1 = {"Ten ", "Eleven ", "Twelve ", "Thirteen ", "Fourteen ",
        "Fifteen ","Sixteen ","Seventeen ","Eighteen ", "Nineteen "};
            string[] words2 = {"Twenty ", "Thirty ", "Forty ", "Fifty ", "Sixty ",
        "Seventy ","Eighty ", "Ninety "};
            string[] words3 = { "Thousand ", "Lakh ", "Crore " };

            numbers[0] = inputNo % 1000; // units
            numbers[1] = inputNo / 1000;
            numbers[2] = inputNo / 100000;
            numbers[1] = numbers[1] - 100 * numbers[2]; // thousands
            numbers[3] = inputNo / 10000000; // crores
            numbers[2] = numbers[2] - 100 * numbers[3]; // lakhs

            for (int i = 3; i > 0; i--)
            {
                if (numbers[i] != 0)
                {
                    first = i;
                    break;
                }
            }
            for (int i = first; i >= 0; i--)
            {
                if (numbers[i] == 0) continue;
                u = numbers[i] % 10; // ones
                t = numbers[i] / 10;
                h = numbers[i] / 100; // hundreds
                t = t - 10 * h; // tens
                if (h > 0) sb.Append(words0[h] + "Hundred ");
                if (u > 0 || t > 0)
                {
                    if (h > 0 || i == 0) sb.Append("and ");
                    if (t == 0)
                        sb.Append(words0[u]);
                    else if (t == 1)
                        sb.Append(words1[u]);
                    else
                        sb.Append(words2[t - 2] + words0[u]);
                }
                if (i != 0) sb.Append(words3[i - 1]);
            }
            return sb.ToString().TrimEnd();

        }
        public static string FindLedgerClosingStr(Int64 pIntLedgerID)
        {
            double DouAmt = new BusLib.Account.BOLedgerTransaction().FindLedgerClosing(pIntLedgerID);
            if (DouAmt < 0)
            {
                return DouAmt.ToString() + " Dr";
            }
            else
            {
                return DouAmt.ToString() + " Cr";
            }
        }


        public static string GetFinancialYear(string pStrDate)
        {
            AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
            string StrReturn = "";
            DateTime DT = DateTime.Parse(pStrDate);
            if (DT.Month >= 4 && DT.Month <= 12)
            {
                StrReturn = Val.Right(DT.Year.ToString(), 2) + "" + Val.Right((DT.Year + 1).ToString(), 2);
            }
            else
            {
                StrReturn = Val.Right((DT.Year - 1).ToString(), 2) + "" + Val.Right((DT.Year).ToString(), 2);
            }

            Val = null;
            return StrReturn;
        }
        public static void OpenPopupParam(string StrTableName, KeyPressEventArgs e, AxonContLib.cTextBox txt, Int64 pIntParentID = 0)
        {
            AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
            if (OnKeyPressToOpenPopup(e))
            {
                FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                FrmSearchPopupBox.mSearchField = "SHORTNAME,PARAMVALUE";
                FrmSearchPopupBox.mDTab = new BOComboFill().FillCmb(StrTableName, pIntParentID);
                FrmSearchPopupBox.mAllowFirstColumnHide = true;
                FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                FrmSearchPopupBox.mColumnsToHide = "PARAM_ID";
                FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                e.Handled = true;
                FrmSearchPopupBox.ShowDialog();

                if (FrmSearchPopupBox.mDRow != null)
                {
                    txt.Tag = Val.ToString(FrmSearchPopupBox.mDRow["PARAM_ID"]);
                    txt.Text = Val.ToString(FrmSearchPopupBox.mDRow["PARAMVALUE"]);
                }
                FrmSearchPopupBox.Hide();
                FrmSearchPopupBox.Dispose();
                FrmSearchPopupBox = null;
                Val = null;
            }
        }
        public static bool OnKeyPressEveToPopup(KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Enter && e.KeyChar != (Char)Keys.Delete && e.KeyChar != (Char)Keys.Back
                && e.KeyChar != (Char)Keys.Left && e.KeyChar != (Char)Keys.Escape && e.KeyChar != (Char)Keys.Right
                && e.KeyChar != (Char)Keys.Tab && e.KeyChar != (Char)Keys.Up
                )
            {
                return true;
            }
            return false;
        }

        public static void ExcelExport(string pStrFileName, DevExpress.XtraGrid.Views.BandedGrid.BandedGridView pGrid)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    pGrid.ExportToXlsx(Filepath);

                    if (Global.Confirm("Do You Want To Open [" + pStrFileName + "] ?") == System.Windows.Forms.DialogResult.Yes)
                    {
                        System.Diagnostics.Process.Start(svDialog.FileName, "CMD");
                    }

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        public static void ExcelExport(string pStrFileName, DevExpress.XtraGrid.Views.Grid.GridView pGrid)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    pGrid.ExportToXlsx(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
        public static void ExcelExport(string pStrFileName, DevExpress.XtraPivotGrid.PivotGridControl pGrid)
        {
            try
            {
                SaveFileDialog svDialog = new SaveFileDialog();
                svDialog.DefaultExt = "xlsx";
                svDialog.Title = "Export to Excel";
                svDialog.FileName = pStrFileName;
                svDialog.Filter = "Excel files 2007(*.xlsx)|*.xlsx|All files (*.*)|*.*";
                if ((svDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK))
                {
                    string Filepath = svDialog.FileName;
                    pGrid.ExportToXlsx(Filepath);
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }



        public static bool OnKeyPressToOpenPopup(KeyPressEventArgs e)
        {
            if (e.KeyChar != (Char)Keys.Enter
                && e.KeyChar != (Char)Keys.Back
                && e.KeyChar != (Char)Keys.Escape
                && e.KeyChar != (Char)Keys.Delete
                && e.KeyChar != (Char)Keys.Left
                && e.KeyChar != (Char)Keys.Right
                && e.KeyChar != (Char)Keys.Up
                && e.KeyChar != (Char)Keys.Tab
                )
            {
                return true;
            }
            return false;
        }

        public static bool OnKeyPressToOpenPopup(KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter
                && e.KeyCode != Keys.Back
                && e.KeyCode != Keys.Escape
                && e.KeyCode != Keys.Delete
                && e.KeyCode != Keys.Left
                && e.KeyCode != Keys.Right
                && e.KeyCode != Keys.Up
                )
            {
                return true;
            }
            return false;
        }





        public static string Encrypt(string plainText)
        {
            try
            {
                byte[] plainTextBytes = Encoding.UTF8.GetBytes(plainText);

                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.Zeros };
                var encryptor = symmetricKey.CreateEncryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));

                byte[] cipherTextBytes;

                using (var memoryStream = new MemoryStream())
                {
                    using (var cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write))
                    {
                        cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
                        cryptoStream.FlushFinalBlock();
                        cipherTextBytes = memoryStream.ToArray();
                        cryptoStream.Close();
                    }
                    memoryStream.Close();
                }
                return Convert.ToBase64String(cipherTextBytes);
            }
            catch (Exception ex)
            {
                return "";
            }

        }


        public static string Decrypt(string encryptedText)
        {
            try
            {
                byte[] cipherTextBytes = Convert.FromBase64String(encryptedText);
                byte[] keyBytes = new Rfc2898DeriveBytes(PasswordHash, Encoding.ASCII.GetBytes(SaltKey)).GetBytes(256 / 8);
                var symmetricKey = new RijndaelManaged() { Mode = CipherMode.CBC, Padding = PaddingMode.None };

                var decryptor = symmetricKey.CreateDecryptor(keyBytes, Encoding.ASCII.GetBytes(VIKey));
                var memoryStream = new MemoryStream(cipherTextBytes);
                var cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];

                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount).TrimEnd("\0".ToCharArray());
            }
            catch (Exception ex)
            {
                return "";
            }

        }

        public static void MessageToster(string pStrMessage)
        {
            gStrMessage = pStrMessage;
        }

        public static void Message(string pStrMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage.ToUpper(), gStrCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Information);

            //FrmMessageBox FrmMessageBox = new FrmMessageBox();
            //FrmMessageBox.ShowForm(pStrMessage, gStrCompanyName, Utility.FrmMessageBox.CustomeMessageBoxIcon.General);
        }

        public static void MessageError(string pStrMessage)
        {
            DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage.ToUpper(), gStrCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage.ToUpper(), gStrCompanyName, MessageBoxButtons.OK, MessageBoxIcon.Error);
            //FrmMessageBox FrmMessageBox = new FrmMessageBox();
            //FrmMessageBox.ShowForm(pStrMessage, gStrCompanyName, Utility.FrmMessageBox.CustomeMessageBoxIcon.Warning);
            //FrmMessageBox.Dispose();
            //FrmMessageBox = null;
        }

        public static DialogResult Confirm(string pStrMessage)
        {
            //DialogResult DResult = DialogResult.No;
            //FrmMessageBoxConfirmation FrmMessageBoxConfirmation = new FrmMessageBoxConfirmation();
            //DResult = FrmMessageBoxConfirmation.ShowForm(pStrMessage, gStrCompanyName);
            //FrmMessageBoxConfirmation.Dispose();
            //FrmMessageBoxConfirmation = null;
            //return DResult;
            return DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage.ToUpper(), gStrCompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }

        public static DialogResult ConfirmPacketGrading(string pStrMessage)
        {
            //DialogResult DResult = DialogResult.No;
            //FrmMessageBoxConfirmation FrmMessageBoxConfirmation = new FrmMessageBoxConfirmation();
            //DResult = FrmMessageBoxConfirmation.ShowForm(pStrMessage, gStrCompanyName);
            //FrmMessageBoxConfirmation.Dispose();
            //FrmMessageBoxConfirmation = null;
            //return DResult;
            return DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage.ToUpper(), gStrCompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
        }


        public static DialogResult ConfirmWithRemark(string pStrMessage, ref string pStrRemarkref)
        {
            //DialogResult DResult = DialogResult.No;
            //FrmMessageBoxConfirmationWithRemark FrmMessageBoxConfirmationWithRemark = new FrmMessageBoxConfirmationWithRemark();
            //DResult = FrmMessageBoxConfirmationWithRemark.ShowForm(pStrMessage, gStrCompanyName);
            //pStrRemarkref = FrmMessageBoxConfirmationWithRemark.Remark;
            //FrmMessageBoxConfirmationWithRemark.Dispose();
            //FrmMessageBoxConfirmationWithRemark = null;
            //return DResult;
            return DevExpress.XtraEditors.XtraMessageBox.Show(pStrMessage, gStrCompanyName, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        }
        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true) //Get Excel Data Without Connection : 26-06-2019
        {
            using (var pck = new OfficeOpenXml.ExcelPackage())
            {
                using (var stream = File.OpenRead(path))
                {
                    pck.Load(stream);
                }
                var ws = pck.Workbook.Worksheets.First();
                DataTable tbl = new DataTable();
                foreach (var firstRowCell in ws.Cells[1, 1, 1, ws.Dimension.End.Column])
                {
                    if (firstRowCell.Text == string.Empty)
                        tbl.Columns.Add(hasHeader ? "A1" : string.Format("Column {0}", firstRowCell.Start.Column));
                    else
                        tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    //var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    var wsRow = ws.Cells[rowNum, 1, rowNum, tbl.Columns.Count];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }
        public static string ConvertIntoDaysHoursFormate(string pStrInp)
        {
            try
            {
                string DaysHours = "";
                int inp = int.Parse(pStrInp);
                int d = 0, h = 0, min = 0, se = 0;
                d = inp / 1440;
                inp = inp % 1440;
                if (inp > 59)
                {
                    h = inp / 60;
                    inp = inp % 60;
                    if (inp > 60)
                    {
                        min = inp / 60;
                        inp = inp % 60;
                        se = inp;
                    }
                    else
                    {
                        min = inp;
                    }

                }
                else
                {
                    h = inp;
                }

                if (d == 0)
                {
                    DaysHours = h.ToString() + " : " + min.ToString() + " : " + se.ToString();

                }
                else
                {
                    DaysHours = d.ToString() + " day " + h.ToString() + " : " + min.ToString() + " : " + se.ToString();
                }

                return DaysHours;

            }
            catch (Exception ex)
            {
                return "";
            }
        }
    }
}
