using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


public partial class PatientMerged_UnMerged : LMCBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hdnStaff.Value = Session["StaffID"].ToString();
        //On page load pass the parameteres for the Jqgrid to fill the data
        if (!IsPostBack)
        {
            
            if (!string.IsNullOrEmpty(Request.QueryString["gridFill"]))
            {

                string _search = Request.QueryString["_search"];
                string nd = Request.QueryString["_search"];
                int rows = Convert.ToInt16(Request.QueryString["rows"]);
                int page = Convert.ToInt16(Request.QueryString["page"]);
                string sidx = Request.QueryString["sidx"];
                string sord = Request.QueryString["sord"];
                string SearchField = string.Empty;
                string searchString = string.Empty;
                if (_search == "true")
                {
                    SearchField = Request.QueryString["searchField"];
                    searchString = Request.QueryString["searchString"];
                }
                Response.Clear();
                string strTicketData = BindGridData((int)Session["StaffID"], Request.QueryString["gridFill"], _search, nd, rows, page, sidx, sord, SearchField, searchString);
                Response.ContentType = "application/json";
                Response.Write(strTicketData);
                Response.End();
                //Response.Flush();
            }


        }

    }


    //Method return json for the ticket Grids
    //1to 6 represent the tab order
    public string BindGridData(int staffID, string GrdTab, string _search, string nd, int rows, int page, string sidx, string sord, string SearchField, string searchString)
    {
        IEmailTemplateService objService = null;
        List<MergedPatientViewModel> lstMergedPatient = null;
        string strSerializeData = "";
        int IsSearch = 0;
        int intTotalPages = 0;
        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new EmailTemplateService();
            if (_search == "true")
                IsSearch = 1;

            switch (GrdTab)
            {
                case "1":
                    lstMergedPatient = objService.GetPatienstLisTotMerge(page, rows, sord, sidx, IsSearch, SearchField, searchString);

                    if (lstMergedPatient.Count > 0)
                    {
                        intTotalPages = lstMergedPatient[0].RecordCount;
                        intTotalPages = (intTotalPages / rows) + 1;
                    }
                    var unMergedPatientData = new
                    {

                        total = intTotalPages,
                        page = page,
                        records = rows,
                        rows = (
                          from d in lstMergedPatient
                          select new
                          {
                              Patientid = d.Patientid,
                              cell = new string[] {
                                d.Patientid.ToString(),d.firstName, d.LastName,d.email ,d.clinic,
                               d.WorkPhone,d.sex,d.actualPatientID.ToString()
                              }
                          }).ToArray()
                    };

                    strSerializeData = serializer.Serialize(unMergedPatientData);
                    break;


                case "2":
                    lstMergedPatient = objService.GetMergedPatientRecord(page, rows, sord, sidx, IsSearch, SearchField, searchString);

                    if (lstMergedPatient.Count > 0)
                    {
                        intTotalPages = lstMergedPatient[0].RecordCount;
                        intTotalPages = (intTotalPages / rows) + 1;
                    }
                    var MergedPatientData = new
                    {

                        total = intTotalPages,
                        page = page,
                        records = rows,
                        rows = (
                          from d in lstMergedPatient
                          select new
                          {
                              MergedPatientID = d.MergedPatientID,
                              cell = new string[] {
                                d.MergedPatientID.ToString(),d.firstName, d.LastName,d.email ,
                               d.WorkPhone,d.sex,d.ActualPatient,d.actualPatientID.ToString()
                              }
                          }).ToArray()
                    };

                    strSerializeData = serializer.Serialize(MergedPatientData);
                    break;
            }





        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }


        finally
        {


        }
        return strSerializeData;
    }

    [System.Web.Services.WebMethod]
    public static bool UnMergedPatient(int MergedPatientID)
    {
        bool isflag = true;
        IEmailTemplateService objService = null;
        try
        {
            objService = new EmailTemplateService();
            isflag=objService.UndoMergedPatient(MergedPatientID);
        }
        catch (System.Exception ex)
        {
            isflag = false;
        }
        return isflag;
    }



    [System.Web.Services.WebMethod]
    public static bool MergedPatientData(int ExistingPatient, int NewAssignPatient, int StaffID)
    {
        bool isflag = true;
        IEmailTemplateService objService = null;
        try
        {
            objService = new EmailTemplateService();
            isflag =objService.MergedPatientData(ExistingPatient, NewAssignPatient, StaffID);
        }
        catch (System.Exception ex)
        {
            isflag = false;
        }
        return isflag;
    }
}

