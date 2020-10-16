using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;

public partial class Autoship_Admin_CancelReasons : LMCBase
{
    #region "Variables"
    IAutoshipCancelReasonService objService = null;
    #endregion
    #region "Events"
    /// <summary>
    /// Method to get the list of auto ship cancel reasons 
    /// </summary>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }
    }
    /// <summary>
    /// In this event reasons records save in database
    /// </summary>
    protected void grdReasons_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Insert")
        {
            TextBox theName = (TextBox)grdReasons.FooterRow.FindControl("txtNewName");
            CheckBox isActive = (CheckBox)grdReasons.FooterRow.FindControl("cboActiveNew");
            objService = new AutoshipCancelReasonService();
            objService.InsertUpdateAutoShipCancelReason(theName.Text, isActive.Checked, 0);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('You have successfully added the record.') </script>");

        }
        if (e.CommandName == "Delete")
        {               
            GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
            int code = Convert.ToInt32(grdReasons.DataKeys[row.RowIndex].Values[0].ToString());
            objService = new AutoshipCancelReasonService();
            if (code > 0)
                objService.DeleteAutoshipCancelReasons(code);
        }
        BindData();



    }
    /// <summary>
    /// In this event gird view cancel event fire.
    /// </summary>
    protected void grdReasons_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        grdReasons.EditIndex = -1;
        BindData();
    }
    protected void grdReasons_RowEditing(object sender, GridViewEditEventArgs e)
    {
        grdReasons.EditIndex = e.NewEditIndex;
        BindData();
    }

    protected void grdReasons_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdReasons.PageIndex = e.NewPageIndex;
        BindData();
    }
    protected void grdReasons_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {

        string ReasonName = ((TextBox)grdReasons.Rows[e.RowIndex].FindControl("txtName")).Text;
        bool Active = ((CheckBox)grdReasons.Rows[e.RowIndex].FindControl("cboActive")).Checked;
        objService = new AutoshipCancelReasonService();
        objService.InsertUpdateAutoShipCancelReason(ReasonName, Active, (int)e.Keys[0]);
        grdReasons.EditIndex = -1;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('You have successfully updated the record.') </script>");
        BindData();
    }
    #endregion

    #region "Methods"
    /// <summary>
    /// Method is used for rebind the grid view when any event fire
    /// </summary>
    private void BindData()
    {
        try
        {
            objService = new AutoshipCancelReasonService();
            grdReasons.DataSource = objService.GetAutoshipCancelReasonList();
            grdReasons.DataBind();
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

    /// <summary>
    /// this method is using for check the duplicate Reason during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 07.Aug.2013        
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Name"></param>    
    /// <param name="tableName"></param>  
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

    protected void grdReasons_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}