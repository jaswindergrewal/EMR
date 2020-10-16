using AjaxControlToolkit;
using Emrdev.AmazonService;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class apt_console : LMCBase
{
    IAppointmentConsole _objAptConsoleService = null;
    IProtocolService _objService = null;
    #region Class Level Properties
    protected int PatientID = 0;
    protected int AptID = 0;
    private int ProviderID = 0;
    int result = 0;
    IProblemListService objService = null;
    ICriticalTaskService objCriticalService = null;
    IPatientVitalsService objPatVitalService = null;
    IAppointmentConsole objIAppointmentConsole = null;


    #endregion

    #region Events
    /// <summary>
    /// get appointment record list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["aptid"] == "" && Request.QueryString["PatientID"] != null && Request.QueryString["PatientID"] != "") Response.Redirect("PatientInfo.aspx?PatientID=" + Request.QueryString["PatientID"]);
        if (Request.QueryString["aptid"] != null) AptID = int.Parse(Request.QueryString["aptid"]);


        _objAptConsoleService = new AppointmentConsole();

        // get the list of Apt_Rec details
        aptDoctorconsoleViewModel thisAppt = _objAptConsoleService.GetAptFordoctorconsole(AptID);

        PatientID = (int)thisAppt.patient_id;

        if (thisAppt.ProviderID != null)
            ProviderID = (int)thisAppt.ProviderID;

        DateTime CorrectDate;

        if (thisAppt.ApptStart == null) CorrectDate = (DateTime)thisAppt.date_entered; else CorrectDate = (DateTime)thisAppt.ApptStart;
        lblAptInfo.Text = thisAppt.FirstName + " " + thisAppt.LastName + "'s " + CorrectDate.ToShortDateString() + " Apt";
        //Commented by jaswinder as not required just add the values in strIds

        //string[] typeIDs = { "80001240-1316387771", "3240000-1110208284", "68A0000-1145673910", "6890000-1145673687", "6870000-1145673532", "6880000-1145673590", "80000EBC-1284404338", "3230000-1110208284", "80000EBB-1284399169", "3240000-1110208284" };

        string strIds = string.Empty;
        strIds = "80001240-1316387771,3240000-1110208284, 68A0000-1145673910,6890000-1145673687,6870000-1145673532,6880000-1145673590,80000EBC-1284404338,3230000-1110208284,80000EBB-1284399169,3240000-1110208284";
        //for (int i = 0; i < typeIDs.Length - 1; i++)
        //{
        //    strIds += typeIDs[i] + ',';
        //}

        var Invoices = _objAptConsoleService.GetInvoiceDetails(strIds, PatientID);

        if (Invoices.Count() > 0)
        {
            thisAppt.InvoiceDue = decimal.Parse(Invoices.First().OpenBalance);
            if (Invoices.First().InvoiceLineItemRefListID == "80001240-1316387771")
            {
                if (Invoices.First().SalesPrice > thisAppt.InvoiceDue)
                    thisAppt.ExpirationDate = ((DateTime)Invoices.First().DueDate).AddMonths(6);
                else
                    thisAppt.ExpirationDate = ((DateTime)Invoices.First().DueDate);
            }
            else
            {
                if (Invoices.First().SalesPrice > thisAppt.InvoiceDue)
                    thisAppt.ExpirationDate = ((DateTime)Invoices.First().DueDate).AddYears(1);
                else
                    thisAppt.ExpirationDate = ((DateTime)Invoices.First().DueDate);
            }
            thisAppt.InvoiceDueDate = Invoices.First().DueDate;
            thisAppt.InvoicePaid = Invoices.First().IsPaid.ToLower() == "true" ? true : false;
            if (Invoices.First().OpenBalance != "" && Invoices.First().OpenBalance != null)
            {
                thisAppt.InvoiceDue = decimal.Parse(Invoices.First().OpenBalance);
            }

        }
        else
        {
            thisAppt.ExpirationDate = null;
        }
        var renDate = thisAppt.ExpirationDate;

        string renMonth = "";
        if (thisAppt.RenewalException != "" && thisAppt.RenewalExcExpire != null && thisAppt.RenewalExcExpire >= DateTime.Now)
        {
            renMonth = thisAppt.RenewalException;
        }
        else
        {
            if (renDate != null)
            {
                if (thisAppt.TermsInMonths == 6)
                {
                    QB_InvoicesViewModel inv = _objAptConsoleService.GetQBInvoiceDetails("80001240-1316387771", PatientID);

                    if (inv != null)
                    {
                        if (((DateTime)inv.Date).Day > 10)
                        {
                            renDate = ((DateTime)renDate).AddMonths(1);
                        }
                    }

                    renMonth = ((DateTime)renDate).ToString("MMMM") + " - 6 Months";
                }
                else
                {
                    QB_InvoicesViewModel inv = _objAptConsoleService.GetQBInvoiceDetails(strIds, PatientID);

                    if (inv != null)
                    {
                        if (((DateTime)inv.Date).Day > 10)
                        {
                            renDate = ((DateTime)renDate).AddMonths(1);
                        }
                    }
                    renMonth = ((DateTime)renDate).ToString("MMMM") + " - 12 Months";

                }

                lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
                if (((DateTime)renDate) < DateTime.Today.AddMonths(-3))
                {
                    lblRenewalMonth.BackColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                renMonth = "None Found";
                lblRenewalMonth.ForeColor = System.Drawing.Color.Black;
            }
        }


        lblRenewalMonth.Text = "Renewal Month: " + renMonth;

        System.Web.UI.HtmlControls.HtmlControl ifrOVU = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "ifrOVU");

        TabPanel OVUPanel = (TabPanel)Utilities.FindControlRecursive(Master, "OVUPanel");
        if (thisAppt.BloodWorkCount == 0)
        {
            try
            {
                OVUPanel.Visible = false;


            }
            catch (System.Exception ex)
            {

            }


        }
        else
        {
            OVUPanel.Visible = true;
        }


        if (!IsPostBack)
        {
            // bind the list for ContactTypeTbl
            objIAppointmentConsole = new AppointmentConsole();
            List<Contact_Type_tblViewModel> lstViewModel = objIAppointmentConsole.GetContactTypeTblList().Where(t => t.Viewable_yn == true).OrderByDescending(t => t.AptTypeDesc).ToList();
            drpContacttype.DataSource = lstViewModel;
            drpContacttype.DataTextField = "AptTypeDesc";
            drpContacttype.DataValueField = "AptTypeID";
            drpContacttype.DataBind();
            drpContacttype.Items.Insert(0, new ListItem("", "0"));

            if (Session["ActiveTab"] != null)
                BindActiveTab(Session["ActiveTab"].ToString());
            else
                BindActiveTab(string.Empty);
           
        }
    }
    /// <summary>
    /// changing the visiblity of tab
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void ConsoleContainer_ActiveTabChanged(object sender, EventArgs e)
    {
        if (Session["ActiveTab"] != null)
            BindActiveTab(Session["ActiveTab"].ToString());
        else
            BindActiveTab(string.Empty);

    }

    #region "Events of Problem List Tab"

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
            result = objService.InsertProblemDiagnosisElements(1, newProb, AptID);
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
            result = objService.InsertProblemDiagnosisElements(2, newProb, AptID);
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

    #endregion

    /// <summary>
    /// updates patients critical tasks
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Requested_CheckedChanged(object sender, EventArgs e)
    {
        //assign date
        try
        {
            CheckBox cboRequest = (CheckBox)sender;
            GridViewRow selRow = (GridViewRow)cboRequest.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            int taskID = int.Parse(taskLabel.Text);
            objCriticalService = new CriticalTaskService();
            objCriticalService.UpdatePatientsCriticalTasks(taskID, cboRequest.Checked);
            BindTaskData();
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
    /// binds the grid with patients uploaded docs
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Received_CheckedChanged(object sender, EventArgs e)
    {
        //asign doc and date
        try
        {
            CheckBox cboReceived = (CheckBox)sender;
            Session["cboReceived"] = cboReceived;
            GridViewRow selRow = (GridViewRow)cboReceived.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            Session["TaskID"] = int.Parse(taskLabel.Text);
            if (cboReceived.Checked)
            {
                objCriticalService = new CriticalTaskService();
                if (Request.QueryString["PatientID"] != null)
                {
                    PatientID = int.Parse(Request.QueryString["PatientID"].ToString());
                }
                grdDocs.DataSource = objCriticalService.GetUploadDocsByPatientID(PatientID);
                grdDocs.DataBind();
                ClientScript.RegisterStartupScript(this.GetType(), "myalert", "alert('On the next screen, open the document, determine the actual date, and then enter it.');", true);
                modReceived.Show();
            }
            else
            {
                objCriticalService = new CriticalTaskService();
                DateTime? ReceivedDate = null;
                int? UploadId = null;
                objCriticalService.UpdatePatientsCriticalTasksUploads((int)Session["TaskID"], cboReceived.Checked, ReceivedDate, UploadId);

                BindTaskData();
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

    protected void Reviewed_CheckedChanged(object sender, EventArgs e)
    {
        //assign date
        try
        {
            CheckBox cboRequest = (CheckBox)sender;
            GridViewRow selRow = (GridViewRow)cboRequest.NamingContainer;
            Label taskLabel = (Label)selRow.Cells[4].Controls[1];
            int taskID = int.Parse(taskLabel.Text);

            objCriticalService = new CriticalTaskService();
            objCriticalService.UpdatePatientsCriticalTasks(taskID, cboRequest.Checked);

            BindTaskData();
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
    /// binds critical tasks list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDocs_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            objCriticalService = new CriticalTaskService();
            DateTime ReceivedDate = DateTime.Now;
            objCriticalService.UpdatePatientsCriticalTasksUploads((int)Session["TaskID"], true, ReceivedDate, (int)grdDocs.SelectedDataKey["ID"]);

            BindTaskData();
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
    /// cancel the document uploads by patient
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelDoc_Click(object sender, EventArgs e)
    {
        try
        {
            CheckBox cboReceived = (CheckBox)Session["cboReceived"];
            cboReceived.Checked = false;
            BindTaskData();
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

    //set the diagnosis which are associated with the particular appointment
    protected void chkDiagnosis_CheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkDiagnosis = (CheckBox)sender;
        objService = new ProblemListService();
        string DiagnosisID = chkDiagnosis.ToolTip;
        if (chkDiagnosis.Checked == true)
        {

            objService.InsertProblemAppointment(PatientID, Convert.ToInt16(DiagnosisID), AptID);
        }
        else
        {
            objService.DeleteProblemAppointment(PatientID, Convert.ToInt16(DiagnosisID), AptID);
        }

    }
    #endregion

    #region Method

    private void BindICDCodes()
    {
        try
        {
            _objService = new ProtocolService();
            List<ICD10CodesViewmodel> ICDCode = _objService.GetIcd10Codes().ToList();
            _objService = new ProtocolService();
            List<ICD10CodesViewmodel> PatientICDCode = _objService.GetPatientIcd10Codes(AptID, PatientID).ToList();
            List<ICD10CodesViewmodel> IcdMale = ICDCode.Where(o => o.Gender == "Male").ToList();
            chkMale.DataSource = IcdMale;
            chkMale.DataBind();
            List<ICD10CodesViewmodel> SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Male").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkMale.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdeFMale = ICDCode.Where(o => o.Gender == "Female").ToList();
            chkFemale.DataSource = IcdeFMale;
            chkFemale.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Female").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkFemale.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdBoth = ICDCode.Where(o => o.Gender == "Both").ToList();
            chkBoth.DataSource = IcdBoth;
            chkBoth.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Both").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkBoth.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdOther = ICDCode.Where(o => o.Gender == "Other").ToList();
            chkOther.DataSource = IcdOther;
            chkOther.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Other").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkOther.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
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
            _objService = null;
        }
    }

    private void BindICDCodessuppliments()
    {
        try
        {
            _objService = new ProtocolService();
            rptSuppliments.DataSource = _objService.GetPatientIcd10CodesSuppliments(AptID, PatientID).ToList();

            rptSuppliments.DataBind();
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
    /// get the list of patients critical task
    /// </summary>
    private void BindTaskData()
    {
        try
        {
            objCriticalService = new CriticalTaskService();
            grdTasks.DataSource = objCriticalService.GetCriticalTaskListByPatientID(PatientID);
            grdTasks.DataBind();
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

    #region Private Methods
    /// <summary>
    /// bind all active tabs
    /// </summary>
    /// <param name="refershTabName"></param>
    private void BindActiveTab(string refershTabName)
    {
        try
        {
            string tabName = string.Empty;
            if (!string.IsNullOrWhiteSpace(refershTabName))
                tabName = refershTabName;
            else
                tabName = ConsoleContainer.ActiveTab.ID;

            switch (tabName)
            {
                case "Details":
                    if (pnlDetails.Visible == true)
                    {
                        PopulateAptDetails();
                        pnlDetails.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 0;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "RX":
                    if (pnlRX.Visible == false)
                    {
                        PopulateRX();
                        pnlRX.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 1;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "MedNotes":
                    if (pnlNotes.Visible == false)
                    {
                        PopulateMedNotes();
                        pnlNotes.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 2;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "LabWork":
                    if (!pnlLab.Visible)
                    {
                        PopulateLabs();
                        pnlLab.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 3;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "Vitals":
                    if (!pnlVitals.Visible)
                    {
                        PopulateVitals();
                        pnlVitals.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 4;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "IcdCode":
                    if (!pnlIcdCode.Visible)
                    {
                        BindICDCodes();
                        pnlIcdCode.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 5;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "SupplimentList":
                    
                        BindICDCodessuppliments();
                        PnlSuppliment.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 6;
                        Session["ActiveTab"] = null;
                    
                    break;
                   
                case "ProblemList":
                    if (!pnlProblems.Visible)
                    {
                        PopulateProblemList();
                        pnlProblems.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 7;
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "Uploads":
                    if (!pnlUploads.Visible)
                    {
                        PopulateUploads();
                        pnlUploads.Visible = true;
                        ConsoleContainer.ActiveTabIndex = 11;
                        Session["ActiveTab"] = null;
                    }
                    break;
               
                case "OVUPanel":
                    if (!pnlOVU.Visible)
                    {
                        Session["goal"] = null;
                        Session["sym"] = null;
                        pnlOVU.Visible = true;
                        System.Web.UI.HtmlControls.HtmlControl frame1 = (System.Web.UI.HtmlControls.HtmlControl)Utilities.FindControlRecursive(Page.Master, "PageContents");
                        if (frame1 != null)
                            frame1.Attributes["src"] = "AptOvuTab.aspx?PatientID=" + PatientID.ToString() + "&AptID=" + AptID.ToString();
                        Session["ActiveTab"] = null;
                    }
                    break;
                case "CriticalTasks":
                    if (!pnlCriticalTasks.Visible)
                    {
                        BindTaskData();
                        pnlCriticalTasks.Visible = true;
                        Session["ActiveTab"] = null;
                    }
                    break;
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }
    /// <summary>
    /// method for bind the gridview control for Patient Documents or Scans 
    /// </summary>
    private void PopulateUploads()
    {

        try
        {
            _objAptConsoleService = new AppointmentConsole();
            rptUploads.DataSource = _objAptConsoleService.GetUploadDetails(PatientID);
            rptUploads.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }

    }

    /// <summary>
    /// Bind repeater with problem diagnosis
    /// </summary>
    public void BindDiagnosis()
    {
        objService = new ProblemListService();
        rptProblems.DataSource = objService.GetProblemSDiagnosisListByPatientID(PatientID, AptID);
        rptProblems.DataBind();
    }

    /// <summary>
    /// bind repeater with symptoms
    /// </summary>
    public void BindSymptoms()
    {
        objService = new ProblemListService();
        rptSymptoms.DataSource = objService.GetProblemSymtomsListByPatientID(PatientID);
        rptSymptoms.DataBind();
    }

    public void BindMiscDiag()
    {
        objService = new ProblemListService();
        rptMisc.DataSource = objService.GetMiscDiagListByPatientID(PatientID);
        rptMisc.DataBind();
    }

    /// <summary>
    /// Methods for bind the repeater controls for 
    /// Diagnoses, Symptoms, Diagnoses handled by 3rd party 
    /// </summary>
    private void PopulateProblemList()
    {


        try
        {
            BindDiagnosis();
            _objAptConsoleService = new AppointmentConsole();

            var DiagList = _objAptConsoleService.GetDiagnosisDetails();
            ddlDiagnoses.DataSource = DiagList;
            ddlDiagnoses.DataTextField = "Text";
            ddlDiagnoses.DataValueField = "Value";
            ddlDiagnoses.DataBind();

            BindSymptoms();

            IProblemListService objProblemService = new ProblemListService();
            ddlSymptom.DataSource = objProblemService.GetSymptomList();
            ddlSymptom.DataTextField = "Text";
            ddlSymptom.DataValueField = "value";
            ddlSymptom.DataBind();

            BindMiscDiag();

            ddlMisc.DataSource = DiagList;
            ddlMisc.DataTextField = "Text";
            ddlMisc.DataValueField = "Value";
            ddlMisc.DataBind();
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

    /// <summary>
    /// method for bind the Vitals details under Vital tab
    /// </summary>
    private void PopulateVitals()
    {


        try
        {

            objPatVitalService = new PatientVitalsService();
            rptVitals.DataSource = objPatVitalService.GetPatientVitalsByPatientId(PatientID);
            rptVitals.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objPatVitalService = null;
        }
    }

    /// <summary>
    /// Method for bind the Medical Note details
    /// </summary>
    private void PopulateMedNotes()
    {


        try
        {
            _objAptConsoleService = new AppointmentConsole();
            rptNotes.DataSource = _objAptConsoleService.GetMedNoteTabDetails(PatientID, 0);
            rptNotes.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }

    }

    /// <summary>
    /// Method for bind the Lab details
    /// </summary>
    private void PopulateLabs()
    {


        try
        {
            _objAptConsoleService = new AppointmentConsole();

            //Get the lab report messageid for the patient
            var lFreq = _objAptConsoleService.GetLabWorkDetails(PatientID);
            var CurrLab = from f in lFreq
                          select new
                          {
                              GroupName = f.GroupName,
                              InRange = f.InRange,
                              Days = f.Days,
                              LastComplete = f.LastComplete,
                              DispLine = Convert.ToBoolean(f.InRange) ? f.Days.ToString() : "<font color='#FF0000'>" + f.Days.ToString() + "</font>",
                          };
            rptCurrLab.DataSource = CurrLab;
            rptCurrLab.DataBind();


            var FullLabs = _objAptConsoleService.GetLabListDetails(PatientID);
            var LabList = from l in FullLabs
                          where ((DateTime)l.ObservationDateTime).Year <= DateTime.Now.Year
                          select l;
            rptLabs.DataSource = LabList;
            rptLabs.DataBind();


            int counter = 0;
            //set the scr for frame for showing the lab report data
            foreach (var theLab in LabList)
            {
                switch (counter)
                {
                    case 0:
                        History1.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist1.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 1:
                        History2.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist2.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 2:
                        History3.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist3.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 3:
                        History4.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist4.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 4:
                        History5.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist5.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 5:
                        History6.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist6.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                    case 6:
                        History7.HeaderText = ((DateTime)theLab.ObservationDateTime).ToShortDateString();
                        ifrHist7.Attributes["src"] = "lab_report_short.aspx?message_id=" + theLab.MessageID.ToString() + "&patientid=" + PatientID;
                        break;
                }
                if (counter == 7) break;
                counter++;
            }
            if (counter < 7)
            {
                for (int x = counter; x < 7; x++)
                {
                    switch (x)
                    {
                        case 0:
                            History1.HeaderText = "NA";
                            ifrHist1.Attributes["src"] = "blank.htm";
                            break;
                        case 1:
                            History2.HeaderText = "NA";
                            ifrHist2.Attributes["src"] = "blank.htm";
                            break;
                        case 2:
                            History3.HeaderText = "NA";
                            ifrHist3.Attributes["src"] = "blank.htm";
                            break;
                        case 3:
                            History4.HeaderText = "NA";
                            ifrHist4.Attributes["src"] = "blank.htm";
                            break;
                        case 4:
                            History5.HeaderText = "NA";
                            ifrHist5.Attributes["src"] = "blank.htm";
                            break;
                        case 5:
                            History6.HeaderText = "NA";
                            ifrHist6.Attributes["src"] = "blank.htm";
                            break;
                        case 6:
                            History7.HeaderText = "NA";
                            ifrHist7.Attributes["src"] = "blank.htm";
                            break;
                    }
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
            _objAptConsoleService = null;
        }
        ifrNewChart.Attributes["src"] = "LabChart/LabTable.aspx?patientID=" + PatientID.ToString();
        ifrOldChart.Attributes["src"] = "LabChart/LabTableOld.aspx?patientID=" + PatientID.ToString();

    }

    /// <summary>
    /// Method for bind the RX details
    /// </summary>
    private void PopulateRX()
    {
        try
        {
            _objAptConsoleService = new AppointmentConsole();
            rptDrugs.DataSource = _objAptConsoleService.GetDrugs(PatientID);
            rptDrugs.DataBind();

            rptSupp.DataSource = _objAptConsoleService.GetPatientSupplement(PatientID);
            rptSupp.DataBind();

            rptThirdParty.DataSource = _objAptConsoleService.GetThirdPartyRX(PatientID);
            rptThirdParty.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }

    }

    /// <summary>
    /// method for bind the grid controls for 
    /// Medical Notes, Schedule Consult, Schedule General Follow Up,
    /// Blood Draw Requests, Prescription(s) Written
    /// </summary>
    private void PopulateAptDetails()
    {
        try
        {
            _objAptConsoleService = new AppointmentConsole();
            rptMedNotes.DataSource = _objAptConsoleService.GetMedicalNotes(AptID);
            rptMedNotes.DataBind();

            rptConsult.DataSource = _objAptConsoleService.GetScheduleConsult(AptID);
            rptConsult.DataBind();

            rptGenFollow.DataSource = _objAptConsoleService.GetScheduleFollowUp(AptID);
            rptGenFollow.DataBind();

            rptBloodDraw.DataSource = _objAptConsoleService.GetScheduleBloodDraw(AptID);
            rptBloodDraw.DataBind();

            rptRX.DataSource = _objAptConsoleService.GetPrescription(AptID);
            rptRX.DataBind();

            rptAppointmentScheduled.DataSource = _objAptConsoleService.GetFutureAppointments(PatientID);
            rptAppointmentScheduled.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }

    }


    #endregion
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        _objAptConsoleService = new AppointmentConsole();
        rptNotes.DataSource = _objAptConsoleService.GetMedNoteTabDetails(PatientID, Convert.ToInt32(drpContacttype.SelectedValue));
        rptNotes.DataBind();


    }

    [WebMethod]
    public static string DownLoadFile(string patientId, string UploadId)
    {
        UploadScanService objService = new UploadScanService();
        uploadtblViewModel objlist = new uploadtblViewModel();
        objlist = objService.GetDocumentUploadedbyID(int.Parse(UploadId));
        string myBucketName = "lmc-emr-uploads";//"punch-scraped-data"; //your s3 bucket name goes here
        string s3DirectoryName = patientId;
        string s3FileName = objlist.Upload_Path;

        CAmazon myDownloader = new CAmazon();
        myDownloader.DownloadFile(s3DirectoryName, s3FileName, myBucketName);
        return s3FileName;


    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {

            _objService = new ProtocolService();
            string IcdCodes = string.Empty;
            foreach (ListItem item in chkMale.Items)
            {
                if (item.Selected == true)
                {
                    IcdCodes = item.Value + "," + IcdCodes.Trim();
                }

            }
            foreach (ListItem item in chkFemale.Items)
            {
                if (item.Selected == true)
                {
                    IcdCodes = item.Value + "," + IcdCodes.Trim();
                }

            }
            foreach (ListItem item in chkBoth.Items)
            {
                if (item.Selected == true)
                {
                    IcdCodes = item.Value + "," + IcdCodes.Trim();
                }

            }
            foreach (ListItem item in chkOther.Items)
            {
                if (item.Selected == true)
                {
                    IcdCodes = item.Value + "," + IcdCodes.Trim();
                }

            }


            _objService.InsertPatientIcdCodes(PatientID,AptID, IcdCodes);
           

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

    protected void btnCopy_Click(object sender, EventArgs e)
    {
        try
        {
            _objService = new ProtocolService();
            List<ICD10CodesViewmodel> ICDCode = _objService.GetIcd10Codes().ToList();
            _objService = new ProtocolService();
            List<ICD10CodesViewmodel> PatientICDCode = _objService.GetPatientIcd10Codes(0, PatientID).ToList();
            List<ICD10CodesViewmodel> IcdMale = ICDCode.Where(o => o.Gender == "Male").ToList();
            chkMale.DataSource = IcdMale;
            chkMale.DataBind();
            List<ICD10CodesViewmodel> SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Male").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkMale.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdeFMale = ICDCode.Where(o => o.Gender == "Female").ToList();
            chkFemale.DataSource = IcdeFMale;
            chkFemale.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Female").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkFemale.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdBoth = ICDCode.Where(o => o.Gender == "Both").ToList();
            chkBoth.DataSource = IcdBoth;
            chkBoth.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Both").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkBoth.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
                }
            }

            List<ICD10CodesViewmodel> IcdOther = ICDCode.Where(o => o.Gender == "Other").ToList();
            chkOther.DataSource = IcdOther;
            chkOther.DataBind();
            SelectedPatientICDCode = PatientICDCode.Where(o => o.Gender == "Other").ToList();
            if (SelectedPatientICDCode != null)
            {
                foreach (var i in SelectedPatientICDCode)
                {
                    foreach (ListItem item in chkOther.Items)
                    {
                        if (i.Id == int.Parse(item.Value))
                        {
                            item.Selected = true;
                            break;
                        }
                    }
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
            _objService = null;
        }
    }
}
