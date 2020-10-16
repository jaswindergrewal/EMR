<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AptOvuTab.aspx.cs" Inherits="AptOvuTab" MasterPageFile="~/DictationConsole/sub.master" EnableEventValidation="false" ViewStateEncryptionMode="Always" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="obout_ListBox" Namespace="Obout.ListBox" TagPrefix="obout" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <link href="DictationConsole/Resources/custom_scripts/row-reorder/row-reorder.css" rel="stylesheet"
        type="text/css" />
      
    <script type="text/javascript">
        window.onload = function () {
            grdNew.OnClientRowsDropped = saveRowsPosition;
            grdGoals.OnClientRowsDropped = saveRowsPosition1;
            reloadGrid();
        }

        function saveRowsPosition() {
            var positions = new Array();
            for (var i = 0; i < grdNew.Rows.length; i++) {
                if (grdNew.Rows[i] && grdNew.Rows[i].Cells) {
                    positions.push(i);
                }
            }

            positions.sort(function (a, b) { return a - b })

            var data = new Array();
            var j = 0;
            for (var i = 0; i < grdNew.Rows.length; i++) {
                if (grdNew.Rows[i] && grdNew.Rows[i].Cells) {
                    var item = grdNew.Rows[i].Cells['Symptom'].Value + '*' + positions[j];
                    grdNew.Rows[i].Cells['RowPosition'].Value = positions[j];
                    data.push(item);
                    j++;
                }
            }
            __doPostBack('custom', data);
            //PageMethods.SaveRowsPosition(data.join(','), reloadGrid, null);
        }

        function saveRowsPosition1() {
            var positions = new Array();
            for (var i = 0; i < grdGoals.Rows.length; i++) {
                if (grdGoals.Rows[i] && grdGoals.Rows[i].Cells) {
                    positions.push(i);
                }
            }

            positions.sort(function (a, b) { return a - b })

            var data = new Array();
            var j = 0;
            for (var i = 0; i < grdNew.Rows.length; i++) {
                if (grdGoals.Rows[i] && grdGoals.Rows[i].Cells) {
                    var item = grdGoals.Rows[i].Cells['Symptom'].Value + '*' + positions[j];
                    grdGoals.Rows[i].Cells['RowPosition'].Value = positions[j];
                    data.push(item);
                    j++;
                }
            }
            __doPostBack('custom1', data);
            //PageMethods.SaveRowsPosition(data.join(','), reloadGrid, null);
        }

        function reloadGrid() {
            // no need to refresh the Grid
            Grid1.refresh();
        }

        function OnBeforeInsertGoal(record) {

            var SelectedGoals = "";
            var lstBoxGoals;
            var lBox = document.getElementsByTagName("select");

            for (i = 0; i < lBox.length; i++) {
                if (lBox[i].id.indexOf("lstGoals") > 0) {
                    lstBoxCountry = lBox[i].id
                    break;
                }
            }
            var listGoals = document.getElementById(lstBoxCountry);

            for (i = 0; i < listGoals.length; i++) {
                if (listGoals.options[i].selected) {
                    SelectedGoals += listGoals.options[i].text + "#" + listGoals.options[i].value + "|";
                }
            }
            __doPostBack('GoalsGrid', SelectedGoals.substr(0, SelectedGoals.length - 1));


            return true;
        }

        function OnBeforeInsertSymptom(record) {

            var SelectedSymptoms = "";
            var lstBoxSymptoms;
            var lBox = document.getElementsByTagName("select");

            for (i = 0; i < lBox.length; i++) {
                if (lBox[i].id.indexOf("lstSymptoms") > 0) {
                    lstBoxSymptoms = lBox[i].id
                    break;
                }
            }
            var listSymptoms = document.getElementById(lstBoxSymptoms);

            for (i = 0; i < listSymptoms.length; i++) {
                if (listSymptoms.options[i].selected) {
                    SelectedSymptoms += listSymptoms.options[i].text + "#" + listSymptoms.options[i].value + "|";
                }
            }
            __doPostBack('SymptomsGrid', SelectedSymptoms.substr(0, SelectedSymptoms.length - 1));


            return true;
        }

        function reloadGrid() {
            grdGoals.refresh();
            grdNew.refresh();
        }




    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
      <script type="text/javascript" src="DictationConsole/Resources/custom_scripts/row-reorder/row-reorder.js"></script>
    <input type="hidden" id="Symptom" value="tester" />
    <table id="MainTable" runat="server" class="border" width="1050px">
        <tr class="border">
            <td valign="top">
                <b>Blood Draw:</b><br />
                <asp:CheckBox ID="cboFasting" runat="server" Text="Are you fasting?" EnableViewState="true" />&nbsp;
				<asp:TextBox ID="txtFasting" runat="server" Columns="10" CssClass="FormField" MaxLength="1000" /><br />
                <asp:CheckBox ID="cboCreams" runat="server" Text="Did you apply your creams and/or take your oral medications<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;at least 2 hours before you blood draw?" />&nbsp;
				<asp:TextBox ID="txtCreams" runat="server" Columns="10" CssClass="FormField" MaxLength="1000" /><br />
                <asp:CheckBox ID="cboPrege" runat="server" Text="Did you take your pregenolone?" />&nbsp;
				<asp:RadioButtonList ID="rdoAmPm" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                    <asp:ListItem Text="AM" Selected="False" Value="AM" />
                    <asp:ListItem Text="PM" Selected="True" Value="PM" />
                </asp:RadioButtonList>
                &nbsp;
				<asp:TextBox ID="txtPrege" runat="server" Columns="10" CssClass="FormField" MaxLength="50" />
                <br />
                
                <asp:CheckBox ID="cboDHEA" runat="server" Text="Did you take your DHEA?" />&nbsp;
				<asp:TextBox ID="txtDHEA" runat="server" Columns="10" CssClass="FormField" MaxLength="1000" />
                <br />
            </td>
            <td valign="top">
                <b>For Men Only:</b><br />
                <asp:CheckBox ID="cboMen1" runat="server" Text="Did you take Anastrozole or Arimidex the week before your test?" />&nbsp;
				<asp:TextBox ID="txtMen1" runat="server" Columns="7" CssClass="FormField" MaxLength="1000" /><br />
                <asp:CheckBox ID="cboMen2" runat="server" Text="Did you wash off your testosterone cream with soap and<br/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;a washcloth the night before your test?" />
                <asp:TextBox ID="txtMen2" runat="server" Columns="10" CssClass="FormField" MaxLength="1000" /><br />
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>

         
            <td valign="top" align="center" style="border-right: 1px solid #003366;">
                <obout:Grid ID="grdNew" runat="server" ShowLoadingMessage="false" AllowPaging="false"
                    PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="true"
                    AllowPageSizeSelection="false" Serialize="true" FolderStyle="grid_styles/style_7"
                    ViewStateMode="Enabled" CallbackMode="true" width="200px">
                    <Columns>
                        <obout:Column AllowEdit="true"  >
                            <TemplateSettings TemplateId="editButtonTemplate" EditTemplateId="updateButtonTemplate" />
                        </obout:Column>
                        <obout:Column DataField="Symptom" HeaderText="Symptoms" ReadOnly="false" Wrap="true">
                            <TemplateSettings TemplateId="SymptomTemplate" RowEditTemplateControlId="lstSymptoms"
                                RowEditTemplateControlPropertyName="SelectedIndexes"  />
                        </obout:Column>
                        <obout:Column DataField="RowPosition" Visible="false" ReadOnly="true" />
                        <obout:Column DataField="Resolved" Visible="false" ReadOnly="true" />
                        <obout:Column ID="Column1" DataField="dir" HeaderText="Change" runat="server" Width="125px"
                            ReadOnly="true">
                            <TemplateSettings TemplateId="TemplateChange" />
                        </obout:Column>
                        <obout:Column DataField="SymptomID" Visible="false" ReadOnly="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="OnBeforeInsertSymptom" />
                    <ScrollingSettings EnableVirtualScrolling="true" />
                    <LocalizationSettings NoRecordsText="No symptoms selected" />
                    <PagingSettings ShowRecordsCount="false" />
                    <TemplateSettings RowEditTemplateId="SymptomTemplateEdit" />
                    <Templates>
                        <obout:GridTemplate ID="SymptomTemplateEdit" runat="server">
                            <Template>
                                <asp:ListBox runat="server" ID="lstSymptoms" Width="100%" SelectionMode="Multiple"
                                    DataTextField="Symptom" DataValueField="SymptomID"
                                    Height="870px"></asp:ListBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="SymptomTemplate" runat="server">
                            <Template>
                                <%# ((string)Container.DataItem["resolved"]) == "True" ? "<del>" + Container.DataItem["Symptom"] + "</del>" : Container.DataItem["Symptom"] %>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="TemplateChange" runat="server">
                            <Template>
                                <asp:ImageButton ID="imgUp" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "up" ?  "images/ArrowUpHot.gif": "images/ArrowUpCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUp_Click" />
                                <asp:ImageButton ID="imgDown" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "down" ?  "images/ArrowDownHot.gif": "images/ArrowDownCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUp_Click" />
                                <asp:ImageButton ID="imgSide" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "side" ?  "images/ArrowSideHot.gif": "images/ArrowSideCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUp_Click" />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="editButtonTemplate" runat="server">
                            <Template>
                                <asp:Button ID="btnEdit" runat="server" CssClass="button" Text='<%# ((string)Container.DataItem["resolved"]) == "True" ? "UnResolve" : "Resolve" %>'
                                    OnClick="btnEdit_Click" ClientIDMode="Predictable" /><br />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="updateButtonTemplate" runat="server">
                            <Template>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" /><br />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Candel" />
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                </obout:Grid>
            </td>
            <td valign="top" align="center">
                <obout:Grid ID="grdGoals" runat="server" ShowLoadingMessage="false" AllowPaging="false"
                    PageSize="-1" AllowSorting="false" AutoGenerateColumns="false" AllowAddingRecords="true"
                    AllowPageSizeSelection="false" Serialize="true" FolderStyle="grid_styles/style_7"
                    ViewStateMode="Enabled" CallbackMode="true" Width="200px">
                    <Columns>
                        <obout:Column AllowEdit="true" >
                            <TemplateSettings TemplateId="editButtonTemplateGoal" EditTemplateId="updateButtonTemplateGoal" />
                        </obout:Column>
                        <obout:Column DataField="Symptom" HeaderText="Goals" ReadOnly="false" Wrap="true">
                            <TemplateSettings TemplateId="GoalTemplate" RowEditTemplateControlId="lstGoals" RowEditTemplateControlPropertyName="SelectedIndexes" />
                        </obout:Column>
                        <obout:Column DataField="RowPosition" Visible="false" ReadOnly="true" />
                        <obout:Column DataField="Resolved" Visible="false" ReadOnly="true" />
                        <obout:Column ID="Column2" DataField="dir" HeaderText="Change" runat="server" 
                            ReadOnly="true">
                            <TemplateSettings TemplateId="TemplateChangeGoal" />
                        </obout:Column>
                        <obout:Column DataField="SymptomID" Visible="false" ReadOnly="true" />
                    </Columns>
                    <ClientSideEvents OnBeforeClientInsert="OnBeforeInsertGoal" />
                    <ScrollingSettings EnableVirtualScrolling="true" />
                    <LocalizationSettings NoRecordsText="No goals selected" />
                    <PagingSettings ShowRecordsCount="false" />
                    <TemplateSettings RowEditTemplateId="GoalTemplateEdit" />
                    <Templates>
                        <obout:GridTemplate ID="GoalTemplateEdit" runat="server">
                            <Template>
                                <asp:ListBox runat="server" ID="lstGoals" Width="200" SelectionMode="Multiple"
                                    DataTextField="Symptom" DataValueField="SymptomID" Height="350px"></asp:ListBox>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="GoalTemplate" runat="server">
                            <Template>
                                <%# ((string)Container.DataItem["resolved"]) == "True" ? "<del>" + Container.DataItem["Symptom"] + "</del>" : Container.DataItem["Symptom"] %>
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="TemplateChangeGoal" runat="server">
                            <Template>
                                <asp:ImageButton ID="imgUp" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "up" ?  "images/ArrowUpHot.gif": "images/ArrowUpCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUpGoal_Click" />
                                <asp:ImageButton ID="imgDown" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "down" ?  "images/ArrowDownHot.gif": "images/ArrowDownCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUpGoal_Click" />
                                <asp:ImageButton ID="imgSide" runat="server" ImageUrl='<%# ((string)Container.DataItem["dir"]) == "side" ?  "images/ArrowSideHot.gif": "images/ArrowSideCold.gif" %>'
                                    ClientIDMode="Predictable" OnClick="imgUpGoal_Click" />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="editButtonTemplateGoal" runat="server">
                            <Template>
                                <asp:Button ID="btnEdit" runat="server" CssClass="button" Text='<%# ((string)Container.DataItem["resolved"]) == "True" ? "UnResolve" : "Resolve" %>'
                                    OnClick="btnEditGoal_Click" ClientIDMode="Predictable" /><br />
                            </Template>
                        </obout:GridTemplate>
                        <obout:GridTemplate ID="updateButtonTemplateGoal" runat="server">
                            <Template>
                                <asp:Button ID="btnUpdate" runat="server" CssClass="button" Text="Update" /><br />
                                <asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Candel" />
                            </Template>
                        </obout:GridTemplate>
                    </Templates>
                </obout:Grid>
                <table cellpadding="6" cellspacing="6">
                    <tr>
                        <td align="left">Are you realizing your goals in coming to the clinic?
							<asp:TextBox ID="txtGoals1" runat="server" Columns="10" CssClass="FormField" /><br />
                            <br />
                            Are you happy with your program?
							<asp:TextBox ID="txtGoals2" runat="server" Columns="10" CssClass="FormField" /><br />
                            <br />
                            Other areas of your health you would like to improve:<br />
                            <asp:TextBox ID="txtGoals3" runat="server" TextMode="MultiLine" Rows="5" Columns="40" CssClass="FormField" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Label ID="Label1" runat="server" CssClass="PageTitle" Text="Lifestyle information" /><br />
                <table cellpadding="6" cellspacing="6" width="530px" class="regText" style="background-image: ur(../images/export/beige_back.gif); border-style: solid; border-color: Gray; border-width: thin;"
                    frame="box" rules="all">
                    <tr>
                        <td valign="middle" width="20%" align="right">Water Intake
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:TextBox ID="txtWater" Columns="5" runat="server" CssClass="FormField" MaxLength="200" Width="44px" />
                            &nbsp;
							<asp:CheckBoxList ID="rdoWater" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="glasses/day " />
                                <asp:ListItem Text="oz/day " />
                                <asp:ListItem Text="qt/day " />
                                <asp:ListItem Text="liter/day " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtWaterComment" runat="server" Columns="15" CssClass="FormField" MaxLength="1000" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Water Source
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoWaterSource" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="Distilled " />
                                <asp:ListItem Text="Filtered" />
                                <asp:ListItem Text="Bottled " />
                                <asp:ListItem Text="Tap Water " />
                                <asp:ListItem Text="Well Water " />
                                
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtWaterSource" runat="server" Columns="15" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Exercise Frequency
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoExercise" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="1-2x/week " />
                                <asp:ListItem Text="3-5x/week " />
                                <asp:ListItem Text="6x or more/week " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtExercise" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Exercise Type
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="chkExcerciseType" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="Aerobic " />
                                <asp:ListItem Text="Resistance " />
                                <asp:ListItem Text="Stretching " />
                                <asp:ListItem Text="Sports " />
                                <asp:ListItem Text="Walking " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtExcerciseType" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Workout Length
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoWorkoutLength" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="< 20 min " />
                                <asp:ListItem Text="20--40 min " />
                                <asp:ListItem Text="40-60 min " />
                                <asp:ListItem Text="l> 60 min " />
                            </asp:CheckBoxList>

                            Comments:
							<asp:TextBox ID="txtWorkoutLength" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Sleep Quality
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoSleep" runat="server" RepeatDirection="Horizontal">
                                <asp:ListItem Text="Poor " />
                                <asp:ListItem Text="Fair " />
                                <asp:ListItem Text="Good " />
                                <asp:ListItem Text="Excellent " />
                            </asp:CheckBoxList>
                            Avg Hours/Night:
							<asp:TextBox ID="txtHours" runat="server" Columns="3" CssClass="FormField" MaxLength="20" Width="43px" />hours Mg. of melatonin you
							take:
							<asp:TextBox ID="txtMelatonin" runat="server" Columns="3" CssClass="FormField" MaxLength="20" Width="43px" />
                            <br />
                            Comments:
							<asp:TextBox ID="txtSleep" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Avg Servings of Raw fruits / Vegetables
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoFruitVeggie" runat="server" RepeatDirection="Horizontal"
                                RepeatLayout="Flow">
                                <asp:ListItem Text="< 3/day " />
                                <asp:ListItem Text="3-5/day " />
                                <asp:ListItem Text="5-8/day " />
                                <asp:ListItem Text="> 8/day " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtFruitVeggie" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Diet Quality
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoDiet" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Poor " />
                                <asp:ListItem Text="Fair " />
                                <asp:ListItem Text="Good " />
                                <asp:ListItem Text="Excellent " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtDiet" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="middle" align="right">Energy Level
                        </td>
                        <td nowrap="nowrap" valign="middle" align="left">
                            <asp:CheckBoxList ID="rdoEnergy" runat="server" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                <asp:ListItem Text="Poor " />
                                <asp:ListItem Text="Fair " />
                                <asp:ListItem Text="Good " />
                                <asp:ListItem Text="Excellent " />
                            </asp:CheckBoxList>
                            <br />
                            Comments:
							<asp:TextBox ID="txtEnergy" runat="server" Columns="10" CssClass="FormField" MaxLength="200" Width="180px" />
                        </td>
                    </tr>
                </table>
            </td>
            <td align="center" valign="top">
                <asp:Label ID="Label2" runat="server" CssClass="PageTitle" Text="Other information" /><br />
                <table cellpadding="4" cellspacing="4" class="regText" style="background-image: ur(../images/export/beige_back.gif); border-style: solid; border-color: Gray; border-width: thin;"
                    frame="box" rules="all">
                    <tr>
                        <td>
                            <asp:CheckBox ID="cboNewMeds" runat="server" Text="<b>New medications from other medical providers: </b>" />
                        </td>
                        <td>List
							<asp:TextBox ID="txt3rdPartyMeds" runat="server" Columns="15" CssClass="FormField" MaxLength="1000" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:CheckBox ID="cboSick" runat="server" Text="<b>New illnesses or injuries since last visit: </b>" /><br />
                        </td>
                        <td>List
							<asp:TextBox ID="txtSick" runat="server" Columns="15" CssClass="FormField" MaxLength="1000" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Date of last physical exam?
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPhysical" runat="server" Columns="10" CssClass="FormField" />
                            <cc1:CalendarExtender ID="CaltxtPhysical" runat="server" TargetControlID="txtPhysical" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Date of last PAP?
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtPAP" runat="server" Columns="10" CssClass="FormField" MaxLength="1000" /><br />
                            <cc1:CalendarExtender ID="CaltxtPAP" runat="server" TargetControlID="txtPAP" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Date of last Mammogram?
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtMammo" runat="server" Columns="10" CssClass="FormField" /><br />
                            <cc1:CalendarExtender ID="CaltxtMammo" runat="server" TargetControlID="txtMammo" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">Date of last Prostate Exam?
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtProstate" runat="server" Columns="10" CssClass="FormField" /><br />
                            <cc1:CalendarExtender ID="CaltxtProstate" runat="server" TargetControlID="txtProstate" />
                        </td>
                    </tr>
                <%--    <tr>
                        <td colspan="2">
                            Clinical Note :<br /><br />
                            <obout:Editor ID="ed" runat="server" AjaxWait="true" Appearance="custom" DefaultFontFamily="Times New Roman"
                                DefaultFontSize="14" Height="276" InitialCleanUp="false" ModeSwitch="false" PathPrefix="Editor_data/"
                                ShowQuickFormat="false" SpellCheckAutoComplete="true" Submit="false">
                                <buttons>
                                    <obout:Toggle Name="Bold" />
                                    <obout:Toggle Name="Italic" />
                                    <obout:Toggle Name="Underline" />
                                    <obout:Toggle Name="StrikeThrough" />
                                    <obout:HorizontalSeparator />
                                    <obout:Method Name="ClearStyles" />
                                    <obout:HorizontalSeparator />
                                    <obout:Select Name="FontName" />
                                    <obout:Select Name="FontSize" />
                                    <obout:HorizontalSeparator />
                                </buttons>
                            </obout:Editor>
                        </td>
                    </tr>--%>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Button ID="btnSave" runat="server" CssClass="button" Text="Save" OnClick="btnSave_Click" />&nbsp;
				<asp:Button ID="btnCancel" runat="server" CssClass="button" Text="Cancel" OnClick="btnCancel_Click" />
            </td>
        </tr>
    </table>

    <%--<asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db %>"
        SelectCommand="SELECT GoalItemID, DisplayName,DisplayOrder FROM GoalItem where GoalItemID not in (select GoalItemID from apt_Goals where AptID=@AptID) order by DisplayOrder"
        SelectCommandType="Text">
        <SelectParameters>
            <asp:Parameter DbType="Int32" DefaultValue="1" Name="AptID" />
        </SelectParameters>
    </asp:SqlDataSource>--%>
</asp:Content>
