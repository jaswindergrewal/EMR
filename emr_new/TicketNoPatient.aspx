<%@ Page Title="Staff Ticket" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="TicketNoPatient.aspx.cs" Inherits="TicketNoPatient" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Flan.Controls" Namespace="Flan.Controls" TagPrefix="flan" %>
<%@ Register TagName="TicketInfo" TagPrefix="Longevity" Src="~/controls/TicketInfo.ascx" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor" TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.ContextMenu"
    TagPrefix="obout" %>
<%@ Register Assembly="Obout.Ajax.UI" TagPrefix="obout" Namespace="Obout.Ajax.UI.HTMLEditor.ToolbarButton" %>
<%@ Register TagPrefix="custom" Namespace="CustomToolbarButton" %>
<%@ Register Assembly="Obout.Ajax.UI" Namespace="Obout.Ajax.UI.HTMLEditor.Popups"
    TagPrefix="obout" %>
<%@ Register Namespace="CustomPopups" TagPrefix="custom" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="pnlTicket" runat="server">
        <ContentTemplate>
            <cc1:TabContainer ID="TicketContainer" runat="server" CssClass="lmc_tab" Width="800px">
                <cc1:TabPanel ID="Details" runat="server" HeaderText="Details">
                    <ContentTemplate>
                        <Longevity:TicketInfo ID="Info" runat="server" />
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
                                    <asp:GridView ID="grdTicketOnly" runat="server" AutoGenerateColumns="False" EmptyDataText="No Details"
                                        Width="100%" OnRowDataBound="grdTicket_RowDataBound" GridLines="None" CssClass="regText"
                                        CellPadding="6" CellSpacing="6">
                                        <Columns>
                                            <asp:BoundField DataField="ContactDateEntered" HeaderText="Date" DataFormatString="{0:g}" />
                                            <asp:BoundField DataField="MessageBody" HeaderText="Message" />
                                        </Columns>
                                        <HeaderStyle CssClass="border" HorizontalAlign="Left" />
                                        <RowStyle VerticalAlign="Top" />
                                    </asp:GridView>
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
                <cc1:TabPanel ID="NewNote" runat="server" HeaderText="New Note">
                    <ContentTemplate>
                        <asp:Button ID="btnNew" runat="server" Text="Submit new note" CssClass="button" OnClick="btnNew_Click" /><br />
                        <obout:Editor ID="edTicket" runat="server" Height="300px" Width="600px">
                            <TopToolbar Appearance="Custom">
                                <AddButtons>
                                    <obout:Bold />
                                    <obout:Italic />
                                    <obout:Underline />
                                    <obout:StrikeThrough />
                                    <obout:HorizontalSeparator />
                                    <obout:FontName />
                                    <obout:FontSize />
                                    <obout:VerticalSeparator />
                                    <obout:Undo />
                                    <obout:Redo />
                                    <obout:HorizontalSeparator />
                                    <obout:PasteWord />
                                    <obout:HorizontalSeparator />
                                    <obout:JustifyLeft />
                                    <obout:JustifyCenter />
                                    <obout:JustifyRight />
                                    <obout:JustifyFull />
                                    <obout:HorizontalSeparator />
                                    <obout:SpellCheck />
                                    <custom:ImmediateImageInsert ID="btnImageInsert" runat="server" />
                                </AddButtons>
                            </TopToolbar>
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
                                        Width="100%" OnRowDataBound="grdTicket_RowDataBound" GridLines="None" CssClass="regText"
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
                <cc1:TabPanel ID="AssignEditClose" runat="server" HeaderText="Assign/Edit/Close">
                    <ContentTemplate>
                        <table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
                            <tr bgcolor="#D6B781" class="regText">
                                <td colspan="2">
                                    <b>Assign/Edit/Close </b>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" nowrap="nowrap">
                                    <asp:Label ID="lblAssign" runat="server" Text="Assign to " CssClass="regText"></asp:Label>
                                </td>
                                <td align="left" valign="top">
                                    <asp:DropDownList ID="ddlAssign" runat="server" CssClass="FormField" />
                                    <br />
                                    <asp:RadioButtonList ID="rdoDept" runat="server" OnSelectedIndexChanged="rdoDept_SelectedIndexChanged"
                                        RepeatDirection="Horizontal" AutoPostBack="true" CssClass="regText">
                                        <asp:ListItem Text="Employees" Selected="True" Value="Emp" />
                                        <asp:ListItem Text="Departments" Value="Dept" />
                                    </asp:RadioButtonList>
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
                            </tr>
                            <tr>
                                <td align="left" style="width: 52px; border: 1 solid black;">
                                    <asp:Label ID="lblDueDate" runat="server" Text="Due Date" CssClass="regText" />
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txtDueDate" runat="server" CssClass="regText" /><cc1:CalendarExtender
                                        ID="calDue" runat="server" TargetControlID="txtDueDate" />
                                </td>
                            </tr>
                            <tr>
                                <td align="left" style="width: 52px; border: 1 solid black;">
                                    <asp:Label ID="lblAttachPatient" runat="server" Text="Attach to patient" CssClass="regText" />
                                </td>
                                <td align="left">This will attach the ticket to a patient record, and it will no longer be a staff ticket.<br />
                                    Type in part of a name and get results in the same way as in patient search.<br />
                                    <asp:TextBox runat="server" ID="txtPatient" CausesValidation="false" TabIndex="0"
                                        AutoPostBack="true" OnTextChanged="txtPatient_TextChanged" Width="273px" Height="10px"
                                        CssClass="regText" onkeydown="return (event.keyCode!=13);" />
                                    <cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="3"
                                        ServiceMethod="GetPatientList" ServicePath="~/PatientNamesServ.asmx" TargetControlID="txtPatient" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <div id="pnlReopen" runat="server">
                                        <asp:CheckBox ID="cboReopen" runat="server" Text="Reopen" CssClass="FormField" />
                                    </div>
                                </td>
                            </tr>


                            <tr>
                                <td colspan="2" align="left">
                                    <div id="pnlClose" runat="server">
                                       <%-- <asp:CheckBox ID="cboClose" runat="server" Text="Close" CssClass="regText" /><br />--%>
                                        <strong>Note: This note is ONLY used for a note related to closing the ticket. If you
											do not close the ticket, this note will NOT be saved. You can write notes at the
											New Note tab. </strong>
                                        <div>
                                            <table>

                                                <tr>
                                                    <td colspan="3">
                                                        <asp:Button ID="btnOkClose" Text="Close Ticket" CssClass="button" runat="server" OnClick="btnOkClose_Click" />
                                                        <asp:Button ID="btnCancelClose" Text="Cancel" CssClass="button" runat="server" OnClick="btnCancelClose_Click" />
                                                    </td>
                                                </tr>
                                            </table>


                                        </div>

                                        <obout:Editor ID="edClose" runat="server" Height="300px" Width="600px">
                                            <TopToolbar Appearance="Custom">
                                                <AddButtons>
                                                    <obout:Bold />
                                                    <obout:Italic />
                                                    <obout:Underline />
                                                    <obout:StrikeThrough />
                                                    <obout:HorizontalSeparator />
                                                    <obout:FontName />
                                                    <obout:FontSize />
                                                    <obout:VerticalSeparator />
                                                    <obout:Undo />
                                                    <obout:Redo />
                                                    <obout:HorizontalSeparator />
                                                    <obout:PasteWord />
                                                    <obout:HorizontalSeparator />
                                                    <obout:JustifyLeft />
                                                    <obout:JustifyCenter />
                                                    <obout:JustifyRight />
                                                    <obout:JustifyFull />
                                                    <obout:HorizontalSeparator />
                                                    <obout:SpellCheck />
                                                </AddButtons>
                                            </TopToolbar>
                                        </obout:Editor>
                                    </div>
                                </td>
                            </tr>

                        </table>
                    </ContentTemplate>
                </cc1:TabPanel>
            </cc1:TabContainer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
