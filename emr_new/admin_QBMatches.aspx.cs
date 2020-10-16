using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.Grid;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class admin_QBMatches : LMCBase
{
    #region "Variable"
    IQBCustMatchPatientService objServiceQBCustMatchPatient = null;
    #endregion
    #region "Events"
    #region "binding quickbooks to dropdownlist and grid on page load event "
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindQuickBookDropDown();
            BindGridQuickBookList();
        }
    }
    #endregion
    /// <summary>
    /// Select a name from the drop down whose watch you wish to break.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnBreakLink_CLick(object sender, EventArgs e)
    {
        try
        {
            objServiceQBCustMatchPatient = new QBCustMatchPatientService();
            objServiceQBCustMatchPatient.DeleteMatch(ddlQBCustomers.SelectedValue);
            BindQuickBookDropDown();
            BindGridQuickBookList();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServiceQBCustMatchPatient = null;
        }
    }
    #region "code for insert and update data in QB_Match table"
    protected void grdQBMatch_UpdateCommand(object sender, GridRecordEventArgs e)
    {
        QB_MatchViewModel objViewModelQB = null;
        try
        {
            objServiceQBCustMatchPatient = new QBCustMatchPatientService();
            if (e.Record["QBCust"].ToString() != "None" && e.Record["QBCust"].ToString() != "")
            {
                //// code for insert the data in QB_Match table
                objViewModelQB = new QB_MatchViewModel();
                objViewModelQB.PatientID = int.Parse(e.Record["PatientID"].ToString());
                objViewModelQB.QBid = e.Record["QBCust"].ToString();
                objViewModelQB.Note = "";
                objServiceQBCustMatchPatient.InsertQBMatch(objViewModelQB);
            }
            else
            {
                //// code for update the data in Patient table    
                PatientViewModel viewModelPatient = objServiceQBCustMatchPatient.GetPatientDetailById(int.Parse(e.Record["PatientID"].ToString()));
                viewModelPatient.Notes = (string)e.Record["Notes"];
                objServiceQBCustMatchPatient = new QBCustMatchPatientService();
                objServiceQBCustMatchPatient.UpdateQBMatch(viewModelPatient);
            }
            BindGridQuickBookList();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objViewModelQB = null;
            objServiceQBCustMatchPatient = null;
        }
    }
    #endregion
    /// <summary>
    /// binding the obout grid on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdQBMatch_DataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            for (int i = 0; i < e.Row.Cells.Count; i++)
            {
                e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    protected void UpdateRecord(object sender, GridRecordEventArgs e) { }
    protected void grdQBMatch_ColumnsCreated(object sender, EventArgs e)
    {
        try
        {
            Grid grid = sender as Grid;

            foreach (Column column in grid.Columns)
            {
                if (column.ID != "QBCust")
                {
                    column.TemplateSettings.TemplateId = "Template1";
                    column.TemplateSettings.HeaderTemplateId = "Template1";
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    #endregion
    #region "Methods"
    #region "Binding quick books to dropdownlist"
    /// <summary>
    /// method for bind the quick book dropdownlist.
    /// </summary>
    private void BindQuickBookDropDown()
    {
        List<QBCustMatchPatientViewModel> objQBCustViewModel = null;
        try
        {
            objServiceQBCustMatchPatient = new QBCustMatchPatientService();
            objQBCustViewModel = new List<QBCustMatchPatientViewModel>();
            objQBCustViewModel = objServiceQBCustMatchPatient.GetQBCustomerList();

            ddlQBCustomers.DataSource = objQBCustViewModel;
            ddlQBCustomers.DataTextField = "FullName";
            ddlQBCustomers.DataValueField = "ListID";
            ddlQBCustomers.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServiceQBCustMatchPatient = null;
            objQBCustViewModel = null;
        }
    }
    #endregion
    #region "Binding quick books to grid"
    /// <summary>
    /// Method to bind quick books to grid control
    /// </summary>
    private void BindGridQuickBookList()
    {
        List<PatientQuickBookViewModel> lstQuickBook = null;
        try
        {
            lstQuickBook = new List<PatientQuickBookViewModel>();
            objServiceQBCustMatchPatient = new QBCustMatchPatientService();
            lstQuickBook = objServiceQBCustMatchPatient.GetPatientQuickBookList();
            grdQBMatch.DataSource = lstQuickBook;
            grdQBMatch.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstQuickBook = null;
            objServiceQBCustMatchPatient = null;
        }
    }
    #endregion
    #endregion
}
