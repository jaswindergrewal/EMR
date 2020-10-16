<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
    CodeFile="Ticket.aspx.cs" Inherits="Ticket" EnableEventValidation="false" %>

<%@ Register TagName="TicketInfo" TagPrefix="Longevity" Src="~/controls/TicketInfo.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="obout_Editor" Namespace="OboutInc.Editor" TagPrefix="obout" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
    <script type="text/javascript">
        //Function added by jaswinder to validate dates 

        function validateData() {
            var editorObject = $find("<%= edTicket.ClientID %>");
            var _content = editorObject.get_content();
            if (_content == '') {
                alert('Please enter the content');
                return false;
            }
            else {
                bool = true;
            }
            return bool;
        }

        function BindAssignDropDown(type) {
            var URL = "";
            $('#<%=ddlAssign.ClientID %>').removeClass("dropDownAddFousClass");
            $('#<%=ddlAssign.ClientID %>').addClass("dropDownRemoveFousClass");

            if (type == "Employee") {
                URL = "Ticket.aspx/BindEmployee";
            }
            else if (type == "Department") {
                URL = "Ticket.aspx/BindDepartment";
            }

            $.ajax
                ({
                    type: "POST",
                    url: URL,
                    data: "{}",
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (response) {
                        var Dropdown = $('#<%=ddlAssign.ClientID %>');
                        Dropdown.empty();
                        $.each(response.d, function (index, item) {
                            if (type == "Employee") {
                                Dropdown.append($("<option></option>").val(item.EmployeeID).html(item.EmployeeName));

                            }
                            else if (type == "Department") {
                                Dropdown.append($("<option></option>").val(item.DepartmentID).html(item.DepartmentName));

                            }
                        });
                    },
                    error: function () {
                        alert("Failed to load data");
                    }
                });
                }


                $("document").ready(function () {


                   /* $('#MainContent_TicketContainer_ClosePanel_btnOkClose').click(function () {
                        $("#loading-div-background").show();

                        debugger;


                        var _content = "";
                        if ($('#MainContent_TicketContainer_ClosePanel_btnOkClose').attr("value") == "Close") {*/
                           // var editorObject = $find("<%= edClose.ClientID %>");
                   // _content = editorObject.get_content();
                }
                   //var PatientID = "<%= PatientID %>";
                   //var TicketID = "<%= TicketID %>";
                   //var UserName = "<%= UserName %>";
                   //var StaffID = "<%= StaffID %>";

                   /*var postData = new Object();
                   postData.TicketID = TicketID;
                   postData.PatientID = PatientID;
                   postData.Content = _content;
                   postData.UserName = UserName;
                   postData.StaffID = StaffID;

                   $.ajax
                       ({
                           type: "POST",
                           url: "Ticket.aspx/CloseRepoenTicket",
                           data: JSON.stringify(postData),
                           contentType: "application/json; charset=utf-8",
                           dataType: "json",
                           cache: false,
                           success: function (response) {
                               if (response.d == true) {
                                   window.parent.location.reload();


                                   setTimeout("$('[id$=loadingdivbackground]').hide();", 1500);


                               }
                               else {

                                   setTimeout("$('[id$=loadingdivbackground]').hide();", 1500);
                                   alert("Failed to load data");
                               }
                           },
                           error: function () {
                               alert("Failed to load data");
                           }
                       });


               });*/




        });



    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">


    <Longevity:TicketInfo ID="Info" runat="server" />
    <br />
    <cc1:TabContainer ID="TicketContainer" runat="server" CssClass="lmc_tab" >
        <cc1:TabPanel ID="Details" runat="server" HeaderText="Details">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                    <tr bgcolor="#D6B781" class="regText">
                        <td>
                            <b>Ticket Details </b>
                        </td>
                        <td>
                            <div align="right">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdTicketOnly" runat="server" AutoGenerateColumns="false" EmptyDataText="No Details"
                                Width="100%"  GridLines="None" CssClass="regText"
                                CellPadding="6" CellSpacing="6" HeaderStyle-CssClass="border" RowStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField DataField="ContactDateEntered" HeaderText="Date" DataFormatString="{0:g}" />
                                    <asp:BoundField DataField="MessageBody" HeaderText="Message" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>

        <cc1:TabPanel ID="NewNote" runat="server" HeaderText="New Note">
            <ContentTemplate>
                <asp:Button ID="btnNew" runat="server" Text="Submit new note" CssClass="button" OnClientClick="return validateData();" OnClick="btnNew_Click" /><br />
                 <obout:Editor PathPrefix="Editor_data/" ID="edTicket" runat="server" Appearance="custom"
		ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14pt" Submit="false"
		AjaxWait="true" Height="300" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="false">
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
			<obout:method name="Preview" />
			<obout:HorizontalSeparator />
			<obout:Method Name="IncreaseHeight" />
			<obout:Method Name="DecreaseHeight" />
			<obout:TextIndicator />
		</Buttons>
	</obout:Editor>
                <br />
                <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                    <tr bgcolor="#D6B781" class="regText">
                        <td>
                            <b>Ticket Details </b>
                        </td>
                        <td>
                            <div align="right">
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:GridView ID="grdNoteDetails" runat="server" AutoGenerateColumns="false" EmptyDataText="No Details"
                                Width="100%" GridLines="None" CssClass="regText"
                                CellPadding="6" CellSpacing="6" HeaderStyle-CssClass="border" RowStyle-VerticalAlign="Top"
                                HeaderStyle-HorizontalAlign="Left">
                                <Columns>
                                    <asp:BoundField DataField="ContactDateEntered" HeaderText="Date" DataFormatString="{0:g}" />
                                    <asp:BoundField DataField="MessageBody" HeaderText="Message" />
                                </Columns>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="CustomInfo" runat="server" Visible="false" HeaderText="Test">
            <ContentTemplate>
                <iframe id="ifrCustom" runat="server" width="100%" height="2000px" style="background-image: images/export/beige_back.gif; border-style: none;"
                    frameborder="0" />
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="AssignEditClose" runat="server" HeaderText="Assign/Edit">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                    <tr bgcolor="#D6B781" class="regText">
                        <td colspan="3">
                            <b>Assign/Edit </b>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" nowrap="nowrap">
                            <asp:Label ID="lblAssign" runat="server" Text="Assign to " CssClass="regText"></asp:Label>
                        </td>
                        <td align="left" valign="top">
                            <asp:DropDownList ID="ddlAssign" runat="server" CssClass="FormField" />
                            <br />
                            <asp:RadioButtonList ID="rdoDept" runat="server"
                                RepeatDirection="Horizontal" AutoPostBack="true" OnSelectedIndexChanged="rdoDept_SelectedIndexChanged">
                                <asp:ListItem  Text="Employee" Value="Employee" Selected="True" />
                                <asp:ListItem  Text="Department" Value="Dept" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="50%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 52px; border: 1 solid black;">
                            <asp:Label ID="lblAttachPatient" runat="server" Text="Attach to patient" CssClass="regText" />
                        </td>
                        <td align="left">This will assign the ticket to a different patient.<br />
                            Type in part of a name and get results in the same way as in patient search.<br />
                            <asp:TextBox runat="server" ID="txtPatient" CausesValidation="false" TabIndex="0"
                                AutoPostBack="true" OnTextChanged="txtPatient_TextChanged" Width="273px" Height="10px"
                                CssClass="regText" onkeydown="return (event.keyCode!=13);" />
                            <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="3"
                                ServiceMethod="GetPatientList" ServicePath="~/PatientNamesServ.asmx" TargetControlID="txtPatient" />
                        </td>
                        <td width="50%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 52px; border: 1 solid black;">
                            <asp:Label ID="Label1" runat="server" Text="Convert to Staff Ticket" CssClass="regText" />
                        </td>
                        <td align="left">
                            <asp:CheckBox ID="cboDetach" runat="server" Text="" />
                        </td>
                        <td width="50%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left" style="width: 52px; border: 1 solid black;">
                            <asp:Label ID="lblSeverity" runat="server" Text="Severity" CssClass="regText" />
                        </td>
                        <td align="left">
                            <asp:RadioButtonList ID="rdoSeverity" runat="server" RepeatDirection="Horizontal"
                                CssClass="regText">
                                <asp:ListItem Text="Severe" Value="1" />
                                <asp:ListItem Text="Normal" Selected="True" Value="2" />
                                <asp:ListItem Text="Low" Value="3" />
                            </asp:RadioButtonList>
                        </td>
                        <td width="50%">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDueDate" runat="server" Text="Due Date" CssClass="regText" />
                        </td>
                        <td>
                            <asp:TextBox ID="txtDueDate" runat="server" Columns="10" />
                            <cc1:CalendarExtender ID="calExtDueDate" runat="server" TargetControlID="txtDueDate" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnOkEdit" Text="Done" CssClass="button" runat="server" OnClick="btnOkEdit_Click" />
                            <asp:Button ID="btnCancelEdit" Text="Cancel" CssClass="button" runat="server" OnClick="CancelChange" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
        <cc1:TabPanel ID="ClosePanel" runat="server" HeaderText="Close">
            <ContentTemplate>
                <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                    <tr bgcolor="#D6B781" class="regText">
                        <td colspan="3">
                            <b>Close or ReOpnen </b>
                        </td>
                        <tr>
                            <td colspan="3" align="left">
                                <div id="pnlClose" runat="server">
                                    <asp:Label ID="lblCloseNote" runat="server" CssClass="PageTitle" Text="Closing Note" />
                                    <br />
                                    <b>Note: This note is ONLY used for a note related to closing the ticket. If you do
												not close the ticket, this note will NOT be saved.
												<br />
                                        You can write notes at the New Note tab.</b>
                                     <obout:Editor PathPrefix="Editor_data/" ID="edClose" runat="server" Appearance="custom"
		ShowQuickFormat="false" DefaultFontFamily="Times New Roman" DefaultFontSize="14pt" Submit="false"
		AjaxWait="true" Height="300" Width="600" SpellCheckAutoComplete="true" InitialCleanUp="false">
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
			<obout:method name="Preview" />
			<obout:HorizontalSeparator />
			<obout:Method Name="IncreaseHeight" />
			<obout:Method Name="DecreaseHeight" />
			<obout:TextIndicator />
		</Buttons>
	</obout:Editor>
                                </div>
                            </td>
                        </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Button ID="btnOkClose" Text="Close" CssClass="button" runat="server"  OnClick="btnOkClose_Click"/>
                            <asp:Button ID="btnCancelClose" Text="Cancel" CssClass="button" runat="server" OnClick="CancelChange" />
                        </td>
                    </tr>
                </table>
            </ContentTemplate>
        </cc1:TabPanel>
    </cc1:TabContainer>


    <div id="loading-div-background">
        <div id="loading-div" class="ui-corner-all">
            <img src="images/indicator.gif" alt="Loading.." />
            <h4 style="color: gray; font-weight: normal;">Please wait....</h4>
        </div>
    </div>
</asp:Content>
