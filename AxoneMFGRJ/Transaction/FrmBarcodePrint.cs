using BusLib;
using BusLib.Configuration;
using BusLib.Attendance;
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
using System.Windows.Forms;
using BusLib.TableName;
using AxoneMFGRJ.Utility;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.Utils;
using Microsoft.VisualBasic;
using BusLib.Master;
using Stimulsoft.Report.Dictionary;
using Stimulsoft.Report;
using System.Runtime.InteropServices;
using System.Drawing.Printing;

namespace AxoneMFGRJ.Transaction
{
    public partial class FrmBarcodePrint : DevExpress.XtraEditors.XtraForm
    {
        [DllImport("Winspool.drv", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool SetDefaultPrinter(string printerName);
        string GetDefaultPrinter()
        {
            PrinterSettings settings = new PrinterSettings();
            foreach (string printer in PrinterSettings.InstalledPrinters)
            {
                settings.PrinterName = printer;
                if (settings.IsDefaultPrinter)
                    return printer;
            }
            return string.Empty;
        }

        BODevGridSelection ObjGridSelection;
        AxonDataLib.BOFormEvents ObjFormEvent = new AxonDataLib.BOFormEvents();
        AxonDataLib.BOConversion Val = new AxonDataLib.BOConversion();
        BOTRN_RoughPurchase ObjMast = new BOTRN_RoughPurchase();
        BOTRN_KapanCreate ObjKapan = new BOTRN_KapanCreate();
        BOTRN_SinglePacketCreate ObjPacket = new BOTRN_SinglePacketCreate();

        DataTable DtabPacket = new DataTable();
        DataTable DTabPacketLiveStock = new DataTable();

        string StrBatchFileName = "";
        string StrBarcodeTxtFileName = "";
        string StrBPrintType = "";
        string pStrOpe = "";

        #region Property Settings

        public FrmBarcodePrint()
        {
            InitializeComponent();
        }

        public void ShowForm()
        {
            Val.FormGeneralSetting(this);
            AttachFormDefaultEvent();

            EmployeeActionRightsProperty PropertyEmployeeActionRights = new BOMST_FormPermission().EmployeeActionRightsGetDataByPK(BusLib.Configuration.BOConfiguration.gEmployeeProperty.LEDGER_ID);

            StrBPrintType = PropertyEmployeeActionRights.BPRINTTYPE;
            if (StrBPrintType == "TSC")
                RbtTSC.Checked = true;
            else if (StrBPrintType.ToUpper() == "CITIZEN")
                RbtCitizen.Checked = true;
            else if (StrBPrintType.ToUpper() == "TSCGALAXY")
                RbtTscGalaxy.Checked = true;
            else if (StrBPrintType == "")
            {
                RbtTSC.Checked = true;
                StrBPrintType = "TSC";
            }

            lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + StrBPrintType + "] Printer";
            //lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTSC.Text + "] Printer";
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

        private void BtnSearch_Click(object sender, EventArgs e)
        {

            if (txtKapan.Text.Trim().Length == 0)
            {
                Global.Message("Kapan Name Is Required");
                txtKapan.Focus();
                return;
            }
            this.Cursor = Cursors.WaitCursor;

            DTabPacketLiveStock = ObjKapan.GetPacketDataForBarcodePrint(txtKapan.Text, "", "", Val.ToInt(txtFromPacketNo.Text), Val.ToInt(txtToPacketNo.Text), Val.ToString(txtTag.Text),"");
            MainGrid.DataSource = DTabPacketLiveStock;
            MainGrid.Refresh();

            if (MainGrid.RepositoryItems.Count == 0)
            {
                ObjGridSelection = new BODevGridSelection();
                ObjGridSelection.View = GrdDet;
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }
            else
            {
                ObjGridSelection.ClearSelection();
            }
            GrdDet.Columns["COLSELECTCHECKBOX"].Fixed = FixedStyle.Left;
            if (ObjGridSelection != null)
            {
                ObjGridSelection.ClearSelection();
                ObjGridSelection.CheckMarkColumn.VisibleIndex = 0;
            }

            this.Cursor = Cursors.Default;
        }

        private DataTable GetTableOfSelectedRows(GridView view, Boolean IsSelect)
        {
            if (view.RowCount <= 0)
            {
                return null;
            }
            ArrayList aryLst = new ArrayList();


            DataTable resultTable = new DataTable();
            DataTable sourceTable = null;
            sourceTable = ((DataView)view.DataSource).Table;

            if (IsSelect)
            {
                aryLst = ObjGridSelection.GetSelectedArrayList();
                resultTable = sourceTable.Clone();
                for (int i = 0; i < aryLst.Count; i++)
                {
                    DataRowView oDataRowView = aryLst[i] as DataRowView;
                    resultTable.Rows.Add(oDataRowView.Row.ItemArray);
                }
            }

            return resultTable;
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnBestFit_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;
            GrdDet.BestFitColumns();
            this.Cursor = Cursors.Default;
        }

        private void txtKapan_KeyPress(object sender, KeyPressEventArgs e)
        {
            try
            {
                if (Global.OnKeyPressToOpenPopup(e))
                {
                    FrmSearchPopupBox FrmSearch = new FrmSearchPopupBox();
                    FrmSearch.mSearchField = "KAPANNAME";
                    FrmSearch.mSearchText = e.KeyChar.ToString();
                    this.Cursor = Cursors.WaitCursor;
                    FrmSearch.mDTab = ObjPacket.FindKapan();

                    this.Cursor = Cursors.Default;
                    FrmSearch.ShowDialog();
                    e.Handled = true;
                    if (FrmSearch.mDRow != null)
                    {
                        txtKapan.Text = Val.ToString(FrmSearch.mDRow["KAPANNAME"]);
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

        private void txtMarkerID_KeyPress(object sender, KeyPressEventArgs e)
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
                        txtMarkerID.Text = Val.ToString(FrmSearch.mDRow["EMPLOYEENAME"]);
                        txtMarkerID.Tag = Val.ToString(FrmSearch.mDRow["EMPLOYEE_ID"]);
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

        private void BtnUpdateMarkerID_Click(object sender, EventArgs e)
        {
            Global.Message("YOU HAVE TO USE. FINAL ISSUE FORM FOR THIS UPDATE");
            return;

            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Update");
                return;
            }

            if (Global.Confirm("Are you Sure You Want To Update [ " + txtMarkerID.Text + " ] To All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (txtMarkerID.Text.Trim().Length == 0)
            {
                if (Global.Confirm("Marker ID Is Blank , Means You Want To Make It Blank ?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
            }
            string StrOpe = "";

            if (RbtMarker.Checked == true)
            {
                StrOpe = "Marker";
            }
            else if (RbtWorker.Checked == true)
            {
                StrOpe = "Worker";
            }

            this.Cursor = Cursors.WaitCursor;
            int IntRes = 0;
            foreach (DataRow DRow in DTab.Rows)
            {
                IntRes = IntRes + ObjPacket.UpdateMainMarkerIDInPacketMaster(StrOpe, Val.ToString(DRow["PACKET_ID"]), Val.ToInt64(txtMarkerID.Tag));
            }
            this.Cursor = Cursors.Default;

            if (IntRes != 0)
            {
                Global.Message("" + StrOpe + " ID ,,, SUCCESSFULLY UPDATED");
                BtnSearch_Click(null, null);
            }

        }

        private void BtnBarcodePrintCurrEmp_Click(object sender, EventArgs e)
        {
            try
            {
                pStrOpe = "BtnBarcodePrintCurrEmp";
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                #region Comment Code

                ////string fileLoc = Application.StartupPath + "\\PrintBarcodeData.txt";
                //string fileLoc = Application.StartupPath + "\\" + StrBarcodeTxtFileName + ".txt";
                //if (System.IO.File.Exists(fileLoc) == true)
                //{
                //    System.IO.File.Delete(fileLoc);
                //}

                //System.IO.File.Create(fileLoc).Dispose();


                ////foreach (DataRow DRow in DTab.Rows)
                ////{
                ////    Global.BarcodePrint(Val.ToString(DRow["KAPANNAME"]),
                ////        Val.ToString(DRow["PACKETNO"]),
                ////        Val.ToString(DRow["TAG"]),
                ////        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                ////        Val.ToString(DRow["LOTCARAT"]),
                ////        Val.ToString(DRow["EMPLOYEECODE"]));
                ////}

                //string StrKapanName = "";
                //int pIntPktNo = 0;
                //string pStrTag = "";
                //string Date = "";
                //string Carat = "";
                //string MarkerCode = "";



                //StreamWriter sw = new StreamWriter(fileLoc);


                /*
                    using (var sw = new StreamWriter(fileLoc, true))
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {
                            StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                            pIntPktNo = Val.ToInt32(DRow["PACKETNO"]);
                            pStrTag = Val.ToString(DRow["TAG"]);
                            Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                            Carat = Val.ToString(DRow["LOTCARAT"]);
                            MarkerCode = Val.ToString(DRow["EMPLOYEECODE"]);

                            string StrBarcode = StrKapanName + Environment.NewLine + pIntPktNo + Environment.NewLine + pStrTag;
                            string StrPrint = StrKapanName + "-" + pIntPktNo + "-" + pStrTag;
                            string OM = "OM";
                            sw.WriteLine("I8,A");
                            sw.WriteLine("ZN");
                            sw.WriteLine("q400");
                            sw.WriteLine("O");
                            sw.WriteLine("JF");
                            sw.WriteLine("KIZZQ0");
                            sw.WriteLine("KI9+0.0");
                            sw.WriteLine("ZT");
                            sw.WriteLine("Q120,25");
                            sw.WriteLine("Arglabel 200 31");
                            sw.WriteLine("exit");
                            sw.WriteLine("KI80");
                            sw.WriteLine("N");
                            //sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                            sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                            sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                            sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                            sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                            sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                            sw.WriteLine("P1");
                            sw.WriteLine("");
                        }
                        sw.Close();
                    }
                */

                //USE IN MULTIPLE BARCODE AT ONE TIME IN TSC
                /* 
                using (var sw = new StreamWriter(fileLoc, true))
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        pIntPktNo = Val.ToInt32(DRow["PACKETNO"]);
                        pStrTag = Val.ToString(DRow["TAG"]);
                        Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                        Carat = Val.ToString(DRow["LOTCARAT"]);
                        MarkerCode = Val.ToString(DRow["EMPLOYEECODE"]);


                        //string StrBarcode = "!104" + StrKapanName + Environment.NewLine + pIntPktNo + Environment.NewLine + pStrTag;
                        string StrBarcode = "!105" + Val.ToString(DRow["BARCODE"]);
                        string StrPrint = StrKapanName + "-" + pIntPktNo + "-" + pStrTag;



                        sw.WriteLine("<xpml><page quantity='0' pitch='15.5 mm'></xpml>SIZE 66.5 mm, 15.5 mm");
                        sw.WriteLine("GAP 2.5 mm, 0 mm");
                        sw.WriteLine("DIRECTION 0,0");
                        sw.WriteLine("REFERENCE 0,0");
                        sw.WriteLine("OFFSET 0 mm");
                        sw.WriteLine("SET PEEL OFF");
                        sw.WriteLine("SET CUTTER OFF");
                        sw.WriteLine("SET PARTIAL_CUTTER OFF");
                        sw.WriteLine("<xpml></page></xpml><xpml><page quantity='1' pitch='15.5 mm'></xpml>SET TEAR ");
                        sw.WriteLine("ON");
                        sw.WriteLine("CLS");
                        sw.WriteLine("CODEPAGE 1252");
                        sw.WriteLine("TEXT 513,103,\"2\",180,1,1,\"" + StrPrint + "\"");
                        //sw.WriteLine("TEXT 186,103,\"2\",180,1,1,\"" + Date + "\""); //2022
                        sw.WriteLine("TEXT 139,103,\"2\",180,1,1,\"" + Date + "\"");   //22
                        //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,3,6,\"!104" + StrBarcode + "\"");
                        //sw.WriteLine("BARCODE 514,82,\"128M\",49,0,180,2,4,\"" + StrBarcode + "\"");
                        sw.WriteLine("BARCODE 508,85,\"128M\",49,0,180,3,6,\"" + StrBarcode + "\"");
                        sw.WriteLine("TEXT 462,28,\"2\",180,1,1,\"" + MarkerCode + "\"");
                        sw.WriteLine("TEXT 125,28,\"2\",180,1,1,\"" + Carat + "\"");
                        sw.WriteLine("PRINT 1,1");
                        sw.WriteLine("<xpml></page></xpml><xpml><end/></xpml>");
                    }
                    sw.Close();
                }*/
                //USE IN MULTIPLE BARCODE AT ONE TIME IN CITIZEN
                /*using (var sw = new StreamWriter(fileLoc, true))
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                        pIntPktNo = Val.ToInt32(DRow["PACKETNO"]);
                        pStrTag = Val.ToString(DRow["TAG"]);
                        Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                        Carat = Val.ToString(DRow["LOTCARAT"]);
                        MarkerCode = Val.ToString(DRow["EMPLOYEECODE"]);

                        //string StrBarcode = "!104" + StrKapanName + Environment.NewLine + pIntPktNo + Environment.NewLine + pStrTag;
                        string StrBarcode = Val.ToString(DRow["BARCODE"]);
                        string StrPrint = StrKapanName + "-" + pIntPktNo + "-" + pStrTag;

                        sw.WriteLine("G0");
                        sw.WriteLine("n");
                        sw.WriteLine("M0500");
                        sw.WriteLine("O0214");
                        sw.WriteLine("V0");
                        sw.WriteLine("t1");
                        sw.WriteLine("Kf0070");
                        sw.WriteLine("L");
                        sw.WriteLine("D11");
                        sw.WriteLine("A2");
                        sw.WriteLine("1e6302400220007C" + StrBarcode + "");
                        sw.WriteLine("ySPM");
                        sw.WriteLine("1911A0600480003" + StrPrint + "");
                        sw.WriteLine("1911A0600090003" + MarkerCode + "");
                        //sw.WriteLine("1911A0600490197" + Date + ""); //2022
                        sw.WriteLine("1911A0600490204" + Date + "");  //22
                        sw.WriteLine("1911A0600100208" + Carat + "");
                        sw.WriteLine("Q0001");
                        sw.WriteLine("E");
                    }
                    sw.Close();
                }*/
                /*
                 //sw.Dispose();
                //sw = null;
                if (File.Exists(Application.StartupPath + "\\" + StrBatchFileName + ".BAT") && File.Exists(fileLoc))
                {
                    string StrPath = Application.StartupPath;
                    //Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                    Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\" + StrBatchFileName + ".BAT " + fileLoc, AppWinStyle.Hide, true, -1);
                }
                 */
                #endregion

                if (RbtTSC.Checked == true)
                {
                    //foreach (DataRow DRow in DTab.Rows)
                    //{
                    //    Global.BarcodePrintTSC(Val.ToString(DRow["KAPANNAME"]),
                    //        Val.ToString(DRow["PACKETNO"]),
                    //        Val.ToString(DRow["TAG"]),
                    //        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    //        Val.ToString(DRow["LOTCARAT"]),
                    //        Val.ToString(DRow["MARKERCODE"]),
                    //        Val.ToString(DRow["BARCODE"])
                    //        );
                    //}
                    Global.BarcodePrintTSC(DTab, "CURREMP");
                }
                else if (RbtCitizen.Checked == true)
                {
                    //foreach (DataRow DRow in DTab.Rows) //ek-ek barcode print thase
                    //{
                    //    Global.BarcodePrintCitizen(Val.ToString(DRow["KAPANNAME"]),
                    //        Val.ToString(DRow["PACKETNO"]),
                    //        Val.ToString(DRow["TAG"]),
                    //        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    //        Val.ToString(DRow["LOTCARAT"]),
                    //        Val.ToString(DRow["EMPLOYEECODE"]),
                    //        Val.ToString(DRow["BARCODE"]),
                    //        Val.ToString(DRow["PKTSERIALNO"]),
                    //        Val.ToString(DRow["PARENTTAG"])
                    //        );
                    //}
                    Global.BarcodePrintCitizen(DTab, "CURREMP", pStrOpe); //Ek sathe Multiple Barcode Print thase
                }
                else if (RbtTscGalaxy.Checked == true)
                {
                    //foreach (DataRow DRow in DTab.Rows)
                    //{
                    //    Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                    //        Val.ToString(DRow["PACKETNO"]),
                    //        Val.ToString(DRow["TAG"]),
                    //        Val.ToString(DRow["LOTCARAT"]),
                    //        Val.ToString(DRow["KAPANMANAGERCODE"]),
                    //        Val.ToString(DRow["PACKETGROUPCODE"]),
                    //        Val.ToString(DRow["COLORSHADECODE"]),
                    //        Val.ToString(DRow["BARCODE"]));
                    //}
                    Global.BarcodePrintTSCGalaxy(DTab, "CURREMP");
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }

        }

        private void BtnPrintMarker_Click(object sender, EventArgs e)
        {
            try
            {
                pStrOpe = "BtnPrintMarker";
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }
                if (RbtTSC.Checked == true)
                {
                    foreach (DataRow DRow in DTab.Rows)
                    {
                        string StrGroup = Val.ToString(DRow["PACKETGRADENAME"]) + "/" + Val.ToString(DRow["PACKETGROUPNAME"]);
                        string StrParentTag = Val.ToString(DRow["PARENTTAG"]).Trim() == "" ? "" : "(" + Val.ToString(DRow["PARENTTAG"]) + ")";

                        Global.BarcodePrintTSC(Val.ToString(DRow["KAPANNAME"]),
                            Val.ToString(DRow["PACKETNO"]),
                            Val.ToString(DRow["TAG"]),
                            Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                            Val.ToString(DRow["LOTCARAT"]),
                            Val.ToString(DRow["MARKERCODE"]),
                            Val.ToString(DRow["BARCODE"]),
                            StrGroup,
                            StrParentTag
                           );
                    }
                }
                else if (RbtCitizen.Checked == true)
                {
                    //foreach (DataRow DRow in DTab.Rows)
                    //{
                    //    Global.BarcodePrintCitizen(Val.ToString(DRow["KAPANNAME"]),
                    //        Val.ToString(DRow["PACKETNO"]),
                    //        Val.ToString(DRow["TAG"]),
                    //        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                    //        Val.ToString(DRow["LOTCARAT"]),
                    //        Val.ToString(DRow["EMPLOYEECODE"]),
                    //        Val.ToString(DRow["BARCODE"]),
                    //        Val.ToString(DRow["PKTSERIALNO"]),
                    //        Val.ToString(DRow["PARENTTAG"])
                    //        );
                    //}
                    Global.BarcodePrintCitizen(DTab, "CURREMP", pStrOpe);
                }
                else if (RbtTscGalaxy.Checked == true)
                {
                    //foreach (DataRow DRow in DTab.Rows)
                    //{
                    //    Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                    //        Val.ToString(DRow["PACKETNO"]),
                    //        Val.ToString(DRow["TAG"]),
                    //        Val.ToString(DRow["LOTCARAT"]),
                    //        Val.ToString(DRow["KAPANMANAGERCODE"]),
                    //        Val.ToString(DRow["PACKETGROUPCODE"]),
                    //        Val.ToString(DRow["COLORSHADECODE"]),
                    //        Val.ToString(DRow["BARCODE"]));
                    //}
                    Global.BarcodePrintTSCGalaxy(DTab, "CURREMP");
                }
            }
            catch (Exception Ex)
            {
                Global.Message(Ex.Message);
            }

        }

        private void BtnPrintWorker_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }

            if (RbtTSC.Checked == true)
            {
                foreach (DataRow DRow in DTab.Rows)
                {

                    string StrGroup = Val.ToString(DRow["PACKETGRADENAME"]) + "/" + Val.ToString(DRow["PACKETGROUPNAME"]);
                    string StrParentTag = Val.ToString(DRow["PARENTTAG"]).Trim() == "" ? "" : "(" + Val.ToString(DRow["PARENTTAG"]) + ")";

                    Global.BarcodePrintTSC(Val.ToString(DRow["KAPANNAME"]),
                        Val.ToString(DRow["PACKETNO"]),
                        Val.ToString(DRow["TAG"]),
                        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                        Val.ToString(DRow["LOTCARAT"]),
                        Val.ToString(DRow["MARKERCODE"]),
                        Val.ToString(DRow["BARCODE"]),
                        StrGroup,
                        StrParentTag
                       );
                }
            }
            else if (RbtCitizen.Checked == true)
            {
                //foreach (DataRow DRow in DTab.Rows)
                //{
                //    Global.BarcodePrintCitizen(Val.ToString(DRow["KAPANNAME"]),
                //            Val.ToString(DRow["PACKETNO"]),
                //            Val.ToString(DRow["TAG"]),
                //            Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                //            Val.ToString(DRow["LOTCARAT"]),
                //            Val.ToString(DRow["EMPLOYEECODE"]),
                //            Val.ToString(DRow["BARCODE"]),
                //            Val.ToString(DRow["PKTSERIALNO"]),
                //            Val.ToString(DRow["PARENTTAG"])
                //            );
                //}
                Global.BarcodePrintCitizen(DTab,"CURREMP", pStrOpe);
            }
            else if (RbtTscGalaxy.Checked == true)
            {
                foreach (DataRow DRow in DTab.Rows)
                {
                    Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                        Val.ToString(DRow["PACKETNO"]),
                        Val.ToString(DRow["TAG"]),
                        Val.ToString(DRow["LOTCARAT"]),
                        Val.ToString(DRow["KAPANMANAGERCODE"]),
                        Val.ToString(DRow["PACKETGROUPCODE"]),
                        Val.ToString(DRow["COLORSHADECODE"]),
                        Val.ToString(DRow["BARCODE"]));
                }
            }
        }

        private void toolTipController1_GetActiveObjectInfo(object sender, DevExpress.Utils.ToolTipControllerGetActiveObjectInfoEventArgs e)
        {
            if (e.SelectedControl != MainGrid) return;
            ToolTipControlInfo info = null;
            try
            {
                GridView view = MainGrid.GetViewAt(e.ControlMousePosition) as GridView;
                if (view == null) return;
                GridHitInfo hi = view.CalcHitInfo(e.ControlMousePosition);
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "EMPLOYEECODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "EMPLOYEENAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "MARKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "MARKERNAME")));
                    return;
                }
                if (hi.HitTest == GridHitTest.RowCell && hi.Column.FieldName == "WORKERCODE")
                {
                    info = new ToolTipControlInfo(hi.RowHandle.ToString() + hi.Column.FieldName, Val.ToString(view.GetRowCellValue(hi.RowHandle, "WORKERNAME")));
                    return;
                }
            }
            finally
            {
                e.Info = info;
            }
        }

        private void BtnByGrdPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                foreach (DataRow DRow in DTab.Rows)
                {

                    if (Val.Val(DRow["BYGRDCARAT"]) > 0)
                    {
                        Global.BarcodeBombayGrdPrint(Val.ToString(DRow["KAPANNAME"]),
                            Val.ToString(DRow["PACKETNO"]),
                            Val.ToString(DRow["TAG"]),
                            Val.ToString(DateTime.Parse(DRow["BYGRDDATE"].ToString()).ToString("dd-MM-yy")),
                            Val.ToString(DRow["BYGRDCARAT"]),
                            "", // Mark Code
                            Val.ToString(DRow["BYSHAPENAME"]),
                            Val.ToString(DRow["BYCOLORNAME"]),
                            Val.ToString(DRow["BYCLARITYNAME"]),
                            Val.ToString(DRow["BYCUTNAME"]),
                            Val.ToString(DRow["BYPOLNAME"]),
                            Val.ToString(DRow["BYSYMNAME"]),
                            Val.ToString(DRow["BYFLNAME"]),
                            Val.ToString(DRow["DIAMIN"]),
                            Val.ToString(DRow["DIAMAX"]),
                            Val.ToString(DRow["HEIGHT"]),
                            "",
                            Val.ToString(DRow["HELIUMRATIO"]),
                            Val.ToString(DRow["HELIUMTOTALDEPTH"]),
                            Val.ToString(DRow["HELIUMTABLEPC"]));
                    }
                }


            }
            catch (Exception ex)
            {
                Global.Message(ex.Message.ToString());
            }
        }

        private void btnBarcodetest_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            string fileLoc = Application.StartupPath + "\\PrintBarcodeData.txt";
            if (System.IO.File.Exists(fileLoc) == true)
            {
                System.IO.File.Delete(fileLoc);
            }

            System.IO.File.Create(fileLoc).Dispose();

            string StrKapanName = "";
            int pIntPktNo = 0;
            string pStrTag = "";
            string Date = "";
            string Carat = "";
            string MarkerCode = "";
            //StreamWriter sw = new StreamWriter(fileLoc);

            using (var sw = new StreamWriter(fileLoc, true))
            {
                foreach (DataRow DRow in DTab.Rows)
                {
                    StrKapanName = Val.ToString(DRow["KAPANNAME"]);
                    pIntPktNo = Val.ToInt32(DRow["PACKETNO"]);
                    pStrTag = Val.ToString(DRow["TAG"]);
                    Date = Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy"));
                    Carat = Val.ToString(DRow["LOTCARAT"]);
                    MarkerCode = Val.ToString(DRow["EMPLOYEECODE"]);

                    string StrBarcode = StrKapanName + Environment.NewLine + pIntPktNo + Environment.NewLine + Tag;
                    string StrPrint = StrKapanName + "-" + pIntPktNo + "-" + Tag;
                    string OM = "OM";
                    sw.WriteLine("I8,A");
                    sw.WriteLine("ZN");
                    sw.WriteLine("q400");
                    sw.WriteLine("O");
                    sw.WriteLine("JF");
                    sw.WriteLine("KIZZQ0");
                    sw.WriteLine("KI9+0.0");
                    sw.WriteLine("ZT");
                    sw.WriteLine("Q120,25");
                    sw.WriteLine("Arglabel 200 31");
                    sw.WriteLine("exit");
                    sw.WriteLine("KI80");
                    sw.WriteLine("N");
                    //sw.WriteLine("B351,87,2,1,2,4,56,N,\"" + StrBarcode + "\"");
                    sw.WriteLine("B325,95,2,1,1,2,51,N,\"" + StrBarcode + "\"");
                    sw.WriteLine("A325,120,2,3,1,1,N,\"" + StrPrint + "\"");
                    sw.WriteLine("A140,140,2,1,1,1,N,\"" + Date + "\"");
                    sw.WriteLine("A325,28,2,3,1,1,N,\"" + MarkerCode + "\"");
                    sw.WriteLine("A240,28,2,3,1,1,N,\"" + Carat + "\"");
                    sw.WriteLine("P1");
                    sw.WriteLine("");
                }
                sw.Close();
            }
            //sw.Dispose();
            //sw = null;


            if (File.Exists(Application.StartupPath + "\\PRINTBARCODE.BAT") && File.Exists(fileLoc))
            {
                Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + "\\PRINTBARCODE.BAT " + fileLoc, AppWinStyle.Hide, true, -1);
            }

        }

        private void RbtTSC_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtCitizen.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtCitizen.Text + "] Printer";
            }
            else if (RbtTscGalaxy.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTscGalaxy.Text + "] Printer";
            }
        }

