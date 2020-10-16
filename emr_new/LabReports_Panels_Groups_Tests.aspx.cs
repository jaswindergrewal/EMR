using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;


public partial class LabReports_Panels_Groups_Tests : System.Web.UI.Page
{
    IAdminLabImport objServiceAdminLabImport = null;

    /// <summary>
    /// Show panal list at the time of page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetPanels();
        }

    }

    /// <summary>
    /// function to get the list of panels
    /// </summary>
    public void GetPanels()
    {
        objServiceAdminLabImport = new AdminLabImport();
        List<LabReportViewModel> panelList = objServiceAdminLabImport.GetLabReport_PanelsGroupsTestsList();
        if (panelList != null)
        {
            foreach (var item in panelList)
            {
                PanelsRadioButtonList.DataSource = panelList;
                PanelsRadioButtonList.DataTextField = "PanelName";
                PanelsRadioButtonList.DataValueField = "PanelID";
                PanelsRadioButtonList.DataBind();
            }
        }
    }

    /// <summary>
    /// Add the panel in the database
    /// </summary>
    /// <param name="PanelName"></param>
    /// <param name="SortOrder"></param>
    /// <param name="PanelDescrip"></param>
    /// <returns></returns>
    [WebMethod]
    public static int AddPanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip)
    {
        IAdminLabImport objServiceAdminLabImport = null;
        int result;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.AddPanels(PanelName, PanelDescrip, SortOrder);

        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }


    /// <summary>
    /// Get the details of panel by panel id
    /// </summary>
    /// <param name="PanelID"></param>
    /// <returns></returns>
    [WebMethod]
    public static LabReportsPanelViewModel GetPanelDetails(int PanelID)
    {
        IAdminLabImport objServiceAdminLabImport = null;
        var panelList = new LabReportsPanelViewModel();
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            panelList = objServiceAdminLabImport.GetPanelDetails(PanelID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return panelList;
    }

    /// <summary>
    /// Update the details of panel on the basis of panel id
    /// </summary>
    /// <param name="PanelName"></param>
    /// <param name="SortOrder"></param>
    /// <param name="PanelDescrip"></param>
    /// <param name="PanelID"></param>
    /// <returns></returns>
    [WebMethod]
    public static int UpdatePanels(string PanelName, Nullable<int> SortOrder, string PanelDescrip, int PanelID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.UpdatePanels(PanelName, SortOrder, PanelDescrip, PanelID);
           
        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }


    /// <summary>
    /// To get both triggers and groups on the basis of selected panel id
    /// </summary>
    /// <param name="panelID"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<LabReportViewModel> GetGroupsTriggers(int panelID)
    {
        IAdminLabImport objServiceAdminLabImport = null;
        List<LabReportViewModel> GroupTestView = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            GroupTestView = objServiceAdminLabImport.GetGroupsTriggersByPanelID(panelID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return GroupTestView;
    }

   
    /// <summary>
    /// Add the group detail data
    /// </summary>
    
    [WebMethod]
    public static int AddGroups(string GroupName, string GroupTitle, int SortOrder, int ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            bool ShowGraphChart = false;
            if (ShowGraph == 1)
                ShowGraphChart = true;
            result = objServiceAdminLabImport.AddGroups(GroupName, GroupTitle, SortOrder, ShowGraphChart, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);
        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }


    /// <summary>
    /// Get group detail by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
   
    [WebMethod]
    public static LabReportGroupViewModel GetGroupByID(int ID)
    {
        IAdminLabImport objServiceAdminLabImport = null;

        var groupList = new LabReportGroupViewModel();
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            groupList = objServiceAdminLabImport.GetGroupByID(ID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return groupList;
    }


    //Update group details
    [WebMethod]
    public static int UpdateGroups(int ID, string GroupName, string GroupTitle, int SortOrder, int ShowGraph, decimal ChartBottom, decimal MaleLongevityHigh, decimal MaleLongevityLow, Nullable<decimal> FemaleLongevityHigh, Nullable<decimal> FemaleLongevityLow, Nullable<decimal> MaleAcceptableHigh, Nullable<decimal> MaleAcceptableLow, Nullable<decimal> FemaleAcceptableHigh, Nullable<decimal> FemaleAcceptableLow, string Description, string MaleHighTxt, string MaleLowTxt, string MaleNormalTxt, string FemHighTxt, string FemLowTxt, string FemNormalTxt, int PanelID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {

            objServiceAdminLabImport = new AdminLabImport();

            bool ShowGraphChart = false;
            if (ShowGraph == 1)
                ShowGraphChart = true;
            result = objServiceAdminLabImport.UpdateGroups(ID, GroupName, GroupTitle, SortOrder, ShowGraphChart, ChartBottom, MaleLongevityHigh, MaleLongevityLow, FemaleLongevityHigh, FemaleLongevityLow, MaleAcceptableHigh, MaleAcceptableLow, FemaleAcceptableHigh, FemaleAcceptableLow, Description, MaleHighTxt, MaleLowTxt, MaleNormalTxt, FemHighTxt, FemLowTxt, FemNormalTxt, PanelID);


        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }

    /// <summary>
    /// Add trigger data
    /// </summary>
    /// <param name="triggerName"></param>
    /// <param name="triggerDesc"></param>
    /// <param name="panelID"></param>
    /// <returns></returns>
    [WebMethod]
    public static int AddTriggers(string triggerName, string triggerDesc, int panelID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {

            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.AddTriggers(triggerName, triggerDesc, panelID);

        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }

    /// <summary>
    /// Get trigger details by ID
    /// </summary>
    /// <param name="ID"></param>
    /// <returns></returns>
    [WebMethod]
    public static LabReportTriggerModel GetTriggerByID(int ID)
    {
        IAdminLabImport objServiceAdminLabImport = null;

        var triggerList = new LabReportTriggerModel();
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            triggerList = objServiceAdminLabImport.GetTriggerByID(ID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return triggerList;
    }

    /// <summary>
    /// Update trigger details
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="triggerName"></param>
    /// <param name="triggerDesc"></param>
    /// <param name="panelID"></param>
    /// <returns></returns>
    [WebMethod]
    public static int UpdateTriggers(int ID, string triggerName, string triggerDesc, int panelID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.UpdateTriggers(ID, triggerName, triggerDesc, panelID);
        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }

    
    /// <summary>
    /// Get the test names thar are associated with the group
    /// </summary>
    /// <param name="groupID"></param>
    /// <returns></returns>
    [WebMethod]
    public static List<LabReportTestModel> GetTests(int groupID)
    {
        IAdminLabImport objServiceAdminLabImport = null;
        List<LabReportTestModel> GroupTestView = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            GroupTestView = objServiceAdminLabImport.GetTestByGroupID(groupID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return GroupTestView;
    }




    /// <summary>
    /// Method to delete the lab data on the basis of Name supplied .
    /// </summary>
    /// <param name="ID"></param>
    /// <param name="Name"></param>
    [WebMethod]
    public static void DeleteLabData(int ID, string Name)
    {

        IAdminLabImport objServiceAdminLabImport = null;
        
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            objServiceAdminLabImport.DeletePanel(ID, Name);
        }
        catch (System.Exception ex)
        {

        }
        finally
        {
            objServiceAdminLabImport = null;
          
        }


    }

    /// <summary>
    /// Get test names that are not associated with any group and are not hide
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static List<LabReportTestModeltest> GetTestNOTINGroups()
    {

        IAdminLabImport objServiceAdminLabImport = null;
        List<LabReportTestModeltest> GroupTestView = null;
        try
        {
           objServiceAdminLabImport = new AdminLabImport();
           GroupTestView= objServiceAdminLabImport.GetTestNOTINGroups();
        }
        catch (System.Exception ex)
        {

        }
        finally
        {
            objServiceAdminLabImport = null;
           
        }
        return GroupTestView;

    } 

    [WebMethod]

    public static List<LabReportTestModel> InsertTestDetailsForGroup(int GroupID, string TestIDs, int Hide)
    {

        IAdminLabImport objServiceAdminLabImport = null;
        List<LabReportTestModel> GroupTestView = null;
        
        try
        {
           objServiceAdminLabImport = new AdminLabImport();
           GroupTestView = objServiceAdminLabImport.InsertTestDetailsForGroup(GroupID, TestIDs, Hide);
        }
        catch (System.Exception ex)
        {

        }
        finally
        {
            objServiceAdminLabImport = null;
           
        }
        return GroupTestView;
    }


    [WebMethod]
    public static int AddCondition(string ConditionName, string ConditionDescrip, string Sex, int TriggerID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {

            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.AddCondition(ConditionName, ConditionDescrip, Sex, TriggerID);

        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }

    [WebMethod]
    public static List<LabReportConditionModel> GetCondition(int triggerID)
    {
        IAdminLabImport objServiceAdminLabImport = null;
        List<LabReportConditionModel> GroupConditionView = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            GroupConditionView = objServiceAdminLabImport.GetConditionBytriggerID(triggerID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return GroupConditionView;
    }

    [WebMethod]
    public static LabReportConditionModel GetConditionByID(int ID)
    {
        IAdminLabImport objServiceAdminLabImport = null;

        var conditionList = new LabReportConditionModel();
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            conditionList = objServiceAdminLabImport.GetConditionByID(ID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return conditionList;
    }

    [WebMethod]
    public static int UpdateConditions(int ID, string conditionName, string conditionDesc, string sex, int triggerID)
    {
        int result;
        IAdminLabImport objServiceAdminLabImport = null;
        try
        {
            objServiceAdminLabImport = new AdminLabImport();
            result = objServiceAdminLabImport.UpdateConditions(ID, conditionName, conditionDesc, sex, triggerID);
        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceAdminLabImport = null;
        }
        return result;
    }



    protected void btnReport_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/LabManageReport.aspx");
    }
}