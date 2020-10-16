using System;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Collections.Generic;
using System.Web.Services;
using System.Linq;
using System.Web.UI.WebControls;

public partial class CRM_admin_CRM_WebUnmachedData : System.Web.UI.Page
{
    ICRMEventsService _objService = null;
    IAddProspectService _objAddProspectService = null;
    IManageService _objManageService = null;
    protected List<ProspectViewmodel> ProspectList = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!IsPostBack)
            {
                _objService = new CRMEventsService();
                ProspectList = _objService.GetProspectDetails();
                grdProspect.DataSource = ProspectList;
                grdProspect.DataBind();

                //JavaScriptSerializer ser = new JavaScriptSerializer();
                //hdnData.Value = ser.Serialize(ProspectList);
                _objAddProspectService = new AddProspectService();
                //TO Fill DropDown With Events
                ddlSeminar.DataSource = _objAddProspectService.GetAllEvents();
                ddlSeminar.DataBind();
                ddlSeminar.Items.Insert(0, new ListItem("--Select--", "0"));

                //TO Fill DropDown With MarketingSources
                _objManageService = new ManageService();
                ddlHowHear.DataSource = _objManageService.GetAllMarketingSource();
                ddlHowHear.DataBind();
                ddlHowHear.Items.Insert(0, new ListItem("--Select--", "0"));
            }
            _objAddProspectService = new AddProspectService();
            //TO Fill DropDown With Events
            ddlSeminar.DataSource = _objAddProspectService.GetAllEvents();
            ddlSeminar.DataBind();
            ddlSeminar.Items.Insert(0, new ListItem("--Select--", "0"));

            //TO Fill DropDown With MarketingSources
            _objManageService = new ManageService();
            ddlHowHear.DataSource = _objManageService.GetAllMarketingSource();
            ddlHowHear.DataBind();
            ddlHowHear.Items.Insert(0, new ListItem("--Select--", "0"));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }


    //Get Detail by Prospect id
    [WebMethod]
    public static ProspectViewmodel ShowData(int ProspectID)
    {
        ProspectViewmodel strData = null;
        CRMEventsService objService = new CRMEventsService();

        try
        {

            strData = objService.GetProspectDetailsByID(ProspectID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }

        return strData;
    }


    // Add the prospect from web site to EMR application CRM prospect table
    [WebMethod]
    public static string AddProspectData(int ProspectID, int ChkEventTrue, string EventName, int EventID, int chkMarketSource, string MarketSourceName, int MarketSourceID, string Email)
    {
        string Comment = string.Empty;
        ICRMEventsService _objService = null;
        DateTime EventDate = DateTime.Now;
        if (ChkEventTrue == 1)
        {
            try
            {
                EventDate = DateTime.Parse(EventName.Split(' ').Last());
            }
            catch
            {
                EventDate = DateTime.Now;
            }

        }

        _objService = new CRMEventsService();
        Comment = _objService.AddProspectData(ProspectID, ChkEventTrue, EventDate, EventName, EventID, chkMarketSource, MarketSourceName, MarketSourceID, Email);
        return Comment;


    }


    //Rebind the obout prospect grid
    protected void grdProspect_Rebind(object sender, EventArgs e)
    {
        _objService = new CRMEventsService();
        ProspectList = _objService.GetProspectDetails();
        grdProspect.DataSource = ProspectList;
        grdProspect.DataBind();
    }
}