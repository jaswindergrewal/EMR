using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using LINQtoCSV;

public partial class admin_SharepointPatientTracking : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{
		if (FileUpload1.HasFile)
		{
			FileUpload1.SaveAs(Server.MapPath("./") + "tempImport.csv");
			CsvFileDescription inputFileDescription = new CsvFileDescription
			{
				SeparatorChar = ',',
				FirstLineHasColumnNames = true
			};

			CsvContext cc = new CsvContext();
			List<WebCSV> contacts = new List<WebCSV>();
			try
			{
				contacts = cc.Read<WebCSV>(Server.MapPath("./") + "tempImport.csv", inputFileDescription).ToList();
			}
			catch
			{
				Response.Write("Improper file format");
				return;
			}
			List<SharePointOut> outputList = new List<SharePointOut>();
			foreach (WebCSV w in contacts)
			{
				SharePointOut theOut = new SharePointOut
				{
					Notes = w.Comments,
					ContactMethod = "Website Online Reg",
					Status = "Sem - Registered",
					SeminarSource = w.Event_Location,
					MktCallDate = DateTime.Parse(w.time_stamp).ToShortDateString() + " " + DateTime.Parse(w.time_stamp).ToShortTimeString(),
					MktSource = GetMktSource(w),
					LastName = w.Last_Name,
					FirstName = w.First_Name,
					Phone = FormatPhone(w.Phone_Pre1),
					Address = w.Address_1,
					City = w.City,
					ST = w.State == null ? "" : w.State.ToUpper(),
					Zip = w.Zip,
					Email = w.email,
				};
				outputList.Add(theOut);
				if (w.Guest_1_First_Name != null && w.Guest_1_First_Name != "")
				{

					SharePointOut Guest1 = new SharePointOut
					{
						Notes = w.Comments,
						ContactMethod = "Website Online Reg",
						Status = "Sem - Registered",
						SeminarSource = w.Event_Location,
						MktCallDate = DateTime.Parse(w.time_stamp).ToShortDateString() + " " + DateTime.Parse(w.time_stamp).ToShortTimeString(),
						MktSource = GetMktSource(w),
						LastName = w.Guest_1_Last_Name,
						FirstName = w.Guest_1_First_Name,
						Phone = FormatPhone(w.Phone_Pre1),
						Address = w.Address_1,
						City = w.City,
						ST = w.State == null ? "" : w.State.ToUpper(),
						Zip = w.Zip,
						Email = w.email,
					};
					outputList.Add(Guest1);
				}
				if (w.Guest_2_First_Name != null && w.Guest_2_First_Name != "")
				{

					SharePointOut Guest2 = new SharePointOut
					{
						Notes = w.Comments,
						ContactMethod = "Website Online Reg",
						Status = "Sem - Registered",
						SeminarSource = w.Event_Location,
						MktCallDate = DateTime.Parse(w.time_stamp).ToShortDateString() + " " + DateTime.Parse(w.time_stamp).ToShortTimeString(),
						MktSource = GetMktSource(w),
						LastName = w.Guest_2_Last_Name,
						FirstName = w.Guest_2_First_Name,
						Phone = FormatPhone(w.Phone_Pre1),
						Address = w.Address_1,
						City = w.City,
						ST = w.State == null ? "" : w.State.ToUpper(),
						Zip = w.Zip,
						Email = w.email,
					};
					outputList.Add(Guest2);
				}

			}
			CsvFileDescription outputFileDescription = new CsvFileDescription
			{
				SeparatorChar = ',', // tab delimited
				FirstLineHasColumnNames = true, // no column names in first record
			};
			
			cc.Write(outputList, Server.MapPath("./") +  "outputlist.csv", outputFileDescription);
			Response.Redirect("outputlist.csv");
		}
	}
	private string FormatPhone(string rawPhone)
	{
		PhoneNumber phone;
		try
		{
			phone = new PhoneNumber(rawPhone, true);
		}
		catch
		{
			return "";
		}
		return phone.ToString().Replace("(","").Replace(")","");
	}

	private string GetMktSource(WebCSV w)
	{
		string ret = "";
		if (w.KTTH_Leading_Edge_Medicine != null && w.KTTH_Leading_Edge_Medicine != "")
			ret += "KTTH LEM;";
		if (w.KVI_Leading_Edge_Medicine != null && w.KVI_Leading_Edge_Medicine != "")
			ret += "KVI LEM;";
		if (w.Facebook != null && w.Facebook != "")
			ret += "Facebook Ad;";
		if (w.KTTH_Ad != null && w.KTTH_Ad != "")
			ret += "KTTH Ad;";
		if (w.KVI_Ad != null && w.KVI_Ad != "")
			ret += "KVI Ad;";
		if (w.TV_Ad_Fox_News != null && w.TV_Ad_Fox_News != "")
			ret += "TV Ad - Fox News (48);";
		if (w.Spirit_Ad != null && w.Spirit_Ad != "")
			ret += "Spirit KCMS 105.3 FM Ad;";
		if (w.Praise_Ad != null && w.Praise_Ad != "")
			ret += "Praise KWPZ 106.5 FM Ad;";
		if (w.Longevity_Patient_Referral != null && w.Longevity_Patient_Referral != "")
			ret += "Patient Referral;";
		if (w.TV_Ad_History != null && w.TV_Ad_History != "")
			ret += "TV Ad - History (37);";
		if (w.Friend != null && w.Friend != "")
			ret += "Friend;";
		if (w.Internet != null && w.Internet != "")
			ret += "Internet;";
		if (w.Phone_Book != null && w.Phone_Book != "")
			ret += "Phone Book;";
		if (w.Doctor_Referral != null && w.Doctor_Referral != "")
			ret += "Doctor Referral";
		if (w.Print_Ad != null && w.Print_Ad != "")
			ret += "Print Ad - Tacoma News Tribune (TNT);";
		if (w.Unknown != null && w.Unknown != "")
			ret += "Unknown;";
		if (w.Mailer != null && w.Mailer != "")
			ret += "Mailer;";
		if (w.Mynorthwest != null && w.Mynorthwest != "")
			ret += "Mynorthwest;";
		if (w.Cosmetic_Dentistry != null && w.Cosmetic_Dentistry != "")
			ret += "Cosmetic Dentistry;";
		if (w.TV_Ad_Hallmark != null && w.TV_Ad_Hallmark != "")
			ret += "TV Ad - Hallmark;";
		if (w.Other != null && w.Other != "")
			ret += "Other;";
		if (ret != "")
			ret = ret.Substring(0, ret.Length - 1);
		return ret;
	}

}
class WebCSV
{
	public string Event_Location { get; set; }
	public string Phone_Area1 { get; set; }
	public string Phone_Pre1 { get; set; }
	public string Phone_Post1 { get; set; }
	public string Phone_Area2 { get; set; }
	public string Phone_Pre2 { get; set; }
	public string Phone_Post2 { get; set; }
	public string Address_1 { get; set; }
	public string City { get; set; }
	public string State { get; set; }
	public string Zip { get; set; }
	public string email { get; set; }
	public string Comments { get; set; }
	public string time_stamp { get; set; }
	public string First_Name { get; set; }
	public string Last_Name { get; set; }
	public string subject { get; set; }
	public string Publish { get; set; }
	public string Guest_1_First_Name { get; set; }
	public string Guest_1_Last_Name { get; set; }
	public string Guest_1_Same_Contact { get; set; }
	public string Guest_2_First_Name { get; set; }
	public string Guest_2_Last_Name { get; set; }
	public string Guest_2_Same_Contact { get; set; }
	public string KTTH_Leading_Edge_Medicine { get; set; }
	public string KVI_Leading_Edge_Medicine { get; set; }
	public string Referral_Source { get; set; }
	public string Facebook { get; set; }
	public string KTTH_Ad { get; set; }
	public string KVI_Ad { get; set; }
	public string TV_Ad_Fox_News { get; set; }
	public string Spirit_Ad { get; set; }
	public string Praise_Ad { get; set; }
	public string Longevity_Patient_Referral { get; set; }
	public string TV_Ad_History { get; set; }
	public string Friend { get; set; }
	public string Internet { get; set; }
	public string Phone_Book { get; set; }
	public string Doctor_Referral { get; set; }
	public string Print_Ad { get; set; }
	public string Unknown { get; set; }
	public string Mailer { get; set; }
	public string Mynorthwest { get; set; }
	public string Cosmetic_Dentistry { get; set; }
	public string TV_Ad_Hallmark { get; set; }
	public string Other { get; set; }
	public string Contact_Choice { get; set; }
}
class SharePointOut
{
	[CsvColumn(FieldIndex = 1)]
	public string Notes { get; set; }
	[CsvColumn(FieldIndex = 2)]
	public string ContactMethod { get; set; }
	[CsvColumn(FieldIndex = 3)]
	public string Status { get; set; }
	[CsvColumn(FieldIndex = 4)]
	public string SeminarSource { get; set; }
	[CsvColumn(FieldIndex = 5)]
	public string MktCallDate { get; set; }
	[CsvColumn(FieldIndex = 6)]
	public string MktSource { get; set; }
	[CsvColumn(FieldIndex = 7)]
	public string LastName { get; set; }
	[CsvColumn(FieldIndex = 8)]
	public string FirstName { get; set; }
	[CsvColumn(FieldIndex = 9)]
	public string Phone { get; set; }
	[CsvColumn(FieldIndex = 10)]
	public string Address { get; set; }
	[CsvColumn(FieldIndex = 11)]
	public string City { get; set; }
	[CsvColumn(FieldIndex = 12)]
	public string ST { get; set; }
	[CsvColumn(FieldIndex = 13)]
	public string Zip { get; set; }
	[CsvColumn(FieldIndex = 14)]
	public string Email { get; set; }
}


