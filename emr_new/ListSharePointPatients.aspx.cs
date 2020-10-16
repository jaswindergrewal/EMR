using Emrdev.ServiceLayer;
using System;
using System.Web.Services;

public partial class ListSharePointPatients : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void grdSharePointPatients_Rebind(object sender, EventArgs e)
    {
        BindGrid();
    }

    protected void btnAddSharePointPatients_Click(object sender, EventArgs e)
    {
        Response.Redirect("SharePointPatientsAddEdit.aspx?Id=0");
    }

    public void BindGrid()
    {

        IAcuitySchedulingService objSchService = new AcuitySchedulingService();

        grdSharePointPatients.DataSource = objSchService.ListSharePointPatients();

        grdSharePointPatients.DataBind();

    }

    [WebMethod]
    public static string DeleteSharePointPatient(string PatientId)
    {
        string result = string.Empty;
        IAcuitySchedulingService objSchService = null;
        try
        {
            
            objSchService = new AcuitySchedulingService();

            objSchService.DeleteSharePointPatient(Convert.ToInt32(PatientId));
            result = "Deleted";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objSchService = null;
        }
        return result;

    }
}