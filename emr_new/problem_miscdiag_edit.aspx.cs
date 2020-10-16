using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

/// <summary>
/// Conversion of asp to aspx
/// Jaswinder 2nd sept 2013
/// </summary>
/// 
public partial class problem_miscdiag_edit : LMCBase
{
    #region "Variables"
    IProblemListService objService = null;
    DiagnosisListViewModel Diag = null;
    #endregion

    #region "Events"

    /// <summary>
    /// Load the master page on the basis of query string
    /// Jaswinder 23 Jan 2014
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/sub.master";
    }

    /// <summary>
    /// Get the problemMiscdiagnosis data on the basis of diagnosis id
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["ProbDiagID"] != null)
                {
                    objService = new ProblemListService();
                    Diag = objService.GetProblemMiscDiagnosisList(int.Parse(Request.QueryString["ProbDiagID"]));
                    if (Diag != null)
                    {
                        ddlPri.SelectedValue = Diag.Priority_num.ToString();
                        ddlSev.SelectedValue = Diag.Severity_num.ToString();
                        lblDiagnosis.Text = Diag.Diag_Title;
                    }
                }
                else
                {
                    Response.Redirect("landingpage.aspx", false);
                }
            }
            catch (System.Exception ex)
            {
                Elmah.ErrorSignal.FromCurrentContext().Raise(ex);

            }
            finally
            {
                objService = null;
                Diag = null;
            }
        }
    }


    /// <summary>
    /// Update the Servity and priority of the problem Misc diagnosis.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new ProblemListService();
            objService.UpdateProblemMiscDiagList(int.Parse(Request.QueryString["ProbDiagID"]), decimal.Parse(ddlPri.SelectedValue), decimal.Parse(ddlSev.SelectedValue));
            string MasterPage = "~/sub.master";
            if (Request.QueryString["MasterPage"] != null)
            {
                MasterPage = Request.QueryString["MasterPage"];
            }
            if (MasterPage == "~/site.master")
            {
                Response.Redirect("apt_console.aspx?patientid=" + Request.QueryString["patientid"] + "&MasterPage=~/site.master&aptid=" + Request.QueryString["AptID"], false);
            }
            else
            {
                Response.Redirect("ProblemList.aspx?patientid=" + Request.QueryString["patientid"] + "&MasterPage=~/sub.master", false);
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

    #endregion
}