using Newtonsoft.Json;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSigner;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using System.Collections;
using CMS_Sampada_BAL;
using System.Text.RegularExpressions;

namespace CMS_Sampada.CoS
{
    public partial class ReportSeeking : System.Web.UI.Page
    {
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];
        eSigner.eSigner _esigner = new eSigner.eSigner();

        string Whatsapp_URL = ConfigurationManager.AppSettings["WhatsappURL"];
        string WhatsApp_Userid = ConfigurationManager.AppSettings["WhatsAppUserid"];
        string WhatsApp_Pwd = ConfigurationManager.AppSettings["WhatsAppPwd"];
        string WhatsApp_Version = ConfigurationManager.AppSettings["WhatsAppVersion"];
        string WhatsApp_Channel = ConfigurationManager.AppSettings["WhatsAppChannel"];
        string citizenBaseUrl = ConfigurationManager.AppSettings["CitizenBaseUrl"];
        string departmentBaseUrl = ConfigurationManager.AppSettings["DepartmentBaseUrl"];

        string SmsUser = ConfigurationManager.AppSettings["SmsUser"];
        string SmsPassword = ConfigurationManager.AppSettings["SmsPassword"];
        string SmsSenderId = ConfigurationManager.AppSettings["SmsSenderId"];
        string SmssecureKey = ConfigurationManager.AppSettings["SmsSecureKey"];
        //string templateid = ConfigurationManager.AppSettings["templateid"];

        ReportSeeking_BAL ReportSeek_BAL = new ReportSeeking_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSNotice_BAL COSNotice_Bal = new CoSNotice_BAL();
        static ClsNewApplication objClsNewApplication_static = new ClsNewApplication();
        CoSFinalOrder_BAL clsFinalOrderBAL = new CoSFinalOrder_BAL();
        int appid;
        string All_OrderSheetFileNme = "";

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            hdnOfficeNameHi.Value = Session["officeNameHi"].ToString();
            //hdnSRODistNameHi.Value = Session["District_NameHI"].ToString();
            //hdnCOSDistNameHi.Value = Session["District_NameHI"].ToString();
            //hdTocan.Value = Session["Token"].ToString();
            //hdnSRONameHi.Value = Session["officeAddress"].ToString();
            //hdnCOSNameHi.Value = Session["officeAddress"].ToString();
            lblHeadingDist1.Text = Session["District_NameHI"].ToString();
            lblHeadingDist2.Text = Session["District_NameHI"].ToString();
            lblCOSOfficeNameHi.Text = Session["District_NameHI"].ToString();
            //lblSRONameHi.Text = Session["officeAddress"].ToString();
            lblCOSOfficeNameHi1.Text = Session["District_NameHI"].ToString();

            lblHeadingDist.Text = Session["District_NameHI"].ToString();

