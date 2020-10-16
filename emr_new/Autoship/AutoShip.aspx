<%@ Page Title="Autoship Home" Language="C#" MasterPageFile="~/external/Site.master" AutoEventWireup="true"
    CodeFile="AutoShip.aspx.cs" Inherits="_Manager" ValidateRequest="false" EnableEventValidation="false" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>


<%@ Register TagPrefix="obout" Namespace="Obout.ComboBox" Assembly="obout_ComboBox" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_NET" %>
<%@ Register TagName="TDD" TagPrefix="Longevity" Src="~/controls/TimeDropDown.ascx" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>



<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">

    <title>Autoship Home</title>
    <%--<script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>--%>
    <script type="text/javascript" src="../Scripts/jquery-1.8.3.js"></script>
    <script type="text/javascript" src="../Scripts/jquery-ui-1.8.20.js"></script>
    <script type="text/javascript" src="../Scripts/jquery.jqGrid.js"></script>
    <script type="text/javascript" src="../Scripts/grid.locale-en.js"></script>


    <link href="../css/base/ui.jqgrid.css" rel="stylesheet" />
    <link href="../css/base/jquery-ui.css" rel="stylesheet" />
    <script src="../Scripts/Scrips.js" type="text/javascript"></script>
    <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .editClass {
            z-index: 100;
        }
        .AutoShipButon {
    font-family: Verdana, Arial, Helvetica, sans-serif;
    font-size: 12px;
    color: #FFFFFF;
    background-color: black;
    border: 1px solid black;
    width: 100px;
    height:35px;
}
        .floatLnPadding {
            padding: 2px 1px 3px 1px;
            float: left;
        }

        .floatLnMargin {
            margin: 2px 1px 3px 1px;
            float: left;
        }

        .BoldL {
            font-weight: 500;
            margin-left: 2px;
        }

        .verticalAlign {
            vertical-align: baseline;
        }

        .DivSearch {
            width: 100%;
            float: left;
            border: 1px solid #ccc;
        }

        .DivSearch1 {
            width: 100%;
            float: left;
            border: 0px solid #aaaaaa;
        }

        .DialogClass {
            top: 50px !important;
            left: 225px !important;
        }

        .DialogClass1 {
            top: 150px !important;
            left: 225px !important;
        }

        .MatchedRow td {
            background-color: #666666;
        }

        .DivSet1 {
            border-right: 1px solid #ccc;
            float: left;
            /*width:29.77%;*/
            width: 31%;
        }

        .DivSet2 {
            border-right: 0px solid #ccc;
            float: left;
            /*width:29.77%;*/
            width: 31%;
        }

        .DivSet3 {
            float: left;
            /*padding:1.9px 18px 1.9px 18.5px*/
            padding: 1.9px 3px 1.9px 3px;
        }
    </style>
    <script type="text/javascript">

        $(document).ready(function () {

        });

        const _MS_PER_DAY = 1000 * 60 * 60 * 24;

        // a and b are javascript Date objects
        function dateDiffInDays(a, b) {
            // Discard the time and time-zone information.
            const utc1 = Date.UTC(a.getFullYear(), a.getMonth(), a.getDate());
            const utc2 = Date.UTC(b.getFullYear(), b.getMonth(), b.getDate());

            return Math.floor((utc2 - utc1) / _MS_PER_DAY);
        }

        // test it


        function CheckGenerateDate() {
            debugger;
            var AutoshipDate = document.getElementById('txtDate').value;

            const date = new Date(AutoshipDate);
            const currentDate = new Date();
            //  b = new Date("2017-07-25"),
            const Datedifference = dateDiffInDays(currentDate, date);
            if (Datedifference > 4) {
                document.getElementById('spnConfirmAutoship').textContent = 'You are attempting to generate orders for ' + Datedifference + ' days, are you sure you want to continue?';
                $find("ModalPopupAutoshpConfirm").show();
                return false;
            }
            else
            { return true; }


        }

        function CloseConfirmAutoship() {
            $find("ModalPopupAutoshpConfirm").hide();
        }


        function LoadProductData() {
            var postData = new Object();
            postData.ProductId = "";

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AutoShip.aspx/GetAutoShipProducts",
                data: JSON.stringify(postData),
                dataType: "json",
                success: function (Result) {

                    $.each(Result.d, function (key, value) {

                        $("#drpProduct").append($("<option></option>").val(value.ProductID).html(value.Name));

                    });

                },

                error: function (Result) {

                    alert("Error");

                }
            });

        }

        function GetProductData(ProductId) {
            var postData = new Object();
            postData.ProductId = ProductId;

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AutoShip.aspx/GetAutoShipProducts",
                data: JSON.stringify(postData),
                dataType: "json",
                success: function (Result) {
                    debugger;
                    if (Result.d != null) {
                        if (Result.d.length > 0) {
                            var listObj = Result.d[0];
                            document.getElementById('lblProductName').innerText = listObj.ProductName;
                            document.getElementById('txtQuantity').value = "1";
                            document.getElementById('txtPrice').value = listObj.AutoshipPrice;
                            document.getElementById('txtWeight').value = listObj.Weight;

                        }
                    }

                },

                error: function (Result) {

                    alert("Error");

                }
            });
        }

        function CheckforSipstationMsg() {
            alert("There is some error while sending connecting to Shipstation.");
        }



        function GridFillOrderItems(Grid, Pager, GridFill) {


            var chkGrid = Grid.substring(1, Grid.length);

            $(Grid).jqGrid({

                url: 'AutoShip.aspx?gridFill=' + GridFill,
                contentType: "application/json",
                datatype: 'json',
                mtype: "GET",


                colNames: ['Sku', 'ProductName', 'Quantity', 'Price', 'Weight', 'Remove', 'Add'],
                colModel: [

                    { name: 'Sku', index: 'Sku', editable: false, sortable: true, width: 250 },
                    { name: 'ProductName', index: 'ProductName', editable: false, sorttype: "string", classes: 'wrap', width: 300 },
                    { name: 'Quantity', index: 'Quantity', editable: false, sorttype: "string", classes: 'wrap', formatter: createEditQtyButton, width: 70 },
                    { name: 'Price', index: 'Price', editable: false, sorttype: "string", classes: 'wrap', width: 70 },
                    { name: 'Weight', index: 'Weight', editable: false, sorttype: "string", classes: 'wrap', width: 70 },
                    { name: 'OrderItemID', index: 'OrderItemID', editable: false, sortable: false, align: "center", formatter: createRemoveButton, width: 70 },
                    { name: 'OrderItemID', index: 'OrderItemID', editable: false, sortable: false, align: "center", formatter: createAddItemButton, width: 70 },
                ],



                pager: Pager,
                rowNum: 100,
                pgbuttons: false,
                pginput: false,

                sortable: true,
                autowidth: true,
                viewrecords: true,
                gridview: true,

                ondblClickRow: function (rowid, iRow, iCol) {

                },


                gridComplete: function () {
                    var rows = $(Grid).getDataIDs();


                    if (rows.length > 0) {
                        $(Grid).show();
                        $("#DivNoRecord" + GridFill).hide();
                    }
                    else {
                        $(Grid).hide();
                        $("#DivNoRecord" + GridFill).show();

                    }


                    var data = 0;
                    var grid = $('#grdCloseOrderItems');


                    for (i = 0; i < rows.length; i++) {
                        var rowData = grid.jqGrid('getRowData', rows[i]);
                        var Total = (parseInt($(rowData['Quantity']).text()) * parseFloat(rowData['Price']));
                        var DecimalPlaces = countDecimals(Total);
                        if (DecimalPlaces > 2) {

                            Total = (Math.round(Total * 100) / 100);
                        }
                        data = data + Total;
                    }

                    var TotalDecimal = countDecimals(data);
                    if (TotalDecimal > 2) {
                        Total = (Math.round(data * 100) / 100);
                        data = Total;
                    }

                    document.getElementById('lblTotalPaymentAmount').innerText = "Total Amount Without Shipping: " + data;


                }

            }).navGrid(Pager,
                {
                    edit: false, add: false, del: false, search: false, refresh: true
                }

                );


        }

        function createEditQtyButton(cellvalue, options, rowObject) {
            debugger;
            var OrderItemId = '"' + String(rowObject[5]) + '"';

            return "<a href='#'  onclick='EditQty(" + OrderItemId + "," + rowObject[2] + " );' >" + rowObject[2] + "</a>";
        }

        //Create a checkbox within the JqGrid
        function createRemoveButton(cellvalue, options, rowObject) {
            var OrderItemId = '"' + String(rowObject[5]) + '"';

            return "<img src='../images/Remove-icon.jpe' style='Height:20px;Width:20px' onclick='DeleteOrderItem(" + OrderItemId + ");' />";
        }

        //Create a checkbox within the JqGrid
        function createAddItemButton(cellvalue, options, rowObject) {
            return "<img src='../images/add-icon.png' style='Height:20px;Width:20px'  onclick='AddItemPopUp();' >";
        }

        function AddItemPopUp() {
            LoadProductData();
            $find("ModalAddOrderItem").show();
        }

        function CloseAddItem() {
            $find("ModalAddOrderItem").hide();
        }

        function CloseQty() {
            $find("ModalQty").hide();
        }

        function CloseOrderItem() {
            grdOderClose.refresh();
            $find("ModalAlert").hide();
        }

        function AddOrderitems() {
            var inpOrderId = document.getElementById('inpOrderId');
            var inpQty = document.getElementById('txtQuantity');
            var inpWeight = document.getElementById('txtWeight');
            var inpPrice = document.getElementById('txtPrice');
            var inpPRoductId = $('#drpProduct').val();


            var Quantity = inpQty.value;
            var OrderId = inpOrderId.value;
            var postData = new Object();
            postData.OrderId = OrderId;
            postData.Quantity = Quantity
            postData.Price = inpPrice.value;
            postData.Weight = inpWeight.value;
            postData.ProductId = inpPRoductId;
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/AddOrderItems",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        {

                            $("#grdCloseOrderItems").trigger("reloadGrid");
                            $("#Paid").prop('checked', false);
                            $("#Invoiced").prop('checked', false);
                            $("#btnInvoiceCreation").prop('disabled', false);
                            $find("ModalAddOrderItem").hide();
                        }

                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
        }
        function DeleteOrderItem(OrderId) {


            var postData = new Object();
            postData.OrderId = OrderId;

            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/DeleteOrderByItemId",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {

                    if (result.d != "") {

                        $("#grdCloseOrderItems").trigger("reloadGrid");
                        $("#Paid").prop('checked', false);
                        $("#Invoiced").prop('checked', false);
                        $("#btnInvoiceCreation").prop('disabled', false);
                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });

        }

        function EditQty(OrderItemId, Qty) {
            debugger;
            document.getElementById('inpQty').value = Qty;
            var inpOrderItemId = document.getElementById('inpOrderItemId');
            inpOrderItemId.value = OrderItemId;
            $find("ModalQty").show();
        }

        function UpdateQty() {
            var inpOrderItemId = document.getElementById('inpOrderItemId');
            var Qty = document.getElementById('inpQty');
            var Quantity = Qty.value;
            var OrderItemId = inpOrderItemId.value;
            var postData = new Object();
            postData.OrderItemId = OrderItemId;
            postData.Quantity = Quantity
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/UpdateQty",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        {

                            $("#grdCloseOrderItems").trigger("reloadGrid");
                            $("#Paid").prop('checked', false);
                            $("#Invoiced").prop('checked', false);
                            $("#btnInvoiceCreation").prop('disabled', false);
                            $find("ModalQty").hide();
                        }

                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
        }

        function btnUpdateAutoShipNote() {
            var editorObject = $find("<%= txtAutoShipNotes.ClientID %>");
            var _content = editorObject.get_content();
            var AutoShipNote = _content;//document.getElementById('txtAutoShipNotes').innerText;
            var InpOrderId = document.getElementById('inpOrderId');
            var InpUserId = document.getElementById('inpUserId');
            var OrderId = InpOrderId.value;
            var UserId = InpUserId.value;
            var postData = new Object();
            postData.OrderId = OrderId;
            postData.AutoShipNote = AutoShipNote;
            postData.UserId = UserId;
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/UpdateAutoShipNote",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        {

                        }

                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
        }

        function btnUpdateOrderNote() {
            debugger;
            var ShippingAddress = document.getElementById('txtShipAddress').value;
            var ShippingCity = document.getElementById('txtShipCity').value;
            var ShippingState = document.getElementById('txtShipState').value;
            var ShippingZip = document.getElementById('txtShipZip').value;
            var OrderNote = document.getElementById('txtOrderNotes').value;
            var InpOrderId = document.getElementById('inpOrderId');
            var InpUserId = document.getElementById('inpUserId');
            var OrderId = InpOrderId.value;
            var UserId = InpUserId.value;
            var postData = new Object();
            postData.OrderId = OrderId;
            postData.ShippingAddress = ShippingAddress;
            postData.ShippingCity = ShippingCity;
            postData.ShippingState = ShippingState;
            postData.ShippingZip = ShippingZip;
            postData.OrderNote = OrderNote;
            postData.UserId = UserId;
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/UpdateOrderNote",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    if (result.d != "") {
                        grdOderClose.refresh();
                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
        }

        function UpdatePaidStatus() {
            if ($("#Invoiced").prop('checked') == true) {
                debugger;
                var InpOrderId = document.getElementById('inpOrderId');
                var OrderId = InpOrderId.value;
                var postData = new Object();
                postData.OrderId = OrderId;

                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/GetOrderById",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {

                        if (result.d != null) {
                            debugger;
                            if (result.d.length > 0) {
                                //if (result.d[0].Invoiced == true && result.d[0].Paid == true) {
                                if (result.d[0].Invoiced == true && result.d[0].Status == "") {
                                    $.ajax({
                                        type: "POST",
                                        url: "AutoShip.aspx/UpdatePaidStatus",
                                        data: JSON.stringify(postData),
                                        dataType: "json",
                                        contentType: "application/json",
                                        async: false,
                                        success: function (result) {

                                            if (result.d != "") {
                                                if ($("#Paid").prop('checked') == true) {
                                                    $("#Paid").prop('checked', false);
                                                }
                                                else {
                                                    $("#Paid").prop('checked', true);
                                                }
                                                $find("ModalAlert").hide();
                                                grdOderClose.refresh();

                                            }
                                            else {
                                                alert("some error occured");
                                            }

                                        },
                                        error: function (obj) {

                                            alert("some error occured");

                                        }
                                    });
                                }
                                else {
                                    if (result.d[0].Paid == true) {

                                        $("#Paid").prop('checked', true);
                                    }
                                    else {
                                        $("#Paid").prop('checked', false);
                                    }


                                }
                            }



                        }

                    },
                    error: function (obj) {

                        alert("some error occured");

                    }
                });
            }
            else {
                $("#Paid").prop('checked', false);
            }



        }

        function UpdateFreeShippingStatus() {

            var InpOrderId = document.getElementById('inpOrderId');
            var OrderId = InpOrderId.value;
            var postData = new Object();
            postData.OrderId = OrderId;


            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/UpdateFreeShippingStatus",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {

                    if (result.d != "") {
                        //if ($("#FreeShipping").prop('checked') == true) {
                        //    $("#FreeShipping").prop('checked', false);
                        //}
                        //else {
                        //    $("#FreeShipping").prop('checked', true);
                        //}
                        //$find("ModalAlert").hide();
                        //grdOderClose.refresh();

                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });




        }

        function UpdateStatus() {
            if ($("#Invoiced").prop('checked') == true && $("#Paid").prop('checked') == true) {
                var InpOrderId = document.getElementById('inpOrderId');
                var OrderId = InpOrderId.value;
                var postData = new Object();
                postData.OrderId = OrderId;
                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/UpdateStatusById",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {
                        if (result.d != "") {
                            $find("ModalAlert").hide();
                            grdOderClose.refresh();

                        }
                        else {
                            alert("some error occured");
                        }

                    },
                    error: function (obj) {

                        alert("some error occured");

                    }
                });
            }
        }

        function GrdPaidStatus(OrderId) {


            var postData = new Object();
            postData.OrderId = OrderId;

            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/GetOrderById",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    debugger;
                    if (result.d != null) {
                        if (result.d.length > 0) {
                            //if (result.d[0].Invoiced == true && result.d[0].Paid == true) {
                            if (result.d[0].Invoiced == true && result.d[0].Status == "") {
                                $.ajax({
                                    type: "POST",
                                    url: "AutoShip.aspx/UpdatePaidStatus",
                                    data: JSON.stringify(postData),
                                    dataType: "json",
                                    contentType: "application/json",
                                    async: false,
                                    success: function (result) {

                                        if (result.d != "") {
                                            GrdReadyStatus(OrderId)
                                            // grdOderClose.refresh();

                                        }
                                        else {
                                            alert("some error occured");
                                        }

                                    },
                                    error: function (obj) {

                                        alert("some error occured");

                                    }
                                });
                            }

                        }

                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });

        }

        function GrdReadyStatus(OrderId) {


            var postData = new Object();
            postData.OrderId = OrderId;

            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/GetOrderById",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {
                    debugger;
                    if (result.d != null) {
                        if (result.d.length > 0) {
                            //if (result.d[0].Invoiced == true && result.d[0].Paid == true) {
                            if (result.d[0].Invoiced == true && result.d[0].Paid == true) {
                                $.ajax({
                                    type: "POST",
                                    url: "AutoShip.aspx/UpdateStatusByIdToggle",
                                    data: JSON.stringify(postData),
                                    dataType: "json",
                                    contentType: "application/json",
                                    async: false,
                                    success: function (result) {

                                        if (result.d != "") {
                                            //skedWindowOrder.Close();
                                            grdOderClose.refresh();

                                        }
                                        else {
                                            alert("some error occured");
                                        }

                                    },
                                    error: function (obj) {

                                        alert("some error occured");

                                    }
                                });
                            }

                        }

                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });


        }



        var checkCommonVar = "";
        function OnBeforeDelete(record) {

            if (confirm("Are you sure you want to delete order " + record.OrderID + " (for " + record.ShipName + ") ?") == false) {
                return false;
            } record

            return true;
        }

        // Manage Product
        // Autoship/AutoShip.aspx
        function ValidateManageProduct(selectedRecords) {

            debugger;
            var URL = 'AutoShip.aspx/CheckDuplicateForProduct';
            var tableName = "AutoshipProducts";
            var ID = selectedRecords.ProductID;
            var Name = selectedRecords.ProductName;
            var SKU = selectedRecords.Sku;

            if (Name == "") {
                alert("Please enter the Product name.");
                return false;
            }
            else {
                if (ID == undefined) {
                    ID = 0;
                }
                CheckRecordExists(ID, Name, SKU, tableName);
                return checkCommonVar;
            }
        }

        function onClientAdd(record) {

            document.getElementById('txtWeight').readOnly = false;
        }

        function onClientEdit(record) {
            document.getElementById('txtWeight').readOnly = true;
        }

        function grdOderClose_Delete(OrderId) {

            debugger;
            var postData = new Object();
            postData.OrderId = OrderId;

            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/GetOrderById",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {

                    if (result.d != null) {
                        if (result.d.length > 0) {
                            if (result.d[0].Paid == true) {


                                document.getElementById('lblDeleteText').innerHTML = "Are you sure you want to delete order number " + OrderId.toString() + " of patient " + result.d[0].ShipName;
                                document.getElementById('lblPatientName').innerHTML = "Name : " + result.d[0].ShipName;
                                var hdnOrderId = document.getElementById('hdnDeleteOrderId');
                                hdnOrderId.value = OrderId;
                                $find("ModalDeleteOrder").show();
                            }


                            else {
                                $.ajax({
                                    type: "POST",
                                    url: "AutoShip.aspx/DeleteOrderById",
                                    data: JSON.stringify(postData),
                                    dataType: "json",
                                    contentType: "application/json",
                                    async: false,
                                    success: function (result) {

                                        if (result.d != "") {


                                        }
                                        else {
                                            alert("some error occured");
                                        }

                                    },
                                    error: function (obj) {

                                        alert("some error occured");

                                    }
                                });
                                grdOderClose.refresh();
                            }



                        }
                        else {
                            $.ajax({
                                type: "POST",
                                url: "AutoShip.aspx/DeleteOrderById",
                                data: JSON.stringify(postData),
                                dataType: "json",
                                contentType: "application/json",
                                async: false,
                                success: function (result) {

                                    if (result.d != "") {


                                    }
                                    else {
                                        alert("some error occured");
                                    }

                                },
                                error: function (obj) {

                                    alert("some error occured");

                                }
                            });
                            grdOderClose.refresh();
                        }

                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
        }

        function DeleteOrder() {
            var XeroDelete = false;
            if ($("#chkDeleteInXero").prop('checked') == true) {
                XeroDelete = true;
            }
            var DeleteReason = document.getElementById('txtDeleteReason').value;
            var OrderId = document.getElementById('hdnDeleteOrderId').value;
            var postData = new Object();
            postData.OrderId = OrderId;
            postData.XeroDelete = XeroDelete;
            postData.DeleteReason = DeleteReason;
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/DeleteOrderByIdWithReason",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {

                    if (result.d != "") {


                    }
                    else {
                        alert("some error occured");
                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
            document.getElementById('txtDeleteReason').value = "";
            $("#chkDeleteInXero").prop('checked', false);
            $find("ModalDeleteOrder").hide();
            grdOderClose.refresh();
        }

        function CloseDeleteOrder() {
            document.getElementById('txtDeleteReason').value = "";
            $("#chkDeleteInXero").prop('checked', false);
            $find("ModalDeleteOrder").hide();
            grdOderClose.refresh();
        }



        function grdOderClose_Edit(record) {
            //alert(record);
            var OrderId = document.getElementById('inpOrderId');
            OrderId.value = record;
            // grdOrderItems.refresh();
            var postData = new Object();
            postData.OrderId = OrderId.value;


            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/GetOrderById",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (result) {

                    if (result.d != null) {
                        debugger;
                        if (result.d.length > 0) {
                            document.getElementById('lblName').innerHTML = result.d[0].ShipName;
                            document.getElementById('lblPatientName').innerHTML = "Name : " + result.d[0].ShipName;
                            var PatientId = document.getElementById('XeroContactId');
                            PatientId.value = result.d[0].PatientID;
                            document.getElementById('lblShipDate').innerHTML = "Ship Date: " + result.d[0].ShipDate;
                            document.getElementById('txtBillingAddress').innerText = result.d[0].BillingStreet + " " + result.d[0].BillingCity + " " + result.d[0].BillingState + " " + result.d[0].BillingZip;
                             document.getElementById('txtHotNotes').innerText = result.d[0].HotNotes;
                            document.getElementById('txtOrderNotes').value = result.d[0].Note;
                            document.getElementById('txtShipAddress').value = result.d[0].ShipAddress1;
                            document.getElementById('txtShipCity').value = result.d[0].ShipCity;
                            document.getElementById('txtShipState').value = result.d[0].ShipState;
                            document.getElementById('txtShipZip').value = result.d[0].ShipZip;

                            var editorObject = $find("<%= txtAutoShipNotes.ClientID %>");
                            editorObject.set_content(result.d[0].AutoshipNote);

                            //var btnPayment = document.getElementById('btnInvoicePayment');
                            var btnInvoice = document.getElementById('btnInvoiceCreation');


                            if (result.d[0].Invoiced == true) {
                                $("#Invoiced").prop('checked', true);
                                $("#btnInvoiceCreation").prop('disabled', true);

                                if (result.d[0].Paid == true) {
                                    $("#Paid").prop('checked', true);
                                    //$("#btnInvoicePayment").prop('disabled', true);
                                }
                                else {
                                    $("#Paid").prop('checked', false);
                                    //$("#btnInvoicePayment").prop('disabled', false);
                                }
                            }
                            else {
                                $("#Invoiced").prop('checked', false);
                                $("#Paid").prop('checked', false);
                                $("#btnInvoiceCreation").prop('disabled', false);
                                //$("#btnInvoicePayment").prop('disabled', true);
                            }

                            if (result.d[0].FreeShipping == true) {
                                $("#FreeShipping").prop('checked', true);
                            }
                            else {
                                $("#FreeShipping").prop('checked', false);
                            }

                            GridFillOrderItems('#grdCloseOrderItems', '#pagernav1', 1);
                            $("#grdCloseOrderItems").trigger("reloadGrid");
                        }

                    }

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });
            $find("ModalAlert").show();
            }

            function CheckRecordExists(ID, Name, SKU, tableName) {
                debugger;
                var isFlag = false;
                var postData = new Object();
                postData.ID = ID;
                postData.Name = Name;
                postData.Sku = SKU;
                postData.tableName = tableName;
                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/CheckDuplicateForProduct",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {

                        if (result.d != '') {
                            if (result.d == "duplicate product")
                                alert("The product name('" + Name + "') that you entered already exists in the database, please choose another.");
                            else if (result.d == "duplicate sku")
                                alert("The sku('" + SKU + "') that you entered already exists in the database, please choose another.");

                            isFlag = false;
                        }
                        else {
                            isFlag = true;
                        }
                    },
                    error: function (obj) {

                        alert("some error occured");
                        isFlag = false;
                    }
                });

                if (isFlag == false) {
                    checkCommonVar = false;
                    return false;
                }
                else {
                    checkCommonVar = true;
                    return true;
                }

            }

            function grdOderClose_Select(sender, args) {

                alert('1');
                for (var i = 0; i < grdOderClose.SelectedRecords.length; i++) {
                    var record = grdOderClose.SelectedRecords[i];
                }
            }

            //XeroContact grid

            function CreateXeroContact() {

                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/CheckForXeroPatient",
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {

                        if (result.d != '') {
                            if (result.d == "true") {
                                $.ajax({
                                    type: "POST",
                                    url: "AutoShip.aspx/CreateXeroInvoice",
                                    dataType: "json",
                                    contentType: "application/json",
                                    async: false,
                                    success: function (response) {

                                        document.location.href = "http://" + document.location.host + "/XeroAuthonticationCall.ashx?checkedInvoiceiDS=" + response.d;

                                    },
                                    error: function (obj) {

                                        alert("some error occured");

                                    }
                                });
                            }

                            else if (result.d == "false") {

                                $find("ModalXeroContact").show();
                            }
                        }

                    },
                    error: function (obj) {

                        alert("some error occured");

                    }
                });

            }

            function CreateXeroInvoice() {

                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/CheckForXeroPatient",
                    dataType: "json",
                    contentType: "application/json",
                    async: false,
                    success: function (result) {

                        if (result.d != '') {

                            $.ajax({
                                type: "POST",
                                url: "AutoShip.aspx/CreateXeroInvoice",
                                dataType: "json",
                                contentType: "application/json",
                                async: false,
                                success: function (response) {

                                    document.location.href = "http://" + document.location.host + "/XeroAuthonticationCall.ashx?checkedInvoiceiDS=" + response.d;

                                },
                                error: function (obj) {
                                    debugger;
                                    alert("some error occured");

                                }
                            });

                        }

                    },
                    error: function (obj) {

                        alert("some error occured");

                    }
                });

            }

            function CloseModalXeroContact() {
                $find("ModalXeroContact").hide();
            }

            var countDecimals = function (value) {
                if (Math.floor(value) === value) return 0;
                return value.toString().split(".")[1].length || 0;
            }
            function InvoicePayment() {

                LoadXeroAccounts();
                var data = 0;
                var grid = $('#grdCloseOrderItems');
                var rows = grid.jqGrid('getDataIDs');

                for (i = 0; i < rows.length; i++) {
                    var rowData = grid.jqGrid('getRowData', rows[i]);
                    var Total = (parseInt($(rowData['Quantity']).text()) * parseFloat(rowData['Price']));
                    var DecimalPlaces = countDecimals(Total);
                    if (DecimalPlaces > 2) {

                        Total = (Math.round(Total * 100) / 100);
                    }
                    data = data + Total;
                }

                var TotalDecimal = countDecimals(data);
                if (TotalDecimal > 2) {
                    Total = (Math.round(data * 100) / 100);
                    data = Total;
                }

                document.getElementById('lblPaymentDue').innerText = data;
                document.getElementById('txtPaymentAmount').value = data;
                var calendarBehavior = $find("CalendarExtender5");
                calendarBehavior.set_selectedDate('<%=DateTime.Now.ToString("MM/dd/yyyy") %>');

            $find("ModalXeroPayment").show();

        }

        function ClosePaymentModal() {
            $find("ModalXeroPayment").hide();
        }


        function LoadXeroAccounts() {

            $.ajax({
                type: "POST",
                contentType: "application/json; charset=utf-8",
                url: "AutoShip.aspx/GetXeroAccounts",
                dataType: "json",
                success: function (Result) {

                    $.each(Result.d, function (key, value) {

                        $("#drpXeroAccounts").append($("<option></option>").val(value.AccountCode).html(value.AccountName));

                    });

                },

                error: function (Result) {

                    alert("Error");

                }
            });

        }



        function UpdatePayment() {

            var postData = new Object();
            postData.AccountCode = $("#drpXeroAccounts option:selected").val();
            postData.Amount = document.getElementById('txtPaymentAmount').value;

            var PaymentDate = document.getElementById('txtPaymentDate').value;

            postData.PaymentDate = PaymentDate;
            postData.PaymentReference = document.getElementById('txtPaymentReference').value;
            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/CreateXeroInvoice",
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function (response) {
                    document.location.href = "http://" + document.location.host + "/XeroAuthonticationCall.ashx?AccountCode=" + postData.AccountCode + "&Amount=" + postData.Amount + "&PaymentDate=" + postData.PaymentDate + "&PaymentRefrence=" + postData.PaymentReference + "&OrderId=" + response.d;
                    //document.location.href = "http://" + document.location.host + "/XeroAuthonticationCall.ashx?checkedInvoiceiDS=" + response.d;

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });


            //$.ajax({
            //    type: "POST",
            //    contentType: "application/json; charset=utf-8",
            //    url: "AutoShip.aspx/UpdatePayment",
            //    data: JSON.stringify(postData),
            //    dataType: "json",
            //    success: function (Result) {



            //    },

            //    error: function (Result) {

            //        alert("Error");

            //    }
            //});


            $find("ModalXeroPayment").hide();

        }


        //This function returns search result to match Xero patients with Local patients.
        function SearchMatchXeroPatientsData(Grid, Pager, GridFill) {
            var FirstName = document.getElementById('FirstNameS').value;
            var LastName = document.getElementById('LastNameS').value;

            var postData = new Object();
            postData.FirstName = FirstName;
            postData.LastName = LastName;

            $.ajax({
                type: "POST",
                url: "AutoShip.aspx/CreateXeroContact",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                async: false,
                success: function () {


                    var chkGrid = Grid.substring(1, Grid);
                    $(Grid).jqGrid({

                        url: 'AutoShip.aspx?gridFill=' + GridFill,
                        contentType: "application/json",
                        datatype: 'json',
                        mtype: "GET",

                        colNames: ['First Name', 'Last Name', 'Action'],
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

                    $(Grid).trigger("reloadGrid");

                },
                error: function (obj) {

                    alert("some error occured");

                }
            });



        }

        //This funcrtion returns matching button html.
        function createMatchButton(cellvalue, options, rowObject) {
            var ContactId = '"' + String(rowObject[2]) + '"';
            var FirstName = '"' + String(rowObject[0]) + '"';
            var LastName = '"' + String(rowObject[1]) + '"';

            var total = FirstName + "," + LastName + "," + ContactId;
            return "<input class='blue-btn MatchBtnClass' value='Match' type='button' id='MatchBtn" + rowObject[2] + "' onclick='MatchRecordWithLocalPatients(" + total + ");' >";
        }

        //This function making matching relation between Xero patients and local user.
        function MatchRecordWithLocalPatients(FirstName, LastName, ContactId) {
            debugger;
            var PatientId = document.getElementById('XeroContactId').value;
            var ParameterF = "\"" + ContactId + "\",\"" + PatientId + "\"";
            var FirstNameP = document.getElementById('lblPatientName').innerText;

            //var EmailP = $("#EmailId").prop('title');
            var Message = "Do you want to match '" + FirstNameP + "'";
            //if (EmailP == "-" || EmailP == "" || EmailP == "null") { Message = Message } else { Message = Message + ", email id '" + EmailP + "'"; }
            Message = Message + " with '" + FirstName + " " + LastName + "'";
            //if (Email == "-" || Email == "" || Email == "null" || Email == null) { Message = Message + "?" } else { Message = Message + ", email id '" + Email + "'?"; }
            var r = confirm(Message);
            if (r == true) {
                $.ajax({
                    type: "POST",
                    url: "AutoShip.aspx/MatchTwoPatients",
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
                            $("#grdXeroPatientsMatchSearch").trigger("reloadGrid");
                            $.ajax({
                                type: "POST",
                                url: "AutoShip.aspx/SetXeroPatientId",
                                data: JSON.stringify({ XeroPatientId: ContactId }),
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {

                                }
                            });
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

    </script>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <p class="PageTitle">
        Manage Autoship
    </p>

    <div id="mask">
    </div>
    <cc1:TabContainer ID="Autoship" runat="server" Width="95%" ActiveTabIndex="0"
        CssClass="lmc_tab">
        <cc1:TabPanel HeaderText="Generate Shipments" runat="server" ID="GenerateOrders"
            CssClass="TabPanel">
            <ContentTemplate>
                <table width="100%">
                    <caption>
                        <h4>Generate Shipments</h4>
                    </caption>
                    <tr>
                        <td align="right">Date:
                        </td>
                        <td>
                            <asp:TextBox ID="txtDate" ClientIDMode="Static"  runat="server" Text='<%# DateTime.Today.ToString("MM/dd/yyyy") %>' />
                            <cc1:CalendarExtender ID="StartExt" runat="server" TargetControlID="txtDate" />
                            <asp:CompareValidator ID="CompareValidator4" runat="server" ControlToValidate="txtDate"
                                Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                                EnableClientScript="true" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" align="center">
                            <asp:Button ID="btnGenOrders" runat="server" ClientIDMode="Static" OnClientClick="return CheckGenerateDate();" OnClick="btnGenOrders_OnClick" Text="Generate"
                                CssClass="button" />&nbsp;
							<asp:Button ID="btnPreviewOrders" runat="server" OnClick="btnPreviewOrders_Click"
                                Text="Preview" CssClass="button" />
                        </td>
                    </tr>
                </table>
                <table width="100%" id="OrderPreviewTable" runat="server" visible="false">
                    <tr>
                        <td>
                            <asp:Button ID="btnPrint" runat="server" Text="View Report" CssClass="button" OnClick="btnPrint_Click" />&nbsp;
							<asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="button" OnClick="btnCancel_Click" />&nbsp;
							<a id="Invoices" runat="server" href="" visible="false" target="_blank">View PDF</a>
                        </td>
                    </tr>
                    <tr>
                        <th>Shipment Preview
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="OrderViewGrid" runat="server"
                                AutoGenerateSelectButton="false" AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID"
                                OnSelectedIndexChanged="OrderViewGrid_SelectedIndexChanged">
                                <Columns>
                                    <asp:CommandField ShowSelectButton="true" ButtonType="Button" ControlStyle-CssClass="button"
                                        SelectText="Select" />
                                    <asp:BoundField DataField="OrderID" HeaderText="Order #" />
                                    <asp:BoundField DataField="DatePrep" HeaderText="Date Prepared" DataFormatString="{0:MM/dd/yyyy}" />
                                    <asp:BoundField DataField="ShipName" HeaderText="Name" />
                                    <asp:BoundField DataField="ShipAddress1" HeaderText="Address" />
                                    <asp:BoundField DataField="ShipCity" HeaderText="City" />
                                    <asp:BoundField DataField="ShipState" HeaderText="State" />
                                    <asp:BoundField DataField="ShipZip" HeaderText="Zip" />
                                    <asp:BoundField DataField="AutoShipNote" HeaderText="Note" />
                                    <asp:BoundField DataField="ShipDate" HeaderText="Due Date" DataFormatString="{0:MM/dd/yyyy}" />
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                    <tr>
                        <th>Shipment Items
                        </th>
                    </tr>
                    <tr>
                        <td>
                            <asp:GridView ID="OrderItemsPreviewGrid" runat="server"
                                AutoGenerateSelectButton="false" AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" EmptyDataText="Select an order to view items.">
                                <Columns>
                                    <asp:BoundField DataField="ProductName" HeaderText="Product" />
                                    <asp:BoundField DataField="Quantity" HeaderText="Quantity" />
                                    <asp:BoundField DataField="Price" HeaderText="Price" DataFormatString="{0:C}" />
                                    <asp:TemplateField HeaderText="Extended Price">
                                        <ItemTemplate>
                                            <asp:Label ID="ExtendedPrice" runat="server" Text='<%# ((int)Eval("Quantity") * (decimal)Eval("Price")).ToString("C") %>' />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>


                <asp:Panel ID="pnlAutoShipConfirm" CssClass="modalPopup" Width="400px" runat="server" Height="100px"
                    Style="position: absolute; z-index: 10000000; top: 50%;" ClientIDMode="Static">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                              
                              
                            </div>
                        </div>

                       


                        <table style="margin-left: 10px; margin-right: 10px; width: 90%; height: 150px;" cellpadding="2" cellspacing="0">
                           

                            <tr>
                                <td colspan="2"><span id="spnConfirmAutoship"  runat="server" clientidmode="Static"></span></td>
                                </tr>
                           
                           
                           
                            <tr>
                               <%-- <td >
                                    <input type="button" runat="server" onclick="AddOrderitems();" clientidmode="Static" value="Add Item" /></td>--%>
                                <td style="width:50%; "><asp:Button ID="btnAutoshipCancel" Text="Cancel" runat="server" clientidmode="Static" style="margin-left: 100px;" OnClientClick="CloseConfirmAutoship()" CssClass="button"/></td>
                                <td><asp:Button ID="btngenrateautoship" Text="Generate Anyway" runat="server" OnClick="btnGenOrders_OnClick" CssClass="button"/></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummyAutoship" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalPopupAutoshpConfirm" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummyAutoship" PopupControlID="pnlAutoShipConfirm"
                    Y="200" ClientIDMode="static" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Manage Complete Orders" runat="server" ID="CreateInvoices"
            CssClass="TabPanel" Visible="false">
            <ContentTemplate>
                <table width="100%">
                    <caption>
                        <h4>Complete Orders</h4>
                    </caption>
                    <tr>
                        <td>Beginning
							<asp:TextBox ID="txtBeginClosed" runat="server" />
                            <cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtBeginClosed"
                                Enabled="True" />
                            and Ending
							<asp:TextBox ID="txtEndClosed" runat="server" />
                            <cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndClosed"
                                Enabled="True" />
                            <asp:Button ID="btnDeleteOrders" runat="server" OnClick="btnDeleteOrders_Click" Text="Go"
                                CssClass="button" /><br />
                            <asp:CompareValidator ID="CompareValidator1" runat="server" ErrorMessage="Start date must come before end date."
                                ControlToValidate="txtBeginClosed" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEndClosed"
                                Display="Dynamic" ForeColor="Red" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <obout:Grid ID="grdDeleteOrders" runat="server" AutoGenerateColumns="False" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="OrderID,DatePrep,ShipName,ShipAddress1,ShipCity,ShipState,ShipZip"
                                EmptyDataText="No Open Orders." OnDeleteCommand="grdDeleteOrders_RowDeleting"
                                Visible="False" FolderStyle="grid_styles/Style_7" OnBeforeClientDelete="OnBeforeDelete">
                                <Columns>
                                    <obout:Column ID="Column2" HeaderText="Delete" AllowDelete="True" Width="60" Index="0">
                                        <ControlStyle CssClass="button" />
                                    </obout:Column>
                                    <obout:Column DataField="OrderID" HeaderText="Order #" InsertVisible="False" ReadOnly="True"
                                        SortExpression="OrderID" Width="60" Index="1" />
                                    <obout:Column DataField="DatePrep" HeaderText="Date Prep" DataFormatString="{0:MM/dd/yyyy}"
                                        ReadOnly="True" Width="100" DataFormatString_GroupHeader="{0:MM/dd/yyyy}" Index="2" />
                                    <obout:Column DataField="ShipName" HeaderText="Name" ReadOnly="True" Index="3" Width="125">
                                        <ItemStyle Wrap="False" />
                                    </obout:Column>
                                    <obout:Column DataField="ShipAddress1" HeaderText="Address" ReadOnly="True" Index="4">
                                        <ItemStyle Wrap="False" />
                                    </obout:Column>
                                    <obout:Column DataField="ShipCity" HeaderText="City" ReadOnly="True" Index="5" Width="100" />
                                    <obout:Column DataField="ShipState" HeaderText="State" ReadOnly="True" Index="6" Width="75" />
                                    <obout:Column DataField="ShipZip" HeaderText="Zip" ReadOnly="True" Index="7" Width="75" />
                                </Columns>
                                <ClientSideEvents OnBeforeClientDelete="OnBeforeDelete" />
                            </obout:Grid>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Shipment Status" runat="server" ID="CloseOrders" CssClass="TabPanel">
            <ContentTemplate>

                <table width="100%">
                    <caption>
                        <h4>Shipment Status</h4>
                    </caption>
                    <tr>

                        <td>Beginning
									<asp:TextBox ID="txtBegin" runat="server" />
                            <cc1:CalendarExtender ID="xx" runat="server" TargetControlID="txtBegin" />
                            and Ending
									<asp:TextBox ID="txtEnd" runat="server" />
                            <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEnd" />
                            <asp:Button ID="btnCloseOrders" runat="server" OnClick="btnCloseOrders_Click" Text="Go"
                                CssClass="button" /><br />
                            <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Start date must come before end date."
                                ControlToValidate="txtBegin" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEnd"
                                Display="Dynamic" ForeColor="Red" />
                        </td>

                    </tr>
                    <tr>

                        <td>


                            <asp:Button ID="ShipToReady" runat="server" OnClick="ShipToReady_Click" Text="Ship Ready orders"
                                CssClass="button" /><br />

                        </td>

                    </tr>
                    <tr>
                        <td>
                            <asp:UpdatePanel runat="server">
                                <ContentTemplate>
                                    <obout:Grid ID="grdOderClose" Serialize="true" runat="server" OnRebind="grdOderClose_Rebind" Width="100%" OnRowDataBound="grdOderClose_RowDataBound"
                                        AutoGenerateColumns="False" AllowAddingRecords="False" FolderStyle="../grid_styles/Style_7" AllowRecordSelection="false"
                                        AllowFiltering="false" ShowFooter="true" AllowPaging="true" PageSize="30" AllowPageSizeSelection="false" CallbackMode="false">
                                        <Columns>
                                            <obout:Column HeaderText="Delete" Width="5%" Index="0">
                                                <TemplateSettings TemplateId="templateDelete" />
                                            </obout:Column>
                                            <obout:Column HeaderText="Edit" Width="4%" Index="1">
                                                <TemplateSettings TemplateId="templateEdit" />
                                            </obout:Column>
                                            <obout:Column DataField="OrderID" HeaderText="OrderId" Width="5%"
                                                Visible="true" Index="2" />

                                            <obout:Column DataField="ShipName" HeaderText="Customer" ReadOnly="True"
                                                Index="3" Width="10%" />
                                            <obout:Column DataField="ShipDate" HeaderText="Ship Date" ReadOnly="True" DataFormatString="{0:MM/dd/yyyy}"
                                                Visible="true" Index="4" Width="8%" />
                                            <obout:CheckBoxColumn DataField="Invoiced" HeaderText="Invoiced" Width="8%" Index="5" />

                                            <obout:CheckBoxColumn DataField="Paid" HeaderText="Paid" Width="8%" Index="6" />

                                            <obout:CheckBoxColumn DataField="Status" HeaderText="Status To Ship" Width="9%" Index="7" />
                                            <obout:Column DataField="CallBeforeShip" HeaderText="Call Before Ship" Width="9%"
                                                Visible="true" Index="8" />

                                            <%-- <obout:Column HeaderText="Ready" Width="10%" Index="8">
                                        <TemplateSettings TemplateId="templateReady" />
                                    </obout:Column>--%>

                                            <obout:Column HeaderText="Ready" Width="9%" Index="9">
                                                <TemplateSettings TemplateId="templatePaid" />
                                            </obout:Column>
                                            <obout:Column DataField="Note" HeaderText="Note" ReadOnly="True" Wrap="true"
                                                Visible="true" Index="10" Width="15%" ></obout:Column>
                                             <obout:Column DataField="HotNotes" HeaderText="Hot Note" ReadOnly="True" Wrap="true"
                                            Visible="true" Index="11" Width="10%" ></obout:Column>
                                        </Columns>
                                       
                                        

                                <Templates>

                                    <obout:GridTemplate ID="templateDelete">
                                        <Template>
                                            <a href="#" onclick="grdOderClose_Delete(<%# Container.DataItem["OrderID"].ToString()%>);">Delete</a>
                                        </Template>
                                    </obout:GridTemplate>
                                    <obout:GridTemplate ID="templateEdit">
                                        <Template>
                                            <a href="#" onclick="grdOderClose_Edit(<%# Container.DataItem["OrderID"].ToString()%>);">Edit</a>
                                        </Template>
                                    </obout:GridTemplate>
                                    <%-- <obout:GridTemplate ID="templateReady">
                                        <Template>
                                            <a href="#" onclick="GrdReadyStatus(<%# Container.DataItem["OrderID"].ToString()%>);">Ready</a>
                                        </Template>
                                    </obout:GridTemplate>--%>
                                    <obout:GridTemplate ID="templatePaid">
                                        <Template>
                                            <a href="#" onclick="GrdPaidStatus(<%# Container.DataItem["OrderID"].ToString()%>);">Paid</a>
                                        </Template>
                                    </obout:GridTemplate>

                                </Templates>
                                        <LocalizationSettings NoRecordsText="No Record Found!" />

                                    </obout:Grid>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="skedWindowOrder" CssClass="modalPopup" Width="1000px" runat="server" Height="97%"
                    Style="position: absolute !important; top: 10px !important;" ClientIDMode="Static">
                    <div class="popup_Container" style="height: 97%; width: 1000px">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Order Details
                                <img src="../images/close-icon.png" onclick="CloseOrderItem();" style="height: 15px; width: 15px; float: right" />
                              
                            </div>
                        </div>
                        
                        <br />
                        <table style="margin-left: 10px; margin-right: 10px; width: 1000px; height: 95%;" cellpadding="0" cellspacing="0">
                            
                            <tr>

                                <td style="width: 430px" class="verticalAlign">
                                    <asp:Label ID="lblName" CssClass="PageTitle" ClientIDMode="Static" runat="server" value=""></asp:Label>&nbsp;&nbsp;&nbsp;

                                        <asp:Label ID="lblShipDate" runat="server" ClientIDMode="Static" value="" CssClass="PageTitle"></asp:Label>
                                </td>
                                <td style="width: 455px" class="verticalAlign ">
                                    <input type="checkbox" runat="server" clientidmode="Static" id="FreeShipping" onclick="UpdateFreeShippingStatus()"/>Free Shipping &nbsp;
                                    <input type="checkbox" runat="server" clientidmode="Static" id="Invoiced"/>Invoiced &nbsp; 
                                    <input type="checkbox" runat="server" clientidmode="Static" id="Paid" onclick="UpdatePaidStatus()"  />Paid &nbsp;
                                </td>
                                <td class="verticalAlign" style="padding-right: 13px">
                                    <input type="button" id="btnReady" onclick="UpdateStatus()" runat="server" style="float: right;" value="Ready" class="AutoShipButon"/>
                                </td>
                            </tr>
                            
                            <tr>
                                <td align="left" class="verticalAlign PageTitle" style="width: 430px" >Billing Address:<br />
                                    <textarea rows="4" cols="50" id="txtBillingAddress" disabled="disabled" clientidmode="Static" runat="server"></textarea>
                                </td>
                                <td class="verticalAlign PageTitle" style="width: 455px" align="right">Shipping Address:<br />
                                    <table>
                                        <tr>
                                            <td class="verticalAlign PageTitle">Address:</td>
                                            <td class="verticalAlign">
                                                <input type="text" runat="server" clientidmode="Static" id="txtShipAddress" /></td>
                                            <td class="verticalAlign PageTitle">City:</td>
                                            <td class="verticalAlign">
                                                <input type="text" runat="server" clientidmode="Static" id="txtShipCity" /></td>
                                        </tr>

                                        <tr>
                                            <td class="verticalAlign PageTitle">State: </td>
                                            <td class="verticalAlign">
                                                <input type="text" runat="server" clientidmode="Static" id="txtShipState" /></td>

                                            <td class="verticalAlign PageTitle">Zip:</td>
                                            <td class="verticalAlign">
                                                <input type="text" runat="server" clientidmode="Static" id="txtShipZip" /></td>
                                        </tr>
                                    </table>
                                    <%-- <textarea rows="4" cols="50" id="txtShippingAddress" clientidmode="Static" runat="server"></textarea>--%>
                                </td>
                                <td style="padding-right: 13px">
                                    <%--<input type="button" id="btnInvoicePayment" clientidmode="Static" onclick="InvoicePayment()" runat="server" style="float: right;" value="Pay Invoice"></input><br />
                                    <br />--%>
                                    <input type="button" id="btnInvoiceCreation" clientidmode="Static" onclick="CreateXeroContact()" runat="server" style="float: right;" value="Create Invoice" class="AutoShipButon" />
                                </td>
                            </tr>
                            <tr >
                                <td align="left" class="verticalAlign PageTitle" style="height: 200px">AutoShip Notes<br />

                                    <obout:Editor ID="txtAutoShipNotes" runat="server">
                                        <TopToolbar Appearance="Custom">
                                            <AddButtons>
                                                <obout:Bold />
                                                <obout:Italic />
                                                <obout:Underline />
                                                <obout:StrikeThrough />
                                                <obout:HorizontalSeparator />
                                                <obout:FontName />
                                                <obout:FontSize />
                                                <obout:VerticalSeparator />
                                                <obout:Undo />
                                                <obout:Redo />
                                                <obout:HorizontalSeparator />
                                                <obout:PasteWord />
                                                <obout:HorizontalSeparator />
                                                <obout:JustifyLeft />
                                                <obout:JustifyCenter />
                                                <obout:JustifyRight />
                                                <obout:JustifyFull />
                                                <obout:HorizontalSeparator />
                                                <obout:SpellCheck />

                                            </AddButtons>
                                        </TopToolbar>
                                    </obout:Editor>
                                    <%--<textarea rows="4" cols="50" id="txtAutoShipNotes" runat="server" clientidmode="Static"></textarea>--%>
                                </td>
                                
                                <td class="verticalAlign PageTitle" align="right" style="height: 200px">Hot Notes<br />
                                    <textarea rows="20" cols="50" id="txtHotNotes" enableviewstate="false" runat="server" clientidmode="Static" style="margin-left: 5px;height: 100px"></textarea><br /><br />
                                    Order Notes<br />
                                    <textarea rows="20" cols="50" id="txtOrderNotes" runat="server" clientidmode="Static" style="margin-left: 5px;height: 100px"></textarea>
                                </td>
                            </tr>

                            <tr>
                                <td class="" align="right">
                                    <input type="button" runat="server" onclick="btnUpdateAutoShipNote()" value="Update" class="AutoShipButon" style="margin-top:10px;"/>
                                    <%-- <asp:Button ID="btnUpdateAutoShipNote" OnClientClick="btnUpdateAutoShipNote()" Text="Update" runat="server" />--%>
                                </td>
                                <td class="" align="right">
                                    <input type="button" runat="server" onclick="btnUpdateOrderNote()" value="Update" class="AutoShipButon" style="margin-top:10px;"/>
                                    <%--<asp:Button ID="btnOrderNote" Text="Update" OnClientClick="btnUpdateOrderNote()" runat="server" />--%>
                                </td>
                            </tr>
                            <tr>

                                <td colspan="3" class="verticalAlign PageTitle">
                                    <asp:Label ID="lblTotalPaymentAmount" runat="server" ClientIDMode="Static"></asp:Label></td>
                            </tr>
                            <tr>
                                <td colspan="3" class="verticalAlign">
                                    <table id="grdCloseOrderItems"></table>
                                    <div id="DivNoRecord1" style="visibility: hidden"><span>No Record</span></div>
                                    <div id="pagernav1"></div>

                                </td>
                            </tr>
                            

                        </table>
                        <input type="hidden" id="inpOrderId" value="0" runat="server" clientidmode="static" />
                         <input type="hidden" id="inpUserId" value="0" runat="server" clientidmode="static" />
                        <input type="hidden" id="inpOrderItemId" value="0" runat="server" clientidmode="static" />
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalAlert" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy" PopupControlID="skedWindowOrder"
                    Y="20" ClientIDMode="static" />
                <asp:Panel ID="skedEditQty" CssClass="modalPopup" Width="200px" runat="server" Height="100px"
                    Style="position: absolute; z-index: 10000000; top: 50%;" ClientIDMode="Static">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Change Quantity
                                <img src="../images/close-icon.png" onclick="CloseQty();" style="float: right; height: 23px; width: 23px;" />
                                <%-- <input type="button" class="button" onclick="CloseQty();" value="Close" style="float:right"/>--%>
                            </div>
                        </div>

                        <input type="hidden" id="Hidden1" value="0" runat="server" clientidmode="static" />

                        <table style="margin-left: 10px; margin-right: 10px; width: 190px; height: 90px;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="align-content: center">Qtantity:&nbsp;&nbsp;<input type="text" id="inpQty" runat="server" clientidmode="Static" style="width: 70px" />
                                    <input type="button" class="button" runat="server" onclick="UpdateQty()" value="Update Qty" /></td>
                            </tr>

                        </table>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy2" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalQty" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy2" PopupControlID="skedEditQty"
                    Y="200" ClientIDMode="static" />




                <asp:Panel ID="skedAddOrderItem" CssClass="modalPopup" Width="500px" runat="server" Height="100px"
                    Style="position: absolute; z-index: 10000000; top: 50%;" ClientIDMode="Static">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Add Order Items
                                  <img src="../images/close-icon.jpeg" onclick="CloseAddItem();" style="height: 23px; width: 23px; float: right" />
                                <%-- <input type="button" class="button" onclick="CloseQty();" value="Close" style="float:right"/>--%>
                            </div>
                        </div>

                        <input type="hidden" id="Hidden2" value="0" runat="server" clientidmode="static" />


                        <table style="margin-left: 10px; margin-right: 10px; width: 90%; height: 150px;" cellpadding="2" cellspacing="0">
                            <tr>
                                <td>Sku:
                                </td>
                                <td>
                                    <asp:DropDownList ID="drpProduct" runat="server" Style="width: 300px;" ClientIDMode="Static" onchange="GetProductData(this.options[this.selectedIndex].value);"></asp:DropDownList>
                            </tr>

                            <tr>
                                <td>Product Name</td>
                                <td>
                                    <asp:Label ID="lblProductName" runat="server" Text="" ClientIDMode="Static" CssClass="FormField"></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Quantity:</td>
                                <td>
                                    <input type="text" id="txtQuantity" runat="server" clientidmode="Static" class="FormField" /></td>
                            </tr>
                            <tr>
                                <td>Price:</td>
                                <td>
                                    <input type="text" id="txtPrice" runat="server" clientidmode="Static" class="FormField" /></td>
                            </tr>
                            <tr>
                                <td>Weight:</td>
                                <td>
                                    <input type="text" id="txtWeight" runat="server" clientidmode="Static" class="FormField" /></td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <input type="button" runat="server" onclick="AddOrderitems();" clientidmode="Static" value="Add Item" /></td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy3" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalAddOrderItem" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy3" PopupControlID="skedAddOrderItem"
                    Y="200" ClientIDMode="static" />

                <asp:Panel ID="skedAddXeroContact" CssClass="modalPopup" Width="1000px" runat="server" Height="650px"
                    Style="position: absolute; top: 20px!important;" ClientIDMode="Static" ScrollBars="Auto">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Xero Contact
                                <img src="../images/close-icon.png" onclick="CloseModalXeroContact();" style="height: 23px; width: 23px; float: right" />

                            </div>
                        </div>


                        <div id="MatchDialog">
                            This Patient has no linked in Xero contacts. Please choose an existing customers or create a new.<br />
                            <br />

                            <asp:Label ID="lblPatientName" ClientIDMode="Static" runat="server" value=""></asp:Label>&nbsp;&nbsp;&nbsp;
                              
                            <input type="hidden" id="XeroContactId" value="0" runat="server" clientidmode="static" />

                            <br />

                            <label id="FirstNameLblT">First Name:-</label>
                            <input placeholder="First Name" type="text" id="FirstNameS" />

                            <label id="LastNameLblT">Last Name:-</label><input type="text" placeholder="Last Name" id="LastNameS" />



                            <input id="SearchPatientsXeroButton" type="button" value="Search" onclick="SearchMatchXeroPatientsData('#grdXeroPatientsMatchSearch', '#pagernav2', '2')" />


                            <div id="XeroPatientsMatchSearch">
                                <table id="grdXeroPatientsMatchSearch"></table>
                                <div id="DivNoRecord2" style="visibility: hidden"><span>No Record</span></div>
                                <div id="pagernav2"></div>
                            </div>

                            <br />
                            <br />
                            <input type="button" value="Create New" onclick="CreateXeroInvoice()" /><br />
                            <br />
                        </div>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy4" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalXeroContact" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy4" PopupControlID="skedAddXeroContact"
                    Y="200" ClientIDMode="static" />


                <asp:Panel ID="skedPayment" CssClass="modalPopup" Width="500px" runat="server" Height="400px"
                    Style="position: absolute; z-index: 10000000; top: 100px;" ClientIDMode="Static">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Payment
                                <img src="../images/close-icon.png" onclick="ClosePaymentModal();" style="float: right; height: 23px; width: 23px;" />

                            </div>
                        </div>

                        <table style="margin-left: 10px; margin-right: 10px; width: 500px; height: 400px;" cellpadding="1" cellspacing="1">
                            <tr>
                                <td colspan="2"></td>
                            </tr>
                            <tr>
                                <td></td>
                                <td>Total Due:&nbsp;&nbsp;<asp:Label ID="lblPaymentDue" runat="server" ClientIDMode="Static" Style="width: 150px" /></td>
                            </tr>
                            <tr>
                                <td>Amount</td>
                                <td>
                                    <input type="text" id="txtPaymentAmount" runat="server" clientidmode="Static" style="width: 150px" /></td>
                            </tr>
                            <tr>
                                <td>Date</td>
                                <td>
                                    <asp:TextBox ID="txtPaymentDate" runat="server" ClientIDMode="Static" Text='<%#DateTime.Today.ToString("MM/dd/yyyy")%>' />
                                    <asp:CompareValidator ID="CompareValidator2" runat="server" ControlToValidate="txtPaymentDate"
                                        Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                                        EnableClientScript="true" />
                                    <cc1:CalendarExtender ID="CalendarExtender5" ClientIDMode="Static" runat="server" TargetControlID="txtPaymentDate" Format="MM/dd/yyyy" />
                                </td>
                            </tr>
                            <tr>
                                <td>Reference</td>
                                <td>
                                    <input type="text" runat="server" clientidmode="Static" id="txtPaymentReference" /></td>
                            </tr>
                            <tr>
                                <td>Account</td>
                                <td>
                                    <asp:DropDownList ID="drpXeroAccounts" runat="server" Style="width: 300px;" ClientIDMode="Static"></asp:DropDownList></td>
                            </tr>
                            <tr>
                                <td colspan="2" style="align-content: center">
                                    <input type="button" class="button" runat="server" onclick="UpdatePayment()" value="Payment" /></td>
                            </tr>

                        </table>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy5" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalXeroPayment" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy5" PopupControlID="skedPayment"
                    Y="200" ClientIDMode="static" />




                <asp:Panel ID="skedDelete" CssClass="modalPopup" Width="600px" runat="server" Height="300px"
                    Style="position: absolute; z-index: 10000000; top: 30%;" ClientIDMode="Static">
                    <div class="popup_Container">
                        <div class="popup_Titlebar">
                            <div class="TitlebarLeft">
                                Delete Order
                                <img src="../images/close-icon.png" onclick="CloseDeleteOrder();" style="float: right; height: 23px; width: 23px;" />

                            </div>
                        </div>
                        <input type="hidden" id="hdnDeleteOrderId" value="0" runat="server" clientidmode="static" />
                        <table style="margin-left: 10px; margin-right: 10px; width: 590px; height: 90px;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="lblDeleteText" runat="server" ClientIDMode="Static" value=""></asp:Label></td>
                            </tr>
                            <tr>
                                <td>Invoice Deleted in Xero:</td>
                                <td>
                                    <input type="checkbox" runat="server" clientidmode="Static" id="chkDeleteInXero" />
                                </td>
                            </tr>
                            <tr>
                                <td>Reason:</td>
                                <td>
                                    <textarea rows="10" cols="50" id="txtDeleteReason" runat="server" clientidmode="Static" style="margin-left: 5px;"></textarea>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" style="align-content: center">
                                    <input type="button" class="button" runat="server" onclick="CloseDeleteOrder()" value="Cancel" />&nbsp;&nbsp; 
                                    <input type="button" class="button" runat="server" onclick="DeleteOrder()" value="Delete" /></td>
                            </tr>

                        </table>
                    </div>
                </asp:Panel>
                <asp:Button ID="dummy6" runat="server" Style="display: none" />
                <cc1:ModalPopupExtender ID="ModalDeleteOrder" BackgroundCssClass="ModalPopupBG" runat="server"
                    TargetControlID="dummy6" PopupControlID="skedDelete"
                    Y="200" ClientIDMode="static" />

            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="TabPanel1" runat="server" HeaderText="Reports">
            <ContentTemplate>
                <asp:UpdatePanel ID="ReportsPanel" runat="server">
                    <ContentTemplate>
                        <asp:Menu ID="ReportsMenu" runat="server" OnMenuItemClick="ReportsMenu_MenuItemClick"
                            Orientation="Horizontal" BackColor="#FFFBD6" DynamicHorizontalOffset="2" Font-Names="Verdana"
                            Font-Size="0.8em" ForeColor="#990000" StaticSubMenuIndent="10px">
                            <DynamicHoverStyle BackColor="#990000" ForeColor="White" />
                            <DynamicMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <DynamicMenuStyle BackColor="#FFFBD6" />
                            <DynamicSelectedStyle BackColor="#FFCC66" />
                            <DynamicItemTemplate>
                                <%# Eval(   "Text") %>
                            </DynamicItemTemplate>
                            <Items>
                                <asp:MenuItem Text="Cancelled orders" Value="1" />
                                <asp:MenuItem Text="Open Orders" Value="2" />
                                <asp:MenuItem Text="Product Demand" Value="3" />
                            </Items>
                            <StaticHoverStyle BackColor="#990000" ForeColor="White" />
                            <StaticMenuItemStyle HorizontalPadding="5px" VerticalPadding="2px" />
                            <StaticSelectedStyle BackColor="#FFCC66" />
                        </asp:Menu>
                        <br />
                        <div id="ProdReportVis" runat="server" visible="false">
                            Report demand as of:
							<asp:TextBox ID="txtProdDate" runat="server" Text='<%# DateTime.Today.AddDays(30).ToString("MM/dd/yyyy") %>' />
                            <asp:CompareValidator ID="CompareValidator6" runat="server" ControlToValidate="txtProdDate"
                                Type="Date" Operator="DataTypeCheck" ErrorMessage="Please enter a valid date."
                                EnableClientScript="true" />
                            <cc1:CalendarExtender ID="CalendarExtender1" runat="server" TargetControlID="txtProdDate" />
                            <br />
                            <asp:Button ID="btnProdGen" runat="server" Text="Create Report" CssClass="button"
                                OnClick="btnProdGen_Click" />

                            <h4 id="ProductRreportHeader" runat="server"></h4>
                            <asp:GridView ID="GrdProductDemand" runat="server"
                                AutoGenerateColumns="false" BackColor="#DEBA84"
                                BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px" DataKeyNames="ProductName"
                                OnRowDataBound="GrdProductDemand_RowDataBound">
                                <Columns>

                                    <asp:BoundField DataField="ProductName" HeaderText="ProductName" />
                                    <asp:BoundField DataField="Day1" />
                                    <asp:BoundField DataField="Day2" />
                                    <asp:BoundField DataField="Day3" />
                                    <asp:BoundField DataField="Day4" />
                                    <asp:BoundField DataField="Day5" />
                                    <asp:BoundField DataField="Day6" />
                                    <asp:BoundField DataField="Day7" />

                                </Columns>
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>
                        </div>
                        <div id="VisDiv" runat="server" visible="false">
                            <h4 id="ReportHeader" runat="server"></h4>
                            <asp:GridView ID="ReportsGrid" runat="server" BackColor="#DEBA84" BorderColor="#DEBA84"
                                BorderStyle="None" BorderWidth="1px">
                                <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                                <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                                <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                                <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                                <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                                <SortedAscendingCellStyle BackColor="#FFF1D4" />
                                <SortedAscendingHeaderStyle BackColor="#B95C30" />
                                <SortedDescendingCellStyle BackColor="#F1E5CE" />
                                <SortedDescendingHeaderStyle BackColor="#93451F" />
                            </asp:GridView>


                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel HeaderText="Manage Products" runat="server" ID="TabPanel2" CssClass="TabPanel">
            <ContentTemplate>
                <asp:UpdatePanel ID="ProductsPanel" runat="server">
                    <ContentTemplate>
                        <h4>Manage Products</h4>
                        <br />
                        <obout:Grid ID="ProductsGrid" runat="server" ShowLoadingMessage="false" AllowPaging="true"
                            PageSize="10" AllowSorting="true" AutoGenerateColumns="false" AllowAddingRecords="true"
                            AllowPageSizeSelection="true" Serialize="false" CellPadding="0" Width="1050"
                            AllowFiltering="true" AllowRecordSelection="true" CellSpacing="0" ShowTotalNumberOfPages="false"
                            OnUpdateCommand="ProductsGrid_RowUpdating" OnInsertCommand="btnAddProduct_Click"
                            FolderStyle="../grid_styles/Style_7" DataSourceID="ProductsSource">
                            <Columns>

                                <obout:Column DataField="ProductID" HeaderText="Product ID" ReadOnly="True" SortExpression="ProductID" />
                                <obout:Column DataField="Sku" HeaderText="Sku" SortExpression="Sku" Width="100" HeaderAlign="center" />
                                <obout:Column DataField="ProductName" HeaderText="Product Name" SortExpression="ProductName" />
                                <obout:Column DataField="AutoshipPrice" HeaderText="Retail Price" SortExpression="AutoshipPrice"
                                    DataFormatString="{0:C}" />
                                <obout:Column DataField="Weight" HeaderText="Weight" SortExpression="Weight" Width="100" Align="center" HeaderAlign="center" />
                                <obout:CheckBoxColumn DataField="Active" HeaderText="Available for Auto Ship" Width="150" />
                                <obout:CheckBoxColumn DataField="Viewable" HeaderText="Viewable for Prescriptions"
                                    Width="175" />
                                <obout:CheckBoxColumn DataField="Reviewed" HeaderText="Reviewed" Width="100" />
                                <obout:CheckBoxColumn DataField="Bundle" HeaderText="Bundle" Width="100" />
                                <obout:Column ID="Column1" HeaderText="Edit" AllowEdit="true" AllowDelete="false"
                                    Width="125" runat="server" />
                            </Columns>


                            <ClientSideEvents OnBeforeClientInsert="ValidateManageProduct" OnBeforeClientUpdate="ValidateManageProduct" OnClientUpdate="MessageAfterUpdateRecords" OnClientInsert="MessageAfterAddRecords" />

                        </obout:Grid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="ManageRights" HeaderText="Manage Access" runat="server">
            <ContentTemplate>
                <asp:UpdatePanel ID="ManageRightsPanel" runat="server">
                    <ContentTemplate>
                        <asp:GridView ID="ManageRightsGrid" runat="server" AutoGenerateEditButton="True"
                            BackColor="#DEBA84" BorderColor="#DEBA84" BorderStyle="None" BorderWidth="1px"
                            AutoGenerateColumns="false" DataKeyNames="EmployeeID,EmployeeName,username" DataSourceID="StaffSource"
                            AllowPaging="true" PageSize="15" CellPadding="2" CellSpacing="2" OnRowUpdating="ManageRightsGrid_RowUpdating"
                            OnRowEditing="ManageRightsGrid_RowEditing" OnDataBound="ManageRightsGrid_DataBound">
                            <Columns>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID" ReadOnly="true" />
                                <asp:BoundField DataField="EmployeeName" HeaderText="Employee Name" ReadOnly="true" />
                                <asp:BoundField DataField="username" HeaderText="UserName" ReadOnly="true" />
                                <asp:TemplateField HeaderText="Access Level">
                                    <ItemTemplate>
                                        <asp:Label ID="lblAccess" runat="server" Text='<%# Eval("AutoshipAccess") %>' />
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="ddAccess" runat="server">
                                            <asp:ListItem Text="Manager" Value="Manager" />
                                            <asp:ListItem Text="Records" Value="Records" />
                                            <asp:ListItem Text="ProdManager" Value="ProdManager" />
                                            <asp:ListItem Text="FrontDesk" Value="FrontDesk" />
                                            <asp:ListItem Text="Blank" Value="Blank" />
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <FooterStyle BackColor="#F7DFB5" ForeColor="#8C4510" />
                            <HeaderStyle BackColor="#A55129" Font-Bold="True" ForeColor="White" />
                            <PagerStyle ForeColor="#8C4510" HorizontalAlign="Center" />
                            <RowStyle BackColor="#FFF7E7" ForeColor="#8C4510" />
                            <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="White" />
                            <SortedAscendingCellStyle BackColor="#FFF1D4" />
                            <SortedAscendingHeaderStyle BackColor="#B95C30" />
                            <SortedDescendingCellStyle BackColor="#F1E5CE" />
                            <SortedDescendingHeaderStyle BackColor="#93451F" />
                        </asp:GridView>
                        <asp:SqlDataSource runat="server" ID="StaffSource" SelectCommand="Select EmployeeID,EmployeeName,username,AutoshipAccess from staff order by EmployeeName"
                            UpdateCommand="Update Staff set AutoshipAccess=@AutoshipAccess where EmployeeID = @EmployeeID"
                            ConnectionString="<%$ ConnectionStrings:db %>" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>
    <asp:SqlDataSource runat="server" ID="ProductsSource" SelectCommand="SELECT [ProductID], [ProductName], STR([AutoshipPrice],10,2)as AutoShipPrice,Active,Viewable,Reviewed,Sku,Weight,ISNULL(Bundle,0) AS Bundle FROM [AutoshipProducts] ORDER BY [ProductName]"
        ConnectionString="<%$ ConnectionStrings:db %>" UpdateCommand="UPDATE AutoshipProducts SET ProductName = @ProductName, AutoshipPrice = @AutoshipPrice, Active=@Active,Sku=@Sku,Weight=@Weight,Bundle=@Bundle WHERE (ProductID = @ProductID)" />
    <asp:SqlDataSource runat="server" ID="DiscountSource" SelectCommand="Select DiscountID,DiscountName from Autoship_Discounts order by DiscountID"
        ConnectionString="<%$ ConnectionStrings:db %>" />
</asp:Content>






