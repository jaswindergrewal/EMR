//show the edit template on double click of the row.
function onDoubleClick(iRecordIndex) {
    grdATicket.editRecord(iRecordIndex);
}

//Validate the Data before insertion
function Validate(record) {
    var IsFocus = '';
    var txtSubject = document.getElementById('txtSubject');
    var txtAutoticketName = document.getElementById('txtAutoticketName');
    var txtBody = document.getElementById('txtBody');
    var txtFrequency = document.getElementById('txtFrequency');
    var txtStartDate = document.getElementById('txtStartDate');
    var ddlDept = document.getElementById("ddlDept");
    var ddlAssigned = document.getElementById("ddlAssigned");
    var ddlFtype = document.getElementById("ddlFtype");
    var ddlCreatedBy = document.getElementById("ddlCreatedBy");
    var ddlFreqType = document.getElementById("ddlFreqType");
    var errString = "";

    if (txtAutoticketName.value == "") {
        if (errString == "")
        { IsFocus = txtAutoticketName }
        errString += "\r\nPlease enter ticket name.";
    }

    if (txtSubject.value == "") {
        if (errString == "")
        { IsFocus = txtSubject }
        errString += "\r\nPlease enter subject name.";
    }

    if (txtBody.value == "") {
        if (errString == "")
        { IsFocus = txtBody }
        errString += "\r\nPlease enter body.";
    }

    if (ddlFtype.value == "") {
        if (errString == "")
        { IsFocus = ddlFtype }
        errString += "Please Select ticket type.";
    }

    if ((ddlDept.value == "" && ddlAssigned.value == "") || (ddlDept.value > 0 && ddlAssigned.value > 0)) {
        if (errString == "")
        { IsFocus = ddlDept }
        errString += "Please Select either departement assigned or group selected.";
    }
    else if (ddlDept.value != "" && ddlAssigned.value != "") {
        if (ddlDept.value < 1 && ddlAssigned.value < 1) {
            if (errString == "")
            { IsFocus = ddlDept }
            errString += "Please Select either departement assigned or group selected.";
        }
    }

    if (txtStartDate.value == "" ) {
        if (errString == "")
        { IsFocus = txtStartDate }
        errString += "\r\nPlease enter start date.";
    }
    
   if (ddlCreatedBy.value == "" && ddlCreatedBy < 1) {
        if (errString == "")
        { IsFocus = ddlCreatedBy }
        errString += "\r\nPlease select created by.";
   }

    if (txtFrequency.value == "") {
        if (errString == "")
        { IsFocus = txtFrequency }
        errString += "\r\nPlease select frequecy.";
    }

    if(!$.isNumeric(txtFrequency.value)) {
        if (errString == "")
        { IsFocus = txtFrequency }
        errString += "\r\nPlease enter numeric value.";
    }

    if (ddlFreqType.value == "") {
        if (errString == "")
        { IsFocus = ddlFreqType }
        errString += "\r\nPlease select frequecy type.";
    }

    if (errString == "") {
        return true;
    }
    else {
        alert(errString);
        IsFocus.focus();
        return false;
    }
}

