using eSigner;
using Newtonsoft.Json.Linq;
using RestSharp;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;
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
using RestSharp;

namespace CMS_Sampada.CoS
{
    public partial class Final_Order_Drafting : System.Web.UI.Page
    {
        private static string CoSPropertyValuation_url = ConfigurationManager.AppSettings["COSPropertyValuationURL"];
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];

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



        string Application_Id_eMudra = ConfigurationManager.AppSettings["ApplicationId_eMudra"];
        string Department_Id_eMudra = ConfigurationManager.AppSettings["DepartmentId_eMudra"];
        string Secretkey_eMudra = ConfigurationManager.AppSettings["Secretkey_eMudra"];
        string eSignURL_eMudra = ConfigurationManager.AppSettings["eSignURL_eMudra"];

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];
        string Java_Path = ConfigurationManager.AppSettings["JavaPath"];

        eSigner.eSigner _esigner = new eSigner.eSigner();
        CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        CoSOrderSheet_BAL clsOrdersheetBAL = new CoSOrderSheet_BAL();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        CoSFinalOrder_BAL clsFinalOrderBAL = new CoSFinalOrder_BAL();
        string All_OrderSheetFileNme = "";




        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        string FileName = string.Empty;
        public byte[] pdfBytes;

        string appid;
        string Appno;


        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }

        protected void Page_Load(object sender, EventArgs e)

        {

            try
            {

                if (!Page.IsPostBack)
                {
                    Session["RecentSheetPath"] = "";
                    lblHeadingDist.Text = Session["District_NameHI"].ToString();
                    //lblHeadingDist2.Text = Session["District_NameHI"].ToString();
                    //lblCOSOfficeNameHi.Text = Session["SRONameHI"].ToString();
                    //lblSROAddressHi.Text = Session["officeAddress"].ToString();
                    hdnCOSOfficeNameHi1.Value = Session["officeNameHi"].ToString();
                    //lblCOSOfficeNameHi2.Text = Session["officeAddress"].ToString();
                    //lblCOSAddress.Text = Session["officeAddress"].ToString();
                    lblDROfficeName.Text = Session["District_NameHI"].ToString();


                    int hearing_id = Convert.ToInt32(Session["hearing_id"].ToString());
                    if (Request.QueryString["Response_type"] != null)
                    {
                        if (Request.QueryString["Response_type"].ToString() == "Hearing_Ordersheet")
                        {
                            DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 1);

                        }
                        else if (Request.QueryString["Response_type"].ToString() == "HearingOrdersheetDSC")
                        {
                            DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 2);

                        }
                        else if (Request.QueryString["Response_type"].ToString() == "Final_Order_eSign")
                        {
                            if (Request.QueryString["Response_Status"].ToString() == "1")
                            { 
                                DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(Session["AppID"].ToString()), "1", Session["DRID"].ToString(), GetLocalIPAddress());
                            }
                            else if (Request.QueryString["Response_Status"].ToString() == "0")
                            {

                            }
                        }
                        else if (Request.QueryString["Response_type"].ToString() == "Final_Order_DSC")
                        {

                            DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(Session["AppID"].ToString()), "2", Session["DRID"].ToString(), GetLocalIPAddress());

                        }


                    }


                    if (Session["Case_Number"] != null)
                    {
                        ViewState["Case_Number"] = Session["Case_Number"].ToString();
                        ViewState["HearingDate"] = Session["HearingDate"].ToString();
                        if (Session["Status_Id"] != null)
                        {
                            Session["Status_Id"] = Session["Status_Id"];
                            ViewState["Status_Id"] = Session["Status_Id"];
                        }
                        if (Session["hearing_id_Final"] != null)
                        {
                            Session["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                            ViewState["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                        }
                        else
                        {
                            ViewState["Hearing_ID"] = Session["Hearing_ID"];
                        }
                        if (Session["AppID"] != null)
                        {
                            ViewState["AppID"] = Session["AppID"];
                        }
                    }

                    hdTocan.Value = Session["Token"].ToString();


                    SetRecentDocPath();

                    if (Request.QueryString["Flag"].ToString() != null)
                    {
                        int Flag = Convert.ToInt32(Request.QueryString["Flag"].ToString());
                        //int Flag = 4;

                        if (Flag == 1)     // Pending for final order
                        {

                            {
                                ViewState["Case_Number"] = "";
                                ViewState["HearingDate"] = "";

                                if (Session["Case_Number"] != null)
                                {
                                    ViewState["Case_Number"] = Session["Case_Number"].ToString();
                                    ViewState["HearingDate"] = Session["HearingDate"].ToString();
                                    if (Session["Status_Id"] != null)
                                    {
                                        Session["Status_Id"] = Session["Status_Id"];
                                        ViewState["Status_Id"] = Session["Status_Id"];
                                    }
                                    if (Session["hearing_id_Final"] != null)
                                    {
                                        Session["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                        ViewState["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                    }

                                    if (Session["Notice_ID"] != null)
                                    {
                                        Session["Notice_Id"] = Session["Notice_ID"].ToString();
                                        ViewState["Notice_Id"] = Session["Notice_ID"].ToString();
                                    }
                                    //Session["HearingDate"] = Session["HearingDate"].ToString(); 00
                                    DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"].ToString()));
                                    CreateAddCopyTable();
                                    string casenumber = ViewState["Case_Number"].ToString();
                                    //ViewState["PartyDetail"] = dt;
                                    if (dt.Rows.Count > 0)
                                    {

                                        lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                                        lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                                        lblCaseNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
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
                                            lblHearingDt.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                                        }
                                        lblHearingDt.Text = HearingDate;
                                        string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                                        string AppNum = lblProposalIdHeading.Text;

                                        Session["AppID"] = dt.Rows[0]["App_ID"].ToString(); ;
                                        Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                                        ViewState["AppID"] = Session["AppID"];



                                        int App_id = Convert.ToInt32(Session["AppID"].ToString());
                                        string appid = Session["AppID"].ToString();
                                        lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                        string Appno = Session["Appno"].ToString();
                                        //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                                        lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                        //summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + lblHearingDt.Text + " को आई.एस.बी.टी.परिसर, मेजनाईन फ्लोर, हबीबगंज भोपाल में 12.00 बजे पूर्वान्ह में की जावेगी।";
                                        //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";

                                        GetPreviousProcedding();
                                        //string Hearing_Dt = (ViewState["HearingDate"].ToString());





                                        DataTable de = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                        if (de.Rows.Count > 0)
                                        {

                                            string fileName = de.Rows[0]["FINAL_PROCEEDING_PDF"].ToString();
                                            Session["RecentSheetPath"] = fileName;
                                            SetRecentDocPath();

                                        }




                                        Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                        All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                                        CreateEmptyFile(All_OrderSheetFileNme);


                                        string All_DocFile_Hearing;
                                        string Proposal_ID = Session["Appno"].ToString();




                                        string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";

                                        PreviousProceeding_List(Convert.ToInt32(appid));

                                        Session["All_DocSheet"] = FileNme;
                                        //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                        All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                                        //CreateEmptyFile(All_DocFile_Hearing);
                                        CraetSourceFile(Convert.ToInt32(appid));
                                        AllDocList(Convert.ToInt32(appid));
                                        DataTable dPtartyDetails = new DataTable();
                                        string App_Id = Session["AppID"].ToString();
                                        int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                        DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                        //string date = DateTime.Now.ToString();
                                        DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                                        //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                        dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, App_Id);
                                        //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());
                                        if (dPtartyDetails.Rows.Count > 0)
                                        {
                                            grdNoticeParty.DataSource = dPtartyDetails;
                                            grdNoticeParty.DataBind();
                                            ViewState["SelPrt"] = dPtartyDetails;


                                        }

                                    }


                                    GuideLineValuePenalityCalculation();
                                    StampDutyPenalityCalculation();
                                    RegistyPenalityCalculation();

                                    if (lblTStampdeficit.Text != "")
                                    {
                                        double NetStampDutyDeficit = Convert.ToDouble(lblTStampdeficit.Text);
                                        double NetRegFeeDeficit = Convert.ToDouble(lblNetDeficitReg.Text);

                                        double TotalAmpountDeficit = NetStampDutyDeficit + NetRegFeeDeficit;
                                        lblTAmtDeficit.Text = TotalAmpountDeficit.ToString();

                                    }

                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                }
                                else
                                {
                                    ViewState["Case_Number"] = "000002/B104/32/2022-23";
                                }


                                if (Request.QueryString["Response_type"] != null)
                                {
                                    if (Request.QueryString["Response_type"].ToString() == "Final_Order")
                                    {


                                    }
                                }


                            }

                        }
                        else
                        {

                            if (Flag == 3)
                            {
                                if (Session["Case_Number"] != null)
                                {
                                    if (Session["Case_Number"] != null)
                                    {
                                        ViewState["Case_Number"] = Session["Case_Number"].ToString();
                                        ViewState["HearingDate"] = Session["HearingDate"].ToString();

                                        //ViewState["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //Session["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //ViewState["Status_Id"] = Request.QueryString["Status_Id"].ToString();
                                        if (Session["hearing_id_Final"] != null)
                                        {
                                            Session["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                            ViewState["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                        }
                                        if (Session["Status_Id"] != null)
                                        {
                                            Session["Status_Id"] = Session["Status_Id"];
                                            ViewState["Status_Id"] = Session["Status_Id"];
                                        }
                                        string Hearing_Id = Session["hearing_id"].ToString();
                                        if (Session["Notice_ID"] != null)
                                        {
                                            Session["Notice_Id"] = Session["Notice_ID"].ToString();
                                            ViewState["Notice_Id"] = Session["Notice_ID"].ToString();
                                        }
                                        //Session["HearingDate"] = Session["HearingDate"].ToString(); 
                                        DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"].ToString()));
                                        CreateAddCopyTable();
                                        string casenumber = ViewState["Case_Number"].ToString();
                                        //ViewState["PartyDetail"] = dt;
                                        if (dt.Rows.Count > 0)
                                        {

                                            lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                                            lblCaseNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
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
                                                lblHearingDt.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                                            }
                                            lblHearingDt.Text = HearingDate;
                                            string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                                            string AppNum = lblProposalIdHeading.Text;

                                            Session["AppID"] = dt.Rows[0]["App_ID"].ToString();
                                            Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            ViewState["AppID"] = Session["AppID"];

                                            int App_id = Convert.ToInt32(Session["AppID"].ToString());
                                            string appid = Session["AppID"].ToString();
                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                            string Appno = Session["Appno"].ToString();
                                            //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                            //summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + lblHearingDt.Text + " को आई.एस.बी.टी.परिसर, मेजनाईन फ्लोर, हबीबगंज भोपाल में 12.00 बजे पूर्वान्ह में की जावेगी।";
                                            //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";

                                            GetPreviousProcedding();
                                            //string Hearing_Dt = (ViewState["HearingDate"].ToString());



                                            //DataSet dsDocRecent = new DataSet();
                                            //dsDocRecent = clsNoticeBAL.GetProposal_Doc_Notice(ViewState["Case_Number"].ToString(), appid);
                                            //if (dsDocRecent != null)
                                            //{
                                            //    if (dsDocRecent.Tables.Count > 0)
                                            //    {

                                            //        if (dsDocRecent.Tables[0].Rows.Count > 0)
                                            //        {
                                            //            string fileName = dsDocRecent.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                            //            //Session["RecentSheetPath"] = fileName.ToString();

                                            //            //grdTOCOrder.DataSource = dsDocRecent;
                                            //            //grdTOCOrder.DataBind();

                                            //        }
                                            //    }
                                            //}

                                            //DataSet dsDocDetails = new DataSet();
                                            //dsDocDetails = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), appid);
                                            //if (dsDocDetails != null)
                                            //{
                                            //    if (dsDocDetails.Tables.Count > 0)
                                            //    {

                                            //        if (dsDocDetails.Tables[0].Rows.Count > 0)
                                            //        {
                                            //            //grdProposalDoc.DataSource = dsDocDetails;
                                            //            //grdProposalDoc.DataBind();


                                            //        }
                                            //    }
                                            //}

                                            //dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS_ToC(Convert.ToInt32(appid), Appno);
                                            //if (dsDocDetails != null)
                                            //{
                                            //    if (dsDocDetails.Tables.Count > 0)
                                            //    {

                                            //        if (dsDocDetails.Tables[0].Rows.Count > 0)
                                            //        {
                                            //            //grdSRDoc.DataSource = dsDocDetails;
                                            //            //grdSRDoc.DataBind();

                                            //        }

                                            //    }
                                            //}

                                            //DataSet dsOrderSheet = new DataSet();
                                            //dsDocDetails = OrderSheet_BAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), appid);
                                            //if (dsDocDetails != null)
                                            //{
                                            //    if (dsDocDetails.Tables.Count > 0)
                                            //    {

                                            //        if (dsDocDetails.Tables[0].Rows.Count > 0)
                                            //        {
                                            //            //grdProposalDoc.DataSource = dsDocDetails;
                                            //            //grdProposalDoc.DataBind();


                                            //        }
                                            //    }
                                            //}

                                            //DataSet dsDocNotice;
                                            //dsDocNotice = clsNoticeBAL.TOC_Doc_Notice(Convert.ToInt32(appid));
                                            //if (dsDocNotice != null)
                                            //{
                                            //    if (dsDocNotice.Tables.Count > 0)
                                            //    {

                                            //        if (dsDocNotice.Tables[0].Rows.Count > 0)
                                            //        {
                                            //            //string fileName = dsDocNotice.Tables[0].Rows[0]["PROPOSALPATH_FIRSTFORMATE"].ToString();
                                            //            //Session["Recent"] = fileName.ToString();
                                            //            //grdNoticeDoc.DataSource = dsDocNotice;
                                            //            //grdNoticeDoc.DataBind();
                                            //            string fileName = dsDocNotice.Tables[0].Rows[0]["SIGNED_NOTICE_PATH"].ToString();
                                            //            //ifRecent.Src = fileName;


                                            //        }
                                            //    }
                                            //}

                                            DataTable det = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                            if (det.Rows.Count > 0)
                                            {

                                                string fileName = det.Rows[0]["FINAL_PROCEEDING_PDF"].ToString();
                                                Session["RecentSheetPath"] = fileName;
                                                SetRecentDocPath();

                                            }
                                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                                            CreateEmptyFile(All_OrderSheetFileNme);


                                            string All_DocFile_Hearing;
                                            string Proposal_ID = Session["Appno"].ToString();




                                            string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";

                                            PreviousProceeding_List(Convert.ToInt32(appid));

                                            Session["All_DocSheet"] = FileNme;
                                            //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                                            //CreateEmptyFile(All_DocFile_Hearing);
                                            CraetSourceFile(Convert.ToInt32(appid));
                                            AllDocList(Convert.ToInt32(appid));
                                            DataTable dPtartyDetails = new DataTable();
                                            string App_Id = Session["AppID"].ToString();
                                            int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                            DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, App_Id);
                                            //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                            //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            //All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                                            //CreateEmptyFile(All_OrderSheetFileNme);


                                            //string All_DocFile_Hearing;
                                            //string Proposal_ID = Session["Appno"].ToString();




                                            //string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";
                                            //PreviousProceeding_List(Convert.ToInt32(appid));


                                            //Session["All_DocSheet"] = FileNme;
                                            ////Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            //All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                                            //CreateEmptyFile(All_DocFile_Hearing);
                                            //CraetSourceFile(Convert.ToInt32(appid));
                                            //AllDocList(Convert.ToInt32(appid));
                                            //DataTable dPtartyDetails = new DataTable();
                                            //string App_Id = Session["AppID"].ToString();
                                            //int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                            //DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            ////string date = DateTime.Now.ToString();
                                            //DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                                            ////DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            //dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, App_Id);
                                            //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());
                                            if (dPtartyDetails.Rows.Count > 0)
                                            {
                                                grdNoticeParty.DataSource = dPtartyDetails;
                                                grdNoticeParty.DataBind();
                                                ViewState["SelPrt"] = dPtartyDetails;


                                            }

                                            DataTable dsDisplayPratilipi = new DataTable();
                                            dsDisplayPratilipi = clsFinalOrderBAL.GetAddCopyDeatils_FinalOrder(Convert.ToInt32(App_Id), Convert.ToInt32(Hearing_Id));
                                            if (dsDisplayPratilipi != null)
                                            {
                                                if (dsDisplayPratilipi.Rows.Count > 0)
                                                {



                                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                                    GrdAddCopy_Details.DataBind();
                                                    ViewState["AddCopyDetails"] = dsDisplayPratilipi;
                                                    PnlPratilipi.Visible = true;
                                                }
                                            }

                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["SelPrt"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["AddCopyDetails"];
                                            DataTable dtCopyShow = (DataTable)ViewState["SelPrt"];


                                            if (dtApp != null)
                                                if (dtApp.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtApp.Rows.Count; i++)
                                                    {
                                                        Copy_Name = dtApp.Rows[i]["NAME"].ToString();
                                                        Copy_SMS = dtApp.Rows[i]["PHONE_NO"].ToString();
                                                        Copy_WhatsAPP = dtApp.Rows[i]["whatsapp_no"].ToString();
                                                        Copy_Email = dtApp.Rows[0]["EMAIL"].ToString();
                                                        //dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                        dtCopyShow.Rows.Add("0", Copy_Name, "", "", Copy_SMS, Copy_SMS, Copy_Email, "", 0);
                                                    }


                                                }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();
                                            //ViewState["CopyDeatils"] = dtCopyShow;  /// To resolve pratilipi issue
                                            pnlSendOrder.Visible = false;
                                            btnDraft.Visible = true;

                                        }




                                        GuideLineValuePenalityCalculation();
                                        //StampDutyPenalityCalculation();
                                        RegistyPenalityCalculation();
                                        pnlOption.Visible = true;
                                        DataTable dtt = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                        if (dtt.Rows.Count > 0)
                                        {
                                            //lblSRProposal.Text = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            summernote.InnerText = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            //lblPartyReply.Text = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            txtSRProposal.InnerText = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            //lblNishkarsh.Text = dtt.Rows[0]["COS_DECISION"].ToString();
                                            txtCOSDecision.InnerText = dtt.Rows[0]["COS_DECISION"].ToString();
                                            //lblFinalRemark.Text = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            txtFinalDecision.InnerText = dtt.Rows[0]["FINAL_REMARK"].ToString();

                                            lblSRProposal.Text = summernote.Value;
                                            lblPartyReply.Text = txtSRProposal.Value;
                                            lblNishkarsh.Text = txtCOSDecision.Value;
                                            lblFinalRemark.Text = txtFinalDecision.Value;

                                            lblSRProposal.Visible = false;
                                            lblPartyReply.Visible = false;
                                            lblNishkarsh.Visible = false;
                                            lblFinalRemark.Visible = false;

                                        }

                                        DataTable ddt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));
                                        if (ddt.Rows.Count > 0)
                                        {
                                            lblTStampdeficit2.Text = ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString();
                                            double DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY = Convert.ToDouble(ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString());
                                            lblTstampPenality.Text = ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString();
                                            double TOTAL_PENALTY_AMOUNT = Convert.ToDouble(ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString());

                                            double TotalDefePenality = DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY + TOTAL_PENALTY_AMOUNT;
                                            lblTotalDefePenality.Text = TotalDefePenality.ToString();

                                            ViewState["FileNameUnSignedPDF"] = ddt.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();
                                            ViewState["FinalOrderUnSignedPDF"] = ddt.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();
                                            ViewState["FinalOrderSignedPDF"] = ddt.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                            //ifRecent.Visible = false;
                                            //sendParties.Visible = true;

                                            //docPath.Visible = true;
                                            //docPath.Src = ViewState["FileNameUnSignedPDF"].ToString();
                                            lblTStampdeficit2.Visible = true;
                                            lblTstampPenality.Visible = true;
                                            lblTotalDefePenality.Visible = true;




                                            if (ddt.Rows[0]["TOTAL_AMOUNT_BY_SR"].ToString() != "0")
                                            {
                                                lblToralStamp.Text = ddt.Rows[0]["TOTAL_AMOUNT_BY_SR"].ToString();

                                                DataTable Dttt = new DataTable();
                                                Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (Dttt.Rows.Count > 0)
                                                {

                                                    lblPratifal.Text = Dttt.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                                    lblPStampCOS.Text = Dttt.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                                                    lblStampMuniciple.Text = Dttt.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                                    lblJanpadD.Text = Dttt.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                                    lblupkar.Text = Dttt.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                                    lblToralStamp.Text = Dttt.Rows[0]["Proposed_StampDuty"].ToString();
                                                    lblRegFee.Text = Dttt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                    lblTotalAmt.Text = Dttt.Rows[0]["Total_BY_SRorPO"].ToString();
                                                    lblNetRegSR.Text = Dttt.Rows[0]["ProposedRecoverableRegFee"].ToString();

                                                    lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                                                    lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();
                                                    lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();

                                                    decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                                                    //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                                                    lblNetRegDeficit.Text = NetRegDeficit.ToString();
                                                    lblTotalRegfee.Text = NetRegDeficit.ToString();
                                                    lblTotalPayable.Text = "0";
                                                    double TotalPenalityWithStamp = Convert.ToDouble(lblTotalDefePenality.Text);
                                                    double TotalNetRegAmount = Convert.ToDouble(lblTotalRegfee.Text);
                                                    double TotalPayableAmount = TotalPenalityWithStamp + TotalNetRegAmount;
                                                    lblTotalPayable.Text = TotalPayableAmount.ToString();
                                                    lblNetRegFee.Text = lblNetRegSR.Text;
                                                    //decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                                                    ////decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                                                    //lblNetRegDeficit.Text = NetRegDeficit.ToString();
                                                    lblNetRegDeficit.Visible = true;

                                                    lblEx_Order.Text = lblMStampPro2.Text;
                                                    lblRegExem.Text = lblRegExemSR.Text;
                                                    rdbtnReportNo.Checked = true;
                                                    RadioButton2.Checked = true;
                                                    pnlCalNo.Visible = true;
                                                }

                                            }


                                            else
                                            {

                                                if (ddt.Rows[0]["TOTAL_AMOUNT_BY_PARTY"].ToString() != "0")
                                                {
                                                    lblToralStamp.Text = ddt.Rows[0]["TOTAL_AMOUNT_BY_PARTY"].ToString();
                                                    DataTable Dttt = new DataTable();
                                                    Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                    if (Dttt.Rows.Count > 0)
                                                    {

                                                        lblPratifal.Text = lblConValue1.Text;
                                                        lblPStampCOS.Text = lblPrinStamDoc2.Text;
                                                        lblStampMuniciple.Text = lblMStamp2.Text;
                                                        lblJanpadD.Text = lblJanpad2.Text;
                                                        lblupkar.Text = lblUpkarDoc2.Text;
                                                        lblToralStamp.Text = lblTStamDoc2.Text;
                                                        lblRegFee.Text = lblTRegDoc2.Text;
                                                        lblNetRegSR.Text = Dttt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                        lblTotalAmt.Text = Dttt.Rows[0]["Total_BY_SRorPO"].ToString();
                                                        lblNetRegFee.Text = lblNetRegParty.Text;
                                                        decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                                                        //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                                                        lblNetRegDeficit.Text = NetRegDeficit.ToString();
                                                        lblTotalPayable.Text = "0.0";
                                                        lblEx_Order.Text = lblEx_Party.Text;
                                                        lblRegExem.Text = lblRegExemParty.Text;
                                                        lblToralStamp.Text = lblTStamDoc2.Text;

                                                        lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                                        lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                        lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();

                                                        rdbtnReportNo.Checked = true;
                                                        RadioButton3.Checked = true;
                                                        pnlCalNo.Visible = true;
                                                    }
                                                }

                                            }




                                            if (Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString()) != 0.0)
                                            {

                                                double TOTALSTAMP_GUIDEVALUE = 0;
                                                TOTALSTAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblCOSGidVale.Visible = true;
                                                lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblPratifal.Visible = true;
                                                lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                lblPStampCOS.Visible = true;
                                                lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                lblStampMuniciple.Visible = true;
                                                lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                lblJanpadD.Visible = true;
                                                lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                lblupkar.Visible = true;
                                                lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                //lblNetRegFee.Visible = true;
                                                //lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                Double STAMP_GUIDEVALUE = 0;
                                                STAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                Double TOTALSTAMP_CONSIAMT = 0;
                                                TOTALSTAMP_CONSIAMT = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                Double TOTALSTAMP_PRINCIPLE = 0;
                                                TOTALSTAMP_PRINCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                Double TOTALSTAMP_MUNCIPLE = 0;
                                                TOTALSTAMP_MUNCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                Double TOTALSTAMP_JANPASD = 0;
                                                TOTALSTAMP_JANPASD = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                Double TOTALSTAMP_UPKAR = 0;
                                                TOTALSTAMP_UPKAR = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                Double TotalCOSCount = STAMP_GUIDEVALUE + TOTALSTAMP_CONSIAMT + TOTALSTAMP_PRINCIPLE + TOTALSTAMP_MUNCIPLE + TOTALSTAMP_JANPASD + TOTALSTAMP_UPKAR;
                                                //lblToralStamp.Text = TotalCOSCount.ToString();
                                                DataTable Dttt = new DataTable();
                                                Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (Dttt.Rows.Count > 0)
                                                {

                                                    lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                                    lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                    lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                                }


                                            }

                                            else
                                            {
                                                DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (dtBasicInfo.Rows.Count > 0)
                                                {
                                                    DataTable ddtt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));
                                                    if (ddtt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString() != "0")
                                                    {

                                                        lblCOSGidVale.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                                                        //(lblGuideDefict1.Text) = (dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        //Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        lblPratifal.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                                        lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                                                        lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                                        lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                                        lblupkar.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                                        lblToralStamp.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                        //lblNetRegFee.Text = ddtt.Rows[0]["NET_REGFEES_COS"].ToString();
                                                    }

                                                    else
                                                    {

                                                        //lblPratifal.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                                        //lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();

                                                        //lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
                                                        //lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
                                                        //lblupkar.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
                                                        //lblToralStamp.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();

                                                        lblPratifal.Text = "0";
                                                        lblPStampCOS.Text = "0";

                                                        lblStampMuniciple.Text = "0";
                                                        lblJanpadD.Text = "0";
                                                        lblupkar.Text = "0";
                                                        lblToralStamp.Text = "0";
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                                                        lblTotalAmt.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();

                                                    }



                                                }


                                            }


                                        }

                                        Get_TotalStampDuty();

                                        lblToralStamp.Visible = true;
                                        pnlEsignDSC.Visible = false;
                                        pnlSendOrder.Visible = false;
                                        btnFinalSubmit.Visible = false;
                                        btnDraft.Visible = true;
                                        pnlOption.Visible = true;

                                        if (Convert.ToDouble(ddt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString()) == 3)
                                        {

                                            double TOTALSTAMP_GUIDEVALUE = 0;
                                            TOTALSTAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                            lblCOSGidVale.Visible = true;
                                            lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                            lblPratifal.Visible = true;
                                            lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                            lblPStampCOS.Visible = true;
                                            lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                            lblStampMuniciple.Visible = true;
                                            lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                            lblJanpadD.Visible = true;
                                            lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                            lblupkar.Visible = true;
                                            lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                            lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                            lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                            lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                            lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                            lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());


                                            lblEx_Order.Visible = true;
                                            lblToralStamp.Visible = true;
                                            lblRegFee.Visible = true;
                                            lblRegExem.Visible = true;
                                            lblNetRegFee.Visible = true;

                                            Double STAMP_GUIDEVALUE = 0;
                                            STAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                            Double TOTALSTAMP_CONSIAMT = 0;
                                            TOTALSTAMP_CONSIAMT = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                            Double TOTALSTAMP_PRINCIPLE = 0;
                                            TOTALSTAMP_PRINCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                            Double TOTALSTAMP_MUNCIPLE = 0;
                                            TOTALSTAMP_MUNCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                            Double TOTALSTAMP_JANPASD = 0;
                                            TOTALSTAMP_JANPASD = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                            Double TOTALSTAMP_UPKAR = 0;
                                            TOTALSTAMP_UPKAR = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                            Double TotalCOSCount = TOTALSTAMP_PRINCIPLE + TOTALSTAMP_MUNCIPLE + TOTALSTAMP_JANPASD + TOTALSTAMP_UPKAR;
                                            //lblToralStamp.Text = TotalCOSCount.ToString();

                                            DataTable Dttt = new DataTable();
                                            Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                            if (Dttt.Rows.Count > 0)
                                            {

                                                lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                                lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                            }

                                            lblRegFee.Text = lblTRegDoc2.Text;
                                            lblRegExem.Text = lblRegExemParty.Text;
                                            lblNetRegFee.Text = lblNetRegParty.Text;
                                            lblTotalAmt.Text = lblTAmtParty2.Text;
                                            lblToralStamp.Text = lblTStamDoc2.Text;
                                            //lblTStampdeficit2.Text = "0.0";
                                            //lblTstampPenality.Text = "0.0";
                                            //lblTotalDefePenality.Text = "0.0";
                                            //lblNetRegDeficit.Text = "0.0";
                                            //lblTotalRegfee.Text = "0.0";
                                            //lblTotalPayable.Text = "0.0";
                                            rdbtnReportNo.Checked = true;
                                            RadioButton3.Checked = true;
                                            pnlCalNo.Visible = true;
                                            pnlChange.Visible = false;
                                            pnlOption.Visible = true;
                                        }
                                        if (Convert.ToDouble(ddt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString()) == 1) //SR calculation
                                        {
                                            decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                                            //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                                            lblNetRegDeficit.Text = NetRegDeficit.ToString();

                                        }

                                        if (Convert.ToDouble(ddt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString()) == 2)
                                        {
                                            DataTable Dttt = new DataTable();
                                            Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                            if (Dttt.Rows.Count > 0)
                                            {

                                                lblPratifal.Text = lblConValue1.Text;
                                                lblPStampCOS.Text = lblPrinStamDoc2.Text;
                                                lblStampMuniciple.Text = lblMStamp2.Text;
                                                lblJanpadD.Text = lblJanpad2.Text;
                                                lblupkar.Text = lblUpkarDoc2.Text;
                                                lblToralStamp.Text = lblTStamDoc2.Text;
                                                lblRegFee.Text = lblTRegDoc2.Text;
                                                lblNetRegSR.Text = Dttt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                lblTotalAmt.Text = Dttt.Rows[0]["Total_BY_SRorPO"].ToString();
                                                lblNetRegFee.Text = lblNetRegParty.Text;

                                                lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                                lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();



                                                decimal NetRegDeficit = Convert.ToDecimal(lblNetPartyReg.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                                                //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                                                lblNetRegDeficit.Text = NetRegDeficit.ToString();
                                                lblTotalPayable.Text = "0.0";
                                                lblEx_Order.Text = lblEx_Party.Text;
                                                lblRegExem.Text = lblRegExemParty.Text;
                                                lblToralStamp.Text = lblTStamDoc2.Text;
                                                rdbtnReportNo.Checked = true;
                                                RadioButton3.Checked = true;
                                                pnlCalNo.Visible = true;
                                            }
                                        }


                                        if (ViewState["Status_Id"].ToString() == "43")
                                        {
                                            pnlAddCopy.Visible = false;
                                            btnDraft.Visible = false;
                                            btnFinalSubmit.Visible = false;
                                            pnlEsignDSC.Visible = true;
                                            pnlSendOrder.Visible = false;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;
                                            //sendParties.Visible = true;

                                            // docPath.Visible = true;
                                            // docPath.Src = ViewState["FinalOrderUnSignedPDF"].ToString();

                                        }
                                        if (ViewState["Status_Id"].ToString() == "66" || ViewState["Status_Id"].ToString() == "67")
                                        {
                                            pnlAddCopy.Visible = false;
                                            btnDraft.Visible = false;
                                            btnFinalSubmit.Visible = false;
                                            pnlEsignDSC.Visible = false;
                                            pnlSendOrder.Visible = true;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;


                                            //docPath.Visible = true;
                                            //docPath.Src = ViewState["FinalOrderSignedPDF"].ToString();

                                        }


                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                    }
                                }

                            }

                            if (Flag == 4)
                            {
                                if (Session["Case_Number"] != null)
                                {
                                    if (Session["Case_Number"] != null)
                                    {
                                        ViewState["Case_Number"] = Session["Case_Number"].ToString();
                                        ViewState["HearingDate"] = Session["HearingDate"].ToString();

                                        //ViewState["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //Session["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //ViewState["Status_Id"] = Request.QueryString["Status_Id"].ToString();
                                        if (Session["hearing_id_Final"] != null)
                                        {
                                            Session["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                            ViewState["Hearing_ID"] = Session["hearing_id_Final"].ToString();

                                        }
                                        else if (Session["Hearing_ID"] != null)
                                        {
                                            ViewState["Hearing_ID"] = Session["Hearing_ID"];
                                        }
                                        if (Session["Status_Id"] != null)
                                        {
                                            Session["Status_Id"] = Session["Status_Id"];
                                            ViewState["Status_Id"] = Session["Status_Id"];
                                        }
                                        string Hearing_Id = Session["hearing_id"].ToString();
                                        if (Session["Notice_ID"] != null)
                                        {
                                            Session["Notice_Id"] = Session["Notice_ID"].ToString();
                                            ViewState["Notice_Id"] = Session["Notice_ID"].ToString();
                                        }

                                        string appid = "";
                                        if (Session["AppID"] != null)
                                        {
                                            appid = Session["AppID"].ToString();
                                        }
                                        //Session["HearingDate"] = Session["HearingDate"].ToString(); 
                                        DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"].ToString()));
                                        CreateAddCopyTable();
                                        string casenumber = ViewState["Case_Number"].ToString();
                                        //ViewState["PartyDetail"] = dt;
                                        if (dt.Rows.Count > 0)
                                        {

                                            lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                                            lblCaseNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
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
                                                lblHearingDt.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                                            }
                                            lblHearingDt.Text = HearingDate;
                                            string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                                            string AppNum = lblProposalIdHeading.Text;

                                            Session["AppID"] = dt.Rows[0]["App_ID"].ToString();
                                            Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            ViewState["AppID"] = Session["AppID"];

                                            int App_id = Convert.ToInt32(Session["AppID"].ToString());

                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                            string Appno = Session["Appno"].ToString();
                                            //summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                            //summernote.Value = "1. कृपया यह सूचना ले कि लिखत दिनांक 05/12/2022 के अन्तर्गत आने वाली सम्पत्तियों पर मुद्रांक शुल्क, बाजार मूल्य एवं उपरोक्त लिखत पर देय शुल्क का अवधारण करने के लिए भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अधीन मेरे समक्ष स्टांपित किये जाने हेतु प्रस्तुत किया है। इस संबंध निर्देशित किया जाता है कि आप लिखत की मूल प्रति एवं अन्य जो भी दस्तावेज है न्यायालय में उपस्थित होकर प्रस्तुत करें। <br><br>  2. आपसे एतद द्वारा यह अपेक्षा की जाती है कि आप यह दर्शाने के लिए कि लिखत में उल्लेखित मुद्रांक शुल्क सत्यता पूर्वक उपवर्णित किया गया है, कि लिखत में अपनी आपत्तियां तथा अभ्यावेदन यदि कोई हो सुसंगत दस्तावेज के साथ यदि कोई हो सुनवाई की तारीख को अधोहस्ताक्षरी के समक्ष मूल दस्तावेज प्रस्तुत करें और यह भी उपदर्षित करे कि क्या आप कोई मौखिक साक्ष्य देने वाञ्छा करते हैं तथा सुनवाई के समय उपस्थित होंवे।  <br><br> 3. प्रश्नाधीन लिखत असम्यक रूप से स्टाम्पित माना गया है क्यों न स्टाम्प अधिनियम की धारा 40(ख) में कमी स्टाम्प ड्यूटी स्टाम्प शुल्क की कमी वाले भाग के लिए प्रतिमाह अथवा उसके भाग के लिए लिखत के निष्पादन की दिनांक से 2 प्रतिशत के बराबर शास्ति अधिरोपित की जाये।  <br><br> 4. यदि आप अधोहस्ताक्षरी के समक्ष उपसंजात होने या उपदर्शित करने के कि क्या आप कोई मौखिक या दस्तावेजी साक्ष्य जो आवश्यक हैं देने की वाञ्छा करते हैं या सुसंगत दस्तावेज प्रस्तुत करने के इस अवसर का लाभ उठाने में चूक करते हैं, तो आगे कोई और अवसर प्रदान नहीं किया जायेगा और उपलब्ध तथ्यों के आधार पर निपटारा कर दिया जायेगा।  <br><br> 5. प्रकरण में मुद्रांक देय शुल्क के अवधारण से सम्बन्धित मामले की सुनवाई तारीख " + lblHearingDt.Text + " को आई.एस.बी.टी.परिसर, मेजनाईन फ्लोर, हबीबगंज भोपाल में 12.00 बजे पूर्वान्ह में की जावेगी।";
                                            //docPath.Src = "../CMS-Sampada/RRCAllNoticeSheetDoc/15_All_RRCNoticeSheet.pdf";

                                            GetPreviousProcedding();
                                            //string Hearing_Dt = (ViewState["HearingDate"].ToString());



                                            DataTable de = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                            if (de.Rows.Count > 0)
                                            {

                                                string fileName = de.Rows[0]["FINAL_PROCEEDING_PDF"].ToString();
                                                Session["RecentSheetPath"] = fileName;
                                                SetRecentDocPath();

                                            }



                                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                                            CreateEmptyFile(All_OrderSheetFileNme);


                                            string All_DocFile_Hearing;
                                            string Proposal_ID = Session["Appno"].ToString();




                                            string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";

                                            PreviousProceeding_List(Convert.ToInt32(appid));

                                            Session["All_DocSheet"] = FileNme;
                                            //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                                            //CreateEmptyFile(All_DocFile_Hearing);
                                            CraetSourceFile(Convert.ToInt32(appid));
                                            AllDocList(Convert.ToInt32(appid));
                                            DataTable dPtartyDetails = new DataTable();
                                            string App_Id = Session["AppID"].ToString();
                                            int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                            DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, App_Id);
                                            //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());
                                            if (dPtartyDetails.Rows.Count > 0)
                                            {
                                                grdNoticeParty.DataSource = dPtartyDetails;
                                                grdNoticeParty.DataBind();
                                                ViewState["SelPrt"] = dPtartyDetails;


                                            }

                                            DataTable dsDisplayPratilipi = new DataTable();
                                            dsDisplayPratilipi = clsFinalOrderBAL.GetAddCopyDeatils_FinalOrder(Convert.ToInt32(App_Id), Convert.ToInt32(Hearing_Id));
                                            if (dsDisplayPratilipi != null)
                                            {
                                                if (dsDisplayPratilipi.Rows.Count > 0)
                                                {



                                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                                    GrdAddCopy_Details.DataBind();
                                                    ViewState["AddCopyDetails"] = dsDisplayPratilipi;
                                                    PnlPratilipi.Visible = true;
                                                }
                                            }

                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["SelPrt"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["AddCopyDetails"];
                                            DataTable dtCopyShow = (DataTable)ViewState["SelPrt"];


                                            if (dtApp != null)
                                                if (dtApp.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtApp.Rows.Count; i++)
                                                    {
                                                        Copy_Name = dtApp.Rows[i]["NAME"].ToString();
                                                        Copy_SMS = dtApp.Rows[i]["PHONE_NO"].ToString();
                                                        Copy_WhatsAPP = dtApp.Rows[i]["whatsapp_no"].ToString();
                                                        Copy_Email = dtApp.Rows[0]["EMAIL"].ToString();
                                                        //dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                        dtCopyShow.Rows.Add("0", Copy_Name, "", "", Copy_SMS, Copy_SMS, Copy_Email, "", 0);

                                                    }


                                                }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ///ViewState["CopyDeatils"] = dtCopyShow;
                                            pnlSendOrder.Visible = false;
                                            btnDraft.Visible = true;

                                        }




                                        GuideLineValuePenalityCalculation();
                                        //StampDutyPenalityCalculation();
                                        RegistyPenalityCalculation();
                                        pnlOption.Visible = true;
                                        DataTable dtt = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                        if (dtt.Rows.Count > 0)
                                        {
                                            //lblSRProposal.Text = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            //lblPartyReply.Text = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            //lblNishkarsh.Text = dtt.Rows[0]["COS_DECISION"].ToString();
                                            //lblFinalRemark.Text = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            //lblSRProposal.Text = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            summernote.InnerText = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            //lblPartyReply.Text = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            txtSRProposal.InnerText = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            //lblNishkarsh.Text = dtt.Rows[0]["COS_DECISION"].ToString();
                                            txtCOSDecision.InnerText = dtt.Rows[0]["COS_DECISION"].ToString();
                                            //lblFinalRemark.Text = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            txtFinalDecision.InnerText = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            lblSRProposal.Text = summernote.Value;
                                            lblPartyReply.Text = txtSRProposal.Value;
                                            lblNishkarsh.Text = txtCOSDecision.Value;
                                            lblFinalRemark.Text = txtFinalDecision.Value;
                                            lblSRProposal.Visible = false;
                                            lblPartyReply.Visible = false;
                                            lblNishkarsh.Visible = false;
                                            lblFinalRemark.Visible = false;

                                        }

                                        DataTable ddt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));

                                        if (ddt.Rows.Count > 0)
                                        {
                                            lblCOSGidVale.Visible = true;
                                            lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                            lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                            lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                            lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                            string path = ddt.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();

                                            if (path != "")
                                            {
                                                setEsigneAndsedPdfPanelVisiblety(path);
                                            }




                                            lblTStampdeficit2.Text = ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString();
                                            double DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY = Convert.ToDouble(ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString());
                                            lblTstampPenality.Text = ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString();
                                            double TOTAL_PENALTY_AMOUNT = Convert.ToDouble(ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString());

                                            double TotalDefePenality = DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY + TOTAL_PENALTY_AMOUNT;
                                            lblTotalDefePenality.Text = TotalDefePenality.ToString();

                                            //ViewState["FileNameUnSignedPDF"] = ddt.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                            //ViewState["FinalOrderSignedPDF"] = ddt.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                            //ifRecent.Visible = false;
                                            //sendParties.Visible = true;

                                            //docPath.Visible = true;
                                            //docPath.Src = ViewState["FileNameUnSignedPDF"].ToString();
                                            lblTStampdeficit2.Visible = true;
                                            lblTstampPenality.Visible = true;
                                            lblTotalDefePenality.Visible = true;

                                            if (ddt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "0")  //From property valuation by COS
                                            {
                                                rdbtnReportYes.Checked = true;
                                                double TOTALSTAMP_GUIDEVALUE = 0;
                                                TOTALSTAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblCOSGidVale.Visible = true;
                                                lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblPratifal.Visible = true;
                                                lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                lblPStampCOS.Visible = true;
                                                lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                lblStampMuniciple.Visible = true;
                                                lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                lblJanpadD.Visible = true;
                                                lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                lblupkar.Visible = true;
                                                lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                //lblPaid_TStamp_COS_evaul.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());

                                                //lblEx_Order.Visible = true;
                                                //lblToralStamp.Visible = true;
                                                //lblRegFee.Visible = true;
                                                //lblRegExem.Visible = true;
                                                //lblNetRegFee.Visible = true;

                                                Double STAMP_GUIDEVALUE = 0;
                                                STAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                Double TOTALSTAMP_CONSIAMT = 0;
                                                TOTALSTAMP_CONSIAMT = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                Double TOTALSTAMP_PRINCIPLE = 0;
                                                TOTALSTAMP_PRINCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                Double TOTALSTAMP_MUNCIPLE = 0;
                                                TOTALSTAMP_MUNCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                Double TOTALSTAMP_JANPASD = 0;
                                                TOTALSTAMP_JANPASD = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                Double TOTALSTAMP_UPKAR = 0;
                                                TOTALSTAMP_UPKAR = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());

                                                Double TotalCOSCount = TOTALSTAMP_PRINCIPLE + TOTALSTAMP_MUNCIPLE + TOTALSTAMP_JANPASD + TOTALSTAMP_UPKAR;
                                                //lblToralStamp.Text = TotalCOSCount.ToString();
                                                lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["cos_totalstamp_duty"].ToString(); //22_05_2024
                                                DataTable Dttt = new DataTable();
                                                Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (Dttt.Rows.Count > 0)
                                                {

                                                    //lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();  //22_05_2024
                                                    //lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                                                    lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                    lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                                }

                                            }

                                            else
                                            {
                                                DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (dtBasicInfo.Rows.Count > 0)
                                                {
                                                    DataTable ddtt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));
                                                    //if (ddtt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString() != "0")
                                                    if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "1")   // Selection of SR
                                                    {
                                                        pnlCalNo.Visible = true;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportNo.Checked = true;
                                                        RadioButton2.Checked = true;
                                                        rdbtnReportYes.Checked = false;
                                                        lblCOSGidVale.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                                                        //(lblGuideDefict1.Text) = (dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        //Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        lblPratifal.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                                        lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                                                        lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                                        lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                                        lblupkar.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                                        lblToralStamp.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                        lblNetRegFee.Text = ddtt.Rows[0]["NET_REGFEES_COS"].ToString();
                                                        lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                        lblupkar.Visible = true;
                                                        lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                        lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                        lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                        lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                        lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                        lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                        lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString();
                                                        // lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString();
                                                        TotalAmountCalculation();
                                                    }

                                                    else if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "2")  //Selection of PArty
                                                    {
                                                        pnlCalNo.Visible = true;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportNo.Checked = true;
                                                        RadioButton2.Checked = false;
                                                        RadioButton3.Checked = true;
                                                        rdbtnReportYes.Checked = false;
                                                        lblPratifal.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                                        lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();

                                                        lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
                                                        lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
                                                        lblupkar.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
                                                        lblToralStamp.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                                                        lblTotalAmt.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
                                                        lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString();
                                                    }
                                                    else if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "3")  //Selection of cos valuation  module
                                                    {
                                                        pnlCalNo.Visible = false;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportYes.Checked = true;
                                                        rdbtnReportNo.Checked = false;
                                                        RadioButton2.Checked = false;
                                                        RadioButton3.Checked = false;


                                                        lblCOSGidVale.Visible = true;
                                                        lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                        lblPratifal.Visible = true;
                                                        lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                        lblPStampCOS.Visible = true;
                                                        lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                        lblStampMuniciple.Visible = true;
                                                        lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                        lblJanpadD.Visible = true;
                                                        lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                        lblupkar.Visible = true;
                                                        lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                        lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                        lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                        lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                        lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                        lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                        lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["cos_totalstamp_duty"].ToString();


                                                    }



                                                }


                                            }


                                        }
                                        Get_TotalStampDuty();
                                        if (ViewState["Status_Id"].ToString() == "43")
                                        {
                                            //pnlAddCopy.Visible = false;
                                            //btnDraft.Visible = false;
                                            //btnFinalSubmit.Visible = false;
                                            //pnlEsignDSC.Visible = true;
                                            //pnlSendOrder.Visible = false;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;


                                        }
                                        if (ViewState["Status_Id"].ToString() == "44" || ViewState["Status_Id"].ToString() == "45") //E-sign Or DSC
                                        {
                                            pnlAddCopy.Visible = false;
                                            btnDraft.Visible = false;
                                            btnFinalSubmit.Visible = false;
                                            pnlEsignDSC.Visible = false;
                                            pnlSendOrder.Visible = true;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;




                                        }


                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                    }
                                }


                            }

                            if (Flag == 2)  /// Use when final order eSingn or DSC done
                            {

                                DataTable ForEsineCheck = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));

                                ///FINAL_ORDER_DOC_PATH
                                ///
                                string path = ForEsineCheck.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();

                                string file = Path.GetFileNameWithoutExtension(path);
                                string NewPath = path.Replace(file, file + "_Signed");
                                if (File.Exists(Server.MapPath(NewPath)))
                                {
                                    Session["RecentSheetPath"] = NewPath;

                                    Console.WriteLine(NewPath); //photo\myFolder\image-resize.jpg
                                    UpdateEsingStatus();
                                    SetRecentDocPath();
                                }

                                else
                                {
                                    ViewState["FileNameUnSignedPDF"] = ForEsineCheck.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();
                                    ViewState["FinalOrderUnSignedPDF"] = ForEsineCheck.Rows[0]["FINAL_ORDER_DOC_PATH"].ToString();
                                    ViewState["FinalOrderSignedPDF"] = ForEsineCheck.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                }

                                lblCOSGidVale.Visible = true;
                                lblCOSGidVale.Text = (ForEsineCheck.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                lblEx_Order.Text = (ForEsineCheck.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                lblRegExem.Text = (ForEsineCheck.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                lblNetRegFee.Text = (ForEsineCheck.Rows[0]["NET_REGFEES_COS"].ToString());
                                lblPaid_TStamp_COS_evaul.Text = ForEsineCheck.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString();
                                if (Session["Case_Number"] != null)
                                {
                                    if (Session["Case_Number"] != null)
                                    {
                                        ViewState["Case_Number"] = Session["Case_Number"].ToString();
                                        ViewState["HearingDate"] = Session["HearingDate"].ToString();

                                        //ViewState["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //Session["Hearing_ID"] = Session["hearing_id"].ToString();
                                        //ViewState["Status_Id"] = Request.QueryString["Status_Id"].ToString();
                                        if (Session["hearing_id_Final"] != null)
                                        {
                                            Session["Hearing_ID"] = Session["hearing_id_Final"].ToString();
                                            ViewState["Hearing_ID"] = Session["hearing_id_Final"].ToString();

                                        }
                                        else if (Session["Hearing_ID"] != null)
                                        {
                                            ViewState["Hearing_ID"] = Session["Hearing_ID"];
                                        }
                                        if (Session["Status_Id"] != null)
                                        {
                                            Session["Status_Id"] = Session["Status_Id"];
                                            ViewState["Status_Id"] = Session["Status_Id"];
                                        }
                                        string Hearing_Id = Session["hearing_id"].ToString();
                                        if (Session["Notice_ID"] != null)
                                        {
                                            Session["Notice_Id"] = Session["Notice_ID"].ToString();
                                            ViewState["Notice_Id"] = Session["Notice_ID"].ToString();
                                        }
                                        //Session["HearingDate"] = Session["HearingDate"].ToString(); 
                                        DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"].ToString()));
                                        CreateAddCopyTable();
                                        string casenumber = ViewState["Case_Number"].ToString();
                                        //ViewState["PartyDetail"] = dt;
                                        if (dt.Rows.Count > 0)
                                        {

                                            lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                                            lblCaseNo.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
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
                                                lblHearingDt.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                                            }
                                            lblHearingDt.Text = HearingDate;
                                            string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");

                                            string AppNum = lblProposalIdHeading.Text;

                                            Session["AppID"] = dt.Rows[0]["App_ID"].ToString();
                                            Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                                            ViewState["AppID"] = Session["AppID"];

                                            int App_id = Convert.ToInt32(Session["AppID"].ToString());
                                            string appid = Session["AppID"].ToString();
                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                                            string Appno = Session["Appno"].ToString();

                                            lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();

                                            GetPreviousProcedding();



                                            Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                                            All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                                            CreateEmptyFile(All_OrderSheetFileNme);


                                            string All_DocFile_Hearing;
                                            string Proposal_ID = Session["Appno"].ToString();




                                            string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";
                                            PreviousProceeding_List(Convert.ToInt32(appid));



                                            Session["All_DocSheet"] = FileNme;

                                            All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                                            //CreateEmptyFile(All_DocFile_Hearing);
                                            CraetSourceFile(Convert.ToInt32(appid));
                                            AllDocList(Convert.ToInt32(appid));
                                            DataTable dPtartyDetails = new DataTable();
                                            string App_Id = Session["AppID"].ToString();
                                            int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                            DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, App_Id);
                                            //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());
                                            if (dPtartyDetails.Rows.Count > 0)
                                            {
                                                grdNoticeParty.DataSource = dPtartyDetails;
                                                grdNoticeParty.DataBind();
                                                ViewState["SelPrt"] = dPtartyDetails;


                                            }

                                            DataTable dsDisplayPratilipi = new DataTable();
                                            dsDisplayPratilipi = clsFinalOrderBAL.GetAddCopyDeatils_FinalOrder(Convert.ToInt32(App_Id), Convert.ToInt32(Hearing_Id));
                                            if (dsDisplayPratilipi != null)
                                            {
                                                if (dsDisplayPratilipi.Rows.Count > 0)
                                                {



                                                    GrdAddCopy_Details.DataSource = dsDisplayPratilipi;
                                                    GrdAddCopy_Details.DataBind();
                                                    ViewState["AddCopyDetails"] = dsDisplayPratilipi;
                                                    PnlPratilipi.Visible = true;
                                                }
                                            }

                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["SelPrt"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["AddCopyDetails"];
                                            DataTable dtCopyShow = (DataTable)ViewState["SelPrt"];


                                            if (dtApp != null)
                                                if (dtApp.Rows.Count > 0)
                                                {
                                                    for (int i = 0; i < dtApp.Rows.Count; i++)
                                                    {
                                                        Copy_Name = dtApp.Rows[i]["NAME"].ToString();
                                                        Copy_SMS = dtApp.Rows[i]["PHONE_NO"].ToString();
                                                        Copy_WhatsAPP = dtApp.Rows[i]["whatsapp_no"].ToString();
                                                        Copy_Email = dtApp.Rows[0]["EMAIL"].ToString();
                                                        //dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                        dtCopyShow.Rows.Add("0", Copy_Name, "", "", Copy_SMS, Copy_SMS, Copy_Email, "", 0);

                                                    }


                                                }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ///ViewState["CopyDeatils"] = dtCopyShow;
                                            pnlSendOrder.Visible = false;
                                            btnDraft.Visible = true;

                                        }




                                        GuideLineValuePenalityCalculation();
                                        //StampDutyPenalityCalculation();
                                        RegistyPenalityCalculation();
                                        pnlOption.Visible = true;
                                        DataTable dtt = clsFinalOrderBAL.Get_Decision_FinalOrder(Convert.ToInt32(Session["AppID"].ToString()), Convert.ToInt32(ViewState["Hearing_ID"].ToString()));
                                        if (dtt.Rows.Count > 0)
                                        {

                                            summernote.InnerText = dtt.Rows[0]["SR_PROPOSAL"].ToString();
                                            //lblPartyReply.Text = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            txtSRProposal.InnerText = dtt.Rows[0]["OPPOSITIONS_REPLY"].ToString();
                                            //lblNishkarsh.Text = dtt.Rows[0]["COS_DECISION"].ToString();
                                            txtCOSDecision.InnerText = dtt.Rows[0]["COS_DECISION"].ToString();
                                            //lblFinalRemark.Text = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            txtFinalDecision.InnerText = dtt.Rows[0]["FINAL_REMARK"].ToString();
                                            lblSRProposal.Text = summernote.Value;
                                            lblPartyReply.Text = txtSRProposal.Value;
                                            lblNishkarsh.Text = txtCOSDecision.Value;
                                            lblFinalRemark.Text = txtFinalDecision.Value;
                                            lblSRProposal.Visible = false;
                                            lblPartyReply.Visible = false;
                                            lblNishkarsh.Visible = false;
                                            lblFinalRemark.Visible = false;

                                        }

                                        DataTable ddt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));
                                        if (ddt.Rows.Count > 0)
                                        {
                                            lblTStampdeficit2.Text = ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString();
                                            double DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY = Convert.ToDouble(ddt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString());
                                            lblTstampPenality.Text = ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString();
                                            double TOTAL_PENALTY_AMOUNT = Convert.ToDouble(ddt.Rows[0]["TOTAL_PENALTY_AMOUNT"].ToString());

                                            double TotalDefePenality = DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY + TOTAL_PENALTY_AMOUNT;
                                            lblTotalDefePenality.Text = TotalDefePenality.ToString();

                                            Session["RecentSheetPath"] = ddt.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                            ViewState["FinalOrderSignedPDF"] = ddt.Rows[0]["FINALORDER_SIGNED_PATH"].ToString();
                                            //ifRecent.Visible = false;
                                            //sendParties.Visible = true;

                                            SetRecentDocPath();
                                            lblTStampdeficit2.Visible = true;
                                            lblTstampPenality.Visible = true;
                                            lblTotalDefePenality.Visible = true;

                                            if (ddt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "0")  //From property valuation by COS
                                            {
                                                rdbtnReportYes.Checked = true;
                                                double TOTALSTAMP_GUIDEVALUE = 0;
                                                TOTALSTAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblCOSGidVale.Visible = true;
                                                lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                lblPratifal.Visible = true;
                                                lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                lblPStampCOS.Visible = true;
                                                lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                lblStampMuniciple.Visible = true;
                                                lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                lblJanpadD.Visible = true;
                                                lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                lblupkar.Visible = true;
                                                lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                //lblPaid_TStamp_COS_evaul.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());   //comment 18062024

                                                //lblEx_Order.Visible = true;
                                                //lblToralStamp.Visible = true;
                                                //lblRegFee.Visible = true;
                                                //lblRegExem.Visible = true;
                                                //lblNetRegFee.Visible = true;

                                                Double STAMP_GUIDEVALUE = 0;
                                                STAMP_GUIDEVALUE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                Double TOTALSTAMP_CONSIAMT = 0;
                                                TOTALSTAMP_CONSIAMT = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                Double TOTALSTAMP_PRINCIPLE = 0;
                                                TOTALSTAMP_PRINCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                Double TOTALSTAMP_MUNCIPLE = 0;
                                                TOTALSTAMP_MUNCIPLE = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                Double TOTALSTAMP_JANPASD = 0;
                                                TOTALSTAMP_JANPASD = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                Double TOTALSTAMP_UPKAR = 0;
                                                TOTALSTAMP_UPKAR = Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());

                                                Double TotalCOSCount = TOTALSTAMP_PRINCIPLE + TOTALSTAMP_MUNCIPLE + TOTALSTAMP_JANPASD + TOTALSTAMP_UPKAR;
                                                //lblToralStamp.Text = TotalCOSCount.ToString();
                                                lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["cos_totalstamp_duty"].ToString(); //22_05_2024
                                                DataTable Dttt = new DataTable();
                                                Dttt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (Dttt.Rows.Count > 0)
                                                {

                                                    //lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();  //22_05_2024
                                                    //lblPaid_TStamp_COS_evaul.Text = Dttt.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                                                    lbl_Stamp_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                                    lbl_Reg_Paid_COS.Text = Dttt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                                }

                                            }

                                            else
                                            {
                                                DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                                                if (dtBasicInfo.Rows.Count > 0)
                                                {
                                                    DataTable ddtt = clsFinalOrderBAL.Get_TotalStampDuty_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));




                                                    //if (ddtt.Rows[0]["DEFICIT_EXEMPTED_AMOUNT_OF_STAMPDUTY"].ToString() != "0")
                                                    if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "1")   // Selection of SR
                                                    {
                                                        pnlCalNo.Visible = true;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportNo.Checked = true;
                                                        RadioButton2.Checked = true;
                                                        rdbtnReportYes.Checked = false;
                                                        lblCOSGidVale.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                                                        //(lblGuideDefict1.Text) = (dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        //Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                                                        lblPratifal.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                                        lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                                                        lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                                        lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                                        lblupkar.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                                        lblToralStamp.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                                        lblNetRegFee.Text = ddtt.Rows[0]["NET_REGFEES_COS"].ToString();
                                                        lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                        lblupkar.Visible = true;
                                                        lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                        lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                        lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                        lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                        lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                        lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());
                                                        lblPaid_TStamp_COS_evaul.Text = ddt.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString();

                                                        TotalAmountCalculation();
                                                    }

                                                    else if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "2")  //Selection of PArty
                                                    {
                                                        pnlCalNo.Visible = true;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportNo.Checked = true;
                                                        RadioButton2.Checked = false;
                                                        RadioButton3.Checked = true;
                                                        rdbtnReportYes.Checked = false;
                                                        lblPratifal.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                                        lblPStampCOS.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();

                                                        lblStampMuniciple.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
                                                        lblJanpadD.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
                                                        lblupkar.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
                                                        lblToralStamp.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();
                                                        lblRegFee.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                                                        lblTotalAmt.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();

                                                    }
                                                    else if (ddtt.Rows[0]["IS_PROPOSEDBY_SR_OR_RO"].ToString() == "3")  //Selection of cos valuation  module
                                                    {
                                                        pnlCalNo.Visible = false;
                                                        pnlChange.Visible = false;
                                                        rdbtnReportYes.Checked = true;
                                                        rdbtnReportNo.Checked = false;
                                                        RadioButton2.Checked = false;
                                                        RadioButton3.Checked = false;


                                                        lblCOSGidVale.Visible = true;
                                                        lblCOSGidVale.Text = (ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString());
                                                        lblPratifal.Visible = true;
                                                        lblPratifal.Text = (ddt.Rows[0]["COS_TOTALSTAMP_CONSIAMT"].ToString());
                                                        lblPStampCOS.Visible = true;
                                                        lblPStampCOS.Text = (ddt.Rows[0]["COS_TOTALSTAMP_PRINCIPLE"].ToString());
                                                        lblStampMuniciple.Visible = true;
                                                        lblStampMuniciple.Text = (ddt.Rows[0]["COS_TOTALSTAMP_MUNCIPLE"].ToString());
                                                        lblJanpadD.Visible = true;
                                                        lblJanpadD.Text = (ddt.Rows[0]["COS_TOTALSTAMP_JANPASD"].ToString());
                                                        lblupkar.Visible = true;
                                                        lblupkar.Text = (ddt.Rows[0]["COS_TOTALSTAMP_UPKAR"].ToString());
                                                        lblEx_Order.Text = (ddt.Rows[0]["EXEM_STAMPDUTY_COS"].ToString());
                                                        lblToralStamp.Text = (ddt.Rows[0]["NET_STAMPDUTY_COS"].ToString());
                                                        lblPaid_TStamp_COS_evaul.Text = (ddt.Rows[0]["COS_TOTALSTAMP_DUTY"].ToString());

                                                        //lblPaid_TStamp_COS_evaul
                                                        lblRegFee.Text = (ddt.Rows[0]["EXEM_WO_REGFEES_COS"].ToString());
                                                        lblRegExem.Text = (ddt.Rows[0]["EXEM_REGFEES_COS"].ToString());
                                                        lblNetRegFee.Text = (ddt.Rows[0]["NET_REGFEES_COS"].ToString());


                                                    }



                                                }


                                            }


                                        }
                                        Get_TotalStampDuty();
                                        DataTable dtStatus = clsFinalOrderBAL.Get_Status_FinalOrder(Convert.ToInt32(ViewState["AppID"].ToString()));

                                        if (dtStatus.Rows.Count > 0)
                                        {
                                            int Status = Convert.ToInt32(dtStatus.Rows[0]["STATUS_ID"].ToString());
                                            ViewState["Status_Id"] = Status;
                                        }

                                        if (ViewState["Status_Id"].ToString() == "43")
                                        {
                                            pnlAddCopy.Visible = false;
                                            btnDraft.Visible = false;
                                            btnFinalSubmit.Visible = false;
                                            pnlEsignDSC.Visible = true;
                                            pnlSendOrder.Visible = false;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;
                                            //sendParties.Visible = true;



                                        }
                                        if (ViewState["Status_Id"].ToString() == "66" || ViewState["Status_Id"].ToString() == "67")
                                        {
                                            pnlAddCopy.Visible = false;
                                            btnDraft.Visible = false;
                                            btnFinalSubmit.Visible = false;
                                            pnlEsignDSC.Visible = false;
                                            pnlSendOrder.Visible = true;
                                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                                            pnlOption.Visible = false;
                                            pnlChange.Visible = false;
                                            pnlCalNo.Visible = false;

                                            //ifRecent.Visible = false;




                                        }


                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                    }
                                }

                            }



                        }

                        SetDocumentBy_API();
                    }

                }

            }
            catch (Exception ex)
            {

            }


        }

        private void setEsigneAndsedPdfPanelVisiblety(string path)
        {
            pnlSendOrder.Visible = false;
            pnlEsignDSC.Visible = false;


            string file = Path.GetFileNameWithoutExtension(path);
            if (!file.Contains("_Signed"))
            {
                if (File.Exists(Server.MapPath(path))) //file found without sign
                {

                    string NewPath = path.Replace(file, file + "_Signed");
                    if (File.Exists(Server.MapPath(NewPath)))
                    {
                        Session["RecentSheetPath"] = NewPath;

                        Console.WriteLine(NewPath); //photo\myFolder\image-resize.jpg
                        UpdateEsingStatus();
                        pnlSendOrder.Visible = true;
                        SetRecentDocPath();

                    }
                    else
                    {
                        pnlAddCopy.Visible = false;
                        pnlSendOrder.Visible = false;
                        pnlEsignDSC.Visible = true;
                        btnDraft.Visible = false;
                        btnFinalSubmit.Visible = false;
                        ViewState["FileNameUnSignedPDF"] = Path.GetFileName(path);
                    }
                }
                else
                {
                    btnDraft.Visible = true;
                    btnFinalSubmit.Visible = false;
                    pnlSendOrder.Visible = false;
                    pnlEsignDSC.Visible = false;


                }

            }
            else
            {
                // string NewPath = path.Replace(file, file + "_Signed");
                if (File.Exists(Server.MapPath(path)))
                {
                    Session["RecentSheetPath"] = path;

                    Console.WriteLine(path); //photo\myFolder\image-resize.jpg
                    UpdateEsingStatus();
                    pnlSendOrder.Visible = true;
                    SetRecentDocPath();

                }
                else
                {
                    pnlSendOrder.Visible = false;
                    pnlEsignDSC.Visible = false;
                    btnDraft.Visible = true;
                    ViewState["FileNameUnSignedPDF"] = Path.GetFileName(path);
                }
            }

        }

        private void SetRecentDocPath()
        {
            if (Session["RecentSheetPath"] != null)
            {
                if (Session["RecentSheetPath"].ToString() != "")
                {
                    ifRecent.Src = Session["RecentSheetPath"].ToString();
                }
                else
                {
                    ifRecent.Src = "";
                }

            }
        }

        private void UpdateEsingStatus()
        {

            int hearing_id = Convert.ToInt32(Session["hearing_id"].ToString());
            if (Request.QueryString["Response_type"] != null)
            {
                if (Request.QueryString["Response_type"].ToString() == "Hearing_Ordersheet")
                {
                    DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 1);

                }
                else if (Request.QueryString["Response_type"].ToString() == "HearingOrdersheetDSC")
                {
                    DataTable dttUp = clsHearingBAL.Update_EsignCopyStatus_HearingFinalOrder(Convert.ToInt32(Session["AppID"].ToString()), hearing_id, 2);

                }
                else if (Request.QueryString["Response_type"].ToString() == "Final_Order_eSign")
                {
                    DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(Session["AppID"].ToString()), "1", Session["DRID"].ToString(), GetLocalIPAddress());

                }
                else if (Request.QueryString["Response_type"].ToString() == "Final_Order_DSC")
                {
                    DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(Session["AppID"].ToString()), "2", Session["DRID"].ToString(), GetLocalIPAddress());

                }


            }

            //DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(ViewState["AppID"].ToString()), "1", "", GetLocalIPAddress());
        }



        public void PreviousProceeding_List(int APP_ID)
        {
            try
            {
                DataSet dsDocList = clsFinalOrderBAL.GetPreviousProceeding_FinalOrder(APP_ID);

                if (dsDocList != null)
                {
                    if (dsDocList.Tables.Count > 0)
                    {

                        if (dsDocList.Tables[0].Rows.Count > 0)
                        {

                            //ifPDFViewerAll.Src = dsDocList.Tables[0].Rows[0]["final_proceeding_PDF"].ToString(); 
                            ifPrevious.Src = dsDocList.Tables[0].Rows[0]["final_proceeding_PDF"].ToString();

                        }

                    }
                }



            }
            catch (Exception ex)
            {

            }

        }
        public void AllDocList(int APP_ID)
        {
            try
            {
                DataSet dsDocList = clsFinalOrderBAL.GetAllDocList_FinalOrder(APP_ID);
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, "");
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
            catch (Exception ex)
            {

            }

        }
        //private void GetPreviousProcedding()
        //{
        //    if (ViewState["Case_Number"] != null)
        //    {

        //        DataTable dt = clsNoticeBAL.Get_Proceeding(Convert.ToInt32(ViewState["AppID"]));
        //        int appid = Convert.ToInt32(ViewState["AppID"]);
        //        ViewState["Proceeding"] = dt;
        //        if (dt.Rows.Count > 0)
        //        {
        //            Session["HearingDate"] = dt.Rows[0]["hearingdateNotice"].ToString();
        //            lblOrderProceeding.Text = dt.Rows[0]["proceeding"].ToString();

        //        }
        //        DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
        //        if (dtBasicInfo.Rows.Count > 0)
        //        {
        //            lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

        //            lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
        //            lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
        //            lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
        //            lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

        //            lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

        //            lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
        //            lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
        //            lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
        //            lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

        //            //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblRemark.Text = dtBasicInfo.Rows[0]["NatureOfDocuments_Remarks"].ToString();
        //            lblGuideValue.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
        //            lblSROGuide.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
        //            lblGuideDefict.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
        //            lblGuideRemark.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();


        //            lblConRemark.Text = dtBasicInfo.Rows[0]["Property_Consider_Remark"].ToString();
        //            lblRegNo.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
        //            //txtCaseDescription.Text = dt.Rows[0]["Case_Brief_description"].ToString();

        //            lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

        //            lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
        //            lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
        //            lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
        //            lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

        //            //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblRemark.Text = dtBasicInfo.Rows[0]["NatureOfDocuments_Remarks"].ToString();
        //            lblGuideValue.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
        //            lblSROGuide.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
        //            lblGuideDefict.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
        //            lblGuideRemark.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();


        //            lblConRemark.Text = dtBasicInfo.Rows[0]["Property_Consider_Remark"].ToString();
        //            lblRegNo.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
        //            //txtCaseDescription.Text = dt.Rows[0]["Case_Brief_description"].ToString();

        //            lblPrinStamDoc2.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();
        //            lblPrinStampPro2.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
        //            lblMStamp2.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
        //            lblMStampPro2.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
        //            lblJanpad2.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
        //            lblJanpadPro2.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
        //            lblUpkarDoc2.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
        //            lblUpkarPro2.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
        //            lblTStamDoc2.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();

        //            lblTStamppro2.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
        //            //lblTStampdeficit2.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
        //            lblTStampdeficit.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
        //            lblTStampRemark.Text = dtBasicInfo.Rows[0]["Stamp_Duty_Remark"].ToString();
        //            lblTRegDoc2.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
        //            lblTRegPro2.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
        //            lblTRegDeficit2.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
        //            lblTRegDeficit.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
        //            lblTRegRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();

        //            lblTAmtParty2.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
        //            lblTAmtSRO2.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
        //            lblTAmtDeficit2.Text = dtBasicInfo.Rows[0]["Total_DeficitValue"].ToString();
        //            lblTAmtDeficit.Text = dtBasicInfo.Rows[0]["Total_DeficitValue"].ToString();

        //            //lblTAmtRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();


        //            //lblTAmtRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();

        //            //txtSRPro.Text = dt.Rows[0]["SR_Proposal"].ToString();


        //            lblDocParty1.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
        //            lblSRPro1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            lblDefict1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
        //            //lblRemark1.Text = dt.Rows[0]["NatureOfDocuments_Remarks"].ToString();
        //            lblRegNo1.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
        //            lblGuideValue1.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
        //            lblSROGuide1.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
        //            lblGuideDefict1.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
        //            //lblGuideRemark1.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();
        //            lblConValue1.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
        //            lblConValue.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
        //            lblSRCon1.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
        //            lblSRCon.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
        //            lblPrincipledeficit.Text = dtBasicInfo.Rows[0]["Deficit_Principal"].ToString();
        //            lblMuncipleDeficit.Text = dtBasicInfo.Rows[0]["Deficit_Muncipal"].ToString();
        //            lblJanpadDe.Text = dtBasicInfo.Rows[0]["Deficit_Janpad"].ToString();
        //            //lblUpkarDe.Text = dtBasicInfo.Rows[0]["Deficit_Upkar"].ToString();
        //            lblPrinStamDoc.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();
        //            lblPrinStampPro.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
        //            lblMStamp.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
        //            lblMStampPro.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
        //            lblJanpad.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
        //            lblJanpadPro.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
        //            lblUpkarDoc.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
        //            lblUpkarPro.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
        //            lblTStamDoc.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();

        //            lblTStamppro.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
        //            lblTRegDoc.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
        //            lblTRegPro.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
        //            lblTAmtParty.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
        //            lblTAmtSRO.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
        //        }


        //    }

        //}
        private void Get_TotalStampDuty()
        {

            Double PartyProGidVale;
            Double Pratifal;
            Double PStampCOS;
            Double StampMuniciple;
            Double Janpad;
            Double upkar;

            if (lblCOSGidVale.Text != "")
            {
                PartyProGidVale = Convert.ToDouble(lblCOSGidVale.Text);
            }
            else
            {
                PartyProGidVale = 0;
            }
            if (lblPratifal.Text != "")
            {
                Pratifal = Convert.ToDouble(lblPratifal.Text);
            }
            else
            {
                Pratifal = 0;
            }
            if (lblPStampCOS.Text != "")
            {
                PStampCOS = Convert.ToDouble(lblPStampCOS.Text);
            }
            else
            {
                PStampCOS = 0;
            }
            if (lblStampMuniciple.Text != "")
            {
                StampMuniciple = Convert.ToDouble(lblStampMuniciple.Text);
            }
            else
            {
                StampMuniciple = 0;
            }
            if (lblJanpadD.Text != "")
            {
                Janpad = Convert.ToDouble(lblJanpadD.Text);
            }
            else
            {
                Janpad = 0;
            }
            if (lblupkar.Text != "")
            {
                upkar = Convert.ToDouble(lblupkar.Text);
            }
            else
            {
                upkar = 0;
            }

            double TotalStamDuty = PStampCOS + StampMuniciple + Janpad + upkar;
            Double PPStampCOS;
            Double PStampMuniciple;
            Double PJanpad;
            Double Pupkar;
            if (lblPrinStamDoc2.Text != "")
            {
                PPStampCOS = Convert.ToDouble(lblPrinStamDoc2.Text);
            }
            else
            {
                PPStampCOS = 0;
            }
            if (lblMStamp2.Text != "")
            {
                PStampMuniciple = Convert.ToDouble(lblMStamp2.Text);
            }
            else
            {
                PStampMuniciple = 0;
            }
            if (lblJanpad2.Text != "")
            {
                PJanpad = Convert.ToDouble(lblJanpad2.Text);
            }
            else
            {
                PJanpad = 0;
            }
            if (lblUpkarDoc2.Text != "")
            {
                Pupkar = Convert.ToDouble(lblUpkarDoc2.Text);
            }
            else
            {
                Pupkar = 0;
            }

            double TotalDuty_Party = PPStampCOS + PStampMuniciple + PJanpad + Pupkar;
            //lblTStampdeficit2.Text = (TotalDuty_Party.ToString());



            lblToralStamp.Visible = true;
            lblTStampdeficit2.Visible = true;
            lblTstampPenality.Visible = true;
            //lblToralStamp.Text = TotalStamDuty.ToString();
            if (lblTStamDoc2.Text != "")
            {
                double TStampDuty_Party = Convert.ToDouble(lblTStamDoc2.Text);
                double TStampDuty_COS = Convert.ToDouble(lblToralStamp.Text);
                if (TStampDuty_COS == 0)
                {
                    lblTStampdeficit2.Text = "0";
                    lblTstampPenality.Text = "0";
                    lblTotalDefePenality.Text = "0";
                }
                else
                {
                    //double COSDeficit = TStampDuty_Party - TotalStamDuty;
                    //double COSDeficit =   TotalStamDuty- TotalDuty_Party;
                    double COSDeficit = TStampDuty_COS - TStampDuty_Party;
                    lblTStampdeficit2.Text = COSDeficit.ToString();


                    StampDutyPenalityCalculation();
                    double TStampPenality = Convert.ToDouble(lblTstampPenality.Text);
                    double TotalStamPDePenality = COSDeficit + TStampPenality;
                    lblTotalDefePenality.Text = TotalStamPDePenality.ToString();
                }


            }
            if (lblNetRegFee.Text != "")
            {
                decimal NetRegDeficit = Convert.ToDecimal(lblNetRegFee.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                lblNetDeficitReg.Text = NetRegDeficit.ToString();
                //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                lblNetRegDeficit.Text = NetRegDeficit.ToString();
                lblTotalRegfee.Text = NetRegDeficit.ToString();
                lblTotalPayable.Text = "0";
                lblToralStamp.Visible = true;
                double TotalPenalityWithStamp = Convert.ToDouble(lblTotalDefePenality.Text);
                double TotalNetRegAmount = Convert.ToDouble(lblTotalRegfee.Text);
                double TotalPayableAmount = TotalPenalityWithStamp + TotalNetRegAmount;
                lblTotalPayable.Text = TotalPayableAmount.ToString();
                lblNetRegDeficit.Visible = true;
                if (lblTStampdeficit.Text != "")
                {
                    double NetStampDutyDeficit = Convert.ToDouble(lblTStampdeficit.Text);
                    double NetRegFeeDeficit = Convert.ToDouble(lblNetDeficitReg.Text);

                    double TotalAmpountDeficit = NetStampDutyDeficit + NetRegFeeDeficit;
                    lblTAmtDeficit.Text = TotalAmpountDeficit.ToString();

                }

                if (lblToralStamp.Text != "")
                {
                    double NetStampDutyDeficitNirnaye = Convert.ToDouble(lblToralStamp.Text);
                    double NetRegFeeDeficitNirnaye = Convert.ToDouble(lblNetRegFee.Text);

                    double TotalAmpountDeficitNirnaye = NetStampDutyDeficitNirnaye + NetRegFeeDeficitNirnaye;
                    lblTotalAmt.Text = TotalAmpountDeficitNirnaye.ToString();

                }
            }



        }

        private void GetPreviousProcedding()
        {
            if (ViewState["Case_Number"] != null)
            {

                DataTable dt = clsNoticeBAL.Get_Proceeding(Convert.ToInt32(ViewState["AppID"]));
                int appid = Convert.ToInt32(ViewState["AppID"]);
                ViewState["Proceeding"] = dt;
                if (dt.Rows.Count > 0)
                {
                    Session["HearingDate"] = dt.Rows[0]["hearingdateNotice"].ToString();
                    lblOrderProceeding.Text = dt.Rows[0]["proceeding"].ToString();
                    ViewState["Ereg_Id"] = dt.Rows[0]["Ereg_Id"].ToString();

                }
                DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                if (dtBasicInfo.Rows.Count > 0)
                {
                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();
                    //ViewState["Ereg_Id"] = dtBasicInfo.Rows[0]["Ereg_Id"].ToString();
                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin_hi"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype_Hi"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin_hi"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype_Hi"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    lblRemark.Text = dtBasicInfo.Rows[0]["NatureOfDocuments_Remarks"].ToString();
                    lblGuideValue.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
                    lblSROGuide.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                    lblGuideDefict.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
                    lblGuideRemark.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();


                    lblConRemark.Text = dtBasicInfo.Rows[0]["Property_Consider_Remark"].ToString();
                    lblRegNo.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
                    //txtCaseDescription.Text = dt.Rows[0]["Case_Brief_description"].ToString();

                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin_hi"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype_Hi"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    lblRemark.Text = dtBasicInfo.Rows[0]["NatureOfDocuments_Remarks"].ToString();
                    lblGuideValue.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
                    lblSROGuide.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                    lblGuideDefict.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
                    lblGuideRemark.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();


                    lblConRemark.Text = dtBasicInfo.Rows[0]["Property_Consider_Remark"].ToString();
                    lblRegNo.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
                    //txtCaseDescription.Text = dt.Rows[0]["Case_Brief_description"].ToString();

                    lblPrinStamDoc2.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();
                    lblPrinStampPro2.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                    lblMStamp2.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
                    lblMStampPro2.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                    lblJanpad2.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
                    lblJanpadPro2.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                    lblUpkarDoc2.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
                    lblUpkarPro2.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                    lblTStamDoc2.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();

                    lblTStamppro2.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
                    //lblTStampdeficit2.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
                    lblTStampdeficit.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
                    lblTStampRemark.Text = dtBasicInfo.Rows[0]["Stamp_Duty_Remark"].ToString();
                    lblTRegDoc2.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();
                    lblTRegPro2.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYRO"].ToString();
                    lblTRegDeficit2.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
                    //lblTRegDeficit.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
                    lblTRegRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();

                    lblTAmtParty2.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
                    lblTAmtSRO2.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
                    lblTAmtDeficit2.Text = dtBasicInfo.Rows[0]["Total_DeficitValue"].ToString();



                    lblDocParty1.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs_Hi"].ToString();
                    lblSRPro1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    lblDefict1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO_Hi"].ToString();
                    //lblRemark1.Text = dt.Rows[0]["NatureOfDocuments_Remarks"].ToString();
                    lblRegNo1.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
                    lblGuideValue1.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
                    lblSROGuide1.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                    lblGuideDefict1.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
                    //lblGuideRemark1.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();
                    lblConValue1.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                    lblConValue.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                    lblSRCon1.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                    lblSRCon.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                    lblPrincipledeficit.Text = dtBasicInfo.Rows[0]["Deficit_Principal"].ToString();
                    lblMuncipleDeficit.Text = dtBasicInfo.Rows[0]["Deficit_Muncipal"].ToString();
                    lblJanpadDe.Text = dtBasicInfo.Rows[0]["Deficit_Janpad"].ToString();
                    //lblUpkarDe.Text = dtBasicInfo.Rows[0]["Deficit_Upkar"].ToString();
                    lblPrinStamDoc.Text = dtBasicInfo.Rows[0]["Principal_StampDuty"].ToString();
                    lblPrinStampPro.Text = dtBasicInfo.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                    lblMStamp.Text = dtBasicInfo.Rows[0]["Municipal_StampDuty"].ToString();
                    lblMStampPro.Text = dtBasicInfo.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                    lblJanpad.Text = dtBasicInfo.Rows[0]["Janpad_SD"].ToString();
                    lblJanpadPro.Text = dtBasicInfo.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                    lblUpkarDoc.Text = dtBasicInfo.Rows[0]["Upkar"].ToString();
                    lblUpkarPro.Text = dtBasicInfo.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                    lblTStamDoc.Text = dtBasicInfo.Rows[0]["StampDuty"].ToString();

                    lblTStamppro.Text = dtBasicInfo.Rows[0]["Proposed_StampDuty"].ToString();
                    lblTRegDoc.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();

                    //lblTRegDoc.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();
                    lblNetPartyReg.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();


                    lblTRegPro.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYRO"].ToString();
                    //lblTRegPro.Text = dtBasicInfo.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYRO"].ToString();
                    lblNetSRReg.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();

                    lblNetDeficitReg.Text = (Convert.ToDouble(lblNetSRReg.Text) - Convert.ToDouble(lblNetPartyReg.Text)).ToString();
                    //lblTAmtParty.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
                    lblTAmtSRO.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
                    //lblExcepted_Party.Text = dtBasicInfo.Rows[0]["EXEMPTEDAMOUNT_BYPARTY"].ToString(); 
                    lblExcepted_Party.Text = dtBasicInfo.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();
                    lblExempted_SR.Text = dtBasicInfo.Rows[0]["SD_Exemptedamount_byro"].ToString();
                    lblEx_Party.Text = dtBasicInfo.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();
                    lblEx_SR.Text = dtBasicInfo.Rows[0]["SD_Exemptedamount_byro"].ToString();
                    lblRegExemParty.Text = dtBasicInfo.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();
                    lblRegExemSR.Text = dtBasicInfo.Rows[0]["SD_Exemptedamount_byro"].ToString();
                    lblNetRegParty.Text = dtBasicInfo.Rows[0]["SD_Exemptedamount_byro"].ToString();
                    lblNetRegParty.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                    lblNetRegSR.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                    lblExemtptedRegParty.Text = dtBasicInfo.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();

                    lblExemtptedRegSR.Text = dtBasicInfo.Rows[0]["SD_Exemptedamount_byro"].ToString();

                    lbl_Paid_Party.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                    lbl_Paid_SR.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();
                    lbl_PaidReg_Party.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                    lbl_PaidReg_SR.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();

                    lbl_Stamp_Paid_Party.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                    lbl_Stamp_Paid_SR.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();
                    lbl_Reg_Paid_Party.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                    lbl_Reg_Paid_SR.Text = dtBasicInfo.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();

                    lbl_TStamp_Party.Text = dtBasicInfo.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                    lbl_TStamp_SR.Text = dtBasicInfo.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                    lblPaid_TStamp_Party_evaul.Text = dtBasicInfo.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                    lblPaid_TStamp_SR_evaul.Text = dtBasicInfo.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();



                    //Add Total count
                    double TotalStampDuty = Convert.ToDouble(lblTStamDoc.Text);
                    double TotalReg = Convert.ToDouble(lblNetPartyReg.Text);
                    double TotalParty = TotalStampDuty + TotalReg;
                    lblTAmtParty.Text = TotalParty.ToString();

                    double TotalStampDutySR = Convert.ToDouble(lblTStamppro.Text);
                    double TotalRegSR = Convert.ToDouble(lblNetSRReg.Text);
                    double TotalSR = TotalStampDutySR + TotalRegSR;
                    lblTAmtSRO.Text = TotalSR.ToString();

                    double TotalStampDuty1 = Convert.ToDouble(lblTStamDoc2.Text);
                    double TotalReg1 = Convert.ToDouble(lblNetRegParty.Text);
                    double TotalParty1 = TotalStampDuty1 + TotalReg1;
                    lblTAmtParty2.Text = TotalParty1.ToString();

                    double TotalStampParty1 = Convert.ToDouble(lblTStamppro2.Text);
                    double TotalRegParty1 = Convert.ToDouble(lblNetRegSR.Text);
                    double TotalPartyParty1 = TotalStampParty1 + TotalRegParty1;
                    lblTAmtSRO2.Text = TotalPartyParty1.ToString();

                }


            }

        }

        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {
            SrDataMapping();
        }
        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {


            PartyDataMapping();
        }
        private void SrDataMapping()
        {
            DataTable dt = new DataTable();
            DataTable dtDelete = new DataTable();
            //dtDelete = clsFinalOrderBAL.CheckDeleteCOS_FinalOrder_Details(Convert.ToInt32(ViewState["AppID"]));


            dt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
            if (dt.Rows.Count > 0)
            {
                //lblOrderDate.Text = dt.Rows[0]["TodayDt"].ToString();

                txtPartyProGidVale.Text = dt.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                lblCOSGidVale.Text = dt.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                //Convert.ToDecimal(lblGuideDefict1.Text) = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                lblPratifal.Text = dt.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                lblPStampCOS.Text = dt.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                lblStampMuniciple.Text = dt.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                lblJanpadD.Text = dt.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                lblupkar.Text = dt.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                lblToralStamp.Text = dt.Rows[0]["Proposed_StampDuty"].ToString();
                lblRegFee.Text = dt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                //lblTotalAmt.Text = dt.Rows[0]["Total_BY_SRorPO"].ToString();
                lblNetRegSR.Text = dt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                lblNetRegFee.Text = lblNetRegSR.Text;
                lblGuideDefict1.Text = dt.Rows[0]["Deficit_GuideLineValue"].ToString();

                lblEx_Order.Text = lblEx_SR.Text;
                lblRegExem.Text = lblRegExemSR.Text;
                //decimal ConValue1 = Convert.ToDecimal(lblSRCon1.Text) - Convert.ToDecimal(lblConValue1.Text);
                decimal Principledeficit = Convert.ToDecimal(lblPrinStampPro2.Text == "" ? "0" : lblPrinStampPro2.Text) - Convert.ToDecimal(lblPrinStamDoc2.Text == "" ? "0" : lblPrinStamDoc2.Text);
                decimal MuncipleDeficit = Convert.ToDecimal(lblMStampPro2.Text == "" ? "0" : lblMStampPro2.Text) - Convert.ToDecimal(lblMStamp2.Text == "" ? "0" : lblMStamp2.Text);
                decimal JanpadD = Convert.ToDecimal(lblJanpadPro2.Text == "" ? "0" : lblJanpadPro2.Text) - Convert.ToDecimal(lblJanpad2.Text == "" ? "0" : lblJanpad2.Text);
                decimal UpkarDe = Convert.ToDecimal(lblUpkarPro2.Text == "" ? "0" : lblUpkarPro2.Text) - Convert.ToDecimal(lblUpkarDoc2.Text == "" ? "0" : lblUpkarDoc2.Text);
                decimal TStampdeficit2 = Convert.ToDecimal(lblTStamppro2.Text == "" ? "0" : lblTStamppro2.Text) - Convert.ToDecimal(lblTStamDoc2.Text == "" ? "0" : lblTStamDoc2.Text);
                decimal TRegDeficit2 = Convert.ToDecimal(lblTRegDoc2.Text == "" ? "0" : lblTRegDoc2.Text) - Convert.ToDecimal(lblTRegPro2.Text == "" ? "0" : lblTRegPro2.Text);

                decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text == "" ? "0" : lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text == "" ? "0" : lblNetRegParty.Text);
                //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                lblNetRegDeficit.Text = NetRegDeficit.ToString();
                lblNetRegDeficit.Visible = true;
                lblTotalRegfee.Text = NetRegDeficit.ToString();

                txtPartyProGidVale.Visible = false;
                txtPratifal.Visible = false;
                txtPStampCOS.Visible = false;
                txtStampMuniciple.Visible = false;
                txtJanpad.Visible = false;
                txtupkar.Visible = false;
                txtToralStamp.Visible = false;
                txtRegFee.Visible = false;
                txtTotalAmt.Visible = false;
                lblConDefict1.Text = "0.0";
                lblPrincipledeficit.Text = "0.0";
                lblMuncipleDeficit.Text = "0.0";
                lblJanpadDe.Text = "0.0";
                lblUpkarDe.Text = "0.0";
                lblTStampdeficit2.Text = "0.0";
                lblTRegDeficit2.Text = "0.0";
                lblTAmtDeficit2.Text = "0.0";


                txtPStampCOS.Text = dt.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                txtStampMuniciple.Text = dt.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                txtJanpad.Text = dt.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                txtupkar.Text = dt.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                //lblConDefict1.Text = 0.0;
                lblPrincipledeficit.Text = Principledeficit.ToString();
                lblMuncipleDeficit.Text = MuncipleDeficit.ToString();
                lblJanpadDe.Text = JanpadD.ToString();
                lblUpkarDe.Text = UpkarDe.ToString();
                lblTStampdeficit2.Text = TStampdeficit2.ToString();
                lblTRegDeficit2.Text = TRegDeficit2.ToString();
                lblPaid_TStamp_COS_evaul.Text = dt.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                lbl_Stamp_Paid_COS.Text = dt.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();
                lbl_Reg_Paid_COS.Text = dt.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();

                //lblTAmtDeficit2.Text = TAmtDeficit2.ToString();
                if (lblToralStamp.Text != "")
                {
                    double NetStampDutyDeficitNirnaye = Convert.ToDouble(lblToralStamp.Text);
                    double NetRegFeeDeficitNirnaye = Convert.ToDouble(lblNetRegFee.Text);

                    double TotalAmpountDeficitNirnaye = NetStampDutyDeficitNirnaye + NetRegFeeDeficitNirnaye;
                    //lblTotalAmt.Text = TotalAmpountDeficitNirnaye.ToString();  // comment on 22_05_2024

                }
                lblTStampdeficit2.Visible = true;
                lblTstampPenality.Visible = true;

                txtPartyProGidVale.Visible = false;
                txtPratifal.Visible = false;
                txtPStampCOS.Visible = false;
                txtStampMuniciple.Visible = false;
                txtJanpad.Visible = false;
                txtupkar.Visible = false;
                lblCOSGidVale.Visible = true;
                lblPratifal.Visible = true;
                lblPStampCOS.Visible = true;
                lblStampMuniciple.Visible = true;
                lblJanpadD.Visible = true;
                lblupkar.Visible = true;
                lblToralStamp.Visible = true;
                lblRegFee.Visible = true;
                lblTotalAmt.Visible = true;
                lblTStampdeficit2.Visible = true;
                lblTstampPenality.Visible = true;
                txtToralStamp.Text = "";
                txtupkar.Text = "";
                txtJanpad.Text = "";
                txtStampMuniciple.Text = "";
                txtPStampCOS.Text = "";
                txtPratifal.Text = "";
                txtPartyProGidVale.Text = "";
                lblNetRegFee.Visible = true;



                StampDutyPenalityCalculation();
                TotalAmountCalculation();

                double TTStampdeficit2 = Convert.ToDouble(lblTStampdeficit2.Text);
                double TTstampPenality = Convert.ToDouble(lblTstampPenality.Text);
                double Total_Amount = TTStampdeficit2 + TTstampPenality;
                lblTotalDefePenality.Text = Total_Amount.ToString();
            }
            lblTotalPayable.Text = "0.0";
            double TotalPenalityWithStamp = Convert.ToDouble(lblTotalDefePenality.Text);
            double TotalNetRegAmount = Convert.ToDouble(lblNetRegDeficit.Text);
            double TotalPayableAmount = TotalPenalityWithStamp + TotalNetRegAmount;
            lblTotalPayable.Text = TotalPayableAmount.ToString();
        }



        private void PartyDataMapping()
        {
            rdbtnReportNo.Checked = true;
            DataTable dtDelete = new DataTable();
            //dtDelete = clsFinalOrderBAL.CheckDeleteCOS_FinalOrder_Details(Convert.ToInt32(ViewState["AppID"]));

            DataTable dt = new DataTable();

            dt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
            if (dt.Rows.Count > 0)
            {
                //lblOrderDate.Text = dt.Rows[0]["TodayDt"].ToString();

                lblCOSGidVale.Text = dt.Rows[0]["Guideline_PropertyValue"].ToString();
                //Convert.ToDecimal(lblGuideDefict1.Text) = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                lblPratifal.Text = dt.Rows[0]["ConsiderationValueOfProperty"].ToString();
                lblPStampCOS.Text = dt.Rows[0]["Principal_StampDuty"].ToString();

                lblStampMuniciple.Text = dt.Rows[0]["Municipal_StampDuty"].ToString();
                lblJanpadD.Text = dt.Rows[0]["Janpad_SD"].ToString();
                lblupkar.Text = dt.Rows[0]["Upkar"].ToString();
                lblToralStamp.Text = dt.Rows[0]["StampDuty"].ToString();
                //lblRegFee.Text = dt.Rows[0]["Reg_Fee"].ToString();
                //lblTotalAmt.Text = dt.Rows[0]["Total_BY_Partys"].ToString();


                lblRegFee.Text = dt.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();
                txtPartyProGidVale.Text = dt.Rows[0]["Guideline_PropertyValue"].ToString();
                lblGuideDefict1.Text = "0.0";
                txtPStampCOS.Text = dt.Rows[0]["Principal_StampDuty"].ToString();
                txtStampMuniciple.Text = dt.Rows[0]["Municipal_StampDuty"].ToString();
                txtJanpad.Text = dt.Rows[0]["Janpad_SD"].ToString();
                txtupkar.Text = dt.Rows[0]["Upkar"].ToString();
                txtRegFee.Text = dt.Rows[0]["Reg_Fee"].ToString();
                txtTotalAmt.Text = dt.Rows[0]["Total_BY_Partys"].ToString();
                txtToralStamp.Text = dt.Rows[0]["StampDuty"].ToString();
                txtPratifal.Text = dt.Rows[0]["ConsiderationValueOfProperty"].ToString();
                // decimal NetRegDeficit = Convert.ToDecimal(lblNetRegSR.Text) - Convert.ToDecimal(lblNetRegParty.Text);
                //lblNetRegFee.Text = NetRegDeficit.ToString();
                lblNetRegFee.Text = lblNetRegParty.Text;
                lblTotalRegfee.Text = "0.0";
                lblEx_Order.Text = lblEx_Party.Text;
                lblRegExem.Text = lblRegExemParty.Text;
                lblNetRegFee.Visible = true;

                lblPaid_TStamp_COS_evaul.Text = dt.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                lbl_Stamp_Paid_COS.Text = dt.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                lbl_Reg_Paid_COS.Text = dt.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                //decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);
                lblNetRegDeficit.Text = "0.0";
                lblNetRegDeficit.Visible = true;
                //lblConDefict1.Text = "0.0";
                lblConDefict1.Text = "0.0";
                lblPrincipledeficit.Text = "0.0";
                lblMuncipleDeficit.Text = "0.0";
                lblJanpadDe.Text = "0.0";
                lblUpkarDe.Text = "0.0";
                lblTStampdeficit2.Text = "0.0";
                lblTRegDeficit2.Text = "0.0";
                lblTAmtDeficit2.Text = "0.0";
                TPenality.Text = "0.0";
                lblRegPenality.Text = "0.0";
                lblTstampPenality.Text = "0.0";
                lblUpkarPenality.Text = "0.0";
                lblJanpadPenality.Text = "0.0";
                lblMunciplePenality.Text = "0.0";
                lblStampPenality.Text = "0.0";
                lblProPenality.Text = "0.0";
                lblGuidePenality.Text = "0.0";
                lblTotalAmt.Visible = true;
                lblTStampdeficit2.Visible = true;
                lblTstampPenality.Visible = true;
                txtPartyProGidVale.Visible = false;
                txtPratifal.Visible = false;
                txtPStampCOS.Visible = false;
                txtStampMuniciple.Visible = false;
                txtJanpad.Visible = false;
                txtupkar.Visible = false;
                lblCOSGidVale.Visible = true;
                lblPratifal.Visible = true;
                lblPStampCOS.Visible = true;
                lblStampMuniciple.Visible = true;
                lblJanpadD.Visible = true;
                lblupkar.Visible = true;
                lblToralStamp.Visible = true;
                lblRegFee.Visible = true;
                lblTotalAmt.Visible = true;
                lblTStampdeficit2.Visible = true;
                lblTstampPenality.Visible = true;
                lblTotalDefePenality.Text = "0.0";
                lblTotalPayable.Text = "0.0";
                double TotalPenalityWithStamp = Convert.ToDouble(lblTotalDefePenality.Text);
                double TotalNetRegAmount = Convert.ToDouble(lblNetRegDeficit.Text);
                double TotalPayableAmount = TotalPenalityWithStamp + TotalNetRegAmount;
                lblTotalPayable.Text = "0.0";

                TotalAmountCalculation();
            }
        }

        private void TotalAmountCalculation()
        {
            if (lblToralStamp.Text != "" && lblNetRegFee.Text != "")
            {
                lblTotalAmt.Text = (Convert.ToInt32(lblToralStamp.Text) + Convert.ToInt32(lblNetRegFee.Text)).ToString();
            }

        }


        protected void rdbtnReportYes_CheckedChanged(object sender, EventArgs e)
        {
            pnlChange.Visible = false;
            pnlCalNo.Visible = false;
            //tblCOSDec.Rows[0].Visible = false;
            rdbtnReportYes.Checked = true;
        }

        protected void rdbtnReportNo_CheckedChanged(object sender, EventArgs e)
        {
            pnlCalNo.Visible = true;
            pnlChange.Visible = false;
            rdbtnReportNo.Checked = true;
            //AddNotice();
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

        }
        protected void CheckBoxList2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void CheckBoxList3_SelectedIndexChanged(object sender, EventArgs e)
        {

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
            string CopyContent = txtTran.Text;
            string Copywhatsapp = txtWhatsApp.Text;
            //ClsPaymentParams. = Appname;

            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
            dtApp.Rows.Add(Copyname, CopyEmail, CopyMob, CopyContent, Copywhatsapp);

            Session["pratilipi"] = dtApp;
            string sa = Session["pratilipi"].ToString();
            ViewState["CopyDeatils"] = dtApp;
            DataTable dtAppp = (DataTable)ViewState["CopyDeatils"];

            if (dtAppp.Rows.Count > 0)
            {

                GrdAddCopy_Details.DataSource = dtAppp;
                GrdAddCopy_Details.DataBind();
                txtCopyName.Text = "";
                txtCopyEmail.Text = "";
                txtMobile.Text = "";
                txtTran.Text = "";
                //txtWhatsApp.Text = "";
                PnlPratilipi.Visible = true;





            }



            else
            {

            }

        }

        protected void btnSaveCopy_Click(object sender, EventArgs e)
        {
            //Add_Copy();
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
            {
                if (txtTran.Text == "" || txtCopyName.Text == "" || txtMobile.Text == "" || txtCopyEmail.Text == "")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> ValidateAddCopy();</script>");
                    txtTran.Focus();
                }

                else
                {

                    Add_Copy();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

                }
            }

        }

        protected void btnSendFinalOrder_Click(object sender, EventArgs e)
        {
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

            //DataSet ds = clsFinalOrderBAL.InsUpdate_ForwardCaseToRRCFromCos(Convert.ToInt32(App_id), "PENDING", Convert.ToInt32(Session["DistrictID"].ToString())
            //      , Session["District_NameEN"].ToString(), 0, lblProposalIdHeading.Text, Session["DRID"].ToString(), "");

            DataSet ds = clsFinalOrderBAL.Update_FinalOrder_Status_After_Send(Convert.ToInt32(App_id), 51);


            if (chechwhats.Checked)
            {


                string Appid = Session["AppID"].ToString();
                dsDocRecent = clsFinalOrderBAL.GetFinalOrder_ID_forwhatsapp(Convert.ToInt32(Session["Notice_Id"].ToString()), Convert.ToInt32(Appid));


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
                        noticepdf = dsDocRecent.Tables[0].Rows[i]["FinalOrder_Path"].ToString();
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


            //if (chechwhats.Checked)
            //{
            //    string Appid = Session["AppID"].ToString();
            //    dsDocRecent = clsNoticeBAL.GetNotice_ID_forwhatsapp(Convert.ToInt32(Session["Notice_ID"].ToString()), Appid);

            //    if (dsDocRecent.Tables[0].Rows.Count > 0)
            //    {
            //        for (int i = 0; i < dsDocRecent.Tables[0].Rows.Count; i++)
            //        {
            //            Name = dsDocRecent.Tables[0].Rows[i]["SENDER_NAME"].ToString();
            //            if (Name == "")
            //            {
            //                Name = "NA";
            //            }
            //            whatsapp = dsDocRecent.Tables[0].Rows[i]["whatsappno"].ToString();
            //            CaseNo = dsDocRecent.Tables[0].Rows[i]["case_no"].ToString();
            //            RegistrationNo = dsDocRecent.Tables[0].Rows[i]["Reg_Initi_Estammp"].ToString();
            //            noticepdf = dsDocRecent.Tables[0].Rows[i]["noticepdf"].ToString();
            //            PartyID = dsDocRecent.Tables[0].Rows[i]["party_id"].ToString();
            //            if (RegistrationNo == "")
            //            {
            //                RegistrationNo = "NA";
            //            }
            //            if (whatsapp != "")
            //            {
            //                Check_Insert_WhatsAppOptINdd(whatsapp, Name, CaseNo, RegistrationNo, noticepdf, PartyID, Session["Notice_ID"].ToString());
            //            }

            //        }
            //    }
            //}























            if (checksms.Checked)
            {
                //dsDocRecent.Clear();
                string apid = Session["AppID"].ToString();
                string notid = Session["Notice_ID"].ToString();
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
                        //string partyurl = "https://" + authority + "/SampadaCMS/Party/FinalOrder_Preview.aspx?App_Id=" + ViewState["AppID"] + "&App_Number=" + Session["Appno"].ToString();

                        //string partyurl = citizenBaseUrl + "/Party/FinalOrder_Preview.aspx?App_Id=" + ViewState["AppID"] + "&App_Number=" + Session["Appno"].ToString();

                        string partyurl = "https://sampada.mpigr.gov.in/";

                        if (MobileNo_SMS != "")
                        {
                            if (RegistrationNo == "")
                            {
                                RegistrationNo = "NA";
                            }
                            string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " जिसका केस नंबर " + CaseNo + " है, कृपया अंतिम आदेश देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                            //string msg = "Dear "+Name+",a case has been registered against your property ID 1234567 having previous case number "+CaseNo+ "and RRC case no" + CaseNo + ". To view the Auction Order click on below link www.google.com ";

                            string templateid = "1407168854103631812";              // Hindi template
                                                                                    //string templateid = "1407168414968789459";            // English template

                            string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, MobileNo_SMS, msg, secureKey, templateid);
                            //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
                            String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                            clsNoticeBAL.SMSResponse_Insert(RegistrationNo, CaseNo, "SMS", msg, response, PageUrl, MobileNo_SMS, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, 0);

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
                        //string partyurl = "http://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Session["Notice_ID"].ToString() + "&Party_Id=" + PartyID;
                        string partyurl = "https://" + authority + "/SampadaCMS/Party/FinalOrder_Preview.aspx?App_Id=" + ViewState["AppID"] + "&App_Number=" + Session["Appno"].ToString();
                        string msgurl = authority + noticepdf;
                        if (Email != "")
                        {
                            if (RegistrationNo == "")
                            {
                                RegistrationNo = "NA";
                            }
                            //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";
                            string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " जिसका केस नंबर " + CaseNo + " है, कृपया अंतिम आदेश देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";


                            String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
                            EmailUtility emailUtility = new EmailUtility();
                            string userid = HttpContext.Current.Profile.UserName;
                            string IP = HttpContext.Current.Request.UserHostAddress;
                            emailUtility.SendEmail(RegistrationNo, CaseNo, Email, msg, PageUrl, userid, IP, noticepdf);


                        }
                    }
                }
            }

            //DataTable dt2 = clsNoticeBAL.UpdateNoticeSend_Status(Convert.ToInt32(App_id));

            ScriptManager.RegisterStartupScript(upnl1, upnl1.GetType(), "none", "<script> FinalOrderSend();</script>", false);
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>FinalOrderSend();</script>");
        }

        public string Check_Insert_WhatsAppOptINdd(string Whatsapp_Number, string Name, string CaseNo, string RegistrationNo, string noticepdf, string PartyID, string Notice_ID)
        {
            string ResStatus = "False";
            try
            {
                var client = new RestClient(Whatsapp_URL + "method=OPT_IN&format=json&userid=" + WhatsApp_Userid + "&password=" + WhatsApp_Pwd + "&phone_number=" + Whatsapp_Number + "&v=1.1&auth_scheme=plain&channel=WHATSAPP");
                //var client = new RestClient("https://media.smsgupshup.com/GatewayAPI/rest?method=OPT_IN&format=json&userid=2000215884&password=tbMsSWEk&phone_number=" + Whatsapp_Number + "&v=1.1&auth_scheme=plain&channel=WHATSAPP");
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
                noticepdf = noticepdf.Replace("~", "");
                //noticepdf = "/SampadaCMS" + noticepdf;
                string noticepdfsave = noticepdf;
                string Link = "http://" + authority + noticepdf;
                string msgurl = authority + noticepdf;

                //string partyurl = "https://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&Notice_Id=" + Notice_ID + "&Party_Id=" + PartyID;
                //string partyurl = "https://" + authority + "/SampadaCMS/Party/Party_Notice.aspx?App_Id=" + ViewState["AppID"] + "&App_Number=" + Session["Appno"].ToString();

                string partyurl = citizenBaseUrl + "FinalOrderPreview.aspx?App_Id=" + Session["AppID"].ToString() + "&App_Number=" + Session["Appno"].ToString();

                string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrtionNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";

                //string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है, कृपया अंतिम आदेश देखने के लिए लिंक पर क्लिक करें " + partyurl + " |";
                //प्रिय { { 1} }, आपकी संपत्ति रजिस्ट्री क्रमांक { { 2} } के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर { { 3} } है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें { { 4} } |

                string RAM_doblebackslace = "://";
                string RAM_backslace = "/";
                string RAM_Encodedoble = HttpUtility.UrlEncode(RAM_doblebackslace).ToString().ToUpper();
                string RAM_Encodesingle = HttpUtility.UrlEncode(RAM_backslace).ToString().ToUpper();
                noticepdf = noticepdf.Replace("/", RAM_Encodesingle);
                string RAM_MediaUrl = "http" + RAM_Encodedoble + authority + noticepdf;
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
                clsNoticeBAL.WhatsappResponse_Insert(RegistrationNo, CaseNo, "whatsapp", msg, responseString, PageUrl, cntctnumb, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, PartyID, noticepdfsave, Notice_ID);
                //Console.WriteLine("Message Send Successfully");

                //Session["WhatsappTest"] = RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString;
                //Response.Write(RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString);
            }


        }
        private void SaveOrderSheetPDF()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                string divCon = summernote.Value;
                string divCon1 = txtSRProposal.Value;

                string divCon2 = txtCOSDecision.Value;
                string divCon3 = txtFinalDecision.Value;
                DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];

                hdnToDate.Value = DateTime.Now.ToString("dd/MM/yyyy");

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class='' style='width: 100%; margin: 0 auto;  border: 0px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='htmldoc' style='margin: 0 auto;  border: 0px solid #ccc; padding: 30px 10px 30px 10px;'>");

                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600; text-align: center'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                //stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>cos.bhopal@mp.gov.in</h3> ");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>" + hdnCOSOfficeNameHi1.Value + " <br> आदेश </b></h2> ");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<div>");
                //stringBuilder.Append("<h3 style='margin: 0;margin: 10px;font-size: 16px;/* float: left; */text-align: left;'> " + lblRecord.Text + " <br><br><b>आवेदक (प्रथम पक्षकार)</b><br><br><br>" + lblDepartment.Text + "<br><br> <b>अनावेदक (द्वितीय पक्षकार) </b></h3>");
                stringBuilder.Append("<table  style='width: 100%; border: 1px solid black; border-collapse: collapse; '>");
                stringBuilder.Append("<tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '> क्रमांक </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '> नाम </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '> पिता का नाम </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '> पता </th></tr>");
                int srno = 1;
                for (int i = 0; i < ((DataTable)ViewState["SelPrt"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '><b>" + srno + "<b></td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["Name_Hi"] + "</td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["Father / Husband Name Hindi"] + "</td>" +
                 "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["SelPrt"]).Rows[i]["Address_Hi"] + "</td>" +
                 "</tr>");
                    srno++;
                }
                stringBuilder.Append("</table>");

                stringBuilder.Append("</div>");

                stringBuilder.Append("<br>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin: 5px;' > प्रकरण की संख्या : </h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin: 5px;'>" + lblCaseNo.Text + "</h6>");
                //  stringBuilder.Append("<p style = 'font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'>" + lblCaseNo.Text + "</p>");
                stringBuilder.Append("</div>");
                //  stringBuilder.Append("<br>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin:5px;'> प्रकरण का स्रोत :</h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin: 5px;'>" + lblSource.Text + "</h6>");
                //stringBuilder.Append();
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin: 5px;' > प्रकरण का प्रकार :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin:5px;'>" + lblTypeCase.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin: 5px;' > दस्तावेज़ का प्रकार :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin: 5px;'>" + lblTypeDoc.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin: 5px;' > निष्पादन की तारीख :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin: 5px;'>" + lblExeDt.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 30%; font-size: 15px;margin: 5px;' > प्रस्तुति / पंजीकरण की तिथि : </h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px;margin: 5px;'>" + lblDtReg.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style ='padding-top: 10px; padding-bottom: 25px'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;margin: 5px 0px;' > प्रकरण का संक्षिप्त विवरण : </h6>");
                stringBuilder.Append("<p style ='text-align: justify; width: 100%; font-size: 15px;'> " + lblOrderProceeding.Text + " </p>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style ='padding-top: 5px; padding-bottom: 25px'>");
                stringBuilder.Append("<h6 style = 'text-align: justify; width: 100%; font-size: 15px;margin: 5px 0px;' > उपपंजीयक / लोकाधिकारी का प्रस्ताव :</h6>");
                stringBuilder.Append(divCon);

                stringBuilder.Append("</div>");

                stringBuilder.Append("<table style = 'width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;' > क्रमांक </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> दस्तावेज़ का विवरण </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> पार्टियों के अनुसार दस्तावेजों का विवरण</ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> एसआर / पीओ का प्रस्ताव </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;' > मूल्य का अंतर </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> रिमार्क </ th >");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tbody>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 1. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > दस्तावेज़ का प्रकार </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDocParty.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDefict.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");

                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 2.</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> दस्तावेज़ पंजीकरण संख्या </td>");
                stringBuilder.Append("<td colspan = '4' style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegNo.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 3. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > संपत्ति का मार्गदर्शिका मूल्य </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideValue.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSROGuide.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideDefict.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 4. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रतिफल राशि / प्रतिभूति राशि </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConValue.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRCon.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConDefict.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 5. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रिंसिपल स्टाम्प ड्यूटी </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStamDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStampPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 6. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > मुन्सिपल स्टाम्प ड्यूटी </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStamp.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStampPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 7. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > जनपद स्टाम्प ड्यूटी </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpad.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 8. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > उपकर </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 9. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल स्टाम्प ड्यूटी </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_TStamp_Party.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_TStamp_SR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 10. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >छूट प्राप्त राशि</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Party.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_SR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 11. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >भुगतान की गई स्टाम्प ड्यूटी</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Paid_Party.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Paid_SR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 12. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध स्टाम्प ड्यूटी </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamppro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> " + lblTStampdeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStampRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 13. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > रजिस्ट्रेशन फीस </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 14. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > छूट प्राप्त राशि </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemParty.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemSR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 15. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >भुगतान की गई रजिस्ट्रेशन फीस </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_PaidReg_Party.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_PaidReg_SR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 16. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध रजिस्ट्रेशन फीस </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegParty.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegSR.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetDeficitReg.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 17. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल राशि </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtParty.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtSRO.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtDeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtRemark.Text + "</td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("</tbody>");
                stringBuilder.Append("</table>");




                stringBuilder.Append("<div style = 'padding-top: 15px;' >");
                stringBuilder.Append("<h6 style = 'text-align: justify; width: 100 %; font-size: 15px;margin: 5px 0px;' > पक्षकार / पक्षकारों का ज़वाब :</h6>");
                stringBuilder.Append(divCon1);

                stringBuilder.Append("<h6 style = 'text-align: justify; width: 100 %; font-size: 15px;margin: 5px 0px;' > निष्कर्ष / विवेचना :</h6>");
                stringBuilder.Append(divCon2);
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style ='margin-top: 240px;'>");
                stringBuilder.Append("<h6 style = 'text - align: justify; width: 100 %; font-size: 15px;margin: 5px 0px;'> निर्णय : </h6>");
                if (rdbChangeNature.Checked == true)
                {
                    stringBuilder.Append("<table style = 'width: 60%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;' > क्रमांक </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> दस्तावेज़ का विवरण </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px;width:10%; white-space: nowrap; padding: 10px; background: #cccccc85;'> पार्टियों के अनुसार दस्तावेजों का विवरण</ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> एसआर / पीओ का प्रस्ताव </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> आदेश अनुसार प्रभार्य शुल्क </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;' > कमी शूल्क </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> दंड की राशि </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> कुल राशि </ th >");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tbody>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 1. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > दस्तावेज़ का प्रकार </td>");

                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDocParty1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRPro1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDefict1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbldocPenality.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");

                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 2.</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> दस्तावेज़ पंजीकरण संख्या </td>");
                    stringBuilder.Append("<td colspan = '6' style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegNo1.Text + "</td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 3. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > संपत्ति का मार्गदर्शिका मूल्य </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideValue1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSROGuide1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblCOSGidVale.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 4. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रतिफल राशि / प्रतिभूति राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConValue1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRCon1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPratifal.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 5. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रिंसिपल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStamDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStampPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPStampCOS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 6. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > मुन्सिपल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStamp2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStampPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblStampMuniciple.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");

                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 7. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > जनपद स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpad2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadD.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");

                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 8. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > उपकर </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblupkar.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 9. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_Party_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_SR_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_COS_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 10. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > छूट प्राप्त राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Order.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Deficit.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 11. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > भुगतान की गई स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_COS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 12. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamppro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblToralStamp.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStampdeficit2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTstampPenality.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalDefePenality.Text + " </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 13. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegFee.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 14. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > छूट प्राप्त राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemSR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExem.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 15. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > भुगतान की गई रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_COS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 16. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegFee.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegDeficit.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalRegfee.Text + " </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 17. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtParty2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtSRO2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalAmt.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> " + lblTotalPayable.Text + "</td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("</tbody>");
                    stringBuilder.Append("</table>");
                }
                else
                {
                    stringBuilder.Append("<table style = 'width: 60%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;' > क्रमांक </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> दस्तावेज़ का विवरण </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px;width:10%; white-space: nowrap; padding: 10px; background: #cccccc85;'> पार्टियों के अनुसार दस्तावेजों का विवरण</ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> एसआर / पीओ का प्रस्ताव </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> आदेश अनुसार प्रभार्य शुल्क </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;' > कमी शूल्क </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> दंड की राशि </ th >");
                    stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> कुल राशि </ th >");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tbody>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 1. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > दस्तावेज़ का प्रकार </td>");

                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDocParty1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRPro1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDefict1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbldocPenality.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");

                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 2.</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> दस्तावेज़ पंजीकरण संख्या </td>");
                    stringBuilder.Append("<td colspan = '6' style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegNo1.Text + "</td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 3. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > संपत्ति का मार्गदर्शिका मूल्य </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideValue1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSROGuide1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblCOSGidVale.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 4. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रतिफल राशि / प्रतिभूति राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConValue1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRCon1.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPratifal.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 5. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रिंसिपल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStamDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStampPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPStampCOS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 6. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > मुन्सिपल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStamp2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStampPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblStampMuniciple.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");

                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 7. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > जनपद स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpad2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadD.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");

                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 8. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > उपकर </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblupkar.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 9. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_Party_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_SR_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPaid_TStamp_COS_evaul.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 10. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > छूट प्राप्त राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Order.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblEx_Deficit.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 11. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > भुगतान की गई स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Stamp_Paid_COS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 12. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध स्टाम्प ड्यूटी </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamppro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblToralStamp.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStampdeficit2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTstampPenality.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalDefePenality.Text + " </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 13. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDoc2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegPro2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegFee.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 14. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > छूट प्राप्त राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExemSR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegExem.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 15. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > भुगतान की गई रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_Party.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_SR.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbl_Reg_Paid_COS.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 16. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > शुद्ध रजिस्ट्रेशन फीस </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegParty.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegFee.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblNetRegDeficit.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalRegfee.Text + " </td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("<tr>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 17. </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > कुल राशि </td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtParty2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtSRO2.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTotalAmt.Text + "</td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                    stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> " + lblTotalPayable.Text + "</td>");
                    stringBuilder.Append("</tr>");
                    stringBuilder.Append("</tbody>");
                    stringBuilder.Append("</table>");
                }
                stringBuilder.Append("</div>");
                //  stringBuilder.Append("</div>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style = 'padding: 20px 0 0 0;'>");
                stringBuilder.Append("<h6 style = 'text - align: justify; width: 100 %; font-size: 15px;'> अंतिम टिप्पणी :</ h6 >");
                stringBuilder.Append(divCon3);
                stringBuilder.Append("</div>");

                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<br/>");
                //   stringBuilder.Append("<div>");
                stringBuilder.Append("<br/>");
                if (dtPratilipi.Rows.Count > 0)
                {

                    stringBuilder.Append("<b> प्रतिलिपि </b>");
                    stringBuilder.Append("<br/>");
                    stringBuilder.Append("<br/>");
                    stringBuilder.Append("<table style='width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>क्र.</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px; '>सूचनार्थ प्रेषित/विवरण</th></tr>");
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
                stringBuilder.Append("<br/>");
                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<p></p>");

                //stringBuilder.Append("<div style ='text-align: right; padding: 2px 0 5px 0; color:#fff; position: relative; right:100px;' ><b>#8M2h8A4@N78O%bJd</b></div>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 240px;left:-180px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 280px;left:130px;'>स्थान- न्यायालय कलेक्टर ऑफ स्टाम्प <br/>एवं जिला पंजीयक कार्यालय, " + lblHeadingDist.Text + "<br/>जारी दिनांक: " + hdnToDate.Value + " <br/> <br/></b> ");

                //stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 230px;left:-80px; color:#fff;'>#8M2h8A4@N78O%bJd<br/><br/> </b>");



               // stringBuilder.Append("<br/>");
                stringBuilder.Append("<div style = 'text-align: right;padding: 2px 0 5px 0;position: relative;top: 40px;' > ");
                //stringBuilder.Append("<b>स्थान- जिला पंजीयक कार्यालय, " + lblHeadingDist.Text + " <br/> जारी दिनांक: " + hdnToDate.Value + " <br/> <br/></b> ");
               

                stringBuilder.Append("</div>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_FinalOrder.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/COS_FinalOrder/", stringBuilder.ToString());
                Session["RecentSheetPath"] = "~/COS_FinalOrder/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                ////SaveNotice("~/COS_Notice/" + FileNme);
                //setRecentSheetPath();


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
        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            //Response.Redirect("CoSHome.aspx", false);
            //pnlSendOrder.Visible = true;
            pnlAddCopy.Visible = false;
            if (ddl_SignOption.SelectedValue != "0")
            {             
                if ((TxtLast4Digit.Text.Length != 4) && (ddl_SignOption.SelectedValue == "1" || ddl_SignOption.SelectedValue == "3"))
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

            if (ddl_SignOption.SelectedValue=="1")
            {
               Esign_CDAC();
            }
            else if (ddl_SignOption.SelectedValue == "3")
            {
                Esign_EMUDRA();
            }
            else if (ddl_SignOption.SelectedValue == "2")    //HSM DSC
            {
                HSM_DSC();
            }


            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");


        }

        private void HSM_DSC()
        {
            
            string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            PdfName = PdfName.Replace("~/COS_FinalOrder/", "");
            //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
            string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_FinalOrder/" + PdfName.ToString());
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
            string reason = "Order";
            string partitionName = Partition_Name;
            string partitionPassword = Partition_Password;
            string hsmSlotNo = HSM_Slot_No; ////Session["HSMSlotNo"].ToString();
            string javaPath = Java_Path;


            string msg = "";

            if(Session["ordersheet_id_Status"]!=null)
            {
                Session["order_id"] = Session["ordersheet_id_Status"].ToString();
            }
            
            
            if (File.Exists(FileNamefmFolder))
            {
                HSMSigner hSMSigner = new HSMSigner(unsignFilePath, signFileFinalPath, label, signName, location, reason, partitionName, partitionPassword, hsmSlotNo, javaPath);

                //hSMSigner.hsm_DSC();
                msg = hSMSigner.hsm_DSC();
                //Session["HSM_DSC"] = hsmMsg.Text;
                //hsmMsg.Text = javaPath + ", error: " + msg;
                if (File.Exists(NewPath))
                {
                    Session["RecentSheetPath"] = NewPath;

                    int Flag = 2;
                    string resp_status = 1.ToString();
                    string Response_From = "Final_Order_DSC";
                    //string url = "Notice.aspx?Case_Number=" + Session["CaseNum"].ToString() + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag + "&Response_Status=" + resp_status;
                    //string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status;
                    string url = "Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + Response_From;

                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


                   
                }
                else
                {
                    if (msg != "")
                    {
                        ddl_SignOption.SelectedValue = "0";
                        ddleAuthMode.SelectedValue = "0";

                        objClsNewApplication.InsertExeption("Index_Tab_ErrorException.Message = " + msg + ",StatusDescription = Error in HSM DSC", "COS Final Order", "Final_Order_Drafting.aspx", GetLocalIPAddress());
                    }
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowErrorMessageDSC('" + msg + "')", true);
                }
                //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "2", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

            }
            else
            {
                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

            }



           
        }

        private void Esign_EMUDRA()
        {
            if (TxtLast4Digit.Text.Length != 4)
            {

                ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                TxtLast4Digit.Focus();
                return;
            }
            Session["HearingDate"] = lblHearingDt.Text;

            int Flag = 1;
            //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

            //-------eSign Start------------------------

            //string Location = "Project Office -" + HF_Office.Value;
            string Location = "Bhopal";

            string ApplicationNo = "";// hdnProposal.Value;

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
                        if (Session["ordersheet_id_Status"] != null)
                        {
                            Session["order_id"] = Session["ordersheet_id_Status"].ToString();
                        }

                        
                        //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                        ResponseURL_eMudra = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx");

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

        private void Esign_CDAC()
        {
            //-------eSign Start------------------------

            //string Location = "Project Office -" + HF_Office.Value;
            string Location = "Bhopal";

            //string ApplicationNo = hdnProposal.Value;

            string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
            PdfName = PdfName.Replace("~/COS_FinalOrder/", "");
            ViewState["filename"] = PdfName;
            //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
            string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/COS_FinalOrder/" + PdfName.ToString());
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






                        //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_FinalOrder.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&HearingDate=" + ViewState["HearingDate"] + "&Appno=" + Session["Appno"] + "&Party_ID=" + Session["Party_ID"] + "&Notice_ID=" + Session["Notice_ID"] + "&Response_type=Final_Order" + "&Hearing_ID=" + Session["Hearing_ID"]);
                        ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_FinalOrder.aspx");

                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "" , false);
                        //getdata();

                        AuthMode authMode = AuthMode.OTP;

                        eSigner.eSigner _esigner = new eSigner.eSigner();

                        _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                        //getdata_Esign(); ~/COS_FinalOrder/IGRSCMS1000102_2024Jan06121910_FinalOrder.pdf


                        string FilePath_Signed = "~/COS_FinalOrder/" + ViewState["filename"].ToString();


                        //DataTable dttUp = clsFinalOrderBAL.Update_EsignCopyStatus(Convert.ToInt32(ViewState["AppID"].ToString()), "1", "", GetLocalIPAddress());

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

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            if (!rdbtnReportYes.Checked && !rdbtnReportNo.Checked)
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> alert('kindly add option');</script>");
                return;
            }
            int App_ID = Convert.ToInt32(Session["AppID"].ToString());
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> ValidateDecision();</script>");

            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> ValidateValidateDecision();</script>"); ValidateDecision

            DataSet dsList = new DataSet();
            dsList = clsFinalOrderBAL.Get_OrderDetails_OrderPending(App_ID);

           
            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {

                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        if (summernote.Value != "" && txtSRProposal.Value != "" && txtCOSDecision.Value != "" && txtFinalDecision.Value != "")
                        {

                            if (rdbtnReportYes.Checked == true)
                            {


                                DataTable dPtartyDetails = new DataTable();
                                string appid = Session["AppID"].ToString();
                                int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                //string date = DateTime.Now.ToString();
                                DateTime HDt = (Hearing_Dt);

                                //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                                //SaveOrderSheetPDF();
                                DataTable dt = new DataTable();

                                dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(ViewState["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                if (dt.Rows.Count > 0)
                                {
                                    int Hearing_ID = 0;
                                    Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                    DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                    DataTable DtFinal = new DataTable();

                                    if (lblConDefict1.Text == "")
                                    {

                                        lblConDefict1.Text = "0.0";
                                    }
                                    int flag = 0;
                                    if (rdbtnReportNo.Checked == true)
                                    {
                                        if (RadioButton2.Checked == true)
                                        {
                                            flag = 2;
                                        }
                                        else if (RadioButton3.Checked == true)
                                        {
                                            flag = 1;
                                        }

                                    }
                                    else if (rdbtnReportYes.Checked == true)
                                    {
                                        flag = 3;



                                    }
                                    Double COS_TOTALSTAMP_DUTY = 0;
                                    Double TOTAL_REGFEES_COS = 0;
                                    if (lblPaid_TStamp_COS_evaul.Text != "")
                                    {
                                        //COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text.ToString());
                                        COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text);
                                    }
                                    if (lblRegFee.Text != "")
                                    {
                                        //TOTAL_REGFEES_COS = Convert.ToDouble(lblRegFee.Text.ToString());
                                        TOTAL_REGFEES_COS = Convert.ToDouble(lblRegFee.Text);
                                    }
                                    Double NET_REGFEES_COS = 0;
                                    Double EXEM_STAMPDUTY_COS = 0;
                                    Double NET_STAMPDUTY_COS = 0;
                                    Double EXEM_REGFEES_COS = 0;
                                    if (lblNetRegFee.Text != "")
                                    {
                                        NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                    }
                                    if (lblEx_Deficit.Text != "")
                                    {
                                        EXEM_REGFEES_COS = Convert.ToDouble(lblEx_Deficit.Text);
                                    }
                                    if (lblEx_Order.Text != "")
                                    {
                                        EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                    }
                                    if (lblToralStamp.Text != "")
                                    {
                                        NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                    }


                                    if (rdbtnReportYes.Checked == true)
                                    {
                                        DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), COS_TOTALSTAMP_DUTY, TOTAL_REGFEES_COS, NET_REGFEES_COS,
                        EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                    }
                                    else
                                    {
                                        DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), COS_TOTALSTAMP_DUTY, TOTAL_REGFEES_COS, NET_REGFEES_COS,
                        EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                    }


                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

                                    if (rdbtnReportNo.Checked == true)
                                    {
                                        if (RadioButton2.Checked == true)
                                        {
                                            flag = 2;
                                        }
                                        else if (RadioButton3.Checked == true)
                                        {
                                            flag = 1;
                                        }

                                    }

                                    if (lblPaid_TStamp_COS_evaul.Text != "")
                                    {
                                        COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text);

                                    }
                                    if (lblRegFee.Text != "")
                                    {
                                        TOTAL_REGFEES_COS = Convert.ToDouble(lblRegFee.Text);
                                    }
                                    if (dtCopy.Rows.Count > 0)
                                    {
                        //                DataTable DtDeleteAdd = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), COS_TOTALSTAMP_DUTY, TOTAL_REGFEES_COS, NET_REGFEES_COS,
                        //EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                        for (int i = 0; i < dtCopy.Rows.Count; i++)
                                        {
                                            {

                                                string Copyname = (dtCopy.Rows[i]["Copyname"].ToString());
                                                string CopyEmail = (dtCopy.Rows[i]["CopyEmail"].ToString());
                                                string CopyMob = (dtCopy.Rows[i]["CopyMob"].ToString());
                                                string CopyContent = (dtCopy.Rows[i]["CopyContent"].ToString());
                                                string CopyWhatsApp = (dtCopy.Rows[i]["CopyWhatsApp"].ToString());


                                                DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                            }


                                        }

                                    }
                                    DataTable dPtarty = new DataTable();
                                    string app_id = Session["AppID"].ToString();

                                    DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                    //string date = DateTime.Now.ToString();
                                    DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                    //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                    dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                    ViewState["PrtDeatils"] = dPtartyDetails;
                                    string Copy_Name;
                                    string Copy_SMS;
                                    string Copy_Email;
                                    String Copy_WhatsAPP;
                                    DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                    DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                    if (dtApp.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtApp.Rows.Count; i++)
                                        {
                                            Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                            Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                            Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                            Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                            dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                        }


                                    }

                                    grdPartyDisplay.DataSource = dtCopyShow;
                                    grdPartyDisplay.DataBind();





                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                    //pnlAddCopy.Visible = false;

                                }
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                if (dPtartyDetails.Rows.Count > 0)
                                {
                                    grdPartyDisplay.DataSource = dPtartyDetails;
                                    grdPartyDisplay.DataBind();

                                }

                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                }





                            }

                            else
                            {

                                if (rdbtnReportNo.Checked == true)

                                {

                                    if (RadioButton2.Checked == true)
                                    {

                                        DataTable dPtartyDetails = new DataTable();
                                        string appid = Session["AppID"].ToString();
                                        int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                        DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                        //string date = DateTime.Now.ToString();
                                        DateTime HDt = (Hearing_Dt);

                                        //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                                        //SaveOrderSheetPDF();
                                        DataTable dt = new DataTable();

                                        dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(Session["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                        if (dt.Rows.Count > 0)
                                        {
                                            int Hearing_ID = 0;
                                            Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                            //DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                            DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];
                                            DataTable DtFinal = new DataTable();

                                            if (lblConDefict1.Text == "")
                                            {

                                                lblConDefict1.Text = "0.0";
                                            }
                                            int flag = 0;
                                            if (rdbtnReportNo.Checked == true)
                                            {
                                                if (RadioButton2.Checked == true)
                                                {
                                                    flag = 2;
                                                }
                                                else if (RadioButton3.Checked == true)
                                                {
                                                    flag = 1;
                                                }

                                            }

                                            Double COS_TOTALSTAMP_DUTY = 0.0;
                                            Double TOTAL_REGFEES_COS = 0.0;
                                            if (lblPaid_TStamp_COS_evaul.Text != "")
                                            {
                                                //COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text.ToString());
                                                COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text);
                                            }
                                            if (lblRegFee.Text != "")
                                            {
                                                //TOTAL_REGFEES_COS = Convert.ToDouble(TOTAL_REGFEES_COS.ToString());
                                                TOTAL_REGFEES_COS = Convert.ToDouble(lblRegFee.Text);
                                            }
                                            Double NET_REGFEES_COS = 0;
                                            Double EXEM_STAMPDUTY_COS = 0;
                                            Double NET_STAMPDUTY_COS = 0;
                                            Double EXEM_REGFEES_COS = 0;
                                            if (lblNetRegFee.Text != "")
                                            {
                                                NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                            }
                                            if (lblEx_Deficit.Text != "")
                                            {
                                                EXEM_REGFEES_COS = Convert.ToDouble(lblEx_Deficit.Text);
                                            }
                                            if (lblEx_Order.Text != "")
                                            {
                                                EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                            }
                                            if (lblToralStamp.Text != "")
                                            {
                                                NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                            }


                                            DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, Convert.ToDouble(lblToralStamp.Text), 0.0, 1, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), COS_TOTALSTAMP_DUTY, TOTAL_REGFEES_COS, NET_REGFEES_COS,
                        EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);

                                            //DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 2, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), flag);



                                            //if (rdbChangeNature.Checked == true)
                                            //{
                                            //    DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, Convert.ToDouble(lblToralStamp.Text), 0.0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                                            //}
                                            //else
                                            //{

                                            //}


                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

                                            if (dtApp.Rows.Count > 0)
                                            {

                                                for (int i = 0; i < dtApp.Rows.Count; i++)
                                                {
                                                    {

                                                        string Copyname = (dtApp.Rows[i]["Copyname"].ToString());
                                                        string CopyEmail = (dtApp.Rows[i]["CopyEmail"].ToString());
                                                        string CopyMob = (dtApp.Rows[i]["CopyMob"].ToString());
                                                        string CopyContent = (dtApp.Rows[i]["CopyContent"].ToString());
                                                        string CopyWhatsApp = (dtApp.Rows[i]["CopyWhatsApp"].ToString());


                                                        DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                                    }


                                                }

                                            }
                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["PrtDeatils"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            //DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                            //DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                            if (dtApp.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dtApp.Rows.Count; i++)
                                                {
                                                    Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                                    Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                                    dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                }


                                            }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                            //pnlAddCopy.Visible = false;

                                        }
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                        //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                        if (dPtartyDetails.Rows.Count > 0)
                                        {
                                            grdPartyDisplay.DataSource = dPtartyDetails;
                                            grdPartyDisplay.DataBind();

                                        }

                                        else
                                        {
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                        }
                                    }
                                    else if (RadioButton3.Checked == true)
                                    {
                                        DataTable dPtartyDetails = new DataTable();
                                        string appid = Session["AppID"].ToString();
                                        int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                        DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                        //string date = DateTime.Now.ToString();
                                        DateTime HDt = (Hearing_Dt);

                                        //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                                        //SaveOrderSheetPDF();
                                        DataTable dt = new DataTable();

                                        dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(Session["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                        if (dt.Rows.Count > 0)
                                        {
                                            int Hearing_ID = 0;
                                            Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                            DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                            DataTable DtFinal = new DataTable();

                                            if (lblConDefict1.Text == "")
                                            {

                                                lblConDefict1.Text = "0.0";
                                            }

                                            int flag = 0;
                                            if (rdbtnReportNo.Checked == true)
                                            {
                                                if (RadioButton2.Checked == true)
                                                {
                                                    flag = 2;
                                                }
                                                else if (RadioButton3.Checked == true)
                                                {
                                                    flag = 1;
                                                }

                                            }
                                            Double COS_TOTALSTAMP_DUTY = 0;
                                            Double TOTAL_REGFEES_COS = 0;
                                            if (lblPaid_TStamp_COS_evaul.Text != "")
                                            {
                                                //COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text.ToString());
                                                COS_TOTALSTAMP_DUTY = Convert.ToDouble(lblPaid_TStamp_COS_evaul.Text);
                                            }
                                            if (lblRegFee.Text != "")
                                            {
                                                //TOTAL_REGFEES_COS = Convert.ToDouble(TOTAL_REGFEES_COS.ToString());
                                                TOTAL_REGFEES_COS = Convert.ToDouble(lblRegFee.Text);
                                            }

                                            Double NET_REGFEES_COS = 0;
                                            Double EXEM_STAMPDUTY_COS = 0;
                                            Double NET_STAMPDUTY_COS = 0;
                                            Double EXEM_REGFEES_COS = 0;
                                            if (lblNetRegFee.Text != "")
                                            {
                                                NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                            }
                                            if (lblEx_Deficit.Text != "")
                                            {
                                                EXEM_REGFEES_COS = Convert.ToDouble(lblEx_Deficit.Text);
                                            }
                                            if (lblEx_Order.Text != "")
                                            {
                                                EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                            }
                                            if (lblToralStamp.Text != "")
                                            {
                                                NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                            }



                                            DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 2, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), COS_TOTALSTAMP_DUTY, TOTAL_REGFEES_COS, NET_REGFEES_COS,
                        EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                            //if (rdbChangeNature.Checked == true)
                                            //{
                                            //    DtFinal = clsNoticeBAL.UpdateCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text));
                                            //}
                                            //else
                                            //{
                                            //    if ((txtPratifal.Text) == "")
                                            //    {
                                            //        (txtPratifal.Text) = "0.0";
                                            //    }

                                            //}


                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

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


                                                        DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                                    }


                                                }

                                            }
                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["PrtDeatils"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                            DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                            if (dtApp.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dtApp.Rows.Count; i++)
                                                {
                                                    Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                                    Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                                    dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                }


                                            }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                            //pnlAddCopy.Visible = false;

                                        }

                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                        //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                        if (dPtartyDetails.Rows.Count > 0)
                                        {
                                            grdPartyDisplay.DataSource = dPtartyDetails;
                                            grdPartyDisplay.DataBind();

                                        }

                                        else
                                        {
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                        }
                                    }


                                }
                            }
                            btnDraft.Visible = false;
                            btnFinalSubmit.Visible = true;
                            pnlAddCopy.Visible = false;
                            pnlOption.Visible = false;
                            pnlChange.Visible = false;
                            pnlCalNo.Visible = false;
                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);

                        }



                    }

                    else
                    {

                        if (summernote.Value != "" && txtSRProposal.Value != "" && txtCOSDecision.Value != "" && txtFinalDecision.Value != "")
                        {

                            if (rdbtnReportYes.Checked == true)
                            {




                                DataTable dPtartyDetails = new DataTable();
                                string appid = Session["AppID"].ToString();
                                int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                //string date = DateTime.Now.ToString();
                                DateTime HDt = (Hearing_Dt);

                                //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                                //SaveOrderSheetPDF();
                                DataTable dt = new DataTable();

                                dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(ViewState["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                if (dt.Rows.Count > 0)
                                {
                                    int Hearing_ID = 0;
                                    Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                    DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                    DataTable DtFinal = new DataTable();

                                    if (lblConDefict1.Text == "")
                                    {

                                        lblConDefict1.Text = "0.0";
                                    }
                                    int flag = 0;
                                    if (rdbtnReportNo.Checked == true)
                                    {
                                        if (RadioButton2.Checked == true)
                                        {
                                            flag = 2;
                                        }
                                        else if (RadioButton3.Checked == true)
                                        {
                                            flag = 1;
                                        }

                                    }
                                    Double NET_REGFEES_COS = 0;
                                    Double EXEM_STAMPDUTY_COS = 0;
                                    Double NET_STAMPDUTY_COS = 0;
                                    Double EXEM_REGFEES_COS = 0;
                                    if (lblNetRegFee.Text != "")
                                    {
                                        NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                    }
                                    if (lblEx_Deficit.Text != "")
                                    {
                                        EXEM_REGFEES_COS = Convert.ToDouble(lblEx_Deficit.Text);
                                    }
                                    if (lblEx_Order.Text != "")
                                    {
                                        EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                    }
                                    if (lblToralStamp.Text != "")
                                    {
                                        NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                    }


                                    if (rdbtnReportYes.Checked == true)
                                    {
                                        DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), NET_REGFEES_COS,
             EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                    }
                                    else
                                    {
                                        DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), NET_REGFEES_COS,
             EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);
                                    }


                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

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


                                                DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                            }


                                        }

                                    }
                                    DataTable dPtarty = new DataTable();
                                    string app_id = Session["AppID"].ToString();

                                    DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                    //string date = DateTime.Now.ToString();
                                    DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                    //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                    dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                    ViewState["PrtDeatils"] = dPtartyDetails;
                                    string Copy_Name;
                                    string Copy_SMS;
                                    string Copy_Email;
                                    String Copy_WhatsAPP;
                                    DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                    DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                    if (dtApp.Rows.Count > 0)
                                    {
                                        for (int i = 0; i < dtApp.Rows.Count; i++)
                                        {
                                            Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                            Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                            Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                            Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                            dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                        }


                                    }

                                    grdPartyDisplay.DataSource = dtCopyShow;
                                    grdPartyDisplay.DataBind();

                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                    //pnlAddCopy.Visible = false;

                                }
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                if (dPtartyDetails.Rows.Count > 0)
                                {
                                    grdPartyDisplay.DataSource = dPtartyDetails;
                                    grdPartyDisplay.DataBind();

                                }

                                else
                                {
                                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                }





                            }

                            else
                            {

                                if (rdbtnReportNo.Checked == true)

                                {

                                    if (RadioButton2.Checked == true)
                                    {

                                        DataTable dPtartyDetails = new DataTable();
                                        string appid = Session["AppID"].ToString();
                                        int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                        DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                        //string date = DateTime.Now.ToString();
                                        DateTime HDt = (Hearing_Dt);

                                        //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                                        //SaveOrderSheetPDF();
                                        DataTable dt = new DataTable();

                                        dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(Session["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                        if (dt.Rows.Count > 0)
                                        {
                                            int Hearing_ID = 0;
                                            Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                            DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                            DataTable DtFinal = new DataTable();

                                            if (lblConDefict1.Text == "")
                                            {

                                                lblConDefict1.Text = "0.0";
                                            }
                                            int flag = 0;
                                            if (rdbtnReportNo.Checked == true)
                                            {
                                                if (RadioButton2.Checked == true)
                                                {
                                                    flag = 2;
                                                }
                                                else if (RadioButton3.Checked == true)
                                                {
                                                    flag = 1;
                                                }

                                            }
                                            string RecentSheetPath = "";
                                            if (Session["RecentSheetPath"] != null)
                                            {
                                                RecentSheetPath = Session["RecentSheetPath"].ToString();
                                            }
                                            Double NET_REGFEES_COS = 0;
                                            Double EXEM_STAMPDUTY_COS = 0;
                                            Double NET_STAMPDUTY_COS = 0;
                                            Double EXEM_REGFEES_COS = 0;
                                            if (lblNetRegFee.Text != "")
                                            {
                                                NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                            }
                                            if (lblRegExem.Text != "")
                                            {
                                                EXEM_REGFEES_COS = Convert.ToDouble(lblRegExem.Text);
                                            }
                                            if (lblEx_Order.Text != "")
                                            {
                                                EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                            }
                                            if (lblToralStamp.Text != "")
                                            {
                                                NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                            }


                                            DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, Convert.ToDouble(lblToralStamp.Text), 0.0, 1, "", "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), NET_REGFEES_COS,
             EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);

                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), flag);

                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0, 0, 0.0, 2, RecentSheetPath.ToString(), "", 0, "", "", 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, flag);

                                            //if (rdbChangeNature.Checked == true)
                                            //{
                                            //    DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, Convert.ToDouble(lblToralStamp.Text), 0.0, 0.0, 3, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), 0.0, 0.0, 0.0, 0.0, 0.0, 0.0);
                                            //}
                                            //else
                                            //{

                                            //}


                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                                            //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

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


                                                        DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                                    }


                                                }

                                            }
                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["PrtDeatils"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                            DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                            if (dtApp.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dtApp.Rows.Count; i++)
                                                {
                                                    Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                                    Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                                    dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                }


                                            }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                            //pnlAddCopy.Visible = false;

                                        }
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                        //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                        if (dPtartyDetails.Rows.Count > 0)
                                        {
                                            grdPartyDisplay.DataSource = dPtartyDetails;
                                            grdPartyDisplay.DataBind();

                                        }

                                        else
                                        {
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                        }
                                    }
                                    else
                                       if (RadioButton3.Checked == true)
                                    {
                                        DataTable dPtartyDetails = new DataTable();
                                        string appid = Session["AppID"].ToString();
                                        int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                                        DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                        //string date = DateTime.Now.ToString();
                                        DateTime HDt = (Hearing_Dt);

                                        //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());


                                        DataTable dt = new DataTable();

                                        dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(Session["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                                        if (dt.Rows.Count > 0)
                                        {
                                            int Hearing_ID = 0;
                                            Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                                            DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                                            DataTable DtFinal = new DataTable();

                                            if (lblConDefict1.Text == "")
                                            {

                                                lblConDefict1.Text = "0.0";
                                            }
                                            int flag = 0;
                                            if (rdbtnReportNo.Checked == true)
                                            {
                                                if (RadioButton2.Checked == true)
                                                {
                                                    flag = 2;
                                                }
                                                else if (RadioButton3.Checked == true)
                                                {
                                                    flag = 1;
                                                }

                                            }



                                            string RecentSheetPath = "";
                                            if (Session["RecentSheetPath"] != null)
                                            {
                                                RecentSheetPath = Session["RecentSheetPath"].ToString();
                                            }


                                            Double NET_REGFEES_COS = 0;
                                            Double EXEM_STAMPDUTY_COS = 0;
                                            Double NET_STAMPDUTY_COS = 0;
                                            Double EXEM_REGFEES_COS = 0;
                                            if (lblNetRegFee.Text != "")
                                            {
                                                NET_REGFEES_COS = Convert.ToDouble(lblNetRegFee.Text);
                                            }
                                            if (lblEx_Deficit.Text != "")
                                            {
                                                EXEM_REGFEES_COS = Convert.ToDouble(lblEx_Deficit.Text);
                                            }
                                            if (lblEx_Order.Text != "")
                                            {
                                                EXEM_STAMPDUTY_COS = Convert.ToDouble(lblEx_Order.Text);
                                            }
                                            if (lblToralStamp.Text != "")
                                            {
                                                NET_STAMPDUTY_COS = Convert.ToDouble(lblToralStamp.Text);
                                            }





                                            DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, Convert.ToDouble(lblToralStamp.Text), 0.0, 2, "", "", Convert.ToDouble(lblTotalPayable.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToDouble(lblCOSGidVale.Text), Convert.ToDouble(lblPratifal.Text), Convert.ToDouble(lblPStampCOS.Text), Convert.ToDouble(lblStampMuniciple.Text), Convert.ToDouble(lblJanpadD.Text), Convert.ToDouble(lblupkar.Text), NET_REGFEES_COS,
             EXEM_STAMPDUTY_COS, NET_STAMPDUTY_COS, EXEM_REGFEES_COS, flag);


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


                                                        DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                                                    }


                                                }

                                            }
                                            DataTable dPtarty = new DataTable();
                                            string app_id = Session["AppID"].ToString();

                                            DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                                            //string date = DateTime.Now.ToString();
                                            DateTime H_Dt = Convert.ToDateTime(HearingDt);
                                            //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                                            dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                                            ViewState["PrtDeatils"] = dPtartyDetails;
                                            string Copy_Name;
                                            string Copy_SMS;
                                            string Copy_Email;
                                            String Copy_WhatsAPP;
                                            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                                            DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                                            if (dtApp.Rows.Count > 0)
                                            {
                                                for (int i = 0; i < dtApp.Rows.Count; i++)
                                                {
                                                    Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                                                    Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                                                    Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                                                    dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                                                }


                                            }

                                            grdPartyDisplay.DataSource = dtCopyShow;
                                            grdPartyDisplay.DataBind();

                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                                            //pnlAddCopy.Visible = false;

                                        }
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                                        //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());

                                        if (dPtartyDetails.Rows.Count > 0)
                                        {
                                            grdPartyDisplay.DataSource = dPtartyDetails;
                                            grdPartyDisplay.DataBind();

                                        }

                                        else
                                        {
                                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                                        }
                                    }


                                }
                            }

                            ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
                            btnDraft.Visible = false;
                            btnFinalSubmit.Visible = true;
                            pnlAddCopy.Visible = false;
                            pnlOption.Visible = false;
                            pnlChange.Visible = false;
                            pnlCalNo.Visible = false;
                            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
                            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

                            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

                            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";

                        }

                    }


                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);

                    string Message = "Please Enter Authority FeedBack";
                    string Title = "Warning";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','Warning');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                    return;



                }

            }


        }



        public void GuideLineValuePenalityCalculation()
        {

            try
            {
                //lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                string today = DateTime.Now.ToString("dd/MM/yyyy");

                //if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                //{

                //Storing input Dates

                //DateTime FromMonthDays = Convert.ToDateTime(lblDateofExecution.Text);
                //DateTime ToMonthDays = Convert.ToDateTime(lblTodate.Text);

                DateTime FromMonthDays = DateTime.ParseExact(lblExeDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToMonthDays = DateTime.ParseExact(today, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //Creating object of TimeSpan Class  
                TimeSpan objTimeSpan = ToMonthDays - FromMonthDays;
                //years  
                int Years = ToMonthDays.Year - FromMonthDays.Year;
                //months  
                int month = ToMonthDays.Month - FromMonthDays.Month;
                //TotalDays                   
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);
                //TotalDays Round figure
                int TotDays = objTimeSpan.Days;
                //Total Months  
                int TotalMonths = (Years * 12) + month;

                //Assining values to td tags  

                //lblYear.Text = Years + "  Year  " + month + "  Months";
                //lblMonths.Text = Convert.ToString(TotalMonths);
                //lblDays.Text = Convert.ToString(Days);
                //lblTDays.Text = Convert.ToString(TotDays);


                decimal GuideLineValDeficit = decimal.Parse(lblGuideDefict1.Text);

                //int GuideLineValDeficit = Convert.ToInt32(lblGuideValueRegDefcit.Text);
                decimal Penality = GuideLineValDeficit * 2 / 100;
                decimal PenalityPerDay = Penality / 30;
                decimal TotalDaysPenality = PenalityPerDay * TotDays;
                //double totalpen = GuideLineValDeficit + PenPerDay;
                //decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                //lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                decimal FinalPenality = Math.Round(TotalDaysPenality, 2);

                if (FinalPenality <= GuideLineValDeficit)
                {
                    lblGuidePenality.Text = FinalPenality.ToString();
                }
                else
                {
                    lblGuidePenality.Text = GuideLineValDeficit.ToString();
                }


            }
            catch (Exception ex)
            {

            }
        }

        public void StampDutyPenalityCalculation()
        {

            try
            {
                /////---------------Old Calculation------- Days wise penality----------------------


                //string today = DateTime.Now.ToString("dd/MM/yyyy");

                ////if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                ////{

                ////Storing input Dates

                ////DateTime FromMonthDays = Convert.ToDateTime(lblDateofExecution.Text);
                ////DateTime ToMonthDays = Convert.ToDateTime(lblTodate.Text);
                ////if (lblExeDt.Text == "")
                ////{
                ////    lblExeDt.Text = "14/02/2023";
                ////}

                //DateTime FromMonthDays = DateTime.ParseExact(lblExeDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //DateTime ToMonthDays = DateTime.ParseExact(today, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                ////Creating object of TimeSpan Class  
                //TimeSpan objTimeSpan = ToMonthDays - FromMonthDays;
                ////years  
                //int Years = ToMonthDays.Year - FromMonthDays.Year;
                ////months  
                //int month = ToMonthDays.Month - FromMonthDays.Month;
                ////TotalDays                   
                //double Days = Convert.ToDouble(objTimeSpan.TotalDays);
                ////TotalDays Round figure
                //int TotDays = objTimeSpan.Days;

                ////Total Months  
                //int TotalMonths = (Years * 12) + month;

                ////decimal JanpadDutyDeficit = decimal.Parse(lblJanpadDe.Text);
                ////decimal UpkarDutyDeficit = decimal.Parse(lblUpkarDe.Text);
                ////decimal MuncipalDutyDeficit = decimal.Parse(lblMuncipleDeficit.Text);
                ////decimal PrincipleDutyDeficit = decimal.Parse(lblPrincipledeficit.Text);
                //decimal StampDutyDeficit = decimal.Parse(lblTStampdeficit2.Text);

                ////decimal JanpadPenality = JanpadDutyDeficit * 2 / 100;
                ////decimal JanpadPenalityPerDay = JanpadPenality / 30;
                ////decimal TotalDaysJanpadPenality = JanpadPenalityPerDay * TotDays;

                ////decimal UpkarPenality = UpkarDutyDeficit * 2 / 100;
                ////decimal UpkarPenalityPerDay = UpkarPenality / 30;
                ////decimal TotalDaysUpkarPenality = UpkarPenalityPerDay * TotDays;

                ////decimal MuncipalPenality = MuncipalDutyDeficit * 2 / 100;
                ////decimal MuncipalPenalityPerDay = MuncipalPenality / 30;
                ////decimal TotalDaysMuncipalPenality = MuncipalPenalityPerDay * TotDays;

                ////decimal PrinciplePenality = PrincipleDutyDeficit * 2 / 100;
                ////decimal PrinciplePenalityPerDay = PrinciplePenality / 30;
                ////decimal TotalDaysPrinciplePenality = PrinciplePenalityPerDay * TotDays;

                //decimal StampDutyPenality = StampDutyDeficit * 2 / 100;
                //decimal StampDutyPenalityPerDay = StampDutyPenality / 30;

                //decimal TotalDaysStampDutyPenality = StampDutyPenalityPerDay * TotDays;

                ////double totalpen = GuideLineValDeficit + PenPerDay;
                ////decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                ////lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                ////decimal finalJanpadPenality = Math.Round(TotalDaysJanpadPenality, 2);

                ////if (finalJanpadPenality <= JanpadDutyDeficit)
                ////{
                ////    lblJanpadPenality.Text = finalJanpadPenality.ToString();
                ////}
                ////else
                ////{
                ////    lblJanpadPenality.Text = JanpadDutyDeficit.ToString();
                ////}

                ////decimal finalUpkarPenality = Math.Round(TotalDaysUpkarPenality, 2);

                ////if (finalUpkarPenality <= UpkarDutyDeficit)
                ////{
                ////    lblUpkarPenality.Text = finalUpkarPenality.ToString();
                ////}
                ////else
                ////{
                ////    lblUpkarPenality.Text = UpkarDutyDeficit.ToString();
                ////}

                ////decimal finalMuncipalPenality = Math.Round(TotalDaysMuncipalPenality, 2);

                ////if (finalMuncipalPenality <= MuncipalDutyDeficit)
                ////{
                ////    lblMunciplePenality.Text = finalMuncipalPenality.ToString();
                ////}
                ////else
                ////{
                ////    lblMunciplePenality.Text = MuncipalDutyDeficit.ToString();
                ////}

                ////decimal finalPrinciplePenality = Math.Round(TotalDaysPrinciplePenality, 2);

                ////if (finalPrinciplePenality <= PrincipleDutyDeficit)
                ////{
                ////    lblStampPenality.Text = finalPrinciplePenality.ToString();
                ////}
                ////else
                ////{
                ////    lblStampPenality.Text = PrincipleDutyDeficit.ToString();
                ////}



                /////---------------Old Calculation------- Days wise penality----------------------



                /////---------------New Calculation------- Month wise penality----------------------


                string today = DateTime.Now.ToString("dd/MM/yyyy");


                DateTime FromMonthDays = DateTime.ParseExact(lblExeDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToMonthDays = DateTime.ParseExact(today, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                //Creating object of TimeSpan Class  
                TimeSpan objTimeSpan = ToMonthDays - FromMonthDays;
                //years  
                int Years = ToMonthDays.Year - FromMonthDays.Year;
                //months  
                int month = ToMonthDays.Month - FromMonthDays.Month;
                //TotalDays                   
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);
                //TotalDays Round figure
                int TotDays = objTimeSpan.Days;
                int totmonth = 0;
                if (TotDays >= 1)
                {
                    totmonth = month + 1;
                    //Total Months  
                    //int TotalMonths = (Years * 12) + month;

                }

                decimal StampDutyDeficit = 0;
                if (lblTStampdeficit2.Text != "")
                {
                    StampDutyDeficit = decimal.Parse(lblTStampdeficit2.Text);
                }

                decimal StampDutyPenality = StampDutyDeficit * 2 / 100;
                //decimal StampDutyPenalityPerDay = StampDutyPenality / 30;

                //decimal TotalDaysStampDutyPenality = StampDutyPenalityPerDay * TotDays;
                decimal TotalMonthsStampDutyPenality = StampDutyPenality * totmonth;
                decimal FinalStampDutyPenality = Math.Round(TotalMonthsStampDutyPenality, 0);

                if (FinalStampDutyPenality <= StampDutyDeficit)
                {
                    lblTstampPenality.Text = FinalStampDutyPenality.ToString();
                }
                else
                {
                    lblTstampPenality.Text = StampDutyDeficit.ToString();
                }

                /////---------------New Calculation------- Month wise penality----------------------



            }
            catch (Exception ex)
            {

            }
        }

        public void RegistyPenalityCalculation()
        {

            try
            {
                string today = DateTime.Now.ToString("dd/MM/yyyy");
                //if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                //{
                //Storing input Dates  
                DateTime FromMonthDays = DateTime.ParseExact(lblExeDt.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                DateTime ToMonthDays = DateTime.ParseExact(today, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                //Creating object of TimeSpan Class  
                TimeSpan objTimeSpan = ToMonthDays - FromMonthDays;
                //years  
                int Years = ToMonthDays.Year - FromMonthDays.Year;
                //months  
                int month = ToMonthDays.Month - FromMonthDays.Month;
                //TotalDays                   
                double Days = Convert.ToDouble(objTimeSpan.TotalDays);
                //TotalDays Round figure
                int TotDays = objTimeSpan.Days;
                //Total Months  
                int TotalMonths = (Years * 12) + month;

                decimal RegistrationFeeDeficit = decimal.Parse(lblTRegDeficit2.Text);

                decimal Penality = RegistrationFeeDeficit * 2 / 100;
                decimal PenalityPerDay = Penality / 30;
                decimal TotalDaysPenality = PenalityPerDay * TotDays;
                //double totalpen = GuideLineValDeficit + PenPerDay;
                //decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                //lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                decimal FinalPenality = Math.Round(TotalDaysPenality, 2);

                if (FinalPenality <= RegistrationFeeDeficit)
                {
                    lblRegPenality.Text = FinalPenality.ToString();
                }
                else
                {
                    lblRegPenality.Text = RegistrationFeeDeficit.ToString();

                }

                decimal TotalPenality;
                decimal RegPenality = Convert.ToDecimal(lblRegPenality.Text);
                decimal StampPenality = Convert.ToDecimal(lblTstampPenality.Text);
                decimal GuidePenality = Convert.ToDecimal(lblGuidePenality.Text);
                TotalPenality = RegPenality + StampPenality + GuidePenality;
                TPenality.Text = TotalPenality.ToString();

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


        protected void rdbChangeNature_CheckedChanged(object sender, EventArgs e)
        {


            //if (summernote.Value != "" && txtSRProposal.Value != "" && txtCOSDecision.Value != "" && txtFinalDecision.Value != "")
            //{

            if (rdbtnReportYes.Checked == true)
            {


                DataTable dPtartyDetails = new DataTable();
                string appid = Session["AppID"].ToString();
                int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                //string date = DateTime.Now.ToString();
                DateTime HDt = (Hearing_Dt);

                //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                DataTable dtDelete = new DataTable();
                //dtDelete = clsFinalOrderBAL.CheckDeleteCOS_FinalOrder_Details(Convert.ToInt32(ViewState["AppID"]));  // change on 22_05_2024 
                //SaveOrderSheetPDF();
                DataTable dt = new DataTable();

                dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(Convert.ToInt32(ViewState["Hearing_ID"].ToString()), AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                if (dt.Rows.Count > 0)
                {
                    int Hearing_ID = 0;
                    Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                    DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                    DataTable DtFinal = new DataTable();






                    string V_application_number = "";
                    if (Session["Appno"] != null)
                    {
                        V_application_number = Session["Appno"].ToString();
                    }
                    int v_AppID = AppId;
                    string v_CaseNumber = lblCaseNo.Text;
                    int V_ereg_id = 0;
                    string V_hearing = Hearing_Dt.ToString("yyyy-MM-dd");
                    int V_response_esign_status = 0;
                    string V_Response_type = "Request";
                    int V_hearing_id = Hearing_ID;
                    int V_notice_id = 0;
                    int V_status_id = 0;
                    string V_request_url = Request.RawUrl.ToString();
                    string v_client_ip = GetLocalIPAddress();




                    DtFinal = clsFinalOrderBAL.InsertCOS_FinalOrder_COSvalue(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0, 0, 0.0, 0, "", "", 0, "", "", 0, 0, 0, 0, 0, 0, 0);

                    DataTable dtfinalOrderRes = clsNoticeBAL.InsertCOSFinalOrder_Reqest_response(V_application_number, v_AppID, v_CaseNumber
                        , V_ereg_id, V_hearing, V_response_esign_status, V_Response_type, V_hearing_id, V_notice_id, V_status_id,
                        V_request_url, v_client_ip);


                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0, 0, 0.0, 0, "", "", 0, "", "", 0, 0, 0, 0, 0,0,0);

                    //DataTable dtfinalOrderRes = clsNoticeBAL.InsertCOSFinalOrder_Reqest_response(V_application_number, v_AppID, v_CaseNumber
                    //    , V_ereg_id, V_hearing, V_response_esign_status, V_Response_type, V_hearing_id, V_notice_id, V_status_id,
                    //    V_request_url, v_client_ip);

                    //if (rdbChangeNature.Checked == true)
                    //{
                    //    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 0, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalDefePenality.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToInt32(txtPartyProGidVale.Text), Convert.ToInt32(txtPratifal.Text), Convert.ToInt32(txtPStampCOS.Text), Convert.ToInt32(txtStampMuniciple.Text), Convert.ToInt32(txtJanpad.Text), Convert.ToInt32(txtupkar.Text));
                    //}
                    //else
                    //{
                    //    DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, 0.0, Convert.ToDouble(lblTStampdeficit2.Text), 0.0, 0, 0, 0.0, 0, Session["RecentSheetPath"].ToString(), "", Convert.ToDouble(lblTotalDefePenality.Text), "", "", Convert.ToDouble(lblTstampPenality.Text), Convert.ToInt32(txtPartyProGidVale.Text), Convert.ToInt32(txtPratifal.Text), Convert.ToInt32(txtPStampCOS.Text), Convert.ToInt32(txtStampMuniciple.Text), Convert.ToInt32(txtJanpad.Text), Convert.ToInt32(txtupkar.Text));
                    //}


                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text));

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


                                DataTable dttUp = clsNoticeBAL.Insert_Final_ADDCopy(Convert.ToInt32(ViewState["AppID"].ToString()), Hearing_ID, Copyname, CopyEmail, CopyMob, CopyContent, "0", CopyWhatsApp);

                            }


                        }

                    }
                    DataTable dPtarty = new DataTable();
                    string app_id = Session["AppID"].ToString();

                    DateTime HearingDt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                    //string date = DateTime.Now.ToString();
                    DateTime H_Dt = Convert.ToDateTime(HearingDt);
                    //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                    dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(H_Dt, app_id);
                    ViewState["PrtDeatils"] = dPtartyDetails;
                    string Copy_Name;
                    string Copy_SMS;
                    string Copy_Email;
                    String Copy_WhatsAPP;
                    DataTable dtApp = (DataTable)ViewState["CopyDeatils"];
                    DataTable dtCopyShow = (DataTable)ViewState["PrtDeatils"];

                    if (dtApp.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtApp.Rows.Count; i++)
                        {
                            Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                            Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                            Copy_WhatsAPP = dtApp.Rows[i]["CopyMob"].ToString();
                            Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                            dtCopyShow.Rows.Add("0", Copy_Name, Copy_SMS, Copy_SMS, Copy_Email);
                        }


                    }

                    grdPartyDisplay.DataSource = dtCopyShow;
                    grdPartyDisplay.DataBind();
                    pnlAddCopy.Visible = false;
                    pnlEsignDSC.Visible = true;

                    //ifRecent.Visible = false;

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);


                }
                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                pnlAddCopy.Visible = false;
                btnDraft.Visible = false;
                pnlSendOrder.Visible = false;
                pnlEsignDSC.Visible = true;








            }









            int ERegID = 0;

            ERegID = Convert.ToInt32(ViewState["Ereg_Id"].ToString());
            //txtPartyProGidVale.Visible = true;
            //txtPratifal.Visible = true;
            //txtPStampCOS.Visible = true;
            //txtStampMuniciple.Visible = true;
            //txtJanpad.Visible = true;
            //txtupkar.Visible = true;
            //lblCOSGidVale.Visible = false;
            //lblPratifal.Visible = false;
            //lblPStampCOS.Visible = false;
            //lblStampMuniciple.Visible = false;
            //lblJanpadD.Visible = false;
            //lblupkar.Visible = false; 
            //lblToralStamp.Visible = false;
            //lblRegFee.Visible = false;
            //lblTotalAmt.Visible = false;
            //lblTStampdeficit2.Visible = false;
            //lblTstampPenality.Visible = false;
            //txtToralStamp.Text = "";
            //txtupkar.Text = "";
            //txtJanpad.Text = "";
            //txtStampMuniciple.Text = "";
            //txtPStampCOS.Text = "";
            //txtPratifal.Text = "";
            //txtPartyProGidVale.Text = "";
            //rdbtnReportYes.Checked = true;


            //Response.Redirect("https://ersuat2.mp.gov.in/department/#/admin/cosValuationScreen/" + ERegID); //UAT
            //Response.Redirect("https://sampada.mpigr.gov.in/department/#/admin/cosValuationScreen/" + ERegID); //PROD

            //Response.Redirect("http://10.115.203.53:85/department/#/admin/cosValuationScreen/" + ERegID); //PROD
            TotalAmountCalculation();

            Response.Redirect(CoSPropertyValuation_url + ERegID); //UAT


            //Response.Redirect(CoSPropertyValuation_url + ERegID); //PROD


        }

        protected void txtPartyProGidVale_TextChanged(object sender, EventArgs e)
        {

        }

        protected void txtupkar_TextChanged(object sender, EventArgs e)
        {
            Double PartyProGidVale;
            Double Pratifal;
            Double PStampCOS;
            Double StampMuniciple;
            Double Janpad;
            Double upkar;
            if (txtPartyProGidVale.Text != "")
            {
                PartyProGidVale = Convert.ToDouble(txtPartyProGidVale.Text);
            }
            else
            {
                PartyProGidVale = 0;
            }
            if (txtPratifal.Text != "")
            {
                Pratifal = Convert.ToDouble(txtPratifal.Text);
            }
            else
            {
                Pratifal = 0;
            }
            if (txtPStampCOS.Text != "")
            {
                PStampCOS = Convert.ToDouble(txtPStampCOS.Text);
            }
            else
            {
                PStampCOS = 0;
            }
            if (txtStampMuniciple.Text != "")
            {
                StampMuniciple = Convert.ToDouble(txtStampMuniciple.Text);
            }
            else
            {
                StampMuniciple = 0;
            }
            if (txtJanpad.Text != "")
            {
                Janpad = Convert.ToDouble(txtJanpad.Text);
            }
            else
            {
                Janpad = 0;
            }
            if (txtupkar.Text != "")
            {
                upkar = Convert.ToDouble(txtupkar.Text);
            }
            else
            {
                upkar = 0;
            }

            double TotalStamDuty = PartyProGidVale + Pratifal + PStampCOS + StampMuniciple + Janpad + upkar;

            lblToralStamp.Visible = true;
            lblTStampdeficit2.Visible = true;
            lblTstampPenality.Visible = true;
            lblToralStamp.Text = TotalStamDuty.ToString();
            if (lblTStamDoc2.Text != "")
            {
                double TStampDuty_Party = Convert.ToDouble(lblTStamDoc2.Text);
                double COSDeficit = TStampDuty_Party - TotalStamDuty;
                lblTStampdeficit2.Text = COSDeficit.ToString();


                StampDutyPenalityCalculation();
                double TStampPenality = Convert.ToDouble(lblTstampPenality.Text);
                double TotalStamPDePenality = COSDeficit + TStampPenality;
                lblTotalDefePenality.Text = TotalStamPDePenality.ToString();
            }



        }

        protected void btnSendFinalOrder_Click1(object sender, EventArgs e)
        {

        }

        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            int StatusID = 0;
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
            pnlAddCopy.Visible = false;
            pnlEsignDSC.Visible = true;
            btnDraft.Visible = false;
            SaveOrderSheetPDF();
            DataTable DtDelete = new DataTable();
            DataTable dtUpdatePaymentStatus = new DataTable();

            DtDelete = clsFinalOrderBAL.UpdateCOS_FinalOrder_PDF(Convert.ToInt32(ViewState["AppID"].ToString()), Session["RecentSheetPath"].ToString());
            if (rdbtnReportYes.Checked == true)
            {

                DtDelete = clsFinalOrderBAL.UpdateCOS_PaymentStatus(102, Convert.ToInt32(ViewState["AppID"].ToString()));

            }

            else
            {
                if (RadioButton2.Checked == true)
                {
                    DtDelete = clsFinalOrderBAL.UpdateCOS_PaymentStatus(99, Convert.ToInt32(ViewState["AppID"].ToString()));
                }
                if (RadioButton3.Checked == true)
                {
                    DtDelete = clsFinalOrderBAL.UpdateCOS_PaymentStatus(99, Convert.ToInt32(ViewState["AppID"].ToString()));
                }

            }

            btnFinalSubmit.Visible = false;
            pnlAddCopy.Visible = false;
            Edit_Final_Order.Attributes["class"] = "nav-link disabled";
            Edit_Party_Reply.Attributes["class"] = "nav-link disabled";

            Edit_COS_Decision.Attributes["class"] = "nav-link disabled";

            Edit_Final_Decision.Attributes["class"] = "nav-link disabled";
            pnlOption.Visible = false;
            pnlChange.Visible = false;
            pnlCalNo.Visible = false;
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
        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_OrderSheetAllSheetDoc/" + All_OrderSheetFileNme;

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Appno);

                //if (dtDocProDetails.Rows.Count > 0)
                //{
                //    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //    {
                //        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //    }


                //}
            }
            DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppID"].ToString()), Appno);
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
            ViewState["CoS_FinalOrderDoc"] = serverpath;
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
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }

                DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocDetails.Rows.Count > 0)
                {
                    if (dtDocDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                        //iAllDoc.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                    }


                }


                DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    try
                    {
                        string[] addedfilename = new string[4];

                        //addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                        //addedfilename[0] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                        addedfilename[0] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                        addedfilename[1] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                        addedfilename[2] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());
                        if (dt.Rows[0]["add_proceedingpath"] != null)
                        {
                            addedfilename[2] = Server.MapPath(dt.Rows[0]["add_proceedingpath"].ToString());
                        }
                        else
                        {
                            addedfilename[2] = "";
                        }
                        if (dt.Rows[0]["FINAL_PROCEEDING_UNSIGNED_PATH"] != null)
                        {
                            addedfilename[3] = Server.MapPath(dt.Rows[0]["FINAL_PROCEEDING_UNSIGNED_PATH"].ToString());
                        }
                        else
                        {
                            addedfilename[3] = "";
                        }



                        string sourceFile = ViewState["CoS_FinalOrderDoc"].ToString();

                        MargeMultiplePDF(addedfilename, sourceFile);
                        setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());

                    }
                    catch (Exception)
                    {

                    }



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

    }
}
