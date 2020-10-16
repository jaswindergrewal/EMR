using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Emrdev.DataLayer.GeneralClasses;
using Emrdev.ViewModelLayer;
using System.Configuration;

namespace Calendar
{
    public static class Patients
    {
        public static DataTable GetPatients(string patientName)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {

                con.Open();
                SqlCommand cmd1 = new SqlCommand("Patients_FindID", con);
                cmd1.CommandType = CommandType.StoredProcedure;
                cmd1.Parameters.Add(new SqlParameter("@PatientName", patientName));

                DataTable dt = Utils.OpenTable(cmd1);

                return dt;
            }
        }

        public static string[] NameList(string prefixText)
        {
            List<Patient> Names = new List<Patient>();
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {

                con.Open();
                SqlCommand cmd = new SqlCommand("Patients_Get", con);
                cmd.CommandType = CommandType.StoredProcedure;
               // cmd.Parameters.Add(new SqlParameter("@search_criteria", prefixText));
                SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                while (rd.Read())
                {
                    try
                    {
                        if (rd["MiddleInitial"].GetType().Name != "DBNull")
                            Names.Add(new Patient((string)rd["LastName"], (string)rd["FirstName"], (string)rd["MiddleInitial"]));
                        else
                            Names.Add(new Patient((string)rd["LastName"], (string)rd["FirstName"], ""));
                    }
                    catch { }
                }
            }

            List<string> NameArray = new List<string>();

            foreach (Patient pat in Names)
            {
                string SearchString = "";
                //Commented by jaswinder 
                //if (pat.MiddleInitial != "")
                if (!string.IsNullOrEmpty(pat.MiddleInitial))
                    SearchString = pat.LastName + " " + pat.FirstName + " " + pat.MiddleInitial;
                else
                    SearchString = pat.LastName + " " + pat.FirstName;

                if (SearchString.ToUpperInvariant().Contains(prefixText.ToUpperInvariant()))
                {
                    DataTable pat1 = GetPatients(SearchString);
                    foreach (DataRow dr in pat1.Rows)
                    {
                        string sName = SearchString;
                        string sClinic = "";
                        string sBrithday = "None Entered";
                        if ((string)dr["Clinic"] == "South") sClinic = "T"; else sClinic = ((string)dr["Clinic"]).Substring(0, 1);
                        if (dr["Birthday"] != DBNull.Value)
                            sBrithday = ((DateTime)dr["Birthday"]).ToShortDateString();
                        sName += " (" + sClinic + ") - (" + sBrithday + ")";
                        NameArray.Add(sName);
                    }
                }

            }
            NameArray.Sort();
            return NameArray.ToArray();
        }


