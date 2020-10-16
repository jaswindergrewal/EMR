using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web.Services;
using System.Web.UI.WebControls;

public partial class MedicalNoteAdd_Long : LMCBase
{
    #region "Class"
    class TResult
    {
        public string PanelName;
        public string TestName;
        public string ResultValue;
        public int PanelID;
        public string Reference;
        public string unit;

    }
    #endregion

    #region "Variables"
    protected string PatientID = "";
    protected string ApptID = "";
    protected int StaffID = 0;
    protected DateTime? DOB;
    protected string Name = "";


    IAppointmentConsole _objAptConsoleService = null;
    protected PatientViewModel pat = null;
    LabLaunchViewModel objModel = null;
    #endregion

    #region "Events"
    /// <summary>
    /// Getting the patient First and last name based on patient id from query string
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["PatientID"] != null) PatientID = Request.QueryString["PatientID"];
        if (Request.QueryString["aptid"] != null) ApptID = Request.QueryString["aptid"];
        StaffID = (int)Session["StaffID"];

        try
        {
            _objAptConsoleService = new AppointmentConsole();
            pat = new PatientViewModel();
            pat = _objAptConsoleService.GetPatientList(int.Parse(PatientID));

            if (pat != null)
            {
                lblPatientName.Text = pat.FirstName + " " + pat.LastName;
                Name = lblPatientName.Text;
                DOB = pat.Birthday;
            }
            if (!IsPostBack)
            {
                LabLaunchService objService = new LabLaunchService();
                objModel = objService.GetByPatientId(int.Parse(PatientID));
                List<NavigationProp> AptDatesList = objModel.JoinCollection.ToList().Take(10).ToList();
                lstAptDates.DataSource = AptDatesList;//.OrderByDescending(t => t.ObservationDateTime).ToList().Take(10) ;
                lstAptDates.DataTextField = "ObservationDateTime";
                lstAptDates.DataValueField = "ObservationDateTime";
                lstAptDates.DataBind();
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }



    }
    /// <summary>
    /// inserting medical notes on click event
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string content = ed.Content;

        try
        {
            Session["ActiveTab"] = "Details";
            _objAptConsoleService = new AppointmentConsole();
            _objAptConsoleService.InsertMedicalNotes(int.Parse(PatientID), (int)Session["StaffID"], content.Trim(), int.Parse(ApptID));


        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            _objAptConsoleService = null;
        }
        Response.Redirect("apt_console.aspx?aptid=" + ApptID);
    }

    //protected void btnObjDrawDates_Click(object sender, EventArgs e)
    //{
    //    /* Initialize local variables */
    //    string Objstring = "";
    //    decimal BMI = 0;
    //    float hwratio = 0;
    //    int OldVID = 0;
    //    int CVID = 0;
    //    DateTime? vitaldate = new DateTime(1950, 1, 1);
    //    DateTime drawdate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
    //    DateTime upperLimit = new DateTime(3000, 1, 1);
    //    string weight = "";
    //    string labresults = "";
    //    string vString = "";
    //    string USID = "";
    //    List<string> USIDL = new List<string>();
    //    List<int> rid = new List<int>();
    //    List<TResult> Results = new List<TResult>();
    //    List<int> plist = new List<int>();
    //    IAppointmentConsole _objAptConsoleService = null;
    //    EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
    //    /*Pull vitals for current patient and entry dates to identify the two most recent data sets. */
    //    _objAptConsoleService = new AppointmentConsole();
    //    var vindex = _objAptConsoleService.GetVindexDetails(int.Parse(PatientID));
    //    string ObjVitalstring = string.Empty;
    //    /* Traverse vindex and Identify the ID's of the 2 most recent vitals sets. */
    //    try
    //    {
    //        foreach (var i in vindex)
    //        {
    //            if (i.DateEntered != null)
    //            {
    //                if (i.DateEntered > vitaldate)
    //                {
    //                    vitaldate = i.DateEntered;
    //                    OldVID = CVID;
    //                    CVID = i.Vital_ID;
    //                }
    //            }
    //            else
    //            {
    //                vitaldate = new DateTime(1950, 1, 1);
    //                OldVID = CVID;
    //                CVID = i.Vital_ID;
    //            }
    //        }

    //        /* Pull the current set of vitals from the database. */
    //        Patient_VitalsViewModel clsCVital = new Patient_VitalsViewModel();
    //        clsCVital = _objAptConsoleService.GetCVitalsList(CVID);
    //        var cVitals = clsCVital;

    //        /* Pull the prior set of vitals from the database. */
    //        clsCVital = _objAptConsoleService.GetCVitalsList(OldVID);
    //        var oVitals = clsCVital;

    //        /* test for empty vitals set and conditionally build vstring. */
    //        if ((cVitals == null) || (cVitals.Hip_Circm == null) || (cVitals.Waist_Circm == null) || (cVitals.BloodPres == null) || (cVitals.Pulse == null) || (cVitals.Temperature == null))
    //        {
    //            vString = "No vitals have been recorded for this patient or vitals entered were incomplete.<br/><br/>";
    //        }
    //        else
    //        {
    //            DateTime VDate = cVitals.DateEntered ?? new DateTime(1950, 1, 1);
    //            if ((cVitals.Wgt != null) && (cVitals.Height != null))
    //            {
    //                if (cVitals.Wgt > 0 && cVitals.Height > 0)
    //                {
    //                    decimal height = cVitals.Height ?? 0;
    //                    decimal bmiweight = cVitals.Wgt ?? 0;

    //                    BMI = (bmiweight / (height * height)) * 703;
    //                }
    //            }

    //            if ((cVitals.Hip_Circm != null) && (cVitals.Waist_Circm != null) && (cVitals.Hip_Circm != "") && (cVitals.Waist_Circm != ""))
    //            {
    //                hwratio = float.Parse(cVitals.Waist_Circm) / float.Parse(cVitals.Hip_Circm);
    //            }

    //            if (oVitals == null)
    //            {
    //                weight = "Measure ";
    //            }
    //            else
    //            {
    //                /* Examine the patients current weight compare it to their prior weight and construct the weight string. */
    //                if (cVitals.Wgt > oVitals.Wgt)
    //                {
    //                    weight = "<b>gain</b> of " + (cVitals.Wgt - oVitals.Wgt).ToString() + " pounds ";
    //                }
    //                else
    //                {
    //                    weight = "<b>loss</b> of " + (oVitals.Wgt - cVitals.Wgt).ToString() + " pounds ";
    //                }
    //            }
    //            /* construct conditional vString. */
    //            vString = "Vitals taken on " + VDate.ToString("d") + ", show a weight " + weight +
    //            "reflecting a current weight of " + cVitals.Wgt.ToString() + " pounds.  Patient's height was measured at, " + cVitals.Height.ToString() +
    //            " inches.  Patient's BMI is currently calculated at " + BMI.ToString("###.#") + ". Patient's waist hip ratio is " + hwratio.ToString("#.###") +
    //            ". Patient's blood pressure was " + cVitals.BloodPres + " and they had a pulse rate of " + cVitals.Pulse + ".  Patient temperature at the time was " +
    //            cVitals.Temperature + ". <br/><br/>";
    //        } /* end else */

    //        /* Pull the the pateints labs */
    //        var labs = (from p in ctx.lab_Patients
    //                    join cos in ctx.lab_CommonOrderSegments
    //                    on p.ID equals cos.PatientID
    //                    join osd in ctx.lab_OrderSegmentDetails
    //                    on cos.ID equals osd.CommonOrderSegmentID
    //                    join ords in ctx.lab_ObservationResultDetailSegments
    //                    on osd.ID equals ords.OrderSegmentDetailID
    //                    where int.Parse(PatientID) == p.CorrespondingPatientID
    //                    select new
    //                    {
    //                        osd.ObservationDateTime,
    //                        ords.ID,
    //                        ords.ObservationIdentifier,
    //                        ords.ObservationValue,
    //                        ords.Units,
    //                        ords.ReferencesRange
    //                    });




    //        /* Pull groups and Panels from DB */

    //        //var tests = _objAptConsoleService.GetTestLabDetails();
    //        var tests = (from t in ctx.LabReports_Tests
    //                     join groups in ctx.LabReports_Groups
    //                     on t.GroupID equals groups.GroupID
    //                     join panels in ctx.LabReports_Panels
    //                     on groups.PanelID equals panels.PanelID
    //                     select new
    //                     {
    //                         t.TestName,
    //                         panels.PanelName,
    //                         panels.PanelID,
    //                         groups.GroupName,
    //                         groups.GroupID
    //                     });



    //        List<LabReportPanelViewModel> panelgroups = _objAptConsoleService.GetLabReportPanel();



    //        /* Test the lab results set and build a conditional labresult string depending on whether labs exist. */

    //        /* Traverse the labs to find the most recent draw date */
    //        ObjVitalstring = "<br/><b>Objective Report: </b><br/><br/>" + vString;
    //        bool AptDrawDate = false;

    //        foreach (ListItem item in lstAptDates.Items)
    //        {

    //            USIDL = new List<string>();
    //            rid = new List<int>();
    //            plist = new List<int>();
    //            Results = new List<TResult>();
    //            if (item.Selected == true)
    //            {

    //                AptDrawDate = true;
    //                drawdate = Convert.ToDateTime(item.Text);

    //                foreach (var l in labs)
    //                {
    //                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
    //                    if (test2.Date == drawdate.Date)
    //                    {
    //                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
    //                        USID = Split[1];
    //                        if (!(USIDL.Contains(USID)))
    //                        {
    //                            USIDL.Add(USID);
    //                            rid.Add(l.ID);
    //                        }
    //                    }
    //                }

    //                foreach (var l in labs)
    //                {
    //                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
    //                    if ((test2.Date == drawdate.Date) && (rid.Contains(l.ID)))
    //                    {
    //                        List<int> GID = new List<int>();
    //                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
    //                        foreach (var t in tests)
    //                        {
    //                            if ((Split[1] == t.TestName) && (!(GID.Contains(t.GroupID))) || ((Split[1] == t.TestName + ' ') && (!(GID.Contains(t.GroupID)))))
    //                            {
    //                                TResult currentTest = new TResult();
    //                                currentTest.PanelID = t.PanelID;
    //                                currentTest.PanelName = t.PanelName;
    //                                currentTest.TestName = t.TestName;
    //                                currentTest.ResultValue = l.ObservationValue;
    //                                currentTest.Reference = l.ReferencesRange;
    //                                currentTest.unit = l.Units;

    //                                plist.Add(t.PanelID);
    //                                GID.Add(t.GroupID);
    //                                Results.Add(currentTest);
    //                            }
    //                        }
    //                    }
    //                }
    //                foreach (var p in panelgroups)
    //                {
    //                    if (plist.Contains(p.PanelID))
    //                    {
    //                        labresults = labresults + "</br><b>" + p.PanelName + ": </b></br>";
    //                        foreach (var r in Results)
    //                        {
    //                            if (r.PanelID == p.PanelID)
    //                                labresults = labresults + r.TestName + ": " + r.ResultValue + " " + r.unit + " <b>Range: </b>" + r.Reference + "<br/>";
    //                        }
    //                    }
    //                }
    //                Objstring = Objstring + "</br><b><font color='red'>The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/></font></b>" +
    //         labresults;
    //                labresults = "";
    //            }
    //        }

    //        if (AptDrawDate == false)
    //        {
    //            foreach (var l in labs)
    //            {

    //                DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
    //                if ((test2.Date > drawdate.Date) && (test2 < upperLimit.Date))
    //                {
    //                    drawdate = test2;
    //                }
    //            }
    //            if (drawdate == new DateTime(1950, 1, 1))
    //            {
    //                labresults = "No labs have yet been resulted into the system.<br/><br/>";
    //            }
    //            else
    //            {
    //                /* Traverse the labs and extract the lab name from the observation identifier use the resulting string to build the lab portion of the
    //                 * Subjective report.*/
    //                foreach (var l in labs)
    //                {
    //                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
    //                    if (test2.Date == drawdate.Date)
    //                    {
    //                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
    //                        USID = Split[1];
    //                        if (!(USIDL.Contains(USID)))
    //                        {
    //                            USIDL.Add(USID);
    //                            rid.Add(l.ID);
    //                        }
    //                    }
    //                }

    //                foreach (var l in labs)
    //                {
    //                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
    //                    if ((test2.Date == drawdate.Date) && (rid.Contains(l.ID)))
    //                    {
    //                        List<int> GID = new List<int>();
    //                        string[] Split = l.ObservationIdentifier.Split(new Char[] { '^' });
    //                        foreach (var t in tests)
    //                        {
    //                            if ((Split[1] == t.TestName) && (!(GID.Contains(t.GroupID))) || ((Split[1] == t.TestName + ' ') && (!(GID.Contains(t.GroupID)))))
    //                            {
    //                                TResult currentTest = new TResult();
    //                                currentTest.PanelID = t.PanelID;
    //                                currentTest.PanelName = t.PanelName;
    //                                currentTest.TestName = t.TestName;
    //                                currentTest.ResultValue = l.ObservationValue;
    //                                currentTest.Reference = l.ReferencesRange;
    //                                currentTest.unit = l.Units;

    //                                plist.Add(t.PanelID);
    //                                GID.Add(t.GroupID);
    //                                Results.Add(currentTest);
    //                            }
    //                        }
    //                    }
    //                }
    //            } /* end else */

    //            foreach (var p in panelgroups)
    //            {
    //                if (plist.Contains(p.PanelID))
    //                {
    //                    labresults = labresults + "</br><b>" + p.PanelName + ": </b></br>";
    //                    foreach (var r in Results)
    //                    {
    //                        if (r.PanelID == p.PanelID)
    //                            labresults = labresults + r.TestName + ": " + r.ResultValue + " " + r.unit + " <b>Range: </b>" + r.Reference + "<br/>";
    //                    }
    //                }
    //            }
    //            Objstring = Objstring + "</br><b><font color='red'>The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/></font></b>" +
    //        labresults;
    //            labresults = "";
    //        }
    //    }
    //    catch (System.Exception ex)
    //    {
    //        Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
    //        //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
    //    }

    //    finally
    //    {
    //        USIDL = null;
    //        rid = null;
    //        Results = null;
    //        plist = null;
    //        _objAptConsoleService = null;

    //    }
    //    /* Finally we use the accumulated data to construct the Objective report. */
    //    Objstring = ObjVitalstring + Objstring;
    //    ed.Content = Objstring;

    //}

    #endregion

    #region "Methods"

    /// <summary>
    /// This function build and returns the string value for the All button
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string allstringbuilder(string PatientID, string ApptID, int StaffID, string Name, DateTime? DOB)
    {
        string allstring = "initial";
        allstring = hstringbuilder(PatientID, ApptID, StaffID, Name, DOB) + sstringbuilder(PatientID, ApptID) + ostringbuilder(PatientID, ApptID) + astringbuilder(PatientID, ApptID) + pstringbuilder(PatientID, ApptID);
        return allstring;

    }


    /// <summary>
    /// This function builds and returns the string value for the header button
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string hstringbuilder(string PatientID, string ApptID, int StaffID, string Name, DateTime? DOB)
    {
        string dictator = "";


        //PatientViewModel pat = null;
        ProviderViewModel prov = null;
        apt_recViewModel apt = null;
        IAppointmentConsole _objAptConsoleService = null;
        string Headstring = "initial";
        try
        {
            _objAptConsoleService = new AppointmentConsole();

            prov = new ProviderViewModel();
            prov = _objAptConsoleService.GetProviderList(StaffID);

            apt = new apt_recViewModel();
            apt = _objAptConsoleService.GetAPTList(int.Parse(ApptID));

            if (prov == null)
            {
                dictator = "An MA or non-physician staff member";
            }
            else
            {
                dictator = prov.ProviderName;
            }
            DateTime Birthday = DOB ?? new DateTime(1850, 1, 1);
            Headstring = "<font weight='normal'><b>Date: </b>" + DateTime.Now.ToString() + "<br/>" + "<b>Dictation for: </b>" + Name + "<br/>" +
                "Age: " + Utilities.CalculateAge((DateTime)DOB).ToString() + "<br/>" + "Birthday: " + Birthday.Date.ToString("MMMM dd, yyyy") + "<br/>" + "Dictated by, " + dictator +
                " Regarding the care received during their " + apt.ApptStart.ToString() + " appointment." + "</font><br/><br/>";
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, true);
        }
        finally
        {

            prov = null;
            apt = null;
            _objAptConsoleService = null;
        }
        return Headstring;
    }

    /// <summary>
    /// This function builds the string value for the subjective button
    /// </summary>
    /// <returns></returns>

    [WebMethod]
    public static string sstringbuilder(string PatientID, string ApptID)
    {
        /* Initialize local variables */
        string NewSymptoms = "";
        string Resolved = "";
        string better = "";
        string same = "";
        string worse = "";
        string noOvu = "";
        DateTime lastAppt = new DateTime(1950, 1, 1);
        int priorID = 0;
        bool NewSymp = true;
        IAppointmentConsole _objAptConsoleService = null;
        string Substring = "initial";

        try
        {

            /* In order to calculate the existence of resolved or new symptoms, I must compare the current OVU with the previous OVU
             * This join pulls the patients prior appointments joined with their appointment types and stores the needed values for comparisum */
            _objAptConsoleService = new AppointmentConsole();
            var aptdates = _objAptConsoleService.GetAptDateDetails(int.Parse(PatientID));

            /* This foreach finds the last appointment with an ovu and store the table ID so we can access the symptomID's of the associated OVU */
            foreach (var i in aptdates)
            {

                if ((lastAppt > i.ApptStart) && (i.apt_id != int.Parse(ApptID)) && (i.OVU == true))
                {
                    priorID = i.apt_id;
                }
                lastAppt = i.ApptStart;
            }

            /* pull current apt from database */
            apt_recViewModel apt = _objAptConsoleService.GetAPTList(int.Parse(ApptID));

            /* Pull current symptom ID's, names and improvement status for current OVU */
            List<OvuAppointment> OVU = _objAptConsoleService.GetOVUDetails(int.Parse(ApptID));

            /* Pull current Symptom ID's from previous OVU */
            List<OvuAppointment> OVUold = _objAptConsoleService.GetOVUOldDetails(priorID);


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


            List<OfficeVisitViewModel> _listOfficeVisit = new List<OfficeVisitViewModel>();
            IOfficeVisitService objOvuService = null;
            objOvuService = new OfficeVisitService();
            _listOfficeVisit = objOvuService.GetOfficeVisitDetails(int.Parse(ApptID));

            string strBloodDraw = string.Empty;
            //for each list, populate data if it exisits
            foreach (var item in _listOfficeVisit)
            {

                if ((bool)item.Anastrozole == true)
                {
                    strBloodDraw = "Did you take Anastrozole or Arimidex the week before your test? :Yes; Comment: " + item.AnastrozoleComment + "<br>";
                }
                else
                {
                    strBloodDraw = "Did you take Anastrozole or Arimidex the week before your test? :NO; Comment: " + item.AnastrozoleComment + "<br>";
                }

                if ((bool)item.Creams == true)
                {
                    strBloodDraw += "Did you apply your creams and/or take your oral medications at least 2 hours before you blood draw?: Yes ; Comment: " + item.CreamComment + "<br>";
                }
                else
                {
                    strBloodDraw += "Did you apply your creams and/or take your oral medications at least 2 hours before you blood draw? : No; Comment: " + item.CreamComment + "<br>";
                }

                if ((bool)item.DHEA == true)
                {
                    strBloodDraw += "Did you take your DHEA?: Yes ; Comment: " + item.DHEAComment + "<br>";
                }
                else
                {
                    strBloodDraw += "Did you take your DHEA? : No; Comment: " + item.DHEAComment + "<br>";
                }

                if ((bool)item.Fasting == true)
                {
                    strBloodDraw += "Are you fasting?: Yes ; Comment: " + item.FastingComment + "<br>";
                }
                else
                {
                    strBloodDraw += "Are you fasting? : No; Comment: " + item.FastingComment + "<br>";
                }

                if ((bool)item.Pregenolone == true)
                {
                    strBloodDraw += "Did you take your pregenolone? : Yes ; " + item.PregenoloneAMPM + " Comment: " + item.PregenoloneComment + "<br>";
                }
                else
                {
                    strBloodDraw += "Did you take your pregenolone?  : No;" + item.PregenoloneAMPM + "  Comment: " + item.PregenoloneComment + "<br>";
                }

                if ((bool)item.Testosterone == true)
                {
                    strBloodDraw += "Did you wash off your testosterone cream with soap and a washcloth the night before your test?: Yes ; Comment: " + item.TestosteroneComment + "<br>";
                }
                else
                {
                    strBloodDraw += "Did you wash off your testosterone cream with soap and a washcloth the night before your test? : No; Comment: " + item.TestosteroneComment + "<br>";
                }

                strBloodDraw += "<b>Life Style</b><br><br>";

                strBloodDraw += "Water Intake:" + item.WaterIntake + " " + item.WaterIntakeComment + " " + item.IntakeType + "<br>";
                strBloodDraw += "Water Source:" + item.WaterSource + " " + item.WaterSourceComment + "<br>";
                strBloodDraw += "Exercise Frequency:" + item.ExerciseFreq + " " + item.ExerciseFreqComment + "<br>";
                strBloodDraw += "Exercise Type:" + item.ExerciseType + " " + item.ExerciseTypeCommnet + "<br>";
                strBloodDraw += "Workout Length:" + item.WorkoutLemgth + " " + item.WorkoutLemgthComment + "<br>";
                strBloodDraw += "Sleep Quality:" + item.SleepQuality + " " + item.SleepHours + " " + item.SleepQualityComment + "<br>";
                strBloodDraw += "Diet Quality:" + item.Diet + " " + item.DietComment + "<br>";
                strBloodDraw += "Energy Level:" + item.EnergeyLevel + " " + item.EnergyComment + "<br>";


            }

            Substring = "<b>Subjective Report: </b><br/>" + "At the time of their last blood draw: <br/><br/>" + strBloodDraw + Resolved + better + same + worse + NewSymptoms + noOvu;
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        return Substring;

    }


    /// <summary>
    /// This function builds the string value for the objective button 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string ostringbuilder(string PatientID, string ApptID)
    {
        /* Initialize local variables */
        string Objstring = "initial";
        decimal BMI = 0;
        float hwratio = 0;
        int OldVID = 0;
        int CVID = 0;
        DateTime? vitaldate = new DateTime(1950, 1, 1);
        DateTime drawdate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
        DateTime upperLimit = new DateTime(3000, 1, 1);
        string weight = "";
        string labresults = "";
        string vString = "";
        string USID = "";
        List<string> USIDL = new List<string>();
        List<int> rid = new List<int>();
        List<TResult> Results = new List<TResult>();
        List<int> plist = new List<int>();
        IAppointmentConsole _objAptConsoleService = null;
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        /*Pull vitals for current patient and entry dates to identify the two most recent data sets. */
        _objAptConsoleService = new AppointmentConsole();
        var vindex = _objAptConsoleService.GetVindexDetails(int.Parse(PatientID));

        /* Traverse vindex and Identify the ID's of the 2 most recent vitals sets. */
        try
        {
            foreach (var i in vindex)
            {
                if (i.DateEntered != null)
                {
                    if (i.DateEntered > vitaldate)
                    {
                        vitaldate = i.DateEntered;
                        OldVID = CVID;
                        CVID = i.Vital_ID;
                    }
                }
                else
                {
                    vitaldate = new DateTime(1950, 1, 1);
                    OldVID = CVID;
                    CVID = i.Vital_ID;
                }
            }

            /* Pull the current set of vitals from the database. */
            Patient_VitalsViewModel clsCVital = new Patient_VitalsViewModel();
            clsCVital = _objAptConsoleService.GetCVitalsList(CVID);
            var cVitals = clsCVital;

            /* Pull the prior set of vitals from the database. */
            clsCVital = _objAptConsoleService.GetCVitalsList(OldVID);
            var oVitals = clsCVital;

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
                    if (cVitals.Wgt > 0 && cVitals.Height > 0)
                    {
                        decimal height = cVitals.Height ?? 0;
                        decimal bmiweight = cVitals.Wgt ?? 0;

                        BMI = (bmiweight / (height * height)) * 703;
                    }
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

            //var tests = _objAptConsoleService.GetTestLabDetails();
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



            var panelgroups = _objAptConsoleService.GetLabReportPanel();



            /* Test the lab results set and build a conditional labresult string depending on whether labs exist. */

            /* Traverse the labs to find the most recent draw date */
            foreach (var l in labs)
            {

                DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
                if ((test2.Date > drawdate.Date) && (test2 < upperLimit.Date))
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
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

        finally
        {
            USIDL = null;
            rid = null;
            Results = null;
            plist = null;
            _objAptConsoleService = null;

        }
        /* Finally we use the accumulated data to construct the Objective report. */
        Objstring = "<b>Objective Report: </b><br/><br/>" + vString + "The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/>" +
            labresults;
        return Objstring;
    }


    /// <summary>
    /// This function builds the string value for the assessment button 
    /// </summary>
    /// <returns></returns>
    [WebMethod]
    public static string astringbuilder(string PatientID, string ApptID)
    {
        /* Initialize the local variables. */
        string astring = "initial";
        string dstring = "";
        try
        {
           
            IProtocolService objServicePro = new ProtocolService();
            List<ICD10CodesViewmodel> ICDCodes = new List<ICD10CodesViewmodel>();
            ICDCodes = objServicePro.GetPatientAssessIcd10Codes(Convert.ToInt32(ApptID), Convert.ToInt16(PatientID));
            if (ICDCodes != null)
            {
                foreach(var item in ICDCodes)
                {
                    dstring = dstring + item.Description + "<br/><br/>";
                }
            }
            else
            {
                /* Collect the patient diagnosis' from the database */
                IProblemListService objServiceDiag = new ProblemListService();
                List<DiagnosisListViewModel> diag = new List<DiagnosisListViewModel>();
                diag = objServiceDiag.GetProblemSDiagnosisListByPatientID(Convert.ToInt16(PatientID), 0);

                if (diag == null)
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
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        /* Finally we take the accumulated data and construct and return the patient assessment. */

        astring = "<b>Patient Assessment and Diagnosis: </b><br/><br/>" + "After a careful review of the patient's reported" +
            " symptoms and analyzing the patient's vitals and laboratory observations I have made the" +
            " assessment that the patient currently has the following medical conditions. <br/><br/>" + dstring +
            "<br/> Based on this assessment I have recommended the following treatment plan for the patient. <br/><br/>";
        return astring;
    }


    /// <summary>
    /// This function builds and returns the string value for the Plan button
    /// </summary>
    /// <returns></returns>

    [WebMethod]
    public static string pstringbuilder(string PatientID, string ApptID)
    {
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);

        /* initialize the local variables. */
        string currentsuplist = "";
        string pstring = "initial";
        string newsuplist = "";
        string closedsuplist = "";
        string represup = "";
        string fullsuplist = "";
        string closeddruglist = "";
        string currentdruglist = "";
        string newdruglist = "";
        string repredrug = "";
        string fulldruglist = "";
        string fullDiagnosisList = "";
        List<int> repreSupID = new List<int>();
        List<int> repreDrugID = new List<int>();

        try
        {
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
                DateTime closedate = s.Closed_Date ?? new DateTime(1950, 1, 1);
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
                    newsuplist = newsuplist + s.ProductName + ":" + s.SuppDose + "<br/><br/>";
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

           // IProblemListService _objProblemListService = new ProblemListService();

            //List<DiagnosistblViewModel> DiagnosisView = _objProblemListService.GetDiagnosisPropemApt(int.Parse(PatientID), int.Parse(ApptID));
            //if (DiagnosisView != null)
            //{
            //    foreach (var lstDiag in DiagnosisView)
            //    {
            //        fullDiagnosisList = fullDiagnosisList + lstDiag.Diag_Title + " [ " + lstDiag.ICD9_Code + " ]<br/>";
            //    }
            //}

        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            repreSupID = null;
            repreDrugID = null;
        }
        /* Take the accumulated data and construct and return the patient plan */
        pstring = "<b>Patient Plan: </b><br/><br/>" + "<b>Supplement Regimen:</b><br/>" + fullsuplist + "<b>Medications: </b><br/>" + fulldruglist;// + "<b>Diagnosis: </b><br/>" + fullDiagnosisList;
        return pstring;
    }

    [WebMethod]
    public static string LabDrawDatesstringbuilder(string PatientID, string ApptID, List<string> LabDrawDate)
    {
        /* Initialize local variables */
        string Objstring = "";
        decimal BMI = 0;
        float hwratio = 0;
        int OldVID = 0;
        int CVID = 0;
        DateTime? vitaldate = new DateTime(1950, 1, 1);
        DateTime drawdate = new DateTime(1950, 1, 1, 0, 0, 0, 0);
        DateTime upperLimit = new DateTime(3000, 1, 1);
        string weight = "";
        string labresults = "";
        string vString = "";
        string USID = "";
        List<string> USIDL = new List<string>();
        List<int> rid = new List<int>();
        List<TResult> Results = new List<TResult>();
        List<int> plist = new List<int>();
        IAppointmentConsole _objAptConsoleService = null;
        EMRDataContext ctx = new EMRDataContext(ConfigurationManager.ConnectionStrings["db"].ConnectionString);
        /*Pull vitals for current patient and entry dates to identify the two most recent data sets. */
        _objAptConsoleService = new AppointmentConsole();
        var vindex = _objAptConsoleService.GetVindexDetails(int.Parse(PatientID));
        string ObjVitalstring = string.Empty;
        /* Traverse vindex and Identify the ID's of the 2 most recent vitals sets. */
        try
        {
            foreach (var i in vindex)
            {
                if (i.DateEntered != null)
                {
                    if (i.DateEntered > vitaldate)
                    {
                        vitaldate = i.DateEntered;
                        OldVID = CVID;
                        CVID = i.Vital_ID;
                    }
                }
                else
                {
                    vitaldate = new DateTime(1950, 1, 1);
                    OldVID = CVID;
                    CVID = i.Vital_ID;
                }
            }

            /* Pull the current set of vitals from the database. */
            Patient_VitalsViewModel clsCVital = new Patient_VitalsViewModel();
            clsCVital = _objAptConsoleService.GetCVitalsList(CVID);
            var cVitals = clsCVital;

            /* Pull the prior set of vitals from the database. */
            clsCVital = _objAptConsoleService.GetCVitalsList(OldVID);
            var oVitals = clsCVital;

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
                    if (cVitals.Wgt > 0 && cVitals.Height > 0)
                    {
                        decimal height = cVitals.Height ?? 0;
                        decimal bmiweight = cVitals.Wgt ?? 0;

                        BMI = (bmiweight / (height * height)) * 703;
                    }
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

            //var tests = _objAptConsoleService.GetTestLabDetails();
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



            List<LabReportPanelViewModel> panelgroups = _objAptConsoleService.GetLabReportPanel();



            /* Test the lab results set and build a conditional labresult string depending on whether labs exist. */

            /* Traverse the labs to find the most recent draw date */
            ObjVitalstring = "<br/><b>Objective Report: </b><br/><br/>" + vString;
            bool AptDrawDate = false;

            foreach (var item in LabDrawDate)
            {

                USIDL = new List<string>();
                rid = new List<int>();
                plist = new List<int>();
                Results = new List<TResult>();


                AptDrawDate = true;
                drawdate = Convert.ToDateTime(item);

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
                Objstring = Objstring + "</br><b><font color='red'>The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/></font></b>" +
         labresults;
                labresults = "";

            }

            if (AptDrawDate == false)
            {
                foreach (var l in labs)
                {

                    DateTime test2 = l.ObservationDateTime ?? new DateTime(1950, 1, 1);
                    if ((test2.Date > drawdate.Date) && (test2 < upperLimit.Date))
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
                Objstring = Objstring + "</br><b><font color='red'>The patient's last set of blood work, drawn on " + drawdate.ToString("d") + ", yielded the following results: <br/></font></b>" +
            labresults;
                labresults = "";
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            //Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

        finally
        {
            USIDL = null;
            rid = null;
            Results = null;
            plist = null;
            _objAptConsoleService = null;

        }
        /* Finally we use the accumulated data to construct the Objective report. */
        Objstring = ObjVitalstring + Objstring;
        return Objstring;
        // ed.Content = Objstring;
    }

    #endregion


}