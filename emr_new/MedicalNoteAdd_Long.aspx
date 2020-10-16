<%@ Page Title="Appointment - Medical Note Entry " Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="MedicalNoteAdd_Long.aspx.cs" Inherits="MedicalNoteAdd_Long" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <link href="css/bootstrap.min.css"
        rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="http://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.0.3/js/bootstrap.min.js"></script>
    <link href="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/css/bootstrap-multiselect.css" rel="stylesheet" type="text/css" />
    <script src="http://cdn.rawgit.com/davidstutz/bootstrap-multiselect/master/dist/js/bootstrap-multiselect.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $('[id*=lstAptDates]').multiselect({
                includeSelectAllOption: true
            });
            $("#spnlstAptDate").hide();
        });
    </script>
   
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <script language="javascript" type="text/JavaScript">
        var postData = new Object();
        postData.PatientID ='<%=PatientID%>';
        postData.ApptID = '<%=ApptID%>';

        function putAllToEditor() {
            $("#loading-div-background").show();
            postData.StaffID ='<%=StaffID%>';
            postData.Name ='<%=Name%>';
            postData.DOB ='<%=DOB%>';
            $("#spnlstAptDate").hide();
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/allstringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });

        }

        function putHeadToEditor() {
            $("#loading-div-background").show();
            $("#spnlstAptDate").hide();
            postData.StaffID ='<%=StaffID%>';
            postData.Name ='<%=Name%>';
            postData.DOB ='<%=DOB%>';
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/hstringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    debugger;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });
        }

        function putSubToEditor() {
            $("#loading-div-background").show();
            $("#spnlstAptDate").hide();
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/sstringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });

        }

        function LabDrawDates() {
           
            $("#loading-div-background").show();
            var LabDrawDates = [];
            var x = document.getElementById("lstAptDates");
            for (var i = 0; i < x.options.length; i++) {
                if (x.options[i].selected) {
                    LabDrawDates.push(x.options[i].value); 
                }
            }
            postData.LabDrawDate = LabDrawDates;
            
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/LabDrawDatesstringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                    $("#spnlstAptDate").hide();
                    ;

                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });
        }

       
        
        function putObjToEditor() {
            //$("#loading-div-background").show();
            $("#spnlstAptDate").show();
            //$.ajax({
            //    type: "POST",
            //    url: "MedicalNoteAdd_Long.aspx/ostringbuilder",
            //    data: JSON.stringify(postData),
            //    dataType: "json",
            //    contentType: "application/json",
            //    success: function (response) {
            //        var res = response.d;
            //        oboutGetEditor('ed').InsertHTML(res);
            //        $("#loading-div-background").hide();
            //        $("#spnlstAptDate").show();
            //        ;

            //    },
            //    error: function (obj) {

            //        alert(obj.responseText);
            //    }
            //});

        }

        function putAssToEditor() {
            $("#loading-div-background").show();
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/astringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                    $("#spnlstAptDate").hide();
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });

        }

        function putPlanToEditor() {

            $("#loading-div-background").show();
            $("#spnlstAptDate").hide();
            $.ajax({
                type: "POST",
                url: "MedicalNoteAdd_Long.aspx/pstringbuilder",
                data: JSON.stringify(postData),
                dataType: "json",
                contentType: "application/json",
                success: function (response) {
                    var res = response.d;
                    oboutGetEditor('ed').InsertHTML(res);
                    $("#loading-div-background").hide();
                },
                error: function (obj) {

                    alert(obj.responseText);
                }
            });
        }

        //Function to check for blank editor       
        function checkBlankContent() {

            var editorObject = oboutGetEditor("ed");


            if (editorObject.getContent.length < 1) {

                alert('Please enter the content');
                return false;

            }
        }
    </script>
   
    <asp:HiddenField ID="hdnLabRecord" runat="server" ClientIDMode="Static" />
    <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
        <tr bgcolor="#D6B781" class="regText">
            <td bgcolor="#D6B781">
                <span class="style1"><strong>Appointment - Medical Note Entry </strong></span>
            </td>
            <td bgcolor="#D6B781">
                <div align="right">
                    <input name="Button" type="button" class="button" onclick="MM_goToURL('parent', 'manage.aspx?patientid=<%=PatientID%>    ');return document.MM_returnValue"
                        value="Back to Patient Details" />
                </div>
            </td>
        </tr>
        <tr bgcolor="#D6B781" class="regText">
            <td width="81%" bgcolor="#D6B781">
                <b>Patient Name:</b>
                <asp:Label ID="lblPatientName" runat="server" />
            </td>
            <td width="19%" bgcolor="#D6B781">
                <div align="right">
                    <input name="Button" type="button" class="button" onclick="MM_goToURL('parent', 'apt_console.aspx?aptid=<%=ApptID%>    ');return document.MM_returnValue"
                        value="Back to Apt Console" />
                </div>
            </td>
        </tr>
    </table>
    <br />
    <table width="800" border="0" cellpadding="6" cellspacing="0">
        <tr>
            <td>
                <input type="button" id="Allbtn" class="button"
                    onclick="putAllToEditor();"
                    style="position: relative;  width: 60px"
                    value="All" /></td>
            <td>
                <input type="button" id="Headbtn" class="button"
                    onclick="putHeadToEditor();"
                    style="position: relative;  width: 60px"
                    value="Header" /></td>
            <td>
                <input type="button" id="SubjectiveBtn" class="button"
                    onclick="putSubToEditor();"
                    style="position: relative;  width: 60px"
                    value="Sub" /></td>
            <td>
                <input id="Objectivebtn" class="button"
                    type="button"
                    onclick="putObjToEditor();"
                    style="position: relative;  width: 60px"
                    value="Obj" /></td>
            <td>
                <input id="AssesmentBtn" class="button"
                    type="button"
                    onclick="putAssToEditor();"
                    style="position: relative;  width: 60px"
                    value="Assess" /></td>
            <td>
                <input id="PlanBtn" class="button"
                    type="button"
                    onclick="putPlanToEditor();"
                    style="position: relative; width: 60px"
                    value="Plan" /></td>
            
            <td><span id="spnlstAptDate"><b>Labs Draw Dates</b><br />
                <asp:ListBox ID="lstAptDates" runat="server"  ClientIDMode="Static" SelectionMode="Multiple" Style="position: relative;  top: 2px; left: 298px; width: 100px"></asp:ListBox>
           &nbsp;&nbsp;
                 <input id="testBtn" class="button"
                    type="button"
                    onclick="LabDrawDates();"
                    style="position: relative; width: 60px"
                    value="Done" />
               </span> </td>
        </tr>
    </table>

    <br />
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
        CssClass="button" OnClientClick="return checkBlankContent();" />
    <obout:Editor PathPrefix="Editor_data/" ID="ed" runat="server" Appearance="custom"
        ShowQuickFormat="false" Submit="false"
        AjaxWait="true" Height="600" Width="800" SpellCheckAutoComplete="true" DefaultStylesInHtml="false" >
        <Buttons>
            <obout:Toggle Name="Bold" />
            <obout:Toggle Name="Italic" />
            <obout:Toggle Name="Underline" />
            <obout:Toggle Name="StrikeThrough" />
            <obout:HorizontalSeparator />
            <obout:Method Name="ClearStyles" />
            <obout:HorizontalSeparator />
            <obout:Select Name="FontName" />
            <obout:Select Name="FontSize" />
            <obout:HorizontalSeparator />
            <obout:VerticalSeparator />
            <obout:Method Name="Undo" />
            <obout:Method Name="Redo" />
            <obout:HorizontalSeparator />
            <obout:Method Name="PasteWord" />
            <obout:HorizontalSeparator />
            <obout:Method Name="JustifyLeft" />
            <obout:Method Name="JustifyCenter" />
            <obout:Method Name="JustifyRight" />
            <obout:Method Name="JustifyFull" />
            <obout:HorizontalSeparator />
            <obout:Method Name="SpellCheck" />
            <obout:Method Name="Preview" />
            <obout:HorizontalSeparator />
            <obout:Method Name="IncreaseHeight" />
            <obout:Method Name="DecreaseHeight" />
            <obout:TextIndicator />
        </Buttons>
    </obout:Editor>


    <div id="loading-div-background">
        <div id="loading-div" class="ui-corner-all">
            <img src="images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>
     
</asp:Content>
