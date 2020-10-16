/*Method for prescriptions*/

var applyFilterTimeout = null;
var applyFilterHistTimeout = null;

//Function to disable the prescription grids
function disableGrid() {
    document.getElementById('grid-overlay').style.display = 'block';
    document.getElementById('overlay-supps').style.display = 'block';
    setTimeout('enableGrid()', 3000);
    setTimeout('grdScrips.cancel_edit()', 3000);
    setTimeout('grdSupps.cancel_edit()', 3000);
}

//Enable grid after perfoming the task
function enableGrid() {
    document.getElementById('grid-overlay').style.display = 'none';
    document.getElementById('overlay-supps').style.display = 'none';
}

//refresh grids on tab changes
function changeActiveTab(sender, e) {
    var activeTab = sender.get_activeTabIndex();

    if (activeTab == 0) grdScrips.refresh();
    if (activeTab == 2) grdSupps.refresh();
    if (activeTab == 1) grdHist.refresh();
    if (activeTab == 3) grdHistSupp.refresh();
}

//go to aptconsole page
function backToApt() {
    window.location = "apt_console.aspx?aptid=" + document.getElementById("txtAptID").value;
}

/*function for Prescription drug list */
//When click from the drug list box the drug name and startdate for that will be display pop box for adding prescription
function grdNew_ClientEdit(sender, record) {
    document.getElementById('txtDrugNameLocal').value = record.DrugName;
    document.getElementById('txtDrugID').value = record.DrugID;
    var currDate = new Date();
    document.getElementById("txtStartDate").value = currDate.toDateString();
    document.getElementById("txtSig").value = '';
    document.getElementById("txtDisp").value = '';
    document.getElementById("txtRefill").value = '';
    document.getElementById("txtEndDate").value = '';
    document.getElementById("txtNotes").value = '';
    NewDrug.Open();
    return false;
}

//When click from the Prescriped prescription list box then all the details will be fill in grid edit template text boxes.
function grdScrips_ClientEdit(sender, record) {
    var txtSigRefill = document.getElementById('txtSigRefill');
    var txtDispRefill = document.getElementById('txtDispRefill');
    var txtRefillsRefill = document.getElementById('txtRefillsRefill');
    var txtStartDateRefill = document.getElementById('txtStartDateRefill');
    var txtDateEndRefill = document.getElementById('txtDateEndRefill');
    var txtNotesRefill = document.getElementById('txtNotesRefill');
    var currDate = new Date();
    txtSigRefill.value = record.Drug_Dose;
    txtDispRefill.value = record.Drug_Dispenses;
    txtRefillsRefill.value = record.Drug_NumbRefills;
    txtStartDateRefill.value = currDate.toDateString();
    txtDateEndRefill.value = '';
    txtNotesRefill.value = record.Notes;
}

//Add the prescription refill in database
function grdSrips_ClientClick() {
    debugger;
    disableGrid();

    var txtSigRefill = document.getElementById('txtSigRefill').value;
    var txtDispRefill = document.getElementById('txtDispRefill').value;
    var txtRefillsRefill = document.getElementById('txtRefillsRefill').value;
    var txtStartDateRefill = document.getElementById('txtStartDateRefill').value;


    if (txtStartDateRefill == "") {
        alert("Please enter start date");
        document.getElementById('txtStartDateRefill').focus();
        return false;
    }
    var txtDateEndRefill = document.getElementById('txtDateEndRefill').value;
    var txtNotesRefill = document.getElementById('txtNotesRefill').value;
    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;
    var rec = grdScrips.SelectedRecords[0];
    try {
        if (rec.Supplement_yn == "True") {
            alert('You cannot refill a supplement here.  Use the Supplements Tab.');
            grdScrips.cancel_edit();
            return false;
        }
    }
    catch (err) {
    }

    var DrugID = rec.DrugID;
    var PrescriptionID = rec.PrescriptionID;
    grdScrips.deselectRecord(grdScrips.RecordInEditMode)

    grdScrips.cancel_edit();

    var postData = new Object();
    postData.txtSig = txtSigRefill;
    postData.txtDisp = txtDispRefill;
    postData.txtRefill = txtRefillsRefill;
    postData.txtStartDate = txtStartDateRefill;
    postData.txtEndDate = txtDateEndRefill;
    postData.txtNotes = txtNotesRefill;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;
    postData.DrugID = DrugID;
    postData.PrescriptionID = PrescriptionID;

    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionRefill",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                alert("Data added successfully.");
                grdScrips.refresh();
                grdNew.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
}

//Refresh the prescription grid and drug list
function refreshScrips(result) {
    // Deselect the selected records from the current page
    for (var i = 0; i < grdScrips.Rows.length; i++) {
        try {
            grdScrips.deselectRecord(i);
        } catch (ex) { };
    }
    grdScrips.refresh();
    grdNew.refresh();
}

function errorScrips(result) {
    alert('Error porcessing prescription');
}

