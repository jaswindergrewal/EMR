using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.Grid;
using System.Data;
using System.Data.SqlClient;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Web.Services;


public partial class Autoship_OneTime : LMCBase
{
    //private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    int PatientID = 5284;
    IOneTimeSaleService objIOneTimeSaleService = null;

    protected void Page_Load(object sender, EventArgs e)
    {
        PatientID = int.Parse(Request.QueryString["PatientID"]);
        lblPatientID.Text = Request.QueryString["PatientID"];
        if (!IsPostBack)
        {
            BindOneTimeOrderList();
        }
    }

    /// <summary>
    /// method for the one time order grid control.
    /// </summary>
    private void BindOneTimeOrderList()
    {
        List<OneTimeSaleViewModel> viewModel = null;
        try
        {
            objIOneTimeSaleService = new OneTimeSaleService();
            viewModel = new List<OneTimeSaleViewModel>();

            viewModel = objIOneTimeSaleService.GetAutoshipOneTimeOrderDetails(PatientID);

            grdOneTime.DataSource = viewModel;
            grdOneTime.DataBind();

            BindAutoshipProduct(); // bind dropdown for autoship product.
            BindAutoshipDiscount(); // bind dropdown for autoship discount.
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            viewModel = null;
            objIOneTimeSaleService = null;
        }
    }

    private string GetShortDate(DateTime? dte)
    {
        if (dte == null) return "";
        return ((DateTime)dte).ToShortDateString();
    }

    protected void grdOneTime_Rebind(object sender, EventArgs e)
    {       
        BindOneTimeOrderList();
    }

    protected void grdOneTime_InsertUpdate(object sender, GridRecordEventArgs e)
    {
        OneTimeSaleViewModel viewModel = null;
        try
        {
            objIOneTimeSaleService = new OneTimeSaleService();
            viewModel = new OneTimeSaleViewModel();

            viewModel.DiscountID = e.Record["DiscountID"].ToString() != "" ? int.Parse(e.Record["DiscountID"].ToString()) : 9;
            viewModel.ProductID = int.Parse(e.Record["ProductID"].ToString());          
            viewModel.Affiliate = bool.Parse(e.Record["Affiliate"].ToString());

            int OneTimeSaleID = 0;
            if (e.Record["OneTimeSaleID"].ToString() != "")
                OneTimeSaleID = int.Parse(e.Record["OneTimeSaleID"].ToString());

            int Quantity = 0;
            if (e.Record["Quantity"].ToString() != "")
                Quantity = int.Parse(e.Record["Quantity"].ToString());

            objIOneTimeSaleService.AddUpdateOneTimeSaleData(OneTimeSaleID, viewModel.DiscountID, PatientID, viewModel.ProductID, Quantity, viewModel.Affiliate);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIOneTimeSaleService = null;
            viewModel = null;
        }
    }

    protected void grdOneTime_Delete(object sender, GridRecordEventArgs e)
    {        
        try
        {
            objIOneTimeSaleService = new OneTimeSaleService();

            objIOneTimeSaleService.DeleteOneTimeSaleData(int.Parse(e.Record["OneTimeSaleID"].ToString()));
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objIOneTimeSaleService = null;
        }
    }

    protected void btnPlaceOrder_Click(object sender, EventArgs e)
    {
        //Patient pa = (from p in ctx.Patients
        //              where p.PatientID == PatientID
        //              select p).First();

        List<int> profIds = new List<int>();
        if (grdOneTime.Rows.Count > 1)
        {
            foreach (GridRow rec in grdOneTime.Rows)
            {               
                IProfileItemService objIProfileItemService = new ProfileItemService();
                objIProfileItemService.AddRecordsInProfileItem(PatientID, int.Parse(rec.Cells[1].Text), int.Parse(rec.Cells[3].Text),
                    DateTime.Parse(txtShipDate.Text).AddDays(-1),
                    DateTime.Parse(txtShipDate.Text).AddDays(1),
                    DateTime.Parse(txtShipDate.Text),
                    DateTime.Parse(txtShipDate.Text).AddMonths(-1),
                    DateTime.Parse(txtShipDate.Text).Day,
                    int.Parse(rec.Cells[0].Text));

                //profIds.Add(prof.ProfileItemID);
            }
        }
       
        pnlCreate.Visible = false;
        pnlComplete.Visible = true;
    }

