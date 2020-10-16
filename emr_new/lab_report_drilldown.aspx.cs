using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Text;

public partial class lab_report_drilldown : System.Web.UI.Page
{
    #region Variables
    public string patientID = "";
    public string labName = "";
    ILabReportService objService = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["patientID"] != null)
            {                
                patientID = Request.QueryString["patientID"].ToString();                               
                if (Request.QueryString["labName"] != null)
                {
                    labName = Request.QueryString["labName"].ToString();                   
                }
                BindLabReportRecords();
               
            }
        }
        catch (System.Exception ex)
        {

        }
    }
    /// <summary>
    ///  Method to get labdrilldown records(obsurvation time/absurvation value) by patient id
    /// </summary>
    private void BindLabReportRecords()
    {
        try
        {
            objService = new LabReportService();
            StringBuilder str = new StringBuilder();
            List<LabDrilldownRecordsViewModel> objLabReportRecords = objService.GetLabDrilldownRecords(Convert.ToInt32(patientID),labName);
            if (objLabReportRecords != null)
            {
                int count = 0;
                str.Append("<b><u>" + labName + "</u></b>");
                foreach (var item in objLabReportRecords)
                {
                    string value = item.ObservationValue;
                    DateTime date = Convert.ToDateTime(item.ObservationDateTime);
                    count = count + 1;                   
                    str.Append("&nbsp;&nbsp;&nbsp;&nbsp;&nbsp");
                    str.Append("<br>");
                    str.Append("<table border='0' cellspacing='3' width=\"100%\">");
                    str.Append("<tr style=\"border: 1px solid #C0C0C0; padding: 0; background-color: #C0C0C0\">");
                    str.Append("<td width=\"50%\">");
                    str.Append(date);
                    str.Append("</td>");                    
                    str.Append("<td width=\"50%\">");
                    str.Append(value);
                    str.Append("</td>");
                    str.Append("</tr>");
                    str.Append("</table>");
                }
                litTestDetails.Text = str.ToString();
            }
        }
        catch (System.Exception ex)
        {
        }
    }
}