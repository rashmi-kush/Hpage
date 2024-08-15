using CMS_Sampada_BAL;
using iTextSharp.text.pdf;
using SCMS_BAL;
using SelectPdf;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
using eSigner;
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
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Collections.Specialized;
using System.Xml;
using System.Web.Services;
using HSM_DSC;
using RestSharp;


namespace CMS_Sampada.CoS
{
    public partial class Final_Order_Further : System.Web.UI.Page
    {
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        string All_DocFile_Hearing = "";
        string All_OrderSheetFileNme = "";
        CoSFinalOrder_BAL clsFinalOrderBAL = new CoSFinalOrder_BAL();
        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        protected void Page_Load(object sender, EventArgs e)
        
        {
            if (!Page.IsPostBack)
            {
                int hdnUserID = Convert.ToInt32(Session["DROID"].ToString());

                fill_ddlTemplate(hdnUserID);
                string today = DateTime.Now.ToString();



                ViewState["Case_Number"] = "";
                if (Session["CaseNumber"] != null)
                {
                    ViewState["Case_Number"] = Session["CaseNumber"].ToString();
                    ViewState["HearingDate"] = Session["Hearing"].ToString(); 
                        
                    string HearingDt = Session["Hearing"].ToString();
                    ViewState["Further_ExeDt"] = Session["Further_ExeDt"].ToString();
                    DateTime Further_ExeDt = Convert.ToDateTime(Session["Further_ExeDt"].ToString());
                    if (Session["Hearing_ID"] != null)
                    {
                        
                        ViewState["Hearing_ID"] = Session["Hearing_ID"].ToString();
                    }
                    if (Session["Notice_Id"] != null)
                    {
                        
                        ViewState["Notice_Id"] = Session["Notice_Id"].ToString();
                    }

                    if (Session["AppID"] != null)
                    {
                        ViewState["AppID"] = Session["AppID"];
                    }

                    lblHearingDt.Text = Further_ExeDt.ToString("dd/MM/yyyy");


                }
                else
                {
                    ViewState["Case_Number"] = "";
                }
                if (ViewState["Case_Number"] != null)
                {
                    if (Session["Hearing_ID"] != null)
                    {
                        //Session["Hearing_ID"] = Request.QueryString["Hearing_ID"].ToString();
                        ViewState["Hearing_ID"] = Session["Hearing_ID"].ToString();
                    }
                    if (Session["Notice_Id"] != null)
                    {
                        //Session["Notice_Id"] = Request.QueryString["Notice_Id"].ToString();
                        ViewState["Notice_Id"] = Session["Notice_Id"].ToString();
                    }
                    DataTable dt = clsHearingBAL.GetOrderSheet(Convert.ToInt32(Session["AppID"]));

                    string casenumber = ViewState["Case_Number"].ToString();
                    //ViewState["PartyDetail"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        hdnfCseNunmber.Value = dt.Rows[0]["Case_Number"].ToString();
                        hdnfApp_Number.Value = dt.Rows[0]["APPLICATION_NO"].ToString();



                        lblProposalIdHeading.Text = dt.Rows[0]["APPLICATION_NO"].ToString();
                        lblCase_Number.Text = dt.Rows[0]["CASE_NUMBER"].ToString();
                        //lblRegisteredDate.Text = dt.Rows[0]["inserteddate"].ToString();
                        //lblHearingdateHeading.Text = dt.Rows[0]["hearingdate"].ToString();

                        //lblHearingdateHeading.Text = ((DateTime)dt.Rows[0]["hearingdate"]).ToString("MM/dd/yyyy");

                        string instDate = dt.Rows[0]["inserteddate"].ToString(); // Assuming it's stored as a string
                        string HearingDate = dt.Rows[0]["hearingdate"].ToString(); // Assuming it's stored as a string
                        DateTime parsedinstDate;

                        if (DateTime.TryParse(instDate, out parsedinstDate))
                        {
                            lblRegisteredDate.Text = parsedinstDate.ToString("dd/MM/yyyy");
                        }
                        lblRegisteredDate.Text = instDate;
                        DateTime PaesedHearing_dt;
                        if (DateTime.TryParse(HearingDate, out PaesedHearing_dt))
                        {
                            lblHearingdateHeading.Text = PaesedHearing_dt.ToString("dd/MM/yyyy");
                        }
                        lblHearingdateHeading.Text = HearingDate;
                        string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        lblToday.Text = Convert.ToString(TDate);
                        //string AppNum = hdnfApp_Number.ToString();
                        string AppNum = lblProposalIdHeading.Text;

                        Session["AppID"] = dt.Rows[0]["App_ID"].ToString(); ;
                        Session["Appno"] = dt.Rows[0]["APPLICATION_NO"].ToString();
                      

                        int App_id = Convert.ToInt32(Session["AppID"].ToString());

                        lblCaseNumber.Text = dt.Rows[0]["Case_Number"].ToString();

                        summernote.Value = "उप पंजीयक भोपाल-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक:  " + lblProposalIdHeading.Text + "  दिनांक " + TDate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";


                        lblCaseNumNo.Text = dt.Rows[0]["Case_Number"].ToString();


                    }


                    //GetReportReason();

                    DataSet dsDocDetails = new DataSet();

                    dsDocDetails = clsHearingBAL.GetOrderSheetProceeding(Convert.ToInt32(Session["AppID"]));
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                RepDetails.DataSource = dsDocDetails;
                                RepDetails.DataBind();



                            }
                        }
                    }

