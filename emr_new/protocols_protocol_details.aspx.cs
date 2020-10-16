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
/// jaswinder 6th aug 2013
/// </summary>

public partial class protocols_protocol_details : System.Web.UI.Page
{
    # region"Variables"
    IProtocolService objService = null;
    public ProtocolViewModel lstProtocol = null;
    public string ProtocolId;
    IAdminLabRemindersService objSymptService = null;
    #endregion

    #region "events"
    /// <summary>
    /// Bind the dropdown and protocol details
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["protocol_id"] != null)
            {
                objService = new ProtocolService();
                ProtocolId = Request.QueryString["protocol_id"];
                lstProtocol = objService.GetProtocolByID(int.Parse(ProtocolId));

                objSymptService = new AdminLabRemindersService();
                drpSymptomlist.DataSource = objSymptService.getAllSymptoms();
                drpSymptomlist.DataBind();

                drpDiagnosisList.DataSource = objSymptService.getAllDiagnosis();
                drpDiagnosisList.DataBind();

            }
        }

    }

    #endregion

    #region "methods"
    /// <summary>
    /// Get data for protocol symptoms
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<SymptomViewModel> BindDataForProtocolSymptoms(string ProtocolId)
    {
        IProtocolService objProtocolService = null;
        List<SymptomViewModel> lstData = null;
        try
        {
            objProtocolService = new ProtocolService();
            lstData = objProtocolService.GetProtocolSymptoms(int.Parse(ProtocolId));
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objProtocolService = null;
        }
        return lstData;
    }


    /// <summary>
    /// Get data for protocol diagnosis
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static List<DiagnosistblViewModel> BindDataForProtocolDiagnosis(string ProtocolId)
    {
        IProtocolService objProtocolService = null;
        List<DiagnosistblViewModel> lstData = null;
        try
        {
            objProtocolService = new ProtocolService();
            lstData = objProtocolService.GetProtocolDiagnosis(int.Parse(ProtocolId));
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objProtocolService = null;
        }
        return lstData;
    }

    /// <summary>
    /// delete diagnosis from a particular protocol id
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <param name="DiagnosisId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int DeleteProtocolDiagnosis(string ProtocolId, string DiagnosisId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            objProtocolService.DeleteProtocolDiagnosis(int.Parse(ProtocolId), int.Parse(DiagnosisId));
            intRet = 1;
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
    /// delete symptoms for protocols
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <param name="SymptomId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int DeleteProtocolSymptoms(string ProtocolId, string SymptomId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            objProtocolService.DeleteProtocolSymptoms(int.Parse(ProtocolId), int.Parse(SymptomId));
            intRet = 1;
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
    /// insert protocol symptoms
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <param name="SymptomId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertProtocolSymptoms(string ProtocolId, string SymptomId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            intRet = objProtocolService.InsertProtocolSymptoms(int.Parse(ProtocolId), int.Parse(SymptomId));

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
    /// insert protocol diagnosis
    /// jaswinder 6th aug 2013
    /// </summary>
    /// <param name="ProtocolId"></param>
    /// <param name="DiagnosisId"></param>
    /// <returns></returns>
    [System.Web.Services.WebMethod]
    public static int InsertProtocolDiagnosis(string ProtocolId, string DiagnosisId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            intRet = objProtocolService.InsertProtocolDiagnosis(int.Parse(ProtocolId), int.Parse(DiagnosisId));

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

    [System.Web.Services.WebMethod]
    public static int DeleteProtocol(string ProtocolId)
    {
        IProtocolService objProtocolService = null;
        int intRet = 0;

        try
        {
            objProtocolService = new ProtocolService();
            objProtocolService.DeleteProtocol(int.Parse(ProtocolId));
            intRet = 1;
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