//Closed the prescriped prescription
function btnClosePrescrip_ClientClick() {
    disableGrid();
    var rec = grdScrips.SelectedRecords[0];
    grdScrips.deselectRecord(grdScrips.RecordInEditMode)
    grdScrips.cancel_edit();

    var postData = new Object();
    postData.PrescriptionID = rec.PrescriptionID;
    postData.ElementID = 1;
    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/ClosePrescription",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while closing prescription");
                isFlag = false;
            }
            else {

                isFlag = true;
                alert("Prescription closed.");

                grdScrips.refresh();

            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
}


//Delete the prescribed prescription
function btnDeletePrescrip_ClientClick() {
    var message = confirm('Are you sure you want to delete this item?');
    if (message != 0) {
        disableGrid();
        var rec = grdScrips.SelectedRecords[0];

        grdScrips.cancel_edit();

        var postData = new Object();
        postData.PrescriptionID = rec.PrescriptionID;
        postData.ElementID = 1;
        $.ajax({
            type: "POST",
            url: "PresrcriptionList.aspx/DeletePrescription",
            data: JSON.stringify(postData),
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var res = response.d;
                if (res == 0) {
                    alert("Some problem occurred while deleting prescription");
                    isFlag = false;
                }
                else {

                    isFlag = true;
                    alert("Prescription deleted.");
                    grdScrips.refresh();

                }

            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });
    }
    else {
        return false;
    }
}

//Add prescription in a prescription grid this function firs when we click on drug list window
function saveChanges() {
    var isFlag = false;
    disableGrid();

    var txtDrugNameLocal = document.getElementById('txtDrugNameLocal').value;
    var txtStartDate = document.getElementById('txtStartDate').value;
    if (txtDrugNameLocal == "") {
        alert("Please enter drug name.");
        document.getElementById('txtNewDrugName').focus();
        return false;
    }
    else if (txtStartDate == "") {
        alert("Please enter start date");
        document.getElementById('txtStartDateAdd').focus();
        return false;
    }
    var txtSig = document.getElementById('txtSig').value;
    var txtDisp = document.getElementById('txtDisp').value;
    var txtRefill = document.getElementById('txtRefill').value;
    var txtStartDate = document.getElementById('txtStartDate').value;
    var txtEndDate = document.getElementById('txtEndDate').value;
    var txtNotes = document.getElementById('txtNotes').value;
    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;
    var chkThirdParty = document.getElementById('chkThirdParty');
    var chkThirdPartyAdd;
    if (chkThirdParty.checked) {
        chkThirdPartyAdd = true;
    }
    else { chkThirdPartyAdd = false; }
    var postData = new Object();
    postData.txtDrugNameLocal = txtDrugNameLocal;
    postData.txtSig = txtSig;
    postData.txtDisp = txtDisp;
    postData.txtRefill = txtRefill;
    postData.txtStartDate = txtStartDate;
    postData.txtEndDate = txtEndDate;
    postData.txtNotes = txtNotes;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;
    postData.chkThirdPartyAdd = chkThirdPartyAdd;


    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionDrug",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                NewDrug.Close();
                alert("Data added successfully.");
                grdScrips.refresh();
                grdNew.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });

}

//close new drug window
function cancelChanges() {
    NewDrug.Close();
}

//Add drug name and the prescription in a prescription grid this function firs when we click on drug list window
function saveChangesAdd() {
    var isFlag = false;
    disableGrid();
    var txtDrugNameLocal = document.getElementById('txtNewDrugName').value;
    var txtStartDate = document.getElementById('txtStartDateAdd').value;
    if (txtDrugNameLocal == "") {
        alert("Please enter drug name.");
        document.getElementById('txtNewDrugName').focus();
        return false;
    }
    else if (txtStartDate == "") {
        alert("Please enter start date");
        document.getElementById('txtStartDateAdd').focus();
        return false;
    }
    var txtSig = document.getElementById('txtSigAdd').value;
    var txtDisp = document.getElementById('txtDispAdd').value;
    var txtRefill = document.getElementById('txtRefillAdd').value;
    var txtStartDate = document.getElementById('txtStartDateAdd').value;
    var txtEndDate = document.getElementById('txtEndDateAdd').value;
    var txtNotes = document.getElementById('txtNotesAdd').value;
    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;
    var chkThirdParty = document.getElementById('chkThirdPartyAdd');
    var chkThirdPartyAdd;
    if (chkThirdParty.checked) {
        chkThirdPartyAdd = true;
    }
    else { chkThirdPartyAdd = false; }
    var postData = new Object();
    postData.txtDrugNameLocal = txtDrugNameLocal;
    postData.txtSig = txtSig;
    postData.txtDisp = txtDisp;
    postData.txtRefill = txtRefill;
    postData.txtStartDate = txtStartDate;
    postData.txtEndDate = txtEndDate;
    postData.txtNotes = txtNotes;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;
    postData.chkThirdPartyAdd = chkThirdPartyAdd;


    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionDrug",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                AddDrugWindow.Close();
                alert("Data added successfully.");
                grdScrips.refresh();
                grdNew.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });

}

//Close Add drug window
function cancelChangesAdd() {
    AddDrugWindow.Close();
}

/********************************************************************************************************************/

