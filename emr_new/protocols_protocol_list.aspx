<%@ Page Title="Pending Auto Ship Requests" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="protocols_protocol_list.aspx.cs" Inherits="protocols_protocol_list" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" Runat="Server">
  
    <script type="text/javascript">
        $("document").ready(function () {

            BindGrid(1);
        });
        var count = 0;
        var currentPageNumber = 1;

        var date = new Date();

        //Bind the Protocol list
        function BindGrid(data) {

            var postData = new Object();
            postData.PageIndex = data;
            var PageSize = 30;
            $.ajax({
                type: "POST",
                url: "protocols_protocol_list.aspx/BindProtocolList",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                   
                    var listObj = response.d;
                    if (listObj != null) {
                        $("#StaffProtocolList").find("tr:gt(1)").remove();
                        for (var i = listObj.length - 1 ; i >= 0; i--) {

                            $("#StaffProtocolList tr:eq(1)").after("<tr><td><a href='protocols_protocol_details.aspx?protocol_id=" + listObj[i].Protocol_ID + "'>" + listObj[i].Protocol_Title + "</a></td>" +
                                "<td>" + convertDate(listObj[i].Date_Entered) + "</td>" +
                                "<td>" + listObj[i].EmployeeName + "</td></tr>");
                            count = Number(listObj[i].RecordCount);
                        }

                        currentPageNumber = data;
                        var totalPages = Math.ceil(count / PageSize);
                        $("#MainContent_lblCurrentPage").text(currentPageNumber);
                        $("#MainContent_lblTotalPages").text(totalPages);

                        if (totalPages == 1) {
                            $("#tdButton").hide();
                            $("#MainContent_lblCurrentPage").hide();
                            $("#MainContent_lblTotalPages").hide();
                            $("#pagingtext").hide();

                            return;
                        }
                        else {
                            $("#tdButton").show();
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
                }

            });
        }



        //Function to format the date in mm/dd/yyyy 
        function convertDate(inputFormat) {
            var d = new Date(inputFormat);
            return [d.getDate(), d.getMonth() + 1, d.getFullYear()].join('/');
        }

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
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" Runat="Server">
    
    
        Protocol List
        <a href="protocols_symptom_add.aspx">[Add Symptoms]</a>
        <a href="protocols_diagnosis_add.aspx">[Add Diagonosis]</a>
        <a href="protocols_protocol_add.aspx">[Add Protocols]</a>
    <br />
    <br />
       
   
    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border" id="StaffProtocolList">
        <tr bgcolor="#D6B781">
            <td colspan="3"><strong></strong></td>
        </tr>
        <tr>
        <td><strong>Title</strong></td>
      <td><strong>Date Written </strong></td>
      <td><strong>Written By </strong></td>
        </tr>     
    </table>
       <table id="tdButton" style="display:none;">
            <tr>
                <td>
                    <input type="button" value="Previous" id="Previous" onclick="ChangePage(this)" class="button"/>
                    <input type="button" value="Next" id="Next" onclick="ChangePage(this)" class="button"/>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp <span id="pagingtext">of</span>
                        <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>
                </td>
            </tr>

        </table>
    <br />
    <p>
        <input name="Submit" type="submit" class="button" onclick="MM_goToURL('parent', 'admin_main.aspx'); return document.MM_returnValue" value="Back to Admin Page"/>
    </p>
</asp:Content>

