function onPopulateControls_grdEvent(sender, selectedRecords) {
    document.getElementById('lblCampaignID').value = sender.ForeignKeys.CampaignID.Value;
    selectedRecords.CampaignID = sender.ForeignKeys.CampaignID.Value;

}

function ValidateEvent(selectedRecords) {

    var campaignID = document.getElementById('lblCampaignID').value;

    var eventID = document.getElementById('lblEventID').value;

    var eventDate = document.getElementById('txtEventDate').value;
    var isFlag = false;
    var txtEventName = document.getElementById('txtEventName');
    //Modified by : Rakesh Kumar
    var txtEventDate = document.getElementById('txtEventDate');
    //Modified by : Rakesh Kumar
    var txtVenue = document.getElementById('txtVenue');

    var errString = "";
    if (txtEventName.value == "") {
        errString += "You must enter an Event Name.";
    }
    if (txtEventDate.value == "") {
        errString += "\r\nYou must enter an Event Date and Time.";
    }
    if (txtVenue.value == "") {
        errString += "\r\nYou must enter a venue.";
    }

    var test = isValidDate(txtEventDate.value);
    if (test == false) {
        errString += "\r\nYou must enter a valid date.";
    }


    if (errString == "") {
        url = '../CRM/Manage.aspx/CheckDuplicateEventName';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            data: "{campaignID:'" + campaignID + "', eventID:'" + eventID + "', eventName:'" + txtEventName.value + "', eventDate:'" + eventDate + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != '') {
                    alert("Record is already exist with this event name and you can not add the duplicate record.");
                    isFlag = false;

                }
                else {
                    isFlag = true;
                }
            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
    }
    else {
        alert(errString);
    }
    if (isFlag == false) {
        return false;
    }
    else {
        return true;
    }

}


function Success() {
    grdAttend.refresh();
}

function Fail() {
    alert("Failed");
}

function cancelChanges() {
    skedWindow.Close();


}

function grdAttend_ClientEdit(record) {
    var ProspectID = document.getElementById('ProspectID');
    ProspectID.value = record;//record.ProspectID;
    skedWindow.Open();

}

function saveChanges() {
    skedWindow.Close();

    var ApptID = document.getElementById('ddlAppts').value;
    var ProspectID = document.getElementById('ProspectID').value;
    var ClinicID = $('#ddlClinic option:selected').text();
    var EventID = $('#ddlEvent option:selected').val();
    var StaffID = document.getElementById('StaffID').value;
    var data = ApptID + '|' + ProspectID + '|' + ClinicID + '|' + StaffID + '|' + EventID;
    var postData = new Object();
    postData.data = data;

    $.ajax({
        type: "POST",
        url: "../CRM/Manage.aspx/RecordEventAppt",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == false) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                alert("Data added successfully.");
                grdAttend.refresh();
            }

        },
        error: function (obj) {


            alert(obj.responseText);
        }
    });

}



var check = "";

function ValiDateManageProspect(record) {

    var txtFirstName = document.getElementById('txtFirstName');
    var txtLastName = document.getElementById('txtLastName');
    var ddlStatus = document.getElementById('ddlStatus');
    var txtEmail = document.getElementById('txtEmail');
    var txtMainPhone = document.getElementById('txtMainPhone');
    var EventID = record.EventID;
    var errString = "";
    if (txtFirstName.value == "") {
        errString += "Please enter first name.";
    }
    if (txtLastName.value == "") {
        errString += "\r\nPlease enter last name.";

    }
    if (txtMainPhone.value == "") {
        errString += "\r\nPlease enter phone number.";

    }
    //Commented by jaswinder as client not this as compulsuory field
    //if (EventID == "" && EventID < 1) {
    //    errString += "\r\nPlease select event.";
    //}

    if (txtEmail.value != "") {
        var filter = /^([a-zA-Z0-9_\.\-])+\@(([a-zA-Z0-9\-])+\.)+([a-zA-Z0-9]{2,4})+$/;

        if (!filter.test(txtEmail.value)) {
            errString += "\r\nPlease enter valid email address.";
        }
    }

    if (ddlStatus.value == "") {
        errString += "\r\nPlease select status.";
    }

    if (errString == "") {

        return true;
    }
    else {
        alert(errString);
        return false;
    }
}

