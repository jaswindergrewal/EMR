using System;
using System.Collections;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;

public partial class admin_match_labs : LMCBase
{
    #region Variable
    IAdminMatchLabService objService = null;
    #endregion

    #region Event
    /// <summary>
    /// bind grid on load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            GetYears();

        }
        //PopulateGrid();
    }

    protected void ddAcdYear_SelectedIndexChanged(object sender, EventArgs e)
    {
        PopulateGrid();
    }

    protected void btnProcessSelected_Click(object sender, EventArgs e)
    {
        try
        {
            foreach (Hashtable row in grdMatchLabs.SelectedRecords)
            {

                objService = new AdminMatchLabService();
                objService.EditMactLab(int.Parse(row["Followup_ID"].ToString()), int.Parse(row["AppointmentID"].ToString()));
            }
            PopulateGrid();
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

    #region Method
    /// <summary>
    /// bind grid
    /// </summary>
    private void PopulateGrid()
    {
        try
        {
            if (ddAcdYear.SelectedItem.Text.ToLower() != "-year-")
            {
                objService = new AdminMatchLabService();

                grdMatchLabs.DataSource = objService.GetMatchLabsList(ddAcdYear.SelectedItem.Text);
                grdMatchLabs.DataBind();
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


    //populate year dropdown from 2003
    public void GetYears()
    {
        try
        {
            int Year = 2003;
            int fromyear = Year;
            ddAcdYear.Items.Clear();
            ddAcdYear.Items.Add("-Year-");
            for (int j = Year; j <= DateTime.Now.Year; j++)
            {

                ListItem fros = new ListItem(fromyear.ToString());

                ddAcdYear.Items.Add(fros);
                fromyear = j + 1;

            }
        }
        catch (System.Exception ex)
        {

        }
    }

    #endregion

}

