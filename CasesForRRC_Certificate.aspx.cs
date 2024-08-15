using SCMS_BAL;using System;using System.Collections.Generic;using System.Data;using System.Linq;using System.Web;using System.Web.UI;using System.Web.UI.WebControls;namespace CMS_Sampada.CoS{    public partial class CasesForRRC_Certificate : System.Web.UI.Page    {

        RRC_Certificate_Bal clsRRC_CertiBAL = new RRC_Certificate_Bal();                protected void Page_Load(object sender, EventArgs e)        {            try            {                                if (!IsPostBack)                {
                                        BindGrid();                                    }            }            catch (Exception)            {            }        }        private void BindGrid()        {            try            {                DataTable dt = clsRRC_CertiBAL.Get_RRC_CertificateCases();                if (dt.Rows.Count > 0)                {
                    Session["Status"] = dt.Rows[0]["STATUS_ID"];                 }                grdCaseList.DataSource = dt;                grdCaseList.DataBind();            }            catch (Exception ex)            {            }        }

        protected string GetStatusText(object statusId)
        {
            int id = Convert.ToInt32(statusId);
            switch (id)
            {
                case 44:
                case 45:
                case 49:
                case 50:
                case 51:
                    return "Pending RRC Certificate Ordersheet";
                case 88:
                    return "Final Submit RRC Certificate Ordersheet Pending";
                case 94:
                    return "Esign RRC Certificate Ordersheet Pending";
                case 95:
                case 96:
                    return "RRC Certificate Pending";
                case 89:
                    return "Final Submit RRC Certificate Pending";
                case 97:
                    return "Esign RRC Certificate Pending";
                default:
                    return "";
            }
        }        protected void lnkSelect_Click(object sender, EventArgs e)        {
            try
            {
                LinkButton lnk = (LinkButton)sender;
                GridViewRow grdrow = (GridViewRow)lnk.Parent.Parent;
                int rowindex = grdrow.RowIndex;
                string CaseNo = grdCaseList.DataKeys[rowindex].Values["Case_Number"].ToString();
                string AppId = grdCaseList.DataKeys[rowindex].Values["app_id"].ToString();
                string Appno = grdCaseList.DataKeys[rowindex].Values["INITIATION_ID"].ToString();
                string Notice_ID = grdCaseList.DataKeys[rowindex].Values["Notice_id"].ToString();
                //DateTime HearingDate = Convert.ToDateTime(grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString());
                string hearingDateString = grdCaseList.DataKeys[rowindex].Values["HearingDate"].ToString();
                string HearingID = (grdCaseList.DataKeys[rowindex].Values["hearing_id"].ToString());
                string ORDERSHEET_ID = (grdCaseList.DataKeys[rowindex].Values["OrderSheetInsertDate"].ToString());
                string InsertedDate = (grdCaseList.DataKeys[rowindex].Values["InsertedDate"].ToString());
               


                DateTime HearingDate;
                string[] formats = { "dd/MM/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "yyyy/MM/dd" }; // Add the formats you expect
                bool isHearingDateValid = DateTime.TryParseExact(hearingDateString, formats,
                                                                 System.Globalization.CultureInfo.InvariantCulture,
                                                                 System.Globalization.DateTimeStyles.None,
                                                                 out HearingDate);
                if (isHearingDateValid)
                {
                    Session["CaseNo"] = CaseNo;
                    Session["Notice_ID"] = Notice_ID;
                    Session["AppId"] = AppId;
                    Session["Appno"] = Appno;
                    Session["HearingID"] = HearingID;
                    Session["ORDERSHEET_ID"] = ORDERSHEET_ID;
                    Session["InsertedDate"] = InsertedDate;
                    Session["InsertedDate"] = InsertedDate;
                    Session["HearingDate"] = HearingDate;

                    if (new[] { 44, 45, 49, 50, 51, 94, 88 }.Contains(Convert.ToInt32(Session["Status"].ToString())))
                    {
                        Session["Status"] = "";
                        Response.Redirect("RRC_Certificate_Proceeding.aspx");
                    }
                    if (new[] { 95, 96, 89, 97 }.Contains(Convert.ToInt32(Session["Status"].ToString())))
                    {
                        Session["Status"] = "";
                        Response.Redirect("RRC_Certificate.aspx");
                    }
                }
                else
                {
                    // Handle the case where the date is not valid
                    ShowAlert("Error!", "The Hearing Date format is invalid. Please ensure it is in the correct format (dd/MM/yyyy).", "error");
                }
            }
            catch (Exception ex)
            {
               
                System.Diagnostics.Debug.WriteLine( ex.Message);
                return;
            }
        }

        protected void btn_Search_click(object sender, EventArgs e)        {            string fromDateText = txtfromdate.Text.Trim();            string toDateText = txttodate.Text.Trim();            string searchText = txtsearch.Text.Trim();            if (!string.IsNullOrEmpty(fromDateText) || !string.IsNullOrEmpty(toDateText))            {                if (string.IsNullOrEmpty(fromDateText))                {                    ShowAlert("Warning!", "Please select the From date for the search!", "warning");                    return;                }                if (string.IsNullOrEmpty(toDateText))                {                    ShowAlert("Warning!", "Please select the To date for the search!", "warning");                    return;                }                if (DateTime.TryParseExact(fromDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fromDate) &&                    DateTime.TryParseExact(toDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime toDate))                {                    if (fromDate > toDate)                    {                        ShowAlert("Warning!", "From Date cannot be greater than To Date!", "warning");                        txtfromdate.Text = "";                        txttodate.Text = "";                        return;                    }                }                else                {                    ShowAlert("Warning!", "Invalid date format. Please use dd/MM/yyyy.", "warning");                    return;                }            }            DataTable dt = clsRRC_CertiBAL.GET_RRC_CERTIFICATE_PENDING_CASES_BY_Search(Session["DROID"].ToString(), fromDateText, toDateText, searchText);            if (dt != null && dt.Rows.Count > 0)            {                grdCaseList.DataSource = dt;                grdCaseList.DataBind();            }            else            {                grdCaseList.DataSource = null;                grdCaseList.DataBind();                ShowAlert("Warning!", "No results found!", "warning");                txtsearch.Text = "";            }        }        private void ShowAlert(string title, string message, string icon)        {            string script = $"Swal.fire('{title}', '{message}', '{icon}');";            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);        }



        protected void Grd_RRC_Certi_Proced_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Find your controls
                LinkButton lnkSelect = (LinkButton)e.Row.FindControl("lnkSelect");

                // Get the FinalOrder_Date value as a string
                string finalOrderDateString = DataBinder.Eval(e.Row.DataItem, "FinalOrder_Date").ToString();

                // Define the expected date format
                string[] dateFormats = { "dd/MM/yyyy" };
                DateTime finalOrderDate;

                // Try to parse the date using the specified formats
                bool isValidDate = DateTime.TryParseExact(finalOrderDateString, dateFormats,
                                                          System.Globalization.CultureInfo.InvariantCulture,
                                                          System.Globalization.DateTimeStyles.None,
                                                          out finalOrderDate);

                //if (isValidDate)  // Enable or disable the button based on the difference
                //{
                //    int daysDifference = (DateTime.Now - finalOrderDate).Days;

                //    lnkSelect.Enabled = daysDifference >= 29;

                //}
                //else
                //{

                //    lnkSelect.Enabled = false;

                //}
            }
        }        protected void Grd_RRC_Certi_Proced_RowEditing(object sender, GridViewEditEventArgs e)        {            grdCaseList.EditIndex = e.NewEditIndex;
            //fill_Grid(); 
        }    }}