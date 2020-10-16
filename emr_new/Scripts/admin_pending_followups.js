var Clinic;
var currentPageNumber = 1;
var PAGE_SIZE = 30;

//A to bind the grid on submit button click
function OnSubmitButtonClick() {  
    var Clinic = ($('#MainContent_ddlClinic option:selected').text()) 
    if (Clinic == "Select Clinic") {
        alert('Please select the clinic');
        return false;
    }
    else {
        BindGrid(1);
    }
}

//  to bind the grid
function BindGrid(data) {
    var Clinic = ($('#MainContent_ddlClinic option:selected').val())
    var OrderBy = ($('#MainContent_ddlOrderBy option:selected').val())
    var postData = new Object();
    postData.Clinic = Clinic;
    postData.OrderBy = OrderBy;
    var date = new Date();

    postData.PageIndex = data;
    $.ajax({
        type: "POST",
        url: "admin_pending_followups.aspx/BindAptGrid",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var listObj = response.d;
            $("#tblClinicList").find("tr:gt(2)").remove();
            var count = 0;
            var rangeDate;
            if (listObj.length > 0) {
                for (var i = listObj.length - 1 ; i >= 0; i--) {
                    if (listObj[i].RangeDate == null) {
                        rangeDate = null;
                    }
                    else rangeDate = convertDate(Date(listObj[i].RangeDate));
                    $("#tblClinicList tr:eq(2)").after("<tr><td><a href='admin_contact_add_pendingfollowups.aspx?FollowUp_ID=" + listObj[i].followup_id + "&patientid=" + listObj[i].patientid + "'>" + listObj[i].Date_Entered + "</a></td>" +
                        "<td><a href='admin_contact_add_pendingfollowups.aspx?FollowUp_ID=" + listObj[i].followup_id + "&patientid=" + listObj[i].patientid + "'>" + listObj[i].RangeDate + "</a></td>" +
                        "<td>" + listObj[i].followup_type_desc + "</td>" +
                        "<td>" + listObj[i].Clinic + "</td>" +
                        "<td>" + listObj[i].EmployeeName + "</td>" +
                        "<td>" + listObj[i].LastName + " " + listObj[i].FirstName + "</td>" +
                        "</tr>");
                    count = Number(listObj[i].RecordCount);
                }
            }



            //For setting current page number and total page number
            currentPageNumber = data;
            var totalPages = Math.ceil(count / PAGE_SIZE);

            $("#MainContent_lblCurrentPage").text(currentPageNumber);
            $("#MainContent_lblTotalPages").text(totalPages);

            //Enable or disable the previous or next button.
            if (totalPages == 1) {

                $("#MainContent_lblCurrentPage").hide();
                $("#MainContent_lblTotalPages").hide();
                $("#pagingtext").hide();
                $("#tdButton").hide();
                $("#tdNoRecord").hide();

                return;
            }
            else if (totalPages == 0) {

                $("#MainContent_lblCurrentPage").hide();
                $("#MainContent_lblTotalPages").hide();
                $("#pagingtext").hide();
                $("#tdNoRecord").show();
                $("#tdButton").hide();
                return;
            }
            else {

                $("#tdButton").show();
                $("#MainContent_lblCurrentPage").show();
                $("#MainContent_lblTotalPages").show();
                $("#pagingtext").show();
                $("#tdNoRecord").hide();
            }

            if (currentPageNumber == 1) {
                $("#Previous").attr("disabled", "disabled");
                if ($("#MainContent_lblCurrentPage") > 0) {
                    $("#Next").attr("disabled", "disabled");
                }
                else {
                    $("#Next").removeAttr("disabled");
                }
            }

            else {
                $("#Previous").removeAttr("disabled");
                if (currentPageNumber == $("#MainContent_lblTotalPages").text())
                    $("#Next").attr("disabled", "disabled");
                else
                    $("#Next").removeAttr("disabled");

            }
        }

    });

}

//Function when page number is change bind the grid
function ChangePage(data) {
    var process = $(data).val();

    var page = Number($("#MainContent_lblCurrentPage").text());
    if (process == "Next") {
        BindGrid(page + 1);
    }
    if (process == "Previous") {
        BindGrid(page - 1);
    }
}

