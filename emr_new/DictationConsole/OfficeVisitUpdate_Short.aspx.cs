using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Obout.Grid;
using System.Web.Services;
using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;


public partial class DictationConsole_OfficeVisitUpdate_Short : LMCBase
{
    #region "Variables"
    protected int AptID = 0;
    protected int PatientID = 0;
    IOfficeVisitService objService = null;

    #endregion

    #region "Events"

    /// <summary>
    /// Fill the form data on page load
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["aptid"] != null)
            {
                AptID = int.Parse(Request.QueryString["aptid"]);
                if (Request.QueryString["PatientID"] != null)
                {
                    PatientID = int.Parse(Request.QueryString["PatientID"]);
                }

                if (Request.Form["__EVENTTARGET"] == "custom")
                {
                    SaveRowsPosition(Request.Form["__EVENTARGUMENT"]);
                }

                if (Request.Form["__EVENTTARGET"] == "custom1")
                {
                    SaveRowsPositionGoals(Request.Form["__EVENTARGUMENT"]);
                }

                if (Request.Form["__EVENTTARGET"] == "GoalsGrid")
                {
                    grdGoals_Insert(Request.Form["__EVENTARGUMENT"]);
                }

                if (Request.Form["__EVENTTARGET"] == "SymptomsGrid")
                {
                    grdNew_Insert(Request.Form["__EVENTARGUMENT"]);
                }
                objService = new OfficeVisitService();
                List<Sympt> _listSymptom = new List<Sympt>();
                _listSymptom = objService.GetSymptom(AptID);

                ListBox lbSymptoms = grdNew.Templates[0].Container.FindControl("lstSymptoms") as ListBox;
                if (lbSymptoms != null)
                {
                    lbSymptoms.DataSource = _listSymptom;
                    lbSymptoms.DataBind();
                }

                List<Sympt> _listGoalItem = new List<Sympt>();
                _listGoalItem = objService.GetGoalItem(AptID);

                ListBox lbtGoals = grdGoals.Templates[0].Container.FindControl("lstGoals") as ListBox;
                if (lbtGoals != null)
                {
                    lbtGoals.DataSource = _listGoalItem;
                    lbtGoals.DataBind();
                }

                if (!IsPostBack)
                {
                    txtPAP.Attributes.Add("readonly", "readonly");
                    txtPhysical.Attributes.Add("readonly", "readonly");
                    txtMammo.Attributes.Add("readonly", "readonly");
                    txtProstate.Attributes.Add("readonly", "readonly");
                    Session["goal"] = null;
                    Session["sym"] = null;

                    Populate();

                }
            }
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }

    }

    /// <summary>
    /// insert a new symptom in a listbox
    /// </summary>
    /// <param name="data"></param>

    protected void grdNew_Insert(string data)
    {

        try
        {
            System.Web.UI.WebControls.ListBox lstSymptoms = (System.Web.UI.WebControls.ListBox)Utilities.FindControlRecursive(Master, "lstSymptoms");
            string[] SelectedSymptoms = data.Split('|');
            List<Sympt> sym1 = (List<Sympt>)Session["sym"];
            if (sym1 == null) sym1 = new List<Sympt>();
            foreach (string theSymptom in SelectedSymptoms)
            {
                string[] SelectedSymptomsRecord = theSymptom.Split('#');
                string SymptomName = SelectedSymptomsRecord[0];
                int SymptomID = Convert.ToInt16(SelectedSymptomsRecord[1].ToString());

                if (sym1 == null || ((from g in sym1 where g.Symptom == SymptomName select g).Count() == 0))
                {
                    Sympt sy = new Sympt();
                    sy.RowPosition = 100;
                    sy.dir = "side";
                    sy.resolved = false;
                    sy.Symptom = SymptomName;
                    sy.SymptomID = SymptomID;
                    sym1.Add(sy);
                }
            }
            List<Sympt> sym = (from s in sym1
                               orderby s.RowPosition
                               select new Sympt
                               {
                                   dir = s.dir,
                                   resolved = s.resolved,
                                   Symptom = s.Symptom,
                                   SymptomID = s.SymptomID,
                                   RowPosition = s.RowPosition,
                               }).ToList();
            Session["sym"] = sym;
            PopulateSymptoms(null);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }

    /// <summary>
    /// Insert a new goal in a listbox
    /// </summary>
    /// <param name="data"></param>
    protected void grdGoals_Insert(string data)
    {
        try
        {
            string[] SelectedGoals = data.Split('|');
            List<Sympt> goal1 = (List<Sympt>)Session["goal"];
            if (goal1 == null) goal1 = new List<Sympt>();
            foreach (string theGoal in SelectedGoals)
            {
                string[] SelectedGoalsRecord = theGoal.Split('#');
                string SymptomName = SelectedGoalsRecord[0];
                int SymptomID = Convert.ToInt16(SelectedGoalsRecord[1].ToString());
                if (goal1 == null || (from g in goal1 where g.Symptom == SymptomName select g).Count() == 0)
                {
                    Sympt sy = new Sympt();
                    sy.RowPosition = 100;
                    sy.dir = "side";
                    sy.resolved = false;
                    sy.Symptom = SymptomName;
                    sy.SymptomID = SymptomID;
                    goal1.Add(sy);
                }
            }
            List<Sympt> goal = (from s in goal1
                                orderby s.RowPosition
                                select new Sympt
                                {
                                    dir = s.dir,
                                    resolved = s.resolved,
                                    Symptom = s.Symptom,
                                    SymptomID = s.SymptomID,
                                    RowPosition = s.RowPosition,
                                }).ToList();
            Session["goal"] = goal;
            PopulateGoals(AptID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }

    }

    /// <summary>
    /// Redirect to appointment page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnCancel_Click(object sender, EventArgs e)
    {

        Response.Redirect("../Appointments.aspx?PatientID=" + PatientID.ToString());
    }

    /// <summary>
    /// Edit the symptom in a list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEdit_Click(object sender, EventArgs e)
    {
        
        Button btnEdit = (Button)sender;
        try
        {
            string[] id = btnEdit.ClientID.Split('_');

            int rowNum = int.Parse(id.Last());

            List<Sympt> sym1 = (List<Sympt>)Session["sym"];
            if (btnEdit.Text == "Resolve")
                sym1[rowNum].resolved = true;
            else
                sym1[rowNum].resolved = false;
            List<Sympt> sym = (from s in sym1
                               orderby s.RowPosition
                               select new Sympt
                               {
                                   dir = s.dir,
                                   resolved = s.resolved,
                                   Symptom = s.Symptom,
                                   SymptomID = s.SymptomID,
                                   RowPosition = s.RowPosition,
                               }).ToList();
            Session["sym"] = sym;
            Populate();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        
    }

    /// <summary>
    /// Edit the goal in a list
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnEditGoal_Click(object sender, EventArgs e)
    {
        Button btnEdit = (Button)sender;
        try
        {
            string[] id = btnEdit.ClientID.Split('_');

            int rowNum = int.Parse(id.Last());

            List<Sympt> goal1 = (List<Sympt>)Session["goal"];
            if (btnEdit.Text == "Resolve")
                goal1[rowNum].resolved = true;
            else
                goal1[rowNum].resolved = false;
            List<Sympt> goal = (from s in goal1
                                orderby s.RowPosition
                                select new Sympt
                                {
                                    dir = s.dir,
                                    resolved = s.resolved,
                                    Symptom = s.Symptom,
                                    SymptomID = s.SymptomID,
                                    RowPosition = s.RowPosition,
                                }).ToList();
            Session["goal"] = goal;
            Populate();
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
       

    }

    /// <summary>
    /// Save the office vist data
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnSave_Click(object sender, EventArgs e)
    {
        try
        {
            Emrdev.ViewModelLayer.apt_BloodWork bld = new Emrdev.ViewModelLayer.apt_BloodWork();
            bld.Anastrozole = cboMen1.Checked;
            bld.AptID = AptID;
            bld.Creams = cboCreams.Checked;
            bld.DHEA = cboDHEA.Checked;
            bld.Fasting = cboFasting.Checked;
            bld.Pregenolone = cboPrege.Checked;
            bld.Testosterone = cboMen2.Checked;
            bld.DateEntered = DateTime.Now;
            bld.EnteredBy = (int)Session["StaffID"];
            bld.AnastrozoleComment = txtMen1.Text;
            bld.CreamComment = txtCreams.Text;
            bld.DHEAComment = txtDHEA.Text;
            bld.FastingComment = txtFasting.Text;
            bld.PregenoloneComment = txtPrege.Text;
            bld.PregenoloneAMPM = rdoAmPm.SelectedValue;

            Emrdev.ViewModelLayer.apt_LisfeStyle lfe = new Emrdev.ViewModelLayer.apt_LisfeStyle();
            lfe.AptID = AptID;
            lfe.DateEntered = DateTime.Now;
            if (rdoDiet.SelectedItem != null) lfe.Diet = GetCheckBoxes(rdoDiet);
            lfe.DietComment = txtDiet.Text;
            if (rdoEnergy.SelectedItem != null) lfe.EnergeyLevel = GetCheckBoxes(rdoEnergy);
            lfe.EnergyComment = txtEnergy.Text;
            lfe.EnteredBy = (int)Session["StaffID"];
            lfe.DateEntered = DateTime.Now;
            if (rdoExercise.SelectedItem != null) lfe.ExerciseFreq = GetCheckBoxes(rdoExercise);
            if (chkExcerciseType.SelectedItem != null) lfe.ExerciseType = GetCheckBoxes(chkExcerciseType);
            if (rdoFruitVeggie.SelectedItem != null) lfe.FruitVeggie = GetCheckBoxes(rdoFruitVeggie);
            if (rdoWater.SelectedItem != null) lfe.IntakeType = GetCheckBoxes(rdoWater); // Modified By Rakesh on 11-June-2014
            lfe.Mealtonin = txtMelatonin.Text != "" ? int.Parse(txtMelatonin.Text) : (int?)null;
            lfe.SleepHours = txtHours.Text != "" ? int.Parse(txtHours.Text) : (int?)null;
            if (rdoSleep.SelectedItem != null) lfe.SleepQuality = GetCheckBoxes(rdoSleep);
            lfe.WaterIntake = txtWater.Text;
            if (rdoWaterSource.SelectedItem != null) lfe.WaterSource = GetCheckBoxes(rdoWaterSource);
            if (rdoWorkoutLength.SelectedItem != null) lfe.WorkoutLemgth = GetCheckBoxes(rdoWorkoutLength);
            lfe.DietComment = txtDiet.Text;
            lfe.EnergyComment = txtEnergy.Text;
            lfe.ExerciseFreqComment = txtExercise.Text;
            lfe.ExerciseTypeCommnet = txtExcerciseType.Text; ;
            lfe.FruitVeggieComment = txtFruitVeggie.Text; ;
            lfe.SleepQualityComment = txtSleep.Text;
            lfe.WaterIntakeComment = txtWaterComment.Text;
            lfe.WaterSourceComment = txtWaterSource.Text;
            lfe.WorkoutLemgthComment = txtWorkoutLength.Text;

            Emrdev.ViewModelLayer.apt_Misc mi = new Emrdev.ViewModelLayer.apt_Misc();
            mi.AptID = AptID;
            mi.DateEntered = DateTime.Now;
            mi.EnteredBy = (int)Session["StaffID"];
            mi.IllnessNames = txtSick.Text;
            mi.LastMammo = txtMammo.Text;
            mi.LastPap = txtPAP.Text;
            mi.LastPhysical = txtPhysical.Text;
            mi.LastProstate = txtProstate.Text;
            mi.NewIllness = cboSick.Checked;
            mi.NewMedicationsYN = cboNewMeds.Checked;
            mi.NewMedsNames = txt3rdPartyMeds.Text;
            mi.RealizeGoals = txtGoals1.Text;
            mi.HappyProgram = txtGoals2.Text;
            mi.Other = txtGoals3.Text;
            mi.SubjNote = "";
            List<apt_Symtpoms> sy = new List<apt_Symtpoms>();
            List<apt_Goals> go = new List<apt_Goals>();

            List<Sympt> sym1 = (List<Sympt>)Session["sym"];
            if (sym1 != null)
            {
                foreach (Sympt row in sym1)
                {
                    apt_Symtpoms sym = new apt_Symtpoms();
                    sym.AptID = AptID;
                    sym.DateEntered = DateTime.Now;
                    sym.dir = row.dir;
                    sym.EnteredBy = (int)Session["StaffID"];
                    sym.Priority = row.RowPosition;
                    sym.SymptomID = row.SymptomID;
                    sym.Resolved = row.resolved;
                    sy.Add(sym);
                }
                objService = new OfficeVisitService();
                objService.InsertSymptoms(sy, AptID);
            }
            List<Sympt> goal1 = (List<Sympt>)Session["goal"];
            if (goal1 != null)
            {
                foreach (Sympt row in goal1)
                {

                    apt_Goals sym = new apt_Goals();
                    sym.AptID = AptID;
                    sym.DateEntered = DateTime.Now;
                    sym.dir = row.dir;
                    sym.EnteredBy = (int)Session["StaffID"];
                    sym.Priority = row.RowPosition;
                    sym.GoalItemID = row.SymptomID;
                    sym.Resolved = row.resolved;
                    go.Add(sym);
                }
                objService = new OfficeVisitService();
                objService.InsertGoals(go, AptID);
            }
            objService = new OfficeVisitService();
            objService.InsetOfficeVisit(bld, lfe, mi);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
        Response.Redirect("../Appointments.aspx?PatientID=" + PatientID.ToString());

    }

    /// <summary>
    /// Save the directions of the arrow in symptom listbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgUp_Click(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;

        int RowNum = int.Parse(imgButton.ClientID.Split('_').Last());


        List<Sympt> sym1 = (List<Sympt>)Session["sym"];
        string SymptomName = sym1[RowNum].Symptom;
        string dir = "";
        if (imgButton.ClientID.Contains("imgUp"))
            dir = "up";
        else if (imgButton.ClientID.Contains("imgDown"))
            dir = "down";
        else
            dir = "side";


        foreach (Sympt ss in sym1)
        {
            if (ss.Symptom == SymptomName)
            {
                ss.dir = dir;
                break;
            }
        }
        List<Sympt> sym = (from s in sym1
                           orderby s.RowPosition
                           select new Sympt
                           {
                               dir = s.dir,
                               resolved = (bool)s.resolved,
                               Symptom = s.Symptom,
                               SymptomID = s.SymptomID,
                               RowPosition = s.RowPosition,
                           }).ToList();
        Session["sym"] = sym;
        Populate();
    }

    /// <summary>
    /// Save the directions of the arrows in goal  listbox
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void imgUpGoal_Click(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;

        int RowNum = int.Parse(imgButton.ClientID.Split('_').Last());


        List<Sympt> goal1 = (List<Sympt>)Session["goal"];
        string SymptomName = goal1[RowNum].Symptom;
        string dir = "";
        if (imgButton.ClientID.Contains("imgUp"))
            dir = "up";
        else if (imgButton.ClientID.Contains("imgDown"))
            dir = "down";
        else
            dir = "side";


        foreach (Sympt ss in goal1)
        {
            if (ss.Symptom == SymptomName)
            {
                ss.dir = dir;
                break;
            }
        }
        List<Sympt> goal = (from s in goal1
                            orderby s.RowPosition
                            select new Sympt
                            {
                                dir = s.dir,
                                resolved = (bool)s.resolved,
                                Symptom = s.Symptom,
                                SymptomID = s.SymptomID,
                                RowPosition = s.RowPosition,
                            }).ToList();
        Session["goal"] = goal;
        Populate();
    }

    #endregion

    #region "Methods"
    protected void Populate()
    {
        try
        {
            List<OfficeVisitViewModel> _listOfficeVisit = new List<OfficeVisitViewModel>();
            objService = new OfficeVisitService();
            _listOfficeVisit = objService.GetOfficeVisitDetails(AptID);


            //for each list, populate data if it exisits
            foreach (var item in _listOfficeVisit)
            {
                PatientID = (int)item.PatientId;
                cboMen1.Checked = (bool)item.Anastrozole;
                txtMen1.Text = item.AnastrozoleComment;
                cboCreams.Checked = (bool)item.Creams;
                txtCreams.Text = item.CreamComment;
                cboDHEA.Checked = (bool)item.DHEA;
                txtDHEA.Text = item.DHEAComment;
                cboFasting.Checked = (bool)item.Fasting;
                txtFasting.Text = item.FastingComment;
                cboPrege.Checked = (bool)item.Pregenolone;
                txtPrege.Text = item.PregenoloneComment;
                cboMen2.Checked = (bool)item.Testosterone;
                txtMen2.Text = item.TestosteroneComment;
                rdoAmPm.SelectedValue = item.PregenoloneAMPM;

                SetCheckBoxList(rdoDiet, item.Diet);
                txtDiet.Text = item.DietComment;
                SetCheckBoxList(rdoEnergy, item.EnergeyLevel);
                txtEnergy.Text = item.EnergyComment;
                SetCheckBoxList(rdoExercise, item.ExerciseFreq);
                txtExercise.Text = item.ExerciseFreqComment;
                SetCheckBoxList(chkExcerciseType, item.ExerciseType);
                txtExcerciseType.Text = item.ExerciseTypeCommnet;
                SetCheckBoxList(rdoFruitVeggie, item.FruitVeggie);
                txtFruitVeggie.Text = item.FruitVeggieComment;
                SetCheckBoxList(rdoWaterSource, item.WaterSource);  //// Modified By Rakesh on 11-June-2014
                txtWaterSource.Text = item.WaterSourceComment;
                txtMelatonin.Text = item.Mealtonin.ToString();
                txtHours.Text = item.SleepHours.ToString();
                SetCheckBoxList(rdoSleep, item.SleepQuality);
                txtSleep.Text = item.SleepQualityComment;
                txtWater.Text = item.WaterIntake.ToString();
                SetCheckBoxList(rdoWater, item.IntakeType); ///// Modified By Rakesh on 11-June-2014
                txtWaterComment.Text = item.WaterIntakeComment;
                SetCheckBoxList(rdoWorkoutLength, item.WorkoutLemgth);
                txtWorkoutLength.Text = item.WorkoutLemgthComment;

                txtSick.Text = item.IllnessNames;
                txtMammo.Text = item.LastMammo;
                txtPAP.Text = item.LastPap;
                txtPhysical.Text = item.LastPhysical;
                txtProstate.Text = item.LastProstate;
                cboSick.Checked = item.NewIllness;
                cboNewMeds.Checked = item.NewMedicationsYN;
                txtGoals1.Text = item.RealizeGoals;
                txtGoals2.Text = item.HappyProgram;
                txtGoals3.Text = item.Other;
                txt3rdPartyMeds.Text = item.NewMedsNames;

            }
            PopulateGoals(AptID);
            PopulateSymptoms(AptID);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
        finally
        {
            objService = null;
        }
    }

    /// <summary>
    /// the row position for symptoms will be saved
    /// </summary>
    /// <param name="data"></param>
    private void SaveRowsPosition(string data)
    {

        string[] items = data.Split(',');
        List<Sympt> sym1 = (List<Sympt>)Session["sym"];

        foreach (string item in items)
        {
            string[] itemData = item.Split('*');
            Sympt theOne = (from s in sym1 where s.Symptom == itemData[0] select s).First();
            theOne.RowPosition = int.Parse(itemData[1]);
        }

        List<Sympt> sym = (from s in sym1
                           orderby s.RowPosition
                           select new Sympt
                           {
                               dir = s.dir,
                               resolved = s.resolved,
                               Symptom = s.Symptom,
                               SymptomID = s.SymptomID,
                               RowPosition = s.RowPosition,
                           }).ToList();
        Session["sym"] = sym;
        Populate();

    }

    /// <summary>
    /// Save the row position of goals
    /// </summary>
    /// <param name="data"></param>
    private void SaveRowsPositionGoals(string data)
    {
        string[] items = data.Split(',');
        List<Sympt> goal1 = (List<Sympt>)Session["goal"];

        foreach (string item in items)
        {
            string[] itemData = item.Split('*');
            Sympt theOne = (from s in goal1 where s.Symptom == itemData[0] select s).First();
            theOne.RowPosition = int.Parse(itemData[1]);
        }

        List<Sympt> goal = (from s in goal1
                            orderby s.RowPosition
                            select new Sympt
                            {
                                dir = s.dir,
                                resolved = s.resolved,
                                Symptom = s.Symptom,
                                SymptomID = s.SymptomID,
                                RowPosition = s.RowPosition,
                            }).ToList();
        Session["goal"] = goal;
        Populate();

    }

    /// <summary>
    /// fill the symptom listbox
    /// </summary>
    /// <param name="aptID"></param>
    private void PopulateSymptoms(int? aptID)
    {
        objService = new OfficeVisitService();
        if (aptID == null && Session["sym"] == null)
            Session["sym"] = new List<Sympt>();
        List<Sympt> sym = new List<Sympt>();
        if (Session["sym"] == null)
        {

            List<Sympt> _listSymptom = new List<Sympt>();
            _listSymptom = objService.GetApt_Symptoms(AptID);


            try
            {
                sym = _listSymptom.ToList();
            }
            catch { }

            Session["sym"] = sym;
        }
        else
        {
            sym = (List<Sympt>)Session["sym"];
        }
        grdNew.DataSource = sym;
        grdNew.DataBind();
    }

    /// <summary>
    /// fill the goal listbox
    /// </summary>
    /// <param name="aptID"></param>
    private void PopulateGoals(int? aptID)
    {
        objService = new OfficeVisitService();
        if (aptID == null && Session["goal"] == null)
            Session["goal"] = new List<Sympt>();
        List<Sympt> goal = new List<Sympt>();
        if (Session["goal"] == null)
        {

            List<Sympt> _listGoals = new List<Sympt>();
            _listGoals = objService.GetApt_Goals(AptID);


            try
            {
                goal = _listGoals.ToList();
            }
            catch { }

            Session["goal"] = goal;
        }
        else
        {
            goal = (List<Sympt>)Session["goal"];
        }
        grdGoals.DataSource = goal;
        grdGoals.DataBind();
    }

    private void SetRadioList(RadioButtonList lst, string tx)
    {
        foreach (ListItem l in lst.Items)
        {
            if (l.Text == tx)
            {
                l.Selected = true;
                break;
            }
        }
    }
    private void SetCheckBoxList(CheckBoxList lst, string tx)
    {
        if (tx == null) tx = "";
        foreach (ListItem l in lst.Items)
        {
            if (tx.Contains(l.Text))
            {
                l.Selected = true;
            }
        }
    }

    private string GetCheckBoxes(CheckBoxList theList)
    {
        string ET = "";
        foreach (ListItem i in theList.Items)
        {
            if (i.Selected)
            {
                ET += i.Text + ";";
            }
        }
        return ET;
    }

    #endregion

}