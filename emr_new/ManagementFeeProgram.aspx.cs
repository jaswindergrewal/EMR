using System;
using Emrdev.ServiceLayer;
using Obout.Grid;

public partial class ManagementFeeProgram : System.Web.UI.Page
{
    IRenewalPackagesService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objService = new RenewalPackagesService();
            ManageProgramGrid.DataSource = objService.GetManagementPrograms();
            ManageProgramGrid.DataBind();
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

    protected void ManageProgramGrid_UpdateCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        Emrdev.ViewModelLayer.ManagementProgramViewModel objProgram = new Emrdev.ViewModelLayer.ManagementProgramViewModel();

        objProgram.IsActive = bool.Parse((string)e.Record["IsActive"]);
        objProgram.ProgramName = (string)e.Record["ProgramName"];
        objProgram.Id = int.Parse(e.Record["Id"].ToString()); 
        objService = new RenewalPackagesService();
        objService.SaveManagementProgram(objProgram);
        objService = new RenewalPackagesService();
        ManageProgramGrid.DataSource = objService.GetManagementPrograms();
        ManageProgramGrid.DataBind();
    }
    protected void ManageProgramGrid_InsertCommand(object sender, Obout.Grid.GridRecordEventArgs e)
    {
        Emrdev.ViewModelLayer.ManagementProgramViewModel objProgram = new Emrdev.ViewModelLayer.ManagementProgramViewModel();

        objProgram.IsActive = bool.Parse((string)e.Record["IsActive"]);
        objProgram.ProgramName = (string)e.Record["ProgramName"];
        objService = new RenewalPackagesService();
        objService.SaveManagementProgram(objProgram);
        objService = new RenewalPackagesService();
        ManageProgramGrid.DataSource = objService.GetManagementPrograms();
        ManageProgramGrid.DataBind();
    }
}