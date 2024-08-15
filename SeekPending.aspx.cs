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
    public partial class SeekPending : System.Web.UI.Page
    {
        ReportSeeking_BAL ReportSeek_BAL = new ReportSeeking_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    BindCaseList();
                }
            }
            catch (Exception)
            {

            }
        }
        void BindCaseList()
        {

            try
            {
                DataTable dsList = new DataTable();
                dsList = ReportSeek_BAL.GetApplicationDetails_SeakpendingCoS();
                if (dsList != null)
                {
                    if (dsList.Rows.Count > 0)
                    {
                        grdCaseList.DataSource = dsList;
                        //grdCaseList.DataSource = dsList.Tables[0].DefaultView;
                        grdCaseList.DataBind();
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
                DataSet dsAppDetails = new DataSet();
                DataSet dsPartyDetails = new DataSet();
                DataSet dsDocDetails = new DataSet();
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;

                //int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString();
                //int Flag = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["Department_ID"].ToString());

                Response.Redirect("ReportSeeking.aspx?Case_Number=" + Appno );




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
    }
}