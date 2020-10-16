//Created By jaswinder

//bind the data for first tab initially on page load

$(document).ready(function () {
   
    PatientNewData('#grdPatientNew', '#pagernav1', '1');
    $("#MatchDialog").hide();
    $("#MatchedFreshbookPatientsListDialog").hide();
    $('#btnTaxrateImport').hide();
    // Watch for the value of the "Choose Image" field to change...
    $('#MainContent_fileUplodTax').change(function () {
        // If the upload field is empty, we'll hide te upload button,
        // Otherwise, let's show it.
        if ('' === $('#MainContent_fileUplodTax').val()) {
            $('#btnTaxrateImport').hide();
        } else {
            var fileName = $('#MainContent_fileUplodTax').val();
            // Use a regular expression to trim everything before final dot
            var extension = fileName.substr((fileName.lastIndexOf('.') + 1));

            // Iff there is no dot anywhere in filename, we would have extension == filename,
            // so we account for this possibility now
            //if (extension == filename) {
            //    extension = '';
            //} else {
            //    // if there is an extension, we convert to lower case
            //    // (N.B. this conversion will not effect the value of the extension
            //    // on the file upload.)
            extension = extension.toLowerCase();
            //}
            if (extension == 'xls' || extension == 'xlsx') {
                $('#btnTaxrateImport').show();
            }
            else {
                alert("Selected file extension must be xls or xlsx");
            }
        } // end if/else

    });
    $.ui.dialog.prototype._makeDraggable = function () {
        this.uiDialog.draggable({
            containment: false
        });
    };
});

//Bind the data with the patients details that need to import to Freshbooks
function PatientNewData(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({
        url: 'FreshBookMain.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        colNames: ['<input type="checkBox" id="CheckAll" onclick="checkBox(this,' + 'chkGrid' + ');">Patient ID', 'Last Name', 'FirstName'],
        colModel: [
             { name: 'PatientID', index: 'PatientID', editable: false, sortable: false, align: "center", formatter: createOpenPOPupButton },
             { name: 'LastName', index: 'LastName', editable: false, sortable: true },
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
        ],
        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }
   );
}

//Find the checkbox and mark them as checked or uncked
$("#grdPatientNew").find('input[type=checkbox]').each(function () {
    $(this).change(function () {
        var colid = $(this).parents('tr:last').attr('id');
        if ($(this).is(':checked')) {
            $("#grdPatientNew").jqGrid('setSelection', colid);
            $(this).prop('checked', true);
        }
        return true;
    });
});


//Create a checkbox within the JqGrid
function createOpenPOPupButton(cellvalue, options, rowObject) {
    return "<input class='blue-btn' type='checkBox' id='" + rowObject[0] + "' >";
}


function btnExportPatientsClick() {
    var checkValues = $("#grdPatientNew").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();
    $('#checkedPatientIds').val(checkValues);
}



//function that fires to bind order grid
function OrderNewData(Grid, Pager, GridFill) {
    $(Grid).jqGrid({
        url: 'FreshBookMain.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        colNames: ['<input type="checkBox" id="CheckAll" onclick="checkBox(this,' + Grid + ');">Order ID', 'Last Name', 'FirstName'],
        colModel: [
             { name: 'OrderID', index: 'OrderID', editable: false, sortable: false, align: "center", formatter: createOpenPOPupButton },
             { name: 'LastName', index: 'LastName', editable: false, sortable: true },
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
        ],
        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }
   );
}

//Get values of checked items for exporting invoice
function btnExportInvoiceclick() {
    var checkValues = $("#grdInvoiceNew").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();
    $('#checkedInvoiceiDS').val(checkValues);
}

