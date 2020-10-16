using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
public partial class admin_pending_pre_fil : LMCBase
{
    #region "Variables"
    protected PendingPrescriptionAproveViewModel PendingPrescriptions = new PendingPrescriptionAproveViewModel();
    PendingPrescriptionAproveService objService = null;
    #endregion

    #region "Events"
    /// <summary>
    /// getting the precription details by staff
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        if (!IsPostBack)
        {
            try
            {
                objService = new PendingPrescriptionAproveService();
                if (Request.QueryString["pre_id"] != null)
                {
                    PendingPrescriptions = objService.GetPescriptionDetail((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]));
                    if (PendingPrescriptions != null)
                    {
                        Sig.Text = PendingPrescriptions.Drug_Dose;
                        Dispenses.Text = PendingPrescriptions.Drug_Dispenses;
                        NumbRefills.Text = PendingPrescriptions.Drug_NumbRefills;
                        DateEntered.Text = DateTime.Now.ToShortDateString();
                        PreNotes.Text = PendingPrescriptions.Notes;
                    }
                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }
            finally
            {
                objService = null;
            }
        }
	}
    /// <summary>
    /// update prescription
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void btnApprove_Click(object sender, EventArgs e)
	{
        try
        {
            objService = new PendingPrescriptionAproveService();
            int PatientID = objService.UpdatePrescription((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]), Sig.Text, Dispenses.Text, NumbRefills.Text, DateTime.Parse(DateEntered.Text), PreNotes.Text);
            Response.Redirect("admin_pending_pre_fil.aspx?pre_id=" + Request.QueryString["pre_id"],false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
	}

    protected void btnPrint_Click(object sender, EventArgs e)
    {

        try
        {
            objService = new PendingPrescriptionAproveService();
            int PatientID = objService.UpdatePrescription((int)Session["StaffID"], int.Parse(Request.QueryString["pre_id"]), Sig.Text, Dispenses.Text, NumbRefills.Text, DateTime.Parse(DateEntered.Text), PreNotes.Text);

            Calendar.Patient pat = new Calendar.Patient((int)PatientID);
            string Clinic = "";
            switch (pat.Clinic)
            {
                case "Kirkland":
                    Clinic = pat.ClinicID;
                    break;
                case "Seattle":
                    Clinic = pat.ClinicID;
                    break;
                case "South":
                    Clinic = pat.ClinicID;
                    break;
                case "Lynnwood":
                    Clinic = pat.ClinicID;
                    break;
            }

            Response.Redirect("PrescriptionPharm.aspx?PatientID=" + PatientID.ToString() + "&scripIds=" + Request.QueryString["pre_id"] + "&Clinic=" + Clinic,false);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion

}
