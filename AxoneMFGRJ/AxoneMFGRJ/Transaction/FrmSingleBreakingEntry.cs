using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
using BusLib.Transaction;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.BandedGrid.ViewInfo;
using DevExpress.XtraGrid.Views.Grid;
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

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleBreakingEntry : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabBreaking = new DataTable();

        DataTable DTabRapDate = new DataTable();
        DataTable DtabBreakingType = new DataTable();

        BOTRN_SingleBreaking ObjBrk = new BOTRN_SingleBreaking();

        #region Property Settings

        public FrmSingleBreakingEntry()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            txtPassForDisplayBack.Tag = ObjPer.PASSWORD;
            
            txtPassForDisplayBack_TextChanged(null, null);

            deleteSelectedAmountToolStripMenuItem.Enabled = ObjPer.ISDELETE;

            this.Show();

            Clear();

            //DTPFromDate.Focus();
        }

        public void AttachFormDefaultEvent()
        {
           ObjFormEvent.mForm = this;
            //ObjFormEvent.FormKeyDown = true;
            ObjFormEvent.FormKeyPress = true;
            ObjFormEvent.FormResize = true;
            ObjFormEvent.FormClosing = true;
            ObjFormEvent.ObjToDisposeList.Add(ObjFormEvent);
            ObjFormEvent.ObjToDisposeList.Add(ObjBrk);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, string ColName2, string ColValue2, string ColName3, string ColValue3, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabBreaking.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper()
                         && Val.ToString(row[ColName2]).ToUpper() == Val.ToString(ColValue2).ToUpper()
                         && Val.ToString(row[ColName3]).ToUpper() == Val.ToString(ColValue3).ToUpper()
                         && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.MessageError(StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        #region Validation

        private bool ValSave()
        {
            int IntCol = 0, IntRow = -1;
            foreach (DataRow dr in DtabBreaking.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty) && !Val.ToString(dr["BREAKING_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    IntCol = 17;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //else if (Val.ToString(dr["PACKETNO"]).Trim().Equals(string.Empty) && !Val.ToString(dr["REPLABOUR_ID"]).Trim().Equals(string.Empty))
                //{
                //    Global.Message("Please Enter PACKET NO.");
                //    IntCol = 2;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}
                //else if (Val.ToString(dr["TAG"]).Trim().Equals(string.Empty) && !Val.ToString(dr["REPLABOUR_ID"]).Trim().Equals(string.Empty))
                //{
                //    Global.Message("Please Enter TAG.");
                //    IntCol = 3;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}
                //end as


                if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty))
                {
                    if (DtabBreaking.Rows.Count == 1)
                    {
                        Global.Message("Please Select Employee.");
                        IntCol = 17;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.ToString(dr["BREAKINGTYPE_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Breaking Type.");
                    IntCol = 1;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                else if (Val.ToString(dr["KAPANNAME"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Kapan.");
                    IntCol = 2;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                else if (Val.ToInt32(dr["PACKETNO"]) <= 0)
                {
                    Global.Message("Please Select PacketNo.");
                    IntCol = 3;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }

                else if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    IntCol = 1;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                else if (Val.Val(dr["BFAMOUNT"]) <= 0)
                {
                    Global.Message("Before Amount Should Be Greater Than 0.");
                    IntCol = 15;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                else if (Val.Val(dr["AFAMOUNT"]) <= 0)
                {
                    Global.Message("After Amount Should Be Greater Than 0.");
                    IntCol = 25;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
            }
            if (IntRow > -1)
            {
                GrdDet.FocusedRowHandle = IntRow;
                GrdDet.FocusedColumn = GrdDet.VisibleColumns[IntCol];
                GrdDet.Focus();
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
            //DateTime firstDay = new DateTime(DateTime.Now.Year, 1, 1);
            //DTPFromDate.Text = Val.ToString(new DateTime(DateTime.Now.Year, 1, 1));

            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            DtabBreakingType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_BREAKINGTYPE);
            repLookUpBrkType.DataSource = DtabBreakingType;
            repLookUpBrkType.DisplayMember = "BREAKINGTYPECODE";
            repLookUpBrkType.ValueMember = "BREAKINGTYPE_ID";
            repLookUpBrkType.PopulateColumns();
            repLookUpBrkType.Columns["BREAKINGTYPE_ID"].Visible = false;


            repCmbBFRapDate.Items.Clear();
            repCmbAFRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                repCmbBFRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
                repCmbAFRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }

            DTPFromDate.Text = Val.ToString(DateTime.Now.AddMonths(-1));
            DTPToDate.Text = Val.ToString(DateTime.Now);
            DTPFromDate.Focus();
            BtnShow_Click(null, null);


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

                //if (Global.Confirm("Are Your Sure To Save All Records ?") == System.Windows.Forms.DialogResult.No)
                //    return;

                string ReturnMessageDesc = "";
                string ReturnMessageType = "";

                string AlreadyExistsPacketNo = "";


                foreach (DataRow Dr in DtabBreaking.Rows)
                {
                    SingleBreakingProperty Property = new SingleBreakingProperty();

                    //Property.ITEMGROUP_ID = Val.ToInt64(txtParaType.Text);

                    if (Val.ToString(Dr["EMPLOYEECODE"]).Trim().Equals(string.Empty) || Val.Val(Dr["BFAMOUNT"]) <= 0 || Val.Val(Dr["AFAMOUNT"]) <= 0)
                        continue;

                    Property.BREAKING_ID = Val.ToString(Dr["BREAKING_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["BREAKING_ID"]));

                    Property.BREAKINGTYPE_ID = Val.ToInt(Dr["BREAKINGTYPE_ID"]); 
                    Property.BREAKINGDATE = Val.SqlDate(Dr["BREAKINGDATE"].ToString());

                    Property.KAPAN_ID = Val.ToString(Dr["KAPAN_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Dr["KAPAN_ID"]));
                    Property.KAPANNAME = Val.ToString(Dr["KAPANNAME"]);

                    Property.PACKET_ID = Val.ToString(Dr["PACKET_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Dr["PACKET_ID"]));
                    Property.PACKETNO = Val.ToInt32(Dr["PACKETNO"]);
                    Property.TAG = Val.ToString(Dr["TAG"]);

                    Property.BFCARAT = Val.Val(Dr["BFCARAT"]);
                    Property.BFSHAPE_ID = Val.ToString(Dr["BFSHAPENAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFSHAPE_ID"]);
                    Property.BFCOLOR_ID = Val.ToString(Dr["BFCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFCOLOR_ID"]);
                    Property.BFCLARITY_ID = Val.ToString(Dr["BFCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFCLARITY_ID"]);
                    Property.BFCUT_ID = Val.ToString(Dr["BFCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFCUT_ID"]);
                    Property.BFPOL_ID = Val.ToString(Dr["BFPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFPOL_ID"]);
                    Property.BFSYM_ID = Val.ToString(Dr["BFSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFSYM_ID"]);
                    Property.BFFL_ID = Val.ToString(Dr["BFFLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["BFFL_ID"]);
                    Property.BFRAPDATE = Val.SqlDate(Val.ToString(Dr["BFRAPDATE"]));
                    Property.BFRAPAPORT = Val.Val(Dr["BFRAPAPORT"]);
                    Property.BFDISCOUNT = Val.Val(Dr["BFDISCOUNT"]);
                    Property.BFPRICEPERCARAT = Val.Val(Dr["BFPRICEPERCARAT"]);
                    Property.BFAMOUNT = Val.Val(Dr["BFAMOUNT"]);


                    Property.AFCARAT = Val.Val(Dr["AFCARAT"]);
                    Property.AFSHAPE_ID = Val.ToString(Dr["AFSHAPENAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFSHAPE_ID"]);
                    Property.AFCOLOR_ID = Val.ToString(Dr["AFCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFCOLOR_ID"]);
                    Property.AFCLARITY_ID = Val.ToString(Dr["AFCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFCLARITY_ID"]);
                    Property.AFCUT_ID = Val.ToString(Dr["AFCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFCUT_ID"]);
                    Property.AFPOL_ID = Val.ToString(Dr["AFPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFPOL_ID"]);
                    Property.AFSYM_ID = Val.ToString(Dr["AFSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFSYM_ID"]);
                    Property.AFFL_ID = Val.ToString(Dr["AFFLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["AFFL_ID"]);
                    Property.AFRAPDATE = Val.SqlDate(Val.ToString(Dr["AFRAPDATE"]));
                    Property.AFRAPAPORT = Val.Val(Dr["AFRAPAPORT"]);
                    Property.AFDISCOUNT = Val.Val(Dr["AFDISCOUNT"]);
                    Property.AFPRICEPERCARAT = Val.Val(Dr["AFPRICEPERCARAT"]);
                    Property.AFAMOUNT = Val.Val(Dr["AFAMOUNT"]);

                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);
                    Property.PENALTYAMOUNT = Val.Val(Dr["PENALTYAMOUNT"]);

                    Property.REMARK = Val.ToString(Dr["REMARK"]);

                    Property = ObjBrk.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    if (ReturnMessageType.Contains("ALREADY EXISTS"))
                    {
                        AlreadyExistsPacketNo = AlreadyExistsPacketNo.Trim().Equals(string.Empty) ? "'" + ReturnMessageDesc + "'" : AlreadyExistsPacketNo + "\n" + "'" + ReturnMessageDesc + "'";
                    }

                    Property = null;
                }

                DtabBreaking.AcceptChanges();

                if (!AlreadyExistsPacketNo.Trim().Equals(string.Empty))
                    Global.Message("This Packets Are Already Exists Please Check. ->\n" + AlreadyExistsPacketNo);
                else
                    Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    if (GrdDet.RowCount >= 1)
                    {
                        BtnShow_Click(null, null);
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
            Global.ExcelExport("Repairing List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    //if (!Val.ToString(dr["KAPANNAME"]).Equals(string.Empty) && Val.ToInt32(dr["PACKETNO"]) > 0 && !Val.ToString(dr["TAG"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    if (!Val.ToString(dr["EMPLOYEECODE"]).Equals(string.Empty) && Val.Val(dr["BFAMOUNT"]) > 0 && Val.Val(dr["AFAMOUNT"]) > 0 && GrdDet.IsLastRow)
                    {
                        DataRow DrNew = DtabBreaking.NewRow();
                        DrNew["BREAKINGDATE"] = Val.ToString(DateTime.Now);
                        DrNew["BFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                        DrNew["AFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                        DrNew["BREAKINGTYPE_ID"] = 0;
                        DtabBreaking.Rows.Add(DrNew);
                    }
                    else if (GrdDet.IsLastRow)
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
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE REPAIRING ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        SingleBreakingProperty Property = new SingleBreakingProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.BREAKING_ID = Val.ToString(Drow["BREAKING_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Drow["BREAKING_ID"]));
                        Property = ObjBrk.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabBreaking.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabBreaking.AcceptChanges();
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

        private void BtnShow_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;

                string StrFromDate = "", StrToDate = "";


                StrFromDate = Val.SqlDate(DTPFromDate.Text);
                StrToDate = Val.SqlDate(DTPToDate.Text);

                DtabBreaking = ObjBrk.Fill(StrFromDate, StrToDate);

                DataRow DrNew = DtabBreaking.NewRow();
                DrNew["BREAKINGDATE"] = Val.ToString(DateTime.Now);
                DrNew["BFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                DrNew["AFRAPDATE"] = DateTime.Parse(Val.ToString(DTabRapDate.Rows[0]["RAPDATE"])).ToString("dd/MM/yyyy");
                DrNew["BREAKINGTYPE_ID"] = 0;
                DtabBreaking.Rows.Add(DrNew);

                MainGrid.DataSource = DtabBreaking;
                MainGrid.Refresh();

                GrdDet.FocusedColumn = GrdDet.VisibleColumns[0];
                GrdDet.FocusedRowHandle = DrNew.Table.Rows.IndexOf(DrNew);
                GrdDet.Focus();
                GrdDet.ShowEditor();

                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
                this.Cursor = Cursors.Default;
            }
        }

        private void repTxtBFShape_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "SHAPE_ID,IMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("BFSHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFSHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                        GrdDet.SetFocusedRowCellValue("BFSHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFSHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFSHAPENAME", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFColor_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("BFCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetFocusedRowCellValue("BFCOLORCODE", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFCOLORNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFCOLORCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFClarity_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("BFCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetFocusedRowCellValue("BFCLARITYCODE", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFCLARITYNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFCLARITYCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFCut_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("BFCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDet.SetFocusedRowCellValue("BFCUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFCUTNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFCUTCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("BFPOL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFPOLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDet.SetFocusedRowCellValue("BFPOLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFPOL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFPOLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFPOLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtBFSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMCODE,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("BFSYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFSYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDet.SetFocusedRowCellValue("BFSYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFSYM_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFSYMNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFSYMCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFColor_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("AFCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetFocusedRowCellValue("AFCOLORCODE", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFCOLORNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFCOLORCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFClarity_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("AFCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetFocusedRowCellValue("AFCLARITYCODE", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFCLARITYNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFCLARITYCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFCut_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("AFCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDet.SetFocusedRowCellValue("AFCUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFCUTNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFCUTCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFPol_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "POLCODE,POLNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_POL);
                    FrmSearch.mColumnsToHide = "POL_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("AFPOL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFPOLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDet.SetFocusedRowCellValue("AFPOLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFPOL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFPOLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFPOLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFSym_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SYMCODE,SYMNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_SYM);
                    FrmSearch.mColumnsToHide = "SYM_ID";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("AFSYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFSYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDet.SetFocusedRowCellValue("AFSYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFSYM_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFSYMNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFSYMCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtEmployee_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("EMPLOYEE_ID", Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]));
                        GrdDet.SetFocusedRowCellValue("EMPLOYEECODE", Val.ToString(FrmSearch.mDRow["EMPLOYEECODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("EMPLOYEE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("EMPLOYEECODE", string.Empty);
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

        private void repTxtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.TRN_KAPANSINGLE);
                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        DataRow dr = GrdDet.GetFocusedDataRow();

                        GrdDet.SetFocusedRowCellValue("KAPANNAME", Val.ToString(FrmSearch.mDRow["KAPANNAME"]));

                        if (CheckDuplicate("KAPANNAME", dr["KAPANNAME"].ToString(), "PACKETNO", dr["PACKETNO"].ToString(), "TAG", dr["TAG"].ToString(), GrdDet.FocusedRowHandle, "This Breaking Packet Is"))
                        {
                            GrdDet.SetFocusedRowCellValue("KAPANNAME", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("PACKETNO", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("TAG", DBNull.Value);
                        }
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

        private void repTxtPacketNo_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "PACKETNO";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    DataRow DrKp = GrdDet.GetFocusedDataRow();

                    FrmSearch.mDTab = new BOFindRap().GetPacketNo(Val.ToString(DrKp["KAPANNAME"]));

                    FrmSearch.mColumnsToHide = "";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        DataRow dr = GrdDet.GetFocusedDataRow();
                        GrdDet.SetFocusedRowCellValue("PACKETNO", Val.ToString(FrmSearch.mDRow["PACKETNO"]));

                        if (CheckDuplicate("KAPANNAME", dr["KAPANNAME"].ToString(), "PACKETNO", dr["PACKETNO"].ToString(), "TAG", dr["TAG"].ToString(), GrdDet.FocusedRowHandle, "This Repairing Packet Is"))
                        {
                            GrdDet.SetFocusedRowCellValue("KAPANNAME", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("PACKETNO", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("TAG", DBNull.Value);
                        }
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

        private void repTxtTag_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SRNO,TAG";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;

                    DataRow DrPkt = GrdDet.GetFocusedDataRow();

                    FrmSearch.mDTab = new BOFindRap().GetTag(Val.ToString(DrPkt["KAPANNAME"]), Val.ToInt(DrPkt["PACKETNO"]));

                    FrmSearch.mColumnsToHide = "KAPAN_ID,PACKET_ID,EMPLOYEE_ID,KAPANNAME,PACKETNO,LOTPCS,BALANCEPCS";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        DataRow dr = GrdDet.GetFocusedDataRow();

                        GrdDet.SetFocusedRowCellValue("TAG", Val.ToString(FrmSearch.mDRow["TAG"]));
                        GrdDet.SetFocusedRowCellValue("PACKET_ID", Val.ToString(FrmSearch.mDRow["PACKET_ID"]));
                        GrdDet.SetFocusedRowCellValue("KAPAN_ID", Val.ToString(FrmSearch.mDRow["KAPAN_ID"]));


                        if (CheckDuplicate("KAPANNAME", dr["KAPANNAME"].ToString(), "PACKETNO", dr["PACKETNO"].ToString(), "TAG", dr["TAG"].ToString(), GrdDet.FocusedRowHandle, "This Repairing Packet Is"))
                        {
                            GrdDet.SetFocusedRowCellValue("KAPANNAME", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("PACKETNO", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("TAG", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("PACKET_ID", DBNull.Value);
                            GrdDet.SetFocusedRowCellValue("KAPAN_ID", DBNull.Value);
                            GrdDet.FocusedColumn = GrdDet.Columns["KAPANNAME"];
                            GrdDet.ShowEditor();
                        }
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

        public void FindBeforeBrkRap()
        {
            try
            {
                GrdDet.PostEditor();
                
                DataRow DRow = GrdDet.GetFocusedDataRow();

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                if (Val.ToString(DRow["BFSHAPECODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFCOLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFCLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFCUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFPOLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["BFSYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["BFFLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;

                clsFindRap.SHAPECODE = Val.ToString(DRow["BFSHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["BFCOLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["BFCOLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["BFCLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["BFCLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["BFCARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["BFCUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["BFPOLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["BFSYMCODE"]);

                //{
                //    clsFindRap.GCARAT = Val.Val(DRow["GCARAT"]);
                //    clsFindRap.GCUTCODE = Val.ToString(DRow["GCUTCODE"]);
                //    clsFindRap.GPOLCODE = Val.ToString(DRow["GPOLCODE"]);
                //    clsFindRap.GSYMCODE = Val.ToString(DRow["GSYMCODE"]);
                //}

                clsFindRap.FLCODE = Val.ToString(DRow["BFFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(DRow["BFRAPDATE"].ToString()); //DateTime.Parse(DRow["BFRAPDATE"].ToString()).ToString("dd-MM-yyyy");
                

                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                //GrdDet.SetFocusedRowCellValue("BFRAPAPORT", clsFindRap.RAPAPORT);
                //GrdDet.SetFocusedRowCellValue("BFPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                //GrdDet.SetFocusedRowCellValue("BFAMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                //GrdDet.SetFocusedRowCellValue("BFDISCOUNT", clsFindRap.DISCOUNT);

                DRow["BFRAPAPORT"] = clsFindRap.RAPAPORT;
                DRow["BFPRICEPERCARAT"] = clsFindRap.PRICEPERCARAT;
                DRow["BFAMOUNT"] =  Math.Round(clsFindRap.AMOUNT, 0);
                DRow["BFDISCOUNT"] = clsFindRap.DISCOUNT;
                GrdDet.RefreshData();
                clsFindRap = null;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }
        public void FindAfterBrkRap()
        {
            try
            {
                GrdDet.PostEditor();
                DataRow DRow = GrdDet.GetFocusedDataRow();

                if (Val.ToString(DRow["AFSHAPECODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["AFCOLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["AFCLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["AFCUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["AFPOLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["AFSYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["AFFLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;
                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                clsFindRap.SHAPECODE = Val.ToString(DRow["AFSHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["AFCOLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["AFCOLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["AFCLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["AFCLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["AFCARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["AFCUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["AFPOLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["AFSYMCODE"]);

                //{
                //    clsFindRap.GCARAT = Val.Val(DRow["GCARAT"]);
                //    clsFindRap.GCUTCODE = Val.ToString(DRow["GCUTCODE"]);
                //    clsFindRap.GPOLCODE = Val.ToString(DRow["GPOLCODE"]);
                //    clsFindRap.GSYMCODE = Val.ToString(DRow["GSYMCODE"]);
                //}

                clsFindRap.FLCODE = Val.ToString(DRow["AFFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(Val.ToString(DRow["AFRAPDATE"]));

                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                GrdDet.SetFocusedRowCellValue("AFRAPAPORT", clsFindRap.RAPAPORT);
                GrdDet.SetFocusedRowCellValue("AFPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                GrdDet.SetFocusedRowCellValue("AFAMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                GrdDet.SetFocusedRowCellValue("AFDISCOUNT", clsFindRap.DISCOUNT);

                clsFindRap = null;
                this.Cursor = Cursors.Default;
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Default;
                Global.Message(ex.Message.ToString());
            }

        }

        private void repTxtLabourRate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                DataRow Drow = GrdDet.GetFocusedDataRow();
                Drow["LABOURAMOUNT"] = Math.Round(Val.Val(GrdDet.EditingValue) * Val.Val(Drow["ISSUECARAT"]), 0);

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void MainGrid_Paint(object sender, PaintEventArgs e)
        {
            GridControl gridC = sender as GridControl;
            GridView gridView = gridC.FocusedView as GridView;
            BandedGridViewInfo info = (BandedGridViewInfo)gridView.GetViewInfo();
            for (int i = 0; i < info.BandsInfo.BandCount; i++)
            {
                e.Graphics.DrawLine(new Pen(Brushes.Black, 1), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
                //if (i == info.BandsInfo.BandCount - 1) e.Graphics.DrawLine(new Pen(Brushes.Black), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.BandsInfo[i].Bounds.Top), new Point(info.BandsInfo[i].Bounds.X + info.BandsInfo[i].Bounds.Width, info.RowsInfo[info.RowsInfo.Count - 1].Bounds.Bottom - 1));
            }
        }

        private void repTxtBFFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("BFFL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
                        GrdDet.SetFocusedRowCellValue("BFFLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetFocusedRowCellValue("BFFLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("BFFL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("BFFLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("BFFLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindBeforeBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFFL_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    DataRow DRow = GrdDet.GetFocusedDataRow();
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
                        GrdDet.SetFocusedRowCellValue("AFFL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFFLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetFocusedRowCellValue("AFFLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFFL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFFLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFFLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtAFShape_KeyPress(object sender, KeyPressEventArgs e)
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
                    FrmSearch.mColumnsToHide = "SHAPE_ID,IMAGE";
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("AFSHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("AFSHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                        GrdDet.SetFocusedRowCellValue("AFSHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("AFSHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("AFSHAPENAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("AFSHAPECODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindAfterBrkRap();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void txtPassForDisplayBack_TextChanged(object sender, EventArgs e)
        {
            if (Val.ToString(txtPassForDisplayBack.Tag) != "" && Val.ToString(txtPassForDisplayBack.Tag).ToUpper() == txtPassForDisplayBack.Text.ToUpper())
            {
                GrdDet.Columns["BFRAPDATE"].Visible = true;
                GrdDet.Columns["BFDISCOUNT"].Visible = true;
                GrdDet.Columns["BFRAPAPORT"].Visible = true;

                GrdDet.Columns["AFRAPDATE"].Visible = true;
                GrdDet.Columns["AFDISCOUNT"].Visible = true;
                GrdDet.Columns["AFRAPAPORT"].Visible = true;
            }
            else
            {
                GrdDet.Columns["BFRAPDATE"].Visible = false;
                GrdDet.Columns["BFDISCOUNT"].Visible = false;
                GrdDet.Columns["BFRAPAPORT"].Visible = false;

                GrdDet.Columns["AFRAPDATE"].Visible = false;
                GrdDet.Columns["AFDISCOUNT"].Visible = false;
                GrdDet.Columns["AFRAPAPORT"].Visible = false;
            }

        }

        private void repCmbAFRapDate_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                repTxtAfterCarat_Validating(null, null);
            }
            catch(Exception ex)
            {
                Global.Message(ex.Message.ToString());   
            }
        }

        private void repTxtBeforeCarat_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                DataRow dr = GrdDet.GetFocusedDataRow();
                dr["BFCARAT"] = Val.Val(GrdDet.EditingValue);

                GrdDet.RefreshData();
                FindBeforeBrkRap();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtAfterCarat_Validating(object sender, CancelEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                DataRow dr = GrdDet.GetFocusedDataRow();
                dr["AFCARAT"] = Val.Val(GrdDet.EditingValue);

                GrdDet.RefreshData();
                FindAfterBrkRap();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }
    }
}
