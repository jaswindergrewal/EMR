using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


/// <summary>
/// Conversion of asp to aspx
/// jaswinder 9th aug 2013
/// </summary>
public partial class admin_icd9_update : System.Web.UI.Page
{
    #region "Variables"
    IProtocolService objService = null;
    public int Diagnosisid = 0;

    #endregion

    #region "Events"
    /// <summary>
    /// Get the values for the diagnois on page load
    /// jaswinder 9 aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if ((Request.QueryString["diagnosis_id"]) != null)
        {

            if (!IsPostBack)
            {
                try
                {
                    objService = new ProtocolService();
                    Diagnosisid = int.Parse(Request.QueryString["diagnosis_id"]);
                    hdnDiagnosisID.Value = (Request.QueryString["diagnosis_id"]);
                    DiagnosistblViewModel DiagnosisDetail = objService.GetDiagnosisByID(Diagnosisid);
                    txtDiagnosis.Text = DiagnosisDetail.Diag_Title;
                    txtICDCode.Text = DiagnosisDetail.ICD9_Code;

                    if (DiagnosisDetail.Viewable_YN == true)
                    {
                        viewable_yn.Checked = true;
                    }
                    else
                    {
                        viewable_yn.Checked = false;
                    }
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
        else
        {
            Response.Redirect("~/admin_icd9_list.aspx");
        }
    }


    #endregion

    #region "Methods
    /// <summary>
    /// Insert data for Diagnosis and return 1 if data inserted
    /// jaswinder 9th aug 2013
    /// </summary>
    /// <param name="SymptomText"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int UpdateDiagnosis(string txtICDCode, string txtDiagnosis, bool Viewable, string DiagnosisId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            intRet = objProtocolService.UpdateDiagnosis(txtICDCode, txtDiagnosis, Viewable, int.Parse(DiagnosisId));

        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objProtocolService = null;
        }
        return intRet;
    }

    #endregion
}