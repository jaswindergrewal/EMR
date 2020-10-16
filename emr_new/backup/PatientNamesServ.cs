using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Configuration;

/// <summary>
/// Summary description for PatientNamesServ
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class PatientNamesServ : System.Web.Services.WebService
{

	private EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
	public PatientNamesServ()
	{

		//Uncomment the following line if using designed components 
		//InitializeComponent(); 
	}

	[WebMethod]
	public string[] GetPatientList(String prefixText, Int32 count)
	{
		// Add your operation implementation here

		return Calendar.Patients.NameList(prefixText);
	}

	[WebMethod]
	public string[] GetDiagList(String prefixText, Int32 count)
	{
		// Add your operation implementation here


		List<string> items = (from d in ctx.Dictation_Diagnosis
							  where d.DiagnosisName.ToLower().Contains(prefixText.ToLower())
							  || d.KeyWords.ToLower().Contains(prefixText.ToLower())
							  select d.DiagnosisName).ToList();

		return items.ToArray();
	}

	[WebMethod]
	public string[] GetPlanList(String prefixText, Int32 count)
	{
		// Add your operation implementation here


		List<string> items = (from p in ctx.Dictation_Plans
							  where p.PlanName.ToLower().Contains(prefixText.ToLower())
							  || p.KeyWords.ToLower().Contains(prefixText.ToLower())
							  select p.PlanName).ToList();

		return items.ToArray();
	}



}