// this method is using for check the duplicate Email address during Add/Edit the record.
// Tab: Manage Prospect
function ValidateProspect() {
    var emailAddress = document.getElementById('txtEmail').value;
    var prospectID = document.getElementById('txtID').value;

    if (emailAddress != "") {
        var isFlag = false;

        url = '../CRM/Manage.aspx/CheckDuplicateProspect';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            data: "{prospectID:'" + prospectID + "', emailAddress:'" + emailAddress + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != '') {
                    alert("Record is already exist with this email address and you can not add the duplicate record.");
                    isFlag = false;

                }
                else {
                    isFlag = true;
                }
            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
        if (isFlag == false) {
            check = false;
            return false;
        }
        else {
            check = true;
            return true;
        }
    }
    else {
        check = true;
        return true;
    }
}

var checkStatusManagement = "";


function ValiDateStatus(selectedRecords) {
    var statusId = selectedRecords.StatusID;

    var statusName = selectedRecords.StatusName;;

    if (statusName == "") {
        alert("You must enter Status Name.");
        return false;
    }
    else {
        CheckDuplicateStatuss(statusId, statusName)
        return checkStatusManagement;
    }

}


// this method is using for check the duplicate Email address during Add/Edit the record.
// Tab: Manage Prospect
function CheckDuplicateStatuss(statusId, statusName) {

    var isFlag = false;
    url = '../CRM/Manage.aspx/CheckDuplicateStatus';
    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{statusID:'" + statusId + "', statusName:'" + statusName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("Record is already exist with this status and you can not add the duplicate record.");
                isFlag = false;

            }
            else {
                isFlag = true;
            }
        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
    if (isFlag == false) {
        checkStatusManagement = false;
        return false;
    }
    else {
        checkStatusManagement = true;
        return true;
    }

}


// this method is using for check the blank value and duplicate marketing source name during Add/Edit the record.
// Tab: Status and Sources
function ValiDateMarketingSource(selectedRecords) {
    var isFlag = false;
    var marketingSourceName = selectedRecords.MarketingSourceName;
    var marketingSourceID = selectedRecords.MarketingSourceID;
    if (marketingSourceName == "") {
        alert("You must enter an Marketing source Name.");
        isFlag = false;
    }
    else {
        url = '../CRM/Manage.aspx/CheckDuplicateMarketingSource';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            data: "{marketingSourceID:'" + marketingSourceID + "', marketingSourceName:'" + marketingSourceName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != '') {
                    alert("Record is already exist with this marketing source name and you can not add the duplicate record.");
                    isFlag = false;

                }
                else {
                    isFlag = true;
                }
            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
    }
    if (isFlag == false) {
        return false;
    }
    else {
        return true;
    }

}

function ValiDateCampaign(selectedRecords) {
    var isFlag = false;
    var IsFocus = "";
    var campaignID = selectedRecords.CampaignID;
    var txtCampaignName = document.getElementById('txtCampaignName');
    var campaignName = txtCampaignName.value;
    var startdate = document.getElementById('txtStartDate');
    var txtStartDate = new Date(startdate.value);
    var enddate = document.getElementById('txtEndDate');
    var txtEndDate = new Date(enddate.value);
    var ddlCampaign = document.getElementById('cboCampaignType');
    var errString = "";

    if (txtCampaignName.value == "") {
        if (errString == "")
        { IsFocus = txtCampaignName }
        errString += "You must enter an Campaign Name.";
        isFlag = false;
    }

    if (ddlCampaign.value == "") {
        if (errString == "")
        { IsFocus = ddlCampaign }
        errString += "\r\nYou must enter Campaign type .";
    }

    if (startdate.value == "") {
        if (errString == "")
        { IsFocus = document.getElementById('txtStartDate') }
        errString += "\r\nYou must enter start date .";
    }

    if (enddate.value == "") {
        if (errString == "")
        { IsFocus = document.getElementById('txtEndDate') }
        errString += "\r\nYou must enter end date .";
    }

    if (txtStartDate > txtEndDate) {
        if (errString == "")
        { IsFocus = document.getElementById('txtStartDate') }
        errString += "\r\nYou must enter start date less than end date .";
        isFlag = false;
    }
    if (errString == "") {
        url = '../CRM/Manage.aspx/CheckDuplicateCampaignName';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            data: "{campaignID:'" + campaignID + "', campaignName:'" + campaignName + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != '') {
                    alert("Record is already exist with this campaign Name and you can not add the duplicate record.");
                    isFlag = false;

                }
                else {
                    isFlag = true;
                }
            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
    }
    else {
        alert(errString);
        IsFocus.focus();
    }


    if (isFlag == false) {

        return false;
    }
    else {
        return true;
    }

}

