using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class lab_launch_short : System.Web.UI.Page
{

    #region Global

    protected int PatientId=0;
    Emrdev.ServiceLayer.LabLaunchService objService;
    Emrdev.ViewModelLayer.LabLaunchViewModel objModel;

    #endregion

    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["patientid"] != null)
            {
                PatientId = int.Parse(Request.QueryString["patientid"]);
                ShowPatientDetail();
            }
        }

    }

    #endregion

    #region Patient Detail

    void ShowPatientDetail()
    {
        try
        {
            //Show Patient Detail By Patient ID
            objService = new Emrdev.ServiceLayer.LabLaunchService();
            objModel = objService.GetByPatientId(PatientId);
            LabelPatientName.Text = objModel.FirstName + " " + objModel.LastName;
            RepeaterPatientInformation.DataSource = objModel.JoinCollection;
            RepeaterPatientInformation.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    #endregion
}