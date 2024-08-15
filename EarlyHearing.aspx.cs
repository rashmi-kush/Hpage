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
    public partial class EarlyHearing : System.Web.UI.Page
    {
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
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
                int DistricId = 47;
                int DRID = 307;
                if (Session["DistrictID"] != null)
                {
                    DistricId = Convert.ToInt32(Session["DistrictID"]);
                }
                if (Session["DRID"] != null)
                {
                    DRID = Convert.ToInt32(Session["DRID"]);
                }
                DataSet dsList = new DataSet();
                dsList = objClsNewApplication.GetCaseListForCoS(DistricId, DRID);
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
                DataSet dsAppDetails = new DataSet();
                DataSet dsPartyDetails = new DataSet();
                DataSet dsDocDetails = new DataSet();
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;

                int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Application_NO"].ToString();
                int Flag = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["Department_ID"].ToString());

                Response.Redirect("AcceptRejectCases_details.aspx?AppId=" + appid + "&AppNo=" + Appno + "&Flag=" + Flag, false);




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