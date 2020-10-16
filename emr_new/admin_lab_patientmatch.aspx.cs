using System;
using System.Collections.Generic;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;
public partial class admin_lab_patientmatch : System.Web.UI.Page
{
    #region Global Variables/Objects
    ILabPatientsService objLabPatientsService = null; public int totalPatient = 0;
    #endregion

    #region Page_Load
    /// <summary>
    /// this is page load event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindUnmatchedLabsPatientData();
        }
    }
    #endregion

    #region BindUnmatchedLabsPatientData
    /// <summary>
    /// this function used for bind Unmatched Labs Patient Data.
    /// </summary>
    /// Created By : Rakesh Kumar
    /// Created Date : 4-Sep-2013
    private void BindUnmatchedLabsPatientData()
    {
        try
        {
            objLabPatientsService = new LabPatientsService();
            List<LabPatientsViewModel> objLabPatientsViewModel = objLabPatientsService.GetUnmatchedLabsPatientData();
            grdtUnmatchedLabsPatientData.DataSource = objLabPatientsViewModel;
            grdtUnmatchedLabsPatientData.DataBind();
            totalPatient = objLabPatientsViewModel.Count;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objLabPatientsService = null;
        }
    }
    #endregion

    #region grdtUnmatchedLabsPatientData_RowDataBound
    /// <summary>
    /// this event chech year of dateofbirth and set content accirding the condition.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdtUnmatchedLabsPatientData_RowDataBound(object sender, Obout.Grid.GridRowEventArgs e)
    {
        if (e.Row.RowType == GridRowType.DataRow)
        {
            string a = e.Row.Cells[grdtUnmatchedLabsPatientData.Columns["DateOfBirth"].Index].Text;
            int year = Convert.ToDateTime(e.Row.Cells[grdtUnmatchedLabsPatientData.Columns["DateOfBirth"].Index].Text).Year;
            if (year == 9999)
            {
                e.Row.Cells[grdtUnmatchedLabsPatientData.Columns["DateOfBirth"].Index].Text = "[No Birthdate Entered in Report]";
            }
            else
            {
                e.Row.Cells[grdtUnmatchedLabsPatientData.Columns["DateOfBirth"].Index].Text = Convert.ToDateTime(e.Row.Cells[grdtUnmatchedLabsPatientData.Columns["DateOfBirth"].Index].Text).ToShortDateString();
            }
        }
    }
    #endregion
}