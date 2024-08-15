using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class MIS_Report : System.Web.UI.Page
    {
        COS_MISReport_BAL ClsMISReport = new COS_MISReport_BAL();

        string Flag = "";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                
                if (!IsPostBack)
                {
                    
                    BindCaseType();
                    BindNoticeCount();
                    BindStatus();
                    BindGrid("1");
                    Get_Department();
                    BindGrid("2");


                }
            }
            catch (Exception)
            {

            }
            
        }

        void BindCaseType()
        {
            try
            {
                DataSet dtCaseType = new DataSet();
                dtCaseType = ClsMISReport.GetCaseType();
                ddlCaseType.DataSource = dtCaseType;
                ddlCaseType.DataTextField = "CaseType";
                ddlCaseType.DataValueField = "ID";
                ddlCaseType.DataBind();
                ddlCaseType.Items.Insert(0, new ListItem("Case Type", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        void BindNoticeCount()
        {
            try
            {
                DataSet dtNoticeCount = new DataSet();
                dtNoticeCount = ClsMISReport.GetNoticeCount();
                ddlNoticeCount.DataSource = dtNoticeCount;
                ddlNoticeCount.DataTextField = "NoticeCount";
                ddlNoticeCount.DataValueField = "ID";
                ddlNoticeCount.DataBind();
                ddlNoticeCount.Items.Insert(0, new ListItem("Notice Count", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        void BindStatus()
        {
            try
            {
                DataSet dtStatus = new DataSet();
                dtStatus = ClsMISReport.GetStatus();
                ddlStatus.DataSource = dtStatus;
                ddlStatus.DataTextField = "Status";
                ddlStatus.DataValueField = "ID";
                ddlStatus.DataBind();
                ddlStatus.Items.Insert(0, new ListItem("Status", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        void Get_Department()
        {
            try
            {
                DataSet dt_Departmet = new DataSet();
                dt_Departmet = ClsMISReport.Get_Department();
                ddlCaseOrigin.DataSource = dt_Departmet;
                ddlCaseOrigin.DataTextField = "DEPARTMENT_NAME";
                ddlCaseOrigin.DataValueField = "DEPARTMENT_ID";
                ddlCaseOrigin.DataBind();
                ddlCaseOrigin.Items.Insert(0, new ListItem("Case Origin", "0"));
            }
            catch (Exception ex)
            {

            }
        }

        private void BindGrid( string Flag)
        {
            
            DateTime now = DateTime.Now;
            int app_id = 0;
            //string p_flag = "";
            string p_flag = "ALL";
            int p_case_origin = 0;
            string p_case_registered_frdt = "";
            string p_case_registered_todt = "";


            string p_hearingdate = "";
            int p_casetype =0;
            string p_paymentstatus = "";
            int p_noticecount = 0;
            int DROID = 0;
            int P_Status = 0;
            string Serach = "";

            if (txtHearingDate.Text != "")
            {
                p_hearingdate = (txtHearingDate.Text);
            }

            if (ddlCaseOrigin.SelectedIndex >0)
            {
                p_case_origin = Convert.ToInt32(ddlCaseOrigin.SelectedValue);
            }

            if (txtfromdate.Text != "")
            {
                
                 p_case_registered_frdt = (txtfromdate.Text);
            }

            if (txttodate.Text != "")
            {
                //p_case_registered_frdt = Convert.ToDateTime(txtfromdate.Text);
                DateTime ToDt = Convert.ToDateTime(txttodate.Text);
                //string date = DateTime.Now.ToString();
                p_case_registered_todt =(txttodate.Text);
                //p_case_registered_todt = Convert.ToDateTime(txttodate.Text);
            }


            if (ddlPaymentStatus.SelectedIndex > 0)
            {
                p_paymentstatus = (ddlPaymentStatus.SelectedItem.Text); 
            }

            if (txtSearch.Text != "")
            {
                Serach = txtSearch.Text;
            }
            if (ddlCaseType.SelectedIndex != 0)
            {
                p_casetype = Convert.ToInt32(ddlCaseType.SelectedValue);
            }


            if (ddlNoticeCount.SelectedIndex != 0)
            {
                p_noticecount = Convert.ToInt32(ddlNoticeCount.SelectedValue); 
            }

            if (ddlStatus.SelectedIndex != 0)
            {
                P_Status = Convert.ToInt32(ddlStatus.SelectedValue);
            }


            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]); 
            }
            if (Session["app_id"] != null)
            {
                app_id = Convert.ToInt32(Session["app_id"]);
            }

            //if (ddlNoticeCount.SelectedIndex > 0)
            //{
            //    p_noticecount = Convert.ToInt32(ddlNoticeCount.SelectedValue);

            //}

            try
            {
                DataSet dsList = new DataSet();
                dsList = ClsMISReport.GetMISReport( Flag, p_case_origin, p_case_registered_frdt, p_case_registered_todt, p_hearingdate, p_casetype, p_paymentstatus, p_noticecount, DROID);
                if (dsList != null)
                {
                    if (dsList.Tables.Count > 0)
                    {
                        if (dsList.Tables[0].Rows.Count > 0)
                        {
                            grdMISReport.DataSource = dsList;
                            grdMISReport.DataBind();


                        }
                    }

                    else
                    {
                        //grdMISReport.DataSource = dsList;
                        //grdMISReport.DataBind();
                    }

                        

                }
            }
            catch (Exception ex)
            {

            }
            //try
            //{
            //    DataSet dsList = new DataSet();
            //    dsList = ClsMISReport.GetMISReportFilter(txtHearingDate.Text, txtCaseOrigin.Text, txtPaymentStatus.Text, txtfromdate.Text, txttodate.Text);
            //    if (dsList != null)
            //    {
            //        if (dsList.Tables.Count > 0)
            //        {
            //            if (dsList.Tables[0].Rows.Count > 0)
            //            {
            //                grdMISReport.DataSource = dsList;
            //                grdMISReport.DataBind();

            //            }
            //        }
            //    }

            //}
            //catch(Exception ex)
            //{

            //}
        }

        protected void btnShow_Click(object sender, EventArgs e)
        {
            try
            {
                BindGrid("2");

            }
            catch (Exception)
            {

            }
        }
    }
}