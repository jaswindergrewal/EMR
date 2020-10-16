using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Text.RegularExpressions;



public partial class MedicalNoteAdd : LMCBase
{
    class TResult
    {
        public string PanelName;
        public string TestName;
        public string ResultValue;
        public int PanelID;
        public string Reference;
        public string unit;
 
    }
  
    
    protected string PatientID = "3799";
	protected string ApptID = "100021";
	protected EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
   
    /*This is where we initialize the strings generated with the "dictation buttons. */
    public string Headstring = "initial";
    public string Substring = "initial";
    public string Objstring = "initial";
    public string astring = "initial";
    public string pstring = "initial";
    public string allstring = "initial";

	
    protected void Page_Load(object sender, EventArgs e)
	{
        
		if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
		inpPatientID.Value = PatientID;
		if (Request.QueryString["aptid"] != null) ApptID = Request.QueryString["aptid"];
		if (!IsPostBack)
		{
			var pat = ctx.Patient_Details(int.Parse(PatientID)).First();

			lblPatientName.Text = pat.FirstName + " " + pat.LastName;
            
		}

	}

	private string CleanHtml(string html)
	{
		// start by completely removing all unwanted tags     
		html = Regex.Replace(html, @"<[/]?(font|span|xml|del|ins|[ovwxp]:\w+)[^>]*?>", "", RegexOptions.IgnoreCase);
		// then run another pass over the html (twice), removing unwanted attributes     
		html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
		html = Regex.Replace(html, @"<([^>]*)(?:class|lang|style|size|face|[ovwxp]:\w+)=(?:'[^']*'|""[^""]*""|[^\s>]+)([^>]*)>", "<$1$2>", RegexOptions.IgnoreCase);
		return html;
	}

	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		string content = ed.Content;
		//if (content.Contains("class=\"MsoNormal\"")) content = CleanHtml(content);
		//ctx.contact_tbl_EMR_Insert(8, int.Parse(PatientID), content.Replace("'","''"), (int)Session["StaffID"], int.Parse(ApptID));
		Contact_tbl cont = new Contact_tbl();
		cont.AptType = 8;
		cont.PatientID = int.Parse(PatientID);
		cont.EnteredBy = (int)Session["StaffID"];
		cont.ContactDateEntered = DateTime.Now;
		cont.MessageBody = content.Trim(); 
		cont.Apt_ID = int.Parse(ApptID);
		cont.FollowUP_Completed = false;
		ctx.Contact_tbls.InsertOnSubmit(cont);

