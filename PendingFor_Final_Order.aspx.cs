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

namespace CMS_Sampada.CoS
{
    public partial class PendingFor_Final_Order : System.Web.UI.Page
    {
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        CoSOrderSheet_BAL clsOrdersheetBAL = new CoSOrderSheet_BAL();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        string All_OrderSheetFileNme = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)


            {

               

                if (Session["Case_Number"] != null)
                {
                    //ViewState["Case_Number"] = Request.QueryString["Case_Number"].ToString();
                    //ViewState["HearingDate"] = Request.QueryString["Hearing"].ToString();
                    ViewState["Case_Number"] = Session["Case_Number"].ToString();
                    ViewState["HearingDate"] = Session["HearingDate"].ToString();

                    if (Session["AppID"] != null)
                    {
                        ViewState["AppID"] = Session["AppID"];
                    }
                    //Session["HearingDate"] = Request.QueryString["Hearing"].ToString(); 00
                    DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"]));
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

                        DataSet dsDocNotice;
                        dsDocNotice = clsNoticeBAL.TOC_Doc_Notice(Convert.ToInt32(appid));
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

                        string All_DocFile_Hearing;
                        string Proposal_ID = Session["Appno"].ToString();




                        string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";



                        Session["All_DocSheet"] = FileNme;
                        //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                        All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                        CreateEmptyFile(All_DocFile_Hearing);
                        CraetSourceFile(Convert.ToInt32(appid));
                        AllDocList(Convert.ToInt32(appid));
                        

                    }


                    GuideLineValuePenalityCalculation();
                    StampDutyPenalityCalculation();
                    RegistyPenalityCalculation();

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");
                }
                else
                {
                    ViewState["Case_Number"] = "000002/B104/32/2022-23";
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

                }
                DataTable dtBasicInfo = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
                if (dtBasicInfo.Rows.Count > 0)
                {
                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblRemark.Text = dtBasicInfo.Rows[0]["NatureOfDocuments_Remarks"].ToString();
                    lblGuideValue.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
                    lblSROGuide.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                    lblGuideDefict.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
                    lblGuideRemark.Text = dtBasicInfo.Rows[0]["Guideline_value_Remark"].ToString();
                   
                   
                    lblConRemark.Text = dtBasicInfo.Rows[0]["Property_Consider_Remark"].ToString();
                    lblRegNo.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
                    //txtCaseDescription.Text = dt.Rows[0]["Case_Brief_description"].ToString();

                    lblCaseNo.Text = dtBasicInfo.Rows[0]["Case_NO"].ToString();

                    lblSource.Text = dtBasicInfo.Rows[0]["CaseOrigin"].ToString();
                    lblTypeCase.Text = dtBasicInfo.Rows[0]["Casetype"].ToString();
                    lblTypeDoc.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblExeDt.Text = dtBasicInfo.Rows[0]["DateOfExecution"].ToString();
                    lblDtReg.Text = dtBasicInfo.Rows[0]["DateOf_Pres_or_Regs"].ToString();

                    //lblReason.Text = dt.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDocParty.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblSRPro.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDefict.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
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
                    lblTStampdeficit2.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
                    lblTStampdeficit.Text = dtBasicInfo.Rows[0]["DeficitDuty"].ToString();
                    //lblTStampRemark.Text = dtBasicInfo.Rows[0]["Stamp_Duty_Remark"].ToString();
                    lblTRegDoc2.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                    lblTRegPro2.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                    lblTRegDeficit2.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
                    lblTRegDeficit.Text = dtBasicInfo.Rows[0]["DeficitRegistrationFees"].ToString();
                    lblTRegRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();

                    lblTAmtParty2.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
                    lblTAmtSRO2.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
                    lblTAmtDeficit2.Text = dtBasicInfo.Rows[0]["Total_DeficitValue"].ToString();
                    lblTAmtDeficit.Text = dtBasicInfo.Rows[0]["Total_DeficitValue"].ToString();

                    lblTAmtRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();


                    lblTAmtRemark.Text = dtBasicInfo.Rows[0]["Reg_Fees_Remark"].ToString();

                    //txtSRPro.Text = dt.Rows[0]["SR_Proposal"].ToString();


                    lblDocParty1.Text = dtBasicInfo.Rows[0]["NatureOfParty_Docs"].ToString();
                    lblSRPro1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    lblDefict1.Text = dtBasicInfo.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                    //lblRemark1.Text = dt.Rows[0]["NatureOfDocuments_Remarks"].ToString();
                    lblRegNo1.Text = dtBasicInfo.Rows[0]["DocsRegNo_asPerparties"].ToString();
                    lblGuideValue1.Text = dtBasicInfo.Rows[0]["Guideline_PropertyValue"].ToString();
                    lblSROGuide1.Text = dtBasicInfo.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                    lblGuideDefict1.Text = dtBasicInfo.Rows[0]["Deficit_GuideLineValue"].ToString();
                    //lblGuideRemark1.Text = dt.Rows[0]["Guideline_value_Remark"].ToString();
                    lblConValue1.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                    lblConValue.Text = dtBasicInfo.Rows[0]["ConsiderationValueOfProperty"].ToString();
                    lblSRCon1.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                    lblSRCon.Text = dtBasicInfo.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                    lblPrincipledeficit.Text = dtBasicInfo.Rows[0]["Deficit_Principal"].ToString();
                    lblMuncipleDeficit.Text = dtBasicInfo.Rows[0]["Deficit_Muncipal"].ToString();
                    lblJanpadDe.Text = dtBasicInfo.Rows[0]["Deficit_Janpad"].ToString();
                    lblUpkarDe.Text = dtBasicInfo.Rows[0]["Deficit_Upkar"].ToString();
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
                    lblTRegDoc.Text = dtBasicInfo.Rows[0]["Reg_Fee"].ToString();
                    lblTRegPro.Text = dtBasicInfo.Rows[0]["ProposedRecoverableRegFee"].ToString();
                    lblTAmtParty.Text = dtBasicInfo.Rows[0]["Total_BY_Partys"].ToString();
                    lblTAmtSRO.Text = dtBasicInfo.Rows[0]["Total_BY_SRorPO"].ToString();
                }


            }

        }


        public void AllDocList(int APP_ID)
        {
            try
            {
                DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);

                if (dsDocList != null)
                {
                    if (dsDocList.Tables.Count > 0)
                    {

                        if (dsDocList.Tables[0].Rows.Count > 0)
                        {
                            grdSRDoc.DataSource = dsDocList;
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
        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                string FileName = FileNme;
                string FinalOrderPath = Server.MapPath(path);
                if (!Directory.Exists(FinalOrderPath))
                {
                    Directory.CreateDirectory(FinalOrderPath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = FinalOrderPath;
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
                converter.Options.MarginLeft = 50;
                converter.Options.MarginRight = 50;
                converter.Options.MarginTop = 30;
                converter.Options.MarginBottom = 80;

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

                using (var stream = File.Create(Path.Combine(FinalOrderPath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return FinalOrderPath + "/" + FileName;
            }
            catch (Exception)
            {
                return "";
            }
        }
        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoS_FinalOrderDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoS_FinalOrderDoc/", "<p>Final Order</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoS_FinalOrderDoc/", "<p>FinalOrder</p>");
            }
            ViewState["ALLDocCAddedPDFPath"] = "~/CoS_FinalOrderDoc/" + filename;
            ViewState["CoS_FinalOrderDoc"] = serverpath;
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
                DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[5];

                    addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    addedfilename[2] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                    addedfilename[3] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                    addedfilename[4] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());

                    string sourceFile = ViewState["CoS_FinalOrderDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                }

            }
            catch (Exception ex)
            {

            }

        }
        protected void RadioButton2_CheckedChanged(object sender, EventArgs e)
        {


            DataTable dt = new DataTable();

            dt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
            if (dt.Rows.Count > 0)
            {
                //lblOrderDate.Text = dt.Rows[0]["TodayDt"].ToString();

                txtPartyProGidVale.Text = dt.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                lblPartyProGidVale.Text= dt.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                               //Convert.ToDecimal(lblGuideDefict1.Text) = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                lblPratifal.Text = dt.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                lblPStampCOS.Text= dt.Rows[0]["Principal_PropsedStmpDuty"].ToString();
                lblStampMuniciple.Text = dt.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                lblJanpadD.Text = dt.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                lblupkar.Text = dt.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                lblToralStamp.Text = dt.Rows[0]["Proposed_StampDuty"].ToString();
                lblRegFee.Text = dt.Rows[0]["ProposedRecoverableRegFee"].ToString();
                lblTotalAmt.Text = dt.Rows[0]["Total_BY_SRorPO"].ToString();

                
                lblGuideDefict1.Text = dt.Rows[0]["Deficit_GuideLineValue"].ToString();
                decimal ConValue1 = Convert.ToDecimal(lblSRCon1.Text) - Convert.ToDecimal(lblConValue1.Text);
                decimal Principledeficit = Convert.ToDecimal(lblPrinStampPro2.Text) - Convert.ToDecimal(lblPrinStamDoc2.Text);
                decimal MuncipleDeficit = Convert.ToDecimal(lblMStampPro2.Text) - Convert.ToDecimal(lblMStamp2.Text); 
                 decimal JanpadD = Convert.ToDecimal(lblJanpadPro2.Text) - Convert.ToDecimal(lblJanpad2.Text);
                decimal UpkarDe = Convert.ToDecimal(lblUpkarPro2.Text) - Convert.ToDecimal(lblUpkarDoc2.Text);
                decimal TStampdeficit2 = Convert.ToDecimal(lblTStamppro2.Text) - Convert.ToDecimal(lblTStamDoc2.Text);
                decimal TRegDeficit2 = Convert.ToDecimal(lblTRegDoc2.Text) - Convert.ToDecimal(lblTRegPro2.Text);
                decimal TAmtDeficit2 = Convert.ToDecimal(lblTAmtParty2.Text) - Convert.ToDecimal(lblTAmtSRO2.Text);

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
                lblConDefict1.Text = ConValue1.ToString();
                lblPrincipledeficit.Text = Principledeficit.ToString();
                lblMuncipleDeficit.Text= MuncipleDeficit.ToString();
                lblJanpadDe.Text = JanpadD.ToString();
                lblUpkarDe.Text = UpkarDe.ToString();
                lblTStampdeficit2.Text = TStampdeficit2.ToString();
                lblTRegDeficit2.Text = TRegDeficit2.ToString();
                lblTAmtDeficit2.Text = TAmtDeficit2.ToString();


            }
        }

        protected void RadioButton3_CheckedChanged(object sender, EventArgs e)
        {

            DataTable dt = new DataTable();

            dt = clsNoticeBAL.Get_FinalOrder_BasicInfo(1, Convert.ToInt32(ViewState["AppID"]), 0);
            if (dt.Rows.Count > 0)
            {
                //lblOrderDate.Text = dt.Rows[0]["TodayDt"].ToString();

                lblPartyProGidVale.Text = dt.Rows[0]["Guideline_PropertyValue"].ToString();
                //Convert.ToDecimal(lblGuideDefict1.Text) = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                Decimal DeficitGuide = Convert.ToDecimal(dt.Rows[0]["Deficit_GuideLineValue"].ToString());
                lblPratifal.Text = dt.Rows[0]["ConsiderationValueOfProperty"].ToString();
                lblPStampCOS.Text = dt.Rows[0]["Principal_StampDuty"].ToString();

                lblStampMuniciple.Text = dt.Rows[0]["Municipal_StampDuty"].ToString();
                lblJanpadD.Text = dt.Rows[0]["Janpad_SD"].ToString();
                lblupkar.Text = dt.Rows[0]["Upkar"].ToString();
                lblToralStamp.Text = dt.Rows[0]["StampDuty"].ToString();
                lblRegFee.Text = dt.Rows[0]["Reg_Fee"].ToString();
                lblTotalAmt.Text = dt.Rows[0]["Total_BY_Partys"].ToString();

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



            }
        }
        private void setAllPdfPath(string vallPdfPath)
        {
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_FinalOrderDoc/" + All_OrderSheetFileNme;
            }
        }

        protected void rdbtnReportYes_CheckedChanged(object sender, EventArgs e)
        {
            pnlChange.Visible = true;
            pnlCalNo.Visible = false;
            //tblCOSDec.Rows[0].Visible = false;
        }

        protected void rdbtnReportNo_CheckedChanged(object sender, EventArgs e)
        {
            pnlCalNo.Visible = true;
            pnlChange.Visible = false;
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
            string CopyWhats = txtWhatsApp.Text;
            string Copy_Name;
            string Copy_SMS;
            string Copy_Email;
            String Copy_WhatsAPP;
            //ClsPaymentParams. = Appname;

            DataTable dtAddCopyparty = (DataTable)ViewState["PartyDeatils"];
            DataTable dtApp = (DataTable)ViewState["CopyDeatils"];

            dtApp.Rows.Add(Copyname, CopyEmail, CopyMob, CopyContent, CopyWhats);
            //dtAddCopyparty.Rows.Add(Name, CopyEmail, CopyMob, CopyContent, CopyWhats)


            ViewState["CopyDeatils"] = dtApp;


            if (dtApp.Rows.Count > 0)
            {
                for (int i = 0; i < dtApp.Rows.Count; i++)
                {
                    Copy_Name = dtApp.Rows[i]["Copyname"].ToString();
                    Copy_SMS = dtApp.Rows[i]["CopyMob"].ToString();
                    Copy_WhatsAPP = dtApp.Rows[i]["CopyWhatsApp"].ToString();
                    Copy_Email = dtApp.Rows[0]["CopyEmail"].ToString();
                    dtAddCopyparty.Rows.Add("0", Copy_Name, Copy_SMS, Copy_WhatsAPP, Copy_Email);
                }


                GrdAddCopy_Details.DataSource = dtApp;
                GrdAddCopy_Details.DataBind();
                grdPartyDisplay.DataSource = dtAddCopyparty;
                grdPartyDisplay.DataBind();


                txtCopyName.Text = "";
                txtCopyEmail.Text = "";
                txtMobile.Text = "";
                txtTran.Text = "";
                txtWhatsApp.Text = "";
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
                    DataTable dPtartyDetails = new DataTable();
                    string appid = Session["AppID"].ToString();
                    int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                    DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                    //string date = DateTime.Now.ToString();
                    DateTime HDt = Convert.ToDateTime(Hearing_Dt);
                    //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());
                    dPtartyDetails = clsNoticeBAL.GetParty_FinalOrder(HDt, appid);
                    //DataTable dt = clsHearingBAL.GetOrderSheet(ViewState["Case_Number"].ToString());
                    if (dPtartyDetails.Rows.Count > 0)
                    {
                        grdPartyDisplay.DataSource = dPtartyDetails;
                        grdPartyDisplay.DataBind();
                        ViewState["PartyDeatils"] = dPtartyDetails;
                    }
                    Add_Copy();
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddNotice();</script>");

                }
            }

        }

        protected void btnSendFinalOrder_Click(object sender, EventArgs e)
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
                string divCon1 = txtSRProposal.Value;

                string divCon2 = txtCOSDecision.Value;
                string divCon3 = txtFinalDecision.Value;
                DataTable dtPratilipi = (DataTable)ViewState["CopyDeatils"];


                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto;  border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto;  border: 1px solid #ccc; padding: 30px 30px 30px 30px;'>");

                stringBuilder.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600; text-align: center '>कलेक्टर ऑफ़ स्टाम्प,न्यायालय</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>cos.bhopal@mp.gov.in <br> भोपाल 2</h3> ");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '><b>ISBT चेतक ब्रिज के पास, भोपाल (म.प्र.) <br> आदेश </b></h2> ");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>मध्यप्रदेश शासन</h2>");
                stringBuilder.Append("<h2 style='margin: 0; margin: 10px; font-size: 16px; text-align: center '>विरुद्ध</h2>");
                stringBuilder.Append("<br>");

           


                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' > प्रकरण की संख्या: </h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblCaseNo.Text + "</h6>");
              //  stringBuilder.Append("<p style = 'font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'>" + lblCaseNo.Text + "</p>");
                stringBuilder.Append("</div>");
              //  stringBuilder.Append("<br>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;'> प्रकरण का स्रोत:</h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblSource.Text + "</h6>");
                //stringBuilder.Append();
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' >प्रकरण का प्रकार :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblTypeCase.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' >दस्तावेज़ का प्रकार :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblTypeDoc.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' >निष्पादन की तारीख :</ h6 >");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblExeDt.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' >प्रस्तुति / पंजीकरण की तिथि : </h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px'>" + lblDtReg.Text);
                stringBuilder.Append("</h6>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex;'>");
                stringBuilder.Append("<h6 style ='text-align: justify; width: 40%; font-size: 15px;' >प्रकरण का संक्षिप्त विवरण : </h6>");
                stringBuilder.Append("<h6 style ='text-align: left; width: 60%; font-size: 15px' > " + lblOrderProceeding.Text + " </h6>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style ='padding-top: 5px; padding-bottom: 25px'>");
                stringBuilder.Append("<b><h6 style = 'text-align: justify; width: 100%; font-size: 15px;' ><b > उपपंजीयक / लोकाधिकारी का प्रस्ताव:</b ></h6> </b> ");
                stringBuilder.Append(divCon);

                stringBuilder.Append("</div>");

                stringBuilder.Append("<table style = 'width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;' > सीरियल < br />नंबर </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> दस्तावेज़ का< br />विवरण </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> पार्टियों के अनुसार दस्तावेज /< br />दस्तावेजों का विवरण</ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> एसआर / पीओ का< br />प्रस्ताव </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;' > मूल्य का< br />अंतर </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> रिमार्क </ th >");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tbody>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 1. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > दस्तावेज़ का प्रकार :</td>");
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
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > संपत्ति का मार्गदर्शिका मूल्य :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideValue.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSROGuide.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideDefict.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 4. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रतिफल राशि /प्रतिभूति राशि :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConValue.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRCon.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConDefict.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 5. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रिंसिपल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStamDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStampPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 6. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > मुन्सिपल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStamp.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStampPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 7. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >जनपद स्टाम्प ड्यूटी:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpad.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 8. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >उपकर :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 9. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >कुल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamppro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 10. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >रजिस्ट्रेशन फीस:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDoc.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegPro.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegRemark.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 11. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >कुल राशि:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtParty.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtSRO.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtDeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtRemark.Text + "</td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("</tbody>");
                stringBuilder.Append("</table>");


                stringBuilder.Append("<div style = 'padding-top: 15px;' >");
                stringBuilder.Append("<h6 style = 'text-align: justify; width: 100 %; font-size: 15px;' >< b > पक्षकार / पक्षकारों का ज़वाब :</b></h6>");
                stringBuilder.Append(divCon1);

                stringBuilder.Append("<h6 style = 'text-align: justify; width: 100 %; font - size: 15px;' >< b >सी ओ एस का निष्कर्ष:</b></h6>");
                stringBuilder.Append(divCon2);
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<h6 style = 'text - align: justify; width: 100 %; font - size: 15px;'> सी ओ एस का निर्णय : </h6>");

                stringBuilder.Append("<table style = 'width: 100%; border: 1px solid black; border-collapse: collapse; text-align: left; white-space: nowrap'>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;' > सीरियल < br />नंबर </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> दस्तावेज़ का< br />विवरण </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> पार्टियों के अनुसार दस्तावेज /< br />दस्तावेजों का विवरण</ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> एसआर / पीओ का< br />प्रस्ताव </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;'> सी ओ एस <br />मूल्य </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 10px; background: #cccccc85;' > मूल्य का< br />अंतर </ th >");
                stringBuilder.Append("<th style = 'border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; white-space: nowrap; padding: 10px; background: #cccccc85;'> दंड की राशि </ th >");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tbody>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 1. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > दस्तावेज़ का प्रकार :</td>");

                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDocParty1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRPro1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblDefict1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lbldocPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");

                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 2.</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'> दस्तावेज़ पंजीकरण संख्या </td>");
                stringBuilder.Append("<td colspan = '5' style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegNo1.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 3. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > संपत्ति का मार्गदर्शिका मूल्य :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideValue1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSROGuide1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtPartyProGidVale.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuideDefict1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblGuidePenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 4. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रतिफल राशि /प्रतिभूति राशि :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConValue1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblSRCon1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtPratifal.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblConDefict1.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblProPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 5. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > प्रिंसिपल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStamDoc2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrinStampPro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtPStampCOS.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblPrincipledeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblStampPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 6. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > मुन्सिपल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStamp2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMStampPro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtStampMuniciple.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMuncipleDeficit.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblMunciplePenality.Text + "</td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 7. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >जनपद स्टाम्प ड्यूटी:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpad2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtJanpad.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadDe.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblJanpadPenality.Text + "</td>");
                stringBuilder.Append("</tr>");

                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 8. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >उपकर :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDoc2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtupkar.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarDe.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblUpkarPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 9. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >कुल स्टाम्प ड्यूटी :</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamDoc2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStamppro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtToralStamp.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTStampdeficit2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTstampPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 10. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >रजिस्ट्रेशन फीस:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDoc2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegPro2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtRegFee.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTRegDeficit2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblRegPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' > 11. </td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;' >कुल राशि:</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtParty2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtSRO2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + txtTotalAmt.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + lblTAmtDeficit2.Text + "</td>");
                stringBuilder.Append("<td style = 'border: 1px solid black; border-collapse: collapse; height: 40px; padding: 10px;'>" + TPenality.Text + "</td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</tbody>");
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
              //  stringBuilder.Append("</div>");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<div style='display: inline-block;float: left '>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style = 'padding: 20px 0 0 0;'>");
                stringBuilder.Append("<h6 style = 'text - align: justify; width: 100 %; font - size: 15px;'> अंतिम टिप्पणी:</ h6 >");
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
                stringBuilder.Append("<div style = 'text-align: right;padding: 2px 0 5px 0;' > ");
                stringBuilder.Append("<b>स्थान- जिला पंजीयक कार्यालय, भोपाल-2 <br/> जारी दिनांक: " + DateTime.Now + " <br/> <br/></b> ");
                stringBuilder.Append("</div>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_FinalOrder.pdf";

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
        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            Response.Redirect("CoSHome.aspx", false);
        }

        protected void btnDraft_Click(object sender, EventArgs e)
        {
            if (summernote.Value != "")
            {
                DataTable dPtartyDetails = new DataTable();
                string appid = Session["AppID"].ToString();
                int AppId = Convert.ToInt32(ViewState["AppID"].ToString());
                DateTime Hearing_Dt = Convert.ToDateTime(ViewState["HearingDate"].ToString());
                //string date = DateTime.Now.ToString();
                DateTime HDt = (Hearing_Dt);
                
                //DateTime Hearing = Convert.toda(ViewState["HearingDate"].ToString());

                SaveOrderSheetPDF();
                DataTable dt = new DataTable();
                
                dt = clsNoticeBAL.InsertCOSDecision_FinalOrder(0, AppId, Hearing_Dt, lblOrderProceeding.Text, txtSRProposal.Value, summernote.Value, txtCOSDecision.Value, txtFinalDecision.Value);
                if (dt.Rows.Count > 0)
                {
                    int Hearing_ID = 0;
                    Hearing_ID = Convert.ToInt32(dt.Rows[0]["Hearing_ID"].ToString());
                    DataTable dtCopy = (DataTable)ViewState["CopyDeatils"];
                    DataTable DtFinal = new DataTable();

                    Double conDeficit = Convert.ToDouble(lblConDefict1.Text);

                    int flag = 0;
                    if (rdbtnReportNo.Checked == true)
                    {
                        if (RadioButton2.Checked == true)
                        {
                            flag = 1;
                        }
                        else if (RadioButton3.Checked == true)
                        {
                            flag = 2;
                        }

                    }
                    Double NET_REGFEES_COS = 0;
                    Double EXEM_STAMPDUTY_COS = 0;
                    Double NET_STAMPDUTY_COS = 0;
                    Double EXEM_REGFEES_COS = 0;
                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToInt32(lblGuideValue1.Text), Convert.ToInt32(lblGuideDefict1.Text), Convert.ToInt32(lblConDefict1.Text),Convert.ToInt32(lblPrincipledeficit.Text), Convert.ToInt32(lblMuncipleDeficit.Text), Convert.ToInt32(lblJanpadDe.Text), Convert.ToInt32(lblUpkarDe.Text),Convert.ToInt32(lblTStampdeficit2.Text), Convert.ToInt32(lblTRegDeficit2.Text),0,0, Convert.ToInt32(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), DateTime.Now.ToString("dd/MM/YYYY"), 0, "", "");
                    //DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, "", "", Convert.ToDecimal(lblGuideValue1.Text), Convert.ToDecimal(lblGuideDefict1.Text), Convert.ToDecimal(lblConDefict1.Text), Convert.ToDecimal(lblPrincipledeficit.Text), Convert.ToDecimal(lblMuncipleDeficit.Text), Convert.ToDecimal(lblJanpadDe.Text), Convert.ToDecimal(lblUpkarDe.Text), Convert.ToDecimal(lblTStampdeficit2.Text), Convert.ToDecimal(lblTRegDeficit2.Text), 0, 0, Convert.ToDecimal(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDecimal(TPenality.Text));
                    DtFinal = clsNoticeBAL.InsertCOS_FinalOrder(AppId, Session["Appno"].ToString(), lblCase_Number.Text, Convert.ToDouble(lblGuideValue1.Text), Convert.ToDouble(lblGuideDefict1.Text), conDeficit, Convert.ToDouble(lblPrincipledeficit.Text), Convert.ToDouble(lblMuncipleDeficit.Text), Convert.ToDouble(lblJanpadDe.Text), Convert.ToDouble(lblUpkarDe.Text), Convert.ToDouble(lblTStampdeficit2.Text), Convert.ToDouble(lblTRegDeficit2.Text), 0, 0, Convert.ToDouble(lblTAmtDeficit2.Text), 0, Session["RecentSheetPath"].ToString(), "", 0, "", "", Convert.ToDouble(TPenality.Text), 0, 0, 0, 0, 0, 0,
                        NET_REGFEES_COS,
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
                    pnlSend.Visible = true;
                    ifRecent.Visible = false;
                    docPath.Visible = true;
                    docPath.Src = Session["RecentSheetPath"].ToString();
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                   

                }

                else
                {
                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('कृपया प्रकर्ण का संचित्त विवरण चयन करे  :', '', 'success')", true);
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

                decimal JanpadDutyDeficit = decimal.Parse(lblJanpadDe.Text);
                decimal UpkarDutyDeficit = decimal.Parse(lblUpkarDe.Text);
                decimal MuncipalDutyDeficit = decimal.Parse(lblMuncipleDeficit.Text);
                decimal PrincipleDutyDeficit = decimal.Parse(lblPrincipledeficit.Text);
                decimal StampDutyDeficit = decimal.Parse(lblTStampdeficit2.Text);

                decimal JanpadPenality = JanpadDutyDeficit * 2 / 100;
                decimal JanpadPenalityPerDay = JanpadPenality / 30;
                decimal TotalDaysJanpadPenality = JanpadPenalityPerDay * TotDays;

                decimal UpkarPenality = UpkarDutyDeficit * 2 / 100;
                decimal UpkarPenalityPerDay = UpkarPenality / 30;
                decimal TotalDaysUpkarPenality = UpkarPenalityPerDay * TotDays;

                decimal MuncipalPenality = MuncipalDutyDeficit * 2 / 100;
                decimal MuncipalPenalityPerDay = MuncipalPenality / 30;
                decimal TotalDaysMuncipalPenality = MuncipalPenalityPerDay * TotDays;

                decimal PrinciplePenality = PrincipleDutyDeficit * 2 / 100;
                decimal PrinciplePenalityPerDay = PrinciplePenality / 30;
                decimal TotalDaysPrinciplePenality = PrinciplePenalityPerDay * TotDays;

                decimal StampDutyPenality = StampDutyDeficit * 2 / 100;
                decimal StampDutyPenalityPerDay = StampDutyPenality / 30;
                decimal TotalDaysStampDutyPenality = StampDutyPenalityPerDay * TotDays;

                //double totalpen = GuideLineValDeficit + PenPerDay;
                //decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                //lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                decimal finalJanpadPenality = Math.Round(TotalDaysJanpadPenality, 2);

                if (finalJanpadPenality <= JanpadDutyDeficit)
                {
                    lblJanpadPenality.Text = finalJanpadPenality.ToString();
                }
                else
                {
                    lblJanpadPenality.Text = JanpadDutyDeficit.ToString();
                }

                decimal finalUpkarPenality = Math.Round(TotalDaysUpkarPenality, 2);

                if (finalUpkarPenality <= UpkarDutyDeficit)
                {
                    lblUpkarPenality.Text = finalUpkarPenality.ToString();
                }
                else
                {
                    lblUpkarPenality.Text = UpkarDutyDeficit.ToString();
                }

                decimal finalMuncipalPenality = Math.Round(TotalDaysMuncipalPenality, 2);

                if (finalMuncipalPenality <= MuncipalDutyDeficit)
                {
                    lblMunciplePenality.Text = finalMuncipalPenality.ToString();
                }
                else
                {
                    lblMunciplePenality.Text = MuncipalDutyDeficit.ToString();
                }

                decimal finalPrinciplePenality = Math.Round(TotalDaysPrinciplePenality, 2);

                if (finalPrinciplePenality <= PrincipleDutyDeficit)
                {
                    lblStampPenality.Text = finalPrinciplePenality.ToString();
                }
                else
                {
                    lblStampPenality.Text = PrincipleDutyDeficit.ToString();
                }

                decimal FinalStampDutyPenality = Math.Round(TotalDaysStampDutyPenality, 2);

                if (FinalStampDutyPenality <= StampDutyDeficit)
                {
                    lblTstampPenality.Text = FinalStampDutyPenality.ToString();
                }
                else
                {
                    lblTstampPenality.Text = StampDutyDeficit.ToString();
                }


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
                decimal StampPenality= Convert.ToDecimal(lblTstampPenality.Text);
                decimal GuidePenality = Convert.ToDecimal(lblGuidePenality.Text);
                TotalPenality = RegPenality + StampPenality + GuidePenality;
                TPenality.Text = TotalPenality.ToString();

            }
            catch (Exception ex)
            {

            }
        }
    }
}