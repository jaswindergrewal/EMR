using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
public partial class LandingCalendar : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

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
                Response.Clear();
                string strTicketData = string.Empty;
                strTicketData = BindTiketGrid((int)Session["StaffID"], Request.QueryString["gridFill"], _search, nd, rows, page, sidx, sord);
                Response.ContentType = "application/json";
                Response.Write(strTicketData);
                Response.End();

                //Response.Flush();
            }


        }

    }



    //Method return json for the ticket Grids
    //1to 6 represent the tab order
    public string BindTiketGrid(int staffID, string GrdTab, string _search, string nd, int rows, int page, string sidx, string sord)
    {
        LandingPageService objService = null;
        List<MyTicketsViewModel> lstTickets = null;
        string strTicketsSerializeData = "";
        int intTotalPages = 0;
        try
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            objService = new LandingPageService();

            switch (GrdTab)
            {
                case "1":
                    lstTickets = objService.GetMyTicketsCAL(staffID, page, rows, sord, sidx);
                    break;
                case "2":
                    lstTickets = objService.GetMyGroupTickets(staffID, 6, page, rows, sord, sidx);
                    break;
                case "3":
                    lstTickets = objService.GetMyGroupTickets(staffID, 5, page, rows, sord, sidx);
                    break;
                case "4":
                    lstTickets = objService.GetMyGroupTickets(staffID, 4, page, rows, sord, sidx);
                    break;

                case "5":
                    lstTickets = objService.GetMyActiveCAL(staffID, page, rows, sord, sidx);
                    break;

                case "6":
                    lstTickets = objService.GetCreatedClosedCAL(staffID, page, rows, sord, sidx);
                    break;
            }



            if (lstTickets.Count > 0)
            {
                intTotalPages = Convert.ToInt16(lstTickets[0].RecordCount);
                intTotalPages = (intTotalPages / rows) + 1;
            }
            var jsonData = new
            {

                total = intTotalPages,
                page = page,
                records = rows,
                rows = (
                  from d in lstTickets
                  select new
                  {
                      FollowUp_ID = d.FollowUp_ID,
                      cell = new string[] {
                        d.FollowUp_ID.ToString(),d.InProgress, d.Name.ToString(),d.Subject ,
                       d.CreateDate.ToString(),d.DaysOld.ToString(),d.Category,d.Priority,d.Assigned,d.PatientID.ToString()
                      }
                  }).ToArray()
            };

            strTicketsSerializeData = serializer.Serialize(jsonData);

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }


        finally
        {
            lstTickets = null;
            objService = null;

        }
        return strTicketsSerializeData;
    }

}