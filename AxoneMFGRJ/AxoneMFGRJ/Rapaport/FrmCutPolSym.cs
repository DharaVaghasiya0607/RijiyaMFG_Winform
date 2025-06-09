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

namespace AxoneMFGRJ.Rapaport
{
    public partial class FrmCutPolSym : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Parameter ObjMast = new BOMST_Parameter();
        DataTable DtabPara = new DataTable();


        #region Property Settings

        public FrmCutPolSym()
        {
            InitializeComponent();
        }


        public void ShowForm(string pStrFormType)
        {

            if (pStrFormType == "SHAPE")
            {
                this.Text = "SHAPE MASTER";
                CmbParameterType.SelectedItem = "SHAPE";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
            }
            else if (pStrFormType == "CHARNI")
            {
                this.Text = "CHARNI MASTER";
                CmbParameterType.SelectedItem = "CHARNI";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
            }
            else if (pStrFormType == "PURITY")
            {
                this.Text = "PURITY MASTER";
                CmbParameterType.SelectedItem = "PURITY";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;

            }
            else if (pStrFormType == "REJECTION")
            {
                this.Text = "REJECTION BOX MASTER";
                CmbParameterType.SelectedItem = "REJECTION";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;

            }
            else if (pStrFormType == "PROCESS")
            {
                this.Text = "PROCESS MASTER";
                CmbParameterType.SelectedItem = "PROCESS";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;

                //GrdDet.Columns["LABOURTYPE"].VisibleIndex = 3;
                //GrdDet.Columns["LABOURRATE"].VisibleIndex = 4;
                //GrdDet.Columns["PARANAME"].VisibleIndex = 5;

            }
            else if (pStrFormType == "DEPARTMENT")
            {
                this.Text = "DEPARTMENT MASTER";
                CmbParameterType.SelectedItem = "DEPARTMENT";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = true;
            }
            else if (pStrFormType == "DESIGNATION")
            {
                this.Text = "DESIGNATION MASTER";
                CmbParameterType.SelectedItem = "DESIGNATION";
                CmbParameterType.Enabled = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = false;
            }


            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DtabPara.Columns.Add("PARA_ID", typeof(System.Int32));
            DtabPara.Columns.Add("PARACODE", typeof(System.String));
            DtabPara.Columns.Add("PARANAME", typeof(System.String));
            DtabPara.Columns.Add("PROCESSGROUP", typeof(System.String));
            DtabPara.Columns.Add("SHORTNAME", typeof(System.String));
            DtabPara.Columns.Add("ISACTIVE", typeof(System.Boolean));
            DtabPara.Columns.Add("SEQUENCENO", typeof(System.Int32));
            DtabPara.Columns.Add("REMARK", typeof(System.String));
            DtabPara.Columns.Add("REQPRDTYPE_ID", typeof(System.String));

           
            BtnAdd_Click(null, null);
            Fill();

            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbPrdType.DataSource = DTabPrdType;
            RepCmbPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbPrdType.ValueMember = "PRDTYPE_ID";

            this.Show();
            CmbParameterType.SelectedIndex = 0;
        }
      
        public void ShowForm()
        {
           
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            DtabPara.Columns.Add("PARA_ID", typeof(System.Int32));
            DtabPara.Columns.Add("PARACODE", typeof(System.String));
            DtabPara.Columns.Add("PARANAME", typeof(System.String));
            DtabPara.Columns.Add("PROCESSGROUP", typeof(System.String));
            DtabPara.Columns.Add("ISACTIVE", typeof(System.Boolean));
            DtabPara.Columns.Add("SHORTNAME", typeof(System.String));
            DtabPara.Columns.Add("SEQUENCENO", typeof(System.Int32));
            DtabPara.Columns.Add("REMARK", typeof(System.String));
            DtabPara.Columns.Add("REQPRDTYPE_ID", typeof(System.String));

            BtnAdd_Click(null, null);
            Fill();
            this.Show();


            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbPrdType.DataSource = DTabPrdType;
            RepCmbPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbPrdType.ValueMember = "PRDTYPE_ID";

            CmbParameterType.SelectedIndex = 0;
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
        }

