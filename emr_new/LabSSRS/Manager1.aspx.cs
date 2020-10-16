using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using NineRays.WebControls;
using System.Text;


public partial class Manager : LMCBase
{
	EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
	{

		if (!IsPostBack)
		{
			FlyTreeNode AlphaNode = new FlyTreeNode("%");
			AlphaNode.ImageUrl = "$vista_folder";
			AlphaNode.Expanded = false;
			AlphaNode.PopulateNodesOnDemand = true;
			Tests.Nodes[0].ChildNodes.Add(AlphaNode);
			AlphaNode = new FlyTreeNode("(");
			AlphaNode.ImageUrl = "$vista_folder";
			AlphaNode.Expanded = false;
			AlphaNode.PopulateNodesOnDemand = true;
			Tests.Nodes[0].ChildNodes.Add(AlphaNode);
			byte thisChar = (byte)'A';
			while ((char)thisChar != 'Z')
			{
				AlphaNode = new FlyTreeNode(((char)thisChar).ToString());
				AlphaNode.ImageUrl = "$vista_folder";
				AlphaNode.Expanded = false;
				AlphaNode.PopulateNodesOnDemand = true;
				Tests.Nodes[0].ChildNodes.Add(AlphaNode);
				thisChar++;
			}
			AlphaNode = new FlyTreeNode("Z");
			AlphaNode.ImageUrl = "$vista_folder";
			AlphaNode.Expanded = false;
			AlphaNode.PopulateNodesOnDemand = true;
			Tests.Nodes[0].ChildNodes.Add(AlphaNode);

			PopulatePanels();
		}
	}

	protected void Panels_NodeInserted(object sender, FlyTreeNodeEventArgs e)
	{
		FlyTreeNode newNode = e.Node;

		switch (e.Node.Parent.Parent.Text)
		{
			case "Groups":
				{
					string GroupID = newNode.Parent.Value;
					string TestID = newNode.Value;
					newNode.ContextMenuID = "mnuOneTest";
					var theTest = (from t in ctx.LabReports_Tests where t.TestID == int.Parse(TestID) select t).First();
					theTest.Hidden = true;
					theTest.GroupID = int.Parse(GroupID);
					ctx.SubmitChanges();

					e.Node.ChildNodes.Clear();
					break;
				}
			case "Triggers":
				{
					string TriggerID = newNode.Parent.Value;
					string GroupID = newNode.Value;

					LabReports_TriggersGroup tg = new LabReports_TriggersGroup();
					tg.TriggerID = int.Parse(TriggerID);
					tg.GroupID = int.Parse(GroupID);
					ctx.LabReports_TriggersGroups.InsertOnSubmit(tg);
					ctx.SubmitChanges();

					e.Node.ChildNodes.Clear(); break;
				}
		}
	}


