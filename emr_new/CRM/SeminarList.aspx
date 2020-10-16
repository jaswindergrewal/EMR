<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/external/Site.master" CodeFile="SeminarList.aspx.cs" Inherits="CRM_SeminarList" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="owd" Namespace="OboutInc.Window" Assembly="obout_Window_NET" %>
<%@ Register TagPrefix="obout" Namespace="Obout.Interface" Assembly="obout_Interface" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>

    <style>
        .floating-box {
            float: left;
            width: 155px;
            line-height: 110px;
            margin: 10px;
            border: 3px solid #000;
            background: #00FFFF;
            text-align: center;
            height: 120px;
        }

        .main-box {
            float: left;
            width: 155px;
            line-height: 100px;
            margin: 10px;
            margin-top:40px;
            text-align: center;
            height: 120px;
        }
        span {
            display: inline-block;
            vertical-align: middle;
            line-height: normal;
        }

        .box-right{
            clear:both;
        }
    </style>
    <script type="text/javascript">
        $(document).ready(function () {

            GetClinic();
            setInterval(function () {
                GetAppointments();
            }, 200000);

            $('#ddlClinic').change(function () {

                debugger;
                var Isvalue = "1";
                var ClinicID = $('#ddlClinic').val();;

                var postData = new Object();
                postData.Isvalue = Isvalue;
                postData.ClinicID = ClinicID;
                $.ajax({
                    type: "POST",
                    url: "SeminarList.aspx/GetPostSeminars",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var result = response.d;
                        if (result != null) {
                            $("#divList").html("");
                            if (result.length > 0) {
                                var previousDate = "";

                                var color = "rgb(244, 150, 43)";
                                    for (var i = result.length - 1 ; i >= 0; i--) {
                                        if (result[i].WeekdayName == "Monday") {
                                            color= "rgb(244, 110, 66)";

                                        }
                                        else if (result[i].WeekdayName == "Tuesday") {
                                            color= "rgb(244, 223, 65)";

                                        }
                                        else if (result[i].WeekdayName == "Wednesday") {
                                            color= "rgb(196, 244, 65)";

                                        }
                                        else if (result[i].WeekdayName == "Thursday") {
                                            color= "rgb(237, 49, 224)";

                                        }
                                        else if (result[i].WeekdayName == "Friday") {
                                            color= "rgb(206, 199, 181)";

                                        }
                                        else if (result[i].WeekdayName == "Saturday") {
                                            color= "rgb(182, 198, 125)";

                                        }
                                    
                                    var newDate = "";
                                    newDate = result[i].FirstName;
                                    if (newDate == previousDate) {
                                        $("#divList").append("<div class='floating-box ' style='background:" + color + "'><span><b>" + result[i].WeekdayName + "</b><br><br>" + result[i].ApptText + "<br>" + result[i].LastName + "</span></div>");
                                        previousDate = result[i].FirstName;
                                    }
                                    else {
                                        $("#divList").append("<div class='box-right'></div><div class='main-box'><span>" + result[i].FirstName + "</span></div><div class='floating-box ' style='background:" + color + "'  ><span><b>" + result[i].WeekdayName + "</b><br><br>" + result[i].ApptText + "<br>" + result[i].LastName + "</span></div>");
                                        previousDate = result[i].FirstName;
                                    }
                                }
                            }
                        }

                    },
                    error: function (obj) {


                        alert(obj.responseText);
                    }
                });


                return false;
            })

        });

        function GetClinic() {

            $.ajax
                ({
                    type: "POST",
                    url: "SeminarList.aspx/GetClinic",
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {

                        var Dropdown = $('#ddlClinic');
                        Dropdown.empty();
                        Dropdown.prepend("<option value='Select Clinic' selected='selected'></option>");
                        $.each(response.d, function (index, item) {

                            Dropdown.append($("<option></option>").val(item.ClinicName).html(item.ClinicName));

                        });
                    },
                    error: function () {
                        alert("Failed to load data");
                    }
                });
        }

        function GetAppointments() {


            var Isvalue = "1";

            var ClinicID = $('#ddlClinic').val();;

            var postData = new Object();
            postData.Isvalue = Isvalue;
            postData.ClinicID = ClinicID;
            $.ajax({
                type: "POST",
                url: "SeminarList.aspx/GetPostSeminars",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var result = response.d;
                    if (result != null) {
                        $("#divList").html("");
                        if (result.length > 0) {
                            var previousDate = "";
                            
                                var color = "rgb(244, 150, 43)";
                                for (var i = result.length - 1 ; i >= 0; i--) {
                                    if (result[i].WeekdayName == "Monday") {
                                        color= "rgb(244, 110, 66)";

                                    }
                                    else if (result[i].WeekdayName == "Tuesday") {
                                        color= "rgb(244, 223, 65)";

                                    }
                                    else if (result[i].WeekdayName == "Wednesday") {
                                        color= "rgb(196, 244, 65)";

                                    }
                                    else if (result[i].WeekdayName == "Thursday") {
                                        color= "rgb(237, 49, 224)";

                                    }
                                    else if (result[i].WeekdayName == "Friday") {
                                        color= "rgb(206, 199, 181)";

                                    }
                                    else if (result[i].WeekdayName == "Saturday") {
                                        color= "rgb(182, 198, 125)";

                                    }
                                    
                                var newDate = "";
                                newDate = result[i].FirstName;
                                if (newDate == previousDate) {
                                    $("#divList").append("<div class='floating-box ' style='background:" + color + "'><span><b>" + result[i].WeekdayName + "</b><br><br>" + result[i].ApptText + "<br>" + result[i].LastName + "</span></div>");
                                    previousDate = result[i].FirstName;
                                }
                                else {
                                    $("#divList").append("<div class='box-right'></div><div class='main-box'><span>" + result[i].FirstName + "</span></div><div class='floating-box ' style='background:" + color + "'><span><b>" + result[i].WeekdayName + "</b><br><br>" + result[i].ApptText + "<br>" + result[i].LastName + "</span></div>");
                                    previousDate = result[i].FirstName;
                                }
                            }
                        }
                    }

                },
                error: function (obj) {


                    alert(obj.responseText);
                }
            });
        }


    </script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <span style="margin-left:20px;">Clinic</span><br />
            <br />
            <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormField " style="margin-left:20px;" ClientIDMode="Static"
                DataTextField="ClinicName" DataValueField="ClinicName" />
            <div id="divList" runat="server" clientidmode="Static"></div>
        </ContentTemplate>
    </asp:UpdatePanel>

</asp:Content>