/*function for Suppliment list */
//When click from the suppliment list box then suppliment name and startdate for that will be display in pop box for adding suppliments
function grdNewSupp_ClientEdit(sender, record) {
    document.getElementById('txtDrugNameLocalSupp').value = record.ProductName;
    document.getElementById('txtDrugIDSupp').value = record.ProductID;
    var currDate = new Date();
    document.getElementById("txtStartDateSupp").value = currDate.toDateString();
    document.getElementById("txtSigSupp").value = '';
    document.getElementById("txtDispSupp").value = '';
    document.getElementById("txtRefillSupp").value = '';
    document.getElementById("txtEndDateSupp").value = '';
    document.getElementById("txtNotesSupp").value = '';
    newSupp.Open();
    return false;
}

//When click from the Prescriped suppliment list box then all the details will be fill in grid edit template text boxes.
function grdSupps_ClientEdit(sender, record) {
    var txtSigRefill = document.getElementById('txtSigRefillSupp');
    var txtDispRefill = document.getElementById('txtDispRefillSupp');
    var txtRefillsRefill = document.getElementById('txtRefillsRefillSupp');
    var txtStartDateRefill = document.getElementById('txtStartDateRefillSupp');
    var txtDateEndRefill = document.getElementById('txtDateEndRefillSupp');
    var txtNotesRefill = document.getElementById('txtNotesRefillSupp');
    var currDate = new Date();

    txtSigRefill.value = record.SuppDose;
    txtDispRefill.value = record.SuppDispenses;
    txtRefillsRefill.value = record.SuppNumbRefills;
    txtStartDateRefill.value = currDate.toDateString();
    txtDateEndRefill.value = '';
    txtNotesRefill.value = record.Notes;
}

//When click from the suppliment  list box then all the details will be fill in grid edit template text boxes.
function grdSupps_ClientClick() {
    disableGrid();
    var isFlag = false;
    var txtSigRefill = document.getElementById('txtSigRefillSupp').value;
    var txtDispRefill = document.getElementById('txtDispRefillSupp').value;
    var txtRefillsRefill = document.getElementById('txtRefillsRefillSupp').value;
    var txtStartDateRefill = document.getElementById('txtStartDateRefillSupp').value;

    if (txtStartDateRefill == "") {
        alert("Please enter start date.");
        return false;
    }
    var txtDateEndRefill = document.getElementById('txtDateEndRefillSupp').value;
    var txtNotesRefill = document.getElementById('txtNotesRefillSupp').value;
    var rec = grdSupps.SelectedRecords[0];

    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;
    var ProductID = rec.ProductID;
    var PresscriptionSuppID = rec.PresscriptionSuppID;
    grdSupps.deselectRecord(grdSupps.RecordInEditMode)

    var postData = new Object();
    postData.txtSig = txtSigRefill;
    postData.txtDisp = txtDispRefill;
    postData.txtRefill = txtRefillsRefill;
    postData.txtStartDate = txtStartDateRefill;
    postData.txtEndDate = txtDateEndRefill;
    postData.txtNotes = txtNotesRefill;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;
    postData.ProductID = ProductID;
    postData.PrescriptionID = PresscriptionSuppID;

    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionSuppRefill",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                alert("Data added successfully.");
                grdSupps.refresh();
                grdNewSupp.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });

}

//Refresh the suppliment grid 
function refreshSupps(result) {
    for (var i = 0; i < grdSupps.Rows.length; i++) {
        try {
            grdSupps.deselectRecord(i);
        } catch (ex) { };
    }
    grdSupps.refresh();
    grdNewSupp.refresh();
}

function errorSupps(result) {
    alert('Error porcessing supplement');
}

//Closed the suppliments
function btnClosePrescripSupp_ClientClick() {
    disableGrid();
    var rec = grdSupps.SelectedRecords[0];
    grdSupps.deselectRecord(grdSupps.RecordInEditMode)
    grdSupps.cancel_edit();
    var postData = new Object();
    postData.PrescriptionID = rec.PresscriptionSuppID;
    postData.ElementID = 2;
    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/ClosePrescription",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while closing supplement");
                isFlag = false;
            }
            else {

                isFlag = true;
                alert("supplement closed.");
                grdSupps.refresh();

            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
}

//Delete the supplement
function btnDeletePrescripSupp_ClientClick() {
    var message = confirm('Are you sure you want to delete this item?');
    if (message != 0) {
        disableGrid();
        var rec = grdSupps.SelectedRecords[0];

        grdSupps.cancel_edit();
        var postData = new Object();
        postData.PrescriptionID = rec.PresscriptionSuppID;
        postData.ElementID = 2;

        $.ajax({
            type: "POST",
            url: "PresrcriptionList.aspx/DeletePrescription",
            data: JSON.stringify(postData),
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var res = response.d;
                if (res == 0) {
                    alert("Some problem occurred while deleting supplement");
                    isFlag = false;
                }
                else {

                    isFlag = true;
                    alert("Supplement deleted.");
                    grdSupps.refresh();

                }

            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });

    }
    else {
        return false;
    }
}

