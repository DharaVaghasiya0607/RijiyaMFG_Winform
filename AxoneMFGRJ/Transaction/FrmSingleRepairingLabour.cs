using BusLib;
using BusLib.Configuration;
using BusLib.Master;
using BusLib.Rapaport;
using BusLib.TableName;
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
using System.Data.OleDb;
using System.Drawing;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmSingleRepairingLabour : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabRepLabour = new DataTable();

        bool IsNextImage = true;

        BOTRN_SingleRepairingLabour ObjRepLabour = new BOTRN_SingleRepairingLabour();

        DataTable DtabExcelData = new DataTable();
        DataTable DtabPara = new DataTable();
        DataTable DtabLedger = new DataTable();
        DataTable DTabRapDate = new DataTable();

        string extension = "";
        string destinationPath = "";
        string StrSheetName = "";
        string StrValidationMessage = "";

        #region Property Settings

        public FrmSingleRepairingLabour()
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
            DtabPara = new BOMST_Parameter().GetParameterData();

            DtabLedger = new BOMST_Ledger().Fill("EMPLOYEE", "ALL");

            DTabRapDate = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.RAPDATE);
            DTabRapDate.DefaultView.Sort = "RAPDATE DESC";
            DTabRapDate = DTabRapDate.DefaultView.ToTable();

            this.Show();

            CmbRapDate.Items.Clear();
            foreach (DataRow DRow in DTabRapDate.Rows)
            {
                CmbRapDate.Items.Add(DateTime.Parse(Val.ToString(DRow["RAPDATE"])).ToString("dd/MM/yyyy"));
            }
            CmbRapDate.SelectedIndex = 0;

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
            ObjFormEvent.ObjToDisposeList.Add(ObjRepLabour);
            ObjFormEvent.ObjToDisposeList.Add(Val);
            ObjFormEvent.ObjToDisposeList.Add(ObjPer);
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, string ColName2, string ColValue2, string ColName3, string ColValue3, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabRepLabour.AsEnumerable()
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
            foreach (DataRow dr in DtabRepLabour.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty) && !Val.ToString(dr["REPLABOUR_ID"]).Trim().Equals(string.Empty))
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
                    if (DtabRepLabour.Rows.Count == 1)
                    {
                        Global.Message("Please Select Employee.");
                        IntCol = 17;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }

                if (Val.ToString(dr["REPDATE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter Date.");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //else if (Val.ToString(dr["PACKETNO"]).Trim().Equals(string.Empty))
                //{
                //    Global.Message("Please Enter Packet No.");
                //    IntCol = 2;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}
                else if (Val.ToString(dr["EMPLOYEECODE"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Select Employee.");
                    IntCol = 17;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //else if (Val.ToString(dr["TAG"]).Trim().Equals(string.Empty))
                //{
                //    Global.Message("Please Enter Tag.");
                //    IntCol = 3;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}

                //Cmnt: Pinali : 01-10-2019
                //else if (Val.Val(dr["LABOURRATE"]) <= 0)
                //{
                //    Global.Message("Please Enter Labour Rate.");
                //    IntCol = 19;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}
                //else if (Val.Val(dr["LABOURAMOUNT"]) <= 0)
                //{
                //    Global.Message("Labour Amount Is Required.");
                //    IntCol = 20;
                //    IntRow = dr.Table.Rows.IndexOf(dr);
                //    break;
                //}

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


                foreach (DataRow Dr in DtabRepLabour.Rows)
                {
                    SingleRepairingLabourProperty Property = new SingleRepairingLabourProperty();

                    //Property.ITEMGROUP_ID = Val.ToInt64(txtParaType.Text);

                    if (Val.ToString(Dr["EMPLOYEECODE"]).Trim().Equals(string.Empty)) //|| Val.Val(Dr["LABOURAMOUNT"]) <= 0
                        continue;

                    Property.REPLABOUR_ID = Val.ToString(Dr["REPLABOUR_ID"]).Trim().Equals(string.Empty) ? Guid.NewGuid() : Guid.Parse(Val.ToString(Dr["REPLABOUR_ID"]));
                    Property.REPDATE = Val.SqlDate(Dr["REPDATE"].ToString());

                    Property.KAPAN_ID = Val.ToString(Dr["KAPAN_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Dr["KAPAN_ID"]));
                    Property.KAPANNAME = Val.ToString(Dr["KAPANNAME"]);

                    Property.PACKET_ID = Val.ToString(Dr["PACKET_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Dr["PACKET_ID"]));
                    Property.PACKETNO = Val.ToInt32(Dr["PACKETNO"]);
                    Property.TAG = Val.ToString(Dr["TAG"]);

                    Property.ISSUECARAT = Val.Val(Dr["ISSUECARAT"]);

                    Property.SHAPE_ID = Val.ToString(Dr["SHAPENAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["SHAPE_ID"]);
                    Property.RCOLOR_ID = Val.ToString(Dr["RCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RCOLOR_ID"]);
                    Property.RCLARITY_ID = Val.ToString(Dr["RCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RCLARITY_ID"]);
                    Property.RCUT_ID = Val.ToString(Dr["RCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RCUT_ID"]);
                    Property.RPOL_ID = Val.ToString(Dr["RPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RPOL_ID"]);
                    Property.RSYM_ID = Val.ToString(Dr["RSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RSYM_ID"]);
                    Property.RFL_ID = Val.ToString(Dr["RFLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["RFL_ID"]);

                    Property.RPRICEPERCARAT = Val.Val(Dr["RPRICEPERCARAT"]);
                    Property.RAMOUNT = Val.Val(Dr["RAMOUNT"]);

                    Property.POLISHCARAT = Val.Val(Dr["POLISHCARAT"]);

                    Property.PCOLOR_ID = Val.ToString(Dr["PCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCOLOR_ID"]);
                    Property.PCLARITY_ID = Val.ToString(Dr["PCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCLARITY_ID"]);
                    Property.PCUT_ID = Val.ToString(Dr["PCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCUT_ID"]);
                    Property.PPOL_ID = Val.ToString(Dr["PPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PPOL_ID"]);
                    Property.PSYM_ID = Val.ToString(Dr["PSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PSYM_ID"]);
                    Property.PFL_ID = Val.ToString(Dr["PFLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PFL_ID"]);

                    Property.PPRICEPERCARAT = Val.Val(Dr["PPRICEPERCARAT"]);
                    Property.PAMOUNT = Val.Val(Dr["PAMOUNT"]);

                    Property.EMPLOYEE_ID = Val.ToInt64(Dr["EMPLOYEE_ID"]);

                    Property.LABOURRATE = Val.Val(Dr["LABOURRATE"]);
                    Property.LABOURAMOUNT = Val.Val(Dr["LABOURAMOUNT"]);

                    Property.LABOURPER = Val.Val(Dr["LABOURPER"]);

                    Property.REMARK = Val.ToString(Dr["REMARK"]);

                    Property = ObjRepLabour.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    if (ReturnMessageType.Contains("ALREADY EXISTS"))
                    {
                        AlreadyExistsPacketNo = AlreadyExistsPacketNo.Trim().Equals(string.Empty) ? "'" + ReturnMessageDesc + "'" : AlreadyExistsPacketNo + "\n" + "'" + ReturnMessageDesc + "'";
                    }

                    Property = null;
                }

                DtabRepLabour.AcceptChanges();

                if (!AlreadyExistsPacketNo.Trim().Equals(string.Empty))
                    Global.Message("This Packets Are Already Exists Please Check. ->\n" + AlreadyExistsPacketNo);
                else
                    Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    if (GrdDet.RowCount > 1)
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
                    if (!Val.ToString(dr["EMPLOYEECODE"]).Equals(string.Empty) && Val.Val(dr["LABOURAMOUNT"]) > 0 && GrdDet.IsLastRow)
                    {
                        DataRow DrNew = DtabRepLabour.NewRow();
                        DrNew["REPDATE"] = Val.ToString(DateTime.Now);
                        DrNew["ISCONSIDERCALCULATED"] = 1;
                        DtabRepLabour.Rows.Add(DrNew);
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
                        SingleRepairingLabourProperty Property = new SingleRepairingLabourProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.REPLABOUR_ID = Val.ToString(Drow["REPLABOUR_ID"]).Trim().Equals(string.Empty) ? Guid.Empty : Guid.Parse(Val.ToString(Drow["REPLABOUR_ID"]));
                        Property = ObjRepLabour.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabRepLabour.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabRepLabour.AcceptChanges();
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

                DtabRepLabour = ObjRepLabour.Fill(StrFromDate, StrToDate);

                DataRow DrNew = DtabRepLabour.NewRow();
                DrNew["REPDATE"] = Val.ToString(DateTime.Now);
                DrNew["ISCONSIDERCALCULATED"] = 1;
                DtabRepLabour.Rows.Add(DrNew);

                MainGrid.DataSource = DtabRepLabour;
                MainGrid.Refresh();

                GrdDet.FocusedColumn = GrdDet.Columns["REPDATE"];
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

        private void repTxtShape_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", Val.ToString(FrmSearch.mDRow["SHAPE_ID"]));
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                        GrdDet.SetFocusedRowCellValue("SHAPECODE", Val.ToString(FrmSearch.mDRow["SHAPECODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("SHAPE_ID", 0);
                        GrdDet.SetFocusedRowCellValue("SHAPENAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("SHAPECODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    //FindLabourDetails();
                    FindRapForRoughDetail();
                    FindRapForPolishDetail();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtRColor_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("RCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetFocusedRowCellValue("RCOLORCODE", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RCOLORNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RCOLORCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtRClarity_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("RCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetFocusedRowCellValue("RCLARITYCODE", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RCLARITYNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RCLARITYCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtRCut_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("RCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDet.SetFocusedRowCellValue("RCUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RCUTNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RCUTCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtRPol_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RPOL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDet.SetFocusedRowCellValue("RPOLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDet.SetFocusedRowCellValue("RPOLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RPOL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RPOLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RPOLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtRSym_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RSYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDet.SetFocusedRowCellValue("RSYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDet.SetFocusedRowCellValue("RSYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RSYM_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RSYMNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RSYMCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPColor_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PCOLOR_ID", Val.ToString(FrmSearch.mDRow["COLOR_ID"]));
                        GrdDet.SetFocusedRowCellValue("PCOLORNAME", Val.ToString(FrmSearch.mDRow["COLORNAME"]));
                        GrdDet.SetFocusedRowCellValue("PCOLORCODE", Val.ToString(FrmSearch.mDRow["COLORCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PCOLOR_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PCOLORNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PCOLORCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    //FindLabourDetails();
                    FindRapForPolishDetail();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPClarity_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PCLARITY_ID", Val.ToString(FrmSearch.mDRow["CLARITY_ID"]));
                        GrdDet.SetFocusedRowCellValue("PCLARITYNAME", Val.ToString(FrmSearch.mDRow["CLARITYNAME"]));
                        GrdDet.SetFocusedRowCellValue("PCLARITYCODE", Val.ToString(FrmSearch.mDRow["CLARITYCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PCLARITY_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PCLARITYNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PCLARITYCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindRapForPolishDetail();
                    //FindLabourDetails();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPCut_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PCUT_ID", Val.ToString(FrmSearch.mDRow["CUT_ID"]));
                        GrdDet.SetFocusedRowCellValue("PCUTNAME", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                        GrdDet.SetFocusedRowCellValue("PCUTCODE", Val.ToString(FrmSearch.mDRow["CUTCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PCUT_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PCUTNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PCUTCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindRapForPolishDetail();
                    //FindLabourDetails();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPPol_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PPOL_ID", Val.ToString(FrmSearch.mDRow["POL_ID"]));
                        GrdDet.SetFocusedRowCellValue("PPOLNAME", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                        GrdDet.SetFocusedRowCellValue("PPOLCODE", Val.ToString(FrmSearch.mDRow["POLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PPOL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PPOLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PPOLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindRapForPolishDetail();
                    //FindLabourDetails();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPSym_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PSYM_ID", Val.ToString(FrmSearch.mDRow["SYM_ID"]));
                        GrdDet.SetFocusedRowCellValue("PSYMNAME", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                        GrdDet.SetFocusedRowCellValue("PSYMCODE", Val.ToString(FrmSearch.mDRow["SYMCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PSYM_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PSYMNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PSYMCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;

                    FindRapForPolishDetail();
                    //FindLabourDetails();
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

        public void FindLabourDetails()
        {
            try
            {
                GrdDet.PostEditor();
                DataRow Dr = GrdDet.GetFocusedDataRow();
                SingleRepairingLabourProperty Property = new SingleRepairingLabourProperty();
                Property.REPDATE = Val.SqlDate(Val.ToString(Dr["REPDATE"]));
                Property.SHAPE_ID = Val.ToString(Dr["SHAPENAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["SHAPE_ID"]);
                Property.PCOLOR_ID = Val.ToString(Dr["PCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCOLOR_ID"]);
                Property.PCLARITY_ID = Val.ToString(Dr["PCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCLARITY_ID"]);
                Property.PCUT_ID = Val.ToString(Dr["PCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PCUT_ID"]);
                Property.PPOL_ID = Val.ToString(Dr["PPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PPOL_ID"]);
                Property.PSYM_ID = Val.ToString(Dr["PSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["PSYM_ID"]);
                Property.ISSUECARAT = Val.Val(Dr["ISSUECARAT"]);
                Property.POLISHCARAT = Val.Val(Dr["POLISHCARAT"]);

                Property.PPRICEPERCARAT = Val.Val(Dr["PPRICEPERCARAT"]);
                Property.LABOURPER = Val.Val(Dr["LABOURPER"]);

                DataTable DtLabour = ObjRepLabour.FindLabourDetail(Property);

                if (DtLabour.Rows.Count > 0)
                {
                    Dr["CALCLABOURRATE"] = Val.Val(DtLabour.Rows[0]["LABOURRATE"]);
                    Dr["CALCLABOURAMOUNT"] = Val.Val(DtLabour.Rows[0]["LABOURAMOUNT"]);
                }
                else
                {
                    Dr["CALCLABOURRATE"] = 0;
                    Dr["CALCLABOURAMOUNT"] = 0;
                }
                if (Val.ToInt32(Dr["ISCONSIDERCALCULATED"]) != 0)
                {
                    Dr["LABOURRATE"] = Dr["CALCLABOURRATE"];
                    Dr["LABOURAMOUNT"] = Dr["CALCLABOURAMOUNT"];
                }
                else
                {
                    Dr["LABOURAMOUNT"] = Math.Round(Val.Val(Dr["LABOURRATE"]) * Val.Val(Dr["ISSUECARAT"]), 2);
                }
                GrdDet.RefreshData();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void repChkCalculated_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                GrdDet.PostEditor();


                DataRow DR = GrdDet.GetFocusedDataRow();

                if (Val.ToInt(DR["ISCONSIDERCALCULATED"]) == 1)
                {
                    FindLabourDetails();
                }
                else
                {
                    if (Val.Val(DR["LABOURPER"]) > 0)
                    {
                        double DouLabourPer = Val.Val(DR["LABOURPER"]);

                        double DouLabourRate = Math.Round((Val.Val(DR["CALCLABOURRATE"]) * DouLabourPer / 100), 2);
                        double DouLabourAmount = DouLabourRate * Val.Val(DR["ISSUECARAT"]);

                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LABOURRATE", DouLabourRate);
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LABOURAMOUNT", DouLabourAmount);
                    }
                    else
                    {
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LABOURRATE", 0);
                        GrdDet.SetRowCellValue(GrdDet.FocusedRowHandle, "LABOURAMOUNT", 0);
                    }
                }
            }
            catch (Exception ex)
            {
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
        private void repTxtPolishCarat_Leave(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                GrdDet.PostEditor();
                FindLabourDetails();
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

        private void repTxtRFL_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("RFL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
                        GrdDet.SetFocusedRowCellValue("RFLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetFocusedRowCellValue("RFLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("RFL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("RFLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("RFLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForRoughDetail();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repTxtPFL_KeyPress(object sender, KeyPressEventArgs e)
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
                        GrdDet.SetFocusedRowCellValue("PFL_ID", Val.ToString(FrmSearch.mDRow["FL_ID"]));
                        GrdDet.SetFocusedRowCellValue("PFLNAME", Val.ToString(FrmSearch.mDRow["FLNAME"]));
                        GrdDet.SetFocusedRowCellValue("PFLCODE", Val.ToString(FrmSearch.mDRow["FLCODE"]));
                    }
                    else
                    {
                        GrdDet.SetFocusedRowCellValue("PFL_ID", 0);
                        GrdDet.SetFocusedRowCellValue("PFLNAME", string.Empty);
                        GrdDet.SetFocusedRowCellValue("PFLCODE", string.Empty);
                    }
                    FrmSearch.Hide();
                    FrmSearch.Dispose();
                    FrmSearch = null;
                }
                FindRapForPolishDetail();
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
                GrdDet.Bands["BANDCALCULATEDLABOUR"].Visible = true;
                GrdDet.Columns["LABOURRATE"].Visible = true;
                GrdDet.Columns["LABOURAMOUNT"].Visible = true;
            }
            else
            {
                GrdDet.Bands["BANDCALCULATEDLABOUR"].Visible = false;
                GrdDet.Columns["LABOURRATE"].Visible = false;
                GrdDet.Columns["LABOURAMOUNT"].Visible = false;
            }
        }

        private void BtnLeft_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsNextImage)
                {
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A6;
                    PnlCopyPaste.Visible = false;
                    IsNextImage = false;
                }
                else
                {
                    BtnLeft.Image = AxoneMFGRJ.Properties.Resources.A5;
                    PnlCopyPaste.Visible = true;
                    IsNextImage = true;
                }
            }
            catch (Exception EX)
            {
            }
        }

        private void BtnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog OpenFileDialog = new OpenFileDialog();
                OpenFileDialog.Filter = "Excel Files (*.xls,*.xlsx)|*.xls;*.xlsx;";
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
                    CmbSheetname.SelectedIndex = 0;
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
                //else if (Path.GetExtension(filePath).Equals(".xlsx"))  //for 2007 Excel file
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
                CmbSheetname.Items.Clear(); //ADD:KULDEEP[24/05/18]
                foreach (DataRow row in dt.Rows)
                {
                    string sheetName = (string)row["TABLE_NAME"];
                    sheets.Add(sheetName);
                    CmbSheetname.Items.Add(sheetName);
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

        private void BtnUpload_Click(object sender, EventArgs e)
        {
            try
            {

                DtabExcelData.Rows.Clear();
                DtabRepLabour.Rows.Clear();

                extension = Path.GetExtension(txtFileName.Text.ToString());
                destinationPath = Application.StartupPath + @"\StoneFiles\" + Path.GetFileName(txtFileName.Text);
                destinationPath = destinationPath.Replace(extension, ".xlsx");

                StrSheetName = CmbSheetname.SelectedItem.ToString();

                if (File.Exists(destinationPath))
                {
                    File.Delete(destinationPath);
                }
                File.Copy(txtFileName.Text, destinationPath);

                DevExpress.Data.CurrencyDataController.DisableThreadingProblemsDetection = false;
                BtnUpload.Enabled = false;
                PanelProgress.Visible = true;
                if (!backgroundWorker1.IsBusy)
                {
                    backgroundWorker1.RunWorkerAsync();
                }

               /* 
                DtabExcelData = Global.ImportExcelXLSWithSheetName(destinationPath, true, CmbSheetname.SelectedItem.ToString(), 1);

                for (int Intcol = 0; Intcol < DtabExcelData.Columns.Count; Intcol++)
                {
                    if (Val.ToString(DtabExcelData.Rows[0][Intcol]).Trim().Equals(string.Empty))
                    {
                        DtabExcelData.Columns.Remove(DtabExcelData.Columns[Intcol]);
                        continue;
                    }

                    if (Val.ToString("Date").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPDATE");

                    if (Val.ToString("KapanName,Kapan").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("KAPANNAME");

                    if (Val.ToString("PacketNo,PktNo").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PACKETNO");

                    if (Val.ToString("Tag").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("TAG");

                    if (Val.ToString("EmployeeCode,Emp,EmpCode").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("EMPLOYEECODE");

                    if (Val.ToString("Shape,Shp").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SHAPENAME");

                    if (Val.ToString("RCol,RColor,RoughColor,").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCOLORNAME");

                    if (Val.ToString("RCla,RClarity,RoughClarity").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCLARITYNAME");

                    if (Val.ToString("RIssCts,IssCts,IssueCarat,IssCarat,Issue Carat").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("ISSUECARAT");

                    if (Val.ToString("RCut,RoughCut").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCUTNAME");

                    if (Val.ToString("RPol,RPolish,RoughPolish").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RPOLNAME");

                    if (Val.ToString("RSym,RSymmetry,RoughSym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RSYMNAME");

                    if (Val.ToString("RFl,RoughFl").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RFLNAME");

                    if (Val.ToString("RPerCts,RoughPerCts").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RPRICEPERCARAT");

                    if (Val.ToString("RAmt,RoughAmount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RAMOUNT");


                    if (Val.ToString("PCol,PColor,PolishColor").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCOLORNAME");

                    if (Val.ToString("PCla,PClarity,PolishClarity").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCLARITYNAME");

                    if (Val.ToString("PIssCts,Pol Cts,PolCts,PolishCarat").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("POLISHCARAT");

                    if (Val.ToString("PCut,PolishCut").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCUTNAME");

                    if (Val.ToString("PPol,PPolish,PolishPol").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PPOLNAME");

                    if (Val.ToString("PSym,PSymmetry,PolishSym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PSYMNAME");

                    if (Val.ToString("PFl,PolishFl").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PFLNAME");

                    if (Val.ToString("PPerCts,PolishPerCts").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PPRICEPERCARAT");

                    if (Val.ToString("PAmt,PolishAmount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAMOUNT");

                    if (Val.ToString("LabourPer,Labour%").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LABOURPER");

                    if (Val.ToString("Remark").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REMARK");
                }
                //DtabExcelData.Rows.Remove(DtabExcelData.Rows[0]);

                DtabExcelData.Columns.Add("SHAPE_ID", typeof(int));

                DtabExcelData.Columns.Add("RCOLOR_ID", typeof(int));
                DtabExcelData.Columns.Add("RCLARITY_ID", typeof(int));
                DtabExcelData.Columns.Add("RCUT_ID", typeof(int));
                DtabExcelData.Columns.Add("RPOL_ID", typeof(int));
                DtabExcelData.Columns.Add("RSYM_ID", typeof(int));
                DtabExcelData.Columns.Add("RFL_ID", typeof(int));


                DtabExcelData.Columns.Add("PCOLOR_ID", typeof(int));
                DtabExcelData.Columns.Add("PCLARITY_ID", typeof(int));
                DtabExcelData.Columns.Add("PCUT_ID", typeof(int));
                DtabExcelData.Columns.Add("PPOL_ID", typeof(int));
                DtabExcelData.Columns.Add("PSYM_ID", typeof(int));
                DtabExcelData.Columns.Add("PFL_ID", typeof(int));
                DtabExcelData.Columns.Add("EMPLOYEE_ID", typeof(Int64));

                //DtabExcelData.Columns.Add("LABOURRATE", typeof(double));
                //DtabExcelData.Columns.Add("LABOURAMOUNT", typeof(double));
                //DtabExcelData.Columns.Add("CALCLABOURRATE", typeof(double));
                //DtabExcelData.Columns.Add("CALCLABOURAMOUNT", typeof(double));

                //DtabExcelData.Columns.Add("ISCONSIDERCALCULATED", typeof(int));

                //DtabExcelData.Columns.Add("KAPAN_ID", typeof(Guid));
                //DtabExcelData.Columns.Add("PACKET_ID", typeof(Guid));

                int IntCount = 0;

                IEnumerable<DataRow> rows = DtabExcelData.Rows.Cast<DataRow>().Where(r => Val.ToString(r["KAPANNAME"]) == ""
                                            || Val.ToInt(r["PACKETNO"]) == 0 || Val.ToString(r["TAG"]) == "");

                rows.ToList().ForEach(r => r.Delete());

                DtabExcelData.AcceptChanges();

                foreach (DataRow DRow in DtabExcelData.Rows)
                {
                    if (Val.ToString(DRow["KAPANNAME"]).Length == 0)
                    {
                        continue;
                    }

                    //DRow["ISCONSIDERCALCULATED"] = 1;

                    if (Val.ToString(DRow["SHAPENAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SHAPE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["SHAPENAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("Shape [" + Val.ToString(DRow["SHAPENAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["SHAPE_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SHAPE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["SHAPENAME"]).ToUpper(), "PARA_ID", true));
                    }

                    //Rough Detail
                    if (Val.ToString(DRow["RCOLORNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCOLORNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RColor [" + Val.ToString(DRow["RCOLORNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCOLOR_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCOLORNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RCLARITYNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCLARITYNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RClarity [" + Val.ToString(DRow["RCLARITYNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCLARITY_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCLARITYNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RCUTNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCUTNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RCut [" + Val.ToString(DRow["RCUTNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCUT_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCUTNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RPOLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RPOLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RPol [" + Val.ToString(DRow["RPOLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RPOL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RPOLNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RSYMNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RSYMNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RSym [" + Val.ToString(DRow["RSYMNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RSYM_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RSYMNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RFLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RFLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("RFluoresence [" + Val.ToString(DRow["RFLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RFL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RFLNAME"]).ToUpper(), "PARA_ID", true));
                    }



                    //Polish Detail
                    if (Val.ToString(DRow["PCOLORNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCOLORNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PColor [" + Val.ToString(DRow["PCOLORNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCOLOR_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCOLORNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PCLARITYNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCLARITYNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PClarity [" + Val.ToString(DRow["PCLARITYNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCLARITY_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCLARITYNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PCUTNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCUTNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PCut [" + Val.ToString(DRow["PCUTNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCUT_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCUTNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PPOLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PPOLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PPol [" + Val.ToString(DRow["PPOLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PPOL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PPOLNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PSYMNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PSYMNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PSym [" + Val.ToString(DRow["PSYMNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PSYM_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PSYMNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PFLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PFLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("PFluoresence [" + Val.ToString(DRow["PFLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PFL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PFLNAME"]).ToUpper(), "PARA_ID", true));
                    }


                    if (Val.ToString(DRow["EMPLOYEECODE"]).Length != 0)
                    {
                        if (Val.ToInt64(Val.SearchText(DtabLedger, "LEDGERCODE", Val.ToString(DRow["EMPLOYEECODE"]).ToUpper(), "LEDGER_ID", false)) == 0)
                        {
                            this.Cursor = Cursors.Default;
                            Global.Message("EmployeeCode [" + Val.ToString(DRow["EMPLOYEECODE"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["EMPLOYEE_ID"] = Val.ToInt64(Val.SearchText(DtabLedger, "LEDGERCODE", Val.ToString(DRow["EMPLOYEECODE"]).ToUpper(), "LEDGER_ID", true));
                    }

                    if (Val.Val(DRow["ISSUECARAT"]) == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("IssueCarat Not Valid For Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                        return;
                    }
                    if (Val.Val(DRow["POLISHCARAT"]) == 0)
                    {
                        this.Cursor = Cursors.Default;
                        Global.Message("PolishCarat Not Valid For Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                        return;
                    }

                    //SingleRepairingLabourProperty Property = new SingleRepairingLabourProperty();
                    //Property.REPDATE = Val.SqlDate(Val.ToString(DRow["REPDATE"]));
                    //Property.SHAPE_ID = Val.ToString(DRow["SHAPENAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["SHAPE_ID"]);
                    //Property.PCOLOR_ID = Val.ToString(DRow["PCOLORNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["PCOLOR_ID"]);
                    //Property.PCLARITY_ID = Val.ToString(DRow["PCLARITYNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["PCLARITY_ID"]);
                    //Property.PCUT_ID = Val.ToString(DRow["PCUTNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["PCUT_ID"]);
                    //Property.PPOL_ID = Val.ToString(DRow["PPOLNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["PPOL_ID"]);
                    //Property.PSYM_ID = Val.ToString(DRow["PSYMNAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(DRow["PSYM_ID"]);
                    //Property.ISSUECARAT = Val.Val(DRow["ISSUECARAT"]);
                    //Property.POLISHCARAT = Val.Val(DRow["POLISHCARAT"]);

                    //DataTable DtLabour = ObjRepLabour.FindLabourDetail(Property);

                    //if (DtLabour.Rows.Count > 0)
                    //{
                    //    DRow["CALCLABOURRATE"] = Val.Val(DtLabour.Rows[0]["LABOURRATE"]);
                    //    DRow["CALCLABOURAMOUNT"] = Val.Val(DtLabour.Rows[0]["LABOURAMOUNT"]);

                    //    if (Val.Val(DRow["LABOURPER"]) > 0)
                    //    {
                    //        double DouLabourRate = Math.Round((Val.Val(DRow["CALCLABOURRATE"]) * Val.Val(DRow["LABOURPER"]) / 100), 2);
                    //        double DouLabourAmount = DouLabourRate * Val.Val(DRow["ISSUECARAT"]);

                    //        DRow["LABOURRATE"] = Val.Val(DouLabourRate);
                    //        DRow["LABOURAMOUNT"] = Val.Val(DouLabourAmount);
                    //    }
                    //    else
                    //    {
                    //        DRow["LABOURRATE"] = Val.Val(DtLabour.Rows[0]["LABOURRATE"]);
                    //        DRow["LABOURAMOUNT"] = Val.Val(DtLabour.Rows[0]["LABOURAMOUNT"]);
                    //    }
                    //}
                    //DataRow Drfinal = DtabRepLabour.NewRow();
                    //DtabRepLabour.Rows.Add(Drfinal);
                   
                }

                DtabExcelData.TableName = "Table1";

                string RepairingExcelDataForXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtabExcelData.WriteXml(sw);
                    RepairingExcelDataForXML = sw.ToString();
                }

                DtabRepLabour = ObjRepLabour.RepairingExcelExportDetailGetData(RepairingExcelDataForXML);



                MainGrid.DataSource = DtabRepLabour;
                GrdDet.RefreshData();
                */

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void lblSampleExcelFile_Click(object sender, EventArgs e)
        {
            try
            {
                string StrFilePathDestination = "";
                StrFilePathDestination = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\RepairingFileUploadFormat_" + DateTime.Now.Year.ToString() + DateTime.Now.ToString("MM") + DateTime.Now.Day.ToString() + ".xlsx";
                if (File.Exists(StrFilePathDestination))
                {
                    File.Delete(StrFilePathDestination);
                }
                File.Copy(AppDomain.CurrentDomain.BaseDirectory + "\\Format\\RepairingFileUploadFormat.xlsx", StrFilePathDestination);

                System.Diagnostics.Process.Start(StrFilePathDestination, "CMD");
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void repTxtRPricePerCarat_Validating(object sender, CancelEventArgs e)
        {
            //try
            //{
            //    if (GrdDet.FocusedRowHandle < 0)
            //        return;

            //    GrdDet.PostEditor();
            //    DataRow Dr = GrdDet.GetFocusedDataRow();
            //    Dr["RAMOUNT"] = Math.Round(Val.Val(Dr["RPRICEPERCARAT"]) * Val.Val(Dr["ISSUECARAT"]),2);
            //    GrdDet.RefreshData();

            //}
            //catch (Exception ex)
            //{
            //    Global.Message(ex.Message.ToString());
            //}
        }

        private void GrdDet_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0)
                    return;

                DataRow DRow = GrdDet.GetDataRow(e.RowHandle);
                switch (e.Column.FieldName.ToUpper())
                {
                    //case "RPRICEPERCARAT":
                    //case "ISSUECARAT":
                    //    double DouRPricePerCarat = Val.Val(DRow["RPRICEPERCARAT"]);
                    //    double DouRAmount = Val.Val(DRow["RPRICEPERCARAT"]) * Val.Val(DRow["ISSUECARAT"]);
                    //    GrdDet.SetRowCellValue(e.RowHandle, "RAMOUNT", DouRAmount);
                    //    break;

                    //case "PPRICEPERCARAT":
                    //case "POLISHCARAT":
                    //    double DouPPricePerCarat = Val.Val(DRow["PPRICEPERCARAT"]);
                    //    double DouPAmount = Val.Val(DRow["PPRICEPERCARAT"]) * Val.Val(DRow["POLISHCARAT"]);
                    //    GrdDet.SetRowCellValue(e.RowHandle, "PAMOUNT", DouPAmount);
                    //    break;

                    //case "LABOURPER":
                    //    double DouCalcLabourRate = Val.Val(DRow["CALCLABOURRATE"]);
                    //    double DouLabourPer = Val.Val(DRow["LABOURPER"]);

                    //    double DouLabourRate = Math.Round((Val.Val(DRow["CALCLABOURRATE"]) * DouLabourPer / 100), 2);
                    //    double DouLabourAmount = DouLabourRate * Val.Val(DRow["ISSUECARAT"]);

                    //    GrdDet.SetRowCellValue(e.RowHandle, "LABOURRATE", DouLabourRate);
                    //    GrdDet.SetRowCellValue(e.RowHandle, "LABOURAMOUNT", DouLabourAmount);
                    //    break;
                    case "RPRICEPERCARAT":
                    case "ISSUECARAT":
                        FindRapForRoughDetail();
                        break;
                    case "PPRICEPERCARAT":
                    case "POLISHCARAT":
                        FindRapForPolishDetail();
                        break;
                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                DtabExcelData = Global.ImportExcelXLSWithSheetName(destinationPath, true, StrSheetName, 1);


                for (int Intcol = 0; Intcol < DtabExcelData.Columns.Count; Intcol++)
                {
                    if (Val.ToString(DtabExcelData.Rows[0][Intcol]).Trim().Equals(string.Empty))
                    {
                        DtabExcelData.Columns.Remove(DtabExcelData.Columns[Intcol]);
                        continue;
                    }

                    if (Val.ToString("Date").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REPDATE");

                    if (Val.ToString("KapanName,Kapan").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("KAPANNAME");

                    if (Val.ToString("PacketNo,PktNo").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PACKETNO");

                    if (Val.ToString("Tag").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("TAG");

                    if (Val.ToString("EmployeeCode,Emp,EmpCode").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("EMPLOYEECODE");

                    if (Val.ToString("Shape,Shp").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("SHAPENAME");

                    if (Val.ToString("RCol,RColor,RoughColor,").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCOLORNAME");

                    if (Val.ToString("RCla,RClarity,RoughClarity").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCLARITYNAME");

                    if (Val.ToString("RIssCts,IssCts,IssueCarat,IssCarat,Issue Carat").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("ISSUECARAT");

                    if (Val.ToString("RCut,RoughCut").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RCUTNAME");

                    if (Val.ToString("RPol,RPolish,RoughPolish").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RPOLNAME");

                    if (Val.ToString("RSym,RSymmetry,RoughSym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RSYMNAME");

                    if (Val.ToString("RFl,RoughFl").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RFLNAME");

                    if (Val.ToString("RPerCts,RoughPerCts").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RPRICEPERCARAT");

                    if (Val.ToString("RAmt,RoughAmount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("RAMOUNT");


                    if (Val.ToString("PCol,PColor,PolishColor").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCOLORNAME");

                    if (Val.ToString("PCla,PClarity,PolishClarity").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCLARITYNAME");

                    if (Val.ToString("PIssCts,Pol Cts,PolCts,PolishCarat").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("POLISHCARAT");

                    if (Val.ToString("PCut,PolishCut").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PCUTNAME");

                    if (Val.ToString("PPol,PPolish,PolishPol").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PPOLNAME");

                    if (Val.ToString("PSym,PSymmetry,PolishSym").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PSYMNAME");

                    if (Val.ToString("PFl,PolishFl").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PFLNAME");

                    if (Val.ToString("PPerCts,PolishPerCts").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PPRICEPERCARAT");

                    if (Val.ToString("PAmt,PolishAmount").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("PAMOUNT");

                    if (Val.ToString("LabourPer,Labour%").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("LABOURPER");

                    if (Val.ToString("Remark").ToUpper().Split(',').Contains(Val.ToString(DtabExcelData.Columns[Intcol].ColumnName.ToUpper())))
                        DtabExcelData.Columns[Intcol].ColumnName = Val.ToString("REMARK");
                }
                //DtabExcelData.Rows.Remove(DtabExcelData.Rows[0]);

                DtabExcelData.Columns.Add("SHAPE_ID", typeof(int));

                DtabExcelData.Columns.Add("RCOLOR_ID", typeof(int));
                DtabExcelData.Columns.Add("RCLARITY_ID", typeof(int));
                DtabExcelData.Columns.Add("RCUT_ID", typeof(int));
                DtabExcelData.Columns.Add("RPOL_ID", typeof(int));
                DtabExcelData.Columns.Add("RSYM_ID", typeof(int));
                DtabExcelData.Columns.Add("RFL_ID", typeof(int));


                DtabExcelData.Columns.Add("PCOLOR_ID", typeof(int));
                DtabExcelData.Columns.Add("PCLARITY_ID", typeof(int));
                DtabExcelData.Columns.Add("PCUT_ID", typeof(int));
                DtabExcelData.Columns.Add("PPOL_ID", typeof(int));
                DtabExcelData.Columns.Add("PSYM_ID", typeof(int));
                DtabExcelData.Columns.Add("PFL_ID", typeof(int));
                DtabExcelData.Columns.Add("EMPLOYEE_ID", typeof(Int64));

                //DtabExcelData.Columns.Add("LABOURRATE", typeof(double));
                //DtabExcelData.Columns.Add("LABOURAMOUNT", typeof(double));
                //DtabExcelData.Columns.Add("CALCLABOURRATE", typeof(double));
                //DtabExcelData.Columns.Add("CALCLABOURAMOUNT", typeof(double));

                //DtabExcelData.Columns.Add("ISCONSIDERCALCULATED", typeof(int));

                //DtabExcelData.Columns.Add("KAPAN_ID", typeof(Guid));
                //DtabExcelData.Columns.Add("PACKET_ID", typeof(Guid));

                int IntCount = 0;

                IEnumerable<DataRow> rows = DtabExcelData.Rows.Cast<DataRow>().Where(r => Val.ToString(r["KAPANNAME"]) == ""
                                            || Val.ToInt(r["PACKETNO"]) == 0 || Val.ToString(r["TAG"]) == "");

                rows.ToList().ForEach(r => r.Delete());

                DtabExcelData.AcceptChanges();


                


                foreach (DataRow DRow in DtabExcelData.Rows)
                {
                    if (Val.ToString(DRow["KAPANNAME"]).Length == 0)
                    {
                        continue;
                    }

                    //DRow["ISCONSIDERCALCULATED"] = 1;

                    if (Val.ToString(DRow["SHAPENAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SHAPE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["SHAPENAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("Shape [" + Val.ToString(DRow["SHAPENAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["SHAPE_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SHAPE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["SHAPENAME"]).ToUpper(), "PARA_ID", true));
                    }

                    //Rough Detail
                    if (Val.ToString(DRow["RCOLORNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCOLORNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RColor [" + Val.ToString(DRow["RCOLORNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCOLOR_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCOLORNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RCLARITYNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCLARITYNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RClarity [" + Val.ToString(DRow["RCLARITYNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCLARITY_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCLARITYNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RCUTNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCUTNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RCut [" + Val.ToString(DRow["RCUTNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RCUT_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RCUTNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RPOLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RPOLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RPol [" + Val.ToString(DRow["RPOLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RPOL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RPOLNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RSYMNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RSYMNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RSym [" + Val.ToString(DRow["RSYMNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RSYM_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RSYMNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["RFLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RFLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("RFluoresence [" + Val.ToString(DRow["RFLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["RFL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["RFLNAME"]).ToUpper(), "PARA_ID", true));
                    }


                    //Polish Detail
                    if (Val.ToString(DRow["PCOLORNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCOLORNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PColor [" + Val.ToString(DRow["PCOLORNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCOLOR_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'COLOR'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCOLORNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PCLARITYNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCLARITYNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PClarity [" + Val.ToString(DRow["PCLARITYNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCLARITY_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CLARITY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCLARITYNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PCUTNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCUTNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PCut [" + Val.ToString(DRow["PCUTNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PCUT_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'CUT'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PCUTNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PPOLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PPOLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PPol [" + Val.ToString(DRow["PPOLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PPOL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'POLISH'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PPOLNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PSYMNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PSYMNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PSym [" + Val.ToString(DRow["PSYMNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PSYM_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'SYMMETRY'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PSYMNAME"]).ToUpper(), "PARA_ID", true));
                    }
                    if (Val.ToString(DRow["PFLNAME"]).Length != 0)
                    {
                        if (Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PFLNAME"]).ToUpper(), "PARA_ID", true)) == 0)
                        {
                            Global.Message("PFluoresence [" + Val.ToString(DRow["PFLNAME"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["PFL_ID"] = Val.ToInt32(Val.SearchText(DtabPara.Select("PARATYPE = 'FLUORESCENCE'").CopyToDataTable(), "LABCODE", Val.ToString(DRow["PFLNAME"]).ToUpper(), "PARA_ID", true));
                    }


                    if (Val.ToString(DRow["EMPLOYEECODE"]).Length != 0)
                    {
                        if (Val.ToInt64(Val.SearchText(DtabLedger, "LEDGERCODE", Val.ToString(DRow["EMPLOYEECODE"]).ToUpper(), "LEDGER_ID", false)) == 0)
                        {
                            Global.Message("EmployeeCode [" + Val.ToString(DRow["EMPLOYEECODE"]) + "] Is Not Valid In Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                            return;
                        }
                        DRow["EMPLOYEE_ID"] = Val.ToInt64(Val.SearchText(DtabLedger, "LEDGERCODE", Val.ToString(DRow["EMPLOYEECODE"]).ToUpper(), "LEDGER_ID", true));
                    }

                    if (Val.Val(DRow["ISSUECARAT"]) == 0)
                    {
                        Global.Message("IssueCarat Not Valid For Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                        return;
                    }
                    if (Val.Val(DRow["POLISHCARAT"]) == 0)
                    {
                        Global.Message("PolishCarat Not Valid For Stone No : '" + Val.ToString(DRow["KAPANNAME"]) + "-" + Val.ToString(DRow["PACKETNO"]) + Val.ToString(DRow["TAG"]) + "'");
                        return;
                    }
                }

                //var duplicates = DtabExcelData.AsEnumerable().GroupBy(i => new
                //{
                //    Name = i.Field<string>("KAPANNAME"),
                //    Subect = i.Field<string>("PACKETNO")
                //    }).Where(g => g.Count() > 1).Select(g => new{ g.Key.Name, g.Key.Subect}).ToList();
                //}

                //var duplicates = DtabExcelData.AsEnumerable().GroupBy(r => r["KAPANNAME"], r => r["KAPANNAME"],).Where(gr => gr.Count() > 1).ToList();

                var duplicates = DtabExcelData.AsEnumerable().GroupBy(i => new { KAPANNAME = i.Field<object>("KAPANNAME"), PACKETNO = i.Field<object>("PACKETNO"), TAG = i.Field<object>("TAG") }).Where(g => g.Count() > 1).Select(g => new { g.Key.KAPANNAME, g.Key.PACKETNO, g.Key.TAG }).ToList();

                if (duplicates.Any())
                {
                    Global.Message("Duplicates Record Found.....List :" + String.Join(", ", duplicates.Select(dupl => "'" + Val.ToString(dupl.KAPANNAME) + "-" + Val.ToString(dupl.PACKETNO) + Val.ToString(dupl.TAG) + "'")) + "");
                    return;
                }

                //var result = from c in DtabExcelData.AsEnumerable()
                //             group c by new
                //             {
                //                 KAPANNAME = c.Field<string>("KAPANNAME"),   //column names for checking duplicate values.
                //                 PACKETNO = c.Field<int>("PACKETNO"),
                //                 TAG = c.Field<string>("TAG")
                //             } into g
                //             where g.Count() > 1
                //             select new
                //             {
                //                 g.Key.KAPANNAME,
                //                 g.Key.PACKETNO,
                //                 g.Key.TAG
                //             };

                //if (result.Any())
                //{
                //    Global.Message(String.Join(", ", result.Select(dupl => dupl.KAPANNAME + dupl.PACKETNO + dupl.TAG)));
                //}

                DtabExcelData.TableName = "Table1";

                string RepairingExcelDataForXML = string.Empty;
                using (StringWriter sw = new StringWriter())
                {
                    DtabExcelData.WriteXml(sw);
                    RepairingExcelDataForXML = sw.ToString();
                }

                DtabRepLabour = ObjRepLabour.RepairingExcelExportDetailGetData(RepairingExcelDataForXML);

            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnUpload.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            try
            {
                MainGrid.DataSource = DtabRepLabour;
                GrdDet.RefreshData();
                PanelProgress.Visible = false;
                BtnUpload.Enabled = true;
            }
            catch (Exception ex)
            {
                PanelProgress.Visible = false;
                BtnUpload.Enabled = true;
                Global.Message(ex.Message.ToString());
            }
        }

        public void FindRapForRoughDetail()
        {
            try
            {
                GrdDet.PostEditor();

                DataRow DRow = GrdDet.GetFocusedDataRow();

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                if (Val.ToString(DRow["SHAPENAME"]).Trim().Equals(string.Empty) || Val.ToString(DRow["RCOLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["RCLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["RCUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["RPOLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["RSYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["RFLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;

                clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["RCOLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["RCOLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["RCLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["RCLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["ISSUECARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["RCUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["RPOLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["RSYMCODE"]);

                clsFindRap.FLCODE = Val.ToString(DRow["RFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());


                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                //GrdDet.SetFocusedRowCellValue("BFRAPAPORT", clsFindRap.RAPAPORT);
                //GrdDet.SetFocusedRowCellValue("BFPRICEPERCARAT", clsFindRap.PRICEPERCARAT);
                //GrdDet.SetFocusedRowCellValue("BFAMOUNT", Math.Round(clsFindRap.AMOUNT, 0));
                //GrdDet.SetFocusedRowCellValue("BFDISCOUNT", clsFindRap.DISCOUNT);

                DRow["RPRICEPERCARAT"] = clsFindRap.PRICEPERCARAT;
                DRow["RAMOUNT"] = Math.Round(clsFindRap.AMOUNT, 0);
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
        public void FindRapForPolishDetail()
        {
            try
            {
                GrdDet.PostEditor();

                DataRow DRow = GrdDet.GetFocusedDataRow();

                Trn_RapSaveProperty clsFindRap = new Trn_RapSaveProperty();

                if (Val.ToString(DRow["SHAPENAME"]).Trim().Equals(string.Empty) || Val.ToString(DRow["PCOLORCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["PCLARITYCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["PCUTCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["PPOLCODE"]).Trim().Equals(string.Empty) || Val.ToString(DRow["PSYMCODE"]).Trim().Equals(string.Empty) ||
                    Val.ToString(DRow["PFLCODE"]).Trim().Equals(string.Empty)
                  )
                    return;

                this.Cursor = Cursors.WaitCursor;

                clsFindRap.SHAPECODE = Val.ToString(DRow["SHAPECODE"]);

                clsFindRap.COLOR_ID = Val.ToInt32(DRow["PCOLOR_ID"]);
                clsFindRap.COLORCODE = Val.ToString(DRow["PCOLORCODE"]);

                clsFindRap.CLARITY_ID = Val.ToInt32(DRow["PCLARITY_ID"]);
                clsFindRap.CLARITYCODE = Val.ToString(DRow["PCLARITYCODE"]);

                clsFindRap.CARAT = Val.Val(DRow["POLISHCARAT"]);
                clsFindRap.CUTCODE = Val.ToString(DRow["PCUTCODE"]);
                clsFindRap.POLCODE = Val.ToString(DRow["PPOLCODE"]);
                clsFindRap.SYMCODE = Val.ToString(DRow["PSYMCODE"]);

                clsFindRap.FLCODE = Val.ToString(DRow["PFLCODE"]);
                clsFindRap.RAPDATE = Val.SqlDate(CmbRapDate.SelectedItem.ToString());

                clsFindRap = new BOFindRap().FindRapWithUpDown(clsFindRap);

                DRow["PPRICEPERCARAT"] = clsFindRap.PRICEPERCARAT;
                DRow["PAMOUNT"] = Math.Round(clsFindRap.AMOUNT, 0);
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
    }
}
