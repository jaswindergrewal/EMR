using System;
using System.Linq;
using System.Web.UI.WebControls;
using System.IO;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;

public partial class TicketNoPatient : LMCBase
{
    ITicketManageService objService = null;
    IStaffService objIStaffService = null;
    IFollowUpTypeService objIFollowUpTypeService = null;

    //ticking system to get the details
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            objService = new TicketManageService();
            grdTicketOnly.DataSource = objService.GetTicketOnlyDetails(int.Parse(Request.QueryString["TicketID"]));
            grdTicketOnly.DataBind();


            grdNoteDetails.DataSource = objService.GetTicketOnlyDetails(int.Parse(Request.QueryString["TicketID"]));
            grdNoteDetails.DataBind();


            ILabAddService objFollowup = new LabAddService();
            apt_FollowUpsViewModel fup = objFollowup.GetFollowupDetails(int.Parse(Request.QueryString["TicketID"]));

            //enable or disable  button air panel on the basis of wether the ticket is closed or not
            if ((bool)fup.FollowUp_Completed_YN)
            {
                pnlClose.Visible = false;
                pnlReopen.Visible = true;
                btnNew.Text = "Disabled";
                btnNew.Enabled = false;
            }
            else
            {
                pnlClose.Visible = true;
                pnlReopen.Visible = false;
                btnNew.Text = "Submit new note";
                btnNew.Enabled = true;
            }

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
            txtDueDate.Text = ((DateTime)fup.DueDate).ToShortDateString();

            //set the department or staff

