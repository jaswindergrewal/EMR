<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="admin_stafflogin_list.aspx.cs" Inherits="admin_stafflogin_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    
    <script type="text/javascript">
        $("document").ready(function () {
        // Bind grid initially with page number as 1    
            BindGrid(1);
        });
        var count = 0;
        var currentPageNumber = 1;
        var PAGE_SIZE = 30;
        function BindGrid(data) {
            var postData = new Object();
            postData.PageIndex = data;
            $.ajax({
                type: "POST",
                url: "admin_stafflogin_list.aspx/BindStaffList",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    var IsActive = "Active";
                    $("#StaffList").find("tr:gt(1)").remove();
                    for (var i = listObj.length - 1; i >= 0; i--) {
                        if (listObj[i].Active_YN == 1)
                        {
                            IsActive = "Active";
                        }
                        else {
                            IsActive = "In Active";
                        }
                        $("#StaffList tr:eq(1)").after("<tr><td><a href='admin_stafflogin_edit.aspx?EmployeeID=" + listObj[i].EmployeeID + "'>" + listObj[i].EmployeeName + "</a></td>" +
                            "<td>" + listObj[i].username + "</td>" +
                            "<td>" + listObj[i].access_level + "</td>" +
                            "<td>" + IsActive + "</td></tr > ");
                        count = Number(listObj[i].RecordCount);
                    }
                    
                    //For setting current page number and total page number
                    currentPageNumber = data;
                    var totalPages = Math.ceil(count / PAGE_SIZE);
                    $("#MainContent_lblCurrentPage").text(currentPageNumber);
                    $("#MainContent_lblTotalPages").text(totalPages);

                    //Enable or disable the previous or next button.
                    if (totalPages == 1) {
                        $("#Previous").hide();
                        $("#Next").hide();
                        $("#MainContent_lblCurrentPage").hide();
                        $("#MainContent_lblTotalPages").hide();
                        $("#pagingtext").hide();
                        return;
                    }
                    else {
                        $("#Previous").show();
                        $("#Next").show();
                        $("#MainContent_lblCurrentPage").show();
                        $("#MainContent_lblTotalPages").show();
                        $("#pagingtext").show();
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
    <asp:HiddenField ID="totalcount" runat="server" />
    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border" id="StaffList">
        <tr bgcolor="#D6B781">
            <td colspan="4"><strong>Logins List </strong></td>
        </tr>
        <tr>
            <td width="169"><strong>Employee Name </strong></td>
            <td width="349"><strong>Username</strong></td>
            <td><strong>Access Level </strong></td>
             <td><strong>Is Active </strong></td>
        </tr>     
    </table>
       <table id="ProcessInfo">
            <tr>
                <td>
                    <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button"/>
                    <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp of &nbsp
                        <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>
                </td>
            </tr>

        </table>
    <p>
        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue" value="Back to Admin Page"/>
    </p>
</asp:Content>