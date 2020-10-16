using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;

public partial class Ticket : LMCBase
{
    protected int PatientID;
    protected int TicketID;
    protected string UserName;
    protected int StaffID;
    IAppointmentConsole objIAppointmentsService = null;
    IFollowUpTypeService objIFollowUpTypeService = null;
    IStaffService objIStaffService = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PatientID = int.Parse(Request.QueryString["PatientID"]);
            TicketID = Convert.ToInt32(Session["ActiveTicket"].ToString());
            UserName = Session["MM_Username"].ToString();
            StaffID = int.Parse(Session["StaffID"].ToString());
            if (!IsPostBack)
            {

                BindGrids();

                objIFollowUpTypeService = new FollowUpTypeService();
                FollowupViewModel fup = objIFollowUpTypeService.GetFollowupListByFollowupId(TicketID);

                if (fup == null || (bool)fup.FollowUp_Completed_YN)
                {
                    pnlClose.Visible = false;
                    btnNew.Text = "Disabled";
                    btnNew.Enabled = false;
                }
                else
                {
                    pnlClose.Visible = true;
                    btnNew.Text = "Submit new note";
                    btnNew.Enabled = true;
                }

                if (fup != null)
                {
                    switch (fup.Severity)
                    {
                        case 1:
                            rdoSeverity.SelectedIndex = 0;
                            break;
                        case 2:
                            rdoSeverity.SelectedIndex = 1;
                            break;
                        case 3:
                            rdoSeverity.SelectedIndex = 2;
                            break;
                    }

                    ddlAssign.Items.Clear();

                    txtDueDate.Text = fup.DueDate != null ? ((DateTime)fup.DueDate).ToShortDateString() : "";

                    if (fup.FollowUp_Completed_YN == true)
                    {
                        btnOkClose.Text = "ReOpen";
                        ClosePanel.HeaderText = "ReOpen";

                    }
                    if (fup.DepartmentAssign == null)
                    {
                        objIStaffService = new StaffService();
                        List<StaffViewModel> AssignList = objIStaffService.GetStaffDetailsByDepartment(1);

                        StaffViewModel blnakOne = new StaffViewModel();
                        blnakOne.EmployeeName = "None Selected";
                        blnakOne.EmployeeID = 154;
                        AssignList.Insert(0, blnakOne);
                        ddlAssign.DataTextField = "EmployeeName";
                        ddlAssign.DataValueField = "EmployeeID";
                        ddlAssign.DataSource = AssignList;
                        ddlAssign.DataBind();

                        if (fup.Assigned != null)
                        {
                            //List<StaffViewModel> assignTo = objIStaffService.GetStaffDetailsByEmployeeId((int)fup.Assigned);
                            //if (assignTo.Count() > 0)
                            if (AssignList.Where(s => s.EmployeeID == fup.Assigned).Count() > 0)
                                ddlAssign.SelectedValue = fup.Assigned.ToString();


                        }

                        rdoDept.SelectedIndex = 0;
                    }
                    else
                    {
                        IDepartmentService objIDepartmentService = new DepartmentService();
                        List<DepartmentViewModel> AssignList = new List<DepartmentViewModel>();

                        // get the department list.
                        AssignList = objIDepartmentService.GetDepartments();

                        ddlAssign.DataTextField = "DepartmentName";
                        ddlAssign.DataValueField = "DepartmentID";

                        ddlAssign.DataSource = AssignList;
                        ddlAssign.DataBind();

                        if (AssignList.Where(s => s.DepartmentID == fup.DepartmentAssign).Count() > 0)
                            ddlAssign.SelectedValue = fup.DepartmentAssign.ToString();

                        rdoDept.SelectedIndex = 1;
                    }

                    //objIAppointmentsService = new AppointmentConsole();
                    //apt_FollowUp_typesViewModel Custom = new apt_FollowUp_typesViewModel();

                    //int followupId = Convert.ToInt32(fup.FollowUp_Cat);
                    //Custom = objIAppointmentsService.GetCustomInfoFromFollowUpType(followupId);
                    //if (Custom != null)
                    //{
                    if (!string.IsNullOrEmpty(fup.CustomPage))
                    {
                        CustomInfo.Visible = true;
                        CustomInfo.HeaderText = fup.CustomHeader;
                        string Qstring = "";
                        if (!string.IsNullOrEmpty(fup.CustomParams))
                        {
                            string[] parms = fup.CustomParams.Split('|');
                            foreach (string parm in parms)
                            {
                                if (Qstring == "")
                                {
                                    Qstring += "?";
                                }
                                else
                                {
                                    Qstring += "&";
                                }
                                switch (parm)
                                {
                                    case "PatientID":
                                        Qstring += "PatientID=" + PatientID.ToString();
                                        break;
                                    case "TicketID":
                                        Qstring += "TicketID=" + TicketID.ToString();
                                        break;
                                }
                            }
                        }
                        ifrCustom.Attributes["src"] = fup.CustomPage + Qstring;
                        CustomInfo.Visible = true;
                    }
                    // }
                    else
                    {
                        CustomInfo.Visible = false;
                        CustomInfo.HeaderText = "";
                        ifrCustom.Attributes["src"] = "";
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
            objIFollowUpTypeService = null;

        }
    }

    /// <summary>
    /// method for bind the all tabs
    /// </summary>
    private void BindGrids()
    {
        try
        {
            objIAppointmentsService = new AppointmentConsole();
            List<Contact_tblViewModel> boxContactsOnly = objIAppointmentsService.GetContactTblByFollowupId(TicketID);
            grdTicketOnly.DataSource = boxContactsOnly;
            grdTicketOnly.DataBind();

            grdNoteDetails.DataSource = boxContactsOnly;
            grdNoteDetails.DataBind();

            //grdTicket.DataSource = boxContactsOnly;
            //grdTicket.DataBind();

            //DateTime Dateevent = DateTime.MinValue;
            //List<Contact_tblViewModel> ContactRecs = objIAppointmentsService.GetContactTblByPatientId(PatientID, true, true, -2, 0, Dateevent);
            //grdContacts.DataSource = ContactRecs;
            //grdContacts.DataBind();

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIAppointmentsService = null;
        }
    }

    //assign the ticket to patient
    protected void txtPatient_TextChanged(object sender, EventArgs e)
    {
        FollowupViewModel viewModelFollowUp = null;
        try
        {
            objIFollowUpTypeService = new FollowUpTypeService();
            viewModelFollowUp = new FollowupViewModel();
            viewModelFollowUp = objIFollowUpTypeService.GetFirstRecordForFollowUpType(TicketID);

            if (txtPatient.Text != "")
            {
                Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);

                viewModelFollowUp.PatientID = pat.ID;
            }
            else
            {
                viewModelFollowUp.PatientID = null;
            }
            objIFollowUpTypeService = new FollowUpTypeService();
            objIFollowUpTypeService.UpdateFollowUp_Completed_YN(viewModelFollowUp);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIFollowUpTypeService = null;
            viewModelFollowUp = null;

        }

    }

