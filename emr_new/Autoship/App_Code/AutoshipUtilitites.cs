using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using NineRays.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;
using System.IO;
using Emrdev.DataLayer.GeneralClasses;

/// <summary>
/// Summary description for Utilitites
/// </summary>
public class AutoshipUtilities
{
    //private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    public string StaffName { get; set; }
    /// <summary>
    /// Tracks the ID of the user
    /// </summary>
    /// 
    int _StaffID = 0;
    public int StaffID
    {
        get
        {
            return _StaffID;
        }
        set
        {
            _StaffID = value;
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("select * from staff where EmployeeID=" + _StaffID.ToString(), conn);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    StaffName = reader["EmployeeName"].ToString();
                }
            }
        }
    }

    /// <summary>
    /// Pupulates the standing orders tree
    /// </summary>
    /// <param name="theTree">Tree object to populate</param>
    /// <param name="PatientID">ID of the patient</param>
    /// <returns></returns>
    public DataTable PopulateTree(FlyTreeView theTree, int PatientID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("ProfileItems_GetTree", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientID", PatientID);

            DataTable dt = OpenTable(cmd);

            conn.Close();
            if (dt.Rows.Count == 0) return dt;

            int currFreq = int.Parse((string)dt.Rows[0]["Frequency"]);
            DateTime currNextDate = (DateTime)dt.Rows[0]["NextShipDate"];
            theTree.Nodes[0].ChildNodes.Clear();
            FlyTreeNode groupNode = AddGroupNode(theTree, currFreq, currNextDate);
            bool hasItems = dt.Rows.Count > 0;
            foreach (DataRow dr in dt.Rows)
            {


                if (int.Parse((string)dr["Frequency"]) != currFreq || (DateTime)dr["NextShipDate"] != currNextDate)
                {
                    groupNode = AddGroupNode(theTree, int.Parse((string)dr["Frequency"]), (DateTime)dr["NextShipDate"]);
                    currFreq = int.Parse((string)dr["Frequency"]);
                    currNextDate = (DateTime)dr["NextShipDate"];
                }
                string Prompt = "";
                if ((string)dr["Pending"] == "No")
                {
                    Prompt = (string)dr["ProductName"] + "<font color='green'> Quantity: " + ((int)dr["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
                    if (dr["DiscountName"] != DBNull.Value) Prompt += (string)dr["DiscountName"] + " </font>"; else Prompt += "None </font>";
                }
                else
                {
                    Prompt = (string)dr["ProductName"] + " Quantity: " + ((int)dr["Quantity"]).ToString() + " Discount: ";
                    if (dr["DiscountName"] != DBNull.Value) Prompt += (string)dr["DiscountName"]; else Prompt += "None";
                    Prompt = "<font color='red'>" + Prompt + "</font>";
                }
                //Added by jaswinder for holding the order
                if (dr["Hold"] == DBNull.Value)
                {
                    groupNode.ContextMenuID = "GroupMenu";
                }
                else if ((int)dr["Hold"] > 0)
                {
                    groupNode.ContextMenuID = "GroupMenuNew";
                    groupNode.Text = "<font color='red'>" + groupNode.Text + "</font>";
                    groupNode.DragDropAcceptNames = "";
                }
                if (dr["Affiliate"] != null) Prompt = Prompt = "<font color='blue'>" + Prompt + "</font>";

                //added by jaswinder to show the group in different color and menu item in case the group has exception

                if (dr["Exception"].Equals("Yes") && dr["Pending"].Equals("No"))
                {

                    groupNode.ContextMenuID = "NoExceptionEdit";
                    groupNode.Text = "<font color='magenta'>" + groupNode.Text + "</font>";
                    groupNode.DragDropAcceptNames = "";
                    Prompt = (string)dr["ProductName"] + " Quantity: " + ((int)dr["Quantity"]).ToString() + " Discount: ";
                    if (dr["DiscountName"] != DBNull.Value)
                        Prompt += (string)dr["DiscountName"];
                    else Prompt += "None";
                    Prompt = "<font color='magenta'>" + Prompt + "</font>";

                }


                //End For Exception check

                FlyTreeNode newNode = new FlyTreeNode(Prompt);

                if ((dr["Hold"] == DBNull.Value || (int)dr["Hold"] == 0) && dr["Exception"].Equals("No"))
                {
                    newNode.ContextMenuID = "ItemMenu";

                }
                else
                {
                    newNode.ContextMenuID = "";
                }


                newNode.Value = ((int)dr["ProfileItemID"]).ToString();
                newNode.DragDropAcceptNames = "";
                groupNode.ChildNodes.Add(newNode);
            }
            conn.Open();
            CheckPending(theTree, PatientID, conn);

            conn.Close();

            Emrdev.DataLayer.Patient _objPatient;
            AdminSuppListDAL objDAL = new AdminSuppListDAL();
            _objPatient = objDAL.Get<Emrdev.DataLayer.Patient>(o => o.PatientID == PatientID);

            if (hasItems)
            {
                _objPatient.Autoship = true;
                objDAL.Edit(_objPatient);
            }
            else
            {
                _objPatient.Autoship = false;
                objDAL.Edit(_objPatient);
            }
            return dt;
        }
    }


    /// <summary>
    /// Populate the future shipments tree
    /// </summary>
    /// <param name="theTree">Tree object to populate</param>
    /// <param name="Patient">PatientID</param>
    /// <returns></returns>
    public List<ScheduleGroup> PopulateFuture(FlyTreeView theTree, int PatientID)
    {

        //get basic tree
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("ProfileItems_GetFuture", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientID", PatientID);

            DataTable dt = OpenTable(cmd);

            conn.Close();
            if (dt.Rows.Count == 0)
            {
                theTree.Nodes[0].ChildNodes.Clear();
                return new List<ScheduleGroup>();
            }

            //build schedule objects
            int CurrFreq = 0;
            DateTime ShipDate = DateTime.MinValue;
            List<Group> Groups = new List<Group>();
            Group thisGroup = new Group();
            thisGroup.StartDate = (DateTime)dt.Rows[0]["NextShipDate"];
            thisGroup.freq = int.Parse((string)dt.Rows[0]["Frequency"]);
            thisGroup.DaystoShip = Convert.ToInt16(dt.Rows[0]["DayToShip"]);
            Groups.Add(thisGroup);
            CurrFreq = Groups[0].freq;
            ShipDate = Groups[0].StartDate;

            foreach (DataRow dr in dt.Rows)
            {
                if (thisGroup.freq != int.Parse((string)dr["Frequency"]) || thisGroup.StartDate != (DateTime)dr["NextShipDate"])
                {
                    thisGroup = new Group();
                    thisGroup.StartDate = (DateTime)dr["NextShipDate"];
                    thisGroup.freq = int.Parse((string)dr["Frequency"]);
                    thisGroup.DaystoShip = Convert.ToInt16(dr["DayToShip"]);
                    Groups.Add(thisGroup);
                }
                string Prompt = (string)dr["ProductName"] + "<font color='green'> Quantity: " + ((int)dr["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
                if (dr["DiscountName"] != DBNull.Value) Prompt += (string)dr["DiscountName"] + " </font>"; else Prompt += "None </font>";
                Item itm = new Item(Prompt);
                itm.ProfileItemID = (int)dr["ProfileItemID"];
                itm.Quantity = (int)dr["Quantity"];
                itm.DiscountID = (int)dr["DiscountID"];
                itm.DiscountName = (string)dr["DiscountName"];
                itm.ProductName = (string)dr["ProductName"];
                itm.StartDate = (DateTime)dr["StartDate"];
                if ((string)dr["Exception"] == "No" && (string)dr["OneTime"] == "No")
                {
                    itm.IsExceptiom = false;
                }
                else
                {
                    itm.IsExceptiom = true;
                }
                if ((string)dr["OneTime"] == "No")
                    itm.OneTime = false;
                else
                    itm.OneTime = true;
                if (dr["EndDate"] != DBNull.Value) itm.EndDate = (DateTime)dr["EndDate"]; else itm.EndDate = DateTime.MaxValue;
                thisGroup.Items.Add(itm);
            }
            //build all shipment objects
            int GroupID = 0;
            int ItemID = 0;
            List<ScheduleGroup> SkedGroups = new List<ScheduleGroup>();
            //List<ScheduleGroup> ChangedGroups = new List<ScheduleGroup>();
            foreach (Group buildGroup in Groups)
            {
                DateTime currNodeDate = buildGroup.StartDate;
                while (currNodeDate < DateTime.Now.AddYears(1))
                {
                    ScheduleGroup newGroup = DateExists(SkedGroups, currNodeDate);
                    if (newGroup == null)
                    {
                        newGroup = new ScheduleGroup();
                        newGroup.Text = "<strong>Ship Date: " + currNodeDate.ToShortDateString() + "</strong>";
                        newGroup.ShipDate = currNodeDate;
                        newGroup.ID = GroupID++;
                        newGroup.freq = buildGroup.freq;
                        CheckAddress(newGroup, new Calendar.Patient(PatientID));
                        SkedGroups.Add(newGroup);
                    }
                    foreach (Item itm in buildGroup.Items)
                    {
                        if (itm.StartDate <= newGroup.ShipDate.AddDays(1) && itm.EndDate > newGroup.ShipDate)
                        {
                            Item newItem = new Item(itm.Prompt);

                            newItem = new Item(itm.Prompt);
                            newItem.ID = ItemID++;
                            newItem.GroupID = newGroup.ID;
                            newItem.ProfileItemID = itm.ProfileItemID;
                            newItem.Quantity = itm.Quantity;
                            newItem.DiscountName = itm.DiscountName;
                            newItem.DiscountID = itm.DiscountID;
                            newItem.ProductName = itm.ProductName;
                            newItem.StartDate = itm.StartDate;
                            newItem.EndDate = itm.EndDate;
                            newItem.OneTime = itm.OneTime;
                            //Jaswinder: Added a patientid parametere in CheckException method
                            CheckException(newItem, newGroup, PatientID);
                            newGroup.Items.Add(newItem);
                        }
                        //else if (itm.StartDate <= newGroup.ShipDate.AddDays(1) && itm.OneTime && itm.EndDate < newGroup.ShipDate)
                        //{
                        //    Item newItem = new Item(itm.Prompt);

                        //    newItem = new Item(itm.Prompt);
                        //    newItem.ID = ItemID++;
                        //    newItem.GroupID = newGroup.ID;
                        //    newItem.ProfileItemID = itm.ProfileItemID;
                        //    newItem.Quantity = itm.Quantity;
                        //    newItem.DiscountName = itm.DiscountName;
                        //    newItem.DiscountID = itm.DiscountID;
                        //    newItem.ProductName = itm.ProductName;
                        //    newItem.StartDate = itm.StartDate;
                        //    newItem.EndDate = itm.EndDate;
                        //    newItem.OneTime = itm.OneTime;
                        //    //Jaswinder: Added a patientid parametere in CheckException method
                        //    CheckException(newItem, newGroup, Patient.ID);
                        //    newGroup.Items.Add(newItem);
                        //}
                    }
                    
                    int days = buildGroup.DaystoShip;
                    DateTime CurreDate = currNodeDate.AddMonths(buildGroup.freq);
                    int month = CurreDate.Month;
                    int year = CurreDate.Year;

                    //if (month != 2)
                    //{
                    //    currNodeDate = currNodeDate.AddMonths(buildGroup.freq);
                    //}
                    //else
                    //{
                       
                        if (days > 28)
                        {

                            switch (month)
                            {
                                case 1:
                                case 3:
                                case 5:
                                case 7:
                                case 8:
                                case 10:
                                case 12:
                                    currNodeDate = new DateTime(year, month, days);

                                    break;
                                case 2:
                                   
                                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                                    {
                                        currNodeDate = new DateTime(year, 2, 29);
                                       
                                    }
                                    else
                                    {
                                        currNodeDate = new DateTime(year, 2, 28);
                                    }
                                    break;
                                case 4:
                                case 6:
                                case 9:
                                case 11:
                                    if (days >= 30)
                                    {
                                        currNodeDate = new DateTime(year, month, 30);
                                    }
                                    else if (days == 29)
                                    {
                                        currNodeDate = new DateTime(year, month, 29);
                                    }
                                 
                                           
                                    break;
                                
                            }

                           
                        }
                        else
                        {
                            currNodeDate = currNodeDate.AddMonths(buildGroup.freq);
                        }
                        
                    //}
                }

            }

            //set up an array of dates to allow correct order
            DateTime[] shipDates = new DateTime[SkedGroups.Count];
            foreach (ScheduleGroup group in SkedGroups)
            {
                shipDates[group.ID] = group.ShipDate;
            }
            Array.Sort(shipDates);
            //now poplulate the tree
            theTree.Nodes[0].ChildNodes.Clear();
            foreach (DateTime dte in shipDates)
            //foreach(ScheduleGroup sked in SkedGroups)
            {
                ScheduleGroup sked = GetGroup(dte, SkedGroups);
                FlyTreeNode newGroup = new FlyTreeNode(sked.Text);
                if (sked.IsException)
                {
                    newGroup.Text = "<font color='blue'>" + sked.Text + "</font>";
                }
                newGroup.ImageUrl = "$vista_folder";
                newGroup.DragDropAcceptNames = "Product";
                newGroup.Value = sked.ID.ToString();
                newGroup.ContextMenuID = "Shipments";
                newGroup.ToolTip = "Shipping Address:" + "\r\n" + sked.StreetAddress + "\r\n" + sked.City + ", " + sked.State + " " + sked.Zip;
                theTree.Nodes[0].ChildNodes.Add(newGroup);
                foreach (Item itm in sked.Items)
                {
                    FlyTreeNode newItem = new FlyTreeNode(itm.Prompt);
                    newItem.DragDropAcceptNames = "";
                    newItem.Value = itm.ID.ToString();
                    newItem.ContextMenuID = "ItemAddressMenu";
                    newItem.CanBeSelected = true;
                    if (itm.IsExceptiom && !newGroup.Text.Contains("'blue'"))
                        newGroup.Text = "<font color='blue'>" + sked.Text + "</font>";

                    newGroup.ChildNodes.Add(newItem);
                }
            }
            conn.Open();
            CheckPendingFuture(theTree, PatientID, conn, SkedGroups);
            //Jaswinder: Added a patientid parametere in CheckShipDates method
            CheckShipDates(theTree, SkedGroups, conn, PatientID);
            CheckHoldFuture(theTree, PatientID, conn, SkedGroups);
            conn.Close();

            return SkedGroups;

        }
    }

    private void CheckShipDates(FlyTreeView theTree, List<ScheduleGroup> changeList, SqlConnection conn, int PatientID)
    {
        foreach (FlyTreeNode theNode in theTree.Nodes[0].ChildNodes)
        {
            foreach (FlyTreeNode node in theNode.ChildNodes)
            {
                Item itm = GetItem(changeList, int.Parse(node.Value));
                ScheduleGroup group = GetGroup(itm.GroupID, changeList);
                //if (itm.IsExceptiom)
                //{
                SqlCommand cmd = new SqlCommand("Exceptions_GetByID", conn);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProfileItemID", itm.ProfileItemID);
                //Jaswinder: apply Foreach loop  and check the original shipdate   
                DataTable dt = OpenTable(cmd);
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["OriginalShipDate"] != System.DBNull.Value)
                    {


                        if (PatientID == (int)dr["PatientIDException"] && group.ShipDate == (DateTime)dr["NextShipDate"])
                        {

                            if (theNode.Text.Contains("<font color='red'>"))
                            {
                                theNode.Text = "<strong><font color='Red'>Ship Date: " + ((DateTime)dr["OriginalShipDate"]).ToShortDateString() + "</font/></strong>";
                            }
                            else
                            {
                                theNode.Text = "<strong><font color='blue'>Ship Date: " + ((DateTime)dr["OriginalShipDate"]).ToShortDateString() + "</font/></strong>";
                            }
                            group.ShipDate = (DateTime)dr["NextShipDate"];
                            group.OrignalShipDate = (DateTime)dr["OriginalShipDate"];
                        }
                    }

                }

                //}
            }
        }
    }

    /// <summary>
    /// CHeck to see if an item already exists in a shipment
    /// </summary>
    /// <param name="baseItem">The item to check for</param>
    /// <param name="group">The group object to look in</param>
    /// <returns>Item object if found, null if not found.</returns>
    private Item ItemExists(Item baseItem, ScheduleGroup group)
    {
        foreach (Item itm in group.Items)
        {
            if (itm.ProductName == baseItem.ProductName)
            {

                //itm.Quantity += baseItem.Quantity;
                return itm;
            }
        }
        return null;
    }

    /// <summary>
    /// Check to see if the item is an exception.  Set the property if it is.
    /// </summary>
    /// <param name="newItem">The item to check</param>
    /// <param name="thisGroup">The group the item is in</param>
    private void CheckException(Item newItem, ScheduleGroup thisGroup, int PatientID)
    {
        if (newItem.EndDate != DateTime.MaxValue)
        {
            newItem.Prompt = newItem.Prompt.Replace(newItem.ProductName, "<font color='blue'>" + newItem.ProductName + "</font>");
            newItem.IsExceptiom = true;
            //return;
        }
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Exceptions_GetByID", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ProfileItemID", newItem.ProfileItemID);

            DataTable dt = OpenTable(cmd);

            conn.Close();
            //Jaswinder: Check for patient and for each loop to check the dates to show the Exceptions in blue color
            DataRow[] theOne = dt.Select("PatientIDException=" + PatientID, "ExceptionID desc");
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    if ((DateTime)dr["DateStart"] < thisGroup.ShipDate && (DateTime)dr["DateEnd"] > thisGroup.ShipDate)
                    {
                        newItem.Quantity = (int)dr["Quantity"];
                        newItem.DiscountID = (int)dr["DiscountID"];
                        newItem.DiscountName = (string)dr["DiscountName"];
                        newItem.IsExceptiom = true;
                        string Prompt = "<font color='blue'>" + (string)dr["ProductName"] + "</font><font color='green'> Quantity: " + ((int)dr["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
                        Prompt += newItem.DiscountName + " </font>";
                        newItem.Prompt = Prompt;
                        thisGroup.Text = "<font color='blue'>" + thisGroup.Text + "</font>";

                    }
                    else if (thisGroup.ShipDate == (DateTime)dr["NextShipDate"])
                    {
                        newItem.Quantity = (int)dr["Quantity"];
                        newItem.DiscountID = (int)dr["DiscountID"];
                        newItem.DiscountName = (string)dr["DiscountName"];
                        newItem.IsExceptiom = true;
                        string Prompt = "<font color='blue'>" + (string)dr["ProductName"] + "</font><font color='green'> Quantity: " + ((int)dr["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
                        Prompt += newItem.DiscountName + " </font>";
                        newItem.Prompt = Prompt;
                        thisGroup.Text = "<font color='blue'>" + thisGroup.Text + "</font>";
                    }
                }
            }
        }
    }

    /// <summary>
    /// Get a group based in the Ship Date
    /// </summary>
    /// <param name="dte">The ship date</param>
    /// <param name="theList">The list to chec</param>
    /// <returns>Group object if found, null if not</returns>
    private ScheduleGroup GetGroup(DateTime dte, List<ScheduleGroup> theList)
    {
        foreach (ScheduleGroup group in theList)
        {
            if (group.ShipDate == dte)
            {
                return group;
            }
        }
        return null;
    }

    /// <summary>
    /// Get a group based on the ID
    /// </summary>
    /// <param name="GroupID">The Group ID</param>
    /// <param name="theList">The List to check</param>
    /// <returns>ShceduleGroup object if found, null if not.</returns>
    private ScheduleGroup GetGroup(int GroupID, List<ScheduleGroup> theList)
    {
        foreach (ScheduleGroup group in theList)
        {
            if (group.ID == GroupID)
            {
                return group;
            }
        }
        return null;
    }

    /// <summary>
    /// See if a schedule group exisits based on the ship date
    /// </summary>
    /// <param name="groups"></param>
    /// <param name="shipDate"></param>
    /// <returns></returns>
    private ScheduleGroup DateExists(List<ScheduleGroup> groups, DateTime shipDate)
    {
        foreach (ScheduleGroup group in groups)
        {
            if (group.ShipDate.ToShortDateString() == shipDate.ToShortDateString())
            {
                return group;
            }
        }

        return null;
    }

    /// <summary>
    /// Chekc for pending orders
    /// </summary>
    /// <param name="theTree">The tree to check</param>
    /// <param name="PatientID">The Patient ID</param>
    /// <param name="conn">An SqlConnection object</param>
    private void CheckPending(FlyTreeView theTree, int PatientID, SqlConnection conn)
    {
        SqlCommand cmd = new SqlCommand("OrderItems_CheckOpen", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PatientID", PatientID);

        DataTable dt = OpenTable(cmd);

        foreach (DataRow dr in dt.Rows)
        {
            FlyTreeNode testNode = theTree.FindByValue(((int)dr["ProfileItemID"]).ToString());
            if (testNode != null)
            {
                testNode.ContextMenuID = "NoEdit";
                testNode.Text = "<font color='red'>" + testNode.Text + "</font>";
                testNode.Parent.Text = "<font color='red'>" + testNode.Parent.Text + "</font>";
                testNode.Parent.ContextMenuID = "NoEdit";
                testNode.Parent.DragDropAcceptNames = "";
            }
        }
    }

    /// <summary>
    /// Chekc pending on the future shipments side.
    /// </summary>
    /// <param name="theTree">The tree to check</param>
    /// <param name="PatientID">The Patient ID</param>
    /// <param name="conn">an SqlConnection object</param>
    /// <param name="theList">The List to check</param>
    private void CheckPendingFuture(FlyTreeView theTree, int PatientID, SqlConnection conn, List<ScheduleGroup> theList)
    {
        SqlCommand cmd = new SqlCommand("OrderItems_CheckOpen", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PatientID", PatientID);

        DataTable dt = OpenTable(cmd);


        foreach (DataRow dr in dt.Rows)
        {
            Item itm = FindProfileItem((int)dr["ProfileItemID"], theList);
            FlyTreeNode testNode = new FlyTreeNode();
            if (itm != null)
            {
                FlyTreeNodeCollection coll = theTree.FindAllByValue(itm.ID.ToString());
                testNode = coll.Last();
            }
            else
                testNode = null;
            if (testNode != null)
            {
                testNode.ContextMenuID = "NoEdit";
                testNode.Text = "<font color='red'>" + testNode.Text + "</font>";
                testNode.Parent.Text = "<font color='red'>" + testNode.Parent.Text + "</font>";
                testNode.Parent.ContextMenuID = "NoEdit";
                testNode.Parent.DragDropAcceptNames = "";
            }
        }
        theTree.Nodes[0].Text = RemoveHTML(theTree.Nodes[0].Text);
    }

    //function to check the upcoming shipment hold  ordres
    private void CheckHoldFuture(FlyTreeView theTree, int PatientID, SqlConnection conn, List<ScheduleGroup> theList)
    {
        SqlCommand cmd = new SqlCommand("ProfileItems_GetHoldFuture", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PatientID", PatientID);

        DataTable dt = OpenTable(cmd);

        int hold = 0;
        FlyTreeNode testNode = new FlyTreeNode();
        foreach (DataRow dr in dt.Rows)
        {

            foreach (ScheduleGroup group in theList)
            {
                Item itm = FindProfileItem((int)dr["ProfileItemID"], theList);

                if (itm != null)
                {
                    if (group.ShipDate == (DateTime)dr["NextShipDate"])
                    {
                        foreach (Item itms in group.Items)
                        {
                            FlyTreeNodeCollection coll = theTree.FindAllByValue(itms.ID.ToString());
                            testNode = coll.Last();
                            if (testNode != null)
                            {
                                testNode.Parent.Text = RemoveHTML(testNode.Parent.Text);
                                testNode.ContextMenuID = "NoEdit";
                                testNode.Parent.ContextMenuID = "NoEdit";
                                testNode.Parent.DragDropAcceptNames = "";
                                testNode.Text = "<font color='red'>" + testNode.Text + "</font>";
                                testNode.Parent.Text = "<font color='red'><strong>" + testNode.Parent.Text + "</strong></font>";
                            }
                        }

                    }
                }
                else
                    testNode = null;


            }

            //Item itm = FindProfileItem((int)dr["ProfileItemID"], theList);
            //FlyTreeNode testNode = new FlyTreeNode();
            //if (itm != null)
            //{
            //    FlyTreeNodeCollection coll = theTree.FindAllByValue(itm.ID.ToString());
            //    testNode = coll.Last();
            //}
            //else
            //    testNode = null;
            //if (testNode != null)
            //{
            //    testNode.ContextMenuID = "NoEdit";
            //    testNode.Parent.ContextMenuID = "NoEdit";
            //    testNode.Parent.DragDropAcceptNames = "";
            //}
        }

    }

    /// <summary>
    /// Find an item based on the Profile Item ID
    /// </summary>
    /// <param name="ProfileItemID">ProfileItemID to look for</param>
    /// <param name="theList">The list to check</param>
    /// <returns>Item object if found, null if not</returns>
    private Item FindProfileItem(int ProfileItemID, List<ScheduleGroup> theList)
    {
        foreach (ScheduleGroup group in theList)
        {
            foreach (Item itm in group.Items)
            {
                if (itm.ProfileItemID == ProfileItemID)
                {
                    return itm;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Populate the Products tree (both tabs)
    /// </summary>
    /// <param name="tree">The tree to populate</param>
    public void PopulateProducts(FlyTreeView tree)
    {
        SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        conn.Open();
        SqlCommand cmd = new SqlCommand();
        cmd.CommandText = "Select * from AutoshipProducts p left outer join AutoshipProductGroup g on p.GroupID = g.AutoshipProductGroupID where p.Active=1  order by GroupID desc,ProductName";
        cmd.Connection = conn;
        cmd.CommandType = CommandType.Text;
        SqlDataReader reader = OpenReader(cmd);

        FlyTreeNode node = new NineRays.WebControls.FlyTreeNode("Autoship Products");

        tree.Nodes.Add(node);
        tree.Nodes[0].ImageUrl = "$vista_folder";
        reader.Read();
        int thisId = ((int)reader["GroupID"]) + 1;
        FlyTreeNode thisNode = new FlyTreeNode(); ;//= null; //Tester.Nodes[0];
        while (true)
        {

            if (thisId != (int)reader["GroupID"] && (int)reader["GroupID"] != 0)
            {
                thisNode = new FlyTreeNode((string)reader["GroupName"]);
                thisNode.DragDropName = "";
                thisNode.CanBeSelected = false;
                thisNode.Expanded = false;
                thisNode.ImageUrl = "$vista_folder";
                tree.Nodes[0].ChildNodes.Add(thisNode);
            }
            if ((int)reader["GroupID"] == 0)
            {
                thisNode = tree.Nodes[0];
            }
            FlyTreeNode newNode = new FlyTreeNode((string)reader["ProductName"]);
            newNode.CanBeSelected = true;
            newNode.DragDropAcceptNames = "";
            newNode.DragDropName = "Product";
            thisNode.ChildNodes.Add(newNode);
            thisId = (int)reader["GroupID"];
            try
            {
                reader.Read();
                int xx = (int)reader["GroupID"];
            }
            catch
            {
                break;
            }
        }
        reader.Close();
        conn.Close();


    }

    /// <summary>
    /// Overload for updating the Discount on the standing orders side
    /// </summary>
    /// <param name="NodeID">The ID of the node</param>
    /// <param name="tree">FlyTreeView containing the node</param>
    /// <param name="DiscountID">The ID of the discount</param>
    /// <param name="DiscountName">The name of the discount</param>
    public void UpdateDiscount(string NodeID, FlyTreeView tree, string DiscountID, string DiscountName)
    {
        UpdateDiscount(NodeID, tree, DiscountID, DiscountName, false, new List<ScheduleGroup>());
    }

    /// <summary>
    /// Sam as other overload for the future side
    /// </summary>
    /// <param name="NodeID"></param>
    /// <param name="tree"></param>
    /// <param name="DiscountID"></param>
    /// <param name="DiscountName"></param>
    /// <param name="future">True is for the future side</param>
    /// <param name="theList">THe list for Futrure items</param>
    public void UpdateDiscount(string NodeID, FlyTreeView tree, string DiscountID, string DiscountName, bool future, List<ScheduleGroup> theList)
    {

        FlyTreeNode theNode = tree.FindByID(NodeID);

        string[] Prompt = theNode.Text.Split(' ');
        string NewPrompt = "";
        for (int x = 0; x < Prompt.Length - 1; x++)
        {

            if (Prompt[x].Contains("Discount:"))
            {
                NewPrompt += Prompt[x] + " ";
                break;
            }
            else
            {
                NewPrompt += Prompt[x] + " ";
            }

        }
        NewPrompt += DiscountName;
        NewPrompt = "<font color='purple'>" + NewPrompt.Replace("<font color='blue'>", "</font><font color='blue'>");
        theNode.Text = NewPrompt;
        if (future)
        {
            Item itm = GetItem(theList, int.Parse(theNode.Value));
            itm.DiscountID = int.Parse(DiscountID);
            itm.DiscountName = DiscountName;
            ScheduleGroup group = GetGroup(itm.GroupID, theList);
            group.Modified = true;
        }
    }

    /// <summary>
    /// Add ne new group node to the standing orders tree
    /// </summary>
    /// <param name="theTree">Standing orders tree object</param>
    /// <param name="Freq">The frequency</param>
    /// <param name="NextShip">Next ship date</param>
    /// <returns>THe new node</returns>
    private FlyTreeNode AddGroupNode(FlyTreeView theTree, int Freq, DateTime NextShip)
    {
        FlyTreeNode newNode = new FlyTreeNode("<strong>Every " + Freq.ToString() + " month(s). Next due: " + NextShip.ToShortDateString() + "</strong>");
        newNode.ImageUrl = "$vista_folder";
        newNode.Expanded = true;
        newNode.ContextMenuID = "GroupMenu";
        newNode.Value = Freq.ToString();
        theTree.Nodes[0].ChildNodes.Add(newNode);
        return newNode;
    }

    /// <summary>
    /// Add a new group to the Future tree
    /// </summary>
    /// <param name="theTree">Future tree object</param>
    /// <param name="Freq">The frequency</param>
    /// <param name="NextShip">Next ship date</param>
    /// <returns>THe new node</returns>
    private FlyTreeNode AddShipmentNode(FlyTreeView theTree, int Freq, DateTime NextShip)
    {
        FlyTreeNode newNode = new FlyTreeNode("<strong>Ship Date: " + NextShip.ToShortDateString() + "</strong>");
        newNode.ImageUrl = "$vista_folder";
        newNode.Expanded = false;
        newNode.ContextMenuID = "";
        newNode.Value = Freq.ToString();
        theTree.Nodes[0].ChildNodes.Add(newNode);
        return newNode;
    }

    /// <summary>
    /// Save all changes from both standing tree
    /// </summary>
    /// <param name="sked">The standing orders tree</param>
    /// <param name="PatientID">The Patient ID</param>
    public void SaveItems(FlyTreeView sked, int PatientID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            foreach (FlyTreeNode Group in sked.Nodes[0].ChildNodes)
            {
                string[] PromptArray = Group.Text.Replace("<strong>", "").Replace("</strong>", "").Replace("<font color='red'>", "").Replace("</font>", "").Replace("<font color='magenta'>", "").Replace("</font>", "").Split(' ');
                int freq = int.Parse(PromptArray[1]);
                DateTime NextDate = DateTime.Parse(PromptArray[PromptArray.Length - 1].Replace("</font>", ""));
                foreach (FlyTreeNode Item in Group.ChildNodes)
                {
                    int ItemID = 0;
                    //added by jaswinder for changing the menu item
                    int Hold = 0;
                    if (Group.ContextMenuID == "GroupMenuNew")
                    {
                        Hold = 1;
                    }
                    try
                    {

                        ItemID = int.Parse(Item.Value);
                    }
                    catch { }
                    string sDetail = RemoveHTML(Item.Text);
                    string[] Detail = sDetail.Split(' ');
                    int quantElem = 0;
                    for (int x = 0; x < Detail.Length - 1; x++)
                    {
                        if (Detail[x].Contains("Quantity:"))
                        {
                            quantElem = x + 1;
                            break;
                        }
                    }
                    int Quantity = int.Parse(Detail[quantElem]);
                    string ProductName = "";
                    for (int x = 0; x < Detail.Length - 2; x++)
                    {
                        ProductName += Detail[x] + " ";
                    }
                    ProductName = ProductName.Trim();
                    SqlCommand cmd = new SqlCommand("ProfileItem_GetByID", conn);
                    DateTime StartDate = DateTime.Now;
                    DateTime LastShipped = DateTime.Now;
                    int DiscountID = 0;
                    if (ItemID != 0)
                    {
                        cmd.Parameters.AddWithValue("@ProfileItemID", ItemID);
                        cmd.CommandType = CommandType.StoredProcedure;
                        SqlDataReader reader = OpenReader(cmd);
                        reader.Read();
                        StartDate = (DateTime)reader["StartDate"];
                        LastShipped = (DateTime)reader["LastShipped"];
                        reader.Close();
                    }

                    if (ItemID == 0)
                    {
                        string sPrompt = RemoveHTML(Item.Text);
                        string[] Prompt = sPrompt.Split(' ');
                        ProductName = "";
                        int x = 0;
                        while (!Prompt[x].Contains("Quantity"))
                        {
                            ProductName += Prompt[x++] + " ";
                        }
                        ProductName = ProductName.Trim();

                        string DiscountName = "";
                        while (!Prompt[x].Contains("Discount"))
                        {
                            x++;
                        }
                        x++;
                        while (x < Prompt.Length)
                        {
                            DiscountName += Prompt[x] + " ";
                            x++;
                        }


                        cmd.Parameters.Clear();
                        cmd.CommandText = "AutoShipProducts_GetID";
                        cmd.Parameters.AddWithValue("@ProductName", ProductName);
                        cmd.CommandType = CommandType.StoredProcedure;
                        int ProductID = (int)cmd.ExecuteScalar();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "Discounts_GetID";
                        cmd.Parameters.AddWithValue("@DiscountName", DiscountName);

                        DiscountID = (int)cmd.ExecuteScalar();
                        cmd.CommandText = "ProfilesItems_Insert";
                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@PatientID", PatientID);
                        cmd.Parameters.AddWithValue("@ProductID", ProductID);
                        cmd.Parameters.AddWithValue("@Quantity", Quantity);
                        cmd.Parameters.AddWithValue("@Frequency", freq);
                        cmd.Parameters.AddWithValue("@StartDate", StartDate);
                        cmd.Parameters.AddWithValue("@NextShip", NextDate);
                        cmd.Parameters.AddWithValue("@Hold", Hold);
                        Item.Value = ((decimal)cmd.ExecuteScalar()).ToString();
                    }
                    else
                    {
                        string sPrompt = RemoveHTML(Item.Text);
                        string[] Prompt = sPrompt.Split(' ');
                        string DiscountName = "";
                        int x = 0;
                        while (!Prompt[x].Contains("Discount"))
                        {
                            x++;
                        }
                        x++;
                        while (x < Prompt.Length)
                        {
                            DiscountName += Prompt[x] + " ";
                            x++;
                        }
                        cmd.Parameters.Clear();
                        cmd.CommandText = "Discounts_GetID";
                        cmd.Parameters.AddWithValue("@DiscountName", DiscountName);
                        DiscountID = (int)cmd.ExecuteScalar();

                        cmd.Parameters.Clear();
                        cmd.CommandText = "ProfileItem_GetByID";
                        cmd.Parameters.AddWithValue("@ProfileItemID", Item.Value);
                        SqlDataReader current = OpenReader(cmd);
                        current.Read();
                        if (Quantity != (int)current["Quantity"]
                            || freq != int.Parse((string)current["FrequencyValue"])
                            || StartDate != (DateTime)current["StartDate"]
                            || LastShipped != (DateTime)current["LastShipped"]
                            || NextDate != (DateTime)current["NextShipDate"]
                            || DiscountID != (int)current["DiscountID"]
                            || Hold != ((current["Hold"] == DBNull.Value) ? 0 : (int)current["Hold"]))
                        {
                            current.Close();
                            cmd.CommandText = "ProfileItems_Update";
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@ProfileItemID", ItemID);
                            cmd.Parameters.AddWithValue("@Quantity", Quantity);
                            cmd.Parameters.AddWithValue("@Frequency", freq);
                            cmd.Parameters.AddWithValue("@StartDate", StartDate);
                            cmd.Parameters.AddWithValue("@LastShipped", LastShipped);
                            cmd.Parameters.AddWithValue("@NextShip", NextDate);
                            cmd.Parameters.AddWithValue("@DiscountID", DiscountID);
                            cmd.Parameters.AddWithValue("@Hold", Hold);
                            cmd.ExecuteNonQuery();
                        }
                        else
                            current.Close();
                    }



                }
            }

            CheckDeletes(sked, PatientID, conn);
            conn.Close();
        }
    }

    /// <summary>
    /// Save items from Puture tree
    /// </summary>
    /// <param name="PatientID">Patient ID</param>
    /// <param name="chagngedGroups">THe groups list with changes</param>
    /// <param name="originalGroups">The original groups list before changes</param>
    public void SaveFuture(int PatientID, List<ScheduleGroup> chagngedGroups, List<ScheduleGroup> originalGroups)
    {
        //loop through all groups in chamged list
        foreach (ScheduleGroup group in chagngedGroups)
        {
            //check if it has changed
            if (group.Modified)
            {
                //get the orginal group
                ScheduleGroup startingGroup = GetGroup(group.ID, originalGroups);
                //check group fields and save as necessary

                //check ship date
                if ((group.ShipDate != startingGroup.ShipDate) || (group.ShipDate == startingGroup.ShipDate && group.OrignalShipDate != startingGroup.ShipDate))
                {
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("", conn);
                        cmd.CommandType = CommandType.Text;
                        //exception for NextShip

                        foreach (Item itm in group.Items)
                        {
                            bool hasException = false;
                            //Jaswinder condition  for patient id and nextshipdate for all the Saving items
                            string sql = "select * from exceptions where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                            cmd.CommandText = sql;
                            SqlDataReader reader = OpenReader(cmd);
                            hasException = reader.HasRows;
                            reader.Close();
                            //Condition for onetime item ,if it false only then it will insert /update the data in Exception table
                            if (itm.OneTime == false)
                            {

                                if (!hasException)
                                {
                                    sql = "INSERT INTO Exceptions (PatientID,ProfileItemID,Quantity,Frequency,DateStart,DateEnd,Notes,NextShipDate,OriginalShipDate)VALUES(";
                                    sql += PatientID.ToString() + ",";
                                    sql += itm.ProfileItemID.ToString() + ",";
                                    sql += itm.Quantity.ToString() + ",";
                                    sql += group.freq + ",'";
                                    if (startingGroup.ShipDate < group.ShipDate)
                                    {
                                        sql += startingGroup.ShipDate.AddDays(-1).ToShortDateString() + "','";
                                        sql += group.ShipDate.AddDays(1).ToShortDateString() + "','','";
                                    }
                                    else
                                    {
                                        sql += group.ShipDate.AddDays(-1).ToShortDateString() + "','";
                                        sql += startingGroup.ShipDate.AddDays(1).ToShortDateString() + "','','";
                                    }
                                    sql += startingGroup.ShipDate.ToShortDateString() + "','";
                                    sql += group.ShipDate.ToShortDateString() + "')"; //jaswinder
                                }
                                else
                                {
                                    if (startingGroup.ShipDate < group.ShipDate)
                                    {
                                        sql = "update exceptions set DateStart='" + startingGroup.ShipDate.AddDays(-1).ToShortDateString();
                                        sql += "', DateEnd='" + group.ShipDate.AddDays(1).ToShortDateString() + "', NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "', OriginalShipDate ='" + group.ShipDate.ToShortDateString() + "'";
                                        sql += " where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                    }
                                    else
                                    {
                                        sql = "update exceptions set DateStart='" + group.ShipDate.AddDays(-1).ToShortDateString();
                                        sql += "', DateEnd='" + startingGroup.ShipDate.AddDays(1).ToShortDateString() + "', NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "', OriginalShipDate ='" + group.ShipDate.ToShortDateString() + "'";
                                        sql += " where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                    }
                                }
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            else
                            {

                                if (hasException)
                                {

                                    if (startingGroup.ShipDate < group.ShipDate)
                                    {
                                        sql = "update exceptions set DateStart='" + startingGroup.ShipDate.AddDays(-1).ToShortDateString();
                                        sql += "', DateEnd='" + group.ShipDate.AddDays(1).ToShortDateString() + "', NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "', OriginalShipDate ='" + group.ShipDate.ToShortDateString() + "'";
                                        sql += " where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + " and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                    }
                                    else
                                    {
                                        sql = "update exceptions set DateStart='" + group.ShipDate.AddDays(-1).ToShortDateString();
                                        sql += "', DateEnd='" + startingGroup.ShipDate.AddDays(1).ToShortDateString() + "', NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "', OriginalShipDate ='" + group.ShipDate.ToShortDateString() + "'";
                                        sql += " where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + " and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                    }
                                    cmd.CommandText = sql;
                                    cmd.ExecuteNonQuery();
                                }
                            }

                            conn.Close();
                        }
                    }
                }



                //check the address
                if (group.City != startingGroup.City
                    || group.State != startingGroup.State
                    || group.StreetAddress != startingGroup.StreetAddress
                    || group.Zip != startingGroup.Zip)
                {
                    //add exception for shipping
                    using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                    {
                        conn.Open();
                        SqlCommand cmd = new SqlCommand("ProfileExceptions_Insert", conn);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@PatientID", PatientID);
                        cmd.Parameters.AddWithValue("@ShippingAddress", group.StreetAddress.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@ShippingCity", group.City.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@ShippingState", group.State.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@ShippingZip", group.Zip.Replace("'", "''"));
                        cmd.Parameters.AddWithValue("@StartDate", group.ShipDate.AddDays(-1));
                        cmd.Parameters.AddWithValue("@EndDate", group.ShipDate.AddDays(1));

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }

                //and check each item
                foreach (Item itm in group.Items)
                {

                    Item startingItem = GetItem(originalGroups, itm.ID);
                    if (itm.Reverted)
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("", conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "delete from exceptions where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'"; //Jaswinder
                            cmd.ExecuteNonQuery();
                            string sql = "delete from ProfileItems where ProductID=" + GetProductID(itm.ProductName, conn);
                            sql += " and EndDate > '" + group.ShipDate.ToShortDateString() + "' and StartDate < '" + group.ShipDate.ToShortDateString() + "'";
                            sql += "and PatientID = " + PatientID;
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            conn.Close();
                        }
                    }
                    //add flags for each possibility and add 1 exception
                    //add exception if discount or quantity changed
                    if ((itm.DiscountID != startingItem.DiscountID
                        || itm.Quantity != startingItem.Quantity) && itm.ProfileItemID != 0)
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("", conn);
                            cmd.CommandType = CommandType.Text;
                            string sql = "";

                            bool hasException = false;
                            sql = "select * from exceptions where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                            cmd.CommandText = sql;
                            SqlDataReader reader = OpenReader(cmd);
                            hasException = reader.HasRows;
                            DateTime ChangedShipDate = group.ShipDate;
                            if (!itm.OneTime)
                            {


                                //reader.Close();
                                if (!hasException)
                                {
                                    hasException = reader.HasRows;
                                    sql = "INSERT INTO Exceptions (PatientID,ProfileItemID,Quantity,Frequency,DateStart,DateEnd,Notes,NextShipDate,DiscountID)VALUES(";
                                    sql += PatientID.ToString() + ",";
                                    sql += itm.ProfileItemID.ToString() + ",";
                                    sql += itm.Quantity.ToString() + ",";
                                    sql += group.freq + ",'";
                                    sql += group.ShipDate.AddDays(-1).ToShortDateString() + "','";
                                    sql += group.ShipDate.AddDays(1).ToShortDateString() + "','','";
                                    sql += group.ShipDate.ToShortDateString() + "',";
                                    sql += itm.DiscountID.ToString() + ")";
                                }
                                else
                                {
                                    //Jaswinder :set date for start and end same as in case of shipdate is change
                                    DateTime Start = ChangedShipDate.AddDays(-1);
                                    DateTime End = ChangedShipDate.AddDays(1);
                                    while (reader.Read())
                                    {
                                        if (reader["OriginalShipDate"] != System.DBNull.Value)
                                        {
                                            ChangedShipDate = (DateTime)reader["OriginalShipDate"];
                                            if (group.ShipDate < (DateTime)reader["OriginalShipDate"])
                                            {
                                                Start = group.ShipDate.AddDays(-1);
                                                End = ChangedShipDate.AddDays(1);
                                            }
                                            else
                                            {
                                                Start = ChangedShipDate.AddDays(-1);
                                                End = group.ShipDate.AddDays(1);
                                            }

                                        }


                                    }

                                    sql = "UPDATE Exceptions set Quantity=" + itm.Quantity.ToString() + ", Frequency=" + group.freq + ", DateStart='" + Start.ToShortDateString() + "', DateEnd='" + End.ToShortDateString() + "' where ProfileItemID=" + itm.ProfileItemID + "  and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                }
                                reader.Close();
                            }

                            //else
                            //{
                            //    bool hasException1 = false;
                            //    sql = "select * from exceptions where ProfileItemID=" + itm.ProfileItemID.ToString() + " and PatientID=" + PatientID.ToString() + "and NextShipDate='" + group.ShipDate.ToShortDateString() + "'"; 
                            //    cmd.CommandText = sql;
                            //    SqlDataReader reader1 = OpenReader(cmd);
                            //    hasException1 = reader1.HasRows;
                            //    reader1.Close();
                            //    if (!hasException1)
                            //    {
                            //        sql = "INSERT INTO Exceptions (PatientID,ProfileItemID,Quantity,Frequency,DateStart,DateEnd,Notes,NextShipDate,DiscountID)VALUES(";
                            //        sql += PatientID.ToString() + ",";
                            //        sql += itm.ProfileItemID.ToString() + ",";
                            //        sql += itm.Quantity.ToString() + ",";
                            //        sql += group.freq + ",'";
                            //        sql += group.ShipDate.AddDays(-1).ToShortDateString() + "','";
                            //        sql += group.ShipDate.AddDays(1).ToShortDateString() + "','','";
                            //        sql += group.ShipDate.ToShortDateString() + "',";
                            //        sql += itm.DiscountID.ToString() + ")";
                            //    }

                            //}
                            else
                            {
                                sql = "UPDATE ProfileItems set Quantity=" + itm.Quantity.ToString() + ", Frequency=" + group.freq + " where ProfileItemID=" + itm.ProfileItemID + "  and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";


                                //Jaswinder :set date for start and end same as in case of shipdate is change
                                DateTime StartExp = ChangedShipDate.AddDays(-1);
                                DateTime EndExp = ChangedShipDate.AddDays(1);
                                while (reader.Read())
                                {
                                    if (reader["OriginalShipDate"] != System.DBNull.Value)
                                    {
                                        ChangedShipDate = (DateTime)reader["OriginalShipDate"];
                                        if (group.ShipDate < (DateTime)reader["OriginalShipDate"])
                                        {
                                            StartExp = group.ShipDate.AddDays(-1);
                                            EndExp = ChangedShipDate.AddDays(1);
                                        }
                                        else
                                        {
                                            StartExp = ChangedShipDate.AddDays(-1);
                                            EndExp = group.ShipDate.AddDays(1);
                                        }

                                    }


                                }

                                string sqlExp = "UPDATE Exceptions set Quantity=" + itm.Quantity.ToString() + ", Frequency=" + group.freq + ", DateStart='" + StartExp.ToShortDateString() + "', DateEnd='" + EndExp.ToShortDateString() + "' where ProfileItemID=" + itm.ProfileItemID + "  and PatientID=" + PatientID.ToString() + "and NextShipDate='" + startingGroup.ShipDate.ToShortDateString() + "'";
                                reader.Close();
                                SqlCommand cmdExp = new SqlCommand("", conn);
                                cmdExp.CommandType = CommandType.Text;
                                cmdExp.CommandText = sqlExp;
                                cmdExp.ExecuteNonQuery();

                            }
                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();


                            conn.Close();
                        }
                    }

                    // add exception with 0 quantity if removed
                    if (itm.Removed)
                    {
                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                        {
                            conn.Open();
                            SqlCommand cmd = new SqlCommand("", conn);
                            cmd.CommandType = CommandType.Text;
                            string sql = "delete from exceptions where ProfileItemID = ";
                            sql += itm.ProfileItemID.ToString();


                            cmd.CommandText = sql;
                            cmd.ExecuteNonQuery();
                            if (itm.EndDate != DateTime.MaxValue)
                            {
                                sql = "delete from ProfileItems where ProfileItemID=" + itm.ProfileItemID.ToString() + " and EndDate is not null";
                                cmd.CommandText = sql;
                                cmd.ExecuteNonQuery();
                            }
                            conn.Close();
                        }
                    }

                    //add profile item with start and end date for one time purchase
                    if (itm.OneTime && itm.Modified)
                    {

                        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                        {
                            conn.Open();
                            //DateTime StartDate = group.ShipDate;
                            //DateTime EndDate = group.ShipDate;

                            //if (group.ShipDate != startingGroup.ShipDate)
                            //{
                            //    if (startingGroup.ShipDate < group.ShipDate)
                            //    {
                            //        StartDate = startingGroup.ShipDate.AddDays(-1);
                            //        EndDate = group.ShipDate.AddDays(1);
                            //    }
                            //    else
                            //    {
                            //        StartDate = group.ShipDate.AddDays(-1);
                            //        EndDate = startingGroup.ShipDate.AddDays(1);
                            //    }


                            //}

                            SqlCommand cmd = new SqlCommand("ProfilesItems_Insert", conn);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@PatientID", PatientID);
                            cmd.Parameters.AddWithValue("@ProductId", GetProductID(itm.ProductName, conn));
                            cmd.Parameters.AddWithValue("@Quantity", itm.Quantity);
                            cmd.Parameters.AddWithValue("@Frequency", group.freq);
                            cmd.Parameters.AddWithValue("@StartDate", startingGroup.ShipDate.AddDays(-1));
                            cmd.Parameters.AddWithValue("@EndDate", startingGroup.ShipDate.AddDays(1));
                            cmd.Parameters.AddWithValue("@NextShip", startingGroup.ShipDate);

                            //Added by jaswinder for hold unhold property.
                            cmd.Parameters.AddWithValue("@Hold", 0);
                            itm.ProfileItemID = Convert.ToInt32(cmd.ExecuteScalar().ToString());



                            string ontimeSQL;

                            //if (group.ShipDate != startingGroup.ShipDate)
                            //{
                            ontimeSQL = "INSERT INTO Exceptions (PatientID,ProfileItemID,Quantity,Frequency,DateStart,DateEnd,Notes,NextShipDate,OriginalShipDate)VALUES(";
                            ontimeSQL += PatientID.ToString() + ",";
                            ontimeSQL += itm.ProfileItemID.ToString() + ",";
                            ontimeSQL += itm.Quantity.ToString() + ",";
                            ontimeSQL += group.freq + ",'";
                            if (startingGroup.ShipDate < group.ShipDate)
                            {
                                ontimeSQL += startingGroup.ShipDate.AddDays(-1).ToShortDateString() + "','";
                                ontimeSQL += group.ShipDate.AddDays(1).ToShortDateString() + "','','";
                            }
                            else
                            {
                                ontimeSQL += group.ShipDate.AddDays(-1).ToShortDateString() + "','";
                                ontimeSQL += startingGroup.ShipDate.AddDays(1).ToShortDateString() + "','','";
                            }
                            ontimeSQL += startingGroup.ShipDate.ToShortDateString() + "','";
                            ontimeSQL += group.ShipDate.ToShortDateString() + "')";
                            cmd = new SqlCommand("", conn);
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = ontimeSQL;
                            cmd.ExecuteNonQuery();
                            //}
                            conn.Close();


                        }
                        itm.Modified = false;
                    }

                }

            }

        }

    }

    /// <summary>
    /// Get the ID of a product based on the name
    /// </summary>
    /// <param name="ProductName">The name of the product</param>
    /// <param name="conn">An SqlConneciton object</param>
    /// <returns>The Product ID</returns>
    private int GetProductID(string ProductName, SqlConnection conn)
    {

        //SqlCommand cmd = new SqlCommand("select ProductID from AutoshipProducts where dbo.strip_spaces(ProductName)='" + ProductName.Replace("'", "''").Trim() + "'", conn);
        //cmd.CommandType = CommandType.Text;
        //return (int)cmd.ExecuteScalar();

        SqlCommand cmd = new SqlCommand("ssp_GetProductID", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@ProductName", ProductName.Trim());
        cmd.ExecuteNonQuery();
        SqlDataReader reader = OpenReader(cmd);
        int productid = 0;
        while (reader.Read())
        {
            productid = (int)reader["ProductID"];

        }

        reader.Close();
        return productid;


    }

    /// <summary>
    /// Check for deleted tree nodes in Standing Orders
    /// </summary>
    /// <param name="Sked"></param>
    /// <param name="PatientID"></param>
    /// <param name="conn"></param>
    private void CheckDeletes(FlyTreeView Sked, int PatientID, SqlConnection conn)
    {
        //get all items in db
        SqlCommand cmd = new SqlCommand("ProfileItems_GetTree", conn);
        cmd.CommandType = CommandType.StoredProcedure;
        cmd.Parameters.AddWithValue("@PatientID", PatientID);

        DataTable dt = OpenTable(cmd);


        //loop for each item
        foreach (DataRow dr in dt.Rows)
        {
            //if not in tree, delete
            string ProfID = ((int)dr["ProfileItemID"]).ToString();
            FlyTreeNode checkNode = Sked.FindByValue(ProfID);
            if (checkNode == null)
            {
                cmd.CommandText = "ProfileItem_Delete";
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@ProfileItemID", ProfID);
                cmd.ExecuteNonQuery();
            }
        }
    }

    /// <summary>
    /// Remove all HTML from a string
    /// </summary>
    /// <param name="inputString">String with HTML</param>
    /// <returns>String w/o HTML</returns>
    public string RemoveHTML(string inputString)
    {
        System.Text.RegularExpressions.Regex objRegExp = new Regex("<(.|\n)+?>");
        inputString = objRegExp.Replace(inputString, String.Empty);
        return inputString;
    }

    /// <summary>
    /// Checks excpetions, then EMR Shipping Address, and finally EMR Billing Address to populate group object
    /// </summary>
    /// <param name="newGroup">A ScheduleGroup object</param>
    /// <param name="Patient">A Calendar.Patient object</param>
    private void CheckAddress(ScheduleGroup newGroup, Calendar.Patient Patient)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("ProfileExceptions_GetByPatient", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientID", Patient.ID);

            DataTable dt = OpenTable(cmd);

            DataRow[] checker = dt.Select("StartDate <='" + newGroup.ShipDate.ToShortDateString() + "' and EndDate >= '" + newGroup.ShipDate.ToShortDateString() + "'");
            if (checker.Count() > 0)
            {
                newGroup.StreetAddress = (string)checker[0]["ShippingStreet"];
                newGroup.City = (string)checker[0]["ShippingCity"];
                newGroup.State = (string)checker[0]["ShippingState"];
                newGroup.Zip = (string)checker[0]["ShippingZip"];
                newGroup.IsException = true;
            }
            else
            {
                if (Patient.ShippingCity != "")
                {
                    newGroup.City = Patient.ShippingCity;
                    newGroup.State = Patient.ShippingState;
                    newGroup.StreetAddress = Patient.ShippingStreet;
                    newGroup.Zip = Patient.ShippingZip;
                    newGroup.IsException = false;
                }
                else
                {
                    newGroup.City = Patient.BillingCity;
                    newGroup.State = Patient.BillingState;
                    newGroup.StreetAddress = Patient.BillingStreet;
                    newGroup.Zip = Patient.BillingZip;
                    newGroup.IsException = false;
                }
            }
        }
    }

    /// <summary>
    /// Find a ScheduoleGroup base on the ID
    /// </summary>
    /// <param name="SkedGroups"></param>
    /// <param name="GroupID"></param>
    /// <returns></returns>
    public ScheduleGroup LocateGroup(List<ScheduleGroup> SkedGroups, int GroupID)
    {
        if (SkedGroups != null)
            foreach (ScheduleGroup group in SkedGroups)
            {
                if (group.ID == GroupID)
                    return group;

            }
        return null;
    }

    /// <summary>
    /// Copies a ScheduleGroup list into a new object
    /// </summary>
    /// <param name="originalList">List to copy</param>
    /// <returns>New Lsit Object</returns>
    public List<ScheduleGroup> CopyList(List<ScheduleGroup> originalList)
    {
        List<ScheduleGroup> newlist = new List<ScheduleGroup>();
        foreach (ScheduleGroup group in originalList)
        {
            ScheduleGroup newGroup = new ScheduleGroup();
            newGroup.City = group.City;
            newGroup.freq = group.freq;
            newGroup.ID = group.ID;
            newGroup.IsException = group.IsException;
            newGroup.Modified = group.Modified;
            newGroup.ShipDate = group.ShipDate;
            newGroup.State = group.State;
            newGroup.StreetAddress = group.StreetAddress;
            newGroup.Text = group.Text;
            newGroup.Zip = group.Zip;
            foreach (Item itm in group.Items)
            {
                Item newItem = new Item(itm.Prompt, itm.ID);
                newItem.DiscountID = itm.DiscountID;
                newItem.DiscountName = itm.DiscountName;
                newItem.GroupID = itm.GroupID;
                newItem.OneTime = itm.OneTime;
                newItem.ProductName = itm.ProductName;
                newItem.ProfileItemID = itm.ProfileItemID;
                newItem.Prompt = itm.Prompt;
                newItem.Quantity = itm.Quantity;
                newItem.Removed = itm.Removed;
                newGroup.Items.Add(newItem);
            }
            newlist.Add(newGroup);

        }

        return newlist;
    }

    /// <summary>
    /// Removes an item from a shipment (Future or Standing)
    /// </summary>
    /// <param name="changeList">List of Schedule Objects</param>
    /// <param name="remNode">The node to remove</param>
    /// <param name="Tree">The FlyTreeView object containing the node</param>
    /// <param name="SkedTree">THe Standing ORders tree</param>
    /// <param name="originalList">Original Schedule Groups list</param>
    public void RemoveItem(List<ScheduleGroup> changeList, FlyTreeNode remNode, string Tree, FlyTreeView SkedTree, List<ScheduleGroup> originalList)
    {
        if (Tree == "Future")
        {
            Item itm = GetItem(changeList, int.Parse(remNode.Value));
            ScheduleGroup group = GetGroup(itm.GroupID, changeList);
            group.Modified = true;

            itm.Quantity = 0;
        }
        else
        {
            Item remItem = null;
            try
            {
                remItem = GetItem(originalList, int.Parse(remNode.Value), true);
            }
            catch { }
            foreach (FlyTreeNode node in SkedTree.Nodes[0].ChildNodes)
            {
                foreach (FlyTreeNode itmNode in node.ChildNodes)
                {
                    if (remItem != null && RemoveHTML(itmNode.Text).Contains(remItem.ProductName))
                    {
                        Item skedItem = GetItem(changeList, int.Parse(itmNode.Value));
                        skedItem.Removed = true;
                    }

                }
            }
        }

    }

    /// <summary>
    /// Get an item based on the ID
    /// </summary>
    /// <param name="changeList">List to look in</param>
    /// <param name="ID">ID of the Item</param>
    /// <returns>Item object if found, null if not</returns>
    public Item GetItem(List<ScheduleGroup> changeList, int ID)
    {
        return GetItem(changeList, ID, false);
    }

    /// <summary>
    /// Overload.  Allows checking by Profile ITem ID
    /// </summary>
    /// <param name="changeList"></param>
    /// <param name="ID"></param>
    /// <param name="ProfileItem"></param>
    /// <returns></returns>
    public Item GetItem(List<ScheduleGroup> changeList, int ID, bool ProfileItem)
    {
        foreach (ScheduleGroup group in changeList)
        {
            foreach (Item itm in group.Items)
            {
                if (itm.ID == ID && !ProfileItem)
                {
                    return itm;
                }
                if (itm.ProfileItemID == ID && ProfileItem)
                {
                    return itm;
                }
            }
        }
        return null;
    }

    /// <summary>
    /// Overload.  Not a one time item
    /// </summary>
    /// <param name="theList">List containing the Item object</param>
    /// <param name="ItemID">ID of the Item</param>
    /// <param name="newQuantity">New Quantity to enter</param>
    /// <returns>null</returns>
    public Item ChangeQuantity(List<ScheduleGroup> theList, int ItemID, int newQuantity)
    {
        Item checkItem = GetItem(theList, ItemID);

        return ChangeQuantity(theList, ItemID, newQuantity, checkItem.OneTime);
    }

    /// <summary>
    /// Change the quantity of the matching item object
    /// </summary>
    /// <param name="theList">List containing the Item object</param>
    /// <param name="ItemID">ID of the Item</param>
    /// <param name="newQuantity">New Quantity to enter</param>
    /// <param name="OneTime">true if this is a one time item</param>
    /// <returns>null</returns>
    public Item ChangeQuantity(List<ScheduleGroup> theList, int ItemID, int newQuantity, bool OneTime)
    {
        Item itm = GetItem(theList, ItemID);
        itm.Quantity = newQuantity;
        itm.OneTime = OneTime;
        ScheduleGroup group = GetGroup(itm.GroupID, theList);
        group.Modified = true;
        return null;
    }

    public void ChangeShipDate(ScheduleGroup theGroup, DateTime newShipDate)
    {
        theGroup.ShipDate = newShipDate;
        theGroup.Modified = true;
    }

    /// <summary>
    /// Get an ID for a new item
    /// </summary>
    /// <param name="theList">List to check</param>
    /// <returns>Item ID</returns>
    private int GetNewItemID(List<ScheduleGroup> theList)
    {
        int retVal = 0;
        foreach (ScheduleGroup group in theList)
        {
            foreach (Item itm in group.Items)
            {
                if (itm.ID > retVal) retVal = itm.ID;
            }
        }

        return ++retVal;
    }

    /// <summary>
    /// add an item to a sked group when dragged in
    /// </summary>
    /// <param name="theList">List for the item</param>
    /// <param name="theNode">Node with the info</param>
    /// <returns></returns>
    public Item AddOneTimeItem(List<ScheduleGroup> theList, FlyTreeNode theNode)
    {

        Item itm = CheckOneTime(theNode, theList);
        if (itm == null)
        {

            itm = new Item(theNode.Text);
            itm.OneTime = true;
            int newID = 0;
            foreach (ScheduleGroup group in theList)
            {
                foreach (Item theIem in group.Items)
                {
                    if (theIem.ID > newID)
                    {
                        newID = theIem.ID;
                    }
                }
            }

            itm.ID = newID;
            itm.DiscountID = 1;
            itm.DiscountName = "10% off";
            itm.GroupID = int.Parse(theNode.Parent.Value);
            itm.ProfileItemID = 0;
            itm.ProductName = GetProductName(itm.Prompt);
            itm.Quantity = 1;
            itm.Removed = false;
            itm.Modified = true;
            theNode.Value = newID.ToString();
        }
        else
        {
            itm.Modified = true;
            itm.Quantity = itm.Quantity + 1;
        }
        ScheduleGroup theGroup = GetGroup(itm.GroupID, theList);
        theGroup.Items.Add(itm);
        theGroup.Modified = true;
        return itm;
    }

    /// <summary>
    /// Gets the product name from a node text value
    /// </summary>
    /// <param name="prompt"></param>
    /// <returns></returns>
    private string GetProductName(string prompt)
    {
        string[] promptArray = RemoveHTML(prompt).Split(' ');
        string retVal = "";
        foreach (string seg in promptArray)
        {
            if (seg != "")
            {
                if (seg.Contains("Quantity"))
                {
                    break;
                }
                else
                {
                    retVal += seg + " ";
                }
            }
        }
        return retVal.Trim();
    }

    /// <summary>
    /// writes the contact record when save is pressed
    /// </summary>
    /// <param name="PatientID">Patient ID</param>
    /// <param name="MessageBody">Body previously built</param>
    private void WriteContactRecord(int PatientID, string MessageBody)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("contact_tbl_AS_Insert", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@AptType", 59);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.Parameters.AddWithValue("@MessageBody", MessageBody);
            cmd.Parameters.AddWithValue("@EmployeeID", StaffID);
            cmd.Parameters.AddWithValue("@AutoShipStr", "WriteContactRecord");
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    /// <summary>
    /// Builds the Contact message for Save All Changes
    /// </summary>
    /// <param name="Sked"></param>
    /// <param name="Future"></param>
    /// <param name="ChangedGroups"></param>
    /// <param name="originalGroups"></param>
    /// <param name="originalStanding"></param>
    /// <param name="PatientID"></param>
    public void AssembleContactRecs(FlyTreeView Sked, FlyTreeView Future, List<ScheduleGroup> ChangedGroups, List<ScheduleGroup> originalGroups, DataTable originalStanding, int PatientID, int sID)
    {
        StaffID = sID;
        string MessageBody = "";
        bool ChangesMade = false;
        //check Standing Orders.  use purple in prompt.
        foreach (FlyTreeNode theNode1 in Sked.Nodes[0].ChildNodes)
        {
            foreach (FlyTreeNode theNode in theNode1.ChildNodes)
            {
                if (theNode.Text.Contains("purple"))
                {
                    MessageBody += AddSkedItem(theNode, MessageBody, originalStanding);
                    ChangesMade = true;
                }
            }
        }
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("ProfileItems_GetTree", conn);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            SqlDataReader reader = OpenReader(cmd);
            while (reader.Read())
            {
                MessageBody += CheckDeletedNode(Sked, (int)reader["ProfileItemID"], (string)reader["ProductName"]);
                MessageBody += CheckDateChangeNode(Sked, (int)reader["ProfileItemID"], ((DateTime)reader["NextShipDate"]).ToShortDateString());
                MessageBody += CheckFreqChangeNode(Sked, (int)reader["ProfileItemID"], (string)reader["Frequency"]);
                ChangesMade = true;

            }
            conn.Close();
        }

        //now check future
        foreach (ScheduleGroup EndingGroup in ChangedGroups)
        {
            if (EndingGroup.Modified)
            {
                ScheduleGroup startingGroup = GetGroup(EndingGroup.ID, originalGroups);

                if (EndingGroup.City != startingGroup.City
                    || EndingGroup.State != startingGroup.State
                    || EndingGroup.StreetAddress != startingGroup.StreetAddress
                    || EndingGroup.Zip != startingGroup.Zip
                    || EndingGroup.ShipDate != startingGroup.ShipDate)
                {
                    MessageBody += AddGroupEntry(originalGroups, ChangedGroups, EndingGroup.ID, MessageBody);
                    ChangesMade = true;
                }

                foreach (Item itm in EndingGroup.Items)
                {

                    Item startingItem = GetItem(originalGroups, itm.ID);

                    if (itm.DiscountID != startingItem.DiscountID
                        || itm.Quantity != startingItem.Quantity
                        || itm.Removed
                        || itm.OneTime)
                    {
                        MessageBody += AddItemEntry(originalGroups, ChangedGroups, itm.ID, MessageBody);
                        ChangesMade = true;
                    }

                }
            }
        }
        if (ChangesMade && MessageBody != "Autoship changes made.<br/>")
        {
            MessageBody += "<br/>Changed by: " + StaffName;
            MessageBody = "Autoship changes made.<br/>" + MessageBody.Replace("Autoship changes made.<br/>", "");
            MarkEmail(PatientID);
            WriteContactRecord(PatientID, MessageBody);
        }
    }

    private string CheckFreqChangeNode(FlyTreeView theTree, int ProfileItemID, string originalFreq)
    {
        FlyTreeNode node = theTree.FindByValue(ProfileItemID.ToString());
        if (node != null)
        {
            string NewDue = RemoveHTML(node.Parent.Text).Split(' ')[1].Trim();
            if (originalFreq.Trim() != NewDue.Trim())
                return "Frequency changed from " + originalFreq + " to " + NewDue + " for " + node.Text + "<br />";
            else
                return "";
        }
        else
            return "";
    }

    private string CheckDateChangeNode(FlyTreeView theTree, int ProfileItemID, string originalShipDate)
    {
        FlyTreeNode node = theTree.FindByValue(ProfileItemID.ToString());
        if (node != null)
        {
            string NewDue = RemoveHTML(node.Parent.Text).Split(':')[1].Trim();
            if (originalShipDate.Trim() != NewDue.Trim())
                return "Ship date changed from " + originalShipDate + " to " + NewDue + " for " + node.Text + "<br />";
            else
                return "";
        }
        else
            return "";
    }

    private string CheckDeletedNode(FlyTreeView theTree, int ProfileItemID, string ProductName)
    {
        FlyTreeNode node = theTree.FindByValue(ProfileItemID.ToString());
        if (node != null)
            return "";
        else
            return "Product " + ProductName + " was removed.<br/>";
    }

    private void MarkEmail(int PatientID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update patients set AutoshipEmail=1 where PatientID=" + PatientID.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
            conn.Close();
        }
    }

    /// <summary>
    /// Prepares group contact record entry if group changes
    /// </summary>
    /// <param name="originalGroups">THe ORiginal Groups list</param>
    /// <param name="ChangedGroups">The Changed Groups List</param>
    /// <param name="ID">Group ID</param>
    /// <param name="MessageBody">The Message body before adding this</param>
    /// <returns>MEssage body with group info added</returns>
    private string AddGroupEntry(List<ScheduleGroup> originalGroups, List<ScheduleGroup> ChangedGroups, int ID, string MessageBody)
    {
        if (!MessageBody.Contains("The following future schedule changes were made."))
            MessageBody += "<br/>The following future schedule changes were made.<br/><br/>";

        ScheduleGroup oldGroup = GetGroup(ID, originalGroups);
        ScheduleGroup newGroup = GetGroup(ID, ChangedGroups);

        if (oldGroup != null)
        {
            MessageBody += "The shipment originally scheduled for " + oldGroup.ShipDate.ToShortDateString() + " was changed as follows:<br/> Original data:<br/>";
            MessageBody += "Ship Date: " + oldGroup.ShipDate.ToShortDateString() + "<br/>";
            MessageBody += "Address: " + oldGroup.StreetAddress + " " + oldGroup.City + ", " + oldGroup.State + " " + oldGroup.Zip + "<br/>";
            MessageBody += "Frequency: " + oldGroup.freq.ToString() + " months<br/><br/>New Data:<br/>";
        }
        MessageBody += "Ship Date: " + newGroup.ShipDate.ToShortDateString() + "<br/>";
        MessageBody += "Address: " + newGroup.StreetAddress + " " + newGroup.City + ", " + newGroup.State + " " + newGroup.Zip + "<br/>";
        MessageBody += "Frequency: " + newGroup.freq.ToString() + " months<br/><br/>";
        return MessageBody;
    }

    /// <summary>
    /// Adds the info for one item
    /// </summary>
    /// <param name="originalGroups">THe ORiginal Groups list</param>
    /// <param name="ChangedGroups">The Changed Groups List</param>
    /// <param name="ID">Item ID</param>
    /// <param name="MessageBody">The Message body before adding this</param>
    /// <returns>MEssage body with Item info added</returns>
    private string AddItemEntry(List<ScheduleGroup> originalGroups, List<ScheduleGroup> ChangedGroups, int ID, string MessageBody)
    {
        Item oldItem = GetItem(originalGroups, ID);
        Item newItem = GetItem(ChangedGroups, ID);
        ScheduleGroup oldGroup = GetGroup(oldItem.GroupID, originalGroups);
        ScheduleGroup newGroup = GetGroup(newItem.GroupID, ChangedGroups);

        if (!MessageBody.Contains("Changes were made to items scheduled to ship on " + oldGroup.ShipDate.ToShortDateString()))
            MessageBody += "Changes were made to items scheduled to ship on " + oldGroup.ShipDate.ToShortDateString() + "<br/>";
        if (oldItem != null)
            MessageBody += "Original item data: " + oldItem.Prompt + "<br/>";
        MessageBody += "New item data: " + newItem.Prompt + "<br/>";
        return MessageBody;
    }

    /// <summary>
    /// Add contact rec for Standing Orders
    /// </summary>
    /// <param name="theNode">Node with info</param>
    /// <param name="MessageBody">The Message body before adding this</param>
    /// <param name="originalStanding">DataTable with original satnding order info</param>
    /// <returns>MEssage body with info added</returns>
    private string AddSkedItem(FlyTreeNode theNode, string MessageBody, DataTable originalStanding)
    {
        if (MessageBody == null) MessageBody = "";
        if (MessageBody == "") MessageBody = "Autoship changes made.<br/>";
        if (!MessageBody.Contains("The following standing schedule changes were made."))
            MessageBody += "The following standing schedule changes were made.<br/><br/>";
        string ProdName = ExtractProdName(theNode.Text);
        DataRow theOne = null;
        foreach (DataRow dr in originalStanding.Rows)
        {
            if ((string)dr["ProductName"] == ProdName)
            {
                theOne = dr;
                break;
            }
        }
        string Prompt = "None";
        if (theOne != null)
        {
            Prompt = (string)theOne["ProductName"] + "<font color='green'> Quantity: " + ((int)theOne["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
            if (theOne["DiscountName"] != DBNull.Value) Prompt += (string)theOne["DiscountName"] + " </font>"; else Prompt += "None </font>";
        }
        MessageBody += "Previous Entry: " + Prompt + "<br/>New Entry: " + theNode.Text + " " + theNode.Parent.Text + "<br/>";

        return MessageBody;
    }


    /// <summary>
    /// Extract a product name from a prompt
    /// </summary>
    /// <param name="Prompt">The prompt</param>
    /// <returns>PRoduct Name</returns>
    private string ExtractProdName(string Prompt)
    {
        string[] promptArray = RemoveHTML(Prompt).Split(' ');
        string retVal = "";
        foreach (string seg in promptArray)
        {
            if (seg != "")
            {
                if (seg.Contains("Quantity"))
                    break;
                else
                    retVal += seg + " ";
            }
        }
        return retVal.Trim();
    }

    /// <summary>
    /// Reverts one item to it's standing order status.
    /// </summary>
    /// <param name="ID"><Item ID/param>
    /// <param name="origanalList">Original List</param>
    /// <param name="changedList">Changed List</param>
    /// <returns></returns>
    public string Revert(string ID, List<ScheduleGroup> origanalList, List<ScheduleGroup> changedList)
    {
        Item oldItem = GetItem(origanalList, int.Parse(ID));
        Item newItem = GetItem(changedList, int.Parse(ID));
        newItem.DiscountID = oldItem.DiscountID;
        newItem.DiscountName = oldItem.DiscountName;
        newItem.EndDate = oldItem.EndDate;
        newItem.IsExceptiom = false;
        newItem.OneTime = false;
        newItem.Quantity = oldItem.Quantity;
        newItem.StartDate = oldItem.StartDate;
        newItem.Reverted = true;
        newItem.Prompt = oldItem.Prompt;
        ScheduleGroup group = GetGroup(newItem.GroupID, changedList);
        group.Modified = true;
        return newItem.ProfileItemID.ToString();
    }

    /// <summary>
    /// Build a prompt from a DataRow
    /// </summary>
    /// <param name="dr">The DataRow</param>
    /// <returns>The Prompt</returns>
    public string BuildPrompt(DataRow dr)
    {
        string Prompt = (string)dr["ProductName"] + "<font color='green'> Quantity: " + ((int)dr["Quantity"]).ToString() + " </font><font color='blue'> Discount: ";
        if (dr["DiscountName"] != DBNull.Value) Prompt += (string)dr["DiscountName"] + " </font>"; else Prompt += "None </font>";
        return Prompt;
    }

    /// <summary>
    /// Build a prompt from an Item object
    /// </summary>
    /// <param name="itm">The Item object</param>
    /// <returns>The Prompt</returns>
    public string BuildPrompt(Item itm)
    {
        string Prompt = "";
        if (itm.IsExceptiom)
            Prompt += "<font color='blue'>" + itm.ProductName + "</font>";
        else
            Prompt += itm.ProductName + "<font color='green'> Quantity: " + itm.Quantity.ToString() + " </font><font color='blue'> Discount: ";
        Prompt += itm.DiscountName + " </font>";
        return Prompt;
    }

    /// <summary>
    /// Build a prompt based on an Item ID
    /// </summary>
    /// <param name="ItemID">The ID </param>
    /// <param name="changeList">The list containing the ITem</param>
    /// <returns>The Prompt</returns>
    public string BuildPrompt(int ItemID, List<ScheduleGroup> changeList)
    {
        Item itm = GetItem(changeList, ItemID);
        string Prompt = itm.ProductName + "<font color='green'> Quantity: " + itm.Quantity.ToString() + " </font><font color='blue'> Discount: ";
        Prompt += itm.DiscountName + " </font>";
        return Prompt;
    }

    /// <summary>
    /// Check to see if an item is a one time purchase
    /// </summary>
    /// <param name="node">The node with the info</param>
    /// <param name="changeList">The list of changed ScheduleGroups</param>
    /// <returns></returns>
    public Item CheckOneTime(FlyTreeNode node, List<ScheduleGroup> changeList)
    {
        string ProductName = GetProductName(node.Text);
        foreach (FlyTreeNode testNode in node.Parent.ChildNodes)
        {
            string checkName = GetProductName(testNode.Text);
            if (checkName == ProductName)
            {
                try
                {
                    Item checkItem = GetItem(changeList, int.Parse(testNode.Value));
                    if (checkItem.OneTime)
                    {
                        return checkItem;
                    }
                }
                catch { }
            }
        }
        return null;
    }

    public bool SendEmail(int PatientID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("select Lastname,FirstName,PatientID,Email from patients where patientID=" + PatientID.ToString(), conn);
            cmd.CommandType = CommandType.Text;

            DataTable dt = OpenTable(cmd);

            if ((string)dt.Rows[0]["Email"] != "")
            {
                string docHeader = "";
                string docFooter = "";
                //Build message body
                AppSettingsReader appReader = new AppSettingsReader();
                docHeader = (string)appReader.GetValue("docHeader", docHeader.GetType());
                StreamReader sr = new StreamReader(docHeader);
                string MessageBody = sr.ReadToEnd();
                sr.Close();
                docFooter = (string)appReader.GetValue("docFooter", docFooter.GetType());
                sr = new StreamReader(docFooter);
                docFooter = sr.ReadToEnd();
                sr.Close();
                MessageBody = MessageBody.Replace("Dear First Last,", "Dear " + (string)dt.Rows[0]["FirstName"] + " " + (string)dt.Rows[0]["Lastname"] + ",") + "<p style=\"FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 11pt\">";
                SqlCommand cmd1 = new SqlCommand("ProfileItems_GetTree", conn);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.AddWithValue("@PatientID", dt.Rows[0]["PatientID"]);

                DataTable dt1 = OpenTable(cmd1);

                MessageBody += AddOrderData((string)appReader.GetValue("shipmentLineSingle", docFooter.GetType()),
                    (string)appReader.GetValue("shipmentLineMultiple", docFooter.GetType()),
                    (string)appReader.GetValue("itemLine", docFooter.GetType()), dt1);
                MessageBody += "</p>" + docFooter;
                SendMessage("AutoShip@longevitymedicalclinic.com", "Longevity Medical Clinic", (string)dt.Rows[0]["Email"], (string)dt.Rows[0]["FirstName"] + " " + (string)dt.Rows[0]["Lastname"], MessageBody, "", "Your supplements order");
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public void SaveEmail(string Address, int PatientID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("update Patients set Email='" + Address + "' where PatientID=" + PatientID.ToString(), conn);
            cmd.CommandType = CommandType.Text;
            cmd.ExecuteNonQuery();
        }
    }

    private static string AddOrderData(string ShipmentTemplateSingle, string ShipmentTemplateMultiple, string ItemTemplate, DataTable dt)
    {
        string MessageBody = "";
        int currFreq = int.Parse((string)dt.Rows[0]["Frequency"]);
        DateTime currNextDate = (DateTime)dt.Rows[0]["NextShipDate"];
        if (currFreq == 1)
        {
            MessageBody += "<strong>" + ShipmentTemplateSingle.Replace("shipdate", currNextDate.ToString("MMMM d, yyyy")) + "</strong><br/>\r\n";
        }
        else
        {
            MessageBody += "<strong>" + ShipmentTemplateMultiple.Replace("shipdate", currNextDate.ToShortDateString()).Replace("Frequency", currFreq.ToString()) + "</strong><br/>\r\n";
        }

        foreach (DataRow dr in dt.Rows)
        {
            if (int.Parse((string)dr["Frequency"]) != currFreq || (DateTime)dr["NextShipDate"] != currNextDate)
            {
                currFreq = int.Parse((string)dr["Frequency"]);
                currNextDate = (DateTime)dr["NextShipDate"]; if (currFreq == 1)
                {
                    MessageBody += "<strong>" + ShipmentTemplateSingle.Replace("shipdate", currNextDate.ToString("MMMM d, yyyy")) + "</strong><br/>\r\n";
                }
                else
                {
                    MessageBody += "<strong>" + ShipmentTemplateMultiple.Replace("shipdate", currNextDate.ToString("MMMM d, yyyy")).Replace("Frequency", currFreq.ToString()) + "</strong><br/>\r\n";
                }

            }
            MessageBody += "&nbsp;&nbsp;&nbsp;" + ItemTemplate.Replace("Quanntity", ((int)dr["Quantity"]).ToString()).Replace("Product", (string)dr["ProductName"]).Replace("Discount", (string)dr["DiscountName"]).Replace(" caps", " capsules") + "<br/>\r\n ";
        }
        return MessageBody;
    }

    public static void SendMessage(string fromAddress, string fromName, string toAddress, string toName, string MessageBody, string Attachment, string Subject)
    {
        System.Net.Mail.MailAddress from = new System.Net.Mail.MailAddress(fromAddress, fromName);
        System.Net.Mail.MailMessage msg1 = new System.Net.Mail.MailMessage();
        msg1.From = from;
        msg1.To.Add(new System.Net.Mail.MailAddress(toAddress, toName));
        msg1.Subject = Subject;
        msg1.Body = MessageBody;
        msg1.IsBodyHtml = true;
        if (Attachment != "")
        {
            msg1.Attachments.Add(new System.Net.Mail.Attachment(Attachment));
        }
        System.Net.Mail.SmtpClient Client = new System.Net.Mail.SmtpClient("longevity-1.LongevityMedicalClinic.local");
        Client.Credentials = System.Net.CredentialCache.DefaultNetworkCredentials;
        Client.Send(msg1);
    }

    public DateTime GetPreviousShipDate(FlyTreeView theTree, FlyTreeNode theNode)
    {
        for (int x = 0; x < theTree.Nodes[0].ChildNodes.Count; x++)
        {
            if (theTree.Nodes[0].ChildNodes[x].Text == theNode.Text)
            {
                if (x == 0)
                {
                    return DateTime.MinValue;
                }
                else
                {
                    string oldText = RemoveHTML(theTree.Nodes[0].ChildNodes[x - 1].Text);
                    string[] oldArray = oldText.Split(' ');
                    return DateTime.Parse(RemoveHTML(oldArray[2]));
                }
            }
        }
        return DateTime.MinValue;
    }
    public DateTime GetNextShipDate(FlyTreeView theTree, FlyTreeNode theNode)
    {
        for (int x = 0; x < theTree.Nodes[0].ChildNodes.Count; x++)
        {
            if (theTree.Nodes[0].ChildNodes[x].Text == theNode.Text)
            {
                string oldText = "";
                try
                {
                    oldText = theTree.Nodes[0].ChildNodes[x + 1].Text;
                }
                catch
                {
                    return DateTime.MaxValue;
                }
                string[] oldArray = RemoveHTML(oldText).Split(' ');
                return DateTime.Parse(RemoveHTML(oldArray[2]));
            }
        }
        return DateTime.MinValue;
    }

    public static SqlDataReader OpenReader(SqlCommand cmd)
    {
        SqlDataReader reader = null;
        int Counter = 0;
        while (Counter < 10)
        {
            try
            {
                reader = cmd.ExecuteReader();
                break;
            }
            catch
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                cmd.Connection.Open();
                Counter++;
            }
        }
        return reader;
    }

    public static DataTable OpenTable(SqlCommand cmd)
    {
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        DataTable dt = new DataTable();
        int Counter = 0;
        while (Counter < 10)
        {
            try
            {
                da.Fill(dt);
                break;
            }
            catch
            {
                if (cmd.Connection.State == ConnectionState.Open)
                    cmd.Connection.Close();
                cmd.Connection.Open();
                Counter++;
            }

        }
        return dt;
    }



}

/// <summary>
/// A Group in Stading Orders
/// </summary>
public class Group
{
    public int freq { get; set; }
    public DateTime StartDate { get; set; }
    public int DaystoShip { get; set; }
    public List<Item> Items = new List<Item>();
}

/// <summary>
/// An Item for either group type
/// </summary>
public class Item
{
    public Item(string prompt)
    {
        Prompt = prompt;
        Removed = false;
        OneTime = false;
        IsExceptiom = false;
        Reverted = false;
        Modified = false;
    }
    public Item(string prompt, int id)
    {
        Prompt = prompt;
        ID = id;
        Removed = false;
        OneTime = false;
        IsExceptiom = false;
        Reverted = false;
        Modified = false;
    }
    public string Prompt { get; set; }
    public int ID { get; set; }
    public int GroupID { get; set; }
    public int ProfileItemID { get; set; }
    public bool Removed { get; set; }
    public int Quantity { get; set; }
    public int DiscountID { get; set; }
    public string DiscountName { get; set; }
    public bool OneTime { get; set; }
    public string ProductName { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public bool IsExceptiom { get; set; }
    public bool Reverted { get; set; }
    public bool Modified { get; set; }
}

/// <summary>
/// A Group for Future
/// </summary>
public class ScheduleGroup
{
    public ScheduleGroup()
    {
        Modified = false;
    }
    public int freq { get; set; }
    public DateTime ShipDate { get; set; }
    public List<Item> Items = new List<Item>();
    public string Text { get; set; }
    public int ID { get; set; }
    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
    public bool IsException { get; set; }
    public bool Modified { get; set; }
    public DateTime OrignalShipDate { get; set; }
}

