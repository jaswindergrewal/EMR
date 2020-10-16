using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class admin_pending_prescriptions : LMCBase
{
    #region "Variable"
    IPrescriptionService objServicePrescription = null;
    List<PendingPrescriptionViewModel> lstPendingPres = null;
    List<PendingSupplementViewModel> lstPendingSupp = null;
    #endregion
    #region "Event"
    /// <summary>
    /// binding grid for Pending Prescription and Supplement request
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        bindpendingPrescription();
        bindpendingSuppliments();


    }

    protected void rptPendingScrips_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptPendingScrips.PageIndex = e.NewPageIndex;
        bindpendingPrescription();
    }
    /// <summary>
    /// Get panding suppliment list and bind it with repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptPendingSupps_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        rptPendingSupps.PageIndex = e.NewPageIndex;
        bindpendingSuppliments();
    }
    #endregion

    #region "Method"

    //bind grid for Pending Prescription Requests
    private void bindpendingPrescription()
    {
        try
        {

            objServicePrescription = new PrescriptionService();
            lstPendingPres = new List<PendingPrescriptionViewModel>();
            lstPendingPres = objServicePrescription.GetPendingPrescriptionList();
            rptPendingScrips.DataSource = lstPendingPres;
            rptPendingScrips.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServicePrescription = null;
            lstPendingPres = null;
        }

    }

    //bind grid for Pending Prescription Requests
    private void bindpendingSuppliments()
    {
        try
        {
            objServicePrescription = new PrescriptionService();
            lstPendingSupp = new List<PendingSupplementViewModel>();
            lstPendingSupp = objServicePrescription.GetPendingSupplementList();
            rptPendingSupps.DataSource = lstPendingSupp;
            rptPendingSupps.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServicePrescription = null;
            lstPendingSupp = null;
        }
    }

    #endregion
}