function CheckForCampaign() {

    var campaignID = $("[id*='ddlCampaign'] :selected").val();

    if (campaignID < 1) {
        alert("You select the campaign.");
        return false;
    }

    else {
        return true;
    }
}



function ValiDateActivity(selectedRecords) {
    var isFlag = false;
    var campaignID = $("[id*='ddlCampaign'] :selected").val();

    var marketingActivityID = document.getElementById('lblMarketingActivityID').value;


    var selectedSource;

    var txtSourceType = document.getElementById('txtSourceType');
    var txtStartDate1 = new Date(document.getElementById('txtStartDate1').value);
    var txtStart = (document.getElementById('txtStartDate1'));

    var txtEnd = (document.getElementById('txtEndDate1'));
    var txtEndDate1 = new Date(document.getElementById('txtEndDate1').value);
    var ddlSource1 = document.getElementById('ddlSource1');

    var errString = "";

    if (txtSourceType.value == "") {

        errString += "You must enter an Source type.";
    }
    if (txtStart.value == "") {
        errString += "\r\nYou must enter start date.";
    }

    if (txtEnd.value == "") {
        errString += "\r\nYou must enter End date.";
    }
    if (txtStart.value != "" && txtEnd.value != "") {
        if (txtStartDate1 > txtEndDate1) {
            errString += "\r\nYou must enter start date less than end date .";
        }
    }
    if (ddlSource1.value == "") {
        errString += "\r\nYou must enter Source type .";
    }
    else { selectedSource = ddlSource1.options[ddlSource1.selectedIndex].value }
    if (errString == "") {
        url = '../CRM/Manage.aspx/CheckDuplicateMarketingActivity';
        $.ajax({
            type: "POST",
            url: url,
            async: false,
            data: "{campaignID:'" + campaignID + "', marketingActivityID:'" + marketingActivityID + "', sourceType:'" + txtSourceType.value + "', startDate:'" + txtStart.value + "', endDate:'" + txtEnd.value + "', sourceID:'" + selectedSource + "'}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (result) {
                if (result.d != '') {
                    alert("Record is already exist with this combination of start date,end date,source type and source.So you can not add the duplicate record.");
                    isFlag = false;

                }
                else {
                    isFlag = true;
                }
            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
    }
    else {
        alert(errString);
    }
    if (isFlag == false) {

        return false;
    }
    else {
        return true;
    }
}


function SelectMarketingResourceDropDown(selectedRecords) {

    for (var i = 0; i < combo.options.length; i++) {
        combo._dropDownList.unselectItemByIndex(i);
    }
    if (selectedRecords.ProspectID == undefined) {
        alert("Please close the previous Edit record")
        return false;
    }
    var index = selectedRecords.ProspectID;
    var EventID = selectedRecords.EventID;
    //FillEventsall(EventID);
    $.ajax({
        url: "GetSelectedMarketingSource.aspx?id=" + index + "&Tab=1",
        success: function (response) {
            var mrkScr = response;
            if (mrkScr != null) {
                for (var i = 0; i < mrkScr.length; i++) {

                    for (var j = 0; j < combo.options.length; j++) {
                        if (mrkScr[i].MarketingSourceID == combo.options[j].value) {

                            combo._dropDownList.selectItemByIndex(j, true, false, true, true, true);
                        }

                    }
                }
            }
        },
        failure: function (xhr, res) {

        }

    });



}

function SelectRecord(sender, records) {

    var record = records[0];
    var url = record.CampaignID;


}

function SelectMarketingResourceDropDownTab2(selectedRecords) {

    for (var i = 0; i < cboSources.options.length; i++) {
        cboSources._dropDownList.unselectItemByIndex(i);
    }
    var index = selectedRecords.CampaignID;
    if (selectedRecords.CampaignID == undefined) {
        alert("Please close the previous Edit record")
        return false;
    }
    // var EventID = selectedRecords.EventID;
    //FillEventsall(EventID);

    $.ajax({
        url: "GetSelectedMarketingSource.aspx?id=" + index + "&Tab=2",
        success: function (response) {
            var bb = response.split(',');
            for (var i = 0; i < bb.length; i++) {

                for (var j = 0; j < cboSources.options.length; j++) {
                    if (bb[i] == cboSources.options[j].value) {

                        cboSources._dropDownList.selectItemByIndex(j, true, false, true, true, true);
                    }

                }
            }

        },
        failure: function (xhr, res) {

        }

    });



}


//Function to delete all the selected prospects
//Jaswinder 13th may 2013
function DeleteProspects() {
    if (grdProspect.SelectedRecords.length > 0) {


        message = confirm('Are you sure you want to delete items?');
        if (message == 0) {
            return false;
        }
        else {

            var prospectID = 0;
            var record;
            for (var i = 0; i < grdProspect.SelectedRecords.length; i++) {
                record = grdProspect.SelectedRecords[i];
                prospectID += "," + record.ProspectID;
            }
            var postData = new Object();
            postData.prospectID = prospectID;
            $.ajax({
                type: "POST",
                url: "../CRM/Manage.aspx/DeleteProspect",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (response) {
                    var res = response.d;
                    if (res == false) {
                        alert("Some problem occurred .");

                    }
                    else {
                        alert("Data deleted successfully .");
                        for (var i = 0; i < grdProspect.Rows.length; i++) {
                            try {
                                grdProspect.deselectRecord(i);
                            } catch (ex) { };
                        }
                    }
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });

            grdProspect.refresh();
        }
    }
    else {
        alert('Please select atleast one record');
        return false;
    }


}

//Function to open a new appointment window
function grdAttenendCreateappointment(record) {
    var ProspectID = document.getElementById('ProspectID');
    ProspectID.value = record; //record.ProspectID;
    AppointmentWindow.Open();


}

//function to load Providers
function BindProviders() {

    $.ajax
        ({
            type: "POST",
            url: "../CRM/Manage.aspx/Getproviders",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var Dropdown = $('#drpProviders');
                Dropdown.empty();

                $.each(response.d, function (index, item) {

                    Dropdown.append($("<option></option>").val(item.id).html(item.ProviderName));

                });
            },
            error: function () {
                alert("Failed to load data");
            }
        });
}

//function to load Providers
function BindAppointmentType() {

    $.ajax
        ({
            type: "POST",
            url: "../CRM/Manage.aspx/GetAppointmentTypes",
            data: "{}",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function (response) {

                var Dropdown = $('#drpAppointment');
                Dropdown.empty();

                $.each(response.d, function (index, item) {

                    Dropdown.append($("<option></option>").val(item.ID).html(item.TypeName));

                });
            },
            error: function () {
                alert("Failed to load data");
            }
        });
}

function setEndTime() {
    var txtEndTime = $('[id$=drpEndTime]');
    var txtStartTime = $('[id$=drpStartTime]');
    txtEndTime.val(parseInt(txtStartTime.val()) + 1);

}

//function to save the CRM new appointment and patient
function saveChangesforAppointment() {
    debugger;
    var ProviderID = document.getElementById('drpProviders').value;
    var ApptID = 0; //document.getElementById('drpAppointment').value;
    var ProspectID = document.getElementById('ProspectID').value;
    var ClinicID = $("[id$='ddlClinic'] :selected").text(); //$('[id$=ddlClinic]').text(); //document.getElementById('ddlClinic').text;//$('#ddlClinic option:selected').text();
    var EventID = document.getElementById('ddlEvent').value; //$('#ddlEvent option:selected').val();
    var StaffID = document.getElementById('StaffID').value;

    var txtStartDate = $('[id$=txtStartDateAppt]');
    var txtEndDate = $('[id$=txtEndDateAppt]');
    var startDate = new Date($('[id$=txtStartDateAppt]').val());
    var endDate = new Date($('[id$=txtEndDateAppt]').val());
    var today = new Date();
    var txtEndTime = $('#drpEndTime option:selected').text();
    var txtStartTime = $('#drpStartTime option:selected').text();

    var Timeexp = /^([0]?[1-9]|[1][0-2]):([0-5][0-9]|[1-9]) [APap][Mm]$/;
    var Timeexp2 = /^(0?[1-9]|1[012])(:[0-5]\d)[APap][mM]$/;
    var dateObjStart = new Date(txtStartDate.val() + ' ' + txtStartTime);
    var dateObjEnd = new Date(txtEndDate.val() + ' ' + txtEndTime);

    var dd, mm, yyyy;
    dd = today.getDate();
    mm = today.getMonth() + 1;
    yyyy = today.getFullYear();
    var CurrentDate = mm + "/" + dd + "/" + yyyy;
    var currdate1 = new Date(CurrentDate);
    var errString = "";
    if (txtStartDate.val() == '') {
        alert('Please enter Start Date!');
        txtStartDate.focus();
        return false;
    }
    else if (txtStartTime == "") {
        alert('Please enter start time!');

        return false;
    }

    else if (txtEndDate == '') {
        alert('Please enter End Date!');
        txtEndDate.focus();
        return false;
    }

    else if (txtEndTime == "") {
        alert('Please enter end time!');

        return false;
    }

    else if (startDate < currdate1) {
        alert('Start date cannot be less than current date!');
        txtStartDate.focus();
        return false;
    }

    else if (dateObjEnd <= dateObjStart) {
        alert('Appointment end datetime should be greater then start datetime !');
        txtEndDate.focus();
        return false;
    }

    AppointmentWindow.Close();

    var postData = new Object();
    postData.ProviderID = ProviderID;
    postData.StartDate = dateObjStart;
    postData.EndDate = dateObjEnd;

    $.ajax({
        type: "POST",
        url: "../CRM/Manage.aspx/CheckDuplicateAppointment",
        data: JSON.stringify(postData),
        dataType: "json",
        async: false,
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == "false") {
                alert("Some problem occurred.");

            }
            else {
                var message;
                if (res == "appoint for provider is already exits do you continue") {

                    message = confirm('appointment for provider is already exits. Do you want to continue');
                }

                if (message != 0) {
                    // insertData(ProviderID, ApptID, ProspectID, ClinicID, StaffID, EventID, dateObjStart, dateObjEnd);

                    postData = new Object();
                    postData.ProviderID = ProviderID;
                    postData.ApptID = ApptID;
                    postData.ProspectID = ProspectID;
                    postData.Clinic = ClinicID;
                    postData.StaffID = StaffID;
                    postData.EventID = EventID;
                    postData.StartDate = dateObjStart;
                    postData.EndDate = dateObjEnd;

                    $.ajax({
                        type: "POST",
                        url: "../CRM/Manage.aspx/SaveNewAppointmentandPatient",
                        data: JSON.stringify(postData),
                        dataType: "json",
                        async: false,
                        contentType: "application/json",
                        success: function (response) {
                            var res = response.d;
                            if (res == false) {
                                alert("Some problem occurred.");

                            }
                            else {


                                alert("Data added successfully.");

                                grdAttend.refresh();

                            }

                        },
                        error: function (obj) {


                            alert(obj.responseText);
                        }
                    });

                }
                else { return false; }
            }

        },
        error: function (obj) {


            alert(obj.responseText);
        }
    });

}

