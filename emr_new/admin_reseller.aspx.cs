using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.Grid;
using Obout.Ajax.UI.HTMLEditor;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class admin_reseller : LMCBase
{
    #region Variable
    IAdminResellerService objService;
    #endregion
    #region Events
    /// <summary>
    /// On page load bind the dropdowns and grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new AdminResellerService();

                //Fill status dropdown in grid
                DropDownList ddlstatus = grdReseller.Templates[0].Container.FindControl("ddlstatus") as DropDownList;
                if (ddlstatus != null)
                {
                    ddlstatus.DataSource = objService.GetResellerStatus();
                    ddlstatus.DataBind();
                }

                //Fill event dropdown in grid
                DropDownList ddlEvent = grdReseller.Templates[0].Container.FindControl("ddlEvent") as DropDownList;
                if (ddlEvent != null)
                {
                    ddlEvent.DataSource = objService.GetResellerEvent();
                    ddlEvent.DataBind();
                }

                //Fill sales rep dropdown in grid
                DropDownList ddlSalesRep = grdReseller.Templates[0].Container.FindControl("ddlSalesRep") as DropDownList;
                if (ddlSalesRep != null)
                {
                    ddlSalesRep.DataSource = objService.GetSaleRep();
                    ddlSalesRep.DataBind();
                }

                //Fill marketing source dropdown in grid
                DropDownList ddlSource = grdReseller.Templates[0].Container.FindControl("ddlSource") as DropDownList;
                if (ddlSource != null)
                {
                    ddlSource.DataSource = objService.GetMarketingSource();
                    ddlSource.DataBind();
                }

                //Bind grid for all reseller
                grdReseller.DataSource = objService.GetAllRellers();
                grdReseller.DataBind();
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
    }
    
    /// <summary>
    /// Change color of column on the basis of the color selected
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="args"></param>
    protected void grdReseller_RowDataBound(object sender, GridRowEventArgs args)
    {
        switch (args.Row.Cells[27].Text.ToLower())
        {
            case "red":
                args.Row.Cells[27].BackColor = System.Drawing.Color.Red;
                args.Row.Cells[27].ForeColor = System.Drawing.Color.White;
                break;
            case "yellow":
                args.Row.Cells[27].BackColor = System.Drawing.Color.Yellow;
                args.Row.Cells[27].ForeColor = System.Drawing.Color.Black;
                break;
            case "green":
                args.Row.Cells[27].BackColor = System.Drawing.Color.Green;
                args.Row.Cells[27].ForeColor = System.Drawing.Color.White;
                break;
        }
    }

    /// <summary>
    /// Rebind the grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdReseller_Rebind(object sender, EventArgs e)
    {
        try
        {
            objService = new AdminResellerService();
            grdReseller.DataSource = objService.GetAllRellers();
            grdReseller.DataBind();
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
    /// Add Contact Details on click of add button from contact
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnAddContact_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new AdminResellerService();
            AdminResellerContactViewModel cont = new AdminResellerContactViewModel();
            cont.DateEntered = DateTime.Now;
            cont.EnteredBy = (int)Session["StaffID"];
            cont.MessageBody = ContactEd.Content;
            cont.ResellerID = int.Parse(((Hashtable)grdReseller.SelectedRecords[0])["ResellerID"].ToString());
            objService.InsertContact(cont);
            
            Response.Redirect("admin_reseller.aspx",false);
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
    /// Update Reseller data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdReseller_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        AdminResellerViewModel res = new AdminResellerViewModel();
        AdminResellerViewModel oldRes = new AdminResellerViewModel();
        try
        {
            objService = new AdminResellerService();
            int ResellerID = 0;
            if (e.Record["ResellerID"].ToString() != "")
            {
                ResellerID = int.Parse((string)e.Record["ResellerID"]);
                res.ResellerID = ResellerID;
              
            }
            res.AttendedDinner = bool.Parse(e.Record["AttendedDinner"].ToString());
            res.BusinessName = e.Record["BusinessName"].ToString();
            res.City = e.Record["City"].ToString();
            res.ContactFirstName = e.Record["ContactFirstName"].ToString();
            res.ContactLastName = e.Record["ContactLastName"].ToString();
            res.Description = e.Record["Description"].ToString();
            res.Email = e.Record["Email"].ToString();
            res.Fax = e.Record["Fax"].ToString();
            res.FirstName = e.Record["FirstName"].ToString();
            res.LastName = e.Record["LastName"].ToString();
            res.Notes = e.Record["Notes"].ToString();
            res.Phone = e.Record["Phone"].ToString();
            if (e.Record["SalesRep"].ToString() != "")
            {
                res.SalesRep = int.Parse(e.Record["SalesRep"].ToString());
            }
            else
            {
                res.SalesRep = 0;
            }
            res.State = e.Record["State"].ToString();
            if (e.Record["StatusID"] != null && e.Record["StatusID"] != "")
            {
                res.StatusID = int.Parse(e.Record["StatusID"].ToString());
            }
            else
            {
                res.StatusID = 0;
            }
            res.StreetAddress = e.Record["StreetAddress"].ToString();
            res.Zip = e.Record["Zip"].ToString();
            res.EventID = e.Record["EventID"].ToString() == "" ? 0 : int.Parse(e.Record["EventID"].ToString());
            res.DateEnrolled = e.Record["DateEnrolled"].ToString() == "" ? (DateTime?)null : DateTime.Parse(e.Record["DateEnrolled"].ToString());
            res.CoManageAgreement = bool.Parse(e.Record["CoManageAgreement"].ToString());
            res.ContractDate = e.Record["ContractDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(e.Record["ContractDate"].ToString());
            res.ContractSigned = bool.Parse(e.Record["ContractSigned"].ToString());
            res.CoManageDate = e.Record["CoManageDate"].ToString() == "" ? (DateTime?)null : DateTime.Parse(e.Record["CoManageDate"].ToString());
            res.ResellerMarketingSourceID = int.Parse(e.Record["ResellerMarketingSourceID"].ToString());
            res.LeadStatus = e.Record["LeadStatus"].ToString();
            if (e.Record["ResellerID"].ToString() == "")
            {
                res.DateEntered = DateTime.Now;
            }
            int staffID = (int)Session["StaffID"];
            objService.InsertResellerInfo(res, staffID);
           
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
}