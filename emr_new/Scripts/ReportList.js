var check = false;
function ValiDateReportList(selectedRecords) {
    var Id = selectedRecords.Id;
    debugger;
    var Name = selectedRecords.ReportName;

    var ReportTypeId = $('#drpReportType option:selected').val();

    if (Name == "") {
        alert("You must enter Report Name.");
        return false;
    }
    else {
        CheckDuplicateReport(Id, Name, ReportTypeId)
        return check;
    }

}


function onCallbackError(message) {
    alert(message);
}
function OnInsertCommand() {
    alert('You have successfully added the record.');
    //grdReportList.refresh();
}
function OnUpdateCommand() {
    alert('You have successfully updated the record.');
    //grdReportList.refresh();
}

function CheckDuplicateReport(Id, Name, ReportTypeId) {
    debugger;
    var isFlag = false;
    url = 'ReportList.aspx/CheckDuplicateReport';
    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{ID:'" + Id + "', Name:'" + Name + "', ReportTypeId:'" + ReportTypeId+"'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {

                alert("Record is already exist with this type and you can not add the duplicate record.");
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

function ViewReport(Id)
{
    var ReportTypeId = $('#drpReportType option:selected').val();
        document.location = "Viewreport.aspx?Id=" + Id +
            "&ReportTypeId=" + ReportTypeId;
   

}