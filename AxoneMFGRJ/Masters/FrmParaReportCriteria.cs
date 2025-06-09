using BusLib;
using BusLib.Configuration;
using BusLib.Master;
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
    public partial class FrmParaReportCriteria : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        BOFormPer ObjPer = new BOFormPer();

        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_ParaReportCriteria ObjMast = new BOMST_ParaReportCriteria();
        DataTable DTabDetail = new DataTable();

        #region Property Settings

        public FrmParaReportCriteria()
        {
            InitializeComponent();
        }

        /*public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DTabDetail.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }
        */
      
        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            //DTabDetail.Columns.Add("SIZE-ID", typeof(System.Int32));
            //DTabDetail.Columns.Add("SIZENAME", typeof(System.String));
            //DTabDetail.Columns.Add("FROMCARAT", typeof(System.Double));
            //DTabDetail.Columns.Add("TOCARAT", typeof(System.Double));
            //DTabDetail.Columns.Add("ISACTIVE", typeof(System.Boolean));
            //DTabDetail.Columns.Add("REMARK", typeof(System.String));
           
            BtnAdd_Click(null, null);
            Fill();
            this.Show();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjMast);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        public void Clear()
        {
            DTabDetail.Rows.Clear();
            DTabDetail.Rows.Add(DTabDetail.NewRow());
            CmbDiamondType.SelectedIndex = 0;
            
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            Clear();

        }

       /* private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {

                
                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DTabDetail.GetChanges().Rows)
                {
                    PolishTransCrieteriaMasterProperty Property = new PolishTransCrieteriaMasterProperty();

                    if (Val.ToInt64(Dr["EMPLOYEE_ID"]) == 0 || Val.ToInt32(Dr["FROMSHAPE_ID"]) == 0 || Val.ToInt32(Dr["TOSHAPE_ID"]) == 0 || Val.ToInt32(Dr["FROMCOLOR_ID"]) == 0 || Val.ToInt32(Dr["TOCOLOR_ID"]) == 0 || Val.ToInt32(Dr["FROMCLARITY_ID"]) == 0 || Val.ToInt32(Dr["TOCLARITY_ID"]) == 0 || Val.Val(Dr["FROMCARAT"]) == 0 || Val.Val(Dr["TOCARAT"]) == 0 || Val.Val(Dr["FROMDOLLORAMOUNT"]) == 0 || Val.Val(Dr["TODOLLORAMOUNT"]) == 0)
                        continue;

                    Property.POLISHTRANSCRIETEARIA_ID = Val.ToInt32(Dr["POLISHTRANSCRIETEARIA_ID"]);
                    Property.DEPARTMENT_ID = Val.ToInt32(Dr["DEPARTMENT_ID"]);
                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    Property.FROMSHAPE_ID = Val.ToInt32(Dr["FROMSHAPE_ID"]);
                    Property.ToShape_ID = Val.ToInt32(Dr["TOSHAPE_ID"]);
                    Property.FROMCOLOR_ID = Val.ToInt32(Dr["FROMCOLOR_ID"]);
                    Property.FROMCOLOR_ID = Val.ToInt32(Dr["TOCOLOR_ID"]);
                    Property.FROMCLARITY_ID = Val.ToInt32(Dr["FROMCLARITY_ID"]);
                    Property.TOCLARITY_ID = Val.ToInt32(Dr["TOCLARITY_ID"]);
                    Property.FROMCARAT = Val.Val(Dr["FROMCARAT"]);
                    Property.TOCARAT = Val.Val(Dr["TOCARAT"]);
                    Property.FROMDOLLORAMOUNT = Val.Val(Dr["FROMDOLLORAMOUNT"]);
                    Property.TODOLLORAMOUNT = Val.Val(Dr["TODOLLORAMOUNT"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabDetail.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }
        */

        public void Fill()
        {
            DTabDetail = ObjMast.Fill(Val.ToString(CmbDiamondType.SelectedItem));
            DTabDetail.Rows.Add(DTabDetail.NewRow());
            MainGrid.DataSource = DTabDetail;
            MainGrid.Refresh();
        }

        private void BtnBack_Click(object sender, EventArgs e)
        {
            this.Close();
        }
       
        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow Dr = GrdDet.GetFocusedDataRow();
                    if (Val.ToInt32(Dr["FROMCOLOR_ID"]) != 0 && Val.ToInt32(Dr["TOCOLOR_ID"]) != 0 && Val.ToInt32(Dr["FROMCLARITY_ID"]) != 0 && Val.ToInt32(Dr["TOCLARITY_ID"]) != 0 && Val.ToInt32(Dr["FROMCUT_ID"]) != 0 && Val.ToInt32(Dr["TOCUT_ID"]) != 0 && Val.Val(Dr["FROMCARAT"]) != 0 && Val.Val(Dr["TOCARAT"]) != 0 && GrdDet.IsLastRow) //Val.ToInt32(Dr["FROMSHAPE_ID"]) != 0 && Val.ToInt32(Dr["TOSHAPE_ID"]) != 0 && 
                    {
                        DTabDetail.Rows.Add(DTabDetail.NewRow());
                    }
                    else if(GrdDet.IsLastRow)
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

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ParaReportCriteriaMasterProperty Property = new ParaReportCriteriaMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.CRITERIA_ID = Val.ToInt32(Drow["POLISHTRANSCRIETEARIA_ID"]);
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DTabDetail.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DTabDetail.AcceptChanges();
                            Fill();
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

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            Fill();            
        }
        private void repTxtFShapeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("FROMSHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMSHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        /*if (CheckDuplicate("FSHAPENAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "FSHAPENAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("FROMSHAPE_ID", 0);
                            GrdDet.SetFocusedRowCellValue("FSHAPENAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("FROMSHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("FROMSHAPENAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }
        private void repTxtTShapeName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("TOSHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOSHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPENAME"]));
                        /*if (CheckDuplicate("TSHAPENAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TSHAPENAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("TOSHAPE_ID", 0);
                            GrdDet.SetFocusedRowCellValue("TSHAPENAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("TOSHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("TOSHAPENAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtFColname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("FROMCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        /*if (CheckDuplicate("FCOLORNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "FCOLORNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("FROMCOLOR_ID", 0);
                            GrdDet.SetFocusedRowCellValue("FCOLORNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("FROMCOLORNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }
        private void repTxtTColname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("TOCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        /*if (CheckDuplicate("TCOLORNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TCOLORNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("TOCOLOR_ID", 0);
                            GrdDet.SetFocusedRowCellValue("TCOLORNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("TOCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("TOCOLORNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }
        private void repTxtFClaname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("FROMCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        /*if (CheckDuplicate("FCLARITYNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "FCLARITYNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("FROMCLARITY_ID", 0);
                            GrdDet.SetFocusedRowCellValue("FCLARITYNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("FROMCLARITYNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }
        private void repTxtTClaname_KeyDown(object sender, KeyEventArgs e)
        {
            
        }
        private void repTxtTClaname_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("TOCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        /*if (CheckDuplicate("TCLARITYNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TCLARITYNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("TOCLARITY_ID", 0);
                            GrdDet.SetFocusedRowCellValue("TCLARITYNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("TOCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("TOCLARITYNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            try
            {


                string ReturnMessageDesc = "";
                string ReturnMessageType = "";


                foreach (DataRow Dr in DTabDetail.GetChanges().Rows)
                {
                    ParaReportCriteriaMasterProperty Property = new ParaReportCriteriaMasterProperty();

                    Property.DIAMONDTYPE = Val.ToString(CmbDiamondType.SelectedItem);

                    if (//Val.ToInt32(Dr["FROMSHAPE_ID"]) == 0 && Val.ToInt32(Dr["TOSHAPE_ID"]) == 0 &&
                        Val.ToInt32(Dr["FROMCOLOR_ID"]) == 0 && Val.ToInt32(Dr["TOCOLOR_ID"]) == 0 
                        && Val.ToInt32(Dr["FROMCLARITY_ID"]) == 0 && Val.ToInt32(Dr["TOCLARITY_ID"]) == 0
                        && Val.ToInt32(Dr["FROMCUT_ID"]) == 0 && Val.ToInt32(Dr["TOCUT_ID"]) == 0
                        && Val.Val(Dr["FROMCARAT"]) == 0 && Val.Val(Dr["TOCARAT"]) == 0)
                        continue;

                    Property.CRITERIA_ID = Val.ToInt32(Dr["CRITERIA_ID"]);
                    
                    Property.FROMCARAT = Val.Val(Dr["FromCarat"]);
                    Property.TOCARAT = Val.Val(Dr["ToCarat"]);

                    Property.FROMSHAPE_ID = Val.ToInt32(Dr["FROMSHAPE_ID"]);
                    Property.TOSHAPE_ID = Val.ToInt32(Dr["TOSHAPE_ID"]);

                    Property.FROMCOLOR_ID = Val.ToInt32(Dr["FROMCOLOR_ID"]);
                    Property.TOCOLOR_ID = Val.ToInt32(Dr["TOCOLOR_ID"]);

                    Property.FROMCLARITY_ID = Val.ToInt32(Dr["FROMCLARITY_ID"]);
                    Property.TOCLARITY_ID = Val.ToInt32(Dr["TOCLARITY_ID"]);

                    Property.FROMCUT_ID = Val.ToInt32(Dr["FROMCUT_ID"]);
                    Property.TOCUT_ID = Val.ToInt32(Dr["TOCUT_ID"]);

                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DTabDetail.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    if (GrdDet.RowCount > 1)
                    {
                        GrdDet.FocusedRowHandle = GrdDet.RowCount - 1;
                    }
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
        }

        private void repTxtFromCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("FROMCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        /*if (CheckDuplicate("TCOLORNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TCOLORNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("TOCOLOR_ID", 0);
                            GrdDet.SetFocusedRowCellValue("TCOLORNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("FROMCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("FROMCUTNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtToCut_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "CUTCODE,CUTNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_CUT);
                    FrmSearch.mColumnsToHide = "CUT_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("TOCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("TOCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        /*if (CheckDuplicate("TCOLORNAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "TCOLORNAME"))
                        {
                            GrdDet.SetFocusedRowCellValue("TOCOLOR_ID", 0);
                            GrdDet.SetFocusedRowCellValue("TCOLORNAME", string.Empty);
                        }*/
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("TOCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("TOCUTNAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void CmbDiamondType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                Fill();
            }
            catch (Exception Ex)
            {
            }

            
        }
    }
}