//This function gets all matched Fresh book patients according to patients Id
function MatchedFreshbookPatientsByPatientsId(Grid, Pager, GridFill, FirstName, LastName, Email, PatientsId) {
    $("#MatchedFreshbookPatientsByPatientIdDiv").html('<table id="grdMatchedFreshBookPatientsByPatientId"></table><div id="DivNoRecord8" style="visibility: hidden"><span>No Record</span></div><div id="pagernav8"></div>');
    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({
        url: 'FreshBookMain.aspx?gridFill=' + GridFill + "&PatientsId=" + PatientsId,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        rowNum: 10,
        colNames: ['First Name', 'Last Name', 'Email', 'Action'],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
              { name: 'LastName', index: 'LastName', editable: false, sortable: true },
              { name: 'Email', index: 'Email', editable: false, sorttype: "string", classes: 'wrap' },
            { name: 'PatientID', index: 'PatientID', editable: false, sortable: false, align: "center", formatter: createMatchRemoveButton },
        ],
        pager: Pager,
        sortable: true,
        height: 230,
        autowidth: true,
        viewrecords: true,
        gridview: true,

        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', 778, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
            OpenPopUpFreshbookContactListByPatientsId(FirstName, LastName, Email, PatientsId);
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }
   );
}
//This function get local patients details who mached with FreshBook patients.
function MatchedPatientsData_FreshBook(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({
        url: 'FreshBookMain.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        colNames: ['First Name', 'Last Name', 'Email', 'Action'],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
              { name: 'LastName', index: 'LastName', editable: false, sortable: true },
              { name: 'Email', index: 'Email', editable: false, sorttype: "string", classes: 'wrap' },
            { name: 'PatientID', index: 'PatientID', editable: false, sortable: false, align: "center", formatter: createFreshBookContactsByPatientsIdDialogButton },
        ],
        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }
   );
}
//Bind the data to different ticket grids on the basis of conditions
function FreshBookPatientsData(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({
        url: 'FreshBookMain.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        colNames: ['First Name', 'Last Name', 'Email', 'Action'],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
              { name: 'LastName', index: 'LastName', editable: false, sortable: true },
              { name: 'Email', index: 'Email', editable: false, sorttype: "string", classes: 'wrap' },
            { name: 'PatientID', index: 'PatientID', editable: false, sortable: false, align: "center", formatter: createMatchPOPupButton },
        ],
        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,

        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', $(window).width() - 50, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: true
        }
   );
}

//This function returns search result to match Freshbook client with Local patients.
function SearchMatchFreshbookPatientsData(Grid, Pager, GridFill) {
    $("#FreshBookPatientsMatchSearch").html('<table id="grdFreshBookPatientsMatchSearch"></table><div id="DivNoRecord6" style="visibility: hidden"><span>No Record</span></div><div id="pagernav6"></div>');

    var FirstName = document.getElementById('FirstNameS').value;
    var LastName = document.getElementById('LastNameS').value;
    var Email = document.getElementById('EmailS').value;

    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({

        url: 'FreshBookMain.aspx?gridFill=' + GridFill + "&FirstName=" + FirstName + "&LastName=" + LastName + "&Email=" + Email,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['First Name', 'Last Name', 'Email', ""],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap1', width: "150" },
             { name: 'LastName', index: 'LastName', editable: false, sortable: true, width: "150" },
             { name: 'Email', index: 'Email', editable: false, sorttype: "string", classes: 'wrap', width: "300" },
             { name: 'PatientID', index: 'PatientID', editable: false, sortable: false, align: "center", width: "150", formatter: createMatchButton },
        ],
        pager: Pager,
        sortable: true,
        height: 500,
        autowidth: true,
        viewrecords: true,
        gridview: true,
        gridComplete: function () {
            $(Grid).jqGrid('setGridWidth', 761, true);
            var rows = $(Grid).getDataIDs();
            if (rows.length > 0) {
                $(Grid).show();
                $("#DivNoRecord" + GridFill).hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord" + GridFill).show();
            }
        }
    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: false
        }
   );
}





///Create a checkbox within the JqGrid
function createMatchPOPupButton(cellvalue, options, rowObject) {
    var ContactId = '"' + String(rowObject[3]) + '"';
    var FirstName = '"' + String(rowObject[0]) + '"';
    var LastName = '"' + String(rowObject[1]) + '"';
    var Email = '"' + String(rowObject[2]) + '"';
    var total = FirstName + "," + LastName + "," + Email + "," + ContactId;
    return "<input class='blue-btn' value='Match' type='button' id='" + rowObject[3] + "' onclick='OpenPopUpForMatch(" + total + ");' >";
}

//This funcrtion returns matching button html.
function createMatchButton(cellvalue, options, rowObject) {
    var PatientsId = '"' + String(rowObject[3]) + '"';
    var FirstName = '"' + String(rowObject[0]) + '"';
    var LastName = '"' + String(rowObject[1]) + '"';
    var Email = '"' + String(rowObject[2]) + '"';
    var total = FirstName + "," + LastName + "," + Email + "," + PatientsId;
    return "<input class='blue-btn MatchBtnClass' value='Match' type='button' id='MatchBtn" + rowObject[3] + "' onclick='MatchRecordWithLocalPatients(" + total + ");' >";
}

