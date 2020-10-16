<%@ Page Language="C#" Title="Add Diagnosis" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="protocols_diagnosis_add.aspx.cs" Inherits="protocols_diagnosis_add" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">

        $("document").ready(function () {

            BindGrid(1);
        });

        var count = 0;
        var currentPageNumber = 1;

        //function to insert Diagnosis details
        function InsertDiagnosis() {
            var DiagnosisText = $("#txtDiagnosis").val();

            if (DiagnosisText == "") {
                alert("Diagnosis name can' be blank");
                return false;
            }
           
            var postData = new Object();
            postData.DiagnosisText = DiagnosisText;
            $.ajax({
                type: "POST",
                url: "protocols_diagnosis_add.aspx/InsertDiagnosis",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    if (res == "1") {
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

        //Function bind the diagnosis list
        function BindGrid(data) {

            var postData = new Object();
            postData.PageIndex = data;
            var PageSize = 30;
            $.ajax({
                type: "POST",
                url: "protocols_diagnosis_add.aspx/BindDiagnosis",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#DiagnosisList").find("tr:gt(0)").remove();
                    for (var i = listObj.length - 1 ; i >= 0; i--) {

                        $("#DiagnosisList tr:eq(0)").after("<tr><td>" + listObj[i].Diag_Title + "</td></tr>");
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
                    <p class="PageTitle">Add Diagnosis</p>
                    <input name="txtDiagnosis" type="text" class="FormField" id="txtDiagnosis" size="40">
                    <input name="Submit" type="submit" class="button" value="Submit" onclick="return InsertDiagnosis();">
                    <input name="Button" type="button" class="button" onClick="MM_goToURL('parent', 'protocols_protocol_list.aspx'); return document.MM_returnValue" value="Back">
                    
                    <br>
                    <br>
                    
                </td>
            </tr>
        </table>
         <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border" id="DiagnosisList">
        
        <tr>
        <td><strong>Diagnosis Name</strong></td>
     
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
