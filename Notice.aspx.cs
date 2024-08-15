using eSigner;
using Newtonsoft.Json.Linq;
using RestSharp;
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
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using System.Collections;
using HSM_DSC;
using RestSharp;
using iTextSharp.text.pdf;

namespace CMS_Sampada.CoS
{

    public partial class Notice : System.Web.UI.Page
    {
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];

        string Application_Id_eMudra = ConfigurationManager.AppSettings["ApplicationId_eMudra"];
        string Department_Id_eMudra = ConfigurationManager.AppSettings["DepartmentId_eMudra"];
        string Secretkey_eMudra = ConfigurationManager.AppSettings["Secretkey_eMudra"];
        string eSignURL_eMudra = ConfigurationManager.AppSettings["eSignURL_eMudra"];

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];
        string Java_Path = ConfigurationManager.AppSettings["JavaPath"];

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
        string secureKey = ConfigurationManager.AppSettings["SmsSecureKey"];
        //string templateid = ConfigurationManager.AppSettings["templateid"];


        eSigner.eSigner _esigner = new eSigner.eSigner();
        CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        CoSOrderSheet_BAL clsOrdersheetBAL = new CoSOrderSheet_BAL();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        string All_OrderSheetFileNme = "";

        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];


        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)


        {
            if (!Page.IsPostBack)

            {
                lblDRoffice.Text = Session["District_NameHI"].ToString();
                lblDRoffice2.Text = Session["District_NameHI"].ToString();
                hdTocan.Value = Session["Token"].ToString();


                int Flag = 0;

                if (Request.QueryString["Flag"] != null)
                {
                    Flag = Convert.ToInt32(Request.QueryString["Flag"]);
                    if (Request.QueryString["Flag"].ToString() == "1" || Request.QueryString["Flag"].ToString() == "2")// Success eSign
                    {
                        if (Request.QueryString["Response_From"] != null)
                        {
                            if (Request.QueryString["Response_From"].ToString() == "Ordersheet")
                            {
                                if (Session["AppID"] != null && Session["order_id"] != null)
                                {
                                    DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(Session["AppID"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["order_id"].ToString()));
                                }
                            }
                            if (Request.QueryString["Response_From"].ToString() == "OrdersheetDSC")
                            {
                                if (Session["AppID"] != null && Session["order_id"] != null)
                                {
                                    DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(Session["AppID"].ToString()), "2", "", GetLocalIPAddress(), Convert.ToInt32(Session["order_id"].ToString()));
                                }

                            }

                        }
                        if (Request.QueryString["Response_type"] != null)
                        {
                            if (Request.QueryString["Response_type"].ToString() == "Notice")
                            {
                                if (Session["AppID"] != null && Session["Notice_ID"] != null)
                                {
                                    DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(Convert.ToInt32(Session["AppID"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["Notice_ID"].ToString()));
                                    pnlEsignDSC.Visible = false;
                                    pnlSend.Visible = true;
                                }
                            }
                            if (Request.QueryString["Response_type"].ToString() == "NoticeDSC")
                            {
                                if (Session["AppID"] != null && Session["Notice_ID"] != null)
                                {
                                    DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(Convert.ToInt32(Session["AppID"].ToString()), "2", "", GetLocalIPAddress(), Convert.ToInt32(Session["Notice_ID"].ToString()));
                                    pnlEsignDSC.Visible = false;
                                    pnlSend.Visible = true;
                                }
                            }
                        }
                    }
                    else if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "Notice") //faild Notice eSign
                    {
                        Flag = 3;
                        if (Session["FileNameUnSignedPDF"] == null)
                        {

                            pnlSaveDraft.Visible = true;

                        }
                        else if (Session["FileNameUnSignedPDF"].ToString() == "")
                        {

                            pnlSaveDraft.Visible = true;
                            pnlEsignDSC.Visible = false;

                        }
                        else
                        {
                            pnlSaveDraft.Visible = false;
                        }


                    }
                }
                if (Session["AppID"] != null)
                {
                    Session["AppID"] = Session["AppID"].ToString();
                    DataTable dt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                    if (dt.Rows[0]["STATUS_ID"].ToString() == "6")
                    {
                        DataTable dtStatus = clsNoticeBAL.Get_Status_Notice(Convert.ToInt32(Session["AppID"].ToString()));

                        if (dtStatus.Rows[0]["notice_docspath"].ToString() == "")
                        {
                            Flag = 4;
                        }
                        else
                        {
                            Flag = 3;

                        }

                    }

                }

                //Whatsss();
                if (Flag == 1)
                {


                    ViewState["Case_Number"] = "";
                    string Notice_ID = "0";
                    if (Session["CaseNum"] == null)
                    {
                        Session["CaseNum"] = Session["Case_Number"];
                    }

                    if (Session["CaseNum"] != null)
                    {
                        ViewState["Case_Number"] = Session["CaseNum"].ToString();
                        string appid = Session["AppID"].ToString();

                        if (Session["Notice_Id"] != null)
                        {
                            Notice_ID = Session["Notice_Id"].ToString();

                        }

                        if (Session["Notice_ID"] == null)
                        {
                            Session["Notice_ID"] = Notice_ID;
                            ViewState["NoticeIID"] = Notice_ID;
                        }
                        //int appid = 2;
                        ViewState["AppID"] = appid;
                        Session["AppID"] = appid;

                        string Appno = Session["ProposalID"].ToString();
                        //string Appno = "IGRSCMS1000102";
                        ViewState["Appno"] = Appno;
                        Session["Appno"] = Appno;
                        edit_notice.Visible = false;
                        DateTime Todate = DateTime.Now;
                        lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                        string HearingDT = DateTime.Now.ToString(Session["HearingDate"].ToString());
                        ViewState["HearingDate"] = HearingDT;
                        Session["HearingDate"] = HearingDT;
                        GetPartyDetail();
                        GetPreviousProcedding();
                        int HearingId = Convert.ToInt32(Session["Hearing_id"]);
                        ViewState["HearingId"] = (HearingId).ToString();
                        CreateAddCopyTable();

                        if (Session["Case_Status"] != null)
                        {
                            if (Session["Party_ID"] != null)
                            {
                                Checkedgrid();
                            }

                            //if (Session["Case_Status"].ToString() == "6")
                            //{

                            //    BtnSaveDraft.Visible = false;
                            //    pnlSaveDraft.Visible = false;
                            //    btnCreateNotice.Visible = false;
                            //    pnlNotice.Visible = true;
                            //    pnlEsignDSC.Visible = true;
                            //    edit_notice.Attributes["class"] = "nav-link disabled";
                            //    DisabledAllCheckBox();
                            //    Checkedgrid();
                            //    string FileNme = Session["NOTICE_DOCSPATH"].ToString();
                            //    ViewState["FileNameUnSignedPDF"] = FileNme;
                            //    Session["FileNameUnSignedPDF"] = FileNme;
                            //}
                        }
                        if (Session["NOTICE_PROCEEDING"] != null)
                        {
                            summernote.Value = Session["NOTICE_PROCEEDING"].ToString();
                            Session["NOTICE_PROCEEDING"] = null;
                        }
                        else
                        {
                            summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + HearingDT + " को जिला पंजीयक कार्यालय " + lblDRoffice.Text + " में 12.00 बजे पूर्वान्ह में की जावेगी।";
                        }

                        //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";
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
                                    docPath.Src = fileName;
                                    //grdTOCOrder.DataSource = dsDocRecent;
                                    //grdTOCOrder.DataBind();

                                }
                            }
                        }

                        DataSet dsDocDetails = new DataSet();
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

                        DataSet dsOrderSheet = new DataSet();
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

                        Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                        All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                        CreateEmptyFile(All_OrderSheetFileNme);
                        CraetSourceFile(Convert.ToInt32(appid));
                        AllDocList(Convert.ToInt32(appid));
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                    }
                    else
                    {
                        ViewState["Case_Number"] = "000002/B104/32/2022-23";
                    }

                }



                else
                {


                    if (Flag == 3)
                    {

                        if (Session["CaseNum"] != null)
                        {
                            ViewState["Case_Number"] = Session["CaseNum"].ToString();
                            string appid = Session["AppID"].ToString();
                            ViewState["App_Id"] = Session["AppID"].ToString();
                            ViewState["AppID"] = Session["AppID"].ToString();
                            string numbs = Session["CaseNum"].ToString();
                            string Notice_ID = Session["Notice_Id"].ToString();
                            ViewState["NoticeIID"] = Notice_ID;
                            ViewState["Notice_ID"] = Notice_ID;
                            ViewState["HearingId"] = (Session["Hearing_id"]).ToString();
                            string status = Request.QueryString["Response_Status"].ToString();
                            //Session["Notice_ID"] = "";
                            //Session["Party_ID"] = "";
                            //Session["Party_ID"] = string.Empty;
                            //Session["HearingDate"] = "";
                            //Session["AppID"] = "";
                            //ViewState["AppID"] = "";
                            //ViewState["Appno"] = "";
                            //Session["Appno"] = "";
                            Session["Notice_ID"] = Notice_ID;
                            Session["Party_ID"] = Session["Partyidram"].ToString();
                            string Party = Session["Partyidram"].ToString();
                            //List<string> uniquesParty = Party.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            //string newStrparty = string.Join(",", uniquesParty);
                            Session["Party_ID"] = Party;
                            Session["HearingDate"] = Session["HearingDate"].ToString();
                            //int appid = 2;

                            ViewState["AppID"] = appid;
                            Session["AppID"] = appid;
                            //string da = Session["Party_ID"].ToString();
                            string Appno = Session["Appno"].ToString();
                            List<string> uniques = Appno.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            string newStr = string.Join(",", uniques);
                            Appno = newStr;
                            Appno = Appno.Replace(",", "");
                            //string Appno = "IGRSCMS1000102";
                            ViewState["Appno"] = Appno;
                            Session["Appno"] = Appno;

                            DateTime Todate = DateTime.Now;
                            lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            string HearingDT = DateTime.Now.ToString(Session["HearingDate"].ToString());
                            //Session["pratilipi"] = dtApp;
                            //string ds = Session["pratilipi"].ToString();
                            //ViewState["pratilipi"] = Session["pratilipi"];
                            DataTable dataTable = (DataTable)ViewState["CopyDeatils"];
                            //DataTable dataTable=new DataTable()ViewState["sa"];
                            //dataTable = Session["pratilipi"].ToString();




                            GetPartyDetail();
                            GetPreviousProcedding();
                            CreateAddCopyTable();
                            //edit_notice.Attributes["class"] = "nav-link disabled";
                            //DisabledAllCheckBox();
                            int App_Id = Convert.ToInt32(appid.ToString());
                            int NoticeId = Convert.ToInt32(Notice_ID.ToString());

                            DataTable dsDisplayPratilipi = new DataTable();
                            dsDisplayPratilipi = clsNoticeBAL.GetAddCopyDeatils_Notice(App_Id, NoticeId);
                            if (dsDisplayPratilipi != null)
                            {
                                if (dsDisplayPratilipi.Rows.Count > 0)
                                {



                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                    GrdAddCopy_Details.DataBind();
                                    PnlPratilipi.Visible = true;
                                }
                            }

                            ViewState["PrtDeatils"] = dsDisplayPratilipi;
                            Checkedgrid();


                            Filldatatogrid();
                            string FileNme = "";
                            //string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_Notice.pdf";
                            if (Session["NOTICE_DOCSPATH"] != null)
                            {
                                FileNme = Session["NOTICE_DOCSPATH"].ToString();
                                ViewState["FileNameUnSignedPDF"] = FileNme;
                                Session["FileNameUnSignedPDF"] = FileNme;
                            }
                            //else
                            //{
                            //    ViewState["FileNameUnSignedPDF"] = FileNme;
                            //}
                            ViewState["status"] = status;
                            if (ViewState["status"] != null)
                            {
                                if (ViewState["status"].ToString() == "0")
                                {
                                    pnlEsignDSC.Visible = true;
                                    pnlSend.Visible = false;

                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','User has cancelled the eSign', 'warning');", true);

                                }
                                if (ViewState["status"].ToString() == "1")
                                {
                                    lblStatus.Text = "Document has successfully eSigned.";
                                    pnlEsignDSC.Visible = false;
                                    pnlSend.Visible = true;
                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','" + lblStatus.Text + "', 'warning');", true);

                                }
                            }

                            DataTable dt = clsNoticeBAL.Get_Notice_proceeding(Notice_ID);
                            if (dt.Rows.Count > 0)
                            {
                                summernote.Value = dt.Rows[0]["Notice_Proceeding"].ToString();
                            }
                            else
                            {
                                Session["NOTICE_PROCEEDING"] = "";
                                summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + HearingDT + " को जिला पंजीयक कार्यालय " + lblDRoffice.Text + " में 12.00 बजे पूर्वान्ह में की जावेगी।";
                            }



                            //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";
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
                                        docPath.Src = fileName;
                                        ifPreviousProceeding.Src = fileName.ToString();
                                        //grdTOCOrder.DataSource = dsDocRecent;
                                        //grdTOCOrder.DataBind();

                                    }
                                }
                            }

                            DataSet dsDocDetails = new DataSet();
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

                            DataSet dsOrderSheet = new DataSet();
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

                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                            CreateEmptyFile(All_OrderSheetFileNme);
                            CraetSourceFile(Convert.ToInt32(appid));

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                        }


                        pnlEsignDSC.Visible = true;
                        pnlNotice.Visible = true;

                        pnlSend.Visible = false;
                        pnlSaveDraft.Visible = false;
                        //PnlPratilipi.Visible = false;

                        AllDocList(Convert.ToInt32(Session["AppID"].ToString()));
                        edit_notice.Visible = true;
                        btnCreateNotice.Visible = true;
                        //GetPartyDetail();

                        //if (Session["SelPrt"]!=null)
                        //{
                        //    DataTable dt = (DataTable)Session["SelPrt"];
                        //    grdSelectedParties.DataSource = dt;
                        //    grdSelectedParties.DataBind();
                        //}

                    }
                    if (Flag == 2)
                    {

                        if (Session["CaseNum"] != null)
                        {
                            ViewState["Case_Number"] = Session["CaseNum"].ToString();
                            string appid = Session["AppID"].ToString();
                            string numbs = Session["CaseNum"].ToString();
                            string Notice_ID = Session["Notice_Id"].ToString();
                            string status = Request.QueryString["Response_Status"].ToString();
                            //Session["Notice_ID"] = "";
                            //Session["Party_ID"] = "";
                            //Session["Party_ID"] = string.Empty;
                            //Session["HearingDate"] = "";
                            //Session["AppID"] = "";
                            //ViewState["AppID"] = "";
                            //ViewState["Appno"] = "";
                            //Session["Appno"] = "";
                            Session["Notice_ID"] = Notice_ID;
                            Session["Party_ID"] = Session["Partyidram"].ToString();
                            string Party = Session["Partyidram"].ToString();
                            //List<string> uniquesParty = Party.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            //string newStrparty = string.Join(",", uniquesParty);
                            Session["Party_ID"] = Party;
                            Session["HearingDate"] = Session["HearingDate"].ToString();
                            //int appid = 2;

                            ViewState["AppID"] = appid;
                            Session["AppID"] = appid;
                            //string da = Session["Party_ID"].ToString();
                            string Appno = Session["Appno"].ToString();
                            List<string> uniques = Appno.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            string newStr = string.Join(",", uniques);
                            Appno = newStr;
                            Appno = Appno.Replace(",", "");
                            //string Appno = "IGRSCMS1000102";
                            ViewState["Appno"] = Appno;
                            Session["Appno"] = Appno;
                            edit_notice.Visible = false;
                            DateTime Todate = DateTime.Now;
                            lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            string HearingDT = DateTime.Now.ToString(Session["HearingDate"].ToString());
                            //Session["pratilipi"] = dtApp;
                            //string ds = Session["pratilipi"].ToString();
                            //ViewState["pratilipi"] = Session["pratilipi"];
                            DataTable dataTable = (DataTable)ViewState["CopyDeatils"];
                            //DataTable dataTable=new DataTable()ViewState["sa"];
                            //dataTable = Session["pratilipi"].ToString();




                            GetPartyDetail();
                            GetPreviousProcedding();
                            CreateAddCopyTable();
                            edit_notice.Attributes["class"] = "nav-link disabled";
                            DisabledAllCheckBox();
                            int App_Id = Convert.ToInt32(appid.ToString());
                            int NoticeId = Convert.ToInt32(Notice_ID.ToString());

                            DataTable dsDisplayPratilipi = new DataTable();
                            dsDisplayPratilipi = clsNoticeBAL.GetAddCopyDeatils_Notice(App_Id, NoticeId);
                            if (dsDisplayPratilipi != null)
                            {
                                if (dsDisplayPratilipi.Rows.Count > 0)
                                {



                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                    GrdAddCopy_Details.DataBind();
                                    PnlPratilipi.Visible = true;
                                }
                            }

                            ViewState["PrtDeatils"] = dsDisplayPratilipi;

                            Checkedgrid();


                            Filldatatogrid();
                            string FileNme = "";
                            //string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_Notice.pdf";
                            if (Session["NOTICE_DOCSPATH"] != null)
                            {
                                FileNme = Session["NOTICE_DOCSPATH"].ToString();
                                ViewState["FileNameUnSignedPDF"] = FileNme;
                                Session["FileNameUnSignedPDF"] = FileNme;
                            }
                            //else
                            //{
                            //    ViewState["FileNameUnSignedPDF"] = FileNme;
                            //}
                            ViewState["status"] = status;
                            if (ViewState["status"] != null)
                            {
                                if (ViewState["status"].ToString() == "0")
                                {
                                    pnlEsignDSC.Visible = true;
                                    pnlSend.Visible = false;

                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','User has cancelled the eSign', 'warning');", true);

                                }
                                if (ViewState["status"].ToString() == "1")
                                {
                                    lblStatus.Text = "Document has successfully eSigned.";
                                    pnlEsignDSC.Visible = false;
                                    pnlSend.Visible = true;
                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','" + lblStatus.Text + "', 'warning');", true);

                                }
                            }

                            DataTable dt = clsNoticeBAL.Get_Notice_proceeding(Notice_ID);
                            if (dt.Rows.Count > 0)
                            {
                                summernote.Value = dt.Rows[0]["Notice_Proceeding"].ToString();
                            }
                            else
                            {
                                Session["NOTICE_PROCEEDING"] = "";
                                summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + HearingDT + " को जिला पंजीयक कार्यालय " + lblDRoffice.Text + " में 12.00 बजे पूर्वान्ह में की जावेगी।";
                            }



                            //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";
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
                                        docPath.Src = fileName;
                                        ifPreviousProceeding.Src = fileName.ToString();
                                        //grdTOCOrder.DataSource = dsDocRecent;
                                        //grdTOCOrder.DataBind();

                                    }
                                }
                            }

                            DataSet dsDocDetails = new DataSet();
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

                            DataSet dsOrderSheet = new DataSet();
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

                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                            CreateEmptyFile(All_OrderSheetFileNme);
                            CraetSourceFile(Convert.ToInt32(appid));

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                        }


                        //pnlEsignDSC.Visible = false;
                        pnlNotice.Visible = true;
                        pnlSaveDraft.Visible = false;
                        //PnlPratilipi.Visible = false;
                        btnCreateNotice.Visible = false;
                        AllDocList(Convert.ToInt32(ViewState["AppID"].ToString()));
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

                                    string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                    ifRecent.Src = fileName;
                                    docPath.Visible = false;
                                    ifRecent.Visible = true;


                                }
                            }
                        }
                        //GetPartyDetail();

                        //if (Session["SelPrt"]!=null)
                        //{
                        //    DataTable dt = (DataTable)Session["SelPrt"];
                        //    grdSelectedParties.DataSource = dt;
                        //    grdSelectedParties.DataBind();
                        //}
                    }
                    if (Flag == 4)
                    {
                        if (Session["CaseNum"] != null)
                        {

                            edit_notice.Visible = true;
                            ViewState["Case_Number"] = Session["CaseNum"].ToString();
                            string appid = Session["AppID"].ToString();
                            ViewState["App_Id"] = Session["AppID"].ToString();
                            string numbs = Session["CaseNum"].ToString();
                            string Notice_ID = "";
                            //Session["Notice_ID"] = "";
                            string status = Request.QueryString["Response_Status"].ToString();
                            if (Session["Notice_ID"] != null)
                            {
                                Notice_ID = Session["Notice_ID"].ToString();
                                Session["Notice_ID"] = Notice_ID;
                                ViewState["NoticeIID"] = Notice_ID;

                            }

                            Session["Party_ID"] = "";
                            Session["Party_ID"] = string.Empty;
                            Session["HearingDate"] = "";
                            Session["AppID"] = "";
                            ViewState["AppID"] = "";
                            ViewState["Appno"] = "";
                            Session["Appno"] = "";
                            //Session["Notice_ID"] = Notice_ID;

                            if (Session["Partyidram"] != null)
                            {
                                Session["Party_ID"] = Session["Partyidram"].ToString();


                            }
                            string Party = Session["Partyidram"].ToString();
                            //List<string> uniquesParty = Party.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            //string newStrparty = string.Join(",", uniquesParty);
                            Session["Party_ID"] = Party;
                            Session["HearingDate"] = Session["HearingDate"].ToString();
                            //int appid = 2;

                            ViewState["AppID"] = appid;
                            Session["AppID"] = appid;
                            //string da = Session["Party_ID"].ToString();
                            string Appno = Session["Appno"].ToString();
                            List<string> uniques = Appno.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
                            string newStr = string.Join(",", uniques);
                            Appno = newStr;
                            Appno = Appno.Replace(",", "");
                            //string Appno = "IGRSCMS1000102";
                            ViewState["Appno"] = Appno;
                            Session["Appno"] = Appno;

                            DateTime Todate = DateTime.Now;
                            lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                            string HearingDT = DateTime.Now.ToString(Session["HearingDate"].ToString());
                            //Session["pratilipi"] = dtApp;
                            //string ds = Session["pratilipi"].ToString();
                            //ViewState["pratilipi"] = Session["pratilipi"];
                            DataTable dataTable = (DataTable)ViewState["CopyDeatils"];
                            //DataTable dataTable=new DataTable()ViewState["sa"];
                            //dataTable = Session["pratilipi"].ToString();




                            GetPartyDetail();
                            GetPreviousProcedding();
                            CreateAddCopyTable();
                            int HearingId = Convert.ToInt32(Session["Hearing_id"]);
                            ViewState["HearingId"] = (Session["Hearing_id"]).ToString();
                            //edit_notice.Attributes["class"] = "nav-link disabled";
                            //DisabledAllCheckBox();
                            int App_Id = Convert.ToInt32(appid.ToString());
                            int NoticeId = Convert.ToInt32(Notice_ID.ToString());

                            DataTable dsDisplayPratilipi = new DataTable();
                            dsDisplayPratilipi = clsNoticeBAL.GetAddCopyDeatils_Notice(App_Id, NoticeId);
                            if (dsDisplayPratilipi != null)
                            {
                                if (dsDisplayPratilipi.Rows.Count > 0)
                                {



                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                    GrdAddCopy_Details.DataBind();
                                    PnlPratilipi.Visible = true;
                                }
                            }
                            ViewState["PrtDeatils"] = dsDisplayPratilipi;
                            Checkedgrid();


                            Filldatatogrid();
                            string FileNme = "";
                            //string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_Notice.pdf";
                            if (Session["NOTICE_DOCSPATH"] != null)
                            {
                                FileNme = Session["NOTICE_DOCSPATH"].ToString();
                                ViewState["FileNameUnSignedPDF"] = FileNme;
                                Session["FileNameUnSignedPDF"] = FileNme;
                            }
                            //else
                            //{
                            //    ViewState["FileNameUnSignedPDF"] = FileNme;
                            //}
                            ViewState["status"] = status;
                            if (ViewState["status"] != null)
                            {
                                if (ViewState["status"].ToString() == "0")
                                {
                                    pnlEsignDSC.Visible = true;
                                    pnlSend.Visible = false;

                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','User has cancelled the eSign', 'warning');", true);

                                }
                                if (ViewState["status"].ToString() == "1")
                                {
                                    lblStatus.Text = "Document has successfully eSigned.";
                                    pnlEsignDSC.Visible = false;
                                    pnlSend.Visible = true;
                                    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','" + lblStatus.Text + "', 'warning');", true);

                                }
                            }

                            DataTable dt = clsNoticeBAL.Get_Notice_proceeding(Notice_ID);
                            if (dt.Rows.Count > 0)
                            {
                                summernote.Value = dt.Rows[0]["Notice_Proceeding"].ToString();
                            }
                            else
                            {
                                Session["NOTICE_PROCEEDING"] = "";
                                summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + HearingDT + " को जिला पंजीयक कार्यालय " + lblDRoffice.Text + " में 12.00 बजे पूर्वान्ह में की जावेगी।";
                            }



                            //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";
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
                                        docPath.Src = fileName;
                                        ifPreviousProceeding.Src = fileName.ToString();
                                        //grdTOCOrder.DataSource = dsDocRecent;
                                        //grdTOCOrder.DataBind();

                                    }
                                }
                            }

                            DataSet dsDocDetails = new DataSet();
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

                            DataSet dsOrderSheet = new DataSet();
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

                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                            CreateEmptyFile(All_OrderSheetFileNme);
                            CraetSourceFile(Convert.ToInt32(appid));

                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                        }


                        pnlEsignDSC.Visible = false;
                        pnlNotice.Visible = true;

                        pnlSend.Visible = false;
                        pnlSaveDraft.Visible = true;
                        //PnlPratilipi.Visible = false;
                        btnCreateNotice.Visible = true;
                        AllDocList(Convert.ToInt32(ViewState["App_Id"].ToString()));
                        //GetPartyDetail();

                        //if (Session["SelPrt"]!=null)
                        //{
                        //    DataTable dt = (DataTable)Session["SelPrt"];
                        //    grdSelectedParties.DataSource = dt;
                        //    grdSelectedParties.DataBind();
                        //}

                    }

                }

                DataTable dtt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                if (dtt.Rows[0]["STATUS_ID"].ToString() == "6")
                {
                    if (Request.QueryString["Flag"].ToString() == "0" && Request.QueryString["Response_type"].ToString() == "Notice") //faild Notice eSign
                    {
                        //Flag = 3;
                        //pnlSaveDraft.Visible = false;
                        edit_notice.Visible = false;
                        DisabledAllCheckBox();
                        btnCreateNotice.Visible = false;
                        pnlNotice.Visible = true;
                        pnlSaveDraft.Visible = false;
                        pnlEsignDSC.Visible = true;




                    }
                    else
                    {
                        pnlNotice.Visible = true;
                        pnlSaveDraft.Visible = true;
                        //PnlPratilipi.Visible = false;
                        btnCreateNotice.Visible = true;
                        btnFinalSubmit.Visible = false;
                    }





                }
                if (dtt.Rows[0]["STATUS_ID"].ToString() == "7" || dtt.Rows[0]["STATUS_ID"].ToString() == "8")
                {

                    pnlNotice.Visible = true;
                    pnlSaveDraft.Visible = false;
                    //PnlPratilipi.Visible = false;
                    btnCreateNotice.Visible = false;
                    btnFinalSubmit.Visible = false;
                    pnlEsignDSC.Visible = false;
                    pnlSend.Visible = true;
                    edit_notice.Visible = false;
                    DisabledAllCheckBox();


                }

                if (dtt.Rows[0]["STATUS_ID"].ToString() == "61")
                {

                    pnlNotice.Visible = true;
                    pnlSaveDraft.Visible = true;
                    //PnlPratilipi.Visible = false;
                    btnCreateNotice.Visible = true;
                    btnFinalSubmit.Visible = false;
                    pnlEsignDSC.Visible = false;
                    pnlSend.Visible = false;


                }

                SetDocumentBy_API();


            }
        }

        public void AllDocList(int APP_ID)
        {
            try
            {
                //DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);
                DataSet dsDocList = clsHearingBAL.GetAllDocList_Notice(APP_ID);
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());
                if (dsDocList != null)
                {
                    if (dsIndexDetails.Tables.Count > 0)
                    {

                        if (dsIndexDetails.Tables[0].Rows.Count > 0)
                        {
                            grdSRDoc.DataSource = dsIndexDetails.Tables[0];
                            grdSRDoc.DataBind();

                            ViewState["SortDirection"] = dsDocList;
                            ViewState["sortdr"] = "Asc";

                        }

                    }
                }



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
                string NoticePath = Server.MapPath(path);
                if (!Directory.Exists(NoticePath))
                {
                    Directory.CreateDirectory(NoticePath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = NoticePath;
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

                using (var stream = File.Create(Path.Combine(NoticePath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return NoticePath + "/" + FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoS_NoticeAllSheetDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoS_NoticeAllSheetDoc/", "<p>Order Sheet</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoS_NoticeAllSheetDoc/", "<p>Order Sheet</p>");
            }
            ViewState["ALLDocCAddedPDFPath"] = "~/CoS_NoticeAllSheetDoc/" + filename;
            ViewState["CoS_NoticeAllSheetDoc"] = serverpath;
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
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();dt.Rows[0]["Application_NO"].ToString();
                    }


                }
                //DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
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

                    string sourceFile = ViewState["CoS_NoticeAllSheetDoc"].ToString();
                    if (IsValidPdf(sourceFile))
                    {
                        MargeMultiplePDF(addedfilename, sourceFile);
                        setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());

                    }


                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
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
                    //RecentdocPath.Src = Base64;
                    RecentdocPath.Visible = true;
                }
                else
                {
                    RecentdocPath.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
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
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
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
            //if (File.Exists(Server.MapPath(vallPdfPath)))
            //{
            //    ifPDFViewerAll.Src = "~/CoS_NoticeAllSheetDoc/" + All_OrderSheetFileNme;
            //}
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_NoticeAllSheetDoc/" + All_OrderSheetFileNme;

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails.Rows.Count > 0)
                {
                    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        //ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
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

                    }

                }
            }
        }
        private void GetPartyDetail()
        {
            StringBuilder sb = new StringBuilder();
            if (ViewState["Case_Number"] != null)
            {
                DataTable dt = clsNoticeBAL.GetPartyDeatil(ViewState["Case_Number"].ToString(), Convert.ToInt32(ViewState["AppID"]), ViewState["Appno"].ToString());

                ViewState["PartyDetail"] = dt;
                if (dt.Rows.Count > 0)
                {
                    //hdnfApp_Id.Value = dt.Rows[0]["Application_NO"].ToString();
                    lblProposalIdHeading.Text = dt.Rows[0]["Application_NO"].ToString();
                    lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                    lblCaseNo.Text = dt.Rows[0]["Case_Number"].ToString();
                    lblDepartment.Text = dt.Rows[0]["department_name_hi"].ToString();
                    DataSet dsDetails = clsOrdersheetBAL.GetRegisteredDate(ViewState["Case_Number"].ToString());

                    if (dsDetails != null)
                    {
                        if (dsDetails.Tables.Count > 0)
                        {

                            if (dsDetails.Tables[0].Rows.Count > 0)
                            {
                                string RegisteredDate = dsDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                                lblRegisteredDate.Text = RegisteredDate;
                            }
                        }
                    }


                    foreach (DataRow dr in dt.Rows)
                    {

                    }
                    //chkblPartys.DataSource = dt;
                    //chkblPartys.DataTextField = "PartyNameWithType";
                    //chkblPartys.DataValueField = "Party_ID";
                    //chkblPartys.DataBind();
                    grdCaseList.DataSource = dt;
                    grdCaseList.DataBind();
                }

            }

        }
        private void GetPartyDetailcount()
        {
            StringBuilder sb = new StringBuilder();
            if (ViewState["Case_Number"] != null)
            {
                DataTable dt = clsNoticeBAL.GetPartyDeatil(ViewState["Case_Number"].ToString(), Convert.ToInt32(ViewState["AppID"]), ViewState["Appno"].ToString());
                if (dt.Rows.Count > 0)
                {
                    foreach (GridViewRow gvrow in grdCaseList.Rows)
                    {
                        CheckBox chk2 = (CheckBox)gvrow.FindControl("chkParty");
                        Label label = (Label)gvrow.FindControl("lblPartyId");
                        if (chk2.Checked == true)
                        {

                            string partyid = label.Text;
                            sb.Append(partyid);
                            sb.Append(",");
                        }
                        //if (label.Text)
                        //{

                        //}

                    }
                    if (sb.ToString() != "")
                    {
                        sb = new StringBuilder(sb.ToString().Substring(0, sb.ToString().Length - 1));
                        string Partyidram = sb.ToString();
                        ViewState["Partyidram"] = Partyidram;
                        Session["Partyidram"] = Partyidram;
                    }


                }
                ViewState["PartyDetail"] = dt;
                if (dt.Rows.Count > 0)
                {
                    //hdnfApp_Id.Value = dt.Rows[0]["Application_NO"].ToString();
                    lblProposalIdHeading.Text = dt.Rows[0]["Application_NO"].ToString();
                    lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                    lblCaseNo.Text = dt.Rows[0]["Case_Number"].ToString();
                    lblDepartment.Text = dt.Rows[0]["department_name_hi"].ToString();
                    DataSet dsDetails = clsOrdersheetBAL.GetRegisteredDate(ViewState["Case_Number"].ToString());

                    if (dsDetails != null)
                    {
                        if (dsDetails.Tables.Count > 0)
                        {

                            if (dsDetails.Tables[0].Rows.Count > 0)
                            {
                                string RegisteredDate = dsDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                                lblRegisteredDate.Text = RegisteredDate;
                            }
                        }
                    }


                    foreach (DataRow dr in dt.Rows)
                    {

                    }
                    //chkblPartys.DataSource = dt;
                    //chkblPartys.DataTextField = "PartyNameWithType";
                    //chkblPartys.DataValueField = "Party_ID";
                    //chkblPartys.DataBind();
                    grdCaseList.DataSource = dt;
                    grdCaseList.DataBind();
                }

            }

        }
        public void CreateAddCopyTable()
        {
            DataTable dtQues = new DataTable();
            try
            {
                dtQues.Columns.Add("Copyname");
                dtQues.Columns.Add("CopyEmail");
                dtQues.Columns.Add("CopyMob");
                dtQues.Columns.Add("CopyContent");
                dtQues.Columns.Add("CopyWhatsApp");
            }
            catch (Exception)
            {

            }
            ViewState["CopyDeatils"] = dtQues;
            DataTable dtPrtDeatils = new DataTable();
            try
            {
                dtPrtDeatils.Columns.Add("Party_ID");
                dtPrtDeatils.Columns.Add("Name");
                dtPrtDeatils.Columns.Add("SMS");
                dtPrtDeatils.Columns.Add("WhatsAPP");
                dtPrtDeatils.Columns.Add("Email");


            }
            catch (Exception)
            {

            }
            ViewState["PrtDeatils"] = dtPrtDeatils;
            DataTable dtSelectParty = new DataTable();
            try
            {
                dtSelectParty.Columns.Add("PartyID");
                dtSelectParty.Columns.Add("Name");
                dtSelectParty.Columns.Add("FatherName");
                dtSelectParty.Columns.Add("Address");



            }
            catch (Exception)
            {

            }
            ViewState["SelectParty"] = dtSelectParty;
        }

        private void Add_Copy()
        {
            string Copyname = txtCopyName.Text;
            string CopyEmail = txtCopyEmail.Text;
            string CopyMob = txtMobile.Text;
            string CopyContent = txt.Text;
            string Copywhatsapp = txtWhatsApp.Text;
            //ClsPaymentParams. = Appname;
            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
            dtApp.Rows.Add(Copyname, CopyEmail, CopyMob, CopyContent, Copywhatsapp);
            DataTable dsDisplayPratilipi = new DataTable();

            if (Session["Notice_Id"] != null && Session["Notice_Id"] != "")
            {
                dsDisplayPratilipi = clsNoticeBAL.GetAddCopyDeatils_Notice(Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(Session["Notice_Id"].ToString()));
                if (dsDisplayPratilipi != null)
                {
                    if (dsDisplayPratilipi.Rows.Count > 0)
                    {
                        for (int i = 0; i < dsDisplayPratilipi.Rows.Count; i++)
                        {
                            {


                                string Copyname_P = (dsDisplayPratilipi.Rows[i]["NAME"].ToString());
                                string CopyEmail_P = (dsDisplayPratilipi.Rows[i]["EMAIL"].ToString());
                                string CopyMob_P = (dsDisplayPratilipi.Rows[i]["PHONE_NO"].ToString());
                                string CopyContent_P = (dsDisplayPratilipi.Rows[i]["CopyContent"].ToString());
                                string CopyWhatsApp_P = (dsDisplayPratilipi.Rows[i]["whatsapp_no"].ToString());
                                dtApp.Rows.Add(Copyname_P, CopyEmail_P, CopyMob_P, CopyContent_P, CopyWhatsApp_P);


                            }


                        }



                    }
                }

            }






            Session["pratilipi"] = dtApp;
            string sa = Session["pratilipi"].ToString();
            ViewState["CopyDeatils"] = dtApp;
            DataTable dtAppp = (DataTable)ViewState["CopyDeatils"];

            if (dtApp.Rows.Count > 0)
            {

                GrdAddCopy_Details.DataSource = dtApp;
                GrdAddCopy_Details.DataBind();

                txtCopyName.Text = "";
                txtCopyEmail.Text = "";
                txtMobile.Text = "";
                txt.Text = "";
                txtWhatsApp.Text = "";
                //txtWhatsApp.Text = "";
                PnlPratilipi.Visible = true;





            }



            else
            {

            }

        }
        private void GetPreviousProcedding()
        {
            if (ViewState["Case_Number"] != null)
            {

                DataTable dt = clsNoticeBAL.Get_Proceeding(Convert.ToInt32(ViewState["AppID"]));
                ViewState["Proceeding"] = dt;
                if (dt.Rows.Count > 0)
                {
                    Session["HearingDate"] = dt.Rows[0]["hearingdateNotice"].ToString();
                    RptrProcedding.DataSource = dt;
                    RptrProcedding.DataBind();
                }

            }

        }
        protected void grdCaseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {


                if (e.CommandName == "SelectApplication")
                {
                    //hdnfApp_Id.Value = e.CommandArgument.ToString().Split(',')[0].ToString();
                    //lblApplication_No.Text = e.CommandArgument.ToString().Split(',')[1].ToString();
                    //hdnfCseNunmber.Value = e.CommandArgument.ToString().Split(',')[2].ToString();
                    hdnfParty_Name.Value = e.CommandArgument.ToString().Split(',')[2].ToString();





                }
                else if (e.CommandName == "NoticeCount")
                {


                }
            }
            catch (Exception)
            {

            }

        }

        protected void grdCaseList_RowCommand1(object sender, GridViewCommandEventArgs e)
        {
            try
            {

                if (e.CommandName == "SelectApplication")
                {
                    //hdnfApp_Id.Value = e.CommandArgument.ToString().Split(',')[0].ToString();
                    //lblApplication_No.Text = e.CommandArgument.ToString().Split(',')[1].ToString();
                    //hdnfCseNunmber.Value = e.CommandArgument.ToString().Split(',')[2].ToString();
                    hdnfParty_Name.Value = e.CommandArgument.ToString().Split(',')[2].ToString();





                }
                else if (e.CommandName == "NoticeCount")
                {


                }
            }
            catch (Exception)
            {

            }

        }

        //protected void btnSendNotice_Click(object sender, EventArgs e)
        //{
        //    string str = string.Empty;
        //    string strname = string.Empty;
        //    foreach (GridViewRow gvrow in grdCaseList.Rows)
        //    {
        //        CheckBox chk = (CheckBox)gvrow.FindControl("chkParty");
        //        if (chk != null & chk.Checked)
        //        {

        //            str += "<b>Name :- </b>" + gvrow.Cells[1].Text + ", ";
        //            str += "<b>Fater Name :- </b>" + gvrow.Cells[2].Text + ", ";
        //            str += "<b>Address :- </b>" + gvrow.Cells[3].Text;

        //            str += "<br />";
        //        }



        //    }
        //    lblRecord.Text = "" + str;
        //    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

        //}

        protected void btnSaveCopy_Click(object sender, EventArgs e)
        {
            //Add_Copy();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
            {
                if (txt.Text == "" || txtCopyName.Text == "" || txtMobile.Text == "" || txtCopyEmail.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> ValidateAddCopy();</script>");
                    txt.Focus();
                }

                else
                {
                    Session["pratilipi"] = txt.Text;
                    Add_Copy();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                }
            }

        }


        protected void btnCreateNotice_Click(object sender, EventArgs e)
        {
            int PartyID = 0;
            string Name;
            string SMS;
            string Email;

            String WhatsAPP;
            String FatherName;
            String Address;
            int PartyCount = 0;
            int SN = 0;
            string str = string.Empty;
            string strname = string.Empty;
            CreateAddCopyTable();
            DataTable dtParID = (DataTable)ViewState["PrtDeatils"];
            DataTable dtSelectPar = (DataTable)ViewState["SelectParty"];
            dtSelectPar.Clear();
            dtParID.Clear();
            foreach (GridViewRow gvrow in grdCaseList.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkParty");
                Label llblPartyId = (Label)gvrow.FindControl("lblPartyId");
                if (chk != null & chk.Checked)
                {
                    str = "";
                    str += ++SN + ".";
                    str += "<b>Name : </b>" + gvrow.Cells[2].Text + ", ";
                    str += "<b>Father Name : </b>" + gvrow.Cells[3].Text + ", ";
                    str += "<b>Address : </b>" + gvrow.Cells[4].Text;

                    str += "<br />";
                    //PartyID = Convert.ToInt32(gvrow.Cells[1].Text);

                    PartyID = Convert.ToInt32(llblPartyId.Text);

                    //ViewState["PartyIdD"] = Convert.ToInt32(gvrow.Cells[1].Text);
                    DataSet DSPartyDisplay = clsNoticeBAL.GetPartDetailsByID_Notice(PartyID);
                    ++PartyCount;
                    if (DSPartyDisplay.Tables.Count > 0)
                    {


                        Name = DSPartyDisplay.Tables[0].Rows[0]["Party_Name_Hi"].ToString();
                        SMS = DSPartyDisplay.Tables[0].Rows[0]["Mob_No"].ToString();
                        WhatsAPP = DSPartyDisplay.Tables[0].Rows[0]["Whatsapp_No"].ToString();
                        Email = DSPartyDisplay.Tables[0].Rows[0]["Email_Id"].ToString();
                        FatherName = DSPartyDisplay.Tables[0].Rows[0]["Father_Name_Hi"].ToString();
                        Address = DSPartyDisplay.Tables[0].Rows[0]["Party_AddressHI"].ToString();
                        dtParID.Rows.Add(PartyID, Name, SMS, WhatsAPP, Email);
                        dtSelectPar.Rows.Add(PartyID, Name, FatherName, Address);
                    }

                    ViewState["PrtDeatils"] = dtParID;
                    ViewState["SelPrt"] = dtSelectPar;
                    Session["SelPrt"] = dtSelectPar;

                }
                Hashtable hTable = new Hashtable();
                ArrayList duplicateList = new ArrayList();

                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dtParID.Rows)
                {
                    if (hTable.Contains(drow["Party_ID"]))
                        duplicateList.Add(drow);
                    else
                        hTable.Add(drow["Party_ID"], string.Empty);
                }

                //Removing a list of duplicate items from datatable.
                foreach (DataRow dRow in duplicateList)
                    dtParID.Rows.Remove(dRow);

                grdPartyDisplay.DataSource = dtParID;
                grdPartyDisplay.DataBind();
                Hashtable hTablet = new Hashtable();
                ArrayList duplicateListt = new ArrayList();

                //Add list of all the unique item value to hashtable, which stores combination of key, value pair.
                //And add duplicate item value in arraylist.
                foreach (DataRow drow in dtSelectPar.Rows)
                {
                    if (hTablet.Contains(drow["PartyID"]))
                        duplicateListt.Add(drow);
                    else
                        hTablet.Add(drow["PartyID"], string.Empty);
                }

                //Removing a list of duplicate items from datatable.
                foreach (DataRow dRow in duplicateListt)
                    dtSelectPar.Rows.Remove(dRow);
                grdSelectedParties.DataSource = dtSelectPar;
                grdSelectedParties.DataBind();


            }
            if (PartyCount == 0)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> NoPartySelect();</script>");
                pnlNotice.Visible = false;
                pnlSend.Visible = false;
            }
            else
            {
                lblRecord.Text = "" + str;
                pnlNotice.Visible = true;
                pnlSaveDraft.Visible = true;




                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                edit_notice.Visible = true;
            }


            //lblRecord.Text = "" + str;
            //pnlNotice.Visible = true;
            //pnlSaveDraft.Visible = true;




            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
        }
        private void SaveNotice(string Path)
        {
            try
            {

                string FileName = "COS_Notice_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                //ViewState["FileNameUnSignedPDF"] = FileName;
                //string OrderSheetPath = Server.MapPath("~/OrderSheet/" + lblApplication_No.Text);
                ViewState["ActualPath"] = Path;
                DateTime V_HEARINGDATE = DateTime.Now;
                if (Session["HearingDate"] != null)
                {
                    V_HEARINGDATE = Convert.ToDateTime(Session["HearingDate"].ToString());

                }


                DataTable dtUp = clsNoticeBAL.UpdateIntoNotice_DocPath(Convert.ToInt32(ViewState["AppID"].ToString()), ViewState["ActualPath"].ToString(), Convert.ToInt32(Session["Notice_ID"].ToString()));

                if (dtUp.Rows.Count > 0)
                {
                    //int Notice_ID = 0;
                    //Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                    int Notice_ID = 0;
                    Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                    Session["Notice_ID"] = Notice_ID;
                    ViewState["Notice_ID"] = dtUp.Rows[0]["notice_id"].ToString();
                    DataTable dtParty = (DataTable)ViewState["PrtDeatils"];

                    DataTable dtPartyCopy = (DataTable)ViewState["CopyDeatils"];





                    DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];



                }

            }
            catch (Exception ex)
            {

            }

        }
        private void SaveNoticePDF()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                string divCon = summernote.Value;
                DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];



                StringBuilder stringBuilder = new StringBuilder();
                // stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='height:1300px;margin: 0 auto; padding: 30px 30px 30px 30px;'>");

                stringBuilder.Append("<h2 style='font-size: 22px; margin: 0; font-weight: 600; text-align: center '>न्यायालय कलेक्टर ऑफ स्टाम्प</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; text-align: center '>जिला पंजीयक कार्यालय " + lblDRoffice.Text + " <br> ई-मेल - igrs@igrs.gov.in</h3> ");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; text-align: center '>अधिनियम 1899 की धारा 33 के स्टाम्प प्रकरणों की सुनवाई हेतु सूचना पत्र <br> प्रकरण क्रमांक -" + lblCaseNo.Text + " धारा - 33 </h3> ");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; text-align: center '>मध्यप्रदेश शासन</h3>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; text-align: center '>विरुद्ध</h3>");
                stringBuilder.Append("<br>");



                stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblRecord.Text + " <br><br><b>आवेदक (प्रथम पक्षकार)</b><br><br><br>" + lblDepartment.Text + "<br><br> <b>अनावेदक (द्वितीय पक्षकार) </b></h3>");
                stringBuilder.Append("<table  style='width: 920px; border: 1px solid black; border-collapse: collapse; '>");
                stringBuilder.Append("<tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>क्रमांक</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>नाम</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पिता का नाम</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पता</th></tr>");
                int srno = 1;
                for (int i = 0; i < ((DataTable)ViewState["SelPrt"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + srno + "</td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["Name"] + "</td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["FatherName"] + "</td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["Address"] + "</td>" +
                 "</tr>");
                    srno++;
                }
                stringBuilder.Append("</table>");

                stringBuilder.Append("</div>");




                stringBuilder.Append("<br>");



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
                if (dtPratilipi.Rows.Count > 0)
                {

                    stringBuilder.Append("<b> प्रतिलिपि </b>");
                    stringBuilder.Append("<br/>");
                    stringBuilder.Append("<br/>");
                    stringBuilder.Append("<table style='width: 920px; border: 1px solid black; border-collapse: collapse; '><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>क्र.</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>सूचनार्थ प्रेषित/विवरण</th></tr>");
                    int srno1 = 1;
                    for (int i = 0; i < ((DataTable)ViewState["CopyDeatils"]).Rows.Count; i++)
                    {
                        stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '><b>" + srno1 + "<b></td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 14px;text-align: center; '>" + ((DataTable)ViewState["CopyDeatils"]).Rows[i]["CopyContent"] + "</td></tr>");
                        srno1++;
                    }
                    stringBuilder.Append("</table>");
                }
                else
                {

                }
                stringBuilder.Append("</div>");


                stringBuilder.Append("<br/>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 260px;left:-80px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 340px;left:150px;'>स्थान- न्यायालय कलेक्टर ऑफ स्टाम्प <br/>एवं जिला पंजीयक कार्यालय, " + lblDRoffice.Text + "<br/>जारी दिनांक: " + lblTodate.Text + " <br/> <br/></b> ");
                stringBuilder.Append("<b style='float: left; text-align: left; padding: 2px 0 5px 5px; position: relative;top: 10px;'>नोट : 1. पार्टी दस्तावेज पंजीयन के दौरान संपदा में पंजीकृत मोबाइल नंबर के साथ उपस्थित हों।<br/></b> ");
                stringBuilder.Append("<b style='float: left; text-align: left; padding: 2px 0 5px 5px; position: relative;top: 10px;'>2. यदि आप व्यक्तिगत सुनवाई चाहते हैं तो न्यायालय में उपस्थित होवें या फिर ऑनलाईन जबाव अपलोड करें।<br/></b> ");


                stringBuilder.Append("</div>");
                ViewState["FileNameUnSignedPDF"] = "";
                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_Notice.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                Session["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/COS_Notice/", stringBuilder.ToString());
                Session["RecentSheetPath"] = "~/COS_Notice/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                SaveNotice("~/COS_Notice/" + FileNme);
                //setRecentSheetPath();


            }
            catch (Exception ex)
            {

            }

        }
        protected void btnSendNotice_Click(object sender, EventArgs e)
        {

            try
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                pnlSaveDraft.Visible = false;

                string App_id = Session["AppID"].ToString();

                DataSet dsDocRecent;
                string Name;
                string whatsapp;
                string MobileNo_SMS;
                string CaseNo;
                string RegistrationNo;
                string Email;
                string noticepdf;
                string PartyID;
                if (chechwhats.Checked == false)
                {
                    if (checksms.Checked == false)
                    {

                        if (chkEmail.Checked == false)
                        {
                            //////Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> ValidNoticeSend();</script>",false);
                            ScriptManager.RegisterStartupScript(this, GetType(), "MultipleFunctions", "<script>ValidNoticeSend();</script>", false);
                        }

                        else
                        {

                            if (chechwhats.Checked)
                            {
                                string Appid = Session["AppID"].ToString();
                                dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);
                                //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                                if (dsDocRecent.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                    {
                                        Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                        if (Name == "")
                                        {
                                            Name = "NA";
                                        }
                                        whatsapp = dsDocRecent.Tables[0].Rows[i]["whatsappno"].ToString();
                                        CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                        RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                        noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                        PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                        if (RegistrationNo == "")
                                        {
                                            RegistrationNo = "NA";
                                        }
                                        if (whatsapp != "")
                                        {
                                            Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, Session["Notice_ID"].ToString());
                                        }

                                    }
                                }
                            }

                            if (checksms.Checked)
                            {
                                //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                                string Appid = Session["AppID"].ToString();
                                dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);

                                if (dsDocRecent.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                    {
                                        Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                        if (Name == "")
                                        {
                                            Name = "NA";
                                        }
                                        MobileNo_SMS = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                        CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                        RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                        noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                        PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                        string authority = HttpContext.Current.Request.Url.Authority;
                                        noticepdf = noticepdf.Replace("~", "");
                                        //noticepdf = "/SampadaCMS" + noticepdf;
                                        string noticepdfsave = noticepdf;
                                        //string Link = "http://" + authority + noticepdf;
                                        string msgurl = authority + noticepdf;
                                        //string msgurl = authority + "Hello";



                                        //string partyurl = citizenBaseUrl + "PartyNotice.aspx?AppID=" + Session["AppID"].ToString() + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

                                        string partyurl = "https://sampada.mpigr.gov.in/";


                                        if (MobileNo_SMS != "")
                                        {
                                            if (RegistrationNo == "")
                                            {
                                                RegistrationNo = "NA";
                                            }
                                            string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए नीचे दिये गए लिंक पर क्लिक करें। " + partyurl;

                                            // प्रिय {#var#}, आपकी संपत्ति रजिस्ट्री क्रमांक {#var#} के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर {#var#} है, कृपया नोटिस देखने के लिए नीचे दिये  गए  लिंक पर क्लिक करें।  {#var#}

                                            string NoticeCount = "1";

                                            //string msg = "Dear " + Name + ", The case has been registered against your property " + RegistrationNo + " having case number : " + CaseNo + " Please see the " + NoticeCount + " notice given below link " + partyurl;

                                            //Dear {#var#}, The case has been registered against your property {#var#} having case number :{#var#} Please see the {#var#} notice given below link {#var#} 


                                            string templateid = "1407168854103631812";              // Hindi template
                                                                                                    //string templateid = "1407168414968789459";            // English template



                                            string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, secureKey, templateid);
                                            //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                                            String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                                            clsNoticeBAL.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));

                                        }

                                    }
                                }
                            }

                            if (chkEmail.Checked)
                            {
                                dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                                if (dsDocRecent.Tables[0].Rows.Count > 0)
                                {
                                    for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                    {
                                        Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                        if (Name == "")
                                        {
                                            Name = "NA";
                                        }
                                        //whatsapp = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                        CaseNo = dsDocRecent.Tables[0].Rows[i]["case_number"].ToString();
                                        RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                        Email = dsDocRecent.Tables[0].Rows[i]["emailID"].ToString();
                                        noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                        PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                        string authority = HttpContext.Current.Request.Url.Authority;
                                        noticepdf = noticepdf.Replace("~", "");
                                        noticepdf = "/SampadaCMS" + noticepdf;
                                        string noticepdfsave = noticepdf;
                                        //string Link = "http://" + authority + noticepdf;
                                        string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

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
                            }

                            DataTable dt2 = clsNoticeBAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id));

                            string message = " swal('','Notice send successfully', 'info')";
                            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                            //return;
                            int Flag = 1;
                            Response.Redirect("Notice_Proceeding.aspx?Flag=" + Flag);
                        }

                    }

                    else
                    {
                        if (chechwhats.Checked)
                        {
                            string Appid = Session["AppID"].ToString();
                            dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);
                            //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                            if (dsDocRecent.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                {
                                    Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                    if (Name == "")
                                    {
                                        Name = "NA";
                                    }
                                    whatsapp = dsDocRecent.Tables[0].Rows[i]["whatsappno"].ToString();
                                    CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                    RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                    noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                    PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                    if (RegistrationNo == "")
                                    {
                                        RegistrationNo = "NA";
                                    }
                                    if (whatsapp != "")
                                    {
                                        Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, Session["Notice_ID"].ToString());
                                    }

                                }
                            }
                        }

                        if (checksms.Checked)
                        {
                            //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                            string Appid = Session["AppID"].ToString();
                            dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);

                            if (dsDocRecent.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                {
                                    Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                    if (Name == "")
                                    {
                                        Name = "NA";
                                    }
                                    MobileNo_SMS = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                    CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                    RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                    noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                    PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                    string authority = HttpContext.Current.Request.Url.Authority;
                                    noticepdf = noticepdf.Replace("~", "");
                                    //noticepdf = "/SampadaCMS" + noticepdf;
                                    string noticepdfsave = noticepdf;
                                    //string Link = "http://" + authority + noticepdf;
                                    string msgurl = authority + noticepdf;
                                    //string msgurl = authority + "Hello";



                                    //string partyurl = citizenBaseUrl + "PartyNotice.aspx?AppID=" + Session["AppID"].ToString() + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

                                    string partyurl = "https://sampada.mpigr.gov.in/";


                                    if (MobileNo_SMS != "")
                                    {
                                        if (RegistrationNo == "")
                                        {
                                            RegistrationNo = "NA";
                                        }
                                        string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए नीचे दिये गए लिंक पर क्लिक करें। " + partyurl;

                                        // प्रिय {#var#}, आपकी संपत्ति रजिस्ट्री क्रमांक {#var#} के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर {#var#} है, कृपया नोटिस देखने के लिए नीचे दिये  गए  लिंक पर क्लिक करें।  {#var#}

                                        string NoticeCount = "1";

                                        //string msg = "Dear " + Name + ", The case has been registered against your property " + RegistrationNo + " having case number : " + CaseNo + " Please see the " + NoticeCount + " notice given below link " + partyurl;

                                        //Dear {#var#}, The case has been registered against your property {#var#} having case number :{#var#} Please see the {#var#} notice given below link {#var#} 


                                        string templateid = "1407168854103631812";              // Hindi template
                                                                                                //string templateid = "1407168414968789459";            // English template



                                        string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, secureKey, templateid);
                                        //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                                        String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                                        clsNoticeBAL.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));

                                    }

                                }
                            }
                        }

                        if (chkEmail.Checked)
                        {
                            dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                            if (dsDocRecent.Tables[0].Rows.Count > 0)
                            {
                                for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                                {
                                    Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                    if (Name == "")
                                    {
                                        Name = "NA";
                                    }
                                    //whatsapp = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                    CaseNo = dsDocRecent.Tables[0].Rows[i]["case_number"].ToString();
                                    RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                    Email = dsDocRecent.Tables[0].Rows[i]["emailID"].ToString();
                                    noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                    PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                    string authority = HttpContext.Current.Request.Url.Authority;
                                    noticepdf = noticepdf.Replace("~", "");
                                    noticepdf = "/SampadaCMS" + noticepdf;
                                    string noticepdfsave = noticepdf;
                                    //string Link = "http://" + authority + noticepdf;
                                    string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

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
                        }

                        DataTable dt2 = clsNoticeBAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id));

                        string message = " swal('','Notice send successfully', 'info')";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                        //return;
                        int Flag = 1;
                        Response.Redirect("Notice_Proceeding.aspx?Flag=" + Flag);

                    }


                }

                else
                {

                    if (chechwhats.Checked)
                    {
                        string Appid = Session["AppID"].ToString();
                        dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);
                        //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                        if (dsDocRecent.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                            {
                                Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                if (Name == "")
                                {
                                    Name = "NA";
                                }
                                whatsapp = dsDocRecent.Tables[0].Rows[i]["whatsappno"].ToString();
                                CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                if (RegistrationNo == "")
                                {
                                    RegistrationNo = "NA";
                                }
                                if (whatsapp != "")
                                {
                                    Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, Session["Notice_ID"].ToString());
                                }

                            }
                        }
                    }

                    if (checksms.Checked)
                    {
                        //dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                        string Appid = Session["AppID"].ToString();
                        dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);

                        if (dsDocRecent.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                            {
                                Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                if (Name == "")
                                {
                                    Name = "NA";
                                }
                                MobileNo_SMS = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
                                RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                string authority = HttpContext.Current.Request.Url.Authority;
                                noticepdf = noticepdf.Replace("~", "");
                                //noticepdf = "/SampadaCMS" + noticepdf;
                                string noticepdfsave = noticepdf;
                                //string Link = "http://" + authority + noticepdf;
                                string msgurl = authority + noticepdf;
                                //string msgurl = authority + "Hello";



                                //string partyurl = citizenBaseUrl + "PartyNotice.aspx?AppID=" + Session["AppID"].ToString() + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

                                string partyurl = "https://sampada.mpigr.gov.in/";


                                if (MobileNo_SMS != "")
                                {
                                    if (RegistrationNo == "")
                                    {
                                        RegistrationNo = "NA";
                                    }
                                    string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए नीचे दिये गए लिंक पर क्लिक करें। " + partyurl;

                                    // प्रिय {#var#}, आपकी संपत्ति रजिस्ट्री क्रमांक {#var#} के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर {#var#} है, कृपया नोटिस देखने के लिए नीचे दिये  गए  लिंक पर क्लिक करें।  {#var#}

                                    string NoticeCount = "1";

                                    //string msg = "Dear " + Name + ", The case has been registered against your property " + RegistrationNo + " having case number : " + CaseNo + " Please see the " + NoticeCount + " notice given below link " + partyurl;

                                    //Dear {#var#}, The case has been registered against your property {#var#} having case number :{#var#} Please see the {#var#} notice given below link {#var#} 


                                    string templateid = "1407168854103631812";              // Hindi template
                                                                                            //string templateid = "1407168414968789459";            // English template



                                    string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, secureKey, templateid);
                                    //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                                    String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                                    clsNoticeBAL.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(PartyID.ToString()));

                                }

                            }
                        }
                    }

                    if (chkEmail.Checked)
                    {
                        dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["AppID"].ToString()), Session["Notice_ID"].ToString());
                        if (dsDocRecent.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
                            {
                                Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
                                if (Name == "")
                                {
                                    Name = "NA";
                                }
                                //whatsapp = dsDocRecent.Tables[0].Rows[i]["mobileno"].ToString();
                                CaseNo = dsDocRecent.Tables[0].Rows[i]["case_number"].ToString();
                                RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
                                Email = dsDocRecent.Tables[0].Rows[i]["emailID"].ToString();
                                noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
                                PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
                                string authority = HttpContext.Current.Request.Url.Authority;
                                noticepdf = noticepdf.Replace("~", "");
                                noticepdf = "/SampadaCMS" + noticepdf;
                                string noticepdfsave = noticepdf;
                                //string Link = "http://" + authority + noticepdf;
                                string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;

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
                    }

                    DataTable dt2 = clsNoticeBAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id));

                    string message = " swal('','Notice send successfully', 'info')";
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "sweetAlert", message, true);
                    //return;
                    int Flag = 1;
                    Response.Redirect("Notice_Proceeding.aspx?Flag=" + Flag);
                }
            }
            catch (Exception ex)
            {

            }

            //DataSet dsDocRecent;
            //dsDocRecent = clsNoticeBAL.GetNotice_Doc_Notice(ViewState["Case_Number"].ToString(), appid);
            //if (dsDocRecent != null)
            //{
            //    if (dsDocRecent.Tables.Count > 0)
            //    {

            //        if (dsDocRecent.Tables[0].Rows.Count > 0)
            //        {
            //            string fileName = dsDocRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
            //            Session["Recent"] = fileName.ToString();
            //            ifRecent.Src = fileName;
            //        }
            //    }
            //}
        }



        protected void BtnSaveDraft_Click(object sender, EventArgs e)
        {

            try
            {

                //string FileName = "COS_Notice_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                ////ViewState["FileNameUnSignedPDF"] = FileName;
                ////string OrderSheetPath = Server.MapPath("~/OrderSheet/" + lblApplication_No.Text);
                //ViewState["ActualPath"] = Path;
                DateTime V_HEARINGDATE = DateTime.Now;
                if (Session["HearingDate"] != null)
                {
                    V_HEARINGDATE = Convert.ToDateTime(Session["HearingDate"].ToString());

                }

                DataTable dtStatus = clsNoticeBAL.Get_Status_Notice(Convert.ToInt32(ViewState["AppID"].ToString()));

                if (dtStatus.Rows.Count > 0)
                {
                    if (dtStatus.Rows[0]["App_id"].ToString() != "")
                    {


                        DataTable dtUp = clsNoticeBAL.UpdateIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", V_HEARINGDATE, 0, 0, "", 0, Convert.ToInt32(ViewState["NoticeIID"].ToString()), txtCopyName.Text, txtCopyEmail.Text, txtMobile.Text, txt.Text, txtWhatsApp.Text, Convert.ToInt32(ViewState["HearingId"].ToString()));

                        if (dtUp.Rows.Count > 0)
                        {
                            //int Notice_ID = 0;
                            //Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                            int Notice_ID = 0;
                            Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                            Session["Notice_ID"] = Notice_ID;
                            ViewState["Notice_ID"] = dtUp.Rows[0]["notice_id"].ToString();
                            DataTable dtParty = (DataTable)ViewState["PrtDeatils"];

                            DataTable dtPartyCopy = (DataTable)ViewState["CopyDeatils"];



                            if (dtParty.Rows.Count > 0)
                            {
                                DataTable dtDeParties = clsNoticeBAL.Get_DeletePreviousParties(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Notice_ID"].ToString()), "0");
                                for (int i = 0; i < dtParty.Rows.Count; i++)
                                {
                                    {
                                        int Party_Id = Convert.ToInt32(dtParty.Rows[i]["Party_ID"].ToString());
                                        DataTable dttUp = clsNoticeBAL.UpdateIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", Convert.ToDateTime(Session["HearingDate"].ToString()), 0, 0, "", Party_Id, Notice_ID, txtCopyName.Text, txtCopyEmail.Text, txtMobile.Text, txt.Text, txtWhatsApp.Text, Convert.ToInt32(ViewState["HearingId"].ToString()));
                                    }

                                }

                            }

                            DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];

                            if (dtCopy.Rows.Count > 0)
                            {
                                DataTable dtDeParties = clsNoticeBAL.Get_DeletePreviousParties(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Notice_ID"].ToString()), "1");
                                for (int i = 0; i < dtCopy.Rows.Count; i++)
                                {
                                    {


                                        string Copyname = (dtCopy.Rows[i]["Copyname"].ToString());
                                        string CopyEmail = (dtCopy.Rows[i]["CopyEmail"].ToString());
                                        string CopyMob = (dtCopy.Rows[i]["CopyMob"].ToString());
                                        string CopyContent = (dtCopy.Rows[i]["CopyContent"].ToString());
                                        string CopyWhatsApp = (dtCopy.Rows[i]["CopyWhatsApp"].ToString());
                                        DataTable dttUp = clsNoticeBAL.UpdateIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", Convert.ToDateTime(Session["HearingDate"].ToString()), 0, 1, "", 0, Notice_ID, Copyname, CopyEmail, CopyMob, CopyContent, CopyWhatsApp, Convert.ToInt32(ViewState["HearingId"].ToString()));


                                    }


                                }

                            }

                            edit_notice.Attributes["class"] = "nav-link disabled";
                            btnCreateNotice.Visible = false;
                            btnFinalSubmit.Visible = true;
                            BtnSaveDraft.Visible = false;
                            //grdCaseList.Columns[7].ReadOnly = true;
                            DisabledAllCheckBox();



                            pnlEsignDSC.Visible = false;

                        }
                    }



                }
                else
                {

                    DataTable dtUp = clsNoticeBAL.InsertIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", V_HEARINGDATE, 0, 0, "", 0, 0, txtCopyName.Text, txtCopyEmail.Text, txtMobile.Text, txt.Text, txtWhatsApp.Text, Convert.ToInt32(ViewState["HearingId"].ToString()));

                    if (dtUp.Rows.Count > 0)
                    {
                        //int Notice_ID = 0;
                        //Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                        int Notice_ID = 0;
                        Notice_ID = Convert.ToInt32(dtUp.Rows[0]["notice_id"].ToString());
                        Session["Notice_ID"] = Notice_ID;
                        ViewState["Notice_ID"] = dtUp.Rows[0]["notice_id"].ToString();
                        DataTable dtParty = (DataTable)ViewState["PrtDeatils"];

                        DataTable dtPartyCopy = (DataTable)ViewState["CopyDeatils"];



                        if (dtParty.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtParty.Rows.Count; i++)
                            {
                                {
                                    int Party_Id = Convert.ToInt32(dtParty.Rows[i]["Party_ID"].ToString());
                                    DataTable dttUp = clsNoticeBAL.InsertIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", Convert.ToDateTime(Session["HearingDate"].ToString()), 0, 0, "", Party_Id, Notice_ID, txtCopyName.Text, txtCopyEmail.Text, txtMobile.Text, txt.Text, txtWhatsApp.Text, Convert.ToInt32(ViewState["HearingId"].ToString()));
                                }

                            }

                        }

                        DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];

                        if (dtCopy.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtCopy.Rows.Count; i++)
                            {
                                {


                                    string Copyname = (dtCopy.Rows[i]["Copyname"].ToString());
                                    string CopyEmail = (dtCopy.Rows[i]["CopyEmail"].ToString());
                                    string CopyMob = (dtCopy.Rows[i]["CopyMob"].ToString());
                                    string CopyContent = (dtCopy.Rows[i]["CopyContent"].ToString());
                                    string CopyWhatsApp = (dtCopy.Rows[i]["CopyWhatsApp"].ToString());
                                    DataTable dttUp = clsNoticeBAL.InsertIntoNotice(Convert.ToInt32(ViewState["AppID"].ToString()), 0, ViewState["Case_Number"].ToString(), "", summernote.Value, "", "", "", Convert.ToDateTime(Session["HearingDate"].ToString()), 0, 1, "", 0, Notice_ID, Copyname, CopyEmail, CopyMob, CopyContent, CopyWhatsApp, Convert.ToInt32(ViewState["HearingId"].ToString()));


                                }


                            }

                        }

                        DataSet dsDocRecent;

                        dsDocRecent = clsNoticeBAL.GetNotice_Doc_Notice(Convert.ToInt32(ViewState["Notice_ID"].ToString()));
                        if (dsDocRecent != null)
                        {
                            if (dsDocRecent.Tables.Count > 0)
                            {

                                if (dsDocRecent.Tables[0].Rows.Count > 0)
                                {
                                    string fileName = dsDocRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                    Session["Recent"] = fileName.ToString();
                                    ifRecent.Src = fileName;
                                }
                            }
                        }

                        edit_notice.Attributes["class"] = "nav-link disabled";
                        btnCreateNotice.Visible = false;
                        btnFinalSubmit.Visible = true;
                        BtnSaveDraft.Visible = false;
                        //grdCaseList.Columns[7].ReadOnly = true;
                        DisabledAllCheckBox();



                        pnlEsignDSC.Visible = false;

                    }
                }




            }
            catch (Exception ex)
            {

            }

            string Copy_Name;
            string Copy_SMS;
            string Copy_Email;
            String Copy_WhatsAPP;
            String Party_ID;
            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
            DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

            DataTable dsCopy = new DataTable();

            try
            {
                dsCopy.Columns.Add("Party_ID");
                dsCopy.Columns.Add("Copyname");
                dsCopy.Columns.Add("CopyEmail");
                dsCopy.Columns.Add("CopyMob");
                dsCopy.Columns.Add("CopyContent");
                dsCopy.Columns.Add("Copywhatsapp");
            }
            catch (Exception)
            {

            }

            //if (dtCopyShow.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtCopyShow.Rows.Count; i++)
            //    {
            //        Party_ID = dtCopyShow.Rows[i]["Party_ID"].ToString();
            //        Copy_Name = dtCopyShow.Rows[i]["Name"].ToString();
            //        Copy_SMS = dtCopyShow.Rows[i]["Phone_No"].ToString();
            //        Copy_WhatsAPP = dtCopyShow.Rows[i]["WhatsApp_No"].ToString();
            //        Copy_Email = dtCopyShow.Rows[i]["Email"].ToString();
            //        dtApp.Rows.Add(Party_ID, Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
            //    }


            //}


            //if (dtApp.Rows.Count > 0)
            //{
            //    for (int i = 0; i < dtApp.Rows.Count; i++)
            //    {
            //        Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
            //        Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
            //        Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
            //        Copy_Email = dtApp.Rows[i]["CopyEmail"].ToString();
            //        dsCopy.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
            //    }


            //}

            //grdPartyDisplay.DataSource = dsCopy;
            //grdPartyDisplay.DataBind();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
            pnlSaveDraft.Visible = true;
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

        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            try
            {
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
                    string PdfName = "";
                    //string ApplicationNo = hdnProposal.Value;
                    if (Session["FileNameUnSignedPDF"] != null)
                    {
                        PdfName = Session["FileNameUnSignedPDF"].ToString();
                    }

                    PdfName = PdfName.Replace("~/COS_Notice/", "");
                    ViewState["filename"] = PdfName;
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_Notice/" + PdfName.ToString());
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
                                    Session["Partyidram"] = Session["Party_ID"].ToString();

                                }
                                else
                                {
                                    GetPartyDetailcount();
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
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Appno=" + Session["Appno"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"]);
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"].ToString() + "&Response_type=Notice");
                                ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Notice.aspx?Response_type=Notice");


                                //getdata();

                                //DataTable dt = clsNoticeBAL.InserteSignDSC_Status_Notice(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), Notice_id);

                                AuthMode authMode = new AuthMode();

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

                }

                else if (ddl_SignOption.SelectedValue == "3")
                {
                    if (TxtLast4Digit.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        TxtLast4Digit.Focus();
                        return;
                    }


                    int Flag = 1;
                    //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                    //-------eSign Start------------------------

                    //string Location = "Project Office -" + HF_Office.Value;
                    string Location = "Bhopal";



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
                                if (Session["Party_ID"] != null)
                                {
                                    Session["Party_ID"] = Session["Party_ID"].ToString();
                                    Session["Partyidram"] = Session["Party_ID"].ToString();

                                }
                                else
                                {
                                    GetPartyDetailcount();
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

                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                                ResponseURL_eMudra = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Notice.aspx?Response_type=Notice");

                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Notice.aspx?Response_type=Notice");

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
                    if (Session["Party_ID"] != null)
                    {
                        Session["Party_ID"] = Session["Party_ID"].ToString();
                        Session["Partyidram"] = Session["Party_ID"].ToString();

                    }
                    else
                    {
                        GetPartyDetailcount();
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

                    string PdfName = Session["FileNameUnSignedPDF"].ToString();
                    PdfName = PdfName.Replace("~/COS_Notice/", "");
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_Notice/" + PdfName.ToString());
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
                    string reason = "Notice";
                    string partitionName = Partition_Name;
                    string partitionPassword = Partition_Password;
                    string hsmSlotNo = HSM_Slot_No; ////Session["HSMSlotNo"].ToString();
                    string javaPath = Java_Path;

                    //string label = "MAHESH_KUMAR";
                    //string signName = "COS";
                    //string location = "Guna";
                    //string reason = "Order Sheet";
                    //string partitionName = "sampadap2";
                    //string partitionPassword = "sampada@part2";
                    string msg = "";

                    //Session["order_id"] = Session["ordersheet_id_Status"].ToString();

                    if (File.Exists(FileNamefmFolder))
                    {
                        HSMSigner hSMSigner = new HSMSigner(unsignFilePath, signFileFinalPath, label, signName, location, reason, partitionName, partitionPassword, hsmSlotNo, javaPath);

                        //hSMSigner.hsm_DSC();
                        msg = hSMSigner.hsm_DSC();
                        //Session["HSM_DSC"] = hsmMsg.Text;
                        hsmMsg.Text = javaPath + ", error: " + msg;
                        if (File.Exists(NewPath))
                        {
                            Session["RecentSheetPath"] = NewPath;

                            int Flag = 2;
                            string resp_status = 1.ToString();
                            string Response_From = "NoticeDSC";
                            //string url = "Notice.aspx?Case_Number=" + Session["CaseNum"].ToString() + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag + "&Response_Status=" + resp_status;
                            //string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status;
                            string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + Response_From;

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


                            ///ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Signed ordersheet saved Successfully');window.location='" + url + "';", true);
                            ///
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Signed ordersheet saved Successfully','success');window.location='" + url + "';", true);

                            //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Signed ordersheet saved Successfully', 'success');window.location='" + url + "'", true);

                        }
                        else
                        {
                            if (msg != "")
                            {
                                ddl_SignOption.SelectedValue = "0";
                                ddleAuthMode.SelectedValue = "0";

                                objClsNewApplication.InsertExeption("Index_Tab_ErrorException.Message = " + msg + ",StatusDescription = Error in HSM DSC", "COS Notice", "Notice.aspx", GetLocalIPAddress());
                            }
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowErrorMessageDSC('" + msg + "')", true);
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



                //getdata();
                //pnlSend.Visible = true;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

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

                            string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                            ifRecent.Src = fileName;
                            docPath.Visible = false;
                            ifRecent.Visible = true;


                        }
                    }
                }
            }
            catch (Exception)
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('',' eSign फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);

            }
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
                //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Method not allowed.','Not done')", true);

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

        protected void chkAllSelect_CheckedChanged(object sender, EventArgs e)
        {

            CheckBox chkAll = (CheckBox)grdCaseList.HeaderRow.FindControl("chkAllSelect");
            try
            {
                foreach (GridViewRow row in grdCaseList.Rows)
                {
                    CheckBox chkRow = (CheckBox)row.FindControl("chkParty");
                    chkRow.Checked = chkAll.Checked;
                }
            }
            catch (Exception ex)
            {

            }
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

                    string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrtionNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + uri + " ।";

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
                    string url = @"https://media.smsgupshup.com/GatewayAPI/rest?userid=" + WhatsApp_Userid + "&password=" + WhatsApp_Pwd + "&send_to=" + cntctnumb + "&v=1.1&format=json&msg_type=DOCUMENT&method=SENDMEDIAMESSAGE&caption=" + message + "&media_url=" + RAM_MediaUrl + "";
                    //string url = @"https://media.smsgupshup.com/GatewayAPI/rest?userid=2000215884&password=tbMsSWEk&send_to=" + cntctnumb + "&v=1.1&format=json&msg_type=DOCUMENT&method=SENDMEDIAMESSAGE&caption=" + message + "&media_url=" + RAM_MediaUrl + "";
                    //string url = Whatsapp_URL + "userid=" + WhatsApp_Userid + "&password=" + WhatsApp_Pwd + "&send_to=" + cntctnumb + "&v=1.1&format=json&msg_type=DOCUMENT&method=SENDMEDIAMESSAGE&caption=" + message + "&media_url=" + RAM_MediaUrl + "";

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
                    clsNoticeBAL.WhatsappResponse_Insert(RegistrationNo, CaseNo, "whatsapp", msg, responseString, PageUrl, cntctnumb, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, PartyID, noticepdfsave, Notice_ID);
                    //Console.WriteLine("Message Send Successfully");

                    //Session["WhatsappTest"] = RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString;
                    //Response.Write(RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString);
                }
            }

            catch (Exception ex)
            {

            }

        }
        public void DisabledAllCheckBox()
        {
            try
            {
                foreach (GridViewRow gvrow in grdCaseList.Rows)
                {
                    CheckBox chk2 = (CheckBox)gvrow.FindControl("chkParty");

                    chk2.Enabled = false;
                }
                CheckBox chk = (CheckBox)grdCaseList.HeaderRow.FindControl("chkAllSelect");
                chk.Enabled = false;
            }
            catch (Exception ex)
            {

            }

        }

        protected void grdCaseList_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Header)
                {
                    // This is header row
                    // You need to put null checks as per need
                    CheckBox cb = (CheckBox)e.Row.FindControl("chkAllSelect");
                    cb.Enabled = false;
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void Filldatatogrid()
        {
            try
            {
                DataSet Partysenddata = new DataSet();
                string Appid = Session["AppID"].ToString();
                Partysenddata = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);
                if (Partysenddata.Tables.Count > 0)
                {
                    if (Partysenddata.Tables[0].Rows.Count > 0)
                    {
                        GridView1.DataSource = Partysenddata.Tables[0].DefaultView;
                        GridView1.DataBind();
                    }
                    else
                    {
                        GridView1.DataSource = null;
                        GridView1.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {

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
        public void Checkedgrid()
        {
            int PartyID = 0;
            string Name;
            string SMS;
            string Email;
            String WhatsAPP;
            String FatherName;
            String Address;
            int PartyCount = 0;
            int SN = 0;
            string str = string.Empty;
            string strname = string.Empty;
            string Partyid = Session["Party_ID"].ToString();
            //Partyid = Partyid.Split(',')[2].ToString();
            //List<string> uniques = Partyid.Split(',').Reverse().Distinct().Take(2).Reverse().ToList();
            //string newStr = string.Join(",", uniques);
            string[] values = Partyid.Split(',');
            string Ram_party = string.Empty;

            DataTable dtSelectPar = (DataTable)ViewState["SelectParty"];
            DataTable dtParID = (DataTable)ViewState["PrtDeatils"];

            //ViewState["CopyDeatils"] = dtParID;


            //dtParID.Clear();
            //dtSelectPar.Clear();


            for (int i = 0; i < values.Length; i++)
            {
                Ram_party = "";
                values[i] = values[i].Trim();
                Ram_party = values[i].ToString();
                foreach (GridViewRow gvrow in grdCaseList.Rows)
                {
                    Label lblPartyId = (Label)gvrow.FindControl("lblPartyId");
                    CheckBox chk2 = (CheckBox)gvrow.FindControl("chkParty");
                    if (Ram_party == lblPartyId.Text)
                    {
                        chk2.Checked = true;
                        if (chk2 != null & chk2.Checked)
                        {

                            str += ++SN + ".";
                            str += "<b>Name : </b>" + gvrow.Cells[2].Text + ", ";
                            str += "<b>Father Name : </b>" + gvrow.Cells[3].Text + ", ";
                            str += "<b>Address : </b>" + gvrow.Cells[4].Text;

                            str += "<br />";
                            //PartyID = Convert.ToInt32(gvrow.Cells[1].Text);

                            PartyID = Convert.ToInt32(values[i]);

                            //ViewState["PartyIdD"] = Convert.ToInt32(gvrow.Cells[1].Text);
                            DataSet DSPartyDisplay = clsNoticeBAL.GetPartDetailsByID_Notice(PartyID);
                            ++PartyCount;
                            if (DSPartyDisplay.Tables.Count > 0)
                            {

                                Name = DSPartyDisplay.Tables[0].Rows[0]["Party_Name"].ToString();
                                SMS = DSPartyDisplay.Tables[0].Rows[0]["Mob_No"].ToString();
                                WhatsAPP = DSPartyDisplay.Tables[0].Rows[0]["Whatsapp_No"].ToString();
                                Email = DSPartyDisplay.Tables[0].Rows[0]["Email_Id"].ToString();
                                FatherName = DSPartyDisplay.Tables[0].Rows[0]["Father_Name_Hi"].ToString();
                                Address = DSPartyDisplay.Tables[0].Rows[0]["Party_AddressHI"].ToString();
                                dtParID.Rows.Add(0, 0, PartyID, Name, Email, SMS, WhatsAPP, "", DateTime.Now, 0, SMS, WhatsAPP);
                                dtSelectPar.Rows.Add(PartyID, Name, FatherName, Address);
                            }
                            ViewState["PrtDeatils"] = dtParID;
                            ViewState["SelPrt"] = dtSelectPar;

                        }
                        grdPartyDisplay.DataSource = dtParID;
                        grdPartyDisplay.DataBind();
                        grdSelectedParties.DataSource = dtSelectPar;
                        grdSelectedParties.DataBind();


                        //gridformessage.DataSource = dtSelectPar;
                        //gridformessage.DataBind();
                    }
                }
            }

        }

        protected void grdSRDoc_Sorted(object sender, GridViewSortEventArgs e)
        {

            try
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
            catch (Exception ex)
            {

            }
        }

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {

            //grdCaseList.Columns[7].ReadOnly = true;

            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

            SaveNoticePDF();
            DataTable dtUp = clsNoticeBAL.ChangeFinalSubmit_Notice(Convert.ToInt32(ViewState["AppID"].ToString()));
            pnlEsignDSC.Visible = true;
            pnlSaveDraft.Visible = false;



        }
    }


}