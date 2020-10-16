var editorObject;
var content; 
       //Function to validate data 
     
function validateData() {    
     var bool = false;
    editorObject = $find("MainContent_ed");
    content = editorObject.get_content();
    if (content == '') {
        alert('Please enter the content'); return false;
    }
    else {
        bool = true;
    }
    return bool;
}

/*Page admin_pending_blood draw aspx*/

//Method to to get the selected record on double click
function onDoubleClick(sender, iRecordIndex) {
    if (grdBlood.RecordInEditMode == null) {
        grdBlood.editRecord(iRecordIndex);
    }
    else {
        grdBlood.cancel_edit();
    }

}

//set the record values for the edit template
function grdBlood_ClientEdit(sender, record) {
    var cboFirstCall = document.getElementById('cboFirstCall');
    var cboSecondCall = document.getElementById('cboSecondCall');
    var cboFinalCall = document.getElementById('cboFinalCall');
    var cboFinalLetter = document.getElementById('cboFinalLetter');
    var txtFirstNote = document.getElementById('txtFirstNote');
    var txtSecondNote = document.getElementById('txtSecondNote');
    var txtFinalNote = document.getElementById('txtFinalNote');
    var txtFinalLetter = document.getElementById('txtFinalLetter');
    var lnkFup = document.getElementById("lnkFup");

    if (record.FirstCall === "True")
        cboFirstCall.checked = true;
    else
        cboFirstCall.checked = false;
    if (record.SecondCall === "True")
        cboSecondCall.checked = true;
    else
        cboSecondCall.checked = false;
    if (record.FinalCall === "True") cboFinalCall.checked = true;
    else
        cboFinalCall.checked = false;
    if (record.LetterSent === "True") cboFinalLetter.checked = true;
    else
        cboFinalLetter.checked = false;

    txtFirstNote.value = record.FirstNotes;
    txtSecondNote.value = record.SecondNotes;
    txtFinalNote.value = record.FinalNotes;
    txtFinalLetter.value = record.LetterNotes;

    lnkFup.href = "admin_contact_add_pendingfollowups.aspx?patientid=" + record.PatientID + "&FollowUp_ID=" + record.ID + "&MasterPage=site.master";

}

//update the edit template record
function grdBlood_ClientClick() {
    var cboFirstCall = document.getElementById('cboFirstCall');
    var cboSecondCall = document.getElementById('cboSecondCall');
    var cboFinalCall = document.getElementById('cboFinalCall');
    var cboFinalLetter = document.getElementById('cboFinalLetter');
    var txtFirstNote = document.getElementById('txtFirstNote');
    var txtSecondNote = document.getElementById('txtSecondNote');
    var txtFinalNote = document.getElementById('txtFinalNote');
    var txtFinalLetter = document.getElementById('txtFinalLetter');
    for (i = 0; i <= grdBlood.SelectedRecords.length - 1; i++) {
        var rec = grdBlood.SelectedRecords[i];
    }
    var data = rec.ID;
    data += '|' + cboFirstCall.checked;
    data += '|' + cboSecondCall.checked;
    data += '|' + cboFinalCall.checked;
    data += '|' + cboFinalLetter.checked;
    data += '|' + txtFirstNote.value;
    data += '|' + txtSecondNote.value;
    data += '|' + txtFinalNote.value;
    data += '|' + txtFinalLetter.value;
    grdBlood.deselectRecord(grdBlood.RecordInEditMode)

    grdBlood.cancel_edit();
    var postData = new Object();
    postData.data = data;

    $.ajax({
        type: "POST",
        url: "admin_pending_blooddraws.aspx/UpdateFollowup",
        async: false,
        data: JSON.stringify(postData),
        dataType: "json",
        cahce: false,
        contentType: "application/json",
        success: function (result) {
            if (result.d == 1) {
                alert("Record Updated.");
                grdBlood.refresh();
                isFlag = true;

            }
            else {
                isFlag = false;
            }
        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });

}




