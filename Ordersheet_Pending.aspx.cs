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
    public partial class Ordersheet_Pending : System.Web.UI.Page
    {
        Encrypt Encrypt = new Encrypt();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {

            try
            {

                if (!IsPostBack)
                {
                    Session["ORDRSHEETPATH"] = null;
                    Session["Case_Number"] = null;
                    Session["CaseNum"] = null;
                    Session["Proposalno"] = null;
                    Session["ProposalID"] = null;
                    Session["AppID"] = null;
                    Session["AppId"] = null;
                    Session["App_Id"] = null;
                    Session["IMPOUND_DATE"] = null;
                    Session["ProImpoundDate"] = null;
                    Session["Case_Status"] = null;
                    Session["HearingDate"] = null;
                    Session["Notice_ID"] = null;

                    Session["hearing_id"] = null;
                    Session["Flag"] = null;


                    BindCaseList();
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
            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }
            try
            {
                DataTable dsList = new DataTable();
                dsList = OrderSheet_BAL.GetApplicationDetails_OrdersheetCoS(DistricId, DROID);
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

                string Case_Status = grdCaseList.DataKeys[rowindex].Values["Case_Status"].ToString();
                Session["ordersheet"] = grdCaseList.DataKeys[rowindex].Values["proceeding"].ToString();
                string appid = (grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                string Case_Number = (grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString());
                string Proposalno = (grdCaseList.DataKeys[rowindex].Values["PROPOSAL_NO"].ToString());
                string IMPOUND_DATE = (grdCaseList.DataKeys[rowindex].Values["IMPOUND_DATE"].ToString());
                Session["ORDRSHEETPATH"] = grdCaseList.DataKeys[rowindex].Values["ORDRSHEETPATH"].ToString();
                Session["Case_Number"] = Case_Number;
                Session["CaseNum"] = Case_Number;
                Session["Proposalno"] = Proposalno;
                Session["ProposalID"] = Proposalno;
                Session["Appno"] = Proposalno;
                Session["AppID"] = appid;
                Session["AppId"] = appid;
                Session["App_Id"] = appid;

                Session["IMPOUND_DATE"] = IMPOUND_DATE;

                Session["ProImpoundDate"] = IMPOUND_DATE;
                Session["Case_Status"] = Case_Status;
                //Response.Redirect("Ordersheet.aspx", false);

                DataTable dt = OrderSheet_BAL.Get_OrderSheetID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    string Ordersheet_id = dt.Rows[0]["Ordersheet_id"].ToString();
                    Response.Redirect("Ordersheet.aspx", false);
                }

                int caseStatus = Convert.ToInt32(Session["Case_Status"].ToString());


                if ((caseStatus >= 13 && caseStatus <= 15) || (caseStatus >= 17 && caseStatus <= 19) || (caseStatus >= 21 && caseStatus <= 23))
                {
                    Response.Redirect("ReportSeeking.aspx", false);
                }

                else if ((caseStatus == 2 || caseStatus == 16 || caseStatus == 20 || caseStatus == 24))
                {
                    Response.Redirect("Ordersheet.aspx", false);
                }
                
                else
                {
                    Response.Redirect("Ordersheet.aspx", false);
                }
                //}


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