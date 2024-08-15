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
    public partial class Dashboard_RRCCertificate_Details : System.Web.UI.Page
    {
        CoSFinalOrder_BAL clsFinalOrderBAL = new CoSFinalOrder_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    BindGrid();
                }
            }
            catch (Exception)
            {

            }
        }

        private void BindGrid()
        {
            try
            {
                
                //string date = DateTime.Now.ToString();
                
                DataTable dt = clsFinalOrderBAL.Get_RRCCertifcate_Dashboard();
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
            Response.Redirect("Hearing.aspx?Case_Number=" + CaseNumber + "&NoticeId=" + Noticeid + "&Hearing=" + Hearing + "&hearing_id=" + HearingID, false);
            //string CaseNumber = grdCaseList.DataKeys[row.RowIndex].Values[0].ToString();
            //Response.Redirect("Hearing.aspx?Case_Number=" + CaseNumber + "&NoticeId=" + Noticeid + "&Hearing=" + Hearing, false);
        }
    }
}