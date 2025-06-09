using AxoneMFGRJ.Utility;
using BusLib.Configuration;
using BusLib.Transaction;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AxoneMFGRJ.ReportGrid
{
    public partial class FrmSaleDemandEntry : Form
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SaleDemandEntry Obj = new BOTRN_SaleDemandEntry();

        DataTable DtabSummry = new DataTable();
        DataTable DtabStockUpload = new DataTable();
        DataTable DtabPara = new DataTable();
        DataTable DtabExcelData = new DataTable();
        DataTable DtabDetail = new DataTable();
        DataTable DTabVerfied = new DataTable();

        BODevGridSelection ObjGridSelection;
        string Mode = "";

        public FrmSaleDemandEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormEvents();

            DtabExcelData.Columns.Add("SALEDEMAND_ID", typeof(string));
            DtabExcelData.Columns.Add("SRNO", typeof(int));
            DtabExcelData.Columns.Add("ORDERNO", typeof(string));
            DtabExcelData.Columns.Add("SHAPE", typeof(string));
            DtabExcelData.Columns.Add("FROMCARAT", typeof(string));
            DtabExcelData.Columns.Add("TOCARAT", typeof(string));
            DtabExcelData.Columns.Add("FROMCLARITY", typeof(string));
            DtabExcelData.Columns.Add("TOCLARITY", typeof(string));
            DtabExcelData.Columns.Add("FROMCOLOR", typeof(string));
            DtabExcelData.Columns.Add("TOCOLOR", typeof(string));
            DtabExcelData.Columns.Add("FROMFL", typeof(string));
            DtabExcelData.Columns.Add("TOFL", typeof(string));
            DtabExcelData.Columns.Add("CUT", typeof(string));
            DtabExcelData.Columns.Add("QTY", typeof(int));
            DtabExcelData.Columns.Add("PERCARAT", typeof(double));
            DtabExcelData.Columns.Add("LABTYPE", typeof(string));
            DtabExcelData.Columns.Add("ORDERDATE", typeof(DateTime));
            DtabExcelData.Columns.Add("ORDERCOMPDATE", typeof(DateTime));
            DtabExcelData.Columns.Add("REMARK", typeof(string));
            DtabExcelData.Columns.Add("ISACTIVE", typeof(Boolean));

            DataRow DR = DtabExcelData.NewRow();
            DR["SRNO"] = 1;
            DtabExcelData.Rows.Add(DR);

            GrdDetSummry.BeginUpdate();
            if (MainGridSummry.RepositoryItems.Count == 12)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDetSummry;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }

            GrdDetSummry.Columns["COLSELECTCHECKBOX"].Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Left;
            GrdDetSummry.EndUpdate();

            MainGridSummry.DataSource = DtabExcelData;
            MainGridSummry.RefreshDataSource();

            DtabPara = Obj.GetParameterData();
            //GrdDetSummry.BestFitColumns();

            this.Show();
        }

        public void AttachFormEvents()
        {
            ObjFormEvent.mForm = this;
            //objBOFormEvents.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {


                OpenFileDialog OpenFileDialog = new OpenFileDialog();
                OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
                //OpenFileDialog.Filter = "All files (*.*)|*.*|All files (*.*)|*.*";
                if (OpenFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    TxtFileName.Text = OpenFileDialog.FileName;

                    string extension = Path.GetExtension(TxtFileName.Text.ToString());
                    string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(TxtFileName.Text);
                    destinationPath = destinationPath.Replace(extension, ".xlsx");
                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
                    }
                    File.Copy(TxtFileName.Text, destinationPath);

                    if (File.Exists(destinationPath))
                    {
                        File.Delete(destinationPath);
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

        private void lblSampleExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFilePathDestination = "";
                StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\SaleDemand_" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
                if (File.Exists(StrFilePathDestination))
                {
                    File.Delete(StrFilePathDestination);
                }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\SaleDemand_.xlsx", StrFilePathDestination);

                System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
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
                if (TxtFileName.Text == "" || TxtFileName.Text.Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Excel File To Upload");
                    TxtFileName.Focus();
                    return;
                }

                this.Cursor = Cursors.WaitCursor;
                BtnBrowse.Enabled = false;
                DtabExcelData.Columns.Clear();

                DtabExcelData.Rows.Clear();
                string extension = Path.GetExtension(TxtFileName.Text.ToString());
                string destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(TxtFileName.Text);
                destinationPath = destinationPath.Replace(extension, ".xlsx");
                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }
                File.Copy(TxtFileName.Text, destinationPath);

                DtabExcelData = GetDataTableFromExcel(destinationPath);

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }

                ObjGridSelection.ClearSelection();


                DtabExcelData.Columns.Add("SALEDEMAND_ID", typeof(string));
                DtabExcelData.Columns.Add("ORDERNO", typeof(string));
                DtabExcelData.Columns.Add("SRNO", typeof(Int32));
                DtabExcelData.Columns.Add("ISACTIVE", typeof(Boolean));

                int intSrNo = 0;
                for (int i = 0; i < DtabExcelData.Rows.Count; i++)
                {
                    string StrOrder = "";

                    DataRow DR = DtabExcelData.Rows[i];
                    StrOrder = "" + DR["SHAPE"] + "-" + DR["FROMCARAT"] + "" + DR["TOCARAT"] + "-" + DR["FROMCLARITY"] + "" + DR["TOCLARITY"] + "-"
                                  + DR["FROMCOLOR"] + "" + DR["TOCOLOR"] + "-" + DR["FROMFL"] + "" + DR["TOFL"] + "-" + DR["ORDERDATE"] + "";

                    if (i == 0)
                    {
                        intSrNo = 1;
                    }
                    else
                    {
                        intSrNo = intSrNo + 1;
                    }
                    DR["ISACTIVE"] = true;
                    DR["ORDERNO"] = StrOrder.ToUpper();
                    DR["SRNO"] = intSrNo;
                    //DR["SALEDEMAND_ID"] = Guid.NewGuid();
                }

                int IntSr = 0;

                IntSr = intSrNo + 1;

                DataRow DRo = DtabExcelData.Rows.Add();
                DRo["SRNO"] = IntSr;

                //DtabExcelData=(DtabExcelData.NewRow(DRo));
                MainGridSummry.DataSource = DtabExcelData;
                this.Cursor = Cursors.Default;
                BtnBrowse.Enabled = true;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
                BtnBrowse.Enabled = true;
            }
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            try
            {
                TxtFileName.Text.Trim().Equals(string.Empty);
                TxtFileName.Text = "";
                LblMode.Text = "Add Mode";
                //DtabExcelData.Columns.Clear();
                DtabExcelData.Rows.Clear();
                DTabVerfied.Rows.Clear();

                ObjGridSelection.ClearSelection();

                DtabExcelData.Rows.Add(DtabExcelData.NewRow());
                MainGridSummry.DataSource = DtabExcelData;
                MainGridSummry.RefreshDataSource();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("ARE YOU WANT TO CLOSE THIS FORM.?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                this.Close();
            }

        }
        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
        {
            DataTable resultTable = new DataTable();
            try
            {
                if (view.RowCount <= 0)
                {
                    return null;
                }
                ArrayList aryLst = new ArrayList();



                DataTable sourceTable = null;
                sourceTable = ((DataView)view.DataSource).Table;

                if (IsSelect)
                {
                    aryLst = ObjGridSelection.GetSelectedArrayList();
                    resultTable = sourceTable.Clone();
                    for (int i = 0; i < aryLst.Count; i++)
                    {
                        DataRowView oDataRowView = aryLst[i] as DataRowView;
                        resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                    }
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

            return resultTable;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                DTabVerfied = GetTableOfSelectedRows(GrdDetSummry, true);

                if (DTabVerfied.Rows.Count <= 0)
                {
                    Global.Message("PLEASE SELECT RECORDS THAT YOU WANT TO SAVE..");
                    return;
                }

                foreach (DataRow DRow in DTabVerfied.Rows)
                {
                    if (Val.ToString(DRow["ORDERNO"]).Length == 0)
                    {
                        continue;
                    }

                    if (Val.ToString(DRow["SHAPE"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SHAPE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["SHAPE"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("Shape [" + Val.ToString(DRow["SHAPE"]) + "] Is Not Valid In Order No : '" + DRow["ORDERNO"] + "'");
                            return;
                        }
                    }
                    if (Val.ToString(DRow["FROMCOLOR"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "PARANAME", Val.ToString(DRow["FROMCOLOR"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("From Color -> '" + Val.ToString(DRow["FROMCOLOR"]) + "' Is Not Valid In Order No : '" + DRow["ORDERNO"] + "'");
                            return;
                        }
                    }
                    if (Val.ToString(DRow["TOCOLOR"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "PARANAME", Val.ToString(DRow["TOCOLOR"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("To Color -> '" + Val.ToString(DRow["TOCOLOR"]) + "' Is Not Valid In Order No : '" + DRow["ORDERNO"] + "'");
                            return;
                        }
                    }
                    if (Val.ToString(DRow["FROMCLARITY"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "PARANAME", Val.ToString(DRow["FROMCLARITY"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("From Clarity -> '" + Val.ToString(DRow["FROMCLARITY"]) + "' Is Not Valid In Order No : '" + DRow["ORDERNO"] + "'");
                            return;
                        }
                    }
                    if (Val.ToString(DRow["TOCLARITY"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "PARANAME", Val.ToString(DRow["TOCLARITY"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("To Clarity -> '" + Val.ToString(DRow["TOCLARITY"]) + "' Is Not Valid In Order No : '" + DRow["ORDERNO"] + "'");
                            return;
                        }
                    }

                    //if (Val.ToInt(DRow["SRNO"]) == "" && DRow["SRNO"].Equals(string.Empty))
                    //{
                    //	Global.Message("SRNO IS REQUIRED IN ORDER NO '"+DRow["ORDERNO"]+"'");
                    //	return;
                    //}

                    if (Val.ToString(DRow["ORDERNO"]) == "" && DRow["ORDERNO"].Equals(string.Empty))
                    {
                        Global.Message("ORDERNO IS REQUIRED IN SRNO '" + DRow["SRNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["FROMCARAT"]) == "" && DRow["FROMCARAT"].Equals(string.Empty))
                    {
                        Global.Message("FROMCARAT IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["TOCARAT"]) == "" && DRow["TOCARAT"].Equals(string.Empty))
                    {
                        Global.Message("TOCARAT IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["FROMCLARITY"]) == "" && DRow["FROMCLARITY"].Equals(string.Empty))
                    {
                        Global.Message("FROMCLARITY IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["TOCLARITY"]) == "" && DRow["TOCLARITY"].Equals(string.Empty))
                    {
                        Global.Message("TOCLARITY IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["FROMCOLOR"]) == "" && DRow["FROMCOLOR"].Equals(string.Empty))
                    {
                        Global.Message("FROMCOLOR IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["TOCOLOR"]) == "" && DRow["TOCOLOR"].Equals(string.Empty))
                    {
                        Global.Message("TOCOLOR IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["FROMFL"]) == "" && DRow["FROMFL"].Equals(string.Empty))
                    {
                        Global.Message("FROMFL IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["TOFL"]) == "" && DRow["TOFL"].Equals(string.Empty))
                    {
                        Global.Message("TOFL IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["CUT"]) == "" && DRow["CUT"].Equals(string.Empty))
                    {
                        Global.Message("CUT IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["QTY"]) == "" && DRow["QTY"].Equals(string.Empty))
                    {
                        Global.Message("QTY IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["PERCARAT"]) == "" && DRow["PERCARAT"].Equals(string.Empty))
                    {
                        Global.Message("PERCARAT IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["LABTYPE"]) == "" && DRow["LABTYPE"].Equals(string.Empty))
                    {
                        Global.Message("LABTYPE IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");

                        return;
                    }

                    if (Val.ToString(DRow["ORDERDATE"]) == "")
                    {
                        Global.Message("ORDERDATE IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }

                    if (Val.ToString(DRow["ORDERCOMPDATE"]) == "")
                    {
                        Global.Message("ORDERCOMPDATE IS REQUIRED IN ORDER NO '" + DRow["ORDERNO"] + "'");
                        return;
                    }
                    
                }

                if (Global.Confirm("ARE YOU WANT TO SAVE SELECTED ORDERS.?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                this.Cursor = Cursors.WaitCursor;

                string StrID = "";

                if (LblMode.Text == "Add Mode")
                {
                    Mode = "ADD MODE";

                    string OrderDetailXML = string.Empty;
                    DTabVerfied.TableName = "Table";
                    using (StringWriter sw = new StringWriter())
                    {
                        DTabVerfied.WriteXml(sw);
                        OrderDetailXML = sw.ToString();
                    }
                    string IntRes = Obj.OrderDemandSave(OrderDetailXML, Mode, "");
                    if (IntRes == "SUCCESS")
                    {
                        Global.Message("ORDER SAVE SUCCESSFULLY..");
                    }
                    BtnClear.PerformClick();
                    TxtFileName.Focus();
                    this.Cursor = Cursors.Default;
                }
                else
                {
                    Mode = "EDIT MODE";

                    string OrderDetailXML = string.Empty;
                    DtabExcelData.TableName = "Table";
                    using (StringWriter sw = new StringWriter())
                    {
                        DtabExcelData.WriteXml(sw);
                        OrderDetailXML = sw.ToString();
                    }
                    string IntRes = Obj.OrderDemandSave(OrderDetailXML, Mode, "");
                    if (IntRes == "SUCCESS")
                    {
                        Global.Message("ORDER UPDATE SUCCESSFULLY..");
                    }
                    BtnClear.PerformClick();
                    TxtFileName.Focus();
                    this.Cursor = Cursors.Default;
                }
            }
            catch (Exception EX)
            {
                Global.Message(EX.Message);
                this.Cursor = Cursors.Default;
            }
        }

        private void BtnShow_Click(object sender, EventArgs e)
        {
            string StrOrderFromDate = "";
            string StrOrderToDate = "";
            string StrOrderCompFromDate = "";
            string StrOrderCompToDate = "";

            this.Cursor = Cursors.WaitCursor;

            if (OrderFromDate.Checked == true && OrderToDate.Checked == true)
            {
                StrOrderFromDate = Val.SqlDate(OrderFromDate.Text);
                StrOrderToDate = Val.SqlDate(OrderToDate.Text);
            }
            else
            {
                StrOrderFromDate = null;
                StrOrderToDate = null;
            }
            if (OrderCompFromDate.Checked == true && OrderCompToDate.Checked == true)
            {
                StrOrderCompFromDate = Val.SqlDate(OrderCompFromDate.Text);
                StrOrderCompToDate = Val.SqlDate(OrderCompToDate.Text);
            }
            else
            {
                StrOrderCompFromDate = null;
                StrOrderCompToDate = null;
            }

            DtabDetail = Obj.OrderDetail(StrOrderFromDate, StrOrderToDate, StrOrderCompFromDate, StrOrderCompToDate, "ALL", "");

            MainGrdDetail.DataSource = DtabDetail;
            MainGrdDetail.RefreshDataSource();
            GrdDetDetail.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void GrdDetDetail_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            try
            {

                if (e.Clicks == 2)
                {
                    DataRow DR = GrdDetDetail.GetFocusedDataRow();

                    if (e.Column.FieldName == "INPROCESS")
                    {
                        DataTable DTabInProcssDeail = Obj.OrderDetail(null, null, null, null, "INPROCESS", Val.ToString(DR["SALEDEMAND_ID"]));
                        if (DTabInProcssDeail.Rows.Count > 0)
                        {
                            FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                            // FrmPopupGrid.DTab = DtData;                   
                            FrmPopupGrid.CountedColumn = "PACKETNO";
                            FrmPopupGrid.ColumnsToHide = "SALEDEMAND_ID,LAB_ID,RNO";
                            FrmPopupGrid.MainGrid.DataSource = DTabInProcssDeail;
                            FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                            FrmPopupGrid.Text = "List Of Packets Which Is In Order(Last Prd).";
                            FrmPopupGrid.ISPostBack = true;
                            this.Cursor = Cursors.Default;

                            FrmPopupGrid.Width = 1000;
                            FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                            //FrmPopupGrid.Size = this.Size;

                            FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                            FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                            FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "PktNo";
                            FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";
                            FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Caption = "PrdType";
                            FrmPopupGrid.GrdDet.Columns["CARAT"].Caption = "Carat";
                            FrmPopupGrid.GrdDet.Columns["ORDERNO"].Caption = "OrderNo";
                            FrmPopupGrid.GrdDet.Columns["PRDEMPCODE"].Caption = "PrdCode";
                            FrmPopupGrid.GrdDet.Columns["TOEMPLOYEECODE"].Caption = "EmpCode";

                            FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                            FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Width = 200;
                            FrmPopupGrid.GrdDet.Columns["ORDERNO"].Width = 200;
                            FrmPopupGrid.ShowDialog();
                            FrmPopupGrid.Hide();
                            FrmPopupGrid.Dispose();
                            FrmPopupGrid = null;
                        }

                    }
                    else if (e.Column.FieldName == "BYSEND")
                    {
                        DataTable DTabInProcssDeail = Obj.OrderDetail(null, null, null, null, "INBYSEND", Val.ToString(DR["SALEDEMAND_ID"]));
                        if (DTabInProcssDeail.Rows.Count > 0)
                        {
                            FrmPopupGrid FrmPopupGrid = new FrmPopupGrid();
                            // FrmPopupGrid.DTab = DtData;                   
                            FrmPopupGrid.CountedColumn = "PACKETNO";
                            FrmPopupGrid.ColumnsToHide = "SALEDEMAND_ID,LAB_ID,RNO";
                            FrmPopupGrid.MainGrid.DataSource = DTabInProcssDeail;
                            FrmPopupGrid.MainGrid.Dock = DockStyle.Fill;
                            FrmPopupGrid.Text = "List Of Packets Which Is In Order(BY Transfer)";
                            FrmPopupGrid.ISPostBack = true;
                            this.Cursor = Cursors.Default;

                            FrmPopupGrid.Width = 1000;
                            FrmPopupGrid.GrdDet.OptionsBehavior.Editable = false;
                            //FrmPopupGrid.Size = this.Size;

                            FrmPopupGrid.GrdDet.Appearance.Row.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;

                            FrmPopupGrid.GrdDet.Columns["KAPANNAME"].Caption = "Kapan";
                            FrmPopupGrid.GrdDet.Columns["PACKETNO"].Caption = "PktNo";
                            FrmPopupGrid.GrdDet.Columns["TAG"].Caption = "Tag";
                            FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Caption = "PrdType";
                            FrmPopupGrid.GrdDet.Columns["CARAT"].Caption = "Carat";
                            FrmPopupGrid.GrdDet.Columns["ORDERNO"].Caption = "OrderNo";
                            FrmPopupGrid.GrdDet.Columns["PRDEMPCODE"].Caption = "PrdCode";
                            FrmPopupGrid.GrdDet.Columns["TOEMPLOYEECODE"].Caption = "EmpCode";

                            FrmPopupGrid.GrdDet.Columns["TAG"].Width = 50;
                            FrmPopupGrid.GrdDet.Columns["PRDTYPE"].Width = 200;
                            FrmPopupGrid.GrdDet.Columns["ORDERNO"].Width = 200;
                            FrmPopupGrid.ShowDialog();
                            FrmPopupGrid.Hide();
                            FrmPopupGrid.Dispose();
                            FrmPopupGrid = null;
                        }

                    }
                    else
                    {
                        ObjGridSelection.ClearSelection();

                        DtabExcelData.Rows.Clear();
                        DataRow DRO = DtabExcelData.NewRow();

                        DRO["SALEDEMAND_ID"] = Val.ToString(DR["SALEDEMAND_ID"]);
                        DRO["SRNO"] = Val.ToString(DR["SRNO"]);
                        DRO["ORDERNO"] = Val.ToString(DR["ORDERNO"]);
                        DRO["SHAPE"] = Val.ToString(DR["SHAPE"]);
                        DRO["FROMCARAT"] = Val.ToString(DR["FCARAT"]);
                        DRO["TOCARAT"] = Val.ToString(DR["TCARAT"]);
                        DRO["FROMCLARITY"] = Val.ToString(DR["FCLARITY"]);
                        DRO["TOCLARITY"] = Val.ToString(DR["TCLARITY"]);
                        DRO["FROMCOLOR"] = Val.ToString(DR["FCOLOR"]);
                        DRO["TOCOLOR"] = Val.ToString(DR["TCOLOR"]);
                        DRO["FROMFL"] = Val.ToString(DR["FFL"]);
                        DRO["TOFL"] = Val.ToString(DR["TFL"]);
                        DRO["CUT"] = Val.ToString(DR["CUT"]);
                        DRO["QTY"] = Val.ToString(DR["QTY"]);
                        DRO["PERCARAT"] = Val.ToString(DR["PERCARAT"]);
                        DRO["LABTYPE"] = Val.ToString(DR["LABTYPE"]);
                        DRO["ORDERDATE"] = Val.ToString(DR["ORDERDATE"]);
                        DRO["ORDERCOMPDATE"] = Val.ToString(DR["ORDERCOMPDATE"]);
                        DRO["ISACTIVE"] = Val.ToBoolean(Val.ToString(DR["ISACTIVE"]));
                        DRO["REMARK"] = Val.ToString(DR["REMARK"]);

                        TxtFileName.Tag = Val.ToString(DR["ORDERNO"]);
                        LblMode.Text = "EDIT";
                        DtabExcelData.Rows.Add(DRO);

                        /*
                        GrdDetSummry.BeginUpdate();
                        if (MainGridSummry.RepositoryItems.Count == 10)
                        {
                            selection = new DevExpressGrid();
                            selection.View = GrdDetSummry;
                            selection.ClearSelection();
                            selection.CheckMarkColumn.VisibleIndex = 0;

                        }
                        else
                        {
                            selection.ClearSelection();
                        }


                        if (selection != null)
                        {
                            selection.ClearSelection();
                            selection.CheckMarkColumn.VisibleIndex = 0;
                        }
                        GrdDetSummry.EndUpdate();
                        */

                        MainGridSummry.DataSource = DtabExcelData;
                        MainGridSummry.RefreshDataSource();

                        xtraTabControl1.SelectedTabPageIndex = 0;
                    }

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Global.Confirm("ARE YOU WANT TO DELETE THIS ORDER.?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                Mode = "DELETE";

                Guid pGuidSaleDemand_ID = Guid.Empty;
                DataRow Dr = GrdDetSummry.GetFocusedDataRow();

                if (Dr == null || Val.ToString(Dr["SALEDEMAND_ID"]).Trim().Equals(string.Empty))
                    return;

                pGuidSaleDemand_ID = Guid.Parse(Val.ToString(Dr["SALEDEMAND_ID"]));

                string IntRes = Obj.OrderDemandDelete(Mode, pGuidSaleDemand_ID);

                if (IntRes == "SUCCESS")
                {
                    Global.Message("ORDER DATA DELETE SUCCESSFULLY..");

                    BtnClear.PerformClick();
                    TxtFileName.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }
        private void BtnClearSearchP_Click(object sender, EventArgs e)
        {
            OrderCompFromDate.Checked = false;
            OrderCompToDate.Checked = false;
            OrderFromDate.Checked = false;
            OrderToDate.Checked = false;

            DtabExcelData.Clear();
            MainGrdDetail.DataSource = DtabExcelData;
            MainGrdDetail.RefreshDataSource();

            BtnShow.Enabled = true;
        }

        private void BtnExitSearchP_Click(object sender, EventArgs e)
        {
            if (Global.Confirm("ARE YOU WANT TO CLOSE THIS FORM.?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }
            else
            {
                this.Close();
            }
        }

        #region POPUP

        private void RepTxtCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);

                    FrmSearchPopupBox.mColumnsToHide = "CUT_ID";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["CUT"] = Val.ToString(FrmSearchPopupBox.mDRow["CUTCODE"]);
                        FrmSearchPopupBox.mColumnsToHide = "SequenceNo";
                    }
                    else
                    {
                        DR["CUT"] = Val.ToString(DBNull.Value);
                    }
                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepTxtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);

                    FrmSearchPopupBox.mColumnsToHide = "	SHAPE_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["SHAPE"] = Val.ToString(FrmSearchPopupBox.mDRow["SHAPECODE"]);
                    }
                    else
                    {
                        DR["SHAPE"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);
                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepTxtCol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "COLORCODE,COLORNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);

                    FrmSearchPopupBox.mColumnsToHide = "	COLOR_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["FROMCOLOR"] = Val.ToString(FrmSearchPopupBox.mDRow["COLORNAME"]);
                    }
                    else
                    {
                        DR["FROMCOLOR"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);

                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepTxtFl_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "FLCODE,FLNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);

                    FrmSearchPopupBox.mColumnsToHide = "	FL_ID";
                    //	FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["FROMFL"] = Val.ToString(FrmSearchPopupBox.mDRow["FLNAME"]);
                    }
                    else
                    {
                        DR["FROMFL"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);
                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void TxtRepCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "CLARITYCODE,CLARITYNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);

                    FrmSearchPopupBox.mColumnsToHide = "	CLARITY_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["FROMCLARITY"] = Val.ToString(FrmSearchPopupBox.mDRow["CLARITYNAME"]);
                    }
                    else
                    {
                        DR["FROMCLARITY"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);

                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepToCol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "COLORCODE,COLORNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);

                    FrmSearchPopupBox.mColumnsToHide = "	COLOR_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["TOCOLOR"] = Val.ToString(FrmSearchPopupBox.mDRow["COLORNAME"]);
                    }
                    else
                    {
                        DR["TOCOLOR"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);
                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepToCla_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "CLARITYCODE,CLARITYNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);

                    FrmSearchPopupBox.mColumnsToHide = "	CLARITY_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["TOCLARITY"] = Val.ToString(FrmSearchPopupBox.mDRow["CLARITYNAME"]);
                    }
                    else
                    {
                        DR["TOCLARITY"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);

                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void RepToFl_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearchPopupBox = new FrmSearchPopupBox();
                    FrmSearchPopupBox.mSearchField = "FLCODE,FLNAME";
                    FrmSearchPopupBox.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearchPopupBox.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);

                    FrmSearchPopupBox.mColumnsToHide = "	FL_ID";
                    //FrmSearchPopupBox.mColumnsToHide = "SEQUENCENO";

                    this.Cursor = Cursors.Default;
                    FrmSearchPopupBox.ShowDialog();
                    e.Handled = true;
                    if (FrmSearchPopupBox.mDRow != null)
                    {
                        DR["TOFL"] = Val.ToString(FrmSearchPopupBox.mDRow["FLNAME"]);
                    }
                    else
                    {
                        DR["TOFL"] = Val.ToString(DBNull.Value);
                    }
                    GenerateOrderNo(DR);

                    FrmSearchPopupBox.Hide();
                    FrmSearchPopupBox.Dispose();
                    FrmSearchPopupBox = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        #endregion

        private void RepBtnRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                DataRow DR = GrdDetSummry.GetFocusedDataRow();
                string StrOrder = "";



                if (e.KeyCode == Keys.Enter && Val.Val(DR["SRNO"]) != 0 && GrdDetSummry.IsLastRow)
                {
                    int MaxSrNo = 0;
                    MaxSrNo = (int)DtabExcelData.Compute("Max(SRNO)", "");
                    DataRow DRE = DtabExcelData.NewRow();
                    DRE["SRNO"] = MaxSrNo + 1;
                    DtabExcelData.Rows.Add(DRE);

                    if (DR["ORDERNO"] == "")
                    {

                        StrOrder = "" + DR["SHAPE"] + "-" + DR["FROMCARAT"] + "" + DR["TOCARAT"] + "-" + DR["FROMCLARITY"] + "" + DR["TOCLARITY"] + "-"
                                      + DR["FROMCOLOR"] + "" + DR["TOCOLOR"] + "-" + DR["FROMFL"] + "" + DR["TOFL"] + "-" + DR["ORDERDATE"] + "";

                        StrOrder = StrOrder.Remove(StrOrder.Length - 8);

                        DR["ORDERNO"] = StrOrder.ToUpper();
                    }

                    MainGridSummry.DataSource = DtabExcelData;
                    MainGridSummry.RefreshDataSource();
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void GrdDetSummry_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {
            GrdDetSummry.PostEditor();

            DataRow DR = GrdDetSummry.GetFocusedDataRow();

            double FROMCARAT = 0;
            double TOCARAT = 0;

            if (GrdDetSummry.FocusedColumn.FieldName == "TOCARAT")
            {
                FROMCARAT = Val.Val(DR["FROMCARAT"]);
                TOCARAT = Val.Val(e.Value);

                if (Val.Val(DR["FROMCARAT"]) != 0 && Val.Val(e.Value) != 0)
                {
                    if (FROMCARAT > TOCARAT)
                    {
                        e.ErrorText = "TOCARAT MUST BE GREATER THEN FROMCARAT..";
                        e.Valid = false;
                        return;
                    }

                }
            }
        }
        private void GenerateOrderNo(DataRow Dr)
        {
            try
            {
                string StrOrderNo = "";
                //StrOrderNo = Val.ToString(Dr["SHAPE"]) + "-" + Val.ToString(Dr["FROMCARAT"]) + "" + Val.ToString(Dr["TOCARAT"]) + "-" + Val.ToString(Dr["FROMCLARITY"]) + "" + Val.ToString(Dr["TOCLARITY"]) + "-"
                //                      + Val.ToString(Dr["FROMCOLOR"]) + "" + Val.ToString(Dr["TOCOLOR"]) + "-" + Val.ToString(Dr["FROMFL"]) + "" + Val.ToString(Dr["TOFL"]) + "-" + Val.ToString(Dr["ORDERDATE"]) + "";

                if (Val.ToString(Dr["ORDERDATE"]) == "")
                {
                    StrOrderNo = Val.ToString(Dr["SHAPE"]) + "-" + Val.ToString(Dr["FROMCARAT"]) + "" + Val.ToString(Dr["TOCARAT"]) + "-" + Val.ToString(Dr["FROMCLARITY"]) + "" + Val.ToString(Dr["TOCLARITY"]) + "-"
                        + Val.ToString(Dr["FROMCOLOR"]) + "" + Val.ToString(Dr["TOCOLOR"]) + "-" + Val.ToString(Dr["FROMFL"]) + "" + Val.ToString(Dr["TOFL"]) + "";// + DateTime.Parse(Val.ToString(Dr["ORDERDATE"])).ToString("dd/MM/yyyy") + "";

                }
                else
                {
                    StrOrderNo = Val.ToString(Dr["SHAPE"]) + "-" + Val.ToString(Dr["FROMCARAT"]) + "" + Val.ToString(Dr["TOCARAT"]) + "-" + Val.ToString(Dr["FROMCLARITY"]) + "" + Val.ToString(Dr["TOCLARITY"]) + "-"
                                                         + Val.ToString(Dr["FROMCOLOR"]) + "" + Val.ToString(Dr["TOCOLOR"]) + "-" + Val.ToString(Dr["FROMFL"]) + "" + Val.ToString(Dr["TOFL"]) + "-" + DateTime.Parse(Val.ToString(Dr["ORDERDATE"])).ToString("dd/MM/yyyy") + "";

                }

//                StrOrderNo = Val.ToString(Dr["SHAPE"]) + "-" + Val.ToString(Dr["FROMCARAT"]) + "" + Val.ToString(Dr["TOCARAT"]) + "-" + Val.ToString(Dr["FROMCLARITY"]) + "" + Val.ToString(Dr["TOCLARITY"]) + "-"
//                                      + Val.ToString(Dr["FROMCOLOR"]) + "" + Val.ToString(Dr["TOCOLOR"]) + "-" + Val.ToString(Dr["FROMFL"]) + "" + Val.ToString(Dr["TOFL"]) + "-" + DateTime.Parse(Val.ToString(Dr["ORDERDATE"])).ToString("dd/MM/yyyy") + "";
//
                

                Dr["ORDERNO"] = StrOrderNo;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void GrdDetSummry_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                switch (e.Column.FieldName)
                {
                    case "FROMCARAT":
                    case "TOCARAT":
                    case "ORDERDATE":
                        GrdDetSummry.PostEditor();
                        DataRow dr = GrdDetSummry.GetFocusedDataRow();
                        GenerateOrderNo(dr);
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void ChkAdminTick_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                GrdDetDetail.PostEditor();
                DataRow DRow = GrdDetDetail.GetFocusedDataRow();
                //if (Val.ToBoolean(DRow["ISORDERCOMPLETE"]))
                //{
                int IntRes = Obj.OrderDemandUpdateCompDate(Val.ToString(DRow["SALEDEMAND_ID"]), Val.ToBooleanToInt(DRow["ISORDERCOMPLETE"]));
                //}
            }
            catch (Exception EX)
            {
                Global.MessageError(EX.Message.ToString());
            }
        }
    }
}
