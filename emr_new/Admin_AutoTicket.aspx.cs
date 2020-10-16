using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using Obout.ComboBox;
using System.Data;
using Emrdev.ServiceLayer;
using System.Collections;
using System.Web.Services;

public partial class Admin_AutoTicket : LMCBase
{
    #region Variables

    IAdminAutoTicketService objService = null;
    #endregion

    #region Events
    /// <summary>
    /// binding list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                objService = new AdminAutoTicketService();
                DropDownList ddlDept = grdATicket.Templates[5].Container.FindControl("ddlDept") as DropDownList;
                if (ddlDept != null)
                {
                    //fill status dropdown with Departments data
                    ddlDept.DataSource = objService.GetAutoshipDepartments();
                    ddlDept.DataBind();
                }

                DropDownList ddlFtype = grdATicket.Templates[7].Container.FindControl("ddlFtype") as DropDownList;
                if (ddlFtype != null)
                {
                    //fill status dropdown with Followup type data
                    ddlFtype.DataSource = objService.GetFollowupTypes();
                    ddlFtype.DataBind();
                }

                INewTicketNoPatientService objStaffService = new NewTicketNoPatientService();
                IEnumerable objStaff = objStaffService.GetDepartmentStaff();
                DropDownList ddlCreatedBy = grdATicket.Templates[11].Container.FindControl("ddlCreatedBy") as DropDownList;
                if (ddlCreatedBy != null)
                {
                    //fill status dropdown with Staff data

                    ddlCreatedBy.DataSource = objStaff;
                    ddlCreatedBy.DataBind();
                }
                DropDownList ddlAssigned = grdATicket.Templates[9].Container.FindControl("ddlAssigned") as DropDownList;

                if (ddlAssigned != null)
                {
                    //fill status dropdown with Staff data

                    ddlAssigned.DataSource = objStaff;
                    ddlAssigned.DataBind();
                }


                grdATicket.DataSource = objService.GetAutoticketList();
                grdATicket.DataBind();


            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }


    /// <summary>
    /// insert auto tickets
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdATicket_Insert(object sender, GridRecordEventArgs e)
    {
        try
        {

            int Assigned = 0;
            int dept = 0;
            int freq = 0;
            if (((string)e.Record["Assigned"]) != "")
            {
                if (int.Parse((string)e.Record["Assigned"]) > 0)
                {
                    Assigned = int.Parse((string)e.Record["Assigned"]);
                }
            }

            if (((string)e.Record["DeptAssign"]) != "")
            {
                if (int.Parse((string)e.Record["DeptAssign"]) > 0)
                {
                    dept = int.Parse((string)e.Record["DeptAssign"]);
                }
            }
            DateTime tester = DateTime.Parse((string)e.Record["StartDate"]);
            freq = int.Parse((string)e.Record["Frequency"]);
          
            /* Set Model properties and pass to BLL to create new ticket */
            Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel = new Emrdev.ViewModelLayer.AdminAutoTicketViewModel();
            AutoTicket tick = new AutoTicket();
            objModel.Assigned = Assigned;
            objModel.AutoticketName = (string)e.Record["AutoticketName"];
            objModel.Body = (string)e.Record["Body"];
            objModel.CreatedID = int.Parse((string)e.Record["CreatedID"]);
            objModel.DeptAssign = dept;
            objModel.FollowUp_Type_ID = int.Parse((string)e.Record["FollowUp_Type_ID"]);
            objModel.Frequency = int.Parse((string)e.Record["Frequency"]);
            objModel.FrequencyType = (string)e.Record["FrequencyType"];
            objModel.LastSent = (DateTime?)null;
            objModel.StartDate = DateTime.Parse((string)e.Record["StartDate"]);
            objModel.Subject = (string)e.Record["Subject"];
            objService = new Emrdev.ServiceLayer.AdminAutoTicketService();
            objService.SaveNewTicket(objModel);
            /* End Here */
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }
    /// <summary>
    /// update auto tickets
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdATicket_Insert_Update(object sender, GridRecordEventArgs e)
    {
        try
        {
            //bool hasEroor = false;
            int Assigned = 0;
            int dept = 0;
            int freq = 0;
            if (((string)e.Record["Assigned"]) != "")
            {
                if (int.Parse((string)e.Record["Assigned"]) > 0)
                {
                    Assigned = int.Parse((string)e.Record["Assigned"]);
                }
            }

            if (((string)e.Record["DeptAssign"]) != "")
            {
                if (int.Parse((string)e.Record["DeptAssign"]) > 0)
                {
                    dept = int.Parse((string)e.Record["DeptAssign"]);
                }
            }
            DateTime tester = DateTime.Parse((string)e.Record["StartDate"]);
            freq = int.Parse((string)e.Record["Frequency"]);
          
           
            /* Set properties to update */

            Emrdev.ViewModelLayer.AdminAutoTicketViewModel objModel = new Emrdev.ViewModelLayer.AdminAutoTicketViewModel();
            objModel.AutoTicketID = int.Parse((string)e.Record["AutoTicketID"]);
            objModel.Assigned = Assigned;
            objModel.AutoticketName = (string)e.Record["AutoticketName"];
            objModel.Body = (string)e.Record["Body"];
            objModel.CreatedID = int.Parse((string)e.Record["CreatedID"]);
            objModel.DeptAssign = dept;
            objModel.FollowUp_Type_ID = int.Parse((string)e.Record["FollowUp_Type_ID"]);
            objModel.Frequency = freq;
            objModel.FrequencyType = (string)e.Record["FrequencyType"];
            objModel.LastSent = (DateTime?)null;
            objModel.StartDate = DateTime.Parse((string)e.Record["StartDate"]);
            objModel.Subject = (string)e.Record["Subject"];
            objService = new Emrdev.ServiceLayer.AdminAutoTicketService();
            objService.UpdateTicket(objModel);
           
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }
    /// <summary>
    /// delete auto ticket
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdATicket_Delete(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new Emrdev.ServiceLayer.AdminAutoTicketService();
            objService.DeleteTicketById(int.Parse((string)e.Record["AutoTicketID"]));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    /// <summary>
    /// Rebind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdATicket_Rebind(object sender, EventArgs e)
    {
        try
        {
            objService = new Emrdev.ServiceLayer.AdminAutoTicketService();
            grdATicket.DataSource = objService.GetAutoticketList();
            grdATicket.DataBind();
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