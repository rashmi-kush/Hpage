using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using SCMS_BAL;
using System.Data;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace CMS_Sampada.CoS
{
    public partial class Cause_List_Upload : System.Web.UI.Page
    {
        CosTemplate_BAL couseListBAL = new CosTemplate_BAL();
        //CoSCauseList_BAL couseListBAL = new CoSCauseList_BAL();

        int selectedCount = 0;
        int EditTemplateId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {

                fill_Grid();
            }

        }

        public void fill_Grid()
        {
            DataTable dt = null;

            try
            {
                string searchText = txtsearch.Text;
                if (ddlSearchType.SelectedValue=="2" || ddlSearchType.SelectedValue == "3")
                {
                    searchText = txtsearchdate.Text.ToString().Split('/')[2]+ txtsearchdate.Text.ToString().Split('/')[1]+ txtsearchdate.Text.ToString().Split('/')[0];
                }


                dt = couseListBAL.GetCauseList(Convert.ToInt32(Session["DistrictID"].ToString()), ddlSearchType.SelectedValue, searchText);

                if (dt != null)
                {
                    Grd_CauseList.DataSource = dt;
                    Grd_CauseList.DataBind();
                }
                else
                {
                    Grd_CauseList.DataSource = null;
                    Grd_CauseList.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }





        protected void btnsearch_Click(object sender, EventArgs e)
        {
            //if (ddlSearchType.SelectedValue == "0")
            //{
            //    string script = "Swal.fire('Warning!', 'Please select the search type of cause list!', 'warning');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

            //    return;
            //}
            //if (string.IsNullOrWhiteSpace(txtsearch.Text) || string.IsNullOrWhiteSpace(txtsearchdate.Text))
            //{
            //    string script = "Swal.fire('Warning!', 'Please enter cause list name or date!', 'warning');";
            //    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

            //    return;
            //}
            fill_Grid();
           
        }



        protected void btndelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Grd_CauseList.Rows)
            {
                try
                {
                    System.Web.UI.WebControls.CheckBox chkbx = (System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBoxCauseList");
                    HiddenField hdnCauseListID = (HiddenField)row.FindControl("hdnCauseListID");

                    if (chkbx.Checked)
                    {
                        selectedCount++;
                        int selectedCauseListID = Convert.ToInt32(hdnCauseListID.Value);
                        DataSet Result = new DataSet();
                        Result = couseListBAL.CauseListDelete(selectedCauseListID, Convert.ToInt32(Session["DistrictID"].ToString()));

                        if (Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                        {
                            string messageFromDatabase = Result.Tables[0].Rows[0][0].ToString();

                            if (messageFromDatabase == "Record has been deleted!")
                            {

                                string script = "Swal.fire('Success!', 'Successfully Record Deleted!', 'success');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'Cause_List_Upload.aspx'; }, 3000);", true);
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }

            }



            if (selectedCount < 1)
            {
                string script = "Swal.fire('Warning!', 'Select the record you want to delete!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;

            }

        }



        protected string TruncateText(object text, int maxLength)
        {
            string originalText = Convert.ToString(text);
            if (originalText.Length <= maxLength)
            {
                return originalText;
            }
            else
            {
                return originalText.Substring(0, maxLength) + "...";
            }
        }

        public static string GetLocalIPAddress()
        {
            var host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (var ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    return ip.ToString();
                }
            }
            return "No network adapters with an IPv4 address in the system!";
        }
        private bool IsValidExtension(string fileName)
        {
            bool isValid = false;
            string[] fileExtension = { ".doc", ".docx", ".pdf" };
            for (int i = 0; i <= fileExtension.Length - 1; i++)
            {
                if (fileName.Contains(fileExtension[i]))
                {
                    isValid = true;
                    break;
                }

            }
            return isValid;
        }

        private void Reset()
        {
            CoSUpload_Doc.PostedFile.InputStream.Dispose();
            CoSUpload_Doc.Dispose();
        }
        protected void btnUpload_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrWhiteSpace(txtCLDate.Text))
            {
                string script = "Swal.fire('Warning!', 'Please enter the cause list date!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }
            if (string.IsNullOrWhiteSpace(txtCLName.Text))
            {
                string script = "Swal.fire('Warning!', 'Please enter the cause list name!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }
            if (CoSUpload_Doc.HasFile == false)
            {
                string script = "Swal.fire('Warning!', 'Please Choose the cause list pdf file!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }

            else
            {
                try
                {
                    int iFileSize = 0;

                    DataSet Result = new DataSet();

                    if (btnUpload.Text == "Upload")
                    {

                        if (CoSUpload_Doc.HasFiles)
                        {
                            if (IsValidExtension(CoSUpload_Doc.FileName))
                            {

                                foreach (HttpPostedFile postedFile in CoSUpload_Doc.PostedFiles)
                                {
                                    iFileSize = postedFile.ContentLength;
                                    if (iFileSize > 5048570)  // 1MB 302941
                                    {
                                        //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Maximum file size should not be more than 1 MB..!', '', 'error')", true);
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageMaxSize();</script>");
                                        return;
                                    }


                                    string fileName = Path.GetFileName(postedFile.FileName);
                                    string docpath = DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;
                                    string docpath1 = "~/CauseList_Pdf/" + DateTime.Now.Date.ToString("ddMMyyyy") + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString() + '_' + fileName;

                                    postedFile.SaveAs(Server.MapPath(docpath1));

                                    string Causelistdt = txtCLDate.Text;
                                    string[] dateComponents = Causelistdt.Split('/');

                                    string CauselistDate = dateComponents[0] + "/" + dateComponents[1] + "/" + dateComponents[2];

                                    //DateTime CauseListDate = Convert.ToDateTime(CauselistDate);


                                    Result = couseListBAL.Insert_Cause_List(txtCLName.Text, txtCLDate.Text, docpath1, Convert.ToInt32(Session["DistrictID"].ToString()), Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DRID"].ToString()), GetLocalIPAddress());

                                    if (Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                                    {
                                        string messageFromDatabase = Result.Tables[0].Rows[0][0].ToString();

                                        if (messageFromDatabase == "Record inserted successfully")
                                        {

                                            string script = "Swal.fire('Success!', 'Successfully Upload Cause List', 'success');";
                                            ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                                            fill_Grid();
                                            // Redirect after a short delay (e.g., 3 seconds)
                                            ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'Cause_List_Upload.aspx'; }, 3000);", true);
                                        }
                                        //else if (messageFromDatabase == "similar record found")
                                        //{

                                        //    string script = "Swal.fire('Warning!', 'Template name already exists!', 'warning');";
                                        //    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                                        //}

                                    }
                                   
                                    Reset();
                                    Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DoCUploadMsg();SetIndexVisibalTrue();</script>");
                                    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Your file has been successfully uploaded to the server. Thank you', '', 'success')", true);
                                }

                            }
                            else
                            {
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>DocTypeErrorMsg();</script>");
                                //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please selelct .doc, .docx, .pdf only  ..!', '', 'error')", true);
                            }

                        }
                        else
                        {
                            Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>NoFileMessage();</script>");
                        }


                    }


                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }


            }
        }

        protected void Grd_CauseList_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Open")
            {
                string causelist_id = e.CommandArgument.ToString();
                //string Party_Id = Session["partyID"].ToString();causelist_id
                DataTable dtAppDetails = couseListBAL.GetDoc_Bycauselist_id(Convert.ToInt32(causelist_id));
                if (dtAppDetails.Rows.Count > 0)
                {

                    CauseList_iframe.Src = dtAppDetails.Rows[0]["PDF_Path"].ToString();
                    

                    CauseList_iframe.Visible = true;
                }

            }
        }
    }
}