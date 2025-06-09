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

namespace AxoneMFGRJ.Masters
{
    public partial class FrmSalaryEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_SalaryEntry ObjMast = new BOMST_SalaryEntry();
        
        DataTable DtabSalary = new DataTable();




        #region Property Settings

        public FrmSalaryEntry()
        {
            InitializeComponent();
        }
        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabSalary.AsEnumerable()
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

            //DtabSalary.Columns.Add("PARA_ID", typeof(System.Int32));
            //DtabSalary.Columns.Add("PARACODE", typeof(System.String));
            //DtabSalary.Columns.Add("PARANAME", typeof(System.String));
            //DtabSalary.Columns.Add("ISACTIVE", typeof(System.Boolean));
            //DtabSalary.Columns.Add("SEQUENCENO", typeof(System.Int32));
            //DtabSalary.Columns.Add("REMARK", typeof(System.String));
            //DataRow Dr = new DataRow();

            //DataRow Dr = DtabSalary.NewRow();
            //DtabSalary.Rows.Add(DtabSalary.NewRow());


            //MainGrid.DataSource = DtabSalary;
            //MainGrid.RefreshDataSource();
            string currentMonth = DateTime.Now.Month.ToString();
            string currentYear = DateTime.Now.Year.ToString();


            txtYear.Text = currentYear;

            CmbMonth.SelectedItem = DateTime.Now.AddMonths(-1).ToString("MMM");
            txtYear.Text = DateTime.Now.Year.ToString();
            BtnAdd_Click(null, null);

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
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (Val.ToInt32(txtYear.Text) <= 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return false;
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
            DtabSalary.Rows.Clear();
            DtabSalary.Rows.Add(DtabSalary.NewRow());

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

                foreach (DataRow Dr in DtabSalary.Rows)
                {
                    SalaryEntryProperty Property = new SalaryEntryProperty();

                    Property.SALARY_ID = Val.ToString(Dr["SALARY_ID"]) == String.Empty ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["SALARY_ID"]));
                    Property.SALARYDATE= Val.ToString(Val.SqlDate(Val.ToString(DTPSalaryDate.Text)));
                    Property.YYYY = Val.ToInt32(txtYear.Text);
                    Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    Property.DEPARTMENT_ID = Val.ToInt32(Dr["DEPARTMENT_ID"]);
                    Property.DESIGNATION_ID = Val.ToInt32(Dr["DESIGNATION_ID"]);
                    Property.MANAGER_ID = Val.ToInt64(Dr["MANAGER_ID"]);

                    Property.SALARYTYPE = Val.ToString(Dr["SALARYTYPE"]);

                    Property.SALARY = Val.Val(Dr["SALARY"]);
                    Property.TOTALPCS = Val.ToInt32(Dr["TOTALPCS"]);
                    Property.TOTALCARAT = Val.Val(Dr["TOTALCARAT"]);
                    Property.TOTALDAYS = Val.Val(Dr["TOTALDAYS"]);
                    Property.WDAYS = Val.Val(Dr["WDAYS"]);

                    Property.TOTALHOURS = Val.Val(Dr["TOTALHOURS"]);
                    Property.WHOURS = Val.Val(Dr["WHOURS"]);
                    //Property.OTHOURS = Val.Val(Dr["OVERTIME"]);
                    Property.OTHOURS = Val.Val(Dr["OTHH"]);

                    Property.WMINTS = Val.ToInt64(Dr["WMINTS"]);
                    Property.OTMINTS = Val.ToInt64(Dr["OTMM"]);
                    Property.TOTALMINTS = Val.ToInt64(Dr["TOTALMINTS"]);

                    Property.AVGSALARY= Val.Val(Dr["AVGSALARY"]);
                    Property.GROSSSALARY = Val.Val(Dr["GROSSSALARY"]);
                    Property.TOTALUPAD = Val.Val(Dr["TOTALUPAD"]);
                    Property.NETSALARY = Val.Val(Dr["NETSALARY"]);
                    Property.OTSALARY = Val.Val(Dr["OTSALARY"]);