            lblCOSOfficeNameHi2.Text = Session["District_NameHI"].ToString();
            hdTocan.Value = Session["Token"].ToString();
            try
            {
                if (!Page.IsPostBack)
                {
                    ViewState["Case_Number"] = "";
                    if (Session["Case_Number"] != null)
                    {
                        ViewState["Case_Number"] = Session["Case_Number"].ToString();

                    }
                    else
                    {
                        //ViewState["Case_Number"] = "000002/B104/32/2022-23";
                    }
                    lblCaseNumber.Text = ViewState["Case_Number"].ToString();


                    ViewState["Case_Number"] = "";
                    if (Session["Case_Number"] != null)
                    {
                        ViewState["Case_Number"] = Session["Case_Number"].ToString();
                        lblCaseNumber.Text = ViewState["Case_Number"].ToString();


                        int appid = Convert.ToInt32(Session["AppID"].ToString());
                        ViewState["AppID"] = appid;

                        string Appno = Session["Appno"].ToString();
                        ViewState["Appno"] = Appno;
                        lblProposalNo.Text = ViewState["Appno"].ToString();
                        //lblProposalSub.Text = ViewState["Appno"].ToString();
                        //lblProposalSub_OtherSR.Text = ViewState["Appno"].ToString();

                        DateTime Todate = DateTime.Now;
                        lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        lblDate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        lblDate_OtherSR.Text = DateTime.Now.ToString("dd/MM/yyyy");

                        GetReportReason();
                        GetOriginalSRDetail(appid);
                        //GetOtherSRDetail(appid);
                        GetSRListDetail();
                        GetAuthorityReason();

                        hdnfldCaseNo_1.Value = ViewState["Case_Number"].ToString();
                        hdnfAppld_1.Value = Session["AppID"].ToString();
                        hdnfAppNo_1.Value = Session["Appno"].ToString();
                        //GetPartyDetail();
                        //GetPreviousProcedding();
                        //CreateAddCopyTable();
                        pnlReport.Visible = false;



                        DataSet ds_reportstatus = new DataSet();


                        //Original SR Pending
                        ds_reportstatus = ReportSeek_BAL.GetSeekReport_Status(appid);
                        if (ds_reportstatus != null)
                        {
                            if (ds_reportstatus.Tables.Count > 0)
                            {

                                if (ds_reportstatus.Tables[0].Rows.Count > 0)
                                {
                                    foreach (DataRow row in ds_reportstatus.Tables[0].Rows)
                                    {
                                        string reportStatus = row["Report_status"].ToString();
                                        if (reportStatus == "1")
                                        {
                                            if (ds_reportstatus.Tables[0].Rows[0]["Report_status"].ToString() == "1")
                                            {
                                                string status = ds_reportstatus.Tables[0].Rows[0]["Report_status"].ToString();
                                                string SIGNED_STATUS = ds_reportstatus.Tables[0].Rows[0]["SIGNED_STATUS"].ToString();
                                                string Subject_OrgSR = ds_reportstatus.Tables[0].Rows[0]["SEEK_REPORT_SUBJECT"].ToString();
                                                string Contant_OrgSR = ds_reportstatus.Tables[0].Rows[0]["SEEK_REPORT_CONTENT"].ToString();
                                                string REASON_OrgSR = ds_reportstatus.Tables[0].Rows[0]["REASON"].ToString();
                                                Session["UNSIGNED_PDF_OrgSR"] = ds_reportstatus.Tables[0].Rows[0]["UNSIGNED_PDF_PATH"].ToString();
                                                Session["Order_Id_org"] = ds_reportstatus.Tables[0].Rows[0]["REPORT_ID"].ToString();

                                                DataTable dt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                                                if (dt.Rows[0]["STATUS_ID"].ToString() == "16")
                                                {
                                                    string Signed_path = ds_reportstatus.Tables[0].Rows[0]["signed_pdf_path"].ToString();
                                                    docPath.Visible = false;
                                                    IfProceeding.Visible = true;
                                                    IfProceeding.Src = Signed_path.ToString();
                                                    custom_tabs_on_profile_tab.Attributes["class"] = "nav-link disabled";
                                                    ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> OriginalSatutus();AddReport();</script>", false);
                                                }
                                                else
                                                {
                                                    if (status == "1" && SIGNED_STATUS == "1")
                                                    {
                                                        string Signed_path = ds_reportstatus.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                                                        IfProceeding.Src = Signed_path.ToString();
                                                        pnlEsignDSC.Visible = false;
                                                        pnlSendCurSR.Visible = true;
                                                        summernote3.Value = Contant_OrgSR.ToString();
                                                        pContent.InnerHtml = summernote3.InnerHtml;
                                                        lblReasonSub_OrgSR.Text = Subject_OrgSR.ToString();
                                                        ddlOrgReason.DataSource = REASON_OrgSR;
                                                        ddlOrgReason.SelectedValue = REASON_OrgSR;
                                                        ddlOrgReason.Enabled = false;

                                                        //pContent.InnerHtml = summernote3.Value;
                                                        pnlBtnSaveReport.Visible = false;

                                                        btnCreateReport.Visible = false;
                                                        btnEdit_Report_OriginalSR.Visible = false;
                                                        txtOrgOtherReason.ReadOnly = true;
                                                        pnlReport.Visible = true;

                                                    }
                                                    else
                                                    {
                                                        GetReportReason();
                                                        summernote3.Value = Contant_OrgSR.ToString();
                                                        pContent.InnerHtml = summernote3.InnerHtml;
                                                        lblReasonSub_OrgSR.Text = Subject_OrgSR.ToString();
                                                        ddlOrgReason.DataSource = REASON_OrgSR;
                                                        ddlOrgReason.SelectedValue = REASON_OrgSR;
                                                        ddlOrgReason.Enabled = false;
                                                        //pContent.InnerHtml = ViewState["SeekReportContent"].ToString();
                                                        //pContent.InnerHtml = summernote3.Value;
                                                        pnlBtnSaveReport.Visible = false;
                                                        pnlEsignDSC.Visible = true;
                                                        btnCreateReport.Visible = false;
                                                        btnEdit_Report_OriginalSR.Visible = false;
                                                        txtOrgOtherReason.ReadOnly = true;
                                                        pnlReport.Visible = true;


                                                    }
                                                }
                                            }
                                        }
                                        //string reportStatus = row["Report_status"].ToString();
                                        if (reportStatus == "2")
                                        {
                                            string status = row["Report_status"].ToString();
                                            string SIGNED_STATUS = row["SIGNED_STATUS"].ToString();

                                            if (status == "2")
                                            {
                                                string Subject_CrntSR = row["SEEK_REPORT_SUBJECT"].ToString();
                                                string Contant_CrntSR = row["SEEK_REPORT_CONTENT"].ToString();
                                                string REASON_CrntSR = row["REASON"].ToString();

                                                string CrntSR_Id = row["SRO_ID"].ToString();
                                                Session["UNSIGNED_PDF_CrntSR"] = row["UNSIGNED_PDF_PATH"].ToString();
                                                Session["Order_Id_Crnt"] = row["REPORT_ID"].ToString();

                                                ddlSRName.Enabled = false;
                                                summernote3.Value = Contant_CrntSR.ToString();
                                                pSRContent.InnerHtml = summernote3.Value;
                                                lblReasonSub_CurrntSR.Text = Subject_CrntSR.ToString();
                                                ddlSRReason.SelectedValue = REASON_CrntSR;
                                                ddlSRReason.Enabled = false;
                                                pnlCurSRESign.Visible = true;
                                                pnlSendCurSR.Visible = false;

                                                pContent.InnerHtml = summernote3.Value;
                                                pnlBtnSaveReport_crnt.Visible = false;

                                                btnCreateReportCurrent.Visible = false;
                                                btnEdit_Report_CurrentSR.Visible = false;
                                                txtCrntOtherReason.ReadOnly = true;
                                                pnlSRReport.Visible = true;
                                                custom_tabs_on_profile_tab.Attributes["class"] = "nav-link disabled";
                                                ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> OriginalSatutus();AddReport();</script>", false);

                                                DataTable dt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                                                if (dt.Rows[0]["STATUS_ID"].ToString() == "20")
                                                {
                                                    string Signed_path = row["signed_pdf_path"].ToString();
                                                    docPath.Visible = false;
                                                    IfProceeding.Visible = true;
                                                    IfProceeding.Src = Signed_path.ToString();
                                                    //    custom_tabs_one_home_tab.Attributes["class"] = "nav-link disabled";
                                                    //    ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> CurrentSatutus();</script>", false);
                                                }

                                                int SelectedSRName = Convert.ToInt32(CrntSR_Id);
                                                DataTable dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, SelectedSRName, 0);
                                                if (dsCurSRDetail != null && dsCurSRDetail.Rows.Count > 0)
                                                {
                                                    ddlSRName.SelectedValue = SelectedSRName.ToString();
                                                    Session["ddlSRName_Current"] = ddlSRName.SelectedValue;
                                                    ddlSRName.Enabled = false;
                                                    txtSROID.Text = dsCurSRDetail.Rows[0]["ID"].ToString();
                                                    txtSRDesignation.Text = "उप रजिस्ट्रार (एस आर)";
                                                    txtSREmail.Text = dsCurSRDetail.Rows[0]["EMAIL"].ToString();
                                                    txtSRMobile.Text = dsCurSRDetail.Rows[0]["MOBILE_NO"].ToString();
                                                    txtSROName.Text = dsCurSRDetail.Rows[0]["office_name_hi"].ToString();
                                                    txtSROOfficeAdd.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();
                                                    lblSRDesign.Text = "उप रजिस्ट्रार (एस आर)";
                                                    lblToSROOffice.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();
                                                    lblSRAddress.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();

                                                    grdPartyDisplay_CurrentSR.DataSource = dsCurSRDetail;
                                                    grdPartyDisplay_CurrentSR.DataBind();
                                                }

                                                if (status == "2" && SIGNED_STATUS == "1")
                                                {
                                                    string Signed_path = row["signed_pdf_path"].ToString();
                                                    Session["Current_SignPath"] = row["signed_pdf_path"].ToString();
                                                    Session["RecentSheetPath_Current"] = row["UNSIGNED_PDF_PATH"].ToString();
                                                    Session["UNSIGNED_PDF_CrntSR"] = row["UNSIGNED_PDF_PATH"].ToString();
                                                    docPath.Visible = false;
                                                    IfProceeding.Visible = true;
                                                    IfProceeding.Src = Signed_path.ToString();
                                                    pnlCurSRESign.Visible = false;
                                                    pnlSRSendParty.Visible = true;


                                                }
                                                else
                                                {

                                                }
                                            }
                                        }
                                    }



                                }

                            }
                        }


                        //Current SR Pending
                        ds_reportstatus = ReportSeek_BAL.GetSeekReport_Status(appid);
                        if (ds_reportstatus != null)
                        {
                            if (ds_reportstatus.Tables.Count > 0)
                            {

                                if (ds_reportstatus.Tables[0].Rows.Count > 0)
                                {





                                }

                            }
                        }


                        //Other SR Pending
                        ds_reportstatus = ReportSeek_BAL.GetSeekReport_Status_forOther(appid);
                        if (ds_reportstatus != null)
                        {
                            if (ds_reportstatus.Tables.Count > 0)
                            {

                                if (ds_reportstatus.Tables[0].Rows.Count > 0)
                                {
                                    if (ds_reportstatus.Tables[0].Rows[0]["Report_status"].ToString() == "3")
                                    {
                                        DataSet ds_reportstatus_Current = new DataSet();
                                        ds_reportstatus_Current = ReportSeek_BAL.GetSeekReport_Status_forOther(appid);
                                        if (ds_reportstatus_Current != null)
                                        {
                                            if (ds_reportstatus_Current.Tables.Count > 0)
                                            {

                                                if (ds_reportstatus_Current.Tables[0].Rows.Count > 0)
                                                {
                                                    string status = ds_reportstatus_Current.Tables[0].Rows[0]["Report_status"].ToString();
                                                    string SIGNED_STATUS = ds_reportstatus.Tables[0].Rows[0]["SIGNED_STATUS"].ToString();

                                                    if (status == "3")
                                                    {
                                                        //string SIGNED_STATUS = ds_reportstatus.Tables[0].Rows[0]["SIGNED_STATUS"].ToString();
                                                        string Subject_OtherSR = ds_reportstatus.Tables[0].Rows[0]["SEEK_REPORT_SUBJECT"].ToString();
                                                        string Contant_OtherSR = ds_reportstatus.Tables[0].Rows[0]["SEEK_REPORT_CONTENT"].ToString();
                                                        string OtherSR_Id = ds_reportstatus.Tables[0].Rows[0]["SRO_ID"].ToString();
                                                        string REASON_OtherSR = ds_reportstatus.Tables[0].Rows[0]["REASON"].ToString();
                                                        Session["UNSIGNED_PDF_OtherSR"] = ds_reportstatus.Tables[0].Rows[0]["UNSIGNED_PDF_PATH"].ToString();
                                                        Session["Order_Id_Other"] = ds_reportstatus.Tables[0].Rows[0]["ID"].ToString();


                                                        //IfProceeding.Src = Signed_path.ToString();
                                                        ddlSRName.Enabled = false;
                                                        summernote3.Value = Contant_OtherSR.ToString();
                                                        pContent_OtherSR.InnerHtml = summernote3.InnerHtml;
                                                        lblReasonSub_OtherSR.Text = Subject_OtherSR.ToString();
                                                        ddlOrgReason_other.SelectedValue = REASON_OtherSR;
                                                        ddlOrgReason_other.Enabled = false;
                                                        PnlEsign_OtherSR.Visible = true;
                                                        PnlSend_OtherSR.Visible = false;

                                                        pContent_OtherSR.InnerHtml = summernote3.Value;
                                                        btnSaveReport_OtherSR.Visible = false;

                                                        btnCreateReportOtherSR.Visible = false;
                                                        btnEditSROther.Visible = false;
                                                        txtOrgOtherReason_OtherSR.ReadOnly = true;
                                                        pnlReport_OtherSR.Visible = true;
                                                        custom_tabs_on_profile_tab.Attributes["class"] = "nav-link disabled";
                                                        //ScriptManager.RegisterStartupScript(pnlSRSendParty, pnlSRSendParty.GetType(), "none", "<script> CurrentSatutus();</script>", false);
                                                        ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>CurrentSatutus();</script>", false);



                                                        DataTable dt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                                                        if (dt.Rows[0]["STATUS_ID"].ToString() == "22")
                                                        {
                                                            PnlSend_OtherSR.Visible = true;
                                                            //string Signed_path = ds_reportstatus.Tables[0].Rows[0]["signed_pdf_path"].ToString();
                                                            //docPath.Visible = false;
                                                            //IfProceeding.Visible = true;
                                                            //IfProceeding.Src = Signed_path.ToString();
                                                            //custom_tabs_one_home_tab.Attributes["class"] = "nav-link disabled";
                                                            //ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> CurrentSatutus();</script>", false);

                                                        }


                                                        DataTable dt_other = ReportSeek_BAL.Get_OtherDeatails(Convert.ToInt32(Session["Order_Id_Other"]));

                                                        string casenumber = ViewState["Case_Number"].ToString();
                                                        //ViewState["PartyDetail"] = dt;
                                                        if (dt_other.Rows.Count > 0)
                                                        {

                                                            int SelectedSRName = Convert.ToInt32(OtherSR_Id);
                                                            ddlAuthority.SelectedValue = SelectedSRName.ToString();

                                                            //ddlAuthority.Text = dt_other.Rows[0]["hearingdate"].ToString();
                                                            txtName.Text = dt_other.Rows[0]["AUTHORITY_NAME"].ToString();
                                                            txtDegisnation.Text = dt_other.Rows[0]["DESIGNATION"].ToString();
                                                            txtemail.Text = dt_other.Rows[0]["EMAIL_ID"].ToString();
                                                            txtphoneNo.Text = dt_other.Rows[0]["PHONE_NO"].ToString();
                                                            txtWhatsapp.Text = dt_other.Rows[0]["WHATSAPP_NO"].ToString();
                                                            txtAddress.Text = dt_other.Rows[0]["OFFICE_ADDRESS"].ToString();
                                                            //txtOtherAuthority.Text = dt_other.Rows[0]["CASE_NUMBER"].ToString();
                                                            lblToDesig_otherSR.Text = dt_other.Rows[0]["DESIGNATION"].ToString();
                                                            lblToSRO_otherSR.Text = dt_other.Rows[0]["AUTHORITYNAME_HI"].ToString();
                                                            lblToAdd_otherSR.Text = dt_other.Rows[0]["OFFICE_ADDRESS"].ToString();

                                                        }





                                                        //ddlOrgReason.SelectedItem.Value = Session["SelectedReason_Org"].ToString();
                                                        ddlOrgReason_other.Enabled = false;
                                                        ddlOrgReason_other.Enabled = false;
                                                        pnlReport_OtherSR.Visible = true;
                                                        //PnlSend_OtherSR.Visible = true;
                                                        btnCreateReportOtherSR.Visible = false;
                                                        btnSaveReport_OtherSR.Visible = false;



                                                        ddlAuthority.Enabled = false;
                                                        txtName.ReadOnly = true;
                                                        txtDegisnation.ReadOnly = true;
                                                        txtemail.ReadOnly = true;
                                                        txtphoneNo.ReadOnly = true;
                                                        txtWhatsapp.ReadOnly = true;
                                                        txtAddress.ReadOnly = true;
                                                        txtOtherAuthority.ReadOnly = true;

                                                    }
                                                    if (status == "3" && SIGNED_STATUS == "1")
                                                    {

                                                        string Signed_path = ds_reportstatus.Tables[0].Rows[0]["signed_pdf_path"].ToString();
                                                        Session["UNSIGNED_PDF_CrntSR"] = ds_reportstatus.Tables[0].Rows[0]["UNSIGNED_PDF_PATH"].ToString();
                                                        docPath.Visible = false;
                                                        IfProceeding.Visible = true;
                                                        IfProceeding.Src = Signed_path.ToString();
                                                        GetOtherSRDetail(Convert.ToInt32(ViewState["AppID"].ToString()));
                                                        PnlEsign_OtherSR.Visible = false;
                                                    }
                                                }

                                            }
                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            string reasonforreport = ddlOrgReason.SelectedItem.Text;
                            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + reasonforreport + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";
                            summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + reasonforreport + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";
                            //summernote3_OtherSR.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + reasonforreport + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";

                            //summernotesubject.Value = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + lblReasonSub.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                            summernotesubject.Value = ddlOrgReason.SelectedItem.Text + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";

                            ViewState["SeekReportContent"] = summernote3.InnerHtml;
                        }







                        DataSet dsDocRecent = new DataSet();

                        DataSet dsDocDetails = new DataSet();

                        dsDocDetails = ReportSeek_BAL.GetRegisteredDate(ViewState["Case_Number"].ToString());
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

                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddReport();</script>");
                    }
                    else
                    {
                        //ViewState["Case_Number"] = "000002/B104/32/2022-23";
                    }
                    DataSet dsRecent = new DataSet();
                    dsRecent = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), ViewState["AppID"].ToString());
                    if (dsRecent != null)
                    {
                        if (dsRecent.Tables.Count > 0)
                        {

                            if (dsRecent.Tables[0].Rows.Count > 0)
                            {
                                string fileName = dsRecent.Tables[0].Rows[0]["PROPOSALPATH_SECONDFORMATE"].ToString();
                                Session["RecentSheetPath"] = fileName.ToString();
                                docPath.Src = fileName;
                            }
                        }
                    }
                    Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                    All_OrderSheetFileNme = Session["All_DocSheet"].ToString();
                    CreateEmptyFile(All_OrderSheetFileNme);
                    AllDocList(Convert.ToInt32(ViewState["AppID"]));
                    CraetSourceFile(Convert.ToInt32(ViewState["AppID"]));


                    int Flag = 0;
                    if (Request.QueryString["Flag"] != null)
                    {
                        Flag = Convert.ToInt32(Request.QueryString["Flag"]);
                        if (Request.QueryString["Flag"].ToString() == "1" || Request.QueryString["Flag"].ToString() == "2")
                        {
                            if (Request.QueryString["Response_type"] != null)
                            {
                                if (Request.QueryString["Response_type"].ToString() == "OriginalSR_Seek_Report")
                                {
                                    int App_ID = Convert.ToInt32(Session["AppID"].ToString());
                                    //int seekreoprt_id_original = Convert.ToInt32(Session["seekreoprt_id_OrgSR"].ToString());
                                    int seekreoprt_id_original = Convert.ToInt32(Session["Order_Id_org"].ToString());
                                    DataTable dt = ReportSeek_BAL.InserteSignDSC_Status(App_ID, "1", "", GetLocalIPAddress(), seekreoprt_id_original);





                                    lblReasonSub_OrgSR.Text = "";
                                    //ddlOrgReason.SelectedItem.Value = Session["SelectedReason_Org"].ToString();
                                    ddlOrgReason.Enabled = false;
                                    pnlEsignDSC.Visible = false;
                                    pnlReport.Visible = true;
                                    pnlSendCurSR.Visible = true;
                                    btnCreateReport.Visible = false;
                                    docPath.Visible = false;
                                    IfProceeding.Visible = true;
                                    AllDocList(Convert.ToInt32(ViewState["AppID"]));
                                    DataSet dtPro = ReportSeek_BAL.Show_OriginalSR_SignedPath(App_ID, 1);
                                    if (dtPro != null)
                                    {
                                        if (dtPro.Tables.Count > 0)
                                        {
                                            IfProceeding.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();



                                            //string Subject_Org11 = Session["SelectedReason_Org"].ToString();
                                            string Subject_Org11 = dtPro.Tables[0].Rows[0]["SEEK_REPORT_SUBJECT"].ToString();
                                            string Contant_OrgSR = dtPro.Tables[0].Rows[0]["SEEK_REPORT_CONTENT"].ToString();
                                            string REASON_OrgSR = dtPro.Tables[0].Rows[0]["REASON"].ToString();

                                            ddlOrgReason.ClearSelection();

                                            //lblReasonSub.Text = Subject_Org11.ToString();



                                            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + Session["SelectedReason_Org"].ToString() + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";
                                            //summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + Session["SelectedReason_Org"].ToString() + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";
                                            summernote3.Value = Contant_OrgSR.ToString();
                                            pContent.InnerHtml = summernote3.InnerHtml;
                                            lblReasonSub_OrgSR.Text = Subject_Org11.ToString();
                                            ddlOrgReason.DataSource = REASON_OrgSR;
                                            ddlOrgReason.SelectedValue = REASON_OrgSR;
                                            ddlOrgReason.Enabled = false;



                                        }
                                    }
                                }

                                if (Request.QueryString["Response_type"].ToString() == "CurrentSR_Seek_Report")
                                {

                                    int App_ID = Convert.ToInt32(Session["AppID"].ToString());
                                    //int seekreoprt_id_current = Convert.ToInt32(Session["seekreoprt_id_CrntSR"].ToString());
                                    int seekreoprt_id_current = Convert.ToInt32(Session["Order_Id_Crnt"].ToString());
                                    DataTable dt = ReportSeek_BAL.InserteSignDSC_Status_Current(App_ID, "1", "", GetLocalIPAddress(), seekreoprt_id_current);

                                    int SelectedSRName = Convert.ToInt32(Session["ddlSRName_Current"].ToString());

                                    ddlSRName.ClearSelection();

                                    ListItem selectedItem_1 = ddlSRName.Items.FindByValue(SelectedSRName.ToString());

                                    if (selectedItem_1 != null)
                                    {
                                        selectedItem_1.Selected = true;

                                    }

                                    DataTable dsCurSRDetail = new DataTable();
                                    dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(SelectedSRName), 0);
                                    if (dsCurSRDetail != null)
                                    {
                                        if (dsCurSRDetail.Rows.Count > 0)
                                        {
                                            txtSROID.Text = dsCurSRDetail.Rows[0]["ID"].ToString();
                                            //txtSRDesignation.Text = dsCurSRDetail.Rows[0]["DESIGNATION_HI"].ToString();
                                            txtSRDesignation.Text = "उप रजिस्ट्रार (एस आर)";
                                            txtSREmail.Text = dsCurSRDetail.Rows[0]["EMAIL"].ToString();
                                            txtSRMobile.Text = dsCurSRDetail.Rows[0]["MOBILE_NO"].ToString();
                                            txtSROName.Text = dsCurSRDetail.Rows[0]["office_name_hi"].ToString();
                                            txtSROOfficeAdd.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();
                                            //lblSRDesign.Text = dsCurSRDetail.Rows[0]["DESIGNATION_HI"].ToString();
                                            lblSRDesign.Text = "उप रजिस्ट्रार (एस आर)";
                                            lblToSROOffice.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();
                                            lblSRAddress.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();

                                            grdPartyDisplay_CurrentSR.DataSource = dsCurSRDetail;
                                            grdPartyDisplay_CurrentSR.DataBind();

                                        }
                                    }




                                    //string Subject_Crnt = Session["SeekReportSubject_Current"].ToString();
                                    //ddlSRReason.ClearSelection();



                                    //lblSRSUB.Text = Subject_Crnt.ToString();

                                    //ListItem selectedItem = ddlSRReason.Items.FindByText(Subject_Crnt);

                                    //if (selectedItem != null)
                                    //{
                                    //    selectedItem.Selected = true;
                                    //    //ddlOrgReason.SelectedItem.Text = "";
                                    //}


                                    pnlCurSRESign.Visible = false;
                                    //ddlSRReason.SelectedItem.Value = Session["SeekReportSubject_Current"].ToString();
                                    ddlSRReason.Enabled = false;
                                    pnlSRReport.Visible = true;
                                    pnlSRSendParty.Visible = true;
                                    btnCreateReportCurrent.Visible = false;
                                    ddlSRName.Enabled = false;

                                    btnSRSaveReport.Visible = false;


                                    docPath.Visible = false;
                                    IfProceeding.Visible = false;
                                    IfProceedingCrnt.Visible = true;
                                    AllDocList(Convert.ToInt32(ViewState["AppID"]));
                                    DataSet dtPro = ReportSeek_BAL.Show_OriginalSR_SignedPath(App_ID, 2);
                                    if (dtPro != null)
                                    {
                                        if (dtPro.Tables.Count > 0)
                                        {

                                            IfProceedingCrnt.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                                            Session["Current_SignPath"] = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();


                                            string Subject_Curnt11 = dtPro.Tables[0].Rows[0]["SEEK_REPORT_SUBJECT"].ToString();
                                            string Contant_CurntSR = dtPro.Tables[0].Rows[0]["SEEK_REPORT_CONTENT"].ToString();

                                            ddlOrgReason.ClearSelection();




                                            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + Session["SelectedReason_Org"].ToString() + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";
                                            //summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + Session["SelectedReason_Org"].ToString() + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";
                                            summernote3.Value = Contant_CurntSR.ToString();
                                            pSRContent.InnerHtml = summernote3.Value;
                                            lblReasonSub_CurrntSR.Text = Subject_Curnt11.ToString();



                                        }
                                    }



                                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> eSignPageLoad_CurrentSR();</script>");

                                    ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> OriginalSatutus();</script>", false);
                                }


                                if (Request.QueryString["Response_type"].ToString() == "Other_Seek_Report")
                                {
                                    int App_ID = Convert.ToInt32(Session["AppID"].ToString());
                                    //int seekreoprt_id_other = Convert.ToInt32(Session["seekreoprt_id_OtherSR"].ToString());
                                    int seekreoprt_id_other = Convert.ToInt32(Session["Order_Id_Other"].ToString());
                                    DataTable dt = ReportSeek_BAL.InserteSignDSC_Status_Other(App_ID, "1", "", GetLocalIPAddress(), seekreoprt_id_other);




                                    int O_Id = Convert.ToInt32(Session["seekreoprt_id_OtherSR"].ToString());
                                    //DataTable dt1 = clsNoticeBAL.InserteSignDSC_Status_NoticeProceeding(Convert.ToInt32(Session["AppID"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["NoticeID"].ToString()));

                                    DataTable dt_other = ReportSeek_BAL.Get_OtherDeatails(O_Id);

                                    string casenumber = ViewState["Case_Number"].ToString();
                                    //ViewState["PartyDetail"] = dt;
                                    if (dt_other.Rows.Count > 0)
                                    {



                                        //ddlAuthority.Text = dt_other.Rows[0]["hearingdate"].ToString();
                                        txtName.Text = dt_other.Rows[0]["AUTHORITY_NAME"].ToString();
                                        txtDegisnation.Text = dt_other.Rows[0]["DESIGNATION"].ToString();
                                        txtemail.Text = dt_other.Rows[0]["EMAIL_ID"].ToString();
                                        txtphoneNo.Text = dt_other.Rows[0]["PHONE_NO"].ToString();
                                        txtWhatsapp.Text = dt_other.Rows[0]["WHATSAPP_NO"].ToString();
                                        txtAddress.Text = dt_other.Rows[0]["OFFICE_ADDRESS"].ToString();
                                        //txtOtherAuthority.Text = dt_other.Rows[0]["CASE_NUMBER"].ToString();
                                        lblToDesig_otherSR.Text = dt_other.Rows[0]["DESIGNATION"].ToString();
                                        lblToSRO_otherSR.Text = dt_other.Rows[0]["AUTHORITYNAME_HI"].ToString();
                                        lblToAdd_otherSR.Text = dt_other.Rows[0]["OFFICE_ADDRESS"].ToString();

                                    }





                                    //ddlOrgReason.SelectedItem.Value = Session["SelectedReason_Org"].ToString();
                                    ddlOrgReason_other.Enabled = false;
                                    ddlOrgReason_other.Enabled = false;
                                    pnlReport_OtherSR.Visible = true;
                                    PnlSend_OtherSR.Visible = true;
                                    btnCreateReportOtherSR.Visible = false;
                                    btnSaveReport_OtherSR.Visible = false;
                                    PnlEsign_OtherSR.Visible = false;


                                    ddlAuthority.Enabled = false;
                                    txtName.ReadOnly = true;
                                    txtDegisnation.ReadOnly = true;
                                    txtemail.ReadOnly = true;
                                    txtphoneNo.ReadOnly = true;
                                    txtWhatsapp.ReadOnly = true;
                                    txtAddress.ReadOnly = true;
                                    txtOtherAuthority.ReadOnly = true;



                                    docPath.Visible = false;
                                    IfProceeding.Visible = false;
                                    IfProceedingCrnt.Visible = false;
                                    IfProceedingOther.Visible = true;
                                    AllDocList(Convert.ToInt32(ViewState["AppID"]));
                                    DataSet dtPro = ReportSeek_BAL.Show_OtherSR_SignedPath(App_ID, 3);
                                    if (dtPro != null)
                                    {
                                        if (dtPro.Tables.Count > 0)
                                        {

                                            IfProceedingOther.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                                            Session["Other_SignPath"] = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();


                                            string Subject_OtherSR = dtPro.Tables[0].Rows[0]["SEEK_REPORT_SUBJECT"].ToString();
                                            string Contant_OtherSR = dtPro.Tables[0].Rows[0]["SEEK_REPORT_CONTENT"].ToString();

                                            string REASON_OtherSR = dtPro.Tables[0].Rows[0]["REASON"].ToString();


                                            ddlOrgReason_other.SelectedValue = REASON_OtherSR;
                                            ddlOrgReason.Enabled = false;

                                            //lblReasonSub_OtherSR.Text = Subject_Other.ToString();



                                            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + Session["SelectedReason_Other"].ToString() + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";
                                            //summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + Session["SelectedReason_Other"].ToString() + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";
                                            pContent_OtherSR.InnerHtml = summernote3.InnerHtml;


                                            summernote3.Value = Contant_OtherSR.ToString();
                                            pContent_OtherSR.InnerHtml = summernote3.InnerHtml;
                                            lblReasonSub_OtherSR.Text = Subject_OtherSR.ToString();

                                            ListItem selectedItem = ddlOrgReason_other.Items.FindByText(Subject_OtherSR);

                                            if (selectedItem != null)
                                            {
                                                selectedItem.Selected = true;
                                                //ddlOrgReason.SelectedItem.Text = "";
                                            }
                                        }
                                    }



                                    GetOtherSRDetail(App_ID);


                                    ScriptManager.RegisterStartupScript(PnlSend_OtherSR, PnlSend_OtherSR.GetType(), "none", "<script> OtherStatus();</script>", false);
                                    ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript11", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    
                                                                    $('#custom-tabs-one-RegisteredForm').hide();
                                                                });
                                                            </script>");



                                }

                            }
                        }
                        else if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "OriginalSR_Seek_Report")
                        {
                            ddlOrgReason.Enabled = false;
                            pContent.InnerHtml = summernote3.Value;
                            pnlBtnSaveReport.Visible = false;
                            pnlEsignDSC.Visible = true;
                            btnCreateReport.Visible = false;
                            btnEdit_Report_OriginalSR.Visible = false;
                            txtOrgOtherReason.ReadOnly = true;
                        }

