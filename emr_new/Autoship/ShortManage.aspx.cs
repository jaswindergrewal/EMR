using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using NineRays.WebControls;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web.UI.WebControls;

using System.Data.SqlClient;
using System.Configuration;

public partial class _ShortManage : LMCBase
{
    //Track PatientID and instantialte a utilites object
    int PatientID = 0;
    AutoshipUtilities util = new AutoshipUtilities();

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];

        else
            this.MasterPageFile = "site.master";

    }

    /// <summary>
    /// Page Load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {

        if (Request.QueryString["PatientID"] != null)
            PatientID = int.Parse(Request.QueryString["PatientID"]);
        else
            Response.Redirect("blnak.htm");

        pat.PatientID = PatientID;
        //BindGridSuppliments(pat.PatientID);

        if (Session["NodeID"] == null) GroupPopUpTitle.InnerText = "Create Group"; else GroupPopUpTitle.InnerText = "Modify Group";
        //Commented by jaswinder to create the order in current date also but not less than that.
        //valFuture.ValueToCompare = DateTime.Now.AddDays(3).ToShortDateString();
        valFuture.ValueToCompare = DateTime.Now.ToShortDateString();


        if (Request.QueryString["StaffID"] != null)
        {
            User usr = new User(Request.QueryString["StaffID"].ToString());
            Session["MM_Username"] = usr.EmployeeName;
            Session["UserID"] = usr.EmployeeID;
            Session["StaffID"] = usr.EmployeeID;
        }
        else
        {
            User usr = new User(Session["StaffID"].ToString());
            Session["MM_Username"] = usr.EmployeeName;
            Session["UserID"] = usr.EmployeeID;
            util.StaffID = usr.EmployeeID;
        }

        if (!IsPostBack)
        {
            txtEMRCity.Text = pat.City;
            txtEMRState.Text = pat.State;
            txtEMRStreet.Text = pat.StreetAddress;
            txtEMRZip.Text = pat.Zip;
            chkCallBeforeShip.Checked = (bool)pat.CallBeforeShip;
            util.PopulateProducts(Tester,pat.PatientID);
            util.PopulateProducts(Products,pat.PatientID);
            util.PopulateSuppliments(SupplimentList, pat.PatientID);

            FlyTreeNode SkedRoot = new FlyTreeNode("Currently defined groups");
            SkedRoot.ImageUrl = "$vista_folder";
            SkedRoot.DragDropAcceptNames = "";
            SkedRoot.Expanded = true;
            SkedRoot.ContextMenuID = "AllGroups";
            Sked.Nodes.Add(SkedRoot);
            Session["OriginalStanding"] = util.PopulateTree(Sked, PatientID);
            Session["ChangedGroups"] = util.PopulateFuture(Future, PatientID);
            Session["SkedGroups"] = util.CopyList((List<ScheduleGroup>)Session["ChangedGroups"]);
        }
    }

    //public void BindGridSuppliments(int patientId)
    //{
    //    IPrescriptionService objPrescriptionService = null;
    //    List<PrescriptionSupplierViewModel> lstSupplierDetails = null;
    //    try
    //    {
    //        lstSupplierDetails = new List<PrescriptionSupplierViewModel>();
    //        objPrescriptionService = new PrescriptionService();
    //        lstSupplierDetails = objPrescriptionService.GetSupplementsDetails(PatientID).ToList();
    //        grdSupps.DataSource = lstSupplierDetails;
    //        grdSupps.DataBind();

    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
    //    }
    //    finally
    //    {
    //        objPrescriptionService = null;
    //        lstSupplierDetails = null;
    //    }
    //}

    /// <summary>
    /// Fires when a node is dragged
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected virtual void OnNodeMoved(object sender, FlyTreeNodeEventArgs e)
    {
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
        btnEmail.Enabled = false;
        bool firstTime = true;
        int currNodeIndex = 0;
        if (e.Node.Parent.Text.Contains("Ship Date"))
        {
            Session["Tree"] = "Future";
        }
        else
        {
            Session["Tree"] = "Sked";
        }
        foreach (FlyTreeNode node in e.Node.Parent.ChildNodes)
        {
            if (node.Text.Contains(e.Node.Text) && node.Text.Contains("Quantity:"))
            {
                firstTime = false;
                break;
            }
            currNodeIndex++;
        }




        if (firstTime || (string)Session["Tree"] == "Future")
        {
            if (!firstTime && (string)Session["Tree"] == "Future")
            {
                e.Node.Remove();
                return;
            }
            e.Node.Text = "<font color='purple'> " + e.Node.Text + " </font><font color='green'>Quantity: 1 </font><font color='blue'> Discount: 10% off </font>";
            if ((string)Session["Tree"] != "Future")
                e.Node.ContextMenuID = "ItemMenu";
            else
                e.Node.ContextMenuID = "ItemAddressMenu";

            if ((string)Session["Tree"] == "Future")
            {
                Item newItem = util.AddOneTimeItem((List<ScheduleGroup>)Session["ChangedGroups"], e.Node);

                if (newItem.ProfileItemID != 0 || (newItem.ProfileItemID == 0 && newItem.Quantity > 1))
                {
                    FlyTreeNode currNode = Future.FindByValue(newItem.ID.ToString());
                    string oldText = currNode.Text;
                    string[] oldArray = oldText.Split(' ');

                    int QuantElem = 0;
                    foreach (string x in oldArray)
                    {
                        if (x.Contains("Quantity:"))
                        {
                            break;
                        }
                        QuantElem++;
                    }

                    int quantity = int.Parse(oldArray[QuantElem + 1]);


                    quantity++;
                    oldArray[QuantElem + 1] = (quantity).ToString();
                    if (oldText.Contains("'purple'"))
                        currNode.Text = "";
                    else
                        currNode.Text = "<font color='purple'>";
                    foreach (string xx in oldArray)
                    {
                        if (oldText.Contains("'purple'"))
                        {
                            currNode.Text += xx + " ";
                        }
                        else
                        {
                            currNode.Text += xx.Replace(" </font><font color='green'>", " <font color='green'>") + " ";
                        }
                    }
                    currNode.Text = currNode.Text.Trim();
                    e.Node.Remove();

                }
            }
        }
        else
        {
            FlyTreeNode currNode = e.Node.Parent.ChildNodes[currNodeIndex];
            string oldText = currNode.Text;
            string[] oldArray = oldText.Split(' ');

            int QuantElem = 0;
            foreach (string x in oldArray)
            {
                if (x.Contains("Quantity:"))
                {
                    break;
                }
                QuantElem++;
            }
            int quantity = int.Parse(oldArray[QuantElem + 1]);


            quantity++;
            oldArray[QuantElem + 1] = (quantity).ToString();
            if (oldText.Contains("'purple'"))
                currNode.Text = "";
            else
                currNode.Text = "<font color='purple'>";
            foreach (string xx in oldArray)
            {
                if (oldText.Contains("'purple'"))
                {
                    currNode.Text += xx + " ";
                }
                else
                {
                    currNode.Text += xx.Replace(" </font><font color='green'>", " <font color='green'>") + " ";
                }
            }
            currNode.Text = currNode.Text.Trim();
            if ((string)Session["Tree"] == "Future")
            {
                util.ChangeQuantity((List<ScheduleGroup>)Session["ChangedGroups"], int.Parse(currNode.Value), quantity, true);
            }
            e.Node.Remove();
        }

    }

    /// <summary>
    /// Fires when a cotect menu item is clicked for an individual item
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public void ItemMenu_Command(Object sender, FlyContextMenuCommandEventArgs e)
    {
        //List<ScheduleGroup> tester = (List<ScheduleGroup>)Session["ChangedGroups"];
        //FlyContextMenu theMenu = (FlyContextMenu)sender;
        FlyTreeNode remNode = Sked.FindByID(e.CommandArgument);
        Session["Tree"] = "Sked";
        if (remNode == null)
        {
            remNode = Future.FindByID(e.CommandArgument);
            Session["Tree"] = "Future";
        }
        FlyTreeNode currNode = remNode;
        FlyTreeNode redNode = currNode;
        switch (e.CommandName)
        {
            case "Affiliate":
                int ProfId = int.Parse(remNode.Value);
                IProfileItemService objIProfileItemService = null;
                objIProfileItemService = new ProfileItemService();
                ProfileItemViewModel profItem = objIProfileItemService.GetProfitItemList(ProfId);

                IPatientService objIPatientService = new PatientService();
                bool Affliliate = objIPatientService.GetPatientAffiliate(pat.PatientID);
                objIProfileItemService = new ProfileItemService();
                objIProfileItemService.UpdateAffliliateInProfileItems(profItem.ProfileItemID, Affliliate);

                //ctx.SubmitChanges();
                util.PopulateTree(Sked, PatientID);
                break;
            case "Remove":
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;

                util.RemoveItem((List<ScheduleGroup>)Session["ChangedGroups"], remNode, (string)Session["Tree"], Future, (List<ScheduleGroup>)Session["SkedGroups"]);
                if (Session["Tree"].ToString() == "Future")
                {
                    string[] pArray = remNode.Text.Split(' ');
                    for (int x = 0; x < pArray.Length; x++)
                    {
                        if (pArray[x].Contains("Quantity"))
                        {
                            pArray[x + 1] = "0";
                            break;
                        }
                    }
                    remNode.Text = "";
                    foreach (string st in pArray)
                    {
                        remNode.Text += st + " ";
                    }
                    remNode.Text = remNode.Text.Trim();
                    remNode.Text = remNode.Text.Replace("'blue'", "'purple'");
                    remNode.Text = remNode.Text.Replace("<font color='purple'>Discount", "<font color='blue'>Discount");
                }
                if (Session["Tree"].ToString() == "Sked") remNode.Remove();
                break;
            case "Reduce":
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;
                string oldText = currNode.Text;
                string[] oldArray = oldText.Split(' ');

                int QuantElem = 0;
                foreach (string x in oldArray)
                {
                    if (x.Contains("Quantity:"))
                    {
                        break;
                    }
                    QuantElem++;
                }
                int quantity = int.Parse(oldArray[QuantElem + 1]);


                quantity--;
                if (quantity == 0)
                {
                    util.RemoveItem((List<ScheduleGroup>)Session["ChangedGroups"], currNode, (string)Session["Tree"], Future, (List<ScheduleGroup>)Session["SkedGroups"]);
                    currNode.Remove();
                }
                else
                {
                    oldArray[QuantElem + 1] = (quantity).ToString();
                    if (oldText.Contains("'purple'"))
                        currNode.Text = "";
                    else
                        currNode.Text = "<font color='purple'>";
                    foreach (string xx in oldArray)
                    {
                        if (oldText.Contains("'purple'"))
                        {
                            currNode.Text += xx + " ";
                        }
                        else
                        {
                            currNode.Text += xx.Replace(" </font><font color='green'>", " <font color='green'>") + " ";
                        }
                    }
                    currNode.Text = currNode.Text.Trim();
                    currNode.Text = currNode.Text.Replace("<font color='blue'>", "");
                }
                if ((string)Session["Tree"] == "Future")
                {
                    Item Prompt = util.ChangeQuantity((List<ScheduleGroup>)Session["ChangedGroups"], int.Parse(currNode.Value), quantity);
                }
                break;
            case "Increase":
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;

                string ioldText = redNode.Text;
                string[] ioldArray = ioldText.Split(' ');

                int iQuantElem = 0;
                foreach (string x in ioldArray)
                {
                    if (x.Contains("Quantity:"))
                    {
                        break;
                    }
                    iQuantElem++;
                }
                int iquantity = int.Parse(ioldArray[iQuantElem + 1]);


                iquantity++;
                if (iquantity == 0)
                {
                    util.RemoveItem((List<ScheduleGroup>)Session["ChangedGroups"], redNode, (string)Session["Tree"], Future, (List<ScheduleGroup>)Session["SkedGroups"]);
                    redNode.Remove();
                }
                else
                {
                    ioldArray[iQuantElem + 1] = (iquantity).ToString();
                    if (ioldText.Contains("'purple'"))
                        redNode.Text = "";
                    else
                        redNode.Text = "<font color='purple'>";
                    foreach (string xx in ioldArray)
                    {
                        if (ioldText.Contains("'purple'"))
                        {
                            redNode.Text += xx + " ";
                        }
                        else
                        {
                            redNode.Text += xx.Replace(" </font><font color='green'>", " <font color='green'>") + " ";
                        }
                    }
                    redNode.Text = redNode.Text.Trim();
                    redNode.Text = redNode.Text.Replace("<font color='blue'>", "");
                }
                if ((string)Session["Tree"] == "Future")
                {
                    util.ChangeQuantity((List<ScheduleGroup>)Session["ChangedGroups"], int.Parse(redNode.Value), iquantity);
                }
                break;
            case "Discount":
                Session["DiscountNodeID"] = e.CommandArgument;
                modDiscount.Show();
                break;
            case "Revert":
                string sProfileItemID = util.Revert(remNode.Value, (List<ScheduleGroup>)Session["SkedGroups"], (List<ScheduleGroup>)Session["ChangedGroups"]);
                Item itm = util.GetItem((List<ScheduleGroup>)Session["ChangedGroups"], int.Parse(remNode.Value));
                foreach (FlyTreeNode node in remNode.Parent.ChildNodes)
                {
                    if (node.Text.Contains(itm.ProductName))
                    {
                        if (node.Text.StartsWith("<font"))
                        {
                            node.Text = node.Text.Replace("'blue'", "'purple'");
                            node.Text = node.Text.Replace("'purple'> Discount", "'blue'> Discount");
                        }
                        else
                        {
                            node.Text = "<font color='purple'>" + node.Text.Replace("<font color='green'>", "</font><font color='green'>");
                        }

                    }
                }
                FlyTreeNode ParentNode = remNode.Parent;
                bool hasException = false;
                foreach (FlyTreeNode node in ParentNode.ChildNodes)
                {
                    if (node.Text.StartsWith("<font color='blue'>"))
                    {
                        hasException = true;
                    }
                }
                if (!hasException)
                {
                    ParentNode.Text = "<strong>" + util.RemoveHTML(ParentNode.Text) + "</strong>";
                }
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;
                break;
        }
    }

    /// <summary>
    /// Fires when OK is pressed in the Discount Modal
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOkDiscount_Click(object sender, EventArgs e)
    {
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
        btnEmail.Enabled = false;
        switch ((string)Session["Tree"])
        {
            case "Sked":
                util.UpdateDiscount((string)Session["DiscountNodeID"], Sked, ddDiscount.SelectedValue, ddDiscount.SelectedItem.Text);
                break;
            case "Future":
                util.UpdateDiscount((string)Session["DiscountNodeID"], Future, ddDiscount.SelectedValue, ddDiscount.SelectedItem.Text, true, (List<ScheduleGroup>)Session["ChangedGroups"]);
                break;
        }

    }
    /// <summary>
    /// Fires when a entry is clsicked in the Group context menu
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GroupMenu_Command(Object sender, FlyContextMenuCommandEventArgs e)
    {

        Session["NodeID"] = e.CommandArgument;
        switch (e.CommandName)
        {
            case "Cancel":
                modConfirmDelete.Show();
                break;
            case "Modify":
                //parse text on node
                FlyTreeNode modNode = Sked.FindByID(e.CommandArgument);
                string[] promptArray = modNode.Text.Split(' ');
                //fill in freq and next due date
                txtFreq.Text = promptArray[1];
                txtNextDue.Text = util.RemoveHTML(promptArray[5]);
                //display modal form
                GroupPopUpTitle.InnerText = "Modify Group";
                modCreateGroup.Show();
                break;

            //Added by jaswinder for provide the functionality of hold and unhold
            case "Hold":
                FlyTreeNode currNode = Sked.FindByID((string)Session["NodeID"]);
                currNode.ContextMenuID = "GroupMenuNew";
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;
                break;
        }
    }

    /// <summary>
    /// Added by jaswinder
    /// Purpose : to show the different menu item for the hold and unhold on the basis of which is currently active
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void GroupMenuNew_Command(Object sender, FlyContextMenuCommandEventArgs e)
    {

        Session["NodeID"] = e.CommandArgument;
        switch (e.CommandName)
        {

            //Added by jaswinder for provide the functionality of hold and unhold
            case "UnHold":
                FlyTreeNode currNode = Sked.FindByID((string)Session["NodeID"]);
                currNode.ContextMenuID = "GroupMenu";
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                btnEmail.Enabled = false;
                break;
        }
    }



    /// <summary>
    /// Fires after confim to delete a shipment group in the s standing orders
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOkayDelete_Click(object sender, EventArgs e)
    {
        FlyTreeNode currNode = Sked.FindByID((string)Session["NodeID"]);
        currNode.Remove();
        Session["NodeID"] = null;
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
        btnEmail.Enabled = false;
    }


    /// <summary>
    /// Fires when OK is clicked when creating a group
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOkayCreate_Click(object sender, EventArgs e)
    {
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
        btnEmail.Enabled = false;
        FlyTreeNode newNode = null;
        newNode = new FlyTreeNode();
        if (Session["NodeID"] != null)
        {
            newNode = Sked.FindByID((string)Session["NodeID"]);
        }

        if (Session["NodeID"] == null)
        {
            newNode.ImageUrl = "$vista_folder";
            newNode.Expanded = true;
            newNode.DragDropAcceptNames = "Product";
            Sked.Nodes[0].ChildNodes.Add(newNode);
        }
        string Prompt = "<strong>Every " + txtFreq.Text + " month(s). Next due: " + txtNextDue.Text + "</strong>";
        //added condition by jaswinder as some time newnode is null when session nodeid is not null
        if (newNode == null)
        {
            newNode = new FlyTreeNode();
            newNode.ImageUrl = "$vista_folder";
            newNode.Expanded = true;
            newNode.DragDropAcceptNames = "Product";
            Sked.Nodes[0].ChildNodes.Add(newNode);
        }
        newNode.Text = Prompt;


        newNode.ContextMenuID = "GroupMenu";
        txtNextDue.Text = "";
        txtFreq.Text = "";
        Session["NodeID"] = null;
    }

    /// <summary>
    /// Fired when contect menu to create a group or the create button is pressed
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void AllGroups_Command(object sender, EventArgs e)
    {
        GroupPopUpTitle.InnerText = "Create Group";
        modCreateGroup.Show();
    }


    /// <summary>
    /// Fires to cancel all changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancelChanges_Click(object sender, EventArgs e)
    {
        Sked.Nodes.Clear();
        FlyTreeNode SkedRoot = new FlyTreeNode("Currnently defined groups");
        SkedRoot.ImageUrl = "$vista_folder";
        SkedRoot.DragDropAcceptNames = "";
        SkedRoot.Expanded = true;
        SkedRoot.ContextMenuID = "AllGroups";
        Sked.Nodes.Add(SkedRoot);
        Session["OriginalStanding"] = util.PopulateTree(Sked, PatientID);
        Session["ChangedGroups"] = util.PopulateFuture(Future, PatientID);
        Session["SkedGroups"] = util.CopyList((List<ScheduleGroup>)Session["ChangedGroups"]);
        lblNotSaved.Visible = false;
        lblNotSaved1.Visible = false;
        btnEmail.Enabled = true;
    }


    /// <summary>
    /// Fires to save changes
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSaveChanges_Click(object sender, EventArgs e)
    {
        List<ScheduleGroup> tester = (List<ScheduleGroup>)Session["ChangedGroups"];
        util.AssembleContactRecs(Sked, Future, (List<ScheduleGroup>)Session["ChangedGroups"], (List<ScheduleGroup>)Session["SkedGroups"], (DataTable)Session["OriginalStanding"], pat.PatientID, (int)Session["StaffID"]);
        util.SaveItems(Sked, pat.PatientID);
        util.SaveFuture(pat.PatientID, (List<ScheduleGroup>)Session["ChangedGroups"], (List<ScheduleGroup>)Session["SkedGroups"]);
        util.PopulateTree(Sked, pat.PatientID);
        util.PopulateFuture(Future, PatientID);
        Session["ChangedGroups"] = util.PopulateFuture(Future, pat.PatientID);
        Session["SkedGroups"] = util.CopyList((List<ScheduleGroup>)Session["ChangedGroups"]);
        lblNotSaved1.Visible = false;
        lblNotSaved.Visible = false;
        btnEmail.Enabled = true;
    }

    protected string PopMessage = "";
    protected void btnEmail_Click(object sender, EventArgs e)
    {
        try
        {
            if (util.SendEmail(pat.PatientID))
            {
                PopMessage = "Email sent.";
                modSentMail.Show();
            }
            else
            {
                modGetAdress.Show();
            }
        }
        catch (System.Exception ex)
        {
            PopMessage = "Email not sent.  Error was" + ex.Message;
            modSentMail.Show();

        }
    }

    protected void btnOkEmail_Click(object sender, EventArgs e)
    {
        util.SaveEmail(txtEmailAddress.Text, pat.PatientID);
        try
        {
            if (util.SendEmail(pat.PatientID))
            {
                PopMessage = "Email sent.";
                modSentMail.Show();
            }
            else
            {
                modGetAdress.Show();
            }
        }
        catch (System.Exception ex)
        {
            PopMessage = "Email not sent.  Error was" + ex.Message;
            modSentMail.Show();

        }
    }

    /// <summary>
    /// Fires to change a shipping address
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Shipments_Command(object sender, FlyContextMenuCommandEventArgs e)
    {
        Session["NodeID"] = e.CommandArgument;
        FlyTreeNode thisNode = Future.FindByID(e.CommandArgument);
        Session["Node"] = thisNode;
        Session["GroupID"] = int.Parse(thisNode.Value);

        List<ScheduleGroup> theGroups = (List<ScheduleGroup>)Session["ChangedGroups"];

        ScheduleGroup theGroup = util.LocateGroup(theGroups, int.Parse(thisNode.Value));
        Session["Group"] = theGroup;
        switch (e.CommandName)
        {
            case "Address":
                txtCity.Text = theGroup.City;
                txtState.Text = theGroup.State;
                txtStreetAddress.Text = theGroup.StreetAddress;
                if (theGroup.IsException)
                {
                    cboRestore.Enabled = true;
                    cboRestore.ForeColor = System.Drawing.Color.Black;
                }
                else
                {
                    cboRestore.Enabled = false;
                    cboRestore.ForeColor = System.Drawing.Color.Gray;
                }
                txtZip.Text = theGroup.Zip;

                modAddress.Show();
                break;
            case "Ship":
                //This will add a NextShipDate exception for each item in the group
                string[] promptArray = util.RemoveHTML(thisNode.Text).Split(' ');
                txtNewShip.Text = promptArray[2];
                lblOldShip.Text = promptArray[2];

                valShipDate.ValueToCompare = util.GetPreviousShipDate(Future, thisNode).ToShortDateString();
                valShipAfter.ValueToCompare = util.GetNextShipDate(Future, thisNode).ToShortDateString();
                modShipDate.Show();
                break;
            case "RevertGroup":
                foreach (FlyTreeNode remNode in thisNode.ChildNodes)
                {
                    string sProfileItemID = util.Revert(remNode.Value, (List<ScheduleGroup>)Session["SkedGroups"], (List<ScheduleGroup>)Session["ChangedGroups"]);
                    Item itm = util.GetItem((List<ScheduleGroup>)Session["ChangedGroups"], int.Parse(remNode.Value));
                    foreach (FlyTreeNode node in remNode.Parent.ChildNodes)
                    {
                        if (node.Text.Contains(itm.ProductName))
                        {
                            if (node.Text.StartsWith("<font"))
                            {
                                node.Text = node.Text.Replace("'blue'", "'purple'");
                                node.Text = node.Text.Replace("'purple'> Discount", "'blue'> Discount");
                            }
                            else
                            {
                                node.Text = "<font color='purple'>" + node.Text.Replace("<font color='green'>", "</font><font color='green'>");
                            }

                        }
                    }
                    FlyTreeNode ParentNode = remNode.Parent;
                    bool hasException = false;
                    foreach (FlyTreeNode node in ParentNode.ChildNodes)
                    {
                        if (node.Text.StartsWith("<font color='blue'>"))
                        {
                            hasException = true;
                        }
                    }
                    if (!hasException)
                    {
                        ParentNode.Text = "<strong>" + util.RemoveHTML(ParentNode.Text) + "</strong>";
                    }
                }
                lblNotSaved.Visible = true;
                lblNotSaved1.Visible = true;
                break;
        }


    }



    protected void btnOkShip_Click(object sender, EventArgs e)
    {
        util.ChangeShipDate((ScheduleGroup)Session["Group"], DateTime.Parse(txtNewShip.Text));
        FlyTreeNode thisNode = Future.FindByID((string)Session["NodeID"]);
        thisNode.Text = "<strong><font color='purple'>Ship Date " + txtNewShip.Text + "</font></strong>";
        Session["Group"] = null;
        Session["Node"] = null;
        foreach (FlyTreeNode node in thisNode.ChildNodes)
        {
            string[] textArray = node.Text.Split(' ');
            textArray[0] = "<font color='purple'>" + util.RemoveHTML(textArray[0]) + "</font>";
            node.Text = "";
            for (int x = 0; x < textArray.Length; x++)
            {
                node.Text += textArray[x] + " ";
            }
            node.Text = node.Text.Trim();
        }
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
    }
    /// <summary>
    /// Fires when OK button pressed in shipping address dialog
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnOkayAddress_Click(object sender, EventArgs e)
    {
        int GroupID = (int)Session["GroupID"];
        ScheduleGroup changeGroup = util.LocateGroup((List<ScheduleGroup>)Session["ChangedGroups"], GroupID);
        changeGroup.City = txtCity.Text;
        changeGroup.State = txtState.Text;
        changeGroup.StreetAddress = txtStreetAddress.Text;
        changeGroup.Zip = txtZip.Text;
        changeGroup.Modified = true;
        FlyTreeNode thisNode = Future.FindByID((string)Session["NodeID"]);
        thisNode.ToolTip = "Shipping Address:" + "\r\n" + changeGroup.StreetAddress + "\r\n" + changeGroup.City + ", " + changeGroup.State + " " + changeGroup.Zip;
        Session["GroupID"] = null;
        Session["NodeID"] = null;
        lblNotSaved.Visible = true;
        lblNotSaved1.Visible = true;
        btnEmail.Enabled = false;
    }
    protected void btnQuit_Click(object sender, EventArgs e)
    {
        IAutoshipCancelReasonService objIAutoshipCancelReasonService = null;
        List<AutoshipCancelReasonViewModel> lstViewModel = null;
        try
        {
            if (Session["QuitAutoship"] == "false")
            {
                objIAutoshipCancelReasonService = new AutoshipCancelReasonService();
                lstViewModel = new List<AutoshipCancelReasonViewModel>();

                lstViewModel = objIAutoshipCancelReasonService.GetAutoshipCancelReasonList();

                // add new item in the list.
                AutoshipCancelReasonViewModel otherReason = new AutoshipCancelReasonViewModel();
                otherReason.ReasonName = "Other";
                otherReason.ReasonID = 0;
                lstViewModel.Add(otherReason);

                rdoReason.DataSource = lstViewModel;
                rdoReason.DataTextField = "ReasonName";
                rdoReason.DataValueField = "ReasonID";
                rdoReason.DataBind();
                modReason.Show();
            }
            else
            {
                // Page.ClientScript.RegisterStartupScript(this.GetType(), "PopupScript", "<script type=text/javascript> alert('Photo uploaded successfully.') </script>");
                ModalAlert.Show();
            }
        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            objIAutoshipCancelReasonService = null;
            lstViewModel = null;
        }
    }

    protected void btnOkQuit_Click(object sender, EventArgs e)
    {
        IAppointmentConsole objIAppointmentConsole = null;
        IAutoshipCancelReasonService objIAutoshipCancelReasonService = null;
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            objIAutoshipCancelReasonService = new AutoshipCancelReasonService();

            // To delete the records from multiple table(Exceptions,ProfileItems, ProfileExceptions)
            objIAutoshipCancelReasonService.DeleteExcepProfileItemsByPatient(PatientID);

            // To get the patient list by patientid
            PatientViewModel viewModelPatient = objIAppointmentConsole.GetPatientList(PatientID);

            if (!string.IsNullOrWhiteSpace(rdoReason.SelectedValue))
                viewModelPatient.AutoshipCancelReasonID = int.Parse(rdoReason.SelectedValue);
            else
                viewModelPatient.AutoshipCancelOther = string.Empty;

            if (rdoReason.SelectedValue == "0")
            {
                viewModelPatient.AutoshipCancelOther = txtOther.Text;
            }

            string msgBody = string.Empty;
            if (!string.IsNullOrWhiteSpace(rdoReason.SelectedValue))
                msgBody = "Quit button pressed.  Reson: " + rdoReason.SelectedItem.Text;
            else
                msgBody = "Quit button pressed.  Reson: ";

            if (rdoReason.SelectedValue == "0") msgBody += " " + txtOther.Text;

            // Add the records in Contact_Tbl table
            objIAutoshipCancelReasonService = new AutoshipCancelReasonService();
            //
            //Code to change patient reasonid by updation patient table
            PatientService objPatientService = new PatientService();
            objPatientService.UpdatePatientAfterDeleingAutoship(PatientID, viewModelPatient.AutoshipCancelReasonID);
            //End of Code to change patient reasonid by updation patient table


            objIAutoshipCancelReasonService.AddContactTbl(59, PatientID, msgBody, (int)Session["StaffID"]);

        }
        catch (System.Exception)
        {
            throw;
        }
        finally
        {
            objIAutoshipCancelReasonService = null;
            objIAppointmentConsole = null;
        }

        Response.Redirect("../patientinfo.aspx?patientid=" + PatientID.ToString());
    }

    protected void btnOneTime_Click(object sender, EventArgs e)
    {
        Response.Redirect("OneTime.aspx?PatientID=" + PatientID.ToString());
    }


    protected void chkCallBeforeShip_CheckedChanged(object sender, EventArgs e)
    {
        QBCustMatchPatientService objIQBCustMatchPatientService = new QBCustMatchPatientService();
        PatientViewModel pat = objIQBCustMatchPatientService.GetPatientDetailById(int.Parse(Request.QueryString["PatientID"]));

        try
        {
            pat.CallBeforeShip = chkCallBeforeShip.Checked;
            PatientService  objIPatientService = new PatientService();
            objIPatientService.UpdatePatientDetails(pat);
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                string msg;
                if (pat.CallBeforeShip == true)
                {
                    msg = "Call before Ship is set true ";
                }
                else
                {
                    msg = "Call before Ship is set false ";

                }
               msg += "Added/Changed by: " + (string)Session["MM_Username"];

                SqlCommand logItem = new SqlCommand("ssp_contact_tbl_AS_Insert", conn);
                logItem.CommandType = CommandType.StoredProcedure;
                logItem.Parameters.AddWithValue("@AptType", 58);
                logItem.Parameters.AddWithValue("@PatientID", pat.PatientID);
                logItem.Parameters.AddWithValue("@MessageBody", msg);
                logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
                logItem.ExecuteNonQuery();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIQBCustMatchPatientService = null;
            
        }
    }
}


