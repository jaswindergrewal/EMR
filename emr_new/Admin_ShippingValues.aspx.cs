using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Admin_ShippingValues : System.Web.UI.Page
{
    IAutoshipProductService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            objService = new AutoshipProductService(); ;
            ShippingValues ShippingValues= objService.GetShippingValues();
            txtShippingFee.Text = ShippingValues.ShippingFee.ToString();
            txtOrderLimit.Text = ShippingValues.OrderTotalLimit.ToString();
        }
    }
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        txtOrderLimit.Text = "";
        txtShippingFee.Text = "";
    }
    protected void btnSumit_Click(object sender, EventArgs e)
    {
        objService = new AutoshipProductService();
        if (txtShippingFee.Text != "" && txtOrderLimit.Text != "")
        {
            decimal ShippingFee = Convert.ToDecimal(txtShippingFee.Text);
            decimal OrderLimit = Convert.ToDecimal(txtOrderLimit.Text);
            objService.AddShippingValues(ShippingFee, OrderLimit);
        }
    }
}