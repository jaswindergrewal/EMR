<%@ Page Language="C#" AutoEventWireup="true" MasterPageFile="~/Site.master" CodeFile="protocols_protocol_details.aspx.cs" Inherits="protocols_protocol_details" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">

    <script type="text/javascript">
        var ProtocolId = '<%= ProtocolId %>';
        $("document").ready(function () {

            BindSymptomGrid();
            BindDiagnosisGrid();

        });

        //Function to insert the diagnosis data
        function InsertProtocolDiagnosis() {
            var DiagnosisText = $("[id*='drpDiagnosisList'] :selected").text();
            var DiagnosisId = $("[id*='drpDiagnosisList'] :selected").val();
            var postData = new Object();

            postData.ProtocolId = ProtocolId;
            postData.DiagnosisId = DiagnosisId;
            $.ajax({
                type: "POST",
                url: "protocols_protocol_details.aspx/InsertProtocolDiagnosis",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    if (res == "1") {
                        alert("Record inserted");
                        $("#divDiagnosis").append("<span class='regText'>" +
                           DiagnosisText + "</span>" +
                           "[<a href='#' onclick='DeleteDiagnosis(" + DiagnosisId + ")'>Remove</a>]<br>");

                    }
                    else {
                        alert("Duplicate data can't be inserted");
                    }

                }

            });
            return false;
        }

        //Function to delete diagnosis data
        function DeleteDiagnosis(DiagnosisId) {

            var message = confirm('Are you sure you want to delete this item?');
            if (message != 0) {

                var postData = new Object();
                postData.ProtocolId = ProtocolId;
                postData.DiagnosisId = DiagnosisId;
                $.ajax({
                    type: "POST",
                    url: "protocols_protocol_details.aspx/DeleteProtocolDiagnosis",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var res = response.d;
                        if (res == "1") {
                            alert("Record deleted successfully");
                            BindDiagnosisGrid();
                        }

                    }

                });
            }
            else {
                return false;
            }
        }

        //Bind Diagnosis grid
        function BindDiagnosisGrid() {
            var postData = new Object();
            postData.ProtocolId = ProtocolId;
            $.ajax({
                type: "POST",
                url: "protocols_protocol_details.aspx/BindDataForProtocolDiagnosis",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#divDiagnosis").html("");
                    for (var i = listObj.length - 1 ; i >= 0; i--) {

                        $("#divDiagnosis").append("<span class='regText'>" +
                            listObj[i].Diag_Title + "</span>" +
                            "[<a href='#' onclick='DeleteDiagnosis(" + listObj[i].Diagnosis_ID + ")'>Remove</a>]<br>");


                    }
                }

            });
        }


        //function to insert symptom details
        function InsertProtocolSymptoms() {
            var SymptomText = $("[id*='drpSymptomlist'] :selected").text();
            var SymptomId = $("[id*='drpSymptomlist'] :selected").val();
            var postData = new Object();
            postData.ProtocolId = ProtocolId;
            postData.SymptomId = SymptomId;
            $.ajax({
                type: "POST",
                url: "protocols_protocol_details.aspx/InsertProtocolSymptoms",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    if (res == "1") {
                        alert("Record inserted");
                        $("#divSymptom").append("<span class='regText'>" +
                           SymptomText + "</span>" +
                           "[<a href='#' onclick='DeleteSymptom(" + SymptomId + ")'>Remove</a>]<br>");
                    }
                    else {
                        alert("Duplicate data can't be inserted");
                    }
                }

            });
            return false;
        }

        //function delete symptom data
        function DeleteSymptom(SymptomId) {

            var message = confirm('Are you sure you want to delete this item?');
            if (message != 0) {
                var postData = new Object();
                postData.ProtocolId = ProtocolId;
                postData.SymptomId = SymptomId;
                $.ajax({
                    type: "POST",
                    url: "protocols_protocol_details.aspx/DeleteProtocolSymptoms",
                    data: JSON.stringify(postData),
                    dataType: "json",
                    contentType: "application/json",
                    success: function (response) {
                        var res = response.d;
                        if (res == "1") {
                            alert("Record deleted successfully");
                            BindSymptomGrid();
                        }
                    }

                });
            }
            else {
                return false;
            }
        }

        //Bind the symptom grid
        function BindSymptomGrid() {
            var postData = new Object();
            postData.ProtocolId = ProtocolId;
            $.ajax({
                type: "POST",
                url: "protocols_protocol_details.aspx/BindDataForProtocolSymptoms",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var listObj = response.d;
                    $("#divSymptom").html("");
                    for (var i = 0; listObj.length > i ; i++) {

                        $("#divSymptom").append("<span class='regText'>" +
                            listObj[i].SymptomName + "</span>" +
                            "[<a href='#' onclick='DeleteSymptom(" + listObj[i].SymptomID + ")'>Remove</a>]<br>");

                    }
                }

            });
        }

        //Delete the protocol details
        //Jaswinder 16th aug 2013
        //Instead of going to new page and the confirm whether you want to delete or do this functionality on the same page
        function DeleteProtocol() {
         

                var message = confirm('Are you sure you want to delete this item?');
                if (message != 0) {
                    var postData = new Object();
                    postData.ProtocolId = ProtocolId;
                    $.ajax({
                        type: "POST",
                        url: "protocols_protocol_details.aspx/DeleteProtocol",
                        data: JSON.stringify(postData),
                        dataType: "json",
                        contentType: "application/json",
                        success: function (response) {
                            var res = response.d;
                            if (res == "1") {
                                alert("Record deleted successfully");
                                window.location.href = location.href;
                                window.location.replace("protocols_protocol_list.aspx");
                            }
                        }

                    });
                }
                else {
                    return false;
                }
           
        }
        


    </script>



