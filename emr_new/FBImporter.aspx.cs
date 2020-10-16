using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class FBImporter : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected void FbImporter_Click(object sender, EventArgs e)
    {
        int FbImporterId = 200;
        //Get last Fb Id
        //using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn.Open();

        //    DataTable FbImporterTable = new DataTable();
        //    SqlCommand cmd = new SqlCommand("select Id from FbImporter", conn);
        //    cmd.CommandType = CommandType.Text;
        //    SqlDataAdapter FbImporteradapter = new SqlDataAdapter();
        //    FbImporteradapter.SelectCommand = cmd;
        //    FbImporteradapter.Fill(FbImporterTable);
        //    cmd.Connection.Close();
        //    IEnumerable<DataRow> FbImporterquery = from data in FbImporterTable.AsEnumerable()

        //                                           select data;
        //    foreach (var Item in FbImporterquery)
        //    {
        //        FbImporterId = Item.Field<int>("Id");
        //    }
        //}
        //End

        //Get Facebook data
        DataTable dt = new DataTable();
        string myConnectionString = "Database=FBLeadDB;Data Source=fbleads.c1oww7bgljz9.us-west-2.rds.amazonaws.com;User Id=Longevity;Password=Kf4f9by%JO%2";
        MySqlConnection myConnection = new MySqlConnection(myConnectionString);
        myConnection.Open();
        string myInsertQuery = "Select * from Leads where ID>" + FbImporterId  ;
        MySqlCommand myCommand = new MySqlCommand(myInsertQuery);
        myCommand.Connection = myConnection;
        MySqlDataAdapter adp = new MySqlDataAdapter(myCommand);
        adp.Fill(dt);


        myCommand.Connection.Close();
        IEnumerable<DataRow> query = from data in dt.AsEnumerable()

                                     select data;
        IAddProspectService objService = new AddProspectService();
        List<CRM_Events_ViewModel> events = objService.GetAllEvents();
        List<CRM_Events_ViewModel> selectedEvent;
        int Id = 0;
        foreach (var Item in query)
        {
            Id = Item.Field<int>("Id");
            string FirstName = Item.Field<string>("FirstName");
            string LastName = Item.Field<string>("LastName");
            string Email = Item.Field<string>("E-mail");
            string EventName = Item.Field<string>("EventName");
            string city= Item.Field<string>("city");
            string state = Item.Field<string>("state");
            string zipCode = Item.Field<string>("zip_code");
            string address= Item.Field<string>("street_address");
            string Phone = Item.Field<string>("phone_number");
            //TO Fill DropDown With Events
            selectedEvent = events.Where(o => o.EventName.ToLower() == EventName.ToLower()).ToList();
            int eventId;
            if (selectedEvent.Count == 0)
            {
                ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "msg("+EventName+")", true);
                objService = new AddProspectService();
                events = objService.GetAllEvents();
                selectedEvent = events.Where(o => o.EventName.ToLower() == EventName.ToLower()).ToList();
            }
            if (selectedEvent.Count > 0)
            {
                eventId = selectedEvent[0].EventID;
                //check duplicate email
                int isExist = 0;

                IManageService _objManageService = null;


                _objManageService = new ManageService();
                isExist = _objManageService.CheckDuplicateFbImporter(EventName, Email);
                //if(isExist==true)
                //{
                //    _objManageService = new ManageService();
                //}
                if (isExist >= 0)
                {


                    _objManageService = new ManageService(); ;
                    //insert Data in Crm_Prospect table
                    _objManageService.InsertUpdateProspect(isExist,
                        address,
                        "",
                        city,
                        "",
                        Email,
                        FirstName,
                        true,
                        LastName,
                        Phone,
                        "16",
                        string.Empty,
                        state,
                        4,
                        zipCode,
                        (int)Session["StaffID"],
                        eventId
                        );
                }


            }
            



        }

        //using (SqlConnection conn1 = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        //{
        //    conn1.Open();

        //    SqlCommand cmd1 = new SqlCommand("Update FbImporter set Id =@Id,DateAdded=@DateAdded", conn1);
        //    //cmd1.CommandType = CommandType.Text;
            
        //    cmd1.Parameters.AddWithValue("Id", Id);
        //    cmd1.Parameters.AddWithValue("@DateAdded", DateTime.Now);
        //    cmd1.ExecuteNonQuery();
        //    cmd1.Connection.Close();

        //}
    }
}