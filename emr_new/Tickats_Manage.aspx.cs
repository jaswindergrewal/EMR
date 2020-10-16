using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.Grid;
using Emrdev.ServiceLayer;
using System.Data;
using System.Web.Services;

public partial class ManageTickets : LMCBase
{
    #region "Variables"
    ITicketManageService objService = null;

    #endregion
    #region "Events"
    /// <summary>
    /// Show all the Manage tickets in the grid view on  page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                //Fill grid with all drugs that with reviewed=false 
                objService = new TicketManageService();

                grdTicketManage.DataSource = objService.GetAllTicketManageList();
                grdTicketManage.DataBind();
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
    #endregion
    #region "Events"
    /// <summary>
    /// Update the record by click on edit button of gridview
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateRecord(object sender, GridRecordEventArgs e)
    {
        try
        {
            if (e.Record["ProcessName"].ToString() == "")
            {
                lblError.Text = "Process Name can't be blank";
            }
            else
            {
                if (e.Record["ProcessID"] != null && e.Record["ProcessID"] != "")
                {
                    objService = new TicketManageService();
                    objService.UpdateTicketManageList(int.Parse(e.Record["ProcessID"].ToString()),
                                                    e.Record["ProcessName"].ToString(), e.Record["Interval"].ToString(),
                                                    e.Record["Enabled"].ToString(), e.Record["Note"].ToString()
                                                    );



                    grdTicketManage.DataSource = objService.GetAllTicketManageList();
                    grdTicketManage.DataBind();
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objService = null;
        }
    }
    #endregion

    /// <summary>
    /// this method is using for check the duplicate process name during Add/Edit the record.    
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
            isExist = _objService.CheckDuplicateRecords(Convert.ToInt32(ID), Name, tableName);
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
}