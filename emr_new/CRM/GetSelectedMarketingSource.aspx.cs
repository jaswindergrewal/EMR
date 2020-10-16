using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


public partial class CRM_GetSelectedMarketingSource : System.Web.UI.Page
{
    IManageService objService = null;
    protected void Page_Load(object sender, EventArgs e)
    {

        int prospectId = int.Parse(Request.QueryString["id"]);
        int Tabid = int.Parse(Request.QueryString["Tab"]);
        objService = new ManageService();
        List<MarketingSourceViewModel> mktScr = new List<MarketingSourceViewModel>();
        mktScr = objService.GetSelectedMarketingSource(prospectId, Tabid);
        Response.End();
    }
}