using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class Admin_EndMedical : System.Web.UI.Page//LMCBase
{    
    IEndMedicalService objServiceEndMedical = null;
    IPatientService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        
        if (!Page.IsPostBack)
        {
            try
            {
                objService = new PatientService();

                ddlClinic.DataSource = objService.GetClinics();
                ddlClinic.DataTextField = "ClinicName";
                ddlClinic.DataValueField = "ClinicID";
                ddlClinic.DataBind();
                ddlClinic.Items.Insert(0, new ListItem("All"));
                BindEndMedical();
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

    #region "Methods"
    /// <summary>
    /// method for bind the End Medical details
    /// </summary>
    private void BindEndMedical()
    {
        List<EndMedicalViewModel> lstEndMedical = null;
        try
        {
            lstEndMedical = new List<EndMedicalViewModel>();
            objServiceEndMedical = new EndMedicalService();
            lstEndMedical = objServiceEndMedical.GetEndMedicalDetails(ddlClinic.SelectedItem.Text.ToString());

            grdEndMedical.DataSource = lstEndMedical;
            grdEndMedical.DataBind();

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstEndMedical = null;
            objServiceEndMedical = null;
        }
    }
    #endregion

    #region "Events"
    protected void grdEndMedical_DataBound(object sender, GridViewRowEventArgs e)
    {
        for (int i = 0; i < e.Row.Cells.Count; i++)
        {
            e.Row.Cells[i].Attributes.Add("style", "white-space: nowrap;");
        }
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }

    protected void grdEndMedical_RowCommand(object sender, GridViewCommandEventArgs e)
    {
    }

    protected void btnEnd_Click(object sender, EventArgs e)
    {
        IAppointmentConsole _objAptConsoleService = null;
        try
        {
            _objAptConsoleService = new AppointmentConsole();
            objServiceEndMedical = new EndMedicalService();
            PatientViewModel lstPatinet = _objAptConsoleService.GetPatientList((int)Session["PatientID"]);
            lstPatinet.EndMedical = DateTime.Parse(txtDate.Text);
            objServiceEndMedical.InsertEndMedical(lstPatinet);
            BindEndMedical();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
            objServiceEndMedical = null;
        }

    }

    protected void grdEndMedical_SelectedIndexChanging(Object sender, GridViewSelectEventArgs e)
    {
        Session["PatientID"] = int.Parse(grdEndMedical.Rows[e.NewSelectedIndex].Cells[1].Text);
        txtDate.Text = DateTime.Now.ToShortDateString();
        modGetDate.Show();
    }

    protected void grdEndMedical_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdEndMedical.PageIndex = e.NewPageIndex;
        BindEndMedical();
    }

    protected void ddlClinic_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindEndMedical();
    }
    #endregion
}

