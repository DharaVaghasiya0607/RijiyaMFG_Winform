using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraPrinting;
using Google.API.Translate;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmDollarLabourPriceDetail : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_SingleDollarLabourDetail ObjDollarLabourDet = new BOTRN_SingleDollarLabourDetail();        
        DataTable DtabPrice = new DataTable();
        DataTable DtabDiscount = new DataTable();
        DataTable DtabHead1 = new DataTable();
        DataTable DtParameter = new DataTable();
        
        #region Property Settings

        public FrmDollarLabourPriceDetail()
        {
            InitializeComponent();
        }
        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            txtYear.Text = DateTime.Now.Year.ToString();
            txtMonth.Text = DateTime.Now.Month.ToString();
            txtYear.Focus();
            this.Show();

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = false;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjDollarLabourDet);
            ObjFormEvent.ObjToDisposeList.Add(Val);
        }

        #endregion

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("DollarLabour_" + txtShape.Text + "" + txtYear.Text + "_" + Global.GetMonthName(Val.ToInt(txtMonth.Text)) + "", PivotGrdDet);
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnRefresh_Click(object sender, EventArgs e)
        {
            //if (txtShape.Text.Trim().Length == 0)
            //{
            //    Global.Message("Shape Is Required");
            //    txtShape.Focus();
            //    return;
            //}
            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.Val(txtMonth.Text) == 0)
            {
                Global.Message("Month Is Required");
                txtMonth.Focus();
                return;
            }

            if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
            {
                Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                txtMonth.Focus();
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            DtabPrice = ObjDollarLabourDet.GetData(Val.ToInt(txtShape.Tag), Val.ToInt(txtYear.Text), Val.ToInt(txtMonth.Text));

            PivotGrdDet.DataSource = DtabPrice;
            PivotGrdDet.BestFit();
            this.Cursor = Cursors.Default;

        }

        private void BtnQuickUpload_Click(object sender, EventArgs e)
        {
            FrmDollarLabourQuickUpload FrmDollarLabourQuickUpload = new FrmDollarLabourQuickUpload();
            FrmDollarLabourQuickUpload.ShowForm();
        }

        private void txtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SHAPECODE,SHAPENAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SHAPE);
                    FrmSearch.mColumnsToHide = "SHAPE_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtShape.Text = Val.ToString(FrmSearch.mDRow["SHAPENAME"]);
                        txtShape.Tag = Val.ToString(FrmSearch.mDRow["SHAPE_ID"]);
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

        private void PivotGrdDet_CustomDrawFieldValue(object sender, DevExpress.XtraPivotGrid.PivotCustomDrawFieldValueEventArgs e)
        {
            DevExpress.XtraPivotGrid.PivotGridControl pPivotGrid = (DevExpress.XtraPivotGrid.PivotGridControl)sender;
            PropertyInfo pi = typeof(PivotCustomDrawFieldValueEventArgs).GetProperty("FieldCellViewInfo", (BindingFlags.NonPublic | BindingFlags.Instance));
            DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo viewInfo = ((DevExpress.XtraPivotGrid.ViewInfo.PivotFieldsAreaCellViewInfo)(pi.GetValue(e, null)));
            if (
                (
                    (
                        (viewInfo.Item.Area == PivotArea.RowArea)
                        &&
                            (
                                (viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.Y)
                                &&
                                (viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.Y)
                            )
                    )
                    ||
                    (
                        (viewInfo.Item.Area == PivotArea.ColumnArea)
                        &&
                            (
                                (viewInfo.MinLastLevelIndex <= pPivotGrid.Cells.FocusedCell.X)
                                &&
                                (viewInfo.MaxLastLevelIndex >= pPivotGrid.Cells.FocusedCell.X)
                            )
                    )
                  )
                )
            {
                e.Appearance.ForeColor = System.Drawing.Color.FromArgb(192, 0, 0);
            }
            else
            {
                e.Appearance.ForeColor = Color.Black;
            }
        }

        private void BtnCopy_Click(object sender, EventArgs e)
        {
          
            if (Val.Val(txtYear.Text) == 0)
            {
                Global.Message("Year Is Required");
                txtYear.Focus();
                return;
            }
            if (Val.Val(txtMonth.Text) == 0)
            {
                Global.Message("Month Is Required");
                txtMonth.Focus();
                return;
            }

            if (Val.Val(txtMonth.Text) > 12 || Val.Val(txtMonth.Text) <= 0)
            {
                Global.Message("Your Month IS Invalid, Must Be Between 0 To 12");
                txtMonth.Focus();
                return;
            }


            if (Val.Val(txtCopyToYear.Text) == 0)
            {
                Global.Message("Copy To Year Is Required");
                txtCopyToYear.Focus();
                return;
            }
            if (Val.Val(txtCopyToMonth.Text) == 0)
            {
                Global.Message("Copy To Month Is Required");
                txtCopyToMonth.Focus();
                return;
            }

            if (Val.Val(txtCopyToMonth.Text) > 12 || Val.Val(txtCopyToMonth.Text) <= 0)
            {
                Global.Message("Copy To Month IS Invalid, Must Be Between 0 To 12");
                txtCopyToMonth.Focus();
                return;
            }

            if (Global.Confirm("Are You Sure You Want Transfer Labour Data?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            this.Cursor = Cursors.WaitCursor;

            int IntRes = ObjDollarLabourDet.CopyPasteDollarLabour(Val.ToInt(txtYear.Text), Val.ToInt(txtCopyToYear.Text), Val.ToInt(txtMonth.Text), Val.ToInt(txtCopyToMonth.Text));
            this.Cursor = Cursors.Default; 
            
            if (IntRes != -1)
            {
                Global.Message("Data Copied Successfully");                
                return;
            }

            

        }

    }
}
