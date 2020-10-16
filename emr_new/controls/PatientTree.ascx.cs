using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Controls_PatientTree : System.Web.UI.UserControl
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

    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public int Populate()
    {
        return Populate(false);
    }

    public int Populate(bool exception)
    {
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
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
            if (reader["Age"] != DBNull.Value) lblAge.Text = ((int)reader["Age"]).ToString();
            lblClinic.Text = (string)reader["Clinic"];
            lblFirstName.Text = (string)reader["FirstName"];
            lblLastName.Text = (string)reader["LastName"];
            if (reader["MiddleInitial"] != DBNull.Value) lblMI.Text = (string)reader["MiddleInitial"];
            if (reader["NickName"] != DBNull.Value) lblNickName.Text = (string)reader["NickName"];
            if (reader["RenewalMonth"] != DBNull.Value) lblRenewalMonth.Text = (string)reader["RenewalMonth"];
            lblSex.Text = (string)reader["Sex"];
            if (!(bool)reader["Inactive"])
            {
                lblStatus.Text = "Active";
                lblInactive.Visible = false;
                CollapsiblePanelExtender1.Collapsed = true;
            }
            else
            {
                lblStatus.Text = "Inactive";
                lblInactive.Visible = true;
                CollapsiblePanelExtender1.Collapsed = false;
            }
            if (!IsPostBack || exception)
            {
                txtDiscount.Text = (string)reader["AutoShipAlerts"];

                txtNote.Text = (string)reader["AutoshipNote"];
                if ((string)reader["ShippingStreet"] != "")
                {
                    StreetAddress = (string)reader["ShippingStreet"];
                    City = (string)reader["ShippingCity"];
                    Zip = (string)reader["ShippingZip"];
                    State = (string)reader["ShippingState"];
                }
                else
                {
                    StreetAddress = (string)reader["BillingStreet"];
                    City = (string)reader["BillingCity"];
                    Zip = (string)reader["BillingZip"];
                    State = (string)reader["BillingState"];
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
                    string ordNote = (from o in ctx.Orders where o.OrderID == (int)reader["OrderID"] select o.Note).First();
                    txtNote.Text = "Order Note:\r\n" + ordNote + "\r\n\r\nNote:\r\n" + txtNote.Text;
                    Button btnQuit = (Button)Utilities.FindControlRecursive(Page.Master, "btnQuit");
                    btnQuit.Enabled = false;
                    break;
                }
                else
                    lblOrderPending.Visible = false;
            }
            if (txtDiscount.Text != "" || lblOrderPending.Visible) CollapsiblePanelExtender1.Collapsed = false;
            if (conn.State == ConnectionState.Open) conn.Close();
        }
        return PatientID;
    }

    protected void btnEditNote_Click(object sender, EventArgs e)
    {
        Session["OldNote"] = txtNote.Text;
        txtNote.Text = "";
        txtNote.Enabled = true;
        btnSaveNote.Enabled = true;
        Session["EditNote"] = true;

    }


    protected void btnSaveNote_Click(object sender, EventArgs e)
    {
        if (Session["EditNote"] != null)
        {
            Calendar.Patient pat = new Calendar.Patient(_PatientID);
            pat.SaveNote(txtNote.Text);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();

                string msg = "AS Note Added/Updated: <br/>";
                msg += "Previous note: " + Session["OldNote"] + "<br/>";
                msg += "New note: " + txtNote.Text + "<br/>";
                msg += "Added/Changed by: " + (string)Session["MM_Username"];
                Session["OldNote"] = null;
                SqlCommand logItem = new SqlCommand("contact_tbl_AS_Insert", conn);
                logItem.CommandType = CommandType.StoredProcedure;
                logItem.Parameters.AddWithValue("@AptType", 58);
                logItem.Parameters.AddWithValue("@PatientID", _PatientID);
                logItem.Parameters.AddWithValue("@MessageBody", msg);
                logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
                logItem.ExecuteNonQuery();
            }
        }
        btnSaveNote.Enabled = false;
    }
    protected void btnEditDiscount_Click(object sender, EventArgs e)
    {
        Session["OldAlert"] = txtDiscount.Text;
        txtDiscount.Enabled = true;
        Session["EditAlert"] = true;
        btnSaveDiscount.Enabled = true;
    }


    protected void btnSaveDiscount_Click(object sender, EventArgs e)
    {
        if (Session["EditAlert"] != null)
        {
            Calendar.Patient pat = new Calendar.Patient(_PatientID);
            pat.SaveDiscount(txtDiscount.Text);

            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                conn.Open();
                string msg = "AS alert Added/Updated: <br/>";
                msg += "Previous alert: " + Session["OldAlert"] + "<br/>";
                msg += "New alert: " + txtDiscount.Text + "<br/>";
                msg += "Added/Changed by: " + (string)Session["MM_Username"];

                SqlCommand logItem = new SqlCommand("contact_tbl_AS_Insert", conn);
                logItem.CommandType = CommandType.StoredProcedure;
                logItem.Parameters.AddWithValue("@AptType", 58);
                logItem.Parameters.AddWithValue("@PatientID", _PatientID);
                logItem.Parameters.AddWithValue("@MessageBody", msg);
                logItem.Parameters.AddWithValue("@EmployeeID", Session["UserID"]);
                logItem.ExecuteNonQuery();
            }

        }
        btnSaveDiscount.Enabled = false;
    }




}