function insertData(ProviderID, ApptID, ProspectID, ClinicID, StaffID, EventID, dateObjStart, dateObjEnd) {

}

//Close the pop on Click of cancel button
function cancelChangesAppointment() {
    AppointmentWindow.Close();
}


//CRM to Create Followups 
//Jaswinder
function grdAttend_Followup(Prospectid) {
    debugger;
    var ProspectID = document.getElementById('ProspectID');
    ProspectID.value = Prospectid;
    var EventID = document.getElementById('ddlEvent').value;
    var StaffID = document.getElementById('StaffID').value;
    var ClinicID = $("[id$='ddlClinic'] :selected").text();
    var postData = new Object();

    postData.ProspectID = Prospectid;
    postData.Clinic = ClinicID;
    postData.StaffID = StaffID;
    postData.EventID = EventID;

    $.ajax({
        type: "POST",
        url: "../CRM/Manage.aspx/SaveNewFollowupandPatient",
        data: JSON.stringify(postData),
        dataType: "json",
        async: false,
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == false) {
                alert("Some problem occurred while saving data");

            }
            else {



                alert("Data added successfully.");

                grdAttend.refresh();

            }

        },
        error: function (obj) {


            alert(obj.responseText);
        }
    });
}

//Function to check wether to active or deactive campaign
//Function to open a new appointment window
function Active_DeactiveCampaign(record) {
    debugger;
    var Enabled = record.Enabled;
    var message = "";
    if (Enabled == "False") {
        message = confirm('Are you sure you want to activate this item?');
    }
    else {
        message = confirm('Are you sure you want to de-activate this item?');
    }

    if (message != 0) {
        return true;
    }
    else {
        return false;
    }


}

