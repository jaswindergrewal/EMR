using System;
using System.Collections.Generic;
using System.Collections;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class admin_ImportTests : LMCBase
{

    IAdminLabImport objServiceAdminLabImport = null;

    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    /// <summary>
    /// values to be inserted for lab report test on button click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnImport_Click(object sender, EventArgs e)
    {
        AdminLabReportsTestViewModel newLabReportTest = null;
        try
        {
            if (grdTests.SelectedRecords != null)
            {
                LabelMessage.Visible = false;
                foreach (Hashtable rec in grdTests.SelectedRecords)
                {
                    newLabReportTest = new AdminLabReportsTestViewModel();
                    newLabReportTest.GroupID = 0;
                    newLabReportTest.Hidden = false;
                    newLabReportTest.LastUsed = DateTime.Parse(rec["LastUsed"].ToString());
                    newLabReportTest.TestName = rec["CleanName"].ToString();
                    objServiceAdminLabImport = new AdminLabImport();
                    objServiceAdminLabImport.InsertLabReportTest(newLabReportTest);
                }
            }
            else
            {
                LabelMessage.Visible = true;
                LabelMessage.Text = "Please select atleast one item to import.";
            }

            Populate();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            newLabReportTest = null;
        }
    }
    #endregion

    #region "Method"
    /// <summary>
    /// method to get admin import test list
    /// </summary>
    private void Populate()
    {
        List<AdminImportViewModel> lstWithDupes = null;
        try
        {
            DateTime startDate;
            DateTime endDate;
            if (DateTime.TryParse(txtStartDate.Text, out startDate))
            {
                endDate = startDate.AddMonths(6);
                objServiceAdminLabImport = new AdminLabImport();
                lstWithDupes = objServiceAdminLabImport.GetAdminImportTestList(startDate, endDate);

                grdTests.DataSource = lstWithDupes;
                grdTests.DataBind();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {

            lstWithDupes = null;
        }
    }
    #endregion

    protected void btnShowRecord_Click(object sender, EventArgs e)
    {
        Populate();
    }
}
