using iTextSharp.text;
using iTextSharp.text.pdf;
using SCMS_BAL;
using SCMS_DAL;
using SelectPdf;
using System;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Services;
using System.Web.UI;
using System.Collections.Generic;
using System.Configuration;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSigner;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using System.Collections;
using RestSharp;

namespace CMS_Sampada.CoS
{
    public partial class Notice_Proceeding : System.Web.UI.Page
    {
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        eSigner.eSigner _esigner = new eSigner.eSigner();

        //CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        CoSOrderSheet_BAL clsOrdersheetBAL = new CoSOrderSheet_BAL();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        string All_OrderSheetFileNme = "";

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //Response.Write(Session["WhatsappTest"]);

            //Response.Write(Session["NoticePDF"]);
            //Response.Write(Session["Link"]);
            //Response.Write(Session["partyurl"]);

            if (!Page.IsPostBack)
            {
               
                string today = DateTime.Now.ToString();
                int hdnUserID = Convert.ToInt32(Session["DROID"].ToString());

                fill_ddlTemplates(hdnUserID);
                lblDRoffice.Text = Session["District_NameHI"].ToString();
                lblDRoffice1.Text = Session["District_NameHI"].ToString();
                lblDRoffice2.Text = Session["District_NameHI"].ToString();
                lblDRoffice3.Text = Session["District_NameHI"].ToString();
                lblDRoffice4.Text = Session["District_NameHI"].ToString();
                lblDRoffice5.Text = Session["District_NameHI"].ToString();
                hdTocan.Value = Session["Token"].ToString();

                ViewState["Case_Number"] = "";
               
                if (Session["CaseNum"] != null)
                {
                    ViewState["Case_Number"] = Session["CaseNum"].ToString();

                }
                else
                {
                    ViewState["Case_Number"] = "";
                }
                if (ViewState["Case_Number"] != null)
                {

                    string appid = "";
                    if (Session["AppID"]!=null)
                    {
                        appid = Session["AppID"].ToString();
                        Session["AppID"] = appid;
                    }
                    else if (Session["AppID"] != null)
                    {
                        appid = Session["AppID"].ToString();
                    }
                    string NoticeID = "";
                    if (Session["Notice_ID"]!=null)
                    {
                        NoticeID = Session["Notice_ID"].ToString();
                        Session["NoticeID"] = NoticeID;
                    }
                    else if (Session["NoticeID"] != null)
                    {
                        NoticeID = Session["NoticeID"].ToString();
                    }
                    


                    //int appid = 2;
                    ViewState["AppID"] = appid;
                    Session["AppID"] = appid;
                    Session["Notice_ID"] = NoticeID;
                    string Appno = "";
                    if (Session["Appno"]!=null)
                    {
                         Appno = Session["Appno"].ToString();
                        Session["Appno"] = Appno;
                    }
                    else if (Session["Appno"] != null)
                    {
                        Appno = Session["Appno"].ToString();
                    }
                    
                    //string Appno = "IGRSCMS1000102";
                    ViewState["Appno"] = Appno;
                    Session["Appno"] = Appno;
                    //pnlTemplate.Visible = false;
                    DataTable dt = clsNoticeBAL.GetNoticeSheet(ViewState["Case_Number"].ToString());

                    string casenumber = ViewState["Case_Number"].ToString();
                    //ViewState["PartyDetail"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        hdnfCseNunmber.Value = dt.Rows[0]["Case_Number"].ToString();
                        hdnfApp_Number.Value = dt.Rows[0]["APPLICATION_NO"].ToString();


                        lblhearing.Text= dt.Rows[0]["hearingdate"].ToString();
                        lblComment.Text = dt.Rows[0]["PROCEEDING"].ToString();
                        lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                        lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                        lblCaseNumNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                        lblCaseNumber.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                        lblTodate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        lblToday.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        lblRegisteredDate.Text = dt.Rows[0]["inserteddate"].ToString();
                        lblHearingdateHeading.Text = dt.Rows[0]["hearingdate"].ToString();
                        lblDate.Text = dt.Rows[0]["inserteddate"].ToString();

                        lblhearing2.Text = dt.Rows[0]["hearingdate"].ToString();
                        lblComment2.Text = dt.Rows[0]["additional_proceeding"].ToString();
                        lblDate2.Text = dt.Rows[0]["inserteddate"].ToString();
                        string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        
                        //string AppNum = hdnfApp_Number.ToString();
                        string AppNum = lblProposalIdHeading.Text;
                        //RepDetails.DataSource = dt;
                        //RepDetails.DataBind();




                        //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";




                        //DataSet dsPartyDetails = new DataSet();
                        //dsPartyDetails = clsHearingBAL.GetPartyDeatil_Hearing(AppNum);




                       
                    }

                    DataSet dsDocDetails = new DataSet();

                    dsDocDetails = clsNoticeBAL.GetOrderSheetProceeding(ViewState["Case_Number"].ToString());
                   
                    DataSet dsDocRecent = new DataSet();
                    dsDocRecent = clsNoticeBAL.GetProposal_Doc_Notice(ViewState["Case_Number"].ToString(), appid);
                    if (dsDocRecent != null)
                    {
                        if (dsDocRecent.Tables.Count > 0)
                        {

                            if (dsDocRecent.Tables[0].Rows.Count > 0)
                            {
                                string fileName = dsDocRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                Session["RecentSheetPath"] = fileName.ToString();
                                //docPath.Src = fileName;
                                //grdTOCOrder.DataSource = dsDocRecent;
                                //grdTOCOrder.DataBind();

                            }
                        }
                    }


                    dsDocDetails = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), appid);
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

