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
    public partial class FrmParameter : DevExpress.XtraEditors.XtraForm
    {
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOMST_Parameter ObjMast = new BOMST_Parameter();
        BOFormPer ObjPer = new BOFormPer();
        DataTable DtabPara = new DataTable();


        #region Property Settings

        public FrmParameter()
        {
            InitializeComponent();
        }


        public void ShowForm(string pStrFormType)
        {

            GrdDet.Columns["KAPANFINALREPORTGRP"].Visible = false;

            if (pStrFormType == "SHAPE")
            {
                this.Text = "SHAPE MASTER";
                CmbParameterType.SelectedItem = "SHAPE";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
            }
            else if (pStrFormType == "CHARNI")
            {
                this.Text = "CHARNI MASTER";
                CmbParameterType.SelectedItem = "CHARNI";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
            }
            else if (pStrFormType == "PURITY")
            {
                this.Text = "PURITY MASTER";
                CmbParameterType.SelectedItem = "PURITY";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
            }
            else if (pStrFormType == "REJECTION")
            {
                this.Text = "REJECTION BOX MASTER";
                CmbParameterType.SelectedItem = "REJECTION";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
            }
            else if (pStrFormType == "PROCESS")
            {
                this.Text = "PROCESS MASTER";
                CmbParameterType.SelectedItem = "PROCESS";
                CmbParameterType.Enabled = false;

                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;

                GrdDet.Columns["KAPANFINALREPORTGRP"].Visible = true;
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
                GrdDet.Columns["ISCOMMANJANGED"].Visible = true;
                GrdDet.Columns["ISCOMMANJANGED"].VisibleIndex = 8;
               

            }
            else if (pStrFormType == "DESIGNATION")
            {
                this.Text = "DESIGNATION MASTER";
                CmbParameterType.SelectedItem = "DESIGNATION";
                CmbParameterType.Enabled = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
            }


            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DtabPara.Columns.Add("PARA_ID", typeof(System.Int32));
            DtabPara.Columns.Add("PARACODE", typeof(System.String));
            DtabPara.Columns.Add("PARANAME", typeof(System.String));
            DtabPara.Columns.Add("PROCESSGROUP", typeof(System.String));
            DtabPara.Columns.Add("SHORTNAME", typeof(System.String));
            DtabPara.Columns.Add("ISACTIVE", typeof(System.Boolean));
            DtabPara.Columns.Add("ISCOMMANJANGED", typeof(System.Boolean));
            DtabPara.Columns.Add("SEQUENCENO", typeof(System.Int32));
            DtabPara.Columns.Add("REMARK", typeof(System.String));
            DtabPara.Columns.Add("REQPRDTYPE_ID", typeof(System.String));
            DtabPara.Columns.Add("PROCESS_ID",typeof(System.String));
            DtabPara.Columns.Add("SUBGROUPNAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADFILENAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERPATH", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERUSERNAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERPASSWORD", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADFILENAME", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERPATH", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERUSERNAME", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERPASSWORD", typeof(System.String));

            Fill();

            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbPrdType.DataSource = DTabPrdType;
            RepCmbPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbPrdType.ValueMember = "PRDTYPE_ID";

            DataTable DTabLockAmtPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbLockAmtPrdType.DataSource = DTabLockAmtPrdType;
            RepCmbLockAmtPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbLockAmtPrdType.ValueMember = "PRDTYPE_ID";

            DataTable DTabprocess = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
            RepProcess.DataSource = DTabprocess;
            RepProcess.DisplayMember = "PROCESSNAME";
            RepProcess.ValueMember = "PROCESS_ID";


            this.Show();
            CmbParameterType.SelectedIndex = 0;
        }

        public void ShowForm()
        {

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;

            DtabPara.Columns.Add("PARA_ID", typeof(System.Int32));
            DtabPara.Columns.Add("PARACODE", typeof(System.String));
            DtabPara.Columns.Add("PARANAME", typeof(System.String));
            DtabPara.Columns.Add("PROCESSGROUP", typeof(System.String));
            DtabPara.Columns.Add("ISACTIVE", typeof(System.Boolean));
            DtabPara.Columns.Add("ISCOMMANJANGED", typeof(System.Boolean));
            DtabPara.Columns.Add("SHORTNAME", typeof(System.String));
            DtabPara.Columns.Add("SEQUENCENO", typeof(System.Int32));
            DtabPara.Columns.Add("REMARK", typeof(System.String));
            DtabPara.Columns.Add("REQPRDTYPE_ID", typeof(System.String));
            DtabPara.Columns.Add("PROCESS_ID", typeof(System.String));
            DtabPara.Columns.Add("SUBGROUPNAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADFILENAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERPATH", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERUSERNAME", typeof(System.String));
            DtabPara.Columns.Add("UPLOADSERVERPASSWORD", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADFILENAME", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERPATH", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERUSERNAME", typeof(System.String));
            DtabPara.Columns.Add("DOWNLOADSERVERPASSWORD", typeof(System.String));


            BtnAdd_Click(null, null);
            Fill();
            this.Show();


            DataTable DTabPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbPrdType.DataSource = DTabPrdType;
            RepCmbPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbPrdType.ValueMember = "PRDTYPE_ID";


            DataTable DTabLockAmtPrdType = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PRDTYPE);
            RepCmbLockAmtPrdType.DataSource = DTabLockAmtPrdType;
            RepCmbLockAmtPrdType.DisplayMember = "PRDTYPENAME";
            RepCmbLockAmtPrdType.ValueMember = "PRDTYPE_ID";

            DataTable DTabprocess = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
            RepProcess.DataSource = DTabprocess;
            RepProcess.DisplayMember = "PROCESSNAME";
            RepProcess.ValueMember = "PROCESS_ID";

            CmbParameterType.SelectedIndex = 0;
        }


        public void ShowForm1(string pStrParaType)
        {

            ObjPer.GetFormPermission(Val.ToString(this.Tag));
            BtnSave.Enabled = ObjPer.ISINSERT;
            deleteSelectedAmountToolStripMenuItem.Enabled = ObjPer.ISDELETE;

            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();


            DataTable DtabSize = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENTMIX);
            rpChkCmbClarityWiseDept.DataSource = DtabSize;
            rpChkCmbClarityWiseDept.DisplayMember = "DEPARTMENTNAME";
            rpChkCmbClarityWiseDept.ValueMember = "DEPARTMENT_ID";

            BtnAdd_Click(null, null);
            Fill();
            this.Show();

            CmbParameterType.SelectedItem = pStrParaType;
            CmbParameterType.Enabled = false;
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


                foreach (DataRow Dr in DtabPara.Rows)
                {
                    ParameterMasterProperty Property = new ParameterMasterProperty();

                    //Property.ITEMGROUP_ID = Val.ToInt64(txtParaType.Text);
                    Property.PARATYPE = Val.ToString(CmbParameterType.SelectedItem);

                    if (Val.ToString(Dr["PARACODE"]).Trim().Equals(string.Empty) || Val.ToString(Dr["PARANAME"]).Trim().Equals(string.Empty))
                        continue;

                    Property.PARA_ID = Val.ToInt32(Dr["PARA_ID"]);
                    Property.PARACODE = Val.ToString(Dr["PARACODE"]);
                    Property.PARANAME = Val.ToString(Dr["PARANAME"]);
                    Property.SHORTNAME = Val.ToString(Dr["SHORTNAME"]);

                    Property.PROCESSGROUP = Val.ToString(Dr["PROCESSGROUP"]);
                    Property.LABCODE = Val.ToString(Dr["LABCODE"]);

                    Property.SEQUENCENO = Val.ToInt32(Dr["SEQUENCENO"]);
                    Property.ISACTIVE = Val.ToBoolean(Dr["ISACTIVE"]);
                    Property.ISCOMMANJANGED = Val.ToBoolean(Dr["ISCOMMANJANGED"]);
                    Property.ISFINALISSUE = Val.ToBoolean(Dr["ISFINALISSUE"]);

                    Property.REMARK = Val.ToString(Dr["REMARK"]);
                    Property.REQPRDTYPE_ID = Val.Trim(Dr["REQPRDTYPE_ID"]);
                    Property.POPUPPROCESS_ID = Val.Trim(Dr["POPUPPROCESS_ID"]);

                    Property.UPPERPARA_ID = Val.ToString(Dr["UPPERPARANAME"]).Trim().Equals(string.Empty) ? 0 : Val.ToInt32(Dr["UPPERPARA_ID"]);

                    Property.NUMBEROFISSUE = Val.ToInt32(Dr["NUMBEROFISSUE"]);

                    Property.LOCKAMTPRDTYPE_ID = Val.Trim(Dr["LOCKAMTPRDTYPE_ID"]);

                    Property.DUEHOURS = Val.Val(Dr["DUEHOURS"]);
                    Property.LOSSPER = Val.Val(Dr["LOSSPER"]);

                    Property.KAPANROLLINGGROUP = Val.ToString(Dr["KAPANROLLINGGROUP"]).Trim();
                    Property.ISDISPLAYONRETURN = Val.ToBoolean(Dr["ISDISPLAYONRETURN"]);

                    Property.LOCATION_ID = Val.ToInt64(Dr["LOCATION_ID"]);

                    Property.KAPANRUNNINGGROUP = Val.ToString(Dr["KAPANRUNNINGGROUP"]).Trim();
                    Property.ISLOSSDPT = Val.ToBoolean(Dr["ISLOSSDPT"]);

                    Property.KAPANFINALREPORTGRP = Val.ToString(Dr["KAPANFINALRPTGROUP"]).Trim();
                    Property.SUBGROUP = Val.ToString(Dr["SUBGROUPNAME"]);

                    Property.UPLOADFILENAME = Val.ToString(Dr["UPLOADFILENAME"]);
                    Property.UPLOADSERVERPATH = Val.ToString(Dr["UPLOADSERVERPATH"]);
                    Property.UPLOADSERVERUSERNAME = Val.ToString(Dr["UPLOADSERVERUSERNAME"]);
                    Property.UPLOADSERVERPASSWORD = Val.ToString(Dr["UPLOADSERVERPASSWORD"]);
                    Property.DOWNLOADFILENAME = Val.ToString(Dr["DOWNLOADFILENAME"]);
                    Property.DOWNLOADSERVERPATH = Val.ToString(Dr["DOWNLOADSERVERPATH"]);
                    Property.DOWNLOADSERVERUSERNAME = Val.ToString(Dr["DOWNLOADSERVERUSERNAME"]);
                    Property.DOWNLOADSERVERPASSWORD = Val.ToString(Dr["DOWNLOADSERVERPASSWORD"]);
                    Property.LABOURPCS = Val.ToInt32(Dr["LABOURPCS"]);

                    Property.CLARITYWISEDEPARTMENT_ID = Val.Trim(Dr["CLARITYWISEDEPARTMENT_ID"]);//gUNJAN:25/03/2023

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
            if (Val.ToString(CmbParameterType.SelectedItem) == "PROCESS")
            {
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["ISFINALISSUE"].Visible = true;
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = true;
                GrdDet.Columns["LOCKAMTPRDTYPE_ID"].Visible = true;
                GrdDet.Columns["KAPANROLLINGGROUP"].Visible = true;
                GrdDet.Columns["KAPANRUNNINGGROUP"].Visible = true;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;
                GrdDet.Columns["ISFINALISSUE"].VisibleIndex = 4;
                GrdDet.Columns["KAPANRUNNINGGROUP"].VisibleIndex = 9;
                
                GrdDet.Columns["POPUPPROCESS_ID"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
                GrdDet.Columns["SUBGROUPNAME"].Visible = true;
                GrdDet.Columns["UPLOADFILENAME"].Visible = true;
                GrdDet.Columns["UPLOADSERVERPATH"].Visible = true;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].Visible = true;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].Visible = true;
                GrdDet.Columns["DOWNLOADFILENAME"].Visible = true;
                GrdDet.Columns["DOWNLOADSERVERPATH"].Visible = true;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].Visible = true;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].Visible = true;

                
                GrdDet.Columns["UPLOADFILENAME"].VisibleIndex = 21;
                GrdDet.Columns["UPLOADSERVERPATH"].VisibleIndex = 22;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].VisibleIndex = 23;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].VisibleIndex = 24;
                GrdDet.Columns["DOWNLOADFILENAME"].VisibleIndex = 25;
                GrdDet.Columns["DOWNLOADSERVERPATH"].VisibleIndex = 26;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].VisibleIndex = 27;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].VisibleIndex = 28;


                GrdDet.Columns["DUEHOURS"].Visible = true; //#P
                GrdDet.Columns["LOSSPER"].Visible = true;  //#P
                GrdDet.Columns["UPPERPARANAME"].Visible = true;
                GrdDet.Columns["UPPERPARANAME"].Caption = "Upper Process";
                GrdDet.Columns["NUMBEROFISSUE"].Visible = true;
                GrdDet.Columns["ISDISPLAYONRETURN"].Visible = true;
                GrdDet.Columns["NUMBEROFISSUE"].VisibleIndex = 7;
                GrdDet.Columns["ISLOSSDPT"].Visible = true;

                repCmbProcessGroup.Items.Clear();
                repCmbProcessGroup.Items.Add("CLV");
                repCmbProcessGroup.Items.Add("MFG");
                repCmbProcessGroup.Items.Add("COMMON");
                repCmbProcessGroup.Items.Add("BOMBAY");
                repCmbProcessGroup.Items.Add("OTHER");
                repCmbProcessGroup.Items.Add("GALAXY_JOB");

                RepCmbKapanRollingGroup.Items.Clear();
                RepCmbKapanRollingGroup.Items.Add("CLV");
                RepCmbKapanRollingGroup.Items.Add("MFG");
                RepCmbKapanRollingGroup.Items.Add("GRADING");
                RepCmbKapanRollingGroup.Items.Add("BLACK");

                RepCmbKpRunningGRP.Items.Clear();
                RepCmbKpRunningGRP.Items.Add("Issue Wise");
                RepCmbKpRunningGRP.Items.Add("Only Loss");
                RepCmbKpRunningGRP.Items.Add("Both");


            }
            else if (Val.ToString(CmbParameterType.SelectedItem) == "DEPARTMENT")
            {
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["LOCKAMTPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["KAPANROLLINGGROUP"].Visible = true;
                GrdDet.Columns["KAPANRUNNINGGROUP"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;
                GrdDet.Columns["ISCOMMANJANGED"].VisibleIndex = 8;
                GrdDet.Columns["POPUPPROCESS_ID"].Visible = true;
                GrdDet.Columns["DUEHOURS"].Visible = true; //#P
                GrdDet.Columns["LOSSPER"].Visible = false;  //#P
                GrdDet.Columns["UPPERPARANAME"].Visible = true;
                GrdDet.Columns["NUMBEROFISSUE"].Visible = false;
                GrdDet.Columns["ISDISPLAYONRETURN"].Visible = false;
                GrdDet.Columns["ISLOSSDPT"].Visible = true;
                GrdDet.Columns["SUBGROUPNAME"].VisibleIndex = 4;
                GrdDet.Columns["UPLOADFILENAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["DOWNLOADFILENAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].Visible = false;





                GrdDet.Columns["LOCATIONNAME"].Visible=true;
                GrdDet.Columns["LOCATIONNAME"].VisibleIndex = 4;

                repCmbProcessGroup.Items.Clear();
                repCmbProcessGroup.Items.Add("CLV");
                repCmbProcessGroup.Items.Add("MFG");
                repCmbProcessGroup.Items.Add("COMMON");
                repCmbProcessGroup.Items.Add("BOMBAY");
                repCmbProcessGroup.Items.Add("OTHER");
                repCmbProcessGroup.Items.Add("GALAXY_JOB");

                RepCmbKapanRollingGroup.Items.Clear();
                RepCmbKapanRollingGroup.Items.Add("CLV");
                RepCmbKapanRollingGroup.Items.Add("MFG");
                RepCmbKapanRollingGroup.Items.Add("GRADING");
                RepCmbKapanRollingGroup.Items.Add("BLACK");
                GrdDet.Columns["LABOURPCS"].Visible = false;

            }
            else if (Val.ToString(CmbParameterType.SelectedItem) == "DESIGNATION")
            {
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["LOCKAMTPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = true;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;

                GrdDet.Columns["KAPANROLLINGGROUP"].Visible = true;
                GrdDet.Columns["ISDISPLAYONRETURN"].Visible = false;
                GrdDet.Columns["KAPANRUNNINGGROUP"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].VisibleIndex = 3;
                GrdDet.Columns["ISLOSSDPT"].Visible = false;
                GrdDet.Columns["POPUPPROCESS_ID"].Visible = false;
                GrdDet.Columns["SUBGROUPNAME"].Visible = false;

                GrdDet.Columns["UPLOADFILENAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["DOWNLOADFILENAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].Visible = false;





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
                repCmbProcessGroup.Items.Add("GALAXY_JOB");

                RepCmbKapanRollingGroup.Items.Clear();
                RepCmbKapanRollingGroup.Items.Add("CLV");
                RepCmbKapanRollingGroup.Items.Add("MFG");
                RepCmbKapanRollingGroup.Items.Add("GRADING");
                RepCmbKapanRollingGroup.Items.Add("BLACK");

                GrdDet.Columns["DUEHOURS"].Visible = false; //#P
                GrdDet.Columns["LOSSPER"].Visible = false;  //#P
                GrdDet.Columns["UPPERPARANAME"].Visible = false;
                GrdDet.Columns["NUMBEROFISSUE"].Visible = false;
                GrdDet.Columns["LABOURPCS"].Visible = false;

            }
            else if (Val.ToString(CmbParameterType.SelectedItem) == "BREAKING_REASON")
            {
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["LOCKAMTPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["DUEHOURS"].Visible = false; //#P
                GrdDet.Columns["LOSSPER"].Visible = false;  //#P
                GrdDet.Columns["UPPERPARANAME"].Visible = false;
                GrdDet.Columns["NUMBEROFISSUE"].Visible = false;
                GrdDet.Columns["KAPANROLLINGGROUP"].Visible = false;
                GrdDet.Columns["ISDISPLAYONRETURN"].Visible = false;
                GrdDet.Columns["KAPANRUNNINGGROUP"].Visible = false;
                GrdDet.Columns["ISLOSSDPT"].Visible = false;
                GrdDet.Columns["POPUPPROCESS_ID"].Visible = false;
                GrdDet.Columns["SUBGROUPNAME"].Visible = false;
                GrdDet.Columns["UPLOADFILENAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["DOWNLOADFILENAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["LABOURPCS"].Visible = true;


            }
           else
            {
                GrdDet.Columns["REQPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["LOCKAMTPRDTYPE_ID"].Visible = false;
                GrdDet.Columns["PROCESSGROUP"].Visible = false;
                GrdDet.Columns["ISCOMMANJANGED"].Visible = false;
                GrdDet.Columns["ISFINALISSUE"].Visible = false;
                GrdDet.Columns["DUEHOURS"].Visible = false; //#P
                GrdDet.Columns["LOSSPER"].Visible = false;  //#P
                GrdDet.Columns["UPPERPARANAME"].Visible = false;
                GrdDet.Columns["NUMBEROFISSUE"].Visible = false;
                GrdDet.Columns["KAPANROLLINGGROUP"].Visible = false;
                GrdDet.Columns["ISDISPLAYONRETURN"].Visible = false;
                GrdDet.Columns["KAPANRUNNINGGROUP"].Visible = false;
                GrdDet.Columns["ISLOSSDPT"].Visible = false;
                GrdDet.Columns["POPUPPROCESS_ID"].Visible = false;
                GrdDet.Columns["SUBGROUPNAME"].Visible = false;
                GrdDet.Columns["UPLOADFILENAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["UPLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["UPLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["DOWNLOADFILENAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPATH"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERUSERNAME"].Visible = false;
                GrdDet.Columns["DOWNLOADSERVERPASSWORD"].Visible = false;
                GrdDet.Columns["LABOURPCS"].Visible = false;

            }

            if (Val.ToString(CmbParameterType.SelectedItem) == "MIX_CLARITY")//gUNJAN:25/03/2023
            {
                GrdDet.Columns["CLARITYWISEDEPARTMENT_ID"].Visible = true;
            }

            else
            {
                GrdDet.Columns["CLARITYWISEDEPARTMENT_ID"].Visible = false;

            }
            Fill();

        }

        private void repTxtUpperDept_KeyPress(object sender, KeyPressEventArgs e)
       {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Val.ToString(CmbParameterType.SelectedItem) == "DEPARTMENT")
                {
                    if (Global.OnKeyPressToOpenPopup(e))
                    {
                        DataRow DRow = GrdDet.GetFocusedDataRow();
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
                            GrdDet.SetFocusedRowCellValue("UPPERPARA_ID", Val.ToString(FrmSearch.mDRow["DEPARTMENT_ID"]));
                            GrdDet.SetFocusedRowCellValue("UPPERPARANAME", Val.ToString(FrmSearch.mDRow["DEPARTMENTNAME"]));
                        }
                        else
                        {
                            GrdDet.SetFocusedRowCellValue("UPPERPARA_ID", 0);
                            GrdDet.SetFocusedRowCellValue("UPPERPARANAME", string.Empty);
                        }
                        FrmSearch.Hide();
                        FrmSearch.Dispose();
                        FrmSearch = null;
                    }
                }
                else
                {//Process Name//
                    if (Global.OnKeyPressToOpenPopup(e))
                    {
                        DataRow DRow = GrdDet.GetFocusedDataRow();
                        FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                        FrmSearch.mSearchField = "PROCESSCODE,PROCESSNAME";
                        FrmSearch.mSearchText = e.KeyChar.ToString();
                        this.Cursor = Cursors.WaitCursor;
                        FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_PROCESS);
                        FrmSearch.mColumnsToHide = "PROCESS_ID";
                        this.Cursor = Cursors.Default;
                        FrmSearch.ShowDialog();
                        e.Handled = true;
                        if (FrmSearch.mDRow != null)
                        {
                            GrdDet.SetFocusedRowCellValue("UPPERPARA_ID", Val.ToString(FrmSearch.mDRow["PROCESS_ID"]));
                            GrdDet.SetFocusedRowCellValue("UPPERPARANAME", Val.ToString(FrmSearch.mDRow["PROCESSNAME"]));
                        }
                        else
                        {
                            GrdDet.SetFocusedRowCellValue("UPPERPARA_ID", 0);
                            GrdDet.SetFocusedRowCellValue("UPPERPARANAME", string.Empty);
                        }
                        FrmSearch.Hide();
                        FrmSearch.Dispose();
                        FrmSearch = null;
                    }
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repLocName_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                DataRow DRow = GrdDet.GetFocusedDataRow();
                FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                FrmSearch.mSearchField = "LOCATIONCODE,LOCATIONNAME";
                FrmSearch.mSearchText = e.KeyChar.ToString();
                this.Cursor = Cursors.WaitCursor;
                FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_LOCATION);
                FrmSearch.mColumnsToHide = "LOCATION_ID";
                this.Cursor = Cursors.Default;
                FrmSearch.ShowDialog();
                e.Handled = true;
                if (FrmSearch.mDRow != null)
                {
                    GrdDet.SetFocusedRowCellValue("LOCATION_ID", Val.ToInt64(FrmSearch.mDRow["LOCATION_ID"]));
                    GrdDet.SetFocusedRowCellValue("LOCATIONNAME", Val.ToString(FrmSearch.mDRow["LOCATIONNAME"]));
                }
                else
                {
                    GrdDet.SetFocusedRowCellValue("LOCATION_ID", 0);
                    GrdDet.SetFocusedRowCellValue("LOCATIONNAME", string.Empty);
                }
                FrmSearch.Hide();
                FrmSearch.Dispose();
                FrmSearch = null;
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
        }

        private void repSubGroup_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (GrdDet.FocusedRowHandle < 0)
                    return;

                if (Global.OnKeyPressToOpenPopup(e))
                {
                    GrdDet.PostEditor();
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "SUBGROUPNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    FrmSearch.mISPostBack = true;
                    FrmSearch.mISPostBackColumn = "SUBGROUPNAME";
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = new BusLib.BOComboFill().FillCmb(BusLib.BOComboFill.TABLE.MST_DEPARTMENTSUBGROUP);
                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        GrdDet.SetFocusedRowCellValue("SUBGROUPNAME", Val.ToString(FrmSearch.mDRow["SUBGROUPNAME"]));
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

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            try
            {
                GrdDet.BestFitColumns();
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message.ToString());
            }
        }
    }
}
