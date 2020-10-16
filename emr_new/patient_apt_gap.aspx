<%@ Page Title="Patient Appointments Gap" Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="patient_apt_gap.aspx.cs" Inherits="patient_apt_gap" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">

        var Days ;
        var currentPageNumber = 1;
        var PAGE_SIZE = 30;

        //Added by jaswinder on 13 aug 2013  to bind the grid on submit button click
        function OnSubmitButtonClick() {
            Days = ($('#<%= ddlDays.ClientID %> option:selected').val())
            if (Days == 0) {
                alert('Please select days');
                return false;
            }
            else {
                BindGrid(1);
            }
        }

        //Added by jaswinder on 13 aug 2013 .Bind Grid at run time
        function BindGrid(data) {
            Days = ($('#<%= ddlDays.ClientID %> option:selected').val())
            var postData = new Object();
            postData.Days = Days;

            var date = new Date();

            postData.PageIndex = data;
            $.ajax({
                type: "POST",
                url: "patient_apt_gap.aspx/BindAptGrid",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#tblClinicList").find("tr:gt(2)").remove();
                    var count = 0;
                    if (listObj.length > 0) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#tblClinicList tr:eq(2)").after("<tr><td><a href='manage.aspx?patientid=" + listObj[i].PatientId + "'>" + listObj[i].LastName + "</a></td>" +
                                "<td>" + listObj[i].LastName + "</td>" +
                                "<td>" + listObj[i].Days + "</td>" +

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
    </script>
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">List of Patient Appointments Gap</p>

    <asp:HiddenField ID="totalcount" runat="server" />


    <select name="days_form" id="ddlDays" runat="server">
        <option value="0">Select Days</option>
        <option value="A">90-120 days</option>
        <option value="B">120+ days</option>
    </select>
    <input type="button" value="Submit" onclick="return OnSubmitButtonClick();" class="button" />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="tblClinicList">
        <tr bgcolor="#D6B781">
            <td colspan="3"><strong>List of Patient Appointments Gap </strong></td>
        </tr>
        <tr>
            <td width='15%'><strong>First Name </strong></td>
            <td width='20%'><strong>Last Name</strong></td>
            <td width='20%'><strong>Days Since Last Apt</strong></td>


        </tr>
        <tr>
            <td colspan='3' style='color: red; display: none;' id='tdNoRecord' align='center' visible="false"><strong>No record found !</strong></td>
        </tr>

    </table>
    <table id="tdButton" style="display: none;">
        <tr>
            <td>
                <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button" />
                <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp <span id="pagingtext">of</span>
                <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>

            </td>
        </tr>


    </table>
    <p>
        <br />
        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue" value="Back to Admin Page" />
    </p>


</asp:Content>





