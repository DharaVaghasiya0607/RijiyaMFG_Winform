using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AxoneMFGRJ.Report
{
    public partial class FrmReportViewerOld : Form
    {

        public FrmReportViewerOld()
        {
            InitializeComponent();
        }

        private void FrmReportViewer_Load(object sender, EventArgs e)
        {
            
        }

        public void ShowForm(string pStrReport, DataTable pDTab)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(pDTab);

                crystalReportViewer1.ReportSource = RepDoc;

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                this.Show();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
            
        }


        public void ShowFormDirectPrint(string pStrReport, DataTable pDTab)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(pDTab);

                crystalReportViewer1.ReportSource = RepDoc;

                //if (RepDoc.Subreports.Count == 1)
                //{
                //    //MessageBox.Show("Message Display");
                //    //MessageBox.Show("User : " + Glb.gStrDBUser);
                //    //MessageBox.Show("Pass : " + Glb.gStrDBPass);
                //    //MessageBox.Show("Ser : " + Glb.gStrServerName);
                //    //MessageBox.Show("DB : " + Glb.gStrDBName);
                //    RepDoc.SetDatabaseLogon(Glb.gStrDBUser, Glb.gStrDBPass, Glb.gStrServerName, Glb.gStrDBName);
                //}

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                PrinterSettings getprinterName = new PrinterSettings();
                RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
                RepDoc.PrintToPrinter(getprinterName.Copies, false, getprinterName.FromPage, getprinterName.ToPage);
                RepDoc.Close();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
            
        }

        public void ShowWithPrint(string pStrReport, DataTable pDTab)
        {
            try
            {
                string Str = AppDomain.CurrentDomain.BaseDirectory + "\\RPT\\" + pStrReport + ".rpt";
                //RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                Str = AppDomain.CurrentDomain.BaseDirectory + "\\RPT\\" + pStrReport + ".rpt";
                RepDoc.Load(@Str);
                RepDoc.SetDataSource(pDTab);

                crystalReportViewer1.ReportSource = RepDoc;

                //if (RepDoc.Subreports.Count == 1)
                //{
                //    //MessageBox.Show("Message Display");
                //    //MessageBox.Show("User : " + Glb.gStrDBUser);
                //    //MessageBox.Show("Pass : " + Glb.gStrDBPass);
                //    //MessageBox.Show("Ser : " + Glb.gStrServerName);
                //    //MessageBox.Show("DB : " + Glb.gStrDBName);
                //    RepDoc.SetDatabaseLogon(Glb.gStrDBUser, Glb.gStrDBPass, Glb.gStrServerName, Glb.gStrDBName);
                //}

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                this.Show();

                PrinterSettings getprinterName = new PrinterSettings();
                RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
                RepDoc.PrintToPrinter(getprinterName.Copies, false, getprinterName.FromPage, getprinterName.ToPage);

                RepDoc.Close();

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }
            
        }


        public void ExportPDF(string pStrReport, DataTable pDTab,string pStrExportFilePath)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(pDTab);

                crystalReportViewer1.ReportSource = RepDoc;

                //if (RepDoc.Subreports.Count == 1)
                //{
                //    //MessageBox.Show("Message Display");
                //    //MessageBox.Show("User : " + Glb.gStrDBUser);
                //    //MessageBox.Show("Pass : " + Glb.gStrDBPass);
                //    //MessageBox.Show("Ser : " + Glb.gStrServerName);
                //    //MessageBox.Show("DB : " + Glb.gStrDBName);
                //    RepDoc.SetDatabaseLogon(Glb.gStrDBUser, Glb.gStrDBPass, Glb.gStrServerName, Glb.gStrDBName);
                //}
                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                RepDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pStrExportFilePath);
            
            }
            catch (Exception ex)
            {

            }
            
        }


        public void AddNewRow(string ReportHeaderName,DataTable DTab)
        {
            if (DTab.Rows.Count == 0)
            {
                DataRow DRow = DTab.NewRow();
                DRow["ReportHeaderName"] =ReportHeaderName;
                DTab.Rows.Add(DRow); 
            }            
        }

        
        public void ShowForm(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Fresh Packet Size Wise", DS.Tables[1]);
                AddNewRow("Fresh Packet FL Wise", DS.Tables[2]);

                AddNewRow("Rough Prediction Size Wise", DS.Tables[3]);
                AddNewRow("Rough Prediction Color Wise", DS.Tables[4]);
                AddNewRow("Rough Prediction Clarity Wise", DS.Tables[5]);

                AddNewRow("Final Prediction Size Wise", DS.Tables[6]);
                AddNewRow("Final Prediction Color Wise", DS.Tables[7]);
                AddNewRow("Final Prediction Clarity Wise", DS.Tables[8]);

                AddNewRow("Makable Prediction Size Wise", DS.Tables[9]);
                AddNewRow("Makable Prediction Color Wise", DS.Tables[10]);
                AddNewRow("Makable Prediction Clarity Wise", DS.Tables[11]);

                AddNewRow("Final Grading Size Wise", DS.Tables[12]);
                AddNewRow("Final Grading Color Wise", DS.Tables[13]);
                AddNewRow("Final Grading Clarity Wise", DS.Tables[14]);

                AddNewRow("Department Wise Loss", DS.Tables[15]);
                AddNewRow("Rejections", DS.Tables[16]);

                RepDoc.Subreports["RoughPacketSize"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["RoughPacketFL"].SetDataSource(DS.Tables[2]);

                RepDoc.Subreports["RoughPrdSize"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RoughPrdColor"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["RoughPrdClarity"].SetDataSource(DS.Tables[5]);

                RepDoc.Subreports["FinalPrdSize"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["FinalPrdColor"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["FinalPrdClarity"].SetDataSource(DS.Tables[8]);

                RepDoc.Subreports["MakPrdSize"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["MakPrdColor"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["MakPrdClarity"].SetDataSource(DS.Tables[11]);

                RepDoc.Subreports["GrdPrdSize"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["GrdPrdColor"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["GrdPrdClarity"].SetDataSource(DS.Tables[14]);

                RepDoc.Subreports["DepartmentLoss"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["Rejection"].SetDataSource(DS.Tables[16]);

                crystalReportViewer1.ReportSource = RepDoc;

                //if (RepDoc.Subreports.Count == 1)
                //{
                //    //MessageBox.Show("Message Display");
                //    //MessageBox.Show("User : " + Glb.gStrDBUser);
                //    //MessageBox.Show("Pass : " + Glb.gStrDBPass);
                //    //MessageBox.Show("Ser : " + Glb.gStrServerName);
                //    //MessageBox.Show("DB : " + Glb.gStrDBName);
                //    RepDoc.SetDatabaseLogon(Glb.gStrDBUser, Glb.gStrDBPass, Glb.gStrServerName, Glb.gStrDBName);
                //}
                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";
               
                this.Show();
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }


        public void ExportPDF(string pStrReport, DataSet DS, string pStrExportFilePath)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Fresh Packet Size Wise", DS.Tables[1]);
                AddNewRow("Fresh Packet FL Wise", DS.Tables[2]);

                AddNewRow("Rough Prediction Size Wise", DS.Tables[3]);
                AddNewRow("Rough Prediction Color Wise", DS.Tables[4]);
                AddNewRow("Rough Prediction Clarity Wise", DS.Tables[5]);

                AddNewRow("Final Prediction Size Wise", DS.Tables[6]);
                AddNewRow("Final Prediction Color Wise", DS.Tables[7]);
                AddNewRow("Final Prediction Clarity Wise", DS.Tables[8]);

                AddNewRow("Makable Prediction Size Wise", DS.Tables[9]);
                AddNewRow("Makable Prediction Color Wise", DS.Tables[10]);
                AddNewRow("Makable Prediction Clarity Wise", DS.Tables[11]);

                AddNewRow("Final Grading Size Wise", DS.Tables[12]);
                AddNewRow("Final Grading Color Wise", DS.Tables[13]);
                AddNewRow("Final Grading Clarity Wise", DS.Tables[14]);

                AddNewRow("Department Wise Loss", DS.Tables[15]);
                AddNewRow("Rejections", DS.Tables[16]);

                RepDoc.Subreports["RoughPacketSize"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["RoughPacketFL"].SetDataSource(DS.Tables[2]);

                RepDoc.Subreports["RoughPrdSize"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RoughPrdColor"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["RoughPrdClarity"].SetDataSource(DS.Tables[5]);

                RepDoc.Subreports["FinalPrdSize"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["FinalPrdColor"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["FinalPrdClarity"].SetDataSource(DS.Tables[8]);

                RepDoc.Subreports["MakPrdSize"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["MakPrdColor"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["MakPrdClarity"].SetDataSource(DS.Tables[11]);

                RepDoc.Subreports["GrdPrdSize"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["GrdPrdColor"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["GrdPrdClarity"].SetDataSource(DS.Tables[14]);

                RepDoc.Subreports["DepartmentLoss"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["Rejection"].SetDataSource(DS.Tables[16]);

                //rptdoc.SetDataSource(ds.Tables[0]);
                //rptdoc.Subreports[0].SetDataSource(ds2.Tables[0]);
                //rptdoc.Subreports[1].SetDataSource(ds3.Tables[0]);

                crystalReportViewer1.ReportSource = RepDoc;

                //if (RepDoc.Subreports.Count == 1)
                //{
                //    //MessageBox.Show("Message Display");
                //    //MessageBox.Show("User : " + Glb.gStrDBUser);
                //    //MessageBox.Show("Pass : " + Glb.gStrDBPass);
                //    //MessageBox.Show("Ser : " + Glb.gStrServerName);
                //    //MessageBox.Show("DB : " + Glb.gStrDBName);
                //    RepDoc.SetDatabaseLogon(Glb.gStrDBUser, Glb.gStrDBPass, Glb.gStrServerName, Glb.gStrDBName);
                //}
                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                RepDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pStrExportFilePath);                
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        private void BtnPrint_Click(object sender, EventArgs e)
        {
            PrinterSettings getprinterName = new PrinterSettings();
            RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
            RepDoc.PrintToPrinter(getprinterName.Copies, false, getprinterName.FromPage, getprinterName.ToPage);
            //crystalReportViewer1.PrintReport();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FrmReportViewer_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void crystalReportViewer1_DrillDownSubreport(object source, CrystalDecisions.Windows.Forms.DrillSubreportEventArgs e)
        {
            e.Handled = true;
        }
    }
}
