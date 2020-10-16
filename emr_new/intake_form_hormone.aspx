<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true" CodeFile="intake_form_hormone.aspx.cs" Inherits="intake_form_hormone" %>

<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <style type="text/css">
        .style1 {
            font-size: 14px;
            font-weight: bold;
        }
    </style>

    <script type="text/javascript">

        function SetValue() {
            var other = $("#txtOther").val();
            $("#HiddenTextBoxOther").val(other);
        }

    </script>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p>
        <span class="style1">Hormone Review</span><br />
        Review these symptoms of aging and check the ones that apply.
    </p>


    <table width="100%" border="0" cellpadding="10" cellspacing="0" bgcolor="#666666" class="border">
        <tr>
            <td>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td><strong>For Women </strong></td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Date of first day of last period:</td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDateOfLastPeriod" CssClass="FormField"></asp:TextBox>
                            (mm/dd/yyyy)
                             <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="TextBoxDateOfLastPeriod" DatePickerSynchronize="true"
                                 DatePickerImagePath="~/images/date_picker1.gif" />
                        </td>
                        <td>Birth control method: 
                            <asp:TextBox runat="server" ID="TextBoxBirthControlMethod" CssClass="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>Are you pregnant?               

                            <asp:RadioButtonList runat="server" ID="RadionButtonListPregnant" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>Date of last PAP test:</td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDateOfLastPapTest" CssClass="FormField"></asp:TextBox>
                            (mm/dd/yyyy)
                            <obout:Calendar ID="Calendar2" runat="server" DatePickerMode="true" TextBoxId="TextBoxDateOfLastPapTest" DatePickerSynchronize="true"
                                DatePickerImagePath="~/images/date_picker1.gif" />

                        </td>
                        <td>Was it:             
                            <asp:DropDownList runat="server" ID="DropDownListLastPapTestResult" CssClass="border" AppendDataBoundItems="true">
                                <asp:ListItem Text="- Select -" Value="NA"></asp:ListItem>
                                <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="Abnormal"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Date of last Mammogram:</td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDateOfLastMammogram" CssClass="FormField"></asp:TextBox>
                            (mm/dd/yyyy)
 <obout:Calendar ID="Calendar3" runat="server" DatePickerMode="true" TextBoxId="TextBoxDateOfLastMammogram" DatePickerSynchronize="true"
     DatePickerImagePath="~/images/date_picker1.gif" />

                        </td>
                        <td>Was it:
                            <asp:DropDownList runat="server" ID="DropDownListMammogramResult" AppendDataBoundItems="true" CssClass="FormField">
                                <asp:ListItem Text="- Select -" Value="NA"></asp:ListItem>
                                <asp:ListItem Text="Normal" Value="Normal"></asp:ListItem>
                                <asp:ListItem Text="Abnormal" Value="Abnormal"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Date of Menopause:</td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDateOfMenopause" CssClass="FormField"> </asp:TextBox>(mm/dd/yyyy)
                             <obout:Calendar ID="Calendar6" runat="server" DatePickerMode="true" TextBoxId="TextBoxDateOfMenopause" DatePickerSynchronize="true"
                                 DatePickerImagePath="~/images/date_picker1.gif" />

                        </td>
                        <td>Have you ever had an abnormal PAP? 
                            
                            <asp:RadioButtonList runat="server" ID="RadioButtonListAbnormalPap" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>

                            <td>When? 
                            <asp:TextBox runat="server" ID="TextBoxAbnormalPapDate" CssClass="FormField"></asp:TextBox>
                                (mm/dd/yyyy) 
                                 <obout:Calendar ID="Calendar4" runat="server" DatePickerMode="true" TextBoxId="TextBoxAbnormalPapDate" DatePickerSynchronize="true"
                                     DatePickerImagePath="~/images/date_picker1.gif" />

                            </td>
                    </tr>
                </table>

                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxHotFlashes" />
                        </td>
                        <td>Hot flashes</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxNightSweats" />
                        </td>
                        <td>Night sweat</td>

                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLeakUrine" />
                        </td>
                        <td>Leak Urine</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFibroCysticBreasts" />
                        </td>
                        <td>Fibro-cystic Breasts
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSleepProblems" />
                        </td>
                        <td>Sleep problems
                        </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxVaginalDrynessPain" />
                        </td>
                        <td>Vaginal dryness / Pain
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLossOfInterestInSex" />
                        </td>
                        <td>Loss of interest in sex
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxIrregularPeriods" />
                        </td>
                        <td>Irregular periods
                        </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSpottingAfterMenopause" />
                        </td>
                        <td>Spotting after menopause</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxMoodSwings" />
                        </td>
                        <td>Mood swings </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxPMS" />
                        </td>
                        <td>PMS</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBloatingLatInCycle" />
                        </td>
                        <td>Bloating late in cycle </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxMigrainesLateInCycle" />
                        </td>
                        <td>Migraines late in cycle </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxCravingsForSugarChocalate" />
                        </td>
                        <td>Cravings for sugar / chocalate</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxPCOS" />
                        </td>
                        <td>PCOS</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxAcne" />
                        </td>
                        <td>Acne</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFacialHair" />
                        </td>
                        <td>Facial hair </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLackOfPeriods" />
                        </td>
                        <td>Lack of periods</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxProblemsInfertility" />
                        </td>
                        <td>Problems w/ infertility</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxOvarianCysts" />
                        </td>
                        <td>Ovarian cysts </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxUterineFibroid" />
                        </td>
                        <td>Uterine fibroid </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxCrampsClotsWithPeriod" />
                        </td>
                        <td>Cramps / Clots with period </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxIncreasedFatAroundHipsThighs" />
                        </td>
                        <td>Increased fat around hips / thighs</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxEndometriosis" />
                        </td>
                        <td>Endometriosis</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxPainfulSex" />
                        </td>
                        <td>Painful sex </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxPainfulPeriods" />
                        </td>
                        <td>Painful periods </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxVaginalIrritation" />
                        </td>
                        <td>Vaginal irritation</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxUnusualVaginalDischarge" />
                        </td>
                        <td>Unusual vaginal discharge </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td><strong>For Men </strong></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="180">Date of last prostate exam:</td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxDateOfLastProstateExam" CssClass="FormField"></asp:TextBox>
                            (mm/dd/yyyy)
                               <obout:Calendar ID="Calendar5" runat="server" DatePickerMode="true" TextBoxId="TextBoxDateOfLastProstateExam" DatePickerSynchronize="true"
                                   DatePickerImagePath="~/images/date_picker1.gif" />

                        </td>
                    </tr>
                </table>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLoweredInterestInSex" />
                        </td>
                        <td>Lowered interest in sex </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxCantMaintainAnErection" />
                        </td>
                        <td>Can't maintain an erection </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxErectionsLessFirm" />
                        </td>
                        <td>Erections less firm </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxEnlargedProstate" />
                        </td>
                        <td>Enlarged prostate</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSlowingUnrinaryStream" />
                        </td>
                        <td>Slowing unrinary stream </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxNightTimeUrination" />
                        </td>
                        <td>Night-time urination</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDifficultyInInitiatingUrineStream" />
                        </td>
                        <td>Difficulty in initiating urine stream </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBladderNotEmptyingCompletely" />
                        </td>
                        <td>Bladder not emptying completely </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxProblemsPrematureEjaculation" />
                        </td>
                        <td>Problems w/ premature ejaculation </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br />
                <span class="style2">For All </span>
                <br>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Thyroid</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">

                            <asp:CheckBox runat="server" ID="CheckBoxDryhair" />
                        </td>
                        <td>Dry hair</td>
                        <td>

                            <asp:CheckBox runat="server" ID="CheckBoxInfertility" /></td>
                        <td>Infertility</td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxMigraines" />
                        </td>
                        <td>Migraines</td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxLosingHair" />
                        </td>
                        <td>Losing hair</td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxConstipation" />
                        </td>
                        <td>Constipation</td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxFluidRetention" />
                        </td>
                        <td>Fluid retention </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxCraveCaffeine" />
                        </td>
                        <td>Crave caffeine </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxDryCoarseSkin" />
                        </td>
                        <td>Dry coarse skin </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxDietsDontWork" />
                        </td>
                        <td>Diets don't work </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxColdHandsFeet" />
                        </td>
                        <td>Cold hands &amp; feet </td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxElevatedCholesterol" />
                        </td>
                        <td>Elevated cholesterol </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxLowBodyTemperature" />
                        </td>
                        <td>Low body temperature </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxFatigueExhaustion" />
                        </td>
                        <td>Fatigue / exhaustion </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedMemory" />
                        </td>
                        <td>Decreased memory</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBrittleUnhealthyNails" />
                        </td>
                        <td>Brittle unhealthy nails </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxUnableToLoseWeight" />
                        </td>
                        <td>Unable to lose weight</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDaytimeDrowsiness" />
                        </td>
                        <td>Daytime drowsiness </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFoggySpaceyMind" />
                        </td>
                        <td>Foggy / Spacey mind </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDepressionAnxiety" />
                        </td>
                        <td>Depression / anxiety </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLowAmbitionMotivation" />
                        </td>
                        <td>Low ambition / motivation </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedConcentration" />
                        </td>
                        <td>Decreased concentration </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFibromyalgiaChronicFatigue" />
                        </td>
                        <td>Fibromyalgia / Chronic fatigue </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFeelColdDressMoreWarmly" />
                        </td>
                        <td>Feel cold / Dress more warmly </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Adrenal</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">


                            <asp:CheckBox runat="server" ID="CheckBoxPalpitations" />
                        </td>
                        <td>Palpitations</td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxSaltCraving" />
                        </td>
                        <td>Salt craving </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxMuscleTension" />
                        </td>
                        <td>Muscle tension </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxEasilyFrustrated" />
                        </td>
                        <td>Easily frustrated </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxPoorStressTolerance" />
                        </td>
                        <td>Poor stress tolerance </td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxSugarCraving" />
                        </td>
                        <td>Sugar craving </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxPanicAttacks" />
                        </td>
                        <td>Panic attacks </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxExcessiveHunger" />
                        </td>
                        <td>Excessive hunger </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxProneToInfection" />
                        </td>
                        <td>Prone to infection </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxLowBloodPressure" />
                        </td>
                        <td>Low blood pressure </td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxLightHeadedOnStandingUp" />
                        </td>
                        <td>Light headed on standing up </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxRacingMindPreventsSleep" />
                        </td>
                        <td>Racing mind prevents sleep </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxSluggishInAMSlowStarter" />
                        </td>
                        <td>Sluggish in AM - Slow starter </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxNeedSunglassesInBrightSunLight" />
                        </td>
                        <td>Need sunglasses in bright sun light </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxLowBackPainWorsensWithFatigueORStress" />
                        </td>
                        <td>Low back pain - Worsens with fatigue or stress </td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Metabolism</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxCanNotSkipMeals" />
                        </td>
                        <td>Can not skip meals </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxHeadacheWithMissedMeal" />
                        </td>
                        <td>Headache with missed meal </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxCravingsForSugarCarbs" />
                        </td>
                        <td>Cravings for sugar &amp; carbs </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxPeriodsOfLowEnergyRelievedWithFood" />
                        </td>
                        <td>Periods of low energy relieved with food </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxShakeWeakEpisodesEatingHelps" />
                        </td>
                        <td>Shake / Weak episodes - eating helps</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxJitteryIrritableEpisodesEatingHelps" />
                        </td>
                        <td>Jittery / Irritable episodes - eating helps </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxAlternatingBetweenHighAndLowMoods" />
                        </td>
                        <td>Alternating between high and low moods </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxAlternatingBetweenSluggishAndHighEnergy" />
                        </td>
                        <td>Alternating between sluggish and high energy</td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxHighBloodPressure" />
                        </td>
                        <td>High blood pressure </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxHighCholesterolTriglyceride" />
                        </td>
                        <td>High Cholesterol / Triglyceride</td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxMidAfternoonDrowsiness" />
                        </td>
                        <td>
                            <p>Mid-afternoon drowsiness</p>
                        </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxIncreasedFatAroundAbdomen" />
                        </td>
                        <td>Increased fat around abdomen</td>
                    </tr>
                    <tr class="regText">
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxProneToInflammationBursitis" />
                        </td>
                        <td>Prone to inflammation &amp; bursitis </td>
                        <td>


                            <asp:CheckBox runat="server" ID="CheckBoxFluidRetentionPuffyInExtremities" />
                        </td>
                        <td>Fluid retention &amp; puffy in extremities</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Cardio-Respiratory</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedAbilityAndDesireForExercise" /></td>
                        <td>Decreased ability and desire for exercise </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedStamina" /></td>
                        <td>Decreased stamina </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedEndurance" /></td>
                        <td>Decreased endurance </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxRunOutOfBreathSooner" /></td>
                        <td>Run out of breath sooner</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxEasilyExhaustedWithExercise" /></td>
                        <td>Easily exhausted with exercise</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Skin / Integumentary </strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxDrySkin" /></td>
                        <td>Dry skin</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxThinLips" /></td>
                        <td>Thin lips </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxGrayingHair" /></td>
                        <td>Graying hair </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSkinBlemishes" /></td>
                        <td>Skin blemishes </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxTendencyToBruising" /></td>
                        <td>Tendency to bruising</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxThinnedSkinHandsFaceArms" /></td>
                        <td>Thinned skin - hands, face, arms</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxThinningHairScalpArmpitsLegs" /></td>
                        <td>Thinning hair - scalp, armpits, legs </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxWrinklingSkinFaceNeckHandsArms" /></td>
                        <td>Wrinkling skin - face, neck, hands &amp; arms </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSaggingSkinUnderEyesArmsFaceBreasts" /></td>
                        <td>Sagging skin - under eyes, arms, face, breasts </td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Muscles / Joints </strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxOsteoporosis" /></td>
                        <td>Osteoporosis</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxAchesPains" /></td>
                        <td>Aches &amp; Pains </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLossOfStrength" /></td>
                        <td>Loss of strength</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBodyJoints" /></td>
                        <td>Body &amp; Joints </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxThinningMusclesButtocksArmsLegs" /></td>
                        <td>Thinning Muscles - buttocks, arms, legs </td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Neuro-cognitive</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxLossOfEsteem" /></td>
                        <td>Loss of esteem</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFeelingHopeLess" /></td>
                        <td>Feeling hopeless </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFeelingDefeated" /></td>
                        <td>Feeling defeated</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFeelingOfApathy" /></td>
                        <td>Feeling of apathy </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxLossOfConfidence" /></td>
                        <td>Loss of confidence </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxVisionDeteriorating" /></td>
                        <td>Vision deteriorating</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxHearingDeteriorating" /></td>
                        <td>Hearing deteriorating </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxMemoryDeteriorating" /></td>
                        <td>Memory deteriorating </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBalanceDeteriorating" /></td>
                        <td>Balance deteriorating </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxCoordinationDeteriorating" /></td>
                        <td>Coordination deteriorating </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSenseOfPowerLessNess" /></td>
                        <td>Sense of powerlessness </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedSenseOfWellBeing" /></td>
                        <td>Decreased sense of well being </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Gastrointestinal</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxIndigestionHyperacidity" /></td>
                        <td>Indigestion / Hyperacidity </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFeelFullFaster" /></td>
                        <td>Feel full faster </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSlowerDigestion" /></td>
                        <td>Slower digestion</td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxEatLessSmallerMeals" /></td>
                        <td>Eat less / Smaller meals </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxFullnessPersistsAfterMeals" /></td>
                        <td>Fullness persists after meals </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxBurpingOrBelchingAfterMeals" /></td>
                        <td>Burping or belching after meals </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDecreasedSenseOfTasteSmell" /></td>
                        <td>Decreased sense of taste / smell </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Diet</strong></td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Are you on any specific diet? (Please Specify) </td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxSpecificDiet" CssClass="FormField"></asp:TextBox>
                        </td>
                        <td>Is it successful?                

                            <asp:RadioButtonList runat="server" ID="RadionButtonListIsItSuccessfull" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0"></asp:ListItem>

                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <tr class="regText">
                        <td width="300">List which diets have been successful in the past: </td>
                        <td>
                            <asp:TextBox runat="server" ID="TextBoxSuccessfullDietInPast" TextMode="MultiLine" CssClass="border" Columns="40" Rows="3" MaxLength="1000"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Stress</strong></td>
                        <td width="200">&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Rate your current stress level:              

                            <asp:DropDownList runat="server" ID="DropDownListRateYourCurrentStressLevel" AppendDataBoundItems="true" CssClass="border">
                                <asp:ListItem Text="- Select -" Value="NA"></asp:ListItem>
                                <asp:ListItem Text="Extreme" Value="Extreme"></asp:ListItem>
                                <asp:ListItem Text="High" Value="High"></asp:ListItem>
                                <asp:ListItem Text="Medium" Value="Medium"></asp:ListItem>
                                <asp:ListItem Text="Low" Value="Low"></asp:ListItem>
                            </asp:DropDownList>

                        </td>
                        <td>How long has it been like this?             

                            <asp:TextBox runat="server" ID="TextBoxHowLongHasItBeenLikeThis" CssClass="FormField" MaxLength="50"> </asp:TextBox>

                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>You expect this to last a 
                            <asp:DropDownList runat="server" ID="DropDownListYouExpectThisToLast" AppendDataBoundItems="true" CssClass="border">
                                <asp:ListItem Text="- Select -" Value="NA"></asp:ListItem>
                                <asp:ListItem Value="Long" Text="Long"></asp:ListItem>
                                <asp:ListItem Value="Medium" Text="Medium"></asp:ListItem>
                                <asp:ListItem Value="Short" Text="Short"></asp:ListItem>
                            </asp:DropDownList>

                            period of time. </td>
                        <td>&nbsp;            </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td>Do you have a solution?
              

                            <asp:RadioButtonList runat="server" ID="RadionButtonListDoYouHaveSolution" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>

                        </td>
                        <td>Do you need help?
                            <asp:RadioButtonList runat="server" ID="RadionButtonListDoYouNeedHelp" AppendDataBoundItems="true" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Y" Value="1" Selected="True"></asp:ListItem>
                                <asp:ListItem Text="N" Value="0"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Exercise</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                        <td width="25">&nbsp;</td>
                        <td width="300">&nbsp;</td>
                        <td width="200">&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">

                            <asp:RadioButtonList runat="server" ID="RadioButtonListExerciseType" AppendDataBoundItems="true" CssClass="border" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Aerobic" Value="Aerobic"></asp:ListItem>
                                <asp:ListItem Text="Weights" Value="Weights"></asp:ListItem>
                                <asp:ListItem Text="Walking" Value="Walking"></asp:ListItem>
                                <asp:ListItem Text="Other" Value="Other">Other<input type="text" id="txtOther" name="TextBoxOther" class="FormField" /> </asp:ListItem>
                            </asp:RadioButtonList>
                            &nbsp;&nbsp;<%--<asp:TextBox runat="server" ID="TextBoxOther" CssClass="FormField"></asp:TextBox>--%>

                            <asp:HiddenField runat="server" ID="HiddenTextBoxOther" ClientIDMode="Static" />
                        </td>

                    </tr>
                    <tr class="regText">
                        <td colspan="4">How long are your workout sessions? </td>
                        <td colspan="4">
                            <asp:TextBox runat="server" ID="TextBoxWorkOutSession" CssClass="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td colspan="4">How many days / week? </td>
                        <td colspan="4">
                            <asp:TextBox runat="server" ID="TextBoxDaysWeek" CssClass="FormField" MaxLength="50"></asp:TextBox>

                        </td>
                        <td>&nbsp;</td>
                    </tr>
                </table>
                <br>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#FFFFFF">
                    <tr bgcolor="#CCCCCC" class="regText">
                        <td colspan="2"><strong>Sleep</strong></td>
                        <td width="25">&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>
                    <tr class="regText">
                        <td width="25">
                            <asp:CheckBox runat="server" ID="CheckBoxEarlyMorningWalking" />
                        </td>
                        <td>Early morning waking </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxTroubleGettingToSleep" /></td>
                        <td>Trouble getting to sleep - Racing mind at bedtime </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxSleepNotAsRestful" /></td>
                        <td>Sleep not as restful / Wake up not rested </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxTossAndTurnThoughNight" /></td>
                        <td>Toss and turn though night / Wake frequently through night </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxDaytimeDrowsinesssleepiness" /></td>
                        <td>Daytime drowsiness or sleepiness especially with periods of inactivity </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxWakeUpThroughTheNightFeeling" /></td>
                        <td>Wake up through the night feeling like you are choking or having a smothered sensation </td>
                    </tr>
                    <tr class="regText">
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxNoticedVeryHeavySnoring" /></td>
                        <td>Your partner has noticed very heavy snoring during sleep or that you stop breathing with snoring </td>
                        <td>
                            <asp:CheckBox runat="server" ID="CheckBoxWorkNightShift" /></td>
                        <td>Do you work night shift? If so, how many shifts per week?
                            <asp:TextBox runat="server" ID="TextBoxNightShift" CssClass="FormField" MaxLength="50"></asp:TextBox>
                        </td>
                    </tr>
                </table>

            </td>
        </tr>
    </table>

    <table width="100%" border="0" cellpadding="10" cellspacing="0">
        <tr>
            <td>
                <div align="center">
                    <asp:Button runat="server" CssClass="button" Text="Next Page ->" ID="ButtonSave" OnClick="ButtonSave_Click" OnClientClick="SetValue();" />
                </div>
            </td>
        </tr>
    </table>
</asp:Content>

