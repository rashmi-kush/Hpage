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
    public partial class COS_Generate_FinalOrderList : System.Web.UI.Page
    {
        Generate_FinalOrderList_BAL Cos_FinalList = new Generate_FinalOrderList_BAL();
        int APPLICATION_NUMBER;
        int APP_ID;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                BindCaseList();
                RRC_Forward_Case();
            }
        }

        void BindCaseList()
        {

            DataTable dt = null;

            try
            {
                dt = Cos_FinalList.GET_FinalOrderList(Convert.ToInt32(Session["DRID"].ToString()));

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

        void RRC_Forward_Case()
        {
            DataTable dt = null;
            try
            {
                dt = Cos_FinalList.Get_Pending_FinalOrderList_forRRC_CaseGen();
                if (dt != null)
                {
                    if (dt.Rows.Count > 0)
                    {
                        foreach (DataRow row in dt.Rows)
                        {

                            int APP_ID = Convert.ToInt32(row["APP_ID"]);
                            string APPLICATION_NUMBER = row["APPLICATION_NUMBER"].ToString();
                            //DateTime current_date = DateTime.Now.Date;
                            string finalorderdate = row["FINAL_ORDER_DATE"].ToString();

                            //string[] dateComponent = finalorderdate.Split('/');
                            //DateTime final_order_date = Convert.ToDateTime(finalorderdate).AddDays(30);

                            //if (final_order_date.Date == current_date)
                            //{
                                try
                                {
                                    DataSet Result = new DataSet();
                                    Result = Cos_FinalList.Case_MoveTo_RRC(APP_ID, "", Convert.ToInt32(Session["DistrictID"].ToString()), Session["District_NameEN"].ToString(), Convert.ToInt32(Session["SROid"].ToString()),
                                        APPLICATION_NUMBER, Session["DRID"].ToString(), "");

                                }
                                catch (Exception ex)
                                {
                                    Response.Write("Error: " + ex.Message);
                                }
                            //}
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
}