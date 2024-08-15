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
using iTextSharp.text.pdf;

namespace CMS_Sampada.CoS
{
    public partial class Ordersheet : System.Web.UI.Page


    {
        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        string Application_Id = ConfigurationManager.AppSettings["ApplicationId"];
        string Department_Id = ConfigurationManager.AppSettings["DepartmentId"];
        string Secretkey = ConfigurationManager.AppSettings["Secretkey"];
        string eSignURL = ConfigurationManager.AppSettings["eSignURL"];

        string Application_Id_eMudra = ConfigurationManager.AppSettings["ApplicationId_eMudra"];
        string Department_Id_eMudra = ConfigurationManager.AppSettings["DepartmentId_eMudra"];
        string Secretkey_eMudra = ConfigurationManager.AppSettings["Secretkey_eMudra"];
        string eSignURL_eMudra = ConfigurationManager.AppSettings["eSignURL_eMudra"];

        string Partition_Name = ConfigurationManager.AppSettings["Partition_Name"];
        string Partition_Password = ConfigurationManager.AppSettings["Partition_Password"];
        string HSM_Slot_No = ConfigurationManager.AppSettings["HSMSlotNo"];
        string Java_Path= ConfigurationManager.AppSettings["JavaPath"];

        private static string RegProposalAttDocument_url = ConfigurationManager.AppSettings["RegProposalAttDoc"];
        string FileName = string.Empty;
        public byte[] pdfBytes;

        string appid;
        string Appno;

        //ClsNewApplication objClsNewApplication = new ClsNewApplication();
        //clsOrderSheet_BAL sheet_BAL = new clsOrderSheet_BAL();

        Encrypt Encrypt = new Encrypt();
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        CoSHearing_BAL clsHearingBAL = new CoSHearing_BAL();

        string All_OrderSheetFileNme = "";

        public string getTransactionID()
        {
            return Guid.NewGuid().ToString();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["AppID"] != null)
            {
            }
            else
            {
                Response.Redirect("https://sampada.mpigr.gov.in"); //PROD
            }


            if (!Page.IsPostBack)
            {
                try
                {
                    hdnSRODistNameHi.Value = Session["District_NameHI"].ToString();
                    hdnCOSDistNameHi.Value = Session["District_NameHI"].ToString();
                    hdTocan.Value = Session["Token"].ToString();
                    hdnSRONameHi.Value = Session["District_NameHI"].ToString();
                    hdnCOSNameHi.Value = Session["officeAddress"].ToString();

                    lblHeadingDist.Text = Session["District_NameHI"].ToString();
                    lblHeadingDist2.Text = Session["District_NameHI"].ToString();
                    lblCOSOfficeNameHi.Text = Session["District_NameHI"].ToString();
                    //lblSRONameHi.Text = Session["officeNameHi"].ToString();
                    lblCOSOfficeNameHi1.Text = Session["District_NameHI"].ToString();
                    lblCOSOfficeNameHi2.Text = Session["District_NameHI"].ToString();
                }
                catch (Exception)
                {

                }

                //Session["Case_Status"] = "";
                try
                {
                    if (Request.QueryString["Flag"] != null)
                    {
                        if (Request.QueryString["Flag"].ToString() == "0")
                        {
                            Session["Case_Status"] = 3;
                        }


                    }


                    if (Session["AppID"] != null)
                    {
                        if (Session["App_Id"] == null)
                        {
                            Session["App_Id"] = Session["AppID"];
                        }
                        string ProImpounddate;
                        if (Session["IMPOUND_DATE"] != null)
                        {
                            Session["ProImpoundDate"] = (Session["IMPOUND_DATE"].ToString());
                            ProImpounddate = Session["ProImpoundDate"].ToString();
                        }
                        else
                        {
                            ProImpounddate = Session["ProImpoundDate"].ToString();
                        }
                        int AppID = Convert.ToInt32(Session["App_Id"].ToString());
                        Session["AppID"] = Session["App_Id"].ToString();
                        DataTable dt = OrderSheet_BAL.Get_OrderSheetID_COSReader(AppID);
                        if (dt.Rows.Count > 0)
                        {
                            //ViewState["Ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString(); 
                            string Ordersheet_id = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["ordersheetReader_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["ordersheet_id_Status"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["OrdersheetContent_Reader"] = dt.Rows[0]["proceeding"].ToString();
                            Session["OrdersheetContent"] = dt.Rows[0]["proceeding"].ToString();
                            //summernote.Value = dt.Rows[0]["proceeding"].ToString();


                        }

                        else
                        {

                            summernote.InnerHtml = hdnSRONameHi.Value + " द्वारा एक पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक: " + Session["Appno"].ToString() + " दिनांक " + Session["ProImpoundDate"].ToString() + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";
                            Session["OrdersheetContent"] = summernote.InnerHtml;
                        }

                    }

                    else
                    {

                        summernote.InnerHtml = hdnSRONameHi.Value + " द्वारा एक पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक: " + Session["Appno"].ToString() + " दिनांक " + Session["ProImpoundDate"].ToString() + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";
                        Session["OrdersheetContent"] = summernote.InnerHtml;
                    }


                    if (Session["ordersheet_id"] != null)
                    {

                    }
                    if (Session["Case_Number"] != null)
                    {
                        if (Session["Case_Status"] != null)
                        {
                            if (Session["ORDRSHEETPATH"] != null)
                            {
                                ViewState["FileNameUnSignedPDF"] = Session["ORDRSHEETPATH"];
                            }
                            if (Session["Case_Status"].ToString() == "3")
                            {

                                DataTable dt = OrderSheet_BAL.Get_Status_OrdersheetId(Convert.ToInt32(Session["AppID"].ToString()));
                                if (dt.Rows[0]["STATUS_ID"].ToString() == "3")
                                {
                                    pnlSeekReport.Visible = false;
                                    pnlHearingDate.Visible = false;
                                    custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
                                    pnlBtnSave.Visible = false;
                                    pnlEsignDSC.Visible = true;


                                }
                                else
                                {
                                    pnlSeekReport.Visible = true;
                                    pnlHearingDate.Visible = true;
                                    //custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
                                    pnlBtnSave.Visible = true;
                                    pnlEsignDSC.Visible = false;
                                }




                            }
                        }
                        //string appid = "3";
                        Session["CaseNum"] = (Session["Case_Number"].ToString());

                        if (Session["Proposalno"] != null && Session["AppID"] != null)
                        {
                            Session["ProposalID"] = (Session["Proposalno"].ToString());
                            Session["AppID"] = (Session["AppID"].ToString());
                            Session["Appno"] = (Session["Proposalno"].ToString());

                            lblProposalIdHeading.Text = Session["ProposalID"].ToString();
                            lblProposal.Text = lblProposalIdHeading.Text;
                            appid = Session["AppID"].ToString();
                            string orderseet = Session["OrdersheetContent"].ToString();
                            summernote.InnerHtml = orderseet;
                            //ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;
                            //if (ViewState["Ordersheet_SeekReportContent"] != null)
                            //{
                            //    pContent.InnerHtml = ViewState["Ordersheet_SeekReportContent"].ToString();
                            //}
                            Appno = Session["Appno"].ToString();
                        }
                        else
                        {
                            lblProposalIdHeading.Text = Session["ProposalID"].ToString();
                            appid = Session["AppID"].ToString();
                            Appno = Session["Appno"].ToString();
                            lblProposal.Text = Appno;
                        }


                        //Session["CaseNum"]= Session["Case_Number"].ToString();


                        string CaseNumber = Session["Case_Number"].ToString();
                        ViewState["Case_Number"] = CaseNumber;

                        lblCase_Number.Text = CaseNumber;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");

                        DataSet dsDocDetails = new DataSet();
                        dsDocDetails = OrderSheet_BAL.GetProposal_Doc(CaseNumber, appid);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    string fileName = dsDocDetails.Tables[0].Rows[0]["PROPOSALPATH_SECONDFORMATE"].ToString();
                                    Session["RecentSheetPath"] = fileName.ToString();
                                    docPath.Src = fileName;
                                }
                            }
                        }
                        dsDocDetails = OrderSheet_BAL.GetProposal_Doc(CaseNumber, appid);
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

                        dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS_ToC(Convert.ToInt32(appid), Appno);
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

                        dsDocDetails = OrderSheet_BAL.GetDocDetails_CoS(Convert.ToInt32(appid), Appno);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {

                                    PreviousDoc.DataSource = dsDocDetails.Tables[0];
                                    PreviousDoc.DataBind();


                                }
                            }
                        }

                        dsDocDetails = OrderSheet_BAL.GetRegisteredDate(CaseNumber);
                        if (dsDocDetails != null)
                        {
                            if (dsDocDetails.Tables.Count > 0)
                            {

                                if (dsDocDetails.Tables[0].Rows.Count > 0)
                                {
                                    string RegisteredDate = dsDocDetails.Tables[0].Rows[0]["case_actiondate"].ToString();
                                    lblRegisteredDate.Text = RegisteredDate;
                                }
                            }
                        }



                        GetPartyDetail();
                        GetPartyDetailTable();

                        Session["All_DocSheet"] = appid + "_All_COSSheet.pdf";
                        All_OrderSheetFileNme = Session["All_DocSheet"].ToString();

