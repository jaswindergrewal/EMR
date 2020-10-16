using System;
using System.Web.Services;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;

//Page created by jaswinder on april 11 2014
public partial class Admin_CrmCampign : System.Web.UI.Page
{
    #region "Variable"
    IManageService _objService = new ManageService();
    CRM_CampaignType_ViewModel CampaignModel = null;
    #endregion
    
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindCampaignType();
        }
    }

    //Method to insert and update record
    protected void grdCampaignType_UpdateInsert(object sender, GridRecordEventArgs e)
    {

        int CampaignID = 0;
        bool Active_YN = false;
        try
        {
            if (e.Record["CampaignID"].ToString() != "")
            {
                CampaignID = int.Parse(e.Record["CampaignID"].ToString());
            }

            if (e.Record["IsActive"].ToString() != "")
            {
                Active_YN = bool.Parse(e.Record["IsActive"].ToString());
            }
            CampaignModel = new CRM_CampaignType_ViewModel();
            CampaignModel.CampaignID = CampaignID;
            CampaignModel.CampaignType = e.Record["CampaignType"].ToString();
            CampaignModel.IsActive = Active_YN;

            _objService = new ManageService();

            _objService.InsertUpdateCampaignType(CampaignModel);
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

    //method to rebind the grid after the record inserted
    protected void grdCampaignType_Rebind(object sender, EventArgs e)
    {
        BindCampaignType();
    }

    //Method for geting the records for the campign type
    private void BindCampaignType()
    {
        try
        {
            _objService = new ManageService();
            grdCampaignType.DataSource = _objService.GetAllactiveCampaignType(false);
            grdCampaignType.DataBind();
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

    //Method to check the duplicate name for type while inserting /updating data
    [WebMethod]
    public static string CheckDuplicateType(string ID, string Name)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        IManageService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(ID))
                ID = "0";
            _objService = new ManageService();
            isExist = _objService.CheckDuplicateCampaignType(Convert.ToInt32(ID), Name);
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