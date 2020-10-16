using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using MailChimp;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.Services;

using Calendar;
using DayPilot.Web.Ui;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
public partial class calendar_CalendarAdmin : LMCBase
{
    #region Variable
    ICalendarService objService = null;
    #endregion

    #region Event
    /// <summary>
    /// Fill the Dropdown table from the database to get the details
    /// </summary>
    /// <param name="sender">1 for provide</param>
    /// <param name="e">2 for appointment types</param>
    /// <param name="e">3 for Status</param>
    /// <param name="e">4 for status</param>

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                objService = new CalendarService();
                ddTables.DataSource = objService.GetCalendarDetails();

                ddTables.DataTextField = "TableName";
                ddTables.DataValueField = "TableID";

                ddTables.DataBind();

               

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
    /// Sort the admin grid and appointmenttype grid
    /// </summary>

    protected void OnSort(object sender, GridViewSortEventArgs e)
    {
        try
        {
            if (ddTables.SelectedIndex == 1 || ddTables.SelectedIndex==4)
            {
                objService = new CalendarService();
                List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
                foreach (var entity in _objGetCalList)
                {
                    if (entity.TableID == ddTables.SelectedIndex)
                    {
                        ProvidersSource.TypeName = entity.TypeObject;
                        ProvidersSource.SelectMethod = entity.SelectMethod;
                        ProvidersSource.InsertMethod = entity.InsertMethod;
                        ProvidersSource.UpdateMethod = entity.UpdateMethod;
                        ProvidersSource.DataBind();
                        Admin.Visible = true;
                        Admin.Caption = "Manage " + ddTables.SelectedItem.Text;
                        Admin.DataBind();
                        break;
                    }
                }

                AddHeader.InnerText = "Add " + ddTables.SelectedItem.Text;
                prov.Visible = true;
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
    /// Fill the grids on the basis of dropdown selected values
    /// </summary>
    protected void ddTables_SelectIndexChanged(object sender, EventArgs e)
    {
        if (ddTables.SelectedIndex == 0)
        {
            prov.Visible = false;
            Admin.Visible = false;
            ApptTypeGrid.Visible = false;
            grdResult.Visible = false;
            return;
        }
        if (ddTables.SelectedIndex == 2)
        {
            Admin.Visible = false;
            ApptTypeGrid.Visible = true;
            grdResult.Visible = false;
        }
        else if(ddTables.SelectedIndex == 3)
        {
            Admin.Visible = false;
            ApptTypeGrid.Visible = false;
            grdResult.Visible = true;
        }
        else
        {
            Admin.Visible = true;
            ApptTypeGrid.Visible = false;
            grdResult.Visible = false;
        }

        try
        {
            objService = new CalendarService();
            List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
            foreach (var entity in _objGetCalList)
            {
                if (entity.TableID == ddTables.SelectedIndex)
                {
                    ProvidersSource.TypeName = entity.TypeObject;
                    ProvidersSource.SelectMethod = entity.SelectMethod;
                    ProvidersSource.InsertMethod = entity.InsertMethod;
                    ProvidersSource.UpdateMethod = entity.UpdateMethod;
                    ProvidersSource.DataBind();
                    Admin.Caption = "Manage " + ddTables.SelectedItem.Text;
                    break;
                }
            }
            AddHeader.InnerText = "Add " + ddTables.SelectedItem.Text;
            prov.Visible = true;
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
    /// On click of edit button this function fires and allow the user to edit the data
    /// </summary>
    protected void Admin_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            if (ddTables.SelectedIndex ==1 ||ddTables.SelectedIndex==4)
            {

                objService = new CalendarService();
                List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
                foreach (var entity in _objGetCalList)
                {
                    if (entity.TableID == ddTables.SelectedIndex)
                    {
                        ProvidersSource.TypeName = entity.TypeObject;
                        ProvidersSource.SelectMethod = entity.SelectMethod;
                        ProvidersSource.InsertMethod = entity.InsertMethod;
                        ProvidersSource.UpdateMethod = entity.UpdateMethod;
                        ProvidersSource.DataBind();
                        Admin.Visible = true;
                        Admin.DataBind();

                    }
                }

                Session["CurrentID"] = Admin.Rows[e.NewEditIndex].Cells[1].Text;
                Admin.EditIndex = e.NewEditIndex;
            }
            else if(ddTables.SelectedIndex==2)
            {
                Session["CurrentID"] = ApptTypeGrid.Rows[e.NewEditIndex].Cells[1].Text;
                ApptTypeGrid.EditIndex = e.NewEditIndex;
            }
            else if (ddTables.SelectedIndex ==3)
            {
                Session["CurrentID"] = grdResult.Rows[e.NewEditIndex].Cells[1].Text;
                grdResult.EditIndex = e.NewEditIndex;
            }
            e.Cancel = true;
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
    /// On click of edit button this function fires and allow the user to edit the data
    /// </summary>
    protected void ApptTypeGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int userid = Convert.ToInt32(ApptTypeGrid.DataKeys[e.NewEditIndex].Value.ToString());
            Session["CurrentID"] = userid.ToString();
            ApptTypeGrid.EditIndex = e.NewEditIndex;

            e.Cancel = true;
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

    protected void ApptTypeGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("drpCampaignList");
                //bind dropdown-list
                MailChimpManager mc = new MailChimpManager(System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"]);
                var campaigns = mc.GetCampaigns();
                List<MailChimpCampaignViewModel> CampaignList = new List<MailChimpCampaignViewModel>();

                for (int i = 0; i < campaigns.Data.Count; i++)
                {
                    MailChimpCampaignViewModel CampaignData = new MailChimpCampaignViewModel();
                    CampaignData.MailChimpCampaignId = campaigns.Data[i].Id + "~" + campaigns.Data[i].ListId;
                    CampaignData.MailChimpCampaignName = campaigns.Data[i].Title + " " + campaigns.Data[i].Id;
                    CampaignList.Add(CampaignData);
                }
                                
                ddList.DataSource = CampaignList;
                ddList.DataTextField = "MailChimpCampaignName";
                ddList.DataValueField = "MailChimpCampaignId";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select Campaign", "-1"));
               

                DropDownList ddStatusList = (DropDownList)e.Row.FindControl("drpStatusList");
                //bind dropdown-list

                ICalendarService _objService = new CalendarService();
                List<CalStatusViewModel> status = _objService.GetCalStatus().Where(s=>s.Active==true).ToList();



                ddStatusList.DataSource = status;
                ddStatusList.DataTextField = "StatusName";
                ddStatusList.DataValueField = "StatusId";
                ddStatusList.DataBind();
                ddStatusList.Items.Insert(0, new ListItem("Select Status", "0"));

                DataRowView dr = e.Row.DataItem as DataRowView;

               
                ddList.SelectedValue = dr["ResultStatusId"].ToString();


                //ddList.SelectedItem.Text = dr["MailChimpCampaignName"].ToString();
                ddList.SelectedValue = dr["MailChimpCampaignId"].ToString();
            }
        }
    }
   

    protected void grdResult_RowEditing(object sender, GridViewEditEventArgs e)
    {
        try
        {
            int userid = Convert.ToInt32(grdResult.DataKeys[e.NewEditIndex].Value.ToString());
            Session["CurrentID"] = userid.ToString();
            grdResult.EditIndex = e.NewEditIndex;

            e.Cancel = true;
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

    protected void grdResult_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            if ((e.Row.RowState & DataControlRowState.Edit) > 0)
            {
                DropDownList ddList = (DropDownList)e.Row.FindControl("drpStatusList");
                //bind dropdown-list

                ICalendarService _objService = new CalendarService();
                List<CalStatusViewModel> status = _objService.GetCalStatus().Where(s => s.Active == true).ToList();

                

                ddList.DataSource = status;
                ddList.DataTextField = "StatusName";
                ddList.DataValueField = "StatusId";
                ddList.DataBind();
                ddList.Items.Insert(0, new ListItem("Select Status", "0"));
                
                DataRowView dr = e.Row.DataItem as DataRowView;

                //ddList.SelectedItem.Text = dr["MailChimpCampaignName"].ToString();
                ddList.SelectedValue = dr["ResultStatusId"].ToString();
            }
        }
    }

    /// <summary>
    /// Add new data in the grids
    /// </summary>
    protected void AddButton_OnClick(object sender, EventArgs e)
    {
        try
        {
            switch (ddTables.SelectedItem.Text)
            {
                case "Providers":
                    Calendar.Providers.AddProvider(ProviderName.Text, true);
                    break;
                case "AppointmentType":
                    Calendar.AppointmentTypes.AppointmentTypeInsert(ProviderName.Text, "black");
                    break;
                case "Results":
                    Calendar.Results.ResultsInsert(ProviderName.Text);
                    break;
                case "Status":
                    Calendar.Status.StatusInsert(ProviderName.Text);
                    break;
            }


            objService = new CalendarService();
            List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
            foreach (var entity in _objGetCalList)
            {
                if (entity.TableID == ddTables.SelectedIndex)
                {
                    if (ddTables.SelectedIndex == 2)
                    {
                        AptTypeSource.TypeName = entity.TypeObject;
                        AptTypeSource.SelectMethod = entity.SelectMethod;
                        AptTypeSource.InsertMethod = entity.InsertMethod;
                        AptTypeSource.UpdateMethod = entity.UpdateMethod;
                        AptTypeSource.DataBind();
                        ApptTypeGrid.Visible = true;
                        ApptTypeGrid.DataBind();
                    }
                    else
                    {

                        ProvidersSource.TypeName = entity.TypeObject;
                        ProvidersSource.SelectMethod = entity.SelectMethod;
                        ProvidersSource.InsertMethod = entity.InsertMethod;
                        ProvidersSource.UpdateMethod = entity.UpdateMethod;
                        ProvidersSource.DataBind();
                        Admin.Visible = true;
                        //Admin.Caption = "Manage " + ddTables.SelectedItem.Text;
                        Admin.DataBind();

                    }
                }
            }
            ProviderName.Text = "";
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
    /// cancelling the edit
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Admin_RowCaneclling(object sender, GridViewCancelEditEventArgs e)
    {
        if (ddTables.SelectedIndex ==1 ||ddTables.SelectedIndex==4)
        {
            try
            {
                objService = new CalendarService();
                List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
                foreach (var entity in _objGetCalList)
                {
                    if (entity.TableID == ddTables.SelectedIndex)
                    {
                        ProvidersSource.TypeName = entity.TypeObject;
                        ProvidersSource.SelectMethod = entity.SelectMethod;
                        ProvidersSource.InsertMethod = entity.InsertMethod;
                        ProvidersSource.UpdateMethod = entity.UpdateMethod;
                        ProvidersSource.DataBind();
                        Admin.Visible = true;
                        Admin.DataBind();

                    }
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
    }
    /// <summary>
    /// updating the row
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Admin_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        try
        {
            //******************** javascript code for check the duplicate records.*************
            //string script = "<script src='../Scripts/jquery-1.7.2.js' type='text/javascript'></script>";
            //script += "<script src='../Scripts/Scrips.js' type='text/javascript'></script>";
            //script += "<script type=text/javascript>ValidateDataServerSideForCalendarAdmin('" + ddTables.SelectedItem.Text + "','" + (string)Session["CurrentID"] + "','" + (string)e.NewValues["ProviderName"] + "'); </script>";
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", script);
            //****************************************************************************

            switch (ddTables.SelectedItem.Text)
            {
                case "Providers":
                    if ((string)e.NewValues["id"] == (string)Session["CurrentID"])
                    {
                        Calendar.Provider prov = new Calendar.Provider(int.Parse(e.NewValues["id"].ToString()), (string)e.NewValues["ProviderName"], (bool)e.NewValues["Active"]);
                        prov.MondayStart = (string)e.NewValues["MondayStart"];
                        prov.MondayEnd = (string)e.NewValues["MondayEnd"];
                        prov.TuesdayStart = (string)e.NewValues["TuesdayStart"];
                        prov.TuesdayEnd = (string)e.NewValues["TuesdayEnd"];
                        prov.WednesdayStart = (string)e.NewValues["WednesdayStart"];
                        prov.WednesdayEnd = (string)e.NewValues["WednesdayEnd"];
                        prov.ThursdayStart = (string)e.NewValues["ThursdayStart"];
                        prov.ThursdayEnd = (string)e.NewValues["ThursdayEnd"];
                        prov.FridayStart = (string)e.NewValues["FridayStart"];
                        prov.FridayEnd = (string)e.NewValues["FridayEnd"];
                        prov.Category = (string)e.NewValues["Category"];
                        Calendar.Providers.udpateProvider(prov);
                    }
                    break;
                case "AppointmentType":

                    //if ((string)e.NewValues["id"] == (string)Session["CurrentID"])
                    //int.Parse((string)e.NewValues["id"], //(Comment this line by Rajesh)
                    if ((string)e.Keys["id"].ToString() == (string)Session["CurrentID"])
                    {
                        e.NewValues["ConfirmationText"] = ((TextBox)ApptTypeGrid.Rows[e.RowIndex].Cells[8].Controls[1]).Text;
                        if (e.NewValues["Attachment"] == null)
                            e.NewValues["Attachment"] = "";
                        if (e.NewValues["WufooFormKey"] == null)
                            e.NewValues["WufooFormKey"] = "";
                        e.NewValues["MailChimpCampaignName"]= ((DropDownList)ApptTypeGrid.Rows[e.RowIndex].Cells[6].Controls[1]).SelectedItem.Text;

                        string MailchimpName = "Select Campaign";
                        if ((string)e.NewValues["MailChimpCampaignName"] == MailchimpName)
                        {
                            e.NewValues["MailChimpCampaignName"] = "";
                            e.NewValues["MailChimpCampaignId"] = "";
                        }
                        else if ((string)e.NewValues["MailChimpCampaignName"] == "")
                        {
                            e.NewValues["MailChimpCampaignName"] = "";
                            e.NewValues["MailChimpCampaignId"] = "";
                        }
                        else
                        {
                            e.NewValues["MailChimpCampaignId"] = ((DropDownList)ApptTypeGrid.Rows[e.RowIndex].Cells[6].Controls[1]).SelectedItem.Value;
                        }

                        e.NewValues["StatusName"] = ((DropDownList)ApptTypeGrid.Rows[e.RowIndex].Cells[9].Controls[1]).SelectedItem.Text;

                        string StatusName = "Select Status";
                        if ((string)e.NewValues["StatusName"] == StatusName)
                        {
                            e.NewValues["StatusName"] = "";
                            e.NewValues["StatusId"] = "0";
                        }
                        else if ((string)e.NewValues["StatusName"] == "")
                        {
                            e.NewValues["StatusName"] = "";
                            e.NewValues["StatusId"] = "0";
                        }
                        else
                        {
                            e.NewValues["StatusId"] = ((DropDownList)ApptTypeGrid.Rows[e.RowIndex].Cells[9].Controls[1]).SelectedItem.Value;
                        }
                        Calendar.AppointmentType type = new Calendar.AppointmentType(int.Parse((string)e.Keys["id"].ToString()), (string)e.NewValues["TypeName"],
                            (string)e.NewValues["Color"], bool.Parse((string)e.NewValues["Active"]), (string)e.NewValues["Category"],
                            bool.Parse((string)e.NewValues["ConfirmationEmail"]), (string)e.NewValues["ConfirmationText"], (string)e.NewValues["Attachment"],
                            (string)e.NewValues["Subject"], (String)e.NewValues["EmailFromAddress"], (string)e.NewValues["EmailFromName"], bool.Parse((string)e.NewValues["OVU"]),
                            (string)e.NewValues["WufooFormKey"],(string)e.NewValues["MailChimpCampaignId"], 
                            (string)e.NewValues["MailChimpCampaignName"], (string)e.NewValues["StatusName"],int.Parse((string)e.NewValues["StatusId"]));
                        Calendar.AppointmentTypes.AppointmentTypeUpdate(type);
                        ApptTypeGrid.EditIndex = -1;
                        ApptTypeGrid.DataBind();
                        Admin.Visible = false;
                    }
                    break;
                case "Results":
                    
                    if ((string)e.Keys["ID"].ToString() == (string)Session["CurrentID"])
                    {
                        
                        e.NewValues["StatusName"] = ((DropDownList)grdResult.Rows[e.RowIndex].Cells[5].Controls[1]).SelectedItem.Text;

                        string StatusName = "Select Status";
                        if ((string)e.NewValues["StatusName"] == StatusName)
                        {
                            e.NewValues["StatusName"] = "";
                            e.NewValues["StatusId"] = "0";
                        }
                        else if ((string)e.NewValues["StatusName"] == "")
                        {
                            e.NewValues["StatusName"] = "";
                            e.NewValues["StatusId"] = "0";
                        }
                        else
                        {
                            e.NewValues["StatusId"] = ((DropDownList)grdResult.Rows[e.RowIndex].Cells[5].Controls[1]).SelectedItem.Value;
                        }


                        Calendar.Results.ResultsUpdate(((string)e.Keys["ID"].ToString()),(string)e.NewValues["ResultName"], bool.Parse((string)e.NewValues["Active"]), bool.Parse((string)e.NewValues["IsActionRequired"]), int.Parse((string)e.NewValues["StatusId"]));
                        grdResult.EditIndex = -1;
                        grdResult.DataBind();
                        Admin.Visible = false;
                        ApptTypeGrid.Visible = false;


                    }
                    break;

                    //if ((string)e.NewValues["ID"] == (string)Session["CurrentID"])
                    //{
                    //    Calendar.Results.ResultsUpdate((string)e.NewValues["ID"], (string)e.NewValues["ResultName"], (bool)e.NewValues["Active"],(bool)e.NewValues["IsActionRequired"],(int)e.NewValues["Result);
                    //}
                    //break;
                case "Status":
                    if ((string)e.NewValues["id"] == (string)Session["CurrentID"])
                    {
                        Calendar.Status.StatusUpdate((string)e.NewValues["id"], (string)e.NewValues["StatusName"], (bool)e.NewValues["Active"]);
                    }
                    break;
            }
            if (ddTables.SelectedIndex ==1||ddTables.SelectedIndex==4)
            {
                objService = new CalendarService();
                List<CalendarViewModel> _objGetCalList = objService.GetCalendarDetails();
                foreach (var entity in _objGetCalList)
                {
                    if (entity.TableID == ddTables.SelectedIndex)
                    {
                        ProvidersSource.TypeName = entity.TypeObject;
                        ProvidersSource.SelectMethod = entity.SelectMethod;
                        ProvidersSource.InsertMethod = entity.InsertMethod;
                        ProvidersSource.UpdateMethod = entity.UpdateMethod;
                        ProvidersSource.DataBind();
                        Admin.Visible = true;

                        Admin.DataBind();

                    }
                }
            }
            e.Cancel = true;
            Admin.EditIndex = -1;
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
    /// this method is using for check the duplicate calendar's details during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 05.Aug.2013    
    /// </summary>
    /// <param name="Text"></param>
    /// <param name="ID"></param>    
    /// <param name="Name"></param>   
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateData(string Text, string ID, string Name)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IProviderService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            _objService = new ProviderService();
            isExist = _objService.CheckDuplicateData(Text, Convert.ToInt32(ID), Name);
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
    /// code for hide the id column which is generated on run-time.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Admin_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Visible = false;
        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Visible = false;
        }
    }
}
