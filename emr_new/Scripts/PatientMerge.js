//bind the data for first tab initially on page load

$(document).ready(function () {

    BindDataToMerge('#grdBindDataToMerge', '#pagernav1', '1');
});




//Bind the data to merge patient 
function BindDataToMerge(Grid, Pager, GridFill) {
        
    $(Grid).jqGrid({

        url: 'PatientMerged_UnMerged.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['Patientid', 'firstName', 'LastName', 'email', 'clinic', 'WorkPhone', 'sex', 'actualPatientID'],
        colModel: [
             { name: 'Patientid', index: 'Patientid', editable: false, title: 'open', sortable: false, align: "center", formatter: createCheckBox, search: false },
             { name: 'firstName', index: 'firstName', editable: false, sortable: true, search: true },
             { name: 'LastName', index: 'LastName', editable: false, sorttype: "string", sortable: true, search: true },
             { name: 'email', index: 'email', editable: false, sorttype: "string", sortable: true, search: true },
             { name: 'clinic', index: 'clinic', editable: false, sortable: true, search: true },
             { name: 'WorkPhone', index: 'WorkPhone', editable: false, sorttype: "string", sortable: true, search: false },
             { name: 'sex', index: 'sex', editable: false, sortable: true, search: false },
             { name: 'actualPatientID', index: 'actualPatientID', editable: false, sortable: true, hidden: true, search: false },

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

            for (var i = 0; i < rows.length; i++) {
                var PatientID = $(Grid).getCell(rows[i], "actualPatientID");
                if (PatientID == $('#hdnPatientID').val()) {
                    $(Grid).jqGrid('setRowData', rows[i], false, { color: 'white', weightfont: 'bold', background: 'green' });
                    $('#' + PatientID).prop('checked', true);

                }
                else if (PatientID == $('#hdnMergedPatientID').val()) {
                    $(Grid).jqGrid('setRowData', rows[i], false, { color: 'white', weightfont: 'bold', background: 'blue' });
                    $('#' + PatientID).prop('checked', true);
                }

            }


        }

    }).navGrid(Pager,
        {
            edit: false, add: false, del: false, search: false, refresh: false
        }

   ); $(Grid).jqGrid('navGrid', Pager,
        { search: false }, { width: 'auto', }).jqGrid('navButtonAdd', Pager,
        {
            caption: '',
            buttonicon: 'ui-icon-search',
            onClickButton: function () {
                $(Grid).jqGrid('searchGrid', { closeAfterSearch: true });
                $(Grid).jqGrid('setGridParam', { datatype: "json", page: 1 }).trigger("reloadGrid")
            },
        }
        );
}





//Create merged check button
function createCheckBox(cellvalue, options, rowObject) {
    
    return "<input class='chkbutton' type='checkbox' id='" + rowObject[0] + "' value='Open' onclick='fnmergedPatient(this,"+options.rowId+","+rowObject[1]+");'>";
}

//function for unmerging patient
function fnmergedPatient(obj, rowId,PatientName) {
    
    var buttonId = $(obj).attr("id");

    if (buttonId != 0) {
        if ($(obj).prop('checked')) {
            if ($('#hdnPatientID').val() == "0") {

                $('#hdnPatientID').val(buttonId);
                $('#hdnPatientName').val(PatientName);
                $("#grdBindDataToMerge").jqGrid('setRowData', rowId, false, { color: 'white', weightfont: 'bold', background: 'green' });
                alert('Now select the patient which you want to merge with ' + PatientName);
            }
            else if ($('#hdnMergedPatientID').val() == "0") {
                $('#hdnMergedPatientID').val(buttonId);
                $('#hdnMergedPatientName').val(PatientName);
                $('#hdnRowId').val(rowId);
                $("#grdBindDataToMerge").jqGrid('setRowData', rowId, false, { color: 'white', weightfont: 'bold', background: 'blue' });
                alert('Now click on merge button to merge the data of ' + PatientName + ' with ' + $('#hdnPatientName').val());

            }
            else if ($('#hdnPatientID').val() != "0" && $('#hdnMergedPatientID').val() != "0") {
                $(obj).prop('checked', false);
                alert('You can not select the more patient');
            }

        }
        else {

            if ($('#hdnMergedPatientID').val() == buttonId) {
                $(obj).prop('checked', false);
                $('#hdnMergedPatientID').val('0');
                $('#hdnMergedPatientName').val('');
                $("#grdBindDataToMerge").jqGrid('setRowData', rowId, false, { color: 'black', weightfont: 'bold', background: 'white' });
            }


            else if ($('#hdnPatientID').val() == buttonId) {
                //$(obj).prop('checked', false);
                $('.chkbutton').prop('checked', false);
                $('#hdnPatientID').val('0');
                $('#hdnPatientName').val('');
                var rowIdMerged = $('#hdnRowId').val();
                $("#grdBindDataToMerge").jqGrid('setRowData', rowId, false, { color: 'black', weightfont: 'bold', background: 'white' });
                $("#grdBindDataToMerge").jqGrid('setRowData', rowIdMerged, false, { color: 'black', weightfont: 'bold', background: 'white' });

                $('#hdnMergedPatientID').val('0');
                $('#hdnMergedPatientName').val('');
                $('#hdnRowId').val('');
            }
        }
        

    }
    
}

