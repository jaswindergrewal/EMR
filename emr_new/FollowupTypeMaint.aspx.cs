using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class FollowupTypeMaint : LMCBase
{
    #region "Variables"
    IDepartmentService objDepartmentService = null;
    IFollowUpTypeService objFollowUpTypeService = null;
    #endregion

    #region "Events"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindData();
        }

    }
    /// <summary>
    /// inserting new row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTypes_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        try
        {
            if (e.CommandName == "Insert")
            {
                apt_FollowUp_type theType = new apt_FollowUp_type();

                CheckBox cboViewableNew = (CheckBox)grdTypes.FooterRow.FindControl("cboViewableNew");
                CheckBox cboConsultNew = (CheckBox)grdTypes.FooterRow.FindControl("cboConsultNew");
                CheckBox cboFollowUpNew = (CheckBox)grdTypes.FooterRow.FindControl("cboFollowUpNew");
                CheckBox cboTicketNew = (CheckBox)grdTypes.FooterRow.FindControl("cboTicketNew");
                CheckBox cboTicketStaffNew = (CheckBox)grdTypes.FooterRow.FindControl("cboTicketStaffNew");
                CheckBox cboAppointmentNew = (CheckBox)grdTypes.FooterRow.FindControl("cboAppointmentNew");
                TextBox txtDescripNew = (TextBox)grdTypes.FooterRow.FindControl("txtDescripNew");
                DropDownList ddlNewDept = (DropDownList)grdTypes.FooterRow.FindControl("ddlNewDept");

                objFollowUpTypeService = new FollowUpTypeService();
                objFollowUpTypeService.InsertAptFollowUpType(txtDescripNew.Text, cboConsultNew.Checked, cboFollowUpNew.Checked, cboTicketNew.Checked,
                    cboAppointmentNew.Checked, cboViewableNew.Checked, cboTicketStaffNew.Checked, int.Parse(ddlNewDept.SelectedValue));
                              
            }

            // code for delete the records from database table
            // created by: Deepak Thakur
            // created date: 13.August.2013
            if (e.CommandName == "Delete")
            {
                GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                int dataKeyValue = Convert.ToInt32(grdTypes.DataKeys[row.RowIndex].Values[0].ToString());
                objFollowUpTypeService = new FollowUpTypeService();
                if (dataKeyValue > 0)
                    objFollowUpTypeService.DeleteAptFollowUptypes(dataKeyValue);
               
            }
            // code for update the records from database table
            // created by: surabhi purohit
            // created date: 25 Dec 2013
            if (e.CommandName == "Update")
            {               
                    GridViewRow row = (GridViewRow)((Button)e.CommandSource).NamingContainer;
                    int dataKeyValue = Convert.ToInt32(grdTypes.DataKeys[row.RowIndex].Values[0].ToString());

                    string FollowUp_Type_Desc = ((TextBox)grdTypes.Rows[row.RowIndex].FindControl("txtDescrip")).Text;
                    bool Viewable_YN = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboViewable")).Checked;
                    bool ConsultType_YN = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboConsult")).Checked;
                    bool FollowUpType_YN = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboFollowUp")).Checked;
                    bool TicketType_YN = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboTicket")).Checked;
                    bool StaffTicketType_YN = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboTicketStaff")).Checked;
                    bool Appointment = ((CheckBox)grdTypes.Rows[row.RowIndex].FindControl("cboAppointment")).Checked;
                    int DepartmentID = Convert.ToInt32(((DropDownList)grdTypes.Rows[row.RowIndex].FindControl("ddlEditDept")).SelectedValue);                    
                    objFollowUpTypeService = new FollowUpTypeService();
                    objFollowUpTypeService.UpdateAptFollowUpType(FollowUp_Type_Desc, Viewable_YN, ConsultType_YN, FollowUpType_YN, TicketType_YN, StaffTicketType_YN, Appointment, DepartmentID, dataKeyValue);                                                  
            }
            BindData();  
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        
    }
    /// <summary>
    /// event on canceling the edit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTypes_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        try
        {
            grdTypes.EditIndex = -1;
            BindData();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    /// <summary>
    /// event on cliking on edit link
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTypes_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            grdTypes.EditIndex = e.NewEditIndex;
            BindData();
            BindDepartmentDropDownListForEdit();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    /// <summary>
    /// updating the row content
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTypes_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            grdTypes.EditIndex = -1;
            BindData();

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// Show data in a grid on pageindex changed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdTypes.PageIndex = e.NewPageIndex;
        BindData();
    }

    #endregion

    #region "Methods"


    /// <summary>
    /// Method to get the followup type list 
    /// </summary>
    private void BindData()
    {
        List<FollowupTypesViewModel> objLst = null;
        try
        {
            objLst = new List<FollowupTypesViewModel>();
            objFollowUpTypeService = new FollowUpTypeService();
            objLst = objFollowUpTypeService.GetFollowupTypeList();

            grdTypes.DataSource = objLst;
            grdTypes.DataBind();

            BindDepartmentDropDownListForFotter();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objLst = null;
            objFollowUpTypeService = null;
        }

    }

    /// <summary>
    /// methos to bind dripdownlist with departments
    /// </summary>

    private void BindDepartmentDropDownListForFotter()
    {
        List<DepartmentViewModel> lstDepartment = null;
        try
        {
            objDepartmentService = new DepartmentService();
            lstDepartment = new List<DepartmentViewModel>();
            lstDepartment = objDepartmentService.GetDepartments();

            GridViewRow row = grdTypes.FooterRow;
            DropDownList drpAddDepartment = ((DropDownList)row.FindControl("ddlNewDept"));

            drpAddDepartment.DataSource = lstDepartment;
            drpAddDepartment.DataTextField = "DepartmentName";
            drpAddDepartment.DataValueField = "DepartmentID";
            drpAddDepartment.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objDepartmentService = null;
            lstDepartment = null;
        }
    }

    /// <summary>
    /// binding dropdownlist with the departments
    /// </summary>

    private void BindDepartmentDropDownListForEdit()
    {
        List<DepartmentViewModel> lstDepartment = null;
        try
        {
            objDepartmentService = new DepartmentService();
            lstDepartment = new List<DepartmentViewModel>();
            lstDepartment = objDepartmentService.GetDepartments();


            foreach (GridViewRow row in grdTypes.Rows)
            {
                //Finding Dropdown control        
                Label lblDept = null;
                lblDept = row.FindControl("lblDept") as Label;
                DropDownList drpEditDepartment = null;
                drpEditDepartment = row.FindControl("ddlEditDept") as DropDownList;
                if (drpEditDepartment != null)
                {
                    drpEditDepartment.DataSource = lstDepartment;
                    drpEditDepartment.DataTextField = "DepartmentName";
                    drpEditDepartment.DataValueField = "DepartmentID";
                    drpEditDepartment.DataBind();
                    drpEditDepartment.SelectedItem.Text = lblDept.Text;
                }
            }
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objDepartmentService = null;
            lstDepartment = null;
        }
    }
    #endregion

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
    protected void grdTypes_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {

    }
}