	protected void mnuTest_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		switch (e.CommandName)
		{
			case "Hide":
				{
					FlyTreeNode theNode = Tests.FindByID(e.CommandArgument);

					var theTest = (from t in ctx.LabReports_Tests where t.TestID == int.Parse(theNode.Value) select t).First();

					theTest.Hidden = true;
					ctx.SubmitChanges();

					if (theNode.Parent.ChildNodes.Count == 1)
					{
						theNode.Parent.Remove();
					}
					else
					{
						theNode.Remove();
					}
					break;
				}
		}
	}

	protected void mnuPanel_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		txtPanelName.Text = "";
		edPanelDescrip.Content = "";
		Session["PanelID"] = "0";
		modPanelInfo.Show();
	}

	protected void mnuGroups_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{

		txtGroupHigh.Text = "";
		txtGroupLow.Text = "";

		txtGroupTitle.Text = "";
		edDescrip.Content = "";
		edHigh.Content = "";
		edLow.Content = "";
		edNormal.Content = "";
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		Session["GroupID"] = 0;
		Session["PanelID"] = theNode.Parent.Value;
		modGroups.Show();
	}

	protected void mnuOneGroup_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		switch (e.CommandName)
		{
			case "Edit":
				{
					Session["GroupID"] = theNode.Value;
					Session["PanelID"] = theNode.Parent.Parent.Value;
					if (Session["GroupID"].ToString() != "0")
					{
						var theGroup = (from g in ctx.LabReports_Groups where g.GroupID == int.Parse(Session["GroupID"].ToString()) select g).First();

						txtGroupHigh.Text = theGroup.LongevityHighValue.ToString();
						txtGroupLow.Text = theGroup.LongevityLowValue.ToString();
						//txtGroupName.Text = reader["GroupName"].ToString();
						txtGroupTitle.Text = theGroup.GroupTitle;
						edDescrip.Content = theGroup.Description;
						edHigh.Content = theGroup.HighText;
						edLow.Content = theGroup.LowText;
						edNormal.Content = Server.HtmlEncode(theGroup.NormalText);

					}
				}


				modGroups.Show();
				break;

			case "Remove":
				{
					FlyTreeNode thisNode = Panels.FindByID(e.CommandArgument);

					var dbTests = from t in ctx.LabReports_Tests where t.GroupID == int.Parse(thisNode.Value) select t;

					foreach (var test in dbTests)
					{
						test.GroupID = 0;
						test.Hidden = false;
						ctx.SubmitChanges();
					}

					ctx.LabReports_Groups.DeleteOnSubmit((from g in ctx.LabReports_Groups where g.GroupID == int.Parse(theNode.Value) select g).First());
					ctx.SubmitChanges();

					PopulatePanels();
					break;

				}
		}
	}

	protected void mnuOnePanel_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		switch (e.CommandName)
		{
			case "Edit":
				{

					var thePanel = (from p in ctx.LabReports_Panels where p.PanelID == int.Parse(theNode.Value) select p).First();


					txtPanelName.Text = thePanel.PanelName;
					edPanelDescrip.Content = thePanel.PanelDescrip;
					Session["PanelID"] = theNode.Value;
					modPanelInfo.Show();
					break;
				}
			case "Remove":
				{
					string PanelID = theNode.Value;

					var dbGroups = from g in ctx.LabReports_Groups where g.PanelID == int.Parse(PanelID) select g;
					foreach (var thisGroup in dbGroups)
					{
						var dbTests = from t in ctx.LabReports_Tests where t.GroupID == thisGroup.GroupID select t;
						foreach (var test in dbTests)
						{
							test.Hidden = false;
							test.GroupID = 0;
							ctx.SubmitChanges();
						}
						ctx.LabReports_Groups.DeleteOnSubmit((from g in ctx.LabReports_Groups where g.GroupID == thisGroup.GroupID select g).First());
						ctx.SubmitChanges();

					}

					ctx.LabReports_Panels.DeleteOnSubmit((from p in ctx.LabReports_Panels where p.PanelID == int.Parse(PanelID) select p).First());
					ctx.SubmitChanges();
					break;
				}
		}
	}

	protected void mnuOneTest_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);

		var theTest = (from t in ctx.LabReports_Tests where t.TestID == int.Parse(theNode.Value) select t).First();

		theTest.GroupID = 0;
		theTest.Hidden = false;
		ctx.SubmitChanges();


		Response.Redirect("Manager.aspx");

	}

	protected void btnOkPanelInfo_Click(object sender, EventArgs e)
	{

		if ((string)Session["PanelID"] == "0")
		{
			LabReports_Panel pnl = new LabReports_Panel();
			pnl.PanelName = txtPanelName.Text;
			pnl.PanelDescrip = edPanelDescrip.Content;
			ctx.LabReports_Panels.InsertOnSubmit(pnl);
			ctx.SubmitChanges();
			FlyTreeNode newPanel = new FlyTreeNode(txtPanelName.Text);
			Panels.Nodes[0].ChildNodes.Add(newPanel);
		}
		else
		{
			var thePanel = (from p in ctx.LabReports_Panels where p.PanelID == int.Parse((string)Session["PanelID"]) select p).First();
			thePanel.PanelName = txtPanelName.Text;
			thePanel.PanelDescrip = edPanelDescrip.Content;
			ctx.SubmitChanges();
			FlyTreeNode theNode = Panels.FindByValue((string)Session["PanelID"]);
			theNode.Text = txtPanelName.Text;
		}
		PopulatePanels();
	}

	protected void btnOkGroupInfo_Click(object sender, EventArgs e)
	{

		if (Session["GroupID"].ToString() == "0")
		{
			LabReports_Group theGroup = new LabReports_Group();
			theGroup.PanelID = int.Parse(Session["PanelID"].ToString());
			theGroup.GroupName = txtGroupTitle.Text;
			theGroup.GroupTitle = txtGroupTitle.Text;
			theGroup.Description = edDescrip.Content;
			theGroup.HighText = edHigh.Content;
			theGroup.NormalText = edNormal.Content;
			theGroup.LowText = edLow.Content;
			theGroup.LongevityHighValue = decimal.Parse(txtGroupHigh.Text);
			theGroup.LongevityLowValue = decimal.Parse(txtGroupLow.Text);
			ctx.LabReports_Groups.InsertOnSubmit(theGroup);
			ctx.SubmitChanges();
		}
		else
		{
			var theGroup = (from g in ctx.LabReports_Groups where g.GroupID == int.Parse(Session["GroupID"].ToString()) select g).First();

			theGroup.GroupName = txtGroupTitle.Text;
			theGroup.GroupTitle = txtGroupTitle.Text;
			theGroup.Description = edDescrip.Content;
			theGroup.HighText = edHigh.Content;
			theGroup.NormalText = Server.HtmlDecode(edNormal.Content);
			theGroup.LowText = edLow.Content;
			theGroup.LongevityHighValue = decimal.Parse(txtGroupHigh.Text);
			theGroup.LongevityLowValue = decimal.Parse(txtGroupLow.Text);

			ctx.SubmitChanges();
		}

		Session["GroupID"] = null;
		Session["PanelID"] = null;


	}

	protected void mnuTriggers_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		Session["PanelID"] = theNode.Parent.Value;
		Session["TriggerID"] = "0";
		modTrigger.Show();
	}


	protected void btnTriggerOk_Click(object sender, EventArgs e)
	{

		if ((string)Session["TriggerID"] == "0")
		{
			LabReports_Trigger newTrigger = new LabReports_Trigger();
			newTrigger.TriggerName = txtTriggerName.Text;
			newTrigger.TriggerDescription = edTriggerDesc.Content;
			newTrigger.PanelID = int.Parse((string)Session["PanelID"]);
			ctx.LabReports_Triggers.InsertOnSubmit(newTrigger);
			ctx.SubmitChanges();
		}
		else
		{
			var theTrigger = (from t in ctx.LabReports_Triggers where t.TriggerID == int.Parse((String)Session["TriggerID"]) select t).First();
			theTrigger.TriggerName = txtTriggerName.Text;
			theTrigger.TriggerDescription = edTriggerDesc.Content;
			ctx.SubmitChanges();
		}

		PopulatePanels();


	}

	protected void mnuOneTrigger_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		switch (e.CommandName)
		{
			case "Edit":
				{
					Session["TriggerID"] = theNode.Value;
					var theTrigger = (from t in ctx.LabReports_Triggers where t.TriggerID == int.Parse(theNode.Value) select t).First();
					txtTriggerName.Text = theTrigger.TriggerName;
					edTriggerDesc.Content = theTrigger.TriggerDescription;

					modTrigger.Show();
					break;
				}
			case "Remove":
				{

					//reomve groups
					var triggerGroups = from tg in ctx.LabReports_TriggersGroups where tg.TriggerID == int.Parse(theNode.Value) select tg;
					foreach (var triggerGroup in triggerGroups)
					{
						ctx.LabReports_TriggersGroups.DeleteOnSubmit((from tg in ctx.LabReports_TriggersGroups where tg.TriggerGroupID == triggerGroup.TriggerGroupID select tg).First());
					}

					//remove Conditions
					foreach (FlyTreeNode Condition in theNode.ChildNodes[0].ChildNodes)
					{
						string ConditionID = Condition.Value;

						//remove ConditiomDetails
						var theConditions = from cd in ctx.LabReports_ConditionDetails where cd.ConditionID == int.Parse(ConditionID) select cd;
						foreach (var condition in theConditions)
						{
							ctx.LabReports_ConditionDetails.DeleteOnSubmit((LabReports_ConditionDetail)condition);
						}

						ctx.LabReports_Conditions.DeleteOnSubmit((from c in ctx.LabReports_Conditions where c.ConditionID == int.Parse(ConditionID) select c).First());

					}

					ctx.LabReports_Triggers.DeleteOnSubmit((from t in ctx.LabReports_Triggers where t.TriggerID == int.Parse(theNode.Value) select t).First());
					ctx.SubmitChanges();

					PopulatePanels();

					break;
				}
		}
	}

	protected void mnuConditions_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		switch (e.CommandName)
		{
			case "Add":
				{
					Session["ActiveNode"] = FindPanelName(theNode);
					Session["TriggerID"] = theNode.Parent.Value;
					Session["ConditionID"] = "0";
					txtConditionName.Text = "";
					edConditionDescrip.Content = "";
					modCondition.Show();
					break;
				}
		}
	}

	private void PopulateDetailGrid()
	{
		PopulateDetailGrid(Session["TriggerID"].ToString(), Session["ConditionID"].ToString());
	}


	private void PopulateDetailGrid(string TriggerID, string ConditionID)
	{

		var theTriggerGroups = from tg in ctx.LabReports_TriggersGroups where tg.TriggerID == int.Parse(TriggerID) select tg;
		//DropDownList ddlInsertGroups = (DropDownList)grdConditions.FooterRow.Cells[1].Controls[1];
		ddlInsertGroups.Items.Clear();


		foreach (var tGroup in theTriggerGroups)
		{
			ddlInsertGroups.Items.Add(new ListItem(tGroup.LabReports_Group.GroupName, tGroup.LabReports_Group.GroupID.ToString()));
		}
		ddlInsertGroups.Items[0].Selected = true;



		grdConditions.DataSource = from cd in ctx.LabReports_ConditionDetails
								   where cd.ConditionID == int.Parse(ConditionID)
								   select new
								   {
									   GroupName = cd.LabReports_Group.GroupName,
									   Operator = cd.Operator,
									   Value = cd.Value,
									   OptionalOperator = cd.AndOperator,
									   OptionalValue = cd.AndValue,
									   ID = cd.ConditionDetailID,
									   ConditionName = cd.LabReports_Condition.ConditionName,
								   };
		grdConditions.DataBind();
		grdConditions.HeaderRow.Cells[0].Text = "Current Details";

	}

	protected void btnConditionOk_Click(object sender, EventArgs e)
	{
		if (Session["ConditionID"].ToString() == "0")
		{
			LabReports_Condition condition = new LabReports_Condition();
			condition.ConditionName = txtConditionName.Text;
			condition.ConditionDescrip = edConditionDescrip.Content;
			condition.TriggerID = int.Parse(Session["TriggerID"].ToString());
			condition.Sex = lstSex.SelectedValue;
			ctx.LabReports_Conditions.InsertOnSubmit(condition);
		}
		else
		{
			var theCondition = (from c in ctx.LabReports_Conditions where c.ConditionID == int.Parse(Session["ConditionID"].ToString()) select c).First();
			theCondition.ConditionName = txtConditionName.Text;
			theCondition.ConditionDescrip = edConditionDescrip.Content;
			theCondition.Sex = lstSex.SelectedValue;
		}
		ctx.SubmitChanges();
		PopulatePanels();
	}


	protected void btnInsertDetail_Click(object sender, EventArgs e)
	{

		LabReports_ConditionDetail detail = new LabReports_ConditionDetail();
		detail.GroupID = int.Parse(ddlInsertGroups.SelectedValue);
		detail.Operator = ddlOperator.SelectedItem.Text;
		detail.Value = double.Parse(txtValue.Text);
		detail.AndOperator = ddlOptOperator.SelectedItem.Text;
		if (txtOptValue.Text != "")
			detail.AndValue = double.Parse(txtOptValue.Text);
		detail.ConditionID = int.Parse(Session["ConditionID"].ToString());

		ctx.LabReports_ConditionDetails.InsertOnSubmit(detail);
		ctx.SubmitChanges();
		PopulateDetailGrid();
		modDetails.Show();



	}

	protected void grdConditions_SelectedIndexChanged(object sender, EventArgs e)
	{
		string ID = grdConditions.SelectedRow.Cells[6].Text;

		ctx.LabReports_ConditionDetails.DeleteOnSubmit((from cd in ctx.LabReports_ConditionDetails where cd.ConditionDetailID == int.Parse(ID) select cd).First());
		ctx.SubmitChanges();

	}

	protected void mnuOneCondition_Command(Object sender, FlyContextMenuCommandEventArgs e)
	{
		FlyTreeNode theNode = Panels.FindByID(e.CommandArgument);
		Session["ActiveNode"] = FindPanelName(theNode);
		switch (e.CommandName)
		{
			case "Edit":
				Session["ConditionID"] = theNode.Value;

				var theCondition = (from c in ctx.LabReports_Conditions where c.ConditionID == int.Parse(theNode.Value) select c).First();


				txtConditionName.Text = theCondition.ConditionName;
				edConditionDescrip.Content = theCondition.ConditionDescrip;
				modCondition.Show();

				break;
			case "Details":
				Session["TriggerID"] = theNode.Parent.Parent.Value;
				Session["ConditionID"] = theNode.Value;
				PopulateDetailGrid(theNode.Parent.Parent.Value, theNode.Value);
				modDetails.Show();
				break;
			case "Remove":

				ctx.LabReports_Conditions.DeleteOnSubmit((from c in ctx.LabReports_Conditions where c.ConditionID == int.Parse(theNode.Value) select c).First());
				ctx.LabReports_ConditionDetails.DeleteAllOnSubmit(from cd in ctx.LabReports_ConditionDetails where cd.ConditionID == int.Parse(theNode.Value) select cd);
				ctx.SubmitChanges();
				PopulatePanels();

				break;

		}
	}

	private void PopulatePanels()
	{
		Panels.Nodes[0].ChildNodes.Clear();


		var dbPanels = from p in ctx.LabReports_Panels select p;

		foreach (var panel in dbPanels)
		{
			FlyTreeNode newNode = new FlyTreeNode(panel.PanelName);
			newNode.ImageUrl = "$vista_folder";
			newNode.ContextMenuID = "mnuOnePanel";
			newNode.Value = panel.PanelID.ToString();
			if (newNode.Text == (string)Session["ActiveNode"])
			{ newNode.Expanded = true; }
			FlyTreeNode groupNode = new FlyTreeNode("Groups");
			groupNode.ImageUrl = "$vista_folder";
			groupNode.ContextMenuID = "mnuGroups";
			newNode.ChildNodes.Add(groupNode);
			newNode.DragDropAcceptNames = "Group";
			newNode.Expanded = false;
			FlyTreeNode TriggerNode = new FlyTreeNode("Triggers");
			TriggerNode.ImageUrl = "$vista_folder";
			TriggerNode.ContextMenuID = "mnuTriggers";
			newNode.ChildNodes.Add(TriggerNode);

			var dbGroups = from g in ctx.LabReports_Groups where g.PanelID == panel.PanelID select g;

			foreach (var theGroup in dbGroups)
			{
				FlyTreeNode theChild = new FlyTreeNode(theGroup.GroupName);
				theChild.DragDropAcceptNames = "Test";
				theChild.DragDropName = "Group";
				theChild.ContextMenuID = "mnuOneGroup";
				theChild.Value = theGroup.GroupID.ToString();

				var dbTests = from t in ctx.LabReports_Tests where t.GroupID == theGroup.GroupID select t;

				foreach (var test in dbTests)
				{
					FlyTreeNode theTest = new FlyTreeNode(test.TestName);
					theTest.Value = test.TestID.ToString();
					theTest.DragDropAcceptNames = "";
					theTest.ContextMenuID = "mnuOneTest";
					theChild.ChildNodes.Add(theTest);
				}
				newNode.ChildNodes[0].ChildNodes.Add(theChild);

			}

			var dbTriggers = from t in ctx.LabReports_Triggers where t.PanelID == panel.PanelID select t;

			foreach (var trigger in dbTriggers)
			{
				FlyTreeNode theChild = new FlyTreeNode(trigger.TriggerName);
				theChild.Value = trigger.TriggerID.ToString();
				theChild.ContextMenuID = "mnuOneTrigger";
				theChild.DragDropAcceptNames = "Group";
				FlyTreeNode Conditions = new FlyTreeNode("Conditions");
				Conditions.ImageUrl = "$vista_folder";
				Conditions.ContextMenuID = "mnuConditions";
				theChild.ChildNodes.Add(Conditions);
				TriggerNode.ChildNodes.Add(theChild);

				//Add groups
				var triggerGroups = from tg in ctx.LabReports_TriggersGroups where tg.TriggerID == trigger.TriggerID select tg;

				foreach (var group in triggerGroups)
				{
					FlyTreeNode GroupNode = new FlyTreeNode(group.LabReports_Group.GroupName);
					GroupNode.ContextMenuID = "";
					GroupNode.DragDropAcceptNames = "";
					theChild.ChildNodes.Add(GroupNode);
				}

				//Add conditions
				var dbConditions = from c in ctx.LabReports_Conditions where c.TriggerID == trigger.TriggerID select c;

				foreach (var condiion in dbConditions)
				{
					FlyTreeNode CondtionNode = new FlyTreeNode(condiion.ConditionName);
					CondtionNode.Value = condiion.ConditionID.ToString();
					CondtionNode.ContextMenuID = "mnuOneCondition";
					CondtionNode.DragDropAcceptNames = "";
					Conditions.ChildNodes.Add(CondtionNode);
				}

			}

			Panels.Nodes[0].ChildNodes.Add(newNode);
		}
	}

	protected void Tests_PopulateNodes(object sender, FlyTreeNodeEventArgs e)
	{

		var theTests = ctx.LabReports_GetTestsByStartLetter(e.Node.Text);

		foreach (var test in theTests)
		{

			FlyTreeNode newNode = new FlyTreeNode(test.TestName);
			newNode.DragDropName = "Test";
			newNode.DragDropAcceptNames = "";
			newNode.Value = (test.TestID.ToString());
			newNode.ToolTip = "Last Date: " + (test.LastUsed).ToShortDateString();
			e.Node.ChildNodes.Add(newNode);

		}

	}

	private string FindPanelName(FlyTreeNode theNode)
	{
		while (theNode.Parent.Text != "Panels")
		{
			theNode = theNode.Parent;
		}
		return theNode.Text;
	}
}