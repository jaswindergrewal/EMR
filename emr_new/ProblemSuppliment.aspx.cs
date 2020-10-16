using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using NineRays.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;
using System.Linq;

public partial class ProblemSuppliment : System.Web.UI.Page
{
    AutoshipUtilities util = new AutoshipUtilities();
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {

            util.PopulateProblemProducts(SupplimentList);

            //FlyTreeNode SkedRoot = new FlyTreeNode("Currently defined groups");
            //SkedRoot.ImageUrl = "$vista_folder";
            //SkedRoot.DragDropAcceptNames = "";
            //SkedRoot.Expanded = true;
            //SkedRoot.ContextMenuID = "AllGroups";
            //Sked.Nodes.Add(SkedRoot);
            util.PopulateProblemList(Sked, 0);
            //Session["SkedGroups"] = util.CopyList((List<ScheduleGroup>)Session["ChangedGroups"]);
        }
    }

    protected virtual void OnNodeMoved(object sender, FlyTreeNodeEventArgs e)
    {
        lblNotSaved.Visible = true;

        int currNodeIndex = 0;

        foreach (FlyTreeNode node in e.Node.Parent.ChildNodes)
        {
            if (node.Text.Contains(e.Node.Text))
            {

                //e.Node.Text = "<font color='purple'> " + e.Node.Text + " </font>";

                currNodeIndex++;

            }


        }
        if (currNodeIndex > 1)
        {
            e.Node.Remove();
        }
        else
        {
            e.Node.Text = "<font color='purple'> " + e.Node.Text + " </font>";
        }





    }

    protected void btnCancelChanges_Click(object sender, EventArgs e)
    {
        Sked.Nodes.Clear();
        util.PopulateProblemList(Sked, 0);
        lblNotSaved.Visible = false;

    }


    /// <summary>
    /// Fires to save changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        util.SaveProblemItems(Sked);
        lblNotSaved.Visible = false;

    }

}