    protected void CancelChange(object sender, EventArgs e)
    {
        Response.Redirect("ticket.aspx?PatientID=" + PatientID.ToString());

    }

    protected void btnOkClose_Click(object sender,EventArgs e)
    {
        FollowupViewModel viewModelFollowUp = null;
        bool Result = false;
        IStaffService objIStaffService = new StaffService();
        IFollowUpTypeService objIFollowUpTypeService = new FollowUpTypeService();
        viewModelFollowUp = new FollowupViewModel();
        try
        {
            viewModelFollowUp = objIFollowUpTypeService.GetFirstRecordForFollowUpType(TicketID);

            if (!(bool)viewModelFollowUp.FollowUp_Completed_YN)
            {
                viewModelFollowUp.FollowUp_Completed_YN = true;
                objIFollowUpTypeService = new FollowUpTypeService();
                objIFollowUpTypeService.UpdateFollowUp_Completed_YN(viewModelFollowUp);

                if (viewModelFollowUp.FollowUp_Cat == 6)
                {
                    IAppointmentConsole objIAppointmentsService = new AppointmentConsole();
                    objIAppointmentsService.InsertMedicalNotesByTicketForm(int.Parse(viewModelFollowUp.PatientID.ToString()), StaffID, viewModelFollowUp.FollowUp_Body + "\r\n<br/>" + edClose.Content);
                }

                objIStaffService.InsertTicketIntoContactTbl(PatientID, "Ticket " + TicketID + " closed.\r\n" + edClose.Content + "\r\nClosed by " + UserName, StaffID, TicketID);
            }
            else
            {
                viewModelFollowUp.FollowUp_Completed_YN = false;
                objIFollowUpTypeService = new FollowUpTypeService();
                objIFollowUpTypeService.UpdateFollowUp_Completed_YN(viewModelFollowUp);
                objIStaffService.InsertTicketIntoContactTbl(PatientID, "Ticket " + TicketID + " reopened.\r\n" + edClose.Content + "\r\nReopened by " + UserName, StaffID, TicketID);

            }
            Result = true;
            Response.Redirect("Manage.aspx?PatientID=" + int.Parse(Request.QueryString["PatientID"]).ToString(), "_top", "");
        }
        catch
        {
            Result = false;
        }
    }

