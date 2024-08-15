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
    public partial class FinalOrder_Preview : System.Web.UI.Page
    {
        CoSFinalOrder_BAL clsFinalOrderBAL = new CoSFinalOrder_BAL();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["App_Id"] != null)
            {
                int App_Id = Convert.ToInt32(Request.QueryString["App_Id"].ToString());
                ViewState["App_Number"] = Request.QueryString["App_Number"].ToString();
                string Application_No = ViewState["App_Number"].ToString();
                
                


                DataSet DSPartyDisplay = clsFinalOrderBAL.GetFinalOrder_Doc(App_Id, Application_No);
                if (DSPartyDisplay.Tables[0].Rows.Count > 0)
                {

                    string fileName = DSPartyDisplay.Tables[0].Rows[0]["FINAL_ORDER_PATH"].ToString();
                    Session["FINAL_ORDER_PATH"] = fileName;

                    NoticePath.Src = fileName;

                    


                }

            }
        }
    }
}