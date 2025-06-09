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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmAttendanceEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer= new BOFormPer();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Attendance ObjMast = new BOMST_Attendance();
        
        DataTable DtabAtd = new DataTable();

        double DouWHours = 0;



        #region Property Settings

        public FrmAttendanceEntry()
        {
            InitializeComponent();
        }
        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabAtd.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }
        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

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
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);            
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (txtPassForDisplayBack.Text.ToUpper() != txtPassForDisplayBack.Tag.ToString().ToUpper())
            {
                if (DTPAsOnDate.Value.ToString("dd/MM/yyyy") != DateTime.Parse(ObjMast.GetServerDate()).ToString("dd/MM/yyyy"))
                {
                    Global.Message("You Can Not Suppose To Change Data, Your Due Date Is Over");
                    return false;
                }    
            }
            
            foreach (DataRow dr in DtabAtd.Rows)
            {
                if (Val.Val(dr["WHOURS"]) != 10 && Val.ToString(dr["REMARK"]).Trim().Length == 0)
                {
                    Global.Message(Val.ToString(dr["EMPLOYEECODE"]) + "-" + Val.ToString(dr["EMPLOYEENAME"]) + "  : Remark Is Required");
                    return false;
                }
            }

            return true;
        }

        private bool ValDelete()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}

            return true;
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
            DtabAtd.Rows.Clear();

            Calculation();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave() == false)
                {
                    return;
                }

                if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                    return;

                this.Cursor = Cursors.WaitCursor;

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";
                int IntSrNo = 0;
                foreach (DataRow Dr in DtabAtd.Rows)
                {
                    IntSrNo++;
                    AttendanceEntryProperty Property = new AttendanceEntryProperty();
                    Property.SRNO = IntSrNo;
                    Property.ATD_ID = Val.ToString(Dr["ATD_ID"]) == String.Empty ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["ATD_ID"]));
                    Property.ATDDATE =Val.SqlDate(DTPAsOnDate.Text);
                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    Property.DEPARTMENT_ID = Val.ToInt32(Dr["DEPARTMENT_ID"]);
                    Property.DESIGNATION_ID = Val.ToInt32(Dr["DESIGNATION_ID"]);
                    Property.AP =Val.ToString(Dr["AP"]);
                    Property.WDAYS = Val.Val(Dr["WDAYS"]);
                    Property.WHOURS = Val.Val(Dr["WHOURS"]);

                    Property.OTHH = Val.ToInt32(Dr["OTHH"]);
                    Property.OTMM = Val.ToInt32(Dr["OTMM"]);  

                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                }
                this.Cursor = Cursors.Default; 
                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    //Fill();
                    //BtnAdd_Click(null, null);
                    BtnSearch_Click(null, null);
                    DTPAsOnDate.Focus();
                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    //txtItemGroupCode.Focus();
                }
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default; 
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            //DtabAtd = ObjMast.Fill(StrLedgerGroup);
            //DtabAtd.Rows.Add(DtabAtd.NewRow());
            //MainGrid.DataSource = DtabAtd;
            //MainGrid.Refresh();

        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    if (GrdDet.FocusedRowHandle >= 0)
            //    {
            //        if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
            //        {
            //            if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
            //                txtDepartment.Tag = string.Empty;

            //            AttendanceEntryProperty Property = new AttendanceEntryProperty();

            //            foreach (DataRow Dr in DtabAtd.Rows)
            //            {
            //                if (Val.ToString(Dr["ATD_ID"]).Trim().Equals(string.Empty))
            //                    continue;

            //                Property.ATD_ID = Guid.Parse(Val.ToString(Dr["ATD_ID"]));
            //                Property.ATDDATE = Val.ToString(Val.SqlDate(DTPAsOnDate.Text));
            //                Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
            //                Property = ObjMast.Delete(Property);
            //                if (Property.ReturnMessageType != "SUCCESS")
            //                    return;
            //            }

            //            DtabAtd.Rows.Clear();
            //            Global.Message(Property.ReturnMessageDesc);
            //            DtabAtd.AcceptChanges();
            //            DTPAsOnDate.Focus();

            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message);
            //}
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmLedger_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Escape)
            //{
            //    if (Global.Confirm("Do You Want To Close The Form?") == System.Windows.Forms.DialogResult.Yes)
            //        BtnBack_Click(null, null);
            //}
            if (e.KeyCode == Keys.F5)
            {
                BtnSearch_Click(null, null);
            }
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            //if (e.RowHandle < 0)
            //{
            //    return;
            //}

            //if (e.Clicks == 2)
            //{
            //    DataRow DR = GrdDet.GetDataRow(e.RowHandle);
            //    FetchValue(DR);
            //    DR = null;
            //}

        }

        private void GrdDet_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.KeyCode == Keys.Enter)
            //{
            //    DataRow DR = GrdDet.GetFocusedDataRow();
            //    FetchValue(DR);
            //    DR = null;
            //}
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

            GrdDet.BestFitColumns();
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

            //TextBrick BrickTitle = e.Graph.DrawString("Daily Attendance Paper Of ( "+DTPAsOnDate.Text+" )", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitle.Font = new Font("Verdana", 12, FontStyle.Bold);
            //BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            //BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //TextBrick BrickData = e.Graph.DrawString("Total " + lblPresent.Text, System.Drawing.Color.Black, new RectangleF(0, 25, 150, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickData.Font = new Font("Verdana", 8, FontStyle.Bold);
            //BrickData.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickData.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //BrickData = e.Graph.DrawString("Total " + lblAbsent.Text, System.Drawing.Color.Black, new RectangleF(0, 45, 150, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickData.Font = new Font("Verdana", 8, FontStyle.Bold);
            //BrickData.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickData.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //BrickData = e.Graph.DrawString("Total " + lblHalfDay.Text, System.Drawing.Color.Black, new RectangleF(0, 65, 150, 20), DevExpress.XtraPrinting.BorderSide.None);
            //BrickData.Font = new Font("Verdana", 8, FontStyle.Bold);
            //BrickData.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            //BrickData.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            //int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            //TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + System.DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Black, new RectangleF(IntX, 25, 400, 18), DevExpress.XtraPrinting.BorderSide.None);
            //BrickTitledate.Font = new Font("Verdana", 8, FontStyle.Bold);
            //BrickTitledate.HorzAlignment = DevExpress.Utils.HorzAlignment.Far;
            //BrickTitledate.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            //BrickTitledate.ForeColor = Color.Black;


            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString(BusLib.Configuration.BOConfiguration.gEmployeeProperty.COMPANYNAME, System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitle.Font = new Font("verdana", 12, FontStyle.Bold);
            BrickTitle.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitle.VertAlignment = DevExpress.Utils.VertAlignment.Center;

            // ' For Group 
            TextBrick BrickTitleseller = e.Graph.DrawString("Daily Attendance Paper Of ( " + DTPAsOnDate.Text + " )", System.Drawing.Color.Navy, new RectangleF(0, 35, e.Graph.ClientPageSize.Width, 35), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitleseller.Font = new Font("verdana", 10, FontStyle.Bold);
            BrickTitleseller.HorzAlignment = DevExpress.Utils.HorzAlignment.Center;
            BrickTitleseller.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitleseller.ForeColor = Color.Black;

            // ' For Filter 
            string Str = txtDepartment.Text.Trim().Length == 0 ? "All Department" : txtDepartment.Text;
            TextBrick BrickTitlesParam = e.Graph.DrawString("Department  : " +Str, System.Drawing.Color.Navy, new RectangleF(0, 70, e.Graph.ClientPageSize.Width, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitlesParam.Font = new Font("verdana", 8, FontStyle.Bold);
            BrickTitlesParam.HorzAlignment = DevExpress.Utils.HorzAlignment.Near;
            BrickTitlesParam.VertAlignment = DevExpress.Utils.VertAlignment.Center;
            BrickTitlesParam.ForeColor = Color.Black;


            int IntX = Convert.ToInt32(Math.Round(e.Graph.ClientPageSize.Width - 400, 0));
            TextBrick BrickTitledate = e.Graph.DrawString("Print Date :- " + DateTime.Now.ToString("dd/MM/yyyy hh:mm:ss tt"), System.Drawing.Color.Navy, new RectangleF(IntX, 70, 400, 30), DevExpress.XtraPrinting.BorderSide.None);
            BrickTitledate.Font = new Font("verdana", 8, FontStyle.Bold);
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


        private void txtDepartment_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DEPARTMENTCODE,DEPARTMENTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENT);

                    FrmSearch.mColumnsToHide = "DEPARTMENT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDepartment.Text = Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]);
                        txtDepartment.Tag = Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]);
                    }
                    else
                    {
                        txtDepartment.Text = Val.ToString(DBNull.Value);
                        txtDepartment.Tag = Val.ToString(DBNull.Value);
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor; 
            AttendanceEntryProperty Property = new AttendanceEntryProperty();

            if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                txtDepartment.Tag = string.Empty;

            
            Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
            Property.ATDDATE = Val.SqlDate(DTPAsOnDate.Text);

            DataSet DS = ObjMast.GetDataForAttendanceEntry(Property);
            DtabAtd = DS.Tables[0];

           if(DS.Tables.Count > 1 && DS.Tables[1].Rows.Count > 0)
            DouWHours = Val.Val(DS.Tables[1].Rows[0]["SETTINGVALUE"]);

            //
            //DtabAtd = ObjMast.GetDataForAttendanceEntry(Property);            
            MainGrid.DataSource = DtabAtd;
            MainGrid.Refresh();
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.ShowEditor();
            GrdDet.Focus();
            Calculation();
            this.Cursor = Cursors.Default; 
        }
        public void Calculation()
        {

            int IntTotal = 0;
            int IntPresent = 0;
            int IntAbsent = 0;
            int IntHalfDay = 0;
            int IntSunday = 0;

            foreach (DataRow DRow in DtabAtd.Rows)
            {
                IntTotal++;
                if (Val.Val(DRow["WHOURS"]) == 0)
                {
                    IntAbsent++;
                }
                else if (Val.Val(DRow["WHOURS"]) >= 10)
                {
                    IntPresent++;
                }
                else if (Val.Val(DRow["WHOURS"]) <= 2)
                {
                    IntSunday++; 
                    
                }
                else if (Val.Val(DRow["WHOURS"]) > 2 && Val.Val(DRow["WHOURS"]) < 10)
                {
                    IntHalfDay++;
                }
              
            }

            lblTotal.Text = "Total : (" + IntTotal.ToString() + ")";
            lblAbsent.Text = "A : Absent (" + IntAbsent.ToString() + ")";
            lblPresent.Text = "P : Present (" + IntPresent.ToString() + ")";
            lblHalfDay.Text = "H : HalfDay (" + IntHalfDay.ToString() + ")";
            lblSunday.Text = "S : Short Leave (" + IntSunday.ToString() + ")";
            
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if(GrdDet.IsLastRow)
                    {
                        BtnSave.Focus();
                        e.Handled = true;
                    }
                    
                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnAddDepartment_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("DEPARTMENT");
        }

        private void CmbAP_SelectedValueChanged(object sender, EventArgs e)
        {
            GrdDet.PostEditor();
            DataRow dr = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
            string str = Val.ToString(dr["AP"]);
            if (Val.ToString(dr["AP"]) == "A")
            {
                dr["WDAYS"] = "0.0";
                dr["WHOURS"] = "0.0";
                dr["OTHH"] = 0;
                dr["OTMM"] = 0;
            }
            else if (Val.ToString(dr["AP"]) == "P")
            {
                dr["WDAYS"] = "1.0";
                dr["WHOURS"] = DouWHours;
                dr["OTHH"] = 0;
                dr["OTMM"] = 0;
            }
            else if (Val.ToString(dr["AP"]) == "H")
            {
                dr["WDAYS"] = "0.5";
                dr["WHOURS"] = Val.Val(DouWHours/2);
                dr["OTHH"] = 0;
                dr["OTMM"] = 0;
            }
            else
            {
                dr["WDAYS"] = "0.0";
                dr["WHOURS"] = "0.0";
                dr["OTHH"] = 0;
                dr["OTMM"] = 0;
            }
            GrdDet.RefreshData();
            Calculation();
        }

        private void GrdDet_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            DataRow DR = GrdDet.GetDataRow(e.RowHandle);

            if (Val.ToString(DR["AP"]) == "A")
            {
                e.Appearance.BackColor = lblAbsentBackColor.BackColor;
            }
            else if (Val.ToString(DR["AP"]) == "P")
            {
                e.Appearance.BackColor = lblPresentBackColor.BackColor;
            }
            else if (Val.ToString(DR["AP"]) == "H")
            {
                e.Appearance.BackColor = lblHalfDayBackColor.BackColor;
            }
            else if (Val.ToString(DR["AP"]) == "S")
            {
                e.Appearance.BackColor = lblSundayBackColor.BackColor;
            }
        }

        private void lblAbsent_Click(object sender, EventArgs e)
        {
            GrdDet.ClearColumnsFilter();
            GrdDet.ActiveFilter.Add(GrdDet.Columns["AP"], new ColumnFilterInfo("[AP] = 'A'"));
        }

        private void lblPresent_Click(object sender, EventArgs e)
        {
            GrdDet.ClearColumnsFilter();
            GrdDet.ActiveFilter.Add(GrdDet.Columns["AP"], new ColumnFilterInfo("[AP] = 'P'"));
        }

        private void lblHalfDay_Click(object sender, EventArgs e)
        {
            GrdDet.ClearColumnsFilter();
            GrdDet.ActiveFilter.Add(GrdDet.Columns["AP"], new ColumnFilterInfo("[AP] = 'H'"));
        }

        private void lblSunday_Click(object sender, EventArgs e)
        {
            GrdDet.ClearColumnsFilter();
            GrdDet.ActiveFilter.Add(GrdDet.Columns["AP"], new ColumnFilterInfo("[AP] = 'S'"));
        }

        private void lblTotal_Click(object sender, EventArgs e)
        {
            GrdDet.ClearColumnsFilter();
        }


       

    }
}
