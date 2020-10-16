
function ConfirmDelete() {

    var message = confirm('Are you sure you want to delete this item?');
    if (message != 0) {
        return true;
    }
    else {
        return false;
    }
}

function OnInsert() {
    alert('You have successfully added the record.');
}
function OnUpdate() {
    alert('You have successfully updated the record.');
}

//function to restrict the Special characters 
function Restrictspecialchar(e) {
    var key;
    key = e.which ? e.which : e.keyCode;
    if ((key > 64 && key < 91) || (key > 96 && key < 123) || key == 8 || key == 32 || (key >= 48 && key <= 57) || (key == 009)) {
        return true;
    }
    else {
        return false;
    }
}

//Function for numeric values and decimal value check on keypress by jaswinder on 14th aug 2013
function check_digit(e, obj, intsize, deczize) {

    var keycode;

    if (window.event) keycode = window.event.keyCode;
    else if (e) keycode = e.which;
    else return true;
    var fieldval = (obj.value);
    var dots = fieldval.split(".").length;

    if (keycode == 46) {
        if (dots > 1) {

            return false;
        } else {

            return true;
        }
    }
    if (keycode == 8 || keycode == 9 || keycode == 46 || keycode == 13) // back space, tab, delete, enter 
    {
        return true;
    }
    if ((keycode >= 32 && keycode <= 45) || keycode == 47 || (keycode >= 58 && keycode <= 127)) {
        return false;
    }



    if (fieldval == "0" && keycode == 48)
        return false;

    if (fieldval.indexOf(".") != -1) {
        if (keycode == 46)
            return false;
        var splitfield = fieldval.split(".");



        if (splitfield[1].length >= deczize && keycode != 8 && keycode != 0)
            return false;
    }
    else if (fieldval.length >= intsize && keycode != 46) {
        return false;
    }
    else return true;
}



//function to break the the text after 50 characters.
function lineBreaks(text, maxlength) {
    var len = text.length;
    var pos = -1;
    var replace = true;
    for (var i = maxlength; i < len; i += maxlength) {
        var separator = "\n"
        for (var pos = i; text.charAt(pos) != " "; pos--) {
            if (pos == i - maxlength) {
                pos = i;
                separator += text.charAt(pos);
                len++;
                break;
            }
        }
        text = text.substring(0, pos) + separator + text.substring(pos + 1, len - 1);
        i = pos;
        replace = true;
    }
    return text;
}

//Function to format the date in mm/dd/yyyy 
function convertDate(inputFormat) {
    var d = new Date(parseInt(inputFormat.substring(6, 19)))
    return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join('/');
}


function UploadImageValidate() {
    //var fileName = $("#MainContent_ImageUpload").val();
    var filename = document.getElementById('MainContent_ImageUpload').value;
    var fileExtension = fileName.substr(fileName.indexOf(".") + 1);
    if (fileName == "") {
        alert('Please specify the Image to upload');
        return false;
    }
    else if (fileExtension == "gif" || fileExtension == "jpg" || fileExtension == "png") {
        alert('Image uploaded successfully');
        debugger;
        var imagePath = document.getElementById('HDImageUrl').value;
        var ImgUrl = imagePath + fileName;
        //$("#MainContent_imgPhoto").attr("src", ImgUrl);
        document.getElementById('MainContent_imgPhoto').setAttribute('src', ImgUrl);
        return true;
    }
    else {
        alert("The type of extension should be gif/jpg/png");
        return false;
    }
}


//clear all input fields
function ClearFields() {
    $('input[type=text]').each(function () {
        $(this).val('');
    });
}

function tmt_winHistory(id, s) {
    var d = eval(id) == null || eval(id + ".closed");
    if (!d) { eval(id + ".history.go(" + s + ")"); }
}

