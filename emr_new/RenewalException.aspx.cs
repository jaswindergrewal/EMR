using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class RenewalException : LMCBase
{
    #region Variable
    PatientViewModel pat;
    IRenewalExceptionService objService = null;
    #endregion

    #region Events

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (!CheckAccess())
                {
                    mainTable.Rows.Clear();
                    TableRow theRow = new TableRow();
                    TableCell theCell = new TableCell();
                    theCell.CssClass = "PageTitle";
                    theCell.Text = "You are not authorized to view this page.";
                    theRow.Cells.Add(theCell);
                    mainTable.Rows.Add(theRow);
                }
                else
                {
                    objService = new RenewalExceptionService();
                    pat = objService.GEtPatientByID(int.Parse(Request.QueryString["PatientID"]));
                    txtException.Text = pat.RenewalException;

                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
            }
            finally
            {
                objService = null;
            }
        }
    }
    /// <summary>
    /// updates the patients renewal exceptions
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void btnSave_Click(object sender, EventArgs e)
	{
        try
        {
            objService = new RenewalExceptionService();
            pat = objService.GEtPatientByID(int.Parse(Request.QueryString["PatientID"]));
            if (txtException.Text != "")
            {
                pat.RenewalException = txtException.Text;
            }
            else
            {
                pat.RenewalException = "";
            }
            objService.UpdatePatientRenewalException(pat);

            Response.Redirect("Manage.aspx?PatientID=" + Request.QueryString["PatientID"], "_top", "");
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
	}
    #endregion

    #region Method
    /// <summary>
    /// get the access permission for staff
    /// </summary>
    /// <returns></returns>
    private bool CheckAccess()
	{
        try
        {
            objService = new RenewalExceptionService();
            StaffViewModel checker = new StaffViewModel();
            checker = objService.GetStaffByStaffID((int)Session["StaffID"]);

            if (checker.access_level == "emr_admin" || checker.access_level == "super") return true; else return false;
        }
        catch (System.Exception ex)
        {
            return false;
        }


    }
    #endregion
}