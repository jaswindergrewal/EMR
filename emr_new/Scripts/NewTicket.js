
function validate() {
   
    var mess = "";
    var isFocus = "";
    var isValid = true;
    var subject = $("#MainContent_txtSubject");
    var Dropdown = $('#MainContent_ddlAssign');
    var ddlAssignValue = $('#MainContent_ddlAssign :selected').val();
    var editor = oboutGetEditor('ed').getContent();
    
    if (ddlAssignValue == "0") {
        if (mess == "")
        { isFocus = Dropdown; isFocus.focus(); }
        $('#MainContent_ddlAssign').addClass("dropDownAddFousClass");
        $('#MainContent_ddlAssign').removeClass("dropDownRemoveFousClass");

        mess += "Please select assign to Department or Employee name.";
    }
    else {
        $('#MainContent_ddlAssign').removeClass("dropDownAddFousClass");
        $('#MainContent_ddlAssign').addClass("dropDownRemoveFousClass");
    }

    if (subject.val() == "") {
        if (mess == "")
        { isFocus = subject; isFocus.focus(); }
        mess += "\r\nPlease enter subject.";
    }
    if (editor == "") {
        mess += "\r\nPlease enter description.";
    }
    if (mess != "") {
        alert(mess);
        isValid = false;
    }
    return isValid;
}
$(document).ready(function ()
{
    BindAssignDropDown('Employee');

   

});



//bind the dropdown on the basis of selected type (employee or department)
function BindAssignDropDown(Assigntype) {

    var URL = "";
    if (Assigntype == "Employee") {
        URL = "NewTicket.aspx/BindEmployee";
    }
    else
        if (Assigntype == "Department") {
            URL = "NewTicket.aspx/BindDepartment";
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

