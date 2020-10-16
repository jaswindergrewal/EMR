<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CallFirePage.aspx.cs" MasterPageFile="~/Site.master" Inherits="CallFirePage" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
    <title>Autoship Home</title>
    <script src="../Scripts/jquery-1.7.2.js" type="text/javascript"></script>
    <script src="../Scripts/Scrips.js" type="text/javascript"></script>
    <link href="../css/lmc_style.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        //$("document").ready(function () {
        //    setTimeout(function () { $("#loading-div-background").show(); }, 3000);




        //});
        //Function to format the date in mm/dd/yyyy 
        function convertDate(inputFormat) {
            debugger;
            var d = new Date(inputFormat);
            return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join('/');
        }

        function my_date_format(inputDate) {
            debugger;
            var d = new Date(inputDate);
            var month = ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'];
            var date = d.getDay() + " " + month[d.getMonth()] + ", " + d.getFullYear();
            var time = d.toLocaleTimeString().toLowerCase();
            return (date + " at " + time);
        };

        function changecolor(elem) {
            debugger;
            var isIE = /*@cc_on!@*/false || !!document.documentMode;
            if (isIE == false) {
                if ($(elem).css("background-color") == "rgb(0, 128, 0)") {
                    $(elem).css('background-color', 'rgb(250 ,250, 210)');
                    $(elem).removeClass();
                }
                else if ($(elem).css("background-color") == "rgb(250, 250, 210)") {
                    $(elem).css('background-color', 'rgb(0, 128, 0)');
                    $(elem).removeClass();
                }
            }
            else {
                if ($(elem).css("background-color") == "rgb(0,128,0)") {
                    $(elem).css('background-color', 'rgb(250,250,210)');
                    $(elem).removeClass();
                }
                else if ($(elem).css("background-color") == "rgb(250,250,210)") {
                    $(elem).css('background-color', 'rgb(0,128,0)');
                    $(elem).removeClass();
                }
            }
        }

        function GetDataforCall() {

            var data = "";
            $('#<%=loadingdiv.ClientID%>').show();
            //setTimeout(function () {
            var varisIE = /*@cc_on!@*/false || !!document.documentMode;
            $('#tblPatient tr').each(function () {
                var grey4 = false;
                var green4 = false;
                var grey5 = false;
                var green5 = false;
                var grey6 = false;
                var green6 = false;
                var phonenumber;
                var name;
                var apptStartTime;
                var apptEndTime;
                var columns = $(this).find('td');

                columns.each(function () {

                    var colIndex = $(this).index();
                    if (colIndex == 2) {
                        name = $(this).text();
                    }
                    if (colIndex == 3) {
                        name = name + " " + $(this).text();
                    }

                    if (colIndex == 4) {
                        apptStartTime = $(this).text();
                    }

                    if (colIndex == 5) {
                        apptEndTime = $(this).text();
                    }
                    debugger;
                    if (colIndex == 6) {

                        if (varisIE == false) {
                            if ($(this).css("background-color") == "rgb(128, 128, 128)") {
                                grey4 = true;
                            }
                            else {
                                if ($(this).text() != "") {
                                    if ($(this).css("background-color") == "rgb(0, 128, 0)") {
                                        green4 = true;
                                        phonenumber = $(this).text();
                                    }
                                    else {
                                        phonenumber = $(this).text();
                                    }
                                }
                            }
                        }
                        else {
                            if ($(this).css("background-color") == "rgb(128,128,128)") {
                                grey4 = true;
                            }
                            else {
                                if ($(this).text() != "") {
                                    if ($(this).css("background-color") == "rgb(0,128,0)") {
                                        green4 = true;
                                        phonenumber = $(this).text();
                                    }
                                    else {
                                        phonenumber = $(this).text();
                                    }
                                }
                            }
                        }

                    }

                    if (colIndex == 7) {
                        if (green4 == false) {
                            if (varisIE == false) {
                                if ($(this).css("background-color") == "rgb(128, 128, 128)") {
                                    grey5 = true;
                                }
                                else {
                                    if ($(this).text() != "") {
                                        if ($(this).css("background-color") == "rgb(0, 128, 0)") {
                                            green5 = true;
                                            phonenumber = $(this).text();
                                        }
                                        else {
                                            phonenumber = $(this).text();
                                        }
                                    }
                                }
                            }
                            else {
                                if ($(this).css("background-color") == "rgb(128,128,128)") {
                                    grey5 = true;
                                }
                                else {
                                    if ($(this).text() != "") {
                                        if ($(this).css("background-color") == "rgb(0,128,0)") {
                                            green5 = true;
                                            phonenumber = $(this).text();
                                        }
                                        else {
                                            phonenumber = $(this).text();
                                        }
                                    }
                                }
                            }
                        }
                        else {
                            green5 = true;
                        }

                    }

                    if (colIndex == 8) {
                        if (green5 == false) {
                            if (varisIE == false) {
                                if ($(this).css("background-color") == "rgb(128, 128, 128)") {
                                    grey6 = true;
                                }
                                else {
                                    if ($(this).text() != "") {
                                        if ($(this).css("background-color") == "rgb(0, 128, 0)") {
                                            green6 = true;
                                            phonenumber = $(this).text();
                                        }
                                        else {
                                            phonenumber = $(this).text();
                                        }
                                    }
                                }
                            }
                            else {
                                if ($(this).css("background-color") == "rgb(128,128,128)") {
                                    grey6 = true;
                                }
                                else {
                                    if ($(this).text() != "") {
                                        if ($(this).css("background-color") == "rgb(0,128,0)") {
                                            green6 = true;
                                            phonenumber = $(this).text();
                                        }
                                        else {
                                            phonenumber = $(this).text();
                                        }
                                    }
                                }
                            }
                        }


                    }

                });
                if (phonenumber != "") {
                    debugger;
                    if (phonenumber != undefined) {

                        data += name + "~" + apptStartTime + "~" + phonenumber + "/";
                    }
                }
            });
            document.getElementById("Selecteddataforcall").value = data;
            document.getElementById("SelectedCampaignId").value = $('#<%=ddlAppointmentType.ClientID %>').val();

            // }, 1000);
        }

        function BindGrid() {

            $('#<%=loadingdiv.ClientID%>').show();
            setTimeout(function () {
                var txtstartdate = document.getElementById('txtBegin');
                var txtenddate = document.getElementById('txtEnd');
                var Dropdown = $('#<%=ddlAppointmentType.ClientID %>');
                var clinic = $('#<%=ddlClinic.ClientID %>');
                var provider = $('#<%=ddlProviders.ClientID %>');
                //$('input[name="SelectedProductId"]').val(Dropdown.val());
                var appointmentType = Dropdown.val();
                var startDate = txtstartdate.value;
                var endDate = txtenddate.value;
                var clinicData = clinic.val();
                var providerData = provider.val();

                var postData = new Object();

                postData.startDate = startDate;
                postData.endDate = endDate;
                postData.appointmentType = appointmentType;
                postData.clinicData = clinicData;
                postData.providerData = providerData;


                $.ajax({
                    type: "POST",
                    url: "CallFirePage.aspx/GetPatientList",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var listObj = response.d;

                        if (listObj != null) {
                            var ClassGreyCell = "None";
                            var ClassGreyWork = "None";
                            var ClassGreyHome = "None";
                            var rowColor = "odd";
                            $("#divPatientList").html("");
                            $("#divPatientList").append(" <table width='100%' id='tblPatient'><tbody><tr><td></td><td>Patient Id</td><td>First Name</td><td>Last Name</td><td>Appt start </td><td>Appt End </td><td>Cell Phone</td> <td>Work Phone </td> <td>Home Phone </td></tr></tbody></table>");
                            for (var i = listObj.length - 1 ; i >= 0; i--) {
                                debugger;
                                if (listObj[i].Cell_NoMessage == true) {

                                    ClassGreyCell = "classcell";
                                }
                                else if (listObj[i].Cell_CB_only == true) {
                                    ClassGreyCell = "ClassGreyCell ";

                                }
                                else {
                                    if (listObj[i].ContactPreference.toLowerCase() == "cell")
                                        ClassGreyCell = "classcell";
                                    else
                                        ClassGreyCell = "None ";
                                }

                                if (listObj[i].Work_Nomessage == true) {
                                    ClassGreyWork = "classcell ";
                                }
                                else if (listObj[i].Work_CB_only == true) {
                                    ClassGreyCell = "ClassGreyWork ";

                                }
                                else {
                                    if (listObj[i].ContactPreference.toLowerCase() == "work")
                                        ClassGreyWork = "classcell";
                                    else
                                        ClassGreyWork = "None ";
                                }

                                if (listObj[i].Home_NoMessage == true) {
                                    ClassGreyHome = "classcell ";
                                }
                                else if (listObj[i].Home_CB_only == true) {
                                    ClassGreyCell = "ClassGreyWork ";

                                }
                                else {
                                    if (listObj[i].ContactPreference.toLowerCase() == "home")
                                        ClassGreyHome = "classcell";
                                    else
                                        ClassGreyHome = "None ";
                                }



                                $("#tblPatient tbody").append("<tr class=" + rowColor + "><td width='10%'><img class='removebutton' src='images/delete.gif' height='20px' width='20 px' /></td>" +
                                    "<td width='10%'>" + listObj[i].PatientID + "</td>" +
                                    "<td width='10%'> " + listObj[i].FirstName + "</td>" +
                                      "<td width='10%'> " + listObj[i].LastName + "</td>" +
                                       "<td width='12%'> " + listObj[i].ApptStart + "</td>" +
                                        "<td width='12%'> " + listObj[i].ApptEnd + "</td>" +
                                       "<td width='12%' onClick='changecolor(this)' style='background-color:rgb(250,250,210);filter: none !important;cursor:pointer' class='" + ClassGreyCell + "'>" + listObj[i].CellPhone + "</td>" +
                                        "<td width='12%' onClick='changecolor(this)' style='background-color:rgb(250,250,210); filter: none !important;cursor:pointer' class='" + ClassGreyWork + "'>" + listObj[i].WorkPhone + "</td>" +
                                         "<td width='12%' onClick='changecolor(this)' style='background-color:rgb(250,250,210);filter: none !important;cursor:pointer' class='" + ClassGreyHome + "'>" + listObj[i].HomePhone + "</td>" +

                            "</tr>");

                            }
                        }


                    },
                    error: function (obj) {
                        $('#<%=loadingdiv.ClientID%>').hide();
                        alert("Some error occured");
                    }
                });
                $('#<%=loadingdiv.ClientID%>').hide();
            }, 1000);
        }

        $(document).ready(function () {
            $('.tblPatient tr:nth-child(odd)').addClass('even');
            $(document).on('click', 'img.removebutton', function () { // <-- changes
                //alert("aa");
                $(this).closest('tr').remove();
                return false;
            });


        });


    </script>
    <style type="text/css">
        .hiddencol {
            display: none;
        }

        .ClassGreyHome {
            background-color: grey !important;
        }

        .ClassGreyWork {
            background-color: grey !important;
        }

        .ClassGreyCell {
            background-color: grey !important;
        }

        .classcell {
            background-color: rgb(0,128,0) !important;
        }

        .even {
            background-color: white;
        }
        /* tr:nth-child(odd) */
        .odd {
            background-color: lightgoldenrodyellow;
            padding-bottom: 3px;
        }
    </style>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div>
        <input type="hidden" id="SelectedCampaignId" runat="server" clientidmode="Static" />
        <input type="hidden" id="Selecteddataforcall" runat="server" clientidmode="Static" />
        <table width="100%">
            <caption>
                <h4>Call to patients</h4>
            </caption>

            <tr>
                <td colspan="3">Beginning Appointment date
									<asp:TextBox ID="txtBegin" runat="server" ClientIDMode="Static" />
                    <cc1:CalendarExtender ID="xx" runat="server" TargetControlID="txtBegin" />
                    and Ending
									<asp:TextBox ID="txtEnd" runat="server" ClientIDMode="Static" />
                    <cc1:CalendarExtender ID="CalendarExtender4" runat="server" TargetControlID="txtEnd" />


                    <asp:CompareValidator ID="CompareValidator8" runat="server" ErrorMessage="Start date must come before end date."
                        ControlToValidate="txtBegin" Type="Date" Operator="LessThanEqual" ControlToCompare="txtEnd"
                        Display="Dynamic" ForeColor="Red" />
                </td>

            </tr>
            <tr>


                <td>Appointment Type:<asp:DropDownList ID="ddlAppointmentType" runat="server" ClientIDMode="Static" AutoPostBack="false" Style="width: 200px;" />

                </td>

                <td>Clinic:<asp:DropDownList ID="ddlClinic" runat="server" ClientIDMode="Static" AutoPostBack="false" Style="width: 150px;" />


                </td>
                <td>Provider:<asp:DropDownList ID="ddlProviders" runat="server" ClientIDMode="Static" AutoPostBack="false" Style="width: 200px;" />
                    <input type="button" value="Go" onclick="return BindGrid();" class="button" /><br />

                </td>
            </tr>
            <tr>
                <td colspan="3">
                    <asp:Button ID="btnCallFire" Text="Broadcast Call" runat="server" OnClientClick="GetDataforCall();" OnClick="btnCallFire_Click" /></td>
            </tr>
            <tr>
                <td colspan="3">
                    <div id="divPatientList"></div>
                </td>
            </tr>
        </table>


    </div>
    <div id="loadingdiv" runat="server">
        <div id="loading-div" class="ui-corner-all">
            <img src="images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>

</asp:Content>
