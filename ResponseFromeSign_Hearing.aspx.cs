using eSigner;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;

namespace CMS_Sampada.CoS
{
    public partial class ResponseFromeSign_Hearing : System.Web.UI.Page
    {
        eSigner.eSigner _esigner = new eSigner.eSigner();
        protected void Page_Load(object sender, EventArgs e)
        {
            getdata();
            try
            {

                if (!IsPostBack)
                {

                }

            }

            catch (Exception ex)
            {
                lblStatus.Text = "Error while signing the document : " + ex.Message;
            }
        }

        //protected void btnDownload_Click(object sender, EventArgs e)
        //{
        //    _esigner.DownLoad();
        //}


        private void getdata()
        {


            string casenum = "";
            string appid = "";
            string AppNo = "";
            string hearingdate = "";
            string Party_ID = "";
            string Notice_Id = "";
            string hearing_id = "";
            string status_id = "";


            if (Request.QueryString["Case_Number"] != null)
            {
                casenum = Request.QueryString["Case_Number"].ToString();
            }
            if (Request.QueryString["App_Id"] != null)
            {
                appid = Request.QueryString["App_Id"].ToString();
            }
            if (Request.QueryString["AppNo"] != null)
            {
                AppNo = Request.QueryString["AppNo"].ToString();
            }
            if (Request.QueryString["HearingDate"] != null)
            {
                hearingdate = Request.QueryString["HearingDate"].ToString();
            }
            if (Request.QueryString["Party_ID"] != null)
            {
                Party_ID = Request.QueryString["Party_ID"].ToString();
            }
            if (Request.QueryString["NoticeId"] != null)
            {
                Notice_Id = Request.QueryString["NoticeId"].ToString();
            }
            if (Request.QueryString["hearing_id"] != null)
            {
                hearing_id = Request.QueryString["hearing_id"].ToString(); 
            }

            if (Request.QueryString["Status_Id"] != null)
            {
                status_id = Request.QueryString["Status_Id"].ToString();
            }



            //string casenum = Request.QueryString["Case_Number"].ToString();
            //Session["Case_Number_Resp"] = casenum;
            //string appid = Request.QueryString["App_Id"].ToString();
            //Session["appid_Resp"] = appid;
            //string Appno = Request.QueryString["AppNo"].ToString();
            //Session["appnum_Resp"] = Appno;
            //string hearingdate = Request.QueryString["HearingDate"].ToString();
            //string Party_ID = Request.QueryString["Party_ID"].ToString();
            //string RAM_Disabled = Request.QueryString["RAM_Disabled"].ToString();
            //string Notice_Id = Request.QueryString["Notice_ID"].ToString();
            //Session["Notice_Id_Resp"] = Notice_Id;
            //Session["HearingDate"] = hearingdate;
            //Session["Party_ID"] = Party_ID;
            //Session["RAMdts"] = RAM_Disabled;


            //if (Session["Party_ID"] != null)
            //{
            //    Session["Party_ID"] = Session["Party_ID"].ToString();
            //}



            //btnDownload.Visible = false;

            NameValueCollection response = Request.Form;
            int status = 0;

            if (!string.IsNullOrEmpty(response["msg"]))
            {
                string xmlData = response["msg"];
                byte[] data1 = Convert.FromBase64String(xmlData);
                string decodedString1 = Encoding.UTF8.GetString(data1);

                //Parse xml response to xml object.
                XmlDocument xmldoc = new XmlDocument();
                xmldoc.PreserveWhitespace = true;
                xmldoc.LoadXml(decodedString1);


                // Create the signed XML object.
                SignedXml sxml = new SignedXml(xmldoc);
                try
                {
                    // Get the XML Signature node and load it into the signed XML object.
                    XmlNode dsig = xmldoc.GetElementsByTagName("Signature", SignedXml.XmlDsigNamespaceUrl)[0];
                    sxml.LoadXml((XmlElement)dsig);
                }
                catch
                {
                    throw new Exception("no signature found in response.");
                }

                X509Certificate2 x509 = new X509Certificate2(Server.MapPath("aspesign.cer"));

                if (sxml.CheckSignature(x509, true))
                {
                    XmlNode _AspEsignResp = xmldoc.GetElementsByTagName("AspEsignResp")[0];
                    string errCode = "", errMsg = "", transId = "";//, environment = "";

                    status = Convert.ToInt16(_AspEsignResp.Attributes["status"].Value);
                    if (_AspEsignResp.Attributes["errCode"] != null)
                        errCode = _AspEsignResp.Attributes["errCode"].Value;
                    if (_AspEsignResp.Attributes["errMsg"] != null)
                        errMsg = _AspEsignResp.Attributes["errMsg"].Value;
                    if (_AspEsignResp.Attributes["transId"] != null)
                        transId = _AspEsignResp.Attributes["transId"].Value;

                    if (status == 1)
                    {
                        ClsPkcs clspk = new ClsPkcs();
                        string hashcode = clspk.readCert(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);

                        string uidtoken = clsHash.readCertUID72Chars(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value);
                        clsHash.embedSignature(xmldoc.GetElementsByTagName("DocSignature")[0].FirstChild.Value, transId);

                        //btnDownload.Visible = true;

                        lblStatus.Text = "Document has successfully eSigned. Please download.";
                    }
                    else
                    {
                        lblStatus.Text = "Error while signing the document Error Code : " + errCode + " errMsg : " + errMsg;
                    }
                }
                else
                    lblStatus.Text = "Error while validating the xml signature.";
            }
            else if (Request.QueryString["filename"] != null)
            {
                _esigner.DownLoad();
            }
            else
            {
                lblStatus.Text = "Method not allowed.";
            }




            //Session["Status_Id"] = "44";
            int Flag = 1;
            string resp_status = status.ToString();
            if(resp_status=="1")
            {
                //string url = "Final_Order_Drafting.aspx?Case_Number="+casenum+"&Hearing="+hearingdate+"&Flag="+Flag+"&Response_Status="+resp_status+"&Response_type=Hearing_Ordersheet" + "&hearing_id="+hearing_id + "&Notice_Id=" + Notice_Id + "&Status_Id=" + "44";
                string strHearing = "Hearing_Ordersheet";
                string url = "Final_Order_Drafting.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + strHearing + "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('eSigned hearing ordersheet saved Successfully','success');window.location='" + url + "';", true);
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
            }
            else
            {

                //string url = "Final_Order_Drafting.aspx?Case_Number="+casenum+"&Hearing="+hearingdate+"&Flag="+Flag+"&Response_Status="+resp_status+"&Response_type=Hearing_Ordersheet" + "&hearing_id="+hearing_id + "&Notice_Id=" + Notice_Id + "&Status_Id=" + "44";
                string strHearing = "Hearing_Ordersheet_esignPending";
                string url = "Hearing.aspx?Flag=" + Flag + "&Response_Status=" + resp_status + "&Response_type=" + strHearing + "";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('eSigned failed, Please try again','failed');window.location='" + url + "';", true);
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());   
            }


        }
    }
}