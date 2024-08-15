using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCMS_BAL;
using System.Data;
namespace CMS_Sampada.CoS
{
    public partial class CollectorStampLegacyCases : System.Web.UI.Page
    {
        LegacyCases_BAL objlegacycase = new LegacyCases_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string caseType = Request.QueryString["caseType"];

                if (!string.IsNullOrEmpty(caseType))
                {

                    Session["CaseType"] = caseType;

                }
                fill_MasterData();
                BindGrid(Session["CaseType"].ToString());

            }

        }

        protected void GrdLegacyCase_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GrdLegacyCaseList.PageIndex = e.NewPageIndex;
            BindGrid(Session["CaseType"].ToString());
        }
        public void fill_MasterData()
        {
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            ddl_SROOffice.Items.Clear();
            ddl_CaseOrigin.Items.Clear();

            try
            {
                ds = objlegacycase.GET_MASTERS_DATA_TO_REGISTER();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column
                        ddl_CaseOrigin.Items.Add(new ListItem(text, value));
                    }
                }

                dt = objlegacycase.GetMasterSRO_Details();
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        string value = row[1].ToString();  // Assuming the second column is for the text
                        string  text = row[2].ToString();  // Assuming the third column is for the value
                        ddl_SROOffice.Items.Add(new ListItem(text, value));
                    }
                }


                // Insert default item at the top of each dropdown list
                ddl_SROOffice.Items.Insert(0, new ListItem("--Select SRO Office--", "0"));
                ddl_CaseOrigin.Items.Insert(0, new ListItem("--Select Case Origin--", "0"));

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        private void BindGrid(string caseType)
        {

            DataTable dt = null;

            try
            {
                dt = objlegacycase.GetALLRegisteredCases(caseType);

                if (dt != null)
                {
                    GrdLegacyCaseList.DataSource = dt;
                    GrdLegacyCaseList.DataBind();
                }
                else
                {
                    GrdLegacyCaseList.DataSource = null;
                    GrdLegacyCaseList.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }
        protected void btnsearch_Click(object sender, EventArgs e)
        {
            
            string fromDateText = txtfromdate.Text.Trim();
            string toDateText = txttodate.Text.Trim();
            string CaseNoText = txtCaseNo.Text.Trim();
            string sroOffice = "";
            string caseType = Session["CaseType"].ToString();
            if (ddl_SROOffice.SelectedItem.Text == "--Select SRO Office--")
            {
                sroOffice = "";
            }
            else 
            {
                sroOffice = ddl_SROOffice.SelectedItem.Text;
            }
            int caseOrigin = Convert.ToInt32(ddl_CaseOrigin.SelectedValue);


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


            if (string.IsNullOrEmpty(CaseNoText) && sroOffice == "" && caseOrigin == 0 && string.IsNullOrEmpty(fromDateText) && string.IsNullOrEmpty(toDateText))
            {
                ShowAlert("Warning!", "Please provide input to search!", "warning");
                return;
            }

            btnsearch.Enabled = false;
            DataTable dsList = objlegacycase.GetALLRegisteredCases(caseType, fromDateText, toDateText, CaseNoText, caseOrigin, sroOffice);
            if (dsList != null && dsList.Rows.Count > 0)
            {
                GrdLegacyCaseList.DataSource = dsList;
                GrdLegacyCaseList.DataBind();
                btnsearch.Enabled = true;

            }
            else
            {
                btnsearch.Enabled = true;
                GrdLegacyCaseList.DataSource = null;
                GrdLegacyCaseList.DataBind();
            }
        }

        protected string GetStatusText(object statusId)
        {
            int id = Convert.ToInt32(statusId);
            switch (id)
            {
                case 1101:
                    return "Pending Physical Case Submit By Reader";

                case 1201:
                    return "Pending Electronic Case Submit By Reader";
                default:
                    return "";
            }
        }
        protected void lnkSelect_Click(object sender, EventArgs e)
        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;
                string DocumentNo = GrdLegacyCaseList.DataKeys[rowindex].Values["Document_No"].ToString();
                string AppId = GrdLegacyCaseList.DataKeys[rowindex].Values["app_id"].ToString();
                string Status_id = GrdLegacyCaseList.DataKeys[rowindex].Values["status_id"].ToString();
                Session["Document_No"] = DocumentNo;
                Session["AppId"] = AppId;
                Session["Status"] = Status_id;
                if (new[] { 1101, 1201 }.Contains(Convert.ToInt32(Status_id)))
                {
                    Response.Redirect("CosViewLegacyCase.aspx");

                }


            }
            catch (Exception ex)
            {

                System.Diagnostics.Debug.WriteLine(ex.Message);
                return;
            }
        }
        private void ShowAlert(string title, string message, string icon, string redirectUrl = "")
        {
            string script = string.IsNullOrEmpty(redirectUrl)
                ? $"Swal.fire('{title}', '{message}', '{icon}');"
                : $"Swal.fire('{title}', '{message}', '{icon}').then((result) => {{ if (result.isConfirmed) {{ window.location.href = '{redirectUrl}'; }} }});";

            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
        }

    }
}