using Newtonsoft.Json;
using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using SelectPdf;
using System.Globalization;
using RestSharp;
using System.Web.Script.Serialization;
using System.Net;
using System.Configuration;
using System.Net.Sockets;
using Newtonsoft.Json.Linq;

namespace CMS_Sampada.CoS
{
    public partial class AcceptRejectCases_details : System.Web.UI.Page
    {
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        string Whatsapp_URL = ConfigurationManager.AppSettings["WhatsappURL"];
        string WhatsApp_Userid = ConfigurationManager.AppSettings["WhatsAppUserid"];
        string WhatsApp_Pwd = ConfigurationManager.AppSettings["WhatsAppPwd"];
        string SmsUser = ConfigurationManager.AppSettings["SmsUser"];
        string SmsPassword = ConfigurationManager.AppSettings["SmsPassword"];
        string SmsSenderId = ConfigurationManager.AppSettings["SmsSenderId"];
        string secureKey = ConfigurationManager.AppSettings["SmsSecureKey"];

        ClsNewApplication objClsNewApplication = new ClsNewApplication();

        static ClsNewApplication objClsNewApplication_static = new ClsNewApplication();
        protected void Page_Load(object sender, EventArgs e)
        {
            int appid = Convert.ToInt32(Session["AppId"].ToString());
            string Appno = Session["AppNo"].ToString();
            hdTocan.Value = Session["Token"].ToString();
            int Flag = Convert.ToInt32(0);
            if (Session["Flag"] != null)
            {
                Flag = Convert.ToInt32(Session["Flag"].ToString());
            }

            try
            {
                if (!Page.IsPostBack)
                {


                    hdappid.Value = appid.ToString();
                    hdAppno.Value = Appno.ToString();
                    ViewState["AppId"] = appid;
                    ViewState["AppNo"] = Appno;
                    ViewState["Flag"] = Flag;
                    DataTable dt = objClsNewApplication.GetApplicationDetails_CoS(appid, Appno, Flag);

                    Session["DtApp"] = dt;
                    bindHead();
                    try
                    {
                        ddlHead1.Items.FindByText(dt.Rows[0]["HEADS_TYPE"].ToString()).Selected = true;
                        bindSection();

                        ddlSec1.Items.FindByText(dt.Rows[0]["SECTIONS_TYPE"].ToString()).Selected = true;

                    }
                    catch (Exception)
                    {

                    }
                    if (Session["AppId"] != null && Session["AppNo"] != null && Session["Flag"] != null)
                    {
                        //int appid = Convert.ToInt32(Session["AppId"].ToString());
                        //string Appno = Session["AppNo"].ToString();
                        //int Flag = Convert.ToInt32(Session["Flag"].ToString());

                        hdappid.Value = appid.ToString();
                        hdAppno.Value = Appno.ToString();
                        ViewState["AppId"] = appid;
                        ViewState["AppNo"] = Appno;
                        ViewState["Flag"] = Flag;

                        DataTable dsAppDetails = new DataTable();
                        DataSet dsPartyDetails = new DataSet();
                        DataSet dsDocDetails = new DataSet();
                        DataSet dsPropertyDetails = new DataSet();


                        //DataSet dsPartyDetails_Pdf = new DataSet();
                        //DataSet dsPropertyDetails_Pdf = new DataSet();



                        //LinkButton lnk = (LinkButton)sender;

                        Session["AppID"] = appid;
                        Session["Appno"] = Appno;
                        Session["ProImpoundDate"] = lblProImpoundDt.Text;
                        dsAppDetails = Session["DtApp"] as DataTable;



                        if (dsAppDetails != null)
                        {
                            if (dsAppDetails.Rows.Count > 0)
                            {
                                //hdnDistrict.Value = dt.Rows[0]["District"].ToString();
                                //lblReasonImpound.Text = dr[2].ToString();
                                lblReasonImpound.Text = dsAppDetails.Rows[0]["ImpoundingReasons_En"].ToString();
                                lblPropertyRegNoInitIdEStampId.Text = dsAppDetails.Rows[0]["Reg_Initi_Estammp_IDs"].ToString();

                                lblProRegDt.Text = dsAppDetails.Rows[0]["Property_Reg_Date"].ToString();

                                //lblReasonImpound.Text = dsAppDetails.Rows[0]["ImpoundingReasons_En"].ToString();
                                lblDateofPresent.Text = dsAppDetails.Rows[0]["DateOfPresentation"].ToString();
                                lblDateofExecution.Text = dsAppDetails.Rows[0]["DateOfExecution"].ToString();



                                lblNatureDoc.Text = dsAppDetails.Rows[0]["NatureOfParty_Docs"].ToString();
                                lblNatureDocRegOff.Text = dsAppDetails.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                                lblNatureDocRemark.Text = dsAppDetails.Rows[0]["NatureOfDocuments_Remarks"].ToString();


                                lblConsidProperty.Text = dsAppDetails.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                lblConsidPropertyRegOff.Text = dsAppDetails.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                lblConsidPropertyRemark.Text = dsAppDetails.Rows[0]["Property_Consider_Remark"].ToString();


                                lblGuideValue.Text = dsAppDetails.Rows[0]["Guideline_PropertyValue"].ToString();
                                lblGuideValueRegOff.Text = dsAppDetails.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                                lblGuideValueRegDefcit.Text = dsAppDetails.Rows[0]["Deficit_GuideLineValue"].ToString();
                                //lblGuideValuePenality.Text = dsAppDetails.Rows[0]["Penality_GuidelineValue"].ToString();
                                lblGuideValueRemark.Text = dsAppDetails.Rows[0]["Guideline_value_Remark"].ToString();


                                lblStampDutyClassJanpad.Text = dsAppDetails.Rows[0]["Janpad_SD"].ToString();
                                lblStampDutyClassUpkar.Text = dsAppDetails.Rows[0]["Upkar"].ToString();
                                lblStampDutyClassMuncipal.Text = dsAppDetails.Rows[0]["Municipal_StampDuty"].ToString();
                                lblStampDutyClassPrinciple.Text = dsAppDetails.Rows[0]["Principal_StampDuty"].ToString();

                                lblTotalStampDuty.Text = dsAppDetails.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                lblTotalStampDutyRO.Text = dsAppDetails.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();
                                //lblStampDutyExemptedAmt.Text = dr[55].ToString();
                                lblStampDutyExemptedAmt.Text = dsAppDetails.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();

                                lblAlreadyPaidDuty.Text = dsAppDetails.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                lblAlreadyPaidDutyRO.Text = dsAppDetails.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();

                                lblProClassJanpad.Text = dsAppDetails.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                lblProClassUpkar.Text = dsAppDetails.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                lblProClassMuncipal.Text = dsAppDetails.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                lblProClassPrinciple.Text = dsAppDetails.Rows[0]["Principal_PropsedStmpDuty"].ToString();

                                //lblProExemptedAmt.Text = dsAppDetails.Rows[0]["Janpad_SD"].ToString();56
                                lblProExemptedAmt.Text = dsAppDetails.Rows[0]["SD_EXEMPTEDAMOUNT_BYRO"].ToString();


                                lblDeficitJanpad.Text = dsAppDetails.Rows[0]["Deficit_Janpad"].ToString();
                                lblDeficitUpkar.Text = dsAppDetails.Rows[0]["Deficit_Upkar"].ToString();
                                lblDeficitMuncipal.Text = dsAppDetails.Rows[0]["Deficit_Muncipal"].ToString();
                                lblDeficitPrinciple.Text = dsAppDetails.Rows[0]["Deficit_Principal"].ToString();


                                lblStamDuty.Text = dsAppDetails.Rows[0]["StampDuty"].ToString();
                                lblProRecStmapDuty.Text = dsAppDetails.Rows[0]["Proposed_StampDuty"].ToString();

                                lblRegiExemptedAmt.Text = dsAppDetails.Rows[0]["REG_FEES_EXEMPTED_AMT_BYPARTY"].ToString();

                                lblEXRegParty.Text = dsAppDetails.Rows[0]["REG_FEES_EXEMPTED_AMT_BYPARTY"].ToString();

                                lblProRegiExemptedAmt.Text = dsAppDetails.Rows[0]["REG_FEES_EXEMPTED_AMT_BYRO"].ToString();

                                lblEXRegSR.Text = dsAppDetails.Rows[0]["REG_FEES_EXEMPTED_AMT_BYRO"].ToString();

                                lblAlreadyPaidRegFee.Text = dsAppDetails.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                lblAlreadyPaidRegFeeRO.Text = dsAppDetails.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();

                                lblRegFee.Text = dsAppDetails.Rows[0]["REG_FEE"].ToString();

                                lblNetRegParty.Text = dsAppDetails.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();

                                lblRecoverRegfee.Text = dsAppDetails.Rows[0]["PROPOSEDRECOVERABLEREGFEE"].ToString();

                                lblNetRegSR.Text = dsAppDetails.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYRO"].ToString();

                                lblStampDutyExempted_P2.Text = dsAppDetails.Rows[0]["SD_EXEMPTEDAMOUNT_BYPARTY"].ToString();
                                lblProStampDuty_P2.Text = dsAppDetails.Rows[0]["SD_EXEMPTEDAMOUNT_BYRO"].ToString();







                                lblDeficitDuty.Text = dsAppDetails.Rows[0]["DeficitDuty"].ToString();
                                //lblStampDutyPenality.Text = dsAppDetails.Rows[0]["Penality_StampDuty"].ToString();
                                lblStampDutyRemark.Text = dsAppDetails.Rows[0]["Stamp_Duty_Remark"].ToString();



                                //lblRegiFee.Text = dsAppDetails.Rows[0]["PropertyValueAtRegTime"].ToString();
                                lblRegiFee.Text = dsAppDetails.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYPARTY"].ToString();
                                lblProRegiFee.Text = dsAppDetails.Rows[0]["REG_FEES_WO_EXEMP_AMT_BYRO"].ToString();
                                //lblRegiExemptedAmt.Text = dsAppDetails.Rows[0]["DeficitPropertyValue"].ToString();
                                //lblProRegiExemptedAmt.Text = dsAppDetails.Rows[0]["Penality_Property_Mrktvalue"].ToString();





                                lblRegFeeRemark.Text = dsAppDetails.Rows[0]["Reg_Fees_Remark"].ToString();


                                lblSROID.Text = dsAppDetails.Rows[0]["SRO_ID"].ToString();
                                lblSRName.Text = dsAppDetails.Rows[0]["Designation"].ToString();
                                lblSROName.Text = dsAppDetails.Rows[0]["Office"].ToString();
                                lblProposalId.Text = dsAppDetails.Rows[0]["Proposal_No"].ToString();
                                lblProposalDate.Text = dsAppDetails.Rows[0]["Impound_Date"].ToString();//comment in sp
                                lblProImpoundDt.Text = dsAppDetails.Rows[0]["Impound_Date"].ToString();
                                lblProposalIdHeading.Text = dsAppDetails.Rows[0]["Proposal_No"].ToString();
                                Session["ProposalID"] = lblProposalIdHeading.Text;

                                lblHeadbySR.Text = dsAppDetails.Rows[0]["Heads_Type"].ToString();
                                string HeadValue = lblHeadbySR.Text;
                                //Session["head"] = lblHeadbySR.Text;
                                lblSecbySR.Text = dsAppDetails.Rows[0]["Sections_Type"].ToString();
                                //Session["section"] = lblSecbySR.Text;
                                lblSRComments.Text = dsAppDetails.Rows[0]["Comments"].ToString();


                                //First Formate Start

                                lblProposalId_P.Text = dsAppDetails.Rows[0]["Proposal_No"].ToString();

                                lblPropertyRegNoInitIdEStampId_P.Text = dsAppDetails.Rows[0]["Reg_Initi_Estammp_IDs"].ToString();

                                lblDateofPresent_P.Text = dsAppDetails.Rows[0]["DateOfPresentation"].ToString();
                                lblDateofExecution_P.Text = dsAppDetails.Rows[0]["DateOfExecution"].ToString();


                                lblNatureDoc_P.Text = dsAppDetails.Rows[0]["NatureOfParty_Docs"].ToString();
                                lblNatureDocRegOff_P.Text = dsAppDetails.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                                lblStamDuty_P.Text = dsAppDetails.Rows[0]["StampDuty"].ToString();
                                lblProRecStmapDuty_P.Text = dsAppDetails.Rows[0]["Proposed_StampDuty"].ToString();
                                //lblProRecStmapDuty_P.Text = dsAppDetails.Rows[0]["Office"].ToString();
                                lblDeficitDuty_P.Text = dsAppDetails.Rows[0]["DeficitDuty"].ToString();
                                lblSRName_P.Text = dsAppDetails.Rows[0]["Designation"].ToString();
                                lblSROName_P.Text = dsAppDetails.Rows[0]["Office"].ToString();
                                lblSRComments_P.Text = dsAppDetails.Rows[0]["Comments"].ToString();
                                //First Formate End


                                //Second formate Start
                                lblPropertyRegNoInitIdEStampId_P2.Text = dsAppDetails.Rows[0]["Reg_Initi_Estammp_IDs"].ToString();

                                lblReasonImpound_P2.Text = dsAppDetails.Rows[0]["ImpoundingReasons_En"].ToString();


                                //lblPropertyRegIdRegOff.Text = dr[3].ToString();
                                lblProRegDt_P2.Text = dsAppDetails.Rows[0]["Property_Reg_Date"].ToString();
                                //lblProRegDtRegOff.Text = dr[4].ToString();

                                lblDateofPresent_P2.Text = dsAppDetails.Rows[0]["DateOfPresentation"].ToString();
                                lblDateofExecution_P2.Text = dsAppDetails.Rows[0]["DateOfExecution"].ToString();


                                lblNatureDoc.Text = dsAppDetails.Rows[0]["NatureOfParty_Docs"].ToString();
                                lblNatureDoc_P2.Text = dsAppDetails.Rows[0]["NatureOfParty_Docs"].ToString();
                                lblNatureDocRegOff.Text = dsAppDetails.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                                lblNatureDocRegOff_P2.Text = dsAppDetails.Rows[0]["NatureOfProposal_DocsRO"].ToString();
                                lblNatureDocRemark.Text = dsAppDetails.Rows[0]["NatureOfDocuments_Remarks"].ToString();

                                lblConsidProperty.Text = dsAppDetails.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                lblConsidPropertyRegOff.Text = dsAppDetails.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                lblConsidPropertyRemark.Text = dsAppDetails.Rows[0]["Property_Consider_Remark"].ToString();

                                lblConsidProperty_P2.Text = dsAppDetails.Rows[0]["ConsiderationValueOfProperty"].ToString();
                                lblConsidPropertyRegOff_P2.Text = dsAppDetails.Rows[0]["proposedConsiderationValueOfProperty"].ToString();
                                //lblConsidPropertyDeficit.Text = dsAppDetails.Rows[0]["NatureOfParty_Docs"].ToString();
                                lblConsidPropertyRemark_P2.Text = dsAppDetails.Rows[0]["Property_Consider_Remark"].ToString();

                                lblStampDutyClassJanpad.Text = dsAppDetails.Rows[0]["Janpad_SD"].ToString();
                                lblStampDutyClassUpkar.Text = dsAppDetails.Rows[0]["Upkar"].ToString();
                                lblStampDutyClassMuncipal.Text = dsAppDetails.Rows[0]["Municipal_StampDuty"].ToString();
                                lblStampDutyClassPrinciple.Text = dsAppDetails.Rows[0]["Principal_StampDuty"].ToString();

                                lblTotalStampDuty_p2.Text = dsAppDetails.Rows[0]["TOTAL_STAMPDUTY_BY_PARTY"].ToString();
                                lblTotalStampDutyRO_p2.Text = dsAppDetails.Rows[0]["TOTAL_STAMPDUTY_BY_RO"].ToString();

                                lblAlreadyPaidDuty_P2.Text = dsAppDetails.Rows[0]["ALREDY_PAID_DUTY_BYPARTY"].ToString();
                                lblAlreadyPaidDutyRO_P2.Text = dsAppDetails.Rows[0]["ALREDY_PAID_DUTY_BYRO"].ToString();


                                lblGuideValue_P2.Text = dsAppDetails.Rows[0]["Guideline_PropertyValue"].ToString();
                                lblGuideValueRegOff_P2.Text = dsAppDetails.Rows[0]["Guideline_PropValue_ByRegisOfficer"].ToString();
                                lblGuideValueRegDefcit_P2.Text = dsAppDetails.Rows[0]["Deficit_GuideLineValue"].ToString();
                                //lblGuideValuePenality_P2.Text = dsAppDetails.Rows[0]["Penality_GuidelineValue"].ToString();
                                lblGuideValueRemark_P2.Text = dsAppDetails.Rows[0]["Guideline_value_Remark"].ToString();

                                lblStampDutyClassJanpad_P2.Text = dsAppDetails.Rows[0]["Janpad_SD"].ToString();
                                lblStampDutyClassUpkar_P2.Text = dsAppDetails.Rows[0]["Upkar"].ToString();
                                lblStampDutyClassMuncipal_P2.Text = dsAppDetails.Rows[0]["Municipal_StampDuty"].ToString();
                                lblStampDutyClassPrinciple_P2.Text = dsAppDetails.Rows[0]["Principal_StampDuty"].ToString();

                                lblProClassJanpad_P2.Text = dsAppDetails.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                                lblProClassUpkar_P2.Text = dsAppDetails.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                                lblProClassMuncipal_P2.Text = dsAppDetails.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                                lblProClassPrinciple_P2.Text = dsAppDetails.Rows[0]["Principal_PropsedStmpDuty"].ToString();

                                lblDeficitJanpad_P2.Text = dsAppDetails.Rows[0]["Deficit_Janpad"].ToString();
                                lblDeficitUpkar_P2.Text = dsAppDetails.Rows[0]["Deficit_Upkar"].ToString();
                                lblDeficitMuncipal_P2.Text = dsAppDetails.Rows[0]["Deficit_Muncipal"].ToString();
                                lblDeficitPrinciple_P2.Text = dsAppDetails.Rows[0]["Deficit_Principal"].ToString();



                                lblStamDuty_P2.Text = dsAppDetails.Rows[0]["StampDuty"].ToString();
                                lblProRecStmapDuty_P2.Text = dsAppDetails.Rows[0]["Proposed_StampDuty"].ToString();


                                lblDeficitDuty_P2.Text = dsAppDetails.Rows[0]["DeficitDuty"].ToString();
                                //lblStampDutyPenality_P2.Text = dsAppDetails.Rows[0]["Penality_StampDuty"].ToString();
                                lblStampDutyRemark_P2.Text = dsAppDetails.Rows[0]["Stamp_Duty_Remark"].ToString();

                                lblRegFee_P2.Text = dsAppDetails.Rows[0]["Reg_Fee"].ToString();
                                lblRecoverRegfee_P2.Text = dsAppDetails.Rows[0]["ProposedRecoverableRegFee"].ToString();
                                lblDeficitRegFee_P2.Text = dsAppDetails.Rows[0]["DeficitRegistrationFees"].ToString();
                                //lblRegFeePenality_P2.Text = dsAppDetails.Rows[0]["Penality_RegsFees"].ToString();
                                //lblRegFeePenality_P3.Text = dsAppDetails.Rows[0]["Penality_RegsFees"].ToString();
                                lblRegFeeRemark_P2.Text = dsAppDetails.Rows[0]["Reg_Fees_Remark"].ToString();

                                //lblAlreadyPaidRegFee_P2.Text = dsAppDetails.Rows[0]["ALREDY_PAID_REG_FEE_BYPARTY"].ToString();
                                // lblAlreadyPaidRegFeeRO_P2.Text = dsAppDetails.Rows[0]["ALREDY_PAID_REG_FEE_BYRO"].ToString();


                                lblSROID_P2.Text = dsAppDetails.Rows[0]["SRO_ID"].ToString();
                                lblSRName_P2.Text = dsAppDetails.Rows[0]["Designation"].ToString();
                                lblSROName_P2.Text = dsAppDetails.Rows[0]["Office"].ToString();
                                lblProposalId_P2.Text = dsAppDetails.Rows[0]["Proposal_No"].ToString();
                                lblProposalDate_P2.Text = dsAppDetails.Rows[0]["Impound_Date"].ToString();
                                //txtSRComments.Text = dr[19].ToString();
                                lblSRComments_P2.Text = dsAppDetails.Rows[0]["Comments"].ToString();

                                Session["ProImpoundDate"] = lblProImpoundDt.Text;
                                Session["IMPOUND_DATE"] = lblProImpoundDt.Text;

                                if (lblRegFee.Text != "")
                                {

                                    if (lblRecoverRegfee.Text != "")
                                    {
                                        double NetRegParty = Convert.ToDouble(lblRegFee.Text);
                                        double NetRegRO = Convert.ToDouble(lblRecoverRegfee.Text);

                                        double NetDeficitReg = NetRegRO - NetRegParty;

                                        lblDeficitRegFee.Text = NetDeficitReg.ToString();

                                        lblNetRegDeficit.Text = NetDeficitReg.ToString();
                                    }



                                }



                                int Depart_ID = Convert.ToInt32(dsAppDetails.Rows[0]["Department_ID"].ToString());

                                //int Depart_ID = Convert.ToInt32(dr[51].ToString());


                                //string PropertyRegNo = dr[53].ToString();
                                //string InitiationId = dr[54].ToString();

                                if (Depart_ID == 1)
                                {
                                    string PropertyRegNo = dsAppDetails.Rows[0]["Property_RegNO"].ToString();
                                    string InitiationId = dsAppDetails.Rows[0]["Initiation_ID"].ToString();
                                    //lblPropertyRegId.Text = dr[7].ToString();
                                    if (PropertyRegNo != "")
                                    {
                                        lblRegInitEStampID.Text = "Document Registration Number";
                                        lblRegInitEStampID_P.Text = "Document Registration Number";
                                        lblRegInitEStampID_P2.Text = "दस्तावेज़ पंजीयन नंबर ";

                                        //lblDepName.Text = "Proposal ID";
                                        lblRegInitDate.Text = "Date of Registration";
                                        lblRegInitDate_P2.Text = "पंजीयन तिथि";
                                        lblOfficeName.Text = "Comments";
                                        lblOfficeName_P2.Text = "टिप्पणी";
                                        lblHeading.Text = "Sub Registrar Details";
                                        lblHeading_P2.Text = "उप रजिस्ट्रार विवरण";
                                        pnlAuditIdDate.Visible = false;
                                        pnlAuditIdandDate.Visible = false;
                                        lblAuditDt.Visible = false;
                                    }
                                    else
                                    {
                                        if (InitiationId != "")
                                        {
                                            lblRegInitEStampID.Text = "Initiation ID";
                                            lblRegInitEStampID_P.Text = "Initiation ID";
                                            lblRegInitEStampID_P2.Text = "प्रारभ आई डी";
                                            pnlRegInitDate.Visible = false;
                                            pnlRegInitDate_P2.Visible = false;
                                            pnlRegNoInitDate.Visible = false;
                                            pnlRegNoInitDate_P2.Visible = false;
                                            //lblPropertyRegId.Text = dr[7].ToString();
                                            //lblRegInitDate.Text = "Date of Intitation";
                                            lblHeading.Text = "Sub Registrar Details";
                                            lblHeading_P2.Text = "उप रजिस्ट्रार विवरण";
                                            lblOfficeName.Text = "Comments";
                                            lblOfficeName_P2.Text = "टिप्पणी";
                                            //lblRegFee.Text = "";
                                            //lblRegFee.Text = "NA";
                                            //lblRecoverRegfee.Text = "";
                                            //lblDeficitRegFee.Text = "";
                                            //lblRegFeePenality.Text = "NA";
                                            lblRegFeeRemark.Text = "";
                                        }
                                        else
                                        {
                                            lblRegInitEStampID.Text = "E-Stamp ID";
                                            lblRegInitEStampID_P.Text = "E-Stamp ID";
                                            lblRegInitEStampID_P2.Text = "ई-स्टाम्प आई डी";
                                            lblHeading.Text = "Sub Registrar Details";
                                            pnlRegInitDate.Visible = false;
                                            pnlRegInitDate_P2.Visible = false;
                                            pnlRegNoInitDate.Visible = false;
                                            pnlRegNoInitDate_P2.Visible = false;

                                            lblDateofExecution.Text = "NA";
                                            lblDateofPresent.Text = "NA";

                                            //lblRegFee.Text = "";
                                            //lblRegFee.Text = "NA";
                                            //lblRecoverRegfee.Text = "";
                                            //lblDeficitRegFee.Text = "";
                                            //lblRegFeePenality.Text = "NA";
                                            lblRegFeeRemark.Text = "";
                                            lblOfficeName.Text = "Comments";
                                            lblOfficeName_P2.Text = "टिप्पणी";
                                        }
                                    }
                                }
                                if (Depart_ID == 2)
                                {

                                    string InternalAuditID = dsAppDetails.Rows[0]["InternalAudit_ID"].ToString();
                                    string AuditDate = dsAppDetails.Rows[0]["Audit_Date"].ToString();
                                    //lblDepName.Text = "Audit ID";
                                    lblRegInitEStampID.Text = "Document Registration Number";

                                    lblRegInitEStampID_P.Text = "Document Registration Number";
                                    lblRegInitEStampID_P2.Text = "दस्तावेज़ पंजीयन नंबर";


                                    lblRegInitDate.Text = "Date of Registry";
                                    lblRegInitDate_P2.Text = "पंजीयन तिथि";
                                    lblOfficeName.Text = "Comments";
                                    lblOfficeName_P2.Text = "टिप्पणी";
                                    lblHeading.Text = "Sub Registrar / Audit Details ";
                                    lblHeading_P2.Text = "उप रजिस्ट्रार विवरण";
                                    //txtComment.Text = (dr[26].ToString());
                                    pnlAuditIdDate.Visible = true;
                                    pnlAuditIdDate_P2.Visible = true;
                                    pnlAuditIdandDate.Visible = true;
                                    pnlAuditIdandDate_P2.Visible = true;
                                    //txtAuditDt.Text = Convert.ToDateTime(dr[21].ToString()).ToString("dd-MM-yyyy");

                                    lblAuditId.Text = dsAppDetails.Rows[0]["InternalAudit_ID"].ToString();
                                    //lblAuditDt.Text = Convert.ToDateTime(dsAppDetails.Rows[0]["Audit_Date"].ToString()).ToString("dd-MM-yyyy");
                                    lblAuditDt.Text = dsAppDetails.Rows[0]["Audit_Date"].ToString();

                                    lblAuditId_P2.Text = dsAppDetails.Rows[0]["InternalAudit_ID"].ToString();
                                    lblAuditDt_P2.Text = dsAppDetails.Rows[0]["Audit_Date"].ToString();

                                    //lblAuditId.Text = dr[53].ToString();
                                    //lblAuditDt.Text = Convert.ToDateTime(dr[54].ToString()).ToString("dd-MM-yyyy");

                                    //ddlHead1.Items.FindByText(dsAppDetails.Rows[0]["HEADS_TYPE"].ToString()).Selected = true;
                                    //bindSection();

                                    //ddlSec1.Items.FindByText(dsAppDetails.Rows[0]["SECTIONS_TYPE"].ToString()).Selected = true;
                                    //ddlHead1.SelectedValue = dsAppDetails.Rows[0]["HEADS_ID"].ToString();
                                }

                            }

                        }


                        //DataSet ds = new DataSet();
                        dsPartyDetails = objClsNewApplication.GetDocDetails_CoS_ToC(appid, Appno);
                        //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(appid, Appno);

                        DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(appid, Appno);
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


                        dsPartyDetails = objClsNewApplication.GetPartyDetails_CoS(appid, Appno);
                        if (dsPartyDetails != null)
                        {
                            if (dsPartyDetails.Tables.Count > 0)
                            {

                                if (dsPartyDetails.Tables[0].Rows.Count > 0)
                                {
                                    grdPartyDetails.DataSource = dsPartyDetails;
                                    grdPartyDetails.DataBind();

                                }

                            }
                        }

                        dsPropertyDetails = objClsNewApplication.GetPropertyDetails_CoS(appid, Appno);
                        if (dsPropertyDetails != null)
                        {
                            if (dsPropertyDetails.Tables.Count > 0)
                            {

                                if (dsPropertyDetails.Tables[0].Rows.Count > 0)
                                {
                                    GVPropertyDetail.DataSource = dsPropertyDetails;
                                    GVPropertyDetail.DataBind();

                                }

                            }
                        }


                        dsDocDetails = objClsNewApplication.GetDocDetails_CoS(appid, Appno);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {

                                    //GdListOfDoc.DataSource = dsDocDetails;
                                    //GdListOfDoc.DataBind();


                                    //string fileName = dsDocDetails.Tables[0].Rows[0]["File_Path"].ToString();
                                    //AlldocPath.Src = "../../../Documents/" + fileName;
                                }
                            }
                        }

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
                                RecentProposalDoc.Attributes["src"] = encodedPdfData;
                            }
                            else
                            {
                                RecentProposalDoc.Visible = false;
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
                            }
                            else
                            {
                                RecentAttachedDoc.Visible = false;
                                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                            }

                        }



                    }

