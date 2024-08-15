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
    public partial class FinalOrder_PendingList : System.Web.UI.Page
    {
        CoSFinalOrder_BAL clsFinalBAL = new CoSFinalOrder_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {

                    Session["HearingDate"] = null;                  
                    Session["HearingDate"] = null;
                    Session["Case_Status"] = null;
                    Session["Case_Number"] = null;
                    Session["Party_ID"] = null;
                    Session["NOTICE_PROCEEDING"] = null;
                    Session["NOTICE_DOCSPATH"] = null;
                    Session["Notice_ID"] = null;
                    Session["hearing_id"] = null;
                    Session["hearing_id_Final"] = null;
                    Session["status_id"] = null;
                    Session["Status_Id"] = null;
                    Session["AppID"] = null;
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
                DataTable dsList = new DataTable();
                dsList = clsFinalBAL.GetDetails_Pending_FinalOrder(DistricId, DROID);
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

                Session["HearingDate"] = grdCaseList.DataKeys[rowindex].Values["Hearing_Date"].ToString();
                string hearingdate = grdCaseList.DataKeys[rowindex].Values["hearingdate"].ToString();
                Session["HearingDate"]= grdCaseList.DataKeys[rowindex].Values["hearingdate"].ToString();
                Session["Case_Status"] = grdCaseList.DataKeys[rowindex].Values["Case_Status"].ToString();
                ViewState["Case_Number"] = grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString();
                Session["Case_Number"] = grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString();
                Session["Party_ID"] = grdCaseList.DataKeys[rowindex].Values["party_id"].ToString();
                int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["app_id"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Proposal_No"].ToString();
                string Caseno = (grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString());
                Session["NOTICE_PROCEEDING"] = grdCaseList.DataKeys[rowindex].Values["NOTICE_PROCEEDING"].ToString();
                Session["NOTICE_DOCSPATH"] = grdCaseList.DataKeys[rowindex].Values["NOTICE_DOCSPATH"].ToString();
                string Notice_ID = grdCaseList.DataKeys[rowindex].Values["Notice_ID"].ToString();
                Session["Notice_ID"] = grdCaseList.DataKeys[rowindex].Values["Notice_ID"].ToString();
                string hearing_id = grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString();
                Session["hearing_id"] = grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString();
                Session["hearing_id_Final"] = grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString();
                Session["status_id"] = grdCaseList.DataKeys[rowindex].Values["status_id"].ToString();
                Session["Status_Id"] = grdCaseList.DataKeys[rowindex].Values["status_id"].ToString();
                Session["AppID"]= grdCaseList.DataKeys[rowindex].Values["app_id"].ToString();
                DataSet dsList = new DataSet();
                dsList = clsFinalBAL.Get_OrderDetails_OrderPending(appid);
                DataTable ddt = clsFinalBAL.Get_TotalStampDuty_FinalOrder(appid);
                

                if (dsList != null)
                {
                    if (dsList.Tables.Count > 0)
                    {

                        if (dsList.Tables[0].Rows.Count > 0)
                        {
                            DataSet dsDecision = new DataSet();
                            dsDecision = clsFinalBAL.Get_OrderDetails_DutyCalculation(appid);
                            if (dsDecision != null)
                            {
                                if (dsDecision.Tables.Count > 0)
                                {

                                    if (dsDecision.Tables[0].Rows[0]["Final_Remark"].ToString() != "")
                                    {

                                        if (ddt.Rows.Count > 0)
                                        {
                                            if (Convert.ToDouble(ddt.Rows[0]["COS_TOTALSTAMP_GUIDEVALUE"].ToString()) != 0.0)
                                            {
                                                int Flag = 4;
                                               
                                               //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + hearingdate + "&App_Id=" + appid + "&AppNo=" + appid + "&Notice_ID=" + Notice_ID + "&Flag=" + Flag + "&Response_type=Final_Order" + "&Hearing_ID=" + hearing_id + "&Status_Id=" + Session["status_id"], false);
                                               Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_type=Final_Order", false);
                                            }
                                            else
                                            {
                                                int Flag = 3;

                                                //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + hearingdate + "&App_Id=" + appid + "&AppNo=" + appid + "&Notice_ID=" + Notice_ID + "&Flag=" + Flag + "&Response_type=Final_Order" + "&Hearing_ID=" + hearing_id + "&Status_Id=" + Session["status_id"], false);
                                                Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_type=Final_Order", false);
                                            }


                                        }

                                        else
                                        {
                                            int Flag = 3;

                                            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + hearingdate + "&App_Id=" + appid + "&AppNo=" + appid + "&Notice_ID=" + Notice_ID + "&Flag=" + Flag + "&Response_type=Final_Order" + "&Hearing_ID=" + hearing_id + "&Status_Id=" + Session["status_id"], false);
                                            Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_type=Final_Order", false);
                                        }

                                          
                                    }
                                    else
                                    {

                                        int Flag = 1;
                                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + Caseno + "&App_Id=" + appid + "&AppNo=" + Appno + "&Flag=" + Flag+ "&Notice_ID="+ Notice_ID);
                                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Session["HearingDate"] + "&Flag=" + "", false);
                                        //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + hearingdate + "&Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet" + "&hearing_id=" + hearing_id + "&Notice_Id=" + Notice_ID, false);
                                        Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet", false);
                                    }

                                }

                            }


                        }

                        else
                        {
                            int Flag = 1;

                            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + hearingdate + "&Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet" + "&hearing_id=" + hearing_id + "&Notice_Id=" + Notice_ID, false);
                            Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet", false);


                        }

                    }




                }


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