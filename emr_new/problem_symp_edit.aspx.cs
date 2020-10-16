﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class problem_symp_edit : LMCBase
{
    #region "Variables"
    IProblemListService objService = null;
    ProblemSymptEditViewModel sympt = null;
    #endregion

    #region "events"

    /// <summary>
    /// Get the symptom details by problem symptomid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {
                if (Request.QueryString["probsymptid"] != null)
                {
                    objService = new ProblemListService();
                    sympt = objService.GetProblemSymtomsList(int.Parse(Request.QueryString["probsymptid"]));

                    ddlPri.SelectedValue = sympt.Priority_num.ToString();
                    ddlSev.SelectedValue = sympt.Severity_num.ToString();
                    ddlTrend.SelectedValue = sympt.Dir;
                    lblSymptom.Text = sympt.SymptomName;
                }
                else{
                    Response.Redirect("landingpage.aspx",true);
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
                sympt = null;
            }
        }
    }

    /// <summary>
    /// Update the problem symptomid
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            objService = new ProblemListService();
            objService.UpdateProblemSymtomsList(int.Parse(Request.QueryString["probsymptid"]), decimal.Parse(ddlPri.SelectedValue), decimal.Parse(ddlSev.SelectedValue), ddlTrend.SelectedValue);
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
        Response.Redirect("ProblemList.aspx?patientid=" + Request.QueryString["patientid"] + "&MasterPage=~/sub.master");

    }

    #endregion
}