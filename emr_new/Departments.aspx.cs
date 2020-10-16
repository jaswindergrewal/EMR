using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using NineRays.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using Microsoft.Office.Interop.Word;

public partial class Departments : LMCBase
{
    #region Variable
    IDepartmentService objService = null;
    #endregion
    #region Events
    /// <summary>
    /// Show all the departs in the tree view o  page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
		{
			Populate();		
		}
    }

	protected void mnuDept_Command(object sender, FlyContextMenuCommandEventArgs e)
	{
		modDept.Show();
	}

	/// <summary>
	/// Remove the departments from the tree node
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    protected void mnuStaff_Command(object sender, FlyContextMenuCommandEventArgs e)
	{
        try
        {
            FlyTreeNode theNode = Dept.FindByID(e.CommandArgument);

            objService = new DepartmentService();
            objService.DeleteDepartments(int.Parse(theNode.Value));
            theNode.Remove();
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
	/// Insert New depatment in the tree node
	/// </summary>
	/// <param name="sender"></param>
	/// <param name="e"></param>
    protected void btnOkDept_Click(object sender, EventArgs e)
	{
        try
        {
            DepartmentViewModel dept = new DepartmentViewModel();
            dept.DepartmentName = txtDeptName.Text;
            objService = new DepartmentService();
            dept = objService.InsertDepartments(dept);
            FlyTreeNode theNode = new FlyTreeNode(dept.DepartmentName, dept.DepartmentID.ToString());
            theNode.DragDropAcceptNames = "staff";
            Dept.Nodes[0].ChildNodes.Add(theNode);
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
    #region Methods
    /// <summary>
	/// Show all the departs in the tree view
	/// </summary>
    private void Populate()
	{
        try
        {
            Dept.Nodes[0].ChildNodes.Clear();
            objService = new DepartmentService();
            List<DepartmentViewModel> deparments = objService.GetDepartments();
            foreach (var d in deparments)
            {
                FlyTreeNode newNode = new FlyTreeNode(d.DepartmentName);
                newNode.Value = d.DepartmentID.ToString();
                newNode.DragDropAcceptNames = "staff";
                Dept.Nodes[0].ChildNodes.Add(newNode);
                List<DepartmentStaffViewModel> deptStaff = objService.GetDepartmentStaff(d.DepartmentID);//from s in ctx.DepartmentStaffs where s.DepartmentID == d.DepartmentID orderby s.Staff.EmployeeName select s;
                foreach (var s in deptStaff)
                {
                    FlyTreeNode staffNode = new FlyTreeNode(s.EmployeeName);
                    staffNode.DragDropName = "staff";
                    staffNode.DragDropAcceptNames = "";
                    staffNode.Value = s.DepartmentStaffID.ToString();
                    if (d.DepartmentID != 1) staffNode.ContextMenuID = "mnuStaff";
                    newNode.ChildNodes.Add(staffNode);
                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
    }
    #endregion
}