//function for merging patient
function fnMergePatientData() {

    if ($('#hdnPatientID').val() == "0" || $('#hdnMergedPatientID').val() == "0")
    {
        alert('Please select the patients to merge');
    }
    else
        {

        if (buttonId != 0) {
            var postData = new Object();
            postData.ExistingPatient = $('#hdnMergedPatientID').val();
            postData.NewAssignPatient = $('#hdnPatientID').val();
            postData.StaffID = $('#hdnStaff').val();
            $.ajax({
                type: "POST",
                url: "PatientMerged_UnMerged.aspx/MergedPatientData",
                data: JSON.stringify(postData),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                cache: false,
                success: function (response) {
                    var listObj = response.d;
                    if (listObj == true) {
                        alert("Patient Mergerd");
                    }
                    else {
                        alert("Some problem occured while merging patient");
                    }

                },
                error: function () {
                    alert("Fail to load data.");
                }
            });
        }
    }
}



//Bind the data for Merged patient list
function BindDataToUnMerged(Grid, Pager, GridFill) {

    $(Grid).jqGrid({

        url: 'PatientMerged_UnMerged.aspx?gridFill=' + GridFill,
        contentType: "application/json",
        datatype: 'json',
        mtype: "GET",

        colNames: ['MergedPatientID', 'firstName', 'LastName','email', 'WorkPhone', 'sex', 'ActualPatient', 'actualPatientID'],
        colModel: [
             { name: 'MergedPatientID', index: 'MergedPatientID', editable: false, title: 'open', sortable: false, align: "center", formatter: createMergeButton, search: false },
             { name: 'firstName', index: 'firstName', editable: false, sortable: true ,search:true},
             { name: 'LastName', index: 'LastName', editable: false, sorttype: "string", sortable: true,  search:true},
             { name: 'email', index: 'email', editable: false, sorttype: "string", sortable: true,search:true },
             { name: 'WorkPhone', index: 'WorkPhone', editable: false, sorttype: "string", sortable: true, search:false },
             { name: 'sex', index: 'sex', editable: false, sortable: true, search: false },
             { name: 'ActualPatient', index: 'ActualPatient', editable: false, sortable: true, search: false },
             { name: 'actualPatientID', index: 'actualPatientID', editable: false, sortable: true, hidden: true, search: false },
             
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

   ); $(Grid).jqGrid('navGrid', Pager,
        { search: false }, { width: 'auto', }).jqGrid('navButtonAdd', Pager,
        {
            caption: '',
            buttonicon: 'ui-icon-search',
            onClickButton: function () {
                $(Grid).jqGrid('searchGrid', { closeAfterSearch: true });
                $(Grid).jqGrid('setGridParam', { datatype: "json", page: 1 }).trigger("reloadGrid")
            },
        }
        );
}

//Create unmerged button
function createMergeButton(cellvalue, options, rowObject) {
    return "<input class='blue-btn' type='button' id='" + rowObject[0] + "' value='Open' onclick='fnunmergedPatient(this); return false;'>";
}

//function for unmerging patient
function fnunmergedPatient(obj) {

    var buttonId = $(obj).attr("id");
    
    if (buttonId != 0) {
        var postData = new Object();
        postData.MergedPatientID = buttonId;

        $.ajax({
            type: "POST",
            url: "PatientMerged_UnMerged.aspx/UnMergedPatient",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            cache: false,
            success: function (response) {
                var listObj = response.d;
                if (listObj == true) {
                    alert("Patient UN-Mergerd");
                }
                else {
                    alert("Some problem occured while unmerging patient");
                }

            },
            error: function () {
                alert("Fail to load data.");
            }
        });

    }
}



