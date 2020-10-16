
var DepartmentID = 0;


$('document').ready(function () {
    $("#DepartmentPopUp").hide();
   
   // PopulateCheckBoxList();
    //on click of Add department button show department pop
    $("#btnAddDepartments").click(function () {

        $("#DepartmentPopUp").show('slow');
        $("#SemiTransparentBG").show('slow');
        $("#txtDeptName").focus();

    });


    //On clock of cancel button close the panel popup
    $("#imgClose").click(function () {

        ClosePopUp();
    });

    //On clock of close button close the panel popup
    $("#btnClose").click(function () {

        ClosePopUp();
    });

    //Close the department popup
    function ClosePopUp() {
        $("#DepartmentPopUp").hide('slow');
        $("#SemiTransparentBG").hide('slow');
        $("#txtDeptName").val('');


    }


    $("#btnOkDept").click(function () {

        AddDepartments();
    });


    //Add department name in database
    function AddDepartments() {
        var DepartmentName = $("#txtDeptName").val();

        if (DepartmentName == "") {
            alert("Please enter department name")
        }
        else {
            var postData = new Object();

            postData.DepartmentName = DepartmentName;
            $.ajax({
                type: "POST",
                url: "DepartmentStaff_Add.aspx/SaveDepartments",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    ClearFields();
                    alert("Data sucessfully saved");
                    MM_goToURL('parent', 'DepartmentStaff_Add.aspx'); return document.MM_returnValue;
                }
            });
        }
    }


    //Add Staffs to departments
    $("#btnAdd").click(function () {
        var staff = $("#ddlStaff").val();
        var Dept = [];
        $('.DeptCheckBoxClass input:checked').each(function () {
            var deptValues = $(this).val();
            Dept.push(deptValues);
        });
        var Departments = (Dept).toString();
        if (staff == "Select a staff") {
            alert("Please select a staff");
        }
        else if (Departments == "") {
            alert("Please select departments")
        }
        else {
            var postData = new Object();
            postData.StaffID = staff;
            postData.DepartmentID = Departments;
            $.ajax({
                type: "POST",
                url: "DepartmentStaff_Add.aspx/SaveDepartmentStaff",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    //ClearFields();
                    alert("Data sucessfully saved");
                }
            });
        }
    });

    $("#btnCancel").click(function () {
        ClearFields();
    });

    //Clear the feilds
    function ClearFields() {
        $('.DeptCheckBoxClass input:checkbox').removeAttr('checked');
        $("#MainContent_ddlStaff option:selected").removeAttr("selected");
    }

    //on change of department selected index fill the grid 
    $("#ddlDepartments").live('change', function () {

        DepartmentID = $(this).val();
        BindDataToMerge('#grdBindDataEmployee', '#pagernav', DepartmentID);
    });

    function PopulateCheckBoxList() {
        $.ajax({
            type: "POST",
            url: "DepartmentStaff_Add.aspx/ConfirmMatchedDataCount",
            contentType: "application/json; charset=utf-8",
            data: "{}",
            dataType: "json",
            success: AjaxSucceeded,
            error: AjaxFailed
        });
    }

    function AjaxSucceeded(result) {
        alert('load checkbox list');
    }
    function AjaxFailed(result) {
        alert('Failed to load checkbox list');
    }
   
    $("#ddlStaff").change(function () {
        var staffID = this.value;
       
        if (staffID <= 0) {
            alert("Please select a staff");
        }
        
        else {
            var postData = new Object();
            postData.staffID = staffID;
           
            $.ajax({
                type: "POST",
                url: "DepartmentStaff_Add.aspx/GetDepartmentStaff",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    listObj = response.d;
                    var list = $('#cbDepartment input');
                    list.prop("checked", false);
                    if (listObj != null) {
                        if (listObj.length > 0) {
                          
                          
                            for (var n = 0; n < listObj.length; ++n) {
                               
                                list.each(function (index) {
                                    var chkitem = $(this).val();
                                    
                                    if ((chkitem) == listObj[n].DepartmentID) {
                                        $(this).attr('checked', true);
                                    }
                                });
                            }
                        }
                    }
                   
                }
            });
        }
    });
   
});

//fill the grid with Employee details
function BindDataToMerge(Grid, Pager, DepartmentID) {

    $(Grid).jqGrid('GridUnload');
    $(Grid).jqGrid({

        url: 'DepartmentStaff_Add.aspx?gridFill=' + DepartmentID,
        contentType: "application/json",
        datatype: 'json',
        mtype: "Get",

        colNames: ['EmployeeId', 'EmployeeName', 'Email', 'Access Level'],
        colModel: [
             { name: 'EmployeeId', index: 'EmployeeId', editable: false, sortable: false, align: "center", search: false },
             { name: 'EmployeeName', index: 'EmployeeName', editable: false, sortable: true, search: true },
             { name: 'Email_Address', index: 'Email_Address', editable: false, sortable: true, search: true },
             { name: 'access_level', index: 'access_level', editable: false, sortable: true, search: true },


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
                $("#DivNoRecord").hide();
            }
            else {
                $(Grid).hide();
                $("#DivNoRecord").show();

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



/*function PopulateCheckBoxList() {
    $.ajax({
        type: "POST",
        url: "DepartmentStaff_Add.aspx/GetCheckBoxDetails",
        contentType: "application/json; charset=utf-8",
        data: "{}",
        dataType: "json",
        success: AjaxSucceeded,
        error: AjaxFailed
    });
}
function AjaxSucceeded(result) {
    BindCheckBoxList(result);
}
function AjaxFailed(result) {
    alert('Failed to load checkbox list');
}
function BindCheckBoxList(result) {

    var items = JSON.parse(result.d);
    CreateCheckBoxList(items);
}
function CreateCheckBoxList(checkboxlistItems) {
    var table = $('<table></table>');
    var counter = 0;
    $(checkboxlistItems).each(function () {
        table.append($('<tr></tr>').append($('<td></td>').append($('<input>').attr({
            type: 'checkbox', name: 'chklistitem', value: this.Value, id: 'chklistitem' + counter
        })).append(
        $('<label>').attr({
            for: 'chklistitem' + counter++
        }).text(this.Name))));
    });

    $('#dvCheckBoxListControl').append(table);
}*/