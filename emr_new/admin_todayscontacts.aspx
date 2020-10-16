<%@ Page Language="C#" AutoEventWireup="true" CodeFile="admin_todayscontacts.aspx.cs" MasterPageFile="~/Site.master" Inherits="admin_todayscontacts" Title="Today's Patient's List " %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">
        $("document").ready(function () {
            // Bind grid initially with page number as 1    
            BindGrid(1);
        });
        var count = 0;
        var currentPageNumber = 1;
        var PAGE_SIZE = '<%= PAGE_SIZE %>';
        function BindGrid(data) {
            var postData = new Object();
            postData.PageIndex = data;
            postData.PageSize = PAGE_SIZE;
            $.ajax({
                type: "POST",
                url: "admin_todayscontacts.aspx/BindList",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#tblListPage").find("tr:gt(1)").remove();
                    if (listObj != null) {
                        for (var i = listObj.length - 1 ; i >= 0; i--) {
                            $("#tblListPage tr:eq(1)").after("<tr><td><a href='Sat_Survey_Entry.asp?patientid=" + listObj[i].PatientID + "'>" + listObj[i].LastName + " " + listObj[i].FirstName + "</a></td>" +
                               "</tr>");
                            count = Number(listObj[i].RecordCount);
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
                        $("#tdNoRecord").hide();
                        return;
                    }
                    else if (totalPages == 0) {

                        $("#MainContent_lblCurrentPage").hide();
                        $("#MainContent_lblTotalPages").hide();
                        $("#tdNoRecord").show();
                        return;
                    }
                    else {

                        $("#tdButton").show();
                        $("#MainContent_lblCurrentPage").show();
                        $("#MainContent_lblTotalPages").show();
                       
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
                    else {
                        window.location.href = location.href;
                        window.location.replace("error.aspx");
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
    <asp:HiddenField ID="totalcount" runat="server" />

    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border" id="tblListPage">
        <tr bgcolor="#D6B781">
            <td colspan="3"><strong>Today's Patient's List </strong></td>
        </tr>
        <tr>
            <td width="169" id="tdPatientName"><strong>Patient Name</strong></td>
            <td width="169" style="color: red ;display:none;" id="tdNoRecord" align="center"><strong>Sorry, no record found!</strong></td>

        </tr>
    </table>
    <table id="tdButton" style="display:none;">
        <tr>
            <td>
                <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button" />
                <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button" />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px">&nbsp;</asp:Label><asp:Label ID="lblOf" runat="server" class="PageTitle" Text=" of"></asp:Label>
                &nbsp;<asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>
            </td>
        </tr>

    </table>
    <p>
        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue" value="Back to Admin Page" />
    </p>
</asp:Content>
