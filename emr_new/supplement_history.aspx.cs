using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class supplement_history : System.Web.UI.Page
{
    public string PatientID;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["patientID"] != null)
        {
            PatientID = Request.QueryString["patientID"].ToString();
        }

    }
      #region "Methods"

    /// <summary>
    /// fetch supplement history by patient Id
    /// Added by surabhi 8 oct 2013
    /// </summary>
    /// <param name="PatientID"></param>      
    [System.Web.Services.WebMethod]
    public static List<prescriptionHistoryViewModel> BindData(int PatientID)
   {
        IPrescriptionService objService = null;
        List<prescriptionHistoryViewModel> SupplementView = null;
        try
        {
            objService = new PrescriptionService();
            SupplementView = objService.GetSupplementHistory(PatientID);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return SupplementView;

    }
      #endregion
}