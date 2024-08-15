using CMS_Sampada_BAL;
using eSigner;
using iTextSharp.text.pdf;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.UI.HtmlControls;
using RestSharp;
using System.Web.Script.Serialization;
using HSM_DSC;

namespace CMS_Sampada.CoS
{
    public partial class Hearing : System.Web.UI.Page
    {
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        eSigner.eSigner _esigner = new eSigner.eSigner();

        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];

        string Application_Id_eMudra = ConfigurationManager.AppSettings["ApplicationId_eMudra"];
        string Department_Id_eMudra = ConfigurationManager.AppSettings["DepartmentId_eMudra"];
        string Secretkey_eMudra = ConfigurationManager.AppSettings["Secretkey_eMudra"];
        string eSignURL_eMudra = ConfigurationManager.AppSettings["eSignURL_eMudra"];

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];
        ClsNewApplication objClsNewApplication = new ClsNewApplication();


        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        string All_DocFile_Hearing = "";

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {

            string noticeId1 = Page.RouteData.Values["NoticeId"] as string;
            string hearingDate = Page.RouteData.Values["Hearing"] as string;
            string hearingId = Page.RouteData.Values["hearing_id"] as string;


            lblDRoffice.Text = Session["District_NameHI"].ToString();
            lblDRoffice1.Text = Session["District_NameHI"].ToString();
            lblDRoffice2.Text = Session["District_NameHI"].ToString();
            lblDRoffice3.Text = Session["District_NameHI"].ToString();
            //lblDRoffice4.Text = Session["District_NameHI"].ToString();
            //lblDRoffice5.Text = Session["District_NameHI"].ToString();
            hdnSROfficeNameHi.Value = Session["District_NameHI"].ToString();
            hdTocan.Value = Session["Token"].ToString();

            if (ViewState["VerifiedParty"] != null)
            {
                //lblVerifiedParty.Text = ViewState["VerifiedParty"].ToString();
            }


            if (!Page.IsPostBack)
            {
                string today = DateTime.Now.ToString();
                //ScriptManager.RegisterStartupScript(this, GetType(), "StartCountdown", "startCountdown();", true);

                int hdnUserID = Convert.ToInt32(Session["DROID"].ToString());

                fill_ddlTemplate1(hdnUserID);
                fill_ddlTemplate2(hdnUserID);
                //lblVerifiedParty1.Text = "vdfvzcv dsv";
                //lblVerifiedParty.Text = ViewState["VerifiedParty"].ToString();
                //setVerifiedparty();
                //edit_notice.Visible = false;
                //AjaxFileUpload1.Visible = false;
                grdPartyDetails.Columns[1].Visible = false;

                ViewState["Case_Number"] = "";
                if (Session["Case_Number"] != null)
                {
                    ViewState["Case_Number"] = Session["Case_Number"].ToString();
                    ViewState["NoticeId"] = Session["Notice_ID"].ToString();
                    ViewState["HearingDate"] = Session["HearingDate"].ToString();
                    ViewState["AppID"] = Session["App_ID"].ToString();
                    ViewState["hearing_id"] = Session["hearing_id"].ToString();
                    Session["hearing_id_Final"] = Session["hearing_id"].ToString();
                    //lblProposalIdHeading.Text = Session["Appno"].ToString();
                    lblCase_Number.Text = Session["Case_Number"].ToString();
                }
                else
                {
                    ViewState["Case_Number"] = "";
                }
                if (ViewState["Case_Number"] != null)
                {
                    DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(ViewState["AppID"].ToString()));

                    string casenumber = ViewState["Case_Number"].ToString();
                    //ViewState["PartyDetail"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        hdnfCseNunmber.Value = dt.Rows[0]["Case_Number"].ToString();
                        hdnfApp_Number.Value = dt.Rows[0]["APPLICATION_NO"].ToString();
                        lblTodate.Text = DateTime.UtcNow.ToString("dd-MM-yyyy");


                        lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                        lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                        //lblRegisteredDate.Text = dt.Rows[0]["inserteddate"].ToString();
                        //lblHearingdateHeading.Text = dt.Rows[0]["hearingdate"].ToString();

                        //lblHearingdateHeading.Text = ((DateTime)dt.Rows[0]["hearingdate"]).ToString("MM/dd/yyyy");

                        string instDate = dt.Rows[0]["inserteddate"].ToString(); // Assuming it's stored as a string
                        string HearingDate = dt.Rows[0]["hearingdate"].ToString(); // Assuming it's stored as a string
                        DateTime parsedinstDate;

                        if (DateTime.TryParse(instDate, out parsedinstDate))
                        {
                            lblRegisteredDate.Text = parsedinstDate.ToString("dd/MM/yyyy");
                        }
                        lblRegisteredDate.Text = instDate;
                        DateTime PaesedHearing_dt;
                        if (DateTime.TryParse(HearingDate, out PaesedHearing_dt))
                        {
                            lblHearingdateHeading.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                            Session["HearingDate"] = PaesedHearing_dt.ToString("dd/MM/yyyy");
                        }
                        //lblHearingdateHeading.Text = HearingDate;
                        //lblHearingdateHeading.Text = ((DateTime)ViewState["HearingDate"]).ToString("dd/MM/yyyy");

                        if (ViewState["HearingDate"] != null && DateTime.TryParse(ViewState["HearingDate"].ToString(), out DateTime parsedDate))
                        {
                            lblHearingdateHeading.Text = parsedDate.ToString("dd/MM/yyyy");
                        }
                        else
                        {
                            // Handle the case where the object stored in ViewState["HearingDate"] cannot be converted to a DateTime
                        }







                        //lblHearingdateHeading.Text = ViewState["HearingDate"].ToString();
                        string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        lblToday.Text = Convert.ToString(TDate);
                        //string AppNum = hdnfApp_Number.ToString();
                        string AppNum = lblProposalIdHeading.Text;

                        Session["AppID"] = dt.Rows[0]["App_ID"].ToString(); ;
                        Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                        PartyDetail();

                        int App_id = Convert.ToInt32(Session["AppID"].ToString());

                        lblCaseNumber.Text = dt.Rows[0]["Case_Number"].ToString();

                        summernote.Value = hdnSROfficeNameHi.Value + " द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";
                        lblTodayDetails.Text = "आज दिनांक: " + TDate + " को प्रकरण प्रस्तुत";
                        lblCaseNumNo.Text = dt.Rows[0]["Case_Number"].ToString();
                        setVerifiedparty();
                        setAdnl_Verifiedparty();
                        //int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());

                        int NoticeId_1 = Convert.ToInt32(ViewState["NoticeId"].ToString());

                        //DataTable dt2 = clsHearingBAL.Get_NoticeID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
                        DataTable dt2 = clsHearingBAL.Get_NoticeID_COSForDraftProceeding(Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(ViewState["hearing_id"].ToString()));
                        if (dt2.Rows.Count > 0)
                        {
                            string noticeIdString = dt2.Rows[dt2.Rows.Count - 1]["NOTICE_ID"].ToString();
                            string Proceeding = dt2.Rows[dt2.Rows.Count - 1]["NEXT_PROCEEDING"].ToString();

                            Session["HearingContent"] = Proceeding;

                            if (Proceeding == "")
                            {
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>hide_x();</script>", false);
                            }
                            else
                            {
                                hdnProceeding.Value = Proceeding;

                                //PartyVerify_Pnl.Visible = true;
                                ClientScript.RegisterStartupScript(this.GetType(), "ShowPartyPnl", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    document.getElementById('PartyVerify_Pnl').style.display = 'block';
                                                                });
                                                            </script>");
                                if (ViewState["VerifiedParty"] != null && ViewState["VerifiedParty"].ToString() != "")
                                {
                                    lblVerifiedParty.Text = ViewState["VerifiedParty"].ToString();
                                }
                                if (ViewState["VerifiedParty_Behalf"] != null && ViewState["VerifiedParty_Behalf"].ToString() != "")
                                {
                                    lblVerifiedParty1.Text = ViewState["VerifiedParty_Behalf"].ToString();
                                }

                                DataSet dsPartyDetails_1 = new DataSet();
                                dsPartyDetails_1 = clsHearingBAL.GetPartyDeatil_Hearing(AppNum, NoticeId_1);
                                if (dsPartyDetails_1 != null)
                                {
                                    if (dsPartyDetails_1.Tables.Count > 0)
                                    {

                                        if (dsPartyDetails_1.Tables[0].Rows.Count > 0)
                                        {
                                            if (dsPartyDetails_1.Tables[0].Rows[0]["OTP_status"].ToString() == "VERIFIED")
                                            {

                                                Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction11", "partyPnl_11();submitForm();", true);


                                                ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript11", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    
                                                                    $('#ifselectyes').show();
                                                                });
                                                            </script>");
                                            }

                                        }

                                    }
                                }


                                string AppNo = Session["AppID"].ToString();
                                DataSet dsAdnlParty = new DataSet();


                                dsAdnlParty = clsHearingBAL.GetAdnlParty_Hearing(AppNo, NoticeId_1);
                                if (dsAdnlParty != null)
                                {
                                    if (dsAdnlParty.Tables.Count > 0)
                                    {

                                        if (dsAdnlParty.Tables[0].Rows.Count > 0)
                                        {

                                            CommentPnl.Visible = true;
                                            GvAddParty.DataSource = dsAdnlParty;
                                            GvAddParty.DataBind();

                                        }

                                    }
                                }
                                DataSet dsTempStatus = new DataSet();
                                dsTempStatus = clsHearingBAL.GetTempStatus_Hearing(AppNum, NoticeId_1);
                                if (dsTempStatus != null)
                                {
                                    if (dsTempStatus.Tables.Count > 0)
                                    {

                                        if (dsTempStatus.Tables[0].Rows.Count > 0)
                                        {
                                            if (dsTempStatus.Tables[0].Rows[0]["Hear_Template_status"].ToString() == "1")
                                            {
                                                //btnDraftSave.Visible = false;
                                                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "showAlert()", true);
                                                //ScriptManager.RegisterStartupScript(this, this.GetType(), "CallMyFunction3444", "<script>showAlert();</script>", false);
                                                ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript11111", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    $('#hearing').hide();
                                                                    $('#OrdersheetDiv').show();
                                                                    $('#newhearing').show();
                                                                });
                                                            </script>");
                                            }

                                        }

                                    }
                                }

                                DataSet dsAllVerfiedCheck = new DataSet();
                                dsAllVerfiedCheck = clsHearingBAL.GetPartyDeatil_Hearing(AppNum, NoticeId_1);
                                if (dsAllVerfiedCheck != null && dsAllVerfiedCheck.Tables.Count > 0)
                                {
                                    DataTable partyDetailsTable = dsAllVerfiedCheck.Tables[0];
                                    bool allVerified = true; // Assume all values are verified initially

                                    foreach (DataRow row in partyDetailsTable.Rows)
                                    {
                                        if (row["OTP_status"].ToString() != "VERIFIED")
                                        {
                                            allVerified = false; // If any value is not verified, set the flag to false and break the loop
                                            break;
                                        }
                                    }

                                    if (allVerified)
                                    {
                                        //ScriptManager.RegisterStartupScript(this, GetType(), "AnyValue11", "TempShow();", true);


                                        ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    $('#ifselectyes_Template').show();
                                                                    $('#ifAdditionalParties').hide();
                                                                    $('#ifselectyes').show();
                                                                    $('#ifselectyes_Template').show();
                                                                });
                                                            </script>");


                                    }
                                }


                                editproceeding.Style.Add("display", "block");

                                DataTable dt_status = clsHearingBAL.Get_Status_HearingId(Convert.ToInt32(Session["AppID"].ToString()));
                                if (dt_status.Rows[0]["STATUS_ID"].ToString() == "34")
                                {
                                    ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "DisableRbtn_NextNotice(); disableradioBtn1();", true);
                                }
                                if (dt_status.Rows[0]["STATUS_ID"].ToString() == "42")
                                {
                                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "viewEsignDSC();DisableNoticeRbtn1();DisableFnlOrderRbtn_1();", true);
                                    hdnStatus.Value = "SendFinalOrder_EsignPnl";
                                    BtnFinalOrder.Visible = false;
                                    btnDraftSave.Visible = false;
                                }








                            }




                            summernote.Value = Proceeding;
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>AddNotice();AddNotice_new();newshownhide();</script>", false);
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>AddNotice();AddNotice_new();newshownhide();</script>", false);

                        }

                    }

                    GetContent();
                    GetHearingDate();
                    //GetReportReason();

                    DataSet dsDocDetails = new DataSet();

                    int appid1 = Convert.ToInt32(Session["AppID"]);

                    dsDocDetails = clsHearingBAL.GetPartyReply(appid1);
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                string PartyReply = dsDocDetails.Tables[0].Rows[0]["PARTY_REPLYCOMMENT"].ToString();

                                if (PartyReply != "")
                                {
                                    string dataToDisplay = PartyReply;
                                    txtReadOnly.Value = dataToDisplay;
                                }
                            }

                        }
                    }

                    dsDocDetails = clsHearingBAL.GetOrderSheetProceeding(Convert.ToInt32(ViewState["AppID"].ToString()));
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


                                GetReportReason();
                            }
                        }
                    }

                    dsDocDetails = clsHearingBAL.GetNoticeProceeding(Convert.ToInt32(ViewState["AppID"].ToString()));
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


                    dsDocDetails = clsHearingBAL.GetNotice_Doc(Convert.ToInt32(ViewState["AppID"].ToString()));
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                string fileName = dsDocDetails.Tables[0].Rows[0]["NOTICE_DOCS"].ToString();
                                Session["RecentSheetPath"] = fileName.ToString();
                                RecentdocPath.Src = fileName;
                                //grdTOCNotice.DataSource = dsDocDetails;
                                //grdTOCNotice.DataBind();



                            }
                        }
                    }




                    //int appid = Convert.ToInt32(Session["AppID"]);

                    DataSet dtPro = clsHearingBAL.Show_All_Proceeding(Convert.ToInt32(Session["AppID"]));
                    if (dtPro != null && dtPro.Tables.Count > 0)
                    {

                        //string src = "";
                        foreach (DataRow row in dtPro.Tables[0].Rows)
                        {
                            string documentPath = row["ORDRSHEETPATH"].ToString();
                            // Append logic to handle documentPath as needed (e.g., checking if it's a valid path)
                            // Assuming Iframeprevious is the ID of your iframe element
                            //Iframeprevious.Src += documentPath + ";"; // Modify this as per your requirement

                            HtmlGenericControl iframe = new HtmlGenericControl("iframe");
                            iframe.Attributes["src"] = documentPath;
                            Iframeprevious.Controls.Add(iframe);
                            //src += documentPath + ";";

                        }
                        //Iframeprevious.Src = src;
                    }

                    //DataSet dtPro = clsHearingBAL.Show_Notice_UpdateProceeding(Convert.ToInt32(ViewState["NoticeId"].ToString()));
                    //if (dtPro != null)
                    //{
                    //    if (dtPro.Tables.Count > 0)
                    //    {

                    //        Iframeprevious.Src = dtPro.Tables[0].Rows[0]["SIGNED_NOTICE_PROCEEDING_PATH"].ToString();

                    //    }
                    //}

                    int appid = Convert.ToInt32(Session["AppID"]);

                    string Proposal_ID = Session["Appno"].ToString();

                    string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";

                    Session["All_DocSheet"] = FileNme;
                    //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                    All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                    CreateEmptyFile(All_DocFile_Hearing);
                    CraetSourceFile(Convert.ToInt32(appid));
                    AllDocList(Convert.ToInt32(appid));

                    //ListOfDocPath(Convert.ToInt32(appid));
                }

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



            if (dtDocDetails.Rows.Count > 0)
            {



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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                }

            }

            DataTable dtDocAttachedDetails = objClsNewApplication.Get_Recent_ATTACHED_DOC_CoS_Hand(appid, Appno);

            if (dtDocAttachedDetails.Rows.Count > 0)
            {

                string Docbase64 = Comsumedata("AdditionalDocument", Convert.ToInt32(dtDocAttachedDetails.Rows[0]["ereg_id"]));
                string encodedPdfData = "";
                if (Docbase64 != null)
                {
                    if (Docbase64 != "")
                    {
                        encodedPdfData = "data:application/pdf;base64," + Docbase64 + "";
                        RecentAttachedDoc.Attributes["src"] = encodedPdfData;
                        RecentAttachedDoc.Visible = true;
                    }
                    else
                    {
                        RecentAttachedDoc.Visible = false;
                    }

                }
                else
                {
                    RecentAttachedDoc.Visible = false;
                    //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
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

        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the data source row for the current GridView row
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Access the second column (index 1) and modify its value
                e.Row.Cells[1].Text = Server.MapPath(rowView["file"].ToString());
            }
        }

        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoSAllHearingSheetDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoSAllHearingSheetDoc/", "<p>Order Sheet</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoSAllHearingSheetDoc/", "<p>Order Sheet</p>");
            }
            ViewState["ALLDocAdded_Hearing"] = "~/CoSAllHearingSheetDoc/" + filename;
            ViewState["CoSAllHearingSheetDoc"] = serverpath;
        }

        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dt = clsHearingBAL.GetHearingAllDoc(APP_ID);
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

                    string sourceFile = ViewState["CoSAllHearingSheetDoc"].ToString();
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
        public void AllDocList(int APP_ID)
        {
            DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(APP_ID, Session["Appno"].ToString());

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

        public void CraetSourceFile_old(int APP_ID)
        {
            try
            {
                DataTable dt = clsHearingBAL.GetHearingAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {
                    string[] insDate = new string[5];

                    string[] addedfilename = new string[5];


                    addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    byte[] byteData = System.IO.File.ReadAllBytes(addedfilename[0]);
                    PdfReader pdfReader = new PdfReader(byteData);
                    int nofPages_SR = pdfReader.NumberOfPages;
                    string pageRange_SR = $"1-{nofPages_SR}";
                    //PageRangeLabel.Text = $"Page Range: {pageRange}";

                    addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    byte[] byteData2 = System.IO.File.ReadAllBytes(addedfilename[1]);
                    PdfReader pdfReader2 = new PdfReader(byteData2);
                    int nofPages_Prop1 = pdfReader2.NumberOfPages;
                    string pageRange_Prop1 = $"{nofPages_SR + 1}-{nofPages_SR + 1 + nofPages_Prop1}";
                    string lastNo = $"{nofPages_SR + 1 + nofPages_Prop1 + 1}";
                    int result = int.Parse(lastNo);

                    addedfilename[2] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                    byte[] byteData3 = System.IO.File.ReadAllBytes(addedfilename[2]);
                    PdfReader pdfReader3 = new PdfReader(byteData3);
                    int nofPages_Prop2 = pdfReader3.NumberOfPages;
                    string pageRange_Prop2 = $"{result}-{result + nofPages_Prop2}";
                    string lastNo1 = $"{result + nofPages_Prop2 + 1}";
                    int result1 = int.Parse(lastNo1);

                    addedfilename[3] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                    byte[] byteData4 = System.IO.File.ReadAllBytes(addedfilename[3]);
                    PdfReader pdfReader4 = new PdfReader(byteData4);
                    int nofPages_Order = pdfReader4.NumberOfPages;
                    string pageRange_Order = $"{result1}-{result1 + 1 + nofPages_Order}";
                    string lastNo2 = $"{result1 + 1 + nofPages_Order + 1}";
                    int result2 = int.Parse(lastNo2);

                    addedfilename[4] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());
                    byte[] byteData5 = System.IO.File.ReadAllBytes(addedfilename[4]);
                    PdfReader pdfReader5 = new PdfReader(byteData5);
                    int nofPages_Notice = pdfReader5.NumberOfPages;
                    string pageRange_Notice = $"{result2}-{result2 + 1 + nofPages_Notice}";


                    string[] InsertPageNo = { pageRange_SR, pageRange_Prop1, pageRange_Prop2, pageRange_Order, pageRange_Notice };
                    string[] InsertPath = { addedfilename[0], addedfilename[1], addedfilename[2], addedfilename[3], addedfilename[4] };
                    InsertMultipleRows(InsertPath, InsertPageNo);

                    string sourceFile = ViewState["CoSAllHearingSheetDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocAdded_Hearing"].ToString());




                }

            }
            catch (Exception ex)
            {

            }

        }
        protected void InsertMultipleRows(string[] InsertPath, string[] InsertPageNo)
        {

            for (int i = 0; i < InsertPath.Length; i++)
            {
                DataTable dtUp = clsHearingBAL.InsertPathwithPage(Convert.ToInt32(Session["AppID"]), ViewState["Case_Number"].ToString(), InsertPath[i], InsertPageNo[i]);

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
                ifPDFViewerAll_Hearing.Src = "~/CoSAllHearingSheetDoc/" + All_DocFile_Hearing;
            }
        }

        private void PartyDetail()
        {

            string AppNum = Session["Appno"].ToString();
            int appid = Convert.ToInt32(Session["AppID"].ToString());
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());

            DataSet dsPartyDetails = new DataSet();
            dsPartyDetails = clsHearingBAL.GetPartyDeatil_Hearing(AppNum, Noticeid);
            if (dsPartyDetails != null)
            {
                if (dsPartyDetails.Tables.Count > 0)
                {

                    if (dsPartyDetails.Tables[0].Rows.Count > 0)
                    {
                        if (dsPartyDetails.Tables[0].Rows[0]["OTP_status"].ToString() == "VERIFIED")
                        {
                            grdPartyDetails.DataSource = dsPartyDetails;
                            grdPartyDetails.DataBind();
                            foreach (GridViewRow gvr in grdPartyDetails.Rows)
                            {
                                LinkButton btn = ((LinkButton)gvr.FindControl("lnkSendSMS"));

                                string labelValue = ((Label)gvr.FindControl("LblStatus")).Text;
                                if (labelValue.Contains("VERIFIED"))
                                {
                                    //e.Row.Cells[7].ImageUrl = "/images/Red.gif";
                                    btn.Enabled = false;
                                    btn.Style.Add("opacity", "0.6");
                                    btn.Style.Add("cursor", "not-allowed");
                                    btn.Style.Add("pointer-events", "none");
                                    gvr.Cells[7].ForeColor = Color.Green;
                                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>disableRadioButtons();</script>");

                                }
                            }
                        }
                        else
                        {
                            grdPartyDetails.DataSource = dsPartyDetails;
                            grdPartyDetails.DataBind();
                        }

                    }

                }
            }
        }

        private void AdditionalPartyDetail()
        {

            string AppNum = Session["AppID"].ToString();
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());
            DataSet dsPartyDetails = new DataSet();
            dsPartyDetails = clsHearingBAL.GetAdnl_PartyDeatil_Hearing(AppNum, Noticeid);
            if (dsPartyDetails != null)
            {
                if (dsPartyDetails.Tables.Count > 0)
                {

                    if (dsPartyDetails.Tables[0].Rows.Count > 0)
                    {
                        GvAddParty.DataSource = dsPartyDetails;
                        GvAddParty.DataBind();

                    }

                }
            }
        }

        //protected void btnSubmit_HearingOrersheet(object sender, EventArgs e)
        //{
        //    if (txtHearingDate.Text == "")
        //    {
        //        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");
        //    }
        //    else
        //    {

        //    }
        //}




       

        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                //string FileName = FileNme;
                //string OrderSheetPath = Server.MapPath(path);
                //if (!Directory.Exists(OrderSheetPath))
                //{
                //    Directory.CreateDirectory(OrderSheetPath);
                //}

                string FileName = FileNme;
                string OrderSheetPath = path;
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

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void ValidateOTP_Click(object sender, EventArgs e)
        {

        }


        protected void grdPartyDetails_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SendOTP")
            {
                txtOTP.Text = "";

                //string mobileno = e.CommandArgument.ToString().Split(',')[0];
                //string propertyId = e.CommandArgument.ToString().Split(',')[1];
                //string Name = e.CommandArgument.ToString().Split(',')[2];
                //Session["Party_mobileno"] = mobileno;
                //Session["property_Id"] = propertyId;



                string mobileno = e.CommandArgument.ToString().Split(',')[0];
                string propertyId = e.CommandArgument.ToString().Split(',')[1];
                string partyId = e.CommandArgument.ToString().Split(',')[1];
                Session["partyId"] = partyId;
                string Name = e.CommandArgument.ToString().Split(',')[2];
                Session["Name"] = Name;
                Session["Party_mobileno"] = mobileno;
                Session["property_Id"] = propertyId;

                txtOTP.ReadOnly = false;
                Random rnd = new Random();
                string OTP = rnd.Next(1000, 9999).ToString();
                lblotp.Text = OTP.ToString();
                //string Party_Name="Imam";
                string msg = "Dear '" + Name + "', please enter OTP '" + OTP + "' for User Verification on SAMPADA Portal. OTP is valid for 15 minutes.";
                //int eregId = 12345;
                //string msg = "Dear user, '" + OTP + "' is one time password for Premium Slot Fee related payment of Registry (ID: '" + eregId + "') through your SAMPADA wallet. The OTP is valid for 30 minutes.";
                //string response = SMSUtility.Send(msg, mobileno, "1407168415452536769");
                string response = SMSUtility.Send(msg, mobileno, "1407168414729061216");

                Session["OTP"] = OTP;

                string mob = mobileno.ToString();

                lblmobnum.Text = mob;
                lblmobnum.Text = string.Format("XXX XXXX") + mob.Substring(mob.Length - 4, 4);

                //lnkSendSMS.Enabled = false;
                //ClientScript.RegisterStartupScript(this.GetType(), "startCountdown", "startCountdown();", true);
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);


            }
        }


        protected void verifyOTP_Click(object sender, EventArgs e)
        {
            //lblMsg.Visible = true;

            string RE_OTP = "";
            string OTP = Session["OTP"].ToString();
            if (Session["RESEND_OTP"] != null)
            {
                if (Session["RESEND_OTP"].ToString() != "")
                {
                    RE_OTP = Session["RESEND_OTP"].ToString();
                }
            }

            if (OTP == txtOTP.Text || RE_OTP == txtOTP.Text)

            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageOtpVerified();</script>");
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();submitForm();</script>", false);

                InsertOTPinDB();


                foreach (GridViewRow gvr in grdPartyDetails.Rows)
                {
                    LinkButton btn = ((LinkButton)gvr.FindControl("lnkSendSMS"));

                    string labelValue = ((Label)gvr.FindControl("LblStatus")).Text;
                    if (labelValue.Contains("VERIFIED"))
                    {
                        //e.Row.Cells[7].ImageUrl = "/images/Red.gif";
                        btn.Enabled = false;
                        //btn.Style.Add("opacity", "0.6");
                        //btn.Style.Add("cursor", "not-allowed");
                        gvr.Cells[7].ForeColor = Color.Green;


                    }
                }
                lblMessage.Visible = true;
                lblMessage.Text = " OTP Verfied successfully.";
                lblMessage.ForeColor = Color.Green;



                setVerifiedparty();
                GetReportReason();
                //setAdnl_Verifiedparty();

                string NewMob = "";
                if (Session["Party_Newmobileno"] != null)
                {
                    if (Session["Party_Newmobileno"].ToString() != "")
                    {
                        NewMob = Session["Party_Newmobileno"].ToString();
                    }
                }

                if (NewMob != "")
                {
                    int Appid = Convert.ToInt32(Session["AppID"].ToString());
                    try
                    {
                        DataTable dtUp = clsHearingBAL.InsertPartyNewMobNum(Appid, Convert.ToInt32(Session["partyId"]).ToString(), NewMob);
                        if (dtUp.Rows.Count > 0)
                        {
                            //
                        }
                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }

                }


                int NoticeId_1 = Convert.ToInt32(ViewState["NoticeId"].ToString());
                string AppNum = lblProposalIdHeading.Text;
                DataSet dsAllVerfiedCheck = new DataSet();

                dsAllVerfiedCheck = clsHearingBAL.GetPartyDeatil_Hearing(AppNum, NoticeId_1);
                if (dsAllVerfiedCheck != null && dsAllVerfiedCheck.Tables.Count > 0)
                {
                    DataTable partyDetailsTable = dsAllVerfiedCheck.Tables[0];
                    bool allVerified = true; // Assume all values are verified initially

                    foreach (DataRow row in partyDetailsTable.Rows)
                    {
                        if (row["OTP_status"].ToString() != "VERIFIED")
                        {
                            allVerified = false; // If any value is not verified, set the flag to false and break the loop
                            break;
                        }
                    }

                    if (allVerified)
                    {
                        ScriptManager.RegisterStartupScript(this, GetType(), "AnyValue11", "TempShow();", true);
                    }
                }






            }
            else
            {

                //ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);



                lblMessage.Visible = true;
                lblMessage.Text = "Sorry, Your OTP is Invalid. Please try again.";

                lblMessage.ForeColor = Color.Red;
                //lblMessage.Font.Bold = true;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>HideLabel();Loaderstop();</script>", false);

                //ClientScript.RegisterStartupScript(this.GetType(), "alert", "HideLabel();Loaderstop();", true);
            }



        }


        public void InsertOTPinDB()
        {
            string mob_no = "";
            int hearing_id = Convert.ToInt32(ViewState["hearing_id"].ToString());
            int partyid = Convert.ToInt32(Session["property_Id"].ToString());
            int Appid = Convert.ToInt32(Session["AppID"].ToString());
            string caseno = ViewState["Case_Number"].ToString();
            string AppNum = Session["Appno"].ToString();
            string otp = Session["OTP"].ToString();
            DateTime ctime = Convert.ToDateTime(System.DateTime.Now.ToString());
            mob_no = Session["Party_mobileno"].ToString();
            if (Session["Party_Newmob"] != null)
            {
                if (Session["Party_Newmob"].ToString() != "")
                {
                    mob_no = Session["Party_Newmob"].ToString();
                }
            }
            int Notice_ID = Convert.ToInt32(ViewState["NoticeId"].ToString());

            DataTable dtUp = clsHearingBAL.InsertPartyOTP(partyid, Appid, caseno, otp, ctime, "VERIFIED", mob_no, AppNum, hearing_id, Notice_ID);
            PartyDetail();
        }
        protected void ResendButton_Click(object sender, EventArgs e)
        {

            string newOTP = GenerateOTP();


            // Save the new OTP to the user's record in the database or session
            //Session["OTP"] = newOTP;




        }
        private string GenerateOTP()
        {

            //ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);

            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);

            // Generate a random 4-digit OTP
            // string mobileno = "8373934166";
            //if (dt.Rows.Count > -0)
            //{
            //    mobileno = dt.Rows[0]["MobileNo"].ToString().Trim();
            //}

            string mobileno = Session["Party_mobileno"].ToString();

            string NewMob = "";
            if (Session["Party_Newmobileno"] != null)
            {
                if (Session["Party_Newmobileno"].ToString() != "")
                {
                    NewMob = Session["Party_Newmobileno"].ToString();
                }
            }
            if (NewMob != "")
            {
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);
                msgdisplay.Style["display"] = "none";
                OtpDiv.Style["display"] = "block";
                dvNewMobileNum.Style["display"] = "none";
                BtnReSend.Style["display"] = "none";
                BtnValidate.Style["display"] = "block";
                mobileno = Session["Party_Newmobileno"].ToString();
            }
            txtOTP.ReadOnly = false;
            Random rnd = new Random();
            string OTP = rnd.Next(1000, 9999).ToString();
            //string mobile = txt_mobile.Text.Trim();
            //string msg = "Your one time password(OTP) for mobile verification is : '" + OTP + "'.MPSEVA";
            string Name = Session["Name"].ToString();
            string msg = "Dear '" + Name + "', please enter OTP '" + OTP + "' for User Verification on SAMPADA Portal. OTP is valid for 15 minutes.";

            //string response = SMSUtility.Send(msg, mobileno, "1307164568982837392");
            string response = SMSUtility.Send(msg, mobileno, "1407168414729061216");

            Session["RESEND_OTP"] = OTP;
            return OTP.ToString();


        }
        public void GetReportReason()
        {
            string AppNum = Session["Appno"].ToString();
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());
            DataSet dsPartyList = new DataSet();
            dsPartyList = clsHearingBAL.GetPartyListHearing(AppNum, Noticeid);
            if (dsPartyList != null)
            {
                if (dsPartyList.Tables.Count > 0)
                {

                    if (dsPartyList.Tables[0].Rows.Count > 0)
                    {


                        ViewState["PartyDetailVeri"] = dsPartyList.Tables[0];
                        ddlPartyBy.DataSource = dsPartyList.Tables[0].DefaultView;
                        ddlPartyBy.DataTextField = "Party_Name";
                        ddlPartyBy.DataValueField = "party_id";
                        ddlPartyBy.DataBind();
                        ddlPartyBy.Items.Insert(0, new ListItem("Select Party By", "0"));



                    }

                }
            }

        }


        protected void Submit(object sender, EventArgs e)
        {
            string message = "";
            foreach (ListItem item in chkContent.Items)
            {
                if (item.Selected)
                {
                    message += "Value: " + item.Value;
                    message += " Text: " + item.Text;
                    message += "\\n";
                }
            }

            string contentAdd = message;

            //lbltemplate

            Session["Content"] = contentAdd;

            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
        }
        public void GetContent()
        {

            DataSet dsPartyList = new DataSet();
            dsPartyList = clsHearingBAL.GetHearingTemplate();
            if (dsPartyList != null)
            {
                if (dsPartyList.Tables.Count > 0)
                {

                    if (dsPartyList.Tables[0].Rows.Count > 0)
                    {

                        chkContent.DataSource = dsPartyList.Tables[0].DefaultView;
                        chkContent.DataTextField = "TEMPLATE_NAME";
                        //chkContent.DataTextField = "CONTENT_VALUE";
                        chkContent.DataValueField = "TEMP_ID";
                        chkContent.DataBind();
                    }

                }
            }
        }




        protected void Button4_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageOtpVerified();</script>");
        }

        protected void Unnamed_Click(object sender, EventArgs e)
        {

        }

        protected void btnSaveCopy_Click(object sender, EventArgs e)
        {

        }

        protected void Unnamed_Click1(object sender, EventArgs e)
        {
            string message = "";
            string Template = "";
            int serial = 1;
            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();</script>", false);
            foreach (ListItem item in chkContent.Items)
            {
                item.Enabled = false;
            }

            foreach (ListItem item in chkContent.Items)
            {
                if (item.Selected)
                {

                    DataTable dt = new DataTable();
                    dt = clsHearingBAL.GetHearingTemplateById();



                    if (dt.Rows.Count > 0)
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            if (dt.Rows[i]["TEMPLATE_NAME"].ToString() == item.Text)
                            {
                                Template = dt.Rows[i]["TEMPLATE_DISCRITPION"].ToString();
                                break;
                            }
                        }
                    }


                    //message += "1: " + item.Value;
                    //message += " 1. " + item.Text;
                    message += Template;

                    //message += serial++ + ". " + Template;
                    message += "<br />";
                }
            }
            CommentAddOnSheetPnl.Visible = true;

            lbltext.Text = message;
            //lbltext1.Text = message;
            AddComtBtn.Visible = false;
            EsignSubmitBtn.Visible = true;
            int Appid = Convert.ToInt32(Session["AppID"].ToString());
            DataTable dt_get = clsHearingBAL.Get_NoticeID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
            if (dt_get.Rows.Count > 0)
            {
                string noticeIdString = dt_get.Rows[dt_get.Rows.Count - 1]["NOTICE_ID"].ToString();
                int hearing_id = Convert.ToInt32(dt_get.Rows[dt_get.Rows.Count - 1]["Hearing_id"].ToString());


                DataTable dt_Temp = clsHearingBAL.InsertTemplate_Status(Appid, hearing_id);

            }





            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + message + "');", true);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>SubmitLoader();</script>");
        }

        protected void btnAddParty_Click(object sender, EventArgs e)
        {

            if (txtName.Text == "" && txtMobile.Text == "" && txtType.Text == "")
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>PartyAlert();</script>");
            }
            else
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loader();Loaderstop();</script>", false);
                try
                {
                    Respondents objSection3 = new Respondents();



                    if (!string.IsNullOrEmpty(txtName.Text))
                        objSection3.Name = txtName.Text;

                    if (!string.IsNullOrEmpty(txtType.Text))
                        objSection3.PartyType = txtType.Text;

                    if (!string.IsNullOrEmpty(txtMobile.Text))
                        objSection3.Mobile_No = txtMobile.Text;

                    if (ddlPartyBy.SelectedIndex > 0)
                    {
                        objSection3.Party_By = ddlPartyBy.SelectedItem.Text;
                        //objSection3.Party_By = ddlPartyBy.SelectedValue;

                    }

                    if (ddlPartyBy.SelectedIndex > 0)
                    {

                        objSection3.Party_id = ddlPartyBy.SelectedValue;

                    }

                    objSection3.Type = "Respondents";
                    objSection3.QFlag = "I";
                    GrdAddParty.Visible = true;
                    GrdAddParty.DataSource = GetDataFromgvRespondentsDetailsGrid3(objSection3, 0);
                    GrdAddParty.DataBind();
                    clearText();
                    PartyBntSubmit.Visible = true;


                }
                catch (Exception ex)
                {
                    throw;
                }
            }

        }

        private void clearText()
        {
            txtName.Text = "";
            txtMobile.Text = "";
            txtType.Text = "";
        }
        private List<Respondents> GetDataFromgvRespondentsDetailsGrid3(Respondents AddInsobj, int index)
        {
            List<Respondents> _list = new List<Respondents>();
            try
            {
                foreach (GridViewRow GVR in GrdAddParty.Rows)
                {
                    Respondents _obj = new Respondents();
                    int roxIndex = GVR.RowIndex;
                    _obj.Sno = GVR.RowIndex;

                    if (!string.IsNullOrEmpty(Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Name"])))
                        _obj.Name = Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Name"].ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["PartyType"])))
                        _obj.PartyType = Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["PartyType"].ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Mobile_No"])))
                        _obj.Mobile_No = Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Mobile_No"].ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Party_By"])))
                        _obj.Party_By = Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Party_By"].ToString());

                    if (!string.IsNullOrEmpty(Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Party_id"])))
                        _obj.Party_id = Convert.ToString(GrdAddParty.DataKeys[GVR.RowIndex].Values["Party_id"].ToString());




                    _obj.QFlag = "I";
                    if (!string.IsNullOrEmpty(_obj.QFlag))
                        _list.Add(_obj);
                }

                //For Footer
                if (AddInsobj.QFlag == "I")
                    _list.Add(AddInsobj);

                else if (AddInsobj.QFlag == "D")
                {
                    if (AddInsobj.Sno >= 0)
                    {
                        _list.RemoveAt(index);
                        int count = GrdAddParty.Rows.Count;
                        if (index == 0 && count == 1)
                        {
                            GrdAddParty.Visible = false;
                            GrdAddParty.DataSource = null;
                            GrdAddParty.DataBind();
                        }
                    }
                    else
                    {
                        Respondents o = _list.Find(delegate (Respondents objDelegate) { return objDelegate.Sno == AddInsobj.Sno; });
                        o.QFlag = "D";
                    }
                }
                else if (AddInsobj.QFlag == "U")
                {
                    _list.RemoveAt(index);
                    if (AddInsobj.Sno == 0)
                        AddInsobj.QFlag = "I";
                    else
                        AddInsobj.QFlag = "U";
                    _list.Insert(index, AddInsobj);
                }

                if (_list.Count == 0)
                    _list.Add(new Respondents());
            }
            catch (Exception ex)
            {
                throw;
            }
            return _list;
#pragma warning disable CS0162 // Unreachable code detected
            PartyBntSubmit.Visible = true;
#pragma warning restore CS0162 // Unreachable code detected

        }


        protected void btnSubmit_AddParty(object sender, EventArgs e)
        {

            int Appid = Convert.ToInt32(Session["AppID"].ToString());
            string caseno = ViewState["Case_Number"].ToString();
            string AppNum = Session["Appno"].ToString();
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());
            try
            {
                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();</script>", false);

                CoSHearing_BAL cbl = new CoSHearing_BAL();

                if (GrdAddParty.Rows.Count == 0)
                {

                    ScriptManager.RegisterStartupScript(this, this.GetType(), "ex", "$.notify({title: '<strong></strong>',message: 'आवेदक की जानकारी भरें.'},{type: 'warning'});", true);
                }

                if (caseno != "")
                {
                    if (GrdAddParty.Rows.Count > 0)
                    {
                        foreach (GridViewRow row in GrdAddParty.Rows)
                        {

                            string name = row.Cells[0].Text;
                            string partyType = row.Cells[1].Text;
                            string mobileNo = row.Cells[2].Text;
                            string PartyBy = row.Cells[3].Text;
                            string Partyid = row.Cells[4].Text;

                            int Party_id = Convert.ToInt32(Partyid);


                            DataTable dt = cbl.InsertAddParty(Appid, caseno, name, partyType, "", mobileNo, PartyBy, "", "", 1, Noticeid, Party_id);

                        }
                    }
                    PartyDetail();
                    CommentPnl.Visible = true;
                    ScriptManager.RegisterStartupScript(this, GetType(), "AnyValue", "TempShow();", true);
                    PartyPnl.Visible = false;
                    Pnl_BtnAddPary.Visible = false;
                    AdditionalPartyDetail();
                    //setVerifiedparty();
                    setAdnl_Verifiedparty();
                    GetReportReason();

                }

            }
            catch (Exception ex)
            {

                throw;
            }



        }



        protected void GrdAddParty_RowDeleting1(object sender, GridViewDeleteEventArgs e)
        {
            try
            {
                Respondents delObj = new Respondents();
                if (!string.IsNullOrEmpty(GrdAddParty.DataKeys[e.RowIndex].Values["Sno"].ToString().Trim()))
                {
                    delObj.Sno = e.RowIndex;
                    delObj.QFlag = "D";
                    if (delObj.Sno >= 0)
                    {
                        delObj.QFlag = "D";
                    }
                    else
                    {
                        delObj.QFlag = string.Empty;
                    }
                }
                GrdAddParty.DataSource = GetDataFromgvRespondentsDetailsGrid3(delObj, e.RowIndex);
                GrdAddParty.DataBind();
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        protected void generate_Hearing_PDF()     //Second Notice
        {

            try
            {

                //string HEARINGDATE = txtHearingDate.Text;

                //string HEARINGDATE = txtHearingDate.Text;
                //DateTime HEARINGDATE = DateTime.Now;
                //HEARINGDATE = Convert.ToDateTime(txtHearingDate.Text);


                string HEARINGDATE;
                HEARINGDATE = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");



                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);

                string divCon = summernote.Value;


                StringBuilder strB = new StringBuilder();

                strB.Append("<div class='main-box htmldoc' style='width: 100%; margin: 0 auto; text-align: center; padding: 30px 30px 30px 30px;'>");
                strB.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblDRoffice.Text + " (म.प्र.)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>प्रारूप-अ</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>राजस्व आदेशपत्र</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>प्रकरण क्रमांक- ( " + lblCaseNumber.Text + " )  </ h2>");
                strB.Append("<br>");

                strB.Append("<table style='width: 1000px; border: 1px solid black; border-collapse: collapse;'>");

                //---------------------
                strB.Append("<tr>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>आदेश क्रमांक कार्यवाही <br> की तारीख एवं स्थान");
                strB.Append("</th>");
                strB.Append("<th style='border:1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही <br> मध्यप्रदेश शासन विरूद्ध " + lblPartyName.Text + "");
                strB.Append("</th>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पक्षों/वकीलों <br> आदेश  पालक  लिपिक के हस्ताक्षर");
                strB.Append("</th>");
                strB.Append("</tr>");

                //---------------------------------
                strB.Append("<tr style='height: 1100px;'>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "");
                strB.Append("</div>");
                strB.Append("</td>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div style='padding: 2px;'>");
                strB.Append("<p style=' text-align: center;'>");
                strB.Append("Case Number : ( " + lblCaseNumNo.Text + " ) ");
                strB.Append("<br/>");
                strB.Append("</p>");

                strB.Append(divCon);

                strB.Append("<br/>");
                //strB.Append("<hr style='width:100%; margin-left:0; border: 1px solid black'>");

                strB.Append(lbltext.Text);

                strB.Append("<br/>");
                strB.Append("<b style ='float:left; text-align: center; padding: 2px 0 5px 0;'> पेशी दिनांक <br/> " + HEARINGDATE + "");
                strB.Append("</b>");


                strB.Append("<p></p>");
                strB.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 100px; color:#fff;'>#8M2h8A4@N78O%bJd<br/></b>");
                strB.Append("<b style='float: right; text-align:center; padding:2px 0 5px 0;'>कलेक्टर ऑफ़ स्टाम्प्स,<br /> " + lblDRoffice.Text);
                strB.Append("<br/><br/>");
                strB.Append("</b>");
                strB.Append("</div>");
                strB.Append("</td>");
                //stringBuilder.Append("<td style='border:1px solid black; border-collapse: collapse; width:20%;'>" + lblVerifiedParty.Text + "</td>");

                if (ViewState["VerifiedParty"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty"].ToString()))
                {

                    if (ViewState["VerifiedParty_Behalf"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty_Behalf"].ToString()))
                    {
                        // ViewState["VerifiedParty_Behalf"] is neither null nor empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + ViewState["VerifiedParty_Behalf"].ToString() + "");

                    }
                    else
                    {
                        // ViewState["VerifiedParty_Behalf"] is null or empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + "");

                    }

                }
                else
                {
                    // ViewState["VerifiedParty_Behalf"] is null or empty
                    strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");

                }











                strB.Append("<br/><br/>");
                // strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" +  + "");
                // strB.Append("</td>");
                strB.Append("</tr>");





                strB.Append("</table>");
                strB.Append(" <br/>");
                strB.Append("</div>");



                string Proposal_ID = Session["Appno"].ToString();


                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "Hearing.pdf";
                string HearingSheetPath = Server.MapPath("~/Hearing/" + Proposal_ID);
                ViewState["Hearing_Path"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;


                //Session["Hearing"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;

                string Savedpath = ConvertHTMToPDF(FileNme, HearingSheetPath, strB.ToString());


            }
            catch (Exception ex)
            {

            }
        }


        protected void SaveHearingBtn_Click(object sender, EventArgs e)
        {

            string ProceedingData = "";
            int NoticeId_1 = Convert.ToInt32(ViewState["NoticeId"].ToString());
            string Proceeding = summernote.Value;
            string finalProceeding = Textarea_2.Value;

            string template = lbltext.Text;


            if (finalProceeding == "")
            {
                ProceedingData = Proceeding + template;
            }
            else
            {
                ProceedingData = finalProceeding + template;
            }


            int Appid = Convert.ToInt32(Session["AppID"].ToString());



            string HearingDateAA = Session["HearingDate"].ToString();



            DataTable dt_update = clsHearingBAL.Get_NoticeID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
            if (dt_update.Rows.Count > 0)
            {
                string noticeIdString = dt_update.Rows[dt_update.Rows.Count - 1]["NOTICE_ID"].ToString();
                int hearing_id = Convert.ToInt32(dt_update.Rows[dt_update.Rows.Count - 1]["Hearing_id"].ToString());

                Session["Hearing_ID"] = hearing_id;

                DataTable dtUp1 = clsHearingBAL.UpdateIntoHearing_Reader(Appid, Convert.ToDateTime(HearingDateAA), "", ProceedingData, hearing_id);


                ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> OrderSheetSave();</script>");

            }


            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop(); showAlert();</script>", false);


            btnDraftSave.Visible = false;
        }

        private void ViewHearing()
        {
            string path = Session["Hearing"] as string;
            //string path = "D:\\Mpsedc_Projects\\Sampada Project\\Sampada 29082023\\Sampada_CMS\\CMS - Sampada\\Hearing\\IGRSCMS1000105\\IGRSCMS1000105_2023Sep01032200Hearing.pdf";
            iFrame1.Src = path;
        }

        protected void btnEsignDSC_Notice_Click(object sender, EventArgs e)
        {
            int Flag = 1;

            string Hearing_ID = Session["Hearing_ID"].ToString();

            Session["Case_Number"] = ViewState["Case_Number"].ToString();
            Session["AppID"] = Session["AppID"].ToString();
            Session["Appno"] = Session["Appno"].ToString();
            Session["Hearing_ID"] = Hearing_ID;
            string Proposal_ID = Session["Appno"].ToString();

            if (ddl_SignOption_Notice.SelectedValue == "1")
            {
                if (TxtLast4Digit_Notice.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit_Notice.Focus();
                    return;
                }
                Session["HearingDate"] = lblHearingDt.Text;


                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                //-------eSign Start------------------------

                //string Location = "Project Office -" + HF_Office.Value;
                string Location = "Bhopal";

                // string ApplicationNo = hdnProposal.Value;

                string PdfName = ViewState["Hearing_Path"].ToString();
                PdfName = PdfName.Replace("~/Hearing/", "");
                //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Hearing/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

                if (File.Exists(FileNamefmFolder))
                {
                    if (ddl_SignOption_Notice.SelectedValue == "1")
                    {
                        if (TxtLast4Digit_Notice.Text.Length != 4)
                        {
                            TxtLast4Digit.Focus();
                            this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                            return;
                        }

                        string Last4DigitAadhaar = TxtLast4Digit_Notice.Text;

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

                            ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing_Notice.aspx");

                            AuthMode authMode = new AuthMode();

                            //string authType = "";

                            if (ddleAuthMode_Notice.SelectedValue == "1")
                            {
                                authMode = AuthMode.OTP;


                            }
                            else if (ddleAuthMode_Notice.SelectedValue == "2")
                            {
                                authMode = AuthMode.Biometric;

                            }



                            eSigner.eSigner _esigner = new eSigner.eSigner();


                            _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
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
                    //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Unable to Get PDF details', '', 'error')", true);
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
                Session["HearingDate"] = lblHearingDt.Text;

                //int Flag = 1;
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                //-------eSign Start------------------------

                //string Location = "Project Office -" + HF_Office.Value;
                string Location = "Bhopal";

                //string ApplicationNo = hdnProposal.Value;

                string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                PdfName = PdfName.Replace("~/OrderSheet/", "");
                //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + PdfName.ToString());
                string flSourceFile = FileNamefmFolder;

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
                            // Session["order_id"] = order_id;
                            //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                            ResponseURL_eMudra = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx");

                            //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "1", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                            AuthMode authMode = new AuthMode();

                            //string authType = "";

                            if (ddleAuthMode_Notice.SelectedValue == "1")
                            {
                                authMode = AuthMode.OTP;


                            }
                            else if (ddleAuthMode_Notice.SelectedValue == "2")
                            {
                                authMode = AuthMode.Biometric;

                            }

                            eSigner.eSigner _esigner = new eSigner.eSigner();

                            _esigner.CreateRequest_eMudra(ResponseURL_eMudra, eSignURL_eMudra, TransactionOn, txtSignedBy, Application_Id_eMudra, UIDToken, Department_Id_eMudra, Secretkey_eMudra, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
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

                        //int Flag = 1;
                        string resp_status = 1.ToString();
                        //string url = "Notice.aspx?Case_Number=" + Session["CaseNum"].ToString() + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag + "&Response_Status=" + resp_status;
                        string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status;

                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


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

            //Response.Redirect("Hearing_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Hearing_ID=" + Hearing_ID + "&Flag=" + Flag);

            //Response.Redirect("Hearing_Notice.aspx?Flag=" + Flag + "", false);

        }


        public void fill_ddlTemplate1(int userid)
        {
            DataTable dt = new DataTable();
            ddlTemplates1.Items.Clear();
            try
            {

                dt = clsHearingBAL.GET_MASTERS_PROCEEDING_TEMPLATES(userid);

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
        public void fill_ddlTemplate2(int userid)
        {
            DataTable dt = new DataTable();
            ddlTemplates2.Items.Clear();
            try
            {

                dt = clsHearingBAL.GET_MASTERS_PROCEEDING_TEMPLATES(userid);

                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddlTemplates2.Items.Add(new System.Web.UI.WebControls.ListItem(text, value));
                    }
                }

                ddlTemplates2.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Template--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }



        [WebMethod]


        public static string GetTemplate_Notice(string TemId)
        {
            CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
            string Template = "<h1>RRC1</h1>";
            DataTable dt = clsHearingBAL.GetTemplates();

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


        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAllDnld.Checked)
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentCoSDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["ALLDocAdded_Hearing"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();

                }
                else if (chkRecentDocDnld.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentCosDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(Session["RecentSheetPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else if (chkAllOrderSheetDnld.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_AllCoSDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["All_RRCOrderSheetFileNmePath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }


        protected void BtnFinalOrder_Click(object sender, EventArgs e)
        {
            //string Hearing = (ViewState["HearingDate"].ToString());

            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing, false);

            string sDate;
            sDate = lblTodate.Text;

            string ProceedingData = "";
            string Hdt = txtHearingDate.Text;

            string Proceeding = summernote.Value;
            string finalProceeding = Textarea_2.Value;


            if (finalProceeding == "")
            {
                ProceedingData = Proceeding;
            }
            else
            {
                ProceedingData = finalProceeding;
            }

            DateTime V_HEARINGDATE = Convert.ToDateTime(DateTime.Now);

            Session["HearingDt"] = V_HEARINGDATE;

            // ViewState["HearingDate"] = Convert.ToDateTime(txtHearingDate.Text).ToString("dd/MM/yyyy");


            int noticeId = Convert.ToInt32(ViewState["NoticeId"].ToString());


            int Appid = Convert.ToInt32(Session["AppID"].ToString());
            string Hearing = (ViewState["HearingDate"].ToString());
            generate_Today_PDF();
            //generate_Hearing_PDF();

            string HearingPath = ViewState["Hearing_Path"].ToString();
            //DataTable dtUp = clsHearingBAL.InsertHearing(Appid, DateTime.Now, HearingPath,summernote.Value);
            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "", false);
            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing, false);
            DataTable dtUp = clsHearingBAL.InsertHearing_Procceding_Today(Appid, ProceedingData, HearingPath, DateTime.Now, Convert.ToInt32(ViewState["hearing_id"].ToString()));

            //pnlEsignDSC.Visible = true;

            //ClientScript.RegisterClientScriptBlock(this.GetType(), "myfunction", "viewEsignDSC()", true);
            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "viewEsignDSC();DisableNoticeRbtn1();DisableFnlOrderRbtn();", true);

            BtnFinalOrder.Visible = false;
            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "", false);




        }


        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            var ci = new CultureInfo("fr-FR");
            txtHearingDate.Text = Convert.ToDateTime(Calendar1.SelectedDate.ToShortDateString()).ToString("dd/MM/yyyy", ci);
            lblHearingDt.Text = Convert.ToDateTime(Calendar1.SelectedDate.ToShortDateString()).ToString("dd/MM/yyyy", ci);

            divCalender.Visible = true;
            divCalender.Style.Add("display", "none");
            hearingdate.Style.Add("display", "block");



        }

        public void GetHearingDate()
        {
            int AppID;
            if (Session["AppID"] != null && int.TryParse(Session["AppID"].ToString(), out AppID))
            {
                DataSet dsHearingDt = clsHearingBAL.GetHearingDt(AppID);
                if (dsHearingDt != null && dsHearingDt.Tables.Count > 0 && dsHearingDt.Tables[0].Rows.Count > 0)
                {
                    string dateString = dsHearingDt.Tables[0].Rows[0]["HEARING_DATE"].ToString();
                    hdnHearingDt1.Value = dsHearingDt.Tables[0].Rows[0]["HEARING_DATE"].ToString();

                    DateTime hearingDate;
                    if (DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out hearingDate))
                    {



                    }
                    else
                    {
                        // Handle the case where dateString is not in the expected format
                    }
                }
            }
            else
            {
                // Handle the case where Session["AppID"] is null or not convertible to int
            }
        }


        protected void Calendar1_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
        {
            divCalender.Visible = true;
            divCalender.Style.Add("display", "block");
            //GetPartyDetail();
            ;
        }

        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            int DROID = Convert.ToInt32(Session["DROID"]);

            if (e.Day.Date < DateTime.Today)
            {
                // Disable the date cell
                e.Day.IsSelectable = false;
                e.Cell.ForeColor = System.Drawing.Color.Gray; // Optionally, you can change the text color for disabled dates


                e.Cell.Font.Strikeout = true;
            }
            DataSet dsList = new DataSet();


            //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
            DateTime HearingDt = Convert.ToDateTime(DateTime.Now);
            CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
            dsList = OrderSheet_BAL.GetHearingCount_COS(DROID);


            if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow DR in dsList.Tables[0].Rows)
                {
                    try
                    {
                        if (DR["HearingDate"] != null)
                        {
                            //string inputDateString = DR["HearingDate"].ToString();
                            string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];


                            DateTime hearingDate = Convert.ToDateTime(systemDate);

                            //string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
                            if (Convert.ToDateTime(e.Day.Date) == hearingDate)
                            {
                                Literal literal1 = new Literal();
                                literal1.Text = "<br/>";
                                e.Cell.Controls.Add(literal1);
                                Label label1 = new Label();
                                label1.Text = " Hearing " + Convert.ToString(DR["TotalCaseHearing"]);
                                //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                                label1.Font.Size = new FontUnit(FontSize.Small);
                                e.Cell.Controls.Add(label1);
                                e.Cell.BackColor = System.Drawing.Color.IndianRed;
                                //e.Cell.ForeColor = System.Drawing.Color.White;
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }




                }


            }

            txtHearingDt.Visible = true;
            //GetPartyDetail();
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No network adapters with an IPv4 address in the system!";
        }


        protected void Attached_Click(object sender, EventArgs e)
        {


            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();</script>", false);

            Panel2.Visible = true;


        }
        private void Reset()
        {
            CoSUpload_Doc.PostedFile.InputStream.Dispose();
            CoSUpload_Doc.Dispose();
        }
        private bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".doc", ".docx", ".pdf" };
            for (int i = 0; i <= fileExtension.Length - 1; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {
                    isValid = true;
                    break;
                }

            }
            return isValid;
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            int iFileSize = 0;

            int App_id = Convert.ToInt32(Session["AppID"].ToString());

            int hearing_id = Convert.ToInt32(ViewState["hearing_id"].ToString());

            try
            {

                if (CoSUpload_Doc.HasFiles)
                {
                    if (IsValidExtension(CoSUpload_Doc.FileName))
                    {

                        foreach (HttpPostedFile postedFile in CoSUpload_Doc.PostedFiles)
                        {
                            iFileSize = postedFile.ContentLength;
                            if (iFileSize > 5048570)  // 1MB 302941
                            {
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Maximum file size should not be more than 1 MB..!', '', 'error')", true);
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageMaxSize();</script>");
                                return;
                            }


                            string fileName = Path.GetFileName(postedFile.FileName);
                            string docpath = DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;
                            string docpath1 = "~/CoSDocument_Hearing/" + DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;

                            postedFile.SaveAs(Server.MapPath(docpath1));


                            //DataTable dt = clsNoticeBAL.InsertPartyDocReply(ViewState["Case_Number"].ToString(), docpath, "", response, Convert.ToDateTime(dates), GetLocalIPAddress(), 2, Party_R_ID);
                            DataTable dt = clsHearingBAL.InsertDoc_HearingByCos(App_id, ViewState["Case_Number"].ToString(), docpath1, GetLocalIPAddress(), hearing_id);
                            if (dt.Rows.Count > 0)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Response Saved Successfully', '', 'success')", true);
                            }
                            Reset();
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DoCUploadMsg();SetIndexVisibalTrue();</script>");
                            //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Your file has been successfully uploaded to the server. Thank you', '', 'success')", true);
                        }

                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DocTypeErrorMsg();</script>");
                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please selelct .doc, .docx, .pdf only  ..!', '', 'error')", true);
                    }

                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>NoFileMessage();</script>");
                }


            }
            catch (Exception ex)
            {

            }

        }

        protected void generate_Today_PDF()             // Select today final order
        {

            try
            {

                string HEARINGDATE;
                HEARINGDATE = lblTodate.Text;


                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);

                string divCon = summernote.Value;


                StringBuilder strB = new StringBuilder();

                strB.Append("<div class='main-box htmldoc' style='width: 100%; margin: 0 auto; text-align: center; padding: 30px 30px 30px 30px;>");
                strB.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblDRoffice.Text + " (म.प्र.)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>प्रारूप-अ</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>राजस्व आदेशपत्र</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>प्रकरण क्रमांक- ( " + lblCaseNumber.Text + " )  </ h2>");
                strB.Append("<br>");

                strB.Append("<table style='width: 1000px; border: 1px solid black; border-collapse: collapse;'>");

                //---------------------
                strB.Append("<tr>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;vertical-align: top;'>आदेश क्रमांक कार्यवाही <br> की तारीख एवं स्थान");
                strB.Append("</th>");
                strB.Append("<th style='border:1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;vertical-align: top;'>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही <br> मध्यप्रदेश शासन विरूद्ध " + lblPartyName.Text + "");
                strB.Append("</th>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;vertical-align: top;'>पक्षों/वकीलों <br> आदेश  पालक  लिपिक के हस्ताक्षर");
                strB.Append("</th>");
                strB.Append("</tr>");

                //---------------------------------
                strB.Append("<tr style='height: 1100px;'>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;vertical-align: top;'>");
                strB.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "");
                strB.Append("</div>");
                strB.Append("</td>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;vertical-align: top;'>");
                strB.Append("<div style='padding: 2px;'>");
                strB.Append("<p style=' text-align: center;'>");
                strB.Append("Case Number : ( " + lblCaseNumNo.Text + " ) ");
                strB.Append("<br/>");
                strB.Append("</p>");

                strB.Append(divCon);

                strB.Append("<br/>");
                //strB.Append("<hr style='width:100%; margin-left:0; border: 1px solid black'>");

                strB.Append(lbltext.Text);

                strB.Append("<br/>");

                strB.Append("<b style ='float:left; text-align: center; padding: 2px 0 5px 0;position: relative;top:140px'> पेशी दिनांक <br/> " + HEARINGDATE + "");
                strB.Append("</b>");

                strB.Append("<p></p>");
                strB.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 100px; color:#fff;left:-120px;'>#8M2h8A4@N78O%bJd<br/></b>");
                strB.Append("<b style='float: right; text-align:center; padding:2px 0 5px 0;position: relative;top: 140px;'>कलेक्टर ऑफ़ स्टाम्प्स,<br /> " + lblDRoffice.Text);
                strB.Append("<br/><br/>");
                strB.Append("</b>");
                strB.Append("</div>");
                strB.Append("</td>");
                //stringBuilder.Append("<td style='border:1px solid black; border-collapse: collapse; width:20%;'>" + lblVerifiedParty.Text + "</td>");

                if (ViewState["VerifiedParty"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty"].ToString()))
                {

                    if (ViewState["VerifiedParty_Behalf"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty_Behalf"].ToString()))
                    {
                        // ViewState["VerifiedParty_Behalf"] is neither null nor empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + ViewState["VerifiedParty_Behalf"].ToString() + "");

                    }
                    else
                    {
                        // ViewState["VerifiedParty_Behalf"] is null or empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + "");

                    }

                }
                else
                {
                    // ViewState["VerifiedParty_Behalf"] is null or empty
                    strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");

                }











                strB.Append("<br/><br/>");
                // strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" +  + "");
                // strB.Append("</td>");
                strB.Append("</tr>");





                strB.Append("</table>");
                strB.Append(" <br/>");
                strB.Append("</div>");



                string Proposal_ID = Session["Appno"].ToString();


                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "Hearing.pdf";
                string HearingSheetPath = Server.MapPath("~/Hearing/" + Proposal_ID);
                ViewState["Hearing_Path"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;
                Session["FileNameUnSignedPDF"] = FileNme;

                //Session["Hearing"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;

                string Savedpath = ConvertHTMToPDF(FileNme, HearingSheetPath, strB.ToString());




            }
            catch (Exception ex)
            {

            }
        }

        protected void generate_SaveLater_PDF()    //Select Later final order
        {

            try
            {


                string HEARINGDATE;
                HEARINGDATE = txtFurtherDt.Text;


                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);

                string divCon = summernote.Value;


                StringBuilder strB = new StringBuilder();

                strB.Append("<div class='main-box htmldoc' style='width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;'>");
                strB.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblDRoffice.Text + " (म.प्र.)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>प्रारूप-अ</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>राजस्व आदेशपत्र</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>प्रकरण क्रमांक- ( " + lblCaseNumber.Text + " )  </ h2>");
                strB.Append("<br>");

                strB.Append("<table style='width: 1000px; border: 1px solid black; border-collapse: collapse;'>");

                //---------------------
                strB.Append("<tr>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>आदेश क्रमांक कार्यवाही <br> की तारीख एवं स्थान");
                strB.Append("</th>");
                strB.Append("<th style='border:1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही <br> मध्यप्रदेश शासन विरूद्ध " + lblPartyName.Text + "");
                strB.Append("</th>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पक्षों/वकीलों <br> आदेश  पालक  लिपिक के हस्ताक्षर");
                strB.Append("</th>");
                strB.Append("</tr>");

                //---------------------------------
                strB.Append("<tr style='height: 1100px;'>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "");
                strB.Append("</div>");
                strB.Append("</td>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div style='padding: 2px;'>");
                strB.Append("<p style=' text-align: center;'>");
                strB.Append("Case Number : ( " + lblCaseNumNo.Text + " ) ");
                strB.Append("<br/>");
                strB.Append("</p>");

                strB.Append(divCon);

                strB.Append("<br/>");
                //strB.Append("<hr style='width:100%; margin-left:0; border: 1px solid black'>");

                strB.Append(lbltext.Text);

                strB.Append("<br/>");

                strB.Append("<b style ='float:left; text-align: center; padding: 2px 0 5px 0; position: relative;top:130px'> आदेश के लिए नियत दिनांक <br/> " + HEARINGDATE + "");
                strB.Append("</b>");





                strB.Append("<p></p>");
                strB.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; color:#fff;position: relative;top: 105px;right:50px;'>#8M2h8A4@N78O%bJd<br/><br/><br/><br/> </b>");

                strB.Append("<b style='float: right; text-align:center; padding:2px 0 5px 0;'>कलेक्टर ऑफ़ स्टाम्प्स,<br /> " + lblDRoffice.Text);
                strB.Append("<br/><br/>");
                strB.Append("</b>");
                strB.Append("</div>");
                strB.Append("</td>");
                //stringBuilder.Append("<td style='border:1px solid black; border-collapse: collapse; width:20%;'>" + lblVerifiedParty.Text + "</td>");

                if (ViewState["VerifiedParty"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty"].ToString()))
                {

                    if (ViewState["VerifiedParty_Behalf"] != null && !String.IsNullOrEmpty(ViewState["VerifiedParty_Behalf"].ToString()))
                    {
                        // ViewState["VerifiedParty_Behalf"] is neither null nor empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + ViewState["VerifiedParty_Behalf"].ToString() + "");

                    }
                    else
                    {
                        // ViewState["VerifiedParty_Behalf"] is null or empty
                        strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" + ViewState["VerifiedParty"].ToString() + "<br/><br/>" + "");

                    }

                }
                else
                {
                    // ViewState["VerifiedParty_Behalf"] is null or empty
                    strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");

                }


                strB.Append("<br/><br/>");
                // strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>" +  + "");
                // strB.Append("</td>");
                strB.Append("</tr>");





                strB.Append("</table>");
                strB.Append(" <br/>");
                strB.Append("</div>");



                string Proposal_ID = Session["Appno"].ToString();


                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "Hearing.pdf";
                string HearingSheetPath = Server.MapPath("~/Hearing/" + Proposal_ID);
                ViewState["Hearing_Path"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;
                Session["FileNameUnSignedPDF_Later"] = FileNme;

                //Session["Hearing"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;

                string Savedpath = ConvertHTMToPDF(FileNme, HearingSheetPath, strB.ToString());


            }
            catch (Exception ex)
            {

            }
        }


        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            string Proposal_ID = Session["Appno"].ToString();

            try
            {

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

                    PdfName = PdfName.Replace("~/Hearing/", "");
                    ViewState["filename"] = PdfName;
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Hearing/" + Proposal_ID + "/" + PdfName.ToString());
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


                                // ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx?Case_Number=" + ViewState["Case_Number"] + "&HearingDate=" + ViewState["HearingDate"] + "&Flag=" + "" + "&hearing_id=" + ViewState["hearing_id"] + "&NoticeId=" + ViewState["NoticeId"]);
                                ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx");
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx?Case_Number="+ViewState["Case_Number"]+"&HearingDate="+ViewState["HearingDate"]+"&Flag="+"");
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"].ToString() + "&Response_type=Notice");


                                //getdata();

                                //////DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);


                                AuthMode authMode = AuthMode.OTP;

                                eSigner.eSigner _esigner = new eSigner.eSigner();

                                _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                                //getdata_Esign();
                                string FilePath_Signed = "~/Hearing/" + ViewState["filename"].ToString();

                                int hearing_id = Convert.ToInt32(ViewState["hearing_id"].ToString());
                                Session["hearing_id"] = hearing_id;
                                //DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), FilePath_Signed);
                                //DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 1);


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

                }

                if (ddl_SignOption.SelectedValue == "2")
                {
                    if (TxtLast4Digit.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        TxtLast4Digit.Focus();
                        return;
                    }

                    //DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);
                }


            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' eSign फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);

            }
        }
        protected void btnSaveLater_Click(object sender, EventArgs e)
        {
            //if (txtFurtherDt.Text != "")
            //{
            //    int Appid = Convert.ToInt32(Session["AppID"].ToString());


            //    generate_SaveLater_PDF();

            //    string HearingPath = ViewState["Hearing_Path"].ToString();

            //    DataTable dt = new DataTable();
            //    dt = clsHearingBAL.InsertFinalOrder_SaveLaterFO(Convert.ToInt32(Session["AppID"].ToString()), ViewState["Case_Number"].ToString(), summernote.Value, Convert.ToDateTime(txtFurtherDt.Text), "", "", HearingPath);
            //    if (dt.Rows.Count > 0)
            //    {

            //        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);


            //    }



            //}
            string ProceedingData = "";
            string Proceeding = summernote.Value;
            string finalProceeding = Textarea_2.Value;

            if (finalProceeding == "")
            {
                ProceedingData = Proceeding;
            }
            else
            {
                ProceedingData = finalProceeding;
            }

            if (txtFurtherDt.Text != "")
            {
                int Appid = Convert.ToInt32(Session["AppID"].ToString());


                generate_SaveLater_PDF();
                //generate_Hearing_PDF();

                string HearingPath = ViewState["Hearing_Path"].ToString();

                DataTable dt = new DataTable();
                dt = clsHearingBAL.InsertFinalOrder_SaveLaterFO(Convert.ToInt32(Session["AppID"].ToString()), ViewState["Case_Number"].ToString(), ProceedingData, Convert.ToDateTime(txtFurtherDt.Text), "", "", HearingPath);
                if (dt.Rows.Count > 0)
                {
                    BtnFinalOrder.Visible = false;
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "viewLaterEsignDSC()();", true);
                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                }
            }
        }



        private void setVerifiedparty()
        {
            string AppNum = Session["Appno"].ToString();
            int appid = Convert.ToInt32(Session["AppID"].ToString());
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());
            string status = "VERIFIED".ToString(); ;
            StringBuilder sb = new StringBuilder();

            DataSet dsPartyDetails = new DataSet();
            dsPartyDetails = clsHearingBAL.GetPartyDeatil_Hearing_OnOrdersheet(AppNum, Noticeid, status);
            if (dsPartyDetails != null)
            {
                if (dsPartyDetails.Tables.Count > 0)
                {

                    if (dsPartyDetails.Tables[0].Rows.Count > 0)
                    {

                        foreach (DataRow row in dsPartyDetails.Tables[0].Rows)
                        {
                            //sb.Append(row["Party_Name"].ToString()).Append(", ");


                            string currentItem = row["Party_Name"].ToString();


                            if (sb.Length > 0)
                            {
                                sb.Append(", ");
                            }

                            sb.Append(currentItem);


                        }



                        string result = sb.ToString();
                        ViewState["VerifiedParty"] = "<b>Verified Party: </b>" + result;
                        //lblVerifiedParty.Text = sb.ToString();




                        //ViewState["VerifiedParty"] += "<b>Behalf Of Party : </b>" + dtApp.Rows[i]["Name"] + ",";

                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>UpdateVerifiedUser('" + ViewState["VerifiedParty"].ToString() + "');Loaderstop();</script>", false);


                    }

                }
            }


        }


        private void setAdnl_Verifiedparty()
        {
            string AppNum = Session["Appno"].ToString();
            int appid = Convert.ToInt32(Session["AppID"].ToString());
            int Noticeid = Convert.ToInt32(ViewState["NoticeId"].ToString());
            string status = "VERIFIED".ToString(); ;

            StringBuilder sb1 = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            DataSet dsPartyDetails = new DataSet();
            dsPartyDetails = clsHearingBAL.GetAdnl_PartyDeatil_Hearing_OnOrdersheet(AppNum, Noticeid);
            if (dsPartyDetails != null)
            {
                if (dsPartyDetails.Tables.Count > 0)
                {

                    if (dsPartyDetails.Tables[0].Rows.Count > 0)
                    {


                        foreach (DataRow row in dsPartyDetails.Tables[0].Rows)
                        {
                            //sb1.Append(row["ADTNL_PARTY_NAME"].ToString()).Append(", ");

                            string PartyName = row["Party_Name"].ToString();

                            string currentItem = row["ADTNL_PARTY_NAME"].ToString();



                            if (sb1.Length > 0)
                            {
                                sb1.Append(", ");
                            }
                            if (sb2.Length > 0)
                            {
                                sb2.Append(", ");
                            }

                            sb1.Append(currentItem);
                            sb2.Append(PartyName);

                            // End of loop

                            //string output = sb1.ToString();



                        }




                        string result2 = sb1.ToString();
                        string result3 = sb2.ToString();
                        ViewState["VerifiedParty_Behalf"] = result2 + "<b> :Behalf Of </b>" + result3;


                        //ViewState["VerifiedParty"] += "<b>Behalf Of Party : </b>" + dtApp.Rows[i]["Name"] + ",";

                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>UpdateVerifiedUse_behalf('" + ViewState["VerifiedParty_Behalf"].ToString() + "');</script>", false);


                    }

                }
            }


        }


        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            // Set the new page index and rebind the GridView
            grdSRDoc.PageIndex = e.NewPageIndex;
            int appid = Convert.ToInt32(Session["AppID"]);
            AllDocList(Convert.ToInt32(appid));
            //BindGridViewData();
        }
        protected void btnsave_dsc_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoSHome.aspx", false);
        }

        protected void btnLaterEsign_Click(object sender, EventArgs e)
        {
            string Proposal_ID = Session["Appno"].ToString();

            try
            {

                if (ddlLaterEsign.SelectedValue == "1")
                {
                    if (txtLaterAddhar.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        txtLaterAddhar.Focus();
                        return;
                    }
                    //-------eSign Start------------------------

                    //string Location = "Project Office -" + HF_Office.Value;
                    string Location = "Bhopal";
                    string PdfName = "";
                    //string ApplicationNo = hdnProposal.Value;
                    if (Session["FileNameUnSignedPDF_Later"] != null)
                    {
                        PdfName = Session["FileNameUnSignedPDF_Later"].ToString();
                    }

                    PdfName = PdfName.Replace("~/Hearing/", "");
                    ViewState["filename"] = PdfName;
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/Hearing/" + Proposal_ID + "/" + PdfName.ToString());
                    string flSourceFile = FileNamefmFolder;

                    if (File.Exists(FileNamefmFolder))
                    {
                        if (ddlLaterEsign.SelectedValue == "1")
                        {
                            if (txtLaterAddhar.Text.Length != 4)
                            {
                                txtLaterAddhar.Focus();
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                                return;
                            }
                            string Last4DigitAadhaar = txtLaterAddhar.Text;

                            if (File.Exists(FileNamefmFolder))
                            {
                                string ResponseURL = null;
                                string txtSignedBy = "Collector of Stamp";
                                string UIDToken = "";
                                string TransactionId = getTransactionID();
                                string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                                string TransactionOn = "Pre";


                                ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing_save_for_later.aspx?Case_Number=" + ViewState["Case_Number"] + "&HearingDate=" + ViewState["HearingDate"] + "&Flag=" + "" + "&hearing_id=" + ViewState["hearing_id"] + "&NoticeId=" + ViewState["NoticeId"]);
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx?Case_Number="+ViewState["Case_Number"]+"&HearingDate="+ViewState["HearingDate"]+"&Flag="+"");
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Hearing.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"].ToString() + "&Response_type=Notice");


                                //getdata();

                                //////DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);


                                AuthMode authMode = AuthMode.OTP;

                                eSigner.eSigner _esigner = new eSigner.eSigner();

                                _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                                //getdata_Esign();
                                string FilePath_Signed = "~/Hearing/" + ViewState["filename"].ToString();
                                int hearing_id = Convert.ToInt32(ViewState["hearing_id"].ToString());
                                Session["hearing_id"] = hearing_id;
                                //DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), FilePath_Signed);
                                //DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 1);



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

                }

                if (ddlLaterEsign.SelectedValue == "2")
                {
                    if (txtLaterAddhar.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        txtLaterAddhar.Focus();
                        return;
                    }

                    //DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);
                }


                //else
                //{
                //    string eSignDSCMessage = "Please select eSign or DSC in dropdown";
                //    string Title = "Success";
                //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + eSignDSCMessage + "','success');", true);
                //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                //    return;

                //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Select eSign or DSC in Dropdown', '', 'error')", true);
                //}



                ////getdata();
                ////pnlSend.Visible = true;
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

                //DataSet dsDocNotice;
                //dsDocNotice = clsNoticeBAL.GetNotice_Doc_Notice(Convert.ToInt32(Session["Notice_ID"]));
                //if (dsDocNotice != null)
                //{
                //    if (dsDocNotice.Tables.Count > 0)
                //    {

                //        if (dsDocNotice.Tables[0].Rows.Count > 0)
                //        {
                //            //string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                //            //Session["Recent"] = fileName.ToString();

                //            string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                //            ifRecent.Src = fileName;
                //            docPath.Visible = false;
                //            ifRecent.Visible = true;


                //        }
                //    }
                //}
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' eSign फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);

            }
        }


       

        protected void btnDraftSave_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop(); dasf();</script>", false);
            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions111", "<script>Loaderstop();PartyPnl();</script>", false);
            //PnlPartySelect.Visible = true;
            string ProceedingData = "";


            btnDraftSave.Visible = false;

            //PartyVerify_Pnl.Visible = true;



            int NoticeId_1 = Convert.ToInt32(ViewState["NoticeId"].ToString());
            string Proceeding = summernote.Value;
            string finalProceeding = Textarea_2.Value;

            if (finalProceeding == "")
            {
                ProceedingData = Proceeding;
            }
            else
            {
                ProceedingData = finalProceeding;
            }


            DateTime? date = null; // Nullable DateTime


            string HearingDateAA = Session["HearingDate"].ToString();

            int Appid = Convert.ToInt32(Session["AppID"].ToString());

            DataTable dt = clsHearingBAL.Get_NoticeID_COSForDraftProceeding(Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(ViewState["hearing_id"].ToString()));
            if (dt.Rows.Count > 0)
            {
                string noticeIdString = dt.Rows[dt.Rows.Count - 1]["NOTICE_ID"].ToString();
                int hearing_id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Hearing_id"].ToString());

                Session["Hearing_ID"] = hearing_id;

                DataTable dtUp1 = clsHearingBAL.UpdateIntoHearing_Reader(Appid, Convert.ToDateTime(HearingDateAA), "", ProceedingData, hearing_id);

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "<script>OrderSheetSave();</script>", true);
                ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> OrderSheetSave();</script>");

            }


        }

        protected void btnSaveNextHearing_Click(object sender, EventArgs e)
        {
            //ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop(); dasf();</script>", false);
            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop();</script>", false);
            //PnlPartySelect.Visible = true;
            string ProceedingData = "";

            btnDraftSave.Visible = false;
            int NoticeId_1 = Convert.ToInt32(ViewState["NoticeId"].ToString());
            string Proceeding = summernote.Value;
            string finalProceeding = Textarea_2.Value;

            if (finalProceeding == "")
            {
                ProceedingData = Proceeding;
            }
            else
            {
                ProceedingData = finalProceeding;
            }





            //DateTime? date = null; // Nullable DateTime


            //string HearingDateAA = Session["HearingDate"].ToString();
            //string NextHearingDate = txtHearingDate.Text;
            string NextHearingDate = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

            int Appid = Convert.ToInt32(Session["AppID"].ToString());

            DataTable dt = clsHearingBAL.Get_NoticeID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
            if (dt.Rows.Count > 0)
            {
                string noticeIdString = dt.Rows[dt.Rows.Count - 1]["NOTICE_ID"].ToString();
                int hearing_id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Hearing_id"].ToString());

                Session["Hearing_ID"] = hearing_id;
                DataTable dtUp = clsHearingBAL.InsertHearing_1(Appid, Convert.ToDateTime(NextHearingDate), "", ProceedingData, 0);



                //Page.ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction1", "<script>OrderSheetSave();</script>", true);
                ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> OrderSheetSave();</script>");

            }
        }


        protected void RadioButton_CheckedChanged(object sender, EventArgs e)
        {

            if (RbtnNewNum.Checked)
            {

                OtpDiv.Style["display"] = "none";
                dvNewMobileNum.Style["display"] = "block";
                BtnValidate.Style["display"] = "none";
                radioButtons.Style["display"] = "none";
                btnNewMobSave.Style["display"] = "block";
                ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);
            }

        }

        protected void btnNewMobSave_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);
            OtpDiv.Style["display"] = "block";
            dvNewMobileNum.Style["display"] = "none";
            BtnValidate.Style["display"] = "block";
            btnNewMobSave.Style["display"] = "none";
            radioButtons.Style["display"] = "none";
            txtOTP.Text = "";

            string mobileno = txtNewMob.Text;
            Session["Party_Newmobileno"] = mobileno;
            Session["property_Id"] = Session["partyId"].ToString();
            string Name = Session["Name"].ToString();
            txtOTP.ReadOnly = false;
            Random rnd = new Random();
            string OTP = rnd.Next(1000, 9999).ToString();
            lblotp.Text = OTP.ToString();
            //string Party_Name="Imam";
            string msg = "Dear '" + Name + "', please enter OTP '" + OTP + "' for User Verification on SAMPADA Portal. OTP is valid for 15 minutes.";
            //int eregId = 12345;
            //string msg = "Dear user, '" + OTP + "' is one time password for Premium Slot Fee related payment of Registry (ID: '" + eregId + "') through your SAMPADA wallet. The OTP is valid for 30 minutes.";
            //string response = SMSUtility.Send(msg, mobileno, "1407168415452536769");
            string response = SMSUtility.Send(msg, mobileno, "1407168414729061216");

            Session["OTP"] = OTP;

            string mob = mobileno.ToString();

            lblmobnum.Text = mob;
            lblmobnum.Text = string.Format("XXX XXXX") + mob.Substring(mob.Length - 4, 4);

            //lnkSendSMS.Enabled = false;
            //ClientScript.RegisterStartupScript(this.GetType(), "startCountdown", "startCountdown();", true);
            ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>$('#Receive_OTP').modal('show');Loaderstop();</script>", false);



        }

        protected void BtnNextNotice_Click(object sender, EventArgs e)
        {
            int Appid = Convert.ToInt32(Session["AppID"].ToString());
            string hearingDate = txtHearingDate.Text;
            if (txtHearingDate.Text == "")
            {

                ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>ShowMessage();Loaderstop();</script>", false);
            }


            else
            {


                DataSet dsHearingDt = clsHearingBAL.GetHearingDt(Appid);
                if (dsHearingDt != null && dsHearingDt.Tables.Count > 0 && dsHearingDt.Tables[0].Rows.Count > 0)
                {
                    string dateString = dsHearingDt.Tables[0].Rows[0]["HEARING_DATE"].ToString();
                    DateTime parsedHearingDate;
                    DateTime parsedLastHearingDate;

                    if (!DateTime.TryParseExact(hearingDate, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedHearingDate))
                    {
                        string Message = "Invalid Hearing Date format";
                        string Title = "error";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','error');", true);

                        return;

                    }

                    if (!DateTime.TryParseExact(dateString, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out parsedLastHearingDate))
                    {
                        string Message = "Invalid Last Hearing Date format.";
                        string Title = "error";
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','error');", true);

                        return;

                    }

                    if (parsedHearingDate <= parsedLastHearingDate)
                    {



                        ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>ShowMessageGreater();Loaderstop();</script>", false);
                    }
                    else
                    {
                        string sDate;
                        sDate = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                        string Hdt = txtHearingDate.Text;

                        int noticeId = Convert.ToInt32(ViewState["NoticeId"].ToString());
                        //int hearingid = Convert.ToInt32(Session["Hearing_ID"].ToString());


                        DateTime V_HEARINGDATE = Convert.ToDateTime(sDate);

                        Session["HearingDt"] = V_HEARINGDATE;

                        // ViewState["HearingDate"] = Convert.ToDateTime(txtHearingDate.Text).ToString("dd/MM/yyyy");
                        ViewState["HearingDate"] = txtHearingDate.Text;


                        DataTable dt = clsHearingBAL.Get_NoticeID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
                        if (dt.Rows.Count > 0)
                        {
                            string noticeIdString = dt.Rows[dt.Rows.Count - 1]["NOTICE_ID"].ToString();
                            int hearing_id = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Hearing_id"].ToString());

                            //Session["Hearing_ID"] = hearing_id;

                            generate_Hearing_PDF();

                            string HearingPath = ViewState["Hearing_Path"].ToString();

                            DataTable dtUp = clsHearingBAL.InsertHearing_1(Appid, V_HEARINGDATE, HearingPath, "", hearing_id);
                            Session["Hearing_ID"] = Convert.ToInt32(dtUp.Rows[0]["Hearing_id"].ToString());
                            Session["Hearing_ID_Previous"] = Convert.ToInt32(dt.Rows[dt.Rows.Count - 1]["Hearing_id"].ToString());
                            if (Session["Hearing_ID"] != null)
                            {
                                BtnNextNotice.Enabled = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "DisableRbtn_NextNotice(); disableradioBtn1();", true);
                                txtHearingDate.Visible = false;

                                BtnNextNotice.Visible = false;
                                btnDraftSave.Visible = false;
                            }


                        }
                    }

                }

            }


        }


    }

    public class Respondents
    {
        public string Name { get; set; }
        public string PartyType { get; set; }
        public string Mobile_No { get; set; }
        public string Party_By { get; set; }
        public string Party_id { get; set; }
        public int Sno { get; set; }

        public string QFlag { get; set; }
        public string Type { get; set; }


    }
}


//GetOrderSheetProceeding