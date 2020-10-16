<%@ Page Title="" Language="C#" MasterPageFile="~/sub.master" AutoEventWireup="true"
	CodeFile="Appointments.aspx.cs" Inherits="Appointments" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
	<table width="100%" border="0" cellpadding="6" cellspacing="0" class="border">
		<tr bgcolor="#D6B781" class="regText">
			<td colspan="5">
				<b>Appointments </b>
			</td>
			<td>
				<div align="right">
				</div>
			</td>
		</tr>
		<tr class="regText">
			<td>
				<strong>Apt Date</strong>
			</td>
			<td>
				<b>Apt Type</b>
			</td>
			<td>
				<b>Provider</b>
			</td>
			<td>
				<b>Mark Occurred</b>
			</td>
			<td>
				<strong>Result</strong>
			</td>
			<td>
				&nbsp;
			</td>
		</tr>
		<asp:Repeater ID="rptAppoinments" runat="server">
			<ItemTemplate>
				<tr valign="top" class="regText">
					<td>
						<a href="apt_console.aspx?aptid=<%# Eval("apt_id") %>" target="_top">
							<%# ((DateTime)Eval("ApptStart")).ToShortDateString() + " " + ((DateTime)Eval("ApptStart")).ToShortTimeString() %></a>
					</td>
					<td>
						<a href="apt_console.aspx?aptid=<%# Eval("apt_id") %>" target="_top">
							<%# Eval("TypeName") %></a>
					</td>
					<td>
						<a href="apt_console.aspx?aptid=<%# Eval("apt_id") %>" target="_top">
							<%# Eval("ProviderName") %></a>
					</td>
					<td>
						<a href="occurred.aspx?aptid=<%# Eval("apt_id") %>&PatientID=<%= PatientID %>">Mark Occurred</a>
					</td>
					<td>
						<a href="apt_console.aspx?aptid=<%# Eval("apt_id") %>" target="_top">
							<%# Eval("ResultName") %></a>
					</td>
					<td colspan="2">
					<%# Eval("OVU") != null && (bool)Eval("OVU") ? "[<a href='DictationConsole/OfficeVisitUpdate_Short.aspx?aptid=" + Eval("apt_id")+"&PatientID="+ PatientID  + "'>Add/Edit OVU</a>]" : "" %>
					</td>
				</tr>
			</ItemTemplate>
		</asp:Repeater>
	</table>
</asp:Content>