//This funcrtion returns matching button html.
function createFreshBookContactsByPatientsIdDialogButton(cellvalue, options, rowObject) {
    var PatientsId = '"' + String(rowObject[3]) + '"';
    var FirstName = '"' + String(rowObject[0]) + '"';
    var LastName = '"' + String(rowObject[1]) + '"';
    var Email = '"' + String(rowObject[2]) + '"';

    var total = '"#grdMatchedFreshBookPatientsByPatientId","#pagernav8","8"' + ',' + FirstName + ',' + LastName + ',' + Email + ',' + PatientsId;
    var Result = "<input class='blue-btn MatchedXeroCBtnClass' value='Freshbook Client' type='button' id='FreshbookContactsBtn" + rowObject[3] + "' onclick='MatchedFreshbookPatientsByPatientsId(" + total + ");' >";
    return Result;
}

function createMatchRemoveButton(cellvalue, options, rowObject) {
    var PatientId = '"' + String(rowObject[3]) + '"';
    var ContactId = '"' + String(rowObject[4]) + '"';
    var ParameterF = PatientId + "," + ContactId;
    return "<input class='blue-btn MatchRemoveBtnClass1' value='Remove Match' type='button' id='MatchRemoveBtn1" + PatientId + "'onclick='MatchRemoveRecordWithLocalPatients1(" + ParameterF + ");' >"
}

