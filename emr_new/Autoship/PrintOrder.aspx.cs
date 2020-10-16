using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using Emrdev.ViewModelLayer;
using Emrdev.ServiceLayer;
using System.Reflection;

public partial class PrintOrder : LMCBase
{
    IAppointmentConsole objIAppointmentConsole = null;
    IProfileItemService objIProfileItemService = null;
    string connection = ConfigurationManager.ConnectionStrings["db"].ConnectionString;
    int PatientID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        int PatientID = 5011;
        if (Request.QueryString["PatientID"] != "")
            PatientID = int.Parse(Request.QueryString["PatientID"]);
        if (!Page.IsPostBack)
        {
            BindData(PatientID);
        }
     
    }

    /// <summary>
    /// code for display the data for print the report.
    /// </summary>
    private void BindData(int PatientIDForPrint)
    {
        try
        {
            objIAppointmentConsole = new AppointmentConsole();
            PatientViewModel viewModel = objIAppointmentConsole.GetPatientList(PatientIDForPrint);

            string docHeader = "";
            string docFooter = "";

            if (viewModel != null)
            {
                //Build message body
                AppSettingsReader appReader = new AppSettingsReader();
                //docHeader = (string)appReader.GetValue("docHeader", docHeader.GetType());
                docHeader = Server.MapPath("~/Autoship/Header.htm");
                StreamReader sr = new StreamReader(docHeader);
                string MessageBody = sr.ReadToEnd();
                //sr.Close();
                //docFooter = (string)appReader.GetValue("docFooter", docFooter.GetType());
                docFooter = Server.MapPath("~/Autoship/Footer.htm");
                sr = new StreamReader(docFooter);
                docFooter = sr.ReadToEnd();
                //sr.Close();

                MessageBody = MessageBody.Replace("Dear First Last,", "Dear " + viewModel.FirstName + " " + viewModel.LastName + ",") + "<p style=\"FONT-FAMILY: 'Arial','sans-serif'; FONT-SIZE: 11pt\">";

                sr.Close();
                objIProfileItemService = new ProfileItemService();

                DataTable dt = new DataTable();
                dt = objIProfileItemService.ProfileItemGetTree1(PatientIDForPrint, connection);

                if (dt.Rows.Count > 0)
                {
                    MessageBody += AddOrderData((string)appReader.GetValue("shipmentLineSingle", docFooter.GetType()),
                        (string)appReader.GetValue("shipmentLineMultiple", docFooter.GetType()),
                        (string)appReader.GetValue("itemLine", docFooter.GetType()), dt);
                    MessageBody += "</p>" + docFooter;
                    MessageBody = MessageBody.Remove(0, MessageBody.IndexOf("<body>") + 6);
                    MessageBody = MessageBody.Remove(MessageBody.IndexOf("</body>"));
                    MessageBody = MessageBody.Replace("You may also reply to this email or write ", "You may also email us at ");
                }
                lblContent.Text = MessageBody;
            }
            else
            {
                lblContent.Text = "Sorry, No Record Exist";
            }


        }
        catch (System.Exception)
        {

            throw;
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

}