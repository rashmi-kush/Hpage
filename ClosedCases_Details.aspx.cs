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

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using iTextSharp.text.pdf;
using HSM_DSC;
using RestSharp;
using System.Text.Json;
using Newtonsoft.Json;

namespace CMS_Sampada.CoS
{
    public partial class ClosedCases_Details : System.Web.UI.Page
    {
        PaymentDetails_BAL PaymentDetails_BAL = new PaymentDetails_BAL();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        string FileName = string.Empty;
        public byte[] pdfBytes;

        string appid;
        string Appno;
        string All_DocFile_Hearing = "";


        Encrypt Encrypt = new Encrypt();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        ReportSeeking_BAL objSeekReport = new ReportSeeking_BAL();
        CoSFinalOrder_BAL objFinalOrder = new CoSFinalOrder_BAL();

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblHeadingDist.Text = Session["District_NameHI"].ToString();
            //lblHeadingDist1.Text = Session["District_NameHI"].ToString();
            //lblHeadingDist2.Text = Session["District_NameHI"].ToString();
            //lblCOSOfficeNameHi.Text = Session["SRONameHI"].ToString();
            //lblSROAddressHi.Text = Session["officeAddress"].ToString();
            hdnCOSOfficeNameHi1.Value = Session["officeAddress"].ToString();
            //lblCOSOfficeNameHi2.Text = Session["officeAddress"].ToString();
            hdTocan.Value = Session["Token"].ToString();
            if (!Page.IsPostBack)
            {
                try
                {

                    int Flag = 0;
                    //if (Request.QueryString["Flag"] != null)
                    //{
                    //    Flag = Convert.ToInt32(Request.QueryString["Flag"].ToString());
                    //}




                    if (Session["AppID"] != null)
                    {
                        //Session["AppID"] = (Request.QueryString["AppId"].ToString());
                        //Session["Appno"] = (Request.QueryString["AppNo"].ToString());
                        //Session["CaseNo"] = (Request.QueryString["CaseNo"].ToString());
                        ViewState["AppID"] = Session["AppID"];
                        ViewState["Appno"] = Session["Appno"];
                        ViewState["CaseNo"] = Session["CaseNo"];//Session["CaseNo"] = (Request.QueryString["CaseNo"].ToString());

                        appid = Session["AppID"].ToString();


                        lblProposalIdHeading.Text = Session["Appno"].ToString();
                        lblCase_Number.Text = Session["CaseNo"].ToString();

                      

                        int Appid = Convert.ToInt32(Session["AppID"].ToString());

                        BindCaseList(Appid);

                     
                        DataSet dsDocFinalOrder;

                        dsDocFinalOrder = PaymentDetails_BAL.TOC_RecentDoc(Appid);

                        DataSet dsDocDetails = new DataSet();
                        if (dsDocFinalOrder != null)
                        {
                            if (dsDocFinalOrder.Tables.Count > 0)
                            {

                                if (dsDocFinalOrder.Tables[0].Rows.Count > 0)
                                {
                                    //string fileName = dsDocFinalOrder.Tables[0].Rows[0]["SIGNED_FINALORDER_PATH"].ToString();
                                    //docPath.Src = fileName;
                                }
                            }
                        }
                      


                        Session["All_DocSheet"] = Appid + "_All_COSSheet.pdf";
                        All_DocFile_Hearing = Session["All_DocSheet"].ToString();

                        CreateEmptyFile(All_DocFile_Hearing);
                        CraetSourceFile(Appid);
                        AllDocList(Appid);
                        DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Convert.ToInt32(appid), Appno);
                        if (dtDocDetails.Rows.Count > 0)
                        {
                            //if (dtDocDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                            //{
                            //    RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                            //    //iAllDoc.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();




                            //}


                            string Base64 = Comsumedata("RegistryDocument", Convert.ToInt32(dtDocDetails.Rows[0]["ereg_id"]));
                            //Response.Write(Base64);
                            string encodedPdfData = "";
                            if (Base64 != null)
                            {
                                hdnbase64.Value = Base64;
                                //encodedPdfData = "data:application/pdf;base64," + Base64 + "";
                                //RecentdocPath.Attributes["src"] = encodedPdfData;
                                //RecentdocPath.Src = Base64;
                                RecentdocPath.Visible = true;
                            }
                            else
                            {
                                RecentdocPath.Visible = false;
                                ///ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                            }


                        }

                        DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(appid), Appno);

                        if (dtDocProDetails.Rows.Count > 0)
                        {




                            string Base64 = Comsumedata("ProposalDocument", Convert.ToInt32(dtDocProDetails.Rows[0]["ereg_id"]));
                            string encodedPdfData = "";
                            if (Base64 != null)
                            {
                                encodedPdfData = "data:application/pdf;base64," + Base64 + "";
                                RecentProposalDoc.Attributes["src"] = encodedPdfData;
                            }
                            else
                            {
                                RecentProposalDoc.Visible = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                            }

                        }

                        DataTable dtDocAttachedDetails = objClsNewApplication.Get_Recent_ATTACHED_DOC_CoS_Hand(Convert.ToInt32(appid), Appno);

                        if (dtDocAttachedDetails.Rows.Count > 0)
                        {
                            //if (dtDocAttachedDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                            //{
                            //    RecentAttachedDoc.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocAttachedDetails.Rows[0]["File_Path"].ToString();
                            //    //iAllDocPro.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocAttachedDetails.Rows[0]["File_Path"].ToString();
                            //}


                            //else
                            //{

                            //    RecentAttachedDoc.Visible = false;
                            //    //iAllDocPro.Visible = false;
                            //}


                            string Docbase64 = Comsumedata("AdditionalDocument", Convert.ToInt32(dtDocAttachedDetails.Rows[0]["ereg_id"]));
                            string encodedPdfData = "";
                            if (Docbase64 != null)
                            {
                                encodedPdfData = "data:application/pdf;base64," + Docbase64 + "";
                                RecentAttachedDoc.Attributes["src"] = encodedPdfData;
                            }
                            else
                            {
                                RecentAttachedDoc.Visible = false;
                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                            }

                        }




                      
                    }



                    else
                    {

                    }

                   
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected string Comsumedata(string DocumentType, int RegID)
        {
            string base64 = "";
            try
            {

                //var Token = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJtYW5vai5kcm8uaGFyZGEuYXBwcm92ZXJAbXAuZ292LmluIiwiaXAiOiIxMDMuMTYwLjQ5LjEzNSIsInVzZXJBZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS8xMjQuMC4wLjAgU2FmYXJpLzUzNy4zNiIsImV4cCI6MTcxMzg4MDE0MCwiaWF0IjoxNzEzODc2NTQwfQ.J8LUONwCcxtSz8nu0PQEisn9WL5iZjTfqoVK-8EWEl29T7TqnB9TOnLQqIaFW9OSGtJYOi8DIbU7mhB-6vh-0OdHuiUcs8GbYVUwZk8gYT6j2JU2ON9k-XLaBb0FiakKLnpLEOPe2t4Rr2KGBBuZXSop4Uk47RG6907OAh9ZcRy-aTNC82MHCYL5sgaUlYO9ATUMWgnR1yihrfoKOqzFJ2S92vk_vac9FcFfABtYfaK2VLngH2YxrZYmQaWaWu58Q_UV4zH3n088mdtSrReqJcVzk7CpTrz09yTcrNHbN7peD_NLnytSbkoLDgVA8y80my9o2Qm9pkFlRF_Krtt-7g";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var client = new RestClient("https://ersuat2.mp.gov.in/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");     //UAT

                var client = new RestClient(RegProposalAttDocument_url + DocumentType + "/" + RegID + "");        //PROD
                                                                                                                  //var client = new RestClient("RegProposalAttDocument_url" + DocumentType + "/" + RegID + "");
                var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Authorization", Token);
                request.AddHeader("Authorization", Session["Token"].ToString());
                //request.AddHeader("Authorization", tocan);
                //request.AddParameter("userid", "1");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                string Result = response.Content;
                //string base64 = "";
                if (Result != "")
                {
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    oJS.MaxJsonLength = 2147483647;
                    ResonseA resonse = oJS.Deserialize<ResonseA>(Result);
                    base64 = resonse.responseData;
                    //bytes= Convert.FromBase64String(resonse.responseData);
                    //Response.Write(base64);
                }
                else
                {
                    //Response.Write("Data not found");
                    base64 = null;
                }
                // Generate a unique file name

                //string encodedPdfData = "data:application/pdf;base64," + base64 + "";


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return base64;

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
                ifPDFViewerAll.Src = "~/CoSAllDocPayment/" + All_DocFile_Hearing;
            }
        }

        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoSAllDocPayment/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoSAllDocPayment/", "<p>Order Sheet</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoSAllDocPayment/", "<p>Order Sheet</p>");
            }
            ViewState["ALLDocAdded_Hearing"] = "~/CoSAllDocPayment/" + filename;
            ViewState["CoSAllDocPayment"] = serverpath;
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

