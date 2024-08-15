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
    public partial class Cases_In_Hearing : System.Web.UI.Page
    {

        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Case_Number"] = null;
                Session["AppID"] = null;
                Session["Appno"] = null;
                Session["HearingDate"] = null;
                Session["Case_Status"] = null;
                Session["Notice_ID"] = null;
                Session["ProposalID"] = null;
                Session["hearing_id"] = null;
                grdCaseList.Columns[3].Visible = false;


                string HearingDT = "";
                if (Session["HearingSelectedDt"]!=null)
                {
                    HearingDT = Session["HearingSelectedDt"].ToString();
                }
                if (HearingDT != "")
                {
                    BindGridFromDashboard();
                }
                else
                {
                    BindGridToday();
                }
                //if (Request.QueryString["Case_Number"] != null)
                //{
                //    ViewState["Case_Number"] = Request.QueryString["Case_Number"].ToString();

                //}
                //else
                //{
                //    ViewState["Case_Number"] = "";
                //}

                //BindGrid();
                
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
        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            txtHearingDate.Text = Calendar1.SelectedDate.ToShortDateString();

            string HearingDate = Calendar1.SelectedDate.ToShortDateString();

            DateTime Appno = Convert.ToDateTime(HearingDate);




            BindGrid();
        }
        private void BindGridToday()
        {
            int DistricId = 0;
            int DRID = 0;
            int DROID = 0;
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DRID"] != null)
            {
                DRID = Convert.ToInt32(Session["DRID"]);
            }
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }

            try
            {

                string date = DateTime.Now.ToString();
                DateTime HDt = Convert.ToDateTime(date);
                DataTable dt = clsHearingBAL.GetHearingDetails(HDt, DistricId, DROID);
                if (dt.Rows.Count > 0)
                {

                    string noticeId = dt.Rows[0]["Notice_id"].ToString();
                    grdCaseList.DataSource = dt;
                    grdCaseList.DataBind();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void BindGridFromDashboard()
        {
            int DistricId = 0;
            int DRID = 0;
            int DROID = 0;



            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DRID"] != null)
            {
                DRID = Convert.ToInt32(Session["DRID"]);
            }
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }

            try
            {
                
                //string date = DateTime.Now.ToString();
                DateTime HDt = Convert.ToDateTime(Session["HearingSelectedDt"].ToString());
                DataTable dt = clsHearingBAL.GetHearingDetails(HDt, DistricId, DROID);
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid()
        {
            int DistricId = 0;
            int DRID = 0;
            int DROID = 0;
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DRID"] != null)
            {
                DRID = Convert.ToInt32(Session["DRID"]);
            }
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }

            try
            {
                DateTime HearingDt = Convert.ToDateTime(txtHearingDate.Text);
                //string date = DateTime.Now.ToString();
                DateTime HDt = Convert.ToDateTime(HearingDt);
                DataTable dt = clsHearingBAL.GetHearingDetails(HDt, DistricId, DROID);
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void lnkSelect_Click(object sender, EventArgs e)
        {

            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            string CaseNumber = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
            string Noticeid = grdCaseList.DataKeys[rowindex].Values["Notice_id"].ToString();
            DateTime Hearing = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString());
            string HearingID = (grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString());
            Session["Appno"] = (grdCaseList.DataKeys[rowindex].Values["application_no"].ToString());
            string AppID = grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString();
            Session["AppID"] = (grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
            Session["Case_Number"] = CaseNumber;

            Session["HearingDate"] = Hearing;
            Session["hearing_id"] = HearingID;
            Session["Notice_ID"] = Noticeid;
            Session["App_ID"] = AppID;

            //Response.Redirect("Hearing.aspx?Case_Number=" + CaseNumber + "&NoticeId=" + Noticeid + "&Hearing=" + Hearing + "&hearing_id=" + HearingID, false);

            Response.Redirect("Hearing.aspx", false);
            //string CaseNumber = grdCaseList.DataKeys[row.RowIndex].Values[0].ToString();
            //Response.Redirect("Hearing.aspx?Case_Number=" + CaseNumber + "&NoticeId=" + Noticeid + "&Hearing=" + Hearing, false);
        }
        protected void MeetingCalendar_DayRender(object sender, DayRenderEventArgs e)
        {
            string dayNumber = e.Day.Date.Day.ToString();
            e.Cell.Text = dayNumber + "<br />";

            e.Cell.Text += "<div align='center'>";
            e.Cell.Text += "    <a href='DailyMeetings.aspx?id=10' title='Day has meeting(s) scheduled.'>";
            e.Cell.Text += "            <img src='../Images/meeting.gif' height='25' width='25' alt='' border='0' />";
            e.Cell.Text += "        </a>";
            e.Cell.Text += "</div>";
        }







        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            DataSet dsList = new DataSet();
            int DROID = 0;
            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }

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
                    catch (Exception ex)
                    {

                    }




                }


            }


            //GetPartyDetail();
        }


    }
}