    protected void btnOkEdit_Click(object sender, EventArgs e)
    {
        //mark ticket closed      
        objIFollowUpTypeService = new FollowUpTypeService();
        int AssignID = int.Parse(ddlAssign.SelectedValue);
        FollowupViewModel fup = new FollowupViewModel();
        fup = objIFollowUpTypeService.GetFirstRecordForFollowUpType(TicketID);
        if (fup != null)
        {
            objIFollowUpTypeService = new FollowUpTypeService();
            fup.DueDate = DateTime.Parse(txtDueDate.Text);
            fup.Severity = int.Parse(rdoSeverity.SelectedValue);
            if (rdoDept.SelectedValue != "Dept")
            {
                fup.Assigned = AssignID;
                fup.DepartmentAssign = null;
            }

                //objIFollowUpTypeService.UpdateFollowupTicket(TicketID, AssignID, int.Parse(rdoSeverity.SelectedValue));

            else
            {
                fup.Assigned = null;
                fup.DepartmentAssign = AssignID;
            }

            objIFollowUpTypeService.UpdateFollowupTicket(fup);
            // objIFollowUpTypeService.UpdateFollowupTicketDept(TicketID, AssignID, int.Parse(rdoSeverity.SelectedValue));
        }
        //add contact record.




        IStaffService objIStaffService = null;
        if (fup.Severity == 1)
        {
            if (rdoDept.SelectedValue != "Dept")
            {
                objIStaffService = new StaffService();
                StaffViewModel viewModelStaff = new StaffViewModel();
                viewModelStaff = objIStaffService.GetFirstOrDefaultStaffByEmployeeId(AssignID);

                Utilities.SendGmailMessage(viewModelStaff.username + "@LongevityMedicalClinic.com", viewModelStaff.EmployeeName, Server.HtmlDecode(fup.FollowUp_Body), "", "You have received a new High Priority ticket.");
            }
            else
            {
                objIStaffService = new StaffService();
                List<StaffViewModel> lstViewModelStaff = objIStaffService.GetStaffDetailsByDepartment(AssignID);
                foreach (StaffViewModel emp in lstViewModelStaff)
                {
                    Utilities.SendGmailMessage(emp.username + "@LongevityMedicalClinic.com", emp.EmployeeName, Server.HtmlDecode(fup.FollowUp_Body), "", "A grroup you are part of has received a new High Priority ticket.");
                }
            }
        }
        if (cboDetach.Checked)
        {
            fup.PatientID = null;
        }
        objIStaffService = new StaffService();
        objIStaffService.InsertTicketIntoContactTbl(int.Parse(Request.QueryString["PatientID"]), "Ticket modified Assigned to " + ddlAssign.SelectedItem.Text + " Severity is " + rdoSeverity.SelectedItem.Text + " Changed by " + Session["MM_Username"], (int)Session["StaffID"], TicketID);

        Response.Redirect("Manage.aspx?PatientID=" + int.Parse(Request.QueryString["PatientID"]).ToString(), "_top", "");
    }