                    Property.EXTRAAMOUNT = Val.Val(Dr["EXTRAAMOUNT"]);
                    Property.EXTRAREMARK = Val.ToString(Dr["EXTRAREMARK"]);
                    
                    Property.NETPAYABLE = Val.Val(Dr["NETPAYABLE"]);

                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;
                   
                }

                this.Cursor = Cursors.Default;

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    txtYear.Focus();
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
                Global.Message(ex.Message);
            }
        }


        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValSave() == false)
                {
                    return;
                }

                if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                    txtDepartment.Tag = string.Empty;

                if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                    txtEmployee.Tag = string.Empty;

                if (Val.ToString(txtDesignation.Text).Trim().Equals(string.Empty))
                    txtDesignation.Tag = string.Empty;

                if (Val.ToString(txtManager.Text).Trim().Equals(string.Empty))
                    txtManager.Tag = string.Empty;

                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                            txtDepartment.Tag = string.Empty;

                        SalaryEntryProperty Property = new SalaryEntryProperty();

                        foreach (DataRow Dr in DtabSalary.Rows)
                        {
                            if(Val.ToString(Dr["SALARY_ID"]).Trim().Equals(string.Empty))
                                continue;

                            Property.SALARY_ID = Val.ToString(Dr["SALARY_ID"]) == String.Empty ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["SALARY_ID"]));
                            Property.YYYY = Val.ToInt32(txtYear.Text);
                            Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);

                            Property = ObjMast.Delete(Property);

                            if (Property.ReturnMessageType != "SUCCESS")
                                return;

                        }
                       
                        //Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
                        //Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
                        //Property.DESIGNATION_ID = Val.ToInt32(txtDesignation.Tag);
                        //Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);
                     
                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message(Property.ReturnMessageDesc);
                            DtabSalary.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabSalary.AcceptChanges();
                            DTPSalaryDate.Focus();
                            BtnSearch_Click(null, null);
                        }
                        else
                        {
                            Global.Message("ERROR IN DELETE ENTRY");
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
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

            GrdDet.OptionsPrint.AutoWidth = true;
            GrdDet.OptionsPrint.UsePrintStyles = true;

            link.Component = MainGrid;
            link.Landscape = true;
            link.PaperKind = System.Drawing.Printing.PaperKind.A4;

            link.Margins.Left = 40;
            link.Margins.Right = 40;
            link.Margins.Bottom = 40;
            link.Margins.Top = 100;

            link.CreateMarginalHeaderArea += new CreateAreaEventHandler(Link_CreateMarginalHeaderArea);
            link.CreateMarginalFooterArea += new CreateAreaEventHandler(Link_CreateMarginalFooterArea);

            link.CreateDocument();

            link.ShowPreview();
            link.PrintDlg();
        }

        public void Link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            // ' For Report Title

            TextBrick BrickTitle = e.Graph.DrawString("Salary Register ( "+txtYear.Text+" - "+Val.ToString(CmbMonth.SelectedItem)+" )", System.Drawing.Color.Navy, new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 20), DevExpress.XtraPrinting.BorderSide.None);
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
            BrickPageNo.Font = new Font("Verdana", 8, FontStyle.Bold); ;
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

        private void BtnAddDepartment_Click(object sender, EventArgs e)
        {

        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;
            SalaryEntryProperty Property = new SalaryEntryProperty();

            if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                txtDepartment.Tag = string.Empty;

            if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                txtEmployee.Tag = string.Empty;

            if (Val.ToString(txtDesignation.Text).Trim().Equals(string.Empty))
                txtDesignation.Tag = string.Empty;

            if (Val.ToString(txtManager.Text).Trim().Equals(string.Empty))
                txtManager.Tag = string.Empty;

            Property.YYYY = Val.ToInt32(txtYear.Text);
            Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
            Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
            Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
            Property.DESIGNATION_ID = Val.ToInt32(txtDesignation.Tag);
            Property.SALARYDATE = Val.SqlDate(DTPSalaryDate.Text);
            Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

            DtabSalary = ObjMast.GetDataForSalaryEntry("EXISTING",Property);
           
            MainGrid.DataSource = DtabSalary;
            MainGrid.Refresh();
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.ShowEditor();
            GrdDet.Focus();
            this.Cursor = Cursors.Default;
        }
        public int CountDay(int year, int month)
        {
            int NoOfSunday = 0;
            var firstDay = new DateTime(year, month, 1);

            var day29 = firstDay.AddDays(28);
            var day30 = firstDay.AddDays(29);
            var day31 = firstDay.AddDays(30);

            if ((day29.Month == month && day29.DayOfWeek == DayOfWeek.Sunday)
            || (day30.Month == month && day30.DayOfWeek == DayOfWeek.Sunday)
            || (day31.Month == month && day31.DayOfWeek == DayOfWeek.Sunday))
            {
                NoOfSunday = 5;
            }
            else
            {
                NoOfSunday = 4;
            }

            int NumOfDay = DateTime.DaysInMonth(year, month);

            return NumOfDay - NoOfSunday;
        }

   


        private void repChkIsAp_Validating(object sender, CancelEventArgs e)
        {
            //GrdDet.PostEditor();
            //DataRow dr = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

            //string str = Val.ToString(dr["AP"]);

            //if (Val.ToBoolean(dr["AP"]))
            //{
            //    dr["WDAYS"] = 0;
            //    dr["OTHH"] = 0;
            //    dr["OTMM"] = 0;

            //}

        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    if (GrdDet.IsLastRow)
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

        private void txtManager_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtManager.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtManager.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                    }
                    else
                    {
                        txtManager.Text = Val.ToString(DBNull.Value);
                        txtManager.Tag = Val.ToString(DBNull.Value);
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

        private void txtDesignation_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "DESIGNATIONCODE,DESIGNATIONNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DESIGNATION);

                    FrmSearch.mColumnsToHide = "DESIGNATION_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtDesignation.Text = Val.ToString(FrmSearch.mDRow["DESIGNATIONNAME"]);
                        txtDesignation.Tag = Val.ToString(FrmSearch.mDRow["DESIGNATION_ID"]);
                    }
                    else
                    {
                        txtDesignation.Text = Val.ToString(DBNull.Value);
                        txtDesignation.Tag = Val.ToString(DBNull.Value);
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

        private void txtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "EMPLOYEECODE";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_EMPLOYEE);

                    FrmSearch.mColumnsToHide = "EMPLOYEE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtEmployee.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtEmployee.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
                    }
                    else
                    {
                        txtEmployee.Text = Val.ToString(DBNull.Value);
                        txtEmployee.Tag = Val.ToString(DBNull.Value);
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

  
        private void repTxtSalary_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                //GrdDet.PostEditor();
                ////DataRow dr = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                //DataRow dr = GrdDet.GetFocusedDataRow();
                //if(Val.Val(dr["SALARY"]) !=0)

                //{
                //    double AvgSal =0;
                //    if(Val.Val(dr["TOTALDAYS"]) !=0)
                //        AvgSal = Math.Round(Val.Val(dr["SALARY"]) / Val.Val(dr["TOTALDAYS"]),2);

                //    double GrossSal = Val.Val(dr["WDAYS"]) * AvgSal;
                //    dr["AVGSALARY"] = AvgSal;
                //    dr["GROSSSALARY"] = GrossSal;
                //    DtabSalary.AcceptChanges();

                //}

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void repTxtSalary_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                GrdDet.PostEditor();
                //DataRow dr = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                DataRow dr = GrdDet.GetFocusedDataRow();
                if (Val.Val(dr["SALARY"]) != 0)
                {
                    double AvgSal = 0;
                    double GrossSal = 0;
                    double NetAmount = 0;

                    if (Val.Val(dr["TOTALDAYS"]) != 0)
                        AvgSal = Math.Round(Val.Val(dr["SALARY"]) / Val.Val(dr["TOTALDAYS"]), 4);

                    GrossSal = Val.Val(dr["WDAYS"]) * AvgSal;
                    NetAmount = Math.Round(GrossSal - Val.Val(dr["TOTALUPAD"]),2) ;
                    


                    dr["AVGSALARY"] = AvgSal;
                    dr["GROSSSALARY"] = GrossSal;
                    dr["NETSALARY"] = NetAmount;
                    DtabSalary.AcceptChanges();

                }

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void FrmSalaryEntry_KeyDown(object sender, KeyEventArgs e)
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

        private void lblProcessSalary_Click(object sender, EventArgs e)
        {
            if (ValSave() == false)
            {
                return;
            }

            if (Global.Confirm("Are You Sure You Want To Process the Salary" ) == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            SalaryEntryProperty Property = new SalaryEntryProperty();

            if (Val.ToString(txtDepartment.Text).Trim().Equals(string.Empty))
                txtDepartment.Tag = string.Empty;

            if (Val.ToString(txtEmployee.Text).Trim().Equals(string.Empty))
                txtEmployee.Tag = string.Empty;

            if (Val.ToString(txtDesignation.Text).Trim().Equals(string.Empty))
                txtDesignation.Tag = string.Empty;

            if (Val.ToString(txtManager.Text).Trim().Equals(string.Empty))
                txtManager.Tag = string.Empty;

            Property.YYYY = Val.ToInt32(txtYear.Text);
            Property.MM = Val.ToInt32(CmbMonth.SelectedIndex + 1);
            Property.EMPLOYEE_ID = Val.ToInt64(txtEmployee.Tag);
            Property.DEPARTMENT_ID = Val.ToInt32(txtDepartment.Tag);
            Property.DESIGNATION_ID = Val.ToInt32(txtDesignation.Tag);
            Property.SALARYDATE = Val.SqlDate(DTPSalaryDate.Text);
            Property.MANAGER_ID = Val.ToInt64(txtManager.Tag);

            DtabSalary = ObjMast.GetDataForSalaryEntry("PROCESS",Property);

            if (DtabSalary.Rows.Count > 0)
            {
                if (!Val.ToString(DtabSalary.Columns["SALARYDATE"]).Equals(string.Empty))
                    DTPSalaryDate.Text = Val.ToString(DtabSalary.Rows[0]["SALARYDATE"]);
            }

            DtabSalary.AcceptChanges();

            MainGrid.DataSource = DtabSalary;
            MainGrid.Refresh();
            GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
            GrdDet.FocusedRowHandle = 0;
            GrdDet.ShowEditor();
            GrdDet.Focus();
            this.Cursor = Cursors.Default;
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            if (e.Column.FieldName.ToUpper()== "EXTRAAMOUNT")
            {
                double  NetSalary = Val.Val(GrdDet.GetFocusedRowCellValue("NETSALARY"));
                double ExtraSalary = Val.Val(GrdDet.GetFocusedRowCellValue("EXTRAAMOUNT"));
                double NetPayable = Math.Round(NetSalary + ExtraSalary, 0);
                GrdDet.SetRowCellValue(e.RowHandle, "NETPAYABLE", NetPayable);

            }
        }

        private void BtnAddDepartment_Click_1(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("DEPARTMENT");
        }

        private void BtnAddDesignation_Click(object sender, EventArgs e)
        {
            FrmParameter FrmParameter = new FrmParameter();
            FrmParameter.MdiParent = Global.gMainRef;
            ObjFormEvent.ObjToDisposeList.Add(FrmParameter);
            FrmParameter.ShowForm("DESIGNATION");
        }
      
        //private void txtMonth_Validating(object sender, CancelEventArgs e)
        //{
        //    if (Val.ToInt32(txtMonth.Text) > 12)
        //    {
        //        Global.Message("Please Enter Proper Month");

        //        e.Cancel=true;
        //    }
        //}

        //private void txtYear_Validating(object sender, CancelEventArgs e)
        //{
        //    //if(!Val.ToString(txtYear.Text).Trim().Equals(string.Empty))
        //    //if (Val.ToString(txtMonth.Text).Trim().Length < 4)
        //    //{
        //    //    Global.Message("Please Enter Proper Year");
        //    //    txtYear.Text = "0";
        //    //    txtYear.Focus();

        //    //}
        //}






    }
}