        public static bool CheckReassign(int PatientID)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                con.Open();
                SqlCommand cmd1 = new SqlCommand("Patients_CheckAllowApptReassign", con);
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new SqlParameter("@Patientid", PatientID));
                return (bool)cmd1.ExecuteScalar();
            }
        }

        public static Patient CheckPatient(string PatientName, int optionalUpdate = 0)
        {
            string[] searcher = PatientName.Split('(');
            if (searcher.Count() != 3)
                return new Patient();
            string patName = searcher[0].Trim();
            string Clinic = searcher[1].Split(')')[0].Trim();
            switch (Clinic)
            {
                case "T":
                    Clinic = "South";
                    break;
                case "S":
                    Clinic = "Seattle";
                    break;
                case "K":
                    Clinic = "Kirkland";
                    break;
                case "L":
                    Clinic = "Lynnwood";
                    break;
                case "C":
                    Clinic = "China-Beijing";
                    break;
            }
            string sBirthday = null;
            try
            {
                sBirthday = DateTime.Parse(PatientName.Split('(')[2].Split(')')[0]).ToShortDateString();
                //Code commentedby jaswinder on 21 Jan 2014 as if dob is 1/1/0001 it throw errors 
                //if (sBirthday.Length < 10)
                //    sBirthday = "0" + sBirthday;
            }
            catch { }
            if (sBirthday == "1/1/0001") sBirthday = null;
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                con.Open();
                //SqlCommand cmd1 = new SqlCommand("Patients_FindID", con);
                SqlCommand cmd1;
                if (optionalUpdate == 0)
                {
                    cmd1 = new SqlCommand("Calendar_Patients_FindID", con);
                }
                else
                {
                    cmd1 = new SqlCommand("Calendar_Patients_FindIDinactive", con);
                }
                cmd1.CommandType = CommandType.StoredProcedure;

                cmd1.Parameters.Add(new SqlParameter("@PatientName", patName));
                cmd1.Parameters.AddWithValue("@Birthday", sBirthday);
                cmd1.Parameters.AddWithValue("@Clinic", Clinic);
                SqlDataReader rd = Utils.OpenReader(cmd1);//cmd1.ExecuteReader();
                Patient retPat = new Patient();
                while (rd.Read())
                {
                    Patient tempPat = new Patient((int)rd["PatientID"]);
                    if (!tempPat.Inactive && optionalUpdate == 0)
                    {
                        return tempPat;
                    }
                    else if (tempPat.Inactive && optionalUpdate == 1)
                    {
                        return tempPat;
                    }
                }
                return retPat;
            }

        }
    }

    [Serializable]
    public class Patient
    {

        public Patient(string lastName, string firstName, string middleInitial)
        {
            FirstName = firstName;
            LastName = lastName;
            MiddleInitial = middleInitial;
            FullName = FirstName + LastName + MiddleInitial;
        }


        public Patient(int PatientID)
        {
            ID = PatientID;
            if (PatientID != 0)
            {
                using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
                {

                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("Patient_Details", con))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add(new SqlParameter("@PatientID", PatientID));

                        SqlDataReader rd = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            rd.Read();
                            FirstName = (string)rd["FirstName"];
                            LastName = (string)rd["LastName"];
                            if (rd["MiddleInitial"] != DBNull.Value)
                                MiddleInitial = (string)rd["MiddleINitial"];
                            BillingStreet = Utils.FixUpString(rd["BillingStreet"]);
                            BillingCity = Utils.FixUpString(rd["BillingCity"]);
                            BillingState = Utils.FixUpString(rd["BillingState"]);
                            BillingZip = Utils.FixUpString(rd["BillingZip"]);
                            ShippingStreet = Utils.FixUpString(rd["ShippingStreet"]);
                            ShippingCity = Utils.FixUpString(rd["ShippingCity"]);
                            ShippingState = Utils.FixUpString(rd["ShippingState"]);
                            ShippingZip = Utils.FixUpString(rd["ShippingZip"]);
                            HomePhone = Utils.FixUpString(rd["HomePhone"]);
                            CellPhone = Utils.FixUpString(rd["CellPhone"]);
                            WorkPhone = Utils.FixUpString(rd["WorkPhone"]);
                            Email = Utils.FixUpString(rd["Email"]);
                            FaxPhone = Utils.FixUpString(rd["FaxPone"]);
                            Clinic = Utils.FixUpString(rd["Clinic"]);
                            LabsMailed = (bool)rd["LabsMailed"];
                            if (rd["Birthday"] != DBNull.Value)
                                Birthday = (DateTime)rd["Birthday"];
                            Inactive = (bool)rd["Inactive"];
                            CancelNSFormSigned = (bool)rd["Cancel_NoShow_frm_signed"];
                            HIPAAFormSigned = (bool)rd["hippa_signed"];
                            Gender = Utils.FixUpString(rd["Sex"]);
                            Concierge = Utils.FixUpString(rd["EmployeeName"]);
                            if (rd["PCP"] != DBNull.Value)
                                PCP = (string)rd["PCP"];
                            NameAlert = (bool)rd["NameAlert"];
                            //EmeregencyContact = (string)rd["EmeregencyContact"];
                            if (rd["LMC_CP"] != DBNull.Value)
                                LMCPhys = (string)rd["LMC_CP"];
                            EmergencyPhone = Utils.FixUpString(rd["EmergencyPhone"]);
                            ContactPreference = Utils.FixUpString(rd["ContactPreference"]);
                            EmergencyRelationship = Utils.FixUpString(rd["EmergencyRelationship"]);
                            Home_detailed_info = (bool)rd["Home_detailed_info"];
                            Home_cb_only = (bool)rd["Home_cb_only"];
                            work_detailed_info = (bool)rd["work_detailed_info"];
                            work_cb_only = (bool)rd["work_cb_only"];
                            cell_detailed_info = (bool)rd["cell_detailed_info"];
                            cell_cb_only = (bool)rd["cell_cb_only"];
                            email_auth_detailed_info = (bool)rd["email_auth_detailed_info"];
                            fax_auth_detailed_info = (bool)rd["fax_auth_detailed_info"];
                            if (rd["AutoshipNote"] != DBNull.Value)
                                AutoshipNote = (string)rd["AutoshipNote"];
                            if (rd["AutoshipAlerts"] != DBNull.Value)
                                AutoshipAlerts = (string)rd["AutoshipAlerts"];
                            if (rd["ClinicID"] != DBNull.Value)
                                ClinicID = (string)rd["ClinicID"].ToString();
                            CallBeforeShip = rd["CallBeforeShip"] != DBNull.Value ? (bool)rd["CallBeforeShip"] : false;
                            ID = (int)rd["PatientId"];
                            ConciergeID = Convert.ToInt32(rd["ConciergeID"]);

                        }
                    }
                }
            }
            else
            {

                BillingStreet = "";
                BillingCity = "";
                BillingState = "";
                BillingZip = "";
                ShippingStreet = "";
                ShippingCity = "";
                ShippingState = "";
                ShippingZip = "";
                HomePhone = "";
                CellPhone = "";
                WorkPhone = "";
                Email = "";
                FaxPhone = "";
                Clinic = "";
                Birthday = DateTime.Now;
                Inactive = true;
                CancelNSFormSigned = true;
                HIPAAFormSigned = true;
                Gender = "";
                Concierge = "";
                PCP = "";
                NameAlert = true;
                EmeregencyContact = "";
                LMCPhys = "";
                EmergencyPhone = "";
                ContactPreference = "";
                EmergencyRelationship = "";
                Home_detailed_info = true; ;
                Home_cb_only = true;
                work_detailed_info = true;
                work_cb_only = true;
                cell_detailed_info = true;
                cell_cb_only = true;
                email_auth_detailed_info = true;
                fax_auth_detailed_info = true;
                ClinicID = "";
                ID = 0;
                ConciergeID=0;

            }
        }

        public Patient()
        {
            // TODO: Complete member initialization
        }

        public void SaveNote(string Note)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Patients_SaveAutoshipNote", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", ID);
                    cmd.Parameters.AddWithValue("@Note", Note);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }

            }

            //
        }
        public void SaveDiscount(string Note)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Patients_SaveAutoshipDiscount", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", ID);
                    cmd.Parameters.AddWithValue("@Note", Note);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            //
        }

        public void SaveHotNotes(string Note)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand("Patients_SaveAutoshipHotNotes", con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@PatientID", ID);
                    cmd.Parameters.AddWithValue("@Note", Note);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }

            //
        }
        public int ID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleInitial { get; set; }
        public string FullName { get; set; }
        public bool AlowReassign { get; set; }
        public string BillingStreet { get; set; }
        public string BillingCity { get; set; }
        public string BillingState { get; set; }
        public string BillingZip { get; set; }
        public string ShippingStreet { get; set; }
        public string ShippingCity { get; set; }
        public string ShippingState { get; set; }
        public string ShippingZip { get; set; }
        public string HomePhone { get; set; }
        public string CellPhone { get; set; }
        public string WorkPhone { get; set; }
        public string Email { get; set; }
        public string FaxPhone { get; set; }
        public string Clinic { get; set; }
        public DateTime Birthday { get; set; }
        public bool Inactive { get; set; }
        public bool CancelNSFormSigned { get; set; }
        public bool HIPAAFormSigned { get; set; }
        public string Gender { get; set; }
        public string Concierge { get; set; }
        public string PCP { get; set; }
        public bool NameAlert { get; set; }
        public string EmeregencyContact { get; set; }
        public string LMCPhys { get; set; }
        public string EmergencyPhone { get; set; }
        public string ContactPreference { get; set; }
        public string EmergencyRelationship { get; set; }
        public bool Home_detailed_info { get; set; }
        public bool Home_cb_only { get; set; }
        public bool work_detailed_info { get; set; }
        public bool work_cb_only { get; set; }
        public bool cell_detailed_info { get; set; }
        public bool cell_cb_only { get; set; }
        public bool email_auth_detailed_info { get; set; }
        public bool fax_auth_detailed_info { get; set; }
        public string AutoshipNote { get; set; }
        public string AutoshipAlerts { get; set; }
        public string ClinicID { get; set; }
        public bool LabsMailed { get; set; }

        public bool CallBeforeShip { get; set; }
        public int ConciergeID { get; set; }
    }
}