                    string sourceFile = ViewState["CoSAllDocPayment"].ToString();
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

        void BindCaseList(int Appid)
        {

            try
            {

                DataTable dsPaymentDetail = new DataTable();
                dsPaymentDetail = PaymentDetails_BAL.GetCaseListById(Appid);
                if (dsPaymentDetail != null)
                {
                    if (dsPaymentDetail.Rows.Count > 0)
                    {
                        txtFinalAmount.Text = dsPaymentDetail.Rows[0]["FINAL_PAYABLE_AMOUNT"].ToString();
                        txtPaidAmount.Text = dsPaymentDetail.Rows[0]["PAID_AMOUNT"].ToString();
                        //txtPaidAmount.Text = dsPaymentDetail.Rows[0]["FINAL_PAYABLE_AMOUNT"].ToString();
                        txtAmountType.Text = dsPaymentDetail.Rows[0]["AMOUNT_TYPE"].ToString();
                        txtURN.Text = dsPaymentDetail.Rows[0]["urn"].ToString();
                        txtPaymentStatus.Text = dsPaymentDetail.Rows[0]["payment_status"].ToString();
                        txtPaidDate.Text = dsPaymentDetail.Rows[0]["payment_date"].ToString();
                        txtPurpose.Text = dsPaymentDetail.Rows[0]["PURPOSE"].ToString();


                        lblRegisteredDate.Text = dsPaymentDetail.Rows[0]["Impound_Date"].ToString();
                        ViewState["eregid"] = dsPaymentDetail.Rows[0]["ereg_id"].ToString();
                        //Session["Reason"] = "COS Certificate - Order passed in Case Number " + Session["Appno"].ToString() + ", Date " + Session["FinalOrderdate"].ToString() + ", Ordered amount " + dsPaymentDetail.Rows[0]["FINAL_PAYABLE_AMOUNT"].ToString() + ", Challan " + dsPaymentDetail.Rows[0]["urn"].ToString() + ", Dated " + dsPaymentDetail.Rows[0]["payment_date"].ToString() + ", Document as stamped by Collector of Stamp.";
                    }
                }


                //if (txtFinalAmount.Text == txtPaidAmount.Text)
                //{
                //    pnlBtnSave.Visible = true;
                //}
                //else
                //{
                //    pnlBtnSave.Visible = false;
                //}

            }
            catch (Exception ex)
            {

            }
        }


