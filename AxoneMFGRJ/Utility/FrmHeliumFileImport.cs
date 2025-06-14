using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.Utility
{
    public partial class FrmHeliumFileImport : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOHEL_ColumnMaster ObjMast = new BOHEL_ColumnMaster();

        DataTable DTabHeliumPath = new DataTable();
        DataTable DTabStoneDetail = new DataTable();
    
        //FUNCTIONS FNC = new FUNCTIONS();

        public FrmHeliumFileImport()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            ObjPer.GetFormPermission(Val.ToString(this.Tag));         
            this.Show();
            TxtKapan.Focus();
            //GetHeliumFilePath();
        }
        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        public void GetHeliumFilePath()
        {
            try
            {
                DTabStoneDetail.Columns.Clear();
                DTabStoneDetail.Columns.Add("ID", typeof(System.String));
                DTabStoneDetail.Columns.Add("PATH", typeof(System.String));
                DTabStoneDetail.Columns.Add("STATUS", typeof(System.String));

                DTabHeliumPath.Columns.Add("PATH");
                foreach (string line in System.IO.File.ReadAllLines(Application.StartupPath + "\\Helium.Txt"))
                {
                    string o = line.ToString();
                    int p = o.IndexOf("=");
                    string q = o.Substring(0, o.IndexOf("="));
                    if (q == "Helium1 ")
                    {
                        DTabHeliumPath.Rows.Add(line.Substring((line.IndexOf("=") + 1)).ToString());
                    }
                    if (q == "Helium2 ")
                    {
                        DTabHeliumPath.Rows.Add(line.Substring((line.IndexOf("=") + 1)).ToString());
                    }
                    if (q == "Helium3 ")
                    {
                        DTabHeliumPath.Rows.Add(line.Substring((line.IndexOf("=") + 1)).ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void ADD_PATH(String YEAR, String MONTH, String DAY)
        {
            string year = YEAR;
            string month = MONTH;
            string day = DAY;

            for (int t = 0; t < DTabHeliumPath.Rows.Count; t++)
            {
                try
                {
                    if (Directory.Exists(DTabHeliumPath.Rows[t][0].ToString() + "\\" + year + "\\" + month + "\\" + day))
                    {
                        #region FOLDER CHE 
                        //string[] files1 = Directory.GetFiles(DTabHeliumPath.Rows[t][0].ToString() + "\\" + year + "\\" + month + "\\" + day);
                        //foreach (string file in files1)
                        //{

                        //    try
                        //    {

                        //        string VALUE = Path.GetFileNameWithoutExtension(file);
                        //        string[] words = VALUE.ToString().Split('-');
                        //        string KP = words[0].ToString();

                        //        if (KP == "DVC13")
                        //        {
                        //            System.IO.File.Move(file, file.Replace(VALUE, VALUE.Replace("DVC13", "C19")));
                        //        }
                        //        else if (KP == "DVC14")
                        //        {
                        //            System.IO.File.Move(file, file.Replace(VALUE, VALUE.Replace("DVC14", "C20")));
                        //        }

                        //        int K = Convert.ToInt32(KP.ToString());
                        //        if (K >= 0 && K <= 100)
                        //        {
                        //            System.IO.File.Move(file, file.Replace(VALUE, "B" + VALUE));
                        //        }
                        //    }
                        //    catch (Exception ex)
                        //    {

                        //    }
                        //    //Int64.Parse("2147483649");

                        //}
                        string[] files = Directory.GetFiles(DTabHeliumPath.Rows[t][0].ToString() + "\\" + year + "\\" + month + "\\" + day);

                        DataTable dt = new DataTable();
                        dt.Columns.Add("Helium_id");
                        dt.Columns.Add("HELIUM_PATH");
                        dt.Columns.Add("DB_ID");

                        int WORD_STATE = 1;
                        string WORD_STR = "";
                        foreach (string file in files)
                        {
                            string PureFileName = (new FileInfo(file).Name + "");
                            string VALUE = Path.GetFileNameWithoutExtension(file);
                            dt.Rows.Add(Path.GetFileNameWithoutExtension(file), file); // STONE ID 
                            string fileName = file; // FILE PATH

                            if (VALUE != "" && WORD_STATE == 1)
                            {
                                WORD_STR += "'" + VALUE + "',";
                                WORD_STATE = 0;
                            }
                            else if (VALUE != "" && WORD_STATE == 0)
                            {
                                //WORD_STR += "," + VALUE + "  ";
                                WORD_STR += "'" + VALUE + "',";
                            }
                        }
                        if (!string.IsNullOrEmpty(WORD_STR))
                        {
                            WORD_STR = WORD_STR.Substring(0, WORD_STR.Length - 1);
                        }
                        if (WORD_STR != "")
                        {
                            DataTable DT1 = new DataTable();

                            DT1 = ObjMast.HeliumData(WORD_STR);
                            Boolean status = false;
                            foreach (DataRow row in dt.Rows)
                            {
                                for (int K = 0; K < DT1.Rows.Count; K++)
                                {
                                    if (row["Helium_id"].ToString() == DT1.Rows[K]["ID"].ToString())
                                    {
                                        row["DB_ID"] = DT1.Rows[K]["ID"].ToString();
                                        status = true;
                                        break;
                                    }
                                    else
                                    {
                                        status = false;
                                    }                                   
                                }
                                if (status == false)
                                {
                                    DTabStoneDetail.Rows.Add(row["Helium_id"].ToString(), row["HELIUM_PATH"].ToString(), "False");
                                }
                            }
                                
                        }
                        #endregion

                        if (DTabStoneDetail.Rows.Count != 0)
                        {
                            var valueDistinctByIdColumn = DTabStoneDetail.AsEnumerable().Distinct(DataRowComparer.Default);
                            DataTable dtDistinctByIdColumn = valueDistinctByIdColumn.CopyToDataTable();
                            MainGridView.DataSource = dtDistinctByIdColumn;
                        }
                    }
                    else
                    {
                        // MessageBox.Show("PLEASE CREATE FOLDER" + DT_HELIUM_PATH.Rows[t][0].ToString() + year + "\\" + month + "\\" + day);
                    }
                }
                catch (Exception ex)
                {
                    Global.Message(ex.Message);
                }

            }
        }


        private static DataTable GetExcelSheetData(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;
            DataTable ExcelTable = new DataTable("Temp");

            try
            {
                String connString = "";
                if (Path.GetExtension(excelFile).Equals(".xls"))//for 97-03 Excel file
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.4.0;" +
                      "Data Source=" + excelFile + ";Extended Properties=Excel 8.0;";
                }
                else
                {
                    connString = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                                 "Data Source=" + excelFile + ";Extended Properties=Excel 12.0;";
                }

                objConn = new OleDbConnection(connString);
                objConn.Open();
                dt = objConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
               
                OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + dt.Rows[0]["TABLE_NAME"] + "]", objConn);
                cmd.CommandType = CommandType.Text;

                new OleDbDataAdapter(cmd).Fill(ExcelTable);
               
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
                return null;
            }
            finally
            {
                if (objConn != null)
                {
                    objConn.Close();
                    objConn.Dispose();
                }
                if (dt != null)
                {
                    dt.Dispose();
                }
            }
            return ExcelTable;
            
        }

        public void InsertInToTable()
        {
            for (int i = 0; i < GridDetail.RowCount; i++)
            {
                #region INSERT

                if (GridDetail.GetRowCellValue(i, "STATUS").ToString() == "False")
                {

                    if (GridDetail.GetRowCellValue(i, "PATH").ToString().Contains(".txt"))
                    {
                        #region .txt
                        try
                        {
                            string CUTTING_TYPE = "";
                            string MAIN_MEASURING_MM = "";
                            Decimal WEIGHT_CT = 0;
                            Decimal TOTAL_DEPTH_PC = 0;
                            Decimal DIAMETER_MMA = 0;
                            Decimal TABLE_PC = 0;
                            Decimal RATIO = 0;
                            string HEIGHT = "";
                            foreach (string line in System.IO.File.ReadAllLines(GridDetail.GetRowCellValue(i, "PATH").ToString()))
                            {

                                if (line.Contains("NumericalParameters"))// GETTING VALUE FROM .TXT FILE 
                                {
                                }
                                else
                                {
                                    string o = line.ToString();
                                    int p = o.IndexOf("=");
                                    string q = o.Substring(0, o.IndexOf("="));
                                    if (q == "CUTTING_TYPE")
                                    {
                                        CUTTING_TYPE = line.Substring((line.IndexOf("=") + 1)).ToString();
                                    }

                                    //if (q == "WEIGHT_CT") 
                                    if (q == "WEIGHT_4_CT")
                                    {
                                        WEIGHT_CT = decimal.Parse(line.Substring((line.IndexOf("=") + 1)));
                                    }

                                    if (q == "TOTAL_DEPTH_PC")
                                    {
                                        TOTAL_DEPTH_PC = decimal.Parse(line.Substring((line.IndexOf("=") + 1)));
                                    }

                                    if (q == "DIAMETER_MMA")
                                    {
                                        DIAMETER_MMA = decimal.Parse(line.Substring((line.IndexOf("=") + 1)));
                                    }

                                    if (q == "TABLE_PC")
                                    {
                                        TABLE_PC = decimal.Parse(line.Substring((line.IndexOf("=") + 1)));
                                    }

                                    if (q == "MAIN_MEASURING_MM")
                                    {
                                        string ss = line.Substring(line.IndexOf("=")).ToString();
                                        int vv = ss.IndexOf("(");
                                        string ff = ss.Substring((vv + 1), (ss.IndexOf(" mm", (vv + 1))
                                                        - (vv - 1)));
                                        ff = ff.Replace("(", "").Replace(")", "").Replace("x", "X").Trim();
                                        MAIN_MEASURING_MM = ff.Replace(" ", "");
                                        MAIN_MEASURING_MM = MAIN_MEASURING_MM.Replace("m", "");

                                        HEIGHT = (MAIN_MEASURING_MM.Substring(MAIN_MEASURING_MM.IndexOf("X") + 1, 4)).ToString();


                                        //String b = FNC.Between_string(ss, "(", ")");//.Replace("-", "/");
                                        //decimal b1 = decimal.Parse(b.Substring(0, b.IndexOf("-") - 1));
                                        //decimal b2 = decimal.Parse(b.Substring(b.IndexOf("-") + 1, b.IndexOf("-")));

                                        //RATIO = b2 / b1;
                                        //RATIO = Math.Round(RATIO, 2, MidpointRounding.AwayFromZero);
                                    }
                                }
                            }
                            #endregion

                            #region insert

                            string IdSplite = GridDetail.GetRowCellValue(i, "ID").ToString();
                            string[] ID = IdSplite.Split('-');
                            string StrKapan = ID[0];
                            int StrPacketSerialNo = Val.ToInt32(ID[1]);

                            StringBuilder st = new StringBuilder();
                            st.Length = 0;
                            st.AppendLine("  IF NOT EXISTS (SELECT 1 FROM Trn_HeliumList WHERE  (ID = '" + GridDetail.GetRowCellValue(i, "ID").ToString() + "')) ");
                            st.AppendLine("  BEGIN");
                            st.AppendLine("  INSERT INTO Trn_HeliumList ");
                            st.AppendLine("  (ID, PACKET_ID,SHAPE, CARAT, DEPTH, TAB, MEASUREMENT,  ");
                            st.AppendLine("  DR, LAB, CULET, GIRDLE, GP, CANG, CHIG, ");
                            st.AppendLine("  PANG,PHIG, SL, LH, RATIO, HEIGHT,   ");
                            st.AppendLine("  EMPLOYEE_ID, LOCATION, DATE) ");
                            st.AppendLine("  VALUES( ");
                            st.AppendLine("  '" + GridDetail.GetRowCellValue(i, "ID").ToString() + "', ");//ID
                            st.AppendLine("  CONVERT(Bigint, (SELECT  Packet_Id FROM TRN_SinglePacketMaster WHERE  KapanName = '" + StrKapan + "' And PktSerialNo = " + StrPacketSerialNo + ")),  ");//Packet_ID


                            String ST = Convert.ToDecimal(WEIGHT_CT).ToString("00.0000").ToString();
                            String ST1 = ST.Remove(5, 2);
                            String ST2 = ST.Substring(ST.Length - 2);


                            String str = "";
                            if (Convert.ToInt32(ST2) >= 90)
                            {
                                double ST4 = Convert.ToDouble(ST1);
                                ST4 = (ST4 + .01);
                                str = ST4.ToString();
                            }
                            else
                            {

                                str = ST1.ToString();
                            }
                            st.AppendLine("  '" + CUTTING_TYPE + "', ");//Shape
                            st.AppendLine("  " + str + ", ");//CARAT  str
                            st.AppendLine("  " + TOTAL_DEPTH_PC + ", ");//DEPTH
                            st.AppendLine("  " + TABLE_PC + ",  ");//TAB
                            st.AppendLine("  '" + MAIN_MEASURING_MM + "', ");//MEASUREMENT
                            st.AppendLine("  " + DIAMETER_MMA + ",");//DR
                            st.AppendLine("  'HELIUM',");//LAB
                            st.AppendLine("  '0.00', ");//CULET
                            st.AppendLine("  '0.00', ");//GIRDLE
                            st.AppendLine("  '0.00',  ");//GP
                            st.AppendLine("  0.00,  ");//CANG
                            st.AppendLine("  0.00, ");//CHIG
                            st.AppendLine("  0.00, ");//PANG
                            st.AppendLine("  0.00, ");//PHIG
                            st.AppendLine("  0.00, ");//SL
                            st.AppendLine("  0.00, ");//LH
                            st.AppendLine("  " + RATIO + " ,");//RATIO
                            st.AppendLine("  " + HEIGHT + ", ");//HEIGHT
                            st.AppendLine("  '1', ");//EMPLOYEE
                            st.AppendLine("  'SURAT', ");//LOCATION
                            st.AppendLine("   CONVERT(DATETIME,'" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "', 102)  ");//DATE
                            st.AppendLine("  )");
                            st.AppendLine(" END");
                            st.ToString();
                            //DF.datainupde(st.ToString());
                            int IntRes = ObjMast.   (st);

                            GridDetail.SetRowCellValue(i, "STATUS", "True");
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Global.Message(ex.Message);
                        }
                    }
                    else
                    {
                        try
                        {
                            #region excel
                            string val1 = "";
                            string val2 = "";
                            string val3 = "";

                            DataTable dt1 = new DataTable();
                            dt1.Columns.Add("val1");
                            dt1.Columns.Add("val2");
                            dt1.Columns.Add("val3");

                            int rw = 0;
                            int cl = 0;

                            string FilePath = Val.ToString(GridDetail.GetRowCellValue(i, "PATH"));

                            string extension = Path.GetExtension(FilePath.ToString());
                            string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(FilePath);

                            //
                            destinationPath = destinationPath.Replace(extension, ".xlsx");
                            if (File.Exists(destinationPath))
                            {
                                File.Delete(destinationPath);
                            }
                            File.Copy(FilePath, destinationPath);

                            DataTable DtabExcelData = GetExcelSheetData(destinationPath);

                            Excel.Application xlApp = new Excel.Application();
                            Excel.Workbook workbook = xlApp.Workbooks.Add();
                            Excel._Worksheet workSheet = workbook.ActiveSheet;

                            for (var N = 0; N < DtabExcelData.Columns.Count; N++)
                            {
                                workSheet.Cells[1, N + 1] = DtabExcelData.Columns[N].ColumnName;
                            }

                            for (var L = 0; L < DtabExcelData.Rows.Count; L++)
                            {
                                for (var j = 0; j < DtabExcelData.Columns.Count; j++)
                                {
                                    workSheet.Cells[L + 2, j + 1] = DtabExcelData.Rows[L][j];
                                }
                            }
                            Excel.Range range = workSheet.UsedRange;

                            rw = range.Rows.Count;
                            cl = range.Columns.Count;

                            int rCnt = 1;
                            for (rCnt = 1; rCnt <= rw; rCnt++)
                            {

                                val1 = ((range.Cells[rCnt, 1] as Excel.Range).Value2).ToString();
                                val2 = ((range.Cells[rCnt, 2] as Excel.Range).Value2).ToString();
                                try
                                {
                                    val3 = ((range.Cells[rCnt, 3] as Excel.Range).Value2).ToString();
                                }
                                catch
                                {
                                    val3 = "";
                                }
                                dt1.Rows.Add(val1, val2, val3);
                            }

                            string a = dt1.Rows[18].Field<string>(2);
                            string a1 = a.Replace(".", "");
                            Decimal a2 = (decimal.Parse("0." + a1) * decimal.Parse(dt1.Rows[3].Field<string>(2)));

                            string c = "";
                            //String b = FNC.Between_string(dt1.Rows[3].Field<string>(1), "(", ")");//.Replace("-", "/");
                            String b = dt1.Rows[3].Field<string>(1);//.Replace("-", "/");
                            IEnumerable<string> tokens = b
                            .Split('(')
                            .Where(t => t.Contains(')'))
                            .Select(t => t.Split(')').First());
                            if (tokens.Any())
                            {
                                c = tokens.First();
                            }
                            decimal b1 = decimal.Parse(c.Substring(0, c.IndexOf("-") - 1));
                            decimal b2 = decimal.Parse(c.Substring(c.IndexOf("-") + 1, c.IndexOf("-")));

                            decimal b3 = b2 / b1;
                            b3 = Math.Round(b3, 2, MidpointRounding.AwayFromZero);

                            workbook.Close(false);
                            xlApp.Quit();

                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workSheet);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(workbook);
                            System.Runtime.InteropServices.Marshal.ReleaseComObject(xlApp);

                            #endregion
                            #region insert

                            string IdSplite = GridDetail.GetRowCellValue(i, "ID").ToString();
                            string[] ID = IdSplite.Split('-');
                            string StrKapan = ID[0];
                            int StrPacketSerialNo = Val.ToInt32(ID[1]);

                            StringBuilder st = new StringBuilder();
                            st.Length = 0;
                            // st.AppendLine("  DELETE FROM Trn_HeliumList WHERE  (ID = '" + GridDetail.GetRowCellValue(i, "ID").ToString() + "') ");
                            st.AppendLine("  IF NOT EXISTS (SELECT 1 FROM Trn_HeliumList WHERE  (ID = '" + GridDetail.GetRowCellValue(i, "ID").ToString() + "')) ");
                            st.AppendLine("  BEGIN");
                            st.AppendLine("  INSERT INTO Trn_HeliumList ");
                            st.AppendLine("  (ID, PACKET_ID,SHAPE, CARAT, DEPTH, TAB, MEASUREMENT,  ");
                            st.AppendLine("  DR, LAB, CULET, GIRDLE, GP, CANG, CHIG, ");
                            st.AppendLine("  PANG,PHIG, SL, LH, RATIO, HEIGHT,   ");
                            st.AppendLine("  EMPLOYEE_ID, LOCATION, DATE) ");
                            st.AppendLine("  VALUES( ");
                            st.AppendLine("  '" + GridDetail.GetRowCellValue(i, "ID").ToString() + "', ");//ID
                            st.AppendLine("  CONVERT(Bigint, (SELECT  Packet_Id FROM TRN_SinglePacketMaster WHERE  KapanName = '" + StrKapan + "' And PktSerialNo = " + StrPacketSerialNo + ")),  ");//Packet_ID

                            //st.AppendLine("  CONVERT(int, (SELECT DISTINCT Para_ID FROM  MST_Para WHERE  (ShortName = '" + dt1.Rows[1].Field<string>(1) + "' or ParaName = '" + dt1.Rows[1].Field<string>(1) + "'))),  ");//SHAPE

                            String ST = Convert.ToDecimal(dt1.Rows[2].Field<string>(1)).ToString("00.0000").ToString();
                            String ST1 = ST.Remove(5, 2);
                            String ST2 = ST.Substring(ST.Length - 2);

                            string StrMeasurment = "";
                            string StrGirdle = "";
                            string StrGirdleNew = "";

                            String M = dt1.Rows[3].Field<string>(1);//.Replace("-", "/");
                            IEnumerable<string> Measurment = M
                            .Split('(')
                            .Where(t => t.Contains(')'))
                            .Select(t => t.Split(')').First());
                            if (Measurment.Any())
                            {
                                StrMeasurment = Measurment.First();
                            }
                            String G = dt1.Rows[13].Field<string>(2);//.Replace("-", "/");
                            IEnumerable<string> Girdle = G
                            .Split('(')
                            .Where(t => t.Contains(')'))
                            .Select(t => t.Split(')').First());
                            if (Girdle.Any())
                            {
                                StrGirdle = Girdle.First();
                            }
                            String G1 = dt1.Rows[14].Field<string>(2);
                            IEnumerable<string> Girdle1 = G1
                            .Split('(')
                            .Where(t => t.Contains(')'))
                            .Select(t => t.Split(')').First());
                            if (Girdle1.Any())
                            {
                                StrGirdleNew = Girdle1.First();
                            }

                            string StGirdleReal = StrGirdle + "-" + StrGirdleNew;
                            String str = "";
                            if (Convert.ToInt32(ST2) >= 90)
                            {
                                double ST4 = Convert.ToDouble(ST1);
                                ST4 = (ST4 + .01);
                                str = ST4.ToString();
                            }
                            else
                            {
                                str = ST1.ToString();
                            }
                            st.AppendLine("  '" + dt1.Rows[1].Field<string>(1) + "', ");//Shape
                            st.AppendLine("  " + str + ", ");//CARAT  str
                            st.AppendLine("  " + dt1.Rows[18].Field<string>(2) + ", ");//DEPTH
                            st.AppendLine("  " + dt1.Rows[4].Field<string>(2) + ",  ");//TAB
                            //st.AppendLine("  '',  ");//MEASUREMENT//Comment By Gunjan:04-10-2023
                            st.AppendLine("  '" + StrMeasurment + "X" + a2 + "', ");//MEASUREMENT
                            st.AppendLine("  " + dt1.Rows[3].Field<string>(2) + ",");//DR
                            st.AppendLine("  'HELIUM',");//LAB
                            st.AppendLine("  '" + dt1.Rows[15].Field<string>(2) + "', ");//CULET
                            // st.AppendLine("  '',  ");//GIRDLE//Comment By Gunjan:04-10-2023
                            st.AppendLine("  '" + StGirdleReal + "', ");//GIRDLE
                            st.AppendLine("  " + dt1.Rows[12].Field<string>(2) + ",  ");//GP
                            st.AppendLine("  " + dt1.Rows[5].Field<string>(2) + ",  ");//CANG
                            st.AppendLine("  " + dt1.Rows[16].Field<string>(2) + ", ");//CHIG
                            st.AppendLine("  " + dt1.Rows[6].Field<string>(2) + ", ");//PANG
                            st.AppendLine("  " + dt1.Rows[17].Field<string>(2) + ", ");//PHIG
                            st.AppendLine("  " + dt1.Rows[10].Field<string>(2) + ", ");//SL
                            st.AppendLine("  " + dt1.Rows[11].Field<string>(2) + ", ");//LH
                            // st.AppendLine("  0.00,  ");//RATIO//Comment By Gunjan:04-10-2023
                            st.AppendLine("  " + decimal.Parse(b3.ToString()) + " ,");//RATIO
                            st.AppendLine("  " + a2 + ", ");//HEIGHT
                            st.AppendLine("  1, ");//EMPLOYEE
                            st.AppendLine("  'SURAT', ");//LOCATION
                            st.AppendLine("   CONVERT(DATETIME,'" + DateTime.Now.ToString("yyyy-MM-dd h:mm tt") + "', 102)  ");//DATE
                            st.AppendLine("  )");
                            st.AppendLine(" END");
                            st.ToString();

                            int IntRes = ObjMast.SavetxtHeliumFile(st);
                            GridDetail.SetRowCellValue(i, "STATUS", "True");
                            dt1.Clear();
                            #endregion
                        }
                        catch (Exception ex)
                        {
                            Global.Message(ex.Message);
                        }
                    }

                }
                #endregion
            }
        }
        private void FrmHeliumFileImport_Load(object sender, EventArgs e)
        {
            try
            {
                GetHeliumFilePath();
                ADD_PATH(DateTime.Today.AddDays(0).ToString("yyyy"), DateTime.Today.AddDays(0).ToString("MM"), DateTime.Today.AddDays(0).ToString("dd"));
                ADD_PATH(DateTime.Today.AddDays(-1).ToString("yyyy"), DateTime.Today.AddDays(-1).ToString("MM"), DateTime.Today.AddDays(-1).ToString("dd"));
                InsertInToTable();
                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {          
            if (e.RowHandle >= 0)
            {
                try
                {
                    if (GridDetail.GetRowCellDisplayText(e.RowHandle, GridDetail.Columns["STATUS"]).ToString() != "")
                    {
                        string category = GridDetail.GetRowCellDisplayText(e.RowHandle, GridDetail.Columns["STATUS"]);
                        if (category == "False")
                        {
                            e.Appearance.BackColor = Color.LightPink;
                        }
                        if (category == "True")
                        {
                            e.Appearance.BackColor = Color.LightGray;
                        }
                    }
                }
                catch (Exception)
                {

                    // throw;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            try
            {
                ADD_PATH(DateTime.Today.AddDays(0).ToString("yyyy"), DateTime.Today.AddDays(0).ToString("MM"), DateTime.Today.AddDays(0).ToString("dd"));
                ADD_PATH(DateTime.Today.AddDays(-1).ToString("yyyy"), DateTime.Today.AddDays(-1).ToString("MM"), DateTime.Today.AddDays(-1).ToString("dd"));
                InsertInToTable();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private bool ValSave()
        {
            if (TxtHelium_ID.Text.Trim().Length == 0)
            {
                Global.MessageError("Helium_ID Is Required");
                TxtHelium_ID.Focus();
                return false;
            }
            else if (TxtKapan.Text.Trim().Length == 0)
            {
                Global.MessageError("Kapan Is Required");
                TxtKapan.Focus();
                return false;
            }
            else if (TxtPacketNo.Text.Trim().Length == 0)
            {
                Global.MessageError("PacketNo Is Required");
                TxtPacketNo.Focus();
                return false;
            }
            else if (TxtCarat.Text.Trim().Length == 0)
            {
                Global.MessageError("Carat Is Required");
                TxtCarat.Focus();
                return false;
            }
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                ADD_PATH(DateTime.Today.AddDays(0).ToString("yyyy"), DateTime.Today.AddDays(0).ToString("MM"), DateTime.Today.AddDays(0).ToString("dd"));
                ADD_PATH(DateTime.Today.AddDays(-1).ToString("yyyy"), DateTime.Today.AddDays(-1).ToString("MM"), DateTime.Today.AddDays(-1).ToString("dd"));
                InsertInToTable();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }
          
                HeliumFileReportProperty Property = new HeliumFileReportProperty();
                Property.HELIUM_ID = Val.ToString(TxtHelium_ID.Tag);
                Property.KAPAN = Val.ToString(TxtKapan.Text);
                Property.PACKETNO = Val.ToString(TxtPacketNo.Text);
                //string sql = "SELECT Packet_Id FROM Trn_MumbaiTransferNew WHERE Kapan = @Kapan AND PacketNo = @PacketNo";
                Property.TAG = Val.ToString(TxtTag.Text);
                Property.HELIUM_ID = Val.ToString(TxtHelium_ID.Text);
                Property.SHAPE = Val.ToString(TxtShape.Text);
                Property.SHAPE_ID = Val.ToInt32(TxtShape.Tag);
                Property.CARAT = Val.ToDouble(TxtCarat.Text);
                Property.DEPTH = Val.ToDouble(Txtdepth.Text);
                Property.TAB = Val.ToDouble(TxtTab.Text);
                Property.H_MEASURMENT = Val.ToString(TxtH_Measurment.Text);
                Property.H_DR = Val.ToDouble(TxtH_Dr.Text);
                Property.H_LAB = Val.ToString(TxtH_Lab.Text);
                Property.H_CULET = Val.ToString(TxtH_Culet.Text);
                Property.H_GIRDLE = Val.ToString(TxtH_Girdle.Text);
                Property.H_GP = Val.ToString(TxtH_Gp.Text);
                Property.H_CANG = Val.ToDouble(TxtH_Cang.Text);
                Property.H_CHIG = Val.ToDouble(TxtH_Chig.Text);
                Property.H_PANG = Val.ToDouble(TxtH_Pang.Text);
                Property.H_PHIG = Val.ToDouble(TxtH_Phig.Text);
                Property.H_LH = Val.ToDouble(TxtH_Lh.Text);
                Property.H_RATIO = Val.ToDouble(TxtH_RAtio.Text);
                Property.H_HEIGHT = Val.ToDouble(TxtH_Height.Text);
                Property.LOCATION = Val.ToString(TxtLocation.Text);
                Property.COLOR = Val.ToString(TxtColor.Text);
                Property.COLOR_ID = Val.ToInt32(TxtColor.Text);
                Property.CLARITY = Val.ToString(TxtClarity.Text);
                Property.CLARITY_ID = Val.ToInt32(TxtClarity.Text);
                Property.CUT = Val.ToString(TxtCut.Text);
                Property.CUT_ID = Val.ToInt32(TxtCut.Text);
                Property.POLISH = Val.ToString(TxtPolish.Text);
                Property.POLISH_ID = Val.ToInt32(TxtPolish.Text);
                Property.SYMM = Val.ToString(TxtSymm.Text);
                Property.SYMM_ID = Val.ToInt32(TxtSymm.Text);
                Property.FLOUR = Val.ToString(TxtFlour.Text);
                Property.FLOUR_ID = Val.ToInt32(TxtFlour.Text);
                Property.MILKY = Val.ToString(TxtMilky.Text);
                Property.MILKY_ID = Val.ToInt32(TxtMilky.Text);
                Property.BROWN = Val.ToString(TxtBrown.Text);
                Property.BROWN_ID = Val.ToInt32(TxtBrown.Text);
                Property.TAB_INC = Val.ToString(TxtTab_Inc.Text);
                Property.TAB_INC_ID = Val.ToInt32(TxtTab_Inc.Text);
                Property.BLA_INC = Val.ToString(TxtBla_Inc.Text);
                Property.BLA_INC_ID = Val.ToInt32(TxtBla_Inc.Text);
                Property.LUSTER = Val.ToString(TxtLuster.Text);
                Property.LUSTER_ID = Val.ToInt32(TxtLuster.Text);
                Property.T_OPEN = Val.ToString(TxtT_Open.Text);
                Property.T_OPEN_ID = Val.ToInt32(TxtT_Open.Text);
                Property.C_OPEN = Val.ToString(TxtC_Open.Text);
                Property.C_OPEN_ID = Val.ToInt32(TxtC_Open.Text);
                Property.P_OPEN = Val.ToString(TxtP_Open.Text);
                Property.P_OPEN_ID = Val.ToInt32(TxtP_Open.Text);
                Property.HA = Val.ToString(TxtHA.Text);
                Property.HA_ID = Val.ToInt32(TxtHA.Text);
                Property.TYPE = Val.ToString(TxtType.Text);
                Property.LAB = Val.ToString(TxtLab.Text);
                Property.LAB_ID = Val.ToInt32(TxtLab.Text);
                Property.GIANONGIA = Val.ToString(TxtGIANONGIA.Text);
                Property.RAPAPORT = Val.ToDouble(TxtRapaport.Text);
                Property.DISCOUNT = Val.ToDouble(TxtDiscount.Text);
                Property.PRICEPERCARAT = Val.ToDouble(TxtPriceperCts.Text);
                Property.AMOUNT = Val.ToDouble(TxtAmount.Text);
                Property.JANGADNO = Val.ToInt64(TxtJangadNo.Text);
                
                Property = ObjMast.Save(Property);
                InsertInToTable();

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnClear_Click(null, null);
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            TxtKapan.Text = string.Empty;
            TxtPacketNo.Text = string.Empty;
            TxtTag.Text = string.Empty;
            TxtHelium_ID.Text = string.Empty;
            TxtShape.Text = string.Empty;
            TxtCarat.Text = string.Empty;
            Txtdepth.Text = string.Empty;
            TxtTab.Text = string.Empty;
            TxtH_Measurment.Text = string.Empty;
            TxtH_Dr.Text = string.Empty;
            TxtH_Culet.Text = string.Empty;
            TxtH_Lab.Text = string.Empty;
            TxtH_Girdle.Text = string.Empty;
            TxtH_Gp.Text = string.Empty;
            TxtH_Cang.Text = string.Empty;
            TxtH_Chig.Text = string.Empty;
            TxtH_Pang.Text = string.Empty;
            TxtH_Phig.Text = string.Empty;
            TxtH_Lh.Text = string.Empty;
            TxtH_RAtio.Text = string.Empty;
            TxtH_Height.Text = string.Empty;
            TxtLocation.Text = string.Empty;
            TxtColor.Text = string.Empty;
            TxtClarity.Text = string.Empty;
            TxtCut.Text = string.Empty;
            TxtPolish.Text = string.Empty;
            TxtSymm.Text = string.Empty;
            TxtFlour.Text = string.Empty;
            TxtMilky.Text = string.Empty;
            TxtBrown.Text = string.Empty;
            TxtBla_Inc.Text = string.Empty;
            TxtTab_Inc.Text = string.Empty;
            TxtLuster.Text = string.Empty;
            TxtT_Open.Text = string.Empty;
            TxtC_Open.Text = string.Empty;
            TxtP_Open.Text = string.Empty;
            TxtLab.Text = string.Empty;
            TxtType.Text = string.Empty;
            TxtHA.Text = string.Empty;
            TxtGIANONGIA.Text = string.Empty;
            TxtRapaport.Text = string.Empty;
            TxtDiscount.Text = string.Empty;
            TxtPriceperCts.Text = string.Empty;
            TxtAmount.Text = string.Empty;
            TxtJangadNo.Text = string.Empty;
        }

        private void TxtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);

                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        TxtShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);

                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtColor.Text = Val.ToString(FrmSearch.mDRow["COLORNAME"]);
                        TxtColor.Tag = Val.ToString(FrmSearch.mDRow["COLOR_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);

                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        TxtClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);

                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtCut.Text = Val.ToString(FrmSearch.mDRow["CUTNAME"]);
                        TxtCut.Tag = Val.ToString(FrmSearch.mDRow["CUT_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtPolish_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);

                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtPolish.Text = Val.ToString(FrmSearch.mDRow["POLNAME"]);
                        TxtPolish.Tag = Val.ToString(FrmSearch.mDRow["POL_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtSymm_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);

                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtSymm.Text = Val.ToString(FrmSearch.mDRow["SYMNAME"]);
                        TxtSymm.Tag = Val.ToString(FrmSearch.mDRow["SYM_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtFlour_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "FLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);

                    FrmSearch.mColumnsToHide = "FL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtFlour.Text = Val.ToString(FrmSearch.mDRow["FLNAME"]);
                        TxtFlour.Tag = Val.ToString(FrmSearch.mDRow["FL_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtMilky_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "MILKYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);

                    FrmSearch.mColumnsToHide = "MILKY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtMilky.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
                        TxtMilky.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtBrown_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "MILKYNAME";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);

            //        FrmSearch.mColumnsToHide = "MILKY_ID";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            TxtColor.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
            //            TxtColor.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void TxtBla_Inc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "MILKYNAME";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);

            //        FrmSearch.mColumnsToHide = "MILKY_ID";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            TxtColor.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
            //            TxtColor.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void TxtTab_Inc_KeyPress(object sender, KeyPressEventArgs e)
        {
            //try
            //{
            //    if (Global.OnKeyPressToOpenPopup(e))
            //    {
            //        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
            //        FrmSearch.mSearchField = "MILKYNAME";
            //        FrmSearch.mSearchText = e.KeyChar.ToString();
            //        this.Cursor = Cursors.WaitCursor;
            //        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_MILKY);

            //        FrmSearch.mColumnsToHide = "MILKY_ID";
            //        this.Cursor = Cursors.Default;
            //        FrmSearch.ShowDialog();
            //        e.Handled = true;
            //        if (FrmSearch.mDRow != null)
            //        {
            //            TxtColor.Text = Val.ToString(FrmSearch.mDRow["MILKYNAME"]);
            //            TxtColor.Tag = Val.ToString(FrmSearch.mDRow["MILKY_ID"]);
            //        }
            //        FrmSearch.Hide();
            //        FrmSearch.Dispose();
            //        FrmSearch = null;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void TxtLuster_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LUSTERNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LUSTER);

                    FrmSearch.mColumnsToHide = "LUSTER_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtLuster.Text = Val.ToString(FrmSearch.mDRow["LUSTERNAME"]);
                        TxtLuster.Tag = Val.ToString(FrmSearch.mDRow["LUSTER_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtT_Open_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "TABLEOPENNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_TABLEOPEN);

                    FrmSearch.mColumnsToHide = "TABLEOPEN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtT_Open.Text = Val.ToString(FrmSearch.mDRow["TABLEOPENNAME"]);
                        TxtT_Open.Tag = Val.ToString(FrmSearch.mDRow["TABLEOPEN_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtC_Open_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CROWNOPENNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CROWNOPEN);

                    FrmSearch.mColumnsToHide = "CROWNOPEN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtC_Open.Text = Val.ToString(FrmSearch.mDRow["CROWNOPENNAME"]);
                        TxtC_Open.Tag = Val.ToString(FrmSearch.mDRow["CROWNOPEN_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtP_Open_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PAVILLIONOPENNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PAVILLIONOPEN);

                    FrmSearch.mColumnsToHide = "PAVILLIONOPEN_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtP_Open.Text = Val.ToString(FrmSearch.mDRow["PAVILLIONOPENNAME"]);
                        TxtP_Open.Tag = Val.ToString(FrmSearch.mDRow["PAVILLIONOPEN_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtHA_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "HANAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_HA);

                    FrmSearch.mColumnsToHide = "HA_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtHA.Text = Val.ToString(FrmSearch.mDRow["HANAME"]);
                        TxtHA.Tag = Val.ToString(FrmSearch.mDRow["HA_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtLab_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "LABNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LAB);

                    FrmSearch.mColumnsToHide = "LAB_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        TxtLab.Text = Val.ToString(FrmSearch.mDRow["LABNAME"]);
                        TxtLab.Tag = Val.ToString(FrmSearch.mDRow["LAB_ID"]);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
    }
}
