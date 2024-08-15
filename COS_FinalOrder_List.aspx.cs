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
    public partial class COS_FinalOrder_List : System.Web.UI.Page
    {
        CoSFinalOrder_BAL objClsFinalOrder = new CoSFinalOrder_BAL();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //if (Session["User"] == null)
                //{
                //    Response.Redirect("LoginPage.aspx");
                //    return;
                //}
                if (!IsPostBack)
                {
                    ViewState["Case_Number"] = "";
                    if (Request.QueryString["Case_Number"] != null)
                    {
                        ViewState["Case_Number"] = Request.QueryString["Case_Number"].ToString();
                        ViewState["HearingDate"] = Request.QueryString["Hearing"].ToString();
                        string HearingDt = Request.QueryString["Hearing"].ToString();
                        
                    }
                    BindCaseList();
                    //GetHead();
                    //GetDharas();
                }
            }
            catch (Exception)
            {

            }
        }
        void BindCaseList()
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
                DataSet dsList = new DataSet();
                dsList = objClsFinalOrder.GetFurtherFinalOrderList(DistricId, DROID);
                if (dsList != null)
                {
                    if (dsList.Tables.Count > 0)
                    {
                        if (dsList.Tables[0].Rows.Count > 0)
                        {
                            grdCaseList.DataSource = dsList.Tables[0].DefaultView;
                            grdCaseList.DataBind();
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
        }
        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;
                string CaseNumber = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
                Session["CaseNumber"] = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
                DateTime Hearing = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString());
                Session["Hearing"] = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString());
                DateTime Further_ExeDt = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["Further_Execution_Date"].ToString());
                Session["Further_ExeDt"] = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["Further_Execution_Date"].ToString());
                Session["AppID"] = grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString();
                Session["Appno"] = (grdCaseList.DataKeys[rowindex].Values["application_no"].ToString());
                string Hearing_ID = grdCaseList.DataKeys[rowindex].Values["Hearing_ID"].ToString();
                Session["Hearing_ID"] = grdCaseList.DataKeys[rowindex].Values["Hearing_ID"].ToString();
                string Notice_Id = grdCaseList.DataKeys[rowindex].Values["notice_id"].ToString();
                Session["Notice_Id"] = grdCaseList.DataKeys[rowindex].Values["notice_id"].ToString();
                //string Notice_Id = "";
                Response.Redirect("Final_Order_Further.aspx", false);



            }
            catch (Exception ex)
            {

            }
        }

        protected void ddlAct_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {

            }
            catch (Exception)
            {

            }
        }
        protected void btnResubmit_Click(object sender, EventArgs e)
        {

        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataSet dsList = new DataSet();
            dsList = objClsFinalOrder.SeachNewProposal(txtsearch.Text, txtfromdate.Text, txttodate.Text);
            if (dsList != null)
            {
                if (dsList.Tables.Count > 0)
                {
                    if (dsList.Tables[0].Rows.Count > 0)
                    {
                        grdCaseList.DataSource = dsList.Tables[0].DefaultView;
                        grdCaseList.DataBind();

                    }
                }
            }
        }
    }
}