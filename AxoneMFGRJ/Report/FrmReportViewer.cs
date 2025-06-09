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
    public partial class FrmReportViewer : Form
    {
        ReportDocument RepDoc = new ReportDocument();
        public FrmReportViewer()
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
      
        public void ShowFormPrintExportInPDF(string pStrReport, DataTable pDTab, string pStrFileName) //Add : Pinali : 16-11-2019
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(pDTab);
                crystalReportViewer1.ReportSource = RepDoc;

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";


                RepDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pStrFileName);
               
                RepDoc.Close();
                RepDoc.Dispose();
                RepDoc = null;

            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
                if (RepDoc != null)
                {
                    RepDoc.Close();
                    RepDoc.Dispose();
                    RepDoc = null;
                }
            }
        }


        public void ShowFormDirectPrint(string pStrReport, DataTable pDTab)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(pDTab);

                crystalReportViewer1.ReportSource = RepDoc;

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                PrinterSettings getprinterName = new PrinterSettings();
                RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
                RepDoc.PrintToPrinter(getprinterName.Copies, false, getprinterName.FromPage, getprinterName.ToPage);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        public void ExportKapanDetailPDF(string pStrReport, DataSet DS, string pStrExportFilePath)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Packet Process Wise", DS.Tables[1]);
                AddNewRow("Packet Loss Wise", DS.Tables[2]);

                RepDoc.Subreports["PacketProcessWise"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["PacketLossWise"].SetDataSource(DS.Tables[2]);

                crystalReportViewer1.ReportSource = RepDoc;

                crystalReportViewer1.Zoom(120);
                crystalReportViewer1.Text = "100";

                RepDoc.ExportToDisk(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, pStrExportFilePath);
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        public void ShowFullKapanAnalysis(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);
                AddNewRow("Purity Wise Report", DS.Tables[1]);
                AddNewRow("Size Wise Report", DS.Tables[2]);
                AddNewRow("WFSG Wise Report", DS.Tables[3]);
                AddNewRow("Purity Wise Summary Report", DS.Tables[4]);
                AddNewRow("Size Wise Summary Report", DS.Tables[5]);
                RepDoc.Subreports["PurityWise"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["SizeWise"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["ShapeWise"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["Rpt_PurityWiseSummary"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["RPT_SizeWiseSummary"].SetDataSource(DS.Tables[5]);

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

        public void ShowKapanDetailForm(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Packet Process Wise-Single", DS.Tables[1]);
                AddNewRow("Packet Loss Wise-Single", DS.Tables[2]);
                AddNewRow("Packet Prd CaratWise-Single", DS.Tables[3]);
                AddNewRow("Packet Prd PcsWise-Single", DS.Tables[4]);

                AddNewRow("Packet Process Wise-Mix", DS.Tables[5]);
                AddNewRow("Packet Loss Wise-Mix", DS.Tables[6]);
                AddNewRow("Packet Prd CaratWise-Mix", DS.Tables[7]);
                AddNewRow("Packet Prd PcsWise-Mix", DS.Tables[8]);

                AddNewRow("Packet Process Wise-Mix", DS.Tables[9]);
                AddNewRow("Packet Loss Wise-Mix", DS.Tables[10]);
                AddNewRow("Packet Prd CaratWise-Mix", DS.Tables[11]);
                AddNewRow("Packet Prd PcsWise-Mix", DS.Tables[12]);

                AddNewRow("Prd Param CriteriaWise", DS.Tables[13]);

                RepDoc.Subreports["PacketProcessWise_Single"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["PacketLossWise_Single"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["PacketPrdCaratWise_Single"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["PacketPrdPcsWise_Single"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["PacketProcessWise_Mix"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["PacketLossWise_Mix"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["PacketPrdCaratWise_Mix"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["PacketPrdPcsWise_Mix"].SetDataSource(DS.Tables[8]);
                RepDoc.Subreports["PacketProcessWise_All"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["PacketLossWise_All"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["PacketPrdCaratWise_All"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["PacketPrdPcsWise_All"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["PrdParamCriteriaWise"].SetDataSource(DS.Tables[13]);

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


        public void ShowMkblShapeWiseSummary(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Mkbl Shape Summary-Single", DS.Tables[1]);
                AddNewRow("Mkbl Shape Summary-Mix", DS.Tables[2]);
                AddNewRow("Mkbl Shape Summary-All", DS.Tables[3]);
                RepDoc.Subreports["MkblShapeSummary_Single"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["MkblShapeSummary_Mix"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["MkblShapeSummary_All"].SetDataSource(DS.Tables[3]);

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


        public void ShowKapanFinalSummaryForm(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Kapan Summary Loss Report", DS.Tables[1]);
                RepDoc.Subreports["KapanFinalSummaryLossReport"].SetDataSource(DS.Tables[1]);
                
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

        public void ShowWithPrint(string pStrReport, DataTable pDTab)
        {
            try
            {
                //string Str = AppDomain.CurrentDomain.BaseDirectory + "\\RPT\\" + pStrReport + ".rpt";
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                //Str = AppDomain.CurrentDomain.BaseDirectory + "\\RPT\\" + pStrReport + ".rpt";
                //RepDoc.Load(@Str);
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

                /* //Comnt : #P : 26-10-2020 : As Per Discuss With Client....
                PrinterSettings getprinterName = new PrinterSettings();
                RepDoc.PrintOptions.PrinterName = getprinterName.PrinterName;
                RepDoc.PrintToPrinter(getprinterName.Copies, false, getprinterName.FromPage, getprinterName.ToPage);
                */
            }
            catch (Exception ex)
            {
                Global.Message(ex.Message);
            }

        }

        public void ExportPDF(string pStrReport, DataTable pDTab, string pStrExportFilePath)
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

        public void AddNewRow(string ReportHeaderName, DataTable DTab)
        {
            if (DTab.Rows.Count == 0)
            {
                DataRow DRow = DTab.NewRow();
                DRow["ReportHeaderName"] = ReportHeaderName;
                DTab.Rows.Add(DRow);
            }
        }

        public void ShowForm(string pStrReport, DataSet DS)
        {
            try
            {
                //Global.Message("Step : 4");
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("Fresh Packet Size Wise", DS.Tables[1]);
                AddNewRow("Fresh Packet FL Wise", DS.Tables[2]);

                AddNewRow("Rough Prediction Size Wise", DS.Tables[3]);
                AddNewRow("Rough Prediction Color Wise", DS.Tables[4]);
                AddNewRow("Rough Prediction Clarity Wise", DS.Tables[5]);
                AddNewRow("Rough Prediction Cut Wise", DS.Tables[6]);
                AddNewRow("Rough Prediction FL Wise", DS.Tables[7]);
                AddNewRow("Rough Prediction Shape Wise", DS.Tables[8]);

                AddNewRow("Final Prediction Size Wise", DS.Tables[9]);
                AddNewRow("Final Prediction Color Wise", DS.Tables[10]);
                AddNewRow("Final Prediction Clarity Wise", DS.Tables[11]);
                AddNewRow("Final Prediction Cut Wise", DS.Tables[12]);
                AddNewRow("Final Prediction FL Wise", DS.Tables[13]);
                AddNewRow("Final Prediction Shape Wise", DS.Tables[14]);

                ////#P : 25-01-2020
                AddNewRow("Checker Prediction Size Wise", DS.Tables[15]);
                AddNewRow("Checker Prediction Color Wise", DS.Tables[16]);
                AddNewRow("Checker Prediction Clarity Wise", DS.Tables[17]);
                AddNewRow("Checker Prediction Cut Wise", DS.Tables[18]);
                AddNewRow("Checker Prediction FL Wise", DS.Tables[19]);
                AddNewRow("Checker Prediction Shape Wise", DS.Tables[20]);

                AddNewRow("Ownership Prediction Size Wise", DS.Tables[21]);
                AddNewRow("Ownership Prediction Color Wise", DS.Tables[22]);
                AddNewRow("Ownership Prediction Clarity Wise", DS.Tables[23]);
                AddNewRow("Ownership Prediction Cut Wise", DS.Tables[24]);
                AddNewRow("Ownership Prediction FL Wise", DS.Tables[25]);
                AddNewRow("Ownership Prediction Shape Wise", DS.Tables[26]);

                ////End : #P : 25-01-2020

                AddNewRow("Makable Prediction Size Wise", DS.Tables[27]);
                AddNewRow("Makable Prediction Color Wise", DS.Tables[28]);
                AddNewRow("Makable Prediction Clarity Wise", DS.Tables[29]);
                AddNewRow("Makable Prediction Cut Wise", DS.Tables[30]);
                AddNewRow("Makable Prediction FL Wise", DS.Tables[31]);
                AddNewRow("Makable Prediction Shape Wise", DS.Tables[32]);

                AddNewRow("Final Grading Size Wise", DS.Tables[33]);
                AddNewRow("Final Grading Color Wise", DS.Tables[34]);
                AddNewRow("Final Grading Clarity Wise", DS.Tables[35]);
                AddNewRow("Final Grading Cut Wise", DS.Tables[36]);
                AddNewRow("Final Grading FL Wise", DS.Tables[37]);
                AddNewRow("Final Grading Shape Wise", DS.Tables[38]);

                AddNewRow("Department Wise Loss", DS.Tables[39]);
                AddNewRow("Rejections", DS.Tables[40]);

                RepDoc.Subreports["RoughPacketSize"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["RoughPacketFL"].SetDataSource(DS.Tables[2]);

                RepDoc.Subreports["RoughPrdSize"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RoughPrdColor"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["RoughPrdClarity"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["RoughPrdCut"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["RoughPrdFL"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["RoughPrdShape"].SetDataSource(DS.Tables[8]);

                RepDoc.Subreports["FinalPrdSize"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["FinalPrdColor"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["FinalPrdClarity"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["FinalPrdCut"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["FinalPrdFL"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["FinalPrdShape"].SetDataSource(DS.Tables[14]);

                //#P : 25-01-2020
                RepDoc.Subreports["CheckerPrdSize"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["CheckerPrdColor"].SetDataSource(DS.Tables[16]);
                RepDoc.Subreports["CheckerPrdClarity"].SetDataSource(DS.Tables[17]);
                RepDoc.Subreports["CheckerPrdCut"].SetDataSource(DS.Tables[18]);
                RepDoc.Subreports["CheckerPrdFL"].SetDataSource(DS.Tables[19]);
                RepDoc.Subreports["CheckerPrdShape"].SetDataSource(DS.Tables[20]);

                RepDoc.Subreports["OwnershipPrdSize"].SetDataSource(DS.Tables[21]);
                RepDoc.Subreports["OwnershipPrdColor"].SetDataSource(DS.Tables[22]);
                RepDoc.Subreports["OwnershipPrdClarity"].SetDataSource(DS.Tables[23]);
                RepDoc.Subreports["OwnershipPrdCut"].SetDataSource(DS.Tables[24]);
                RepDoc.Subreports["OwnershipPrdFL"].SetDataSource(DS.Tables[25]);
                RepDoc.Subreports["OwnershipPrdShape"].SetDataSource(DS.Tables[26]);

                //End : #P : 25-01-2020

                RepDoc.Subreports["MakPrdSize"].SetDataSource(DS.Tables[27]);
                RepDoc.Subreports["MakPrdColor"].SetDataSource(DS.Tables[28]);
                RepDoc.Subreports["MakPrdClarity"].SetDataSource(DS.Tables[29]);
                RepDoc.Subreports["MakPrdCut"].SetDataSource(DS.Tables[30]);
                RepDoc.Subreports["MakPrdFL"].SetDataSource(DS.Tables[31]);
                RepDoc.Subreports["MakPrdShape"].SetDataSource(DS.Tables[32]);

                RepDoc.Subreports["GrdPrdSize"].SetDataSource(DS.Tables[33]);
                RepDoc.Subreports["GrdPrdColor"].SetDataSource(DS.Tables[34]);
                RepDoc.Subreports["GrdPrdClarity"].SetDataSource(DS.Tables[35]);
                RepDoc.Subreports["GrdPrdCut"].SetDataSource(DS.Tables[36]);
                RepDoc.Subreports["GrdPrdFL"].SetDataSource(DS.Tables[37]);
                RepDoc.Subreports["GrdPrdShape"].SetDataSource(DS.Tables[38]);

                RepDoc.Subreports["DepartmentLoss"].SetDataSource(DS.Tables[39]);
                RepDoc.Subreports["Rejection"].SetDataSource(DS.Tables[40]);

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

        public void ShowFormForMemoAnalysis(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("PrdDetails", DS.Tables[1]);
                AddNewRow("LossDetail", DS.Tables[2]);
                AddNewRow("ExtraRejectionPcnDetail", DS.Tables[3]);
                AddNewRow("RMPSizeDetail", DS.Tables[4]);
                AddNewRow("BreakingDetail", DS.Tables[5]);
                AddNewRow("ByGrdSizeWiseDetail", DS.Tables[6]);
                AddNewRow("ByGrdShapeWiseDetail", DS.Tables[7]);
                AddNewRow("ByGrdGIADetail", DS.Tables[8]);
                AddNewRow("ByGrdMixDetail", DS.Tables[9]);
                AddNewRow("ByGrdShapeWiseChartDetail", DS.Tables[7]);
                AddNewRow("ByGrdFloWiseChartDetail", DS.Tables[10]);
                AddNewRow("ByGrdGIAMixWiseChartDetail", DS.Tables[11]);
                AddNewRow("ByGrdSizeWiseChartDetail", DS.Tables[6]);
                AddNewRow("ByGrdPointerWiseChartDetail", DS.Tables[12]);
                AddNewRow("ByGrdColorWiseChartDetail", DS.Tables[13]);
                AddNewRow("ByGrdClarityWiseChartDetail", DS.Tables[14]);
                AddNewRow("FreshPacketRoughSizeWiseDetail", DS.Tables[15]);
                AddNewRow("ByGrdCutWiseChartDetail", DS.Tables[16]);
                AddNewRow("KapanWiseMumbaiRateDetail", DS.Tables[17]);
                AddNewRow("KapanCompletePer", DS.Tables[18]);
                AddNewRow("LabCompletePer", DS.Tables[19]);
                AddNewRow("SaleCompletePer", DS.Tables[20]);

                RepDoc.Subreports["PrdWiseSummary"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["LossDetail"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["ExtraRejectionPcnDetail"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RMPSizeDetail"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["BreakingDetail"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["ByGrdSizeWiseDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdShapeWiseDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdGIADetail"].SetDataSource(DS.Tables[8]);
                RepDoc.Subreports["ByGrdMixDetail"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["ByGrdShapeWiseChartDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdFloWiseChartDetail"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["ByGrdGIAMixWiseChartDetail"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["ByGrdSizeWiseChartDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdPointerWiseChartDetail"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["ByGrdColorWiseChartDetail"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["ByGrdClarityWiseChartDetail"].SetDataSource(DS.Tables[14]);
                RepDoc.Subreports["FreshPacketRoughSizeWiseDetail"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["ByGrdCutWiseChartDetail"].SetDataSource(DS.Tables[16]);
                RepDoc.Subreports["KapanWiseMumbaiRateDetail"].SetDataSource(DS.Tables[17]);
                RepDoc.Subreports["KapanCompletePer"].SetDataSource(DS.Tables[18]);
                RepDoc.Subreports["LabCompletePer"].SetDataSource(DS.Tables[19]);
                RepDoc.Subreports["SaleCompletePer"].SetDataSource(DS.Tables[20]);

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

        public void ShowFormForMemoAnalysiReportOther(string pStrReport, DataSet DS)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("PrdDetails", DS.Tables[1]);
                AddNewRow("LossDetail", DS.Tables[2]);
                AddNewRow("ExtraRejectionPcnDetail", DS.Tables[3]);
                AddNewRow("RMPSizeDetail", DS.Tables[4]);
                AddNewRow("BreakingDetail", DS.Tables[5]);
                AddNewRow("ByGrdSizeWiseDetail", DS.Tables[6]);
                AddNewRow("ByGrdShapeWiseDetail", DS.Tables[7]);
                AddNewRow("ByGrdGIADetail", DS.Tables[8]);
                AddNewRow("ByGrdMixDetail", DS.Tables[9]);
                AddNewRow("ByGrdShapeWiseChartDetail", DS.Tables[7]);
                AddNewRow("ByGrdFloWiseChartDetail", DS.Tables[10]);
                AddNewRow("ByGrdGIAMixWiseChartDetail", DS.Tables[11]);
                AddNewRow("ByGrdSizeWiseChartDetail", DS.Tables[6]);
                AddNewRow("ByGrdPointerWiseChartDetail", DS.Tables[12]);
                AddNewRow("ByGrdColorWiseChartDetail", DS.Tables[13]);
                AddNewRow("ByGrdClarityWiseChartDetail", DS.Tables[14]);
                AddNewRow("FreshPacketRoughSizeWiseDetail", DS.Tables[15]);
                AddNewRow("ByGrdCutWiseChartDetail", DS.Tables[16]);
                AddNewRow("KapanCompletePer", DS.Tables[18]);

                RepDoc.Subreports["PrdWiseSummary"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["LossDetail"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["ExtraRejectionPcnDetail"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RMPSizeDetail"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["BreakingDetail"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["ByGrdSizeWiseDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdShapeWiseDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdGIADetail"].SetDataSource(DS.Tables[8]);
                RepDoc.Subreports["ByGrdMixDetail"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["ByGrdShapeWiseChartDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdFloWiseChartDetail"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["ByGrdGIAMixWiseChartDetail"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["ByGrdSizeWiseChartDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdPointerWiseChartDetail"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["ByGrdColorWiseChartDetail"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["ByGrdClarityWiseChartDetail"].SetDataSource(DS.Tables[14]);
                RepDoc.Subreports["FreshPacketRoughSizeWiseDetail"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["ByGrdCutWiseChartDetail"].SetDataSource(DS.Tables[16]);
                RepDoc.Subreports["KapanCompletePer"].SetDataSource(DS.Tables[18]);

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
                AddNewRow("Rough Prediction Cut Wise", DS.Tables[6]);
                AddNewRow("Rough Prediction FL Wise", DS.Tables[7]);

                AddNewRow("Final Prediction Size Wise", DS.Tables[8]);
                AddNewRow("Final Prediction Color Wise", DS.Tables[9]);
                AddNewRow("Final Prediction Clarity Wise", DS.Tables[10]);
                AddNewRow("Final Prediction Cut Wise", DS.Tables[11]);
                AddNewRow("Final Prediction FL Wise", DS.Tables[12]);

                //#P : 25-01-2020
                AddNewRow("Checker Prediction Size Wise", DS.Tables[13]);
                AddNewRow("Checker Prediction Color Wise", DS.Tables[14]);
                AddNewRow("Checker Prediction Clarity Wise", DS.Tables[15]);
                AddNewRow("Checker Prediction Cut Wise", DS.Tables[16]);
                AddNewRow("Checker Prediction FL Wise", DS.Tables[17]);

                AddNewRow("Ownership Prediction Size Wise", DS.Tables[18]);
                AddNewRow("Ownership Prediction Color Wise", DS.Tables[19]);
                AddNewRow("Ownership Prediction Clarity Wise", DS.Tables[20]);
                AddNewRow("Ownership Prediction Cut Wise", DS.Tables[21]);
                AddNewRow("Ownership Prediction FL Wise", DS.Tables[22]);
                //End : #P : 25-01-2020

                AddNewRow("Makable Prediction Size Wise", DS.Tables[23]);
                AddNewRow("Makable Prediction Color Wise", DS.Tables[24]);
                AddNewRow("Makable Prediction Clarity Wise", DS.Tables[25]);
                AddNewRow("Makable Prediction Cut Wise", DS.Tables[26]);
                AddNewRow("Makable Prediction FL Wise", DS.Tables[27]);

                AddNewRow("Final Grading Size Wise", DS.Tables[28]);
                AddNewRow("Final Grading Color Wise", DS.Tables[29]);
                AddNewRow("Final Grading Clarity Wise", DS.Tables[30]);
                AddNewRow("Final Grading Cut Wise", DS.Tables[31]);
                AddNewRow("Final Grading FL Wise", DS.Tables[32]);

                AddNewRow("Department Wise Loss", DS.Tables[33]);
                AddNewRow("Rejections", DS.Tables[34]);

                RepDoc.Subreports["RoughPacketSize"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["RoughPacketFL"].SetDataSource(DS.Tables[2]);

                RepDoc.Subreports["RoughPrdSize"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RoughPrdColor"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["RoughPrdClarity"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["RoughPrdCut"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["RoughPrdFL"].SetDataSource(DS.Tables[7]);

                RepDoc.Subreports["FinalPrdSize"].SetDataSource(DS.Tables[8]);
                RepDoc.Subreports["FinalPrdColor"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["FinalPrdClarity"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["FinalPrdCut"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["FinalPrdFL"].SetDataSource(DS.Tables[12]);

                //#P : 25-01-2020
                RepDoc.Subreports["CheckerPrdSize"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["CheckerPrdColor"].SetDataSource(DS.Tables[14]);
                RepDoc.Subreports["CheckerPrdClarity"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["CheckerPrdCut"].SetDataSource(DS.Tables[16]);
                RepDoc.Subreports["CheckerPrdFL"].SetDataSource(DS.Tables[17]);

                RepDoc.Subreports["OwnershipPrdSize"].SetDataSource(DS.Tables[18]);
                RepDoc.Subreports["OwnershipPrdColor"].SetDataSource(DS.Tables[19]);
                RepDoc.Subreports["OwnershipPrdClarity"].SetDataSource(DS.Tables[20]);
                RepDoc.Subreports["OwnershipPrdCut"].SetDataSource(DS.Tables[21]);
                RepDoc.Subreports["OwnershipPrdFL"].SetDataSource(DS.Tables[22]);
                //End : #P : 25-01-2020

                RepDoc.Subreports["MakPrdSize"].SetDataSource(DS.Tables[23]);
                RepDoc.Subreports["MakPrdColor"].SetDataSource(DS.Tables[24]);
                RepDoc.Subreports["MakPrdClarity"].SetDataSource(DS.Tables[25]);
                RepDoc.Subreports["MakPrdCut"].SetDataSource(DS.Tables[26]);
                RepDoc.Subreports["MakPrdFL"].SetDataSource(DS.Tables[27]);

                RepDoc.Subreports["GrdPrdSize"].SetDataSource(DS.Tables[28]);
                RepDoc.Subreports["GrdPrdColor"].SetDataSource(DS.Tables[29]);
                RepDoc.Subreports["GrdPrdClarity"].SetDataSource(DS.Tables[30]);
                RepDoc.Subreports["GrdPrdCut"].SetDataSource(DS.Tables[31]);
                RepDoc.Subreports["GrdPrdFL"].SetDataSource(DS.Tables[32]);

                RepDoc.Subreports["DepartmentLoss"].SetDataSource(DS.Tables[33]);
                RepDoc.Subreports["Rejection"].SetDataSource(DS.Tables[34]);

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

        public void ExportPDFForMemoAnalaysis(string pStrReport, DataSet DS, string pStrExportFilePath)
        {
            try
            {
                RepDoc.Load(Application.StartupPath + "\\RPT\\" + pStrReport + ".rpt");
                RepDoc.SetDataSource(DS.Tables[0]);

                AddNewRow("PrdDetails", DS.Tables[1]);
                AddNewRow("LossDetail", DS.Tables[2]);
                AddNewRow("ExtraRejectionPcnDetail", DS.Tables[3]);
                AddNewRow("RMPSizeDetail", DS.Tables[4]);
                AddNewRow("BreakingDetail", DS.Tables[5]);
                AddNewRow("ByGrdSizeWiseDetail", DS.Tables[6]);
                AddNewRow("ByGrdShapeWiseDetail", DS.Tables[7]);
                AddNewRow("ByGrdGIADetail", DS.Tables[8]);
                AddNewRow("ByGrdMixDetail", DS.Tables[9]);
                AddNewRow("ByGrdShapeWiseChartDetail", DS.Tables[7]);
                AddNewRow("ByGrdFloWiseChartDetail", DS.Tables[10]);
                AddNewRow("ByGrdGIAMixWiseChartDetail", DS.Tables[11]);
                AddNewRow("ByGrdSizeWiseChartDetail", DS.Tables[6]);
                AddNewRow("ByGrdPointerWiseChartDetail", DS.Tables[12]);
                AddNewRow("ByGrdColorWiseChartDetail", DS.Tables[13]);
                AddNewRow("ByGrdClarityWiseChartDetail", DS.Tables[14]);
                AddNewRow("FreshPacketRoughSizeWiseDetail", DS.Tables[15]);
                AddNewRow("ByGrdCutWiseChartDetail", DS.Tables[16]);
                AddNewRow("KapanWiseMumbaiRateDetail", DS.Tables[17]);

                RepDoc.Subreports["PrdWiseSummary"].SetDataSource(DS.Tables[1]);
                RepDoc.Subreports["LossDetail"].SetDataSource(DS.Tables[2]);
                RepDoc.Subreports["ExtraRejectionPcnDetail"].SetDataSource(DS.Tables[3]);
                RepDoc.Subreports["RMPSizeDetail"].SetDataSource(DS.Tables[4]);
                RepDoc.Subreports["BreakingDetail"].SetDataSource(DS.Tables[5]);
                RepDoc.Subreports["ByGrdSizeWiseDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdShapeWiseDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdGIADetail"].SetDataSource(DS.Tables[8]);
                RepDoc.Subreports["ByGrdMixDetail"].SetDataSource(DS.Tables[9]);
                RepDoc.Subreports["ByGrdShapeWiseChartDetail"].SetDataSource(DS.Tables[7]);
                RepDoc.Subreports["ByGrdFloWiseChartDetail"].SetDataSource(DS.Tables[10]);
                RepDoc.Subreports["ByGrdGIAMixWiseChartDetail"].SetDataSource(DS.Tables[11]);
                RepDoc.Subreports["ByGrdSizeWiseChartDetail"].SetDataSource(DS.Tables[6]);
                RepDoc.Subreports["ByGrdPointerWiseChartDetail"].SetDataSource(DS.Tables[12]);
                RepDoc.Subreports["ByGrdColorWiseChartDetail"].SetDataSource(DS.Tables[13]);
                RepDoc.Subreports["ByGrdClarityWiseChartDetail"].SetDataSource(DS.Tables[14]);
                RepDoc.Subreports["FreshPacketRoughSizeWiseDetail"].SetDataSource(DS.Tables[15]);
                RepDoc.Subreports["ByGrdCutWiseChartDetail"].SetDataSource(DS.Tables[16]);
                RepDoc.Subreports["KapanWiseMumbaiRateDetail"].SetDataSource(DS.Tables[17]);

                crystalReportViewer1.ReportSource = RepDoc;

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
                if (MessageBox.Show("ARE YOU SURE TO CLOSE THE REPORT ?", System.Configuration.ConfigurationManager.AppSettings["CompanyName"].ToString(), MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
                {
                    this.Close();
                }
            }
        }

        private void crystalReportViewer1_DrillDownSubreport(object source, CrystalDecisions.Windows.Forms.DrillSubreportEventArgs e)
        {
            e.Handled = true;
        }

        private void FrmReportViewer_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                if (RepDoc != null)
                {
                    RepDoc.Close();
                    RepDoc.Dispose();
                    RepDoc = null;
                }
            }
            catch (Exception EX)
            {

            }

        }
    }
}
