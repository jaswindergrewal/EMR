using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System.IO;
//using Microsoft.Office.Interop.Excel;

public partial class PatientFormXERO : System.Web.UI.Page
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
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
                if (Request.QueryString["gridFill"] == "1")
                {
                    strData = BindGridXeroContactsNotMatched(Request.QueryString["gridFill"], _search, nd, rows, page, sidx, sord);
                }
                else if (Request.QueryString["gridFill"] == "2")
                {
                    strData = BindGridSearchedMatch(Request.QueryString["gridFill"], _search, nd, rows, page, sidx, sord, Request.QueryString["FirstName"], Request.QueryString["LastName"]);
                }


                Response.ContentType = "application/json";
                Response.Write(strData);
                Response.End();
            }

        }
    }



    //onclick of button the tax rates get posted to XERO from EXCEL file.
    protected void btnTaxrateImport_Click(object sender, EventArgs e)
    {
        if (fldUplodTax.HasFile == false)
        {

            lblerror.Text = "Please select a file";
        }
        else
        {
            string csvPath = Server.MapPath("~/File/") + Path.GetFileName(fldUplodTax.PostedFile.FileName);
            fldUplodTax.SaveAs(csvPath);
            Response.Redirect("~/XeroAuthonticationCall.ashx?checkedTaxfile=" + csvPath);
        }

    }


    //onclick of upload button to the Invoice Created for credit and Sales get posted to XERO from CSV file.
    protected void Upload(object sender, EventArgs e)
    {
        if (FileUpload1.HasFile == false)
        {

            lblerror.Text = "Please select a file";
        }
        else
        {
            string csvPath = Server.MapPath("~/File/") + Path.GetFileName(FileUpload1.PostedFile.FileName);
            FileUpload1.SaveAs(csvPath);
            Response.Redirect("~/XeroAuthonticationCall.ashx?checkedfile=" + csvPath);
        }
    }

    public string BindGridXeroContactsNotMatched(string GrdTab, string _search, string nd, int rows, int page, string sidx, string sord)
    {
        IXeroAPIService objService = null;
        List<XeroNotMatchedContacts> lstPatientData = null;
        string strSerializeData = "";
        int intTotalPages = 0;
        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new XeroAPIService();

            //In case of 5 fill the grid for Contacts that are fatched form xero and not matched with application existing user.                    
            lstPatientData = objService.GetXeroPatientsNotMathed(page, rows, sord, sidx);

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
                      PatientID = d.PatientID,
                      cell = new string[] {
                        d.FirstName, d.LastName.ToString(),d.BillingStreet,d.BillingState,d.BillingCountry,d.PatientID.ToString()
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

    //onclick of button the Xero contacts that are entered from XERO get inserted in EMR database
    protected void btnImportContacts_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/XeroAuthonticationCall.ashx?ImportContactsList=1");
    }

    [System.Web.Services.WebMethod]
    public static string MatchTwoPatients(string PatientId, string ContactId)
    {
        IXeroAPIService objService = new XeroAPIService();
        string Result = objService.MatchAppPatientsWithXeroContacts(PatientId, ContactId);
        return Result;
    }

    protected void btnAccounts_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/XeroAuthonticationCall.ashx?ImportAccountList=1");
    }
}