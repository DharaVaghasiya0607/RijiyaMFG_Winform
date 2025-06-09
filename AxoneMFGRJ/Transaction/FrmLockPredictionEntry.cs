using AxoneMFGRJ.Masters;
using AxoneMFGRJ.Utility;
using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.TableName;
using BusLib.Transaction;
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
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmLockPredictionEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_LockPredictionSize ObjRough = new BOTRN_LockPredictionSize();
        BOFormPer ObjPer = new BOFormPer();

        DataTable DTabDetail = new DataTable();
        
        #region Property Settings

        public FrmLockPredictionEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            this.Show();

            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            CmbPrdType.DataSource = DTabPrdType;
            CmbPrdType.DisplayMember = "PRDTYPENAME";
            CmbPrdType.ValueMember = "PRDTYPE_ID";
            FetchData();
        }

        public void AttachFormDefaultEvent()
        {
            ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjRough);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        

       
        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {
                
                
                if (Global.Confirm("Are you Sure Your Want To Save This Entry ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                DataTable DTab = DTabDetail.GetChanges();

                LockPredictionSizeEntryProperty Property = new LockPredictionSizeEntryProperty();
                DTab.TableName = "Table1";
                string RoughDetailXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DTab.WriteXml(sw);
                    RoughDetailXML = sw.ToString();
                }
                Property.XMLDETAIL = RoughDetailXML;
                Property = ObjRough.Save(Property);
                if (Property.ReturnMessageType == "SUCCESS")
                {
                    Global.Message(Property.ReturnMessageDesc);
                    FetchData();
                }
                else
                {
                    Global.MessageError(Property.ReturnMessageDesc);
                    BtnSave.Focus();
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

        private void BtnExport_Click(object sender, EventArgs e)
        {
            Global.ExcelExport("List", GrdDet);
        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        LockPredictionSizeEntryProperty Property = new LockPredictionSizeEntryProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.ID = Val.ToInt64(Drow["ID"]);
                        Property = ObjRough.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message(Property.ReturnMessageDesc);
                            DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDetail.AcceptChanges();
                        }
                        else
                        {
                            Global.MessageError(Property.ReturnMessageDesc);
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void repTxtShape_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
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
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                    }
                    
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtFromColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtToColor_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "COLORNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "COLOR_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_COLOR);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                    }
                   
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtFromClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        public void FetchData()
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                GrdDet.BeginUpdate();
                DTabDetail = ObjRough.GetData();
                MainGrid.DataSource = DTabDetail;
                GrdDet.RefreshData();
                GrdDet.BestFitColumns();
                GrdDet.EndUpdate();
                DataRow DRow = DTabDetail.NewRow();
                DRow["SRNO"] = GrdDet.RowCount + 1;
                DTabDetail.Rows.Add(DRow);
               
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.MessageError(ex.Message);
            }
         
        }

        private void repTxtToClarity_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CLARITYNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mColumnsToHide = "CLARITY_ID";
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CLARITY);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }


        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab)
            {
                if (Val.ToString(GrdDet.GetFocusedRowCellValue("SHAPENAME")) !="" &&
                    GrdDet.IsLastRow == true
                    )
                {
                    DataRow DRowPre = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);

                    DataRow DRow = DTabDetail.NewRow();
                    DRow["SRNO"] = GrdDet.RowCount + 1;
                   
                    if (GrdDet.RowCount > 0)
                    {
                        DRow["ROUGHTYPE"] = DRowPre["ROUGHTYPE"];
                        DRow["SHAPENAME"] = DRowPre["SHAPENAME"];
                        DRow["FROMCOLORNAME"] = DRowPre["FROMCOLORNAME"];
                        DRow["TOCOLORNAME"] = DRowPre["TOCOLORNAME"];
                        DRow["FROMCLARITYNAME"] = DRowPre["FROMCLARITYNAME"];
                        DRow["TOCLARITYNAME"] = DRowPre["TOCLARITYNAME"];
                        DRow["PRDTYPE_ID"] = DRowPre["PRDTYPE_ID"];
                    }
                    DTabDetail.Rows.Add(DRow);
                    GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    GrdDet.FocusedColumn = GrdDet.Columns["SHAPENAME"];
                }
                else
                {
                    BtnSave.Focus();
                }
            }
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FetchData();
        }


    }
}
