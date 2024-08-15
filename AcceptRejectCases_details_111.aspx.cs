using Newtonsoft.Json;
using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class AcceptRejectCases_details : System.Web.UI.Page
    {


        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["AppId"] != null && Request.QueryString["AppNo"] != null && Request.QueryString["Flag"] != null)
                {
                    int appid = Convert.ToInt32(Request.QueryString["AppId"]);
                    string Appno = Request.QueryString["AppNo"].ToString();
                    int Flag = Convert.ToInt32(Request.QueryString["Flag"]);

                    ViewState["AppId"] = appid;
                    ViewState["AppNo"] = Appno;
                    ViewState["Flag"] = Flag;

                    DataTable dsAppDetails = new DataTable();
                    DataSet dsPartyDetails = new DataSet();
                    DataSet dsDocDetails = new DataSet();
                    DataSet dsPropertyDetails = new DataSet();
                    //LinkButton lnk = (LinkButton)sender;

                    Session["AppID"] = appid;
                    Session["Appno"] = Appno;

                    dsAppDetails = objClsNewApplication.GetApplicationDetails_CoS(appid, Appno, Flag);

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
                            lblDateofExecution.Text = dsAppDetails.Rows[0]["DateOfExecution"].ToString();
                            lblDateofPresent.Text = dsAppDetails.Rows[0]["DateOfPresentation"].ToString();


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

                            //lblStampDutyExemptedAmt.Text = dr[55].ToString();

                            lblProClassJanpad.Text = dsAppDetails.Rows[0]["Janpad_ProposedStmpDuty"].ToString();
                            lblProClassUpkar.Text = dsAppDetails.Rows[0]["Upkar_ProposedStmpDuty"].ToString();
                            lblProClassMuncipal.Text = dsAppDetails.Rows[0]["Muncipal_ProposedStmpDuty"].ToString();
                            lblProClassPrinciple.Text = dsAppDetails.Rows[0]["Principal_PropsedStmpDuty"].ToString();

                            //lblProExemptedAmt.Text = dsAppDetails.Rows[0]["Janpad_SD"].ToString();56


                            lblDeficitJanpad.Text = dsAppDetails.Rows[0]["Deficit_Janpad"].ToString();
                            lblDeficitUpkar.Text = dsAppDetails.Rows[0]["Deficit_Upkar"].ToString();
                            lblDeficitMuncipal.Text = dsAppDetails.Rows[0]["Deficit_Muncipal"].ToString();
                            lblDeficitPrinciple.Text = dsAppDetails.Rows[0]["Deficit_Principal"].ToString();


                            lblStamDuty.Text = dsAppDetails.Rows[0]["StampDuty"].ToString();
                            lblProRecStmapDuty.Text = dsAppDetails.Rows[0]["Proposed_StampDuty"].ToString();
                            lblDeficitDuty.Text = dsAppDetails.Rows[0]["DeficitDuty"].ToString();
                            //lblStampDutyPenality.Text = dsAppDetails.Rows[0]["Penality_StampDuty"].ToString();
                            lblStampDutyRemark.Text = dsAppDetails.Rows[0]["Stamp_Duty_Remark"].ToString();








                            lblRegiFee.Text = dsAppDetails.Rows[0]["PropertyValueAtRegTime"].ToString();
                            lblProRegiFee.Text = dsAppDetails.Rows[0]["ProposedPropertyValue"].ToString();
                            lblRegiExemptedAmt.Text = dsAppDetails.Rows[0]["DeficitPropertyValue"].ToString();
                            lblProRegiExemptedAmt.Text = dsAppDetails.Rows[0]["Penality_Property_Mrktvalue"].ToString();




                            //lblRegiFee.Text = dr[57].ToString();
                            //lblProRegiFee.Text = dr[58].ToString();
                            //lblRegiExemptedAmt.Text = dr[59].ToString();
                            //lblProRegiExemptedAmt.Text = dr[60].ToString();

                            lblRegFee.Text = dsAppDetails.Rows[0]["Reg_Fee"].ToString();
                            lblRecoverRegfee.Text = dsAppDetails.Rows[0]["ProposedRecoverableRegFee"].ToString();
                            lblDeficitRegFee.Text = dsAppDetails.Rows[0]["DeficitRegistrationFees"].ToString();
                            //lblRegFeePenality.Text = dsAppDetails.Rows[0]["Penality_RegsFees"].ToString();
                            lblRegFeeRemark.Text = dsAppDetails.Rows[0]["Reg_Fees_Remark"].ToString();


                            lblSROID.Text = dsAppDetails.Rows[0]["SRO_ID"].ToString();
                            lblSRName.Text = dsAppDetails.Rows[0]["Designation"].ToString();
                            lblSROName.Text = dsAppDetails.Rows[0]["Office"].ToString();
                            lblProposalId.Text = dsAppDetails.Rows[0]["Proposal_No"].ToString();
                            //lblProposalDate.Text = dsAppDetails.Rows[0]["Impound_Date"].ToString();comment in sp


                            lblHeadbySR.Text = dsAppDetails.Rows[0]["Heads_Type"].ToString();
                            Session["head"] = lblHeadbySR.Text;


                            lblSecbySR.Text = dsAppDetails.Rows[0]["Sections_Type"].ToString();
                            Session["section"] = lblSecbySR.Text;


                            lblSRComments.Text = dsAppDetails.Rows[0]["Comments"].ToString();

                            int Depart_ID = Convert.ToInt32(dsAppDetails.Rows[0]["Department_ID"].ToString());

                            //int Depart_ID = Convert.ToInt32(dr[51].ToString());

                            string PropertyRegNo = dsAppDetails.Rows[0]["Property_RegNO"].ToString();
                            string InitiationId = dsAppDetails.Rows[0]["Initiation_ID"].ToString();

                            //string PropertyRegNo = dr[53].ToString();
                            //string InitiationId = dr[54].ToString();

                            if (Depart_ID == 1)
                            {
                                //lblPropertyRegId.Text = dr[7].ToString();
                                if (PropertyRegNo != "")
                                {
                                    lblRegInitEStampID.Text = "Document Registration Number";
                                    //lblDepName.Text = "Proposal ID";
                                    lblRegInitDate.Text = "Date of Registration";
                                    lblOfficeName.Text = "Comments";
                                    lblHeading.Text = "Sub Registrar Details";

                                    pnlAuditIdDate.Visible = false;
                                    pnlAuditIdandDate.Visible = false;
                                    lblAuditDt.Visible = false;
                                }
                                else
                                {
                                    if (InitiationId != "")
                                    {
                                        lblRegInitEStampID.Text = "Intitation ID";
                                        pnlRegInitDate.Visible = false;
                                        pnlRegNoInitDate.Visible = false;
                                        //lblPropertyRegId.Text = dr[7].ToString();
                                        //lblRegInitDate.Text = "Date of Intitation";
                                        lblHeading.Text = "Sub Registrar Details";
                                        lblOfficeName.Text = "Comments";
                                        lblRegFee.Text = "NA";
                                        lblRegFee.Text = "NA";
                                        lblRecoverRegfee.Text = "NA";
                                        lblDeficitRegFee.Text = "NA";
                                        lblRegFeePenality.Text = "NA";
                                        lblRegFeeRemark.Text = "NA";
                                    }
                                    else
                                    {
                                        lblRegInitEStampID.Text = "E-Stamp ID";
                                        lblHeading.Text = "Sub Registrar Details";
                                        pnlRegInitDate.Visible = false;
                                        pnlRegNoInitDate.Visible = false;
                                        lblDateofExecution.Text = "NA";
                                        lblDateofPresent.Text = "NA";
                                        lblRegFee.Text = "NA";
                                        lblRegFee.Text = "NA";
                                        lblRecoverRegfee.Text = "NA";
                                        lblDeficitRegFee.Text = "NA";
                                        lblRegFeePenality.Text = "NA";
                                        lblRegFeeRemark.Text = "NA";
                                        lblOfficeName.Text = "Comments";
                                    }
                                }
                            }
                            if (Depart_ID == 2)
                            {
                                //lblDepName.Text = "Audit ID";
                                lblRegInitEStampID.Text = "Document Registration Number";

                                lblRegInitDate.Text = "Date of Registry";
                                lblOfficeName.Text = "Comments";
                                lblHeading.Text = "Sub Registrar / Audit Details ";
                                //txtComment.Text = (dr[26].ToString());
                                pnlAuditIdDate.Visible = true;
                                pnlAuditIdandDate.Visible = true;
                                //txtAuditDt.Text = Convert.ToDateTime(dr[21].ToString()).ToString("dd-MM-yyyy");

                                lblAuditId.Text = dsAppDetails.Rows[0]["Property_RegNO"].ToString();
                                lblAuditDt.Text = Convert.ToDateTime(dsAppDetails.Rows[0]["Initiation_ID"].ToString()).ToString("dd-MM-yyyy");

                                //lblAuditId.Text = dr[53].ToString();
                                //lblAuditDt.Text = Convert.ToDateTime(dr[54].ToString()).ToString("dd-MM-yyyy");
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


                }
            }
            catch (Exception ex)
            {
                HttpContext.Current.Response.Write("<script>alert('" + ex.Message + "')</script>");

                //throw;
            }
        }
        protected void lnkPartyView_Click(object sender, EventArgs e)
        {
            pnlPartyDetails.Visible = true;
            DataSet dsPDetails = new DataSet();
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            int Party_ID = Convert.ToInt32(grdPartyDetails.DataKeys[rowindex].Values["Party_ID"].ToString());
            Session["Party_ID"] = Party_ID;
            dsPDetails = objClsNewApplication.GetPartyByID(Party_ID);
            if (dsPDetails != null)
            {
                if (dsPDetails.Tables.Count > 0)
                {

                    if (dsPDetails.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow dr in dsPDetails.Tables[0].Rows)
                        {
                            //lblDepName.Text = dr[5].ToString(); 
                            
                            txtPartyFName.Text = dr[4].ToString();
                            txtPartyAddress.Text = dr[6].ToString();
                            txtPartyDist.Text = dr[11].ToString();
                           
                            //txtIdentity.Text = dr[12].ToString();



                        }
                    }

                }
            }
        }


        [WebMethod]
        public string DocFile()
        {
           
            DataSet ds = new DataSet();
            ClsNewApplication objClsNewApplication = new ClsNewApplication();
            int appid = Convert.ToInt32(Request.QueryString["AppId"]);
            string Appno = Request.QueryString["AppNo"].ToString();

            try
            {
                ds = objClsNewApplication.GetDocDetails_CoS(appid, Appno);
                if (ds != null)
                {
                    if (ds.Tables.Count > 0)
                    {

                        if (ds.Tables[0].Rows.Count > 0)
                        {

                            //ds = objClsNewApplication.SelectCalenderEvents();
                        }

                    }
                }
            }
            catch (Exception)
            {

                throw;
            }         
           

            string JSONString = string.Empty;
            JSONString = JsonConvert.SerializeObject(ds);
            return JSONString;
        }
    }
}