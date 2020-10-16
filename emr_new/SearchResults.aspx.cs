using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class SearchResults : LMCBase
{

    #region"variables"
    protected int currentPageNumber = 1;
    ISearchResultService objService = null;
    IPatientService objPatientService = null;
    private string PAGE_SIZE = (ConfigurationManager.AppSettings["PAGE_SIZE"].ToString());

    #endregion

    #region Events
    /// <summary>
    /// To show the Patients on the basis of text enter in search textbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        try
        {
            if (!IsPostBack)
            {
                BindClinic();
                BindGrid(1);

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
    /// <summary>
    /// to search the person who is active user
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        BindGrid(1);
    }

    /// <summary>
    /// To show the message in case there is no data in grid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void rptSearch_ItemDataBound(object sender, RepeaterItemEventArgs e)
    {
        if (rptSearch.Items.Count < 1)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                Label lblFooter = (Label)e.Item.FindControl("lblEmptyData");
                lblFooter.Visible = true;
            }
        }
    }

    #endregion

    #region "Methods"

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// </summary>
    /// <param name="PageIndex"></param>
    private void BindGrid(int PageIndex)
    {
        try
        {
            objService = new SearchResultService();
            bool inactive = false;
            if (ddlStatus.SelectedValue == "InActive")
            {
                inactive = true;
            }

            List<Patient_Details_ViewModel> lstPatients = objService.SearchResult(txtFirstName.Text, txtLastName.Text, txtMiddleInitial.Text, txtHomePhone.Text, ddlClinic.SelectedItem.Text, inactive, currentPageNumber, int.Parse(PAGE_SIZE));
            rptSearch.DataSource = lstPatients;
            rptSearch.DataBind();

            //Paging for grid
            int RecordCount = 0;
            foreach (var data in lstPatients)
            {
                RecordCount = data.RecordCount;
                break;
            }

            double totalRows = (int)RecordCount;


            if (totalRows < 1)
            {
                Btn_Previous.Visible = false;
                Btn_Next.Visible = false;
                lblTotalPages.Text = "";
                lblCurrentPage.Text = "";
                pagingtext.Visible = false;
            }
            else
            {
                Btn_Previous.Visible = true;
                Btn_Next.Visible = true;
                pagingtext.Visible = true;
                lblTotalPages.Text = CalculateTotalPages(totalRows).ToString();

                lblCurrentPage.Text = currentPageNumber.ToString();
                if (currentPageNumber == 1)
                {
                    Btn_Previous.Enabled = false;

                    if (Int32.Parse(lblTotalPages.Text) > 1)
                    {
                        Btn_Next.Enabled = true;
                    }
                    else
                        Btn_Next.Enabled = false;

                }

                else
                {
                    Btn_Previous.Enabled = true;

                    if (currentPageNumber == Int32.Parse(lblTotalPages.Text))
                        Btn_Next.Enabled = false;
                    else Btn_Next.Enabled = true;
                }
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
    /// Method set the current page number on click of previous or next button
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ChangePage(object sender, CommandEventArgs e)
    {
        try
        {
            switch (e.CommandName)
            {
                case "Previous":
                    currentPageNumber = Int32.Parse(lblCurrentPage.Text) - 1;
                    break;

                case "Next":
                    currentPageNumber = Int32.Parse(lblCurrentPage.Text) + 1;
                    break;
            }

            BindGrid(currentPageNumber);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }

    }

    /// <summary>
    /// Get the total number of pages on the the basis of how much record are showing on per page
    /// </summary>
    /// <param name="totalRows"></param>
    /// <returns></returns>
    private int CalculateTotalPages(double totalRows)
    {
        int totalPages = (int)Math.Ceiling(totalRows / int.Parse(PAGE_SIZE));
        return totalPages;

    }

    /// <summary>
    /// Bind the clinic drop down
    /// </summary>
    public void BindClinic()
    {
        try
        {
            objPatientService = new PatientService();
            ddlClinic.DataSource = objPatientService.GetClinics();
            ddlClinic.DataTextField = "ClinicName";
            ddlClinic.DataValueField = "ClinicName";
            ddlClinic.DataBind();
            ddlClinic.Items.Insert(0, new ListItem("All"));

        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPatientService = null;
        }
    }

    #endregion

    
}