//Add suppliments name and the suppliment in a  grid
function saveChangesSupp() {

    var isFlag = false;
    disableGrid();
    var txtDrugNameLocalSupp = document.getElementById('txtDrugNameLocalSupp').value;
    var txtStartDateSupp = document.getElementById('txtStartDateSupp').value;

    if (txtDrugNameLocalSupp == "") {
        alert("Please enter supplement name.");
        document.getElementById('txtDrugNameLocalSupp').focus();
        return false;
    }
    else if (txtStartDateSupp == "") {
        alert("Please enter start date.");
        document.getElementById('txtStartDateSupp').focus();
        return false;
    }
    var txtSigSupp = document.getElementById('txtSigSupp').value;
    var txtDispSupp = document.getElementById('txtDispSupp').value;
    var txtRefillSupp = document.getElementById('txtRefillSupp').value;

    var txtEndDateSupp = document.getElementById('txtEndDateSupp').value;
    var txtNotesSupp = document.getElementById('txtNotesSupp').value;
    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;

    var postData = new Object();
    postData.txtDrugNameLocalSupp = txtDrugNameLocalSupp;
    postData.txtSig = txtSigSupp;
    postData.txtDisp = txtDispSupp;
    postData.txtRefill = txtRefillSupp;
    postData.txtStartDate = txtStartDateSupp;
    postData.txtEndDate = txtEndDateSupp;
    postData.txtNotes = txtNotesSupp;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;

    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionSupp",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                newSupp.Close();
                alert("Data added successfully.");
                grdSupps.refresh();
                grdNewSupp.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
}

function saveChangesAddSupp() {
    var isFlag = false;
    disableGrid();
    var txtDrugNameLocalSupp = document.getElementById('txtNewDrugNameSupp').value;
    var txtStartDateSupp = document.getElementById('txtStartDateAddSupp').value;

    if (txtDrugNameLocalSupp == "") {
        alert("Please enter supplement name.");
        document.getElementById('txtNewDrugNameSupp').focus();
        return false;
    }
    else if (txtStartDateSupp == "") {
        alert("Please enter start date.");
        document.getElementById('txtStartDateAddSupp').focus();
        return false;
    }
    var txtSigSupp = document.getElementById('txtSigAddSupp').value;
    var txtDispSupp = document.getElementById('txtDispAddSupp').value;
    var txtRefillSupp = document.getElementById('txtRefillAddSupp').value;

    var txtEndDateSupp = document.getElementById('txtEndDateAddSupp').value;
    var txtNotesSupp = document.getElementById('txtNotesAddSupp').value;
    var txtPatientID = document.getElementById('txtPatientID').value;
    var txtStaffID = document.getElementById('txtStaffID').value;
    var txtAptID = document.getElementById('txtAptID').value;

    var postData = new Object();
    postData.txtDrugNameLocalSupp = txtDrugNameLocalSupp;
    postData.txtSig = txtSigSupp;
    postData.txtDisp = txtDispSupp;
    postData.txtRefill = txtRefillSupp;
    postData.txtStartDate = txtStartDateSupp;
    postData.txtEndDate = txtEndDateSupp;
    postData.txtNotes = txtNotesSupp;
    postData.txtPatientID = txtPatientID;
    postData.txtStaffID = txtStaffID;
    postData.txtAptID = txtAptID;

    $.ajax({
        type: "POST",
        url: "PresrcriptionList.aspx/InsertPrescriptionSupp",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res == 0) {
                alert("Some problem occurred while saving data");
                isFlag = false;
            }
            else {

                isFlag = true;
                AddNewSupp.Close();
                alert("Data added successfully.");
                grdSupps.refresh();
                grdNewSupp.refresh();
            }

        },
        error: function (obj) {

            alert(obj.responseText);
        }
    });
}




function applyFilter() {
    if (applyFilterTimeout) {
        window.clearTimeout(applyFilterTimeout);
    }

    applyFilterTimeout = window.setTimeout(doFiltering, 1000);
}

function applyFilterHist() {
    if (applyFilterHistTimeout) {
        window.clearTimeout(applyFilterHistTimeout);
    }

    applyFilterHistTimeout = window.setTimeout(doFilteringHist, 1000);
}


function applyFilterSupp() {
    if (applyFilterTimeout) {
        window.clearTimeout(applyFilterTimeout);
    }

    applyFilterTimeout = window.setTimeout(doFilteringSupp, 1000);
}

function doFiltering() {
    grdNew.filter();
}
function doFilteringHist() {
    grdHist.filter();
}

function doFilteringSupp() {
    grdNewSupp.filter();
}

function onDoubleClick(sender, iRecordIndex) {
    grdNew.editRecord(iRecordIndex);
}

function onDoubleClickSupp(sender, iRecordIndex) {
    grdNewSupp.editRecord(iRecordIndex);
}

function onDoubleClickCurrent(sender, iRecordIndex) {
    if (grdScrips.RecordInEditMode == null) {
        grdScrips.editRecord(iRecordIndex);
        var rec = grdScrips.Rows[iRecordIndex];
        if (rec.Cells[13].Value == "True") btnRefill.disable(); else btnRefill.enable();
        document.getElementById('txtRefillsRefill').value = "0";
    }
    else {
        grdScrips.cancel_edit();
    }

}

