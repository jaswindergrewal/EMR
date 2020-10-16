using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using System.Collections;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class Admin_LabReminders : LMCBase
{
    #region Variable
    IAdminLabRemindersService objService = null;
    #endregion
    #region Event

    /// <summary>
    /// binding sysmtoms and diagnosis details
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
	{
        try
        {
            if (!IsPostBack)
            {
                objService = new AdminLabRemindersService();
                ddlSymptom.DataSource = objService.getAllSymptoms();
                ddlSymptom.DataBind();

                ddlDiagnosis.DataSource = objService.getAllDiagnosis();
                ddlDiagnosis.DataBind();

                PopulateSymptom();
                PopulateDiag();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
	}

    /// <summary>
    /// performing grid designing
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void Grid_PreRender(object sender, EventArgs e)
	{
		Grid grd = (Grid)sender;
		if (grd.Rows != null && grd.Rows.Count > 1)
		{
			foreach (GridRow row in grd.Rows)
			{
				if (row.Cells[1].Text == "Yes")
				{

					row.BackColor = System.Drawing.Color.Beige;
					row.ForeColor = System.Drawing.Color.Black;
				}
				else
				{
					row.BackColor = System.Drawing.Color.Chartreuse;
					row.ForeColor = System.Drawing.Color.White;
				}
			}
		}
	}

  /// <summary>
  /// inserting diagnosis details
  /// </summary>
  /// <param name="sender"></param>
  /// <param name="e"></param>
	protected void btnRecordDiag_Click(object sender, EventArgs e)
	{
        try
        {
            if (grdDiagnosisLab.SelectedRecords == null) return;
            int[] selIds = new int[grdDiagnosisLab.SelectedRecords.Count];
            for (int i = 0; i < grdDiagnosisLab.SelectedRecords.Count; i++)
            {
                selIds[i] = int.Parse(((Hashtable)grdDiagnosisLab.SelectedRecords[i])["GroupID"].ToString());
            }
            objService = new AdminLabRemindersService();

            objService.DeleteDiagnosisLab(int.Parse(ddlDiagnosis.SelectedValue));

            foreach (GridRow row in grdDiagnosisLab.Rows)
            {
                int ProdID = int.Parse(row.Cells[3].Text.ToString());
                if (selIds.Contains(ProdID))
                {
                    DiagnosisLabViewModel sysup = new DiagnosisLabViewModel()
                    {
                        GroupID = ProdID,
                        DiagnosisID = int.Parse(ddlDiagnosis.SelectedValue),
                    };

                    objService.InsertDiagnosisLab(sysup);
                }
            }

            PopulateDiag();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
	}

    /// <summary>
    /// insert sysmtoms
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void btnRecord_Click(object sender, EventArgs e)
	{
        try
        {
            if (grdSymptomLab.SelectedRecords == null) return;
            int[] selIds = new int[grdSymptomLab.SelectedRecords.Count];
            for (int i = 0; i < grdSymptomLab.SelectedRecords.Count; i++)
            {
                selIds[i] = int.Parse(((Hashtable)grdSymptomLab.SelectedRecords[i])["GroupID"].ToString());
            }

            objService = new AdminLabRemindersService();
            objService.DeleteSymptomLabs(int.Parse(ddlSymptom.SelectedValue));
            foreach (GridRow row in grdSymptomLab.Rows)
            {
                int ProdID = int.Parse(row.Cells[3].Text.ToString());
                if (selIds.Contains(ProdID))
                {
                    SymptomLabViewModel sysup = new SymptomLabViewModel()
                    {
                        GroupID = ProdID,
                        SymptomID = int.Parse(ddlSymptom.SelectedValue),
                    };
                    objService.InsertSymptomLab(sysup);
                }
            }


            PopulateSymptom();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
	}

    /// <summary>
    /// populating diagnosis details on selecting from diagnosis dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void ddlDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
	{
		PopulateDiag();
	}
    /// <summary>
    /// populating symptoms details on selecting from symptoms dropdown
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void ddlSymptom_SelectedIndexChanged(object sender, EventArgs e)
	{
		PopulateSymptom();
	}
    /// <summary>
    /// rebinding diagnosis details the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void grdDiagnosisLab_Rebind(object sender, EventArgs e)
	{
		PopulateDiag();
	}
    /// <summary>
    /// rebinding symptoms details in the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
	protected void grdSymptomLab_Rebind(object sender, EventArgs e)
	{
		PopulateSymptom();
	}
    #endregion
    #region Methods
    /// <summary>
    /// populating the symptoms details on  symptoms dropdown select 
    /// </summary>
    private void PopulateSymptom()
	{
		int SymptomID = 0;
		try
		{
			SymptomID = int.Parse(ddlSymptom.SelectedValue);
		}
		catch(System.Exception ex)
		{
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
		}

   
        objService = new AdminLabRemindersService();
        grdSymptomLab.DataSource = objService.GetLabSymptoms(SymptomID);
		grdSymptomLab.DataBind();

	}
    /// <summary>
    /// populating the diagnosis details on  diagnosis dropdown select 
    /// </summary>
	private void PopulateDiag()
	{
		int DiagnosisID = 0;
		try
		{
			DiagnosisID = int.Parse(ddlDiagnosis.SelectedValue);
		}
		catch(System.Exception ex)
		{
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
		}

       
        objService = new AdminLabRemindersService();
        grdDiagnosisLab.DataSource = objService.GetLabDiagnosis(DiagnosisID);
		grdDiagnosisLab.DataBind();

    }
    #endregion
}