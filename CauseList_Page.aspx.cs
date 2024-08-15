using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CMS_Sampada.CoS
{
    public partial class CauseList_Page : System.Web.UI.Page
    {
        CoSCauseList_BAL couseListBAL = new CoSCauseList_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
       
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                
                
                if (Request.QueryString["Case_Number"] != null)
                {
                    ViewState["Case_Number"] = Request.QueryString["Case_Number"].ToString();

                }
                else
                {
                    ViewState["Case_Number"] = "";
                }

                //BindGrid();
                BindGridToday();
                BindGrid_RRC_Today();
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
            BindGrid_RRC();
        }
        private void BindGridToday()
        {
            int DistricId = 0;
            int DROID = 0;
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
                //datetime currentdate = datetime.now;
                //string date = currentdate.tostring("dd/mm/yyyy");/*datetime.now.tostring();*/
                //datetime hdt = convert.todatetime(date);


                //DateTime HearingDt = Convert.ToDateTime(txtHearingDate.Text);
                string date = DateTime.Now.ToString();
                DateTime HDt = Convert.ToDateTime(date);


                DataTable dt = couseListBAL.GetHearingDetailsForCOS(HDt, DistricId, DROID);
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
                Session["grdCaseList"] = dt;
            }
            catch (Exception ex)
            {

            }
        }


        private void BindGrid()
        {
            int DistricId = 0;
            int DROID = 0;
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
                //    DateTime HearingDt = Convert.ToDateTime(txtHearingDate.Text);
                //    //string date = DateTime.Now.ToString();
                //    string date = HearingDt.ToString("dd/MM/yyyy");
                //    DateTime HDt = Convert.ToDateTime(date);

                DateTime HearingDt = Convert.ToDateTime(txtHearingDate.Text);
                //string date = DateTime.Now.ToString();
                DateTime HDt = Convert.ToDateTime(HearingDt);

                DataTable dt = couseListBAL.GetHearingDetailsForCOS(HDt, DistricId, DROID);
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
                Session["grdCaseList"] = dt;
            }
            catch (Exception ex)
            {

            }
        }
        private void BindGrid_RRC()
        {
            int DistricId = 0;
            int DROID = 0;
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
                DataTable dt = couseListBAL.GetRRC_HearingDetails_CaseListForRRC(HDt, DistricId, DROID);
                GrdRRCCaseList.DataSource = dt;
                GrdRRCCaseList.DataBind();
                Session["GrdRRCCaseList"] = dt;
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid_RRC_Today()
        {
            int DistricId = 0;
            int DROID = 0;
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

                DataTable dt = couseListBAL.GetRRC_HearingDetails_CaseListForRRC(HDt, DistricId, DROID);
                GrdRRCCaseList.DataSource = dt;
                GrdRRCCaseList.DataBind();
                Session["GrdRRCCaseList"] = dt;

            }
            catch (Exception ex)
            {

            }
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


            //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
            DateTime HearingDt = Convert.ToDateTime(DateTime.Now);

            dsList = clsHearingBAL.GetRRC_HearingCount_COS();

            if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow DR in dsList.Tables[0].Rows)
                {
                    try
                    {
                        if (DR["HearingDate_cos"].ToString() != "" && DR["HearingDate_rrc"].ToString() != "")
                        {
                            string systemDate = DR["HearingDate_cos"].ToString().Split('/')[1] + "/" + DR["HearingDate_cos"].ToString().Split('/')[0] + "/" + DR["HearingDate_cos"].ToString().Split('/')[2];
                            if (e.Day.Date == Convert.ToDateTime(systemDate))
                            {
                                Literal literal1 = new Literal();
                                literal1.Text = "<br/>";
                                e.Cell.Controls.Add(literal1);
                                Label label1 = new Label();
                                label1.Text = "COS:" + Convert.ToString(DR["app_id_count_cos"]);
                                literal1.Text = "<br/>";
                                Label label2 = new Label();
                                label2.Text = "RRC:" + Convert.ToString(DR["app_id_count_rrc"]);

                                //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                                label1.Font.Size = new FontUnit(FontSize.Small);
                                label2.Font.Size = new FontUnit(FontSize.Small);
                                e.Cell.Controls.Add(label1);
                                e.Cell.Controls.Add(label2);
                                //label1.ForeColor= System.Drawing.Color.LightGreen;
                                e.Cell.BackColor = System.Drawing.Color.LightGreen;
                                //e.Cell.ForeColor = System.Drawing.Color.White;
                            }
                        }
                        else if (DR["HearingDate_cos"].ToString() == "" || DR["HearingDate_rrc"].ToString() != "")
                        {
                            string systemDate = DR["HearingDate_rrc"].ToString().Split('/')[1] + "/" + DR["HearingDate_rrc"].ToString().Split('/')[0] + "/" + DR["HearingDate_rrc"].ToString().Split('/')[2];
                            if (e.Day.Date == Convert.ToDateTime(systemDate))
                            {
                                Literal literal1 = new Literal();
                                literal1.Text = "<br/>";
                                e.Cell.Controls.Add(literal1);
                                //Label label1 = new Label();
                                //label1.Text = "COS Hearing " + Convert.ToString(DR["app_id_count_cos"]);

                                Label label2 = new Label();
                                label2.Text = "RRC:" + Convert.ToString(DR["app_id_count_rrc"]);

                                //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                                label2.Font.Size = new FontUnit(FontSize.Small);
                                e.Cell.Controls.Add(label2);
                                //label1.ForeColor= System.Drawing.Color.LightGreen;
                                e.Cell.BackColor = System.Drawing.Color.LightGreen;
                                //e.Cell.ForeColor = System.Drawing.Color.White;
                            }
                        }
                        else if (DR["HearingDate_cos"].ToString() != "" || DR["HearingDate_rrc"].ToString() == "")
                        {
                            string systemDate = DR["HearingDate_cos"].ToString().Split('/')[1] + "/" + DR["HearingDate_cos"].ToString().Split('/')[0] + "/" + DR["HearingDate_cos"].ToString().Split('/')[2];
                            if (e.Day.Date == Convert.ToDateTime(systemDate))
                            {
                                Literal literal1 = new Literal();
                                literal1.Text = "<br/>";
                                e.Cell.Controls.Add(literal1);
                                Label label1 = new Label();
                                label1.Text = "COS:" + Convert.ToString(DR["app_id_count_cos"]);

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

        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Session["Case_Number"] = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
            Session["hearing_id"] = grdCaseList.DataKeys[rowindex].Values["Hearing_ID"].ToString();
            Session["Notice_ID"] = grdCaseList.DataKeys[rowindex].Values["NOTICE_ID"].ToString();
            string Hearingdate = grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString();
            Session["Appno"]= grdCaseList.DataKeys[rowindex].Values["application_no"].ToString();
            Session["App_ID"] = grdCaseList.DataKeys[rowindex].Values["app_id"].ToString();
            Session["AppID"] = grdCaseList.DataKeys[rowindex].Values["app_id"].ToString();
            string[] dateComponents = Hearingdate.Split('/');
            string formattedDate = dateComponents[1] + "/" + dateComponents[0] + "/" + dateComponents[2];
            //DateTime Hearing = Convert.ToDateTime(formattedDate);
            Session["HearingDate"] = Convert.ToDateTime(formattedDate);

            //Response.Redirect("Hearing.aspx?Case_Number=" + CaseNumber + "&NoticeId=" + Noticeid + "&Hearing=" + Hearing + "&hearing_id=" + hearing_id, false);
            Response.Redirect("Hearing.aspx");
        }
        protected void lnkSelectRRC_Click(object sender, EventArgs e)
        {
            LinkButton lnk = (LinkButton)sender;
            GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
            int rowindex = grdrow.RowIndex;
            Session["rrc_caseid"] = GrdRRCCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
            string rid = Session["rrc_caseid"].ToString();
            Session["cos_caseno"] = GrdRRCCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
            string rno = Session["cos_caseno"].ToString();
            Session["app_id"] = GrdRRCCaseList.DataKeys[rowindex].Values["App_ID"].ToString();
            string rap = Session["app_id"].ToString();
            //string  Hearingdate = GrdRRCCaseList.DataKeys[rowindex].Values["HearingDate"].ToString();
            //string[] dateComponents = Hearingdate.Split('/');
            //string formattedDate = dateComponents[1] + "/" + dateComponents[0] + "/" + dateComponents[2];
            //DateTime Hearing = Convert.ToDateTime(formattedDate);
            Session["Final_Order_Date"] = GrdRRCCaseList.DataKeys[rowindex].Values["CASE_ACTIONDATE"].ToString();
            Session["RRC_Registered_Date"] = GrdRRCCaseList.DataKeys[rowindex].Values["INSERTEDDATE"].ToString();

            //Response.Redirect("../RRCDR/Ordersheet.aspx?rrc_caseid=" + RRCCaseNumber + "&cos_caseno=" + CaseNumber + "&app_id=" + Appid);
            Response.Redirect("../RRCDR/Ordersheet.aspx");
        }



        protected void btnsearchCOS_Click(object sender, EventArgs e)
        {
            if (txtfromdateCos.Text == "")
            {
                string script = "Swal.fire('Warning!', 'Please select Hearing Date!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                return;
            }
            if (txttodateCos.Text == "")
            {
                string script = "Swal.fire('Warning!', 'Please select Next Hearing Date!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                return;
            }
            if (txtfromdateCos.Text != "" && txttodateCos.Text != "")
            {
                string[] dateComponents1 = txtfromdateCos.Text.Split('/');
                string formattedDate1 = dateComponents1[1] + "/" + dateComponents1[0] + "/" + dateComponents1[2];

                string[] dateComponents2 = txttodateCos.Text.Split('/');
                string formattedDate2 = dateComponents2[1] + "/" + dateComponents2[0] + "/" + dateComponents2[2];

                DateTime datefrom = DateTime.ParseExact(formattedDate1, "MM/dd/yyyy", null);
                DateTime dateto = DateTime.ParseExact(formattedDate2, "MM/dd/yyyy", null);

                if (datefrom > dateto)
                {
                    string script = "Swal.fire('Warning!', 'From Date can not be greater than To Date!', 'warning');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    txtfromdateCos.Text = "";
                    txttodateCos.Text = "";
                    return;
                }


            }
            DataSet dsList = couseListBAL.SeachCOSCauseListByDate(txtfromdateCos.Text, txttodateCos.Text);
            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        grdCaseList.DataSource = dsList.Tables[0].DefaultView;
                        Session["grdCaseList"] = dsList.Tables[0].DefaultView;
                        grdCaseList.DataBind();

                    }
                }
            }

        }

        protected void btnsearchRRC_Click(object sender, EventArgs e)
        {
            if (txtFromDateRRC.Text == "")
            {
                string script = "Swal.fire('Warning!', 'Please select Hearing Date!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                return;
            }
            if (txtFromToRRC.Text == "")
            {
                string script = "Swal.fire('Warning!', 'Please select Next Hearing Date!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                return;
            }
            if (txtFromDateRRC.Text != "" && txtFromToRRC.Text != "")
            {
                string[] dateComponents1 = txtFromDateRRC.Text.Split('/');
                string formattedDate1 = dateComponents1[1] + "/" + dateComponents1[0] + "/" + dateComponents1[2];

                string[] dateComponents2 = txtFromToRRC.Text.Split('/');
                string formattedDate2 = dateComponents2[1] + "/" + dateComponents2[0] + "/" + dateComponents2[2];

                DateTime datefrom = DateTime.ParseExact(formattedDate1, "MM/dd/yyyy", null);
                DateTime dateto = DateTime.ParseExact(formattedDate2, "MM/dd/yyyy", null);

                if (datefrom > dateto)
                {
                    string script = "Swal.fire('Warning!', 'From Date can not be greater than To Date!', 'warning');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    txtFromDateRRC.Text = "";
                    txtFromToRRC.Text = "";
                    return;
                }

            }
            DataSet dsList = couseListBAL.SeachRRCCauseListByDate(txtFromDateRRC.Text, txtFromToRRC.Text);
            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        GrdRRCCaseList.DataSource = dsList.Tables[0].DefaultView;
                        GrdRRCCaseList.DataBind();
                        Session["GrdRRCCaseList"] = dsList.Tables[0].DefaultView;

                    }
                }
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
        protected void grdCaseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (Session["USER_ID"] != null)
            //{
            string V_INSERTED_BY = "2254";/*Session["USER_ID"].ToString();*/
            string V_SYSTEM_IP = GetLocalIPAddress();

            LinkButton lnkView = (LinkButton)e.CommandSource;
            string App_ID = lnkView.CommandArgument;

            int RowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            LinkButton lbSaveRemarkCOS = (LinkButton)grdCaseList.Rows[RowIndex].FindControl("lbSaveRemarkCOS");

            if (lbSaveRemarkCOS.Text == "Save" || lbSaveRemarkCOS.Text == "Add")
            {
                if (e.CommandName == "SaveRemarkCOS")
                {


                    //LinkButton lnkView = (LinkButton)e.CommandSource;
                    //string App_ID = lnkView.CommandArgument;

                    //int RowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
                    string NextDateCOS = ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtNHdateCOS")).Text;

                    string HearingDateCOS = txtHearingDate.Text;

                    if (NextDateCOS == "")
                    {
                        string script = "Swal.fire('Warning!', 'Please select Next Hearing Date!', 'warning');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        //((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtRemarkCOS")).Text = "";
                        return;

                    }
                    //if (NextDateCOS != "")
                    //{
                    //    DateTime _dtFrom = DateTime.Now;
                    //    string[] dateComponent = NextDateCOS.Split('/');
                    //    string formatedDate = dateComponent[1] + "/" + dateComponent[0] + "/" + dateComponent[2];


                    //    DateTime dateto = DateTime.ParseExact(formatedDate, "MM/dd/yyyy", null);

                    //    if (_dtFrom < dateto)
                    //    {
                    //        string script = "Swal.fire('Warning!', 'Next Hearing Date should be future date!', 'warning');";
                    //        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    //        ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtNHdateCOS")).Text = "";
                    //        return;
                    //    }
                    //}
                    string RemarkCOS = ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtRemarkCOS")).Text;
                    string RemarkL_COS = ((LinkButton)grdCaseList.Rows[RowIndex].FindControl("lbSaveRemarkCOS")).Text;
                    if (RemarkCOS == "")
                    {
                        string script = "Swal.fire('Warning!', 'Please Add Remark!', 'warning');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        return;
                    }
                    //converting  NextDateCOS date
                    string[] dateComponents = NextDateCOS.Split('/');
                    string formattedDate = dateComponents[1] + "/" + dateComponents[0] + "/" + dateComponents[2];
                    DateTime NHDate = Convert.ToDateTime(formattedDate);


                    //write link button click event code here
                    DataSet dtUp = couseListBAL.InsertCOSRemarkNDate(App_ID, NHDate, RemarkCOS, V_INSERTED_BY, V_SYSTEM_IP);
                    if (dtUp.Tables[0].Rows.Count > 0)
                    {
                        // Inserted successfully
                        string script = "Swal.fire('Success!', 'Inserted successfully!', 'success');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtRemarkCOS")).ToolTip = RemarkL_COS;


                        ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtRemarkCOS")).ReadOnly = true;



                        lbSaveRemarkCOS.Text = "Edit";
                    }
                    else
                    {
                        // Case not available for the next hearing
                        string script = "Swal.fire('Error!', 'Cases are not available for the next hearing', 'error');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }

                }
                //}
            }
            else
            {
                if (e.CommandName == "SaveRemarkCOS")
                {

                    ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtRemarkCOS")).ReadOnly = false;
                    lbSaveRemarkCOS.Text = "Save";

                }
            }


        }

        

        protected void GrdRRCCaseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            //if (Session["USER_ID"] != null)
            //{
            string V_INSERTED_BY = "2254";/*Session["USER_ID"].ToString();*/
            string V_SYSTEM_IP = GetLocalIPAddress();

            LinkButton lnkView = (LinkButton)e.CommandSource;
            string App_ID = lnkView.CommandArgument;

            int RowIndex = ((GridViewRow)((LinkButton)e.CommandSource).NamingContainer).RowIndex;
            LinkButton lbSaveRemarkRRC = (LinkButton)GrdRRCCaseList.Rows[RowIndex].FindControl("lbSaveRemarkRRC");


            // Check if the click count is even or odd
            if (lbSaveRemarkRRC.Text == "Save" || lbSaveRemarkRRC.Text == "Add")
            {
                if (e.CommandName == "SaveRemarkRRC")
                {
                    
                    string NextDateRRC = ((TextBox)GrdRRCCaseList.Rows[RowIndex].FindControl("txtNHdateRRC")).Text;

                    string HearingDateCOS = txtHearingDate.Text;

                    if (NextDateRRC == "")
                    {
                        string script = "Swal.fire('Warning!', 'Please select Next Hearing Date!', 'warning');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        return;

                    }
                    //if (NextDateRRC != "")
                    //{
                    //    DateTime _dtFrom = DateTime.Now;
                    //    string[] dateComponent = NextDateRRC.Split('/');
                    //    string formatedDate = dateComponent[1] + "/" + dateComponent[0] + "/" + dateComponent[2];


                    //    DateTime dateto = DateTime.ParseExact(formatedDate, "MM/dd/yyyy", null);

                    //    if (_dtFrom > dateto)
                    //    {
                    //        string script = "Swal.fire('Warning!', 'Next Hearing Date should be future date!', 'warning');";
                    //        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    //        ((TextBox)grdCaseList.Rows[RowIndex].FindControl("txtNHdateCOS")).Text = "";
                    //        return;
                    //    }
                    //}



                    string RemarkRRC = ((TextBox)GrdRRCCaseList.Rows[RowIndex].FindControl("txtRemarkRRC")).Text;
                    if (RemarkRRC == "")
                    {
                        string script = "Swal.fire('Warning!', 'Please Add Remark!', 'warning');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                        return;
                    }

                    string[] dateComponents = NextDateRRC.Split('/');
                    string formattedDate = dateComponents[1] + "/" + dateComponents[0] + "/" + dateComponents[2];
                    DateTime NHDate = Convert.ToDateTime(formattedDate);
                    // write link button click event code here
                    DataSet dtUp = couseListBAL.InsertRRCRemarkNDate(App_ID, NHDate, RemarkRRC, V_INSERTED_BY, V_SYSTEM_IP);
                    if (dtUp.Tables[0].Rows.Count > 0)
                    {
                        // Inserted successfully
                        string script = "Swal.fire('Success!', 'Inserted successfully!', 'success');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                        ((TextBox)GrdRRCCaseList.Rows[RowIndex].FindControl("txtRemarkRRC")).ToolTip = RemarkRRC;


                        ((TextBox)GrdRRCCaseList.Rows[RowIndex].FindControl("txtRemarkRRC")).ReadOnly = true;



                        lbSaveRemarkRRC.Text = "Edit";
                    }
                    else
                    {
                        // Case not available for the next hearing
                        string script = "Swal.fire('Error!', 'Cases are not available for the next hearing', 'error');";
                        ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    }
                }
                //}
            }
            else
            {
                if (e.CommandName == "SaveRemarkRRC")
                {

                    ((TextBox)GrdRRCCaseList.Rows[RowIndex].FindControl("txtRemarkRRC")).ReadOnly = false;
                    lbSaveRemarkRRC.Text = "Save";

                }
            }

        }

        protected void btnPrintPDFCOS_Click(object sender, EventArgs e)
        {
            //DataTable dt = Session["grdCaseList_test"] as DataTable;
            DataView dv = Session["grdCaseList"] as DataView;
            if (dv != null)
            {
                DataTable dt = dv.Table; 
                ExportToPdf(dt);                        
            }
            
        }

        protected void btnPrintdPDFRRC_Click(object sender, EventArgs e)
        {
            //DataTable dt = Session["GrdRRCCaseList"] as DataTable;
            //DataTable dt = new DataTable();
            //dt.Columns.AddRange(new DataColumn[3] { new DataColumn("Id", typeof(int)),
            //            new DataColumn("Name", typeof(string)),
            //            new DataColumn("Country",typeof(string)) });
            //dt.Rows.Add(1, "John Hammond", "United States");
            //dt.Rows.Add(2, "Mudassar Khan", "India");
            //dt.Rows.Add(3, "Suzanne Mathews", "France");
            //dt.Rows.Add(4, "Robert Schidner", "Russia");
            
            DataView dv = Session["GrdRRCCaseList"] as DataView;
            if (dv != null)
            {
                DataTable dt = dv.Table;
                ExportToPdf(dt);
            }
        }


        //public void ExportToPdf(DataTable myDataTable)
        //{
        //    DataTable dt = myDataTable;
        //    Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
        //    Font font13 = FontFactory.GetFont("ARIAL", 13);
        //    Font font18 = FontFactory.GetFont("ARIAL", 18);
        //    try
        //    {
        //        PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
        //        pdfDoc.Open();

        //        if (dt.Rows.Count > 0)
        //        {
        //            PdfPTable PdfTable = new PdfPTable(1);
        //            PdfTable.TotalWidth = 200f;
        //            PdfTable.LockedWidth = true;

        //            PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Employee Details", font18)));
        //            PdfPCell.Border = Rectangle.NO_BORDER;
        //            PdfTable.AddCell(PdfPCell);
        //            DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
        //            pdfDoc.Add(PdfTable);

        //            PdfTable = new PdfPTable(dt.Columns.Count);
        //            PdfTable.SpacingBefore = 20f;
        //            for (int columns = 0; columns <= dt.Columns.Count - 1; columns++)
        //            {
        //                PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Columns[columns].ColumnName, font18)));
        //                PdfTable.AddCell(PdfPCell);
        //            }

        //            for (int rows = 0; rows <= dt.Rows.Count - 1; rows++)
        //            {
        //                for (int column = 0; column <= dt.Columns.Count - 1; column++)
        //                {
        //                    PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
        //                    PdfTable.AddCell(PdfPCell);
        //                }
        //            }
        //            pdfDoc.Add(PdfTable);
        //        }
        //        pdfDoc.Close();
        //        Response.ContentType = "application/pdf";
        //        Response.AddHeader("content-disposition", "attachment; filename=dsejReport_" + DateTime.Now.Date.Day.ToString() + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.Date.Hour.ToString() + DateTime.Now.Date.Minute.ToString() + DateTime.Now.Date.Second.ToString() + DateTime.Now.Date.Millisecond.ToString() + ".pdf");
        //        System.Web.HttpContext.Current.Response.Write(pdfDoc);
        //        Response.Flush();
        //        Response.End();
        //    }
        //    catch (DocumentException de)
        //    {
        //    }
        //    // System.Web.HttpContext.Current.Response.Write(de.Message)
        //    catch (IOException ioEx)
        //    {
        //    }
        //    // System.Web.HttpContext.Current.Response.Write(ioEx.Message)
        //    catch (Exception ex)
        //    {
        //    }
        //}



        public void ExportToPdf(DataTable myDataTable)
        {
            DataTable dt = myDataTable;
            Document pdfDoc = new Document(PageSize.A4.Rotate(), 10, 10, 10, 10);
            Font font13 = FontFactory.GetFont("ARIAL", 13);
            Font font18 = FontFactory.GetFont("ARIAL", 18);
            try
            {
                PdfWriter writer = PdfWriter.GetInstance(pdfDoc, System.Web.HttpContext.Current.Response.OutputStream);
                pdfDoc.Open();

                if (dt.Rows.Count > 0)
                {
                    PdfPTable PdfTable = new PdfPTable(1);
                    PdfTable.TotalWidth = 200f;
                    PdfTable.LockedWidth = true;

                    PdfPCell PdfPCell = new PdfPCell(new Phrase(new Chunk("Cause List Details", font18)));
                    PdfPCell.Border = Rectangle.NO_BORDER;
                    PdfTable.AddCell(PdfPCell);
                    DrawLine(writer, 25f, pdfDoc.Top - 30f, pdfDoc.PageSize.Width - 25f, pdfDoc.Top - 30f, new BaseColor(System.Drawing.Color.Red));
                    pdfDoc.Add(PdfTable);

                    // Modify this part to set your desired column headings
                    string[] columnHeadings = { "ID", "Case Number", "Case Registration Date", "Reason For Which Case Is Assigned", "Details of Applicant Appellant", "Subject", "Hearing Date", "Next Hearing Date", "Notice ID"};
                    //string[] columnHeadings = { "Column1", "Column2", "Column3", /* ... */ };
                    PdfTable = new PdfPTable(columnHeadings.Length);
                    PdfTable.SpacingBefore = 20f;

                    for (int columns = 0; columns < columnHeadings.Length; columns++)
                    {
                        PdfPCell = new PdfPCell(new Phrase(new Chunk(columnHeadings[columns], font18)));
                        PdfTable.AddCell(PdfPCell);
                    }

                    for (int rows = 0; rows < dt.Rows.Count; rows++)
                    {
                        for (int column = 0; column < dt.Columns.Count; column++)
                        {
                            PdfPCell = new PdfPCell(new Phrase(new Chunk(dt.Rows[rows][column].ToString(), font13)));
                            PdfTable.AddCell(PdfPCell);
                        }
                    }
                    pdfDoc.Add(PdfTable);
                }
                pdfDoc.Close();
                Response.ContentType = "application/pdf";
                Response.AddHeader("content-disposition", "attachment; filename=Cause-List_" + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".pdf");
                System.Web.HttpContext.Current.Response.Write(pdfDoc);
                Response.Flush();
                Response.End();
            }
            catch (DocumentException de)
            {
                // Handle DocumentException
            }
            catch (IOException ioEx)
            {
                // Handle IOException
            }
            catch (Exception ex)
            {
                // Handle other exceptions
            }
        }

        private static void DrawLine(PdfWriter writer, float x1, float y1, float x2, float y2, BaseColor color)
        {
            PdfContentByte contentByte = writer.DirectContent;
            contentByte.SetColorStroke(color);
            contentByte.MoveTo(x1, y1);
            contentByte.LineTo(x2, y2);
            contentByte.Stroke();
        }




        //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        //{
        //    DataSet dsList = new DataSet();


        //    //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
        //    DateTime HearingDt = Convert.ToDateTime(DateTime.Now);
        //    CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        //    dsList = clsHearingBAL.GetRRC_HearingCount_COS();

        //    if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow DR in dsList.Tables[0].Rows)
        //        {
        //            try
        //            {
        //                if (DR["HearingDate_cos"] != null)
        //                {
        //                    string inputDateString = DR["HearingDate_cos"].ToString();
        //                    DateTime hearingDate = DateTime.ParseExact(inputDateString, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

        //                    // Format the DateTime object to the desired output format
        //                    string formattedDate = hearingDate.ToString("dd/MM/yyyy");

        //                    //string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
        //                    string systemDate = formattedDate.ToString().Split('/')[1] + "/" + formattedDate.ToString().Split('/')[0] + "/" + formattedDate.ToString().Split('/')[2];

        //                    //string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
        //                    if (e.Day.Date == Convert.ToDateTime(systemDate))
        //                    {
        //                        Literal literal1 = new Literal();
        //                        literal1.Text = "<br/>";
        //                        e.Cell.Controls.Add(literal1);
        //                        Label label1 = new Label();
        //                        Label label2 = new Label();
        //                        label1.Text = " COS " + Convert.ToString(DR["TotalCaseHearing"]);
        //                        label2.Text = " RRC " + Convert.ToString(DR["TotalCaseHearing"]);
        //                        //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
        //                        label1.Font.Size = new FontUnit(FontSize.Small);
        //                        e.Cell.Controls.Add(label1);
        //                        label1.Font.Size = new FontUnit(FontSize.Small);
        //                        e.Cell.Controls.Add(label2);
        //                        label2.Font.Size = new FontUnit(FontSize.Small);
        //                        //label1.ForeColor= System.Drawing.Color.LightGreen;
        //                        e.Cell.BackColor = System.Drawing.Color.LightGreen;
        //                        //e.Cell.ForeColor = System.Drawing.Color.White;
        //                    }
        //                }

        //            }
        //            catch (Exception)
        //            {

        //            }




        //        }


        //    }


        //    //GetPartyDetail();
        //}




    }
}