</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <div id="Layer3" style="position: absolute; width: 327px; z-index: 26; left: 703px; top: 161px; background-color: #CCCCCC; layer-background-color: #CCCCCC; border: 1px none #000000;display:table">
        <table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td>
                    <p><strong class="regText">Symptoms for this Protocol</strong></p>
                    <asp:DropDownList ID="drpSymptomlist" DataTextField="SymptomName" DataValueField="SymptomID" runat="server"></asp:DropDownList>
                    <input name="Submit" type="submit" class="button" value="Link to Symptom" onclick="return InsertProtocolSymptoms();">

                    <br>
                    <br>

                    <div id="divSymptom" ></div>

                </td>
                
            </tr>
            <tr>
                 <td style="border-top: 1px solid #000000">
                    <p><strong class="regText">Diagnosis for this Protocol</strong></p>
                    <asp:DropDownList ID="drpDiagnosisList" DataTextField="Diag_Title" DataValueField="Diagnosis_ID" runat="server"></asp:DropDownList>
                    <input name="Submit" type="submit" class="button" value="Link to Diagnosis" onclick="return InsertProtocolDiagnosis();">

                    <br>
                    <br>
                    <div id="divDiagnosis" style="display:table"></div>
                </td>


            </tr>
        </table>

    </div>

    <div id="Layer4" style="position: absolute; width: 673px; height: 229px; z-index: 27; left: 10px; top: 128px;">
        <p><span class="PageTitle">Protocol Details</span></p>
        <table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td height="34" valign="top"><span class="PageTitle"><%= lstProtocol.Protocol_Title %></span></td>
                <td valign="top">
                    <div align="right"><span class="PageTitle"><span class="regText">[<a href="protocols_protocol_list.aspx">Protocol Home</a>] [<a href="protocols_protocol_edit.aspx?protocol_id=<%= lstProtocol.Protocol_ID%>">Edit</a>] [<a href="#" onclick="DeleteProtocol();">Delete</a>] </span></span></div>
                </td>
            </tr>
            <tr>
                <td colspan="2" valign="top">
                    <p class="regText">
                        <strong>Written By</strong>: <%= lstProtocol.EnteredBy%><br>
                        <strong>Date Written</strong>: <%= lstProtocol.DateEntered%>
                    </p>
                    <p class="regText"><%= lstProtocol.Protocol_Desc%></p>
                </td>
            </tr>
        </table>
    </div>

    <%--<div id="Layer2" style="position: absolute; width: 327px; z-index: 26; left: 703px; background-color: #CCCCCC; layer-background-color: #CCCCCC; border: 1px none #000000;display:table">
        <table width="100%" height="100%" border="0" cellpadding="5" cellspacing="0" class="border">
            <tr>
                <td>
                    <p><strong class="regText">Diagnosis for this Protocol</strong></p>
                    <asp:DropDownList ID="drpDiagnosisList" DataTextField="Diag_Title" DataValueField="Diagnosis_ID" runat="server"></asp:DropDownList>
                    <input name="Submit" type="submit" class="button" value="Link to Diagnosis" onclick="return InsertProtocolDiagnosis();">

                    <br>
                    <br>
                    <div id="divDiagnosis" style="display:table"></div>
                </td>
            </tr>
        </table>
    </div>--%>
</asp:Content>
