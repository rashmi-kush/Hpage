using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;

namespace CMS_Sampada.CoS
{
    public partial class COSLegacyDataFormCreate : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            List<string[]> stringList = null;

            DataTable dt = new DataTable();
            DataRow dr = null;
            dt.Columns.Add("ProductID", System.Type.GetType("System.Double"));
            dt.Columns.Add("serialnumber", System.Type.GetType("System.String"));
            dt.Columns.Add("Documentnumber", System.Type.GetType("System.String"));
            dt.Columns.Add("RegisteredDocument", System.Type.GetType("System.String"));
            dt.Columns.Add("InstrumentName", System.Type.GetType("System.String"));
            dt.Columns.Add("DeedName", System.Type.GetType("System.String"));
            dt.Columns.Add("Impoundedby", System.Type.GetType("System.String"));
            dt.Columns.Add("ReasonforImpound", System.Type.GetType("System.String"));

            dr = dt.NewRow();
            dr["ProductID"] = 1;
            dr["serialnumber"] = "1";
            dr["Documentnumber"] = "12345678";
            dr["RegisteredDocument"] = "Electronic";
            dr["InstrumentName"] = "";
            dr["DeedName"] = "";
            dr["Impoundedby"] = "";
            dr["ReasonforImpound"] = "Stamp Duty Defeciency";
            dt.Rows.Add(dr);

            // 2

            dr = dt.NewRow();
            // dr["ProductID"] = 2;
            // dr["ladliname"] = "539854209";
            // dr["fathersamgraid"] = "67429810";
            // dr["MotherSamgraIs"] = "45239876";
            // dr["checkboxselection"] = "JHY";
            // dt.Rows.Add(dr);


            dt.AcceptChanges();
            Product.DataSource = dt;
            Product.DataBind();

        }
    }
}