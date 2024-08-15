using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class CoSHome : System.Web.UI.Page
    {
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();

        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //Response.Write(Session["FeatureCode"].ToString() + " , " + Session["FeatureName"].ToString() + " , " + Session["FeatureNameEn"].ToString());

                //lblOfficecode.Text = Session["DistrictID"].ToString();
                Session["ORDRSHEETPATH"] = null;
                Session["Case_Number"] = null;
                Session["Proposalno"] = null;
                Session["AppID"] = null;
                Session["IMPOUND_DATE"] = null;
                Session["ProImpoundDate"] = null;
                Session["App_Id"] = null;
                Session["AppNo"] = null;
                Session["Flag"] = null;
                Session["Case_Status"] = null;
                ProposalCount();

                int DROID = Convert.ToInt32(Session["DROID"]);
                DataSet dsDocDetails = clsHearingBAL.GetUpcomingHearing(DROID);

                if (dsDocDetails != null)
                {
                    if (dsDocDetails.Tables.Count > 0)
                    {

                        if (dsDocDetails.Tables[0].Rows.Count > 0)
                        {
                            RepDetails.DataSource = dsDocDetails;
                            RepDetails.DataBind();
                        }
                    }
                }
            }
        }
        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DataSet dsList = new DataSet();
            int DROID = Convert.ToInt32(Session["DROID"]);
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

                            string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];


                            DateTime hearingDate = Convert.ToDateTime(systemDate);
                            //string inputDateString = DR["HearingDate"].ToString();
                            //DateTime hearingDate = DateTime.ParseExact(inputDateString, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                            //// Format the DateTime object to the desired output format
                            //string formattedDate = hearingDate.ToString("dd/MM/yyyy");

                            ////string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
                            //string systemDate = formattedDate.ToString().Split('/')[1] + "/" + formattedDate.ToString().Split('/')[0] + "/" + formattedDate.ToString().Split('/')[2];

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
                                //label1.ForeColor= System.Drawing.Color.LightGreen;
                                e.Cell.BackColor = System.Drawing.Color.LightGreen;
                                //e.Cell.ForeColor = System.Drawing.Color.White;
                            }
                        }

                    }
                    catch (Exception)
                    {

                    }

                }

            }

            
            //GetPartyDetail();
        }


        protected void ProposalCount()
        {
            DataSet dataSet = new DataSet();
            try
            {
                int distid = Convert.ToInt32(Session["DistrictID"]);
                //int drid = Convert.ToInt32(Session["DRID"]);
                int droid = Convert.ToInt32(Session["DROID"]);
                dataSet = objClsNewApplication.GetNewProposalcount(distid, droid);
                lblNewProposal.Text = dataSet.Tables[0].Rows[0]["New_proposal"].ToString();
                //lblregistered.Text = dataSet.Tables[1].Rows[0]["Registered_Cases"].ToString();
                lblordered.Text = dataSet.Tables[2].Rows[0]["Ordered_Cases"].ToString();
                lblpendingcases.Text = dataSet.Tables[3].Rows[0]["Total_Pending_Cases"].ToString();
                lblearlyhearing.Text = dataSet.Tables[4].Rows[0]["Early_Hearing_Cases"].ToString();
                lblCasesmovedtorrc.Text = dataSet.Tables[5].Rows[0]["Cases_moved_to_RRC"].ToString();
                lblTodaysHearingCases.Text = dataSet.Tables[6].Rows[0]["Todays_Hearing_Cases"].ToString();
                //lblAppealCases.Text = dataSet.Tables[7].Rows[0]["Appeal_Cases"].ToString();
               
                lblFinalOrderCount.Text = dataSet.Tables[7].Rows[0]["Final_Order_Pending_Cases"].ToString();
                //lblFinalOrderSend.Text = dataSet.Tables[8].Rows[0]["Pending_FinalOrder_Send_Cases"].ToString();
                lblTemplate.Text = dataSet.Tables[10].Rows[0]["Total_Templates"].ToString();

                LblPayment.Text = dataSet.Tables[11].Rows[0]["Total_Cos_Cases_Payment_Done"].ToString();
                //lblFinalOrderList.Text = dataSet.Tables[12].Rows[0]["Cos_Final_Order_List"].ToString();

                lblRRCCertificate.Text = dataSet.Tables[13].Rows[0]["RRCCertificate_List"].ToString();
                lblClosedCases.Text = dataSet.Tables[14].Rows[0]["GetClosedCases"].ToString();
                //lblLegacyCases.Text = dataSet.Tables[15].Rows[0]["RRCCertificate_List"].ToString();
            }
            catch (Exception ex)
            {

            }
            
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtHearingDate.Text = Calendar1.SelectedDate.ToShortDateString();

            Session["HearingSelectedDt"] = txtHearingDate.Text;

            Response.Redirect("Cases_In_Hearing", false);
        }

        //public static boolean validateFeatureAccess(String featureServiceUrl, List<MstFeatureBean> featureList)
        //{
        //    Boolean validateFeatureServiceUrl = Boolean.FALSE;
        //    if (featureList != null && featureList.size() > 0)
        //    {
        //        if (featureList.stream().anyMatch(feature->feature.getStatus() == 1 && ((feature.getFeatureCode() != null && feature.getFeatureCode().trim().equalsIgnoreCase(featureServiceUrl.trim())) || (feature.getFeatureServiceUrl() != null && feature.getFeatureServiceUrl().trim().equalsIgnoreCase(featureServiceUrl.trim())))))
        //        {
        //            validateFeatureServiceUrl = Boolean.TRUE;
        //        }
        //    }
        //    return validateFeatureServiceUrl;
        //}

    }
}