                    GuideLineValuePenalityCalculation();
                    StampDutyPenalityCalculation();
                    RegistyPenalityCalculation();
                }
                DataSet dsPartyDetails_Pdf = objClsNewApplication.GetPartyDetails_PrintDoc(appid, Appno);
                if (dsPartyDetails_Pdf != null)
                {
                    if (dsPartyDetails_Pdf.Tables.Count > 0)
                    {

                        if (dsPartyDetails_Pdf.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt2 = dsPartyDetails_Pdf.Tables[0];
                            StringBuilder html = new StringBuilder();
                            html.Append("<table style='width: 100%; margin-left: 20px; margin-top: -40px; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2 %; '>");
                            html.Append("<tr>");
                            foreach (DataColumn column in dt2.Columns)
                            {
                                html.Append("<th style='border: 1px solid black; padding: 5px 15px;'>");
                                html.Append(column.ColumnName);
                                html.Append("</th>");
                            }
                            html.Append("</tr>");
                            foreach (DataRow row in dt2.Rows)
                            {
                                html.Append("<tr>");
                                foreach (DataColumn column in dt2.Columns)
                                {
                                    html.Append("<td style='border: 1px solid black; padding: 5px 15px;'>");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }
                                html.Append("</tr>");
                            }
                            html.Append("</table>");
                            PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                            PlaceHolder2.Controls.Add(new Literal { Text = html.ToString() });
                            //grdPartyDetails.DataSource = dsPartyDetails;
                            //grdPartyDetails.DataBind();
                        }

                    }
                }

                //dsPartyDetails = objClsNewApplication.GetPartyDetails_CoS(appid, Appno);
                //dsPartyDetails_Pdf = objClsNewApplication.GetPartyDetails_PrintPdf(appid, Appno); 
                dsPartyDetails_Pdf = objClsNewApplication.GetPartyDetails_PrintPdf_Hindi(appid, Appno);
                if (dsPartyDetails_Pdf != null)
                {
                    if (dsPartyDetails_Pdf.Tables.Count > 0)
                    {

                        if (dsPartyDetails_Pdf.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt3 = dsPartyDetails_Pdf.Tables[0];
                            StringBuilder html = new StringBuilder();
                            html.Append("<table style='width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2 %; '>");
                            html.Append("<tr>");
                            foreach (DataColumn column in dt3.Columns)
                            {
                                html.Append("<th style='border: 1px solid black; padding: 5px 15px;'>");
                                html.Append(column.ColumnName);
                                html.Append("</th>");
                            }
                            html.Append("</tr>");
                            foreach (DataRow row in dt3.Rows)
                            {
                                html.Append("<tr>");
                                foreach (DataColumn column in dt3.Columns)
                                {
                                    html.Append("<td style='border: 1px solid black; padding: 5px 15px;'>");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }
                                html.Append("</tr>");
                            }
                            html.Append("</table>");
                            PlaceHolder3.Controls.Add(new Literal { Text = html.ToString() });
                            //grdPartyDetails.DataSource = dsPartyDetails;
                            //grdPartyDetails.DataBind();
                        }

                    }
                }


                //DataSet    dsPropertyDetails_Pdf = objClsNewApplication.GetPropertyDetailsForPdf_CoS(appid, Appno);  
                DataSet dsPropertyDetails_Pdf = objClsNewApplication.GetPropertyDetailsForPdf_CoS_Hindi(appid, Appno);
                if (dsPropertyDetails_Pdf != null)
                {
                    if (dsPropertyDetails_Pdf.Tables.Count > 0)
                    {

                        if (dsPropertyDetails_Pdf.Tables[0].Rows.Count > 0)
                        {
                            DataTable dt4 = dsPropertyDetails_Pdf.Tables[0];
                            StringBuilder html = new StringBuilder();
                            html.Append("<table style='width: 100%; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2 %; '>");
                            html.Append("<tr style='border: 1px solid black; padding: 5px 15px;'>");
                            foreach (DataColumn column in dt4.Columns)
                            {
                                html.Append("<th style='border: 1px solid black; padding: 5px 15px;'>");
                                html.Append(column.ColumnName);
                                html.Append("</th>");
                            }
                            html.Append("</tr>");
                            foreach (DataRow row in dt4.Rows)
                            {
                                html.Append("<tr>");
                                foreach (DataColumn column in dt4.Columns)
                                {
                                    html.Append("<td style='border: 1px solid black; padding: 5px 15px;'>");
                                    html.Append(row[column.ColumnName]);
                                    html.Append("</td>");
                                }
                                html.Append("</tr>");
                            }
                            html.Append("</table>");
                            PlaceHolder4.Controls.Add(new Literal { Text = html.ToString() });
                            //grdPartyDetails.DataSource = dsPartyDetails;
                            //grdPartyDetails.DataBind();
                        }

                    }
                }

            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");

                //throw;
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
                if (response.ErrorException != null)
                {
                    //Result = response.ErrorException.InnerException.Message;
                    //string responseStatusDescription = "";
                    //string response_ResponseUri_AbsoluteUri = "";

                    //if (response.StatusDescription != null)
                    //{
                    //    responseStatusDescription = response.StatusDescription;
                    //}
                    //if (response.ResponseUri != null)
                    //{
                    //    response_ResponseUri_AbsoluteUri = response.ResponseUri.AbsoluteUri.ToString();
                    //}

                    if (response.ErrorMessage != "")
                    {
                        Result = response.ErrorMessage;
                        objClsNewApplication_static.InsertExeption("Index_Tab_ErrorException.Message = " + response.ErrorMessage + ",StatusDescription = " + response.StatusDescription, response.ResponseUri.AbsoluteUri.ToString(), "AcceptRejectCases_details", GetLocalIPAddress());
                    }

                    //objClsNewApplication.InsertExeption("All_Tab_ErrorException.Message = " + Result + ",StatusDescription = " + responseStatusDescription, response_ResponseUri_AbsoluteUri, "AcceptRejectCases_details", GetLocalIPAddress());
                }
                // Generate a unique file name

                //string encodedPdfData = "data:application/pdf;base64," + base64 + "";


            }
            catch (Exception ex)
            {
                objClsNewApplication_static.InsertExeption(ex.Message, Request.Url.ToString(), ex.TargetSite.Name, Request.UserHostAddress);
                Response.Write(ex.Message);

            }
            return base64;

        }

        //protected string Comsumedata(string DocumentType, int RegID)
        //{
        //    string base64 = "";
        //    try
        //    {

        //    //var Token = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJtYW5vai5kcm8uaGFyZGEuYXBwcm92ZXJAbXAuZ292LmluIiwiaXAiOiIxMDMuMTYwLjQ5LjEzNSIsInVzZXJBZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS8xMjQuMC4wLjAgU2FmYXJpLzUzNy4zNiIsImV4cCI6MTcxMzg4MDE0MCwiaWF0IjoxNzEzODc2NTQwfQ.J8LUONwCcxtSz8nu0PQEisn9WL5iZjTfqoVK-8EWEl29T7TqnB9TOnLQqIaFW9OSGtJYOi8DIbU7mhB-6vh-0OdHuiUcs8GbYVUwZk8gYT6j2JU2ON9k-XLaBb0FiakKLnpLEOPe2t4Rr2KGBBuZXSop4Uk47RG6907OAh9ZcRy-aTNC82MHCYL5sgaUlYO9ATUMWgnR1yihrfoKOqzFJ2S92vk_vac9FcFfABtYfaK2VLngH2YxrZYmQaWaWu58Q_UV4zH3n088mdtSrReqJcVzk7CpTrz09yTcrNHbN7peD_NLnytSbkoLDgVA8y80my9o2Qm9pkFlRF_Krtt-7g";
        //    ServicePointManager.Expect100Continue = true;
        //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //    //var client = new RestClient("https://ersuat2.mp.gov.in/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");     //UAT

        //    var client = new RestClient(RegProposalAttDocument_url + DocumentType + "/" + RegID + "");        //PROD
        //    //var client = new RestClient("RegProposalAttDocument_url" + DocumentType + "/" + RegID + "");
        //    var request = new RestRequest(Method.POST);
        //    //request.AddHeader("Content-Type", "application/json");
        //    //request.AddHeader("Authorization", Token);
        //    request.AddHeader("Authorization", Session["Token"].ToString());
        //    //request.AddHeader("Authorization", tocan);
        //    //request.AddParameter("userid", "1");
        //    request.RequestFormat = DataFormat.Json;
        //    IRestResponse response = client.Execute(request);
        //    string Result = response.Content;
        //    //string base64 = "";
        //    if (Result != "")
        //    {
        //        JavaScriptSerializer oJS = new JavaScriptSerializer();
        //        oJS.MaxJsonLength = 2147483647;
        //        ResonseA resonse = oJS.Deserialize<ResonseA>(Result);
        //        base64 = resonse.responseData;
        //            //bytes= Convert.FromBase64String(resonse.responseData);
        //       //Response.Write(base64);
        //    }
        //    else
        //    {
        //            //Response.Write("Data not found");
        //            base64 = null;
        //    }
        //    // Generate a unique file name

        //    //string encodedPdfData = "data:application/pdf;base64," + base64 + "";


        //    }
        //    catch(Exception ex)
        //    {
        //        Response.Write(ex.Message);
        //    }
        //    return base64;

        //}

        protected static string Api_Comsumedata(string DocumentType, int RegID, string Tocan)
        {
            //var tocan = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJtYW5vai5kcm8uaGFyZGEuYXBwcm92ZXJAbXAuZ292LmluIiwiaXAiOiIxMDMuMTYwLjQ5LjE0MCIsInVzZXJBZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS8xMjMuMC4wLjAgU2FmYXJpLzUzNy4zNiBFZGcvMTIzLjAuMC4wIiwiZXhwIjoxNzEzMDIxMTQzLCJpYXQiOjE3MTMwMTc1NDN9.jSBvREDB2fGh_Cwkp0qTgXDGQQY1pfn8SczD11U2_sETckblfy5ErPgwoHYHEYfEDMjRO_xQm5MnMAiWHnfDeTc_kTRnI9EMcvxnxIZ4t_d7LgAAEwB9eJmtosiHULc9SskJqfdnWxh5LbBOX4DBkpSrQF5dtICaT7uvKMPeSwhIx9Qi9P5g6gyFnIBlm4BXD-7VnE62K7G0l6RrNgzDc9vnfttrFSPC77tyw1mnH7tYCGgNQmL0AbKkWuCum0YEGhA9tYHFK9Fx7NQpBMai0fkJ3DEg0-T2RBMwGagiy8nL_c2BJ0OWBe87xAIQkm5f8EyTz-5OPDnNxb5S7cPI_Q";
            //var client = new RestClient("https://ersuat2.mp.gov.in/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");      //UAT
            //var client = new RestClient("https://10.115.204.23/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");       //PROD

            var client = new RestClient(RegProposalAttDocument_url + DocumentType + "/" + RegID + "");
            var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Tocan);

            //request.AddHeader("Authorization", tocan);
            //request.AddParameter("userid", "1");
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            string Result = response.Content;
            string base64 = "";
            if (Result != "")
            {
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                oJS.MaxJsonLength = 8000000;
                ResonseA resonse = oJS.Deserialize<ResonseA>(Result);
                base64 = resonse.responseData;
                //bytes= Convert.FromBase64String(resonse.responseData);
            }
            else
            {
                base64 = null;
            }
            if (response.ErrorMessage != "")
            {
                Result = response.ErrorMessage;
                objClsNewApplication_static.InsertExeption("Index_Tab_ErrorException.Message = " + response.ErrorMessage + ",StatusDescription = " + response.StatusDescription, response.ResponseUri.AbsoluteUri.ToString(), "AcceptRejectCases_details", GetLocalIPAddress());
            }
            // Generate a unique file name

            //string encodedPdfData = "data:application/pdf;base64," + base64 + "";
            return base64;

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

        private void bindHead()
        {
            try
            {
                DataSet ds = objClsNewApplication.Bind_Head(Convert.ToInt32(ViewState["AppId"].ToString()));
                ddlHead1.DataSource = ds.Tables[0];

                ddlHead1.DataTextField = "HEADS_TYPE";
                ddlHead1.DataValueField = "HEAD_ID";
                ddlHead1.DataBind();

            }
            catch (Exception)
            {

            }

        }

        private void bindSection()
        {
            try
            {
                DataSet ds = objClsNewApplication.Bind_Section(Convert.ToInt32(ViewState["AppId"].ToString()), ddlHead1.SelectedValue);
                ddlSec1.DataSource = ds.Tables[0];

                ddlSec1.DataTextField = "SECTIONS_TYPE";
                ddlSec1.DataValueField = "SECTIONS_ID";
                ddlSec1.DataBind();
                ddlSec1.Items.Insert(0, "-Select-");

            }
            catch (Exception)
            {

            }

        }

        protected void lnkPartyView_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = true;
            pnlPropertyDetails.Visible = false;
            DataTable dsPDetails = new DataTable();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            int Party_ID = Convert.ToInt32(grdPartyDetails.DataKeys[rowindex].Values["party_id"].ToString());
            Session["Party_ID"] = Party_ID;
            dsPDetails = objClsNewApplication.GetPartyByID(Party_ID);
            if (dsPDetails != null)
            {
                if (dsPDetails != null)
                {

                    if (dsPDetails.Rows.Count > 0)
                    {

                        lblTRANSACTION_THROUGH_POA.Text = dsPDetails.Rows[0]["TRANSACTION_THROUGH_POA"].ToString();
                        lblPartyFName.Text = dsPDetails.Rows[0]["PARTYFATHER_ORHUSBAND_ORGUARDIANNAME"].ToString();
                        lblWhatsappNo.Text = dsPDetails.Rows[0]["Whatsapp_No"].ToString();
                        lblPartyAddress.Text = dsPDetails.Rows[0]["Party_Address"].ToString();
                        lblPartyDist.Text = dsPDetails.Rows[0]["Party_District"].ToString();
                        lblOwnerName.Text = dsPDetails.Rows[0]["Owner_Name"].ToString();
                        lblOwnerMob.Text = dsPDetails.Rows[0]["OWNER_MOB_NO"].ToString();
                        lblOwnerWhatsapp.Text = dsPDetails.Rows[0]["OWNER_WHATSAPP_NO"].ToString();
                        lblOwnerEmail.Text = dsPDetails.Rows[0]["OWNER_EMAIL_ID"].ToString();
                        lblEmailID.Text = dsPDetails.Rows[0]["Email_Id"].ToString();






                    }

                }
            }
        }


        [WebMethod]
        public static string GetPath(int appid, string Appno)
        {
            string path = string.Empty;
            DataSet ds = new DataSet();
            ClsNewApplication objClsNewApplication = new ClsNewApplication();


            List<Doc> lstDoc = new List<Doc>();
            try
            {

                ds = objClsNewApplication.GetDocDetails_CoS(appid, Appno);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Doc doc = new Doc();

                                doc.FILE_PATH = ds.Tables[0].Rows[i]["FILE_PATH"].ToString();

                                lstDoc.Add(doc);

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }


            //string JSONString = string.Empty;
            path = JsonConvert.SerializeObject(lstDoc);
            return path;
        }

        [WebMethod]
        public static string DocFile(int appid, string Appno)
        {
            DataSet ds = new DataSet();
            ClsNewApplication objClsNewApplication = new ClsNewApplication();
            //appid = Convert.ToInt32(Request.QueryString["AppId"]);
            //Appno = Request.QueryString["AppNo"].ToString();
            List<Doc> lstDoc = new List<Doc>();
            try
            {

                ds = objClsNewApplication.GetDocDetails_CoS(appid, Appno);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {
                        if (ds.Tables[0].Rows.Count > 0)
                        {
                            for (int i = 0; i < ds.Tables[0].Rows.Count; i++)
                            {
                                Doc doc = new Doc();
                                doc.APPLICATION_NO = ds.Tables[0].Rows[i]["APPLICATION_NO"].ToString();
                                doc.DOC_NAME = ds.Tables[0].Rows[i]["DOC_NAME"].ToString();
                                doc.FILE_PATH = ds.Tables[0].Rows[i]["FILE_PATH"].ToString();
                                doc.PROPERTY_REGNO = ds.Tables[0].Rows[i]["PROPERTY_REGNO"].ToString();
                                lstDoc.Add(doc);

                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(lstDoc);
            return JSONString;
        }
        [WebMethod]
        public static string BindOrderSheet()
        {

            return "";

        }

        public class DocDetails
        {
            public List<Doc> lstDoc { get; set; }
        }

        public class Doc
        {
            public string FILE_PATH { get; set; }
            public string PROPERTY_REGNO { get; set; }
            public string APPLICATION_NO { get; set; }
            public string DOC_NAME { get; set; }
        }


        protected void lnkPropertyView_Click(object sender, EventArgs e)
        {
            pnlPropertyDetails.Visible = true;
            pnlPartyDetails.Visible = false;
            DataTable dsPDetails = new DataTable();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            int Property_ID = Convert.ToInt32(GVPropertyDetail.DataKeys[rowindex].Values["Property_ID"].ToString());
            Session["Property_ID"] = Property_ID;
            dsPDetails = objClsNewApplication.GetPropertyByID(Property_ID);


            if (dsPDetails != null)
            {
                if (dsPDetails != null)
                {

                    if (dsPDetails.Rows.Count > 0)
                    {
                        lblLandDiversion.Text = dsPDetails.Rows[0]["Land_Diversion"].ToString();
                        lblLandDiversionDetails.Text = dsPDetails.Rows[0]["Land_DiversionDeclaration"].ToString();
                        lblPropertyAddress.Text = dsPDetails.Rows[0]["PropertyAddress"].ToString();
                        //imgProperty.ImageUrl = dsPDetails.Rows[0]["Property_Image"].ToString();
                        //imgPropertyMap.ImageUrl = dsPDetails.Rows[0]["Property_Map"].ToString();


                    }

                }
            }

            //if (dsPDetails != null)
            //{
            //    if (dsPDetails.Tables.Count > 0)
            //    {

            //        if (dsPDetails.Tables[0].Rows.Count > 0)
            //        {
            //            foreach (DataRow dr in dsPDetails.Tables[0].Rows)
            //            {

            //                txtLandDiversion.Text = dr[1].ToString();
            //                txtLandDiversionDetails.Text = dr[2].ToString();
            //                txtPropertyAddress.Text = dr[3].ToString();
            //                imgProperty.ImageUrl = dr[4].ToString();
            //                imgPropertyMap.ImageUrl = dr[5].ToString();

            //                //txtIdentity.Text = dr[12].ToString();



            //            }
            //        }

            //    }
            //}

        }
        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {

                string FileName = FileNme;
                string ProposalPath = path;
                if (!Directory.Exists(ProposalPath))
                {
                    Directory.CreateDirectory(ProposalPath);
                }

                string htmlString = strhtml;
                string baseUrl = ProposalPath;
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

                using (var stream = File.Create(Path.Combine(ProposalPath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return ProposalPath + "/" + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }


        protected void generateFormateSecond_PDF()
        {
            Pcontent.Visible = true;
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //Control con = Pcontent.FindControl("pContent");
                // System.Web.UI.HtmlControls.HtmlTextArea conte = (System.Web.UI.HtmlControls.HtmlTextArea) con.FindControl("summernote");
                //System.Web.UI.HtmlControls.HtmlGenericControl para = (System.Web.UI.HtmlControls.HtmlGenericControl)con.FindControl("pContent");
                //para.InnerHtml = summernote.Value;
                Pcontent.RenderControl(iHTW);

                // string divContentInnerHtml = divContent.InnerText;

                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append(iHTW.InnerWriter.ToString());

                string Proposal_ID = Session["ProposalID"].ToString();
                //string FileNme = lblapp_id.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_RRCNoticeSheet.pdf";

                //ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/RRCNoticeSheet/", stringBuilder.ToString());
                //Session["RecentSheetPath"] = "~/RRCOrderSheet/" + FileNme;
                //ifPDFViewer.Src = "~/RRCNoticeSheet/" + FileNme;

                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_Formate2.pdf";
                string ProposalSheetPath = Server.MapPath("~/Proposal/" + Proposal_ID);
                ViewState["SecondFormate_Path"] = "~/Proposal/" + Proposal_ID + "/" + FileNme;

                string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, stringBuilder.ToString());
            }
            catch (Exception ex)
            {

            }
        }


        protected void Button1_Click(object sender, EventArgs e)
        {
            generateFormateFirst_PDF();
        }
        protected void Button2_Click(object sender, EventArgs e)
        {
            generateFormateSecond_PDF();
        }
        public void generateFormateFirst_PDF()
        {
            int appid = Convert.ToInt32(Session["AppId"]);
            string Appno = Session["AppNo"].ToString();
            //string appPath = HttpContext.Current.Request.ApplicationPath;
            //string path = Server.MapPath(appPath + "/Proposal/"+ lblProposalIdHeading.Text+ DateTime.Now.ToString("ddMMyyyyhhmmss")+".pdf");
            StringWriter iSW = new StringWriter();
            HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
            //summernote.RenderControl(iHTW);
            //string divCon = summernote.Value;
            StringWriter sw = new StringWriter();
            HtmlTextWriter hw = new HtmlTextWriter(sw);
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("<div style='width: 90%;  margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 40px 30px 30px 30px; margin-top: 0px;'>");
            stringBuilder.Append("<h2 style='font-size: 32px; margin: 0;'>");
            stringBuilder.Append("<label style='font: bold'>" + lblOfficeHeading.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<h2 style='font-size: 24px; margin: 10px;'>Form-I ");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<h2 style='font-size: 22px; margin-top: 10px;'>[See rule 5 (1)] ");
            stringBuilder.Append("</h2>");
            stringBuilder.Append("<div class='section'>");
            stringBuilder.Append("<div class='point-1'>");
            stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>1. Name and Address of Executants ( Seller / Doner / Releasor / Leaser )</h2>");

            DataSet dsPartyDetails_Pdf = new DataSet();
            dsPartyDetails_Pdf = objClsNewApplication.GetPartyDetails_PDF(appid, "Executants", Appno);
            if (dsPartyDetails_Pdf != null)
            {
                if (dsPartyDetails_Pdf.Tables.Count > 0)
                {

                    if (dsPartyDetails_Pdf.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = dsPartyDetails_Pdf.Tables[0];
                        //StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("<table style='width: 100%; margin-left: 20px; margin-top:-40px; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2%;'>");
                        stringBuilder.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            stringBuilder.Append("<th style='border: 1px solid black; padding: 5px 15px;'>");
                            stringBuilder.Append(column.ColumnName);
                            stringBuilder.Append("</th>");
                        }
                        stringBuilder.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            stringBuilder.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                stringBuilder.Append("<td style='border: 1px solid black; padding: 5px 15px;'>");
                                stringBuilder.Append(row[column.ColumnName]);
                                stringBuilder.Append("</td>");
                            }
                            stringBuilder.Append("</tr>");
                        }
                        stringBuilder.Append("</table>");

                    }

                }
            }

            stringBuilder.Append("</h2>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div class='section'>");
            stringBuilder.Append("<div class='point-1'>");

            stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>2. Name and Address of Claimants ( Buyer / Recipient / Releasee / Lessee )</h2>");
            DataSet dsPartyDetails_Pdf1 = new DataSet();
            dsPartyDetails_Pdf1 = objClsNewApplication.GetPartyDetails_PDF(appid, "Claimant", Appno);
            if (dsPartyDetails_Pdf1 != null)
            {
                if (dsPartyDetails_Pdf1.Tables.Count > 0)
                {

                    if (dsPartyDetails_Pdf1.Tables[0].Rows.Count > 0)
                    {
                        DataTable dt = dsPartyDetails_Pdf1.Tables[0];
                        //StringBuilder stringBuilder = new StringBuilder();
                        stringBuilder.Append("<table style='width: 100%; margin-left: 20px; margin-top:-40px; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2%;'>");
                        stringBuilder.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            stringBuilder.Append("<th style='border: 1px solid black; padding: 5px 15px;'>");
                            stringBuilder.Append(column.ColumnName);
                            stringBuilder.Append("</th>");
                        }
                        stringBuilder.Append("</tr style='border: 1px solid black; padding: 5px 15px;'>");
                        foreach (DataRow row in dt.Rows)
                        {
                            stringBuilder.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                stringBuilder.Append("<td style='border: 1px solid black; padding: 5px 15px;'>");
                                stringBuilder.Append(row[column.ColumnName]);
                                stringBuilder.Append("</td>");
                            }
                            stringBuilder.Append("</tr>");
                        }
                        stringBuilder.Append("</table>");


                    }

                }
            }

            stringBuilder.Append("</h2>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");


            stringBuilder.Append("<div class='point-2'>");
            stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>3. Case Details</h2>");
            stringBuilder.Append("<table style='width: 100%; margin-left: 20px; margin-top: -40px; border: 1px solid black; border-collapse: collapse; margin-top: 25px; font-family: sans-serif; margin-left: 2%;'>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th style='border-collapse: collapse; border: 1px solid black; width: 50%'>Proposal No.</th>");

            stringBuilder.Append("<th style='border-collapse: collapse; border: 1px solid black; width: 50%'>" + lblRegInitEStampID_P.Text);
            stringBuilder.Append("</th>");
            stringBuilder.Append("</tr>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; padding: 5px 15px;'>" + lblProposalId_P.Text);
            stringBuilder.Append("</td>");
            stringBuilder.Append("<td style='border: 1px solid black; padding: 5px 15px;'>" + lblPropertyRegNoInitIdEStampId_P.Text);
            stringBuilder.Append("</td>");
            stringBuilder.Append("</th>");
            stringBuilder.Append("</tr>");
            stringBuilder.Append("</table>");
            stringBuilder.Append("</div>");


            stringBuilder.Append("<div id='pr_d'>");
            stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>4. Proposal Details</h2>");

            stringBuilder.Append("<table style='width: 100%; border: 1px solid black; text-align: left; border-collapse: collapse; margin-top: 50px; font-family: sans-serif; margin-left: 2 %;'>");

            stringBuilder.Append("<tbody>");
            stringBuilder.Append("<tr>");
            stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 30px;'>Sr. No.</th>");
            stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 30px;'>Particulars</th>");
            stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 25px; height: 40px; padding: 0 1px 0 30px;'>Details</th>");
            stringBuilder.Append("</tr>");



            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>2</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Date of Presentation</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblDateofPresent_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>1</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Date of Executants</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblDateofExecution_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</td>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>3</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Nature of Document</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblNatureDoc_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>4</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Duty paid on the document</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblStamDuty_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>5</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Nature of the document and duty chargeable their upon as in the opinion of the Registering Officer</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblNatureDocRegOff_P.Text);
            stringBuilder.Append("&nbsp;and &nbsp;<label>" + lblProRecStmapDuty_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>6</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Basis of duty calculation by Registering officer. </td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            //stringBuilder.Append("<label>" + lblStamDuty_P.Text);
            //stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>7</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Deficit duty as opinioned by the Registering Officer </td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblStamDuty_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");

            stringBuilder.Append("<tr>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>8</td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px; font-weight: bold'>Remarks (If any) </td>");
            stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 25px; padding: 0 50px 0 50px;'>");
            stringBuilder.Append("<b style='white-space: nowrap; height: 50px; display: flex; justify-content: left; align-items: center; font-weight: normal;'>");
            stringBuilder.Append("<label>" + lblSRComments_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</b>");
            stringBuilder.Append("</tr>");
            stringBuilder.Append("</tbody>");
            stringBuilder.Append("</table>");
            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div style='width: 90%; height:1300px; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 40px 30px 30px 30px; margin-top: 10px; position: relative;top: 180px;'>");

            stringBuilder.Append("<div style='text-align: left; font-weight: bold; padding: 20px;'>");
            stringBuilder.Append("<p class='note' style='text-align: left'>");
            stringBuilder.Append("<span style='text-align: left; text-align: left; width: auto; float: left;'>Note: Reference section 17 of Indian Stamp act 1899 says on instrument chargeable with duty and executed by any person in India shall stamped before or at the time of execution</span>");
            stringBuilder.Append("</p>");
            stringBuilder.Append("</div>");

            stringBuilder.Append("<div style='display: flex; justify-content: space-between; margin-top: 100px; font-weight: bold;'>");
            stringBuilder.Append("<div>");
            stringBuilder.Append("<p style=font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;'>Place...................................");
            stringBuilder.Append("</p>");
            stringBuilder.Append("<p style=font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;'>Date...................................");
            stringBuilder.Append("</p>");
            stringBuilder.Append("</div>");



            stringBuilder.Append("<div>");
            stringBuilder.Append("<p style='font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;'>Name of Registering Officer:");
            stringBuilder.Append("<label>" + lblSRName_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</p>");
            stringBuilder.Append("<p style='font-size: 19px; line-height: 30px; margin-top: 20px; text-align: justify;'>");
            stringBuilder.Append("<label>" + lblSROName_P.Text);
            stringBuilder.Append("</label>");
            stringBuilder.Append("</p>");
            stringBuilder.Append("</div>");


            stringBuilder.Append("</div>");
            stringBuilder.Append("</div>");

            string Proposal_ID = Session["ProposalID"].ToString();

            string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "__Formate1.pdf";
            string ProposalSheetPath = Server.MapPath("~/Proposal/" + Proposal_ID);
            ViewState["FirstFormate_Path"] = "~/Proposal/" + Proposal_ID + "/" + FileNme;
            //ViewState["SecondFormate_Path"] = "";

            string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, stringBuilder.ToString());
            generateFormateSecond_PDF();

            //ViewState["FirstFormate_Path"] = ConvertHTMToPDF(FileNme, "~/Proposal/", stringBuilder.ToString());

            //string caseno = Session["CAESNUMBER"].ToString();

            //DataTable dtUp = objClsNewApplication.InsertProposalSheetPath(Convert.ToInt32(appid), ViewState["FirstFormate_Path"].ToString(), ViewState["SecondFormate_Path"].ToString(), caseno, "Proposal Copy");


        }

        protected void btnAccept_Click(object sender, EventArgs e)
        {

            try
            {

                //if (Session["User"] == null)
                //{
                //    Response.Redirect("LoginPage.aspx");
                //    return;
                //}

                //sweetalert();


                int appid = Convert.ToInt32(Session["AppID"]);
                string AppNo = (string)Session["Appno"];

                //string headid = lblHeadbySR.Text;
                //string secid = lblSecbySR.Text;

                int distid = Convert.ToInt32(Session["DistrictID"]);
                //int sroid = Convert.ToInt32(Session["SROid"]);
                //int sroid = 0;
                string distname = Session["District_NameEN"].ToString();

                string headid = ddlHead1.SelectedValue;
                string secid = ddlSec1.SelectedValue;

                string headType = ddlHead1.SelectedItem.Text;
                string secType = ddlSec1.SelectedItem.Text;

                if (ddlSec1.SelectedIndex == 0)
                {
                    string Message = "Please select section";
                    string Title = "info";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "Swal.fire('Please select section')", true);
                    return;
                }

                string status = "Accepted";
                //string mobile = Session["AppMobile"].ToString();
                //string caseno = objClsNewApplication.AcceptRejectCase(appid, AppNo, status, txtCoSComment.Text.Trim(), headid, secid);
                string caseno = objClsNewApplication.AcceptRejectCase(appid, distid, distname, AppNo, status, "", "", "", headType, secType, headid, secid);


                SendNotification();


                if (!string.IsNullOrEmpty(caseno))
                {
                    string msg = "Dear applicant, your Sampada CMS case number against the application number '" + AppNo + "' is '" + caseno + "'. Govt of MP";


                    msg = "Application Accepted Successfully, Case number is - " + caseno;
                    //string url = "Ordersheet.aspx?Case_Number=" + caseno;

                    hdnfldHead.Value = headType;
                    hdnfldSection.Value = secType;
                    hdnfldCase.Value = caseno;
                    Session["Case_Number"] = caseno;
                    hdnAppID.Value = Session["AppID"].ToString();

                    Session["CAESNUMBER"] = caseno;

                    string CAESNUMBER = Session["CAESNUMBER"].ToString();
                    generateFormateFirst_PDF();
                    generateFormateSecond_PDF();

                    DataTable dtUp = objClsNewApplication.InsertProposalSheetPath(Convert.ToInt32(appid), ViewState["FirstFormate_Path"].ToString(), ViewState["SecondFormate_Path"].ToString(), caseno, "Proposal Copy");


                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageCaseReg();</script>");

                    //sweetalert1();


                }
            }
            catch (Exception ex)
            {

            }

        }

        private void SendNotification()
        {
            int appid = Convert.ToInt32(Session["AppID"]);
            DataTable dt = objClsNewApplication.GetPartyDeatilForNotification(appid);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string Name = dt.Rows[i]["Sender_Name"].ToString();
                string RegistrationNo = dt.Rows[i]["Property_RegNO"].ToString();
                string CaseNo = dt.Rows[i]["case_no"].ToString();
                string AppNumber = dt.Rows[i]["Reg_Initi_Estammp"].ToString();
                string whatsapp = dt.Rows[i]["whatsappno"].ToString();
                string MobileNo = dt.Rows[i]["mobileno"].ToString();
                string Email = dt.Rows[i]["emailID"].ToString();
                string PartyID = dt.Rows[i]["party_id"].ToString();
                SendCaseNumber(Name, RegistrationNo, CaseNo, AppNumber, whatsapp, MobileNo, Email, PartyID);
            }
        }

        public void SendCaseNumber(string Name, string RegistrationNo, string CaseNo, string AppNumber, string whatsapp, string MobileNo, string Email, string PartyID)
        {

            SendWatsapMSG(Name, RegistrationNo, CaseNo, AppNumber, whatsapp, MobileNo, Email, PartyID);
            SendSmsMSG(Name, RegistrationNo, CaseNo, AppNumber, whatsapp, MobileNo, Email, PartyID);
            SendEmail(Name, RegistrationNo, CaseNo, AppNumber, whatsapp, MobileNo, Email, PartyID);

        }

        private void SendEmail(string name, string registrationNo, string caseNo, string AppNumber, string whatsapp, string mobileNo, string email, string partyID)
        {



            string msg = "प्रिय " + name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + registrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + caseNo + " है, कृपया नोटिस देखने के लिए लिंक पर क्लिक करें |";

            String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            EmailUtility emailUtility = new EmailUtility();
            string userid = HttpContext.Current.Profile.UserName;
            string IP = HttpContext.Current.Request.UserHostAddress;
            emailUtility.SendEmail(registrationNo, caseNo, email, msg, PageUrl, userid, IP, "");
        }

        private void SendSmsMSG(string name, string registrationNo, string caseNo, string appId, string whatsapp, string mobileNo, string email, string partyID)
        {
            string caseNo1 = "";
            string caseNo2 = "";

            if (caseNo!="")
            {
                //caseNo1 = caseNo.Split('/')[0].ToString()+"/"+ caseNo.Split('/')[1].ToString()+"/"+ caseNo.Split('/')[2].ToString()+"/";
                caseNo1 = caseNo.Split('/')[0].ToString()+"/"+ caseNo.Split('/')[1].ToString()+"/"+ caseNo.Split('/')[2].ToString()+"";
                caseNo2 = caseNo.Split('/')[3].ToString()+"/"+ caseNo.Split('/')[4].ToString()+"/"+ caseNo.Split('/')[5].ToString();
            }

            
            //प्रिय {#var#}, आपकी संपत्ति रजिस्ट्री क्रमांक {#var#} के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर {#var#}/{#var#} है ।

            string msg = "प्रिय " + name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + registrationNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + caseNo1 + "/" + caseNo2 + " है।";

            // प्रिय {#var#}, आपकी संपत्ति रजिस्ट्री क्रमांक {#var#} के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर {#var#} है, कृपया नोटिस देखने के लिए नीचे दिये  गए  लिंक पर क्लिक करें।  {#var#}

            //Dear {#var1#},case has been registered against your property {#var2#} having case number :{#var3#}{#var4#}.

            //string msg = "Dear " + name + ",case has been registered against your property " + registrationNo + " having case number : " + caseNo1 + "" + caseNo2 + ".";

            
            //Dear {#var#}, The case has been registered against your property {#var#} having case number :{#var#} Please see the {#var#} notice given below link {#var#} 


            string templateid = "1407171929723764438";              // Hindi template 
            //string templateid = "1407171929699321761";            // English template



            string response = CMS_Sampada_BAL.SMSUtility.sendUnicodeSMS(SmsUser, SmsPassword, SmsSenderId, mobileNo, msg, secureKey, templateid);
            //sendUnicodeSMS("DITMP-CTDDRS", "qazxswedc123#", "CTDDRS", whatsapp, msg, "9a1e5526-e38f-4cff-b19b-754c0221066f", "1407168854103631812");
            String PageUrl = new Uri(HttpContext.Current.Request.Url.AbsoluteUri).OriginalString;
            objClsNewApplication.SMSResponse_Insert(registrationNo, caseNo, "SMS", msg, response, PageUrl, mobileNo, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, Convert.ToInt32(partyID.ToString()));



        }

        private void SendWatsapMSG(string name, string registrationNo, string caseNo, string appId, string whatsapp, string mobileNo, string email, string partyID)
        {

            Check_Insert_WhatsAppOptINdd(whatsapp, name, caseNo, registrationNo, "", partyID, "");
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
                string caseNo1 = "";
                string caseNo2 = "";

                if (CaseNo != "")
                {
                    //caseNo1 = caseNo.Split('/')[0].ToString()+"/"+ caseNo.Split('/')[1].ToString()+"/"+ caseNo.Split('/')[2].ToString()+"/";
                    caseNo1 = CaseNo.Split('/')[0].ToString() + "/" + CaseNo.Split('/')[1].ToString() + "/" + CaseNo.Split('/')[2].ToString() + "";
                    caseNo2 = CaseNo.Split('/')[3].ToString() + "/" + CaseNo.Split('/')[4].ToString() + "/" + CaseNo.Split('/')[5].ToString();
                }

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

                    string noticepdfsave = noticepdf;

                    string Link = "http://" + authority + noticepdfsave;


                    string msgurl = authority + noticepdf;

                    Session["HendlerURL"] = Link;

                    string partyurl = "https://sampada.mpigr.gov.in/";

                    string msg = "प्रिय " + Name + ", आपकी संपत्ति रजिस्ट्री क्रमांक " + RegistrtionNo + " के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर " + CaseNo + " है ।";
                    //प्रिय { { 1} }, आपकी संपत्ति रजिस्ट्री क्रमांक { { 2} } के विरुद्ध मामला दर्ज किया गया है जिसका केस नंबर { { 3} }{ { 4} } है ।

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
                    objClsNewApplication.WhatsappResponse_Insert(RegistrationNo, CaseNo, "whatsapp", msg, responseString, PageUrl, cntctnumb, HttpContext.Current.Profile.UserName, HttpContext.Current.Request.UserHostAddress, PartyID, noticepdfsave, Notice_ID);
                    //Console.WriteLine("Message Send Successfully");

                    //Session["WhatsappTest"] = RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString;
                    //Response.Write(RAM_MediaUrl + "     ---    " + authority + "     ----    " + responseString);
                }
            }

            catch (Exception ex)
            {

            }

        }
        public void GuideLineValuePenalityCalculation()
        {

            try
            {
                lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");

                //string today = DateTime.Now.ToString("dd/MM/yyyy");

                if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                {

                    //Storing input Dates

                    //DateTime FromMonthDays = Convert.ToDateTime(lblDateofExecution.Text);
                    //DateTime ToMonthDays = Convert.ToDateTime(lblTodate.Text);

                    DateTime FromMonthDays = DateTime.ParseExact(lblDateofExecution.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToMonthDays = DateTime.ParseExact(lblTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

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


                    decimal GuideLineValDeficit = decimal.Parse(lblGuideValueRegDefcit.Text);

                    //int GuideLineValDeficit = Convert.ToInt32(lblGuideValueRegDefcit.Text);
                    decimal Penality = GuideLineValDeficit * 2 / 100;
                    decimal PenalityPerDay = Penality / 30;
                    decimal TotalDaysPenality = PenalityPerDay * TotDays;
                    //double totalpen = GuideLineValDeficit + PenPerDay;
                    //decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                    //lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                    decimal FinalPenality = Math.Round(TotalDaysPenality, 2);

                    //if (FinalPenality <= GuideLineValDeficit)
                    //{
                    //    lblGuideValuePenality.Text = FinalPenality.ToString();
                    //}
                    //else
                    //{
                    //    lblGuideValuePenality.Text = GuideLineValDeficit.ToString();
                    //}

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
                lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                {
                    //Storing input Dates  
                    //DateTime FromMonthDays = Convert.ToDateTime(lblDateofExecution.Text);
                    //DateTime ToMonthDays = Convert.ToDateTime(lblTodate.Text);

                    DateTime FromMonthDays = DateTime.ParseExact(lblDateofExecution.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToMonthDays = DateTime.ParseExact(lblTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

                    decimal JanpadDutyDeficit = decimal.Parse(lblDeficitJanpad.Text);
                    decimal UpkarDutyDeficit = decimal.Parse(lblDeficitUpkar.Text);
                    decimal MuncipalDutyDeficit = decimal.Parse(lblDeficitMuncipal.Text);
                    decimal PrincipleDutyDeficit = decimal.Parse(lblDeficitPrinciple.Text);
                    decimal StampDutyDeficit = decimal.Parse(lblDeficitDuty.Text);

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
                        lblPenalityJanpad.Text = finalJanpadPenality.ToString();
                    }
                    else
                    {
                        lblPenalityJanpad.Text = JanpadDutyDeficit.ToString();
                    }

                    decimal finalUpkarPenality = Math.Round(TotalDaysUpkarPenality, 2);

                    if (finalUpkarPenality <= UpkarDutyDeficit)
                    {
                        lblPenalityUpkar.Text = finalUpkarPenality.ToString();
                    }
                    else
                    {
                        lblPenalityUpkar.Text = UpkarDutyDeficit.ToString();
                    }

                    decimal finalMuncipalPenality = Math.Round(TotalDaysMuncipalPenality, 2);

                    if (finalMuncipalPenality <= MuncipalDutyDeficit)
                    {
                        lblPenalityMuncipal.Text = finalMuncipalPenality.ToString();
                    }
                    else
                    {
                        lblPenalityMuncipal.Text = MuncipalDutyDeficit.ToString();
                    }

                    decimal finalPrinciplePenality = Math.Round(TotalDaysPrinciplePenality, 2);

                    if (finalPrinciplePenality <= PrincipleDutyDeficit)
                    {
                        lblPenalityPrinciple.Text = finalPrinciplePenality.ToString();
                    }
                    else
                    {
                        lblPenalityPrinciple.Text = PrincipleDutyDeficit.ToString();
                    }

                    decimal FinalStampDutyPenality = Math.Round(TotalDaysStampDutyPenality, 2);

                    //if (FinalStampDutyPenality <= StampDutyDeficit)
                    //{
                    //    lblStampDutyPenality.Text = FinalStampDutyPenality.ToString();
                    //}
                    //else
                    //{
                    //    lblStampDutyPenality.Text = StampDutyDeficit.ToString();
                    //}

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
                lblTodate.Text = DateTime.Now.ToString("dd/MM/yyyy");
                if (lblDateofExecution.Text != "" && lblTodate.Text != "")
                {
                    //Storing input Dates  
                    DateTime FromMonthDays = DateTime.ParseExact(lblDateofExecution.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                    DateTime ToMonthDays = DateTime.ParseExact(lblTodate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

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

                    decimal RegistrationFeeDeficit = decimal.Parse(lblDeficitRegFee.Text);

                    decimal Penality = RegistrationFeeDeficit * 2 / 100;
                    decimal PenalityPerDay = Penality / 30;
                    decimal TotalDaysPenality = PenalityPerDay * TotDays;
                    //double totalpen = GuideLineValDeficit + PenPerDay;
                    //decimal GrandTotalPenality = GuideLineValDeficit + TotalDaysPenality;
                    //lblGuideValuePenality.Text = GrandTotalPenality.ToString();
                    decimal FinalPenality = Math.Round(TotalDaysPenality, 2);

                    //if (FinalPenality <= RegistrationFeeDeficit)
                    //{
                    //    lblRegFeePenality.Text = FinalPenality.ToString();
                    //}
                    //else
                    //{
                    //    lblRegFeePenality.Text = RegistrationFeeDeficit.ToString();
                    //}


                }
            }
            catch (Exception ex)
            {

            }
        }



        private void sweetalert()
        {
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageHeadSection();</script>");
            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>test1();</script>");
        }

        private void sweetalert1()
        {

            //Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "none", "<script>ShowMessageCaseReg();</script>");
            Page.ClientScript.RegisterClientScriptInclude(this.GetType(), "none", "<script>test2();</script>");
        }

        protected void ddlHead1_SelectedIndexChanged(object sender, EventArgs e)
        {
            bindSection();
            lblHeadbySR.Text = ddlHead1.SelectedItem.Text;
        }

        protected void ddlSec1_SelectedIndexChanged(object sender, EventArgs e)
        {

            lblSecbySR.Text = ddlSec1.SelectedItem.Text;
            //objClsNewApplication.update_Head_And_Section(Convert.ToInt32(ViewState["AppId"].ToString()), ddlHead1.SelectedValue, ddlSec1.SelectedValue);
        }

        protected void Button2_Click1(object sender, EventArgs e)
        {

        }

        //[WebMethod]
        //public static string show_Pdf_BY_Hendler_Deed_Doc(string PDFPath, string DocType, string EREG_ID,string Tocan)
        //{
        //    string binaryPdfPath = "";

        //    if (DocType== "RegistryDocument" || DocType == "ProposalDocument" || DocType == "AdditionalDocument")
        //    {
        //        //EREG_ID = "1358";
        //        string encodedPdfData = Api_Comsumedata(DocType, Convert.ToInt32(EREG_ID), Tocan);

        //        if (encodedPdfData != null)
        //        {
        //            binaryPdfPath = "data:application/pdf;base64," + encodedPdfData + "";
        //            //binaryPdfPath =  encodedPdfData;
        //        }

        //    }
        //    else if (DocType == "REG")
        //    {
        //        binaryPdfPath = "../GeteRegDoc_Handler.ashx?pageURL=" + PDFPath;

        //    }
        //    else if (DocType == "PROP")
        //    {
        //        binaryPdfPath = "../GetProposalFormDoc_Handler.ashx?pageURL=" + PDFPath;

        //    }
        //    else if (DocType == "ATTCH")
        //    {
        //        binaryPdfPath = "../GetAttachedDoc_Handler.ashx?pageURL=" + PDFPath;

        //    }
        //    else
        //    {
        //        if (PDFPath.Contains(@"~"))
        //        {
        //            binaryPdfPath = PDFPath.Replace(@"~", "..");
        //        }
        //        else
        //        {
        //            binaryPdfPath = PDFPath;
        //        }

        //    }
        //    // binaryPdfPath = "../GetDocumentsREG_PRO_DOC.ashx?pageURL=" + PDFPath;

        //    return binaryPdfPath;

        //}

        [WebMethod]
        public static string show_Pdf_BY_Hendler_Deed_Doc(string PDFPath, string DocType, string EREG_ID, string Tocan)
        {
            string binaryPdfPath = "";

            if (DocType == "RegistryDocument" || DocType == "ProposalDocument" || DocType == "AdditionalDocument")
            {
                //EREG_ID = "1358";
                string encodedPdfData = Api_Comsumedata(DocType, Convert.ToInt32(EREG_ID), Tocan);

                if (encodedPdfData != null)
                {
                    //binaryPdfPath = "data:application/pdf;base64," + encodedPdfData + "";
                    binaryPdfPath = encodedPdfData;
                }

            }



            else if (DocType == "REG")
            {
                binaryPdfPath = "../GeteRegDoc_Handler.ashx?pageURL=" + PDFPath;

            }
            else if (DocType == "PROP")
            {
                binaryPdfPath = "../GetProposalFormDoc_Handler.ashx?pageURL=" + PDFPath;

            }
            else if (DocType == "ATTCH")
            {
                binaryPdfPath = "../GetAttachedDoc_Handler.ashx?pageURL=" + PDFPath;

            }
            else
            {
                if (PDFPath.Contains(@"~"))
                {
                    binaryPdfPath = PDFPath.Replace(@"~", "..");
                }
                else
                {
                    binaryPdfPath = PDFPath;
                }

            }
            //binaryPdfPath = "../GetDocumentsREG_PRO_DOC.ashx?pageURL=" + PDFPath;

            return binaryPdfPath;

        }



        protected void grdSRDoc_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }
    }


    public class ResonseA
    {
        public string responseData { get; set; }
        public string responseStatus { get; set; }
    }
}