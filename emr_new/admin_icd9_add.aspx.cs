using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

/// <summary>
/// Conversion of asp to aspx
/// jaswinder 9 aug 2013
/// </summary>
public partial class admin_icd9_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "Method"
    /// <summary>
    /// Insert data for Diagnosis and return 1 if data inserted
    /// jaswinder 9th aug 2013
    /// </summary>
    /// <param name="SymptomText"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertDiagnosis(string txtICDCode, string txtDiagnosis, bool Viewable)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            intRet = objProtocolService.InsertDiagnosis(txtICDCode, txtDiagnosis, Viewable);

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