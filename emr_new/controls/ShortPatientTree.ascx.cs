using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Controls_ShortPatientTree : System.Web.UI.UserControl
{
    int _PatientID = 0;
    public int PatientID
    {
        get
        {
            return _PatientID;
        }
        set
        {
            _PatientID = value;
            Populate();
        }
    }

    public string StreetAddress { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Zip { get; set; }
        public Nullable<bool> CallBeforeShip { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int Populate()
    {
        return Populate(false);
    }

    public int Populate(bool exception)
    {
        //EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
        {
            conn.Open();
            SqlCommand cmd = new SqlCommand("Patient_Details", conn);
            cmd.Parameters.AddWithValue("@PatientID", PatientID);
            cmd.CommandType = CommandType.StoredProcedure;
            bool connGood = false;
            int counter = 0;
            SqlDataReader reader = null;
            while (counter < 10 && !connGood)
            {
                try
                {
                    reader = cmd.ExecuteReader();
                    connGood = true;
                }
                catch
                {
                    try
                    {
                        reader.Close();
                    }
                    catch { }
                    conn.Close();
                    conn.Open();
                    counter++;
                }
            }
            reader.Read();

            if (!IsPostBack || exception)
            {
                if (reader.HasRows)
                {
                    //edContent.Content = (string)reader["AutoshipDiscounts"];
                    edContent.Content = (string)reader["AutoshipNote"];
                    edHotNotes.Content = (string)reader["HotNotes"];
                    //txtNote.Text = (string)reader["AutoshipNote"];
                    if (reader["ShippingStreet"] != null || (string)reader["ShippingStreet"] != "")
                    {
                        StreetAddress = reader["ShippingStreet"].ToString();
                        City = reader["ShippingCity"].ToString();
                        Zip = reader["ShippingZip"].ToString();
                        State = reader["ShippingState"].ToString();
                    }
                    else
                    {
                        StreetAddress = reader["BillingStreet"].ToString();
                        City = reader["BillingCity"].ToString();
                        Zip = reader["BillingZip"].ToString();
                        State = reader["BillingState"].ToString();
                    }
                    CallBeforeShip = reader["CallBeforeShip"] != DBNull.Value ? (bool)reader["CallBeforeShip"] : false;
                }
            }

            reader.Close();
            cmd = new SqlCommand();
            cmd.CommandText = "Orders_GetOpen";
            cmd.Parameters.Clear();
            cmd.Connection = conn;
            reader = cmd.ExecuteReader();
            while (reader.Read())
            {
                if ((int)reader["PatientID"] == _PatientID)
                {
                    lblOrderPending.Visible = true;
                    lblOrderPending.Visible = true;
                    //string ordNote = (from o in ctx.Orders where o.OrderID == (int)reader["OrderID"] select o.Note).First();
                    //edContent.Content = "Order Note:\r\n" + ordNote + "\r\n\r\nNote:\r\n" + edContent.Content; 
                    edContent.Content = edContent.Content;
                    //Button btnQuit = (Button)Utilities.FindControlRecursive(Page.Master, "btnQuit");
                    //btnQuit.Enabled = false;
                    break;
                }
                else
                    lblOrderPending.Visible = false;
            }
            if (edContent.Content != "" || lblOrderPending.Visible) CollapsiblePanelExtender1.Collapsed = false;
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return PatientID;
    }

    //protected void btnEditNote_Click(object sender, EventArgs e)
    //{
    //    Session["OldNote"] = txtNote.Text;
    //    //txtNote.Text = "";
    //    txtNote.Enabled = true;
    //    btnSavediscouNote.Enabled = true;
    //    Session["EditNote"] = true;

    //}


    //protected void btnSaveNote_Click(object sender, EventArgs e)
    //{
    //    if (Session["EditNote"] != null)
    //    {
    //        Calendar.Patient pat = new Calendar.Patient(_PatientID);
    //        pat.SaveNote(txtNote.Text);

    //        using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
    //        {
    //            conn.Open();

    //            string AutoShipStr = txtDiscount.Text + ' ' + txtNote.Text;
    //            string msg = "AS Note Added/Updated: <br/>";
    //            msg += "Previous note: " + Session["OldNote"] + "<br/>";
    //            msg += "New note: " + txtNote.Text + "<br/>";
    //            msg += "Added/Changed by: " + (string)Session["MM_Username"];
    //            Session["OldNote"] = null;
    //            SqlCommand logItem = new SqlCommand("contact_tbl_AS_Insert", conn);
    //            logItem.CommandType = CommandType.StoredProcedure;
    //            logItem.Parameters.AddWithValue("@AptType", 58);
    //            logItem.Parameters.AddWithValue("@PatientID", _PatientID);
    //            logItem.Parameters.AddWithValue("@MessageBody", msg);
    //            logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
    //            logItem.Parameters.AddWithValue("@AutoShipStr", AutoShipStr);
    //            logItem.ExecuteNonQuery();
    //            txtNote.Text = "";
    //            txtDiscount.Text = AutoShipStr; 
    //        }
    //    }
    //    btnSaveNote.Enabled = false;
    //}
    protected void btnEditDiscount_Click(object sender, EventArgs e)
    {
        //Session["OldAlert"] = txtDiscount.Text;
        //edContent.enabled = true;
        Session["EditAlert"] = true;
        btnSaveDiscount.Enabled = true;
        edContent.Enabled = true;
    }


    protected void btnSaveDiscount_Click(object sender, EventArgs e)
    {
        if (Session["EditAlert"] != null)
        {
            Calendar.Patient pat = new Calendar.Patient(_PatientID);
            //pat.SaveDiscount(txtDiscount.Text);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                string AutoShipStr = edContent.Content;
                //string msg = "AS alert Added/Updated: <br/>";
                //msg += "Previous alert: " + Session["OldAlert"] + "<br/>";
                //msg += "New alert: " + edContent.Content + "<br/>";
                //msg += "Added/Changed by: " + (string)Session["MM_Username"];

                SqlCommand logItem = new SqlCommand("contact_tbl_AS_Insert", conn);
                logItem.CommandType = CommandType.StoredProcedure;
                logItem.Parameters.AddWithValue("@AptType", 58);
                logItem.Parameters.AddWithValue("@PatientID", _PatientID);
                logItem.Parameters.AddWithValue("@MessageBody", AutoShipStr);
                logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
                logItem.Parameters.AddWithValue("@AutoShipStr", AutoShipStr);
                logItem.ExecuteNonQuery();
            }

        }
        btnSaveDiscount.Enabled = false;
        edContent.Enabled = false;
    }


    protected void btnEditHoteNote_Click(object sender, EventArgs e)
    {
        Session["OldHotNotes"] = edHotNotes.Content;
        Session["EditHotNotes"] = true;
        btnSaveHotNotes.Enabled = true;
        edHotNotes.Enabled = true;
    }

    protected void btnSaveHotNotes_Click(object sender, EventArgs e)
    {
        if (Session["EditHotNotes"] != null)
        {
            Calendar.Patient pat = new Calendar.Patient(_PatientID);
            pat.SaveHotNotes(edHotNotes.Content);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                string msg = "AS Hot Notes Added/Updated: <br/>";
                msg += "Previous alert: " + Session["OldHotNotes"] + "<br/>";
                msg += "New alert: " + edHotNotes.Content + "<br/>";
                msg += "Added/Changed by: " + (string)Session["MM_Username"];

                SqlCommand logItem = new SqlCommand("ssp_contact_tbl_AS_Insert", conn);
                logItem.CommandType = CommandType.StoredProcedure;
                logItem.Parameters.AddWithValue("@AptType", 58);
                logItem.Parameters.AddWithValue("@PatientID", _PatientID);
                logItem.Parameters.AddWithValue("@MessageBody", msg);
                logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
                logItem.ExecuteNonQuery();
            }

        }
        btnSaveHotNotes.Enabled = false;
    }
}