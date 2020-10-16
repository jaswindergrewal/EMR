using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

public partial class SOA : System.Web.UI.Page
{
    private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    protected int PatientID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        PatientID = int.Parse(Request.QueryString["PatientID"]);
        Patient pat = (from p in ctx.Patients
                       where p.PatientID == PatientID
                       select p).First();
        
        TextBox1.Text = pat.LastName;
    }

}