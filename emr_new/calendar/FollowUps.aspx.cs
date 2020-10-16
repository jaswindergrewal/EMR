using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Reflection;

public partial class FollowUps : LMCBase
{
    #region "Variables"
    ICalendarService objService = null;
    #endregion

    #region "Events"
    /// <summary>
    /// Get the details for calendar followups
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            if (!IsPostBack)
            {
                string script = "$(document).ready(function () { $('[id*=btnSubmit]').click(); });";
                ClientScript.RegisterStartupScript(this.GetType(), "load", script, true);

                objService = new CalendarService();
                FollowupGrid.DataSource = objService.GetCalendarFollowups();
                FollowupGrid.DataBind();

            }

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    /// on click of this button all the checked followups status will set as complete
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnComplete_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (GridViewRow theRow in FollowupGrid.Rows)
            {
                CheckBox cbComplete = (CheckBox)theRow.Cells[0].Controls[1];
                if (cbComplete.Checked)
                {
                    string apt_id = theRow.Cells[13].Text;
                    //set the followups appoints as complete
                    Calendar.Appointments.FollowupComplete(apt_id);

                    string alertScript = "alert('Followups status completed !')";
                    ScriptManager.RegisterStartupScript(this, GetType(), "Key", alertScript, true);
                }
            }
            objService = new CalendarService();
            System.Threading.Thread.Sleep(3000);
            FollowupGrid.DataSource = objService.GetCalendarFollowups();
            FollowupGrid.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    /// On Change of page index show the next page results
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void FollowupGrid_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        FollowupGrid.PageIndex = e.NewPageIndex;
        objService = new CalendarService();
        FollowupGrid.DataSource = objService.GetCalendarFollowups();
        FollowupGrid.DataBind();
    }



    protected void FollowupGrid_Sorting(object sender, GridViewSortEventArgs e)
    {

        string Sortdir = GetSortDirection(e.SortExpression);
        string SortExp = e.SortExpression;
        objService = new CalendarService();
        var list = objService.GetCalendarFollowups();
        if (Sortdir == "ASC")
        {
            list = Sort<CalendarFollowupViewModel>(list, SortExp, SortDirection.Ascending);
        }
        else
        {
            list = Sort<CalendarFollowupViewModel>(list, SortExp, SortDirection.Descending);
        }
        this.FollowupGrid.DataSource = list;
        this.FollowupGrid.DataBind();

    }
    /// <summary>
    /// GEt Sorting direction
    /// </summary>
    /// <param name="column"></param>
    /// <returns></returns>
    private string GetSortDirection(string column)
    {


        string sortDirection = "ASC";
        string sortExpression = ViewState["SortExpression"] as string;
        if (sortExpression != null)
        {
            if (sortExpression == column)
            {
                string lastDirection = ViewState["SortDirection"] as string;
                if ((lastDirection != null) && (lastDirection == "ASC"))
                {
                    sortDirection = "DESC";
                }
            }
        }
        ViewState["SortDirection"] = sortDirection;
        ViewState["SortExpression"] = column;
        return sortDirection;
    }
    /// <summary>
    /// Sort function
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <param name="list"></param>
    /// <param name="sortBy"></param>
    /// <param name="direction"></param>
    /// <returns></returns>
    public List<CalendarFollowupViewModel> Sort<TKey>(List<CalendarFollowupViewModel> list, string sortBy, SortDirection direction)
    {


        PropertyInfo property = list.GetType().GetGenericArguments()[0].GetProperty(sortBy);
        if (direction == SortDirection.Ascending)
        {
            return list.OrderBy(e => property.GetValue(e, null)).ToList<CalendarFollowupViewModel>();
        }
        else
        {
            return list.OrderByDescending(e => property.GetValue(e, null)).ToList<CalendarFollowupViewModel>();
        }
    }
    protected void FollowupGrid_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[12].Text = "Notes";
        }
    }
}


    #endregion
