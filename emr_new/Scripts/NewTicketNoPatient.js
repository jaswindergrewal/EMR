var StaffID;

//on document ready bind the dropdown
$(document).ready(function () {
    BindAssignDropDown('Employee');
    BindCategory();
    $(document).on('change', '#MainContent_ddlAssign', function () {
        BindCategory();
    });
    StaffID = $("#SessionTextBox").val();
});

//bind the dropdown on the basis of selected type (employee or department)
function BindAssignDropDown(Assigntype) {

    var URL = "";
    if (Assigntype == "Employee") {
        URL = "NewTicketNoPatient.aspx/BindEmployee";
    }
    else
        if (Assigntype == "Department") {
            URL = "NewTicketNoPatient.aspx/BindDepartment";
        }
    $.ajax({
        type: "POST",
        url: URL,
        data: "{}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {

            $("#MainContent_ddlAssign").empty();
            $("#MainContent_ddlAssign").append($('<option></option>').val(0).html("Select"));

            $.each(response.d, function (key, item) {
                if (Assigntype == "Employee") {
                    $("#MainContent_ddlAssign").append($('<option></option>').val(item.EmployeeID).html(item.EmployeeName));
                }
                else
                    if (Assigntype == "Department") {
                        $("#MainContent_ddlAssign").append($('<option></option>').val(item.DepartmentID).html(item.DepartmentName));
                    }
            });
        },
        error: function () {
            alert("Failed to load data");
        }
    });
}


//on change of ddlassign dropdown bind the category dropdown
function BindCategory() {
    var postData = new Object();
    var Type;
    var selectedValue;
    if ($("#MainContent_ddlAssign option:selected").length > 0) {
        Type = $('#MainContent_rdoDept input:checked').val();
        selectedValue = $('#MainContent_ddlAssign :selected').val();
    }
    else {
        Type = "";
        selectedValue = 0;
    }
    postData.ID = selectedValue;
    postData.Type = Type;

    $.ajax({
        type: "POST",
        url: 'NewTicketNoPatient.aspx/BindCategory',
        data: JSON.stringify(postData),
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        cache: false,
        success: function (response) {

            $("#MainContent_AptType").empty();
            $.each(response.d, function (index, item) {
                $("#MainContent_AptType").append($("<option></option>").val(item.FollowUp_Type_ID).html(item.FollowUp_Type_Desc));
            });
        },
        error: function () {
            alert("Failed to load data");
        }
    });
}

//added by surabhi 13 feb 2014 for validating and submitting data
$(document).on('click', '#SubmitTicketID', function () {

    var txtSubject = $("#MainContent_txtSubject");
    var editorObject = $find("MainContent_ed");
    var _content = editorObject.get_content();
    var ddlAssignValue = $('#MainContent_ddlAssign :selected').val();
    var AptType = $('#MainContent_AptType :selected').val();
    if (ddlAssignValue == 0) {
        alert('You have not assigned this ticket');
        $('#MainContent_ddlAssign').focus();
        return false;
    }
    else if (AptType == -1) {
        alert('You have not selected a category for this ticket');
        $('#MainContent_AptType').focus();
        return false;
    }
    else if (txtSubject.val() == '') {
        alert('Please enter subject');
        txtSubject.focus();
        return false;
    }
    else if (_content == '') {
        alert('Please enter the content');
        editorObject.focus();
        return false;
    }

    else {
        var severity = $('#MainContent_rdoSeverity input:checked').val();
        var Type = $('#MainContent_rdoDept input:checked').val();
        var selectedValue = $('#MainContent_ddlAssign :selected').val() + ' ' + Type;
        var dueDate = $("#MainContent_txtDueDate").val();
        var postData = new Object();
        postData.EnteredBy = StaffID;
        postData.body = _content;
        postData.followup_Cat = AptType;
        postData.severity = severity;
        postData.selectedValue = selectedValue;
        postData.subject = txtSubject.val();
        postData.dueDate = dueDate;
        $.ajax({
            type: "POST",
            url: "NewTicketNoPatient.aspx/SaveData",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                document.location.href = 'LandingPage.aspx';
            },
            error: function () {
                alert("Fail to save data.");
            }
        });
    }
});

