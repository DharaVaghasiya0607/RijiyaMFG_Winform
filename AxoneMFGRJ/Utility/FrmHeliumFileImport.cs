using BusLib.Configuration;
using BusLib.Master;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
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
                            int IntRes = ObjMast.SavetxtHeliumFile(st);

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
    }
}
