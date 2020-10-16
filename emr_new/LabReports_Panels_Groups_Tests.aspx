<%@ Page Title="" Language="C#" MasterPageFile="~/site.master" AutoEventWireup="true" CodeFile="LabReports_Panels_Groups_Tests.aspx.cs" Inherits="LabReports_Panels_Groups_Tests" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>
<%@ Register Namespace="CustomPopups" TagPrefix="custom" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="Scripts/jquery.filter_input.js" type="text/javascript"></script>

    <script type="text/javascript">

        var panelID;
        var conditionID;
        var groupID;
        var TriggerIDMain;
        var TestIDMain;
        $(function () {

            // only numbers are allowed

            $('#<%= txtPanelSort.ClientID %>').filter_input({ regex: '[0-9]' });
            $("#<%=txtChartBottom.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtSortOrder.ClientID %>").filter_input({ regex: '[0-9]' });
            $("#<%=txtGroupHigh.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtGroupLow.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtFemHigh.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtFemLow.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtNormHigh.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtNormLow.ClientID %>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtFemNormHigh.ClientID%>").filter_input({ regex: '[0-9 .]' });
            $("#<%=txtFemNormLow.ClientID%>").filter_input({ regex: '[0-9 .]' });

        });
        //Hide the Pop on before loads

        $("document").ready(function () {
            $("#SemiTransparentBG").hide();
            $("#PanelPopUp").hide();
            $("#divGroups").hide();
            $("#TriggerPopUp").hide();
            $('#loading').hide();
        });


        //Show the Group list/Triggers for corresponding panel on the selection of radio button

        $(".PanelRadioClass input").live("click", function () {
            $('#TestDiv').hide();
            $("#GroupTestDiv").show();
            $("#GroupTestDiv").empty();
            $('#TriggerDiv').empty();
            $('#TriggerDiv').show();


            panelID = $(this).val();
            var postData = new Object();
            var recordCheckGroup = 0;
            var recordCheckTrigger = 0;
            postData.panelID = panelID;
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetGroupsTriggers",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objGroupList = response.d;
                    if (objGroupList != null) {
                        var groupID = "";
                        var groupName = "";
                        var triggerId = "";
                        var triggerName = "";

                        $("#GroupTestDiv").append('<table id="tblGroup" width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#E7D5B4" ><tbody></tbody></table>');
                        $("#tblGroup tbody").append('<tr> <td align="center"><input type="button" ID="btnAddGroups" Class="button" value="Add Group" onclick="AddGroups()" />&nbsp;<input type="button" ID="btnEditGroup" Class="button" value="Edit Group" onclick="EditGroups()" /> &nbsp;<input type="button" ID="btnDeleteGroup" Class="button" value="Delete" onclick="DeleteGroups()" /></td></tr>');

                        $("#TriggerDiv").append('<table id="tblTrigger" width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#E7D5B4" ><tbody></tbody></table>');
                        $('#tblTrigger tbody').append('<tr> <td align="center"><input type="button" ID="btnTriggerAdd" Class="button" value="Add Trigger" onclick="AddTriggerPopUp()" />&nbsp;<input type="button" ID="btnEditTrigger" Class="button" value="Edit Trigger" onclick="EditTriggerPopUp()" /> &nbsp;<input type="button" ID="btnDeleteTrigger" Class="button" value="Delete" onclick="DeleteTrigger()" /></td></tr>');

                        for (var i = 0 ; i < objGroupList.length; i++) {
                            if (objGroupList[i].GroupID != 0 && objGroupList[i].GroupName != null) {
                                recordCheckGroup = 1;
                                groupID = objGroupList[i].GroupID;
                                groupName = objGroupList[i].GroupName;
                                $("#tblGroup tbody").append("<tr bgcolor='#E7D5B4'><td id=" + groupID + " ><input id=RadioButton" + i + " onclick=RecordCheck(this) type=radio name=rdGroups value=" + groupID + " />&nbsp;" + groupName + "</td></tr>");

                            }
                            else if (objGroupList[i].TriggerID != 0 && objGroupList[i].TriggerName != null) {
                                recordCheckTrigger = 1;
                                triggerID = objGroupList[i].TriggerID;
                                triggerName = objGroupList[i].TriggerName;
                                $('#tblTrigger tbody').append("<tr bgcolor='#E7D5B4'><td id=" + triggerID + " ><input id=RadioButtonTrigger" + i + " onclick=GetCondition(this) type=radio name=rdTrigger value=" + triggerID + " />&nbsp;" + triggerName + "</td></tr>");
                            }
                        }

                        if (recordCheckGroup == 0) {
                            $("#tblGroup tbody").append("<tr bgcolor='#E7D5B4'><td >No record for groups</td></tr>");
                        }

                        if (recordCheckTrigger == 0) {
                            $('#tblTrigger tbody').append("<tr bgcolor='#E7D5B4'><td >No record for triggers</td></tr>");
                        }

                    }
                }
            });

        });

        //on click of add panel button open the panel popup
        function ShowPopUpPanel(AddEdit) {
            if (AddEdit == 'Add') {
                $("#btnAddPanelDetails").show();
                $("#btnUpdate").hide();
            }
            else {
                $("#btnAddPanelDetails").hide();
                $("#btnUpdate").show();

            }
            $("#PanelPopUp").show('slow');
            $("#SemiTransparentBG").show('slow');
            $("#MainContent_txtPanelName").focus();
        }

        //On clock of cancel button close the panel popup
        function ClosePanelPopUp() {
            $("#PanelPopUp").hide('slow');
            $("#SemiTransparentBG").hide('slow');
            $("#MainContent_txtPanelName").val('');
            var sortOrder = $("#MainContent_txtPanelSort").val('');
            var editorObject = $find("MainContent_txtPanelDescription");
            var panelDesc = editorObject.set_content('');
        }

        //Add panel Details 
        function AddPanel() {
            var panelName = $("#MainContent_txtPanelName").val();
            var sortOrder = $("#MainContent_txtPanelSort").val();
            var editorObject = $find("<%= txtPanelDescription.ClientID %>");
            var panelDesc = editorObject.get_content();

            if (panelName == "") {
                alert("Please enter the Panel name");
                $("#MainContent_txtPanelName").focus()
                return false;
            }
            else {
                var postData = new Object();
                postData.PanelName = panelName;
                postData.SortOrder = sortOrder;
                postData.PanelDescrip = panelDesc;

                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/AddPanels",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {

                            var message = confirm('Data sucessfully saved. Do you Want to add more?');
                            var count = $("#MainContent_PanelsRadioButtonList tbody tr").length;
                            count = count - 1;
                            $('#MainContent_PanelsRadioButtonList tbody').append('<tr><td><input id="MainContent_PanelsRadioButtonList_' + count + '" name="ctl00$MainContent$PanelsRadioButtonList" value="' + objTestList + '" type="radio"><label for="MainContent_PanelsRadioButtonList_' + count + '">' + panelName + '</label></td></tr>');

                            $("#MainContent_txtPanelName").val("");
                            $("#MainContent_txtPanelSort").val("");
                            editorObject.set_content('');
                            if (message != 0) {

                                $("#MainContent_txtPanelName").focus();

                            }
                            else {
                                ClosePanelPopUp();

                            }


                        }
                        else if (objTestList == 0) {
                            alert("Duplicate PanelName. Please insert new value");
                            $("#MainContent_txtPanelName").val("");
                            $("#MainContent_txtPanelName").focus();
                        }
                        else {
                            alert("Some error occur while saving the data");
                        }

                    },
                    error: function (response) {
                        alert("Some error occur while saving the data");
                    }
                });
            }
        }

        //function to delete the panel from list 
        function DeletePanel() {

            if (panelID > 0 && panelID != null) {
                var postData = new Object();
                postData.ID = panelID;
                postData.Name = 'Panel';

                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/DeleteLabData",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        alert("Data sucessfully deleted");
                        $("#MainContent_PanelsRadioButtonList tbody td input[value=" + panelID + "]").closest('tr').remove();
                        panelID = 0;
                        $('#TestDiv').hide();
                        $("#GroupTestDiv").hide();
                        $("#GroupTestDiv").empty();
                        $('#TriggerDiv').empty();
                        $('#TriggerDiv').hide();
                    },
                    error: function (response) {
                        alert("error");
                    }
                });
            }
            else {
                alert("Please select the panel that you want to delete");
            }
        }

        //function to get  the panel details
        function GetPanelDetail() {

            if (panelID > 0 && panelID != null) {
                var postData = new Object();
                postData.PanelID = panelID;
                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/GetPanelDetails",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTriggerList = response.d;

                        $("#MainContent_txtPanelName").val(objTriggerList.PanelName);
                        $("#MainContent_txtPanelSort").val(objTriggerList.SortOrder);
                        var editorObject = $find("<%= txtPanelDescription.ClientID %>");
                        editorObject.set_content(objTriggerList.PanelDescrip);
                        ShowPopUpPanel('Edit');
                    }
                });
            }
            else {
                alert("Please select the panel that you want to edit");
            }
        }

        //function to update the panel data
        function UpdatePanel() {
            var panelName = $("#MainContent_txtPanelName").val();
            var sortOrder = $("#MainContent_txtPanelSort").val();
            var editorObject = $find("<%= txtPanelDescription.ClientID %>");
            var panelDesc = editorObject.get_content();

            if (panelName == "") {
                alert("Please enter the Panel name");
                return false;
            }
            else {
                var postData = new Object();
                postData.PanelName = panelName;
                postData.SortOrder = sortOrder;
                postData.PanelDescrip = panelDesc;
                postData.PanelID = panelID;
                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/UpdatePanels",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            alert("Data sucessfully updated");

                            $("#MainContent_PanelsRadioButtonList tbody td input[value=" + panelID + "]").closest("td").find('label').html(panelName);
                            $("#MainContent_txtPanelName").val('');
                            $("#MainContent_txtPanelSort").val('');
                            var editorObject = $find("MainContent_txtPanelDescription");
                            var panelDesc = editorObject.set_content('');
                            ClosePanelPopUp();
                        }
                        else if (objTestList == 0) {
                            alert("Duplicate PanelName. Please insert new value");
                            $("#MainContent_txtPanelName").val("");
                        }
                        else {
                            alert("Some error occur while saving the data");
                        }
                    },


                    error: function (response) {
                        alert("Some error occur while saving the data");
                    }


                });
            }
        }

        //Get the test data on the basis of selected group
        function RecordCheck(ID) {
            var grpID = $(this).text();
            var postData = new Object();
            groupID = ID.value
            postData.groupID = ID.value;
            $("#TestDiv").empty();
            $("#TestDiv").show();
            $("#ConditionDiv").empty();
            $("#ConditionDiv").hide();
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetTests",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objTestList = response.d;
                    if (objTestList != null) {
                        var testID = "";
                        var testName = "";
                        $("#TestDiv").append('<table id ="tblTestMain" width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#E7D5B4" ><tbody></tbody></table>');
                        $("#tblTestMain tbody").append('<tr> <td align="center"><input type="button" ID="btnAddTest" Class="button" value="Add test to group" onclick="AddTest()" />&nbsp;<input type="button" ID="btnDeleteTest" Class="button" value="Delete" onclick="AddTestDetails(2)" /></td></tr>');
                        for (var i = 0 ; i < objTestList.length; i++) {
                            if (objTestList != objTestList[i].TestID) {
                                testID = objTestList[i].TestID;
                                testName = objTestList[i].TestName;
                                $("#tblTestMain tbody").append("<tr bgcolor='#E7D5B4'><td id=" + testID + " ><input id=RadioButton" + i + " onclick=RecordTest(this) type=radio name=rdTest value=" + testID + " />&nbsp;" + testName + "</td></tr>");
                            }
                        }
                        if (objTestList.length == 0) {
                            $("#tblTestMain tbody").append("<tr bgcolor='#E7D5B4'><td >No record for tests</td></tr>");
                        }
                    }
                }
            });
        }


        function RecordTest(ID) {

            TestIDMain = ID.value
        }


        //function to show  group popup
        function AddGroups() {

            $("#SemiTransparentBG").show('slow');
            $("#divGroups").show('slow');
            $("#btnUpdateGroups").hide();
            $("#btnAddGroup").show();
            $("#MainContent_txtGroupTitle").focus();
        }


        //function to hide group popup and clear all the fields
        function CloseGroupPopUp() {
            $("#SemiTransparentBG").hide('slow');
            $("#divGroups").hide('slow');
            blankGroupfields();

        }

        function blankGroupfields() {
            $("#MainContent_txtGroupTitle").val("");
            $("#MainContent_txtSortOrder").val("");
            $("#MainContent_txtChartBottom").val("");
            $("#MainContent_txtGroupHigh").val("");
            $("#MainContent_txtGroupLow").val("");
            $("#MainContent_txtFemHigh").val("");
            $("#MainContent_txtFemLow").val("");
            $("#MainContent_txtNormHigh").val("");
            $("#MainContent_txtNormLow").val("");
            $("#MainContent_txtFemNormHigh").val("");
            $("#MainContent_txtFemNormLow").val("");
            $('#MainContent_cboGraph').removeAttr('checked');
            var editorDesc = $find("<%= edDescrip.ClientID %>");
            var description = editorDesc.set_content('');
            var editorMaleHigh = $find("<%= edHigh.ClientID %>");
            var maleHighTxt = editorMaleHigh.set_content('');
            var editorMaleLow = $find("<%= edLow.ClientID %>");
            var maleLowTxt = editorMaleLow.set_content('');
            var editorMaleNormal = $find("<%= edNormal.ClientID %>");
            var maleNormalTxt = editorMaleNormal.set_content('');
            var editorFemHigh = $find("<%= FemHigh.ClientID %>");
            var femHighTxt = editorFemHigh.set_content('');
            var editorFemLow = $find("<%= FemLow.ClientID %>");
            var femLowTxt = editorFemLow.set_content('');
            var editorFemNormal = $find("<%= FemNormal.ClientID %>");
            var femNormalTxt = editorFemNormal.set_content('');

        }

        //function add lab report group details
        function AddGroupDetails() {
            var groupName = $("#MainContent_txtGroupTitle").val();
            var groupTitle = groupName;
            var sortOrder = $("#MainContent_txtSortOrder").val();
            var showGraph = $("#MainContent_cboGraph").is(':checked') ? 1 : 0;
            var chartBottom = $("#MainContent_txtChartBottom").val();
            var maleLongevityHigh = $("#MainContent_txtGroupHigh").val();
            var maleLongevityLow = $("#MainContent_txtGroupLow").val();
            var femaleLongevityHigh = $("#MainContent_txtFemHigh").val();
            var femaleLongevityLow = $("#MainContent_txtFemLow").val();
            var maleAcceptableHigh = $("#MainContent_txtNormHigh").val();
            var maleAcceptableLow = $("#MainContent_txtNormLow").val();
            var femaleAcceptableHigh = $("#MainContent_txtFemNormHigh").val();
            var femaleAcceptableLow = $("#MainContent_txtFemNormLow").val();
            var editorDesc = $find("<%= edDescrip.ClientID %>");
            var description = editorDesc.get_content();
            var editorMaleHigh = $find("<%= edHigh.ClientID %>");
            var maleHighTxt = editorMaleHigh.get_content();
            var editorMaleLow = $find("<%= edLow.ClientID %>");
            var maleLowTxt = editorMaleLow.get_content();
            var editorMaleNormal = $find("<%= edNormal.ClientID %>");
            var maleNormalTxt = editorMaleNormal.get_content();
            var editorFemHigh = $find("<%= FemHigh.ClientID %>");
            var femHighTxt = editorFemHigh.get_content();
            var editorFemLow = $find("<%= FemLow.ClientID %>");
            var femLowTxt = editorFemLow.get_content();
            var editorFemNormal = $find("<%= FemNormal.ClientID %>");
            var femNormalTxt = editorFemNormal.get_content();

            if (groupName == "" || groupTitle == "") {
                alert("Please enter the group name");
                $("#MainContent_txtGroupTitle").focus();
            }
            else if (sortOrder == "") {
                alert("Please enter sort order");
                $("#MainContent_txtSortOrder").focus();
            }

            else if (chartBottom == "") {
                alert("Please enter chart bottom");
                $("#MainContent_txtChartBottom").focus();
            }
            else if (maleLongevityHigh == "") {
                alert("Please enter maleLongevityHigh");
                $("#MainContent_txtGroupHigh").focus();
            }
            else if (maleLongevityLow == "") {
                alert("Please enter maleLongevityLow");
                $("#MainContent_txtGroupLow").focus();
            }

            else {
                var postData = new Object();
                postData.GroupName = groupName;
                postData.GroupTitle = groupTitle;
                postData.SortOrder = sortOrder;
                postData.ShowGraph = showGraph;
                postData.ChartBottom = chartBottom;
                postData.MaleLongevityHigh = maleLongevityHigh;
                postData.MaleLongevityLow = maleLongevityLow;
                postData.FemaleLongevityHigh = femaleLongevityHigh;
                postData.FemaleLongevityLow = femaleLongevityLow;
                postData.MaleAcceptableHigh = maleAcceptableHigh;
                postData.MaleAcceptableLow = maleAcceptableLow;
                postData.FemaleAcceptableHigh = femaleAcceptableHigh;
                postData.FemaleAcceptableLow = femaleAcceptableLow;
                postData.Description = description;
                postData.MaleHighTxt = maleHighTxt;
                postData.MaleLowTxt = maleLowTxt;
                postData.MaleNormalTxt = maleNormalTxt;
                postData.FemHighTxt = femHighTxt;
                postData.FemLowTxt = femLowTxt;
                postData.FemNormalTxt = femNormalTxt;
                postData.PanelID = panelID;
                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/AddGroups",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            var message = confirm('Data sucessfully saved. Do you Want to add more?');

                            var count = $("#GroupTestDiv tr").length;
                            count = count - 1;
                            $("#tblGroup").find("td:contains('No record for groups')").closest('tr').remove();
                            $("#tblGroup tbody").append("<tr bgcolor='#E7D5B4'><td id=" + objTestList + " ><input id=RadioButton" + count + "  type=radio onclick=RecordCheck(this); name=rdGroups value=" + objTestList + " />&nbsp;" + groupName + "</td></tr>");
                            if (message != 0) {

                                blankGroupfields();
                                $("#MainContent_txtGroupTitle").focus();

                            }
                            else {
                                CloseGroupPopUp();

                            }

                        }
                        else {
                            alert("Duplicate GroupTitle. Please insert new value");
                            $("#MainContent_txtGroupTitle").val("");
                            $("#MainContent_txtGroupTitle").focus();
                        }
                    },
                    error: function (response) {
                        alert("error");
                    }
                });
            }
        }

        //function to delete the Groups
        function DeleteGroups() {
            //if (!document.querySelector('input[name="rdGroups"]:checked')) {
            if (groupID < 0 && groupID == null) {
                alert("Please select one Group");
                return false;
            }
            var GroupID = groupID //document.querySelector('input[name="rdGroups"]:checked').value;
            var postData = new Object();
            postData.ID = GroupID;
            postData.Name = 'Group';

            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/DeleteLabData",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    alert("Data sucessfully deleted");
                    $("#GroupTestDiv input[value=" + GroupID + "]").closest('tr').remove();
                    var count = $("#GroupTestDiv tr").length;
                    count = count
                    if (count == 1) {
                        $("#tblGroup tbody").append("<tr bgcolor='#E7D5B4'><td >No record for Conditions</td></tr>");
                    }
                    $("#TestDiv").hide();
                },
                error: function (response) {
                    alert("error");
                }
            });
        }

        //function to edit the group details
        function EditGroups() {
            $("#btnAddGroup").hide();
            $("#btnUpdateGroups").show();
            var GroupID = groupID
            if (GroupID < 1 && GroupID == null) {
                alert("Please select one Group");
                return false;
            }
            var GroupID = groupID //document.querySelector('input[name="rdGroups"]:checked').value;
            var postData = new Object();
            postData.ID = GroupID;
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetGroupByID",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objGroupList = response.d;

                    $("#MainContent_txtGroupTitle").val(objGroupList.GroupTitle);
                    $("#MainContent_txtSortOrder").val(objGroupList.SortOrder);
                    $("#MainContent_txtChartBottom").val(objGroupList.ChartBottom);
                    $("#MainContent_txtGroupHigh").val(objGroupList.LongevityHighValue);
                    $("#MainContent_txtGroupLow").val(objGroupList.LongevityLowValue);
                    $("#MainContent_txtFemHigh").val(objGroupList.FemaleLongevityHigh);
                    $("#MainContent_txtFemLow").val(objGroupList.FemaleLongevityLow);
                    $("#MainContent_txtNormHigh").val(objGroupList.AcceptableHighValue);
                    $("#MainContent_txtNormLow").val(objGroupList.AcceptableLowValue);
                    $("#MainContent_txtFemNormHigh").val(objGroupList.FemaleAcceptableHigh);
                    $("#MainContent_txtFemNormLow").val(objGroupList.FemaleAcceptableLow);
                    if (objGroupList.ShowGraph == true) {
                        $('#MainContent_cboGraph').attr('checked', 'checked');
                    }
                    else {
                        $('#MainContent_cboGraph').removeAttr('checked');
                    }
                    var editorDesc = $find("<%= edDescrip.ClientID %>");
                    var description = editorDesc.set_content(objGroupList.Description);
                    var editorMaleHigh = $find("<%= edHigh.ClientID %>");
                    var maleHighTxt = editorMaleHigh.set_content(objGroupList.HighText);
                    var editorMaleLow = $find("<%= edLow.ClientID %>");
                    var maleLowTxt = editorMaleLow.set_content(objGroupList.LowText);
                    var editorMaleNormal = $find("<%= edNormal.ClientID %>");
                    var maleNormalTxt = editorMaleNormal.set_content(objGroupList.NormalText);
                    var editorFemHigh = $find("<%= FemHigh.ClientID %>");
                    var femHighTxt = editorFemHigh.set_content(objGroupList.HighFemale);
                    var editorFemLow = $find("<%= FemLow.ClientID %>");
                    var femLowTxt = editorFemLow.set_content(objGroupList.LowFemale);
                    var editorFemNormal = $find("<%= FemNormal.ClientID %>");
                    var femNormalTxt = editorFemNormal.set_content(objGroupList.NormalFemale);

                    $("#divGroups").show('slow');
                    $("#SemiTransparentBG").show('slow');
                    $("#MainContent_txtGroupTitle").focus();

                },
                error: function (response) {
                    alert("error");
                }
            });

        }

        //Update the details for group
        function UpdateGroups() {

            var GroupID = groupID;//document.querySelector('input[name="rdGroups"]:checked').value;
            var postData = new Object();
            var groupName = $("#MainContent_txtGroupTitle").val();
            var groupTitle = $("#MainContent_txtGroupTitle").val();
            var sortOrder = $("#MainContent_txtSortOrder").val();
            var showGraph = $("#MainContent_cboGraph").is(':checked') ? 1 : 0;
            var chartBottom = $("#MainContent_txtChartBottom").val();
            var maleLongevityHigh = $("#MainContent_txtGroupHigh").val();
            var maleLongevityLow = $("#MainContent_txtGroupLow").val();
            var femaleLongevityHigh = $("#MainContent_txtFemHigh").val();
            var femaleLongevityLow = $("#MainContent_txtFemLow").val();
            var maleAcceptableHigh = $("#MainContent_txtNormHigh").val();
            var maleAcceptableLow = $("#MainContent_txtNormLow").val();
            var femaleAcceptableHigh = $("#MainContent_txtFemNormHigh").val();
            var femaleAcceptableLow = $("#MainContent_txtFemNormLow").val();
            var editorDesc = $find("<%= edDescrip.ClientID %>");
            var description = editorDesc.get_content();
            var editorMaleHigh = $find("<%= edHigh.ClientID %>");
            var maleHighTxt = editorMaleHigh.get_content();
            var editorMaleLow = $find("<%= edLow.ClientID %>");
            var maleLowTxt = editorMaleLow.get_content();
            var editorMaleNormal = $find("<%= edNormal.ClientID %>");
            var maleNormalTxt = editorMaleNormal.get_content();
            var editorFemHigh = $find("<%= FemHigh.ClientID %>");
            var femHighTxt = editorFemHigh.get_content();
            var editorFemLow = $find("<%= FemLow.ClientID %>");
            var femLowTxt = editorFemLow.get_content();
            var editorFemNormal = $find("<%= FemNormal.ClientID %>");
            var femNormalTxt = editorFemNormal.get_content();

            if (groupName == "" || groupTitle == "") {
                alert("Please enter the group name");
                $("#MainContent_txtGroupTitle").focus();
            }
            else if (sortOrder == "") {
                alert("Please enter sort order");
                $("#MainContent_txtSortOrder").focus();
            }

            else if (chartBottom == "") {
                alert("Please enter chart bottom");
                $("#MainContent_txtChartBottom").focus();
            }
            else if (maleLongevityHigh == "") {
                alert("Please enter maleLongevityHigh");
                $("#MainContent_txtGroupHigh").focus();
            }
            else if (maleLongevityLow == "") {
                alert("Please enter maleLongevityLow");
                $("#MainContent_txtGroupLow").focus();
            }

            else {
                postData.ID = GroupID
                postData.GroupName = groupName;
                postData.GroupTitle = groupTitle;
                postData.SortOrder = sortOrder;
                postData.ShowGraph = showGraph;
                postData.ChartBottom = chartBottom;
                postData.MaleLongevityHigh = maleLongevityHigh;
                postData.MaleLongevityLow = maleLongevityLow;
                postData.FemaleLongevityHigh = femaleLongevityHigh;
                postData.FemaleLongevityLow = femaleLongevityLow;
                postData.MaleAcceptableHigh = maleAcceptableHigh;
                postData.MaleAcceptableLow = maleAcceptableLow;
                postData.FemaleAcceptableHigh = femaleAcceptableHigh;
                postData.FemaleAcceptableLow = femaleAcceptableLow;
                postData.Description = description;
                postData.MaleHighTxt = maleHighTxt;
                postData.MaleLowTxt = maleLowTxt;
                postData.MaleNormalTxt = maleNormalTxt;
                postData.FemHighTxt = femHighTxt;
                postData.FemLowTxt = femLowTxt;
                postData.FemNormalTxt = femNormalTxt;
                postData.PanelID = panelID

                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/UpdateGroups",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            alert("Data sucessfully updated");
                            //change by jas
                            $("#tblGroup tbody td input[value=" + GroupID + "]").closest('tr').find("td").html("<input type=radio onclick=RecordCheck(this); id=" + GroupID + " name=rdGroups value=" + GroupID + " > &nbsp" + groupTitle);
                            //$("#GroupTestDiv input[value=" + GroupID + "]").closest('tr').html("<td id=" + GroupID + " ><input type=radio  id=" + GroupID + " name=rdGroups value=" + GroupID + " > &nbsp" + groupTitle + "</td></tr>");
                            CloseGroupPopUp();

                        }
                        else {
                            alert("Duplicate GroupTitle. Please insert new value");
                            $("#MainContent_txtGroupTitle").val("");
                        }
                    },
                    error: function (response) {
                        alert("error");
                    }
                });
            }
        }

        //function to show trigger pop up
        function AddTriggerPopUp() {
            $("#SemiTransparentBG").show('slow');
            $("#TriggerPopUp").show('slow');
            $("#btnUpdateTrigger").hide();
            $("#btnAddTrigger").show();
            $("#MainContent_txtTriggerName").focus();
        }
        //function to hide trigger pop up and clear all fields
        function CloseTriggerPopUp() {
            $("#SemiTransparentBG").hide('slow');
            $("#TriggerPopUp").hide('slow');
            $("#MainContent_txtTriggerName").val("");
            var editortriggerDesc = $find("<%= txtTriggerDesc.ClientID %>");
            var triggerDesc = editortriggerDesc.set_content('');

        }
        //function to add trigger details
        function AddTriggerDetails() {
            $("#btnUpdateTrigger").hide();
            $("#btnAddTrigger").show();

            var triggerName = $("#MainContent_txtTriggerName").val();
            var triggerDesc = $("#MainContent_TxtTriggerDesc").val();
            var editortriggerDesc = $find("<%= txtTriggerDesc.ClientID %>");
            var triggerDesc = editortriggerDesc.get_content();
            if (triggerName == "" || triggerDesc == "") {
                alert("please fill all fields");
            }
            else {

                var postData = new Object();
                postData.triggerName = triggerName;
                postData.triggerDesc = triggerDesc;
                postData.panelID = panelID;
                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/AddTriggers",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {

                            var message = confirm('Data sucessfully saved. Do you Want to add more?');
                            $("#tblTrigger").find("td:contains('No record for triggers')").closest('tr').remove();
                            $("#tblTrigger tbody").append("<tr bgcolor='#E7D5B4'><td id=" + objTestList + " ><input id=" + objTestList + " type=radio name=rdTrigger value=" + objTestList + "  onclick=GetCondition(this) />&nbsp;" + triggerName + "</td></tr>");

                            if (message != 0) {

                                $("#MainContent_txtTriggerName").val("");
                                editortriggerDesc.set_content('');
                                $("#MainContent_txtTriggerName").focus();

                            }
                            else {
                                CloseTriggerPopUp();

                            }

                        }
                        else {
                            alert("duplicate trigger name.please fill new value");
                            $("#MainContent_txtTriggerName").val("");
                            $("#MainContent_txtTriggerName").focus();
                        }
                    }
                });
            }
        }


        //function to update trigger
        function UpdateTriggerDetails() {
            var triggerID = TriggerIDMain; //document.querySelector('input[name="rdTrigger"]:checked').value;
            var triggerName = $("#MainContent_txtTriggerName").val();
            var editortriggerDesc = $find("<%= txtTriggerDesc.ClientID %>");
            var triggerDesc = editortriggerDesc.get_content();

            if (triggerName == "" || triggerDesc == "") {
                alert("please fill all fields");
            }
            else {

                var postData = new Object();
                postData.ID = triggerID;
                postData.triggerName = triggerName;
                postData.triggerDesc = triggerDesc;
                postData.panelID = panelID;

                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/UpdateTriggers",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            alert("Data sucessfully updated");
                            $("#tblTrigger tbody td input[value=" + triggerID + "]").closest('tr').find("td").html("<input type=radio onclick=GetCondition(this); id=" + triggerID + " name=rdTrigger value=" + triggerID + " > &nbsp" + triggerName);
                            CloseTriggerPopUp();
                        }
                        else {
                            alert("duplicate trigger name.please fill new value");
                            $("#MainContent_txtTriggerName").val("");
                            $("#MainContent_txtTriggerName").focus();
                        }
                    }
                });
            }
        }

        //function to get trigger details
        function EditTriggerPopUp() {
            if (TriggerIDMain < 1 && TriggerIDMain == null) {
                //if (!document.querySelector('input[name="rdTrigger"]:checked')) {
                alert("Please select one Trigger");
                return false;
            }
            $("#btnUpdateTrigger").show();
            $("#btnAddTrigger").hide();
            var triggerID = TriggerIDMain;// document.querySelector('input[name="rdTrigger"]:checked').value;
            var postData = new Object();
            postData.ID = triggerID;
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetTriggerByID",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objGroupList = response.d;
                    var triggerName = "";
                    var triggerDesc = "";
                    triggerName = objGroupList.TriggerName;
                    triggerDesc = objGroupList.TriggerDescription;
                    $("#MainContent_txtTriggerName").val(triggerName);
                    var editortriggerDesc = $find("<%= txtTriggerDesc.ClientID %>");
                    var triggerDesc = editortriggerDesc.set_content(triggerDesc);
                },
                error: function (response) {
                    alert("error");
                }
            });
            $("#SemiTransparentBG").show('slow');
            $("#TriggerPopUp").show('slow');
        }


        //function to delete  trigger
        function DeleteTrigger() {
            if (TriggerIDMain < 1 && TriggerIDMain == null) {
                //if (!document.querySelector('input[name="rdTrigger"]:checked')) {
                alert("Please select one Trigger");
                return false;
            }
            var triggerID = TriggerIDMain; //document.querySelector('input[name="rdTrigger"]:checked').value;
            var postData = new Object();
            postData.ID = triggerID;
            postData.Name = 'Trigger';

            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/DeleteLabData",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    alert("Data sucessfully deleted");
                    $("#TriggerDiv input[value=" + triggerID + "]").closest('tr').remove();

                    var count = $("#TriggerDiv tr").length;
                    count = count
                    if (count == 1) {
                        $("#tblTrigger tbody").append("<tr bgcolor='#E7D5B4'><td >No record for triggers</td></tr>");
                    }
                    $("#ConditionDiv").hide();

                },
                error: function (response) {
                    alert("error");
                }
            });
        }

        //function to show the test that are not associated with any group and  are not hide.
        function AddTest() {

            var postData = new Object();
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetTestNOTINGroups",
                cache: false,
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objTestList = response.d;
                    if (objTestList != null) {
                        var TestID = "";
                        var TestName = "";

                        $("#TestPopup").append('<div class="InnerPopup" id="InnerpopTest"> <img src="images/close_icon.png"  onclick="CloseOpenDiv(' + "'" + 'TestPopup' + "'" + ');" class="closeDiv" /><p><strong>Test Information</strong></p></div>')
                        $("#InnerpopTest").append('<table width="100%" border="0" cellpadding="6" class="TableRow" cellspacing="0" id="tblTest"></table>');

                        for (var i = 0 ; i < objTestList.length; i++) {

                            TestID = objTestList[i].TestID;
                            TestName = objTestList[i].TestName;

                            $("#tblTest").append("<tr'><td id=" + TestID + " ><input id=Chk" + i + "  type=checkbox name=rdGroups value=" + TestID + " />&nbsp;" + TestName + "</td></tr>");

                        }


                        $("#tblTest").append(' <tr><td><br><input type="button" id="btnSaveTest" class="button" value="Add" onclick="AddTestDetails(0);" />&nbsp <input type="button" id="btnHideTest" class="button" value="Hide" onclick="AddTestDetails(1);" /> &nbsp;<input type="button" id="btnCloseTest" class="button" value="Close" onclick="CloseTestPopUp();" /></td></tr>  ');

                        $("#SemiTransparentBG").show('slow');
                        $("#TestPopup").show('slow');


                    }

                },
                error: function (response) {
                    alert("error");
                }
            });

        }

        //function delete add and hide tests
        function AddTestDetails(HideValue) {
            var values = "";
            if (HideValue == 0 || HideValue == 1) {
                values = $('#InnerpopTest input:checkbox:checked').map(function () {
                    return this.value;
                }).get().join(",");
            }
            else {
                if (TestIDMain < 1 && TestIDMain == null) {
                    //if (!document.querySelector('input[name="rdTest"]:checked')) {
                    alert("Please select one Test");
                    return false;
                }
                values = TestIDMain;//document.querySelector('input[name="rdTest"]:checked').value;
            }


            if (values == "" && values == null) {
                alert("Please select atleast one test");
                return false;
            }
            //var ID.value = document.querySelector('input[name="rdGroups"]:checked').value;
            var postData = new Object();
            postData.GroupID = groupID
            postData.TestIDs = values;
            postData.Hide = HideValue;

            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/InsertTestDetailsForGroup",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objTestList = response.d;
                    if (HideValue == 0) {
                        if (objTestList != null) {
                            var TestID = "";
                            var TestName = "";
                            alert("Data sucessfully added");
                            var count = $("#TestDiv tr").length;
                            count = count - 1;

                            $("#tblTestMain").find("td:contains('No record for tests')").closest('tr').remove();
                            for (var i = 0 ; i < objTestList.length; i++) {
                                var counttr = count + i;

                                $("#tblTestMain tbody").append("<tr bgcolor='#E7D5B4'><td id=" + objTestList[i].TestID + " ><input id=RadioButton" + counttr + " onclick=RecordTest(this) type=radio name=rdTest value=" + objTestList[i].TestID + " />&nbsp;" + objTestList[i].TestName + "</td></tr>");
                            }
                        }
                    }
                    if (HideValue == 2) {
                        if (objTestList != null) {
                            var TestID = "";
                            var TestName = "";
                            alert("Data sucessfully Deleted");

                            $("#TestDiv input[value=" + values + "]").closest('tr').remove();

                        }
                    }
                    CloseTestPopUp();

                },
                error: function (response) {
                    alert("error");
                }
            });

        }

        function CloseTestPopUp() {
            $("#SemiTransparentBG").hide('slow');
            $("#TestPopup").hide('slow');
            $("#TestPopup").empty();
        }

        //function to get condition by triggerid
        function GetCondition(ID) {
            var triggerID = $(this).text();
            var postData = new Object();
            postData.triggerID = ID.value;
            TriggerIDMain = ID.value;
            $("#TestDiv").empty();
            $("#TestDiv").hide();
            $("#ConditionDiv").empty();
            $("#ConditionDiv").show();
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetCondition",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objTestList = response.d;
                    if (objTestList != null) {
                        var conditionID = "";
                        var conditionName = "";
                        $("#ConditionDiv").append('<table id="tblCondition" width="100%" border="0" cellpadding="6" cellspacing="0" bgcolor="#E7D5B4" ><tbody></tbody></table>');
                        $("#tblCondition tbody").append('<tr><td><input type="button" ID="btnConditionAdd" Class="button" value="Add Condition" onclick="AddConditionPopUp()" />&nbsp;<input type="button" ID="btnEditCondition" Class="button" value="Edit Condition" onclick="EditConditionPopUp()" />&nbsp;<input type="button" ID="btnDeleteCondition" Class="button" value="Delete" onclick="DeleteCondition()" /></td></tr>');
                        for (var i = 0 ; i < objTestList.length; i++) {
                            if (objTestList != objTestList[i].ConditionID) {
                                conditionID = objTestList[i].ConditionID;
                                conditionName = objTestList[i].ConditionName;
                                $("#tblCondition tbody").append("<tr bgcolor='#E7D5B4'><td id=" + conditionID + " ><input id=RadioButton" + i + " onclick=RecordCondition(this)  type=radio name=rdCondition value=" + conditionID + " />&nbsp;" + conditionName + "</td></tr>");
                            }
                        }
                        if (objTestList.length == 0) {
                            $("#tblCondition tbody").append("<tr bgcolor='#E7D5B4'><td >No record for Conditions</td></tr>");
                        }

                        //$("#ConditionDiv").append('</table>');
                    }
                }
            });
        }
        //function to show condition pop up
        function AddConditionPopUp() {
            $("#SemiTransparentBG").show('slow');
            $("#ConditionPopUp").show('slow');
            $("#btnAddConditionDetail").show();
            $("#btnUpdateConditionDetail").hide();
        }
        //function to hide condition pop up
        function CloseConditionPopUp() {
            $("#SemiTransparentBG").hide('slow');
            $("#ConditionPopUp").hide('slow');
            ClearConditionFields();
        }

        function RecordCondition(ID) {
            conditionID = ID.value;
        }
        //function to add condition details
        function AddConditionDetails() {
            var conditionName = $("#MainContent_txtConditionName").val();
            var sex = $('#<%=lstSex.ClientID %> input:checked').val();
            var editorObject = $find("<%= ConditionDesc.ClientID %>");
            var conditionDesc = editorObject.get_content();
            if (conditionName == "" || conditionDesc == "") {
                alert("Please fill all fields");
                return false;
            }

            else {
                var triggerID = TriggerIDMain;// document.querySelector('input[name="rdTrigger"]:checked').value;
                var postData = new Object();
                postData.ConditionName = conditionName;
                postData.ConditionDescrip = conditionDesc;
                postData.Sex = sex;
                postData.TriggerID = triggerID;
                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/AddCondition",
                    data: JSON.stringify(postData),
                    cache: false,
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            alert("Data sucessfully saved");
                            $("#tblCondition").find("td:contains('No record for Conditions')").closest('tr').remove();
                            $("#tblCondition tbody").append("<tr bgcolor='#E7D5B4'><td id=" + objTestList + " ><input id=" + objTestList + " onclick=RecordCondition(this) type=radio name=rdCondition value=" + objTestList + " />&nbsp;" + conditionName + "</td></tr>");
                            ClearConditionFields();
                        }
                        else {
                            alert("duplicate condition name.please fill new value");
                            $("#MainContent_txtConditionName").val("");
                        }
                    },
                    error: function (response) {
                        alert("Some error occur while saving the data");
                    }
                });
            }
        }
        //function to clear all fields of condition popup
        function ClearConditionFields() {
            $("#MainContent_txtConditionName").val("");
            var editorObject = $find("<%= ConditionDesc.ClientID %>");
            var conditionDesc = editorObject.set_content('');
            $(':radio[value="' + "A" + '"]').attr('checked', 'checked');
        }


        //function to get condition details
        function EditConditionPopUp() {
            if (conditionID < 1 && conditionID == null) {
                alert("Please select one condition");
                return false;
            }

            $("#btnAddConditionDetail").hide();
            $("#btnUpdateConditionDetail").show();
            //conditionID = conditionID; //document.querySelector('input[name="rdCondition"]:checked').value;

            var postData = new Object();
            postData.ID = conditionID;
            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/GetConditionByID",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var objGroupList = response.d;
                    var conditionName = "";
                    var conditionDesc = "";
                    var sex = "";
                    conditionName = objGroupList.ConditionName;
                    conditionDesc = objGroupList.ConditionDescrip;
                    sex = objGroupList.Sex;
                    if (sex == "F") {
                        $(':radio[value="' + "F" + '"]').attr('checked', 'checked');
                    }
                    else if (sex == "M") {
                        $(':radio[value="' + "M" + '"]').attr('checked', 'checked');
                    }
                    else {
                        $(':radio[value="' + "A" + '"]').attr('checked', 'checked');
                    }
                    $("#MainContent_txtConditionName").val(conditionName);
                    var editorDesc = $find("<%= ConditionDesc.ClientID %>");
                    var conditionDesc = editorDesc.set_content(conditionDesc);
                },
                error: function (response) {
                    alert("error");
                }
            });
            $("#SemiTransparentBG").show('slow');
            $("#ConditionPopUp").show('slow');
        }
        //function to update condition details
        function UpdateConditionDetails() {
            //conditionID = document.querySelector('input[name="rdCondition"]:checked').value;
            var conditionName = $("#MainContent_txtConditionName").val();
            var sex = $('#<%=lstSex.ClientID %> input:checked').val();
            var editorObject = $find("<%= ConditionDesc.ClientID %>");
            var conditionDesc = editorObject.get_content();
            if (conditionName == "" || conditionDesc == "") {
                alert("please fill all fields");
            }
            else {

                var postData = new Object();
                postData.ID = conditionID;
                postData.conditionName = conditionName;
                postData.conditionDesc = conditionDesc;
                postData.sex = sex;
                postData.triggerID = triggerID;

                $.ajax({
                    type: "POST",
                    url: "LabReports_Panels_Groups_Tests.aspx/UpdateConditions",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var objTestList = response.d;
                        if (objTestList > 0) {
                            alert("Data sucessfully updated");
                            $("#tblCondition tbody td input[value=" + conditionID + "]").closest('tr').find("td").html("<input type=radio onclick=RecordCondition(this); id=" + conditionID + " name=rdCondition value=" + conditionID + " >&nbsp;" + conditionName);
                            ClearConditionFields();
                            $("#SemiTransparentBG").hide('slow');
                            $("#ConditionPopUp").hide('slow');
                        }
                        else {
                            alert("Duplicate condition name.please fill new value");
                            $("#MainContent_txtConditionName").val("");
                        }
                    }
                });
            }

        }
        //function to delete condition by id
        function DeleteCondition() {
            if (conditionID < 1 && conditionID == null) {
                alert("Please select one condition");
                return false;
            }
            //conditionID = document.querySelector('input[name="rdCondition"]:checked').value;
            var postData = new Object();
            postData.ID = conditionID;
            postData.Name = 'Condition';

            $.ajax({
                type: "POST",
                url: "LabReports_Panels_Groups_Tests.aspx/DeleteLabData",
                data: JSON.stringify(postData),
                cache: false,
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    alert("Data sucessfully deleted");
                    $("#ConditionDiv input[value=" + conditionID + "]").closest('tr').remove();
                    var count = $("#ConditionDiv tr").length;
                    count = count - 1
                    if (count == 1) {
                        $("#ConditionDiv").append("<tr bgcolor='#E7D5B4'><td >No record for Conditions</td></tr>");
                    }
                },
                error: function (response) {
                    alert("error");
                }
            });
        }

        function CloseOpenDiv(divName) {

            $("#SemiTransparentBG").hide('slow');
            var strDivName = '#' + divName;
            $(strDivName).hide('slow');
        }

    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <table width="800px" class="TableRow">
        <tr>
            <td>
                <asp:Button ID="btnReport" runat="server" Text="Report" CssClass="button" OnClick="btnReport_Click" /></td>
        </tr>

        <tr>

            <td style="width: 260px; vertical-align: top;">
                <table class="border">
                    <tr>
                        <td align="center">
                            <input type="button" id="btnAddPanel" class="button" value="Add Panel" onclick="ShowPopUpPanel('Add')" />
                            <input type="button" id="btnEditPanel" class="button" value="Edit Panel" onclick="GetPanelDetail()" />
                            <input type="button" id="btnDelete" class="button" value="Delete Panel" onclick="DeletePanel()" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:RadioButtonList ID="PanelsRadioButtonList" runat="server" name="rdPanel" class="PanelRadioClass"></asp:RadioButtonList>
                        </td>
                    </tr>
                </table>

            </td>
            <td style="width: 260px; vertical-align: top;">

                <div id="GroupTestDiv" style="width: 250px; display: none;" visible="false" bgcolor="#FFFFFF" class="border"></div>

                <div id="TriggerDiv" style="width: 250px; display: none;" visible="false" bgcolor="#FFFFFF" class="border"></div>

            </td>
            <td style="width: 260px; vertical-align: top;">
                <div id="TestDiv" style="width: 250px; display: none;" visible="false" bgcolor="#FFFFFF" class="border"></div>
                <div id="ConditionDiv" style="width: 250px; display: none;" visible="false" bgcolor="#FFFFFF" class="border"></div>
            </td>
        </tr>
    </table>



    <div id="loading" style="display: none;" visible="false" class="fadePanel ">
        <!-- this is the loading gif -->
        <img src="~/images/indicator.gif" alt="Loading" />
    </div>

    <div id="SemiTransparentBG" class="fadePanel " style="display: none;" visible="false"></div>

    <%-- For panel Div--%>
    <div id="PanelPopUp" class="Popup " style="display: none;" visible="false">
        <div class="InnerPopup">
            <img src="images/close_icon.png"  onclick="CloseOpenDiv('PanelPopUp');"  class="closeDiv" />
            <p><strong>Panel Information</strong></p>
            <p id="PopUpBody">
                <asp:Label ID="lblPanelName" runat="server" Text="Panel Name" />
                :&nbsp;
                <asp:TextBox ID="txtPanelName" runat="server" CssClass="FormFieldWhite" />
                <asp:Label ID="lblPanelSort" runat="server" Text="Sort" />
                <asp:TextBox ID="txtPanelSort" runat="server" Text="" CssClass="FormFieldWhite" /><br />
                <asp:Label ID="lblPnaelDescription" runat="server" Text="Panel Description" />
                :<br />

                <obout:Editor ID="txtPanelDescription" runat="server" Height="200px" Width="600">
                    <TopToolbar Appearance="Custom">
                        <AddButtons>
                            <obout:Bold />
                            <obout:Italic />
                            <obout:Underline />
                            <obout:StrikeThrough />
                            <obout:HorizontalSeparator />
                            <obout:FontName />
                            <obout:FontSize />
                            <obout:VerticalSeparator />
                            <obout:Undo />
                            <obout:Redo />
                            <obout:HorizontalSeparator />
                            <obout:PasteWord />
                            <obout:HorizontalSeparator />
                            <obout:JustifyLeft />
                            <obout:JustifyCenter />
                            <obout:JustifyRight />
                            <obout:JustifyFull />
                            <obout:HorizontalSeparator />
                            <obout:SpellCheck />

                        </AddButtons>
                    </TopToolbar>
                </obout:Editor>
            </p>
            <p>
                <input type="button" id="btnAddPanelDetails" class="button"
                    value="Add" onclick="AddPanel();" style="display: none;" visible="false" />
                <input type="button" id="btnUpdate" class="button"
                    value="Update" onclick="UpdatePanel();" style="display: none;" visible="false" />&nbsp;
                <input type="button" id="Close" class="button"
                    value="Close" onclick="ClosePanelPopUp();" />
            </p>
        </div>
    </div>
    <%-- End Panel Div--%>

    <div id="divGroups" class="Popup " style="display: none;" visible="false">
        <div class="InnerPopup">
            <img src="images/close_icon.png"  onclick="CloseOpenDiv('divGroups');"  class="closeDiv" />
            <p><strong>Group Information</strong></p>
            <table width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblGroupTitle" runat="server" Text="Group Title" CssClass="regText" />
                        <asp:TextBox ID="txtGroupTitle" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="lblSortOrder" runat="server" Text="Sort Order" CssClass="regText" />
                        <asp:TextBox ID="txtSortOrder" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:CheckBox ID="cboGraph" runat="server" Text="Show Graph" CssClass="regText" />
                    </td>
                    <td>
                        <asp:Label ID="lblChartBottom" runat="server" Text="Lowest Value" CssClass="regText" />
                        <asp:TextBox ID="txtChartBottom" runat="server" CssClass="FormFieldWhite" />
                    </td>
                </tr>
                <tr>

                    <td>
                        <asp:Label ID="lblHigh" runat="server" Text="Male Longevity Range Upper" CssClass="regText" />
                        <asp:TextBox ID="txtGroupHigh" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="lblLow" runat="server" Text="Male Longevity Range Lower" CssClass="regText" />
                        <asp:TextBox ID="txtGroupLow" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="Label8" runat="server" Text="Female Longevity Range Upper" CssClass="regText" />
                        <asp:TextBox ID="txtFemHigh" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="Label9" runat="server" Text="Female Longevity Range Lower" CssClass="regText" />
                        <asp:TextBox ID="txtFemLow" runat="server" CssClass="FormFieldWhite" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label10" runat="server" Text="Male Normal Range Upper" CssClass="regText" />
                        <asp:TextBox ID="txtNormHigh" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="Label11" runat="server" Text="Male Normal Range Lower" CssClass="regText" />
                        <asp:TextBox ID="txtNormLow" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="Label12" runat="server" Text="Female Normal Range Upper" CssClass="regText" />
                        <asp:TextBox ID="txtFemNormHigh" runat="server" CssClass="FormFieldWhite" />
                    </td>
                    <td>
                        <asp:Label ID="Label13" runat="server" Text="Female Normal Range Lower" CssClass="regText" />
                        <asp:TextBox ID="txtFemNormLow" runat="server" CssClass="FormFieldWhite" />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Label ID="lbl1" runat="server" Text="Description" CssClass="regText" />
                        <obout:Editor ID="edDescrip" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label1" runat="server" Text="Male High Text" CssClass="regText" />
                        <obout:Editor ID="edHigh" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label2" runat="server" Text="Male Low Text" CssClass="regText" />
                        <obout:Editor ID="edLow" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label3" runat="server" Text="Male Normal Text" CssClass="regText" />
                        <obout:Editor ID="edNormal" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label14" runat="server" Text="Female High Text" CssClass="regText" />
                        <obout:Editor ID="FemHigh" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label15" runat="server" Text="Female Low Text" CssClass="regText" />
                        <obout:Editor ID="FemLow" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                        <br />
                        <asp:Label ID="Label16" runat="server" Text="Female Normal Text" CssClass="regText" />
                        <obout:Editor ID="FemNormal" runat="server" Height="200px" Width="600">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />

                                </AddButtons>
                            </TopToolbar>
                        </obout:Editor>
                    </td>
                </tr>
                <tr>
                    <td>
                        <input type="button" id="btnAddGroup" class="button" value="Add" onclick="AddGroupDetails();" />&nbsp;
                                    <input type="button" id="btnUpdateGroups" class="button" value="Update" onclick="UpdateGroups();" />
                        <input type="button" id="btnClose" class="button" value="Close" onclick="CloseGroupPopUp();" />
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <%--Trigger Popup--%>
    <div id="TriggerPopUp" class="Popup " style="display: none;" visible="false">
        <div class="InnerPopup">
            <img src="images/close_icon.png"  onclick="CloseOpenDiv('TriggerPopUp');"  class="closeDiv" />
            <p><strong>Trigger Information</strong></p>
            <p id="P1">
                <asp:Label ID="lblTrigger" runat="server" Text="Trigger Name" />
                :&nbsp;
                <asp:TextBox ID="txtTriggerName" runat="server" CssClass="FormFieldWhite" /><br />
                <asp:Label ID="lblTriggerDesc" runat="server" Text="Trigger Description" />
                <obout:Editor ID="txtTriggerDesc" runat="server" Height="200px" Width="600">
                    <TopToolbar Appearance="Custom">
                        <AddButtons>
                            <obout:Bold />
                            <obout:Italic />
                            <obout:Underline />
                            <obout:StrikeThrough />
                            <obout:HorizontalSeparator />
                            <obout:FontName />
                            <obout:FontSize />
                            <obout:VerticalSeparator />
                            <obout:Undo />
                            <obout:Redo />
                            <obout:HorizontalSeparator />
                            <obout:PasteWord />
                            <obout:HorizontalSeparator />
                            <obout:JustifyLeft />
                            <obout:JustifyCenter />
                            <obout:JustifyRight />
                            <obout:JustifyFull />
                            <obout:HorizontalSeparator />
                            <obout:SpellCheck />

                        </AddButtons>
                    </TopToolbar>
                </obout:Editor>
                <br />
            </p>
            <p>
                <input type="button" id="btnAddTrigger" class="button" value="Add" onclick="AddTriggerDetails();" />&nbsp;
                <input type="button" id="btnUpdateTrigger" class="button" value="Update" onclick="UpdateTriggerDetails();" />&nbsp;
                <input type="button" id="btnCloseTrigger" class="button" value="Close" onclick="CloseTriggerPopUp();" />
            </p>
        </div>
    </div>

    <%--End trigger--%>

    <%--Test Popup--%>
    <div id="TestPopup" class="Popup " style="display: none;" visible="false">
        
    </div>
    <%--End Test--%>

    <%--Condition Popup--%>
    <div id="ConditionPopUp" class="Popup" style="display: none;" visible="false">
        <div class="InnerPopup">
            <img src="images/close_icon.png"  onclick="CloseOpenDiv('ConditionPopUp');"  class="closeDiv" />
            <p><strong>Condition Information</strong></p>
            <p id="P2">
                <asp:Label ID="lblConditionName" runat="server" Text="Condition Name" />
                :&nbsp;
                <asp:TextBox ID="txtConditionName" runat="server" CssClass="FormFieldWhite" />
                <br />
                <p>
                    <asp:Label ID="lblSex" runat="server" Text="Sex" />
                    <asp:RadioButtonList ID="lstSex" runat="server" Name="SexRadioList" RepeatDirection="Horizontal" CssClass="ConditionRadioBtnList">
                        <asp:ListItem Text="Male" Value="M" />
                        <asp:ListItem Text="Female" Value="F" />
                        <asp:ListItem Text="Both" Value="A" Selected="True" />
                    </asp:RadioButtonList>
                </p>
                <asp:Label ID="lblConditionDesc" runat="server" Text="Condition Description" />:<br />
                <obout:Editor ID="ConditionDesc" runat="server" Height="200px" Width="600">
                    <TopToolbar Appearance="Custom">
                        <AddButtons>
                            <obout:Bold />
                            <obout:Italic />
                            <obout:Underline />
                            <obout:StrikeThrough />
                            <obout:HorizontalSeparator />
                            <obout:FontName />
                            <obout:FontSize />
                            <obout:VerticalSeparator />
                            <obout:Undo />
                            <obout:Redo />
                            <obout:HorizontalSeparator />
                            <obout:PasteWord />
                            <obout:HorizontalSeparator />
                            <obout:JustifyLeft />
                            <obout:JustifyCenter />
                            <obout:JustifyRight />
                            <obout:JustifyFull />
                            <obout:HorizontalSeparator />
                            <obout:SpellCheck />
                        </AddButtons>
                    </TopToolbar>
                </obout:Editor>
                <br />
            </p>
            <p>
                <input type="button" id="btnAddConditionDetail" class="button" value="Add" onclick="AddConditionDetails();" />&nbsp;
                <input type="button" id="btnUpdateConditionDetail" class="button" value="Update" onclick="UpdateConditionDetails();" />&nbsp;
                <input type="button" id="btnCloseConditionPopUp" class="button" value="Close" onclick="CloseConditionPopUp();" />
            </p>
        </div>
    </div>
    <%--End Condition--%>
</asp:Content>

