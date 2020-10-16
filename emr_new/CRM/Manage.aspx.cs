using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using Obout.ComboBox;
using System.Configuration;
using System.Data;
using Emrdev.ServiceLayer;
using System.Collections;
using System.Web.Services;
using Emrdev.ViewModelLayer;
using Calendar;
using System.Web.Script.Serialization;
using System.Net;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
public partial class CRM_Manage : LMCBase
{
    #region "Variable"
    ManageService _objService = null;
    #endregion

    #region "Events"
    /// <summary>
    /// binding all the controls on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            StaffID.Value = ((int)Session["StaffID"]).ToString();
            if (!IsPostBack)
            {
                _objService = new ManageService();

                //Method to get all Apt records
                ddlAppts.DataSource = _objService.GetAllapt_recs();
                ddlAppts.DataBind();

                drpProviders.DataSource = Providers.getProviderList().ToList();
                drpProviders.DataBind();

                //fill grid with all crm prospects
                grdProspect.DataSource = _objService.GetAllProspect();
                grdProspect.DataBind();

                //fill grid with all crm campaingns
                grdCampaign.DataSource = _objService.GetAllCrmCampaign();
                grdCampaign.DataBind();

                DropDownList ddlstatus = grdProspect.Templates[0].Container.FindControl("ddlstatus") as DropDownList;
                if (ddlstatus != null)
                {
                    //fill status dropdown with status data
                    ddlstatus.DataSource = _objService.GetAllactiveStatus();
                    ddlstatus.DataBind();
                }


                ComboBox ddlMarketingSource = grdProspect.Templates[0].Container.FindControl("combo") as ComboBox;
                IEnumerable MarketingSource = _objService.GetAllMarketingSource();
                if (ddlMarketingSource != null)
                {
                    //fill MarketingSource dropdown with MarketingSource data
                    ddlMarketingSource.DataSource = MarketingSource;
                    ddlMarketingSource.DataBind();
                }


                ComboBox ddlCampignMarketingSource = grdCampaign.Templates[0].Container.FindControl("cboSources") as ComboBox;
                if (ddlCampignMarketingSource != null)
                {
                    //fill MarketingSource dropdown with MarketingSource data
                    ddlCampignMarketingSource.DataSource = MarketingSource;
                    ddlCampignMarketingSource.DataBind();
                }



                DropDownList ddlEvents1 = grdProspect.Templates[0].Container.FindControl("cboEvents") as DropDownList;
                IEnumerable Events = _objService.GetAllEvents();



                if (ddlEvents1 != null)
                {
                    //fill Event dropdown with all events data
                    ddlEvents1.DataSource = Events;
                    ddlEvents1.DataBind();
                    ddlEvents1.Items.Insert(0, new ListItem("--Select Event--", "0"));
                }

                //fill Event clinic with all clinic names data
                ddlClinic.DataSource = _objService.GetAllClinic();
                ddlClinic.DataBind();



                DropDownList cboCampaignType = grdCampaign.Templates[0].Container.FindControl("cboCampaignType") as DropDownList;
                if (cboCampaignType != null)
                {
                    //fill Event dropdown with all events data
                    cboCampaignType.DataSource = _objService.GetAllactiveCampaignType(true);
                    cboCampaignType.DataBind();
                    cboCampaignType.Items.Insert(0, new ListItem("None", "0"));

                }


                grdStatus.DataSource = _objService.GetAllactiveStatus();
                grdStatus.DataBind();


                grdMSource.DataSource = MarketingSource;
                grdMSource.DataBind();

                PopulateTime();

                IEmailTemplateService objTemplateService = new EmailTemplateService();
                CRMEmailTemplateViewModel EmailTemplate = objTemplateService.GetCRMEmailTemplate();
                if (EmailTemplate != null)
                {
                    txtCRMWufooFormLink.Text = EmailTemplate.WufooFormLink;
                    edContent.Content = EmailTemplate.EmailDescription;

                    chkIsActive.Checked = EmailTemplate.IsActive;
                }

            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    /// <summary>
    /// Delete Data from GrdEvent
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEvent_Delete(object sender, GridRecordEventArgs e)
    {
        try
        {
            _objService = new ManageService();
            _objService.DeleteEvent(int.Parse(e.Record["EventID"].ToString()));

            DropDownList ddlEvents1 = grdProspect.Templates[0].Container.FindControl("cboEvents") as DropDownList;
            IEnumerable Events = _objService.GetAllEvents();



            if (ddlEvents1 != null)
            {
                //fill Event dropdown with all events data
                ddlEvents1.DataSource = Events;
                ddlEvents1.DataBind();
                ddlEvents1.Items.Insert(0, new ListItem("--Select Event--", "0"));
            }

            ddlEvent.DataSource = Events;
            ddlEvent.DataBind();
            ddlEvent.Items.Insert(0, new ListItem("Select Event", "0"));

            DropDownList cboCampaignType = grdCampaign.Templates[0].Container.FindControl("cboCampaignType") as DropDownList;
            if (cboCampaignType != null)
            {
                //fill Event dropdown with all events data
                cboCampaignType.DataSource = _objService.GetAllactiveCampaignType(true);
                cboCampaignType.DataBind();
                cboCampaignType.Items.Insert(0, new ListItem("None", "0"));

            }

            ddlCampaign.DataSource = _objService.GetAllCampaign();
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", "0"));

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Delete Data from campaing grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdCampaign_Delete(object sender, GridRecordEventArgs e)
    {
        try
        {
            _objService = new ManageService();
            _objService.DeleteCampaign(int.Parse(e.Record["CampaignID"].ToString()));

            DropDownList ddlEvents1 = grdProspect.Templates[0].Container.FindControl("cboEvents") as DropDownList;
            IEnumerable Events = _objService.GetAllEvents();



            if (ddlEvents1 != null)
            {
                //fill Event dropdown with all events data
                ddlEvents1.DataSource = Events;
                ddlEvents1.DataBind();
                ddlEvents1.Items.Insert(0, new ListItem("--Select Event--", "0"));
            }

            ddlEvent.DataSource = Events;
            ddlEvent.DataBind();
            ddlEvent.Items.Insert(0, new ListItem("Select Event", "0"));

            DropDownList cboCampaignType = grdCampaign.Templates[0].Container.FindControl("cboCampaignType") as DropDownList;
            if (cboCampaignType != null)
            {
                //fill Event dropdown with all events data
                cboCampaignType.DataSource = _objService.GetAllactiveCampaignType(true);
                cboCampaignType.DataBind();
                cboCampaignType.Items.Insert(0, new ListItem("None", "0"));

            }

            ddlCampaign.DataSource = _objService.GetAllCampaign();
            ddlCampaign.DataBind();
            ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", "0"));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }




    /// <summary>
    /// this method is using for check the duplicate Email address during Add/Edit the record.
    /// Tab: Manage Prospect
    /// created by: Deepak Thakur
    /// created date: 02.Aug.2013
    /// </summary>
    /// <param name="diagnosisID"></param>
    /// <param name="diagName"></param>
    /// <param name="iCDCode"></param>
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateProspect(string prospectID, string emailAddress)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(prospectID))
                prospectID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateProspect(Convert.ToInt32(prospectID), emailAddress);
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
    /// this method is using for check the duplicate Email address during Add/Edit the record.
    /// Tab: Manage Prospect
    /// created by: Deepak Thakur
    /// created date: 02.Aug.2013
    /// </summary>
    /// <param name="diagnosisID"></param>
    /// <param name="diagName"></param>
    /// <param name="iCDCode"></param>
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateStatus(string statusID, string statusName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(statusID))
                statusID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateStatus(Convert.ToInt32(statusID), statusName);
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
    /// this method is using for check the duplicate marketing source name during Add/Edit the record.
    /// Tab: Status and Sources
    /// created by: Rakesh Kumar
    /// created date: 05.Aug.2013
    /// </summary>
    /// <param name="marketingSourceID"></param>
    /// <param name="marketingSourceName"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string CheckDuplicateMarketingSource(string marketingSourceID, string marketingSourceName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(marketingSourceID))
                marketingSourceID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateMarketingSource(Convert.ToInt32(marketingSourceID), marketingSourceName);
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
    /// this method is using for check the duplicate CampaignName during Add/Edit the record.
    /// Tab: Status and Sources
    /// created by: Rakesh Kumar
    /// created date: 05.Aug.2013
    /// </summary>
    /// <param name="campaignID"></param>
    /// <param name="campaignName"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string CheckDuplicateCampaignName(string campaignID, string campaignName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(campaignID) || campaignID == "undefined")
                campaignID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateCampaignName(Convert.ToInt32(campaignID), campaignName);
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
    /// this method is using for check the duplicate CampaignName during Add/Edit the record.
    /// Tab: Status and Sources
    /// created by: Rakesh Kumar
    /// created date: 05.Aug.2013
    /// </summary>
    /// <param name="campaignID"></param>
    /// <param name="campaignName"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string CheckDuplicateEventName(string campaignID, string eventID, string eventName, string eventDate)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(campaignID) || campaignID == "undefined")
                campaignID = "0";
            if (string.IsNullOrEmpty(eventID) || eventID == "undefined")
                eventID = "0";
            DateTime eventDateNew = Convert.ToDateTime(eventDate);
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateEventName(Convert.ToInt32(campaignID), Convert.ToInt32(eventID), eventName, eventDateNew);
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
    /// this method is using for check the duplicate marketing activity during Add/Edit the record.
    /// Tab: Status and Sources
    /// created by: Rakesh Kumar
    /// created date: 05.Aug.2013
    /// </summary>
    /// <param name="campaignID"></param>
    /// <param name="campaignName"></param>
    /// <returns>string</returns>
    [WebMethod]
    public static string CheckDuplicateMarketingActivity(string campaignID, string marketingActivityID, string sourceType, string startDate, string endDate, string sourceID)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(campaignID) || campaignID == "undefined")
                campaignID = "0";
            if (string.IsNullOrEmpty(marketingActivityID) || marketingActivityID == "undefined")
                marketingActivityID = "0";
            DateTime StartDate = Convert.ToDateTime(startDate);
            DateTime EndDate = Convert.ToDateTime(endDate);

            _objService = new ManageService();
            isExist = _objService.CheckDuplicateMarketingActivity(Convert.ToInt32(campaignID), Convert.ToInt32(marketingActivityID), sourceType, StartDate, EndDate, Convert.ToInt32(sourceID));
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
    /// insert and update records in prospect grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUpdateProspect(object sender, GridRecordEventArgs e)
    {
        int ProspectID = 0;
        bool Flagged;
        int StatusID = 0;
        int EventID = 0;

        try
        {
            if (e.Record["ProspectID"].ToString() != "")
                ProspectID = int.Parse(e.Record["ProspectID"].ToString());

            if (e.Record["Flagged"].ToString() != "")
            {
                Flagged = bool.Parse(e.Record["Flagged"].ToString());
            }
            else
            {
                Flagged = false;
            }

            if (e.Record["StatusID"].ToString() != "")
                StatusID = int.Parse(e.Record["StatusID"].ToString());

            if (e.Record["EventID"].ToString() != "")
                EventID = int.Parse(e.Record["EventID"].ToString());
            _objService = new ManageService();
            _objService.InsertUpdateProspect(
                ProspectID,
                e.Record["Address"].ToString(),
                e.Record["AltPhone"].ToString(),
                e.Record["City"].ToString(),
                e.Record["ContactMethod"].ToString(),
                e.Record["Email"].ToString(),
                e.Record["FirstName"].ToString(),
                Flagged,
                e.Record["LastName"].ToString(),
                e.Record["MainPhone"].ToString(),
                e.Record["MarketingSources"].ToString(),
                e.Record["Notes"].ToString(),
                e.Record["State"].ToString(),
                StatusID,
                e.Record["Zip"].ToString(),
                int.Parse(Session["StaffID"].ToString()),
                EventID
                );

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);

        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Delete data from prospectGrid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProspect_Delete(object sender, GridRecordEventArgs e)
    {
        try
        {
            _objService = new ManageService();

            _objService.DeleteProspect(int.Parse(e.Record["ProspectID"].ToString()));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// insert- update records in status grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdStatus_UpdateInsert(object sender, GridRecordEventArgs e)
    {

        int StatusID = 0;
        bool Active_YN = false;
        try
        {
            if (e.Record["StatusID"].ToString() != "")
            {
                StatusID = int.Parse(e.Record["StatusID"].ToString());
            }

            if (e.Record["Active_YN"].ToString() != "")
            {
                Active_YN = bool.Parse(e.Record["Active_YN"].ToString());
            }
            _objService = new ManageService();

            _objService.InsertUpdateStatus(StatusID, Active_YN, e.Record["StatusName"].ToString());
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Rebind the data to status grid    
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdStatus_Rebind(object sender, EventArgs e)
    {
        BindStatusMgmt();
    }

    private void BindStatusMgmt()
    {
        try
        {
            _objService = new ManageService();
            IEnumerable status = _objService.GetAllactiveStatus();
            grdStatus.DataSource = status;
            grdStatus.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Insert-update data in marketing source grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdMSource_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        try
        {
            int MarketingSourceID = 0;
            bool Active_YN = false;
            if (e.Record["MarketingSourceID"].ToString() != "")
            {
                MarketingSourceID = int.Parse(e.Record["MarketingSourceID"].ToString());
            }

            if (e.Record["Active_YN"].ToString() != "")
            {
                Active_YN = bool.Parse(e.Record["Active_YN"].ToString());
            }

            _objService = new ManageService();
            _objService.InsertUpdateMSource(MarketingSourceID, Active_YN, e.Record["MarketingSourceName"].ToString());
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Rebind the data to status grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdMSource_Rebind(object sender, EventArgs e)
    {
        MarketingSourceMgmt();
    }

    private void MarketingSourceMgmt()
    {
        try
        {
            _objService = new ManageService();
            IEnumerable grdMarketingSource = _objService.GetAllMarketingSource();
            grdMSource.DataSource = grdMarketingSource;
            grdMSource.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }





    /// <summary>
    /// Bind grdMarketingActivity if ddlcampaingn dropdown is greater than 0
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ddlCampaign_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (int.Parse(ddlCampaign.SelectedValue) > 0)
            {
                _objService = new ManageService();
                grdMarketingActivity.DataSource = _objService.GetMarketingActivity(int.Parse(ddlCampaign.SelectedValue));
                grdMarketingActivity.DataBind();

                DropDownList ddlSource1 = grdMarketingActivity.Templates[0].Container.FindControl("ddlSource1") as DropDownList;
                if (ddlSource1 != null)
                {
                    ddlSource1.DataSource = _objService.GetAllMarketingSource();
                    ddlSource1.DataBind();
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
            _objService = null;
        }
    }


    /// <summary>
    /// Insert- Update Activity record
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUpdateActivity(object sender, GridRecordEventArgs e)
    {
        try
        {
            int MarketingActivityID = 0;

            if (e.Record["MarketingActivityID"].ToString() != "")
            {
                MarketingActivityID = int.Parse(e.Record["MarketingActivityID"].ToString());
            }

            _objService = new ManageService();
            _objService.InsertUpdateActivity(MarketingActivityID, ddlCampaign.SelectedValue, e.Record["EndDate"].ToString(),
                                    e.Record["Money_Spent"].ToString(), e.Record["Notes"].ToString(), e.Record["SourceID"].ToString(),
                                    e.Record["SourceType"].ToString(), e.Record["StartDate"].ToString());
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Rebind the Marketing Activity grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdMarketingActivity_Rebind(object sender, EventArgs e)
    {
        try
        {
            int Campaignvalue = int.Parse(ddlCampaign.SelectedValue);
            if (Campaignvalue > 0)
            {
                _objService = new ManageService();
                grdMarketingActivity.DataSource = _objService.GetMarketingActivity(Campaignvalue);
                grdMarketingActivity.DataBind();
            }
            else
            {
                grdMarketingActivity.ClearPreviousDataSource();
                grdMarketingActivity.DataSource = null;
                grdMarketingActivity.DataBind();
            }

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }



    protected void ddlEvent_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            _objService = new ManageService();
            int EventID = int.Parse(ddlEvent.SelectedValue);
            lblEventName.Text = ddlEvent.SelectedItem.Text;
            if (EventID > 0)
            {
                grdAttend.DataSource = _objService.GetAllAttend(EventID);
                grdAttend.DataBind();
            }
            else
            {
                grdAttend.ClearPreviousDataSource();
                grdAttend.DataSource = null;
                grdAttend.DataBind();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }



    /// <summary>
    /// Rebind the Attendent grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdAttend_Rebind(object sender, EventArgs e)
    {
        try
        {
            _objService = new ManageService();
            int EventID = int.Parse(ddlEvent.SelectedValue);
            if (EventID > 0)
            {
                grdAttend.DataSource = _objService.GetAllAttend(EventID);
                grdAttend.DataBind();
            }
            else
            {
                grdAttend.ClearPreviousDataSource();
                grdAttend.DataSource = null;
                grdAttend.DataBind();
            }
        }

        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    protected void grdAttend_Select(object sender, GridRecordEventArgs e)
    {
        if (e.RecordsCollection != null)
        {

        }
    }

    //protected void grdAttend_UpdateCommand(object sender, GridRecordEventArgs e)
    //{
    //    _objService = new ManageService();
    //    int EventID = int.Parse(ddlEvent.SelectedValue);
    //    if (EventID > 0)
    //    {
    //        grdAttend.DataSource = _objService.GetAllAttend(EventID);
    //        grdAttend.DataBind();
    //    }
    //    else
    //    {
    //        grdAttend.ClearPreviousDataSource();
    //        grdAttend.DataSource = null;
    //        grdAttend.DataBind();
    //    }
    //}


    /// <summary>
    /// Add Attendent data in grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [WebMethod]
    public static bool btnAttendedRecord(int eventID, int prospectID)
    {
        bool Result = false;
        ManageService _objService = new ManageService();
        _objService.AddRecordAttendee(eventID, prospectID);
        Result = true;


        return Result;


    }
    //protected void btnAttended_Click(object sender, EventArgs e)
    //{
    //    try
    //    {
    //        int EventID = int.Parse(ddlEvent.SelectedValue);

    //        if (grdAttend.SelectedRecords != null)
    //        {
    //            _objService = new ManageService();
    //            foreach (Hashtable oRecord in grdAttend.SelectedRecords)
    //            {

    //                _objService.AddRecordAttendee(EventID, int.Parse(oRecord["ProspectID"].ToString()));
    //            }

    //            grdAttend.DataSource = _objService.GetAllAttend(EventID);
    //            grdAttend.DataBind();
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
    //    }
    //    finally
    //    {
    //        _objService = null;
    //    }
    //}


    /// <summary>
    ///  insert update Campaing grid data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUpdateCampaign(object sender, GridRecordEventArgs e)
    {
        try
        {
            int CampaignID = 0;

            if (e.Record["CampaignID"].ToString() != "")
            {
                CampaignID = int.Parse(e.Record["CampaignID"].ToString());
            }

            _objService = new ManageService();
            _objService.InsertUpdateCampaign(CampaignID, e.Record["CampaignName"].ToString(), e.Record["CampaignTypeID"].ToString(),
                                    e.Record["MarketingBudget"].ToString(), e.Record["StartDate"].ToString(), e.Record["EndDate"].ToString(), e.Record["MarketingSources"].ToString());
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Rebind the Campaingn grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdCampaign_Rebind(object sender, EventArgs e)
    {
        try
        {
            _objService = new ManageService();
            grdCampaign.DataSource = _objService.GetAllCrmCampaign();
            grdCampaign.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Rebind the prospect grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdProspect_Rebind(object sender, EventArgs e)
    {
        try
        {
            _objService = new ManageService();
            grdProspect.DataSource = _objService.GetAllProspect();
            grdProspect.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// insert update event grid data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void InsertUpdateEvent(object sender, GridRecordEventArgs e)
    {
        try
        {
            int EventID = 0;
            if (e.Record["EventID"].ToString() != "")
            {
                EventID = int.Parse(e.Record["EventID"].ToString());

            }
            _objService = new ManageService();
            _objService.InsertUpdateEvent(EventID, e.Record["EventDate"].ToString(), e.Record["EventName"].ToString(),
                                   e.Record["Notes"].ToString(), e.Record["Venue"].ToString(), e.Record["Appointments"].ToString(), e.Record["AudienceQuality"].ToString(),
                                   e.Record["AudienceReaction"].ToString(), e.Record["Callbacks"].ToString(), e.Record["EventLength"].ToString(), e.Record["FacilityInteriorExterior"].ToString(),
                                    e.Record["Location"].ToString(), e.Record["OverallPerformance"].ToString(), e.Record["Parking"].ToString(), e.Record["VenueQuality"].ToString(),
                                    e.Record["Walkins"].ToString(), Convert.ToInt32(e.Record["CampaignID"]));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    /// <summary>
    /// method for delete the Status Management records
    /// created by: Deepak Thakur[14.April.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteStatusMgmt(object sender, GridRecordEventArgs e)
    {
        try
        {
            _objService = new ManageService();
            if (!string.IsNullOrEmpty(e.Record["StatusID"].ToString()))
                _objService.DeleteStatusMgmt(int.Parse(e.Record["StatusID"].ToString()));
            BindStatusMgmt();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }

    /// <summary>
    /// method for delete the Marketing Source Management records
    /// created by: Deepak Thakur[14.April.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteMarketingSourceMgmt(object sender, GridRecordEventArgs e)
    {
        try
        {
            _objService = new ManageService();
            if (!string.IsNullOrEmpty(e.Record["MarketingSourceID"].ToString()))
                _objService.DeleteMarketingSourceMgmt(int.Parse(e.Record["MarketingSourceID"].ToString()));
            MarketingSourceMgmt();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objService = null;
        }
    }


    /// <summary>
    /// Method to mark the event associated with particualar prospect as attended
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool RecordEventAppt(string data)
    {
        bool test;
        IManageService _objService = new ManageService();
        try
        {
            int PatientID = 0;
            int ProspectID = int.Parse(data.Split('|')[1]);
            int AptID = int.Parse(data.Split('|')[0]);
            string Clinic = (data.Split('|')[2]);
            int StaffID = int.Parse(data.Split('|')[3]);
            int EventID = int.Parse(data.Split('|')[4]);
            test = _objService.ReCordAttendee(PatientID, ProspectID, AptID, StaffID, Clinic, EventID);

            IMailChimpCampaignService objMailChimpCampaign = new MailChimpCampaignService();
            MailChimpCampaignViewModel CampaignData = new MailChimpCampaignViewModel();
            CampaignData = objMailChimpCampaign.GetMalChimpCampaign();
            if (CampaignData != null)
            {
                ManageGrdProspectViewModel ProspectDetails = _objService.GetProspectById(ProspectID);
                if (!string.IsNullOrEmpty(ProspectDetails.Email))
                {
                    string APIKey = System.Configuration.ConfigurationManager.AppSettings["MailChimpApiKey"];
                    string[] ItemData = Regex.Split(APIKey, "-");

                    if (ItemData != null)
                    {
                        if (ItemData.Length == 2)
                        {
                            var SaveData = AddOrUpdateListMember(ItemData[1], ItemData[0], CampaignData.MailChimpCampaignListId, ProspectDetails.Email, ProspectDetails.FirstName, ProspectDetails.LastName);

                        }
                    }

                }
            }

        }
        catch (System.Exception ex)
        {
            test = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            _objService = null;
        }

        return test;

    }

    private static string AddOrUpdateListMember(string dataCenter, string apiKey, string listId, string subscriberEmail, string FirstName, string LastName)
    {
        var sampleListMember = new JavaScriptSerializer().Serialize(
            new
            {
                email_address = subscriberEmail,
                merge_fields =
                new
                {
                    FNAME = FirstName,
                    LNAME = LastName
                },
                status_if_new = "subscribed"
            });

        var hashedEmailAddress = string.IsNullOrEmpty(subscriberEmail) ? "" : CalculateMD5Hash(subscriberEmail.ToLower());
        var uri = string.Format("https://{0}.api.mailchimp.com/3.0/lists/{1}/members/{2}", dataCenter, listId, hashedEmailAddress);
        try
        {
            using (var webClient = new WebClient())
            {
                webClient.Headers.Add("Accept", "application/json");
                webClient.Headers.Add("Authorization", "apikey " + apiKey);

                return webClient.UploadString(uri, "PUT", sampleListMember);
            }
        }
        catch (WebException we)
        {
            using (var sr = new StreamReader(we.Response.GetResponseStream()))
            {
                return sr.ReadToEnd();
            }
        }
    }

    private static string CalculateMD5Hash(string input)
    {
        // Step 1, calculate MD5 hash from input.
        var md5 = System.Security.Cryptography.MD5.Create();
        byte[] inputBytes = System.Text.Encoding.ASCII.GetBytes(input);
        byte[] hash = md5.ComputeHash(inputBytes);

        // Step 2, convert byte array to hex string.
        var sb = new StringBuilder();
        foreach (var @byte in hash)
        {
            sb.Append(@byte.ToString("X2"));
        }
        return sb.ToString();
    }



    /// <summary>
    /// Method to delete all prospects
    /// </summary>
    /// <param name="ProspectID"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool DeleteProspect(string prospectID)
    {
        ManageService _objService = new ManageService();
        bool result = true;

        try
        {
            _objService.DeleteProspectAll(prospectID);
        }

        catch (System.Exception ex)
        {
            result = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            _objService = null;
        }
        return result;

    }


    //function to populate time in listbox from the currentdattime with the interval of 30 mins.
    private void PopulateTime()
    {
        int newInterval = 30;
        DateTime beginTime = DateTime.Parse(DateTime.Now.ToShortDateString());
        string minsAfterHour = beginTime.ToShortTimeString().Split(':')[1];
        DateTime currentTime = DateTime.Parse(beginTime.ToShortDateString() + " 12:" + minsAfterHour.Replace("PM", "AM")).AddMinutes(newInterval);
        bool selectedOne = false;
        drpEndTime.Items.Clear();
        drpStartTime.ClearSelection();

        //while (currentTime < beginTime.AddDays(1))
        for (int i = 0; currentTime < beginTime.AddDays(1); i++)
        {
            ListItem itm = new ListItem(currentTime.ToShortTimeString(), i.ToString());
            drpStartTime.Items.Insert(i, itm);

            drpEndTime.Items.Insert(i, itm);
            if (currentTime.ToShortTimeString() == beginTime.ToShortTimeString() && !selectedOne)
            {
                drpStartTime.SelectedItem.Text = currentTime.ToShortTimeString();
                selectedOne = true;
            }


            currentTime = currentTime.AddMinutes(newInterval);
        }

        drpStartTime.DataBind();
        drpEndTime.DataBind();

    }

    /// <summary>
    /// Method for disable the link
    /// By jaswinder
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdCampaign_RowDataBound(object sender, GridRowEventArgs e)
    {
        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell cell = e.Row.Cells[8] as GridDataControlFieldCell;
            Control linksContainer = cell.Controls[0].Controls[0];

            // check row values to determine whether to show the active or deactive text the Delete links
            if ((e.Row.Cells[9] as GridDataControlFieldCell).Text == "False")
            {
                ((System.Web.UI.WebControls.HyperLink)(linksContainer.Controls[2])).Text = "Active";


            }

        }
    }

    /// <summary>
    /// Method for disable the link
    /// By jaswinder
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdEvent_RowDataBound(object sender, GridRowEventArgs e)
    {
        if (e.Row.RowType == GridRowType.DataRow)
        {
            GridDataControlFieldCell cell = e.Row.Cells[17] as GridDataControlFieldCell;
            Control linksContainer = cell.Controls[0].Controls[0];

            // check row values to determine whether to show the active or deactive text the Delete links
            if ((e.Row.Cells[18] as GridDataControlFieldCell).Text == "False")
            {
                ((System.Web.UI.WebControls.HyperLink)(linksContainer.Controls[2])).Text = "Active";


            }

        }
    }


    //function to Get the provider List 
    [WebMethod]
    public static List<Calendar.Provider> Getproviders()
    {
        List<Calendar.Provider> provList = null;
        try
        {
            provList = Providers.getProviderList().ToList();
        }

        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }

        return provList;

    }

    //function to Get the Appointments List 
    [WebMethod]
    public static List<AppointmentTypeModel> GetAppointmentTypes()
    {
        List<AppointmentTypeModel> AptTypeList = null;
        ManageService _objService = new ManageService();

        try
        {
            AptTypeList = _objService.GetAppointmentTypes().ToList();
        }

        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }

        return AptTypeList;

    }


    /// <summary>
    /// Method to mark the check for duplicate appointment
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateAppointment(int ProviderID, DateTime StartDate, DateTime EndDate)
    {

        string result = string.Empty;
        IManageService _objService = new ManageService();
        try
        {
            result = _objService.CheckDuplicateAppointment(ProviderID, StartDate, EndDate);
        }
        catch (System.Exception ex)
        {
            result = "false";
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            _objService = null;
        }

        return result;

    }

    /// <summary>
    /// Method to mark the event associated with particualar prospect as attended
    /// </summary>
    /// <param name="data"></param>
    /// <returns></returns>
    [WebMethod]
    public static bool SaveNewAppointmentandPatient(int ProviderID, int ApptID, int ProspectID, string Clinic, int StaffID, int EventID, DateTime StartDate, DateTime EndDate)
    {

        bool result;
        IManageService _objService = new ManageService();
        try
        {
            int PatientID = 0;

            result = _objService.SaveAppoint_Patient(PatientID, ApptID, ProspectID, Clinic, StaffID, EventID, ProviderID, StartDate, EndDate);
        }
        catch (System.Exception ex)
        {
            result = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            _objService = null;
        }

        return result;

    }

    #endregion

    [WebMethod]
    public static bool SaveNewFollowupandPatient(int ProspectID, string Clinic, int StaffID, int EventID)
    {
        bool result;
        IManageService _objService = new ManageService();
        try
        {
            int PatientID = 0;

            result = _objService.SaveFollowup_Patient(ProspectID, Clinic, StaffID, EventID);
            // HttpContext.Current.Response.Redirect("~/XeroAuthonticationCall.ashx?checkedMerchantiDS=" + PatientID.ToString(), false);

        }
        catch (System.Exception ex)
        {
            result = false;
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

        }
        finally
        {
            _objService = null;
        }

        return result;
    }

    /// <summary>
    /// Deleteing Attendent data from grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    [WebMethod]
    public static string btnDeleteAttendedRecord(int eventID, int prospectID)
    {
        string Result = "";
        ManageService _objService = new ManageService();

        //
        //Calling SP to delete prospects record if pateintid is null
        Result = _objService.DeleteRecordAttendee(eventID, prospectID);
        //
        //Returning the status of the deletion
        return Result;
    }

    protected void Autoship_ActiveTabChanged(object sender, EventArgs e)
    {
        _objService = new ManageService();
        string tabName = Autoship.ActiveTab.ID;
        IEnumerable MarketingSource;
        IEnumerable Events;
        switch (tabName)
        {
            case "ManageProspects":
                DropDownList ddlEvents1 = grdProspect.Templates[0].Container.FindControl("cboEvents") as DropDownList;
                Events =
                    _objService.GetAllEvents();



                if (ddlEvents1 != null)
                {
                    //fill Event dropdown with all events data
                    ddlEvents1.DataSource = Events;
                    ddlEvents1.DataBind();
                    ddlEvents1.Items.Insert(0, new ListItem("--Select Event--", "0"));
                }

                grdProspect.DataSource = _objService.GetAllProspect();
                grdProspect.DataBind();
                break;

            case "Activity":

                ddlCampaign.DataSource = _objService.GetAllCampaign();
                ddlCampaign.DataBind();
                ddlCampaign.Items.Insert(0, new ListItem("Select Campaign", "0"));

                MarketingSource = _objService.GetAllMarketingSource();

                DropDownList ddlSource1 = grdMarketingActivity.Templates[0].Container.FindControl("ddlSource1") as DropDownList;
                if (ddlSource1 != null)
                {
                    ddlSource1.DataSource = MarketingSource;
                    ddlSource1.DataBind();
                }

                grdMarketingActivity.ClearPreviousDataSource();
                grdMarketingActivity.DataSource = null;
                grdMarketingActivity.DataBind();

                break;

            case "Seminars":
                Events = _objService.GetAllEvents();
                ddlEvent.DataSource = Events;
                ddlEvent.DataBind();
                ddlEvent.Items.Insert(0, new ListItem("Select Event", "0"));
                lblEventName.Text = "";
                grdAttend.ClearPreviousDataSource();
                grdAttend.DataSource = null;
                grdAttend.DataBind();

                break;
        }

    }

    protected void btnSumit_Click(object sender, EventArgs e)
    {
        IEmailTemplateService objTemplateService = new EmailTemplateService();
        bool IsActive = true;
        if (chkIsActive.Checked == false)
            IsActive = false;
        objTemplateService.SaveCRMEmailTemplate(edContent.Content, txtCRMWufooFormLink.Text, IsActive);
    }

    protected void btnUserName_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {UserName}";
    }
    protected void btnURL_Click(object sender, EventArgs e)
    {
        edContent.Content = edContent.Content + " {Url}";
    }

    [WebMethod]
    public static bool saveProspect(int EventID, int StaffID, string FirstName, string LastName, string Phone, string Email)
    {
        bool Result = true;
        ManageService _objService = new ManageService();

        try
        {

            _objService.InsertUpdateProspect(
                0,
                "",
                "",
               "",
                "",
                Email,
                FirstName,
                false,
                LastName,
               Phone,
               "",
               "",
               "",
                4,
                "",
                StaffID,
                EventID
                );
        }
        catch
        {
            Result = false;
        }
        return Result;
    }

}