                        CreateEmptyFile(All_OrderSheetFileNme);
                        CraetSourceFile(Convert.ToInt32(appid));
                        AllDocList(Convert.ToInt32(appid));
                    }


                    DataTable dt1 = OrderSheet_BAL.Get_OrderSheetID_COSReader(Convert.ToInt32(appid));
                    if (dt1.Rows.Count > 0)
                    {
                        //ViewState["Ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString(); 
                        string Ordersheet_id = dt1.Rows[0]["Ordersheet_id"].ToString();
                        Session["ordersheetReader_id"] = dt1.Rows[0]["Ordersheet_id"].ToString();
                        Session["ordersheet_id_Status"] = dt1.Rows[0]["Ordersheet_id"].ToString();
                        Session["OrdersheetContent_Reader"] = dt1.Rows[0]["proceeding"].ToString();
                        Session["OrdersheetContent"] = dt1.Rows[0]["proceeding"].ToString();
                        ViewState["OrdersheetContent"] = dt1.Rows[0]["proceeding"].ToString();
                        //summernote.Value = dt.Rows[0]["proceeding"].ToString();


                    }
                    DataSet dtSR_Sub = OrderSheet_BAL.Show_AllSR_Subject(Convert.ToInt32(appid));
                    if (dtSR_Sub != null && dtSR_Sub.Tables.Count > 0)
                    {
                        string SR_OriginalSub = null;
                        string SR_CurrentSub = null;
                        string SR_OtherSub = null;

                        foreach (DataRow row in dtSR_Sub.Tables[0].Rows)
                        {
                            string SR_Status = row["REPORT_STATUS"].ToString();

                            if (SR_Status == "1")
                            {
                                SR_OriginalSub = row["SEEK_REPORT_SUBJECT"].ToString();
                                Session["SeekReportSubject"] = SR_OriginalSub.ToString();
                            }
                            else if (SR_Status == "2")
                            {
                                SR_CurrentSub = row["SEEK_REPORT_SUBJECT"].ToString();
                                Session["SeekReportSubject_Current"] = SR_CurrentSub.ToString();
                            }
                            else if (SR_Status == "3")
                            {
                                SR_OtherSub = row["SEEK_REPORT_SUBJECT"].ToString();
                                Session["SeekReportSubject_other"] = SR_OtherSub.ToString();
                            }
                        }

                        DataTable dsCurSRDetail = new DataTable();
                        dsCurSRDetail = OrderSheet_BAL.Get_SRDetail(Convert.ToInt32(appid));
                        if (dsCurSRDetail != null)
                        {
                            if (dsCurSRDetail.Rows.Count > 0)
                            {
                                string RegistrarOffice = dsCurSRDetail.Rows[0]["office_name_hi"].ToString();

                                Session["SubRegistrarOffice"] = RegistrarOffice;


                            }
                        }


                    }




                    //1 only Original SR
                    //if (Session["OrdersheetContent_Reader"] != null && Session["SeekReportSubject"] != null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] == null && Session["SeekReportSubject_other"] == null)
                    if (Session["OrdersheetContent_Reader"] != null && Session["SeekReportSubject"] != null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] == null && Session["SeekReportSubject_other"] == null)
                    {

                        string OrdersheetContent_Reader = Session["OrdersheetContent_Reader"].ToString();
                        //string seekreportcontent;
                        summernote.InnerHtml = Session["OrdersheetContent_Reader"].ToString() + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = true;
                        IfProceedingCrnt.Visible = false;
                        IfProceedingOther.Visible = false;

                        //ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "DisableSeekReport_Rbtn();", true);
                        DataSet dtPro = objClsNewApplication.Show_OriginalSR_SignedPath(Convert.ToInt32(appid), 1);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceeding.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }


                    }

                    //2 only Current SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] == null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] != null && Session["SeekReportSubject_other"] == null)
                    {

                        //string seekreportcontent;
                        summernote.InnerHtml = Session["OrdersheetContent"].ToString() + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject_Current"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = true;
                        IfProceedingOther.Visible = false;

                        DataSet dtPro = objClsNewApplication.Show_OriginalSR_SignedPath(Convert.ToInt32(appid), 2);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingCrnt.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }

                    }

                    //3 only Other SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] == null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] == null && Session["SeekReportSubject_other"] != null)
                    {
                        //string seekreportcontent;
                        summernote.InnerHtml = ViewState["OrdersheetContent"] + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject_other"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = false;
                        IfProceedingOther.Visible = true;

                        AllDocList(Convert.ToInt32(appid));
                        DataSet dtPro = objClsNewApplication.Show_OtherSR_SignedPath(Convert.ToInt32(appid), 3);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingOther.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }
                    }

                    //4 Original and Current SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] != null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] != null && Session["SeekReportSubject_other"] == null)
                    {
                        //string seekreportcontent;
                        summernote.InnerHtml = Session["OrdersheetContent"].ToString() + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject"] + ") </br> (" + Session["SeekReportSubject_Current"] + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = true;
                        IfProceedingOther.Visible = false;

                        DataSet dtPro = objClsNewApplication.Show_OriginalSR_SignedPath(Convert.ToInt32(appid), 2);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingCrnt.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }


                    }

                    //5 Original and Current and other SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] != null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] != null && Session["SeekReportSubject_other"] != null)
                    {
                        //string seekreportcontent;
                        summernote.InnerHtml = ViewState["OrdersheetContent"] + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject"].ToString() + ") </br> (" + Session["SeekReportSubject_Current"].ToString() + ") </br> (" + Session["SeekReportSubject_other"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = false;
                        IfProceedingOther.Visible = true;

                        AllDocList(Convert.ToInt32(appid));
                        DataSet dtPro = objClsNewApplication.Show_OtherSR_SignedPath(Convert.ToInt32(appid), 3);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingOther.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }

                    }

                    //6 Current and Other SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] == null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] != null && Session["SeekReportSubject_other"] != null)
                    {
                        summernote.InnerHtml = ViewState["OrdersheetContent"] + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject_Current"].ToString() + ") </br> (" + Session["SeekReportSubject_other"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = false;
                        IfProceedingOther.Visible = true;

                        AllDocList(Convert.ToInt32(appid));
                        DataSet dtPro = objClsNewApplication.Show_OtherSR_SignedPath(Convert.ToInt32(appid), 3);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingOther.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }
                    }

                    //7 Original and Other SR
                    if (Session["OrdersheetContent"] != null && Session["SeekReportSubject"] != null && Session["SubRegistrarOffice"] != null && Session["SeekReportSubject_Current"] == null && Session["SeekReportSubject_other"] != null)
                    {
                        summernote.InnerHtml = ViewState["OrdersheetContent"] + "<br/><br/>" + "<p> " + Session["SubRegistrarOffice"] + " से विषयान्तर्गत </p>(" + Session["SeekReportSubject"].ToString() + ") </br> (" + Session["SeekReportSubject_other"].ToString() + ") प्राप्त किया जाये।";

                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;

                        pnlHearingDate.Visible = true;
                        pnlSeekReport.Visible = true;
                        pnlBtnSave.Visible = true;
                        lblHearingDt.Text = Session["HearingDate"].ToString();
                        lblHearingDt1.Text = Session["HearingDate"].ToString();
                        txtHearingDate.Text = Session["HearingDate"].ToString();
                        rdReportYes.Checked = true;
                        //Session["OrdersheetContent"] = null;
                        //Session["SeekReportSubject"] = null;
                        //Session["HearingDate"] = null;
                        //Session["SubRegistrarOffice"] = null;
                        docPath.Visible = false;
                        IfProceeding.Visible = false;
                        IfProceedingCrnt.Visible = false;
                        IfProceedingOther.Visible = true;

                        AllDocList(Convert.ToInt32(appid));
                        DataSet dtPro = objClsNewApplication.Show_OtherSR_SignedPath(Convert.ToInt32(appid), 3);
                        if (dtPro != null)
                        {
                            if (dtPro.Tables.Count > 0)
                            {

                                IfProceedingOther.Src = dtPro.Tables[0].Rows[0]["signed_pdf_path"].ToString();

                            }
                        }
                    }


                }
                catch (Exception ex)
                {


                }
                SetDocumentBy_API();

            }
        }
        public void AllDocList(int APP_ID)
        {
            try
            {
                //DataSet dsDocList = clsHearingBAL.GetAllDocList(APP_ID);
                DataSet dsDocList = clsHearingBAL.GetAllDocList_Ordersheet(APP_ID);
                //DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(APP_ID, Appno);
                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index_API(APP_ID, Appno);

                if (dsIndexDetails != null)
                {
                    if (dsIndexDetails.Tables.Count > 0)
                    {

                        if (dsIndexDetails.Tables[0].Rows.Count > 0)
                        {
                            grdSRDoc.DataSource = dsIndexDetails.Tables[0];
                            grdSRDoc.DataBind();

                            ViewState["SortDirection"] = dsDocList;
                            ViewState["sortdr"] = "Asc";

                        }

                    }
                }



            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        private void SetDocumentBy_API()
        {
            try
            {
                int appid = 0;
                if (Session["AppID"] != null)
                {
                    appid = Convert.ToInt32(Session["AppID"].ToString());
                }
                string Appno = "";

                DataTable dtDocDetails = objClsNewApplication.GetRecent_EREG_Doc_CoS_Hand_CoS(appid, Appno);


                if (dtDocDetails.Rows.Count > 0)
                {


                    string Base64 = Comsumedata("RegistryDocument", Convert.ToInt32(dtDocDetails.Rows[0]["ereg_id"]));

                    string encodedPdfData = "";
                    if (Base64 != null)
                    {
                        hdnbase64.Value = Base64;

                        RecentdocPath.Visible = true;
                    }
                    else
                    {
                        RecentdocPath.Visible = false;
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Document not found.','error');", true);
                    }


                }

                DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(appid, Appno);

                if (dtDocProDetails.Rows.Count > 0)
                {




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
            catch (Exception)
            {


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
        private void setAllPdfPath(string vallPdfPath)
        {
            try
            {
                if (File.Exists(Server.MapPath(vallPdfPath)))
                {
                    ifPDFViewerAll.Src = "~/CoS_OrderSheetAllSheetDoc/" + All_OrderSheetFileNme;

                    DataTable dtDocProDetails = objClsNewApplication.Get_Recent_PROPOSAL_DOC_CoS_Hand(Convert.ToInt32(Session["AppID"].ToString()), Appno);

                }
                DataSet dsIndexDetails = objClsNewApplication.GetDocDetails_CoS_Index(Convert.ToInt32(Session["AppID"].ToString()), Appno);
                string DocType = "";
                string PDFPath = "";
                RecentAttachedDoc.Visible = false;
                RecentdocPath.Visible = false;
                ifProposal1.Visible = false;
                if (dsIndexDetails.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < dsIndexDetails.Tables[0].Rows.Count; i++)
                    {
                        PDFPath = dsIndexDetails.Tables[0].Rows[i]["FILE_PATH"].ToString();
                        DocType = dsIndexDetails.Tables[0].Rows[i]["docType"].ToString();

                    }

                }
            }
            catch (Exception)
            {


            }



        }
        public void CreateEmptyFile(string filename)
        {
            try
            {
                string serverpath = Server.MapPath("~/CoS_OrderSheetAllSheetDoc/" + filename);
                if (!File.Exists(serverpath))
                {
                    ConvertHTMToPDF(filename, "~/CoS_OrderSheetAllSheetDoc/", "<p>Order Sheet</p>");
                }
                else
                {
                    File.Delete(serverpath);
                    ConvertHTMToPDF(filename, "~/CoS_OrderSheetAllSheetDoc/", "<p>Order Sheet</p>");
                }
                ViewState["ALLDocCAddedPDFPath"] = "~/CoS_OrderSheetAllSheetDoc/" + filename;
                ViewState["CoS_OrderSheetAllSheetDoc"] = serverpath;
            }
            catch (Exception)
            {


            }
        }

        public static void MargeMultiplePDF(string[] PDFfileNames, string OutputFile)
        {
            try
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
                        if (fileName != null)
                        {
                            // Create a PDFreader for a certain PDFdocument  
                            if (File.Exists(fileName))
                            {
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
                        }

                    }
                    // Close the PDFdocument and PDFwriter  
                    PDFwriter.Close();
                    PDFdoc.Close();
                }// Disposes the Object of FileStream  
            }
            catch (Exception)
            {


            }
        }

        public void CraetSourceFile(int APP_ID)
        {
            try
            {
                //DataTable dt = OrderSheet_BAL.GetOrderSheetAllDoc(APP_ID);
                //if (dt.Rows.Count > 0)
                //{

                //    string[] addedfilename = new string[2];

                //    addedfilename[0] = Server.MapPath(dt.Rows[0]["file_path"].ToString());
                //    //addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_firstformate"].ToString());
                //    addedfilename[1] = Server.MapPath(dt.Rows[0]["proposalpath_secondformate"].ToString());


                //    string sourceFile = ViewState["CoS_OrderSheetAllSheetDoc"].ToString();

                //    MargeMultiplePDF(addedfilename, sourceFile);
                //    setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());




                //}

                DataTable dt = clsHearingBAL.GetHearingAllDoc(APP_ID);
                if (dt.Rows.Count > 0)
                {

                    string[] addedfilename = new string[dt.Rows.Count];
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        if (File.Exists(Server.MapPath(dt.Rows[i]["ORDRSHEETPATH"].ToString())))
                        {
                            addedfilename[i] = Server.MapPath(dt.Rows[i]["ORDRSHEETPATH"].ToString());
                        }
                    }

                    string sourceFile = ViewState["CoS_OrderSheetAllSheetDoc"].ToString();
                    if (IsValidPdf(sourceFile))
                    {
                        MargeMultiplePDF(addedfilename, sourceFile);
                        setAllPdfPath(ViewState["ALLDocCAddedPDFPath"].ToString());

                    }


                }

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
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

        protected void OnView(object sender, EventArgs e)
        {
            string filePath = (sender as LinkButton).CommandArgument;
            Session["FileName"] = filePath;
            ClientScript.RegisterStartupScript(this.GetType(), "Popup", "ShowPopup();", true);
        }



        protected void btnClose_Click(object sender, EventArgs e)
        {
            try
            {
                pnlDocView.Visible = false;
                ScriptManager.RegisterStartupScript(Page, Page.GetType(), "", "setTimeout(function () { window.scrollTo(0,document.body.scrollHeight); }, 25);", true);

            }
            catch (Exception)
            {


            }
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            try
            {
                if (chkAll.Checked)
                {
                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_AllRRCDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(ViewState["ALLDocCAddedPDFPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();

                }
                else if (chkRecentDoc.Checked)
                {


                    Response.ContentType = "application/octet-stream";
                    Response.AppendHeader("Content-Disposition", "attachment;filename=" + lblProposalIdHeading.Text + "_RecentRRCDoc_" + DateTime.Now.ToString("yyyy-MMM-dd-hhmmss") + ".pdf");
                    string filePath = Server.MapPath(Session["RecentSheetPath"].ToString());
                    Response.TransmitFile(filePath);
                    Response.End();
                }

            }
            catch (Exception ex)
            {

            }
        }

        private void GetPartyDetail()
        {
            try
            {

                if (ViewState["Case_Number"] != null)
                {
                    DataTable dt = OrderSheet_BAL.GetDeatil_OrderSheet(ViewState["Case_Number"].ToString());
                    ViewState["PartyDetail"] = dt;
                    if (dt.Rows.Count > 0)
                    {
                        hdnfApp_Id.Value = dt.Rows[0]["App_ID"].ToString();
                        lblAppID.Text = dt.Rows[0]["App_ID"].ToString(); ;
                        ViewState["App_ID"] = dt.Rows[0]["App_ID"].ToString();
                        //lblApplication_No.Text = dt.Rows[0]["Application_NO"].ToString();
                        lblProposal.Text = dt.Rows[0]["Application_NO"].ToString();
                        lblCase_Number.Text = dt.Rows[0]["Case_Number"].ToString();
                        hdnfCseNunmber.Value = dt.Rows[0]["Case_Number"].ToString();
                        lblCaseNo.Text = dt.Rows[0]["Case_Number"].ToString();
                        hdnDistrict.Value = dt.Rows[0]["District"].ToString();
                        lblDistrict.Text = dt.Rows[0]["District"].ToString();
                        hdnSOOffice.Value = dt.Rows[0]["SubRegistrarOffice"].ToString();
                        lblSROffice.Text = dt.Rows[0]["SubRegistrarOffice"].ToString();
                        lblSRONameHi.Text = dt.Rows[0]["SubRegistrarOffice"].ToString();
                        hdnSource.Value = dt.Rows[0]["Department_Name"].ToString();
                        lblSource.Text = dt.Rows[0]["Department_Name"].ToString();
                        lblCaseNumber.Text = dt.Rows[0]["Case_Number"].ToString();
                        lblCNo.Text = dt.Rows[0]["Case_Number"].ToString();
                        hdnProposal.Value = dt.Rows[0]["Application_NO"].ToString();
                        hdnPartyName1.Value = dt.Rows[0]["PartyNameWithType"].ToString();
                        lblPartyName.Text = dt.Rows[0]["PartyNameWithType"].ToString();
                        //hdnPartyDocName.Value = dt.Rows[0]["DocumentsDiscription"].ToString();
                        hdnPartyFather_Name.Value = dt.Rows[0]["PartyFatherName"].ToString();
                        hdnParty_Address.Value = dt.Rows[0]["PartyAddress"].ToString();
                        lblHearingDt.Text = dt.Rows[0]["HearingDate"].ToString();
                        lblHearingDt1.Text = dt.Rows[0]["HearingDate"].ToString();
                        txtHearingDate.Text = dt.Rows[0]["HearingDate"].ToString();
                        Session["HearingDate"] = dt.Rows[0]["HearingDate"].ToString();


                        string TDate = DateTime.UtcNow.ToString("dd-MM-yyyy");
                        lblTodatDt.Text = Convert.ToString(TDate);

                        lblToday.Text = Convert.ToString(TDate);


                        string ProImpounddate;
                        if (Session["IMPOUND_DATE"] != null)
                        {
                            Session["ProImpoundDate"] = (Session["IMPOUND_DATE"].ToString());
                            ProImpounddate = Session["ProImpoundDate"].ToString();
                        }
                        else
                        {
                            ProImpounddate = Session["ProImpoundDate"].ToString();
                        }

                        string Ordersheetid = "";
                        //string OrdersheetContent = Session["OrdersheetContent"].ToString();
                        //summernote.Value = Session["OrdersheetContent"].ToString();//"उप पंजीयक गुना-2  द्वारा एक  पंजीकृत दस्तावेज दान पत्र  विलेख क्रमांक: " + lblProposal.Text + " दिनांक " + ProImpounddate + " को न्यून मुद्रांकित मानते हुए उक्त दस्तावेज पर मुद्रांक एवं पंजीयन शुल्क वसूली हेतु भेजा गया है। उप पंजीयक द्वारा दस्तावेज की मूल प्रति प्रेषित की गई है जिसे भारतीय स्टाम्प अधिनियम, 1899 की धारा-33 के अंतर्गत दर्ज किया गया।";

                        if (Session["ordersheet_id"] != null)
                        {
                            Ordersheetid = Session["ordersheet_id"].ToString();
                            DataTable ds = OrderSheet_BAL.Get_Ordersheet_proceeding(Ordersheetid);
                            summernote.InnerHtml = ds.Rows[0]["Proceeding"].ToString();
                            ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;
                            Session["ordersheet_id"] = null;
                        }



                        ViewState["Ordersheet_SeekReportContent"] = summernote.InnerHtml;
                        ViewState["OrdersheetContent"] = summernote.InnerHtml;
                        Session["OrdersheetContent"] = summernote.InnerHtml;
                        GetPartyDetailTable();
                        GetPartyDetailDoc();
                        //ClientScript.RegisterStartupScript(this.GetType(), "CallMyFunction", "LoadNotice();", true);
                        DataTable dtt = OrderSheet_BAL.Get_SeekContent_Notice(Convert.ToInt32(ViewState["App_ID"].ToString()));
                        ViewState["PartyDetail"] = dt;

                    }

                }
            }
            catch (Exception)
            {


            }

        }

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
        }



        public string DataTableToJSONWithJavaScriptSerializer(DataTable table)
        {
            JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
            List<Dictionary<string, object>> parentRow = new List<Dictionary<string, object>>();
            Dictionary<string, object> childRow;
            try
            {
                foreach (DataRow row in table.Rows)
                {
                    childRow = new Dictionary<string, object>();
                    foreach (DataColumn col in table.Columns)
                    {
                        childRow.Add(col.ColumnName, row[col]);
                    }
                    parentRow.Add(childRow);
                }
            }
            catch (Exception)
            {


            }
            return jsSerializer.Serialize(parentRow);
        }

        private void GetPartyDetailTable()
        {
            try
            {
                if (ViewState["Case_Number"] != null)
                {
                    int App_ID = Convert.ToInt32(ViewState["App_ID"]);
                    DataTable dsDocDetails = new DataTable();
                    dsDocDetails = OrderSheet_BAL.GetDeatil_OrderSheet_Page(ViewState["Case_Number"].ToString(), App_ID);
                    ViewState["PartyDetail"] = dsDocDetails;
                    ViewState["PartyDetail_PDF"] = dsDocDetails;

                    hdpartyList.Value = DataTableToJSONWithJavaScriptSerializer(dsDocDetails);


                    if (dsDocDetails.Rows.Count > 0)
                    {

                        grdlPartys.DataSource = dsDocDetails;
                        grdlPartys.DataBind();

                        DataTable dt = dsDocDetails;
                        StringBuilder html = new StringBuilder();
                        //html.Append("<table border = '1'>");
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {
                                html.Append("<td style='border: 1px solid black; border - collapse: collapse; padding: 5px 15px;'>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                        // hdpartyList.Value = html.ToString();

                        // html.Append("</table>");

                        //hdpartyList.Controls.Add(new Literal { Text = html.ToString() });
                        //PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                        //grdPartyDetails.DataSource = dsPartyDetails;
                        //grdPartyDetails.DataBind();
                    }

                }
            }
            catch (Exception)
            {


            }

        }

        private void GetPartyDetailDoc()
        {
            try
            {
                if (ViewState["Case_Number"] != null)
                {
                    DataTable dsDocDetails = new DataTable();
                    dsDocDetails = OrderSheet_BAL.GetDeatil_OrderSheet_Doc(ViewState["Case_Number"].ToString());
                    ViewState["PartyDoc"] = dsDocDetails;
                    hdpartyDoc.Value = DataTableToJSONWithJavaScriptSerializer(dsDocDetails);


                    if (dsDocDetails.Rows.Count > 0)
                    {

                        grdDocFile.DataSource = dsDocDetails;
                        grdDocFile.DataBind();
                        DataTable dt = dsDocDetails;
                        StringBuilder html = new StringBuilder();
                        //html.Append("<table border = '1'>");
                        html.Append("<tr>");
                        foreach (DataColumn column in dt.Columns)
                        {
                            html.Append("<th>");
                            html.Append(column.ColumnName);
                            html.Append("</th>");
                        }
                        html.Append("</tr>");
                        foreach (DataRow row in dt.Rows)
                        {
                            html.Append("<tr>");
                            foreach (DataColumn column in dt.Columns)
                            {

                                html.Append("<td style='float: right; border: 1px solid black; border - collapse: collapse; padding: 5px 5px; '>");
                                html.Append(row[column.ColumnName]);
                                html.Append("</td>");
                            }
                            html.Append("</tr>");
                        }
                        // hdpartyList.Value = html.ToString();

                        // html.Append("</table>");

                        //hdpartyList.Controls.Add(new Literal { Text = html.ToString() });
                        //PlaceHolder1.Controls.Add(new Literal { Text = html.ToString() });
                        //grdPartyDetails.DataSource = dsPartyDetails;
                        //grdPartyDetails.DataBind();
                    }

                }
            }
            catch (Exception)
            {


            }

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {

            string PartyType = "";
            if (txtHearingDate.Text == "")
            {

                string Message = "Please select hearing date";
                string Title = "Success";
                ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','success');", true);
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                return;

            }


            else
            {

                SaveOrderSheetPDF();
                //editOS.Attributes.Add("style", "display");
                custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
                pnlEsignDSC.Visible = true;
                pnlHearingDate.Visible = false;
                pnlBtnSave.Visible = true;
                pnlSeekReport.Visible = false;

            }


            if (ViewState["Ordersheet_SeekReportContent"] != null)
            {
                pContent.InnerHtml = ViewState["Ordersheet_SeekReportContent"].ToString();
            }
            else
            {
                pContent.InnerHtml = ViewState["OrdersheetContent"].ToString();
                lblHearingDt.Text = ViewState["HearingDate"].ToString();
                lblHearingDt1.Text = ViewState["HearingDate"].ToString();
            }
        }

        public void generateFormateFirst_PDF()
        {
            try
            {
                int appid = Convert.ToInt32(Session["AppID"].ToString());
                string Appno = Session["Appno"].ToString();
                //string appPath = HttpContext.Current.Request.ApplicationPath;
                //string path = Server.MapPath(appPath + "/Proposal/"+ lblProposalIdHeading.Text+ DateTime.Now.ToString("ddMMyyyyhhmmss")+".pdf");
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                //string divCon = summernote.Value;
                StringWriter sw = new StringWriter();
                HtmlTextWriter hw = new HtmlTextWriter(sw);
                StringBuilder stringBuilder = new StringBuilder();
                stringBuilder.Append("<div style='width: 90%; margin: 0 auto; text-align: center; padding: 40px 30px 30px 30px; margin-top: 0px;'>");
                stringBuilder.Append("<h2 style='font-size: 32px; margin: 0;'>");
                stringBuilder.Append("<label style='font: bold'>");
                stringBuilder.Append("</label>");
                stringBuilder.Append("</h2>");
                stringBuilder.Append("<h2 style='font-size: 24px; margin: 10px;'>Form-I ");
                stringBuilder.Append("</h2>");
                stringBuilder.Append("<h2 style='font-size: 22px; margin-top: 10px;'>[See rule 5 (1)] ");
                stringBuilder.Append("</h2>");
                stringBuilder.Append("<div class='section'>");
                stringBuilder.Append("<div class='point-1'>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin-top: 50px; text-align: left; margin-left: 15px; background: #cccccc70; padding: 5px; width: 99%; letter-spacing: 0.2px; box-shadow: 0 0 4px rgba(0,0,0,0.4'>1. Name and Address of Executants ( Seller / Doner / Releasor / Leaser )</h2>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                string Proposal_ID = Session["ProposalID"].ToString();

                string FileNme = Proposal_ID + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + ".pdf";
                string ProposalSheetPath = Server.MapPath("~/Proposal/" + Proposal_ID);
                ViewState["FirstFormate_Path"] = "~/Proposal/" + Proposal_ID + "/" + FileNme;
                ViewState["SecondFormate_Path"] = "";

                string Savedpath = ConvertHTMToPDF(FileNme, ProposalSheetPath, stringBuilder.ToString());


                //ViewState["FirstFormate_Path"] = ConvertHTMToPDF(FileNme, "~/Proposal/", stringBuilder.ToString());
                //ViewState["SecondFormate_Path"] = "";


                string caseno = Session["CAESNUMBER"].ToString();


                //DataTable dtUp = objClsNewApplication.InsertProposalSheetPath(Convert.ToInt32(appid), ViewState["FirstFormate_Path"].ToString(), ViewState["SecondFormate_Path"].ToString(), caseno, "Proposal Copy");

            }
            catch (Exception)
            {


            }

        }

        protected void Calendar2_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                DataSet dsList = new DataSet();
                int DROID = Convert.ToInt32(Session["DROID"]);

                //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
                DateTime HearingDt = Convert.ToDateTime(DateTime.Now);
                dsList = OrderSheet_BAL.GetHearingCount_COS(DROID);

                if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow DR in dsList.Tables[0].Rows)
                    {

                        if (e.Day.Date == Convert.ToDateTime(DR["HearingDate"]))
                        {
                            Literal literal1 = new Literal();
                            literal1.Text = "<br/>";
                            e.Cell.Controls.Add(literal1);
                            Label label1 = new Label();
                            label1.Text = " Hearing " + Convert.ToString(DR["TotalCaseHearing"]);
                            //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                            label1.Font.Size = new FontUnit(FontSize.Small);
                            e.Cell.Controls.Add(label1);
                            e.Cell.BackColor = System.Drawing.Color.IndianRed;
                            //e.Cell.ForeColor = System.Drawing.Color.White;
                        }

                    }


                }
            }
            catch (Exception)
            {


            }
        }

        private void SaveOrderSheet(string Path)
        {
            try
            {

                GetPartyDetailTable();
                GetPartyDetailDoc();

                string FileName = "OrderSheet_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                //string OrderSheetPath = Server.MapPath("~/OrderSheet/" + lblApplication_No.Text);
                ViewState["ActualPath"] = Path;

                Int16 PartyID = 0;

                string sDate;
                sDate = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                string Hdt = txtHearingDate.Text;
                //DateTime V _HEARINGDATE = DateTime.Now;
                DateTime V_HEARINGDATE = Convert.ToDateTime(sDate);
                ViewState["HearingDate"] = txtHearingDate.Text;
                //ViewState["HearingDate"] = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                GetLocalIPAddress();
                //DateTime HearingDt = Convert.ToDateTime(Hdt);

                //DateTime DT = DateTime.ParseExact(Hdt, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                DataTable dt = OrderSheet_BAL.Get_OrderSheetID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
                if (dt.Rows.Count > 0)
                {
                    //ViewState["Ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                    string Ordersheet_id = dt.Rows[0]["Ordersheet_id"].ToString();
                    Session["ordersheetReader_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                    Session["ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                    Session["ordersheet_id_Status"] = dt.Rows[0]["Ordersheet_id"].ToString();
                    DataSet dtUp = OrderSheet_BAL.UpdateIntoOrderSheet_Reader(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), ViewState["ActualPath"].ToString(), PartyID, summernote.Value, "", "", Convert.ToInt32(Session["ordersheetReader_id"].ToString()));

                    if (dtUp.Tables.Count > 0)
                    {
                        Session["ordersheet_id"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                        Session["ordersheet_id_Status"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                        Session["PROCEEDING"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                        summernote.Value = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                        Session["OrdersheetContent"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
                    }

                }
                else
                {
                    DataSet dtUp = OrderSheet_BAL.InsertIntoOrderSheet(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), ViewState["ActualPath"].ToString(), PartyID, summernote.Value, "", "");

                    if (dtUp.Tables.Count > 0)
                    {
                        Session["ordersheet_id"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                        Session["ordersheet_id_Status"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                        Session["Hearing_id"] = dtUp.Tables[1].Rows[0]["Hearing_id"].ToString();
                        GetPartyDetail();
                    }
                }


                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");

                //Response.Redirect("Notice.aspx", true);
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>AddNotice();</script>");

                //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage1111();</script>");


            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }

        }

        private void SaveOrderSheetPDF()
        {
            try
            {
                StringWriter iSW = new StringWriter();
                HtmlTextWriter iHTW = new HtmlTextWriter(iSW);
                //summernote.RenderControl(iHTW);
                string divCon = summernote.Value;

                StringBuilder stringBuilder = new StringBuilder();
                //  stringBuilder.Append("<div class='main-box' style='width: 100%; margin: 0 auto; text-align: center; border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; text-align: center; padding: 30px 30px 30px 30px;height:1150px'>");
                //stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600; '>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, गुना (म.प्र.)</h2>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600; '>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblHeadingDist.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; '>प्रारूप-अ</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px; '>(परिपत्र दो-1 की कंडिका 1)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px; '>सेक्शन 40(D) - भारतीय स्टाम्प अधीनियम 1899 की धारा 40 (1)(ख) के अंतर्गत एवं 40 (1)(घ) के पालन में</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px; '>आदेश पत्रक</h2><br>");

                stringBuilder.Append("<div style='display: flex; justify-content: start; align-items: baseline; '>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0' >कलेक्टर ऑफ़ स्टाम्प्स का नाम: </p> ");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 15px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'><b>" + hdnCOSDistNameHi.Value + " </b></p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style = 'display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div> <p style = 'font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0;'> प्रकरण  क्रमांक: </p> ");
                stringBuilder.Append("</div>");
                stringBuilder.Append(" <div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'><b> " + lblCaseNo.Text + "</b> </p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div style='display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0; '> विषय:</p> ");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'><b> बाज़ार मूल्य अवधारण / मुद्रांक शुल्क निर्धारण</b></p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 30px; text-align: center; margin: 0; margin-left: 50px; margin-top: 40px; margin-bottom: 25px;'> <b> पक्षकारों के नाम</b> </p>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<div style='float: left;'><p style='font-size: 20px; line-height: 30px; text-align: center; margin: 0; margin-right: 50px;'><b> आवेदक : </b> </p></div>");

                stringBuilder.Append("<table style='width: 940px;border: 1px solid black; border-collapse: collapse; '>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>क्रमांक </th>");
                stringBuilder.Append("<th class='new_th' style='border: 1px solid black; border-collapse: collapse; line-height: 20px;padding: 0 50px 0 50px!important; font-size: 18px; '>नाम </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;'>पता </th>");
                //stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px;  padding: 5px; font-size: 18px; '>पिता / पति </th>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>1.</td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>म प्र शासन<br /> </td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 18px;text-align: center; '>" + lblSROffice.Text + " </td>");
                //stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; padding: 5px; font-size: 18px;text-align: center; '></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");

                stringBuilder.Append("</div>");
                stringBuilder.Append("<div class='m-top'>");
                stringBuilder.Append("<p style='font-size: 18px;text-align: center; margin: 0; margin-top: 18px; margin-bottom: 10px; '><b> विरुद्ध </b></p>");
                stringBuilder.Append("<div style='float: left;'>");
                stringBuilder.Append("<p style='font-size: 20px; line-height: 30px; text-align: center; margin: 0; margin-right: 50px;'> <b> अनावेदक : </b></p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<table  style='width: 940px;border: 1px solid black; border-collapse: collapse;'>");
                stringBuilder.Append("<tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>क्रमांक</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पक्षकार का प्रकार</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>नाम</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पता</th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पिता / पति</th></tr>");
                int srno = 1;
                for (int i = 0; i < ((DataTable)ViewState["PartyDetail_PDF"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + srno + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["TYPE"] + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["Name"] + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["Address"] + "</td>" +
                        "<td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDetail_PDF"]).Rows[i]["Father Name"] + "</td>" +
                        "</tr>");
                    srno++;
                }
                stringBuilder.Append("</table>");
                stringBuilder.Append("<div style = 'display: flex; justify-content: start; align-items: baseline;padding-top: 10px;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0' > सब रजिस्ट्रार कार्यालय:</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                //stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 75px'>" + lblSROffice.Text + "</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style = 'display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; width: 250px; margin: 0' >दस्तावेज़ पंजीयन क्रमांक / प्रारंभ आई डी  :</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px' >" + lblProposal.Text + "</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style = 'display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0'> जिला: </p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 80px'>" + lblDistrict.Text + "</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; width: 220px; margin: 0'> पहली सुनवाई की तारीख  :</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style = 'font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 80px' >" + lblHearingDt.Text + "</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='display: flex; justify-content: start; align-items: baseline;'>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left;  width: 252px; margin: 0'>प्रकरण परिबद्ध करने वाले अधिकारी का नाम :</p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<p style='font-size: 18px; line-height: 22px; text-align: left; margin: 0; margin-left: 50px'>" + lblSource.Text + " </p>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                //stringBuilder.Append("</div>");
                stringBuilder.Append("<p style='font-size: 20px; line-height: 30px; text-align: center; margin: 0; margin-left: 0px; margin-top: 15px; margin-bottom: 3px;'> <b> दस्तावेजों की सूची</b></p>");
                stringBuilder.Append("<div class='row'>");
                stringBuilder.Append("<div class='col-lg-12 p0'>");
                stringBuilder.Append("<table  style='width: 940px;border: 1px solid black; border-collapse: collapse; '><tr><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>क्र.सं.</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पृष्ठों की संख्या</th><th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>दस्तावेजों आदि का विवरण</th></tr>");
                int srno1 = 1;
                for (int i = 0; i < ((DataTable)ViewState["PartyDoc"]).Rows.Count; i++)
                {
                    stringBuilder.Append("<tr><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '><b>" + srno1 + "<b></td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDoc"]).Rows[i]["DocCount"] + "</td><td style='border: 1px solid black; border-collapse: collapse;  padding: 5px; font-size: 18px;text-align: center; '>" + ((DataTable)ViewState["PartyDoc"]).Rows[i]["E_REGISTRY_DEED_DOC_NAME"] + "</td></tr>");
                    srno++;
                }
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                //stringBuilder.Append("</div>"); यायालय कले टर ऑफ़ टा स, भोपाल (म. .)

                stringBuilder.Append("<br>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");

                stringBuilder.Append("<div style='page-break-inside: avoid;'>");
                // stringBuilder.Append("<div class='main-box' style='margin: 0 auto; text-align: center; border: 1px solid #ccc;padding: 0px;margin-top: 0px;'>");
                stringBuilder.Append("<div class='main-box htmldoc' style='margin: 0 auto; text-align: center; padding: 20px 30px 30px 30px; height:1350px; position:relative;top:0px;'>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; font-weight: 600;'>न्यायालय कलेक्टर ऑफ़ स्टाम्प्स, " + lblHeadingDist2.Text + " (म.प्र.)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>प्रारूप-अ</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>(परिपत्र दो-1 की कंडिका 1)</h2>");
                stringBuilder.Append("<h3 style='margin: 0; margin: 10px; font-size: 20px;'>राजस्व आदेशपत्र</h3>");
                stringBuilder.Append("<h2 style='font-size: 20px; margin: 0; margin-bottom: 10px;'>कलेक्टर ऑफ़ स्टाम्प, " + hdnCOSDistNameHi.Value + " के न्यायालय में मामला क्रमांक-" + lblCaseNumber.Text + "</h2> ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("<table style='width: 940px;border: 1px solid black; border-collapse: collapse;' ");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>आदेश क्रमांक कार्यवाही ");
                stringBuilder.Append("<br>");
                stringBuilder.Append("की तारीख एवं स्थान </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पीठासीन अधिकारी के हस्ताक्षर सहित आदेश पत्र अथवा कार्यवाही");
                stringBuilder.Append("<br/>मध्यप्रदेश शासन विरूद्ध </th>");
                stringBuilder.Append("<th style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px; '>पक्षों/वकीलों <br> आदेश  पालक लिपिक के हस्ताक्षर</th>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("<tr>");
                stringBuilder.Append("<td style='border: 1px solid black; height: 1030px;vertical-align: baseline;border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '>");
                stringBuilder.Append("<div class='content' style='padding: 15px'>" + lblToday.Text + "</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black;  height: 1030px;vertical-align: baseline;border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '>");
                stringBuilder.Append("<div style='padding: 2px;'>");
                stringBuilder.Append("<p style='text-align: center;'>प्रकरण क्रमांक: " + (lblCNo.Text) + "</p>");
                //stringBuilder.Append("<p style='text-align: center;'>Case Number: " + (lblCNo.Text) +  lblAppID.Text + lblTodatDt.Text + "</p>");
                stringBuilder.Append("<p style='text-align: justify !important;'>" + divCon + "</p>");
                //stringBuilder.Append("<p style='text - align: left;'>" + lblSeekContent.Text + "</p>");
                stringBuilder.Append("<div>");
                stringBuilder.Append("<b style='float: left; text-align: center; padding: 2px 0 5px 0;position: relative;top: 170px;'>पेशी दिनांक <br/>");
                //stringBuilder.Append(lblHearingDt.Text + "</b>");
                stringBuilder.Append("<b style='text-align: left;position: relative;top: 5px;'>" + (txtHearingDate.Text) + "</b>");

                stringBuilder.Append("</b>");
                //stringBuilder.Append(DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture));
                stringBuilder.Append("<p></p>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 120px; color:#fff;left:-80px;'>#8M2h8A4@N78O%bJd<br/></b>");
                stringBuilder.Append("<b style='float: right; text-align: center; padding: 2px 0 5px 0; position: relative;top: 200px;left:130px;'>कलेक्टर ऑफ़ स्टाम्प्स,<br/>" + hdnCOSDistNameHi.Value + " <br/><br/> </b>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</td>");
                stringBuilder.Append("<td style='border: 1px solid black; border-collapse: collapse; line-height: 20px; padding: 5px; font-size: 18px;text-align: center; '></td>");
                stringBuilder.Append("</tr>");
                stringBuilder.Append("</table>");
                stringBuilder.Append("</div>");
                stringBuilder.Append("</div>");
                // stringBuilder.Append("</div>");



                string FileNme = lblProposalIdHeading.Text + "_" + DateTime.Now.ToString("yyyyMMMddhhmmss") + "_OrderSheet.pdf";
                ViewState["FileNameUnSignedPDF"] = FileNme;
                ViewState["UnSignedPDF"] = ConvertHTMToPDF(FileNme, "~/OrderSheet/", stringBuilder.ToString());
                Session["RecentSheetPath"] = "~/OrderSheet/" + FileNme;
                //ifPDFViewer.Src = "~/RRCOrderSheet/" + FileNme;

                SaveOrderSheet("~/OrderSheet/" + FileNme);
                //setRecentSheetPath();
                txtHearingDate.Text = "";

            }
            catch (Exception ex)
            {

            }

        }
        public string ConvertHTMToPDF(string FileNme, string path, string strhtml)
        {
            try
            {
                string FileName = FileNme;
                string OrderSheetPath = Server.MapPath(path);
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
                converter.Options.MarginLeft = 30;
                converter.Options.MarginRight = 30;
                converter.Options.MarginTop = 20;
                converter.Options.MarginBottom = 30;

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
            catch (Exception)
            {
                return "";
            }
        }

        protected void rdReportYes_CheckedChanged(object sender, EventArgs e)
        {
            try
            {
                if (rdReportYes.Checked == true)
                {

                    ScriptManager.RegisterStartupScript(this, GetType(), "ServerControlScript", "<script>Loaderstop();</script>", false);

                    if (txtHearingDate.Text == "")
                    {
                        Session["OrdersheetContent"] = summernote.InnerHtml;
                        Session["HearingDate"] = txtHearingDate.Text;


                        Session["AppID"] = Session["AppID"].ToString();
                        Session["Appno"] = Session["Appno"].ToString();
                        Session["Case_Number"] = ViewState["Case_Number"].ToString();


                        //Response.Redirect("ReportSeeking.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
                        Response.Redirect("ReportSeeking.aspx", false);

                       
                    }
                    else
                    {
                        Session["OrdersheetContent"] = summernote.InnerHtml;
                        Session["HearingDate"] = txtHearingDate.Text;

                        Session["AppID"] = Session["AppID"].ToString();
                        Session["Appno"] = Session["Appno"].ToString();
                        Session["Case_Number"] = ViewState["Case_Number"].ToString();

                        //Response.Redirect("ReportSeeking.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
                        Response.Redirect("ReportSeeking.aspx", false);


                    }
                }
            }
            catch (Exception)
            {


            }
        }

        protected void btnEsignDSC_Click(object sender, EventArgs e)
        {
            try
            {
                int App_ID = Convert.ToInt32(ViewState["App_ID"]);
                int order_id = Convert.ToInt32(Session["ordersheet_id_Status"].ToString());
                Session["AppID"] = App_ID;

                //DataTable dtUp = OrderSheet_BAL.Get_Orderid(Convert.ToInt32(Session["AppID"].ToString()));
                //if (dtUp.Rows.Count > 0)
                //{
                //    Session["ordersheet_id"] = dtUp.Rows[0]["ordersheet_id"].ToString();
                //}

                //int order_id = Convert.ToInt32(Session["ordersheet_id"].ToString());
                //int order_id = 2105;
                if (ddl_SignOption.SelectedValue == "1")    //eSign CDAC
                {
                    if (TxtLast4Digit.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        TxtLast4Digit.Focus();
                        return;
                    }
                    Session["HearingDate"] = lblHearingDt.Text;

                    int Flag = 1;
                    //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                    //-------eSign Start------------------------

                    //string Location = "Project Office -" + HF_Office.Value;
                    string Location = "Bhopal";

                    string ApplicationNo = hdnProposal.Value;

                    string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                    PdfName = PdfName.Replace("~/OrderSheet/", "");
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + PdfName.ToString());
                    string flSourceFile = FileNamefmFolder;

                    if (File.Exists(FileNamefmFolder))
                    {
                        if (ddl_SignOption.SelectedValue == "1")
                        {
                            if (TxtLast4Digit.Text.Length != 4)
                            {
                                TxtLast4Digit.Focus();
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                                return;
                            }

                            string Last4DigitAadhaar = TxtLast4Digit.Text;

                            if (File.Exists(FileNamefmFolder))
                            {
                                //txtDepartmentID.Text = "MPWCDDOH01";
                                string ResponseURL = null;
                                //string eSignURL = null;
                                //string secretKey = null;
                                string txtSignedBy = "Collector of Stamp";
                                string UIDToken = "";
                                //string DepartmentID = txtDepartmentID.Text;
                                //string Last4DigitAadhaar = TxtLast4Digit.Text;
                                string TransactionId = getTransactionID();
                                string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                                string TransactionOn = "Pre";
                                Session["order_id"] = order_id;
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                                ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx");

                                //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "1", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                                AuthMode authMode = new AuthMode();

                                //string authType = "";

                                if (ddleAuthMode.SelectedValue == "1")
                                {
                                    authMode = AuthMode.OTP;


                                }
                                else if (ddleAuthMode.SelectedValue == "2")
                                {
                                    authMode = AuthMode.Biometric;

                                }



                                eSigner.eSigner _esigner = new eSigner.eSigner();


                                _esigner.CreateRequest(ResponseURL, eSignURL, TransactionOn, txtSignedBy, Application_Id, UIDToken, Department_Id, Secretkey, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                                //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), order_id);
                            }
                            else
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','pdf फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);
                            }

                        }



                    }
                    else
                    {
                        //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);
                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Unable to Get PDF details', '', 'error')", true);
                    }
                }

                else if (ddl_SignOption.SelectedValue == "3")     // eSign Emudra
                {
                    if (TxtLast4Digit.Text.Length != 4)
                    {

                        ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Enter Last 4 Digit of Adhar Card', '', 'error')", true);
                        TxtLast4Digit.Focus();
                        return;
                    }
                    Session["HearingDate"] = lblHearingDt.Text;

                    int Flag = 1;
                    //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag);

                    //-------eSign Start------------------------

                    //string Location = "Project Office -" + HF_Office.Value;
                    string Location = "Bhopal";

                    string ApplicationNo = hdnProposal.Value;

                    string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                    PdfName = PdfName.Replace("~/OrderSheet/", "");
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + PdfName.ToString());
                    string flSourceFile = FileNamefmFolder;

                    if (File.Exists(FileNamefmFolder))
                    {
                        if (ddl_SignOption.SelectedValue == "3")
                        {
                            if (TxtLast4Digit.Text.Length != 4)
                            {
                                TxtLast4Digit.Focus();
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                                return;
                            }

                            string Last4DigitAadhaar = TxtLast4Digit.Text;

                            if (File.Exists(FileNamefmFolder))
                            {
                                //txtDepartmentID.Text = "MPWCDDOH01";
                                string ResponseURL_eMudra = null;
                                //string eSignURL = null;
                                //string secretKey = null;
                                string txtSignedBy = "Collector of Stamp";
                                string UIDToken = "";
                                //string DepartmentID = txtDepartmentID.Text;
                                //string Last4DigitAadhaar = TxtLast4Digit.Text;
                                string TransactionId = getTransactionID();
                                string aspesignpemFilePath = Server.MapPath("aspesign.pem");
                                string TransactionOn = "Pre";
                                Session["order_id"] = order_id;
                                //ResponseURL = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx?Case_Number=" + Session["CaseNum"] + "&App_Id=" + Session["AppID"] + "&AppNo=" + Session["ProposalID"] + "&Flag=" + Flag + "&Order_id=" + order_id);
                                ResponseURL_eMudra = Request.Url.OriginalString.Replace(Path.GetFileName(Request.Url.AbsoluteUri), "ResponseFromeSign_Ordersheet.aspx");

                                //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "1", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                                AuthMode authMode = new AuthMode();

                                //string authType = "";

                                if (ddleAuthMode.SelectedValue == "1")
                                {
                                    authMode = AuthMode.OTP;


                                }
                                else if (ddleAuthMode.SelectedValue == "2")
                                {
                                    authMode = AuthMode.Biometric;

                                }

                                eSigner.eSigner _esigner = new eSigner.eSigner();

                                _esigner.CreateRequest_eMudra(ResponseURL_eMudra, eSignURL_eMudra, TransactionOn, txtSignedBy, Application_Id_eMudra, UIDToken, Department_Id_eMudra, Secretkey_eMudra, Last4DigitAadhaar, TransactionId, flSourceFile, authMode, aspesignpemFilePath, Page);
                                //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), order_id);
                            }
                            else
                            {
                                this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','pdf फाइल बनाने में त्रुटी हुई है | कृपया पुन: प्रयास करें  ', 'warning');", true);
                            }

                        }



                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                    }
                }

                else if (ddl_SignOption.SelectedValue == "2")    //HSM DSC
                {
                    //TxtLast4Digit.Visible = false;


                    string PdfName = ViewState["FileNameUnSignedPDF"].ToString();
                    PdfName = PdfName.Replace("~/OrderSheet/", "");
                    //string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + ApplicationNo + "/" + PdfName.ToString());
                    string FileNamefmFolder = HttpContext.Current.Server.MapPath(@"~/OrderSheet/" + PdfName.ToString());
                    string flSourceFile = FileNamefmFolder;
                    string unsignFilePath = FileNamefmFolder;

                    string path = @"" + unsignFilePath;
                    string file = Path.GetFileNameWithoutExtension(path);
                    string NewPath = path.Replace(file, file + "_Signed");

                    //string signFileFinalPath = @"/CMS/OrderSheet/CMS-R-05032400001549_2024Mar20122627_OrderSheet-Signed.pdf";
                    string signFileFinalPath = NewPath;

                    string label = Session["HSMCertLabel"].ToString();
                    string signName = "Collector of Stamp"; //// Session["Designation"].ToString();
                    string location = Session["District_NameEN"].ToString();
                    string reason = "Order Sheet";
                    string partitionName = Partition_Name;
                    string partitionPassword = Partition_Password;
                    string hsmSlotNo = HSM_Slot_No; ////Session["HSMSlotNo"].ToString();
                    string javaPath = Java_Path;

                    //string label = "MAHESH_KUMAR";
                    //string signName = "COS";
                    //string location = "Guna";
                    //string reason = "Order Sheet";
                    //string partitionName = "sampadap2";
                    //string partitionPassword = "sampada@part2";
                    string msg = "";
                    
                    Session["order_id"] = Session["ordersheet_id_Status"].ToString();
                    if (File.Exists(FileNamefmFolder))
                    {
                        HSMSigner hSMSigner = new HSMSigner(unsignFilePath, signFileFinalPath, label, signName, location, reason, partitionName, partitionPassword, hsmSlotNo, javaPath);

                        //hSMSigner.hsm_DSC();
                        msg = hSMSigner.hsm_DSC();
                        //Session["HSM_DSC"] = hsmMsg.Text;
                        //hsmMsg.Text = javaPath + ", error: " + msg;
                        if (File.Exists(NewPath))
                        {
                            Session["RecentSheetPath"] = NewPath;

                            int Flag = 1;
                            string resp_status = 1.ToString();
                            string Response_From = "OrdersheetDSC";
                            //string url = "Notice.aspx?Case_Number=" + Session["CaseNum"].ToString() + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString() + "&Flag=" + Flag + "&Response_Status=" + resp_status;
                            //string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status;
                            string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_From=" + Response_From;

                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowMessageDSC('" + url + "')", true);


                            ///ScriptManager.RegisterStartupScript(this, this.GetType(), "myalert", "alert('Signed ordersheet saved Successfully');window.location='" + url + "';", true);
                            ///
                            //ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('Signed ordersheet saved Successfully','success');window.location='" + url + "';", true);

                            //this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Signed ordersheet saved Successfully', 'success');window.location='" + url + "'", true);

                        }
                        else
                        {
                            if (msg != "")
                            {
                                ddl_SignOption.SelectedValue = "0";
                                ddleAuthMode.SelectedValue = "0";

                                objClsNewApplication.InsertExeption("Index_Tab_ErrorException.Message = " + msg + ",StatusDescription = Error in HSM DSC", "COS Ordersheet", "Ordersheet.aspx", GetLocalIPAddress());
                            }
                            ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "MyFunction", "ShowErrorMessageDSC('" + msg + "')", true);
                        }
                        //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(App_ID), "2", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

                    }
                    else
                    {
                        this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','Unable to Get PDF details', 'info');", true);

                    }



                    //eSigner.eSigner _esigner = new eSigner.eSigner();

                    //if (TxtLast4Digit.Text.Length != 4)
                    //{
                    //    TxtLast4Digit.Focus();
                    //    this.ClientScript.RegisterStartupScript(this.GetType(), "SweetAlert", "swal('','कृपया आधार के अंतिम 4 अंक प्रविष्ट करें', 'warning');", true);
                    //    return;
                    //}

                    //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(App_ID, ddl_SignOption.SelectedValue, "", GetLocalIPAddress(), order_id);


                }
            }
            catch (Exception)
            {


            }
            //else
            //{
            //    string eSignDSCMessage = "Please select eSign or DSC in dropdown";
            //    string Title = "Success";
            //    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + eSignDSCMessage + "','success');", true);
            //    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
            //    return;

            //    //ClientScript.RegisterClientScriptBlock(this.GetType(), "alert", "swal('Please Select eSign or DSC in Dropdown', '', 'error')", true);
            //}



            //-------eSign End------------------------

        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                var ci = new CultureInfo("fr-FR");
                txtHearingDate.Text = Convert.ToDateTime(Calendar1.SelectedDate.ToShortDateString()).ToString("dd/MM/yyyy", ci);
                lblHearingDt.Text = Convert.ToDateTime(Calendar1.SelectedDate.ToShortDateString()).ToString("dd/MM/yyyy", ci);
                lblHearingDt1.Text = Convert.ToDateTime(Calendar1.SelectedDate.ToShortDateString()).ToString("dd/MM/yyyy", ci);
                divCalender.Visible = true;
                divCalender.Style.Add("display", "none");

                //ViewState["OrdersheetContent"] = summernote.InnerHtml;
                //GetPartyDetail();
                //pContent.InnerHtml = ViewState["OrdersheetContent"].ToString();

                pContent.InnerHtml = summernote.InnerText;
            }
            catch (Exception)
            {


            }

        }

        protected void Calendar1_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e)
        {
            divCalender.Visible = true;
            divCalender.Style.Add("display", "block");
            //GetPartyDetail();
            pContent.InnerHtml = ViewState["OrdersheetContent"].ToString();


        }

        //protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        //{
        //    DataSet dsList = new DataSet();
        //    if (e.Day.Date < DateTime.Today)
        //    {
        //        // Disable the date cell
        //        e.Day.IsSelectable = false;
        //        e.Cell.ForeColor = System.Drawing.Color.Gray; // Optionally, you can change the text color for disabled dates


        //        e.Cell.Font.Strikeout = true;
        //    }

        //    //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
        //    DateTime HearingDt = Convert.ToDateTime(DateTime.Now);
        //    dsList = OrderSheet_BAL.GetHearingCount_COS();

        //    if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow DR in dsList.Tables[0].Rows)
        //        {
        //            try
        //            {
        //                if (DR["HearingDate"] != null)
        //                {

        //                    string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];


        //                    DateTime hearingDate = Convert.ToDateTime(systemDate);
        //                    //string inputDateString = DR["HearingDate"].ToString();
        //                    //DateTime hearingDate = DateTime.ParseExact(inputDateString, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

        //                    //// Format the DateTime object to the desired output format
        //                    //string formattedDate = hearingDate.ToString("dd/MM/yyyy");

        //                    ////string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
        //                    //string systemDate = formattedDate.ToString().Split('/')[1] + "/" + formattedDate.ToString().Split('/')[0] + "/" + formattedDate.ToString().Split('/')[2];

        //                    //string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
        //                    if (Convert.ToDateTime(e.Day.Date) == hearingDate)
        //                    {
        //                        Literal literal1 = new Literal();
        //                        literal1.Text = "<br/>";
        //                        e.Cell.Controls.Add(literal1);
        //                        Label label1 = new Label();
        //                        label1.Text = " Hearing " + Convert.ToString(DR["TotalCaseHearing"]);
        //                        //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
        //                        label1.Font.Size = new FontUnit(FontSize.Small);
        //                        e.Cell.Controls.Add(label1);
        //                        e.Cell.BackColor = System.Drawing.Color.IndianRed;
        //                        //e.Cell.ForeColor = System.Drawing.Color.White;
        //                    }
        //                }

        //            }
        //            catch (Exception)
        //            {

        //            }

        //        }

        //    }
        //    pContent.InnerHtml = Session["OrdersheetContent"].ToString();

        //    //GetPartyDetail();
        //}



        protected void Calendar1_DayRender(object sender, DayRenderEventArgs e)
        {
            try
            {
                DataSet dsList = new DataSet();
                if (e.Day.Date < DateTime.Today)
                {
                    // Disable the date cell
                    e.Day.IsSelectable = false;
                    e.Cell.ForeColor = System.Drawing.Color.Gray; // Optionally, you can change the text color for disabled dates


                    e.Cell.Font.Strikeout = true;
                }

                //DataTable dt = clsNoticeBAL.GetHearingDetails(HearingDt);
                DateTime HearingDt = Convert.ToDateTime(DateTime.Now);
                dsList = OrderSheet_BAL.GetHearingCount_COS_OrderSheet();

                if (dsList != null && dsList.Tables.Count > 0 && dsList.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow DR in dsList.Tables[0].Rows)
                    {
                        try
                        {
                            if (DR["HearingDate"] != null)
                            {

                                string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];


                                DateTime hearingDate = Convert.ToDateTime(systemDate);
                                //string inputDateString = DR["HearingDate"].ToString();
                                //DateTime hearingDate = DateTime.ParseExact(inputDateString, "MM/dd/yyyy hh:mm:ss tt", CultureInfo.InvariantCulture);

                                //// Format the DateTime object to the desired output format
                                //string formattedDate = hearingDate.ToString("dd/MM/yyyy");

                                ////string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
                                //string systemDate = formattedDate.ToString().Split('/')[1] + "/" + formattedDate.ToString().Split('/')[0] + "/" + formattedDate.ToString().Split('/')[2];

                                //string systemDate = DR["HearingDate"].ToString().Split('/')[1] + "/" + DR["HearingDate"].ToString().Split('/')[0] + "/" + DR["HearingDate"].ToString().Split('/')[2];
                                if (Convert.ToDateTime(e.Day.Date) == hearingDate)
                                {
                                    Literal literal1 = new Literal();
                                    literal1.Text = "<br/>";
                                    e.Cell.Controls.Add(literal1);
                                    Label label1 = new Label();
                                    label1.Text = " Hearing " + Convert.ToString(DR["TotalCaseHearing"]);
                                    //label1.Text = (string)HolidayList[e.Day.Date.ToShortDateString()];
                                    label1.Font.Size = new FontUnit(FontSize.Small);
                                    e.Cell.Controls.Add(label1);
                                    e.Cell.BackColor = System.Drawing.Color.IndianRed;
                                    //e.Cell.ForeColor = System.Drawing.Color.White;
                                }
                            }

                        }
                        catch (Exception)
                        {

                        }

                    }

                }
                pContent.InnerHtml = summernote.InnerHtml.ToString();

                //pContent.InnerHtml = ViewState["OrdersheetContent"].ToString();

                //GetPartyDetail();
            }
            catch (Exception)
            {


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
        protected void grdSRDoc_Sorted(object sender, GridViewSortEventArgs e)
        {
            try
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
            catch (Exception)
            {


            }

        }

        protected void btnDraftSave_Click(object sender, EventArgs e)
        {
            try
            {
                string PartyType = "";
                if (txtHearingDate.Text == "")
                {

                    string Message = "Please select hearing date";
                    string Title = "Success";
                    ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "Script", "swal('" + Title + "','" + Message + "','success');", true);
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessageNotVerified();</script>");
                    return;

                }


                else
                {

                    //SaveOrderSheetPDF();
                    try
                    {

                        GetPartyDetailTable();
                        GetPartyDetailDoc();

                        //string FileName = "OrderSheet_" + DateTime.Now.ToString("ddMMyyyyhhmmss") + ".pdf";
                        ////string OrderSheetPath = Server.MapPath("~/OrderSheet/" + lblApplication_No.Text);
                        //ViewState["ActualPath"] = Path;

                        Int16 PartyID = 0;

                        string sDate;
                        sDate = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", null).ToString("MM/dd/yyyy");

                        string Hdt = txtHearingDate.Text;
                        //DateTime V _HEARINGDATE = DateTime.Now;
                        DateTime V_HEARINGDATE = Convert.ToDateTime(sDate);
                        ViewState["HearingDate"] = txtHearingDate.Text;
                        //ViewState["HearingDate"] = DateTime.ParseExact(txtHearingDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);

                        GetLocalIPAddress();
                        //DateTime HearingDt = Convert.ToDateTime(Hdt);

                        //DateTime DT = DateTime.ParseExact(Hdt, "dd/MM/yyyy", System.Globalization.CultureInfo.CurrentUICulture.DateTimeFormat);
                        DataTable dt = OrderSheet_BAL.Get_OrderSheetID_COSReader(Convert.ToInt32(Session["AppID"].ToString()));
                        if (dt.Rows.Count > 0)
                        {
                            //ViewState["Ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            string Ordersheet_id = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["ordersheetReader_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["ordersheet_id"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            Session["ordersheet_id_Status"] = dt.Rows[0]["Ordersheet_id"].ToString();
                            DataSet dtUp = OrderSheet_BAL.InsertIntoOrderSheet_Reader(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), "", PartyID, summernote.Value, "", "", Convert.ToInt32(Session["ordersheetReader_id"].ToString()));

                            if (dtUp.Tables.Count > 0)
                            {
                                Session["ordersheet_id"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                                Session["ordersheet_id_Status"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                                Session["PROCEEDING"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                                summernote.Value = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                                Session["OrdersheetContent"] = dtUp.Tables[0].Rows[0]["PROCEEDING"].ToString();
                                Session["Hearing_id"] = dtUp.Tables[1].Rows[0]["Hearing_id"].ToString();
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> AddOrdersheet();</script>");
                            }

                        }
                        else
                        {
                            DataSet dtUp = OrderSheet_BAL.InsertIntoOrderSheet(Convert.ToInt32(Session["AppID"].ToString()), V_HEARINGDATE, lblProposalIdHeading.Text, ViewState["Case_Number"].ToString(), "", PartyID, summernote.Value, "", "");

                            if (dtUp.Tables.Count > 0)
                            {
                                Session["ordersheet_id"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                                Session["ordersheet_id_Status"] = dtUp.Tables[0].Rows[0]["ordersheet_id"].ToString();
                                Session["Hearing_id"] = dtUp.Tables[1].Rows[0]["Hearing_id"].ToString();
                                GetPartyDetail();
                            }
                        }


                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");



                        //Response.Redirect("Notice.aspx", true);
                        //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());

                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>AddNotice();</script>");



                        //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage1111();</script>");


                    }
                    catch (Exception ex)
                    {
                        Response.Write("Error: " + ex.Message);
                    }
                    //Page.ClientScript.RegisterStartupScript(this.GetType(), "none", "<script>ShowMessage();</script>");
                    //editOS.Attributes.Add("style", "display");


                    //custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
                    pnlEsignDSC.Visible = false;
                    pnlHearingDate.Visible = true;
                    pnlBtnSave.Visible = true;
                    pnlSeekReport.Visible = true;

                }

                if (ViewState["Ordersheet_SeekReportContent"] != null)
                {
                    //pContent.InnerHtml = ViewState["Ordersheet_SeekReportContent"].ToString();
                    //pContent.InnerHtml = Session["OrdersheetContent"].ToString();

                }
                else
                {
                    pContent.InnerHtml = Session["OrdersheetContent"].ToString();//ViewState["OrdersheetContent"].ToString();
                    lblHearingDt.Text = ViewState["HearingDate"].ToString();
                    lblHearingDt1.Text = ViewState["HearingDate"].ToString();
                }

                //Response.Redirect("Ordersheet_Pending.aspx");
                ClientScript.RegisterStartupScript(this.GetType(), "none", "<script> OrderSheetSave();</script>");
            }
            catch (Exception)
            {


            }
        }



        protected void btnFinalSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                SaveOrderSheetPDF();

                custom_tabs_one_profile_tab.Attributes["class"] = "nav-link disabled";
                pnlEsignDSC.Visible = true;
                pnlHearingDate.Visible = false;
                pnlBtnSave.Visible = false;
                pnlSeekReport.Visible = false;

                pContent.InnerHtml = Session["OrdersheetContent"].ToString();//ViewState["OrdersheetContent"].ToString();
                lblHearingDt.Text = ViewState["HearingDate"].ToString();
                lblHearingDt1.Text = ViewState["HearingDate"].ToString();

                string divCon = summernote.Value;
                if (ViewState["Ordersheet_SeekReportContent"] != null)
                {
                    pContent.InnerHtml = ViewState["Ordersheet_SeekReportContent"].ToString();
                }

            }
            catch (Exception)
            {


            }
        }

        protected static string Api_Comsumedata(string DocumentType, int RegID, string Tocan)
        {
            //var tocan = "eyJhbGciOiJSUzI1NiJ9.eyJzdWIiOiJtYW5vai5kcm8uaGFyZGEuYXBwcm92ZXJAbXAuZ292LmluIiwiaXAiOiIxMDMuMTYwLjQ5LjE0MCIsInVzZXJBZ2VudCI6Ik1vemlsbGEvNS4wIChXaW5kb3dzIE5UIDEwLjA7IFdpbjY0OyB4NjQpIEFwcGxlV2ViS2l0LzUzNy4zNiAoS0hUTUwsIGxpa2UgR2Vja28pIENocm9tZS8xMjMuMC4wLjAgU2FmYXJpLzUzNy4zNiBFZGcvMTIzLjAuMC4wIiwiZXhwIjoxNzEzMDIxMTQzLCJpYXQiOjE3MTMwMTc1NDN9.jSBvREDB2fGh_Cwkp0qTgXDGQQY1pfn8SczD11U2_sETckblfy5ErPgwoHYHEYfEDMjRO_xQm5MnMAiWHnfDeTc_kTRnI9EMcvxnxIZ4t_d7LgAAEwB9eJmtosiHULc9SskJqfdnWxh5LbBOX4DBkpSrQF5dtICaT7uvKMPeSwhIx9Qi9P5g6gyFnIBlm4BXD-7VnE62K7G0l6RrNgzDc9vnfttrFSPC77tyw1mnH7tYCGgNQmL0AbKkWuCum0YEGhA9tYHFK9Fx7NQpBMai0fkJ3DEg0-T2RBMwGagiy8nL_c2BJ0OWBe87xAIQkm5f8EyTz-5OPDnNxb5S7cPI_Q";
            //var client = new RestClient("https://ersuat2.mp.gov.in/sampadaService/department/ereg/downloadDocument/" + DocumentType + "/" + RegID + "");
            var client = new RestClient(RegProposalAttDocument_url + DocumentType + "/" + RegID + "");

            var request = new RestRequest(Method.POST);
            //request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", Tocan);

            //request.AddHeader("Authorization", tocan);
            //request.AddParameter("userid", "1");
            request.RequestFormat = DataFormat.Json;
            IRestResponse response = client.Execute(request);
            string Result = response.Content;
            string base64 = "";
            if (Result != "")
            {
                JavaScriptSerializer oJS = new JavaScriptSerializer();
                oJS.MaxJsonLength = 5000000;
                ResonseA resonse = oJS.Deserialize<ResonseA>(Result);
                base64 = resonse.responseData;
                //bytes= Convert.FromBase64String(resonse.responseData);
            }
            else
            {
                base64 = null;
            }
            // Generate a unique file name

            //string encodedPdfData = "data:application/pdf;base64," + base64 + "";
            return base64;

        }



    }
}