            ddlAssign.Items.Clear();
            INewTicketNoPatientService objAssign = null;
            if (fup.DepartmentAssign == null)
            {
                objAssign = new NewTicketNoPatientService();
                var AssignList = objAssign.GetDepartmentStaff();//from ds in ctx.DepartmentStaffs where ds.Staff.Active_YN == true && ds.DepartmentID == 1 orderby ds.Staff.EmployeeName select ds.Staff;
                ddlAssign.DataTextField = "EmployeeName";
                ddlAssign.DataValueField = "EmployeeID";
                ddlAssign.DataSource = AssignList;
                ddlAssign.DataBind();
                //if ((from ds in AssignList where ds.DepartmentStaffID == fup.Assigned select ds ).Count() > 0)
                ddlAssign.SelectedValue = fup.Assigned.ToString();

                rdoDept.SelectedIndex = 0;
            }
            else
            {
                objAssign = new NewTicketNoPatientService();
                var AssignList = objAssign.GetDepartments();//from ds in ctx.DepartmentStaffs orderby ds.Department.DepartmentName select ds.Department;
                ddlAssign.DataTextField = "DepartmentName";
                ddlAssign.DataValueField = "DepartmentID";
                ddlAssign.DataSource = AssignList;
                ddlAssign.DataBind();
                if ((from s in AssignList where s.DepartmentID == (int)fup.DepartmentAssign select s).Count() > 0)
                    ddlAssign.SelectedValue = fup.DepartmentAssign.ToString();

                rdoDept.SelectedIndex = 1;
            }

        }
        btnImageInsert.UploadFolder = "~/UserImages/" + ((int)Session["StaffID"]).ToString() + "/";

    }


    protected void txtPatient_TextChanged(object sender, EventArgs e)
    {
        FollowupViewModel viewModelFollowUp = null;
        try
        {
            objIFollowUpTypeService = new FollowUpTypeService();
            viewModelFollowUp = new FollowupViewModel();
            viewModelFollowUp = objIFollowUpTypeService.GetFirstRecordForFollowUpType((int)Session["ActiveTicket"]);

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
        catch (System.Exception)
        {

            throw;
        }
        finally
        {

            viewModelFollowUp = null;

        }



        //apt_FollowUp fup = (from f in ctx.apt_FollowUps where f.FollowUp_ID == (int)Session["ActiveTicket"] select f).First();
        //if (txtPatient.Text != "")
        //{
        //    Calendar.Patient pat = Calendar.Patients.CheckPatient(txtPatient.Text);

        //    fup.PatientID = pat.ID;
        //}
        //else
        //{
        //    fup.PatientID = null;
        //}
        //ctx.SubmitChanges();

    }

    protected void rdoDept_SelectedIndexChanged(object sender, EventArgs e)
    {
        INewTicketNoPatientService objAssign = null;
        switch (rdoDept.SelectedValue)
        {
            case "Emp":
                objAssign = new NewTicketNoPatientService();
                //var AssignList = from ds in ctx.DepartmentStaffs where ds.DepartmentID == 1 orderby ds.Staff.EmployeeName select ds.Staff;
                ddlAssign.Items.Clear();
                ddlAssign.DataTextField = "EmployeeName";
                ddlAssign.DataValueField = "EmployeeID";
                ddlAssign.DataSource = objAssign.GetDepartmentStaff();
                ddlAssign.DataBind();
                break;
            case "Dept":
                //var AssignList1 = from d in ctx.Departments where d.DepartmentID != 1 orderby d.DepartmentName select d;
                objAssign = new NewTicketNoPatientService();
                ddlAssign.Items.Clear();
                ddlAssign.DataTextField = "DepartmentName";
                ddlAssign.DataValueField = "DepartmentID";
                ddlAssign.DataSource = objAssign.GetDepartments();
                ddlAssign.DataBind();
                break;
        }
    }

    protected void btnOkClose_Click(object sender, EventArgs e)
    {
        //mark ticket closed

        FollowupViewModel viewModelFollowUp = null;

        objIStaffService = new StaffService();
        objIFollowUpTypeService = new FollowUpTypeService();

        viewModelFollowUp = new FollowupViewModel();
        viewModelFollowUp = objIFollowUpTypeService.GetFirstRecordForFollowUpType((int)Session["ActiveTicket"]);

        if (!(bool)viewModelFollowUp.FollowUp_Completed_YN )
        {
            viewModelFollowUp.FollowUp_Completed_YN = true;

        }
        if ((bool)viewModelFollowUp.FollowUp_Completed_YN && cboReopen.Checked)
        {
            viewModelFollowUp.FollowUp_Completed_YN = false;
        }
        viewModelFollowUp.DueDate = DateTime.Parse(txtDueDate.Text);

        objService = new TicketManageService();
        bool CboCloseId;
        //if (cboClose.Checked)
        //{
        //    CboCloseId = true;
        //}
        //else
        //{
        //    CboCloseId = false;
        //}
        CboCloseId = true;
        INewTicketNoPatientService objAssign = null;
        objAssign = new NewTicketNoPatientService();
        objAssign.InsertUpdateApt_Followups(viewModelFollowUp, (int)Session["ActiveTicket"], int.Parse(ddlAssign.SelectedValue), int.Parse(rdoSeverity.SelectedValue),
                                               rdoSeverity.SelectedItem.Text, rdoDept.SelectedValue, Session["MM_Username"].ToString(), (int)Session["StaffID"], CboCloseId
                                               , edClose.Content, ddlAssign.SelectedItem.Text);



        Response.Redirect("LandingPage.aspx");


    }

    protected void grdTicket_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Server.HtmlDecode(e.Row.Cells[1].Text);
            e.Row.Cells[0].Attributes.Add("style", "white-space:nowrap;");
        }
    }
    protected void grdContacts_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Server.HtmlDecode(e.Row.Cells[1].Text);
            e.Row.Cells[0].Attributes.Add("style", "white-space:nowrap;");
        }
    }
    protected void btnNew_Click(object sender, EventArgs e)
    {
        string theContent = edTicket.Content;

        int startPos = 0;

        while (theContent.IndexOf("<img", startPos) != -1)
        {

            startPos = theContent.IndexOf("<img", startPos);
            string workString = theContent.Substring(startPos);
            string src = workString.Substring(workString.IndexOf("src") + 5);
            string uri = src.Split('\"')[0];
            string timeStamp = DateTime.Now.Ticks.ToString();

            string origName = uri.Split('/').Last();
            string NewName = origName.Split('.').First() + timeStamp + "." + origName.Split('.').Last();

            theContent = theContent.Replace(origName, NewName);
            string[] dirPath = Request.ServerVariables["PATH_TRANSLATED"].Split('\\');
            string thisPath = "";
            foreach (string seg in dirPath)
            {
                if (seg == "emr_new" || seg.ToLower() == "newemr") break;
                thisPath += seg + "\\";
            }
            thisPath += "emr_new\\UserImages\\" + ((int)Session["StaffID"]).ToString() + "\\";
            File.Move(thisPath + origName, thisPath + NewName);
            startPos = startPos + 5;
        }
        INewTicketNoPatientService ObjAssign = new NewTicketNoPatientService();
        ObjAssign.InsertContactDetails(theContent + "\r\n Ticket " + (int.Parse(Request.QueryString["TicketID"])).ToString(), (int)Session["StaffID"], int.Parse(Request.QueryString["TicketID"]));

        Response.Redirect("TicketNoPatient.aspx?TicketID=" + Request.QueryString["TicketID"]);
    }

    protected void btnCancelClose_Click(object sender, EventArgs e)
    {
        Response.Redirect("TicketNoPatient.aspx?TicketID=" + Request.QueryString["TicketID"]);

    }
}