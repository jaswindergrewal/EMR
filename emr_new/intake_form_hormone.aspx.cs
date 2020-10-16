using Emrdev.ServiceLayer;
using Emrdev.ViewModelLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class intake_form_hormone : System.Web.UI.Page
{
    #region Global

    IntakeHormoneViewModel objModel;
    IntakeHormoneService objService;

    int MasterFormId=0;
    int PatientId=0;

    #endregion

    #region Page Load Event

    protected void Page_Load(object sender, EventArgs e)
    {
        TextBoxAbnormalPapDate.Attributes.Add("readonly", "readonly");
        TextBoxDateOfLastMammogram.Attributes.Add("readonly", "readonly");
        TextBoxDateOfLastPapTest.Attributes.Add("readonly", "readonly");
        TextBoxDateOfLastPeriod.Attributes.Add("readonly", "readonly");
        TextBoxDateOfLastProstateExam.Attributes.Add("readonly", "readonly");
        TextBoxDateOfMenopause.Attributes.Add("readonly", "readonly");
       
    }

    #endregion

    #region Save Form Fields

    protected void ButtonSave_Click(object sender, EventArgs e)
    {
        //Save all form fields
        int.TryParse(Convert.ToString(Request.QueryString["form_id"]), out MasterFormId);
        int.TryParse(Convert.ToString(Request.QueryString["patient_id"]), out PatientId);
        SaveForm();
        Response.Redirect("wellness_questionaire.asp");
    }

    /// <summary>
    /// Save Intake Form Hormone
    /// </summary>
    void SaveForm()
    {
        try
        {
            objService = new IntakeHormoneService();
            objModel = new Emrdev.ViewModelLayer.IntakeHormoneViewModel();
            //=============Foriegn Key(s)======================//

            objModel.MasterFormId = MasterFormId;
            objModel.PatientId = PatientId;
            objModel.DateEntered = DateTime.Now;
            //==================For Women===================//

            if (TextBoxDateOfLastPeriod.Text != string.Empty)
                objModel.DateOfLastPeriod = Convert.ToDateTime(TextBoxDateOfLastPeriod.Text);
            objModel.BirthCtrlMethod = TextBoxBirthControlMethod.Text;
            objModel.Pregnant = Convert.ToInt32(RadionButtonListPregnant.SelectedValue) == 1 ? true : false;
            if (TextBoxDateOfLastPapTest.Text != string.Empty)
                objModel.DateOfLastPapTest = Convert.ToDateTime(TextBoxDateOfLastPapTest.Text);
            objModel.LastPapTestResult = DropDownListLastPapTestResult.SelectedValue;
            if (TextBoxDateOfLastMammogram.Text != string.Empty)
                objModel.DateOfLastMammogram = Convert.ToDateTime(TextBoxDateOfLastMammogram.Text);
            objModel.LastMammogramResult = DropDownListMammogramResult.SelectedValue;
            if (TextBoxDateOfMenopause.Text != string.Empty)
                objModel.DateOfMenopause = Convert.ToDateTime(TextBoxDateOfMenopause.Text);
            objModel.AbnormalPap = Convert.ToInt32(RadioButtonListAbnormalPap.SelectedValue) == 1 ? true : false;
            if (TextBoxAbnormalPapDate.Text != string.Empty)
                objModel.AbnormalPapDate = Convert.ToDateTime(TextBoxAbnormalPapDate.Text);
            objModel.HotFlashes = CheckBoxHotFlashes.Checked;
            objModel.NightSweats = CheckBoxNightSweats.Checked;
            objModel.LeakUrine = CheckBoxLeakUrine.Checked;
            objModel.FibrocysticBreasts = CheckBoxFibroCysticBreasts.Checked;
            objModel.SleepProblems = CheckBoxSleepProblems.Checked;
            objModel.VaginalDrynessPain = CheckBoxVaginalDrynessPain.Checked;
            objModel.LossOfInterestInSex = CheckBoxLossOfInterestInSex.Checked;
            objModel.IrregularPeriods = CheckBoxIrregularPeriods.Checked;
            objModel.SpottingAfterMenopause = CheckBoxSpottingAfterMenopause.Checked;
            objModel.MoodSwings = CheckBoxMoodSwings.Checked;
            objModel.Pms = CheckBoxPMS.Checked;
            objModel.BloatingLateInCycle = CheckBoxBloatingLatInCycle.Checked;
            objModel.MigrainesLateInCycle = CheckBoxMigrainesLateInCycle.Checked;
            objModel.CravingsSugarChocalate = CheckBoxCravingsForSugarChocalate.Checked;
            objModel.Pcos = CheckBoxPCOS.Checked;
            objModel.Acne = CheckBoxAcne.Checked;
            objModel.FacialHair = CheckBoxFacialHair.Checked;
            objModel.LackOfPeriods = CheckBoxLackOfPeriods.Checked;
            objModel.ProblemsWithInfertility = CheckBoxProblemsInfertility.Checked;
            objModel.OvarianCysts = CheckBoxOvarianCysts.Checked;
            objModel.UterineFibroid = CheckBoxUterineFibroid.Checked;
            objModel.CrampsClotsWithPeriod = CheckBoxCrampsClotsWithPeriod.Checked;
            objModel.IncreasedFatHipsThighs = CheckBoxIncreasedFatAroundHipsThighs.Checked;
            objModel.Endometriosis = CheckBoxEndometriosis.Checked;
            objModel.PainfulSex = CheckBoxPainfulPeriods.Checked;
            objModel.PainfulPeriods = CheckBoxPainfulPeriods.Checked;
            objModel.VaginalIrritation = CheckBoxVaginalIrritation.Checked;
            objModel.UnusualVaginalDischarge = CheckBoxUnusualVaginalDischarge.Checked;
            //=========================For Men=========================//

            if (TextBoxDateOfLastProstateExam.Text != string.Empty)
                objModel.DateOfLastProstateExam = Convert.ToDateTime(TextBoxDateOfLastProstateExam.Text);
            objModel.LoweredInterestInSex = CheckBoxLoweredInterestInSex.Checked;
            objModel.CannotMaintainErecetion = CheckBoxCantMaintainAnErection.Checked;
            objModel.ErectionLessFirm = CheckBoxErectionsLessFirm.Checked;
            objModel.EnlargedProstate = CheckBoxEnlargedProstate.Checked;
            objModel.SlowingUrinaryStream = CheckBoxSlowingUnrinaryStream.Checked;
            objModel.NightTimeUrination = CheckBoxNightTimeUrination.Checked;
            objModel.DifficultyInitiatingUrineStream = CheckBoxDifficultyInInitiatingUrineStream.Checked;
            objModel.BladderNotEmptying = CheckBoxBladderNotEmptyingCompletely.Checked;
            objModel.PrematureEjaculation = CheckBoxProblemsPrematureEjaculation.Checked;
            //========================Thyroid========================//

            objModel.DryHair = CheckBoxDryhair.Checked;
            objModel.Infertility = CheckBoxInfertility.Checked;
            objModel.Migraines = CheckBoxMigraines.Checked;
            objModel.LosingHair = CheckBoxLosingHair.Checked;
            objModel.Constipation = CheckBoxConstipation.Checked;
            objModel.FluidRetention = CheckBoxFluidRetention.Checked;
            objModel.CraveCaffeine = CheckBoxCraveCaffeine.Checked;
            objModel.DryCoarseSkin = CheckBoxDryhair.Checked;
            objModel.DietsDontWork = CheckBoxDietsDontWork.Checked;
            objModel.ColdHandsFeet = CheckBoxColdHandsFeet.Checked;
            objModel.ElevatedCholesterol = CheckBoxElevatedCholesterol.Checked;
            objModel.LowBodyTemp = CheckBoxLosingHair.Checked;
            objModel.FatigueExhaustion = CheckBoxFatigueExhaustion.Checked;
            objModel.DecreasedMemory = CheckBoxDecreasedMemory.Checked;
            objModel.BrittleNails = CheckBoxBrittleUnhealthyNails.Checked;
            objModel.UnableToLoseWeight = CheckBoxUnableToLoseWeight.Checked;
            objModel.DayTimeDrowsiness = CheckBoxDaytimeDrowsiness.Checked;
            objModel.FoggyMind = CheckBoxFoggySpaceyMind.Checked;
            objModel.DepressionAnxiety = CheckBoxDepressionAnxiety.Checked;
            objModel.LowAmbitionMotivation = CheckBoxLowAmbitionMotivation.Checked;
            objModel.DecreasedConcentration = CheckBoxDecreasedConcentration.Checked;
            objModel.FibromyalgiaChronicFatigue = CheckBoxFibromyalgiaChronicFatigue.Checked;
            objModel.FeelCold = CheckBoxFeelColdDressMoreWarmly.Checked;
            //==================Adrenal=======================//

            objModel.Palpitations = CheckBoxPalpitations.Checked;
            objModel.SaltCraving = CheckBoxSaltCraving.Checked;
            objModel.MuscleTension = CheckBoxMuscleTension.Checked;
            objModel.EasilyFrustrated = CheckBoxEasilyFrustrated.Checked;
            objModel.PoorStressTolerance = CheckBoxPoorStressTolerance.Checked;
            objModel.SugarCraving = CheckBoxSugarCraving.Checked;
            objModel.PanicAttacks = CheckBoxPanicAttacks.Checked;
            objModel.ExcessiveHungar = CheckBoxExcessiveHunger.Checked;
            objModel.ProneToInfection = CheckBoxProneToInfection.Checked;
            objModel.LowBloodPressure = CheckBoxLowBloodPressure.Checked;
            objModel.LightHeadedStandingUp = CheckBoxLightHeadedOnStandingUp.Checked;
            objModel.RacingMindNoSleep = CheckBoxRacingMindPreventsSleep.Checked;
            objModel.SluggishAmSlowStarter = CheckBoxSluggishInAMSlowStarter.Checked;
            objModel.NeedSunglassesInSunlight = CheckBoxNeedSunglassesInBrightSunLight.Checked;
            objModel.LowBackPainWithStress = CheckBoxLowBackPainWorsensWithFatigueORStress.Checked;
            //===================Metabolism====================//

            objModel.CannotSkipMeals = CheckBoxCanNotSkipMeals.Checked;
            objModel.HeadacheWhenMissedMeal = CheckBoxHeadacheWithMissedMeal.Checked;
            objModel.CraveSugarCarbs = CheckBoxCravingsForSugarCarbs.Checked;
            objModel.LowEnergyRelievedWithFood = CheckBoxPeriodsOfLowEnergyRelievedWithFood.Checked;
            objModel.ShakeWeak = CheckBoxShakeWeakEpisodesEatingHelps.Checked;
            objModel.JitteryIrratable = CheckBoxJitteryIrritableEpisodesEatingHelps.Checked;
            objModel.HighAndLowMoods = CheckBoxAlternatingBetweenHighAndLowMoods.Checked;
            objModel.SluggishAndHighEnergy = CheckBoxAlternatingBetweenSluggishAndHighEnergy.Checked;
            objModel.HighBloodPressure = CheckBoxHighBloodPressure.Checked;
            objModel.HighCholesterol = CheckBoxHighCholesterolTriglyceride.Checked;
            objModel.MidAfternoonDrowsiness = CheckBoxMidAfternoonDrowsiness.Checked;
            objModel.IncreasedFatAbdomen = CheckBoxIncreasedFatAroundAbdomen.Checked;
            objModel.InflammationBursitis = CheckBoxProneToInflammationBursitis.Checked;
            objModel.FluidRetentionPuffyExtremities = CheckBoxFluidRetentionPuffyInExtremities.Checked;
            //=======================Cardio-Respiratory=========================//

            objModel.DecreasedStamina = CheckBoxDecreasedStamina.Checked;
            objModel.DecreasedEndurance = CheckBoxDecreasedEndurance.Checked;
            objModel.RunOutOfBreath = CheckBoxRunOutOfBreathSooner.Checked;
            objModel.EasilyExhausted = CheckBoxEasilyExhaustedWithExercise.Checked;
            objModel.DecreasedDesireExercise = CheckBoxDecreasedAbilityAndDesireForExercise.Checked;
            objModel.DrySkin = CheckBoxDrySkin.Checked;
            objModel.ThinLips = CheckBoxThinLips.Checked;
            objModel.GrayingHair = CheckBoxGrayingHair.Checked;
            objModel.SkinBlemishes = CheckBoxSkinBlemishes.Checked;
            objModel.TendencyBruising = CheckBoxTendencyToBruising.Checked;
            objModel.ThinnedSkin = CheckBoxThinnedSkinHandsFaceArms.Checked;
            objModel.ThinningHair = CheckBoxThinningHairScalpArmpitsLegs.Checked;
            objModel.WrinklingSkin = CheckBoxWrinklingSkinFaceNeckHandsArms.Checked;
            objModel.SaggingSkin = CheckBoxSaggingSkinUnderEyesArmsFaceBreasts.Checked;
            //===========================Muscles / Joints ======================//

            objModel.Osteoporosis = CheckBoxOsteoporosis.Checked;
            objModel.AchesPains = CheckBoxAchesPains.Checked;
            objModel.LossOfStrength = CheckBoxLossOfStrength.Checked;
            objModel.BodyJoints = CheckBoxBodyJoints.Checked;
            objModel.ThinningMuscles = CheckBoxThinningMusclesButtocksArmsLegs.Checked;
            //======================Neuro-cognitive=============================//

            objModel.LossOfEsteem = CheckBoxLossOfEsteem.Checked;
            objModel.FeelingHopeless = CheckBoxFeelingHopeLess.Checked;
            objModel.FeelingDefeated = CheckBoxFeelingDefeated.Checked;
            objModel.FeelingApathy = CheckBoxFeelingOfApathy.Checked;
            objModel.LossOfConfidence = CheckBoxLossOfConfidence.Checked;
            objModel.VisionDeteriorating = CheckBoxVisionDeteriorating.Checked;
            objModel.HearingDeteriorating = CheckBoxHearingDeteriorating.Checked;
            objModel.MemoryDeteriorating = CheckBoxMemoryDeteriorating.Checked;
            objModel.BalanceDeteriorating = CheckBoxBalanceDeteriorating.Checked;
            objModel.CoordinationDeteriorating = CheckBoxCoordinationDeteriorating.Checked;
            objModel.SenseOfPowerLessNess = CheckBoxSenseOfPowerLessNess.Checked;
            objModel.DecreasedSenseOfWellBeing = CheckBoxDecreasedSenseOfWellBeing.Checked;
            //======================Gastrointestinal================================//

            objModel.Indigestion = CheckBoxIndigestionHyperacidity.Checked;
            objModel.FeelFullFaster = CheckBoxFeelFullFaster.Checked;
            objModel.SlowerDigestion = CheckBoxSlowerDigestion.Checked;
            objModel.EatLessMeals = CheckBoxEatLessSmallerMeals.Checked;
            objModel.FullnessPersistsAfterMeal = CheckBoxFullnessPersistsAfterMeals.Checked;
            objModel.BurpingBelchingAfterMeal = CheckBoxBurpingOrBelchingAfterMeals.Checked;
            objModel.DecreasedSenseOfTasteSmell = CheckBoxDecreasedSenseOfTasteSmell.Checked;
            //============================Diet=================================//

            objModel.SpecificDiet = TextBoxSpecificDiet.Text.Trim();
            objModel.DietIsSuccessfull = Convert.ToInt32(RadionButtonListIsItSuccessfull.SelectedValue) == 1 ? true : false;
            objModel.PastSuccessfulDiets = TextBoxSuccessfullDietInPast.Text.Trim();
            //===================Stress================================//

            objModel.StressLevel = DropDownListRateYourCurrentStressLevel.SelectedValue;
            objModel.StressHowLong = TextBoxHowLongHasItBeenLikeThis.Text.Trim();
            objModel.StressExpectToLast = DropDownListYouExpectThisToLast.SelectedValue;
            objModel.StressSolution = Convert.ToInt32(RadionButtonListDoYouHaveSolution.SelectedValue) == 1 ? true : false;
            objModel.NeedHelpWithStress = Convert.ToInt32(RadionButtonListDoYouNeedHelp.SelectedValue) == 1 ? true : false;
            //=========================Exercise=============================//

            objModel.ExerciseType = RadioButtonListExerciseType.SelectedValue;
            objModel.ExerciseLengthOfSession = TextBoxWorkOutSession.Text.Trim();
            objModel.ExerciseFrequency = TextBoxDaysWeek.Text.Trim();
            objModel.ExerciseTypeOther = HiddenTextBoxOther.Value.Trim();
            //===================Sleep================================//

            objModel.EarlyMorningWaking = CheckBoxTroubleGettingToSleep.Checked;
            objModel.TroubleGettingToSleep = CheckBoxTroubleGettingToSleep.Checked;
            objModel.SleepNotRestful = CheckBoxSleepNotAsRestful.Checked;
            objModel.SleepTossAndTurn = CheckBoxTossAndTurnThoughNight.Checked;
            objModel.DaytimeDrowsinessSleepiness = CheckBoxDaytimeDrowsinesssleepiness.Checked;
            objModel.SleepFeelingSmotheredChoke = CheckBoxWakeUpThroughTheNightFeeling.Checked;
            objModel.HeavingSnoringStopBreathing = CheckBoxNoticedVeryHeavySnoring.Checked;
            objModel.WorkNightHhift = CheckBoxWorkNightShift.Checked;
            objModel.NightShiftsPerWeek = TextBoxNightShift.Text.Trim();
            objService.SaveIntakeFormHormone(objModel);
        }
        catch (System.Exception ex)
        {
            Elmah.ErrorSignal.FromCurrentContext().Raise(ex);
            Response.Redirect("~/Error.aspx?message=" + ex.Message, false);
        }
    }

    #endregion
}