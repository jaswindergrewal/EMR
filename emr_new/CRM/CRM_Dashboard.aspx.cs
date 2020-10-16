using System;
using System.Collections.Generic;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class CRM_CRM_Dashboard :LMCBase
{
    ICRMdashboardService objService = new CRMdashboardService();
    IManageService objServiceManage = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        //function to get dashboard statistics
        CRMStatisticViewModel objStatsModel = null;
            
        objStatsModel = objService.GetCRMStatisticData();
        if (objStatsModel != null)
        {
            lblLeftAttendant.InnerText=objStatsModel.ProspectAttended.ToString();
            lblLeftConverted.InnerText = objStatsModel.PatientConverted.ToString();
            lblLeftMedStart.InnerText = objStatsModel.MedStartPatient.ToString();
            lblLeftTotal.InnerText = objStatsModel.Total.ToString();
        }

        //Fill Campaign Dropdown
        objServiceManage=new ManageService();
        ddlCampaign.DataSource = objServiceManage.GetAllCampaign();
        ddlCampaign.DataBind();

        objServiceManage=new ManageService();
        ddlEvent.DataSource = objServiceManage.GetAllEvents();
        ddlEvent.DataBind();

        
    
    }

    
    

    
    //Get the data for the Campaign Bar graph
    [System.Web.Services.WebMethod]
    public static List<PlottGraphViewModel> PlotCampaignGraph(int campaignID)
    {
        List<PlottGraphViewModel> isflag = null ;
        ICRMdashboardService objService = new CRMdashboardService();
        isflag = objService.GetCampaignGraph(campaignID);
        
        return isflag;
    }


    //Get the data for the Event graph
    [System.Web.Services.WebMethod]
    public static List<CRMStatisticViewModel> PlotEventGraph(int eventID)
    {
        List<CRMStatisticViewModel> isflag = null;
        ICRMdashboardService objService = new CRMdashboardService();
        isflag = objService.GetEventGraph(eventID);

        return isflag;
    }




    protected void btnManageProspect_Click(object sender, EventArgs e)
    {
        Response.Redirect("Manage.aspx");
    }
    protected void btnNewProspect_Click(object sender, EventArgs e)
    {
        Response.Redirect("AddProspect.aspx");
    }
}