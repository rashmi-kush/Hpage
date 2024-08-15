using RestSharp;
using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class ClosedCases_List : System.Web.UI.Page
    {
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
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

                DataTable dt = clsFinalOrderBAL.Get_ClosedCases_Dashboard(Session["DROID"].ToString());
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
            }
            catch (Exception ex)
            {

            }
        }

        protected void btn_Search_click(object sender, EventArgs e)
        {
            string fromDateText = txtfromdate.Text.Trim();
            string toDateText = txttodate.Text.Trim();
            string searchText = txtsearch.Text.Trim();

            //if (string.IsNullOrEmpty(fromDateText) && string.IsNullOrEmpty(toDateText) && string.IsNullOrEmpty(searchText))
            //{
            //    ShowAlert("Warning!", "Please enter Case/Proposal number or select the dates for the search!", "warning");
            //    return;
            //}

            if (!string.IsNullOrEmpty(fromDateText) || !string.IsNullOrEmpty(toDateText))
            {
                if (string.IsNullOrEmpty(fromDateText))
                {
                    ShowAlert("Warning!", "Please select the From date for the search!", "warning");
                    return;
                }

                if (string.IsNullOrEmpty(toDateText))
                {
                    ShowAlert("Warning!", "Please select the To date for the search!", "warning");
                    return;
                }

                if (DateTime.TryParseExact(fromDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fromDate) &&
                    DateTime.TryParseExact(toDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime toDate))
                {
                    if (fromDate > toDate)
                    {
                        ShowAlert("Warning!", "From Date cannot be greater than To Date!", "warning");
                        txtfromdate.Text = "";
                        txttodate.Text = "";
                        return;
                    }
                }
                else
                {
                    ShowAlert("Warning!", "Invalid date format. Please use dd/MM/yyyy.", "warning");
                    return;
                }
            }

            DataTable dt = clsFinalOrderBAL.GET_CLOSED_CASES_BY_Search(Session["DROID"].ToString(),fromDateText,toDateText,searchText);

            if (dt != null && dt.Rows.Count > 0)
            {
                grdCaseList.DataSource = dt;
                grdCaseList.DataBind();
            }
            else
            {
                grdCaseList.DataSource = null;
                grdCaseList.DataBind();
                ShowAlert("Warning!", "No results found!", "warning");
                txtsearch.Text = "";
            }
        }

        private void ShowAlert(string title, string message, string icon)
        {
            string script = $"Swal.fire('{title}', '{message}', '{icon}');";
            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
        }

        protected string Comsumedata(string DocumentType, int RegID)
        {
            string base64 = "";


            try
            {

                //var Token = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJtYW5vai5kcm8uaGFyZGEuYXBwcm92ZXJAbXAuZ292LmluIiwiaXAiOiIxMDMuMTYwLjQ5LjEzNSIsInVzZXJBZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS8xMjQuMC4wLjAgU2FmYXJpLzUzNy4zNiIsImV4cCI6MTcxMzg4MDE0MCwiaWF0IjoxNzEzODc2NTQwfQ.J8LUONwCcxtSz8nu0PQEisn9WL5iZjTfqoVK-8EWEl29T7TqnB9TOnLQqIaFW9OSGtJYOi8DIbU7mhB-6vh-0OdHuiUcs8GbYVUwZk8gYT6j2JU2ON9k-XLaBb0FiakKLnpLEOPe2t4Rr2KGBBuZXSop4Uk47RG6907OAh9ZcRy-aTNC82MHCYL5sgaUlYO9ATUMWgnR1yihrfoKOqzFJ2S92vk_vac9FcFfABtYfaK2VLngH2YxrZYmQaWaWu58Q_UV4zH3n088mdtSrReqJcVzk7CpTrz09yTcrNHbN7peD_NLnytSbkoLDgVA8y80my9o2Qm9pkFlRF_Krtt-7g";
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //var client = new RestClient("https://ersuat2.mp.gov.in/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");     //UAT

                var client = new RestClient(RegProposalAttDocument_url + DocumentType + "/" + RegID + "");        //PROD
                                                                                                                  //var client = new RestClient("RegProposalAttDocument_url" + DocumentType + "/" + RegID + "");
                var request = new RestRequest(Method.POST);
                //request.AddHeader("Content-Type", "application/json");
                //request.AddHeader("Authorization", Token);
                request.AddHeader("Authorization", Session["Token"].ToString());
                //request.AddHeader("Authorization", tocan);
                //request.AddParameter("userid", "1");
                request.RequestFormat = DataFormat.Json;
                IRestResponse response = client.Execute(request);
                string Result = response.Content;
                //string base64 = "";
                if (Result != "")
                {
                    JavaScriptSerializer oJS = new JavaScriptSerializer();
                    oJS.MaxJsonLength = 2147483647;
                    ResonseA resonse = oJS.Deserialize<ResonseA>(Result);
                    base64 = resonse.responseData;
                    //bytes= Convert.FromBase64String(resonse.responseData);
                    //Response.Write(base64);
                }
                else
                {
                    //Response.Write("Data not found");
                    base64 = null;
                }
                // Generate a unique file name

                //string encodedPdfData = "data:application/pdf;base64," + base64 + "";


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
            return base64;

        }
        protected void grdCaseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName== "SelectApplication")
            {
                 Session["AppID"] = e.CommandArgument.ToString().Split(',')[0].ToString();
                Session["CaseNo"] = e.CommandArgument.ToString().Split(',')[1].ToString();
                Session["Appno"] = e.CommandArgument.ToString().Split(',')[2].ToString();

                Response.Redirect("ClosedCases_Details.aspx");
            }
            
        }
    }
}