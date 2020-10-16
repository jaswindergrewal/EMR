using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Web.Services;

public partial class DepartmentStaff_Add : System.Web.UI.Page
{
    IStaffService ObjStaffService = null;
    IDepartmentService ObjDepartments = null;

    //page load method for getting the details for the jqgrid and bind dropdowns
    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            BindStaff();
            GetDepartments();

            if (!string.IsNullOrEmpty(Request.QueryString["gridFill"]))
            {

                string _search = Request.QueryString["_search"];
                string nd = Request.QueryString["_search"];
                int rows = Convert.ToInt16(Request.QueryString["rows"]);
                int page = Convert.ToInt16(Request.QueryString["page"]);
                string sidx = Request.QueryString["sidx"];
                string sord = Request.QueryString["sord"];
                string SearchField = string.Empty;
                string searchString = string.Empty;
                if (_search == "true")
                {
                    SearchField = Request.QueryString["searchField"];
                    searchString = Request.QueryString["searchString"];
                }
                Response.Clear();
                string strTicketData = BindGridData(Request.QueryString["gridFill"], _search, nd, rows, page, sidx, sord, SearchField, searchString);
                Response.ContentType = "application/json";
                Response.Write(strTicketData);
                Response.End();
                Response.Flush();
            }

        }
    }

    //Bind the employees detail on the basis of departmentid
    public string BindGridData(string DepartmentID, string _search, string nd, int rows, int page, string sidx, string sord, string SearchField, string searchString)
    {
        IDepartmentStaffService objService = null;
        List<StaffViewModel> lstStaff = null;
        string strSerializeData = "";
        int IsSearch = 0;
        int intTotalPages = 0;
        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new DepartmentStaffService();
            if (_search == "true")
                IsSearch = 1;


            int DeptID = Convert.ToInt32(DepartmentID);

            lstStaff = objService.GetStaffDetails(page, rows, sord, sidx, IsSearch, SearchField, searchString, DeptID);

            if (lstStaff.Count > 0)
            {
                intTotalPages = lstStaff[0].RecordCount;
                intTotalPages = (intTotalPages / rows) + 1;
            }
            var ListEmployee = new
            {

                total = intTotalPages,
                page = page,
                records = rows,
                rows = (
                  from d in lstStaff
                  select new
                  {
                      EmployeeID = d.EmployeeID,
                      cell = new string[] {
                                d.EmployeeID.ToString(),d.EmployeeName,d.Email_Address,d.access_level
                              }
                  }).ToArray()
            };

            strSerializeData = serializer.Serialize(ListEmployee);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

        finally
        {

            lstStaff = null;
            objService = null;
        }
        return strSerializeData;
    }

    //Method to get all staff and bind it with dropdown list
    public void BindStaff()
    {
        try
        {
            ObjStaffService = new StaffService();
            ddlStaff.DataSource = ObjStaffService.GetStaff();
            ddlStaff.DataTextField = "EmployeeName";
            ddlStaff.DataValueField = "EmployeeID";
            ddlStaff.DataBind();
            ddlStaff.Items.Insert(0, new ListItem("Select a staff"));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            ObjStaffService = null;
        }
    }


    //Method to get all departments and bind it with checkbox list
    public void GetDepartments()
    {
        ObjDepartments = new DepartmentService();
        List<DepartmentViewModel> dept = ObjDepartments.GetDepartments();
        cbDepartment.DataSource = dept;
        cbDepartment.DataTextField = "DepartmentName";
        cbDepartment.DataValueField = "DepartmentID";
        cbDepartment.DataBind();

        ddlDepartments.DataSource = dept;
        ddlDepartments.DataTextField = "DepartmentName";
        ddlDepartments.DataValueField = "DepartmentID";
        ddlDepartments.DataBind();
        ddlDepartments.Items.Insert(0, new ListItem("Select a department"));

    }


    //Method to insert staff and departments 
    [WebMethod]
    public static int SaveDepartmentStaff(int StaffID, string DepartmentID)
    {
        IDepartmentStaffService objServiceDeptStaff = null;
        int result;
        try
        {
            objServiceDeptStaff = new DepartmentStaffService();
            result = objServiceDeptStaff.SaveDepartmentStaff(StaffID, DepartmentID);

        }
        catch (System.Exception ex)
        {
            result = -1;
        }
        finally
        {
            objServiceDeptStaff = null;
        }
        return result;
    }



    //Method to insert departments 
    [WebMethod]
    public static bool SaveDepartments(string DepartmentName)
    {
        DepartmentService objService = null;
        bool result = true;
        try
        {
            DepartmentViewModel dept = new DepartmentViewModel();
            dept.DepartmentName = DepartmentName;

            objService = new DepartmentService();
            dept = objService.InsertDepartments(dept);

        }
        catch (System.Exception ex)
        {
            result = false;
        }
        finally
        {
            objService = null;
        }
        return result;

    }

    //Method to insert departments 
    [WebMethod]
    public static List<DepartmentViewModel> GetCheckBoxDetails()
    {
        IDepartmentService objService = null;
        List<DepartmentViewModel> chkListDepartment = null;


        try
        {
            objService = new DepartmentService();
            chkListDepartment = objService.GetDepartments();

        }

        finally
        {
            objService = null;
        }
        return chkListDepartment;

    }


    //Method to insert departments 
    [WebMethod]
    public static List<DepartmentViewModel> GetDepartmentStaff(int staffID)
    {
        IDepartmentService objService = null;
        List<DepartmentViewModel> chkListDepartment = null;


        try
        {
            objService = new DepartmentService();
            chkListDepartment = objService.GetDepartmentsforStaff(staffID);

        }

        finally
        {
            objService = null;
        }
        return chkListDepartment;

    }



}