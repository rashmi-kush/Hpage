using SCMS_BAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;


namespace CMS_Sampada.CoS
{
    public partial class COS_Cases_Pending_ToSend_FinalOrder : System.Web.UI.Page
    {
        Generate_FinalOrderList_BAL Cos_FinalList = new Generate_FinalOrderList_BAL();
        int APPLICATION_NUMBER;
        int APP_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCaseList();
           
            }
        }

        void BindCaseList()
        {

            DataTable dt = null;

            try
            {
                dt = Cos_FinalList.GET_Pending_Cases_SendTo_FinalOrderList(Convert.ToInt32(Session["DRID"].ToString()));

                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        grdCaseList.DataSource = dt;
                        grdCaseList.DataBind();


                    }
                    else
                    {
                        grdCaseList.DataSource = null;
                        grdCaseList.DataBind();
                    }
                }
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
        }

       
    }
}