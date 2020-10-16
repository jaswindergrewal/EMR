var check = false;

//validate the packages before saving and updating
function ValiDatePackage(selectedRecords) {
    var Id = selectedRecords.RenewalID;
    var PackageName = selectedRecords.PackageName;
    var Duration = selectedRecords.Duration;
    if (PackageName == "") {
        alert("You must enter Package Name.");
        return false;
    }
    else if (Duration == "") {
        alert("You must enter duration .");
        return false;
    }
    
    else {
        CheckDuplicateType(Id, PackageName)
        return check;
    }
}

//Duplicate check for package name
function CheckDuplicateType(Id, PackageName) {

    var isFlag = false;
    url = 'Admin_RenewalPackages.aspx/CheckDuplicateType';
    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{ID:'" + Id + "', PackageName:'" + PackageName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("Record is already exist with this name and you can not add the duplicate record.");
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