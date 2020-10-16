using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using System.Configuration;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;


public partial class admin_labSchedule : LMCBase
{
    #region "Variables"

    ILabScehduleService objServiceLabScehdule = null;
    #endregion
    #region "Event"

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            BindLabGroup();
            BindLabTest();
        }

    }
    /// <summary>
    /// group management grid update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdGroup_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        LabScehduleGroupsViewModel objViewModel = null;
        try
        {
            objViewModel = new LabScehduleGroupsViewModel();
            objServiceLabScehdule = new LabScehduleService();

            if (e.Record["GroupID"].ToString() != "")
            {
                objViewModel = objServiceLabScehdule.GetLabScehduleGroupListByGroupId(int.Parse(e.Record["GroupID"].ToString()));
            }

            objViewModel.GroupName = e.Record["GroupName"].ToString();
            objViewModel.Male = e.Record["Male"].ToString() == "0" ? null : (int?)int.Parse(e.Record["Male"].ToString());
            objViewModel.Female = e.Record["Female"].ToString() == "0" ? null : (int?)int.Parse(e.Record["Female"].ToString());
            objViewModel.SearchText = "none";
            objViewModel.FullTestName = "none";
            if (e.Record["GroupID"].ToString() == "")
            {
                objServiceLabScehdule.InsertLabScehduleGroups(objViewModel);
            }
            else
            {
                objServiceLabScehdule = new LabScehduleService();
                objServiceLabScehdule.UpdateLabScehduleGroups(objViewModel);
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objViewModel = null;
            objServiceLabScehdule = null;
        }
    }
    /// <summary>
    /// rebind group management grid on reload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdGroup_Rebind(object sender, EventArgs e)
    {
        BindLabGroup();
    }
    /// <summary>
    /// test management grid update
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTest_UpdateInsert(object sender, GridRecordEventArgs e)
    {
        LabScheduleTestsViewModel objViewModel = null;
        try
        {
            objViewModel = new LabScheduleTestsViewModel();
            objServiceLabScehdule = new LabScehduleService();

            if (e.Record["TestID"].ToString() != "")
            { objViewModel = objServiceLabScehdule.GetLabScehduleTestListByTestId(int.Parse(e.Record["TestID"].ToString())); }
            objViewModel.TestName = e.Record["TestName"].ToString();
            objViewModel.GroupID = int.Parse(e.Record["GroupID"].ToString());
            if (e.Record["TestID"].ToString() == "")
            {
                objServiceLabScehdule.InsertLabScehduleTest(objViewModel);

            }
            else
            {
                objServiceLabScehdule = new LabScehduleService();
                objServiceLabScehdule.UpdateLabScehduleTest(objViewModel);
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objViewModel = null;
            objServiceLabScehdule = null;
        }
    }
    /// <summary>
    /// rebind test management grid on reload
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdTest_Rebind(object sender, EventArgs e)
    {
        BindLabTest();
    }
    #endregion
    #region "Methods"
    #region "Method to get lab schedule"
    /// <summary>
    /// binding grid with lab schedule details
    /// </summary>
    private void BindLabGroup()
    {
        List<LabScehduleGroupsViewModel> lstLabScehduleGroups = null;
        try
        {
            lstLabScehduleGroups = new List<LabScehduleGroupsViewModel>();
            objServiceLabScehdule = new LabScehduleService();
            lstLabScehduleGroups = objServiceLabScehdule.GetLabScehduleGroupsDetails();

            grdGroup.DataSource = lstLabScehduleGroups;
            grdGroup.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstLabScehduleGroups = null;
            objServiceLabScehdule = null;
        }
    }
    #endregion
    #region "Method to get lab test schedule"
    /// <summary>
    /// binding grid with lab  test schedule details
    /// </summary>
    private void BindLabTest()
    {
        List<LabScheduleTestsViewModel> lstLabScehduleTest = null;
        try
        {
            lstLabScehduleTest = new List<LabScheduleTestsViewModel>();
            objServiceLabScehdule = new LabScehduleService();
            lstLabScehduleTest = objServiceLabScehdule.GetLabScehduleTestDetails();

            grdTest.DataSource = lstLabScehduleTest;
            grdTest.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstLabScehduleTest = null;
            objServiceLabScehdule = null;
        }
    }
    #endregion
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

    /// <summary>
    /// method for delete the records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteRecord(object sender, GridRecordEventArgs e)
    {
        try
        {
            objServiceLabScehdule = new LabScehduleService();
            if (!string.IsNullOrEmpty(e.Record["GroupID"].ToString()))
                objServiceLabScehdule.DeleteLabScehduleGroups(int.Parse(e.Record["GroupID"].ToString()));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServiceLabScehdule = null;
        }
    }

    /// <summary>
    /// method for delete the records
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteLabTest(object sender, GridRecordEventArgs e)
    {
        try
        {
            objServiceLabScehdule = new LabScehduleService();
            if (!string.IsNullOrEmpty(e.Record["TestID"].ToString()))
                objServiceLabScehdule.DeleteLabScheduleTests(int.Parse(e.Record["TestID"].ToString()));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objServiceLabScehdule = null;
        }
    }

    

}