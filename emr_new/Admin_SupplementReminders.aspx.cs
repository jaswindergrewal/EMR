using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using System.Collections;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;

public partial class Admin_SupplementReminders : LMCBase
{
    #region "Variables"
    ISymptomService objISymptomService = null;
    #endregion

    #region "Events"
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindControls();
        }
    }

    protected void Grid_PreRender(object sender, EventArgs e)
    {
        try
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
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void btnRecordDiag_Click(object sender, EventArgs e)
    {
        try
        {
            objISymptomService = new SymptomService();

            if (grdDiagnosisSupplement.SelectedRecords == null) return;
            int[] selIds = new int[grdDiagnosisSupplement.SelectedRecords.Count];
            for (int i = 0; i < grdDiagnosisSupplement.SelectedRecords.Count; i++)
            {
                selIds[i] = int.Parse(((Hashtable)grdDiagnosisSupplement.SelectedRecords[i])["ProductID"].ToString());
            }
            objISymptomService.DeleteDiagnosisSupplements(int.Parse(ddlDiagnosis.SelectedValue));

            foreach (GridRow row in grdDiagnosisSupplement.Rows)
            {
                int ProdID = int.Parse(row.Cells[3].Text.ToString());
                if (selIds.Contains(ProdID))
                {
                    DiagnosisSupplementViewModel viewModelDiagnosisSupplement = new DiagnosisSupplementViewModel();
                    viewModelDiagnosisSupplement.SupplementID = ProdID;
                    viewModelDiagnosisSupplement.Diagnosis_ID = int.Parse(ddlDiagnosis.SelectedValue);

                    objISymptomService = new SymptomService();
                    objISymptomService.InsertDiagnosisSupplement(viewModelDiagnosisSupplement);
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

    protected void btnRecordLabs_Click(object sender, EventArgs e)
    {
        try
        {
            objISymptomService = new SymptomService();
            if (grdLabSupplement.SelectedRecords == null) return;
            int[] selIds = new int[grdLabSupplement.SelectedRecords.Count];
            for (int i = 0; i < grdLabSupplement.SelectedRecords.Count; i++)
            {
                selIds[i] = int.Parse(((Hashtable)grdLabSupplement.SelectedRecords[i])["ProductID"].ToString());
            }
            objISymptomService.DeleteGroupRangeSupplement(int.Parse(ddlRanges.SelectedValue));

            foreach (GridRow row in grdLabSupplement.Rows)
            {
                int ProdID = int.Parse(row.Cells[3].Text.ToString());
                if (selIds.Contains(ProdID))
                {

                    GroupRangeSupplementViewModel viewModelGroupRangeSupplement = new GroupRangeSupplementViewModel();
                    viewModelGroupRangeSupplement.SupplementID = ProdID;
                    viewModelGroupRangeSupplement.GroupRangeID = int.Parse(ddlRanges.SelectedValue);

                    objISymptomService = new SymptomService();
                    objISymptomService.InsertGroupRangeSupplement(viewModelGroupRangeSupplement);
                }
            }

            PopulateLabs();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    protected void btnRecord_Click(object sender, EventArgs e)
    {
        try
        {
            objISymptomService = new SymptomService();
            if (grdSymptomSupplement.SelectedRecords == null) return;
            int[] selIds = new int[grdSymptomSupplement.SelectedRecords.Count];
            for (int i = 0; i < grdSymptomSupplement.SelectedRecords.Count; i++)
            {
                selIds[i] = int.Parse(((Hashtable)grdSymptomSupplement.SelectedRecords[i])["ProductID"].ToString());
            }
            objISymptomService.DeleteSymptomSupplement(int.Parse(ddlSymptom.SelectedValue));

            foreach (GridRow row in grdSymptomSupplement.Rows)
            {
                int ProdID = int.Parse(row.Cells[3].Text.ToString());
                if (selIds.Contains(ProdID))
                {
                    SymptomSupplementViewModel viewModelSymptomSupplement = new SymptomSupplementViewModel();
                    viewModelSymptomSupplement.SupplementID = ProdID;
                    viewModelSymptomSupplement.SymptomID = int.Parse(ddlSymptom.SelectedValue);

                    objISymptomService = new SymptomService();
                    objISymptomService.InsertSymptomSupplement(viewModelSymptomSupplement);
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

    protected void SaveChanges(object sender, EventArgs e)
    {
        GroupRangeViewModel objGroupViewModel = null;

        try
        {
            objGroupViewModel = new GroupRangeViewModel();
            objISymptomService = new SymptomService();

            objGroupViewModel.GroupID = int.Parse(ddlGroups.SelectedValue);
            objGroupViewModel.HighRange = int.Parse(txtHigh.Text);
            objGroupViewModel.LowRange = int.Parse(txtLow.Text);

            objISymptomService.AddGroupRange(objGroupViewModel);
            BindRangeListBox();

            txtHigh.Text = string.Empty;
            txtLow.Text = string.Empty;
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objGroupViewModel = null;
            objISymptomService = null;
        }
    }

    protected void ddlRanges_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateLabs();
    }

    protected void ddlDiagnosis_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateDiag();
    }

    protected void ddlSymptom_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateSymptom();
    }

    protected void ddlGroups_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateLabs();
    }

    protected void grdDiagnosisSupplement_Rebind(object sender, EventArgs e)
    {
        PopulateDiag();
    }

    protected void grdSymptomSupplement_Rebind(object sender, EventArgs e)
    {
        PopulateSymptom();
    }

    protected void grdLabSupplement_Rebind(object sender, EventArgs e)
    {
        PopulateLabs();
    }
    #endregion

    #region "Methods"

    /// <summary>
    /// calls the methods to bind controls on page load
    /// </summary>
    private void BindControls()
    {
        PopulateSymptom();
        PopulateDiag();
        PopulateLabs();
        BindSymptomDropDown();
        BindRangeListBox();
    }

    /// <summary>
    /// this methods binds the grid with symtops and the basis of symptomID
    /// </summary>

    private void PopulateSymptom()
    {
        List<AutoshipProductsForSyymptomViewModel> lstObj = null;
        int SymptomID = 0;
        try
        {
            objISymptomService = new SymptomService();
            SymptomID = objISymptomService.GetSymptomId();

            objISymptomService = new SymptomService();
            lstObj = new List<AutoshipProductsForSyymptomViewModel>();
            lstObj = objISymptomService.PopulateSymptomList(SymptomID);

            grdSymptomSupplement.DataSource = lstObj;
            grdSymptomSupplement.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objISymptomService = null;
            lstObj = null;
        }
    }


    /// <summary>
    /// populates the grid with diagnosis list
    /// </summary>
    private void PopulateDiag()
    {
        int DiagnosisID = 0;
        List<AutoshipProductsForSyymptomViewModel> lstDiagnosis = null;
        try
        {
            objISymptomService = new SymptomService();
            DiagnosisID = objISymptomService.GetDiagnosisID();

            lstDiagnosis = new List<AutoshipProductsForSyymptomViewModel>();
            objISymptomService = new SymptomService();
            lstDiagnosis = objISymptomService.PopulateDiagList(DiagnosisID);

            grdDiagnosisSupplement.DataSource = lstDiagnosis;
            grdDiagnosisSupplement.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objISymptomService = null;
            lstDiagnosis = null;
        }

    }


    /// <summary>
    /// populating the lablist based on range selected
    /// </summary>
    private void PopulateLabs()
    {

        int RangeID = 0;
        List<AutoshipProductsForSyymptomViewModel> lstLabs = null;
        try
        {
            objISymptomService = new SymptomService();
            RangeID = objISymptomService.GetRangeID();

            ddlRanges.SelectedValue = RangeID.ToString();

            lstLabs = new List<AutoshipProductsForSyymptomViewModel>();
            objISymptomService = new SymptomService();
            lstLabs = objISymptomService.PopulateLabList(RangeID);

            grdLabSupplement.DataSource = lstLabs;
            grdLabSupplement.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objISymptomService = null;
            lstLabs = null;
        }
    }

    /// <summary>
    ///binds the dropdownlist with the symptoms 
    /// </summary>
    private void BindSymptomDropDown()
    {
        List<LabReportGroupViewModel> lstLabReportGroup = null;
        ILabReportService objILabReportService = null;
        try
        {
            objILabReportService = new LabReportService();
            lstLabReportGroup = new List<LabReportGroupViewModel>();

            lstLabReportGroup = objILabReportService.GetLabReportGroupDetails();
            ddlGroups.DataSource = lstLabReportGroup;
            ddlGroups.DataTextField = "GroupName";
            ddlGroups.DataValueField = "GroupID";
            ddlGroups.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// binding the listbox with the range
    /// </summary>
    private void BindRangeListBox()
    {
        try
        {
            ddlRanges.Items.Clear();
            objISymptomService = new SymptomService();
            var obj = objISymptomService.BindRangeListBox();

            ddlRanges.DataSource = obj;
            ddlRanges.DataTextField = "RangeName";
            ddlRanges.DataValueField = "GroupRangeID";
            ddlRanges.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false); ;
        }
        finally
        {
            objISymptomService = null;
        }
    }
    #endregion

}