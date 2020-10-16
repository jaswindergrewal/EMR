using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Emrdev.ServiceLayer;
using System.Collections;
using Emrdev.ViewModelLayer;

public partial class ProblemList : LMCBase
{
    #region Variable

    IProblemListService objService = null;
    protected int PatientID = 0;
    int result = 0;
    #endregion

    #region Events
    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "~/site.master";

    }

    /// <summary>
    /// binding Diagnoses,Symptoms,Diagnoses handled by 3rd party ,Priority,Severity dropdown lists on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        objService = new ProblemListService();
        try
        {

            if (Request.QueryString["patientid"] != null)
            {
                PatientID = int.Parse(Request.QueryString["patientid"]);
                if (!IsPostBack)
                {
                    IEnumerable DiagList = objService.GetDiagnosisList();

                    ddlDiagnoses.DataSource = DiagList;
                    ddlDiagnoses.DataTextField = "Text";
                    ddlDiagnoses.DataValueField = "Value";
                    ddlDiagnoses.DataBind();

                    ddlMisc.DataSource = DiagList;
                    ddlMisc.DataTextField = "Text";
                    ddlMisc.DataValueField = "Value";
                    ddlMisc.DataBind();


                    ddlSymptom.DataSource = objService.GetSymptomList();
                    ddlSymptom.DataTextField = "Text";
                    ddlSymptom.DataValueField = "Value";
                    ddlSymptom.DataBind();

                    BindDiagnosis();
                    BindSymptoms();
                    BindMiscDiag();
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

    /// <summary>
    /// binds the diagnosis list of the patient into repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnProblems_Click(object sender, EventArgs e)
    {
        try
        {
            MisDiagnosisListViewModel newProb = new MisDiagnosisListViewModel();
            newProb.DiagnosisID = int.Parse(ddlDiagnoses.SelectedValue);
            newProb.Priority_num = decimal.Parse(Priority.SelectedValue);
            newProb.Severity_num = decimal.Parse(Severity.SelectedValue);
            newProb.PatientID = PatientID;
            newProb.DateEntered = DateTime.Now;
            newProb.Active_YN = true;
            newProb.BeingAddressed_YN = true;
            objService = new ProblemListService();
            result = objService.InsertProblemDiagnosisElements(1, newProb, 0);
            if (result == 1)
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "PopupScript", "DuplicateRecord();", true);

            }
            else
            {
                BindDiagnosis();
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
    /// binds the problem symtoms of the patient into repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSympt_Click(object sender, EventArgs e)
    {
        try
        {
            ProblemSymptInsertListViewModel newProb = new ProblemSymptInsertListViewModel();
            newProb.SymptomID = int.Parse(ddlSymptom.SelectedValue);
            newProb.Priority_num = decimal.Parse(Priority_sym.SelectedValue);
            newProb.Severity_num = decimal.Parse(Severity_sym.SelectedValue);
            newProb.PatientID = PatientID;
            newProb.DateEntered = DateTime.Now;
            newProb.Active_YN = true;
            newProb.BeingAddressed_YN = true;
            objService = new ProblemListService();

            result = objService.InsertProblemSymptoms(newProb);
            if (result == 1)
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "PopupScript", "DuplicateRecord();", true);
            }
            else
            {
                BindSymptoms();
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
    /// binds the miscelleneous symtoms of the patient into repeater
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnMisc_Click(object sender, EventArgs e)
    {
        try
        {
            MisDiagnosisListViewModel newProb = new MisDiagnosisListViewModel();
            newProb.DiagnosisID = int.Parse(ddlMisc.SelectedValue);
            newProb.Priority_num = decimal.Parse(Priority.SelectedValue);
            newProb.Severity_num = decimal.Parse(Severity.SelectedValue);
            newProb.PatientID = PatientID;
            newProb.DateEntered = DateTime.Now;
            newProb.Active_YN = true;
            newProb.BeingAddressed_YN = true;
            objService = new ProblemListService();
            result = objService.InsertProblemDiagnosisElements(2, newProb, 0);
            if (result == 1)
            {
                Page.ClientScript.RegisterStartupScript(typeof(Page), "PopupScript", "DuplicateRecord();", true);
            }
            else
            {
                BindMiscDiag();
            }

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }




    protected void rptProblems_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.DeleteProblemListElements(1, diagID);


        }

        else if (e.CommandName == "Inactive")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListStatus(1, diagID, 0);

        }

        else if (e.CommandName == "Active")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListStatus(1, diagID, 1);

        }

        else if (e.CommandName == "Address")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListAddress(1, diagID);

        }
        BindDiagnosis();

    }
    protected void rptSymptoms_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.DeleteProblemListElements(2, diagID);


        }

        else if (e.CommandName == "Inactive")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListStatus(2, diagID, 0);

        }

        else if (e.CommandName == "Active")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListStatus(2, diagID, 1);

        }

        else if (e.CommandName == "Address")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListAddress(2, diagID);

        }
        BindSymptoms();
    }
    protected void rptMisc_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "delete")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.DeleteProblemListElements(3, diagID);


        }

        else if (e.CommandName == "Inactive")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListStatus(3, diagID, 0);

        }



        else if (e.CommandName == "Address")
        {
            objService = new ProblemListService();
            int diagID = Convert.ToInt16(e.CommandArgument.ToString());
            objService.UpdateProblemListAddress(3, diagID);

        }
        BindMiscDiag();
    }
    #endregion

    #region"Methods"

    public void BindDiagnosis()
    {
        rptProblems.DataSource = objService.GetProblemSDiagnosisListByPatientID(PatientID, 0);
        rptProblems.DataBind();
    }


    public void BindSymptoms()
    {
        rptSymptoms.DataSource = objService.GetProblemSymtomsListByPatientID(PatientID);
        rptSymptoms.DataBind();
    }

    public void BindMiscDiag()
    {
        rptMisc.DataSource = objService.GetMiscDiagListByPatientID(PatientID);
        rptMisc.DataBind();
    }

    #endregion
}