        private void RbtCitizen_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtTSC.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTSC.Text + "] Printer";
            }
            else if (RbtTscGalaxy.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTscGalaxy.Text + "] Printer";
            }
        }

        private void RbtTscGalaxy_CheckedChanged(object sender, EventArgs e)
        {
            if (RbtTSC.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtTSC.Text + "] Printer";
            }
            else if (RbtCitizen.Checked == true)
            {
                lblBPrintMessage.Text = "Note : Selected Packet Barcode Is Printed In : [" + RbtCitizen.Text + "] Printer";
            }
        }

        private void BtnMakablePrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

                if (DTab.Rows.Count == 0)
                {
                    Global.Message("Please Select at lease One Row For Barcode Print");
                    return;
                }

                if (RbtCitizen.Checked == true)
                {
                    string StrBatchFileName = "";
                    string StrBarcodeTxtFileName = "";

                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_Citizen.txt";
                    StrBatchFileName = "\\PRINTBARCODE_Citizen.BAT ";

                    string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }
                    System.IO.File.Create(fileLoc).Dispose();

                    StreamWriter sw = new StreamWriter(fileLoc);

                    using (sw)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {

                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();
                            string StrDate = DateTime.Now.ToString("dd-MM");
                            Global.BarcodeProntMkblCitizen(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);

                        }
                        sw.Close();
                    }

                    sw.Dispose();
                    sw = null;
                    if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                    }

                    System.Threading.Thread.Sleep(800);

                }
                else if (RbtTSC.Checked == true)
                {
                    string StrBatchFileName = "";
                    string StrBarcodeTxtFileName = "";

                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_TSC.txt";
                    StrBatchFileName = "\\PRINTBARCODE_TSC.BAT ";

                    string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }
                    System.IO.File.Create(fileLoc).Dispose();

                    StreamWriter sw = new StreamWriter(fileLoc);

                    using (sw)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {

                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();
                            Global.BarcodeProntMkblTSC(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);

                        }
                        sw.Close();
                    }

                    sw.Dispose();
                    sw = null;
                    if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                    }

                    System.Threading.Thread.Sleep(800);

                }
            }
            catch (Exception ex)
            {
                Global.Message(ex.ToString());
            }
        }

        private void BPrintMainMngr_Click(object sender, EventArgs e)
        {
            DataTable DTab = GetTableOfSelectedRows(GrdDet, true);

            if (DTab.Rows.Count == 0)
            {
                Global.Message("Please Select at lease One Row For Barcode Print");
                return;
            }

            if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
            {
                return;
            }


            if (RbtTSC.Checked == true)
            {
                Global.BarcodePrintTSC(DTab, "MAINMNGR");
            }
            else if (RbtCitizen.Checked == true)
            {
                //foreach (DataRow DRow in DTab.Rows) //ek-ek barcode print thase
                //{
                //    Global.BarcodePrintCitizen(Val.ToString(DRow["KAPANNAME"]),
                //        Val.ToString(DRow["PACKETNO"]),
                //        Val.ToString(DRow["TAG"]),
                //        Val.ToString(DateTime.Parse(DRow["ENTRYDATE"].ToString()).ToString("dd-MM-yy")),
                //        Val.ToString(DRow["LOTCARAT"]),
                //        Val.ToString(DRow["EMPLOYEECODE"]),
                //        Val.ToString(DRow["BARCODE"]),
                //        Val.ToString(DRow["PKTSERIALNO"]),
                //        Val.ToString(DRow["PARENTTAG"])
                //        );
                //}
                Global.BarcodePrintCitizen(DTab, "MAINMNGR", pStrOpe);  //Ek sathe Multiple Barcode Print thase
            }
            else if (RbtTscGalaxy.Checked == true)
            {
                //foreach (DataRow DRow in DTab.Rows)
                //{
                //    Global.BarcodePrintTSCGalaxy(Val.ToString(DRow["KAPANNAME"]),
                //        Val.ToString(DRow["PACKETNO"]),
                //        Val.ToString(DRow["TAG"]),
                //        Val.ToString(DRow["LOTCARAT"]),
                //        Val.ToString(DRow["KAPANMANAGERCODE"]),
                //        Val.ToString(DRow["PACKETGROUPCODE"]),
                //        Val.ToString(DRow["COLORSHADECODE"]),
                //        Val.ToString(DRow["BARCODE"]));
                //}
                Global.BarcodePrintTSCGalaxy(DTab, "MAINMNGR");
            }
        }

        private void BtnBprintMkblNew_Click(object sender, EventArgs e)
        {
            try//Gunjan:30/06/2023
            {

                DataTable DTab = Global.GetSelectedRecordOfGrid(GrdDet, true, ObjGridSelection);

                if (DTab.Rows.Count == 0 || DTab == null)
                {
                    Global.Message("Please Select At Least One Record For Barcode Print.. ");
                    return;
                }

                if (Global.Confirm("Are you Sure You Want For Print Barcode of All Selected Packets?") == System.Windows.Forms.DialogResult.No)
                {
                    return;
                }

                if (RbtCitizen.Checked == true)
                {
                    string StrBatchFileName = "";
                    string StrBarcodeTxtFileName = "";

                    StrBarcodeTxtFileName = "\\PrintBarcodeDataMkbl_Citizen.txt";
                    StrBatchFileName = "\\PRINTBARCODE_Citizen.BAT ";

                    string fileLoc = Application.StartupPath + StrBarcodeTxtFileName;
                    if (System.IO.File.Exists(fileLoc) == true)
                    {
                        System.IO.File.Delete(fileLoc);
                    }
                    System.IO.File.Create(fileLoc).Dispose();

                    StreamWriter sw = new StreamWriter(fileLoc);

                    using (sw)
                    {
                        foreach (DataRow DRow in DTab.Rows)
                        {

                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();
                            string StrDate = DateTime.Now.ToString("dd-MM");
                            Global.BarcodeProntMkblCitizen(sw, StrBarcode, StrKapanNames, StrEmployeeCode, StrPktNoTag, StrPktSrNo.ToString(), StrParameterAmt, StrShpBlnCts);

                        }
                        sw.Close();
                    }

                    sw.Dispose();
                    sw = null;
                    if (File.Exists(Application.StartupPath + StrBatchFileName) && File.Exists(fileLoc))
                    {
                        Microsoft.VisualBasic.Interaction.Shell(Application.StartupPath + StrBatchFileName + fileLoc, AppWinStyle.Hide, true, -1);
                    }

                    System.Threading.Thread.Sleep(800);

                }
                else if (RbtTSC.Checked == true)
                {
                    

                    string StrBatchFileName = "";string DefaultPrinter = "";
                    StrBatchFileName = Application.StartupPath + "\\TSC_MakableBarcodeNew.txt ";

                    string[] lines = File.ReadAllLines(StrBatchFileName);
                    DefaultPrinter = GetDefaultPrinter();


                    this.Cursor = Cursors.WaitCursor;
                        List<StiReport> rps = new List<StiReport>();

                        int IntCount = 0;
                        foreach (DataRow DRow in DTab.Rows)
                        {
                            string StrBarcode = Val.ToString(DRow["BARCODE"]);
                            string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                            string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                            string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                            int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                            string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                            string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();

                            IntCount++;

                            StiReport report = new StiReport();
                            string BarcodeName = "TSC_MakableBarcode";
                            report.Load(Application.StartupPath + "\\Barcode\\" + BarcodeName + ".mrt");
                            report.Compile();
                            report.RequestParameters = false;

                            foreach (Stimulsoft.Report.Dictionary.StiSqlDatabase item in report.CompiledReport.Dictionary.Databases)
                            {
                                item.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;
                            }
                            report["BARCODE"] = "'" + StrBarcode + "'";
                            report["KAPANNAME"] = "'" + StrKapanNames + "'";
                            report["MKBLEMPLOYEECODE"] = "'" + StrEmployeeCode + "'";
                            report["PACKETNOTAG"] = "'" + StrPktNoTag + "'";
                            report["PKTSERIALNO"] = StrPktSrNo;
                            report["PARAMETERAMT"] = "'" + StrParameterAmt + "'";
                            report["SHPBLNCTS"] = "'" + StrShpBlnCts + "'";

                            StiSqlDatabase sql = new StiSqlDatabase("Connection", BusLib.Configuration.BOConfiguration.ConnectionString);
                            sql.Alias = "Connection";
                            report.CompiledReport.Dictionary.Databases.Clear();
                            report.CompiledReport.Dictionary.Databases.Add(sql);

                            report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
                            report.Render(false);

                            rps.Add(report);
                        }

                        StiReport singleFile = new StiReport();
                        singleFile.NeedsCompiling = false;
                        singleFile.IsRendered = true;

                        Stimulsoft.Report.Units.StiUnit newUnit = Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(singleFile.ReportUnit);
                        singleFile.RenderedPages.Clear();
                        foreach (StiReport rpt in rps)
                        {
                            foreach (Stimulsoft.Report.Components.StiPage page in rpt.CompiledReport.RenderedPages)
                            {
                                page.Report = singleFile;
                                page.NewGuid();
                                Stimulsoft.Report.Units.StiUnit oldUnit = Stimulsoft.Report.Units.StiUnit.GetUnitFromReportUnit(rpt.ReportUnit);
                                if (singleFile.ReportUnit != rpt.ReportUnit)
                                {
                                    page.Convert(oldUnit, newUnit);
                                }
                                singleFile.RenderedPages.Add(page);
                            }
                        }

                        SetDefaultPrinter(lines[0]);
                        //SetDefaultPrinter(@"\\192.168.0.14\TSC");
                        singleFile.Print(false);
                        SetDefaultPrinter(DefaultPrinter);
                        rps.Clear();
                        rps = null;
                       
                }

               
                //foreach (DataRow DRow in DTab.Rows)7
                //{
                //    string StrBarcode = Val.ToString(DRow["BARCODE"]);
                //    string StrKapanNames = Val.ToString(DRow["KAPANNAME"]);
                //    string StrEmployeeCode = Val.ToString(DRow["MKBLEMPLOYEECODE"]);
                //    string StrPktNoTag = Val.ToString(DRow["PACKETNO"]) + "" + Val.ToString(DRow["TAG"]);
                //    int StrPktSrNo = Val.ToInt(DRow["PKTSERIALNO"]);
                //    string StrParameterAmt = DRow["MKBLCOLORNAME"].ToString() + "-" + DRow["MKBLCLARITYNAME"].ToString() + "-" + DRow["MKBLCUTNAME"].ToString() + "-" + DRow["MKBLFLNAME"].ToString() + "-" + DRow["MKBLPRDAMOUNT"].ToString();
                //    string StrShpBlnCts = DRow["MKBLSHAPENAME"].ToString() + "-" + DRow["MKBLPRDCARAT"].ToString() + "-" + DRow["MKBLBALANCECARAT"].ToString();

                //    Stimulsoft.Report.StiReport report = new Stimulsoft.Report.StiReport();
                //    string BarcodeName = "TSC_MakableBarcode";
                //    report.Load(Application.StartupPath + "\\Barcode\\" + BarcodeName + ".mrt");
                //    report.Compile();
                //    report.RequestParameters = false;
                //    foreach (StiSqlDatabase item in report.CompiledReport.Dictionary.Databases)
                //    {
                //        item.ConnectionString = BusLib.Configuration.BOConfiguration.ConnectionString;
                //    }

                //    report["BARCODE"] = "'" + StrBarcode + "'";
                //    report["KAPANNAME"] = "'" + StrKapanNames + "'";
                //    report["MKBLEMPLOYEECODE"] = "'" + StrEmployeeCode + "'";
                //    report["PACKETNOTAG"] = "'" + StrPktNoTag + "'";
                //    report["PKTSERIALNO"] = StrPktSrNo;
                //    report["PARAMETERAMT"] = "'" + StrParameterAmt + "'";
                //    report["SHPBLNCTS"] = "'" + StrShpBlnCts + "'";

                //    StiSqlDatabase sql = new StiSqlDatabase("Connection", BusLib.Configuration.BOConfiguration.ConnectionString);
                //    sql.Alias = "Connection";
                //    report.CompiledReport.Dictionary.Databases.Clear();
                //    report.CompiledReport.Dictionary.Databases.Add(sql);
                //    report.PreviewMode = StiPreviewMode.StandardAndDotMatrix;
                //    report.Render(false);

                //    //SetDefaultPrinter(@"\\192.168.0.14\TSC");
                //    SetDefaultPrinter(lines[0]);
                //    //report.PrinterSettings.PrinterName = "TSC"; //Uncomment When gives update to Client                   
                //    //report.Print(false);
                //    //report.PrinterSettings.PrinterName = "Microsoft Print to PDF"; //Comment When gives update to Client
                //    report.Print(false);
                //}

                if (ObjGridSelection != null)
                {
                    ObjGridSelection.ClearSelection();
                }
            }
            catch (Exception ex)
            {
                Global.MessageError(ex.Message);
            }
            finally
            {
                this.Cursor = Cursors.Default;
            }//End as Gunjan
        }





    }
}
