var check = false;
function ValiDateCampaignType(selectedRecords) {
    var Id = selectedRecords.CampaignID;

    var Name = selectedRecords.CampaignType;

   
    if (Name == "") {
        alert("You must enter Campaign Type.");
        return false;
    }
    else {
        CheckDuplicateCampaignType(Id, Name)
        return check;
    }

}

function CheckDuplicateCampaignType(Id, Name) {

    var isFlag = false;
    url = 'Admin_CrmCampign.aspx/CheckDuplicateType';
    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{ID:'" + Id + "', Name:'" + Name + "'}",
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