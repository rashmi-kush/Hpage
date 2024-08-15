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
    public partial class AcceptRejectCases : System.Web.UI.Page
    {
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
                    Session["AppId"] = null;
                    Session["AppID"] = null;
                    Session["App_Id"] = null;
                    Session["AppNo"] = null;
                    Session["Appno"] = null;
                    Session["Flag"] = null;
                    Session["Case_Status"] = null;
                    
                    Session["Case_Number"] = null;
                                      
                    Session["HearingDate"] = null;
                    Session["Case_Status"] = null;
                    Session["Notice_ID"] = null;
                    Session["ProposalID"] = null;
                    Session["hearing_id"] = null;


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
            int  DistricId = 0;
            int DRID = 0;
            int DRO_ID = 0;
            if (Session["DistrictID"] != null)
            {
                DistricId = Convert.ToInt32(Session["DistrictID"]);
            }
            if (Session["DRID"] != null)
            {
                DRID = Convert.ToInt32(Session["DRID"]);
            }
            if (Session["DROID"] != null)
            {
                DRO_ID = Convert.ToInt32(Session["DROID"]);
            }
            try
            {
                DataSet dsList = new DataSet();
                dsList = objClsNewApplication.GetCaseListForCoS(DistricId, DRO_ID);
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
                //if (Session["User"] == null)
                //{
                //    Response.Redirect("LoginPage.aspx");
                //    return;
                //}
                //pnlDetails.Visible = true;
                DataSet dsAppDetails = new DataSet();
                DataSet dsPartyDetails = new DataSet();
                DataSet dsDocDetails = new DataSet();
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;

                int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Application_NO"].ToString();
                int Flag = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["Department_ID"].ToString());

                Session["AppId"] = appid;
                Session["App_Id"] = appid;
                Session["AppNo"] = Appno;
                Session["Flag"] = Flag;

                Session["AppId"] = appid;
                Session["AppID"] = appid;
                Session["App_Id"] = appid;
                Session["AppNo"] = Appno;
                Session["Appno"] = Appno;



                Response.Redirect("AcceptRejectCases_details.aspx", false);




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
            dsList = objClsNewApplication.SeachNewProposal(txtsearch.Text, txtfromdate.Text, txttodate.Text);
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