function Active_DeactiveEvent(sender, selectedRecords) {

    debugger;
    var Enabled = selectedRecords.Enabled;
    var message = "";
    if (Enabled == "False") {
        message = confirm('Are you sure you want to activate this item?');
    }
    else {
        message = confirm('Are you sure you want to de-activate this item?');
    }

    if (message != 0) {
        return true;
    }
    else {
        return false;
    }

}

function AddProspect() {
    var EventID = $('#ddlEvent option:selected').val();
    if (EventID > 0) {
        AddProspectWindow.Open();
        document.getElementById('hdnProspectEventId').value = EventID;
        $('#lblProspectEventName').text($('#ddlEvent option:selected').text());
    }
    else {
        alert("Please select the event!");
    }
}

function cancelProspect() {
    AddProspectWindow.Close();
    $('#txtProspectFirstName').val("");
    $('#txtProspectLastName').val("");
    $('#txtProspectMainPhone').val("");
    $('#txtProspectEmail').val("");
}

function saveProspect() {
    debugger;

    var EventID = $('#ddlEvent option:selected').val();;
    var StaffID = document.getElementById('StaffID').value;
    var FirstName = $('#txtProspectFirstName').val();
    var LastName = $('#txtProspectLastName').val();
    var Phone = $('#txtProspectMainPhone').val();
    var Email = $('#txtProspectEmail').val();
    var postData = new Object();

    postData.EventID = EventID;
    postData.StaffID = StaffID;
    postData.FirstName = FirstName;
    postData.LastName = LastName;
    postData.Phone = Phone;
    postData.Email = Email;

    if (FirstName == "") {
        alert('Enter first name');
        $('#txtProspectFirstName').focus();
        return false;
    }

    else if (LastName == "") {
        alert('Enter last name');
        $('#txtProspectLastName').focus();
        return false;
    }

    else if (Phone == "") {
        alert('Enter Phone');
        $('#txtProspectMainPhone').focus();
        return false;
    }

    $.ajax({
        type: "POST",
        url: "../CRM/Manage.aspx/saveProspect",
        data: JSON.stringify(postData),
        dataType: "json",
        async: false,
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == false) {
                alert("Some problem occurred while saving data");

            }
            else {



                alert("Data added successfully.");

                grdAttend.refresh();
                cancelProspect();
            }

        },
        error: function (obj) {


            alert(obj.responseText);
        }
    });
}








