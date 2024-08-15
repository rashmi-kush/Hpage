using SCMS_BAL;using System;using System.Collections.Generic;using System.Data;using System.Linq;using System.Web;using System.Web.UI;using System.Web.UI.WebControls;using System.Net;using System.Net.Sockets;namespace CMS_Sampada.CoS{    public partial class Done_RRC_Certificate_Cases : System.Web.UI.Page    {

        RRC_Certificate_Bal clsRRC_CertiBAL = new RRC_Certificate_Bal();        protected void Page_Load(object sender, EventArgs e)        {            try            {                if (!IsPostBack)                {
                    int Flag = 0;

                    if (Request.QueryString["Flag"] != null)
                    {
                        Flag = Convert.ToInt32(Request.QueryString["Flag"]);
                        if (Request.QueryString["Flag"].ToString() == "1")// Success eSign
                        {
                            if (Request.QueryString["Response_From"] != null)
                            {
                                if (Request.QueryString["Response_From"].ToString() == "RRC_Certificate")
                                {

                                    DataTable dt = clsRRC_CertiBAL.InserteSignDSC_Status(Convert.ToInt32(Session["AppId"].ToString()), "1", "", GetLocalIPAddress(), Convert.ToInt32(Session["RRC_Certificate_id"].ToString()));

                                }

                            }

                        }

                    }


                    BindGrid();                }            }            catch (Exception)            {            }        }        private void BindGrid()        {            try            {

                //string date = DateTime.Now.ToString();

                DataTable dt = clsRRC_CertiBAL.Get_Completed_RRC_CertificateCases();                grdCaseList.DataSource = dt;                grdCaseList.DataBind();            }            catch (Exception ex)            {            }        }
        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            string ipAdd = "";
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    ipAdd = ip.ToString();
                    return ipAdd;
                }
            }
            return ipAdd;
        }        protected void btn_Search_click(object sender, EventArgs e)        {            string fromDateText = txtfromdate.Text.Trim();            string toDateText = txttodate.Text.Trim();            string searchText = txtsearch.Text.Trim();            if (!string.IsNullOrEmpty(fromDateText) || !string.IsNullOrEmpty(toDateText))            {                if (string.IsNullOrEmpty(fromDateText))                {                    ShowAlert("Warning!", "Please select the From date for the search!", "warning");                    return;                }                if (string.IsNullOrEmpty(toDateText))                {                    ShowAlert("Warning!", "Please select the To date for the search!", "warning");                    return;                }                if (DateTime.TryParseExact(fromDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime fromDate) &&                    DateTime.TryParseExact(toDateText, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out DateTime toDate))                {                    if (fromDate > toDate)                    {                        ShowAlert("Warning!", "From Date cannot be greater than To Date!", "warning");                        txtfromdate.Text = "";                        txttodate.Text = "";                        return;                    }                }                else                {                    ShowAlert("Warning!", "Invalid date format. Please use dd/MM/yyyy.", "warning");                    return;                }            }            DataTable dt = clsRRC_CertiBAL.GET_RRC_CERTIFICATE_CASES_BY_Search(Session["DROID"].ToString(), fromDateText, toDateText, searchText);            if (dt != null && dt.Rows.Count > 0)            {                grdCaseList.DataSource = dt;                grdCaseList.DataBind();            }            else            {                grdCaseList.DataSource = null;                grdCaseList.DataBind();                ShowAlert("Warning!", "No results found!", "warning");                txtsearch.Text = "";            }        }        private void ShowAlert(string title, string message, string icon)        {            string script = $"Swal.fire('{title}', '{message}', '{icon}');";            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);        }


    }}