function onDoubleClickCurrentSupp(sender, iRecordIndex) {
    if (grdSupps.RecordInEditMode == null) {
        grdSupps.editRecord(iRecordIndex);
    }
    else {
        grdSupps.cancel_edit();
    }

}


function cancelChangesSupp() {
    newSupp.Close();
}





//function saveChangesAutoshipYes() {
//    disableGrid();
//    var txtScripData = document.getElementById('txtScripData');
//    CheckAutoship.Close();
//    var txtPatientID = document.getElementById('txtPatientID');
//    var txtStaffID = document.getElementById('txtStaffID');
//    var txtAptID = document.getElementById('txtAptID');
//    txtScripData.value += '|' + 'true|' + txtPatientID.value + '|' + txtStaffID.value + '|' + txtAptID.value;

//    var proxy = new JsonpAjaxService.EMRData();
//    proxy.NewSuppScrip(txtScripData.value, refreshSupps, errorSupps);

//}
//function cancelChangesAutosipNo() {
//    disableGrid();
//    var txtScripData = document.getElementById('txtScripData');
//    CheckAutoship.Close();
//    var txtPatientID = document.getElementById('txtPatientID');
//    var txtStaffID = document.getElementById('txtStaffID');
//    var txtAptID = document.getElementById('txtAptID');
//    txtScripData.value += '|' + 'false|' + txtPatientID.value + '|' + txtStaffID.value + '|' + txtAptID.value;

//    var proxy = new JsonpAjaxService.EMRData();
//    proxy.NewSuppScrip(txtScripData.value, refreshSupps, errorSupps);

//}



function cancelChangesAddSupp() {
    AddNewSupp.Close();
}



function AddDrugWin() {
    var currDate = new Date();
    document.getElementById("txtStartDateAdd").value = currDate.toDateString();

    AddDrugWindow.Open();
}

function AddDrugWinSupp() {
    var currDate = new Date();
    document.getElementById("txtStartDateAddSupp").value = currDate.toDateString();
    AddNewSupp.Open();
}

function ClosePrintScrips() {
    PrintScripsWindow.Close();
}
function ClosePrintSupps() {
    PrintSuppsWindow.Close();
}

function requestPermission() {

    PrintScripsWindow.Open();
}

function requestPermissionSupp() {

    PrintSuppsWindow.Open();
}

function btnPrintPatient_Click() {
    disableGrid();
    //get selected records
    var txtPatientID = document.getElementById('txtPatientID');

    var data = txtPatientID.value;
    for (i = 0; i < grdAutoship.SelectedRecords.length; i++) {
        if (data == "")
            data += grdAutoship.SelectedRecords[i].PrescriptionSuppID;
        else
            data += "," + grdAutoship.SelectedRecords[i].PrescriptionSuppID;
    }

    if (data != "") {
        //call service method to send followup
        //var proxy = new JsonpAjaxService.EMRData();
        //proxy.AutoshipFollowUp(data, '', '');
        var postData = new Object();
        postData.data = data;
        $.ajax({
            type: "POST",
            url: "PresrcriptionList.aspx/AutoshipFollowUp",
            data: JSON.stringify(postData),
            dataType: "json",
            contentType: "application/json",
            success: function (response) {
                var res = response.d;
                if (res == 0) {
                    alert("Some problem occurred while saving data");
                    isFlag = false;
                }


            },
            error: function (obj) {

                alert(obj.responseText);
            }
        });

    }
    //redirect to reoprt page.
    CheckAutoship.Close();
    window.location = "SciprList.aspx?PatientID=" + txtPatientID.value;
}
function GetAutoship() {
    grdAutoship.refresh();
    CheckAutoship.Open();

}


/*
* Date Format 1.2.3
* (c) 2007-2009 Steven Levithan <stevenlevithan.com>
* MIT license
*
* Includes enhancements by Scott Trenda <scott.trenda.net>
* and Kris Kowal <cixar.com/~kris.kowal/>
*
* Accepts a date, a mask, or a date and a mask.
* Returns a formatted version of the given date.
* The date defaults to the current date/time.
* The mask defaults to dateFormat.masks.default.
*/

