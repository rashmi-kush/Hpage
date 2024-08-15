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
    public partial class Payment_Details : System.Web.UI.Page
    {
        PaymentDetails_BAL PaymentDetails_BAL = new PaymentDetails_BAL();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        private static string Endorsement_URL = ConfigurationManager.AppSettings["EndorsementURL"];
        string Endorsement_Id = ConfigurationManager.AppSettings["EndorsementID"];
        string Endorsement_Key = ConfigurationManager.AppSettings["EndorsementKey"];


        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];

        string FileName = string.Empty;
        public byte[] pdfBytes;

        string appid;
        string Appno;
        string caseno;
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
            lblHeadingDist.Text = Session["District_NameHI"].ToString();
            lblHeadingDist1.Text = Session["District_NameHI"].ToString();
            lblHeadingDist2.Text = Session["District_NameHI"].ToString();
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
                    
                  

                    if (Request.QueryString["Flag"] != null)
                    {
                        Flag = Convert.ToInt32(Request.QueryString["Flag"]);
                        if (Request.QueryString["Flag"].ToString() == "1")// Success eSign
                        {
                            
                            if (Request.QueryString["Response_type"] != null)
                            {
                                if (Request.QueryString["Response_type"].ToString() == "Payment_Proceeding")
                                {
                                    DataTable dt = PaymentDetails_BAL.InserteSignDSC_Status_Payment(Convert.ToInt32(Session["AppID"].ToString()), "1", "", GetLocalIPAddress());
                                    btnSavePayment.Visible = false;
                                    btnendorsement.Visible = true;
                                    pnlBtnEndo.Visible = true;
                                    A1.Visible = false;

                                }
                            }
                        }
                        else if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "Payment_Proceeding") //faild Notice eSign
                        {
                            //Flag = 3;
                            if (Session["FileNameUnSignedPDF"] == null)
                            {

                                pnlEsignDSC.Visible = true;

                            }
                            else if (Session["FileNameUnSignedPDF"].ToString() == "")
                            {

                               
                                pnlEsignDSC.Visible = false;

                            }
                            else
                            {
                                
                            }


                        }
                    }
                    



                    if (Session["AppID"] != null)
                    {
                        //Session["AppID"] = (Request.QueryString["AppId"].ToString());
                        //Session["Appno"] = (Request.QueryString["AppNo"].ToString());
                        //Session["CaseNo"] = (Request.QueryString["CaseNo"].ToString());
                        ViewState["AppID"] = Session["AppID"];
                        ViewState["Appno"] = Session["Appno"];
                        ViewState["CaseNo"] = Session["CaseNo"];//Session["CaseNo"] = (Request.QueryString["CaseNo"].ToString());

                        appid = Session["AppID"].ToString();
                        caseno = Session["CaseNo"].ToString();

                        lblProposalIdHeading.Text = Session["Appno"].ToString();
                        lblCase_Number.Text = Session["CaseNo"].ToString();

                        summernote.Value = lblHeadingDist1.Text + " द्वारा एक पंजीकृत दस्तावेज दान पत्र विलेख क्रमांक: " + Session["Appno"].ToString() + "  को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";

                        lblCNo.Text = Session["CaseNo"].ToString();
                        lblCaseNumber.Text = Session["CaseNo"].ToString();
                        pContent.InnerText = summernote.Value;

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;
                        AllDocList(Convert.ToInt32(Session["AppID"].ToString()));


                        int Appid = Convert.ToInt32(Session["AppID"].ToString());

                        BindCaseList(Appid);

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
                        dsDocDetails = clsHearingBAL.GetNoticeProceeding_Payment(Convert.ToInt32(appid));
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




                        if (Flag == 1)
                        {
  
                            GetPaymentProceeding();

                        }

                    }

                   

                    else
                    {

                    }

                    DataTable dtDeatil = clsHearingBAL.GET_ALL_CMS_COS_PAYMENT_DETAIL(Convert.ToInt32(Session["AppID"].ToString()));

                    if (dtDeatil.Rows.Count > 0)
                    {
                        A1.InnerHtml = "Edit Proceeding";
                        summernote.Value = dtDeatil.Rows[0]["PAYMENT_PROCEEDING"].ToString();

                        string path= dtDeatil.Rows[0]["payment_ordersheet_path"].ToString();
                        string file = Path.GetFileNameWithoutExtension(path);
                        string NewPath = path.Replace(file, file + "_Signed");
                        if (File.Exists(Server.MapPath(NewPath)))
                        {
                            Session["RecentSheetPath"] = NewPath;
                            btnSavePayment.Visible = false;
                            btnendorsement.Visible = true;
                            pnlBtnEndo.Visible = true;
                            A1.Visible = false;
                            pnl_Proceding.Visible = false;
                            custom_tabs_one_Display.Visible = true;
                            custom_tabs_one_Display.Attributes.Add("Class", "active");
                            docPath.Src = Session["RecentSheetPath"].ToString();
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "script", "AddOrdersheet();", true);
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "AddOrdersheet()", true);

                            //Console.WriteLine(NewPath); //photo\myFolder\image-resize.jpg

                        }

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


                        lblRegisteredDate.Text = dsPaymentDetail.Rows[0]["CASE_ACTIONDATE"].ToString();
                        ViewState["eregid"] = dsPaymentDetail.Rows[0]["ereg_id"].ToString();
                        Session["Reason"] = "COS Certificate - Order passed in Case Number " + Session["CaseNo"].ToString() + ", Date " + Session["FinalOrderdate"].ToString() + ", Ordered amount " + dsPaymentDetail.Rows[0]["FINAL_PAYABLE_AMOUNT"].ToString() + ", Challan " + dsPaymentDetail.Rows[0]["urn"].ToString() + ", Dated " + dsPaymentDetail.Rows[0]["payment_date"].ToString() + ", Document as stamped by Collector of Stamp.";
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


        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            try
            {

                int App_ID = Convert.ToInt32(Session["AppID"].ToString());
                //int Notice_id = Convert.ToInt32(Session["Notice_ID"].ToString());

                if (ddl_SignOption.SelectedValue == "1")
                {
                    if (TxtLast4Digit.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        TxtLast4Digit.Focus();
                        return;
                    }
                    //-------eSign Start------------------------

                    //string Location = "Project Office -" + HF_Office.Value;
                    string Location = "Bhopal";
                    string PdfName = "";
                    //string ApplicationNo = hdnProposal.Value;
                    if (Session["FileNameUnSignedPDF"] != null)
                    {
                        PdfName = Session["FileNameUnSignedPDF"].ToString();
                    }

                    PdfName = PdfName.Replace("~/CoS_Payment/", "");
                    ViewState["filename"] = PdfName;

                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/CoS_Payment/" + PdfName.ToString());
                    string flSourceFile = FileNamefmFolder;

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
                                Session["AppID"] = ViewState["AppID"];
                                Session["Appno"] = ViewState["Appno"];
                                Session["CaseNo"] = ViewState["CaseNo"];
                               
                               
                                    DataTable dsStatusDetail = new DataTable();
                                    dsStatusDetail = objSeekReport.UpdateNoticeSend_Status(App_ID, 54);


                                
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Appno=" + Session["Appno"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"]);
                                ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_PaymentProceeding.aspx?" + "&Response_type=PaymentProceeding");


                                //getdata();
                                //DataTable dt = PaymentDetails_BAL.InserteSignDSC_Status_PaymentProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress());

                                AuthMode authMode = AuthMode.OTP;

                                eSigner.eSigner _esigner = new eSigner.eSigner();

                                _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                                //getdata_Esign();

                                pnlEsignDSC.Visible = false;
                                pnlBtnEndo.Visible = true;

                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage_esign();", true);

                            }
                            else
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' pdf फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);
                            }

                        }

                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                    }

                    //-------eSign End------------------------

                }

                if (ddl_SignOption.SelectedValue == "2")
                {
                    //TxtLast4Digit.Visible = false;


                    string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                    PdfName = PdfName.Replace("~/OrderSheet/", "");
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + PdfName.ToString());
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
                        //hsmMsg.Text= hSMSigner.hsm_DSC();
                        //Session["HSM_DSC"] = hsmMsg.Text;
                        if (File.Exists(NewPath))
                        {
                            Session["RecentSheetPath"] = NewPath;


                            int Flag = 1;
                            string resp_status = 1.ToString();
                            string url = "Notice.aspx?Case_Number=" + Session["CaseNum"].ToString() + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag + "&Response_Status=" + resp_status;

                            //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


                            ///ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Signed ordersheet saved Successfully');window.location='" + url + "';", true);
                            ///
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Signed ordersheet saved Successfully','success');window.location='" + url + "';", true);

                            //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Signed ordersheet saved Successfully', 'success');window.location='" + url + "'", true);

                        }

                        //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "2", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                    }



                    //eSigner.eSigner _esigner = new eSigner.eSigner();

                    //if (TxtLast4Digit.Text.Length != 4)
                    //{
                    //    TxtLast4Digit.Focus();
                    //    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                    //    return;
                    //}

                    //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), order_id);


                }


                else
                {
                    string eSignDSCMessage = "Please select eSign or DSC in dropdown";
                    string Title = "Success";
                    //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + eSignDSCMessage + "','success');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                    return;

                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Select eSign or DSC in Dropdown', '', 'error')", true);
                }



                //-------eSign End------------------------



            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' eSign फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);

            }


        }

        protected void ConsumeEndorsementAPI(string deptUserId, string eregId, string remarks)
        {
            try
            {
                ReqJsonEndorsement reqJson = new ReqJsonEndorsement();

                reqJson.deptUserId = Convert.ToInt32(deptUserId);
                reqJson.eregId = Convert.ToInt32(eregId);
                reqJson.remarks = remarks;

                HttpWebRequest _req = (HttpWebRequest)HttpWebRequest.Create(Endorsement_URL);
                _req.ContentType = "application/json; charset=utf-8";
                _req.Headers.Add("Authorization", Session["Token"].ToString());
                _req.Headers.Add("x-parse-application-id", Endorsement_Id); 
                _req.Headers.Add("x-parse-rest-api-key", Endorsement_Key);
                //_req.Headers.Add("Content-Type", "application/json");
                _req.ReadWriteTimeout = 10000;
                _req.Method = "POST";
                string Json = JsonConvert.SerializeObject(reqJson);
                byte[] _postBytes = Encoding.UTF8.GetBytes(Json);
                _req.ContentLength = _postBytes.Length;

                //Response.Write("Json " + Json);


                //System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls12;

                using (Stream _reqStream = _req.GetRequestStream())
                {

                    _reqStream.Write(_postBytes, 0, _postBytes.Length);

                }

                //Response.Write("post success " + _postBytes.Length);
                string Message = string.Empty;

                using (HttpWebResponse _resp = (HttpWebResponse)_req.GetResponse())
                {
                    using (Stream _responseStream = _resp.GetResponseStream())
                    {
                        using (StreamReader _responseStreamReader = new StreamReader(_responseStream, Encoding.UTF8))
                        {
                            string _response = _responseStreamReader.ReadToEnd();


                            if (!string.IsNullOrEmpty(_response))
                            {
                                ResJsonEndorsement resonse = JsonConvert.DeserializeObject<ResJsonEndorsement>(_response);


                                if (resonse.responseData == true)
                                {
                                    Message = resonse.responseMessageEn;

                                    DataTable dtUp = PaymentDetails_BAL.Insert_Endorsement_details(remarks, Session["DROID"].ToString(), GetLocalIPAddress(), Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(eregId), Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(deptUserId));
                                    DataTable DtIgrs = PaymentDetails_BAL.UpdateCOS_PaymentStatus_IGRS(71,101, Convert.ToInt32(ViewState["AppID"].ToString()));////Update status for update sampada core application tables after endorsement 
                                    if (dtUp.Rows.Count > 0)
                                    {
                                        //
                                    }
                                    hdnmsg.Value = Message;
                                }
                                else if (resonse.responseData == false)
                                {
                                    Message = resonse.responseMessageEn;
                                    hdnmsg.Value = Message;

                                }



                            }
                            else
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage_Endorsement();", true);
                            }
                        }
                    }
                }



                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage_Endorsement();", true);
            }
            catch (Exception ex)
            {
                hdnmsg.Value = ex.Message+" Try again for endrsement";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage_Endorsement();", true);
            }
        }


        protected void btnClose_Click(object sender, EventArgs e)
        {

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {

        }
        private void SaveOrderSheetPDF()
        {

            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                string divCon = summernote.Value;

                StringBuilder stringBuilder = new StringBuilder();


                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; text-align: center; padding: 20px 30px 30px 30px; height:1350px; position:relative;top:0px;'>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>प्रारूप-अ</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>राजस्व आदेशपत्र</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>कलेक्टर ऑफ़ स्टाम्प, " + lblHeadingDist.Text + " के न्यायालय में मामला क्रमांक-" + lblCaseNumber.Text + "</h2> ");
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
                stringBuilder.Append("<p style='text-align: justify;'>" + divCon + "</p>");
                //stringBuilder.Append("<p style='text - align: left;'>" + lblSeekContent.Text + "</p>");
                stringBuilder.Append("<div>");


                stringBuilder.Append("</b>");
                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<table border='1' style='width: 940px;border: 1px solid black; border-collapse: collapse;' ><tr><td>भुगतान की राशि</td><td>भुगतान की गई राशि</td><td>भुगतान की तिथि</td></tr>");
                stringBuilder.Append("<tr><td>"+ txtFinalAmount.Text + "</td><td>"+ txtPaidAmount.Text + "</td><td>"+ txtPaidDate.Text + "</td></tr>");
                stringBuilder.Append("<tr><td>यू आर एन नंबर</td><td>भुगतान की स्थिति</td><td>राशि का प्रकार</td></tr>");
                stringBuilder.Append("<tr><td>"+ txtURN.Text + "</td><td>"+ txtPaymentStatus.Text + "</td><td>"+ txtAmountType.Text + "</td></tr>");
                stringBuilder.Append("<tr><td colspan='3'>उद्देश्य</td></tr>");
                stringBuilder.Append("<tr><td colspan='3'>" + txtPurpose.Text + "</td></tr></table>");
                //stringBuilder.Append(DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                stringBuilder.Append("<p>&nbsp;</p>");
                stringBuilder.Append("<p>&nbsp;</p>");
                stringBuilder.Append("<p>&nbsp;</p>");
                stringBuilder.Append("<p>&nbsp;</p>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 100px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><</b>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 150px;left:130px;'>कलेक्टर ऑफ़ स्टाम्प्स,<br/>"+ lblHeadingDist.Text + " <br/><br/> </b>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                // stringBuilder.Append("</div>");



                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_PaymentOrderSheet.pdf";
                ViewState["ActualPath"] = FileNme;
                ViewState["ActualPath"] = ConvertHTMToPDF(FileNme, "~/CoS_Payment/", stringBuilder.ToString());
                Session["ActualPath"] = "~/CoS_Payment/" + FileNme;
                Session["FileNameUnSignedPDF"] = "~/CoS_Payment/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                SaveOrderSheet("~/CoS_Payment/" + FileNme);


            }
            catch (Exception ex)
            {

            }

        }

        private void SaveOrderSheet(string Path)
        {
            int AppId = Convert.ToInt32(Session["AppID"].ToString());
            string Appnum = Session["Appno"].ToString();
            string casenum = Session["CaseNo"].ToString();

            try
            {

                string FileName = "PaymentOrderSheet_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                ViewState["ActualPath"] = Path;


                DataSet dtUp = PaymentDetails_BAL.InsertPaymentOrderSheet(AppId, Appnum, casenum, ViewState["ActualPath"].ToString(), summernote.Value, "", GetLocalIPAddress());

                if (dtUp.Tables.Count > 0)
                {
                    DataTable dsStatusDetail = new DataTable();
                    dsStatusDetail = objSeekReport.UpdateNoticeSend_Status(AppId, 53);


                }
                pnlEsignDSC.Visible = true;
                btnSavePayment.Visible = false;
                pnl_Proceding.Visible = false;
                custom_tabs_one_Display.Visible = true;
                custom_tabs_one_Display.Attributes.Add("Class", "active");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowMessageScript", "ShowMessage111();", true);
                


            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
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

        protected void btnSavePayment_Click(object sender, EventArgs e)
        {
            SaveOrderSheetPDF();
            //GetPaymentProceeding();




        }

        protected void btnendorsement_Click(object sender, EventArgs e)
        {
            //
            ConsumeEndorsementAPI(Session["DRID"].ToString(), ViewState["eregid"].ToString(), Session["Reason"].ToString());
        }

        public void GetPaymentProceeding()
        {

            //try
            //{
            //    DataSet dsPayment = new DataSet();
            //    dsPayment = objFinalOrder.GetPaymentProceeding(Session["CaseNo"].ToString());
            //    if (dsPayment != null)
            //    {
            //        if (dsPayment.Tables.Count > 0)
            //        {

            //            if (dsPayment.Tables[0].Rows.Count > 0)
            //            {

            //                pnlPaymentProceeding.Visible = true;
            //                RptPaymentProceeding.DataSource = dsPayment;
            //                RptPaymentProceeding.DataBind();

            //                foreach (RepeaterItem item in RptPaymentProceeding.Items)
            //                {
            //                    Label lab = item.FindControl("lblDRofficeHPay") as Label;


            //                    //Label lab2 = item.FindControl("lblDRoffice5") as Label;
            //                    if (Session["District_NameHI"] != null)
            //                    {
            //                        lab.Text = Session["District_NameHI"].ToString();
            //                        //lab2.Text = Session["District_NameHI"].ToString();
            //                    }
            //                    else
            //                    {
            //                        lab.Text = "भोपाल";
            //                        //lab2.Text = "भोपाल";
            //                    }

            //                }



            //            }
            //        }
            //    }

            //}
            //catch (Exception ex)
            //{

            //}
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
    }
    public class ResonseAN
    {
        public bool responseData { get; set; }
        public string responseStatus { get; set; }
        public string responseMessageEn { get; set; }
        public string responseMessageHi { get; set; }
        public string responseMessage { get; set; }
    }


    public class ReqJsonEndorsement
    {
        
        public int deptUserId { get; set; }
        public int eregId { get; set; }
        public string remarks { get; set; }
    }

    public class ResJsonEndorsement
    {
        public bool responseData { get; set; }
        public string responseStatus { get; set; }
        public string responseMessageEn { get; set; }
        public string responseMessageHi { get; set; }
        public string responseMessage { get; set; }

    }
}