        #endregion

        public bool CheckDuplicate(string ColName, string ColValue, int IntRowIndex, string StrMsg)
        {
            if (Val.ToString(ColValue).Trim().Equals(string.Empty))
                return false;

            var Result = from row in DtabPara.AsEnumerable()
                         where Val.ToString(row[ColName]).ToUpper() == Val.ToString(ColValue).ToUpper() && row.Table.Rows.IndexOf(row) != IntRowIndex
                         select row;


            if (Result.Any())
            {
                Global.Message(CmbParameterType.SelectedItem + " " + StrMsg + " ALREADY EXISTS.");
                return true;
            }
            return false;
        }

        #region Validation

        private bool ValSave()
        {
            //if (txtItemGroupCode.Text.Trim().Length == 0)
            //{
            //    Global.Message("Group Code Is Required");
            //    txtItemGroupCode.Focus();
            //    return false;
            //}
            int IntCol = 0, IntRow = 0;
            foreach (DataRow dr in DtabPara.Rows)
            {
                //For Update Validation
                if (Val.ToString(dr["PARACODE"]).Trim().Equals(string.Empty) && !Val.ToString(dr["PARA_ID"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter Code");
                    IntCol = 0;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
                //end as


                if (Val.ToString(dr["PARACODE"]).Trim().Equals(string.Empty))
                {
                    if (DtabPara.Rows.Count == 1)
                    {
                        Global.Message("Please Enter Code");
                        IntCol = 0;
                        IntRow = dr.Table.Rows.IndexOf(dr);
                        break;

                    }
                    else
                        continue;
                }
                if (Val.ToString(dr["PARANAME"]).Trim().Equals(string.Empty))
                {
                    Global.Message("Please Enter " + CmbParameterType.SelectedItem + " Name");
                    IntCol = 1;
                    IntRow = dr.Table.Rows.IndexOf(dr);
                    break;
                }
               
            }
            if (IntRow > 0)
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
            DtabPara.Rows.Clear();
            DtabPara.Rows.Add(DtabPara.NewRow());            
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


                foreach (DataRow Dr in DtabPara.GetChanges().Rows)
                {
                    ParameterMasterProperty Property = new ParameterMasterProperty();

                    //Property.ITEMGROUP_ID = Val.ToInt64(txtParaType.Text);
                    Property.PARATYPE = Val.ToString(CmbParameterType.SelectedItem);
                
                    if (Val.ToString(Dr["PARACODE"]).Trim().Equals(string.Empty)|| Val.ToString(Dr["PARANAME"]).Trim().Equals(string.Empty))
                        continue;

                    Property.PARA_ID = Val.ToInt32(Dr["PARA_ID"]);
                    Property.PARACODE = Val.ToString(Dr["PARACODE"]);
                    Property.PARANAME = Val.ToString(Dr["PARANAME"]);
                    Property.SHORTNAME = Val.ToString(Dr["SHORTNAME"]);

                    Property.PROCESSGROUP = Val.ToString(Dr["PROCESSGROUP"]);

                    Property.SEQUENCENO = Val.ToInt32(Dr["SEQUENCENO"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.ISFINALISSUE = Val.ToBoolean(Dr["ISFINALISSUE"]);
                    
                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property.REQPRDTYPE_ID = Val.Trim(Dr["REQPRDTYPE_ID"]);

                    Property = ObjMast.Save(Property);

                    ReturnMessageDesc = Property.ReturnMessageDesc;
                    ReturnMessageType = Property.ReturnMessageType;

                    Property = null;
                }
                DtabPara.AcceptChanges();

                Global.Message(ReturnMessageDesc);

                if (ReturnMessageType == "SUCCESS")
                {
                    Fill();
                    //BtnAdd_Click(null, null);

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

        public void Fill()
        {
            DtabPara = ObjMast.Fill(Val.ToString(CmbParameterType.SelectedItem));
            DtabPara.Rows.Add(DtabPara.NewRow());
            MainGrid.DataSource = DtabPara;
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
            Global.ExcelExport(CmbParameterType.SelectedItem.ToString() + "List", GrdDet);
        }

        private void repTxtRemark_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {


                    DataRow dr = GrdDet.GetFocusedDataRow();
                    if (!Val.ToString(dr["PARACODE"]).Equals(string.Empty) && !Val.ToString(dr["PARANAME"]).Equals(string.Empty) && GrdDet.IsLastRow)
                    {
                        DtabPara.Rows.Add(DtabPara.NewRow());
                        //DtabPara.AcceptChanges();

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

        private void repTxtParaName_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (CheckDuplicate("PARANAME", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Name"))
                e.Cancel = true;
            return;

        }

        private void deleteSelectedAmountToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle >= 0)
                {
                    if (Global.Confirm("ARE YOU SURE YOU WANT TO DELETE ENTRY") == System.Windows.Forms.DialogResult.Yes)
                    {
                        ParameterMasterProperty Property = new ParameterMasterProperty();
                        DataRow Drow = GrdDet.GetDataRow(GrdDet.FocusedRowHandle);
                        Property.PARA_ID = Val.ToInt32(Drow["PARA_ID"]);
                        Property = ObjMast.Delete(Property);

                        if (Property.ReturnMessageType == "SUCCESS")
                        {
                            Global.Message("ENTRY DELETED SUCCESSFULLY");
                            DtabPara.Rows.RemoveAt(GrdDet.FocusedRowHandle);
                            DtabPara.AcceptChanges();
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

        private void repTxtParaCode_Validating(object sender, CancelEventArgs e)
        {
            DataRow Dr = GrdDet.GetFocusedDataRow();
            if (CheckDuplicate("PARACODE", Val.ToString(GrdDet.EditingValue), GrdDet.FocusedRowHandle, "Code"))
                e.Cancel = true;
            return;

        }

        private void CmbParameterType_SelectedIndexChanged(object sender, EventArgs e)
        {
            Clear();
            if (Val.ToString(CmbParameterType.SelectedItem) == "PROCESS" )
            {
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["ISFINALISSUE"].Visible = true;
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = true;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;
                GrdDet.Columns["ISFINALISSUE"].VisibleIndex = 4;

                repCmbProcessGroup.Items.Clear();
                repCmbProcessGroup.Items.Add("CLV");
                repCmbProcessGroup.Items.Add("MFG");
                repCmbProcessGroup.Items.Add("COMMON");
                repCmbProcessGroup.Items.Add("BOMBAY");
                repCmbProcessGroup.Items.Add("OTHER");
            }
            else if (Val.ToString(CmbParameterType.SelectedItem) == "DEPARTMENT")
            {
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;

                repCmbProcessGroup.Items.Clear();
                repCmbProcessGroup.Items.Add("CLV");
                repCmbProcessGroup.Items.Add("MFG");
                repCmbProcessGroup.Items.Add("COMMON");
                repCmbProcessGroup.Items.Add("BOMBAY");
                repCmbProcessGroup.Items.Add("OTHER");
            }
            else if (Val.ToString(CmbParameterType.SelectedItem) == "DESIGNATION")
            {
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;

                repCmbProcessGroup.Items.Clear();
                repCmbProcessGroup.Items.Add("ADMIN");
                repCmbProcessGroup.Items.Add("HEAD");
                repCmbProcessGroup.Items.Add("MANAGER");
                repCmbProcessGroup.Items.Add("MARKER");
                repCmbProcessGroup.Items.Add("CHECKER");
                repCmbProcessGroup.Items.Add("GRADER");
                repCmbProcessGroup.Items.Add("WORKER");
                repCmbProcessGroup.Items.Add("OPERATOR");
                repCmbProcessGroup.Items.Add("OTHER");
            }
            else
            {
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
            }
            Fill();
            
        }

    }
}
