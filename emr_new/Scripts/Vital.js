var PatientID;
var vitalID;
$("document").ready(function () {
    PatientID = $("#MainContent_HDPatientID").val();
    BindVitalDetails();   
    $("#btnUpdate").hide();
    $("#btnCancle").hide();
    //clear all fields and redirect to add new vital div
    $("#btnCancle").click(function () {
        ClearFields();
        $("#AddVitalDiv").show();
        $("#btnUpdate").hide();
        $("#btnSubmit").show();
        $("#btnCancle").hide();
    });
});
//bind the vital data for patients
function BindVitalDetails() {   
    var postData = new Object();
    postData.PatientID = PatientID;   
    $.ajax({
        type: "POST",
        url: "Vitals.aspx/BindVitalDetails",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            $("#VitalDetailsDiv").html("");
            var listObj = response.d;
            if (listObj.length > 0) {
                for (var i = 0; i < listObj.length ; i++) {                                        
                    if (listObj[i].WaistHipRatio == null) {
                        listObj[i].WaistHipRatio = "";
                    }                                       
                    if (listObj[i].BMI == null) {
                        listObj[i].BMI = "";
                    }                   
                    $("#VitalDetailsDiv").append(" <table width='337px' border='0' cellpadding='3' cellspacing='0' class='border'>" +

                    "<tr bgcolor='#D6B781'>" +
                        "<td width='85' bgcolor='#D6B781'><strong>Date Taken</strong></td>" +
                        "<td colspan='4' bgcolor='#D6B781'>" + listObj[i].Date_Entered + "</td>" +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong>Weight (lbs)</strong></td>" +
                        "<td width='80'>" + listObj[i].Wgt + "</td>" +
                        "<td width='7'>&nbsp;</td>" +
                        " <td width='85'><strong>Height (in)</strong></td>" +
                        "<td width='80'>" + listObj[i].Height + "</td> " +
                    "</tr>" +

                    "<tr><td colspan='5'>&nbsp;</td></tr>" +

                    "<tr>" +
                        "<td width='85'><strong>BP</strong></td>" +
                        "<td>" + listObj[i].BloodPres + "</td>" +
                        "<td>&nbsp;</td>" +
                        "<td width='85'><strong>Temp</strong></td>" +
                        "<td>" + listObj[i].Temperature + "</td> " +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong>Pulse</strong></td>" +
                        "<td>" + listObj[i].Pulse + "</td>" +
                        "<td >&nbsp;</td>" +
                        "<td  width='85'></td>" +
                        "<td></td>" +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'>&nbsp;</td>" +
                        " <td >&nbsp;</td>" +
                        " <td >&nbsp;</td>" +
                        "<td  width='85'>&nbsp;</td>" +
                        "<td>&nbsp;</td>" +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong>Waist Cir</strong></td>" +
                        "<td>" + listObj[i].Waist_Circm + "</td>" +
                        "<td>&nbsp;</td>" +
                        " <td width='85'><strong>BMI</strong></td>" +
                        "<td  width='85'>" + listObj[i].BMI + "</td> " +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong>Hip Cir</strong></td>" +
                        "<td>" + listObj[i].Hip_Circm + "</td>" +
                        "<td>&nbsp;</td>" +
                        " <td width='85'><strong>Waist Hip Ratio</strong></td>" +
                        "<td  width='85'>" + listObj[i].WaistHipRatio + "</td> " +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong></strong></td>" +
                        "<td></td>" +
                        "<td>&nbsp;</td>" +
                        " <td width='85'><strong>Left Grip (lbs)</strong></td>" +
                        "<td  width='85'>" + listObj[i].grip_l_lbs + "</td> " +
                    "</tr>" +

                    "<tr>" +
                        "<td width='85'><strong>Right Grip (lbs)</strong></td>" +
                        "<td>" + listObj[i].grip_r_lbs + "</td>" +
                        "<td>&nbsp;</td>" +
                        " <td width='85'></td>" +
                        "<td  width='85'></td> " +
                    "</tr>" +


                     "<tr>" +
                        "<td width='85'><input type='button' ID=" + listObj[i].Vital_ID + " Class='button' value='Edit' onclick='GetVitalDetail(this.id)' name='VitalEdit'/></td>" +
                        "<td colspan='2'><input type='button' ID=" + listObj[i].Vital_ID + " Class='button' value='Delete' onclick='DeleteVitals(this.id)' style='margin-left: -35px;'/></td>" +
                    "</tr>" +

                    "</table><br>");

                }
            }
        }
    });
}
//insert/update the vital details for the patients
function SaveVitalData() {
    $("#btnUpdate").hide();
    $("#btnSubmit").show();
    var txtweight = $('[id$=txtweight]').val();
    var txtbloodPres = $('[id$=txtbloodPres]').val();
    var txttempr = $('[id$=txttempr]').val();
    var txtpulse = $('[id$=txtpulse]').val();
    var txtwaistcir = $('[id$=txtwaistcir]').val();
    var txthipcirm = $('[id$=txthipcirm]').val();
    var txtheight = $('[id$=txtheight]').val();
    var txtLeftGrip = $('[id$=txtLeftGrip]').val();
    var txtRightGrip = $('[id$=txtRightGrip]').val();

    if (txtweight == "") {
        alert("Please enter weight");
        $('[id$=txtweight]').focus();
        return false;
    }
    else if (txtheight == "") {
        alert("Please enter height");
        $('[id$=txtheight]').focus();
        return false;
    }
    var postData = new Object();
    postData.txtweight = txtweight;
    postData.txtbloodPres = txtbloodPres;
    postData.txttempr = txttempr;
    postData.txtpulse = txtpulse;
    postData.txtwaistcir = txtwaistcir;
    postData.txthipcirm = txthipcirm;
    postData.txtheight = txtheight;
    postData.txtLeftGrip = txtLeftGrip;
    postData.txtRightGrip = txtRightGrip;
    postData.PatientID = PatientID;
    if (vitalID == undefined || vitalID==null || vitalID==0) {
        postData.VitalID=0        
    }
    else {
        postData.VitalID = vitalID;        
    }
    $.ajax({
        type: "POST",
        url: "Vitals.aspx/InsertUpdateVitals",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            vitalID = 0;
            var result = response.d;
            if (result == 0) {
                alert("Data is not submitted succesfully there is some error occured!");
                return false;
            }
            else {
                BindVitalDetails();
                ClearFields();
                $("#btnUpdate").hide();
                $("#btnSubmit").show();
                $("#btnCancle").hide();
                alert("Data entered successfully");
            }

        }

    });
    return false;
}
//get vital details by vital id
function GetVitalDetail(ID) {
    $("#btnUpdate").show();
    $("#btnSubmit").hide();
    $("#btnCancle").show();
    vitalID = ID;
    var postData = new Object();
    postData.vitalID = vitalID;
    $.ajax({
        type: "POST",
        url: "Vitals.aspx/GetVitalsByVitalId",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var objVitalList = response.d;
            if (objVitalList == null) {
                alert("There is some error");
                return false;
            }
            else {
                $("#txtweight").val(objVitalList.Wgt);
                $("#txtbloodPres").val(objVitalList.BloodPres);
                $("#txttempr").val(objVitalList.Temperature);
                $("#txtpulse").val(objVitalList.Pulse);
                $("#txtwaistcir").val(objVitalList.Waist_Circm);
                $("#txthipcirm").val(objVitalList.Hip_Circm);
                $("#txtheight").val(objVitalList.Height);
                $("#txtLeftGrip").val(objVitalList.grip_l_lbs);
                $("#txtRightGrip").val(objVitalList.grip_r_lbs);
            }
        }
    });
}
//delete vital details by vital id
function DeleteVitals(ID) {
    var postData = new Object();
    postData.ID = ID;
    $.ajax({
        type: "POST",
        url: "Vitals.aspx/DeleteVitalsByID",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            alert("Data deleted sucessfully");
            BindVitalDetails();
            $("#AddVitalDiv").show();
            $("#btnUpdate").hide();
            $("#btnSubmit").show();
            $("#btnCancle").hide();
            ClearFields();
        }
    });
}