                    dsDocDetails = clsHearingBAL.GetNoticeProceeding(Convert.ToInt32(Session["AppID"]));
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                string Notice_Pros = dsDocDetails.Tables[0].Rows[0]["Notice_PROCEEDING"].ToString();

                                if (Notice_Pros != "")
                                {
                                    PnlNotice.Visible = true;
                                    Repeater_Notice.DataSource = dsDocDetails;
                                    Repeater_Notice.DataBind();
                                }
                                else
                                {
                                    PnlNotice.Visible = false;
                                }


                            }

                        }
                    }


                    dsDocDetails = clsHearingBAL.GetNotice_Doc(Convert.ToInt32(Session["AppID"]));
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                string fileName = dsDocDetails.Tables[0].Rows[0]["NOTICE_DOCS"].ToString();
                                Session["RecentSheetPath"] = fileName.ToString();
                                RecentdocPath.Src = fileName;
                                //grdTOCNotice.DataSource = dsDocDetails;
                                //grdTOCNotice.DataBind();



                            }
                        }
                    }


                    //SR Documents
                    dsDocDetails = clsHearingBAL.GetDocDetails_CoS_ToC(Convert.ToInt32(Session["AppID"]), Session["Appno"].ToString());
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                //grdSRDoc.DataSource = dsDocDetails;
                                //grdSRDoc.DataBind();

                            }

                        }
                    }

                    //Proposal Doc
                    dsDocDetails = clsHearingBAL.GetProposal_Doc(ViewState["Case_Number"].ToString(), Session["AppID"].ToString());
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                //grdProposalDoc.DataSource = dsDocDetails;
                                //grdProposalDoc.DataBind();


                            }
                        }
                    }

                    //OrderSheet Doc
                    dsDocDetails = clsHearingBAL.GetOrderSheet_Doc(ViewState["Case_Number"].ToString(), Session["AppID"].ToString());
                    if (dsDocDetails != null)
                    {
                        if (dsDocDetails.Tables.Count > 0)
                        {

                            if (dsDocDetails.Tables[0].Rows.Count > 0)
                            {
                                //grdTOCOrder.DataSource = dsDocDetails;
                                //grdTOCOrder.DataBind();

                            }
                        }
                    }

                    int appid = Convert.ToInt32(Session["AppID"]);

                    string Proposal_ID = Session["Appno"].ToString();




                    string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_All_COSSheet.pdf";

                    Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                    All_OrderSheetFileNme = Session["All_DocSheet"].ToString();


                    Session["All_DocSheet"] = FileNme;
                    //Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                    All_DocFile_Hearing = Session["All_DocSheet"].ToString();
                    CreateEmptyFile(All_DocFile_Hearing);
                    CraetSourceFile(Convert.ToInt32(appid));
                    AllDocList(Convert.ToInt32(appid));
                    hdTocan.Value = Session["Token"].ToString();
                    //ListOfDocPath(Convert.ToInt32(appid));
                    SetDocumentBy_API();

                    

                    

                    
                }




            }
        }


        protected void GridView1_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                // Get the data source row for the current GridView row
                DataRowView rowView = (DataRowView)e.Row.DataItem;

                // Access the second column (index 1) and modify its value
                e.Row.Cells[1].Text = Server.MapPath(rowView["file"].ToString());
            }
        }


       
        public void CreateEmptyFile(string filename)
        {
            string serverpath = Server.MapPath("~/CoS_FinalOrderDoc/" + filename);
            if (!File.Exists(serverpath))
            {
                ConvertHTMToPDF(filename, "~/CoS_FinalOrderDoc/", "<p>Final Order</p>");
            }
            else
            {
                File.Delete(serverpath);
                ConvertHTMToPDF(filename, "~/CoS_FinalOrderDoc/", "<p>FinalOrder</p>");
            }
            ViewState["ALLDocCAddedPDFPath"] = "~/CoS_FinalOrderDoc/" + filename;
            ViewState["CoS_FinalOrderDoc"] = serverpath;
        }
       
        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                DataTable dtDocProDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails.Rows.Count > 0)
                {
                    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        Session["RegPath"] = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }
                DataTable dtDocProDetails_Reg = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails_Reg.Rows.Count > 0)
                {
                    if (dtDocProDetails_Reg.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }

                DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocDetails.Rows.Count > 0)
                {
                    if (dtDocDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        ifReg.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                        //iAllDoc.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                    }


                }


                DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[3];

                    //addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    //addedfilename[0] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    addedfilename[0] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                    addedfilename[1] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                    addedfilename[2] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());

                    string sourceFile = ViewState["CoS_FinalOrderDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                }

            }
            catch (Exception ex)
            {

            }

        }


        protected void grdSRDoc_Sorted(object sender, GridViewSortEventArgs e)
        {
            //DataTable dt = grdSRDoc.DataSource as DataTable;
            DataTable dt = (DataTable)ViewState["SortDirection"];

            if (dt != null)
            {
                // Sort the data based on the selected column and sort direction.
                DataView dv = new DataView(dt);
                dv.Sort = e.SortExpression + " " + GetSortDirection(e.SortDirection);

                // Rebind the GridView with the sorted data.
                grdSRDoc.DataSource = dv;
                grdSRDoc.DataBind();
            }

        }
        private string GetSortDirection(SortDirection sortDirection)
        {
            // Determine the sort direction (ASC or DESC).
            string newSortDirection = "ASC";

            if (ViewState["SortDirection"] != null)
            {
                if (ViewState["SortDirection"].ToString() == "ASC")
                {
                    newSortDirection = "DESC";
                }
            }

            // Store the new sort direction in ViewState.
            ViewState["SortDirection"] = newSortDirection;

            return newSortDirection;
        }
        public void AllDocList(int APP_ID)
        {
            try
            {
                DataSet dsDocList = clsFinalOrderBAL.GetAllDocList_FinalOrder(APP_ID);
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, "");
                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(APP_ID, Session["Appno"].ToString());
                if (dsIndexDetails != null)
                {
                    if (dsIndexDetails.Tables.Count > 0)
                    {

                        if (dsIndexDetails.Tables[0].Rows.Count > 0)
                        {
                            grdSRDoc.DataSource = dsIndexDetails;
                            grdSRDoc.DataBind();

                            ViewState["SortDirection"] = dsIndexDetails;
                            ViewState["sortdr"] = "Asc";

                        }

                    }
                }



            }
            catch (Exception ex)
            {

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

        public void CraetSourceFile_old(int APP_ID)
        {
            try
            {
                DataTable dt = clsHearingBAL.GetHearingAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {
                    string[] insDate = new string[5];

                    string[] addedfilename = new string[5];


                    addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                    byte[] byteData = System.IO.File.ReadAllBytes(addedfilename[0]);
                    PdfReader pdfReader = new PdfReader(byteData);
                    int nofPages_SR = pdfReader.NumberOfPages;
                    string pageRange_SR = $"1-{nofPages_SR}";
                    //PageRangeLabel.Text = $"Page Range: {pageRange}";

                    addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                    byte[] byteData2 = System.IO.File.ReadAllBytes(addedfilename[1]);
                    PdfReader pdfReader2 = new PdfReader(byteData2);
                    int nofPages_Prop1 = pdfReader2.NumberOfPages;
                    string pageRange_Prop1 = $"{nofPages_SR + 1}-{nofPages_SR + 1 + nofPages_Prop1}";
                    string lastNo = $"{nofPages_SR + 1 + nofPages_Prop1 + 1}";
                    int result = int.Parse(lastNo);

                    addedfilename[2] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());
                    byte[] byteData3 = System.IO.File.ReadAllBytes(addedfilename[2]);
                    PdfReader pdfReader3 = new PdfReader(byteData3);
                    int nofPages_Prop2 = pdfReader3.NumberOfPages;
                    string pageRange_Prop2 = $"{result}-{result + nofPages_Prop2}";
                    string lastNo1 = $"{result + nofPages_Prop2 + 1}";
                    int result1 = int.Parse(lastNo1);

                    addedfilename[3] = Server.MapPath(dt.Rows[0]["ordrsheetpath"].ToString());
                    byte[] byteData4 = System.IO.File.ReadAllBytes(addedfilename[3]);
                    PdfReader pdfReader4 = new PdfReader(byteData4);
                    int nofPages_Order = pdfReader4.NumberOfPages;
                    string pageRange_Order = $"{result1}-{result1 + 1 + nofPages_Order}";
                    string lastNo2 = $"{result1 + 1 + nofPages_Order + 1}";
                    int result2 = int.Parse(lastNo2);

                    addedfilename[4] = Server.MapPath(dt.Rows[0]["NOTICE_DOCSPATH"].ToString());
                    byte[] byteData5 = System.IO.File.ReadAllBytes(addedfilename[4]);
                    PdfReader pdfReader5 = new PdfReader(byteData5);
                    int nofPages_Notice = pdfReader5.NumberOfPages;
                    string pageRange_Notice = $"{result2}-{result2 + 1 + nofPages_Notice}";


                    string[] InsertPageNo = { pageRange_SR, pageRange_Prop1, pageRange_Prop2, pageRange_Order, pageRange_Notice };
                    string[] InsertPath = { addedfilename[0], addedfilename[1], addedfilename[2], addedfilename[3], addedfilename[4] };
                    InsertMultipleRows(InsertPath, InsertPageNo);

                    string sourceFile = ViewState["CoSAllHearingSheetDoc"].ToString();

                    MargeMultiplePDF(addedfilename, sourceFile);
                    setAllPdfPath(ViewState["ALLDocAdded_Hearing"].ToString());




                }

            }
            catch (Exception ex)
            {

            }

        }
        protected void InsertMultipleRows(string[] InsertPath, string[] InsertPageNo)
        {

            for (int i = 0; i < InsertPath.Length; i++)
            {
                DataTable dtUp = clsHearingBAL.InsertPathwithPage(Convert.ToInt32(Session["AppID"]), ViewState["Case_Number"].ToString(), InsertPath[i], InsertPageNo[i]);

            }






        }
        public static void MargeMultiplePDF(string[] PDFfileNames, string OutputFile)
        {
            iTextSharp.text.Document PDFdoc = new iTextSharp.text.Document();
            // Create a object of FileStream which will be disposed at the end  
            using (System.IO.FileStream MyFileStream = new System.IO.FileStream(OutputFile, System.IO.FileMode.Create))
            {
                // Create a PDFwriter that is listens to the Pdf document  
                iTextSharp.text.pdf.PdfCopy PDFwriter = new iTextSharp.text.pdf.PdfCopy(PDFdoc, MyFileStream);
                if (PDFwriter == null)
                {
                    return;
                }
                // Open the PDFdocument  
                PDFdoc.Open();
                foreach (string fileName in PDFfileNames)
                {
                    // Create a PDFreader for a certain PDFdocument  
                    iTextSharp.text.pdf.PdfReader PDFreader = new iTextSharp.text.pdf.PdfReader(fileName);
                    PDFreader.ConsolidateNamedDestinations();
                    // Add content  
                    for (int i = 1; i <= PDFreader.NumberOfPages; i++)
                    {
                        iTextSharp.text.pdf.PdfImportedPage page = PDFwriter.GetImportedPage(PDFreader, i);
                        PDFwriter.AddPage(page);
                    }
                    iTextSharp.text.pdf.PRAcroForm form = PDFreader.AcroForm;
                    if (form != null)
                    {
                        PDFwriter.CopyDocumentFields(PDFreader);
                    }
                    // Close PDFreader  
                    PDFreader.Close();
                }
                // Close the PDFdocument and PDFwriter  
                PDFwriter.Close();
                PDFdoc.Close();
            }// Disposes the Object of FileStream  
        }

        private void setAllPdfPath(string vallPdfPath)
        {
            //if (File.Exists(Server.MapPath(vallPdfPath)))
            //{
            //    ifPDFViewerAll.Src = "~/CoS_FinalOrderDoc/" + All_OrderSheetFileNme;
            //}
            if (File.Exists(Server.MapPath(vallPdfPath)))
            {
                ifPDFViewerAll.Src = "~/CoS_OrderSheetAllSheetDoc/" + All_OrderSheetFileNme;

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Session["Appno"].ToString());

                if (dtDocProDetails.Rows.Count > 0)
                {
                    if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                    {
                        ifProposal1.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                        //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                    }


                }
            }
        }




        protected void btnSavePDF_Click(object sender, EventArgs e)
        {
           


        }

        private void SavePDF()
        {
            try
            {

                //string HEARINGDATE = txtHearingDate.Text;

                //string HEARINGDATE = txtHearingDate.Text;
                //DateTime HEARINGDATE = DateTime.Now;
                //HEARINGDATE = Convert.ToDateTime(txtHearingDate.Text);


                //string HEARINGDATE;
                //HEARINGDATE = DateTime.ParseExact(lblHearingDt.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");



                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);

                string divCon = summernote.Value;


                StringBuilder strB = new StringBuilder();

                strB.Append("<div class='main-box htmldoc' style='width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc; padding: 30px 30px 30px 30px;'>");
                strB.Append("<h2 style='font-size: 18px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, भोपाल (म.प्र.)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>प्ररूप-अ</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                strB.Append("<h3 style='margin: 0; margin: 10px; font-size: 16px;'>राजस्व आदेशपत्र</h3>");
                strB.Append("<h2 style='font-size: 16px; margin: 0; margin-bottom: 10px;'>कलेक्टर ऑफ़ स्टाम्प, भोपाल के न्यायालय में मामला क्रमांक- ( " + lblCaseNumber.Text + " )  </ h2>");
                strB.Append("<br>");

                strB.Append("<table style='width: 1000px; border: 1px solid black; border-collapse: collapse;'>");

                //---------------------
                strB.Append("<tr>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>आदेश क्रमांक कार्यवाही <br> की तारीख एवं स्थान");
                strB.Append("</th>");
                strB.Append("<th style='border:1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही <br> मध्यप्रदेश शासन विरूद्ध " + lblPartyName.Text + "");
                strB.Append("</th>");
                strB.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 14px;'>पक्षों/वकीलों <br> आदेश  पालक  लिपिक के हस्ताक्षर");
                strB.Append("</th>");
                strB.Append("</tr>");

                //---------------------------------
                strB.Append("<tr>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "");
                strB.Append("</div>");
                strB.Append("</td>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("<div style='padding: 2px;'>");
                strB.Append("<p style=' text-align: center;'>");
                strB.Append("Case Number : ( " + lblCaseNumNo.Text + " ) ");
                strB.Append("<br/>");
                strB.Append("</p>");

                strB.Append(divCon);

                strB.Append("<br/>");
                strB.Append("<hr style='width:100%; margin-left:0; border: 1px solid black'>");

                strB.Append(lbltext.Text);

                strB.Append("<br/>");
                strB.Append("<b style ='float:left; text-align: center; padding: 2px 0 5px 0;'> आदेश के लिए नियत दिनांक <br/> " + lblHearingDt.Text + "");
                strB.Append("</b>");


                strB.Append("<p></p>");

                strB.Append("<b style='float: right; text-align:center; padding:2px 0 5px 0;'>कलेक्टर ऑफ़ स्टाम्प्स,<br /> भोपाल");
                strB.Append("<br/><br/>");
                strB.Append("</b>");
                strB.Append("</div>");
                strB.Append("</td>");
                strB.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 14px;'>");
                strB.Append("</td>");


                strB.Append("</tr>");





                strB.Append("</table>");
                strB.Append(" <br/>");
                strB.Append("</div>");



                string Proposal_ID = Session["Appno"].ToString();


                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "Hearing.pdf";
                string HearingSheetPath = Server.MapPath("~/Hearing/" + Proposal_ID);
                ViewState["Hearing_Path"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;


                //Session["Hearing"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;

                string Savedpath = ConvertHTMToPDF(FileNme, HearingSheetPath, strB.ToString());
                Session["RecentSheetPath"] = "~/Hearing/" + Proposal_ID + "/" + FileNme;

            }
            catch (Exception ex)
            {

            }
        }


        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                //string FileName = FileNme;
                //string OrderSheetPath = Server.MapPath(path);
                //if (!Directory.Exists(OrderSheetPath))
                //{
                //    Directory.CreateDirectory(OrderSheetPath);
                //}

                string FileName = FileNme;
                string OrderSheetPath = path;
                if (!Directory.Exists(OrderSheetPath))
                {
                    Directory.CreateDirectory(OrderSheetPath);
                }

                string htmlString = strhtml;// + " <br>  <div style='width: 100%;text-align: right;height: 25px;'> इस आदेश को ऑनलाइन देखने के लिये लिंक <u><a href='https://tinyurl.com/y9frzn9j'>https://tinyurl.com/y9frzn9j </a></u>पर जाये । </div>";  //sb.ToString(); // changed on 14-06-2022
                string baseUrl = OrderSheetPath;
                string pdf_page_size = "A4";
                PdfPageSize pageSize = (PdfPageSize)Enum.Parse(typeof(PdfPageSize), pdf_page_size, true);
                string pdf_orientation = "Portrait";
                PdfPageOrientation pdfOrientation =
                    (PdfPageOrientation)Enum.Parse(typeof(PdfPageOrientation),
                    pdf_orientation, true);
                int webPageWidth = 1024;
                int webPageHeight = 0;
                // instantiate a html to pdf converter object
                HtmlToPdf converter = new HtmlToPdf();
                // set converter options
                converter.Options.PdfPageSize = pageSize;
                converter.Options.PdfPageOrientation = pdfOrientation;
                converter.Options.WebPageWidth = webPageWidth;
                converter.Options.WebPageHeight = webPageHeight;
                converter.Options.MarginLeft = 50;
                converter.Options.MarginRight = 50;
                converter.Options.MarginTop = 30;
                converter.Options.MarginBottom = 80;

                /****** Start PAGE FOOTER PAGE NUMBERING ******/
                bool showFooterOnFirstPage = true;
                bool showFooterOnOddPages = true;
                bool showFooterOnEvenPages = true;

                int footerHeight = 50;
                converter.Options.DisplayFooter = showFooterOnFirstPage || showFooterOnOddPages || showFooterOnEvenPages;
                converter.Footer.DisplayOnFirstPage = showFooterOnFirstPage;
                converter.Footer.DisplayOnOddPages = showFooterOnOddPages;
                converter.Footer.DisplayOnEvenPages = showFooterOnEvenPages;
                converter.Footer.Height = footerHeight;


                PdfTextSection text = new PdfTextSection(0, 10,
                            "Page: {page_number} of {total_pages}  ",
                            new System.Drawing.Font("Arial", 8));
                text.HorizontalAlign = PdfTextHorizontalAlign.Right;
                converter.Footer.Add(text);


                /****** End PAGE FOOTER PAGE NUMBERING ******/

                // create a new pdf document converting an url
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(htmlString, baseUrl);

                // create a new pdf font (system font)

                //string filpt = "";
                //string ToSaveFileTo = NoticePath + "/" + FileName;
                //byte[] bytes2 = null;

                byte[] bth = doc.Save();

                using (var stream = File.Create(Path.Combine(OrderSheetPath, FileName)))
                {
                    stream.Write(bth, 0, bth.Length);
                }

                //// close pdf document
                doc.Close();

                return OrderSheetPath + "/" + FileName;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

        }
        protected void ValidateOTP_Click(object sender, EventArgs e)
        {

        }

        public void fill_ddlTemplate(int userid)
        {
            DataTable dt = new DataTable();
            ddlTemplates1.Items.Clear();
            try
            {

                dt = clsHearingBAL.GET_MASTERS_PROCEEDING_TEMPLATES_Final_Order(userid);

                foreach (DataRow row in dt.Rows)
                {
                    if (row.ItemArray.Length >= 2) // Ensure each row has at least two values
                    {
                        string text = row.ItemArray[1].ToString(); // Assuming the text value is in the second column
                        string value = row.ItemArray[0].ToString(); // Assuming the value is in the first column

                        ddlTemplates1.Items.Add(new System.Web.UI.WebControls.ListItem(text, value));
                    }
                }

                ddlTemplates1.Items.Insert(0, new System.Web.UI.WebControls.ListItem("--Select Template--", "0"));
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }



        
        //public static string GetTemplate_Notice(string TemId)
        //{
        //    CoSNotice_BAL clsNoticeBAL = new CoSNotice_BAL();
        //    string Template = "<h1>HEARING1</h1>";
        //    DataTable dt = clsNoticeBAL.GetTemplate();

        //    if (dt.Rows.Count > 0)
        //    {
        //        for (int i = 0; i < dt.Rows.Count; i++)
        //        {
        //            if (dt.Rows[i]["COS_TEMP_ID"].ToString() == TemId)
        //            {
        //                Template = dt.Rows[i]["TEMPLATE_VALUE"].ToString();
        //                break;
        //            }
        //            //Console.WriteLine(dt.Rows[i]["RRC_TEMP_ID"]);
        //        }


        //    }


        //    return Template;



        //}
        [WebMethod]
        public static string GetTemplate_Notice(string TemId)
        {
            CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();
            //string Template = "<h1>HEARING1</h1>";
            string Template = "<h1>FinalOrder</h1>";
            DataTable dt = clsHearingBAL.GetTemplateNotice();

            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["COS_TEMP_ID"].ToString() == TemId)
                    {
                        Template = dt.Rows[i]["TEMPLATE_VALUE"].ToString();
                        break;
                    }
                    //Console.WriteLine(dt.Rows[i]["RRC_TEMP_ID"]);
                }


            }


            return Template;



        }
        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAllDnld.Checked)
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentCoSDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["ALLDocAdded_Hearing"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();

                }
                else if (chkRecentDocDnld.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentCosDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(Session["RecentSheetPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }
                else if (chkAllOrderSheetDnld.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_AllCoSDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["All_RRCOrderSheetFileNmePath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }


        protected void BtnFinalOrder_Click(object sender, EventArgs e)
        {
            string Hearing = (ViewState["HearingDate"].ToString());

            //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing, false);
            int Flag = 1;


            //string url = "Final_Order_Drafting.aspx?Case_Number="+casenum+"&Hearing="+hearingdate+"&Flag="+Flag+"&Response_Status="+resp_status+"&Response_type=Hearing_Ordersheet" + "&hearing_id="+hearing_id + "&Notice_Id=" + Notice_Id + "&Status_Id=" + "44";
            string strHearing = "Hearing_Ordersheet";
            //string url = "Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + strHearing + "";
            Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet", false);


        }

        protected void btnSaveProceeding_Click(object sender, EventArgs e)
        {
           
            {
                
                SavePDF();
                string FilePath = Session["RecentSheetPath"].ToString();
                DataTable dt = new DataTable();

                DataTable dtUp = clsHearingBAL.InsertHearing_Procceding_Today(Convert.ToInt32(Session["AppID"].ToString()), summernote.Value, FilePath, DateTime.Now, Convert.ToInt32(Session["Hearing_ID"].ToString()));
                //dt = clsHearingBAL.InsertFinalOrder_SaveLaterFO(Convert.ToInt32(Session["AppID"].ToString()), ViewState["Case_Number"].ToString(), summernote.Value,DateTime.Now, "", "", FilePath);
                if (dtUp.Rows.Count > 0)
                {

                    ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Record Saved Successfully', '', 'success')", true);
                    string Hearing = (ViewState["HearingDate"].ToString());

                    Session["Case_Number"] =ViewState["Case_Number"].ToString();
                    //Session["Hearing"]= ViewState["HearingDate"].ToString();
                    Session["HearingDate"] = ViewState["Further_ExeDt"].ToString();
                    Session["hearing_id_Final"] = ViewState["Hearing_ID"].ToString();
                    Session["Notice_ID"] = ViewState["Notice_Id"].ToString();
                    Session["AppID"] = ViewState["AppID"].ToString();



                    //int Flag = 1;
                    //string resp_status = "1";
                    //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=Hearing_Ordersheet" + "&hearing_id=" + Session["Hearing_ID"].ToString() + "&Notice_Id=" + Session["Notice_Id"].ToString() + "&Status_Id=" + "49", false);
                    int Flag = 1;
                    

                    //string url = "Final_Order_Drafting.aspx?Case_Number="+casenum+"&Hearing="+hearingdate+"&Flag="+Flag+"&Response_Status="+resp_status+"&Response_type=Hearing_Ordersheet" + "&hearing_id="+hearing_id + "&Notice_Id=" + Notice_Id + "&Status_Id=" + "44";
                    string strHearing = "Hearing_Ordersheet";
                    //string url = "Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + strHearing + "";
                    Response.Redirect("Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + "" + "&Response_type=Hearing_Ordersheet", false);

                    //Response.Redirect("Final_Order_Drafting.aspx?Case_Number=" + ViewState["Case_Number"] + "&Hearing=" + Hearing + "&Flag=" + "2", false);

                }

               

            }

        }

        private void SetDocumentBy_API()
        {
            int appid = 0;
            if (Session["AppID"] != null)
            {
                appid = Convert.ToInt32(Session["AppID"].ToString());
            }
            string Appno = "";

            DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(appid, Appno);


            //string fileName = "estamp_party_Signed_1705996231292.pdf";

            //// RecentdocPath.Src = "Z:/backup/IGRS/E_REGISTRY/01-2024/EREG_COMP_ASP/" + fileName;
            //fileName = "estamp_party_Signed_1705996231292.pdf";
            //string ERegDate = "01-2024";
            //string EREGDocType = "EREG_COMP_ASP";
            if (dtDocDetails.Rows.Count > 0)
            {
                //if (dtDocDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //{
                //    RecentdocPath.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();
                //    //iAllDoc.Src = "../GeteRegDoc_Handler.ashx?pageURL=" + dtDocDetails.Rows[0]["File_Path"].ToString();




                //}


                string Base64 = Comsumedata("RegistryDocument", Convert.ToInt32(dtDocDetails.Rows[0]["ereg_id"]));
                //Response.Write(Base64);
                string encodedPdfData = "";
                if (Base64 != null)
                {
                    hdnbase64.Value = Base64;
                    //encodedPdfData = "data:application/pdf;base64," + Base64 + "";
                    //RecentdocPath.Attributes["src"] = encodedPdfData;
                    ////RecentdocPath.Src = Base64;
                    ifReg.Visible = true;
                }
                else
                {
                    ifReg.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                }


            }

            DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(appid, Appno);

            if (dtDocProDetails.Rows.Count > 0)
            {
                //if (dtDocProDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //{
                //    RecentProposalDoc.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //    //iAllDocReg.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocProDetails.Rows[0]["File_Path"].ToString();
                //}



                string Base64 = Comsumedata("ProposalDocument", Convert.ToInt32(dtDocProDetails.Rows[0]["ereg_id"]));
                string encodedPdfData = "";
                if (Base64 != null)
                {
                    encodedPdfData = "data:application/pdf;base64," + Base64 + "";
                    ifProposal1.Attributes["src"] = encodedPdfData;
                    ifProposal1.Visible = true;
                }
                else
                {
                    ifProposal1.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                }

            }

            DataTable dtDocAttachedDetails = objClsNewApplication.Get_Recent_ATTACHED_DOC_CoS_Hand(appid, Appno);

            if (dtDocAttachedDetails.Rows.Count > 0)
            {
                //if (dtDocAttachedDetails.Rows[0]["File_Path"].ToString().Contains("pdf"))
                //{
                //    RecentAttachedDoc.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocAttachedDetails.Rows[0]["File_Path"].ToString();
                //    //iAllDocPro.Src = "../GetProposalFormDoc_Handler.ashx?pageURL=" + dtDocAttachedDetails.Rows[0]["File_Path"].ToString();
                //}


                //else
                //{

                //    RecentAttachedDoc.Visible = false;
                //    //iAllDocPro.Visible = false;
                //}


                string Docbase64 = Comsumedata("AdditionalDocument", Convert.ToInt32(dtDocAttachedDetails.Rows[0]["ereg_id"]));
                string encodedPdfData = "";
                if (Docbase64 != null)
                {
                    encodedPdfData = "data:application/pdf;base64," + Docbase64 + "";
                    RecentAttachedDoc.Attributes["src"] = encodedPdfData;
                    RecentAttachedDoc.Visible = true;
                }
                else
                {
                    RecentAttachedDoc.Visible = false;
                    ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                }

            }
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

    }

}


//GetOrderSheetProceeding