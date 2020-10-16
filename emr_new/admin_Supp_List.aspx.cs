using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Obout.Grid;
using Emrdev.ServiceLayer;
using System.Data;
using System.Web.Services;

public partial class admin_Supp_List : LMCBase
{
    #region Variable
    IAdminSuppListService objService = null;
    #endregion

    #region Events
    /// <summary>
    /// fill Grid with Supp list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            BindSupplimentList();
        }

    }

    /// <summary>
    /// Get the suppliment list
    /// </summary>
    private void BindSupplimentList()
    {
        try
        {
            objService = new AdminSuppListService();
            Grid1.DataSource = objService.GetAllSuppList();
            Grid1.DataBind();
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
    /// update record for suppliers
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void UpdateRecord(object sender, GridRecordEventArgs e)
    {
        if (e.Record["ProductName"].ToString() == "")
        {
            lblError.Text = "ProductName can't be blank";
        }
        else
        {
            if (e.Record["ProductID"] != "" && e.Record["ProductID"] != null)
            {
                objService = new AdminSuppListService();
                objService.UpdateAdminSuppList(int.Parse(e.Record["ProductID"].ToString()), e.Record["ProductName"].ToString().Trim(), e.Record["Viewable"].ToString(), e.Record["Reviewed"].ToString());
                BindSupplimentList();
            }
        }
    }
    #endregion

    /// <summary>
    /// this method is using for check the duplicate test during Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 08.Aug.2013        
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
            isExist = _objService.CheckDuplicateRecords(Convert.ToInt32(ID), HttpContext.Current.Server.HtmlDecode(Name).Trim(), tableName);
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
    /// method for delete the Suppliment records
    /// created by: Deepak Thakur[13.August.2013]
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void DeleteSupplimentList(object sender, GridRecordEventArgs e)
    {
        try
        {
            objService = new AdminSuppListService();
            if (!string.IsNullOrEmpty(e.Record["ProductID"].ToString()))
                objService.DeleteSupplimentList(int.Parse(e.Record["ProductID"].ToString()));
            BindSupplimentList();
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