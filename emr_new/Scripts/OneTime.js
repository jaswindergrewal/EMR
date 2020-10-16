
function grdOneTime_ClientAdd(record) {

    var txtShipDate = document.getElementById('txtShipDate');
    var lblPatientID = document.getElementById('lblPatientID');
    var PatientID = lblPatientID.innerHTML;
    var ProductID = record.ProductID;
    var ShipDate = txtShipDate.value;
    var postData = new Object();
    postData.PatientID = PatientID;
    postData.ProductID = ProductID;
    postData.ShipDate = ShipDate;


    $.ajax({
        type: "POST",
        url: "../Autoship/OneTime.aspx/CheckOneTime",
        data: JSON.stringify(postData),
        dataType: "json",
        contentType: "application/json",
        success: function (response) {
            var res = response.d;
            if (res != "") {
                alert(res);

            }

        },
        error: function (obj) {

            alert("Error");
        }
    });

}


function grdOneTime_BeforeClientAdd() {
    var txtShipDate = document.getElementById('txtShipDate');

    if (txtShipDate.value == "") {
        alert('You must enter a ship date before adding items.');
        return false
    }
    return true;


}

function checkQuantityBlank() {
    var txtQuantity = document.getElementById('txtQuantity');
    if (txtQuantity.value == "") {
        alert('You must enter a quantity before adding items.');
        return false
    }
    return true;

}