var dateFormat = function () {
    var token = /d{1,4}|m{1,4}|yy(?:yy)?|([HhMsTt])\1?|[LloSZ]|"[^"]*"|'[^']*'/g,
		timezone = /\b(?:[PMCEA][SDP]T|(?:Pacific|Mountain|Central|Eastern|Atlantic) (?:Standard|Daylight|Prevailing) Time|(?:GMT|UTC)(?:[-+]\d{4})?)\b/g,
		timezoneClip = /[^-+\dA-Z]/g,
		pad = function (val, len) {
		    val = String(val);
		    len = len || 2;
		    while (val.length < len) val = "0" + val;
		    return val;
		};

    // Regexes and supporting functions are cached through closure
    return function (date, mask, utc) {
        var dF = dateFormat;

        // You can't provide utc if you skip other args (use the "UTC:" mask prefix)
        if (arguments.length == 1 && Object.prototype.toString.call(date) == "[object String]" && !/\d/.test(date)) {
            mask = date;
            date = undefined;
        }

        // Passing date through Date applies Date.parse, if necessary
        date = date ? new Date(date) : new Date;
        if (isNaN(date)) throw SyntaxError("invalid date");

        mask = String(dF.masks[mask] || mask || dF.masks["default"]);

        // Allow setting the utc argument via the mask
        if (mask.slice(0, 4) == "UTC:") {
            mask = mask.slice(4);
            utc = true;
        }

        var _ = utc ? "getUTC" : "get",
			d = date[_ + "Date"](),
			D = date[_ + "Day"](),
			m = date[_ + "Month"](),
			y = date[_ + "FullYear"](),
			H = date[_ + "Hours"](),
			M = date[_ + "Minutes"](),
			s = date[_ + "Seconds"](),
			L = date[_ + "Milliseconds"](),
			o = utc ? 0 : date.getTimezoneOffset(),
			flags = {
			    d: d,
			    dd: pad(d),
			    ddd: dF.i18n.dayNames[D],
			    dddd: dF.i18n.dayNames[D + 7],
			    m: m + 1,
			    mm: pad(m + 1),
			    mmm: dF.i18n.monthNames[m],
			    mmmm: dF.i18n.monthNames[m + 12],
			    yy: String(y).slice(2),
			    yyyy: y,
			    h: H % 12 || 12,
			    hh: pad(H % 12 || 12),
			    H: H,
			    HH: pad(H),
			    M: M,
			    MM: pad(M),
			    s: s,
			    ss: pad(s),
			    l: pad(L, 3),
			    L: pad(L > 99 ? Math.round(L / 10) : L),
			    t: H < 12 ? "a" : "p",
			    tt: H < 12 ? "am" : "pm",
			    T: H < 12 ? "A" : "P",
			    TT: H < 12 ? "AM" : "PM",
			    Z: utc ? "UTC" : (String(date).match(timezone) || [""]).pop().replace(timezoneClip, ""),
			    o: (o > 0 ? "-" : "+") + pad(Math.floor(Math.abs(o) / 60) * 100 + Math.abs(o) % 60, 4),
			    S: ["th", "st", "nd", "rd"][d % 10 > 3 ? 0 : (d % 100 - d % 10 != 10) * d % 10]
			};

        return mask.replace(token, function ($0) {
            return $0 in flags ? flags[$0] : $0.slice(1, $0.length - 1);
        });
    };
}();

// Some common format strings
dateFormat.masks = {
    "default": "ddd mmm dd yyyy HH:MM:ss",
    shortDate: "m/d/yy",
    mediumDate: "mmm d, yyyy",
    longDate: "mmmm d, yyyy",
    fullDate: "dddd, mmmm d, yyyy",
    shortTime: "h:MM TT",
    mediumTime: "h:MM:ss TT",
    longTime: "h:MM:ss TT Z",
    isoDate: "yyyy-mm-dd",
    isoTime: "HH:MM:ss",
    isoDateTime: "yyyy-mm-dd'T'HH:MM:ss",
    isoUtcDateTime: "UTC:yyyy-mm-dd'T'HH:MM:ss'Z'"
};

// Internationalization strings
dateFormat.i18n = {
    dayNames: [
		"Sun", "Mon", "Tue", "Wed", "Thu", "Fri", "Sat",
		"Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"
    ],
    monthNames: [
		"Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec",
		"January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December"
    ]
};

// For convenience...
Date.prototype.format = function (mask, utc) {
    return dateFormat(this, mask, utc);
};


// page: Admin_Reseller_Data.aspx
// STATUS MANAGEMENT
// this method is using for check the duplicate status name during Add/Edit the record.
// Status Management
function CheckDuplicateResellerStatus(resellerStatusId, statusName) {
    var isFlag = false;
    url = 'admin_reseller_data.aspx/CheckDuplicateResellerStatus';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{resellerStatusId:'" + resellerStatusId + "', statusName:'" + htmlEncode(statusName) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + statusName + "') that you entered already exists in the database, please choose another.");
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

var checkStatusManagement = "";

function ValidateResellerStatus(selectedRecords) {
    var statusId = selectedRecords.StatusID;
    var statusName = selectedRecords.Status;
    var errString = "";
    if (statusName == "") {
        alert("Please enter status name.");
        return false;
    }
    else {
        CheckDuplicateResellerStatus(statusId, statusName)
        return checkStatusManagement;
    }
}

// EVENT MANAGEMENT
var checkEventManagement = "";
function ValidateEventManagement(selectedRecords) {
    var EventID = selectedRecords.EventID;
    var EventName = selectedRecords.EventName;
    var errString = "";
    if (EventName == "") {
        alert("Please enter event name.");
        return false;
    }
    else {
        CheckDuplicateEvent(EventID, EventName)
        return checkEventManagement;
    }
}

function CheckDuplicateEvent(EventID, EventName) {
    var isFlag = false;
    url = 'admin_reseller_data.aspx/CheckDuplicateEvent';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{eventID:'" + EventID + "', eventName:'" + htmlEncode(EventName) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + EventName + "') that you entered already exists in the database, please choose another.");
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
        checkEventManagement = false;
        return false;
    }
    else {
        checkEventManagement = true;
        return true;
    }

}

