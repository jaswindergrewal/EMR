using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Obout.Grid;
using System;
using System.Web.Services;
using System.Web.UI.WebControls;

//Page created by jaswinder on april 11 2014
public partial class ReportList : System.Web.UI.Page
{
    #region "Variable"
    ILabReportService _objService = new LabReportService();
   
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindReportType();
        }
    }

    //Method to insert and update record
    protected void grdReportList_UpdateInsert(object sender, GridRecordEventArgs e)
    {

        int ReportId = 0;
        bool Active_YN = false;
        try
        {
            if (e.Record["Id"].ToString() != "")
            {
                ReportId = int.Parse(e.Record["Id"].ToString());
            }

            if (e.Record["IsActive"].ToString() != "")
            {
                Active_YN = bool.Parse(e.Record["IsActive"].ToString());
            }
            int ReportTypeId = Convert.ToInt32(drpReportType.SelectedValue);
            ReportListViewModel ReportListViewModel = new ReportListViewModel();
            ReportListViewModel.ReportName = e.Record["ReportName"].ToString(); ;
            ReportListViewModel.Id = ReportId;
            ReportListViewModel.IsActive = Active_YN;
            ReportListViewModel.ReportTypeId = ReportTypeId;

            _objService = new LabReportService();

            _objService.InsertUpdateReportList(ReportListViewModel);
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
    protected void grdReportList_Rebind(object sender, EventArgs e)
    {
        BindReportList();
    }

    private void BindReportType()
    {
        try
        {
            _objService = new LabReportService();
            drpReportType.DataSource = _objService.GetReportType();
            drpReportType.DataTextField = "TypeName";
            drpReportType.DataValueField = "Id";
            drpReportType.DataBind();
            drpReportType.Items.Insert(0, new ListItem("Select Report Type","0"));
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

    //Method for geting the records for the campign type
    private void BindReportList()
    {
        try
        {
            int ReportTypeId = Convert.ToInt32(drpReportType.SelectedValue);
            _objService = new LabReportService();
            grdReportList.DataSource = _objService.GetReportList(ReportTypeId);
            grdReportList.DataBind();
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
    public static string CheckDuplicateReport(string ID, string Name, string ReportTypeId)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        ILabReportService _objService = null;
        try
        {
            if (Convert.ToInt32(ReportTypeId) > 0)
            {
                if (string.IsNullOrEmpty(ID))
                    ID = "0";
                _objService = new LabReportService();
                isExist = _objService.CheckDuplicateReport(Convert.ToInt32(ID), Name, Convert.ToInt32(ReportTypeId));
                if (isExist == true)
                    strDuplicate = "duplicate";
                else
                    strDuplicate = string.Empty;
            }
            else
            {
                strDuplicate = "Select Report Type";
            }
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



    protected void drpReportType_SelectedIndexChanged(object sender, EventArgs e)
    {
        BindReportList();
    }

   
}