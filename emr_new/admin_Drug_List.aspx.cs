using System;
using System.Web;
using System.Web.UI;
using Obout.Grid;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class admin_Drug_List : LMCBase
{
    #region Variables
    IAdminDrugListService objService = null;
    #endregion

    #region Events
    
    protected void Page_Load(object sender, EventArgs e)
    {

    }



    /// <summary>
    /// update Record in grid by click on the edit button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateRecord(object sender, GridRecordEventArgs e)
    {

        try
        {
            if (e.Record["DrugName"].ToString() == "")
            {

                lblError.Text = "Drug Name can't be blank";

            }
            else
            {

                if (e.Record["DrugID"] != null && e.Record["DrugID"] != "")
                {
                    objService = new AdminDrugListService();
                    objService.UpdateAdminDrugList(int.Parse(e.Record["DrugID"].ToString()),
                                                    e.Record["DrugName"].ToString(), e.Record["Viewable_yn"].ToString(),
                                                    e.Record["Gender"].ToString(), e.Record["Supplement_yn"].ToString(),
                                                    e.Record["Reviewed"].ToString());

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
            objService = null;
        }

    }
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
            isExist = _objService.CheckDuplicateRecords(Convert.ToInt32(ID), Name.Trim(), tableName);
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
    /// method for delete the Drug item
    /// created by: Deepak Thakur[14.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteDrug(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new AdminDrugListService();
            if (!string.IsNullOrEmpty(e.Record["DrugID"].ToString()))
                objService.DeleteDrug(int.Parse(e.Record["DrugID"].ToString()));
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
}

