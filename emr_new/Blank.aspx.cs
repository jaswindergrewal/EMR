using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Blank : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
		Response.Redirect("PatientInfo.aspx?PatientID=" + Request.QueryString["PatientID"]);
    }
}