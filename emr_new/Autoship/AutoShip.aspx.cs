using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Obout.Grid;
using System.Web.Services;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.Diagnostics;
using System.Web;
using System.Web.UI;
using System.Text;
using System.Collections;
using System.Net;
using Newtonsoft.Json.Serialization;
using System.Web.Script.Serialization;
using Newtonsoft.Json;

public partial class _Manager : LMCBase
{
    #region Global

    OrderService objService;

    ProfileItemService objProfileItemService;

    AutoshipProductService objAutoshipProductService;

    OrderService objOrderService;

    PatientService objPatientService;

    StaffService objStaffService;

    ContactService objContactService;

    #endregion

    #region Page Load & PreInit Event(s)

    void Page_PreInit(Object sender, EventArgs e)
    {
        if (Request.QueryString["MasterPage"] != null)
            this.MasterPageFile = Request.QueryString["MasterPage"];
        else
            this.MasterPageFile = "site.master";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!IsPostBack)
        {
            objService = new OrderService();

            //drpProduct.DataSource = objService.GetAllSkuProduts(null);
            //drpProduct.DataBind();
            ((TextBox)GenerateOrders.FindControl("txtDate")).Text = DateTime.Today.ToShortDateString();
            if (Session["UserID"] == null)
                Response.Redirect("../login.asp");
            User usr = new User(Session["UserID"].ToString());
            inpUserId.Value = usr.EmployeeID.ToString();
            if (usr.AutoshipAccess == "none") Response.Redirect("LandingPage.aspx");
            txtBegin.Text = DateTime.Today.ToString("d");
            txtEnd.Text = DateTime.Today.AddMonths(1).ToString("d");
            txtBeginClosed.Text = DateTime.Today.AddDays(-1).ToString("d");
            txtEndClosed.Text = DateTime.Today.AddDays(1).ToString("d");
            txtBeginClosed.Attributes.Add("readonly", "readonly");
            txtEndClosed.Attributes.Add("readonly", "readonly");
            txtBegin.Attributes.Add("readonly", "readonly");
            txtEnd.Attributes.Add("readonly", "readonly");
            if (Session["TabName"] != null)
            {
                if (!string.IsNullOrEmpty(Session["TabName"].ToString()))
                {
                    Autoship.ActiveTabIndex = 2;
                    if (Session["BeginDate"] != null && Session["EndDate"] != null)
                    {
                        if (!string.IsNullOrEmpty(Session["BeginDate"].ToString()) && !string.IsNullOrEmpty(Session["EndDate"].ToString()))
                        {
                            txtBegin.Text = Session["BeginDate"].ToString();
                            txtEnd.Text = Session["EndDate"].ToString();
                            Session["TabName"] = null;
                            BindCloseOrderGrid();
                        }
                    }


                }
            }
            //Fill the grid 
            if (!string.IsNullOrEmpty(Request.QueryString["gridFill"]))
            {

                string _search = Request.QueryString["_search"];
                string nd = Request.QueryString["_search"];
                int rows = Convert.ToInt16(Request.QueryString["rows"]);
                int page = Convert.ToInt16(Request.QueryString["page"]);
                string sidx = Request.QueryString["sidx"];
                string sord = Request.QueryString["sord"];
                Response.Clear();
                string strData = string.Empty;
                if (Request.QueryString["gridFill"] != "0")
                {
                    string OrderId;
                    if (!string.IsNullOrEmpty(Session["Xero"].ToString()))
                    {
                        if (Session["Xero"].ToString() == "1")
                        {
                            string FirstName = Session["FirstName"].ToString();
                            string LastName = Session["LastName"].ToString();
                            strData = BindGridSearchedMatch("2", _search, nd, rows, page, sidx, sord, FirstName, LastName);
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(Session["GridFill"].ToString()))
                            {
                                OrderId = Session["GridFill"].ToString();
                                strData = BindOrderItemGrid("1", _search, nd, 1000, page, sidx, sord, OrderId);
                            }
                        }
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(Session["GridFill"].ToString()))
                        {
                            OrderId = Session["GridFill"].ToString();
                            strData = BindOrderItemGrid("1", _search, nd, 1000, page, sidx, sord, OrderId);
                        }
                    }
                }

