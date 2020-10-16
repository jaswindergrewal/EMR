//Created By jaswinder

//bind the data for first tab initially on page load

    $(document).ready(function () {
      
        $("#MatchDialog").hide();
        $("#MatchedXeroPatientsListDialog").hide();
        $.ui.dialog.prototype._makeDraggable = function () {
            this.uiDialog.draggable({
                containment: false
            });
        };
    });




//Bind the data to different ticket grids on the basis of conditions
function PatientNewData(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid.length);
    $(Grid).jqGrid({

        url: 'PatientFormXERO.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",


        colNames: ['<input type="checkBox" id="CheckAll" onclick="checkBox(this,' + chkGrid + ');">Patient ID', 'Last Name', 'FirstName'],
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

//Get values of checked items for importing invoice
function btnInvoiceImport_click() {

    var checkValues = $("#grdInvoiceNew").find('input[type=checkbox]:checked').map(function () {
        return $(this).attr("id");
    }).get();

    $('#checkedInvoiceiDS').val(checkValues);


}


//function that fires on click of open button from grid
function OpenPopupforSearchPages1(obj) {

    var buttonId = $(obj).attr("id");
    var arr = buttonId.split('/');
    var strPatientId = arr[0];
    var strFollowupId = arr[1];

    if (strFollowupId != 0) {
        var postData = new Object();
        postData.SessionValue = strFollowupId;

        $.ajax({
            type: "POST",
            url: "LandingPage.aspx/SetSession",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var listObj = response.d;
                if (listObj > 0) {
                    if (strPatientId != '') {
                        MM_goToURL('parent', 'Manage.aspx?TicketID=' + strFollowupId + '&PatientID=' + strPatientId); return document.MM_returnValue;
                    }
                    else {
                        MM_goToURL('parent', 'TicketNoPatient.aspx?TicketID=' + strFollowupId); return document.MM_returnValue;
                    }
                }

            },
            error: function () {
                alert("Fail to load data.");
            }
        });

    }
}

//function that fires to bind order grid
function OrderNewData(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid.length);
    $(Grid).jqGrid({

        url: 'PatientFormXERO.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['<input type="checkBox" id="CheckAll" onclick="checkBox(this,' + chkGrid + ');">Order ID', 'Last Name', 'FirstName'],
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


//Bind the data to different ticket grids on the basis of conditions
function XeroPatientsData(Grid, Pager, GridFill) {
    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({
        url: 'PatientFormXERO.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",
        colNames: ['First Name', 'Last Name', 'Street','State','Country', 'Action'],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap' },
              { name: 'LastName', index: 'LastName', editable: false, sortable: true },
              { name: 'BillingStreet', index: 'BillingStreet', editable: false, sorttype: "string", classes: 'wrap' },
              { name: 'BillingState', index: 'BillingState', editable: false, sorttype: "string", classes: 'wrap' },
              { name: 'BillingCountry', index: 'BillingCountry', editable: false, sorttype: "string", classes: 'wrap' },
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

//Create a checkbox within the JqGrid
function createMatchPOPupButton(cellvalue, options, rowObject) {
    var PatientID = '"' + String(rowObject[5]) + '"';
    var FirstName = '"' + String(rowObject[0]) + '"';
    var LastName = '"' + String(rowObject[1]) + '"';
    var total = FirstName + "," + LastName + "," + PatientID;
    return "<input class='blue-btn' value='Match' type='button' id='" + rowObject[5] + "' onclick='OpenPopUpForMatch(" + total + ");' >";
}

//This window open jquery popup to make search.
function OpenPopUpForMatch(FirstName, LastName, PatientID) {
    $("#XeroPatientsMatchSearch").html('<table id="grdXeroPatientsMatchSearch"></table><div id="DivNoRecord2" style="visibility: hidden"><span>No Record</span></div><div id="pagernav2"></div>');

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

   
    if (PatientID == "null") { document.getElementById("XeroContactId").value = "-"; }
    else { document.getElementById("XeroContactId").value = PatientID; }
    $("#MatchDialog").show();
    $("#SearchPatientsXeroButton").show();
    $("#MatchDialog").dialog({
        height: 530,
        width: 800,
        modal: true,
        left: 100,
        top: 50,
        dialogClass: 'DialogClass'
    });
}

//This function returns search result to match Xero patients with Local patients.
function SearchMatchXeroPatientsData(Grid, Pager, GridFill) {
    $("#XeroPatientsMatchSearch").html('<table id="grdXeroPatientsMatchSearch"></table><div id="DivNoRecord2" ><span>No Record</span></div><div id="pagernav2"></div>');

    var FirstName = document.getElementById('FirstNameS').value;
    var LastName = document.getElementById('LastNameS').value;
    var Email = document.getElementById('EmailS').value;

    var chkGrid = Grid.substring(1, Grid);
    $(Grid).jqGrid({

        url: 'PatientFormXERO.aspx?gridFill=' + GridFill + "&FirstName=" + FirstName + "&LastName=" + LastName ,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['First Name', 'Last Name',  'Action'],
        colModel: [
             { name: 'FirstName', index: 'FirstName', editable: false, sorttype: "string", sortable: true, classes: 'wrap1', width: "150" },
             { name: 'LastName', index: 'LastName', editable: false, sortable: true, width: "150" },
         
             { name: 'ContactId', index: 'ContactId', editable: false, sortable: false, align: "center", width: "150", formatter: createMatchButton },
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

//This funcrtion returns matching button html.
function createMatchButton(cellvalue, options, rowObject) {
    var ContactId = '"' + String(rowObject[2]) + '"';
    var FirstName = '"' + String(rowObject[0]) + '"';
    var LastName = '"' + String(rowObject[1]) + '"';
   
    var total = FirstName + "," + LastName + ","+ ContactId;
    return "<input class='blue-btn MatchBtnClass' value='Match' type='button' id='MatchBtn" + rowObject[2] + "' onclick='MatchRecordWithLocalPatients(" + total + ");' >";
}

//This function making matching relation between Xero patients and local user.
function MatchRecordWithLocalPatients(FirstName, LastName, ContactId) {
    var PatientId = document.getElementById('XeroContactId').value;
    var ParameterF = "\"" + ContactId + "\",\"" + PatientId + "\"";
    var FirstNameP = $("#FirstName").prop('title');
    var LastNameP = $("#LastName").prop('title');
    //var EmailP = $("#EmailId").prop('title');
    var Message = "Do you want to match '" + FirstNameP + " " + LastNameP + "'";
    //if (EmailP == "-" || EmailP == "" || EmailP == "null") { Message = Message } else { Message = Message + ", email id '" + EmailP + "'"; }
    Message = Message + " with '" + FirstName + " " + LastName + "'";
    //if (Email == "-" || Email == "" || Email == "null" || Email == null) { Message = Message + "?" } else { Message = Message + ", email id '" + Email + "'?"; }
    var r = confirm(Message);
    if (r == true) {
        $.ajax({
            type: "POST",
            url: "PatientFormXERO.aspx/MatchTwoPatients",
            data: JSON.stringify({ PatientId: PatientId, ContactId: ContactId }),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var returnedstring = data.d;
                if (returnedstring == "Success") {
                    var MatchButton = $(".MatchBtnClass");
                    MatchButton.each(function () {
                        $(this).hide();
                    })
                    $("#MatchBtn" + PatientId).parents('tr').find('td').addClass("ChangeBackground");
                    $("#grdXeroPatients").trigger("reloadGrid");
                    $("#grdMatchedPatients_Xero").trigger("reloadGrid");
                    $("#SearchPatientsXeroButton").hide();
                    alert("Record successfully matched.");
                   // $("#MatchBtn" + PatientsId).after("<input class='blue-btn MatchRemoveBtnClass' value='Remove Match' type='button' id='MatchRemoveBtn" + PatientsId + "'onclick='MatchRemoveRecordWithLocalPatients(" + ParameterF + ");' >");
                }
                else { alert("Oops! There is an error.") }
            }
        });
    }
    else {
        alert("Click on match button if you want to match this Xero patient record with local record.");
    }
}


