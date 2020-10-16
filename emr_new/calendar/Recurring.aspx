<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Recurring.aspx.cs" Inherits="Recurring"
	MasterPageFile="Site.master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">

	<asp:UpdatePanel ID="upd" runat="server">
		<ContentTemplate>
			<table border="0" cellpadding="2" cellspacing="2">
				<caption>
					<h3>
						Recurring Appointment</h3>
				</caption>
				<tr>
					<td>
						Patient
					</td>
					<td align="left">
						<asp:TextBox runat="server" ID="txtPatient" Text='' CausesValidation="true" TabIndex="1"
							AutoPostBack="true" OnTextChanged="txtPatient_TextChanged" />
						<cc1:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" MinimumPrefixLength="1"
							ServiceMethod="GetPatientList" ServicePath="~/PatientNamesServ.asmx" TargetControlID="txtPatient" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Label ID="Label1" runat="server" Text="Provider  " />
						<asp:DropDownList ID="ProviderDropDown" runat="server" DataSourceID="ProvidersSource"
							DataTextField="ProviderName" DataValueField="id" AutoPostBack="true" TabIndex="8"
							OnSelectedIndexChanged="ProviderDropDown_SelectedIndexChanged" OnDataBound="ProviderDropDown_DataBound">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Label ID="Label2" runat="server" Text="Appointment Type" />
						<asp:DropDownList ID="ApptTypeDropDown" runat="server" DataTextField="TypeName" DataValueField="id"
							TabIndex="7">
						</asp:DropDownList>
					</td>
				</tr>
				<tr>
					<td colspan="2" valign="top">
						<asp:Label ID="Label3" runat="server" Text="Repeat every" />
						&nbsp;<asp:DropDownList ID="DropDownList1" runat="server">
							<asp:ListItem Text="1" Selected="True" />
							<asp:ListItem Text="2" />
							<asp:ListItem Text="3" />
							<asp:ListItem Text="4" />
						</asp:DropDownList>
						&nbsp;week(s)<br />
						<asp:CheckBoxList ID="CheckBoxList1" runat="server" RepeatDirection="Vertical">
							<asp:ListItem>Monday</asp:ListItem>
							<asp:ListItem>Tuesday</asp:ListItem>
							<asp:ListItem>Wednesday</asp:ListItem>
							<asp:ListItem>Thursday</asp:ListItem>
							<asp:ListItem>Friday</asp:ListItem>
						</asp:CheckBoxList>
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:CheckBox ID="cbAllDay" runat="server" Text="All Day" />
					</td>
				</tr>
				<tr>
					<td>
						<font color="#666666"><b>Start</b></font>
					</td>
					<td>
						<asp:TextBox runat="server" ID="txtStartDate" Columns="6" Text='<%# Eval("ApptStart") %>'
							CausesValidation="true" AutoPostBack="true" TabIndex="2" />
						<cc1:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtStartDate" />
						&nbsp;
						<asp:TextBox runat="server" ID="txtStartTime" Columns="6" Text='' CausesValidation="true"
							AutoPostBack="true" TabIndex="3" />*
					</td>
				</tr>
				<tr>
					<td>
						<font color="#666666"><b>End</b></font>
					</td>
					<td nowrap="nowrap">
						<asp:TextBox runat="server" ID="txtEndDate" Columns="6" Text='<%# Eval("ApptEnd") %>'
							AutoPostBack="true" TabIndex="4" />&nbsp;
						<cc1:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtEndDate" />
						<asp:TextBox runat="server" ID="txtEndTime" Columns="6" Text='' CausesValidation="true"
							AutoPostBack="true" TabIndex="5" />*
					</td>
				</tr>
				<tr>
					<td colspan="2">
						Note:<br />
						<asp:TextBox ID="txtNote" runat="server" TextMode="MultiLine" />
					</td>
				</tr>
				<tr>
					<td colspan="2">
						<asp:Button ID="Button1" runat="server" Text="Schedule" CausesValidation="true" OnClick="Button1_Click" CssClass="button" />
						<asp:Label ID="lblFinished" runat="server" Text="Appointments scheduled!" Font-Bold="true"
							Font-Size="Larger" Visible="false" />
						<asp:ObjectDataSource ID="ProvidersSource" runat="server" TypeName="Calendar.Providers"
							SelectMethod="getProviderList"></asp:ObjectDataSource>
					</td>
				</tr>
			</table>
		</ContentTemplate>
	</asp:UpdatePanel>
</asp:Content>