                Response.ContentType = "application/json";
                Response.Write(strData);
                Response.End();
            }
        }
    }

    #endregion

    #region "Tab Manage Complete Orders"


    /// <summary>
    /// Delete the row for the selected orderid and bind the grid 
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void grdDeleteOrders_RowDeleting(object sender, GridRecordEventArgs e)
    {
        objService = new OrderService();
        int orderId = int.Parse(e.Record["OrderID"].ToString());
        try
        {
            objService.DeleteOrder(orderId);
            BindgrdDeleteOrders();
        }
        catch (FormatException ex)
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
    /// On click of  Go button bind the grid  for complete orders
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnDeleteOrders_Click(object sender, EventArgs e)
    {
        BindgrdDeleteOrders();
    }

    /// <summary>
    /// Method to bind the complete order detailson the basis of start and end date
    /// </summary>
    public void BindgrdDeleteOrders()
    {
        DateTime startDate;
        DateTime endDate;
        //Check For Valid Date
        try
        {
            if (DateTime.TryParse(txtBeginClosed.Text, out startDate) && DateTime.TryParse(txtEndClosed.Text, out endDate))
            {
                objService = new OrderService();
                grdDeleteOrders.DataSource = objService.OrderGetToCloseAutoShip(startDate, endDate);
                grdDeleteOrders.Visible = true;
                grdDeleteOrders.DataBind();
            }
        }
        catch (FormatException ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }

    }


    #endregion

    #region "Tab Shipment status"
    public void DeleteOrderClose(int OrderId)
    {
        objOrderService = new OrderService();
        objOrderService.CancelAnOrder(OrderId);
        BindCloseOrderGrid();
    }

    //protected void CloseOrdersGrid_RowDeleting(Object sender, GridViewDeleteEventArgs e)
    //{
    //    //int orderId = (int)e.Keys[0];
    //    //if ((string)Session["CommandName"] == "Delete")
    //    //{
    //    //    /* Cancel Order*/
    //    //    try
    //    //    {
    //    //        objOrderService = new OrderService();
    //    //        objOrderService.CancelAnOrder(orderId);
    //    //    }
    //    //    catch (System.Exception ex)
    //    //    {
    //    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
    //    //    }
    //    //}
    //    //else
    //    //{
    //    //    /* Close Order*/
    //    //    try
    //    //    {
    //    //        objOrderService = new OrderService();
    //    //        objOrderService.CloseOrder(orderId);
    //    //    }
    //    //    catch (System.Exception ex)
    //    //    {
    //    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    //        Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
    //    //    }
    //    //    /* Select Order By Order Id*/
    //    //    List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> lstOrderItems = objOrderService.GetOrderItemsByOrderId(orderId);
    //    //    lstOrderItems.ForEach(i =>
    //    //    {
    //    //        /*Execute code for each OrderItem in an Order*/
    //    //        /*Update NextShipDate and LastShiped Date*/
    //    //        try
    //    //        {
    //    //            objOrderService.GetOrderItemById(i.OrderItemID);
    //    //        }
    //    //        catch (System.Exception ex)
    //    //        {
    //    //            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //    //            Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
    //    //        }
    //    //    });
    //    //}
    //    /* Bind CloseOrdersGrid Grid*/
    //    DateTime startDate;
    //    DateTime endDate;
    //    if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    {
    //        CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //        CloseOrdersGrid.Visible = true;
    //        CloseOrdersGrid.DataBind();
    //        e.Cancel = true;
    //        //objPatientService = new PatientService();
    //        //int PatientID = objPatientService.GetPatientIdByOrderId(orderId);
    //        //Response.Redirect("shortmanage.aspx?PatientID=" + PatientID.ToString() + "&StaffID=" + (int)Session["StaffID"], "_New", "");
    //        ////Commented by jaswinder as now he wants to open it in new window
    //        ////Response.Redirect("~/Manage.aspx?PatientID=" + PatientID.ToString() + "&StaffID=" +Session["StaffID"].ToString() + "&AutoShip=True",false);
    //    }

    //}

    #endregion


    #region Add Product

    protected void btnAddProduct_Click(object sender, GridRecordEventArgs e)
    {
        /*Created new object of AutoshipProductsViewModel and set all required properties then pass to BAL*/

        Emrdev.ViewModelLayer.AutoshipProductViewModel objProduct = new Emrdev.ViewModelLayer.AutoshipProductViewModel();
        objProduct.Active = bool.Parse((string)e.Record["Active"]);
        if (!string.IsNullOrEmpty(e.Record["AutoshipPrice"].ToString()))
            objProduct.AutoshipPrice = decimal.Parse((string)e.Record["AutoshipPrice"]);
        else
            objProduct.AutoshipPrice = decimal.Parse("0.00");
        //objProduct.Weight = (string)e.Record["Weight"];
        if (!string.IsNullOrEmpty(e.Record["Weight"].ToString()))
        {
            objProduct.Weight = e.Record["Weight"].ToString();
        }
        else
        {
            objProduct.Weight = null;
        }
        if (!string.IsNullOrEmpty(e.Record["Sku"].ToString()))
        {
            objProduct.Sku = e.Record["Sku"].ToString();
        }
        else
        {
            objProduct.Sku = "";
        }
        objProduct.ProductName = (string)e.Record["ProductName"];
        objProduct.Reviewed = bool.Parse((string)e.Record["Reviewed"]);
        objProduct.Viewable = bool.Parse((string)e.Record["Viewable"]);
        objProduct.Bundle = bool.Parse((string)e.Record["Bundle"]);
        objProduct.QBId = string.Empty;
        objAutoshipProductService = new AutoshipProductService();
        objAutoshipProductService.AddNewProduct(objProduct); /*Save New Autoship Product*/
        ProductsGrid.DataBind();
    }

    #endregion


    #region Grid "ProductsGrid" Events

    protected void ProductsGrid_RowUpdating(object sender, GridRecordEventArgs e)
    {
        /*Update Autoship Product*/

        Emrdev.ViewModelLayer.AutoshipProductViewModel objProduct = new Emrdev.ViewModelLayer.AutoshipProductViewModel();
        objProduct.ProductID = int.Parse(e.Record["ProductID"].ToString());
        objProduct.ProductName = e.Record["ProductName"].ToString();
        objProduct.AutoshipPrice = decimal.Parse(e.Record["AutoshipPrice"].ToString().Trim());
        objProduct.Active = Convert.ToBoolean(e.Record["Active"]);
        objProduct.Viewable = Convert.ToBoolean(e.Record["Viewable"]);
        objProduct.Reviewed = Convert.ToBoolean(e.Record["Reviewed"]);
        objProduct.Bundle = Convert.ToBoolean(e.Record["Bundle"]);
        if (!string.IsNullOrEmpty(e.Record["Weight"].ToString()))
        {
            objProduct.Weight = (e.Record["Weight"].ToString());
        }
        else
        {
            objProduct.Weight = null;
        }
        if (!string.IsNullOrEmpty(e.Record["Sku"].ToString()))
        {
            objProduct.Sku = e.Record["Sku"].ToString();
        }
        else
        {
            objProduct.Sku = "";
        }
        objAutoshipProductService = new AutoshipProductService();
        objAutoshipProductService.UpdateProduct(objProduct);
    }

    #endregion


    #region Preview Order

    protected void btnPreviewOrders_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            DateTime StartDate = DateTime.Parse(txtDate.Text);
            //get all items with a start date before date entered
            DataTable Candidates = AutoshipReportsUtil.GetCandidates(StartDate, conn);
            //remove candidates that have a freqeuency that rules them out
            bool[] RowsToRemove = new bool[Candidates.Rows.Count];
            for (int x = 0; x < Candidates.Rows.Count; x++)
            {
                DataRow dr = Candidates.Rows[x];


                if ((DateTime)dr["NextShipDate"] > StartDate)
                {
                    RowsToRemove[x] = true;
                }
                else
                {
                    RowsToRemove[x] = false;
                }

            }
            //remove the ones that were marked
            for (int x = 0; x < RowsToRemove.Count(); x++)
            {
                if (RowsToRemove[x])
                    Candidates.Rows[x].Delete();
            }

            Session["Batch"] = AutoshipReportsUtil.GenOrders(Candidates, true, conn, StartDate, int.Parse(Session["UserID"].ToString()));

            /* Initialize object of Order Service*/
            objOrderService = new OrderService();
            OrderViewGrid.DataSource = objOrderService.GetAllOrdersByBatchId(int.Parse(Session["Batch"].ToString()));
            OrderViewGrid.DataBind();
            OrderPreviewTable.Visible = true;
        }
    }

    #endregion


    #region Generate Order

    protected void btnGenOrders_OnClick(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            DateTime StartDate = DateTime.Parse(txtDate.Text);
            //get all items with a start date before date entered
            DataTable Candidates = AutoshipReportsUtil.GetCandidates(StartDate, conn);
            //remove candidates that have a freqeuency that rules them out
            bool[] RowsToRemove = new bool[Candidates.Rows.Count];
            DateTime ShipDate = DateTime.MinValue;
            int PatientID = 0;
            for (int x = 0; x < Candidates.Rows.Count; x++)
            {
                DataRow dr = Candidates.Rows[x];


                if ((DateTime)dr["NextShipDate"] > StartDate)
                {
                    RowsToRemove[x] = true;
                    continue;
                }
                //Remove row if order is on hold
                else if (dr["Hold"] != DBNull.Value)
                {
                    if (Convert.ToInt32(dr["Hold"]) > 0)
                    {
                        RowsToRemove[x] = true;
                        ShipDate = (DateTime)dr["NextShipDate"];
                        PatientID = (int)dr["PatientID"];
                        continue;

                    }
                }
                else if (ShipDate == (DateTime)dr["NextShipDate"] && PatientID == (int)dr["PatientID"])
                {
                    RowsToRemove[x] = true;
                }
                else
                {
                    RowsToRemove[x] = false;
                }

            }
            //remove the ones that were marked
            for (int x = 0; x < RowsToRemove.Count(); x++)
            {
                if (RowsToRemove[x])
                    Candidates.Rows[x].Delete();
            }
            Candidates.AcceptChanges();
            Session["Batch"] = AutoshipReportsUtil.GenOrders(Candidates, false, conn, StartDate, int.Parse(Session["UserID"].ToString()));
            /* Initialize object of Order Service*/
            objOrderService = new OrderService();
            OrderViewGrid.DataSource = objOrderService.GetAllOrdersByBatchId(int.Parse(Session["Batch"].ToString()));
            OrderViewGrid.DataBind();
            OrderPreviewTable.Visible = true;

        }
    }

    #endregion


    #region Grid " OrderViewGrid" Events

    protected void OrderViewGrid_SelectedIndexChanged(object sender, EventArgs e)
    {
        objOrderService = new OrderService();
        OrderItemsPreviewGrid.DataSource = objOrderService.GetOrderItemsByOrderId(int.Parse(OrderViewGrid.SelectedDataKey.Values["OrderID"].ToString()));
        OrderItemsPreviewGrid.DataBind();
    }

    #endregion


    #region Print

    protected void btnPrint_Click(object sender, EventArgs e)
    {
        Response.Redirect("http://lmcsql/Reports/Pages/Report.aspx?ItemPath=%2fAutoship%2fDaily+Shipments");
    }

    #endregion


    #region Grid "CloseOrdersGrid" Events

    //protected void CloseOrdersGrid_SelectedIndexChanged(object sender, EventArgs e)
    //{
    //    if ((string)Session["CommandName"] == "Delete")
    //    {
    //        objOrderService = new OrderService();
    //        objOrderService.CancelAnOrder(int.Parse(CloseOrdersGrid.SelectedRow.Cells[1].Text));
    //    }
    //    if ((string)Session["CommandName"] != "Delete")
    //    {

    //        objOrderService = new OrderService();
    //        objOrderService.ResetOrderCloseDate(int.Parse(CloseOrdersGrid.SelectedRow.Cells[1].Text));

    //        /*Get All Items for an Order*/
    //        objOrderService = new OrderService();
    //        List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> lstOrderItems = objOrderService.GetOrderItemsByOrderId(int.Parse(CloseOrdersGrid.SelectedRow.Cells[1].Text));
    //        lstOrderItems.ForEach(i =>
    //        {
    //            Emrdev.ViewModelLayer.ProfileItem_ShipViewModel objProfileItem = objOrderService.GetOrderItemById(i.OrderItemID);
    //            if (objProfileItem != null)
    //            {
    //                if (objProfileItem.DayToShip > 28)
    //                {
    //                    //if (objProfileItem.LastShipped.Day != objProfileItem.DayToShip)
    //                    //{
    //                    try
    //                    {

    //                        int days = Convert.ToInt16(objProfileItem.DayToShip);
    //                        DateTime CurreDate = objProfileItem.LastShipped.AddMonths(Convert.ToInt16(objProfileItem.Frequency));
    //                        int month = CurreDate.Month;
    //                        int year = CurreDate.Year;
    //                        DateTime NewNext;
    //                        objProfileItemService = new ProfileItemService();
    //                        switch (month)
    //                        {
    //                            case 1:
    //                            case 3:
    //                            case 5:
    //                            case 7:
    //                            case 8:
    //                            case 10:
    //                            case 12:
    //                                NewNext = new DateTime(year, month, days);

    //                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
    //                                break;
    //                            case 2:

    //                                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
    //                                {
    //                                    NewNext = new DateTime(year, 2, 29);

    //                                }
    //                                else
    //                                {
    //                                    NewNext = new DateTime(year, 2, 28);
    //                                }

    //                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
    //                                break;
    //                            case 4:
    //                            case 6:
    //                            case 9:
    //                            case 11:
    //                                if (days >= 30)
    //                                {
    //                                    NewNext = new DateTime(year, month, 30);

    //                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
    //                                }
    //                                else if (days == 29)
    //                                {
    //                                    NewNext = new DateTime(year, month, 29);

    //                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
    //                                }


    //                                break;

    //                        }


    //                        /*int days = ((int)objProfileItem.DayToShip) - 1;
    //                        DateTime NewNext = DateTime.Parse((((DateTime)objProfileItem.NextShipDate).Month).ToString() + "/" + "1" + "/" + (((DateTime)objProfileItem.NextShipDate).Year).ToString());
    //                        NewNext = NewNext.AddDays(days);*/
    //                        //DateTime NewNext = DateTime.Parse((((DateTime)objProfileItem.NextShipDate).Month).ToString() + "/" + ((int)objProfileItem.DayToShip).ToString() + "/" + (((DateTime)objProfileItem.NextShipDate).Year).ToString());
    //                        // DateTime NewNext = DateTime.Parse((((DateTime)(objProfileItem.NextShipDate)).Month).ToString() + "/" + ((int)objProfileItem.DayToShip).ToString() + "/" + (((DateTime)objProfileItem.NextShipDate).Year).ToString());
    //                        //DateTime NewNext = DateTime.Parse(((objProfileItem.NextShipDate).Month).ToString() + "/" + (objProfileItem.DayToShip).ToString() + "/" + ((objProfileItem.NextShipDate).Year).ToString());
    //                        /*objProfileItemService = new ProfileItemService();
    //                        objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);*/
    //                    }
    //                    catch (FormatException ex)
    //                    {
    //                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //                        Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
    //                    }

    //                }
    //                //}
    //            }
    //        });

    //    }
    //    DateTime startDate;
    //    DateTime endDate;
    //    if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    {
    //        /* Get Orders to close by StartDate and EndDate*/
    //        CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //        CloseOrdersGrid.Visible = true;
    //        CloseOrdersGrid.DataBind();
    //    }
    //}
    //protected void CloseOrdersGrid_RowCommand(object sender, GridViewCommandEventArgs e)
    //{



    //    if (e.CommandName == "Delete")
    //    {
    //        objOrderService = new OrderService();
    //        LinkButton btnDelete = (LinkButton)e.CommandSource;
    //        GridViewRow gvrow = (GridViewRow)btnDelete.NamingContainer;
    //        string orderId = CloseOrdersGrid.DataKeys[gvrow.RowIndex].Value.ToString();
    //        objOrderService.CancelAnOrder(Convert.ToInt32(orderId));

    //        CloseOrdersGrid.DeleteRow(Convert.ToInt32(orderId));
    //    }
    //    else if (e.CommandName == "ShowPopup")
    //    {
    //        LinkButton btnEdit = (LinkButton)e.CommandSource;
    //        GridViewRow gvrow = (GridViewRow)btnEdit.NamingContainer;
    //        objService=new OrderService(); 
    //        List<AutoShipStatusOrder> orderdeatails=objService.GetOrderById(Convert.ToInt32(CloseOrdersGrid.DataKeys[gvrow.RowIndex].Value.ToString()));
    //        if (orderdeatails.Count > 0)
    //        {
    //            txtBillingAddress.InnerHtml = orderdeatails[0].BillingStreet + "<br>" + orderdeatails[0].BillingCity + "<br>" + orderdeatails[0].BillingState + "<br>" +  orderdeatails[0].BillingZip + "<br>";
    //            lblName.Text = HttpUtility.HtmlDecode(gvrow.Cells[13].Text);
    //            lblShipDate.Text = HttpUtility.HtmlDecode(gvrow.Cells[14].Text);
    //            txtAutoShipNotes.InnerHtml = orderdeatails[0].AutoshipNote;
    //            txtOrderNotes.InnerText = orderdeatails[0].Note;
    //            txtShippingAddress.InnerText = orderdeatails[0].ShipAddress1 + "<br>" + orderdeatails[0].ShipCity + "<br>" + orderdeatails[0].ShipState + "<br>" + orderdeatails[0].ShipZip + "<br>";
    //        }
    //        Session["OrderId"] = Convert.ToInt32(CloseOrdersGrid.DataKeys[gvrow.RowIndex].Value.ToString());
    //        grdOrderItems.DataSource = orderdeatails;
    //        grdOrderItems.DataBind();

    //        //lblID.Text = CloseOrdersGrid.DataKeys[gvrow.RowIndex].Value.ToString();
    //        // DataRow dr = dt.Select("CustomerID=" + GridViewData.DataKeys[gvrow.RowIndex].Value.ToString())[0];
    //       // lblContactName.Text = HttpUtility.HtmlDecode(gvrow.Cells[2].Text);
    //        //txtAddress.Text = HttpUtility.HtmlDecode(gvrow.Cells[3].Text);
    //        //txtCity.Text = HttpUtility.HtmlDecode(gvrow.Cells[4].Text);
    //        Popup(true);
    //    }

    //    else
    //    {
    //        Session["CommandName"] = e.CommandName;
    //    }

    //    //DateTime startDate;
    //    //DateTime endDate;
    //    //if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    //{
    //    //    objOrderService = new OrderService();
    //    //    CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //    //    CloseOrdersGrid.Visible = true;
    //    //    CloseOrdersGrid.DataBind();
    //    //}
    //}

    protected void grdOderClose_Rebind(object sender, EventArgs e)
    {
        BindCloseOrderGrid();
    }

    [WebMethod]
    public static string GetGridData(string GridFill)
    {
        DataTable labTable = new DataTable();
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            int orderid = Convert.ToInt32(GridFill);

            using (SqlCommand cmd = new SqlCommand("ssp_GetOrderByIdForGrid", conn))
            {
                cmd.Parameters.Add(new SqlParameter("@OrderId", orderid));
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cmd;
                da.Fill(labTable);
            }

        }
        return JsonConvert.SerializeObject(labTable);



    }

    protected string BindOrderItemGrid(string GrdTab, string _search, string nd, int rows, int page, string sidx, string sord, string OrderId)
    {
        IOrderService objService = null;
        List<AutoShipStatusOrder> OrderDetails = null;
        string strSerializeData = "";
        int intTotalPages = 0;

        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new OrderService();

            //In case of 5 fill the grid for Contacts that are fatched form xero and not matched with application existing user.                    
            OrderDetails = objService.GetOrderById(Convert.ToInt32(OrderId.ToString()));

            if (OrderDetails.Count > 0)
            {
                intTotalPages = OrderDetails.Count > 0 ? OrderDetails.Count : 0;
                intTotalPages = (intTotalPages / rows) + 1;
            }
            var jsonData = new
            {
                total = intTotalPages,
                page = page,
                records = 100,
                rows = (
                  from d in OrderDetails
                  select new
                  {
                      OrderItemID = d.OrderItemID,
                      cell = new string[] {
                        d.Sku, d.ProductName.ToString(),d.Quantity.ToString(),d.Price.ToString(),d.Weight,d.OrderItemID.ToString(),d.OrderItemID.ToString()
                      }
                  }).ToArray()
            };
            strSerializeData = serializer.Serialize(jsonData);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            OrderDetails = null;
            objService = null;
        }
        return strSerializeData;

    }

    public string BindGridSearchedMatch(string GrdTab, string _search, string nd, int rows, int page, string sidx, string sord, string FirstName, string LastName)
    {
        IXeroAPIService objService = null;
        List<XEROpatientsMatchedSearchModel> lstPatientData = null;
        string strSerializeData = string.Empty;
        int intTotalPages = 0;
        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new XeroAPIService();

            lstPatientData = objService.GetXeroPatientsMathedSearch(page, rows, sord, sidx, FirstName, LastName);

            if (lstPatientData.Count > 0)
            {
                intTotalPages = lstPatientData[0].RecordCount.HasValue ? lstPatientData[0].RecordCount.Value : 0;
                intTotalPages = (intTotalPages / rows) + 1;
            }
            var jsonData = new
            {
                total = intTotalPages,
                page = page,
                records = rows,
                rows = (
                  from d in lstPatientData
                  select new
                  {
                      ContactId = d.ContactId,
                      cell = new string[] {
                        d.FirstName, d.LastName.ToString(),d.ContactId.ToString()
                      }
                  }).ToArray()
            };
            strSerializeData = serializer.Serialize(jsonData);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            lstPatientData = null;
            objService = null;
        }
        return strSerializeData;
    }

    protected void grdOrderItems_Rebind(object sender, EventArgs e)
    {
        //BindOrderItemGrid();
    }

    ////To show message after performing operations
    //void Popup(bool isDisplay)
    //{
    //    StringBuilder builder = new StringBuilder();
    //    if (isDisplay)
    //    {

    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "ShowPopup", "ShowPopup()", true);


    //    }
    //    else
    //    {
    //        ScriptManager.RegisterStartupScript(this, this.GetType(), "HidePopup", "HidePopup()", true);
    //    }
    //}

    //protected void btnUpdate_Click(object sender, EventArgs e)
    //{
    //        Popup(false);

    //}


    #endregion


    #region Grid "ReportsMenu" Events

    protected void ReportsMenu_MenuItemClick(object sender, MenuEventArgs e)
    {

        switch (e.Item.Value)
        {
            case "1":
                ReportsGrid.DataSource = AutoshipReportsUtil.CancelledOrders();
                ReportsGrid.EmptyDataText = "No cancelled orders found.";
                ReportHeader.InnerText = "Cancelled Orders";
                ReportsGrid.DataBind();
                VisDiv.Visible = true;
                ProdReportVis.Visible = false;
                break;
            case "2":
                ReportsGrid.DataSource = AutoshipReportsUtil.OpenOrders();
                ReportsGrid.EmptyDataText = "No open orders found.";
                ReportHeader.InnerText = "Open Orders";
                ReportsGrid.DataBind();
                VisDiv.Visible = true;
                ProdReportVis.Visible = false;
                break;
            case "3":
                ProdReportVis.Visible = true;
                VisDiv.Visible = false;
                break;
        }

    }

    #endregion

    #region Create Report

    protected void btnProdGen_Click(object sender, EventArgs e)
    {
        GrdProductDemand.DataSource = AutoshipReportsUtil.ProductDemand(DateTime.Parse(txtProdDate.Text));
        GrdProductDemand.EmptyDataText = "No product demand found.";
        ProductRreportHeader.InnerText = "Product Demand";
        GrdProductDemand.DataBind();

    }

    #endregion


    #region Grid "ManageRightsGrid" Events

    protected void ManageRightsGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    {
        int staffId = int.Parse(ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[1].Text);
        string Access = ((DropDownList)ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[4].Controls[1]).SelectedValue;
        objStaffService = new StaffService();
        objStaffService.UpdateStaffAutoShipAccess(staffId, Access != "Blank" ? Access : string.Empty);
        e.Cancel = true;
        ManageRightsGrid.EditIndex = -1;
        ManageRightsGrid.DataBind();
    }

    protected void ManageRightsGrid_RowEditing(object sender, GridViewEditEventArgs e)
    {
        int employeeId = int.Parse(ManageRightsGrid.Rows[e.NewEditIndex].Cells[1].Text);
        objStaffService = new StaffService();
        string access = objStaffService.GetAutoshipAccessByEmployeeID(employeeId);
        Session["Access"] = access;
        if ((string)Session["Access"] == string.Empty)
            Session["Access"] = "Blank";
    }

    protected void ManageRightsGrid_DataBound(object sender, EventArgs e)
    {
        if (ManageRightsGrid.EditIndex != -1)
        {
            ((DropDownList)ManageRightsGrid.Rows[ManageRightsGrid.EditIndex].Cells[4].Controls[1]).SelectedValue = (String)Session["Access"];
        }
    }

    #endregion


    #region Close Order

    protected void btnCloseOrders_Click(object sender, EventArgs e)
    {
        Session["BeginDate"] = txtBegin.Text;
        Session["EndDate"] = txtEnd.Text;
        BindCloseOrderGrid();
    }

    protected void grdOderClose_Select(object sender, GridRecordEventArgs e)
    {
        if (e.RecordsCollection != null)
        {
            int i = 0;
        }
    }
    public void BindCloseOrderGrid()
    {
        DateTime startDate;
        DateTime endDate;
        if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
        {
            objOrderService = new OrderService();
            grdOderClose.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
            // CloseOrdersGrid.Visible = true;
            grdOderClose.DataBind();
        }
    }

    #endregion


    #region Remove By BatchId

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            AutoshipReportsUtil.RemoveBacth((int)Session["Batch"], conn);
            OrderPreviewTable.Visible = false;
        }
    }

    #endregion


    #region Refresh Order

    protected void btnRrefershOrders_Click(object sender, EventArgs e)
    {
        OrderPreviewTable.Visible = false;
    }

    #endregion


    #region Grid "CloseOrdersGrid" Events

    //protected void CloseOrdersGrid_Editing(object sender, GridViewEditEventArgs e)
    //{
    //    //DateTime startDate;
    //    //DateTime endDate;
    //    //if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    //{
    //    //    objOrderService = new Emrdev.ServiceLayer.OrderService();
    //    //    CloseOrdersGrid.DataSource = objOrderService.OrderGetToClose(startDate, endDate);
    //    //    CloseOrdersGrid.Visible = true;
    //    //    CloseOrdersGrid.DataBind();
    //    //}
    //    CloseOrdersGrid.EditIndex = e.NewEditIndex;
    //}

    //protected void CloseOrdersGrid_RowUpdating(object sender, GridViewUpdateEventArgs e)
    //{
    //    objOrderService = new OrderService();
    //    objPatientService = new PatientService();
    //    string newNote = (string)e.NewValues["Note"];
    //    if (e.NewValues["Note"] != null) newNote = newNote.Replace("'", "''"); else newNote = "";
    //    objOrderService.UpdateNoteByOrderId(int.Parse(e.Keys["OrderID"].ToString()), newNote);/*Update Order Note*/

    //    /*Save New Contact Detail*/
    //    Contact_tblViewModel objModel = new Contact_tblViewModel();
    //    objContactService = new ContactService();
    //    objModel.AptType = 59;
    //    objModel.PatientID = objPatientService.GetPatientIdByOrderId(int.Parse(e.Keys["OrderID"].ToString()));/* Get Patient Id by OrderId*/
    //    objModel.MessageBody = "Order Note added: " + newNote;
    //    objModel.EnteredBy = (int)Session["StaffID"];
    //    objModel.Apt_ID = null;
    //    objModel.ContactDateEntered = DateTime.Now;
    //    objContactService.InsertContactDetail(objModel);
    //    /*End Here*/

    //    DateTime startDate;
    //    DateTime endDate;

    //    CloseOrdersGrid.EditIndex = -1;
    //    if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    {
    //        CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //        CloseOrdersGrid.Visible = true;
    //        CloseOrdersGrid.DataBind();
    //    }
    //}

    //protected void CloseOrdersGrid_RowCancelingEdit(object sender, EventArgs e)
    //{
    //    CloseOrdersGrid.EditIndex = -1;
    //    DateTime startDate;
    //    DateTime endDate;
    //    if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    {
    //        objOrderService = new OrderService();
    //        CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //        CloseOrdersGrid.Visible = true;
    //        CloseOrdersGrid.DataBind();
    //    }

    //}

    #endregion


    #region WebMethod

    /// <summary>
    /// this method is using for check the duplicate test during Add/Edit the record.    
    /// created by: Deepak Thakur
    /// created date: 06.Aug.2013        
    /// </summary>
    /// <param name="eventID"></param>
    /// <param name="eventName"></param>    
    /// <returns></returns>
    [WebMethod]
    public static string CheckDuplicateRecords(string ID, string Name, string tableName)
    {
        bool isExist = false;
        string strDuplicate = string.Empty;
        ILabScehduleService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(ID) || ID == "undefined")
                ID = "0";
            _objService = new LabScehduleService();
            isExist = _objService.CheckDuplicateRecords(Convert.ToInt32(ID), Name, tableName);
            if (isExist == true)
                strDuplicate = "duplicate";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }

    [WebMethod]
    public static string CheckDuplicateForProduct(string ID, string Name, string Sku, string tableName)
    {
        int isExist = 0;
        string strDuplicate = string.Empty;
        IOrderService _objService = null;
        try
        {
            if (string.IsNullOrEmpty(ID) || ID == "undefined")
                ID = "0";
            _objService = new OrderService();
            isExist = _objService.CheckDuplicateForProduct(Convert.ToInt32(ID), Name, Sku, tableName);
            if (isExist == 1)
                strDuplicate = "duplicate product";
            else if (isExist == 2)
                strDuplicate = "duplicate sku";
            else
                strDuplicate = string.Empty;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            _objService = null;
        }
        return strDuplicate;
    }

    [WebMethod]
    public static List<AutoShipStatusOrder> GetOrderById(string OrderId)
    {
        HttpContext.Current.Session["GridFill"] = OrderId;
        HttpContext.Current.Session["Xero"] = 0;
        IOrderService objService = new OrderService();
        List<AutoShipStatusOrder> orderDetails = objService.GetOrderById(Convert.ToInt32(OrderId));
        if (orderDetails != null)
        {
            if (orderDetails.Count > 0)
                HttpContext.Current.Session["XeroPatientId"] = orderDetails[0].XeropatientId;
        }

        return orderDetails;

    }

    [WebMethod]
    public static List<AutoShipProduct> GetAutoShipProducts(string ProductId)
    {
        IOrderService objService = new OrderService();
        int? productId = null;
        if (!string.IsNullOrEmpty(ProductId))
            productId = Convert.ToInt32(ProductId);

        List<AutoShipProduct> orderDetails = objService.GetAllSkuProduts(productId);
        return orderDetails;

    }

    [WebMethod]
    public static string DeleteOrderByIdWithReason(string OrderId, bool XeroDelete, string DeleteReason)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        IProfileItemService objProfileItemService = null;
        try
        {
            /*Get All Items for an Order*/
            objOrderService = new OrderService();
            List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> lstOrderItems = objOrderService.GetOrderItemsByOrderId(Convert.ToInt32(OrderId));
            lstOrderItems.ForEach(i =>
            {
                Emrdev.ViewModelLayer.ProfileItem_ShipViewModel objProfileItem = objOrderService.GetOrderItemById(i.OrderItemID);
                if (objProfileItem != null)
                {
                    if (objProfileItem.DayToShip > 28)
                    {
                        //if (objProfileItem.LastShipped.Day != objProfileItem.DayToShip)
                        //{
                        try
                        {

                            int days = Convert.ToInt16(objProfileItem.DayToShip);
                            DateTime CurreDate = objProfileItem.LastShipped.AddMonths(Convert.ToInt16(objProfileItem.Frequency));
                            int month = CurreDate.Month;
                            int year = CurreDate.Year;
                            DateTime NewNext;
                            objProfileItemService = new ProfileItemService();
                            switch (month)
                            {
                                case 1:
                                case 3:
                                case 5:
                                case 7:
                                case 8:
                                case 10:
                                case 12:
                                    NewNext = new DateTime(year, month, days);

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    break;
                                case 2:

                                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                                    {
                                        NewNext = new DateTime(year, 2, 29);

                                    }
                                    else
                                    {
                                        NewNext = new DateTime(year, 2, 28);
                                    }

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    break;
                                case 4:
                                case 6:
                                case 9:
                                case 11:
                                    if (days >= 30)
                                    {
                                        NewNext = new DateTime(year, month, 30);

                                        objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    }
                                    else if (days == 29)
                                    {
                                        NewNext = new DateTime(year, month, 29);

                                        objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    }


                                    break;

                            }

                        }
                        catch (FormatException ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            //Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
                        }

                    }
                    //}
                }
            });

            objOrderService = new OrderService();
            objOrderService.CancelAnOrderWithReason(Convert.ToInt32(OrderId), XeroDelete, DeleteReason);
            result = "Deleted";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }


    [WebMethod]
    public static string DeleteOrderById(string OrderId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        IProfileItemService objProfileItemService = null;
        try
        {
            /*Get All Items for an Order*/
            objOrderService = new OrderService();
            List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> lstOrderItems = objOrderService.GetOrderItemsByOrderId(Convert.ToInt32(OrderId));
            lstOrderItems.ForEach(i =>
            {
                Emrdev.ViewModelLayer.ProfileItem_ShipViewModel objProfileItem = objOrderService.GetOrderItemById(i.OrderItemID);
                if (objProfileItem != null)
                {
                    if (objProfileItem.DayToShip > 28)
                    {
                        //if (objProfileItem.LastShipped.Day != objProfileItem.DayToShip)
                        //{
                        try
                        {

                            int days = Convert.ToInt16(objProfileItem.DayToShip);
                            DateTime CurreDate = objProfileItem.LastShipped.AddMonths(Convert.ToInt16(objProfileItem.Frequency));
                            int month = CurreDate.Month;
                            int year = CurreDate.Year;
                            DateTime NewNext;
                            objProfileItemService = new ProfileItemService();
                            switch (month)
                            {
                                case 1:
                                case 3:
                                case 5:
                                case 7:
                                case 8:
                                case 10:
                                case 12:
                                    NewNext = new DateTime(year, month, days);

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    break;
                                case 2:

                                    if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                                    {
                                        NewNext = new DateTime(year, 2, 29);

                                    }
                                    else
                                    {
                                        NewNext = new DateTime(year, 2, 28);
                                    }

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    break;
                                case 4:
                                case 6:
                                case 9:
                                case 11:
                                    if (days >= 30)
                                    {
                                        NewNext = new DateTime(year, month, 30);

                                        objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    }
                                    else if (days == 29)
                                    {
                                        NewNext = new DateTime(year, month, 29);

                                        objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                    }


                                    break;

                            }

                        }
                        catch (FormatException ex)
                        {
                            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                            //Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
                        }

                    }
                    //}
                }
            });

            objOrderService = new OrderService();
            objOrderService.CancelAnOrder(Convert.ToInt32(OrderId));
            result = "Deleted";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string DeleteOrderByItemId(string OrderId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        IProfileItemService objProfileItemService = null;
        try
        {
            /*Get All Items for an Order*/
            objOrderService = new OrderService();

            Emrdev.ViewModelLayer.ProfileItem_ShipViewModel objProfileItem = objOrderService.GetOrderItemById(Convert.ToInt32(OrderId));
            if (objProfileItem != null)
            {
                if (objProfileItem.DayToShip > 28)
                {
                    //if (objProfileItem.LastShipped.Day != objProfileItem.DayToShip)
                    //{
                    try
                    {

                        int days = Convert.ToInt16(objProfileItem.DayToShip);
                        DateTime CurreDate = objProfileItem.LastShipped.AddMonths(Convert.ToInt16(objProfileItem.Frequency));
                        int month = CurreDate.Month;
                        int year = CurreDate.Year;
                        DateTime NewNext;
                        objProfileItemService = new ProfileItemService();
                        switch (month)
                        {
                            case 1:
                            case 3:
                            case 5:
                            case 7:
                            case 8:
                            case 10:
                            case 12:
                                NewNext = new DateTime(year, month, days);

                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                break;
                            case 2:

                                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                                {
                                    NewNext = new DateTime(year, 2, 29);

                                }
                                else
                                {
                                    NewNext = new DateTime(year, 2, 28);
                                }

                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                break;
                            case 4:
                            case 6:
                            case 9:
                            case 11:
                                if (days >= 30)
                                {
                                    NewNext = new DateTime(year, month, 30);

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                }
                                else if (days == 29)
                                {
                                    NewNext = new DateTime(year, month, 29);

                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                }


                                break;

                        }

                    }
                    catch (FormatException ex)
                    {
                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                        //Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
                    }

                }
                //}
            }


            objOrderService = new OrderService();
            objOrderService.DeleteOrderItem(Convert.ToInt32(OrderId));
            result = "Deleted";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string AddOrderItems(string OrderId, string Quantity, string Price, string Weight, string ProductId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService = new OrderService();
            objOrderService.AddOrderItems(Convert.ToInt32(OrderId), Convert.ToInt32(Quantity), Convert.ToDecimal(Price), Weight, Convert.ToInt32(ProductId));
            result = "Deleted";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string UpdateStatusById(string OrderId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService = new OrderService();
            objOrderService.ResetOrderStatus(Convert.ToInt32(OrderId), "Ready");
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string UpdatePaidStatus(string OrderId)
    {
        string result = string.Empty;
        IXeroAPIService objOrderService = null;
        try
        {
            objOrderService = new XeroAPIService();
            objOrderService.UpdateOrderPaid(Convert.ToInt32(OrderId));
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string UpdateFreeShippingStatus(string OrderId)
    {
        string result = string.Empty;
        IXeroAPIService objOrderService = null;
        try
        {
            objOrderService = new XeroAPIService();
            objOrderService.UpdateFreeShippingStatus(Convert.ToInt32(OrderId));
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }



    [WebMethod]
    public static string UpdateStatusByIdToggle(string OrderId)
    {

        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService = new OrderService();
            objOrderService.ResetToggleOrderStatus(Convert.ToInt32(OrderId), "Ready");
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;


    }


    [WebMethod]
    public static string UpdateAutoShipNote(string OrderId, string AutoShipNote,string UserId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService = new OrderService();
            objOrderService.UpdateAutoShipNote(Convert.ToInt32(OrderId), AutoShipNote, Convert.ToInt32(UserId));
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string UpdateOrderNote(string OrderId, string ShippingAddress, string ShippingCity, string ShippingState, string ShippingZip, string OrderNote,string UserId)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService = new OrderService();
            objOrderService.UpdateOrderNote(Convert.ToInt32(OrderId), OrderNote, ShippingAddress, ShippingCity, ShippingState, ShippingZip,Convert.ToInt32(UserId));
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static string UpdateQty(string OrderItemId, string Quantity)
    {
        string result = string.Empty;
        IOrderService objOrderService = null;
        try
        {
            objOrderService =
                new OrderService();
            objOrderService.UpdateQty(Convert.ToInt32(OrderItemId), Convert.ToInt32(Quantity));
            result = "Completed";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
        }
        finally
        {
            objOrderService = null;
        }
        return result;

    }

    [WebMethod]
    public static void CreateXeroContact(string FirstName, string LastName)
    {

        HttpContext.Current.Session["Xero"] = 1;
        HttpContext.Current.Session["FirstName"] = FirstName;
        HttpContext.Current.Session["LastName"] = LastName;


    }
    [WebMethod]
    public static string CheckForXeroPatient()
    {
        string Flag = "false";
        if (HttpContext.Current.Session["XeroPatientId"] != null)
        {
            Flag = "true";
        }
        return Flag;
    }

    [WebMethod]
    public static string CreateXeroInvoice()
    {
        string OrderId = HttpContext.Current.Session["GridFill"].ToString();
        return OrderId;

    }
    [WebMethod]

    public static string SetXeroPatientId(string XeroPatientId)
    {
        HttpContext.Current.Session["XeroPatientId"] = XeroPatientId;
        return "true";

    }


    [System.Web.Services.WebMethod]
    public static string MatchTwoPatients(string PatientId, string ContactId)
    {
        IXeroAPIService objService = new XeroAPIService();
        string Result = objService.MatchAppPatientsWithXeroContacts(PatientId, ContactId);
        return Result;
    }

    [System.Web.Services.WebMethod]
    public static List<XeroAccounts> GetXeroAccounts()
    {
        List<XeroAccounts> XeroAccounts = null;
        IXeroAPIService objService = new XeroAPIService();
        XeroAccounts = objService.GetXeroAccounts();
        return XeroAccounts;
    }

    [System.Web.Services.WebMethod]
    public static string UpdatePayment(string AccountCode, decimal Amount, DateTime PaymentDate, string PaymentReference)
    {
        string OrderId = HttpContext.Current.Session["GridFill"].ToString();
        IXeroAPIService objService = new XeroAPIService();
        objService.UpdatePayment(Convert.ToInt32(OrderId), AccountCode, Amount, PaymentDate, PaymentReference);
        return "";
    }


    #endregion
    protected void GrdProductDemand_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        DateTime StartDate = DateTime.Parse(txtProdDate.Text.ToString());
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[1].Text = StartDate.AddDays(1).ToString("MM/dd/yyyy");
            e.Row.Cells[2].Text = StartDate.AddDays(2).ToString("MM/dd/yyyy");
            e.Row.Cells[3].Text = StartDate.AddDays(3).ToString("MM/dd/yyyy");
            e.Row.Cells[4].Text = StartDate.AddDays(4).ToString("MM/dd/yyyy");
            e.Row.Cells[5].Text = StartDate.AddDays(5).ToString("MM/dd/yyyy");
            e.Row.Cells[6].Text = StartDate.AddDays(6).ToString("MM/dd/yyyy");
            e.Row.Cells[7].Text = StartDate.AddDays(6).ToString("MM/dd/yyyy");
        }
    }
    //protected void CloseOrdersGrid_RowEditing(object sender, GridViewEditEventArgs e)
    //{
    //    CloseOrdersGrid.EditIndex = e.NewEditIndex;
    //    DateTime startDate;
    //    DateTime endDate;
    //    if (DateTime.TryParse(txtBegin.Text, out startDate) && DateTime.TryParse(txtEnd.Text, out endDate))
    //    {
    //        objOrderService = new OrderService();
    //        CloseOrdersGrid.DataSource = objOrderService.OrderGetToCloseAutoShip(startDate, endDate);
    //        CloseOrdersGrid.Visible = true;
    //        CloseOrdersGrid.DataBind();
    //    }
    //}
    protected void grdOderClose_RowDataBound(object sender, GridRowEventArgs args)
    {

        if (args.Row.Cells[7].Text.ToLower() == "true")
        {

            args.Row.BackColor = System.Drawing.Color.LightGreen;
            args.Row.ForeColor = System.Drawing.Color.White;

        }
        else
        {
            if (args.Row.Cells[5].Text.ToLower() == "true")
            {
                args.Row.BackColor = System.Drawing.Color.Yellow;
                args.Row.ForeColor = System.Drawing.Color.White;
            }
            else
            {

                if (args.Row.Cells[8].Text.ToLower() == "true")
                {

                    args.Row.BackColor = System.Drawing.Color.Aqua;
                    args.Row.ForeColor = System.Drawing.Color.White;

                }
            }

        }



    }
    protected void ShipToReady_Click(object sender, EventArgs e)
    {
        foreach (Obout.Grid.GridRow row in grdOderClose.Rows)
        {
            Hashtable data = null;
            if (row.RowType == GridRowType.DataRow)
            {
                data = row.ToHashtable() as Hashtable;
                if (data["Status"].ToString() == "True")
                {
                    string checkExist = data["OrderID"].ToString();

                    WebClient Objclient = new WebClient();
                    Objclient.Headers.Add("Content-Type", "application/json;charset=UTF-8");

                    //jaswinder key
                    //Objclient.Headers.Add("Authorization", "Basic ODhkYjQwMGY4ZDI3NDZlZWJhMGU4Y2IxMDQxMzIzYWI6MWZiYzExYzRhOGUwNDRmMmJiOGQyZmEwOGI3YmIxZWE=");

                    // ClientID Key
                    // Objclient.Headers.Add("Authorization", "Basic YzMzY2RmYWEzMGQwNDg0ZGFkOGUzYzYzZjBhYmYzMmQ6ZGQ1MjVlZWQxN2UyNGM5Y2I0MmEyMmQ1ZWE4YjYyYzU=");

                    Objclient.Headers.Add("Authorization", "Basic NDhmZWY4NmI3OGI4NDZlNmEzNWE1NmQ0YzU1YzMwM2U6NzUzZmNmMjU3NTJkNGI1ZmE0ZGU1MDJjNjc3ZGZkNmE=");

                    var Result = Objclient.DownloadString("https://ssapi.shipstation.com/orders?ordernumber=" + checkExist + "&orderstatus=awaiting_shipment");
                    JavaScriptSerializer parser = new JavaScriptSerializer();
                    var info = parser.Deserialize<Orders1>(Result);
                    int count = info.orders.Count();
                    if (count <= 0)
                    {

                        objOrderService = new OrderService();
                        objOrderService.ResetOrderCloseDate(int.Parse(data["OrderID"].ToString()));
                        /*Get All Items for an Order*/
                        objOrderService = new OrderService();
                        List<Emrdev.ViewModelLayer.OrderItems_GetOrderViewModel> lstOrderItems = objOrderService.GetOrderItemsByOrderId(int.Parse(data["OrderID"].ToString()));
                        lstOrderItems.ForEach(i =>
                        {
                            Emrdev.ViewModelLayer.ProfileItem_ShipViewModel objProfileItem = objOrderService.GetOrderItemById(i.OrderItemID);
                            if (objProfileItem != null)
                            {
                                if (objProfileItem.DayToShip > 28)
                                {
                                    //if (objProfileItem.LastShipped.Day != objProfileItem.DayToShip)
                                    //{
                                    try
                                    {

                                        int days = Convert.ToInt16(objProfileItem.DayToShip);
                                        DateTime CurreDate = objProfileItem.LastShipped.AddMonths(Convert.ToInt16(objProfileItem.Frequency));
                                        int month = CurreDate.Month;
                                        int year = CurreDate.Year;
                                        DateTime NewNext;
                                        objProfileItemService = new ProfileItemService();
                                        switch (month)
                                        {
                                            case 1:
                                            case 3:
                                            case 5:
                                            case 7:
                                            case 8:
                                            case 10:
                                            case 12:
                                                NewNext = new DateTime(year, month, days);

                                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                                break;
                                            case 2:

                                                if ((year % 4 == 0 && year % 100 != 0) || (year % 400 == 0))
                                                {
                                                    NewNext = new DateTime(year, 2, 29);

                                                }
                                                else
                                                {
                                                    NewNext = new DateTime(year, 2, 28);
                                                }

                                                objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                                break;
                                            case 4:
                                            case 6:
                                            case 9:
                                            case 11:
                                                if (days >= 30)
                                                {
                                                    NewNext = new DateTime(year, month, 30);

                                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                                }
                                                else if (days == 29)
                                                {
                                                    NewNext = new DateTime(year, month, 29);

                                                    objProfileItemService.UpdateNextShipDateByProfileItemId(objProfileItem.ProfileItemID, NewNext);
                                                }


                                                break;

                                        }

                                    }
                                    catch (FormatException ex)
                                    {
                                        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
                                        Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
                                    }

                                }
                                //}
                            }
                        });
                        SyncApiIntegrationforInsertShipstation(int.Parse(data["OrderID"].ToString()));
                        //
                    }
                }
            }
        }


        BindCloseOrderGrid();

    }


    public void SyncApiIntegrationforInsertShipstation(int orderid)
    {

        string shipped_awating_Order_No = string.Empty;
        string postcode = string.Empty;
        string county = string.Empty;
        objOrderService = new OrderService();
        ReadyToShippedOrder ReadyToShippedOrder = objOrderService.GetOrderForShippedByID(orderid);
        if (ReadyToShippedOrder != null)
        {
            string checkExist = orderid.ToString();

            WebClient Objclient = new WebClient();
            Objclient.Headers.Add("Content-Type", "application/json;charset=UTF-8");
            //Objclient.Headers.Add("Authorization", "Basic Yzg3NzU3ODk5ZWUzNGJjYWIyMzc2Yzg4MmJhODNhYmU6NWQzY2NkZGRlZmIxNDVhNzgyMzdlNTBiNGVkMzUwODc=");
            // Objclient.Headers.Add("Authorization", "Basic NThlM2QwY2NmYWMzNDcyZmI4NTljYjZmNzAwOWVmODk6ZThjMDJmNjZkYWIyNDMxYzgyN2IzOTljN2Y2ODJiYzM=");//For Registered Key  
            //jaswinder key
            //Objclient.Headers.Add("Authorization", "Basic ODhkYjQwMGY4ZDI3NDZlZWJhMGU4Y2IxMDQxMzIzYWI6MWZiYzExYzRhOGUwNDRmMmJiOGQyZmEwOGI3YmIxZWE=");

            // ClientID Key
            // Objclient.Headers.Add("Authorization", "Basic YzMzY2RmYWEzMGQwNDg0ZGFkOGUzYzYzZjBhYmYzMmQ6ZGQ1MjVlZWQxN2UyNGM5Y2I0MmEyMmQ1ZWE4YjYyYzU=");

              Objclient.Headers.Add("Authorization", "Basic NDhmZWY4NmI3OGI4NDZlNmEzNWE1NmQ0YzU1YzMwM2U6NzUzZmNmMjU3NTJkNGI1ZmE0ZGU1MDJjNjc3ZGZkNmE=");

            var Result = Objclient.DownloadString("https://ssapi.shipstation.com/orders?ordernumber=" + checkExist + "&orderstatus=awaiting_shipment");
            JavaScriptSerializer parser = new JavaScriptSerializer();
            var info = parser.Deserialize<Orders1>(Result);
            int count = info.orders.Count();
            if (count <= 0)
            {
                try
                {
                    var requestor = new Container();
                    requestor.orderNumber = orderid.ToString();
                    requestor.orderDate = ReadyToShippedOrder.orderDate;
                    requestor.orderStatus = ReadyToShippedOrder.orderStatus;
                    requestor.orderKey = ReadyToShippedOrder.orderKey;

                    requestor.billto = new billto
                    {
                        name = ReadyToShippedOrder.Name
                    };
                    if (!string.IsNullOrEmpty(ReadyToShippedOrder.PostalCode))
                    {
                        postcode = ReadyToShippedOrder.PostalCode;
                    }
                    else
                    {
                        postcode = "";
                    }
                    if (!string.IsNullOrEmpty(ReadyToShippedOrder.country))
                    {
                        county = ReadyToShippedOrder.country;
                    }
                    else
                    {
                        county = "";
                    }
                    if (string.IsNullOrEmpty(ReadyToShippedOrder.street1.ToString()) && string.IsNullOrEmpty(ReadyToShippedOrder.street2.ToString()))
                    {
                        ReadyToShippedOrder.street1 = "TempStreet";
                    }
                    if (string.IsNullOrEmpty(ReadyToShippedOrder.city.ToString()) && string.IsNullOrEmpty(ReadyToShippedOrder.state.ToString()))
                    {
                        ReadyToShippedOrder.city = "TempCity";
                    }
                    requestor.shipto = new shipto
                    {
                        name = ReadyToShippedOrder.Name,
                        residential = true,
                        company = ReadyToShippedOrder.company,
                        street1 = ReadyToShippedOrder.street1,
                        street2 = ReadyToShippedOrder.street2,
                        city = ReadyToShippedOrder.city,
                        state = ReadyToShippedOrder.state,
                        postalCode = postcode,
                        country = county
                    };
                    List<AutoShipStatusOrder> itemOrders = objOrderService.GetOrderById(Convert.ToInt32(orderid));
                    //List<OrderItems_GetOrderViewModel> itemOrders = objOrderService.GetOrderItemsByOrderId(orderid);
                    List<ItemS> ItemsToAdd = new List<ItemS>();
                    for (int k = 0; k < itemOrders.Count; k++)
                    {
                        string prduct_name = itemOrders[k].ProductName;
                        int quantity = Convert.ToInt32(itemOrders[k].Quantity);
                        Double price = Convert.ToDouble(itemOrders[k].Price);
                        string sku = itemOrders[k].Sku;
                        requestor.items = new List<ItemS>
                          {
                             new  ItemS { sku = sku, name =prduct_name ,quantity=quantity,unitPrice=price}

                          };
                        foreach (var dataitem in requestor.items)
                        {
                            ItemsToAdd.Add(dataitem);
                        }
                        requestor.items = ItemsToAdd;
                        JsonSerializerSettings settings = new JsonSerializerSettings();
                        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
                        var data = JsonConvert.SerializeObject(requestor, settings);
                        string path = "https://ssapi.shipstation.com/orders/createorder";
                        StringBuilder sb = new StringBuilder();
                        //string test = "c87757899ee34bcab2376c882ba83abe:5d3ccdddefb145a78237e50b4ed35087";
                        // string test = "c33cdfaa30d0484dad8e3c63f0abf32d:dd525eed17e24c9cb42a22d5ea8b62c5";//Registered key  
                        string test = "48fef86b78b846e6a35a56d4c55c303e:753fcf25752d4b5fa4de502c677dfd6a";//Registered key  
                        //jaswinderkey
                        //test = "88db400f8d2746eeba0e8cb1041323ab:1fbc11c4a8e044f2bb8d2fa08b7bb1ea";
                        UTF8Encoding enc = new UTF8Encoding();
                        byte[] b = enc.GetBytes(test);
                        string cvtd = Convert.ToBase64String(b);
                        sb.AppendLine(cvtd);
                        byte[] c = Convert.FromBase64String(cvtd);
                        string backAgain = enc.GetString(c);
                        backAgain = sb.ToString();
                        string URL = "Basic" + " " + backAgain;
                        WebClient client = new WebClient();
                        // string data = new JavaScriptSerializer().Serialize(new { requestor });
                        client.Headers.Add("Content-Type", "application/json;charset=UTF-8");
                        client.Headers.Add("Authorization", URL);
                        string Result133 = client.UploadString(path, "POST", data);
                        string ff = Result133;
                    }
                }
                catch (WebException ex)
                {
                    ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "Startup", "CheckforSipstationMsg();", true);
                }


            }
        }
    }

}
