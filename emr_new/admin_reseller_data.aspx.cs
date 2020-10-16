using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;

public partial class admin_reseller_data : LMCBase
{
    #region Variable
    IAdminResellerService objService;
    #endregion

    #region Events
    /// <summary>
    /// binging all the grids on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Bind grid for reseller event
                EventManagement();

                //Bind grid for Reseller Status
                BindStatusManagement();

                //Bind grid for MArketing Source
                MarketingSource();
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
            }
           
        }
    }
    /// <summary>
    /// get the reseller status and rebind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdStatus_Rebind(object sender, EventArgs e)
    {
        BindStatusManagement();
    }

    private void BindStatusManagement()
    {
        try
        {
            objService = new AdminResellerService();
            grdStatus.DataSource = objService.GetResellerStatus();
            grdStatus.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    ///  Insert/update reseller staus grid data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdStatus_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["Status"].ToString() != "")
            {
                objService = new AdminResellerService();

                int statusID = 0;
                if (e.Record["StatusID"].ToString() != "")
                {
                    statusID = int.Parse(e.Record["StatusID"].ToString());

                }

                objService.InsertUpdateStatus(statusID, bool.Parse(e.Record["Active_YN"].ToString()), e.Record["Status"].ToString());
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }
    /// <summary>
    /// rebind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEvent_Rebind(object sender, EventArgs e)
    {
        EventManagement();
    }

    private void EventManagement()
    {
        try
        {
            objService = new AdminResellerService();
            grdEvent.DataSource = objService.GetResellerEvent();
            grdEvent.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    /// Insert/update Reseller Event grid data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEvent_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["EventName"].ToString() != "")
            {
                int EventId = 0;
                if (e.Record["EventID"].ToString() != "")
                {
                    EventId = int.Parse(e.Record["EventID"].ToString());

                }
                objService = new AdminResellerService();
                objService.InsertUpdateEvent(EventId, bool.Parse(e.Record["Active_YN"].ToString()), e.Record["EventName"].ToString());
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }

    }
    /// <summary>
    /// rebind grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSource_Rebind(object sender, EventArgs e)
    {
        MarketingSource();
    }

    private void MarketingSource()
    {
        objService = new AdminResellerService();
        grdSource.DataSource = objService.GetMarketingSource();
        grdSource.DataBind();
    }

    /// <summary>
    /// Insert/update Reseller source data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdSource_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["SourceName"].ToString() != "")
            {
                int ResellerMarketingSourceID = 0;
                if (e.Record["ResellerMarketingSourceID"].ToString() != "")
                {
                    ResellerMarketingSourceID = int.Parse(e.Record["ResellerMarketingSourceID"].ToString());

                }
                objService = new AdminResellerService();
                objService.InsertUpdateSource(ResellerMarketingSourceID, bool.Parse(e.Record["Active_YN"].ToString()), e.Record["SourceName"].ToString());
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion

    #region "Web Methods"
    /// <summary>
    /// this method is using for check the duplicate status name during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 05.Aug.2013    
    /// </summary>
    /// <param name="diagnosisID"></param>
    /// <param name="diagName"></param>
    /// <param name="iCDCode"></param>
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateResellerStatus(string resellerStatusId, string statusName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IAdminResellerService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(resellerStatusId))
                resellerStatusId = "0";
            _objService = new AdminResellerService();
            isExist = _objService.CheckDuplicateResellerStatus(Convert.ToInt32(resellerStatusId), HttpContext.Current.Server.HtmlDecode(statusName));
            if (isExist == true)
                strDuplicate = "duplicate";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }

    /// <summary>
    /// this method is using for check the duplicate event's details during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 05.Aug.2013    
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateEvent(string eventID, string eventName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IAdminResellerService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(eventID))
                eventID = "0";
            _objService = new AdminResellerService();
            isExist = _objService.CheckDuplicateEvent(Convert.ToInt32(eventID), HttpContext.Current.Server.HtmlDecode(eventName));
            if (isExist == true)
                strDuplicate = "duplicate";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }

    /// <summary>
    /// this method is using for check the duplicate marketing source details during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 05.Aug.2013    
    /// </summary>
    /// <param name="resellerMarketingSourceID"></param>
    /// <param name="sourceName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateSource(string resellerMarketingSourceID, string sourceName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IAdminResellerService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(resellerMarketingSourceID))
                resellerMarketingSourceID = "0";
            _objService = new AdminResellerService();
            isExist = _objService.CheckDuplicateSource(Convert.ToInt32(resellerMarketingSourceID), HttpContext.Current.Server.HtmlDecode(sourceName));
            if (isExist == true)
                strDuplicate = "duplicate";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }
    #endregion

    #region "Delete Methods"
    /// <summary>
    /// method for delete the Status Management records
    /// created by: Deepak Thakur[14.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteStatusManagement(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new AdminResellerService();
            if (!string.IsNullOrEmpty(e.Record["StatusID"].ToString()))
                objService.DeleteStatusManagement(int.Parse(e.Record["StatusID"].ToString()));
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

    /// <summary>
    /// method for delete the Status Management records
    /// created by: Deepak Thakur[14.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteEventManagement(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new AdminResellerService();
            if (!string.IsNullOrEmpty(e.Record["EventID"].ToString()))
                objService.DeleteEventManagement(int.Parse(e.Record["EventID"].ToString()));
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

    /// <summary>
    /// method for delete the Marketing Source Management records
    /// created by: Deepak Thakur[14.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteMarketingSourceManagement(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new AdminResellerService();
            if (!string.IsNullOrEmpty(e.Record["ResellerMarketingSourceID"].ToString()))
                objService.DeleteMarketingSourceManagement(int.Parse(e.Record["ResellerMarketingSourceID"].ToString()));
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
    #endregion
}