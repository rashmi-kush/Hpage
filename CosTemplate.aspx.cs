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
    public partial class CosTemplate : System.Web.UI.Page
    {
        CosTemplate_BAL couseListBAL = new CosTemplate_BAL();
       
        int selectedCount = 0;
        int EditTemplateId;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                fill_ddlType();
                fill_ddlCategory();
                fill_Grid();
            }

        }


        public void fill_Grid()
        {
            DataTable dt = null;

            try
            {
                dt = couseListBAL.GetDetailsALLTemplate(Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DistrictID"].ToString()));

                if (dt != null)
                {
                    Grd_Template.DataSource = dt;
                    Grd_Template.DataBind();
                }
                else
                {
                    Grd_Template.DataSource = null;
                    Grd_Template.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        public void fill_ddlType()
        {
            DataTable dt = new DataTable();
            ddl_hearing.Items.Clear();
            try
            {

                dt = couseListBAL.GET_MASTERS_PROCEEDING_TEMPLATE_TYPE();

                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddl_hearing.Items.Add(new ListItem(text, value));
                    }
                }

                ddl_hearing.Items.Insert(0, new ListItem("--Select Type of Template--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        public void fill_ddlCategory()
        {
            DataTable dt = new DataTable();
            ddl_Template.Items.Clear();
            try
            {

                dt = couseListBAL.GET_MASTERS_PROCEEDING_TEMPLATE_CATEGORY();
                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddl_Template.Items.Add(new ListItem(text, value));
                    }
                }

                ddl_Template.Items.Insert(0, new ListItem("--Select Category of Template--", "0"));

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        protected void btnsearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtsearch.Text))
            {
                string script = "Swal.fire('Warning!', 'Please provide the template name!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }
            DataTable dsList = new DataTable();
            dsList = couseListBAL.SearchTemplate(txtsearch.Text, Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DistrictID"].ToString()));
            if (dsList != null)
            {
                if (dsList.Rows.Count > 0)
                {

                    Grd_Template.DataSource = dsList;
                    Grd_Template.DataBind();
                    txtsearch.Text = "";
                }
                else
                {

                    Grd_Template.DataSource = null;
                    Grd_Template.DataBind();
                    string script = "Swal.fire('Warning!', 'No results found!', 'warning');";
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                    //btnsrchlbl.Text = "No results found.";
                    txtsearch.Text = "";
                    btndelete.Visible = false;
                    btnClose.Visible = false;
                }
                
            }
            else
            {
                // Handle the case where dataset is null (optional)
                // You can put some code here if required
            }
        }

        protected void btnTemplateSave_Click(object sender, EventArgs e)
        {

            if (ddl_hearing.SelectedValue == "0")
            {
                string script = "Swal.fire('Warning!', 'Please select the type of template!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }


            if (ddl_Template.SelectedValue == "0")
            {

                string script = "Swal.fire('Warning!', 'Please select the template category!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;

            }
            if (string.IsNullOrWhiteSpace(txtTemplateName.Text))
            {
                string script = "Swal.fire('Warning!', 'Please provide the template name!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }
            if (string.IsNullOrWhiteSpace(txtTemplateDescription.Value))
            {

                string script = "Swal.fire('Warning!', 'Please provide the template Description!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;
            }

            else
            {
                try
                {
                    DataSet Result = new DataSet();
                    
                    if (btnAdd.Text == "Add")
                    {
                        

                        Result = couseListBAL.Insert_Template(Convert.ToInt32(Session["DistrictID"].ToString()), Convert.ToInt32(Session["DROID"].ToString()), txtTemplateName.Text, txtTemplateDescription.Value, Convert.ToInt32(ddl_Template.SelectedValue), Convert.ToInt32(ddl_hearing.SelectedValue), "");

                        if (Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                        {
                            string messageFromDatabase = Result.Tables[0].Rows[0][0].ToString();

                            if (messageFromDatabase == "Record inserted successfully")
                            {

                                string script = "Swal.fire('Success!', 'Successfully Template inserted!', 'success');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                                // Redirect after a short delay (e.g., 3 seconds)
                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'CosTemplate.aspx'; }, 3000);", true);
                            }
                            else if (messageFromDatabase == "similar record found")
                            {

                                string script = "Swal.fire('Warning!', 'Template name already exists!', 'warning');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);       
                               
                            }

                        }
                    }

                    else if (btnAdd.Text == "Update")
                    {
                        

                        Result = couseListBAL.Update_Template( Convert.ToInt32(Session["DistrictID"].ToString()), Convert.ToInt32(Session["DROID"].ToString()), txtTemplateName.Text, txtTemplateDescription.Value, Convert.ToInt32(ddl_Template.SelectedValue), Convert.ToInt32(ddl_hearing.SelectedValue), "", Convert.ToInt32(Session["EditTemplateId"].ToString()), Convert.ToInt32(Session["DRID"].ToString()));

                        if (Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                        {
                            string messageFromDatabase = Result.Tables[0].Rows[0][0].ToString();

                            if (messageFromDatabase == "Record Updated")
                            {
                                string script = "Swal.fire('Success!', 'Successfully Template Updated!', 'success');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                                // Redirect after a short delay (e.g., 3 seconds)
                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'CosTemplate.aspx'; }, 3000);", true);
                            }
                            else if (messageFromDatabase == "Template name already exists for another ID. Please choose a different name.")
                            {

                                string script = "Swal.fire('Warning!', 'Template name already exists!', 'warning');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                            }
                        }
                    }  
                }
                catch (Exception ex)
                {
                    Response.Write("Error: " + ex.Message);
                }


            }


        }

        protected void btndelete_Click(object sender, EventArgs e)
        {
            foreach (GridViewRow row in Grd_Template.Rows)
            {
                try
                {
                    System.Web.UI.WebControls.CheckBox chkbx = (System.Web.UI.WebControls.CheckBox)row.FindControl("CheckBoxTemplate");
                    HiddenField hdnTemplateID = (HiddenField)row.FindControl("hdnTemplateID");

                    if (chkbx.Checked)
                    {
                        selectedCount++;
                        int selectedTemplateID = Convert.ToInt32(hdnTemplateID.Value);
                        DataSet Result = new DataSet();
                        Result = couseListBAL.TemplateDelete(selectedTemplateID, Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DistrictID"].ToString()));

                        if (Result.Tables.Count > 0 && Result.Tables[0].Rows.Count > 0)
                        {
                            string messageFromDatabase = Result.Tables[0].Rows[0][0].ToString();

                            if (messageFromDatabase == "Record has been deleted!")
                            {
                              
                                string script = "Swal.fire('Success!', 'Successfully Template Deleted!', 'success');";
                                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                                ClientScript.RegisterStartupScript(this.GetType(), "Redirect", "setTimeout(function(){ window.location.href = 'CosTemplate.aspx'; }, 3000);", true);
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
                string script = "Swal.fire('Warning!', 'Select the template You want to delete!', 'warning');";
                ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);

                return;

            }

        }
        protected void Grd_Template_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DataTable templateData = new DataTable();
            if (e.CommandName == "Edit")
            {
                Session["EditTemplateId"] = Convert.ToInt32(e.CommandArgument);

                templateData = couseListBAL.GetTemplateData(Convert.ToInt32(Session["EditTemplateId"].ToString()), Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DistrictID"].ToString()));

                if (templateData.Rows.Count > 0)
                {

                    ddl_Template.SelectedValue = templateData.Rows[0][1].ToString();
                    ddl_hearing.SelectedValue = templateData.Rows[0][2].ToString();
                    txtTemplateName.Text = templateData.Rows[0][3].ToString();
                    txtTemplateDescription.Value = templateData.Rows[0][4].ToString();



                    btnAdd.Text = "Update";
                }
            }
            else if (e.CommandName == "View")
            {
                int templateId = Convert.ToInt32(e.CommandArgument);
                templateData = couseListBAL.GetTemplateData(templateId, Convert.ToInt32(Session["DROID"].ToString()), Convert.ToInt32(Session["DistrictID"].ToString()));

                if (templateData.Rows.Count > 0)
                {
                    string TemplateDescription = templateData.Rows[0][4].ToString();
                    string script = "Swal.fire('Template Description', '" + TemplateDescription + "');"; 
                    ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", script, true);
                }
            }


        }

        protected void Grd_Template_RowEditing(object sender, GridViewEditEventArgs e)
        {
            
            Grd_Template.EditIndex = e.NewEditIndex;          
            //fill_Grid(); 
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


    }



}