function ImportPatientsClick() {
    $("#loading-div-background").show(); // show wait
    $.ajax({
        type: "POST",
        url: "FreshBookMain.aspx/ImportPatients_Click",
        data: "{}",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#grdFreshBookPatients").trigger("reloadGrid");
            $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
            $("#grdPatientNew").trigger("reloadGrid");
            $("#loading-div-background").hide(); // remove wait, done!
            alert("Clients has imported successfully.");
        },
        error: function () {
            $("#loading-div-background").hide(); // remove wait, done!
            alert("There is some internal error!");
    }
    });
}
function ExportPatientsClick() {
    $("#loading-div-background").show(); // show wait
    btnExportPatientsClick();
    var PatientId = $("#checkedPatientIds").val();
    $.ajax({
        type: "POST",
        url: "FreshBookMain.aspx/ExportPatients_Click",
        data: JSON.stringify({ checkedPatientIds : PatientId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#grdFreshBookPatients").trigger("reloadGrid");
            $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
            $("#grdPatientNew").trigger("reloadGrid");
            $("#loading-div-background").hide(); // remove wait, done!
            alert("Patient has exported successfully.");
        },
        error: function () {
            $("#loading-div-background").hide(); // remove wait, done!
            alert("There is some internal error!");
        }
    });

}

function ExportInvoiceClick() {
    $("#loading-div-background").show(); // show wait
    btnExportInvoiceclick();
    var InvoiceId = $("#checkedInvoiceiDS").val();
    $.ajax({
        type: "POST",
        url: "FreshBookMain.aspx/ExportInvoice_Click",
        data: JSON.stringify({ checkedInvoiceiDS: InvoiceId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#grdFreshBookPatients").trigger("reloadGrid");
            $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
            $("#grdPatientNew").trigger("reloadGrid");
            $("#loading-div-background").hide(); // remove wait, done!
            alert("Clients are imported successfully.");
        },
        error: function () {
            $("#loading-div-background").hide(); // remove wait, done!
            alert("There is some internal error!");
        }
    });

}

function updateExportPatClick() {
    $("#loading-div-background").show(); // show wait
    btnupdateExportPatclick();
    var PatientId = $("#checkedPatientIds").val();
    $.ajax({
        type: "POST",
        url: "FreshBookMain.aspx/updateExportPat_Click",
        data: JSON.stringify({ checkedPatientIds1: PatientId }),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#grdFreshBookPatients").trigger("reloadGrid");
            $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
            $("#grdPatientNew").trigger("reloadGrid");
            $("#loading-div-background").hide(); // remove wait, done!
            alert("Clients are imported successfully.");
        },
        error: function () {
            $("#loading-div-background").hide(); // remove wait, done!
            alert("There is some internal error!");
           
        }
    });

}

//This function making matching relation between Freshbook client and local user.
function MatchRecordWithLocalPatients(FirstName, LastName, Email, PatientsId) {
    var ContactId = document.getElementById('FreshbookContactId').value;
    var ParameterF = "\"" + PatientsId + "\",\"" + ContactId + "\"";
    var FirstNameP = $("#FirstName").prop('title');
    var LastNameP = $("#LastName").prop('title');
    var EmailP = $("#EmailId").prop('title');
    var Message = "Do you want to match '" + FirstNameP + " " + LastNameP + "'";
    if (EmailP == "-" || EmailP == "" || EmailP == "null") { Message = Message } else { Message = Message + ", email id '" + EmailP + "'"; }
    Message = Message + " with '" + FirstName + " " + LastName + "'";
    if (Email == "-" || Email == "" || Email == "null" || Email == null) { Message = Message + "?" } else { Message = Message + ", email id '" + Email + "'?"; }
    var r = confirm(Message);
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "FreshBookMain.aspx/MatchTwoPatients",
            data: JSON.stringify({ PatientId: PatientsId, ContactId: ContactId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                if (returnedstring == "Success") {
                    var MatchButton = $(".MatchBtnClass");
                    MatchButton.each(function () {
                        $(this).hide();
                    })
                    $("#MatchBtn" + PatientsId).parents('tr').find('td').addClass("ChangeBackground");
                    $("#grdFreshBookPatients").trigger("reloadGrid");
                    $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
                    $("#grdPatientNew").trigger("reloadGrid");
                    $("#SearchPatientsFreshbookButton").hide();
                    alert("Record successfully matched.");
                    $("#MatchBtn" + PatientsId).after("<input class='blue-btn MatchRemoveBtnClass' value='Remove Match' type='button' id='MatchRemoveBtn" + PatientsId + "'onclick='MatchRemoveRecordWithLocalPatients(" + ParameterF + ");' >");
                }
                else { alert("Oops! There is an error.") }
            }
        });
    }
    else {
        alert("Click on match button if you want to match this freshbook client record with local record.");
    }
}
//Remove matching relation between Freshbook patients and local patients.
function MatchRemoveRecordWithLocalPatients1(PatientId, ContactId) {
    var Message = "Do you want to remove this match?";
    var r = confirm(Message);
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "FreshBookMain.aspx/RemoveMatchTwoPatients",
            data: JSON.stringify({ PatientId: PatientId, ContactId: ContactId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                if (returnedstring == "Success") {
                    $("#grdMatchedFreshBookPatientsByPatientId").trigger("reloadGrid");
                    $("#grdFreshBookPatients").trigger("reloadGrid");
                    $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
                    $("#grdPatientNew").trigger("reloadGrid");
                    alert("Matching successfully removed.");
                }
                else { alert("Oops! There is an error.") }
            }
        });
    }
    else {
        //alert("Click on match button if you want to match this Freshbook client record with local record.");

    }
}

//Remove matching relation between Freshbook client and local patients.
function MatchRemoveRecordWithLocalPatients(PatientId, ContactId) {
    var Message = "Do you want to remove this match?";
    var r = confirm(Message);
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "FreshBookMain.aspx/RemoveMatchTwoPatients",
            data: JSON.stringify({ PatientId: PatientId, ContactId: ContactId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                if (returnedstring == "Success") {
                    var MatchButton = $(".MatchBtnClass");
                    MatchButton.each(function () {
                        $(this).show();
                    })
                    $("#SearchPatientsFreshbookButton").show();
                    $("#MatchBtn" + PatientId).parents('tr').find('td').removeClass("ChangeBackground");
                    $("#grdFreshBookPatients").trigger("reloadGrid");
                    $("#grdMatchedPatients_FreshBook").trigger("reloadGrid");
                    $("#grdPatientNew").trigger("reloadGrid");
                    $(".MatchRemoveBtnClass").on().remove();
                    alert("Matching successfully removed.");
                }
                else { alert("Oops! There is an error.") }
            }
        });
    }
    else {
        //alert("Click on match button if you want to match this Freshbook client record with local record.");

    }
}

