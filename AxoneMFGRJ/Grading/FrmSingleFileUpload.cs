using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
using BusLib.TableName;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Columns;
using System.Data.OleDb;
using System.Text.RegularExpressions;
using BusLib.Rapaport;

namespace AxoneMFGRJ.Grading
{
    public partial class FrmSingleFileUpload : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjMast = new BOMST_Attendance();

        DataTable DtabExcelData = new DataTable();
        DataTable DtabFinal = new DataTable();
        DataTable DtabFileUpload = new DataTable();
        string StrUploadFilename = "";


        BOTRN_SingleFileUpload ObjUpload = new BOTRN_SingleFileUpload();
        Guid mUpload_ID = Guid.Empty;
        Guid mGroup_ID = Guid.Empty;

        #region Property Settings

        public FrmSingleFileUpload()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            BtnAdd_Click(null, null);
            Fill();
            this.Show();

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjUpload);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion


        #region Enter Event

        private void ControlEnterForGujarati_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.GUJARATI);
        }
        private void ControlEnterForEnglish_Enter(object sender, EventArgs e)
        {
            Global.SelectLanguage(Global.LANGUAGE.ENGLISH);
        }


        #endregion

        public void Clear()
        {
            this.Cursor = Cursors.WaitCursor;
            CmbSheetName.Text = string.Empty;
            CmbSheetName.SelectedIndex = -1;
            CmbLabType.SelectedIndex = 0;
            DTPUploadDate.Text = Val.ToString(DateTime.Now);

            txtFileName.Text = string.Empty;


            DTPFromDate.Text = Val.ToString(DateTime.Now.AddDays(-7));
            DTPToDate.Text = Val.ToString(DateTime.Now);
            txtKapan.Text = string.Empty;
            txtPacketNo.Text = string.Empty;
            txtPacketTag.Text = string.Empty;
            //ChkCmbLab.DeselectAll();
            ChkCmbLab.SetEditValue(0);
            DtabExcelData.Rows.Clear();

            //-------------------

            MainGrid.BringToFront();
            MainGrid.Dock = DockStyle.Fill;
            MainGrdSearch.SendToBack();

            //------------------------


            MainGrid.DataSource = null;
            GrdDet.Columns.Clear();
            CmbLabType.Focus();
            mUpload_ID = Guid.Empty;
            Fill();
            GrdDet.OptionsBehavior.Editable = true;
            this.Cursor = Cursors.Default;

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        public void Fill()
        {
            //DtabAtd = ObjMast.Fill(StrLedgerGroup);
            //DtabAtd.Rows.Add(DtabAtd.NewRow());
            //MainGrid.DataSource = DtabAtd;
            //MainGrid.Refresh();
            DtabFileUpload = ObjUpload.GetFileUploadData(mUpload_ID, "", "", "", "", "", "");
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F5)
            {
                //BtnSearch_Click(null, null);
            }
        }

        public void FetchValue(DataRow DR)
        {
            //txtParaType.Text = Val.ToString(DR["ITEMGROUP_ID"]);
        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            DevExpress.XtraPrinting.PrintingSystem PrintSystem = new DevExpress.XtraPrinting.PrintingSystem();

            PrinterSettingsUsing pst = new PrinterSettingsUsing();

            PrintSystem.PageSettings.AssignDefaultPrinterSettings(pst);

            //Lesson2 link = new Lesson2(PrintSystem);
            PrintableComponentLink link = new PrintableComponentLink(PrintSystem);

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = false;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 120;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Daily Attendance Paper Of ( " + DTPUploadDate.Text + " )", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Black, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("Verdana", 8, FontStyle.Bold);
            BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitledate.ForeColor = Color.Black;
        }

        public void Link_CreateMarginalFooterArea(object sender, CreateAreaEventArgs e)
        {
            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 100, 0));

            PageInfoBrick BrickPageNo = e.Graph.DrawPageInfo(PageInfo.NumberOfTotal, "Page {0} of {1}", System.Drawing.Color.Black, new RectangleF(IntX, 0, 100, 15), DevExpress.XtraPrinting.BorderSide.None);
            BrickPageNo.LineAlignment = BrickAlignment.Far;
            BrickPageNo.Alignment = BrickAlignment.Far;
            // BrickPageNo.AutoWidth = true;
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold);
            BrickPageNo.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            BrickPageNo.VertAlignment = DevExpress.Utils.VertAlignment.Center;
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog = new OpenFileDialog();
                //OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
                OpenFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";


                if (CmbLabType.Text == "GIA" || CmbLabType.Text == "IGI" || CmbLabType.Text == "HRD")
                {
                    OpenFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                    if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtFileName.Text = OpenFileDialog.FileName;

                        FileInfo f = new FileInfo(txtFileName.Text);

                        if (f.Extension.ToUpper().Contains("CSV"))
                        {
                            DtabExcelData = Global.GetDataTableFromCsv(txtFileName.Text, true);
                            StrUploadFilename = Path.GetFileName(txtFileName.Text);
                        }
                        else
                        {
                            txtFileName.Text = OpenFileDialog.FileName;

                            string extension = Path.GetExtension(txtFileName.Text.ToString());
                            string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                            destinationPath = destinationPath.Replace(extension, ".xlsx");
                            if (File.Exists(destinationPath))
                            {
                                File.Delete(destinationPath);
                            }
                            File.Copy(txtFileName.Text, destinationPath);

                            //GetExcelSheetNames(destinationPath);
                            //CmbSheetName.SelectedIndex = 0;
                            StrUploadFilename = Path.GetFileName(txtFileName.Text);

                            if (File.Exists(destinationPath))
                            {
                                File.Delete(destinationPath);
                            }
                        }

                    }
                }
                else
                {
                    if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        txtFileName.Text = OpenFileDialog.FileName;

                        string extension = Path.GetExtension(txtFileName.Text.ToString());
                        string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                        destinationPath = destinationPath.Replace(extension, ".xlsx");
                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }
                        File.Copy(txtFileName.Text, destinationPath);

                        GetExcelSheetNames(destinationPath);
                        CmbSheetName.SelectedIndex = 0;
                        StrUploadFilename = Path.GetFileName(txtFileName.Text);

                        if (File.Exists(destinationPath))
                        {
                            File.Delete(destinationPath);
                        }
                    }
                }
                OpenFileDialog.Dispose();
                OpenFileDialog = null;
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString() + "InValid File Name");
            }
        }

        private String[] GetExcelSheetNames(string excelFile)
        {
            OleDbConnection objConn = null;
            System.Data.DataTable dt = null;

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
                List<string> sheets = new List<string>();
                if (dt == null)
                {
                    return null;
                }
                String[] excelSheets = new String[dt.Rows.Count];
                CmbSheetName.Items.Clear(); //ADD:KULDEEP[24/05/18]
                foreach (DataRow row in dt.Rows)
                {
                    string sheetName = (string)row["TABLE_NAME"];
                    sheets.Add(sheetName);
                    CmbSheetName.Items.Add(sheetName);
                }

                return excelSheets;
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
        }


        public static DataTable GetDataTableFromExcel(string path, bool hasHeader = true)
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
                    if (Convert.ToString(firstRowCell.Text).Equals(string.Empty))
                        continue;

                    tbl.Columns.Add(hasHeader ? firstRowCell.Text : string.Format("Column {0}", firstRowCell.Start.Column));
                }
                var startRow = hasHeader ? 2 : 1;
                for (int rowNum = startRow; rowNum <= ws.Dimension.End.Row; rowNum++)
                {
                    var wsRow = ws.Cells[rowNum, 1, rowNum, ws.Dimension.End.Column];
                    DataRow row = tbl.Rows.Add();
                    foreach (var cell in wsRow)
                    {
                        if (Convert.ToString(cell.Text).Equals(string.Empty))
                            continue;

                        row[cell.Start.Column - 1] = cell.Text;
                    }
                }
                return tbl;
            }
        }

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                int IntSrNo = 1;
                mGroup_ID = Guid.NewGuid();

                int IntCount = 0;


                if (Path.GetExtension(txtFileName.Text.ToString()).ToUpper().Contains("XLSX") || Path.GetExtension(txtFileName.Text.ToString()).ToUpper().Contains("XLS"))
                {
                    string extension = Path.GetExtension(txtFileName.Text.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                    destinationPath = destinationPath.Replace(extension, ".xlsx");
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(txtFileName.Text, destinationPath);

                    // DtabExcelData = Global.ImportExcelXLSWithSheetName(destinationPath, true, CmbSheetName.SelectedItem.ToString());

                    DtabExcelData = GetDataTableFromExcel(destinationPath, true);

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                }

                if (DtabExcelData.Rows.Count <= 0)
                {
                    this.Cursor = Cursors.Default;
                    return;
                }


                DtabFinal = DtabFileUpload.Clone();


                if (!DtabExcelData.Columns.Contains("Status"))
                {
                    DtabExcelData.Columns.Add("Status", typeof(string));
                }
                for (int Intcol = 0; Intcol < DtabExcelData.Columns.Count; Intcol++)
                {
                    if (Val.ToString("JobNo,Job No,Certif.No,Document No,SampleResults: Our Ref.").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("JOBNO");

                    if (Val.ToString("Control No,ControlNo,Dir.No,Other Report Number").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CONTROLNO");

                    if (Val.ToString("Diamond Dossier").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("DIAMONDDOSSIER");

                    if (Val.ToString("Report No,ReportNo,Report Number,Report.No").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPORTNO");

                    if (Val.ToString("Report Dt,Report Date,ReportDate,ReportDt,Cert.Date,SaleDate,GrdDate,Valid From").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPORTDATE");

                    if (Val.ToString("Client ref No,Client Ref,StoneNo,Customer Ref No.,#Stock").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CLIENTREFNO");

                    if (Val.ToString("MemoNo,Memo,Memo No").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("MEMONO");

                    if (Val.ToString("Shape,Shp,Shape Name").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SHAPE");

                    if (Val.ToString("Length,Len,Measurement1,Diam / LW Min").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LENGTH");

                    if (Val.ToString("Width,Wid,Measurement2,Diam / LW Max").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("WIDTH");

                    if (Val.ToString("Depth,Dep,Measurement3,Height CAvg").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("DEPTH");

                    if (Val.ToString("Weight,Carat,Wt,Weight R").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CARAT");

                    if (Val.ToString("Color,Col.,Col,Color (Short),Colour").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("COLOR");

                    if (Val.ToString("Color Descriptions,Color Desc,Color Desc.,Color (Long)").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("COLORDESC");

                    if (Val.ToString("Clarity").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper().Trim())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CLARITY");

                    if (Val.ToString("Clarity Status").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CLARITYSTATUS");

                    if (Val.ToString("Final Cut,cut,CUT-PROP,Proportions").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CUT");

                    if (Val.ToString("Polish,Pol,POL or pol/sym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("POLISH");

                    if (Val.ToString("Symmetry,Symm,Sym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SYMMETRY");

                    if (Val.ToString("Fl,Fluorescence,Flo,Fluor,Fluorescence Intensity,LW-fluo").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("FLUORESCENCE");

                    if (Val.ToString("Fluorescence Color").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("FLUORESCENCECOLOR");

                    if (Val.ToString("Girdle,Girdle Desc,Girdle description,Girdle Name").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("GIRDLEDESC");

                    if (Val.ToString("Girdle Condition,Girdle Cond,Girdle nature,Girdle description").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("GIRDLECONDITION");

                    if (Val.ToString("Culet Size,Culet nature").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CULETSIZE");

                    if (Val.ToString("Depth Per,Depth %,Total Depth,Total depth%").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("DEPTHPER");

                    if (Val.ToString("Table %,Table Per,Table,Table width %").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("TABLE1");

                    if (Val.ToString("Crn Ag,Crown Angle,Crown angle°").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CRANGLE");

                    if (Val.ToString("Crn Ht,Crown Height,Crown Ht,Cr Height,Height CAvg,Cr. Height%").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CRHEIGHT");

                    if (Val.ToString("Pav Ag,Pav Ang,Pav Angle,Pavillion Angle,Pavillion Ag,Pavilion Angle,Pavillion angle°").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAVANGLE");

                    if (Val.ToString("Pav Dp,Pavillion Depth,pav Depth,Pavilion Depth,Pav. depth%").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAVHEIGHT");

                    if (Val.ToString("Star Length,Str Ln").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("STARLENGTH");

                    if (Val.ToString("Lr Half,Lower Half,LowerHalf").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LOWERHALF");

                    if (Val.ToString("Painting").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAINTING");

                    if (Val.ToString("Proportion,Proportions").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PROPORTIONS");

                    if (Val.ToString("Paint Comm").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAINTCOMM");

                    if (Val.ToString("Key to Symbols,Key To Sym,Key To Symm").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("KEYTOSYMBOL");

                    if (Val.ToString("Report Comments,Report Comment").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPORTCOMMENT");

                    if (Val.ToString("Inscription").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("INSCRIPTION");

                    if (Val.ToString("Synthetic Indicator").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SYNTHETICINDICATOR");

                    if (Val.ToString("Girdle %,Girdle Per,Girdle size %,Girdle Percent,Girdle size %").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("GIRDLEPER");

                    if (Val.ToString("Polish Features").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("POLISHFEATURES");

                    if (Val.ToString("Symmetry Features").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SYMMETRYFEATURES");

                    if (Val.ToString("Shape Description,Shape Desc").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SHAPEDESC");

                    if (Val.ToString("Report Type").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPORTTYPE");

                    if (Val.ToString("Diamond Link").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("DIAMONDLINK");

                    if (Val.ToString("Validitydate").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("VALIDITYDATE");

                    if (Val.ToString("Culet nature").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CULETNATURE");

                    if (Val.ToString("SaleDoneBy,GrdDoneBy").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("GRDDONEBY");

                    if (Val.ToString("Rapaport").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RAPAPORT");

                    if (Val.ToString("Back").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("BACK");

                    if (Val.ToString("PricePerCarat").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PRICEPERCARAT");

                    if (Val.ToString("Amount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("AMOUNT");

                    if (Val.ToString("BackDisc").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("BACKDISC");

                    if (Val.ToString("SalePrice").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SALEPRICE");

                    if (Val.ToString("SaleAmount,Sale Amount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SALEAMOUNT");

                    if (Val.ToString("Lab Charge").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LABCHARGE");

                    if (CmbLabType.Text == "HRD")
                    {
                        if (Val.ToString("Remark").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                            DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REMARK");
                    }
                    else
                    {
                        if (Val.ToString("Remarks,Remark").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                            DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REMARK");
                    }
                    if (Val.ToString("Milky").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("MILKY");

                    if (Val.ToString("LBLC").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LBLC");

                    if (Val.ToString("Natts").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("Natts");


                    if (Val.ToString("HNA,HA").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("HA");

                    if (Val.ToString("PAV").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAV");

                    if (Val.ToString("BINC").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("BINC");

                    if (Val.ToString("OINC").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("OINC");

                    if (Val.ToString("WINC").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("WINC");

                    if (Val.ToString("Tension").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("TENSION");

                    if (Val.ToString("CS").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("CS");

                    if (Val.ToString("Luster").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LUSTER");

                    if (Val.ToString("Natural").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("NATURAL");

                    if (Val.ToString("Grain ").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("GRAIN");

                    if (Val.ToString("EyeClean").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("EYECLEAN");

                    if (Val.ToString("Lab").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LAB");

                    if (Val.ToString("Special Comments").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SPECIALCOMMENT");   //Used In IGI Format

                    if (Val.ToString("Result").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RESULT");          //Used In IGI Format


                }

                if (CmbLabType.Text == "GIA" )
                {
                    foreach (DataRow Dr in DtabExcelData.Rows)
                    {
                        string Str = "";
                        string PacketNo = "";
                        string StrPcketTag = "";
                        DataRow DrFinal = DtabFinal.NewRow();
                        DrFinal["UPLOAD_ID"] = Guid.NewGuid();
                        DrFinal["PACKET_ID"] = Guid.Empty;
                        DrFinal["GROUP_ID"] = mGroup_ID;

                        DrFinal["UPLOADDATE"] = Val.ToString(DTPUploadDate.Text);
                        DrFinal["UPLOADTYPE"] = Val.ToString(CmbLabType.Text);

                        if (Val.ToString(Dr["CLIENTREFNO"]).Contains("-"))
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("-")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("-") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty);
                        }
                        else
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]);
                            DrFinal["PACKETNO"] = "";
                        }
                        StrPcketTag = Regex.Replace(Str.ToString(), @"[\d-]", "");

                        DrFinal["PACKETNO"] = Val.ToString(PacketNo);
                        DrFinal["TAG"] = Val.ToString(StrPcketTag);


                        DrFinal["STATUS"] = Dr["STATUS"];
                        //DrFinal["REMARK"] = Val.ToString(CmbLabType.Text) == "HRD" ? Val.ToString(Dr["REMARK"]) : "";
                        DrFinal["REMARK"] = Val.ToString(Dr["REMARK"]);

                        DrFinal["GRDFLAG"] = 0;
                        DrFinal["TYPEFLAG"] = 0;
                        DrFinal["UPLOADFILENAME"] = StrUploadFilename;
                        DrFinal["SRNO"] = IntSrNo;
                        DrFinal["JOBNO"] = Dr["JOBNO"];
                        DrFinal["CONTROLNO"] = Val.ToString(Dr["CONTROLNO"]);
                        DrFinal["DIAMONDDOSSIER"] = Dr["DIAMONDDOSSIER"];
                        DrFinal["REPORTNO"] = Dr["REPORTNO"];
                        DrFinal["REPORTDATE"] = Val.ToString(Dr["REPORTDATE"]);

                        DrFinal["CLIENTREFNO"] = Dr["CLIENTREFNO"];
                        DrFinal["MEMONO"] = Dr["MEMONO"];

                        DrFinal["SHAPE"] = Dr["SHAPE"];
                        DrFinal["LENGTH"] = Dr["LENGTH"];
                        DrFinal["WIDTH"] = Dr["WIDTH"];
                        DrFinal["DEPTH"] = Dr["DEPTH"];
                        DrFinal["CARAT"] = Dr["CARAT"];
                        DrFinal["COLOR"] = Dr["COLOR"];

                        DrFinal["COLORDESC"] = Dr["COLORDESC"];

                        DrFinal["CLARITY"] = Dr["CLARITY"];
                        DrFinal["CLARITYSTATUS"] = Dr["CLARITYSTATUS"];
                        DrFinal["CUT"] = Dr["CUT"];
                        DrFinal["POLISH"] = Dr["POLISH"];
                        DrFinal["SYMMETRY"] = Dr["SYMMETRY"];
                        DrFinal["FLUORESCENCE"] = Dr["FLUORESCENCE"];
                        DrFinal["FLUORESCENCECOLOR"] = Dr["FLUORESCENCECOLOR"];

                        DrFinal["GIRDLEDESC"] = Dr["GIRDLEDESC"];
                        DrFinal["GIRDLECONDITION"] = Dr["GIRDLECONDITION"];

                        DrFinal["CULETSIZE"] = Dr["CULETSIZE"];
                        DrFinal["DEPTHPER"] = Dr["DEPTHPER"];
                        DrFinal["TABLE1"] = Dr["TABLE1"];


                        DrFinal["CRANGLE"] = Regex.Replace(Val.ToString(Dr["CRANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["CRHEIGHT"] = Regex.Replace(Val.ToString(Dr["CRHEIGHT"]), @"[^0-9\.]+", "");
                        DrFinal["PAVANGLE"] = Regex.Replace(Val.ToString(Dr["PAVANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["PAVHEIGHT"] = Regex.Replace(Val.ToString(Dr["PAVHEIGHT"]), @"[^0-9\.]+", "");

                        DrFinal["STARLENGTH"] = Regex.Replace(Val.ToString(Dr["STARLENGTH"]), @"[^0-9\.]+", "");
                        DrFinal["LOWERHALF"] = Regex.Replace(Val.ToString(Dr["LOWERHALF"]), @"[^0-9\.]+", "");

                        DrFinal["PAINTING"] = Dr["PAINTING"];
                        DrFinal["PROPORTIONS"] = Dr["PROPORTIONS"];
                        DrFinal["PAINTCOMM"] = Dr["PAINTCOMM"];

                        DrFinal["KEYTOSYMBOL"] = Dr["KEYTOSYMBOL"];

                        DrFinal["REPORTCOMMENT"] = Dr["REPORTCOMMENT"];

                        DrFinal["INSCRIPTION"] = Dr["INSCRIPTION"];

                        DrFinal["SYNTHETICINDICATOR"] = Dr["SYNTHETICINDICATOR"];
                        DrFinal["GIRDLEPER"] = Dr["GIRDLEPER"];
                        DrFinal["POLISHFEATURES"] = Dr["POLISHFEATURES"];
                        DrFinal["SYMMETRYFEATURES"] = Dr["SYMMETRYFEATURES"];
                        DrFinal["SHAPEDESC"] = Dr["SHAPEDESC"];

                        DrFinal["REPORTTYPE"] = CmbLabType.Text == "GIA" ? Dr["REPORTTYPE"] : "";

                        if (RbtResult.Checked == true)
                        {
                            DrFinal["GIARESULTSTATUS"] = "RESULT";
                        }
                        else if (RbtReturn.Checked == true)
                        {
                            DrFinal["GIARESULTSTATUS"] = "RETURN";
                        }

                        IntSrNo++;

                        DtabFinal.Rows.Add(DrFinal);
                    }

                    DtabFileUpload = ObjUpload.Save(DtabFinal, mGroup_ID, Val.ToString(CmbLabType.Text), Val.SqlDate(DTPUploadDate.Value.ToShortDateString()));
                }
                else if (CmbLabType.Text == "IGI")
                {
                    foreach (DataRow Dr in DtabExcelData.Rows)
                    {
                        string Str = "";
                        string PacketNo = "";
                        string StrPcketTag = "";
                        DataRow DrFinal = DtabFinal.NewRow();
                        DrFinal["UPLOAD_ID"] = Guid.NewGuid();
                        DrFinal["PACKET_ID"] = Guid.Empty;
                        DrFinal["GROUP_ID"] = mGroup_ID;

                        DrFinal["UPLOADDATE"] = Val.ToString(DTPUploadDate.Text);
                        DrFinal["UPLOADTYPE"] = Val.ToString(CmbLabType.Text);

                        if (Val.ToString(Dr["CLIENTREFNO"]).Trim().Equals(string.Empty))
                            continue;

                        //if (Val.ToString(CmbLabType.Text).Trim().ToUpper() == "IGI")
                        if (Val.ToString(Dr["CLIENTREFNO"]).Contains("/"))
                        {

                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("/")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("/") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty).Replace("-", "");
                        }
                        else if (Val.ToString(Dr["CLIENTREFNO"]).Contains("-"))
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("-")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("-") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty);
                        }
                        else
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]);
                            DrFinal["PACKETNO"] = "";
                        }
                        StrPcketTag = Regex.Replace(Str.ToString(), @"[\d-]", "");

                        DrFinal["PACKETNO"] = Val.ToString(PacketNo);
                        DrFinal["TAG"] = Val.ToString(StrPcketTag);


                        DrFinal["STATUS"] = Dr["STATUS"];
                        DrFinal["REMARK"] = Val.ToString(Dr["REMARK"]);

                        DrFinal["GRDFLAG"] = 0;
                        DrFinal["TYPEFLAG"] = 0;
                        DrFinal["UPLOADFILENAME"] = StrUploadFilename;
                        DrFinal["SRNO"] = IntSrNo;
                        DrFinal["JOBNO"] = Dr["JOBNO"];
                        DrFinal["CONTROLNO"] = Val.ToString(Dr["CONTROLNO"]);
                        DrFinal["DIAMONDDOSSIER"] = "";
                        DrFinal["REPORTNO"] = Dr["REPORTNO"];
                        DrFinal["REPORTDATE"] = Val.ToString(Dr["REPORTDATE"]);

                        DrFinal["CLIENTREFNO"] = Dr["CLIENTREFNO"];
                        DrFinal["MEMONO"] = Dr["MEMONO"];

                        DrFinal["SHAPE"] = Dr["SHAPE"];
                        DrFinal["LENGTH"] = Val.ToString(Dr["LENGTH"]).Trim().Equals(string.Empty) ? "" : Val.ToString(Dr["LENGTH"]);
                        DrFinal["WIDTH"] = Dr["WIDTH"];
                        DrFinal["DEPTH"] = Dr["DEPTH"];
                        DrFinal["CARAT"] = Dr["CARAT"];
                        DrFinal["COLOR"] = Dr["COLOR"];

                        DrFinal["COLORDESC"] = Dr["COLORDESC"];

                        DrFinal["CLARITY"] = Dr["CLARITY"];
                        DrFinal["CLARITYSTATUS"] = "";
                        DrFinal["CUT"] = Dr["CUT"];
                        DrFinal["POLISH"] = Dr["POLISH"];
                        DrFinal["SYMMETRY"] = Dr["SYMMETRY"];
                        DrFinal["FLUORESCENCE"] = Dr["FLUORESCENCE"];
                        DrFinal["FLUORESCENCECOLOR"] = "";

                        DrFinal["GIRDLEDESC"] = Dr["GIRDLEDESC"];
                        DrFinal["GIRDLECONDITION"] = "";

                        DrFinal["CULETSIZE"] = Dr["CULETSIZE"];
                        DrFinal["DEPTHPER"] = Dr["DEPTHPER"];
                        DrFinal["TABLE1"] = Dr["TABLE1"];


                        DrFinal["CRANGLE"] = Regex.Replace(Val.ToString(Dr["CRANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["CRHEIGHT"] = Regex.Replace(Val.ToString(Dr["CRHEIGHT"]), @"[^0-9\.]+", "");
                        DrFinal["PAVANGLE"] = Regex.Replace(Val.ToString(Dr["PAVANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["PAVHEIGHT"] = Regex.Replace(Val.ToString(Dr["PAVHEIGHT"]), @"[^0-9\.]+", "");

                        DrFinal["STARLENGTH"] = "";
                        DrFinal["LOWERHALF"] = "";

                        DrFinal["PAINTING"] = "";
                        DrFinal["PROPORTIONS"] = "";
                        DrFinal["PAINTCOMM"] = "";

                        DrFinal["KEYTOSYMBOL"] = "";

                        DrFinal["REPORTCOMMENT"] = Dr["REPORTCOMMENT"];

                        DrFinal["INSCRIPTION"] = "";

                        DrFinal["SYNTHETICINDICATOR"] = "";
                        DrFinal["GIRDLEPER"] = Dr["GIRDLEPER"];
                        DrFinal["POLISHFEATURES"] = "";
                        DrFinal["SYMMETRYFEATURES"] = "";
                        DrFinal["SHAPEDESC"] = "";

                        DrFinal["REPORTTYPE"] = "";

                        DrFinal["DIAMONDLINK"] = "";

                        DrFinal["SPECIALCOMMENT"] = Dr["SPECIALCOMMENT"];
                        //DrFinal["RESULT"] = Dr["RESULT"];

                        IntSrNo++;

                        DtabFinal.Rows.Add(DrFinal);
                    }
                    DtabFileUpload = ObjUpload.Save(DtabFinal, mGroup_ID, Val.ToString(CmbLabType.Text), Val.SqlDate(DTPUploadDate.Value.ToShortDateString()));
                }
                else if (CmbLabType.Text == "HRD")
                {
                    foreach (DataRow Dr in DtabExcelData.Rows)
                    {
                        string Str = "";
                        string PacketNo = "";
                        string StrPcketTag = "";
                        DataRow DrFinal = DtabFinal.NewRow();
                        DrFinal["UPLOAD_ID"] = Guid.NewGuid();
                        DrFinal["PACKET_ID"] = Guid.Empty;
                        DrFinal["GROUP_ID"] = mGroup_ID;

                        DrFinal["UPLOADDATE"] = Val.ToString(DTPUploadDate.Text);
                        DrFinal["UPLOADTYPE"] = Val.ToString(CmbLabType.Text);

                        if (Val.ToString(Dr["CLIENTREFNO"]).Trim().Equals(string.Empty))
                            continue;

                        //if (Val.ToString(CmbLabType.Text).Trim().ToUpper() == "IGI")
                        if (Val.ToString(Dr["CLIENTREFNO"]).Contains("/"))
                        {

                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("/")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("/") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty).Replace("-", "");
                        }
                        else if (Val.ToString(Dr["CLIENTREFNO"]).Contains("-"))
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("-")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("-") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty);
                        }
                        else
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]);
                            DrFinal["PACKETNO"] = "";
                        }
                        StrPcketTag = Regex.Replace(Str.ToString(), @"[\d-]", "");

                        DrFinal["PACKETNO"] = Val.ToString(PacketNo);
                        DrFinal["TAG"] = Val.ToString(StrPcketTag);


                        DrFinal["STATUS"] = Dr["STATUS"];
                        DrFinal["REMARK"] = Val.ToString(Dr["REMARK"]);

                        DrFinal["GRDFLAG"] = 0;
                        DrFinal["TYPEFLAG"] = 0;
                        DrFinal["UPLOADFILENAME"] = StrUploadFilename;
                        DrFinal["SRNO"] = IntSrNo;
                        DrFinal["JOBNO"] = Dr["JOBNO"];
                        DrFinal["CONTROLNO"] = string.Empty; // Val.ToString(Dr["CONTROLNO"]);
                        DrFinal["DIAMONDDOSSIER"] = "";
                        DrFinal["REPORTNO"] = Dr["REPORTNO"];
                        DrFinal["REPORTDATE"] = Val.ToString(Dr["REPORTDATE"]);

                        DrFinal["CLIENTREFNO"] = Dr["CLIENTREFNO"];
                        DrFinal["MEMONO"] = string.Empty; // Dr["MEMONO"];

                        DrFinal["SHAPE"] = Dr["SHAPE"];
                        DrFinal["LENGTH"] = Val.ToString(Dr["LENGTH"]).Trim().Equals(string.Empty) ? "" : Val.ToString(Dr["LENGTH"]);
                        DrFinal["WIDTH"] = Dr["WIDTH"];
                        DrFinal["DEPTH"] = Dr["DEPTH"];
                        DrFinal["CARAT"] = Dr["CARAT"];
                        DrFinal["COLOR"] = Dr["COLOR"];

                        DrFinal["COLORDESC"] = Dr["COLORDESC"]; // Dr["COLORDESC"];

                        DrFinal["CLARITY"] = Dr["CLARITY"];
                        DrFinal["CLARITYSTATUS"] = "";
                        DrFinal["CUT"] = Dr["CUT"];
                        DrFinal["POLISH"] = Dr["POLISH"];
                        DrFinal["SYMMETRY"] = Dr["SYMMETRY"];
                        DrFinal["FLUORESCENCE"] = Dr["FLUORESCENCE"];
                        DrFinal["FLUORESCENCECOLOR"] = "";

                        DrFinal["GIRDLEDESC"] = Dr["GIRDLEDESC"];
                        DrFinal["GIRDLECONDITION"] = "";

                        DrFinal["CULETSIZE"] = Dr["CULETSIZE"];
                        DrFinal["DEPTHPER"] = Dr["DEPTHPER"];
                        DrFinal["TABLE1"] = Dr["TABLE1"];


                        DrFinal["CRANGLE"] = Regex.Replace(Val.ToString(Dr["CRANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["CRHEIGHT"] = Regex.Replace(Val.ToString(Dr["CRHEIGHT"]), @"[^0-9\.]+", "");
                        DrFinal["PAVANGLE"] = Regex.Replace(Val.ToString(Dr["PAVANGLE"]), @"[^0-9\.]+", "");
                        DrFinal["PAVHEIGHT"] = Regex.Replace(Val.ToString(Dr["PAVHEIGHT"]), @"[^0-9\.]+", "");

                        DrFinal["STARLENGTH"] = "";
                        DrFinal["LOWERHALF"] = "";

                        DrFinal["PAINTING"] = "";
                        DrFinal["PROPORTIONS"] = "";
                        DrFinal["PAINTCOMM"] = "";

                        DrFinal["KEYTOSYMBOL"] = "";

                        DrFinal["REPORTCOMMENT"] = ""; // Dr["REPORTCOMMENT"];

                        DrFinal["INSCRIPTION"] = "";

                        DrFinal["SYNTHETICINDICATOR"] = "";
                        DrFinal["GIRDLEPER"] = Dr["GIRDLEPER"];
                        DrFinal["POLISHFEATURES"] = "";
                        DrFinal["SYMMETRYFEATURES"] = "";
                        DrFinal["SHAPEDESC"] = "";

                        DrFinal["REPORTTYPE"] = "";

                        DrFinal["DIAMONDLINK"] = "";

                        DrFinal["SPECIALCOMMENT"] = ""; // Dr["SPECIALCOMMENT"];
                        //DrFinal["RESULT"] = Dr["RESULT"];

                        IntSrNo++;

                        DtabFinal.Rows.Add(DrFinal);
                    }
                    DtabFileUpload = ObjUpload.Save(DtabFinal, mGroup_ID, Val.ToString(CmbLabType.Text), Val.SqlDate(DTPUploadDate.Value.ToShortDateString()));
                }
                else if (CmbLabType.Text == "SALES" || CmbLabType.Text == "BOMBAY")
                {
                    foreach (DataRow Dr in DtabExcelData.Rows)
                    {

                        if (Val.ToString(Dr["CLIENTREFNO"]).Trim().Equals(string.Empty))
                            continue;

                        string Str = "";
                        string PacketNo = "";
                        string StrPcketTag = "";
                        DataRow DrFinal = DtabFinal.NewRow();
                        DrFinal["UPLOAD_ID"] = Guid.NewGuid();
                        DrFinal["PACKET_ID"] = Guid.Empty;
                        DrFinal["GROUP_ID"] = mGroup_ID;

                        DrFinal["UPLOADDATE"] = Val.ToString(DTPUploadDate.Text);
                        DrFinal["UPLOADTYPE"] = Val.ToString(CmbLabType.Text);

                        if (Val.ToString(Dr["CLIENTREFNO"]).Contains("-"))
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]).Substring(0, Val.ToString(Dr["CLIENTREFNO"]).IndexOf("-")).Trim();
                            Str = Dr["CLIENTREFNO"].ToString().Substring(Val.ToString(Dr["CLIENTREFNO"]).LastIndexOf("-") + 1);
                            PacketNo = Regex.Replace(Val.ToString(Str), "[^0-9]+", string.Empty);
                        }
                        else
                        {
                            DrFinal["KAPANNAME"] = Val.ToString(Dr["CLIENTREFNO"]);
                            DrFinal["PACKETNO"] = "";
                        }
                        StrPcketTag = Regex.Replace(Str.ToString(), @"[\d-]", "");

                        DrFinal["PACKETNO"] = Val.ToString(PacketNo);
                        DrFinal["TAG"] = Val.ToString(StrPcketTag);

                        DrFinal["STATUS"] = Dr["STATUS"];

                        DrFinal["BACKDISC"] = "";
                        DrFinal["SALEPRICE"] = "";
                        DrFinal["SALEAMOUNT"] = CmbLabType.Text == "SALES" ? Dr["SALEAMOUNT"] : "";
                        DrFinal["LABCHARGE"] = "";
                        DrFinal["AMOUNT"] = Dr["AMOUNT"];
                        DrFinal["UPLOADFILENAME"] = StrUploadFilename;

                        if (CmbLabType.Text == "BOMBAY")
                        {
                            DrFinal["REMARK"] = Dr["REMARK"];

                            DrFinal["GRDFLAG"] = 0;
                            DrFinal["TYPEFLAG"] = 0;
                            
                            DrFinal["SRNO"] = IntSrNo;

                            DrFinal["CLIENTREFNO"] = Dr["CLIENTREFNO"];
                            DrFinal["GRDDONEBY"] = Dr["GRDDONEBY"];
                            DrFinal["REPORTDATE"] = Val.ToString(Dr["REPORTDATE"]);

                            DrFinal["CARAT"] = Dr["CARAT"];
                            DrFinal["SHAPE"] = Dr["SHAPE"];
                            DrFinal["COLOR"] = Dr["COLOR"];
                            DrFinal["CLARITY"] = Dr["CLARITY"];
                            DrFinal["CUT"] = Dr["CUT"];
                            DrFinal["POLISH"] = Dr["POLISH"];
                            DrFinal["SYMMETRY"] = Dr["SYMMETRY"];
                            DrFinal["FLUORESCENCE"] = Dr["FLUORESCENCE"];

                            DrFinal["RAPAPORT"] = Dr["RAPAPORT"];
                            DrFinal["BACK"] = Dr["BACK"];
                            DrFinal["PRICEPERCARAT"] = Dr["PRICEPERCARAT"];
                            DrFinal["AMOUNT"] = Dr["AMOUNT"];

                            //DrFinal["BACKDISC"] = CmbLabType.Text == "SALES" ? Dr["BACKDISC"] : "";
                            //DrFinal["SALEPRICE"] = CmbLabType.Text == "SALES" ? Dr["SALEPRICE"] : "";
                            //DrFinal["SALEAMOUNT"] = CmbLabType.Text == "SALES" ? Dr["SALEAMOUNT"] : "";
                            //DrFinal["LABCHARGE"] = CmbLabType.Text == "SALES" ? Dr["LABCHARGE"] : "";

                            DrFinal["REMARK"] = Dr["REMARK"];

                            DrFinal["MILKY"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["MILKY"]) : "";
                            DrFinal["LBLC"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["LBLC"]) : "";
                            DrFinal["NATTS"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["NATTS"]) : "";
                            DrFinal["HA"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["HA"]) : "";
                            DrFinal["PAV"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["PAV"]) : "";
                            DrFinal["BINC"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["BINC"]) : "";
                            DrFinal["OINC"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["OINC"]) : "";
                            DrFinal["WINC"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["WINC"]) : "";
                            DrFinal["TENSION"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["TENSION"]) : "";
                            DrFinal["CS"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["CS"]) : "";
                            DrFinal["LUSTER"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["LUSTER"]) : "";
                            DrFinal["NATURAL"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["NATURAL"]) : "";
                            DrFinal["GRAIN"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["GRAIN"]) : "";
                            DrFinal["EYECLEAN"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["EYECLEAN"]) : "";
                            DrFinal["LAB"] = CmbLabType.Text == "BOMBAY" ? Val.ToString(Dr["LAB"]) : "";
                        }

                        IntSrNo++;
                        DtabFinal.Rows.Add(DrFinal);
                    }
                    DtabFileUpload = ObjUpload.Save(DtabFinal, mGroup_ID, CmbLabType.Text, Val.SqlDate(DTPUploadDate.Value.ToShortDateString()));
                }

                if (DtabFileUpload.Rows.Count > 0)
                {
                    this.Cursor = Cursors.Default;
                    Global.Message("File Upload Successfully");
                    GrdDet.Columns.Clear();

                    MainGrdSearch.SendToBack();
                    MainGrid.BringToFront();
                    MainGrid.Dock = DockStyle.Fill;
                    MainGrid.DataSource = DtabFileUpload;
                    MainGrid.Refresh();


                    GrdDet.Columns["PACKET_ID"].Visible = false;
                    GrdDet.Columns["UPLOAD_ID"].Visible = false;
                    GrdDet.Columns["GROUP_ID"].Visible = false;
                    GrdDet.Columns["UPLOADDATE"].Visible = false;
                    //GrdDet.Columns["UPLOADTYPE"].Visible = false;
                    GrdDet.Columns["UPLOADFILENAME"].Visible = false;
                    GrdDet.Columns["GRDFLAG"].Visible = false;
                    GrdDet.Columns["TYPEFLAG"].Visible = false;

                    GrdDet.OptionsBehavior.Editable = false;
                    ChangeGridColumnsCaption(GrdDet);
                    GrdDet.BestFitColumns();

                    mUpload_ID = Guid.Empty;
                    DtabFinal.Columns.Clear();
                }

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }
        public void ChangeGridColumnsCaption(DevExpress.XtraGrid.Views.Grid.GridView GrdView)
        {
            if (CmbLabType.Text == "GIA" || CmbLabType.Text == "IGI")
            {
                GrdView.Columns["KAPANNAME"].Caption = "Kapan";
                GrdView.Columns["PACKETNO"].Caption = "Packet No";
                GrdView.Columns["TAG"].Caption = "Tag";
                GrdView.Columns["UPLOADTYPE"].Caption = "Lab Type";
                GrdView.Columns["STATUS"].Caption = "Status";
                GrdView.Columns["SRNO"].Caption = "Sr No";
                GrdView.Columns["JOBNO"].Caption = "Job No";
                GrdView.Columns["CONTROLNO"].Caption = "Control No";
                GrdView.Columns["DIAMONDDOSSIER"].Caption = "Diamond Dossier";
                GrdView.Columns["REPORTNO"].Caption = "Report No";
                GrdView.Columns["REPORTDATE"].Caption = "Report Date";
                GrdView.Columns["CLIENTREFNO"].Caption = "Client Ref";
                GrdView.Columns["MEMONO"].Caption = "Memo No";
                GrdView.Columns["SHAPE"].Caption = "Shape";
                GrdView.Columns["LENGTH"].Caption = "Length";
                GrdView.Columns["WIDTH"].Caption = "Width";
                GrdView.Columns["DEPTH"].Caption = "Depth";

                GrdView.Columns["CARAT"].Caption = "Carat";
                GrdView.Columns["COLOR"].Caption = "Color";
                GrdView.Columns["COLORDESC"].Caption = "Color Desc";
                GrdView.Columns["CLARITY"].Caption = "Clarity";
                GrdView.Columns["CLARITYSTATUS"].Caption = "Clarity Status";
                GrdView.Columns["CUT"].Caption = "Cut";

                GrdView.Columns["POLISH"].Caption = "Polish";

                GrdView.Columns["SYMMETRY"].Caption = "Symm";
                GrdView.Columns["FLUORESCENCE"].Caption = "FL";
                GrdView.Columns["FLUORESCENCECOLOR"].Caption = "FL Color";

                GrdView.Columns["GIRDLEDESC"].Caption = "Girdle Desc";
                GrdView.Columns["GIRDLECONDITION"].Caption = "Grd Condition";
                GrdView.Columns["CULETSIZE"].Caption = "Culte Size";

                GrdView.Columns["DEPTHPER"].Caption = "Depth Per";
                GrdView.Columns["TABLE1"].Caption = "Table";
                GrdView.Columns["CRANGLE"].Caption = "Cr Angle";
                GrdView.Columns["CRHEIGHT"].Caption = "Cr Height";
                GrdView.Columns["PAVANGLE"].Caption = "Pav Angle";
                GrdView.Columns["PAVHEIGHT"].Caption = "Pav Height";

                GrdView.Columns["STARLENGTH"].Caption = "Str Length";
                GrdView.Columns["LOWERHALF"].Caption = "Lwr Half";
                GrdView.Columns["PAINTING"].Caption = "Painting";
                GrdView.Columns["PAINTCOMM"].Caption = "Paint Comm";
                GrdView.Columns["KEYTOSYMBOL"].Caption = "Key To Symbol";

                GrdView.Columns["REPORTCOMMENT"].Caption = "Report Comment";
                GrdView.Columns["INSCRIPTION"].Caption = "Inscription";
                GrdView.Columns["SYNTHETICINDICATOR"].Caption = "SyntheticIndicator";
                GrdView.Columns["GIRDLEPER"].Caption = "Girdle %";
                GrdView.Columns["POLISHFEATURES"].Caption = "Pol Features";
                GrdView.Columns["SYMMETRYFEATURES"].Caption = "Symm Features";
                GrdView.Columns["SHAPEDESC"].Caption = "Shape Desc";
                GrdView.Columns["REPORTTYPE"].Caption = "Report Type";
                GrdView.Columns["DIAMONDLINK"].Caption = "Diamond Link";

                GrdView.Columns["SPECIALCOMMENT"].Caption = "Special Comm.";
                GrdView.Columns["RESULT"].Caption = "Result";


            }
            else if (CmbLabType.Text == "SALES" || CmbLabType.Text == "BOMBAY")
            {
                GrdView.Columns["KAPANNAME"].Caption = "Kapan";
                GrdView.Columns["PACKETNO"].Caption = "Packet No";
                GrdView.Columns["TAG"].Caption = "Tag";
                GrdView.Columns["UPLOADTYPE"].Caption = "Lab Type";
                GrdView.Columns["STATUS"].Caption = "Status";
                GrdView.Columns["SRNO"].Caption = "Sr No";
                GrdView.Columns["REPORTDATE"].Caption = "Sale Date";
                GrdView.Columns["CLIENTREFNO"].Caption = "Stone No";

                GrdView.Columns["GRDDONEBY"].Caption = "Done By";

                GrdView.Columns["SHAPE"].Caption = "Shape";
                GrdView.Columns["CARAT"].Caption = "Carat";
                GrdView.Columns["COLOR"].Caption = "Color";
                GrdView.Columns["CLARITY"].Caption = "Clarity";
                GrdView.Columns["CUT"].Caption = "Cut";

                GrdView.Columns["POLISH"].Caption = "Polish";

                GrdView.Columns["SYMMETRY"].Caption = "Symm";
                GrdView.Columns["FLUORESCENCE"].Caption = "FL";

                GrdView.Columns["RAPAPORT"].Caption = "Rapaport";
                GrdView.Columns["BACK"].Caption = "Back";
                GrdView.Columns["PRICEPERCARAT"].Caption = "PricePerCarat";
                GrdView.Columns["AMOUNT"].Caption = "Amount";

                GrdView.Columns["BACKDISC"].Caption = "BackDisc";

                GrdView.Columns["SALEPRICE"].Caption = "SalePrice";
                GrdView.Columns["SALEAMOUNT"].Caption = "SaleAmount";

                GrdView.Columns["LABCHARGE"].Caption = "Lab Charge";

                GrdView.Columns["REMARK"].Caption = "Remark";

                GrdView.Columns["BACKDISC"].Visible = CmbLabType.Text == "SALES" ? true : false; ;
                GrdView.Columns["SALEPRICE"].Visible = CmbLabType.Text == "SALES" ? true : false; ;
                GrdView.Columns["SALEAMOUNT"].Visible = CmbLabType.Text == "SALES" ? true : false; ;
                GrdView.Columns["LABCHARGE"].Visible = CmbLabType.Text == "SALES" ? true : false; ;

                GrdView.Columns["MILKY"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["LBLC"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["NATTS"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["HA"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["PAV"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["BINC"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["OINC"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["WINC"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["TENSION"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["CS"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["LUSTER"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["NATURAL"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["GRAIN"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["EYECLEAN"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;
                GrdView.Columns["LAB"].Visible = CmbLabType.Text == "BOMBAY" ? true : false;

            }
        }

        private void CmbLabType_SelectedIndexChanged(object sender, EventArgs e)
        {
            //try
            //{

            //    if (CmbLabType.Text == "GIA" || CmbLabType.Text == "IGI" || CmbLabType.Text == "HRD")
            //        CmbSheetName.Enabled = false;
            //    else
            //        CmbSheetName.Enabled = true;

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        private void BtnImport_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.Columns.Count > 0)
                    GrdDet.Columns.Clear();

                MainGrid.DataSource = null;

                if (!DtabExcelData.Columns.Contains(Val.ToString("STATUS").ToUpper()))
                {
                    DtabExcelData.Columns.Add("STATUS", typeof(string));
                }

                MainGrid.DataSource = DtabExcelData;
                MainGrid.Refresh();
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            try
            {

                this.Cursor = Cursors.WaitCursor;
                string StrFromDate = "", StrToDate = "", StrKapan = "", StrPacket = "", StrPacketTag = "", StrLabType = "";

                StrFromDate = Val.SqlDate(DTPFromDate.Text);
                StrToDate = Val.SqlDate(DTPToDate.Text);
                StrKapan = Val.ToString(txtKapan.Text);
                StrPacket = Val.ToString(txtPacketNo.Text);
                StrPacketTag = Val.ToString(txtPacketTag.Text);
                //StrLabType = Val.ToString(ChkCmbLab.Text);

                String[] Str = ChkCmbLab.Text.Split(',');

                if (!ChkCmbLab.Text.ToString().Trim().Equals(string.Empty))
                    foreach (string str1 in Str)
                    {
                        if (StrLabType.ToString().Trim().Equals(string.Empty))
                            StrLabType = str1.Trim();
                        else
                            StrLabType = StrLabType + "," + str1.Trim();
                    }

                DataTable DtabSearch = ObjUpload.GetFileUploadData(Guid.Empty, StrFromDate, StrToDate, StrKapan, StrPacket, StrPacketTag, StrLabType);

                MainGrdSearch.DataSource = DtabSearch;
                GrdSearch.RefreshData();

                if (!StrLabType.Trim().Equals(string.Empty))
                {
                    GrdSearch.Bands["BANDSALESBOMBAY"].Visible = false;
                    GrdSearch.Bands["BANDGIAIGIHRD"].Visible = false;

                    if (StrLabType.Trim().Contains("GIA") || StrLabType.Trim().Contains("IGI") || StrLabType.Trim().Contains("HRD"))
                        GrdSearch.Bands["BANDGIAIGIHRD"].Visible = true;
                    if ((StrLabType.Trim().Contains("SALES") || StrLabType.Trim().Contains("BOMBAY")))
                        GrdSearch.Bands["BANDSALESBOMBAY"].Visible = true;
                }
                else
                {
                    GrdSearch.Bands["BANDSALESBOMBAY"].Visible = true;
                    GrdSearch.Bands["BANDGIAIGIHRD"].Visible = true;

                }



                MainGrdSearch.Dock = DockStyle.Fill;
                MainGrdSearch.BringToFront();
                MainGrid.SendToBack();

                this.Cursor = Cursors.Default;



            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void lblDownload_Click(object sender, EventArgs e)
        {
            if (Val.ToString(CmbLabType.SelectedItem) == "GIA")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\GIAGrading.xlsx", "CMD");
            }
            else if (Val.ToString(CmbLabType.SelectedItem) == "IGI")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\IGIGrading.xlsx", "CMD");
            }
            else if (Val.ToString(CmbLabType.SelectedItem) == "BOMBAY")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\BYGradingFormat.xlsx", "CMD");
            }
            else if (Val.ToString(CmbLabType.SelectedItem) == "SALES")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\SaleUploadFormat.xlsx", "CMD");
            }
            else if (Val.ToString(CmbLabType.SelectedItem) == "HRD")
            {
                System.Diagnostics.Process.Start(Application.StartupPath + "\\Format\\HRDGrading.xlsx", "CMD");
            }
        }


    }
}