// MARKETING SOURCE
var checkMarketingSource = "";
function ValidateMarketingSource(selectedRecords) {
    var ResellerMarketingSourceID = selectedRecords.ResellerMarketingSourceID;
    var SourceName = selectedRecords.SourceName;
    var errString = "";
    if (SourceName == "") {
        alert("Please enter marketing source name.");
        return false;
    }
    else {
        CheckDuplicateSource(ResellerMarketingSourceID, SourceName)
        return checkMarketingSource;
    }
}

function CheckDuplicateSource(ResellerMarketingSourceID, SourceName) {
    var isFlag = false;
    url = 'admin_reseller_data.aspx/CheckDuplicateSource';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{resellerMarketingSourceID:'" + ResellerMarketingSourceID + "', sourceName:'" + htmlEncode(SourceName) + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + SourceName + "') that you entered already exists in the database, please choose another.");
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
        checkMarketingSource = false;
        return false;
    }
    else {
        checkMarketingSource = true;
        return true;
    }

}

function MessageAfterUpdateRecords() {
    alert('You have successfully updated the record.');
}

function MessageAfterAddRecords() {
    alert('You have successfully added the record.');
}

//////////////

//******************* Calendar/CalendarAdmin.aspx *******************//
// This function is using for check the duplicate records in tables(Providers, AppointmentType, AppointmentResults, Status) 
var checkCalendarFlag = "";
function ValidateDataForCalendarAdmin() {
    var headerText = $(".headerText").html();
    var strText = headerText.replace("Add ", "");
    var name = $(".TextValue").val();
    var ID = 0;
    if (name == "") {
        alert("Please enter the value.");
        return false;
    }
    else {
        CheckDuplicateData(strText, ID, name)
        return checkCalendarFlag;
    }
}

var newValue = "";
function ValidateDataServerSideForCalendarAdmin(strText, ID, name) {
    if (name == "") {
        alert("Please enter the value.");
        return false;
    }
    else {
        CheckDuplicateData(strText, ID, name)
        return checkCalendarFlag;
    }
}

function CheckDuplicateData(strText, ID, Name) {
    var isFlag = false;
    url = 'CalendarAdmin.aspx/CheckDuplicateData';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{Text:'" + strText + "', ID:'" + ID + "', Name:'" + Name + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + Name + "') that you entered already exists in the database, please choose another.");
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
        checkCalendarFlag = false;
        newValue = "hello";
        return false;
    }
    else {
        checkCalendarFlag = true;
        newValue = "welcome";
        return true;
    }

}


// PANEL MANAGEMENT
// used on Admin_LabRequest.aspx
// check the duplicate record during add/update the panels.
var checkPanelManagement = "";
function ValidateLabRequestPanels(selectedRecords) {
    var PanelID = selectedRecords.LabRequest_PanelID;
    var PanelName = selectedRecords.PanelName;
    if (PanelName == "") {
        alert("Please enter panel name.");
        return false;
    }
    else {
        CheckDuplicatePanel(PanelID, PanelName)
        return checkPanelManagement;
    }
}

function CheckDuplicatePanel(PanelID, PanelName) {
    var isFlag = false;
    url = 'admin_LabRequest.aspx/CheckDuplicatePanel';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{panelID:'" + PanelID + "', panelName:'" + PanelName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + PanelName + "') that you entered already exists in the database, please choose another.");
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
        checkPanelManagement = false;
        return false;
    }
    else {
        checkPanelManagement = true;
        return true;
    }

}


// TEST MANAGEMENT
// used on Admin_LabRequest.aspx
// check the duplicate record during add/update the panels.
var checkTestManagement = "";
function ValidateLabRequestTest(selectedRecords) {
    var TestID = selectedRecords.LabRequest_TestID;
    var TestName = selectedRecords.TestName;

    if (TestName == "") {
        alert("Please enter test name.");
        return false;
    }
    else {
        CheckDuplicateTest(TestID, TestName)
        return checkTestManagement;
    }
}

function CheckDuplicateTest(TestID, TestName) {
    var isFlag = false;
    url = 'admin_LabRequest.aspx/CheckDuplicateTest';

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{testID:'" + TestID + "', testName:'" + TestName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + TestName + "') that you entered already exists in the database, please choose another.");
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
        checkTestManagement = false;
        return false;
    }
    else {
        checkTestManagement = true;
        return true;
    }

}