        //public void AllDocList(int APP_ID)
        //{
        //    try
        //    {
        //        DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);
        //        DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, Session["Appno"].ToString());

        //        if (dsIndexDetails != null)
        //        {
        //            if (dsIndexDetails.Tables.Count > 0)
        //            {

        //                if (dsIndexDetails.Tables[0].Rows.Count > 0)
        //                {
        //                    grdSRDoc.DataSource = dsIndexDetails;
        //                    grdSRDoc.DataBind();

        //                    ViewState["SortDirection"] = dsIndexDetails;
        //                    ViewState["sortdr"] = "Asc";

        //                }

        //            }
        //        }



        //    }
        //    catch (Exception ex)
        //    {
        //        Response.Write("Error: " + ex.Message);
        //    }

        //}

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
                            grdSRDoc.DataSource = dsIndexDetails;
                            grdSRDoc.DataBind();

                            ViewState["SortDirection"] = dsIndexDetails;
                            ViewState["sortdr"] = "Asc";

                        }

                    }
                }



            }
            catch (Exception ex)
            {

            }

        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {

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
                string OrderSheetPath = Server.MapPath(path);
                if (!Directory.Exists(OrderSheetPath))
                {
                    Directory.CreateDirectory(OrderSheetPath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = OrderSheetPath;
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

                using (var stream = File.Create(Path.Combine(OrderSheetPath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return OrderSheetPath + "/" + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

      
       

      
      

       

    }

   
}