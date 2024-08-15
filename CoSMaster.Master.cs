using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class CoSMaster : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //lblCoSName.Text = "District Guna Approver";
            //lblCoSNamePro.Text = "District Guna Approver";
            if (Session["Token"] != null)
            {
                //if (Session["SubRolrCode"] != null)
                //{
                //    if ((Session["SubRolrCode"].ToString() == "UAT_DRO_ROLE_APPROVER"))
                //    {
                //        lblCoSName.Text = Session["Name"].ToString();
                //        lblCoSNamePro.Text = Session["Name"].ToString();
                //    }
                //    else
                //    {
                //        //Response.Redirect("https://ersuat2.mp.gov.in/igrs/#/"); //UAT

                //        Response.Redirect("https://sampada.mpigr.gov.in"); //PROD
                //    }
                //}


                if (Session["FeatureCode"] != null)
                {
                    if ((Session["FeatureCode"].ToString() == "F-0185"))
                    {
                        lblCoSName.Text = Session["Name"].ToString();
                        lblCoSNamePro.Text = Session["Name"].ToString();
                    }
                    else
                    {
                        //Response.Redirect("https://ersuat2.mp.gov.in/igrs/#/"); //UAT

                        Response.Redirect("https://sampada.mpigr.gov.in"); //PROD
                    }
                }

            }
            else
            {
                //Response.Redirect("https://ersuat2.mp.gov.in/igrs/#/"); //UAT

                Response.Redirect("https://sampada.mpigr.gov.in"); //PROD
            }
        }

        protected void lnkbtnLogout_Click(object sender, EventArgs e)
        {
            Session["Token"] = null;
            Session["FeatureCode"] = null;
            Session.Abandon();
            Session.Clear();

            //Response.Redirect("https://ersuat2.mp.gov.in/igrs/#/login");  //UAT

            Response.Redirect("https://sampada.mpigr.gov.in");  //PROD

        }
    }
}