//// LabSchedule Group Management
var checkCommonVar = "";
function ValidateGroupForLabSchedule(selectedRecords) {
    var URL = 'admin_labSchedule.aspx/CheckDuplicateRecords';
    var tableName = "LabScehdule_Groups";
    var GroupID = selectedRecords.GroupID;
    var GroupName = selectedRecords.GroupName;
    var Male = selectedRecords.Male;
    var Female = selectedRecords.Female;


    if (GroupName == "") {
        alert("Please enter group name.");
        return false;
    }
    else if (Male == "") {
        alert("Please enter the number of days in male field.");
        return false;
    }
    else if (Female == "") {
        alert("Please enter the number of days in female field.");
        return false;
    }
    else if (Male.length > 3) {
        alert('Please enter only 3 digit value in male field');
        return false;
    }
    else if (!$.isNumeric(Male)) {
        alert('Please enter only numeric value in male field');
        return false;
    }
    else if (Female.length > 3) {
        alert('Please enter only 3 digit value in female field');
        return false;
    }
    else if (!$.isNumeric(Female)) {
        alert('Please enter only numeric value in female field');
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, GroupID, GroupName, tableName);
        return checkCommonVar;
    }
}

function ValidateTestForLabSchedule(selectedRecords) {
    var URL = 'admin_labSchedule.aspx/CheckDuplicateRecords';
    var tableName = "LabSchedule_Tests";
    var TestID = selectedRecords.TestID;
    var TestName = selectedRecords.TestName;
    var GroupID = selectedRecords.GroupID;


    if (TestName == "") {
        alert("Please enter test name.");
        return false;
    }
    else if (!$.isNumeric(GroupID)) {
        alert('Please enter only numeric value in Assigned Group ID field');
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, TestID, TestName, tableName);
        return checkCommonVar;
    }
}

// Manage External Labs and Panels
// External/Panels.aspx
function ValidatePanels(selectedRecords) {
    var URL = 'Panels.aspx/CheckDuplicateRecords';
    var tableName = "ExternalPanels";
    var ExternalPanelsID = selectedRecords.ExternalPanelsID;
    var PanelName = selectedRecords.PanelName;

    if (PanelName == "") {
        alert("Please enter panel name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ExternalPanelsID, PanelName, tableName);
        return checkCommonVar;
    }
}

// Manage Automatic Ticket Processes 
// Tickats_Manage.aspx
function ValidateTicketProcessing(selectedRecords) {
    var URL = 'Tickats_Manage.aspx/CheckDuplicateRecords';
    var tableName = "Tickets_Manage";
    var ID = selectedRecords.ProcessID;
    var Name = selectedRecords.ProcessName;

    if (Name == "") {
        alert("Please enter the Process name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
        return checkCommonVar;
    }
}

// Manage Product
// Autoship/AutoShip.aspx
function ValidateManageProduct(selectedRecords) {
    var URL = 'Default.aspx/CheckDuplicateRecords';
    var tableName = "AutoshipProducts";
    var ID = selectedRecords.ProductID;
    var Name = selectedRecords.ProductName;

    if (Name == "") {
        alert("Please enter the Product name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
        return checkCommonVar;
    }
}

// Edit Drug List
// admin_Drug_List.aspx
function ValidateDrugs(selectedRecords) {
    var URL = 'admin_Drug_List.aspx/CheckDuplicateRecords';
    var tableName = "Drugs";
    var ID = selectedRecords.DrugID;
    var Name = selectedRecords.DrugName;

    if (Name == "") {
        alert("Please enter the Drug name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
        return checkCommonVar;
    }
}

// Edit Suppliment List
// admin_Supp_List.aspx
function ValidateSupplement(selectedRecords) {
    var URL = 'admin_Supp_List.aspx/CheckDuplicateRecords';
    var tableName = "AutoshipProducts";
    var ID = selectedRecords.ProductID;
    var Name = selectedRecords.ProductName;

    if (Name == "") {
        alert("Please enter the Supplement name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
        return checkCommonVar;
    }
}

// Edit Uploadtag List
// admin_UploadTag_List.aspx
function ValidateTags(selectedRecords) {
    var URL = 'Admin_UploadTags.aspx/CheckDuplicateRecords';
    var tableName = "UploadTags";
    var ID = selectedRecords.Id;
    var Name = selectedRecords.Name;

    if (Name == "") {
        alert("Please enter the Tag name.");
        return false;
    }
    else {
        CommonFunctionForCheckDuplicate(URL, ID, Name, tableName);
        return checkCommonVar;
    }
}


function CommonFunctionForCheckDuplicate(url, id, name, tableName) {
    var isFlag = false;

    $.ajax({
        type: "POST",
        url: url,
        async: false,
        data: "{ID:'" + id + "', Name:'" + htmlEncode(name) + "', tableName:'" + tableName + "'}",
        contentType: "application/json; charset=utf-8",
        dataType: "json",
        success: function (result) {
            if (result.d != '') {
                alert("The value('" + name + "') that you entered already exists in the database, please choose another.");
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
        checkCommonVar = false;
        return false;
    }
    else {
        checkCommonVar = true;
        return true;
    }

}

// Function for display the confirmation message before delete any item.
function ConfirmDelete() {
    var message = confirm('Are you sure you want to delete this item?');
    if (message != 0) {
        return true;
    }
    else {
        return false;
    }
}

// common function for replace the special characters in HTML format
function htmlEncode(str) {
    return String(str).replace(/"/g, '&quot;').replace(/'/g, "&#39;");
}