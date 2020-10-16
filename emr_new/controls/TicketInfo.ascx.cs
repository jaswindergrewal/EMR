using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class controls_TicketInfo : System.Web.UI.UserControl
{
    
    TicketPatientViewModel Pat = new TicketPatientViewModel();
    ITicketManageService objService = null;

    /// <summary>
    /// Get the patient ticket details by active ticket id 
    /// Jaswinder 19 sept 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Page_Load(object sender, EventArgs e)
	{
        objService = new TicketManageService(); 
        Pat = objService.GetAllTicketManageList(Convert.ToInt32(Session["ActiveTicket"].ToString()));
        if (Pat != null)
        {
            lblAssign.Text = Pat.Assigned;
            DateTime? DateEntered = Pat.DateEntered;
            if (DateEntered != null)
            {
                lblDate.Text = ((DateTime)Pat.DateEntered).ToShortDateString();
            }
            lblEnteredBy.Text = Pat.EnteredBy;
            lblSeverity.Text = Pat.Severity == 1 ? "High" : Pat.Severity == 2 ? "Normal" : "Low";
            lblSubject.Text = Pat.FollowUp_Subject;
            lblCategory.Text = Pat.FollowUp_Type_Desc;
        }
        
	}
}