                        else if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "CurrentSR_Seek_Report")
                        {
                            pSRContent.InnerHtml = summernote3.Value;
                            //pnlBtnSaveReport.Visible = false;
                            //pnlEsignDSC.Visible = true;
                            //btnCreateReport.Visible = false;
                            pnlCurSRESign.Visible = true;
                            btnSRSaveReport.Visible = false;
                            btnCreateReportCurrent.Visible = false;
                            btnEdit_Report_CurrentSR.Visible = false;
                            txtOrgOtherReason.ReadOnly = true;

                            ddlSRName.Enabled = false;
                            ddlSRReason.Enabled = false;
                        }

                        else if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "Other_Seek_Report")
                        {
                            pContent_OtherSR.InnerHtml = summernote3.Value;

                            PnlEsign_OtherSR.Visible = true;
                            btnSaveReport_OtherSR.Visible = false;
                            btnCreateReportOtherSR.Visible = false;
                            txtOrgOtherReason_OtherSR.ReadOnly = true;
                            ddlAuthority.Enabled = false;
                            txtName.ReadOnly = true;
                            txtDegisnation.ReadOnly = true;
                            txtemail.ReadOnly = true;
                            txtphoneNo.ReadOnly = true;
                            txtWhatsapp.ReadOnly = true;
                            txtAddress.ReadOnly = true;
                            txtOtherAuthority.ReadOnly = true;
                            ddlOrgReason_other.Enabled = false;
                            //custom_tabs_one_other_tab.Attributes["class"] = "nav-link disabled";

                            ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript11", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    
                                                                    $('#custom-tabs-one-RegisteredForm').hide();
                                                                });
                                                            </script>");
                        }
                    }





                }
            }

            catch (Exception ex)
            {
                //HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");

                //throw;

                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
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
            catch (Exception)
            {
                return "";
            }
        }




        public void GetAuthorityReason()
        {
            try
            {
                DataSet dsRAuthority = new DataSet();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsRAuthority = ReportSeek_BAL.Get_Authority_CoS();
                if (dsRAuthority != null)
                {
                    if (dsRAuthority.Tables.Count > 0)
                    {
                        if (dsRAuthority.Tables[0].Rows.Count > 0)
                        {
                            ddlAuthority.DataSource = dsRAuthority.Tables[0].DefaultView;
                            ddlAuthority.DataTextField = "AUTHORITYNAME_HI";
                            ddlAuthority.DataValueField = "Authority_Id";
                            ddlAuthority.DataBind();
                            ddlAuthority.Items.Insert(0, new ListItem("--Select Reason--", "0"));



                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }
        public void GetReportReason()
        {
            try
            {
                DataSet dsRReason = new DataSet();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsRReason = ReportSeek_BAL.Get_ReportReason_CoS();
                if (dsRReason != null)
                {
                    if (dsRReason.Tables.Count > 0)
                    {
                        if (dsRReason.Tables[0].Rows.Count > 0)
                        {


                            ddlOrgReason.DataSource = dsRReason.Tables[0].DefaultView;
                            //ddlOrgReason.DataTextField = "ReasonForReport_En";
                            ddlOrgReason.DataTextField = "REASONFORREPORT_HI";
                            ddlOrgReason.DataValueField = "Report_id";
                            ddlOrgReason.DataBind();
                            ddlOrgReason.Items.Insert(0, new ListItem("--Select Reason--", "0"));


                            ddlSRReason.DataSource = dsRReason.Tables[0].DefaultView;
                            //ddlSRReason.DataTextField = "ReasonForReport_En";
                            ddlSRReason.DataTextField = "REASONFORREPORT_HI";
                            ddlSRReason.DataValueField = "Report_id";
                            ddlSRReason.DataBind();
                            ddlSRReason.Items.Insert(0, new ListItem("--Select Reason--", "0"));

                            ddlOrgReason_other.DataSource = dsRReason.Tables[0].DefaultView;
                            //ddlOrgReason_other.DataTextField = "ReasonForReport_En";
                            ddlOrgReason_other.DataTextField = "REASONFORREPORT_HI";
                            ddlOrgReason_other.DataValueField = "Report_id";
                            ddlOrgReason_other.DataBind();
                            ddlOrgReason_other.Items.Insert(0, new ListItem("--Select Reason--", "0"));
                            //ddlReason.Items.Add(new ListItem("Other", "-1"));

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }

        protected void ddlReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrgReason.SelectedItem.Text == "अन्य")
            {
                //txtReason.Visible = true;
                pnlReason.Visible = true;
            }
            else
            {
                pnlReason.Visible = false;
            }
        }

        public void GetOriginalSRDetail(int appid)
        {
            try
            {
                DataTable dsCurSRDetail = new DataTable();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsCurSRDetail = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(appid);
                if (dsCurSRDetail != null)
                {
                    if (dsCurSRDetail.Rows.Count > 0)
                    {
                        txtOrgSROID.Text = dsCurSRDetail.Rows[0]["sro_id"].ToString();
                        txtOrgSREmail.Text = dsCurSRDetail.Rows[0]["email"].ToString();
                        txtOrgSRMobile.Text = dsCurSRDetail.Rows[0]["officeContactNumber"].ToString();
                        txtOrgSRDesignation.Text = dsCurSRDetail.Rows[0]["DESIGNATIN_HI"].ToString();
                        lblToDesig.Text = dsCurSRDetail.Rows[0]["DESIGNATIN_HI"].ToString();
                        txtOrgSROName.Text = dsCurSRDetail.Rows[0]["OFFICE_NAME_HI"].ToString();
                        lblToSRO.Text = dsCurSRDetail.Rows[0]["OFFICE_NAME_HI"].ToString();
                        Session["SubRegistrarOffice"] = lblToSRO.Text;
                        txtOrgSROfficeAdd.Text = dsCurSRDetail.Rows[0]["OFFICE_ADDRESS_HI"].ToString();
                        lblToAdd.Text = dsCurSRDetail.Rows[0]["OFFICE_ADDRESS_HI"].ToString();
                        grdPartyDisplay.DataSource = dsCurSRDetail;
                        grdPartyDisplay.DataBind();


                        //lblToDesig_otherSR.Text = dsCurSRDetail.Rows[0]["designation"].ToString();
                        //lblToSRO_otherSR.Text = dsCurSRDetail.Rows[0]["office_name_en"].ToString();
                        //lblToAdd_otherSR.Text = dsCurSRDetail.Rows[0]["office_address"].ToString();



                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }


        public void GetOtherSRDetail(int appid)
        {
            try
            {
                DataTable dsCurSRDetail = new DataTable();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsCurSRDetail = ReportSeek_BAL.Get_Other_ReportSeeking_CoS(appid);
                if (dsCurSRDetail != null)
                {
                    if (dsCurSRDetail.Rows.Count > 0)
                    {

                        grdOtherSrDetails.DataSource = dsCurSRDetail;
                        grdOtherSrDetails.DataBind();

                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }






        public void GetSRListDetail()
        {
            try
            {
                int droid = Convert.ToInt32(Session["DistrictID"].ToString());
                DataTable dsCurSRDetail = new DataTable();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(1, 0, droid);
                //dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS_ByDRO(droid);
                if (dsCurSRDetail != null)
                {
                    if (dsCurSRDetail.Rows.Count > 0)
                    {


                        //ddlSRName.DataSource = dsCurSRDetail;

                        //ddlSRName.DataTextField = "NAME_EN";
                        //ddlSRName.DataValueField = "ID";
                        //ddlSRName.DataBind();


                        ddlSRName.DataSource = dsCurSRDetail;
                        ddlSRName.DataTextField = "NAME_HI";
                        ddlSRName.DataValueField = "ID";
                        ddlSRName.DataBind();
                        ddlSRName.Items.Insert(0, new ListItem("--Select Current SR--", "0"));



                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }

        protected void btnDSCCurSR_Click(object sender, EventArgs e)
        {
            pnlEsignDSC.Visible = false;
            pnlSendCurSR.Visible = true;

        }



        protected void btnCreateReport_Click(object sender, EventArgs e)
        {
            string reasonforreport = ddlOrgReason.SelectedItem.Text;



            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + reasonforreport + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";


            Session["SelectedReason_Org"] = ddlOrgReason.SelectedItem.Text;


            if (ddlOrgReason.SelectedIndex == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> NoPartySelect();</script>");
            }
            else
            {
                pnlReport.Visible = true;
                //pnlReport.v

                if (ddlOrgReason.SelectedItem.Text == "अन्य")
                {
                    //lblReasonSub.Text = txtOrgOtherReason.Text;
                    string lblReasonSub = txtOrgOtherReason.Text;

                    //string SeekReportSubject_Org = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + lblReasonSub.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                    string SeekReportSubject_Org = lblReasonSub + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";
                    summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + lblReasonSub + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";




                    Session["SeekReportSubject_Org"] = SeekReportSubject_Org.ToString();

                }
                else
                {
                    //lblReasonSub.Text = ddlOrgReason.SelectedItem.Text;

                    //string SeekReportSubject_Org = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + ddlOrgReason.SelectedItem.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                    string SeekReportSubject_Org = ddlOrgReason.SelectedItem.Text + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";

                    summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + reasonforreport + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";
                    lblReasonSub_OrgSR.Text = SeekReportSubject_Org;
                    Session["SeekReportSubject_Org"] = SeekReportSubject_Org.ToString();



                }


                pContent.InnerHtml = summernote3.InnerHtml;
                //StringWriter iSW1 = new StringWriter();
                //HtmlTextWriter iHTW1 = new HtmlTextWriter(iSW1);
                //DataTable dt = new DataTable();
                ////Control con1 = divContentTosave.FindControl("pContent");
                //pSubject.RenderControl(iHTW1);
                //docPath.Src = pSubject.TagName;
                string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + ".pdf";
                string ProposalSheetPath = Server.MapPath("~/Proposal/" + lblProposalNo.Text);
                ViewState["FirstFormate_Path"] = "~/Proposal/" + lblProposalNo.Text + "/" + FileNme;
                ViewState["SecondFormate_Path"] = "";

                string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, hdnvalue.Value);
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddReport();</script>");
                pnlBtnSaveReport.Visible = true;
                btnEdit_Report_OriginalSR.Visible = true;
            }


        }



        public string Check_Insert_WhatsAppOptINdd(string Whatsapp_Number, string Name, string CaseNo, string RegistrationNo, string noticepdf, string PartyID, string Notice_ID)
        {
            string ResStatus = "False";
            try
            {
                var client = new RestClient(Whatsapp_URL + "method=OPT_IN&format=json&userid=" + WhatsApp_Userid + "&password=" + WhatsApp_Pwd + "&phone_number=" + Whatsapp_Number + "&v=1.1&auth_scheme=plain&channel=WHATSAPP");
                //var client = new RestClient("https://media.smsgupshup.com/GatewayAPI/rest?method=OPT_IN&format=json&userid=2000203534&password=79wZJ7F4&phone_number=" + Whatsapp_Number + "&v=1.1&auth_scheme=plain&channel=WHATSAPP");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                IRestResponse response = client.Execute(request);
                var jsonString = response.Content;
                sendwhatsapp1(Whatsapp_Number, Name, CaseNo, RegistrationNo, noticepdf, PartyID, Notice_ID);
                var jObject = JObject.Parse(jsonString);
                var id = jObject["response"]["id"].ToString();
                var phone = jObject["response"]["phone"].ToString();
                var details = jObject["response"]["details"].ToString();
                var status = jObject["response"]["status"].ToString();
                //Insert_WhatsAppOptIN(id, MobileNo, status, Userid);

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
            return ResStatus;
        }
        public void sendwhatsapp1(string Whatsapp, string Name, string CaseNo, string RegistrtionNo, string noticepdf, string PartyID, string Notice_ID)
        {
            try
            {
                String responseString = string.Empty;

                string str = string.Empty;
                string RegistrationNo = string.Empty;
                DataSet DSPartyDisplay = new DataSet();
                using (WebClient webClient = new WebClient())
                {
                    string cntctnumb = Whatsapp; //9584471013

                    if (RegistrtionNo == "")
                    {
                        RegistrationNo = "NA";
                    }
                    string authority = HttpContext.Current.Request.Url.Authority;
                    //string authority = baseUrl;
                    noticepdf = noticepdf.Replace("~", "");
                    //noticepdf = "/CMS" + noticepdf;


                    //Session["NoticePDF"] = noticepdf;
                    //string handlerUrl = "WhatsappNoticeHandler.ashx";
                    string noticepdfsave = noticepdf;

                    string Link = "http://" + authority + noticepdfsave;

                    //Session["Link"] = Link;
                    string msgurl = authority + noticepdf;

                    Session["HendlerURL"] = Link;

                    //string partyurl = "http://" + authority + "/CMSCITIZEN/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Notice_ID + "&Party_Id=" + PartyID;
                    string partyurl = citizenBaseUrl + "PartyNotice.aspx?AppID=" + Session["AppID"].ToString() + "&Notice_Id=" + Notice_ID + "&Party_Id=" + PartyID;
                    Uri uri = new Uri(partyurl);


                    Session["partyurl"] = uri;

                    string msg = "प्रिय " + Name + ", केस क्रमांक " + CaseNo + " एवं प्रस्ताव क्रमांक " + RegistrationNo + " के लिए " + CaseNo + " से संबंधित पत्र देखने के लिए, कृपया दिए गए लिंक पर क्लिक करें:" + partyurl + " |";


                    //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrtionNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + uri + " |";

                    //प्रिय Manoj, आपकी संपत्ति रजिस्ट्री क्रमांक 12345 के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर 123 है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें { { 4} } |

                    string RAM_doblebackslace = "://";
                    string RAM_backslace = "/";
                    string RAM_Encodedoble = HttpUtility.UrlEncode(RAM_doblebackslace).ToString().ToUpper();
                    string RAM_Encodesingle = HttpUtility.UrlEncode(RAM_backslace).ToString().ToUpper();
                    noticepdf = noticepdf.Replace("/", RAM_Encodesingle);
                    //string RAM_MediaUrl = "http" + RAM_Encodedoble + authority + noticepdf;
                    string RAM_MediaUrl = Link;
                    //string RAM_MediaUrl = "https%3A%2F%2Fsugam.mp.gov.in%2FUploadedDocument%2FDoc%2F22092023030925khuss.pdf";
                    string message = HttpUtility.UrlEncode(msg).ToString().ToUpper();
                    string url = @"https://media.smsgupshup.com/GatewayAPI/rest?userid=2000215884&password=tbMsSWEk&send_to=" + cntctnumb + "&v=1.1&format=json&msg_type=DOCUMENT&method=SENDMEDIAMESSAGE&caption=" + message + "&media_url=" + RAM_MediaUrl + "";
                    //string url = @"https://media.smsgupshup.com/GatewayAPI/rest?userid=2000215884&password=tbMsSWEk&send_to=" + cntctnumb + "&v=1.1&format=json&msg_type=DOCUMENT&method=SENDMEDIAMESSAGE&caption=" + message + "&media_url=" + RAM_MediaUrl + "";

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.Method = "POST";
                    request.ContentType = "application/x-www-form-urlencoded";
                    request.Headers.Add("Cache-Control", "no-cache");
                    request.Credentials = CredentialCache.DefaultCredentials;

                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // Get the stream associated with the response.
                    Stream receiveStream = response.GetResponseStream();
                    // Pipes the stream to a higher level stream reader with the required encoding format. 
                    StreamReader readStream = new StreamReader(receiveStream, Encoding.UTF8);
                    responseString = readStream.ReadToEnd();
                    response.Close();
                    readStream.Close();
                    String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                    //clsNoticeBAL.WhatsappResponse_Insert(RegistrationNo, CaseNo, "whatsapp", msg, responseString, PageUrl, cntctnumb, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, PartyID, noticepdfsave, Notice_ID);
                    //Console.WriteLine("Message Send Successfully");

                    //Session["WhatsappTest"] = RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString;
                    //Response.Write(RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString);
                }
            }

            catch (Exception ex)
            {

            }


        }


        protected void btnSaveReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop();</script>", false);
            SaveSeekReportPDF();

            StringWriter iSW1 = new StringWriter();
            HtmlTextWriter iHTW1 = new HtmlTextWriter(iSW1);
            DataTable dt = new DataTable();
            //Control con1 = divContentTosave.FindControl("pContent");
            lblReasonSub_OrgSR.RenderControl(iHTW1);
            string otherreason = txtOrgOtherReason.Text;


            //string content = summernotesubject.Value;
            string Subject_Org = Session["SeekReportSubject_Org"].ToString();


            string Subject = hdnvalue.Value;
            //ViewState["NoticeCoptent"] = iHTW1.InnerWriter.ToString();
            dt = ReportSeek_BAL.InsertSeek_report(Session["Appno"].ToString(), Convert.ToInt32(ViewState["AppID"].ToString()),
                Subject_Org, summernote3.Value, ddlOrgReason.SelectedItem.Text, "", "",
                Context.Request.UserHostName, Context.Request.UserHostAddress, Session["RecentSheetPath"].ToString(), txtOrgOtherReason.Text, "", 0, txtOrgSROID.Text, "Original SR", 1, 13, ddlOrgReason.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                Session["seekreoprt_id_OrgSR"] = dt.Rows[0]["REPORT_ID"].ToString();
            }

            Session["SeekReportSubject"] = iHTW1.InnerWriter.ToString();

            ddlOrgReason.Enabled = false;
            //pContent.InnerHtml = ViewState["SeekReportContent"].ToString();
            pContent.InnerHtml = summernote3.Value;
            pnlBtnSaveReport.Visible = false;
            pnlEsignDSC.Visible = true;
            btnCreateReport.Visible = false;
            btnEdit_Report_OriginalSR.Visible = false;
            txtOrgOtherReason.ReadOnly = true;
            DataSet dsRecent = new DataSet();

            //dsRecent = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), ViewState["AppID"].ToString());
            //if (dsRecent != null)
            //{
            //    if (dsRecent.Tables.Count > 0)
            //    {

            //        if (dsRecent.Tables[0].Rows.Count > 0)
            //        {
            //            string fileName = dsRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
            //            Session["RecentSheetPath"] = fileName.ToString();
            //            docPath.Src = fileName;
            //        }
            //    }
            //}


        }

        private void SaveSeekReport(string Path)
        {
            try
            {

                string FileName = "COS_SeekReport_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                //ViewState["FileNameUnSignedPDF"] = FileName;
                //string OrderSheetPath = Server.MapPath("~/OrderSheet/" + lblApplication_No.Text);
                ViewState["ActualPath"] = Path;


                DataTable dt = new DataTable();

                dt = ReportSeek_BAL.InsertSeek_report(Session["Appno"].ToString(), Convert.ToInt32(ViewState["AppID"].ToString()),
               summernotesubject.Value, summernote3.Value, ddlOrgReason.SelectedItem.Text, "", "",
               Context.Request.UserHostName, Context.Request.UserHostAddress, ViewState["UnSignedPDF"].ToString(), txtOrgOtherReason.Text, "", 0, txtOrgSROID.Text, "Original SR", 1, 13, ddlOrgReason.SelectedValue);



            }
            catch (Exception ex)
            {

            }

        }



        private void SaveSeekReportPDF()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                //string divSub = summernotesubject.Value;
                string divCon = summernote3.Value;
                //DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];
                //summernotesubject.Value = "In reference to the Proposal ID- " + lblProposalSub.Text + ".regarding  " + lblReasonSub.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                //string divSub = summernotesubject.Value;

                string divSub = Session["SeekReportSubject_Org"].ToString();



                StringBuilder stringBuilder = new StringBuilder();
                // stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; padding: 30px 30px 30px 30px;'>");

                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; text-align: center '>कार्यालय जिला पंजीयक एवं न्यायालय कलेक्टर ऑफ स्टाम्प जिला " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '> " + hdnOfficeNameHi.Value + " <br> ई - मेल - igrs@igrs.gov.in</h3> ");
                //stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>एस.बी.टी. परिसर, मेजनाईन फ्लोर, गुना <br> ई - मेल - igrs@igrs.gov.in</h3> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र <br> प्रकरण क्रमांक -" + lblCaseNo.Text + " धारा - 33 </b></h2> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");


                stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblRecord.Text + " <br><br><b>आवेदक (प्रथम पक्षकार)</b><br><br><br>" + lblDepartment.Text + "<br><br> <b>अनावेदक (द्वितीय पक्षकार) </b></h3>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>प्रति</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToDesig.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToSRO.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToAdd.Text + "</h2>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("<br>");



                stringBuilder.Append("<div style='display: inline-block;'>");
                stringBuilder.Append("<p style='display: inline-block; margin: 0;'>");
                stringBuilder.Append("<b>विषय :  </b>");
                stringBuilder.Append("</p>");
                stringBuilder.Append("<div style='display: inline-block;'>");
                stringBuilder.Append(divSub); // Assuming divSub is the value part
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");

                //stringBuilder.Append("<div style='display: inline-block;float: left '>");
                //stringBuilder.Append("<p style = 'float: left;'>");
                //stringBuilder.Append("<b> Subject:  </b>");
                //stringBuilder.Append("</p>");
                //stringBuilder.Append(divSub);
                //stringBuilder.Append("</div>");


                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append(divCon);
                stringBuilder.Append("</div>");


                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");


                stringBuilder.Append("</div>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 220px;left:-50px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");
                stringBuilder.Append("<br/>");

                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 240px;left:150px;'>स्थान- " + hdnOfficeNameHi.Value + " <br/> जारी दिनांक: " + lblTodate.Text + " <br/> <br/></b> ");


                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                ViewState["FileNameUnSignedPDF"] = "";
                string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_SeekReport_Original_SR.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/COS_SeekReport/", stringBuilder.ToString());
                Session["RecentSheetPath"] = "~/COS_SeekReport/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                //SaveSeekReport("~/COS_SeekReport/" + FileNme);


                //setRecentSheetPath();


            }
            catch (Exception ex)
            {

            }

        }

        private void SaveSeekReportPDF_Current()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                //string divSub = summernotesubject.Value;
                string divCon = summernote3.Value;
                //string divCon1 = pSRContent.InnerText;
                //DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];

                //summernotesubject.Value = "In reference to the Proposal ID- " + lblProposalSub.Text + ".regarding  " + lblSRSUB.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                //string divSub = summernotesubject.Value;




                string divSub = Session["SeekReportSubject_Current"].ToString();


                StringBuilder stringBuilder = new StringBuilder();
                // stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; padding: 30px 30px 30px 30px;'>");

                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; text-align: center '>कार्यालय जिला पंजीयक एवं न्यायालय कलेक्टर ऑफ स्टाम्प जिला " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '> " + hdnOfficeNameHi.Value + " <br> ई - मेल - igrs@igrs.gov.in</h3> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र <br> प्रकरण क्रमांक -" + lblCaseNo.Text + " धारा - 33 </b></h2> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");


                stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblRecord.Text + " <br><br><b>आवेदक (प्रथम पक्षकार)</b><br><br><br>" + lblDepartment.Text + "<br><br> <b>अनावेदक (द्वितीय पक्षकार) </b></h3>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>प्रति</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblSRDesign.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToSROOffice.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblSRAddress.Text + "</h2>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("<br>");


                stringBuilder.Append("<div style='display: inline-block;'>");
                stringBuilder.Append("<p style='display: inline-block; margin: 0;'>");
                stringBuilder.Append("<b>विषय :  </b>");
                stringBuilder.Append("</p>");
                stringBuilder.Append("<div style='display: inline-block;'>");
                stringBuilder.Append(divSub); // Assuming divSub is the value part
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");



                //stringBuilder.Append("<div style='display: inline-block;float: left '>");
                //stringBuilder.Append("<p style = 'float: left;'>");
                //stringBuilder.Append("<b> Subject:  </b>");
                //stringBuilder.Append("</p>");
                //stringBuilder.Append(divSub);
                //stringBuilder.Append("</div>");


                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append(divCon);
                stringBuilder.Append("</div>");


                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");


                stringBuilder.Append("</div>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 220px;left:-50px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");
                stringBuilder.Append("<br/>");

                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 240px;left:150px;'>स्थान- " + hdnOfficeNameHi.Value + " <br/> जारी दिनांक: " + lblTodate.Text + " <br/> <br/></b> ");


                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                ViewState["FileNameUnSignedPDF"] = "";
                string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_SeekReport_Current_SR.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF_Current"] = ConvertHTMToPDF(FileNme, "~/COS_SeekReport/", stringBuilder.ToString());
                //Session["RecentSheetPath_Current"] = "~/COS_SeekReport/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;
                Session["RecentSheetPath_Current"] = ViewState["UnSignedPDF_Current"].ToString();

                Session["RecentSheetPath_Current"] = "~/COS_SeekReport/" + FileNme;
                //SaveSeekReport_CurrentSR("~/COS_SeekReport/" + FileNme);




            }
            catch (Exception ex)
            {

            }

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
        //public void CraetSourceFile(int APP_ID)
        //{
        //    try
        //    {
        //        DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
        //        if (dt.Rows.Count > 0)
        //        {

        //            string[] addedfilename = new string[3];

        //            addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
        //            addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
        //            addedfilename[2] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
        //            //addedfilename[3] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());


        //            string sourceFile = ViewState["CoS_OrderSheetAllSheetDoc"].ToString();

        //            MargeMultiplePDF(addedfilename, sourceFile);
        //            setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




        //        }

        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}

        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[2];

                    addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    //addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());


                    string sourceFile = ViewState["CoS_OrderSheetAllSheetDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
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
        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_OrderSheetAllSheetDoc/" + All_OrderSheetFileNme;
                DataSet dsIndexDetails = objClsNewApplication_static.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppID"].ToString()), ViewState["Appno"].ToString());

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
        public void AllDocList(int APP_ID)
        {
            try
            {
                //DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);
                DataSet dsIndexDetails = objClsNewApplication_static.GetDocDetails_CoS_Index_API(APP_ID, ViewState["Appno"].ToString());
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
        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {

            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();</script>", false);
            if (ddl_SignOption.SelectedValue == "1")
            {
                if (TxtLast4Digit.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4Digit.Focus();
                    return;
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


            //-------eSign Start------------------------

            //string Location = "Project Office -" + HF_Office.Value;
            string Location = "Bhopal";
            //string PdfName = "";
            //string ApplicationNo = hdnProposal.Value;

            //if (ViewState["FileNameUnSignedPDF"] == null)
            //{
            //    string PdfName = Session["UNSIGNED_PDF_OrgSR"].ToString();
            //}
            //else
            //{
            //    string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            //}

            string PdfName = "";
            if (ViewState["FileNameUnSignedPDF"] == null)
            {
                PdfName = Session["UNSIGNED_PDF_OrgSR"].ToString();
            }
            else
            {
                PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            }




            PdfName = PdfName.Replace("~/COS_SeekReport/", "");
            ViewState["filename"] = PdfName;
            //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
            string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_SeekReport/" + PdfName.ToString());
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


                        ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_SeekReport.aspx");




                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "" , false);
                        //getdata();

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
            pnlEsignDSC.Visible = false;
            btnCreateReport.Visible = false;
            pnlSendCurSR.Visible = true;
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

        protected void btnSendOther_Click(object sender, EventArgs e)
        {

        }

        protected void btnSendOriginalSR_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> Loaderstop();AddNotice();</script>");
            //pnlSaveDraft.Visible = false;

            string App_id = Session["AppID"].ToString();

            //DataSet dsDocRecent;
            string Name;
            string whatsapp;
            string MobileNo_SMS;
            string CaseNo;
            string RegistrationNo;
            string Email;
            string noticepdf;
            string PartyID;
            if (chechwhats.Checked)
            {
                string Appid = Session["AppID"].ToString();
                DataTable ds = new DataTable();

                ds = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(Convert.ToInt32(Appid));

                if (ds.Rows.Count > 0)
                {

                    whatsapp = ds.Rows[0]["officeContactNumber"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    CaseNo = ds.Rows[0]["application_no"].ToString();
                    RegistrationNo = ds.Rows[0]["application_no"].ToString();
                    //noticepdf = ViewState["SIGNED_PDF_PATH"].ToString(); 
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString();
                    PartyID = ds.Rows[0]["sro_id"].ToString();
                    if (RegistrationNo == "")
                    {
                        RegistrationNo = "NA";
                    }
                    if (whatsapp != "")
                    {
                        Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, "");
                    }

                }
            }

            if (checksms.Checked)
            {
                //dsDocRecent.Clear();
                DataTable ds = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(Convert.ToInt32(App_id));

                if (ds.Rows.Count > 0)
                {

                    MobileNo_SMS = ds.Rows[0]["officeContactNumber"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    CaseNo = ds.Rows[0]["application_no"].ToString();  //ds.Tables[0].Rows[i]["case_no"].ToString();
                    RegistrationNo = ds.Rows[0]["application_no"].ToString(); //dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString(); //dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                    //PartyID = ds.Rows[0]["sro_id"].ToString(); 
                    PartyID = ds.Rows[0]["id"].ToString();

                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    noticepdf = "/SampadaCMS" + noticepdf;
                    string noticepdfsave = noticepdf;
                    //string Link = "http://" + authority + noticepdf;
                    string msgurl = authority + noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;

                    if (MobileNo_SMS != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                        string msg = "प्रिय " + Name + ", केस क्रमांक " + CaseNo + " एवं प्रस्ताव क्रमांक " + RegistrationNo + " के लिए " + CaseNo + " से संबंधित पत्र देखने के लिए, कृपया दिए गए लिंक पर क्लिक करें:" + partyurl + " |";

                        //string msg = "Dear "+Name+",a case has been registered against your property ID 1234567 having previous case number "+CaseNo+ "and RRC case no" + CaseNo + ". To view the Auction Order click on below link www.google.com ";

                        string response = SMSUtility.Send(msg, MobileNo_SMS, "1407168415452536769");

                        string SmsUser = ConfigurationManager.AppSettings["SmsUser"];
                        string SmsPassword = ConfigurationManager.AppSettings["SmsPassword"];
                        string SmsSenderId = ConfigurationManager.AppSettings["SmsSenderId"];
                        string secureKey = ConfigurationManager.AppSettings["SmsSecureKey"];
                        string templateid = ConfigurationManager.AppSettings["templateid"];

                        //string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, secureKey, templateid);
                        //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        COSNotice_Bal.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));

                    }

                }

            }

            if (chkEmail.Checked)
            {
                DataTable ds = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(Convert.ToInt32(App_id));
                if (ds.Rows.Count > 0)
                {
                    //CaseNo = dsDocRecent.Tables[0].Rows[i]["case_number"].ToString();
                    //RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                    //Email = dsDocRecent.Tables[0].Rows[i]["emailID"].ToString();
                    //noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                    //PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();

                    Email = ds.Rows[0]["email"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    CaseNo = ds.Rows[0]["application_no"].ToString();  //ds.Tables[0].Rows[i]["case_no"].ToString();
                    RegistrationNo = ds.Rows[0]["application_no"].ToString(); //dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                    //noticepdf = ViewState["SIGNED_PDF_PATH"].ToString(); //dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString();
                    PartyID = ds.Rows[0]["sro_id"].ToString(); //dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    //noticepdf = "/SampadaCMS" + noticepdf;
                    string noticepdfsave = noticepdf;
                    //string Link = "http://" + authority + noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;

                    string msgurl = authority + noticepdf;
                    if (Email != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        EmailUtility emailUtility = new EmailUtility();
                        string userid = HttpContext.Current.Profile.UserName;
                        string IP = HttpContext.Current.Request.UserHostAddress;
                        emailUtility.SendEmail(RegistrationNo, CaseNo, Email, msg, PageUrl, userid, IP, noticepdf);


                    }
                }


            }

            DataTable dt2 = ReportSeek_BAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id), 16);
            hdnfldCaseNo.Value = ViewState["Case_Number"].ToString();
            hdnfAppld.Value = Session["AppID"].ToString();
            hdnfAppNo.Value = Session["Appno"].ToString();

            //Response.Redirect("Ordersheet.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());


            ScriptManager.RegisterStartupScript(pnlSendCurSR, pnlSendCurSR.GetType(), "none", "<script> ShowMessageYesNo();</script>", false);
            //custom-tabs-one-profile-tab.Attributes["class"] = "nav-link disabled";
            custom_tabs_on_profile_tab.Attributes["class"] = "nav-link disabled";


        }

        protected void ddlSRName_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable dsCurSRDetail = new DataTable();
            //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
            if (ddlSRName.SelectedIndex > 0)
            {
                dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(ddlSRName.SelectedValue), 0);
                if (dsCurSRDetail != null)
                {
                    if (dsCurSRDetail.Rows.Count > 0)
                    {
                        txtSROID.Text = dsCurSRDetail.Rows[0]["ID"].ToString();
                        //txtSRDesignation.Text = dsCurSRDetail.Rows[0]["DESIGNATION_HI"].ToString();
                        txtSRDesignation.Text = "उप रजिस्ट्रार (एस आर)";
                        txtSREmail.Text = dsCurSRDetail.Rows[0]["EMAIL"].ToString();
                        txtSRMobile.Text = dsCurSRDetail.Rows[0]["MOBILE_NO"].ToString();
                        txtSROName.Text = dsCurSRDetail.Rows[0]["office_name_hi"].ToString();
                        txtSROOfficeAdd.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();
                        //lblSRDesign.Text = dsCurSRDetail.Rows[0]["DESIGNATION_HI"].ToString();
                        lblSRDesign.Text = "उप रजिस्ट्रार (एस आर)";
                        lblToSROOffice.Text = dsCurSRDetail.Rows[0]["office_name_hi"].ToString();
                        lblSRAddress.Text = dsCurSRDetail.Rows[0]["office_Location"].ToString();

                        //grdPartyDisplay_CurrentSR.DataSource = dsCurSRDetail;
                        //grdPartyDisplay_CurrentSR.DataBind();

                    }
                }
            }
        }

        public void GetCurrentSRDetail()
        {
            try
            {
                DataTable dsCurSRDetail = new DataTable();
                //dsHead = objClsNewApplication.GetHead(Convert.ToInt32(ddlHead.SelectedValue));
                dsCurSRDetail = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(ddlSRName.SelectedValue), 0);

                if (dsCurSRDetail != null)
                {
                    if (dsCurSRDetail.Rows.Count > 0)
                    {
                        //string sro = dsCurSRDetail.Rows[0]["user_id"].ToString();
                        grdPartyDisplay_CurrentSR.DataSource = dsCurSRDetail;
                        grdPartyDisplay_CurrentSR.DataBind();




                    }
                }
            }
            catch (Exception ex)
            {
                string message = " swal('','" + ex.Message.ToString() + "', 'info')";
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                return;
            }
        }


        protected void btnCreateReportCurrent_Click(object sender, EventArgs e)
        {
            DataSet dsDocDetails = new DataSet();

            dsDocDetails = ReportSeek_BAL.GetHearingDate(Convert.ToInt32(Session["AppID"].ToString()));
            if (dsDocDetails != null)
            {
                if (dsDocDetails.Tables.Count > 0)
                {

                    if (dsDocDetails.Tables[0].Rows.Count > 0)
                    {
                        string HearingDate = dsDocDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                        Session["HearingDate"] = HearingDate.ToString();
                    }
                }
            }
            string reasonforreport = ddlSRReason.SelectedItem.Text;
            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + reasonforreport + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";



            if (ddlSRName.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(upnl1, upnl1.GetType(), "none", "<script> SelectCurrentSR();</script>", false);
            }


            else if (ddlSRReason.SelectedIndex == 0)
            {

                ScriptManager.RegisterStartupScript(upnl1, upnl1.GetType(), "none", "<script> SelectReasonCurrentSR();</script>", false);

            }
            else
            {
                pnlSRReport.Visible = true;
                //lblSRSUB.Text = ddlSRReason.SelectedItem.Text;
                pSRContent.InnerHtml = summernote3.Value;
                btnEdit_Report_CurrentSR.Visible = true;
            }

            Session["ddlSRName_Current"] = ddlSRName.SelectedValue;



            if (ddlSRReason.SelectedItem.Text == "अन्य")
            {
                string HearingDt = Session["HearingDate"].ToString();
                string CaseNo = ViewState["Case_Number"].ToString();

                string lblSRSUB = txtCrntOtherReason.Text;
                //lblSRSUB.Text = txtCrntOtherReason.Text;
                //string SeekReportSubject_Current = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + lblSRSUB.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                string SeekReportSubject_Current = lblSRSUB + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";

                summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + lblSRSUB + ". मामले की सुनवाई की तारीख  " + HearingDt + " है, इसलिए मामले संख्या " + CaseNo + " के लिए इससे पहले दस्तावेज पेश करें। ";

                Session["SeekReportSubject_Current"] = SeekReportSubject_Current.ToString();
            }
            else
            {

                //string SeekReportSubject_Current = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + ddlSRReason.SelectedItem.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                string SeekReportSubject_Current = ddlSRReason.SelectedItem.Text + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";

                summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + reasonforreport + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";

                lblReasonSub_CurrntSR.Text = SeekReportSubject_Current;

                Session["SeekReportSubject_Current"] = SeekReportSubject_Current.ToString();
            }
            pSRContent.InnerHtml = summernote3.Value;

            string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + ".pdf";
            string ProposalSheetPath = Server.MapPath("~/Proposal/" + lblProposalNo.Text);
            ViewState["FirstFormate_Path"] = "~/Proposal/" + lblProposalNo.Text + "/" + FileNme;
            ViewState["SecondFormate_Path"] = "";

            string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, hdnvalue.Value);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddReport_CurrentSR();</script>");
            pnlBtnSaveReport_crnt.Visible = true;

            btnEdit_Report_CurrentSR.Visible = true;


        }

        protected void btnSRSaveReport_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "Script", "Loaderstop();", true);
            //SaveSeekReportPDF();
            SaveSeekReportPDF_Current();
            //string current_SR_content = pSRContent.InnerText;
            string current_SR_content = summernote3.Value;

            //string currentSR_content = Regex.Replace(current_SR_content, "<.*?>", string.Empty);
            //string formattedContent = currentSR_content.Replace("\n", "<br>");

            StringWriter iSW1 = new StringWriter();
            HtmlTextWriter iHTW1 = new HtmlTextWriter(iSW1);
            DataTable dt = new DataTable();
            //Control con1 = divContentTosave.FindControl("pContent");
            lblReasonSub_OrgSR.RenderControl(iHTW1);
            string otherreason = txtOrgOtherReason.Text;
            //string content = summernotesubject.Value;
            string Subject_Current = Session["SeekReportSubject_Current"].ToString();
            string Subject = hdnvalue.Value;
            //ViewState["NoticeCoptent"] = iHTW1.InnerWriter.ToString();
            dt = ReportSeek_BAL.InsertSeek_report(Session["Appno"].ToString(), Convert.ToInt32(ViewState["AppID"].ToString()),
                Subject_Current, summernote3.Value, ddlOrgReason.SelectedItem.Text, "", "",
                Context.Request.UserHostName, Context.Request.UserHostAddress, Session["RecentSheetPath_Current"].ToString(), txtOrgOtherReason.Text, "", 0, txtSROID.Text, "Current SR", 2, 17, ddlSRReason.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                Session["seekreoprt_id_CrntSR"] = dt.Rows[0]["REPORT_ID"].ToString();
            }

            //Session["SeekReportSubject"] = iHTW1.InnerWriter.ToString();Session["SeekReportSubject_Current"]
            //Session["SeekReportSubject"] = iHTW1.InnerWriter.ToString();


            //pContent.InnerHtml = ViewState["SeekReportContent"].ToString();
            pSRContent.InnerHtml = summernote3.Value;
            //pnlBtnSaveReport.Visible = false;
            //pnlEsignDSC.Visible = true;
            //btnCreateReport.Visible = false;
            pnlCurSRESign.Visible = true;
            btnSRSaveReport.Visible = false;
            btnCreateReportCurrent.Visible = false;
            btnEdit_Report_CurrentSR.Visible = false;
            txtOrgOtherReason.ReadOnly = true;

            ddlSRName.Enabled = false;
            ddlSRReason.Enabled = false;

            DataSet dsRecent = new DataSet();
            dsRecent = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), ViewState["AppID"].ToString());
            if (dsRecent != null)
            {
                if (dsRecent.Tables.Count > 0)
                {

                    if (dsRecent.Tables[0].Rows.Count > 0)
                    {
                        string fileName = dsRecent.Tables[0].Rows[0]["PROPOSALPATH_SECONDFORMATE"].ToString();
                        Session["RecentSheetPath"] = fileName.ToString();
                        docPath.Src = fileName;
                    }
                }
            }


        }

        protected void btnSREsignDSC_Click(object sender, EventArgs e)
        {
            GetCurrentSRDetail();



            if (ddl_SignOptionCurSR.SelectedValue == "1")
            {
                if (TxtLast4DigitCurSR.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4DigitCurSR.Focus();
                    return;
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


            //-------eSign Start------------------------

            //string Location = "Project Office -" + HF_Office.Value;
            string Location = "Bhopal";

            //string ApplicationNo = hdnProposal.Value;

            //string PdfName = ViewState["FileNameUnSignedPDF"].ToString();

            string PdfName = "";
            if (ViewState["FileNameUnSignedPDF"] == null)
            {
                PdfName = Session["UNSIGNED_PDF_CrntSR"].ToString();
            }
            else
            {
                PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            }




            PdfName = PdfName.Replace("~/COS_SeekReport/", "");
            ViewState["filename"] = PdfName;
            //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
            string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_SeekReport/" + PdfName.ToString());
            string flSourceFile = FileNamefmFolder;

            if (File.Exists(FileNamefmFolder))
            {
                if (ddl_SignOptionCurSR.SelectedValue == "1")
                {
                    if (TxtLast4DigitCurSR.Text.Length != 4)
                    {
                        TxtLast4DigitCurSR.Focus();
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                        return;
                    }
                    string Last4DigitAadhaar = TxtLast4DigitCurSR.Text;

                    if (File.Exists(FileNamefmFolder))
                    {
                        string ResponseURL = null;
                        string txtSignedBy = "Collector of Stamp";
                        string UIDToken = "";
                        string TransactionId = getTransactionID();
                        string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                        string TransactionOn = "Pre";



                        ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_CurSRSeekReport.aspx");

                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "" , false);
                        //getdata();

                        AuthMode authMode = AuthMode.OTP;

                        eSigner.eSigner _esigner = new eSigner.eSigner();

                        _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                        //getdata_Esign();

                        DataTable dt2 = clsFinalOrderBAL.Update_Status_COS(Convert.ToInt32(ViewState["AppID"].ToString()), 18);
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




            pnlSRSendParty.Visible = true;
            pnlCurSRESign.Visible = false;
        }
        protected void Attached_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loaderstop();</script>", false);
            Panel2.Visible = true;
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            int iFileSize = 0;
            int App_id = Convert.ToInt32(Session["AppID"].ToString());
            string DocType = txtUploadDocType.Text;
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
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageMaxSize();</script>");
                                return;
                            }
                            string fileName = Path.GetFileName(postedFile.FileName);
                            string docpath = DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;
                            string docpath1 = "~/CoSDocument_ReportSeek/" + DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;

                            postedFile.SaveAs(Server.MapPath(docpath1));

                            DataTable dt = ReportSeek_BAL.InsertDoc_ReportSeekByCos(App_id, ViewState["Case_Number"].ToString(), docpath1, GetLocalIPAddress(), DocType);
                            if (dt.Rows.Count > 0)
                            {
                                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Response Saved Successfully', '', 'success')", true);
                            }
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DoCUploadMsg();</script>");
                            Panel2.Visible = false;
                            AllDocList(Convert.ToInt32(ViewState["AppID"]));
                        }
                        Response.Redirect(Request.Url.AbsoluteUri);
                    }
                    else
                    {
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DocTypeErrorMsg();</script>");
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

        protected void ddlSRReason_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlSRReason.SelectedItem.Text == "अन्य")
            {
                //txtReason.Visible = true;
                PnlCurrentOtherRsn.Visible = true;
            }
            else
            {
                PnlCurrentOtherRsn.Visible = false;
            }




        }

        protected void BtnSend_CurrentSR_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "none", "<script>Loader();Loaderstop();</script>", false);

            //SaveSeekReportPDF_Current();
            //DataSet dsDocRecent;
            string Name;
            string whatsapp;
            string MobileNo_SMS;
            string CaseNo = ViewState["Case_Number"].ToString();
            string RegistrationNo = ViewState["Appno"].ToString();
            string Email;
            string noticepdf;
            string PartyID;
            if (chkWhatsApp_CurrentSR.Checked)
            {
                string Appid = Session["AppID"].ToString();
                DataTable ds = new DataTable();

                //ds = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(Convert.ToInt32(Appid));
                ds = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(ddlSRName.SelectedValue), 0);

                if (ds.Rows.Count > 0)
                {

                    whatsapp = ds.Rows[0]["Whatsapps"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    //CaseNo = ds.Rows[0]["application_no"].ToString();  
                    //RegistrationNo = ds.Rows[0]["application_no"].ToString(); 
                    //noticepdf = ViewState["UnSignedPDF_Current"].ToString();

                    noticepdf = Session["Current_SignPath"].ToString();
                    PartyID = ds.Rows[0]["user_id"].ToString();
                    if (RegistrationNo == "")
                    {
                        RegistrationNo = "NA";
                    }
                    if (whatsapp != "")
                    {
                        Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, "");
                    }

                }
            }
            if (chkSMS_CurrentSR.Checked)
            {
                //dsDocRecent.Clear();
                DataTable ds = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(ddlSRName.SelectedValue), 0);

                if (ds.Rows.Count > 0)
                {

                    MobileNo_SMS = ds.Rows[0]["MOBILE_NO"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    //CaseNo = ds.Rows[0]["application_no"].ToString();  
                    //RegistrationNo = ds.Rows[0]["application_no"].ToString(); 
                    //noticepdf = ViewState["UnSignedPDF_Current"].ToString();
                    noticepdf = Session["RecentSheetPath_Current"].ToString();
                    //Session["RecentSheetPath_Current"] = "~/COS_SeekReport/" + FileNme;
                    //noticepdf = Session["Current_SignPath"].ToString();
                    //noticepdf = Session["RecentSheetPath_Current"].ToString();
                    PartyID = ds.Rows[0]["user_id"].ToString();

                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    noticepdf = "/SampadaCMS" + noticepdf;
                    string noticepdfsave = noticepdf;
                    //string Link = "http://" + authority + noticepdf;
                    string msgurl = authority + noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;

                    if (MobileNo_SMS != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                        string msg = "प्रिय " + Name + ", केस क्रमांक " + CaseNo + " एवं प्रस्ताव क्रमांक " + RegistrationNo + " के लिए " + CaseNo + " से संबंधित पत्र देखने के लिए, कृपया दिए गए लिंक पर क्लिक करें:" + partyurl + " |";

                        //string msg = "Dear "+Name+",a case has been registered against your property ID 1234567 having previous case number "+CaseNo+ "and RRC case no" + CaseNo + ". To view the Auction Order click on below link www.google.com ";


                        string templateid = "1407168854332661538";

                        string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, SmssecureKey, templateid);
                        //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        COSNotice_Bal.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));







                    }

                }

            }

            if (chkEmail_CurrentSR.Checked)
            {
                //DataTable ds = ReportSeek_BAL.Get_CurrentSR_ReportSeeking_CoS(Convert.ToInt32(App_id));
                string Appid = Session["AppID"].ToString();
                DataSet dtPro = ReportSeek_BAL.Show_OriginalSR_SignedPath(Convert.ToInt32(Appid), 2);
                if (dtPro != null)
                {
                    if (dtPro.Tables.Count > 0)
                    {

                        string Current_SignPath = dtPro.Tables[0].Rows[0]["UNSIGNED_PDF_PATH"].ToString();

                        Session["CurrentSR_SignPath"] = Current_SignPath.ToString();


                    }
                }
                DataTable ds = ReportSeek_BAL.Get_SRList_ReportSeeking_CoS(2, Convert.ToInt32(ddlSRName.SelectedValue), 0);
                if (ds.Rows.Count > 0)
                {


                    Email = ds.Rows[0]["EMAIL"].ToString();
                    Name = ds.Rows[0]["office_name_en"].ToString();
                    //CaseNo = ds.Rows[0]["application_no"].ToString();  
                    //RegistrationNo = ds.Rows[0]["application_no"].ToString(); 
                    //noticepdf = ViewState["UnSignedPDF_Current"].ToString();

                    noticepdf = Session["CurrentSR_SignPath"].ToString();
                    //noticepdf = Session["RecentSheetPath_Current"].ToString();
                    PartyID = ds.Rows[0]["user_id"].ToString();

                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    //noticepdf = "/SampadaCMS" + noticepdf;
                    string noticepdfsave = noticepdf;
                    string Link = "http://" + authority + noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;

                    string msgurl = authority + noticepdf;
                    if (Email != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        EmailUtility emailUtility = new EmailUtility();
                        string userid = HttpContext.Current.Profile.UserName;
                        string IP = HttpContext.Current.Request.UserHostAddress;


                        if (File.Exists(Server.MapPath(noticepdf)))
                        {
                            emailUtility.SendEmail(RegistrationNo, CaseNo, Email, msg, PageUrl, userid, IP, Server.MapPath(noticepdf));
                        }




                    }



                }


            }
            DataTable dt2 = clsFinalOrderBAL.Update_Status_COS(Convert.ToInt32(Session["AppID"].ToString()), 20);
            hdnfldCaseNo.Value = ViewState["Case_Number"].ToString();
            hdnfAppld.Value = Session["AppID"].ToString();
            hdnfAppNo.Value = Session["Appno"].ToString();



            ScriptManager.RegisterStartupScript(pnlSRSendParty, pnlSRSendParty.GetType(), "none", "<script>ShowMessageYesNo_CurrentSR();</script>", false);
            //custom-tabs-one-profile-tab.Attributes["class"] = "nav-link disabled";
            custom_tabs_one_home_tab.Attributes["class"] = "nav-link disabled";

        }

        protected void ddlOrgReason_other_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlOrgReason_other.SelectedItem.Text == "अन्य")
            {
                //txtReason.Visible = true;
                PnlOtherRsn_other.Visible = true;
            }
            else
            {
                PnlOtherRsn_other.Visible = false;
            }
        }

        protected void btnCreateReportOtherSR_Click(object sender, EventArgs e)
        {
            DataSet dsDocDetails = new DataSet();

            dsDocDetails = ReportSeek_BAL.GetHearingDate(Convert.ToInt32(Session["AppID"].ToString()));
            if (dsDocDetails != null)
            {
                if (dsDocDetails.Tables.Count > 0)
                {

                    if (dsDocDetails.Tables[0].Rows.Count > 0)
                    {
                        string HearingDate = dsDocDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                        Session["HearingDate"] = HearingDate.ToString();
                    }
                }
            }

            lblToDesig_otherSR.Text = txtDegisnation.Text;
            lblToSRO_otherSR.Text = ddlAuthority.SelectedItem.Text;
            lblToAdd_otherSR.Text = txtAddress.Text;


            string reasonforreport = ddlOrgReason_other.SelectedItem.Text;
            //summernote3.Value = "Please Produce nature of documents and Nature of documents by registring officer as well " + reasonforreport + ". The hearing date falls for the case on  " + Session["HearingDate"].ToString() + " therefore produce the documents before that for the the case number " + ViewState["Case_Number"].ToString() + ". ";

            //pContent_OtherSR.InnerHtml = summernote3.InnerHtml;
            //Session["SelectedReason_Other"] = ddlOrgReason_other.SelectedItem.Text;



            if (ddlAuthority.SelectedIndex == 0)
            {
                ScriptManager.RegisterStartupScript(UpdPnl_OtherSR, UpdPnl_OtherSR.GetType(), "none", "<script> SelectAuthority();</script>", false);
            }
            else if (txtName.Text == "" || txtDegisnation.Text == "" || txtemail.Text == "" || txtphoneNo.Text == "" || txtWhatsapp.Text == "")
            {
                ScriptManager.RegisterStartupScript(UpdPnl_OtherSR, UpdPnl_OtherSR.GetType(), "none", "<script> ValidateOtherDetails();</script>", false);

                txtName.Focus();
            }
            else if (ddlOrgReason_other.SelectedIndex == 0)
            {
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> NoPartySelect();</script>");
                ScriptManager.RegisterStartupScript(UpdPnl_OtherSR, UpdPnl_OtherSR.GetType(), "none", "<script> NoPartySelect();</script>", false);
            }
            else
            {
                pnlReport_OtherSR.Visible = true;
                //pnlReport.v

                if (ddlOrgReason_other.SelectedItem.Text == "अन्य")
                {
                    lblReasonSub_OtherSR.Text = txtOrgOtherReason_OtherSR.Text;
                    //string SeekReportSubject_other = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + lblReasonSub_OtherSR.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                    string SeekReportSubject_other = lblReasonSub_OtherSR.Text + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";
                    summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + lblReasonSub_OtherSR.Text + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";


                    Session["SeekReportSubject_other"] = SeekReportSubject_other.ToString();
                }
                else
                {
                    //lblReasonSub_OtherSR.Text = ddlOrgReason_other.SelectedItem.Text;
                    //string SeekReportSubject_other = "In reference to the Proposal ID- " + lblProposalSub.Text + ". Regarding  " + ddlOrgReason_other.SelectedItem.Text + " , Case Number - " + lblCaseNoSub.Text + ". ";
                    string SeekReportSubject_other = ddlOrgReason_other.SelectedItem.Text + "प्रस्ताव आईडी-  " + ViewState["Appno"].ToString() + " के संदर्भ में,  प्रकरण क्रमांक - " + ViewState["Case_Number"].ToString() + ". ";

                    lblReasonSub_OtherSR.Text = SeekReportSubject_other;

                    summernote3.Value = "उपरुक्त विषयान्तर्गत लेख है की  आपकी ओर से प्रकरण के लिए " + reasonforreport + ". मामले की सुनवाई की तारीख  " + Session["HearingDate"].ToString() + " है, इसलिए मामले संख्या " + ViewState["Case_Number"].ToString() + " के लिए इससे पहले दस्तावेज पेश करें। ";

                    Session["SeekReportSubject_other"] = SeekReportSubject_other.ToString();
                }

                pContent_OtherSR.InnerHtml = summernote3.InnerHtml;


                string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + ".pdf";
                string ProposalSheetPath = Server.MapPath("~/Proposal/" + lblProposalNo.Text);
                ViewState["FirstFormate_Path"] = "~/Proposal/" + lblProposalNo.Text + "/" + FileNme;
                ViewState["SecondFormate_Path"] = "";

                string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, hdnvalue.Value);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddReport_OtherSR();</script>");
                ScriptManager.RegisterStartupScript(UpdPnl_OtherSR, UpdPnl_OtherSR.GetType(), "none", "<script> AddReport_other();</script>", false);
                pnlBtnSaveReport.Visible = true;
                btnEditSROther.Visible = true;
            }




        }


        private void SaveSeekReportPDF_Other()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                //string divSub = summernotesubject.Value;
                string divCon = summernote3.Value;
                //DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];

                //summernotesubject.Value = "In reference to the Proposal ID- " + lblProposalSub_OtherSR.Text + ".regarding  " + lblReasonSub_OtherSR.Text + " , Case Number - " + lblCaseNoSub_OtherSR.Text + ". ";
                //string divSub = summernotesubject.Value;


                string divSub = Session["SeekReportSubject_other"].ToString();


                StringBuilder stringBuilder = new StringBuilder();
                // stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto;  padding: 30px 30px 30px 30px;'>");

                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; text-align: center '>कार्यालय जिला पंजीयक एवं न्यायालय कलेक्टर ऑफ स्टाम्प जिला " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '> " + hdnOfficeNameHi.Value + " <br> ई - मेल - igrs@igrs.gov.in</h3> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र <br> प्रकरण क्रमांक -" + lblCaseNo.Text + " धारा - 33 </b></h2> ");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                //stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<br>");


                stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblRecord.Text + " <br><br><b>आवेदक (प्रथम पक्षकार)</b><br><br><br>" + lblDepartment.Text + "<br><br> <b>अनावेदक (द्वितीय पक्षकार) </b></h3>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>प्रति</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToDesig_otherSR.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToSRO_otherSR.Text + "</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: left '>" + lblToAdd_otherSR.Text + "</h2>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("<br>");






                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append("<p style = 'float: left;'>");
                stringBuilder.Append("<b> विषय :  </b>");
                stringBuilder.Append("</p>");
                stringBuilder.Append("<p style = 'text-align: justify;margin-bottom: 4px;'>");
                stringBuilder.Append(divSub);
                stringBuilder.Append("</p>");
                stringBuilder.Append("</div>");


                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append(divCon);
                stringBuilder.Append("</div>");


                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");


                stringBuilder.Append("</div>");
                //stringBuilder.Append("<div style ='text-align: right; padding: 2px 0 5px 0; color:#fff; position: top: 220px;relative; right:80px;' ><b>#8M2h8A4@N78O%bJd</b></div>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 220px;left:-50px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");
                stringBuilder.Append("<br/>");

                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 240px;left:150px;'>स्थान- " + hdnOfficeNameHi.Value + " <br/> जारी दिनांक: " + lblTodate.Text + " <br/> <br/></b> ");


                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                ViewState["FileNameUnSignedPDF"] = "";
                string FileNme = lblProposalNo.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_SeekReport_Other_SR.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF_Other"] = ConvertHTMToPDF(FileNme, "~/COS_SeekReport", stringBuilder.ToString());
                //ViewState["UnSignedPDF_Other"] = ConvertHTMToPDF(FileNme, "~/COS_SeekReport/", FileNme);

                Session["UnSignedPDF_Other"] = ViewState["UnSignedPDF_Other"].ToString();

                //string PATH = Session["UnSignedPDF_Other"].ToString();

                Session["RecentSheetPath_Other"] = "~/COS_SeekReport/" + FileNme;



            }
            catch (Exception ex)
            {

            }

        }

        protected void btnSaveReport_OtherSR_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop();</script>", false);
            SaveSeekReportPDF_Other();

            StringWriter iSW1 = new StringWriter();
            HtmlTextWriter iHTW1 = new HtmlTextWriter(iSW1);
            DataTable dt = new DataTable();

            string otherreason = txtOrgOtherReason.Text;
            string content = summernotesubject.Value;
            string Subject = hdnvalue.Value;
            string AuthorityName = txtName.Text;
            string AuthorityDegisnation = txtDegisnation.Text;
            string Authorityemail = txtemail.Text;
            string AuthorityphoneNo = txtphoneNo.Text;
            string AuthorityWhatsapp = txtWhatsapp.Text;
            string AuthorityAddress = txtAddress.Text;

            string subject_OtherSR = Session["SeekReportSubject_other"].ToString();



            //ViewState["NoticeCoptent"] = iHTW1.InnerWriter.ToString();
            dt = ReportSeek_BAL.InsertSeek_report_ForOther(Convert.ToInt32(ViewState["AppID"].ToString()), Session["Appno"].ToString(),
                subject_OtherSR, summernote3.Value, ddlOrgReason_other.SelectedItem.Text, "",
                AuthorityName, AuthorityDegisnation, Authorityemail, AuthorityphoneNo,
                AuthorityWhatsapp, AuthorityAddress, Context.Request.UserHostName, Context.Request.UserHostAddress, Session["RecentSheetPath_Other"].ToString(), txtOrgOtherReason_OtherSR.Text, 0, ddlAuthority.Text, "Other SR", 3, ddlOrgReason_other.SelectedValue);

            if (dt.Rows.Count > 0)
            {
                Session["seekreoprt_id_OtherSR"] = dt.Rows[0]["id"].ToString();
            }

            Session["SeekReportSubject_other"] = iHTW1.InnerWriter.ToString();


            //pContent_OtherSR.InnerHtml = ViewState["SeekReportContent"].ToString();
            pContent_OtherSR.InnerHtml = summernote3.Value;



            PnlEsign_OtherSR.Visible = true;
            btnSaveReport_OtherSR.Visible = false;
            btnCreateReportOtherSR.Visible = false;
            txtOrgOtherReason_OtherSR.ReadOnly = true;
            ddlAuthority.Enabled = false;
            txtName.ReadOnly = true;
            txtDegisnation.ReadOnly = true;
            txtemail.ReadOnly = true;
            txtphoneNo.ReadOnly = true;
            txtWhatsapp.ReadOnly = true;
            txtAddress.ReadOnly = true;
            txtOtherAuthority.ReadOnly = true;
            ddlOrgReason_other.Enabled = false;
            //custom_tabs_one_other_tab.Attributes["class"] = "nav-link disabled";

            ClientScript.RegisterStartupScript(this.GetType(), "ShowHideScript11", @"
                                                            <script type='text/javascript'>
                                                                $(document).ready(function() {
                                                                    
                                                                    $('#custom-tabs-one-RegisteredForm').hide();
                                                                });
                                                            </script>");


            //DataSet dsRecent = new DataSet();
            //dsRecent = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), ViewState["AppID"].ToString());
            //if (dsRecent != null)
            //{
            //    if (dsRecent.Tables.Count > 0)
            //    {

            //        if (dsRecent.Tables[0].Rows.Count > 0)
            //        {
            //            string fileName = dsRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
            //            Session["RecentSheetPath"] = fileName.ToString();
            //            docPath.Src = fileName;
            //        }
            //    }
            //}
        }

        protected void ddlAuthority_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (ddlAuthority.SelectedItem.Text == "अन्य")
            {
                PnlAuthority.Visible = true;
            }
            else
            {
                PnlAuthority.Visible = false;
            }


        }

        protected void btnEsignDSC_OtherSR_Click(object sender, EventArgs e)
        {
            int app_id = Convert.ToInt32(ViewState["AppID"].ToString());


            if (ddl_SignOptionOther.SelectedValue == "1")
            {
                if (TxtLast4DigitOther.Text.Length != 4)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                    TxtLast4DigitOther.Focus();
                    return;
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


            //-------eSign Start------------------------

            //string Location = "Project Office -" + HF_Office.Value;
            string Location = "Bhopal";

            //string ApplicationNo = hdnProposal.Value;

            //string PdfName = ViewState["FileNameUnSignedPDF"].ToString();

            string PdfName = "";
            if (ViewState["FileNameUnSignedPDF"] == null)
            {
                PdfName = Session["UNSIGNED_PDF_OtherSR"].ToString();
            }
            else
            {
                PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            }




            PdfName = PdfName.Replace("~/COS_SeekReport/", "");
            ViewState["filename"] = PdfName;
            //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
            string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_SeekReport/" + PdfName.ToString());
            string flSourceFile = FileNamefmFolder;

            if (File.Exists(FileNamefmFolder))
            {
                if (ddl_SignOptionOther.SelectedValue == "1")
                {
                    if (TxtLast4DigitOther.Text.Length != 4)
                    {
                        TxtLast4DigitOther.Focus();
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                        return;
                    }
                    string Last4DigitAadhaar = TxtLast4DigitOther.Text;

                    if (File.Exists(FileNamefmFolder))
                    {
                        string ResponseURL = null;
                        string txtSignedBy = "Collector of Stamp";
                        string UIDToken = "";
                        string TransactionId = getTransactionID();
                        string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                        string TransactionOn = "Pre";



                        ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_OtherSeekReport.aspx");

                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "" , false);
                        //getdata();

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





            PnlSend_OtherSR.Visible = true;
            btnEsignDSC_OtherSR.Visible = false;
            PnlEsign_OtherSR.Visible = false;
            btnSaveReport_OtherSR.Visible = false;
            GetOtherSRDetail(app_id);

        }

        protected void btnSendOtherSR_Click(object sender, EventArgs e)
        {
            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>Loaderstop();</script>", false);

            string App_id = Session["AppID"].ToString();

            //DataSet dsDocRecent;
            string Name;
            string whatsapp;
            string MobileNo_SMS;
            string CaseNo;
            string RegistrationNo;
            string Email;
            string noticepdf;
            string PartyID;
            if (chkWhatsApp_OtherSR.Checked)
            {
                string Appid = Session["AppID"].ToString();
                DataTable ds = new DataTable();

                ds = ReportSeek_BAL.Get_Other_ReportSeeking_CoS(Convert.ToInt32(Appid));

                if (ds.Rows.Count > 0)
                {

                    whatsapp = ds.Rows[0]["WHATSAPP_NO"].ToString();
                    Name = ds.Rows[0]["AUTHORITY_NAME"].ToString();
                    CaseNo = ds.Rows[0]["case_number"].ToString();
                    RegistrationNo = ds.Rows[0]["APPLICATIONNUMBER"].ToString();
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString();
                    //noticepdf = ViewState["UNSIGNED_PDF_PATH"].ToString();
                    PartyID = ds.Rows[0]["ID"].ToString();
                    if (RegistrationNo == "")
                    {
                        RegistrationNo = "NA";
                    }
                    if (whatsapp != "")
                    {
                        Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, "");
                    }

                }
            }
            if (chkSMS_OtherSR.Checked)
            {
                //dsDocRecent.Clear();
                DataTable ds = ReportSeek_BAL.Get_Other_ReportSeeking_CoS(Convert.ToInt32(App_id));

                if (ds.Rows.Count > 0)
                {

                    MobileNo_SMS = ds.Rows[0]["PHONE_NO"].ToString();
                    Name = ds.Rows[0]["AUTHORITY_NAME"].ToString();
                    CaseNo = ds.Rows[0]["case_number"].ToString();  //ds.Tables[0].Rows[i]["case_no"].ToString();
                    RegistrationNo = ds.Rows[0]["APPLICATIONNUMBER"].ToString(); //dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                    //noticepdf = ViewState["UNSIGNED_PDF_PATH"].ToString(); //dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString();
                    PartyID = ds.Rows[0]["ID"].ToString(); //dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();

                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    noticepdf = "/SampadaCMS" + noticepdf;
                    string noticepdfsave = noticepdf;
                    //string Link = "http://" + authority + noticepdf;
                    string msgurl = authority + noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;

                    if (MobileNo_SMS != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        //string msg = "Dear " + Name + ", To view the letter regarding " + RegistrationNo + " for case no. " + CaseNo + " & proposal no. " + RegistrationNo + " please click on the below link: " + partyurl + " |";




                        string msg = "प्रिय " + Name + ", केस क्रमांक " + CaseNo + " एवं प्रस्ताव क्रमांक " + RegistrationNo + " के लिए " + CaseNo + " से संबंधित पत्र देखने के लिए, कृपया दिए गए लिंक पर क्लिक करें:" + partyurl + " |";

                        //string msg = "Dear "+Name+",a case has been registered against your property ID 1234567 having previous case number "+CaseNo+ "and RRC case no" + CaseNo + ". To view the Auction Order click on below link www.google.com ";


                        string templateid = "1407168854332661538";

                        string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, SmssecureKey, templateid);
                        //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        COSNotice_Bal.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));





                    }

                }

            }

            if (chkEmail_OtherSR.Checked)
            {
                DataTable ds = ReportSeek_BAL.Get_Other_ReportSeeking_CoS(Convert.ToInt32(App_id));
                if (ds.Rows.Count > 0)
                {


                    Email = ds.Rows[0]["EMAIL_ID"].ToString();
                    Name = ds.Rows[0]["AUTHORITY_NAME"].ToString();
                    CaseNo = ds.Rows[0]["case_number"].ToString();
                    RegistrationNo = ds.Rows[0]["ApplicationNumber"].ToString(); //dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                    noticepdf = ds.Rows[0]["SIGNED_PDF_PATH"].ToString();
                    //noticepdf = Session["UnSignedPDF_Other"].ToString();

                    PartyID = ds.Rows[0]["ID"].ToString();
                    string authority = HttpContext.Current.Request.Url.Authority;
                    noticepdf = noticepdf.Replace("~", "");
                    noticepdf = "/SampadaCMS" + noticepdf;

                    string noticepdfsave = noticepdf;
                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + "" + "&Party_Id=" + PartyID;
                    string msgurl = authority + noticepdf;

                    if (Email != "")
                    {
                        if (RegistrationNo == "")
                        {
                            RegistrationNo = "NA";
                        }
                        //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";
                        string msg = "Dear " + Name + ", To view the letter regarding " + RegistrationNo + " for case no. " + CaseNo + " & proposal no. " + RegistrationNo + " please click on the below link: " + partyurl + " |";

                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                        EmailUtility emailUtility = new EmailUtility();
                        string userid = HttpContext.Current.Profile.UserName;
                        string IP = HttpContext.Current.Request.UserHostAddress;
                        if (File.Exists(Server.MapPath(noticepdf)))
                        {
                            emailUtility.SendEmail(RegistrationNo, CaseNo, Email, msg, PageUrl, userid, IP, Server.MapPath(noticepdf));
                        }
                        //emailUtility.SendEmail(RegistrationNo, CaseNo, Email, msg, PageUrl, userid, IP, Server.MapPath(noticepdf));


                    }
                }


            }



            DataTable dt2 = ReportSeek_BAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id), 24);

            hdnfldCaseNo.Value = ViewState["Case_Number"].ToString();
            hdnfAppld.Value = Session["AppID"].ToString();
            hdnfAppNo.Value = Session["Appno"].ToString();
            //DataTable dt2 = clsFinalOrderBAL.Update_Status_COS(Convert.ToInt32(App_id), 24);
            //Response.Redirect("Ordersheet.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageYesNo();</script>");

            //Page.ClientScript.RegisterClientScriptBlock(this.GetType(), "none", "ShowMessageYesNo();", true);

            ScriptManager.RegisterStartupScript(PnlSend_OtherSR, PnlSend_OtherSR.GetType(), "none", "<script> ShowMessageYesNo_Other();</script>", false);
            //custom-tabs-one-profile-tab.Attributes["class"] = "nav-link disabled";
            custom_tabs_on_profile_tab.Attributes["class"] = "nav-link disabled";
            custom_tabs_one_home_tab.Attributes["class"] = "nav-link disabled";


        }

        protected void btnEdit_Report_CurrentSR_Click(object sender, EventArgs e)
        {

        }

        protected void btnSkip_Click(object sender, EventArgs e)
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageYesNo_Skip();</script>");
        }
    }
}