using eSigner;
using Newtonsoft.Json.Linq;
using RestSharp;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using System.Collections;

namespace CMS_Sampada.CoS
{
    public partial class Notice_Pending : System.Web.UI.Page
    {
        CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {

                if (!IsPostBack)
                {
                    Session["Case_Number"] = null;
                    Session["CaseNum"] = null;
                    Session["AppID"] = null;
                    Session["AppId"] = null;
                    Session["App_Id"] = null;
                    Session["Appno"] = null;
                    Session["AppNo"] = null;
                    Session["HearingDate"] = null;
                    Session["Case_Status"] = null;
                    Session["Notice_ID"] = null;
                    Session["ProposalID"] = null;
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
                dsList = clsNoticeBAL.GetApplicationDetails_NoticesheetCoS(DistricId, DROID);
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

        private void GetPartyDetailcount()
        {
            StringBuilder sb = new StringBuilder();
            if (Session["Case_Number"] != null)
            {
                DataTable dt = clsNoticeBAL.GetPartyDeatil_NoticePending(Session["Case_Number"].ToString(), Convert.ToInt32(Session["AppID"]), Session["Appno"].ToString());
                if (dt.Rows.Count > 0)
                {
                    //foreach (GridViewRow gvrow in grdCaseList.Rows)
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        

                            string partyid = dt.Rows[i]["Party_Id"].ToString();
                        sb.Append(partyid);
                            sb.Append(",");
                        
                        //if (label.Text)
                        //{

                        //}

                    }
                    if (sb.ToString() != "")
                    {
                        sb = new StringBuilder(sb.ToString().Substring(0, sb.ToString().Length - 1));
                        string Partyidram = sb.ToString();
                        Session["Partyidram"] = Partyidram;
                    }


                }
                ViewState["PartyDetail"] = dt;
               
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
                Session["Case_Status"] = grdCaseList.DataKeys[rowindex].Values["Case_Status"].ToString();
                //Session["Party_ID"] = grdCaseList.DataKeys[rowindex].Values["party_id"].ToString();
                int appid = Convert.ToInt32(grdCaseList.DataKeys[rowindex].Values["app_id"].ToString());
                string Appno = grdCaseList.DataKeys[rowindex].Values["Proposal_No"].ToString();
                string Caseno = (grdCaseList.DataKeys[rowindex].Values["Registered_CaseNO"].ToString());
                string NOTICE_DOCSPATH = (grdCaseList.DataKeys[rowindex].Values["NOTICE_DOCSPATH"].ToString());
                string Notice_ID = (grdCaseList.DataKeys[rowindex].Values["Notice_ID"].ToString());
                Session["FileNameUnSignedPDF"] = NOTICE_DOCSPATH;
                Session["Case_Number"] = Caseno;
                Session["CaseNum"]= Caseno;
                Session["AppID"] = grdCaseList.DataKeys[rowindex].Values["app_id"].ToString();
                Session["Appno"] = Appno;
                Session["ProposalID"] = Appno;
                //Session["NOTICE_PROCEEDING"] = grdCaseList.DataKeys[rowindex].Values["NOTICE_PROCEEDING"].ToString();
                //Session["NOTICE_DOCSPATH"] = grdCaseList.DataKeys[rowindex].Values["NOTICE_DOCSPATH"].ToString();
                string hearing_id = grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString();
                Session["Hearing_id"] = grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString();
                Session["Notice_Id"] = Notice_ID;
                GetPartyDetailcount();
                DataSet dsList = new DataSet();
                dsList = clsNoticeBAL.Get_NoticeDetails_NoticePending(Convert.ToInt32(hearing_id), "");
                if (dsList != null)
                {
                    if (dsList.Tables.Count > 0)
                    {
                        if (Session["Case_Status"].ToString() == "35" || Session["Case_Status"].ToString() == "36")
                        {
                            int Flag = 1;

                            Response.Redirect("Hearing_Notice.aspx?Flag=" + Flag + "&Response_Status=" + "");
                        }
                        if (Session["Case_Status"].ToString() == "38" || Session["Case_Status"].ToString() == "39")
                        {
                            int Flag = 0;

                            Response.Redirect("Hearing_Notice.aspx?Flag=" + Flag + "&Response_type=Next_Notice" + "&Response_Status=" + "0");
                        }
                        else
                        {
                            if (dsList.Tables[0].Rows.Count > 0)
                            {

                                int Flag = 0;
                                //Response.Redirect("PendingForNoticeDetails.aspx?Case_Number=" + Caseno + "&App_Id=" + appid + "&AppNo=" + Appno + "&Flag=" + Flag + "&hearing_id=" + hearing_id);

                                string Notice_Id = dsList.Tables[0].Rows[0]["notice_id"].ToString();
                                //Session["Notice_Id"] = Notice_Id;
                                //Response.Redirect("PendingForNoticeDetails.aspx?Case_Number=" + Caseno + "&App_Id=" + appid + "&AppNo=" + Appno + "&Flag=" + Flag + "&hearing_id=" + hearing_id);
                                //Response.Redirect("Notice.aspx?Case_Number=" + Caseno + "&App_Id=" + appid + "&AppNo=" + Appno + "&HearingDate=" + Session["HearingDate"] + "&Flag=" + Flag + "&Party_ID=" + Session["Partyidram"].ToString() + "&Notice_ID=" + Notice_Id + "&Response_type=Notice" + "&Response_Status=" + "1");
                                Response.Redirect("Notice.aspx?Flag=" + Flag + "&Response_type=Notice" + "&Response_Status=" + "0");

                            }

                            else
                            {
                                int Flag = 1;
                                //Response.Redirect("Notice.aspx?Case_Number=" + Caseno + "&App_Id=" + appid + "&AppNo=" + Appno + "&Flag=" + Flag + "&Response_Status=" + "");
                                Response.Redirect("Notice.aspx?Flag=" + Flag + "&Response_Status=" + "");

                            }
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