                    DataSet dsDocNotice;
                    dsDocNotice = clsNoticeBAL.GetNotice_Doc_Notice(Convert.ToInt32(Session["Notice_ID"]));
                    if (dsDocNotice != null)
                    {
                        if (dsDocNotice.Tables.Count > 0)
                        {

                            if (dsDocNotice.Tables[0].Rows.Count > 0)
                            {
                                //string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                //Session["Recent"] = fileName.ToString();
                                //grdNoticeDoc.DataSource = dsDocNotice;
                                //grdNoticeDoc.DataBind();
                                string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                ifRecent.Src = fileName;

                            }
                        }
                    }
                    Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                    All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                    CreateEmptyFile(All_OrderSheetFileNme);
                    CraetSourceFile(Convert.ToInt32(appid));
                    AllDocList(Convert.ToInt32(appid));
                }


                if (Request.QueryString["Response_type"] != null)
                {
                    if (Request.QueryString["Response_type"].ToString() == "Notice_Proceeding")
                    {
                        DataTable dt1 = clsNoticeBAL.InserteSignDSC_Status_NoticeProceeding(Convert.ToInt32(Session["AppID"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["NoticeID"].ToString()));
                        
                        DataTable dt = clsNoticeBAL.GetNoticeSheet(ViewState["Case_Number"].ToString());

                        string casenumber = ViewState["Case_Number"].ToString();
                        //ViewState["PartyDetail"] = dt;
                        if (dt.Rows.Count > 0)
                        {



                            lblhearing.Text = dt.Rows[0]["hearingdate"].ToString();
                            lblComment.Text = dt.Rows[0]["PROCEEDING"].ToString();
                            lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                            lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                            lblCaseNumNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                            lblCaseNumber.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                            lblTodate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                            lblToday.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                            lblRegisteredDate.Text = dt.Rows[0]["inserteddate"].ToString();
                            lblHearingdateHeading.Text = dt.Rows[0]["hearingdate"].ToString();
                            lblDate.Text = dt.Rows[0]["inserteddate"].ToString();

                            lblhearing2.Text = dt.Rows[0]["hearingdate"].ToString();
                            lblComment2.Text = dt.Rows[0]["additional_proceeding"].ToString();
                            lblDate2.Text = dt.Rows[0]["inserteddate"].ToString();
                            string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                            //string AppNum = hdnfApp_Number.ToString();
                            string AppNum = lblProposalIdHeading.Text;
                            //RepDetails.DataSource = dt;
                            //RepDetails.DataBind();


                            //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                            //DataSet dsPartyDetails = new DataSet();
                            //dsPartyDetails = clsHearingBAL.GetPartyDeatil_Hearing(AppNum);
                            btnSkip.Visible = false;
                            btnCloseProce.Visible = true;
                        }
                        pnlNoticeProceeding.Visible = true;
                        custom_tabs_four_settings_tab.Attributes["style"] = "pointer-events: visible;";

                        //add_green_proceeding.Attributes["class"] = "btn btn-success disabled;";
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DisableAddProceeding();</script>");

                        DataSet dtPro = clsNoticeBAL.Show_Notice_UpdateProceeding(Convert.ToInt32(Session["Notice_ID"].ToString()));
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {
                                lblPrevious.Text = dtPro.Tables[0].Rows[0]["additional_proceeding"].ToString();
                                IfProceeding.Src = dtPro.Tables[0].Rows[0]["SIGNED_NOTICE_PROCEEDING_PATH"].ToString();

                            }
                        }

                    }
                }
                SetDocumentBy_API();

            }
        }


        private void SetDocumentBy_API()
        {
            int appid = 0;
            if (Session["AppID"] != null)
            {
                appid = Convert.ToInt32(Session["AppID"].ToString());
            }
            string Appno = "";

            DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(appid, Appno);


            //string fileName = "estamp_party_Signed_1705996231292.pdf";

            //// RecentdocPath.Src = "Z:/backup/IGRS/E_REGISTRY/01-2024/EREG_COMP_ASP/" + fileName;
            //fileName = "estamp_party_Signed_1705996231292.pdf";
            //string ERegDate = "01-2024";
            //string EREGDocType = "EREG_COMP_ASP";
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
                    ////RecentdocPath.Src = Base64;
                    RecentdocPath.Visible = true;
                }
                else
                {
                    RecentdocPath.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                }


            }

            DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(appid, Appno);

            if (dtDocProDetails.Rows.Count > 0)
            {
                //if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //{
                //    RecentProposalDoc.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //    //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //}



                string Base64 = Comsumedata("ProposalDocument", Convert.ToInt32(dtDocProDetails.Rows[0]["ereg_id"]));
                string encodedPdfData = "";
                if (Base64 != null)
                {
                    encodedPdfData = "data:application/pdf;base64," + Base64 + "";
                    ifProposal1.Attributes["src"] = encodedPdfData;
                    ifProposal1.Visible = true;
                }
                else
                {
                    ifProposal1.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','Proposal Not Available');", true);
                }

            }

            DataTable dtDocAttachedDetails = objClsNewApplication.Get_Recent_ATTACHED_DOC_CoS_Hand(appid, Appno);

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
                    RecentAttachedDoc.Visible = true;
                }
                else
                {
                    RecentAttachedDoc.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','Attached Document Not Available');", true);
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
        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_OrderSheetAllSheetDoc/" + All_OrderSheetFileNme;

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails.Rows.Count > 0)
                {
                    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }

                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());
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
                        if (DocType == "REG")
                        {
                            if (PDFPath != "")
                            {
                                RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + PDFPath;
                                RecentdocPath.Visible = true;
                            }
                        }
                        else if (DocType == "PROP")
                        {
                            if (PDFPath != "")
                            {
                                ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + PDFPath;
                                ifProposal1.Visible = true;
                            }



                        }
                        else if (DocType == "ATTCH")
                        {
                            if (PDFPath != "")
                            {
                                RecentAttachedDoc.Src = "../GetAttachedDoc_Handler.ashx?pageURL=" + PDFPath;
                                RecentAttachedDoc.Visible = true;
                            }

                        }
                    }

                }
            }
        }
        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dtDocProDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails.Rows.Count > 0)
                {
                    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        Session["RegPath"] = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }
                DataTable dtDocProDetails_Reg = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails_Reg.Rows.Count > 0)
                {
                    if (dtDocProDetails_Reg.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        Session["FirstProposal"] = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }

                DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[3];

                    //addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    //addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    addedfilename[0] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                    addedfilename[1] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                    addedfilename[2] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());


                    string sourceFile = ViewState["CoS_OrderSheetAllSheetDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                }

            }
            catch (Exception ex)
            {

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
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }

        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoS_OrderSheetAllSheetDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoS_OrderSheetAllSheetDoc/", "<p>Order Sheet</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoS_OrderSheetAllSheetDoc/", "<p>Order Sheet</p>");
            }
            ViewState["ALLDocCAddedPDFPath"] = "~/CoS_OrderSheetAllSheetDoc/" + filename;
            ViewState["CoS_OrderSheetAllSheetDoc"] = serverpath;
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

                int footerHeight = 40;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void ValidateOTP_Click(object sender, EventArgs e)
        {

        }
        private void SaveNoticeProPDF()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                
                string divCon = summernote.Value;
                string lblTodate = DateTime.Now.ToString("dd/MM/yyyy");


                StringBuilder stringBuilder = new StringBuilder();

                stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto; text-align: center; border: 0px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='' style='width: 100%; margin: 0 auto; text - align: center; border: 0px solid #ccc; padding: 30px 30px 30px 30px;'>");
                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblDRoffice.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>प्ररूप-अ</h3>");
                stringBuilder.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>राजस्व आदेशपत्र</h3>");
                stringBuilder.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>कलेक्टर ऑफ़ स्टाम्प," + lblDRoffice.Text + " के न्यायालय में मामला क्रमांक-" + lblCase_Number.Text + "</h2> ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<table style='width: 1000px; height:800px; border: 1px solid black; border-collapse: collapse; '>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>आदेश क्रमांक कार्यवाही ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("की तारीख एवं स्थान </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही");
                stringBuilder.Append("<br/>मध्यप्रदेश शासन विरूद्ध </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>पक्षों/वकीलों <br> आदेश  पालक लिपिक के हस्ताक्षर</th>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; height:1000px;vertical-align:top;position: relative;top:150px;line -height: 20px; padding: 5px; font-size: 14px;text-align: center; '>");
                stringBuilder.Append("<div class='content' style='padding: 15px'>" + lblTodate + "</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black; vertical-align:top; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;text-align: center; '>");
                stringBuilder.Append("<div style='padding: 2px;'>");
                stringBuilder.Append("<p> Case Number:" + lblCase_Number.Text + "</p>"); //+ (lblCase_Number.Text) + "<br/>" + lblAppID.Text + lblTodatDt.Text + "</p>");
                //stringBuilder.Append(divCon);
                stringBuilder.Append("<p style='text-align: justify;'> " + lblOrderProcceding.Text + " <br><br></p>");
                stringBuilder.Append("<p style='text-align: justify;'> " + divCon + " <br><br></p>");
                //stringBuilder.Append("<p style='text - align: left;'>" + lblSeekContent.Text + "</p>");


                stringBuilder.Append("<div>");

                stringBuilder.Append("<br/>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; color:#fff;position: relative;top: 95px;right:30px;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");

                stringBuilder.Append("<b style='float: left; text-align: center; padding: 2px 0 5px 0;position: relative;top: 140px;'>पेशी दिनांक <br/>");
                //stringBuilder.Append(lblHearingDt.Text + "</b>");
                stringBuilder.Append(lblHearingdateHeading.Text);
                //stringBuilder.Append(DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0;position: relative;top: -60px;left:630px;'>कलेक्टर ऑफ़ स्टाम्प्स,<br/>" + lblDRoffice.Text + " <br/><br/> </b>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;text-align: center; '></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                //stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                //stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto;  border: 1px solid #ccc; padding: 30px 30px 30px 30px;'>");

                //stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; text-align: center '>कार्यालय जिला पंजीयक एवं न्यायालय कलेक्टर ऑफ स्टाम्प जिला भोपाल (म.प्र.)</h2>");
                //stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>आई.एस.बी.टी. परिसर, मेजनाईन फ्लोर, हबीबगंज भोपाल <br> ई - मेल - igrs@igrs.gov.in</h3> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र <br> प्रकरण क्रमांक -" + lblPrevious.Text + " धारा - 33 </b></h2> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                //stringBuilder.Append("<br>");
                //stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblOrderProcceding.Text + " <br><br><br></h3>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + divCon + " <br><br></h3>");
                //stringBuilder.Append("</div>");
                //stringBuilder.Append("<br>");
                //stringBuilder.Append("<div style='display: inline-block;float: left '>");
                ////stringBuilder.Append("<div>");
                ////stringBuilder.Append(summernote.Value);
                ////stringBuilder.Append(divCon);
                ////stringBuilder.Append("</div>");
                ////stringBuilder.Append("</div>");
                ////stringBuilder.Append("<div>");
                //stringBuilder.Append("</div>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<div>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<br/>");
                
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<br/>");
                ////stringBuilder.Append("<table style='width: 920px; border: 1px solid black; border-collapse: collapse; '><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>क्र.</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>सूचनार्थ प्रेषित/विवरण</th></tr>");
                ////int srno1 = 1;
                ////for (int i = 0; i < ((DataTable)ViewState["CopyDeatils"]).Rows.Count; i++)
                ////{
                ////    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '><b>" + srno1 + "<b></td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["CopyDeatils"]).Rows[i]["CopyContent"] + "</td></tr>");
                ////    srno1++;
                ////}
                //stringBuilder.Append("</table>");
                //stringBuilder.Append("</div>");
                //stringBuilder.Append("<br/>");
                //stringBuilder.Append("<div style = 'text-align: right;padding: 2px 0 5px 0;' > ");
                //stringBuilder.Append("<b>स्थान- जिला पंजीयक कार्यालय, भोपाल-2 <br/> जारी दिनांक:   <br/> <br/></b> ");
                //stringBuilder.Append("</div>");

                //stringBuilder.Append("</div>");
                //stringBuilder.Append("</div>");

                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_NoticeProceeding.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/COS_Notice_Proceeding/", stringBuilder.ToString());
                Session["AddProPath"] = "~/COS_Notice_Proceeding/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                //SaveNotice("~/COS_Notice/" + FileNme);
                //setRecentSheetPath();


            }
            catch (Exception ex)
            {

            }

        }
        protected void BtnSave_Click(object sender, EventArgs e)
        {
            //pnlEsignDSC.Visible = true;
            try
            {


                if (summernote.Value != "")
                {

                    DataSet dtOrderPro = clsNoticeBAL.Show_Notice_OrderProceeding(Convert.ToInt32(Session["AppID"].ToString()));
                    if (dtOrderPro != null)
                    {
                        if (dtOrderPro.Tables.Count > 0)
                        {
                            lblOrderProcceding.Text = dtOrderPro.Tables[0].Rows[0]["Proceeding"].ToString();

                        }
                    }
                    SaveNoticeProPDF();

                    string Notice_Proceeding = summernote.Value;
                    DataTable dtUp = clsNoticeBAL.InsertIntoNotice_Proceeding(Convert.ToInt32(Session["Notice_ID"].ToString()), Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(Session["Notice_ID"].ToString()), summernote.Value, Session["AddProPath"].ToString());
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");

                    //DataSet dtPro = clsNoticeBAL.Show_Notice_UpdateProceeding(Convert.ToInt32(Session["Notice_ID"].ToString()));
                    //if (dtPro != null)
                    //{
                    //    if (dtPro.Tables.Count > 0)
                    //    {
                    //        lblPrevious.Text = dtPro.Tables[0].Rows[0]["additional_proceeding"].ToString();
                    //        IfProceeding.Src = dtPro.Tables[0].Rows[0]["SIGNED_NOTICE_PROCEEDING_PATH"].ToString();

                    //    }
                    //}

                    
                    //lblPrevious.Text = "कृपया यह सूचना ले कि लिखत दिनांक 05.12.2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है.न्यायालय में उपस्थित होकर प्रस्तुत करें।";
                    summernote.Value = "";
                
                    //custom_tabs_four_settings_tab.Attributes["style"] = "pointer-events: visible;";
                    //add_green_proceeding.Attributes["class"] = "btn btn-success disabled;";
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DisableAddProceeding();</script>");
                    //ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>showAlert();</script>", false);
                    ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>ConfirmMsg();</script>", false);
                    BtnSave.Visible = false;
                    pnlEsignDSC.Visible = true;
                    //Edit.Attributes["class"] = "nav-link disabled";
                }

                else
                { 
                    lblPrevious.Text = "";
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");

                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }


        }
        public void fill_ddlTemplates(int userid)
        {
            DataTable dt = new DataTable();
            ddlTemplates.Items.Clear();
            try
            {

                dt = clsNoticeBAL.GET_MASTERS_PROCEEDING_TEMPLATES(userid);

                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddlTemplates.Items.Add(new System.Web.UI.WebControls.ListItem(text, value));
                    }
                }

                ddlTemplates.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Template--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }
        [WebMethod]
        //public static string GetTemplate_Notice(string TemId)
        //{
        //    CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        //    string Template = "<h1>RRC1</h1>";
        //    DataTable dt = clsNoticeBAL.GetTemplate();

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (dt.Rows[i]["COS_TEMP_ID"].ToString() == TemId)
        //            {
        //                Template = dt.Rows[i]["TEMPLATE_VALUE"].ToString();
        //                break;
        //            }
        //            //Console.WriteLine(dt.Rows[i]["RRC_TEMP_ID"]);
        //        }


        //    }


        //    return Template;



        //}
        public static string GetTemplate_Notice(string TemId)
        {
            CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
            string Template = "<h1>RRC1</h1>";
            DataTable dt = clsNoticeBAL.GetTemplates();

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
        protected void btnSkip_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageYesNo();</script>");
        }

        protected void btnCloseProce_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageCloseYesNo();</script>");
        }

        protected void btnSkipProceeding_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageYesNo();</script>");
        }

        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {

            //btnSkip.Text = "Close";
            btnSkip.Visible = false;
            btnCloseProce.Visible = true;
            int App_ID = Convert.ToInt32(Session["AppID"].ToString());
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

                //string Location = "Project Office -" + HF_Office.Value;
                string Location = "Bhopal";

                //string ApplicationNo = hdnProposal.Value;

                string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                PdfName = PdfName.Replace("~/COS_Notice_Proceeding/", "");
                ViewState["filename"] = PdfName;
                //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_Notice_Proceeding/" + PdfName.ToString());
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

                            if (Session["Party_ID"] != null)
                            {
                                Session["Party_ID"] = Session["Party_ID"].ToString();

                            }
                            else
                            {
                                //GetPartyDetailcount();
                            }
                            if (ViewState["Partyidram"] != null)
                            {
                                Session["Party_ID"] = ViewState["Partyidram"];
                                string pas = Session["Party_ID"].ToString();
                            }
                            if (ViewState["HearingDate"] != null)
                            {
                                ViewState["HearingDate"] = ViewState["HearingDate"];
                            }
                            if (ViewState["CopyDeatils"] != null)
                            {
                                ViewState["CopyDeatils"] = ViewState["CopyDeatils"];
                            }
                            //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Notice_ID=" + Session["Notice_ID"].ToString() + "&Response_type=Notice_Proceeding");
                            //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "Notice_Proceeding.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Notice_ID=" + Session["Notice_ID"].ToString() +"&Response_type=Notice_Proceeding");
                            ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_AddNoticeProceeding.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Notice_ID=" + Session["Notice_ID"].ToString() +"&Response_type=Notice_Proceeding");

                            //getdata();

                            DataTable dt1 = clsNoticeBAL.InserteSignDSC_Status_NoticeProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);


                            AuthMode authMode = AuthMode.OTP;

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


                //DataTable dt1 = clsNoticeBAL.InserteSignDSC_Status_NoticeProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);

            }

            if (ddl_SignOption.SelectedValue == "2")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
                }
                DataTable dt2 = clsNoticeBAL.InserteSignDSC_Status_NoticeProceeding(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);
            }




            DataTable dt = clsNoticeBAL.GetNoticeSheet(ViewState["Case_Number"].ToString());

            string casenumber = ViewState["Case_Number"].ToString();
            //ViewState["PartyDetail"] = dt;
            if (dt.Rows.Count > 0)
            {
                


                lblhearing.Text = dt.Rows[0]["hearingdate"].ToString();
                lblComment.Text = dt.Rows[0]["PROCEEDING"].ToString();
                lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                lblCaseNumNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                lblCaseNumber.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                lblTodate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                lblToday.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");
                lblRegisteredDate.Text = dt.Rows[0]["inserteddate"].ToString();
                lblHearingdateHeading.Text = dt.Rows[0]["hearingdate"].ToString();
                lblDate.Text = dt.Rows[0]["inserteddate"].ToString();

                lblhearing2.Text = dt.Rows[0]["hearingdate"].ToString();
                lblComment2.Text = dt.Rows[0]["additional_proceeding"].ToString();
                lblDate2.Text = dt.Rows[0]["inserteddate"].ToString();
                string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                //string AppNum = hdnfApp_Number.ToString();
                string AppNum = lblProposalIdHeading.Text;
                //RepDetails.DataSource = dt;
                //RepDetails.DataBind();


                //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                //DataSet dsPartyDetails = new DataSet();
                //dsPartyDetails = clsHearingBAL.GetPartyDeatil_Hearing(AppNum);


            }
            pnlNoticeProceeding.Visible = true;
            custom_tabs_four_settings_tab.Attributes["style"] = "pointer-events: visible;";
          
            //add_green_proceeding.Attributes["class"] = "btn btn-success disabled;";
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DisableAddProceeding();</script>");
        }
        protected void btnClose_Click(object sender, EventArgs e)
        {
            pnlDocView.Visible = false;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "setTimeout(function () { window.scrollTo(0,document.body.scrollHeight); }, 25);", true);

        }
        //protected void add_green_proceeding_Click(object sender, EventArgs e)
        //{
        //    pnlTemplate.Visible = true;
        //}


        //private void BindOrdersheetProceding()

        //{



        //    //string aaa=Session["NextHearing"].ToString();

        //    if (ViewState["Case_Number"] != null)
        //    {
        //        DataTable dt = clsHearingBAL.GetOrderSheetProceeding(ViewState["Case_Number"].ToString());

        //        string casenumber = ViewState["Case_Number"].ToString();
        //        //ViewState["PartyDetail"] = dt;
        //        if (dt.Rows.Count > 0)
        //        {


        //            txtHearingOrder.Text = dt.Rows[0]["proceeding"].ToString();
        //            lblHearingdate.Text = dt.Rows[0]["hearingdate"].ToString();
        //            lblordersheetDate.Text = dt.Rows[0]["inserteddate"].ToString();
        //        }

        //    }

        //}
        public void AllDocList(int APP_ID)
        {
            try
            {
                //DataSet dsDocList = clsNoticeBAL.GetAllDocList_NoticeProceeding(APP_ID);
                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(APP_ID,ViewState["Appno"].ToString());

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


        private void getdata_Esign()
        {

            NameValueCollection response = Request.Form;

            if (!string.IsNullOrEmpty(response["msg"]))
            {
                string xmlData = response["msg"];
                byte[] data1 = Convert.FromBase64String(xmlData);
                string decodedString1 = Encoding.UTF8.GetString(data1);

                //Parse xml response to xml object.
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.PreserveWhitespace = true;
                xmldoc.LoadXml(decodedString1);


                // Create the signed XML object.
                SignedXml sxml = new SignedXml(xmldoc);
                try
                {
                    // Get the XML Signature node and load it into the signed XML object.
                    XmlNode dsig = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];
                    sxml.LoadXml((XmlElement)dsig);
                }
                catch
                {
                    throw new Exception("no signature found in response.");
                }

                X509Certificate2 x509 = new X509Certificate2(Server.MapPath("aspesign.cer"));

                if (sxml.CheckSignature(x509, true))
                {
                    XmlNode _AspEsignResp = xmldoc.GetElementsByTagName("AspEsignResp")[0];
                    string errCode = "", errMsg = "", transId = "";//, environment = "";

                    int status = Convert.ToInt16(_AspEsignResp.Attributes["status"].Value);
                    if (_AspEsignResp.Attributes["errCode"] != null)
                        errCode = _AspEsignResp.Attributes["errCode"].Value;
                    if (_AspEsignResp.Attributes["errMsg"] != null)
                        errMsg = _AspEsignResp.Attributes["errMsg"].Value;
                    if (_AspEsignResp.Attributes["transId"] != null)
                        transId = _AspEsignResp.Attributes["transId"].Value;
                    ViewState["status"] = status;

                    if (status == 1)
                    {
                        ClsPkcs clspk = new ClsPkcs();
                        string hashcode = clspk.readCert(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);

                        string uidtoken = clsHash.readCertUID72Chars(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);
                        clsHash.embedSignature(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value, transId);

                        //btnDownload.Visible = true;


                    }
                    else
                    {
                        lblStatus.Text = "Error while signing the document Error Code : " + errCode + " errMsg : " + errMsg;
                    }
                }
                else
                    lblStatus.Text = "Error while validating the xml signature.";
            }
            else if (ViewState["filename"] != null)
            {
                _esigner.DownLoad();
            }
            else
            {
                lblStatus.Text = "Method not allowed.";
            }


            //string Stringpath = Session["Case"] as string;
            //string url = "Notice.aspx?Case_Number=" + Stringpath;

            //string url = "Notice.aspx?Case_Number=" + casenum + "&App_Id=" + appid + "&AppNo=" + Appno;
            int Flag = 2;
            //string url = "Notice.aspx?Case_Number=" + casenum + "&App_Id=" + appid + "&AppNo=" + Appno + "&HearingDate=" + hearingdate + "&Flag=" + Flag;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('eSigned notice saved successfully','success')", true);
            //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
        }
        private void getdata()
        {


            //string casenum = Session["CaseNum"].ToString();
            //string appid = Session["AppID"].ToString();
            //string Appno = Session["Appno"].ToString();




            //btnDownload.Visible = false;

            NameValueCollection response = Request.Form;

            if (!string.IsNullOrEmpty(response["msg"]))
            {
                string xmlData = response["msg"];
                byte[] data1 = Convert.FromBase64String(xmlData);
                string decodedString1 = Encoding.UTF8.GetString(data1);

                //Parse xml response to xml object.
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.PreserveWhitespace = true;
                xmldoc.LoadXml(decodedString1);


                // Create the signed XML object.
                SignedXml sxml = new SignedXml(xmldoc);
                try
                {
                    // Get the XML Signature node and load it into the signed XML object.
                    XmlNode dsig = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];
                    sxml.LoadXml((XmlElement)dsig);
                }
                catch
                {
                    throw new Exception("no signature found in response.");
                }

                X509Certificate2 x509 = new X509Certificate2(Server.MapPath("aspesign.cer"));

                if (sxml.CheckSignature(x509, true))
                {
                    XmlNode _AspEsignResp = xmldoc.GetElementsByTagName("AspEsignResp")[0];
                    string errCode = "", errMsg = "", transId = "";//, environment = "";

                    int status = Convert.ToInt16(_AspEsignResp.Attributes["status"].Value);
                    if (_AspEsignResp.Attributes["errCode"] != null)
                        errCode = _AspEsignResp.Attributes["errCode"].Value;
                    if (_AspEsignResp.Attributes["errMsg"] != null)
                        errMsg = _AspEsignResp.Attributes["errMsg"].Value;
                    if (_AspEsignResp.Attributes["transId"] != null)
                        transId = _AspEsignResp.Attributes["transId"].Value;

                    if (status == 1)
                    {
                        ClsPkcs clspk = new ClsPkcs();
                        string hashcode = clspk.readCert(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);

                        string uidtoken = clsHash.readCertUID72Chars(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);
                        clsHash.embedSignature(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value, transId);

                        //btnDownload.Visible = true;

                        //lblStatus.Text = "Document has successfully eSigned. Please download.";
                    }
                    else
                    {
                        //lblStatus.Text = "Error while signing the document Error Code : " + errCode + " errMsg : " + errMsg;
                    }
                }
                else
                    lblStatus.Text = "Error while validating the xml signature.";
            }
            else if (Request.QueryString["filename"] != null)
            {
                // _esigner.DownLoad();
            }
            else
            {
                lblStatus.Text = "Method not allowed.";
            }


            //string Stringpath = Session["Case"] as string;
            //string url = "Notice.aspx?Case_Number=" + Stringpath;

            //string url = "Notice.aspx?Case_Number=" + casenum + "&App_Id=" + appid + "&AppNo=" + Appno;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('eSigned notice saved successfully','success');", true);
            //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
        }

    }
}


//GetOrderSheetProceeding