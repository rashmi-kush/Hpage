using eSigner;
using SCMS_BAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web.UI;
using System.Xml;
using System.Xml.Linq;

namespace CMS_Sampada.CoS
{
    public partial class ResponseFromeSign : System.Web.UI.Page
    {
        CoSOrderSheet_BAL OrderSheet_BAL = new CoSOrderSheet_BAL();
        eSigner.eSigner _esigner = new eSigner.eSigner();
        protected void Page_Load(object sender, EventArgs e)
        {
            //string Stringpath = Session["Case"].ToString();
           

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
        private void getdata()
        {

            string casenum = "";
            string appid = "";
            string AppNo = "";
            string hearingdate = "";
            string Party_ID = "";
            string Notice_Id = "";
            string order_id = "";

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
           
            if (Request.QueryString["Order_id"]!=null)
            {
                order_id = Request.QueryString["Order_id"].ToString();
            }
             
            
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

                        lblStatus.Text = "Document has successfully eSigned.";
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

            //DataTable dt = OrderSheet_BAL.InserteSignDSC_Status(Convert.ToInt32(appid), "1", "", GetLocalIPAddress(), Convert.ToInt32(order_id));

            //string Stringpath = Session["Case"] as string;
            //string url = "Notice.aspx?Case_Number=" + Stringpath;
           
            if (status==1)
            {
                int Flag = 1;
                string resp_status = status.ToString();
                //string url = "Notice.aspx?Case_Number="+casenum+"&App_Id="+appid+"&AppNo="+AppNo+"&Flag="+Flag+"&Response_Status="+resp_status;
                string url = "Notice.aspx?Flag=" + Flag + "&Response_Status=" + resp_status+ "&Response_From=" + "Ordersheet";

                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('eSigned ordersheet saved Successfully','success');window.location='" + url + "';", true);
                //Response.Redirect("Notice.aspx?Case_Number=" + ViewState["Case_Number"] + "&App_Id=" + Session["AppID"].ToString() + "&AppNo=" + Session["Appno"].ToString());
            }
            else
            {
                int Flag = 0;
                string resp_status = status.ToString();
                //string url = "Notice.aspx?Case_Number="+casenum+"&App_Id="+appid+"&AppNo="+AppNo+"&Flag="+Flag+"&Response_Status="+resp_status;
                string url = "Ordersheet.aspx?Flag=" + Flag + "&Response_Status=" + resp_status+ "&Response_From=" + "Ordersheet";
                ScriptManager.RegisterStartupScript(this, this.GetType(), "alert", "swal('"+lblStatus.Text+ "','info');window.location='" + url + "';", true);

            }

        }
    }
}