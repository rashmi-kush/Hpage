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
    public partial class Payment_List : System.Web.UI.Page
    {
        PaymentDetails_BAL PayDetailList = new PaymentDetails_BAL();
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
            int DROID = 0;

            if (Session["DROID"] != null)
            {
                DROID = Convert.ToInt32(Session["DROID"]);
            }
            //int DistricId = 24;
            //if (Session["DistrictID"] != null)
            //{
            //    DistricId = Convert.ToInt32(Session["DistrictID"]);
            //}
            try
            {
                DataSet dsList = new DataSet();
                dsList = PayDetailList.GetCaseListForCoS(DROID);
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

        //protected void lnkSelect_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        //if (Session["User"] == null)
        //        //{
        //        //    Response.Redirect("LoginPage.aspx");
        //        //    return;
        //        //}
        //        //pnlDetails.Visible = true;
        //        DataSet dsAppDetails = new DataSet();
        //        DataSet dsPartyDetails = new DataSet();
        //        DataSet dsDocDetails = new DataSet();
        //        LinkButton lnk = (LinkButton)sender;
        //        GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
        //        int rowindex = grdrow.RowIndex;

        //        int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
        //        string Appno = grdCaseList.DataKeys[rowindex].Values["Proposal_No"].ToString();
        //        string Case_Number = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
        //        //int Flag = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["Department_ID"].ToString());
        //        Session["FinalOrderdate"]= grdCaseList.DataKeys[rowindex].Values["FinalOrder_Date"].ToString();
        //        Response.Redirect("Payment_Details.aspx?AppId=" + appid + "&AppNo=" + Appno + "&CaseNo=" + Case_Number, false);




        //    }
        //    catch (Exception ex)
        //    {

        //    }
        //}
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
                Button lnk = (Button)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;

                int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                Session["AppID"] = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["App_ID"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Proposal_No"].ToString();
                Session["Appno"] = grdCaseList.DataKeys[rowindex].Values["Proposal_No"].ToString();
                string Case_Number = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
                Session["CaseNo"] = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
                string FinalOrder = grdCaseList.DataKeys[rowindex].Values["FinalOrder_Date"].ToString();
                //int Flag = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["Department_ID"].ToString());
                Session["FinalOrderdate"] = FinalOrder;
                Response.Redirect("Payment_Details.aspx", false);




            }
            catch (Exception ex)
            {

            }
        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            DataSet dsList = new DataSet();
            dsList = PayDetailList.SeachNewProposal(txtsearch.Text, txtfromdate.Text, txttodate.Text);
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