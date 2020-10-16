<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true"
    CodeFile="admin_RemoveRecurring.aspx.cs" Inherits="admin_RemoveRecurring" %>

<%@ Register Assembly="obout_Grid_NET" Namespace="Obout.Grid" TagPrefix="obout" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagName="TDD" TagPrefix="Longevity" Src="~/controls/TimeDropDown.ascx" %>
<%@ Register TagPrefix="obout" Namespace="OboutInc.Calendar2" Assembly="obout_Calendar2_Net" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:UpdatePanel ID="pnl1" runat="server">
        <ContentTemplate>
            <table width="800" border="0" cellpadding="6" cellspacing="0" class="border">
                <tr bgcolor="#D6B781" class="PageTitle">
                    <td colspan="2">Remove Recurring Appointment
                    </td>
                </tr>
                <tr>
                    <td align="right">Provider
                    </td>
                    <td align="left">
                        <%--<asp:DropDownList ID="ddlProvider" runat="server" DataTextField="ProviderName" DataValueField="id" AutoPostBack="true" OnSelectedIndexChanged="NoPreview" />--%>
                        <asp:DropDownList ID="ddlProvider" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Appointment Type
                    </td>
                    <td align="left">
                        <%--<asp:DropDownList ID="ddlApptType" runat="server" DataTextField="TypeName" DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="NoPreview" />--%>
                        <asp:DropDownList ID="ddlApptType" runat="server" OnSelectedIndexChanged="NoPreview" />
                    </td>
                </tr>
                <tr>
                    <td align="right">Beginning Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStart" runat="server" AutoPostBack="true" OnTextChanged="NoPreview" />
                        <obout:Calendar runat="server" DatePickerMode="true" TextBoxId="txtStart" DatePickerSynchronize="true"
                            DatePickerImagePath="~/images/date_picker1.gif" OnDateChanged="NoPreview" AutoPostBack="true" />
                        
                        <asp:RequiredFieldValidator ID="req1" runat="server" ControlToValidate="txtStart"
                            ForeColor="Red" Display="Dynamic" Text="Beginning date is required." />
                    </td>
                </tr>
                <tr>
                    <td align="right">Ending Date
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEnd" runat="server" AutoPostBack="true" OnTextChanged="NoPreview" />
                        <obout:Calendar ID="Calendar1" runat="server" DatePickerMode="true" TextBoxId="txtEnd"
                            DatePickerSynchronize="true" DatePickerImagePath="~/images/date_picker1.gif" OnDateChanged="NoPreview" AutoPostBack="true" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtEnd"
                            ForeColor="Red" Display="Dynamic" Text="End date is required." />
                    </td>
                </tr>
                <tr>
                    <td align="right">Appointment Start Time
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtStartTime" runat="server" Columns="2" AutoPostBack="true" OnTextChanged="NoPreview" />:<asp:TextBox ID="txtStartMinutes"
                            runat="server" Columns="2" Text="00" AutoPostBack="true" OnTextChanged="NoPreview" />
                        (24 hour clock)
						<asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtStartTime"
                            ForeColor="Red" Display="Dynamic" Text="Start hour is required." />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtStartMinutes"
                            ForeColor="Red" Display="Dynamic" Text="Start minutes is required." />
                    </td>
                </tr>
                <tr>
                    <td align="right">Appointment End Time
                    </td>
                    <td align="left">
                        <asp:TextBox ID="txtEndTime" runat="server" Columns="2" AutoPostBack="true" OnTextChanged="NoPreview" />:<asp:TextBox ID="txtEndMinutes"
                            runat="server" Columns="2" Text="00" AutoPostBack="true" OnTextChanged="NoPreview" />
                        (24 hour clock)
						<asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEndTime"
                            ForeColor="Red" Display="Dynamic" Text="End hour is required." />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtEndMinutes"
                            ForeColor="Red" Display="Dynamic" Text="End minutes is required." />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnPreview" runat="server" Text="Preview" CssClass="button" OnClick="UpdatePreview" />
                        <asp:Button ID="btnDelete" runat="server" CssClass="button" Text="Delete all preview items"
                            Visible="false" OnClick="btnDelete_Click" /><br />
                        <obout:Grid ID="grdPreview" runat="server" AutoGenerateColumns="true" AllowPaging="true"
                            PageSize="100" CellPadding="6" CellSpacing="6" CallbackMode="true" Serialize="true"
                            AllowRecordSelection="false" AllowGrouping="false" AllowColumnResizing="false"
                            AllowAddingRecords="false" AllowFiltering="false" FolderStyle="grid_styles/style_1"
                            AllowSorting="true">
                        </obout:Grid>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
