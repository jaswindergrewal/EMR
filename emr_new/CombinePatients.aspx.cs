using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.IO;

public partial class CombinePatients : LMCBase
{
	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	protected void Page_Load(object sender, EventArgs e)
    {

    }

	protected void btnGo_Click(object sender, EventArgs e)
	{
		//check for valid patient ID's

		Calendar.Patient source = new Calendar.Patient(int.Parse(txtSource.Text));
		Calendar.Patient dest = new Calendar.Patient(int.Parse(txtDest.Text));

		//Combine scans
			//Move database items
		var theUploads = from u in ctx.Upload_tbls
						 where u.PatientID == source.ID
						 select u;
		foreach (var thisUpload in theUploads)
		{
			thisUpload.PatientID = dest.ID;
		}
			//Move files
		string[] dirPath = Request.ServerVariables["PATH_TRANSLATED"].Split('\\');
		string thisPath = "";
		foreach(string seg in dirPath)
		{
			if (seg == "emr_new" || seg.ToLower() == "newemr") break;
			thisPath += seg + "\\";
		}
		var theFiles = Directory.EnumerateFiles(thisPath + "uploads\\" + source.ID.ToString());
		string destPath = thisPath + "emr_new\\uploads\\" + dest.ID.ToString();
		if(!Directory.Exists(destPath))
		{
			Directory.CreateDirectory(destPath);
		}
		foreach (string file in theFiles)
		{
			//move the files
			FileInfo thisOne = new FileInfo(file);
			string theName = thisOne.Name;
			thisOne.MoveTo(destPath + "\\" + theName);
		}
		

		//Combine Appointments
			//replace patient id's
		List<apt_rec> oldAppts = (from a in ctx.apt_recs
								 where a.patient_id == source.ID
								 select a).ToList();

		foreach (apt_rec aa in oldAppts)
		{
			aa.patient_id = dest.ID;
		}

		//Combine Contact Records
			//replace pateint id's

		List<Contact_tbl> oldContacts = (from c in ctx.Contact_tbls
										 where c.PatientID == source.ID
										 select c).ToList();

		foreach (Contact_tbl cc in oldContacts)
		{
			cc.PatientID = dest.ID;
		}

		//Combine Prescriptions
			//replace pateint id's

		List<Prescription> oldScrips = (from p in ctx.Prescriptions
										where p.PatientID == source.ID
										select p).ToList();

		foreach (Prescription pp in oldScrips)
		{
			pp.PatientID = dest.ID;
		}

		List<PresscriptionSupp> oldSsupps = (from p in ctx.PresscriptionSupps
										where p.PatientID == source.ID
										select p).ToList();

		foreach (PresscriptionSupp pp in oldSsupps)
		{
			pp.PatientID = dest.ID;
		}

		//Combine Labs
			//replace Corresponding Pateint ID's

		List<lab_Patient> oldLabs = (from l in ctx.lab_Patients
									 where l.CorrespondingPatientID == source.ID
									 select l).ToList();

		foreach (lab_Patient ll in oldLabs)
		{
			ll.CorrespondingPatientID = dest.ID;
		}


		//Combine Followups
			//replace pateint id's

		List<apt_FollowUp> oldFups = (from f in ctx.apt_FollowUps
									  where f.PatientID == source.ID
									  select f).ToList();

		foreach (apt_FollowUp ff in oldFups)
		{
			ff.PatientID = dest.ID;
		}

		//Combine Vitals
			//replace pateint id's

		List<Patient_Vital> oldVitals = (from v in ctx.Patient_Vitals
										 where v.Patient_ID == source.ID
										 select v).ToList();

		foreach (Patient_Vital vv in oldVitals)
		{
			vv.Patient_ID = dest.ID;
		}

		//Combine AS
			//ProfileItems
		List<ProfileItem> oldItems = (from i in ctx.ProfileItems
									  where i.PatientID == source.ID
									  select i).ToList();

		foreach (ProfileItem ii in oldItems)
		{
			ii.PatientID = dest.ID;
		}

			//Exceptions

		List<Exception> oldExc = (from ex in ctx.Exceptions
								  where ex.PatientID == source.ID
								  select ex).ToList();

		foreach (Exception ee in oldExc)
		{
			ee.PatientID = dest.ID;
		}

			//PRofile exceptions

		List<ProfileException> oldProfExc = (from pe in ctx.ProfileExceptions
											 where pe.PatientID == source.ID
											 select pe).ToList();

		foreach(ProfileException pepe in oldProfExc)
		{
			pepe.PatientID = dest.ID;
		}

			//Orders

		List<Order> oldOrders = (from o in ctx.Orders
								 where o.PatientID == source.ID
								 select o).ToList();

		foreach (Order oo in oldOrders)
		{
			oo.PatientID = dest.ID;
		}
								 


		//Combine Problems List
			//Problem_Diagnosis_joins
		List<Problem_Diagnosis_join> oldDiag = (from pdj in ctx.Problem_Diagnosis_joins
												where pdj.PatientID == source.ID
												select pdj).ToList();

		foreach (Problem_Diagnosis_join pdjpdj in oldDiag)
		{
			pdjpdj.PatientID = dest.ID;
		}
			//Problem_Symptom_joins

		List<Problem_Symptom_join> oldSympt = (from psj in ctx.Problem_Symptom_joins
											   where psj.PatientID == source.ID
											   select psj).ToList();

		foreach (Problem_Symptom_join psjpsj in oldSympt)
		{
			psjpsj.PatientID = dest.ID;
		}

			//Problem_MiscDiagnosis_joins

		List<Problem_MiscDiagnosis_join> oldMisc = (from m in ctx.Problem_MiscDiagnosis_joins
													where m.PatientID == source.ID
													select m).ToList();

		foreach (Problem_MiscDiagnosis_join mm in oldMisc)
		{
			mm.PatientID = dest.ID;
		}

		//Combine QB
		List<QB_Match> oldMatch = (from q in ctx.QB_Matches
								   where q.PatientID == source.ID
								   select q).ToList();

		foreach (QB_Match qq in oldMatch)
		{
			qq.PatientID = dest.ID;
		}

		//Combine Old Visits
		List<Visit> OldVisits = (from ov in ctx.Visits
								 where ov.PatientID == source.ID
								 select ov).ToList();

		foreach (Visit ovov in OldVisits)
		{
			ovov.PatientID = dest.ID;
		}

		List<Callbacks_old> OldNotes = (from oc in ctx.Callbacks_olds
										where oc.PatientID == source.ID
										select oc).ToList();

		foreach (Callbacks_old ococ in OldNotes)
		{
			ococ.PatientID = dest.ID;
		}

		ctx.SubmitChanges();
	}
}