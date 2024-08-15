using System;
using System.IO;
using System.Web.UI.WebControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Data;
using System.Net;
using System.Net.Sockets;
using SCMS_BAL;
using SelectPdf;
using iTextSharp.text.pdf;

namespace CMS_Sampada.CoS
{
    public partial class CosViewLegacyCase : System.Web.UI.Page
    {

        LegacyCases_BAL objlegacy = new LegacyCases_BAL();

        string All_LegacyDocSheetFileNme = "_All_LegacyDocSheet.pdf";
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                Session["All_LegacyDocSheetName"] = Session["AppId"].ToString() + "_All_LegacyDocSheet.pdf";
                CreateEmptyFile(Session["All_LegacyDocSheetName"].ToString());
                CreateSourceFile(Convert.ToInt32(Session["AppId"].ToString()));
                if (!Page.IsPostBack)
                {
                    fill_HeadSection();
                    if (Session["AppId"] != null)
                    {

                        int App_Id = Convert.ToInt32(Session["AppId"].ToString());
                        fill_RegCaseDetails(App_Id);
                        fill_PartyDetails(App_Id);
                        fill_PropertyDetails(App_Id);
                        fill_ProposalDates(App_Id);
                        LoadProposalDetails(App_Id);
                        fill_SRO_CaseDetails(App_Id);
                        BindComments();




                        //Load Documents.....



                        //Index Documents
                        DataSet dsIndexDetails = objlegacy.GetDocDetails_Legacy_Index(App_Id);
                        if (dsIndexDetails != null)
                        {
                            if (dsIndexDetails.Tables.Count > 0)
                            {

                                if (dsIndexDetails.Tables[0].Rows.Count > 0)
                                {
                                    grdSRDoc.DataSource = dsIndexDetails.Tables[0];
                                    grdSRDoc.DataBind();

                                }

                            }
                        }

                        //Recent Document
                        DataTable dtDocDetails = objlegacy.GetRecent_Doc(App_Id);
                        if (dtDocDetails.Rows.Count > 0)
                        {
                            if (dtDocDetails.Rows[0]["FILE_PATH"].ToString().Contains("pdf"))
                            {
                                string docpath = dtDocDetails.Rows[0]["FILE_PATH"].ToString();
                                if (dtDocDetails.Rows[0]["FILE_PATH"].ToString().Contains("~"))
                                {
                                    docpath = docpath.Replace("~", "..");
                                }
                                // RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["FILE_PATH"].ToString();
                                ifPDFViewer.Src = docpath;
                                ifPDFViewer.Visible = true;
                            }


                        }


                        //All Documents pdf
                        All_LegacyDocSheetFileNme = Session["All_LegacyDocSheetName"].ToString();
                        CreateEmptyFile(All_LegacyDocSheetFileNme);
                        CreateSourceFile(App_Id);

                    }
                }
            }
            catch (Exception ex)
            {

            }
        }


        public void fill_HeadSection()
        {

            ddl_Head.Items.Clear();
            ddl_Section.Items.Clear();
            DataSet ds = new DataSet();
            try
            {

                ds = objlegacy.GetMasterHeadSection();


                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column
                        ddl_Head.Items.Add(new ListItem(text, value));
                    }
                }


                foreach (DataRow row in ds.Tables[1].Rows)
                {
                    if (row.ItemArray.Length >= 2)
                    {
                        string text = row.ItemArray[1].ToString();
                        string value = row.ItemArray[0].ToString();
                        ddl_Section.Items.Add(new ListItem(text, value));
                    }
                }


                ddl_Head.Items.Insert(0, new ListItem("--Select--", "0"));
                ddl_Section.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        public void fill_RegCaseDetails(int App_Id)
        {
            DataTable dt = null;

            try
            {
                dt = objlegacy.GetRegCaseDetails(App_Id);

                if (dt != null)
                {

                    GrdCaseRegDetails.DataSource = dt;
                    GrdCaseRegDetails.DataBind();
                }
                else
                {
                    GrdCaseRegDetails.DataSource = null;
                    GrdCaseRegDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        public void fill_PartyDetails(int App_Id)
        {
            DataTable dt = null;

            try
            {
                dt = objlegacy.Get_Party_Details(App_Id);

                if (dt != null)
                {
                    GrdPartyDetails.DataSource = dt;
                    GrdPartyDetails.DataBind();
                }
                else
                {
                    GrdPartyDetails.DataSource = null;
                    GrdPartyDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }
        public void fill_PropertyDetails(int AppId)
        {
            DataTable dt = null;

            try
            {
                dt = objlegacy.GetPropertyDetails(AppId);

                if (dt != null)
                {
                    GrdPropertyDetails.DataSource = dt;
                    GrdPropertyDetails.DataBind();
                }
                else
                {
                    GrdPropertyDetails.DataSource = null;
                    GrdPropertyDetails.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

        private void LoadProposalDetails(int appId)
        {

            // Fetch data from the database
            DataTable dt = objlegacy.GetProposalDetails(appId);

            if (dt != null && dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];



                txtNatureDocParty.Text = row["NatureOfParty_Docs"].ToString();
                txtNatureDocRegOff.Text = row["NatureOfProposal_DocsRO"].ToString();
                //txtNatureDocDeficit.Text = row["Deficit_GuideLineValue"].ToString();
                txtNatureDocRemark.Text = row["Nature_Of_Documents_Remarks"].ToString();

                txtConsiderationParty.Text = row["ConsiderationValueOfProperty"].ToString();
                txtConsiderationRegOff.Text = row["proposedConsiderationValueOfProperty"].ToString();
                //txtConsiderationDeficit.Text = row["DeficitDuty"].ToString();
                txtConsiderationRemark.Text = row["Property_Consider_Remark"].ToString();

                txtGuidePropertyParty.Text = row["Guideline_PropertyValue"].ToString();
                txtGuidePropertyRegOff.Text = row["Guideline_PropValue_ByRegisOfficer"].ToString();
                txtGuidePropertyDeficit.Text = row["Deficit_GuideLineValue"].ToString();
                txtGuidePropertyRemark.Text = row["Guideline_value_Remark"].ToString();

                txtPrincipalParty.Text = row["Principal_StampDuty"].ToString();
                txtPrincipalRegOff.Text = row["Principal_PropsedStmpDuty"].ToString();
                txtPrincipalDeficit.Text = row["Deficit_Principal"].ToString();
                // txtPrincipalRemark.Text = row["Stamp_Duty_Remark"].ToString();

                txtMuncipalParty.Text = row["Municipal_StampDuty"].ToString();
                txtMuncipalRegOff.Text = row["Muncipal_ProposedStmpDuty"].ToString();
                txtMuncipalDeficit.Text = row["Deficit_Muncipal"].ToString();
                //txtMuncipalRemark.Text = row["Stamp_Duty_Remark"].ToString();

                txtJanpadParty.Text = row["Janpad_SD"].ToString();
                txtJanpadRegOff.Text = row["Janpad_ProposedStmpDuty"].ToString();
                txtJanpadDeficit.Text = row["Deficit_Janpad"].ToString();
                //txtJanpadRemark.Text = row["Stamp_Duty_Remark"].ToString();

                txtUpkarParty.Text = row["Upkar"].ToString();
                txtUpkarRegOff.Text = row["Upkar_ProposedStmpDuty"].ToString();
                txtUpkarDeficit.Text = row["Deficit_Upkar"].ToString();
                //txtUpkarRemark.Text = row["Stamp_Duty_Remark"].ToString();

                lblTLStampDutyParty.Text = row["TOTAL_STAMPDUTY_BYPARTY"].ToString();
                lblTLStampDutyRegOff.Text = row["TOTAL_STAMPDUTY_BY_RO"].ToString();
                //lblTLStampDutyDeficit.Text = row["DeficitDuty"].ToString();
                txtTLStampDutyRemark.Text = row["Stamp_Duty_Remark"].ToString();

                txtPaidStampDutyParty.Text = row["ALREADY_PAID_DUTY_BYPARTY"].ToString();
                txtPaidStampDutyRegOff.Text = row["ALREADY_PAID_DUTY_BYRO"].ToString();
                //txtPaidStampDutyDeficit.Text = row["DeficitDuty"].ToString();
                //txtPaidStampDutyRemark.Text = row["Stamp_Duty_Remark"].ToString();

                txtExemptedAmtParty.Text = row["exempted_amount_byparty"].ToString();
                txtExemptedAmtRegOff.Text = row["exempted_amount_byparty"].ToString();
                txtExemptedAmtDeficit.Text = row["DEFICIT_EXEMPTED_SD"].ToString();
                //txtExemptedAmtRemark.Text = row["Exempted_Amount_Remark"].ToString();

                //lblNetStamDutyParty.Text = row["Net_StampDuty"].ToString();
                //lblNetStamDutyRegOff.Text = row["Net_StampDuty"].ToString();
                lblNetStamDutyDeficit.Text = row["DeficitNetStampDuty"].ToString();
                //txtNetStamDutyRemark.Text = row["Net_StampDuty_Remark"].ToString();

                txtRegFeeParty.Text = row["Total_Reg_Fee_ByParty"].ToString();
                txtRegFeeRegOff.Text = row["Total_Reg_Fee_ByRO"].ToString();
                //txtRegFeeDeficit.Text = row["DeficitRegistrationFees"].ToString();
                txtRegFeeRemark.Text = row["Reg_Fee_Remark"].ToString();

                txtPaidRegFeeParty.Text = row["ALREADY_PAID_REG_FEE_BYPARTY"].ToString();
                txtPaidRegFeeRegOff.Text = row["ALREADY_PAID_REG_FEE_BYRO"].ToString();
                //txtPaidRegFeeDeficit.Text = row["DeficitRegistrationFees"].ToString();
                // txtPaidRegFeeRemark.Text = row["Reg_Fees_Remark"].ToString();

                txtRegFeeExemtAmtParty.Text = row["REG_FEE_EXEMPTED_AMT_BYPARTY"].ToString();
                txtRegFeeExemtAmtRegOff.Text = row["REG_FEE_EXEMPTED_AMT_BYRO"].ToString();
                //txtRegFeeExemtAmtDeficit.Text = row["Exempted_Reg_Fee"].ToString();
                //txtRegFeeExemtAmtRemark.Text = row["Exempted_Reg_Fee_Remark"].ToString();

                lblNetRegFeeParty.Text = row["NET_REG_FEE"].ToString();
                lblNetRegFeeRegOff.Text = row["NET_REG_FEE_BYRO"].ToString();
                lblNetRegFeeDeficit.Text = row["DeficitRegistrationFees"].ToString();
                //txtNetRegFeeRemark.Text = row["Net_Reg_Fee_Remark"].ToString();

                //lblTotalParty.Text = row["Total"].ToString();
                //lblTotalRegOff.Text = row["Total"].ToString();
                //lblTotalDeficit.Text = row["Total_Deficit"].ToString();
                //txtTotalRemark.Text = row["Total_Remark"].ToString();


            }
        }
        public void fill_ProposalDates(int appId)
        {
            DataTable dt = null;

            try
            {
                dt = objlegacy.GetProposalDates(appId);

                if (dt != null)
                {
                    GrdProposalDates.DataSource = dt;
                    GrdProposalDates.DataBind();
                }
                else
                {
                    GrdProposalDates.DataSource = null;
                    GrdProposalDates.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }


        public void fill_SRO_CaseDetails(int AppId)
        {
            DataSet ds = null;

            try
            {
                ds = objlegacy.GetSRO_CaseDetails(AppId);

                if (ds != null)
                {
                    GrdSroDetails.DataSource = ds.Tables[0];
                    GrdSroDetails.DataBind();

                    DataRow row = ds.Tables[1].Rows[0];
                    lblNumNoticeSend.Text = row["NUMBER_OF_NOTICE_SEND"].ToString();
                    lblImpoundDate.Text = row["IMPOUND_DATE"].ToString();
                    lblNextHearingDate.Text = row["NEXT_HEARING_DATE"].ToString();
                    lblRegisteredCaseYear.Text = row["YEAR_CASE_REGISTERED"].ToString();
                    ddl_Head.SelectedItem.Text= row["Heads_type"].ToString();
                    ddl_Section.SelectedItem.Text = row["Sections_Type"].ToString();

                }
                else
                {
                    GrdSroDetails.DataSource = null;
                    GrdSroDetails.DataBind();
                    
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }


        protected void btn_UpdateData(object sender, EventArgs e)
        {

            int App_Id = Convert.ToInt32(Session["AppId"].ToString());
            string status = "";
            DataTable dt = objlegacy.GetRegisteredDocumentName(App_Id);
            if (dt.Rows.Count > 0)
            {

                string messageFromDatabase = dt.Rows[0][1].ToString();
                status = dt.Rows[0][2].ToString();

                if (messageFromDatabase == "Physical Registered")
                {
                    if (status == "1101" || status == "1102")
                    {
                        Session["Status"] = status;
                        Response.Redirect("ManualLegacyDataRegistered.aspx");
                    }
                }
                if (messageFromDatabase == "Electronic Registered (SAMPADA 1.0)")
                {
                    if (status == "1201" || status == "1202")
                    {
                        Session["Status"] = status;
                        Response.Redirect("LegacyElectronicRegistered.aspx");
                    }
                }
            }


        }

        private void BindComments()
        {
            DataTable dt = null;
            int App_Id = Convert.ToInt32(Session["AppId"].ToString());
            try
            {

                dt = objlegacy.GetComments(App_Id);

                if (dt != null && dt.Rows.Count > 0)
                {

                    rptComments.DataSource = dt;
                    rptComments.DataBind();
                }
                else
                {
                    rptComments.DataSource = null;
                    rptComments.DataBind();
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

           
        }
        protected void btn_SaveCosComment(object sender, EventArgs e)
        {

            if (txtCoscomment.Text != "")
            {
                // save Reader Comment and status change on this function 
                btnSendBack.Enabled = false;
                int App_Id = Convert.ToInt32(Session["AppId"].ToString());
                

                DataTable dt = objlegacy.InsertSaveComment(App_Id, Session["DRID"].ToString(), GetLocalIPAddress(),"" , txtCoscomment.Text);
                if (dt.Rows.Count > 0)
                {

                    string messageFromDatabase = dt.Rows[0][0].ToString();

                    if (messageFromDatabase == "Record Inserted successfully")
                    {
                        ShowAlert("Success!", "The case has been successfully sent Back to Reader!", "success", "CollectorStampLegacyCases.aspx");
                    }
                }
                else
                {
                    btnSendBack.Enabled = true;
                    ShowAlert("Warning!", "There was an issue while sending the case!", "Warning");
                }

            }
            else if (txtCoscomment.Text == "")
            {
                btnSendBack.Enabled = true;
                ShowAlert("Warning!", "Please enter the Comment.", "warning");
            }
        }

        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/LegacyAllSheetDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/LegacyAllSheetDoc/", "<p>Legacy Doc Sheet</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/LegacyAllSheetDoc/", "<p>Legacy Doc Sheet</p>");
            }
            ViewState["ALLLegacyAddedPDFPath"] = "~/LegacyAllSheetDoc/" + filename;
            ViewState["LegacyAllSheetDoc"] = serverpath;
        }

        public void CreateSourceFile(int APP_ID)
        {
            try
            {

                DataTable dt = objlegacy.GetAllLegacyDoc(APP_ID);

                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (File.Exists(Server.MapPath(dt.Rows[i]["FILE_PATH"].ToString())))
                        {
                            addedfilename[i] = Server.MapPath(dt.Rows[i]["FILE_PATH"].ToString());
                        }

                    }
                    string sourceFile = ViewState["LegacyAllSheetDoc"].ToString();
                    if (IsValidPdf(sourceFile))
                    {
                        MergeMultiplePDFs(addedfilename, sourceFile);
                        SetAllPdfPath(ViewState["ALLLegacyAddedPDFPath"].ToString());

                    }


                }
            }
            catch (Exception ex)
            {
                // Log the exception (use your logging mechanism)
                // Example: Logger.LogError(ex);
            }
        }


        private bool IsValidPdf(string filepath)
        {
            bool Ret = true;

            PdfReader reader = null;

            try
            {
                if (File.Exists(filepath))
                {
                    reader = new PdfReader(filepath);
                }
                else
                {
                    return true;
                }

            }
            catch
            {
                Ret = false;

            }
            finally
            {
                if (reader != null)
                {
                    reader.Dispose();
                }

            }

            return Ret;
        }
        public static void MergeMultiplePDFs(string[] pdfFileNames, string outputFile)
        {
            using (var pdfDoc = new iTextSharp.text.Document())
            {
                using (var writer = new iTextSharp.text.pdf.PdfCopy(pdfDoc, new FileStream(outputFile, FileMode.Create)))
                {
                    if (writer == null) return;

                    pdfDoc.Open();
                    foreach (var fileName in pdfFileNames)
                    {
                        if (string.IsNullOrEmpty(fileName)) continue;

                        using (var reader = new iTextSharp.text.pdf.PdfReader(fileName))
                        {
                            reader.ConsolidateNamedDestinations();
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                writer.AddPage(writer.GetImportedPage(reader, i));
                            }

                            if (reader.AcroForm != null)
                            {
                                writer.CopyDocumentFields(reader);
                            }
                        }
                    }
                }
            }
        }

        private void SetAllPdfPath(string allPdfPath)
        {
            if (File.Exists(Server.MapPath(allPdfPath)))
            {

                ifPDFViewerAll.Src = allPdfPath;
                Session["LegacyAllSheetDoc"] = allPdfPath;
                ViewState["LegacyAllSheet"] = allPdfPath;
            }

        }

        public string ConvertHTMToPDF(string fileName, string path, string htmlContent)
        {
            try
            {
                string legacyDocumentPath = Server.MapPath(path);
                if (!Directory.Exists(legacyDocumentPath))
                {
                    Directory.CreateDirectory(legacyDocumentPath);
                }

                var converter = new HtmlToPdf
                {
                    Options =
            {
                PdfPageSize = PdfPageSize.A4,
                PdfPageOrientation = PdfPageOrientation.Portrait,
                WebPageWidth = 1024,
                MarginLeft = 30,
                MarginRight = 30,
                MarginTop = 20,
                MarginBottom = 30,
                DisplayFooter = true
            }
                };

                converter.Footer.Height = 50;
                converter.Footer.DisplayOnFirstPage = true;
                converter.Footer.DisplayOnOddPages = true;
                converter.Footer.DisplayOnEvenPages = true;

                var text = new PdfTextSection(0, 10, "Page: {page_number} of {total_pages}", new System.Drawing.Font("Arial", 8))
                {
                    HorizontalAlign = PdfTextHorizontalAlign.Right
                };
                converter.Footer.Add(text);

                var pdfDocument = converter.ConvertHtmlString(htmlContent, legacyDocumentPath);

                string filePath = Path.Combine(legacyDocumentPath, fileName);
                pdfDocument.Save(filePath);
                pdfDocument.Close();

                return filePath;
            }
            catch (Exception ex)
            {
                // Log the exception (use your logging mechanism)
                return string.Empty;
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

        protected void btnRegisterCase_Click(object sender, EventArgs e)
        {
            //  status change and it will go to Hearing page 
            try
            {

                btnRegister.Enabled = false;
                int Heads_ID = Convert.ToInt32(ddl_Head.SelectedValue);
                int Sections_ID = Convert.ToInt32(ddl_Head.SelectedValue);
                string Heads_Type = ddl_Head.SelectedItem.Text;
                string Sections_Type = ddl_Section.SelectedItem.Text;
                int App_Id = Convert.ToInt32(Session["AppId"].ToString());
                DataTable Result = new DataTable();
                Result = objlegacy.LegacyCaseRegister(App_Id, Convert.ToInt32(Session["DistrictID"]), Session["District_NameEN"].ToString(), Session["DRID"].ToString(), GetLocalIPAddress(), Heads_Type, Sections_Type, Heads_ID, Sections_ID);
                if (Result.Rows.Count > 0)
                {
                    string messageFromDatabase = Result.Rows[0][0].ToString();

                    if (messageFromDatabase != "")
                    {
                        ShowAlert("Success!", $"The case is registered successfully and the case number is {messageFromDatabase}.", "success", "CollectorStampLegacyCases.aspx");
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
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