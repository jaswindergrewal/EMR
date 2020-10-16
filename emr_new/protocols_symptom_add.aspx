<%@ Page Language="C#" Title="Add Symptom" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="protocols_symptom_add.aspx.cs" Inherits="protocols_symptom_add" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">

        $("document").ready(function () {

            BindGrid(1);
        });

        var count = 0;
        var currentPageNumber = 1;

        //function to insert symptom details
        function InsertSymptoms() {
            var SymptomText = $("#txtsymptom").val();
            if (SymptomText == "") {
                alert("Symptom name can' be blank");
                return false;
            }
            var postData = new Object();
            postData.SymptomText = SymptomText;
            $.ajax({
                type: "POST",
                url: "protocols_symptom_add.aspx/InsertSymptoms",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    if (res == 1) {
                        alert("Record inserted");
                        BindGrid(1);
                    }
                    else {
                        alert("Duplicate data can't be inserted");
                    }
                }

            });
            return false;
        }

        //bind the symptom data
        function BindGrid(data) {

            var postData = new Object();
            postData.PageIndex = data;
            var PageSize = 30;
            $.ajax({
                type: "POST",
                url: "protocols_symptom_add.aspx/BindSymptomList",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#SymptomList").find("tr:gt(0)").remove();
                    for (var i = listObj.length - 1 ; i >= 0; i--) {

                        $("#SymptomList tr:eq(0)").after("<tr><td>"+ listObj[i].SymptomName + "</td></tr>");
                        count = Number(listObj[i].RecordCount);
                    }

                    currentPageNumber = data;
                    var totalPages = Math.ceil(count / PageSize);
                    $("#MainContent_lblCurrentPage").text(currentPageNumber);
                    $("#MainContent_lblTotalPages").text(totalPages);

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
   
   
        <table width="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td>
                    <p class="PageTitle">Add Symptom</p>
                    <input name="symptom" type="text" class="FormField" id="txtsymptom" size="40">
                    <input name="Submit" type="submit" class="button" value="Submit" onclick="return InsertSymptoms();">
                    <input name="Button" type="button" class="button" onClick="MM_goToURL('parent', 'protocols_protocol_list.aspx'); return document.MM_returnValue" value="Back">
                    
                    <br>
                    <br>
                    
                </td>
            </tr>
        </table>
         <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="SymptomList">
        
        <tr>
        <td><strong>Symptom Name</strong></td>
     
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
                    <asp:Label ID="lblCurrentPage" runat="server" class="PageTitle" Style="margin-left: 30px"></asp:Label>&nbsp <span id="pagingtext">of</span>
                        <asp:Label ID="lblTotalPages" runat="server" class="PageTitle"></asp:Label>
                </td>
            </tr>

        </table>

</asp:Content>
