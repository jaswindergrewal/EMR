using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class prescription_history : System.Web.UI.Page
{
    IPrescriptionService objService = null;
    public string PatientID;
    protected void Page_Load(object sender, EventArgs e)
    {
      
            if (Request.QueryString["patientID"] != null)
            {
                PatientID = Request.QueryString["patientID"].ToString();
            }
       
       
        
    }

    #region "Methods"



  
    [System.Web.Services.WebMethod]
    public static List<prescriptionHistoryViewModel> BindData(int  PatientID)
    {
       
        IPrescriptionService objService = null;
        List<prescriptionHistoryViewModel> PrescriptionView = null;

        try
        {
            
          
            objService = new PrescriptionService();
            PrescriptionView = objService.GetPrescriptionHistory(PatientID);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return PrescriptionView;
    }

    #endregion

   
}