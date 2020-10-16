<%@ Page Language="C#" MasterPageFile="~/Site.master" Title="Special Appointment List" AutoEventWireup="true" CodeFile="apt_99_special.aspx.cs" Inherits="apt_99_special" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">


        $("document").ready(function () {
            BindGrid(1);
        });

        var currentPageNumber = 1;
        var PAGE_SIZE = 30;
        function BindGrid(data) {
            var postData = new Object();
            var date = new Date();

            postData.PageIndex = data;
            $.ajax({
                type: "POST",
                url: "apt_99_special.aspx/BindAptGrid",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    //$("#tblClinicList").find("tr:gt(2)").remove();
                    var count = 0;
                    if (listObj.length > 0) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#tblClinicList tr:eq(1)").after("<tr><td> " + listObj[i].DateEntered + "</td>" +
                                "<td>" + listObj[i].LastName + " " + listObj[i].FirstName + "</td>" +
                                "<td>" + listObj[i].shippingstreet + "</td>" +
                                "<td>" + listObj[i].shippingcity + "</td>" +
                                "<td>" + listObj[i].shippingzip + "</td>" +
                                "<td>" + listObj[i].billingstreet + "</td>" +
                                "<td>" + listObj[i].billingcity + "</td>" +
                                "<td>" + listObj[i].billingstate + "</td>" +
                                "<td>" + listObj[i].billingzip + "</td>" +
                                "<td>" + listObj[i].workPhone + "</td>" +
                                //"<td>" + listObj[i].shippingzip +  "</td>" +
                                "<td>" + listObj[i].cellphone + "</td>" +
                                "<td>" + listObj[i].homephone + "</td>" +
                                "<td>" + listObj[i].email + "</td>" +
                                //"<td>" + listObj[i].patientid + "</td>" +
                                "<td>" + listObj[i].AptStart + "</td>" +
                                "<td>" + listObj[i].notes + "</td>" +
                                "<td>" + listObj[i].typename + "</td>" +
                                "<td>" + listObj[i].sourcetypedesc + "</td>" +
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

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <p class="PageTitle">Special Appointment List</p>

    <asp:HiddenField ID="totalcount" runat="server" />


    <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="tblClinicList">

        <tr bgcolor="#D6B781">
            <td><strong>Created Date</strong></td>
            <td><strong>Patient Name</strong></td>
            <td><strong>shippingstreet</strong></td>
            <td><strong>shippingcity</strong></td>
            <td><strong>shippingzip</strong></td>
            <td><strong>billingstreet</strong></td>
            <td><strong>billingcity</strong></td>
            <td><strong>billingstate</strong></td>
            <td><strong>billingzip</strong></td>
            <td><strong>workPhone</strong></td>
            <td><strong>cellphone</strong></td>
            <td><strong>homephone</strong></td>
            <td><strong>email</strong></td>
            <td><strong>Apt Entry Date</strong></td>
            <td><strong>notes</strong></td>
            <td><strong>typename</strong></td>
            <td><strong>sourcetypedesc</strong></td>
        </tr>
        <tr>
            <td colspan='21' style='color: red; display: none;' id='tdNoRecord' align='center' visible="false"><strong>No record found !</strong></td>
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



