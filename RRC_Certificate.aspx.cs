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
using System.Web.Services;
using HSM_DSC;



namespace CMS_Sampada.CoS
{
    public partial class RRC_Certificate : System.Web.UI.Page


    {
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        string FileName = string.Empty;
        public byte[] pdfBytes;

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];

        string appid;
        string Appno;
        Encrypt Encrypt = new Encrypt();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        RRC_Certificate_Bal clsRRC_CertificateBAL = new RRC_Certificate_Bal();

        string All_RRC_CertiSheetFileNme = "";

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                hdnSRODistNameHi.Value = Session["District_NameHI"].ToString();
                hdnCOSDistNameHi.Value = Session["District_NameHI"].ToString();
                hdnSRONameHi.Value = Session["officeAddress"].ToString();
                hdnCOSNameHi.Value = Session["officeAddress"].ToString();

                Session["Case_Status"] = "";
                try
                {
                    int Flag = 0;

                    if (Request.QueryString["Flag"] != null)
                    {
                        Flag = Convert.ToInt32(Request.QueryString["Flag"]);
                        if (Request.QueryString["Flag"].ToString() == "1")// Success eSign
                        {
                            if (Request.QueryString["Response_From"] != null)
                            {
                                if (Request.QueryString["Response_From"].ToString() == "RRC_Certificate_Proceeding")
                                {

                                    DataTable dt2 = clsRRC_CertificateBAL.InserteSignDSC_Status_RRC_CertiProceeding(Convert.ToInt32(Session["AppId"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["RRC_CertiProced_id"].ToString()));

                                }

                            }

                        }

                    }

                    DataTable dt1 = clsRRC_CertificateBAL.Get_RRC_CertificateCases();
                    if (new[] { 95,96}.Contains(Convert.ToInt32(dt1.Rows[0]["STATUS_ID"])))
                    {
                        //
                    }

                    else if (dt1.Rows[0]["STATUS_ID"].ToString() == "89") //Draft proceeding completed
                    {
                        Flag = 1; // for final submit
                        DataTable dt2 = clsRRC_CertificateBAL.Get_RRC_Certificate_Details(Convert.ToInt32(Session["AppId"].ToString()));
                        if (dt2.Rows.Count > 0)
                        {
                            summernote.Value = dt2.Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                            //Session["RRC_CertificateContent"]= dt2.Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        }

                    }
                    else if (dt1.Rows[0]["STATUS_ID"].ToString() == "97") //final submit proceeding completed
                    {

                        Flag = 2; // for esign
                        DataTable dt3 = clsRRC_CertificateBAL.Get_RRC_Certificate_Details(Convert.ToInt32(Session["AppId"].ToString()));
                        if (dt3.Rows.Count > 0)
                        {
                            summernote.Value = dt3.Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                            Session["RRC_CERT_DOCSPATH"] = dt3.Rows[0]["RRC_CERT_DOCSPATH"].ToString();
                         
                        }
                    }
                    if (Session["AppId"].ToString()!= null)
                    {

                        int AppID = Convert.ToInt32(Session["AppId"].ToString());

                        Session["CaseRegDate"] = Session["InsertedDate"].ToString();

                        DataTable dt = clsRRC_CertificateBAL.Get_RRC_Certificate_Details(AppID);
                        if (dt.Rows.Count > 0)
                        {
                            string RRC_CERT_id = dt.Rows[0]["RRC_CERT_ID"].ToString();
                            Session["RRC_CertificateReader_id"] = dt.Rows[0]["RRC_CERT_ID"].ToString();

                            Session["RRC_CertificateContent_Reader"] = dt.Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                            Session["RRC_CertificateContent"] = dt.Rows[0]["RRC_CERT_PROCEEDING"].ToString();

                        }

                        else
                        {
                            Session["RRC_CertificateContent"] = summernote.InnerHtml;
                        }

                    }

                    else
                    {

                        
                        summernote.InnerHtml = "";
                        Session["RRC_CertificateContent"] = summernote.InnerHtml;
                    }


                    if (Session["CaseNo"].ToString() != null)
                    {


                        if (Session["Appno"].ToString() != null && Session["AppId"].ToString() != null)
                        {
                            Session["ProposalID"] = Session["Appno"].ToString();

                            lblProposalIdHeading.Text = Session["ProposalID"].ToString();
                            lblCase_Number.Text = Session["CaseNo"].ToString();
                            lblRegisteredDate.Text= Session["CaseRegDate"].ToString();
                            appid = Session["AppId"].ToString();
                            string RRC_CERTseet = Session["RRC_CertificateContent"].ToString();
                            
                            Appno = Session["Appno"].ToString();
                        }
                        else
                        {
                            lblProposalIdHeading.Text = Session["ProposalID"].ToString();
                            appid = Session["AppId"].ToString();
                            Appno = Session["Appno"].ToString();
                        }
                        string CaseNumber = Session["CaseNo"].ToString();
                        ViewState["Case_Number"] = CaseNumber;

                        lblCase_Number.Text = CaseNumber;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");

                        DataSet dsDocDetails = new DataSet();
                        dsDocDetails = clsRRC_CertificateBAL.GetRRC_CertiProced_Doc(CaseNumber, appid);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    string fileName = dsDocDetails.Tables[0].Rows[0]["RRC_CERT_ORDERSHEET_PATH"].ToString();
                                    Session["RecentSheetPath"] = fileName.ToString();
                                    docPath.Src = fileName;
                                }
                            }
                        }
                        dsDocDetails = OrderSheet_BAL.GetProposal_Doc(CaseNumber, appid);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    //grdProposalDoc.DataSource = dsDocDetails;
                                    //grdProposalDoc.DataBind();


                                }
                            }
                        }

                        dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS_ToC(Convert.ToInt32(appid), Appno);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    //grdSRDoc.DataSource = dsDocDetails;
                                    //grdSRDoc.DataBind();

                                }

                            }
                        }

                        //dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS(Convert.ToInt32(appid), Appno);
                        dsDocDetails = clsRRC_CertificateBAL.GetRRC_CertiProced_Doc(CaseNumber, appid);
                        //dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS(Convert.ToInt32(appid), Appno);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {

                                    PreviousDoc.DataSource = dsDocDetails.Tables[0];
                                    PreviousDoc.DataBind();


                                }
                            }
                        }

                        dsDocDetails = OrderSheet_BAL.GetRegisteredDate(CaseNumber);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    string RegisteredDate = dsDocDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                                    lblRegisteredDate.Text = RegisteredDate;
                                }
                            }
                        }

                        GetPartyDetail();

                        Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                        All_RRC_CertiSheetFileNme = Session["All_DocSheet"].ToString();

                        CreateEmptyFile(All_RRC_CertiSheetFileNme);
                        CraetSourceFile(Convert.ToInt32(appid));
                        AllDocList(Convert.ToInt32(appid));

                        if (Flag == 1)   // for final submit
                        {
                            
                            pnlDraft.Visible = true;
                            pnlBtnSave.Visible = false;
                            pnlEsignDSC.Visible = false;
                            pContent.InnerHtml = "9. " + Session["RRC_CertificateContent"].ToString();
                            string script1 = "$('#custom_tabs_one_Display').addClass('active');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab1", script1, true);
                            custom_tabs_one_profile_tab.Style.Add("display", "block");
                            string script2 = "$('#custom_tabs_one_profile_tab').removeClass('active show');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab2", script2, true);

                        }
                        else if (Flag == 2) // for esign
                        {
                            
                            pnlDraft.Visible = false;
                            pnlBtnSave.Visible = false;
                            pnlEsignDSC.Visible = true;
                            string script1 = "$('#custom_tabs_one_Display').addClass('active');";
                            ClientScript.RegisterStartupScript(this.GetType(), "ActivateTab1", script1, true);
                            custom_tabs_one_profile_tab.Style.Add("display", "block");
                            string script2 = "$('#custom_tabs_one_profile_tab').removeClass('active show');" +
                                "$('a[href=\"#custom-tabs-one-RegisteredForm\"]').addClass('disabled').attr('aria-disabled', 'true');";
                            ClientScript.RegisterStartupScript(this.GetType(), "DeactivateTab2", script2, true);
                            string FileNme = "";
                            if (Session["RRC_CERT_DOCSPATH"] != null)
                            {
                                FileNme = Session["RRC_CERT_DOCSPATH"].ToString();
                                Session["ActualPath"] = FileNme;
                            }
                        }
                        else
                        {

                        }
                    }


                    //1
                    if (Session["RRC_CertificateContent_Reader"] != null && Session["SubRegistrarOffice"] != null)
                    {

                        string OrdersheetContent_Reader = Session["RRC_CertificateContent_Reader"].ToString();

                        summernote.InnerHtml = Session["RRC_CertificateContent_Reader"].ToString() + "<br/><br/>" + "<p>उप पंजीयक " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p> प्राप्त किया जाये।";

                        pnlBtnSave.Visible = true;

                        Session["RRC_CertificateContent"] = null;


                        Session["SubRegistrarOffice"] = null;
                    }

                    //2
                    if (Session["RRC_CertificateContent"] != null && Session["SubRegistrarOffice"] != null)
                    {

                        summernote.InnerHtml = Session["RRC_CertificateContent"].ToString() + "<br/><br/>" + "<p>उप पंजीयक " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p> प्राप्त किया जाये।";

                        Session["RRC_CertificateContent"] = null;


                        Session["SubRegistrarOffice"] = null;
                    }




                }
                catch (Exception ex)
                {

                    throw;
                }

            }
        }
        public void AllDocList(int APP_ID)
        {
            try
            {
                
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, Appno);
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
                Response.Write("Error: " + ex.Message);
            }

        }
        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_RRC_CertificateAllSheetDoc/" + All_RRC_CertiSheetFileNme;

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppId"].ToString()), Appno);

                //if (dtDocProDetails.Rows.Count > 0)
                //{
                //    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //    {
                //        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //    }


                //}
            }
            DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppId"].ToString()), Appno);
            string DocType = "";
            string PDFPath = "";
            RecentAttachedDoc.Visible = false;
            RecentdocPath.Visible = false;
            ifProposal1.Visible = false;
            if (dsIndexDetails.Tables[0].Rows.Count > 0)
            {
                for (int i = 0; i < dsIndexDetails.Tables[0].Rows.Count; i++)
                {
                    PDFPath = dsIndexDetails.Tables[0].Rows[i]["FILE_PATH"].ToString();
                    DocType = dsIndexDetails.Tables[0].Rows[i]["docType"].ToString();
                    if (DocType == "RRC-CP")
                    {
                        if (PDFPath != "")
                        {
                            RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + PDFPath;
                            RecentdocPath.Visible = true;
                        }
                    }
        
                }

            }



        }
        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoS_RRC_CertificateAllSheetDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoS_RRC_CertificateAllSheetDoc/", "<p>RRC_Certificate</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoS_RRC_CertificateAllSheetDoc/", "<p>RRC_Certificate</p>");
            }
            ViewState["ALLDocCAddedPDFPath"] = "~/CoS_RRC_CertificateAllSheetDoc/" + filename;
            ViewState["CoS_RRC_CertificateAllSheetDoc"] = serverpath;
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
                    if (fileName != null)
                    {
                        // Create a PDFreader for a certain PDFdocument  
                        if (File.Exists(fileName))
                        {
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

                }
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }

        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dt = clsRRC_CertificateBAL.GetAllDoc(APP_ID);
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

                    string sourceFile = ViewState["CoS_RRC_CertificateAllSheetDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }


        protected void OnView(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Session["FileName"] = filePath;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }

        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlDocView.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "setTimeout(function () { window.scrollTo(0,document.body.scrollHeight); }, 25);", true);

        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked)
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_AllRRCDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["ALLDocCAddedPDFPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();

                }
                else if (chkRecentDoc.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentRRCDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(Session["RecentSheetPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void GetPartyDetail()
        {
            ViewState["App_ID"] = Session["AppId"].ToString();

            if (ViewState["App_ID"] != null)

            {
                DataTable dt1 = clsRRC_CertificateBAL.GetDeatil_RRC_CERTIFICATE(ViewState["App_ID"].ToString(), 1);

                if (dt1.Rows.Count > 0)
                {
                    lblCaseNo.Text = dt1.Rows[0]["case_number"].ToString();
                    lblDEMOGRAPHY_NAME.Text = dt1.Rows[0]["DEMOGRAPHY_NAME_EN"].ToString();
                    lblDistrict.Text = Session["District_NameHI"].ToString();
                }

                DataTable dt5 = clsRRC_CertificateBAL.GetDeatil_RRC_CERTIFICATE(ViewState["App_ID"].ToString(), 5);

                if (dt5.Rows.Count > 0)
                {
                    lblTodaydate.Text = ((DateTime)dt5.Rows[0]["sysdate"]).ToString("dd/MM/yyyy");
                    lblDistrictNAME_EN.Text = dt5.Rows[0]["NAME_EN"].ToString();
                }
                DataTable dsPartyDetails = clsRRC_CertificateBAL.GetDeatil_RRC_CERTIFICATE(ViewState["App_ID"].ToString(), 2);
                ViewState["PartyDetail_PDF"] = dsPartyDetails;
                if (dsPartyDetails.Rows.Count > 0)
                {
                    hdpartyList.Value = DataTableToJSONWithJavaScriptSerializer(dsPartyDetails);
                    if (dsPartyDetails.Rows.Count > 0)
                    {
                        grdlPartys.DataSource = dsPartyDetails;
                        grdlPartys.DataBind();
                        DataTable dt = dsPartyDetails;
                        StringBuilder html = new StringBuilder();
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                html.Append("<td style='border: 1px solid black; border - collapse: collapse; padding: 5px 15px;'>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                    }
                }

                DataTable dsPartyAmount = clsRRC_CertificateBAL.GetDeatil_RRC_CERTIFICATE(ViewState["App_ID"].ToString(), 3);
                ViewState["PartyAmount_PDF"] = dsPartyAmount;
                if (dsPartyAmount.Rows.Count > 0)
                {
                    hdpartyList.Value = DataTableToJSONWithJavaScriptSerializer(dsPartyAmount);
                    if (dsPartyAmount.Rows.Count > 0)
                    {
                        grdAmount.DataSource = dsPartyAmount;
                        grdAmount.DataBind();
                        DataTable dt = dsPartyAmount;
                        StringBuilder html = new StringBuilder();
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                html.Append("<td style='border: 1px solid black; border - collapse: collapse; padding: 5px 15px;'>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                    }
                }

                DataTable dsPropertyDetails = clsRRC_CertificateBAL.GetDeatil_RRC_CERTIFICATE(ViewState["App_ID"].ToString(), 4);
                ViewState["PropertyDetails_PDF"] = dsPropertyDetails;
                if (dsPropertyDetails.Rows.Count > 0)
                {
                    hdpartyList.Value = DataTableToJSONWithJavaScriptSerializer(dsPropertyDetails);
                    if (dsPropertyDetails.Rows.Count > 0)
                    {
                        grdPropertyDetails.DataSource = dsPropertyDetails;
                        grdPropertyDetails.DataBind();
                        DataTable dt = dsPropertyDetails;
                        StringBuilder html = new StringBuilder();
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                html.Append("<td style='border: 1px solid black; border - collapse: collapse; padding: 5px 15px;'>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                    }
                }






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



        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            foreach (DataRow row in table.Rows)
            {
                childRow = new Dictionary<string, object>();
                foreach (DataColumn col in table.Columns)
                {
                    childRow.Add(col.ColumnName, row[col]);
                }
                parentRow.Add(childRow);
            }
            return jsSerializer.Serialize(parentRow);
        }



        protected void btnSubmit_Click(object sender, EventArgs e)
        {


            SaveRRC_CertificatePDF();

            custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
            pnlEsignDSC.Visible = true;

            pnlBtnSave.Visible = true;

            pContent.InnerHtml = "9. " + ViewState["RRC_CertificateContent"].ToString();

        }

        public void generateFormateFirst_PDF()
        {
            int appid = Convert.ToInt32(Session["AppId"].ToString());
            string Appno = Session["Appno"].ToString();
            //string appPath = HttpContext.Current.Request.ApplicationPath;
            //string path = Server.MapPath(appPath + "/Proposal/"+ lblProposalIdHeading.Text+ DateTime.Now.ToString("ddMMyyyyhhmmss")+".pdf");
            StringWriter iSW = new StringWriter();
            HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
            //summernote.RenderControl(iHTW);
            //string divCon = summernote.Value;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div style='width: 90%; margin: 0 auto; text-align: center; padding: 40px 30px 30px 30px; margin-top: 0px;'>");
            stringBuilder.Append("<h2 style='font-size: 32px; margin: 0;'>");
            stringBuilder.Append("<label style='font: bold'>");
            stringBuilder.Append("</label>");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<h2 style='font-size: 24px; margin: 10px;'>Form-I ");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<h2 style='font-size: 22px; margin-top: 10px;'>[See rule 5 (1)] ");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<div class='section'>");
            stringBuilder.Append("<div class='point-1'>");
            stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>1. Name and Address of Executants ( Seller / Doner / Releasor / Leaser )</h2>");

            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            string Proposal_ID = Session["ProposalID"].ToString();

            string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + ".pdf";
            string ProposalSheetPath = Server.MapPath("~/Proposal/" + Proposal_ID);
            ViewState["FirstFormate_Path"] = "~/Proposal/" + Proposal_ID + "/" + FileNme;
            ViewState["SecondFormate_Path"] = "";

            string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, stringBuilder.ToString());


            //ViewState["FirstFormate_Path"] = ConvertHTMToPDF(FileNme, "~/Proposal/", stringBuilder.ToString());
            //ViewState["SecondFormate_Path"] = "";




            //DataTable dtUp = objClsNewApplication.InsertProposalSheetPath(Convert.ToInt32(appid), ViewState["FirstFormate_Path"].ToString(), ViewState["SecondFormate_Path"].ToString(), caseno, "Proposal Copy");


        }

        private void SaveRRC_CertificatePDF()
        {
            try
            {
                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");

                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);


                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<div class='main-box htmldoc' style='overflow: auto; width: 100 %; margin: 0 auto; text - align: center; padding: 15px 30px 0px 30px; margin: 5px 0 0px 0px;'> ");

                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; padding: 6px 0px '>प्रारूप-19</h2>");
                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; '>(नियम 14 देखिए ) </h2>");
                stringBuilder.Append("<h3 style='font-size: 16px; font-family: fantasy; padding: 6px 0 0 0 '>मध्यप्रदेश भू –राजस्व संहिता ( भू –राजस्व की उगाही ) नियम, 2020 </h3>");
                stringBuilder.Append("<h3 style='font-size: 16px; font-family: fantasy; line-height: 27px;'>राशियों की भू –राजस्व के बकाया के तौर पर वसूली के लिए आवेदन पत्र <br>[मध्यप्रदेश भू –राजस्व संहिता, 1959 की धारा 155 के अधीन]<br>प्रकरण क्रमांक नंबर: " + lblCase_Number.Text + "   </h3>");
                stringBuilder.Append("<div style='float: left; text-align: justify; align-items: baseline; '>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0' >प्रति ,<br>अतिरिक्त तहसीलदार / जिला पंजीयक <br> " + lblDEMOGRAPHY_NAME.Text + " </p> ");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'>यह विनिश्चित किया गया है कि निम्नलिखित राशियों को , जिसका विवरण नीचे दिया गया है , भू –राजस्व के बकाया तौर पर वसूल किया जाना चाहिए । कृपया इन्हें भू –राजस्व के बकाया के तौर पर वसूल करैं तथा नीचे मद क्रमांक 7 में वर्णित लेखा शीर्ष में जमा करें :- </p>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style = 'loat: left; text-align: justify; margin-bottom: 10px;'>1.	विभाग /समक्ष प्राधिकारी का नाम जिसे कि राशि शोध्य है - पंजीयन विभाग <br>2.	उस व्यक्ति का नाम, पिता का नाम तथा पूरा पता जिससे कि राशियां शोध्य है ।<br>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");



                stringBuilder.Append("<table width='100%' cellspacing='0' cellpadding='5' border='1' border-spacing='0' style='margin: 0; padding: 0; '><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>क्रमांक </th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>पार्टी का नाम </th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>पिता का नाम</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>पता </th></tr>");
                int srno = 1;
                for (int i = 0; i < ((DataTable)ViewState["PartyDetail_PDF"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '><b>" + srno + "<b></td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["Party_Name"] + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["PARTYFATHER_ORHUSBAND_ORGUARDIANNAME"] + "</td></tr>" + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["PARTYFATHER_ORHUSBAND_ORGUARDIANNAME"] + "</td></tr>" + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["Party_Address"] + "</td></tr>");
                    srno++;
                }
                stringBuilder.Append("</table>");

                stringBuilder.Append(" <div>");
                stringBuilder.Append("<p style='float: left; text-align: justify;'><b> 3.	शोध्य राशियों का विवरण <br></p>");
                stringBuilder.Append("</div>");


                stringBuilder.Append("<div style='float: left; text-align: justify; margin-bottom: 12px;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0; '>4.	विधि का वह उपबंध जिसके अधीन राशियां भू -राजस्व के बकाया के तौर पर वसूली योग्य हैं ।" +
                    "<br>[भारती स्टाम्प अधिनियम 1899 की धारा 48 शुल्कों ओर सस्ती की वसूली के प्रावधान के अनुसार ]<br>" +
                    " 5.	वह आदेशिका जिसके द्वारा राशि वसूल की जा सकेगी ।<br>" +
                    "[मध्यप्रदेश भू –राजस्व संहिता, 1959 एवं मध्यप्रदेश भू –राजस्व संहिता ( भू –राजस्व की उगाही ) नियम, 2020 के प्रावधान के अनुसार ]<br>" +
                    " 6.	उस संपत्ति का विवरण जिसके विरुद्ध आदेशिका निष्पादित की का सकेगी । <br> </p> ");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<table width='100%' cellspacing='0' cellpadding='5' border='1' border-spacing='0' style='margin: 0; padding: 0; '><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>क्रमांक </th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>कमी मुद्रांक शुल्क </th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>अर्थ दंड </th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>कुल राशि</th></tr>");
                int sno = 1;
                for (int i = 0; i < ((DataTable)ViewState["PartyAmount_PDF"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '><b>" + sno + "<b></td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyAmount_PDF"]).Rows[i]["TOTAL_DEFICIT_AMOUNT"] + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyAmount_PDF"]).Rows[i]["TOTAL_PENALTY_AMOUNT"] + "</td></tr>" + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PartyAmount_PDF"]).Rows[i]["FINAL_PAYABLE_AMOUNT"]);
                    sno++;
                }
                stringBuilder.Append("</table>");


                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'><b> बाज़ार मूल्य अवधारण / मुद्रांक शुल्क निर्धारण</b></p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-left: 50px; margin-top: 40px; margin-bottom: 25px;'> <b> पक्षकारों के नाम</b> </p>");
                stringBuilder.Append("<div>");

                stringBuilder.Append("<div class='m-top'>");
                stringBuilder.Append("<p style='font-size: 18px;text-align: center; margin: 0; margin-top: 18px; margin-bottom: 10px; '><b> विरुद्ध </b></p>");
                stringBuilder.Append("<div style='float: left;'>");
                stringBuilder.Append("<p style='font-size: 20px; line-height: 30px; text-align: center; margin: 0; margin-right: 50px;'> <b> अनावेदक : </b></p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<table  style='width: 940px;border: 1px solid black; border-collapse: collapse;'>");
                stringBuilder.Append("<tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>क्रमांक</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '></th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पता</th></tr>");
                int sro = 1;
                for (int i = 0; i < ((DataTable)ViewState["PropertyDetails_PDF"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + sro + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PropertyDetails_PDF"]).Rows[i]["PROPERTY_GIVEN_ID"] + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PropertyDetails_PDF"]).Rows[i]["INITIATION_ID"] + "</td></tr>" + "</td></tr>" + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PropertyDetails_PDF"]).Rows[i]["PROPERTY_TYPE"] + "</td></tr>" + "</td></tr>" + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["PropertyDetails_PDF"]).Rows[i]["PROPERTY_AREA"] + "</td></tr>");
                    sro++;
                }
                stringBuilder.Append("</table>");

                stringBuilder.Append("<div>");

                stringBuilder.Append("<p style = 'float: left; text-align: justify;' > 7.	वह लेखा शीर्ष जिसमें वसूली के पश्चात धनराशि जमा की जाएगी । <br>A76<br> 8.	क्या धार 155 के खण्ड  (घ), (ई), (एक), (जी) अथवा (ज) के अधीन यथास्थिति अपेक्षित प्रमाण -पत्र कुर्की किए गई हैं ?<br>[हा] </p>");
                stringBuilder.Append("<p style='text-align: justify;'>9. ");


                stringBuilder.Append(dfd);
                stringBuilder.Append("</p>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='width:100%; margin-top:50px'>");


                stringBuilder.Append("<b style='top:120px;float:left; text-align:left; padding-left:5px;'>  दिनांक - " + lblTodaydate + "</b>");

                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<div  width='50%' style='text-align:right'>");

                stringBuilder.Append("<b style='padding: 2px 0 5px 0;'>अतिरिक्त तहसीलदार एवं <br/> जिला पंजीयक<br/> " + lblDistrictNAME_EN + " (म0प्र0) </b><br/>");


                stringBuilder.Append("</div>");

                stringBuilder.Append("</div>");


                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_RRC_Certificate.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/Cos_RRC_Certificate/", stringBuilder.ToString());
                Session["RecentSheetPath"] = "~/Cos_RRC_Certificate/" + FileNme;
                Session["ActualPath"] = "~/Cos_RRC_Certificate/" + FileNme;


                SaveRRC_Certificate("~/Cos_RRC_Certificate/" + FileNme);



            }
            catch (Exception ex)
            {

            }

        }

        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                string FileName = FileNme;
                string RRC_CertificatePath = Server.MapPath(path);
                if (!Directory.Exists(RRC_CertificatePath))
                {
                    Directory.CreateDirectory(RRC_CertificatePath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = RRC_CertificatePath;
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

                using (var stream = File.Create(Path.Combine(RRC_CertificatePath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return RRC_CertificatePath + "/" + FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }

        protected void btnDraftSave_Click(object sender, EventArgs e)
        {

            try
            {
                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");
                GetLocalIPAddress();

                DataTable dt = clsRRC_CertificateBAL.Get_RRC_Certificate_Details(Convert.ToInt32(Session["AppId"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    string RRC_CertiProced_id = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_CertiProcedReader_id"] = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_CertiProced_id"] = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_CertiProced_id_Status"] = dt.Rows[0]["status_id"].ToString();
                    //DataSet dtUp = OrderSheet_BAL.InsertIntoOrderSheet_Reader(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), "", PartyID, summernote.Value, "", "", Convert.ToInt32(Session["ordersheetReader_id"].ToString()));
                    DataSet dtUp = clsRRC_CertificateBAL.Update_RRC_Certificate_Detail_Reader(Convert.ToInt32(Session["AppId"].ToString()), Convert.ToInt32(Session["RRC_CertiProcedReader_id"].ToString()), "", dfd, "", "", Session["CaseNo"].ToString());
                    if (dtUp.Tables.Count > 0)
                    {
                        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["RRC_CERT_ID"].ToString();
                        Session["RRC_Certificate_id_Status"] = dtUp.Tables[0].Rows[0]["STATUS_ID"].ToString();
                        Session["PROCEEDING"] = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        summernote.Value = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        Session["RRC_CertificateContent"] = dtUp.Tables[0].Rows[0]["RRC_CERT_PROCEEDING"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
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
                    DataSet dtUp = clsRRC_CertificateBAL.Insert_RRC_Certificate_Detail(Convert.ToInt32(Session["AppId"].ToString()), Session["CaseNo"].ToString(), "", dfd, "", "");

                    if (dtUp.Tables.Count > 0)
                    {
                        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["RRC_CERT_ID"].ToString();
                        Session["RRC_Certificate_id_Status"] = dtUp.Tables[1].Rows[0]["STATUS_ID"].ToString();


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
            pnlBtnSave.Visible = true;
            pContent.InnerHtml = Session["RRC_CertificateContent"].ToString();



            ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> OrderSheetSave();</script>");

        }
        private void SaveRRC_Certificate(string Path)
        {
            try
            {
                string divCon = summernote.Value;
                string dfd = divCon.Replace("<p>", "");
                dfd = dfd.Replace("</p>", "");

                string FileName = "RRC_Certificate_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";

                ViewState["ActualPath"] = Path;



                GetLocalIPAddress();
                DateTime V_HEARINGDATE = DateTime.Today;
                int PartyID = 0;

                DataTable dt = clsRRC_CertificateBAL.Get_Certificate_COSReader(Convert.ToInt32(Session["AppId"].ToString()));
                if (dt.Rows.Count > 0)
                {

                    string RRC_Certificate_id = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_CertiReader_id"] = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_Certi_id"] = dt.Rows[0]["RRC_CERT_ID"].ToString();
                    Session["RRC_Certi_id_Status"] = dt.Rows[0]["STATUS_ID"].ToString();

                    
                //    DataSet dtUp = clsRRC_CertificateBAL.Update_RRC_Certificate_Detail_Reader(Convert.ToInt32(Session["AppId"].ToString()), Convert.ToInt32(Session["RRC_CertiReader_id"].ToString()), ViewState["ActualPath"].ToString(), dfd, "","", Session["CaseNo"].ToString());
                //    if (dtUp.Tables.Count > 0)
                //    {
                //        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["RRC_CERT_ID"].ToString();
                //        //Session["RRC_Certificate_id_Status"] = dtUp.Tables[0].Rows[0]["RRC_Certificate_id"].ToString();
                //        //Session["PROCEEDING"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                //        //summernote.Value = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                //        //Session["RRC_CertificateContent"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                //        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
                //    }

                //}
                //else
                //{
                    
                    DataSet dtUp = clsRRC_CertificateBAL.Final_Insert_RRC_Certificate_Detail(Convert.ToInt32(Session["AppId"].ToString()),Convert.ToInt32(Session["RRC_CertiReader_id"].ToString()), Session["CaseNo"].ToString(), ViewState["ActualPath"].ToString(), dfd, "", "");
                    if (dtUp.Tables.Count > 0)
                    {
                        Session["RRC_Certificate_id"] = dtUp.Tables[0].Rows[0]["RRC_CERT_ID"].ToString();
                        Session["RRC_Certificate_id_Status"] = dtUp.Tables[0].Rows[0]["RRC_CERT_ID"].ToString();

                        GetPartyDetail();
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
            int App_ID = Convert.ToInt32(ViewState["App_ID"]);
            int Certificate_id = Convert.ToInt32(Session["RRC_Certificate_id"].ToString());

            if (ddl_SignOption.SelectedValue == "1")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
                }
                //Session["HearingDate"] = lblHearingDt.Text;

                int Flag = 1;
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                //-------eSign Start------------------------

                //string Location = "Project Office -" + HF_Office.Value;
                string Location = "Bhopal";

                string ApplicationNo = hdnProposal.Value;

                //string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                //PdfName = PdfName.Replace("~/Cos_RRC_Certificate_UNSIGN/", "");
                ////string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_Certificate_UNSIGN/" + PdfName.ToString());
                //string flSourceFile = FileNamefmFolder;

                string PdfName = Session["RecentSheetPath"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_Certificate/", "");

                ViewState["filename"] = PdfName;


                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_Certificate/" + PdfName.ToString());
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
                            //txtDepartmentID.Text = "MPWCDDOH01";
                            string ResponseURL = null;
                            //string eSignURL = null;
                            //string secretKey = null;
                            string txtSignedBy = "Collector of Stamp";
                            string UIDToken = "";
                            //string DepartmentID = txtDepartmentID.Text;
                            //string Last4DigitAadhaar = TxtLast4Digit.Text;
                            string TransactionId = getTransactionID();
                            string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                            string TransactionOn = "Pre";

                            ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_RRC_Certificate.aspx");

                            // DataTable dt = clsRRC_CertificateBAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "1", "", GetLocalIPAddress(), Convert.ToInt32(Certificate_id));

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
                            //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Convert.ToInt32(Certificate_id_status));
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
            }

            else if (ddl_SignOption.SelectedValue == "3")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
                }


                string PdfName = Session["RecentSheetPath"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_Certificate/", "");

                ViewState["filename"] = PdfName;


                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_Certificate/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

                string unsignFilePath = FileNamefmFolder;

                string path = @"" + unsignFilePath;
                string file = Path.GetFileNameWithoutExtension(path);
                string NewPath = path.Replace(file, file + "_Signed");

                //string signFileFinalPath = @"/CMS/OrderSheet/CMS-R-05032400001549_2024Mar20122627_OrderSheet-Signed.pdf";
                string signFileFinalPath = NewPath;

                
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
            if (ddl_SignOption.SelectedValue == "2")
            {



                string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                PdfName = PdfName.Replace("~/Cos_RRC_Certificate/", "");

                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Cos_RRC_Certificate/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;
                string unsignFilePath = FileNamefmFolder;

                string path = @"" + unsignFilePath;
                string file = Path.GetFileNameWithoutExtension(path);
                string NewPath = path.Replace(file, file + "_Signed");


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
                    hsmMsg.Text = hSMSigner.hsm_DSC();
                    //Session["HSM_DSC"] = hsmMsg.Text;
                    if (File.Exists(NewPath))
                    {
                        Session["RecentSheetPath"] = NewPath;


                        int Flag = 1;
                        string resp_status = 1.ToString();


                    }

                    DataTable dt = clsRRC_CertificateBAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "2", "", GetLocalIPAddress(), Convert.ToInt32(Certificate_id));

                }
                else
                {
                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                }





            }
            else
            {
                string eSignDSCMessage = "Please select eSign or DSC in dropdown";
                string Title = "Success";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + eSignDSCMessage + "','success');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                return;

                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Select eSign or DSC in Dropdown', '', 'error')", true);
            }



            //-------eSign End------------------------

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

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            SaveRRC_CertificatePDF();

            custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
            pnlEsignDSC.Visible = true;
            pnlDraft.Visible = false;
            pnlBtnSave.Visible = false;

        }

        [WebMethod]
        public static string show_Pdf_BY_Hendler_Deed_Doc(string PDFPath, string DocType)
        {
            string binaryPdfPath = "";
            if (DocType == "REG")
            {
                binaryPdfPath = "../GeteRegDoc_Handler.ashx?pageURL=" + PDFPath;

            }
            else if (DocType == "PROP")
            {
                binaryPdfPath = "../GetProposalFormDoc_Handler.ashx?pageURL=" + PDFPath;

            }
            else if (DocType == "ATTCH")
            {
                binaryPdfPath = "../GetDocumentsREG_PRO_DOC.ashx?pageURL=" + PDFPath;

            }
            else
            {
                binaryPdfPath = PDFPath;
            }
            //

            return binaryPdfPath;

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