    private void CloseOrder(int OrderID)
    {
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlTransaction tranClose = conn.BeginTransaction("Close");

            try
            {
                SqlCommand cmd = new SqlCommand("", conn);
                cmd.Transaction = tranClose;

                cmd.CommandText = "OrderItems_GetOrder";
                cmd.Parameters.AddWithValue("@OrderID", OrderID);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();
                da.Fill(dt);

                SqlCommand UpdateShip = new SqlCommand("ProfileItem_Ship", conn);
                UpdateShip.Transaction = tranClose;
                foreach (DataRow reader in dt.Rows)
                {
                    UpdateShip.Parameters.Clear();
                    UpdateShip.Parameters.AddWithValue("@OrderItemID", (int)reader["OrderItemID"]);
                    UpdateShip.CommandType = CommandType.StoredProcedure;
                    //UpdateShip.ExecuteNonQuery();
                    SqlDataReader theItem = UpdateShip.ExecuteReader();
                    if (theItem.Read())
                    {
                        if ((int)theItem["DayToShip"] > 28)
                        {
                            if (((DateTime)theItem["NextShipDate"]).Day != (int)theItem["DayToShip"])
                            {
                                try
                                {
                                    DateTime NewNext = DateTime.Parse((((DateTime)theItem["NextShipDate"]).Month).ToString() + "/" + ((int)theItem["DayToShip"]).ToString() + "/" + (((DateTime)theItem["NextShipDate"]).Year).ToString());
                                    SqlCommand ChangeDay = new SqlCommand("update ProfileItems set NextShipDate='" + NewNext.ToShortDateString() + "' where ProfileItemID=" + ((int)theItem["ProfileItemID"]).ToString(), conn, tranClose);
                                    ChangeDay.CommandType = CommandType.Text;
                                    theItem.Close();
                                    ChangeDay.ExecuteNonQuery();
                                }
                                catch (FormatException ex)
                                {
                                    theItem.Close();
                                }

                            }
                            else
                            {
                                theItem.Close();
                            }
                        }
                        else
                        {
                            try
                            {
                                theItem.Close();
                            }
                            catch { }
                        }
                    }
                    else
                        theItem.Close();


                }
                cmd.CommandText = "Orders_CloseOrder";
                cmd.ExecuteNonQuery();

                tranClose.Commit();
            }
            catch
            {
                tranClose.Rollback();
            }

        }

    }

    /// <summary>
    /// bind dropdown for autoship product.
    /// </summary>
    private void BindAutoshipProduct()
    {
        IAutoshipCancelReasonService objService = null;
        List<AutoshipProductsViewModel> lstModel = null;
        try
        {
            objService = new AutoshipCancelReasonService();
            lstModel = new List<AutoshipProductsViewModel>();
            lstModel = objService.GetAutoshipProductList();
            
            DropDownList drpProduct = grdOneTime.Templates[0].Container.FindControl("ddlProductName") as DropDownList;
            drpProduct.DataSource = lstModel;
            drpProduct.DataTextField = "ProductName";
            drpProduct.DataValueField = "ProductID";
            drpProduct.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
            lstModel = null;
        }
    }

    /// <summary>
    /// bind dropdown for autoship discount.
    /// </summary>
    private void BindAutoshipDiscount()
    {
        IAutoshipCancelReasonService objService = null;
        List<AutoshipDiscountViewModel> lstModel = null;
        try
        {
            objService = new AutoshipCancelReasonService();
            lstModel = new List<AutoshipDiscountViewModel>();
            lstModel = objService.GetAutoshipDiscount();
            
            DropDownList drpProduct = grdOneTime.Templates[0].Container.FindControl("ddlDiscount") as DropDownList;
            drpProduct.DataSource = lstModel;
            drpProduct.DataTextField = "DiscountName";
            drpProduct.DataValueField = "DiscountID";
            drpProduct.DataBind();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
            lstModel = null;
        }
    }

    //Method to check the One time order
    [WebMethod]
    public static string CheckOneTime(int PatientID, int ProductID, string ShipDate)
    {
        string result = "";
        IAutoshipProductService _objService = null;
        try
        {
            _objService = new AutoshipProductService();
            result = _objService.CheckOneTimeOrder(PatientID, ProductID, ShipDate);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            
        }
        finally
        {
            _objService = null;
            
        }
        return result;

    }

}

 