
var PAGE_SIZE;
var count = 0;
var currentPageNumber = 1;

//Function to format the date in mm/dd/yyyy 
//and to minus the days from the current date  as we need to show this date on header
//added by jaswinder on 12 th aug 2013

function convertDate(inputFormat, dayMinus) {
    var d = inputFormat;
    d = [d.getDate(), d.getMonth() + 1, d.getFullYear()].join('/');
    dateParts = d.split('/');

    year = dateParts[2];

    month = parseInt(dateParts[1], 10) - 1;

    day = parseInt(dateParts[0], 10) - dayMinus;


    newDate = new Date(year, month, day);

    year = newDate.getFullYear();

    month = newDate.getMonth() + 1;

    day = newDate.getDate();

    formattedDate = month + '/' + day + '/' + year;
    return formattedDate;
}


$("document").ready(function () {
    // Bind grid initially with page number as 1 
    PAGE_SIZE = $("input[id$=hdnPageSize]").val();
    BindGrid(1);
});

//function to bind grid
function BindGrid(data) {
    var postData = new Object();
    postData.PageSize = PAGE_SIZE;
    var date = new Date();
    //Set the header Dates
    $("#StaffList").html("<tr bgcolor='#D6B781'><td colspan='6'><strong>Last 5 Days Prescription List </strong> &nbsp;&nbsp; <img src='images/Patient.png' height='20px' width='20 px' /> <b>-Prescription provides <img src='images/Cross.png' height='20px' width='20 px' />- Not provided </b></td></tr> <tr>" +
    "<td width='25%'><strong>Patient Name </strong></td>" +
    "<td width='15%'><strong>Today's - " + convertDate(date, 0) + "</strong></td>" +
    "<td width='15%'><strong>Yesterday's - " + convertDate(date, 1) + "</strong></td>" +
    "<td width='15%'><strong>2 Days Ago - " + convertDate(date, 2) + "</strong></td>" +
    "<td width='15%'><strong>3 Days Ago - " + convertDate(date, 3) + "</strong></td>" +
    "<td width='15%'><strong>4 Days Ago - " + convertDate(date, 4) + "  </strong></td></tr>" +
    "<tr><td colspan='6' style='color: red; display:none;' id='tdNoRecord' align='center' visible='false'><strong>No record found !</strong></td></tr> ");
    postData.PageIndex = data;
    $.ajax({
        type: "POST",
        url: "admin_5Days_prescription.aspx/BindAptGrid",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var listObj = response.d;
            if (listObj != null) {
                for (var i = listObj.length - 1 ; i >= 0; i--) {
                    $("#StaffList tr:eq(1)").after("<tr><td><a href='manage.aspx?PatientID=" + listObj[i].PatientID + "'>" + listObj[i].LastName + " " + listObj[i].FirstName + "</a></td>" +
                        (listObj[i].ToDay == 'YES' ? ("<td align='center'>" + "<img src='images/Patient.png' height='20px' width='20 px' />" + "</td>") : ("<td align='center'>" + "<img src='images/Cross.png' height='20px' width='20 px' />" + "</td>")) +
                        (listObj[i].OneDay == 'YES' ? ("<td align='center'>" + "<img src='images/Patient.png' height='20px' width='20 px' />" + "</td>") : ("<td align='center'>" + "<img src='images/Cross.png' height='20px' width='20 px' />" + "</td>")) +
                        (listObj[i].TwoDay == 'YES' ? ("<td align='center'>" + "<img src='images/Patient.png' height='20px' width='20 px' />" + "</td>") : ("<td align='center'>" + "<img src='images/Cross.png' height='20px' width='20 px' />" + "</td>")) +
                        (listObj[i].ThreeDay == 'YES' ? ("<td align='center'>" + "<img src='images/Patient.png' height='20px' width='20 px' />" + "</td>") : ("<td align='center'>" + "<img src='images/Cross.png' height='20px' width='20 px' />" + "</td>")) +
                        (listObj[i].FourDay == 'YES' ? ("<td align='center'>" + "<img src='images/Patient.png' height='20px' width='20 px' />" + "</td>") : ("<td align='center'>" + "<img src='images/Cross.png' height='20px' width='20 px' />" + "</td>")) +
                        "</tr>");
                    count = Number(listObj[i].RecordCount);
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

                    return;
                }
                else if (totalPages == 0) {

                    $("#MainContent_lblCurrentPage").hide();
                    $("#MainContent_lblTotalPages").hide();
                    $("#pagingtext").hide();
                    $("#tdNoRecord").show();
                    return;
                }
                else {

                    $("#tdButton").show();
                    $("#MainContent_lblCurrentPage").show();
                    $("#MainContent_lblTotalPages").show();
                    $("#pagingtext").show();
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
            else {
                window.location.href = location.href;
                window.location.replace("error.aspx");
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
