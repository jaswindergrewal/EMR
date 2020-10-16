var PatientId;
var PatientIDbyAptFollowUps;
var StaffId;
var url;

//function for changing parent URL
function change_parent_url(url) {
    document.location = url;
}


//function for loading the url in ifram
function loader(url) {
    var iframe = $("#MainContent_PageContents");

    $("#loading-div-background").show(); // show wait

    iframe.on('load', function () {
        $("#loading-div-background").hide(); // remove wait, done!
    });
    setTimeout(function () {
        iframe.attr('src', url);
    },
            400);
}


$("document").ready(function () {
    PatientId = $("#HDPatientID").val();
    PatientIDbyAptFollowUps = $("#HDPatientIdByAptfollowUps").val();
    StaffId = $("#HDStaffID").val();
    var postData = new Object();
    postData.PatientId = PatientId;
    // postData.StaffId = StaffId;
    $.ajax({
        type: "POST",
        url: "Manage.aspx/CheckPatientMed_renewalDate",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var listObj = response.d;
            var strAlert = "";
            //show alert in case of patient QB match and if patient dont have lab review within one year.
            if (listObj != null) {
                if (listObj.RenewalDate != null) {
                    var RenewalDate;

                    RenewalDate = new Date(convertDate(listObj.RenewalDate));
                    var Currentday = new Date();
                    var Today = ((Currentday.getMonth() + 1) + '/' + Currentday.getDate() + '/' + Currentday.getFullYear());
                    var current = new Date(Today);
                    if (RenewalDate < current) {
                        strAlert = "Renewal date for patient is expired.";
                    }
                }
                else {
                    if (listObj.Medical == true) {
                        strAlert = "No renewal date entered.";
                    }
                }

                //var splitstring = listObj.split(',');
                //var Match = splitstring[0];
                //var LabReview = splitstring[1];
                //if (Match != "") {
                //    strAlert = "This patient is not matched in Quick Books.<br><br>Click on Match QuickBooks in the Admin menu to match this patient.<br><br>";
                //}
                //if (LabReview =="0")
                //{
                //    var strlabreview="This patient has had not a lab review for the past one year."
                //    strAlert = strAlert + strlabreview.fontcolor("red");
                //}
                if (strAlert != "") {

                    alert(strAlert);
                }
            }
        }
    });
    if (document.getElementById("hidAffiliate").value == "yes") {
        alert("This patient is an affiliate!");
    }
    //accordion
    $("#accordion").accordion({
        collapsible: true,
        heightStyle: 'content'
    });
    //loads tickets
    getTickets();
    //Link's color change onclick event
    $('a').bind("click", function () {
        $(this).css('background-color', 'rgb(0, 51, 102)');
        $(this).css('color', 'white');
        $('a').not(this).css('background-color', '');
        $('a').not(this).css('color', '');
        if ($("#HDSpecialAttention").val() > 0) {
            if ($("#lnkSpecialAttention").click(function () {
                $("#lnkSpecialAttention").css('color', 'red');
                $('a').not($("#lnkSpecialAttention")).css('color', '');
            }));
            $("#lnkSpecialAttention").css('color', 'red');
        }
    });
    //link color change in Ticket's div
    $('#TicketsDiv ').delegate('a', 'click', function () {
        $(this).css('background-color', 'rgb(0, 51, 102)');
        $(this).css('color', 'white');
        $('a').not(this).css('background-color', '');
        $('a').not(this).css('color', '');
    });
    //Images change on click
    $('.ui-accordion-header').bind("click", function () {
        $(this).css('background', 'url("images/expand.jpg") no-repeat');
        $(this).css('background-color', 'rgb(214, 183, 129)');
        $('.ui-accordion-header').not(this).css('background', 'url("images/collapse.jpg") no-repeat');

        $('.ui-accordion-header').not(this).css('background-color', 'rgb(214, 183, 129)');
    });
    //Add Photo
    $("#ImageButton").click(function () {
        url = "AddPhoto.aspx?PatientID=" + PatientId;
        loader(url);
    });
    //list personal information section
    $("#lnkPersonalInfo").bind("click", function () {
        url = "PatientInfo.aspx?patientid=" + PatientId;
        loader(url);
    });

    $("#lnkStatusLog").bind("click", function () {
        url = "CalendarPatientLog.aspx?PatientID=" + PatientId;
        loader(url);
    });
    
    $("#lnkScanUpload").bind("click", function () {
        url = "ScansUploads.aspx?patientid=" + PatientId;
        loader(url);
    });
    //link color'll turn red if special attention count is greater then 0
    if ($("#HDSpecialAttention").val() > 0) {
        $("#lnkSpecialAttention").css('color', 'red');
    }
    $("#lnkSpecialAttention").bind("click", function () {
        url = "SpecialAttention.aspx?PatientID=" + PatientId;
        loader(url);
    });
    $("#lnkAppointment").bind("click", function () {
        url = "Appointments.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkPrintOvu").bind("click", function () {
        url = "DictationConsole/OVUForm.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkContactRecord").bind("click", function () {
        url = "Contacts.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkPrescrption").bind("click", function () {
        url = "PresrcriptionList1.aspx?patientid=" + PatientId + "&MasterPage=~/sub.master";
        loader(url);
    });
    //$("#lnktest").bind("click", function () {
    //    url = "PresrcriptionList1.aspx?patientid=" + PatientId + "&MasterPage=~/sub.master";
    //    loader(url);
    //});

    $("#lnkLab").bind("click", function () {
        url = "Lab_Launch_short.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkLabReport").bind("click", function () {
        url = "LabSSRS/Default.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkShareFile").bind("click", function () {
        url = "ShareFile.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkSummary").bind("click", function () {
        url = "SummaryReport.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkCriticalTask").bind("click", function () {
        url = "Patients_CriticalTasks.aspx?patientid=" + PatientId + "&MasterPage=~/sub.master";
        loader(url);
    });
    $("#lnkProblemList").bind("click", function () {
        url = "ProblemList.aspx?patientid=" + PatientId + "&MasterPage=~/sub.master";
        loader(url);
    });
    $("#lnkFollowups").bind("click", function () {
        url = "PendingFollowUps.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkClinicVisits").bind("click", function () {
        url = "OldVisits.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkVitals").bind("click", function () {
        url = "vitals.aspx?patientid=" + PatientId;
        loader(url);
    });
    $("#lnkOpenInvoice").bind("click", function () {
        if ($("#MainContent_HDQBCount").val() > 0) {
            url = "OpenInvoices.aspx?patientid=" + PatientId;
            loader(url);
        }
        else {
            url = "qb_match.aspx?patientid=" + PatientId;
            loader(url);
        }
    });
    $("#lnkAllergies").bind("click", function () {
        url = "allergies_edit.aspx?patientid=" + PatientId;
        loader(url);
    });


    //list autoship section
    $("#lnkOrder").bind("click", function () {
        url = "autoship/shortmanage.aspx?PatientID=" + PatientId + "&StaffID=" + StaffId + "&MasterPage=~/LabSSRS/sub.master";
        loader(url);
    });
    $("#lnkManagement").bind("click", function () {
        url = "autoship/AutoShip.aspx?" + "StaffID=" + StaffId + "&MasterPage=sub.master";
        loader(url);
    });
    $("#lnkDailyShipment").bind("click", function () {
        url = "http://lmcsql/Reports/Pages/Report.aspx?ItemPath=%2fAutoship%2fDaily+Shipments";
        loader(url);
    });
    $("#lnkOpenOrders").bind("click", function () {
        url = "http://lmcsql/Reports/Pages/Report.aspx?ItemPath=%2fAutoship%2fOpen+Orders";
        loader(url);
    });


    //list AestheticNotes section
    $("#lnkAestheticNotes").bind("click", function () {
        url = "AestheticNotes.aspx?PatientID=" + PatientId;
        loader(url);
    })
    $("#lnkAestheticFollowUp").bind("click", function () {
        url = "AestheticFollowUps.aspx?PatientID=" + PatientId;
        loader(url);
    });


    //list Admin section
    $("#lnkCloseAccount").bind("click", function () {
        url = "EndMEdical.aspx?PatientID=" + PatientId + "&StaffID=" + StaffId;
        loader(url);
    });
    $("#lnkMatchQuickBook").bind("click", function () {
        url = "qb_match.aspx?PatientID=" + PatientId + "&StaffID=" + StaffId;
        loader(url);
    });
    $("#lnkRenewalExecption").bind("click", function () {
        url = "RenewalException.aspx?PatientID=" + PatientId + "&StaffID=" + StaffId;
        loader(url);
    });
});

//function to get tickets
function getTickets() {
    if (PatientId == "") {
        PatientId = PatientIDbyAptFollowUps;
    }
    var Currentday = new Date();
    var Today = ((Currentday.getMonth() + 1) + '/' + Currentday.getDate() + '/' + Currentday.getFullYear());

    var postData = new Object();
    postData.PatientID = PatientId;
    $.ajax({
        type: "POST",
        url: "Manage.aspx/GetTickets",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",

        success: function (response) {

            var listObj = response.d;
            if (listObj.length > 0) {

                $("#TicketsDiv").append(" <table id='tblTicket'><tbody></tbody></table>");
                for (var i = 0; i < listObj.length ; i++) {
                    var Due;
                    if (listObj[i].DueDate != null) {
                        Due = new Date(convertDate(listObj[i].DueDate));
                    }
                    var current = new Date(Today);
                    if (listObj[i].FollowUp_Completed_YN == false && (listObj[i].DueDate == null || Due >= current)) {

                        if (listObj[i].Severity == 1) {
                            $("#tblTicket tbody").append("<tr >" +
               "<td >" + "<font face='wingdings 3' color='red'>p</font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 followupActiveColor' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
           "</tr>");
                        }
                        else if (listObj[i].Severity == 2) {
                            $("#tblTicket tbody").append("<tr >" +
     "<td >" + "<font face='wingdings 3' color='black'></font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 followupActiveColor' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
 "</tr>");
                        }
                        else if (listObj[i].Severity == 3) {
                            $("#tblTicket tbody").append("<tr >" +
                "<td >" + "<font face='wingdings 3' color='black'></font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 followupActiveColor' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
            "</tr>");
                        }
                        else {
                            $("#tblTicket tbody").append("<tr >" +
                                              "<td >" + "<a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 followupActiveColor' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
                                          "</tr>");
                        }
                    }
                    else if (listObj[i].FollowUp_Completed_YN == false && Due < current) {
                        if (listObj[i].Severity == 1) {
                            $("#tblTicket tbody").append("<tr >" +
               "<td >" + "<font face='wingdings 3' color='red'>p</font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 BlueFont' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
           "</tr>");
                        }
                        else if (listObj[i].Severity == 2) {
                            $("#tblTicket tbody").append("<tr >" +
     "<td >" + "<font face='wingdings 3' color='black'></font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 BlueFont' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
 "</tr>");
                        }
                        else if (listObj[i].Severity == 3) {
                            $("#tblTicket tbody").append("<tr >" +
                "<td >" + "<font face='wingdings 3' color='black'></font><a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 BlueFont' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
            "</tr>");
                        }
                        else {
                            $("#tblTicket tbody").append("<tr >" +
                                              "<td >" + "<a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 BlueFont' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
                                          "</tr>");
                        }

                    }
                    else {
                        $("#tblTicket tbody").append("<tr >" +
                                               "<td >" + "<a id=" + listObj[i].FollowUp_ID + " class='a_class fontsize_11 striketext' onclick='PatientTicket(this.id)'>" + listObj[i].FollowUp_Subject + "</a>" + "</td>" +
                                           "</tr>");
                    }
                }
                $("#tblTicket tbody").append("<tr >" +
              "<td >" + "<hr width='95px' align='center'/>" + "<a id=" + 0 + " class='a_class fontsize_11' onclick='PatientTicket(this.id)'>" + "New Ticket" + "</a>" + "</td>" +
          "</tr>");
            }
            else {
                $("#TicketsDiv").append(" <table id='tblTicket'><tbody></tbody></table>");
                $("#tblTicket tbody").append("<tr>" +
              "<td >" + "<a id=" + 0 + " class='a_class fontsize_11' onclick='PatientTicket(this.id)'>" + "New Ticket" + "</a>" + "</td>" +
          "</tr>");
            }
        }
    });
}


//function to load ticket
function PatientTicket(ID) {
    if (ID != 0) {
        var postData = new Object();
        postData.SessionValue = ID;
        $.ajax({
            type: "POST",
            url: "Manage.aspx/SetSession",
            data: JSON.stringify(postData),
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            success: function () {
            }
        });
        url = "Ticket.aspx?PatientID=" + PatientId;
        loader(url);
    }
    else {
        url = "NewTicket.aspx?PatientID=" + PatientId + "&IsAutoShipTicket=False";
        loader(url);
    }
}


/*********************************************/
//Creating custom alert box

var ALERT_TITLE = "Alert!";
var ALERT_BUTTON_TEXT = "Ok";

if (document.getElementById) {
    window.alert = function (txt) {
        createCustomAlert(txt);
    }
}

function createCustomAlert(txt) {
    d = document;

    if (d.getElementById("modalContainer")) return;

    mObj = d.getElementsByTagName("body")[0].appendChild(d.createElement("div"));
    mObj.id = "modalContainer";
    mObj.style.height = d.documentElement.scrollHeight + "px";

    alertObj = mObj.appendChild(d.createElement("div"));
    alertObj.id = "alertBox";
    if (d.all && !window.opera) alertObj.style.top = document.documentElement.scrollTop + "px";
    alertObj.style.left = (d.documentElement.scrollWidth - alertObj.offsetWidth) / 2 + "px";
    alertObj.style.visiblity = "visible";

    h1 = alertObj.appendChild(d.createElement("h1"));
    h1.appendChild(d.createTextNode(ALERT_TITLE));

    msg = alertObj.appendChild(d.createElement("p"));
    //msg.appendChild(d.createTextNode(txt));
    msg.innerHTML = txt;

    btn = alertObj.appendChild(d.createElement("a"));
    btn.id = "closeBtn";
    btn.appendChild(d.createTextNode(ALERT_BUTTON_TEXT));
    btn.href = "#";
    btn.focus();
    btn.onclick = function () { removeCustomAlert(); return false; }

    alertObj.style.display = "block";

}

function removeCustomAlert() {
    document.getElementsByTagName("body")[0].removeChild(document.getElementById("modalContainer"));
}