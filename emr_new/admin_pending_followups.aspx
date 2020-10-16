<%@ Page Language="C#" Title="Pending Followups" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_pending_followups.aspx.cs" Inherits="admin_pending_followups" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">
        var Clinic;
        var currentPageNumber = 1;
        var PAGE_SIZE = 30;

        //Added by jaswinder on 13 aug 2013  to bind the grid on submit button click
        function OnSubmitButtonClick() {
            var Clinic = ($('#<%= ddlClinic.ClientID %> option:selected').text())
            if (Clinic == "Select Clinic") {
                alert('Please select the clinic');
                return false;
            }
            else {
                BindGrid(1);
            }
        }

        //Added by jaswinder on 13 aug 2013  to bind the grid
        function BindGrid(data) {
            var Clinic = ($('#<%= ddlClinic.ClientID %> option:selected').val())
            var OrderBy = ($('#<%= ddlOrderBy.ClientID %> option:selected').val())
            var postData = new Object();
            postData.Clinic = Clinic;
            postData.OrderBy = OrderBy;
            var date = new Date();

            postData.PageIndex = data;
            $.ajax({
                type: "POST",
                url: "admin_pending_followups.aspx/BindAptGrid",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#tblClinicList").find("tr:gt(2)").remove();
                    var count = 0;
                    var rangeDate;
                    if (listObj.length > 0) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            if (listObj[i].RangeDate == null) {
                                rangeDate = null;
                            }
                            else rangeDate = convertDate(Date(listObj[i].RangeDate));
                            $("#tblClinicList tr:eq(2)").after("<tr><td><a href='admin_contact_add_pendingfollowups.aspx?FollowUp_ID=" + listObj[i].followup_id + "&patientid=" + listObj[i].patientid + "'>" + listObj[i].Date_Entered + "</a></td>" +
                                "<td><a href='admin_contact_add_pendingfollowups.aspx?FollowUp_ID=" + listObj[i].followup_id + "&patientid=" + listObj[i].patientid + "'>" + listObj[i].RangeDate + "</a></td>" +
                                "<td>" + listObj[i].followup_type_desc + "</td>" +
                                "<td>" + listObj[i].Clinic + "</td>" +
                                "<td>" + listObj[i].EmployeeName + "</td>" +
                                "<td>" + listObj[i].LastName + " " + listObj[i].FirstName + "</td>" +
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

        //Function to format the date in mm/dd/yyyy 
        function convertDate(inputFormat) {
            var d = new Date(inputFormat);
            return [d.getMonth() + 1, d.getDate(), d.getFullYear()].join('/');
        }
    </script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">Pending Follow Up Requests</p>

    <asp:HiddenField ID="totalcount" runat="server" />
    <strong>Clinic :</strong> <asp:DropDownList ID="ddlClinic" runat="server" CssClass="FormFieldWhite"></asp:DropDownList>
    
    <%--<asp:DropDownList ID="ddlClinic" runat="server" Width="150px">
        <asp:ListItem Selected="True" Value="" Text="Select Clinic" />
        <asp:ListItem Value="Kirkland" Text="Kirkland" />
        <asp:ListItem Value="Lynnwood" Text="Lynnwood" />
        <asp:ListItem Value="South" Text="Tacoma" />
    </asp:DropDownList>--%>
    <strong>Order By :</strong><asp:DropDownList ID="ddlOrderBy" runat="server" Width="100px">
        <asp:ListItem Selected="True" Value="A" Text="ASC" />
        <asp:ListItem Value="D" Text="DESC" />

    </asp:DropDownList>
    <input type="button" value="Submit" onclick="return OnSubmitButtonClick();" class="button" />
    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="tblClinicList">
        <tr bgcolor="#D6B781">
            <td colspan="6"><strong>List of Pending Follow Up Requests </strong></td>
        </tr>
        <tr>
            <td width='15%'><strong>Appointment Date </strong></td>
            <td width='20%'><strong>Date Range for Follow Up</strong></td>
            <td width='20%'><strong>Follow Type</strong></td>
            <td width='10%'><strong>Clinic</strong></td>
            <td width='15%'><strong>Entered By</strong></td>
            <td width='20%'><strong>Patient Name</strong></td>

        </tr>
        <tr>
            <td colspan='6' style='color: red; display: none;' id='tdNoRecord' align='center' visible="false"><strong>No record found !</strong></td>
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
