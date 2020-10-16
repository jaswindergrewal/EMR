using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
public partial class ViewReport : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request.QueryString["Id"] != null)
            {
                if (Request.QueryString["ReportTypeId"] != null)
                {
                    int ReportId = Convert.ToInt32(Request.QueryString["Id"]);
                    int ReportTypeId = Convert.ToInt32(Request.QueryString["ReportTypeId"]);
                    ILabReportService _objService = new LabReportService();
                    List<ReportListViewModel> ListReport = _objService.GetReportList(ReportTypeId);
                    foreach(var item in ListReport)
                    {
                        if(item.Id== ReportId)
                        {
                            ReportViewer1.ServerReport.ReportPath = "/"+item.ActualName+"/"+item.ReportName;
                            Uri myUri = new Uri("http://10.0.2.89/reportserver");
                            
                            ReportViewer1.ServerReport.ReportServerUrl = myUri;
                           
                        }
                    }
                    
                }
            }
        }
    }
}