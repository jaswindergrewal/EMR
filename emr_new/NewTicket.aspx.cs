using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Services;
using Emrdev.ViewModelLayer;
public partial class NewTicket : LMCBase
{
    protected int PatientID = 0;
    EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

    //Fill the Dropdowns and set the due date as read only
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PatientID = int.Parse(Request.QueryString["PatientID"]);
            string IsAutoShipTicket = (Request.QueryString["IsAutoShipTicket"]);
            txtDueDate.Attributes.Add("readonly", "readonly");
            if (!IsPostBack)
            {
                hdnIsAutoshipTicket.Value = IsAutoShipTicket;
                txtDueDate.Text = DateTime.Now.ToShortDateString();

                var Types = from t in ctx.apt_FollowUp_types where t.TicketType_YN == true orderby t.FollowUp_Type_Desc select t;
                AptType.DataSource = Types;
                AptType.DataTextField = "FollowUp_Type_Desc";
                AptType.DataValueField = "FollowUp_Type_ID";
                AptType.DataBind();
                if (IsAutoShipTicket == "True")
                {
                    txtSubject.Text = "Autoship change req.";
                    AptType.SelectedValue = "16";
                    rdoDept.Items.FindByValue("Dept").Selected = true;
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    //Create new ticket and also insert the data in contacttbl table
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        try
        {
            int newID = TicketUtils.MakeTicket((int)Session["StaffID"], ed.Content, int.Parse(AptType.SelectedValue), PatientID, int.Parse(rdoSeverity.SelectedValue), rdoDept.SelectedValue.ToUpper() == "EMP" ? "i" : "d", int.Parse(Request.Form[ddlAssign.UniqueID].ToString()), txtSubject.Text, DateTime.Parse(txtDueDate.Text));

            ctx.contact_tbl_Ticket_Insert(PatientID, "New Ticket Entered.\r\n" + ed.Content + "\r\nTicket " + newID.ToString(), (int)Session["StaffID"], newID);
            Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> parent.change_parent_url('Manage.aspx?PatientID=" + Request.QueryString["PatientID"] + "'); </script>");
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
    }

    //Method for populate Employee dropdown 
    [WebMethod]
    public static List<DepartmentStaffViewModel> BindEmployee()
    {
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        List<DepartmentStaffViewModel> obj = new List<DepartmentStaffViewModel>();
        var AssignList = from ds in ctx.DepartmentStaffs where ds.Staff.Active_YN == true && ds.DepartmentID == 1 orderby ds.Staff.EmployeeName select ds.Staff;
        DepartmentStaffViewModel blnakOne = new DepartmentStaffViewModel();
        blnakOne.EmployeeName = "None Selected";
        blnakOne.EmployeeID = 154;
        obj.Add(blnakOne);
        foreach (var ds in AssignList)
        {
            DepartmentStaffViewModel objDepartmentStaff = new DepartmentStaffViewModel();
            objDepartmentStaff.EmployeeName = ds.EmployeeName;
            objDepartmentStaff.EmployeeID = ds.EmployeeID;
            obj.Add(objDepartmentStaff);
        }
        return obj;
    }

    //Method for populate Department dropdown 
    [WebMethod]
    public static List<Department> BindDepartment()
    {
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        List<Department> obj = new List<Department>();
        var AssignList = from d in ctx.Departments where d.DepartmentID != 1 orderby d.DepartmentName select d;
        foreach (var ds in AssignList)
        {
            Department objDepartment = new Department();
            objDepartment.DepartmentName = ds.DepartmentName;
            objDepartment.DepartmentID = ds.DepartmentID;
            obj.Add(objDepartment);
        }
        return obj;
    }
}
