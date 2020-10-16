using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
public partial class admin_south_patients : System.Web.UI.Page
{
    IPatientService objService=null;
    protected int currentPageNumber = 1;
    private const int PAGE_SIZE = 90;
    protected void Page_Load(object sender, EventArgs e)
    {
        
            if (!IsPostBack)
            {
                BindGrid(1);
            }
    }

    /// <summary>
    /// Get the data to bind the grid and also enabled or disabled the button
    /// Jaswinder Aug 9 2012
    /// </summary>
    /// <param name="PageIndex"></param>
    private void BindGrid(int PageIndex)
    {
        try
        {
            objService = new PatientService();

            List<PatientViewModel> lstPatients = objService.GetSouthPatientList(PageIndex, PAGE_SIZE);
            grdSouthPatients.DataSource = lstPatients;
            grdSouthPatients.DataBind();

            //Pageing for grid
            int RecordCount = 0;
            foreach (var data in lstPatients)
            {
                RecordCount = data.RecordCount;
                break;
            }

            double totalRows = (int)RecordCount;

            lblTotalPages.Text = CalculateTotalPages(totalRows).ToString();

            lblCurrentPage.Text = currentPageNumber.ToString();

            if (currentPageNumber == 1)
            {
                Btn_Previous.Enabled = false;

                if (Int32.Parse(lblTotalPages.Text) > 0)
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
    /// Jaswinder 9th aug 2012
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
    /// jaswinder 9th aug 2012
    /// </summary>
    /// <param name="totalRows"></param>
    /// <returns></returns>
    private int CalculateTotalPages(double totalRows)
    {
        int totalPages = (int)Math.Ceiling(totalRows / PAGE_SIZE);
        return totalPages;

    }


}