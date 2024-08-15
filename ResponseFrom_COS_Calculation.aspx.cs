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
    public partial class ResponseFrom_COS_Calculation : System.Web.UI.Page
    {

        ClsNewApplication objClsNewApplication = new ClsNewApplication();
        protected void Page_Load(object sender, EventArgs e)
        {

            if (Request.QueryString["EREG_ID"] != null)
            {
                GetTheUrelDeatil(Request.QueryString["EREG_ID"].ToString());
            }
            else
            {
                GetTheUrelDeatil("14345");
            }
        }

        private void GetTheUrelDeatil(string EREG_ID)
        {
            DataTable dt = objClsNewApplication.GetCOS_Calculation(EREG_ID);

            if (dt.Rows.Count > 0)
            {
                //Response.Redirect("~/CoS/Final_Order_Drafting.aspx?Case_Number="+ 
                //    dt.Rows[0]["CASE_NUMBER"].ToString() + "&Hearing=" +
                //    dt.Rows[0]["HEARING"].ToString() + "&Flag=1&Response_Status=1&Response_type=COS_Calculation_Valuation&hearing_id="+
                //    dt.Rows[0]["HEARING_ID"].ToString() + "&Notice_Id="+
                //    dt.Rows[0]["NOTICE_ID"].ToString() + "&Status_Id="+
                //    dt.Rows[0]["STATUS_ID"].ToString() + "");
                Session["Case_Number"] = dt.Rows[0]["CASE_NUMBER"].ToString();
                Session["HearingDate"] = dt.Rows[0]["HEARING"].ToString();
                Session["Status_Id"] = dt.Rows[0]["STATUS_ID"].ToString();
                Session["hearing_id"] = dt.Rows[0]["HEARING_ID"].ToString();
                Session["Notice_ID"] = dt.Rows[0]["Notice_ID"].ToString();
                Session["AppID"] = dt.Rows[0]["App_Id"].ToString();

                Response.Redirect("~/CoS/Final_Order_Drafting.aspx?"
                   + "&Flag=4&Response_type=COS_Calculation_Valuation");

                //Response.Redirect("~/CoS/Final_Order_Drafting.aspx?Case_Number=" +
                //   dt.Rows[0]["CASE_NUMBER"].ToString() + "&Hearing=" +
                //   dt.Rows[0]["HEARING"].ToString() + "&App_Id=" +
                //   dt.Rows[0]["App_Id"].ToString()
                //   + "&AppNo=" + dt.Rows[0]["App_Id"].ToString()
                //    + "&Notice_ID=" +
                //   dt.Rows[0]["Notice_ID"].ToString()
                //   + "&Flag=4&Response_type=COS_Calculation_Valuation&Hearing_ID=" +
                //   dt.Rows[0]["HEARING_ID"].ToString() + "&Status_Id=" +
                //   dt.Rows[0]["STATUS_ID"].ToString() + "");
            }


            //Response.Redirect("~/CoS/Final_Order_DraftingPPP.aspx?Case_Number=" +
            //       "000008/B104/19/2023-2024/BHOP/1600" + "&Hearing=" +
            //       "2/1/2024%2012:00:00" + "&App_Id=" +
            //       "2"
            //       + "&AppNo=" +
            //       "2"
            //        + "&Notice_ID=" +
            //       "285"
            //       + "&Flag=4&Response_type=COS_Calculation_Valuation&Hearing_ID=" +
            //       "245" + "&Status_Id=" +
            //       "44" + "");
        }
    }
}