using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmDiscountDiff : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOPRI_DiscountDiff ObjDiscDiff = new BOPRI_DiscountDiff();
        DataTable DTabDiscDiff = new DataTable();

        bool IsNextImage = true;

        #region Property Settings

        public FrmDiscountDiff()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();
            this.Show();
            BtnAdd_Click(null, null);

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            BtnDelete.Enabled = ObjPer.ISDELETE;

        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjDiscDiff);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        #region Validation

        private bool ValSave()
        {
            if (txtShape.Text.Trim() == string.Empty)
            {
                Global.Message("Shape Is Required");
                txtShape.Focus();
                return true;
            }
            else if (txtColor.Text.Trim() == string.Empty)
            {
                Global.Message("Color Is Required");
                txtColor.Focus();
                return true;
            }
            else if (txtClarity.Text.Trim() == string.Empty)
            {
                Global.Message("Clarity Is Required");
                txtClarity.Focus();
                return true;
            }
            else if (Val.Val(txtFromCarat.Text) <= 0.00)
            {
                Global.Message("FromCarat Is Required");
                txtFromCarat.Focus();
                return true;
            }
            else if (Val.Val(txtToCarat.Text) <= 0.00)
            {
                Global.Message("ToCarat Is Required");
                txtToCarat.Focus();
                return true;
            }
            else if (Val.Val(txtDiscountDiff.Text) == 0.00)
            {
                Global.Message("Discount Is Required");
                txtDiscountDiff.Focus();
                return true;
            }
            else if(Val.Val(txtFromCarat.Text) > Val.Val(txtToCarat.Text))
            {
                Global.Message("FromCarat Must Be Less Than ToCarat..Please Check.");
                txtFromCarat.Focus();
                return true;
            }
            else if (Val.Val(txtToCarat.Text) < Val.Val(txtFromCarat.Text))
            {
                Global.Message("ToCarat Must Be Greater Than FromCarat..Please Check.");
                txtToCarat.Focus();
                return true;
            }

            return false;
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

        public void Clear()
        {
            txtDiscount_ID.Text = string.Empty;
            txtShape.Tag = string.Empty;
            txtShape.Text = string.Empty;
            txtShape.AccessibleDescription = string.Empty;

            txtColor.Tag = string.Empty;
            txtColor.Text = string.Empty;
            txtColor.AccessibleDescription = string.Empty;

            txtClarity.Tag = string.Empty;
            txtClarity.Text = string.Empty;
            txtClarity.AccessibleDescription = string.Empty;

            txtFL.Tag = string.Empty;
            txtFL.Text = string.Empty;
            txtFL.AccessibleDescription = string.Empty;

            txtFromCarat.Text = "0.000";
            txtToCarat.Text = "0.000";
            txtDiscountDiff.Text = "0.000";

            DTPRapDate.Text = Val.ToString(DateTime.Now);

            Fill();

            txtShape.Focus();

        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                if (ValSave())
                {
                    return;
                }

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                PriDiscountDiffProperty Property = new PriDiscountDiffProperty();

                Property.DISCOUNT_ID = Val.ToString(txtDiscount_ID.Text).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(txtDiscount_ID.Text));
                Property.S_CODE = Val.ToString(txtShape.AccessibleDescription);
                Property.C_CODE = Val.ToString(txtColor.AccessibleDescription);
                Property.Q_CODE = Val.ToString(txtClarity.AccessibleDescription);
                Property.FL_CODE = Val.ToString(txtFL.AccessibleDescription);
                Property.CUT_CODE = string.Empty;
                Property.POL_CODE = string.Empty;
                Property.SYM_CODE = string.Empty;
                Property.RAPDATE = Val.SqlDate(DTPRapDate.Text);
                Property.FROMCARAT = Val.Val(txtFromCarat.Text);
                Property.TOCARAT = Val.Val(txtToCarat.Text);
                Property.DISCOUNTDIFF = Val.Val(txtDiscountDiff.Text);
                Property = ObjDiscDiff.Save(Property);

                ReturnMessageDesc = Property.ReturnMessageDesc;
                ReturnMessageType = Property.ReturnMessageType;

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    BtnAdd_Click(null, null);

                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                    txtShape.Focus();
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        public void Fill()
        {
            DTabDiscDiff = ObjDiscDiff.Fill();
            MainGrid.DataSource = DTabDiscDiff;
            MainGrid.Refresh();

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
        }

        private void GrdDet_RowCellClick(object sender, DevExpress.XtraGrid.Views.Grid.RowCellClickEventArgs e)
        {
            if (e.RowHandle < 0)
            {
                return;
            }

            if (e.Clicks == 2)
            {
                DataRow DR = GrdDet.GetDataRow(e.RowHandle);
                FetchValue(DR);
                DR = null;
            }

        }

        public void FetchValue(DataRow DR)
        {
            txtDiscount_ID.Text = Val.ToString(DR["DISCOUNT_ID"]);

            txtShape.Tag = Val.ToString(DR["S_ID"]);
            txtShape.AccessibleDescription = Val.ToString(DR["S_CODE"]);
            txtShape.Text = Val.ToString(DR["S_NAME"]);

            txtColor.Tag = Val.ToString(DR["C_ID"]);
            txtColor.AccessibleDescription = Val.ToString(DR["C_CODE"]);
            txtColor.Text = Val.ToString(DR["C_NAME"]);

            txtClarity.Tag = Val.ToString(DR["Q_ID"]);
            txtClarity.AccessibleDescription = Val.ToString(DR["Q_CODE"]);
            txtClarity.Text = Val.ToString(DR["Q_NAME"]);

            txtFL.Tag = Val.ToString(DR["FL_ID"]);
            txtFL.AccessibleDescription = Val.ToString(DR["FL_CODE"]);
            txtFL.Text = Val.ToString(DR["FL_NAME"]);

            txtFromCarat.Text = Val.ToString(DR["F_CARAT"]);
            txtToCarat.Text = Val.ToString(DR["T_CARAT"]);

            txtDiscountDiff.Text = Val.ToString(DR["DISCOUNTDIFF"]);

            txtShape.Focus();

        }

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("Discount List", GrdDet);
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

                    FrmSearch.mColumnsToHide = "SHAPE_ID,IMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtShape.AccessibleDescription = Val.ToString(FrmSearch.mDRow["SHAPECODE"]);
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

        private void txtColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORCODE,COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);

                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtColor.AccessibleDescription = Val.ToString(FrmSearch.mDRow["COLORCODE"]);
                        txtColor.Text = Val.ToString(FrmSearch.mDRow["COLORNAME"]);
                        txtColor.Tag = Val.ToString(FrmSearch.mDRow["COLOR_ID"]);
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

        private void txtClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYCODE,CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);

                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtClarity.AccessibleDescription = Val.ToString(FrmSearch.mDRow["CLARITYCODE"]);
                        txtClarity.Text = Val.ToString(FrmSearch.mDRow["CLARITYNAME"]);
                        txtClarity.Tag = Val.ToString(FrmSearch.mDRow["CLARITY_ID"]);
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

        private void txtFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "FLCODE,FLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_FL);

                    FrmSearch.mColumnsToHide = "FL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtFL.AccessibleDescription = Val.ToString(FrmSearch.mDRow["FLCODE"]);
                        txtFL.Text = Val.ToString(FrmSearch.mDRow["FLNAME"]);
                        txtFL.Tag = Val.ToString(FrmSearch.mDRow["FL_ID"]);
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

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (Val.ToString(txtDiscount_ID.Text).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Records From The List That You Want To Delete.");
                    return;
                }
                PriDiscountDiffProperty Property = new PriDiscountDiffProperty();
                Property.DISCOUNT_ID =  Guid.Parse(Val.ToString(txtDiscount_ID.Text));

                Property = ObjDiscDiff.Delete(Property);

                Global.Message(Property.ReturnMessageDesc);

                if (Property.ReturnMessageType == "SUCCESS")
                {
                    BtnAdd_Click(null, null);
                    Fill();
                }
                else
                {
                    txtShape.Focus();
                }
            }
            catch(Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

    }
}