//function to validate update patient
//17jan 2014
function UpdatePatientDetails() {
    var IsFocus = "";
    var patientId = 0;
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
    var isFlag = false;
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
    //if (birthday.val() == "") {
    //    if (errString == "")
    //    { IsFocus = birthday }
    //    errString += "\r\nPlease enter birthday date.";

    //}
    if (concierge.val() == "None") {
        if (errString == "")
        { IsFocus = concierge }
        errString += "\r\nPlease select concierge.";
    }
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

    var monthfield = $("#MainContent_txtBirthday").val().split("/")[0];
    var dayfield = $("#MainContent_txtBirthday").val().split("/")[1];
    var yearfield = $("#MainContent_txtBirthday").val().split("/")[2];

    if (yearfield > year) {
        if (errString == "")
        { IsFocus = $("#MainContent_txtBirthday"); }
        errString += "\r\nDate of birth can not be in future";


    }
    if (yearfield == year) {
        if (monthfield > currentMonth) {
            if (errString == "")
            { IsFocus = $("#MainContent_txtBirthday"); }
            errString += "\r\nDate of birth can not be in future";


        }
        else if (monthfield == currentMonth) {
            if (dayfield > currentDay) {
                if (errString == "")
                { IsFocus = $("#MainContent_txtBirthday"); }
                errString += "\r\nDate of birth can not be in future";
            }
        }
    }
    if (monthfield == 2) {
        if (yearfield % 4 == 0) {
            if (dayfield > 29) {
                if (errString == "")
                { IsFocus = $("#MainContent_txtBirthday"); }
                errString += "\r\nDate can not be greater then 29, for Februrary month";
            }
        }
        else if (yearfield % 4 != 0) {
            if (dayfield > 28) {
                if (errString == "")
                { IsFocus = $("#MainContent_txtBirthday"); }
                errString += "\r\nDate can not be greater then 28 ,for Februrary month";
            }
        }
    }
    if (monthfield == 4) {
        if (dayfield > 30) {
            if (errString == "")
            { IsFocus = $("#MainContent_txtBirthday"); }
            errString += "\r\nDate can not be greater then 30 ,for April month";
        }
    }
    if (monthfield == 6) {
        if (dayfield > 30) {
            if (errString == "")
            { IsFocus = $("#MainContent_txtBirthday"); }
            errString += "\r\nDate can not be greater then 30, for June month";
        }
    }
    if (monthfield == 9) {
        if (dayfield > 30) {
            if (errString == "")
            { IsFocus = $("#MainContent_txtBirthday"); }
            errString += "\r\nDate can not be greater then 30, for September month";
        }
    }
    if (monthfield == 11) {
        if (dayfield > 30) {
            if (errString == "")
            { IsFocus = $("#MainContent_txtBirthday"); }
            errString += "\r\nDate can not be greater then 30, for November month";
        }
    }
    if (monthfield > 12 || dayfield > 31) {
        if (errString == "")
        { IsFocus = $("#MainContent_txtBirthday"); }
        errString += "\r\nDate is not in currect format";
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

//Function for check a valid date
//14th April 2014
function isValidDate(date) {
    var valid = true;

    date = date.replace('/-/g', '');

    var month = parseInt(date.substring(0, 2), 10);
    var day = parseInt(date.substring(3, 5), 10);
    var year = parseInt(date.substring(6, 10), 10);

    //Commented by jaswinder
    //var day = parseInt(date.substring(2, 4), 10);
    //var year = parseInt(date.substring(4, 8), 10);

    //Added by jaswinder if is NAN
    if (date != "") {
        if (isNaN(month) || isNaN(year) || isNaN(day)) {
            valid = false
        }
    }
    //End by jaswinder
    if ((month < 1) || (month > 12)) valid = false;
    else if ((day < 1) || (day > 31)) valid = false;
    else if (((month == 4) || (month == 6) || (month == 9) || (month == 11)) && (day > 30)) valid = false;
    else if ((month == 2) && (((year % 400) == 0) || ((year % 4) == 0)) && ((year % 100) != 0) && (day > 29)) valid = false;
    else if ((month == 2) && ((year % 100) == 0) && (day > 29)) valid = false;

    return valid;
}
