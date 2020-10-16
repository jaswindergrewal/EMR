using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace Calendar
{
	public static class AppointmentTypes
	{

		private static List<AppointmentType> ApptTypes { get; set; }

		public static void dbGetApptTypes(bool all)
		{
			ApptTypes = new List<AppointmentType>();
			using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
			{
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_Get"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();
                    using (SqlDataReader rd = Utils.OpenReader(cmd))//cmd.ExecuteReader();
                    {
                        if (all)
                            ApptTypes.Add(new AppointmentType(0, "All", "", true, "", false, "", "", "", "", "", false,"","","","",0));
                        while (rd.Read())
                        {

                            ApptTypes.Add(new AppointmentType((int)rd["id"], (string)rd["TypeName"], (string)rd["Color"], (bool)rd["Active"], (string)rd["Category"],
                                (bool)rd["ConfirmationEmail"], (string)rd["ConfirmationText"], (string)rd["Attachment"], (string)rd["Subject"],
                                (string)rd["EmailFromAddress"], (string)rd["EmailfromName"], (bool)rd["OVU"],(string)rd["WufooFormKey"],(string)rd["MailChimpCampaignId"],(string)rd["MailChimpCampaignName"],(string)rd["StatusName"],(int)rd["ResultStatusId"]));
                        }
                    }
                }
			}
		}

		public static List<AppointmentType> getApptTypeList()
		{
			dbGetApptTypes(true);
			return ApptTypes;
		}
		public static DataTable getApptTypeListOnly()
		{
			return getApptTypeListOnly(null);
		}

        public static DataTable getApptTypeListOnly(string ProviderID, int aptID)
        {
            ApptTypes = new List<AppointmentType>();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_GetByID"; 
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", aptID);
                    conn.Open();
                    using (SqlDataReader rd = Utils.OpenReader(cmd))//cmd.ExecuteReader();
                    {
                       
                        while (rd.Read())
                        {

                            ApptTypes.Add(new AppointmentType((int)rd["id"], (string)rd["TypeName"], (string)rd["Color"], (bool)rd["Active"], (string)rd["Category"],
                                (bool)rd["ConfirmationEmail"], (string)rd["ConfirmationText"], (string)rd["Attachment"], (string)rd["Subject"],
                                (string)rd["EmailFromAddress"], (string)rd["EmailfromName"], (bool)rd["OVU"], (string)rd["WufooFormKey"],(string)rd["MailChimpCampaignId"],(string)rd["MailChimpCampaignName"],(string)rd["StatusName"],(int)rd["ResultStatusId"]));
                        }
                    }
                }
            }

            List<AppointmentType> ret = new List<AppointmentType>();
            if (ProviderID != null)
            {
                Provider prov = Provider.GetProvider(ProviderID)[0];
                for (int x = 0; x < ApptTypes.Count; x++)
                {
                    AppointmentType t = ApptTypes[x];
                    if (t.Category.Contains(prov.Category) || string.IsNullOrEmpty(t.Category) || t.Category.Contains("Locked"))
                        ret.Add(t);
                }
            }
            else
            {
                ret = ApptTypes;
            }
            DataTable dt = new DataTable();
            dt.Columns.Add("id", System.Type.GetType("System.Int32"));
            dt.Columns.Add("TypeName");
            dt.Columns.Add("Color");
            dt.Columns.Add("Active", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("Category");
            dt.Columns.Add("ConfirmationEmail", System.Type.GetType("System.Boolean"));
            dt.Columns.Add("ConfirmationText");
            dt.Columns.Add("Attachment");
            dt.Columns.Add("Subject");
            dt.Columns.Add("EmailFromAddress");
            dt.Columns.Add("EmailFromName");
            dt.Columns.Add("OVU");
            dt.Columns.Add("WufooFormKey");
            foreach (AppointmentType at in ret)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = at.id;
                dr["TypeName"] = at.TypeName;
                dr["Color"] = at.Color;
                dr["Active"] = at.Active;
                dr["Category"] = at.Category;
                dr["ConfirmationEmail"] = at.ConfirmationEmail;
                dr["ConfirmationText"] = at.ConfirmationText;
                dr["Attachment"] = at.Attachment;
                dr["Subject"] = at.Subject;
                dr["EmailFromAddress"] = at.EmailFromAddress;
                dr["EmailFromName"] = at.EmailFromName;
                dr["OVU"] = at.OVU;
                dr["WufooFormKey"] = at.WufooFormKey;
                dt.Rows.Add(dr);
            }
            return dt;
        
        }

		public static DataTable getApptTypeListOnly(string ProviderID)
		{
			dbGetApptTypes(false);
			List<AppointmentType> ret = new List<AppointmentType>();
			if (ProviderID != null)
			{
				Provider prov = Provider.GetProvider(ProviderID)[0];
				for (int x = 0; x < ApptTypes.Count; x++)
				{
					AppointmentType t = ApptTypes[x];
					if (t.Category.Contains(prov.Category) || string.IsNullOrEmpty(t.Category) ||t.Category.Contains("Locked"))
						ret.Add(t);
				}
			}
			else
			{
				ret = ApptTypes;
			}
			DataTable dt = new DataTable();
			dt.Columns.Add("id", System.Type.GetType("System.Int32"));
			dt.Columns.Add("TypeName");
			dt.Columns.Add("Color");
			dt.Columns.Add("Active", System.Type.GetType("System.Boolean"));
			dt.Columns.Add("Category");
			dt.Columns.Add("ConfirmationEmail", System.Type.GetType("System.Boolean"));
			dt.Columns.Add("ConfirmationText");
			dt.Columns.Add("Attachment");
			dt.Columns.Add("Subject");
			dt.Columns.Add("EmailFromAddress");
			dt.Columns.Add("EmailFromName");
			dt.Columns.Add("OVU");
            dt.Columns.Add("WufooFormKey");
            dt.Columns.Add("MailChimpCampaignId");
            dt.Columns.Add("MailChimpCampaignName");
            dt.Columns.Add("StatusName");
            dt.Columns.Add("ResultStatusId", System.Type.GetType("System.Int32"));

            foreach (AppointmentType at in ret)
			{
				DataRow dr = dt.NewRow();
				dr["id"] = at.id;
				dr["TypeName"] = at.TypeName;
				dr["Color"] = at.Color;
				dr["Active"] = at.Active;
				dr["Category"] = at.Category;
				dr["ConfirmationEmail"] = at.ConfirmationEmail;
				dr["ConfirmationText"] = at.ConfirmationText;
				dr["Attachment"] = at.Attachment;
				dr["Subject"] = at.Subject;
				dr["EmailFromAddress"] = at.EmailFromAddress;
				dr["EmailFromName"] = at.EmailFromName;
				dr["OVU"] = at.OVU;
                dr["WufooFormKey"] = at.WufooFormKey;
                dr["MailChimpCampaignId"] = at.MailChimpCampaignId;
                dr["MailChimpCampaignName"] = at.MailChimpCampaignName;
                dr["StatusName"] = at.StatusName;
                dr["ResultStatusId"] = at.ResultStatusId;

                dt.Rows.Add(dr);
			}
			return dt;
		}

		public static void AppointmentTypeUpdate(AppointmentType type)
		{
			//string id, string TypeName, string Color, bool Active, string Category
			if (type.Category == null) type.Category = "";
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_Update";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", type.id);
                    if (type.TypeName != null)
                        cmd.Parameters.AddWithValue("@TypeName", type.TypeName);
                    else
                        cmd.Parameters.AddWithValue("@TypeName", "");
                    cmd.Parameters.AddWithValue("@Color", type.Color);
                    cmd.Parameters.AddWithValue("@Active", type.Active);
                    cmd.Parameters.AddWithValue("@Category", type.Category);
                    cmd.Parameters.AddWithValue("@ConfirmationEmail", type.ConfirmationEmail);
                    cmd.Parameters.AddWithValue("@ConfirmationText", type.ConfirmationText);
                    cmd.Parameters.AddWithValue("@Attachment", type.Attachment);
                    cmd.Parameters.AddWithValue("@Subject", type.Subject);
                    cmd.Parameters.AddWithValue("@EmailFromAddress", type.EmailFromAddress);
                    cmd.Parameters.AddWithValue("@EmailFromName", type.EmailFromName);
                    cmd.Parameters.AddWithValue("@OVU", type.OVU);
                    cmd.Parameters.AddWithValue("@WufooFormKey", type.WufooFormKey);
                    cmd.Parameters.AddWithValue("@MailChimpCampaignId", type.MailChimpCampaignId);
                    cmd.Parameters.AddWithValue("@MailChimpCampaignName", type.MailChimpCampaignName);
                    cmd.Parameters.AddWithValue("@ResultStatusId", type.ResultStatusId);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
		}

        //Remove parameter 'string id' from the function
        public static void AppointmentTypeInsert(string TypeName, string Color)
		{
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_Insert"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(new SqlParameter("@TypeName", TypeName));
                    cmd.Parameters.Add(new SqlParameter("@Color", Color));
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
		}

		public static string GetAptTypeName(int id)
		{
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_GetByID";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    string retVal = (string)cmd.ExecuteScalar();
                    return retVal;
                }
            }
			
		}

		public static AppointmentType GetApptType(int id)
		{
			AppointmentType retVal = new AppointmentType();
            using (SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["db"].ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "AppointmentType_GetByID"; ;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id", id);
                    conn.Open();
                    SqlDataReader reader = Utils.OpenReader(cmd);//cmd.ExecuteReader();
                    if (reader.HasRows)
                    {
                        reader.Read();
                        retVal.Active = (bool)reader["Active"];
                        retVal.Attachment = (string)reader["Attachment"];
                        retVal.Category = (string)reader["Category"];
                        retVal.Color = (string)reader["Color"];
                        retVal.ConfirmationEmail = (bool)reader["ConfirmationEmail"];
                        retVal.ConfirmationText = (string)reader["ConfirmationText"];
                        retVal.id = id;
                        retVal.EmailFromName = (string)reader["EmailFromName"];
                        retVal.EmailFromAddress = (string)reader["EmailFromAddress"];
                        retVal.Subject = (string)reader["Subject"];
                        retVal.TypeName = (string)reader["TypeName"];
                        retVal.WufooFormKey = (string)reader["WufooFormKey"];
                        retVal.MailChimpCampaignId = (string)reader["MailChimpCampaignId"];
                        retVal.MailChimpCampaignName = (string)reader["MailChimpCampaignName"];
                        retVal.StatusName = (string)reader["StatusName"];
                        retVal.ResultStatusId = (int)reader["ResultStatusId"];
                    }
                }
            }
			return retVal;
		}
	}
	public class AppointmentType
	{
		public AppointmentType() { }
		public AppointmentType(int ID, string typeName, string color, bool active, string category, bool confirmationEmail, string confirmationText, string attachment, string subject
			, string emailFromAddress, string emailFromName, bool OVUin,string wufooFormKey, string mailChimpCampaignId,string mailChimpCampaignName,string statusName,int resultStatusId)
		{
			id = ID;
			TypeName = typeName;
			Color = color;
			Active = active;
			Category = category;
			ConfirmationEmail = confirmationEmail;
			ConfirmationText = confirmationText;
			Attachment = attachment;
			Subject = subject;
			EmailFromAddress = emailFromAddress;
			EmailFromName = emailFromName;
			OVU = OVUin;
            WufooFormKey = wufooFormKey;
            MailChimpCampaignId = mailChimpCampaignId;
            MailChimpCampaignName =mailChimpCampaignName;
            StatusName = statusName;
            ResultStatusId = resultStatusId;
		}
		public int id { get; set; }
		public string TypeName { get; set; }
		public string Color { get; set; }
		public bool Active { get; set; }
		public string Category { get; set; }
		public bool ConfirmationEmail { get; set; }
		public string ConfirmationText { get; set; }
		public string Attachment { get; set; }
		public string Subject { get; set; }
		public string EmailFromAddress { get; set; }
		public string EmailFromName { get; set; }
		public bool OVU { get; set; }
        public string WufooFormKey { get; set; }
        public string MailChimpCampaignId { get; set; }
        public string MailChimpCampaignName { get; set; }
        public string StatusName { get; set; }

        public int ResultStatusId { get; set; }
    }
}
