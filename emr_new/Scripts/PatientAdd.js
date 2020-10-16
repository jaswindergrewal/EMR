var ReturnCheckduplicatePatient = false;
var patientId = 0;
//Validation for patients before adding the data
function AddPatientDetails() {
   
    var key = "PatientID";
    var filter = /^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$/;
    var query = window.location.search.substring(1);
    //alert(query);
    var vars = query.split("&");
    for (var i = 0; i < vars.length; i++) {
        var pair = vars[i].split("=");
        if (pair[0] == key) {
            patientId = pair[1];
        }
    }

    var emailId = $(".emailClass");
    var firstName = $(".firstName");
    var lastName = $(".lastName");
    var birthday = $(".birthday");
    var clinic = $(".clinic");
    var concierge = $(".concierge");
    var middleInitial=$(".middleInitial");
    var errString = "";

    if (firstName.val() == "") {
        if (errString == "")
        { IsFocus = firstName }
        errString += "Please enter first name.";

    }

    if (lastName.val() == "") {
        if (errString == "")
        { IsFocus = lastName }
        errString += "\r\nPlease enter last name.";

    }

    if (emailId.val() == "") {
        if (errString == "")
        { IsFocus = emailId }
        errString += "\r\nPlease enter email id.";

    }
    
    //if (concierge.val() == "None") {
    //    if (errString == "")
    //    { IsFocus = concierge }
    //    errString += "\r\nPlease select concierge.";
    //}

    if (clinic.val() == "Select a clinic") {
        if (errString == "")
        { IsFocus = clinic }
        errString += "\r\nPlease select clinic.";
    }

    if (emailId.val() != "") {
        if (!filter.test(emailId.val())) {
            if (errString == "")
            { IsFocus = emailId }
            errString += "\r\nPlease enter valid email id.";
        }

    }

    //Date Of Birth Validation 
    var currentDate = new Date();
    var currentMonth = currentDate.getMonth() + 1;    //since month indexing starts with 0       
    var currentDay = currentDate.getDate();
    var year = currentDate.getFullYear();

    var monthfield = birthday.val().split("/")[0];
    var dayfield = birthday.val().split("/")[1];
    var yearfield = birthday.val().split("/")[2];

    if (yearfield > year) {
        if (errString == "")
        { IsFocus = birthday }
        errString += "\r\nDate of birth can not be in future";


    }
    if (yearfield == year) {
        if (monthfield > currentMonth) {
            if (errString == "")
            { IsFocus = birthday }
            errString += "\r\nDate of birth can not be in future";


        }
        else if (monthfield == currentMonth) {
            if (dayfield > currentDay) {
                if (errString == "")
                { IsFocus = birthday }
                errString += "\r\nDate of birth can not be in future";
            }
        }
    }
    if (monthfield == 2) {
        if (yearfield % 4 == 0) {
            if (dayfield > 29) {
                if (errString == "")
                { IsFocus = birthday }
                errString += "\r\nDate can not be greater then 29, for Februrary month";
            }
        }
        else if (yearfield % 4 != 0) {
            if (dayfield > 28) {
                if (errString == "")
                { IsFocus = birthday }
                errString += "\r\nDate can not be greater then 28 ,for Februrary month";
            }
        }
    }
    
    if (monthfield == 4 || monthfield == 6 || monthfield == 9 || monthfield == 11) {
        if (dayfield > 30) {
            if (errString == "")
            { IsFocus = birthday }
            errString += "\r\nDate can not be greater then 30 days";
        }
    }
   
  
    if (monthfield > 12 || dayfield > 31) {
        if (errString == "")
        { IsFocus = birthday }
        errString += "\r\nDate is not in correct format";
    }

    if (errString == "") {
       
        CheckduplicatePatients();
        return ReturnCheckduplicatePatient;
       
    }
    else {
        alert(errString);
        IsFocus.focus();
        return false;
      

    }
   
}


//method to check duplicate patient in database if firstname,lastname and middleinitials are same.

function CheckduplicatePatients() {
    var firstName = $(".firstName");
    var lastName = $(".lastName");
    var middleInitial = $(".middleInitial");
    
    var postData = new Object();
    var isFlag = false;
    postData.firstName = firstName.val();
    postData.lastName = lastName.val();
    postData.middleInitial = middleInitial.val();
    postData.patientId = patientId;
    $.ajax({
        type: "POST",
        async: false,
        url: "Patient_Add.aspx/GetDuplicatepatient",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {

            var result = response.d;
            if (result == true) {
                alert("This patient is already in the system (First name,last name and middle initial should not be same).  This includes inactive patients.");
                isFlag = false;
            }
            else { isFlag = true; }


        },
        error: function (obj) {

            alert(obj.responseText);
        }

    });
    if (isFlag == false) {
        ReturnCheckduplicatePatient = false;
        
    }
    else {
        ReturnCheckduplicatePatient = true;
       
    }
}

//function to copy the shipping info to billing contact info
function showHideDropDowns() {
    if ($("#chkCopyAddress").is(':checked')) {
        var txtShipAddress = $("#txtShipAddress").val();
        $("#txtBillAddress").val(txtShipAddress);
        var txtShipCity = $("#txtShipCity").val();
        $("#txtBillCity").val(txtShipCity);
        var txtShipZip = $("#txtShipZip").val();
        $("#txtBillZip").val(txtShipZip);
        var ddlShipState = $("#ddlShipState").val();

        $("#ddlBillState").val(ddlShipState);
    }
}
