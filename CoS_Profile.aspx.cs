using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace CMS_Sampada.CoS
{
    public partial class CoS_Profile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Session["USER_ID"] != null)
            {
                getUserDetail(Session["USER_ID"].ToString());
            }
            else
            {
                getUserDetail("3643");
            }
        }

        private void getUserDetail(string USER_ID)
        {
            ClsRRCApplication objClsNewApplication = new ClsRRCApplication();
            DataTable dt = objClsNewApplication.GetUserDetail(Convert.ToInt32(USER_ID));
            if (dt.Rows.Count > 0)
            {
                lblDesignation.Text = dt.Rows[0]["DESIGNATION"].ToString();
                lblDesignation1.Text = dt.Rows[0]["DESIGNATION"].ToString();
                lblEmail.Text = dt.Rows[0]["EMAIL"].ToString();
                lblFullName.Text = dt.Rows[0]["NAME"].ToString();
                lblName.Text = dt.Rows[0]["NAME"].ToString();
                lblMobile.Text = dt.Rows[0]["MOBILE_NO"].ToString();
                lblAssignRole.Text = dt.Rows[0]["DESIGNATION"].ToString();
                lblDemography.Text = dt.Rows[0]["DEMOGRAPHY_NAME_EN"].ToString();
                lblOfficeAddress.Text = dt.Rows[0]["OFFICE_ADDRESS"].ToString();
                lblOfficeCode.Text = dt.Rows[0]["OFFICE_CODE"].ToString();
                lblOfficeName.Text = dt.Rows[0]["OFFICE_NAME"].ToString();
                lblOfficePhone.Text = dt.Rows[0]["OFFICE_CONTACT_NUMBER"].ToString();
                lblOfficeLevel.Text = dt.Rows[0]["HSM_CER_LABEL"].ToString();
            }
        }
    }
}