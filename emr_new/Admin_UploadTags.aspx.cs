using System;
using System.Web;
using System.Web.UI;
using Obout.Grid;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class Admin_UploadTags : System.Web.UI.Page
{
    #region Variables
    IUploadScanService objService = null;
    #endregion

    #region Events

    /// <summary>
    /// Get the detail list for labs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            objService = new UploadScanService();
            grdtaglist.DataSource = objService.GetTagList(false);
            grdtaglist.DataBind();
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


    /// <summary>
    /// insert- update records in status grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdtaglist_UpdateInsert(object sender, GridRecordEventArgs e)
    {

        int TagId = 0;
        bool Active_YN = false;
        try
        {
            if (e.Record["Id"].ToString() != "")
            {
                TagId = int.Parse(e.Record["Id"].ToString());
            }

            if (e.Record["Disabled"].ToString() != "")
            {
                Active_YN = bool.Parse(e.Record["Disabled"].ToString());
            }
            objService = new UploadScanService();

            objService.InsertUpdateTags(TagId, Active_YN, e.Record["Name"].ToString());
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

    protected void grdtaglist_Rebind(object sender, EventArgs e)
    {
        objService = new UploadScanService();
        bool Disabled = false;
        if (chkDisabled.Checked == true)
        {
            Disabled = true;
        }
        grdtaglist.DataSource = objService.GetTagList(Disabled);
        grdtaglist.DataBind();
    }

    protected void grdtaglist_Delete(object sender, GridRecordEventArgs e)
    {
        int TagId = 0;
        bool Active_YN = false;
        try
        {
            if (e.Record["Id"].ToString() != "")
            {
                TagId = int.Parse(e.Record["Id"].ToString());
            }


            Active_YN = true;

            objService = new UploadScanService();

            objService.InsertUpdateTags(TagId, Active_YN, e.Record["Name"].ToString());
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

    ///// <summary>
    ///// update Record in grid by click on the edit button
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //protected void UpdateRecord(object sender, GridRecordEventArgs e)
    //{

    //    try
    //    {
    //        if (e.Record["Name"].ToString() == "")
    //        {

    //            lblError.Text = "Drug Name can't be blank";

    //        }
    //        else
    //        {

    //            if (e.Record["TagID"] != null && e.Record["TagID"] != "")
    //            {
    //                objService = new AdminDrugListService();
    //                objService.UpdateAdminTagList(int.Parse(e.Record["TagID"].ToString()),
    //                                                e.Record["Name"].ToString(), e.Record["Enabled"].ToString()
    //                                             );

    //            }
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, false);

    //    }
    //    finally
    //    {
    //        objService = null;
    //    }

    //}
    #endregion


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


    //protected void DeleteTag(object sender, GridRecordEventArgs e)
    //{
    //    try
    //    {
    //        objService = new AdminDrugListService();
    //        if (!string.IsNullOrEmpty(e.Record["TagID"].ToString()))
    //            objService.DeleteTag(int.Parse(e.Record["TagID"].ToString()));
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
    //    }
    //    finally
    //    {
    //        objService = null;
    //    }
    //}
    protected void chkDisabled_CheckedChanged(object sender, EventArgs e)
    {
        objService = new UploadScanService();
        bool Disabled = false;
        if (chkDisabled.Checked == true)
        {
            Disabled = true;
        }
        grdtaglist.DataSource = objService.GetTagList(Disabled);
        grdtaglist.DataBind();
    }
}

