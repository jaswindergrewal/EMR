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
/// jaswinder  7th aug 2013
/// </summary>
public partial class protocols_symptom_add : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region "Method"
    /// <summary>
    /// Insert data for symptoms and return 1 if data inserted
    /// jaswinder 7th aug 2013
    /// </summary>
    /// <param name="SymptomText"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertSymptoms(string SymptomText)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            intRet = objProtocolService.InsertSymptoms(SymptomText);

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


    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// jaswinder 7th aug 2013
    /// </summary>
    /// <param name="PageIndex"></param>
    [System.Web.Services.WebMethod]
    public static List<SymptomViewModel> BindSymptomList(int PageIndex)
    {
        IProtocolService objService = null;
        List<SymptomViewModel> lstSymptom = null;
        try
        {
            int PAGE_SIZE = 30;
            objService = new ProtocolService();
            lstSymptom = objService.BindSymptomList(PageIndex, PAGE_SIZE);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
        return lstSymptom;

    }

    #endregion
}