using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class admin_LabRequest : LMCBase
{
    #region Variable
    ILabLogService objService = new LabLogService();
    #endregion

    #region Event
    /// <summary>
    /// binding panel management grid and test management grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new LabLogService();

                grdPanel.DataSource = objService.GetLabPanelList();
                grdPanel.DataBind();

                grdTest.DataSource = objService.GetLabTestList();
                grdTest.DataBind();
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
    /// <summary>
    /// insert new record or update existing record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateInsert(object sender, GridRecordEventArgs e)
    {

        try
        {
            string gridName = ((Obout.Grid.Grid)sender).ID;
            objService = new LabLogService();
            int RequestID = 0;
            switch (gridName)
            {
                case "grdPanel":

                    if (e.Record["LabRequest_PanelID"].ToString() != "")
                    {
                        RequestID = int.Parse(e.Record["LabRequest_PanelID"].ToString());
                    }

                    objService.InsertUpdateLabRequests(gridName, RequestID, bool.Parse(e.Record["Active_YN"].ToString()), e.Record["PanelName"].ToString());

                    break;
                case "grdTest":


                    if (e.Record["LabRequest_TestID"].ToString() != "")
                    {
                        RequestID = int.Parse(e.Record["LabRequest_TestID"].ToString());

                    }
                    objService.InsertUpdateLabRequests(gridName, RequestID, bool.Parse(e.Record["Active_YN"].ToString()), e.Record["TestName"].ToString());

                    break;
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
    /// <summary>
    /// rebinding panel management grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdPanel_Rebind(object sender, EventArgs e)
    {
        try
        {
            objService = new LabLogService();
            grdPanel.DataSource = objService.GetLabPanelList();
            grdPanel.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    /// <summary>
    /// rebinding test management grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTest_Rebind(object sender, EventArgs e)
    {
        try
        {
            objService = new LabLogService();
            grdTest.DataSource = objService.GetLabTestList();
            grdTest.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    #endregion

    #region "Web Methods"
    /// <summary>
    /// this method is using for check the duplicate event's details during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 05.Aug.2013    
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicatePanel(string panelID, string panelName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        ILabLogService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(panelID) || panelID == "undefined")
                panelID = "0";
            _objService = new LabLogService();
            isExist = _objService.CheckDuplicatePanel(Convert.ToInt32(panelID), panelName);
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
    /// this method is using for check the duplicate test during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 06.Aug.2013        
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateTest(string testID, string testName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        ILabLogService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(testID) || testID == "undefined")
                testID = "0";
            _objService = new LabLogService();
            isExist = _objService.CheckDuplicateTest(Convert.ToInt32(testID), testName);
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
    /// method for delete the Panel Management records
    /// created by: Deepak Thakur[13.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeletePanelMgmt(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new LabLogService();
            if (!string.IsNullOrEmpty(e.Record["LabRequest_PanelID"].ToString()))
                objService.DeleteLabRequestPanels(int.Parse(e.Record["LabRequest_PanelID"].ToString()));
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
    /// method for delete the Test Management records
    /// created by: Deepak Thakur[13.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteTestMgmt(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new LabLogService();
            if (!string.IsNullOrEmpty(e.Record["LabRequest_TestID"].ToString()))
                objService.DeleteLabRequestTests(int.Parse(e.Record["LabRequest_TestID"].ToString()));
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