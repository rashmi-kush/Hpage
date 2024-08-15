using eSigner;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web;
using System.Web.Services;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using iTextSharp.text.pdf;
using HSM_DSC;

namespace CMS_Sampada.CoS
{
    public partial class RRC_Certificate_Proceeding : System.Web.UI.Page
    {
        PaymentDetails_BAL PaymentDetails_BAL = new PaymentDetails_BAL();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];

        string FileName = string.Empty;
        public byte[] pdfBytes;

        string appid;
        string Appno;
        string All_DocFile_Hearing = "";


        Encrypt Encrypt = new Encrypt();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        RRC_Certificate_Bal clsRRC_CertificateBAL = new RRC_Certificate_Bal();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            lblHeadingDist.Text = Session["District_NameHI"].ToString();
            lblHeadingDist1.Text = Session["District_NameHI"].ToString();
            lblHeadingDist2.Text = Session["District_NameHI"].ToString();
            int Flag = 0;

            if (!Page.IsPostBack)
            {
                try
                {

                    if (Session["AppId"].ToString() != null)
                    {
                        DataTable dt = clsRRC_CertificateBAL.Get_RRC_CertificateCases();
                        if (new[] { 44, 45, 49, 50, 51 }.Contains(Convert.ToInt32(dt.Rows[0]["STATUS_ID"])))
                        {
                            //
                        }

                        else if (dt.Rows[0]["STATUS_ID"].ToString() == "88") //Draft proceeding completed
                        {
                            Flag = 1; // for final submit
                            DataTable dt1 = clsRRC_CertificateBAL.Get_RRC_Certi_proceeding_Details(Convert.ToInt32(Session["AppId"].ToString()));
                            if (dt1.Rows.Count > 0)
                            {
                                summernote.Value = dt1.Rows[0]["RRC_CERT_PROCEEDING"].ToString();

                            }

                        }
                        else if (dt.Rows[0]["STATUS_ID"].ToString() == "94") //final submit proceeding completed
                        {

                            Flag = 2; // for esign
                            DataTable dt1 = clsRRC_CertificateBAL.Get_RRC_Certi_proceeding_Details(Convert.ToInt32(Session["AppId"].ToString()));
                            if (dt1.Rows.Count > 0)
                            {
                                summernote.Value = dt1.Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                                Session["RRC_CERT_ORDERSHEET_PATH"] = dt1.Rows[0]["RRC_CERT_ORDERSHEET_PATH"].ToString();
                            }
                        }


                        if (Session["Appno"].ToString() != null)
                        {
                            int hdnUserID = Convert.ToInt32(Session["DROID"].ToString());

                            fill_ddlTemplate1(hdnUserID);

                            Session["case_RegisDate"] = Session["InsertedDate"].ToString();

                            appid = Session["AppId"].ToString();
                            lblProposalIdHeading.Text = Session["Appno"].ToString();
                            lblCase_Number.Text = Session["CaseNo"].ToString();
                            lblCNo.Text = Session["CaseNo"].ToString();
                            lblCaseNumber.Text = Session["CaseNo"].ToString();
                            lblRegisteredDate.Text = Session["case_RegisDate"].ToString();
                            pContent.InnerText = summernote.Value;

                            AllDocList(Convert.ToInt32(Session["AppId"].ToString()));


                            int Appid = Convert.ToInt32(Session["AppId"].ToString());

                            //BindCaseList(Appid);

                            string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                            lblTodatDt.Text = Convert.ToString(TDate);

                            lblToday.Text = Convert.ToString(TDate);
                            DataSet dsDocFinalOrder;

                            dsDocFinalOrder = PaymentDetails_BAL.TOC_RecentDoc(Appid);

                            DataSet dsDocDetails = new DataSet();
                            if (dsDocFinalOrder != null)
                            {
                                if (dsDocFinalOrder.Tables.Count > 0)
                                {

                                    if (dsDocFinalOrder.Tables[0].Rows.Count > 0)
                                    {
                                        string fileName = dsDocFinalOrder.Tables[0].Rows[0]["SIGNED_FINALORDER_PATH"].ToString();
                                        docPath.Src = fileName;
                                    }
                                }
                            }
                            dsDocDetails = clsHearingBAL.GetOrderSheetProceeding(Convert.ToInt32(Session["AppId"].ToString()));
                            if (dsDocDetails != null)
                            {
                                if (dsDocDetails.Tables.Count > 0)
                                {

                                    if (dsDocDetails.Tables[0].Rows.Count > 0)
                                    {
                                        RepDetails.DataSource = dsDocDetails;
                                        RepDetails.DataBind();

                                        foreach (RepeaterItem item in RepDetails.Items)
                                        {
                                            Label lab = item.FindControl("lblDRoffice4") as Label;


                                            //Label lab2 = item.FindControl("lblDRoffice5") as Label;
                                            if (Session["District_NameHI"] != null)
                                            {
                                                lab.Text = Session["District_NameHI"].ToString();
                                                //lab2.Text = Session["District_NameHI"].ToString();
                                            }
                                            else
                                            {
                                                lab.Text = "भोपाल";
                                                //lab2.Text = "भोपाल";
                                            }

                                        }



                                    }
                                }
                            }

                            dsDocDetails = clsHearingBAL.GetNoticeProceeding(Convert.ToInt32(Session["AppId"].ToString()));
                            if (dsDocDetails != null)
                            {
                                if (dsDocDetails.Tables.Count > 0)
                                {

                                    if (dsDocDetails.Tables[0].Rows.Count > 0)
                                    {
                                        string Notice_Pros = dsDocDetails.Tables[0].Rows[0]["Notice_PROCEEDING"].ToString();

                                        if (Notice_Pros != "")
                                        {
                                            PnlNotice.Visible = true;
                                            Repeater_Notice.DataSource = dsDocDetails;
                                            Repeater_Notice.DataBind();
                                            foreach (RepeaterItem item in Repeater_Notice.Items)
                                            {
                                                //Label lab = item.FindControl("lblDRoffice5") as Label;


                                                Label lab2 = item.FindControl("lblDRoffice5") as Label;
                                                if (Session["District_NameHI"] != null)
                                                {
                                                    //lab.Text = Session["District_NameHI"].ToString();
                                                    lab2.Text = Session["District_NameHI"].ToString();
                                                }
                                                else
                                                {
                                                    //lab.Text = "भोपाल";
                                                    lab2.Text = "भोपाल";
                                                }

                                            }
                                        }
                                        else
                                        {
                                            PnlNotice.Visible = false;
                                        }

                                    }
                                }
                            }

                            dsDocDetails = clsRRC_CertificateBAL.GetRRC_CertificateProceeding(Session["CaseNo"].ToString());
                            if (dsDocDetails != null)
                            {
                                if (dsDocDetails.Tables.Count > 0)
                                {

                                    if (dsDocDetails.Tables[0].Rows.Count > 0)
                                    {
                                        string RRC_Certificate_Pros = dsDocDetails.Tables[0].Rows[0]["PROCEEDING"].ToString();

                                        if (RRC_Certificate_Pros != "")
                                        {
                                            pnlRRC_Certificate.Visible = true;
                                            RptRRC_Certificate.DataSource = dsDocDetails;
                                            RptRRC_Certificate.DataBind();
                                            foreach (RepeaterItem item in RptRRC_Certificate.Items)
                                            {
                                                //Label lab = item.FindControl("lblDRoffice5") as Label;


                                                Label lab2 = item.FindControl("lblDRofficeCertificate") as Label;
                                                if (Session["District_NameHI"] != null)
                                                {
                                                    //lab.Text = Session["District_NameHI"].ToString();
                                                    lab2.Text = Session["District_NameHI"].ToString();
                                                }
                                                else
                                                {
                                                    //lab.Text = "भोपाल";
                                                    lab2.Text = "भोपाल";
                                                }

                                            }
                                        }
                                        else
                                        {
                                            pnlRRC_Certificate.Visible = false;
                                        }

                                    }
                                }
                            }


                            Session["All_DocSheet"] = Appid + "_All_COSSheet.pdf";
                            All_DocFile_Hearing = Session["All_DocSheet"].ToString();

                            CreateEmptyFile(All_DocFile_Hearing);
                            CraetSourceFile(Appid);
                            AllDocList(Appid);

                            DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Appid, Appno);
                            if (dtDocDetails.Rows.Count > 0)
                            {
                                if (dtDocDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                                {
                                    RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                                    //iAllDoc.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                                    RecentdocPath.Visible = true;
                                }


                            }

                            DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Appid, Appno);

                            if (dtDocProDetails.Rows.Count > 0)
                            {
                                if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                                {
                                    RecentProposalDoc.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                                    //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                                    RecentProposalDoc.Visible = true;
                                }


                            }

                            DataTable dtDocPrevProced = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Appid, Appno);

                            //if (dtDocPrevProced.Rows.Count > 0)
                            //{

                            //        lblPrevious.Text = dtDocPrevProced.Rows[0]["additional_proceeding"].ToString();
                            //        IfProceeding.Src = dtDocPrevProced.Rows[0]["SIGNED_NOTICE_PROCEEDING_PATH"].ToString();


                            //}

                            DataTable dtDocAttachedDetails = objClsNewApplication.Get_Recent_ATTACHED_DOC_CoS_Hand(Appid, Appno);



                        }
                        if (Flag == 1)   // for final submit
                        {
                            Pnl_AddProceeding.Visible = false;
                            pnlDraft.Visible = true;
                            btnSaveRRCcerti.Style.Add("display", "block");
                            pnlEsignDSC.Visible = false;
                            string script1 = "$('#custom_tabs_one_Display').addClass('active');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab1", script1, true);
                            custom_tabs_one_profile_tab.Style.Add("display", "block");
                            string script2 = "$('#custom_tabs_one_profile_tab').removeClass('active show');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab2", script2, true);

                        }
                        else if (Flag == 2) // for esign
                        {
                            Pnl_AddProceeding.Visible = false;
                            pnlDraft.Visible = false;
                            btnSaveRRCcerti.Style.Add("display", "none");
                            pnlEsignDSC.Visible = true;
                            string script1 = "$('#custom_tabs_one_Display').addClass('active');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab1", script1, true);
                            custom_tabs_one_profile_tab.Style.Add("display", "block");
                            string script2 = "$('#custom_tabs_one_profile_tab').removeClass('active show');" +
                                "$('a[href=\"#custom-tabs-one-RegisteredForm\"]').addClass('disabled').attr('aria-disabled', 'true');";
                            ClientScript.RegisterStartupScript(this.GetType(), "DeactivateTab2", script2, true);
                            string FileNme = "";
                            if (Session["RRC_CERT_ORDERSHEET_PATH"] != null)
                            {
                                FileNme = Session["RRC_CERT_ORDERSHEET_PATH"].ToString();
                                Session["ActualPath"] = FileNme;
                            }
                        }
                        else
                        {

                        }


                    }
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
        }

        public static void MargeMultiplePDF(string[] PDFfileNames, string OutputFile)
        {
            iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
            // Create a object of FileStream which will be disposed at the end  
            using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
            {
                // Create a PDFwriter that is listens to the Pdf document  
                iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
                if (PDFwriter == null)
                {
                    return;
                }
                // Open the PDFdocument  
                PDFdoc.Open();
                foreach (string fileName in PDFfileNames)
                {
                    if (fileName == null)
                    {

                    }
                    else
                    {
                        // Create a PDFreader for a certain PDFdocument  
                        iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);
                        PDFreader.ConsolidateNamedDestinations();
                        // Add content  
                        for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                        {
                            iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                            PDFwriter.AddPage(page);
                        }
                        iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                        if (form != null)
                        {
                            PDFwriter.CopyDocumentFields(PDFreader);
                        }
                        // Close PDFreader  
                        PDFreader.Close();
                    }

                }
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }

        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CosAllDocRRC_Certificate_Proceed/" + All_DocFile_Hearing;
            }
        }

        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CosAllDocRRC_Certificate_Proceed/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CosAllDocRRC_Certificate_Proceed/", "<p>RRC_Certificate_Proceeding</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CosAllDocRRC_Certificate_Proceed/", "<p>RRC_Certificate_Proceeding</p>");
            }
            ViewState["ALLDocAdded_Hearing"] = "~/CosAllDocRRC_Certificate_Proceed/" + filename;
            ViewState["CosAllDocRRC_Certificate_Proceed"] = serverpath;
        }


        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dt = PaymentDetails_BAL.GetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (File.Exists(Server.MapPath(dt.Rows[i]["ORDRSHEETPATH"].ToString())))
                        {
                            addedfilename[i] = Server.MapPath(dt.Rows[i]["ORDRSHEETPATH"].ToString());
                        }
                    }

                    string sourceFile = ViewState["CosAllDocRRC_Certificate_Proceed"].ToString();
                    if (IsValidPdf(sourceFile))
                    {
                        MargeMultiplePDF(addedfilename, sourceFile);
                        setAllPdfPath(ViewState["ALLDocAdded_Hearing"].ToString());

                    }


                }

            }
            catch (Exception ex)
            {

            }

        }

        private bool IsValidPdf(string filepath)
        {
            bool Ret = true;

            PdfReader reader = null;

            try
            {
                if (File.Exists(filepath))
                {
                    reader = new PdfReader(filepath);
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                Ret = false;

            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

            }

            return Ret;
        }

        public void AllDocList(int APP_ID)
        {
            try
            {
                DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, "");
                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(APP_ID, Appno);
                if (dsIndexDetails != null)
                {
                    if (dsIndexDetails.Tables.Count > 0)
                    {

                        if (dsIndexDetails.Tables[0].Rows.Count > 0)
                        {
                            grdSRDoc.DataSource = dsIndexDetails.Tables[0];
                            grdSRDoc.DataBind();

                        }

                    }
                }




            }
            catch (Exception ex)
            {

            }

        }

        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }
        private void SaveRRC_CertificateProceedingPDF()
        {

            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");
                StringBuilder stringBuilder = new StringBuilder();


                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 20px 30px 30px 30px; height:1350px; position:relative;top:0px;'>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, गुना (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>प्रारूप-अ</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>राजस्व आदेशपत्र</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>कलेक्टर ऑफ़ स्टाम्प, गुना के न्यायालय में मामला क्रमांक-" + lblCaseNumber.Text + "</h2> ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<table style='width: 940px;border: 1px solid black; border-collapse: collapse;' ");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>आदेश क्रमांक कार्यवाही ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("की तारीख एवं स्थान </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही");
                stringBuilder.Append("<br/>मध्यप्रदेश शासन विरूद्ध </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पक्षों/वकीलों <br> आदेश  पालक लिपिक के हस्ताक्षर</th>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style='border: 1px solid black; height: 1030px;vertical-align: baseline;border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '>");
                stringBuilder.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black;  height: 1030px;vertical-align: baseline;border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '>");
                stringBuilder.Append("<div style='padding: 2px;'>");
                stringBuilder.Append("<p style='text-align: center;'>Case Number: " + (lblCNo.Text) + "</p>");
                //stringBuilder.Append("<p style='text-align: center;'>Case Number: " + (lblCNo.Text) +  lblAppID.Text + lblTodatDt.Text + "</p>");
                stringBuilder.Append("<p style='text-align: justify;'>" + dfd + " <br><br></p>");
                //stringBuilder.Append("<p style='text - align: left;'>" + lblSeekContent.Text + "</p>");
                stringBuilder.Append("<div>");


                stringBuilder.Append("</b>");
                //stringBuilder.Append(DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 100px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><</b>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 150px;left:130px;'>कलेक्टर ऑफ़ स्टाम्प्स,<br/>गुना <br/><br/> </b>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                // stringBuilder.Append("</div>");



                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_RRC_CertificateProceeding.pdf";
                ViewState["ActualPath"] = FileNme;
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["ActualPath"] = ConvertHTMToPDF(FileNme, "~/Cos_RRC_CertificateProceeding/", stringBuilder.ToString());
                Session["ActualPath"] = "~/Cos_RRC_CertificateProceeding/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                SaveRRC_CertificateProceeding("~/Cos_RRC_CertificateProceeding/" + FileNme);


            }
            catch (Exception ex)
            {

            }

        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAdd = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAdd = ip.ToString();
                    return ipAdd;
                }
            }
            return ipAdd;
        }
        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                string FileName = FileNme;
                string RRC_CertificateProceedingPath = Server.MapPath(path);
                if (!Directory.Exists(RRC_CertificateProceedingPath))
                {
                    Directory.CreateDirectory(RRC_CertificateProceedingPath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = RRC_CertificateProceedingPath;
                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);
                string pdf_orientation = "Portrait";
                PdfPageOrientation pdfOrientation =
                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                    pdf_orientation, true);
                int webPageWidth = 1024;
                int webPageHeight = 0;
                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();
                // set converter options
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.MarginLeft = 30;
                converter.Options.MarginRight = 30;
                converter.Options.MarginTop = 20;
                converter.Options.MarginBottom = 30;

                /****** Start PAGE FOOTER PAGE NUMBERING ******/
                bool showFooterOnFirstPage = true;
                bool showFooterOnOddPages = true;
                bool showFooterOnEvenPages = true;

                int footerHeight = 50;
                converter.Options.DisplayFooter = showFooterOnFirstPage || showFooterOnOddPages || showFooterOnEvenPages;
                converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
                converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
                converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
                converter.Footer.Height = footerHeight;


                PdfTextSection text = new PdfTextSection(0, 10,
                            "Page: {page_number} of {total_pages}  ",
                            new System.Drawing.Font("Arial", 8));
                text.HorizontalAlign = PdfTextHorizontalAlign.Right;
                converter.Footer.Add(text);


                /****** End PAGE FOOTER PAGE NUMBERING ******/

                // create a new pdf document converting an url
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

                // create a new pdf font (system font)

                //string filpt = "";
                //string ToSaveFileTo = NoticePath + "/" + FileName;
                //byte[] bytes2 = null;

                byte[] bth = doc.Save();

                using (var stream = File.Create(Path.Combine(RRC_CertificateProceedingPath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return RRC_CertificateProceedingPath + "/" + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        protected void btnRRC_Certi_Click(object sender, EventArgs e)
        {
            SaveRRC_CertificateProceedingPDF();

            //custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
            btnSaveRRCcerti.Style.Add("display", "none");
            pnlEsignDSC.Visible = true;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>  DisplayESign();</script>");
        }


        protected void grdSRDoc_Sorted(object sender, GridViewSortEventArgs e)
        {
            //DataTable dt = grdSRDoc.DataSource as DataTable;
            DataTable dt = (DataTable)ViewState["SortDirection"];

            if (dt != null)
            {
                // Sort the data based on the selected column and sort direction.
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + " " + GetSortDirection(e.SortDirection);

                // Rebind the GridView with the sorted data.
                grdSRDoc.DataSource = dv;
                grdSRDoc.DataBind();
            }

        }

        private string GetSortDirection(SortDirection sortDirection)
        {
            // Determine the sort direction (ASC or DESC).
            string newSortDirection = "ASC";

            if (ViewState["SortDirection"] != null)
            {
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    newSortDirection = "DESC";
                }
            }

            // Store the new sort direction in ViewState.
            ViewState["SortDirection"] = newSortDirection;

            return newSortDirection;
        }
        protected void btnDraftSave_Click(object sender, EventArgs e)
        {

            try
            {
                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");
                GetLocalIPAddress();
                int AppId = Convert.ToInt32(Session["AppId"].ToString());
                string CaseNo = Session["CaseNo"].ToString();

                DataTable dt = clsRRC_CertificateBAL.Get_CertificateProoceding_COSReader(AppId);
                if (dt.Rows.Count > 0)
                {
                    string RRC_CertiProced_id = dt.Rows[0]["ID"].ToString();
                    Session["RRC_CertiProcedReader_id"] = dt.Rows[0]["ID"].ToString();
                    Session["RRC_CertiProced_id"] = dt.Rows[0]["ID"].ToString();
                    Session["RRC_CertiProced_id_Status"] = dt.Rows[0]["STATUS_ID"].ToString();

                    int RRC_CertiReader_id = Convert.ToInt32(Session["RRC_CertiProced_id"].ToString());
                    //DataSet dtUp = OrderSheet_BAL.InsertIntoOrderSheet_Reader(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), "", PartyID, summernote.Value, "", "", Convert.ToInt32(Session["ordersheetReader_id"].ToString()));
                    DataSet dtUp = clsRRC_CertificateBAL.UPDATE_RRC_CERTIFICATE_PROCEEDING_DTLREADER(AppId, CaseNo, dfd, "", "", "", Convert.ToInt32(Session["RRC_CertiProced_id"].ToString()));
                    if (dtUp.Tables.Count > 0)
                    {

                        Session["RRC_CertiProced_id"] = dtUp.Tables[0].Rows[0]["ID"].ToString();
                        Session["RRC_CertiProced_id_Status"] = dtUp.Tables[0].Rows[0]["STATUS_ID"].ToString();
                        Session["RRC_CertiProced_PROCEEDING"] = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        summernote.Value = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        Session["RRC_CertiProcedContent"] = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        //pnl_Proceding.Visible = false;
                        //custom_tabs_one_Display.Visible=true;

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>  DisplayESign();</script>");
                    }
                    else
                    {
                        string Message = "Sorry! Something Went while saving the draft.";
                        string Title = "Success";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','success');", true);
                        return;
                    }

                }
                else
                {
                    DataSet dtUp = clsRRC_CertificateBAL.insert_RRC_CERT_OrderSheet_Details(Convert.ToInt32(Session["AppId"].ToString()), Session["CaseNo"].ToString(), dfd, "", "", "");

                    if (dtUp.Tables.Count > 0)
                    {

                        Session["RRC_CertiProced_id"] = dtUp.Tables[0].Rows[0]["ID"].ToString();
                        Session["RRC_CertiProced_id_Status"] = dtUp.Tables[0].Rows[0]["STATUS_ID"].ToString();


                    }
                }




            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            btnDraftSave.Visible = false;
            pnlEsignDSC.Visible = false;
            //pnlHearingDate.Visible = true;
            btnSaveRRCcerti.Style.Add("display", "block");// = true;
            pContent.InnerHtml = summernote.Value;



            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>  DisplayESign();</script>");

        }

        private void SaveRRC_CertificateProceeding(string Path)
        {

            try
            {

                string FileName = "RRC_CertificateProceeding_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                ViewState["ActualPath"] = Path;

                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");



                DataTable dt = clsRRC_CertificateBAL.Get_CertificateProoceding_COSReader(Convert.ToInt32(Session["AppId"].ToString()));
                if (dt.Rows.Count > 0)
                {

                    string RRC_Certificate_id = dt.Rows[0]["ID"].ToString();
                    Session["RRC_CertiReader_id"] = dt.Rows[0]["ID"].ToString();
                    Session["RRC_Certi_id"] = dt.Rows[0]["ID"].ToString();
                    Session["RRC_Certi_id_Status"] = dt.Rows[0]["STATUS_ID"].ToString();


                    //    DataSet dtUp = clsRRC_CertificateBAL.INSERT_RRC_CERTIFICATE_PROCEEDING_DTLREADER(Convert.ToInt32(Session["AppId"].ToString()), Convert.ToInt32(Session["RRC_CertiReader_id"].ToString()), ViewState["ActualPath"].ToString(), dfd, "", "", Session["CaseNo"].ToString());
                    //    if (dtUp.Tables.Count > 0)
                    //    {
                    //        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["ID"].ToString();
                    //        //Session["RRC_Certificate_id_Status"] = dtUp.Tables[0].Rows[0]["RRC_Certificate_id"].ToString();
                    //        //Session["PROCEEDING"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                    //        //summernote.Value = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                    //        //Session["RRC_CertificateContent"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                    //        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
                    //    }

                    //}
                    //else
                    //{

                    DataSet dtUp = clsRRC_CertificateBAL.FINAL_insert_RRC_CERT_OrderSheet_Details(Convert.ToInt32(Session["AppId"].ToString()), Convert.ToInt32(Session["RRC_CertiReader_id"].ToString()), Session["CaseNo"].ToString(), dfd, ViewState["ActualPath"].ToString(), "", "");
                    if (dtUp.Tables.Count > 0)
                    {
                        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["ID"].ToString();
                        Session["RRC_Certificate_id_Status"] = dtUp.Tables[0].Rows[0]["STATUS_ID"].ToString();


                    }
                }




            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {

            int App_ID = Convert.ToInt32(Session["AppId"].ToString());
            int Notice_id = Convert.ToInt32(Session["Notice_ID"].ToString());
            if (ddl_SignOption.SelectedValue == "1")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
                }



                //-------eSign Start------------------------


                string Location = "Bhopal";

                //string ApplicationNo = hdnProposal.Value;

                string PdfName = Session["ActualPath"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_CertificateProceeding/", "");

                ViewState["filename"] = PdfName;


                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_CertificateProceeding/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

                string unsignFilePath = FileNamefmFolder;

                string path = @"" + unsignFilePath;
                string file = Path.GetFileNameWithoutExtension(path);
                string NewPath = path.Replace(file, file + "_Signed");

                //string signFileFinalPath = @"/CMS/OrderSheet/CMS-R-05032400001549_2024Mar20122627_OrderSheet-Signed.pdf";
                string signFileFinalPath = NewPath;



                if (File.Exists(FileNamefmFolder))
                {
                    if (ddl_SignOption.SelectedValue == "1")
                    {
                        if (TxtLast4Digit.Text.Length != 4)
                        {
                            TxtLast4Digit.Focus();
                            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                            return;
                        }
                        string Last4DigitAadhaar = TxtLast4Digit.Text;

                        if (File.Exists(FileNamefmFolder))
                        {
                            string ResponseURL = null;
                            string txtSignedBy = "Collector of Stamp";
                            string UIDToken = "";
                            string TransactionId = getTransactionID();
                            string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                            string TransactionOn = "Pre";


                            ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_RRC_CertificateProced.aspx?Response_type=RRC_Certificate_Proceeding");

                            // DataTable dt = clsRRC_CertificateBAL.InserteSignDSC_Status_RRC_CertiProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Convert.ToInt32(Session["RRC_CertiProced_id"].ToString()));


                            AuthMode authMode = new AuthMode();

                            //string authType = "";

                            if (ddleAuthMode.SelectedValue == "1")
                            {
                                authMode = AuthMode.OTP;


                            }
                            else if (ddleAuthMode.SelectedValue == "2")
                            {
                                authMode = AuthMode.Biometric;

                            }



                            eSigner.eSigner _esigner = new eSigner.eSigner();


                            _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);

                            //getdata_Esign();
                        }
                        else
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' प्रमाण पत्र के लिए pdf फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);
                        }

                    }

                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                }

                //-------eSign End------------------------

                //####
                // DataTable dt1 = clsRRC_CertificateBAL.InserteSignDSC_Status_RRC_CertiProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Convert.ToInt32(Session["RRC_CertiProced_id"].ToString()));

            }
            else if (ddl_SignOption.SelectedValue == "3")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
                }


                string PdfName = Session["ActualPath"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_CertificateProceeding/", "");

                ViewState["filename"] = PdfName;


                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_CertificateProceeding/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

                string unsignFilePath = FileNamefmFolder;

                string path = @"" + unsignFilePath;
                string file = Path.GetFileNameWithoutExtension(path);
                if (File.Exists(FileNamefmFolder))
                {
                    if (ddl_SignOption.SelectedValue == "3")
                    {
                        if (TxtLast4Digit.Text.Length != 4)
                        {
                            TxtLast4Digit.Focus();
                            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                            return;
                        }

                        string Last4DigitAadhaar = TxtLast4Digit.Text;

                        if (File.Exists(FileNamefmFolder))
                        {
                            //txtDepartmentID.Text = "MPWCDDOH01";
                            string ResponseURL_eMudra = null;
                            //string eSignURL = null;
                            //string secretKey = null;
                            string txtSignedBy = "Collector of Stamp";
                            string UIDToken = "";
                            //string DepartmentID = txtDepartmentID.Text;
                            //string Last4DigitAadhaar = TxtLast4Digit.Text;
                            string TransactionId = getTransactionID();
                            string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                            string TransactionOn = "Pre";

                            //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                            //ResponseURL_eMudra = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx");

                            //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "1", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                            AuthMode authMode = new AuthMode();

                            //string authType = "";

                            if (ddleAuthMode.SelectedValue == "1")
                            {
                                authMode = AuthMode.OTP;


                            }
                            else if (ddleAuthMode.SelectedValue == "2")
                            {
                                authMode = AuthMode.Biometric;

                            }

                            eSigner.eSigner _esigner = new eSigner.eSigner();

                            //_esigner.CreateRequest_eMudra(ResponseURL_eMudra, eSignURL_eMudra, TransactionOn, txtSignedBy, Application_Id_eMudra, UIDToken, Department_Id_eMudra, Secretkey_eMudra, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                            //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), order_id);
                        }
                        else
                        {
                            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','pdf फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);
                        }

                    }



                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                }
            }
            else if (ddl_SignOption.SelectedValue == "2")
            {

                string PdfName = Session["ActualPath"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_CertificateProceeding/", "");

                ViewState["filename"] = PdfName;


                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_CertificateProceeding/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

                string unsignFilePath = FileNamefmFolder;

                string path = @"" + unsignFilePath;
                string file = Path.GetFileNameWithoutExtension(path);
                string NewPath = path.Replace(file, file + "_Signed");

                //string signFileFinalPath = @"/CMS/OrderSheet/CMS-R-05032400001549_2024Mar20122627_OrderSheet-Signed.pdf";
                string signFileFinalPath = NewPath;

                string label = Session["HSMCertLabel"].ToString();
                string signName = "Collector of Stamp"; //// Session["Designation"].ToString();
                string location = Session["District_NameEN"].ToString();
                string reason = "Order Sheet";
                string partitionName = Partition_Name;
                string partitionPassword = Partition_Password;
                string hsmSlotNo = HSM_Slot_No; ////Session["HSMSlotNo"].ToString();

                if (File.Exists(FileNamefmFolder))
                {
                    HSMSigner hSMSigner = new HSMSigner(unsignFilePath, signFileFinalPath, label, signName, location, reason, partitionName, partitionPassword, hsmSlotNo);

                    hSMSigner.hsm_DSC();
                    ;
                    if (File.Exists(NewPath))
                    {
                        Session["RecentSheetPath"] = NewPath;


                        int Flag = 1;
                        string resp_status = 1.ToString();

                        //string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status;

                        // ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


                    }


                    DataTable dt2 = clsRRC_CertificateBAL.InserteSignDSC_Status_RRC_CertiProceeding(App_ID, "2", "", GetLocalIPAddress(), Convert.ToInt32(Session["RRC_CertiProced_id"].ToString()));
                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                }




                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage_esign();", true);

            }
        }
        public void fill_ddlTemplate1(int userid)
        {
            DataTable dt = new DataTable();
            ddlTemplates1.Items.Clear();
            try
            {

                dt = clsRRC_CertificateBAL.GET_MASTERS_PROCEEDING_TEMPLATES(userid);

                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddlTemplates1.Items.Add(new System.Web.UI.WebControls.ListItem(text, value));
                    }
                }

                ddlTemplates1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Template--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        [WebMethod]
        public static string GetTemplate_Notice(string TemId)
        {

            RRC_Certificate_Bal clscerti_Bal = new RRC_Certificate_Bal();
            string Template = "<h1>RRC1</h1>";
            DataTable dt = clscerti_Bal.GetTemplates();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["COS_TEMP_ID"].ToString() == TemId)
                    {
                        Template = dt.Rows[i]["TEMPLATE_VALUE"].ToString();
                        break;
                    }
                    //Console.WriteLine(dt.Rows[i]["RRC_TEMP_ID"]);
                }


            }


            return Template;



        }


        private void ShowAlert(string title, string message, string icon, string redirectUrl = "")
        {
            string script = string.IsNullOrEmpty(redirectUrl)
                ? $"Swal.fire('{title}', '{message}', '{icon}');"
                : $"Swal.fire('{title}', '{message}', '{icon}').then((result) => {{ if (result.isConfirmed) {{ window.location.href = '{redirectUrl}'; }} }});";

            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
        }

    }
}