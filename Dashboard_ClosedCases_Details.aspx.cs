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
    public partial class Dashboard_ClosedCases_Details : System.Web.UI.Page
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


    }
}