
//function to validate data before insertion
function Validate(Add_Edit) {
    var IsFocus = '';
    var txtICDCode = document.getElementById('txtICDCode');
    var txtDiagnosis = document.getElementById('txtDiagnosis');
    var errString = "";

    if (txtICDCode.value == "") {
        if (errString == "")
        { IsFocus = txtICDCode }
        errString += "You must enter ICD Code.";
    }

    if (txtDiagnosis.value == "") {
        if (errString == "")
        { IsFocus = txtDiagnosis }
        errString += "\r\nYou must enter Diagnosis name.";
    }

    if (errString == "") {
        if (Add_Edit == 'Add')
            ProcessDiagnosis();
        if (Add_Edit == 'Edit')
            UpdateProcessDiagnosis();
    }
    else {
        alert(errString);
        IsFocus.focus();
        return false;
    }
}

//Insert data for diagnosis details
function ProcessDiagnosis() {

    var txtICDCode = document.getElementById('txtICDCode').value;
    var txtDiagnosis = document.getElementById('txtDiagnosis').value;
    var Viewable = $('#viewable_yn').is(':checked');

    var postData = new Object();
    postData.txtICDCode = txtICDCode;
    postData.txtDiagnosis = txtDiagnosis;
    postData.Viewable = Viewable;

    $.ajax({
        type: "POST",
        url: "admin_icd9_add.aspx/InsertDiagnosis",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert(" Duplicate data can't be inserted");

            }
            else {

                window.location.href = location.href;
                window.location.replace("admin_icd9_list.aspx");

            }

        }
    });

}

//Update Diagnosis Details
function UpdateProcessDiagnosis() {
    var DiagnosisId = $("input[id$=hdnDiagnosisID]").val();
    var txtICDCode = document.getElementById('txtICDCode').value;
    var txtDiagnosis = document.getElementById('txtDiagnosis').value;
    var Viewable = $("[id*='viewable_yn']").is(':checked');

    var postData = new Object();
    postData.txtICDCode = txtICDCode;
    postData.txtDiagnosis = txtDiagnosis;
    postData.Viewable = Viewable;
    postData.DiagnosisId = DiagnosisId;

    $.ajax({
        type: "POST",
        url: "admin_icd9_Update.aspx/UpdateDiagnosis",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert(" Duplicate data can't be inserted");

            }
            else {

                window.location.href = location.href;
                window.location.replace("admin_icd9_list.aspx");
            }
        }
    });
}

//list page functions start
var count = 0;
var currentPageNumber = 1;
$("document").ready(function () {

    BindGrid(1);
});

//Function bind the diagnosis list
function BindGrid(data) {
    var PageSize = $("input[id$=hdnPageSize]").val();
    var postData = new Object();
    postData.PageIndex = data;
    postData.PageSize = PageSize;

    $.ajax({
        type: "POST",
        url: "admin_icd9_list.aspx/BindDiagnosis",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var listObj = response.d;
            $("#DiagnosisList").find("tr:gt(0)").remove();
            for (var i = listObj.length - 1 ; i >= 0; i--) {

                $("#DiagnosisList tr:eq(0)").after("<tr><td><a href='admin_icd9_update.aspx?diagnosis_id=" + listObj[i].Diagnosis_ID + "'>" + listObj[i].ICD9_Code + "</a></td>" +
                    "<td><a id='a_" + listObj[i].Diagnosis_ID + "' href='admin_icd9_update.aspx?diagnosis_id=" + listObj[i].Diagnosis_ID + "'>" + listObj[i].Diag_Title + "</a></td>" +
                    "<td>" + listObj[i].Viewable_YN + "</td> </tr>");
                //added by jaswinder on 16th aug 2013 as when we add the character using html tag it not display the data.

                $("#DiagnosisList").find('a#a_' + listObj[i].Diagnosis_ID).text(listObj[i].Diag_Title);
                count = Number(listObj[i].RecordCount);
            }

            currentPageNumber = data;
            var totalPages = Math.ceil(count / PageSize);
            $("#MainContent_lblCurrentPage").text(currentPageNumber);
            $("#MainContent_lblTotalPages").text(totalPages);

            if (totalPages == 1) {
                $("#Previous").hide();
                $("#Next").hide();
                $("#MainContent_lblCurrentPage").hide();
                $("#MainContent_lblTotalPages").hide();
                $("#pagingtext").hide();

                return;
            }
            else {
                $("#Previous").show();
                $("#Next").show();
                $("#MainContent_lblCurrentPage").show();
                $("#MainContent_lblTotalPages").show();
                $("#pagingtext").show();
            }

            if (currentPageNumber == 1) {
                $("#Previous").attr("disabled", "disabled");
                if ($("#MainContent_lblCurrentPage") > 0) {
                    $("#Next").attr("disabled", "disabled");
                }
                else {
                    $("#Next").removeAttr("disabled");
                }
            }

            else {
                $("#Previous").removeAttr("disabled");
                if (currentPageNumber == $("#MainContent_lblTotalPages").text())
                    $("#Next").attr("disabled", "disabled");
                else
                    $("#Next").removeAttr("disabled");

            }
        }

    });
}

//Page change function
function ChangePage(data) {
    var process = $(data).val();

    var page = Number($("#MainContent_lblCurrentPage").text());
    if (process == "Next") {
        BindGrid(page + 1);
    }
    if (process == "Previous") {
        BindGrid(page - 1);
    }
}
//list page functions End