    //close or open ticket
    [WebMethod]
    public static bool CloseRepoenTicket(int TicketID, int PatientID, string Content, string UserName, int StaffID)
    {
        FollowupViewModel viewModelFollowUp = null;
        bool Result = false;
        IStaffService objIStaffService = new StaffService();
        IFollowUpTypeService objIFollowUpTypeService = new FollowUpTypeService();
        viewModelFollowUp = new FollowupViewModel();
        try
        {
            viewModelFollowUp = objIFollowUpTypeService.GetFirstRecordForFollowUpType(TicketID);

            if (!(bool)viewModelFollowUp.FollowUp_Completed_YN)
            {
                viewModelFollowUp.FollowUp_Completed_YN = true;
                objIFollowUpTypeService = new FollowUpTypeService();
                objIFollowUpTypeService.UpdateFollowUp_Completed_YN(viewModelFollowUp);

                if (viewModelFollowUp.FollowUp_Cat == 6)
                {
                    IAppointmentConsole objIAppointmentsService = new AppointmentConsole();
                    objIAppointmentsService.InsertMedicalNotesByTicketForm(int.Parse(viewModelFollowUp.PatientID.ToString()), StaffID, viewModelFollowUp.FollowUp_Body + "\r\n<br/>" + Content);
                }

                objIStaffService.InsertTicketIntoContactTbl(PatientID, "Ticket " + TicketID + " closed.\r\n" + Content + "\r\nClosed by " + UserName, StaffID, TicketID);
            }
            else
            {
                viewModelFollowUp.FollowUp_Completed_YN = false;
                objIFollowUpTypeService = new FollowUpTypeService();
                objIFollowUpTypeService.UpdateFollowUp_Completed_YN(viewModelFollowUp);
                objIStaffService.InsertTicketIntoContactTbl(PatientID, "Ticket " + TicketID + " reopened.\r\n" + Content + "\r\nReopened by " + UserName, StaffID, TicketID);

            }
            Result = true;
        }
        catch
        {
            Result = false;
        }
        return Result;

    }


    //protected void grdTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    //e.Row.Cells[1].Text = Server.HtmlDecode(e.Row.Cells[1].Text);
    //    e.Row.Cells[0].Attributes.Add("style", "white-space:nowrap;");
    //}


    //create a new ticket data
    protected void btnNew_Click(object sender, EventArgs e)
    {
        try
        {
            objIStaffService = new StaffService();
            objIStaffService.InsertTicketIntoContactTbl(PatientID, edTicket.Content + " tt" + "\r\n Ticket " + TicketID, (int)Session["StaffID"], TicketID);

            objIAppointmentsService = new AppointmentConsole();
            List<Contact_tblViewModel> boxContactsNote = objIAppointmentsService.GetContactTblByFollowupId(TicketID);

            grdNoteDetails.DataSource = boxContactsNote;
            grdNoteDetails.DataBind();
        }
        catch (System.Exception ex)
        {

            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIStaffService = null;
            objIAppointmentsService = null;
        }

    }


    [WebMethod]
    public static List<StaffViewModel> BindEmployee()
    {

        IStaffService objIStaffService = new StaffService();
        List<StaffViewModel> lstViewModelStaff = objIStaffService.GetStaffDetailsByDepartment(Convert.ToInt32(DepartmentsEnum.Employees));
        return lstViewModelStaff;
    }

    //method to bind the departments
    [WebMethod]
    public static List<DepartmentViewModel> BindDepartment()
    {

        IDepartmentService objIDepartmentService = new DepartmentService();
        List<DepartmentViewModel> viewModelDepartment = objIDepartmentService.GetDepartments().Where(d => d.DepartmentID != Convert.ToInt32(DepartmentsEnum.Employees)).ToList();
        return viewModelDepartment;
    }



    protected void rdoDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        if(rdoDept.SelectedValue=="Employee")
        {
            IStaffService objIStaffService = new StaffService();
            List<StaffViewModel> AssignList = objIStaffService.GetStaffDetailsByDepartment(Convert.ToInt32(DepartmentsEnum.Employees));
            StaffViewModel blnakOne = new StaffViewModel();
            blnakOne.EmployeeName = "None Selected";
            blnakOne.EmployeeID = 154;
            AssignList.Insert(0, blnakOne);
            ddlAssign.DataTextField = "EmployeeName";
            ddlAssign.DataValueField = "EmployeeID";
            ddlAssign.DataSource = AssignList;
            ddlAssign.DataBind();
        }
        else
        {
            IDepartmentService objIDepartmentService = new DepartmentService();
            List<DepartmentViewModel> viewModelDepartment = objIDepartmentService.GetDepartments().Where(d => d.DepartmentID != Convert.ToInt32(DepartmentsEnum.Employees)).ToList();
            ddlAssign.DataTextField = "DepartmentName";
            ddlAssign.DataValueField = "DepartmentID";
            ddlAssign.DataSource = viewModelDepartment;
            ddlAssign.DataBind();
        }
    }
}

