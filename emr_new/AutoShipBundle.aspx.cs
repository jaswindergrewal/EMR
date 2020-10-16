using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Obout.Grid;

public partial class AutoShipBundle : System.Web.UI.Page
{
    AutoshipProductService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //objService = new AutoshipProductService();
            //grdBundleProduct.DataSource = objService.GetBundleGridData(Convert.ToInt32(drpBundleList.SelectedItem.Value));
            //grdBundleProduct.DataBind();
        }
    }
    protected void btnAddProduct_Click(object sender, EventArgs e)
    {
        int bundleId = Convert.ToInt32(drpBundleList.SelectedItem.Value);
        int ProductId = Convert.ToInt32(drpProducts.SelectedItem.Value);
        objService = new AutoshipProductService();
        objService.AddBundledata(bundleId, ProductId);

        objService = new AutoshipProductService();
        grdBundleProduct.DataSource = objService.GetBundleGridData(bundleId);
        grdBundleProduct.DataBind();
       
    }
    protected void drpBundleList_SelectedIndexChanged(object sender, EventArgs e)
    {
        objService = new AutoshipProductService();
        grdBundleProduct.DataSource = objService.GetBundleGridData(Convert.ToInt32(drpBundleList.SelectedItem.Value));
        grdBundleProduct.DataBind();
    }
}