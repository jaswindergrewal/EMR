using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using System.Collections;
using System.Xml.Linq;
using System.Web.Services;
using Emrdev.ServiceLayer;


public partial class External_Panels : LMCBase
{
    #region Global

    string FileLocation = @"\\emr\c$\inetpub\DoctorPortal\XML\";
    Emrdev.ServiceLayer.PanelService objService;

    #endregion


    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            try
            {
                BindAllPanel();
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
            }

    }

    #endregion


    #region Bind All External Panel

    void BindAllPanel()
    {
        objService = new PanelService();
        grdPanel.DataSource = objService.SelectAllPanel();
        grdPanel.DataBind();
    }


    #endregion


    #region Grid Events

    #region Delete Selected Panel

    protected void DeleteRecord(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new PanelService();
            int panelId=int.Parse(e.Record["ExternalPanelsID"].ToString());
            objService.DeletePanel(panelId);
            BindAllPanel();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);         
        }
    }

    #endregion


    #region Update Panel

    protected void UpdateRecord(object sender, GridRecordEventArgs e)
    {
        try
        {
            int panelId=int.Parse(e.Record["ExternalPanelsID"].ToString());
            string panelNewName=e.Record["PanelName"].ToString().Trim();
            objService = new PanelService();
            objService.UpdatePanel(panelId, panelNewName);
            BindAllPanel();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    #endregion


    #region Insert New Panel

    protected void InsertRecord(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new PanelService();
            string panelName=e.Record["PanelName"].ToString().Trim();
            objService.InsertPanel(panelName);
            BindAllPanel();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    #endregion


    #region Rebind Panel Grid

    protected void RebindGrid(object sender, EventArgs e)
    {
        BindAllPanel();
    }

    #endregion

    #endregion


    #region WebMethod

    /// <summary>
    /// this method is using for check the duplicate test during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 06.Aug.2013        
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateRecords(string ID, string Name, string tableName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        ILabScehduleService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(ID) || ID == "undefined")
                ID = "0";
            _objService = new LabScehduleService();
            isExist = _objService.CheckDuplicateRecords(Convert.ToInt32(ID), HttpContext.Current.Server.HtmlDecode(Name), tableName);
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
}