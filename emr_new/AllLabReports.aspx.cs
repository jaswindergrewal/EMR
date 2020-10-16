using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using System.Configuration;
using System.IO;

public partial class AllLabReports : LMCBase
{
	protected void Page_Load(object sender, EventArgs e)
	{

	}
	protected void BtnPdf_Click(object sender, EventArgs e)
	{
		EMRDataContext ctx = new EMRDataContext("Data Source=lmcsql\\lmcsql;Initial Catalog=lmc_020505;Integrated Security=true;Connect Timeout=120");
		DateTime startDate = DateTime.Parse(txtStartDate.Text);
		int days = int.Parse(txtDays.Text);
		int[] apts = (from a in ctx.apt_recs
					  where (a.AppointmentTypeID == 3
					  || a.AppointmentTypeID == 4
					  || a.AppointmentTypeID == 84
					  || a.AppointmentTypeID == 87
					  || a.AppointmentTypeID == 91
					  || a.AppointmentTypeID == 96
					  )
					  && a.ApptStart < startDate.AddDays(days)
					  && a.ApptStart >= startDate
					  && a.ActionNeeded == "No"
					  select (int)a.patient_id).ToArray();
		foreach (int pat in apts)
		{
			string fileName = (from p in ctx.Patients where p.PatientID == pat select p.PatientID.ToString() + ".pdf").FirstOrDefault();

			RenderPdf(pat, fileName, txtPath.Text);
		}
	}
	private void RenderPdf(int PatientID, string fileName, string Path)
	{


		try
		{
			ReportParameter param = new ReportParameter("PatientName", PatientID.ToString());
			ReportParameter panels = new ReportParameter("Panels", GetPanels(PatientID).ToArray());
			RptVr.ServerReport.SetParameters(param);
			RptVr.ServerReport.SetParameters(panels);
			Byte[] results = null;

			results = RptVr.ServerReport.Render("PDF");
			File.WriteAllBytes(Path + fileName, results);
		}
		catch
		{

		}


		//This is very important if you want to directly download from stream instead of file

	}

	public List<string> GetPanels(int PatientID)
	{
		EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
		var Apts = from a in ctx.apt_recs
				   where a.patient_id == PatientID
				   && (a.AppointmentTypeID == 2
				   || a.AppointmentTypeID == 3
				   || a.AppointmentTypeID == 4
				   || a.AppointmentTypeID == 80
				   || a.AppointmentTypeID == 84)
				   && a.ApptStart < DateTime.Now
				   && a.Results == 3
				   orderby a.ApptStart descending
				   select a;
		DateTime LastReview = DateTime.MinValue;
		if (Apts.Count() < 2)
		{
			LastReview = DateTime.MinValue;
		}
		else
		{
			List<apt_rec> LstApts = Apts.ToList();
			LastReview = (DateTime)LstApts[1].ApptStart;
		}


		List<string> retList = new List<string>();
		Patient pat = (from p in ctx.Patients
					   where p.PatientID == PatientID
					   select p).ToList().First();
		var temp = (from osd in ctx.lab_OrderSegmentDetails
					join cos in ctx.lab_CommonOrderSegments on osd.CommonOrderSegmentID equals cos.ID
					join lp in ctx.lab_Patients on cos.PatientID equals lp.ID
					join p in ctx.Patients on lp.CorrespondingPatientID equals p.PatientID
					join ord in ctx.lab_ObservationResultDetailSegments on osd.ID equals ord.OrderSegmentDetailID
					where ord.ObservationDateTime != null
					&& p.PatientID == PatientID
					orderby p.PatientID, osd.ObservationDateTime descending
					select new
					{
						ObservationIdentifier = GetTestName(ord.ObservationIdentifier),
						ObservationValue = ord.ObservationValue,
						Units = ord.Units,
						ObservationDateTime = osd.ObservationDateTime,
						ReportedDateTime = osd.ResultsReportOrStatusDateTime,
						ReceivedDateTime = osd.SpecimenReceivedDateTime,
						PatientID = p.PatientID,
						FirstName = p.FirstName,
						LastName = p.LastName,
						Birthday = p.Birthday,
						Sex = p.Sex,
					}).ToList();

		List<string> tempList = (from t in temp
								 join te in ctx.LabReports_Tests on t.ObservationIdentifier equals te.TestName
								 join g in ctx.LabReports_Groups on te.GroupID equals g.GroupID
								 join pa in ctx.LabReports_Panels on g.PanelID equals pa.PanelID
								 where t.ObservationDateTime > LastReview
								 && t.ObservationDateTime < DateTime.Now
								 orderby pa.SortOrder,
								 te.TestID
								 select
									 //PanelDescrip = pa.PanelDescrip,
											 pa.PanelID.ToString()
			//PanelName = pa.PanelName,
			//PatientID = PatientID,
											).ToList();

		string PanelName = "";

		foreach (string pi in tempList)
		{
			if (pi != PanelName)
			{
				retList.Add(pi);
				PanelName = pi;
			}
		}



		return retList;

	}

	private string GetTestName(string ObservationIdentifier)
	{
		string retVal = string.Empty;
		try
		{
			retVal = ObservationIdentifier.Split('^')[4];
		}
		catch (IndexOutOfRangeException)
		{
			string[] vals = ObservationIdentifier.Split('^');
			int counter = vals.Length - 1;
			while (counter >= 0)
			{
				if (vals[counter] != "")
				{
					retVal = vals[counter];
					break;
				}
				counter--;
			}

		}
		return retVal;
	}

}