//This dialog is open to display List of matched Freshbook client by patients id.
function OpenPopUpFreshbookContactListByPatientsId(FirstName, LastName, Email, PatientsId) {

    if (FirstName == "null") { document.getElementById("FirstName1").innerHTML = "-"; $("#FirstName1").attr('title', "-"); }
    else {
        if (FirstName.length > 18) {
            document.getElementById("FirstName1").innerHTML = FirstName.substring(0, 17) + "...";
        }
        else {
            document.getElementById("FirstName1").innerHTML = FirstName;
        }
        $("#FirstName1").attr('title', FirstName);
    }

    if (LastName == "null") { document.getElementById("LastName1").innerHTML = "-"; $("#LastName1").attr('title', "-"); }
    else {
        if (LastName.length > 18) {
            document.getElementById("LastName1").innerHTML = LastName.substring(0, 17) + "...";
        }
        else {
            document.getElementById("LastName1").innerHTML = LastName;
        }
        $("#LastName1").attr('title', LastName);
    }

    if (Email == "null") { document.getElementById("EmailId1").innerHTML = "-"; $("#EmailId1").attr('title', "-"); }
    else {
        if (Email.length > 18) {
            document.getElementById("EmailId1").innerHTML = Email.substring(0, 17) + "...";
        }
        else {
            document.getElementById("EmailId1").innerHTML = Email;
        }
        $("#EmailId1").attr('title', Email);
    }

    //if (PatientsId == "null") { document.getElementById("PatientsId1").value = "-"; }
    //else { document.getElementById("PatientsId1").value = PatientsId; }

    $("#MatchedFreshbookPatientsListDialog").show();
    $("#MatchedFreshbookPatientsListDialog").dialog({
        height: 342,
        width: 800,
        modal: true,
        dialogClass: 'DialogClass1'
    });
}
//This window open jquery popup to make search.
function OpenPopUpForMatch(FirstName, LastName, Email, ContactId) {
    $("#FreshBookPatientsMatchSearch").html('<table id="grdFreshBookPatientsMatchSearch"></table><div id="DivNoRecord6" style="visibility: hidden"><span>No Record</span></div><div id="pagernav6"></div>');

    if (FirstName == "null") { document.getElementById("FirstName").innerHTML = "-"; $("#FirstName").attr('title', "-"); }
    else {
        if (FirstName.length > 18) {
            document.getElementById("FirstName").innerHTML = FirstName.substring(0, 17) + "...";
        }
        else {
            document.getElementById("FirstName").innerHTML = FirstName;
        }
        $("#FirstName").attr('title', FirstName);
    }

    if (LastName == "null") { document.getElementById("LastName").innerHTML = "-"; $("#LastName").attr('title', "-"); }
    else {
        if (LastName.length > 18) {
            document.getElementById("LastName").innerHTML = LastName.substring(0, 17) + "...";
        }
        else {
            document.getElementById("LastName").innerHTML = LastName;
        }
        $("#LastName").attr('title', LastName);
    }

    if (Email == "null") { document.getElementById("EmailId").innerHTML = "-"; $("#EmailId").attr('title', "-"); }
    else {
        if (Email.length > 18) {
            document.getElementById("EmailId").innerHTML = Email.substring(0, 17) + "...";
        }
        else {
            document.getElementById("EmailId").innerHTML = Email;
        }
        $("#EmailId").attr('title', Email);
    }

    if (ContactId == "null") { document.getElementById("FreshbookContactId").value = "-"; }
    else { document.getElementById("FreshbookContactId").value = ContactId; }
    $("#MatchDialog").show();
    $("#SearchPatientsFreshbookButton").show();
    $("#MatchDialog").dialog({
        height: 530,
        width: 800,
        modal: true,
        left: 100,
        top: 50,
        dialogClass: 'DialogClass'
    });
}
//Find checked checkboxes within the grid
function checkBox(obj, grid) {
    var chkGrid = '#' + grid;
    $(chkGrid).find("input[type=checkbox]").prop('checked', true);
}

//Get values of checked items for importing patients
function btnupdateImport_click() {
    var checkValues = $("#grdPatUpdate").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();
    $('#checkedMerchantiDS').val(checkValues);
}

function btnImport_click() {
    var checkValues = $("#grdPatientNew").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();
    $('#checkedMerchantiDS').val(checkValues);
}





function btnupdateExportPatclick() {
    var checkValues = $("#grdPatUpdate").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();
    $('#checkedPatientIds').val(checkValues);
}