		ctx.SubmitChanges();
		Response.Redirect("apt_console.aspx?aptid=" + ApptID);
	}

    /* This function build and returns the string value for the All button. */
    public string allstringbuilder()
    {
        allstring = hstringbuilder() + sstringbuilder() + ostringbuilder() + astringbuilder() + pstringbuilder();
        return allstring;
    }

    /* This function builds and returns the string value for the header button.*/
    public string hstringbuilder()
    {
        string dictator = "";

        /* Pull Patient info from dbase */
        Patient pat = (from p in ctx.Patients
                       where p.PatientID == int.Parse(PatientID)
                       select p).First();

        /* Pull Provider info from dbase */
        Provider prov = (from pr in ctx.Providers
                         where (int)Session["StaffID"] == pr.EmployeeID
                         select pr).FirstOrDefault();

        /* pull current apt from database */
        apt_rec apt = (from a in ctx.apt_recs
                       where int.Parse(ApptID) == a.apt_id
                      select a).First();

        if (prov == null)
        {
            dictator = "An MA or non-physician staff member";
        }
        else
        {
            dictator = prov.ProviderName;
        }
        DateTime Birthday = pat.Birthday ?? new DateTime(1850, 1, 1);
        Headstring = "<b>Date: </b>" + DateTime.Now.ToString() + "<br/>" + "<b>Dictation for: </b>" + pat.FirstName + " " + pat.LastName + "<br/>" +
            "Age: " + Utilities.CalculateAge((DateTime)pat.Birthday).ToString() + "<br/>" + "Birthday: " + Birthday.Date.ToString("MMMM dd, yyyy") + "<br/>" + "Dictated by, " + dictator +
            " Regarding the care received during their " + apt.ApptStart.ToString() + " appointment." + "<br/><br/>";
        return Headstring;
    }

    /* This function builds the string value for the subjective button */
    public string sstringbuilder()
    {
        /* Initialize local variables */
        string NewSymptoms = "";
        string Resolved = "";
        string better = "";
        string same = "";
        string worse = "";
        string noOvu = "";
        DateTime lastAppt = new DateTime(1950,1,1);
        int priorID = 0;
        bool NewSymp = true;


        /* In order to calculate the existence of resolved or new symptoms, I must compare the current OVU with the previous OVU
         * This join pulls the patients prior appointments joined with their appointment types and stores the needed values for comparisum */
        var aptdates = (from a in ctx.apt_recs
                        join aptt in ctx.AppointmentTypes on a.AppointmentTypeID equals aptt.ID
                        where int.Parse(PatientID) == a.patient_id
                        select new
                        {
                            a.ApptStart,
                            a.apt_id,
                            a.AppointmentTypeID,
                            aptt.OVU
                        });

        /* This foreach finds the last appointment with an ovu and store the table ID so we can access the symptomID's of the associated OVU */
        foreach (var i in aptdates)
        {
            if ((lastAppt < i.ApptStart) && (i.apt_id != int.Parse(ApptID)) && (i.OVU == true))
            {
                priorID = i.apt_id;
            }
        }

        /* pull current apt from database */
        apt_rec apt = (from a in ctx.apt_recs
                       where int.Parse(ApptID) == a.apt_id
                      select a).First();

        /* Pull current symptom ID's, names and improvement status for current OVU */
        var OVU = (from sy in ctx.apt_Symtpoms
                                  join Symname in ctx.Symptoms on sy.SymptomID equals Symname.SymptomID
                            where int.Parse(ApptID) == sy.AptID
                            select new
                            {
                                sy.AptID,
                                sy.dir,
                                sy.SymptomID,
                                Symname.SymptomName
                            });

        /* Pull current Symptom ID's from previous OVU */
        var OVUold = (from sy in ctx.apt_Symtpoms
                      join Symname in ctx.Symptoms on sy.SymptomID equals Symname.SymptomID
                      where priorID == sy.AptID
                      select new
                      {
                          sy.SymptomID,
                          Symname.SymptomName
                      }); 
                      

        /* Traverse the current OVU symptom ID list and compare it the the symptoms in the previous OVU list.
         * Add new symptoms to the NewSymptom string and any existing symptoms to the better, worse, or 
         * thesame string according to its arrow direction value */
        foreach (var c in OVU)
        {

            foreach (var o in OVUold)
            {

                if (o.SymptomID == c.SymptomID)
                {
                    NewSymp = false;
 
                    break;
                }
                else NewSymp = true;
 
            }
            if (NewSymp == true)
            {
                NewSymptoms = NewSymptoms + c.SymptomName + "<br/>";
            }
            else
            {
                if (c.dir == "up")
                {
                    better = better + c.SymptomName + "<br/>";
                }

                if (c.dir == "side")
                {
                    same = same + c.SymptomName + "<br/>";
                }
                if (c.dir == "down")
                {
                    worse = worse + c.SymptomName + "<br/>";
                }
            }
        }

        /* Traverse the prior OVU and Current OVU symptom list and find any symptoms that have resolved */

        foreach (var o in OVUold)
        {
            bool healed = true;
            foreach (var c in OVU)
            {
                if (c.SymptomID == o.SymptomID)
                {
                    healed = false;
                    break;
                }

            }
            if (healed == true)
            {
               Resolved = Resolved + o.SymptomName + "<br/>";
            }
        
        }

        /* Test each section of the symptom list and conditionally generate text depending on whether each part is empty. */
        if (Resolved != "")
        {
            Resolved = " the patient reported the resolution of the following previously identified symptoms.<br/><br/>" + Resolved + "<br/><br/>";
        }

        if (better != "")
        {
            better = "The patient reported that the following symptoms were improved from their last visit.<br/><br/>" + better + "<br/><br/>";
        }

        if (same != "")
        {
            same = "The patient reported the following symptoms were unchanged from their last visit.<br/><br/>" + same + "<br/><br/>";
        }

        if (worse != "")
        {
            worse = "The patient reported that the following symptoms have grown worse since their last visit.<br/><br/>" + worse + "<br/><br/>";
        }

        if (NewSymptoms != "")
        {
            NewSymptoms = "The patient reported the onset of the following new symptoms since their last visit. <br/><br/>" + NewSymptoms + "<br/><br/>";
        }
        if (Resolved + better + same + worse + NewSymptoms == "")
        {
            noOvu = "No symptom information has been entered for this patient.<br/><br/>";
        }

        /* Finally we take the accumulated data strings and build the subjective report from them. */

        Substring = "<b>Subjective Report: </b><br/>" + "At the time of their last blood draw: <br/><br/>" + Resolved + better + same + worse + NewSymptoms + noOvu;
        return Substring;
        
    }

    /* This function builds the string value for the objective button */
    public string ostringbuilder()
    {
        /* Initialize local variables */
        decimal BMI = 0;
        float hwratio = 0;
        int OldVID = 0;
        int CVID = 0;
        DateTime? vitaldate = new DateTime(1950, 1, 1);
        DateTime drawdate = new DateTime(1950, 1, 1,0,0,0,0);
        DateTime upperLimit = new DateTime(3000, 1, 1);
        string weight = "";
        string labresults = "";
        string vString = "";
        string USID = "";
        List<string> USIDL = new List<string>();
        List<int> rid = new List<int>();
        List<TResult> Results = new List<TResult>();
        List<int> plist = new List<int>();
        /*Pull vitals for current patient and entry dates to identify the two most recent data sets. */
        var vindex = (from v in ctx.Patient_Vitals
                      where int.Parse(PatientID) == v.Patient_ID
                      select new
                      {
                          v.Vital_ID,
                          v.DateEntered
                      });

        /* Traverse vindex and Identify the ID's of the 2 most recent vitals sets. */
        
        foreach (var i in vindex)
        {
            
            if (i.DateEntered > vitaldate)
            {
                vitaldate = i.DateEntered;
                OldVID = CVID;
                CVID = i.Vital_ID;
            }
        }

        /* Pull the current set of vitals from the database. */
        var cVitals = (from v in ctx.Patient_Vitals
                       where CVID == v.Vital_ID
                       select v).FirstOrDefault();

        /* Pull the prior set of vitals from the database. */
        var oVitals = (from v in ctx.Patient_Vitals
                       where OldVID == v.Vital_ID
                       select v).FirstOrDefault();
        
        /* test for empty vitals set and conditionally build vstring. */
        if ((cVitals == null) || (cVitals.Hip_Circm == null) || (cVitals.Waist_Circm == null) || (cVitals.BloodPres == null) || (cVitals.Pulse == null) || (cVitals.Temperature == null))
        {
            vString = "No vitals have been recorded for this patient or vitals entered were incomplete.<br/><br/>";
        }
        else
        {
            DateTime VDate = cVitals.DateEntered ?? new DateTime(1950, 1, 1);
            if ((cVitals.Wgt != null) && (cVitals.Height != null))
            {
                decimal height = cVitals.Height ?? 0;
                decimal bmiweight = cVitals.Wgt ?? 0;
                
                BMI = (bmiweight / (height * height)) * 703;
            }

            if ((cVitals.Hip_Circm != null) && (cVitals.Waist_Circm != null) && (cVitals.Hip_Circm != "") && (cVitals.Waist_Circm != ""))
            {
                hwratio = float.Parse(cVitals.Waist_Circm) / float.Parse(cVitals.Hip_Circm);
            }

            if (oVitals == null)
            {
                weight = "Measure ";
            }
            else
            {
                /* Examine the patients current weight compare it to their prior weight and construct the weight string. */
                if (cVitals.Wgt > oVitals.Wgt)
                {
                    weight = "<b>gain</b> of " + (cVitals.Wgt - oVitals.Wgt).ToString() + " pounds ";
                }
                else
                {
                    weight = "<b>loss</b> of " + (oVitals.Wgt - cVitals.Wgt).ToString() + " pounds ";
                }
            }
            /* construct conditional vString. */
            vString = "Vitals taken on " + VDate.ToString("d") + ", show a weight " + weight +
            "reflecting a current weight of " + cVitals.Wgt.ToString() + " pounds.  Patient's height was measured at, " + cVitals.Height.ToString() +
            " inches.  Patient's BMI is currently calculated at " + BMI.ToString("###.#") + ". Patient's waist hip ratio is " + hwratio.ToString("#.###") + 
            ". Patient's blood pressure was " + cVitals.BloodPres + " and they had a pulse rate of " + cVitals.Pulse + ".  Patient temperature at the time was " + 
            cVitals.Temperature + ". <br/><br/>";
        } /* end else */

        /* Pull the the pateints labs */
        var labs = (from p in ctx.lab_Patients
                    join cos in ctx.lab_CommonOrderSegments
                    on p.ID equals cos.PatientID
                    join osd in ctx.lab_OrderSegmentDetails
                    on cos.ID equals osd.CommonOrderSegmentID
                    join ords in ctx.lab_ObservationResultDetailSegments
                    on osd.ID equals ords.OrderSegmentDetailID
                    where int.Parse(PatientID) == p.CorrespondingPatientID
                    select new
                    {
                        osd.ObservationDateTime,
                        ords.ID,
                        ords.ObservationIdentifier,
                        ords.ObservationValue,
                        ords.Units,
                        ords.ReferencesRange
                    });

        /* Pull groups and Panels from DB */
        var tests = (from t in ctx.LabReports_Tests
                     join groups in ctx.LabReports_Groups
                     on t.GroupID equals groups.GroupID
                     join panels in ctx.LabReports_Panels
                     on groups.PanelID equals panels.PanelID
                     select new
                     {
                         t.TestName,
                         panels.PanelName,
                         panels.PanelID,
                         groups.GroupName,
                         groups.GroupID
                     });
        var panelgroups = (from p in ctx.LabReports_Panels
                      select new
                      {
                          p.PanelID,
                          p.PanelName
                      });
        


        /* Test the lab results set and build a conditional labresult string depending on whether labs exist. */

            /* Traverse the labs to find the most recent draw date */
            foreach (var l in labs)
            {

                DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
                if ((test2.Date > drawdate.Date ) && (test2 < upperLimit.Date))
                {
                    drawdate = test2;
                }
            }
            if (drawdate == new DateTime(1950, 1, 1))
            {
                labresults = "No labs have yet been resulted into the system.<br/><br/>";
            }
            else
            {
            /* Traverse the labs and extract the lab name from the observation identifier use the resulting string to build the lab portion of the
             * Subjective report.*/
                foreach (var l in labs)
                {
                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
                    if (test2.Date == drawdate.Date)
                    {
                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
                        USID = Split[1];
                        if (!(USIDL.Contains(USID)))
                        {
                            USIDL.Add(USID);
                            rid.Add(l.ID);
                        }
                    }
                }

                foreach (var l in labs)
                {
                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
                    if ((test2.Date == drawdate.Date) && (rid.Contains(l.ID)))
                    {
                        List<int> GID = new List<int>();
                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
                        foreach (var t in tests)
                        {
                            if ((Split[1] == t.TestName) && (!(GID.Contains(t.GroupID))) || ((Split[1] == t.TestName + ' ') && (!(GID.Contains(t.GroupID)))))
                            {
                                TResult currentTest = new TResult();
                                currentTest.PanelID = t.PanelID;
                                currentTest.PanelName = t.PanelName;
                                currentTest.TestName = t.TestName;
                                currentTest.ResultValue = l.ObservationValue;
                                currentTest.Reference = l.ReferencesRange;
                                currentTest.unit = l.Units;

                                plist.Add(t.PanelID);
                                GID.Add(t.GroupID);
                                Results.Add(currentTest);
                            }
                        }
                    }
                }
        } /* end else */

            foreach (var p in panelgroups)
            {
                if (plist.Contains(p.PanelID))
                {
                    labresults = labresults + "</br><b>" + p.PanelName + ": </b></br>";
                    foreach (var r in Results)
                    {
                        if (r.PanelID == p.PanelID)
                            labresults = labresults + r.TestName + ": " + r.ResultValue + " " + r.unit + " <b>Range: </b>" + r.Reference + "<br/>";
                    }
                }
            }

        /* Finally we use the accumulated data to construct the Objective report. */
        Objstring = "<b>Objective Report: </b><br/><br/>" + vString + "The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/>" + 
            labresults;
        return Objstring;
    }

    /* This function builds the string value for the assessment button */
    public string astringbuilder()
    {
        /* Initialize the local variables. */
        string dstring = "";

        /* Collect the patient diagnosis' from the database */
        var diag = (from d in ctx.Problem_Diagnosis_joins
                    join dtbl in ctx.Diagnosis_tbls
                    on d.DiagnosisID equals dtbl.Diagnosis_ID
                    where int.Parse(PatientID) == d.PatientID
                    select new
                    {
                        d.DateEntered,
                        dtbl.Diag_Title
                    });
        if (diag.FirstOrDefault() == null)
        {
            dstring = "No Diagnosis has been made for this patient.<br/>";
        }
        else
        {
            /* traverse the diag list and construct the dstring */
            foreach (var d in diag)
            {
                dstring = dstring + d.Diag_Title + "<br/><br/>";
            }
        } /* end else */
        /* Finally we take the accumulated data and construct and return the patient assessment. */

        astring = "<b>Patient Assessment: </b><br/><br/>" + "After a careful review of the patient's reported" +
            " symptoms and analyzing the patient's vitals and laboratory observations I have made the" +
            " assessment that the patient currently has the following medical conditions. <br/><br/>" + dstring +
            "<br/> Based on this assessment I have recommended the following treatment plan for the patient. <br/><br/>";
        return astring;
    }

    /* This function builds and returns the string value for the Plan button. */
    public string pstringbuilder()
    {

        /* initialize the local variables. */
        string currentsuplist = "";
        string newsuplist = "";
        string closedsuplist = "";
        string represup = "";
        string fullsuplist = ""; 
        string closeddruglist = "";
        string currentdruglist = "";
        string newdruglist = "";
        string repredrug = "";
        string fulldruglist = "";
        List<int> repreSupID = new List<int>();
        List<int> repreDrugID = new List<int>();
        


        /* Pull drug prescriptions from the database. */
        var drugs = (from d in ctx.Prescriptions
                     join name in ctx.Drugs
                     on d.Drug_ID equals name.DrugID
                     where int.Parse(PatientID) == d.PatientID
                     select new
                     {
                         name.DrugName,
                         d.Drug_Dose,
                         d.Drug_DatePrescibed,
                         d.viewable_yn,
                         d.ThirdParty_YN,
                         d.Closed_Date,
                         d.Closed_yn,
                         d.AptID,
                         d.RePre_yn,
                         d.Drug_ID
                     });

        /* Pull Supplement Prescriptions from the database. */
        var supp = (from s in ctx.PresscriptionSupps
                    join name in ctx.AutoshipProducts
                    on s.ProductID equals name.ProductID
                    where int.Parse(PatientID) == s.PatientID
                    select new
                    {
                        name.ProductName,
                        s.SuppDose,
                        s.DateEntered,
                        s.viewable_yn,
                        s.Closed_Date,
                        s.Closed_yn,
                        s.AptID,
                        s.RePre_yn,
                        s.ProductID
                    });

        var appt = (from a in ctx.apt_recs
                    where int.Parse(ApptID) == a.apt_id
                    select a).FirstOrDefault();

        /*Traverse the current supplement list and build a list of represcribed items. */

        foreach (var s in supp)
        {
            DateTime closedate = s.Closed_Date ?? new DateTime(1950, 1, 1);
            DateTime appdate = appt.ApptStart ?? new DateTime(1950, 1, 1);

            if (((s.viewable_yn == true) && (s.Closed_yn == false) && (s.AptID == int.Parse(ApptID)) && (s.RePre_yn == true)) || ((s.viewable_yn == true) && (s.Closed_yn == false) && (s.DateEntered.Date == DateTime.Now.Date) && (s.RePre_yn == true)))
            {
                repreSupID.Add(s.ProductID);
                represup = represup + s.ProductName + ": " + s.SuppDose + "<br/><br/>";
            }
        }
        /*Traverse the current supplement list and build the list of closed, continued, and new supplements. */
        foreach (var s in supp)
        {
            DateTime closedate = s.Closed_Date ?? new DateTime (1950, 1, 1);
            DateTime appdate = appt.ApptStart ?? new DateTime(1950, 1, 1);
            
            
            /* test and add supps closed today or during the appointment. */
            if (((s.Closed_Date != null) && (closedate.Date == appdate.Date) && (!(repreSupID.Contains(s.ProductID)))) || ((s.Closed_Date != null) && (closedate.Date == DateTime.Now.Date) && (!(repreSupID.Contains(s.ProductID)))))
            {
                closedsuplist = closedsuplist + s.ProductName + ": " + s.SuppDose + "<br/><br/>";
            }

            /* Test and add the previously prescribed supplements. */
            if ((s.viewable_yn == true) && (s.Closed_yn == false) && (s.AptID != int.Parse(ApptID)) && (s.DateEntered.Date != DateTime.Now.Date))
            {
                
                currentsuplist = currentsuplist + s.ProductName + ": " + s.SuppDose + "<br/><br/>";
            }

            /* Test and add the newly prescribed supplements. */
            if (((s.viewable_yn == true) && (s.Closed_yn == false) && (s.AptID == int.Parse(ApptID)) && (s.RePre_yn != true)) || ((s.viewable_yn == true) && (s.Closed_yn == false) && (s.DateEntered.Date == DateTime.Now.Date) && (s.RePre_yn != true)))
            {
                newsuplist = newsuplist + s.ProductName + ": " + s.SuppDose + "<br/><br/>";
            }
        }

        /* This section takes the closed current and full supplement list and constructs the supplement portion of the patient plan */
        if (represup != "")
        {
            fullsuplist = fullsuplist + "I am renewing the patient's prescriptions for the following previously prescribed supplements: <br/><br/>" + represup;
        }
        if (closedsuplist != "")
        {
            fullsuplist = fullsuplist + "I am recommending that the patient discontinue their use of the following previously prescribed nutritional supplements: <br/><br/>" + closedsuplist;
        }
        if (currentsuplist != "")
        {
            fullsuplist = fullsuplist + "I am recommending that the patient continue their current regimen of the following nutritional supplements: <br/><br/>" + currentsuplist;
        }
        if (newsuplist != "")
        {
            fullsuplist = fullsuplist + "I am recommending that the patient begin a regimen of the following nutritional supplements: <br/><br/>" + newsuplist;
        }

        /* Traverse Drugs and build a list of represcribed drug ID's, as refills are identified add them to the repredrug string */

        foreach (var d in drugs)
        {
            DateTime closedate = d.Closed_Date ?? new DateTime(1950, 1, 1);
            DateTime appdate = appt.ApptStart ?? new DateTime(1950, 1, 1);
            DateTime writedate = d.Drug_DatePrescibed ?? new DateTime(1950, 1, 1);
            int DrugID = d.Drug_ID ?? 0;

            if (((d.viewable_yn == true) && (d.Closed_yn == false) && (d.AptID == int.Parse(ApptID)) && (d.RePre_yn == true)) || ((d.viewable_yn == true) && (d.Closed_yn == false) && (writedate.Date == DateTime.Now.Date) && (d.RePre_yn == true)))
            {
                repreDrugID.Add(DrugID);
                repredrug = repredrug + d.DrugName + ": " + d.Drug_Dose + "<br/><br/>";
            }
        }

        /* Traverse Drugs and build the closed, current, and new drug lists. */
        foreach (var d in drugs)
        {
            DateTime closedate = d.Closed_Date ?? new DateTime(1950, 1, 1);
            DateTime appdate = appt.ApptStart ?? new DateTime(1950, 1, 1);
            DateTime writedate = d.Drug_DatePrescibed ?? new DateTime(1950, 1, 1);
            int DrugID = d.Drug_ID ?? 0;


            /* test and add drugs closed today or during the appointment. */
            if (((d.Closed_Date != null) && (closedate.Date == appdate.Date) && (!(repreDrugID.Contains(DrugID)))) || ((d.Closed_Date != null) && (closedate.Date == DateTime.Now.Date) && (!(repreDrugID.Contains(DrugID)))))
            {
                closeddruglist = closeddruglist + d.DrugName + ": " + d.Drug_Dose + "<br/><br/>";
            }

            /* Test and add the previously prescribed drugs. */
            if ((d.ThirdParty_YN == false) && (d.viewable_yn == true) && (d.Closed_yn == false) && (d.AptID != int.Parse(ApptID)) && (writedate.Date != DateTime.Now.Date))
            {

                currentdruglist = currentdruglist + d.DrugName + ": " + d.Drug_Dose + "<br/><br/>";
            }

            /* Test and add the newly prescribed drugs. */
            if (((d.viewable_yn == true) && (d.Closed_yn == false) && (d.AptID == int.Parse(ApptID)) && (d.RePre_yn != true)) || ((d.viewable_yn == true) && (d.Closed_yn == false) && (writedate.Date == DateTime.Now.Date) && (d.RePre_yn != true)))
            {
                newdruglist = newdruglist + d.DrugName + ": " + d.Drug_Dose + "<br/><br/>";
            }
      
        }

        /* This section takes the closed, current, and new drug lists and contstucts the drug part of the plan. */
        if (repredrug != "")
            fulldruglist = fulldruglist + "I am refilling the following previously prescribed medications: <br/><br/>" + repredrug;

        if (closeddruglist != "")
        {
            fulldruglist = fulldruglist + "I am recommending the patient discontinue or modify their use of the following previously prescribed medications: <br/><br/>" +
                closeddruglist;
        }
        if (currentdruglist != "")
        {
            fulldruglist = fulldruglist + "I am recommending the patient continue their current regimen of the following previously prescribed medications: <br/><br/>" +
                currentdruglist;
        }
        if (newdruglist != "")
        {
            fulldruglist = fulldruglist + "I am also prescribing the following additional medications: <br/><br/>" +
                newdruglist;
        }

        /* Take the accumulated data and construct and return the patient plan */
        pstring = "<b>Patient Plan: </b><br/><br/>" + "<b>Supplement Regimen:</b><br/>" + fullsuplist + "<b>Medications: </b><br/>" + fulldruglist;
        return pstring;
    }
}